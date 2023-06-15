using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_jared : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            string waz_dt;
            waz_dt=Request.Cookies["waz_dt"].Value.ToString();
            DateTime dtwaz_dt = DateTime.ParseExact(waz_dt, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            waz_dt = dtwaz_dt.ToString("yyyyMM");
            bll.vBindingComboToSp(ref cbSalesPointCD, "sp_tmst_salespoint_get", "salespointcd", "salespoint_desc");
            cbSalesPointCD.SelectedValue = Request.Cookies["sp"].Value;
            bll.vBindingComboToSp(ref cbMonthCD, "sp_tblTRYearMonth_get", "period", "ymtName");
            bll.vBindingFieldValueToCombo(ref cbsiteDest, "whs_typ");
            cbMonthCD.SelectedValue = waz_dt;
            cbsiteDest_SelectedIndexChanged(sender, e);
            cbMonthCD_SelectedIndexChanged(sender, e);
            rbRpt.SelectedValue = "D";
            rbRpt_SelectedIndexChanged(sender, e);
            bll.vBindingComboToSp(ref cbitem_cdFr, "sp_tmst_item_get4", "ID", "ItemName");
            bll.vBindingComboToSp(ref cbitem_cdTo, "sp_tmst_item_get4", "ID", "ItemName");
            bll.vBindingComboToSp(ref cbProd_cdFr, "sp_tmst_product_get3", "ID", "ProdName");
            bll.vBindingComboToSp(ref cbProd_cdTo, "sp_tmst_product_get3", "ID", "ProdName");
            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingComboToSp(ref cbBINFr, "sp_tfield_value_Bin_Cd_get", "ID", "BinName", arr);
            
        }
    }
    protected void cbwhs_cd_SelectedIndexChanged(object sender, EventArgs e)
    {    
    }
    protected void cbsiteDest_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (cbsiteDest.SelectedValue == "DEPO")
        {
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@level_no", 1));
            bll.vBindingComboToSp(ref cbwhs_cd, "sp_tmst_warehouse_get", "whs_cd", "whs_nm", arr);
            lbwhs.Text="Wareshouse";
        }
        if (cbsiteDest.SelectedValue == "SUB")
        {
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@level_no", 2));
            bll.vBindingComboToSp(ref cbwhs_cd, "sp_tmst_warehouse_get", "whs_cd", "whs_nm", arr);
            lbwhs.Text = "Wareshouse";
        }
        if (cbsiteDest.SelectedValue == "VS")
        {
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingComboToSp(ref cbwhs_cd, "sp_tmst_vehicle_salesman_get", "vhc_cd", "emp_nm", arr);
            lbwhs.Text = "VAN";
        }
        cbwhs_cd_SelectedIndexChanged(sender, e);
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        DateTime dtfrom = Convert.ToDateTime(DateTime.ParseExact(txfrom.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture));
        DateTime startOfMonth = new DateTime(dtfrom.Year, dtfrom.Month, 1);
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@SalesPointCD", cbSalesPointCD.SelectedValue));
        arr.Add(new cArrayList("@MonthCD", cbMonthCD.SelectedValue));
        arr.Add(new cArrayList("@siteDest", cbsiteDest.SelectedValue));
        arr.Add(new cArrayList("@whs_cd", cbwhs_cd.SelectedValue));
        //arr.Add(new cArrayList("@bin_cd", cbbin_cd.SelectedValue));
        arr.Add(new cArrayList("@dtFrom", DateTime.ParseExact(txfrom.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToShortDateString()));
        arr.Add(new cArrayList("@dtTo", DateTime.ParseExact(txto.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToShortDateString()));
        arr.Add(new cArrayList("@dtStart", startOfMonth.ToShortDateString()));
        arr.Add(new cArrayList("@item_cdFr", cbitem_cdFr.SelectedValue));
        arr.Add(new cArrayList("@item_cdTo", cbitem_cdTo.SelectedValue));
        arr.Add(new cArrayList("@prod_cdFr", cbProd_cdFr.SelectedValue));
        arr.Add(new cArrayList("@prod_cdTo", cbProd_cdTo.SelectedValue));
        arr.Add(new cArrayList("@Bin_cd_fr", cbBINFr.SelectedValue));

        if (rbRpt.SelectedValue == "D")
        {
            if (cbsiteDest.SelectedValue.ToString() == "DEPO")
            {
                Session["lParamjareddpo"] = arr;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=jareddpo');", true);
            }
            else if (cbsiteDest.SelectedValue.ToString() == "SubDepo")
            {

            }
            else
            {
                
            }
        }
        else if (rbRpt.SelectedValue == "S")
        {
            Session["lParamjared"] = arr;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=jared');", true);
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
    protected void rbRpt_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbRpt.SelectedValue == "S")
        {
            cbsiteDest.Visible = false;
            cbsiteDestPnl.Visible = false;
            cbwhs_cd.Visible = false;
            cbwhs_cdPnl.Visible = false;
            //cbbin_cd.Visible = false;
            lbwhs.Visible = false;
            //lbbin.Visible = false;
            lbsitetype.Visible = false;
            //lb1.Visible = false;
            //lb2.Visible = false;
            //lb3.Visible = false;
        }
        else if (rbRpt.SelectedValue == "D")
        {
            cbsiteDest.Visible = true;
            cbsiteDestPnl.Visible = true;
            cbwhs_cd.Visible = true;
            cbwhs_cdPnl.Visible = true;
            //cbbin_cd.Visible = true;
            lbwhs.Visible = true;
            lbbin.Visible = true;
            lbsitetype.Visible = true;
            //lb1.Visible = true;
            //lb2.Visible = true;
            //lb3.Visible = true;
        }
        
    }
}