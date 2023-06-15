using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_loginsms2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btsubmit_Click(object sender, EventArgs e)
    {
        string sSmsCode = (string)Session["smscode"];

        if (txsms.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dd", "sweetAlert('Token can not empty','please try again !','error')", true);
            Session["smscode"] = sSmsCode;
            return;
        }

        if (sSmsCode.Trim() != txsms.Text.Trim())
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dd", "sweetAlert('Wrong token','please try again !','error')", true);
            Session["smscode"] = sSmsCode;
            return;
        }
        else
        {
            if (Request.Cookies["sp"].Value.ToString()=="0")
            {
                Response.Redirect("fm_selectsalespoint.aspx");
            }
            else
            {
                Response.Redirect("default.aspx");
            }
        }
    }
}