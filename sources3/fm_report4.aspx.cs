using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;

public partial class fm_report4 : System.Web.UI.Page
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
        foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
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
        foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
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

        foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
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
        foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
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
        foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
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
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Page_Init(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        List<string> lformula = new List<string>();
        List<cArrayList> lparam = new List<cArrayList>();
        string sSrc = Request.QueryString["src"];
        string sImage = bll.sGetControlParameter("image_path");
        switch (sSrc)
        {
            case "bankdeposit":
                string sEmpCode = Request.QueryString["emp"];
                string sStartDate = Request.QueryString["start_dt"];
                string sEndDate = Request.QueryString["end_dt"];
                //lparam.Add(new cArrayList("@start_dt", Convert.ToDateTime(sStartDate)));
                //lparam.Add(new cArrayList("@end_dt", Convert.ToDateTime(sEndDate)));
                vShowReportWithParameter(ref crv, "rp_bankdeposit.rpt", lparam);
                break;
            case "it":
                string sSalespoint = Request.QueryString["sp"];
                string sTransferno = Request.QueryString["trf"];
                string sPrintBy = "SYS";
                lparam.Add(new cArrayList("@SalesPointCD",sSalespoint));
                lparam.Add(new cArrayList("@trf_no", sTransferno));
                lparam.Add(new cArrayList("@printby", sPrintBy));
                vShowReportWithParameter(ref crv, "rptInternalTransfer2.rpt", lparam);
                break;
        }
        }
}