using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;

public partial class fm_closingmonthly : System.Web.UI.Page
{
    cbll bll = new cbll();
    creport rep = new creport();
    Utitlity ut = new Utitlity();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            try
            {
                lbmonth.Text = bll.sGetControlParameterSalespoint("period", Request.Cookies["sp"].Value.ToString());
                string sDate = Request.Cookies["waz_dt"].Value.ToString();
                DateTime dt = DateTime.ParseExact(sDate, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                if (Convert.ToDouble(lbmonth.Text.Substring(4, 2)) == dt.Month)
                {
                    btprocess.CssClass = "divhid";
                    btprocess.Visible = false;
                }
                else
                {
                    btprocess.CssClass = "button2 add";
                    btprocess.Visible = true;
                }
                btpostpone.Visible = false;

            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_closingmonthly");
                Response.Redirect("fm_ErrorPage.aspx");
            }
           
        }
    }
    protected void btprocess_Click(object sender, EventArgs e)
    {
        try
        {

            System.Data.SqlClient.SqlDataReader rs = null; string sTemp = string.Empty;
            List<cArrayList> arr = new List<cArrayList>();
            //Aging Report every month to the Accounting        
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report3.aspx?src=jaret');", true);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report3.aspx?src=jaretdpo');", true);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report3.aspx?src=mcl');", true);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report3.aspx?src=notdue');", true);
            List<string> lformula = new List<string>();
            string sMsg = string.Empty;
            int ncount = 1;
            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBatchPrepropcessMonthly(ref rs, arr);
            while (rs.Read())
            {
                if (ncount == 1)
                {
                    sMsg += rs["inv_no"].ToString() + ",";
                }
                else
                { sMsg += rs["inv_no"].ToString() + ","; }
                ncount++;
            }
            rs.Close();
            if (sMsg != string.Empty)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('There invoice not yet received back from customer','" + sMsg + "','warning');", true);
                return;

            }

            string sTarger = bll.vLookUp("select dbo.fn_checksalestarget('" + Request.Cookies["sp"].Value.ToString() + "')");
            if (sTarger != "ok")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('" + sTarger + "','Sales Target','warning');", true);
                return;
            }
            //closing stock jaret monthly
            string sMsgjaret = string.Empty;
            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vclosingstockjaretmonthly(ref rs, arr);
            while (rs.Read())
            {
                sMsgjaret += rs["whs_cd"].ToString() + '-' + rs["bin_cd"].ToString() + '-' + rs["item_cd"].ToString() + '-' + rs["diff"].ToString() + " ,";
            }
            rs.Close();
            if (sMsgjaret != string.Empty)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('There are different stock system and stock opname, please check Closing Stock Jaret Monthly Report','" + sMsgjaret + "','warning');", true);
                return;

            }

            string sInvMonthly = bll.vLookUp("select dbo.fn_checkinvoicenotbackmonthly('" + Request.Cookies["sp"].Value.ToString() + "')");
            if (sInvMonthly != "ok")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Invoice on going delivery must received back ALL','" + sInvMonthly + "','warning');", true);
                //return;
                btprocess.Visible = false;
                btpostpone.Visible = true;
            }
            //------------
            // Task 1
            bll.vBatchMonthcustbalance2(arr);
            // Stock Movement
            arr.Clear();
            arr.Add(new cArrayList("@varCurrMonthCD", lbmonth.Text));
            arr.Add(new cArrayList("@varSalesPointCD", Request.Cookies["sp"].Value.ToString()));
            bll.vBatchMontStockEndPeriod(arr);

            //ut.BranchTargetPriorityMonthly(bll.sGetControlParameterSalespoint("period", Request.Cookies["sp"].Value.ToString()));
            //ut.BranchTargetPriorityVanMonthly(bll.sGetControlParameterSalespoint("period", Request.Cookies["sp"].Value.ToString()));

            //send Car Maintenance Report By Mamoun
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report3.aspx?src=carMaintenance');", true);


            arr.Clear();
            arr.Add(new cArrayList("@closing_typ", "M"));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@system_dt", null));
            bll.vInsertTclosinglog(arr);
            ////by yanto 02-09-2019
            //string lastDayOfMonth, syear, smonth;
            //syear = lbmonth.Text.Substring(0, 4);
            //smonth = lbmonth.Text.Substring(lbmonth.Text.Length - 2, 2);
            //Int32 slastday = DateTime.DaysInMonth(Convert.ToInt32(syear), Convert.ToInt32(smonth));
            //lastDayOfMonth = syear + "-" + smonth + "-" + slastday.ToString();
            //arr.Clear();
            //arr.Add(new cArrayList("@lastDay", lastDayOfMonth));
            //bll.vbatchrecalctacc_stock(arr);
            ////-------------------------------------------
            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBatchMonthAdvancedPeriod(arr);
            // Nofify to Business Analyst
            //List<string> lapp = bll.lGetApproval("ba", 1);
            //string sEmailTo = lapp[1];
            //string sText = "Dear Business Analyst Team,\n\r\n\r";
            //sText += "Monthly Closing for Branch " + bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + " Was Completed !\n\r\n\r";
            //sText += "Wazaran Admin";
            //bll.vSendMail(lapp[1], "Closing Monthly notification", sText);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Monthly Process Completed','Advanced Period : " + bll.sGetControlParameterSalespoint("period", Request.Cookies["sp"].Value.ToString()) + "','success');", true);
            lbmonth.Text = bll.sGetControlParameterSalespoint("period", Request.Cookies["sp"].Value.ToString());
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_closingmonthly");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }


    protected void btpostpone_Click(object sender, EventArgs e)
    {
        try
        {

            List<cArrayList> arr = new List<cArrayList>();

            if (hdconfirm.Value.ToString() == "true")
            {
                arr.Clear();
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vUpdatePostponeInvAll(arr);
                btprocess.Visible = true;
                btpostpone.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_closingmonthly");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
}
