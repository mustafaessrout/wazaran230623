using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;

public partial class fm_excel : System.Web.UI.Page
{
    public string pfilename { set; get; }
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    void vSearchGrid(string sType)
    {
        if (!fup.HasFile)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('File excel must selected','File Upload XLS','warning');", true);
            return;
        }
        pfilename = "F"+ Convert.ToString( DateTime.Today.Day)+Convert.ToString(DateTime.Today.Month)+Convert.ToString(DateTime.Today.Year)+ Convert.ToString( DateTime.Now.Hour) + Convert.ToString( DateTime.Now.Minute) + Convert.ToString( DateTime.Now.Second);
        fup.SaveAs(bll.vLookUp("select dbo.fn_getcontrolparameter('image_path')") + @"\" + pfilename );

        //OleDbDataReader rs = null;

        string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=d:\\images\\"+pfilename+";Extended Properties=Excel 12.0";
        // Create the connection object
        OleDbConnection oledbConn = new OleDbConnection(connString);
        //try
        //{ 
        // Open connection
        oledbConn.Open();
        OleDbCommand cmd = new OleDbCommand();
        // Create OleDbCommand object and select data from worksheet Sheet1
        if (sType == "B")
        {
            cmd = new OleDbCommand("SELECT * FROM [customerprice$]", oledbConn);
        }
        else if (sType == "G")
        {
            cmd = new OleDbCommand("SELECT * FROM [adjustmentgroup$] where item_cd <>'' OR item_cd is not null", oledbConn);
        }
        else if (sType == "C")
        {
            cmd = new OleDbCommand("SELECT * FROM [adjustmentcustomer$]", oledbConn);
        }
        OleDbDataAdapter oleda = new OleDbDataAdapter();
        oleda.SelectCommand = cmd;
        DataSet ds = new DataSet();
        oleda.Fill(ds, "customerprice");
        if (sType == "B")
        {
            grd.DataSource = ds.Tables[0].DefaultView;
            grd.DataBind();
            grdcust.Style.Add("display", "none");
            grdgroup.Style.Add("display", "none");
            grd.Style.Add("display", "normal");
        }
        else if (sType == "G")
        {
            grdgroup.DataSource = ds.Tables[0].DefaultView;
            grdgroup.DataBind();
            grdcust.Style.Add("display", "none");
            grdgroup.Style.Add("display", "normal");
            grd.Style.Add("display", "none");
        }
        else if (sType == "C")
        {
            grdcust.DataSource = ds.Tables[0].DefaultView;
            grdcust.DataBind();
            grdcust.Style.Add("display", "normal");
            grdgroup.Style.Add("display", "none");
            grd.Style.Add("display", "none");
        }
        oledbConn.Close();
        oledbConn = null;
    }

    protected void btsearch_Click(object sender, EventArgs e)
    {
        if (!fup.HasFile)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('select XLS file','File not selected','warning');", true);
            return;
        }
        if (cbtype.SelectedValue == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('select type upload','Upload type','warning');", true);
            return;
        }
        vSearchGrid(cbtype.SelectedValue);
    }

    protected void grdgroup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbitemcode = (Label)e.Row.FindControl("lbitemcode");
            Label lbcusttype = (Label)e.Row.FindControl("lbcusttype");
            Label lbcusgrcd = (Label)e.Row.FindControl("lbcusgrcd");
            Label lbunitprice = (Label)e.Row.FindControl("lbunitprice");
            Label lbadjust = (Label)e.Row.FindControl("lbadjust");
            Label lbexpectprice = (Label)e.Row.FindControl("lbexpectprice");

            if (string.IsNullOrEmpty(lbcusgrcd.Text))
            {
               // grd.DeleteRow(e.Row.DataItemIndex);
                return;
                
            }
            // string sCurrentPrice = bll.vLookUp("select dbo.fn_gettotalprice('" + lbitemcode.Text + "','" + lbcusttype.Text + "','" + lbcusgrcd.Text + "')");
            string sCurrentPrice = bll.vLookUp("select unitprice from tcustomertype_price where cust_typ='"+lbcusttype.Text+"' and item_cd='"+lbitemcode.Text+"'");
            if (string.IsNullOrEmpty(sCurrentPrice))
            {
                sCurrentPrice = "0";
            }
            lbunitprice.Text = sCurrentPrice;
            lbadjust.Text = (Convert.ToDecimal(lbexpectprice.Text) - Convert.ToDecimal(lbunitprice.Text)).ToString();
        }
    }

    protected void grdcust_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbbaseprice = (Label)e.Row.FindControl("lbbaseprice");
            Label lbexpectprice = (Label)e.Row.FindControl("lbexpectprice");
            Label lbadjust = (Label)e.Row.FindControl("lbadjust");
            Label lbsalespoint = (Label)e.Row.FindControl("lbsalespoint");
            Label lbitemcode = (Label)e.Row.FindControl("lbitemcode");
            Label lbcustcode = (Label)e.Row.FindControl("lbcustcode");
            string sBasePrice = bll.vLookUp("select dbo.fn_gettotalpricecust('"+lbitemcode.Text+"','"+lbcustcode.Text+"','"+lbsalespoint.Text+"')");
            lbbaseprice.Text = sBasePrice;
            lbadjust.Text = (Convert.ToDecimal(lbexpectprice.Text) - Convert.ToDecimal(sBasePrice)).ToString();
        }
    }

    protected void btexport_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (cbtype.SelectedValue == "B")
        {
            foreach (GridViewRow row in grd.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    Label lbstartdate = (Label)row.FindControl("lbstartdate");
                    if ((Convert.ToDateTime(lbstartdate.Text) - System.DateTime.Today).TotalDays <= 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Start Date can not same or less than today','Check Start Date','warning');", true);
                        return;
                    }
                }
            }
            foreach (GridViewRow row in grd.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    Label lbitemcode = (Label)row.FindControl("lbitemcode");
                    Label lbcusttype = (Label)row.FindControl("lbcusttype");
                    Label lbunitprice = (Label)row.FindControl("lbunitprice");
                    Label lbstartdate = (Label)row.FindControl("lbstartdate");
                    Label lbenddate = (Label)row.FindControl("lbenddate");
                    arr.Clear();
                    arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
                    arr.Add(new cArrayList("@cust_typ", lbcusttype.Text));
                    arr.Add(new cArrayList("@unitprice", lbunitprice.Text));
                    arr.Add(new cArrayList("@start_dt", lbstartdate.Text));
                    arr.Add(new cArrayList("@end_dt", lbenddate.Text));
                    arr.Add(new cArrayList("@booking_dt", System.DateTime.Today));
                    bll.vInsertBookingPrice(arr);
                }
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Booking has been saved!','successfully','success');", true);
        }
        else if (cbtype.SelectedValue == "G")
        {
            foreach (GridViewRow row in grdgroup.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    Label lbstartdate = (Label)row.FindControl("lbstartdate");
                    if ((Convert.ToDateTime(lbstartdate.Text) - System.DateTime.Today).TotalDays <= 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Start Date can not same or less than today','Check Start Date','warning');", true);
                        return;
                    }
                }
            }
            foreach (GridViewRow row in grdgroup.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    Label lbitemcode = (Label)row.FindControl("lbitemcode");
                    Label lbcusgrcd = (Label)row.FindControl("lbcusgrcd");
                    Label lbunitprice = (Label)row.FindControl("lbunitprice");
                    Label lbstartdate = (Label)row.FindControl("lbstartdate");
                    Label lbenddate = (Label)row.FindControl("lbenddate");
                    Label lbexpectprice = (Label)row.FindControl("lbexpectprice");
                    Label lbcusttype = (Label)row.FindControl("lbcusttype");
                    Label lbadjust = (Label)row.FindControl("lbadjust");

                    arr.Clear();
                    arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
                    arr.Add(new cArrayList("@cusgrcd", lbcusgrcd.Text));
                    arr.Add(new cArrayList("@start_dt", lbstartdate.Text));
                    arr.Add(new cArrayList("@end_dt", lbenddate.Text));
                    arr.Add(new cArrayList("@cust_typ", lbcusttype.Text));
                    arr.Add(new cArrayList("@expect_price", lbexpectprice.Text));
                    bll.vInsertAdjustmentPriceGusgrcdBooking(arr);
                }
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Booking group has been saved!','successfully','success');", true);
        }

        else if (cbtype.SelectedValue == "C")
        {
            foreach (GridViewRow row in grdcust.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    Label lbstartdate = (Label)row.FindControl("lbstartdate");
                    if ((Convert.ToDateTime(lbstartdate.Text) - System.DateTime.Today).TotalDays <= 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Start Date can not same or less than today','Check Start Date','warning');", true);
                        return;
                    }
                }
            }
            foreach (GridViewRow row in grdcust.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    Label lbitemcode = (Label)row.FindControl("lbitemcode");
                    Label lbcustcode = (Label)row.FindControl("lbcustcode");
                    Label lbstartdate = (Label)row.FindControl("lbstartdate");
                    Label lbenddate = (Label)row.FindControl("lbenddate");
                    Label lbexpectprice = (Label)row.FindControl("lbexpectprice");
                    Label lbadjust = (Label)row.FindControl("lbadjust");
                    Label lbsalespoint = (Label)row.FindControl("lbsalespoint");
                    arr.Clear();
                    arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
                    arr.Add(new cArrayList("@cust_cd", lbcustcode.Text));
                    arr.Add(new cArrayList("@salespointcd", lbsalespoint.Text));
                    arr.Add(new cArrayList("@expect_price", lbexpectprice.Text));
                    arr.Add(new cArrayList("@start_dt", lbstartdate.Text));
                    arr.Add(new cArrayList("@end_dt", lbenddate.Text));
                    arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value));
                    bll.vInsertAdjusmentPriceCustomerBooking(arr);
                }
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Booking adjustment customer successfully','Booking Adjustment','success');", true);
        }

    }

    protected void btprint_Click(object sender, EventArgs e)
    {
       // ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=bookprice');", true);
    }

    protected void grdgroup_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
}