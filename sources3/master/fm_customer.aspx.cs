using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class master_fm_customer : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_getbyall", "salespointcd", "salespoint_nm");
        }
    }
    protected void btview_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tmst_customer_getbysp", arr);
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('/fm_report2.aspx?src=custsp&sp="+cbsalespoint.SelectedValue.ToString()+"')", true);
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
       
}protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
{
    grd.PageIndex = e.NewPageIndex;
    List<cArrayList> arr = new List<cArrayList>();
    arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
    bll.vBindingGridToSp(ref grd, "sp_tmst_customer_getbysp", arr);
}
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('/lookup_map.aspx?ct=1');", true);
    }
}
