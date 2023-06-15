using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class fm_bookingsanad : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            txbookingno.CssClass = "ro";
            arr.Add(new cArrayList("@qry_cd", "SalesJob"));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingComboToSp(ref cbsalesman, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
            bll.vBindingFieldValueToCombo(ref cbdoctype, "doctyp");
            bll.vBindingFieldValueToCombo(ref cbdoctype, "doc_typ");
            dtbooking.Text = Request.Cookies["waz_dt"].Value.ToString();
            dtbooking.CssClass = "makeitreadonly";
            lbperiod.Text =  DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(),"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year.ToString();  //bll.vLookUp("select dbo.fn_getperiodname()");
        }
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        if (txbookno.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Booking Number can not empty!','Booking No','warning');", true);
            return;
        }
        double dStart; double dEnd;
        if (!double.TryParse(txstartno.Text, out dStart))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Start Number must numeric','Start No','warning');", true);
            return;
        }
        if (!double.TryParse(txendno.Text, out dEnd))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('End Number must numeric','End No','warning');", true);
            return;
        }
        if (dEnd < dStart)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Start can not bigger than End','Wrong book number','warning');", true);
            return;
        }
        string sSanad = bll.vLookUp("select dbo.fn_getbookingsanad(" + txstartno.Text +  "," + txendno.Text + ",'"+cbdoctype.SelectedValue.ToString()+"')");
        if (sSanad != "ok")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('" + sSanad + "','Sanad Booking','warning');", true);
            return;
        }
        if (!upl.HasFile)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Received book must be attached!','Evidence Attached','warning');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@book_dt", DateTime.ParseExact(dtbooking.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@salesman_cd", cbsalesman.SelectedValue.ToString()));
        arr.Add(new cArrayList("@start_no", txstartno.Text));
        arr.Add(new cArrayList("@end_no", txendno.Text));
        arr.Add(new cArrayList("@doc_typ", cbdoctype.SelectedValue.ToString()));
        arr.Add(new cArrayList("@filename", upl.FileName));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@batch_no", txbookno.Text));
        string sBookNo = string.Empty;
        bll.vInsertBookingSanad(arr, ref sBookNo);
        txbookingno.Text = sBookNo;
        upl.SaveAs(bll.vLookUp("select dbo.fn_getcontrolparameter('image_path')") + upl.FileName);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(),"al","sweetAlert('Booking sanad has been created','Ref Code. : "+ sBookNo + "','success');",true);
        dtbooking.CssClass = "ro";
        txbookingno.CssClass = "ro";
        txendno.CssClass = "ro";
        txstartno.CssClass = "ro";
        cbsalesman.CssClass = "ro";
        btsave.CssClass = "divhid";
        btprint.CssClass = "button2 print";
        cbdoctype.CssClass = "ro";
        
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_bookingsanad.aspx");
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=boksan');", true);
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "popupwindow('lookup_book.aspx');", true);
    }
    protected void btrefresh_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        SqlDataReader rs = null;
        arr.Add(new cArrayList("@book_no", hdbookno.Value.ToString()));
        bll.vGetBookingSanad(arr, ref rs);
        while (rs.Read())
        {
            txbookingno.Text = hdbookno.Value.ToString();
            txendno.Text = rs["end_no"].ToString();
            txstartno.Text = rs["start_no"].ToString();
            dtbooking.Text = Convert.ToDateTime(rs["book_dt"]).ToString("d/M/yyyy");
            cbsalesman.SelectedValue = rs["salesman_cd"].ToString();
            cbdoctype.SelectedValue = rs["doc_typ"].ToString();
            txendno.CssClass = "ro";
            txstartno.CssClass = "ro";
            dtbooking.CssClass = "ro";
            cbsalesman.CssClass = "ro";
            dtbooking.CssClass = "ro";
            cbdoctype.CssClass = "ro";
        } rs.Close();
    }
}