using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using System.Net;

public partial class fm_login5 : System.Web.UI.Page
{
    cbll bll = new cbll();


    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cookies["startapp"].Value = "";
        //directlogin(sender, e);
        lbnotice.Text = "";
    }


    protected void btlogin_Click(object sender, EventArgs e)
    {
        try
        {

            SqlDataReader rs = null;
            string sFullName = "";
            string sMobileNo = "";
            string sSalespoint = "";
            string sEmpCode = ""; string sEmail = "";
            string sPwdExpDt = "";
            DateTime dPwdExpDt;
            DateTime dwaz_dt;
            int cInfo = -1;
            string pass = "", usr = "";

            Response.Cookies["usersql"].Value = txusername.Text; //by yanto 19-11-2018

            bool bCheck = false;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", txusername.Text));
            arr.Add(new cArrayList("@password", txpassword.Text));
            bll.vGetUserProfile(arr, ref rs);
            //usr = 0 ok ,usr = -1 not ok, 
            //pass = 1 ok from passwd, pass = 2 ok from pwdbinary, pass = -1 not ok, pass = 0 usr not ok
            if (txusername.Text != "" && txpassword.Text != "")
            {
                while (rs.Read())
                {
                    bCheck = true;
                    //usr = rs["usr"].ToString();
                    //pass = rs["pass"].ToString();
                    usr = "0";pass = "2";
                    if (usr == "0" && pass != "-1")
                    {
                        sFullName = rs["fullname"].ToString();
                        sMobileNo = rs["mobile_no"].ToString();
                        sSalespoint = rs["salespointcd"].ToString();
                        sEmpCode = rs["emp_cd"].ToString();
                        sEmail = rs["email"].ToString();
                        sPwdExpDt = rs["pwd_exp_dt"].ToString();
                    }

                } rs.Close();
            }

            if (bCheck == false)
            {
                lbnotice.Text = "Your User id or Password or both are not Entered, Please try again !";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                return;
            }
            else if (usr == "-1")
            {
                lbnotice.Text = "Your User id is invalid, Please try again !";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                return;
            }
            else if (pass == "-1")
            {
                lbnotice.Text = "Your Password is invalid, Please try again !";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                return;
            }


            else
            {
                Response.Cookies["usr_id"].Value = sEmpCode;
                Response.Cookies["sp"].Value = sSalespoint;
                Response.Cookies["spn"].Value = bll.sGetSalespointname(sSalespoint);
                Response.Cookies["fullname"].Value = sFullName;
                if (Request.Cookies["lang"] == null)
                {
                    Response.Cookies["lang"].Value = "EN";
                }
                Response.Cookies["startapp"].Value = bll.sGetControlParameter("startup");
                Response.Cookies["waz_dt"].Value = bll.sGetControlParameter("wazaran_dt");
                string waz_dt = bll.vLookUp("select dbo.fn_getLatestDate()");
                dwaz_dt = System.DateTime.ParseExact(waz_dt.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                dPwdExpDt = Convert.ToDateTime(sPwdExpDt.ToString());
                double sub_dt = dwaz_dt.Subtract(dPwdExpDt).Days;

                if (sub_dt == 0)
                {
                    //lbnotice.Text = "Your Password is 8 days age, Please change your Password Soon!";
                    lbnotice.Text = "Your Password is expired today, Please change your Password!";
                    btlogin.Visible = false;
                    btchangepswd.Visible = true;
                    btcontinue.Visible = true;
                    lbforgot.Visible = false;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                }
                else if (sub_dt > 0)
                {
                    //lbnotice.Text = "Your Password is more than 8 days age, Please change your Password immediately!";
                    lbnotice.Text = "Your Password is expired today, Please change your Password!";
                    btlogin.Visible = false;
                    btchangepswd.Visible = true;
                    lbforgot.Visible = false;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);

                }
                else
                {
                    arr.Clear();
                    arr.Add(new cArrayList("@emp_cd", Request.Cookies["usr_id"].Value.ToString()));
                    bll.vGetuserInfo(arr, ref rs);
                    while (rs.Read())
                    {
                        cInfo = Convert.ToInt32(rs["cinfo"].ToString());
                    } rs.Close();
                    arr.Clear();

                    if (cInfo == 0)
                    {
                        lbnotice.Text = "Your Personal Information is not Available, Please Add your Personal Information Immediately!";
                        btlogin.Visible = false;
                        btchangepswd.Visible = false;
                        lbforgot.Visible = false;
                        btInfo.Visible = true;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                    }
                    else if (cInfo > 0)
                    {
                        List<string> arr1 = new List<string>();
                        List<string> arraspx = new List<string>();
                        List<string> arrmenucaption = new List<string>();
                        List<string> arrallmenu = new List<string>();
                        List<string> arrallmenucaption = new List<string>();
                        List<string> arrallaspx = new List<string>();
                        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                        bll.vCheckMenu(arr, ref rs);
                        while (rs.Read())
                        {
                            arr1.Add(rs["menu_cd"].ToString());
                            arraspx.Add(rs["aspfile"].ToString());
                            arrmenucaption.Add(rs["menu_nm"].ToString());
                        }
                        rs.Close();
                        bll.vGetMstMenu(ref rs);
                        while (rs.Read())
                        {
                            arrallmenu.Add(rs["menu_cd"].ToString());
                            arrallmenucaption.Add(rs["menu_nm"].ToString());
                            arrallaspx.Add(rs["aspfile"].ToString());
                        } rs.Close();
                        Session["themenu"] = arr1;
                        Session["aspfile"] = arraspx;
                        Session["menucaption"] = arrmenucaption;
                        Session["allmenu"] = arrallmenu;
                        Session["allaspfile"] = arrallaspx;
                        Session["allmenucaption"] = arrallmenucaption;

                        // Log Login Details.
                        //HttpContext.Current.User.Identity.Name;

                        //string IP = Request.UserHostName;
                        //IPAddress myIP = IPAddress.Parse(IP);
                        //IPHostEntry GetIPHost = Dns.GetHostEntry(myIP);
                        //var tt = Dns.GetHostAddresses(IP);
                        //List<string> compName = GetIPHost.HostName.ToString().Split('.').ToList();
                        var cn = "";//compName.First();


                        //var t1 = System.Environment.MachineName;
                        //var t2 = HttpContext.Current.Server.MachineName;
                        //var t3 = Dns.GetHostName();
                        //var t4 = Environment.MachineName;
                        //var t5 = System.Windows.Forms.SystemInformation.ComputerName;
                        //var t6 = System.Environment.GetEnvironmentVariable(t3);
                        //var t7 = System.Net.Dns.GetHostName();

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
                        arr.Add(new cArrayList("@FormName", Convert.ToString("fm_login5")));
                        arr.Add(new cArrayList("@EventName", Convert.ToString("btn_login")));
                        arr.Add(new cArrayList("@CreatedBy", Convert.ToString(HttpContext.Current.Request.Cookies["usr_id"].Value)));

                        bll.vInsertLogUser_Track(arr);


                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                        //Response.Redirect("default.aspx");
                        Response.Redirect("default.aspx", false);
                        Context.ApplicationInstance.CompleteRequest();
                    }
                    else
                    {
                        lbnotice.Text = "There is a Problem, Please Contact System Admin Immediately!";
                        btlogin.Visible = false;
                        btchangepswd.Visible = false;
                        lbforgot.Visible = false;
                        btInfo.Visible = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            if (ex.Message.Contains("Login failed for user"))
            {
                lbnotice.Text = "Your User id is invalid or is not available, Please Enter Correctly or Contact System Admin Immediately!";
            }
            else
            {
                lbnotice.Text = ex.Message;
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@err_source", "LOGIN"));
                arr.Add(new cArrayList("@err_description", ex.Message.ToString()));
                bll.vInsertErrorLog(arr);
            }
        }
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
    protected void btchangepswd_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_changePassword.aspx");
    }
    protected void btcontinue_Click(object sender, EventArgs e)
    {
        SqlDataReader rs = null;
        string sFullName = "";
        string sMobileNo = "";
        string sSalespoint = "";
        string sEmpCode = ""; string sEmail = "";
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", txusername.Text));
        arr.Add(new cArrayList("@password", txpassword.Text));
        bll.vGetUserProfile(arr, ref rs);
        while (rs.Read())
        {
            sFullName = rs["fullname"].ToString();
            sMobileNo = rs["mobile_no"].ToString();
            sSalespoint = rs["salespointcd"].ToString();
            sEmpCode = rs["emp_cd"].ToString();
            sEmail = rs["email"].ToString();

        } rs.Close();
        Response.Cookies["usr_id"].Value = sEmpCode;
        Response.Cookies["sp"].Value = sSalespoint;
        Response.Cookies["spn"].Value = bll.sGetSalespointname(sSalespoint);
        Response.Cookies["fullname"].Value = sFullName;
        if (Request.Cookies["lang"] == null)
        {
            Response.Cookies["lang"].Value = "EN";
        }
        Response.Cookies["startapp"].Value = bll.sGetControlParameter("startup");
        Response.Cookies["waz_dt"].Value = bll.sGetControlParameter("wazaran_dt");

        arr.Clear();
        List<string> arr1 = new List<string>();
        List<string> arraspx = new List<string>();
        List<string> arrmenucaption = new List<string>();
        List<string> arrallmenu = new List<string>();
        List<string> arrallmenucaption = new List<string>();
        List<string> arrallaspx = new List<string>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vCheckMenu(arr, ref rs);
        while (rs.Read())
        {
            arr1.Add(rs["menu_cd"].ToString());
            arraspx.Add(rs["aspfile"].ToString());
            arrmenucaption.Add(rs["menu_nm"].ToString());
        }
        rs.Close();
        bll.vGetMstMenu(ref rs);
        while (rs.Read())
        {
            arrallmenu.Add(rs["menu_cd"].ToString());
            arrallmenucaption.Add(rs["menu_nm"].ToString());
            arrallaspx.Add(rs["aspfile"].ToString());
        } rs.Close();
        Session["themenu"] = arr1;
        Session["aspfile"] = arraspx;
        Session["menucaption"] = arrmenucaption;
        Session["allmenu"] = arrallmenu;
        Session["allaspfile"] = arrallaspx;
        Session["allmenucaption"] = arrallmenucaption;
        Response.Redirect("default.aspx");

    }
    protected void lbforgot_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_forgotPassword.aspx");
    }

    protected void btInfo_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_addPersonalInfo.aspx");

    }

    protected void directlogin(object sender, EventArgs e)
    {
        SqlDataReader rs = null;
        string sFullName = "";
        string sMobileNo = "";
        string sSalespoint = "";
        string sEmpCode = ""; string sEmail = "";
        Response.Cookies["usersql"].Value = "mamoun"; //by yanto 19-11-2018
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", "mamoun"));
        //arr.Add(new cArrayList("@password", "Sbtc1234"));
        arr.Add(new cArrayList("@password", "Sbtc@12"));
        bll.vGetUserProfile(arr, ref rs);
        while (rs.Read())
        {
            sFullName = rs["fullname"].ToString();
            sMobileNo = rs["mobile_no"].ToString();
            sSalespoint = rs["salespointcd"].ToString();
            sEmpCode = rs["emp_cd"].ToString();
            sEmail = rs["email"].ToString();

        } rs.Close();
        Response.Cookies["usr_id"].Value = sEmpCode;
        Response.Cookies["sp"].Value = sSalespoint;
        Response.Cookies["spn"].Value = bll.sGetSalespointname(sSalespoint);
        Response.Cookies["fullname"].Value = sFullName;
        if (Request.Cookies["lang"] == null)
        {
            Response.Cookies["lang"].Value = "EN";
        }
        Response.Cookies["startapp"].Value = bll.sGetControlParameter("startup");
        Response.Cookies["waz_dt"].Value = bll.sGetControlParameter("wazaran_dt");

        arr.Clear();
        List<string> arr1 = new List<string>();
        List<string> arraspx = new List<string>();
        List<string> arrmenucaption = new List<string>();
        List<string> arrallmenu = new List<string>();
        List<string> arrallmenucaption = new List<string>();
        List<string> arrallaspx = new List<string>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vCheckMenu(arr, ref rs);
        while (rs.Read())
        {
            arr1.Add(rs["menu_cd"].ToString());
            arraspx.Add(rs["aspfile"].ToString());
            arrmenucaption.Add(rs["menu_nm"].ToString());
        }
        rs.Close();
        bll.vGetMstMenu(ref rs);
        while (rs.Read())
        {
            arrallmenu.Add(rs["menu_cd"].ToString());
            arrallmenucaption.Add(rs["menu_nm"].ToString());
            arrallaspx.Add(rs["aspfile"].ToString());
        } rs.Close();
        Session["themenu"] = arr1;
        Session["aspfile"] = arraspx;
        Session["menucaption"] = arrmenucaption;
        Session["allmenu"] = arrallmenu;
        Session["allaspfile"] = arrallaspx;
        Session["allmenucaption"] = arrallmenucaption;
        Response.Redirect("default.aspx");

    }
}