using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_prnstockHO : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btPrint_Click(object sender, EventArgs e)
    {

        if (dtfr.Text == "" || dtfr.Text ==null)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Date Can not Blank','error');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
            return;
        }

        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@dtto", DateTime.ParseExact(dtfr.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        Session["lParamBrnStkMonitorRpt"] = arr;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('/fm_report2.aspx?src=BrnStkMonitorRpt');", true);
    }
    private void bindinggrd()
    {
        DateTime dt = DateTime.ParseExact(dtfr.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        string sdate;
        sdate = dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day.ToString();
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@todate", sdate));
        bll.vBindingGridToSp(ref grd, "sp_rptStockHO_diff", arr);
    }
    protected void dtfr_TextChanged(object sender, EventArgs e)
    {
        bindinggrd();
    }
}