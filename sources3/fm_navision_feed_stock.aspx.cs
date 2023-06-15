using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static Utitlity;

public partial class fm_navision_feed_stock : System.Web.UI.Page
{
    cbll bll = new cbll();
    cbll2 bll2 = new cbll2();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dtstart.Text = Request.Cookies["waz_dt"].Value;
            dtend.Text = string.Empty;
            cd.v_disablecontrol(dtend);
            cd.v_hiddencontrol(btfeed);
        }
    }

    public void vBindingGrid()
    {
        //List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@start_dt", System.DateTime.ParseExact(dtstart.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        //arr.Add(new cArrayList("@end_dt", System.DateTime.ParseExact(dtend.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        //bll.vBindingGridToSp(ref grd, "sp_navision_feed_stock", arr);
        DateTime _start = System.DateTime.ParseExact(dtstart.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime _end = _start; //System.DateTime.ParseExact(dtend.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //string _url = bll2.fn_getcontrolparameter("api_navision") + "Stock?s=" + _start.ToString("M/d/yyyy") + "&e=" + _end.ToString("M/d/yyyy");
        //using (WebClient ws = new WebClient())
        //{
        //    try
        //    {
        //        using (WebClient webClient = new WebClient())
        //        {
        //            //webClient.BaseAddress = StaticItems.EndPoint;
        //            var json = webClient.DownloadString(_url);
        //            List<navision_stock> list = JsonConvert.DeserializeObject<List<navision_stock>>(json);
        //            grd.DataSource = list;
        //            grd.DataBind();
        //            Session["navision_stock"] = list;
        //        }
        //    }
        //    catch (WebException ex)
        //    {
        //        throw ex;
        //    }
        //}
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@start_dt", _start));
        arr.Add(new cArrayList("@end_dt", _end));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        List<navision_stock> _navision_stock = bll2.lNavisionStock(arr);
        grd.DataSource = _navision_stock;
        grd.DataBind();
        //arr.Clear();
        //arr.Add(new cArrayList("@start_dt", _start));
        //bll.vBindingGridToSp(ref grdsumm, "sp_navisionfeed_summarystock", arr);
        Session["navision_stock"] = _navision_stock;
    }

    protected void btsearch_Click(object sender, EventArgs e)
    {
        vBindingGrid();
        cd.v_showcontrol(btfeed);
    }

    protected void btfeed_Click(object sender, EventArgs e)
    {
        cbllHO bllnav = new cbllHO();
        //string Datastring = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@start_dt", System.DateTime.ParseExact(dtstart.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@end_dt", System.DateTime.ParseExact(dtstart.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        List<navision_stock> _navision_stock = bll2.lNavisionStock(arr);

        //string _url = "http://192.168.120.123:6048/INDOMOROC_API/OData/Wazaran_Item_Transaction";
        ////List<navision_stock> _navision_stock = (List<navision_stock>)Session["navision_stock"];
        //using (WebClient ws = new WebClient())
        //{
        //    ws.Headers[HttpRequestHeader.ContentType] = "application/json";
        //    ws.Credentials = new NetworkCredential("NAVAPI", "CCXLFGq+5xIRijJAK/ghkhMJwuScb0/O2f8xHSJ9Wrk=");
        //    Datastring = JsonConvert.SerializeObject(_navision_stock);
        //    ws.UploadString(_url, "POST", Datastring);
        //    System.Threading.Thread.Sleep(2000);
        //}

        foreach (navision_stock _t in _navision_stock)
        {
            arr.Clear();
            arr.Add(new cArrayList("@Period", _t.Period));
            arr.Add(new cArrayList("@EntryNo", _t.Entry_No));
            arr.Add(new cArrayList("@TransactionType", _t.Transaction_Type));
            arr.Add(new cArrayList("@PostingDate", _t.Posting_Date));
            arr.Add(new cArrayList("@DocumentNo", _t.Document_No));
            arr.Add(new cArrayList("@ReferenceNo", _t.Reference_No));
            arr.Add(new cArrayList("@Salesman", _t.Salesman));
            arr.Add(new cArrayList("@ItemNo", _t.Item_No));
            arr.Add(new cArrayList("@CustomerNo", _t.Customer_No));
            arr.Add(new cArrayList("@VendorNo", _t.Vendor_No));
            arr.Add(new cArrayList("@Description", _t.Description));
            arr.Add(new cArrayList("@LocationCode", _t.Location_Code));
            arr.Add(new cArrayList("@BinCode", _t.Bin_Code));
            arr.Add(new cArrayList("@QuantityIn", _t.Quantity_In));
            arr.Add(new cArrayList("@QuantityOut", _t.Quantity_Out));
            arr.Add(new cArrayList("@UnitofMeasureCode", _t.Unit_of_Measure_Code));
            arr.Add(new cArrayList("@SalesPrice", _t.Sales_Price));
            arr.Add(new cArrayList("@SalesAmount", _t.Sales_Amount));
            arr.Add(new cArrayList("@PurchasePrice", _t.Purchase_Price));
            arr.Add(new cArrayList("@PurchaseAmount", _t.Purchase_Amount));
            arr.Add(new cArrayList("@DiscountAmount", _t.Discount_Amount));
            arr.Add(new cArrayList("@VAT", _t.VAT));
            arr.Add(new cArrayList("@VATAmount", _t.VAT_Amount));
            arr.Add(new cArrayList("@TotalAmount", _t.Total_Amount));
            arr.Add(new cArrayList("@LogisticDiscountAmount", _t.Discount_Logistic));
            bllnav.vInsertWazaranItemTransaction(arr);
        }


        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
            "sweetAlert('Data wazaran has been feed !','Successfully','success');", true);
    }

    protected void btprintsummary_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), 
            "openreport('fm_report2.aspx?src=navisionstock&dt="+dtstart.Text+"');", true);
    }
}