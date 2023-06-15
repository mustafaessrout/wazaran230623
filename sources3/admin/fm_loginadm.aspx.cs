using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_fm_loginadm : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Response.Cookies["adm"].Value = null;
            ////return;
            //Response.Cookies["adm"].Value = "irwanag";
            //Response.Cookies["sp"].Value = bll.sGetControlParameter("salespoint");
            //if (Request.Cookies["usr_id"].Value.ToString() == "")
            //{ Response.Cookies["usr_id"].Value = bll.vLookUp("select emp_cd from tuser_profile where usr_id='" + txusername.Text + "'"); }
            //Response.Cookies["usr_nm"].Value = "2540:IRWAN AGUSYONO";
            //Response.Redirect("fm_main.aspx");

            //Response.Cookies["usr_nm"].Value = Request.Cookies["usr_id"].Value.ToString() + " : " + bll.vLookUp("select fullname from tuser_profile where emp_cd='" + Request.Cookies["usr_id"].Value.ToString() + "'");
            //Response.Redirect("fm_main.aspx");
        }
    }
    protected void btlogin_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        int nCheck = bll.nCheckUserPassword(txusername.Text, txpassword.Text);
        if (nCheck > 0)
        {
            string _emp = bll.vLookUp("select emp_cd from tuser_profile where usr_id='" + txusername.Text + "'");
            string sFld = bll.vLookUp("select fld_valu from tfield_value where fld_nm='admwazaran' and fld_valu='" + _emp + "'");
            Response.Cookies["usr_id"].Value = _emp;
            if (sFld == "")
            {
                Response.Cookies["adm"].Value = null;
                return;
            }
            else
            {
                Response.Cookies["adm"].Value = txusername.Text;
               // Response.Cookies["sp"].Value = bll.sGetControlParameter("salespoint");
                if (Request.Cookies["usr_id"].Value == null)
                { Response.Cookies["usr_id"].Value = bll.vLookUp("select emp_cd from tuser_profile where usr_id='" + txusername.Text + "'"); }
                Response.Redirect("fm_main.aspx");
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Wrong username or password','Try Again','warning');", true);
        }
    }
}