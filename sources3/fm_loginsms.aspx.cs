using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_loginsms : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
          //  string sSMSCode = (string)Session["smscode"];
          //  Response.Write(sSMSCode);
            
        }
    }
    protected void btSend_Click(object sender, EventArgs e)
    {
        string sSmsCode = (string)Session["smscode"];
        if (sSmsCode.Trim() != txsms.Text.Trim())
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dd", "alert('Wrong SMS')", true);
            Session["smscode"] = sSmsCode;
        }
        else
        {
            Response.Redirect("default.aspx");
        }
    }
}