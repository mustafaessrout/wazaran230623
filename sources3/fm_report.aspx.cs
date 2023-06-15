using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_report : System.Web.UI.Page
{
    creport crep = new creport();
    protected void Page_Load(object sender, EventArgs e)
    {
      ////  if (!IsPostBack)
      ////  {
      //      string sSrc = Request.QueryString["src"].ToString();
      //      switch (sSrc) { 
      //          case "do":
      //              List<string> lformula =(List<string>)Session["lformula"];
      //              crep.vShowReport(ref crviewer, "rppo.rpt", lformula);
      //              break;
      //          case "po":
      //              List<string> lformulapo =(List<string>)Session["lformula"];
      //              crep.vShowReport(ref crviewer, "rppo.rpt", lformulapo);
      //              break;
      //          case "inv":
      //              List<string> lformulainv = (List<string>)Session["lformula"];
      //              crep.vShowReport(ref crviewer, "rp_invoicebranch.rpt", lformulainv);
      //              break;
      //          case "trip":
      //              List<string> lformulatrip = (List<string>)Session["lformula"];
      //              crep.vShowReport(ref crviewer, "rp_invoicemanifest.rpt", lformulatrip);
      //              break;
      //          case "so":
      //              List<string> lformulaSO = (List<string>)Session["lformulaSO"];
      //              crep.vShowReport(ref crviewer, "rptSO.rpt", lformulaSO);
      //              break;
      //          case "Invoice":
      //              List<string> lformulaInvoice = (List<string>)Session["lformulaInvoice"];
      //              crep.vShowReport(ref crviewer, "rptInvoice.rpt", lformulaInvoice);
      //              break;
      //          case "itemprice":
      //              crep.vShowReport(ref crviewer, "rp_itemprice.rpt");
      //              break;
      //          case "itemprice2":
      //              crep.vShowReport(ref crviewer, "rp_itemprice2.rpt");
      //              break;
      //          case "it":
      //              crep.vShowReport(ref crviewer, "rp_internaltransfer.rpt");
      //              break;
      //          case "sop":
      //              List<string> lformulastock = (List<string>)Session["lformula"];
      //              crep.vShowReport(ref crviewer, "rp_stockopname.rpt", lformulastock);
      //              break;
      //          case "doload":
      //              List<cArrayList> lParamdoload = (List<cArrayList>)Session["lparamdoload"];
      //              crep.vShowReportWithParameter(ref crviewer,"rpdoloading.rpt" ,lParamdoload);
      //              break;
      // //     }
          
      //  }
    }
}