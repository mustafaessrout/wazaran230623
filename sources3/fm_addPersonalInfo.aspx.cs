using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using System.Data.SqlClient;

public partial class fm_addPersonalInfo : System.Web.UI.Page
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
        string sMobileNo = "";
        int cInfo = -1;
        SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();

        arr.Clear();
        arr.Add(new cArrayList("@emp_cd", Request.Cookies["usr_id"].Value.ToString()));
        bll.vGetuserInfo(arr, ref rs);
        while (rs.Read())
        {
            cInfo = Convert.ToInt32(rs["cinfo"].ToString());
        } rs.Close();

        if (cInfo == 0)
        {

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
                sMobileNo = '0' + MobileNo.Text;
            }

            arr.Clear();
            arr.Add((new cArrayList("@emp_cd", Request.Cookies["usr_id"].Value.ToString())));
            arr.Add((new cArrayList("@iqamaNumber", sID)));
            arr.Add((new cArrayList("@dob", dtBDay)));
            arr.Add((new cArrayList("@mobilenumber", sMobileNo)));
            arr.Add((new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString())));
            bll.vInsertTuserInfo(arr);
            dtBirthDate.Text = DateTime.Now.ToString("d/M/yyyy");
            Response.Redirect("fm_loginol.aspx");
        }
        else
        {
            Response.Redirect("fm_checkPersonalInfo.aspx");

        }
    }
}