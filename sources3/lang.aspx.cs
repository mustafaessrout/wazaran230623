using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lang : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string sLang = Request.Cookies["lang"].Value.ToString();
        if (sLang == "EN")
        { Response.Cookies["lang"].Value = "SA"; }
        else { Response.Cookies["lang"].Value = "EN"; }
      
        Response.Redirect("default.aspx");
    }
}