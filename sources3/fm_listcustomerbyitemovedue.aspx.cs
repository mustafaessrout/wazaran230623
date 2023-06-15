using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_listcustomerbyitemovedue : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@qry_cd", "SalesJob"));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingComboToSp(ref cbsalesman, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
            bll.vBindingComboToSp(ref cbProd_cdFr, "sp_tmst_product_get3", "ID", "ProdName");
            bll.vBindingComboToSp(ref cbProd_cdTo, "sp_tmst_product_get3", "ID", "ProdName");
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
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        System.Data.SqlClient.SqlDataReader rs = null;
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        List<string> lCust = new List<string>();
        string sCust = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        // arr.Add(new cArrayList("@salesman_cd", contextKey));
        arr.Add(new cArrayList("@cust_cd", prefixText));
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        bll.vSearchMstCustomer(arr, ref rs);
        while (rs.Read())
        {
            sCust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + "-" + rs["cust_nm"].ToString(), rs["cust_cd"].ToString());
            lCust.Add(sCust);
        }
        rs.Close();
        return (lCust.ToArray());
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        if (txcustomer.Enabled == true)
        {
            if (txcustomer.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al2", "sweetAlert('Customer must be inserted','Customer Missing','error');", true);
                return;
            }
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue));
        arr.Add(new cArrayList("@prod_cdFr", cbProd_cdFr.SelectedValue));
        arr.Add(new cArrayList("@prod_cdTo", cbProd_cdTo.SelectedValue));
        arr.Add(new cArrayList("@prod_cdFrtx", cbProd_cdFr.SelectedItem.Text));
        arr.Add(new cArrayList("@prod_cdTotx", cbProd_cdTo.SelectedItem.Text));
        if (chsls.Checked)
        {
            arr.Add(new cArrayList("@salesman_cd", null));
        }
        else
        {
            arr.Add(new cArrayList("@salesman_cd", cbsalesman.SelectedValue));
        }

        if (chcust.Checked)
        {
            arr.Add(new cArrayList("@cust_cd", null));
        }
        else
        {
            arr.Add(new cArrayList("@cust_cd", hdcust.Value));
        }

        arr.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
        Session["lParamlistcustitemoverdue"] = arr;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=custitemoverdue');", true);
    }
    protected void chsls_CheckedChanged(object sender, EventArgs e)
    {
        if (chsls.Checked)
        {
            cbsalesman.Enabled = false;
        }
        else
        {
            cbsalesman.Enabled = true;
        }
    }
    protected void chcust_CheckedChanged(object sender, EventArgs e)
    {
        if (chcust.Checked)
        {
            txcustomer.Text = "";
            txcustomer.Enabled = false;
        }
        else
        {
            txcustomer.Enabled = true;
        }
    }
}