using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_attendance_list : System.Web.UI.Page
{
    cbll bll = new cbll();
    public string rptTYpe = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                List<cArrayList> arr = new List<cArrayList>();
                string waz_dt, waz_year;
                string sho;
                    
                waz_dt = Request.Cookies["waz_dt"].Value.ToString();
                DateTime dtwaz_dt = DateTime.ParseExact(waz_dt, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                waz_dt = dtwaz_dt.ToString("yyyyMM");
                waz_year = dtwaz_dt.ToString("yyyy");
                arr.Clear();
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                bll.vBindingComboToSp(ref cbSalesPointCD, "sp_tmst_salespoint_getuser", "salespointcd", "salespoint_desc", arr);
                cbSalesPointCD.Items.Insert(0, new ListItem("<< ALL Branch >>", "-1"));
                cbSalesPointCD.SelectedValue = Request.Cookies["sp"].Value.ToString();
                bll.vBindingComboToSp(ref cbMonthCD, "sp_tblTRYearMonth_get", "period", "ymtName");
                cbMonthCD.SelectedValue = waz_dt;
                cbMonthCD_SelectedIndexChanged(sender, e);
                sho = Request.Cookies["sp"].Value.ToString();
                if (sho == "0")
                {
                    cbSalesPointCD.Enabled = true;
                    cbSalesPointCD.CssClass = "form-control";
                    //cbSalesPointCD.Items.RemoveAt(0);
                    //cbsalespointmth.Items.RemoveAt(0);
                }
                else
                {
                    cbSalesPointCD.Enabled = false;
                    cbSalesPointCD.CssClass = "makeitreadonly ro form-control";
                }

            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_attendance_list");
                Response.Redirect("fm_ErrorPage.aspx");
            }
        }
    }

    protected void cbMonthCD_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strFrom, strTo;
        strFrom = bll.vLookUp("select convert(varchar, ymtStart, 103)  from tblTRYearMonth where period='" + cbMonthCD.SelectedValue + "'");
        strTo = bll.vLookUp("select  convert(varchar,ymtEnd, 103) from tblTRYearMonth where period='" + cbMonthCD.SelectedValue + "'");
        //DateTime dtTo =strTo; //DateTime.ParseExact(strTo, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        txfrom.Text = strFrom;
        txto.Text = strTo;
    }

    protected void txfrom_TextChanged(object sender, EventArgs e)
    {
        string strFrom, strTo;
        strFrom = bll.vLookUp("select convert(varchar, ymtStart, 103)  from tblTRYearMonth where period='" + cbMonthCD.SelectedValue + "'");
        strTo = bll.vLookUp("select  convert(varchar,ymtEnd, 103) from tblTRYearMonth where period='" + cbMonthCD.SelectedValue + "'");
        DateTime dtstrfrom = DateTime.ParseExact(strFrom, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime dtstrto = DateTime.ParseExact(strTo, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime dtfrom = DateTime.ParseExact(txfrom.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime dtto = DateTime.ParseExact(txto.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        if (dtfrom < dtstrfrom || dtfrom > dtstrto)
        {
            txfrom.Text = strFrom;
        }
    }

    protected void txto_TextChanged(object sender, EventArgs e)
    {
        string strFrom, strTo;
        strFrom = bll.vLookUp("select convert(varchar, ymtStart, 103)  from tblTRYearMonth where period='" + cbMonthCD.SelectedValue + "'");
        strTo = bll.vLookUp("select  convert(varchar,ymtEnd, 103) from tblTRYearMonth where period='" + cbMonthCD.SelectedValue + "'");
        DateTime dtstrfrom = DateTime.ParseExact(strFrom, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime dtstrto = DateTime.ParseExact(strTo, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime dtfrom = DateTime.ParseExact(txfrom.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime dtto = DateTime.ParseExact(txto.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        if (dtto < dtstrfrom || dtto > dtstrto)
        {
            txto.Text = strTo;
        }
    }

    protected void btprint_Click(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue));
            arr.Add(new cArrayList("@start_dt", DateTime.ParseExact(txfrom.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToShortDateString()));
            arr.Add(new cArrayList("@end_dt", DateTime.ParseExact(txto.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToShortDateString()));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            Session["lParamAttendance"] = arr;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=attendance');", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_attendance_list");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

}