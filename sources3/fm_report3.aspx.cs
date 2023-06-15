using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
public partial class fm_report3 : System.Web.UI.Page
{
    cbll bll = new cbll();
    ReportDocument doc = new ReportDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {

        }
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "window.close();", true);
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        List<string> lformula = new List<string>();
        List<cArrayList> lparam = new List<cArrayList>();
        string sSrc = Request.QueryString["src"];
        string sImage = bll.sGetControlParameter("image_path");
        List<string> lapproval;
        try
        {
            switch (sSrc)
            {
                case "customerCashSalesOverDue":
                    string date = Convert.ToString(DateTime.Now.Year + "" + DateTime.Now.Month + "" + DateTime.Now.Day);
                    string customerCashSalesOverDueRptFileName = date + "_" + Request.Cookies["sp"].Value.ToString() + "_CustCashSalesOverDue" + ".xls";
                    vShowReportToExcell(ref crv, "rp_CustomerCashSalesOverDue.rpt", sImage + customerCashSalesOverDueRptFileName);
                    string sMessage1 = "Dear All,<BR>";
                    sMessage1 += "This is the Customer Cash Sales Over Due Report for Branch " + bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + "!<BR>";
                    sMessage1 += "Note. This Email was sent to Alert You, Due to The System Allowing Cash Selling for Customer who has a Due, Ask the Salesman for the Reason Behind it.<BR>";
                    sMessage1 += "Wazaran Admin";
                    lapproval = bll.lGetApproval("CashOverDue", 1);
                    string appr = string.Empty;
                    for (int i = 1; i <= lapproval.Count; i += 2)
                    {
                        if (i < lapproval.Count - 2)
                        { appr += lapproval[i] + ";"; }
                        else
                        { appr += lapproval[i]; }
                    }
                    List<cArrayList> array = new List<cArrayList>();
                    array.Add(new cArrayList("@token", null));
                    array.Add(new cArrayList("@doc_typ", "CashOverDue"));
                    array.Add(new cArrayList("@to", appr));
                    array.Add(new cArrayList("@doc_no", null));
                    array.Add(new cArrayList("@emailsubject", "Daily Closing Customer Cash Sales Over Due Report for Branch " + bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd=((select parm_Valu from tcontrol_parameter where parm_nm='salespoint'))") + "!"));
                    array.Add(new cArrayList("@msg", sMessage1));
                    array.Add(new cArrayList("@file_attachment", customerCashSalesOverDueRptFileName));
                    bll.vInsertEmailOutbox(array);
                    break;
                case "carMaintenance":
                    string period = bll.sGetControlParameter("period");
                    decimal month = Convert.ToInt32(period.Substring(4, 2));
                    int year = Convert.ToInt32(period.Substring(0, 4));

                    if (month == 1)
                    {
                        month = 12;
                        year = year - 1;
                    }
                    else
                    {
                        month = month - 1;
                    }
                    if (month < 10)
                    {
                        period = year.ToString() + "0" + month.ToString();
                    }
                    else
                    {
                        period = year.ToString() + month.ToString();
                    }

                    string carMaintenanceFileName = period + "_" + bll.vLookUp("select salespoint_sn from tmst_salespoint where salespointcd=((select parm_Valu from tcontrol_parameter where parm_nm='salespoint'))") + "_car" + ".xls";
                    vShowReportToExcell(ref crv, "rp_CarMaintenance.rpt", sImage + carMaintenanceFileName);
                    string sMessage = "Dear Car Maintenance Team,<BR>";
                    sMessage += "This is the Monthly Closing Car Maintenance Report for Branch " + bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + "!<BR>";
                    sMessage += "Note. Select the whole table cells to filter.<BR>";
                    sMessage += "Wazaran Admin";
                    List<cArrayList> arry1 = new List<cArrayList>();
                    arry1.Add(new cArrayList("@token", null));
                    arry1.Add(new cArrayList("@doc_typ", "Car Maintenance"));
                    arry1.Add(new cArrayList("@to", bll.vLookUp("select qry_data from tmap_query where qry_cd='CarMaintenanceEmails'")));
                    arry1.Add(new cArrayList("@doc_no", null));
                    arry1.Add(new cArrayList("@emailsubject", "Monthly Closing Car Maintenance Report for Branch " + bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd=((select parm_Valu from tcontrol_parameter where parm_nm='salespoint'))") + "!"));
                    arry1.Add(new cArrayList("@msg", sMessage));
                    arry1.Add(new cArrayList("@file_attachment", carMaintenanceFileName));
                    bll.vInsertEmailOutbox(arry1);
                    break;
                case "notdue":
                    //string sysdt2 = bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='wazaran_dt'");
                    string sysdt2 = Request.Cookies["sp"].Value.ToString();
                    DateTime dtnotdue1 = DateTime.ParseExact(sysdt2, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    string notduesFileName = Request.Cookies["sp"].Value.ToString() + " - " + dtnotdue1.Year.ToString() + "-" + dtnotdue1.Month.ToString("00") + "-" + dtnotdue1.Day.ToString("00") + "-Due Salesman" + ".xls";
                    string notduescustomerFileName = Request.Cookies["sp"].Value.ToString() + " - " + dtnotdue1.Year.ToString() + "-" + dtnotdue1.Month.ToString("00") + "-" + dtnotdue1.Day.ToString("00") + "-Due customer" + ".xls";
                    vShowReportToExcell(ref crv, "rp_summ_outletperformanceDUE.rpt", sImage + notduesFileName);
                    vShowReportToExcell(ref crv, "rp_summ_outletperformanceDUECUSTOMER.rpt", sImage + notduescustomerFileName);
                    lapproval = bll.lGetApproval("notifydue", 1);
                    List<cArrayList> arr1 = new List<cArrayList>();
                    arr1.Add(new cArrayList("@token", null));
                    arr1.Add(new cArrayList("@doc_typ", "notdue"));
                    arr1.Add(new cArrayList("@to", lapproval[1]));
                    arr1.Add(new cArrayList("@doc_no", notduesFileName));
                    arr1.Add(new cArrayList("@emailsubject", "Salesman Due Report By Wazaran " + bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'") + "!"));
                    arr1.Add(new cArrayList("@msg", "Salesman Due Report " + bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'")));
                    arr1.Add(new cArrayList("@file_attachment", notduesFileName));
                    bll.vInsertEmailOutbox(arr1);
                    arr1.Clear();
                    arr1.Add(new cArrayList("@token", null));
                    arr1.Add(new cArrayList("@doc_typ", "notdue"));
                    arr1.Add(new cArrayList("@to", lapproval[1]));
                    arr1.Add(new cArrayList("@doc_no", notduescustomerFileName));
                    arr1.Add(new cArrayList("@emailsubject", "Customer Due Report By Wazaran " + bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'") + "!"));
                    arr1.Add(new cArrayList("@msg", "Customer Due Report " + bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'")));
                    arr1.Add(new cArrayList("@file_attachment", notduescustomerFileName));
                    bll.vInsertEmailOutbox(arr1);
                    //bll.vSendMail(, "NE & BS Report By Wazaran!", "NE & BS  Report " + bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'"), notduesFileName);
                    break;
                case "notbebs":

                    string sysdt1 = bll.vLookUp("select dbo.fn_getsystemdate('"+ Request.Cookies["sp"].Value.ToString() + "')");
                    DateTime dtnotbebs1 = DateTime.ParseExact(sysdt1, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    string notbebsFileName = Request.Cookies["sp"].Value.ToString() + " - " + dtnotbebs1.Year.ToString() + "-" + dtnotbebs1.Month.ToString("00") + "-" + dtnotbebs1.Day.ToString("00") + "-NE BS RETURN" + ".xls";
                    //arr.Add(new cArrayList("@startdate", dtpayp1.Year.ToString() + "-" + dtpayp1.Month.ToString("00") + "-" + dtpayp1.Day.ToString("00")));
                    //arr.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                    //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=outletsperformanceacc&startdate=" + Request.Cookies["waz_dt"].Value.ToString() + "&salp=" + cbsalespoint.SelectedValue.ToString() + "');", true);
                    vShowReportToExcell(ref crv, "rp_weeklyreturn.rpt", sImage + notbebsFileName);
                    lapproval = bll.lGetApproval("notifynebs", 1);
                    //bll.vSendMail(lapproval[1], "Aging Report By Wazaran!", "Aging Report By Wazaran for Salespoint:" + bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'"), sFileName);
                    bll.vSendMail("o.alamoudi@sbtcgroup.com", "NE & BS Report By Wazaran!", "NE & BS  Report " + bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'"), notbebsFileName);
                    bll.vSendMail(lapproval[1], "NE & BS Report By Wazaran!", "NE & BS  Report " + bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'"), notbebsFileName);
                    break;
                //case "jaretdpo":
                //    string sysdtjrddp = bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='wazaran_dt'");
                //    DateTime dtjrddp = DateTime.ParseExact(sysdtjrddp, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                //    string jrtdpFileName = Request.Cookies["sp"].Value.ToString() + "-" + dtjrddp.Year + "-" + dtjrddp.Month + "-" + dtjrddp.Day + "-jared depo" + ".xls";
                //    List<cArrayList> arr3 = new List<cArrayList>();
                //    arr3.Add(new cArrayList("@SalesPointCD", Request.Cookies["sp"].Value.ToString()));
                //    arr3.Add(new cArrayList("@MonthCD",  bll.vLookUp("SELECT CONVERT(VARCHAR(6),CONVERT(date,DATEADD(mm, DATEDIFF(mm, 0, CONVERT(date,dateadd(d,-(day(dbo.fn_getsystemdate())),dbo.fn_getsystemdate()),106)), 0)), 112)")));
                //    arr3.Add(new cArrayList("@siteDest", "DEPO"));
                //    arr3.Add(new cArrayList("@whs_cd", bll.vLookUp("select whs_cd from tmst_warehouse where level_no=1 and salespointcd='"+Request.Cookies["sp"].Value.ToString()+"'")));
                //    arr3.Add(new cArrayList("@dtFrom", bll.vLookUp("SELECT CONVERT(date,DATEADD(mm, DATEDIFF(mm, 0, CONVERT(date,dateadd(d,-(day(dbo.fn_getsystemdate())),dbo.fn_getsystemdate()),106)), 0))")));
                //    arr3.Add(new cArrayList("@dtTo",bll.vLookUp("select CONVERT(date,dateadd(d,-(day(dbo.fn_getsystemdate())),dbo.fn_getsystemdate()),106)")));
                //    arr3.Add(new cArrayList("@dtStart", bll.vLookUp("SELECT CONVERT(date,DATEADD(mm, DATEDIFF(mm, 0, CONVERT(date,dateadd(d,-(day(dbo.fn_getsystemdate())),dbo.fn_getsystemdate()),106)), 0))")));
                //    arr3.Add(new cArrayList("@item_cdFr", "-1"));
                //    arr3.Add(new cArrayList("@item_cdTo", "-1"));
                //    arr3.Add(new cArrayList("@prod_cdFr", "-1"));
                //    arr3.Add(new cArrayList("@prod_cdTo", "-1"));
                //    arr3.Add(new cArrayList("@Bin_cd_fr", "-1"));
                //    vShowReportToExcell(ref crv, "rp_jareddepo.rpt", sImage + jrtdpFileName, arr3);

                //    string sjrtdpoSubject = "#Jared DEPO REPORT " + dtjrddp.Year + "-" + dtjrddp.Month + "-" + dtjrddp.Day + " Has Been Created";
                //    arr.Clear();
                //    arr.Add(new cArrayList("@token", null));
                //    arr.Add(new cArrayList("@doc_typ", "JRT"));
                //    arr.Add(new cArrayList("@doc_no", Request.Cookies["sp"].Value.ToString() + "-" + dtjrddp.Year + "-" + dtjrddp.Month + "-" + dtjrddp.Day));
                //    arr.Add(new cArrayList("@emailsubject", sjrtdpoSubject));
                //    arr.Add(new cArrayList("@msg", "THIS REPORT SENT BY SYSTEM NO NEED TO REPLY THIS EMAIL"));
                //    arr.Add(new cArrayList("@file_attachment", jrtdpFileName));
                //    List<string> lapprovaljrtdp2 = bll.lGetApprovalall("jared");
                //    string semailjrtdp = "0";
                //    int k = 0;
                //    foreach (var email in lapprovaljrtdp2.Skip(1))
                //    {
                //        k = k + 1;
                //        if (k % 2 != 0)
                //        {
                //            if (semailjrtdp == "0")
                //                semailjrtdp = email;
                //            else
                //                semailjrtdp = semailjrtdp + ';' + email;
                //        }
                //    }
                //    arr.Add(new cArrayList("@to", semailjrtdp));                    
                //    bll.vInsertEmailOutbox(arr);
                //    break;
                case "jaret":
                    //string sysdtjrd = bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='wazaran_dt'");
                    string sysdtjrd = Request.Cookies["waz_dt"].Value.ToString();
                    DateTime dtjrd = DateTime.ParseExact(sysdtjrd, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    string jrtFileName = Request.Cookies["sp"].Value.ToString() + "-" + dtjrd.Year + "-" + dtjrd.Month + "-" + dtjrd.Day + "-jared" + ".xls";
                    List<cArrayList> arr2 = new List<cArrayList>();
                    arr2.Add(new cArrayList("@SalesPointCD", Request.Cookies["sp"].Value.ToString()));
                    arr2.Add(new cArrayList("@MonthCD", bll.vLookUp("SELECT CONVERT(VARCHAR(6),CONVERT(date,DATEADD(mm, DATEDIFF(mm, 0, CONVERT(date,dateadd(d,-(day(dbo.fn_getsystemdate('"+ Request.Cookies["sp"].Value.ToString() + "'))),dbo.fn_getsystemdate('"+ Request.Cookies["sp"].Value.ToString() + "')),106)), 0)), 112)")));
                    arr2.Add(new cArrayList("@siteDest", null));
                    arr2.Add(new cArrayList("@whs_cd", null));
                    arr2.Add(new cArrayList("@dtFrom", bll.vLookUp("SELECT CONVERT(date,DATEADD(mm, DATEDIFF(mm, 0, CONVERT(date,dateadd(d,-(day(dbo.fn_getsystemdate('"+ Request.Cookies["sp"].Value.ToString() + "'))),dbo.fn_getsystemdate('"+ Request.Cookies["sp"].Value.ToString() + "')),106)), 0))")));
                    arr2.Add(new cArrayList("@dtTo", bll.vLookUp("select CONVERT(date,dateadd(d,-(day(dbo.fn_getsystemdate('"+ Request.Cookies["sp"].Value.ToString() + "'))),dbo.fn_getsystemdate('"+ Request.Cookies["sp"].Value.ToString() + "')),106)")));
                    arr2.Add(new cArrayList("@dtStart", bll.vLookUp("SELECT CONVERT(date,DATEADD(mm, DATEDIFF(mm, 0, CONVERT(date,dateadd(d,-(day(dbo.fn_getsystemdate('"+ Request.Cookies["sp"].Value.ToString() + "'))),dbo.fn_getsystemdate('"+ Request.Cookies["sp"].Value.ToString() + "')),106)), 0))")));
                    arr2.Add(new cArrayList("@item_cdFr", "-1"));
                    arr2.Add(new cArrayList("@item_cdTo", "-1"));
                    arr2.Add(new cArrayList("@prod_cdFr", "-1"));
                    arr2.Add(new cArrayList("@prod_cdTo", "-1"));
                    arr2.Add(new cArrayList("@Bin_cd_fr", "-1"));
                    vShowReportToExcell(ref crv, "rp_jared.rpt", sImage + jrtFileName, arr2);

                    string sjrtSubject = "#Jared REPORT " + dtjrd.Year + "-" + dtjrd.Month + "-" + dtjrd.Day + " Has Been Created";
                    arr.Clear();
                    arr.Add(new cArrayList("@token", null));
                    arr.Add(new cArrayList("@doc_typ", "JRT"));
                    arr.Add(new cArrayList("@doc_no", Request.Cookies["sp"].Value.ToString() + "-" + dtjrd.Year + "-" + dtjrd.Month + "-" + dtjrd.Day));
                    arr.Add(new cArrayList("@emailsubject", sjrtSubject));
                    arr.Add(new cArrayList("@msg", "THIS REPORT SENT BY SYSTEM NO NEED TO REPLY THIS EMAIL"));
                    arr.Add(new cArrayList("@file_attachment", jrtFileName));
                    List<string> lapprovaljrt2 = bll.lGetApprovalall("jared");
                    string semailjrt = "0";
                    int h = 0;
                    foreach (var email in lapprovaljrt2.Skip(1))
                    {
                        h = h + 1;
                        if (h % 2 != 0)
                        {
                            if (semailjrt == "0")
                                semailjrt = email;
                            else
                                semailjrt = semailjrt + ';' + email;
                        }
                    }
                    arr.Add(new cArrayList("@to", semailjrt));
                    bll.vInsertEmailOutbox(arr);
                    break;
                case "weeklycashier":
                    string sysdtcsh = bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='wazaran_dt'");
                    DateTime dtcsh = DateTime.ParseExact(sysdtcsh, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    string wkcshFileName = Request.Cookies["sp"].Value.ToString() + "-" + dtcsh.Year + "-" + dtcsh.Month + "-" + dtcsh.Day + "-Weekly Cashier" + ".xls";
                    vShowReportToExcell(ref crv, "RP_WEEKLYCASHIER.rpt", sImage + wkcshFileName);
                    string sSubject = "#WEEKLY CAHIER REPORT " + dtcsh.Year + "-" + dtcsh.Month + "-" + dtcsh.Day + " Has Been Created";
                    arr.Clear();
                    arr.Add(new cArrayList("@token", null));
                    arr.Add(new cArrayList("@doc_typ", "CASHIER WEEKLY"));

                    arr.Add(new cArrayList("@doc_no", Request.Cookies["sp"].Value.ToString() + "-" + dtcsh.Year + "-" + dtcsh.Month + "-" + dtcsh.Day));
                    arr.Add(new cArrayList("@emailsubject", sSubject));
                    arr.Add(new cArrayList("@msg", "THIS REPORT SENT BY SYSTEM NO NEED TO REPLY THIS EMAIL"));
                    arr.Add(new cArrayList("@file_attachment", wkcshFileName));
                    List<string> lapproval2 = bll.lGetApprovalall("weeklycashier");
                    string semail = "0";
                    int j = 0;
                    foreach (var email in lapproval2.Skip(1))
                    {
                        j = j + 1;
                        if (j % 2 != 0)
                        {
                            if (semail == "0")
                                semail = email;
                            else
                                semail = semail + ';' + email;
                        }
                    }
                    arr.Add(new cArrayList("@to", semail));
                    bll.vInsertEmailOutbox(arr);
                    break;
                case "hopayment":
                    //lparam.Add(new cArrayList("p_date", System.DateTime.ParseExact( Request.Cookies["waz_dt"].Value,"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                    //lparam.Add(new cArrayList("p_salespoint", Request.Cookies["sp"].Value.ToString()));
                    Random rnd = null;

                    string n = Convert.ToDateTime(bll.vLookUp("select dbo.fn_getsystemdate()")).ToString("ddmmyyyy");
                    string sReportName = "HOP" + n + ".pdf";
                    vShowReportToPDF(ref crv, "rp_hopayment.rpt", sReportName);
                    List<string> lHop = bll.lGetApproval("hopayment", 1);
                    for (int i = 0; i < lHop.Count; i++)
                    {
                        bll.vSendMail(lHop[i], @"#Cheque/Bank Transfer/Internal Trf/Cash Deposit from Branch", "Information detail as attached.</br></br>Best Regards,<br/>Wazaran Admin", sReportName);
                    }
                    break;
                case "retprodspv":
                    // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=rtn&returno="+txreturno.Text+"');", true);
                    lformula.Add("{tsalesreturn.retur_no} = '" + Request.QueryString["returno"] + "'");
                    //lparam.Add(new cArrayList("?p_user", Request.Cookies["usr_id"].Value.ToString()));
                    List<string> lapp = bll.lAppProdSpv(Request.QueryString["returno"]);
                    if (lapp.Count > 0)
                    {
                        lapp.Add("irwan.agusyono@gmail.com");
                        string sFilertn = Request.QueryString["returno"] + ".pdf";
                        vShowReportToPDF(ref crv, "rp_salesreturn2.rpt", lformula, sFilertn);
                        for (int i = 0; i < lapp.Count; i++)
                        {

                            bll.vSendMail(lapp[i], "#Return from customer and has related with your product", "Information detail as attached.</br></br>Best Regards,<br/>Wazaran Admin", sFilertn);
                        }
                    }
                    break;
                case "inv5days":
                    System.Data.SqlClient.SqlDataReader rsx = null;
                    string sChecking = (bll.vLookUp("select dbo.fn_checkinv5days('"+ Request.Cookies["sp"].Value.ToString() + "')")).ToString();
                    if (sChecking != "ok")
                    {
                        arr.Clear();
                        arr.Add(new cArrayList("@doc_typ", "inv5"));
                        string sFilex = sSrc + System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("ddmmyyyy") + ".pdf";
                        vShowReportToPDF(ref crv, "rp_notifyinv5days.rpt", sFilex);
                        bll.vGetApprovalPattern(arr, ref rsx);
                        while (rsx.Read())
                        {
                            bll.vSendMail(rsx["email"].ToString(), "Invoice More Than 5 days not yet back!", "Please see attachment for your review", sFilex);
                        }
                        rsx.Close();
                    }
                    break;
                case "inv120days":
                    System.Data.SqlClient.SqlDataReader rs = null;
                    arr.Clear();
                    arr.Add(new cArrayList("@doc_typ", "inv120"));
                    string sFile = sSrc + System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("ddmmyyyy") + ".pdf";
                    vShowReportToPDF(ref crv, "rp_inv120day.rpt", sFile);

                    bll.vGetApprovalPattern(arr, ref rs);
                    while (rs.Read())
                    {
                        bll.vSendMail(rs["email"].ToString(), "Invoice More Than 120 days", "Please see attachment for your review", sFile);
                    }
                    rs.Close();
                    break;
                case "mcl":
                    //string sysdt = bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='wazaran_dt'");
                    string sysdt = Request.Cookies["waz_dt"].Value.ToString();
                    DateTime dtpayp1 = DateTime.ParseExact(sysdt, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    string aggFileName = Request.Cookies["sp"].Value.ToString() + " - " + dtpayp1.Year.ToString() + "-" + dtpayp1.Month.ToString("00") + "-" + dtpayp1.Day.ToString("00") + "-Agging" + ".xls";
                    arr.Add(new cArrayList("@startdate", dtpayp1.Year.ToString() + "-" + dtpayp1.Month.ToString("00") + "-" + dtpayp1.Day.ToString("00")));
                    arr.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=outletsperformanceacc&startdate=" + Request.Cookies["waz_dt"].Value.ToString() + "&salp=" + cbsalespoint.SelectedValue.ToString() + "');", true);
                    vShowReportToExcell(ref crv, "rp_outletsperformanceacc.rpt", arr, sImage + aggFileName);
                    lapproval = bll.lGetApproval("notifyaccbalance", 1);
                    //bll.vSendMail(lapproval[1], "Aging Report By Wazaran!", "Aging Report By Wazaran for Salespoint:" + bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'"), sFileName);
                    //bll.vSendMail("o.alamoudi@sbtcgroup.com", "Aging Report By Wazaran!", "Aging Report " + bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'"), aggFileName);
                    //bll.vSendMail(lapproval[1], "Aging Report By Wazaran!", "Aging Report " + bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'"), aggFileName);
                    arr.Clear();
                    arr.Add(new cArrayList("@token", null));
                    arr.Add(new cArrayList("@doc_typ", "MCL"));
                    arr.Add(new cArrayList("@doc_no", Request.Cookies["sp"].Value.ToString() + "-" + dtpayp1.Year + "-" + dtpayp1.Month + "-" + dtpayp1.Day));
                    arr.Add(new cArrayList("@emailsubject", "Aging Report By Wazaran!"));
                    arr.Add(new cArrayList("@msg", "THIS REPORT SENT BY SYSTEM NO NEED TO REPLY THIS EMAIL"));
                    arr.Add(new cArrayList("@file_attachment", aggFileName));
                    List<string> lapprovalmcl2 = bll.lGetApprovalall("notifyaccbalance");
                    string semailmcl = "0";
                    int hmcl = 0;
                    foreach (var email in lapprovalmcl2.Skip(1))
                    {
                        hmcl = hmcl + 1;
                        if (hmcl % 2 != 0)
                        {
                            if (semailmcl == "0")
                                semailmcl = email;
                            else
                                semailmcl = semailmcl + ';' + email;
                        }
                    }
                    arr.Add(new cArrayList("@to", semailmcl));
                    bll.vInsertEmailOutbox(arr);
                    break;
                case "bs":
                    if (bll.vLookUp("select dbo.fn_checkbadstock('"+ Request.Cookies["sp"].Value.ToString() + "')") != "ok")
                    {
                        string sFileName = "BS" + Request.Cookies["sp"].Value.ToString() + System.DateTime.Now.ToString("hhmss") + ".xls";
                        vShowReportToExcell(ref crv, "rp_badstock.rpt", sImage + sFileName);
                        lapproval = bll.lGetApproval("notifybadstock", 1);
                        bll.vSendMail(lapproval[1], "Bad Stock Notification!", "Please Check Bad Stock for Salespoint:" + bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'"), sFileName);
                        bll.vSendMail("irwan.agusyono@gmail.com", "Bad Stock Notification!", "Please Check Bad Stock for Salespoint:" + bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'"), sFileName);
                    }
                    break;
                case "cb":
                    string sPeriod = Request.QueryString["prd"];
                    string pdfname = sSrc + sPeriod + ".pdf";
                    lformula.Add("{tmonthend_custbalance.period} = '" + sPeriod + "'");
                    vShowReportToPDF(ref crv, "rp_custbalance.rpt", lformula, sImage + pdfname);
                    bll.vSendMail("irwan.agusyono@gmail.com", "Testing", "Ini lho statement", pdfname);
                    break;
                case "loadingnotification":
                    lapproval = bll.lGetApproval("LoadingNotification", 1);
                    string pdfloadingname = sSrc + "loadingnotification" + ".pdf";
                    vShowReportToPDF(ref crv, "rp_DeliveryNotification.rpt", sImage + pdfloadingname);
                    bll.vSendMail(lapproval[1], "LoadingNotification", "Loading Notification", pdfloadingname);
                    break;
                case "py":
                    lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                    lformula.Add("{tmst_payment.payment_no} = '" + Request.QueryString["noy"] + "'");
                    lformula.Add("{tmst_payment.salespointcd} = '" + Request.Cookies["sp"].Value.ToString() + "'");
                    vShowreportDirectToPrinter(ref crv, "rp_paymentreceipt2.rpt", lformula, lparam);
                    break;
                case "inv":
                    string sPrintCount = bll.vLookUp("select qty from tprint_control where print_cd='CANVINV' and doc_no='" + Request.QueryString["no"] + "'");
                    lformula.Add("{trpt_dosalesinvoice.inv_no} = '" + Request.QueryString["no"] + "' and {trpt_dosalesinvoice.usr_id} = '" + Request.Cookies["usr_id"].Value.ToString() + "'");
                    lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                    if (sPrintCount == "")
                    {
                        lparam.Add(new cArrayList("p_print", ""));
                    }
                    else { lparam.Add(new cArrayList("p_print", "RE-PRINTED No. " + sPrintCount)); }

                    //lparam.Add(new cArrayList("p_rps", bll.vLookUp("select dbo.fn_getrps('" + Request.QueryString["no"] + "')")));
                    lparam.Add(new cArrayList("p_rps", bll.vLookUp("select dbo.fn_getrps('" + Request.QueryString["no"] + "','" + Request.Cookies["sp"].Value.ToString() + "')")));
                    vShowreportDirectToPrinter(ref crv, "rp_dosalesinvoice3.rpt", lformula, lparam);
                    break;
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_report3");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    public void vShowReportToExcell(ref CrystalReportViewer crv, string sReportName, string XlsName)
    {
        try
        {
            ConnectionInfo cninfo = new ConnectionInfo();
            ParameterFields pfs = new ParameterFields();
            ParameterField pf = null;
            ParameterDiscreteValue pfd = new ParameterDiscreteValue();

            doc.Load(HttpContext.Current.Server.MapPath("/" + sReportName));

            cninfo.ServerName = bll.sGetControlParameter("cr_server");  // ".\\sqlexpress";
            cninfo.DatabaseName = bll.sGetControlParameter("cr_db");// "MUS";
            cninfo.UserID = bll.sGetControlParameter("cr_uid");// "sa";
            cninfo.Password = bll.sGetControlParameter("cr_pwd"); // "preketek";

            Tables crTables = doc.Database.Tables;
            foreach (Table crTable in crTables)
            {
                TableLogOnInfo crtablelogoninfo = crTable.LogOnInfo;
                crtablelogoninfo.ConnectionInfo = cninfo;
                crTable.ApplyLogOnInfo(crtablelogoninfo);
            }

            doc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.Excel, XlsName);
            doc.Close();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "self.close();", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_report3");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    public void vShowReportToExcell(ref CrystalReportViewer crv, string sReportName, List<string> lFormula, string XlsName)
    {
        try
        {
            ConnectionInfo cninfo = new ConnectionInfo();
            ParameterFields pfs = new ParameterFields();
            ParameterField pf = null;
            ParameterDiscreteValue pfd = new ParameterDiscreteValue();

            doc.Load(HttpContext.Current.Server.MapPath("/" + sReportName));
            //  crv.EnableParameterPrompt = false;
            // crv.ReportSource = doc;
            cninfo.ServerName = bll.sGetControlParameter("cr_server");  // ".\\sqlexpress";
            cninfo.DatabaseName = bll.sGetControlParameter("cr_db");// "MUS";
            cninfo.UserID = bll.sGetControlParameter("cr_uid");// "sa";
            cninfo.Password = bll.sGetControlParameter("cr_pwd"); // "preketek";

            Tables crTables = doc.Database.Tables;
            foreach (Table crTable in crTables)
            {
                TableLogOnInfo crtablelogoninfo = crTable.LogOnInfo;
                crtablelogoninfo.ConnectionInfo = cninfo;
                crTable.ApplyLogOnInfo(crtablelogoninfo);
            }
            //foreach (string sFormula in lFormula)
            //{
            //    crv.SelectionFormula = sFormula;
            //}

            doc.RecordSelectionFormula = lFormula[0];
            //for (int i = 0; i < lParameter.Count; i++)
            //{
            //    pfd = new ParameterDiscreteValue();
            //    pf = new ParameterField();
            //    cArrayList arr = lParameter[i];
            //    pf.Name = arr.paramid;
            //    pfd.Value = arr.ParamValue;
            //    pf.CurrentValues.Add(pfd);
            //    pfs.Add(pf);

            //}
            //   crv.ParameterFieldInfo = pfs;
            //    crv.DataBind();
            doc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.Excel, XlsName);
            doc.Close();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "self.close();", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_report3");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    public void vShowReportToExcell(ref CrystalReportViewer crv, string sReportName, string XlsName, List<cArrayList> lparam)
    {
        try
        {
            ConnectionInfo cninfo = new ConnectionInfo();
            ParameterFields pfs = new ParameterFields();
            ParameterField pf = null;
            ParameterDiscreteValue pfd = new ParameterDiscreteValue();

            doc.Load(HttpContext.Current.Server.MapPath("/" + sReportName));
            //  crv.EnableParameterPrompt = false;
            // crv.ReportSource = doc;
            cninfo.ServerName = bll.sGetControlParameter("cr_server");  // ".\\sqlexpress";
            cninfo.DatabaseName = bll.sGetControlParameter("cr_db");// "MUS";
            cninfo.UserID = bll.sGetControlParameter("cr_uid");// "sa";
            cninfo.Password = bll.sGetControlParameter("cr_pwd"); // "preketek";

            Tables crTables = doc.Database.Tables;
            foreach (Table crTable in crTables)
            {
                TableLogOnInfo crtablelogoninfo = crTable.LogOnInfo;
                crtablelogoninfo.ConnectionInfo = cninfo;
                crTable.ApplyLogOnInfo(crtablelogoninfo);
            }
            //foreach (string sFormula in lFormula)
            //{
            //    crv.SelectionFormula = sFormula;
            //}

            // doc.RecordSelectionFormula = lFormula[0];
            for (int i = 0; i < lparam.Count; i++)
            {
                //pfd = new ParameterDiscreteValue();
                //pf = new ParameterField();
                //cArrayList arr = lparam[i];
                //pf.Name = arr.paramid;
                //pfd.Value = arr.ParamValue;
                //pf.CurrentValues.Add(pfd);
                //pfs.Add(pf);
                pfd = new ParameterDiscreteValue();
                pf = new ParameterField();
                cArrayList arr = lparam[i];
                pf.Name = arr.paramid;
                pfd.Value = arr.ParamValue;
                pf.CurrentValues.Add(pfd);
                //pfs.Add(pf);
                // doc.ParameterFields.Add(pf);
                doc.SetParameterValue(arr.paramid, arr.ParamValue);
            }
            crv.ParameterFieldInfo = pfs;

            //crv.DataBind();
            doc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.ExcelRecord, XlsName);
            doc.Close();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "self.close();", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_report3");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    public void vShowreportDirectToPrinter(ref CrystalReportViewer crv, string sReportName)
    {
        try
        {
            ConnectionInfo cninfo = new ConnectionInfo();
            ParameterFields pfs = new ParameterFields();
            ParameterField pf = null;
            ParameterDiscreteValue pfd = new ParameterDiscreteValue();

            doc.Load(HttpContext.Current.Server.MapPath("/" + sReportName));
            //  crv.EnableParameterPrompt = false;
            // crv.ReportSource = doc;
            cninfo.ServerName = bll.sGetControlParameter("cr_server");  // ".\\sqlexpress";
            cninfo.DatabaseName = bll.sGetControlParameter("cr_db");// "MUS";
            cninfo.UserID = bll.sGetControlParameter("cr_uid");// "sa";
            cninfo.Password = bll.sGetControlParameter("cr_pwd"); // "preketek";

            Tables crTables = doc.Database.Tables;
            foreach (Table crTable in crTables)
            {
                TableLogOnInfo crtablelogoninfo = crTable.LogOnInfo;
                crtablelogoninfo.ConnectionInfo = cninfo;
                crTable.ApplyLogOnInfo(crtablelogoninfo);
            }

            //foreach (string sFormula in lFormula)
            //{
            //    // crv.SelectionFormula = sFormula;
            //    doc.RecordSelectionFormula = sFormula;
            //}
            //for (int i = 0; i < lParameter.Count; i++)
            //{
            //    pfd = new ParameterDiscreteValue();
            //    pf = new ParameterField();
            //    cArrayList arr = lParameter[i];
            //    pf.Name = arr.paramid;
            //    pfd.Value = arr.ParamValue;
            //    pf.CurrentValues.Add(pfd);
            //    pfs.Add(pf);
            //    doc.ParameterFields.Add(pf);

            //}
            //crv.ParameterFieldInfo = pfs;
            //crv.DataBind();

            doc.PrintToPrinter(1, false, 0, 0);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "self.close();", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_report3");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    public void vShowreportDirectToPrinter(ref CrystalReportViewer crv, string sReportName, List<string> lFormula)
    {
        try
        {
            ConnectionInfo cninfo = new ConnectionInfo();
            ParameterFields pfs = new ParameterFields();
            ParameterField pf = null;
            ParameterDiscreteValue pfd = new ParameterDiscreteValue();

            doc.Load(HttpContext.Current.Server.MapPath("/" + sReportName));
            //  crv.EnableParameterPrompt = false;
            // crv.ReportSource = doc;
            cninfo.ServerName = bll.sGetControlParameter("cr_server");  // ".\\sqlexpress";
            cninfo.DatabaseName = bll.sGetControlParameter("cr_db");// "MUS";
            cninfo.UserID = bll.sGetControlParameter("cr_uid");// "sa";
            cninfo.Password = bll.sGetControlParameter("cr_pwd"); // "preketek";

            Tables crTables = doc.Database.Tables;
            foreach (Table crTable in crTables)
            {
                TableLogOnInfo crtablelogoninfo = crTable.LogOnInfo;
                crtablelogoninfo.ConnectionInfo = cninfo;
                crTable.ApplyLogOnInfo(crtablelogoninfo);
            }

            foreach (string sFormula in lFormula)
            {
                // crv.SelectionFormula = sFormula;
                doc.RecordSelectionFormula = sFormula;
            }
            //for (int i = 0; i < lParameter.Count; i++)
            //{
            //    pfd = new ParameterDiscreteValue();
            //    pf = new ParameterField();
            //    cArrayList arr = lParameter[i];
            //    pf.Name = arr.paramid;
            //    pfd.Value = arr.ParamValue;
            //    pf.CurrentValues.Add(pfd);
            //    pfs.Add(pf);
            //    doc.ParameterFields.Add(pf);

            //}
            //crv.ParameterFieldInfo = pfs;
            //crv.DataBind();

            doc.PrintToPrinter(1, false, 0, 0);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "self.close();", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_report3");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    public void vShowreportDirectToPrinter(ref CrystalReportViewer crv, string sReportName, List<string> lFormula, List<cArrayList> lParameter)
    {
        try
        {
            ConnectionInfo cninfo = new ConnectionInfo();
            ParameterFields pfs = new ParameterFields();
            ParameterField pf = null;
            ParameterDiscreteValue pfd = new ParameterDiscreteValue();

            doc.Load(HttpContext.Current.Server.MapPath("/" + sReportName));
            //  crv.EnableParameterPrompt = false;
            // crv.ReportSource = doc;
            cninfo.ServerName = bll.sGetControlParameter("cr_server");  // ".\\sqlexpress";
            cninfo.DatabaseName = bll.sGetControlParameter("cr_db");// "MUS";
            cninfo.UserID = bll.sGetControlParameter("cr_uid");// "sa";
            cninfo.Password = bll.sGetControlParameter("cr_pwd"); // "preketek";

            Tables crTables = doc.Database.Tables;
            foreach (Table crTable in crTables)
            {
                TableLogOnInfo crtablelogoninfo = crTable.LogOnInfo;
                crtablelogoninfo.ConnectionInfo = cninfo;
                crTable.ApplyLogOnInfo(crtablelogoninfo);
            }

            foreach (string sFormula in lFormula)
            {
                // crv.SelectionFormula = sFormula;
                doc.RecordSelectionFormula = sFormula;
            }
            for (int i = 0; i < lParameter.Count; i++)
            {
                pfd = new ParameterDiscreteValue();
                pf = new ParameterField();
                cArrayList arr = lParameter[i];
                pf.Name = arr.paramid;
                pfd.Value = arr.ParamValue;
                pf.CurrentValues.Add(pfd);
                //pfs.Add(pf);
                // doc.ParameterFields.Add(pf);
                doc.SetParameterValue(arr.paramid, arr.ParamValue);
            }
            //crv.ParameterFieldInfo = pfs;
            //crv.DataBind();

            doc.PrintToPrinter(1, false, 0, 0);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "self.close();", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_report3");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    public void vShowReportToExcell(ref CrystalReportViewer crv, string sReportName, List<cArrayList> lParameter, string XlsName)
    {
        try
        {
            ConnectionInfo cninfo = new ConnectionInfo();
            ParameterFields pfs = new ParameterFields();
            ParameterField pf = null;
            ParameterDiscreteValue pfd = new ParameterDiscreteValue();

            doc.Load(HttpContext.Current.Server.MapPath("/" + sReportName));
            //  crv.EnableParameterPrompt = false;
            // crv.ReportSource = doc;
            cninfo.ServerName = bll.sGetControlParameter("cr_server");  // ".\\sqlexpress";
            cninfo.DatabaseName = bll.sGetControlParameter("cr_db");// "MUS";
            cninfo.UserID = bll.sGetControlParameter("cr_uid");// "sa";
            cninfo.Password = bll.sGetControlParameter("cr_pwd"); // "preketek";

            Tables crTables = doc.Database.Tables;
            foreach (Table crTable in crTables)
            {
                TableLogOnInfo crtablelogoninfo = crTable.LogOnInfo;
                crtablelogoninfo.ConnectionInfo = cninfo;
                crTable.ApplyLogOnInfo(crtablelogoninfo);
            }
            //foreach (string sFormula in lFormula)
            //{
            //    crv.SelectionFormula = sFormula;
            //}

            for (int i = 0; i < lParameter.Count; i++)
            {
                pfd = new ParameterDiscreteValue();
                pf = new ParameterField();
                cArrayList arr = lParameter[i];
                pf.Name = arr.paramid;
                pfd.Value = arr.ParamValue;
                pf.CurrentValues.Add(pfd);
                //pfs.Add(pf);
                // doc.ParameterFields.Add(pf);
                doc.SetParameterValue(arr.paramid, arr.ParamValue);
            }
            //for (int i = 0; i < lParameter.Count; i++)
            //{
            //    pfd = new ParameterDiscreteValue();
            //    pf = new ParameterField();
            //    cArrayList arr = lParameter[i];
            //    pf.Name = arr.paramid;
            //    pfd.Value = arr.ParamValue;
            //    pf.CurrentValues.Add(pfd);
            //    pfs.Add(pf);

            //}
            //   crv.ParameterFieldInfo = pfs;
            //    crv.DataBind();
            doc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.Excel, XlsName);
            doc.Close();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "self.close();", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_report3");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    public void vShowReportToPDF(ref CrystalReportViewer crv, string sReportName, List<string> lFormula, string PdfName)
    {
        try
        {
            ConnectionInfo cninfo = new ConnectionInfo();
            ParameterFields pfs = new ParameterFields();
            ParameterField pf = null;
            ParameterDiscreteValue pfd = new ParameterDiscreteValue();

            doc.Load(HttpContext.Current.Server.MapPath("/" + sReportName));
            //  crv.EnableParameterPrompt = false;
            // crv.ReportSource = doc;
            cninfo.ServerName = bll.sGetControlParameter("cr_server");  // ".\\sqlexpress";
            cninfo.DatabaseName = bll.sGetControlParameter("cr_db");// "MUS";
            cninfo.UserID = bll.sGetControlParameter("cr_uid");// "sa";
            cninfo.Password = bll.sGetControlParameter("cr_pwd"); // "preketek";

            Tables crTables = doc.Database.Tables;
            foreach (Table crTable in crTables)
            {
                TableLogOnInfo crtablelogoninfo = crTable.LogOnInfo;
                crtablelogoninfo.ConnectionInfo = cninfo;
                crTable.ApplyLogOnInfo(crtablelogoninfo);
            }
            //foreach (string sFormula in lFormula)
            //{
            //    crv.SelectionFormula = sFormula;
            //}

            doc.RecordSelectionFormula = lFormula[0];
            //for (int i = 0; i < lParameter.Count; i++)
            //{
            //    pfd = new ParameterDiscreteValue();
            //    pf = new ParameterField();
            //    cArrayList arr = lParameter[i];
            //    pf.Name = arr.paramid;
            //    pfd.Value = arr.ParamValue;
            //    pf.CurrentValues.Add(pfd);
            //    pfs.Add(pf);

            //}
            //   crv.ParameterFieldInfo = pfs;
            //    crv.DataBind();
            string sImagePath = bll.sGetControlParameter("image_path");
            doc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, sImagePath + PdfName);
            doc.Close();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "self.close();", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_report3");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    public void vShowReportToPDF(ref CrystalReportViewer crv, string sReportName, List<string> lFormula, List<cArrayList> lParameter, string PdfName)
    {
        try
        {

            ConnectionInfo cninfo = new ConnectionInfo();
            ParameterFields pfs = new ParameterFields();
            ParameterField pf = null;
            ParameterDiscreteValue pfd = new ParameterDiscreteValue();

            doc.Load(HttpContext.Current.Server.MapPath("/" + sReportName));
            //crv.EnableParameterPrompt = false;
            //crv.ReportSource = doc;
            cninfo.ServerName = bll.sGetControlParameter("cr_server");  // ".\\sqlexpress";
            cninfo.DatabaseName = bll.sGetControlParameter("cr_db");// "MUS";
            cninfo.UserID = bll.sGetControlParameter("cr_uid");// "sa";
            cninfo.Password = bll.sGetControlParameter("cr_pwd"); // "preketek";

            Tables crTables = doc.Database.Tables;
            foreach (Table crTable in crTables)
            {
                TableLogOnInfo crtablelogoninfo = crTable.LogOnInfo;
                crtablelogoninfo.ConnectionInfo = cninfo;
                crTable.ApplyLogOnInfo(crtablelogoninfo);
            }
            doc.RecordSelectionFormula = lFormula[0];

            //foreach (string sFormula in lFormula)
            //{
            //    crv.SelectionFormula = sFormula;

            //}
            for (int i = 0; i < lParameter.Count; i++)
            {
                pfd = new ParameterDiscreteValue();
                pf = new ParameterField();
                cArrayList arr = lParameter[i];
                pf.Name = arr.paramid;
                pfd.Value = arr.ParamValue;
                pf.CurrentValues.Add(pfd);
                pfs.Add(pf);
                doc.ParameterFields.Add(pfs);

            }
            //crv.ParameterFieldInfo = pfs;
            //    crv.DataBind();
            string sImagePath = bll.sGetControlParameter("image_path");
            doc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, sImagePath + PdfName);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "self.close();", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_report3");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    public void vShowReportToPDF(ref CrystalReportViewer crv, string sReportName, string PdfName)
    {
        try
        {
            ConnectionInfo cninfo = new ConnectionInfo();
            ParameterFields pfs = new ParameterFields();
            ParameterField pf = null;
            ParameterDiscreteValue pfd = new ParameterDiscreteValue();

            doc.Load(HttpContext.Current.Server.MapPath("/" + sReportName));
            crv.EnableParameterPrompt = false;
            crv.ReportSource = doc;
            cninfo.ServerName = bll.sGetControlParameter("cr_server");  // ".\\sqlexpress";
            cninfo.DatabaseName = bll.sGetControlParameter("cr_db");// "MUS";
            cninfo.UserID = bll.sGetControlParameter("cr_uid");// "sa";
            cninfo.Password = bll.sGetControlParameter("cr_pwd"); // "preketek";

            Tables crTables = doc.Database.Tables;
            foreach (Table crTable in crTables)
            {
                TableLogOnInfo crtablelogoninfo = crTable.LogOnInfo;
                crtablelogoninfo.ConnectionInfo = cninfo;
                crTable.ApplyLogOnInfo(crtablelogoninfo);
            }
            //foreach (string sFormula in lFormula)
            //{
            //    crv.SelectionFormula = sFormula;
            //}
            //for (int i = 0; i < lParameter.Count; i++)
            //{
            //    pfd = new ParameterDiscreteValue();
            //    pf = new ParameterField();
            //    cArrayList arr = lParameter[i];
            //    pf.Name = arr.paramid;
            //    pfd.Value = arr.ParamValue;
            //    pf.CurrentValues.Add(pfd);
            //    pfs.Add(pf);

            //}
            //crv.ParameterFieldInfo = pfs;
            //    crv.DataBind();
            string sImagePath = bll.sGetControlParameter("image_path");
            doc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, sImagePath + PdfName);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "self.close();", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_report3");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
}