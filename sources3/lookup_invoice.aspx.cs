using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lookup_invoice : System.Web.UI.Page
{
    cbll bll = new cbll();
    double totalQty = 0;
    double totalInv = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            vBindingGrid();
        }
    }

    void vBindingGrid()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@so_cd", Request.QueryString["number"]));
        arr.Add(new cArrayList("@type", Request.QueryString["type"]));
        lblNumber.Text = Request.QueryString["number"];
        bll.vBindingGridToSp(ref grd, "sp_getInvDetails", arr);
    }
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd.PageIndex = e.NewPageIndex;
        vBindingGrid();
        //bll.vBindingGridToSp(ref grd, "sp_tmst_salesorder_get", arr);
    }
    protected void cbstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        vBindingGrid();
    }
    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       /* if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbsubtotal = (Label)e.Row.FindControl("qty");
            totalQty += Convert.ToDouble(lbsubtotal.Text);
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbtotsubtotal = (Label)e.Row.FindControl("lblTotalQty");
            lbtotsubtotal.Text = totalQty.ToString("N2");
            //e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            //            lblTFreeItem.Text = lbtotsubtotal.Text;
        }*/
    }
}