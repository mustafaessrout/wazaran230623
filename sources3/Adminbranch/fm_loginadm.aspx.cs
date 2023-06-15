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
            //by yanto 6-1-2019
            HttpCookie scookusersql = new HttpCookie("usersql");
            scookusersql = Request.Cookies["usersql"];
            if (scookusersql == null)
            {
                Response.Cookies["usersql"].Value = "sa";
            }
            //-------------------------
            Response.Cookies["adm"].Value = null;
            //return;
            //Response.Cookies["adm"].Value = "irwanag";
            //Response.Cookies["sp"].Value = bll.sGetControlParameter("salespoint");
            //Response.Cookies["usr_id"].Value = bll.vLookUp("select emp_cd from tuser_profile where usr_id='" + txusername.Text + "'");
            //Response.Cookies["usr_nm"].Value = "2540:IRWAN AGUSYONO";
            //Response.Redirect("fm_main.aspx");
            Response.Cookies["waz_dt"].Value = bll.sGetControlParameter("wazaran_dt");
        }
        Response.Cookies["waz_dt"].Value = bll.sGetControlParameter("wazaran_dt");
    }
    protected void btlogin_Click(object sender, EventArgs e)
    {
        Response.Cookies["usersql"].Value = txusername.Text; //by yanto 19-11-2018
        Response.Cookies["waz_dt"].Value = bll.sGetControlParameter("wazaran_dt");
        List<cArrayList> arr = new List<cArrayList>();
        int nCheck = bll.nCheckUserPassword(txusername.Text, txpassword.Text);
        if (nCheck > 0)
        {
            string sEmp = bll.vLookUp("select emp_cd from tuser_profile where usr_id='" + txusername.Text +"'");
            string sFld = bll.vLookUp("select fld_valu from tfield_value where fld_nm='admwazaran' and fld_valu='" + sEmp + "'");
            if (sFld=="")
            {
                Response.Cookies["adm"].Value = null;
                return;
            }
            else
            {
                Response.Cookies["adm"].Value = txusername.Text;
                Response.Cookies["sp"].Value = bll.sGetControlParameter("salespoint");
                Response.Cookies["usr_id"].Value = bll.vLookUp("select emp_cd from tuser_profile where usr_id='" +txusername.Text+"'");
                Response.Cookies["usr_nm"].Value = "ADMINISTRATOR WAZARAN";
                Response.Redirect("fm_main.aspx");
            }

        }
        else {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Wrong username or password','Try Again','warning');", true);
        }
    }
}