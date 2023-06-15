using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class fm_login : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx"); //Remark THis to bypass login auth
    }

    protected void Page_Activated(object sender, EventArgs e)
    {
        Login1.Focus();
    }
    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        Random random = new Random();
        csendsms sms = new csendsms();
        bool bAuth=false;
        string sUserID = Login1.UserName;
        string sPassword = Login1.Password;
        string sMobileNo = "";
        SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", sUserID));
        arr.Add(new cArrayList("@password", sPassword));
        bll.vGetUserProfile(arr, ref rs);
        while (rs.Read())
        {
            bAuth = true;
            sMobileNo = rs["mobile_no"].ToString().Trim();
        } rs.Close();
      //  sMobileNo = "0503743024";
        if (bAuth == false)
        {
           // Response.Write("<strong>Wrong password or user name, pls try again !</strong>");
            Login1.FailureText = "Wrong username/password, pls try again !";
            return;
        }

        try
        {
            int password = random.Next(1000, 9999);
            Response.Cookies["usr_id"].Value = "2540";
            Response.Cookies["sp"].Value = "6201";
            if (Request.Cookies["lang"] == null)
            {
                Response.Cookies["lang"].Value = "EN";
            }

            sms.vSendSms("Wazaran System Token : " + password.ToString(), sMobileNo);
            Session["smscode"] = password.ToString();
            Response.Redirect("fm_loginsms.aspx");
        }
        catch (Exception ex)
        {
            Login1.FailureText= ex.Message;
            sms = null;
        }
    }
}