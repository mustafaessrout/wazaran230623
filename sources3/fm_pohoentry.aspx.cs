using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data.SqlClient;
public partial class fm_pohoentry : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            bll.vBindingFieldValueToCombo(ref cbcurrency, "curr_cd");
            bll.vBindingComboToSp(ref cbvendor, "sp_tmst_vendor_get", "vendor_cd", "vendor_nm");
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vDelTwrkPoDtl(arr);
            dtpo.Text = System.DateTime.Today.ToShortDateString();
            cbvendor_SelectedIndexChanged(sender, e);
        }
    }
    protected void cbvendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@vendor_cd", cbvendor.SelectedValue.ToString()));
        SqlDataReader rs = null;
        bll.vGetMstVendor2(arr, ref rs);
        while (rs.Read())
        { 
            lbaddr.Text = rs["address1"].ToString();
            lbcity.Text = rs["city_cd"].ToString();
        } rs.Close();
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetListItem(string prefixText, int count)
    {
        cbll bll = new cbll();
        SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@item_nm", prefixText));
        List<string> lItem = new List<string>();
        bll.vSearchMstItem(arr, ref rs);
        while (rs.Read())
        { lItem.Add(rs["item_nm"].ToString());} rs.Close();
        return (lItem);
    }

    public string GetValue()
    {
        return (txsearchitem.Text);   
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        string sItemCd = bll.vLookUp("select item_cd from tmst_item where item_nm='" + txsearchitem.Text + "'");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@item_cd", sItemCd));
        arr.Add(new cArrayList("@usr_id",Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@qty", txqty.Text));
        bll.vInsertWrkPoDtl(arr); arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grditem, "sp_twrk_podtl_get", arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ccd", "ClearControl()", true);
        lbtotalqty.Text = sQty();
        lbtotalgross.Text = sGross();

        
    }

    string sQty()
    {
        return(bll.vLookUp("select sum(qty) from twrk_podtl where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "'"));

    }

    string sGross()
    {
        return (bll.vLookUp("select sum(a.price_buy * b.qty) from tmst_item a join twrk_podtl b on a.item_cd=b.item_cd where b.usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "'"));
    }
    protected void btsaved_Click(object sender, EventArgs e)
    {
        string sPoNo = "";
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@po_dt", dtpo.Text));
        arr.Add(new cArrayList("@referens", txref.Text));
        arr.Add(new cArrayList("@vendor_cd", cbvendor.SelectedValue.ToString()));
        arr.Add(new cArrayList("@curr_cd", cbcurrency.SelectedValue.ToString()));
        arr.Add(new cArrayList("@remark", txremark.Text));
        arr.Add(new cArrayList("@grandtotal", lbtotalgross.Text));
        arr.Add(new cArrayList("@po_sta_id", "N"));
        bll.vInsertMstPoHO(arr, ref sPoNo);
        txpono.Text = sPoNo;
        arr.Clear();
        arr.Add(new cArrayList("@po_no", sPoNo));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vInsertPoHoDtl(arr);
     //   cd.vSendSms("New PO Head Office Has been created with no. " + sPoNo, "0503743024");
        Response.Redirect("fm_poholist.aspx");

    }
    protected void grditem_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbitemcode = (Label)grditem.Rows[e.RowIndex].FindControl("lbitemcode");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vDelWrkPoDtl(arr); arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grditem, "sp_twrk_podtl_get", arr);
        lbtotalqty.Text = sQty();
        lbtotalgross.Text = sGross();
    }
}