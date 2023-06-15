using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

public class cbll2
{
    cdal dal = new cdal();

    public void vInsertMstLocationSalesman(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_location_salesman_ins", arr);
    }
    public List<tacc_cndn> lAccCndn (List<cArrayList>arr)
    {
        SqlDataReader rs = null;
        List<tacc_cndn> _temp = new List<tacc_cndn>();
        dal.vGetRecordsetSP("sp_tacc_cndn_get", ref rs, arr);
        while (rs.Read())
        {
            _temp.Add(new tacc_cndn
            {
                cndnAdj_sta_id = rs["cndnadj_sta_id"].ToString(),
                cndntype = rs["cndntype"].ToString(),
                cndn_cd = rs["cndn_cd"].ToString(),
                cndn_dt = Convert.ToDateTime(rs["cndn_dt"]),
                createdby = rs["createdby"].ToString(),
                cust_cd = rs["cust_cd"].ToString(),
                deleted = Convert.ToBoolean(rs["deleted"]),
                post_dt = Convert.ToDateTime(rs["post_dt"]),
                reasn_cd = rs["reasn_cd"].ToString(),
                refho_no = rs["refho_no"].ToString(),
                remark = rs["remark"].ToString(),
                salespointcd = rs["salespointcd"].ToString(),
                tax_cd = rs["tax_cd"].ToString(),
                totamtCN = Convert.ToDouble(rs["totamtCN"]),
                totamtDN = Convert.ToDouble(rs["totamtDN"]),
                vatamt = Convert.ToDouble(rs["vatamt"]),
                vatincluded = Convert.ToBoolean(rs["vatincluded"]),
                Amount = Convert.ToDouble(rs["Amount"])
            });
        }
        rs.Close();

        return _temp;
    }
    public List<navision_cndn> lNavisionCndn(List<cArrayList> arr)
    {
        SqlDataReader rs = null;
        List<navision_cndn> _temp = new List<navision_cndn>();
        dal.vGetRecordsetSP("sp_navisionfeed_cndn", ref rs, arr);
        while (rs.Read())
        {
            _temp.Add(new navision_cndn
            {
                AccountNo = rs["AccountNo"].ToString(),
                AccountType = rs["AccountType"].ToString(),
                CreditAmount = Convert.ToDouble(rs["CreditAmount"]),
                DebitAmount = Convert.ToDouble(rs["DebitAmount"]),
                DocumentNo = rs["DOcumentNo"].ToString(),
                EntryNo = Convert.ToInt16(rs["EntryNo"]),
                EntryType = rs["Entry_Type"].ToString(),
                InvoiceNo = rs["invoiceNo"].ToString(),
                Period = rs["period_cd"].ToString(),
                PostingDate = Convert.ToDateTime(rs["PostingDate"]),
                Reason = rs["Reason"].ToString(),
                Remarks = rs["Remarks"].ToString(),
                Salesman = rs["Salesman"].ToString(),
                SalesPoint = rs["Salespoint"].ToString(),
                VAT = Convert.ToDouble(rs["VAT"]),
                Customer_Type = rs["Customer_Type"].ToString(),
                Promotion_Type = rs["Promotion_Type"].ToString(),
                Amount = Convert.ToDouble( rs["Amount"]),
                VATAmount = Convert.ToDouble(rs["VatAmount"])
               
            });
        }
        rs.Close();
        return _temp;
    }
    public List<trps_group> lGetRpsGroup(List<cArrayList> arr)
    {
        SqlDataReader rs = null;
        List<trps_group> _temp = new List<trps_group>();
        dal.vGetRecordsetSP("sp_trps_group_get", ref rs, arr);
        while (rs.Read())
        {
            _temp.Add(new trps_group
            {
                day_cd = Convert.ToInt32(rs["day_cd"]),
                day_nm = rs["day_nm"].ToString(),
                district_cd = rs["district_cd"].ToString(),
                district_nm = rs["district_nm"].ToString(),
                grouprps_cd = rs["grouprps_cd"].ToString(),
                grouprps_nm = rs["grouprps_nm"].ToString(),
                emp_cd = rs["emp_cd"].ToString(),
                emp_nm = rs["emp_nm"].ToString()
            });
        }
        rs.Close();
        return _temp;
    }
    public void vInsertRpsGroup(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_trps_group_ins", arr);
    }
    public void vInsertMstCndnCustomer(List<cArrayList> arr, ref string cndnumber)
    {
        dal.vExecuteSP("sp_tmst_cndncustomer_ins", arr, "@cndn_no", ref cndnumber);
    }
    public void vSearchMstCustomerByInvoice(ref SqlDataReader rs, List<cArrayList> arr)
    {
        dal.vGetRecordsetSP("sp_tmst_customer_searchbyinvoice", ref rs, arr);
    }
    public void vInsertBankDepositInfo(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tbankdeposit_info_ins", arr);
    }
    public List<trps_district> lRpsDistrict(List<cArrayList> arr)
    {
        SqlDataReader rs = null;
        List<trps_district> _temp = new List<trps_district>();
        dal.vGetRecordsetSP("sp_trps_district_getbyempday", ref rs, arr);
        while (rs.Read())
        {
            _temp.Add(new trps_district
            {
                day_cd = Convert.ToInt32(rs["day_cd"]),
                district_cd = rs["district_cd"].ToString(),
                district_desc = rs["district_desc"].ToString(),
                emp_cd = rs["emp_cd"].ToString()
            });
        }
        rs.Close();
        return _temp;
    }
    public void vInsertRpsDistrict(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_trps_district_ins", arr);
    }
    public List<navision_cashout> lnavisionCashout(List<cArrayList> arr)
    {
        List<navision_cashout> _temp = new List<navision_cashout>();
        SqlDataReader rs = null;
        dal.vGetRecordsetSP("sp_navisionfeed_cashout", ref rs, arr);
        while (rs.Read())
        {
            _temp.Add(new navision_cashout
            {
                Account_Type = rs["account_type"].ToString(),
                Document_no = rs["document_no"].ToString(),
                Entry_no = Convert.ToInt32(rs["entry_no"]),
                Entry_Type = rs["entry_type"].ToString(),
                Period = rs["period"].ToString(),
                Posting_Date = Convert.ToDateTime(rs["posting_date"]),
                Salesman = rs["salesman"].ToString(),
                Salespoint = rs["salespoint"].ToString(),
                Account_no = rs["Account_no"].ToString(),
                amount = Convert.ToDouble(rs["amount"]),
                credit = Convert.ToDouble(rs["credit"]),
                debit = Convert.ToDouble(rs["debit"]),
                invoice_no = rs["invoice_no"].ToString(),
                posting_date = Convert.ToDateTime(rs["posting_date"]),
                reference_no = rs["reference_no"].ToString(),
                vat_rate = Convert.ToDouble(rs["vat_rate"]),
                vendor = Convert.ToInt32(rs["vendor"]),
                vendor_no = rs["vendor_no"].ToString(),
                Department = rs["Department"].ToString(),
                pic = rs["pic"].ToString()
            });
        }
        rs.Close();
        return _temp;
    }
    public List<navision_salesman> lNavisionSalesman()
    {
        List<navision_salesman> _temp = new List<navision_salesman>();
        SqlDataReader rs = null;
        dal.vGetRecordsetSP("sp_navisionfeed_salesman", ref rs);
        while (rs.Read())
        {
            _temp.Add(new navision_salesman
            {
                entry_type = rs["entry_typ"].ToString(),
                Job_Title = rs["job_title"].ToString(),
                Name = rs["emp_nm"].ToString(),
                No = rs["emp_cd"].ToString(),
                salespoint = rs["salespoint"].ToString()
            });
        }
        rs.Close();
        return _temp;
    }
    public List<trps_dtl> lRpsDetail(List<cArrayList> arr)
    {
        List<trps_dtl> _temp = new List<trps_dtl>();
        SqlDataReader rs = null;
        dal.vGetRecordsetSP("sp_trps_dtl_getbysalesmanday", ref rs, arr);
        while (rs.Read())
        {
            _temp.Add(new trps_dtl
            {
                cust_cd = rs["cust_cd"].ToString(),
                cust_nm = rs["cust_nm"].ToString(),
                day_cd = Convert.ToInt32(rs["day_cd"]),
                emp_cd = rs["emp_cd"].ToString(),
                payment_term = rs["payment_term"].ToString(),
                credit_limit = Convert.ToDouble(rs["credit_limit"])
            });
        }
        rs.Close();
        return _temp;
    }
    public List<navisionfeed_customer> lNavisionFeedCustomer()
    {
        SqlDataReader rs = null;
        List<navisionfeed_customer> _temp = new List<navisionfeed_customer>();
        dal.vGetRecordsetSP("sp_navisionfeed_customer", ref rs);
        while (rs.Read())
        {
            _temp.Add(new navisionfeed_customer
            {
                Channel = rs["otlcd"].ToString(),
                CustAddress = rs["addr"].ToString(),
                CustCity = rs["city_cd"].ToString(),
                CustDistrict = rs["district_cd"].ToString(),
                CustName = rs["cust_nm"].ToString(),
                CustNo = rs["cust_cd"].ToString(),
                Salesman = rs["salesman_cd"].ToString(),
                SalesPoint = rs["salespointcd"].ToString(),
                TermofPayment = rs["payment_term"].ToString()
            });
        }
        rs.Close();
        return _temp;
    }
    public void vInsertAccCndnDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tacc_cndndtl_ins", arr);
    }
    public List<tacc_cndndtl> lGetAccCndnDtlByCustomer(List<cArrayList> arr)
    {
        SqlDataReader rs = null;
        List<tacc_cndndtl> _temp = new List<tacc_cndndtl>();
        dal.vGetRecordsetSP("sp_tdosales_invoice_getbycustomer", ref rs, arr);
        while (rs.Read())
        {
            _temp.Add(new tacc_cndndtl
            {
                cndntype = string.Empty,
                cndn_cd = string.Empty,
                inv_balance = Convert.ToDouble(rs["balance"]),
                inv_cnamount = 0,
                inv_dnamount = 0,
                inv_dt = Convert.ToDateTime(rs["inv_dt"]),
                inv_no = rs["inv_no"].ToString(),
                inv_noamount = Convert.ToDouble(rs["balance"]),
                manual_no = rs["manual_no"].ToString(),
                inv_sta_id_nm = rs["inv_sta_id_nm"].ToString(),
                totamt = Convert.ToDouble(rs["totamt"]),
                salespointcd = rs["salespointcd"].ToString(),

            });

        }
        rs.Close();
        return _temp;
    }
    public void vSearchMstCustomerHaveBalance(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_customer_getbyhavebalance", ref rs, arr);
    }
    public List<tacc_cndndtl> lAccCndnDtl(List<cArrayList> arr)
    {
        SqlDataReader rs = null;
        List<tacc_cndndtl> _temp = new List<tacc_cndndtl>();
        dal.vGetRecordsetSP("sp_tacc_cndndtl_get", ref rs, arr);
        while (rs.Read())
        {
            _temp.Add(new tacc_cndndtl
            {
                inv_no = rs["inv_no"].ToString(),
                salespointcd = rs["salespointcd"].ToString(),
                inv_balance = Convert.ToDouble(rs["inv_balance"])
            });
        }
        rs.Close();
        return _temp;
    }
    public List<navisionfeed_cashin> lNavisionFeedCashin(List<cArrayList> arr)
    {
        List<navisionfeed_cashin> _temp = new List<navisionfeed_cashin>();
        SqlDataReader rs = null;
        dal.vGetRecordsetSP("sp_navision_feed_cashin", ref rs, arr);
        while (rs.Read())
        {
            _temp.Add(new navisionfeed_cashin
            {
                account_no = rs["account_no"].ToString(),
                account_type = rs["account_type"].ToString(),
                balance = Convert.ToDouble(rs["balance"]),
                credit = Convert.ToDouble(rs["credit"]),
                debit = Convert.ToDouble(rs["debit"]),
                document_no = rs["document_no"].ToString(),
                entry_no = Convert.ToInt16(rs["entry_no"]),
                entry_type = rs["entry_type"].ToString(),
                invoice_no = rs["invoice_no"].ToString(),
                period = rs["period"].ToString(),
                posting_date = Convert.ToDateTime(rs["posting_date"]),
                reference_no = rs["reference_no"].ToString(),
                salesman = rs["salesman"].ToString(),
                salespoint = rs["salespoint"].ToString(),
                Cheque = Convert.ToInt32(rs["Cheque"]),
                ChequeNo = rs["ChequeNo"].ToString(),
                clearance = Convert.ToInt32(rs["Clearance"])
            });
        }
        rs.Close();
        return _temp;
    }
    public void vInsertAddLoseVanDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_taddlose_van_dtl_ins", arr);
    }
    public void vInsertMstAddLoseVan(List<cArrayList> arr, ref string _addlosenumber)
    {
        dal.vExecuteSP("sp_tmst_addlose_van_ins", arr, "@addlose_cd", ref _addlosenumber);
    }
    public void vInsertAccMstSupplier(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tacc_mst_supplier_ins", arr);
    }
    public List<tapprovalpattern> lApprovalPattern(List<cArrayList> arr)
    {
        SqlDataReader rs = null;
        List<tapprovalpattern> _temp = new List<tapprovalpattern>();
        dal.vGetRecordsetSP("sp_tapprovalpattern_getbytype", ref rs, arr);
        while (rs.Read())
        {
            _temp.Add(new tapprovalpattern
            {
                whatsapp_no = rs["whatsapp_no"].ToString(),
                emp_cd = rs["emp_cd"].ToString(),
                email = rs["email"].ToString()
            });
        }
        rs.Close();
        return _temp;
    }
    public List<navision_stock> lNavisionStock(List<cArrayList> arr)
    {
        DateTime _dt = System.DateTime.Today;
        foreach (cArrayList _l in arr)
        {
            if (_l.paramid == "@start_dt")
            {
                _dt = Convert.ToDateTime(_l.ParamValue);
            }
        }
        SqlDataReader rs = null; int _count = 1;
        List<navision_stock> _t = new List<navision_stock>();
        dal.vGetRecordsetSP("sp_navision_feed_stock", ref rs, arr);
        while (rs.Read())
        {
            _t.Add(new navision_stock
            {
                Period = rs["period"].ToString() + ((_dt.Day < 10) ? "0" + _dt.Day.ToString() : _dt.Day.ToString()),
                Bin_Code = rs["Bin_Code"].ToString(),
                Customer_No = (rs["Customer_No"] == DBNull.Value ? string.Empty : rs["Customer_No"].ToString()),
                Description = rs["Description"].ToString(),
                Discount_Amount = (rs["Discount_Amount"] == DBNull.Value ? 0 : Convert.ToDouble(rs["Discount_Amount"])).ToString(),
                Document_No = (rs["Document_No"] == DBNull.Value ? string.Empty : rs["Document_No"].ToString()),
                Entry_No = Convert.ToInt16(rs["Entry_No"]),
                Item_No = rs["Item_No"].ToString(),
                Location_Code = rs["Location_Code"].ToString(),
                Posting_Date = Convert.ToDateTime(rs["Posting_Date"]).ToString("yyyy-MM-dd'T'HH:mm:ss"),
                Purchase_Amount = Convert.ToDouble(rs["Purchase_Amount"]).ToString(),
                Purchase_Price = Convert.ToDouble(rs["Purchase_Price"]).ToString(),
                Quantity_In = Convert.ToDouble(rs["Quantity_In"]).ToString(),
                Quantity_Out = Convert.ToDouble(rs["Quantity_Out"]).ToString(),
                Reference_No = (rs["Reference_No"] == DBNull.Value ? string.Empty : rs["Reference_No"].ToString()),
                Salesman = (rs["Salesman"] == DBNull.Value ? string.Empty : rs["Salesman"].ToString()),
                Sales_Amount = (rs["Sales_Amount"] == DBNull.Value ? 0 : Convert.ToDouble(rs["Sales_Amount"])).ToString(),
                Sales_Price = (rs["Sales_Price"] == DBNull.Value ? 0 : Convert.ToDouble(rs["Sales_Price"])).ToString(),
                Total_Amount = (rs["Total_Amount"] == DBNull.Value ? 0 : Convert.ToDouble(rs["Total_Amount"])).ToString(),
                Transaction_Type = Convert.ToInt16(rs["Transaction_type"]).ToString(),
                Unit_of_Measure_Code = rs["Unit_of_Measure_Code"].ToString(),
                VAT = (rs["VAT"] == DBNull.Value ? 0 : Convert.ToDouble(rs["VAT"])).ToString(),
                VAT_Amount = (rs["VAT_Amount"] == DBNull.Value ? 0 : Convert.ToDouble(rs["VAT_Amount"])).ToString(),
                Vendor_No = (rs["Vendor_No"] == DBNull.Value ? "V000" : rs["Vendor_No"].ToString()),
                Discount_Logistic = (rs["Discount_Logistic"] == DBNull.Value ? 0 : Convert.ToDouble(rs["Discount_Logistic"])).ToString()
            });
            _count++;
        }
        return _t;
    }
    public void vBatchDiscountLogistic(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batchdiscountlogistic", arr);
    }
    public List<tcustomer_address> lGetCustomerAddress(List<cArrayList> arr)
    {
        SqlDataReader rs = null;
        List<tcustomer_address> _temp = new List<tcustomer_address>();
        dal.vGetRecordsetSP("sp_tcustomer_address_get", ref rs, arr);
        while (rs.Read())
        {
            _temp.Add(new tcustomer_address
            {
                address1 = rs["address1"].ToString(),
                address2 = rs["address2"].ToString(),
                address3 = rs["address3"].ToString(),
                addr_typ = rs["addr_typ"].ToString(),
                addr_typ_nm = rs["addr_typ_nm"].ToString(),
                city = rs["city"].ToString(),
                country = rs["country"].ToString(),
                cust_cd = rs["cust_cd"].ToString(),
                district_cd = rs["district_cd"].ToString(),
                isdefault = Convert.ToBoolean(rs["isdefault"]),
                salespointcd = rs["salespointcd"].ToString(),
                zipcode = rs["zipcode"].ToString()
            });
        }
        rs.Close();
        return _temp;
    }
    public string fn_getcontrolparameter(string _p)
    {
        SqlDataReader rs = null; string _temp = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@parm_nm", _p));
        dal.vGetRecordsetSP("sp_tcontrol_parameter_get", ref rs, arr);
        while (rs.Read())
        {
            _temp = rs["parm_valu"].ToString();
        }
        return _temp;
    }
    public void vInsertCndnSalesman(List<cArrayList> arr, ref string _cndncode)
    {
        dal.vExecuteSP("sp_tcndn_salesman_ins", arr, "@cndn_cd", ref _cndncode);
    }
    public void vInsertRepackingdtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_trepacking_dtl_ins", arr);
    }
    public void vInsertMstRepacking(List<cArrayList> arr, ref string _repackcode)
    {
        dal.vExecuteSP("sp_tmst_repacking_ins", arr, "@repack_cd", ref _repackcode);
    }
    public void vInsertBomDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tbom_dtl_ins", arr);
    }
    public void vInsertMstBom(List<cArrayList> arr, ref string _bom_cd)
    {
        dal.vExecuteSP("sp_tmst_bom_ins", arr, "@bom_cd", ref _bom_cd);
    }
    public void vSearchDistrict(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_location_searchdistrict", ref rs, arr);
    }
    public void vBatchDiscountPercentage(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batchdiscountpercentage", arr);
    }
    public void vSearchMstLocation(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_location_search", ref rs, arr);
    }
    public List<tuser_profiles_approval> luserprofilegetbyapproval(List<cArrayList> arr)
    {
        SqlDataReader rs = null;
        List<tuser_profiles_approval> _temp = new List<tuser_profiles_approval>();
        dal.vGetRecordsetSP("sp_tuser_profile_getbymapqry", ref rs, arr);
        while (rs.Read())
        {
            _temp.Add(new tuser_profiles_approval
            {
                qry_cd = rs["qry_cd"].ToString(),
                whatsapp_no = rs["whatsapp_no"].ToString(),
                email = rs["email"].ToString(),
                emp_cd = rs["emp_cd"].ToString()
            });
        }
        rs.Close();
        return _temp;
    }
    public void vUpdateCustomerAddressByCust(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_customer_updbycust", arr);
    }
    public void vGetMstCustomerByCode(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_customer_getbycode", ref rs, arr);
    }
    public void vInsertCashregisterClosingAdvance(List<cArrayList> arr, ref string _chclosing)
    {
        dal.vExecuteSP("sp_tcashregister_advance_closing_ins", arr, "@chclosingno", ref _chclosing);
    }
    public double fn_gettotalfooter(ref GridView grd, string _name_of_label)
    {
        double _temp = 0;
        foreach (GridViewRow _row in grd.Rows)
        {
            if (_row.RowType == DataControlRowType.DataRow)
            {
                Label lbqty = (Label)_row.FindControl(_name_of_label);
                _temp += Convert.ToDouble(lbqty.Text);
            }
        }

        return _temp;
    }
    public void vInsertDosalesinvoiceDriver(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdosalesinvoice_driver_ins", arr);
    }
    public void vInsertDosalesInvoiceReceived(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdosalesinvoice_received_ins", arr);
    }
    public void vInsertContractDiscountOtlcd(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcontractdiscount_otlcd_ins", arr);
    }
    public void vInsertContractDiscountCusgrcd(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcontractdiscount_cusgrcd_ins", arr);
    }
    public void vInsertContractDiscountCustomer(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcontractdiscount_customer_ins", arr);
    }
    public void vSearchMstCustomerAll(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_customer_searchall", ref rs, arr);
    }
    public void vInsertMstContractDiscount(List<cArrayList> arr, ref string _contract_cd)
    {
        dal.vExecuteSP("sp_tmst_contractdiscount_ins", arr, "@contractdiscount_cd", ref _contract_cd);
    }
    public void vInsertDosalesBlDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdosales_bl_dtl_ins", arr);
    }

    public void vInsertDosalesBl(List<cArrayList> arr, ref string _blno)
    {
        dal.vExecuteSP("sp_tdosales_bl_ins", arr, "@bl_no", ref _blno);
    }
    public void vBatchSalesreturnTab(List<cArrayList> arr, ref string _returno)
    {
        dal.vExecuteSP("sp_ttab_salesreturn_ins3", arr, "@retur_no", ref _returno);
    }
}