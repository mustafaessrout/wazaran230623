using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmPrnStockAcc : System.Web.UI.Page
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
            bll.vBindingComboToSp(ref cbSalesPointCD, "sp_tmst_salespoint_getall", "salespointcd", "salespoint_desc");
            cbSalesPointCD.SelectedValue = Request.Cookies["sp"].Value;
            bll.vBindingComboToSp(ref cbMonthCD, "sp_tmst_period_get", "period_cd", "period_nm");
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
            bll.vBindingComboToSp(ref cbBINFr, "sp_tfield_value_Bin_Cd_get", "ID", "BinName");
            string sho = bll.sGetControlParameter("salespoint");
            //cbSalesPointCD.SelectedValue = sho;
            cbSalesPointCD.CssClass = "form-control ro";
            //if (sho == "0")
            //{
            //    cbSalesPointCD.Enabled = true;
            //    cbSalesPointCD.CssClass = "";
            //}
            //else
            //{
            //    cbSalesPointCD.Enabled = false;
            //    cbSalesPointCD.CssClass = "makeitreadonly ro form-control";
            //}

        }
    }
    protected void cbwhs_cd_SelectedIndexChanged(object sender, EventArgs e)
    {
        //    cbbin_cd.Items.Clear();
        //    List<cArrayList> arr = new List<cArrayList>();
        //    if (cbsiteDest.SelectedValue.ToString() == "VS")
        //    {
        //        arr.Add(new cArrayList("@vhc_cd", cbwhs_cd.SelectedValue.ToString()));
        //        bll.vBindingComboToSp(ref cbbin_cd, "sp_tvan_bin_get", "bin_cd", "bin_nm", arr);
        //    }
        //    if (cbsiteDest.SelectedValue.ToString() == "DEPO")
        //    {
        //        arr.Add(new cArrayList("@whs_cd", cbwhs_cd.SelectedValue.ToString()));
        //        bll.vBindingComboToSp(ref cbbin_cd, "sp_twarehouse_bin_get", "bin_cd", "bin_nm", arr);
        //    }
        //    if (cbsiteDest.SelectedValue.ToString() == "SUB")
        //    {
        //        arr.Add(new cArrayList("@whs_cd", cbwhs_cd.SelectedValue.ToString()));
        //        bll.vBindingComboToSp(ref cbbin_cd, "sp_twarehouse_bin_get", "bin_cd", "bin_nm", arr);
        //    }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@whs_cd", cbwhs_cd.SelectedValue));
        //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        bll.vBindingComboToSp(ref cbBINFr, "sp_twarehouse_bin_get", "bin_cd", "bin_nm", arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);

    }
    protected void cbsiteDest_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (cbsiteDest.SelectedValue == "DEPO")
        {
            arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue));
            arr.Add(new cArrayList("@level_no", 1));
            bll.vBindingComboToSp(ref cbwhs_cd, "sp_tmst_warehouse_get", "whs_cd", "whs_nm", arr);
            lbwhs.Text = "Warehouse";
        }
        if (cbsiteDest.SelectedValue == "SUB")
        {
            arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue));
            arr.Add(new cArrayList("@level_no", 2));
            bll.vBindingComboToSp(ref cbwhs_cd, "sp_tmst_warehouse_get", "whs_cd", "whs_nm", arr);
            lbwhs.Text = "Wareshouse";
        }
        if (cbsiteDest.SelectedValue == "VS")
        {
            arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue));
            bll.vBindingComboToSp(ref cbwhs_cd, "sp_tmst_vehicle_salesman_get", "vhc_cd", "emp_nm", arr);
            lbwhs.Text = "VAN";

        }
        cbwhs_cd_SelectedIndexChanged(sender, e);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        if (txfrom.Text == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Start Date must choosen','Start Date','warning');", true);
            return;
        }

        DateTime dtfrom = Convert.ToDateTime(DateTime.ParseExact(txfrom.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture));
        DateTime dtto = Convert.ToDateTime(DateTime.ParseExact(txto.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture));
        DateTime startOfMonth = new DateTime(dtfrom.Year, dtfrom.Month, 1);
        DateTime lastOfThisMonth = new DateTime(dtfrom.Year, dtfrom.Month, DateTime.DaysInMonth(dtfrom.Year, dtfrom.Month));
        
        if (dtfrom < startOfMonth)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Start Date outside this periode ','Start Date','warning');", true);
            return;
        }
        if (dtto > lastOfThisMonth)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Date To outside this periode ','Start Date','warning');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@SalesPointCD", cbSalesPointCD.SelectedValue));
        arr.Add(new cArrayList("@MonthCD", cbMonthCD.SelectedValue));
        //arr.Add(new cArrayList("@siteDest", cbsiteDest.SelectedValue));
        //arr.Add(new cArrayList("@whs_cd", cbwhs_cd.SelectedValue));
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
            Session["lParamAccstockDtl"] = arr;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=AccstockDtl');", true);
        }
        else if (rbRpt.SelectedValue == "S")
        {
            Session["lParamAccstockSumm"] = arr;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=AccstockSumm');", true);
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
        if (rbRpt.SelectedValue == "S" )
        {
            cbsiteDest.Visible = false;
            cbwhs_cd.Visible = false;
            //cbbin_cd.Visible = false;
            lbwhs.Visible = false;
            //lbbin.Visible = false;
            lbsitetype.Visible = false;
            txfrom.Enabled = false;
            txto.Enabled = false;
            cbMonthCD_SelectedIndexChanged(sender, e);

        }
        else if (rbRpt.SelectedValue == "D")
        {
            cbsiteDest.Visible = false;
            cbwhs_cd.Visible = false;
            //cbbin_cd.Visible = true;
            lbwhs.Visible = false;
            lbbin.Visible = true;
            lbsitetype.Visible = false;
            txfrom.Enabled = true;
            txto.Enabled = true;

        }
       
    }
    protected void cbSalesPointCD_SelectedIndexChanged(object sender, EventArgs e)
    {


        if (cbSalesPointCD.SelectedValue == "-1" && rbRpt.SelectedValue == "S")
        {

            cbwhs_cd.Visible = false;
            lbwhs.Visible = false;

            cbsiteDest.Visible = false;
            lbsitetype.Visible = false;

        }
        else
        {
            cbwhs_cd.Visible = true;
            lbwhs.Visible = true;

            cbsiteDest.Visible = true;
            lbsitetype.Visible = true;

            cbsiteDest_SelectedIndexChanged(sender, e);
        }
    }

    protected void txto_TextChanged(object sender, EventArgs e)
    {

        //string strFrom, strTo;
        //strFrom = bll.vLookUp("select start_dt from tmst_period where period_cd='"+cbMonthCD.SelectedValue+"'");  // bll.vLookUp("select convert(varchar, ymtStart, 103)  from tblTRYearMonth where period='" + cbMonthCD.SelectedValue + "'");
        //strTo = bll.vLookUp("select end_dt from tmst_period where period_cd='" + cbMonthCD.SelectedValue + "'"); //bll.vLookUp("select  convert(varchar,ymtEnd, 103) from tblTRYearMonth where period='" + cbMonthCD.SelectedValue + "'");
        //DateTime dtstrfrom = Convert.ToDateTime( strFrom); //DateTime.ParseExact(txfrom.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //DateTime dtstrto = Convert.ToDateTime( strTo); //DateTime.ParseExact(txto.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //DateTime dtfrom = dtstrfrom; // DateTime.ParseExact(txfrom.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //DateTime dtto = dtstrto; DateTime.ParseExact(txto.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //if (dtto < dtstrfrom || dtto > dtstrto)
        //{
        //    txto.Text = strTo;
        //}
        //bindinggrd();
    }
    protected void txfrom_TextChanged(object sender, EventArgs e)
    {
        string strFrom, strTo;
        strFrom = bll.vLookUp("select convert(varchar, ymtStart, 103)  from tblTRYearMonth where period='" + cbMonthCD.SelectedValue + "'");
        strTo = bll.vLookUp("select  convert(varchar,ymtEnd, 103) from tblTRYearMonth where period='" + cbMonthCD.SelectedValue + "'");
        DateTime dtstrfrom = DateTime.ParseExact(txfrom.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime dtstrto = DateTime.ParseExact(txto.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime dtfrom = DateTime.ParseExact(txfrom.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime dtto = DateTime.ParseExact(txto.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        if (dtfrom < dtstrfrom || dtfrom > dtstrto)
        {
            txfrom.Text = strFrom;
        }
    }

}