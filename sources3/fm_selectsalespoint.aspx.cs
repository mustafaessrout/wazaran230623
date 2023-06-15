using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_selectsalespoint : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if  (!IsPostBack)
        {
           bll.vBindingComboToSp(ref cbosp, "sp_tmst_salespoint_getall", "salespointcd", "salespoint_nm");
        }
    }
    protected void btselect_Click(object sender, EventArgs e)
    {

        Response.Cookies["sp"].Value = cbosp.SelectedValue.ToString();
        Response.Cookies["spn"].Value = bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() +  "'");
        Response.Redirect("default.aspx");
        
    }
    protected void cbosp_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
}