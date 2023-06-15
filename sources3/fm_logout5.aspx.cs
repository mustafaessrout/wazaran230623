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
using System.Globalization;
using System.Net.NetworkInformation;


public partial class fm_logout5 : System.Web.UI.Page
{
    cbll bll = new cbll();
    cdal cdl = new cdal();
    Utitlity ut = new Utitlity();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cookies.Clear();
        Request.Cookies.Clear();

        Response.Redirect("fm_loginol.aspx",false);
        //if (!IsPostBack)
        //{
        //    try
        //    {
        //        var t1 = System.Environment.MachineName;
        //        var t2 = HttpContext.Current.Server.MachineName;
        //        var t3 = Dns.GetHostName();
        //        var t4 = Environment.MachineName;
        //        var t5 = System.Windows.Forms.SystemInformation.ComputerName;
        //        var t6 = System.Environment.GetEnvironmentVariable(t3);
        //        var t7 = System.Net.Dns.GetHostName();

        //        //string IP = Request.UserHostName;
        //        //IPAddress myIP = IPAddress.Parse(IP);
        //        //IPHostEntry GetIPHost = Dns.GetHostEntry(myIP);
        //        //List<string> compName = GetIPHost.HostName.ToString().Split('.').ToList();
        //        var cn = "";//compName.First();

        //        var machineName = System.Net.Dns.GetHostName();
        //        var domainName = IPGlobalProperties.GetIPGlobalProperties().DomainName;
        //        var domainUserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        //        var userDisplayName = cn;//GetMacAddress();//System.DirectoryServices.AccountManagement.UserPrincipal.Current.DisplayName;
        //        var macID = (
        //                                  from nic in NetworkInterface.GetAllNetworkInterfaces()
        //                                  where nic.OperationalStatus == OperationalStatus.Up
        //                                  select nic.GetPhysicalAddress().ToString()
        //                              ).FirstOrDefault();

        //        List<cArrayList> arr = new List<cArrayList>();

        //        arr.Add(new cArrayList("@Salespoint_cd", Convert.ToString(HttpContext.Current.Request.Cookies["sp"].Value)));
        //        arr.Add(new cArrayList("@MacID", Convert.ToString(macID)));
        //        arr.Add(new cArrayList("@UserID", Convert.ToString(HttpContext.Current.Request.Cookies["usr_id"].Value)));
        //        //arr.Add(new cArrayList("@TrackDate", Convert.ToString(HttpContext.Current.Request.Cookies["sp"])));
        //        arr.Add(new cArrayList("@MachineName", Convert.ToString(machineName)));
        //        arr.Add(new cArrayList("@DomainName", Convert.ToString(domainName)));
        //        arr.Add(new cArrayList("@DomainUserName", Convert.ToString(domainUserName)));
        //        arr.Add(new cArrayList("@UserDisplayName", Convert.ToString(cn)));
        //        arr.Add(new cArrayList("@FormName", Convert.ToString("fm_logout5")));
        //        arr.Add(new cArrayList("@EventName", Convert.ToString("btn_logout")));
        //        arr.Add(new cArrayList("@CreatedBy", Convert.ToString(HttpContext.Current.Request.Cookies["usr_id"].Value)));

        //        bll.vInsertLogUser_Track(arr);

        //        //Response.Cookies.Clear();
        //        //Session.RemoveAll();
        //        //Session.Abandon();
        //        Response.Redirect("fm_loginol.aspx");
        //        //HttpCookie myCookie = new HttpCookie("usr_id");
        //        //myCookie.Expires = DateTime.Now.AddDays(-1d);
        //        //Response.Cookies.Add(myCookie);
        //        //Response.Redirect("fm_loginol.aspx");

        //    }
        //    catch (Exception ex)
        //    {
        //        Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
        //        bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_logout5");
        //        Response.Redirect("fm_ErrorPage.aspx");
        //    }
        //}
    }

    private string GetMacAddress()
    {
        const int MIN_MAC_ADDR_LENGTH = 12;
        string macAddress = string.Empty;
        long maxSpeed = -1;

        foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
        {
            //log.Debug(
            //    "Found MAC Address: " + nic.GetPhysicalAddress() +
            //    " Type: " + nic.NetworkInterfaceType);

            string tempMac = nic.GetPhysicalAddress().ToString();
            if (nic.Speed > maxSpeed &&
                !string.IsNullOrEmpty(tempMac) &&
                tempMac.Length >= MIN_MAC_ADDR_LENGTH)
            {
                //log.Debug("New Max Speed = " + nic.Speed + ", MAC: " + tempMac);
                maxSpeed = nic.Speed;
                macAddress = tempMac;
            }
        }

        return macAddress;
    }
}