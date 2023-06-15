using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class vendor_list : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            vInitGrid();
        }
    }

    void vInitGrid()
    {
        bll.vBindingGridToSp(ref grd, "sp_tmst_vendor_get");
    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        Response.Redirect("vendor_entry.aspx");
    }
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        grd.PageIndex = e.NewPageIndex;
        bll.vBindingGridToSp(ref grd, "sp_tmst_vendor_get");
    
    }
}