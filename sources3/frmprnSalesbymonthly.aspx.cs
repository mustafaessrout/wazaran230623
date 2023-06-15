using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmprnSalesbymonthly : System.Web.UI.Page
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
        bll.vBindingComboToSp(ref cbYearCD, "sp_tblTRYear_get", "YearCD", "YearCD");
        
        List<cArrayList> arr = new List<cArrayList>(); // Get salesman Info
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        //arr.Add(new cArrayList("@qry_cd", "SalesJob"));
        bll.vBindingComboToSp(ref cbsalesman, "sp_tmst_employee_salesman_get", "ID", "EmpName", arr);
        bll.vBindingComboToSp(ref cbotlcdFr, "sp_tfield_value_otlcd_get", "ID", "otlName");
        bll.vBindingComboToSp(ref cbotlcdTo, "sp_tfield_value_otlcd_get", "ID", "otlName");
        bll.vBindingComboToSp(ref cbitem_cdFr, "sp_tmst_item_get4", "ID", "ItemName");
        bll.vBindingComboToSp(ref cbitem_cdTo, "sp_tmst_item_get4", "ID", "ItemName");
        bll.vBindingComboToSp(ref cbProd_cdFr, "sp_tmst_product_get3", "ID", "ProdName");
        bll.vBindingComboToSp(ref cbProd_cdTo, "sp_tmst_product_get3", "ID", "ProdName");
        bll.vBindingFieldValueToCombo(ref cbreport, "rptsalesbymonthly");
        cbYearCD.SelectedValue = waz_dt;
        cbYearCD_SelectedIndexChanged(sender, e);
        //cbreport_SelectedIndexChanged(sender, e);
        }
    }
    protected void btprint_Click(object sender, EventArgs e)
    {

        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@SalesPointCD", cbSalesPointCD.SelectedValue));
        arr.Add(new cArrayList("@dtFrom", DateTime.ParseExact(txfrom.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToShortDateString()));
        arr.Add(new cArrayList("@dtTo", DateTime.ParseExact(txto.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToShortDateString()));
        arr.Add(new cArrayList("@salesman_cd", cbsalesman.SelectedValue));
        arr.Add(new cArrayList("@otlcdFr", cbotlcdFr.SelectedValue));
        arr.Add(new cArrayList("@otlcdTo", cbotlcdTo.SelectedValue));
        arr.Add(new cArrayList("@item_cdFr", cbitem_cdFr.SelectedValue));
        arr.Add(new cArrayList("@item_cdTo", cbitem_cdTo.SelectedValue));
        arr.Add(new cArrayList("@prod_cdFr", cbProd_cdFr.SelectedValue));
        arr.Add(new cArrayList("@prod_cdTo", cbProd_cdTo.SelectedValue));

        string sreport = "Salesbymonthly"+cbreport.SelectedValue;
        Session["lParamSalesbymonthly" + cbreport.SelectedValue] = arr;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src="+sreport+"');", true);
       
    }

    protected void cbYearCD_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strFrom, strTo;
        strFrom = bll.vLookUp("select convert(varchar, yeaStart, 103)  from tblTRYear where yearCd='" + cbYearCD.SelectedValue + "'");
        strTo = bll.vLookUp("select  convert(varchar,yeaEnd, 103) from tblTRYear where yearCd='" + cbYearCD.SelectedValue + "'");
        txfrom.Text = strFrom;
        txto.Text = strTo;
        cbreport_SelectedIndexChanged(sender, e);
    }
    protected void cbreport_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbreport.SelectedValue.ToString() == "1" || cbreport.SelectedValue.ToString() == "2" || cbreport.SelectedValue.ToString() == "4")
        {
        }
        else
        {
            txfrom.Enabled = true;
            txto.Enabled = true;
        }
    }
}