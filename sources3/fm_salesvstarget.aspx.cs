using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_salesvstarget : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txdt1.Text = Request.Cookies["waz_dt"].Value.ToString();
            txdt2.Text = Request.Cookies["waz_dt"].Value.ToString();
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

            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@qry_cd", "SalesJob"));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingComboToSp(ref cbsalesman, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
            bll.vBindingComboToSp(ref cbProd_cdFr, "sp_tmst_product_get4", "ID", "ProdName");
            bll.vBindingComboToSp(ref cbProd_cdTo, "sp_tmst_product_get4", "ID", "ProdName");
            arr.Clear();
            arr.Add(new cArrayList("@fld_nm", "salesvstarget"));
            bll.vBindingComboToSp(ref cbreporttype, "sp_tfield_value_get", "fld_valu", "fld_desc", arr);

        }
    }

    protected void btreport_Click(object sender, EventArgs e)
    {

        DateTime dtpayp1 = DateTime.ParseExact(txdt1.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime dtpayp2 = DateTime.ParseExact(txdt2.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        string dt1 = dtpayp1.Year.ToString() + "-" + dtpayp1.Month.ToString("00") + "-" + dtpayp1.Day.ToString("00");
        string dt2 = dtpayp2.Year.ToString() + "-" + dtpayp2.Month.ToString("00") + "-" + dtpayp2.Day.ToString("00");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@startdate", dt1));
        arr.Add(new cArrayList("@enddate", dt2));
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        arr.Add(new cArrayList("@cbprod_cdFr", cbProd_cdFr.SelectedValue));
        arr.Add(new cArrayList("@cbprod_cdTo", cbProd_cdTo.SelectedValue));
        if (cbreporttype.SelectedValue == "0")
        {
            if (chall.Checked)
            {

                Session["lParamslsvstrg"] = arr;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=salesvstargetsalesman');", true);
            }
            else
            {
                arr.Add(new cArrayList("@salesman_cd", cbsalesman.SelectedValue.ToString()));
                Session["lParamslsvstrg"] = arr;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=salesvstarget');", true);
            }
        }
        else if (cbreporttype.SelectedValue == "1")
        {
            Session["lParamslsvstrg"] = arr;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=salesvstargetbranch');", true);
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
        }
        rs.Close();
        return (lcust.ToArray());

    }
    protected void cbreporttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbreporttype.SelectedValue == "1")
        {
            cbsalesman.Enabled = false;
        }
        else
        {
            if (chall.Checked)
            {
                cbsalesman.Enabled = false;
            }
            else
            {
                cbsalesman.Enabled = true;

            }

        }

    }
    protected void chall_CheckedChanged(object sender, EventArgs e)
    {
        if (chall.Checked)
        {
            cbsalesman.Enabled = false;
        }
        else
        {
            cbsalesman.Enabled = true;

        }
    }
}