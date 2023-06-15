using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_bankdepositlist : System.Web.UI.Page
{
    cbll bll = new cbll();
    cbll2 bll2 = new cbll2();   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                bll.vBindingFieldValueToComboByQryWithEmptyChoosen(ref cbstatus, "dep_sta_id", "dep_sta_id");
                bll.vBindingComboToSpWithEmptyChoosen(ref cbsalespoint, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm");
                string _salespointho = bll.sGetControlParameter("salespointho");
                if (_salespointho == Request.Cookies["sp"].Value)
                {
                    cd.v_enablecontrol(cbsalespoint);
                }
                else
                {
                    cbsalespoint.SelectedValue = Request.Cookies["sp"].Value;
                    cd.v_disablecontrol(cbsalespoint);
                }
               
                //cbstatus_SelectedIndexChanged(sender, e);
                btnew.Visible = false;
                btrefresh.Visible = false;

            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_bankdepositlist");
                Response.Redirect("fm_ErrorPage.aspx");
            }
        }
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_bankdepositentry.aspx");
    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {

            grd.EditIndex = e.NewEditIndex;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@dep_sta_id", cbstatus.SelectedValue));
            arr.Add(new cArrayList("@salespointcd",cbsalespoint.SelectedValue));
            bll.vBindingGridToSp(ref grd, "sp_tbank_deposit_get", arr);
            DropDownList cbstatusx = (DropDownList)grd.Rows[e.NewEditIndex].FindControl("cbstatus");
            bll.vBindingFieldValueToComboByQryWithEmptyChoosen(ref cbstatusx, "dep_sta_id", "dep_sta_id_app");
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_bankdepositlist");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        finally { ScriptManager.RegisterStartupScript(Page,Page.GetType(),Guid.NewGuid().ToString(),"HideProgress();",true); } 
    }
    protected void cbstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@dep_sta_id", cbstatus.SelectedValue));
            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue));
            bll.vBindingGridToSp(ref grd, "sp_tbank_deposit_get", arr);
            cd.v_disablecontrol(cbsalespoint);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_bankdepositlist");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        finally
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
        }
    }
    protected void btrefresh_Click(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@dep_sta_id", cbstatus.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespointcd",cbsalespoint.SelectedValue));
            bll.vBindingGridToSp(ref grd, "sp_tbank_deposit_get", arr);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_bankdepositlist");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        finally { ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true); }
    }
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grd.PageIndex = e.NewPageIndex;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@dep_sta_id", cbstatus.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue));
            bll.vBindingGridToSp(ref grd, "sp_tbank_deposit_get", arr);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_bankdepositlist");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList cbaction = (DropDownList)e.Row.FindControl("cbaction");

        }
    }

    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        string _salespoint = Request.Cookies["sp"].Value;
        DropDownList cbstatusx = (DropDownList)grd.Rows[e.RowIndex].FindControl("cbstatus");
        HiddenField hddepositid = (HiddenField)grd.Rows[e.RowIndex].FindControl("hddepositid");
        Label lbrefno = (Label)grd.Rows[e.RowIndex].FindControl("lbrefno");
        TextBox dtbank = (TextBox)grd.Rows[e.RowIndex].FindControl("dtbank");
        DateTime _bank_dt = System.DateTime.ParseExact(dtbank.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime _dt = DateTime.ParseExact(Request.Cookies["waz_dt"].Value,"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        if (cbstatusx.SelectedValue == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), 
                "sweetAlert('Please select action !','Approve or Reject Choosen','warning');", true);
            return;
        }
        string _sql = "update tbank_deposit set confirmed_dt=dbo.fn_getsystemdate('"+_salespoint+"'), dep_sta_id='" + cbstatusx.SelectedValue + "' where deposit_id='" + hddepositid.Value + "'";
        bll.vExecuteSQL(_sql);
        _sql = "update tsalesman_balance_deposit set salesdep_sta_id='C' where deposit_cd='"+lbrefno.Text+"'";
        bll.vExecuteSQL(_sql);
        arr.Clear();
        arr.Add(new cArrayList("@deposit_id", hddepositid.Value));
        arr.Add(new cArrayList("@sweeping_dt", _dt));
        arr.Add(new cArrayList("@sweepingby", Request.Cookies["usr_id"].Value));
        arr.Add(new cArrayList("@bank_dt", _bank_dt));
        bll2.vInsertBankDepositInfo(arr);   
        
        grd.EditIndex = -1;
        arr.Clear();
        arr.Add(new cArrayList("@dep_sta_id", cbstatus.SelectedValue));
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue));
        bll.vBindingGridToSp(ref grd, "sp_tbank_deposit_get", arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
            "HideProgress();sweetAlert('Deposit has been aknowloedge !','"+cbstatusx.SelectedItem.Text+" for No."+lbrefno.Text+"','success');", true);
    }

    protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        grd.EditIndex = -1;
        arr.Add(new cArrayList("@dep_sta_id", cbstatus.SelectedValue));
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue));
        bll.vBindingGridToSp(ref grd, "sp_tbank_deposit_get", arr);
    }

    protected void grd_RowCancelingEdit1(object sender, GridViewCancelEditEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        grd.EditIndex = -1;
        arr.Add(new cArrayList("@dep_sta_id", cbstatus.SelectedValue));
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue));
        bll.vBindingGridToSp(ref grd, "sp_tbank_deposit_get", arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
    }

    protected void grd_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                foreach (LinkButton button in e.Row.Cells[13].Controls.OfType<LinkButton>())
                {
                    if ((button.CommandName == "Edit") || (button.CommandName == "Update") ||(button.CommandName=="Cancel"))
                    {
                        button.Attributes.Add("OnClick", "ShowProgress();");
                    }
                }
            HyperLink _url = (HyperLink)e.Row.FindControl("urlfile");
            HiddenField hddepositid = (HiddenField)e.Row.FindControl("hddepositid");
            HiddenField hddeposittype = (HiddenField)e.Row.FindControl("hddeposittype");
            Label lbdepositonumber = (Label)e.Row.FindControl("lbdepositonumber");
            Label lbrefno =  (Label)e.Row.FindControl("lbrefno");
            TextBox dtbank = (TextBox)e.Row.FindControl("dtbank");
            dtbank.Text = Request.Cookies["waz_dt"].Value;
            string _tabno = bll.vLookUp("select tab_deposit_cd from tsalesman_balance_deposit where deposit_cd=" 
               + "(select ref_no from tbank_deposit where deposit_id='" + hddepositid.Value + "')");
            string _tabnopayment = bll.vLookUp("select tab_no from tmst_payment where payment_no='"+lbrefno.Text+"'");
            if (hddeposittype.Value == "CH")
            {
                _url.NavigateUrl = "javascript:popupwindow('lookupfile.aspx?src=" + hddeposittype.Value + "&f=" + _tabno + "')";
            }
            else if ((hddeposittype.Value == "CQ") || (hddeposittype.Value == "BT"))
            {
                _url.NavigateUrl = "javascript:popupwindow('lookupfile.aspx?src=" + hddeposittype.Value + "&f=" + _tabnopayment+ "')";
            }
        }
    }
}