using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmprnlistcustbysalesman : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string waz_dt;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@qry_cd", "SalesJob"));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingComboToSp(ref cbsalesman, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);

            waz_dt = Request.Cookies["waz_dt"].Value.ToString();
            DateTime dtwaz_dt = DateTime.ParseExact(waz_dt, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            waz_dt = dtwaz_dt.ToString("yyyyMM");
            bll.vBindingComboToSp(ref cbMonthCDFr, "sp_tmst_period_get", "period_cd", "period_nm");
            bll.vBindingComboToSp(ref cbMonthCDTo, "sp_tmst_period_get", "period_cd", "period_nm");
            cbMonthCDFr.SelectedValue = waz_dt;
            cbMonthCDTo.SelectedValue = waz_dt;
            dateperiod();
        }
    }
    protected void btprint_Click(object sender, EventArgs e)
    {

        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@SalesPointCD", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@dtStart", DateTime.ParseExact(txfrom.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToShortDateString()));
        arr.Add(new cArrayList("@dtTo", DateTime.ParseExact(txto.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToShortDateString()));

        if (!chall.Checked)
        {
            arr.Add(new cArrayList("@Salesman", cbsalesman.SelectedValue));
            Session["lParamlistcustbysalesman"] = arr;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=listcustbysalesman');", true);
        }
        else
        {
            Session["lParamlistcustbysalesman"] = arr;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=listcustbybranch');", true);
        }


    }
    protected void cbMonthCDFr_SelectedIndexChanged(object sender, EventArgs e)
    {
        dateperiod();
    }
    protected void cbMonthCDTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        dateperiod();
    }
    private void dateperiod()
    {
        string strFrom, strTo;
        strFrom = bll.vLookUp("select convert(varchar, start_dt, 103)  from tmst_period where period_cd='" + cbMonthCDFr.SelectedValue + "'");
        strTo = bll.vLookUp("select convert(varchar, end_dt, 103)  from tmst_period where period_cd='" + cbMonthCDTo.SelectedValue + "'");
        txfrom.Text = strFrom;
        txto.Text = strTo;
    }
    protected void btclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx");
    }

    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
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