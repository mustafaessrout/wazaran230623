using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using System.Data.SqlClient;

public partial class fm_checkPersonalInfo : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dtBirthDate_CalendarExtender.EndDate = System.DateTime.ParseExact(bll.vLookUp("select dbo.fn_getLatestDate()"), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).AddYears(-Convert.ToInt32(bll.sGetControlParameter("bdateYear")));
        }
    }
    protected void btSubmit_Click(object sender, EventArgs e)
    {
        string sID = "";
        string waz_dt = bll.vLookUp("select dbo.fn_getLatestDate()");
        DateTime dtBDay = System.DateTime.ParseExact(waz_dt.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime dwaz_dt = System.DateTime.ParseExact(waz_dt.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        string sMobileNo = "";
        SqlDataReader rs = null;
        int countEmp = -1;

        if (UserID.Text == "")
        {
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "The Id Number Can not Be Empty, Please Try Again !";
            return;
        }
        else { sID = UserID.Text; }
        if (dtBirthDate.Text == "")
        {
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "The Birth Date Can not Be Empty, Please Try Again !";
            return;
        }
        else
        {
            dtBDay = DateTime.ParseExact(dtBirthDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        }
        if (MobileNo.Text == "")
        {
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "The Mobile Number Can not Be Empty, Please Try Again !";
            return;
        }
        else
        {
             sMobileNo ="0" + MobileNo.Text;
        }

        List<cArrayList> arr = new List<cArrayList>();
        arr.Add((new cArrayList("@emp_cd", Request.Cookies["usr_id"].Value.ToString())));
        arr.Add((new cArrayList("@iqamaNumber", sID)));
        arr.Add((new cArrayList("@dob", dtBDay)));
        arr.Add((new cArrayList("@mobilenumber", sMobileNo)));
        bll.vCheckTuserInfo(arr,ref rs);
         while (rs.Read())
        {
          countEmp = Convert.ToInt32(rs["countemp"].ToString());
        } rs.Close();

        if (countEmp != 1)
        {
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "Your Personal Info is Not Correct, Please Try Again or Contact System Admin!";
            return;
        }
        if (countEmp == 1)
        {
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@passwd", Request.Cookies["NewPassword"].Value.ToString()));
            arr.Add(new cArrayList("@waz_dt", dwaz_dt));
            bll.vUpdatetuser_profiletemp(arr);

            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@NewPassword", Request.Cookies["NewPassword"].Value.ToString()));
            arr.Add(new cArrayList("@OldPassword", Request.Cookies["oldPassword"].Value.ToString()));
            arr.Add(new cArrayList("@SalespointCD", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@period", bll.sGetControlParameter("period")));
            arr.Add(new cArrayList("@CreatedDate", dwaz_dt));
            arr.Add(new cArrayList("@Year", bll.sGetControlParameter("wazaran_year")));
            bll.vInserttempPassintotUserPasswordMap(arr);



            arr.Clear();
            string tPassMsg = "Wazaran Temp Password for [" + Request.Cookies["sp"].Value.ToString() + "] is : " + Request.Cookies["NewPassword"].Value.ToString();
            string Subject = "Temp Password for salespoint code : " + Request.Cookies["sp"].Value.ToString();
            string Body = "This is your Temp Password to login into Wazaran : " + tPassMsg;
            arr.Add(new cArrayList("@emailsubject", Subject));
            arr.Add(new cArrayList("@msg", Body));
            arr.Add(new cArrayList("@to", Request.Cookies["email"].Value.ToString()));
            arr.Add(new cArrayList("@doc_no", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@doc_typ", "temppass"));
            bll.vInsertEmailOutbox(arr);

            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Text = "Password has been sent successfully.";
            btSubmit.Enabled = false;
            Response.Redirect("fm_loginol.aspx");
        
        }
        
    }
}
