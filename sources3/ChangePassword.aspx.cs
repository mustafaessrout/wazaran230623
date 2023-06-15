using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using AjaxControlToolkit;
using System.Text.RegularExpressions;
public partial class ChangePassword : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    //private bool ChangeUserPassword(string oldPassword, string newPassword, string confirmNewPassword)
    //{
    //    //if (userId != null && userId.Length > 0)
    //    //    return true;
    //    //else return false;
    //    string pwd;
    //    System.Data.SqlClient.SqlDataReader rs = null;
    //    List<cArrayList> arr = new List<cArrayList>();
    //    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
    //    bll.vGettuser_profile_get2(ref rs, arr);
    //    while (rs.Read())
    //    {
    //        pwd = rs["passwd"].ToString();

    //    }
    //    rs.Close();

    //}

    protected void btsave_Click(object sender, EventArgs e)
    {
        string suser_id = Request.Cookies["usr_id"].Value.ToString();
        string email = "";
        string SalespointCD = "";
        SqlDataReader rs = null;
        string waz_dt = bll.vLookUp("select dbo.fn_getLatestDate()");
        DateTime dwaz_dt = System.DateTime.ParseExact(waz_dt.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        string oldPassword = CurrentPassword.Text;
        string newPassword = NewPassword.Text;
        string confirmNewPassword = ConfirmNewPassword.Text;
        string pwd = "";
        string pwdexpdate = "";
        int countpass = -1;

        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", suser_id));
        bll.vGetpassfromtuserprofile(ref rs, arr);

        while (rs.Read())
        {
            SalespointCD = rs["Salespointcd"].ToString();
            pwd = rs["passwdbinary"].ToString();
            pwdexpdate = rs["pwd_exp_dt"].ToString();
            email = rs["email"].ToString();
        }
        rs.Close();

        if (newPassword != confirmNewPassword)
        {
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "Confirm Password and New Password must be equal.";
            return;
        }
        else if (oldPassword != pwd)
        {
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "Current Password does not match with our database records.";
            return;
        }
        else //(newPassword==pwd)
        {
            List<cArrayList> arr1 = new List<cArrayList>();
            arr1.Add(new cArrayList("@usr_id", suser_id));
            arr1.Add(new cArrayList("@NewPassword", newPassword));
            arr1.Add(new cArrayList("@Year", bll.sGetControlParameter("wazaran_year")));
            bll.vCheckOldpassNewpass(arr1, ref rs);
            while (rs.Read())
            {
                countpass = Convert.ToInt32(rs["countpass"].ToString());
            }
            rs.Close();

            if (countpass != 0 || oldPassword == newPassword)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "The Enterd Password has already been used, Please Enter a New Password!";
                return;
            }
            else
            {
                int passchk = (int)CheckingPasswordStrength(newPassword);

                if (passchk >= 3)
                {

                    arr1.Clear();
                    arr1.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    arr1.Add(new cArrayList("@NewPassword", newPassword));
                    arr1.Add(new cArrayList("@OldPassword", oldPassword));
                    arr1.Add(new cArrayList("@SalespointCD", SalespointCD));
                    arr1.Add(new cArrayList("@period", bll.sGetControlParameter("period")));
                    arr1.Add(new cArrayList("@CreatedDate", dwaz_dt));
                    arr1.Add(new cArrayList("@Year", bll.sGetControlParameter("wazaran_year")));
                    bll.vInsertPassintotUserPasswordMap(arr1);


                    arr1.Clear();
                    arr1.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    arr1.Add(new cArrayList("@passwd", newPassword));
                    arr1.Add(new cArrayList("@waz_dt", dwaz_dt));
                    bll.vUpdatetuser_profile2(arr1);




                    arr1.Clear();
                    string nPassMsg = "Wazaran New Password for [" + SalespointCD + "] is : " + newPassword;
                    string Subject = "New Password for salespoint code : " + SalespointCD;
                    string Body = "This is your New Password to login into Wazaran : " + nPassMsg;
                    arr1.Add(new cArrayList("@emailsubject", Subject));
                    arr1.Add(new cArrayList("@msg", Body));
                    arr1.Add(new cArrayList("@to", email));
                    arr1.Add(new cArrayList("@doc_no", Request.Cookies["usr_id"].Value.ToString()));
                    arr1.Add(new cArrayList("@doc_typ", "rstpass"));
                    bll.vInsertEmailOutbox(arr1);

                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    lblMessage.Text = "Password has been changed successfully.";

                }
            }
        }
    }
    
    protected void btclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx");
    }
     public enum PasswordScore
{
   Blank = 0,
   Weak = 1,
   Medium = 2,
   Strong = 3,
   VeryStrong = 4
}
   public static PasswordScore CheckingPasswordStrength(string password)
   {
       int score = 1;
       if (password.Length < 1)
           return PasswordScore.Blank;
       if (password.Length < 4)
           return PasswordScore.Weak;
       if (password.Length >= 6) score++;
       if (Regex.IsMatch(password, @"[0-9]+(\.[0-9][0-9]?)?"))   //number only //"^\d+$" if you need to match more than one digit.
           score++;
       if (Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z]).+$")) //both, lower and upper case
           score++;
       if (Regex.IsMatch(password, @"[!,@,#,$,%,^,&,*,?,_,~,\-,£,(,),.,:,;,']")) //^[A-Z]+$
           score++;
       return (PasswordScore)score;
   }
}