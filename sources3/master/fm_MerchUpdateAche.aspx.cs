using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppClassTools;
using System.IO;

public partial class fm_MerchUpdateAche : System.Web.UI.Page
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


    void BindGrid()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        arr.Add(new cArrayList("@period", ddlPeriod.SelectedValue.ToString()));
        arr.Add(new cArrayList("@emp_cd", cbEmployee.SelectedValue.ToString()));
        arr.Add(new cArrayList("@level_cd", cblevel.SelectedValue.ToString()));
        arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grdAchievement, "sp_kpiresultdtl_merch_achi", arr);
    }
    protected void cbsalespoint_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindEmployee();
    }

    protected void cblevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindEmployee();
        cbEmployee_SelectedIndexChanged(sender, e);
    }

    protected void cbjobtitle_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindEmployee();
        cbEmployee_SelectedIndexChanged(sender, e);
    }

    protected void ddlPeriod_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindEmployee();
    }



    protected void cbEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }



    protected void grdAchievement_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (lblIsIncentive.Text == "True")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Can not delete becuase incentive already apporved','Invalid to delete','warning');", true);
            return;
        }
        HiddenField merchUpload_cd = (HiddenField)grdAchievement.Rows[e.RowIndex].FindControl("merchUpload_cd");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@merchUpload_cd", cbsalespoint.SelectedValue.ToString()));
        arr.Add(new cArrayList("@emp_cd", Convert.ToString(Request.Cookies["usr_id"].Value)));
        bll.vDeleteMerchandiserDocuments(arr);
    }

    protected void grdAchievement_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdAchievement.EditIndex = -1;
        BindGrid();
    }

    protected void grdAchievement_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdAchievement.EditIndex = e.NewEditIndex;
        BindGrid();
    }

    protected void grdAchievement_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        if (lblIsIncentive.Text == "True")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Can not delete becuase incentive already apporved','Invalid to delete','warning');", true);
            return;
        }
        HiddenField hdmerchUpload_cd = (HiddenField)grdAchievement.Rows[e.RowIndex].FindControl("hdmerchUpload_cd");
        HiddenField hdftarget = (HiddenField)grdAchievement.Rows[e.RowIndex].FindControl("hdftarget");
        TextBox txachievement = (TextBox)grdAchievement.Rows[e.RowIndex].FindControl("txachievement");

        if (Convert.ToDouble(txachievement.Text) > Convert.ToDouble(hdftarget.Value))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Can not achievement greater then target','Invalid achievement','warning');", true);
            return;
        }
        else {
            bll.vLookUp("update trpt_hrd_kpiresultdtl set achievement = " + Convert.ToDecimal(txachievement.Text) + " where ids='" + hdmerchUpload_cd.Value + "' and emp_cd='" + cbEmployee.SelectedValue + "'");
        }
        grdAchievement.EditIndex = -1;
        BindGrid();
    }

    protected void grdAchievement_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}