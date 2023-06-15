using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_DriverIncentiveSummary : System.Web.UI.Page
{
    cbll bll = new cbll();
    cdal cdl = new cdal();
    Utitlity ut = new Utitlity();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            DataTable dtTemp = new DataTable();

            DataTable dtPeriod = new DataTable();
            arr.Clear();
            dtPeriod = cdl.GetValueFromSP("sp_tmst_period_get", arr);


            //bll.vBindingComboToSp(ref cbperiod, "sp_tmst_period_get", "period_cd", "period_nm", arr);
            if (dtPeriod != null)
            {
                if (dtPeriod.Rows.Count > 0)
                {
                    lbPeriod.DataSourceID = String.Empty;
                    lbPeriod.DataSource = dtPeriod;
                    lbPeriod.DataValueField = "period_cd";
                    lbPeriod.DataTextField = "period_nm";

                    //cbperiod.DataSourceID = "period";
                    lbPeriod.DataBind();
                    //cbperiod.SelectedValue = bll.sGetControlParameter("period");
                }
            }
            dtPeriod.Clear();
            BindBranch();
        }
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
                cbsalespoint.DataSourceID = String.Empty;
                cbsalespoint.DataSource = dtPeriod;
                cbsalespoint.DataValueField = "salespointcd";
                cbsalespoint.DataTextField = "salespoint_nm";

                //cbperiod.DataSourceID = "period";
                cbsalespoint.DataBind();
                //cbperiod.SelectedValue = bll.sGetControlParameter("period");
            }
        }
    }

    protected void cbsalespoint_SelectedIndexChanged(object sender, EventArgs e)
    {
        //UpdateControl();
        BindEmployee();
    }

    protected void chkAllSalespoint_CheckedChanged(object sender, EventArgs e)
    {
        UpdateControl();
    }

    void BindEmployee()
    {
        string selectBranch = string.Empty;
        int cnt = 0;
        List<cArrayList> arr = new List<cArrayList>();
        foreach (ListItem li in cbsalespoint.Items)
        {
            li.Enabled = true;
            if (li.Selected == true)
            {
                // you are always using lstCatID.SelectedItem.Value.
                selectBranch += li.Value + ",";
                cnt += 1;
            }
        }
        //if (chkAllSalespoint.Checked)
        //{
        //    UpdateControl();
        //}
        //else
        //{
        arr.Clear();
        cbemployee.DataSource = null;
        cbemployee.DataBind();
        DataTable dtEmployee = new DataTable();
        arr.Add(new cArrayList("@job_title_cd", "5"));
        arr.Add(new cArrayList("@level_cd", "1"));
        arr.Add(new cArrayList("@salespointcd", selectBranch));
        dtEmployee = cdl.GetValueFromSP("sp_tmst_employee_getbyjobtitleActiveMultiBranch", arr);


        //bll.vBindingComboToSp(ref cbperiod, "sp_tmst_period_get", "period_cd", "period_nm", arr);
        if (dtEmployee != null)
        {
            if (dtEmployee.Rows.Count > 0)
            {
                cbemployee.DataSourceID = String.Empty;
                cbemployee.DataSource = dtEmployee;
                cbemployee.DataValueField = "emp_cd";
                cbemployee.DataTextField = "emp_nm";
                cbemployee.DataBind();
            }
            else
            {
                cbemployee.Items.Clear();
                //cbemployee.Items.Add(new ListItem { Text = "", Value = "" });
                //cbemployee.DataSourceID = String.Empty;
                //cbemployee.DataSource = null;
                //cbemployee.DataValueField = "emp_cd";
                //cbemployee.DataTextField = "emp_nm";
                cbemployee.DataBind();
            }
        }

        //}
    }

    protected void cbperiod_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ShowReport();
    }

    void ShowReport()
    {
        string selectBranch = string.Empty;
        string selectPeriod = string.Empty;
        string selectEmployee = string.Empty;

        foreach (ListItem li in cbsalespoint.Items)
        {
            if (li.Selected == true)
            {
                selectBranch += li.Value + ",";
            }
        }
        foreach (ListItem li in cbemployee.Items)
        {
            if (li.Selected == true)
            {
                selectEmployee += li.Value + ",";
            }
        }
        foreach (ListItem li in lbPeriod.Items)
        {
            if (li.Selected == true)
            {
                selectPeriod += li.Value + ",";
            }
        }
        if (selectEmployee.Length > 0)
        {
            selectEmployee = "" + selectEmployee.Substring(0, selectEmployee.Length - 1) + "";
        }
        if (selectPeriod.Length > 0)
        {
            selectPeriod = "" + selectPeriod.Substring(0, selectPeriod.Length - 1) + "";
        }
        if (selectBranch.Length > 0)
        {
            selectBranch = "" + selectBranch.Substring(0, selectBranch.Length - 1) + "";
        }

        Response.Cookies["incentiveSummaryPeriod"].Value = selectPeriod;
        Response.Cookies["incentiveSummaryBranch"].Value = selectBranch;
        Response.Cookies["incentiveSummaryEmp"].Value = selectEmployee;
        if (selectPeriod == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please select period.','Select period','warning');", true);
            return;
        }
        if (chkAllSalespoint.Checked == false && selectBranch == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please select branch.','Select branch','warning');", true);
            return;
        }

        if (chkAllSalespoint.Checked == true)
        {
            Response.Cookies["incentiveSummaryBranch"].Value = null;
            Response.Cookies["incentiveSummaryEmp"].Value = null;
        }

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('../fm_report2.aspx?src=DriverIncentiveSummary');", true);

    }

    protected void bprint_Click(object sender, EventArgs e)
    {
        ShowReport();
    }



    protected void cbemployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        //UpdateControl();
    }


    void UpdateControl()
    {
        if (chkAllSalespoint.Checked)
        {
            cbsalespoint.Enabled = false;
            cbemployee.Enabled = false;
            foreach (ListItem li in cbemployee.Items)
            {
                li.Enabled = false;
            }
            foreach (ListItem li in cbsalespoint.Items)
            {
                li.Enabled = false;
            }
        }
        else
        {
            cbsalespoint.Enabled = true;
            cbemployee.Enabled = true;
            BindBranch();
            BindEmployee();
        }
    }

  
}