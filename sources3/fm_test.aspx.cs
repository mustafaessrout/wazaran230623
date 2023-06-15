using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // 1
        //System.Net.IPHostEntry host = new System.Net.IPHostEntry();
        //host = System.Net.Dns.GetHostEntry(Request.ServerVariables["REMOTE_HOST"]);
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('" + host.HostName + "',HostName','warning');", true);
        //Response.Write(host.HostName);

        //2
        //string input = "your IP address goes here";

        //IPAddress address;
        //if (IPAddress.TryParse(input, out address))
        //{
        //    switch (address.AddressFamily)
        //    {
        //        case System.Net.Sockets.AddressFamily.InterNetwork:
        //            //return Ip4Address;
        //            // we have IPv4
        //            break;
        //        case System.Net.Sockets.AddressFamily.InterNetworkV6:
        //            // we have IPv6
        //            break;
        //        default:
        //            // umm... yeah... I'm going to need to take your red packet and...
        //            break;
        //    }
        //}

        //3
        //System.Web.HttpContext context = System.Web.HttpContext.Current;
        //string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        //if (!string.IsNullOrEmpty(ipAddress))
        //{
        //    string[] addresses = ipAddress.Split(',');
        //    if (addresses.Length != 0)
        //    {
        //        //return addresses[0]; 
        //        var t1 = addresses[0]; 
        //    }
        //}

        ////return context.Request.ServerVariables["REMOTE_ADDR"];
        //var t2 = context.Request.ServerVariables["REMOTE_ADDR"];
        
        //4
        IPAddress[] ipv4Addresses = Array.FindAll(
        Dns.GetHostEntry(string.Empty).AddressList,
        a => a.AddressFamily == AddressFamily.InterNetwork);

        var t1 = ipv4Addresses.FirstOrDefault().Address.ToString();
        var t2 = ipv4Addresses.FirstOrDefault().AddressFamily.ToString();
        var t3 = ipv4Addresses.FirstOrDefault().IsIPv4MappedToIPv6.ToString();
        var t4 = ipv4Addresses.FirstOrDefault().IsIPv6LinkLocal.ToString();
        var t5 = ipv4Addresses.FirstOrDefault().IsIPv6Multicast.ToString();
        var t6 = ipv4Addresses.FirstOrDefault().IsIPv6SiteLocal.ToString();
        var t7 = ipv4Addresses.FirstOrDefault().IsIPv6Teredo.ToString();
        var t8 = ipv4Addresses.FirstOrDefault().ScopeId.ToString();

        string str = "1 " + t1 + " , 2 " + t2 + ", 3 " + t3 + ", 4 " + t4 + ", 5 " + t5 + ", 6 " + t6 + ", 7 " + t7 + ", 8 " + t8;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('" + str + "','Wrong','warning');", true);
        lbltemp.Text = Convert.ToString(t1 + "," + t2 + "," + t3 + "," + t4 + "," + t5 + "," + t6 + "," + t7 + "," + t8);
        
    }
}