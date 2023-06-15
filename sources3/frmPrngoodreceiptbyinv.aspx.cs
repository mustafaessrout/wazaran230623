using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmPrnStock : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string waz_dt;
            waz_dt=Request.Cookies["waz_dt"].Value.ToString();
            DateTime dtwaz_dt = DateTime.ParseExact(waz_dt, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            waz_dt = dtwaz_dt.ToString("yyyyMM");
            bll.vBindingComboToSp(ref cbSalesPointCD, "sp_tmst_salespoint_get", "salespointcd", "salespoint_desc");
            cbSalesPointCD.SelectedValue = Request.Cookies["sp"].Value;
            bll.vBindingComboToSp(ref cbMonthCD, "sp_tblTRYearMonth_get", "period", "ymtName");
            cbMonthCD.SelectedValue = waz_dt;
            cbMonthCD_SelectedIndexChanged(sender, e);
            bll.vBindingComboToSp(ref cbitem_cdFr, "sp_tmst_item_get4", "ID", "ItemName");
            bll.vBindingComboToSp(ref cbitem_cdTo, "sp_tmst_item_get4", "ID", "ItemName");
            bll.vBindingComboToSp(ref cbProd_cdFr, "sp_tmst_product_get3", "ID", "ProdName");
            bll.vBindingComboToSp(ref cbProd_cdTo, "sp_tmst_product_get3", "ID", "ProdName");
            
        }
    }
   
    protected void btprint_Click(object sender, EventArgs e)
    {
        //DateTime dtfrom = Convert.ToDateTime(DateTime.ParseExact(txfrom.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture));
        //DateTime startOfMonth = new DateTime(dtfrom.Year, dtfrom.Month, 1);
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@SalesPointCD", cbSalesPointCD.SelectedValue));
        //arr.Add(new cArrayList("@MonthCD", cbMonthCD.SelectedValue));
        arr.Add(new cArrayList("@dtFrom", DateTime.ParseExact(txfrom.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToShortDateString()));
        arr.Add(new cArrayList("@dtTo", DateTime.ParseExact(txto.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToShortDateString()));
        //arr.Add(new cArrayList("@dtStart", startOfMonth.ToShortDateString()));
        arr.Add(new cArrayList("@item_cdFr", cbitem_cdFr.SelectedValue));
        arr.Add(new cArrayList("@item_cdTo", cbitem_cdTo.SelectedValue));
        arr.Add(new cArrayList("@prod_cdFr", cbProd_cdFr.SelectedValue));
        arr.Add(new cArrayList("@prod_cdTo", cbProd_cdTo.SelectedValue));

        Session["lParamgoodreceiptbyreference"] = arr;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();openreport('fm_report2.aspx?src=goodreceiptbyreference');", true);
       
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