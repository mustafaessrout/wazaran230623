using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
public partial class fm_closingyearly : System.Web.UI.Page
{
    cbll bll = new cbll();
    creport rep = new creport();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbyear.Text = bll.sGetControlParameterSalespoint("wazaran_year", Request.Cookies["sp"].Value.ToString());
            string sDate = Request.Cookies["waz_dt"].Value.ToString();
            DateTime dt = DateTime.ParseExact(sDate, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            if (Convert.ToInt16(lbyear.Text) == dt.Year)
            {
                btprocess.CssClass = "divhid";
            }
            else { btprocess.CssClass = "button2 add"; }
        }
    }
    protected void btprocess_Click(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            //bll.vBatchBackupDB();
            chbackup.Checked = true;
            chbackup.CssClass = "ro";
            string sreturnPending = bll.vLookUp("select dbo.fn_checkreturnmustbedone('"+ Request.Cookies["sp"].Value.ToString() + "')").ToString();
            if (sreturnPending != "ok")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('There is pending process return','" + sreturnPending + "','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "vDisableShow();", true);
                return;
            }
            chreturnPending.Checked = true;
            chreturnPending.CssClass = "ro";
            string sTOPending = bll.vLookUp("select dbo.fn_checktakeordermustdone('"+ Request.Cookies["sp"].Value.ToString() + "')").ToString();
            if (sTOPending != "ok")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('There is pending process take order on Loading or Need Approval','" + sTOPending + "','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "vDisableShow();", true);
                return;
            }
            chTOPending.Checked = true;
            chTOPending.CssClass = "ro";
            
            string sCheckCustomerReceived = bll.vLookUp("select dbo.fn_checkcustomerreceived('"+ Request.Cookies["sp"].Value.ToString() + "')");
            if (sCheckCustomerReceived != "ok")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('There invoices has not yet received back from customer after 7 days printed','" + sCheckCustomerReceived + "','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "vDisableShow();", true);
                return;
            }
            chCheckCustomerReceived.Checked = true;
            chCheckCustomerReceived.CssClass = "ro";

            string sChstock = bll.vLookUp("select dbo.fn_checkstockminus('"+ Request.Cookies["sp"].Value.ToString() + "')");
            if (sChstock != "ok")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('There stock minus ','" + sChstock + "','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "vDisableShow();", true);
                return;
            }
            Chstock.Checked = true;
            Chstock.CssClass = "ro";
            string schcqduedate = bll.vLookUp("select dbo.fn_checkchequeduedate('"+ Request.Cookies["sp"].Value.ToString() + "')");
            if (schcqduedate != "ok")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('There is Cheque or Transfer Due Date !','" + schcqduedate + "','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "vDisableShow();", true);
                return;
            }
            //chcqduedate.Checked = true;
            //chcqduedate.CssClass = "ro";
            //string schclaimpending = bll.vLookUp("select dbo.fn_checkclaimpending()");
            //if (schclaimpending != "ok")
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('There is Claim Pending !','" + schclaimpending + "','warning');", true);
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "vDisableShow();", true);
            //    return;
            //}
            chclaimpending.Checked = true;
            chclaimpending.CssClass = "ro";
            string schnosusp = bll.vLookUp("select dbo.fn_checknosuspense('"+ Request.Cookies["sp"].Value.ToString() + "')");
            if (schnosusp != "ok")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('There is Suspense Payment !','" + schnosusp + "','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "vDisableShow();", true);
                return;
            }
            chnosusp.Checked = true;
            chnosusp.CssClass = "ro";

            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBatchwazaran_year2(arr);
            Response.Cookies["wazaran_year"].Value = null;
            Response.Cookies["wazaran_year"].Value = bll.sGetControlParameterSalespoint("wazaran_year", Request.Cookies["sp"].Value.ToString());
            string syear = bll.sGetControlParameterSalespoint("wazaran_year", Request.Cookies["sp"].Value.ToString());
            lbyear.Text = syear;
            btprocess.CssClass = "divhid";
            arr.Clear();
            arr.Add(new cArrayList("@varYearCD", syear));
            //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString())); 
            bll.vInsertMonthYearWiz(arr);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "vDisableShow();", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Yearly Closed Successfully','New Open Year " + syear + "','success')", true);
            
        }
        catch (Exception ex)
        {
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : Yearly Closing");
        }

    }
}

