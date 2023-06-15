using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppClassTools;
using System.IO;

public partial class fm_MerchandiserUpload : System.Web.UI.Page
{
    cbll bll = new cbll();
    Utitlity ut = new Utitlity();
    cdal cdl = new cdal();

    AppClass app = new AppClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingComboToSp(ref ddlPeriod, "sp_tmst_period_get", "period_cd", "period_nm");
            bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm");
            bll.vBindingFieldValueToCombo(ref cbjobtitle, "job_title_cd");
            bll.vBindingFieldValueToCombo(ref cblevel, "level_cd");
            cbsalespoint_SelectedIndexChanged(sender, e);
            cblevel_SelectedIndexChanged(sender, e);
            cbjobtitle_SelectedIndexChanged(sender, e);
            BindGrid();
            //ddlPeriodTo_SelectedIndexChanged(sender, e);
        }
    }

    void BindEmployee()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
        arr.Add(new cArrayList("@level_cd", cblevel.SelectedValue.ToString()));
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        //arr.Add(new cArrayList("@isTL", ckTL.Checked));
        arr.Add(new cArrayList("@period", ddlPeriod.SelectedValue));
        bll.vBindingComboToSp(ref cbEmployee, "sp_tmst_employee_getActiveMerchandiser", "emp_cd", "emp_desc", arr);
    }
    void BindKPI()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@level_cd", cblevel.SelectedValue.ToString()));
        arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
        arr.Add(new cArrayList("@keyresp_cd", "DISP"));
        arr.Add(new cArrayList("@section_cd", "BHV"));
        bll.vBindingComboToSp(ref cbKPI, "sp_thrd_mst_kpi_display_get", "IDS", "KPI", arr);
    }

    void BindGrid()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        arr.Add(new cArrayList("@period", ddlPeriod.SelectedValue.ToString()));
        arr.Add(new cArrayList("@emp_cd", cbEmployee.SelectedValue.ToString()));
        arr.Add(new cArrayList("@kpi_ids", cbKPI.SelectedValue.ToString()));
        arr.Add(new cArrayList("@level_cd", cblevel.SelectedValue.ToString()));
        arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grdUploadDocuments, "sp_tmst_MerchandiserUpload_get", arr);
    }
    protected void cbsalespoint_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindEmployee(); 
    }

    protected void cblevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindEmployee();
        BindKPI();
        cbEmployee_SelectedIndexChanged(sender, e);
    }

    protected void cbjobtitle_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindEmployee();
        BindKPI();
        cbKPI_SelectedIndexChanged(sender, e);
        cbEmployee_SelectedIndexChanged(sender, e);
    }

    protected void ddlPeriod_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindEmployee();
    }

    protected void cbKPI_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblKPIWeight.Text = "0";
        if (cbKPI.SelectedValue != "")
        {
            lblKPIWeight.Text = bll.vLookUp("select weight_kpi from thrd_mst_kpi where IDS='" + cbKPI.SelectedValue + "'") + "%";
        }
        cbEmployee_SelectedIndexChanged(sender, e);

    }

    protected void cbEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblTarget.Text = "0";
        lblIsIncentive.Text = "";
        lblAchievement.Text = "0";
        if (cbKPI.SelectedValue != "" && cbEmployee.SelectedValue != "")
        {
            lblTarget.Text = bll.vLookUp("select qty from thrd_kpi_target where IDS='" + cbKPI.SelectedValue
            + "' and emp_cd='" + cbEmployee.SelectedValue + "' and period='" + ddlPeriod.SelectedValue
            + "' and salespointcd ='" + cbsalespoint.SelectedValue + "' and level_cd='" + cblevel.SelectedValue
            + "' and job_title_cd ='" + cbjobtitle.SelectedValue + "'");
            lblAchievement.Text = bll.vLookUp("select achievement from trpt_hrd_kpiresultdtl where emp_cd='" + cbEmployee.SelectedValue
            + "' and period='" + ddlPeriod.SelectedValue + "' and job_title_cd ='" + cbjobtitle.SelectedValue
            + "' and IDS = '" + cbKPI.SelectedValue
            + "' and salespointcd ='" + cbsalespoint.SelectedValue + "' and level_cd='" + cblevel.SelectedValue + "'");
        }
        if (cbEmployee.SelectedValue != "")
        {
            lblIsIncentive.Text = bll.vLookUp("select isApproved from trpt_hrd_kpiresult where emp_cd='" + cbEmployee.SelectedValue
                + "' and period='" + ddlPeriod.SelectedValue + "' and job_title_cd ='" + cbjobtitle.SelectedValue
                + "' and salespointcd ='" + cbsalespoint.SelectedValue + "' and level_cd='" + cblevel.SelectedValue + "'");
            if (lblIsIncentive.Text == "True")
            {
                lblIsIncentive.ForeColor = System.Drawing.Color.Green;
            }
            else if (lblIsIncentive.Text == "True")
            {
                lblIsIncentive.ForeColor = System.Drawing.Color.Red;
            }
        }

        BindGrid();

    }



    protected void grdUploadDocuments_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            if (lblIsIncentive.Text == "True")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Can not delete becuase incentive already apporved','Invalid to delete','warning');", true);
                return;
            }
            HiddenField hdmerchUpload_cd = (HiddenField)grdUploadDocuments.Rows[e.RowIndex].FindControl("hdmerchUpload_cd");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@merchUpload_cd", hdmerchUpload_cd.Value.ToString()));
            arr.Add(new cArrayList("@emp_cd", Convert.ToString(Request.Cookies["usr_id"].Value)));
            bll.vDeleteMerchandiserDocuments(arr);
            BindGrid();

            lblAchievement.Text = bll.vLookUp("select achievement from trpt_hrd_kpiresultdtl where emp_cd='" + cbEmployee.SelectedValue
                + "' and period='" + ddlPeriod.SelectedValue + "' and job_title_cd ='" + cbjobtitle.SelectedValue
                + "' and IDS = '" + cbKPI.SelectedValue
                + "' and salespointcd ='" + cbsalespoint.SelectedValue + "' and level_cd='" + cblevel.SelectedValue + "'");

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('File deleted successfully!','New KPI','true');", true);
        }
        catch (Exception ex)
        {
            ut.Logs("", "HR", "Merchandiser", "fm_MerchandiserUpload", "grdUploadDocuments_RowDeleting", "Exception", ex.Message + ex.InnerException);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('" + ex.Message + ex.InnerException + "','Error to save','warning');", true);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('File upload failed!','File upload failed');", true);

        }
    }

    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            if (upUploadDoc.HasFile == false)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please upload document','Upload document','warning');", true);
                return;
            }
            if (lblIsIncentive.Text == "True")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Can not add becuase incentive already apporved','Invalid to delete','warning');", true);
                return;
            }
            string merchUpload_cd = string.Empty;
            if (cbKPI.SelectedValue == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Select Product Display','Invalid Display','warning');", true);
                return;
            }
            if (cbsalespoint.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Select Branch','Invalid Branch','warning');", true);
                return;
            }
            if (ddlPeriod.SelectedValue == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Select Period','Invalid Period','warning');", true);
                return;
            }
            if (cbEmployee.SelectedValue == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Select Employee','Invalid Employee','warning');", true);
                return;
            }
            if (cblevel.SelectedValue == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Select Job Level','Invalid Job Level','warning');", true);
                return;
            }
            if (cbjobtitle.SelectedValue == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Select Job Title','Invalid Job Title','warning');", true);
                return;
            }
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
            arr.Add(new cArrayList("@period", ddlPeriod.SelectedValue.ToString()));
            arr.Add(new cArrayList("@emp_cd", cbEmployee.SelectedValue.ToString()));
            arr.Add(new cArrayList("@docName", Convert.ToString(Request.Cookies["usr_id"].Value)));
            arr.Add(new cArrayList("@kpi_ids", cbKPI.SelectedValue.ToString()));
            arr.Add(new cArrayList("@level_cd", cblevel.SelectedValue.ToString()));
            arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
            arr.Add(new cArrayList("@created_by", Convert.ToString(Request.Cookies["usr_id"].Value)));

            bll.vInsertMerchandiserUploadHO(arr, ref merchUpload_cd);


            if (upUploadDoc.HasFile)
            {
                FileInfo fi = new FileInfo(upUploadDoc.FileName);
                string ext = fi.Extension;
                byte[] fs = upUploadDoc.FileBytes;
                string upload_doc = string.Empty;
                //if (fs.Length <= 104857600)
                //{
                if ((upUploadDoc.FileName != "") || (upUploadDoc.FileName != null))
                {
                    upload_doc = merchUpload_cd + ext;
                    upUploadDoc.SaveAs(bll.sGetControlParameter("image_path") + "/merchUpload/" + upload_doc);
                    bll.vLookUp("update tmst_MerchandiserUpload set docName='" + upload_doc + "' where merchUpload_cd='" + merchUpload_cd + "'");
                }
                //}
            }
            BindGrid();

            lblAchievement.Text = bll.vLookUp("select achievement from trpt_hrd_kpiresultdtl where emp_cd='" + cbEmployee.SelectedValue
                + "' and period='" + ddlPeriod.SelectedValue + "' and job_title_cd ='" + cbjobtitle.SelectedValue
                + "' and IDS = '" + cbKPI.SelectedValue
                + "' and salespointcd ='" + cbsalespoint.SelectedValue + "' and level_cd='" + cblevel.SelectedValue + "'");

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('File upload successfully!','New KPI','true');", true);

        }
        catch (Exception ex)
        {
            ut.Logs("", "HR", "Merchandiser", "fm_MerchandiserUpload", "btnSave_Click", "Exception", ex.Message + ex.InnerException);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('" + ex.Message + ex.InnerException + "','Error to save','warning');", true);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('File upload failed!','File upload failed');", true);

        }
    }
}