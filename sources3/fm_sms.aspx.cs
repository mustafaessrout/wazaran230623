using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_sms : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btlogin_Click(object sender, EventArgs e)
    {
        string sSmsCode = (string)Session["smscode"];
        string susr_id = Request.Cookies["usr_id"].Value.ToString();
        string sSmsCode2 = bll.vLookUp("select dbo.fn_token('" + susr_id + "')");
        if (txsms.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dd", "sweetAlert('Token can not empty','please try again !','error')", true);
            Session["smscode"] = sSmsCode;
            return;
        }
        if (sSmsCode2.Trim() != txsms.Text.Trim()) //by yanto 05-22-2017 for offline token
        {
            if (sSmsCode.Trim() != txsms.Text.Trim())
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dd", "sweetAlert('Wrong token','please try again !','error')", true);
                Session["smscode"] = sSmsCode;
                return;
            }
        }
        else
        {
            if (Request.Cookies["sp"].Value.ToString() == "0")
            {
                Response.Redirect("fm_selectsalespoint.aspx");
            }
            else
            {
                Response.Cookies["sms"].Value = sSmsCode;
                Response.Redirect("default.aspx");
            }

        }
    }
}