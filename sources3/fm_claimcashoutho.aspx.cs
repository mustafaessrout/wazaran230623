using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_claimcashoutho : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        {
            dtcreate.CssClass = "form-control input-sm ro";
            dtcreate.Enabled = false;
            dtdelivery.CssClass = "form-control input-sm ro";
            dtdelivery.Enabled = false;
            dtend.CssClass = "form-control input-sm ro";
            dtend.Enabled = false;
            dtstart.CssClass = "form-control input-sm ro";
            dtstart.Enabled = false;
            txref.CssClass = "form-control input-sm ro";
            txref.Enabled = false;
            dtcreate.CssClass = "form-control input-sm ro";
            dtcreate.Enabled = false;
            dtdelivery.CssClass = "form-control input-sm ro";
            dtdelivery.Enabled = false;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vDelWrkProposalPaidHo(arr);
            btsave.CssClass = "divhid";
            btprint.CssClass = "divhid";
            vContract.Attributes.Add("style", "display:none");
            bll.vBindingComboToSp(ref ddYear, "sp_tmst_period_getbyyear", "yearvalue", "yearvalue");
            ddMonth.SelectedValue = DateTime.Now.ToString("mm");
            uploadDoc.Attributes.Add("style", "display:none");
            btapprove.Visible = false;
            btreject.Visible = false;
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        System.Data.SqlClient.SqlDataReader rs = null;
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        List<string> lprop = new List<string>();
        string sProp = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        // arr.Add(new cArrayList("@salesman_cd", contextKey));
        // arr.Add(new cArrayList("@cust_cd", prefixText));
        arr.Add(new cArrayList("@prop_no", prefixText));
        bll.vGetProposalPaymentByDate(arr, ref rs);
        while (rs.Read())
        {
            sProp = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["prop_no"].ToString() + "-" + rs["bgremark"].ToString(), rs["prop_no"].ToString());
            lprop.Add(sProp);
        }
        rs.Close();
        return (lprop.ToArray());
    }
    protected void txprop_TextChanged(object sender, EventArgs e)
    {

    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        System.Data.SqlClient.SqlDataReader rs = null;
        
        if (hdclaimno.Value.ToString() != "")
        {
            string sPropNo = bll.vLookUp("select prop_no from tproposal_paid_ho where claimco_cd='" + hdclaimno.Value.ToString() + "'");
            string sContract = bll.vLookUp("select contract_no from tproposal_paid_ho where claimco_cd = '"+hdclaimno.Value.ToString()+"'");
            string sMonth = bll.vLookUp("select tx_month from tproposal_paid_ho where claimco_cd = '" + hdclaimno.Value.ToString() + "'");
            string sYear = bll.vLookUp("select tx_year from tproposal_paid_ho where claimco_cd = '" + hdclaimno.Value.ToString() + "'");
            string sCDV = bll.vLookUp("select cdv_no from tproposal_paid_ho where claimco_cd = '" + hdclaimno.Value.ToString() + "'");
            txprop.Text = sPropNo;
            hdprop.Value = sPropNo;
            ddMonth.SelectedValue = sMonth;
            ddYear.SelectedValue = sYear;
            txcdv.Text = sCDV;
            string sType = bll.vLookUp("select promogroup from tmst_proposal where prop_no='" + hdprop.Value.ToString() + "'");
            if (sType == "DR" || sType == "TB" || sType == "SB" || sType == "DS")
            {
                vContract.Attributes.Remove("style");
                txcontract.Text = sContract;
                if (bll.vLookUp("select rdcust from tmst_proposal where prop_no='" + txprop.Text.ToString() + "'") == "C")
                {
                    cbreceive.SelectedValue = bll.vLookUp("select top 1 cust_cd from tcontract_customer where contract_no='" + txcontract.Text + "'");
                }
            }
            else
            {
                vContract.Attributes.Add("style", "display:none");
            }
            string sSel = bll.vLookUp("select rdinternal from tproposal_paid_ho where claimco_cd='" + hdclaimno.Value.ToString() + "'");
            if (sSel != "")
            {
                rdrcpt.SelectedValue = sSel;
            }
            txname.Text = bll.vLookUp("select (case when rdinternal='I' then (receivedby+'-'+emp_nm) else name end)as receive from tproposal_paid_ho left join tmst_employee on tproposal_paid_ho.receivedby = tmst_employee.emp_cd where claimco_cd='" + hdclaimno.Value.ToString() + "'");
            lbclaimno.Text = hdclaimno.Value.ToString();
            txphone.Text = bll.vLookUp("select phone_no from tproposal_paid_ho where claimco_cd='" + hdclaimno.Value.ToString() + "'");
            txamt.Text = bll.vLookUp("select amt from tproposal_paid_ho where claimco_cd='" + hdclaimno.Value.ToString() + "'");
            dtpaid.Text = Convert.ToDateTime(bll.vLookUp("select paid_dt from tproposal_paid_ho where claimco_cd='" + hdclaimno.Value.ToString() + "'")).ToString("d/M/yyyy");
            //cbreceive.Enabled = false; 
        }
        else
        {
            txprop.Text = hdprop.Value;
            string sType = bll.vLookUp("select promogroup from tmst_proposal where prop_no='" + hdprop.Value.ToString() + "'");
            if (sType == "DR" || sType == "TB" || sType == "SB" || sType == "DS")
            {
                vContract.Attributes.Remove("style");
            }
            else
            {
                vContract.Attributes.Add("style","display:none");
            }
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
        bll.vGetMstProposal(arr, ref rs);
        while (rs.Read())
        {
            dtcreate.Text = Convert.ToDateTime(rs["prop_dt"]).ToString("d/M/yyyy");
            dtstart.Text = Convert.ToDateTime(rs["start_dt"]).ToString("d/M/yyyy");
            dtend.Text = Convert.ToDateTime(rs["end_dt"]).ToString("d/M/yyyy");
            dtdelivery.Text = Convert.ToDateTime(rs["delivery_dt"]).ToString("d/M/yyyy");
            txref.Text = rs["ref_no"].ToString();
            txpaid.Text = rs["rdpaymentname"].ToString();
            txbudget.Text = rs["budget_limit"].ToString();
            lbremark.Text = rs["bgremark"].ToString();            
            txpromogroup.Text = bll.vLookUp("select promo_nm from tmst_promotion where promo_cd='" + rs["promogroup"].ToString() + "'");
            txpromotype.Text = bll.vLookUp("select promotyp_nm from tpromotion_dtl where promo_cd='" + rs["promogroup"].ToString() + "' and promo_typ='" + rs["promotype"].ToString() + "'");            
            btsave.CssClass = "btn-warning btn btn-save";
            btsave.Enabled = true;
            btprint.CssClass = "btn-info btn btn-print";
            btprint.Enabled = true;

            string sType = rs["rdcust"].ToString().ToUpper();
            if (sType == "C")
            {
                cbreceive.Visible = true;
                cbreceivePanel.Visible = true;
                arr.Clear();
                arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
                arr.Add(new cArrayList("@type", "cust"));
                bll.vBindingComboToSp(ref cbreceive, "sp_getproposal_bytype", "cust_cd", "cust_nm", arr);
                lbtype.Text = "By Cust";
                //string sCust = bll.vLookUp("select cust_cd from tproposal_customer where prop_no='"+hdprop.Value.ToString()+"'");
                //lbname.Text = bll.vLookUp("select cust_nm from tmst_customer where cust_cd='"+sCust+"' and salespointcd='"+Request.Cookies["sp"].Value.ToString()+"'");
            }
            else if (sType == "G")
            {
                lbtype.Text = "By Group";
                arr.Clear();
                arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
                arr.Add(new cArrayList("@type", "cusgrcd"));
                bll.vBindingComboToSp(ref cbreceive, "sp_getproposal_bytype", "cusgrcd", "fld_desc", arr);
                //string sGroup = bll.vLookUp("select cusgrcd from tproposal_cusgrcd where prop_no='" + hdprop.Value.ToString() + "'");
                //lbname.Text = bll.vLookUp("select fld_desc from tfield_value where fld_nm='cusgrcd' and fld_valu='" + sGroup + "'");
            }
            else if (sType == "T")
            {
                lbtype.Text = "By Channel";
                //GetCompletionList3.Cont
                cbreceive_AutoCompleteExtender.ContextKey = hdprop.Value.ToString();
                // string sOtlCode = bll.vLookUp("select cust_typ from tproposal_custtype where prop_no='" + hdprop.Value.ToString() + "'");
                //lbname.Text = bll.vLookUp("select fld_desc from tfield_value where fld_nm='otlcd' and fld_valu='" + sOtlCode + "'");
            }
            cbreceive.Enabled = true; 
        }
        rs.Close();
        arr.Clear();
        arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
        lbclaimno.CssClass = "form-control input-sm ";
        lbclaimno.Enabled = true;
        bll.vBindingComboToSp(ref cbpaymentschedule, "sp_tproposal_payment_getbypropno", "sequenceno", "prop_desc", arr);

        // Payment Date Set to Wazaran 
        dtpaid.CssClass = "form-control input-sm ro";
        dtpaid.Enabled = false;
        dtpaid.Text = Request.Cookies["waz_dt"].Value.ToString();

        // Customer by Contract 
        txcontract.Enabled = false;       
        txprop.CssClass = "form-control input-sm ro";
        txprop.Enabled = false;

        // Amount to be paid 
        arr.Clear();
        arr.Add(new cArrayList("@claimco_cd", hdclaimno.Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tproposalpaidho_get", arr);

        string sfile_nm = bll.vLookUp("select fileloc from tproposal_paid_ho_doc where claimco_cd ='" + hdclaimno.Value.ToString() + "'");
        if (sfile_nm != "")
        {
            hpfile_nm.Visible = true;
            upl.Visible = false;
            lblocfile.Text = sfile_nm;
            hpfile_nm.NavigateUrl = "/images/claim_cashout_ho/" + sfile_nm;
        }

        btprint.Visible = false;
        btsave.Visible = false;
        btapprove.Visible = true;
        btreject.Visible = true;

    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        grd.EditIndex = e.NewEditIndex;
        arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tproposal_payment_get", arr);
    }
    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Label lbsequence = (Label)grd.Rows[e.RowIndex].FindControl("lbsequence");
        Label lbschedule = (Label)grd.Rows[e.RowIndex].FindControl("lbschedule");
        TextBox dtpaid = (TextBox)grd.Rows[e.RowIndex].FindControl("dtpaid");
        TextBox txamt = (TextBox)grd.Rows[e.RowIndex].FindControl("txamt");
        Random rnd = new Random();
        int token = rnd.Next(1000, 9999);
        string sCashout = bll.vLookUp("select dbo.fn_checkexistcashout('" + hdprop.Value.ToString() + "')");
        if (sCashout != "ok")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('" + sCashout + "','" + hdprop.Value.ToString() + "','warning');", true);
            return;
        }

        if (txamt.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Amount for cashout can not empty!','" + hdprop.Value.ToString() + "','warning');", true);
            return;
        }
        if (dtpaid.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Date for paid can not empty!','" + hdprop.Value.ToString() + "','warning');", true);
            return;
        }

        // string sClaimNo = string.Empty;
        // List<cArrayList> arr = new List<cArrayList>();
        // arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
        // arr.Add(new cArrayList("@sequenceno" , lbsequence.Text));
        // arr.Add(new cArrayList("@schedule_dt", System.DateTime.ParseExact( lbschedule.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        // arr.Add(new cArrayList("@paid_dt", System.DateTime.ParseExact(dtpaid.Text,"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        // arr.Add(new cArrayList("@amt", txamt.Text));
        // arr.Add(new cArrayList("@claimco_sta_id", "N"));
        // arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
        // arr.Add(new cArrayList("@rdinternal", rdrcpt.SelectedValue.ToString()));
        // arr.Add(new cArrayList("@phone_no", txphone.Text));
        // arr.Add(new cArrayList("@receivedby", txname.Text));
        // bll.vInsertProposalPaid(arr, ref sClaimNo);
        // lbclaimno.Text = sClaimNo;
        // grd.EditIndex =- 1;
        // arr.Clear();
        // arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
        // bll.vBindingGridToSp(ref grd, "sp_tproposal_payment_get", arr);
        // //Send Approval SMS
        // string sSPN = bll.vLookUp("select salespoint_sn from tmst_salespoint where salespointcd='"+ Request.Cookies["sp"].Value.ToString()+ "'");
        // List<string> lapproval = bll.lGetApproval("claimapp", 1);
        // string sSMS = "#Claim Cashout Prop:" + hdprop.Value.ToString() + ", SP:"+sSPN+", will be paid:" + txamt.Text + ",pls reply with (Y/N)" + token.ToString();
        //// cd.vSendSms(sSMS, lapproval[0]);
        // arr.Clear();
        // arr.Add(new cArrayList("@token", token.ToString()));
        // arr.Add(new cArrayList("@doc_typ", "claimapp"));
        // arr.Add(new cArrayList("@to", lapproval[0]));
        // arr.Add(new cArrayList("@doc_no", sClaimNo));
        // arr.Add(new cArrayList("@msg", sSMS));
        // bll.vInsertSmsOutbox(arr);
        // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Proposal paid has been saved successfully','" + hdprop.Value.ToString() + "','success');", true);
        // return;

    }
    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    Label lbpaiddate = (Label)e.Row.FindControl("lbpaiddate");
        //    Label lbamt = (Label)e.Row.FindControl("lbamt");
        //    Label lbsequence = (Label)e.Row.FindControl("lbsequence");

        //    string sPaid = bll.vLookUp("select amt from tproposal_paid where prop_no='" + hdprop.Value.ToString() + "' and sequenceno=" + lbsequence.Text);
        //    string sDate = bll.vLookUp("select paid_dt from tproposal_paid where prop_no='" + hdprop.Value.ToString() + "' and sequenceno=" + lbsequence.Text);
        //    if (sDate != "")
        //    {
        //        lbpaiddate.Text = Convert.ToDateTime(sDate).ToString("d/M/yyyy");
        //    }
        //    if (sPaid != "")
        //    {
        //        lbamt.Text = sPaid;
        //        e.Row.ToolTip = "This proposal has been paid !";
        //        e.Row.Enabled = false;
        //    }
        //}
    }
    protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        grd.EditIndex = -1;
        arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tproposal_payment_get", arr);
    }
    protected void bnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_claimcashoutho.aspx");
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        if (hdclaimno.Value == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Claim not yet save','','warning');", true);
            return;
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=clco&cno=" + hdclaimno.Value.ToString() + "');", true);
        // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report.aspx?src=clco');", true);
    }
    protected void rdrcpt_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdrcpt.SelectedValue.ToString() == "I")
        { txname_AutoCompleteExtender.Enabled = true; uploadDoc.Attributes.Remove("style"); }
        else { txname_AutoCompleteExtender.Enabled = false; uploadDoc.Attributes.Remove("style"); }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList2(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lEmployee = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string sEmployee = string.Empty;
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        arr.Add(new cArrayList("@emp_nm", prefixText));
        arr.Add(new cArrayList("@job_title", "cashout_job"));
        bll.vSearchMstEmployee2bysalespoint(arr, ref rs);
        while (rs.Read())
        {
            sEmployee = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["emp_cd"].ToString() + "-" + rs["emp_nm"], rs["emp_cd"].ToString());
            lEmployee.Add(sEmployee);
        }
        return (lEmployee.ToArray());
    }
    protected void btsearchclaim_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "popupwindow('fm_lookupclaimcashoutho.aspx');", true);
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        double totalBA = 0; string nameSms = "";
        string sType = bll.vLookUp("select promogroup from tmst_proposal where prop_no='" + hdprop.Value.ToString() + "'");
        if (sType == "DR" || sType == "TB" || sType == "SB" || sType == "DS")
        {
            if (txcontract.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('This Cash Out need Business Agreement. ','BA No.','warning');", true);
                return;
            }

            totalBA = double.Parse(bll.vLookUp("select a.amount-(isnull(amt,0)) from tcontract_payschedule a left join tproposal_paid_ho b on a.contract_no = b.contract_no and b.claimco_sta_id ='A' where a.contract_no='"+hdcontract.Value.ToString()+"'"));

            //if (double.Parse(txamt.Text) > totalBA)
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('This Cash Out Amount Exceed.','Cashout Amount','warning');", true);
            //    return;
            //}

        }

        if (rdrcpt.SelectedValue.ToString() == "E" || rdrcpt.SelectedValue.ToString() == "I")
        {
            if (upl.FileName == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('No Image uploaded','Please scan the document and upload','warning');", true);
                return;
            }
        }

        string cdv_no = bll.vLookUp("select count(cdv_no) from tproposal_paid_ho where cdv_no = '"+txcdv.Text+"' and claimco_sta_id not in ('L','E')");

        if (Convert.ToInt16(cdv_no) > 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('CDV No Exists.','CDV No.','warning');", true);
            return;
        }
        if (rdrcpt.SelectedValue.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Claim recepients not yet selected','Select external or internal','warning');", true);
            return;
        }
        if (txname.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt2", "sweetAlert('Claim recepients not yet selected','Select Recepient','warning');", true);
            return;
        }
        if (txphone.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt2", "sweetAlert('Phone No Can Not be Empty.','Phone No','warning');", true);
            return;
        }
        string sClaimNo = string.Empty;
        Random rnd = new Random();
        int token = rnd.Next(1000, 9999);
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
        arr.Add(new cArrayList("@cdv_no", txcdv.Text));
        arr.Add(new cArrayList("@sequenceno", cbpaymentschedule.SelectedValue.ToString()));
        arr.Add(new cArrayList("@schedule_dt", System.DateTime.ParseExact(dtpaid.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@paid_dt", System.DateTime.ParseExact(dtpaid.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@amt", txamt.Text));
        arr.Add(new cArrayList("@claimco_sta_id", "N"));
        arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@rdinternal", rdrcpt.SelectedValue.ToString()));
        arr.Add(new cArrayList("@phone_no", txphone.Text));
        arr.Add(new cArrayList("@month", ddMonth.Text));
        arr.Add(new cArrayList("@year", ddYear.Text));
        arr.Add(new cArrayList("@isVat", rdVat.SelectedValue.ToString()));

        if (rdVat.SelectedValue.ToString() == "0")
        {
            if (hdemp.Value.ToString() == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Claim recepients not yet selected','Select external or internal','warning');", true);
                    return;
            }
            arr.Add(new cArrayList("@receivedby", hdemp.Value.ToString()));
            arr.Add(new cArrayList("@name", bll.vLookUp("select (emp_cd + ' - ' +emp_nm) from tmst_employee where emp_cd='" + hdemp.Value.ToString() + "'")));
            arr.Add(new cArrayList("@vat", "0.0"));
            nameSms = bll.vLookUp("select (emp_cd + ' - ' +emp_nm) from tmst_employee where emp_cd='"+hdemp.Value.ToString()+"'");
        }
        else
        {
            if(lbtype.Text == "By Cust" || lbtype.Text == "By Group")
            {
                if (cbreceive.SelectedValue.ToString() == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Claim recepients not yet selected','Select external or internal','warning');", true);
                    return;
                }
                arr.Add(new cArrayList("@receivedby", cbreceive.SelectedValue.ToString()));
                arr.Add(new cArrayList("@name", txreceive.Text));
                arr.Add(new cArrayList("@vat", (double.Parse(txamt.Text) * 0.05).ToString()));
                nameSms = bll.vLookUp("select (cust_cd + ' - ' +cust_nm) from tmst_customer where cust_cd='" + cbreceive.SelectedValue.ToString() + "'");
            }
            else
            {
                if (hdcust.Value.ToString() == "")
                {
                    if (lbtype.Text != "By Channel")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Claim recepients not yet selected','Select external or internal','warning');", true);
                        return;
                    }                   
                }
                if (lbtype.Text == "By Channel")
                {
                    arr.Add(new cArrayList("@receivedby", hdemp.Value.ToString()));
                    arr.Add(new cArrayList("@name", bll.vLookUp("select (emp_cd + ' - ' +emp_nm) from tmst_employee where emp_cd='" + hdemp.Value.ToString() + "'")));
                    arr.Add(new cArrayList("@vat", "0.0"));
                    nameSms = bll.vLookUp("select (emp_cd + ' - ' +emp_nm) from tmst_employee where emp_cd='" + hdemp.Value.ToString() + "'");
                }
                else
                {
                    arr.Add(new cArrayList("@receivedby", hdcust.Value.ToString()));
                    arr.Add(new cArrayList("@name", txname.Text));
                    arr.Add(new cArrayList("@vat", (double.Parse(txamt.Text) * 0.05).ToString()));
                    nameSms = bll.vLookUp("select (cust_cd + ' - ' +cust_nm) from tmst_customer where cust_cd='" + hdcust.Value.ToString() + "'");
                }
            }
            //arr.Add(new cArrayList("@vat", (double.Parse(txamt.Text) * 0.05).ToString()));

        }
        if (sType == "DR" || sType == "TB" || sType == "SB" || sType == "DS")
        {
            arr.Add(new cArrayList("@contract_no", txcontract.Text));
        }
        bll.vInsertProposalPaidHo(arr, ref sClaimNo);
        lbclaimno.Text = sClaimNo;
        hdclaimno.Value = sClaimNo;
        grd.EditIndex = -1;

        if (rdrcpt.SelectedValue.ToString() == "E" || rdrcpt.SelectedValue.ToString() == "I")
        {
            FileInfo fi = new FileInfo(upl.FileName);
            string ext = fi.Extension;
            byte[] fs = upl.FileBytes;
            if (fs.Length <= 5000000)
            {
                if (ext == ".jpg" || ext == ".jpeg" || ext == ".bmp" || ext == ".gif" || ext == ".png" || ext == ".JPEG" || ext == ".JPG" || ext == ".BMP" || ext == ".GIF" || ext == ".PNG")
                {
                    if ((upl.FileName != "") || (upl.FileName != null))
                    {
                        arr.Clear();
                        arr.Add(new cArrayList("@claimco_cd", sClaimNo));
                        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                        arr.Add(new cArrayList("@file", sClaimNo.ToString() + ext));
                        upl.SaveAs(bll.sGetControlParameter("image_path") + "/claim_cashout_ho/" + sClaimNo.ToString() + ext);

                        bll.vInsertCashoutHoDocument(arr);
                        arr.Clear();
                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Image Uploaded','" + sClaimNo.ToString() + ext + "','success');", true);
                        hpfile_nm.Visible = true;
                        upl.Visible = false;
                        lblocfile.Text = sClaimNo.ToString() + ext;
                        hpfile_nm.NavigateUrl = "/images/claim_cashout_ho/" + sClaimNo.ToString() + ext;
                        //return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('You should upload image','jpg,bmp,gif and png upload document again');", true);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Image size too big','Image size must be less than 500KB');", true);
                return;
            }
        }

        arr.Clear();
        arr.Add(new cArrayList("@claimco_cd", sClaimNo));
        bll.vBindingGridToSp(ref grd, "sp_tproposalpaidho_get", arr);
        arr.Clear();
        btsave.CssClass = "divhid";
        btprint.CssClass = "btn-info btn btn-print";
        btprint.Enabled = true;
        btapprove.Visible = true;
        btreject.Visible = true;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Claim Cashout (HO) has been saved successfully','" + hdprop.Value.ToString() + "','success');", true);
        return;
    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        if ((Convert.ToDouble(txbudget.Text) < (Convert.ToDouble(txamt.Text))))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Total Amount must be less than / equal with budget','Amount','warning');", true);
            return;
        }        
        if (rdrcpt.SelectedValue.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Claim recepients not yet selected','Select external or internal','warning');", true);
            return;
        }
        if (txname.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt2", "sweetAlert('Claim recepients not yet selected','Select Recepient','warning');", true);
            return;
        }
        double dAmt = 0;
        if (!double.TryParse(txamt.Text, out dAmt))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt3", "sweetAlert('Amount must numeric','Entry Amount','warning');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@sequenceno", cbpaymentschedule.SelectedValue.ToString()));
        arr.Add(new cArrayList("@qty", txamt.Text));
        arr.Add(new cArrayList("@payment_dt", System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@isVat", rdVat.SelectedValue.ToString()));
        if (rdVat.SelectedValue.ToString() == "1")
        {
            arr.Add(new cArrayList("@vat", (double.Parse(txamt.Text)*0.05).ToString()));
        }
        else
        {
            arr.Add(new cArrayList("@vat", "0.0"));
        }
        bll.vInsertWrkProposalPaidHo(arr);
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_twrk_proposalpaidho_get", arr);
        btsave.CssClass = "btn-warning btn btn-save";
        //btsave.CssClass = "btn-warning btn button2 btn-save";
        btsave.Enabled = true;
        cbpaymentschedule.Focus();

    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList3(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lCustomer = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string sCustomer = string.Empty;
        arr.Add(new cArrayList("@type", "channel"));
        arr.Add(new cArrayList("@prop_no", contextKey));
        arr.Add(new cArrayList("@cust_nm", prefixText));
        bll.vSearchMstEmployee(arr, ref rs);
        while (rs.Read())
        {
            sCustomer = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + "-" + rs["cust_nm"], rs["cust_cd"].ToString());
            lCustomer.Add(sCustomer);
        }
        return (lCustomer.ToArray());
    }
    protected void btsearchprop_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ops", "popupwindow('lookproposal2.aspx');", true);
    }

    protected void btsearch2_Click(object sender, EventArgs e)
    {
        //txphone.CssClass = "form-control input-sm ro";
        string sPhone = bll.vLookUp("select phone_no from tmst_employee where emp_cd='" + hdemp.Value.ToString() + "'");
        txphone.Text = sPhone;
    }

    protected void btsearchcontract_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "popupwindow('fm_lookupcontract.aspx?prop="+hdprop.Value.ToString()+"');", true);
    }
    protected void txcontract_TextChanged(object sender, EventArgs e)
    {

    }
    protected void btsearch3_Click(object sender, EventArgs e)
    {
        string rdcust = "";
        txcontract.CssClass = "form-control input-sm ro";
        txcontract.Enabled = false;
        txcontract.Text = hdcontract.Value.ToString();
        txamt.Text = bll.vLookUp("select amount from tcontract_payschedule where contract_no='"+hdcontract.Value.ToString()+"' order by amount desc");
        
        rdcust = bll.vLookUp("select rdcust from tmst_proposal where prop_no='"+txprop.Text.ToString()+"'");
        if (rdcust == "C")
        {
            cbreceive.SelectedValue = bll.vLookUp("select top 1 cust_cd from tcontract_customer where contract_no='" + hdcontract.Value.ToString() + "'");
        }

        //cbreceive.Enabled = false;
        //txamt.CssClass = "form-control input-sm ro";
        //btadd_Click(sender, e);
        //btadd.Enabled = false;
    }
    protected void btapprove_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();

        if (bll.nCheckAccess("appcoho", Request.Cookies["usr_id"].Value.ToString()) == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To approve this Cashout (HO), contact Administrator !!','warning');", true);
            return;
        }

        arr.Add(new cArrayList("@claimco_cd", hdclaimno.Value.ToString()));
        arr.Add(new cArrayList("@approveby", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@claimco_sta_id", "A"));
        bll.vUpdateProposalPaidHo(arr);
        arr.Clear();
        btapprove.Visible = false;
        btreject.Visible = false;
        bnew.Visible = true;

    }
    protected void btreject_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();

        if (bll.nCheckAccess("appcoho", Request.Cookies["usr_id"].Value.ToString()) == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To reject this Cashout (HO), contact Administrator !!','warning');", true);
            return;
        }

        arr.Add(new cArrayList("@claimco_cd", hdclaimno.Value.ToString()));
        arr.Add(new cArrayList("@approveby", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@claimco_sta_id", "E"));
        bll.vUpdateProposalPaidHo(arr);
        arr.Clear();
        btapprove.Visible = false;
        btreject.Visible = false;
        bnew.Visible = true;
    }
}