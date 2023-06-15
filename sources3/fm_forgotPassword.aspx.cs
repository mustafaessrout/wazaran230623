using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

public partial class fm_forgotPassword : System.Web.UI.Page
{
    cbll bll = new cbll();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Label2.Visible = false;
            Email.Visible = false;
        }

    }


    protected void btSubmit_Click(object sender, EventArgs e)
    {
        string userid = UserID.Text;
        string tEmail = Email.Text;
        string email = "";
        string SalespointCD = "";
        string oldPassword = "";
        bool  bCheck = false;
        string emp_cd = "";
        SqlDataReader rs = null;
        string sNewPassword = (Guid.NewGuid().ToString()).Substring(0 , 6);
        string waz_dt = bll.vLookUp("select dbo.fn_getLatestDate()");
        DateTime  dwaz_dt = System.DateTime.ParseExact(waz_dt.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        int cInfo = -1;
        string stemail = "", usr = "";
     
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", userid));
        //arr.Add(new cArrayList("@email", tEmail));
        bll.vGetemailfromtuserprofile(ref rs, arr);
        //usr = 0 ok ,usr = -1 not ok, 
        //stemail = 1 ok , stemail = -1 not ok, stemail = 0 usr not ok
        if  (UserID.Text != "" )
        {
            while (rs.Read())
            {
                bCheck = true;
                usr = rs["usr"].ToString();
                stemail = rs["stemail"].ToString();
                if ((usr == "0" && stemail == "1") || (usr == "0" && stemail == "-1"))
                {
                    //email = rs["email"].ToString();
                    SalespointCD = rs["Salespointcd"].ToString();
                    oldPassword = rs["passwdbinary"].ToString();
                    emp_cd = rs["emp_cd"].ToString();
                }
            }
            rs.Close();
        }

         if (bCheck == false)
        {
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "The User id or Email or both are not Entered, Please try again !";
            return;
        }

         else if (usr == "-1")
         {
             lblMessage.ForeColor = System.Drawing.Color.Red;
             lblMessage.Text = "The Entered User Id is invalid, Please try again !";
             return;
         }
         else if (stemail == "-1")
         {
             lblMessage.ForeColor = System.Drawing.Color.Red;
             lblMessage.Text = "The Entered Email is invalid, Please try again !";
             return;

         }
         else
         {
             Response.Cookies["usr_id"].Value = emp_cd;
             Response.Cookies["sp"].Value = SalespointCD;
             Response.Cookies["spn"].Value = bll.sGetSalespointname(SalespointCD);
             Response.Cookies["oldPassword"].Value = oldPassword;
             Response.Cookies["NewPassword"].Value = sNewPassword;
             Response.Cookies["email"].Value = tEmail;

             arr.Clear();
             arr.Add(new cArrayList("@emp_cd", emp_cd));
             bll.vGetuserInfo(arr, ref rs);
             while (rs.Read())
             {
                 cInfo = Convert.ToInt32(rs["cinfo"].ToString());
             } rs.Close();
             arr.Clear();

             if (cInfo == 0)
             {
                 lblMessage.ForeColor = System.Drawing.Color.Red;
                 lblMessage.Text = "Your Personal Information is not Available, Please Add your Personal Information Immediately and Try Again!";
                 btInfo.Visible = true;
                 btSubmit.Visible = false;
             }
             else if (cInfo == 1)
             { Response.Redirect("fm_checkPersonalInfo.aspx"); }
             else
             {
                 lblMessage.ForeColor = System.Drawing.Color.Red;
                 lblMessage.Text = "There is a Problem, Please Contact System Admin Immediately!";
                 btInfo.Visible = false;
                 btSubmit.Visible = false;
             }
        
             //arr.Clear();
             //arr.Add(new cArrayList("@usr_id", emp_cd));
             //arr.Add(new cArrayList("@passwd", sNewPassword));
             //arr.Add(new cArrayList("@waz_dt", dwaz_dt));
             //bll.vUpdatetuser_profiletemp(arr);

             //arr.Clear();
             //arr.Add(new cArrayList("@usr_id", emp_cd));
             //arr.Add(new cArrayList("@NewPassword", sNewPassword));
             //arr.Add(new cArrayList("@OldPassword", oldPassword));
             //arr.Add(new cArrayList("@SalespointCD", SalespointCD));
             //arr.Add(new cArrayList("@period", bll.sGetControlParameter("period")));
             //arr.Add(new cArrayList("@CreatedDate", dwaz_dt));
             //arr.Add(new cArrayList("@Year", bll.sGetControlParameter("wazaran_year")));
             //bll.vInserttempPassintotUserPasswordMap(arr);



             //arr.Clear();
             //string tPassMsg = "Wazaran Temp Password for [" + SalespointCD + "] is : " + sNewPassword;
             //string Subject = "Temp Password for salespoint code : " + SalespointCD;
             //string Body = "This is your Temp Password to login into Wazaran : " + tPassMsg;
             //arr.Add(new cArrayList("@emailsubject", Subject));
             //arr.Add(new cArrayList("@msg", Body));
             //arr.Add(new cArrayList("@to", email));
             //arr.Add(new cArrayList("@doc_no", emp_cd));
             //arr.Add(new cArrayList("@doc_typ", "temppass"));
             //bll.vInsertEmailOutbox(arr);

             //lblMessage.ForeColor = System.Drawing.Color.Green;
             //lblMessage.Text = "Password has been sent successfully.";
             //btSubmit.Enabled = false;
             //Response.Redirect("fm_login5.aspx");

         }
               
            
        
    }
    //protected void btClose_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("fm_login5.aspx");
    //}
    protected void btInfo_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_addPersonalInfo.aspx");

    }

}