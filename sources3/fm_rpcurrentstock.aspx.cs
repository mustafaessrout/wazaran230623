using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_rpcurrentstock : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            dtbalance.Text = Request.Cookies["waz_dt"].Value.ToString();
            dtbalance.CssClass = "makeitreadwrite";
        }
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=stbal&dt=" + dtbalance.Text + "');", true);
    }
}