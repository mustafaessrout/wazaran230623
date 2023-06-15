using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_itempricelist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_itempriceentry.aspx");
    }
}