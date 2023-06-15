using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
using System.Net;
using Newtonsoft.Json;
public partial class fm_reportx : System.Web.UI.Page
{
    cbll bll = new cbll();
    ReportDocument doc = new ReportDocument();
    void x<T>(ref List<T> _T , string _url)
    {
        using (WebClient ws = new WebClient())
        {
           
            var _data = ws.DownloadString(_url);
            _T  = JsonConvert.DeserializeObject<List<T>>(_data).ToList();
        }
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        string _who = bll.sGetControlParameter("api_headoffice");
        crv.ReportSource = null;
        string _src = Request.QueryString["src"];
        switch (_src)
        {
            case "stockindirect":
                arr.Add(new cArrayList("@stockin_no", Request.QueryString["s"]));
                List<rptstockindirect> _rptstockindirect = bll.rptstockindirect(arr);
                vShowReport<rptstockindirect>(ref crv, _rptstockindirect, "rp_stockindirect.rpt");
                break;
            case "stocksample":
                arr.Add(new cArrayList("@sample_cd", Request.QueryString["s"]));
                arr.Add(new cArrayList("@printby", bll.vLookUp("select emp_nm from tmst_employee where emp_cd='" + Request.Cookies["usr_id"].Value +"'")));
                List<rptstocksample> _rptstocksample = bll.rptstocksample(arr);
                vShowReport<rptstocksample>(ref crv, _rptstocksample, "rp_stocksample.rpt");
                break;
            case "stockall":
                List<cArrayList> lParamstock_all = (List<cArrayList>)Session["lParamstock"];
                //lParamstock_all.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value));
                List<rptStock_all> _rptstockall = bll.lRptStockAll(lParamstock_all);
                vShowReport<rptStock_all>(ref crv, _rptstockall, "rptStock_all.rpt");
                break;
        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void vShowReport<T>(ref CrystalReportViewer crv, List<T> lDataSource, string _report_name)
    {
       
        doc.Load(HttpContext.Current.Server.MapPath("/" + _report_name));
        doc.SetDataSource(lDataSource);
        crv.ReportSource = doc;

        crv.DataBind();
        crv.RefreshReport();

    }

    protected void crv_Unload(object sender, EventArgs e)
    {
        crv.RefreshReport();
        doc.Close();
        doc.Dispose();
        GC.Collect();
    }
}
