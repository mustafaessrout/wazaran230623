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
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            try
            {
                string sSalespoint, sUsrId;
                //SqlDataReader rs = null;
                List<cArrayList> arr = new List<cArrayList>();
                //List<string> arr1 = new List<string>();
                //List<string> arraspx = new List<string>();
                //List<string> arrmenucaption = new List<string>();

                if (Request.QueryString["sp"] != null && Request.QueryString["sp"] != "")
                {
                    sSalespoint = Request.QueryString["sp"];
                    if (sSalespoint != null && sSalespoint != "")
                    {

                        // Inital Menu by SP
                        //sUsrId = Request.Cookies["usr_id"].Value.ToString();
                        ////sUsrId = Response.Cookies["usr_id"].Value.ToString();
                        //arr.Clear();
                        //arr.Add(new cArrayList("@usr_id", sUsrId));
                        //arr.Add(new cArrayList("@salespoint_typ", bll.vLookUp("select salespoint_typ from tmst_salespoint where salespointcd='"+sSalespoint+"'")));
                        //bll.vCheckMenu(arr, ref rs);
                        //while (rs.Read())
                        //{
                        //    arr1.Add(rs["menu_cd"].ToString());
                        //    arraspx.Add(rs["aspfile"].ToString());
                        //    arrmenucaption.Add(rs["menu_nm"].ToString());
                        //}
                        //rs.Close();
                        //Session["themenu"] = arr1;
                        //Session["aspfile"] = arraspx;
                        //Session["menucaption"] = arrmenucaption;

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
                        sSalespoint = Request.Cookies["sp"].Value;
                        //sSalespoint = Convert.ToString(HttpContext.Current.Request.Cookies["sp"].Value);
                    }


                    //  popuplogin.Show();
                    //  Response.Cookies["usr_id"].Value = "2540";
                    //  Response.Cookies["sp"].Value ="6101";
                    //  if (Request.Cookies["lang"] == null)
                    //  {
                    //      Response.Cookies["lang"].Value = "EN";
                    //  }

                    arr.Clear();
                    string[] arrPage; string previousPageURL;



                    if (sSalespoint == "" && sSalespoint == null)
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
                                }
                            }

                        }
                        //}                
                    }
                }
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
}


