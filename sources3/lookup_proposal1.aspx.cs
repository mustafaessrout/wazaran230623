using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lookup_proposal1 : System.Web.UI.Page
{
    cbll bll = new cbll();
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
        string type = "";
        if (Request.QueryString["reportType"] == "FG")
        {
            type = "FG";

        }
        else if (Request.QueryString["reportType"] == "DC")
        {
            type = "CH";
        }
        else
        {
            type = "";
        }
        arr.Add(new cArrayList("@type", type));
        arr.Add(new cArrayList("@month", Request.QueryString["month"]));
        arr.Add(new cArrayList("@year", Request.QueryString["year"]));
        bll.vBindingGridToSp(ref grd, "sp_getProposal1", arr);
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
    protected void btsearch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@prop_no", txsearchprop.Text));
        arr.Add(new cArrayList("@month", Request.QueryString["month"]));
        arr.Add(new cArrayList("@year", Request.QueryString["year"]));
        bll.vBindingGridToSp(ref grd, "sp_tmst_discount_search", arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix()", true);
    }

    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbdue = (Label)e.Row.FindControl("lbdue");
            if (!string.IsNullOrEmpty(lbdue.Text))
            {
                if (Convert.ToInt32(lbdue.Text) <= 3)
                {                    
                    
                    e.Row.Cells[4].BackColor = System.Drawing.Color.Red;
                }
                else
                {

                    e.Row.Cells[4].BackColor = System.Drawing.Color.LightGreen;
                }
            }
        }
    }
}