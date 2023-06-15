using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class fm_login3 : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           // return;
            Response.Redirect("fm_login4.aspx");
            SqlDataReader rs = null;
            List<cArrayList> arr = new List<cArrayList>();
            List<string> arr1 = new List<string>();
            List<string> arraspx = new List<string>();
            List<string> arrmenucaption = new List<string>();
            List<string> arrallmenu = new List<string>();
            List<string> arrallmenucaption = new List<string>();
            List<string> arrallaspx = new List<string>();
            arr.Add(new cArrayList("@usr_id", "2540"));
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
            //Start Remark if production  -----------------------------------
            Response.Cookies.Clear();
            Response.Cookies["usr_id"].Value = "2540";
            Response.Cookies["fullname"].Value = "TEST PURPOSED";
            Response.Cookies["sp"].Value = "101";
            Response.Cookies["spn"].Value = "Jeddah";
            Response.Cookies["lang"].Value = "EN";
            Response.Cookies["waz_dt"].Value = bll.sGetControlParameter("wazaran_dt");
            Response.Redirect("default.aspx");
        
        }
    }
    protected void btlogin_Click(object sender, EventArgs e)
    {
         //csendsms sms = new csendsms();
        Random random = new Random();
        string sMobileNo = "";
        int password = random.Next(1000, 9999);
        SqlDataReader rs = null;
        string sFullName = "";
        string sSalespoint = "";
        string sLastToken = "";
        string sEmpCode = ""; string sEmail = "";
        
        bool bCheck = false;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", txusername.Text));
        arr.Add(new cArrayList("@password", txpassword.Text));
        bll.vGetUserProfile(arr, ref rs);
        while (rs.Read())
        {
            bCheck = true;
            sFullName = rs["fullname"].ToString();
            sMobileNo = rs["mobile_no"].ToString();
            sSalespoint = rs["salespointcd"].ToString();
            sEmpCode = rs["emp_cd"].ToString();
            sLastToken = rs["last_token"].ToString();
            sEmail = rs["email"].ToString();
        } rs.Close();
        if (bCheck == false)
        {
            lbnotice.Text = "Wrong username or password !";
        }
        else
        {
            string sSms;
            Response.Cookies["usr_id"].Value = sEmpCode;
            Response.Cookies["sp"].Value = sSalespoint;
            Response.Cookies["spn"].Value = bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd='" + sSalespoint + "'");
            Response.Cookies["fullname"].Value = sFullName;
            if (Request.Cookies["lang"] == null)
            {
                Response.Cookies["lang"].Value = "EN";
            }
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            if (bll.nCheckToken(arr) == 0)
            {
                arr.Clear();
                arr.Add(new cArrayList("@last_token", password.ToString()));
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                bll.vUpdatetuserprofile(arr);
                sSms = password.ToString();
                cd.vSendSms("Wazaran System Token : " + sSms, sMobileNo);
                    //Sending email 
                    string sSubject = "Token SMS for salespoint code : " + sSalespoint ;
                    string sBody = "This is your token for login to Wazaran : " + sSms;
                    bll.vSendMail(sEmail, sSubject, sBody);
              
            }
            else
            {
                sSms = sLastToken;
                string sSubject = "Token SMS";
                string sBody = "This is your token for login to Wazaran : " + sSms;
                bll.vSendMail(sEmail, sSubject, sBody);
            }
          
            Session["smscode"] = sSms; //password.ToString();
            Response.Cookies["waz_dt"].Value = bll.sGetControlParameter("wazaran_dt");
         //   Response.Redirect("fm_loginsms.aspx");
          //SqlDataReader rs = null;
            //List<cArrayList> arr = new List<cArrayList>();
           // SqlDataReader rs = null;
           // List<cArrayList> arr = new List<cArrayList>();
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
            Response.Redirect("fm_loginsms2.aspx");
          //  Response.Redirect("default.aspx");
        }
    }
    }
