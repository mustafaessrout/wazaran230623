using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Fm_rptclosingstockjaretmonthly : System.Web.UI.Page
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
            cbMonthCD_SelectedIndexChanged(sender, e);
            bll.vBindingFieldValueToCombo(ref cbwhs_type, "whs_typ");


        }
    }
    protected void btclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx");
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        string speriod=cbMonthCD.SelectedValue;
        string ssitedest = cbwhs_type.SelectedValue;
        if (ssitedest == "DEPO")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=closingstockjaretmonthly&period=" + speriod + "');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=closingstockjaretVanSubdepomonthly&period=" + speriod + "&sitedest=" + ssitedest + "');", true);
        }
    }
    protected void txto_TextChanged(object sender, EventArgs e)
    {

    }
    protected void cbMonthCD_SelectedIndexChanged(object sender, EventArgs e)
    {
        string  strTo;
        strTo = bll.vLookUp("select  convert(varchar,ymtEnd, 103) from tblTRYearMonth where period='" + cbMonthCD.SelectedValue + "'");
        txto.Text = strTo;
    }
}