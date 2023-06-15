using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Net.Mail;
using System.IO;
using System.Diagnostics;
using System.ServiceModel;
using System.Net;

public class cbll
{
    cdal dal = new cdal();


    public List<tmst_goodreceipt> lMstGoodReceipt(List<cArrayList> arr)
    {
        SqlDataReader rs = null;
        List<tmst_goodreceipt> _temp = new List<tmst_goodreceipt>();
        dal.vGetRecordsetSP("sp_tmst_goodreceipt_get", ref rs, arr);
        while (rs.Read())
        {
            _temp.Add(new tmst_goodreceipt
            {
                receipt_no = rs["receipt_no"].ToString(),
                do_no = rs["do_no"].ToString()
            });
        }
        rs.Close();
        return _temp;
    }
    public List<rptcustomercontrolroom> lRptCustomerControlRoom(List<cArrayList> arr)
    {
        SqlDataReader rs = null;
        List<rptcustomercontrolroom> _temp = new List<rptcustomercontrolroom>();
        dal.vGetRecordsetSP("rptcustomercontrolroom", ref rs, arr);
        while (rs.Read())
        {
            _temp.Add(new rptcustomercontrolroom
            {
                addr = rs["addr"].ToString(),
                addr_google = String.Empty,
                cust_cd = rs["cust_cd"].ToString(),
                cust_nm = rs["cust_nm"].ToString(),
                salesman_nm = rs["salesman_nm"].ToString(),
                salesman_mobile_no = rs["salesman_mobile_no"].ToString(),
                latitude = rs["latitude"].ToString(),
                longitude = rs["longitude"].ToString()
            });
        }
        rs.Close();
        return _temp;
    }
    public List<tsalesman_gasoline_consume> lGetSalesmanGasoline(List<cArrayList> arr)
    {
        SqlDataReader rs = null;
        List<tsalesman_gasoline_consume> _t = new List<tsalesman_gasoline_consume>();
        dal.vGetRecordsetSP("sp_tsalesman_gasoline_consume_getbyperiod", ref rs, arr);
        while (rs.Read())
        {
            _t.Add(new tsalesman_gasoline_consume
            {
                fill_dt = Convert.ToDateTime(rs["fill_dt"]),
                gas_cd = rs["gas_cd"].ToString(),
                kilometer = Convert.ToDecimal(rs["kilometer"]),
                liter = Convert.ToDecimal(rs["liter"]),
                salesman_cd = rs["salesman_cd"].ToString(),
                salesman_nm = rs["salesman_nm"].ToString(),
                salespointcd = rs["salespointcd"].ToString(),
                tab_gas_cd = rs["tab_gas_cd"].ToString()
            });
        }
        return _t;
    }
    public void vInsertSalesmanGasolineConsume(List<cArrayList> arr, ref string _gascode)
    {
        dal.vExecuteSP("sp_tsalesman_gasoline_consume_ins", arr, "@gas_cd", ref _gascode);
    }
    public List<tsalesman_balance_deposit> lSalesmanBalanceDeposit(List<cArrayList> arr)
    {

        SqlDataReader rs = null;
        List<tsalesman_balance_deposit> _temp = new List<tsalesman_balance_deposit>();
        dal.vGetRecordsetSP("sp_salesman_balance_deposit_get", ref rs, arr);
        while (rs.Read())
        {
            _temp.Add(new tsalesman_balance_deposit
            {
                acc_no = rs["acc_no"].ToString(),
                amt = Convert.ToDecimal(rs["amt"]),
                deposit_cd = rs["deposit_cd"].ToString(),
                deposit_dt = Convert.ToDateTime(rs["deposit_dt"]),
                salesdep_sta_id = rs["salesdep_sta_id"].ToString(),
                salesman_cd = rs["salesman_cd"].ToString(),
                salesman_nm = rs["salesman_nm"].ToString(),
                salespointcd = rs["salespointcd"].ToString(),
                salesdep_sta_nm = rs["salesdep_sta_nm"].ToString()

            });
        }
        rs.Close();
        return _temp;
    }
    public List<tsalesman_balance_deposit> lSalesmanBalanceDepositBySalespoint(List<cArrayList> arr)
    {

        SqlDataReader rs = null;
        List<tsalesman_balance_deposit> _temp = new List<tsalesman_balance_deposit>();
        dal.vGetRecordsetSP("sp_salesman_balance_deposit_getbysalespoint", ref rs, arr);
        while (rs.Read())
        {
            _temp.Add(new tsalesman_balance_deposit
            {
                acc_no = rs["acc_no"].ToString(),
                amt = Convert.ToDecimal(rs["amt"]),
                deposit_cd = rs["deposit_cd"].ToString(),
                deposit_dt = Convert.ToDateTime(rs["deposit_dt"]),
                salesdep_sta_id = rs["salesdep_sta_id"].ToString(),
                salesman_cd = rs["salesman_cd"].ToString(),
                salesman_nm = rs["salesman_nm"].ToString(),
                salespointcd = rs["salespointcd"].ToString()
            });
        }
        rs.Close();
        return _temp;
    }
    public void vInsertCashregisterClosingInfoAdvance(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcashregisterclosing_advance_info_ins", arr);
    }
    public void vInsertCashregisterClosingInfo(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcashregisterclosing_info_ins", arr);
    }
    public void vBatchClosingCashierAdv(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batchclosingcashieradv", arr);
    }
    public void vInserttcashregister_closingAdv(List<cArrayList> arr, ref string schclosingno)
    {
        dal.vExecuteSP("sp_tcashregister_advance_closing_ins", arr, "@chclosingno", ref schclosingno);
    }
    public void vGetCashoutRequest_advance(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tcashout_advance_get", ref rs, arr);
    }
    public void vInsertSalesmanDeposit(List<cArrayList> arr, ref string _depositcode)
    {
        dal.vExecuteSP("sp_tsalesman_balance_deposit_ins", arr, "@deposit_cd", ref _depositcode);
    }
    public void vUpdateWRKCashoutAttribute_advance(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_cashoutattribute_advance_upd", arr);
    }
    public string vSendFile(string _phone, string _filename)
    {
        string _msg = string.Empty;
        string URI = sGetControlParameter("wazaran_api") + "Whatsapp/SendFile";
        string myParameters = "phone=" + _phone + "&filename=" + _filename;
        try
        {
            using (WebClient wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                string HtmlResult = wc.UploadString(URI, myParameters);
                _msg = HtmlResult;
            }
        }
        catch (Exception ex)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@phone_no", _phone));
            arr.Add(new cArrayList("@wa_text", _filename));
            vHandledError(ref ex, "Send Whatsapp");
            vInsertWhatsappError(arr);
            vHandledError(ref ex, "Send File Whatsapp");
            _msg = ex.Message.ToString();
        }
        return _msg;
    }
    public void vInsertWhatsappError(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twhatsapp_error_ins", arr);
    }
    public string vSendWhatsapp(string _phone, string _text)
    {
        string _msg = string.Empty;
        string URI = sGetControlParameter("wazaran_api") + "Whatsapp/SendMessage";
        string myParameters = "phone=" + _phone + "&text=" + _text;

        try
        {
            using (WebClient wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                string HtmlResult = wc.UploadString(URI, myParameters);
                //return HtmlResult;
                _msg = HtmlResult;
            }
        }
        catch (Exception ex)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@phone_no", _phone));
            arr.Add(new cArrayList("@wa_text", _text));
            vHandledError(ref ex, "Send Whatsapp");
            //vInsertWhatsappError(arr);
            _msg = ex.Message.ToString();
        }
        return _msg;
    }
    public void vInsertApprovalDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tapproval_dtl_ins", arr);
    }
    public void vInsertCashoutAttribute_advance(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcashout_attribute_advance_ins", arr);
    }
    public void vUpdateCashoutRequest_advance(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcashout_advance_upd", arr);
    }
    public void vInsertCashoutRequestHTE_advance(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcashout_request_hte_ins", arr);
    }
    public void vInsertCashoutRequest_advance_Car(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcashout_request_car_advance_info_ins", arr);
    }
    public void vInsertCashoutRequestTax_advance(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcashoutrequest_tax_advance_ins", arr);
    }
    public void vInsertCashoutRequest_advance(List<cArrayList> arr, ref string sCashout)
    {
        dal.vExecuteSP("sp_tcashout_advance_ins", arr, "@cashout_cd", ref sCashout);
    }
    public void vInsertWRKCashoutAttribute_advance(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_cashoutattribute_advance_ins", arr);
    }
    public void vDelWRKCashoutAttribute_advance(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_cashoutattribute_advance_del", arr);
    }
    public void vUpdateCashRegister_advance(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcashregister_advance_upd", arr);
    }
    public List<rptStock_all> lRptStockAll(List<cArrayList> arr)
    {
        SqlDataReader rs = null;
        List<rptStock_all> _temp = new List<rptStock_all>();
        dal.vGetRecordsetSP("sp_rptStock_all", ref rs, arr);
        while (rs.Read())
        {
            _temp.Add(new rptStock_all
            {
                Bin_cd_fr = String.Empty, //rs["bin_cd_fr"].ToString(),
                dtFrom = Convert.ToDateTime(rs["dtFrom"]),
                dtStart = Convert.ToDateTime(rs["dtStart"]),
                dtTo = Convert.ToDateTime(rs["dtTo"]),
                item_cdFr = rs["item_cdFr"].ToString(),
                item_cdTo = rs["item_cdTo"].ToString(),
                MonthCD = rs["MOnthCD"].ToString(),
                prod_cdFr = rs["prod_cdFr"].ToString(),
                prod_cdTo = rs["prod_cdTo"].ToString(),
                SalesPointCD = rs["salespointcd"].ToString(),
                //siteDest = rs["siteDest"].ToString(),
                usr_id = String.Empty,
                whs_cd = rs["whs_cd"].ToString()

            });
        }
        rs.Close();
        return _temp;
    }
    public List<rptstocksample> rptstocksample(List<cArrayList> arr)
    {
        SqlDataReader rs = null;
        List<rptstocksample> _rptstocksample = new List<rptstocksample>();
        dal.vGetRecordsetSP("rptstocksample", ref rs, arr);
        while (rs.Read())
        {
            _rptstocksample.Add(new rptstocksample
            {
                cust_cd = rs["cust_cd"].ToString(),
                emp_nm = rs["emp_nm"].ToString(),
                ref_no = rs["ref_no"].ToString(),
                remark = rs["remark"].ToString(),
                salespointcd = rs["salespointcd"].ToString(),
                responsible_by = rs["responsible_by"].ToString(),
                salespoint_nm = rs["salespoint_nm"].ToString(),
                sampleoutby = rs["sampleoutby"].ToString(),
                sample_cd = rs["sample_cd"].ToString(),
                sample_dt = Convert.ToDateTime(rs["sample_dt"]),
                sample_nm = rs["sample_nm"].ToString(),
                branded_nm = rs["branded_nm"].ToString(),
                item_cd = rs["item_cd"].ToString(),
                item_nm = rs["item_nm"].ToString(),
                qty = Convert.ToDecimal(rs["qty"]),
                size = rs["size"].ToString(),
                uom = rs["uom"].ToString()
            });
        }
        rs.Close();
        return _rptstocksample;
    }
    public List<rptstockindirect> rptstockindirect(List<cArrayList> arr)
    {
        SqlDataReader rs = null;
        List<rptstockindirect> _rptstockindirect = new List<rptstockindirect>();
        dal.vGetRecordsetSP("sp_rpt_stockindirect", ref rs, arr);
        while (rs.Read())
        {
            _rptstockindirect.Add(new rptstockindirect
            {
                bin_cd = rs["bin_cd"].ToString(),
                bin_nm = rs["bin_nm"].ToString(),
                branded_nm = rs["branded_nm"].ToString(),
                createdby = rs["createdby"].ToString(),
                //do_no = rs["do_no"].ToString(),
                //file_nm = rs["file_nm"].ToString(),
                item_arabic = rs["item_arabic"].ToString(),
                item_cd = rs["item_cd"].ToString(),
                item_nm = rs["item_nm"].ToString(),
                item_nm_en = rs["item_nm_en"].ToString(),
                item_shortname = rs["item_shortname"].ToString(),
                packing = rs["packing"].ToString(),
                po_no = rs["po_no"].ToString(),
                qty = Convert.ToDecimal(rs["qty"]),
                refno = rs["refno"].ToString(),
                remark = rs["remark"].ToString(),
                salespointcd = rs["salespointcd"].ToString(),
                size = rs["size"].ToString(),
                stockin_dt = Convert.ToDateTime(rs["stockin_dt"]),
                stockin_no = rs["stockin_no"].ToString(),
                stockin_sta_id = rs["stockin_sta_id"].ToString(),
                //vendor_cd = rs["vendor_cd"].ToString(),
                whs_cd = rs["whs_cd"].ToString(),
                whs_nm = rs["whs_nm"].ToString()
            });
        }
        rs.Close();
        return _rptstockindirect;
    }
    public void vInsertStockInDirectDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tstockindirect_dtl_ins", arr);
    }
    public void vInsertStockInDirect(List<cArrayList> arr, ref string _stockin_no)
    {
        dal.vExecuteSP("sp_tstockin_direct_ins", arr, "@stockin_no", ref _stockin_no);
    }
    public void vDeleteStockInDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tstockindirect_dtl_del", arr);
    }
    public void vInsertSampleStock(List<cArrayList> arr, ref string _sample_cd)
    {
        dal.vExecuteSP("sp_tstock_sample_ins", arr, "@sample_cd", ref _sample_cd);
    }
    public void vInsertTblStock(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tblstock_ins", arr);
    }
    public void vSearchMstCustomerBySalespoint(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_customer_searcbysalespoint", ref rs, arr);
    }

    public void vInsertWhatsappOutbox(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twhatsapp_outbox_ins", arr);
    }
    public List<tuser_profiles_approval> luserprofilegetbyapproval(string _emp_cd)
    {
        SqlDataReader rs = null;
        List<tuser_profiles_approval> _temp = new List<tuser_profiles_approval>();
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@emp_cd", _emp_cd));
        dal.vGetRecordsetSP("sp_tuser_profile_getbyemployee", ref rs, arr);
        while (rs.Read())
        {
            _temp.Add(new tuser_profiles_approval
            {
                qry_cd = string.Empty,
                whatsapp_no = rs["whatsapp_no"].ToString(),
                email = rs["email"].ToString(),
                emp_cd = rs["emp_cd"].ToString()
            });
        }
        rs.Close();
        return _temp;
    }
    public void vUpdateStockSampleDtlByQtySent(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tstocksample_dtl_updbyqtysent", arr);
    }
    public List<tstocksample_dtl> lstoksampledtl(List<cArrayList> arr)
    {
        List<tstocksample_dtl> _dtl = new List<tstocksample_dtl>();
        SqlDataReader rs = null;
        dal.vGetRecordsetSP("sp_tstocksample_dtl_get", ref rs, arr);
        while (rs.Read())
        {
            _dtl.Add(new tstocksample_dtl
            {
                item_cd = rs["item_cd"].ToString(),
                bin_cd = rs["bin_cd"].ToString(),
                whs_cd = rs["whs_cd"].ToString(),
                uom = rs["uom"].ToString(),
                qty = Convert.ToDecimal(rs["qty"]),
                stockavl = Convert.ToDecimal(rs["stockavl"]),
                qtysent = Convert.ToDecimal(rs["qtysent"]),
                sample_cd = rs["sample_cd"].ToString(),
                item_nm = rs["item_nm"].ToString(),
                whs_nm = rs["whs_nm"].ToString(),
                bin_nm = rs["bin_nm"].ToString(),
                size = rs["size"].ToString()

            });
        }
        rs.Close();
        return _dtl;
    }
    public void vUpdateStockSampleByStatus(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tstock_sample_updbystatus", arr);
    }
    public void vInsertStockSampleDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tstocksample_dtl_ins", arr);
    }
    public void vInsertStockSample(List<cArrayList> arr, ref string _sample_cd)
    {
        dal.vExecuteSP("sp_tstock_sample_ins", arr, "@sample_cd", ref _sample_cd);
    }
    private DataTable PivotTable(DataTable origTable)
    {

        DataTable newTable = new DataTable();
        DataRow dr = null;
        //Add Columns to new Table
        for (int i = 0; i < origTable.Rows.Count; i++)
        {
            newTable.Columns.Add(new DataColumn(origTable.Columns[i].ColumnName, typeof(String)));
        }

        //Execute the Pivot Method
        for (int cols = 0; cols < origTable.Columns.Count; cols++)
        {
            dr = newTable.NewRow();
            for (int rows = 0; rows < origTable.Rows.Count; rows++)
            {
                if (rows < origTable.Columns.Count)
                {
                    dr[0] = origTable.Columns[cols].ColumnName; // Add the Column Name in the first Column
                    dr[rows + 1] = origTable.Rows[rows][cols];
                }
            }
            newTable.Rows.Add(dr); //add the DataRow to the new Table rows collection
        }
        return newTable;
    }
    public void vBindingGridToSpPivot(ref GridView grd, string sSPName, List<cArrayList> arr)
    {
        SqlDataReader rs = null;
        dal.vGetRecordsetSP(sSPName, ref rs, arr);
        DataTable dta = new DataTable();
        dta.Load(rs);
        //grd.DataSource = dta.DefaultView;
        //grd.DataBind();
        DataTable pivotedTable = PivotTable(dta);
        grd.DataSource = pivotedTable;
        grd.DataBind();

    }

    public string sErrorPage(string code)
    {
        string sTemp = "";
        List<cArrayList> arr = new List<cArrayList>();
        SqlDataReader rs = null;
        arr.Add(new cArrayList("@code", code));
        dal.vGetRecordsetSP("sp_tcontrol_parameter_get", ref rs, arr);
        while (rs.Read())
        {
            sTemp = rs["parm_valu"].ToString();
        }
        rs.Close();
        return (sTemp);
    }

    public void vUpdateCNDNRequestByStatus(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tacc_cndn_request_updbystatus", arr);
    }
    public void vBatchwazaran_year2(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batch_wazaran_year2", arr);
    }
    public void vUpdateMstPaymentHOConfirm(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_payment_ho_confirm_upd", arr);
    }
    public void vInsertPaymentDtl1pctDiscHO(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tpayment_dtl1pctDisc_ho_ins", arr);
    }
    public void vInsertPaymentBooking1pctDiscHO(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tpayment_booking1pctDisc_ho_ins", arr);
    }
    public void vInsertPaymentInfoHO(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tpayment_info_ho_ins", arr);
    }
    public void vDeletePaymentWrkSalespoint(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_paymentsalespoint_del", arr);
    }
    public void vDelWrkPaymentInvoiceHO(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_paymentinvoice_ho_del", arr);
    }
    public void vGetDOSalesInvoiceByStatus2(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tdosales_invoice_getbyinvno2", ref rs, arr);
    }
    public void vSearchInvoiceByTypeAll(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tdosales_invoice_searchbytype_all", ref rs, arr);
    }
    public void vInsertMstPaymentHO(List<cArrayList> arr, ref string sPaymentNo)
    {
        dal.vExecuteSP("sp_tmst_payment_ho_ins", arr, "@payment_no", ref sPaymentNo);
    }
    public void vInsertPaymentWrkSalespoint(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_paymentsalespoint_ins", arr);
    }
    public void vUpdateAppStockConfirmation(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_app_stockconfirmation_upd", arr);
    }

    public void vInsertDiscountMaximum(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdiscount_info_ins", arr);
    }
    public void vInsertContractInfo(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcontract_info_ins", arr);
    }
    public void vBatchGivenDiscountVatTab(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batchgivendiscountvat_tab", arr);
    }
    public void vInsertCustomerAddress(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcustomer_address_ins", arr);
    }
    public void vImportTmpSalesOrderXls(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmp_salesorder_import", arr);
    }
    public void vInsTmpSalesOrderXls(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmp_salesorder_xls_ins", arr);
    }
    public void vDelTmpSalesOrderXls(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmp_salesorder_xls_del", arr);
    }
    public void vBatchGivenDiscountVat(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batchgivendiscountvat", arr);
    }
    public void vBatchTblStockCost(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batch_tblstockCost", arr);
    }
    public void vBatchCleanWrkUser(List<cArrayList> arr)
    { dal.vExecuteSP("sp_batchclearwrk_user", arr); }
    public void vUpdateCustomerTransferCustomer(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcustomertransfer_customer_upd", arr);
    }
    public void vInsCustomerTransferCustomer(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcustomertransfer_customer_ins", arr);
    }
    public void vDelCustomerTransferCustomer(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcustomertransfer_customer_del", arr);
    }
    public void vGenAttendanceBranch(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_thrd_attendance_ins", arr);
    }
    public void vUpdateAttendanceDtlAuto(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_thrd_attendance_dtl_auto_upd", arr);
    }
    public void vAutoAttendanceEmployee(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_thrd_attendance_dtl_auto_ins", arr);
    }
    public void vUpdateCashierClosing(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcashregister_closing_upd", arr);
    }
    public void vInsertWrkSalesORderDtl2(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_salesorderdtl_ins2", arr);
    }
    public void vSetupNewBranchIns(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_newsalespoint_ins", arr);
    }

    public void vDeleteEmployeeAccessSalespoint(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tuser_salespoint_del", arr);
    }
    public void vInsertEmployeeAccessSalespoint(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tuser_salespoint_ins", arr);
    }

    public void vReviewTmstIT(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_reviewIT_upd", arr);
    }
    public void vReviewTmstSO(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_reviewSO_upd", arr);
    }

    public void vUpdateTblStockAdjApprove(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tblstockadj_updbystatus", arr);
    }
    public void vUpdateTblTrnStockApprove(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tbltrnstock_updbystatus", arr);
    }
    public void vUpdateStockOpnameDtlAuto(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tstockopname_dtl_auto_upd", arr);
    }
    public void vAutoStockOpname(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tstockopname_dtl_auto_ins", arr);
    }
    public void vUpdateMstContractInvoiceOrder(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcontract_invoice_order_upd", arr);
    }
    public void vUpdateMstContractOrder(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_contract_order_upd", arr);
    }
    public void vInsertContractOrderDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcontract_payment_order_ins", arr);
    }
    public void vInsertMstContractOrder(List<cArrayList> arr, ref string sSoNo)
    {
        dal.vExecuteSP("sp_tmst_contract_order_ins", arr, "@so_cd", ref sSoNo);
    }
    public void vBatchVatDirect(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batchvatdirect", arr);
    }
    public void vInsertDosalesInvoiceFreeFromFreeItemDirect(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdosalesinvoice_free_insfromfreeitemdirect", arr);
    }
    public void vBatchEditDiscountDirect(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batcheditdiscountdirect", arr);
    }
    public void vBatchTakeOrderDiscountDirect(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batchtakeorderdiscountdirect", arr);
    }
    public void vInsertCanvasOrderDiscCashItem(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcanvasorder_disccashitem_ins", arr);
    }
    public void vInsertSalesOrderFreeCashItem(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_salesorderfreecashitem_ins", arr);
    }
    public void vInsertWrkCanvasOrderFreeCashItem(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_canvasorderfreecashitem_ins", arr);
    }
    public void vBatchCanvasOrderDiscountDirect(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batchcanvasorderdiscountdirect", arr);
    }
    public void vAutoInternalTransfer(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tinternal_transfer_dtl_auto_ins", arr);
    }
    public void vBatchSalesmanTransferReturn(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batchsalesmantransferreturn", arr);
    }
    public void vUpdateCustomerTransferByStatus(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcustomer_transfer_updbystatus", arr);
    }
    public void vUpdateFullSalesreturnByStatus(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_Invoice_fullReturn_updbyID", arr);
    }
    public void vUpdateCashregisterClosingAcknowledge(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcashregister_closing_acknowledge", arr);
    }
    public void vDeleteWrkSalesOrderDtlTax(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_salesorderdtl_tax_del", arr);
    }

    public void vGetMstDOAll(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_do_all_get", ref rs, arr);
    }

    public void vUpdateMstPaymentBank(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tbank_deposit_updbypayment", arr);
    }
    public void vInserttcashregisterclosing_dtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcashregister_closing_dtl_ins", arr);
    }
    public void vInsertMstDiscount(List<cArrayList> arr, ref string sDiscNo)
    {
        dal.vExecuteSP("sp_tmst_discount_ins", arr, "@disc_cd", ref sDiscNo);
    }
    public void vDeleteCndnCustomer_tax(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcndncustomer_tax_del", arr);
    }
    public void vInsertCndnCustomer_tax(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcndncustomer_tax_ins", arr);
    }
    public void vDeleteACCcndn_tax(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tacc_cndn_tax_del", arr);
    }
    public void vInsertACCcndn_tax(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tacc_cndn_tax_ins", arr);
    }
    public void vDeleteCashoutRequest_Tax(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcashout_request_tax_del", arr);
    }
    public void vInsertCashoutRequest_Tax(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcashout_request_tax_ins", arr);
    }
    public void vDelDistributionPlanDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tpch_distplan_dtl_del", arr);
    }
    public void vInsertDistributionPlanDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tpch_distplan_dtl_ins", arr);
    }
    public void vInsertDistributionPlan(List<cArrayList> arr, ref string sDistPlanNo)
    {
        dal.vExecuteSP("sp_tpch_distplan_ins", arr, "@pp_cd", ref sDistPlanNo);
    }

    public void vUpdateAppInternalTransfer(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_app_internaltransfer_upd", arr);
    }
    public void vUpdateAppInternalTransferDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_app_internaltransfer_dtl_upd", arr);
    }
    public void vInsertMstTabletProfile(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_ttablet_profile_ins", arr);
    }
    public void vInsertMstUserProfile(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tuser_profile_ins2", arr);
    }
    public void vGetMstTax(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_tax_get", ref rs, arr);
    }
    public void vUpdateMstTax(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_tax_upd", arr);
    }
    public void vInsertMstTax(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_tax_ins", arr);
    }
    public void vUpdateMstSalespoint(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_salespoint_upd", arr);
    }
    public void vInsertMstSalespoint(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_salespoint_ins", arr);
    }
    public void vApprovalCustomerLocation(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmstcustomer_location_upd", arr);
    }
    public void vBatchDiscountSpecialCustomer(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tbatch_specialcustomer", arr);
    }
    public void vInsertPreInvOrder(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tpreinv_dtl_ins_order", arr);
    }
    public void vSearchMstPreInv(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_preinv_search", ref rs, arr);
    }
    public void vGetMstPreInv(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_preinv_get", ref rs, arr);
    }
    public void vInsertMstPreInv(List<cArrayList> arr, ref string sInvNo)
    {
        dal.vExecuteSP("sp_tmst_preinv_ins", arr, "@inv_no", ref sInvNo);
    }
    public void vUpdMstPreInv(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_preinv_upd", arr);
    }
    public void vDeltpreinv_dtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tpreinv_dtl_del", arr);
    }
    public void vInserttpreinv_dtl_ins(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tpreinv_dtl_ins", arr);
    }
    public void vGetMstCustomerSpecial(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_customer_special_get", ref rs, arr);
    }
    public void vUpdatePOHOdetail(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tpo_dtl_ho_upd", arr);
    }
    public void vUpdMstPOHO(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_po_ho_upd", arr);
    }
    public void vGetMstPOHO(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_po_ho_get", ref rs, arr);
    }

    public void vGetCustCashOverDueCount(ref SqlDataReader rs, List<cArrayList> arr)
    {
        dal.vGetRecordsetSP("sp_CustomerOverDueCheckCashOrder_count", ref rs, arr);
    }
    public void vtemployeeTaskDtl(List<cArrayList> arr, ref string taskdtl_cd)
    {
        dal.vExecuteSP("sp_temployeeTask_dtl_ins", arr, "@taskdtl_cd", ref taskdtl_cd);
    }
    public void vSearchEmployeeTask(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_temployeeTask_getByID", ref rs, arr);
    }
    public void vtemployeeTask(List<cArrayList> arr, ref string temployeetask_cd)
    {
        dal.vExecuteSP("sp_temployeeTask_ins", arr, "@temployeetask_cd", ref temployeetask_cd);
    }
    public void vInsertMstLocation(List<cArrayList> arr, ref string sLoccd)
    {
        dal.vExecuteSP("sp_tmst_location_ins1", arr, "@loc_cd", ref sLoccd);
    }
    public void vDeleteWrkSalesReturnwrk(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_tsalesreturn_wrk_del", arr);
    }
    public void vInsertWrkSalesReturnWrk(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_tsalesreturn_wrk_ins", arr);
    }
    public void vInsertMSTSalesReturnWrk(List<cArrayList> arr, ref string sReturNo)
    {
        dal.vExecuteSP("sp_tmst_salesreturn_wrk_ins", arr, "@retur_no", ref sReturNo);
    }
    public void vInsertSalesReturDtlWrk(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsalesreturn_wrk_dtl_ins", arr);
    }

    public void vUpdateWrkSalesReturnWrk(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_salesreturn_wrk_upd", arr);
    }
    public void vGetSalesReturnWrk(ref SqlDataReader rs, List<cArrayList> arr)
    {
        dal.vGetRecordsetSP("sp_tmst_salesreturn_wrk_get", ref rs, arr);
    }
    public void vInsertTwrkSalesReturnFromCoreWrk(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_salesreturn_wrk_insfromcore", arr);
    }
    public void vGetWrkSalesReturnWrk(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_twrk_tsalesreturn_wrk_get", ref rs, arr);
    }
    public void vSearchCNDNAdjustmentrtn(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_CNDN_search_forrtn", ref rs, arr);
    }

    public void vInsertCashoutRequest_Car(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcashout_request_car_info_ins", arr);
    }
    public void vUpdatePODocument(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsalesorder_doc_upd", arr);
    }
    public void vDeletePODocument(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsalesorder_doc_del", arr);
    }
    public void vInsertPODocument(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsalesorder_doc_ins", arr);
    }
    public void vMerchandiserIncentiveCloning(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_Merchandisertarget_cloning", arr);
    }
    public void vSuportingTicketClose(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_ttab_problem_solving_close", arr);
    }
    public void vSuportingTicketReply(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_ttab_problem_solving_dtl_reply", arr);
    }
    public void vGetManualNo(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_get_manualno", ref rs, arr);
    }
    public void vInsertManualNo(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_ins_manualno", arr);
    }
    public void vGetSTDItemStock(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tpch_std_stock_get", ref rs, arr);
    }
    public void vGetSTDItemList(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tpch_std_item_get", ref rs, arr);
    }
    public void vGetCMO(ref SqlDataReader rs, List<cArrayList> arr)
    {
        dal.vGetRecordsetSP("sp_tpch_cmo_get", ref rs, arr);
    }
    public void vInsertCMO(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tpch_cmo_ins", arr);
    }
    public void vInsertSTD(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tpch_std_ins", arr);
    }
    public void vGetSalesPlan(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tpch_mst_salesplan_get", ref rs, arr);
    }
    public void vInsertSalesPlan(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tpch_mst_salesplan_ins", arr);
    }

    public void vDeleteAddItemSalesPlan(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tpch_salesplan_item_del", arr);
    }
    public void vInsertAddItemSalesPlan(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tpch_salesplan_item_ins", arr);
    }
    public void vUpdateCarModel(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_carmodel_upd", ref rs, arr);
    }
    public void vUpdateCarBrand(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_carbrand_upd", ref rs, arr);
    }
    public void vDeleteCarModelorBrand(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_carmodelorbrand_del", arr);
    }
    public void vInsertCarModel(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_carmodel_ins", ref rs, arr);
    }
    public void vInsertCarBrand(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_carbrand_ins", ref rs, arr);
    }

    public void vbatchrecalctacc_stock(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batchrecalctacc_stock", arr);
    }
    public void vBatchtacc_stock(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batch_tacc_stock", arr);
    }
    public void vSalestargetAchievementByProd(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_hr_salestargetAchievement_rptbyProduct_ins", arr);
    }
    public void vSalestargetAchievement(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_hr_salestargetAchievement_rpt_ins", arr);
    }
    public void vDeleteActiveMerchandiserHO(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_activeMerchandiser_dlt", arr);
    }
    public void vInsertActiveMerchandiserCloneHO(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_activeMerchandiser_insClone", arr);
    }
    public void vDeleteMerchandiserDocuments(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_MerchandiserUpload_dlt", arr);
    }
    public void vInsertActiveMerchandiserHO(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_activeMerchandiser_ins", arr);
    }
    public void vInsertMerchandiserUploadHO(List<cArrayList> arr, ref string merchUpload_cd)
    {
        dal.vExecuteSP("sp_tmst_MerchandiserUpload_ins", arr, "@merchUpload_cd", ref merchUpload_cd);
    }
    public void vInsertPaymentDtlreturn(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tpayment_dtl_ins_return", arr);
    }
    public void vInsertgoodreceiptinfo(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tgoodreceipt_info_ins", arr);
    }
    public void vUpdateMstEmployeeApp(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_employee_app_updt", arr);
    }
    public void vSearchMstEmployeeApp(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("Sp_tacc_mst_EmpApp_getByName", ref rs, arr);
    }
    public void vUpdate_tmst_employee_appByHO(List<cArrayList> arr)
    {
        dal.vExecuteSP("tmst_employee_app_upd_byHO", arr);
    }
    public void vUpdate_tmst_employee_app(List<cArrayList> arr)
    {
        dal.vExecuteSP("tmst_employee_app_upd", arr);
    }
    public void vInsert_tmst_employee_app(List<cArrayList> arr, ref string employee_app_cd)
    {
        dal.vExecuteSP("tmst_employee_app_ins", arr, "@employee_app_cd", ref employee_app_cd);
    }
    public void vDelVehicle(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_vehicle_dlt", arr);
    }
    public void vInsertAdjusmentPriceCustomerBooking(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tadjustmentprice_customer_ins2", arr);
    }
    public void vInsertAdjustmentPriceGusgrcdBooking(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tadjustmentprice_cusgrcd_ins2", arr);
    }
    public void vInsertBookingPrice(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcustomerprice_booking_ins2", arr);
    }
    public void vSearchFullReturn(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tdosales_invoice_getCustFullReturn", ref rs, arr);
    }
    public void vUpdateFullReturn(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_Invoice_fullReturn_upd", arr);
    }
    public void vInsertFullReturn(List<cArrayList> arr, ref string invIFR_no)
    {
        dal.vExecuteSP("sp_Invoice_fullReturn_ins", arr, "@invIFR_no", ref invIFR_no);
    }
    public void vUpdateCustomerTransfer(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcustomer_transfer_updbyfilename", arr);
    }
    public void vUpdateItemCustomer_Block_back(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_itemCustomer_Block_back", arr);
    }
    public void vUpdateItemCustomer_Block(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_itemCustomer_Block_upd", arr);
    }
    public void vInsertItemCustomer_Block(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_itemCustomer_Block_int", arr);
    }
    public void vDeleteItemCustomer_Block(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_itemCustomer_Block_dlt", arr);
    }
    public void vDeleteItemCustomer_Block_hist(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_itemCustomer_Block_dlt_hist", arr);
    }
    public void vInsertSalesOrderCancel(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsalesorder_cancel_ins", arr);
    }
    public void vInsertTargetAchievement(List<cArrayList> arr, ref string salesTargetAchievementID)
    {
        dal.vExecuteSP("sp_tmst_SalesTargetAchievement_int", arr, "@SalesTargetAchievementID", ref salesTargetAchievementID);
    }
    public void vInsertTargetAchievementDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tSalesTargetAchievement_Dtl_int", arr);
    }

    public void vSearchMstEmployeeByJobTitleQry(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_employee_searchbyjobtitleqry", ref rs, arr);
    }
    public void vJSONGetInfoCustomer(ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_json_infocustomer", ref rs);
    }
    public void vJSONGetInfoSalesman(ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_json_infosalesman", ref rs);
    }
    public void vJSONGetSalespoint(ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_json_salespoint", ref rs);
    }
    public void vJSONGetLastSalesman(ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_json_lastsalesman", ref rs);
    }
    public void vJSONGetPlaces(ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_json_places", ref rs);
    }


    public void vUpdateMstReqPromo(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_reqdiscount_upd", arr);
    }
    public void vUpdateMstClaimCMHO(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_claim_cmho_upd", arr);
    }
    public void vGetMstinvoiceHO(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_invoiceinfo_ho_get", ref rs, arr);
    }
    public void vInsertTWRKClaimHOConfirm(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrkclaimconfirm_ho_ins", arr);
    }

    public void vDeleteItemMerchandiser(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_titem_merchandiser_del", arr);
    }
    public void vInsertItemMerchandiser(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_titem_merchandiser_ins", arr);
    }

    public void vBatchEmailProposal(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_sendmailtogm", arr);
    }
    public void vUpdatePettycashCashoutRequest(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_temployee_advanced_updt", arr);
    }
    public void vUpdatePettycashCashoutRequestByStatus(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_temployee_advanced_updbystatus", arr);
    }
    public void vInsertPettycashCashoutRequest(List<cArrayList> arr, ref string sDoc_no)
    {
        dal.vExecuteSP("sp_temployee_advanced_ins", arr, "@doc_no", ref sDoc_no);
    }
    public void vGetPettyCashEmp(ref SqlDataReader rs, List<cArrayList> arr)
    {
        //dal.vGetRecordsetSP("sp_temployee_advancedEmp_get2", ref rs, arr);
        dal.vGetRecordsetSP("sp_temployee_advancedEmp_get6", ref rs, arr);
    }
    public void vGetPettyCashEmpByRefno(ref SqlDataReader rs, List<cArrayList> arr)
    {
        dal.vGetRecordsetSP("sp_temployee_advancedEmps_pettycashRefno_get", ref rs, arr);
    }
    public void vGetPettyCashEmpTotalBal(ref SqlDataReader rs, List<cArrayList> arr)
    {
        //dal.vGetRecordsetSP("sp_temployee_advancedEmp_TotalBal_get", ref rs, arr);
        dal.vGetRecordsetSP("sp_temployee_advancedEmp_get2", ref rs, arr);
    }
    public void vDelCustomerMerchandiser(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcustomer_merchandiser_del", arr);
    }
    public void vInsertCustomerMerchandiser(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcustomer_merchandiser_ins", arr);
    }
    public void vDelReqPromoSalespoint(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_treqdiscount_salespoint_del", arr);
    }
    public void vInsertReqPromoSalespoint(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_treqdiscount_salespoint_ins", arr);
    }
    public void vInsertReqPromoDocument(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_treqdiscount_doc_ins", arr);
    }
    public void vUpdateContractKADocument(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcontract_ka_doc_upd", arr);
    }
    public void vGetCMByClaim(ref SqlDataReader rs, List<cArrayList> arr)
    {
        dal.vGetRecordsetSP("sp_tcreditmemo_doc_claim", ref rs, arr);
    }
    public void vGetAgreementCMByClaim(ref SqlDataReader rs, List<cArrayList> arr)
    {
        dal.vGetRecordsetSP("sp_tcontract_ka_doc_claim", ref rs, arr);
    }
    public void vInsertCustLoc(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_customerLoc_updt", arr);
    }
    public void vGetCustomersBySalesman(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_salesman_customers_get", ref rs, arr);
    }
    public void vDeleteDNCutomerAmt(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_cndncustomer_dlt", arr);
    }
    public void vUpdtDNCutomerAmt(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_cndncustomer_updtAmt", arr);
    }
    public void vAppContractKA(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_contract_ka_app", arr);
    }
    public void vInsertClaimdtlcashoutHO(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tclaimdtl_cashout_ho_ins", arr);
    }
    public void vGetCashoutHOList(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_auto_cashout_ho_get", ref rs, arr);
    }
    public void vUpdtDNCutomerApp(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_cndncustomer_updt", arr);
    }

    public void vBatchSendClaimPrincipal()
    {
        dal.vExecuteSP("sp_sendclaimtoprincipal");
    }
    public void vGetClaimDetailHo(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tclaimho_get", ref rs, arr);
    }
    public void vInsertRpmDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_trpm_dtl_ins", arr);
    }
    public void vDelRpmDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_trpm_dtl_del", arr);
    }
    public void vInsertItemPromotion(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_titem_promotion_ins", arr);
    }

    public void vInsCNDNCustomer(List<cArrayList> arr, ref string cndn_cd)
    {
        dal.vExecuteSP("sp_tmst_cndncustomer_int", arr, "@cndn_no", ref cndn_cd);
    }

    public void vUpdtCNDNApp(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tacc_cndn_updt", arr);
    }
    public void vInsActivedriver(List<cArrayList> arr, ref string retValue)
    {
        dal.vExecuteSP("sp_tmst_activedriver_int", arr, "@retValue", ref retValue);
    }
    public void vUpdActivedriver(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_activedriver_upd", arr);
    }
    public void vSearchMstCustomerBytype(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_customer_searchBytype", ref rs, arr);
    }
    public void vGet_tproposal_paid_pettycash_DailyClosingBalance(ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tproposal_paid_pettycash_DailyClosingBalance", ref rs);
    }
    public void vGetpettycash_DailyClosingBalance(ref SqlDataReader rs, List<cArrayList> arr)
    {
        dal.vGetRecordsetSP("sp_tpch_pettycash_DailyClosingBalance", ref rs);
    }

    public void vDelCustomerReturnInvoice(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcustomer_returninvoice_del", arr);
    }
    public void vInsertCustomerReturnInvoice(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcustomer_returninvoice_ins", arr);
    }
    public void vSearchInvoiceByItem(ref SqlDataReader rs, List<cArrayList> arr)
    {
        dal.vGetRecordsetSP("sp_tdosales_invoice_getbyitem", ref rs, arr);
    }
    public void vInsertCashoutHoDocument(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tproposal_paid_ho_doc_ins", arr);
    }
    public void vUpdateProposalPaidHo(List<cArrayList> arr)
    {
        dal.vExecuteSP("tproposal_paid_ho_upd", arr);
    }
    public void vInsertWrkProposalPaidHo(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_proposalpaid_ho_ins", arr);
    }
    public void vDelWrkProposalPaidHo(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_proposalpaid_ho_del", arr);
    }
    public void vInsertProposalPaidHo(List<cArrayList> arr, ref string sClaimNo)
    {
        dal.vExecuteSP("tproposal_paid_ho_ins", arr, "@claimco_cd", ref sClaimNo);
    }
    public void vInsertTclaimDocHo(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tclaim_documentho_ins", arr);
    }
    public void vInsertClaimdtlcontractKA(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tclaimdtl_contractka_ins", arr);
    }
    public void vInsertClaimHO(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tclaimho_ins", arr);
    }
    public void vGetContractKAList(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_auto_contractka_get", ref rs, arr);
    }

    public void vGetEmpAdvBalance(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tpch_pettycash_getEmpPettycashBalance", ref rs, arr);
    }
    public void vGetEmpAdvBalance2(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tpch_pettycash_getEmpPettycashBalance2", ref rs, arr);
    }
    public void vInserClaimInvVat(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tclaim_payment_inv_ins", arr);
    }

    public void vBranchHOConfirmationInst(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_BranchHOConfirmation_int", arr);
    }
    public void vDriverIncentiveCloning(List<cArrayList> arr, ref string CalIncetiveID)
    {
        dal.vExecuteSP("sp_driverIncentiveCloning", arr, "@return", ref CalIncetiveID);
    }
    public void vInsertCreditMemoDoc(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcreditmemo_doc_ins", arr);
    }
    public void vUpdateCreditMemoHO(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_creditmemo_upd", arr);
    }
    public void vGetmstcreditmemobyno(ref SqlDataReader rs, List<cArrayList> arr)
    {
        dal.vGetRecordsetSP("sp_tmst_creditmemo_search", ref rs, arr);
    }
    public void vUpdateContractKA(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_contract_ka_upd", arr);
    }
    public void vDelContractKA(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_contract_ka_del", arr);
    }
    public void vInsertClaimSuspendDocument(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tclaim_reject_doc", arr);
    }
    public void vSearchProposalBySalespoint(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_proposal_salespoint", ref rs, arr);
    }
    public void vInsertReqPromoItemFree(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_treqdiscount_itemfree_ins", arr);
    }
    public void vInsertReqPromoProductFree(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_treqdiscount_productfree_ins", arr);
    }
    public void vDelReqPromoItemFree(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_treqdiscount_itemfree_del", arr);
    }
    public void vDelReqPromoProductFree(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_treqdiscount_productfree_del", arr);
    }

    public void vUpdateSalesReturnCostingByReturNo(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsalesreturn_costing_updbyreturno", arr);
    }
    public void vDelSalesReturnCosting(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsalesreturn_costing_del", arr);
    }
    public void vInsertSalesReturnCosting(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsalesreturn_costing_ins", arr);
    }
    public void vGetItemcashout(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_itemcashout_cust", ref rs, arr);
    }

    public void vInsertTdosalesinvoice_fullreturn(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdosalesinvoicefullreturn_ins", arr);
    }
    public void vGetMstinvoiceCO(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_invoiceinfoCO_get", ref rs, arr);
    }
    public void vGetMstinvoiceCNDN(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_invoiceinfoCNDN_get", ref rs, arr);
    }

    public void vInsertSalesReturn3(List<cArrayList> arr, ref string sReturNo)
    {
        dal.vExecuteSP("sp_tsalesreturn_ins3", arr, "@retur_no", ref sReturNo);
    }
    public void vSearchMstCustomerBySalesmanOffice(ref SqlDataReader rs, List<cArrayList> arr)
    {
        dal.vGetRecordsetSP("sp_tmst_customer_getbysalesmanoffice", ref rs, arr);
    }
    public void vDelWrkSpecialOrder(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_specialorder_del", arr);
    }
    public void vInsertWrkSpecialOrder(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_specialorder_ins", arr);
    }
    public void vGetMstCNDNEmployee(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_cndnemployee_get", ref rs, arr);
    }
    public void vBatchProposalToReqDiscount(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batchproposaltoreqdiscount", arr);
    }
    public void vUpdateMstCNDNEmployeeByFIle(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_cndnemployee_updbyfile", arr);
    }
    public void vSearchCustomerBySalesman2(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_customer_searchbysalesman", ref rs, arr);
    }
    public void vUpdateWrkCnDnEmployee(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_cndnemployee_upd", arr);
    }
    public void vDelWrkCnDnEmployee(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_cndnemployee_del", arr);
    }
    public void vInsertWrkCNDNEmployee(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_cndnemployee_ins", arr);
    }
    public void vInsertMstCnDnEmployee(List<cArrayList> arr, ref string sCnDn)
    {
        dal.vExecuteSP("sp_tmst_cndnemployee_ins", arr, "@cndn_no", ref sCnDn);
    }
    public void vInsertReqDiscountFormula(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_treqdiscount_formula_ins", arr);
    }
    public void vDelReqDiscountFormula(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_treqdiscount_formula_del", arr);
    }
    public void vDelReqDiscountFreeProduct(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_treqdiscount_freeproduct_del", arr);
    }
    public void vInsertReqDiscountFreeProduct(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_treqdiscount_freeproduct_ins", arr);
    }
    public void vDelREqDiscountFreeItem(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_treqdiscount_freeitem_del", arr);
    }
    public void vInsertReqDiscountFreeItem(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_treqdiscount_freeitem_ins", arr);
    }
    public void vSyncBrnToHo()
    {
        dal.vExecuteSP("sp_syncbrntoho");
    }
    public void vInsertClosingDailyVal(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_ClosingDailyVal_tblins", arr);
    }
    public void validationDailyClosing(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_validationDailyClosing", arr);
    }

    public void vApprovalClaimCNDN(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tclaim_reqcndn_app", arr);
    }
    public void vApprovalMstReqPromo(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_reqdiscount_app", arr);
    }

    public void vInsertMstDiscountPromo(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_discount_promo_ins", arr);
    }
    public void vInsertDiscSalespointPromo(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdisc_salespoint_promo_ins", arr);
    }
    public void vInsertDiscCustomerPromo(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdisc_customer_promo_ins", arr);
    }

    public void vInsertDiscCusgrcdPromo(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdisc_cusgrcd_promo_ins", arr);
    }

    public void vInsertDiscCustypePromo(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdisc_custtype_promo_ins", arr);
    }
    public void vInsertDiscItemPromo(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdisc_item_promo_ins", arr);
    }
    public void vInsertDiscProductPromo(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdisc_product_promo_ins", arr);
    }
    public void vGeneratedDiscountFormulaPromo(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdiscount_formula_promo_gen", arr);
    }

    //public void vApprovalClaimCNDN(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tclaim_reqcndn_app", arr);
    //}
    //public void vApprovalMstReqPromo(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tmst_reqdiscount_app", arr);
    //}
    public void vInsertCashAdvanced(List<cArrayList> arr, ref string cashout_cd)
    {
        dal.vExecuteSP("sp_temployee_cashadvanced_ins", arr, "@cashout_cd", ref cashout_cd);
    }
    public void vSearchEmployee(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_employee_cust", ref rs, arr);
    }
    public void vSearchItemCashOut(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_itemcashout_cust", ref rs, arr);
    }
    public void vGetReqPromo(ref SqlDataReader rs, List<cArrayList> arr)
    {
        dal.vGetRecordsetSP("sp_tmst_reqdiscount_get", ref rs, arr);
    }

    public void vUpdateMstReqPromoNo(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_reqdiscount_upd", arr);
    }
    public void vInsertMstReqPromo(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_reqdiscount_ins", arr);
    }
    public void vDelReqPromoProduct(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_treqdiscount_product_del", arr);
    }
    public void vInsertReqPromoProduct(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_treqdiscount_product_ins", arr);
    }
    public void vDelReqPromoItem(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_treqdiscount_item_del", arr);
    }
    public void vInsertReqPromoItem(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_treqdiscount_item_ins", arr);
    }
    public void vDelReqPromoCustType(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_treqdiscount_custtype_del", arr);
    }
    public void vInsertReqPromoCustType(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_treqdiscount_custtype_ins", arr);
    }
    public void vDelReqPromoCusgrcd(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_treqdiscount_cusgrcd_del", arr);
    }
    public void vInsertReqPromoCusgrcd(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_treqdiscount_cusgrcd_ins", arr);
    }
    public void vDelReqPromoCustomer(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_treqdiscount_customer_del", arr);
    }
    public void vInsertReqPromoCustomer(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_treqdiscount_customer_ins", arr);
    }

    public void vInsertCNDNDocument(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tclaim_reqcndn_doc_ins", arr);
    }
    public void vInsertClaimdtlprice(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tclaimdtl_price_ins", arr);
    }
    public void vGetDifferentPriceAdjList(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_auto_price_get", ref rs, arr);
    }
    public void vInsertAdjustmentBookingCustomerType(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tadjustmentprice_booking_customertype_ins", arr);
    }
    public void vBatchUpdatePostponeInv(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batchinvpostpone", arr);
    }
    public void vUpdatePostponeInvAll(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_inv_postpone_upd_all", arr);
    }
    public void vUpdatePostponeInv(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_inv_postpone_upd", arr);
    }
    public void vInsertCreditMemoHO(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_creditmemo_ins", arr);
    }
    public void vSearchMstCustomerCM(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_customer_searchpro", ref rs, arr);
    }
    public void vDeleteCreditMemoTransfer(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcreditmemo_transfer_del", arr);
    }
    public void vInsertCreditMemoTransfer(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcreditmemo_transfer_ins", arr);
    }
    public void vDeleteCreditMemoAgreement(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcreditmemo_agreement_del", arr);
    }
    public void vInsertCreditMemoAgreement(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcreditmemo_agreement_ins", arr);
    }
    public void vDeleteCreditMemoDebt(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcreditmemo_debt_del", arr);
    }
    public void vInsertCreditMemoDebt(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcreditmemo_debt_ins", arr);
    }
    public void vDeleteCreditMemoBank(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcreditmemo_bank_del", arr);
    }
    public void vInsertCreditMemoBank(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcreditmemo_bank_ins", arr);
    }
    public void vDeleteCreditMemoCash(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcreditmemo_cash_del", arr);
    }
    public void vInsertCreditMemoCash(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcreditmemo_cash_ins", arr);
    }
    public void vTacc_Employeelog_Ins(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tacc_employeelog_ins", ref rs, arr);
    }
    public void vInsertPaymentDtl1pctDisc(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tpayment_dtl1pctDisc_ins", arr);
    }
    public void vInsertPaymentBooking1pctDisc(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tpayment_booking1pctDisc_ins", arr);
    }

    public void vSearchMstHoAg(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_contract_ka_search", ref rs, arr);
    }
    public void vInsertsalesorderfreebyitemcancel(List<cArrayList> arr)
    {
        dal.vExecuteSP("SP_TSALESORDER_DISCFREECANCEL", arr);
    }
    public void vSearchMstCustomerForOtherPayment(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_customerOtherPayment_search", ref rs, arr);
    }
    public void vInsertcanvasorderfreebyitem(List<cArrayList> arr)
    {
        dal.vExecuteSP("SP_TCANVASORDER_DISCFREE", arr);
    }

    public void vInsEditVatInvoice(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tclaim_payment_inv_upd", arr);
    }

    public void vBatchSendPaymentNotificationConfirmed(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_sendpaymentconfirmho", arr);
    }
    public void vUpdateAppReturnHo(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_treturnho_booking_updbystatus", arr);
    }
    public void vInsertsalesorderfreebyitem(List<cArrayList> arr)
    {
        dal.vExecuteSP("SP_TSALESORDER_DISCFREE", arr);
    }
    public void vTdriver_Otherdelivery_Ins(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tdriver_otherdelivery_ins", ref rs, arr);
    }
    public void vInsertContractKADocument(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcontract_ka_doc_ins", arr);
    }
    public void vDeleteContractKACusgrcd(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcontract_ka_cusgrcd_del", arr);
    }
    public void vDeleteContractKACustomer(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcontract_ka_customer_del", arr);
    }
    public void vInsertContractKACustomer(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcontract_ka_customer_ins", arr);
    }

    public void vUpdateBankDepositByStatusByHO(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tbank_deposit_updbystatusByHO", arr);
    }
    public void vGetmstcontractKAbyno(ref SqlDataReader rs, List<cArrayList> arr)
    {
        dal.vGetRecordsetSP("sp_tmst_contract_ka_getbyno", ref rs, arr);
    }
    public void vSearchMstProposalAll(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_proposal_search_all", ref rs, arr);
    }
    public void vInsertContractKAProposal(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcontract_ka_proposal_ins", arr);
    }
    public void vInsertContractKA(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_contract_ka_ins", arr);
    }
    public void vDeleteContractKAProposal(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcontract_ka_proposal_del", arr);
    }

    public void vGetContractByNumber(ref SqlDataReader rs, List<cArrayList> arr)
    {
        dal.vGetRecordsetSP("sp_tcontract_invoice_getByContract_no", ref rs, arr);
    }
    public void vInsertContract_otherPayment(List<cArrayList> arr, ref string topID)
    {
        dal.vExecuteSP("sp_tcontract_otherPayment_int", arr, "@topID", ref topID);
    }
    public void vSearchMstCustomerAG(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_customer_searchAG", ref rs, arr);
    }
    public void vInsertAdjustmentBookingCustomer(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tadjustmentprice_booking_customer_ins", arr);
    }
    public void vInsertAdjustmentBookingCusGrCd(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tadjustmentprice_booking_cusgrcd_ins", arr);
    }

    public void vInsertPassintotUserPasswordMap(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tUser_PasswordMap_ins", arr);
    }
    public void vGetemailinfofromtuserprofile(ref SqlDataReader rs, List<cArrayList> arr)
    {
        dal.vGetRecordsetSP("sp_tuserprofile_email_getinfo", ref rs, arr);
    }
    public void vCheckOldpassNewpass(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_checkoldpass_newpass", ref rs, arr);
    }
    public void vGetpassfromtuserprofile(ref SqlDataReader rs, List<cArrayList> arr)
    {
        dal.vGetRecordsetSP("sp_tuserprofile_pass_get", ref rs, arr);
    }
    public void vInsertTuserInfo(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tuser_info_ins", arr);
    }
    public void vInserttempPassintotUserPasswordMap(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tUser_PasswordMap_instemp", arr);
    }

    public void vUpdatetuser_profiletemp(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tuser_profile_updtemp", arr);
    }
    public void vCheckTuserInfo(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tuser_info_check", ref rs, arr);
    }
    public void vGetemailfromtuserprofile(ref SqlDataReader rs, List<cArrayList> arr)
    {
        dal.vGetRecordsetSP("sp_tuserprofile_email_get", ref rs, arr);
    }
    public void vGetuserInfo(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tuser_info_get", ref rs, arr);
    }

    public void vSearchCNDNAdjustment(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_CNDN_search", ref rs, arr);
    }
    public void vGetinv(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_getinv", ref rs, arr);
    }

    public void vSearchAccMstSupplier2(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tacc_mst_supplier_search", ref rs, arr);
    }
    public void vDeleteContractKAAgreement(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcontract_ka_agreement_del", arr);
    }
    public void vInsertContractKAAgreement(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcontract_ka_agreement_ins", arr);
    }

    public void vDeleteContractKASalespoint(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcontract_ka_salespoint_del", arr);
    }
    public void vInsertContractKASalespoint(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcontract_ka_salespoint_ins", arr);
    }

    public void vInsertContractInvoice(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcontract_invoice_ins", arr);
    }

    public void vInsertfreebyitem(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_freebyitem", arr);
    }
    public void vInsertCashoutDocument(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tproposal_paid_doc_ins", arr);
    }
    public void vSearchAccMstSupplier(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tacc_mst_supplier_search", ref rs, arr);
    }

    public void vUpdateDosalesinvoicePanda(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_dosalesinvoicepada_instocore", arr);
    }
    public void vUpdateWrkDosalesInvoicePanda(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_dosalesinvoicepanda_upd", arr);
    }
    public void vDelWrkDosalesInvoicePanda(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_dosalesinvoicepanda_del", arr);
    }
    public void vInsertWrkDosalesinvoicePandaFromCore(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_dosalesinvoicepanda_insfromcore", arr);
    }
    public void vInsertWrkPaymentInvoiceFromCore(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_paymentinvoice_insfromcore", arr);
    }

    public void vDelDosalesinvoicePanda(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_dosalesinvoicepanda_del", arr);
    }
    public void vBatchDiscamountInvoice(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batchdiscamountinvoice", arr);
    }
    public void vtacc_cndndtl_dtl_dlt(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tacc_cndndtl_dlt", arr);
    }
    public void vInsertACC_cndn(List<cArrayList> arr, ref string cndn_cd)
    {
        dal.vExecuteSP("sp_tacc_cndn_ins", arr, "@cndn_cd", ref cndn_cd);
    }

    public void vtacc_cndndtl_dtl_int(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tacc_cndndtl_int", arr);
    }
    public void vtacc_stock_cardByGoodReceipt(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batchstockcard", arr);
    }

    public void vtacc_stock_cardBySalesReturn(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batchstockcard", arr);
    }
    public void vDelAccTransactionMap(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tacc_transactionmap_del", arr);
    }
    public void vGetInvoiceConfirmCash(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_twrk_dosalescash_get", ref rs, arr);
    }

    public void vGetInvoiceConfirmFree(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_twrk_dosalesfree_get", ref rs, arr);
    }

    public void vGetInvoiceConfirmDtl(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_twrk_dosalesdtl_get", ref rs, arr);
    }

    public void vgps_salesman_customer_search(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSPHO("sp_gps_salesman_customer_search", ref rs, arr);
    }

    public string vLookUpHO(string sSQL)
    {
        string sTemp = "";
        SqlDataReader rsx = null;
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = cd.getConnectionHO();
        cmd.CommandText = sSQL;
        cmd.CommandType = CommandType.Text;
        rsx = cmd.ExecuteReader();
        while (rsx.Read())
        {
            sTemp = rsx[0].ToString();
        }
        rsx.Close();
        return (sTemp);
    }
    public void vBindingGridToSpHO(ref GridView grd, string sSPName, List<cArrayList> arr)
    {
        SqlDataReader rs = null;
        dal.vGetRecordsetSPHO(sSPName, ref rs, arr);
        DataTable dta = new DataTable();
        dta.Load(rs);
        grd.DataSource = dta.DefaultView;
        grd.DataBind();
    }
    public void vInsertMstItemCashout1(List<cArrayList> arr, ref string sNo)
    {
        dal.vExecuteSP("sp_tmst_itemcashout1_ins", arr, "@return", ref sNo);
    }
    public void vGetgetmaps_salesmanol(ref SqlDataReader rs)
    {
        dal.vGetRecordsetSPHO("sp_getmaps_salesmanol", ref rs);
    }

    public void vInsertDoDriverInvoiceReceived(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdosalesinvoice_received_driver", arr);
    }
    public void vInsertWrkDoDriver(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_dodriver_ins", arr);
    }
    public void vUpdateWrkDosalesCash(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_dosalescash_upd", arr);
    }
    public void vDelWrkDoSalesCash(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_dosalescash_del", arr);
    }
    public void vInsertWrkDosalesCash(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_dosalescash_ins", arr);
    }
    public void vGetMstBalanceconfirm(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_balanceconfirm_get", ref rs, arr);
    }
    public void vInsertTbalanceconfirmremark(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tbalanceconfirmremark_ins", arr);
    }
    public void vInsertTbalanceconfirmdtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tbalanceconfirmdtl_ins", arr);
    }
    public void vUpdateMSTBalanceConfirmed(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_balanceconfirm_upd", arr);
    }
    //public void vGetMstBalanceconfirm(List<cArrayList> arr, ref SqlDataReader rs)
    //{
    //    dal.vGetRecordsetSP("sp_tmst_balanceconfirm_get", ref rs, arr);
    //}
    public void vInsertMstBalanceconfirm(List<cArrayList> arr, ref string sBcNo)
    {
        dal.vExecuteSP("sp_tmst_balanceconfirm_ins", arr, "@confirm_no", ref sBcNo);
    }
    //public void vGetMstBalanceconfirm(List<cArrayList> arr, ref SqlDataReader rs)
    //{
    //    dal.vGetRecordsetSP("sp_tmst_balanceconfirm_get", ref rs, arr);
    //}
    //public void vInsertMstBalanceconfirm(List<cArrayList> arr, ref string sBcNo)
    //{
    //    dal.vExecuteSP("sp_tmst_balanceconfirm_ins", arr, "@confirm_no", ref sBcNo);
    //}
    public void vInsertCustPO(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsalesorder_po_ins", arr);
    }
    public void vGetCustomerBalance(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_customer_balance_get", ref rs, arr);
    }
    public void vGetMstPO1(ref SqlDataReader rs, List<cArrayList> arr)
    {
        dal.vGetRecordsetSP("sp_tmst_po_get1", ref rs, arr);
    }
    public void vGetPONo(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_getsystemno", ref rs, arr);
    }
    public DataTable vGetCashOutRemarkByCode(List<cArrayList> arr)
    {
        return dal.GetValueFromSP("sp_cashOutRemarkByCode_get", arr);
    }
    public DataTable vGetCashOutRemarkByIDS(List<cArrayList> arr)
    {
        return dal.GetValueFromSP("sp_cashOutremarkBySeq_get", arr);
    }
    public void vDeleteItemCashOutRemark(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_titemcashout_Remark_dlt", arr);
    }
    public DataTable vGetCashOutByCode(List<cArrayList> arr)
    {
        return dal.GetValueFromSP("sp_cashOutByCode_get", arr);
    }
    public DataTable vGetCashOutAttributeByCode(List<cArrayList> arr)
    {
        return dal.GetValueFromSP("sp_cashOutAttributeByCode_get", arr);
    }
    public DataTable vGetCashOutAttributeByIDS(List<cArrayList> arr)
    {
        return dal.GetValueFromSP("sp_cashOutAttributeByIDS_get", arr);
    }

    public void vInsertItemCashOutAttribute(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_titemcashout_attribute_ins", arr);
    }

    public void vUpdateItemCashOutAttribute(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_titemcashout_attribute_upd", arr);
    }
    public void vDeleteItemCashOutAttribute(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_titemcashout_attribute_dlt", arr);
    }
    public void vUpdateWrkDriver(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_driver_upd", arr);
    }
    public void vUpdateDOSalesDtlFromWRK4(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdosales_dtl_updfromwrk4", arr);
    }
    public void vUpdateDOSalesDtlFromWRK3(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdosales_dtl_updfromwrk3", arr);
    }
    public void vUpdateDOSalesDtlFromWRK2(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdosales_dtl_updfromwrk2", arr);
    }
    public void vUpdateMstDosalesByStatus(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_dosales_updbystatus", arr);
    }

    public void vInsertAccTransactionMap(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tacc_transactionmap_ins", arr);
    }
    public void vGetUserProfileByName(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tuser_profile_getbyname", ref rs, arr);
    }
    public void vUpdateSalesReturnByStatus2(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsalesreturn_updbystatus2", arr);
    }
    public void vGetCashoutRequest(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tcashout_request_get", ref rs, arr);
    }
    public void vBatchAccTransactionLog(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batchacctransaction", arr);
    }
    public void vInsertAccTransactionLog(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tacc_transactionlog_ins", arr);
    }

    public void vInsertOtherRecommendation(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_trpt_hrd_kpiresult_upd", arr);
    }
    public void vInsertHRDKPITarDeleteDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_thrd_kpitargetdtl_dlt", arr);
    }
    public void vInsertHRDKPITargetDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_thrd_kpitargetdtl_ins", arr);
    }
    public void vSyncSalesTarget(List<cArrayList> arr, string procedureName)
    {
        dal.vExecuteSP(procedureName, arr);
    }
    public void vBatchEmailtoPAFL(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_sendmailtopafl", arr);
    }
    public void vUpdateIncetiveRange(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_IncetiveRange_upd", arr);
    }
    public void vInsertCalculateIncetive(List<cArrayList> arr, ref string CalIncetiveID)
    {
        dal.vExecuteSP("sp_thrd_CalculateIncetive_int", arr, "@CalIncetiveID", ref CalIncetiveID);
    }
    public void vInsertCalculateDtlIncetive(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_thrd_CalIncetiveDtl_int", arr);
    }
    public void vInsertCalculateRangeDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_thrd_IncetiveRangeDtl_int", arr);
    }
    public void vUpdateWrkDosalesFree(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_dosalesfree_upd", arr);
    }
    public void vDelWrkDoSalesFree(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_dosalesfree_del", arr);
    }
    public void vInsertWrkDosalesFree(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_dosalesfree_ins", arr);
    }
    public void vInsertMstItemCashoutByID(List<cArrayList> arr, ref string itemco_cd)
    {
        dal.vExecuteSP("sp_tmst_itemcashoutByID_ins", arr, "@itemco_cd", ref itemco_cd);
    }
    public void vUpdateMstItemCashoutByID(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_itemcashoutByID_upd", arr);
    }
    public void vAppPaymentClaim(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tclaim_payment_app", arr);
    }
    public void vInsertMastAccSupplier(List<cArrayList> arr, ref string supplier_cd)
    {
        dal.vExecuteSP("sp_tacc_mst_supplier_ins", arr);
    }
    public void vUpdateMastAccSupplier(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tacc_mst_supplier_upd", arr);
    }
    public void vDeleteMastAccSupplier(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tacc_mst_supplier_del", arr);
    }

    public void vUpdatePaymentPromised1(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tpayment_promised_upd1", arr);
    }

    public void vDelClaimConfirmPostpone(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tclaimconfirm_postone_del", arr);
    }
    public void vInsertClaimConfirmPostpone(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tclaimconfirm_postone", arr);
    }
    public void vUpdateWRKCashoutAttribute(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_cashoutattribute_upd", arr);
    }
    public void vInsertWRKCashoutAttribute(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_cashoutattribute_ins", arr);
    }
    public void vDelWRKCashoutAttribute(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_cashoutattribute_del", arr);
    }
    public void vInsertCashoutAttribute(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcashout_attribute_ins", arr);
    }
    public void vUpdateCashoutRequest(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcashout_request_upd", arr);
    }
    public void vSearchPaymentPromised(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tpayment_promised_search", ref rs);
    }
    //public void vUpdateWRKCashoutAttribute(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_twrk_cashoutattribute_upd", arr);
    //}
    //public void vInsertWRKCashoutAttribute(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_twrk_cashoutattribute_ins", arr);
    //}
    //public void vDelWRKCashoutAttribute(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_twrk_cashoutattribute_del", arr);
    //}
    //public void vInsertCashoutAttribute(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tcashout_attribute_ins", arr);
    //}
    //public void vUpdateCashoutRequest(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tcashout_request_upd", arr);
    //}
    //public void vSearchPaymentPromised(List<cArrayList> arr, ref SqlDataReader rs)
    //{
    //    dal.vGetRecordsetSP("sp_tpayment_promised_search", ref rs);
    //}
    //public void vUpdateWRKCashoutAttribute(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_twrk_cashoutattribute_upd", arr);
    //}
    //public void vInsertWRKCashoutAttribute(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_twrk_cashoutattribute_ins", arr);
    //}
    //public void vDelWRKCashoutAttribute(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_twrk_cashoutattribute_del", arr);
    //}
    //public void vInsertCashoutAttribute(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tcashout_attribute_ins", arr);
    //}
    //public void vUpdateCashoutRequest(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tcashout_request_upd", arr);
    //}
    //public void vSearchPaymentPromised(List<cArrayList> arr, ref SqlDataReader rs)
    //{
    //    dal.vGetRecordsetSP("sp_tpayment_promised_search", ref rs);
    //}
    public void vUpdatePromisePaymentByStatus(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tpayment_promised_updbystatus", arr);
    }
    public void vInsertClaimProposal(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tclaim_doc_proposal", arr);
    }
    public void vBatchNotifyClaimPending()
    {
        dal.vExecuteSP("sp_batchsendnotifyclaimpending");
    }
    public void vDelDoDtlByItem(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdo_dtl_delByItem", arr);
    }
    public void vGeneratedDiscountFreeProduct(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdiscount_freeproduct_gen", arr);
    }
    public void vGeneratedDiscountFreeitem(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdiscount_freeitem_gen", arr);
    }
    public void vGeneratedDiscountFormula(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdiscount_formula_gen", arr);
    }
    public void vUpdateDOHO(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_do_updHO", arr);
    }
    public void vInsertMstDOHO(List<cArrayList> arr, ref string sDONo)
    {
        dal.vExecuteSP("sp_tmst_do_insHO", arr, "@do_no", ref sDONo);
    }
    public void vDelDoDtl3(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdo_dtl_del", arr);
    }
    public void vInsertDoDtlHO(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdo_dtl_insHO", arr);
    }
    public void vUpdateDoDtlHO(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdo_dtl_updHO", arr);
    }
    public void vUpdatePoDtlHO(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_po_Dtl_upd_HO", arr);
    }
    public void vInserttpo_dtl_insHO(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tpo_dtl_ins_HO", arr);
    }
    public void vUpdatePoStatus(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_po_updStatus_Branch", arr);
    }
    public void vBindingComboToSpHO(ref DropDownList cbo, string sSPName, string sVal, string sDisp, List<cArrayList> arr)
    {
        SqlDataReader rs = null;
        DataTable dta = new DataTable();
        dal.vGetRecordsetSPHO(sSPName, ref rs, arr);
        dta.Load(rs);
        cbo.DataValueField = sVal;
        cbo.DataTextField = sDisp;
        cbo.DataSource = dta.DefaultView;
        cbo.DataBind();
    }
    public void vGetgetmaps_salesman_tran(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSPHO("sp_getmaps_salesman_tran", ref rs, arr);
    }
    public void vUpdateBankDepositByStatus(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tbank_deposit_updbystatus", arr);
    }
    public void vBatchStockCard(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batchstockcard", arr);
    }
    public void vUpdateDOSalesDtlFromWRK(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdosales_dtl_updfromwrk", arr);
    }
    public void vSearchAccSupplier(ref SqlDataReader rs, List<cArrayList> arr)
    {
        dal.vGetRecordsetSP("sp_tacc_mst_supplier_getbycode", ref rs, arr);
    }
    public void vUpdateRptHRDKPIResult(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_trpt_hrd_kpiresultdtl", arr);
    }
    public void vBatchResultKPI(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_rpt_kpiresult", arr);
    }
    public void vUpdateHRDmstKPI(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_thrd_mst_kpi_upd", arr);
    }
    public void vInsertHRDKPITarget(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_thrd_kpi_target_ins", arr);
    }
    public void vInsertHRDKPITargetHO(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_thrd_kpi_target_insHO", arr);
    }
    public void vDeleteMstKPI(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_thrd_mst_kpi_del", arr);
    }
    public void vInsertMstKPI(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_thrd_mst_kpi_ins", arr);
    }
    public void vDelMstClaim(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_claim_ho_del", arr);
    }
    public void vUpdApproveClaim(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tclaim_approve_upd", arr);
    }
    public void vGetHRDMstEmployeeData(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_thrd_mstemployeeERP_search", ref rs, arr);
    }

    public void vInsertMstDOHO(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_do_insHO", arr);
    }
    public void vInsMastEmpDependent(List<cArrayList> arr, ref string retValue)
    {
        dal.vExecuteSP("sp_tacc_mst_EmpDependent_ins", arr, "@retValue", ref retValue);
    }
    public void vUpdateMastEmpDependent(List<cArrayList> arr, ref string retValue)
    {
        dal.vExecuteSP("sp_tacc_mst_EmpDependent_upd", arr, "@retValue", ref retValue);
    }
    public void vDeleteMastEmpDependent(List<cArrayList> arr)
    {
        dal.vExecuteSP("Sp_tacc_mst_EmpDependent_del", arr);
    }


    public void vInsMastEmp(List<cArrayList> arr, ref string retValue)
    {
        dal.vExecuteSP("sp_tacc_mst_Emp_ins", arr, "@retValue", ref retValue);
    }
    public void vUpdateMastEmp(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tacc_mst_Emp_upd", arr);
    }
    public void vDeleteMastEmp(List<cArrayList> arr)
    {
        dal.vExecuteSP("Sp_tacc_mst_Emp_del", arr);
    }

    public void vUpdateContractCancel(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_contract_can", arr);
    }
    public void vUpdateContractRet(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_contract_ret", arr);
    }
    public void vGetAsserData(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tacc_mst_assetQuery_get", ref rs, arr);
    }
    public void vBatchVat(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batchvat", arr);
    }
    public void vSyncClaim(string branch)
    {
        dal.vExecuteSP(branch);
    }
    public void vInsertWrkPaymentInvoice2(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_tpaymentinvoice_ins2", arr);
    }
    public void vGetDOSalesInvoiceByStatus(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tdosales_invoice_getbyinvno", ref rs, arr);
    }
    public void vSearchInvoiceByType(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tdosales_invoice_searchbytype", ref rs, arr);
    }
    public void vUpdateCashoutRequestByStatus(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcashout_request_updbystatus", arr);
    }
    public void vInsertCashoutRequest(List<cArrayList> arr, ref string sCashout)
    {
        dal.vExecuteSP("sp_tcashout_request_ins", arr, "@cashout_cd", ref sCashout);
    }
    public void vInsMastAsset(List<cArrayList> arr, ref string retValue)
    {
        dal.vExecuteSP("sp_tacc_mst_asset_ins", arr, "@assetno", ref retValue);
    }
    public void vUpdateMastAsset(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tacc_mst_asset_upd", arr);
    }
    public void vDeleteMastAsset(List<cArrayList> arr)
    {
        dal.vExecuteSP("Sp_tacc_mst_asset_del", arr);
    }

    public void vUpdateWrkDosalesDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_dosalesdtl_upd", arr);
    }
    public void vDelWrkDoSalesDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_dosalesdtl_del", arr);
    }
    public void vInsertWrkDosalesDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_dosalesdtl_ins", arr);
    }
    public void vInsertWrkDriver(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_driver_ins", arr);
    }
    public void vDelWrkDriver(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_driver_del", arr);
    }
    public void vGetdosalesdtlbyinvoice(ref SqlDataReader rs, List<cArrayList> arr)
    {
        dal.vGetRecordsetSP("sp_tdosales_dtl_getbyinvoice", ref rs, arr);
    }
    public void vUploadInvoiceUpd(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tclaim_inv_upload_upd", arr);
    }
    public void vGetClaimInvoice(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_claim_invoice_get", ref rs, arr);
    }
    public void vGetMapsSalesman(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_getmaps_salesman", ref rs, arr);
    }
    //public void vInsMastAsset(List<cArrayList> arr, ref string retValue)
    //{  
    //    dal.vExecuteSP("sp_tacc_mst_asset_ins", arr, "@assetno", ref retValue);
    //}
    //public void vUpdateMastAsset(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tacc_mst_asset_upd", arr);
    //}
    //public void vDeleteMastAsset(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("Sp_tacc_mst_asset_del", arr);
    //}

    public void vInsertAccTransaction(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tacc_transaction_ins", arr);
    }
    public void vDelMstProcess(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_process_del", arr);
    }
    public void vInsertMstProcess(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_process_ins", arr);
    }
    public void vGetProposalNewNo(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tproposalno_new", ref rs, arr);
    }
    public void vBatchCopyProposal(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batchcopyproposal", arr);
    }
    public void vDeleteItemCashoutInfo(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_titemcashout_info_del", arr);
    }
    public void vInsertItemCashoutInfo(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_titemcashout_info_upd", arr);
    }
    public void vDeleteAccMstCOA(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tacc_mst_coa_del", arr);
    }
    public void vInsertMapQuery(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmap_query_ins", arr);
    }

    public void vDeleteMapQuery(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmap_query_del", arr);
    }

    public void vUpdateExhibitMstItemCashout(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_texhibit_mstitemcashout_upd", arr);
    }
    public void vSearchAccMstCOA(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tacc_mst_coa_search", ref rs, arr);
    }
    public void vDelItemCashoutCOA(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_titemcashout_coa_del", arr);
    }
    public void vInsertItemCashoutCOA(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_titemcashout_coa_ins", arr);
    }
    public void vInserttdosalesinvoice_driver(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdosalesinvoice_driver_ins", arr);
    }
    public void vDelAccJournalMapping(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tacc_journal_mapping_del", arr);
    }
    public void vInsertAccJournalMapping(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tacc_journal_mapping", arr);
    }
    public void vInsertAccMstCOA(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tacc_mst_coa_ins", arr);
    }
    public void vGetProposalTRACK(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tproposal_get", ref rs, arr);
    }
    public void vGetClaimtracking(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("SP_TRACKINGCLAIM", ref rs, arr);
    }
    public void vGetAccCoaByParent(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tacc_mst_coa_getbyparent", ref rs, arr);
    }
    public void vInsertWrkBOR(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_bor_ins", arr);
    }
    public void vDelWrkBor(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_bor_del", arr);
    }


    public void vSearchMstCustomerContract(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_customer_search_contract", ref rs, arr);
    }

    public void vGetMstContraCT(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_invoiceinfoBA_get", ref rs, arr);
    }
    public void vUpdContract(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_contract_upd", arr);
    }
    public void vUpdateReqReturHo(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_treturnho_booking_upd", arr);
    }
    public void vInsertReturnHoBooking(List<cArrayList> arr, ref string sBooking)
    {
        dal.vExecuteSP("sp_treturnho_booking_ins", arr, "@ids", ref sBooking);
    }
    public void vBatchNotifyHOPayment(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batchsendnotifybookingpayment", arr);
    }
    public void vnotify_Password_expire()
    {
        dal.vExecuteSP("sp_notify_Password_expire");
    }
    //public void vInsertReturnHoBooking(List<cArrayList> arr,ref string sBookNo)
    //{
    //    dal.vExecuteSP("sp_treturnho_booking_ins", arr, "@IDS", ref sBookNo);
    //}
    public void vDelWrkFreeProduct2(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_freeproduct_del2", arr);
    }
    public void vSearchProposalByRemark(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_proposal_searchbyremark", ref rs, arr);
    }
    public void vDelPromotionGroup(List<cArrayList> arr, ref string retValue)
    {
        dal.vExecuteSP("sp_tmst_promotionFull_dlt", arr, "@retValue", ref retValue);
    }

    public void vInsPromotionGroup(List<cArrayList> arr, ref string retValue)
    {
        dal.vExecuteSP("sp_tmst_promotionFull_int", arr, "@retValue", ref retValue);
    }

    public void vInsPromotionType(List<cArrayList> arr, ref string retValue)
    {
        dal.vExecuteSP("sp_tpromotion_dtlFull_int", arr, "@retValue", ref retValue);
    }

    public void vDelPromotionType(List<cArrayList> arr, ref string retValue)
    {
        dal.vExecuteSP("sp_tmst_promotionTypeFull_dlt", arr, "@retValue", ref retValue);
    }

    public void vDelProposalSignByVendor(List<cArrayList> arr, ref string retValue)
    {
        dal.vExecuteSP("sp_tproposal_signbyvendorFull_dlt", arr, "@retValue", ref retValue);
    }

    public void vInsProposalPrincipal(List<cArrayList> arr, ref string retValue)
    {
        dal.vExecuteSP("sp_tproposal_signbyvendorFull_int", arr, "@retValue", ref retValue);
    }

    public void vInsProposalSBTCEmployee(List<cArrayList> arr, ref string retValue)
    {
        dal.vExecuteSP("sp_tproposal_signoffFull_int", arr, "@retValue", ref retValue);
    }

    public void vDelProposalSignBySBTC(List<cArrayList> arr, ref string retValue)
    {
        dal.vExecuteSP("sp_tproposal_signoffFull_dlt", arr, "@retValue", ref retValue);
    }

    public void vInsProposalStatus(List<cArrayList> arr, ref string retValue)
    {
        dal.vExecuteSP("sp_tmst_proposal_statusFull_int", arr, "@retValue", ref retValue);
    }

    public void vDelProposalStatus(List<cArrayList> arr, ref string retValue)
    {
        dal.vExecuteSP("sp_tmst_proposal_statusFull_dlt", arr, "@retValue", ref retValue);
    }

    public void vInsProposalIssue(List<cArrayList> arr, ref string retValue)
    {
        dal.vExecuteSP("sp_tmst_issueFull_int", arr, "@retValue", ref retValue);
    }

    public void vDelProposalIssue(List<cArrayList> arr, ref string retValue)
    {
        dal.vExecuteSP("sp_tmst_issueFull_dlt", arr, "@retValue", ref retValue);
    }
    public void vBatchCopyDicount(ref string sSysNo, List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batchcopydiscount", arr, "@sysno", ref sSysNo);
    }
    public void vUpdatetblarcnbystatus(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tblarcn_updbystatus", arr);
    }

    public void vDelContract(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_contract_del", arr);
    }
    public void vInsertFieldValue(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tfield_value_ins", arr);
    }
    public void vInsertMtnMstVehicle(List<cArrayList> arr, ref string vhccode)
    {
        dal.vExecuteSP("sp_tmtn_mstvehicle_ins", arr, "@vhc_cd", ref vhccode);
    }
    public void vGetTopNews(ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_gettopnews", ref rs);
    }
    public void vDelWrkFreeItem2(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_freeitem_del2", arr);
    }
    public void vInsertWrkFreeProduct2(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_freeproduct_ins2", arr);
    }
    public void vInsertProposalFreeItem(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tproposal_freeitem_ins", arr);
    }
    public void vInsertProposalFreeProduct(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tproposal_freeproduct_ins", arr);
    }
    //
    // Claim Automation 

    public void vGetDiscountList(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_auto_discount_get", ref rs, arr);
    }
    public void vGetInvoiceList(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_auto_invoice_get", ref rs, arr);
    }
    public void vGetCashoutList(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_auto_cashout_get", ref rs, arr);
    }
    public void vGetCNDNList(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_auto_cndn_get", ref rs, arr);
    }
    public void vGetBAList(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_auto_ba_get", ref rs, arr);
    }
    public void vGetSwitcher(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_switcherapp", ref rs, arr);
    }
    public void vGetMtnMstVehicle(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmtn_mstvehicle_get", ref rs, arr);
    }
    public void vInserttargetxls(List<cArrayList> arr, ref string trg)
    {
        dal.vExecuteSP("sp_tsalestargetho_excel_ins", arr, "@target_cd", ref trg);
    }
    public void vBatchSendEmailProdSpv(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_getinvprodspv", arr);
    }
    public List<string> lAppProdSpv(string sReturNo)
    {
        List<string> lTemp = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        SqlDataReader rs = null;
        arr.Add(new cArrayList("@retur_no", sReturNo));
        dal.vGetRecordsetSP("sp_getreturnprodspv", ref rs, arr);
        while (rs.Read())
        {
            lTemp.Add(rs.GetValue(0).ToString());
        }
        rs.Close();
        return lTemp;

    }
    public void vUpdateControlParameter(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcontrol_parameter_upd", arr);
    }
    public string GetSupervisoor()
    {

        SqlDataReader rs = null; string sTemp = "N/A";

        dal.vGetRecordsetSP("sp_getSupervisoor", ref rs);

        while (rs.Read())
        {

            sTemp = rs["fullname"].ToString() + " [" + rs["mobile_no"].ToString() + "]";

        }
        rs.Close(); return (sTemp);

    }
    public void vDelPoDtl3(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tpo_dtl_del", arr);
    }
    public void nCheckDriverKPI(List<cArrayList> arr, ref string driverCount)
    {
        dal.vExecuteSP("sp_tmst_appraisal_checkDriverKPI", arr, "@cnt", ref driverCount);
    }
    public void vInsertIncentiveTargetDriver(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_ttarget_driverIncentive_ins", arr);
    }
    public void vDelDriverTarget(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_ttarget_driver_del", arr);
    }
    public double dEISGetCustomerBuy(List<cArrayList> arr)
    {
        double dTemp = 0;
        SqlDataReader rs = null;
        dal.vGetRecordsetSP("sp_eis_getcustomerbuy", ref rs, arr);
        while (rs.Read())
        {
            dTemp = Convert.ToDouble(rs.GetValue(0));
        }
        rs.Close();
        return dTemp;
    }
    public void vExecuteSQL(string sSQL)
    {
        dal.vExecuteSQL(sSQL);
    }
    public void vInsertClaimdtlcontract(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tclaimdtl_contract_ins", arr);
    }
    public void vBatchZeroInvFromDiscount(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batchzeroinvcausediscount", arr);
    }
    public void vInsertExhibitOtherOrder(List<cArrayList> arr, ref string sNo)
    {
        dal.vExecuteSP("sp_texhibition_otherorder_ins", arr, "@otherorder_cd", ref sNo);
    }
    public void vInsertWrkExhibitOtherOrder(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_exhibitotherorder_ins", arr);
    }
    public void vDelWrkExhibitOtherOrder(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_exhibitotherorder_del", arr);
    }
    public void vGetHRDMstEmployeeByQry(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_thrd_mstemployee_getbyqry", ref rs, arr);
    }
    public void vRptSalesBranches(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_rpt_salesbranches", ref rs, arr);
    }
    public void vInsertWrkExhibitCashoutFromCore(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_exhibitcashout_insfromcore", arr);
    }
    public void vGetHRDMstEmployee(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_thrd_mstemployee_get", ref rs, arr);
    }
    public void vSearchHRDMstEmployee(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_thrd_mstemployee_search", ref rs, arr);
    }
    public void vBatchExhibitInitBalance(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batchexhinitbalance", arr);
    }
    public void vInsertExhibitCashout(List<cArrayList> arr, ref string sNo)
    {
        dal.vExecuteSP("sp_texhibit_cashout_ins", arr, "@cashout_cd", ref sNo);
    }
    public void vInsertWrkExhibitCashOut(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_exhibitcashout_ins", arr);
    }

    public void vDeleteWrkExhibitCashOut(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_exhibitcashout_del", arr);
    }
    public void vInsertExhibitMstItemCashout(List<cArrayList> arr, ref string sNo)
    {
        dal.vExecuteSP("sp_texhibit_mstitemcashout_ins", arr, "@itemco_cd", ref sNo);
    }
    public void vInsertExhibitMstItemCashout(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_texhibit_mstitemcashout_ins", arr);
    }
    public void vGetMaps(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_getmaps", ref rs, arr);
    }
    public void vInsertTFreebyItem()
    {
        dal.vExecuteSP("SP_BATCHDISCFREEBYITEM");
    }
    public void vInsertTclosinglog(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_closing_log_ins", arr);
    }
    public void vGetExhibitMstDiscount(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_exhibition_mstdiscount_getbydisc", ref rs, arr);
    }
    public void vInsertWrkExhibitionStockInDtlFromCore(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_texhibition_stockindtl_insfromcore", arr);
    }
    public void vInsertWrkExhibitionBoothFromCore(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_texhibition_booth_insfromcore", arr);
    }
    public void vDeleteWrkExhibitioBooth(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_exhibitionbooth_del", arr);
    }
    public void vInsertCcnr(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tccnr_ins", arr);
    }

    public void vGetMstOutsource(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_outsource_get", ref rs, arr);
    }
    public void vBatchExhAdvancedDate(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batchexhadvanceddate", arr);
    }
    public void vSearchMstItemByExhibitionBooth(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_item_searchbyprodbooth", ref rs, arr);
    }
    public void vSearchMstItemByProd(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_item_searchbyprod", ref rs, arr);
    }
    public void vInsertMstOutsource(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_outsource_ins", arr);
    }
    // Insert Adjustment Date Proposal
    public void vInsertAdjustmentProposal(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tproposal_adjustment_ins", arr);
    }

    // Update Adjustment Date Proposal
    public void vUpdateAdjustmentProposal(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tproposal_adjustment_upd", arr);
    }
    // Update Background Proposal 
    public void vUpdateBgProposal(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tproposal_background_upd", arr);
    }

    // Insert proposal Document
    public void vInsertTproposalDoc(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tproposal_document_ins", arr);
    }

    // Get Budget Calculation MTD / YTD 

    public void vGetBudgetMtdYtd(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tproposal_budget_record", ref rs, arr);
    }

    // Update Proposal Status 
    public void vUpdateStatusProposal(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tproposal_status_ins", arr);
    }
    // Claim HO

    // search item by proposal
    public void vSearchItembyProposal(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_item_search", ref rs, arr);
    }

    public void vInsertMstClaimDetail(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_claim_ho_dtl_ins", arr);
    }

    public void vDelMstClaimDetail(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_claim_ho_dtl_del", arr);
    }

    public void vInsertMstClaimHO(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_claim_ho_ins", arr);
    }

    public void vUpdateMstClaimHO(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_claim_ho_upd", arr);
    }

    public void vUpdatetMstClaimNo(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_claim_ho_updall", arr);
    }

    public void vSearchMstClaimHO(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_claim_ho_search", ref rs, arr);
    }

    public void vInsertClaimSalesDepartment(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tclaim_sales_ins", arr);
    }

    public void vGetMstClaimHO(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_claim_ho_get", ref rs, arr);
    }

    public void vGetMstClaimViewHO(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_claim_ho_view_get", ref rs, arr);
    }

    public void vSearchMstPropClaim(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_claim_proposal_search", ref rs, arr);
    }

    public void vInsertClaimVendor(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tclaim_vendor_approval_ins", arr);
    }

    public void vSearchClaimVendor(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tclaim_vendor_approval_search", ref rs, arr);
    }

    public void vInsertClaimVendorDepartment(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tclaim_vendor_ins", arr);
    }

    public void vInsertClaimSendPrincipal(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_send_claim_vendor", arr);
    }

    public void vDelWrkClaimSendPrincipal(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_send_claim_vendor_del", arr);
    }

    public void vSearchMstProposal2(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_proposal_search2", ref rs, arr);
    }

    public void vGetMstClaimViewHeader(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tclaim_header_view_get", ref rs, arr);
    }

    public void vGetClaimVendor(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_claim_vendor_get", ref rs, arr);
    }

    public void vBindingListToSp(ref ListBox lst, string sSPName, string sVal, string sDisp, List<cArrayList> arr)
    {
        SqlDataReader rs = null;
        DataTable dta = new DataTable();
        dal.vGetRecordsetSP(sSPName, ref rs, arr);
        dta.Load(rs);
        lst.DataValueField = sVal;
        lst.DataTextField = sDisp;
        lst.DataSource = dta.DefaultView;
        lst.DataBind();
    }

    public void vBindingListToSp(ref ListBox lst, string sSPName, string sVal, string sDisp)
    {
        SqlDataReader rs = null;
        DataTable dta = new DataTable();
        dal.vGetRecordsetSP(sSPName, ref rs);
        dta.Load(rs);
        lst.DataValueField = sVal;
        lst.DataTextField = sDisp;
        lst.DataSource = dta.DefaultView;
        lst.DataBind();
    }

    public void vBindingFieldValueToComboAll(ref DropDownList cbo, string sParamName)
    {
        SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@fld_nm", sParamName));
        DataTable dta = new DataTable();
        dal.vGetRecordsetSP("sp_tfield_value_get3", ref rs, arr);
        dta.Load(rs);
        cbo.DataValueField = "fld_valu";
        cbo.DataTextField = "fld_note";
        cbo.DataSource = dta.DefaultView;
        cbo.DataBind();
    }

    public void vInsClaimDoc(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tclaim_doc_ins", arr);
    }

    public void vInsReturnClaim(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tclaim_return", arr);
    }
    public void vInsRejectClaim(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tclaim_reject", arr);
    }

    public void vInsApproveClaim(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tclaim_approve", arr);
    }

    public void vGetPaymentDN(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tclaim_pay_dn", ref rs, arr);
    }

    public void vInsClaimPayment(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tclaim_pay", arr);
    }

    public void vInsClaimPaymentDN(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tclaim_pay_dn_ins", arr);
    }

    public void vUpdClaimPayment(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tclaim_pay_upd", arr);
    }

    public void vUpdateClaimData(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_claim_adj", arr);
    }

    public void vUpdateClaimDetail(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tclaim_dtl_adj", arr);
    }

    public void vGetClaimBy(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_claim_ho_get", ref rs, arr);
    }

    public void vUpdateClaimDoc(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tclaim_doc_upd", arr);
    }

    public void vInsertPaymentClaimDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tclaim_payment_dtl_ins", arr);
    }

    public void vDeletePaymentClaimDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tclaim_payment_dtl_del", arr);
    }

    public void vUpdatePaymentClaimDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tclaim_payment_dtl_upd", arr);
    }

    public void vDeleteClaimDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tclaim_header_dtl_del", arr);
    }

    public void vUpdateCoverLetter(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tclaim_header_upd", arr);
    }

    public void vEditPaymentClaimDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tclaim_payment_dtl_edit", arr);
    }

    public void vDelPaymentClaimDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tclaim_payment_dtl_del2", arr);
    }
    // Claim HO
    // Add Feature For Proposal

    public void vGetBudgetLimitItem(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tproposal_item_get", ref rs, arr);
    }

    public void vGetBudgetLimitProduct(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tproposal_product_get", ref rs, arr);
    }

    // Tmst Contract 

    //public void vUpdContract(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tmst_contract_upd", arr);
    //}

    //public void vSearchMstCustomerContract(List<cArrayList> arr, ref SqlDataReader rs)
    //{
    //    dal.vGetRecordsetSP("sp_tmst_customer_search_contract", ref rs, arr);
    //}

    //public void vDelContract(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tmst_contract_del", arr);
    //}

    public void vUpdateContractApp(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_contract_app", arr);
    }

    public void vUpdateContractRej(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_contract_rej", arr);
    }

    //public void vDelContractItem(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tcontract_item_del", arr);
    //}

    //public void vDelContractProduct(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tcontract_product_del", arr);
    //}

    //public void vDelContractFreeProduct(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tcontract_freeproduct_del", arr);
    //}

    //public void vDelContractFreeItem(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tcontract_freeitem_del", arr);
    //}

    //public void vSearchMstProposal2(List<cArrayList> arr, ref SqlDataReader rs)
    //{
    //    dal.vGetRecordsetSP("sp_tmst_proposal_search2", ref rs, arr);
    //}

    //public void vSearchMstCustomer(List<cArrayList> arr, ref SqlDataReader rs)
    //{
    //    dal.vGetRecordsetSP("sp_tmst_customer_search", ref rs, arr);
    //}

    //public void vSearchMstItemBySalespoint(List<cArrayList> arr, ref SqlDataReader rs)
    //{
    //    dal.vGetRecordsetSP("sp_tmst_item_searchbysalespoint2", ref rs, arr);
    //}

    public void vInsertContractDR(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcontract_displayrent_ins", arr);
    }

    public void vInsertContractSB(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcontract_signboard_ins", arr);
    }

    public void vInsertContractTB(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcontract_tactical_ins", arr);
    }

    public void vGetContractNo(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_getsystemno", ref rs, arr);
    }
    public void vGetContract(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_lookup_contract", ref rs, arr);
    }

    //public void vInsertMstContract(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tmst_contract_ins", arr);
    //}

    //public void vInsertContractCustomer(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tcontract_customer_ins", arr);
    //}

    //public void vInsertContractCusgrcd(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tcontract_cusgrcd_ins", arr);
    //}

    //public void vUpdateContractItem(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_contract_item_upd", arr);
    //}

    //public void vUpdateContractProduct(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tcontract_product_upd", arr);
    //}

    //public void vUpdateContractFreeitem(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_contract_freeitem_upd", arr);
    //}

    //public void vUpdateContractFreeproduct(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tcontract_freeproduct_upd", arr);
    //}

    //public void vUpdateContractPaySchedule(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tcontract_payschedule_upd", arr);
    //}

    //public void vInsertContractProduct(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tcontract_product_ins", arr);
    //}

    //public void vInsertContractPaySchedule(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tcontract_payschedule_ins", arr);
    //}

    //public void vInsertContractFreeItem(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_contract_freeitem_ins", arr);
    //}

    //public void vInsertContractFreeProduct(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tcontract_freeproduct_ins", arr);
    //}

    public void vUpdateContractDate(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcontract_payschedule_postpone", arr);
    }

    public void vInsertContractPayment(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcontract_payment_ins", arr);
    }

    public void vInsertContractContact(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcontract_contact_ins", arr);
    }

    public void vUpdateContractDoc(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_contract_file", arr);
    }

    public void vInsertContractDriverRcp(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcontract_payment_dtl_ins", arr);
    }

    public void vInsertContractCustRcp(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcontract_payment_dtl_upd", arr);
    }
    // Tmst Contract 

    // Add feature Proposal Customer Exclude 

    public void vDelProposalCustomerEx(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tproposal_customer_ex_del", arr);
    }
    public void vInsertProposalCustomerEx(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tproposal_customer_ex_ins", arr);
    }

    // Add Feature Proposal Customer Exclue 
    public void vGetExhibitionOrder(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_texhibition_order_get", ref rs, arr);
    }
    public void vDeleteWrkExhibitSalesDisc(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_exhibitsalesdisc_del", arr);
    }
    public void vInsertExhibitOrder(List<cArrayList> arr, ref string sNo)
    {
        dal.vExecuteSP("sp_texhibition_order_ins", arr, "@exhso_cd", ref sNo);
    }
    public void vBatchExhibitDiscCalc(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batchexhibitdisccalc", arr);
    }
    public void vInsertExhibitInternalTransfer(List<cArrayList> arr, ref string sNo)
    {
        dal.vExecuteSP("sp_texhibit_internaltransfer_ins", arr, "@trfno", ref sNo);
    }
    public void vDeleteWrkExhibitInternalTrf(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_exhibitinternaltrf_del", arr);
    }

    public void vSearchMstItemByExhibitionInit(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_item_searchbyexhibitinit", ref rs, arr);
    }
    public void vSearchMstItemByExhibition(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_item_searchbyexhibit", ref rs, arr);
    }
    public void vInsertExhibitionMstDiscount(List<cArrayList> arr, ref string sDiscCode)
    {
        dal.vExecuteSP("sp_texhibition_mstdiscount_ins", arr, "@disc_cd", ref sDiscCode);
    }
    public void vDeleteWrkExhibitionDiscItem(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_exhibitdiscitem_del", arr);
    }
    public void vInsertWrkExhibitDiscItem(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_exhibitdiscitem_ins", arr);
    }
    public void vDeleteWrkExhibitSales(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_exhibitsales_del", arr);
    }
    public void vInsertWrkExhibitSales(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_exhibitsales_ins", arr);
    }
    public void vInsertWrkExhibitionDiscFormula(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_exhibitiondiscformula_ins", arr);
    }
    public void vDeleteWrkExhibitionDiscFormula(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_exhibitiondiscformula_del", arr);
    }
    public void vInsertWrkExhibitionInternalTrf(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_exhibitinternaltrf_ins", arr);
    }
    public void vDeleteWrkExhibitionStockIn(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_exhibitionstockin_del", arr);
    }
    public void vInsertExhibitionStockIn(List<cArrayList> arr, ref string sNo)
    {
        dal.vExecuteSP("sp_texhibition_stockin_ins", arr, "@stockin_cd", ref sNo);
    }
    public void vInsertExhibitPrice(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_texhibition_price_ins", arr);
    }
    public void vInsertWrkExhibitionStockIn(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_exhibitionstockin_ins", arr);
    }
    public void vInsertWrkExhibitioBooth(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_exhibitionbooth_ins", arr);
    }

    public void vDeleteWrkExhibitionBooth(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_exhibitionbooth_del", arr);
    }
    public void vInsertExhibitionBooth(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_texhibition_booth_ins", arr);
    }

    public void vInsertMstExhibition(List<cArrayList> arr, ref string sNo)
    {
        dal.vExecuteSP("sp_tmst_exhibition_ins", arr, "@exhibit_cd", ref sNo);
    }

    public void vGetMstExhibition(ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_exhibition_get", ref rs);
    }

    public void vGetMstExhibition(ref SqlDataReader rs, List<cArrayList> arr)
    {
        dal.vGetRecordsetSP("sp_tmst_exhibition_get", ref rs, arr);
    }
    public decimal fn_getachievedriver(string sIDS, string sPeriod, string sEmpCode)
    {
        decimal _achieved = 0;
        List<cArrayList> arr = new List<cArrayList>();
        SqlDataReader rs = null;
        arr.Add(new cArrayList("@ids", sIDS));
        arr.Add(new cArrayList("@period", sPeriod));
        arr.Add(new cArrayList("@emp_cd", sEmpCode));
        dal.vGetRecordsetSP("sp_getdriverachieved", ref rs, arr);
        while (rs.Read())
        {
            _achieved = Convert.ToDecimal(rs[0]);
        }
        rs.Close();


        return (_achieved);
    }
    public decimal fn_gettargetdriver(string sIDS, string sPeriod, string sEmpCode)
    {
        decimal _target = 0;
        List<cArrayList> arr = new List<cArrayList>();
        SqlDataReader rs = null;
        arr.Add(new cArrayList("@ids", sIDS));
        arr.Add(new cArrayList("@period", sPeriod));
        arr.Add(new cArrayList("@emp_cd", sEmpCode));
        dal.vGetRecordsetSP("sp_gettargetdriver", ref rs, arr);
        while (rs.Read())
        {
            _target = Convert.ToDecimal(rs[0]);
        }
        rs.Close();


        return (_target);
    }
    public void vSearchMstCustomerInRPS(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_customerInRPS_search", ref rs, arr);
    }
    public void vDeletetdocblockcustomer_cust_cd_exclude(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdocblockcustomer_cust_cd_exclude_del", arr);
    }
    public void vInserttdocblockcustomer_cust_cd(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdocblockcustomer_cust_cd_exclude_ins", arr);
    }
    public void vSearchtdocblockcustomer_cust(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tdocblockcustomer_cust_search", ref rs, arr);
    }
    public void vDeletetdocblockcustomer_custcate_cd(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdocblockcustomer_custcate_cd_del", arr);
    }
    public void vInserttdocblockcustomer_otlcd(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdocblockcustomer_otlcd_ins", arr);
    }
    public void vInserttdocblockcustomer_doc_cd(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdocblockcustomer_doc_cd_ins", arr);
    }
    public void vInserttdocblockcustomer_custcate_cd(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdocblockcustomer_custcate_cd_ins", arr);
    }
    public void vInserttrps_bydate(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_trps_bydate_ins", arr);
    }
    // Claim Confirm
    public void vInsertClaimConfirm(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tclaimconfirm_ins", arr);
    }

    public void vInsertTWRKClaimConfirm(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrkclaimconfirm_ins", arr);
    }

    public void vInsertTclaimExclude(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tclaimexclude_ins", arr);
    }

    public void vGetMstinvoice(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_invoiceinfo_get", ref rs, arr);
    }

    public void vInsertWrkClaimExclude(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_claimexclude_ins", arr);
    }

    public void vDelWrkClaimExclude(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_claimexclude_del", arr);
    }

    public void vUpdateWrkClaimExclude(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_claimexclude_upd", arr);
    }
    public void vUploadInvoice(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tclaim_inv_upload", arr);
    }
    // Claim Confirm 

    // Claim Automation 




    public void vDelContractItem(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcontract_item_del", arr);
    }

    public void vDelContractProduct(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcontract_product_del", arr);
    }

    public void vDelContractFreeProduct(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcontract_freeproduct_del", arr);
    }

    public void vDelContractFreeItem(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcontract_freeitem_del", arr);
    }

    public void vSearchMstCustomerMoroc(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_customer_searchmoroc", ref rs, arr);
    }
    public void vSearchMstCustomer(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_customer_search", ref rs, arr);
    }

    public void vSearchMstItemBySalespoint2(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_item_searchbysalespoint2", ref rs, arr);
    }


    public void vInsertMstContract(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_contract_ins", arr);
    }

    public void vInsertContractCustomer(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcontract_customer_ins", arr);
    }

    public void vInsertContractCusgrcd(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcontract_cusgrcd_ins", arr);
    }

    public void vUpdateContractItem(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_contract_item_upd", arr);
    }

    public void vUpdateContractProduct(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcontract_product_upd", arr);
    }

    public void vUpdateContractFreeitem(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_contract_freeitem_upd", arr);
    }

    public void vUpdateContractFreeproduct(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcontract_freeproduct_upd", arr);
    }

    public void vUpdateContractPaySchedule(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcontract_payschedule_upd", arr);
    }

    public void vInsertContractProduct(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcontract_product_ins", arr);
    }

    public void vInsertContractPaySchedule(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcontract_payschedule_ins", arr);
    }

    public void vInsertContractFreeItem(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_contract_freeitem_ins", arr);
    }

    public void vInsertContractFreeProduct(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcontract_freeproduct_ins", arr);
    }


    public void vSearchDiscountByRemark(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_discount_searchbyremark", ref rs, arr);
    }
    public void vInserttcustomerdocblock_doc_cd(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcustomerdocblock_doc_cd_ins", arr);
    }
    public void vInsertbookingSanad(List<cArrayList> arr)
    {

        dal.vExecuteSP("sp_ins_tbookingsanad_ho", arr);
    }
    public void vUpdatebookingSanad(List<cArrayList> arr)
    {

        dal.vExecuteSP("UpdateSanadBookingHO", arr);
    }

    public void vDeletebookingSanad(List<cArrayList> arr)
    {

        dal.vExecuteSP("sp_dlt_tbookingsanad_ho", arr);
    }



    public void vInsertSanadBookingBranch(List<cArrayList> arr, ref string bookingSanad)
    {

        dal.vExecuteSP("InsertSanadBookingBranch", arr, "@SanadBookingBranchNo", ref bookingSanad);
    }




    public void vUpdateSanadBookingBranch(List<cArrayList> arr)
    {

        dal.vExecuteSP("UpdateSanadBookingBranch", arr);
    }
    public void ReceiveSanadBookingByBranch(List<cArrayList> arr)
    {
        dal.vExecuteSP("ReceiveSanadBookingByBranch", arr);
    }
    public void vInsertSanadBookingEmployee(List<cArrayList> arr, ref string bookingSanad)
    {
        dal.vExecuteSP("InsertSanadBookingEmployee", arr, "@SanadBookingEmployeeNo", ref bookingSanad);
    }


    public void vGetSanadBookingBranchNewStatus(List<cArrayList> arr, ref SqlDataReader rs)
    {

        dal.vGetRecordsetSP("Get_SanadBookingBranch_NewStatus", ref rs, arr);
    }

    //public int vInsertEquipmente(List<cArrayList> arr)
    //{
    //    //dal.vExecuteSP("InsertSanadBookingEmployee", arr, "@SanadBookingEmployeeNo", ref bookingSanad);
    //    int ret =0;
    //    DataTable dt = new DataTable();
    //    dt = dal.GetValueFromSP("sp_inst_tmst_equipment", arr);
    //    if (dt.Rows.Count > 0) {
    //        return Convert.ToInt32(dt.Rows[0][0]);
    //    }
    //    else { return ret; }
    //}


    public void vDelCanvasOrderItemLoad(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcanvasorder_itemload_del", arr);
    }
    public void vInsertCanvasOrderItemLoad(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcanvasorder_itemload_ins", arr);
    }
    public void vInsertDosalesInvoiceLedger(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdosalesinvoice_ledger_ins", arr);
    }
    public void vDelCustomerBlocked(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcustomer_blocked_del", arr);
    }
    public void vInsertCustomerBlock(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcustomer_blocked_ins", arr);
    }
    public void vInsertMstGroupCreditLimist(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_groupcreditlimit_ins", arr);
    }
    public void vInsertWrkAppraisal(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_appraisal_ins", arr);
    }
    public void vDelWrkAppraisal(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_appraisal_del", arr);
    }
    public string sGetFieldValue(string sFldName, string sFldValue)
    {
        string stemp = string.Empty;
        stemp = vLookUp("select fld_desc from tfield_value where fld_nm='" + sFldName + "' and fld_valu='" + sFldValue + "'");
        return stemp;
    }
    public void vUpdatetmst_returho_approve(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_returho_approve_upd", arr);
    }
    public void vInsertItemPosMaterial(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_itemposmaterial_ins", arr);
    }
    public void vclosingInternalTransferLastMonth(ref SqlDataReader rs, List<cArrayList> arr)
    {
        dal.vGetRecordsetSP("sp_closingInternalTransferLastMonth", ref rs, arr);
    }
    public void vInsertAppraisalJobTitle(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tappraisal_jobtitle_ins", arr);
    }

    public List<string> lGetApprovalall(string doc_typ)
    {
        SqlDataReader rs = null;
        List<string> lTemp = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@doc_typ", doc_typ));
        dal.vGetRecordsetSP("sp_tapprovalpattern_get", ref rs, arr);
        while (rs.Read())
        {
            lTemp.Add(rs["mobile_no"].ToString());
            lTemp.Add(rs["email"].ToString());
        }
        rs.Close();
        return (lTemp);
    }
    public void vUpdateccnno(List<cArrayList> arr)
    {
        dal.vExecuteSP("SP_TCLAIM_CCNT_UPD", arr);
    }
    public void vBatchCleanSmsOutbox(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batchcleansmsoutbox", arr);
    }
    public void vUpdatetinternal_transfer_approve(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tinternal_transfer_approve_upd", arr);
    }
    public void vInsertTargetDriver(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_ttarget_driver_ins", arr);
    }
    public void vInsertClaimdtlcashout(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tclaimdtl_cashout_ins", arr);
    }
    public void vInsertClaimdtlcndn(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tclaimdtl_cndn_ins", arr);
    }
    //
    public void vInsertProposalPaid(List<cArrayList> arr, ref string sClaimNo)
    {
        dal.vExecuteSP("tproposal_paid_ins", arr, "@claimco_cd", ref sClaimNo);
    }
    public void vGetProposalPayment(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tproposal_payment_get", ref rs, arr);
    }
    public void vHandledError(ref Exception ex, string sSource)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@err_source", sSource));
            arr.Add(new cArrayList("@err_description", ex.ToString()));
            vInsertErrorLog(arr);
        }
        catch (Exception)
        {
            HttpContext.Current.Response.Redirect("fm_loginol.aspx");
        }
    }
    public void vGetProposalPaymentByDate(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tproposal_payment_getbydate", ref rs, arr);
    }
    public void vBatchNotifyBadStock()
    {
        dal.vExecuteSP("sp_batchnotifybadstock");
    }
    public void vDelMstAppraisal(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_appraisal_del", arr);
    }
    public void vInsertMstAppraisal(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_appraisal_ins", arr);
    }


    public void vDeletetcustomerdocblock_cust_cd(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcustomerdocblock_cust_cd_del", arr);
    }
    public void vInserttcustomerdocblock_cust_cd(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcustomerdocblock_cust_cd_ins", arr);
    }
    public void vInserttcustomerdocblock_cuscate_cd(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcustomerdocblock_cuscate_cd_ins", arr);
    }
    public void vSearchtcustomerdocblock_cust(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tcustomerdocblock_cust_search", ref rs, arr);
    }
    public void vDeletetcustomerdocblock_cusgrcd(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcustomerdocblock_cusgrcd_del", arr);
    }
    public void vInserttcustomerdocblock_cusgrcd(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcustomerdocblock_cusgrcd_ins", arr);
    }
    public void vDeletetcustomerdocblock_otlcd(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcustomerdocblock_otlcd_del", arr);
    }
    public void vInserttcustomerdocblock_otlcd(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcustomerdocblock_otlcd_ins", arr);
    }
    public void vGetClaimCNDN(List<cArrayList> arr, ref System.Data.SqlClient.SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tclaim_reqcndn_get", ref rs, arr);
    }
    public void vInsertClaimCNDN(List<cArrayList> arr, ref string sClaimCN)
    {
        dal.vExecuteSP("sp_tclaim_reqcndn_ins", arr, "@cndn_cd", ref sClaimCN);
    }
    public void vDelWrkClaimCNDN(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_claimcndn_del", arr);
    }
    public void vInsertWrkClaimCNDN(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_claimcndn_ins", arr);
    }

    public void vGetDiscountNo(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_getsystemno", ref rs, arr);
    }

    public void vInsertDiscSalespoint(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdisc_salespoint_ins", arr);
    }

    public void vInsertDiscCustomer(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdisc_customer_ins", arr);
    }

    public void vInsertDiscCusgrcd(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdisc_cusgrcd_ins", arr);
    }

    public void vInsertDiscCustype(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdisc_custtype_ins", arr);
    }

    public void vInsertDiscItem(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdisc_item_ins", arr);
    }

    public void vInsertDiscProduct(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdisc_product_ins", arr);
    }

    public void vInsertMstDiscount(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_discount_ins", arr);
    }

    public void vUpdateMstDiscount4(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_discount_upd4", arr);
    }

    public void vGetProductBranded(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_product_branded", ref rs, arr);
    }
    public void vSearchMstCustomerDisc(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_customer_searchdisc", ref rs, arr);
    }

    public void vInsertReqClaimCNDN(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tclaim_reqcndb_ins", arr);
    }
    public void vInsertItemBlockOtlCd(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_titemblock_otlcd_ins", arr);
    }
    public void vInsertEmailOutbox(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_temail_outbox_ins", arr);
    }
    public void vUpdateItemBlockCusgrcd(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_titemblock_cusgrcd_upd", arr);
    }
    public void vInsertItemBlockCusgrcd(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_titemblock_cusgrcd_ins", arr);
    }
    public void vUpdateDosalesInvoiceByStatus(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdosales_invoice_updbystatus", arr);
    }
    public void vGetDosalesInvoiceByReturnFull(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tdosales_invoice_getbyreturn", ref rs, arr);
    }
    public void vBindingRepeaterToSp(ref Repeater rpt, string sSPName)
    {
        SqlDataReader rs = null;
        dal.vGetRecordsetSP(sSPName, ref rs);
        DataTable dta = new DataTable();
        dta.Load(rs);
        rpt.DataSource = dta.DefaultView;
        rpt.DataBind();
    }
    public void vGettprod_support_sms_pic(ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tprod_support_sms_pic_get", ref rs);
    }
    public void vDeletetprodsupport_document(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tprodsupport_document_del", arr);
    }
    public void vUpdatetprodsupport_document(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tprodsupport_document_upd", arr);
    }
    public void vInserttprodsupport_document(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tprodsupport_document_ins", arr);
    }
    public void vUpdatetprod_support(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tprod_support_upd", arr);
    }
    public void vGettprod_support(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tprod_support_get", ref rs, arr);
    }
    public void vInserttprod_support(List<cArrayList> arr, ref string ssup_no)
    {
        dal.vExecuteSP("sp_tprod_support_ins", arr, "@sup_no", ref ssup_no);
    }
    public void vUpdateTabInternalTransferByTrfDate(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_ttab_internaltransfer_updbytrfdate", arr);
    }
    public void vInsertRPSCustomVisit(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_trps_customvisit_ins", arr);
    }
    public void vDelMstHoliday(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_holiday_del", arr);
    }
    public void vInsertMstHoliday(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_holiday_ins", arr);
    }
    public void vBatchStartup(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batchstartup", arr);
    }
    public void vInsertPaymentPromised(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tpayment_promised_upd", arr);
    }
    // Insert Adjustment Date Proposal

    public void vInsertAdjustmentPriceCusttype(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tadjustmentprice_custtype_ins", arr);
    }
    public void vGetPaymentPromised(ref SqlDataReader rs, List<cArrayList> arr)
    {
        dal.vGetRecordsetSP("sp_tpayment_promised_get", ref rs, arr);
    }
    public void vBatchCopyOrder(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batchcopyorder", arr);
    }
    public void vBatchwazaran_year()
    {
        dal.vExecuteSP("sp_batch_wazaran_year");
    }
    public void vBatchBackupDBYearly()
    {
        dal.vExecuteSP("sp_batchbackupdbyearly");
    }
    public void vInsertWrkPrintAll(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_printall_ins", arr);
    }
    public void vDelWrkPrintAll(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_printall_del", arr);
    }
    public void vUpdatePaymentPromised(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tpayment_promised_upd", arr);
    }
    public void vcheckstockminus(ref SqlDataReader rs, List<cArrayList> arr)
    {
        dal.vGetRecordsetSP("sp_checkstockminus", ref rs, arr);
    }
    public void vGetCustomer(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_lookup_customer1", ref rs, arr);
    }

    public void vInsertPaymentPromise(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tpayment_promise_ins", arr);
    }

    public void vUpdPaymentPromise(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tpayment_promise_upd", arr);
    }

    public void vInsertPaymentPromised(List<cArrayList> arr, ref string sOut)
    {
        dal.vExecuteSP("sp_tpayment_promised_ins", arr, "@promised_cd", ref sOut);
    }
    public void vSearchdriver(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_employee_searcbydrv", ref rs, arr);
    }
    public void vinsertpaymentrtdiscount(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_payment_ins_rtdisc", arr);
    }
    public void vUpdateMstPaymentrt(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_payment_upd1", arr);
    }

    public void vUpdateWrkSubOrder(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_suborder_upd", arr);
    }
    public void vDelWrkSubOrder(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_suborder_del", arr);
    }
    public void vInsertWrkSubOrderFromCore(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_suborder_insfromcore", arr);
    }
    public void vInsertWrkProposalPaid(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_proposalpaid_ins", arr);
    }
    public void vDelWrkProposalPaid(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_proposalpaid_del", arr);
    }
    public void vBatchApprovedPanda()
    {
        dal.vExecuteSP("sp_batchapprovepanda");
    }
    public void vInsertSmsOutbox(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsms_outbox_ins", arr);
    }
    public void vDelWrkPaymentInvoice(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_paymentinvoice_del", arr);
    }
    public void vUpdateMstPaymentByDeleted(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_payment_updbydeleted", arr);
    }
    public void vUpdatePromotionDtlByItemCo(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tpromotion_dtl_updbyitemco", arr);
    }
    public void vClearWrkPaymentInvoice(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_paymentinvoice_updclear", arr);
    }
    public void vclosingcheckjaret1stday(ref SqlDataReader rs, List<cArrayList> arr)
    {
        dal.vGetRecordsetSP("sp_checkjaret1stday", ref rs, arr);
    }
    public void vUpdateWrkPaymentInvoiceByOnOff(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_paymentinvoice_updbyonoff", arr);
    }
    public void vInsertWrkPaymentInvoiceByCusgrcd(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_tpaymentinvoice_insbygroup", arr);
    }
    public void vBatchCollectingFreeItem(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batchcollectingfreeitem", arr);
    }
    public void vUpdateWrkPaymentInvoice(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_paymentinvoice_upd", arr);
    }
    public void vInsertWrkPaymentInvoice(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_tpaymentinvoice_ins", arr);
    }
    public void vUpdateStockOpnameSchedule(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tstockopname_schedule_upd", arr);
    }
    public void vclosingschedulejaretmonthly(ref SqlDataReader rs, List<cArrayList> arr)
    {
        dal.vGetRecordsetSP("sp_closingschedulejaretmonthly", ref rs, arr);
    }
    public void vclosingschedulejaret(ref SqlDataReader rs, List<cArrayList> arr)
    {
        dal.vGetRecordsetSP("sp_closingschedulejaret", ref rs, arr);
    }
    //public void vInsertProposalPaid(List<cArrayList> arr, ref string sClaimNo)
    //{
    //    dal.vExecuteSP("tproposal_paid_ins", arr, "@claimco_cd", ref sClaimNo);
    //}
    //public void vGetProposalPayment(List<cArrayList> arr, ref SqlDataReader rs)
    //{
    //    dal.vGetRecordsetSP("sp_tproposal_payment_get", ref rs, arr);
    //}
    //public void vHandledError(ref Exception ex, string sSource)
    //{
    //    List<cArrayList> arr = new List<cArrayList>();
    //    arr.Add(new cArrayList("@err_source", sSource));
    //    arr.Add(new cArrayList("@err_description", ex.ToString()));
    //    vInsertErrorLog(arr);
    //}
    //public void vGetProposalPaymentByDate(List<cArrayList> arr, ref SqlDataReader rs)
    //{
    //    dal.vGetRecordsetSP("sp_tproposal_payment_getbydate", ref rs, arr);
    //}
    public void vclosingDestroyLastMonth(ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_closingDestroyLastMonth", ref rs);
    }
    public void vGetBgCustom(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tproposal_bgcustom", ref rs, arr);
    }
    public void vCheckDiscount(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_check_discount", ref rs, arr);
    }
    public void vUpdateCashRegisterByCashStaID(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcashregister_updbycashstaid", arr);
    }
    public void vGetCashRegister(ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tcashregister_getbycashstaid", ref rs);
    }
    public void vInsertAuditTrail(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_taudit_trail_ins", arr);
    }
    public void vUpdateMstCustomerByApps(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_customer_updbyapps", arr);
    }
    public void vUpdateCanvasOrderInfoByAppStaID(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcanvasorder_info_updbyappstaid", arr);
    }

    // SP For Master Discount @nico 06092016

    public void vUpdateDosalesInvoiceInfoByIsDiscOnePct(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdosalesinvoice_info_updbyisdisconepct", arr);
    }
    public void vUpdateFieldValue(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tfield_value_upd", arr);
    }
    public void vInsertClaimrRemark(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tclaimremark_ins", arr);
    }
    public void vUpdateTmstclaim(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_claimpren_upd", arr);
    }
    public void vDeleteWrkDiscountRelated(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_wrk_fordiscount_del", arr);
    }
    public void vUpdateCashRegisterClosingAck(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcashregister_closing_updbyack", arr);
    }
    //public void vGetDiscountNo(List<cArrayList> arr, ref SqlDataReader rs)
    //{
    //    dal.vGetRecordsetSP("sp_getsystemno", ref rs, arr);
    //}

    //public void vInsertDiscSalespoint(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tdisc_salespoint_ins", arr);
    //}

    //public void vInsertDiscCustomer(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tdisc_customer_ins", arr);
    //}

    //public void vInsertDiscCusgrcd(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tdisc_cusgrcd_ins", arr);
    //}

    //public void vInsertDiscCustype(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tdisc_custtype_ins", arr);
    //}

    //public void vInsertDiscItem(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tdisc_item_ins", arr);
    //}

    //public void vInsertDiscProduct(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tdisc_product_ins", arr);
    //}

    //public void vInsertMstDiscount(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tmst_discount_ins", arr);
    //}

    //public void vUpdateMstDiscount4(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tmst_discount_upd4", arr);
    //}
    public void vUpdateDosalesInvoiceInfoByReason(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdosalesinvoice_info_updbyreason", arr);
    }
    public void vGetDosalesInvoiceDtl(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tdosalesinvoice_dtl_get", ref rs, arr);
    }
    public void vDelWrkCustDoc(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_custdoc_del", arr);
    }
    public void vInsertWrkCustDoc(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_custdoc_ins", arr);
    }
    public void vUpdateSalesOrderInfoByStatus(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsalesorder_info_updbystatus", arr);
    }
    public void vDelApprovalPattern(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tapprovalpattern_del", arr);
    }
    public void vInsertApprovalPattern(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tapprovalpattern_ins", arr);
    }
    public void vGetPaymentDeposit(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tpayment_deposit_get", ref rs, arr);
    }
    public void vInsertTuserProfileHO(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tuser_profile_HO_ins", arr);
    }
    public void vInsertTuserProfile(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tuser_profile_ins", arr);
    }
    public void vUpdateTuserProfileHO(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tuser_profile_HO_upd1", arr);
    }
    public void vUpdateTuserProfile(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tuser_profile_upd1", arr);
    }
    public void vSearchtuserprofile(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tuser_profile_getbyempcd1", ref rs, arr);
    }
    public void vGetMstProposal(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_proposal_get", ref rs, arr);
    }
    public void vDelWrkProduct(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_product_del", arr);
    }
    public void vSearchMstProposalSearchByActive(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_proposal_searchbyactive", ref rs, arr);
    }
    public void vSearchMstProposal(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_proposal_search", ref rs, arr);
    }
    public void vInsertWrkProduct(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_product_ins", arr);
    }
    public void vInsertPaymentDeposit(List<cArrayList> arr, ref string sDepNo)
    {
        dal.vExecuteSP("sp_tpayment_deposit_ins", arr, "@dep_cd", ref sDepNo);
    }
    public void vBatchNotifiedInvoice5Days()
    {
        dal.vExecuteSP("sp_batchnotifiedinvoice5days");
    }
    public void vUpdateTTabInternalTransferDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_ttab_internaltransfer_dtl_upd", arr);
    }
    public void vUpdateDoSalesINvoiceByDriver(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdosalesinvoice_info_updbydriver", arr);
    }
    public void vUpdateMstDoSalesByDate(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_dosales_updbydodt", arr);
    }
    public void vUpdateTsalesorderInfoByDriverStaID(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsalesorder_info_updbydriverstaid", arr);
    }
    public void vSearchMstCustomerByCusgrcd(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tdosales_invoice_getbycusgrcd", ref rs, arr);
    }
    public void vUpdateTMstCustomerByMaps(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_customer_updbymaps", arr);
    }
    public void vUpdateTmstDiscountByEndDate(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_discount_updbyenddate", arr);
    }
    public void vInserttstockopnamecheck_dtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tstockopnamecheck_dtl_ins", arr);
    }
    public void vDeletetstockopnamecheck_dtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tstockopnamecheck_dtl_Del", arr);
    }

    public void vDeletetstock_opname_check(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tstock_opname_check_Del", arr);
    }
    public void vUpdateStockOpnamecheckDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tstockopnamecheck_dtl_upd", arr);
    }

    public void vGenStockOpnamecheck(List<cArrayList> arr, ref string sStockNo)
    {
        dal.vExecuteSP("sp_tstock_opname_check_gen", arr, "@stock_no", ref sStockNo);
    }
    public void vGetStockOpnamecheck(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tstockopnamecheck_get", ref rs, arr);
    }
    public void vDelsoacustomer(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_trp_bycustomer_del", arr);
    }
    public void vDelsoacustomer1(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_trp_bycustomer_del1", arr);
    }

    public void vInsertsoasalesman(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_trp_bysalesman_ins", arr);
    }
    public void vDelsoasalesman(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_trp_bysalesman_del", arr);
    }
    public void vDelsoasalesman1(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_trp_bysalesman_del1", arr);
    }
    public void vDelSalesmanVisit(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsalesman_visit_del", arr);
    }
    public void vInsertSalesmanVisit(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsalesman_visit_ins", arr);
    }
    public void vGetSalesmanVisit(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tsalesman_visit_get", ref rs, arr);
    }
    public void vUpdateMstDoSalesByDriverByInv(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_dosales_updbydriverbyinv", arr);
    }
    public void vInsertMstProposal(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_proposal_ins", arr);
    }

    public void vUpdatetMstProposalNo(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tproposalno_alltable_upd", arr);
    }

    // Add GetBrandedName for ProposalNo @26-05-2016 By Nico
    public void vGetProposalNo(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tproposalno_product_get", ref rs, arr);
    }

    // Add GetProposal @11-06-2016 By Nico
    public void vGetProposal(List<cArrayList> arr, ref SqlDataReader rs)
    {
        //dal.vGetRecordsetSP("sp_look_proposal", ref rs, arr);sp_report_proposal
        dal.vGetRecordsetSP("sp_report_proposal", ref rs, arr);
    }

    // Add GetListRegionSalespoint @13-07-2016 By Nico
    public void vBindingRegionToCombo(ref DropDownList cbo)
    {
        SqlDataReader rs = null;
        DataTable dta = new DataTable();
        dal.vGetRecordsetSP("sp_tmst_region_get", ref rs);
        dta.Load(rs);
        cbo.DataValueField = "region_cd";
        cbo.DataTextField = "region_nm";
        cbo.DataSource = dta.DefaultView;
        cbo.DataBind();
    }

    // Add GetListSalesPoint @31-05-2016 By Nico
    public void vBindingSalespointToCombo(ref DropDownList cbo)
    {
        SqlDataReader rs = null;
        DataTable dta = new DataTable();
        dal.vGetRecordsetSP("sp_tmst_salespoint_get", ref rs);
        dta.Load(rs);
        cbo.DataValueField = "salespointcd";
        cbo.DataTextField = "salespoint_desc";
        cbo.DataSource = dta.DefaultView;
        cbo.DataBind();
    }

    // Add Insert SalesPoint @31-05-2016 By Nico
    public void vInsertProposalSalespoint(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tproposal_salespoint_ins", arr);
    }

    // Delete SalesPoint @31-05-2016 By Nico
    public void vDelProposalSalespoint(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tproposal_salespoint_del", arr);
    }

    // Search Customer for Proposal @31-05-2016 By Nico
    public void vSearchMstCustomerPro(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_customer_searchpro", ref rs, arr);
    }

    // Delete Customer Selected for Proposal @01062016 By Nico
    public void vDelProposalCustomerSelected(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tproposal_customer_sel_del", arr);
    }

    // Delete Customer Group Selected for Proposal @01062016 By Nico
    public void vDelProposalCustomerGroupSelected(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tproposal_customergroup_sel_del", arr);
    }

    // Insert Mechanism Rebate for Proposal @04062016 By Nico
    public void vInsertProposalMechRebate(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tproposal_mech_rebate_ins", arr);
    }

    // Delete Mechanism Rebate for Proposal @05062016 By Nico
    public void vDelProposalMechRebate(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tproposal_mech_rebate_del", arr);
    }

    // Insert Mechanism Other Promo for Proposal @04062016 By Nico
    public void vInsertProposalMechOtherPromo(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tproposal_mech_otherpromo_ins", arr);
    }

    // Delete Mechanism Rebate for Proposal @05062016 By Nico
    public void vDelProposalMechOtherPromo(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tproposal_mech_otherpromo_del", arr);
    }

    // Insert Mechanism DisplayRent for Proposal @04062016 By Nico
    public void vInsertProposalMechDisplayRent(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tproposal_mech_rent_ins", arr);
    }

    // Delete Mechanism DisplayRent for Proposal @05062016 By Nico
    public void vDelProposalMechDisplayRent(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tproposal_mech_rent_del", arr);
    }

    // Insert Mechanism Fee for Proposal @04062016 By Nico
    public void vInsertProposalMechFee(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tproposal_mech_fee_ins", arr);
    }

    // Delete Mechanism Fee for Proposal @05062016 By Nico
    public void vDelProposalMechFee(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tproposal_mech_fee_del", arr);
    }

    // Insert Mechanism Car Branding for Proposal @07062016 By Nico
    public void vInsertProposalMechCar(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tproposal_mech_car_ins", arr);
    }

    // Delete Mechanism Car Branding for Proposal @07062016 By Nico
    public void vDelProposalMechCar(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tproposal_mech_car_del", arr);
    }

    // Insert Mechanism Cook for Proposal @07062016 By Nico
    public void vInsertProposalMechCook(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tproposal_mech_cook_ins", arr);
    }

    // Delete Mechanism Cook for Proposal @07062016 By Nico
    public void vDelProposalMechCook(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tproposal_mech_cook_del", arr);
    }

    // Insert Mechanism Signboard for Proposal @07062016 By Nico
    public void vInsertProposalMechSignboard(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tproposal_mech_signboard_ins", arr);
    }

    // Delete Mechanism Signboard for Proposal @07062016 By Nico
    public void vDelProposalMechSignboard(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tproposal_mech_signboard_del", arr);
    }

    // Insert Mechanism Signboard for Proposal @07062016 By Nico
    public void vInsertProposalMechChep(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tproposal_mech_chep_ins", arr);
    }

    // Delete Mechanism Signboard for Proposal @07062016 By Nico
    public void vDelProposalMechChep(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tproposal_mech_chep_del", arr);
    }

    // Insert Add Proposal Sign @13062016 By Nico
    public void vInsertProposalSign(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tproposal_sign_ins", arr);
    }

    public void vDelProposalSign(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tproposal_sign_del", arr);
    }

    public void vUpdateProposalSign(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tproposal_sign_upd", arr);
    }

    // Get Estimated Cost for Proposal @15062016 BY Nico
    public void vGetEstimatedCost(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tproposal_cost", ref rs, arr);
    }

    // Insert Item from Product Group into Deleted for Proposal @19062016 By Nico
    public void vInsProposalProductGroup(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tproposal_productgroup_ins", arr);
    }

    // Deleted Product Group 
    public void vDelProposalProductGroup(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tproposal_productgroup_del", arr);
    }

    // Get Budget Proposal for Product Group
    public void vGetBudgetProductProposal(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_budget_product_get", ref rs, arr);
    }

    // Get Budget Proposal for Item
    public void vGetBudgetItemProposal(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_budget_item_get", ref rs, arr);
    }

    // Get Coloumn Group Budget for Proposal 28062016 By Nico
    public void vGetColumnBudget(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_budget_column_get", ref rs, arr);
    }

    public void vInsertBudgetProduct(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tproposal_product_budget_ins", arr);
    }

    public void vUpdateBudgetProduct(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tproposal_product_budget_upd", arr);
    }

    // Update Approval Proposal
    public void vUpdatetMstProposal(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_proposal_upd", arr);
    }


    // Add GetCustLocation for Outlet Region @28-05-2016 By Nico
    public void vBindingCustLocationToCombo(ref DropDownList cbo)
    {
        SqlDataReader rs = null;
        DataTable dta = new DataTable();
        dal.vGetRecordsetSP("sp_tmst_location_cust", ref rs);
        dta.Load(rs);
        cbo.DataValueField = "loc_cd";
        cbo.DataTextField = "loc_nm";
        cbo.DataSource = dta.DefaultView;
        cbo.DataBind();
    }

    // Add GetCustName for Outlet by Region and Type @28052016 By Nico
    public void vBindingCustNameToCombo(ref DropDownList cbo, string sType, string sLoc)
    {
        SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@otlcd", sType));
        arr.Add(new cArrayList("@city", sLoc));
        DataTable dta = new DataTable();
        dal.vGetRecordsetSP("sp_tmst_cust_by_loctype_get", ref rs, arr);
        dta.Load(rs);
        cbo.DataValueField = "cust_cd";
        cbo.DataTextField = "cust_nm";
        cbo.DataSource = dta.DefaultView;
        cbo.DataBind();

    }



    // Delete Mechanism Rebate for Proposal @05062016 By Nico
    //public void vDelProposalMechRebate(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tproposal_mech_rebate_del", arr);
    //}
    public void vUpdateSalesreturnByStatus(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsalesreturn_updbystatus", arr);
    }
    public void vUpdateTTabSalesReturnDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_ttab_paymentreceipt_dtl_upd", arr);
    }
    public void vInsertsoacustomer(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_trp_bycustomer_ins", arr);
    }
    //public void vDelsoacustomer(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_trp_bycustomer_del", arr);
    //}
    //public void vDelsoacustomer1(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_trp_bycustomer_del1", arr);
    //}

    //public void vInsertsoasalesman(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_trp_bysalesman_ins", arr);
    //}
    //public void vDelsoasalesman(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_trp_bysalesman_del", arr);
    //}
    //public void vDelsoasalesman1(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_trp_bysalesman_del1", arr);
    //}
    public void vInsertWrkSalesDiscountFromCore(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_salesdiscount_insfromcore", arr);
    }
    public void vInsertWrkCanvasdiscountFromCore(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_canvasdiscount_insfromcore", arr);
    }
    public void vUpdateDosalesInvoiceByZero(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdosales_invoice_updbyzero", arr);
    }
    public void vInsertPaymentSuspenseDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsuspensepayment_dtl_ins", arr);
    }
    public void vInsertTmstPaymentFromSuspense(List<cArrayList> arr, ref string sPaymentNo)
    {
        dal.vExecuteSP("sp_tmst_payment_insfromsuspense", arr, "@payment_no", ref sPaymentNo);
    }
    public void vUpdateTSalesorderdtlbystockavl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsalesorder_dtl_updbystockavl", arr);
    }
    public void vSearchMstDiscount(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_discount_search", ref rs, arr);
    }
    public void vGetTPaymentSupense(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tpayment_suspense_get", ref rs, arr);
    }
    public void vUpdateTTabSalesReturn(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_ttab_salesreturn_upd", arr);
    }
    public void vDeleteTTabSalesreturn(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_ttab_salesreturn_del", arr);
    }
    public void vDeleteTTabletSalesorder(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_ttablet_salesorder_del", arr);
    }
    public void vUpdateTTabletSalesorder(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_ttablet_salesorder_upd", arr);
    }
    public void vUpdateTTabInternalTransfer(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_ttab_internaltransfer_upd", arr);
    }
    public void vDelTTabInternalTransfer(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_ttab_internaltransfer_del", arr);
    }
    public void vUpdateTTabCanvasOrder(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_ttab_canvasorder_upd", arr);
    }
    public void vDelTtabCanvasorder(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_ttab_canvasorder_del", arr);
    }
    public void vUpdateTabPaymentReceiptByDate(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_ttab_paymentreceipt_updbydate", arr);
    }
    public void vDelTabPaymentReceipt(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_ttab_paymentreceipt_del", arr);
    }
    public void vUpdateTabPaymentReceipt(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_ttab_paymentreceipt_upd", arr);
    }
    public void vGetDirectSpv(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_getdirectspv", ref rs, arr);
    }
    public void vBatchItemSoldBranch()
    {
        dal.vExecuteSP("sp_batchbranchitemsold");
    }
    public void vBatchAchievementBranch()
    {
        dal.vExecuteSP("sp_batchachievementbranch");
    }
    public void vBatchTargetCollectionBranch()
    {
        dal.vExecuteSP("sp_batch_createtargetcollection");
    }
    public void vclosingstockjaretLastMonth(ref SqlDataReader rs, List<cArrayList> arr)
    {
        dal.vGetRecordsetSP("sp_closingstockjaretLastMonth", ref rs, arr);
    }
    public void vBatchGenerateDailyRPS()
    {
        dal.vExecuteSP("sp_batch_generatedailyrps");
    }
    public void vTabsalesorderdiscount(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_ttabsalesorder_discount", arr);
    }
    public void vCancelDosalesInvoice(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdosales_invoice_del", arr);
    }
    public void vGetDOSalesInvoice2(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tdosales_invoice_get2", ref rs, arr);
    }
    public void vSearchDOSalesInvoice(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tdosales_invoice_search", ref rs, arr);
    }
    public void vGettblARCNDtl(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tblARCNDtl_get", ref rs, arr);
    }
    public void vBatchTabPaymentReceipt(List<cArrayList> arr, ref string sComment)
    {
        dal.vExecuteSP("sp_ttabbatchpaymentreceipt", arr, "@msg", ref sComment);
    }
    public void vSearchFldValue(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tfield_value_search", ref rs, arr);
    }
    public void vInsertSMSSentHist(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsms_sent_hist_ins", arr);
    }
    public void vGetWrkSalesReturn(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_twrk_salesreturn_get", ref rs, arr);
    }
    public void vDelInternalTransferDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tinternal_transfer_dtl_del2", arr);
    }
    public void vDelSalesOrderDiscHist(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsalesorder_dischist_del", arr);
    }
    public void vInsertCcnr(List<cArrayList> arr, ref string sCCNR)
    {
        dal.vExecuteSP("sp_tccnr_ins", arr, "@ccnr_no", ref sCCNR);
    }
    public void vInsertClaim(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tclaim_ins", arr);
    }
    public void vApproveClaim(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tclaim_upd1", arr);
    }
    public void vInsertTclaimDoc(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tclaim_document_ins", arr);
    }
    public void vUpdateClaim(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tclaim_upd", arr);
    }
    public void vGetClaimDetail(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tclaim_get", ref rs, arr);
    }
    public void vInsertClaim(List<cArrayList> arr, ref string sCLNO)
    {
        dal.vExecuteSP("sp_tclaim_ins", arr, "@claim_no", ref sCLNO);
    }

    public void vInsertClaimdtlfreeso(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tclaimdtl_sofree_ins", arr);
    }
    public void vInsertClaimdtlfreeco(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tclaimdtl_cofree_ins", arr);
    }
    public void vInsertClaimdtlcashso(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tclaimdtl_socash_ins", arr);
    }
    public void vInsertClaimdtlcashco(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tclaimdtl_cocash_ins", arr);
    }
    public void vGetBookingSanad(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tbooking_sanad_get", ref rs, arr);
    }
    public void vclosingstockjaretmonthly(ref SqlDataReader rs, List<cArrayList> arr)
    {
        dal.vGetRecordsetSP("sp_closingstockjaretmonthly", ref rs, arr);
    }
    public void vBatchCore2WrkDiscount(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batchcore2wrkdiscount", arr);
    }
    public void vInsertTabCanvasDiscount(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_ttabcanvasdiscount_ins", arr);
    }
    public void vInsertStockConfirmDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tstockconfirm_dtl_ins", arr);
    }
    public void vBatchStockConfirm(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_stock_tmst_van_stock_upd", arr);
    }
    public void vUpdatCashregisterClosing(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcashregister_closing_upd", arr);
    }
    public void vInsertPaymentInfo(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tpayment_info_ins", arr);
    }
    public void vUpdateSalesTargetSalespoint(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsalestargetsalespoint_upd", arr);
    }
    public void vInsertTargetSalespoint(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsalestargetsalespoint_ins", arr);
    }
    public void vInsertStockConfirm(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tstock_confirm_ins", arr);
    }
    public void vInserttab_tinternaltransfer_dtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tab_tinternaltransfer_dtl_ins", arr);
    }
    public void vInsertttab_salesreturn_dtl2(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_ttab_salesreturn_dtl_ins2", arr);
    }

    public void vInsertttab_payment_dtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_ttab_payment_dtl_ins2", arr);
    }
    public void vInserttabsalesOrderDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_ttabsalesorder_dtl_ins", arr);
    }
    public void vInserttabCanvasOrderDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_ttabcanvasorder_dtl_ins", arr);
    }
    public void vDeleteSalesTargetHO(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsalestargetho_del", arr);
    }
    public int nCheckExistingToken(int ntoken)
    {
        int nTemp = 0;
        nTemp = Convert.ToInt32(vLookUp("select dbo.fn_checkexistingtoken(" + ntoken.ToString() + ")"));
        return (nTemp);
    }
    //public void vInsertttab_payment_dtl(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_ttab_payment_dtl_ins", arr);
    //}
    //public void vInserttabsalesOrderDtl(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_ttabsalesorder_dtl_ins", arr);
    //}
    //public void vInserttabCanvasOrderDtl(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_ttabcanvasorder_dtl_ins", arr);
    //}
    public void vDeletetwrk_salesorderfreeitem(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_salesorderfreeitem_delete", arr);
    }
    public void vDeletetwrk_canvasorderfreeitem(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_canvasorderfreeitem_delete", arr);
    }
    public void vDelStockOpnameSchedule(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tstockopname_schedule_del", arr);
    }
    public void vInsertStockOpnameSchedule(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tstockopname_schedule_ins", arr);
    }
    public void vDelTstockopnamedtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tstockopname_dtlbystockno", arr);
    }
    public void vBatchCustomerPriceBooking(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batchcustomerpricebooking", arr);
    }
    public void vDelCustomerPriceBooking(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcustomerprice_booking_del", arr);
    }
    public void vInsertCustomerPriceBooking(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcustomerprice_booking_ins", arr);
    }
    public void vInsertPromotionDoc(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tpromotion_doc_ins", arr);
    }
    public void vBatchBfrClosingday(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batchsoawithsysdt", arr);
    }
    public void vDelProposalCustType(List<cArrayList> arr)
    {
        dal.vExecuteSP("tproposal_custtype_del", arr);
    }
    public void vInsertProposalCustType(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tproposal_custtype_ins", arr);
    }
    public void vBatchBfrClosingCashier(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batchbfrclosingcashier", arr);
    }
    public void vGetcustomer_email(ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_customer_email_update", ref rs);
    }
    public void vInsertTcustomer_Check(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcustomer_check_ins", arr);
    }
    public void vGetcustomer_documentcredit3(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_customer_sendcredit3", ref rs, arr);
    }
    public void vGetcustomer_documentcredit2(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_customer_sendcredit2", ref rs, arr);
    }
    public void vGetcustomer_documentcredit1(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_customer_sendcredit1", ref rs, arr);
    }
    public void vGetcustomer_documentcash(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_customer_sendcash", ref rs, arr);
    }
    public void vInsertBookingSanad(List<cArrayList> arr, ref string sBookNo)
    {
        dal.vExecuteSP("sp_tbooking_sanad_ins", arr, "@book_no", ref sBookNo);
    }
    public void vGettsalestargetsp(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tsalestargetsp_get2", ref rs, arr);
    }
    public void vUpdatetsalestargetsp(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsalestargetsp_upd2", arr);
    }
    public void vUpdatetsalestargetsp_dtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsalestargetsp_dtl_upd2", arr);
    }
    public void vDeletetsalestargetsp_dtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsalestargetsp_dtl_del", arr);
    }
    //public void vInsertClaim(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tclaim_ins", arr);
    //}

    //public void vInsertTclaimDoc(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tInsertClaimDoc", arr);
    //}
    public void vDelGoodReceiptHODtl2(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tgoodreceiptho_dtl_del2", arr);
    }
    public void vDelGoodReceiptHODtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tgoodreceiptho_dtl_del", arr);
    }
    public void vDelPoDtl2(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tpo_dtl_del", arr);
    }
    public void vUpdatetinternal_transfer(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tinternal_transfer_upd", arr);
    }
    public void vUpdatetblrptstockopnameformVS(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_rptstockopnameformVS", arr);
    }
    //public void vGetcustomer_email(ref SqlDataReader rs)
    //{
    //    dal.vGetRecordsetSP("sp_customer_email_update", ref rs);
    //}
    //public void vInsertTcustomer_Check(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tcustomer_check_ins", arr);
    //}
    //public void vGetcustomer_documentcredit3(List<cArrayList> arr, ref SqlDataReader rs)
    //{
    //    dal.vGetRecordsetSP("sp_tmst_customer_sendcredit3", ref rs, arr);
    //}
    //public void vGetcustomer_documentcredit2(List<cArrayList> arr, ref SqlDataReader rs)
    //{
    //    dal.vGetRecordsetSP("sp_tmst_customer_sendcredit2", ref rs, arr);
    //}
    //public void vGetcustomer_documentcredit1(List<cArrayList> arr, ref SqlDataReader rs)
    //{
    //    dal.vGetRecordsetSP("sp_tmst_customer_sendcredit1", ref rs, arr);
    //}
    //public void vGetcustomer_documentcash(List<cArrayList> arr, ref SqlDataReader rs)
    //{
    //    dal.vGetRecordsetSP("sp_tmst_customer_sendcash", ref rs, arr);
    //}
    public void vGetCanvasOrderApproval(ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_canvasorder_pendingapp", ref rs);
    }
    public void vInsertCashregoutCoreToWrk(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcashregout_coretowrk", arr);
    }
    public void vGetCashRegout(SqlDataReader rs, List<cArrayList> arr)
    {
        dal.vGetRecordsetSP("sp_tcashregout_search", ref rs, arr);
    }
    public void vDelProposalPayment(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tproposal_payment_del", arr);
    }
    public void vInsertProposalPayment(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tproposal_payment_ins", arr);
    }
    //public void vUpdatetinternal_transfer(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tinternal_transfer_upd", arr);
    //}
    public void vDelProposalProduct(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tproposal_product_del", arr);
    }
    public void vDelProposalCusgrcd(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tproposal_cusgrcd_del", arr);
    }
    public void vInsertProposalCusgrcd(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcustomer_cusgrcd_ins", arr);
    }
    public void vDelProposalCustomer(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tproposal_customer_del", arr);
    }
    public void vInsertProposalCustomer(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tproposal_customer_ins", arr);
    }
    public void vDelProposalItem(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tproposal_item_del", arr);
    }
    public void vInsertProposalProduct(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tproposal_product_ins", arr);
    }
    public void vInsertProposalItem(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tproposal_item_ins", arr);
    }
    public void vInsertMstProposal(List<cArrayList> arr, ref string sPropNo)
    {
        dal.vExecuteSP("sp_tmst_proposal_ins", arr, "@prop_no", ref sPropNo);
    }
    public void vBatchCleanWrk(List<cArrayList> arr)
    { dal.vExecuteSP("sp_batchclearwrk", arr); }
    public void vSearchtblTrnStock(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tblTrnStock_search", ref rs, arr);
    }
    public void vInserttinvestigationDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tinvestigationDtl_ins", arr);
    }
    public void vDeletetinvestigationDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tinvestigationDtl_Del", arr);
    }
    public void vInserttinvestigation(List<cArrayList> arr, ref string sivgno)
    {
        dal.vExecuteSP("sp_tinvestigation_ins", arr, "@ivgno", ref sivgno);
    }
    public void vUpdatetinvestigation(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tinvestigation_upd", arr);
    }
    public void vGettinvestigation(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tinvestigation_get", ref rs, arr);
    }
    public void vDeletetinvestigation(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tinvestigation_Del", arr);
    }
    public void vUpdatetinvestigationList(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tinvestigationList_upd", arr);
    }
    public void vDelWrkCanvasDiscount(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_canvasdiscount_del", arr);
    }
    public void vBatchEditCanvasDiscount(List<cArrayList> arr)
    { dal.vExecuteSP("sp_batcheditdiscount2", arr); }
    public void vUpdateWrkCanvasDiscount(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_canvasdiscount_upd", arr);
    }
    public void vDelWrkEditFreeCash(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_editfreecash_del", arr);
    }
    //public void vInserttinvestigationDtl(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tinvestigationDtl_ins", arr);
    //}
    //public void vDeletetinvestigationDtl(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tinvestigationDtl_Del", arr);
    //}
    //public void vInserttinvestigation(List<cArrayList> arr, ref string sivgno)
    //{
    //    dal.vExecuteSP("sp_tinvestigation_ins", arr, "@ivgno", ref sivgno);
    //}
    //public void vUpdatetinvestigation(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tinvestigation_upd", arr);
    //}
    //public void vGettinvestigation(List<cArrayList> arr, ref SqlDataReader rs)
    //{
    //    dal.vGetRecordsetSP("sp_tinvestigation_get", ref rs, arr);
    //}
    //public void vDeletetinvestigation(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tinvestigation_Del", arr);
    //}
    public void vUpdateDiscountExcluded(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdiscount_excluded_upd", arr);
    }
    public List<string> ltrnstockApproval(string strnstockApproval)
    {
        List<string> lTemp = new List<string>();
        string sEmpID = vLookUp("select emp_cd from tmst_employee where emp_cd in (select qry_data from tmap_query where qry_cd='" + strnstockApproval + "')");
        if (sEmpID == "")
        {
            sEmpID = "2548";//dummy account sending;
        }
        lTemp = lGetApproval(sEmpID);
        return (lTemp);
    }
    public void vInsertWrkEditFreeCash(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_editfreecash_ins", arr);
    }
    public void vDelTwrkEditFreeCash(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_editfreecash_del", arr);
    }
    public void vBatchEditDiscount(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batcheditdiscount", arr);
    }
    public void vInsertSalesOrderDiscHist(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsalesorder_dischist_ins", arr);
    }
    public void vUpdateWrkSalesDiscount(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_salesdiscount_upd", arr);
    }
    public void vDeletetsoa_collection(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsoa_collection_del", arr);
    }

    public void vbatchtsoa_collection(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsoa_collection_gen", arr);
    }
    public void vBatchTurnOffDiscount(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batchturnoffdiscount", arr);
    }
    public void vDelDiscountExcluded(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdiscount_excluded_del", arr);
    }
    public void vInsertDiscountExcluded(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdiscount_excluded_ins", arr);
    }
    public void vUpdateReprintDocByManualNo(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_reprintdocument_updbymanualno", arr);
    }
    public void vRptpriceprodspv(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_rptpriceprodspv", arr);
    }
    public void vDelSalesOrderDiscItem(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsalesorder_discitem_del", arr);
    }
    public void vDelDOSalesInvoiceDiscCash(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdosalesinvoice_disccash_del", arr);
    }
    public void vInsertSalesOrderFreeItemFromCore(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_salesorderfreeitem_fromcore", arr);
    }
    public void vDelDosalesinvoiceDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdosalesinvoice_del", arr);
    }
    public void vDelDosalesDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdosales_dtl_del", arr);
    }
    public void vDelSalesOrderDisccashitem(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsalesorder_disccashitem_del", arr);
    }
    public void vDelSalesOrderDiscCash(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsalesorder_disccash_del", arr);
    }
    public void vDelSalesorderShipment(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsalesorder_shipment_del", arr);
    }
    public void vDelDosalesInvoiceFree(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdosalesinvoice_free_del", arr);
    }
    public void vDelSalesorderDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsalesorder_dtl_del", arr);
    }
    public void vDelWrkSalesDiscount(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_salesdiscount_del", arr);
    }
    public void vInsertAdjustmentprice_Customer(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tadjustmentprice_customer_ins", arr);
    }
    public void vGetCustomerTransfer(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tcustomer_transfer_get", ref rs, arr);
    }
    public void vInsertPaymentBooking(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tpayment_booking_ins", arr);
    }
    public void vBatchSalesmanTransfer(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batchsalesmantransfer", arr);
    }
    //public void vInsertCustomerTransfer(List<cArrayList> arr, ref string sTransferNo)
    //{
    //    dal.vExecuteSP("sp_tcustomer_transfer_ins", arr, "@trf_no", ref sTransferNo);
    //}

    public void vBatchBanner2()
    {
        dal.vExecuteSP("sp_batchbanner2");
    }
    public void vInsertAdjustmentPriceCusGrCd(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_adjustmentprice_cusgrcd_ins", arr);
    }

    public void vInsertWrkSalesreturnFromCore(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_salesreturn_insfromcore", arr);
    }
    public void vInsertAdjustmentCusGrCd(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tadjustmentprice_cusgrcd_ins", arr);
    }
    public void vInserttwrkadjustmentPrice(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrkadjustmentprice_ins", arr);
    }
    public void vDeltwrkadjustmentPrice(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrkadjustmentprice_del", arr);
    }
    public void vBatchStockVanBalance()
    {
        dal.vExecuteSP("sp_batchstockvanbalance");
    }
    public void vInsertTdosalesinvoicedisccash(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdosalesinvoice_disccash", arr);
    }
    public void vBatchStockBalance(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batchstockbalance", arr);
    }
    public void vUpdateSalesTargetSPDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsalestargetsp_dtl_upd", arr);
    }
    public void vDelSalesTargetDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsalestargetsp_dtl_del", arr);
    }
    public void vInsertSalesTargetDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsalestargetsp_dtl_ins", arr);
    }
    public void vGetPeriod(List<cArrayList> arr, SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_getperiodyear", ref rs);
    }
    public void vBatchCancelTO(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batchcancelto", arr);
    }
    public void vGetLastTrans(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_getlasttrans", ref rs, arr);
    }
    public void vInsertCustomerTypePrice_Adjustment(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcustomerprice_adjustment_ins", arr);
    }
    public void vInsertContractAgreement(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcontract_agreement", arr);
    }
    public void vInsertContractTactical(List<cArrayList> arr, ref string sContract)
    {
        dal.vExecuteSP("sp_tcontract_tactical_ins", arr, "@contract_no", ref sContract);
    }
    public int nCheckAccess(string saccesscode, string sUserID)
    {
        int nTemp = 0; SqlDataReader rs = null;
        nTemp = Convert.ToInt16(vLookUp("select dbo.fn_checkaccess('" + saccesscode + "','" + sUserID + "')"));
        return (nTemp);
    }
    //public void vUpdateContractFreeproduct(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tcontract_freeproduct_upd", arr);
    //}
    //public void vUpdateContractFreeitem(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_contract_freeitem_upd", arr);
    //}
    public void vUpdateContractCusgrcd(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcontract_cusgrcd", arr);
    }
    public void vUpdateContractCustomer(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcontract_customer_upd", arr);
    }
    //public void vInsertContractCusgrcd(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tcontract_cusgrcd_ins", arr);
    //}
    public void vDelContractCusgrcd(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcontract_cusgrcd_del", arr);
    }
    public void vDelContractCustomer(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcontract_customer_del", arr);
    }
    //public void vInsertContractCustomer(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tcontract_customer_ins", arr);
    //}
    public void vInsertContractTerm(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcontract_term_ins", arr);
    }
    public void vInsertMstContract(List<cArrayList> arr, ref string sContractNo)
    {
        dal.vExecuteSP("sp_tmst_contract_ins", arr, "@contract_no", ref sContractNo);
    }
    //public void vInsertContractFreeItem(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_contract_freeitem_ins", arr);
    //}
    //public void vInsertContractFreeProduct(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tcontract_freeproduct_ins", arr);
    //}
    //public void vDelContractFreeItem(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tcontract_freeitem_del", arr);
    //}
    //public void vDelContractFreeProduct(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tcontract_freeproduct_del", arr);
    //}
    //public void vUpdateContractPaySchedule(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tcontract_payschedule_upd", arr);
    //}
    //public void vDelContractItem(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tcontract_item_del", arr);
    //}
    //public void vDelContractProduct(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tcontract_product_del", arr);
    //}
    //public void vInsertContractProduct(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tcontract_product_ins", arr);
    //}
    public void vInsertContractItem(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcontract_item_ins", arr);
    }
    public void vDelContractPaySchedule(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcontract_payschedule_del", arr);
    }
    //public void vInsertContractPaySchedule(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tcontract_payschedule_ins", arr);
    //}

    public void vGettsalesreturn_dtl(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tsalesreturn_dtl_get", ref rs, arr);
    }
    public void vDelWrkIsClaim(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_isclaim_del", arr);
    }
    public void vGetWrkIsClaim(ref SqlDataReader rs, List<cArrayList> arr)
    {
        dal.vGetRecordsetSP("sp_twrk_isclaim_get", ref rs, arr);
    }
    public void vInsertWrkIsClaim(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_isclaim_ins", arr);
    }
    public void vInsertCustomerInfo(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcustomer_info_ins", arr);
    }
    public void vBatchClaimProcess()
    {
        dal.vExecuteSP("sp_batchclaimprocess");
    }
    public int nAccessRight(string sUsrID, string sPageName)
    {
        int nTemp = 0;
        SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", sUsrID));
        arr.Add(new cArrayList("@aspx", sPageName));
        dal.vGetRecordsetSP("sp_checkaccessright", ref rs, arr);
        while (rs.Read())
        {
            nTemp = Convert.ToInt32(rs["ncount"]);
        }
        rs.Close(); return (nTemp);
    }
    public void vGetcashregout(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_cashregout_get", ref rs, arr);
    }
    public void vGettbltrnstockDtl(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tbltrnstockDtl_get", ref rs, arr);
    }
    public void vUpdaterptSalesbyseqment(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_rptSalesbyseqment", arr);
    }
    public void vApproveItem(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_item_approve", arr);
    }
    public void vTabletMstVanStock(ref SqlDataReader rs, List<cArrayList> arr)
    {
        dal.vGetRecordsetSP("sp_tmst_van_stock_get", ref rs, arr);
    }
    public void vUpdateMstSalesOrderByStatus(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_salesorder_updstatus", arr);
    }
    public void vInsertEmployeeDocument(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_temployee_document_ins", arr);
    }
    public void vDelEmployeeEducation(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_temployee_education_del", arr);
    }
    public void vInsertEmployeeEducation(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_temployee_education_ins", arr);
    }
    public void vUpdateTmstEmployeeAll(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_employee_updall", arr);
    }
    public void vUpdateTmst_balanceconfirmation(List<cArrayList> arr, ref string datest)
    {
        dal.vExecuteSP("sp_tmst_balanceconfirmation_upd", arr, "@datest", ref datest);
    }
    public void vGetCustomerBalanceConfermation(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_balanceconfirmation_get2", ref rs, arr);
    }
    public void vInsertTmst_balanceconfirmation_dtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_balanceconfirmation_insdtl", arr);
    }
    public void vInsertTmst_balanceconfirmation(List<cArrayList> arr, ref string stmtNo)
    {
        dal.vExecuteSP("sp_tmst_balanceconfirmation_ins", arr, "@stmt_no", ref stmtNo);
    }
    public void vRptMstDiscount(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_trpt_mstdiscount", arr);
    }

    //public void vGetcashregout(List<cArrayList> arr, ref SqlDataReader rs)
    //{
    //    dal.vGetRecordsetSP("sp_cashregout_get", ref rs, arr);
    //}
    public void vGetTabletMstStock(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tabletmststock", ref rs, arr);
    }
    public void vBatchMontStockEndPeriod(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batch_stockEndOfPeriod", arr);
    }
    public void vGetCustomerTypePrice(ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tabletcustprice_get", ref rs);
    }
    public void vGetCustomerTypePrice2(ref SqlDataReader rs, List<cArrayList> arr)
    {
        dal.vGetRecordsetSP("sp_tcustomertype_price_get", ref rs, arr);
    }
    public void vBatchMonthAdvancedPeriod(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batchmonthadvanceperiod", arr);
    }
    public void vBatchMonthcustbalance2(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batchmonthcustbalance2", arr);
    }
    public void vGetDosalesInvoiceByOutstanding(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tdosales_invoice_getbyoutstanding", ref rs, arr);
    }
    public void vInsertTtableCanvasorder(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_ttablet_canvasorder", arr);
    }
    public void vTabletMstItem(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tabletmstitem", ref rs, arr);
    }

    public void vTabletMstItem(ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tabletmstitem", ref rs);
    }
    public void vTabletMstProduct(ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tabletmstproduct", ref rs);
    }
    public void vGettcashregout_dtl(ref SqlDataReader rs, List<cArrayList> arr)
    {
        dal.vGetRecordsetSP("sp_tcashregout_dtl_get", ref rs, arr);
    }
    public void vTabletUserProfile(ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tuser_profile_tablet", ref rs);
    }
    public void vUpdatetcashregout_approve(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcashregout_approve_upd", arr);
    }
    public void vUpdateMstDiscount3(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_discount_upd3", arr);
    }
    public void vupdatetmst_stock_tmst_van_stock(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_stock_tmst_van_stock_upd", arr);
    }
    //public void vupdatetmst_stock_tmst_van_stock(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tmst_stock_tmst_van_stock_upd", arr);
    //}
    public void vApproveCanvasOrder(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcanvasorder_info_upd", arr);
    }
    public void vInsertCanvasInfo(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcanvasorder_info_ins", arr);
    }
    //public void vUpdatetcashregout_approve(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tcashregout_approve_upd", arr);
    //}

    //public void vUpdateMstDiscount3(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tmst_discount_upd3", arr);
    //}
    public void vApproveSalesOrder(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsalesorder_info_upd", arr);
    }
    public void vInsertSalesorderInfo(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsalesorder_info_ins", arr);
    }
    public void vGetDosalesInvoiceByDueDate(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tdosales_invoice_getbyduedate", ref rs, arr);
    }
    public void vUpdateDoSalesInvoice(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdosales_invoice_upd", arr);
    }
    public void vUpdateTmst_Customer(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_customer_upd1", arr);
    }
    public void vSendMail(string sTo, string sSubject, string sMessage, string sfile_attachment)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@to", sTo));
        arr.Add(new cArrayList("@mailtext", sMessage));
        arr.Add(new cArrayList("@mailsubject", sSubject));
        arr.Add(new cArrayList("@file_attachment", sfile_attachment));
        dal.vExecuteSP("sp_sendmailwithattachment", arr);
    }
    //public void vUpdatetcashregout_approve(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tcashregout_approve_upd", arr);
    //}
    public void vBatchTblStock(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batchtblstock", arr);
    }
    public void vDelCanvasOrderFreeItem(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_canvasorderfreeitem_del", arr);
    }
    public void vInserttgoodreceiptho_dtl_to_po_branch(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tgoodreceiptho_dtl_to_po_branch_ins", arr);
    }
    public void vBatchPreDaily(ref SqlDataReader rs, List<cArrayList> arr)
    {
        dal.vGetRecordsetSP("sp_batchpredaily", ref rs, arr);
    }
    public void vUpdateDosalesInvoiceByManualNo(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdosales_invoice_updbymanualno", arr);
    }
    public void vGetLastMonthlyJob(ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_thist_job_get", ref rs);
    }
    public void vBatchPrepropcessMonthly(ref SqlDataReader rs, List<cArrayList> arr)
    {
        dal.vGetRecordsetSP("sp_batchpreprocessmonthly", ref rs, arr);
    }
    public void vUpdateMstDosalesByDriver(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_dosales_updbydriver", arr);
    }
    public void vInsertDosalesInvoiceReceived(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdosalesinvoice_received", arr);
    }
    public void vGetDosalesinvoiceReceived(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tdosales_invoice_getbyreceived", ref rs, arr);
    }
    public void vInsertDOcanvasorderfreeinvoiceitem(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdosalesinvoice_free_insfromcnfreeitem", arr);
    }
    public void vInsertReportCustBalance(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_trpt_customerbalance_ins", arr);
    }
    public void vInsertCustomerTransfer(List<cArrayList> arr, ref string sTrfNo)
    {
        dal.vExecuteSP("sp_tcustomer_transfer_ins", arr, "@trf_no", ref sTrfNo);
    }
    public void vBatchBalanceCustomer(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batchdailybalancecustomer", arr);
    }
    public void vInsertDosalesInvoiceFreeFromFreeItem(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdosalesinvoice_free_insfromfreeitem", arr);
    }
    public void vsp_batchsoa(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batchsoa", arr);
    }
    public void vBatchSOA(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batchsoa", arr);
    }
    public void vInsertDosalesinvoiceFree(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdosalesinvoice_free_ins", arr);
    }
    public void vGetMstMenu(ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_menu_get", ref rs);
    }
    public void vInsertMenuRole(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmenu_role_ins", arr);
    }
    public void vDelMenuRole(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmenu_role_del", arr);
    }
    public void vCheckMenu(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_checkmenu", ref rs, arr);
    }

    public string sSetupMenu(string sUserID)
    {
        string sMenu = string.Empty;
        sMenu = "<li><a href='fm_mstcustomerlist.aspx' title='Customer'>Customer</a></li>"
                                      + "<li><a href='fm_mstsalespointlist.aspx' title='Salespoint'>Salespoint</a></li>"
                                      + "<li><a href='fm_mstdiscount2.aspx' title='Discount'>Discount</a></li>"
                                      + "<li><a href='fm_mstrps.aspx' title='RPS'>Route Plan Salesman</a></li>"
                                      + "<li><a href='fm_mstreasonlist.aspx' title=''>Reason</a></li>"
                                      + "<li><a href='fm_mstloc.aspx' title='Location'>Location</a></li>"
                                      + "<li><a href='fm_salestargetsp.aspx' title='Take Order'>Sales target SP</a></li>"
                                      + "<li><a href='fm_outlettarget.aspx' title='Take Order'>Outlet target</a></li>"
                                      + "<li><a href='fm_salesmantargetcollection.aspx' title='Take Order'>Salesman Collection target</a></li>"
                                      + "<li><a href='fm_docreject.aspx' title='Rejected'>Document Reject</a></li>"
                                      + "<li><a href='frmcustomercategory_doc.aspx' title=''>Doc Cust Category</a></li>"
                                      + "<li><a href='fm_mstdisctypedoc.aspx' title='Document Discount'>Discount Document</a></li>"
                                      + "<li class='last'><a href='fm_mstcontractentry.aspx' title=''>Contract</a></li>";
        return (sMenu);
    }
    public void vDiscountCheck(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_discountcheck", ref rs, arr);
    }
    public void vInserttblstockadjAuto(List<cArrayList> arr, ref string sstkadjno)
    {
        dal.vExecuteSP("sp_auto_ajustment", arr, "@stkadjno2", ref sstkadjno);
    }
    public void vGetDiscountCustType(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tdiscount_custtype_getbywrk", ref rs, arr);
    }
    public string sBatchpreprocess()
    {
        SqlDataReader rs = null; string sTemp = string.Empty;
        dal.vGetRecordSql("select  dbo.fn_checkpreprocess()", ref rs);
        while (rs.Read())
        { sTemp = rs[0].ToString(); }
        rs.Close();
        return (sTemp);
    }
    public void vInsertSalesorderdiscitem_ins(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsalesorder_discitem_ins", arr);
    }
    public void vInsertTwrkSalesReturnFromCore(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_salesreturn_insfromcore", arr);
    }
    public void vGetSalesReturn(ref SqlDataReader rs, List<cArrayList> arr)
    {
        dal.vGetRecordsetSP("sp_tsalesreturn_get", ref rs, arr);
    }
    public void vBatchMakeCompletePayment()
    {
        dal.vExecuteSP("sp_batchmakecompletepayment");
    }
    public void vBatchCashRegisterCheck(List<cArrayList> arr, ref string sMsg)
    {
        dal.vExecuteSP("sp_batchcheckcashregister", arr, "@msg", ref sMsg);
    }
    public void vUpdateSalesreturnByPaymentno(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsalesreturn_updbypaymentno", arr);
    }
    public void vUpdateTsalesReturn(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsalesreturn_upd", arr);
    }
    //public double dGetOpenBalanceCashier()
    //{
    //    System.Data.SqlClient.SqlDataReader rs = null;
    //   // dal.vGetRecordSql("select ")
    //}

    public void vDelWrkSalesReturn(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_salesreturn_del", arr);
    }
    public string sGetSalespointname(string sSalespointCd)
    {
        string sTemp = ""; System.Data.SqlClient.SqlDataReader rs = null;
        dal.vGetRecordSql("select salespoint_nm from tmst_salespoint where salespointcd='" + sSalespointCd + "'", ref rs);
        while (rs.Read())
        { sTemp = rs["salespoint_nm"].ToString(); }
        rs.Close();
        return sTemp;
    }
    public void vInserttblARCNDtl_ins1st(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tblARCNDtl_ins1st", arr);
    }
    public void vInserttmp_sumrysales(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_sumofsalebyoutlet", arr);
    }
    public void vBatchGivenDiscount(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batchgivendiscount", arr);
    }
    public void vUpdateCashRegister2(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcashregister_upd2", arr);
    }
    public double dItemPrice(string sCustCode, string sItemCode, string sUOMTo)
    {
        SqlDataReader rs = null; double dTemp = 0;
        dal.vGetRecordSql("select dbo.fn_getpriceitem", ref rs);
        while (rs.Read())
        {
            dTemp = Convert.ToDouble(rs[0]);
        }
        rs.Close(); return (dTemp);
    }

    public void vInserttcustomercategory_doc(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcustomercategory_doc_ins", arr);
    }
    public void vDeletetcustomercategory_doc(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcustomercategory_doc_del", arr);
    }
    public void vUpdatetuser_profile2(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tuser_profile_upd2", arr);
    }

    public void vGettcashregister_closing(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tcashregister_closing_get", ref rs, arr);
    }
    public void vBatchBackupDBAfter()
    {
        dal.vExecuteSP("sp_batchbackupdbafter");
    }
    public void vUpdatetblARCNDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tblARCNDtl_upd", arr);
    }
    public void vUpdatetblGrdBA(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tblso6_upd", arr);
    }
    public void vInserttblARCN(List<cArrayList> arr, ref string sarcn_no)
    {
        dal.vExecuteSP("sp_tblARCN_ins", arr, "@arcn_no", ref sarcn_no);
    }
    public void vGettblARCN(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tblARCN_get", ref rs, arr);
    }
    public void vUpdatetblARCN_approve(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tblARCN_approve_upd", arr);
    }
    public List<string> lARCNpproval(string sARCNApproval)
    {
        List<string> lTemp = new List<string>();
        string sEmpID = vLookUp("select emp_cd from tmst_employee where emp_cd in (select qry_data from tmap_query where qry_cd='" + sARCNApproval + "')");
        if (sEmpID == "")
        {
            sEmpID = "2548";//dummy account sending;
        }
        lTemp = lGetApproval(sEmpID);
        return (lTemp);
    }
    public void vUpdatetblARCN(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tblARCN_upd", arr);
    }
    public void vInserttcashregister_closing(List<cArrayList> arr, ref string schclosingno)
    {
        dal.vExecuteSP("sp_tcashregister_closing_ins", arr, "@chclosingno", ref schclosingno);
    }
    public void vUpdateMstItemCashout(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_itemcashout_upd", arr);
    }
    public void vBatchClosingCashier(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batchclosingcashier", arr);
    }
    public void vSearchSalesman(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_employee_searcbysalesman", ref rs, arr);
    }
    public void BatchTransaction(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batch_transaction", arr);
    }
    public void vSearchMstEmployee2bysalespoint(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_employee_get4", ref rs, arr);
    }

    public void vInserttinternal_transfer_dtl_tblstock(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tinternal_transfer_dtl_tblstock__ins", arr);
    }
    public void vInserttcontractdocument(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcontractdocument_ins", arr);
    }
    public void vInserttcontractschedule(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcontractschedule_ins", arr);
    }

    public void vInserttmst_contract(List<cArrayList> arr, ref string sSONo)
    {
        dal.vExecuteSP("sp_tmst_contract_ins", arr, "@contract_no", ref sSONo);
    }
    public void vDeltmp_contractdocument(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmp_contractdocument_del", arr);
    }
    public void vInserttmp_contractdocument(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmp_contractdocument", arr);
    }
    public void vGetMstPaymentByNo(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_payment_getbyno", ref rs, arr);
    }
    public void vUpdateWrkSalesReturn(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_salesreturn_upd", arr);
    }
    public void vGetPaymentReceipt(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tpaymentreceipt_get", ref rs, arr);
    }
    public double dGetItemPrice(string sItemCode, string sOtlCode, string sUOM)
    {
        SqlDataReader rs = null; double dTemp = 0;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@otlcd", sOtlCode));
        arr.Add(new cArrayList("@item_cd", sItemCode));
        dal.vGetRecordsetSP("sp_getitemprice", ref rs, arr);
        while (rs.Read())
        {
            dTemp = Math.Round(Convert.ToDouble(rs["unitprice"]), 2);
        }
        rs.Close();
        double dQty = 0;
        dal.vGetRecordSql("select isnull(qty,0) as qty from titem_conversion where item_cd='" + sItemCode + "' and uom_from='CTN' and uom_to='" + sUOM + "'", ref rs);
        while (rs.Read())
        { dQty = Math.Round(Convert.ToDouble(rs["qty"]), 2); }
        rs.Close();
        if (sUOM != "CTN")
        {
            if (dQty == 0)
            { dTemp = 0; }
            else { dTemp = dTemp / dQty; }
        }

        return (Math.Round(dTemp, 2));
    }
    public void vUpdatetblTrnStock_document(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tblTrnStock_document_upd", arr);
    }
    public void vInserttblstockadjdtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tblstockadjdtl_ins", arr);
    }
    public void vInserttblstockadj(List<cArrayList> arr, ref string sstkadjno)
    {
        dal.vExecuteSP("sp_tblstockadj_ins", arr, "@stkadjno", ref sstkadjno);
    }
    public void vGettblstockadj(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tblstockadj_get", ref rs, arr);
    }
    public void vDeletetblstockadjdtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tblstockadjdtl_del", arr);
    }
    public void vUpdatetblstockadjdtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tblstockadjdtl_upd", arr);
    }
    public void vInserttblTrnStock_document(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tblTrnStock_document_ins", arr);
    }
    public void vUpdatetblTrnStock(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tblTrnStock_upd", arr);
    }
    public void vUpdatsalespointgrd(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_salespoint_upd", arr);
    }
    public void vBatchTakeOrderDiscount(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batchtakeorderdiscount", arr);
    }
    public void vUpdateBankDeposit(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tbank_deposit_upd", arr);
    }
    public void vGetBankDeposit(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tbank_deposit_getbydepositid", ref rs, arr);
    }
    public void vGetCashRegisterByCashID(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tcashregister_getbycashid", ref rs, arr);
    }
    public void vInsertCashRegisterStock(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcashregister_stock_ins", arr);
    }
    public void vUpdateCashRegister(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcashregister_upd", arr);
    }
    public int nCheckCanvasDiscountItem(List<cArrayList> arr)
    {
        SqlDataReader rs = null;
        int nTemp = 0;
        dal.vGetRecordsetSP("sp_checkcanvasdiscountfreeitem", ref rs, arr);
        while (rs.Read())
        { nTemp = (int)rs["nCount"]; }
        rs.Close();
        return (nTemp);
    }
    public void vDeletevendorgrp(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_group_vendor_del", arr);
    }
    public void vUpdatevendorgrp(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_group_vendor_upd", arr);
    }
    public void vGetTabPaymentReceipt(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_ttab_paymentreceipt_get", ref rs, arr);
    }
    public void vDelWrkSalesTargetHo(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_tsalestargetho_del", arr);
    }

    public void vInserttSalesTargetdtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsalestargetho_ins1", arr);
    }
    public void vDelWrkSalesTargetDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_salestargetdtl_del", arr);
    }
    public void vInserttmstSalesTargetHO(List<cArrayList> arr, ref string sPoNo)
    {
        dal.vExecuteSP("sp_tsalestargetho_ins", arr, "@target_cd", ref sPoNo);
    }
    public void vInserttworkSalesTargetHO(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tworksalestargetho_ins", arr);
    }
    public void vInsertPrintControl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tprint_control_ins", arr);
    }
    public int nCheckExistFreeItem(List<cArrayList> arr)
    {
        SqlDataReader rs = null;
        int nTemp = 0;
        dal.vGetRecordsetSP("sp_checkexistfreeitem", ref rs, arr);
        while (rs.Read())
        { nTemp = (int)rs["nCount"]; }
        rs.Close();
        return (nTemp);
    }
    public void vUpdateDoSalesInvoiceFree(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdosalesinvoice_free_upd", arr);
    }
    public string sGetEmployeeName(string sEmpCode)
    {
        string sEmpName = string.Empty;
        SqlDataReader rs = null;
        dal.vGetRecordSql("select emp_nm from tmst_employee where emp_cd='" + sEmpCode + "'", ref rs);
        while (rs.Read())
        {
            sEmpName = rs["emp_nm"].ToString();
        }
        rs.Close();
        return sEmpName;
    }
    public void vInserttinternal_transferrequest(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tinternal_transferrequest_ins", arr);
    }
    public void vInsertRptDosalesInvoice(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_trpt_dosalesinvoice_ins", arr);
    }
    public void vInsertCanvasOrderDiscItem(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcanvasorder_discitem_ins", arr);
    }
    public void vGetWrkCanvasOrderDiscount(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_twrk_canvasdiscount_get", ref rs, arr);
    }
    public void vBatchCanvasOrderDiscount(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batchcanvasorderdiscount", arr);
    }
    public void vInsertWrkFileUpload(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_fileupload_ins", arr);
    }
    public void vBatchDiscountCashByItem(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batchdiscountcashitem", arr);
    }
    public void vDeltwrk_discountprod(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_discountprod_del", arr);
    }
    public void vInserttbltrnstockDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tbltrnstockDtl_ins2", arr);
    }
    public void vUpdatetbltrnstockDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tbltrnstockDtl_upd", arr);
    }
    public void vDeletetbltrnstockDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tbltrnstockDtl_del", arr);
    }
    public void vDeletetbltrnstock(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tbltrnstock_Del", arr);
    }
    public void vInserttbltrnstock(List<cArrayList> arr, ref string strnstkNo)
    {
        dal.vExecuteSP("sp_tbltrnstock_ins", arr, "@trnstkNo", ref strnstkNo);
    }
    public void vGettbltrnstock(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tbltrnstock_get", ref rs, arr);
    }

    //public List<string> ltrnstockApproval(string strnstockApproval)
    //{
    //    List<string> lTemp = new List<string>();
    //    string sEmpID = vLookUp("select emp_cd from tmst_employee where emp_cd in (select qry_data from tmap_query where qry_cd='" + strnstockApproval + "'");
    //    if (sEmpID == "")
    //    {
    //        sEmpID = "2548";//dummy account sending;
    //    }
    //    lTemp = lGetApproval(sEmpID);
    //    return (lTemp);
    //}
    public void vUpdatetbltrnstock_approve(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tbltrnstock_approve_upd", arr);
    }
    public void vInserttbltrnstockDtl_destroy(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tbltrnstockDtl_destroy_ins", arr);
    }
    public void vDelWrkDiscountCash(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_discountcash_del", arr);
    }

    public void vInsertWrkSalesOrderDtlFromOrder(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_salessorder_dtl_insfromorder", arr);
    }
    public void vGetMstCanvasorder(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_canvasorder_get", ref rs, arr);
    }

    public void vSearchCanvasOrder(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tcanvasorder_search", ref rs, arr);
    }
    public void vInsertWrkFreeProduct(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_freeproduct_ins", arr);
    }
    public void vInsertDiscountFreeProduct(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdiscount_freeproduct_ins", arr);
    }
    public void vReportcustomertypeprice_trancate()
    {
        dal.vExecuteSP("sp_temppricetype_trancate");
    }
    public void vInsertFreeDiscountProduct(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdiscount_freeproduct_ins", arr);
    }

    public void vDelWrkFreeProduct(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_freeproduct_del", arr);
    }
    public void vInsertWrkProductFree(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_freeproduct_ins", arr);
    }
    public void vBatchBackupDB()
    {
        dal.vExecuteSP("sp_batchbackupdb");
    }

    //public void vDelCanvasOrderFreeItem(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tcanvasorderfreeitem_del", arr);
    //}
    public void vInsertWrkCanvasOrderFreeItem(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_canvasorderfreeitem_ins", arr);
    }
    public void vInsertCanvasOrderDiscount(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcanvassorder_discount_ins", arr);
    }
    public void vReportcustomertypeprice()
    {
        dal.vExecuteSP("sp_temppricetype");
    }
    public void vUpdateMstDiscount2(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_discount_upd2", arr);
    }
    public void vInsertWrkFreeItemFromDiscount(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_freeitem_insfromdiscount", arr);
    }
    public void vInsertWrkFormulaDiscountfromDiscount(List<cArrayList> arr)
    {
        dal.vExecuteSP("twrk_formuladiscount_insfromdiscount", arr);
    }
    public void vInsertWrkItemFromDiscount(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_item_insfromdiscount", arr);
    }
    public void vInsertWrkDiscProductFromDiscount(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_discountprod_insfromdiscount", arr);
    }
    public void vInsertWrkSalespointFromDiscount(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_salespoint_insfromdiscount", arr);
    }
    public void vInsertWrkCustTypeFromDiscount(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_custtype_insfromdiscount", arr);
    }
    public void vInsertWrkCusgrcdFromDiscount(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_cusgrcd_insfromdiscount", arr);
    }
    public void vInsertWrkCustomerFromDiscount(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_customer_insfromdiscount", arr);
    }

    public void vUpdatetgoodreceiptho_dtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tgoodreceiptho_dtl_upd", arr);
    }
    public void vInserttpo_dtl_ins3(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tpo_dtl_ins3", arr);
    }
    public void vupdatetpo_dtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tpo_dtl_upd", arr);
    }
    public void vDelPoDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tpo_dt_del", arr);
    }
    public void vGettmst_po(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_po_get", ref rs, arr);
    }
    public void vDeletetinternal_transfer_dtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tinternal_transfer_dtl_Del", arr);
    }
    public void vBatchWazaranDate(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batch_wazarandate", arr);
    }
    //public void vDeletetinternal_transfer_dtl(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tinternal_transfer_dtl_Del", arr);
    //}
    public void vInsertSalesReturDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsalesreturn_dtl_ins", arr);
    }
    public double dUnitPrice(string sItemCode, string sCustCode, string sSalespointcode)
    {
        string sCustType = string.Empty; string sTemp = string.Empty;
        double dTemp = 0;
        sCustType = vLookUp("select otlcd from tmst_customer where cust_cd='" + sCustCode + "' and salespointcd='" + sSalespointcode + "'");
        sTemp = vLookUp("select unitprice from tcustomertype_price where item_cd='" + sItemCode + "' and cust_typ='" + sCustType + "'");
        if (sTemp == "") { sTemp = "0"; }
        dTemp = Convert.ToDouble(sTemp);
        return (dTemp);
    }
    public void vUpdatetinternal_transfer_dtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tinternal_transfer_dtl_upd", arr);
    }
    public void vInsertSalesReturn(List<cArrayList> arr, ref string sReturNo)
    {
        dal.vExecuteSP("sp_tsalesreturn_ins", arr, "@retur_no", ref sReturNo);
    }
    public void vUpdateMstPayment(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_payment_upd", arr);
    }
    public void vDeleteWrkSalesReturn(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_salesreturn_del", arr);
    }
    public void vInsertWrkSalesReturn(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_salesreturn_ins", arr);
    }
    public void vUpdateSalesmancolTarget(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsalesmancoll_target_upd", arr);
    }
    public void vInsertSalesmancolTarget(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsalesmancoll_target_ins", arr);
    }
    public void vUpdateOutletTarget(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_toutlettarget_upd", arr);
    }
    public void vInsertOutletTarget(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_outlettarget_ins", arr);
    }

    public void vUpdateSalesTargetHO(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsalestargetho_upd", arr);
    }
    public void vUpdateSalesTargetSP(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsalestargetsp_upd", arr);
    }
    public void vInsertSalesTargetSP(List<cArrayList> arr, ref string sTargetNo)
    {
        dal.vExecuteSP("sp_tsalestargetSP_ins", arr, "@target_no", ref sTargetNo);
    }
    public void vInsertSalesTargetHO(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsalestargetho_ins", arr);
    }
    public void vInsertPaymentSuspense(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tpayment_suspense_ins", arr);
    }
    public void vInsertMstRetur(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_retur_ins", arr);
    }
    //public void vInsertSalesTargetHO(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tsalestargetho_ins", arr);
    //}
    public void vUpdateDOSalesInvoice(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_canvasorder_upd", arr);
    }
    public void vUpdateTmst_po(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_po_upd", arr);
    }//by Othman
    public void vUpdatePoDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_po_Dtl_upd", arr);
    } //by Othman
    public void vupdateGridToSp(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_podtl_upd", arr);
    }//by Othman
     //public void vUpdatePoDtl(List<cArrayList> arr)
     //{
     //    dal.vExecuteSP("sp_po_Dtl_upd", arr);
     //} //by Othman
    public void vGettmst_returho(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_returho_get2", ref rs, arr);
    }
    public void vSearchtmst_poho(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_poho_get_search", ref rs, arr);
    }
    public void vSearchMstItem4(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_item_search4", ref rs, arr);
    }
    public void vInserttreturho_dtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_treturho_dtl_ins2", arr);
    }
    public void vInsertMstReturHO2(List<cArrayList> arr, ref string sReturHoNo)
    {
        dal.vExecuteSP("sp_tmst_returho_ins2", arr, "@returho_no", ref sReturHoNo);
    }
    public void vDeletetreturho_dtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_treturho_dtl_Del", arr);
    }
    public void vDeltmst_returho_usr_id(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_returho_usr_id_del", arr);
    }
    public void vSearchtmst_do2(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_do_search2", ref rs, arr);
    }
    public void vInserttblRecReturnHO_ins(List<cArrayList> arr, ref string srecRetHO_no)
    {
        dal.vExecuteSP("sp_tblRecReturnHO_ins", arr, "@recRetHO_no", ref srecRetHO_no);
    }
    public void vUpdatetblRecReturnHO(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tblRecReturnHO_upd", arr);
    }
    public void vGettblRecReturnHO(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tblRecReturnHO_get", ref rs, arr);
    }
    public void vUpdatetreturho_dtl_rec(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_treturho_dtl_rec_upd", arr);
    }
    public void vUpdatetreturho_dtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_treturho_dtl_upd", arr);
    }
    public void vDeletetmst_returho(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_returho_Del", arr);
    }
    public void vUpdatetmst_returho2(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_returho_upd2", arr);
    }
    public void vInserttinternal_transfer_dtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tinternal_transfer_dtl_ins3", arr);
    }
    public void vGettinternal_transfer(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tinternal_transfer_get", ref rs, arr);
    }
    public void vInserttstockopname_dtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tstockopname_dtl_ins", arr);
    }
    public void vDeletetstockopname_dtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tstockopname_dtl_Del", arr);
    }
    public void vDeletetstock_opname(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tstock_opname_Del", arr);
    }
    public void vInsertCanvasOrderDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcanvasorder_dtl_ins", arr);
    }
    public void vInsertMstCanvasOrder(List<cArrayList> arr, ref string sSONo)
    {
        dal.vExecuteSP("sp_tmst_canvasorder_ins", arr, "@so_cd", ref sSONo);
    }
    public void vDeleteDocTypeDocument(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdisctype_document_del", arr);
    }
    public void vInsertDocTypeDocument(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdisctype_document_ins", arr);
    }

    public void vReportDiscount(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_rptdiscount", arr);
    }
    public string sDiscountProduct(string sDiscCode)
    {
        string sSP = string.Empty;
        SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@disc_cd", sDiscCode));
        dal.vGetRecordsetSP("sp_tdiscount_product_get", ref rs, arr);
        while (rs.Read())
        {
            if (sSP.Equals(string.Empty))
            {
                sSP += rs["prod_nm"].ToString();
            }
            else
            {
                sSP += "," + rs["prod_nm"].ToString();
            }
        }
        rs.Close();
        return (sSP);
    }
    public string sDiscountItem(string sDiscCode)
    {
        string sSP = string.Empty;
        SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@disc_cd", sDiscCode));
        dal.vGetRecordsetSP("sp_tdiscount_item_get", ref rs, arr);
        while (rs.Read())
        {
            if (sSP.Equals(string.Empty))
            {
                sSP += rs["item_cd"].ToString();
            }
            else
            {
                sSP += "," + rs["item_cd"].ToString();
            }
        }
        rs.Close();
        return (sSP);
    }
    public string sDiscountSalespoint(string sDiscCode)
    {
        string sSP = string.Empty;
        SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@disc_cd", sDiscCode));
        dal.vGetRecordsetSP("sp_tdiscount_salespoint_get", ref rs, arr);
        while (rs.Read())
        {
            if (sSP.Equals(string.Empty))
            {
                sSP += rs["salespoint_sn"].ToString();
            }
            else
            {
                sSP += "," + rs["salespoint_nm"].ToString();
            }
        }
        rs.Close();
        return (sSP);
    }
    public void vGetEmailSent(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_temail_sent_get", ref rs, arr);
    }
    public void vInsertEmailSent(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_temail_sent_ins", arr);
    }
    public void vTrnStockSetDocumentUpd(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tblTrnStock_set_document_upd", arr);
    }
    public void vInsertLogUser_Track(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tLogUser_Track_int", arr);
    }
    public List<string> lProductSupervisorApproval(string sProductCode)
    {
        List<string> lTemp = new List<string>();
        string sEmpID = vLookUp("select supervisor_cd from tmst_product where prod_cd='" + sProductCode + "'");
        if (sEmpID == "")
        {
            sEmpID = "2540";//dummy account sending;
        }
        lTemp = lGetApproval(sEmpID);
        return (lTemp);
    }
    public void vInsertDiscountSalespoint(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdiscount_salespoint_ins", arr);
    }
    //public void vInsertMstContract(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tmst_contract_ins", arr);
    //}
    public void vGetMstEmployeeByQry(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_employee_getbyqry", ref rs);
    }
    public void vInsertCashRegister(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcashregister_ins", arr);
    }
    public void vInsertBankDeposit(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tbank_deposit_ins", arr);
    }
    public void vButtonNew(ref Button btnew, ref Button btedit, ref Button btsave, ref Button btprint)
    {
        btnew.Visible = true;
        btedit.Visible = false;
        btsave.Visible = true;
        btprint.Visible = false;

    }
    public void vButtonInit(ref Button btnew, ref Button btedit, ref Button btsave, ref Button btprint)
    {
        btnew.Visible = true;
        btedit.Visible = false;
        btsave.Visible = false;
        btprint.Visible = false;

    }
    public void vGetMstPOByQry(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_po_getbyqry", ref rs, arr);
    }
    public void vSearchtmst_returho(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_returho_search", ref rs, arr);
    }
    public void vSearchMstCust3(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_customer_search3", ref rs, arr);
    }
    public void vInserttitem_adjustment_price_dtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_titem_adjustment_price_dtl_ins", arr);
    }
    public void vDeletetitem_adjustment_price_dtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_titem_adjustment_price_dtl_Del", arr);
    }
    public void vInserttitem_adjustment_price_ins(List<cArrayList> arr, ref string sadjp_cd)
    {
        dal.vExecuteSP("sp_titem_adjustment_price_ins", arr, "@adjp_cd", ref sadjp_cd);
    }
    public void vUpdateitem_adjustment_price(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_titem_adjustment_price_upd", arr);
    }
    public void vDeletetitem_adjustment_price(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_titem_adjustment_price_Del", arr);
    }
    public void vSearchtfield_value_otlcd(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tfield_value_otlcd_search", ref rs, arr);
    }
    public void vGettitem_adjustment_price(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_titem_adjustment_price_get", ref rs, arr);
    }
    public void vInsertWrkSalesOrderFreeItemFromCore(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_salesorderfreeitem_insfromtsalesorderfreeitem", arr);
    }
    public void vInsertSalesInvoice(List<cArrayList> arr, ref string sInvNo)
    {
        dal.vExecuteSP("sp_tsalesorder_invoice_ins", arr, "@inv_no", ref sInvNo);
    }
    public void vGetWrkSalesOrderDiscount(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_twrk_salesdiscount_get", ref rs, arr);
    }

    public void vUpdateMstSalesOrder(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_salesorder_upd", arr);
    }
    public void vInsertWrkSalesOrderDtlFromSODtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_salesorderdtl_insfromsodtl", arr);
    }
    public void vInsertWrkSalesOrderFreeItem(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_salesorderfreeitem_ins", arr);
    }
    public void vDelWrkSalesorderFreeItem(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_salesorderfreeitem_del", arr);
    }
    public void vInsertSalesOrderShipment(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsalesorer_shipment_ins", arr);
    }
    public void vGetMstSalesOrder(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_salesorder_get", ref rs, arr);
    }
    public void vDelWrkSalesOrderDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_salesorderdtl_del", arr);
    }
    public void vInsertExpeditionDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_texpedition_dtl_ins", arr);
    }
    public void vDelWrkMstExpedition(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_mstexpedition_del", arr);
    }
    public void vInsertWrkMstExpedition(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_mstexpedition_ins", arr);
    }
    public void vInsertMstExpedition(List<cArrayList> arr, ref string sTripNo)
    {
        dal.vExecuteSP("sp_tmst_expedition_ins", arr, "@trip_no", ref sTripNo);
    }
    public void vGetMstDoSales(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_dosales_get", ref rs, arr);
    }
    public void vGetMstVehicleByEmpcd(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_vehicle_getbyempcd", ref rs, arr);
    }
    public void vInsertMstVehicle(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_vmst_vehicle_ins", arr);
    }
    //public void vSearchtmst_returho(List<cArrayList> arr, ref SqlDataReader rs)
    //{
    //    dal.vGetRecordsetSP("sp_tmst_returho_search", ref rs, arr);
    //}
    public void vBatchInvoiceStatus(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batch_invoicestatus", arr);
    }
    public void vInsertSuspensePayment(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsuspense_payment_ins", arr);
    }

    public void vInsertMstPayment(List<cArrayList> arr, ref string sPaymentNo)
    {
        dal.vExecuteSP("sp_tmst_payment_ins", arr, "@payment_no", ref sPaymentNo);
    }
    public void vInsertPaymentDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tpayment_dtl_ins", arr);
    }
    public void vGetDosalesInvoice(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tdosales_invoice_getbycust", ref rs, arr);
    }
    public void vInsertMstPayment(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_payment_ins", arr);
    }
    public void vUpdateMstDoSales(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_dosales_upd", arr);
    }
    public void vGetMstDoSalesByQry(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_dosales_getbyqry", ref rs, arr);
    }
    public void vUpdateMstDo2(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_do_upd2", arr);
    }
    public void vInserttblWHSHO(List<cArrayList> arr, ref string sWHSHOCD)
    {
        dal.vExecuteSP("sp_tblWHSHO_ins", arr, "@WHSHOCD", ref sWHSHOCD);
    }
    public void vGettblwhsHO(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tblwhsHO_get", ref rs, arr);
    }
    public void vDeletetblwhsHO(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tblwhsHO_Del", arr);
    }
    public void vUpdatetblwhsHO(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tblWHSHO_upd", arr);
    }
    public void vSearchtmst_po2(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_po_search2", ref rs, arr);
    }
    public void vInserttgoodreceiptho_dtl1(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tgoodreceiptho_dtl_ins1", arr);
    }
    public void vInserttmst_goodreceiptho_ins(List<cArrayList> arr, ref string sreceipt_no)
    {
        dal.vExecuteSP("sp_tmst_goodreceiptho_ins", arr, "@receipt_no", ref sreceipt_no);
    }
    public void vUpdatetmst_goodreceiptho(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_goodreceiptho_upd", arr);
    }
    public void vDeletetmst_goodreceiptho(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_goodreceiptho_Del", arr);
    }
    public void vGettmst_goodreceiptho(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_goodreceiptho_get", ref rs, arr);
    }
    public void vDeletetgoodreceiptho_dtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tgoodreceiptho_dtl_Del", arr);
    }

    public void vBatchSalesOrderStatus(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batch_salesorderstatus", arr);
    }
    public void vInsertDoSalesDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdosales_dtl_ins", arr);
    }
    public void vInsertMstDoSales(List<cArrayList> arr, ref string sDoNo)
    {
        dal.vExecuteSP("sp_tmst_dosales_ins", arr, "@do_no", ref sDoNo);
    }
    public void vInsertSalesOrderDiscount(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsalesorder_discount_ins", arr);
    }
    public void vInsertMstSalesOrder(List<cArrayList> arr, ref string sSoNo)
    {
        dal.vExecuteSP("sp_tmst_salesorder_ins", arr, "@so_cd", ref sSoNo);
    }
    public void vDelSalesOrderFreeItem(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_salesorderfreeitem_del", arr);
    }
    public void vInsertSalesOrderFreeItem(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_salesorderfreeitem_ins", arr);
    }
    public void vInsertSalesOrderDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsalesorder_dtl_ins", arr);
    }
    public void vGetDiscountFreeitem(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tdiscount_freeitem_get", ref rs, arr);
    }
    public void vBatchSalesOrderDiscount(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_batchsalesorderdiscount", arr);
    }
    public void vDelWrkDiscountProduct(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_discountprod_del", arr);
    }
    public void vInsertWrkDiscountProduct(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_discountprod_ins", arr);
    }
    public void vInsertDiscountProduct(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdiscount_product_ins", arr);
    }
    public void vSearchCustomerBySalesman(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_customer_searcbysalesman", ref rs, arr);
    }
    public void vInsertWrkSalesORderDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_salesorderdtl_ins", arr);
    }
    public void vDeleteWrkSalesOrderDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_salesorderdtl_del", arr);
    }
    public void vSearchMstItemBySalespoint(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_item_searchbysalespoint", ref rs, arr);
    }
    public void vSearchCustomerBySales(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_customer_searchbysales", ref rs, arr);
    }
    public void vDelRpsDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_trps_dtl_del", arr);
    }
    public void vInserttblSOdtlDiscCH(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tblSOdtlDiscCH_ins", arr);
    }
    public void vDeletetblSODtlDisc(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tblSODtlDisc_Del", arr);
    }
    public void vInsertTblARRecDtlCH(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tblARRecdtlCH_ins", arr);
    }
    public void vDeletetblARRecDetCH(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tblARRecDetCH_Del", arr);
    }
    public void vInsertsprWizRecToInstruStep2(List<cArrayList> arr)
    {
        dal.vExecuteSP("sprWizRecToInstruStep2", arr);
    }
    public void vInsertsprWizRecToInstruStep3(List<cArrayList> arr)
    {
        dal.vExecuteSP("sprWizRecToInstruStep3", arr);
    }
    public void vInsertsprWizRecToInstruStep4(List<cArrayList> arr)
    {
        dal.vExecuteSP("sprWizRecToInstruStep4", arr);
    }
    public void vInsertsprWizRecToInstruStep5(List<cArrayList> arr)
    {
        dal.vExecuteSP("sprWizRecToInstruStep5", arr);
    }
    public void vInsertWrkCustomerFromRpsDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_customer_insfromrpsdtl", arr);
    }
    public void vInsertRpsDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_trps_dtl_ins", arr);
    }
    public void vInsertMstRps(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_rps_ins", arr);
    }
    public void vUpdateMstDo(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_do_upd", arr);
    }
    public void vUpdateDoDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdo_dtl_upd", arr);
    }
    //public void vInserttblSOdtlDiscCH(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tblSOdtlDiscCH_ins", arr);
    //}
    //public void vDeletetblSODtlDisc(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tblSODtlDisc_Del", arr);
    //}
    public void vUpdateMstCustomer(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_customer_upd", arr);
    }
    public void vUpdateAutoPO(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tautopo_upd", arr);
    }
    public void vDelWrkCustomerContact(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_customercontact_del", arr);
    }
    public void vInsertWrkCustomerContact(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_customercontact_ins", arr);
    }
    public void vInsertCustomerContact(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcustomer_contact_ins", arr);
    }
    public string sCheckNull(object obj)
    {
        string sTemp = "";
        if (obj != DBNull.Value)
        {
            sTemp = obj.ToString();
        }
        return (sTemp);
    }
    public void vInserttblARRecAddDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tblARRecAddDtl_ins", arr);
    }
    public void vUpdatetblARRecAddDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tblARRecAddDtl_upd", arr);
    }
    public void vInsertTblARRecDtlAddDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tblARRecdtlAddDtl_ins", arr);
    }
    public void vInserttwrk_tblSODtlDisc(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_tblSODtlDisc_ins", arr);
    }
    public void vUpdatetwrk_tblSODtlDisc(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_tblSODtlDisc_upd", arr);
    }
    public void vInserttblSODtlDisc(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tblSODtlDisc_ins", arr);
    }
    public void vInserttblRPS(List<cArrayList> arr, ref string sRPSCD)
    {
        dal.vExecuteSP("sp_tblRPS_ins", arr, "@RPSCD", ref sRPSCD);
    }
    public void vUpdatetblRPS(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tblRPS_upd", arr);
    }
    public void vSearchSalesman_CD(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_Salesman_search", ref rs, arr);
    }
    public void vGetTblRPS(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tblRPS_get", ref rs, arr);
    }
    public void vInsertTblRPSDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tblRPSDtl_ins", arr);
    }
    public void vDeleteTblRPS(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tblRPS_Del", arr);
    }
    public void vDeleteTblRPSDet(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tblRPSDet_Del", arr);
    }
    public void vGetRequestItem(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_trequest_item_get", ref rs, arr);
    }
    public void vInsertCustomertypePrice4Wrk(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcustomertype_price4wrk", arr);
    }
    public void vDelWrkItemPrice(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_itemprice_del", arr);
    }
    public void vInsertWrkItemPrice(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_itemprice_ins", arr);
    }
    public void vGetMstItemSize(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_item_getsize", ref rs, arr);
    }
    public int nCheckItemExist(string sItemCode)
    {
        string sSQL = "select count(1) from tmst_item where item_cd='" + sItemCode + "'";
        int nCount = Convert.ToInt16(vLookUp(sSQL));
        return (nCount);
    }
    public void vDelMstItemCashout(List<cArrayList> arr)
    {
        dal.vExecuteSP("tmst_itemcashout_del", arr);
    }
    public void vInsertMstItemCashout(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_itemcashout_ins", arr);
    }
    public void vInsertCashRegOut(List<cArrayList> arr, ref string sCasregno)
    {
        dal.vExecuteSP("sp_tcashregout_ins", arr, "@casregout_cd", ref sCasregno);
    }
    public void vGetSetup(ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tblsetup_get", ref rs);
    }
    public void vDelWrkcasregoutdtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_casregoutdtl_del", arr);
    }
    public void vInsertWrkCasregoutdtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_casregoutdtl_ins", arr);
    }
    public void vDelMstProduct(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_product_del", arr);
    }
    public void vUpdateMstItem(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_item_upd", arr);
    }
    public void vGetMstItemGetByStatus(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_item_get_by_sta", ref rs, arr);
    }
    public void vInsertItemSalespoint4Wrk(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_titem_salespoint4wrk_ins", arr);
    }
    public void vInsertRequestItem(List<cArrayList> arr, ref string sRequestNo)
    {
        dal.vExecuteSP("sp_trequest_item_ins", arr, "@request_no", ref sRequestNo);
    }
    public void vDelScheduleWeekly(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tschedule_weekly_del", arr);
    }
    public void vGetpricelevel(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_getpricelevel", ref rs, arr);
    }
    //public void vInserttblARRecAddDtl(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tblARRecAddDtl_ins", arr);
    //}
    //public void vUpdatetblARRecAddDtl(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tblARRecAddDtl_upd", arr);
    //}
    //public void vInsertTblARRecDtlAddDtl(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tblARRecdtlAddDtl_ins", arr);
    //}

    public void vAlert(string sMsg)
    {
        ScriptManager.RegisterStartupScript(null, this.GetType(), "al", "window.alert('" + sMsg + "');", true);
    }
    public void vInsertScheduleWeekly(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tschedule_weekly_ins", arr);
    }
    public void vGetFieldValue(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tfield_value_get", ref rs, arr);
    }
    public void vGetStockOpname(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tstockopname_get", ref rs, arr);
    }
    public void vUpdateStockOpnameDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tstockopname_dtl_upd", arr);
    }
    public void vGenStockOpname(List<cArrayList> arr, ref string sStockNo)
    {
        dal.vExecuteSP("sp_tstock_opname_gen", arr, "@stock_no", ref sStockNo);
    }
    public void vGetMstStock(List<cArrayList> arr, SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_stock_get", ref rs, arr);
    }
    public void vUpdateMstReturHo(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_returho_upd", arr);
    }
    public void vInsertReturHoDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_treturho_dtl_ins", arr);
    }
    public void vInsertMstReturHO(List<cArrayList> arr, ref string sReturHoNo)
    {
        dal.vExecuteSP("sp_tmst_returho_ins", arr, "@returho_no", ref sReturHoNo);
    }
    public void vSearchMstItem3(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_item_search3", ref rs, arr);
    }
    public void vDelWrkReturHo(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_returho_del", arr);
    }
    public void vInsertWrkReturHo(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_returho_ins", arr);
    }
    public void vSearchMstEmployee2(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_employee_get3", ref rs, arr);
    }
    public void sFormat2ddmmyyyy(ref Label lbl)
    {
        if ((lbl.Text == "") || (lbl.Text == null))
        {
            lbl.Text = System.DateTime.Today.ToShortDateString();
        }
        DateTime dt = DateTime.ParseExact(lbl.Text, "M/d/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        lbl.Text = dt.ToString("d/M/yyyy");
    }
    public void sFormat2ddmmyyyy(ref TextBox lbl)
    {
        if ((lbl.Text == "") || (lbl.Text == null)) { lbl.Text = HttpContext.Current.Request.Cookies["waz_dt"].ToString(); }
        DateTime dt = DateTime.ParseExact(lbl.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        lbl.Text = dt.ToString("d/M/yyyy");
    }
    public void vInsertInternalTransferDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tinternal_transfer_dtl_ins", arr);
    }
    public void vDelWrkItemQty(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_itemqty_del", arr);
    }
    public void vInsertWrkItemQty(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_itemqty_ins", arr);
    }
    public void vUpdateMstVehicle(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_vehicle_upd", arr);
    }
    public void vGetMstEmployee2(ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_employee_get2", ref rs);
    }
    public void vInsertInternalTransfer(List<cArrayList> arr, ref string sTrfNo)
    {
        dal.vExecuteSP("sp_tinternal_transfer_ins", arr, "@trf_no", ref sTrfNo);
    }
    public void vSearchMstEmployee(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_employee_search", ref rs, arr);
    }

    //public void vSearchMstEmployeeByJobTitleQry(List<cArrayList> arr, ref SqlDataReader rs)
    //{
    //    dal.vGetRecordsetSP("sp_tmst_employee_searchbyjobtitleqry", ref rs, arr);
    //}
    public void vSearchMstEmployeeByJobTitle(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_employee_searchbyjobtitle", ref rs, arr);
    }
    public void vGetMstDiscount(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_discount_get", ref rs, arr);
    }
    //public void vGetpricelevel(List<cArrayList> arr, ref SqlDataReader rs)
    //{
    //    dal.vGetRecordsetSP("sp_getpricelevel", ref rs, arr);
    //}
    public void vInsertDiscountDocument(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdiscount_document_ins", arr);
    }
    public void vInsertDiscountIssued(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdiscount_issued_ins", arr);
    }
    public void vInsertDiscountCusttype(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdiscount_custtype_ins", arr);
    }
    public void vInsertDiscountCusGrCd(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdiscount_cusgrcd_ins", arr);
    }
    public void vInserttblSO(List<cArrayList> arr, ref string sSOCD)
    {
        dal.vExecuteSP("sp_tblSO_ins", arr, "@SOCD", ref sSOCD);
    }
    public void vUpdatetblSO(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tblSO_upd", arr);
    }
    public void vInsertTblSODtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tblSOdtl_ins", arr);
    }
    public void vGetTblSO(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tblso_get3", ref rs, arr);
    }
    public void vDeleteTblSODet(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tblSODet_Del", arr);
    }
    public void vDeleteTblSO(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tblSO_Del", arr);
    }
    public void vUpdTransStatus(List<cArrayList> arr)
    {
        dal.vExecuteSP("sprUpdTransStatus", arr);
    }
    public void vSearchMstCust2(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_customer_search2", ref rs, arr);
    }
    public void vInsertTblARRec(List<cArrayList> arr, ref string sARRecCD)
    {
        dal.vExecuteSP("sp_tblARRec_ins", arr, "@ARRecCD", ref sARRecCD);
    }
    public void vUpdateTblARRec(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tblARRec_upd", arr);
    }
    public void vGettblARRec(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tblARRec_get2", ref rs, arr);
    }
    public void vInsertTblARRecDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tblARRecdtl_ins", arr);
    }
    public void vDeleteTblARRec(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tblARRec_Del", arr);
    }
    public void vDeletetblARRecDet(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tblARRecDet_Del", arr);
    }
    public void vInserttblReturn(List<cArrayList> arr, ref string sreturnCD)
    {
        dal.vExecuteSP("sp_tblReturn_ins", arr, "@returnCD", ref sreturnCD);
    }
    public void vInsertTblReturnDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tblreturndtl_ins", arr);
    }
    public void vGetTblReturn(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tblreturn_get2", ref rs, arr);
    }
    public void vUpdatetblReturn(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tblreturn_upd", arr);
    }
    public void vDeleteTblReturn(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tblReturn_Del", arr);
    }
    public void vDeleteTblReturnDet(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tblReturnDet_Del", arr);
    }
    public void vSearchMstVehicle(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_vehicle_search", ref rs, arr);
    }
    public void vSearchMstwarehouse(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_warehouse_search", ref rs, arr);
    }
    public void vInsertWrkCustType(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_custtype_ins", arr);
    }
    public void vInsertWrkCusGrCd(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_cusgrcd_ins", arr);
    }
    public void vInsertCustomerDocument(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcustomer_document_ins", arr);
    }
    public void vInsertSMSSent(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsms_sent_ins", arr);
    }
    public void vUpdateMstDiscount(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_discount_upd", arr);
    }
    public string sGetBranded(string sItemCode)
    {
        SqlDataReader rs = null; string sTemp = "N/A";
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@item_cd", sItemCode));
        dal.vGetRecordsetSP("sp_getbranded", ref rs, arr);
        while (rs.Read())
        {
            sTemp = rs["prod_nm"].ToString();
        }
        rs.Close(); return (sTemp);
    }
    public void vInsertCustomerTypePrice(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcustomertype_price_ins", arr);
    }
    public void vInsertDiscountFreeitem(List<cArrayList> arr)
    {
        dal.vExecuteSP("tdiscount_freeitem_ins", arr);
    }
    public void vInsertDiscountItem(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_discount_item_ins", arr);
    }
    public void vInsertDiscountCustomer(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdiscount_customer_ins", arr);
    }
    public void vInsertWrkItemByProd(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_item_byproduct_ins", arr);
    }
    public void vInsertWrkCustByType(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_customer_bytype_ins", arr);
    }

    public void vInsertWrkCustByGroup(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_customer_bygroup_ins", arr);
    }

    public List<string> lGetApproval(string sEmpCD)
    {
        SqlDataReader rs = null;
        List<string> lTemp = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@emp_cd", sEmpCD));
        dal.vGetRecordsetSP("sp_tuser_profile_getbyempcd", ref rs, arr);
        while (rs.Read())
        {
            lTemp.Add(rs["mobile_no"].ToString());
            lTemp.Add(rs["email"].ToString());
        }
        rs.Close();
        return (lTemp);
    }
    public List<string> lGetApproval(string doc_typ, int levelno)
    {
        SqlDataReader rs = null;
        List<string> lTemp = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@doc_typ", doc_typ));
        arr.Add(new cArrayList("@level_no", levelno));
        dal.vGetRecordsetSP("sp_tapprovalpattern_get", ref rs, arr);
        while (rs.Read())
        {
            lTemp.Add(rs["mobile_no"].ToString());
            lTemp.Add(rs["email"].ToString());
        }
        rs.Close();
        return (lTemp);
    }

    public void vDelwrkFormulaDiscount(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_formuladiscount_del", arr);
    }
    public void vGetWrkFormulaDiscount(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_twrk_formuladiscount_get", ref rs, arr);
    }
    public void vInsertWrkFomulaDiscount(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_formuladiscount_ins", arr);
    }
    public void vInsertRequestDiscount(List<cArrayList> arr, ref string sDiscNo)
    {
        dal.vExecuteSP("sp_trequest_discount_ins", arr, "@request_no", ref sDiscNo);
    }
    public void vInsertWrkFreeItem2(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_freeitem_ins2", arr);
    }
    public void vDelWrkCustomer(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_customer_del", arr);
    }
    public void vInsertWrkCustomer(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_customer_ins", arr);
    }
    public void vGetTblSalesTarget2(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tblSalesTarget_get2", ref rs, arr);
    }
    //public void vInserttblSO(List<cArrayList> arr, ref string sSOCD)
    //{
    //    dal.vExecuteSP("sp_tblSO_ins", arr, "@SOCD", ref sSOCD);
    //}
    //public void vUpdatetblSO(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tblSO_upd", arr);
    //}
    //public void vInsertTblSODtl(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tblSOdtl_ins", arr);
    //}
    public void vSearchtblSalesTargetDet(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_TblSalesTargetDet_search", ref rs, arr);
    }
    public void vGetTblSalesTargetDet(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tblSalesTargetDet_get2", ref rs, arr);
    }
    public void vInsertTblSalesTargetSP(List<cArrayList> arr, ref string sslsTargetSPCD)
    {
        dal.vExecuteSP("sp_tblSalesTargetSP_ins", arr, "@slsTargetSPCD", ref sslsTargetSPCD);
    }
    public void vUpdateTblSalesTargetSP(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tblSalesTargetSP_upd", arr);
    }
    public void vGetTblSalesTargetSP(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tblSalesTargetSP_get2", ref rs, arr);
    }
    public void vInserttblSalesTargetSPDet(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tblSalesTargetSPDet_ins", arr);
    }
    public void vDeletetblSalesTargetSPDet(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tblSalesTargetSPDet_Del", arr);
    }
    //public void vGetTblSO(List<cArrayList> arr, ref SqlDataReader rs)
    //{
    //    dal.vGetRecordsetSP("sp_tblso_get3", ref rs, arr);
    //}
    //public void vDeleteTblSODet(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tblSODet_Del", arr);
    //}
    //public void vDeleteTblSO(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tblSO_Del", arr);
    //}
    //public void vUpdTransStatus(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sprUpdTransStatus", arr);
    //}
    //public void vSearchMstCust2(List<cArrayList> arr, ref SqlDataReader rs)
    //{
    //    dal.vGetRecordsetSP("sp_tmst_customer_search2", ref rs, arr);
    //}
    public void vBindingComboToSp2(ref DropDownList cbo, string sSPName, string sVal, string sDisp, List<cArrayList> arr, string sAddValue)
    {
        SqlDataReader rs = null;
        DataTable dta = new DataTable();
        dal.vGetRecordsetSP(sSPName, ref rs, arr);
        dta.Load(rs);
        cbo.DataValueField = sVal;
        cbo.DataTextField = sDisp;
        cbo.DataSource = dta.DefaultView;
        cbo.DataBind();
        ListItem itm = new ListItem();
        itm.Value = sAddValue;
        itm.Text = "";
        cbo.Items.Add(itm);
        cbo.SelectedValue = sAddValue;

    }
    public void vGetApprovalPattern(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tapprovalpattern_get", ref rs, arr);
    }
    public void vSendMail(string sTo, string sSubject, string sMessage)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@to", sTo));
        arr.Add(new cArrayList("@mailtext", sMessage));
        arr.Add(new cArrayList("@mailsubject", sSubject));
        dal.vExecuteSP("sp_sendmail", arr);
    }
    public void vInsertGoodReceiptDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tgoodreceipt_dtl_ins2", arr);
    }
    public void vInsertMstGoodReceipt(List<cArrayList> arr, ref string sReceiptNo)
    {
        dal.vExecuteSP("sp_tmst_goodreceipt_ins", arr, "receipt_no", ref sReceiptNo);
    }
    public void vInsertManifestDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmanifest_dtl_ins", arr);
    }
    public void vGetMstDO(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_do_get", ref rs, arr);
    }
    public void vDelWrkBranchInvoice(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_branchinvoice_del", arr);
    }
    public void vInsertWrkBranchInvoice(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_branchinvoice_ins", arr);
    }
    public void vInsertWrkPoDtl2(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_podtl_ins2", arr);
    }
    public void vGetManifestInvoice(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmanifest_invoice_get", ref rs, arr);
    }
    public void vInsertManifestInvoice(List<cArrayList> arr, ref string sTripNo)
    {
        dal.vExecuteSP("sp_tmanifest_invoice_ins", arr, "@trip_no", ref sTripNo);
    }
    public void vGetDoDtl(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tdo_dtl_get", ref rs, arr);
    }
    public void vSearchMstDO(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_do_search", ref rs, arr);
    }
    public void vGetPoDtl(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tpo_dtl_get", ref rs, arr);
    }
    public void vInsertPoDtl2(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tpo_dtl_ins2", arr);
    }
    public void vInsertWarehouseBin(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twarehouse_bin_ins", arr);
    }
    public void vInsertMstWarehouse(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_warehouse_ins", arr);
    }
    public void vInsertRequestCustomer(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_trequest_customer_ins", arr);
    }
    public void vInsertDiscountFormula(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdiscount_formula_ins", arr);
    }
    public void vGetShipmentHOSchedule(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tshipmentho_schedule_get", ref rs);
    }
    public void vInsertShipmentHOSchedule(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tshipmentho_schedule_ins", arr);
    }
    public void vInsertWrkItem2(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_item_ins2", arr);
    }
    public void vGetMstDocument(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_document_get", ref rs, arr);
    }
    public void vInsertPoHoDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tpoho_dtl_ins", arr);
    }
    public void vDelWrkPoDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_podtl_del", arr);
    }
    public void vInsertPoHODtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_poho_dtl_ins", arr);
    }
    public void vInsertMstPoHO(List<cArrayList> arr, ref string sPoNo)
    {
        dal.vExecuteSP("sp_tmst_poho_ins", arr, "@po_no", ref sPoNo);
    }
    public void vInsertErrorLog(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_terror_log_ins", arr);
    }
    public void vGetMstCustomerCategory(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_customercategory_get", ref rs, arr);
    }
    public void vInsertMonthYear(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_MonthYear_ins", arr);
    }
    public void vInsertYear(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_Year_ins", arr);
    }
    public void vGetYear(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_Year_get", ref rs, arr);
    }
    public void vInsertMonthYearWiz(List<cArrayList> arr)
    {
        dal.vExecuteSP("sprTRInsYearMonth", arr);
    }
    public void vGetTblYear(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_Year_get", ref rs, arr);
    }
    public void vDelMontYear(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tblTRYearMonth_del", arr);
    }
    public void vInserttblSalesTargetDet(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tblSalesTargetDet_ins", arr);
    }
    public void vDeletetblSalesTargetDet(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tblSalesTargetDet_Del", arr);
    }
    public void vInsertTblSalesTarget(List<cArrayList> arr, ref string sslsTargetCD)
    {
        dal.vExecuteSP("sp_tblSalesTarget_ins", arr, "@slsTargetCD", ref sslsTargetCD);
    }
    public void vUpdateTblSalesTarget(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tblSalesTarget_upd", arr);
    }
    public void vGetTblSalesTarget(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tblSalesTarget_get2", ref rs, arr);
    }
    public int nCheckToken(List<cArrayList> arr)
    {
        int nCheck = 0;
        SqlDataReader rs = null;
        dal.vGetRecordsetSP("sp_checktokenexist", ref rs, arr);
        while (rs.Read())
        {
            nCheck = Convert.ToInt32(rs["nCount"]);
        }
        rs.Close();
        return (nCheck);
    }
    public void vInsertSMSReceived(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tsms_received_ins", arr);
    }
    public void vGetWrkApproval(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_twrk_approval_get", ref rs, arr);
    }
    public void vGetAppMstCustomer(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_appmstcustomer_get", ref rs, arr);
    }
    public void vUpdateWrkApproval(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_approval_upd", arr);
    }
    public void vSearchMstPo(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_po_search", ref rs, arr);
    }
    public void vDelMstReason(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_reason_del", arr);
    }
    public void vInsertMstReason(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_reason_ins", arr);
    }
    public void vInsertPricelevelDtl2(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tpricelevel_dtl2_ins", arr);
    }
    public void vInsertPriceDtlToWrk(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tpricelevel_real_to_wrk_ins", arr);
    }
    public void vGetMstPriceLevel(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_pricelevel_get", ref rs);
    }
    public void vInsertPriceLevelDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tpricelevel_dtl_ins", arr);
    }
    public void vDelWrkPriceLevelDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_pricelevel_dtl_del", arr);
    }
    public void vGetWrkPriceLevelDtl(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_twrk_pricelevel_dtl_get", ref rs, arr);
    }
    public void vInsertWrkPriceLevelDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_pricelevel_dtl", arr);
    }
    //public void vInsertPriceLevelDtl(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tpricelevel_dtl_ins", arr);
    //}
    public void vInsertMstPriceLevel(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_pricelive_ins", arr);
    }
    public void vInsertWrkLookupCustomer(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_lookupcustomer_ins", arr);
    }
    public void vGetMstCustomer2(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_customer_get2", ref rs, arr);
    }
    public void vinsertMstCustomer(List<cArrayList> arr, ref string sCustNo)
    {
        dal.vExecuteSP("sp_tmst_customer_ins", arr, "@cust_cd", ref sCustNo);
    }
    public void vGetMstcustomer2(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_customer_get2", ref rs, arr);
    }
    public void vInsertMstProduct(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_product_ins", arr);
    }
    public void vGetMstProduct2(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_product_get2", ref rs, arr);
    }
    public void vGetMstProduct(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_product_get", ref rs, arr);
    }
    public void vDelTwrkPoDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_podtl_del", arr);
    }
    public void vInsertWrkPoDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_podtl_ins", arr);
    }
    public void vGetMstVendor2(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_vendor_get2", ref rs, arr);
    }
    public void vInsertWrkSalespointFromReal(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_salespoint_from_real", arr);
    }
    public void vInsertSalesPointFromWrk(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_titem_salespoint_from_wrk_ins", arr);
    }
    public void vInsertWrkItemConversionFromReal(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_uomconversion_from_real_ins", arr);
    }
    public void vInsertItemCoversionFromWrk(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_titem_coversion_from_wrk_ins", arr);
    }
    public void vGetSMSReceiver(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tsms_receiver_get", ref rs, arr);
    }
    public int nCheckUserPassword(string sUserID, string sPassword)
    {
        int nTemp = 0;
        SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", sUserID));
        arr.Add(new cArrayList("@passwd", sPassword));
        dal.vGetRecordsetSP("sp_checkuser", ref rs, arr);
        while (rs.Read())
        {
            nTemp = (int)rs["nCount"];
        }
        rs.Close();
        return (nTemp);
    }
    public void vSearchMstItem2(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_item_search2", ref rs, arr);
    }
    public void vtbltrnstockDtlGetQty(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tbltrnstockDtl_get_qty", ref rs, arr);
    }
    public void vDelWrkUomConversion(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_uomconversion_del", arr);
    }
    public void vGetWrkUomConversion(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_twrk_uomconversion_get", ref rs);
    }
    public void vInsertWrkUomConversion(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_uomconversion_ins", arr);
    }
    public void vSearchMstSalespoint(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_salespoint_search", ref rs, arr);
    }
    public void vInsertItemSalespoint(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_titem_salespoint_ins", arr);
    }
    public void vGetMstLocation3(ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_location_get3", ref rs);
    }
    public void vGetMstSalespoint(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_salespoint_get", ref rs, arr);
    }

    public void vGetMstSalespoint(ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_salespoint_get", ref rs);
    }
    public void vGetUserProfile(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tuser_profile_get", ref rs, arr);
    }
    public void vDelWrkFreeItem(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_freeitem_del", arr);
    }
    public void vInsertWrkSalespointAll(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_salespointall_ins", arr);
    }
    public void vDelWrkCusGrCD(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_cusgrcd_del", arr);
    }
    public void vGetWrkCustGrCD(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_twrk_cusgrcd_get", ref rs, arr);
    }
    public void vInsertWrkCustGrCD(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_cusgrcd_ins", arr);
    }
    public void vInsertWrkLookupCustomerByOtlCD(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_lookupcustomer_byotlcd", arr);
    }
    public void vInsertMstLocation(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_location_ins", arr);
    }
    public void vGetMstLocation2(ref SqlDataReader rs, List<cArrayList> arr)
    {
        dal.vGetRecordsetSP("sp_tmst_location_get2", ref rs, arr);
    }
    public void vGetMstLocation(ref SqlDataReader rs, List<cArrayList> arr)
    {
        dal.vGetRecordsetSP("sp_tmst_location_get", ref rs, arr);
    }
    public void vGetMstCity(ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_city_get", ref rs);
    }

    public void vGetMstCity(ref SqlDataReader rs, List<cArrayList> arr)
    {
        dal.vGetRecordsetSP("sp_tmst_city_get", ref rs, arr);
    }
    public void vinsertMstCity(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_city_ins", arr);
    }

    public void vGetWrkLookupItem(ref SqlDataReader rs, List<cArrayList> arr)
    {
        dal.vGetRecordsetSP("sp_twrk_lookupitem_get", ref rs, arr);
    }
    public void vDelWrkLookupItem(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_lookupitem_del", arr);
    }
    public void vDelWrkLookupCustomer(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_lookupcustomer_del", arr);
    }
    //public void vInsertWrkLookupCustomer(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_twrk_lookupcustomer_ins", arr);
    //}
    public void vBatchDO(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tbatch_do", arr);
    }
    public void vGetPO4DO(ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_po_search4do", ref rs);
    }
    public void vInsertMstDO(List<cArrayList> arr, ref string sDONo)
    {
        dal.vExecuteSP("sp_tmst_do_ins", arr, "@do_no", ref sDONo);
    }
    public void vInsertDoDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdo_dtl_ins", arr);
    }
    public void vDelWrkLookupPO(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_lookuppo_del", arr);
    }
    public void vGetWrkLookupPO(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_twrk_lookuppo_get", ref rs, arr);
    }
    public void vInsertWrkLookupPO(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_lookuppo_ins", arr);
    }
    public void vGetMstCustomer(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_customer_get", ref rs, arr);
    }
    public double dblSumTmpPoDtl(List<cArrayList> arr)
    {
        double dTemp = 0;
        SqlDataReader rs = null;
        dal.vGetRecordsetSP("sp_sumtmppodtl", ref rs, arr);
        while (rs.Read())
        {
            dTemp = Convert.ToDouble(rs["subtotal"]);
        }
        rs.Close(); return (dTemp);
    }
    //public void vSearchMstCustomer(List<cArrayList> arr, ref SqlDataReader rs)
    //{
    //    dal.vGetRecordsetSP("sp_tmst_customer_search", ref rs, arr);
    //}
    public void vGetMstWarehouse(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_warehouse_get", ref rs, arr);
    }
    public void vInsertWrkLookupItem(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_lookupitem_ins", arr);
    }
    public void vInsertSchemaMethodItem(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tschema_method_item_ins", arr);
    }
    public void vInsertWrkFreeItem(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_freeitem_ins", arr);
    }
    public void vInsertSchemaCustType(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tschema_custtype_ins", arr);
    }
    public void vInsertSchemaSalespoint(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tschema_salespoint_ins", arr);
    }
    public void vInsertSchemaItem(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tschema_item_ins", arr);
    }
    public void vGetDocReject(List<cArrayList> arr, SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tdoc_reject_get", ref rs, arr);
    }
    public void vInsertDocReject(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tdoc_reject_ins", arr);
    }
    public void vInsertSchemaFreeItem(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tschema_freeitem_ins", arr);
    }
    public void vDelWrkLookup(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_lookup2_del", arr);
    }
    public void vInsertWrkLookup(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_lookup2_ins", arr);
    }
    public void vDelWrkDiscMethod(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_discmethod_del", arr);
    }
    public void vInsertWrkDiscMethod(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_discmethod_ins", arr);
    }
    public void vDelWrkItem(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_item_del", arr);
    }
    public void vInsertWrkItem(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_item_ins", arr);
    }
    public void vDelWrkCustType(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_custtype_del", arr);
    }
    public void vInsertWrkCustTypeAll(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_custtypeall_ins", arr);
    }
    public void vInsertTwrkCustType(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_custtype_ins", arr);
    }
    public void vDelWrkSalespoint(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_salespoint_del", arr);
    }

    public void vGetWrkSalespoint(ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_twrk_salespoint_get", ref rs);
    }

    public void vInsertWrkSalespoint(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_twrk_salespoint_ins", arr);
    }
    public void vLang(ref Label lbl)
    {
        lbl.Text = vLookUp("select arabic_word from tmap_lang where latin_word='" + lbl.Text + "'");
    }

    public void vLang(ref Button lbl)
    {
        lbl.Text = vLookUp("select arabic_word from tmap_lang where latin_word='" + lbl.Text + "'");
    }
    public void vGetMapLang(ref SqlDataReader rs, List<cArrayList> arr)
    {
        dal.vGetRecordsetSP("sp_tmap_lang_get", ref rs, arr);
    }
    public void vInsertItemPrice(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_titem_price_ins", arr);
    }
    public void vInsertMstEmployee(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_employee_ins", arr);
    }
    public void vGetMstEmployee(ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_employee_get", ref rs);
    }
    public void vGetMstEmployee(ref SqlDataReader rs, List<cArrayList> arr)
    {
        dal.vGetRecordsetSP("sp_tmst_employee_get", ref rs, arr);
    }
    public void vInsertMstSchema(List<cArrayList> arr, ref string sOut)
    {
        dal.vExecuteSP("sp_tmst_schema_ins", arr, "@sch_cd", ref sOut);
    }
    public void vGetMstItem2(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_item_get2", ref rs, arr);
    }
    public double dSumOfPO()
    {
        double dTemp = 0;
        SqlDataReader rs = null;
        dal.vGetRecordsetSP("sp_gettotalpo", ref rs);
        while (rs.Read())
        { dTemp = Convert.ToDouble(rs["nSum"]); }
        rs.Close();
        return (dTemp);
    }
    public void vInsertPoDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tpo_dtl_ins", arr);
    }
    public void vGetMstPO(ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_po_get", ref rs);
    }
    public void vGetMstPO(ref SqlDataReader rs, List<cArrayList> arr)
    {
        dal.vGetRecordsetSP("sp_tmst_po_get", ref rs, arr);
    }
    public void vInsertMstPO(List<cArrayList> arr, ref string sPoNO)
    {
        dal.vExecuteSP("sp_tmst_po_ins", arr, "@po_no", ref sPoNO);
    }
    public void vDelTmpPoDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmp_po_dtl_del", arr);
    }
    public void vInsertTmpPoDtl(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmp_po_dtl_ins", arr);
    }
    public void vSearchMstItem(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_item_search", ref rs, arr);
    }
    public void vInsertUserProfile(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tuser_profile_ins", arr);
    }
    public void vGetMstItem(ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_item_get", ref rs);
    }
    public void vInsertMstItem(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_item_ins", arr);
    }
    public void vGetMstVendor(ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_vendor_get", ref rs);
    }
    public void vGetMstVendor(ref SqlDataReader rs, List<cArrayList> arr)
    {
        dal.vGetRecordsetSP("sp_tmst_vendor_get", ref rs, arr);
    }
    public void vInsertMstVendor(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_vendor_ins", arr);
    }
    public void vBindingGridToSp(ref GridView grd, string sSPName)
    {
        SqlDataReader rs = null;
        dal.vGetRecordsetSP(sSPName, ref rs);
        DataTable dta = new DataTable();
        dta.Load(rs);
        grd.DataSource = dta.DefaultView;
        grd.DataBind();
    }

    public void vBindingGridToSp(ref GridView grd, string sSPName, List<cArrayList> arr)
    {
        SqlDataReader rs = null;
        dal.vGetRecordsetSP(sSPName, ref rs, arr);
        DataTable dta = new DataTable();
        dta.Load(rs);
        grd.DataSource = dta.DefaultView;
        grd.DataBind();
    }
    public void vGetMstGroupVendor(ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_group_vendor_get", ref rs);
    }
    public void vInsertMstGroupVendor(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tmst_group_vendor_ins", arr);
    }
    public string vLookUp(string sSQL)
    {
        string sTemp = string.Empty;
        SqlDataReader rsx = null;
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = cd.getConnection();
        cmd.CommandText = sSQL;
        cmd.CommandType = CommandType.Text;
        rsx = cmd.ExecuteReader();
        while (rsx.Read())
        {
            sTemp = rsx[0].ToString();
        }
        rsx.Close();
        return (sTemp);
    }

    public string sGetControlParameterSalespoint(string sParmName, string sSp)
    {
        string sTemp = "";
        List<cArrayList> arr = new List<cArrayList>();
        SqlDataReader rs = null;
        arr.Add(new cArrayList("@salespointcd", sSp));
        arr.Add(new cArrayList("@parm_nm", sParmName));
        dal.vGetRecordsetSP("sp_tsalespoint_parameter_get", ref rs, arr);
        while (rs.Read())
        {
            sTemp = rs["parm_valu"].ToString();
        }
        rs.Close();
        return (sTemp);
    }

    public string sGetControlParameter(string sParmName)
    {
        string sTemp = "";
        List<cArrayList> arr = new List<cArrayList>();
        SqlDataReader rs = null;
        arr.Add(new cArrayList("@parm_nm", sParmName));
        dal.vGetRecordsetSP("sp_tcontrol_parameter_get", ref rs, arr);
        while (rs.Read())
        {
            sTemp = rs["parm_valu"].ToString();
        }
        rs.Close();
        return (sTemp);
    }

    public string sGetMessage(string sMsgID)
    {
        string sTemp = "";
        List<cArrayList> arr = new List<cArrayList>();
        SqlDataReader rs = null;
        arr.Add(new cArrayList("@msg_id", sMsgID));
        dal.vGetRecordsetSP("sp_tmessage_get", ref rs, arr);
        while (rs.Read())
        {
            sTemp = rs["msg_text"].ToString();
        }
        rs.Close();
        return (sTemp);
    }

    public void vUpdatetuserprofile(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tuser_profile_upd", arr);
    }

    public int nCheckEmail(string sUserID)
    {
        int ntemp = 0;
        SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", sUserID));
        dal.vGetRecordsetSP("sp_checkemail", ref rs, arr);
        while (rs.Read())
        { ntemp = Convert.ToInt16(rs["ncount"]); }
        rs.Close();
        return (ntemp);
    }

    public int nCheckUser(string username, string password)
    {
        int nTemp = 0;
        SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", username));
        arr.Add(new cArrayList("@usr_pwd", password));
        dal.vGetRecordsetSP("sp_checkuser", ref rs, arr);
        while (rs.Read())
        { nTemp = Convert.ToInt16(rs["ncount"]); }
        rs.Close();
        return (nTemp);
    }

    public string sendEmailSmarter(string sTo, string sBody)
    {
        string sEmail = "";
        MailMessage m = new MailMessage();
        SmtpClient sc = new SmtpClient();
        try
        {
            m.From = new MailAddress("irwan.agusyono@gmail.com");
            m.To.Add(sTo);
            m.Subject = "Registration alunalun.id";
            m.IsBodyHtml = true;
            m.Body = sBody;
            //sc.Host = "smtp.gmail.com";
            sc.Host = "smtp.gmail.com";
            sc.Port = 465;
            sc.Credentials = new System.Net.NetworkCredential("irwan.agusyono@gmail.com", "h4r1y4n1");

            sc.EnableSsl = true;
            sc.Send(m);
            sEmail = "Email Send successfully";
        }
        catch (Exception ex)
        {
            sEmail = ex.Message;
        }
        return (sEmail);
    }

    public string SendEmail()
    {
        string sTemp = "Email has sent !";
        System.Net.ICredentialsByHost ic;

        try
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add("irwan.agusyono@pirantaskarya.com");
            mailMessage.From = new MailAddress("irwan.agusyono@gmail.com");
            mailMessage.Subject = "ASP.NET e-mail test";
            mailMessage.Body = "Hello world,\n\nThis is an ASP.NET test e-mail!";
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Port = 465;
            smtpClient.EnableSsl = false;
            smtpClient.Send(mailMessage);
            // Response.Write("E-mail sent!");
        }
        catch (Exception ex)
        {
            sTemp = ex.Message;
        }
        return (sTemp);
    }

    //public void vInsertUserProfile(List<cArrayList> arr)
    //{
    //    dal.vExecuteSP("sp_tuser_profile_ins", arr);
    //}

    public void vBindingComboToSp(ref DropDownList cbo, string sSPName, string sVal, string sDisp, List<cArrayList> arr)
    {
        SqlDataReader rs = null;
        DataTable dta = new DataTable();
        dal.vGetRecordsetSP(sSPName, ref rs, arr);
        dta.Load(rs);
        cbo.DataValueField = sVal;
        cbo.DataTextField = sDisp;
        cbo.DataSource = dta.DefaultView;
        cbo.DataBind();
    }

    public void vBindingComboToSp(ref ListBox cbo, string sSPName, string sVal, string sDisp, List<cArrayList> arr)
    {
        SqlDataReader rs = null;
        DataTable dta = new DataTable();
        dal.vGetRecordsetSP(sSPName, ref rs, arr);
        dta.Load(rs);
        cbo.DataValueField = sVal;
        cbo.DataTextField = sDisp;
        cbo.DataSource = dta.DefaultView;
        cbo.DataBind();
    }
    public void vBindingComboToSpMulti(ref ListBox cbo, string sSPName, string sVal, string sDisp, List<cArrayList> arr)
    {
        SqlDataReader rs = null;
        DataTable dta = new DataTable();
        dal.vGetRecordsetSP(sSPName, ref rs, arr);
        dta.Load(rs);
        cbo.DataValueField = sVal;
        cbo.DataTextField = sDisp;
        cbo.DataSource = dta.DefaultView;
        cbo.DataBind();
    }

    public void vBindingComboToSp(ref DropDownList cbo, string sSPName, string sVal, string sDisp, List<cArrayList> arr, string sAddValue)
    {
        SqlDataReader rs = null;
        DataTable dta = new DataTable();
        dal.vGetRecordsetSP(sSPName, ref rs, arr);
        dta.Load(rs);
        cbo.DataValueField = sVal;
        cbo.DataTextField = sDisp;
        cbo.DataSource = dta.DefaultView;
        cbo.DataBind();
        ListItem itm = new ListItem();
        itm.Value = sAddValue;
        itm.Text = "--- ALL ---";
        cbo.Items.Add(itm);
        cbo.SelectedValue = sAddValue;

    }

    public void vBindingComboToSp(ref DropDownList cbo, string sSPName, string sVal, string sDisp, string sAddItemValue)
    {
        SqlDataReader rs = null;
        DataTable dta = new DataTable();
        dal.vGetRecordsetSP(sSPName, ref rs);
        dta.Load(rs);
        cbo.DataValueField = sVal;
        cbo.DataTextField = sDisp;
        cbo.DataSource = dta.DefaultView;
        cbo.DataBind();
        ListItem itm = new ListItem();
        itm.Value = sAddItemValue;
        itm.Text = "--- ALL ---";
        cbo.Items.Add(itm);
        cbo.SelectedValue = sAddItemValue;

    }

    public void vBindingFieldValueToComboByQryWithEmptyChoosen(ref DropDownList cbo, string sParamName, string sQryCD)
    {
        SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@fld_nm", sParamName));
        arr.Add(new cArrayList("@qry_cd", sQryCD));
        DataTable dta = new DataTable();
        dal.vGetRecordsetSP("sp_tfield_value_getbyqry", ref rs, arr);
        dta.Load(rs);
        cbo.DataValueField = "fld_valu";
        cbo.DataTextField = "fld_desc";
        cbo.DataSource = dta.DefaultView;
        cbo.DataBind();
        cbo.Items.Insert(0, new ListItem("--- Select ---", string.Empty));
        cbo.SelectedIndex = 0;
    }
    public void vBindingFieldValueToComboWithChoosen(ref DropDownList cbo, string sParamName)
    {
        SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@fld_nm", sParamName));
        DataTable dta = new DataTable();
        dal.vGetRecordsetSP("sp_tfield_value_get", ref rs, arr);
        dta.Load(rs);
        cbo.DataValueField = "fld_valu";
        cbo.DataTextField = "fld_desc";
        cbo.DataSource = dta.DefaultView;
        cbo.DataBind();
        cbo.Items.Insert(0, new ListItem("--- Select ---", string.Empty));
        cbo.SelectedIndex = 0;
    }
    public void vBindingComboToSpWithEmptyChoosen(ref DropDownList cbo, string sSPName, string sVal, string sDisp, List<cArrayList> arr)
    {
        SqlDataReader rs = null;
        DataTable dta = new DataTable();
        dal.vGetRecordsetSP(sSPName, ref rs, arr);
        dta.Load(rs);
        cbo.DataValueField = sVal;
        cbo.DataTextField = sDisp;
        cbo.DataSource = dta.DefaultView;
        cbo.DataBind();
        cbo.Items.Insert(0, new ListItem("--- Select ---", string.Empty));
        cbo.SelectedIndex = 0;

    }
    public void vBindingComboToSpWithEmptyChoosen(ref DropDownList cbo, string sSPName, string sVal, string sDisp)
    {
        SqlDataReader rs = null;
        DataTable dta = new DataTable();
        dal.vGetRecordsetSP(sSPName, ref rs);
        dta.Load(rs);
        cbo.DataValueField = sVal;
        cbo.DataTextField = sDisp;
        cbo.DataSource = dta.DefaultView;
        cbo.DataBind();
        cbo.Items.Insert(0, new ListItem("--- Select ---", string.Empty));
        cbo.SelectedIndex = 0;

    }

    public void vBindingComboToSp(ref DropDownList cbo, string sSPName, string sVal, string sDisp)
    {
        SqlDataReader rs = null;
        DataTable dta = new DataTable();
        dal.vGetRecordsetSP(sSPName, ref rs);
        dta.Load(rs);
        cbo.DataValueField = sVal;
        cbo.DataTextField = sDisp;
        cbo.DataSource = dta.DefaultView;
        cbo.DataBind();
    }

    public void vBindingFieldValueToComboWithALL(ref DropDownList cbo, string sParamName)
    {
        SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@fld_nm", sParamName));
        DataTable dta = new DataTable();
        dal.vGetRecordsetSP("sp_tfield_value_getbyall", ref rs, arr);
        dta.Load(rs);
        cbo.DataValueField = "fld_valu";
        cbo.DataTextField = "fld_desc";
        cbo.DataSource = dta.DefaultView;
        cbo.DataBind();

    }
    public void vBindingFieldValueToCombo(ref DropDownList cbo, string sParamName)
    {
        SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@fld_nm", sParamName));
        DataTable dta = new DataTable();
        dal.vGetRecordsetSP("sp_tfield_value_get", ref rs, arr);
        dta.Load(rs);
        cbo.DataValueField = "fld_valu";
        cbo.DataTextField = "fld_desc";
        cbo.DataSource = dta.DefaultView;
        cbo.DataBind();

    }
    public void vBindingFieldValueToCombo(ref DropDownList cbo, string sParamName, int nFlagArabic)
    {
        SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@fld_nm", sParamName));
        DataTable dta = new DataTable();
        dal.vGetRecordsetSP("sp_tfield_value_get", ref rs, arr);
        dta.Load(rs);
        cbo.DataValueField = "fld_valu";
        cbo.DataTextField = "fld_arabic";
        cbo.DataSource = dta.DefaultView;
        cbo.DataBind();

    }

    public void vBindingFieldValueToCombo(ref DropDownList cbo, string sParamName, bool bHiddenData)
    {
        SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@hiddendata", bHiddenData));
        arr.Add(new cArrayList("@fld_nm", sParamName));
        DataTable dta = new DataTable();
        dal.vGetRecordsetSP("sp_tfield_value_get", ref rs, arr);
        dta.Load(rs);
        cbo.DataValueField = "fld_valu";
        cbo.DataTextField = "fld_desc";
        cbo.DataSource = dta.DefaultView;
        cbo.DataBind();
    }

    public void vBindingFieldValueToCombo(ref DropDownList cbo, string qry_cd, string field_nm)
    {
        SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@fld_nm", field_nm));
        arr.Add(new cArrayList("@qry_cd", qry_cd));
        DataTable dta = new DataTable();
        dal.vGetRecordsetSP("sp_tfield_value_getbyqry", ref rs, arr);
        dta.Load(rs);
        cbo.DataValueField = "fld_valu";
        cbo.DataTextField = "fld_desc";
        cbo.DataSource = dta.DefaultView;
        cbo.DataBind();
    }

    public void vBindingFieldValueToComboWithEmptyChoosen(ref DropDownList cbo, string qry_cd, string field_nm)
    {
        SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@fld_nm", field_nm));
        arr.Add(new cArrayList("@qry_cd", qry_cd));
        DataTable dta = new DataTable();
        dal.vGetRecordsetSP("sp_tfield_value_getbyqry", ref rs, arr);
        dta.Load(rs);
        cbo.DataValueField = "fld_valu";
        cbo.DataTextField = "fld_desc";
        cbo.DataSource = dta.DefaultView;
        cbo.DataBind();
        cbo.Items.Insert(0, new ListItem("--- Select ---", string.Empty));
        cbo.SelectedIndex = 0;
    }
    public void vGetTblTrnStockDocumentUpd(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tblTrnStock_get_document_upd", ref rs, arr);
    }
    public void vBindingFieldValueToCombo(ref DropDownList cbo, List<cArrayList> arr)
    {
        SqlDataReader rs = null; DataTable dta = new DataTable();
        dal.vGetRecordsetSP("sp_tfield_value_get2", ref rs, arr);
        dta.Load(rs);
        cbo.DataValueField = "fld_valu";
        cbo.DataTextField = "fld_desc";
        cbo.DataSource = dta.DefaultView;
        cbo.DataBind();

    }
    //public void vBindingFieldValueToCombo(ref DropDownList cbo, string sParamName,string sAddValue)
    //{
    //    SqlDataReader rs = null;
    //    List<cArrayList> arr = new List<cArrayList>();
    //    arr.Add(new cArrayList("@fld_nm", sParamName));
    //    DataTable dta = new DataTable();
    //    dal.vGetRecordsetSP("sp_tfield_value_get", ref rs, arr);
    //    dta.Load(rs);
    //    cbo.DataValueField = "fld_valu";
    //    cbo.DataTextField = "fld_desc";
    //    cbo.DataSource = dta.DefaultView;
    //    cbo.DataBind();
    //    ListItem itm = new ListItem();
    //    itm.Value = sAddValue;
    //    itm.Text = "--- ALL ---";
    //    cbo.Items.Add(itm);
    //    cbo.SelectedValue = sAddValue;
    //}


    public void vInitMenuCategory(ref TreeView tvw)
    {
        List<cArrayList> arr = new List<cArrayList>();
        SqlDataReader rs = null;
        arr.Add(new cArrayList("@parent_id", "0"));
        dal.vGetRecordsetSP("sp_tcategory_get", ref rs, arr);
        tvw.Nodes.Clear();
        TreeNode tn = new TreeNode();
        tn.Text = "KATAGORI";
        tn.Value = "root";

    }
    public void vGetTrnstkno(string dt, string sSP, ref SqlDataReader rs)
    {
        dal.vGetRecordSql("select  trnstkno from tblTrnStock where sta_id='N' and invtype='12' and trn_trnstkdate='" + dt + "' and salespointcd='" + sSP + "'", ref rs);
    }
    public void vGetDestApp(string dt, string sSp, ref SqlDataReader rs)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@sys_date", dt));
        arr.Add(new cArrayList("@salespointcd", sSp));
        dal.vGetRecordsetSP("sp_Destroy_appv", ref rs, arr);
    }
    //public void doGenerateDepreciationSchedule(List<cArrayList> arrDepre)
    //{
    //    dal.vExecuteSP("sp_tacc_depreciationschedule_generate", arrDepre);
    //}
    public void vGenerateAssetAdjustment(List<cArrayList> arr, ref string retValue)
    {
        dal.vExecuteSP("sp_tacc_tpurchase_ins", arr, "@assetPurchaseRegNo", ref retValue);
    }
    public void vDeleteAssetAdjustment(List<cArrayList> arr, ref string retValue)
    {
        dal.vExecuteSP("sp_tacc_tpurchase_del", arr, "@assetPurchaseRegNo", ref retValue);
    }
    public void vDeleteAssetAdjustment(List<cArrayList> arr)
    {
        throw new NotImplementedException();
    }
    public void vUpdateAssetPurchaseAdjustment(List<cArrayList> arr2)
    {
        dal.vExecuteSP("sp_tacc_depreciationschedule_adjustment", arr2);
    }
    public void vSearchMstBank(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tacc_bank_search", ref rs, arr);
    }
    public void vSearchMstSupplier(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tacc_supplier_search", ref rs, arr);
    }
    public void vSearchMstSalesman(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tacc_salesman_search", ref rs, arr);
    }
    public void vSearchMstCustomer2(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tacc_customer_search", ref rs, arr);
    }
    public void vSearchMstDepartment(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tacc_department_search", ref rs, arr);
    }
    public void vSearchMstCOA(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tacc_account_search", ref rs, arr);
    }

    public string vGetLatestJournalNo(List<cArrayList> arr)
    {
        SqlDataReader rs = null; string sTemp = "N/A";

        dal.vGetRecordsetSP("sp_latestJournalno_get", ref rs, arr);

        while (rs.Read())
        {

            sTemp = rs["field"].ToString();

        }
        rs.Close(); return (sTemp);
    }
    public void vDeletemanualjournaldet(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tacc_manual_journal_det_del", arr);
    }
    public void vDeletemanualjournal(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tacc_manual_journal_del", arr);
    }
    public void vInsertmanualjournal(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tacc_manual_journal_ins", arr);
    }
    public void vReturnmanualjournal(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tacc_manual_journal_return", arr);
    }
    public void vApprovemanualjournal(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tacc_manual_journal_approve", arr);
    }
    //public void vIsJournalEmptyGrid(List<cArrayList> arr)
    //{
    //    System.Data.SqlClient.SqlCommand sqlcomm = new System.Data.SqlClient.SqlCommand("sp_journal_get", cd.getConnection());
    //    SqlParameter retval = sqlcomm.Parameters.Add("@isEmptyGrid", SqlDbType.VarChar);
    //    retval.Direction = ParameterDirection.ReturnValue;
    //    sqlcomm.ExecuteNonQuery(); // MISSING
    //    string isEmptyGrid = (string)sqlcomm.Parameters["@isEmptyGrid"].Value;
    //}
    public void vIsJournalEmptyGrid(List<cArrayList> arr, ref string retValue)
    {
        dal.vExecuteSP("sp_journal_get", arr, "@isEmptyGridStr", ref retValue);
    }
    public void vSavemanualjournal(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tacc_manual_journal_save", arr);
    }
    public void vIsFinreportEmptyGrid(List<cArrayList> arr, ref string retValue)
    {
        dal.vExecuteSP("sp_tacc_fin_report_get", arr, "@isEmptyGridStr", ref retValue);
    }
    public void vSaveSettingFinReport(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tacc_fin_report_save", arr);
    }
    public void vAddRowSettingFinReport(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tacc_fin_report_add", arr);
    }
    public void vDeletefinreportdet(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tacc_fin_report_det_del", arr);
    }
    public void vDeletefinreport(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tacc_fin_report_del", arr);
    }
    public void vInsAssetPurchase(List<cArrayList> arr, ref string retValue)
    {
        dal.vExecuteSP("sp_tacc_tpurchase_ins", arr, "@assetPurchaseRegNo", ref retValue);
    }
    public void vUpdateAssetPurchase(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tacc_tpurchase_upd", arr);
    }
    public void vDeleteAssetPurchase(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tacc_tpurchase_del", arr);
    }
    public void vAppDestroy(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_apprv_trnstock", arr);
    }
    public void vGetTranstckApp(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_get_dstry_app", ref rs, arr);
    }
    public void vUpdateClosePeriod(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tacc_closePeriod", arr);
    }
    public void vUpdateClaimCashoutRequestByStatus(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tproposal_paid_updbystatus", arr);
    }
    public void vGetPettyCashEmp2(ref SqlDataReader rs, List<cArrayList> arr)
    {
        dal.vGetRecordsetSP("sp_temployee_advancedEmp_get2", ref rs, arr);
    }
    public void vInsertPettycashCashoutHandover(List<cArrayList> arr, ref string sDoc_no)
    {
        dal.vExecuteSP("sp_temployee_advanced_hov_ins", arr, "@doc_no", ref sDoc_no);
    }
    public void vInsertCashoutRequestHTE(List<cArrayList> arr)
    {
        dal.vExecuteSP("sp_tcashout_request_hte_ins", arr);
    }
    public void vIsCashAdvanceEmptyGrid(List<cArrayList> arr, ref string retValue)
    {
        dal.vExecuteSP("sp_acc_cash_advance_get", arr, "@isEmptyGridStr", ref retValue);
    }
    public void vIsCashAdvanceSettlementEmptyGrid(List<cArrayList> arr, ref string retValue)
    {
        dal.vExecuteSP("sp_acc_cash_advance_settlement_get", arr, "@isEmptyGridStr", ref retValue);
    }
    public void vIsCashAdvanceSettlementClaimEmptyGrid(List<cArrayList> arr, ref string retValue)
    {
        dal.vExecuteSP("sp_acc_cash_advance_settlement_claim_get", arr, "@isEmptyGridStr", ref retValue);
    }
    public void vSearchItemCashoutPurpose(List<cArrayList> arr, ref SqlDataReader rs)
    {
        dal.vGetRecordsetSP("sp_tmst_itemcashout_lookup_get", ref rs, arr);
    }
}
