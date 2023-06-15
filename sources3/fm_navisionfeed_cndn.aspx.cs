using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_navisionfeed_cndn : System.Web.UI.Page
{
    cbll bll = new cbll();
    cbllHO bllnav = new cbllHO();
    cbll2 bll2 = new cbll2();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cd.v_disablecontrol(dtend);
            dtstart.Text = Request.Cookies["waz_dt"].Value;
        }
    }

    protected void btsearch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@start_dt", System.DateTime.ParseExact(dtstart.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        //bll.vBindingGridToSp(ref grd, "sp_navisionfeed_cndn", arr);
        List<navision_cndn> _navision_cndn = bll2.lNavisionCndn(arr);
        grd.DataSource = _navision_cndn;
        grd.DataBind();
        Session["navision_cndn"] = _navision_cndn;
    }

    protected void btprintsummary_Click(object sender, EventArgs e)
    {

    }

    protected void btfeed_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        List<navision_cndn> _navision_cndn = (List<navision_cndn>)Session["navision_cndn"];
        foreach(navision_cndn _t in _navision_cndn)
        {
            arr.Clear();
            arr.Add(new cArrayList("@Period", _t.Period));
            arr.Add(new cArrayList("@EntryNo", _t.EntryNo));
            arr.Add(new cArrayList("@PostingDate", _t.PostingDate));
            arr.Add(new cArrayList("@EntryType", _t.EntryType));
            arr.Add(new cArrayList("@DocumentNo", _t.DocumentNo));
            arr.Add(new cArrayList("@SalesPoint", _t.SalesPoint));    
            arr.Add(new cArrayList("@Salesman", _t.Salesman));
            arr.Add(new cArrayList("@AccountType", _t.AccountType));
            arr.Add(new cArrayList("@AccountNo", _t.AccountNo));
            arr.Add(new cArrayList("@InvoiceNo",_t.InvoiceNo));
            arr.Add(new cArrayList("@Remarks", _t.Remarks));
            arr.Add(new cArrayList("@Reason", _t.Reason));  
            arr.Add(new cArrayList("@DebitAmount", _t.DebitAmount));
            arr.Add(new cArrayList("@CreditAmount", _t.CreditAmount));
            arr.Add(new cArrayList("@VAT", _t.VAT));
            arr.Add(new cArrayList("@Amount",_t.DebitAmount - _t.CreditAmount));
            arr.Add(new cArrayList("@CustomerType", _t.Customer_Type));
            arr.Add(new cArrayList("@PromotionType", _t.Promotion_Type));
            arr.Add(new cArrayList("@VATAmount", _t.VATAmount));
            
            bllnav.vInsertWazaranCNDN(arr);


        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), 
            "sweetAlert('Navision Feed CNDN has been succeeded !','Nav Feed','success');", true);
        
    }
}