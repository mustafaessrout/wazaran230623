using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_customerreport : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
            cbcst_SelectedIndexChanged(sender, e);
        }
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (chsalesman.Checked)
        {
            arr.Add(new cArrayList("@salesman_Cd", null));
        }
        else
        {
            arr.Add(new cArrayList("@salesman_cd", hdsalesman_cd.Value.ToString()));
        }
        
            Session["lParamcusrp"] = arr;
        if (cbcst.SelectedValue.ToString()=="0")
        {
            
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=cusrp&p=0');", true);
        }
        else if (cbcst.SelectedValue.ToString() == "1")
        {            
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=cusrp&p=1');", true);
        }
        else if (cbcst.SelectedValue.ToString() == "2")
        {           
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=cusdoc');", true);
        }
        else if (cbcst.SelectedValue.ToString() == "3")
        {
            if (chsalesman.Checked)
            {
                arr.Clear();
                arr.Add(new cArrayList("@salesman_Cd", null));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                Session["lParamcusrp"] = arr;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=cusdocdt');", true);
            }
            else
            {
                arr.Clear();
                arr.Add(new cArrayList("@salesman_Cd", hdsalesman_cd.Value.ToString()));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                Session["lParamcusrp"] = arr;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=cusdocdt');", true);
            }
        }
        else if (cbcst.SelectedValue.ToString() == "4")
        {
            string dtYearStart, dtMonthStart, dtDayStart, dtStart;
            string dtYearEnd, dtMonthEnd, dtDayEnd, dtEnd;
            dtYearStart = DateTime.ParseExact(dtdata.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year.ToString();
            dtMonthStart = DateTime.ParseExact(dtdata.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month.ToString();
            dtDayStart = DateTime.ParseExact(dtdata.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).Day.ToString();
            dtStart = dtYearStart + "-" + dtMonthStart + "-" + dtDayStart;

            dtYearEnd = DateTime.ParseExact(dtdata1.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year.ToString();
            dtMonthEnd = DateTime.ParseExact(dtdata1.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month.ToString();
            dtDayEnd = DateTime.ParseExact(dtdata1.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).Day.ToString();
            dtEnd = dtYearEnd + "-" + dtMonthEnd + "-" + dtDayEnd;

            arr.Clear();
            arr.Add(new cArrayList("@start_dt", dtStart));
            arr.Add(new cArrayList("@end_dt", dtEnd));
            arr.Add(new cArrayList("@product", cbproduct.SelectedValue.ToString()));
            arr.Add(new cArrayList("@item", cbitem.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespoint", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@customer", "NOT BUY"));
            Session["lParamcusrp"] = arr;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=custnotbuy');", true);
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
    protected void cbcst_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbcst.SelectedValue.ToString() == "2")
        {
            txsalesman.Enabled = false;
            viewProduct.Visible = false;
            viewSalesman.Visible = true;
            viewPeriod.Visible = false;
        }
        else if (cbcst.SelectedValue.ToString() == "4")
        {
            List<cArrayList> arr = new List<cArrayList>();
            viewProduct.Visible = true;
            viewSalesman.Visible = false;
            viewPeriod.Visible = true;
            arr.Add(new cArrayList("@level_no", "3"));
            bll.vBindingComboToSp(ref cbproduct, "sp_tmst_product_get", "prod_cd", "prod_nm",arr);
            cbproduct.Items.Insert(0, new ListItem("All Product", "ALL"));
            cbproduct_SelectedIndexChanged(sender, e);
        }
        else
        {
            txsalesman.Enabled = true;
            viewProduct.Visible = false;
            viewSalesman.Visible = true;
            viewPeriod.Visible = false;
        }
    }
    protected void chsalesman_CheckedChanged(object sender, EventArgs e)
    {
        if (chsalesman.Checked)
        {
            txsalesman.CssClass = "form-control ro";
        }
        else
        {
            txsalesman.CssClass = "form-control ";
        }
        
    }
    protected void cbproduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@prod_cd", cbproduct.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbitem, "sp_tmst_item_get", "item_cd", "item_nm", arr);
        cbitem.Items.Insert(0, new ListItem("All Item", "ALL"));
    }
}