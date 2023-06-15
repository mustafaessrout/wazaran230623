using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_fm_userreg : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingComboToSp(ref cbbranch, "sp_tmst_salespoint_get", "salespointcd", "salespoint_desc");
            // cbbranch.SelectedValue = Request.Cookies["sp"].Value.ToString();
            bll.vBindingComboToSp(ref cbdepartment, "sp_tmst_role_get", "role_cd", "role_name");

        }
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        if (hdemp_cd.Value.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alr", "sweetAlert('Please select employee','','error');", true);
            return;
        }
        if (txemail.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alr", "sweetAlert('Please insert employee email','','error');", true);
            return;
        }
        if (txmobile.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alr", "sweetAlert('Please insert employee mobile No.','','error');", true);
            return;
        }
        if (txuid.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alr", "sweetAlert('Please insert employee username','','error');", true);
            return;
        } if (txconfirmpass.Text == "" || txpassword.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alr", "sweetAlert('Please insert user password','','error');", true);
            return;
        }
        if (txconfirmpass.Text != txpassword.Text)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alr", "sweetAlert('Please  password and password confirmation should be same','','error');", true);
            return;
        }
        btsave.Enabled = false;
        btsave.Visible = true;

        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", txuid.Text));
        arr.Add(new cArrayList("@emp_cd", hdemp_cd.Value.ToString()));
        arr.Add(new cArrayList("@fullname", txshortname.Text));
        arr.Add(new cArrayList("@email", txemail.Text));
        arr.Add(new cArrayList("@mobile_no", txmobile.Text));
        arr.Add(new cArrayList("@passwd", txpassword.Text));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@role_cd", cbdepartment.SelectedValue.ToString()));
        arr.Add(new cArrayList("@home_no", txmobile.Text));       
        arr.Add(new cArrayList("@isactive", "1"));

        if (Request.Cookies["sp"].Value.ToString() != "0")
        {
            bll.vInsertUserProfile(arr);
        }
        //{
        //    bll.vInsertTuserProfileHO(arr);
        //}
        //else
        //{
        //   // bll.vInsertTuserProfileHO(arr);
        //    bll.vInsertTuserProfile(arr);
        //}
       
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alr", "sweetAlert('You have sucessfully registered','New User');", true);
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        List<string> lcust = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string scust = string.Empty;
        System.Data.SqlClient.SqlDataReader rs = null;
        //arr.Add(new cArrayList("@cust_cd", prefixText));
        arr.Add(new cArrayList("@emp_nm", prefixText));
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        bll.vSearchMstEmployee(arr, ref rs);
        //bll.vSearchMstCustomer(arr, ref rs);
        while (rs.Read())
        {
            scust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["emp_cd"].ToString() + "-" + rs["emp_nm"].ToString(), rs["emp_cd"].ToString());
            lcust.Add(scust);
        }
        rs.Close();

        return (lcust.ToArray());
    }
    protected void btrefresh_Click(object sender, EventArgs e)
    {
        string emp_cd;
        emp_cd = bll.vLookUp("select 1 from tuser_profile where emp_cd='" + hdemp_cd.Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
        if (emp_cd == "1")
        {
            List<cArrayList> arr = new List<cArrayList>();
            System.Data.SqlClient.SqlDataReader rs = null;
            arr.Clear();
            arr.Add(new cArrayList("@emp_cd", hdemp_cd.Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vSearchtuserprofile(arr, ref rs);
            while (rs.Read())
            {
                btsave.Visible = false;
                btupd.Visible = true;
                txshortname.Text = "";
                txshortname.Text = rs["fullname"].ToString();
                txuid.Text = "";
                txuid.Text = rs["usr_id"].ToString();
                txuid.CssClass = "ro";
                cbbranch.CssClass = "ro";
                txpassword.Text = "";
                txpassword.Text = rs["passwd"].ToString();
                txconfirmpass.Text = "";
                txconfirmpass.Text = rs["passwd"].ToString();
                txemail.Text = "";
                txemail.Text = rs["email"].ToString();
                txmobile.Text = "";
                txmobile.Text = rs["mobile_no"].ToString();
                if (rs["salespointcd"].ToString() != "" || rs["salespointcd"].ToString() == null)
                {
                    cbbranch.SelectedValue = rs["salespointcd"].ToString();
                }
                if (rs["role_cd"].ToString() != "" || rs["role_cd"].ToString() == null)
                {
                    cbdepartment.SelectedValue = rs["role_cd"].ToString();
                }
            }
            rs.Close();
            cbbranch.SelectedValue = Request.Cookies["sp"].Value.ToString();
        }
        else
        {
            txshortname.Text = "";
            txuid.Text = "";
            txuid.CssClass = "";
            cbbranch.CssClass = "";
            txpassword.Text = "";
            txconfirmpass.Text = "";
            txemail.Text = "";
            txmobile.Text = "";
            List<cArrayList> arr = new List<cArrayList>();
            System.Data.SqlClient.SqlDataReader rs = null;
            arr.Clear();
            arr.Add(new cArrayList("@emp_nm", hdemp_cd.Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vSearchMstEmployee(arr, ref rs);
            while (rs.Read())
            {
                txshortname.Text = rs["emp_nm"].ToString().Substring(0, 10);
                txuid.Text = rs["emp_nm"].ToString().Substring(0, 3) + hdemp_cd.Value.ToString();
            }
            rs.Close();
            txpassword.Text = "Sbtc123";
            txconfirmpass.Text = "Sbtc123";
        }
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("../admin/fm_userreg.aspx");
    }
    protected void btupd_Click(object sender, EventArgs e)
    {
        if (hdemp_cd.Value.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alr", "sweetAlert('Please select employee','','error');", true);
            return;
        }
        if (txemail.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alr", "sweetAlert('Please insert employee email','','error');", true);
            return;
        }
        if (txmobile.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alr", "sweetAlert('Please insert employee mobile No.','','error');", true);
            return;
        }
        if (txuid.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alr", "sweetAlert('Please insert employee username','','error');", true);
            return;
        } if (txconfirmpass.Text == "" || txpassword.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alr", "sweetAlert('Please insert user password','','error');", true);
            return;
        }
        if (txconfirmpass.Text != txpassword.Text)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alr", "sweetAlert('Please  password and password confirmation should be same','','error');", true);
            return;
        }
        btsave.Enabled = false;
        btsave.Visible = true;

        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@emp_cd", hdemp_cd.Value.ToString()));
        arr.Add(new cArrayList("@fullname", txshortname.Text));
        arr.Add(new cArrayList("@email", txemail.Text));
        arr.Add(new cArrayList("@mobile_no", txmobile.Text));
        arr.Add(new cArrayList("@passwd", txpassword.Text));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@role_cd", cbdepartment.SelectedValue.ToString()));
        arr.Add(new cArrayList("@server", null));
        if (Request.Cookies["sp"].Value.ToString() == "0")
        {
            bll.vUpdateTuserProfile(arr);
        }
        else
        {
            bll.vUpdateTuserProfileHO(arr);
            bll.vUpdateTuserProfile(arr);
        }

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alr", "sweetAlert('Youhave sucessfully registered','Done');", true);
    }
}