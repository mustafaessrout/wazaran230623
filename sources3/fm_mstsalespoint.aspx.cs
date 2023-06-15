using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_mstsalespoint : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                listBranch.Attributes.Remove("style");
                newBranch.Attributes.Add("style", "display:none");
                btsave.Visible = false;
                //lbsalespoint_cd.Text = "NEW";
                bll.vBindingGridToSp(ref grd, "sp_tmst_salespoint_get");
            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstsalespoint");
                Response.Redirect("fm_ErrorPage.aspx");
            }
            
        }
    }

    protected void btnew_Click(object sender, EventArgs e)
    {
        //Response.Redirect("fm_mstsalespoint.aspx");
        newBranch.Attributes.Remove("style");
        btsave.Visible = true;
        btsave.Text = "Save";
        //lbsalespoint_cd.Text = "NEW";
        txsalespoint_cd.Text = "";
        txsalespoint_nm.Text = "";
        txsalespoint_sn.Text = "";
        txphone.Text = "";
        txemail.Text = "";
        txaddress.Text = "";
    }

    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {

            if (bll.nCheckAccess("mstbranchsave", Request.Cookies["usr_id"].Value.ToString()) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','Create/Modify Branch','warning');", true);
                return;
            }

            if (txsalespoint_cd.Text == "" || txsalespoint_nm.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Branch Code / Name must be filled...','Create/Modify Branch','warning');", true);
                return;
            }

            List<cArrayList> arr = new List<cArrayList>();
            if (btsave.Text == "Save")
            {
                arr.Clear();
                arr.Add(new cArrayList("@salespointcd", txsalespoint_cd.Text));
                arr.Add(new cArrayList("@salespoint_typ", cbType.SelectedValue.ToString()));
                arr.Add(new cArrayList("@salespoint_nm", txsalespoint_nm.Text));
                arr.Add(new cArrayList("@salespoint_sn", txsalespoint_sn.Text));
                arr.Add(new cArrayList("@phone_no", txphone.Text));
                arr.Add(new cArrayList("@email", txemail.Text));
                arr.Add(new cArrayList("@addr1", txaddress.Text));
                bll.vInsertMstSalespoint(arr);
            }
            else
            {
                arr.Clear();
                arr.Add(new cArrayList("@salespointcd", txsalespoint_cd.Text));
                arr.Add(new cArrayList("@salespoint_typ", cbType.SelectedValue.ToString()));
                arr.Add(new cArrayList("@salespoint_nm", txsalespoint_nm.Text));
                arr.Add(new cArrayList("@salespoint_sn", txsalespoint_sn.Text));
                arr.Add(new cArrayList("@phone_no", txphone.Text));
                arr.Add(new cArrayList("@email", txemail.Text));
                arr.Add(new cArrayList("@addr1", txaddress.Text));
                bll.vUpdateMstSalespoint(arr);
            }
            //lbsalespoint_cd.Text = "NEW";
            txsalespoint_cd.Text = "";
            txsalespoint_nm.Text = "";
            txsalespoint_sn.Text = "";
            txphone.Text = "";
            txemail.Text = "";
            txaddress.Text = "";
            btsave.Visible = false;
            newBranch.Attributes.Add("style", "display:none");
            bll.vBindingGridToSp(ref grd, "sp_tmst_salespoint_get");
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstsalespoint");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd.PageIndex = e.NewPageIndex;
        bll.vBindingGridToSp(ref grd, "sp_tmst_salespoint_get");
    }

    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {

            Label lbsalespointcd = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbsalespointcd");
            SqlDataReader rs = null;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", lbsalespointcd.Text));
            bll.vGetMstSalespoint(arr, ref rs);
            while (rs.Read())
            {
                txsalespoint_cd.Text = rs["salespointcd"].ToString();
                txsalespoint_nm.Text = rs["salespoint_nm"].ToString();
                txsalespoint_sn.Text = rs["salespoint_sn"].ToString();
                txphone.Text = rs["phone_no"].ToString();
                txemail.Text = rs["email"].ToString();
                txaddress.Text = rs["addr1"].ToString();
            }
            rs.Close();
            txsalespoint_cd.CssClass = "makeitreadonly ro form-control";
            newBranch.Attributes.Remove("style");
            btsave.Visible = true;
            btsave.Text = "Update";
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstsalespoint");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void grd_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //Adding a column manually once the header created
        //if (e.Row.RowType == DataControlRowType.Header) // If header created
        //{
        //    GridView SPGrid = (GridView)sender;

        //    // Creating a Row
        //    GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

        //    // Merging Column
        //    TableCell HeaderCell = new TableCell();
        //    HeaderCell.Text = "Code#";
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderCell.RowSpan = 2; // For merging first, second row cells to one
        //    HeaderCell.CssClass = "HeaderStyle";
        //    HeaderRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "Branch Name";
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderCell.RowSpan = 2;
        //    HeaderCell.CssClass = "HeaderStyle";
        //    HeaderRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "Branch ShortName";
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderCell.RowSpan = 2;
        //    HeaderCell.CssClass = "HeaderStyle";
        //    HeaderRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "Address";
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderCell.RowSpan = 2;
        //    HeaderCell.CssClass = "HeaderStyle";
        //    HeaderRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "Phone No";
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderCell.RowSpan = 2;
        //    HeaderCell.CssClass = "HeaderStyle";
        //    HeaderRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "Email";
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderCell.RowSpan = 2;
        //    HeaderCell.CssClass = "HeaderStyle";
        //    HeaderRow.Cells.Add(HeaderCell);

        //    //Adding Merging Coloumn
        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "Total";
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderCell.ColumnSpan = 2; 
        //    HeaderCell.CssClass = "HeaderStyle";
        //    HeaderRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "Status";
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderCell.RowSpan = 2;
        //    HeaderCell.CssClass = "HeaderStyle";
        //    HeaderRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "A";
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderCell.RowSpan = 2;
        //    HeaderCell.CssClass = "HeaderStyle";
        //    HeaderRow.Cells.Add(HeaderCell);

        //    //Adding the Row at the 0th position (first row) in the Grid
        //    SPGrid.Controls[0].Controls.AddAt(0, HeaderRow);
        //}
    }
}