using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class promotor_fm_login : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingComboToSp(ref cbexhibition, "sp_tmst_exhibition_get", "exhibit_cd", "exhibit_nm");
            //Response.Cookies["usr_id"].Value = "2540";
            //Response.Cookies["usr_nm"].Value = "Irwan Agusyono";
            //Response.Cookies["exh_dt"].Value = "5/1/2017";
            //Response.Cookies["exh_cd"].Value = "EH005170003";
            //Response.Redirect("default.aspx");
          
        }

        
    }
    protected void btlogin_Click(object sender, EventArgs e)
    {
        if (bll.nCheckUserPassword(txuserid.Text, txpassword.Text) == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Wrong User or Password!','Try Again','error');", true);
            return;
        }
        else {
            if (cbexhibition.Items.Count > 0)
            {
                Response.Cookies["exh_cd"].Value = cbexhibition.SelectedValue.ToString();
                Response.Cookies["exh_dt"].Value = Convert.ToDateTime( bll.vLookUp("select dbo.fn_getexhdate('"+cbexhibition.SelectedValue.ToString()+"')")).ToString("d/M/yyyy");
                Response.Cookies["usr_id"].Value = bll.vLookUp("select emp_cd from tuser_profile where usr_id='" + txuserid.Text + "'");
                Response.Cookies["usr_nm"].Value = bll.vLookUp("select (emp_cd+':'+emp_nm) from tmst_employee where emp_cd='" + Request.Cookies["usr_id"].Value.ToString() + "'");
                Response.Redirect("index.aspx");
            }
            else
            {
                Response.Cookies["usr_id"].Value = bll.vLookUp("select emp_cd from tuser_profile where usr_id='" + txuserid.Text + "'");
                Response.Cookies["usr_nm"].Value = bll.vLookUp("select (emp_cd+':'+emp_nm) from tmst_employee where emp_cd='" + Request.Cookies["usr_id"].Value.ToString() + "'");
                Response.Redirect("index.aspx");
            }
        }
    }
}