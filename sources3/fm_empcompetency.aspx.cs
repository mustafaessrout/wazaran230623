using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppClassTools;
using System.Data;

public partial class fm_empcompetency : System.Web.UI.Page
{
    cbll bll = new cbll();

    AppClass app = new AppClass();
    Utitlity ut = new Utitlity();
    cdal cdl = new cdal();

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Visible = false;
        if (!IsPostBack)
        {
            try
            {
                bll.vBindingComboToSp(ref cbperiod, "sp_tmst_period_get", "period_cd", "period_nm");
                cbperiod.SelectedValue = bll.vLookUp("select dbo.fn_getcontrolparameter('period')");
                bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm");
                bll.vBindingFieldValueToCombo(ref cbjobtitle, "job_title_cd");
                cbjobtitle_SelectedIndexChanged(sender, e);
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                //bll.vDelWrkAppraisal(arr);
            }
            catch (Exception ex)
            {
                app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
                ut.Logs("", "Appraisal", "APPRAISAL Form", "fm_empcompetency", "Page_Load", "Exception", ex.Message + ex.InnerException);
            }
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);

    }
    protected void cbjobtitle_SelectedIndexChanged(object sender, EventArgs e)
    {
        vBindingCboEmp();
        vBindingAllGrid();
        cbemployee_SelectedIndexChanged(sender, e);
    }

    void vBindingAllGrid()
    {
        try
        {
            int month = Convert.ToInt32(Convert.ToString(cbperiod.SelectedValue).Substring(4, 2));
            int year = Convert.ToInt32(Convert.ToString(cbperiod.SelectedValue).Substring(0, 4));
            var lastDateValue = DateTime.DaysInMonth(year, month);

            DateTime lastDate = Convert.ToDateTime(Convert.ToString(month) + "/" + Convert.ToString(lastDateValue) + "/" + Convert.ToString(year));
            DateTime fisrtDate = Convert.ToDateTime(Convert.ToString(month) + "/" + Convert.ToString(1) + "/" + Convert.ToString(year));

            List<cArrayList> arr = new List<cArrayList>();
            arr.Clear();

            if (Convert.ToString(cbjobtitle.SelectedValue) == "5")
            {
                arr.Add(new cArrayList("@appraisal_cat", "KPI"));
                arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
                arr.Add(new cArrayList("@salesman_cd", Convert.ToString(cbemployee.SelectedValue)));
                arr.Add(new cArrayList("@startDate", fisrtDate));
                arr.Add(new cArrayList("@enddate", lastDate));
                arr.Add(new cArrayList("@period", Convert.ToString(cbperiod.SelectedValue)));
                arr.Add(new cArrayList("@salespoint_cd", Convert.ToString(cbsalespoint.SelectedValue)));
                bll.vBindingGridToSp(ref grdkpi, "sp_tmst_appraisal_driver_get", arr);
                arr.Clear();
            }
            else
            {
                arr.Add(new cArrayList("@appraisal_cat", "KPI"));
                arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
                arr.Add(new cArrayList("@salespoint_cd", Convert.ToString(cbsalespoint.SelectedValue)));
                arr.Add(new cArrayList("@period", Convert.ToString(cbperiod.SelectedValue)));
                arr.Add(new cArrayList("@nationality", Convert.ToString(txtNationality.Text)));

                bll.vBindingGridToSp(ref grdkpi, "sp_tmst_appraisal_get", arr);
                arr.Clear();
            }

            arr.Add(new cArrayList("@appraisal_cat", "CMP"));
            arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespoint_cd", Convert.ToString(cbsalespoint.SelectedValue)));
            arr.Add(new cArrayList("@period", Convert.ToString(cbperiod.SelectedValue)));
            arr.Add(new cArrayList("@nationality", Convert.ToString(txtNationality.Text)));
            bll.vBindingGridToSp(ref grdcompetency, "sp_tmst_appraisal_get", arr);

            arr.Clear();
            arr.Add(new cArrayList("@appraisal_cat", "KR"));
            arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespoint_cd", Convert.ToString(cbsalespoint.SelectedValue)));
            arr.Add(new cArrayList("@period", Convert.ToString(cbperiod.SelectedValue)));
            arr.Add(new cArrayList("@nationality", Convert.ToString(txtNationality.Text)));
            bll.vBindingGridToSp(ref grdkr, "sp_tmst_appraisal_get", arr);
        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "APPRAISAL", "APPRAISAL FORM", "fm_empcompetency", "vBindingAllGrid", "Exception", ex.Message + ex.InnerException);
        }
    }

    void vBindingCboEmp()
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();

            arr.Add(new cArrayList("@qry_cd", "driver"));
            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
            bll.vBindingComboToSp(ref cbemployee, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);

            //arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
            //arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
            //bll.vBindingComboToSp(ref cbemployee, "sp_tmst_employee_getbyjobtitle", "emp_cd", "emp_desc", arr);

        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "APPRAISAL", "APPRAISAL FORM", "fm_empcompetency", "vBindingCboEmp", "Exception", ex.Message + ex.InnerException);
        }
    }

    void vBindingGrid()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
        //bll.vBindingGridToSp(ref grd, "sp_tmst_competency_getbyjobtitle", arr);
    }

    protected void btsave_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('KPI Rating has been saved !','" + cbemployee.SelectedItem.Text + "','success');", true);
    }

    protected void grdkpi_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            grdkpi.EditIndex = e.NewEditIndex;
            List<cArrayList> arr = new List<cArrayList>();
            int month = Convert.ToInt32(Convert.ToString(cbperiod.SelectedValue).Substring(4, 2));
            int year = Convert.ToInt32(Convert.ToString(cbperiod.SelectedValue).Substring(0, 4));
            var lastDateValue = DateTime.DaysInMonth(year, month);
            DateTime lastDate = Convert.ToDateTime(Convert.ToString(month) + "/" + Convert.ToString(lastDateValue) + "/" + Convert.ToString(year));
            DateTime fisrtDate = Convert.ToDateTime(Convert.ToString(month) + "/" + Convert.ToString(1) + "/" + Convert.ToString(year));
            if (Convert.ToString(cbjobtitle.SelectedValue) == "5")
            {
                arr.Add(new cArrayList("@appraisal_cat", "KPI"));
                arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
                arr.Add(new cArrayList("@salesman_cd", Convert.ToString(cbemployee.SelectedValue)));
                arr.Add(new cArrayList("@startDate", fisrtDate));
                arr.Add(new cArrayList("@enddate", lastDate));
                arr.Add(new cArrayList("@period", Convert.ToString(cbperiod.SelectedValue)));
                arr.Add(new cArrayList("@salespoint_cd", Convert.ToString(cbsalespoint.SelectedValue)));
                bll.vBindingGridToSp(ref grdkpi, "sp_tmst_appraisal_driver_get", arr);
                arr.Clear();
            }
            else
            {
                arr.Add(new cArrayList("@appraisal_cat", "KPI"));
                arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
                arr.Add(new cArrayList("@salespoint_cd", Convert.ToString(cbsalespoint.SelectedValue)));
                arr.Add(new cArrayList("@period", Convert.ToString(cbperiod.SelectedValue)));
                arr.Add(new cArrayList("@nationality", Convert.ToString(txtNationality.Text)));
                bll.vBindingGridToSp(ref grdkpi, "sp_tmst_appraisal_get", arr);
                arr.Clear();
            }
            DropDownList cbpoint = (DropDownList)grdkpi.Rows[e.NewEditIndex].FindControl("cbpoint");
            bll.vBindingFieldValueToCombo(ref cbpoint, "point");
        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "APPRAISAL", "APPRAISAL FORM", "fm_empcompetency", "grdkpi_RowEditing", "Exception", ex.Message + ex.InnerException);
        }
    }

    protected void grdkr_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            grdkr.EditIndex = e.NewEditIndex;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@appraisal_cat", "KR"));
            arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespoint_cd", Convert.ToString(cbsalespoint.SelectedValue)));
            arr.Add(new cArrayList("@period", Convert.ToString(cbperiod.SelectedValue)));
            arr.Add(new cArrayList("@nationality", Convert.ToString(txtNationality.Text)));
            bll.vBindingGridToSp(ref grdkr, "sp_tmst_appraisal_get", arr);
            DropDownList cbpoint = (DropDownList)grdkr.Rows[e.NewEditIndex].FindControl("cbpoint");
            bll.vBindingFieldValueToCombo(ref cbpoint, "point");
        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "APPRAISAL", "APPRAISAL FORM", "fm_empcompetency", "grdkr_RowEditing", "Exception", ex.Message + ex.InnerException);
        }
    }
    protected void grdcompetency_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            grdcompetency.EditIndex = e.NewEditIndex;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@appraisal_cat", "CMP"));
            arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespoint_cd", Convert.ToString(cbsalespoint.SelectedValue)));
            arr.Add(new cArrayList("@period", Convert.ToString(cbperiod.SelectedValue)));
            arr.Add(new cArrayList("@nationality", Convert.ToString(txtNationality.Text)));
            bll.vBindingGridToSp(ref grdcompetency, "sp_tmst_appraisal_get", arr);
            DropDownList cbpoint = (DropDownList)grdcompetency.Rows[e.NewEditIndex].FindControl("cbpoint");
            bll.vBindingFieldValueToCombo(ref cbpoint, "point");
        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "APPRAISAL", "APPRAISAL FORM", "fm_empcompetency", "grdcompetency_RowEditing", "Exception", ex.Message + ex.InnerException);
        }
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                "openreport('fm_report2.aspx?src=Appraisal&job_title_cd=" + cbjobtitle.SelectedValue.ToString() +
                "&period=" + Convert.ToString(cbperiod.SelectedValue) +
                "&salespoint_cd=" + Convert.ToString(cbsalespoint.SelectedValue) +
                "&salesman_cd=" + Convert.ToString(cbemployee.SelectedValue) + "');", true);
        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "APPRAISAL", "APPRAISAL FORM", "fm_empcompetency", "btprint_Click", "Exception", ex.Message + ex.InnerException);
        }
    }
    protected void btnOldReport_Click(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                "openreport('fm_report2.aspx?src=OldAppraisal&job_title_cd=" + cbjobtitle.SelectedValue.ToString() +
                "&period=" + Convert.ToString(cbperiod.SelectedValue) +
                "&salespoint_cd=" + Convert.ToString(cbsalespoint.SelectedValue) +
                "&salesman_cd=" + Convert.ToString(cbemployee.SelectedValue) + "');", true);
        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "APPRAISAL", "APPRAISAL FORM", "fm_empcompetency", "btprint_Click", "Exception", ex.Message + ex.InnerException);
        }
    }

    protected void btnIncentive_Click(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                "openreport('fm_report2.aspx?src=Incentive&job_title_cd=" + cbjobtitle.SelectedValue.ToString() +
                "&period=" + Convert.ToString(cbperiod.SelectedValue) +
                "&salespoint_cd=" + Convert.ToString(cbsalespoint.SelectedValue) +
                "&salesman_cd=" + Convert.ToString(cbemployee.SelectedValue) + "');", true);
        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "APPRAISAL", "APPRAISAL FORM", "fm_empcompetency", "btnIncentive_Click", "Exception", ex.Message + ex.InnerException);
        }
    }

    protected void grdkr_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            HiddenField hdappraisal = (HiddenField)e.Row.FindControl("hdappraisal");
            HiddenField hdfPoint = (HiddenField)e.Row.FindControl("hdfPoint");
            if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate))
            {
                string sPoint = string.Empty;
                Label lbpoint = (Label)e.Row.FindControl("lbpoint");
                //if (hdappraisal.Value.ToString() != "")
                //{
                //    sPoint = bll.vLookUp("select dbo.fn_getwrkappraisalpoint('" + Request.Cookies["usr_id"].Value.ToString() + "','" + hdappraisal.Value.ToString() + "')");
                //}
                //else { sPoint = "N"; }
                //lbpoint.Text = bll.sGetFieldValue("point", sPoint);
                DropDownList cbpoint = (DropDownList)e.Row.FindControl("cbpoint");
                if (cbpoint != null)
                {
                    bll.vBindingFieldValueToCombo(ref cbpoint, "point");
                    cbpoint.SelectedValue = Convert.ToString(hdfPoint.Value);
                }
                lbpoint.Text = bll.sGetFieldValue("point", Convert.ToString(hdfPoint.Value));
            }
            else if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
            {
                string sPoint = string.Empty;
                Label lbpoint = (Label)e.Row.FindControl("lbpoint");
                DropDownList cbpoint = (DropDownList)e.Row.FindControl("cbpoint");
                if (cbpoint != null)
                {
                    bll.vBindingFieldValueToCombo(ref cbpoint, "point");
                    cbpoint.SelectedValue = Convert.ToString(hdfPoint.Value);

                }
                if (lbpoint != null)
                {
                    lbpoint.Text = bll.sGetFieldValue("point", Convert.ToString(hdfPoint.Value));
                }
                cbpoint.SelectedValue = Convert.ToString(hdfPoint.Value);

            }
        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "APPRAISAL", "APPRAISAL FORM", "fm_empcompetency", "grdkr_RowDataBound", "Exception", ex.Message + ex.InnerException);
        }
    }
    protected void grdkr_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            HiddenField hdappraisal = (HiddenField)grdkr.Rows[e.RowIndex].FindControl("hdappraisal");
            DropDownList cbpoint = (DropDownList)grdkr.Rows[e.RowIndex].FindControl("cbpoint");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@appraisal_id", hdappraisal.Value.ToString()));
            arr.Add(new cArrayList("@point", cbpoint.SelectedValue.ToString()));
            bll.vInsertWrkAppraisal(arr);
            grdkr.EditIndex = -1;
            arr.Clear();
            arr.Add(new cArrayList("@appraisal_cat", "KR"));
            arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespoint_cd", Convert.ToString(cbsalespoint.SelectedValue)));
            arr.Add(new cArrayList("@period", Convert.ToString(cbperiod.SelectedValue)));
            arr.Add(new cArrayList("@nationality", Convert.ToString(txtNationality.Text)));
            bll.vBindingGridToSp(ref grdkr, "sp_tmst_appraisal_get", arr);
            //vBindingGrid();
        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "APPRAISAL", "APPRAISAL FORM", "fm_empcompetency", "grdkr_RowUpdating", "Exception", ex.Message + ex.InnerException);
        }
    }
    protected void grdkr_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            grdkr.EditIndex = -1;
            arr.Add(new cArrayList("@appraisal_cat", "KR"));
            arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespoint_cd", Convert.ToString(cbsalespoint.SelectedValue)));
            arr.Add(new cArrayList("@period", Convert.ToString(cbperiod.SelectedValue)));
            arr.Add(new cArrayList("@nationality", Convert.ToString(txtNationality.Text)));
            bll.vBindingGridToSp(ref grdkr, "sp_tmst_appraisal_get", arr);
        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "APPRAISAL", "APPRAISAL FORM", "fm_empcompetency", "grdkr_RowCancelingEdit", "Exception", ex.Message + ex.InnerException);
        }
    }
    protected void grdkpi_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            HiddenField hdappraisal = (HiddenField)e.Row.FindControl("hdappraisal");
            HiddenField hdfTotalInvoice = (HiddenField)e.Row.FindControl("hdfTotalInvoice");
            HiddenField hdfTotalQty = (HiddenField)e.Row.FindControl("hdfTotalQty");
            HiddenField hdfTragetInvoice = (HiddenField)e.Row.FindControl("hdfTragetInvoice");
            HiddenField hdfTragetQty = (HiddenField)e.Row.FindControl("hdfTragetQty");
            HiddenField hdfPoint = (HiddenField)e.Row.FindControl("hdfPoint");
            if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate))
            {
                string sPoint = string.Empty; string sTargetInv = string.Empty; string sTargetQty = string.Empty;
                Label lbpoint = (Label)e.Row.FindControl("lbpoint");
                Label lbtarget = (Label)e.Row.FindControl("lbtarget");
                Label lbactual = (Label)e.Row.FindControl("lbactual");
                Label lbappraisal = (Label)e.Row.FindControl("lbappraisal");
                Label lbpct = (Label)e.Row.FindControl("lbpct");
                DropDownList cbpoint = (DropDownList)e.Row.FindControl("cbpoint");
                if (cbpoint != null)
                {
                    bll.vBindingFieldValueToCombo(ref cbpoint, "point");
                    cbpoint.SelectedValue = Convert.ToString(hdfPoint.Value);
                }
                if (hdappraisal.Value.ToString() != "")
                {
                    sPoint = bll.vLookUp("select dbo.fn_getwrkappraisalpoint('" + Request.Cookies["usr_id"].Value.ToString() + "','" + hdappraisal.Value.ToString() + "')");
                }
                else { sPoint = "N"; }
                //lbpoint.Text = bll.sGetFieldValue("point", sPoint);
                lbpoint.Text = bll.sGetFieldValue("point", Convert.ToString(hdfPoint.Value));

                if (lbappraisal.Text == "KPI QTY")
                {
                    lbtarget.Text = Convert.ToString(hdfTragetQty.Value);// bll.fn_gettargetdriver(hdappraisal.Value.ToString(), cbperiod.SelectedValue.ToString(), cbemployee.SelectedValue.ToString()).ToString();
                    lbactual.Text = Convert.ToString(hdfTotalQty.Value);
                    double dactual = Convert.ToDouble(lbactual.Text);
                    double dtarget = Convert.ToDouble(lbtarget.Text);
                    double dPct = (dactual / dtarget) * 100;
                    lbpct.Text = dPct.ToString("#,##0.00") + " %";
                }
                else if (lbappraisal.Text == "KPI Invoice")
                {
                    lbtarget.Text = Convert.ToString(hdfTragetInvoice.Value);//bll.fn_gettargetdriver(hdappraisal.Value.ToString(), cbperiod.SelectedValue.ToString(), cbemployee.SelectedValue.ToString()).ToString();
                    lbactual.Text = Convert.ToString(hdfTotalInvoice.Value); ;
                    double dactual = Convert.ToDouble(lbactual.Text);
                    double dtarget = Convert.ToDouble(lbtarget.Text);
                    double dPct = (dactual / dtarget) * 100;
                    lbpct.Text = dPct.ToString("#,##0.00") + " %";
                }

            }
            else if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
            {
                string sPoint = string.Empty;
                DropDownList cbpoint = (DropDownList)e.Row.FindControl("cbpoint");
                if (cbpoint != null)
                {
                    bll.vBindingFieldValueToCombo(ref cbpoint, "point");
                    cbpoint.SelectedValue = Convert.ToString(hdfPoint.Value);

                }
                if (hdappraisal.Value.ToString() != "")
                {
                    Label lbpoint = (Label)e.Row.FindControl("lbpoint");
                    Label lbtarget = (Label)e.Row.FindControl("lbtarget");
                    Label lbactual = (Label)e.Row.FindControl("lbactual");
                    Label lbappraisal = (Label)e.Row.FindControl("lbappraisal");
                    Label lbpct = (Label)e.Row.FindControl("lbpct");
                    if (lbappraisal.Text == "KPI QTY")
                    {
                        lbtarget.Text = Convert.ToString(hdfTragetQty.Value);// bll.fn_gettargetdriver(hdappraisal.Value.ToString(), cbperiod.SelectedValue.ToString(), cbemployee.SelectedValue.ToString()).ToString();
                        lbactual.Text = Convert.ToString(hdfTotalQty.Value);
                        double dactual = Convert.ToDouble(lbactual.Text);
                        double dtarget = Convert.ToDouble(lbtarget.Text);
                        double dPct = (dactual / dtarget) * 100;
                        lbpct.Text = dPct.ToString("#,##0.00") + " %";
                    }
                    else if (lbappraisal.Text == "KPI Invoice")
                    {
                        lbtarget.Text = Convert.ToString(hdfTragetInvoice.Value);//bll.fn_gettargetdriver(hdappraisal.Value.ToString(), cbperiod.SelectedValue.ToString(), cbemployee.SelectedValue.ToString()).ToString();
                        lbactual.Text = Convert.ToString(hdfTotalInvoice.Value); ;
                        double dactual = Convert.ToDouble(lbactual.Text);
                        double dtarget = Convert.ToDouble(lbtarget.Text);
                        double dPct = (dactual / dtarget) * 100;
                        lbpct.Text = dPct.ToString("#,##0.00") + " %";
                    }
                    //lbtarget.Text = bll.fn_gettargetdriver(hdappraisal.Value.ToString(), cbperiod.SelectedValue.ToString(), cbemployee.SelectedValue.ToString()).ToString();
                    //lbactual.Text = bll.fn_getachievedriver(hdappraisal.Value.ToString(), cbperiod.SelectedValue.ToString(), cbemployee.SelectedValue.ToString()).ToString();
                    //double dactual = Convert.ToDouble(lbactual.Text);
                    //double dtarget = Convert.ToDouble(lbtarget.Text);
                    //double dPct = (dactual / dtarget) * 100;
                    //lbpct.Text = dPct.ToString("#,##0.00") + " %";
                    //sPoint = bll.vLookUp("select dbo.fn_getwrkappraisalpoint('" + Request.Cookies["usr_id"].Value.ToString() + "','" + hdappraisal.Value.ToString() + "')");
                    if (lbpoint != null)
                    {
                        lbpoint.Text = bll.sGetFieldValue("point", Convert.ToString(hdfPoint.Value));
                    }
                }
                else { sPoint = "N"; }
                cbpoint.SelectedValue = Convert.ToString(hdfPoint.Value);
            }
        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "APPRAISAL", "APPRAISAL FORM", "fm_empcompetency", "grdkpi_RowDataBound", "Exception", ex.Message + ex.InnerException);
        }
    }
    protected void grdkpi_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            HiddenField hdappraisal = (HiddenField)grdkpi.Rows[e.RowIndex].FindControl("hdappraisal");
            DropDownList cbpoint = (DropDownList)grdkpi.Rows[e.RowIndex].FindControl("cbpoint");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@appraisal_id", hdappraisal.Value.ToString()));
            arr.Add(new cArrayList("@point", cbpoint.SelectedValue.ToString()));
            bll.vInsertWrkAppraisal(arr);
            grdkpi.EditIndex = -1;
            arr.Clear();
            int month = Convert.ToInt32(Convert.ToString(cbperiod.SelectedValue).Substring(4, 2));
            int year = Convert.ToInt32(Convert.ToString(cbperiod.SelectedValue).Substring(0, 4));
            var lastDateValue = DateTime.DaysInMonth(year, month);
            DateTime lastDate = Convert.ToDateTime(Convert.ToString(month) + "/" + Convert.ToString(lastDateValue) + "/" + Convert.ToString(year));
            DateTime fisrtDate = Convert.ToDateTime(Convert.ToString(month) + "/" + Convert.ToString(1) + "/" + Convert.ToString(year));

            if (Convert.ToString(cbjobtitle.SelectedValue) == "5")
            {
                arr.Add(new cArrayList("@appraisal_cat", "KPI"));
                arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
                arr.Add(new cArrayList("@salesman_cd", Convert.ToString(cbemployee.SelectedValue)));
                arr.Add(new cArrayList("@startDate", fisrtDate));
                arr.Add(new cArrayList("@enddate", lastDate));
                arr.Add(new cArrayList("@period", Convert.ToString(cbperiod.SelectedValue)));
                arr.Add(new cArrayList("@salespoint_cd", Convert.ToString(cbsalespoint.SelectedValue)));
                bll.vBindingGridToSp(ref grdkpi, "sp_tmst_appraisal_driver_get", arr);
                arr.Clear();
            }
            else
            {
                arr.Add(new cArrayList("@appraisal_cat", "KPI"));
                arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
                arr.Add(new cArrayList("@salespoint_cd", Convert.ToString(cbsalespoint.SelectedValue)));
                arr.Add(new cArrayList("@period", Convert.ToString(cbperiod.SelectedValue)));
                arr.Add(new cArrayList("@nationality", Convert.ToString(txtNationality.Text)));
                bll.vBindingGridToSp(ref grdkpi, "sp_tmst_appraisal_get", arr);
                arr.Clear();
            }
        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "APPRAISAL", "APPRAISAL FORM", "fm_empcompetency", "grdkpi_RowUpdating", "Exception", ex.Message + ex.InnerException);
        }
    }
    protected void grdkpi_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            grdkpi.EditIndex = -1;
            int month = Convert.ToInt32(Convert.ToString(cbperiod.SelectedValue).Substring(4, 2));
            int year = Convert.ToInt32(Convert.ToString(cbperiod.SelectedValue).Substring(0, 4));
            var lastDateValue = DateTime.DaysInMonth(year, month);
            DateTime lastDate = Convert.ToDateTime(Convert.ToString(month) + "/" +Convert.ToString(lastDateValue)  + "/" + Convert.ToString(year));
            DateTime fisrtDate = Convert.ToDateTime(Convert.ToString(month) + "/" + Convert.ToString(1) + "/" + Convert.ToString(year));

            if (Convert.ToString(cbjobtitle.SelectedValue) == "5")
            {
                arr.Add(new cArrayList("@appraisal_cat", "KPI"));
                arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
                arr.Add(new cArrayList("@salesman_cd", Convert.ToString(cbemployee.SelectedValue)));
                arr.Add(new cArrayList("@startDate", fisrtDate));
                arr.Add(new cArrayList("@enddate", lastDate));
                arr.Add(new cArrayList("@period", Convert.ToString(cbperiod.SelectedValue)));
                arr.Add(new cArrayList("@salespoint_cd", Convert.ToString(cbsalespoint.SelectedValue)));
                bll.vBindingGridToSp(ref grdkpi, "sp_tmst_appraisal_driver_get", arr);
                arr.Clear();
            }
            else
            {
                arr.Add(new cArrayList("@appraisal_cat", "KPI"));
                arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
                arr.Add(new cArrayList("@salespoint_cd", Convert.ToString(cbsalespoint.SelectedValue)));
                arr.Add(new cArrayList("@period", Convert.ToString(cbperiod.SelectedValue)));
                arr.Add(new cArrayList("@nationality", Convert.ToString(txtNationality.Text)));
                bll.vBindingGridToSp(ref grdkpi, "sp_tmst_appraisal_get", arr);
                arr.Clear();
            }
        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "APPRAISAL", "APPRAISAL FORM", "fm_empcompetency", "grdkpi_RowCancelingEdit", "Exception", ex.Message + ex.InnerException);
        }
    }
    protected void grdcompetency_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            HiddenField hdappraisal = (HiddenField)e.Row.FindControl("hdappraisal");
            HiddenField hdfPoint = (HiddenField)e.Row.FindControl("hdfPoint");
            HiddenField hdfAchiveTarget = (HiddenField)e.Row.FindControl("hdfAchiveTarget");
            
            if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate))
            {
                string sPoint = string.Empty;
                Label lbpoint = (Label)e.Row.FindControl("lbpoint");
                Label lblAchive = (Label)e.Row.FindControl("lblAchive");
                Label lblTarget = (Label)e.Row.FindControl("lblTarget");
                Label lblAchiveWeight = (Label)e.Row.FindControl("lblAchiveWeight");
                DropDownList cbpoint = (DropDownList)e.Row.FindControl("cbpoint");
                DropDownList cbAchive = (DropDownList)e.Row.FindControl("cbAchive");
                if (cbpoint != null)
                {
                    bll.vBindingFieldValueToCombo(ref cbpoint, "point");
                    cbpoint.SelectedValue = Convert.ToString(hdfPoint.Value);
                }
                lbpoint.Text = bll.sGetFieldValue("point", Convert.ToString(hdfPoint.Value));
                if (cbAchive != null)
                {
                    cbAchive.SelectedValue = Convert.ToString(hdfAchiveTarget.Value);
                }
                lblAchive.Text = Convert.ToString(hdfAchiveTarget.Value);

                if (hdfAchiveTarget != null)
                {
                    if (hdfAchiveTarget.Value == "") hdfAchiveTarget.Value = "0";
                }

                if (Convert.ToDouble(hdfAchiveTarget.Value) != 0)
                {
                    double achive = Convert.ToInt32(Convert.ToDouble(hdfAchiveTarget.Value) * 100) / Convert.ToDouble(lblTarget.Text);
                    lblAchiveWeight.Text = Convert.ToString(achive);
                }

            }
            else if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
            {
                string sPoint = string.Empty;
                Label lbpoint = (Label)e.Row.FindControl("lbpoint");
                Label lblAchive = (Label)e.Row.FindControl("lblAchive");

                Label lblTarget = (Label)e.Row.FindControl("lblTarget");
                Label lblAchiveWeight = (Label)e.Row.FindControl("lblAchiveWeight");
                DropDownList cbpoint = (DropDownList)e.Row.FindControl("cbpoint");
                DropDownList cbAchive = (DropDownList)e.Row.FindControl("cbAchive");
                if (cbpoint != null)
                {
                    bll.vBindingFieldValueToCombo(ref cbpoint, "point");
                    cbpoint.SelectedValue = Convert.ToString(hdfPoint.Value);

                }
                if (lbpoint != null)
                {
                    lbpoint.Text = bll.sGetFieldValue("point", Convert.ToString(hdfPoint.Value));
                }
                if (cbAchive != null)
                {
                    cbAchive.SelectedValue = Convert.ToString(hdfAchiveTarget.Value);
                } 
                if (lblAchive != null)
                {
                    lblAchive.Text = Convert.ToString(hdfAchiveTarget.Value);
                }
                if (hdfAchiveTarget != null)
                {
                    if (hdfAchiveTarget.Value == "") hdfAchiveTarget.Value = "0";
                }

                if (Convert.ToDouble(hdfAchiveTarget.Value) != 0)
                {
                    double achive = Convert.ToInt32(Convert.ToDouble(hdfAchiveTarget.Value) * 100) / Convert.ToDouble(lblTarget.Text);
                    lblAchiveWeight.Text = Convert.ToString(achive);
                }
            }
        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "APPRAISAL", "APPRAISAL FORM", "fm_empcompetency", "grdcompetency_RowDataBound", "Exception", ex.Message + ex.InnerException);
        }
    }
    protected void grdcompetency_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            HiddenField hdappraisal = (HiddenField)grdcompetency.Rows[e.RowIndex].FindControl("hdappraisal");
            HiddenField hdfTargetValue = (HiddenField)grdcompetency.Rows[e.RowIndex].FindControl("hdfTargetValue");
            HiddenField hdfTargetWeight = (HiddenField)grdcompetency.Rows[e.RowIndex].FindControl("hdfTargetWeight");
            DropDownList cbpoint = (DropDownList)grdcompetency.Rows[e.RowIndex].FindControl("cbpoint");
            DropDownList cbAchive = (DropDownList)grdcompetency.Rows[e.RowIndex].FindControl("cbAchive");
            List<cArrayList> arr = new List<cArrayList>();

            if (Convert.ToDouble(hdfTargetValue.Value) >= Convert.ToDouble(cbAchive.SelectedValue))
            {
               
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@appraisal_id", hdappraisal.Value.ToString()));
                arr.Add(new cArrayList("@point", cbpoint.SelectedValue.ToString()));
                bll.vInsertWrkAppraisal(arr);
                arr.Clear();
                double TargetValue = Convert.ToDouble(hdfTargetValue.Value);
                double  TargetWeight = Convert.ToDouble(hdfTargetWeight.Value);
                double Achive = Convert.ToDouble(cbAchive.SelectedValue);
                double AchiveWeight = (Achive / TargetValue) * TargetWeight;

                arr.Add(new cArrayList("@appraisal_id", hdappraisal.Value.ToString()));
                arr.Add(new cArrayList("@TargetValue", Convert.ToDouble(hdfTargetValue.Value)));
                arr.Add(new cArrayList("@TargetWeight", Convert.ToDouble(hdfTargetWeight.Value)));
                arr.Add(new cArrayList("@AchiveTarget", Convert.ToDouble(cbAchive.SelectedValue)));
                arr.Add(new cArrayList("@AchiveWeight", Convert.ToDouble(AchiveWeight)));
                arr.Add(new cArrayList("@SalesPoint_cd", Convert.ToString(cbsalespoint.SelectedValue)));
                arr.Add(new cArrayList("@Period", Convert.ToString(cbperiod.SelectedValue)));
                arr.Add(new cArrayList("@emp_cd", Convert.ToString(cbemployee.SelectedValue)));
                bll.vInsertIncentiveTargetDriver(arr);
                arr.Clear();
                
            }
            grdcompetency.EditIndex = -1;
            arr.Clear();
            arr.Add(new cArrayList("@appraisal_cat", "CMP"));
            arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespoint_cd", Convert.ToString(cbsalespoint.SelectedValue)));
            arr.Add(new cArrayList("@period", Convert.ToString(cbperiod.SelectedValue)));
            arr.Add(new cArrayList("@nationality", Convert.ToString(txtNationality.Text)));
            bll.vBindingGridToSp(ref grdcompetency, "sp_tmst_appraisal_get", arr);
        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "APPRAISAL", "APPRAISAL FORM", "fm_empcompetency", "grdcompetency_RowUpdating", "Exception", ex.Message + ex.InnerException);
        }
    }
    protected void grdcompetency_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            grdcompetency.EditIndex = -1;
            arr.Add(new cArrayList("@appraisal_cat", "CMP"));
            arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespoint_cd", Convert.ToString(cbsalespoint.SelectedValue)));
            arr.Add(new cArrayList("@period", Convert.ToString(cbperiod.SelectedValue)));
            arr.Add(new cArrayList("@nationality", Convert.ToString(txtNationality.Text)));
            bll.vBindingGridToSp(ref grdcompetency, "sp_tmst_appraisal_get", arr);
        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "APPRAISAL", "APPRAISAL FORM", "fm_empcompetency", "grdcompetency_RowCancelingEdit", "Exception", ex.Message + ex.InnerException);
        }
    }
    protected void cbemployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@qry_cd", "driver"));
        arr.Add(new cArrayList("@salespointcd",cbsalespoint.SelectedValue.ToString()));

        DataTable dtDriverList = new DataTable();
        dtDriverList = cdl.GetValueFromSP("sp_tmst_employee_getbyqry", arr);

        var results = from myRow in dtDriverList.AsEnumerable()
                      where myRow.Field<string>("emp_cd") == Convert.ToString(cbemployee.SelectedValue)
                      select myRow;

        if (results.ToList().Count > 0)
        {
            txtNationality.Text = Convert.ToString(results.ToList()[0]["nationality"]);
        }
        vBindingAllGrid();
        //cbjobtitle_SelectedIndexChanged(sender, e);
    }
}