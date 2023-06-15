using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using WhatsAppApi.Helper;
public class navision_salesman
{
    public string entry_type { set; get; }
    public string No { set; get; }
    public string Name { set; get; }
    public string Job_Title { set; get; }
    public string salespoint { set; get; }
}

public class navisionfeed_customer
{
    public string CustNo { set; get; }
    public string CustName { set; get; }
    public string CustAddress { set; get; }
    public string CustDistrict { set; get; }
    public string CustCity { set; get; }
    public string SalesPoint { set; get; }
    public string Channel { set; get; }
    public string TermofPayment { set; get; }
    public string Salesman { set; get; }
}
public class Root
{
    [JsonProperty("odata.metadata")]
    public string odatametadata { get; set; }
    public List<Value> value { get; set; }
}


public class navisionfeed_cashin
{
    public string period { set; get; }
    public int entry_no { set; get; }
    public DateTime posting_date { set; get; }
    public string entry_type { set; get; }
    public string document_no { set; get; }
    public string salespoint { set; get; }
    public string salesman { set; get; }
    public string account_type { set; get; }    
    public string account_no { set; get; }  
    public string invoice_no { set; get; }  
    public string reference_no { set; get; }    
    public int clearance { set; get; }
    public int Cheque { set; get; }
    public string ChequeNo { set; get; }
    public double debit { set; get; }   
    public double credit { set;get; }
    public double balance { set; get; } 
}
public class Value
{
    public string No { get; set; }
    public string Name { get; set; }
    public string ETag { get; set; }
}

public class Valuesupplier
{
    public string No { get; set; }
    public string Name { get; set; }
    public string ETag { get; set; }
}
public class root_header
{
    [JsonProperty("odata.metadata")]
    public string OdataMetadata { get; set; }
    public List<nav_header> value { get; set; }
}
public class nav_header
{
    public string No { get; set; }
    public DateTime Order_Date { get; set; }
    public DateTime Posting_Date { get; set; }
    public DateTime Shipment_Date { get; set; }
    public string DO_Branch_Code { get; set; }
    public string Reference_No { get; set; }
    public string ETag { get; set; }
    public string Driver_Name { set; get; }
    public bool Canceled { set; get; }
    //public int Canceled { set; get; }
}

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class root_detail
{
    [JsonProperty("odata.metadata")]
    public string OdataMetadata { get; set; }
    public List<nav_detail2> value { get; set; }
}

public class nav_detail2
{
    public string Document_No { get; set; }
    public int Line_No { get; set; }
    public DateTime Posting_Date { get; set; }
    public string DO_Item_No_1 { get; set; }
    public string DO_Item_No_2 { get; set; }
    public string DO_Qty { get; set; }
    public string DO_Unit_Price { get; set; }
    public string DO_UOM { get; set; }
    public string DO_Description { get; set; }
    public string DO_LotNo { get; set; }
    public string DO_ExpDate { get; set; }
    public string ETag { get; set; }
    public string DO_ProdDate { set; get; }

}

