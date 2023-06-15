using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_rptsoa : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void batchsalesman()
    {
        List<cArrayList> arr = new List<cArrayList>();      
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
        arr.Add(new cArrayList("@startdate", DateTime.ParseExact(dtsoa.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@enddate", DateTime.ParseExact(dtsoato.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        bll.vBindingComboToSp(ref cbsalesman, "sp_soa_bycustslsmn", "salesman_cd", "emp_nm", arr);        
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            dtsoa.Text = Request.Cookies["waz_dt"].Value.ToString();
            dtsoato.Text = Request.Cookies["waz_dt"].Value.ToString();
            bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_get", "salespointcd", "salespoint_desc");
            cbsalespoint.SelectedValue = Request.Cookies["sp"].Value.ToString();
            if (Request.Cookies["sp"].Value.ToString() == "0")
            {
                cbsalespoint.Enabled = true;
            }
            else
            {
                cbsalespoint.Enabled = false;
            }
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        if (chall.Checked)
        {
            hdcust.Value = null;           
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        //string dtclose = dtsoato.Text;
        //DateTime dtclose2 = DateTime.ParseExact(dtclose, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //arr.Add(new cArrayList("@closing_dt", dtclose2.Year.ToString() + "-" + dtclose2.Month.ToString("00") + "-" + dtclose2.Day.ToString("00")));
        //bll.vsp_batchsoa(arr);  
        bll.vBatchBfrClosingday(arr);
        if (cbtype.SelectedValue.ToString()=="0")
        {
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=soa&startdate=" + dtsoa.Text + "&enddate=" + dtsoato.Text + "&p=" + cbtype.SelectedValue + "&salsp=" + cbsalespoint.SelectedValue.ToString() + "');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=soa&cust=" + hdcust.Value.ToString() + "&startdate=" + dtsoa.Text + "&enddate=" + dtsoato.Text + "&p=" + cbtype.SelectedValue + "&salsp=" + cbsalespoint.SelectedValue.ToString() + "&salesman=" + cbsalesman.SelectedValue.ToString() + "');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=soa&cust=" + hdcust.Value.ToString() + "&startdate=" + dtsoa.Text + "&enddate=" + dtsoato.Text + "&p=" + cbtype.SelectedValue + "&salsp=" + cbsalespoint.SelectedValue.ToString() + "&salesman=" + cbsalesman.SelectedValue.ToString() + "');", true);
        }
        
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        cbll bll = new cbll();
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lcust = new List<string>();
        string sCust = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cust_cd", prefixText));
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        bll.vSearchMstCustomer(arr, ref rs);
        while (rs.Read())
        {
            sCust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + " - " + rs["cust_nm"].ToString(), rs["cust_cd"].ToString());
            lcust.Add(sCust);            
        } rs.Close();
        return (lcust.ToArray());
             
    }
    protected void chall_CheckedChanged(object sender, EventArgs e)
    {
        if(chall.Checked)
        {
            txcust.Text = "";
            txcust.Enabled = false;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@qry_cd", "SalesJob"));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingComboToSp(ref cbsalesman, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
        }
        else
        {
            txcust.Text = "";
            txcust.Enabled = true;
        }
            
    }

    protected void cbtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        txcust.Text = "";
    }
    protected void dtsoa_TextChanged(object sender, EventArgs e)
    {
        dtsoa.Enabled = false;
    }

    protected void btsl_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
        arr.Add(new cArrayList("@startdate", DateTime.ParseExact(dtsoa.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@enddate", DateTime.ParseExact(dtsoato.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        bll.vBindingComboToSp(ref cbsalesman, "sp_soa_bycustslsmn", "salesman_cd", "emp_nm", arr);
    }
}