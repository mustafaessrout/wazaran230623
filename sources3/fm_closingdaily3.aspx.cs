using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class fm_closingdaily3 : System.Web.UI.Page
{
    cbll bll = new cbll();
    Utitlity ut = new Utitlity();
    List<cArrayList> arr = new List<cArrayList>();
    cdal cdl = new cdal();
    SqlDataReader rs = null;
    bool stclosing = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                arr.Clear();
                arr.Add(new cArrayList("@job_typ", "D"));
                lbdate.Text = Request.Cookies["waz_dt"].Value.ToString();

                arr.Clear();
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vBindingGridToSp(ref grdDepositConfirm, "sp_closingbankcashconfirm", arr);

                if (Request.Cookies["Grd_Empty"] != null)
                {
                    if (Request.Cookies["Grd_Empty"].Value.ToString() == "True")
                    {
                        btstart.Enabled = true;
                        btnValClosingDaily.Enabled = false;
                        Response.Cookies["Grd_Empty"].Value = "False";
                        //btstart_Click(sender, e);
                    }
                }
                if (Request.Cookies["ref"] != null)
                {
                    if (Request.Cookies["ref"].Value.ToString() == "True")
                    {
                        Response.Cookies["ref"].Value = "False";
                        btnValClosingDaily_Click(sender, e);
                    }
                }

            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_closingdaily3");
                Response.Redirect("fm_ErrorPage.aspx");
            }
        }

    }
    protected void btnValClosingDaily_Click(object sender, EventArgs e)
    {
        try
        {

            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.validationDailyClosing(arr);
            closingSp_Validation(sender, e);
            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref Grd, "sp_tmst_ClosingDaily_get_tbl",arr);

            if (Grd.Rows.Count.Equals(0))
            {
                btnCleared.Enabled = true;
                btnRefresh.Enabled = false;
                lballdone.Enabled = true;
                lballdone.Text = "All Done";
                modal_dialog.Style.Add("height", "auto");
                modal_body.Style.Add("height", "auto");
            }



            if (Grd.Rows.Count > 0)
            {
                if (Grd.Rows.Count < 6)
                {
                    modal_dialog.Style.Add("height", "auto");
                    modal_body.Style.Add("height", "auto");
                }
                for (int i = 0; i < Grd.Rows.Count; i++)
                {
                    Grd.Rows[i].Style.Add("height", "50px");

                }
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_closingdaily3");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btnCleared_Click(object sender, EventArgs e)
    {
        Response.Cookies["Grd_Empty"].Value = "True";
        Response.Redirect("fm_closingdaily3.aspx");
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Response.Cookies["ref"].Value = "True";
        Response.Redirect("fm_closingdaily3.aspx");

    }
    protected void grd_DataBound(object sender, EventArgs e)
    {
        try
        {

            for (int i = 0; i < Grd.Rows.Count; i++)
            {
                Label lblvalidVal = (Label)Grd.Rows[i].FindControl("validVal");

                if (lblvalidVal.Text != "Done")
                {
                    lblvalidVal.ForeColor = System.Drawing.Color.Red;
                    lblvalidVal.Text = lblvalidVal.Text.Replace(",", " , ");
                }
                else
                {
                    lblvalidVal.ForeColor = System.Drawing.Color.Green;
                }

            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_closingdaily3");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void closingSp_Validation(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < Grd.Rows.Count; i++)
            {
                Label lblvalidVal = (Label)Grd.Rows[i].FindControl("validVal");

                if (lblvalidVal.Text != "Done")
                {
                    lblvalidVal.ForeColor = System.Drawing.Color.Red;
                    lblvalidVal.Text = lblvalidVal.Text.Replace(",", " , ");
                }
                else
                {
                    lblvalidVal.ForeColor = System.Drawing.Color.Green;
                }

            }
            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            string sVal = "";

            string sMsg = string.Empty;
            int count = 1;
            bll.vBatchPreDaily(ref rs, arr);
            while (rs.Read())
            {
                if (count == 1)
                {
                    sMsg += rs["itemco_cd"].ToString() + ",";
                }
                else { sMsg += rs["itemco_cd"].ToString(); }
                count++;
            }

            rs.Close();


            //string sMsgstockminus = string.Empty;
            //arr.Clear();
            //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            //bll.vcheckstockminus(ref rs, arr);
            //while (rs.Read())
            //{
            //    sMsgstockminus += rs["whs_cd"].ToString() + '-' + rs["bin_cd"].ToString() + '-' + rs["item_cd"].ToString() + '-' + rs["stock_amt"].ToString() + " ,";
            //}
            //rs.Close();
            //if (sMsgstockminus != string.Empty)
            //{
            //    sVal = ("There are Stock Minus, Please Check Stock Report" + sMsgstockminus);

            //}
            //else
            //{
            //    sVal = "Done";
            //}
            //arr.Clear();
            //arr.Add(new cArrayList("@messageVal", sVal));
            //arr.Add(new cArrayList("@valID", "VD0034"));
            //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            //bll.vInsertClosingDailyVal(arr);



            string sMsgjaret1stday = string.Empty;
            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vclosingcheckjaret1stday(ref rs, arr);
            while (rs.Read())
            {
                sMsgjaret1stday += rs["whs_cd"].ToString() + '-' + rs["emp_nm"].ToString() + " ,";
            }
            rs.Close();
            if (sMsgjaret1stday != string.Empty)
            {
                sVal = ("Please Do Jaret Entry" + sMsgjaret1stday);
            }
            else
            {
                sVal = "Done";
            }
            arr.Clear();
            arr.Add(new cArrayList("@messageVal", sVal));
            arr.Add(new cArrayList("@valID", "VD0035"));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vInsertClosingDailyVal(arr);


            string sMsgschedulejaret = string.Empty;
            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vclosingschedulejaretmonthly(ref rs, arr);
            while (rs.Read())
            {
                sMsgschedulejaret += rs["whs_cd"].ToString() + '-' + rs["emp_nm"].ToString() + " ,";
            }
            rs.Close();
            if (sMsgschedulejaret != string.Empty)
            {
                sVal = ("Please Do Schedule Jaret Monthly Entry" + sMsgschedulejaret);
            }
            else
            {
                sVal = "Done";
            }
            arr.Clear();
            arr.Add(new cArrayList("@messageVal", sVal));
            arr.Add(new cArrayList("@valID", "VD0036"));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vInsertClosingDailyVal(arr);



            string sMsgjaret = string.Empty;
            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vclosingstockjaretLastMonth(ref rs, arr);
            while (rs.Read())
            {
                sMsgjaret += rs["whs_cd"].ToString() + '-' + rs["bin_cd"].ToString() + '-' + rs["item_cd"].ToString() + '-' + rs["diff"].ToString() + " ,";
            }
            rs.Close();
            if (sMsgjaret != string.Empty)
            {
                sVal = ("There are Difference Between Stock System and Stock Opname, Please Check Closing Stock Jaret Monthly Report" + sMsgjaret);
            }
            else
            {
                sVal = "Done";
            }
            arr.Clear();
            arr.Add(new cArrayList("@messageVal", sVal));
            arr.Add(new cArrayList("@valID", "VD0037"));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vInsertClosingDailyVal(arr);



            string sMsgjaretschedule = string.Empty;
            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vclosingschedulejaret(ref rs, arr);
            while (rs.Read())
            {
                sMsgjaretschedule += rs["whs_cd"].ToString() + '-' + rs["bin_cd"].ToString() + '-' + rs["item_cd"].ToString() + '-' + rs["diff"].ToString() + " ,";
            }
            rs.Close();
            if (sMsgjaretschedule != string.Empty)
            {
                sVal = ("There are Difference Between Stock System and Stock Opname, please check Closing Stock Jaret Schedule Report" + sMsgjaretschedule);
            }
            else
            {
                sVal = "Done";
            }
            arr.Clear();
            arr.Add(new cArrayList("@messageVal", sVal));
            arr.Add(new cArrayList("@valID", "VD0038"));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vInsertClosingDailyVal(arr);

            string sMsginternaltransfer = string.Empty;
            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vclosingInternalTransferLastMonth(ref rs, arr);
            while (rs.Read())
            {
                sMsginternaltransfer += rs["trf_no"].ToString() + " ,";
            }
            rs.Close();
            if (sMsginternaltransfer != string.Empty)
            {
                sVal = ("There are Internal Transfer not yet Printed  or not yet Approved" + sMsginternaltransfer);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "vDisableShow();", true);
            }
            else
            {
                sVal = "Done";
            }
            arr.Clear();
            arr.Add(new cArrayList("@messageVal", sVal));
            arr.Add(new cArrayList("@valID", "VD0039"));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vInsertClosingDailyVal(arr);


            SqlDataReader rsas = null;
            int contDestrApp = Int32.Parse(bll.vLookUp("select count(*) from tblTrnStock where invtype='12' and sta_id='A' and trn_trnstkdate='" + System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'"));
            bll.vGetDestApp(System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString(), Request.Cookies["sp"].Value.ToString(), ref rsas);
            string ssd = "";
            while (rsas.Read())
            {
                ssd += rsas["trnstkno"].ToString() + " ,";
            }
            if (contDestrApp > 0)
            {
                sVal = ("There are Destroy not yet completed, Please upload files after destroy to complete (" + ssd + ")");
            }
            else
            {
                sVal = "Done";
            }
            arr.Clear();
            arr.Add(new cArrayList("@messageVal", sVal));
            arr.Add(new cArrayList("@valID", "VD0040"));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vInsertClosingDailyVal(arr);

            string cntTrnStockApp = bll.vLookUp("select  COUNT(trnstkno) from tblTrnStock where invtype='12' and trn_trnstkdate='" + System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "' and sta_id='N'");
            System.Data.SqlClient.SqlDataReader rsa = null;
            string ddmss = "";
            bll.vGetTrnstkno(System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString(), Request.Cookies["sp"].Value.ToString(), ref rsa);
            while (rsa.Read())
            {
                ddmss += rsa["trnstkno"].ToString() + " ,";
            }
            if (Convert.ToDouble(cntTrnStockApp) > 0)
            {
                sVal = ("There are Destroy not yet approved :" + ddmss);
            }
            else
            {
                sVal = "Done";
            }
            arr.Clear();
            arr.Add(new cArrayList("@messageVal", sVal));
            arr.Add(new cArrayList("@valID", "VD0041"));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vInsertClosingDailyVal(arr);


            //string sMsgpettycash = string.Empty;
            //arr.Clear();
            //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            //bll.vGetpettycash_DailyClosingBalance(ref rs,arr);
            //while (rs.Read())
            //{
            //    sMsgpettycash += rs["cashout_cd"].ToString() + " ,";
            //}
            //rs.Close();
            //if (sMsgpettycash != "Done ,")
            //{
            //    sVal = ("There are Cashout for Employee Pettycash Balance need to be Paid " + sMsgpettycash);
            //}
            //else
            //{
            //    sVal = "Done";
            //}
            //arr.Clear();
            //arr.Add(new cArrayList("@messageVal", sVal));
            //arr.Add(new cArrayList("@valID", "VD0045"));
            //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            //bll.vInsertClosingDailyVal(arr);



            //string sMsgClaimpettycash = string.Empty;
            //arr.Clear();
            //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            //bll.vGet_tproposal_paid_pettycash_DailyClosingBalance(ref rs,arr);
            //while (rs.Read())
            //{
            //    sMsgClaimpettycash += rs["claimco_cd"].ToString() + " ,";
            //}
            //rs.Close();
            //if (sMsgClaimpettycash != "Done ,")
            //{
            //    sVal = ("There are Claim Cashout from Pettycash Source need to be Approved " + sMsgClaimpettycash);
            //}
            //else
            //{
            //    sVal = "Done";
            //}
            //arr.Clear();
            //arr.Add(new cArrayList("@messageVal", sVal));
            //arr.Add(new cArrayList("@valID", "VD0046"));
            //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            //bll.vInsertClosingDailyVal(arr);

            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            DataTable dtFullReturn = new DataTable();
            dtFullReturn = cdl.GetValueFromSP("sp_tdosales_invoice_getbyStatusApp", arr);
            if (dtFullReturn.Rows.Count != 0)
            {
                arr.Clear();
                string invLst = string.Empty;

                foreach (DataRow dr in dtFullReturn.Rows)
                {
                    invLst += dr["inv_no"] + " ";
                }

                arr.Add(new cArrayList("@messageVal", invLst));
                arr.Add(new cArrayList("@valID", "VD0051"));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vInsertClosingDailyVal(arr);
            }
            else
            {
                arr.Clear();
                arr.Add(new cArrayList("@messageVal", "Done"));
                arr.Add(new cArrayList("@valID", "VD0051"));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vInsertClosingDailyVal(arr);
            }

            // Additional to fast daily closing from btstart_closing
            string sCheckmaxCQnTF = bll.vLookUp("select dbo.fn_checkmaxCQnTF('" + Request.Cookies["sp"].Value.ToString() + "')");
            if (sCheckmaxCQnTF != "ok")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There bank transfer or Cheque maximum 15 days to process clearing','" + sCheckmaxCQnTF + "','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "vDisableShow();", true);
                return;
            }

            stclosing = true;

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_closingdaily3");
            Response.Redirect("fm_ErrorPage.aspx");
        }


    }

    protected void closingPraStart()
    {

        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@closing_dt", System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBatchClosingCashier(arr);

        arr.Clear();
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@datdate", System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        bll.vBatchTblStock(arr);

        //arr.Clear();
        //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        //arr.Add(new cArrayList("@dtdate", System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        //bll.vBatchTblStockCost(arr);

        arr.Clear();
        arr.Add(new cArrayList("@closing_dt", System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBatchSOA(arr);

        arr.Clear();
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBatchStockBalance(arr);
        bll.vBatchTurnOffDiscount(arr);

        arr.Clear();
        arr.Add(new cArrayList("@dtdate", System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBatchtacc_stock(arr);

        arr.Clear();
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBatchStockConfirm(arr);

    }

    protected void btstart_Click(object sender, EventArgs e)
    {
        try
        {
            btstart.Enabled = false;
            btstart.CssClass = "divhid";
            btstart.Attributes.Add("style", "display:none");

            string error = string.Empty;
            foreach (GridViewRow row in grdDepositConfirm.Rows)
            {

                Label lbTransaction = row.FindControl("lbTransaction") as Label;
                Label lbbranch = row.FindControl("lbbranch") as Label;
                TextBox txho = row.FindControl("txho") as TextBox;
                double ho = 0;
                if (lbbranch != null && txho != null)
                {
                    if (Convert.ToDouble(lbbranch.Text.Split(' ')[0]) > 0)
                    {
                        if (txho.Text == "")
                        {
                            error += lbTransaction.Text + " must be numeric. ";
                        }
                        else if (!double.TryParse(txho.Text, out ho))
                        {
                            error += lbTransaction.Text + " must be numeric. ";
                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('" + lbTransaction.Text + " must be numeric','Pls check "+ lbTransaction.Text + "','warning');", true);

                        }
                    }
                }

            }
            if (error.Length > 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('" + error + "','Pls check error ','warning');", true);
                return;
            }
            else
            {

                btstart.Enabled = false;

                System.Data.SqlClient.SqlDataReader rs = null;
                List<cArrayList> arr = new List<cArrayList>();
                lbdate.Text = Request.Cookies["waz_dt"].Value.ToString();

                string s = (DateTime.ParseExact(lbdate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToShortDateString());
                string sBatctToday = bll.vLookUp("select dbo.fn_checkbatchsamedate('" + Request.Cookies["sp"].Value.ToString() + "')");
                if (sBatctToday != "ok")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Closing can not done with more calendar date as today!','System date can not same with calendar date','warning');", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "vDisableShow();", true);
                    return;
                }

                //string scheckitempriority_dailyclosing = bll.vLookUp("select dbo.fn_checkitempriority_dailyclosing('"+ Request.Cookies["sp"].Value.ToString() + "')");
                //if (scheckitempriority_dailyclosing != "ok")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There are sales target item priority not entry','" + scheckitempriority_dailyclosing + "','warning');", true);
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "vDisableShow();", true);
                //    return;
                //}

                //string sCLAIMPending = bll.vLookUp("select dbo.fn_checkclaim('"+ Request.Cookies["sp"].Value.ToString() + "')").ToString();
                //if (sCLAIMPending != "ok")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There is pending scheme not yet create claim','" + sCLAIMPending + "','warning');", true);
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "vDisableShow();", true);
                //    return;
                //}

                //string sTOPending = bll.vLookUp("select dbo.fn_checktakeordermustdone('"+ Request.Cookies["sp"].Value.ToString() + "')").ToString();
                //if (sTOPending != "ok")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There is pending process take order on Loading or Need Approval','" + sTOPending + "','warning');", true);
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "vDisableShow();", true);
                //    return;
                //}

                //string sCanvasNotPrinted = bll.vLookUp("select dbo.fn_checkcanvasmustbedone('"+ Request.Cookies["sp"].Value.ToString() + "')");
                //if (sCanvasNotPrinted != "ok")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There is Canvas Order not yet printed !','" + sCanvasNotPrinted + "','warning');", true);
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "vDisableShow();", true);
                //    return;
                //}

                //string sCanvasPending = bll.vLookUp("select dbo.fn_checkcustpaymentcash('"+ Request.Cookies["sp"].Value.ToString() + "')").ToString();
                //if (sCanvasPending != "ok")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Customer cash is not paid by cash today','" + sCanvasPending + "','warning');", true);
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "vDisableShow();", true);
                //    return;
                //}

                //string sPaidCash = bll.vLookUp("select dbo.fn_checkpaidbycash('"+ Request.Cookies["sp"].Value.ToString() + "')").ToString();
                //if (sCanvasPending != "ok")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There are paid cash on transaction but not yet paid !','" + sPaidCash + "','warning');", true);
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "vDisableShow();", true);
                //    return;
                //}

                //string sPaymentNo = bll.vLookUp("select dbo.fn_checkpaidnotreceived('"+ Request.Cookies["sp"].Value.ToString() + "')");
                //if (sPaymentNo != "ok")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There are CASH IN not yet received !','" + sPaymentNo + "','warning');", true);
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "vDisableShow();", true);
                //    return;
                //}
                //string sTab = bll.vLookUp("select dbo.fn_checktablettoday('" + Request.Cookies["sp"].Value.ToString() + "')");
                //if (sTab != "ok")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Some tablet data has not imported as today, please do action ','" + sTab + "','warning');", true);
                //    return;
                //}
                chprebatch.Checked = true;
                chandroid.Checked = true;

                //string sCashReg = bll.vLookUp("select dbo.fn_checkcashregister('"+ Request.Cookies["sp"].Value.ToString() + "')");
                //if (sCashReg != "ok")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There are pending cashier not received/paid all !','" + sCashReg + "','warning');", true);
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "vDisableShow();", true);
                //    return;
                //}
                //string sClosingCashier = bll.vLookUp("select dbo.fn_checkclosingcashier('"+ Request.Cookies["sp"].Value.ToString() + "')");
                //if (sClosingCashier != "ok")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('" + sClosingCashier + "','Cashier','warning');", true);
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "vDisableShow();", true);
                //    return;
                //}

                // MOdified by IA as 5 April 2016
                //string sAck = bll.vLookUp("select dbo.fn_checkcashieracknowledge('"+ Request.Cookies["sp"].Value.ToString() + "')");
                //if (sAck != "ok")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('" + sAck + "','Branch SPV not yet acknowledge your cashier','warning');", true);
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "vDisableShow();", true);
                //    return;
                //}

                chcashier.Checked = true;

                //string sCNDN = bll.vLookUp("select dbo.fn_checkcndn3days('"+ Request.Cookies["sp"].Value.ToString() + "')");
                //if (sCNDN != "ok")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There is CN / DN has not yet approved more than 3 days','" + sCNDN + "','warning');", true);
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "vDisableShow();", true);
                //    return;
                //}
                //string sSalesTargetHOSP = bll.vLookUp("select dbo.fn_checksalestargethosp('"+ Request.Cookies["sp"].Value.ToString() + "')");
                //if (sSalesTargetHOSP != "ok")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('" + sSalesTargetHOSP + "','Sales Target','warning');", true);
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "vDisableShow();", true);
                //    return;
                //}

                //string sCheckCustomerReceived = bll.vLookUp("select dbo.fn_checkcustomerreceived('"+ Request.Cookies["sp"].Value.ToString() + "')");
                //if (sCheckCustomerReceived != "ok")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There invoices has not yet received back from customer after 7 days printed','" + sCheckCustomerReceived + "','warning');", true);
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "vDisableShow();", true);
                //    return;
                //}
                string sMsg = string.Empty;
                int count = 1;
                arr.Clear();
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //bll.vBatchPreDaily(ref rs, arr);
                //while (rs.Read())
                //{
                //    if (count == 1)
                //    {
                //        sMsg += rs["itemco_cd"].ToString() + ",";
                //    }
                //    else { sMsg += rs["itemco_cd"].ToString(); }
                //    count++;
                //}

                //rs.Close();
                //string sCheckJaret = bll.vLookUp("select dbo.fn_checkjaret('"+ Request.Cookies["sp"].Value.ToString() + "')");
                //if (sCheckJaret != "ok")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Stock Opname in this period has not yet done !','Do stock opname before closing','warning');", true);
                //    return;
                //}

                //by daryanto 05-12-2016
                //string sMsgstockminus = string.Empty;
                //arr.Clear();
                //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //bll.vcheckstockminus(ref rs, arr);
                //while (rs.Read())
                //{
                //    sMsgstockminus += rs["whs_cd"].ToString() + '-' + rs["bin_cd"].ToString() + '-' + rs["item_cd"].ToString() + '-' + rs["stock_amt"].ToString() + " ,";
                //}
                //rs.Close();
                //if (sMsgstockminus != string.Empty)
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There are stock minus, please check stock Report','" + sMsgstockminus + "','warning');", true);
                //    return;

                //}

                //11 May 2016 by IA
                //string sstockconfirm = bll.vLookUp("select dbo.fn_checkstockconfirm('"+ Request.Cookies["sp"].Value.ToString() + "')");

                //if (sstockconfirm != "ok")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('" + sstockconfirm + "','Do stock confirm/postpone before closing','warning');", true);
                //    return;
                //}

                //if (bll.vLookUp("select dbo.fn_checkconfirmstock5days('"+ Request.Cookies["sp"].Value.ToString() + "')") != "ok")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Stock 5 days before not yet confirmed!','Please confirm for stock 5 days before !','warning');", true);
                //    return;
                //}

                //if (bll.vLookUp("select dbo.fn_checkstockvan('" + Request.Cookies["sp"].Value.ToString() + "')") != "ok")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Stock in VAN still available!!','Please Do Internal Transfer Van To Depo !','warning');", true);
                //    return;
                //}


                chstock.Checked = true;

                //string sReturPrice = bll.vLookUp("select dbo.fn_checkreturncustprice('"+ Request.Cookies["sp"].Value.ToString() + "')");
                //if (sReturPrice != "ok")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('" + sReturPrice + "','Retur has no price','warning');", true);
                //    return;
                //}
                //string sInvCancel = bll.vLookUp("select dbo.fn_checkinvcancelbywhs('"+ Request.Cookies["sp"].Value.ToString() + "')");
                //if (sInvCancel != "ok")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There are some invoice received by warehouse cancelled','Pls cancel invoice : " + sInvCancel + "','warning');", true);
                //    return;
                //}
                
                //---------------
                //check jaret 1st Day
                //string sMsgjaret1stday = string.Empty;
                //arr.Clear();
                //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //bll.vclosingcheckjaret1stday(ref rs, arr);
                //while (rs.Read())
                //{
                //    sMsgjaret1stday += rs["whs_cd"].ToString() + '-' + rs["emp_nm"].ToString() + " ,";
                //}
                //rs.Close();
                //if (sMsgjaret1stday != string.Empty)
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please entry Jaret ','" + sMsgjaret1stday + "','warning');", true);
                //    return;

                //}
                //------------
                //check schedule jaret
                //string sMsgschedulejaret = string.Empty;
                //arr.Clear();
                //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //bll.vclosingschedulejaretmonthly(ref rs,arr);
                //while (rs.Read())
                //{
                //    sMsgschedulejaret += rs["whs_cd"].ToString() + '-' + rs["emp_nm"].ToString() + " ,";
                //}
                //rs.Close();
                //if (sMsgschedulejaret != string.Empty)
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please entry schedule Jaret ','" + sMsgschedulejaret + "','warning');", true);
                //    return;

                //}
                //------------
                //string sMsgjaret = string.Empty;
                //arr.Clear();
                //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //bll.vclosingstockjaretLastMonth(ref rs,arr);
                //while (rs.Read())
                //{
                //    sMsgjaret += rs["whs_cd"].ToString() + '-' + rs["bin_cd"].ToString() + '-' + rs["item_cd"].ToString() + '-' + rs["diff"].ToString() + " ,";
                //}
                //rs.Close();
                //if (sMsgjaret != string.Empty)
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There are different stock system and stock opname, please check Closing Stock Jaret Monthly Report','" + sMsgjaret + "','warning');", true);
                //    return;

                //}
                //string sMsgdestroy = string.Empty; by yanto 03-04-2018
                //arr.Clear();
                //bll.vclosingDestroyLastMonth(ref rs);
                //while (rs.Read())
                //{
                //    sMsgdestroy += rs["trnstkno"].ToString() + " ,";
                //}
                //rs.Close();
                //if (sMsgdestroy != string.Empty)
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There are transaction destroy not approve yet','" + sMsgdestroy + "','warning');", true);
                //    return;

                //}
                //by daryanto 18-10-2016
                //string sMsgjaretschedule = string.Empty;
                //arr.Clear();
                //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //bll.vclosingschedulejaret(ref rs,arr);
                //while (rs.Read())
                //{
                //    sMsgjaretschedule += rs["whs_cd"].ToString() + '-' + rs["bin_cd"].ToString() + '-' + rs["item_cd"].ToString() + '-' + rs["diff"].ToString() + " ,";
                //}
                //rs.Close();
                //if (sMsgjaretschedule != string.Empty)
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There are different stock system and stock opname, please check Closing Stock Jaret schedule Report','" + sMsgjaretschedule + "','warning');", true);
                //    return;

                //}

                //string sCheckpendinglastmonth = bll.vLookUp("select dbo.fn_checkpendinglastmonth('"+ Request.Cookies["sp"].Value.ToString() + "')");
                //if (sCheckpendinglastmonth != "ok")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('There pending transaction','" + sCheckpendinglastmonth + "','warning');", true);
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "vDisableShow();", true);
                //    return;
                //}
                //string sCheckstockprev = bll.vLookUp("select dbo.fn_checkstockprev(null,'"+ Request.Cookies["sp"].Value.ToString() + "')");
                //if (sCheckstockprev != "ok")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('There are differences with previous stock ','" + sCheckstockprev + "','warning');", true);
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "vDisableShow();", true);
                //    return;
                //}
                //---------------


                chstock.Checked = true;
                chstockin.Checked = true;
                chbadstock.Checked = true;

                //string sdriverreceipt = bll.vLookUp("select dbo.fn_checkinvoiceage('"+ Request.Cookies["sp"].Value.ToString() + "')");
                //if (sdriverreceipt != "ok")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There some invoice within 3 days not yet taken by driver','" + sdriverreceipt + "','warning');", true);
                //    return;
                //}

                //string sKA = bll.vLookUp("select dbo.fn_checkinvoicekeyaccount7day('"+ Request.Cookies["sp"].Value.ToString() + "')");
                //if (sKA != "ok")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There some invoice more " + bll.sGetControlParameter("maxinvkeyaccount") + " days for Key Account not yet receipt','" + sKA + "','warning');", true);
                //    return;
                //}

                //Check return must processed today
                //string sReturnProcessed = bll.vLookUp("select dbo.fn_checkreturneedprocess('"+ Request.Cookies["sp"].Value.ToString() + "')");
                //if (sReturnProcessed != "ok")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There return by today must be process to approval and apply payment!','" + sReturnProcessed + "','warning');", true);
                //    return;
                //}
                //string sCheckBadStock = bll.vLookUp("select dbo.fn_checkbadstock()");  by yanto 03-04-2018
                //if (sCheckBadStock != "ok")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There is bad stock must destroyed before end of this month!','" + sCheckBadStock + "','warning');", true);
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "vDisableShow();", true);
                //    return;
                //}
                //string scheckperiod = bll.vLookUp("select dbo.fn_checkperiod('"+ Request.Cookies["sp"].Value.ToString() + "')");
                //if (scheckperiod != "ok")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please closing monthly !','" + scheckperiod + "','warning');", true);
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "vDisableShow();", true);
                //    return;
                //}
                //string scheckyear = bll.vLookUp("select dbo.fn_checkyear('"+ Request.Cookies["sp"].Value.ToString() + "')");
                //if (scheckyear != "ok")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please closing Yearly !','" + scheckyear + "','warning');", true);
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "vDisableShow();", true);
                //    return;
                //}

                //string sMsginternaltransfer = string.Empty;
                //arr.Clear();
                //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //bll.vclosingInternalTransferLastMonth(ref rs,arr);
                //while (rs.Read())
                //{
                //    sMsginternaltransfer += rs["trf_no"].ToString() + " ,";
                //}
                //rs.Close();
                //if (sMsginternaltransfer != string.Empty)
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There are Internal Transfer not print yet or not approve yet','" + sMsginternaltransfer + "','warning');", true);
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "vDisableShow();", true);
                //    return;

                //}

                //string sPP = bll.vLookUp("select dbo.fn_batchcheckexistpaymentpromised('"+ Request.Cookies["sp"].Value.ToString() + "')");
                //if (sPP != "ok")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There are payment promised has not yet paid today!','" + sPP + "','warning');", true);
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "vDisableShow();", true);
                //    return;
                //}

                //string sCDC = bll.vLookUp("select dbo.fn_batchcheckexistclaimconfirm('"+ Request.Cookies["sp"].Value.ToString() + "')");
                //if (sCDC != "ok")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There are Invoice Received, not yet confirm to claim today!','Access into Accounting -> Claim Confirmation','warning');", true);
                //    return;
                //}

                // 17 Agustus : Check Claim activated
                //string sClaim = bll.vLookUp("select dbo.fn_checklaimpending('"+ Request.Cookies["sp"].Value.ToString() + "')");
                //if (sClaim != "ok")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There are claim not yet process and already due!','Prop No:" + sClaim + "','warning');", true);
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "vDisableShow();", true);
                //    return;
                //}

                // 17 Agustus : Check SOA DIFFERENT
                //string Ssoa = bll.vLookUp("select dbo.fn_checksoadiff('"+ Request.Cookies["sp"].Value.ToString() + "')");
                //if (Ssoa != "ok")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There are different SOA With balance Cust No:','" + Ssoa + "','warning');", true);
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "vDisableShow();", true);
                //    return;
                //}

                charcond.Checked = true;
                chSOA.Checked = true;

                //string sCheckReturHo = bll.vLookUp("select dbo.fn_checkreturhopostponeall('"+ Request.Cookies["sp"].Value.ToString() + "')").ToString();
                //if (sCheckReturHo != "ok")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There are retur to HO is not yet confirmed, pls contact Wazaran Admin','" + sCheckReturHo + "','warning');", true);
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "vDisableShow();", true);
                //    return;
                //}

                // Check 1-April-2018 : By IAG
                //string sCashDeposit = bll.vLookUp("select dbo.fn_checkpaymentnotconfirmed('"+ Request.Cookies["sp"].Value.ToString() + "')");
                //if (sCashDeposit != "ok")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There are CASH DEPOSIT is not yet confirmed, pls contact Wazaran Admin/PIC Confirmation','" + sCashDeposit + "','warning');", true);
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "vDisableShow();", true);
                //    return;
                //}

                //if (bll.vLookUp("select datepart(dw,dbo.fn_getsystemdate('"+ Request.Cookies["sp"].Value.ToString() + "'))") == "6")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report3.aspx?src=notbebs');", true);
                //}

                //by yanto 01-05-2018
                //string sloadingNotPrinted = bll.vLookUp("select dbo.fn_checkloadingmustbedone('"+ Request.Cookies["sp"].Value.ToString() + "')");
                //if (sloadingNotPrinted != "ok")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please Print Loading !','" + sloadingNotPrinted + "','warning');", true);
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "vDisableShow();", true);
                //    return;
                //}
                //if (bll.vLookUp("select isnull((select count(*) from tmst_salesorder inner join tsalesorder_info on tmst_salesorder.so_cd=tsalesorder_info.so_cd and tmst_salesorder.salespointcd=tsalesorder_info.salespointcd where so_sta_id = 'N' AND loading_dt = dateadd(day, 1,[dbo].[fn_getsystemdate]('"+ Request.Cookies["sp"].Value.ToString() + "')) and tmst_salesorder.salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'),0)") != "0")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report3.aspx?src=loadingnotification');", true);
                //}
                //-----------------------

                //By CINO 05-05-2018 (Check Postpone Invoice)
                //string sCheckPostponeInvoice = bll.vLookUp("select dbo.fn_checkinvoicepostpone('"+ Request.Cookies["sp"].Value.ToString() + "')");
                //if (sCheckPostponeInvoice != "ok")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There invoices has not yet received back from customer after postponed.','" + sCheckPostponeInvoice + "','warning');", true);
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "vDisableShow();", true);
                //    return;
                //}

                //string sChkStkBalance = bll.vLookUp("select sum(stkBalance*unitprice) from tblstock a inner join tcustomertype_price b on a.item_cd=b.item_cd where b.cust_typ='BRN' and MonthCD=(select parm_valu from tcontrol_parameter where parm_nm='period') and bin_cd='BS'");
                //string maxStkBlnce = bll.sGetControlParameter("thresholdvaluebadstock");
                //if (Convert.ToDouble(sChkStkBalance) >= Convert.ToDouble(maxStkBlnce))
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('total stock more than "+ maxStkBlnce + ".','','warning');", true);
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "vDisableShow();", true);
                //    return;
                //}

                //string cntTrnStockApp = bll.vLookUp("select  COUNT(trnstkno) from tblTrnStock where trn_trnstkdate='" + System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString() + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "' and sta_id='N'");
                //System.Data.SqlClient.SqlDataReader rsa = null;
                //string ddms = "";
                //bll.vGetTrnstkno(System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString(), Request.Cookies["sp"].Value.ToString(), ref rsa);
                //while (rsa.Read())
                //{
                //    ddms += rsa["trnstkno"].ToString() + " ,";
                //}
                //if (Convert.ToDouble(cntTrnStockApp) > 0)
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There Destroy not approved','" + ddms + "','warning');", true);
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "vDisableShow();", true);
                //    return;
                //}
                //======================= cant closing if destroy just appv not complete=================

                //System.Data.SqlClient.SqlDataReader rsas = null;
                //int contDestrApp = Int32.Parse(bll.vLookUp("select count(*) from tblTrnStock where invtype='12' and sta_id='A' and trn_trnstkdate='" + System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString() + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "' "));
                //bll.vGetDestApp(System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString(), Request.Cookies["sp"].Value.ToString(), ref rsas);
                //string ssd = "";
                //while (rsas.Read())
                //{
                //    ssd += rsas["trnstkno"].ToString() + " ,";
                //}
                //if (contDestrApp > 0)
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There not completed Destroy ','Please upload files after destroy to complete (" + ssd + ")','warning');", true);
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "vDisableShow();", true);
                //    return;
                //}


                //-----------------------

                chprebatch.Checked = true;
                chprebatch.CssClass = "ro";
                //bll.vBatchBackupDB();
                arr.Clear();
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@dt", System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                bll.vBatchCustomerPriceBooking(arr);
                //chCustomerPrice.Checked = true;
                //chCustomerPrice.CssClass = "ro";
                //arr.Clear();
                //arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                //arr.Add(new cArrayList("@closing_dt", System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //bll.vBatchClosingCashier(arr);
                chcashier.Checked = true;
                chcashier.CssClass = "ro";
                //arr.Clear();
                //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //arr.Add(new cArrayList("@datdate", System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                //bll.vBatchTblStock(arr);
                chstock.Checked = true;
                chstock.CssClass = "ro";
                arr.Clear();

                chrtu.Checked = true;
                chrtu.CssClass = "ro";
                // System.Threading.Thread.Sleep(1000);
                // System.Threading.Thread.Sleep(1000);
                chkdate.Checked = true;
                chkdate.CssClass = "ro";
                //SOA Process
                //--------------------------------------------------------------------------------------------------------------
                //arr.Clear();
                //arr.Add(new cArrayList("@closing_dt", System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //bll.vBatchSOA(arr);
                chSOA.Checked = true;
                chSOA.CssClass = "ro";
                //-----------------------------------------------------------------------------------------------------------
                //arr.Clear();
                //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //bll.vBatchStockBalance(arr);
                //    bll.vBatchStockVanBalance();
                //bll.vBatchCancelTO(arr);
                //chcancelTO.Checked = true;
                //chcancelTO.CssClass = "ro";
                //------------------------------------------------------------------------------------------------------------
                //bll.vBatchTurnOffDiscount(arr);
                chturnoffdiscount.Checked = true;
                chturnoffdiscount.CssClass = "ro";
                //-------------------------------------
                //bll.vBatchCleanWrk(arr);
                chcleanwrk.Checked = true;
                chcleanwrk.CssClass = "ro";
                //------------------Batch Send email pending invoice 5 days -------------------------
                //bll.vBatchNotifiedInvoice5Days();
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report3.aspx?src=inv5days');", true);
                //chnotifiedinv.Checked = true;
                //chnotifiedinv.CssClass = "ro";
                //-----------------Batch notified bad stock -----------------------------------------
                //bll.vBatchNotifyBadStock();
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report3.aspx?src=bs');", true);
                //chnotifiedbadstock.Checked = true;
                //chnotifiedbadstock.CssClass = "ro";
                // ----------------- Batch Clean SMS Outbox -------------------------------------------
                //bll.vBatchCleanSmsOutbox(arr);
                //cbCleanSmsOutbo.CssClass = "ro";
                //cbCleanSmsOutbo.Checked = true;
                // Batch Terminated price based on end date
                closingPraStart();
                //----------------------------Batch Collecting Free Item ----------------------------
                //bll.vBatchCollectingFreeItem(arr);
                //chcollfreeitem.Checked = true;
                arr.Clear();
                arr.Add(new cArrayList("@closing_typ", "D"));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@system_dt", null));
                bll.vInsertTclosinglog(arr);
                arr.Clear();
                //bll.vInsertTFreebyItem();			
                //arr.Clear();
                //	PDF sending for inv due more than 120
                //if (bll.vLookUp("select dbo.fn_checkhitnext120('"+ Request.Cookies["sp"].Value.ToString() + "')") == "ok")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report3.aspx?src=inv120days');", true);
                //    bll.vExecuteSQL("update tcontrol_parameter set parm_valu=dateadd(d,14,dbo.fn_getcontrolparameter('nextinv120')) where parm_nm='nextinv120'");
                //}

                //ut.BranchTargetPriority();
                //ut.BranchTargetPriorityVan();

                bll.vBatchBackupDBAfter(); 
                //Reset timing for last processes
                //arr.Clear();
                //arr.Add(new cArrayList("@parm_nm", "time_deadline_closing"));
                //arr.Add(new cArrayList("@parm_valu", "12:00"));
                //bll.vUpdateControlParameter(arr);
                //--------------------------------- Send Email automatically for HO pending payment
                //  ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report3.aspx?src=hopayment');", true);
                //arr.Clear();
                //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //bll.vBatchNotifyHOPayment(arr);
                //bll.vnotify_Password_expire(); // send email when wazaran user password will expire by yanto 07-07-2018
                //By yanto 26-08-2019 (Check maximum cheque and transfer bank 15 days)
                //string sCheckmaxCQnTF = bll.vLookUp("select dbo.fn_checkmaxCQnTF('"+ Request.Cookies["sp"].Value.ToString() + "')");
                //if (sCheckmaxCQnTF != "ok")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There bank transfer or Cheque maximum 15 days to process clearing','" + sCheckmaxCQnTF + "','warning');", true);
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "vDisableShow();", true);
                //    return;
                //}
                //arr.Clear();
                //arr.Add(new cArrayList("@dtdate", System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //bll.vBatchtacc_stock(arr);


                //-------------------------------------------------------
                //send Customer Cash Sales Over Due Rpt By Mamoun 
                //SqlDataReader rrs = null;
                //int counter = -1;
                //arr.Clear();
                //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //bll.vGetCustCashOverDueCount(ref rrs, arr);

                //while (rrs.Read())
                //{
                //    counter = Convert.ToInt32(rrs["count"].ToString());
                //} rrs.Close();

                //if (counter > 0)
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report3.aspx?src=customerCashSalesOverDue');", true);
                //}

                //-------------------------------------------------------
                arr.Clear();
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vBatchWazaranDate(arr);
                //--------------------------- Batch Android 
                //Batch Startup status
                bll.vBatchStartup(arr);
                //bll.vBatchGenerateDailyRPS();
                chandroid.Checked = true;
                chandroid.CssClass = "ro";
                //chbackupafter.Checked = true;
                //chbackupafter.CssClass = "ro";
                //--------------------------- Claim Daily Confirmation
                //chrequest.Checked = true;
                //chrequest.CssClass = "ro";
                //arr.Clear();
                //--------------------------- Send Email for Claim Pending
                //bll.vBatchNotifyClaimPending();
                // 27-MAR-2018 : By IAG : Add email notification for Payment COnfirmed by HO (Omar Madura)
                //bll.vBatchSendPaymentNotificationConfirmed(arr);
                //--------------------------- After Postpone Update To Must be received
                bll.vBatchUpdatePostponeInv(arr);
                // 05-05-2015 : By CINO : Add Update Status Postpone After Closing 
                //--------------------------- Update Customer Transfer if end date same with Wazaran Date.
                bll.vBatchSalesmanTransferReturn(arr);
                //SET ALL CONTROL PARAMETER TO DEFAULT
                string _sql = "update tcontrol_parameter set parm_valu='N' where parm_nm in ('bypassed_deposit','bypassed_paymentcash','bypassed_invoicereceived')";
                bll.vExecuteSQL(_sql);
                string errorCheckAgain = string.Empty;
                foreach (GridViewRow row in grdDepositConfirm.Rows)
                {
                    List<cArrayList> arrDepositConfirm = new List<cArrayList>();
                    Label lbTransaction = row.FindControl("lbTransaction") as Label;
                    Label lbbranch = row.FindControl("lbbranch") as Label;
                    Label lbcode = row.FindControl("lbcode") as Label;
                    TextBox txho = row.FindControl("txho") as TextBox;
                    if (txho.Text == "")
                    {
                        error += lbTransaction.Text + " must be numeric. ";
                    }
                    else if (lbbranch != null && txho != null)
                    {
                        if (Convert.ToDouble(lbbranch.Text.Split(' ')[0]) > 0 && Convert.ToDouble(txho.Text) > 0)
                        {
                            arrDepositConfirm.Clear();
                            arrDepositConfirm.Add(new cArrayList("@code_cd", Convert.ToString(lbcode.Text)));
                            arrDepositConfirm.Add(new cArrayList("@wazaranQty", Convert.ToDouble(lbbranch.Text.Split(' ')[0])));
                            arrDepositConfirm.Add(new cArrayList("@hoQty", Convert.ToDouble(txho.Text)));
                            arrDepositConfirm.Add(new cArrayList("@createdBy", Request.Cookies["usr_id"].Value.ToString()));
                            arrDepositConfirm.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                            bll.vBranchHOConfirmationInst(arrDepositConfirm);
                        }
                    }

                }

            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_closingdaily3");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        Response.Cookies["waz_dt"].Value = null;
        Response.Cookies["waz_dt"].Value = bll.sGetControlParameterSalespoint("WAZARAN_DT", Request.Cookies["sp"].Value.ToString());
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "vDisableShow();", true);
        //  showmessagex.Visible = false;
        string sDate = bll.sGetControlParameterSalespoint("WAZARAN_DT", Request.Cookies["sp"].Value.ToString());
        // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al2", "location.reload();", true);
        //if (bll.vLookUp("select 1 from tfield_value where fld_nm ='weeklycashier' and fld_valu=(select day(dbo.fn_getsystemdate()))") == "1" || bll.vLookUp("SELECT convert(date,DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,(select dbo.fn_getsystemdate()))+1,0)))") == bll.vLookUp("select dbo.fn_getsystemdate()"))
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report3.aspx?src=weeklycashier');", true);
        //    chweeklycashier.Checked = true;
        //    chweeklycashier.CssClass = "ro";
        //}

        cbll2 bll2 = new cbll2();
        string _message = "#Closing daily has been completed !, opening date now : " + sDate;
        arr.Clear();
        arr.Add(new cArrayList("@doc_typ", "batchcompleted"));
        arr.Add(new cArrayList("@level_no", 1));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        //List<tapprovalpattern> _tapprovalpattern = bll2.lApprovalPattern(arr);
        //foreach(tapprovalpattern _t in _tapprovalpattern)
        //{
        //    bll.vSendWhatsapp(_t.whatsapp_no, _message);
        //}
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();sweetAlert('Daily Closed Successfully','New Open Date " + sDate + "','success')", true);


        // System.Threading.Thread.Sleep(5000);
        //  Response.Redirect("fm_login2.aspx?m=1");
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "window.close();", true);
    }

}