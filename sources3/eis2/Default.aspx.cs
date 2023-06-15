using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class eis2_Default : System.Web.UI.Page
{
    private string server = "127.0.0.1";
    private Uri link_server;
    private string report;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"].ToString() == null)
            {
                report = "Dashboard";
            }
            else
            {
                report = Request.QueryString["id"].ToString();
            }


            link_server = new Uri("http://" + server + "/ReportServer");
            MyReportViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            MyReportViewer.ServerReport.ReportServerUrl = link_server;
            MyReportViewer.ServerReport.ReportPath = "/Wazaran/" + report.ToString();

            //MyReportViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            //MyReportViewer.ServerReport.ReportServerUrl = new Uri("http://172.16.1.18/ReportServer"); // Report Server URL
            //MyReportViewer.ServerReport.ReportPath = "/Wazaran/Dashboard"; // Report Name

            if (report == "Dashboard")
            {
                // Configuration Report 
                MyReportViewer.AsyncRendering = true;
                MyReportViewer.SizeToReportContent = true;
                MyReportViewer.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;
                MyReportViewer.ShowParameterPrompts = false;
                MyReportViewer.ShowPrintButton = false;
                MyReportViewer.ShowToolBar = false;
                MyReportViewer.ShowFindControls = false;
                MyReportViewer.ShowPageNavigationControls = false;
                // Configuration Report
            }
            else
            {
                // Configuration Report 
                MyReportViewer.AsyncRendering = true;
                MyReportViewer.SizeToReportContent = true;
                MyReportViewer.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;
                MyReportViewer.ShowParameterPrompts = true;
                MyReportViewer.ShowPrintButton = false;
                MyReportViewer.ShowToolBar = false;
                MyReportViewer.ShowFindControls = false;
                MyReportViewer.ShowPageNavigationControls = false;
                // Configuration Report
            }
            
        }
    }
}