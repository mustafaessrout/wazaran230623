using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class fm_cndnCustomerApproved : System.Web.UI.Page
{
    cbll bll = new cbll();
    cdal cdl = new cdal();
    Utitlity ut = new Utitlity();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingComboToSpWithEmptyChoosen(ref cbsalespoint, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm");
            bll.vBindingGridToSp(ref grd, "sp_tmst_cndncustomer_getNew");
            //List<cArrayList> arr = new List<cArrayList>();
            //arr.Add(new cArrayList("doc_typ", "cndncustomer"));
            //arr.Add(new cArrayList("@level_no", 1));
            //arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue));
            //bll.vBindingComboToSpWithEmptyChoosen(ref ddlApprove, "sp_tapprovalpattern_getbytype","emp_cd","emp_nm", arr);

        }
    }

    protected void ddlApprove_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lblCNDN_no = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lblCNDN_no");
        HiddenField hdfstatus = (HiddenField)grd.Rows[e.NewSelectedIndex].FindControl("hdfstatus");
        hdcndnCust.Value = Convert.ToString(lblCNDN_no.Text);
        if (hdfstatus.Value == "N")
        {
            btnApproved.Visible = true;
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please select another DN for approved!!','Wrong Selection','warning');", true);
            return;
        }
    }

    protected void btnApproved_Click(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@cndn_no", hdcndnCust.Value));
            arr.Add(new cArrayList("@cndncust_sta_id", "A"));
            arr.Add(new cArrayList("@updatedBy", Convert.ToString(Request.Cookies["usr_id"].Value)));
            arr.Add(new cArrayList("@updatMethod", "email"));

            bll.vUpdtDNCutomerApp(arr);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert(' Approved Successfully ! ','" + hdcndnCust.Value + "','success');", true);
            
            bll.vBindingGridToSp(ref grd, "sp_tmst_cndncustomer_getNew");
            btnApproved.Visible = false;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('" + ex.Message + ex.InnerException + "','"+ ex.Message + ex.InnerException + "','warning');", true);
            ut.Logs("", "Account", "DN Approved", "fm_cndnCustomerApproved", "btnApproved_Click", "Exception", ex.Message + ex.InnerException);
        }
    }

    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grd.EditIndex = e.NewEditIndex;
        bll.vBindingGridToSp(ref grd, "sp_tmst_cndncustomer_getNew");
        Label lblCNDN_no = (Label)grd.Rows[e.NewEditIndex].FindControl("lblCNDN_no");
        DropDownList cbstatus = (DropDownList)grd.Rows[e.NewEditIndex].FindControl("cbstatus");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@qry_cd", "cndncust_sta_id"));
        arr.Add(new cArrayList("@fld_nm", "cndncust_sta_id"));
        bll.vBindingFieldValueToComboByQryWithEmptyChoosen(ref cbstatus,"cndncust_sta_id", "cndncust_sta_id");
    }

    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        DropDownList cbstatus = (DropDownList)grd.Rows[e.RowIndex].FindControl("cbstatus");
        Label lblCNDN_no = (Label)grd.Rows[e.RowIndex].FindControl("lblCNDN_no");
        string _sql = "update tmst_cndncustomer set cndncust_sta_id='"+cbstatus.SelectedValue+"', post_dt='" + System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value,"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)+"' where cndn_no='"+lblCNDN_no.Text+"'";
        bll.vExecuteSQL(_sql);
        grd.EditIndex = -1;
        bll.vBindingGridToSp(ref grd, "sp_tmst_cndncustomer_getNew");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), 
            "sweetAlert('CNDN Customer has been approved!','"+lblCNDN_no.Text+"','success');", true);
    }

    protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grd.EditIndex = -1;
        bll.vBindingGridToSp(ref grd, "sp_tmst_cndncustomer_getNew");
    }

    protected void cbsalespoint_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue));
        arr.Add(new cArrayList("@level_no", 1));
        arr.Add(new cArrayList("@doc_typ", "cndncustomer"));
        bll.vBindingComboToSpWithEmptyChoosen(ref ddlApprove, "sp_tapprovalpattern_getbytype", "emp_cd", "emp_nm", arr);
    }
}