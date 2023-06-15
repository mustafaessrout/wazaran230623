using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using iTextSharp.text;
//using iTextSharp.text.pdf;
//using iTextSharp.text.html;
//using iTextSharp.text.html.simpleparser;
using System.IO;
public partial class fm_salescontract : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingComboToSp(ref cbbranch, "sp_tmst_salespoint_get", "salespointcd", "salespoint_desc");
            cbbranch.SelectedValue = Request.Cookies["sp"].Value.ToString();
            bll.vBindingComboToSp(ref cbProd_cdFr, "sp_tmst_product_get4", "ID", "ProdName");
            bll.vBindingComboToSp(ref cbProd_cdTo, "sp_tmst_product_get4", "ID", "ProdName");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@report", "SALESEX"));
            bll.vDelsoasalesman1(arr);
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
    }
   

    
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    //public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    //{
    //    cbll bll = new cbll();
    //    HttpCookie cok;
    //    cok = HttpContext.Current.Request.Cookies["sp"];
    //    List<string> lcust = new List<string>();
    //    List<cArrayList> arr = new List<cArrayList>();
    //    string scust = string.Empty;
    //    System.Data.SqlClient.SqlDataReader rs = null;
    //    arr.Add(new cArrayList("@cust_cd", prefixText));
    //    //arr.Add(new cArrayList("@salesman_cd", prefixText));
    //    arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
    //    //bll.vSearchSalesman(arr, ref rs);
    //    bll.vSearchMstCustomer(arr, ref rs);
    //    while (rs.Read())
    //    {
    //        scust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["emp_cd"].ToString() + "-" + rs["emp_nm"].ToString(), rs["emp_cd"].ToString());
    //        lcust.Add(scust);
    //    }
    //    rs.Close();

    //    return (lcust.ToArray());
    //}
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
        arr.Add(new cArrayList("@cust_cd", prefixText));
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        //bll.vSearchSalesman(arr, ref rs);
        bll.vSearchMstCustomer(arr, ref rs);
        while (rs.Read())
        {
            scust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_Cd"].ToString() + "-" + rs["cust_nm"].ToString(), rs["cust_cd"].ToString());
            lcust.Add(scust);
        }
        rs.Close();

        return (lcust.ToArray());
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        DateTime dtpayp1 = DateTime.ParseExact(dtstart.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime dtpayp2 = DateTime.ParseExact(dtend.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        string dt1 = dtpayp1.Year.ToString() + "-" + dtpayp1.Month.ToString("00") + "-" + dtpayp1.Day.ToString("00");
        string dt2 = dtpayp2.Year.ToString() + "-" + dtpayp2.Month.ToString("00") + "-" + dtpayp2.Day.ToString("00");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@startdate", dt1));
        arr.Add(new cArrayList("@enddate", dt2));
        arr.Add(new cArrayList("@salespointcd", cbbranch.SelectedValue.ToString()));
        arr.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@salesmacd", null));
        arr.Add(new cArrayList("@cbprod_cdFr", cbProd_cdFr.SelectedValue));
        arr.Add(new cArrayList("@cbprod_cdTo", cbProd_cdTo.SelectedValue));
        arr.Add(new cArrayList("@cbprod_cdFrtx", cbProd_cdFr.SelectedItem.Text));
        arr.Add(new cArrayList("@cbprod_cdTotx", cbProd_cdTo.SelectedItem.Text));

        if (cbreport.SelectedValue.ToString() == "0")
        {            
            Session["lParamsls"] = arr;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=salesbybranchbyproductcontract');", true);

        }
        else if (cbreport.SelectedValue.ToString() == "1")
        {           
            Session["lParamsls"] = arr;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=salesbysalesmanbyproductcontract');", true);

        }
		else if (cbreport.SelectedValue.ToString() == "2")
        {
            Session["lParamsls"] = arr;
            arr.Add(new cArrayList("@cust_cd", hdcust_cd.Value.ToString()));
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=salesofcustomerbyproductcontract');", true);

        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    
    protected void cbreport_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbreport.SelectedValue.ToString() == "2")
        {
            txcust.Enabled = true;
        }
        else
        {
            txcust.Enabled = false;
        }
    }
}