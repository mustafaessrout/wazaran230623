using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
public partial class fm_report2 : System.Web.UI.Page
{
    cbll bll = new cbll();
    ReportDocument doc = new ReportDocument();
    public void vShowReport(ref CrystalReportViewer crv, string sReportName, List<string> lFormula, List<cArrayList> lParameter)
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
        foreach (string sFormula in lFormula)
        {
            crv.SelectionFormula = sFormula;
        }
        for (int i = 0; i < lParameter.Count; i++)
        {
            pfd = new ParameterDiscreteValue();
            pf = new ParameterField();
            cArrayList arr = lParameter[i];
            pf.Name = arr.paramid;
            pfd.Value = arr.ParamValue;
            pf.CurrentValues.Add(pfd);
            pfs.Add(pf);

        }
        crv.ParameterFieldInfo = pfs;
        crv.DataBind();
    }
    public void vShowReport(ref CrystalReportViewer crv, string sReportName, List<string> lFormula)
    {
        //ReportDocument doc = new ReportDocument();
        ConnectionInfo cninfo = new ConnectionInfo();
        doc.Load(HttpContext.Current.Server.MapPath("/" + sReportName));
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

        foreach (string sFormula in lFormula)
        {
            crv.SelectionFormula = sFormula;
        }
        crv.DataBind();
        crv.RefreshReport();
        //   Session["docreport"] = doc;

    }
    public void vShowReport(ref CrystalReportViewer crv, string sReportName)
    {
        // ReportDocument doc = new ReportDocument();
        ConnectionInfo cninfo = new ConnectionInfo();
        //string sReportPath = bll.sGetControlParameter("report_path") + reportname;
        doc.Load(HttpContext.Current.Server.MapPath("/" + sReportName));
        crv.ReportSource = doc;
        cninfo.ServerName = bll.sGetControlParameter("cr_server");  // ".\\sqlexpress";
        cninfo.DatabaseName = bll.sGetControlParameter("cr_db");// "MUS";
        cninfo.UserID = bll.sGetControlParameter("cr_uid");// "sa";
        cninfo.Password = bll.sGetControlParameter("cr_pwd"); // "preketek";

        //  cninfo.IntegratedSecurity = false;
        Tables crTables = doc.Database.Tables;

        foreach (Table crTable in crTables)
        {
            TableLogOnInfo crtablelogoninfo = crTable.LogOnInfo;
            crtablelogoninfo.ConnectionInfo = cninfo;
            crTable.ApplyLogOnInfo(crtablelogoninfo);

        }
        crv.DataBind();
        crv.RefreshReport();
        //   Session["docreport"] = doc;

    }

    public void vShowReportWithParameter(ref CrystalReportViewer crv, string sReportName, List<cArrayList> lParameter)
    {
        //   ReportDocument doc = new ReportDocument();
        ConnectionInfo cninfo = new ConnectionInfo();
        ParameterFields pfs = new ParameterFields();
        ParameterField pf = null;
        ParameterDiscreteValue pfd = new ParameterDiscreteValue();
        //string sReportPath = bll.sGetControlParameter("report_path") + reportname;
        string sReportPath = HttpContext.Current.Server.MapPath("/" + sReportName);
        doc.Load(HttpContext.Current.Server.MapPath("/" + sReportName));
        crv.EnableParameterPrompt = true;
        for (int i = 0; i < lParameter.Count; i++)
        {
            pfd = new ParameterDiscreteValue();
            pf = new ParameterField();
            cArrayList arr = lParameter[i];
            pf.Name = arr.paramid;
            pfd.Value = arr.ParamValue;
            pf.CurrentValues.Add(pfd);
            pfs.Add(pf);

        }

        crv.ReportSource = doc;
        crv.ParameterFieldInfo = pfs;
        cninfo.ServerName = bll.sGetControlParameter("cr_server");  // ".\\sqlexpress";
        cninfo.DatabaseName = bll.sGetControlParameter("cr_db");// "MUS";
        cninfo.UserID = bll.sGetControlParameter("cr_uid");// "sa";
        cninfo.Password = bll.sGetControlParameter("cr_pwd"); // "preketek";
        //  cninfo.IntegratedSecurity = false;
        Tables crTables = doc.Database.Tables;
        foreach (Table crTable in crTables)
        {
            TableLogOnInfo crtablelogoninfo = crTable.LogOnInfo;
            crtablelogoninfo.ConnectionInfo = cninfo;
            crTable.ApplyLogOnInfo(crtablelogoninfo);
        }

        crv.ParameterFieldInfo = pfs;
        crv.DataBind();
        //  Session["docreport"]  doc;

    }

    public void vShowReportWithParameter(ref CrystalReportViewer crv, string sReportName, List<cArrayList> lParameter, List<string> lFormula)
    {
        //  ReportDocument doc = new ReportDocument();
        ConnectionInfo cninfo = new ConnectionInfo();
        ParameterFields pfs = new ParameterFields();
        ParameterField pf = null;
        ParameterDiscreteValue pfd = new ParameterDiscreteValue();
        //string sReportPath = bll.sGetControlParameter("report_path") + reportname;
        doc.Load(HttpContext.Current.Server.MapPath("/" + sReportName));
        crv.EnableParameterPrompt = true;
        for (int i = 0; i < lParameter.Count; i++)
        {
            pfd = new ParameterDiscreteValue();
            pf = new ParameterField();
            cArrayList arr = lParameter[i];
            pf.Name = arr.paramid;
            pfd.Value = arr.ParamValue;
            pf.CurrentValues.Add(pfd);
            pfs.Add(pf);

        }

        crv.ReportSource = doc;
        crv.ParameterFieldInfo = pfs;
        cninfo.ServerName = bll.sGetControlParameter("cr_server");  // ".\\sqlexpress";
        cninfo.DatabaseName = bll.sGetControlParameter("cr_db");// "MUS";
        cninfo.UserID = bll.sGetControlParameter("cr_uid");// "sa";
        cninfo.Password = bll.sGetControlParameter("cr_pwd"); // "preketek";
        //  cninfo.IntegratedSecurity = false;
        Tables crTables = doc.Database.Tables;
        foreach (Table crTable in crTables)
        {
            TableLogOnInfo crtablelogoninfo = crTable.LogOnInfo;
            crtablelogoninfo.ConnectionInfo = cninfo;
            crTable.ApplyLogOnInfo(crtablelogoninfo);
        }

        crv.ParameterFieldInfo = pfs;
        foreach (string sFormula in lFormula)
        {
            crv.SelectionFormula = sFormula;
        }
        crv.DataBind();
        //  Session["docreport"] = doc;

    }
    protected void Page_Init(object sender, EventArgs e)
    {
        crviewer.ReportSource = null;
        string sPrintCount = string.Empty;
        //  creport crep = new creport();
        //if (!IsPostBack)
        //{
        List<string> lformula = new List<string>();
        string sSrc = Request.QueryString["src"].ToString();
        List<cArrayList> lparam = new List<cArrayList>();

        switch (sSrc)
        {
            case "canvastab":
                DateTime startdate = System.DateTime.ParseExact(Request.QueryString["s"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime enddate = System.DateTime.ParseExact(Request.QueryString["e"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                lparam.Add(new cArrayList("@start_dt", startdate));
                lparam.Add(new cArrayList("@end_dt", enddate));
                vShowReportWithParameter(ref crviewer, "rp_canvastab.rpt", lparam);
                break;
            case "rpstarget":
                DateTime _daterps = System.DateTime.ParseExact(Request.QueryString["dt"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                lparam.Add(new cArrayList("@dt", _daterps));
                lparam.Add(new cArrayList("@emp_cd", Request.QueryString["e"]));
                vShowReportWithParameter(ref crviewer, "rp_rpstarget.rpt", lparam);
                break;
            case "stockcard":
                lparam.Add(new cArrayList("@whs_cd", Request.QueryString["w"]));
                lparam.Add(new cArrayList("@bin_cd", Request.QueryString["b"]));
                lparam.Add(new cArrayList("@period", Request.QueryString["p"]));
                lparam.Add(new cArrayList("@item_cd", Request.QueryString["i"]));
                vShowReportWithParameter(ref crviewer, "rp_stockcard.rpt", lparam);
                break;
            case "navisioncashin":
                DateTime _dtsumm = System.DateTime.ParseExact(Request.QueryString["dt"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                lparam.Add(new cArrayList("@cash_dt", _dtsumm.ToString("M/d/yyyy")));
                lparam.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
                vShowReportWithParameter(ref crviewer, "rp_navisionsummcashin.rpt", lparam);
                break;
            case "navisionstock":
                DateTime _dt = System.DateTime.ParseExact(Request.QueryString["dt"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                lparam.Add(new cArrayList("@start_dt", _dt.ToString("M/d/yyyy")));
                vShowReportWithParameter(ref crviewer, "rp_navisionstock.rpt", lparam);
                break;
            case "salesmanbalanceall":
                lparam.Add(new cArrayList("@start_dt", DateTime.ParseExact(Request.QueryString["s"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                lparam.Add(new cArrayList("@end_dt", DateTime.ParseExact(Request.QueryString["e"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                lparam.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
                vShowReportWithParameter(ref crviewer, "rp_salesmanbalanceAll.rpt", lparam);
                break;
            case "rawdatastock":
                lparam.Add(new cArrayList("@start_dt", DateTime.ParseExact(Request.QueryString["s"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                lparam.Add(new cArrayList("@end_dt", DateTime.ParseExact(Request.QueryString["e"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                vShowReportWithParameter(ref crviewer, "rp_rawdatastock.rpt", lparam);
                break;
            case "rpsdistrict":
                lparam.Add(new cArrayList("@emp_cd", Request.QueryString["e"]));
                vShowReportWithParameter(ref crviewer, "rp_rpsdistrict.rpt", lparam);
                break;
            case "rpssalesman":
                lparam.Add(new cArrayList("@emp_cd", Request.QueryString["s"]));
                lparam.Add(new cArrayList("@day_cd", Request.QueryString["d"]));
                vShowReportWithParameter(ref crviewer, "rp_rpssalesman.rpt", lparam);
                break;
            case "cndn":
                lparam.Add(new cArrayList("@cndn_cd", Request.QueryString["c"]));
                vShowReportWithParameter(ref crviewer, "rp_cndn.rpt", lparam);
                break;
            case "rawdata":
                DateTime _dtstart = System.DateTime.ParseExact(Request.QueryString["s"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime _dtend = System.DateTime.ParseExact(Request.QueryString["e"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                lparam.Add(new cArrayList("@dtord1", _dtstart));
                lparam.Add(new cArrayList("@dtord2", _dtend));
                lparam.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
                vShowReportWithParameter(ref crviewer, "rp_datarow.rpt", lparam);
                break;
            case "cashierpettycash":
                lparam.Add(new cArrayList("@start_dt", System.DateTime.ParseExact(Request.QueryString["s"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                lparam.Add(new cArrayList("@end_dt", System.DateTime.ParseExact(Request.QueryString["e"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                lparam.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
                lparam.Add(new cArrayList("@cashier_id", Request.QueryString["c"]));
                vShowReportWithParameter(ref crviewer, "rp_cashieradvance.rpt", lparam);
                break;
            case "salesmancndn":
                lparam.Add(new cArrayList("@cndn_cd", Request.QueryString["n"]));
                vShowReportWithParameter(ref crviewer, "rp_cndnsalesman.rpt", lparam);
                break;
            case "repack":
                lparam.Add(new cArrayList("@repack_cd", Request.QueryString["n"]));
                vShowReportWithParameter(ref crviewer, "rp_repack.rpt", lparam);
                break;
            case "bom":
                lparam.Add(new cArrayList("@bom_cd", Request.QueryString["b"]));
                vShowReportWithParameter(ref crviewer, "rp_bom.rpt", lparam);
                break;
            case "salesmanbalance":
                string _salespoint = bll.vLookUp("select salespointcd from tmst_employee where emp_cd='" + Request.QueryString["emp"] + "'");
                DateTime _start = System.DateTime.ParseExact(Request.QueryString["start"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime _end = System.DateTime.ParseExact(Request.QueryString["end"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                string _emp = Request.QueryString["emp"];
                lparam.Add(new cArrayList("@start_dt", _start));
                lparam.Add(new cArrayList("@end_dt", _end));
                lparam.Add(new cArrayList("@emp_cd", Request.QueryString["emp"]));
                lparam.Add(new cArrayList("@salespointcd", _salespoint));
                vShowReportWithParameter(ref crviewer, "rp_salesmanbalance.rpt", lparam);
                break;
            case "invoicelog":
                lparam.Add(new cArrayList("@period", Request.QueryString["p"]));
                vShowReportWithParameter(ref crviewer, "rpinvoicelogdate.rpt", lparam);
                break;
            case "salesgas":
                lparam.Add(new cArrayList("@gas_cd", Request.QueryString["n"]));
                vShowReportWithParameter(ref crviewer, "rp_salesmangasoline.rpt", lparam);
                break;
            case "salesdep":
                lparam.Add(new cArrayList("@deposit_cd", Request.QueryString["d"]));
                vShowReportWithParameter(ref crviewer, "rp_salesdep.rpt", lparam);
                break;
            case "cashierpetty":
                List<cArrayList> lParamrcshpetty = (List<cArrayList>)Session["lParamrcshpetty"];
                lParamrcshpetty.Add(new cArrayList("logo", Request.Cookies["logo"].Value.ToString()));
                vShowReportWithParameter(ref crviewer, "rp_cashierpetty.rpt", lParamrcshpetty);
                break;
            case "croreq2":
                //lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                //{tcashregout.casregout_cd} = "CO00002"
                //lformula.Add("{tcashout_request.cashout_cd} = '" + Request.QueryString["no"] + "'");
                lparam.Add(new cArrayList("@cashout_cd", Request.QueryString["no"]));
                //lparam.Add(new cArrayList("logo", Request.Cookies["logo"].Value.ToString()));
                //lparam.Add(new cArrayList("p_cashoutype", bll.vLookUp("select fld_desc from tfield_value where fld_nm='cashout_typ' and fld_valu=(select cashout_typ from tcashregout_dtl where casregout_cd='" + Request.QueryString["no"] + "')")));
                //if (isFISession == true) { vShowReportWithParameter(ref crviewer, "rp_cashrequest.rpt", lparam, lformula); }
                //else { vShowReportWithParameter(ref crviewer, "rp_cashregrequest.rpt", lparam, lformula); }
                vShowReportWithParameter(ref crviewer, "rp_cashoutrequest.rpt", lparam);

                break;
            case "bl":
                lparam.Add(new cArrayList("@bl_no", Request.QueryString["no"]));
                vShowReportWithParameter(ref crviewer, "rp_bl.rpt", lparam);
                break;
            case "grnav":
                lparam.Add(new cArrayList("@receipt_no", Request.QueryString["n"]));
                lparam.Add(new cArrayList("printby", bll.vLookUp("select emp_nm from tmst_employee where emp_cd='" + Request.Cookies["usr_id"].Value + "'")));
                vShowReportWithParameter(ref crviewer, "rp_goodreceived_gdn.rpt", lparam);
                break;
            case "stockindirect":
                lparam.Add(new cArrayList("@stockin_no", Request.QueryString["no"]));
                vShowReportWithParameter(ref crviewer, "rp_stockindirect.rpt", lparam);
                break;
            case "pychequeho":
                string sPayment2 = Request.QueryString["py"];
                lparam.Add(new cArrayList("@payment_no", Request.QueryString["py"]));
                vShowReportWithParameter(ref crviewer, "rp_paymentreceiptcheque_ho.rpt", lparam);
                break;
            case "pyho":
                //lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                lparam.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                lparam.Add(new cArrayList("@payment_no", Request.QueryString["noy"]));
                vShowReportWithParameter(ref crviewer, "rp_paymentreceipt_ho.rpt", lparam);
                break;
            case "stockall":
                List<cArrayList> lParamstock_all = (List<cArrayList>)Session["lParamstock"];
                vShowReportWithParameter(ref crviewer, "rptStock_all.rpt", lParamstock_all);
                break;
            case "stockuomall":
                List<cArrayList> lParamstockUom_all = (List<cArrayList>)Session["lParamstockUom"];
                vShowReportWithParameter(ref crviewer, "rptStock_uom_all.rpt", lParamstockUom_all);
                break;
            case "sum_salesmandaily":
                List<cArrayList> lParamsum_salesmandaily = (List<cArrayList>)Session["lParamsum_salesmandaily"];
                vShowReportWithParameter(ref crviewer, "rp_sum_dailysalesman.rpt", lParamsum_salesmandaily);
                break;
            case "stockconfirm_uom":
                List<cArrayList> lParamstockconfirm_uom = (List<cArrayList>)Session["lParamstockconfirm_uom"];
                vShowReportWithParameter(ref crviewer, "rptstockconfirm_uom.rpt", lParamstockconfirm_uom);
                break;
            case "sopu":
                List<cArrayList> lParamsopu = (List<cArrayList>)Session["lParamsopu"];
                vShowReportWithParameter(ref crviewer, "rp_stockopname_uom.rpt", lParamsopu);
                break;
            case "master_employee":
                List<cArrayList> lParam_mst_employee = (List<cArrayList>)Session["lParam_mst_employee"];
                vShowReportWithParameter(ref crviewer, "rp_master_employee.rpt", lParam_mst_employee);
                break;
            case "stockCost":
                List<cArrayList> lParamstockCost = (List<cArrayList>)Session["lParamstockCost"];
                vShowReportWithParameter(ref crviewer, "rptStockCost.rpt", lParamstockCost);
                break;
            case "stockCostSumm":
                List<cArrayList> lParamstockCostSumm = (List<cArrayList>)Session["lParamstockCostSumm"];
                vShowReportWithParameter(ref crviewer, "rptStockCostSumm.rpt", lParamstockCostSumm);
                break;
            case "sum_salesdailybybranchdetail":
                List<cArrayList> lParamsum_salesdailybybranchdetail = (List<cArrayList>)Session["lParamsum_salesdailybybranchdetail"];
                vShowReportWithParameter(ref crviewer, "rp_detail_salesbyday.rpt", lParamsum_salesdailybybranchdetail);
                break;
            case "sum_salesdailybyotlcd":
                List<cArrayList> lParamsum_salesdailybyotlcd = (List<cArrayList>)Session["lParamsum_salesdailybyotlcd"];
                vShowReportWithParameter(ref crviewer, "rp_sum_salesbycategory.rpt", lParamsum_salesdailybyotlcd);
                break;
            case "attendance":
                List<cArrayList> lParamAttendance = (List<cArrayList>)Session["lParamAttendance"];
                vShowReportWithParameter(ref crviewer, "rp_attendance_list.rpt", lParamAttendance);
                break;
            case "master_customer":
                List<cArrayList> lParam_mst_customer = (List<cArrayList>)Session["lParam_mst_customer"];
                vShowReportWithParameter(ref crviewer, "rp_master_customer.rpt", lParam_mst_customer);
                break;
            case "sum_salesdailybybranch":
                List<cArrayList> lParamsum_salesdailybybranch = (List<cArrayList>)Session["lParamsum_salesdailybybranch"];
                vShowReportWithParameter(ref crviewer, "rp_sum_salesbyday.rpt", lParamsum_salesdailybybranch);
                break;
            case "invto1_uom":
                sPrintCount = bll.vLookUp("select isnull(qty,0) from tprint_control where print_cd='TOINV' and doc_no='" + Request.QueryString["no"] + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                //lformula.Add("{trpt_dosalesinvoice.inv_no} = '" + Request.QueryString["no"] + "' and {trpt_dosalesinvoice.usr_id} = '" + Request.Cookies["usr_id"].Value.ToString() + "'");
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                if (bll.vLookUp("select distinct 1 from tdosalesinvoice_free where inv_no='" + Request.QueryString["no"] + "'") == "1")
                {
                    lparam.Add(new cArrayList("p_free", "+"));
                }
                else
                {
                    lparam.Add(new cArrayList("p_free", ""));
                }
                if (sPrintCount == "")
                {
                    lparam.Add(new cArrayList("p_print", ""));
                }
                else { lparam.Add(new cArrayList("p_print", "PRINTED No. " + sPrintCount)); }
                //lformula.Add("{trpt_dosalesinvosoa2bycustice.inv_no} = '" + Request.QueryString["no"] + "' and {trpt_dosalesinvoice.usr_id} = '" + Request.Cookies["usr_id"].Value.ToString() + "'");
                lparam.Add(new cArrayList("@inv_no", Request.QueryString["no"]));
                lparam.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                vShowReportWithParameter(ref crviewer, "rp_dosalesinvoice4p_uom.rpt", lparam, lformula);
                break;
            case "invto_uom":

                sPrintCount = bll.vLookUp("select cast(isnull(qty,0) as numeric(18,0)) from tprint_control where print_cd='TOINV' and doc_no='" + Request.QueryString["no"] + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                //lformula.Add("{trpt_dosalesinvoice.inv_no} = '" + Request.QueryString["no"] + "' and {trpt_dosalesinvoice.usr_id} = '" + Request.Cookies["usr_id"].Value.ToString() + "'");
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                lparam.Add(new cArrayList("@inv_no", Request.QueryString["no"]));
                //lparam.Add(new cArrayList("p_terbilang_french", Request.QueryString["f"]));
                vShowReportWithParameter(ref crviewer, "rp_invoice.rpt", lparam);
                //if (sPrintCount == "")
                //{
                //    lparam.Add(new cArrayList("p_print", ""));
                //}
                //else { lparam.Add(new cArrayList("p_print", "RE-PRINTED No. " + sPrintCount)); }
                //lparam.Add(new cArrayList("p_rps", bll.vLookUp("select dbo.fn_getrps('" + Request.QueryString["no"] + "','" + Request.Cookies["sp"].Value.ToString() + "')")));
                //lparam.Add(new cArrayList("@inv_no", Request.QueryString["no"]));
                //lparam.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //vShowReportWithParameter(ref crviewer, "rp_dosalesinvoice3_uom.rpt", lparam, lformula);
                break;
            case "blwprice":

                sPrintCount = bll.vLookUp("select cast(isnull(qty,0) as numeric(18,0)) from tprint_control where print_cd='TOINV' and doc_no='" + Request.QueryString["no"] + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                lparam.Add(new cArrayList("@inv_no", Request.QueryString["no"]));

                vShowReportWithParameter(ref crviewer, "rp_invoicebl.rpt", lparam);

                break;
            case "so_uom":
                //lformula.Add("{vtmst_salesorder.salespointcd} = '" + Request.Cookies["sp"].Value.ToString() + "'");
                //lformula.Add("{vtmst_salesorder.so_cd} = '" + Request.QueryString["so"] + "'");
                lparam.Add(new cArrayList("@so_cd", Request.QueryString["so"]));
                vShowReportWithParameter(ref crviewer, "rp_salesorder_uom.rpt", lparam);
                break;
            case "so1_uom":
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                lparam.Add(new cArrayList("@so_cd", Request.QueryString["so"]));
                lparam.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                vShowReportWithParameter(ref crviewer, "rp_salesorderp_uom.rpt", lparam, lformula);
                break;
            case "MSTPRICE":
                lparam.Clear();
                //lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                vShowReport(ref crviewer, "rptcustomertypeprice3.rpt");
                break;
            case "sum_po_stkin":
                List<cArrayList> lParamsum_po_stkin = (List<cArrayList>)Session["lParamsum_po_stkin"];
                vShowReportWithParameter(ref crviewer, "rp_sum_po2.rpt", lParamsum_po_stkin);
                break;
            case "sum_do_stkin":
                List<cArrayList> lParamsum_do_stkin = (List<cArrayList>)Session["lParamsum_do_stkin"];
                vShowReportWithParameter(ref crviewer, "rp_sum_do2.rpt", lParamsum_do_stkin);
                break;
            case "sum_gr_stkin":
                List<cArrayList> lParamsum_gr_stkin = (List<cArrayList>)Session["lParamsum_gr_stkin"];
                vShowReportWithParameter(ref crviewer, "rp_sum_gr2.rpt", lParamsum_gr_stkin);
                break;
            case "sum_salesbytargetflavor":
                List<cArrayList> lParamsum_slsbytargetflv = (List<cArrayList>)Session["lParamsum_slsbytargetflv"];
                vShowReportWithParameter(ref crviewer, "rp_sum_salesmantargetbyitem.rpt", lParamsum_slsbytargetflv);
                break;
            case "sum_expense":
                List<cArrayList> lParamsum_expense = (List<cArrayList>)Session["lParamsum_expense"];
                vShowReportWithParameter(ref crviewer, "rp_sum_expense.rpt", lParamsum_expense);
                break;
            case "sum_cashierho":
                List<cArrayList> lParamsum_cashierho = (List<cArrayList>)Session["lParamsum_cashierho"];
                vShowReportWithParameter(ref crviewer, "rp_sum_cashierhodeposit.rpt", lParamsum_cashierho);
                break;
            case "sum_bank":
                List<cArrayList> lParamsum_bank = (List<cArrayList>)Session["lParamsum_bank"];
                vShowReportWithParameter(ref crviewer, "rp_sum_bankdeposit.rpt", lParamsum_bank);
                break;
            case "sum_bank_rejected":
                List<cArrayList> lParamsum_bank_rej = (List<cArrayList>)Session["lParamsum_bank"];
                vShowReportWithParameter(ref crviewer, "rp_sum_bankdeposit_Rejected.rpt", lParamsum_bank_rej);
                break;
            case "stockuom":
                List<cArrayList> lParamstockUom = (List<cArrayList>)Session["lParamstockUom"];
                vShowReportWithParameter(ref crviewer, "rptStock_uom.rpt", lParamstockUom);
                break;
            case "sum_customerbyflavor":
                List<cArrayList> lParamsum_custbyflv = (List<cArrayList>)Session["lParamsum_custbyflv"];
                vShowReportWithParameter(ref crviewer, "rp_sum_customerbyitem.rpt", lParamsum_custbyflv);
                break;
            case "sum_salesbyflavorqty":
                List<cArrayList> lParamsum_slsbyflvqty = (List<cArrayList>)Session["lParamsum_slsbyflvqty"];
                vShowReportWithParameter(ref crviewer, "rp_sum_salesmanbyitemflavour.rpt", lParamsum_slsbyflvqty);
                break;
            case "sum_salesbyflavorcoll":
                List<cArrayList> lParamsum_slsbyflvcoll = (List<cArrayList>)Session["lParamsum_slsbyflvcoll"];
                vShowReportWithParameter(ref crviewer, "rp_sum_salesmanbyitem_collection.rpt", lParamsum_slsbyflvcoll);
                break;
            case "sum_stokin":
                //List<cArrayList> lParamsum_stksum = (List<cArrayList>)Session["lParamsum_stksum"];
                //vShowReportWithParameter(ref crviewer, "rp_sum_stockin2.rpt", lParamsum_stksum);
                DateTime _startdate = System.DateTime.ParseExact(Request.QueryString["s"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime _enddate = System.DateTime.ParseExact(Request.QueryString["e"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                string _salespointcd = Request.QueryString["sp"];
                string _user = Request.Cookies["usr_id"].ToString();
                lparam.Add(new cArrayList("@start_dt", _startdate));
                lparam.Add(new cArrayList("@end_dt", _enddate));
                //lparam.Add(new cArrayList("@usr_id", _user));
                lparam.Add(new cArrayList("@salespointcd", _salespointcd));
                vShowReportWithParameter(ref crviewer, "rp_grnsummary.rpt", lparam);
                break;
            case "sum_monthlyclosingall":
                List<cArrayList> lParamsum_monthlyclosingall = (List<cArrayList>)Session["lParamsum_monthlyclosingall"];
                vShowReportWithParameter(ref crviewer, "rp_sum_monthlyclosing3.rpt", lParamsum_monthlyclosingall);
                break;
            case "sum_monthlyclosing":
                List<cArrayList> lParamsum_monthlyclosing = (List<cArrayList>)Session["lParamsum_monthlyclosing"];
                vShowReportWithParameter(ref crviewer, "rp_sum_monthlyclosing2.rpt", lParamsum_monthlyclosing);
                break;
            case "sum_dailyclosing":
                List<cArrayList> lParamsum_dailyclosing = (List<cArrayList>)Session["lParamsum_dailyclosing"];
                vShowReportWithParameter(ref crviewer, "rp_sum_dailyclosing2.rpt", lParamsum_dailyclosing);
                break;
            case "stockraw":
                List<cArrayList> lParamstockraw = (List<cArrayList>)Session["lParamstockraw"];
                vShowReportWithParameter(ref crviewer, "rptStockRaw.rpt", lParamstockraw);
                break;
            case "invtofree":
                //sPrintCount = bll.vLookUp("select isnull(qty,0) from tprint_control where print_cd='TOINV' and doc_no='" + Request.QueryString["no"] + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                //lformula.Add("{trpt_dosalesinvoice.inv_no} = '" + Request.QueryString["no"] + "' and {trpt_dosalesinvoice.usr_id} = '" + Request.Cookies["usr_id"].Value.ToString() + "'");
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                //if (sPrintCount == "")
                //{
                lparam.Add(new cArrayList("p_print", ""));
                //}
                //else { lparam.Add(new cArrayList("p_print", "RE-PRINTED No. " + sPrintCount)); }
                //lparam.Add(new cArrayList("p_rps", bll.vLookUp("select dbo.fn_getrps('" + Request.QueryString["no"] + "','" + Request.Cookies["sp"].Value.ToString() + "')")));
                lparam.Add(new cArrayList("p_rps", ""));
                lparam.Add(new cArrayList("@inv_no", Request.QueryString["no"]));
                lparam.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                vShowReportWithParameter(ref crviewer, "rp_dosalesinvoice3free.rpt", lparam, lformula);
                break;
            case "invto1free":
                //sPrintCount = bll.vLookUp("select isnull(qty,0) from tprint_control where print_cd='TOINV' and doc_no='" + Request.QueryString["no"] + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                //lformula.Add("{trpt_dosalesinvoice.inv_no} = '" + Request.QueryString["no"] + "' and {trpt_dosalesinvoice.usr_id} = '" + Request.Cookies["usr_id"].Value.ToString() + "'");
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                //if (bll.vLookUp("select distinct 1 from tdosalesinvoice_free where inv_no='" + Request.QueryString["no"] + "'") == "1")
                //{
                //    lparam.Add(new cArrayList("p_free", "+"));
                //}
                //else
                //{
                lparam.Add(new cArrayList("p_free", ""));
                //}
                //if (sPrintCount == "")
                //{
                lparam.Add(new cArrayList("p_print", ""));
                //}
                //else { lparam.Add(new cArrayList("p_print", "PRINTED No. " + sPrintCount)); }
                //lformula.Add("{trpt_dosalesinvosoa2bycustice.inv_no} = '" + Request.QueryString["no"] + "' and {trpt_dosalesinvoice.usr_id} = '" + Request.Cookies["usr_id"].Value.ToString() + "'");
                lparam.Add(new cArrayList("@inv_no", Request.QueryString["no"]));
                lparam.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                vShowReportWithParameter(ref crviewer, "rp_dosalesinvoice4pfree.rpt", lparam, lformula);
                break;
            case "sofree":
                lparam.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                lparam.Add(new cArrayList("@so_cd", Request.QueryString["so"]));
                vShowReportWithParameter(ref crviewer, "rp_salesorderfree.rpt", lparam, lformula);
                break;

            case "so1free":
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                lparam.Add(new cArrayList("@so_cd", Request.QueryString["so"]));
                lparam.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                vShowReportWithParameter(ref crviewer, "rp_salesorderpfree.rpt", lparam, lformula);
                break;
            case "doho":
                lparam.Clear();
                lparam.Add(new cArrayList("@do_no", Request.QueryString["no"]));
                vShowReportWithParameter(ref crviewer, "rptdoho.rpt", lparam);
                break;
            case "proformainv":
                lparam.Clear();
                lparam.Add(new cArrayList("@inv_no", Request.QueryString["no"]));
                vShowReportWithParameter(ref crviewer, "rp_proformainv.rpt", lparam);
                break;
            case "po_br ":
                lparam.Clear();
                lparam.Add(new cArrayList("@prop_no", Request.QueryString["no"]));
                lparam.Add(new cArrayList("@cust", Request.QueryString["salespoint"]));
                vShowReportWithParameter(ref crviewer, "rppo_br.rpt", lparam);
                break;
            case "goodreceipt":
                List<cArrayList> lformulagoodreceipt = (List<cArrayList>)Session["lformulagoodreceipt"];
                vShowReportWithParameter(ref crviewer, "rptgoodreceipt.rpt", lformulagoodreceipt);
                break;
            case "retHO1":
            case "retHO2":
                List<cArrayList> lParamretHO1 = (List<cArrayList>)Session["lParamretHO1"];
                vShowReportWithParameter(ref crviewer, "rp_" + sSrc + ".rpt", lParamretHO1);
                break;
            case "rtnwrk":
                lparam.Add(new cArrayList("@retur_no", Request.QueryString["returno"]));
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                //string sRemark = "";
                //sRemark = bll.vLookUp("select remark from tsalesreturn where retur_no='"+Request.QueryString["returno"]+"'");
                //sRemark = bll.vLookUp("select reasn_nm from tmst_reason where reasn_typ='return' and reasn_cd='"+sRemark+"'");
                //lparam.Add(new cArrayList("p_remark", sRemark));                
                vShowReportWithParameter(ref crviewer, "rp_salesreturn_wrk.rpt", lparam);
                break;
            case "advancedempAllSummary":
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                string sStartDateS = "1/1/2018"; //DateTime.ParseExact(Request.QueryString["start_dt"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString();
                string sEndDateS = DateTime.ParseExact(Request.QueryString["end_dt"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString();
                lparam.Add(new cArrayList("@start_dt", Convert.ToDateTime(sStartDateS)));
                lparam.Add(new cArrayList("@end_dt", Convert.ToDateTime(sEndDateS)));
                vShowReportWithParameter(ref crviewer, "rp_employeeadvancedAllSummary.rpt", lparam);
                break;
            case "stockcustomerne_emp":
                lparam.Add(new cArrayList("@period_cd", Convert.ToString(Request.QueryString["period_cd"])));
                lparam.Add(new cArrayList("@emp_cd", Convert.ToString(Request.QueryString["emp"])));
                vShowReportWithParameter(ref crviewer, "rp_stockcustomerne_emp.rpt", lparam);
                break;

            case "IncentiveMerch_Other":
                lparam.Add(new cArrayList("@emp_cd", Convert.ToString(Request.QueryString["emp"])));
                lparam.Add(new cArrayList("@period", Convert.ToString(Request.QueryString["period"])));
                lparam.Add(new cArrayList("@salespointcd", Convert.ToString(Request.QueryString["sp"])));
                lparam.Add(new cArrayList("@job_title_cd", Convert.ToString(Request.QueryString["job_title_cd"])));
                lparam.Add(new cArrayList("@level_cd", Convert.ToString(Request.QueryString["level_cd"])));
                if (Convert.ToString(Request.QueryString["job_title_cd"]) == "22")
                {
                    vShowReport(ref crviewer, "rp_hrdkpiIncentive_Merch_TL_other.rpt", lformula, lparam);
                }
                else
                {
                    vShowReport(ref crviewer, "rp_hrdkpiIncentive_Merch_other.rpt", lformula, lparam);
                }
                break;
            case "MerchandiserHis":
                lparam.Add(new cArrayList("@driver_cd", Convert.ToString(Request.QueryString["driver_cd"])));
                lparam.Add(new cArrayList("@period_cd", Convert.ToString(Request.QueryString["period_cd"])));
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                vShowReport(ref crviewer, "rp_MerchandiserHis.rpt", lformula, lparam);
                break;
            case "rp_salestargetAchievementAllBranchPeriod":
                string achievementPeriodBr = Request.Cookies["achievementPeriod"].Value.ToString();
                lparam.Add(new cArrayList("@period", achievementPeriodBr));
                vShowReportWithParameter(ref crviewer, "rp_salestargetAchievementAllBranchPeriod.rpt", lparam);
                break;
            case "salestargetAchievementHO":
                string achievementPeriod = Request.Cookies["achievementPeriod"].Value.ToString();
                string achievementDate = Request.Cookies["achievementDate"].Value.ToString();
                lparam.Add(new cArrayList("@period", achievementPeriod));
                if (achievementDate == "")
                {
                    lparam.Add(new cArrayList("@createdDate", null));
                    Response.Cookies["achievementDate"].Value = "";
                }
                else { lparam.Add(new cArrayList("@createdDate", achievementDate)); }
                vShowReportWithParameter(ref crviewer, "rp_salestargetAchievementAllBranch.rpt", lparam);
                break;
            case "salestargetAchievementByProd":
                string periodByProd = Request.QueryString["period"].ToString();
                string prodByProd = Request.QueryString["prod_cd"].ToString();
                lparam.Add(new cArrayList("@period", periodByProd));
                lparam.Add(new cArrayList("@prod_cd", prodByProd));
                vShowReportWithParameter(ref crviewer, "rp_salestargetAchievement_byProd.rpt", lparam);
                break;
            case "salestargetAchievement":
                string periodTarget = Request.QueryString["period"].ToString();
                lparam.Add(new cArrayList("@period", periodTarget));
                vShowReportWithParameter(ref crviewer, "rp_salestargetAchievement.rpt", lparam);
                break;
            case "poreport":
                List<cArrayList> lParamporeport = (List<cArrayList>)Session["lParamporeport"];
                vShowReportWithParameter(ref crviewer, "rp_poreport.rpt", lParamporeport);
                break;
            case "advancedempAll":
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                string sStartDateA = "1/1/2018"; //DateTime.ParseExact(Request.QueryString["start_dt"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString();
                string sEndDateA = DateTime.ParseExact(Request.QueryString["end_dt"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString();
                lparam.Add(new cArrayList("@start_dt", Convert.ToDateTime(sStartDateA)));
                lparam.Add(new cArrayList("@end_dt", Convert.ToDateTime(sEndDateA)));
                vShowReportWithParameter(ref crviewer, "rp_employeeadvancedAll.rpt", lparam);
                break;
            case "TransCustApp":
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                lparam.Add(new cArrayList("@from_dt", System.DateTime.ParseExact(Request.QueryString["from_dt"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                lparam.Add(new cArrayList("@to_dt", System.DateTime.ParseExact(Request.QueryString["to_dt"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                vShowReportWithParameter(ref crviewer, "rp_transfercustomerAllApp.rpt", lparam);
                break;
            case "MerchindiserVisit":
                lparam.Add(new cArrayList("@emp_cd", Convert.ToString(Request.QueryString["emp"])));
                lparam.Add(new cArrayList("@period_cd", Convert.ToString(Request.QueryString["period"])));
                vShowReport(ref crviewer, "rp_merchandiser_visit.rpt", lformula, lparam);
                break;
            case "employeeAppAllbrn":
                lparam.Add(new cArrayList("@salespointcd", Convert.ToString(Request.QueryString["sp"])));
                vShowReport(ref crviewer, "rp_employee_app_all.rpt", lformula, lparam);
                break;
            case "employeeAppAll":
                lparam.Add(new cArrayList("@salespointcd", null));
                vShowReport(ref crviewer, "rp_employee_app_all.rpt", lformula, lparam);
                break;
            case "IncentiveMerch":
                lparam.Add(new cArrayList("@emp_cd", Convert.ToString(Request.QueryString["emp"])));
                lparam.Add(new cArrayList("@period", Convert.ToString(Request.QueryString["period"])));
                lparam.Add(new cArrayList("@salespointcd", Convert.ToString(Request.QueryString["sp"])));
                lparam.Add(new cArrayList("@job_title_cd", Convert.ToString(Request.QueryString["job_title_cd"])));
                lparam.Add(new cArrayList("@level_cd", Convert.ToString(Request.QueryString["level_cd"])));
                if (Convert.ToString(Request.QueryString["sp"]) == "201" || Convert.ToString(Request.QueryString["sp"]) == "208" || Convert.ToString(Request.QueryString["sp"]) == "204" || Convert.ToString(Request.QueryString["sp"]) == "209" || Convert.ToString(Request.QueryString["sp"]) == "202")
                {
                    if (Convert.ToString(Request.QueryString["job_title_cd"]) == "22")
                    {
                        vShowReport(ref crviewer, "rp_hrdkpiIncentive_Merch_TL.rpt", lformula, lparam);
                    }
                    else
                    {
                        //vShowReport(ref crviewer, "rp_hrdkpiIncentive_Merch_Lukman.rpt", lformula, lparam);
                        vShowReport(ref crviewer, "rp_hrdkpiIncentive_Merch_Lukman_Vr_2.rpt", lformula, lparam);
                    }
                }
                else
                {
                    if (Convert.ToString(Request.QueryString["job_title_cd"]) == "22")
                    {
                        vShowReport(ref crviewer, "rp_hrdkpiIncentive_Merch_TL.rpt", lformula, lparam);
                    }
                    else
                    {
                        //vShowReport(ref crviewer, "rp_hrdkpiIncentive_Merch.rpt", lformula, lparam);
                        vShowReport(ref crviewer, "rp_hrdkpiIncentive_Merch_Vr_2.rpt", lformula, lparam);
                    }
                }
                break;
            case "PerEvalMerch":
                lparam.Add(new cArrayList("@emp_cd", Convert.ToString(Request.QueryString["emp"])));
                lparam.Add(new cArrayList("@period", Convert.ToString(Request.QueryString["period"])));
                lparam.Add(new cArrayList("@salespointcd", Convert.ToString(Request.QueryString["sp"])));
                lparam.Add(new cArrayList("@job_title_cd", Convert.ToString(Request.QueryString["job_title_cd"])));
                lparam.Add(new cArrayList("@level_cd", Convert.ToString(Request.QueryString["level_cd"])));
                //vShowReport(ref crviewer, "rp_hrdkpiPer_Eval_Merch.rpt", lformula, lparam);
                vShowReport(ref crviewer, "rp_hrdkpiPer_Eval_Merch_Vr_2.rpt", lformula, lparam);
                break;
            case "employeeBranchApp":
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                if ((Request.QueryString["emp"]) == null)
                {
                    lparam.Add(new cArrayList("@emp_cd", null));
                    lparam.Add(new cArrayList("@employee_app_cd", Convert.ToString(Request.QueryString["rec"])));
                }
                else
                {
                    lparam.Add(new cArrayList("@emp_cd", Convert.ToString(Request.QueryString["emp"])));
                    lparam.Add(new cArrayList("@employee_app_cd", null));
                }
                vShowReport(ref crviewer, "rp_mst_employeeBranchApp.rpt", lformula, lparam);
                break;
            case "acccndnCustomTot":
                DateTime cndnFromt = DateTime.Now;
                DateTime cndnTot = DateTime.Now;
                DateTime postFromt = DateTime.Now;
                DateTime postTot = DateTime.Now;


                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));

                if (Request.QueryString["postFrom"] != null)
                {
                    postFromt = DateTime.ParseExact(Request.QueryString["postFrom"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                }
                if (Request.QueryString["postTo"] != null)
                {
                    postTot = DateTime.ParseExact(Request.QueryString["postTo"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                }
                if (Request.QueryString["cndnFrom"] != null)
                {
                    cndnFromt = DateTime.ParseExact(Request.QueryString["cndnFrom"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                }
                if (Request.QueryString["cndnTo"] != null)
                {
                    cndnTot = DateTime.ParseExact(Request.QueryString["cndnTo"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                }

                if (Convert.ToString(Request.QueryString["query"]) == "1")
                {
                    lparam.Add(new cArrayList("@query", 1));
                    lparam.Add(new cArrayList("@postFrom", postFromt.Year + " - " + postFromt.Month + " - " + postFromt.Day));
                    lparam.Add(new cArrayList("@postTo", postTot.Year + " - " + postTot.Month + " - " + postTot.Day));
                    lparam.Add(new cArrayList("@cndnFrom", null));
                    lparam.Add(new cArrayList("@cndnTo", null));
                    lparam.Add(new cArrayList("@cust_cd", null));
                    lparam.Add(new cArrayList("@cndn_cd", null));

                }
                else if (Convert.ToString(Request.QueryString["query"]) == "2")
                {
                    lparam.Add(new cArrayList("@query", 2));
                    lparam.Add(new cArrayList("@cndnFrom", cndnFromt.Year + " - " + cndnFromt.Month + " - " + cndnFromt.Day));
                    lparam.Add(new cArrayList("@cndnTo", cndnTot.Year + " - " + cndnTot.Month + " - " + cndnTot.Day));
                    lparam.Add(new cArrayList("@postFrom", null));
                    lparam.Add(new cArrayList("@postTo", null));
                    lparam.Add(new cArrayList("@cust_cd", null));
                    lparam.Add(new cArrayList("@cndn_cd", null));
                }
                else if (Convert.ToString(Request.QueryString["query"]) == "3")
                {
                    lparam.Add(new cArrayList("@query", 3));
                    lparam.Add(new cArrayList("@cust_cd", Convert.ToString(Request.QueryString["cust_cd"])));
                    lparam.Add(new cArrayList("@cndnFrom", null));
                    lparam.Add(new cArrayList("@cndnTo", null));
                    lparam.Add(new cArrayList("@postFrom", null));
                    lparam.Add(new cArrayList("@postTo", null));
                    lparam.Add(new cArrayList("@cndn_cd", null));
                }
                else if (Convert.ToString(Request.QueryString["query"]) == "4")
                {
                    lparam.Add(new cArrayList("@query", 4));
                    lparam.Add(new cArrayList("@cndn_cd", Convert.ToString(Request.QueryString["cndn_cd"])));
                    lparam.Add(new cArrayList("@cndnFrom", null));
                    lparam.Add(new cArrayList("@cndnTo", null));
                    lparam.Add(new cArrayList("@postFrom", null));
                    lparam.Add(new cArrayList("@postTo", null));
                    lparam.Add(new cArrayList("@cust_cd", null));
                }
                vShowReport(ref crviewer, "rp_acccndnadjCustomWithTotal.rpt", lformula, lparam);
                break;
            case "returhoreport":
                List<cArrayList> lPreturhoreport = (List<cArrayList>)Session["lPreturhoreport"];
                vShowReportWithParameter(ref crviewer, "rp_" + sSrc + ".rpt", lPreturhoreport);
                break;
            case "DLCR":
                lparam.Clear();
                string DLCR_from_str = Request.QueryString["date_from"];
                string DLCR_to_str = Request.QueryString["date_to"];
                DateTime DLCR_from = DateTime.ParseExact(DLCR_from_str, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime DLCR_to = DateTime.ParseExact(DLCR_to_str, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                lparam.Add(new cArrayList("Branch", Convert.ToString(Request.Cookies["spn"].Value)));
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                lparam.Add(new cArrayList("@date_from", DLCR_from));
                lparam.Add(new cArrayList("@date_to", DLCR_to));
                vShowReportWithParameter(ref crviewer, "rp_DriverLoadingCustReceiveTrans_Raw.rpt", lparam);
                break;
            case "rtntot":
                lparam.Add(new cArrayList("p_user", "SYS"));
                vShowReportWithParameter(ref crviewer, "rp_salesreturn3.rpt", lparam);
                break;
            case "custnotbuy":
                List<cArrayList> lParamcustnotbuy = (List<cArrayList>)Session["lParamcusrp"];
                vShowReportWithParameter(ref crviewer, "rp_customernotbuy.rpt", lParamcustnotbuy);
                break;
            case "itemmerchan":
                vShowReport(ref crviewer, "rp_itemmerchan.rpt");
                break;
            case "merchduration":
                lparam.Add(new cArrayList("@start_dt", System.DateTime.ParseExact(Request.QueryString["start_dt"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                lparam.Add(new cArrayList("@end_dt", System.DateTime.ParseExact(Request.QueryString["end_dt"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                vShowReportWithParameter(ref crviewer, "rp_merchanduration.rpt", lparam);
                break;
            case "stockcustomerne":
                lparam.Add(new cArrayList("@start_dt", System.DateTime.ParseExact(Request.QueryString["start_dt"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                lparam.Add(new cArrayList("@end_dt", System.DateTime.ParseExact(Request.QueryString["end_dt"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                vShowReportWithParameter(ref crviewer, "rp_stockcustomerne.rpt", lparam);
                break;
            case "merchanreport":
                lparam.Add(new cArrayList("@emp_cd", Request.QueryString["emp"]));
                lparam.Add(new cArrayList("@prod_cd", Request.QueryString["prod"]));
                lparam.Add(new cArrayList("@visit_dt", System.DateTime.ParseExact(Request.QueryString["dt"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                vShowReportWithParameter(ref crviewer, "rp_merchandiserstock.rpt", lparam);
                break;
            case "customerstock":
                lparam.Add(new cArrayList("@emp_cd", Request.QueryString["emp"]));
                lparam.Add(new cArrayList("@prod_cd", Request.QueryString["prod"]));
                lparam.Add(new cArrayList("@visit_dt", System.DateTime.ParseExact(Request.QueryString["dt"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                vShowReportWithParameter(ref crviewer, "rp_customerstock1.rpt", lparam);
                break;
            case "CNDNNormal":
                lparam.Clear();
                string postCNDNFrom = string.Empty;//DateTime.Now.AddMonths(-3) ;
                string postCNDNSalesTo = string.Empty;//DateTime.Now;
                if (Request.QueryString["cndnFrom"] != null)
                {
                    postCNDNFrom = DateTime.ParseExact(Request.QueryString["cndnFrom"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString();
                }
                if (Request.QueryString["cndnTo"] != null)
                {
                    postCNDNSalesTo = DateTime.ParseExact(Request.QueryString["cndnTo"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString();
                }
                lparam.Add(new cArrayList("@cndnFrom", postCNDNFrom));
                lparam.Add(new cArrayList("@cndnTo", postCNDNSalesTo));
                lparam.Add(new cArrayList("@cust_cd", null));
                lparam.Add(new cArrayList("@salespointcd", Convert.ToString(Request.Cookies["sp"].Value)));
                lparam.Add(new cArrayList("p_user", Convert.ToString(Request.Cookies["usr_id"].Value.ToString())));
                vShowReport(ref crviewer, "rpt_CNDN.rpt", lformula, lparam);
                break;
            case "DiscountSales":

                string postDiscountSalesFrom = string.Empty;//DateTime.Now.AddMonths(-3) ;
                string postDiscountSalesTo = string.Empty;//DateTime.Now;
                if (Request.QueryString["cndnFrom"] != null)
                {
                    postDiscountSalesFrom = DateTime.ParseExact(Request.QueryString["cndnFrom"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString();
                }
                if (Request.QueryString["cndnTo"] != null)
                {
                    postDiscountSalesTo = DateTime.ParseExact(Request.QueryString["cndnTo"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString();
                }

                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                lparam.Add(new cArrayList("@postFrom", postDiscountSalesFrom));
                lparam.Add(new cArrayList("@postTo", postDiscountSalesTo));
                lparam.Add(new cArrayList("@inv_no", "NA"));
                lparam.Add(new cArrayList("@Case", 1));
                lparam.Add(new cArrayList("@salespointcd", Convert.ToString(Request.Cookies["sp"].Value)));
                vShowReportWithParameter(ref crviewer, "rp_DiscountSales.rpt", lparam, lformula);
                break;

            case "DriverIncentiveSummary":


                string summaryPeriod = Request.Cookies["incentiveSummaryPeriod"].Value.ToString();
                string summaryBranch = Request.Cookies["incentiveSummaryBranch"].Value.ToString();
                string summaryEmp = Request.Cookies["incentiveSummaryEmp"].Value.ToString();

                lparam.Add(new cArrayList("@period", summaryPeriod));


                if (summaryBranch == "") { lparam.Add(new cArrayList("@salespointcd", null)); }
                else { lparam.Add(new cArrayList("@salespointcd", summaryBranch)); }

                if (summaryEmp == "") { lparam.Add(new cArrayList("@employee", null)); }
                else { lparam.Add(new cArrayList("@employee", summaryEmp)); }
                //the formula in the report file Has been updated to accommodate the no more 100% achievement for all kpi 
                vShowReportWithParameter(ref crviewer, "rp_DriverIncentiveSummary.rpt", lparam);
                //vShowReport(ref crviewer, "rp_DriverIncentiveSummary.rpt", lformula, lparam);

                break;
            case "reqdiscount":
                lparam.Clear();
                lparam.Add(new cArrayList("@disc_cd", Request.QueryString["disc"]));
                if (Request.QueryString["type"] == "BB")
                { vShowReportWithParameter(ref crviewer, "rp_requestdiscountBB.rpt", lparam); }
                else { vShowReportWithParameter(ref crviewer, "rp_requestdiscountPD.rpt", lparam); }
                break;
            case "cndnCustomerCustom":
                DateTime cndnFrom1 = DateTime.Now;
                DateTime cndnTo1 = DateTime.Now;
                DateTime postFrom1 = DateTime.Now;
                DateTime postTo1 = DateTime.Now;

                string cndnType = Request.QueryString["cndnType"].ToString();
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));

                if (Request.QueryString["postFrom"] != null)
                {
                    postFrom1 = DateTime.ParseExact(Request.QueryString["postFrom"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                }
                if (Request.QueryString["postTo"] != null)
                {
                    postTo1 = DateTime.ParseExact(Request.QueryString["postTo"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                }
                if (Request.QueryString["cndnFrom"] != null)
                {
                    cndnFrom1 = DateTime.ParseExact(Request.QueryString["cndnFrom"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                }
                if (Request.QueryString["cndnTo"] != null)
                {
                    cndnTo1 = DateTime.ParseExact(Request.QueryString["cndnTo"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                }

                if (Convert.ToString(Request.QueryString["query"]) == "1")
                {
                    lparam.Add(new cArrayList("@query", 1));
                    lparam.Add(new cArrayList("@cndnFrom", cndnFrom1.Year + " - " + cndnFrom1.Month + " - " + cndnFrom1.Day));
                    lparam.Add(new cArrayList("@cndnTo", cndnTo1.Year + " - " + cndnTo1.Month + " - " + cndnTo1.Day));

                    lparam.Add(new cArrayList("@postFrom", null));
                    lparam.Add(new cArrayList("@postTo", null));
                    lparam.Add(new cArrayList("@cust_cd", null));
                    lparam.Add(new cArrayList("@cndn_no", null));
                }
                else if (Convert.ToString(Request.QueryString["query"]) == "3")
                {
                    lparam.Add(new cArrayList("@query", 3));
                    lparam.Add(new cArrayList("@cust_cd", Convert.ToString(Request.QueryString["cust_cd"])));
                    lparam.Add(new cArrayList("@cndnFrom", null));
                    lparam.Add(new cArrayList("@cndnTo", null));
                    lparam.Add(new cArrayList("@postFrom", null));
                    lparam.Add(new cArrayList("@postTo", null));
                    lparam.Add(new cArrayList("@cndn_no", null));
                }
                else if (Convert.ToString(Request.QueryString["query"]) == "3")
                {
                    lparam.Add(new cArrayList("@query", 3));
                    lparam.Add(new cArrayList("@cndn_no", Convert.ToString(Request.QueryString["cndn_no"])));
                    lparam.Add(new cArrayList("@cndnFrom", null));
                    lparam.Add(new cArrayList("@cndnTo", null));
                    lparam.Add(new cArrayList("@postFrom", null));
                    lparam.Add(new cArrayList("@postTo", null));
                    lparam.Add(new cArrayList("@cust_cd", null));
                }
                if (cndnType == "sort")
                {
                    vShowReport(ref crviewer, "rp_cndnCustomerCustom.rpt", lformula, lparam);
                }
                else if (cndnType == "details")
                {
                    vShowReport(ref crviewer, "rp_cndnCustomerCustomTable.rpt", lformula, lparam);
                }
                break;
            case "stockcust":
            case "MERCHANDISERJARED":
                lparam.Add(new cArrayList("@cs_dt", System.DateTime.ParseExact(Request.QueryString["dt"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                //vShowReportWithParameter(ref crviewer, "rp_stockcustomer.rpt", lparam);
                vShowReportWithParameter(ref crviewer, "rp_" + sSrc + ".rpt", lparam);
                break;
            case "merchanvisitall":
                lparam.Add(new cArrayList("@visit_dt", System.DateTime.ParseExact(Request.QueryString["dt"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                lparam.Add(new cArrayList("@end_dt", System.DateTime.ParseExact(Request.QueryString["end_dt"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                vShowReportWithParameter(ref crviewer, "rp_merchanvisitall.rpt", lparam);
                break;
            case "merchanvisit":
                lparam.Add(new cArrayList("@emp_cd", Request.QueryString["emp"]));
                lparam.Add(new cArrayList("@visit_dt", System.DateTime.ParseExact(Request.QueryString["dt"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                lparam.Add(new cArrayList("@end_dt", System.DateTime.ParseExact(Request.QueryString["end_dt"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                vShowReportWithParameter(ref crviewer, "rp_merchanvisit.rpt", lparam);
                break;
            case "cndnCustomer":
                lparam.Add(new cArrayList("@salespointcd", Convert.ToString(Request.Cookies["sp"].Value.ToString())));
                lparam.Add(new cArrayList("@cndn_no", Convert.ToString(Request.QueryString["cndn_no"])));
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                vShowReport(ref crviewer, "rp_cndnCustomer.rpt", lformula, lparam);
                break;
            case "rp":
                lparam.Add(new cArrayList("@emp_cd", Request.QueryString["emp"]));
                lparam.Add(new cArrayList("@day_cd", Request.QueryString["da"]));
                lparam.Add(new cArrayList("salespointcd", Request.Cookies["sp"].Value.ToString()));
                vShowReportWithParameter(ref crviewer, "rp_rps.rpt", lparam);
                break;
            case "hrdkpiBSV_ValidationSheet":
                lparam.Add(new cArrayList("@period", Convert.ToString(Request.QueryString["period"])));
                lparam.Add(new cArrayList("@salespointcd", Convert.ToString(Request.QueryString["sp"])));
                vShowReport(ref crviewer, "rp_ValidationSheetBSV.rpt", lformula, lparam);
                break;
            case "hrdkpiBSV_Incentive":
                lparam.Add(new cArrayList("@emp_cdBSV", Convert.ToString(Request.QueryString["emp"])));
                lparam.Add(new cArrayList("@period", Convert.ToString(Request.QueryString["period"])));
                lparam.Add(new cArrayList("@salespointcd", Convert.ToString(Request.QueryString["sp"])));
                vShowReport(ref crviewer, "rp_hrdkpiIncentiveBSV1.rpt", lformula, lparam);
                break;
            case "hrdkpiBSV_Pa":
                lparam.Add(new cArrayList("@emp_cdBSV", Convert.ToString(Request.QueryString["emp"])));
                lparam.Add(new cArrayList("@period", Convert.ToString(Request.QueryString["period"])));
                lparam.Add(new cArrayList("@salespointcd", Convert.ToString(Request.QueryString["sp"])));
                vShowReport(ref crviewer, "rp_hrdkpiBSV.rpt", lformula, lparam);
                break;
            case "DriverHisDt":
                string sdt1 = Request.QueryString["dtstart"];
                string edt1 = Request.QueryString["dtend"];
                DateTime dts1 = DateTime.ParseExact(sdt1, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime dte1 = DateTime.ParseExact(edt1, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                lparam.Add(new cArrayList("@driver_cd", Convert.ToString(Request.QueryString["emp"])));
                lparam.Add(new cArrayList("@dtstart", dts1.Year + " - " + dts1.Month + " - " + dts1.Day));
                lparam.Add(new cArrayList("@dtend", dte1.Year + " - " + dte1.Month + " - " + dte1.Day));
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                vShowReport(ref crviewer, "rp_driverHisDt.rpt", lformula, lparam);
                break;
            case "DriverHisAllDriverDt":
                string sdtall = Request.QueryString["dtstart"];
                string edtall = Request.QueryString["dtend"];
                string isActive = Request.QueryString["isActive"];
                DateTime dtsall = DateTime.ParseExact(sdtall, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime dteall = DateTime.ParseExact(edtall, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                lparam.Add(new cArrayList("@dtstart", dtsall.Year + " - " + dtsall.Month + " - " + dtsall.Day));
                lparam.Add(new cArrayList("@dtend", dteall.Year + " - " + dteall.Month + " - " + dteall.Day));
                lparam.Add(new cArrayList("@isActive", isActive));
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));

                vShowReport(ref crviewer, "rp_driverHisAllDriverDt.rpt", lformula, lparam);
                break;
            case "rp_hrdkpiIncentiveBSVRaw":
                lparam.Add(new cArrayList("@emp_cdBSV", Convert.ToString(Request.QueryString["emp"])));
                lparam.Add(new cArrayList("@period", Convert.ToString(Request.QueryString["period"])));
                lparam.Add(new cArrayList("@salespointcd", Convert.ToString(Request.QueryString["sp"])));
                vShowReport(ref crviewer, "rp_hrdkpiIncentiveBSVRaw.rpt", lformula, lparam);
                break;
            case "agclaimrep":
                lparam.Clear();
                lparam.Add(new cArrayList("@type", Request.QueryString["type"]));
                lparam.Add(new cArrayList("@dtstart", Request.QueryString["dtstart"]));
                lparam.Add(new cArrayList("@dtend", Request.QueryString["dtend"]));
                vShowReportWithParameter(ref crviewer, "rp_agreementrep.rpt", lparam);
                break;
            case "cashoutclaimrep":
                lparam.Clear();
                lparam.Add(new cArrayList("@word", null));
                lparam.Add(new cArrayList("@dtstart", Convert.ToDateTime(Request.QueryString["dtstart"])));
                lparam.Add(new cArrayList("@dtend", Convert.ToDateTime(Request.QueryString["dtend"])));
                vShowReportWithParameter(ref crviewer, "rp_cashoutclaimrep.rpt", lparam);
                break;
            case "cndnclaimrep":
                lparam.Clear();
                lparam.Add(new cArrayList("@cndn_cd", null));
                lparam.Add(new cArrayList("@word", null));
                lparam.Add(new cArrayList("@dtstart", Convert.ToDateTime(Request.QueryString["dtstart"])));
                lparam.Add(new cArrayList("@dtend", Convert.ToDateTime(Request.QueryString["dtend"])));
                vShowReportWithParameter(ref crviewer, "rp_cndnclaimrep.rpt", lparam);
                break;
            case "acccndnCustom":
                //string postFrom = string.Empty;
                //string postTo = string.Empty;
                //string cndnFrom = string.Empty;
                //string cndnTo = string.Empty;

                DateTime cndnFrom = DateTime.Now;
                DateTime cndnTo = DateTime.Now;
                DateTime postFrom = DateTime.Now;
                DateTime postTo = DateTime.Now;


                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));

                if (Request.QueryString["postFrom"] != null)
                {
                    //postFrom = DateTime.ParseExact(Request.QueryString["postFrom"], "mm/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString();
                    postFrom = DateTime.ParseExact(Request.QueryString["postFrom"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                }
                if (Request.QueryString["postTo"] != null)
                {
                    //postTo = DateTime.ParseExact(Request.QueryString["postTo"], "mm/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString();
                    postTo = DateTime.ParseExact(Request.QueryString["postTo"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                }
                if (Request.QueryString["cndnFrom"] != null)
                {
                    cndnFrom = DateTime.ParseExact(Request.QueryString["cndnFrom"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    //cndnFrom = DateTime.ParseExact(Request.QueryString["cndnFrom"], "mm/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString();
                }
                if (Request.QueryString["cndnTo"] != null)
                {
                    cndnTo = DateTime.ParseExact(Request.QueryString["cndnTo"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    //cndnTo = DateTime.ParseExact(Request.QueryString["cndnTo"], "mm/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString();
                }

                if (Convert.ToString(Request.QueryString["query"]) == "1")
                {
                    lparam.Add(new cArrayList("@query", 1));
                    //lparam.Add(new cArrayList("@postFrom", postFrom));
                    //lparam.Add(new cArrayList("@postTo", postTo));
                    lparam.Add(new cArrayList("@postFrom", postFrom.Year + " - " + postFrom.Month + " - " + postFrom.Day));
                    lparam.Add(new cArrayList("@postTo", postTo.Year + " - " + postTo.Month + " - " + postTo.Day));
                    //lparam.Add(new cArrayList("@postFrom", postFrom.Year.ToString() + "," + postFrom.Month.ToString("00") + "," + postFrom.Day.ToString("00")));
                    //lparam.Add(new cArrayList("@postTo", postTo.Year.ToString() + "," + postTo.Month.ToString("00") + "," + postTo.Day.ToString("00")));

                    lparam.Add(new cArrayList("@cndnFrom", null));
                    lparam.Add(new cArrayList("@cndnTo", null));
                    lparam.Add(new cArrayList("@cust_cd", null));
                    lparam.Add(new cArrayList("@cndn_cd", null));

                }
                else if (Convert.ToString(Request.QueryString["query"]) == "2")
                {
                    lparam.Add(new cArrayList("@query", 2));
                    //lparam.Add(new cArrayList("@cndnFrom", cndnFrom));
                    //lparam.Add(new cArrayList("@cndnTo", cndnTo));
                    lparam.Add(new cArrayList("@cndnFrom", cndnFrom.Year + " - " + cndnFrom.Month + " - " + cndnFrom.Day));
                    lparam.Add(new cArrayList("@cndnTo", cndnTo.Year + " - " + cndnTo.Month + " - " + cndnTo.Day));

                    //lparam.Add(new cArrayList("@cndnFrom", cndnFrom.Year.ToString() + "," + cndnFrom.Month.ToString("00") + "," + cndnFrom.Day.ToString("00")));
                    //lparam.Add(new cArrayList("@cndnTo", cndnTo.Year.ToString() + "," + cndnTo.Month.ToString("00") + "," + cndnTo.Day.ToString("00")));

                    lparam.Add(new cArrayList("@postFrom", null));
                    lparam.Add(new cArrayList("@postTo", null));
                    lparam.Add(new cArrayList("@cust_cd", null));
                    lparam.Add(new cArrayList("@cndn_cd", null));
                }
                else if (Convert.ToString(Request.QueryString["query"]) == "3")
                {
                    lparam.Add(new cArrayList("@query", 3));
                    lparam.Add(new cArrayList("@cust_cd", Convert.ToString(Request.QueryString["cust_cd"])));
                    lparam.Add(new cArrayList("@cndnFrom", null));
                    lparam.Add(new cArrayList("@cndnTo", null));
                    lparam.Add(new cArrayList("@postFrom", null));
                    lparam.Add(new cArrayList("@postTo", null));
                    lparam.Add(new cArrayList("@cndn_cd", null));
                }
                else if (Convert.ToString(Request.QueryString["query"]) == "4")
                {
                    lparam.Add(new cArrayList("@query", 4));
                    lparam.Add(new cArrayList("@cndn_cd", Convert.ToString(Request.QueryString["cndn_cd"])));
                    lparam.Add(new cArrayList("@cndnFrom", null));
                    lparam.Add(new cArrayList("@cndnTo", null));
                    lparam.Add(new cArrayList("@postFrom", null));
                    lparam.Add(new cArrayList("@postTo", null));
                    lparam.Add(new cArrayList("@cust_cd", null));
                }
                //lparam.Add(new cArrayList("@postFrom", DateTime.ParseExact(Request.QueryString["postFrom"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString();
                //lparam.Add(new cArrayList("@postTo", DateTime.ParseExact(Request.QueryString["postTo"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString();
                //lparam.Add(new cArrayList("@cndnFrom", DateTime.ParseExact(Request.QueryString["cndnFrom"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString();
                //lparam.Add(new cArrayList("@cndnTo", DateTime.ParseExact(Request.QueryString["cndnTo"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString();
                //lparam.Add(new cArrayList("@cust_cd", Convert.ToString(Request.Cookies["cust_cd"].Value)));
                //lparam.Add(new cArrayList("@cndn_cd", Convert.ToString(Request.Cookies["cndn_cd"].Value)));



                vShowReport(ref crviewer, "rp_acccndnadjCustom.rpt", lformula, lparam);
                break;
            case "rp_hrdkpiIncentiveBSV":
                lparam.Add(new cArrayList("@emp_cdBSV", Convert.ToString(Request.QueryString["emp"])));
                lparam.Add(new cArrayList("@period", Convert.ToString(Request.QueryString["period"])));
                lparam.Add(new cArrayList("@salespointcd", Convert.ToString(Request.QueryString["sp"])));
                vShowReport(ref crviewer, "rp_hrdkpiIncentiveBSV1.rpt", lformula, lparam);
                //vShowReport(ref crviewer, "rp_hrdkpiIncentiveBSV.rpt", lformula, lparam);
                break;
            case "histpr":
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                vShowReportWithParameter(ref crviewer, "rptcustomertypeprice_hist.rpt", lparam);
                break;
            case "adjpr":
                lparam.Add(new cArrayList("@note", Request.QueryString["note"]));
                if (Request.QueryString["cust"] == "C")
                {
                    vShowReportWithParameter(ref crviewer, "rpt_adjustmentprice_c.rpt", lparam);
                }
                else
                {
                    vShowReportWithParameter(ref crviewer, "rpt_adjustmentprice_g.rpt", lparam);
                }
                break;
            case "darowclaimdetail":
                lparam.Clear();
                lparam.Add(new cArrayList("@year", Request.QueryString["year"]));
                vShowReportWithParameter(ref crviewer, "rp_datarowclaimdetail.rpt", lparam);
                break;
            case "advancedemp":
                string sEmpCode = Request.QueryString["emp"];
                string sStartDate = "1/1/2018"; //DateTime.ParseExact(Request.QueryString["start_dt"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString();
                string sEndDate = DateTime.ParseExact(Request.QueryString["end_dt"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString();
                lparam.Add(new cArrayList("@emp_cd", sEmpCode));
                lparam.Add(new cArrayList("@start_dt", Convert.ToDateTime(sStartDate)));
                lparam.Add(new cArrayList("@end_dt", Convert.ToDateTime(sEndDate)));
                //lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                //lparam.Add(new cArrayList("openingBalance", Request.QueryString["openingBalance"]));
                vShowReportWithParameter(ref crviewer, "rp_employeeadvanced.rpt", lparam);
                break;
            case "cndnemp":
                lparam.Clear();
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                lparam.Add(new cArrayList("openingBalance", Request.QueryString["openingBalance"]));
                lparam.Add(new cArrayList("@cndn_no", Request.QueryString["id"]));
                vShowReportWithParameter(ref crviewer, "rp_cndnemployee.rpt", lparam);
                break;
            case "supplier":
                vShowReport(ref crviewer, "sp_accsupplier.rpt");
                break;
            case "PettyCashRAW":
                lparam.Clear();
                lparam.Add(new cArrayList("Branch", (Convert.ToString(Request.Cookies["sp"].Value + " - " + Convert.ToString(Request.Cookies["spn"].Value)))));
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                lparam.Add(new cArrayList("@startdate", Request.QueryString["date_from"]));
                lparam.Add(new cArrayList("@enddate", Request.QueryString["date_to"]));
                vShowReportWithParameter(ref crviewer, "rp_PettyCashRAW.rpt", lparam);
                break;

            case "AdvancePettyCashRAW":
                lparam.Clear();
                lparam.Add(new cArrayList("Branch", (Convert.ToString(Request.Cookies["sp"].Value + " - " + Convert.ToString(Request.Cookies["spn"].Value)))));
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                lparam.Add(new cArrayList("@startdate", Request.QueryString["date_from"]));
                lparam.Add(new cArrayList("@enddate", Request.QueryString["date_to"]));
                vShowReportWithParameter(ref crviewer, "rp_AdvancePettyCashRAW.rpt", lparam);
                break;
            case "CashInCashOutRAW":
                lparam.Clear();
                lparam.Add(new cArrayList("Branch", (Convert.ToString(Request.Cookies["sp"].Value + " - " + Convert.ToString(Request.Cookies["spn"].Value)))));
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                lparam.Add(new cArrayList("@startdate", Request.QueryString["date_from"]));
                lparam.Add(new cArrayList("@enddate", Request.QueryString["date_to"]));
                vShowReportWithParameter(ref crviewer, "rp_CashinCashoutRAW.rpt", lparam);
                break;
            case "PettyCash":
                lparam.Clear();
                lparam.Add(new cArrayList("@salespointcd", Convert.ToString(Request.Cookies["sp"].Value)));
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                lparam.Add(new cArrayList("@startdate", Request.QueryString["date_from"]));
                lparam.Add(new cArrayList("@enddate", Request.QueryString["date_to"]));
                lparam.Add(new cArrayList("@emp_cd", Request.QueryString["Emp_CD"]));
                vShowReportWithParameter(ref crviewer, "rp_CashierPettyCash.rpt", lparam);
                break;
            case "DOI":
                lparam.Clear();
                string DOI_from_str = Request.QueryString["date_from"];
                string DOI_to_str = Request.QueryString["date_to"];
                DateTime DOI_from = DateTime.ParseExact(DOI_from_str, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime DOI_to = DateTime.ParseExact(DOI_to_str, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                lparam.Add(new cArrayList("Branch", Convert.ToString(Request.Cookies["spn"].Value)));
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                lparam.Add(new cArrayList("@date_from", DOI_from));
                lparam.Add(new cArrayList("@date_to", DOI_to));
                vShowReportWithParameter(ref crviewer, "rp_DeliveryOrderInvoice_Raw.rpt", lparam);
                break;
            case "DODI":
                lparam.Clear();
                string DODI_from_str = Request.QueryString["date_from"];
                string DODI_to_str = Request.QueryString["date_to"];
                DateTime DODI_from = DateTime.ParseExact(DODI_from_str, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime DODI_to = DateTime.ParseExact(DODI_to_str, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                lparam.Add(new cArrayList("Branch", Convert.ToString(Request.Cookies["spn"].Value)));
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                lparam.Add(new cArrayList("@date_from", DODI_from));
                lparam.Add(new cArrayList("@date_to", DODI_to));
                vShowReportWithParameter(ref crviewer, "rp_DeliveryOrderDriverInvoice_Raw.rpt", lparam);
                break;
            case "OD":
                lparam.Clear();
                string OD_from_str = Request.QueryString["date_from"];
                string OD_to_str = Request.QueryString["date_to"];
                DateTime OD_from = DateTime.ParseExact(OD_from_str, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime OD_to = DateTime.ParseExact(OD_to_str, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                lparam.Add(new cArrayList("Branch", Convert.ToString(Request.Cookies["spn"].Value)));
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                lparam.Add(new cArrayList("@date_from", OD_from));
                lparam.Add(new cArrayList("@date_to", OD_to));
                vShowReportWithParameter(ref crviewer, "rp_DriverOtherDelivery_Raw.rpt", lparam);
                break;
            case "AllCollectionByDate":
                lparam.Clear();
                //sp_rptPaymentAll
                lparam.Add(new cArrayList("@from", Request.QueryString["from"]));
                lparam.Add(new cArrayList("@to", Request.QueryString["to"]));
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                vShowReportWithParameter(ref crviewer, "rp_PaymentUnpaidAllV2.rpt", lparam);
                break;
            case "py":
                //lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                lparam.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                lparam.Add(new cArrayList("@payment_no", Request.QueryString["noy"]));
                lparam.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                vShowReportWithParameter(ref crviewer, "rp_paymentreceipt2.rpt", lparam);
                break;

            case "customernotbuy":
                List<cArrayList> lParamlistcustnotbuy = (List<cArrayList>)Session["lParamlistcustnotbuy"];
                vShowReportWithParameter(ref crviewer, "rpt" + sSrc + ".rpt", lParamlistcustnotbuy);
                break;
            case "darowagho":
                lparam.Add(new cArrayList("@month", Request.QueryString["month"]));
                lparam.Add(new cArrayList("@year", Request.QueryString["year"]));
                string rawtyp = Request.QueryString["type"];
                if (rawtyp == "0")
                {
                    vShowReportWithParameter(ref crviewer, "rp_aghodataraw.rpt", lparam);
                }
                else if (rawtyp == "1")
                {
                    vShowReportWithParameter(ref crviewer, "rp_cmhodataraw.rpt", lparam);
                }
                break;
            case "vatinvlogbook":
                lparam.Clear();
                lparam.Add(new cArrayList("@salespoint", Request.QueryString["sp"]));
                lparam.Add(new cArrayList("@year", Request.QueryString["yr"]));
                lparam.Add(new cArrayList("@month", Request.QueryString["mt"]));
                vShowReportWithParameter(ref crviewer, "rp_claimvatinvoice.rpt", lparam);
                break;

            case "PaymentCashDeposit":
                lparam.Clear();
                lparam.Add(new cArrayList("@from", Request.QueryString["from"]));
                lparam.Add(new cArrayList("@to", Request.QueryString["to"]));
                vShowReportWithParameter(ref crviewer, "rp_PaymentCashDeposit.rpt", lparam);
                break;
            case "PaymentBank":
                lparam.Clear();
                lparam.Add(new cArrayList("@from", Request.QueryString["from"]));
                lparam.Add(new cArrayList("@to", Request.QueryString["to"]));
                vShowReportWithParameter(ref crviewer, "rp_PaymentBank.rpt", lparam);
                break;
            case "PaymentUnpaid":
                lparam.Clear();
                lparam.Add(new cArrayList("@from", Request.QueryString["from"]));
                lparam.Add(new cArrayList("@to", Request.QueryString["to"]));
                vShowReportWithParameter(ref crviewer, "rp_PaymentUnpaid.rpt", lparam);
                break;
            case "BrnStkMonitorRpt":
                List<cArrayList> lparamBrnStkMonitorRpt = (List<cArrayList>)Session["lparamBrnStkMonitorRpt"];
                vShowReportWithParameter(ref crviewer, "rptBranchStockMonitoring.rpt", lparamBrnStkMonitorRpt);
                break;
            case "EmpTrxn":
                lparam.Clear();
                lparam.Add(new cArrayList("@ids", Request.QueryString["ids"]));
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                lparam.Add(new cArrayList("Branch", Convert.ToString(Request.Cookies["spn"].Value)));
                vShowReportWithParameter(ref crviewer, "rp_EmployeeTransaction.rpt", lparam);
                break;
            case "EmpTrxnAll":
                lparam.Clear();
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                lparam.Add(new cArrayList("Branch", Convert.ToString(Request.Cookies["spn"].Value)));
                vShowReportWithParameter(ref crviewer, "rp_EmployeeTransactionAll.rpt", lparam);
                break;
            case "EmpTrxnSelect":
                if (Request.QueryString["Trxn_typ"] == "null" && Request.QueryString["Date_From"] == "null")
                {
                    lparam.Add(new cArrayList("@Date_From", null));
                    lparam.Add(new cArrayList("@Date_To", null));
                    lparam.Add(new cArrayList("@Emp_CD", Request.QueryString["Emp_CD"]));
                    lparam.Add(new cArrayList("@Trxn_typ", null));
                    lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                    lparam.Add(new cArrayList("Branch", Convert.ToString(Request.Cookies["spn"].Value)));
                    vShowReportWithParameter(ref crviewer, "rp_EmployeeTransactionSelect.rpt", lparam);
                }
                else if (Request.QueryString["Trxn_typ"] != "null" && Request.QueryString["Date_From"] == "null")
                {
                    lparam.Add(new cArrayList("@Date_From", null));
                    lparam.Add(new cArrayList("@Date_To", null));
                    lparam.Add(new cArrayList("@Emp_CD", Request.QueryString["Emp_CD"]));
                    lparam.Add(new cArrayList("@Trxn_typ", Request.QueryString["Trxn_typ"]));
                    lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                    lparam.Add(new cArrayList("Branch", Convert.ToString(Request.Cookies["spn"].Value)));
                    vShowReportWithParameter(ref crviewer, "rp_EmployeeTransactionSelect.rpt", lparam);
                }
                else if (Request.QueryString["Trxn_typ"] == "null" && Request.QueryString["Date_From"] != "null")
                {
                    string strxndate_from = Request.QueryString["Date_From"];
                    string strxndate_to = Request.QueryString["Date_To"];
                    DateTime dtdate_from_trxn = DateTime.ParseExact(strxndate_from, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    DateTime dtdate_to_trxn = DateTime.ParseExact(strxndate_to, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    lparam.Add(new cArrayList("@Date_From", dtdate_from_trxn.Year.ToString() + "," + dtdate_from_trxn.Month.ToString("00") + "," + dtdate_from_trxn.Day.ToString("00")));
                    lparam.Add(new cArrayList("@Date_To", dtdate_to_trxn.Year.ToString() + "," + dtdate_to_trxn.Month.ToString("00") + "," + dtdate_to_trxn.Day.ToString("00")));
                    lparam.Add(new cArrayList("@Emp_CD", Request.QueryString["Emp_CD"]));
                    lparam.Add(new cArrayList("@Trxn_typ", null));
                    lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                    lparam.Add(new cArrayList("Branch", Convert.ToString(Request.Cookies["spn"].Value)));
                    vShowReportWithParameter(ref crviewer, "rp_EmployeeTransactionSelect.rpt", lparam);
                }
                else if (Request.QueryString["Trxn_typ"] != "null" && Request.QueryString["Date_From"] != "null")
                {
                    string strxndate_from = Request.QueryString["Date_From"];
                    string strxndate_to = Request.QueryString["Date_To"];
                    DateTime dtdate_from_trxn = DateTime.ParseExact(strxndate_from, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    DateTime dtdate_to_trxn = DateTime.ParseExact(strxndate_to, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    lparam.Add(new cArrayList("@Date_From", dtdate_from_trxn.Year.ToString() + "," + dtdate_from_trxn.Month.ToString("00") + "," + dtdate_from_trxn.Day.ToString("00")));
                    lparam.Add(new cArrayList("@Date_To", dtdate_to_trxn.Year.ToString() + "," + dtdate_to_trxn.Month.ToString("00") + "," + dtdate_to_trxn.Day.ToString("00")));
                    lparam.Add(new cArrayList("@Emp_CD", Request.QueryString["Emp_CD"]));
                    lparam.Add(new cArrayList("@Trxn_typ", Request.QueryString["Trxn_typ"]));
                    lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                    lparam.Add(new cArrayList("Branch", Convert.ToString(Request.Cookies["spn"].Value)));
                    vShowReportWithParameter(ref crviewer, "rp_EmployeeTransactionSelect.rpt", lparam);
                }
                break;
            case "BrnInctv":
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                lparam.Add(new cArrayList("@salespointcd", Convert.ToString(Request.QueryString["sp"])));
                vShowReport(ref crviewer, "rp_BranchIncentiveSummaryExcel.rpt", lformula, lparam);
                //creport rep = new creport();
                //List<cArrayList> arr = new List<cArrayList>();
                //arr.Add(new cArrayList("@salespointcd", Convert.ToString(Request.QueryString["sp"])));
                //arr.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                //rep.vShowReportToEXCEL("rp_BranchIncentiveSummaryExcel.rpt", arr, bll.sGetControlParameter("image_path") + "BranchIncentiveSummary.xlsx");
                break;
            case "DrOtherDel":
                lparam.Clear();
                lparam.Add(new cArrayList("@ids", Request.QueryString["ids"]));
                vShowReportWithParameter(ref crviewer, "rp_DriverOtherDelivery.rpt", lparam);
                break;
            case "DrOtherDelAll":
                lparam.Clear();
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                vShowReportWithParameter(ref crviewer, "rp_DriverOtherDeliveryAll.rpt", lparam);
                break;
            case "DrOtherDelSelect":
                string sdate_from = Request.QueryString["date_from"];
                string sdate_to = Request.QueryString["date_to"];
                DateTime dtdate_from = DateTime.ParseExact(sdate_from, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime dtdate_to = DateTime.ParseExact(sdate_to, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                lparam.Add(new cArrayList("@date_from", dtdate_from.Year.ToString() + "," + dtdate_from.Month.ToString("00") + "," + dtdate_from.Day.ToString("00")));
                lparam.Add(new cArrayList("@date_to", dtdate_to.Year.ToString() + "," + dtdate_to.Month.ToString("00") + "," + dtdate_to.Day.ToString("00")));
                lparam.Add(new cArrayList("@driver_cd", Request.QueryString["driver_cd"]));
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                vShowReportWithParameter(ref crviewer, "rp_DriverOtherDeliverySelect.rpt", lparam);
                break;
            case "rpsdtl1":
            case "rpsdtl2":
                List<cArrayList> lParamrps1 = (List<cArrayList>)Session["lParamrps1"];
                vShowReportWithParameter(ref crviewer, "rp_" + sSrc + ".rpt", lParamrps1);
                break;
            case "DSTADDLOSSREPORTTRXADD":
            case "DSTADDLOSSREPORTTRXDST":
            case "DSTADDLOSSREPORTTRXLOSS":
                List<cArrayList> lParamDSTADDLOSS = (List<cArrayList>)Session["lParamDSTADDLOSS"];
                vShowReportWithParameter(ref crviewer, "rp_" + sSrc + ".rpt", lParamDSTADDLOSS);
                break;
            case "invedit":
                lparam.Clear();
                lparam.Add(new cArrayList("@no", Request.QueryString["no"]));
                lparam.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                vShowReportWithParameter(ref crviewer, "rp_dosalesinvoiceedit.rpt", lparam);
                break;
            case "BankCollectionByDate":
                lparam.Clear();
                lparam.Add(new cArrayList("@from", Request.QueryString["from"]));
                lparam.Add(new cArrayList("@to", Request.QueryString["to"]));
                vShowReportWithParameter(ref crviewer, "rp_BankCollection.rpt", lparam);
                break;
            case "OtherCollectionByDate":
                lparam.Clear();
                lparam.Add(new cArrayList("@from", Request.QueryString["from"]));
                lparam.Add(new cArrayList("@to", Request.QueryString["to"]));
                vShowReportWithParameter(ref crviewer, "rp_OtherCollection.rpt", lparam);
                break;
            case "acccndnHO":
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                lparam.Add(new cArrayList("@salespointcd", Convert.ToString(Request.Cookies["sp"].Value)));
                lparam.Add(new cArrayList("@RefNo", null));
                vShowReport(ref crviewer, "rp_acccndnadjHO.rpt", lformula, lparam);
                break;
            case "acccndnRefNo":
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                lparam.Add(new cArrayList("@salespointcd", Convert.ToString(Request.Cookies["sp"].Value)));
                lparam.Add(new cArrayList("@RefNo", Convert.ToString(Request.QueryString["RefNo"])));
                vShowReport(ref crviewer, "rp_acccndnadjRefNo.rpt", lformula, lparam);
                break;
            case "DiscountReport":

                string postFromDis = string.Empty;//DateTime.Now.AddMonths(-3) ;
                string postToDis = string.Empty;//DateTime.Now;
                if (Request.QueryString["cndnFrom"] != null)
                {
                    postFromDis = DateTime.ParseExact(Request.QueryString["cndnFrom"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString();
                }
                if (Request.QueryString["cndnTo"] != null)
                {
                    postToDis = DateTime.ParseExact(Request.QueryString["cndnTo"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString();
                }
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                lparam.Add(new cArrayList("@postFrom", postFromDis));
                lparam.Add(new cArrayList("@postTo", postToDis));
                lparam.Add(new cArrayList("@salespointcd", Convert.ToString(Request.Cookies["sp"].Value)));
                //vShowReport(ref crviewer, "rp_discountReport_1Percent.rpt", lformula, lparam);
                vShowReport(ref crviewer, "rp_CashDiscount1Percentage.rpt", lformula, lparam);
                break;
            case "DiscountReportByInv":

                string postFromDisInv = string.Empty;
                string postToDisInv = string.Empty;
                if (Request.QueryString["cndnFrom"] != null)
                {
                    postFromDisInv = DateTime.ParseExact(Request.QueryString["cndnFrom"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString();
                }
                if (Request.QueryString["cndnTo"] != null)
                {
                    postToDisInv = DateTime.ParseExact(Request.QueryString["cndnTo"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString();
                }
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                lparam.Add(new cArrayList("@fromdate", postFromDisInv));
                lparam.Add(new cArrayList("@todate", postToDisInv));
                lparam.Add(new cArrayList("@salespointcd", Convert.ToString(Request.Cookies["sp"].Value)));
                //vShowReport(ref crviewer, "rp_discountReport_1Percent.rpt", lformula, lparam);
                vShowReport(ref crviewer, "rp_PaymentDetailsByInv.rpt", lformula, lparam);
                break;
            case "clhvatinv":
                lparam.Clear();
                lparam.Add(new cArrayList("@inv_no", Request.QueryString["inv"]));
                vShowReportWithParameter(ref crviewer, "rp_invoiceTax_eng2.rpt", lparam);
                break;
            case "acccndn":
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                lparam.Add(new cArrayList("@salespointcd", Convert.ToString(Request.Cookies["sp"].Value)));
                lparam.Add(new cArrayList("@RefNo", null));
                vShowReport(ref crviewer, "rp_acccndnadj.rpt", lformula, lparam);
                break;
            case "DriverHis":
                lparam.Add(new cArrayList("@driver_cd", Convert.ToString(Request.QueryString["emp"])));
                lparam.Add(new cArrayList("@period_cd", Convert.ToString(Request.QueryString["period"])));
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                vShowReport(ref crviewer, "rp_driverHis.rpt", lformula, lparam);
                break;
            case "DriverHisAllDriver":
                //lparam.Add(new cArrayList("@driver_cd", Convert.ToString(Request.QueryString["emp"])));
                lparam.Add(new cArrayList("@period_cd", Convert.ToString(Request.QueryString["period"])));
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                vShowReport(ref crviewer, "rp_driverHisAllDriver.rpt", lformula, lparam);
                break;
            case "invoiceTaxeng":
                lparam.Add(new cArrayList("@inv_no", (Request.QueryString["iv"])));
                lparam.Add(new cArrayList("@p_user", Request.Cookies["usr_id"].Value.ToString()));
                vShowReport(ref crviewer, "rp_invoiceTax_eng2.rpt", lformula, lparam);
                break;
            case "invoiceTaxara":
                lparam.Add(new cArrayList("@inv_no", (Request.QueryString["iv"])));
                lparam.Add(new cArrayList("@p_user", Request.Cookies["usr_id"].Value.ToString()));
                vShowReport(ref crviewer, "rp_invoiceTax_ara2.rpt", lformula, lparam);
                break;
            case "pa":
                lparam.Clear();
                lparam.Add(new cArrayList("@period", Request.QueryString["period"]));
                lparam.Add(new cArrayList("@emp_cd", Request.QueryString["emp"]));
                vShowReportWithParameter(ref crviewer, "rp_hrdkpiresult.rpt", lparam);
                break;
            case "kpi":
                lparam.Clear();
                lparam.Add(new cArrayList("@job_title_cd", Request.QueryString["jt"]));
                lparam.Add(new cArrayList("@level_cd", Request.QueryString["lv"]));
                vShowReportWithParameter(ref crviewer, "rp_mstkpi.rpt", lparam);
                break;
            case "ArabicForSaudi":
                lparam.Add(new cArrayList("@emp_cd", Convert.ToString(Request.QueryString["emp"])));
                lparam.Add(new cArrayList("@period", Convert.ToString(Request.QueryString["period"])));
                lparam.Add(new cArrayList("@totlaBF", Convert.ToString(Request.QueryString["totlaBF"])));
                lparam.Add(new cArrayList("@salespointcd", Convert.ToString(Request.QueryString["sp"])));
                lparam.Add(new cArrayList("@totlaDP", Convert.ToString(Request.QueryString["totlaDP"])));
                vShowReport(ref crviewer, "rp_hrdkpiArabicForDriver.rpt", lformula, lparam);
                break;
            case "ArabicForNonSaudi":
                lparam.Add(new cArrayList("@emp_cd", Convert.ToString(Request.QueryString["emp"])));
                lparam.Add(new cArrayList("@period", Convert.ToString(Request.QueryString["period"])));
                lparam.Add(new cArrayList("@totlaBF", Convert.ToString(Request.QueryString["totlaBF"])));
                lparam.Add(new cArrayList("@salespointcd", Convert.ToString(Request.QueryString["sp"])));
                lparam.Add(new cArrayList("@totlaDP", Convert.ToString(Request.QueryString["totlaDP"])));
                //vShowReport(ref crviewer, "rp_hrdkpiArabic.rpt", lformula, lparam);
                vShowReport(ref crviewer, "rp_hrdkpi_ara.rpt", lformula, lparam);
                break;
            case "EnglishForSaudiDriver":
                lparam.Add(new cArrayList("@emp_cd", Convert.ToString(Request.QueryString["emp"])));
                lparam.Add(new cArrayList("@period", Convert.ToString(Request.QueryString["period"])));
                lparam.Add(new cArrayList("@totlaBF", Convert.ToString(Request.QueryString["totlaBF"])));
                lparam.Add(new cArrayList("@salespointcd", Convert.ToString(Request.QueryString["sp"])));
                lparam.Add(new cArrayList("@totlaDP", Convert.ToString(Request.QueryString["totlaDP"])));
                vShowReport(ref crviewer, "rp_hrdkpiArabicDriver.rpt", lformula, lparam);
                break;
            case "EnglishForNonSaudi":
                lparam.Add(new cArrayList("@emp_cd", Convert.ToString(Request.QueryString["emp"])));
                lparam.Add(new cArrayList("@period", Convert.ToString(Request.QueryString["period"])));
                lparam.Add(new cArrayList("@totlaBF", Convert.ToString(Request.QueryString["totlaBF"])));
                lparam.Add(new cArrayList("@salespointcd", Convert.ToString(Request.QueryString["sp"])));
                lparam.Add(new cArrayList("@totlaDP", Convert.ToString(Request.QueryString["totlaDP"])));
                //vShowReport(ref crviewer, "rp_hrdkpi.rpt", lformula, lparam);
                //pak saleh Maryono Request no extra %
                vShowReport(ref crviewer, "rp_hrdkpi_new.rpt", lformula, lparam);
                break;
            case "ValidationSheet":
                lparam.Add(new cArrayList("@salespointcd", Convert.ToString(Request.QueryString["sp"])));
                lparam.Add(new cArrayList("@period", Convert.ToString(Request.QueryString["period"])));
                //vShowReport(ref crviewer, "rp_ValidationSheet1.rpt", lformula, lparam);
                //vShowReport(ref crviewer, "rp_ValidationSheet.rpt", lformula, lparam);
                //pak saleh Maryono Request no extra %
                vShowReport(ref crviewer, "rp_ValidationSheet_new.rpt", lformula, lparam);
                break;
            case "csh":
                string sdt = Request.QueryString["tg"];
                //DateTime dts = DateTime.ParseExact(sdt, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                //lparam.Add(new cArrayList("@cash_dt", dts.Year + " - " + dts.Month + " - " + dts.Day));
                lparam.Add(new cArrayList("@cash_dt", sdt));
                lparam.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                // lparam.Add(new cArrayList("p_batchdt", dts.ToShortDateString()));
                vShowReportWithParameter(ref crviewer, "rp_closingcashier2.rpt", lparam);
                // vShowReportWithParameter(ref crviewer , "rp_closingcashier2.rpt", lparam);
                break;
            case "inv":
                ctowords cw = new ctowords();
                string _inv_no = Request.QueryString["no"];
                //decimal _value = Convert.ToDecimal(bll.vLookUp("select (sum(unitprice * qty) - sum(disc_amt) + sum(vat)) from tdosalesinvoice_dtl where inv_no='" + _inv_no + "'"));
                //decimal _value = 0;
                //string _p = cw.ConvertNumberToWords(_value,  Language.French);
                sPrintCount = bll.vLookUp("select qty from tprint_control where print_cd='CANVINV' and doc_no='" + Request.QueryString["no"] + "' and salespointcd='" + Request.Cookies["usr_id"].Value.ToString() + "'");
                //lparam.Add(new cArrayList("@inv_no", Request.QueryString["no"]));
                //lparam.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                lparam.Add(new cArrayList("@inv_no", _inv_no));
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                //if (sPrintCount == "")
                //{
                //    lparam.Add(new cArrayList("p_print", Request.Cookies["usr_id"].Value));
                //}
                //else { lparam.Add(new cArrayList("p_print", "RE-PRINTED No. " + sPrintCount)); }
                //lparam.Add(new cArrayList("p_rps", bll.vLookUp("select dbo.fn_getrps('" + Request.QueryString["no"] + "','" + Request.Cookies["sp"].Value.ToString() + "')")));
                //lparam.Add(new cArrayList("p_terbilang_french", "zero"));
                //vShowReportWithParameter(ref crviewer, "rp_dosalesinvoice3.rpt", lparam);
                //vShowReportWithParameter(ref crviewer, "rp_invoicecanvas.rpt", lparam);
                vShowReportWithParameter(ref crviewer, "rp_invoicecanvas2.rpt", lparam);

                break;
            case "invto":
                sPrintCount = bll.vLookUp("select isnull(qty,0) from tprint_control where print_cd='TOINV' and doc_no='" + Request.QueryString["no"] + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                //lformula.Add("{trpt_dosalesinvoice.inv_no} = '" + Request.QueryString["no"] + "' and {trpt_dosalesinvoice.usr_id} = '" + Request.Cookies["usr_id"].Value.ToString() + "'");
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                if (sPrintCount == "")
                {
                    lparam.Add(new cArrayList("p_print", ""));
                }
                else { lparam.Add(new cArrayList("p_print", "RE-PRINTED No. " + sPrintCount)); }
                lparam.Add(new cArrayList("p_rps", bll.vLookUp("select dbo.fn_getrps('" + Request.QueryString["no"] + "','" + Request.Cookies["sp"].Value.ToString() + "')")));
                lparam.Add(new cArrayList("@inv_no", Request.QueryString["no"]));
                lparam.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                vShowReportWithParameter(ref crviewer, "rp_dosalesinvoice3.rpt", lparam, lformula);
                break;
            case "invto3":
                sPrintCount = bll.vLookUp("select isnull(qty,0) from tprint_control where print_cd='TOINV' and doc_no='" + Request.QueryString["no"] + "'");
                //lformula.Add("{trpt_dosalesinvoice.inv_no} = '" + Request.QueryString["no"] + "' and {trpt_dosalesinvoice.usr_id} = '" + Request.Cookies["usr_id"].Value.ToString() + "'");
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                if (bll.vLookUp("select distinct 1 from tdosalesinvoice_free where inv_no='" + Request.QueryString["no"] + "'") == "1")
                {
                    lparam.Add(new cArrayList("p_free", "+"));
                }
                else
                {
                    lparam.Add(new cArrayList("p_free", ""));
                }
                if (sPrintCount == "")
                {
                    lparam.Add(new cArrayList("p_print", ""));
                }
                else { lparam.Add(new cArrayList("p_print", "RE-PRINTED No. " + sPrintCount)); }
                //lformula.Add("{trpt_dosalesinvosoa2bycustice.inv_no} = '" + Request.QueryString["no"] + "' and {trpt_dosalesinvoice.usr_id} = '" + Request.Cookies["usr_id"].Value.ToString() + "'");
                lparam.Add(new cArrayList("@inv_no", Request.QueryString["no"]));
                vShowReportWithParameter(ref crviewer, "rp_dosalesinvoice6p.rpt", lparam, lformula);
                break;
            case "invto1":
                sPrintCount = bll.vLookUp("select isnull(qty,0) from tprint_control where print_cd='TOINV' and doc_no='" + Request.QueryString["no"] + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                //lformula.Add("{trpt_dosalesinvoice.inv_no} = '" + Request.QueryString["no"] + "' and {trpt_dosalesinvoice.usr_id} = '" + Request.Cookies["usr_id"].Value.ToString() + "'");
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                if (bll.vLookUp("select distinct 1 from tdosalesinvoice_free where inv_no='" + Request.QueryString["no"] + "'") == "1")
                {
                    lparam.Add(new cArrayList("p_free", "+"));
                }
                else
                {
                    lparam.Add(new cArrayList("p_free", ""));
                }
                if (sPrintCount == "")
                {
                    lparam.Add(new cArrayList("p_print", ""));
                }
                else { lparam.Add(new cArrayList("p_print", "PRINTED No. " + sPrintCount)); }
                //lformula.Add("{trpt_dosalesinvosoa2bycustice.inv_no} = '" + Request.QueryString["no"] + "' and {trpt_dosalesinvoice.usr_id} = '" + Request.Cookies["usr_id"].Value.ToString() + "'");
                lparam.Add(new cArrayList("@inv_no", Request.QueryString["no"]));
                lparam.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                vShowReportWithParameter(ref crviewer, "rp_dosalesinvoice4p.rpt", lparam, lformula);
                break;

            case "rprprp":
                sPrintCount = bll.vLookUp("select isnull(qty,0) from tprint_control where print_cd='TOINV' and doc_no='" + Request.QueryString["no"] + "'");
                lparam.Add(new cArrayList("p_print", "RE-PRINTED No. " + sPrintCount));
                lparam.Add(new cArrayList("@inv_no", Request.QueryString["no"]));
                vShowReportWithParameter(ref crviewer, "rp_dosalesinvoice4p.rpt", lparam, lformula);
                break;
            case "IncentiveNew":
                lparam.Add(new cArrayList("@emp_cd", Convert.ToString(Request.QueryString["emp"])));
                lparam.Add(new cArrayList("@period", Convert.ToString(Request.QueryString["period"])));
                lparam.Add(new cArrayList("@totlaBF", Convert.ToString(Request.QueryString["totlaBF"])));
                lparam.Add(new cArrayList("@totlaDP", Convert.ToString(Request.QueryString["totlaDP"])));
                lparam.Add(new cArrayList("@salespointcd", Convert.ToString(Request.QueryString["sp"])));
                //vShowReport(ref crviewer, "rp_hrdkpiIncentive.rpt", lformula, lparam);
                //pak saleh Maryono Request no extra %
                vShowReport(ref crviewer, "rp_hrdkpiIncentive_new.rpt", lformula, lparam);
                break;
            case "IncentiveNewSaudi":
                lparam.Add(new cArrayList("@emp_cd", Convert.ToString(Request.QueryString["emp"])));
                lparam.Add(new cArrayList("@period", Convert.ToString(Request.QueryString["period"])));
                lparam.Add(new cArrayList("@totlaBF", Convert.ToString(Request.QueryString["totlaBF"])));
                lparam.Add(new cArrayList("@totlaDP", Convert.ToString(Request.QueryString["totlaDP"])));
                vShowReport(ref crviewer, "rp_hrdkpiIncentiveSaudi.rpt", lformula, lparam);
                break;
            case "paArabic":
                lparam.Add(new cArrayList("@emp_cd", Convert.ToString(Request.QueryString["emp"])));
                lparam.Add(new cArrayList("@period", Convert.ToString(Request.QueryString["period"])));
                lparam.Add(new cArrayList("@totlaBF", Convert.ToString(Request.QueryString["totlaBF"])));
                lparam.Add(new cArrayList("@totlaDP", Convert.ToString(Request.QueryString["totlaDP"])));
                vShowReport(ref crviewer, "rp_hrdkpiArabic.rpt", lformula, lparam);
                break;
            case "salesvstargetbranch":
            case "salesvstarget":
            case "salesvstargetsalesman":
                List<cArrayList> lparamslstrg = (List<cArrayList>)Session["lParamslsvstrg"];
                vShowReportWithParameter(ref crviewer, "rp_" + sSrc + ".rpt", lparamslstrg);
                break;
            case "salesmanrps":
                vShowReport(ref crviewer, "rp_salesmanrps.rpt");
                break;
            case "balanceconfirm":
                List<cArrayList> lParambalanceconfirm = (List<cArrayList>)Session["lParambalanceconfirm"];
                vShowReportWithParameter(ref crviewer, "rp_balanceconfirm.rpt", lParambalanceconfirm);
                break;
            case "AllSupplier":
                lparam.Add(new cArrayList("@supplier_cd", null));
                vShowReport(ref crviewer, "rp_AllSupplier.rpt", lformula, lparam);
                break;

            case "SkipPrioirtyItemsLog":
                string ssperiodSkip = Request.QueryString["period"];
                string ssalespointcdSkip = Request.QueryString["salespointcd"];
                lparam.Add(new cArrayList("@salespointcd", ssalespointcdSkip));
                lparam.Add(new cArrayList("@period", ssperiodSkip));
                //lformula.Add("{tsalestargetho.period} = '" + ssperiodSkip + "' and {tsalestargetho.salespointcd} = '" + ssalespointcdSkip + "'");
                vShowReportWithParameter(ref crviewer, "rp_SkipPriorityItemsLog.rpt", lparam);
                //vShowReport(ref crviewer, "rp_SkipPriorityItemsLog1.rpt", lformula);
                break;
            case "po_ho":
                lparam.Clear();
                lparam.Add(new cArrayList("@do_sta_id", null));
                lparam.Add(new cArrayList("@salespointcd", Request.QueryString["salespointCD"]));
                vShowReportWithParameter(ref crviewer, "po_HO.rpt", lparam);
                break;
            case "poSummary_ho":
                lparam.Clear();
                lparam.Add(new cArrayList("@do_sta_id", null));
                lparam.Add(new cArrayList("@salespointcd", Request.QueryString["salespointCD"]));
                vShowReportWithParameter(ref crviewer, "poSummary_HO.rpt", lparam);
                break;
            case "returbyexp":
                List<cArrayList> lParamreturbyexp = (List<cArrayList>)Session["lParamreturbyexp"];
                vShowReportWithParameter(ref crviewer, "rp_" + sSrc + ".rpt", lParamreturbyexp);
                break;
            case "balance":
                List<cArrayList> lbalance = (List<cArrayList>)Session["sbalance"];
                vShowReportWithParameter(ref crviewer, "rp_ho_rep_balance.rpt", lbalance);
                break;

            case "claimvsclaimdtl":
                lparam.Clear();
                lparam.Add(new cArrayList("@year", Request.QueryString["yr"]));
                lparam.Add(new cArrayList("@vendor", Request.QueryString["vd"]));
                lparam.Add(new cArrayList("@cost", Request.QueryString["cs"]));
                lparam.Add(new cArrayList("@type", Request.QueryString["tp"]));
                vShowReportWithParameter(ref crviewer, "rp_claimclaim_dtl.rpt", lparam);
                break;
            case "claimvsclaimsum":
                lparam.Clear();
                lparam.Add(new cArrayList("@year", Request.QueryString["yr"]));
                lparam.Add(new cArrayList("@vendor", Request.QueryString["vd"]));
                lparam.Add(new cArrayList("@cost", Request.QueryString["cs"]));
                lparam.Add(new cArrayList("@type", Request.QueryString["tp"]));
                vShowReportWithParameter(ref crviewer, "rp_claimclaim_sum.rpt", lparam);
                break;
            case "cusbuy":
                lparam.Clear();
                lparam.Add(new cArrayList("@salespointcd", Request.QueryString["sp"]));
                lparam.Add(new cArrayList("@start_dt", Request.QueryString["sd"]));
                lparam.Add(new cArrayList("@end_dt", Request.QueryString["ed"]));
                lparam.Add(new cArrayList("@otlcd", Request.QueryString["ch"]));
                lparam.Add(new cArrayList("@salesman_cd", Request.QueryString["sal"]));
                lparam.Add(new cArrayList("@prod_cd", Request.QueryString["pd"]));
                lparam.Add(new cArrayList("@item_cd", Request.QueryString["it"]));
                vShowReportWithParameter(ref crviewer, "/eis/rp_salescontribute.rpt", lparam);
                break;
            case "eissales":
                lparam.Clear();
                lparam.Add(new cArrayList("@salespointcd", Request.QueryString["sp"]));
                lparam.Add(new cArrayList("@start_dt", Request.QueryString["sd"]));
                lparam.Add(new cArrayList("@end_dt", Request.QueryString["ed"]));
                vShowReportWithParameter(ref crviewer, "/eis/rp_salesachive.rpt", lparam);
                break;
            case "paylogbook":
                lparam.Clear();
                lparam.Add(new cArrayList("@month", Request.QueryString["mt"]));
                lparam.Add(new cArrayList("@year", Request.QueryString["yr"]));
                lparam.Add(new cArrayList("@vendor", Request.QueryString["vd"]));
                lparam.Add(new cArrayList("@cost", Request.QueryString["cs"]));
                lparam.Add(new cArrayList("@type", Request.QueryString["tp"]));
                lparam.Add(new cArrayList("@payment", Request.QueryString["py"]));
                vShowReportWithParameter(ref crviewer, "rp_claimpay_log.rpt", lparam);
                break;
            case "payvsclaimsum":
                lparam.Clear();
                lparam.Add(new cArrayList("@year", Request.QueryString["yr"]));
                lparam.Add(new cArrayList("@vendor", Request.QueryString["vd"]));
                lparam.Add(new cArrayList("@cost", Request.QueryString["cs"]));
                lparam.Add(new cArrayList("@type", Request.QueryString["tp"]));
                vShowReportWithParameter(ref crviewer, "rp_claimpay_sum.rpt", lparam);
                break;
            case "payvsclaimdtl":
                lparam.Clear();
                lparam.Add(new cArrayList("@year", Request.QueryString["yr"]));
                lparam.Add(new cArrayList("@vendor", Request.QueryString["vd"]));
                lparam.Add(new cArrayList("@cost", Request.QueryString["cs"]));
                lparam.Add(new cArrayList("@type", Request.QueryString["tp"]));
                lparam.Add(new cArrayList("@payment", Request.QueryString["py"]));
                vShowReportWithParameter(ref crviewer, "rp_claimpay_dtl.rpt", lparam);
                break;
            case "claimstatement":
                lparam.Clear();
                lparam.Add(new cArrayList("@salespoint", Request.QueryString["sp"]));
                lparam.Add(new cArrayList("@year", Request.QueryString["yr"]));
                lparam.Add(new cArrayList("@month", Request.QueryString["mt"]));
                vShowReportWithParameter(ref crviewer, "rp_claimliststatement.rpt", lparam);
                break;
            case "propvsclaimsum":
                lparam.Clear();
                lparam.Add(new cArrayList("@year", Request.QueryString["yr"]));
                lparam.Add(new cArrayList("@vendor", Request.QueryString["vd"]));
                lparam.Add(new cArrayList("@cost", Request.QueryString["cs"]));
                lparam.Add(new cArrayList("@type", Request.QueryString["tp"]));
                vShowReportWithParameter(ref crviewer, "rp_claimprop_sum.rpt", lparam);
                break;
            case "propvsclaimdtl":
                lparam.Clear();
                lparam.Add(new cArrayList("@year", Request.QueryString["yr"]));
                lparam.Add(new cArrayList("@vendor", Request.QueryString["vd"]));
                lparam.Add(new cArrayList("@cost", Request.QueryString["cs"]));
                lparam.Add(new cArrayList("@type", Request.QueryString["tp"]));
                vShowReportWithParameter(ref crviewer, "rp_claimprop_dtl.rpt", lparam);
                break;
            case "sumlogclaim":
                lparam.Clear();
                lparam.Add(new cArrayList("@year", Request.QueryString["yr"]));
                vShowReportWithParameter(ref crviewer, "rp_claimdatasum.rpt", lparam);
                break;
            case "logclaim":
                lparam.Clear();
                lparam.Add(new cArrayList("@status", Request.QueryString["st"]));
                lparam.Add(new cArrayList("@salespointcd", Request.QueryString["sp"]));
                lparam.Add(new cArrayList("@vendor", Request.QueryString["vd"]));
                lparam.Add(new cArrayList("@month", Request.QueryString["mt"]));
                lparam.Add(new cArrayList("@year", Request.QueryString["yr"]));
                vShowReportWithParameter(ref crviewer, "rp_claimdata.rpt", lparam);
                break;
            case "Appraisal":
                string job_title_cd = Request.QueryString["job_title_cd"];
                string period = Request.QueryString["period"];
                string salesman_cd = Request.QueryString["salesman_cd"];
                string salespoint_cd = Request.QueryString["salespoint_cd"];
                int month = Convert.ToInt32(Convert.ToString(period).Substring(4, 2));
                int year = Convert.ToInt32(Convert.ToString(period).Substring(0, 4));
                var lastDateValue = DateTime.DaysInMonth(year, month);
                DateTime lastDate = Convert.ToDateTime(Convert.ToString(lastDateValue) + "/" + Convert.ToString(month) + "/" + Convert.ToString(year));
                DateTime fisrtDate = Convert.ToDateTime(Convert.ToString(1) + "/" + Convert.ToString(month) + "/" + Convert.ToString(year));


                lparam.Add(new cArrayList("@appraisal_cat", "KPI"));
                lparam.Add(new cArrayList("@job_title_cd", job_title_cd));
                lparam.Add(new cArrayList("@salesman_cd", salesman_cd));
                lparam.Add(new cArrayList("@startDate", fisrtDate.Year + "-" + fisrtDate.Month + "-" + fisrtDate.Day));
                lparam.Add(new cArrayList("@enddate", lastDate.Year + "-" + lastDate.Month + "-" + lastDate.Day));
                lparam.Add(new cArrayList("@period", period));
                lparam.Add(new cArrayList("@salespoint_cd", salespoint_cd));

                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                vShowReport(ref crviewer, "Appraisal_DriverUpdated.rpt", lformula, lparam);
                //vShowReport(ref crviewer, "Appraisal_Driver.rpt", lformula, lparam);
                break;
            case "OldAppraisal":
                string job_title_cdOld = Request.QueryString["job_title_cd"];
                string periodOld = Request.QueryString["period"];
                string salesman_cdOld = Request.QueryString["salesman_cd"];
                string salespoint_cdOld = Request.QueryString["salespoint_cd"];
                int monthOld = Convert.ToInt32(Convert.ToString(periodOld).Substring(4, 2));
                int yearOld = Convert.ToInt32(Convert.ToString(periodOld).Substring(0, 4));
                var lastDateValueOld = DateTime.DaysInMonth(yearOld, monthOld);

                DateTime lastDateOld = Convert.ToDateTime(Convert.ToString(monthOld) + "/" + Convert.ToString(lastDateValueOld) + "/" + Convert.ToString(yearOld));
                DateTime fisrtDateOld = Convert.ToDateTime(Convert.ToString(monthOld) + "/" + Convert.ToString(1) + "/" + Convert.ToString(yearOld));


                //DateTime lastDateOld = Convert.ToDateTime(Convert.ToString(lastDateValueOld) + "-" + Convert.ToString(monthOld) + "-" + Convert.ToString(yearOld));
                //DateTime fisrtDateOld = Convert.ToDateTime(Convert.ToString(1) + "-" + Convert.ToString(monthOld) + "-" + Convert.ToString(yearOld));


                lparam.Add(new cArrayList("@appraisal_cat", "KPI"));
                lparam.Add(new cArrayList("@job_title_cd", job_title_cdOld));
                lparam.Add(new cArrayList("@salesman_cd", salesman_cdOld));
                lparam.Add(new cArrayList("@startDate", fisrtDateOld.Year + "-" + fisrtDateOld.Month + "-" + fisrtDateOld.Day));
                lparam.Add(new cArrayList("@enddate", lastDateOld.Year + "-" + lastDateOld.Month + "-" + lastDateOld.Day));
                lparam.Add(new cArrayList("@period", periodOld));
                lparam.Add(new cArrayList("@salespoint_cd", salespoint_cdOld));

                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                vShowReport(ref crviewer, "Appraisal_Driver.rpt", lformula, lparam);
                break;
            case "Incentive":
                string job_title_cdIncentive = Request.QueryString["job_title_cd"];
                string periodIncentive = Request.QueryString["period"];
                string salesman_cdIncentive = Request.QueryString["salesman_cd"];
                string salespoint_cdIncentive = Request.QueryString["salespoint_cd"];
                int monthIncentive = Convert.ToInt32(Convert.ToString(periodIncentive).Substring(4, 2));
                int yearIncentive = Convert.ToInt32(Convert.ToString(periodIncentive).Substring(0, 4));
                var lastDateValueIncentive = DateTime.DaysInMonth(yearIncentive, monthIncentive);
                //DateTime lastDateIncentive = Convert.ToDateTime(Convert.ToString(lastDateValueIncentive) + "/" + Convert.ToString(monthIncentive) + "/" + Convert.ToString(yearIncentive));
                //DateTime fisrtDateIncentive = Convert.ToDateTime(Convert.ToString(1) + "/" + Convert.ToString(monthIncentive) + "/" + Convert.ToString(yearIncentive));

                DateTime lastDateIncentive = Convert.ToDateTime(Convert.ToString(monthIncentive) + "/" + Convert.ToString(lastDateValueIncentive) + "/" + Convert.ToString(yearIncentive));
                DateTime fisrtDateIncentive = Convert.ToDateTime(Convert.ToString(monthIncentive) + "/" + Convert.ToString(1) + "/" + Convert.ToString(yearIncentive));


                lparam.Add(new cArrayList("@appraisal_cat", "KPI"));
                lparam.Add(new cArrayList("@job_title_cd", job_title_cdIncentive));
                lparam.Add(new cArrayList("@salesman_cd", salesman_cdIncentive));
                lparam.Add(new cArrayList("@startDate", fisrtDateIncentive.Year + "-" + fisrtDateIncentive.Month + "-" + fisrtDateIncentive.Day));
                lparam.Add(new cArrayList("@enddate", lastDateIncentive.Year + "-" + lastDateIncentive.Month + "-" + lastDateIncentive.Day));
                lparam.Add(new cArrayList("@period", periodIncentive));
                lparam.Add(new cArrayList("@salespoint_cd", salespoint_cdIncentive));

                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                vShowReport(ref crviewer, "Appraisal_DriverIncentive.rpt", lformula, lparam);
                break;

            case "iv120":
                lparam.Clear();
                //List<cArrayList> arr = new List<cArrayList>();
                lparam.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                vShowReport(ref crviewer, "rp_inv120dayraw.rpt", lformula, lparam);
                break;
            //case "rpsdtl1":
            //    List<cArrayList> lParamrps1 = (List<cArrayList>)Session["lParamrps1"];
            //    vShowReportWithParameter(ref crviewer, "rp_dailysalesmanreportdetail.rpt", lParamrps1);
            //    break;
            case "outletsperformancecss":
            case "outletsperformancecs":
            case "outletsperformance":
            case "outletsperformanceacc":
                List<cArrayList> lParamoutletperf = (List<cArrayList>)Session["lParamoutletsperformance"];
                vShowReportWithParameter(ref crviewer, "rp_" + sSrc + ".rpt", lParamoutletperf);
                break;
            case "listcustbysalesman":
            case "listcustbybranch":
                List<cArrayList> lParamlistcustbysalesman = (List<cArrayList>)Session["lParamlistcustbysalesman"];
                vShowReportWithParameter(ref crviewer, "rpt" + sSrc + ".rpt", lParamlistcustbysalesman);
                break;

            case "carprivate":
                lparam.Add(new cArrayList("@vhc_cd", Request.QueryString["carprivs"]));
                vShowReportWithParameter(ref crviewer, "/maintenance/rp_vehicleprivate.rpt", lparam);
                break;
            case "car":
                vShowReport(ref crviewer, @"/maintenance/rp_vehicle.rpt");
                break;
            case "rtn2":
            case "rtn1":
                List<cArrayList> lParamrtn = (List<cArrayList>)Session["lParamrtn"];
                vShowReportWithParameter(ref crviewer, "rp_" + sSrc + ".rpt", lParamrtn);
                break;
            case "rpsdtl":
                List<cArrayList> lparamrps = (List<cArrayList>)Session["lParamrps"];
                vShowReportWithParameter(ref crviewer, "rp_dailysalesmanreport.rpt", lparamrps);
                break;
            case "contractinvg":
                lparam.Clear();
                lparam.Add(new cArrayList("@contract_no", Request.QueryString["ba"]));
                lparam.Add(new cArrayList("@seq", Request.QueryString["no"]));
                lparam.Add(new cArrayList("@cust", Request.QueryString["cust"]));
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                lparam.Add(new cArrayList("p_print", Request.Cookies["usr_id"].Value.ToString()));
                lparam.Add(new cArrayList("p_free", null));
                vShowReportWithParameter(ref crviewer, "rp_contractinvoice5p.rpt", lparam);
                break;
            case "contractinv":
                lparam.Clear();
                lparam.Add(new cArrayList("@contract_no", Request.QueryString["ba"]));
                lparam.Add(new cArrayList("@seq", Request.QueryString["no"]));
                lparam.Add(new cArrayList("@cust", null));
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                lparam.Add(new cArrayList("p_print", Request.Cookies["usr_id"].Value.ToString()));
                vShowReportWithParameter(ref crviewer, "rp_contractinvoice5p.rpt", lparam);
                break;
            case "contract":
                lparam.Clear();
                lparam.Add(new cArrayList("@contract_no", Request.QueryString["cno"]));
                if (Request.QueryString["type"] == "DR") { vShowReportWithParameter(ref crviewer, "rp_contractDR.rpt", lparam); }
                else if (Request.QueryString["type"] == "SB") { vShowReportWithParameter(ref crviewer, "rp_contractSB.rpt", lparam); }
                else { vShowReportWithParameter(ref crviewer, "rp_contractTB.rpt", lparam); }
                break;
            case "claimsummbyper":
            case "claimsum":
            case "salesvsclaimdetail":
                List<cArrayList> lParamclaimsum = (List<cArrayList>)Session["lParamclaimsum"];
                vShowReportWithParameter(ref crviewer, "rp_" + sSrc + ".rpt", lParamclaimsum);
                break;
            case "tgtdrv":
                lparam.Add(new cArrayList("p_period", Request.QueryString["period"]));
                vShowReportWithParameter(ref crviewer, "rp_targetdriver.rpt", lparam);
                break;
            case "claimMonthly":
                lparam.Add(new cArrayList("@salespointcd", Request.QueryString["branch"]));
                lparam.Add(new cArrayList("@claim_sta_id", Request.QueryString["status"]));
                lparam.Add(new cArrayList("@month", Request.QueryString["month"]));
                lparam.Add(new cArrayList("@year", Request.QueryString["year"]));
                lparam.Add(new cArrayList("@prin", Request.QueryString["prin"]));

                vShowReportWithParameter(ref crviewer, "rp_claimMonthly.rpt", lparam);
                break;
            case "cancelinv":
                lparam.Add(new cArrayList("@inv_no", Request.QueryString["iv"]));
                vShowReportWithParameter(ref crviewer, "rp_cancelinvoice.rpt", lparam);
                break;
            case "claimcn":
                lparam.Clear();
                lparam.Add(new cArrayList("@cndn", Request.QueryString["no"]));
                vShowReportWithParameter(ref crviewer, "rp_claimcndn.rpt", lparam);
                break;
            case "sendclaim":
                lparam.Clear();
                lparam.Add(new cArrayList("@clh_no", Request.QueryString["clh"]));
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                lparam.Add(new cArrayList("@NewLineChar", null));
                vShowReportWithParameter(ref crviewer, "rp_sendClaimPrincipal.rpt", lparam);
                break;
            case "sendclaimCO":
                lparam.Clear();
                lparam.Add(new cArrayList("@clh_no", Request.QueryString["clh"]));
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                vShowReportWithParameter(ref crviewer, "rp_sendClaimPrincipalCO.rpt", lparam);
                break;
            case "claimho":
                lparam.Clear();
                lparam.Add(new cArrayList("@claim_no", Request.QueryString["cl"]));
                lparam.Add(new cArrayList("@ccnr", Request.QueryString["cr"]));
                vShowReportWithParameter(ref crviewer, "rp_claim.rpt", lparam);
                break;
            case "claimhoCO":
                lparam.Clear();
                lparam.Add(new cArrayList("@claim_no", Request.QueryString["cl"]));
                vShowReportWithParameter(ref crviewer, "rp_claimCO.rpt", lparam);
                break;
            case "darowprop":
                lparam.Clear();
                lparam.Add(new cArrayList("@prop_no", null));
                lparam.Add(new cArrayList("@vendor", Request.QueryString["vendor"]));
                lparam.Add(new cArrayList("@cost", Request.QueryString["cost"]));
                lparam.Add(new cArrayList("@promo", Request.QueryString["promo"]));
                lparam.Add(new cArrayList("@year", Request.QueryString["year"]));
                lparam.Add(new cArrayList("@month", Request.QueryString["month"]));
                vShowReportWithParameter(ref crviewer, "rp_proposaldata.rpt", lparam);
                break;
            case "prop":
                lparam.Clear();
                lparam.Add(new cArrayList("@prop_no", Request.QueryString["prop_no"]));
                lparam.Add(new cArrayList("@cust", Request.QueryString["cust"]));
                lparam.Add(new cArrayList("@product", Request.QueryString["product"]));
                lparam.Add(new cArrayList("@salespoint", null));
                lparam.Add(new cArrayList("@dbp", Request.QueryString["dbp"]));
                lparam.Add(new cArrayList("@vendor", null));
                lparam.Add(new cArrayList("@cost", null));
                lparam.Add(new cArrayList("@promo", null));
                lparam.Add(new cArrayList("@year", null));
                lparam.Add(new cArrayList("@month", null));
                lparam.Add(new cArrayList("@type", null));
                vShowReportWithParameter(ref crviewer, "rp_proposal.rpt", lparam);
                break;
            case "py2":
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                vShowReport(ref crviewer, "rp_paymentreceipt3.rpt", lformula, lparam);
                break;
            case "cusdocdt":
                List<cArrayList> lParamcusdocdt = (List<cArrayList>)Session["lParamcusrp"];
                vShowReportWithParameter(ref crviewer, "rp_customerdocrep.rpt", lParamcusdocdt);
                break;
            //case "invto":
            //    sPrintCount = bll.vLookUp("select isnull(qty,0) from tprint_control where print_cd='TOINV' and doc_no='" + Request.QueryString["no"] + "'");
            //    lformula.Add("{trpt_dosalesinvoice.inv_no} = '" + Request.QueryString["no"] + "' and {trpt_dosalesinvoice.usr_id} = '" + Request.Cookies["usr_id"].Value.ToString() + "'");
            //    lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
            //    if (sPrintCount == "")
            //    {
            //        lparam.Add(new cArrayList("p_print", ""));
            //    }
            //    else { lparam.Add(new cArrayList("p_print", "RE-PRINTED No. " + sPrintCount)); }
            //    lparam.Add(new cArrayList("p_rps", bll.vLookUp("select dbo.fn_getrps('" + Request.QueryString["no"] + "')")));
            //    vShowReportWithParameter(ref crviewer, "rp_dosalesinvoice3.rpt", lparam, lformula);
            //    break;
            case "spall":
                //string sPeriodSP = Request.QueryString["prd"];
                //lformula.Add("{tsalestargetho.period} = '" + sPeriodSP + "'");
                //vShowReport(ref crviewer, "rp_salestargetho.rpt", lformula);
                lparam.Add(new cArrayList("@period", Request.QueryString["prd"]));
                lparam.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                vShowReportWithParameter(ref crviewer, "rp_salestargetho.rpt", lparam);
                break;
            case "payprom":
                string sPromisedCode = Request.QueryString["no"];
                //  string sFormula = "{tpayment_promised.promised_cd} = '"+sPromisedCode+"'";
                //   string sSpv = bll.vLookUp("select direcspv_cd from temployee_info where emp_cd='"+Request.QueryString["emp"]+"'");
                //   sSpv = bll.vLookUp("select (emp_cd+':'+emp_nm) from tmst_employee where emp_cd='" + sSpv + "'");
                lparam.Add(new cArrayList("@promised_cd", sPromisedCode));
                //   lformula.Add(sFormula);
                //vShowReport(ref crviewer, "rp_paymentpromised.rpt", );
                vShowReportWithParameter(ref crviewer, "rp_paymentpromised.rpt", lparam);
                break;
            case "cusdoc":
                vShowReport(ref crviewer, "rp_customerdoc.rpt");
                break;
            case "cusrp":
                List<cArrayList> lParamcusrp = (List<cArrayList>)Session["lParamcusrp"];
                if (Request.QueryString["p"] == "0")
                {
                    vShowReportWithParameter(ref crviewer, "rp_mstcustomerbysls.rpt", lParamcusrp);
                }
                else
                {
                    vShowReportWithParameter(ref crviewer, "rp_mstcustomerbysls1.rpt", lParamcusrp);
                }


                break;
            case "custbl":
                List<cArrayList> lParamcustbl = (List<cArrayList>)Session["custbl"];
                vShowReportWithParameter(ref crviewer, "rp_custbalancedue.rpt", lParamcustbl);
                break;
            //case "outletsperformancecs":
            //case "outletsperformance":
            //case "outletsperformanceacc":
            //    string dtp1 = Request.QueryString["startdate"];
            //    //string dtp2 = Request.QueryString["enddate"];
            //    DateTime dtpayp1 = DateTime.ParseExact(dtp1, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //    //DateTime dtpayp2 = DateTime.ParseExact(dtp2, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //    lparam.Add(new cArrayList("@startdate", dtpayp1.Year.ToString() + "-" + dtpayp1.Month.ToString("00") + "-" + dtpayp1.Day.ToString("00")));
            //    //lparam.Add(new cArrayList("@enddate", dtpayp2.Year.ToString() + "," + dtpayp2.Month.ToString("00") + "," + dtpayp2.Day.ToString("00")));
            //    lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
            //    lparam.Add(new cArrayList("@salespointcd", Request.QueryString["salp"]));
            //    vShowReportWithParameter(ref crviewer, "rp_" + sSrc + ".rpt", lparam);
            //    break;
            case "cashiermonthly":
                List<cArrayList> lParamcsh = (List<cArrayList>)Session["lParamcsh"];
                vShowReportWithParameter(ref crviewer, "rp_cashier.rpt", lParamcsh);
                break;
            case "soa1bybranch3":
            case "soa1bybranch":
            case "soa1byslsman":
            case "soa1bycust":
            case "soa1bycusgr":
            case "soa2bybranch":
            case "soa2byslsman":
            case "soa2bycust":
            case "soa2bycusgr":
                List<cArrayList> lParamsoabr = (List<cArrayList>)Session["lParamsoabr"];
                DateTime soa1bycust = DateTime.ParseExact(lParamsoabr[1].ParamValue.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime soa1bycust2 = DateTime.ParseExact(lParamsoabr[2].ParamValue.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                lparam.Add(new cArrayList("@salespointcd", lParamsoabr[0].ParamValue.ToString()));
                lparam.Add(new cArrayList("@enddate", soa1bycust2.Year + "-" + soa1bycust2.Month + "-" + soa1bycust2.Day));
                lparam.Add(new cArrayList("@startdate", soa1bycust.Year + "-" + soa1bycust.Month + "-" + soa1bycust.Day));
                if (Request.QueryString["p"] == "2" || Request.QueryString["p"] == "1")
                {
                    lparam.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                }
                if (Request.QueryString["p"] == "3")
                {
                    lparam.Add(new cArrayList("@cusgrcd", lParamsoabr[3].ParamValue.ToString()));
                }

                lparam.Add(new cArrayList("pobox", bll.vLookUp("select fld_desc from tfield_value where fld_nm='SOA' and fld_valu='pobox'")));
                lparam.Add(new cArrayList("post", bll.vLookUp("select fld_desc from tfield_value where fld_nm='SOA' and fld_valu='post'")));
                lparam.Add(new cArrayList("tele", bll.vLookUp("select fld_desc from tfield_value where fld_nm='SOA' and fld_valu='tele'")));
                lparam.Add(new cArrayList("addr", bll.vLookUp("select fld_desc from tfield_value where fld_nm='SOA' and fld_valu='addr'")));
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                //lparam.Add(new cArrayList("@startdate", soa1bycust.Year.ToString() + "-" + soa1bycust.Month.ToString("00") + "-" + soa1bycust.Day.ToString("00")));
                //lparam.Add(new cArrayList("@endtdate", soa1bycust2.Year.ToString() + "-" + soa1bycust2.Month.ToString("00") + "-" + soa1bycust2.Day.ToString("00")));
                vShowReportWithParameter(ref crviewer, "rp_" + sSrc + ".rpt", lparam);
                break;
            case "pycheque":
                string sPayment = Request.QueryString["py"];
                lparam.Add(new cArrayList("@payment_no", Request.QueryString["py"]));
                lparam.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                vShowReportWithParameter(ref crviewer, "rp_paymentreceiptcheque.rpt", lparam);
                break;
            case "closingstockjaretVanSubdepomonthly":
                lparam.Add(new cArrayList("@period", Request.QueryString["period"]));
                lparam.Add(new cArrayList("@sitedest", Request.QueryString["sitedest"]));
                vShowReportWithParameter(ref crviewer, "rptclosingstockjaretVanSubdepomonthly.rpt", lparam);
                break;
            //case "claimsum":
            //    List<cArrayList> lParamclaimsum = (List<cArrayList>)Session["lParamclaimsum"];
            //    vShowReportWithParameter(ref crviewer, "rp_claimsum.rpt", lParamclaimsum);
            //    break;
            case "jareddpo":
                List<cArrayList> lParamjareddpo = (List<cArrayList>)Session["lParamjareddpo"];
                vShowReportWithParameter(ref crviewer, "rp_jareddepo.rpt", lParamjareddpo);
                break;
            case "clco":
                // List<cArrayList> lParamstockschentry = (List<cArrayList>)Session["lParamstockschentry"];

                string sClaimNo = Request.QueryString["cno"];
                //lformula.Add("{tproposal_paid.claimco_cd} = '" + sClaimNo + "'");
                lparam.Add(new cArrayList("@claimco_cd", sClaimNo));
                vShowReportWithParameter(ref crviewer, "rp_cdvclaim.rpt", lparam);
                //vShowReport(ref crviewer, "rp_cdvclaim.rpt", lformula);
                break;
            case "stockschentry":
                List<cArrayList> lParamstockschentry = (List<cArrayList>)Session["lParamstockschentry"];
                vShowReportWithParameter(ref crviewer, "rptclosingschedulejaret.rpt", lParamstockschentry);
                break;
            case "rpout3":
                List<cArrayList> lParamrpout3 = (List<cArrayList>)Session["lParamrpout2"];
                vShowReportWithParameter(ref crviewer, "rp_invoutletanddatebycusgrcd.rpt", lParamrpout3);
                break;
            //case "prop":
            //    lparam.Clear();
            //    lparam.Add(new cArrayList("@prop_no", Request.QueryString["prop_no"]));
            //    lparam.Add(new cArrayList("@cust", Request.QueryString["cust"]));
            //    lparam.Add(new cArrayList("@product", Request.QueryString["product"]));
            //    lparam.Add(new cArrayList("@salespoint", null));
            //    vShowReportWithParameter(ref crviewer, "rp_proposal.rpt", lparam);
            //    break;
            case "rcusrcd1":
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                lparam.Add(new cArrayList("@diffre", null));
                lparam.Add(new cArrayList("@sysdt", null));
                vShowReportWithParameter(ref crviewer, "rp_invoicereceiptdrv.rpt", lparam);
                break;
            case "rcusrcd15":
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                lparam.Add(new cArrayList("@diffre", "5"));
                lparam.Add(new cArrayList("@sysdt", null));
                vShowReportWithParameter(ref crviewer, "rp_invoicereceiptdrv.rpt", lparam);
                break;
            case "rcusrc1":
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                lparam.Add(new cArrayList("@diffre", null));
                lparam.Add(new cArrayList("@sysdt", null));
                vShowReportWithParameter(ref crviewer, "rp_invoicereceipt.rpt", lparam);
                break;
            case "rcusrc15":
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                lparam.Add(new cArrayList("@diffre", "5"));
                lparam.Add(new cArrayList("@sysdt", null));
                vShowReportWithParameter(ref crviewer, "rp_invoicereceipt.rpt", lparam);
                break;
            case "int05r":
                string dtint05r = Request.QueryString["tg"];
                string dtint2 = Request.QueryString["tg1"];
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                DateTime dtint = DateTime.ParseExact(dtint05r, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                lparam.Add(new cArrayList("@startdate", dtint.Year.ToString() + "-" + dtint.Month.ToString("00") + "-" + dtint.Day.ToString("00")));
                DateTime dtint22 = DateTime.ParseExact(dtint2, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                lparam.Add(new cArrayList("@enddate", dtint22.Year.ToString() + "-" + dtint22.Month.ToString("00") + "-" + dtint22.Day.ToString("00")));
                if (Request.QueryString["p"] == "0")
                {
                    lparam.Add(new cArrayList("@salesman_cd", null));
                    vShowReportWithParameter(ref crviewer, "rp_tinternal.rpt", lparam, lformula);
                }
                else if (Request.QueryString["p"] == "1")
                {
                    lparam.Add(new cArrayList("@salesman_cd", Request.QueryString["sls"]));
                    vShowReportWithParameter(ref crviewer, "rp_tinternal.rpt", lparam, lformula);
                }
                break;
            //case "cusrp":
            //    List<cArrayList> lParamcusrp = (List<cArrayList>)Session["lParamcusrp"];
            //    if (Request.QueryString["p"] == "0")
            //    {
            //        vShowReportWithParameter(ref crviewer, "rp_mstcustomerbysls.rpt", lParamcusrp);
            //    }
            //    else
            //    {
            //        vShowReportWithParameter(ref crviewer, "rp_mstcustomerbysls1.rpt", lParamcusrp);
            //    }


            //    break;
            case "cshden":
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                lparam.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                vShowReportWithParameter(ref crviewer, "rp_ccashdenominationrep.rpt", lparam);
                break;
            case "rpaydt":
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                string rpaydt = Request.QueryString["tg"];
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                DateTime rpaydtpay = DateTime.ParseExact(rpaydt, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                lparam.Add(new cArrayList("@payment_dt", rpaydtpay.Year.ToString() + "-" + rpaydtpay.Month.ToString("00") + "-" + rpaydtpay.Day.ToString("00")));
                vShowReportWithParameter(ref crviewer, "rp_rekappayment1.rpt", lparam);
                break;
            //case "int05r":
            //    string dtint05r = Request.QueryString["tg"];
            //    string dtint2 = Request.QueryString["tg1"];
            //    lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
            //    DateTime dtint = DateTime.ParseExact(dtint05r, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //    lparam.Add(new cArrayList("@startdate", dtint.Year.ToString() + "-" + dtint.Month.ToString("00") + "-" + dtint.Day.ToString("00")));
            //    DateTime dtint22 = DateTime.ParseExact(dtint2, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //    lparam.Add(new cArrayList("@enddate", dtint22.Year.ToString() + "-" + dtint22.Month.ToString("00") + "-" + dtint22.Day.ToString("00")));
            //    if (Request.QueryString["p"] == "0")
            //    {
            //        lparam.Add(new cArrayList("@salesman_cd", null));
            //        vShowReportWithParameter(ref crviewer, "rp_tinternal.rpt", lparam, lformula);
            //    }
            //    else if (Request.QueryString["p"] == "1")
            //    {
            //        lparam.Add(new cArrayList("@salesman_cd", Request.QueryString["sls"]));
            //        vShowReportWithParameter(ref crviewer, "rp_tinternal.rpt", lparam, lformula);
            //    }
            //    break;
            //case "cusrp":
            //    List<cArrayList> lParamcusrp = (List<cArrayList>)Session["lParamcusrp"];
            //    vShowReportWithParameter(ref crviewer, "rp_mstcustomerbysls.rpt", lParamcusrp);
            //    break;
            //case "rpaydt":
            //    string rpaydt = Request.QueryString["tg"];
            //    lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
            //    DateTime rpaydtpay = DateTime.ParseExact(rpaydt, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //    lparam.Add(new cArrayList("@payment_dt", rpaydtpay.Year.ToString() + "-" + rpaydtpay.Month.ToString("00") + "-" + rpaydtpay.Day.ToString("00")));
            //    vShowReportWithParameter(ref crviewer, "rp_rekappayment1.rpt", lparam);
            //    break;

            case "rpout":
                List<cArrayList> lParamrpout = (List<cArrayList>)Session["lParamrpout"];
                vShowReportWithParameter(ref crviewer, "rp_invoutletanddate.rpt", lParamrpout);
                break;
            case "it1":
                List<cArrayList> lParamit1 = (List<cArrayList>)Session["lParamit1"];
                vShowReportWithParameter(ref crviewer, "rptInternalTranfer1.rpt", lParamit1);
                break;
            case "oer14":
                string dtoer14 = Request.QueryString["tg"];
                string dtoer2 = Request.QueryString["tg1"];
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                DateTime dtoer = DateTime.ParseExact(dtoer14, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                lparam.Add(new cArrayList("@startdt", dtoer.Year.ToString() + "-" + dtoer.Month.ToString("00") + "-" + dtoer.Day.ToString("00")));
                DateTime dtoer22 = DateTime.ParseExact(dtoer2, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                lparam.Add(new cArrayList("@enddt", dtoer22.Year.ToString() + "-" + dtoer22.Month.ToString("00") + "-" + dtoer22.Day.ToString("00")));
                if (Request.QueryString["p"] == "0")
                {
                    vShowReportWithParameter(ref crviewer, "rp_oer14.rpt", lparam, lformula);
                }
                else
                {

                    lparam.Add(new cArrayList("@salesman_cd", Request.QueryString["sls"]));
                    vShowReportWithParameter(ref crviewer, "rp_oer14bysls.rpt", lparam, lformula);
                }

                break;
            case "rpout1":
                List<cArrayList> lParamrp1out = (List<cArrayList>)Session["lParamrpout"];
                vShowReportWithParameter(ref crviewer, "rp_invoutletanddate1.rpt", lParamrp1out);
                break;
            case "summtosa":
                string dtsummtosa1 = Request.QueryString["startdate"];
                string dtsummtosa2 = Request.QueryString["enddate"];
                DateTime dtpaysummtosa1 = DateTime.ParseExact(dtsummtosa1, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime dtpaysummtosa2 = DateTime.ParseExact(dtsummtosa2, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                lparam.Add(new cArrayList("@startdate", dtpaysummtosa1.Year.ToString() + "," + dtpaysummtosa1.Month.ToString("00") + "," + dtpaysummtosa1.Day.ToString("00")));
                lparam.Add(new cArrayList("@enddate", dtpaysummtosa2.Year.ToString() + "," + dtpaysummtosa2.Month.ToString("00") + "," + dtpaysummtosa2.Day.ToString("00")));
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                lparam.Add(new cArrayList("@salespointcd", Request.QueryString["sap"]));
                string pptype1 = Request.QueryString["ptyp"];
                if (pptype1 == "0")
                {
                    vShowReportWithParameter(ref crviewer, "rp_sumtakeordercollfromcustbysals.rpt", lparam);
                }
                else if (pptype1 == "1")
                {
                    lparam.Add(new cArrayList("@salesman_cd", Request.QueryString["sls"]));
                    vShowReportWithParameter(ref crviewer, "rp_balancecustomer.rpt", lparam);
                }
                else if (pptype1 == "2")
                {
                    vShowReportWithParameter(ref crviewer, "rp_summto2.rpt", lparam);
                }
                else if (pptype1 == "3")
                {
                    vShowReportWithParameter(ref crviewer, "rp_balancecustomerbranch.rpt", lparam);
                }
                break;

            case "rpay"://{tmst_payment.payment_dt} = Date (2015, 10, 01)
                string dt = Request.QueryString["tg"];
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                DateTime dtpay = DateTime.ParseExact(dt, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                lparam.Add(new cArrayList("@payment_dt", dtpay.Year.ToString() + "-" + dtpay.Month.ToString("00") + "-" + dtpay.Day.ToString("00")));
                //  vShowReport(ref crviewer, "rp_rekappayment.rpt",lparam);
                vShowReportWithParameter(ref crviewer, "rp_rekappayment.rpt", lparam);
                break;
            case "rinv":
                string dtrinv = Request.QueryString["tg"];
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                DateTime dtinv = DateTime.ParseExact(dtrinv, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                lparam.Add(new cArrayList("@inv_dt", dtinv.Year.ToString() + "-" + dtinv.Month.ToString("00") + "-" + dtinv.Day.ToString("00")));
                vShowReportWithParameter(ref crviewer, "rp_dailyinvoicewh.rpt", lparam, lformula);
                break;
            case "rcusrc":
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                lparam.Add(new cArrayList("@inv_no", null));
                vShowReportWithParameter(ref crviewer, "rp_invoicenotreceived.rpt", lparam);
                break;
            //case "invto1":
            //    sPrintCount = bll.vLookUp("select isnull(qty,0) from tprint_control where print_cd='TOINV' and doc_no='" + Request.QueryString["no"] + "'");
            //    lformula.Add("{trpt_dosalesinvoice.inv_no} = '" + Request.QueryString["no"] + "' and {trpt_dosalesinvoice.usr_id} = '" + Request.Cookies["usr_id"].Value.ToString() + "'");
            //    lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
            //    if (bll.vLookUp("select distinct 1 from tdosalesinvoice_free where inv_no='" + Request.QueryString["no"] + "'") == "1")
            //    {
            //        lparam.Add(new cArrayList("p_free", "+"));
            //    }
            //    else
            //    {
            //        lparam.Add(new cArrayList("p_free", ""));
            //    }
            //    if (sPrintCount == "")
            //    {
            //        lparam.Add(new cArrayList("p_print", ""));
            //    }
            //    else { lparam.Add(new cArrayList("p_print", "RE-PRINTED No. " + sPrintCount)); }
            //    vShowReportWithParameter(ref crviewer, "rp_dosalesinvoice3p.rpt", lparam, lformula);
            //    break;
            case "sus":
                string sPaymentNo = Request.QueryString["pn"];
                lformula.Add("{tpayment_suspense.payment_no} ='" + sPaymentNo + "'");
                lformula.Add("{tpayment_suspense.salespointcd} ='" + Request.Cookies["sp"].Value.ToString() + "'");
                vShowReport(ref crviewer, "rp_suspenseapplied.rpt", lformula);
                break;

            case "claimcndn":
                List<cArrayList> lParamcn = (List<cArrayList>)Session["Secndn"];
                vShowReportWithParameter(ref crviewer, "rp_claimsumcndn.rpt", lParamcn);
                break;
            case "claimCashOut":
                List<cArrayList> lParamcshout = (List<cArrayList>)Session["Secshout"];
                vShowReportWithParameter(ref crviewer, "rp_" + sSrc + ".rpt", lParamcshout);
                break;
            case "claimFG":
            case "claimDC":
            case "claimTB":
            case "claimGD":
            case "claimSP":
            case "claimLP":
                List<cArrayList> lParamFG = (List<cArrayList>)Session["FG"];
                vShowReportWithParameter(ref crviewer, "rp_" + sSrc + ".rpt", lParamFG);
                break;
            //case "claimMonthly":
            //    lparam.Add(new cArrayList("@salespointcd", Request.QueryString["branch"]));
            //    lparam.Add(new cArrayList("@claim_sta_id", Request.QueryString["status"]));
            //    lparam.Add(new cArrayList("@month", Request.QueryString["month"]));
            //    lparam.Add(new cArrayList("@year", Request.QueryString["year"]));

            //    vShowReportWithParameter(ref crviewer, "rp_claimMonthly.rpt", lparam);
            //    break;
            case "claimdtl":
                lparam.Add(new cArrayList("@salespointcd", Request.QueryString["branch"]));
                lparam.Add(new cArrayList("@month", Request.QueryString["month"]));
                lparam.Add(new cArrayList("@year", Request.QueryString["year"]));
                vShowReportWithParameter(ref crviewer, "rp_claimstatus.rpt", lparam);
                break;
            case "jared":
                List<cArrayList> lParamjared = (List<cArrayList>)Session["lParamjared"];
                vShowReportWithParameter(ref crviewer, "rp_jared.rpt", lParamjared);
                break;
            case "sdc":
                List<cArrayList> lParamsdc = (List<cArrayList>)Session["lParamsdc"];
                vShowReportWithParameter(ref crviewer, "rp_" + sSrc + ".rpt", lParamsdc);
                break;
            case "salesofcustomerbyproductcontract":
            case "salesofsalesmanproductbysls":
            case "salesbybranchbyproductcontract":
            case "salesbysalesmanbyproductcontract":
            case "salesreturnofbranch":
            case "salesofbranchbyotlcdprod":
            case "salesofbranchbyotlcd":
            case "salesbybranchbyproduct":
            case "salesbybranchbyitem":
            case "salesofsalesman":
            case "salesofsalesmanproduct":
            case "salesofcustbyslsman":
            case "salesofcustbyslsman_byprod":
            case "monthlysales":
            case "salesofcustomer":
            case "salesofcustomeritem":
                List<cArrayList> lparamsls = (List<cArrayList>)Session["lParamsls"];
                vShowReportWithParameter(ref crviewer, "rp_" + sSrc + ".rpt", lparamsls);
                break;
            case "ccnr":
                List<cArrayList> lparamccnr = (List<cArrayList>)Session["lparamccnr"];
                vShowReportWithParameter(ref crviewer, "rp_ccnr.rpt", lparamccnr);
                break;


            case "boksan":
                vShowReport(ref crviewer, "rp_bookingsanad.rpt");
                break;

            case "stocksch":
                List<cArrayList> lParamstocksch = (List<cArrayList>)Session["lParamstocksch"];
                vShowReportWithParameter(ref crviewer, "rp_stockschedule.rpt", lParamstocksch);
                break;

            case "soa":
                string dtsoa1 = Request.QueryString["startdate"];
                string dtsoa2 = Request.QueryString["enddate"];
                DateTime dtpsoa1 = DateTime.ParseExact(dtsoa1, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime dtpsoa2 = DateTime.ParseExact(dtsoa2, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                lparam.Add(new cArrayList("@startdate", dtpsoa1.Year.ToString() + "," + dtpsoa1.Month.ToString("00") + "," + dtpsoa1.Day.ToString("00")));
                lparam.Add(new cArrayList("@enddate", dtpsoa2.Year.ToString() + "," + dtpsoa2.Month.ToString("00") + "," + dtpsoa2.Day.ToString("00")));

                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                lparam.Add(new cArrayList("@salespointcd", Request.QueryString["salsp"]));
                lparam.Add(new cArrayList("@salesman_cd", Request.QueryString["salesman"]));
                string p = Request.QueryString["p"];
                if (p == "0")
                {
                    if (Request.QueryString["cust"] != "")
                    {
                        lparam.Add(new cArrayList("@cust_cd", Request.QueryString["cust"]));
                        vShowReportWithParameter(ref crviewer, "rp_soa1.rpt", lparam);
                    }
                    else
                    {
                        lparam.Add(new cArrayList("@cust_cd", null));
                        vShowReportWithParameter(ref crviewer, "rp_soa1all.rpt", lparam);
                    }

                }
                else
                {
                    if (Request.QueryString["cust"] != "")
                    {
                        lparam.Add(new cArrayList("@cust_cd", Request.QueryString["cust"]));
                        vShowReportWithParameter(ref crviewer, "rp_soa2s.rpt", lparam);
                    }
                    else
                    {
                        lparam.Add(new cArrayList("@cust_cd", null));
                        vShowReportWithParameter(ref crviewer, "rp_soa2sall.rpt", lparam);
                    }
                    // vShowReportWithParameter(ref crviewer, "rp_soa2.rpt", lparam);
                }
                break;
            //case "py":
            //    lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
            //    lformula.Add("{tmst_payment.payment_no} = '" + Request.QueryString["noy"] + "'");
            //    vShowReport(ref crviewer, "rp_paymentreceipt2.rpt", lformula, lparam);
            //    break;
            case "croreq":
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                lparam.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //{tcashregout.casregout_cd} = "CO00002"
                //lformula.Add("{tcashout_request.cashout_cd} = '" + Request.QueryString["no"] + "'");
                //lformula.Add("{tcashout_request.salespointcd} = '" + Request.Cookies["sp"].Value.ToString() + "'");
                lparam.Add(new cArrayList("p_cashoutype", bll.vLookUp("select fld_desc from tfield_value where fld_nm='cashout_typ' and fld_valu=(select cashout_typ from tcashregout_dtl where casregout_cd='" + Request.QueryString["no"] + "')")));
                lparam.Add(new cArrayList("@cashout_cd", Request.QueryString["no"]));
                vShowReportWithParameter(ref crviewer, "rp_cashregrequest.rpt", lparam, lformula);
                break;
            case "saltar":
                string speriod = Request.QueryString["period"];
                string ssalesman = Request.QueryString["scd"];
                lparam.Add(new cArrayList("@period", Request.QueryString["period"]));
                lparam.Add(new cArrayList("@salesman_cd", Request.QueryString["scd"]));
                lparam.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //lformula.Add("{tsalestargetsalespoint.period} = '" + speriod + "' and {tsalestargetsalespoint.salesman_cd} = '" + ssalesman + "'");
                //vShowReport(ref crviewer, "rp_salestargetsalespoint.rpt", lformula);
                vShowReportWithParameter(ref crviewer, "rp_salestargetsalespoint.rpt", lparam);
                break;
            case "stockconfirm":
                List<cArrayList> lParamstockconfirm = (List<cArrayList>)Session["lParamstockconfirm"];
                vShowReportWithParameter(ref crviewer, "rptstockconfirm.rpt", lParamstockconfirm);
                break;
            case "sto":
                //string ssperiod = Request.QueryString["period"];
                //string ssalespointcd = Request.QueryString["salespointcd"];
                //lformula.Add("{tsalestargetho.period} = '" + ssperiod + "' and {tsalestargetho.salespointcd} = '" + ssalespointcd + "'");
                lparam.Add(new cArrayList("@period", Request.QueryString["period"]));
                lparam.Add(new cArrayList("@salespointcd", Request.QueryString["salespointcd"]));
                vShowReportWithParameter(ref crviewer, "rp_salestargetho.rpt", lparam);
                //vShowReport(ref crviewer, "rp_salestargetho.rpt", lformula);
                break;
            case "stkmon1":
                List<cArrayList> lParamstkmon1 = (List<cArrayList>)Session["lParamstkmon1"];
                vShowReportWithParameter(ref crviewer, "rp_stockmonitoring_dtl.rpt", lParamstkmon1);
                break;
            case "salestargetsp":
                List<cArrayList> lParamsalestargetsp = (List<cArrayList>)Session["lParamsalestargetsp"];
                vShowReportWithParameter(ref crviewer, "rp_salestargetsp.rpt", lParamsalestargetsp);
                break;

            case "DSR":
                List<cArrayList> lParamDSR = (List<cArrayList>)Session["lParamDSR"];
                vShowReportWithParameter(ref crviewer, "rp_driverDSR.rpt", lParamDSR);
                break;
            case "collduedt":
                List<cArrayList> lParamcolldudt = (List<cArrayList>)Session["lParamcolldudt"];
                lParamcolldudt.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                string rptyp = Request.QueryString["rptyp"];
                if (rptyp == "0")
                {
                    vShowReportWithParameter(ref crviewer, "rp_collectionduedatebysls.rpt", lParamcolldudt);
                }
                else
                {
                    vShowReportWithParameter(ref crviewer, "rp_collectionduedatebycst.rpt", lParamcolldudt);
                }
                break;
            case "stkmon":
                List<cArrayList> lParamstkmon = (List<cArrayList>)Session["lParamstkmon"];
                //DateTime dtstkmon = DateTime.ParseExact(Request.Cookies["waz_dt"].Value, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                //lParamstkmon.Add(new cArrayList("@so_dt", dtstkmon.Year.ToString() + "," + dtstkmon.Month.ToString() + "," + dtstkmon.Day.ToString()));
                vShowReportWithParameter(ref crviewer, "rp_stockmonitoring.rpt", lParamstkmon);
                break;

            case "pinv":
                lparam.Add(new cArrayList("@salespoint_cd", Request.QueryString["sap"].ToString()));
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                vShowReportWithParameter(ref crviewer, "rp_pedndinginvoices.rpt", lparam);
                break;

            case "collec":
                //string collec = Request.QueryString["dt"];
                //DateTime collec1 = DateTime.ParseExact(collec, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                //lparam.Add(new cArrayList("@dt", collec1.Year.ToString() + "," + collec1.Month.ToString("00") + "," + collec1.Day.ToString("00")));
                lparam.Add(new cArrayList("@salespointcd", Request.QueryString["spcd"].ToString()));
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                vShowReportWithParameter(ref crviewer, "rp_collection.rpt", lparam);
                break;
            case "duedate":
                string duedate = Request.QueryString["dt"];
                DateTime duedate1 = DateTime.ParseExact(duedate, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                lparam.Add(new cArrayList("@inv_dt", duedate1.Year.ToString() + "," + duedate1.Month.ToString("00") + "," + duedate1.Day.ToString("00")));
                lparam.Add(new cArrayList("@salespointcd", Request.QueryString["spcd"].ToString()));
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                vShowReportWithParameter(ref crviewer, "rp_duedate.rpt", lparam);
                break;
            case "rpout2":
                List<cArrayList> lParamrpout2 = (List<cArrayList>)Session["lParamrpout2"];
                vShowReportWithParameter(ref crviewer, "rp_invoutletanddatebycusgrcd.rpt", lParamrpout2);
                break;
            case "custtran":
                string sCT = Request.QueryString["no"];
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                lparam.Add(new cArrayList("@trf_no", Request.QueryString["no"].ToString()));
                vShowReportWithParameter(ref crviewer, "rp_transfercustomer.rpt", lparam);
                break;
            case "custtranBeforeApp":
                string sCT1 = Request.QueryString["no"];
                lparam.Add(new cArrayList("@trf_no", sCT1));//rp_transfercustomerBeforeAppr
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                vShowReportWithParameter(ref crviewer, "CrystalReport3.rpt", lparam);
                break;
            case "stkvan":
                DateTime dtvan = DateTime.ParseExact(Request.QueryString["dt"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                lformula.Add("{tstock_vanbalance.balance_dt} = Date (" + dtvan.Year.ToString() + "," + dtvan.Month.ToString() + "," + dtvan.Day.ToString() + ")");
                vShowReport(ref crviewer, "rp_vanstock.rpt", lformula);
                break;
            case "stbal":
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vBatchStockBalance(arr);
                DateTime dtstock = DateTime.ParseExact(Request.Cookies["waz_dt"].Value, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                lformula.Add("{tstock_balance.balance_dt} = Date (" + dtstock.Year.ToString() + "," + dtstock.Month.ToString() + "," + dtstock.Day.ToString() + ")");
                vShowReport(ref crviewer, "rp_stockbalance.rpt", lformula);
                break;
            case "so":
                //  {tmst_salesorder.so_cd} = "SO00001"
                //  List<string> lformula = new List<string>();
                lformula.Add("{vtmst_salesorder.salespointcd} = '" + Request.Cookies["sp"].Value.ToString() + "'");
                lformula.Add("{vtmst_salesorder.so_cd} = '" + Request.QueryString["so"] + "'");
                vShowReport(ref crviewer, "rp_salesorder.rpt", lformula);
                break;
            case "lo":
                //   List<string> lformuallo = new List<string>;
                lformula.Add("{tmst_salesorder.so_cd} = '" + Request.QueryString["so"] + "'");
                vShowReport(ref crviewer, "rp_salesloading.rpt", lformula);
                break;
            case "in":
                lformula.Add("{tdosales_invoice.inv_no} = '" + Request.QueryString["no"] + "'");
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                vShowReportWithParameter(ref crviewer, "rp_invoicesales.rpt", lparam, lformula);
                break;
            case "GRHO":
                List<cArrayList> lParamGRHO = (List<cArrayList>)Session["lparamGRHO"];
                vShowReportWithParameter(ref crviewer, "rpGoodReceiptHO.rpt", lParamGRHO);
                break;
            case "StockOpname":
                List<cArrayList> lParamStockOpname = (List<cArrayList>)Session["lParamStockOpname"];
                vShowReportWithParameter(ref crviewer, "rp_stockOpnameForm.rpt", lParamStockOpname);
                break;
            case "sop":
                List<cArrayList> lParamsop = (List<cArrayList>)Session["lParamsop"];
                vShowReportWithParameter(ref crviewer, "rp_stockopname.rpt", lParamsop);
                break;
            case "retHO":
                List<cArrayList> lParamretHO = (List<cArrayList>)Session["lParamretHO"];
                vShowReportWithParameter(ref crviewer, "rp_returHO.rpt", lParamretHO);
                break;
            //case "retHO1":
            //    List<cArrayList> lParamretHO1 = (List<cArrayList>)Session["lParamretHO1"];
            //    vShowReportWithParameter(ref crviewer, "rp_returHO1.rpt", lParamretHO1);
            //    break;
            case "recRetHO":
                List<cArrayList> lParamrecRetHO = (List<cArrayList>)Session["lParamrecRetHO"];
                vShowReportWithParameter(ref crviewer, "rp_recReturHO.rpt", lParamrecRetHO);
                break;
            //case "invto":
            //    sPrintCount = bll.vLookUp("select isnull(qty,0) from tprint_control where print_cd='TOINV' and doc_no='" + Request.QueryString["no"] + "'");
            //    lformula.Add("{trpt_dosalesinvoice.inv_no} = '" + Request.QueryString["no"] + "' and {trpt_dosalesinvoice.usr_id} = '" + Request.Cookies["usr_id"].Value.ToString() + "'");
            //    lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
            //    if (sPrintCount == "")
            //    {
            //        lparam.Add(new cArrayList("p_print", ""));
            //    }
            //    else { lparam.Add(new cArrayList("p_print", "RE-PRINTED No. " + sPrintCount)); }
            //    vShowReportWithParameter(ref crviewer, "rp_dosalesinvoice3.rpt", lparam, lformula);
            //    break;
            //case "inv":
            //    sPrintCount = bll.vLookUp("select qty from tprint_control where print_cd='CANVINV' and doc_no='" + Request.QueryString["no"] + "'");
            //    lformula.Add("{trpt_dosalesinvoice.inv_no} = '" + Request.QueryString["no"] + "' and {trpt_dosalesinvoice.usr_id} = '" + Request.Cookies["usr_id"].Value.ToString() + "'");
            //    lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
            //    if (sPrintCount == "")
            //    {
            //        lparam.Add(new cArrayList("p_print", ""));
            //    }
            //    else { lparam.Add(new cArrayList("p_print", "RE-PRINTED No. " + sPrintCount)); }

            //    lparam.Add(new cArrayList("p_rps", bll.vLookUp("select dbo.fn_getrps('" + Request.QueryString["no"] + "')")));
            //    vShowReportWithParameter(ref crviewer, "rp_dosalesinvoice3.rpt", lparam, lformula);
            //    break;
            case "invf":
                lformula.Add("{tdosalesinvoice_free.inv_no} = '" + Request.QueryString["noi"] + "'");
                lformula.Add("{tdosalesinvoice_free.salespointcd} = '" + Request.Cookies["sp"].Value.ToString() + "'");
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                vShowReportWithParameter(ref crviewer, "rp_invoicefree2.rpt", lparam, lformula);
                break;
            //case "soa":
            //     string dtsoa1 = Request.QueryString["startdate"];
            //     string dtsoa2 = Request.QueryString["enddate"];
            //     DateTime dtpsoa1 = DateTime.ParseExact(dtsoa1, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //     DateTime dtpsoa2 = DateTime.ParseExact(dtsoa2, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //     lparam.Add(new cArrayList("@startdate", dtpsoa1.Year.ToString() + "," + dtpsoa1.Month.ToString("00") + "," + dtpsoa1.Day.ToString("00")));
            //     lparam.Add(new cArrayList("@enddate", dtpsoa2.Year.ToString() + "," + dtpsoa2.Month.ToString("00") + "," + dtpsoa2.Day.ToString("00")));

            //     lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
            //     lparam.Add(new cArrayList("@salespointcd", Request.QueryString["salsp"]));
            //     lparam.Add(new cArrayList("@salesman_cd", Request.QueryString["salesman"]));                
            //     string p = Request.QueryString["p"];
            //     if (p == "0")
            //     {
            //         if (Request.QueryString["cust"]!="")
            //         {
            //             lparam.Add(new cArrayList("@cust_cd", Request.QueryString["cust"]));
            //             vShowReportWithParameter(ref crviewer, "rp_soa1.rpt", lparam);
            //         }else{
            //             lparam.Add(new cArrayList("@cust_cd", null));
            //             vShowReportWithParameter(ref crviewer, "rp_soa1all.rpt", lparam);
            //         }

            //     }
            //     else
            //     {

            //         vShowReportWithParameter(ref crviewer, "rp_soa2.rpt", lparam);
            //     }
            //     break;
            // case "py":
            //     lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
            //     lformula.Add("{tmst_payment.payment_no} = '" + Request.QueryString["noy"] + "'");
            //     vShowReport(ref crviewer, "rp_paymentreceipt2.rpt",  lformula, lparam);
            //     break;
            case "riv":
                //vShowReport(ref crviewer, "rp_rekapinvoice.rpt");
                vShowReportWithParameter(ref crviewer, "rp_rekapinvoice.rpt", lparam);
                break;
            case "is":
                lformula.Add("{tdosales_invoice.salesman_cd} = '" + Request.QueryString["sa"] + "'");
                vShowReport(ref crviewer, "rp_invbysalesman.rpt", lformula);
                break;
            case "cu":
                vShowReport(ref crviewer, "rp_mstcustomer.rpt");
                break;
            case "em":
                vShowReport(ref crviewer, "rp_mstemployee.rpt");
                break;
            case "dc":
                lformula.Add("{tmst_discount.disc_cd} = '" + Request.QueryString["no"] + "'");
                vShowReport(ref crviewer, "rp_discount.rpt", lformula);
                break;
            case "stock":
                //List<cArrayList> lParamstock = (List<cArrayList>)Session["lParamstock"];
                //vShowReportWithParameter(ref crviewer, "rptStock.rpt", lParamstock);
                lparam.Add(new cArrayList("@whs_cd", Request.QueryString["w"]));
                lparam.Add(new cArrayList("@bin_cd", Request.QueryString["b"]));
                lparam.Add(new cArrayList("@period", Request.QueryString["p"]));
                vShowReportWithParameter(ref crviewer, "rp_stockmovement2.rpt", lparam);
                break;
            case "AccstockDtl":
                List<cArrayList> lParamAccstockDtl = (List<cArrayList>)Session["lParamAccstockDtl"];
                vShowReportWithParameter(ref crviewer, "rptStockAccDtl.rpt", lParamAccstockDtl);
                break;
            case "AccstockSumm":
                List<cArrayList> lParamAccstockSumm = (List<cArrayList>)Session["lParamAccstockSumm"];
                vShowReportWithParameter(ref crviewer, "rptStockAccSumm.rpt", lParamAccstockSumm);
                break;
            case "StockInvoice":
                List<cArrayList> lParamStockInvoice = (List<cArrayList>)Session["lParamStockInvoice"];
                vShowReportWithParameter(ref crviewer, "rptStockInvoice.rpt", lParamStockInvoice);
                break;
            case "StockInvoiceLV":
                List<cArrayList> lParamStockInvoiceLV = (List<cArrayList>)Session["lParamStockInvoiceLV"];
                vShowReportWithParameter(ref crviewer, "rptStockInvoiceLV.rpt", lParamStockInvoiceLV);
                break;
            case "stockTransaction":
                List<cArrayList> lParamstockTransaction = (List<cArrayList>)Session["lParamstockTransaction"];
                // DateTime _startdate = Convert.ToDateTime(Request.QueryString["start"]);
                // DateTime _enddate = Convert.ToDateTime(Request.QueryString["end"]);
                // DateTime _startmonth = Convert.ToDateTime(Request.QueryString["from"]);
                //foreach(cArrayList _arr in lParamstockTransaction)
                //{
                //   if (_arr.paramid == "@dtStart")
                //{
                //   _arr.ParamValue = _startdate;
                //}
                //if (_arr.paramid == "@dtEnd")
                // {
                //  _arr.ParamValue = _enddate;
                //}
                //if (_arr.paramid== "@dtFrom")
                //{
                //     _arr.ParamValue = _startmonth;
                //   }
                // }
                //lParamstockTransaction.Add(new cArrayList("@dtStart", _startdate));
                //lParamstockTransaction.Add(new cArrayList("@dtEnd", _enddate));
                //lParamstockTransaction.Add(new cArrayList("@dtFrom", _startmonth));
                vShowReportWithParameter(ref crviewer, "rptStockTransaction.rpt", lParamstockTransaction);
                break;
            case "it":
                lparam.Add(new cArrayList("@printby", Request.Cookies["usr_id"].Value.ToString()));
                lparam.Add(new cArrayList("@trf_no", Request.QueryString["no"]));
                lparam.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                vShowReportWithParameter(ref crviewer, "rptInternalTranfer2.rpt", lparam);
                //List<cArrayList> lParamit = (List<cArrayList>)Session["lParamit"];
                //vShowReportWithParameter(ref crviewer, "rptInternalTranfer.rpt", lParamit);
                break;
            case "rd":
                // if (Request.QueryString["sta"] == "all")
                //  {
                //vShowReport(ref crviewer, "rp_mstdiscountall.rpt");
                lformula.Add("{trpt_mstdiscount.usr_id} = '" + Request.Cookies["usr_id"].Value.ToString() + "'");
                vShowReport(ref crviewer, "rp_discount2.rpt", lformula);
                // }
                // else
                // {
                //     List<cArrayList> arr = new List<cArrayList>();
                //     lformula.Add("{tmst_discount.disc_sta_id} = '" + Request.QueryString["sta"] + "'");
                //lparam.Add(Request.Cookies["usr_id"].Value.ToString());
                //      vShowReport(ref crviewer, "rp_discountstatus.rpt", lformula );
                //  }
                break;
            case "pr":
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                vShowReportWithParameter(ref crviewer, "rptcustomertypeprice.rpt", lparam);
                break;
            case "stockSumm1":
            case "stockSumm":
                List<cArrayList> lParamstockSumm = (List<cArrayList>)Session["lParamstockSumm"];
                vShowReportWithParameter(ref crviewer, "rpt" + sSrc + ".rpt", lParamstockSumm);
                break;
            case "trnstk":
                List<cArrayList> lparamtrnstk = (List<cArrayList>)Session["lparamtrnstk"];
                vShowReportWithParameter(ref crviewer, "rptTranStock_uom.rpt", lparamtrnstk);
                break;
            //case "croreq":
            //    //{tcashregout.casregout_cd} = "CO00002"
            //    lformula.Add("{tcashregout.casregout_cd} = '" + Request.QueryString["no"] + "'");
            //    vShowReport(ref crviewer, "rp_cashregrequest.rpt", lformula);
            //    break;
            case "GRPVEN":
                lparam.Clear();
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                vShowReportWithParameter(ref crviewer, "rp_vendorgroup.rpt", lparam);
                break;
            case "MSTSL":
                lparam.Clear();
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                vShowReportWithParameter(ref crviewer, "rp_mstitem2.rpt", lparam);
                break;
            case "darow":
                string dtrow = Request.QueryString["date"];
                DateTime dtrow1 = DateTime.ParseExact(dtrow, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                lparam.Add(new cArrayList("@dtord1", dtrow1.Year.ToString() + "-" + dtrow1.Month.ToString("00") + "-" + dtrow1.Day.ToString("00")));
                string dtrow2 = Request.QueryString["date1"];
                DateTime dtrowd2 = DateTime.ParseExact(dtrow2, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                rawtyp = Request.QueryString["type"];
                lparam.Add(new cArrayList("@dtord2", dtrowd2.Year.ToString() + "-" + dtrowd2.Month.ToString("00") + "-" + dtrowd2.Day.ToString("00")));
                lparam.Add(new cArrayList("@salespointcd", Request.QueryString["sp"]));
                //if (rawtyp == "0")
                // {
                //   vShowReportWithParameter(ref crviewer, "rp_salesdatarow2n.rpt", lparam);
                // }
                if (rawtyp == "0")
                {
                    vShowReportWithParameter(ref crviewer, "rp_vatrawdata.rpt", lparam);
                }
                else if (rawtyp == "1")
                {
                    vShowReportWithParameter(ref crviewer, "rp_vatrawdata.rpt", lparam);
                }
                else
                { vShowReportWithParameter(ref crviewer, "rp_rawdataclaim.rpt", lparam); }
                break;
            case "msveh":
                lparam.Clear();
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                vShowReportWithParameter(ref crviewer, "rp_mstvehiclelist.rpt", lparam);
                break;

            case "mswhs":
                lparam.Clear();
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                vShowReportWithParameter(ref crviewer, "rp_tmstwarehouse.rpt", lparam);
                break;
            case "mschout":
                lparam.Clear();
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                vShowReportWithParameter(ref crviewer, "rp_tmst_itemcashout.rpt", lparam);
                break;
            case "stkadj":
                List<cArrayList> lParamstkadjno = (List<cArrayList>)Session["lParamstkadjno"];
                vShowReportWithParameter(ref crviewer, "rptStockAdj.rpt", lParamstkadjno);
                break;
            case "po":
                List<string> lformulapo = (List<string>)Session["lformula"];
                lparam.Add(new cArrayList("@po_no", Request.QueryString["no"]));
                vShowReportWithParameter(ref crviewer, "rppo.rpt", lparam);
                break;
            //case "rpay"://{tmst_payment.payment_dt} = Date (2015, 10, 01)
            //    string dt= Request.QueryString["tg"];
            //    lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
            //    DateTime dtpay = DateTime.ParseExact(dt, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //    lformula.Add("DateTime({tmst_payment.payment_dt})=Date(" + dtpay.Year.ToString() +  "," + dtpay.Month.ToString("00") + "," + dtpay.Day.ToString("00") +  ") and {tmst_payment.payment_sta_id} <> 'L'");
            //  vShowReport(ref crviewer, "rp_rekappayment.rpt",lparam);
            //vShowReportWithParameter(ref crviewer, "rp_rekappayment.rpt", lparam, lformula);
            //break;
            case "ARCN":
                List<cArrayList> lParamarcn_no = (List<cArrayList>)Session["lParamarcn_no"];
                vShowReportWithParameter(ref crviewer, "rptARCNDN.rpt", lParamarcn_no);
                break;
            case "spt":
                string sNo = Request.QueryString["no"];
                //formula.Add("{tsalestargetsp.yearcd} = " + Request.QueryString["y"] + " and {tsalestargetsp.monthcd} = " + Request.QueryString["m"]);
                lformula.Add("{tsalestargetsp.target_no} = '" + sNo + "'");
                vShowReport(ref crviewer, "rp_salestargetsp.rpt");
                break;
            //case "listcustbysalesman":
            //    List<cArrayList> lParamlistcustbysalesman = (List<cArrayList>)Session["lParamlistcustbysalesman"];
            //    vShowReportWithParameter(ref crviewer, "rptlistcustbysalesman.rpt", lParamlistcustbysalesman);
            //    break;
            case "sumsale":
                List<cArrayList> lParamsumsale = (List<cArrayList>)Session["lParamsumsale"];
                vShowReportWithParameter(ref crviewer, "rp_sumsabprdsman.rpt", lParamsumsale);
                break;
            case "dailyinvrecp":
                string dt1 = Request.QueryString["startdate"];
                string dt2 = Request.QueryString["enddate"];
                DateTime dtpay1 = DateTime.ParseExact(dt1, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime dtpay2 = DateTime.ParseExact(dt2, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                lparam.Add(new cArrayList("@startdate", dtpay1.Year.ToString() + "," + dtpay1.Month.ToString("00") + "," + dtpay1.Day.ToString("00")));
                lparam.Add(new cArrayList("@enddate", dtpay2.Year.ToString() + "," + dtpay2.Month.ToString("00") + "," + dtpay2.Day.ToString("00")));
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                vShowReportWithParameter(ref crviewer, "rp_dailyinvoicevoucher.rpt", lparam);
                break;
            case "rtn":
                lparam.Add(new cArrayList("@retur_no", Request.QueryString["returno"]));
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                //string sRemark = "";
                //sRemark = bll.vLookUp("select remark from tsalesreturn where retur_no='"+Request.QueryString["returno"]+"'");
                //sRemark = bll.vLookUp("select reasn_nm from tmst_reason where reasn_typ='return' and reasn_cd='"+sRemark+"'");
                //lparam.Add(new cArrayList("p_remark", sRemark));                
                vShowReportWithParameter(ref crviewer, "rp_salesreturn.rpt", lparam);
                break;
            case "so1":
                //  {tmst_salesorder.so_cd} = "SO00001"
                //  List<string> lformula = new List<string>();
                //lformula.Add("{vtmst_salesorder.salespointcd} = '" + Request.Cookies["sp"].Value.ToString() + "'");
                //lformula.Add("{vtmst_salesorder.so_cd} = '" + Request.QueryString["so"] + "'");
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                lparam.Add(new cArrayList("@so_cd", Request.QueryString["so"]));
                lparam.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                vShowReportWithParameter(ref crviewer, "rp_salesorderp.rpt", lparam, lformula);

                break;

            //case "invto1":
            //    sPrintCount = bll.vLookUp("select isnull(qty,0) from tprint_control where print_cd='TOINV' and doc_no='" + Request.QueryString["no"] + "'");
            //    lformula.Add("{trpt_dosalesinvoice.inv_no} = '" + Request.QueryString["no"] + "' and {trpt_dosalesinvoice.usr_id} = '" + Request.Cookies["usr_id"].Value.ToString() + "'");
            //    lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
            //    if (sPrintCount == "")
            //    {
            //        lparam.Add(new cArrayList("p_print", ""));
            //    }
            //    else { lparam.Add(new cArrayList("p_print", "RE-PRINTED No. " + sPrintCount)); }
            //    vShowReportWithParameter(ref crviewer, "rp_dosalesinvoice3p.rpt", lparam, lformula);
            //    break;

            case "invf2":
                lformula.Add("{tdosalesinvoice_free.inv_no} = '" + Request.QueryString["noi"] + "'");
                lformula.Add("{tdosalesinvoice_free.salespointcd} = '" + Request.Cookies["sp"].Value.ToString() + "'");
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                vShowReportWithParameter(ref crviewer, "rp_dosalesinvoice3freeinv.rpt", lparam, lformula);
                break;
            //case "summtosa":
            //     string dtsummtosa1 = Request.QueryString["startdate"];
            //     string dtsummtosa2 = Request.QueryString["enddate"];
            //     DateTime dtpaysummtosa1 = DateTime.ParseExact(dtsummtosa1, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //     DateTime dtpaysummtosa2 = DateTime.ParseExact(dtsummtosa2, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //     lparam.Add(new cArrayList("@startdate", dtpaysummtosa1.Year.ToString() + "," + dtpaysummtosa1.Month.ToString("00") + "," + dtpaysummtosa1.Day.ToString("00")));
            //     lparam.Add(new cArrayList("@enddate", dtpaysummtosa2.Year.ToString() + "," + dtpaysummtosa2.Month.ToString("00") + "," + dtpaysummtosa2.Day.ToString("00")));
            //     lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
            //     lparam.Add(new cArrayList("@salespointcd", Request.QueryString["sap"]));
            //     string pptype1 = Request.QueryString["ptyp"];
            //     if (pptype1 == "0")
            //     {
            //         vShowReportWithParameter(ref crviewer, "rp_sumtakeordercollfromcustbysals.rpt", lparam);
            //     }
            //     else {
            //         vShowReportWithParameter(ref crviewer, "rp_balancecustomer.rpt", lparam);
            //     }

            //     break;
            case "closingstockjaretmonthly":
                lparam.Add(new cArrayList("@period", Request.QueryString["period"]));
                vShowReportWithParameter(ref crviewer, "rptclosingstockjaretmonthly.rpt", lparam);
                break;
            //case "salesreturnofbranch":
            //case "salesofbranchbyotlcdprod":
            //case "salesofbranchbyotlcd":
            //case "salesbybranchbyproduct":
            //case "salesbybranchbyitem":
            //case "salesofsalesman":
            //case "salesofsalesmanproduct":
            //case "salesofcustbyslsman":
            //case "salesofcustbyslsman_byprod":
            //case "monthlysales":
            //     List<cArrayList> lparamsls = (List<cArrayList>)Session["lParamsls"];
            //     vShowReportWithParameter(ref crviewer, "rp_" + sSrc + ".rpt", lparamsls);
            //     break;

            case "balan":
                lparam.Clear();
                //lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));                
                string dtbln = Request.QueryString["startdate"];
                string ddend = Request.QueryString["enddate"];
                DateTime dtblnce = DateTime.ParseExact(dtbln, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime dtblncee = DateTime.ParseExact(ddend, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                lformula.Add("{trpt_customerbalance.usr_id} = '" + Request.Cookies["usr_id"].Value.ToString() + "'");
                lparam.Add(new cArrayList("p_startdate", dtblnce.ToShortDateString()));
                //lparam.Add(new cArrayList("p_enddate", dtblncee.ToShortDateString()));
                // vShowReportWithParameter(ref crviewer, "rp_balancecustomer.rpt", lparam, lformula);
                vShowReport(ref crviewer, "rp_balancecustomer.rpt", lformula, lparam);
                break;

            case "cust":
                lformula.Add("{tmst_customer.cust_cd} = '" + Request.QueryString["cust_cd"] + "'");
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                vShowReportWithParameter(ref crviewer, "rp_customer.rpt", lparam, lformula);
                break;
            case "clm":
                string dtclm1 = Request.QueryString["dtstart"];
                string dtclm2 = Request.QueryString["dtend"];
                DateTime dtclaim1 = DateTime.ParseExact(dtclm1, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime dtclaim2 = DateTime.ParseExact(dtclm2, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                lparam.Add(new cArrayList("@dtstart", dtclaim1.Year.ToString() + "," + dtclaim1.Month.ToString("00") + "," + dtclaim1.Day.ToString("00")));
                lparam.Add(new cArrayList("@dtend", dtclaim2.Year.ToString() + "," + dtclaim2.Month.ToString("00") + "," + dtclaim2.Day.ToString("00")));
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                string clm = Request.QueryString["p"];
                if (clm == "0")
                {
                    vShowReportWithParameter(ref crviewer, "rp_claimlist.rpt", lparam);
                }
                break;
            case "cb":
                string sPeriod = Request.QueryString["prd"];
                lformula.Add("{tmonthend_custbalance.period} = '" + sPeriod + "'");
                vShowReport(ref crviewer, "rp_custbalance.rpt", lformula, lparam);
                break;
            case "goodreceiptbyreference":
                List<cArrayList> lParamgoodreceiptbyreference = (List<cArrayList>)Session["lParamgoodreceiptbyreference"];
                vShowReportWithParameter(ref crviewer, "rptgoodreceiptbyinv.rpt", lParamgoodreceiptbyreference);
                break;
            case "balnperf":
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                string rep2 = Request.QueryString["rep"];
                if (rep2 == "0")
                {
                    lformula.Add("{tmonthend_custbalance.cust_cd} = '" + Request.QueryString["cust"] + "'");
                    vShowReportWithParameter(ref crviewer, "rp_perf_customer_end_balance1.rpt", lparam, lformula);
                }
                else
                {
                    lformula.Add("{sp_monthendsalesmanbalance;1.salesman_cd} = '" + Request.QueryString["salesman"] + "'");
                    vShowReportWithParameter(ref crviewer, "rp_perf_salesman_end_balance.rpt", lparam, lformula);
                }
                break;
            case "stmtconf":
                lformula.Add("{tmst_balanceconfirmation.stmt_no}  = '" + Request.QueryString["no"] + "'");
                vShowReport(ref crviewer, "rp_customer_balanconf.rpt", lformula);
                break;
            case "SalestargetVsActualchart":
                List<cArrayList> lParamSalestargetVsActualchart = (List<cArrayList>)Session["lParamSalestargetVsActualchart"];
                vShowReportWithParameter(ref crviewer, "rptSalestargetVsActualchart.rpt", lParamSalestargetVsActualchart);
                break;
            case "Salesbymonthly1":
                List<cArrayList> lParamSalesbymonthly1 = (List<cArrayList>)Session["lParamSalesbymonthly1"];
                vShowReportWithParameter(ref crviewer, "rptSalesbymonthly.rpt", lParamSalesbymonthly1);
                break;
            case "Salesbymonthly2":
                List<cArrayList> lParamSalesbymonthly2 = (List<cArrayList>)Session["lParamSalesbymonthly2"];
                vShowReportWithParameter(ref crviewer, "rptSalesbySalesman.rpt", lParamSalesbymonthly2);
                break;
            case "Salesbymonthly3":
                List<cArrayList> lParamSalesbymonthly3 = (List<cArrayList>)Session["lParamSalesbymonthly3"];
                bll.vUpdaterptSalesbyseqment(lParamSalesbymonthly3);
                vShowReport(ref crviewer, "rptSalesbysegment.rpt");
                break;
            case "Salesbymonthly4":
                List<cArrayList> lParamSalesbymonthly4 = (List<cArrayList>)Session["lParamSalesbymonthly4"];
                vShowReportWithParameter(ref crviewer, "rptSalesbysegmentmonthly.rpt", lParamSalesbymonthly4);
                break;
            case "Salesbymonthly5":
                List<cArrayList> lParamSalesbymonthly5 = (List<cArrayList>)Session["lParamSalesbymonthly5"];
                vShowReportWithParameter(ref crviewer, "rptdailystockmovement.rpt", lParamSalesbymonthly5);
                break;
            case "SalesActivityByCustomer":
                List<cArrayList> lParamSalesActivityByCustomer = (List<cArrayList>)Session["lParamSalesActivityByCustomer"];
                vShowReportWithParameter(ref crviewer, "rptSalesActivityByCustomer.rpt", lParamSalesActivityByCustomer);
                break;
            case "mst_asset":
                lparam.Add(new cArrayList("@salespointcd", null));
                lparam.Clear();
                //lparam.Add(new cArrayList("@salespointcd", null));
                lparam.Add(new cArrayList("@asset_typ", null));
                vShowReportWithParameter(ref crviewer, "/master/rp_mast_asset.rpt", lparam);
                break;
            case "tpurchase":
                List<cArrayList> lParampurchase = (List<cArrayList>)Session["lParampurchase"];
                vShowReportWithParameter(ref crviewer, "/master/rp_tpurchase.rpt", lParampurchase);
                break;
            case "depreciation_schedule":
                List<cArrayList> lParamdepreciationschedule = (List<cArrayList>)Session["lParamdepreciationschedule"];
                vShowReportWithParameter(ref crviewer, "rp_acc_depreciation_schedule.rpt", lParamdepreciationschedule);
                break;
            case "acc_stockcard":
                List<cArrayList> lParamstockcard = (List<cArrayList>)Session["lParamstockcard"];
                vShowReportWithParameter(ref crviewer, "rp_acc_stockcard.rpt", lParamstockcard);
                break;
            case "acc_stockcard_all":
                List<cArrayList> lParamstockcardall = (List<cArrayList>)Session["lParamstockcardall"];
                vShowReportWithParameter(ref crviewer, "rp_acc_stockcardAll.rpt", lParamstockcardall);
                break;
            ////
            case "acc_glbyuow":
                List<cArrayList> lParamglbyuow = (List<cArrayList>)Session["lParamglbyuow"];
                vShowReportWithParameter(ref crviewer, "rp_acc_gl_byuow.rpt", lParamglbyuow);
                break;

            case "acc_glalluow":
                List<cArrayList> lParamglalluow = (List<cArrayList>)Session["lParamglalluow"];
                vShowReportWithParameter(ref crviewer, "rp_acc_gl_alluow.rpt", lParamglalluow);
                break;

            case "acc_journal":
                List<cArrayList> lParamjournal = (List<cArrayList>)Session["lParamjournal"];
                //lParamjournal.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                //lParamjournal.Add(new cArrayList("@post_periodbyperiodbyuow", null));
                //lParamjournal.Add(new cArrayList("@salespointcd", null));
                //lParamjournal.Add(new cArrayList("@post_periodbyperiodalluow", null));
                vShowReportWithParameter(ref crviewer, "rp_acc_journal.rpt", lParamjournal);
                break;

            case "acc_journalbyperiodbyuow":
                List<cArrayList> lParamjournalbyperiodbyuow = (List<cArrayList>)Session["lParamjournalbyperiodbyuow"];
                //lParamjournalbyperiodbyuow.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                //lParamjournalbyperiodbyuow.Add(new cArrayList("@journal_no", null));
                //lParamjournalbyperiodbyuow.Add(new cArrayList("@post_periodbyperiodalluow", null));
                vShowReportWithParameter(ref crviewer, "rp_acc_journal_byperiod_byuow.rpt", lParamjournalbyperiodbyuow);
                break;

            case "acc_journalbyperiodalluow":
                List<cArrayList> lParamjournalbyperiodalluow = (List<cArrayList>)Session["lParamjournalbyperiodalluow"];
                //lParamjournalbyperiodalluow.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                //lParamjournalbyperiodalluow.Add(new cArrayList("@journal_no", null));
                //lParamjournalbyperiodalluow.Add(new cArrayList("@post_periodbyperiodbyuow", null));
                //lParamjournalbyperiodalluow.Add(new cArrayList("@salespointcd", null));
                vShowReportWithParameter(ref crviewer, "rp_acc_journal_byperiod_alluow.rpt", lParamjournalbyperiodalluow);
                break;

            case "acc_stockCard_byproductno":
                List<cArrayList> lParamstockcardbyproductno = (List<cArrayList>)Session["lParamstockcardbyproductno"];
                vShowReportWithParameter(ref crviewer, "rp_acc_stockCard_byproductno.rpt", lParamstockcardbyproductno);
                break;

            case "acc_stockCard_byperiodbyuow":
                List<cArrayList> lParamstockcardbyuow = (List<cArrayList>)Session["lParamstockcardbyuow"];
                vShowReportWithParameter(ref crviewer, "rp_acc_stockCard_byuow.rpt", lParamstockcardbyuow);
                break;

            case "acc_stockCard_byperiodalluow":
                List<cArrayList> lParamstockcardalluow = (List<cArrayList>)Session["lParamstockcardalluow"];
                vShowReportWithParameter(ref crviewer, "rp_acc_stockCard_alluow.rpt", lParamstockcardalluow);
                break;

            case "acc_trialBalance":
                List<cArrayList> lParamtrialbalance = (List<cArrayList>)Session["lParamtrialbalance"];
                vShowReportWithParameter(ref crviewer, "rp_acc_trialBalance.rpt", lParamtrialbalance);
                break;

            case "acc_trialBalanceWorkSheet":
                List<cArrayList> lParamtrialbalanceWS = (List<cArrayList>)Session["lParamtrialbalanceWS"];
                vShowReportWithParameter(ref crviewer, "rp_acc_trialBalanceWorkSheet.rpt", lParamtrialbalanceWS);
                break;

            case "acc_financialReportSME":
                List<cArrayList> lParamfinancialReportSME = (List<cArrayList>)Session["lParamfinancialReportSME"];
                vShowReportWithParameter(ref crviewer, "rp_acc_financialReportSME.rpt", lParamfinancialReportSME);
                break;

            case "acc_financialReportTSM":
                List<cArrayList> lParamfinancialReportTSM = (List<cArrayList>)Session["lParamfinancialReportTSM"];
                vShowReportWithParameter(ref crviewer, "rp_acc_financialReportTSM.rpt", lParamfinancialReportTSM);
                break;

            case "acc_financialReportSPR":
                List<cArrayList> lParamfinancialReportSPR = (List<cArrayList>)Session["lParamfinancialReportSPR"];
                vShowReportWithParameter(ref crviewer, "rp_acc_financialReportSPR.rpt", lParamfinancialReportSPR);
                break;

            case "acc_financialReportSME_MTD":
                List<cArrayList> lParamfinancialReportSME_MTD = (List<cArrayList>)Session["lParamfinancialReportSME_MTD"];
                vShowReportWithParameter(ref crviewer, "rp_acc_financialReportSME_MTD.rpt", lParamfinancialReportSME_MTD);
                break;

            case "acc_financialReportTSM_MTD":
                List<cArrayList> lParamfinancialReportTSM_MTD = (List<cArrayList>)Session["lParamfinancialReportTSM_MTD"];
                vShowReportWithParameter(ref crviewer, "rp_acc_financialReportTSM_MTD.rpt", lParamfinancialReportTSM_MTD);
                break;

            case "acc_financialReportSPR_MTD":
                List<cArrayList> lParamfinancialReportSPR_MTD = (List<cArrayList>)Session["lParamfinancialReportSPR_MTD"];
                vShowReportWithParameter(ref crviewer, "rp_acc_financialReportSPR_MTD.rpt", lParamfinancialReportSPR_MTD);
                break;

            case "PettycashCashoutEmpbyDt":
                string sdtpc = Request.QueryString["tg"];
                DateTime dtspc = DateTime.ParseExact(sdtpc, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                lparam.Add(new cArrayList("@cash_dt", dtspc.Year + " - " + dtspc.Month + " - " + dtspc.Day));
                lparam.Add(new cArrayList("@emp_cd", Request.QueryString["emp"]));
                vShowReportWithParameter(ref crviewer, "rp_EmpPettycashCashoutbyDt.rpt", lparam);
                break;
            case "cashierPettycashEmp":
                string sdtse = Request.QueryString["tg"];
                DateTime dtsse = DateTime.ParseExact(sdtse, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                lparam.Add(new cArrayList("@cash_dt", dtsse.Year + " - " + dtsse.Month + " - " + dtsse.Day));
                lparam.Add(new cArrayList("@emp_cd", Request.QueryString["emp"]));
                vShowReportWithParameter(ref crviewer, "rp_cashierPettycashforclaimbyemp.rpt", lparam);
                break;
            case "cashierPettycash":
                string sdtt = Request.QueryString["tg"];
                DateTime dtss = DateTime.ParseExact(sdtt, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                lparam.Add(new cArrayList("@cash_dt", dtss.Year + " - " + dtss.Month + " - " + dtss.Day));
                vShowReportWithParameter(ref crviewer, "rp_cashierPettycashforclaim.rpt", lparam);
                break;
            case "PettycashCashoutEmp":
                lparam.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                lparam.Add(new cArrayList("@PettycashRefno", Request.QueryString["PettycashRefno"]));
                lparam.Add(new cArrayList("@emp_cd", Request.QueryString["emp"]));
                vShowReportWithParameter(ref crviewer, "rp_EmpPettycashCashout.rpt", lparam);
                break;
            case "PettycashCashoutClaimEmp":
                lparam.Add(new cArrayList("p_user", bll.vLookUp("select fullname+', '+emp_cd from tuser_profile where emp_cd='" + Request.Cookies["usr_id"].Value.ToString() + "'")));
                lparam.Add(new cArrayList("@PettycashRefno", Request.QueryString["PettycashRefno"]));
                lparam.Add(new cArrayList("@emp_cd", Request.QueryString["emp"]));
                vShowReportWithParameter(ref crviewer, "rp_EmpPettycashClaimCashout.rpt", lparam);
                break;
            case "acc_employeeCashAdvance":
                List<cArrayList> lParamEmployeeCashAdvance = (List<cArrayList>)Session["lParamEmployeeCashAdvance"];
                vShowReportWithParameter(ref crviewer, "rp_acc_employeeCashAdvance.rpt", lParamEmployeeCashAdvance);
                break;
            case "acc_employeeCashAdvanceSummary":
                List<cArrayList> lParamEmployeeCashAdvanceSummary = (List<cArrayList>)Session["lParamEmployeeCashAdvanceSummary"];
                vShowReportWithParameter(ref crviewer, "rp_acc_employeeCashAdvanceSummary.rpt", lParamEmployeeCashAdvanceSummary);
                break;
            case "acc_stockcardFIFO":
                List<cArrayList> lParamstockcardfifo = (List<cArrayList>)Session["lParamstockcardfifo"];
                vShowReportWithParameter(ref crviewer, "rp_acc_inventory_stockcard_FIFO.rpt", lParamstockcardfifo);
                break;
            case "acc_stockcardFIFOAll":
                List<cArrayList> lParamstockcardfifoall = (List<cArrayList>)Session["lParamstockcardfifoall"];
                vShowReportWithParameter(ref crviewer, "rp_acc_inventory_stockcard_FIFO_All.rpt", lParamstockcardfifoall);
                break;
            case "acc_stockpricelist":
                List<cArrayList> lParamstockpricelist = (List<cArrayList>)Session["lParamstockpricelist"];
                vShowReportWithParameter(ref crviewer, "rp_acc_inventory_stockprice.rpt", lParamstockpricelist);
                break;
            case "acc_ap_ho":
                List<cArrayList> lParamapho = (List<cArrayList>)Session["lParamapho"];
                vShowReportWithParameter(ref crviewer, "rp_acc_ap_ho.rpt", lParamapho);
                break;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            crviewer.ReuseParameterValuesOnRefresh = true;
            crviewer.RefreshReport();
        }
        //  crviewer.ReportSource = null;
        ////  creport crep = new creport();
        //  //if (!IsPostBack)
        //  //{
        //      List<string> lformula = new List<string>();
        //      string sSrc = Request.QueryString["src"].ToString();

        //      switch (sSrc)
        //      {
        //          case "so":
        //              //  {tmst_salesorder.so_cd} = "SO00001"
        //              //  List<string> lformula = new List<string>();
        //              lformula.Add("{tmst_salesorder.so_cd} = '" + Request.QueryString["so"] + "'");
        //              vShowReport(ref crviewer, "rp_salesorder.rpt", lformula);
        //              break;
        //          case "lo":
        //              //   List<string> lformuallo = new List<string>;
        //              lformula.Add("{tmst_salesorder.so_cd} = '" + Request.QueryString["so"] + "'");
        //              vShowReport(ref crviewer, "rp_salesloading.rpt", lformula);
        //              break;
        //          case "in":
        //              lformula.Add("{tdosales_invoice.inv_no} = '" + Request.QueryString["no"] + "'");
        //              vShowReport(ref crviewer, "rp_invoicesales.rpt", lformula);
        //              break;
        //          case "GRHO":
        //              List<cArrayList> lParamGRHO = (List<cArrayList>)Session["lparamGRHO"];
        //              vShowReportWithParameter(ref crviewer, "rpGoodReceiptHO.rpt", lParamGRHO);
        //              break;
        //          case "StockOpname":
        //              List<cArrayList> lParamStockOpname = (List<cArrayList>)Session["lParamStockOpname"];
        //              vShowReportWithParameter(ref crviewer, "rp_stockOpnameForm.rpt", lParamStockOpname);
        //              break;
        //          case "sop":
        //              List<cArrayList> lParamsop = (List<cArrayList>)Session["lParamsop"];
        //              vShowReportWithParameter(ref crviewer, "rp_stockopname.rpt", lParamsop);
        //              break;
        //          case "retHO":
        //              List<cArrayList> lParamretHO = (List<cArrayList>)Session["lParamretHO"];
        //              vShowReportWithParameter(ref crviewer, "rp_returHO.rpt", lParamretHO);
        //              break;
        //          case "recRetHO":
        //              List<cArrayList> lParamrecRetHO = (List<cArrayList>)Session["lParamrecRetHO"];
        //              vShowReportWithParameter(ref crviewer, "rp_recReturHO.rpt", lParamrecRetHO);
        //              break;
        //          case "inv":
        //              lformula.Add("{tdosales_invoice.inv_no} = '" + Request.QueryString["no"] + "'");
        //              vShowReport(ref crviewer, "rp_invoicesales.rpt", lformula);
        //              break;
        //          case "soa":
        //              vShowReport(ref crviewer, "rp_soa.rpt");
        //              break;
        //          case "py":
        //              lformula.Add("{tmst_payment.payment_no} = '" + Request.QueryString["noy"] + "'");
        //              vShowReport(ref crviewer, "rp_paymentreceipt.rpt", lformula);
        //              break;
        //          case "riv":
        //              vShowReport(ref crviewer, "rp_rekapinvoice.rpt");
        //              break;
        //      }

        //}
        //else
        //{
        //    ConnectionInfo cninfo =new ConnectionInfo();
        //    cninfo.ServerName = bll.sGetControlParameter("cr_server");  // ".\\sqlexpress";
        //    cninfo.DatabaseName = bll.sGetControlParameter("cr_db");// "MUS";
        //    cninfo.UserID = bll.sGetControlParameter("cr_uid");// "sa";
        //    cninfo.Password = bll.sGetControlParameter("cr_pwd"); // "preketek";

        //    //  cninfo.IntegratedSecurity = false;
        //    ReportDocument doc = (ReportDocument) Session["docreport"];
        //    Tables crTables = doc.Database.Tables;

        //    foreach (Table crTable in crTables)
        //    {
        //        TableLogOnInfo crtablelogoninfo = crTable.LogOnInfo;
        //        crtablelogoninfo.ConnectionInfo = cninfo;
        //        crTable.ApplyLogOnInfo(crtablelogoninfo);

        //    }

        //  //  crviewer.DataBind();

        //    crviewer.ReportSource = doc;
        //    crviewer.DataBind();
        //   // crviewer.ShowNextPage();
        //    //crviewer.RefreshReport();
        //    Session["docreport"] = doc;
        //}
    }
    protected void crviewer_Unload(object sender, EventArgs e)
    {
        crviewer.RefreshReport();
        doc.Close();
        doc.Dispose();
        GC.Collect();
    }
}