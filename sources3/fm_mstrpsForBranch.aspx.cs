using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_mstrpsForBranch : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string waz_dt;
            waz_dt = Request.Cookies["waz_dt"].Value.ToString();
            DateTime dtwaz_dt = DateTime.ParseExact(waz_dt, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            waz_dt = dtwaz_dt.ToString("yyyyMM");
            bll.vBindingComboToSp(ref cbMonthCD, "sp_tblTRYearMonth_get", "period", "ymtName");
            cbMonthCD.SelectedValue = waz_dt;

            List<cArrayList> arr = new List<cArrayList>();
            bll.vBindingFieldValueToCombo(ref cbdaycode, "day_cd");
            bll.vBindingFieldValueToCombo(ref cbrpstype, "rps_typ");
            cbrpstype_SelectedIndexChanged(sender, e);
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vDelWrkCustomer(arr);
            cbsalesman_SelectedIndexChanged(sender, e);
            cbdaycode_SelectedIndexChanged(sender, e);
            txcustsearch.Attributes.Add("onkeypress", "SetContextKey()");
            rdby.SelectedIndex = 0;
            rdby_SelectedIndexChanged(sender, e);
            BindingGrid();
        } 
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        HttpCookie cook;
        cook = HttpContext.Current.Request.Cookies["sp"];
        System.Data.SqlClient.SqlDataReader rs = null;
        cbll bll = new cbll();
        List<string> lcust = new List<string>();
        string scust = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cust_cd", prefixText));
        arr.Add(new cArrayList("@salesman_cd", contextKey));
        bll.vSearchCustomerBySalesman(arr, ref rs);
        while (rs.Read())
        { 
           scust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + "-" + rs["cust_nm"].ToString(),rs["cust_cd"].ToString());
           lcust.Add(scust);
        } rs.Close();
        return (lcust.ToArray());
    }
    protected void btadd_Click(object sender, EventArgs e)
    {

        if (rdby.SelectedIndex == 0)
        {
            if (hdcust.Value == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please input Customer','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "vDisableShow();", true);
                return;

            }
            string sCheckrpsbydate = bll.vLookUp("select dbo.fn_checkbydate('" + cbsalesman.SelectedValue + "','" + hdcust.Value + "')");
            if (sCheckrpsbydate != "ok")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('There are rps by date this customer code ','" + sCheckrpsbydate + "','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "vDisableShow();", true);
                return;
            }
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vInsertWrkCustomer(arr);
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_twrk_customer_get", arr);
        }
        else
        {
            if (hdcust.Value == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please input Customer','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "vDisableShow();", true);
                return;

            }
            if (dtrps_dt.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please input Date','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "vDisableShow();", true);
                return;

            }
            string sCheckrpsbyday = bll.vLookUp("select dbo.fn_checkrpsbyday('" + cbsalesman.SelectedValue+ "','"+hdcust.Value+"')");
            if (sCheckrpsbyday != "ok")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('There are rps by day this customer code ','" + sCheckrpsbyday + "','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "vDisableShow();", true);
                return;
            }
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@emp_cd", cbsalesman.SelectedValue.ToString()));
            arr.Add(new cArrayList("@cust_cd", hdcust.Value));
            arr.Add(new cArrayList("@rps_dt", DateTime.ParseExact(dtrps_dt.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@created_dt", DateTime.Now));
            arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
            bll.vInserttrps_bydate(arr);
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "$get('" + txcustsearch.ClientID +  "').value='';", true);
        BindingGrid();
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>(); int nCount = 1;
        arr.Add(new cArrayList("@day_cd", cbdaycode.SelectedValue.ToString()));
        arr.Add(new cArrayList("@emp_cd", cbsalesman.SelectedValue.ToString()));
        arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
        bll.vInsertMstRps(arr);
        arr.Clear();
        arr.Add(new cArrayList("@day_cd", cbdaycode.SelectedValue.ToString()));
        arr.Add(new cArrayList("@emp_cd", cbsalesman.SelectedValue.ToString()));
        bll.vDelRpsDtl(arr);
        foreach (GridViewRow row in grd.Rows)
        {
            Label lbcustcode = (Label)row.FindControl("lbcustcode");
            arr.Clear();
            arr.Add(new cArrayList("@cust_cd", lbcustcode.Text));
            arr.Add(new cArrayList("@day_cd", cbdaycode.SelectedValue.ToString()));
            arr.Add(new cArrayList("@emp_cd", cbsalesman.SelectedValue.ToString()));
            arr.Add(new cArrayList("@created_dt", System.DateTime.Today.ToShortDateString()));
            arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@sequenceno", nCount));
            bll.vInsertRpsDtl(arr);
            nCount++;
        }

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('RPS has been saved','RPS has saved','info');", true);  
    }
    protected void cbsalesman_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vDelWrkCustomer(arr);
        arr.Add(new cArrayList("@day_cd", cbdaycode.SelectedValue.ToString()));
        arr.Add(new cArrayList("@emp_cd", cbsalesman.SelectedValue.ToString()));
        bll.vInsertWrkCustomerFromRpsDtl(arr);
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_twrk_customer_get", arr);
        //txcustsearch_AutoCompleteExtender.ContextKey = "";
        //txcustsearch_AutoCompleteExtender.ContextKey = cbsalesman.SelectedValue.ToString();
    }
    protected void cbdaycode_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vDelWrkCustomer(arr);
        arr.Add(new cArrayList("@day_cd", cbdaycode.SelectedValue.ToString()));
        arr.Add(new cArrayList("@emp_cd", cbsalesman.SelectedValue.ToString()));
        bll.vInsertWrkCustomerFromRpsDtl(arr);
        arr.Clear();
        BindingGrid();
    }

    void BindingGrid()
    {

        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_twrk_customer_get", arr);
        arr.Clear();
        arr.Add(new cArrayList("@emp_cd", cbsalesman.SelectedValue.ToString()));
        arr.Add(new cArrayList("@monthcd", cbMonthCD.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grdbydate, "sp_trps_bydate_get", arr);
    }
    protected void cbrpstype_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        string sqrycd = "";
        if (cbrpstype.SelectedValue == "SAL")
        {
            sqrycd = "SalesCD";
        }
        else { sqrycd = "merchandcd"; }
        arr.Add(new cArrayList("@qry_cd", sqrycd));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBindingComboToSp(ref cbsalesman, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
        cbsalesman_SelectedIndexChanged(sender, e);
        arr.Clear();
    }
    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbcustcode = (Label)grd.Rows[e.RowIndex].FindControl("lbcustcode");

        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@cust_cd", lbcustcode.Text));
        bll.vDelWrkCustomer(arr);
        BindingGrid();
    }

    protected void brprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=rp&da=" + cbdaycode.SelectedValue.ToString() + "');",true);
    }
    protected void rdby_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdby.SelectedIndex == 0)
        {
            UpdatePanel4.Visible = false;
            lbdate.Visible = false;
            //lb1.Visible = false;
            //cbdaycode.Visible = true;
         }
        else
        {
            UpdatePanel4.Visible = true;
            lbdate.Visible = true;
            //lb1.Visible = true;
            //cbdaycode.Visible = false;
        }

    }
    protected void cbMonthCD_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindingGrid();
    }
}