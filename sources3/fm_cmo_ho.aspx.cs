using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_cmo_ho : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string waz_dt;
            waz_dt = DateTime.Now.ToString("d/M/yyyy");
            DateTime dtwaz_dt = DateTime.ParseExact(waz_dt, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            waz_dt = dtwaz_dt.ToString("yyyyMM");
            bll.vBindingComboToSp(ref cbMonthCD, "sp_tblTRYearMonth_get", "period", "ymtName");
            cbMonthCD.SelectedValue = waz_dt;
            cbMonthCD_SelectedIndexChanged(sender, e);
            txdays.Text = "11";
            showResult.Attributes.Add("style", "display:none");
            btnew.Visible = false;
            btgenerate.Text = "Check";
            lbcmono.Text = "NEW";
            
        }
    }

    protected void cbMonthCD_SelectedIndexChanged(object sender, EventArgs e)
    {
        DateTimeFormatInfo mfi = new DateTimeFormatInfo();
        var firstDayOfMonth = new DateTime(int.Parse(cbMonthCD.SelectedValue.ToString().Substring(0,4)), int.Parse(cbMonthCD.SelectedValue.ToString().Substring(4,2)), 1).AddMonths(-3);
        var lastDayOfMonth = firstDayOfMonth.AddMonths(3).AddDays(-1);
        txfrom.Text = firstDayOfMonth.ToString("d/M/yyyy");
        txto.Text = lastDayOfMonth.ToString("d/M/yyyy");
        ch1.Text = "STD (Sales To Distributor) : " + mfi.GetMonthName(firstDayOfMonth.AddMonths(2).Month).ToString().Substring(0,3);
        ch2.Text = "STT (Actual Sales) : " + mfi.GetMonthName(firstDayOfMonth.AddMonths(2).Month).ToString().Substring(0, 3);
        ch3.Text = "Sales Plan : " + mfi.GetMonthName(firstDayOfMonth.AddMonths(3).Month).ToString().Substring(0, 3);
        ch4.Text = "OPB (Opening Balance Stock) : " + mfi.GetMonthName(firstDayOfMonth.AddMonths(2).Month).ToString().Substring(0, 3);
        ch5.Text = "STT (Actual Sales 3Months) :" + mfi.GetMonthName(firstDayOfMonth.Month).ToString().Substring(0, 3) + " - " + mfi.GetMonthName(firstDayOfMonth.AddMonths(2).Month).ToString().Substring(0, 3);
    }

    protected void btsearch_Click(object sender, EventArgs e)
    {

    }

    protected void cbProduct_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btadd_Click(object sender, EventArgs e)
    {

    }

    //protected void grditem_RowCreated(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.Header)
    //    {
    //        GridView HeaderGrid = (GridView)sender;
    //        GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
    //        TableCell HeaderCell = new TableCell();
    //        HeaderCell.Text = "Department";
    //        HeaderCell.ColumnSpan = 2;
    //        HeaderGridRow.Cells.Add(HeaderCell);

    //        HeaderCell = new TableCell();
    //        HeaderCell.Text = "Employee";
    //        HeaderCell.ColumnSpan = 2;
    //        HeaderGridRow.Cells.Add(HeaderCell);S
    //    }
    //}

    protected void grditem_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void grditem_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }

    protected void grditem_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void grditem_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }

    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_cmo_ho.aspx");
    }

    protected void btgenerate_Click(object sender, EventArgs e)
    {
        string cmo_no = "";
        DateTimeFormatInfo mfi = new DateTimeFormatInfo();
        List<cArrayList> arr = new List<cArrayList>();
        System.Data.SqlClient.SqlDataReader rs = null;
        int dataSTD = 0, dataSalesplan = 0;

        try
        {            
            var lastDayOfMonth = new DateTime(int.Parse(cbMonthCD.SelectedValue.ToString().Substring(0, 4)), int.Parse(cbMonthCD.SelectedValue.ToString().Substring(4, 2)), 1).AddMonths(-1);
            if (btgenerate.Text == "Check")
            {
                arr.Clear();
                dataSTD = int.Parse(bll.vLookUp("select count(distinct std_dt) from tpch_std where month(std_dt)='" + lastDayOfMonth.Month.ToString() + "' and year(std_dt)='" + lastDayOfMonth.Year.ToString() + "'"));
                dataSalesplan = int.Parse(bll.vLookUp("select count(distinct period) from tpch_mst_salesplan where period = '" + cbMonthCD.SelectedValue.ToString() + "'"));

                if (dataSTD < 28)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('STD : " + mfi.GetMonthName(lastDayOfMonth.Month).ToString().Substring(0, 3) + ", Not Yet Entry Completed... ','Entry Monthly STD','warning');", true);
                    return;
                }

                if (dataSalesplan == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Salesplan : " + mfi.GetMonthName(lastDayOfMonth.AddMonths(1).Month).ToString().Substring(0, 3) + ", Not Yet Entry Completed... ','Entry Monthly Sales Plan.','warning');", true);
                    return;
                }
                btgenerate.Text = "Generate";
                showResult.Attributes.Remove("style");
            }
            else if (btgenerate.Text == "Generate")
            {
                arr.Clear();
                arr.Add(new cArrayList("@sys", "cmo"));
                arr.Add(new cArrayList("@sysno", ""));
                bll.vGetDiscountNo(arr, ref rs);
                while (rs.Read())
                {
                    cmo_no = rs["generated"].ToString();
                }
                arr.Clear();
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@cmo_cd", cmo_no));
                arr.Add(new cArrayList("@prevmonth", lastDayOfMonth.Month.ToString()));
                arr.Add(new cArrayList("@prevperiode", lastDayOfMonth.ToString("yyyyMM")));
                arr.Add(new cArrayList("@thisperiode", cbMonthCD.SelectedValue.ToString()));
                arr.Add(new cArrayList("@firstdayofprevmonth", lastDayOfMonth.AddDays(-1).ToString("yyyy-mm-dd")));
                arr.Add(new cArrayList("@firstdayofbuffmonth", lastDayOfMonth.AddMonths(-2).ToString("yyyy-mm-dd")));
                arr.Add(new cArrayList("@lastdayofbuffmonth", lastDayOfMonth.AddMonths(1).AddDays(-1).ToString("yyyy-mm-dd")));
                arr.Add(new cArrayList("@buffer", txdays.Text.ToString()));
                arr.Add(new cArrayList("@pct_east", txeastfactory.Text.ToString()));
                arr.Add(new cArrayList("@pct_west", txwestfactory.Text.ToString()));
                bll.vInsertCMO(arr);
                lbcmono.Text = cmo_no;
                if (lbcmono.Text != "NEW")
                {
                    btgenerate.Text = "Check";
                    showResult.Attributes.Add("style", "display:none");
                    vBindingGrid(cmo_no);
                }
                else
                {
                    btgenerate.Visible = false;
                    btnew.Visible = true;
                }                
            }
        }
        catch (Exception ex)
        {
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : Generate CMO");
        }
        finally
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('CMO Generated (" + cmo_no + ").','Periode : " + cbMonthCD.SelectedValue.ToString() + "','success');", true);
        }        
    }

    void vBindingGrid(string cmo)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cmo_no", cmo));
        bll.vBindingGridToSp(ref grd, "sp_tpch_cmo_info_get", arr);
    }

    protected void btselect_Click(object sender, EventArgs e)
    {
        btnew.Visible = true;
        btgenerate.Visible = false;
        List<cArrayList> arr = new List<cArrayList>();
        System.Data.SqlClient.SqlDataReader rs = null;
        arr.Add(new cArrayList("@cmo_no", hdcmo.Value));
        bll.vGetCMO(ref rs, arr);
        while (rs.Read())
        {
            cbMonthCD.SelectedValue = rs["period"].ToString();
            lbcmono.Text = rs["cmo_cd"].ToString();
            txdays.Text = rs["cmo_buffer"].ToString();
            txeastfactory.Text = rs["cmo_east_pct"].ToString();
            txwestfactory.Text = rs["cmo_west_pct"].ToString();
        }
        rs.Close();
        cbMonthCD_SelectedIndexChanged(sender, e);
        showResult.Attributes.Remove("style");
        vBindingGrid(lbcmono.Text);
    }
}