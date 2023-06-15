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
            bll.vBindingComboToSp(ref cbSalesPointCD, "sp_tmst_salespoint_getall", "salespointcd", "salespoint_desc");
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
            bll.vBindingComboToSp(ref cbBINFr, "sp_tfield_value_Bin_Cd_get", "ID", "BinName");
            string sho = bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='salespoint'");
            if (sho=="0")
            {
                cbSalesPointCD.Enabled = true;
            }
            else
            {
                cbSalesPointCD.Enabled = false;
            }
            
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
        
    }
    protected void cbsiteDest_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (cbsiteDest.SelectedValue == "DEPO")
        {
            arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue));
            arr.Add(new cArrayList("@level_no", 1));
            bll.vBindingComboToSp(ref cbwhs_cd, "sp_tmst_warehouse_get", "whs_cd", "whs_nm", arr);
            lbwhs.Text="Wareshouse";
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
            Session["lParamstock"] = arr;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=stock');", true);
        }
        else if (rbRpt.SelectedValue == "S")
        {
            Session["lParamstockSumm"] = arr;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=stockSumm');", true);
        }
        else if (rbRpt.SelectedValue == "J")
        {
            Session["lParamjared"] = arr;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=jared');", true);
        }
        else if (rbRpt.SelectedValue == "F")
        {
            string swhs_nm,sbin_nm,sdatefr,sdateto; 
            if (cbwhs_cd.SelectedValue=="-1") {swhs_nm="ALL";}else {swhs_nm=cbwhs_cd.SelectedItem.Text;};
            if (cbBINFr.SelectedValue=="-1") {sbin_nm="ALL";}else {sbin_nm=cbBINFr.SelectedItem.Text;};
            sdatefr=txfrom.Text;
            sdateto = txto.Text;
            Session["lParamstockForm"] = arr;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "popupwindow('/reporting/fm_rptstockForm.aspx?qswhs_nm=" + swhs_nm + "&qsbin_nm=" + sbin_nm + "&qsdatefr=" + sdatefr + "&qsdateto=" + sdateto + "');", true);
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
            cbwhs_cd.Visible = false;
            //cbbin_cd.Visible = false;
            lbwhs.Visible = false;
            //lbbin.Visible = false;
            lbsitetype.Visible = false;
       
        }
        else if (rbRpt.SelectedValue == "D")
        {
            cbsiteDest.Visible = true;
            cbwhs_cd.Visible = true;
            //cbbin_cd.Visible = true;
            //lbwhs.Visible = true;
            lbbin.Visible = true;
            lbsitetype.Visible = true;
       
        }
        else if (rbRpt.SelectedValue == "J")
        {
            cbsiteDest.Visible = false;
            cbwhs_cd.Visible = false;
            cbBINFr.Visible = false;
            lbbin.Visible = false;
            lbsitetype.Visible = false;
        
            lbwhs.Visible = false;
            txfrom.Enabled = false;
            txto.Enabled = false;
        }
        else if (rbRpt.SelectedValue == "F")
        {
            cbsiteDest.Visible = true;
            cbwhs_cd.Visible = true;
            //cbbin_cd.Visible = true;
            //lbwhs.Visible = true;
            lbbin.Visible = true;
            lbsitetype.Visible = true;
       
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
    private void bindinggrd()
    {
        DateTime dt = DateTime.ParseExact(txto.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        string sdate;
        sdate = dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day.ToString();
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@todate", sdate));
        bll.vBindingGridToSp(ref grd, "sp_rptStockHO_diff", arr);
    }
    protected void txto_TextChanged(object sender, EventArgs e)
    {
        bindinggrd();
    }
}