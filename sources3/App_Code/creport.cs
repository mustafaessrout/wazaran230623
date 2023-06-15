using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;

/// <summary>
/// Summary description for creport
/// </summary>
public class creport
{
    cbll bll = new cbll();

    ReportDocument doc = null;
    public void vShowReportToEXCEL(string sReportName, List<cArrayList> lparam, string PdfName)
    {

        ConnectionInfo cninfo = new ConnectionInfo();
        ParameterFields pfs = new ParameterFields();
        ParameterField pf = null;
        ParameterDiscreteValue pfd = new ParameterDiscreteValue();
        doc = new ReportDocument();
        doc.Load(HttpContext.Current.Server.MapPath("/" + sReportName));
        //  crv.EnableParameterPrompt = false;
        // crv.ReportSource = doc;
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
        //  doc.RecordSelectionFormula = lFormula[0];
        for (int i = 0; i < lparam.Count; i++)
        {
            //pfd = new ParameterDiscreteValue();
            //pf = new ParameterField();
            //  cArrayList arr = lparam[i];
            //pf.Name = arr.paramid;
            //pfd.Value = arr.ParamValue;
            //pf.CurrentValues.Add(pfd);
            //doc.ParameterFields.Add(pf);
            // pfs.Add(pf);
            doc.SetParameterValue(lparam[i].paramid, lparam[i].ParamValue);
            // doc.ParameterFields[arr.paramid].CurrentValues.Add(arr.ParamValue);
        }
        //  doc.ParameterFields.Add(pfs);
        doc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.Excel, PdfName);
        doc.Close();

    }
    public void vShowReportToPDF(string sReportName, string PdfName)
    {

        ConnectionInfo cninfo = new ConnectionInfo();
        ParameterFields pfs = new ParameterFields();
        ParameterField pf = null;
        ParameterDiscreteValue pfd = new ParameterDiscreteValue();
        doc = new ReportDocument();
        doc.Load(HttpContext.Current.Server.MapPath("/" + sReportName));
        //  crv.EnableParameterPrompt = false;
        // crv.ReportSource = doc;
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
       // doc.RecordSelectionFormula = lFormula[0];
        doc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, PdfName);
        doc.Close();

    }
    public void vShowReportToPDF(string sReportName, List<string> lFormula, string PdfName)
    {

        ConnectionInfo cninfo = new ConnectionInfo();
        ParameterFields pfs = new ParameterFields();
        ParameterField pf = null;
        ParameterDiscreteValue pfd = new ParameterDiscreteValue();
        doc = new ReportDocument();
        doc.Load(HttpContext.Current.Server.MapPath("/" + sReportName));
        //  crv.EnableParameterPrompt = false;
        // crv.ReportSource = doc;
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
        doc.RecordSelectionFormula = lFormula[0];
        doc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, PdfName);
        doc.Close();
      
    }

    public void vShowReportToPDF(string sReportName, List<string> lFormula, List<cArrayList> lparam, string PdfName)
    {

        ConnectionInfo cninfo = new ConnectionInfo();
        ParameterFields pfs = new ParameterFields();
        ParameterField pf = null;
        ParameterDiscreteValue pfd = new ParameterDiscreteValue();
        doc = new ReportDocument();
        doc.Load(HttpContext.Current.Server.MapPath("/" + sReportName));
        //  crv.EnableParameterPrompt = false;
        // crv.ReportSource = doc;
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
        doc.RecordSelectionFormula = lFormula[0];
        for (int i = 0; i < lparam.Count; i++)
        {
           // pfd = new ParameterDiscreteValue();
           // pf = new ParameterField();
            cArrayList arr = lparam[i];
           // pf.Name = arr.paramid;
          //  pfd.Value = arr.ParamValue;
          //  pf.CurrentValues.Add(pfd);
          //  doc.ParameterFields[arr.paramid].CurrentValues.Add(arr.ParamValue);
            doc.SetParameterValue(lparam[i].paramid,lparam[i].ParamValue);
            //doc.ParameterFields.Add(pf);
            //  pfs.Add(pf);
        }
        
        doc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, PdfName);
        doc.Close();

    }

    public void vShowReportToPDF(string sReportName, List<cArrayList> lparam, string PdfName)
    {

        ConnectionInfo cninfo = new ConnectionInfo();
        ParameterFields pfs = new ParameterFields();
        ParameterField pf = null;
        ParameterDiscreteValue pfd = new ParameterDiscreteValue();
        doc = new ReportDocument();
        doc.Load(HttpContext.Current.Server.MapPath("/" + sReportName));
        //  crv.EnableParameterPrompt = false;
        // crv.ReportSource = doc;
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
      //  doc.RecordSelectionFormula = lFormula[0];
        for (int i = 0; i < lparam.Count; i++)
        {
            //pfd = new ParameterDiscreteValue();
            //pf = new ParameterField();
          //  cArrayList arr = lparam[i];
            //pf.Name = arr.paramid;
            //pfd.Value = arr.ParamValue;
            //pf.CurrentValues.Add(pfd);
            //doc.ParameterFields.Add(pf);
            // pfs.Add(pf);
            doc.SetParameterValue(lparam[i].paramid, lparam[i].ParamValue);
           // doc.ParameterFields[arr.paramid].CurrentValues.Add(arr.ParamValue);
        }
      //  doc.ParameterFields.Add(pfs);
        doc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, PdfName);
        doc.Close();

    }

    public void vShowReportToPDFWithSP(string sReportName, List<cArrayList> lparam, string PdfName)
    {
        CrystalReportViewer crv = new CrystalReportViewer();
        ConnectionInfo cninfo = new ConnectionInfo();
        ParameterFields pfs = new ParameterFields();
        ParameterField pf = null;
        ParameterDiscreteValue pfd = new ParameterDiscreteValue();
        doc = new ReportDocument();
        doc.Load(HttpContext.Current.Server.MapPath("/" + sReportName));
        //  crv.EnableParameterPrompt = false;
        // crv.ReportSource = doc;
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
        //  doc.RecordSelectionFormula = lFormula[0];
       // cArrayList arr ;
        for (int i = 0; i < lparam.Count; i++)
        {
         //   pfd = new ParameterDiscreteValue();
         //   pf = new ParameterField();
           //   arr = lparam[i];
        //    pf.Name = arr.paramid;
        //    pfd.Value = arr.ParamValue;
        //    pf.CurrentValues.Add(pfd);
            //doc.ParameterFields.Add(pf);
            //pfs.Add(pf);
            doc.SetParameterValue(lparam[i].paramid, lparam[i].ParamValue);
            //doc.SetParameterValue(arr.paramid, arr.ParamValue);
          //  doc.ParameterFields[arr.paramid].CurrentValues.Add(arr.ParamValue);
        }
      //  doc.ParameterFields.Add(pfs);
        //crv.ParameterFieldInfo = pfs;
        doc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, PdfName);
        doc.Close();
         
    }
}