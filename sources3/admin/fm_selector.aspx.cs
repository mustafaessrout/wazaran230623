using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_fm_selector : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Response.Redirect("fm_main.aspx");
            bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm");
        }
    }
    protected void btgo_Click(object sender, EventArgs e)
    {
        Response.Cookies["sp"].Value = cbsalespoint.SelectedValue.ToString();
        Response.Cookies["usr_id"].Value = "2540";
        string surl = bll.vLookUp("select comp_cd from tmst_salespoint where salespointcd='"+cbsalespoint.SelectedValue.ToString()+"'");
        Response.Redirect(surl);
    }
}