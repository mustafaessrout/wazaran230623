using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_poreport : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingComboToSp(ref cbperiod, "sp_tmst_period_get", "period_cd", "period_nm");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("fld_nm", "poreport"));
            bll.vBindingComboToSp(ref cbtyp, "sp_tfield_value_get", "fld_valu", "fld_desc", arr);
            string waz_dt;
            waz_dt = Request.Cookies["waz_dt"].Value.ToString();
            DateTime dtwaz_dt = DateTime.ParseExact(waz_dt, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            waz_dt = dtwaz_dt.ToString("yyyyMM");
            cbperiod.SelectedValue = waz_dt;
            cbperiod_SelectedIndexChanged(sender, e);
        }
    }

    protected void cbperiod_SelectedIndexChanged(object sender, EventArgs e)
    {
        string dstart, dend;
        dstart = bll.vLookUp("select convert(varchar, start_dt, 103)  from tmst_period where period_cd='" + cbperiod.SelectedValue + "'");
        dend = bll.vLookUp("select convert(varchar, end_dt, 103)  from tmst_period where period_cd='" + cbperiod.SelectedValue + "'");
        dtstart.Text = dstart;
        dtend.Text = dend;
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        //string X = cbtyp.SelectedValue.ToString();
        //startdate,enddate,p_user
        List<cArrayList> arr = new List<cArrayList>();
        DateTime dts = DateTime.ParseExact(dtstart.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime dte = DateTime.ParseExact(dtend.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);

        arr.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@startdate", dts.Year + "-" + dts.Month + "-" + dts.Day));
        arr.Add(new cArrayList("@enddate", dte.Year + "-" + dte.Month + "-" + dte.Day));
        Session["lParamporeport"] = arr;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=poreport');", true);

    }
}