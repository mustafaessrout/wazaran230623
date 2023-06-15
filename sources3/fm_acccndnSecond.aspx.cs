using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_acccndnSecond : System.Web.UI.Page
{
    cbll bll = new cbll();
    cdal cdl = new cdal();
    Utitlity ut = new Utitlity();
    decimal totAmount, totCNDNAmount, totBalance, totVat, totAlreadyCNDN, totCNAmount, totDNAmount;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Some branch very slow , so we making this form
            btnAutomatic.Enabled = true;
            dtpost.Text = DateTime.Now.ToString("d/M/yyyy");
            BindControl();
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@refho_no", Convert.ToString(Request.Cookies["usr_id"].Value)));
            bll.vtacc_cndndtl_dtl_dlt(arr);
            ddlOperation_SelectedIndexChanged(sender, e);
        }
    }

    void BindControl()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@reasn_typ", Convert.ToString("cndn")));
        bll.vBindingComboToSp(ref ddlReason, "sp_tmst_reason_get", "reasn_cd", "reasn_nm", arr);

    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        System.Data.SqlClient.SqlDataReader rs = null;
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        List<string> lCust = new List<string>();
        string sCust = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        // arr.Add(new cArrayList("@salesman_cd", contextKey));
        arr.Add(new cArrayList("@cust_cd", prefixText));
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        bll.vSearchMstCustomer(arr, ref rs);
        while (rs.Read())
        {
            sCust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + "-" + rs["cust_nm"].ToString(), rs["cust_cd"].ToString());
            lCust.Add(sCust);
        }
        rs.Close();
        return (lCust.ToArray());
    }
    protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            grd.EditIndex = -1;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespoint", Convert.ToString(Request.Cookies["sp"].Value)));
            if (hdcust.Value == "")
            {
                arr.Add(new cArrayList("@cust_cd", Convert.ToString(txcust.Text)));
            }
            else
            {
                arr.Add(new cArrayList("@cust_cd", Convert.ToString(hdcust.Value)));
            }
            grdDetails.DataSource = null;
            grdDetails.DataBind();
            ClearControl();
            bll.vBindingGridToSp(ref grd, "sp_tdosales_invoiceByStat_get", arr);
        }
        catch (Exception ex)
        {
            ut.Logs("", "Account", "CN DN Adjustment", "fm_acccndn", "grd_RowCancelingEdit", "Exception", ex.Message + ex.InnerException);
        }
    }
    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            grd.EditIndex = e.NewEditIndex;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespoint", Convert.ToString(Request.Cookies["sp"].Value)));
            if (hdcust.Value == "")
            {
                arr.Add(new cArrayList("@cust_cd", Convert.ToString(txcust.Text)));
            }
            else
            {
                arr.Add(new cArrayList("@cust_cd", Convert.ToString(hdcust.Value)));
            }
            grdDetails.DataSource = null;
            grdDetails.DataBind();
            ClearControl();
            bll.vBindingGridToSp(ref grd, "sp_tdosales_invoiceByStat_get", arr);
        }
        catch (Exception ex)
        {
            ut.Logs("", "Account", "CN DN Adjustment", "fm_acccndn", "grd_RowEditing", "Exception", ex.Message + ex.InnerException);
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "editTable", "editTable();", true);
    }
    
    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            string userID = Convert.ToString(Request.Cookies["usr_id"].Value);
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbltotamt = (Label)e.Row.FindControl("lbltotamt");
                Label lblCNDN = (Label)e.Row.FindControl("lblCNDN");
                Label lblBalance = (Label)e.Row.FindControl("lblBalance");
                Label lbltotVat = (Label)e.Row.FindControl("lbltotVat");
                Label lblinv_no = (Label)e.Row.FindControl("lblinv_no");
                Label lblVATCNDN = (Label)e.Row.FindControl("lblVATCNDN");
                HiddenField hdfsalesman_cd = (HiddenField)e.Row.FindControl("hdfsalesman_cd");
                List<cArrayList> arr = new List<cArrayList>();
                if (lblCNDN != null && hdfAutomaticAmount.Value != "")
                {
                    if (ddlOperation.SelectedValue == "Automatic" && Convert.ToDecimal(hdfAutomaticAmount.Value) > 0)
                    {
                        decimal currentAmount = Convert.ToDecimal(hdfAutomaticAmount.Value);
                        decimal totamount = Convert.ToDecimal(lbltotamt.Text);
                        var cndn = string.Empty;
                        if (ddlCNDN.SelectedValue == "CN")
                        {
                            cndn = bll.vLookUp("SELECT sum(inv_CNAmount) FROM tacc_cndndtl WHERE inv_no ='" + lblinv_no.Text + "' and cndnType='CN'");
                        }
                        else if (ddlCNDN.SelectedValue == "DN")
                        {
                            cndn = bll.vLookUp("SELECT sum(inv_DNAmount) FROM tacc_cndndtl WHERE inv_no ='" + lblinv_no.Text + "' and cndnType='DN'");
                        }

                        if (cndn != "")
                        {
                            totamount = totamount - Convert.ToDecimal(cndn);
                        }
                        if (currentAmount >= totamount && currentAmount > 0)
                        {

                            lblCNDN.Text = Convert.ToString(totamount);
                            currentAmount = currentAmount - totamount;
                            hdfAutomaticAmount.Value = Convert.ToString(currentAmount);
                            arr.Clear();

                            arr.Add(new cArrayList("@cndn_cd", userID));
                            arr.Add(new cArrayList("@cndnType", Convert.ToString(ddlCNDN.SelectedValue)));
                            arr.Add(new cArrayList("@refho_no", userID));
                            arr.Add(new cArrayList("@salespointcd", Convert.ToString(Request.Cookies["sp"].Value)));
                            arr.Add(new cArrayList("@inv_no", lblinv_no.Text));
                            arr.Add(new cArrayList("@inv_noAmount", Convert.ToDecimal(lbltotamt.Text)));

                            #region Here we calculate balance
                            decimal balance = 0;
                            decimal dnAmount = 0;
                            decimal cnAmount = 0;
                            decimal invAmount = 0;
                            string strBal = string.Empty;

                            invAmount = Convert.ToDecimal(lbltotamt.Text);
                            strBal = bll.vLookUp("SELECT sum(inv_DNAmount) FROM tacc_cndndtl WHERE inv_no ='" + lblinv_no.Text + "'");
                            if (strBal != "")
                            {
                                dnAmount = Convert.ToDecimal(strBal);
                                strBal = "";
                            }
                            strBal = bll.vLookUp("SELECT sum(inv_CNAmount) FROM tacc_cndndtl WHERE inv_no ='" + lblinv_no.Text + "'");
                            if (strBal != "")
                            {
                                cnAmount = Convert.ToDecimal(strBal);
                                strBal = "";
                            }
                            balance = invAmount - cnAmount + dnAmount;
                            if (ddlCNDN.SelectedValue == "CN")
                            {
                                balance = balance - Convert.ToDecimal(lblCNDN.Text);
                            }
                            else { balance = balance + Convert.ToDecimal(lblCNDN.Text); }

                            #endregion

                            if (ddlCNDN.SelectedValue == "CN")
                            {
                                arr.Add(new cArrayList("@inv_CNAmount", Convert.ToDecimal(lblCNDN.Text)));
                                arr.Add(new cArrayList("@inv_DNAmount", Convert.ToDecimal(0)));
                                
                            }
                            else if (ddlCNDN.SelectedValue == "DN")
                            {
                                arr.Add(new cArrayList("@inv_CNAmount", Convert.ToDecimal(0)));
                                arr.Add(new cArrayList("@inv_DNAmount", Convert.ToDecimal(lblCNDN.Text)));
                                
                            }
                            arr.Add(new cArrayList("@inv_Balance", balance));
                            arr.Add(new cArrayList("@salesman_cd", hdfsalesman_cd.Value));
                            bll.vtacc_cndndtl_dtl_int(arr);
                        }
                        else
                        {
                            lblCNDN.Text = Convert.ToString(currentAmount);
                            currentAmount = Convert.ToDecimal(hdfAutomaticAmount.Value) - totamount;
                            hdfAutomaticAmount.Value = Convert.ToString(currentAmount);
                            //currentAmount = currentAmount - totamount;
                            //hdfAutomaticAmount.Value = Convert.ToString(hdfAutomaticAmount.Value);
                            arr.Clear();
                            arr.Add(new cArrayList("@cndn_cd", userID));
                            arr.Add(new cArrayList("@cndnType", Convert.ToString(ddlCNDN.SelectedValue)));
                            arr.Add(new cArrayList("@refho_no", userID));
                            arr.Add(new cArrayList("@salespointcd", Convert.ToString(Request.Cookies["sp"].Value)));
                            arr.Add(new cArrayList("@inv_no", lblinv_no.Text));
                            arr.Add(new cArrayList("@inv_noAmount", Convert.ToDecimal(lbltotamt.Text)));

                            #region Here we calculate balance
                            decimal balance = 0;
                            decimal dnAmount = 0;
                            decimal cnAmount = 0;
                            decimal invAmount = 0;
                            string strBal = string.Empty;

                            invAmount = Convert.ToDecimal(lbltotamt.Text);
                            strBal = bll.vLookUp("SELECT sum(inv_DNAmount) FROM tacc_cndndtl WHERE inv_no ='" + lblinv_no.Text + "'");
                            if (strBal != "")
                            {
                                dnAmount = Convert.ToDecimal(strBal);
                                strBal = "";
                            }
                            strBal = bll.vLookUp("SELECT sum(inv_CNAmount) FROM tacc_cndndtl WHERE inv_no ='" + lblinv_no.Text + "'");
                            if (strBal != "")
                            {
                                cnAmount = Convert.ToDecimal(strBal);
                                strBal = "";
                            }
                            balance = invAmount - cnAmount + dnAmount;
                            if (ddlCNDN.SelectedValue == "CN")
                            {
                                balance = balance - Convert.ToDecimal(lblCNDN.Text);
                            }
                            else { balance = balance + Convert.ToDecimal(lblCNDN.Text); }


                            #endregion

                            if (ddlCNDN.SelectedValue == "CN")
                            {
                                arr.Add(new cArrayList("@inv_CNAmount", Convert.ToDecimal(lblCNDN.Text)));
                                arr.Add(new cArrayList("@inv_DNAmount", Convert.ToDecimal(0)));
                                
                            }
                            else if (ddlCNDN.SelectedValue == "DN")
                            {
                                arr.Add(new cArrayList("@inv_CNAmount", Convert.ToDecimal(0)));
                                arr.Add(new cArrayList("@inv_DNAmount", Convert.ToDecimal(lblCNDN.Text)));

                                //if (cndn != "")
                                //{

                                //    arr.Add(new cArrayList("@inv_Balance", (totamount + Convert.ToDecimal(currentAmount))));
                                //}
                                //arr.Add(new cArrayList("@inv_Balance", Convert.ToDecimal(totamount + Convert.ToDecimal(lblCNDN.Text))));
                            }

                            arr.Add(new cArrayList("@inv_Balance", balance));
                            arr.Add(new cArrayList("@salesman_cd", hdfsalesman_cd.Value));
                            bll.vtacc_cndndtl_dtl_int(arr);
                        }
                    }
                    else
                    {
                        lblCNDN.Text = "0";
                    }

                }
                if (lblCNDN != null)
                {
                    string cndn = string.Empty;

                    if (ddlCNDN.SelectedValue == "CN")
                    {
                        cndn = bll.vLookUp("SELECT sum(inv_CNAmount) FROM tacc_cndndtl WHERE inv_no ='" + lblinv_no.Text + "' and cndnType='CN'");
                    }
                    else if (ddlCNDN.SelectedValue == "DN")
                    {
                        cndn = bll.vLookUp("SELECT sum(inv_DNAmount) FROM tacc_cndndtl WHERE inv_no ='" + lblinv_no.Text + "' and cndnType='DN'");
                    }


                    if (cndn != "")
                    {
                        lblCNDN.Text = cndn;
                    }
                    else { lblCNDN.Text = "0"; }
                }
                 totCNDNAmount += Convert.ToDecimal(lblCNDN.Text);
                totAmount += Convert.ToDecimal(lbltotamt.Text);
                totVat += Convert.ToDecimal(lbltotVat.Text);
                if (lblCNDN == null)
                {
                   
                }
                else
                {
                    lblBalance.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(lbltotamt.Text) - (Convert.ToDecimal(lblCNDN.Text))));
                }

                totBalance += Convert.ToDecimal(lblBalance.Text);

                //totAlreadyCNDN += Convert.ToDecimal(lblCNDN.Text);
            }
            else if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate))
            {
                Label lbltotamt = (Label)e.Row.FindControl("lbltotamt");
                Label lblCNDN = (Label)e.Row.FindControl("lblCNDN");
                Label lblBalance = (Label)e.Row.FindControl("lblBalance");
                Label lbltotVat = (Label)e.Row.FindControl("lbltotVat");
                Label lblinv_no = (Label)e.Row.FindControl("lblinv_no");
                if (lblCNDN != null && hdfAutomaticAmount.Value != "")
                {
                    if (ddlOperation.SelectedValue == "Automatic" && Convert.ToDecimal(hdfAutomaticAmount.Value) > 0)
                    {
                        decimal currentAmount = Convert.ToDecimal(hdfAutomaticAmount.Value);
                        decimal totamount = Convert.ToDecimal(lbltotamt.Text);

                        if (currentAmount >= totamount && currentAmount > 0)
                        {
                            lblCNDN.Text = Convert.ToString(totamount);
                            currentAmount = currentAmount - totamount;
                            hdfAutomaticAmount.Value = Convert.ToString(currentAmount);
                        }
                        else
                        {
                            lblCNDN.Text = Convert.ToString(currentAmount);
                            //currentAmount = currentAmount - totamount;
                            hdfAutomaticAmount.Value = Convert.ToString(0);
                        }
                    }
                    else
                    {
                        lblCNDN.Text = "0";
                    }
                }
                if (lblCNDN != null)
                {
                    var cndn = string.Empty;
                    if (ddlCNDN.SelectedValue == "CN")
                    {
                        cndn = bll.vLookUp("SELECT sum(inv_CNAmount) FROM tacc_cndndtl WHERE inv_no ='" + lblinv_no.Text + "' and refho_no !='" + userID + "'");
                    }
                    else if (ddlCNDN.SelectedValue == "DN")
                    {
                        cndn = bll.vLookUp("SELECT sum(inv_DNAmount) FROM tacc_cndndtl WHERE inv_no ='" + lblinv_no.Text + "' and refho_no !='" + userID + "'");
                    }


                    if (cndn != "")
                    {
                        lblCNDN.Text = cndn;
                    }
                    else { lblCNDN.Text = "0"; }
                }
                totCNDNAmount += Convert.ToDecimal(lblCNDN.Text); 
                totAmount += Convert.ToDecimal(lbltotamt.Text);
                totVat += Convert.ToDecimal(lbltotVat.Text);

                lblBalance.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(lbltotamt.Text) - Convert.ToDecimal(lblCNDN.Text))); 
                totBalance += Convert.ToDecimal(lblBalance.Text);
            }
            else if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
            {
                Label lbltotamt = (Label)e.Row.FindControl("lbltotamt");
                Label lblCNDN = (Label)e.Row.FindControl("lblCNDN");
                Label lblBalance = (Label)e.Row.FindControl("lblBalance");
                Label lbltotVat = (Label)e.Row.FindControl("lbltotVat");
                Label lblinv_no = (Label)e.Row.FindControl("lblinv_no");
                if (lblCNDN != null && hdfAutomaticAmount.Value != "")
                {
                    if (ddlOperation.SelectedValue == "Automatic" && Convert.ToDecimal(hdfAutomaticAmount.Value) > 0)
                    {
                        decimal currentAmount = Convert.ToDecimal(hdfAutomaticAmount.Value);
                        decimal totamount = Convert.ToDecimal(lbltotamt.Text);

                        if (currentAmount >= totamount && currentAmount > 0)
                        {
                            lblCNDN.Text = Convert.ToString(totamount);
                            currentAmount = currentAmount - totamount;
                            hdfAutomaticAmount.Value = Convert.ToString(currentAmount);
                        }
                        else
                        {
                            lblCNDN.Text = Convert.ToString(currentAmount);
                            //currentAmount = currentAmount - totamount;
                            hdfAutomaticAmount.Value = Convert.ToString(0);
                        }

                    }
                    else
                    {
                        lblCNDN.Text = "0";
                    }
                }
                if (lblCNDN != null)
                {
                    var cndn = string.Empty;
                    if (ddlCNDN.SelectedValue == "CN")
                    {
                        cndn = bll.vLookUp("SELECT sum(inv_CNAmount) FROM tacc_cndndtl WHERE inv_no ='" + lblinv_no.Text + "' and refho_no !='" + userID + "'");
                    }
                    else if (ddlCNDN.SelectedValue == "DN")
                    {
                        cndn = bll.vLookUp("SELECT sum(inv_DNAmount) FROM tacc_cndndtl WHERE inv_no ='" + lblinv_no.Text + "' and refho_no !='" + userID + "'");
                    }

                    if (cndn != "")
                    {
                        lblCNDN.Text = cndn;
                    }
                    else { lblCNDN.Text = "0"; }
                }
                
                else { totCNDNAmount += Convert.ToDecimal(lblCNDN.Text); }
                totAmount += Convert.ToDecimal(lbltotamt.Text);
                totVat += Convert.ToDecimal(lbltotVat.Text);

                if (lblCNDN == null)
                {
                    lblBalance.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(lbltotamt.Text) - Convert.ToDecimal(lblCNDN.Text)));
                }
                else { lblBalance.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(lbltotamt.Text) - Convert.ToDecimal(lblCNDN.Text))); }
                totBalance += Convert.ToDecimal(lblBalance.Text);
                //totAlreadyCNDN += Convert.ToDecimal(lblCNDN.Text);
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbVat = (Label)e.Row.FindControl("lbVat");
                Label lbtotAmount = (Label)e.Row.FindControl("lbtotAmount");
                Label lbCNDNAmount = (Label)e.Row.FindControl("lbCNDNAmount");
                Label lbtotBalance = (Label)e.Row.FindControl("lbtotBalance");
                Label lblTotVATCNDN = (Label)e.Row.FindControl("lblTotVATCNDN");

                lbtotAmount.Text = String.Format("{0:0.00}", totAmount);
                lbCNDNAmount.Text = String.Format("{0:0.00}", totCNDNAmount);
                lbtotBalance.Text = String.Format("{0:0.00}", totBalance);

                lbVat.Text = String.Format("{0:0.00}", totVat);

                if (ddlCNDNType.SelectedValue == "VAT")
                {
                    if (ddlCNDN.SelectedValue == "CN")
                    {
                        totCNDNAmount = Convert.ToDecimal(bll.vLookUp("SELECT sum(inv_CNAmount) FROM tacc_cndndtl WHERE inv_no in (select inv_no  from tdosales_invoice where cust_cd='" + hdcust.Value + "' and  inv_sta_id in ('P','R') and salespointcd ='" + Convert.ToString(Request.Cookies["sp"].Value) + "')  and refho_no ='" + userID + "'"));
                    }
                    else if (ddlCNDN.SelectedValue == "DN")
                    {
                        totCNDNAmount = Convert.ToDecimal(bll.vLookUp("SELECT sum(inv_DNAmount) FROM tacc_cndndtl WHERE inv_no in (select inv_no  from tdosales_invoice where cust_cd='" + hdcust.Value + "' and  inv_sta_id in ('P','R') and salespointcd ='" + Convert.ToString(Request.Cookies["sp"].Value) + "')  and refho_no ='" + userID + "'"));
                    }

                    lblVat.Text = String.Format("{0:0.00}", (totCNDNAmount - ((totCNDNAmount * 100) / 105)));
                }
            }
        }
        catch (Exception ex)
        {
            ut.Logs("", "Account", "CN DN Adjustment", "fm_acccndn", "grd_RowUpdating", "Exception", ex.Message + ex.InnerException);
        }
    }
    protected void btShowInvoice_Click(object sender, EventArgs e)
    {
        try
        {
            ddlOperation.SelectedValue = "Manual";
            ddlOperation_SelectedIndexChanged(sender, e);
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespoint", Convert.ToString(Request.Cookies["sp"].Value)));
            if (hdcust.Value == "") {
                arr.Add(new cArrayList("@cust_cd", Convert.ToString(txcust.Text)));
            }
            else {
                arr.Add(new cArrayList("@cust_cd", Convert.ToString(hdcust.Value)));
            }
            grdDetails.DataSource = null;
            grdDetails.DataBind();
            ClearControl();
            bll.vBindingGridToSp(ref grd, "sp_tdosales_invoiceByStat_get", arr);

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Invoice load failed','Invoice Load','warning');", true);
            ut.Logs("", "CNDN Adjustment", "CNDN Adjustment", "fm_acccndn", "bind grid", "Exception", ex.Message + ex.InnerException);
        }
    }


    protected void btSave_Click(object sender, EventArgs e)
    {
        try
        {
            decimal vatCNDN = 0;
            string cndn_cd = string.Empty;
            if (Convert.ToString(txtRef.Text) == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Ref required','Ref required','warning');", true);
                return;
            }
            else if (Convert.ToString(dtpost.Text) == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Post date required','Post date required','warning');", true);
                return;
            }
            else if (Convert.ToString(dtCNDNDate.Text) == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('CN DN date required','CN DN date required','warning');", true);
                return;
            }
            else if (ddlCNDNType.SelectedValue == "VAT" && Convert.ToDecimal(lblVat.Text) <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Vat is in correct','Vat is in correct','warning');", true);
                return;
            }
            else if (ddlCNDNType.SelectedValue == "VAT" && DateTime.ParseExact(dtCNDNDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture) <= Convert.ToDateTime("31-Dec-2017 23:59:59"))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Can not select vat on this CNDN date','CN DN wrong','warning');", true);
                return;
            }
            bool vatincluded = false;


            vatincluded = ddlCNDNType.SelectedValue == "NonVAT" ? false : true;
            vatCNDN = vatincluded == true ? Convert.ToDecimal(lblVat.Text) : 0;

            //@salespointcd varchar(50),
            //@post_dt datetime,
            //@cndn_dt date,
            //@createdby varchar(50),
            //@cndn_cd varchar(50) out

            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@reasn_cd", Convert.ToString(ddlReason.SelectedValue)));
            arr.Add(new cArrayList("@cndnType", Convert.ToString(ddlCNDN.SelectedValue)));

            if (ddlCNDN.SelectedValue == "CN")
            {
                arr.Add(new cArrayList("@totamtCN", totCNDNAmount));
                arr.Add(new cArrayList("@totamtDN", 0));
            }
            else if (ddlCNDN.SelectedValue == "DN")
            {
                arr.Add(new cArrayList("@totamtCN", 0));
                arr.Add(new cArrayList("@totamtDN", totCNDNAmount));
            }
            arr.Add(new cArrayList("@cust_cd", hdcust.Value));
            arr.Add(new cArrayList("@remark", Convert.ToString(txtRemarks.Text)));
            arr.Add(new cArrayList("@vatamt", vatCNDN));
            arr.Add(new cArrayList("@vatincluded", vatincluded));
            arr.Add(new cArrayList("@refho_no", Convert.ToString(txtRef.Text)));
            arr.Add(new cArrayList("@salespointcd", Convert.ToString(Request.Cookies["sp"].Value)));
            arr.Add(new cArrayList("@post_dt", DateTime.ParseExact(dtpost.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@cndn_dt", DateTime.ParseExact(dtCNDNDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@createdby", Convert.ToString(Request.Cookies["usr_id"].Value)));

            bll.vInsertACC_cndn(arr, ref cndn_cd);

            if (cndn_cd == "-2")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Ref no already available.','Ref no already available. ','warning');", true);
                return;
            }
            else
            {
                lbsysno.Text = cndn_cd;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Data Save Successfully.','Data Save Successfully.','success');", true);
                lblVat.Text = "0";
                txtAutomaticAmount.Text = "0";
                hdcust.Value = "";
                hdfAutomaticAmount.Value = "0";
                totCNDNAmount = 0;
                txtRemarks.Text = "";
                vatCNDN = 0;
                vatincluded = false;
                txcust.Text = "";
                txtRef.Text = "";
                dtCNDNDate.Text = "";
                ddlCNDN.SelectedValue = "CN";
                btnAutomatic.Enabled = true;
                ddlCNDNType.SelectedValue = "NonVAT";
                arr.Clear();
                arr.Add(new cArrayList("@salespoint", Convert.ToString(Request.Cookies["sp"].Value)));
                if (hdcust.Value == "")
                {
                    arr.Add(new cArrayList("@cust_cd", Convert.ToString(txcust.Text)));
                }
                else
                {
                    arr.Add(new cArrayList("@cust_cd", Convert.ToString(hdcust.Value)));
                }
                grdDetails.DataSource = null;
                grdDetails.DataBind();
                ClearControl();
                bll.vBindingGridToSp(ref grd, "sp_tdosales_invoiceByStat_get", arr);
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Error during save','Error during save','warning');", true);

            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('" + ex.Message.ToString() + ex.InnerException.ToString() + "','" + ex.Message.ToString() + ex.InnerException.ToString() + "','warning');", true);
            ut.Logs("", "Account", "CN DN Adjustment", "fm_acccndn", "grd_RowUpdating", "Exception", ex.Message + ex.InnerException);
        }
    }

    protected void btAutomatic_Click(object sender, EventArgs e)
    {
        decimal dAutomaticAmount = 0;
        string lbtotBalance = (grd.FooterRow.FindControl("lbtotBalance") as Label).Text;
        hdfAutomaticAmount.Value = Convert.ToString(txtAutomaticAmount.Text);
        if (!decimal.TryParse(txtAutomaticAmount.Text, out dAutomaticAmount))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please amount on numeric only','Amount not inserted properly','warning');", true);
            return;
        }
        else if (Convert.ToDecimal(lbtotBalance) < Convert.ToDecimal(hdfAutomaticAmount.Value))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Assign amount greater than balance','Assign amount greater than balance','warning');", true);
            return;
        }


        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespoint", Convert.ToString(Request.Cookies["sp"].Value)));
        if (hdcust.Value == "")
        {
            arr.Add(new cArrayList("@cust_cd", Convert.ToString(txcust.Text)));
        }
        else
        {
            arr.Add(new cArrayList("@cust_cd", Convert.ToString(hdcust.Value)));
        }
        grdDetails.DataSource = null;
        grdDetails.DataBind();
        ClearControl();
        bll.vBindingGridToSp(ref grd, "sp_tdosales_invoiceByStat_get", arr);
        btnAutomatic.Enabled = false;

    }
    protected void btPrint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=acccndn');", true);
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_acccndn.aspx");
    }
    protected void ddlOperation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOperation.SelectedValue == "Manual")
        {
            dvAutomactic.Visible = false;
            dvbtnAutomatic.Visible = false;
            txtAutomaticAmount.Text = "0";
            hdfAutomaticAmount.Value = "0";
            //dvCNDNVat.Visible = false;
        }
        else
        {
            dvAutomactic.Visible = true; dvbtnAutomatic.Visible = true;
            //dvCNDNVat.Visible = true; 
        }
    }
    protected void ddlCNDNType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCNDNType.SelectedValue == "VAT" && ddlOperation.SelectedValue == "Automatic" && Convert.ToDecimal(txtAutomaticAmount.Text) > 0)
        {
            //dvCNDNVatCal.Visible = true;
            //B9-((B9*100)/105)

            decimal vatAmount = 0;
            decimal dCNDNAmount = Convert.ToDecimal(txtAutomaticAmount.Text);
            vatAmount = dCNDNAmount - ((dCNDNAmount * 100) / 105);
            lblVat.Text = Convert.ToString(vatAmount);
        }
        if (ddlCNDNType.SelectedValue == "VAT")
        {
            string lbCNDNAmount = (grd.FooterRow.FindControl("lbCNDNAmount") as Label).Text;
            if (lbCNDNAmount != "")
            {
                decimal vatAmount = 0;
                decimal dCNDNAmount = Convert.ToDecimal(lbCNDNAmount);
                vatAmount = dCNDNAmount - ((dCNDNAmount * 100) / 105);
                lblVat.Text = Convert.ToString(vatAmount);
            }
        }
        else
        {
            lblVat.Text = Convert.ToString(0);
            //dvCNDNVatCal.Visible = false;
        }
    }
    protected void ddlCNDN_SelectedIndexChanged(object sender, EventArgs e)
    {
        //List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@salespoint", Convert.ToString(Request.Cookies["sp"].Value)));
        //arr.Add(new cArrayList("@cust_cd", Convert.ToString(hdcust.Value)));
        //bll.vBindingGridToSp(ref grd, "sp_tdosales_invoiceByStat_get", arr);
    }
    protected void grdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string userID = Convert.ToString(Request.Cookies["usr_id"].Value);
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblCN = (Label)e.Row.FindControl("lblCN");
            Label lblDN = (Label)e.Row.FindControl("lblDN");
            totCNAmount += Convert.ToDecimal(lblCN.Text);
            totDNAmount += Convert.ToDecimal(lblDN.Text);
        }
        else if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate))
        {
            Label lblCN = (Label)e.Row.FindControl("lblCN");
            Label lblDN = (Label)e.Row.FindControl("lblDN");
            totCNAmount += Convert.ToDecimal(lblCN.Text);
            totDNAmount += Convert.ToDecimal(lblDN.Text);
        }
        else if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
        {
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbtotDtlCNAmount = (Label)e.Row.FindControl("lbtotDtlCNAmount");
            Label lbtotDtlDNAmount = (Label)e.Row.FindControl("lbtotDtlDNAmount");
            lbtotDtlCNAmount.Text = String.Format("{0:0.00}", totCNAmount);
            lbtotDtlDNAmount.Text = String.Format("{0:0.00}", totDNAmount);
        }
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        
        Label lblinv_no = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lblinv_no");
        if (lblinv_no.Text != "") {
            // Bind Control
            
            Label lblinv_dt = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lblinv_dt");
            Label lblmanual_no = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lblmanual_no");
            Label lblsalesMan = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lblsalesMan");
            Label lblstatusVal = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lblstatusVal");
            Label lbltotamt = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbltotamt");
            Label lbltotVat = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbltotVat");
            Label lblCNDN = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lblCNDN");
            Label lblBalanceGrid = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lblBalance");
            HiddenField hdfsalesman_cd = (HiddenField)grd.Rows[e.NewSelectedIndex].FindControl("hdfsalesman_cd");

            lblInvNo.Text = lblinv_no.Text;
            lblInvDt.Text = lblinv_dt.Text;
            lblManualNumber.Text = lblmanual_no.Text;
            lblSalesMan.Text = lblsalesMan.Text;
            lblStatus.Text = lblstatusVal.Text;
            lblAmount.Text = lbltotamt.Text;
            lblCNDNAmount.Text = "0";
            lblBalance.Text = lblBalanceGrid.Text;
            lblVATInInv.Text = lbltotVat.Text;

            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespoint", Convert.ToString(Request.Cookies["sp"].Value)));
            if (hdcust.Value == "")
            {
                arr.Add(new cArrayList("@cust_cd", Convert.ToString(txcust.Text)));
            }
            else
            {
                arr.Add(new cArrayList("@cust_cd", Convert.ToString(hdcust.Value)));
            }
            arr.Add(new cArrayList("@inv_no", Convert.ToString(lblinv_no.Text)));
            bll.vBindingGridToSp(ref grdDetails, "sp_tdosales_invoiceDtl_get", arr);


        }
        else
        {
            grdDetails.DataSource = null;
            grdDetails.DataBind();
        }
    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        try
        {
            Label lblinv_no = lblInvNo;
            Label lbltotamt = lblAmount;
            TextBox txtCNDN = lblCNDNAmount;
            string userID = Convert.ToString(Request.Cookies["usr_id"].Value);
            List<cArrayList> arr = new List<cArrayList>();
            var alreadyPaid = string.Empty;

            if (ddlCNDN.SelectedValue == "CN")
            {
                alreadyPaid = bll.vLookUp("SELECT sum(inv_CNAmount) FROM tacc_cndndtl WHERE inv_no ='" + lblinv_no.Text + "' and refho_no !='" + userID + "'");
            }
            else if (ddlCNDN.SelectedValue == "DN")
            {
                alreadyPaid = bll.vLookUp("SELECT sum(inv_DNAmount) FROM tacc_cndndtl WHERE inv_no ='" + lblinv_no.Text + "' and refho_no !='" + userID + "'");
            }
            decimal dAlreadyPaid = 0;
            if (alreadyPaid != "")
            {
                dAlreadyPaid = Convert.ToDecimal(alreadyPaid);
            }

            decimal balnace = 0;
            balnace = Convert.ToDecimal(lbltotamt.Text) - (Convert.ToDecimal(dAlreadyPaid) + Convert.ToDecimal(txtCNDN.Text));
            if (balnace < 0)
            {
                arr.Clear();
                arr.Add(new cArrayList("@salespoint", Convert.ToString(Request.Cookies["sp"].Value)));
                if (hdcust.Value == "")
                {
                    arr.Add(new cArrayList("@cust_cd", Convert.ToString(txcust.Text)));
                }
                else
                {
                    arr.Add(new cArrayList("@cust_cd", Convert.ToString(hdcust.Value)));
                }
                grdDetails.DataSource = null;
                grdDetails.DataBind();
                bll.vBindingGridToSp(ref grd, "sp_tdosales_invoiceByStat_get", arr);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('CN DN Amount greater than invoice amount.','CN DN Amount','warning');", true);
                return;
            }

            #region Here we calculate balance
            decimal balance = 0;
            decimal dnAmount = 0;
            decimal cnAmount = 0;
            decimal invAmount = 0;
            string strBal = string.Empty;

            invAmount = Convert.ToDecimal(lbltotamt.Text);
            strBal = bll.vLookUp("SELECT sum(inv_DNAmount) FROM tacc_cndndtl WHERE inv_no ='" + lblinv_no.Text + "'");
            if (strBal != "" && strBal != "0.00000")
            {
                dnAmount = Convert.ToDecimal(strBal);
                strBal = "";
            }
            strBal = bll.vLookUp("SELECT sum(inv_CNAmount) FROM tacc_cndndtl WHERE inv_no ='" + lblinv_no.Text + "'");
            if (strBal != "" && strBal != "0.00000")
            {
                cnAmount = Convert.ToDecimal(strBal);
                strBal = "";
            }
            balance = invAmount - cnAmount + dnAmount;

            if (ddlCNDN.SelectedValue == "CN")
            {
                balance = balance - Convert.ToDecimal(txtCNDN.Text);
            }
            else { balance = balance + Convert.ToDecimal(txtCNDN.Text); }

            #endregion

            arr.Add(new cArrayList("@cndn_cd", userID));
            arr.Add(new cArrayList("@cndnType", Convert.ToString(ddlCNDN.SelectedValue)));
            arr.Add(new cArrayList("@refho_no", userID));
            arr.Add(new cArrayList("@salespointcd", Convert.ToString(Request.Cookies["sp"].Value)));
            arr.Add(new cArrayList("@inv_no", lblinv_no.Text));
            arr.Add(new cArrayList("@inv_noAmount", Convert.ToDecimal(lbltotamt.Text)));



            if (ddlCNDN.SelectedValue == "CN")
            {
                arr.Add(new cArrayList("@inv_CNAmount", Convert.ToDecimal(txtCNDN.Text)));
                arr.Add(new cArrayList("@inv_DNAmount", Convert.ToDecimal(0)));

            }
            else if (ddlCNDN.SelectedValue == "DN")
            {
                arr.Add(new cArrayList("@inv_CNAmount", Convert.ToDecimal(0)));
                arr.Add(new cArrayList("@inv_DNAmount", Convert.ToDecimal(txtCNDN.Text)));

            }
            arr.Add(new cArrayList("@salesman_cd", hdfsalesman_cd.Value));
            arr.Add(new cArrayList("@inv_Balance", balance));
            bll.vtacc_cndndtl_dtl_int(arr);
            //ClearControl();

            arr.Clear();
            arr.Add(new cArrayList("@salespoint", Convert.ToString(Request.Cookies["sp"].Value)));
            if (hdcust.Value == "")
            {
                arr.Add(new cArrayList("@cust_cd", Convert.ToString(txcust.Text)));
            }
            else
            {
                arr.Add(new cArrayList("@cust_cd", Convert.ToString(hdcust.Value)));
            }
            grd.EditIndex = -1;
            grdDetails.DataSource = null;
            grdDetails.DataBind();
            ClearControl();
            bll.vBindingGridToSp(ref grd, "sp_tdosales_invoiceByStat_get", arr);
        }
        catch (Exception ex)
        {
            ut.Logs("", "Account", "CN DN Adjustment", "fm_acccndn", "grd_RowUpdating", "Exception", ex.Message + ex.InnerException);
        }
    }

    void ClearControl() {
        lblInvNo.Text = string.Empty;
        lblInvDt.Text = string.Empty;
        lblManualNumber.Text = string.Empty;
        lblSalesMan.Text = string.Empty;
        lblStatus.Text = string.Empty;
        lblAmount.Text = string.Empty;
        lblCNDNAmount.Text = string.Empty;
        lblBalance.Text = string.Empty;
        lblVATInInv.Text = string.Empty;
        hdfsalesman_cd.Value = string.Empty;
    }
}