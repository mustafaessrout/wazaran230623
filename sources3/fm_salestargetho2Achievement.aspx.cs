using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_salestargetho2Achievement : System.Web.UI.Page
{
    cbll bll = new cbll();
    cdal cdl = new cdal();
    Utitlity ut = new Utitlity();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string selectPeriod = string.Empty;
            Response.Cookies["achievementPeriod"].Value = null;
            BindBranch(); BindPeriod();
            //ddlReportTypes_SelectedIndexChanged(sender, e);
            if (ddlReportTypes.SelectedValue == "D")
            {
                //foreach (ListItem li in lbPeriod.Items)
                //{
                //    if (li.Selected == true)
                //    {
                //        selectPeriod += li.Value + ",";
                //    }
                //}
                //if (selectPeriod.Length > 0)
                //{
                //    selectPeriod = "" + selectPeriod.Substring(0, selectPeriod.Length - 1) + "";
                //}
                if (selectPeriod == "")
                {
                    selectPeriod = bll.vLookUp("select top(1) period from tmst_hr_salestargetAchievement where closing_typ='D' order by convert(int, period) desc ");
                }

                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@period", selectPeriod));
                bll.vBindingComboToSp(ref ddlDate, "sp_getrecordDateSaletarget", "createdDate", "createdDate", arr);
                Response.Cookies["achievementPeriod"].Value = selectPeriod;
                Response.Cookies["achievementDate"].Value = ddlDate.SelectedValue;

                foreach (ListItem li in lbPeriod.Items)
                {
                    if (li.Value == selectPeriod)
                    {
                        li.Selected = true;
                        selectPeriod += li.Value + ",";
                    }
                    else { li.Selected = false; }
                }
                ddlReportTypes_SelectedIndexChanged(sender, e);
            }
        }
    }
    void UpdateControl()
    {
        if (chkAllSalespoint.Checked)
        {
            //cbsalespoint.Enabled = false;
            //foreach (ListItem li in cbsalespoint.Items)
            //{
            //    li.Enabled = false;
            //}
        }
        else
        {
            //cbsalespoint.Enabled = true;
            BindBranch();
        }
    }

    void UpdatePeriod()
    {
        if (chkAllPeriod.Checked)
        {
            lbPeriod.Enabled = false;
            foreach (ListItem li in lbPeriod.Items)
            {
                li.Enabled = false;
            }
        }
        else
        {
            lbPeriod.Enabled = true;
            BindPeriod();
        }
    }
    void BindPeriod()
    {
        List<cArrayList> arr = new List<cArrayList>();

        DataTable dtPeriod = new DataTable();
        dtPeriod = cdl.GetValueFromSP("sp_tmst_period_get", arr);
        if (dtPeriod != null)
        {
            if (dtPeriod.Rows.Count > 0)
            {
                lbPeriod.DataSourceID = String.Empty;
                lbPeriod.DataSource = dtPeriod;
                lbPeriod.DataValueField = "period_cd";
                lbPeriod.DataTextField = "period_nm";
                lbPeriod.DataBind();
            }
        }
    }
    protected void chkAllSalespoint_CheckedChanged(object sender, EventArgs e)
    {
        UpdateControl();
    }
    void BindBranch()
    {
        List<cArrayList> arr = new List<cArrayList>();

        DataTable dtPeriod = new DataTable();
        dtPeriod = cdl.GetValueFromSP("sp_tmst_salespoint_get", arr);


        //bll.vBindingComboToSp(ref cbperiod, "sp_tmst_period_get", "period_cd", "period_nm", arr);
        if (dtPeriod != null)
        {
            if (dtPeriod.Rows.Count > 0)
            {
                //cbsalespoint.DataSourceID = String.Empty;
                //cbsalespoint.DataSource = dtPeriod;
                //cbsalespoint.DataValueField = "salespointcd";
                //cbsalespoint.DataTextField = "salespoint_nm";

                ////cbperiod.DataSourceID = "period";
                //cbsalespoint.DataBind();
                ////cbperiod.SelectedValue = bll.sGetControlParameter("period");
            }
        }
    }

    protected void chkAllPeriod_CheckedChanged(object sender, EventArgs e)
    {
        UpdatePeriod();
    }
    protected void btnAchievement_Click(object sender, EventArgs e)
    {
        ShowReport();
    }
    void ShowReport()
    {
        string selectBranch = string.Empty;
        string selectPeriod = string.Empty;
        string selectEmployee = string.Empty;
        foreach (ListItem li in lbPeriod.Items)
        {
            if (li.Selected == true)
            {
                selectPeriod += li.Value + ",";
            }
        }
        if (selectPeriod.Length > 0)
        {
            selectPeriod = "" + selectPeriod.Substring(0, selectPeriod.Length - 1) + "";
            Response.Cookies["achievementPeriod"].Value = selectPeriod;
        }
        //if (selectPeriod == "")
        //{
        //    selectPeriod = bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='period'");
        //    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please select period.','Select period','warning');", true);
        //    //return;
        //}
        //if (defaultLoading == true)
        //{
        //    selectPeriod = bll.vLookUp("select top(1) period from tmst_hr_salestargetAchievement where closing_typ='M' order by convert(int, period) desc ");
        //    Response.Cookies["achievementPeriod"].Value = selectPeriod;
        //}
        //else {
        //    Response.Cookies["achievementPeriod"].Value = selectPeriod;
        //}
        if (ddlReportTypes.SelectedValue=="M")
        {
            Response.Cookies["achievementDate"].Value = null;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=rp_salestargetAchievementAllBranchPeriod');", true);
        }
        else if (ddlReportTypes.SelectedValue == "D")
        {
            Response.Cookies["achievementDate"].Value = ddlDate.SelectedValue;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=salestargetAchievementHO');", true);
        }
    }

    protected void ddlReportTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        string selectPeriod = string.Empty;
        string currentPeriod = string.Empty;
        if (ddlReportTypes.SelectedValue == "M")
        {
            ddlDate.DataSource = null;
            ddlDate.DataBind();
            ddlDate.Enabled = false;
            ddlDate.CssClass = "form-control ro";
            currentPeriod = bll.vLookUp("select top(1) period from tmst_hr_salestargetAchievement where closing_typ='M' order by convert(int, period) desc ");
            foreach (ListItem li in lbPeriod.Items)
            {
                if (li.Value == currentPeriod)
                {
                    li.Selected = true;
                }
                else { li.Selected = false; }
            }
        }
        else if(ddlReportTypes.SelectedValue == "D")
        {
            ddlDate.Enabled = true;
            ddlDate.CssClass = "form-control";
            currentPeriod = bll.vLookUp("select top(1) period from tmst_hr_salestargetAchievement where closing_typ='D' order by convert(int, period) desc ");
            foreach (ListItem li in lbPeriod.Items)
            {
                if (li.Value == currentPeriod)
                {
                    li.Selected = true;
                    selectPeriod += li.Value + ",";
                }
                else { li.Selected = false; }
            }
            if (selectPeriod.Length > 0)
            {
                selectPeriod = "" + selectPeriod.Substring(0, selectPeriod.Length - 1) + "";
            }
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@period", selectPeriod));
            bll.vBindingComboToSp(ref ddlDate, "sp_getrecordDateSaletarget", "createdDate", "createdDate", arr);
        }
    }
}