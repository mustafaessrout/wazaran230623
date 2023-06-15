using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_reqitemlist : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@item_sta_id", "N"));
            bll.vBindingGridToSp(ref grd, "sp_tmst_item_get_by_sta", arr);
            bll.vBindingFieldValueToCombo(ref cbuom, "uom");
            bll.vBindingFieldValueToCombo(ref cbprodtype, "prod_typ");
            bll.vBindingFieldValueToCombo(ref cbpacking, "packing");
            bll.vBindingComboToSp(ref cbvendor, "sp_tmst_vendor_get", "vendor_cd", "vendor_nm");
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grd.EditIndex = e.NewEditIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@item_sta_id", "N"));
        bll.vBindingGridToSp(ref grd, "sp_tmst_item_get_by_sta", arr);
        TextBox txitemno = (TextBox)grd.Rows[e.NewEditIndex].FindControl("txitemno");
        txitemno.Focus();
    }
    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        TextBox txitemno = (TextBox)grd.Rows[e.RowIndex].FindControl("txitemno");
        Label lbrequestno = (Label)grd.Rows[e.RowIndex].FindControl("lbrequestno");
        if ((bll.nCheckItemExist(txitemno.Text) == 1) || (txitemno.Text==""))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "alert('Item code : " + txitemno.Text +  " already registered or empty, please try again !');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@item_cd", txitemno.Text));
        arr.Add(new cArrayList("@item_sta_id", "W"));
        arr.Add(new cArrayList("@request_no", lbrequestno.Text));
        bll.vUpdateMstItem(arr);
        grd.EditIndex = -1;
        arr.Clear();
        arr.Add(new cArrayList("@item_sta_id", "N"));
        bll.vBindingGridToSp(ref grd, "sp_tmst_item_get_by_sta", arr);
        //Sending SMS to second approval
        Random rnd= new Random();
        int token = rnd.Next(1000,9999);
        List<string> lapproval = bll.lGetApproval("item", 2);
        string stoken = bll.vLookUp("select short_ip from tmst_salespoint where salespointcd=(select parm_valu from tcontrol_parameter where parm_nm='salespoint')") + token.ToString();
        cd.vSendSms("New request item has request with item code : " + txitemno.Text + ", do you want to approve ? Please repply with (Y/N)" + stoken.ToString(), lapproval[0]);
        arr.Clear();
        arr.Add(new cArrayList("@doc_typ", "item"));
        arr.Add(new cArrayList("@receiver", lapproval[0]));
        arr.Add(new cArrayList("@token", stoken.ToString()));
        arr.Add(new cArrayList("@doc_no", txitemno.Text));
        bll.vInsertSMSSent(arr);
    }

    protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grd.EditIndex = -1;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@item_sta_id", "N"));
        bll.vBindingGridToSp(ref grd, "sp_tmst_item_get_by_sta", arr);
    
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        System.Data.SqlClient.SqlDataReader rs=null;
        List<cArrayList> arr = new List<cArrayList>();
        Label lbitemcode = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbrequestno");
        arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
        bll.vGetMstItem2(arr, ref rs);
        while (rs.Read())
        { 
            if (rs["item_shortname"] != DBNull.Value)
            {
            txshortname.Text =   rs["shortname"].ToString();
            }

            cbuom.SelectedValue = bll.sCheckNull(rs["uom_base"]);
            cbpacking.SelectedValue = bll.sCheckNull(rs["packing"]);
            cbprodtype.SelectedValue = bll.sCheckNull(rs["prod_typ"]);
            txremark.Text = bll.sCheckNull(rs["remark"]);
            cbvendor.SelectedValue = bll.sCheckNull(rs["vendor_cd"]);
           
        }
        rs.Close();

    }
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd.PageIndex = e.NewPageIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@item_sta_id", "N"));
        bll.vBindingGridToSp(ref grd, "sp_tmst_item_get_by_sta", arr);
        
    }
}