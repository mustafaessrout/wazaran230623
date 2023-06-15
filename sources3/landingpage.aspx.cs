using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class landingpage : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Data.SqlClient.SqlDataReader rs = null;
        if (!IsPostBack)
        {
            //by yanto 6-1-2019
            HttpCookie scookusersql = new HttpCookie("usersql");
            scookusersql = Request.Cookies["usersql"];
            if (scookusersql == null)
            {
                Response.Cookies["usersql"].Value = "sa"; 
            }
            //-------------------------
            string sAppCode = Request.QueryString["appcode"];
            string sStatus = Request.QueryString["sta"];
            string strnname = Request.QueryString["trnname"];
            string ssalespointcd = Request.QueryString["salespointcd"];
            string sToken = string.Empty;
            string sDocNo = string.Empty;
            string sCheck = string.Empty;
            string sRefno = Request.QueryString["RefNo"];
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@token", sAppCode));
            arr.Add(new cArrayList("@doc_no", sRefno));
            if (strnname != "retho")
            {
                bll.vGetEmailSent(arr, ref rs);
                while (rs.Read())
                {
                    sToken = rs["token"].ToString();
                    sDocNo = rs["doc_no"].ToString();
                } rs.Close();
            }
            string sCheckStatus="";
            if (strnname == "casregout") 
            //{ sCheckStatus = bll.vLookUp("select cashout_sta_id from tcashregout where casregout_cd='" + sDocNo + "' and salespointcd='" + ssalespointcd + "'"); }
            { sCheckStatus = bll.vLookUp("select cashout_sta_id from tcashregout_dtl where casregout_cd='" + sDocNo + "'"); }
            else if (strnname == "trnstock") { sCheckStatus = bll.vLookUp("select sta_id from tbltrnstock where trnstkno='" + sDocNo + "' and salespointcd='" + ssalespointcd + "'"); }
            else if (strnname == "trnstock") { sCheckStatus = bll.vLookUp("select sta_id from tbltrnstock where trnstkno='" + sDocNo + "' and salespointcd='" + ssalespointcd + "'"); }
            else if (strnname == "internaltransfer")
            { sCheckStatus = bll.vLookUp("select sta_id from tinternal_transfer where trf_no='" + sDocNo + "'"); }
            else if (strnname == "canvasorder")
            { sCheckStatus = bll.vLookUp("select app_sta_id from tcanvasorder_info where so_cd='" + sDocNo + "'"); }
            else if (strnname == "retho")
            {
                sCheckStatus = bll.vLookUp("select reqretho_sta_id from treturnho_booking where IDS='" + sAppCode + "'");
            }
            if (sCheckStatus.Equals("A") || (sCheckStatus.Equals("R")) || (sCheckStatus.Equals("C")))
            {
                lbstatus.Text = "Ooops there are already proceed in approval process, please contact Wazaran Admin !";
                return;
            
            }
            
            if (!sToken.Equals(string.Empty) || strnname=="retho")
            {
                arr.Clear();
                if (strnname == "casregout")
                {
                    arr.Add(new cArrayList("@casregout_cd", sDocNo));
                    //arr.Add(new cArrayList("@salespointcd", ssalespointcd));
                    arr.Add(new cArrayList("@cashout_sta_id", sStatus));
                    bll.vUpdatetcashregout_approve(arr);
                    arr.Clear();
                    arr.Add(new cArrayList("@doc_typ", "casregout"));
                    arr.Add(new cArrayList("@token", sAppCode));
                  //  bll.vInsertSMSSentHist(arr);
                }
                if (strnname == "cashout")
                {
                    sCheck = bll.vLookUp("select cashout_Sta_id from tcashout_request where cashout_cd='"+sDocNo+"'");
                    if ((sCheck == "N"))
                    {
                        arr.Clear();
                        arr.Add(new cArrayList("@cashout_cd", sDocNo));
                        arr.Add(new cArrayList("@cashout_sta_id", sStatus));
                        bll.vUpdateCashoutRequestByStatus(arr);
                    }
                    else
                    {
                        lbstatus.Text = "Ooops there are already proceeded, can not re-process!";
                        return;
                    }
                }
                else if (strnname == "trnstock")
                {
                    arr.Add(new cArrayList("@trnstkNo", sDocNo));
                    arr.Add(new cArrayList("@salespointcd", ssalespointcd));
                    arr.Add(new cArrayList("@sta_id", sStatus));
                    bll.vUpdatetbltrnstock_approve(arr);
                    arr.Clear();
                    arr.Add(new cArrayList("@doc_typ", "trnstock"));
                    arr.Add(new cArrayList("@token", sAppCode));
                    //  bll.vInsertSMSSentHist(arr);
                }
                else if (strnname == "internaltransfer")
                {
                    arr.Add(new cArrayList("@trf_no", sDocNo));
                    arr.Add(new cArrayList("@salespointcd", ssalespointcd));
                    arr.Add(new cArrayList("@sta_id", sStatus));
                    bll.vUpdatetinternal_transfer_approve(arr);
                    arr.Clear();
                    arr.Add(new cArrayList("@doc_typ", "internaltransfer"));
                    arr.Add(new cArrayList("@token", sAppCode));
                    // bll.vInsertSMSSentHist(arr);
                }
                else if (strnname == "returnho")
                {
                    arr.Add(new cArrayList("@returho_no", sDocNo));
                    arr.Add(new cArrayList("@salespointcd", ssalespointcd));
                    arr.Add(new cArrayList("@retho_sta_id", sStatus));
                    bll.vUpdatetmst_returho_approve(arr);
                    arr.Clear();
                    arr.Add(new cArrayList("@doc_typ", "returnho"));
                    arr.Add(new cArrayList("@token", sAppCode));
                    //  bll.vInsertSMSSentHist(arr);
                }
                else if (strnname == "salesorder")
                {
                    arr.Add(new cArrayList("@so_cd", sDocNo));
                    arr.Add(new cArrayList("@salespointcd", ssalespointcd));
                    arr.Add(new cArrayList("@app_sta_id", sStatus));
                    bll.vApproveSalesOrder(arr);
                    arr.Clear();
                    arr.Add(new cArrayList("@doc_typ", "salesorder"));
                    arr.Add(new cArrayList("@token", sAppCode));
                    // bll.vInsertSMSSentHist(arr);
                }
                else if (strnname == "canvasorder")
                {
                    arr.Add(new cArrayList("@so_cd", sDocNo));
                    arr.Add(new cArrayList("@salespointcd", ssalespointcd));
                    arr.Add(new cArrayList("@app_sta_id", sStatus));
                    bll.vApproveCanvasOrder(arr);
                    arr.Clear();
                    arr.Add(new cArrayList("@doc_typ", "canvasorder"));
                    arr.Add(new cArrayList("@token", sAppCode));
                    // bll.vInsertSMSSentHist(arr);
                }
                else if (strnname == "cashier")
                {
                    arr.Add(new cArrayList("@chclosingno", sDocNo));
                    arr.Add(new cArrayList("@salespointcd", ssalespointcd));
                    arr.Add(new cArrayList("@acknowledge", sStatus));
                    bll.vUpdatCashregisterClosing(arr);
                    arr.Clear();
                    arr.Add(new cArrayList("@doc_typ", "cashier"));
                    arr.Add(new cArrayList("@token", sAppCode));
                    //  bll.vInsertSMSSentHist(arr);

                }
                else if (strnname == "retho")
                {
                    arr.Add(new cArrayList("@IDS", sAppCode));
                    arr.Add(new cArrayList("@reqretho_sta_id", sStatus));
                    bll.vUpdateReqReturHo(arr);
                }
                else if (strnname == "invconfirm")
                {

                    if (sStatus == "A")
                    {
                        int grdfree = 0;
                        grdfree = int.Parse(bll.vLookUp("select count(*) from twrk_dosalesfree where usr_id='" + sRefno + "'"));

                        arr.Clear();
                        arr.Add(new cArrayList("@refno", sRefno));
                        arr.Add(new cArrayList("@stockcard_typ", "SALESCANCEL"));
                        bll.vBatchStockCard(arr);

                        arr.Clear();
                        arr.Add(new cArrayList("@usr_id", sRefno));
                        arr.Add(new cArrayList("@inv_no", sRefno));
                        bll.vUpdateDOSalesDtlFromWRK3(arr);

                        arr.Clear();
                        arr.Add(new cArrayList("@inv_no", sRefno));
                        arr.Add(new cArrayList("@received_dt", System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                        arr.Add(new cArrayList("@received_by", '0'));
                        bll.vInsertDosalesInvoiceReceived(arr);

                        arr.Clear();
                        arr.Add(new cArrayList("@refno", sRefno));
                        arr.Add(new cArrayList("@stockcard_typ", "SALES"));
                        bll.vBatchStockCard(arr);

                        if (grdfree > 0)
                        {
                            arr.Clear();
                            arr.Add(new cArrayList("@refno", sRefno));
                            arr.Add(new cArrayList("@stockcard_typ", "SALESFREECANCEL"));
                            bll.vBatchStockCard(arr);
                            arr.Clear();
                            arr.Add(new cArrayList("@refno", sRefno));
                            arr.Add(new cArrayList("@stockcard_typ", "SALESFREE"));
                            bll.vBatchStockCard(arr);
                        }

                    }
                    else
                    {
                        arr.Clear();
                        arr.Add(new cArrayList("@usr_id", sRefno));
                        arr.Add(new cArrayList("@inv_no", sRefno));
                        bll.vUpdateDOSalesDtlFromWRK4(arr);
                    }
                }

                if ((sStatus == "A" || sStatus == "Y"))
                {
                    lbstatus.Text = "Transaction No. " + sDocNo + " HAS BEEN APPROVED ! \r\n , Confirmed";
                }
                else { lbstatus.Text = "Transaction No. " + sDocNo + " HAS BEEN REJECTED ! \r\n Now"; }
            }
            else
            { lbstatus.Text = "Ooops there are already proceeded, can not re-process!"; }
        }
    }
}