using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_rptsummary : System.Web.UI.Page
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
                //string rptTYpe = "";
                rptTYpe = Request.QueryString["type"]; 

                if (rptTYpe != "" || rptTYpe != null)
                {
                    bindingType(rptTYpe);
                    
                    waz_dt = Request.Cookies["waz_dt"].Value.ToString();
                    DateTime dtwaz_dt = DateTime.ParseExact(waz_dt, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    waz_dt = dtwaz_dt.ToString("yyyyMM");
                    waz_year = dtwaz_dt.ToString("yyyy");
                    arr.Clear();
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    bll.vBindingComboToSp(ref cbSalesPointCD, "sp_tmst_salespoint_getuser", "salespointcd", "salespoint_desc", arr);
                    cbSalesPointCD.Items.Insert(0, new ListItem("<< ALL Branch >>", "-1"));
                    bll.vBindingComboToSp(ref cbsalespointmth, "sp_tmst_salespoint_getuser", "salespointcd", "salespoint_desc", arr);
                    cbsalespointmth.Items.Insert(0, new ListItem("<< ALL Branch >>", "-1"));
                    //bll.vBindingComboToSp(ref cbSalesPointCD, "sp_tmst_salespoint_getall", "salespointcd", "salespoint_desc");
                    //bll.vBindingComboToSp(ref cbsalespointmth, "sp_tmst_salespoint_getall", "salespointcd", "salespoint_desc");

                    cbSalesPointCD.SelectedValue = Request.Cookies["sp"].Value.ToString();
                    cbsalespointmth.SelectedValue = Request.Cookies["sp"].Value.ToString();

                    bll.vBindingComboToSp(ref cbMonthCD, "sp_tblTRYearMonth_get", "period", "ymtName");
                    bll.vBindingComboToSp(ref cbmonthperiod, "sp_tblTRYearMonth_get", "period", "ymtName");
                    bll.vBindingComboToSp(ref cbperiodmth, "sp_tblTRYear_get", "YearCD", "YearCD");
                    cbMonthCD.SelectedValue = waz_dt;
                    cbmonthperiod.SelectedValue = waz_dt;
                    cbperiodmth.SelectedValue = waz_year;
                    cbMonthCD_SelectedIndexChanged(sender, e);
                    cbsalespointmth_SelectedIndexChanged(sender, e);
                    sho = Request.Cookies["sp"].Value.ToString();
                    if (sho == "0")
                    {
                        cbSalesPointCD.Enabled = true;
                        cbSalesPointCD.CssClass = "form-control";
                        cbsalespointmth.Enabled = true;
                        cbsalespointmth.CssClass = "form-control";
                        //cbSalesPointCD.Items.RemoveAt(0);
                        //cbsalespointmth.Items.RemoveAt(0);
                    }
                    else
                    {
                        //cbSalesPointCD.Enabled = true;
                        //cbSalesPointCD.CssClass = "makeitreadonly ro form-control";
                        //cbsalespointmth.Enabled = true;
                        //cbsalespointmth.CssClass = "makeitreadonly ro form-control";

                        cbSalesPointCD.Enabled = true;
                        cbSalesPointCD.CssClass = "form-control";
                        cbsalespointmth.Enabled = true;
                        cbsalespointmth.CssClass = "form-control";
                    }
                }
                else
                {
                    Response.Redirect("Default_2.aspx");
                }

            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_rptsummary");
                Response.Redirect("fm_ErrorPage.aspx");
            }
        }
    }

    void bindingType(string type)
    {
        if (type == "dayclo")
        {
            lbTitle.Text = "Daily Closing Summary";
            summaryclosing.Visible = true;
            monthlyclosing.Visible = false;
            typesales.Visible = false;
            listSalespoint.Visible = true;
            typeStockIn.Visible = false;
            typeSalesQtySummary.Visible = false;
            typeOutletCustomer.Visible = false;
            typesumreport.Visible = false;
        }
        else if (type == "mthclo")
        {
            lbTitle.Text = "Monthly Closing Summary";
            summaryclosing.Visible = false;
            monthlyclosing.Visible = true;
            typesales.Visible = false;
            listSalespoint.Visible = true;
            typeStockIn.Visible = false;
            typeSalesQtySummary.Visible = false;
            typeOutletCustomer.Visible = false;
            typesumreport.Visible = false;
        }
        else if(type == "stksum")
        {
            lbTitle.Text = "GDN / GRN Summary";
            summaryclosing.Visible = true;
            monthlyclosing.Visible = false;
            typesales.Visible = false;
            listSalespoint.Visible = true;
            typeStockIn.Visible = false;
            typeSalesQtySummary.Visible = false;
            typeOutletCustomer.Visible = false;
            typesumreport.Visible = false;
        }
        else if(type == "slsbyflv")
        {
            lbTitle.Text = "Sales by Flavor Summary";
            summaryclosing.Visible = true;
            monthlyclosing.Visible = false;
            typesales.Visible = true;
            listSalespoint.Visible = true;
            typeStockIn.Visible = false;
            typeSalesQtySummary.Visible = false;
            typeOutletCustomer.Visible = false;
            typesumreport.Visible = false;
        }
        else if (type == "bank")
        {
            lbTitle.Text = "Bank Deposit Summary";
            summaryclosing.Visible = true;
            monthlyclosing.Visible = false;
            typesales.Visible = false;
            listSalespoint.Visible = true;
            typeStockIn.Visible = false;
            typeSalesQtySummary.Visible = false;
            typeOutletCustomer.Visible = false;
            typesumreport.Visible = false;
            ButtonPrintRejected.Visible = true;
        }
        else if (type == "expense")
        {
            lbTitle.Text = "Expense Summary";
            summaryclosing.Visible = true;
            monthlyclosing.Visible = false;
            typesales.Visible = false;
            listSalespoint.Visible = true;
            typeStockIn.Visible = false;
            typeSalesQtySummary.Visible = false;
            typeOutletCustomer.Visible = false;
            typesumreport.Visible = false;
        }
        else if (type == "cashierho")
        {
            lbTitle.Text = "Cashier HO Deposit Summary";
            summaryclosing.Visible = true;
            monthlyclosing.Visible = false;
            typesales.Visible = false;
            listSalespoint.Visible = true;
            typeStockIn.Visible = false;
            typeSalesQtySummary.Visible = false;
            typeOutletCustomer.Visible = false;
            typesumreport.Visible = false;
        }
        else if (type == "stkinsum")
        {
            lbTitle.Text = "PO / DO / GoodReceipt Summary Report";
            summaryclosing.Visible = true;
            monthlyclosing.Visible = false;
            typesales.Visible = false;
            listSalespoint.Visible = false;
            typeStockIn.Visible = true;
            typeSalesQtySummary.Visible = false;
            typeOutletCustomer.Visible = false;
            typesumreport.Visible = false;
        }
        else if (type == "salesdaily")
        {
            lbTitle.Text = "Sales (Qty) Daily Summary";
            summaryclosing.Visible = true;
            monthlyclosing.Visible = false;
            typesales.Visible = false;
            listSalespoint.Visible = true;
            
            typeStockIn.Visible = false;
            typeSalesQtySummary.Visible = true;
            typeOutletCustomer.Visible = false;
            typesumreport.Visible = true;
        }
        else if (type == "salesmandaily")
        {
            lbTitle.Text = "Salesman Daily Activity Summary";
            summaryclosing.Visible = true;
            monthlyclosing.Visible = false;
            typesales.Visible = false;
            listSalespoint.Visible = true;

            typeStockIn.Visible = false;
            typeSalesQtySummary.Visible = false;
            typeOutletCustomer.Visible = false;
            typesumreport.Visible = false;

            txfrom.Visible = true;
            txto.Visible = false;
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
            string type = Request.QueryString["type"];
            DateTime dtfrom = Convert.ToDateTime(DateTime.ParseExact(txfrom.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture));
            DateTime startOfMonth = new DateTime(dtfrom.Year, dtfrom.Month, 1);
            List<cArrayList> arr = new List<cArrayList>();
            //arr.Add(new cArrayList("@SalesPointCD", cbSalesPointCD.SelectedValue));
            //arr.Add(new cArrayList("@period", cbMonthCD.SelectedValue));
            //arr.Add(new cArrayList("@dtFrom", DateTime.ParseExact(txfrom.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToShortDateString()));
            //arr.Add(new cArrayList("@dtTo", DateTime.ParseExact(txto.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToShortDateString()));
            //arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));

            if (type == "dayclo")
            {
                arr.Clear();
                arr.Add(new cArrayList("@SalesPointCD", cbSalesPointCD.SelectedValue));
                arr.Add(new cArrayList("@period", cbMonthCD.SelectedValue));
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                Session["lParamsum_dailyclosing"] = arr;
                Session["lParamit"] = arr;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=sum_dailyclosing');", true);
            }
            if (type == "mthclo")
            {
                arr.Clear();
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                if (cbsalespointmth.SelectedValue.ToString() == "-1")
                {
                    arr.Add(new cArrayList("@period", cbmonthperiod.SelectedValue.ToString()));
                    arr.Add(new cArrayList("@start_dt", DateTime.ParseExact(txstartmth.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToShortDateString()));
                    arr.Add(new cArrayList("@end_dt", DateTime.ParseExact(txendmth.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToShortDateString()));
                    Session["lParamsum_monthlyclosingall"] = arr;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=sum_monthlyclosingall');", true);
                }
                else
                {
                    arr.Add(new cArrayList("@period", cbperiodmth.SelectedValue.ToString()));
                    arr.Add(new cArrayList("@SalesPointCD", cbsalespointmth.SelectedValue));
                    Session["lParamsum_monthlyclosing"] = arr;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=sum_monthlyclosing');", true);
                }
            }
            if (type == "stksum")
            {
                arr.Clear();
                //arr.Add(new cArrayList("@SalesPointCD", cbSalesPointCD.SelectedValue));
                //arr.Add(new cArrayList("@start_dt", DateTime.ParseExact(txfrom.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToShortDateString()));
                //arr.Add(new cArrayList("@end_dt", DateTime.ParseExact(txto.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToShortDateString()));
                //arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                //Session["lParamsum_stksum"] = arr;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                    "openreport('fm_report2.aspx?src=sum_stokin&sp=" + cbSalesPointCD.SelectedValue + "&s=" + txfrom.Text + "&e=" + txto.Text + "');", true);
            }
            if (type == "slsbyflv")
            {
                if (cbtypesales.SelectedValue.ToString() == "salesmanqty")
                {
                    arr.Clear();
                    //arr.Add(new cArrayList("@SalesPointCD", cbSalesPointCD.SelectedValue));
                    //arr.Add(new cArrayList("@start_dt", DateTime.ParseExact(txfrom.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToShortDateString()));
                    //arr.Add(new cArrayList("@end_dt", DateTime.ParseExact(txto.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToShortDateString()));
                    arr.Add(new cArrayList("@period", cbMonthCD.SelectedValue));
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue.ToString()));
                    Session["lParamsum_slsbyflvqty"] = arr;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=sum_salesbyflavorqty');", true);
                }
                if (cbtypesales.SelectedValue.ToString() == "salesmancoll")
                {
                    arr.Clear();
                    //arr.Add(new cArrayList("@SalesPointCD", cbSalesPointCD.SelectedValue));
                    //arr.Add(new cArrayList("@start_dt", DateTime.ParseExact(txfrom.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToShortDateString()));
                    //arr.Add(new cArrayList("@end_dt", DateTime.ParseExact(txto.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToShortDateString()));
                    //arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    arr.Add(new cArrayList("@period", cbMonthCD.SelectedValue));
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue.ToString()));
                    Session["lParamsum_slsbyflvcoll"] = arr;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=sum_salesbyflavorcoll');", true);
                }
                else if (cbtypesales.SelectedValue.ToString() == "customer")
                {
                    arr.Clear();
                    arr.Add(new cArrayList("@SalesPointCD", cbSalesPointCD.SelectedValue));
                    arr.Add(new cArrayList("@start_dt", DateTime.ParseExact(txfrom.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToShortDateString()));
                    arr.Add(new cArrayList("@end_dt", DateTime.ParseExact(txto.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToShortDateString()));
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    Session["lParamsum_custbyflv"] = arr;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=sum_customerbyflavor');", true);
                }
                if (cbtypesales.SelectedValue.ToString() == "targetsalesman")
                {
                    arr.Clear();
                    arr.Add(new cArrayList("@SalesPointCD", cbSalesPointCD.SelectedValue));
                    arr.Add(new cArrayList("@start_dt", DateTime.ParseExact(txfrom.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToShortDateString()));
                    arr.Add(new cArrayList("@end_dt", DateTime.ParseExact(txto.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToShortDateString()));
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    Session["lParamsum_slsbytargetflv"] = arr;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=sum_salesbytargetflavor');", true);
                }
            }
            if (type == "bank")
            {
                DateTime dtStart = DateTime.ParseExact(txfrom.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime dtEnd = DateTime.ParseExact(txto.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                string dateStart = Convert.ToString(dtStart.Year) + "-" + Convert.ToString(dtStart.Month) + "-" + Convert.ToString(dtStart.Day);
                string dateEnd = Convert.ToString(dtEnd.Year) + "-" + Convert.ToString(dtEnd.Month) + "-" + Convert.ToString(dtEnd.Day);

                if (cbSalesPointCD.SelectedValue.ToString() == "-1")
                {

                }

                arr.Clear();
                arr.Add(new cArrayList("@SalesPointCD", cbSalesPointCD.SelectedValue));
                arr.Add(new cArrayList("@start_dt", dateStart));
                arr.Add(new cArrayList("@end_dt", dateEnd));
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                Session["lParamsum_bank"] = arr;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=sum_bank');", true);
            }

            if (type == "cashierho")
            {
                arr.Clear();
                arr.Add(new cArrayList("@SalesPointCD", cbSalesPointCD.SelectedValue));
                arr.Add(new cArrayList("@start_dt", DateTime.ParseExact(txfrom.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToShortDateString()));
                arr.Add(new cArrayList("@end_dt", DateTime.ParseExact(txto.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToShortDateString()));
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                Session["lParamsum_cashierho"] = arr;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=sum_cashierho');", true);
            }
            if (type == "expense")
            {
                arr.Clear();
                arr.Add(new cArrayList("@SalesPointCD", cbSalesPointCD.SelectedValue));
                arr.Add(new cArrayList("@start_dt", DateTime.ParseExact(txfrom.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToShortDateString()));
                arr.Add(new cArrayList("@end_dt", DateTime.ParseExact(txto.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToShortDateString()));
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                Session["lParamsum_expense"] = arr;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=sum_expense');", true);
            }
            if (type == "stkinsum")
            {
                if (cbtypestockin.SelectedValue.ToString() == "po")
                {
                    arr.Clear();
                    //arr.Add(new cArrayList("@SalesPointCD", cbSalesPointCD.SelectedValue));
                    arr.Add(new cArrayList("@start_dt", DateTime.ParseExact(txfrom.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToShortDateString()));
                    arr.Add(new cArrayList("@end_dt", DateTime.ParseExact(txto.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToShortDateString()));
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    Session["lParamsum_po_stkin"] = arr;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=sum_po_stkin');", true);
                }
                else if (cbtypestockin.SelectedValue.ToString() == "do")
                {
                    arr.Clear();
                    //arr.Add(new cArrayList("@SalesPointCD", cbSalesPointCD.SelectedValue));
                    arr.Add(new cArrayList("@start_dt", DateTime.ParseExact(txfrom.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToShortDateString()));
                    arr.Add(new cArrayList("@end_dt", DateTime.ParseExact(txto.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToShortDateString()));
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    Session["lParamsum_do_stkin"] = arr;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=sum_do_stkin');", true);
                }
                if (cbtypestockin.SelectedValue.ToString() == "gr")
                {
                    arr.Clear();
                    //arr.Add(new cArrayList("@SalesPointCD", cbSalesPointCD.SelectedValue));
                    arr.Add(new cArrayList("@start_dt", DateTime.ParseExact(txfrom.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToShortDateString()));
                    arr.Add(new cArrayList("@end_dt", DateTime.ParseExact(txto.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToShortDateString()));
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    Session["lParamsum_gr_stkin"] = arr;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=sum_gr_stkin');", true);
                }
            }
            if (type == "salesdaily")
            {
                if (cbsumtypesalesqty.SelectedValue.ToString() == "detailbranch")
                {
                    arr.Clear();
                    arr.Add(new cArrayList("@period", cbMonthCD.SelectedValue));
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue.ToString()));
                    Session["lParamsum_salesdailybybranchdetail"] = arr;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=sum_salesdailybybranchdetail');", true);
                }
                else if (cbsumtypesalesqty.SelectedValue.ToString() == "sumbranch")
                {
                    arr.Clear();
                    arr.Add(new cArrayList("@period", cbMonthCD.SelectedValue));
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue.ToString()));
                    Session["lParamsum_salesdailybybranch"] = arr;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=sum_salesdailybybranch');", true);
                }
                else if (cbsumtypesalesqty.SelectedValue.ToString() == "sumcategory")
                {
                    arr.Clear();
                    arr.Add(new cArrayList("@period", cbMonthCD.SelectedValue));
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    arr.Add(new cArrayList("@category", cbotlcd.SelectedValue.ToString()));
                    arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue.ToString()));
                    Session["lParamsum_salesdailybyotlcd"] = arr;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=sum_salesdailybyotlcd');", true);
                }
            }
            if (type == "salesmandaily")
            {
                arr.Clear();
                arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue.ToString()));
                arr.Add(new cArrayList("@start_dt", DateTime.ParseExact(txfrom.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToShortDateString()));
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                Session["lParamsum_salesmandaily"] = arr;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=sum_salesmandaily');", true);

            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_rptsummary");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }



    protected void btprint_Rejected_Click(object sender, EventArgs e)
    {
        try
        {
            string type = Request.QueryString["type"];
            DateTime dtfrom = Convert.ToDateTime(DateTime.ParseExact(txfrom.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture));
            DateTime startOfMonth = new DateTime(dtfrom.Year, dtfrom.Month, 1);
            List<cArrayList> arr = new List<cArrayList>();
             
    
            if (type == "bank")
            {
                DateTime dtStart = DateTime.ParseExact(txfrom.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime dtEnd = DateTime.ParseExact(txto.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                string dateStart = Convert.ToString(dtStart.Year) + "-" + Convert.ToString(dtStart.Month) + "-" + Convert.ToString(dtStart.Day);
                string dateEnd = Convert.ToString(dtEnd.Year) + "-" + Convert.ToString(dtEnd.Month) + "-" + Convert.ToString(dtEnd.Day);

                if (cbSalesPointCD.SelectedValue.ToString() == "-1")
                {

                }

                arr.Clear();
                arr.Add(new cArrayList("@SalesPointCD", cbSalesPointCD.SelectedValue));
                arr.Add(new cArrayList("@start_dt", dateStart));
                arr.Add(new cArrayList("@end_dt", dateEnd));
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                Session["lParamsum_bank"] = arr;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=sum_bank_rejected');", true);
            }

         
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_rptsummary");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void cbsumtypesalesqty_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbsumtypesalesqty.SelectedValue.ToString() == "sumbranch" || cbsumtypesalesqty.SelectedValue.ToString() == "detailbranch")
        {
            typeOutletCustomer.Visible = false;
            typesumreport.Visible = false;
        }
        else
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Clear();
            arr.Add(new cArrayList("@fld_nm", "otlcd"));
            arr.Add(new cArrayList("@hiddendata", "0"));
            bll.vBindingComboToSp(ref cbotlcd, "sp_tfield_value_withselect_get", "fld_valu", "FLD_DESC", arr);
            typeOutletCustomer.Visible = true;
            typesumreport.Visible = true;
            cbotlcd.Items.RemoveAt(0);
            cbotlcd.Items.Insert(0, new ListItem("<< ALL Category >>", "-1"));
        }
    }

    protected void rbsumrpt_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void cbsalespointmth_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbsalespointmth.SelectedValue.ToString() == "-1")
        {
            cbmonthperiod.Visible = true;
            cbperiodmth.Visible = false;
            periodDate.Visible = true;
            cbmonthperiod_SelectedIndexChanged(sender, e);
        }
        else
        {
            cbmonthperiod.Visible = false;
            cbperiodmth.Visible = true;
            periodDate.Visible = false;
        }
    }

    protected void cbmonthperiod_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strFrom, strTo;
        strFrom = bll.vLookUp("select convert(varchar, ymtStart, 103)  from tblTRYearMonth where period='" + cbmonthperiod.SelectedValue + "'");
        strTo = bll.vLookUp("select  convert(varchar,ymtEnd, 103) from tblTRYearMonth where period='" + cbmonthperiod.SelectedValue + "'");
        //DateTime dtTo =strTo; //DateTime.ParseExact(strTo, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        txstartmth.Text = strFrom;
        txendmth.Text = strTo;
    }

    protected void txstartmth_TextChanged(object sender, EventArgs e)
    {
        string strFrom, strTo;
        strFrom = bll.vLookUp("select convert(varchar, ymtStart, 103)  from tblTRYearMonth where period='" + cbmonthperiod.SelectedValue + "'");
        strTo = bll.vLookUp("select  convert(varchar,ymtEnd, 103) from tblTRYearMonth where period='" + cbmonthperiod.SelectedValue + "'");
        DateTime dtstrfrom = DateTime.ParseExact(strFrom, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime dtstrto = DateTime.ParseExact(strTo, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime dtfrom = DateTime.ParseExact(txstartmth.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime dtto = DateTime.ParseExact(txendmth.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        if (dtfrom < dtstrfrom || dtfrom > dtstrto)
        {
            txstartmth.Text = strFrom;
        }
    }

    protected void txendmth_TextChanged(object sender, EventArgs e)
    {
        string strFrom, strTo;
        strFrom = bll.vLookUp("select convert(varchar, ymtStart, 103)  from tblTRYearMonth where period='" + cbmonthperiod.SelectedValue + "'");
        strTo = bll.vLookUp("select  convert(varchar,ymtEnd, 103) from tblTRYearMonth where period='" + cbmonthperiod.SelectedValue + "'");
        DateTime dtstrfrom = DateTime.ParseExact(strFrom, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime dtstrto = DateTime.ParseExact(strTo, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime dtfrom = DateTime.ParseExact(txstartmth.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime dtto = DateTime.ParseExact(txendmth.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        if (dtto < dtstrfrom || dtto > dtstrto)
        {
            txendmth.Text = strTo;
        }
    }
}