using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_navisionfeed_cashout : System.Web.UI.Page
{
    cbll bll = new cbll();
    cbll2 bll2 = new cbll2();
    cbllHO bllnav = new cbllHO();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            dtfeed.Text = Request.Cookies["waz_dt"].Value;
        }
    }

    protected void btsearch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        DateTime _dt = System.DateTime.ParseExact(dtfeed.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        arr.Add(new cArrayList("@expense_dt", _dt));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        List<navision_cashout> _navision_cashout = bll2.lnavisionCashout(arr);
        grd.DataSource = _navision_cashout;
        grd.DataBind();
        cd.v_showcontrol(btfeed);
        Session["navision_cashout"] = _navision_cashout;
    }

    protected void btfeed_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        List<navision_cashout> _navision_cashout = (List<navision_cashout>)Session["navision_cashout"];
        foreach(navision_cashout _t in _navision_cashout)
        {
            arr.Clear();
            arr.Add(new cArrayList("@Period", _t.Period));
            arr.Add(new cArrayList("@EntryNo", _t.Entry_no));   
            arr.Add(new cArrayList("@PostingDate", _t.posting_date));   
            arr.Add(new cArrayList("@EntryType",_t.Entry_Type));
            arr.Add(new cArrayList("@DocumentNo",_t.Document_no));
            arr.Add(new cArrayList("@SalesPoint", _t.Salespoint));
            arr.Add(new cArrayList("@Salesman",_t.Salesman));   
            arr.Add(new cArrayList("@AccountType",_t.Account_Type));
            arr.Add(new cArrayList("@AccountNo", _t.Account_no));
            arr.Add(new cArrayList("@InvoiceNo", _t.invoice_no));   
            arr.Add(new cArrayList("@RefNo",_t.reference_no));
            arr.Add(new cArrayList("@Vendor", _t.vendor_no));
            arr.Add(new cArrayList("@VendorNo", _t.vendor_no));
            arr.Add(new cArrayList("@VAT", _t.vat_rate));
            arr.Add(new cArrayList("@DebitAmount", _t.debit));
            arr.Add(new cArrayList("@CreditAmount", _t.credit));
            arr.Add(new cArrayList("@Amount", _t.amount));
            arr.Add(new cArrayList("@DepartmentCode", _t.Department));
            bllnav.vInsertWazaranCashout(arr);  
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), 
            "sweetAlert('Feed cashout navision has been succeeded !','Cashout Feed','success');", true);
    }
}