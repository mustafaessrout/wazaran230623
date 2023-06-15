using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_claimcashout : System.Web.UI.Page
{
    cbll bll = new cbll();
    System.Data.SqlClient.SqlDataReader rs = null;

    double amount = 0;
    double vatAmount = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        {
            dtTransdate.CssClass = "form-control input-sm ro";
            dtTransdate.Enabled = false;
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
            bll.vDelWrkProposalPaid(arr);
            btsave.CssClass = "divhid";
            btprint.CssClass = "divhid";
            vContract.Attributes.Add("style", "display:none");
            Emp.Attributes.Add("style", "display:none");
            PettycashRefno.Attributes.Add("style", "display:none");
            Balance.Attributes.Add("style", "display:none");
            bll.vBindingComboToSp(ref ddlEmp, "sp_tpch_pettycash_getEmpPettycashProfile2", "emp_cd", "emp_desc");
            ddlEmp.Items.Insert(0, new ListItem("Select", "0"));
            bll.vBindingComboToSp(ref ddYear, "sp_tmst_period_getbyyear", "yearvalue", "yearvalue");
            ddMonth.SelectedValue = DateTime.Now.Month.ToString("00");
            lblBalance.Text = "0.00";

            rdrcpt.SelectedValue = "I";

            if (ddlpettycashRefno.Visible == true && ddlpettycashRefno != null && ddlpettycashRefno.SelectedValue.ToString() != "")
            {
                rdVat.SelectedValue = bll.vLookUp("select isnull(r.isvat,0)isvat from (select case when vat_amt>0 then 1 else 0 end isvat from tcashout_request where vat_amt>0 and cashout_cd='" + ddlpettycashRefno.SelectedValue.ToString() + "') r right join (select 1 isvat)x on r.isvat=x.isvat");
            }
        }
        ddlPaySrc.Items.FindByValue("Pettycash").Attributes.Add("style", "background-color:#ADFF2F");
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

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList3(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        //HttpCookie cok;
        //cok = HttpContext.Current.Request.Cookies["sp"];
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lCustomer = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string sCustomer = string.Empty;
        arr.Add(new cArrayList("@prop_no", contextKey));
        arr.Add(new cArrayList("@type", HttpContext.Current.Request.Cookies["btype"].Value.ToString()));
        bll.vSearchMstCustomerBytype(arr, ref rs);
        while (rs.Read())
        {
            sCustomer = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_nm"].ToString(), rs["cust_cd"].ToString());
            lCustomer.Add(sCustomer);
        }
        return (lCustomer.ToArray());
    }


    protected void txprop_TextChanged(object sender, EventArgs e)
    {
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        System.Data.SqlClient.SqlDataReader rs = null;
        string sPaySrc = string.Empty;
        if (hdclaimno.Value.ToString() != "")
        {
            string sPropNo = bll.vLookUp("select prop_no from tproposal_paid where claimco_cd='" + hdclaimno.Value.ToString() + "'");
            string sContract = bll.vLookUp("select contract_no from tproposal_paid where claimco_cd = '" + hdclaimno.Value.ToString() + "'");
            string sMonth = bll.vLookUp("select tx_month from tproposal_paid where claimco_cd = '" + hdclaimno.Value.ToString() + "'");
            string sYear = bll.vLookUp("select tx_year from tproposal_paid where claimco_cd = '" + hdclaimno.Value.ToString() + "'");
            string sCDV = bll.vLookUp("select cdv_no from tproposal_paid where claimco_cd = '" + hdclaimno.Value.ToString() + "'");
            sPaySrc = bll.vLookUp("select (case when PaySrc = 'Pettycash' then 'Pettycash' else 'Regular' end)PaySrc from tproposal_paid where claimco_cd = '" + hdclaimno.Value.ToString() + "'");
            string sPettycashEmp_cd = bll.vLookUp("select PettycashEmp_cd from tproposal_paid where claimco_cd = '" + hdclaimno.Value.ToString() + "'");
            string sPettycashEmp_nm = bll.vLookUp("select a.PettycashEmp_cd+' - '+b.emp_nm from tproposal_paid a left join tmst_employee b on a.PettycashEmp_cd = b.emp_cd where a.claimco_cd = '" + hdclaimno.Value.ToString() + "'");
            string sPettycashRefno = bll.vLookUp("select pettycashRefno from tproposal_paid where claimco_cd = '" + hdclaimno.Value.ToString() + "'");
            txprop.Text = sPropNo;
            hdprop.Value = sPropNo;
            ddMonth.SelectedValue = sMonth;
            ddYear.SelectedValue = sYear;
            txcdv.Text = sCDV;
            txname.Text = "";
            txreceive.Text = "";
            if (sPaySrc == "Pettycash")
            {
                Emp.Attributes.Remove("style");
                ddlPaySrc.SelectedValue = sPaySrc;
                if (sPettycashEmp_cd != "")
                {
                    ddlEmp.Items.Clear();
                    ddlEmp.Items.Insert(0, new ListItem("Select", "0"));
                    ddlEmp.Items.Insert(1, new ListItem(sPettycashEmp_nm, sPettycashEmp_cd));
                    ddlEmp.SelectedValue = sPettycashEmp_cd;
                    PettycashRefno.Attributes.Remove("style");
                    Balance.Attributes.Remove("style");
                    ddlpettycashRefno.Items.Clear();
                    ddlpettycashRefno.Items.Insert(0, new ListItem("Select", "0"));
                    ddlpettycashRefno.Items.Insert(1, new ListItem(sPettycashRefno, sPettycashRefno));
                    ddlpettycashRefno.SelectedValue = sPettycashRefno;
                    //dtTransdate.Text = Convert.ToDateTime(bll.vLookUp("select convert(date,trns_dt) from tpch_pettycash where cashout_cd='" + sPettycashRefno + "'")).ToString("dd/MM/yyyy");
                    dtTransdate.Text = Convert.ToDateTime(bll.vLookUp("select schedule_dt from tproposal_paid where pettycashRefno='" + sPettycashRefno + "'")).ToString("dd/MM/yyyy");
                    //lblBalance.Text = bll.vLookUp("select balance from tpch_pettycash where cashout_cd='" + sPettycashRefno + "'");
                    lblBalance.Text = bll.vLookUp("select sum(balance)balance from(select doc_no,isnull(sum(credit),0)+isnull(sum(vat_Amt),0)balance from temployee_advanced_cash where emp_cd='" + ddlEmp.SelectedValue.ToString() + "' and pc_sta_id='A' group by doc_no union all select pettycashRefno,isnull(sum(amt),0)+isnull(sum(vat),0)balance from tproposal_paid where claimco_sta_id<>'R' and PettycashEmp_cd='" + ddlEmp.SelectedValue.ToString() + "' group by pettycashRefno)x where doc_no='" + sPettycashRefno + "'");
                    txamt.Text = lblBalance.Text;
                    txamt.Enabled = false;
                }

            }
            else
            {
                Emp.Attributes.Add("style", "display:none");
                PettycashRefno.Attributes.Add("style", "display:none");
                Balance.Attributes.Add("style", "display:none");
                ddlEmp.SelectedValue = "0";
                lblBalance.Text = "0.00";
                ddlPaySrc.SelectedValue = sPaySrc;
                txamt.Text = "";
                txamt.Enabled = true;
                dtTransdate.Text = "";
            }
            string sType = bll.vLookUp("select promogroup from tmst_proposal where prop_no='" + hdprop.Value.ToString() + "'");
            if (sType == "DR" || sType == "TB" || sType == "SB" || sType == "DS")
            {
                vContract.Attributes.Remove("style");
                txcontract.Text = sContract;
                if (bll.vLookUp("select rdcust from tmst_proposal where prop_no='" + txprop.Text.ToString() + "'") == "C")
                {
                    //cbreceive.SelectedValue = bll.vLookUp("select top 1 cust_cd from tcontract_customer where contract_no='" + txcontract.Text + "'");
                    txreceive.Text = bll.vLookUp("select top 1 (a.cust_cd +' - '+ cust_nm)cust_nm from tcontract_customer a left join tmst_customer b on a.cust_cd=b.cust_cd where a.contract_no='" + txcontract.Text + "'");
                    hdcust.Value = bll.vLookUp("select top 1 cust_cd from tcontract_customer where contract_no='" + txcontract.Text + "'");
                }
            }
            else
            {
                vContract.Attributes.Add("style", "display:none");
            }
            string sSel = bll.vLookUp("select rdinternal from tproposal_paid where claimco_cd='" + hdclaimno.Value.ToString() + "'");
            if (sSel != "")
            {
                rdrcpt.SelectedValue = sSel;
            }
            //txname.Text = bll.vLookUp("select (case when rdinternal='I' then (receivedby+'-'+emp_nm) else name end)as receive from tproposal_paid left join tmst_employee on tproposal_paid.receivedby = tmst_employee.emp_cd where claimco_cd='" + hdclaimno.Value.ToString() + "'");
            txname.Text = bll.vLookUp("select (receivedby+'-'+emp_nm)  as receive from tproposal_paid left join tmst_employee on tproposal_paid.receivedby = tmst_employee.emp_cd where claimco_cd='" + hdclaimno.Value.ToString() + "'");
            if (txreceive.Text == "")
            { txreceive.Text = bll.vLookUp("select (case when rdinternal='E' then (receivedbyCust+'-'+cust_nm) else name end)as receive from tproposal_paid left join tmst_customer on tproposal_paid.receivedbyCust = tmst_customer.cust_cd where claimco_cd='" + hdclaimno.Value.ToString() + "'"); }
            lbclaimno.Text = hdclaimno.Value.ToString();
            txphone.Text = bll.vLookUp("select phone_no from tproposal_paid where claimco_cd='" + hdclaimno.Value.ToString() + "'");
            txamt.Text = bll.vLookUp("select amt from tproposal_paid where claimco_cd='" + hdclaimno.Value.ToString() + "'");
            dtpaid.Text = Convert.ToDateTime(bll.vLookUp("select paid_dt from tproposal_paid where claimco_cd='" + hdclaimno.Value.ToString() + "'")).ToString("d/M/yyyy");
            //cbreceive.Enabled = false; 
            string sfile_nm = bll.vLookUp("select fileloc from tproposal_paid_doc where claimco_cd ='" + hdclaimno.Value.ToString() + "'");
            if (sfile_nm != "")
            {
                hpfile_nm.Visible = true;
                upl.Visible = false;
                lblocfile.Text = sfile_nm;
                hpfile_nm.NavigateUrl = "/images/claim_cashout/" + sfile_nm;
            }

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
                vContract.Attributes.Add("style", "display:none");
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
                //cbreceive.Visible = true;
                //cbreceivePanel.Visible = true;
                //txreceive.Visible=true;
                txreceive_AutoCompleteExtender.ContextKey = hdprop.Value.ToString();
                Response.Cookies["btype"].Value = "cust";
                lbtype.Text = "By Cust";
                //arr.Clear();
                //arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
                //arr.Add(new cArrayList("@type", "cust"));
                //bll.vBindingComboToSp(ref cbreceive, "sp_getproposal_bytype", "cust_cd", "cust_nm", arr);               
                //string sCust = bll.vLookUp("select cust_cd from tproposal_customer where prop_no='"+hdprop.Value.ToString()+"'");
                //lbname.Text = bll.vLookUp("select cust_nm from tmst_customer where cust_cd='"+sCust+"' and salespointcd='"+Request.Cookies["sp"].Value.ToString()+"'");
            }
            else if (sType == "G")
            {
                //cbreceivePanel.Visible = true;
                //txreceive.Visible=true;
                lbtype.Text = "By Group";
                txreceive_AutoCompleteExtender.ContextKey = hdprop.Value.ToString();
                Response.Cookies["btype"].Value = "cusgrcd";
                //arr.Clear();
                //arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
                //arr.Add(new cArrayList("@type", "cusgrcd"));
                //bll.vBindingComboToSp(ref cbreceive, "sp_getproposal_bytype", "cusgrcd", "fld_desc", arr);
                //string sGroup = bll.vLookUp("select cusgrcd from tproposal_cusgrcd where prop_no='" + hdprop.Value.ToString() + "'");
                //lbname.Text = bll.vLookUp("select fld_desc from tfield_value where fld_nm='cusgrcd' and fld_valu='" + sGroup + "'");
            }
            else if (sType == "T")
            {
                //cbreceivePanel.Visible = true;
                //txreceive.Visible=true;
                lbtype.Text = "By Channel";
                txreceive_AutoCompleteExtender.ContextKey = hdprop.Value.ToString();
                Response.Cookies["btype"].Value = "otlcd";
                //GetCompletionList3.Cont
                //cbreceive_AutoCompleteExtender.ContextKey = hdprop.Value.ToString();
                // string sOtlCode = bll.vLookUp("select cust_typ from tproposal_custtype where prop_no='" + hdprop.Value.ToString() + "'");
                //lbname.Text = bll.vLookUp("select fld_desc from tfield_value where fld_nm='otlcd' and fld_valu='" + sOtlCode + "'");
            }
            //cbreceive.Enabled = true;        
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
        if (sPaySrc == "Regular")
        {
            grd2.Visible = false;
            grd.Visible = true;
            bll.vBindingGridToSp(ref grd, "sp_tproposalpaid_get", arr);
        }
        if (sPaySrc == "Pettycash")
        {
            grd.Visible = false;
            grd2.Visible = true;
            bll.vBindingGridToSp(ref grd2, "sp_tproposalpaid_get", arr);
        }

        rdrcpt_SelectedIndexChanged(sender, e);
        ddlPaySrc_SelectedIndexChanged(sender, e);
        if (ddlPaySrc.SelectedValue == "Pettycash")
        {
            uploadDoc.Visible = false;
        }
        else
        {
            uploadDoc.Visible = true;
        }

        //lbpropbudget.Text = String.Format("{0:0.00}", bll.vLookUp("select isnull(cast(budget_limit as numeric(18,2)),0)+isnull(cast(budget_add as numeric(18,2)),0) prop_budget_limit from tmst_proposal where prop_no='" + txprop.Text.ToString() + "'"));
        lbpropsbtc.Text = String.Format("{0:0.00}", bll.vLookUp("select isnull(sbtccost,0) from tmst_proposal where prop_no='" + txprop.Text.ToString() + "'"));
        lbpropprincipal.Text = String.Format("{0:0.00}", bll.vLookUp("select isnull(principalcost,0) from tmst_proposal where prop_no='" + txprop.Text.ToString() + "'"));

    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        grd.EditIndex = e.NewEditIndex;
        arr.Add(new cArrayList("@claimco_cd", hdprop.Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tproposalpaid_get", arr);
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

        arr.Add(new cArrayList("@claimco_cd", hdclaimno.Value.ToString()));
        if (grd.Visible)
        {
            grd.EditIndex = -1;
            bll.vBindingGridToSp(ref grd, "sp_tproposalpaid_get", arr);
        }
        if (grd2.Visible)
        {
            grd2.EditIndex = -1;
            bll.vBindingGridToSp(ref grd2, "sp_tproposalpaid_get", arr);
        }
    }
    protected void bnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_claimcashout.aspx");
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

        txname_AutoCompleteExtender.Enabled = true; //uploadDoc.Attributes.Remove("style");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "callJSFunction", "SelectRdrcpt();", true);

    }

    protected void btRdrcpt_Click(object sender, EventArgs e)
    {
        if (rdrcpt.SelectedValue.ToString() == "I")
        {
            cbreceivePanel.Visible = false;
            txreceive.Visible = false;
        }
        if (rdrcpt.SelectedValue.ToString() == "E")
        {
            if (lbtype.Text == "By Cust" || lbtype.Text == "By Group" || lbtype.Text == "By Channel")
            {
                cbreceivePanel.Visible = true;
                txreceive.Visible = true;
                txreceive_AutoCompleteExtender.Enabled = true;
            }
        }
        if (ddlPaySrc.SelectedValue == "Pettycash")
        {
            uploadDoc.Visible = false;
        }
        else
        {
            uploadDoc.Visible = true;
        }
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
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "popupwindow('fm_lookupclaim.aspx');", true);
    }
    public static bool IsNumeric(object Expression) {
        double retNum;
        bool isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
        return isNum;
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

            totalBA = double.Parse(bll.vLookUp("select a.amount-(isnull(amt,0)) from tcontract_payschedule a left join tproposal_paid b on a.contract_no = b.contract_no and b.claimco_sta_id ='A' where a.contract_no='" + hdcontract.Value.ToString() + "'"));

            //if (double.Parse(txamt.Text) > totalBA)
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('This Cash Out Amount Exceed.','Cashout Amount','warning');", true);
            //    return;
            //}

        }
        if (ddlPaySrc.SelectedIndex.Equals("Pettycash"))
        {

            if (ddlEmp.SelectedIndex.Equals("0"))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('PettyCash Employee not yet selected','Please Select PettyCash Employee','warning');", true);
                return;
            }

            if (ddlpettycashRefno.SelectedValue.Equals("0"))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('PettyCash Refno not yet selected','Please Select PettyCash Refno','warning');", true);
                return;
            }
        }
        if (ddlPaySrc.SelectedValue.Equals("Regular"))
        {
            if (rdrcpt.SelectedValue.ToString() == "E" || rdrcpt.SelectedValue.ToString() == "I")
            {
                if (upl.FileName == "" || (upl.FileName.Equals(null)))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('No Image uploaded','Please scan the document and upload','warning');", true);
                    return;
                }

                FileInfo fi = new FileInfo(upl.FileName);
                string ext = fi.Extension;
                byte[] fs = upl.FileBytes;
                if (fs.Length <= 5242880)
                {
                    if (ext != ".jpg" && ext != ".jpeg" && ext != ".bmp" && ext != ".gif" && ext != ".png" && ext != ".JPEG" && ext != ".JPG" && ext != ".BMP" && ext != ".GIF" && ext != ".PNG" && ext != ".pdf" && ext !=".PDF")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('You should upload image','jpg,bmp,gif,png or pdf upload document again');", true);
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Image size too big','Image size must be less than 5MB');", true);
                    return;
                }

            }

        }


        string cdv_no = bll.vLookUp("select count(cdv_no) from tproposal_paid where cdv_no = '" + txcdv.Text + "' and claimco_sta_id not in ('L','E')");

        if (Convert.ToInt16(cdv_no) > 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('CDV No Exists, has been chosen or not Entered','CDV No." + txcdv.Text + "','warning');", true);
            return;
        }
        if (rdrcpt.SelectedValue.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Claim recepients not yet selected','Select external or internal','warning');", true);
            return;
        }
        if (txname.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt2", "sweetAlert('Employee Claim recepients not yet selected','Select Employee Recepient','warning');", true);
            return;
        }
        if (txreceive.Text == "" && rdrcpt.SelectedValue.ToString() == "E")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt2", "sweetAlert('Customer Claim recepients not yet selected','Select Customer Recepient','warning');", true);
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
     
        //arr.Add(new cArrayList("@amt", txamt.Text));
        arr.Add(new cArrayList("@claimco_sta_id", "N"));
        arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@rdinternal", rdrcpt.SelectedValue.ToString()));
        arr.Add(new cArrayList("@phone_no", txphone.Text));
        arr.Add(new cArrayList("@month", ddMonth.Text));
        arr.Add(new cArrayList("@year", ddYear.Text));
        arr.Add(new cArrayList("@isVat", rdVat.SelectedValue.ToString()));
        arr.Add(new cArrayList("@PaySrc", ddlPaySrc.SelectedValue.ToString()));
        double vat_amt = 0.00;

        if (IsNumeric(txamt.Text) == false) {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt2", "sweetAlert('Amount should be Numeric','Please input in Number','warning');", true);
            return;
        }

        double dAmut = double.Parse(txamt.Text);
        vat_amt = (double.Parse(txamt.Text) * 0.05);

        if (rdVat.SelectedValue.ToString() == "0" || rdVat.SelectedValue.ToString() == "")
        {
            arr.Add(new cArrayList("@vat", "0.0"));
            //arr.Add(new cArrayList("@amt", txamt.Text));
            if (ddlPaySrc.SelectedValue.Equals("Pettycash"))
            {
                amount = Convert.ToDouble(double.Parse(bll.vLookUp("select isnull(credit,0)amt from temployee_advanced_cash where doc_no='" + ddlpettycashRefno.SelectedValue.ToString() + "';")));
                arr.Add(new cArrayList("@amt", amount));
            }
            else if (ddlPaySrc.SelectedValue.Equals("Regular"))
            {
                arr.Add(new cArrayList("@amt", txamt.Text));
            }
        }

        else if (rdVat.SelectedValue.ToString() == "1")
        {
            //double dPctVat = Convert.ToDouble(bll.sGetControlParameter("vat"));
            //double dValue = (1 / (1 + dPctVat)) * dAmut;
            //double dVat = dAmut - dValue;
            //arr.Add(new cArrayList("@vat", Math.Round(dVat, 2, MidpointRounding.AwayFromZero).ToString()));
            //arr.Add(new cArrayList("@amt", Math.Round(dValue, 2, MidpointRounding.AwayFromZero).ToString()));

            if (ddlPaySrc.SelectedValue == "Pettycash")
            {
                vatAmount = Convert.ToDouble(double.Parse(bll.vLookUp("select isnull(vat_amt,0)amt from temployee_advanced_cash where doc_no='" + ddlpettycashRefno.SelectedValue.ToString() + "';")));
                arr.Add(new cArrayList("@vat", vatAmount));
                amount = Convert.ToDouble(double.Parse(bll.vLookUp("select isnull(credit,0)+isnull(amt_returned,0)amt from temployee_advanced_cash where doc_no='" + ddlpettycashRefno.SelectedValue.ToString() + "';")));
                arr.Add(new cArrayList("@amt", amount));
            }
            else if (ddlPaySrc.SelectedValue == "Regular")
            {
                double dPctVat = Convert.ToDouble(bll.sGetControlParameter("vat"));
                double dValue = (1 / (1 + dPctVat)) * dAmut;
                double dVat = dAmut - dValue;
                arr.Add(new cArrayList("@vat", Math.Round(dVat, 2, MidpointRounding.AwayFromZero).ToString()));
                arr.Add(new cArrayList("@amt", Math.Round(dValue, 2, MidpointRounding.AwayFromZero).ToString()));
            }
        }

        if (ddlPaySrc.SelectedValue.ToString() == "Pettycash")
        {
            if (ddlEmp.SelectedValue.ToString() != "0")
            {
                arr.Add(new cArrayList("@PettycashEmp_cd", ddlEmp.SelectedValue.ToString()));
                arr.Add(new cArrayList("@pettycashRefno", ddlpettycashRefno.SelectedValue.ToString()));
            }
        }
        else
        {
            arr.Add(new cArrayList("@PettycashEmp_cd", "N/A"));
            arr.Add(new cArrayList("@pettycashRefno", "N/A"));
        }


        //if (rdVat.SelectedValue.ToString() == "1")
        //{
        //    //arr.Add(new cArrayList("@vat", (double.Parse(txamt.Text) * 0.05).ToString()));
        //    vatAmount = Convert.ToDouble(double.Parse(bll.vLookUp("select isnull(vat_amt,0)amt from temployee_advanced_cash where doc_no='" + ddlpettycashRefno.SelectedValue.ToString() + "';")));
        //    arr.Add(new cArrayList("@vat", vatAmount));
        //}
        //if (rdVat.SelectedValue.ToString() == "1")
        //{
        //    arr.Add(new cArrayList("@vat", (double.Parse(txamt.Text) * 0.05).ToString()));
        //}

        if (rdrcpt.SelectedValue.ToString() == "I")
        {
            if (hdemp.Value.ToString() == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Employee Claim recepients not yet selected','Select external or internal','warning');", true);
                return;
            }
            arr.Add(new cArrayList("@receivedby", hdemp.Value.ToString()));
            arr.Add(new cArrayList("@receivedbyCust", "N/A"));
            arr.Add(new cArrayList("@name", bll.vLookUp("select (emp_cd + ' - ' +emp_nm) from tmst_employee where emp_cd='" + hdemp.Value.ToString() + "'")));
            nameSms = bll.vLookUp("select (emp_cd + ' - ' +emp_nm) from tmst_employee where emp_cd='" + hdemp.Value.ToString() + "'");
        }
        if (rdrcpt.SelectedValue.ToString() == "E")
        {
            if (lbtype.Text == "By Cust" || lbtype.Text == "By Group" || lbtype.Text == "By Channel")
            {
                if (hdemp.Value.ToString() == "" || hdcust.Value.ToString() == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Customer or Employee Claim recepients not yet selected','Select external or internal','warning');", true);
                    return;
                }
                arr.Add(new cArrayList("@receivedbyCust", hdcust.Value.ToString()));
                arr.Add(new cArrayList("@receivedby", hdemp.Value.ToString()));
                arr.Add(new cArrayList("@name", bll.vLookUp("select (cust_cd + ' - ' +cust_nm) from tmst_customer where cust_cd='" + hdcust.Value.ToString() + "'")));
                nameSms = bll.vLookUp("select (cust_cd + ' - ' +cust_nm) from tmst_customer where cust_cd='" + hdcust.Value.ToString() + "'");
            }

            // if (lbtype.Text == "By Channel")
            //{
            //    if (hdcust.Value.ToString() == "")
            //        {
            //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Claim recepients not yet selected','Select external or internal','warning');", true);
            //            return;
            //        }
            //    arr.Add(new cArrayList("@receivedby", hdemp.Value.ToString()));
            //    arr.Add(new cArrayList("@name", bll.vLookUp("select (emp_cd + ' - ' +emp_nm) from tmst_employee where emp_cd='" + hdemp.Value.ToString() + "'")));
            //    arr.Add(new cArrayList("@vat", "0.0"));
            //    nameSms = bll.vLookUp("select (emp_cd + ' - ' +emp_nm) from tmst_employee where emp_cd='" + hdemp.Value.ToString() + "'");
            //    }
            //    else
            //    {
            //        arr.Add(new cArrayList("@receivedby", hdcust.Value.ToString()));
            //        arr.Add(new cArrayList("@name", txname.Text));
            //        arr.Add(new cArrayList("@vat", (double.Parse(txamt.Text) * 0.05).ToString()));
            //        nameSms = bll.vLookUp("select (cust_cd + ' - ' +cust_nm) from tmst_customer where cust_cd='" + hdcust.Value.ToString() + "'");
            //    }
        }
        //arr.Add(new cArrayList("@vat", (double.Parse(txamt.Text) * 0.05).ToString()));

        if (sType == "DR" || sType == "TB" || sType == "SB" || sType == "DS")
        {
            arr.Add(new cArrayList("@contract_no", txcontract.Text));
        }

        //arr.Add(new cArrayList("@approval_cd", cbapproval.SelectedValue.ToString()));

        bll.vInsertProposalPaid(arr, ref sClaimNo);
        lbclaimno.Text = sClaimNo;
        hdclaimno.Value = sClaimNo;
        grd.EditIndex = -1;
        if (ddlPaySrc.SelectedValue.Equals("Regular"))
        {
            if ((upl.FileName != "") && (!upl.FileName.Equals(null)))
            {
                if (rdrcpt.SelectedValue.ToString() == "E" || rdrcpt.SelectedValue.ToString() == "I")
                {
                    FileInfo fi = new FileInfo(upl.FileName);
                    string ext = fi.Extension;
                    byte[] fs = upl.FileBytes;
                    if (fs.Length <= 5242880)
                    {
                        if (ext == ".jpg" || ext == ".jpeg" || ext == ".bmp" || ext == ".gif" || ext == ".png" || ext == ".JPEG" || ext == ".JPG" || ext == ".BMP" || ext == ".GIF" || ext == ".PNG" || ext == ".pdf" || ext == ".PDF")
                        {
                            arr.Clear();
                            arr.Add(new cArrayList("@claimco_cd", sClaimNo));
                            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                            arr.Add(new cArrayList("@file", sClaimNo.ToString() + ext));
                            upl.SaveAs(bll.sGetControlParameter("image_path") + "/claim_cashout/" + sClaimNo.ToString() + ext);

                            bll.vInsertCashoutDocument(arr);
                            arr.Clear();
                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Image Uploaded','" + sClaimNo.ToString() + ext + "','success');", true);
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Data saved successfully','" + sClaimNo.ToString() + "','success');", true);
                            hpfile_nm.Visible = true;
                            upl.Visible = false;
                            lblocfile.Text = sClaimNo.ToString() + ext;
                            hpfile_nm.NavigateUrl = "/images/claim_cashout/" + sClaimNo.ToString() + ext;
                            //return;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('You should upload image','jpg,bmp,gif,png and pdf upload document again');", true);
                            return;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Image size too big','Image size must be less than 5MB');", true);
                        return;
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('You should upload image','jpg,bmp,gif,png or pdf upload document again');", true);
                return;
            }
        }
        else
        {
            arr.Clear();
            arr.Add(new cArrayList("@claimco_cd", sClaimNo));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@file", null));
            bll.vInsertCashoutDocument(arr);
            arr.Clear();
        }




        arr.Clear();
        arr.Add(new cArrayList("@claimco_cd", sClaimNo));
        if (ddlPaySrc.SelectedValue.Equals("Pettycash"))
        {
            grd2.DataSource = null;
            grd2.DataBind();
            bll.vBindingGridToSp(ref grd2, "sp_tproposalpaid_get", arr);
        }
        if (ddlPaySrc.SelectedValue.Equals("Regular"))
        {
            grd.DataSource = null;
            grd.DataBind();
            bll.vBindingGridToSp(ref grd, "sp_tproposalpaid_get", arr);

        }
        arr.Clear();
        arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
        //Send Approval SMS
        string sSPN = bll.vLookUp("select salespoint_sn from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
        List<string> lapproval = bll.lGetApproval("claimcashoutapp", 1);
        string sPropDest = bll.vLookUp("select dbo.fn_getproposaldestination('" + hdprop.Value.ToString() + "')");

        string sSMS = "";
        string stoken = bll.vLookUp("select short_ip from tmst_salespoint where salespointcd=(select parm_valu from tcontrol_parameter where parm_nm='salespoint')") + token.ToString();
        if (rdVat.SelectedValue.ToString() == "0")
        {
            sSMS = "#Claim Cashout(" + sClaimNo.ToString() + ") Prop no:" + hdprop.Value.ToString() + ", BRN:" + sSPN + ",Rcived:" + nameSms + " ,will be paid:" + txamt.Text + " and payment Source " + ddlPaySrc.SelectedItem.Text + " ,pls reply with (Y/N)" + stoken;
        }
        else
        {
            sSMS = "#Claim Cashout(" + sClaimNo.ToString() + ") Prop no:" + hdprop.Value.ToString() + ", BRN:" + sSPN + ",Rcived:" + nameSms + " ,will be paid:" + txamt.Text + " and vat " + (double.Parse(txamt.Text) * 0.05).ToString() + " and payment Source " + ddlPaySrc.SelectedItem.Text + ",pls reply with (Y/N)" + stoken;
        }

        // cd.vSendSms(sSMS, lapproval[0]);
        arr.Clear();
        arr.Add(new cArrayList("@token", stoken));
        arr.Add(new cArrayList("@doc_typ", "claimcashoutapp"));
        arr.Add(new cArrayList("@to", lapproval[0]));
        arr.Add(new cArrayList("@doc_no", sClaimNo));
        arr.Add(new cArrayList("@msg", sSMS));
        bll.vInsertSmsOutbox(arr);
        btsave.CssClass = "divhid";
        btprint.CssClass = "btn-info btn btn-print";
        btprint.Enabled = true;
        if (ddlPaySrc.SelectedValue == "Pettycash")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Pettycash / Cash Advance Claim No." + lbclaimno.Text + " has been saved successfully','Proposal No." + hdprop.Value.ToString() + "','success');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Proposal paid has been saved successfully','" + hdprop.Value.ToString() + "','success');", true);
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Proposal paid has been saved successfully','" + hdprop.Value.ToString() + "','success');", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Proposal paid has been saved successfully','" + sClaimNo + "','success');", true);
        return;
    }
    protected void btadd_Click(object sender, EventArgs e)
    {


        if (ddlPaySrc.SelectedValue == "Regular")
        {
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
            if (txreceive.Text == "" && rdrcpt.SelectedValue.ToString() == "E")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt2", "sweetAlert('Customer Claim recepients not yet selected','Select Customer Recepient','warning');", true);
                return;
            }
            if (txamt.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Amount must be numeric','Amount','warning');", true);
                return;
            }
            double dAmt = 0;
            if (!double.TryParse(txamt.Text, out dAmt))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt3", "sweetAlert('Amount must numeric','Entry Amount','warning');", true);
                return;
            }   

            if ((Convert.ToDouble(txbudget.Text) < (Convert.ToDouble(txamt.Text))))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Total Amount must be less than / equal with budget','Amount','warning');", true);
                return;
            }

            if (txphone.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt2", "sweetAlert('Phone No Can Not be Empty.','Phone No','warning');", true);
                return;
            }
            string cdv_no = bll.vLookUp("select count(cdv_no) from tproposal_paid where cdv_no = '" + txcdv.Text + "' and claimco_sta_id not in ('L','E')");

            if (Convert.ToInt16(cdv_no) > 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('CDV No Exists, has been chosen or not Entered','CDV No."+ txcdv.Text + "','warning');", true);
                return;
            }
        }
        else if (ddlPaySrc.SelectedValue == "Pettycash")
        {
            if (ddlEmp.SelectedValue.Equals("0"))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('PettyCash Employee not yet selected','Please Select PettyCash Employee','warning');", true);
                return;
            }
            if (ddlpettycashRefno.SelectedValue.Equals("0"))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('PettyCash Refno not yet selected','Please Select PettyCash Refno','warning');", true);
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
            if (txreceive.Text == "" && rdrcpt.SelectedValue.ToString() == "E")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt2", "sweetAlert('Customer Claim recepients not yet selected','Select Customer Recepient','warning');", true);
                return;
            }
            if (txamt.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Amount must be numeric','Amount','warning');", true);
                return;
            }

            if (double.Parse(txamt.Text) > double.Parse(lblBalance.Text))
            {
                ddlPaySrc.SelectedValue = "Regular";
                txamt.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "callJSFunction", "alert();", true);
                return;
            }
            //else
            //{
            //    if ((Convert.ToDouble(lblBalance.Text) < (Convert.ToDouble(txamt.Text))))
            //    {
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Total Amount must be less than / equal with balance','Amount','warning');", true);
            //        return;
            //    }
            //}

            if (txphone.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt2", "sweetAlert('Phone No Can Not be Empty.','Phone No','warning');", true);
                return;
            }
            string cdv_no = bll.vLookUp("select count(cdv_no) from tproposal_paid where cdv_no = '" + txcdv.Text + "' and claimco_sta_id not in ('L','E')");

            if (Convert.ToInt16(cdv_no) > 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('CDV No Exists, has been chosen or not Entered','CDV No." + txcdv.Text + "','warning');", true);
                return;
            }
        }



        List<cArrayList> arr = new List<cArrayList>();

        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@sequenceno", cbpaymentschedule.SelectedValue.ToString()));
        arr.Add(new cArrayList("@payment_dt", System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@isVat", rdVat.SelectedValue.ToString()));

        if (rdVat.SelectedValue.ToString() == "1")
        {
            //arr.Add(new cArrayList("@vat", (double.Parse(txamt.Text) * 0.05).ToString()));
            //double vatAmount = Convert.ToDouble(double.Parse(txamt.Text) * 0.05);
            //if (ddlPaySrc.SelectedValue == "Regular")
            //{
            //    if (((Convert.ToDouble(txbudget.Text) + vatAmount) < (Convert.ToDouble(txamt.Text))))
            //    {
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Total Amount must be less than / equal with budget','Amount','warning');", true);
            //        return;
            //    }
            //}
            //else if (ddlPaySrc.SelectedValue == "Pettycash")
            //{
            //    if (((Convert.ToDouble(lblBalance.Text) + vatAmount) < (Convert.ToDouble(txamt.Text))))
            //    {
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Total Amount must be less than / equal with balance','Amount','warning');", true);
            //        return;
            //    }
            //}
            
            if (ddlPaySrc.SelectedValue == "Pettycash")
            {
                vatAmount = Convert.ToDouble(double.Parse(bll.vLookUp("select isnull(vat_amt,0)amt from temployee_advanced_cash where doc_no='" + ddlpettycashRefno.SelectedValue.ToString() + "';")));
                arr.Add(new cArrayList("@vat", vatAmount));
                amount = Convert.ToDouble(double.Parse(bll.vLookUp("select isnull(credit,0)+isnull(amt_returned,0)amt from temployee_advanced_cash where doc_no='" + ddlpettycashRefno.SelectedValue.ToString() + "';")));
                arr.Add(new cArrayList("@qty", amount));
            }
            else if (ddlPaySrc.SelectedValue == "Regular")
            {
                double dAmut = double.Parse(txamt.Text);
                double dPctVat = Convert.ToDouble(bll.sGetControlParameter("vat"));
                double dValue = (1 / (1 + dPctVat)) * dAmut;
                double dVat = dAmut - dValue;
                double vatAmount = Convert.ToDouble(double.Parse(txamt.Text) * 0.05);
                //arr.Add(new cArrayList("@vat", (double.Parse(txamt.Text) * 0.05).ToString()));
                arr.Add(new cArrayList("@vat", Math.Round(dVat, 2, MidpointRounding.AwayFromZero).ToString()));
                arr.Add(new cArrayList("@qty", Math.Round(dValue, 2, MidpointRounding.AwayFromZero).ToString()));
            }
            //arr.Add(new cArrayList("@qty", txamt.Text));

            if (ddlPaySrc.SelectedValue == "Pettycash")
            {
                if (((Convert.ToDouble(lblBalance.Text) + vatAmount) < (Convert.ToDouble(txamt.Text))))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Total Amount must be less than / equal with balance','Amount','warning');", true);
                    return;
                }

            }
            else if (ddlPaySrc.SelectedValue == "Regular")
            {
                if ((Convert.ToDouble(txbudget.Text) < (Convert.ToDouble(txamt.Text))+vatAmount))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Total Amount must be less than / equal with budget','Amount','warning');", true);
                    return;
                }
            }

            //if (((Convert.ToDouble(txbudget.Text) + vatAmount) < (Convert.ToDouble(txamt.Text))))
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Total Amount must be less than / equal with budget','Amount','warning');", true);
            //    return;
            //}
        }
        else
        {
            arr.Add(new cArrayList("@vat", "0.0"));
            arr.Add(new cArrayList("@qty", txamt.Text));
        }

        if (ddlPaySrc.SelectedValue.Equals("Regular"))
        {
            arr.Add(new cArrayList("@isPettycash", "0"));
            arr.Add(new cArrayList("@PettycashEmp_cd", "N/A"));
            bll.vInsertWrkProposalPaid(arr);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Data saved successfully','','success');", true);
        }
        else if (ddlPaySrc.SelectedValue.Equals("Pettycash"))
        {

            arr.Add(new cArrayList("@isPettycash", "1"));
            arr.Add(new cArrayList("@PettycashEmp_cd", ddlEmp.SelectedValue.ToString()));
            bll.vInsertWrkProposalPaid(arr);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Data saved successfully','','success');", true);
        }

        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        if (ddlPaySrc.SelectedValue.Equals("Pettycash"))
        {
            grd2.DataSource = null;
            grd2.DataBind();
            bll.vBindingGridToSp(ref grd2, "sp_twrk_proposalpaid_get", arr);
            for (int i = 0; i < grd2.Rows.Count; i++)
            {
                Label lblPettyCash = (Label)grd2.Rows[i].FindControl("lbPettyCash");
                if (lblPettyCash.Text == "1")
                {
                    lblPettyCash.Text = "Yes";
                }
                if (lblPettyCash.Text == "0")
                {
                    lblPettyCash.Text = "No";
                }
            }
            for (int i = 0; i < grd2.Rows.Count; i++)
            {
                Label lblPettycashEmp_cd = (Label)grd2.Rows[i].FindControl("lbPettycashEmp_cd");
                if (lblPettycashEmp_cd.Text == null)
                {
                    lblPettycashEmp_cd.Text = "N/A";
                }
            }
        }
        else
        {
            grd.DataSource = null;
            grd.DataBind();
            bll.vBindingGridToSp(ref grd, "sp_twrk_proposalpaid_get", arr);
        }
        btsave.CssClass = "btn-warning btn btn-save";
        //btsave.CssClass = "btn-warning btn button2 btn-save";
        btsave.Enabled = true;
        cbpaymentschedule.Focus();

    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList32(string prefixText, int count, string contextKey)
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
        if (sPhone == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt2", "sweetAlert('Phone No is not Available.','Please Conatact Wazaran Admin to update Phone No','warning');", true);
            return;
        }
    }

    protected void btsearchcontract_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "popupwindow('fm_lookupcontract.aspx?prop=" + hdprop.Value.ToString() + "');", true);
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
        txamt.Text = bll.vLookUp("select amount from tcontract_payschedule where contract_no='" + hdcontract.Value.ToString() + "' order by amount desc");

        rdcust = bll.vLookUp("select rdcust from tmst_proposal where prop_no='" + txprop.Text.ToString() + "'");
        if (rdcust == "C")
        {
            //cbreceive.SelectedValue = bll.vLookUp("select top 1 cust_cd from tcontract_customer where contract_no='" + hdcontract.Value.ToString() + "'");
            txreceive.Text = bll.vLookUp("select top 1 (a.cust_cd +' - '+ cust_nm)cust_nm from tcontract_customer a left join tmst_customer b on a.cust_cd=b.cust_cd where a.contract_no='" + hdcontract.Value.ToString() + "'");
            hdcust.Value = bll.vLookUp("select top 1 cust_cd from tcontract_customer where contract_no='" + hdcontract.Value.ToString() + "'");


        }

        //cbreceive.Enabled = false;
        //txamt.CssClass = "form-control input-sm ro";
        //btadd_Click(sender, e);
        //btadd.Enabled = false;
    }

    protected void ddlPaySrc_SelectedIndexChanged(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "callJSFunction", "SelectPaySrc();", true);
        if (ddlPaySrc.SelectedValue == "Regular")
        {
            dvRegular.Visible = true;
            dvRegular1.Visible = true;
            TittleX.Style["background-color"] = "White";
            TittleX.InnerText = "Claim Cash Out Request";
            rdVat.Enabled = true;

            cbapproval.Items.Clear();
            cbapproval.CssClass = cd.csstextro;
        }
        else if (ddlPaySrc.SelectedValue == "Pettycash")
        {
            dvRegular.Visible = false;
            dvRegular1.Visible = false;
            TittleX.Style["background-color"] = "#ADFF2F";
            TittleX.InnerText = "Claim Cash Out Request - Cash Advance Settlement";
            rdVat.Enabled = false;

            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@doc_typ", "cashout"));
            arr.Add(new cArrayList("@level_no", "1"));
            bll.vBindingComboToSp(ref cbapproval, "sp_tapprovalpattern_get", "emp_cd", "emp_nm", arr);
            cbapproval.Items.Insert(0, new ListItem("Select", "0"));
            cbapproval.CssClass = cd.csstext;

        }
    }
    protected void btPaySrc_Click(object sender, EventArgs e)
    {
        if (ddlPaySrc.SelectedValue.ToString() == "Pettycash")
        {
            Emp.Attributes.Remove("style");
            dvRegular.Visible = false;
            dvRegular1.Visible = false;
            Balance.Attributes.Add("style", "display:none");
            uploadDoc.Attributes.Add("style", "display:none");
            if (ddlEmp.SelectedValue.ToString() != "0")
            {
                PettycashRefno.Attributes.Remove("style");
                if (ddlpettycashRefno.SelectedValue.ToString() != "0")
                {
                    Balance.Attributes.Remove("style");
                }
                else
                {
                    dtTransdate.Text = "";
                    txamt.Text = "";
                    txamt.Enabled = true;
                    Balance.Attributes.Add("style", "display:none");

                }
            }
            else
            {
                PettycashRefno.Attributes.Add("style", "display:none");
            }

        }
        else
        {
            dvRegular.Visible = true;
            dvRegular1.Visible = true;
            Emp.Attributes.Add("style", "display:none");
            PettycashRefno.Attributes.Add("style", "display:none");
            Balance.Attributes.Add("style", "display:none");
            ddlEmp.SelectedValue = "0";
            txamt.Text = "";
            txamt.Enabled = true;
            lblBalance.Text = "0.00";
            dtTransdate.Text = "";
            uploadDoc.Attributes.Remove("style");
            uploadDoc.Visible = true;

        }

    }

    protected void ddlEmp_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@emp_cd", ddlEmp.SelectedValue.ToString()));
        ScriptManager.RegisterStartupScript(this, this.GetType(), "callJSFunction", "SelectPaySrc();", true);
        bll.vBindingComboToSp(ref ddlpettycashRefno, "sp_tpch_pettycash_getEmpPettycashRefno2", "cashout_cd", "cashout_cd", arr);
        ddlpettycashRefno.Items.Insert(0, new ListItem("Select", "0"));

    }

    protected void ddlpettycashRefno_SelectedIndexChanged(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "callJSFunction", "SelectPaySrc();", true);
        double balance = 0.00;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@pettycash_refno", ddlpettycashRefno.SelectedValue.ToString()));
        bll.vGetEmpAdvBalance2(arr, ref rs);
        while (rs.Read())
        {
            balance = double.Parse(rs["balance"].ToString());
            dtTransdate.Text = Convert.ToDateTime(rs["trns_dt"]).ToString("dd/MM/yyyy");
            rdVat.SelectedValue = (bll.vLookUp("select case when isVat='Yes'then 1 else 0 end isVat from temployee_advanced_cash where doc_no='" + ddlpettycashRefno.SelectedValue.ToString() + "'"));
        }
        if(balance==0.00)
        {
            rdVat.ClearSelection();
        }


        int empExists = int.Parse(bll.vLookUp("select count(PettycashEmp_cd) from tproposal_paid where claimco_sta_id ='N' and schedule_dt = (select dateadd(day,(-1),(SELECT CONVERT(date, getdate())))) and PettycashEmp_cd ='" + ddlEmp.SelectedValue.ToString() + "'"));
        if (empExists > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "callJSFunction", "alert2();", true);
            lblBalance.Text = "0.00";
            ddlPaySrc.SelectedValue = "Regular";
            return;
        }


        if (ddlEmp.SelectedIndex == 0)
        {
            lblBalance.Text = "0.00";
            ddlpettycashRefno.SelectedValue = "0";
        }
        else
        {
            //lblBalance.Text = balance.ToString("0.00");
            lblBalance.Text = balance.ToString("0.00");
            txamt.Text = lblBalance.Text;
            txamt.Enabled = false;
        }
    }


}