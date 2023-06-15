using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_mgtsales : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            dtstart.Text = Request.Cookies["waz_dt"].Value.ToString();
            dtend.Text =  Request.Cookies["waz_dt"].Value.ToString();
            bll.vBindingComboToSp(ref cbsp, "sp_tmst_salespoint_get","salespointcd","salespoint_nm");
        }
    }
}