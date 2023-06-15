using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_Error : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var error = Session["error"];
            lblError.Text = error.ToString();
        }
    }
    protected void Submit1_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "updpnl", "closewin()", true);
        //Response.Redirect(Request.RawUrl);    
        //Response.Redirect(Request.Url.AbsoluteUri);
    }
}