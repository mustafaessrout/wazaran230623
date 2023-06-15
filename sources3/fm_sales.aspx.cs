using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.IO;
public partial class fm_sales : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingComboToSp(ref cbbranch, "sp_tmst_salespoint_get", "salespointcd", "salespoint_desc");
            cbbranch.SelectedValue = Request.Cookies["sp"].Value.ToString();
            bll.vBindingComboToSp(ref cbProd_cdFr, "sp_tmst_product_get4", "ID", "ProdName");
            bll.vBindingComboToSp(ref cbProd_cdTo, "sp_tmst_product_get4", "ID", "ProdName");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@report", "SALESEX"));
            bll.vDelsoasalesman1(arr);
        }
    }
    protected void grdsl_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbsalesman_cd = (Label)grdsl.Rows[e.RowIndex].FindControl("lbsalesman_cd");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@salespointcd", cbbranch.SelectedValue.ToString()));
        arr.Add(new cArrayList("@salesman_cd", lbsalesman_cd.Text));
        arr.Add(new cArrayList("@report", "SALESEX"));
        bll.vDelsoasalesman(arr); arr.Clear(); 
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@salespointcd", cbbranch.SelectedValue.ToString()));
        arr.Add(new cArrayList("@report", "SALESEX"));
        bll.vBindingGridToSp(ref grdsl, "sp_trp_bysalesman_get", arr);
    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        if (txsalesman.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Salesman Error','Please select Salesman','warning');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        string x = bll.vLookUp("select max(seq_no) from trp_bysalesman where report='SOA' and usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='" + cbbranch.SelectedValue.ToString() + "'");
        if (x == "" || x == null)
        {
            x = "0";
        }

        arr.Add(new cArrayList("@salespointcd", cbbranch.SelectedValue.ToString()));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@salesman_nm", bll.vLookUp("select emp_nm from tmst_employee where emp_cd='" + hdsalesman_cd.Value + "'")));
        arr.Add(new cArrayList("@salesman_cd", hdsalesman_cd.Value));
        arr.Add(new cArrayList("@report", "SALESEX"));
        arr.Add(new cArrayList("@seq_no", Convert.ToInt16(x) + 1));
        bll.vInsertsoasalesman(arr);
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@salespointcd", cbbranch.SelectedValue.ToString()));
        arr.Add(new cArrayList("@report", "SALESEX"));
        bll.vBindingGridToSp(ref grdsl, "sp_trp_bysalesman_get", arr);
        txsalesman.Text = "";
        hdsalesman_cd.Value = null;
        txsalesman.Focus();
        // bll.vBindingComboToSp(ref cbcity, "sp_tmst_location_get3", "loc_cd", "loc_nm", arr, "ALL");
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        List<string> lcust = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string scust = string.Empty;
        System.Data.SqlClient.SqlDataReader rs = null;
        //arr.Add(new cArrayList("@cust_cd", prefixText));
        arr.Add(new cArrayList("@salesman_cd", prefixText));
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        bll.vSearchSalesman(arr, ref rs);
        //bll.vSearchMstCustomer(arr, ref rs);
        while (rs.Read())
        {
            scust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["emp_cd"].ToString() + "-" + rs["emp_nm"].ToString(), rs["emp_cd"].ToString());
            lcust.Add(scust);
        }
        rs.Close();

        return (lcust.ToArray());
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        if(grdsl.Rows.Count.ToString()=="0")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Salesman Error','Please add at least one Salesman','warning');", true);
            return;
        }
        if (dtstart.Text=="" || dtend.Text=="")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('DATE Error','Please Insert  Date','warning');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        DateTime dtpayp1 = DateTime.ParseExact(dtstart.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        string dt1 = dtpayp1.Year.ToString() + "-" + dtpayp1.Month.ToString("00") + "-" + dtpayp1.Day.ToString("00");
        DateTime dtpayp2 = DateTime.ParseExact(dtend.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        string dt2 = dtpayp2.Year.ToString() + "-" + dtpayp2.Month.ToString("00") + "-" + dtpayp2.Day.ToString("00");
        arr.Add(new cArrayList("@startdate", dt1));
        arr.Add(new cArrayList("@enddate", dt2));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@report", "SALESEX"));
        arr.Add(new cArrayList("@salespointcd", cbbranch.SelectedValue.ToString()));
        arr.Add(new cArrayList("@cbProd_cdFr", cbProd_cdFr.SelectedValue.ToString()));
        arr.Add(new cArrayList("@cbProd_cdTo", cbProd_cdTo.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_sales", arr);
        grd.Visible = true;
        Response.ClearContent();
        //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //Response.AddHeader("content-disposition", "attachment;filename=GridView.xls");
        Response.AddHeader("content-disposition", "attachment; filename=Salesreport"+dt2+".xlsx");
        Response.ContentType = "application/vnd.ms-excel";
        System.IO.StringWriter sw = new System.IO.StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        htw.WriteLine("<b><u><font size='5'> Sales Report from " + dtstart.Text + " to " + dtend.Text + " </font></u></b>");

        grd.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
        //DataTable dt = new DataTable("GridView_Data");
        //foreach (TableCell cell in grd.HeaderRow.Cells)
        //{
        //    dt.Columns.Add(cell.Text);
        //}
        //foreach (GridViewRow row in grd.Rows)
        //{
        //    dt.Rows.Add();
        //    for (int i = 0; i < row.Cells.Count; i++)
        //    {
        //        dt.Rows[dt.Rows.Count - 1][i] = row.Cells[i].Text;
        //    }
        //}
        //using (XLWorkbook wb = new XLWorkbook())
        //{
        //    wb.Worksheets.Add(dt);

        //    Response.Clear();
        //    Response.Buffer = true;
        //    Response.Charset = "";
        //    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //    Response.AddHeader("content-disposition", "attachment;filename=GridView.xlsx");
        //    using (MemoryStream MyMemoryStream = new MemoryStream())
        //    {
        //        wb.SaveAs(MyMemoryStream);
        //        MyMemoryStream.WriteTo(Response.OutputStream);
        //        Response.Flush();
        //        Response.End();
        //    }
        //}
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void btpdf_Click(object sender, EventArgs e)
    {       
        //row.Cells(1).Style.Add("background-color", "#C2D69B")
        List<cArrayList> arr = new List<cArrayList>();
        DateTime dtpayp1 = DateTime.ParseExact(dtstart.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        string dt1 = dtpayp1.Year.ToString() + "-" + dtpayp1.Month.ToString("00") + "-" + dtpayp1.Day.ToString("00");
        DateTime dtpayp2 = DateTime.ParseExact(dtend.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        string dt2 = dtpayp2.Year.ToString() + "-" + dtpayp2.Month.ToString("00") + "-" + dtpayp2.Day.ToString("00");
        arr.Add(new cArrayList("@startdate", dt1));
        arr.Add(new cArrayList("@enddate", dt2));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@report", "SALESEX"));
        arr.Add(new cArrayList("@salespointcd", cbbranch.SelectedValue.ToString()));
        arr.Add(new cArrayList("@cbProd_cdFr", cbProd_cdFr.SelectedValue.ToString()));
        arr.Add(new cArrayList("@cbProd_cdTo", cbProd_cdTo.SelectedValue.ToString()));        
        //grd.Visible = true;
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition","attachment;filename=GridViewExport.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        hw.WriteLine("<b><u><font size='5'> Sales Report from " + dtstart.Text + " to " + dtend.Text + " </font></u></b>");
        grd.AllowPaging = false;
        bll.vBindingGridToSp(ref grd, "sp_sales", arr);
        grd.RenderControl(hw);        
        StringReader sr = new StringReader(sw.ToString());
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();  
    }
}