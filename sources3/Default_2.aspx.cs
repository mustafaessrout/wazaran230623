using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using AjaxControlToolkit;
using System.Web.Services;
using System.Web.Script.Services;
using System.Runtime.Remoting.Channels;
using System.Text.RegularExpressions;
using GsmComm.PduConverter;
using GsmComm.PduConverter.SmartMessaging;
using GsmComm.GsmCommunication;
using GsmComm.Interfaces;
using GsmComm.Server;
using System.Globalization;
using System.Net.NetworkInformation;
using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page
{
    cbll bll = new cbll();
    decimal totsalesqty = 0, totsalesamount = 0, totcashout = 0, totstockin = 0, totstockout = 0;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            try
            {
                string sSalespoint, sUsrId;
                //SqlDataReader rs = null;
                List<cArrayList> arr = new List<cArrayList>();

                if (Request.QueryString["sp"] != null && Request.QueryString["sp"] != "")
                {
                    sSalespoint = Request.QueryString["sp"];
                    if (sSalespoint != null || sSalespoint != "")
                    {

                        Response.Cookies["sp"].Value = sSalespoint;
                        Response.Cookies["spn"].Value = bll.sGetSalespointname(sSalespoint);
                        Request.Cookies["sp"].Value = sSalespoint;
                        Request.Cookies["spn"].Value = bll.sGetSalespointname(sSalespoint);
                        Response.Cookies["waz_dt"].Value = bll.sGetControlParameterSalespoint("wazaran_dt", sSalespoint);
                        Request.Cookies["waz_dt"].Value = bll.sGetControlParameterSalespoint("wazaran_dt", sSalespoint);
                    }
                    else
                    {
                        sSalespoint = Response.Cookies["sp"].Value;
                        sSalespoint = Request.Cookies["sp"].Value.ToString();
                        //sSalespoint = Convert.ToString(HttpContext.Current.Request.Cookies["sp"].Value);
                    }


                    arr.Clear();
                    string[] arrPage; string previousPageURL;



                    if (sSalespoint == "" || sSalespoint == null)
                    {
                        //Session.Clear();
                        //Session.Abandon();
                        Response.Redirect("fm_loginol.aspx");
                    }
                    else
                    {
                        int bolSalespoint = int.Parse(bll.vLookUp("select count(*) from tmst_salespoint where salespointcd='" + sSalespoint + "'"));
                        if (bolSalespoint <= 0)
                        {
                            Response.Redirect("fm_loginol.aspx");
                        }
                        //if (sSalespoint == "0")
                        //{
                        if (!String.IsNullOrEmpty(Request.UrlReferrer.ToString()))
                        {
                            previousPageURL = Request.UrlReferrer.ToString();
                            arrPage = previousPageURL.Replace("http://", "").Split('/');

                            if (arrPage[1] == "switcherol.aspx")
                            {
                                if (bolSalespoint <= 0)
                                {
                                    Response.Redirect("fm_loginol.aspx");
                                }
                                else
                                {

                                    var cn = "";//compName.First();
                                    var machineName = System.Net.Dns.GetHostName();
                                    var domainName = IPGlobalProperties.GetIPGlobalProperties().DomainName;
                                    var domainUserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                                    var userDisplayName = cn;//GetMacAddress();//System.DirectoryServices.AccountManagement.UserPrincipal.Current.DisplayName;
                                    var macID = (
                                                    from nic in NetworkInterface.GetAllNetworkInterfaces()
                                                    where nic.OperationalStatus == OperationalStatus.Up
                                                    select nic.GetPhysicalAddress().ToString()
                                                ).FirstOrDefault();

                                    arr.Clear();

                                    arr.Add(new cArrayList("@Salespoint_cd", Convert.ToString(HttpContext.Current.Request.Cookies["sp"].Value)));
                                    arr.Add(new cArrayList("@MacID", Convert.ToString(macID)));
                                    arr.Add(new cArrayList("@UserID", Convert.ToString(HttpContext.Current.Request.Cookies["usr_id"].Value)));
                                    //arr.Add(new cArrayList("@TrackDate", Convert.ToString(HttpContext.Current.Request.Cookies["sp"])));
                                    arr.Add(new cArrayList("@MachineName", Convert.ToString(machineName)));
                                    arr.Add(new cArrayList("@DomainName", Convert.ToString(domainName)));
                                    arr.Add(new cArrayList("@DomainUserName", Convert.ToString(domainUserName)));
                                    arr.Add(new cArrayList("@UserDisplayName", Convert.ToString(userDisplayName)));
                                    arr.Add(new cArrayList("@FormName", Convert.ToString("fm_loginol")));
                                    arr.Add(new cArrayList("@EventName", Convert.ToString("btn_login")));
                                    arr.Add(new cArrayList("@CreatedBy", Convert.ToString(HttpContext.Current.Request.Cookies["usr_id"].Value)));

                                    bll.vInsertLogUser_Track(arr);
                                    arr.Clear();
                                }
                            }
                        }
                        //}                
                    }
                }

                // Dashboard Configuration 
                dtstart.Text = Request.Cookies["waz_dt"].Value.ToString();
                dtend.Text = Request.Cookies["waz_dt"].Value.ToString();
                arr.Clear();
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_getuser", "salespointcd", "salespoint_desc", arr);
                cbsalespoint.Items.Insert(0,new ListItem("<< ALL Branch >>", "-1"));
                if (Request.Cookies["sp"].Value.ToString() != "0") { cbsalespoint.SelectedValue = Request.Cookies["sp"].Value.ToString();  cbsalespoint.Enabled = false; }
                cbtype_SelectedIndexChanged(sender, e);

                arr.Clear();
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                bll.vBatchCleanWrkUser(arr);

            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : default");
                Response.Redirect("fm_loginol.aspx");
            }            
        }
    }

    [WebMethod]
    [ScriptMethod]
    public static Slide[] GetSlides()
    {
        List<Slide> slides = new List<Slide>();
        Slide sld = new Slide( "indomie1.png", "", "");
        slides.Add(sld);
        sld = new Slide("indomie2.png", "", "");
        slides.Add(sld);
        sld = new Slide("indomie3.png", "", "");
        slides.Add(sld);
        sld = new Slide("indomie.png", "", "");
        slides.Add(sld);
        //sld = new Slide("export.jpg", "", "");
        //slides.Add(sld);

        //string path = HttpContext.Current.Server.MapPath("~/image/");
        //if (path.EndsWith("\\"))
        //{
        //    path = path.Remove(path.Length - 1);
        //}
        //Uri pathUri = new Uri(path, UriKind.Absolute);
        //string[] files = Directory.GetFiles(path);
        //foreach (string file in files)
        //{
        //    Uri filePathUri = new Uri(file, UriKind.Absolute);
        //    slides.Add(new Slide
        //    {
        //        Name = Path.GetFileNameWithoutExtension(file),
        //        Description = Path.GetFileNameWithoutExtension(file) + ".jpg",
        //        ImagePath = pathUri.MakeRelativeUri(filePathUri).ToString()
        //    });
        //}
        return slides.ToArray();
    }
    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {

    }
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
      
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        GsmCommMain comm = new GsmCommMain("COM67", 9600, 150);
        if (!comm.IsConnected())
        { comm.Open(); }
        SmsSubmitPdu pdu;
        byte dcs = (byte)DataCodingScheme.GeneralCoding.Alpha7BitDefault;
        //   int times = 1;
        pdu = new SmsSubmitPdu("New Discount has been created, please see on HO referens - Wazaran Admin", "0559613110", dcs);
        comm.SendMessage(pdu);
        comm.Close();
        //  MessageBox.Show("Message sent");
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "xxx", "alert('message sent')", true);
    }

    protected void cbtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();        
        string sType = cbtype.SelectedValue.ToString();

        DateTime dtStart = DateTime.ParseExact(dtstart.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime dtEnd = DateTime.ParseExact(dtend.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        string dateStart = Convert.ToString(dtStart.Year) + "-" + Convert.ToString(dtStart.Month) + "-" + Convert.ToString(dtStart.Day);
        string dateEnd = Convert.ToString(dtEnd.Year) + "-" + Convert.ToString(dtEnd.Month) + "-" + Convert.ToString(dtEnd.Day);


        arr.Clear();
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@dtstart", dateStart));
        arr.Add(new cArrayList("@dtend", dateEnd));
        bll.vBindingGridToSp(ref grdsales, "sp_summarysales_byuser", arr);
        bll.vBindingGridToSp(ref grdstock, "sp_summarystock_byuser", arr);
        bll.vBindingGridToSp(ref grdcash, "sp_summarycashier_byuser", arr);

        if (sType == "s1")
        {
            
            vSalesSummary.Visible = true;
            vStockSummary.Visible = false;
            vCashierSummary.Visible = false;
            appSalesSummary.Visible = true;
            appSummaryStock.Visible = false;
            appSummaryCashier.Visible = false;

        }else if(sType == "s2")
        {
            vSalesSummary.Visible = false;
            vStockSummary.Visible = true;
            vCashierSummary.Visible = false;
            appSalesSummary.Visible = false;
            appSummaryStock.Visible = true;
            appSummaryCashier.Visible = false;
        }
        else if(sType == "s3")
        {
            vSalesSummary.Visible = false;
            vStockSummary.Visible = false;
            vCashierSummary.Visible = true;
            appSalesSummary.Visible = false;
            appSummaryStock.Visible = false;
            appSummaryCashier.Visible = true;
        }

        if (grdsales.Rows.Count == 0) { lbltotsalesqty.Text = "0.0 CTN"; lbtotsalesamt.Text = "0.0 EGP"; }
        if (grdstock.Rows.Count == 0) { lbtotstockin.Text = "0.0 CTN"; lbtotstockout.Text = "0.0 CTN"; }
        if (grdcash.Rows.Count == 0) { lbtotcashout.Text = "0.0 EGP"; }

        lbtotappsalesreturn.Text = bll.vLookUp("select dbo.fn_totalapppending('salesreturn','"+ Request.Cookies["usr_id"].Value.ToString() + "','"+cbsalespoint.SelectedValue.ToString()+"')");
        lbtotappsalesfullreturn.Text = bll.vLookUp("select dbo.fn_totalapppending('fullreturn','" + Request.Cookies["usr_id"].Value.ToString() + "','" + cbsalespoint.SelectedValue.ToString() + "')");
        lbtotappcusttransfer.Text = bll.vLookUp("select dbo.fn_totalapppending('custtransfer','" + Request.Cookies["usr_id"].Value.ToString() + "','" + cbsalespoint.SelectedValue.ToString() + "')");
        lbtotappinternaltransfer.Text = bll.vLookUp("select dbo.fn_totalapppending('internaltransfer','" + Request.Cookies["usr_id"].Value.ToString() + "','" + cbsalespoint.SelectedValue.ToString() + "')");
        lbtotappstockaddloss.Text = bll.vLookUp("select dbo.fn_totalapppending('stokaddloss','" + Request.Cookies["usr_id"].Value.ToString() + "','" + cbsalespoint.SelectedValue.ToString() + "')");
        lbtotappstockadj.Text = bll.vLookUp("select dbo.fn_totalapppending('stockadj','" + Request.Cookies["usr_id"].Value.ToString() + "','" + cbsalespoint.SelectedValue.ToString() + "')");
        lbtotappbankdeposit.Text = bll.vLookUp("select dbo.fn_totalapppending('bankdeposit','" + Request.Cookies["usr_id"].Value.ToString() + "','" + cbsalespoint.SelectedValue.ToString() + "')");
        lbtotappcashout.Text = bll.vLookUp("select dbo.fn_totalapppending('cashout','" + Request.Cookies["usr_id"].Value.ToString() + "','" + cbsalespoint.SelectedValue.ToString() + "')");
        lbtotappclosingcashier.Text = bll.vLookUp("select dbo.fn_totalapppending('closingcashier','" + Request.Cookies["usr_id"].Value.ToString() + "','" + cbsalespoint.SelectedValue.ToString() + "')");

    }

    protected void dtend_TextChanged(object sender, EventArgs e)
    {
        cbtype_SelectedIndexChanged(sender, e);
    }

    protected void dtstart_TextChanged(object sender, EventArgs e)
    {
        cbtype_SelectedIndexChanged(sender, e);
    }

    protected void cbsalespoint_SelectedIndexChanged(object sender, EventArgs e)
    {
        cbtype_SelectedIndexChanged(sender, e);
    }

    protected void grdsales_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdqtyorder = (HiddenField)e.Row.FindControl("hdqtyorder");
                HiddenField hdqtyfree = (HiddenField)e.Row.FindControl("hdqtyfree");
                HiddenField hdamount = (HiddenField)e.Row.FindControl("hdamount");
                
                totsalesqty = totsalesqty + decimal.Parse(hdqtyorder.Value) + decimal.Parse(hdqtyfree.Value);
                totsalesamount = totsalesamount + decimal.Parse(hdamount.Value);

            }

            lbltotsalesqty.Text = totsalesqty.ToString("#,##0.00") + " CTN";
            lbtotsalesamt.Text = totsalesamount.ToString("#,##0.00") + " EGP";

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : Default");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void grdstock_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdstockin = (HiddenField)e.Row.FindControl("hdstockin");
                HiddenField hdvanin = (HiddenField)e.Row.FindControl("hdvanin");
                HiddenField hdvanout = (HiddenField)e.Row.FindControl("hdvanout");

                totstockin = totstockin + decimal.Parse(hdstockin.Value) + decimal.Parse(hdvanout.Value);
                totstockout = totstockout + decimal.Parse(hdvanin.Value);

            }

            lbtotstockin.Text = totstockin.ToString("#,##0.00") + " CTN";
            lbtotstockout.Text = totstockout.ToString("#,##0.00") + " CTN";

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : Default");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void grdcash_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdcashout = (HiddenField)e.Row.FindControl("hdcashout");

                totcashout = totcashout + decimal.Parse(hdcashout.Value);

            }

            lbtotcashout.Text = totcashout.ToString("#,##0.00") + " EGP";

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : Default");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
}