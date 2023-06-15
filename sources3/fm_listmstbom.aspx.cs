using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_listmstbom : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //dtstart.Text = Request.Cookies["waz_dt"].Value;
            //dtend.Text = Request.Cookies["waz_dt"].Value;

            string _salespointcd = Request.Cookies["sp"].Value;

            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", _salespointcd));
            //bll.vBindingComboToSpWithEmptyChoosen(ref cbsalespoint, "sp_salespointstructure_member", "salespointcd", "salespoint_nm", arr);
            //cbsalespoint.SelectedValue = _salespointcd;
            bll.vBindingGridToSp(ref grd, "sp_Listmstbom", arr);

            ribbon.Style.Add("display", "normal");
            lbstatus.Style.Add("display", "normal");

            //cbsalespoint.CssClass = cd.csstext;

        }
    }

    protected void btprint_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        //arr.Add(new cArrayList("@startdate", System.DateTime.ParseExact(dtstart.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        //arr.Add(new cArrayList("@enddate", System.DateTime.ParseExact(dtend.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));

        //Session["lParamOflistdo"] = arr;
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=listofdo');", true);
        Session["lParamOflistmstbom"] = arr;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=listmstbom');", true);


    }

    protected void grd_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btview_Click(object sender, EventArgs e)
    {

        List<cArrayList> arr = new List<cArrayList>();

        //arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue));
        bll.vBindingGridToSp(ref grd, "sp_Listmstbom", arr);
        //dtstart.CssClass = cd.csstextro;
        //dtend.CssClass = cd.csstextro;
        btprint.Style.Add("display", "normal");
        ribbon.Style.Add("display", "normal");
        lbstatus.Style.Add("display", "normal");

    }

    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        grd.PageIndex = e.NewPageIndex;
        //arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue));
        //arr.Add(new cArrayList("@startdate", System.DateTime.ParseExact(dtstart.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        //arr.Add(new cArrayList("@enddate", System.DateTime.ParseExact(dtend.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));

        bll.vBindingGridToSp(ref grd, "sp_ListOfDO", arr);

    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_listmstbom.aspx");
    }
    protected void btmstbom_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_mstbompack.aspx");
    }

    protected void btsearchi_Click(object sender, EventArgs e)
    {
        {
            string do_no = txsearch.Text;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@number", do_no));

            bll.vBindingGridToSp(ref grd, "sp_Listmstbom_search", arr);

        }

    }

    protected void cbsalespoint_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void cbreport_SelectedIndexChanged(object sender, EventArgs e)
    {

    }


    // Variable to hold employee count

    int Count = 0;
    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView grdbom = (GridView)e.Row.FindControl("grdbom");
            Label lbbomcd = (Label)e.Row.FindControl("lbbomcd");
            arr.Add(new cArrayList("@bom_cd", lbbomcd.Text));
            bll.vBindingGridToSp(ref grdbom, "sp_tbom_detail_get", arr);
        }

        // If the row is Data row
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Increment No count
            Count += 1;
        }

        // If the row is a footer row
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            // Clear all the cells in the footer row
            e.Row.Cells.Clear();
            // Create a new table cell
            TableCell tableCell = new TableCell();
            // Set the ColumnSpan 
            tableCell.ColumnSpan = 10;
            // Set the Text alignment
            tableCell.HorizontalAlign = HorizontalAlign.Center;
            // Set the text that you want to display in the footer
            //tableCell.Text = "Total Pencarian = " + Count.ToString();
            lbstatus.Text = "Total Pencarian  " + Count.ToString();

            // Finally add the cell to the footer row
            e.Row.Cells.Add(tableCell);
        }


    }
    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //if (Request.Cookies["waz_dt"].Value.ToString() != dtstocktopname.Text)
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "swal('Attention','Transaction Date Out Of Periode Date','error');", true);
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
        //    return;
        //}
        Label lbbomcd = (Label)grd.Rows[e.RowIndex].FindControl("lbbomcd");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@bom_cd", lbbomcd.Text));
        //bll.vDeletettmst_bom_dtl(arr);
       // bll.vUpdatetmst_bom(arr);
        bindinggrd();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "swal('Success','Data Inactive successfully !','success');", true);
    }
    private void bindinggrd()
    {
        string _salespointcd = Request.Cookies["sp"].Value;

        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", _salespointcd));
        bll.vBindingGridToSp(ref grd, "sp_Listmstbom", arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
    }
}
