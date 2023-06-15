using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppClassTools;
using System.Data;

public partial class fm_targetdriver : System.Web.UI.Page
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

                List<cArrayList> arr = new List<cArrayList>();
                bll.vBindingComboToSp(ref cbsp, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm");
                bll.vBindingComboToSp(ref cbperiod, "sp_tmst_period_get", "period_cd", "period_nm");
                cbperiod.SelectedValue = bll.vLookUp("select dbo.fn_getcontrolparameter('period')");
                arr.Add(new cArrayList("@qry_cd", "driver"));
                arr.Add(new cArrayList("@salespointcd", cbsp.SelectedValue.ToString()));
                bll.vBindingComboToSp(ref cbdriver, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
                cbdriver_SelectedIndexChanged(sender, e);
                cbsp_SelectedIndexChanged(sender, e);
            }
            catch (Exception ex)
            {
                app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
                ut.Logs("", "Appraisal", "Target Driver", "fm_targetdriver", "Page_Load", "Exception", ex.Message + ex.InnerException);
            }
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);

    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        try
        {
            double dQty = 0;
            if (!double.TryParse(txqty.Text, out dQty))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Target Qty must be numeric','Check your qty target','warning');", true);
                return;
            }
            else if (!double.TryParse(txinvamt.Text, out dQty))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Invoice must be numeric','Check your Invoice target','warning');", true);
                return;
            }
            else if (!double.TryParse(txtInvoiceWeight.Text, out dQty))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Invoice Weight must be numeric','Check your Invoice Weight','warning');", true);
                return;
            }
            else if (!double.TryParse(txtQtyWeight.Text, out dQty))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Qty Weight must be numeric','Check your Qty Weight','warning');", true);
                return;
            }
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@totqty", txqty.Text));
            arr.Add(new cArrayList("@totinvoice", txinvamt.Text));
            arr.Add(new cArrayList("@TotalInvoiceWeight", txtInvoiceWeight.Text));
            arr.Add(new cArrayList("@TotalQtyWeight", txtQtyWeight.Text));
            arr.Add(new cArrayList("@period", cbperiod.SelectedValue.ToString()));
            arr.Add(new cArrayList("@emp_cd", cbdriver.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespoint_cd", Convert.ToString(cbsp.SelectedValue)));
            arr.Add(new cArrayList("@CreatedBy", Convert.ToString(Session["usr_id"])));
            arr.Add(new cArrayList("@CreateDate", DateTime.Now.Date));
            arr.Add(new cArrayList("@UpdatedBy", Convert.ToString(Session["usr_id"])));
            arr.Add(new cArrayList("@UpdatedDate", DateTime.Now.Date));
            bll.vInsertTargetDriver(arr);
            txqty.Text = "0";
            txinvamt.Text = "0";
            vBindingGrid();
            BindWeight();
        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "Appraisal", "Target Driver", "fm_targetdriver", "Page_Load", "Exception", ex.Message + ex.InnerException);
        }
    }
    protected void cbsp_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            vBindingGrid();
            BindWeight();
            cbdriver_SelectedIndexChanged(sender, e);
        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "Appraisal", "Target Driver", "fm_targetdriver", "cbsp_SelectedIndexChanged", "Exception", ex.Message + ex.InnerException);
        }
    }

    void vBindingGrid()
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@qry_cd", "driver"));
            arr.Add(new cArrayList("@salespointcd", cbsp.SelectedValue.ToString()));
            bll.vBindingComboToSp(ref cbdriver, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
            arr.Clear();
            arr.Add(new cArrayList("@period", cbperiod.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespointcd", cbsp.SelectedValue.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_ttarget_driver_get", arr);

            arr.Clear();

            CalulateWeight();
        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "Appraisal", "Target Driver", "fm_targetdriver", "vBindingGrid", "Exception", ex.Message + ex.InnerException);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report3.aspx?src=bs');", true);
        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "Appraisal", "Target Driver", "fm_targetdriver", "vBindingGrid", "Exception", ex.Message + ex.InnerException);
        }
    }
    protected void cbdriver_SelectedIndexChanged(object sender, EventArgs e)
    {
        //vBindingGrid();

        
        txtNationality.Text = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@qry_cd", "driver"));
        arr.Add(new cArrayList("@salespointcd", cbsp.SelectedValue.ToString()));

        DataTable dtDriverList = new DataTable();
        dtDriverList = cdl.GetValueFromSP("sp_tmst_employee_getbyqry", arr);

        var results = from myRow in dtDriverList.AsEnumerable()
                      where myRow.Field<string>("emp_cd") == Convert.ToString(cbdriver.SelectedValue)
                      select myRow;

        txtNationality.Text = Convert.ToString(results.ToList()[0]["nationality"]);

        BindWeight();

    }

    void BindWeight() {
        string nationality = string.Empty;
        if (txtNationality.Text != "saudi")
        {
            nationality = "nonsaudi";
        }
        else {
            nationality = "saudi";
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@appraisal_cat", "KPI"));
        arr.Add(new cArrayList("@job_title_cd","5"));
        arr.Add(new cArrayList("@salespoint_cd", cbsp.SelectedValue.ToString()));
        arr.Add(new cArrayList("@period", Convert.ToString(cbperiod.SelectedValue)));
        arr.Add(new cArrayList("@nationality", Convert.ToString(nationality)));
       
        DataTable dtMasterKeyAppraisal = new DataTable();
        dtMasterKeyAppraisal = cdl.GetValueFromSP("sp_tmst_appraisal_get", arr);
        txtQtyWeight.Text = string.Empty;
        txtInvoiceWeight.Text = string.Empty;
        if (dtMasterKeyAppraisal.Rows.Count > 0)
        {
            txtQtyWeight.Text = Convert.ToString(dtMasterKeyAppraisal.Rows[0]["WeightValue"]);
            txtInvoiceWeight.Text = Convert.ToString(dtMasterKeyAppraisal.Rows[1]["WeightValue"]);
        }
    }
    protected void cbperiod_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            vBindingGrid();
            BindWeight();
            cbdriver_SelectedIndexChanged(sender, e);
        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "Appraisal", "Target Driver", "fm_targetdriver", "cbperiod_SelectedIndexChanged", "Exception", ex.Message + ex.InnerException);
        }
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=tgtdrv&period=" + cbperiod.SelectedValue.ToString() + "');", true);
    }

    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            HiddenField hdfsalespointcd = (HiddenField)grd.Rows[e.RowIndex].FindControl("hdfsalespointcd");
            HiddenField hdempcode = (HiddenField)grd.Rows[e.RowIndex].FindControl("hdempcode");
            HiddenField hdfperiod = (HiddenField)grd.Rows[e.RowIndex].FindControl("hdfperiod");

            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@emp_cd", hdempcode.Value.ToString()));
            arr.Add(new cArrayList("@salespoint_cd", hdfsalespointcd.Value.ToString()));
            arr.Add(new cArrayList("@period", hdfperiod.Value.ToString()));
            bll.vDelDriverTarget(arr);
            vBindingGrid();
        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "Appraisal", "Target Driver", "fm_targetdriver", "grd_RowDeleting", "Exception", ex.Message + ex.InnerException);
        }
    }


    void CalulateWeight()
    {
        try
        {
            DataTable dt = ((DataView)grd.DataSource).Table;
            int weight = 0;
            lblWeight.Text = "0";
            if (dt.Rows.Count > 0)
            {
                int TotalQtyWeight = Convert.ToInt32(dt.Compute("Sum(TotalQtyWeight)", ""));
                int TotalInvoiceWeight = Convert.ToInt32(dt.Compute("Sum(TotalInvoiceWeight)", ""));

                weight = Convert.ToInt32(TotalQtyWeight + TotalInvoiceWeight);
            }



            lblWeight.Text = Convert.ToString(weight) + "%";
            if (weight > 100)
            {
                lblWeight.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                lblWeight.ForeColor = System.Drawing.Color.Black;
            }
        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "APPRAISAL", "Target Driver", "fm_targetdriver", "CalulateWeight", "Exception", ex.Message + ex.InnerException);
        }
    }


}