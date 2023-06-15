using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static iTextSharp.text.pdf.events.IndexEvents;
using WhatsAppApi.Helper;

public partial class fm_navisionfeed_cashin : System.Web.UI.Page
{
    cbll bll = new cbll();
    cbll2 bll2 = new cbll2();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cd.v_disablecontrol(dtend);
            dtstart.Text = Request.Cookies["waz_dt"].Value;
        }
    }

    protected void btfeed_Click(object sender, EventArgs e)
    {
        cbllHO bllnav = new cbllHO();
        List<navisionfeed_cashin> _navisionfeed_cashin = (List<navisionfeed_cashin>)Session["navisionfeed_cashin"];
        List<cArrayList> arr = new List<cArrayList>();
        foreach (navisionfeed_cashin _t in _navisionfeed_cashin)
        {
            //           @Period nvarchar(10),@EntryNo int, @PostingDate datetime, @EntryType nvarchar(30),
            //               @DocumentNo nvarchar(30), @SalesPoint nvarchar(30),
            //@Salesman nvarchar(30), @AccountType nvarchar(30),@AccountNo nvarchar(30), @InvoiceNo nvarchar(30), 
            //               @RefNo nvarchar(30), @Cheque tinyint, @ChequeNo nvarchar(50),
            //@DebitAmount decimal(38, 20), @CreditAmount decimal(38, 20), @Amount decimal(38, 20)
            arr.Clear();
            arr.Add(new cArrayList("@Period", _t.period));
            arr.Add(new cArrayList("@PostingDate", _t.posting_date));
            arr.Add(new cArrayList("@EntryType", _t.entry_type));
            arr.Add(new cArrayList("@EntryNo", _t.entry_no));
            arr.Add(new cArrayList("@DocumentNo", _t.document_no));
            arr.Add(new cArrayList("@SalesPoint", _t.salespoint));
            arr.Add(new cArrayList("@Salesman", _t.salesman));
            arr.Add(new cArrayList("@AccountType", _t.account_type));
            arr.Add(new cArrayList("@AccountNo", _t.account_no));
            arr.Add(new cArrayList("@InvoiceNo", _t.invoice_no));
            arr.Add(new cArrayList("@RefNo", _t.reference_no));
            arr.Add(new cArrayList("@Clearance",_t.clearance));
            arr.Add(new cArrayList("@Cheque",_t.Cheque));
            arr.Add(new cArrayList("@ChequeNo", _t.ChequeNo));
            arr.Add(new cArrayList("@DebitAmount", _t.debit));
            arr.Add(new cArrayList("@CreditAmount", _t.credit));
            arr.Add(new cArrayList("@Amount", _t.balance));
            bllnav.vInsertIntoWazaranCashTransaction(arr);

        }


        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
            "sweetAlert('Data wazaran has been feed !','As Date : "+dtstart.Text+"','success');", true);
    }

    protected void btsearch_Click(object sender, EventArgs e)
    {
        DateTime _date = DateTime.ParseExact(dtstart.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cash_dt", _date));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        List<navisionfeed_cashin> _navision_cashin = bll2.lNavisionFeedCashin(arr);
        grd.DataSource = _navision_cashin;
        grd.DataBind();
        Session["navisionfeed_cashin"] = _navision_cashin;
        //bll.vBindingGridToSp(ref grd, "sp_navision_feed_cashin", arr);
    }

    protected void btprintsummary_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), 
            "openreport('fm_report2.aspx?src=navisioncashin&dt="+dtstart.Text+"');", true);
    }
}