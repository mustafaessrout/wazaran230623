using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_dailysalesmanreport1 : System.Web.UI.Page
{
    cbll bll = new cbll();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm");
            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
            bll.vBindingComboToSp(ref cbsalesman, "sp_tmst_salesman_getall", "salesman_cd", "salesman_nm", arr);
            cbreport_SelectedIndexChanged(sender, e);

        }
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        List<string> lcust = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string scust = string.Empty;
        System.Data.SqlClient.SqlDataReader rs = null;
        //arr.Add(new cArrayList("@cust_cd", prefixText));
        arr.Add(new cArrayList("@salesman_cd", prefixText));

        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        bll.vSearchSalesman(arr, ref rs);
        //bll.vSearchMstCustomer(arr, ref rs);
        while (rs.Read())
        {
            scust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["emp_cd"].ToString() + "-" + rs["emp_nm"].ToString(), rs["emp_cd"].ToString());
            lcust.Add(scust);
        }
        rs.Close();

        return (lcust.ToArray());
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();




        if (cbreport.SelectedValue.ToString() == "0")
        {
            DateTime dtpayp1 = DateTime.ParseExact(dtrps.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            arr.Add(new cArrayList("@rpsdate", dtpayp1.Year + "-" + dtpayp1.Month + "-" + dtpayp1.Day));
            Session["lParamrps"] = arr;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('/fm_report2.aspx?src=rpsdtl');", true);
        }
        else if (cbreport.SelectedValue.ToString() == "1")
        {
            DateTime dtpayp1 = DateTime.ParseExact(dtrps.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            arr.Add(new cArrayList("@dt", dtpayp1.Year + "-" + dtpayp1.Month + "-" + dtpayp1.Day));
            arr.Add(new cArrayList("@salesman_cd", cbsalesman.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
            Session["lParamrps1"] = arr;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('/fm_report2.aspx?src=rpsdtl1');", true);
        }
        else if (cbreport.SelectedValue.ToString() == "2")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('/fm_report2.aspx?src=salesmanrps');", true);
        }
        else if (cbreport.SelectedValue.ToString() == "3")
        {
            DateTime dtpayp1 = DateTime.ParseExact(dtrps.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime dtpayp2 = DateTime.ParseExact(dtrps2.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            arr.Add(new cArrayList("@dt", dtpayp1.Year + "-" + dtpayp1.Month + "-" + dtpayp1.Day));
            arr.Add(new cArrayList("@dt2", dtpayp2.Year + "-" + dtpayp2.Month + "-" + dtpayp2.Day));
            //arr.Add(new cArrayList("@salesman_cd", cbsalesman.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
            Session["lParamrps1"] = arr;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('/fm_report2.aspx?src=rpsdtl2');", true);
        }
    }
    protected void cbreport_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbreport.SelectedValue.ToString() == "0")
        {
            dtrps.Enabled = true;
            cbsalesman.Enabled = false;
            dtrps2.Enabled = false;
        }
        else if (cbreport.SelectedValue.ToString() == "1")
        {
            dtrps.Enabled = true;
            cbsalesman.Enabled = true;
            dtrps2.Enabled = false;
        }
        else if (cbreport.SelectedValue.ToString() == "2")
        {
            cbsalesman.Enabled = false;
            dtrps.Enabled = false;
            dtrps2.Enabled = false;
        }
        else if (cbreport.SelectedValue.ToString() == "3")
        {
            cbsalesman.Enabled = false;
            dtrps.Enabled = true;
            dtrps2.Enabled = true;
        }
    }
    protected void cbsalespoint_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbsalesman, "sp_tmst_salesman_getall", "salesman_cd", "salesman_nm", arr);
    }
    protected void btsendemail_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        DateTime dtsyst1 = DateTime.ParseExact(dtrps.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        arr.Add(new cArrayList("@dt", dtrps.Text));
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        Session["lParamdsr"] = arr;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('/fm_report3.aspx?src=dailysalesmanreportdetail');", true);
    }
}