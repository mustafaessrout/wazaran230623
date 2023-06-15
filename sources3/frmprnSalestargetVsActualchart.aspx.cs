using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmprnSalestargetVsActualchart : System.Web.UI.Page
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
        bll.vBindingComboToSp(ref cbSalesPointCD, "sp_tmst_salespoint_get", "salespointcd", "salespoint_desc");
        cbSalesPointCD.SelectedValue = Request.Cookies["sp"].Value;
        bll.vBindingComboToSp(ref cbMonthCD, "sp_tblTRYearMonth_get", "period", "ymtName");
        cbMonthCD.SelectedValue = waz_dt;
        cbMonthCD_SelectedIndexChanged(sender, e);
        List<cArrayList> arr = new List<cArrayList>(); // Get salesman Info
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@qry_cd", "SalesJob"));
        bll.vBindingComboToSp(ref cbsalesman, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
        }
    }
    protected void btprint_Click(object sender, EventArgs e)
    {

        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@SalesPointCD", cbSalesPointCD.SelectedValue));
        arr.Add(new cArrayList("@dtFrom", DateTime.ParseExact(txfrom.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToShortDateString()));
        arr.Add(new cArrayList("@dtTo", DateTime.ParseExact(txto.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToShortDateString()));
        arr.Add(new cArrayList("@salesman_cd", cbsalesman.SelectedValue));

        Session["lParamSalestargetVsActualchart"] = arr;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=SalestargetVsActualchart');", true);
       
    }
    protected void cbMonthCD_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strFrom, strTo;
        strFrom = bll.vLookUp("select convert(varchar, ymtStart, 103)  from tblTRYearMonth where period='" + cbMonthCD.SelectedValue + "'");
        strTo = bll.vLookUp("select  convert(varchar,ymtEnd, 103) from tblTRYearMonth where period='" + cbMonthCD.SelectedValue + "'");
        txfrom.Text = strFrom;
        txto.Text = strTo;
    }
}