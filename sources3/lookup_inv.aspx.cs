using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lookup_inv : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindingGrid();
        }
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        if (txinv.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Invoice No','Should be inserted','warning');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@inv_no", txinv.Text));
        bll.vBindingGridToSp(ref grd, "sp_tdosales_invoice_getbyreceived1", arr);
    }
    void BindingGrid()
    {
        bll.vBindingGridToSp(ref grd, "sp_tdosales_invoice_getbyreceived");
    }
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd.PageIndex = e.NewPageIndex;
        BindingGrid();
    }
}