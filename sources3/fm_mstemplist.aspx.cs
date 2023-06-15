using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_mstemplist : System.Web.UI.Page
{
    cbll bll = new cbll();
    public string sp = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                List<cArrayList> arr = new List<cArrayList>();
                sp = Request.Cookies["sp"].Value.ToString();
                bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_get", "salespointcd", "salespoint_desc");
                bll.vBindingComboToSp(ref cbdepartment, "sp_tmst_department_get", "dept_cd", "dept_nm");
                bll.vBindingFieldValueToCombo(ref cbnationaligy, "nationality");
                if (sp == "0")
                {
                    cbsalespoint.Items.Insert(0, new ListItem("<< ALL Branch >>", "-1"));
                    cbsalespoint.SelectedValue = "-1";
                    arr.Add(new cArrayList("@salespointcd", "-1"));
                    chsp.Enabled = true;
                    chsp.Checked = true;
                }
                else
                {                    
                    chsp.Checked = false;
                    chsp.Enabled = false;
                    cbsalespoint.Enabled = false;
                    cbsalespoint.SelectedValue = sp;
                    arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
                }
                cbnationaligy.SelectedValue = "EGY";
                chnat.Checked = true;
                chdept.Checked = true;
                if (!chdept.Checked) { arr.Add(new cArrayList("@dept_cd", cbdepartment.SelectedValue.ToString())); }
                else { arr.Add(new cArrayList("@dept_cd", "-1")); }
                if (!chnat.Checked) { arr.Add(new cArrayList("@nationality", cbdepartment.SelectedValue.ToString())); }
                else { arr.Add(new cArrayList("@nationality", "-1")); }
                bll.vBindingGridToSp(ref grd, "sp_tmst_employee_get6",arr);
                
                
            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstemplist");
                Response.Redirect("fm_ErrorPage.aspx");
            }
            
        }
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_mstempentry.aspx");
    }
    protected void grd_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Label lbl = (Label)grd.Rows[e.NewEditIndex].FindControl("lbempcd");
        Response.Redirect("fm_mstempentry.aspx?emp=" + lbl.Text);
    }
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            if (chsp.Checked)
            {
                arr.Add(new cArrayList("@salespointcd", "-1"));
                chsp.Enabled = true;
            }
            else
            {
                arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
                chsp.Checked = false;
                chsp.Enabled = false;
            }
            grd.PageIndex = e.NewPageIndex;
            if (!chdept.Checked) { arr.Add(new cArrayList("@dept_cd", cbdepartment.SelectedValue.ToString())); }
            else { arr.Add(new cArrayList("@dept_cd", "-1")); }
            if (!chnat.Checked) { arr.Add(new cArrayList("@nationality", cbdepartment.SelectedValue.ToString())); }
            else { arr.Add(new cArrayList("@nationality", "-1")); }
            bll.vBindingGridToSp(ref grd, "sp_tmst_employee_get6", arr);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstemplist");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        HttpCookie cookieSP;
        cookieSP = HttpContext.Current.Request.Cookies["sp"];
        string sEmp = string.Empty;
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lEmp = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", cookieSP.Value.ToString()));
        arr.Add(new cArrayList("@job_title", "SalesJob"));
        //arr.Add(new cArrayList("@emp_nm", prefixText));
        cbll bll = new cbll();
        bll.vSearchMstEmployee2bysalespoint(arr, ref rs);
        while (rs.Read())
        {
            sEmp = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["emp_desc"].ToString(), rs["emp_cd"].ToString());
            lEmp.Add(sEmp);
        } rs.Close();
        return (lEmp.ToArray());
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        try
        {

            if (txemp.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Error!','Please Insert Employee Name or Code.','error');", true);
                return;
            }
            // System.Data.SqlClient.SqlDataReader rs = null;
            List<cArrayList> arr = new List<cArrayList>();
            if (!chdept.Checked) { arr.Add(new cArrayList("@dept_cd", cbdepartment.SelectedValue.ToString())); }
            else { arr.Add(new cArrayList("@dept_cd", "-1")); }
            if (!chnat.Checked) { arr.Add(new cArrayList("@nationality", cbdepartment.SelectedValue.ToString())); }
            else { arr.Add(new cArrayList("@nationality", "-1")); }
            if (chsp.Checked)
            {
                arr.Add(new cArrayList("@salespointcd", "-1"));
            }
            else
            {
                arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
            }

            arr.Add(new cArrayList("@emp_nm", txemp.Text));
            bll.vBindingGridToSp(ref grd, "sp_tmst_employee_get6", arr);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstemplist");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Clear();
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        //arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        if (!chdept.Checked) { arr.Add(new cArrayList("@dept_cd", cbdepartment.SelectedValue.ToString())); }
        else { arr.Add(new cArrayList("@dept_cd", "-1")); }
        if (!chnat.Checked) { arr.Add(new cArrayList("@nationality", cbdepartment.SelectedValue.ToString())); }
        else { arr.Add(new cArrayList("@nationality", "-1")); }
        //arr.Add(new cArrayList("@cust_cd", null));
        Session["lParam_mst_employee"] = arr;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=master_employee');", true);
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=em');", true);
    }
    protected void cbsalespoint_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            if (!chdept.Checked) { arr.Add(new cArrayList("@dept_cd", cbdepartment.SelectedValue.ToString())); }
            else { arr.Add(new cArrayList("@dept_cd", "-1")); }
            if (!chnat.Checked) { arr.Add(new cArrayList("@nationality", cbdepartment.SelectedValue.ToString())); }
            else { arr.Add(new cArrayList("@nationality", "-1")); }
            if (chsp.Checked)
            {
                arr.Add(new cArrayList("@salespointcd", "-1"));
            }
            else
            {
                arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
            }
            bll.vBindingGridToSp(ref grd, "sp_tmst_employee_get6", arr);
            //bll.vBindingGridToSp(ref grd, "sp_tmst_employee_get33", arr);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstemplist");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void chsp_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            if (!chdept.Checked) { arr.Add(new cArrayList("@dept_cd", cbdepartment.SelectedValue.ToString()));}
            else{arr.Add(new cArrayList("@dept_cd", "-1"));}
            if (!chnat.Checked) { arr.Add(new cArrayList("@nationality", cbdepartment.SelectedValue.ToString())); }
            else { arr.Add(new cArrayList("@nationality", "-1")); }
            if (chsp.Checked)
            {
                arr.Add(new cArrayList("@salespointcd", "-1"));
                cbsalespoint.SelectedValue = "-1";

            }
            else
            {
                arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
            }
            bll.vBindingGridToSp(ref grd, "sp_tmst_employee_get6", arr);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstemplist");
            Response.Redirect("fm_ErrorPage.aspx");
        }

    }
}