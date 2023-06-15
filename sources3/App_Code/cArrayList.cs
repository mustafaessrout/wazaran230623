using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class cArrayList
{
    protected string _paramid;
    protected object _paramvalue;

    public string paramid
    { set { _paramid = value; } get { return (_paramid); } }

    public object ParamValue
    { set { _paramvalue = value; } get { return (_paramvalue); } }

    public cArrayList(string sParamID, object oParamValue)
    {
        paramid = sParamID;
        ParamValue = oParamValue;
    }
}

public class tacc_cndn
{
    public string cndn_cd { set; get; }
    public string remark { set; get; }
    public string reasn_cd { set; get; }
    public string cndntype { set; get; }
    public double totamtCN { set; get; }
    public double totamtDN { set; get; }
    public string cust_cd { set; get; }
    public double vatamt { set; get; }
    public bool vatincluded { set; get; }
    public string refho_no { set; get; }
    public string salespointcd { set; get; }
    public DateTime post_dt { set; get; }
    public DateTime cndn_dt { set; get; }
    public string createdby { set; get; }
    public bool deleted { set; get; }
    public string cndnAdj_sta_id { set; get; }
    public string tax_cd { set; get; }
    public double Amount { set; get; }

}
public class navision_cndn
{
    public string Period { set; get; }
    public int EntryNo { set; get; }
    public DateTime PostingDate { set; get; }
    public string EntryType { set; get; }
    public string DocumentNo { set; get; }
    public string SalesPoint { set; get; }
    public string Salesman { set; get; }
    public string AccountType { set; get; }
    public string AccountNo { set; get; }
    public string InvoiceNo { set; get; }
    public string Remarks { set; get; }
    public string Reason { set; get; }
    public double DebitAmount { set; get; }
    public double CreditAmount { set; get; }
    public double VAT { set; get; }
    public string Customer_Type { set; get; }
    public string Promotion_Type { set; get; }
    public double Amount { set; get; }  
    public double VATAmount { set; get; }   
}


public class trps_group
{
    public string grouprps_cd { set; get; }
    public int day_cd { set; get; }
    public string district_cd { set; get; }
    public string day_nm { set; get; }
    public string district_nm { set; get; }
    public string grouprps_nm { set; get; }
    public string emp_cd { set; get; }
    public string emp_nm { set; get; }  
}
public class trps_district
{
    public string emp_cd { set; get; }
    public string district_cd { set; get; }
    public int day_cd { set; get; }
    public string district_desc { set; get; }
}
public class navision_cashout
{
    public string Period { set; get; }
    public int Entry_no { set; get; }
    public DateTime Posting_Date { set; get; }
    public string Entry_Type { set; get; }
    public string Document_no { set; get; }
    public string Salespoint { set; get; }
    public string Salesman { set; get; }
    public string pic { set; get; }
    public string Department { set; get; }
    public string Account_Type { set; get; }
    public string Account_no { set; get; }
    public string invoice_no { set; get; }
    public string reference_no { set; get; }
    public int vendor { set; get; }
    public string vendor_no { set; get; }
    public double vat_rate { set; get; }
    public double debit { set; get; }
    public double credit { set; get; }
    public double amount { set; get; }
    public DateTime posting_date { set; get; }


}
public class trps_dtl
{
    public string emp_cd { set; get; }
    public int day_cd { set; get; }
    public string cust_cd { set; get; }
    public string cust_nm { set; get; }
    public string payment_term { set; get; }
    public double credit_limit { set; get; }

}
public class tacc_cndndtl
{
    public string cndn_cd { set; get; }
    public string cndntype { set; get; }
    public string refho_no { set; get; }
    public string salespointcd { set; get; }
    public string inv_no { set; get; }
    public double inv_noamount { set; get; }
    public double inv_cnamount { set; get; }
    public double inv_dnamount { set; get; }
    public double inv_balance { set; get; }
    public DateTime inv_dt { set; get; }
    public string manual_no { set; get; }
    public string salesman_desc { set; get; }
    public string inv_sta_id_nm { set; get; }
    public string salesman_cd { set; get; }
    public double totalvat { set; get; }
    public double totamt { set; get; }
    public string tax_cd { set; get; }
    public double tax_formula { set; get; }
    public double based_amt { set; get; }
    public double vat_rate { set; get; }
}

public class taddlose_van_dtl
{
    public string item_cd { set; get; }
    public double qty { set; get; }
    public double qty_ctn { set; get; }
    public double qty_pcs { set; get; }
    public string uom { set; get; }
    public double vat_rat { set; get; }
    public double unitprice { set; get; }
    public double vat { set; get; }
    public double unitprice_ctn { set; get; }
    public double unitprice_pcs { set; get; }
    public string item_nm { set; get; }
}
public class tacc_mst_supplier
{
    public string No { set; get; }
    public string Name { set; get; }
    public string ETag { set; get; }
}
public class navision_stock
{
    public string Period { get; set; }
    public int Entry_No { set; get; }
    public string Transaction_Type { set; get; }
    public string Posting_Date { set; get; }
    public string Document_No { set; get; }
    public string Reference_No { set; get; }
    public string Salesman { set; get; }
    public string Item_No { set; get; }
    public string Customer_No { set; get; }
    public string Vendor_No { set; get; }
    public string Description { set; get; }
    public string Location_Code { set; get; }
    public string Bin_Code { set; get; }
    public string Quantity_In { set; get; }
    public string Quantity_Out { set; get; }
    public string Unit_of_Measure_Code { set; get; }
    public string Sales_Price { set; get; }
    public string Sales_Amount { set; get; }
    public string Purchase_Price { set; get; }
    public string Purchase_Amount { set; get; }
    public string Discount_Amount { set; get; }
    public string VAT { set; get; }
    public string VAT_Amount { set; get; }
    public string Total_Amount { set; get; }
    public string Discount_Logistic { set; get; }
}
public class tmst_salestarget
{
    public string salespointcd { set; get; }
}
public class tbom_dtl
{
    public string item_cd { set; get; }
    public string item_nm { set; get; }
    public double qty { set; get; }
    public string uom { set; get; }
}
public class tlog_titem_processingdtl
{
    public string item_cd { set; get; }
    public string item_nm { set; get; }
    public string uom { set; get; }
    public decimal unit_qty { set; get; }
    public decimal in_qty { set; get; }
    public decimal out_qty { set; get; }
    public string whs_cd { set; get; }
    public string bin_cd { set; get; }
    public decimal stock_real { set; get; }
    public decimal qty_tally { set; get; }
    public decimal stock_booking { set; get; }
    public decimal stockavl { set; get; }

}

public class tcustomer_address
{
    public string cust_cd { set; get; }
    public string addr_typ { set; get; }
    public string addr_typ_nm { set; get; }
    public string salespointcd { set; get; }
    public string address1 { set; get; }
    public string address2 { set; get; }
    public string address3 { set; get; }
    public string city { set; get; }
    public string country { set; get; }
    public string zipcode { set; get; }
    public bool isdefault { set; get; }
    public string district_cd { set; get; }
}
public class tuser_profiles_approval
{
    public string emp_cd { set; get; }
    public string whatsapp_no { set; get; }
    public string email { set; get; }
    public string qry_cd { set; get; }

}
public class tsalestarget_ho
{
    public string salespointcd { set; get; }
    public string prod_cd { set; get; }
    public string prod_nm { set; get; }
    public string uom { set; get; }
    public double qty { set; get; }
    public string period_cd { set; get; }
}
public class tdosalesinvoice_driver
{
    public string emp_cd { set; get; }
    public string emp_nm { set; get; }
    public string uom { set; get; }
    public double qty { set; get; }
    public DateTime driver_receipt_dt { set; get; }

}
public class tcontractdiscount_formula
{
    public string contractdiscount_cd { set; get; }
    public double pct { set; get; }
    public double qty { set; get; }
}
public class tcontractdiscount_otlcd
{
    public string otlcd { set; get; }
    public string otlcd_nm { set; get; }
}
public class tcontractdiscount_customer
{
    public string cust_cd { set; get; }
    public string cust_nm { set; get; }
}
public class tcontractdiscount_cusgrd
{
    public string cusgrcd { set; get; }
    public string csgrc_nm { set; get; }
}
public class rptcustomercontrolroom
{
    public string cust_cd { set; get; }
    public string cust_nm { set; get; }
    public string addr { set; get; }
    public string addr_google { set; get; }
    public string salesman_nm { set; get; }
    public string salesman_mobile_no { set; get; }
    public string latitude { set; get; }
    public string longitude { set; get; }
}
public class tsalesman_gasoline_consume
{
    public string gas_cd { set; get; }
    public DateTime fill_dt { set; get; }
    public decimal kilometer { set; get; }
    public decimal liter { set; get; }
    public string salesman_cd { set; get; }
    public string salesman_nm { set; get; }
    public string tab_gas_cd { set; get; }
    public string salespointcd { set; get; }
}
public class tsalesman_balance_deposit
{
    public string salesman_cd { set; get; }
    public string salesman_nm { set; get; }
    public string deposit_cd { set; get; }
    public string acc_no { set; get; }
    public string salesdep_sta_id { set; get; }
    public decimal amt { set; get; }
    public DateTime deposit_dt { set; get; }
    public string salespointcd { set; get; }
    public string salesdep_sta_nm { set; get; }

}
public class tgoodreceipt_dtl
{
    public string receipt_no { set; get; }
    public string salespointcd { set; get; }
    public decimal qty { set; get; }
    public decimal qty_received { set; get; }
    public string stock_typ { set; get; }
    public string whs_cd { set; get; }
    public string bin_cd { set; get; }
    public DateTime expire_date { set; get; }
    public DateTime prod_date { set; get; }
    public string uom { set; get; }
    public string item_cd { set; get; }
    public string item_nm { set; get; }
    public string size { set; get; }
    public string branded_nm { set; get; }
    public decimal qty2 { set; get; }
    public string uom2 { set; get; }
    public string lot_no { set; get; }

}
public class tmst_goodreceipt
{
    public string receipt_no { set; get; }
    public string do_no { set; get; }
}
public class rptstockmovement
{
    public string item_cd { set; get; }
    public string item_nm { set; get; }
    public string size { set; get; }
    public string branded_nm { set; get; }
    public decimal opening { set; get; }
    public decimal stockin { set; get; }
    public decimal depoout { set; get; }
}
public class tinternal_transfer_dtl
{
    public string trf_no { set; get; }
    public string salespointcd { set; get; }
    public string item_cd { set; get; }
    public double qty { set; get; }
    public string uom { set; get; }
    public string uom2 { set; get; }
    public double qty2 { set; get; }
    public double stock_qty { set; get; }
    public double unitprice { set; get; }
    public string item_shortname { set; get; }
    public string size { set; get; }
    public string branded_nm { set; get; }
    public double qty_conv { set; get; }
    public double qty_ctn { set; get; }
    public double qty_pcs { set; get; }
    public double stockqty_conv { set; get; }
    public double stockqty_ctn { set; get; }
    public double stockqty_pcs { set; get; }
    public string seqID { set; get; }
    public string item_nm { set; get; }
    public DateTime exp_dt { set; get; }
    public DateTime prod_dt { set; get; }

}
//public class nav_header
//{
//    public string DO_No { set; get; }
//    public DateTime Order_Date { set; get; }
//    public DateTime Posting_Date { set; get; }
//    public DateTime Shipment_Date { set; get; }
//    public string DO_Branch_Code { set; get; }
//    public string DO_Branch_Name { set; get; }
//    public string DO_Status { set; get; }
//}
public class nav_detail
{
    public string Do_No { set; get; }
    public string DO_Line_No { set; get; }
    public string DO_Item_1 { set; get; }
    public string DO_Item_2 { set; get; }
    public string DO_Qty { set; get; }
    public string DO_Unit_Price { set; get; }
    public string DO_Uom { set; get; }
    public string DO_Description { set; get; }
}
public class rptStock_all
{
    public string usr_id { set; get; }
    public string SalesPointCD { set; get; }
    public string MonthCD { set; get; }
    public string siteDest { set; get; }
    public string whs_cd { set; get; }
    public DateTime dtFrom { set; get; }
    public DateTime dtTo { set; get; }
    public DateTime dtStart { set; get; }
    public string item_cdFr { set; get; }
    public string item_cdTo { set; get; }
    public string prod_cdFr { set; get; }
    public string prod_cdTo { set; get; }
    public string Bin_cd_fr { set; get; }
}
public class rptstocksample
{
    public string sample_cd { set; get; }
    public DateTime sample_dt { set; get; }
    public string sample_nm { set; get; }
    public string cust_cd { set; get; }
    public string emp_nm { set; get; }
    public string responsible_by { set; get; }
    public string remark { set; get; }
    public string sampleoutby { set; get; }
    public string ref_no { set; get; }
    public string salespointcd { set; get; }
    public string salespoint_nm { set; get; }
    public string item_cd { set; get; }
    public string item_nm { set; get; }
    public string size { set; get; }
    public string branded_nm { set; get; }
    public decimal qty { set; get; }
    public string uom { set; get; }

}
public class rptstockindirect
{
    public string stockin_no { set; get; }
    public DateTime stockin_dt { set; get; }
    public string remark { set; get; }
    public string refno { set; get; }
    public string createdby { set; get; }
    public string stockin_sta_id { set; get; }
    public string salespointcd { set; get; }
    public string po_no { set; get; }
    public string vendor_cd { set; get; }
    public string file_nm { set; get; }
    public string do_no { set; get; }
    public string item_cd { set; get; }
    public string item_nm { set; get; }
    public string item_shortname { set; get; }
    public string item_arabic { set; get; }
    public string size { set; get; }
    public string packing { set; get; }
    public string branded_nm { set; get; }
    public string item_nm_en { set; get; }
    public decimal qty { set; get; }
    public string whs_cd { set; get; }
    public string bin_cd { set; get; }
    public string whs_nm { set; get; }
    public string bin_nm { set; get; }
}
public class tstockindirect_dtl
{
    public string item_cd { set; get; }
    public string uom { set; get; }
    public decimal qty { set; get; }
    public decimal susut { set; get; }
    public decimal tallysheet { set; get; }
    public string whs_cd { set; get; }
    public string bin_cd { set; get; }
    public DateTime exp_dt { set; get; }
    public DateTime prod_dt { set; get; }
    public decimal stock_avl { set; get; }
    public string item_nm { set; get; }
    public decimal adjustvalue { set; get; }
    public decimal unitprice { set; get; }
    public string whs_nm { set; get; }
    public string bin_nm { set; get; }
}
public class tmessages
{

    public string instanceId { set; get; }
    public List<message> messages { set; get; }


}
public class message
{
    public string id { set; get; }
    public string body { set; get; }

    public bool fromMe { set; get; }
    public int self { set; get; }
    public int isForwarded { set; get; }
    public string author { set; get; }
    public Int32 time { set; get; }
    public string chatId { set; get; }
    public int messageNumber { set; get; }
    public string type { set; get; }
    public string senderName { set; get; }
    public string caption { set; get; }
    public string quotedMsgBody { set; get; }
    public string quotedMsgId { set; get; }
    public string quotedMsgType { set; get; }
    public string metadata { set; get; }
    public int? ack { set; get; }
    public string chatName { set; get; }
}
public class tstocksample_dtl
{
    public string sample_cd { set; get; }
    public string item_cd { set; get; }
    public string uom { set; get; }
    public string size { set; get; }
    public string item_nm { set; get; }
    public decimal qty { set; get; }
    public string whs_cd { set; get; }
    public string bin_cd { set; get; }
    public string whs_nm { set; get; }
    public string bin_nm { set; get; }
    public decimal stockavl { set; get; }
    public decimal qtysent { set; get; }

}

public class tgdn_header_nav
{
    public string DO_No { set; get; }
    public DateTime Order_Date { set; get; }
    public DateTime Posting_Date { set; get; }
    public DateTime Shipment_Date { set; get; }
    public string DO_Branch_Code { set; get; }
    public string DO_Branch_Name { set; get; }
    public string DO_Status { set; get; }
}
public class tapprovalpattern
{
    public string emp_cd { set; get; }
    public string mobile_no { set; get; }
    public string whatsapp_no { set; get; }
    public string email { set; get; }
}

public class do_header
{
    public string do_no { get; set; }
    public DateTime? order_date { get; set; }
    public DateTime? posting_date { get; set; }
    public DateTime? shipment_date { get; set; }
    public string do_branch_code { get; set; }
    public string do_branch_name { get; set; }
    public string do_status { get; set; }
}

public class do_detail
{
    public string do_no { get; set; }
    public DateTime? shipment_date { get; set; }
    public string do_line_no { get; set; }
    public string do_item_1 { get; set; }
    public string do_item_2 { get; set; }
    public double do_qty { get; set; }
    public double do_unit_price { get; set; }
    public string do_uom { get; set; }
    public string do_description { get; set; }
    public string item_nm { get; set; }
    public string item_cd { get; set; }
    public string size { get; set; }
    public string uom { get; set; }
    public string qty_conv { get; set; }
    public double qty_ctn { get; set; }
    public double qty_pcs { get; set; }
    public string whs_cd { get; set; }
    public string bin_cd { get; set; }
    public DateTime? expire_date { get; set; }
    public DateTime? prod_date { get; set; }

}

