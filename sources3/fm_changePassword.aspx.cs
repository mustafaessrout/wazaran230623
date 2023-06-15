using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

public partial class fm_ChangePassword : System.Web.UI.Page
{
    cbll bll = new cbll();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            NewPassword.Attributes.Add("autocomplete", "off"); 
            NewPassword.Text = string.Empty;
            NewPassword.Attributes["AUTOCOMPLETE"] = "off";
            NewPassword.AutoCompleteType = AutoCompleteType.Disabled;
            txpassword.Attributes.Add("autocomplete", "off");
            txpassword.Text = string.Empty;
            txpassword.Attributes["AUTOCOMPLETE"] = "off";
            txpassword.AutoCompleteType = AutoCompleteType.Disabled;
            //NewPassword.Text = "S123456";
        }
    }


    protected void btConfirm_Click(object sender, EventArgs e)
    {
        string oldpassword = "";
        string SalespointCD = "";
        string pwdexpdate = "";
        string email = "";
        int countpass = -1;
        SqlDataReader rs = null;
        string oldPassword = txpassword.Text;
        string newPassword = NewPassword.Text;
        string confirmNewPassword = ConfirmNewPassword.Text;
        string waz_dt = bll.vLookUp("select dbo.fn_getLatestDate()");
        DateTime dwaz_dt = System.DateTime.ParseExact(waz_dt.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);

        if (UserID.Text == "")
        {
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "The User ID is empty!";
            return;
        }
        else if (txpassword.Text == "")
        {
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "The Old Password is empty!";
            return;
        }
        else if (NewPassword.Text =="")
        {
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "The New Password is empty!";
            return;
        }
        else if (ConfirmNewPassword.Text =="")
        {
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = " is empty!";
            return;
        }
        else
        {
            string stUser = "";
            stUser = bll.vLookUp("select usr_id from tuser_profile where usr_id='"+UserID.Text+ "' and (select dbo.fn_decryptpassword(pwdbinary,'sbtc'))='"+txpassword.Text+"'");

            if (stUser == "")
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "The Old Password is not correct!";
                return;
            }

            if (txpassword.Text == NewPassword.Text)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "The Old Password same with new Password!";
                return;
            }

        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", UserID.Text));
        bll.vGetpassfromtuserprofile(ref rs, arr);

        while (rs.Read())
        {
            oldpassword = rs["passwdbinary"].ToString(); 
            SalespointCD =  rs["Salespointcd"].ToString();
            pwdexpdate = rs["pwd_exp_dt"].ToString();
            email = rs["email"].ToString();
        }
        rs.Close();

        if (newPassword != confirmNewPassword)
        {
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "New Password and Confirm New Password must be equal.";
            return;
        }
        else //(newPassword==pwd)
        {
            List<cArrayList> arr1 = new List<cArrayList>();
            arr1.Add(new cArrayList("@usr_id", UserID.Text));
            arr1.Add(new cArrayList("@NewPassword", newPassword));
            arr1.Add(new cArrayList("@Year", DateTime.Now.Year.ToString()));
            bll.vCheckOldpassNewpass(arr1, ref rs);
             while (rs.Read())
            {
                countpass = Convert.ToInt32(rs["countpass"].ToString());
            }
        rs.Close();

            //if(countpass !=0 || oldpassword == newPassword)
            //if (oldpassword == newPassword)
            //{
            //    lblMessage.ForeColor = System.Drawing.Color.Red;
            //    lblMessage.Text = "The Enterd Password has already been used, Please Enter a New Password!";
            //    return;
            //}
            //else
            //{
                int passchk = (int)CheckingPasswordStrength(newPassword);

                if (passchk >= 3)
               {

            arr1.Clear();
            arr1.Add(new cArrayList("@usr_id", UserID.Text));
            arr1.Add(new cArrayList("@NewPassword", newPassword));
            arr1.Add(new cArrayList("@OldPassword",oldpassword));
            arr1.Add(new cArrayList("@SalespointCD",SalespointCD));
            arr1.Add(new cArrayList("@period",DateTime.Now.Year.ToString()+DateTime.Now.Month.ToString()  ));
            arr1.Add(new cArrayList("@CreatedDate", dwaz_dt));
            arr1.Add(new cArrayList("@Year", DateTime.Now.Year.ToString()));
            bll.vInsertPassintotUserPasswordMap(arr1);


            arr1.Clear();
            arr1.Add(new cArrayList("@usr_id", UserID.Text));
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
            arr1.Add(new cArrayList("@doc_no", UserID.Text));
            arr1.Add(new cArrayList("@doc_typ" , "rstpass"));
            //bll.vInsertEmailOutbox(arr1);

            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Text = "Password has been changed successfully.";
            btConfirm.Enabled = false;
            Response.Redirect("fm_loginol.aspx");

               }
            //}
        }
        }
    }
    //protected void btClose_Click(object sender, EventArgs e)
    //{
       //Response.Redirect("fm_login5.aspx");
    //}

  
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