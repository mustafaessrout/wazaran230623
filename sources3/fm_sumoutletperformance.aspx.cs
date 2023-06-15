using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_sumoutletperformance : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            chall.Enabled = false;
            //txdt1.Text = Request.Cookies["waz_dt"].Value.ToString();
            //txdt2.Text = Request.Cookies["waz_dt"].Value.ToString();
            bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_get", "salespointcd", "salespoint_desc");
            bll.vBindingComboToSp(ref cbper, "sp_tmst_period_get", "end_dt", "period_cd");
            cbsalespoint.SelectedValue = Request.Cookies["sp"].Value;

            if (Request.Cookies["sp"].Value != "0")
            {
                cbsalespoint.Enabled = false;
            }
            else
            {
                cbsalespoint.Enabled = true;
            }
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
    protected void btreport_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        DateTime dtpayp1 = DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        arr.Add(new cArrayList("@startdate", dtpayp1.Year.ToString() + "-" + dtpayp1.Month.ToString("00") + "-" + dtpayp1.Day.ToString("00")));
        arr.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        if (cbtype.SelectedValue.ToString() == "0")
        {
            if (cb120.Checked)
            {                
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=iv120');", true);
            }
            else
            {
                if (cbdt.Checked)
                {
                    Session["lParamoutletsperformance"] = arr;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=outletsperformanceacc');", true);
                }
                else
                {
                    Session["lParamoutletsperformance"] = arr;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=outletsperformance');", true);
                }
            }
        }
        else if (cbtype.SelectedValue.ToString() == "2")
        {
            if (chall.Checked)
            {
                Session["lParamoutletsperformance"] = arr;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=outletsperformancecs');", true);
            }
            else
            {
                if (txsalesman.Text == "")
                {

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Salesman Error','Please select Salesman','warning');", true);
                    return;
                }
                arr.Add(new cArrayList("@salesman_cd", hdsalesman_cd.Value.ToString()));
                Session["lParamoutletsperformance"] = arr;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=outletsperformancecss');", true);
            }
        }
        else if (cbtype.SelectedValue.ToString() == "1")
        {
            //TextBox tx = new TextBox();
            //tx.Text = bll.vLookUp("select end_dt from tmst_period where period_cd='" + cbper.SelectedItem.ToString() + "'");
            //Session["lParamoutletsperformance"] = arr;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=outletper1&startdate=" + cbper.SelectedItem.ToString() + "&salp=" + cbsalespoint.SelectedValue.ToString() + "');", true);

        }
    }
    protected void cbtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbtype.SelectedValue.ToString() == "0" || cbtype.SelectedValue.ToString() == "2")
        {
            chall.Enabled = false;
            cbperPnl.Visible = false;
            cbper.Visible = false;
            lblper.Visible = false;
            cbdt.Visible = true;
            cb120.Visible = true;
            txsalesman.CssClass = "RO";
            if (cbtype.SelectedValue.ToString() == "2")
            {
                cb120.Visible = false;
                cbdt.Visible = false;
                chall.Enabled = true;
                txsalesman.CssClass = "";
            }
        }
        else
        {
            txsalesman.CssClass = "RO";
            chall.Enabled = false;
            cbperPnl.Visible = true;
            cbper.Visible = true;
            lblper.Visible = true;
            cbdt.Visible = false;
            cb120.Visible = false;
        }
    }
    protected void chall_CheckedChanged(object sender, EventArgs e)
    {
        if (chall.Checked)
        {
            txsalesman.Enabled = true;
        }
        else
        {
            txsalesman.Enabled = false;
        }
    }
}