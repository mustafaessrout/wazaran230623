using System;
using System.Activities.Tracking;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lookupfile : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string _src = Request.QueryString["src"];
            if (_src == "CH")
            {
                img.ImageUrl = @"/images/deposit/" + Request.QueryString["f"] + ".jpeg";
            }
            else if ((_src == "CQ") || (_src == "BT"))
            {
                img.ImageUrl = @"/images/payment/" + bll.vLookUp("select picture from ttab_payment_info where tab_no='" + Request.QueryString["f"] +"'");
            }
        }
    }
}