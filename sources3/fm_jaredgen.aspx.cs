using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
public partial class fm_jaredgen : System.Web.UI.Page
{
    cbll bll = new cbll();
    ReportDocument doc = new ReportDocument();
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
            case "jrt":
                string sysdtjrd =bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='filenmdtjrt'");
                DateTime dtjrd = DateTime.ParseExact(sysdtjrd, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                string jrtFileName = bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='salespoint'") + "-" + dtjrd.Year + "-" + dtjrd.Month + "-" + dtjrd.Day + "-jared" + ".xls";
                List<cArrayList> arr2 = new List<cArrayList>();
                arr2.Add(new cArrayList("@SalesPointCD", bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='salespoint'")));
                arr2.Add(new cArrayList("@MonthCD", bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='periodjrt'")));
                arr2.Add(new cArrayList("@siteDest", null));
                arr2.Add(new cArrayList("@whs_cd", null));
                arr2.Add(new cArrayList("@dtFrom", bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='startdtjrt'")));
                arr2.Add(new cArrayList("@dtTo", bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='enddtjrt'")));
                arr2.Add(new cArrayList("@dtStart", bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='startdtjrt'")));
                arr2.Add(new cArrayList("@item_cdFr", "-1"));
                arr2.Add(new cArrayList("@item_cdTo", "-1"));
                arr2.Add(new cArrayList("@prod_cdFr", "-1"));
                arr2.Add(new cArrayList("@prod_cdTo", "-1"));
                arr2.Add(new cArrayList("@Bin_cd_fr", "-1"));
                vShowReportToExcell(ref crv, "rp_jared.rpt", sImage + jrtFileName, arr2);
                break;
        }
    }
        public void vShowReportToExcell(ref CrystalReportViewer crv, string sReportName, string XlsName, List<cArrayList> lparam)
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
    }