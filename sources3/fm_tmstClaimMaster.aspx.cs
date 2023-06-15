using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_tmstClaimMaster : System.Web.UI.Page
{
    cbll bll = new cbll();

    protected void Page_Load(object sender, EventArgs e)
    {

        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        {



            txname_AutoCompleteExtender.Enabled = true;
            var promokind = Convert.ToString(cbpromokind.SelectedValue);
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@promokind", promokind));
            bll.vBindingGridToSp(ref grdPromotionGroup, "sp_tmst_promotionFull_get", arr);

            arr.Clear();
            bll.vBindingComboToSp(ref ddlPromotionGroup, "sp_tmst_promotionCbo_get", "promo_cd", "promo_nm", arr);

            var promotionGroup = Convert.ToString(ddlPromotionGroup.SelectedValue);
            arr.Clear();
            arr.Add(new cArrayList("@promoCD", promotionGroup));
            bll.vBindingGridToSp(ref gvPromotionType, "sp_tmst_promotionTypeFull_get", arr);

            bll.vBindingComboToSp(ref cbvendor, "sp_tmst_vendor_get", "vendor_cd", "vendor_nm");

            arr.Clear();
            arr.Add(new cArrayList("@position", rdProposalSignByPrincipal.SelectedValue));
            arr.Add(new cArrayList("@vendor_cd", cbvendor.SelectedValue));
            bll.vBindingGridToSp(ref gvProposalPrincipal, "sp_tproposal_signbyvendorFull_get", arr);

            arr.Clear();
            arr.Add(new cArrayList("@job_cd", rdProposalSignBySBTC.SelectedValue));
            bll.vBindingGridToSp(ref gvProposalSBTC, "sp_tproposal_signoffFull_get", arr);

            arr.Clear();
            //arr.Add(new cArrayList("@job_cd", rdProposalSignBySBTC.SelectedValue));
            //bll.vBindingComboToSp(ref cbProposalEmployee, "sp_tproposal_signoff_get", "ids", "emp_desc", arr);
            //bll.vBindingComboToSp(ref cbProposalEmployee, "sp_tmst_employee_get", "emp_cd", "emp_nm");

            arr.Clear();
            bll.vBindingGridToSp(ref gvProposalStatus, "sp_tmst_proposal_status_get", arr);

            bll.vBindingComboToSp(ref cbgroup, "sp_get_issue_group", "issue_group", "issue_group");
            cbgroup_SelectedIndexChanged(sender, e);

        }
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lEmployee = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string sEmployee = string.Empty;
        arr.Add(new cArrayList("@emp_nm", prefixText));
        bll.vSearchMstEmployee(arr, ref rs);
        while (rs.Read())
        {
            sEmployee = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["emp_cd"].ToString() + "-" + rs["emp_nm"], rs["emp_cd"].ToString());
            lEmployee.Add(sEmployee);
        }
        return (lEmployee.ToArray());
    }
    protected void cbpromokind_SelectedIndexChanged(object sender, EventArgs e)
    {
        var promokind = Convert.ToString(cbpromokind.SelectedValue);
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@promokind", promokind));
        bll.vBindingGridToSp(ref grdPromotionGroup, "sp_tmst_promotionFull_get", arr);
    }
    protected void btSavePromotionGroup_Click(object sender, EventArgs e)
    {
        string userid = Convert.ToString(Request.Cookies["usr_id"].Value.ToString());
        if (userid == "2476")
        {
            var promoCode = txtPromo_cd.Text;
            var promoName = txtPromo_nm.Text;
            var promokind = cbpromokind.SelectedValue;
            string retValue = "";
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@promo_cd", promoCode));
            arr.Add(new cArrayList("@promo_nm", promoName));
            arr.Add(new cArrayList("@promokind", promokind));
            bll.vInsPromotionGroup(arr, ref retValue);
            if (retValue == "-2")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Can not insert duplicate promo group " + promoCode + "','Insert fail ','warning');", true);
            }
            else if (retValue == "-3")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Can not insert duplicate promo group information ','Insert fail ','warning');", true);
            }
            else if (retValue == "2")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Record Inserted successfully ','Insert Success','success');", true);
                txtPromo_cd.Text = string.Empty;
                txtPromo_nm.Text = string.Empty;
            }
            arr.Clear();
            arr.Add(new cArrayList("@promokind", promokind));
            bll.vBindingGridToSp(ref grdPromotionGroup, "sp_tmst_promotionFull_get", arr);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You can not access this section. Please contact claim department','Access fail ','warning');", true);
            Response.Redirect("default.aspx?claimMasterAccess=No");
        }
    }
    protected void grdPromotionGroup_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string userid = Convert.ToString(Request.Cookies["usr_id"].Value.ToString());
        if (userid == "2476")
        {
            string retValue = "";
            HiddenField hdPromokind = (HiddenField)grdPromotionGroup.Rows[e.RowIndex].FindControl("hdPromokind");
            Label lblPromo_cd = (Label)grdPromotionGroup.Rows[e.RowIndex].FindControl("lblPromo_cd");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@promo_cd", lblPromo_cd.Text));
            bll.vDelPromotionGroup(arr, ref retValue); arr.Clear();

            if (retValue == "-2")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('This promo group already use. You can not delete.','Delete failed ','warning');", true);
            }
            else if (retValue == "2")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Record deleted successfully','" + lblPromo_cd.Text + "','success');", true);
            }
            arr.Clear();
            arr.Add(new cArrayList("@promokind", Convert.ToString(cbpromokind.SelectedValue)));
            bll.vBindingGridToSp(ref grdPromotionGroup, "sp_tmst_promotionFull_get", arr);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You can not access this section. Please contact claim department','Access fail ','warning');", true);
            Response.Redirect("default.aspx?claimMasterAccess=No");
        }
    }
    protected void grdPromotionGroup_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdPromotionGroup.EditIndex = -1;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Clear();
        arr.Add(new cArrayList("@promokind", Convert.ToString(cbpromokind.SelectedValue)));
        bll.vBindingGridToSp(ref grdPromotionGroup, "sp_tmst_promotionFull_get", arr);
    }
    protected void ddlPromotionGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        var promotionGroup = Convert.ToString(ddlPromotionGroup.SelectedValue);
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@promoCD", promotionGroup));
        bll.vBindingGridToSp(ref gvPromotionType, "sp_tmst_promotionTypeFull_get", arr);
    }
    protected void btnPromotionType_Click(object sender, EventArgs e)
    {
        string userid = Convert.ToString(Request.Cookies["usr_id"].Value.ToString());
        if (userid == "2476")
        {

            var promotionTypeCode = txtPromotionType.Text;
            var promotionTypeName = txtPromotionTypeName.Text;
            var promotionGroup = ddlPromotionGroup.SelectedValue;
            var itemco_cd = Convert.ToString(Regex.Replace(txtPromotionTypeName.Text, @"\s+", ""));

            string retValue = "";
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@promo_typ", promotionTypeCode));
            arr.Add(new cArrayList("@promo_cd", promotionGroup));
            arr.Add(new cArrayList("@promotyp_nm", promotionTypeName));
            arr.Add(new cArrayList("@itemco_cd", itemco_cd));
            bll.vInsPromotionType(arr, ref retValue);
            if (retValue == "-2")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Can not insert duplicate promo group " + promotionTypeCode + "','Insert fail ','warning');", true);
            }
            else if (retValue == "-3")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Can not insert duplicate promo group information ','Insert fail ','warning');", true);
            }
            else if (retValue == "2")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Record Inserted successfully ','Insert Success','success');", true);
                txtPromotionType.Text = string.Empty;
                txtPromotionTypeName.Text = string.Empty;
            }
            arr.Clear();
            arr.Add(new cArrayList("@promoCD", promotionGroup));
            bll.vBindingGridToSp(ref gvPromotionType, "sp_tmst_promotionTypeFull_get", arr);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You can not access this section. Please contact claim department','Access fail ','warning');", true);
            Response.Redirect("default.aspx?claimMasterAccess=No");
        }
    }
    protected void gvPromotionType_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string userid = Convert.ToString(Request.Cookies["usr_id"].Value.ToString());
        if (userid == "2476")
        {
            string retValue = "";
            HiddenField hdfPromoType = (HiddenField)gvPromotionType.Rows[e.RowIndex].FindControl("hdfPromoType");
            HiddenField hdPromokind = (HiddenField)gvPromotionType.Rows[e.RowIndex].FindControl("hdPromokind");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@promotype", hdfPromoType.Value));
            bll.vDelPromotionType(arr, ref retValue); arr.Clear();

            if (retValue == "-2")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('This promo type already use. You can not delete. ','Delete failed ','warning');", true);
            }
            else if (retValue == "2")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Record deleted successfully ','" + hdfPromoType.Value + "','success');", true);
            }
            arr.Clear();
            arr.Add(new cArrayList("@promoCD", ddlPromotionGroup.SelectedValue));
            bll.vBindingGridToSp(ref gvPromotionType, "sp_tmst_promotionTypeFull_get", arr);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You can not access this section. Please contact claim department','Access fail ','warning');", true);
            Response.Redirect("default.aspx?claimMasterAccess=No");
        }
    }
    protected void gvPromotionType_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvPromotionType.EditIndex = -1;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Clear();
        arr.Add(new cArrayList("@promoCD", ddlPromotionGroup.SelectedValue));
        bll.vBindingGridToSp(ref gvPromotionType, "sp_tmst_promotionTypeFull_get", arr);
    }
    protected void cbvendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@position", rdProposalSignByPrincipal.SelectedValue));
        arr.Add(new cArrayList("@vendor_cd", cbvendor.SelectedValue));
        bll.vBindingGridToSp(ref gvProposalPrincipal, "sp_tproposal_signbyvendorFull_get", arr);
    }
    protected void rdProposalSignByPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@position", rdProposalSignByPrincipal.SelectedValue));
        arr.Add(new cArrayList("@vendor_cd", cbvendor.SelectedValue));
        bll.vBindingGridToSp(ref gvProposalPrincipal, "sp_tproposal_signbyvendorFull_get", arr);
    }
    protected void gvProposalPrincipal_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string userid = Convert.ToString(Request.Cookies["usr_id"].Value.ToString());
        if (userid == "2476")
        {
            string retValue = "";
            HiddenField hdfPosition = (HiddenField)gvProposalPrincipal.Rows[e.RowIndex].FindControl("hdfPosition");
            HiddenField hdfidv = (HiddenField)gvProposalPrincipal.Rows[e.RowIndex].FindControl("hdfidv");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@position", hdfPosition.Value));
            arr.Add(new cArrayList("@idv", hdfidv.Value));
            bll.vDelProposalSignByVendor(arr, ref retValue); arr.Clear();

            if (retValue == "-2")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('This proposal principal already use. You can not delete. ','Delete failed ','warning');", true);
            }
            else if (retValue == "2")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Record deleted successfully ','','success');", true);
            }
            arr.Clear();
            arr.Add(new cArrayList("@position", rdProposalSignByPrincipal.SelectedValue));
            arr.Add(new cArrayList("@vendor_cd", cbvendor.SelectedValue));
            bll.vBindingGridToSp(ref gvProposalPrincipal, "sp_tproposal_signbyvendorFull_get", arr);

        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You can not access this section. Please contact claim department','Access fail ','warning');", true);
            Response.Redirect("default.aspx?claimMasterAccess=No");
        }
    }
    protected void gvProposalPrincipal_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvProposalPrincipal.EditIndex = -1;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Clear();
        arr.Add(new cArrayList("@position", rdProposalSignByPrincipal.SelectedValue));
        arr.Add(new cArrayList("@vendor_cd", cbvendor.SelectedValue));
        bll.vBindingGridToSp(ref gvProposalPrincipal, "sp_tproposal_signbyvendorFull_get", arr);

    }
    protected void btnPrincipal_Click(object sender, EventArgs e)
    {
        string userid = Convert.ToString(Request.Cookies["usr_id"].Value.ToString());
        if (userid == "2476")
        {

            string retValue = "";
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@vendor_cd", cbvendor.SelectedValue));
            arr.Add(new cArrayList("@position", rdProposalSignByPrincipal.SelectedValue));
            arr.Add(new cArrayList("@fullname", Convert.ToString(txtProposalPrincipalName.Text)));
            bll.vInsProposalPrincipal(arr, ref retValue);
            if (retValue == "-2")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Can not insert duplicate ','Insert fail ','warning');", true);
            }

            else if (retValue == "2")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Record Inserted successfully ','Insert Success','success');", true);
                txtProposalPrincipalName.Text = string.Empty;
            }
            arr.Clear();
            arr.Add(new cArrayList("@position", rdProposalSignByPrincipal.SelectedValue));
            arr.Add(new cArrayList("@vendor_cd", cbvendor.SelectedValue));
            bll.vBindingGridToSp(ref gvProposalPrincipal, "sp_tproposal_signbyvendorFull_get", arr);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You can not access this section. Please contact claim department','Access fail ','warning');", true);
            Response.Redirect("default.aspx?claimMasterAccess=No");
        }
    }
    protected void gvProposalSBTC_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string userid = Convert.ToString(Request.Cookies["usr_id"].Value.ToString());
        if (userid == "2476")
        {
            string retValue = "";
            HiddenField hdfJobCD = (HiddenField)gvProposalSBTC.Rows[e.RowIndex].FindControl("hdfJobCD");
            HiddenField hdfids = (HiddenField)gvProposalSBTC.Rows[e.RowIndex].FindControl("hdfids");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@job_cd", hdfJobCD.Value));
            arr.Add(new cArrayList("@emp_cd", hdemp.Value));
            arr.Add(new cArrayList("@ids", hdfids.Value));
            bll.vDelProposalSignBySBTC(arr, ref retValue); arr.Clear();

            if (retValue == "-2")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('This proposal principal already use. You can not delete. ','Delete failed ','warning');", true);
            }
            else if (retValue == "2")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Record deleted successfully ','','success');", true);
            }
            arr.Clear();
            arr.Add(new cArrayList("@job_cd", rdProposalSignBySBTC.SelectedValue));
            bll.vBindingGridToSp(ref gvProposalSBTC, "sp_tproposal_signoffFull_get", arr);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You can not access this section. Please contact claim department','Access fail ','warning');", true);
            Response.Redirect("default.aspx?claimMasterAccess=No");
        }
    }
    protected void gvProposalSBTC_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvProposalSBTC.EditIndex = -1;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Clear();
        arr.Add(new cArrayList("@job_cd", rdProposalSignBySBTC.SelectedValue));
        bll.vBindingGridToSp(ref gvProposalSBTC, "sp_tproposal_signoffFull_get", arr);
    }
    protected void rdProposalSignBySBTC_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@job_cd", rdProposalSignBySBTC.SelectedValue));
        bll.vBindingGridToSp(ref gvProposalSBTC, "sp_tproposal_signoffFull_get", arr);

        arr.Clear();
        arr.Add(new cArrayList("@job_cd", rdProposalSignBySBTC.SelectedValue));
        //bll.vBindingComboToSp(ref cbProposalEmployee, "sp_tproposal_signoff_get", "ids", "emp_desc", arr);
    }
    protected void btnSBTCEmployee_Click(object sender, EventArgs e)
    {
        string userid = Convert.ToString(Request.Cookies["usr_id"].Value.ToString());
        if ((userid == "2476") || (userid == "2831"))
        {
            string retValue = "";
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@job_cd", rdProposalSignBySBTC.SelectedValue));
            arr.Add(new cArrayList("@emp_cd", hdemp.Value));

            bll.vInsProposalSBTCEmployee(arr, ref retValue);
            if (retValue == "-2")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Can not insert duplicate ','Insert fail ','warning');", true);
            }

            else if (retValue == "2")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Record Inserted successfully ','Insert Success','success');", true);
            }
            arr.Clear();
            arr.Add(new cArrayList("@job_cd", rdProposalSignBySBTC.SelectedValue));
            bll.vBindingGridToSp(ref gvProposalSBTC, "sp_tproposal_signoffFull_get", arr);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You can not access this section. Please contact claim department','Access fail ','warning');", true);
            Response.Redirect("default.aspx?claimMasterAccess=No");
        }
    }
    protected void btProposalStatus_Click(object sender, EventArgs e)
    {
        string userid = Convert.ToString(Request.Cookies["usr_id"].Value.ToString());
        if (userid == "2476")
        {
            string retValue = "";
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@status_nm", txtProposalStatus.Text));

            bll.vInsProposalStatus(arr, ref retValue);
            if (retValue == "-2")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Can not insert duplicate ','Insert fail ','warning');", true);
            }

            else if (retValue == "2")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Record Inserted successfully ','Insert Success','success');", true);
            }
            arr.Clear();
            bll.vBindingGridToSp(ref gvProposalStatus, "sp_tmst_proposal_status_get", arr);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You can not access this section. Please contact claim department','Access fail ','warning');", true);
            Response.Redirect("default.aspx?claimMasterAccess=No");
        }
    }
    protected void gvProposalStatus_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string userid = Convert.ToString(Request.Cookies["usr_id"].Value.ToString());
        if (userid == "2476")
        {
            string retValue = "";
            HiddenField hdStatusNo = (HiddenField)gvProposalStatus.Rows[e.RowIndex].FindControl("hdStatusNo");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@status_no", hdStatusNo.Value));
            bll.vDelProposalStatus(arr, ref retValue); arr.Clear();

            if (retValue == "-2")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('This status already use. You can not delete. ','Delete failed ','warning');", true);
            }
            else if (retValue == "2")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Record deleted successfully ','','success');", true);
            }
            arr.Clear();
            bll.vBindingGridToSp(ref gvProposalStatus, "sp_tmst_proposal_status_get", arr);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You can not access this section. Please contact claim department','Access fail ','warning');", true);
            Response.Redirect("default.aspx?claimMasterAccess=No");
        }
    }
    protected void gvProposalStatus_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvProposalStatus.EditIndex = -1;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Clear();
        bll.vBindingGridToSp(ref gvProposalStatus, "sp_tmst_proposal_status_get", arr);
    }

    protected void cbgroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@Issue_group", cbgroup.SelectedValue));
        bll.vBindingGridToSp(ref gvIssue, "sp_tmst_issueFull_get", arr);
    }


    protected void btnIssue_Click(object sender, EventArgs e)
    {
        string userid = Convert.ToString(Request.Cookies["usr_id"].Value.ToString());
        if (userid == "2476")
        {
            string retValue = "";
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@Issue_No", txtIssueCode.Text));
            arr.Add(new cArrayList("@Issue_group", cbgroup.SelectedValue));
            arr.Add(new cArrayList("@IssueName", txtSubGroup.Text));

            bll.vInsProposalIssue(arr, ref retValue);
            if (retValue == "-2")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Can not insert duplicate ','Insert fail ','warning');", true);
            }
            else if (retValue == "-3")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Can not insert duplicate ','Insert fail ','warning');", true);
            }
            else if (retValue == "2")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Record Inserted successfully ','Insert Success','success');", true);
            }
            arr.Clear();
            arr.Add(new cArrayList("@Issue_group", cbgroup.SelectedValue));
            bll.vBindingGridToSp(ref gvIssue, "sp_tmst_issueFull_get", arr);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You can not access this section. Please contact claim department','Access fail ','warning');", true);
            Response.Redirect("default.aspx?claimMasterAccess=No");
        }
    }
    protected void gvIssue_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string userid = Convert.ToString(Request.Cookies["usr_id"].Value.ToString());
        if (userid == "2476")
        {
            string retValue = "";
            HiddenField hdfIssue_No = (HiddenField)gvIssue.Rows[e.RowIndex].FindControl("hdfIssue_No");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@issue_no", hdfIssue_No.Value));
            bll.vDelProposalIssue(arr, ref retValue); arr.Clear();

            if (retValue == "-2")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('This status already use. You can not delete. ','Delete failed ','warning');", true);
            }
            else if (retValue == "2")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Record deleted successfully ','','success');", true);
            }
            arr.Clear();
            arr.Add(new cArrayList("@Issue_group", cbgroup.SelectedValue));
            bll.vBindingGridToSp(ref gvIssue, "sp_tmst_issueFull_get", arr);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You can not access this section. Please contact claim department','Access fail ','warning');", true);
            Response.Redirect("default.aspx?claimMasterAccess=No");
        }
    }
    protected void gvIssue_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvIssue.EditIndex = -1;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Clear();
        arr.Add(new cArrayList("@Issue_group", cbgroup.SelectedValue));
        bll.vBindingGridToSp(ref gvIssue, "sp_tmst_issueFull_get", arr);
    }
}