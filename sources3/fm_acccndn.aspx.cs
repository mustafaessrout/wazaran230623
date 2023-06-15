using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_acccndn : System.Web.UI.Page
{
    cbll bll = new cbll();
    cdal cdl = new cdal();
    Utitlity ut = new Utitlity();
    decimal totAmount, totCNAmount, totDNAmount, totBalance, totVat, totAlreadyCNDN, totCNDetailsAmount, totDNDetailsAmount, totCurrentCNDN;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            btsearch.Enabled = true;
            btShowInvoice.Enabled = true;
            btnAutomatic.Enabled = true;
            btnew.Enabled = true;
            btSave.Enabled = true;

            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@doc_typ", "CNDNAdj"));
            arr.Add(new cArrayList("@level_no", "1"));
            bll.vBindingComboToSpWithEmptyChoosen(ref cbapproval, "sp_tapprovalpattern_get", "emp_cd", "emp_nm", arr);

            btnAutomatic.Enabled = true;
            dtpost.Text = DateTime.Now.ToString("d/M/yyyy");
            BindControl();
            arr.Clear();
            arr.Add(new cArrayList("@refho_no", Convert.ToString(Request.Cookies["usr_id"].Value)));
            arr.Add(new cArrayList("@salespointcd", Convert.ToString(Request.Cookies["sp"].Value)));
            bll.vtacc_cndndtl_dtl_dlt(arr);
            ddlOperation_SelectedIndexChanged(sender, e);
            ddlHOFormType_SelectedIndexChanged(sender, e);

            // Support Multiple Tax
            showTax.Attributes.Add("style", "display:none");
            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@cndn_cd", Request.Cookies["usr_id"].Value.ToString()));
            bll.vDeleteACCcndn_tax(arr);
            dtCNDNDate.Text = Request.Cookies["waz_dt"].Value;
        }
    }

    void BindControl()
    {
        //List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@reasn_typ", Convert.ToString("cndn")));
        //bll.vBindingComboToSp(ref ddlReason, "sp_tmst_reason_get", "reasn_cd", "reasn_nm", arr);
        List<cArrayList> arr = new List<cArrayList>();
        arr.Clear();
        arr.Add(new cArrayList("@reasn_typ", Convert.ToString("cndnOnTheAccount")));
        bll.vBindingComboToSpWithEmptyChoosen(ref ddlOnAccount, "sp_tmst_reason_get", "reasn_cd", "reasn_nm", arr);

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
            bll.vBindingGridToSp(ref grd, "sp_tdosales_invoiceByStat_get", arr);
        }
        catch (Exception ex)
        {
            ut.Logs("", "Account", "CN DN Adjustment", "fm_acccndn", "grd_RowEditing", "Exception", ex.Message + ex.InnerException);
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "editTable", "editTable();", true);
    }
    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            Label lblinv_no = (Label)grd.Rows[e.RowIndex].FindControl("lblinv_no");
            Label lbltotamt = (Label)grd.Rows[e.RowIndex].FindControl("lbltotamt");
            //Label lblAlreadyCNDN = (Label)grd.Rows[e.RowIndex].FindControl("lblAlreadyCNDN");
            TextBox txtCNDN = (TextBox)grd.Rows[e.RowIndex].FindControl("txtCNDN");
            //Label lblBalance = (Label)grd.Rows[e.RowIndex].FindControl("lblBalance");
            HiddenField hdfsalesman_cd = (HiddenField)grd.Rows[e.RowIndex].FindControl("hdfsalesman_cd");
            string userID = Convert.ToString(Request.Cookies["usr_id"].Value);
            List<cArrayList> arr = new List<cArrayList>();
            var alreadyPaid = string.Empty;

            if (txtCNDN.Text != "")
            {
                if (Convert.ToDecimal(txtCNDN.Text) <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('CN DN Amount can not less and equal zero.','Wrong CN DN','warning');", true);
                    return;
                }
            }

            else if (ddlCNDN.SelectedValue == "CN")
            {
                alreadyPaid = bll.vLookUp("SELECT isnull(sum(isnull(inv_DNAmount,0)),0) FROM tacc_cndndtl WHERE inv_no ='" + lblinv_no.Text + "' and refho_no !='" + userID + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
            }
            else if (ddlCNDN.SelectedValue == "DN")
            {
                alreadyPaid = bll.vLookUp("SELECT isnull(sum(isnull(inv_DNAmount,0)),0) FROM tacc_cndndtl WHERE inv_no ='" + lblinv_no.Text + "' and refho_no !='" + userID + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
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

            //arr.Add(new cArrayList("@refho_no", userID));
            //arr.Add(new cArrayList("@inv_no", lblinv_no.Text));
            //arr.Add(new cArrayList("@inv_noAmount", Convert.ToDecimal(lbltotamt.Text)));
            //arr.Add(new cArrayList("@inv_CNDNAmount", Convert.ToDecimal(txtCNDN.Text)));
            //arr.Add(new cArrayList("@inv_Balance", Convert.ToDecimal(balnace)));
            //bll.vtacc_cndndtl_dtl_int(arr);

            #region Here we calculate balance
            decimal balance = 0;
            decimal dnAmount = 0;
            decimal cnAmount = 0;
            decimal invAmount = 0;
            string strBal = string.Empty;

            invAmount = Convert.ToDecimal(lbltotamt.Text);
            strBal = bll.vLookUp("SELECT sum(inv_DNAmount) FROM tacc_cndndtl WHERE inv_no ='" + lblinv_no.Text + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
            if (strBal != "" && strBal != "0.00000")
            {
                dnAmount = Convert.ToDecimal(strBal);
                strBal = "";
            }
            strBal = bll.vLookUp("SELECT sum(inv_CNAmount) FROM tacc_cndndtl WHERE inv_no ='" + lblinv_no.Text + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
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

            if (txtCNDN.Text != "")
            {
                if (Convert.ToDecimal(txtCNDN.Text) <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('CN DN Amount can not less and equal zero.','Wrong CN DN','warning');", true);
                }
                else
                {
                    bll.vtacc_cndndtl_dtl_int(arr);
                }
            }


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
            bll.vBindingGridToSp(ref grd, "sp_tdosales_invoiceByStat_get", arr);
            ddlCNDNType_SelectedIndexChanged(sender, e);
        }
        catch (Exception ex)
        {
            ut.Logs("", "Account", "CN DN Adjustment", "fm_acccndn", "grd_RowUpdating", "Exception", ex.Message + ex.InnerException);
        }
    }
    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            string userID = Convert.ToString(Request.Cookies["usr_id"].Value);
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbltotamt = (Label)e.Row.FindControl("lbltotamt");
                Label lblAlreadyCN = (Label)e.Row.FindControl("lblAlreadyCN");
                Label lblAlreadyDN = (Label)e.Row.FindControl("lblAlreadyDN");
                //Label lblBalance = (Label)e.Row.FindControl("lblBalance");
                Label lbltotVat = (Label)e.Row.FindControl("lbltotVat");
                Label lblCNDNCurrent = (Label)e.Row.FindControl("lblCNDNCurrent");
                TextBox txtCNDN = (TextBox)e.Row.FindControl("txtCNDN");
                Label lblinv_no = (Label)e.Row.FindControl("lblinv_no");
                Label lblVATCNDN = (Label)e.Row.FindControl("lblVATCNDN");
                HiddenField hdfsalesman_cd = (HiddenField)e.Row.FindControl("hdfsalesman_cd");
                List<cArrayList> arr = new List<cArrayList>();
                if ((lblAlreadyCN != null || lblAlreadyDN != null) && hdfAutomaticAmount.Value != "")
                {
                    if (ddlOperation.SelectedValue == "Automatic" && Convert.ToDecimal(hdfAutomaticAmount.Value) > 0)
                    {
                        decimal currentAmount = Convert.ToDecimal(hdfAutomaticAmount.Value);
                        decimal totamount = Convert.ToDecimal(lbltotamt.Text);
                        var cndn = string.Empty;
                        if (ddlCNDN.SelectedValue == "CN")
                        {
                            cndn = bll.vLookUp("SELECT sum(inv_CNAmount) FROM tacc_cndndtl WHERE inv_no ='" + lblinv_no.Text + "' and cndnType='CN' and refho_no !='" + userID + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
                        }
                        else if (ddlCNDN.SelectedValue == "DN")
                        {
                            cndn = bll.vLookUp("SELECT sum(inv_DNAmount) FROM tacc_cndndtl WHERE inv_no ='" + lblinv_no.Text + "' and cndnType='DN' and refho_no !='" + userID + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
                        }

                        //if (cndn != "")
                        //{
                        //    totamount = totamount - Convert.ToDecimal(cndn);
                        //}
                        if (currentAmount >= totamount && currentAmount > 0)
                        {
                            if (ddlCNDN.SelectedValue == "CN")
                            {
                                lblAlreadyCN.Text = Convert.ToString(totamount);
                                lblAlreadyDN.Text = Convert.ToString(0);
                            }
                            else if (ddlCNDN.SelectedValue == "DN")
                            {
                                lblAlreadyCN.Text = Convert.ToString(0);
                                lblAlreadyDN.Text = Convert.ToString(totamount);
                            }
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
                            strBal = bll.vLookUp("SELECT sum(inv_DNAmount) FROM tacc_cndndtl WHERE inv_no ='" + lblinv_no.Text + "'  and refho_no !='" + userID + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
                            if (strBal != "")
                            {
                                dnAmount = Convert.ToDecimal(strBal);
                                strBal = "";
                            }
                            strBal = bll.vLookUp("SELECT sum(inv_CNAmount) FROM tacc_cndndtl WHERE inv_no ='" + lblinv_no.Text + "' and refho_no !='" + userID + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
                            if (strBal != "")
                            {
                                cnAmount = Convert.ToDecimal(strBal);
                                strBal = "";
                            }
                            //balance = invAmount - cnAmount + dnAmount;
                            if (ddlCNDN.SelectedValue == "CN")
                            {
                                balance = balance - Convert.ToDecimal(lblAlreadyCN.Text);
                            }
                            else { balance = balance + Convert.ToDecimal(lblAlreadyDN.Text); }

                            #endregion

                            if (ddlCNDN.SelectedValue == "CN")
                            {
                                arr.Add(new cArrayList("@inv_CNAmount", Convert.ToDecimal(lblAlreadyCN.Text)));
                                arr.Add(new cArrayList("@inv_DNAmount", Convert.ToDecimal(0)));

                            }
                            else if (ddlCNDN.SelectedValue == "DN")
                            {
                                arr.Add(new cArrayList("@inv_CNAmount", Convert.ToDecimal(0)));
                                arr.Add(new cArrayList("@inv_DNAmount", Convert.ToDecimal(lblAlreadyDN.Text)));

                            }
                            arr.Add(new cArrayList("@inv_Balance", balance));
                            arr.Add(new cArrayList("@salesman_cd", hdfsalesman_cd.Value));

                            if (ddlCNDN.SelectedValue == "CN")
                            {
                                if (lblAlreadyCN.Text != "")
                                {
                                    if (Convert.ToDecimal(lblAlreadyCN.Text) <= 0)
                                    {
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('CN  Amount can not less and equal zero.','Wrong CN DN','warning');", true);
                                    }
                                    else
                                    {

                                        bll.vtacc_cndndtl_dtl_int(arr);
                                    }
                                }
                            }
                            else if (ddlCNDN.SelectedValue == "DN")
                            {
                                if (lblAlreadyDN.Text != "")
                                {
                                    if (Convert.ToDecimal(lblAlreadyDN.Text) <= 0)
                                    {
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert(' DN Amount can not less and equal zero.','Wrong CN DN','warning');", true);
                                    }
                                    else
                                    {

                                        bll.vtacc_cndndtl_dtl_int(arr);
                                    }
                                }
                            }


                        }
                        else
                        {
                            if (ddlCNDN.SelectedValue == "CN")
                            {
                                lblAlreadyCN.Text = Convert.ToString(currentAmount);
                            }
                            else if (ddlCNDN.SelectedValue == "DN")
                            {
                                lblAlreadyDN.Text = Convert.ToString(currentAmount);
                            }
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
                            strBal = bll.vLookUp("SELECT sum(inv_DNAmount) FROM tacc_cndndtl WHERE inv_no ='" + lblinv_no.Text + "' and refho_no !='" + userID + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
                            if (strBal != "")
                            {
                                dnAmount = Convert.ToDecimal(strBal);
                                strBal = "";
                            }
                            strBal = bll.vLookUp("SELECT sum(inv_CNAmount) FROM tacc_cndndtl WHERE inv_no ='" + lblinv_no.Text + "' and refho_no !='" + userID + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
                            if (strBal != "")
                            {
                                cnAmount = Convert.ToDecimal(strBal);
                                strBal = "";
                            }
                            balance = invAmount - cnAmount + dnAmount;
                            if (ddlCNDN.SelectedValue == "CN")
                            {
                                balance = balance - Convert.ToDecimal(lblAlreadyCN.Text);
                            }
                            else { balance = balance + Convert.ToDecimal(lblAlreadyDN.Text); }


                            #endregion

                            if (ddlCNDN.SelectedValue == "CN")
                            {
                                arr.Add(new cArrayList("@inv_CNAmount", Convert.ToDecimal(lblAlreadyCN.Text)));
                                arr.Add(new cArrayList("@inv_DNAmount", Convert.ToDecimal(0)));
                                //if (cndn != "")
                                //{
                                //    if (totamount >= Convert.ToDecimal(currentAmount))
                                //    {
                                //        arr.Add(new cArrayList("@inv_Balance", (totamount - Convert.ToDecimal(currentAmount))));
                                //    }
                                //    else
                                //    {
                                //        arr.Add(new cArrayList("@inv_Balance", (Convert.ToDecimal(currentAmount))));
                                //    }
                                //}
                                //else { arr.Add(new cArrayList("@inv_Balance", Convert.ToDecimal(currentAmount))); }

                                ////arr.Add(new cArrayList("@inv_Balance", Convert.ToDecimal(totamount - Convert.ToDecimal(lblAlreadyCNDN.Text))));
                            }
                            else if (ddlCNDN.SelectedValue == "DN")
                            {
                                arr.Add(new cArrayList("@inv_CNAmount", Convert.ToDecimal(0)));
                                arr.Add(new cArrayList("@inv_DNAmount", Convert.ToDecimal(lblAlreadyDN.Text)));

                                //if (cndn != "")
                                //{

                                //    arr.Add(new cArrayList("@inv_Balance", (totamount + Convert.ToDecimal(currentAmount))));
                                //}
                                //arr.Add(new cArrayList("@inv_Balance", Convert.ToDecimal(totamount + Convert.ToDecimal(lblAlreadyCNDN.Text))));
                            }

                            arr.Add(new cArrayList("@inv_Balance", balance));
                            arr.Add(new cArrayList("@salesman_cd", hdfsalesman_cd.Value));

                            if (ddlCNDN.SelectedValue == "CN")
                            {
                                if (lblAlreadyCN.Text != "")
                                {
                                    if (Convert.ToDecimal(lblAlreadyCN.Text) <= 0)
                                    {
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('CN  Amount can not less and equal zero.','Wrong CN DN','warning');", true);
                                    }
                                    else
                                    {
                                        bll.vtacc_cndndtl_dtl_int(arr);
                                    }
                                }
                            }
                            else if (ddlCNDN.SelectedValue == "DN")
                            {
                                if (lblAlreadyDN.Text != "")
                                {
                                    if (Convert.ToDecimal(lblAlreadyDN.Text) <= 0)
                                    {
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert(' DN Amount can not less and equal zero.','Wrong CN DN','warning');", true);
                                    }
                                    else
                                    {
                                        bll.vtacc_cndndtl_dtl_int(arr);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        lblAlreadyCN.Text = "0";
                        lblAlreadyDN.Text = "0";
                    }

                }
                if (lblAlreadyCN != null || lblAlreadyDN != null)
                {
                    string cn = string.Empty;
                    string dn = string.Empty;


                    cn = bll.vLookUp("SELECT sum(inv_CNAmount) FROM tacc_cndndtl WHERE cndn_cd in (select cndn_cd from tacc_cndn where cndnAdj_sta_id='A') and inv_no ='" + lblinv_no.Text + "' and cndnType='CN' and refho_no !='" + userID + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");

                    dn = bll.vLookUp("SELECT sum(inv_DNAmount) FROM tacc_cndndtl WHERE cndn_cd in (select cndn_cd from tacc_cndn where cndnAdj_sta_id='A') and inv_no ='" + lblinv_no.Text + "' and cndnType='DN' and refho_no !='" + userID + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");



                    if (cn != "")
                    {
                        lblAlreadyCN.Text = cn;
                    }
                    else { lblAlreadyCN.Text = "0"; }
                    if (dn != "")
                    {
                        lblAlreadyDN.Text = dn;
                    }
                    else { lblAlreadyDN.Text = "0"; }
                }
                if (txtCNDN != null && hdfAutomaticAmount.Value != "")
                {
                    if (ddlOperation.SelectedValue == "Automatic" && Convert.ToDecimal(hdfAutomaticAmount.Value) > 0)
                    {
                        decimal currentAmount = Convert.ToDecimal(hdfAutomaticAmount.Value);
                        decimal totamount = Convert.ToDecimal(txtCNDN.Text);

                        if (currentAmount >= totamount && currentAmount > 0)
                        {
                            txtCNDN.Text = Convert.ToString(totamount);

                            currentAmount = currentAmount - totamount;

                            hdfAutomaticAmount.Value = Convert.ToString(currentAmount);
                        }
                        else
                        {
                            txtCNDN.Text = Convert.ToString(currentAmount);
                            //currentAmount = currentAmount - totamount;
                            hdfAutomaticAmount.Value = Convert.ToString(0);
                        }

                        var cndn = string.Empty;
                        if (ddlCNDN.SelectedValue == "CN")
                        {
                            cndn = bll.vLookUp("SELECT sum(inv_CNAmount) FROM tacc_cndndtl WHERE inv_no ='" + lblinv_no.Text + "' and cndnType='CN' and cndn_cd !='" + userID + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
                        }
                        else if (ddlCNDN.SelectedValue == "DN")
                        {
                            cndn = bll.vLookUp("SELECT sum(inv_DNAmount) FROM tacc_cndndtl WHERE inv_no ='" + lblinv_no.Text + "' and cndnType='DN' and cndn_cd !='" + userID + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
                        }
                        if (cndn != "")
                        {
                            txtCNDN.Text = cndn;
                        }
                    }
                    else
                    {
                        txtCNDN.Text = "0";
                    }
                }
                if (txtCNDN != null)
                {
                    if (txtCNDN.Text == "")
                    {
                        totCNAmount += 0;
                        totDNAmount += 0;
                    }
                    else
                    {
                        totCNAmount += Convert.ToDecimal(txtCNDN.Text);
                        totDNAmount += Convert.ToDecimal(txtCNDN.Text);
                    }

                }
                else
                {
                    totCNAmount += Convert.ToDecimal(lblAlreadyCN.Text);
                    totDNAmount += Convert.ToDecimal(lblAlreadyDN.Text);
                }
                totAmount += Convert.ToDecimal(lbltotamt.Text);
                totVat += Convert.ToDecimal(lbltotVat.Text);
                if (lblAlreadyCN == null || lblAlreadyDN == null)
                {
                    if (txtCNDN.Text == "") txtCNDN.Text = "0";
                    //lblBalance.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(lbltotamt.Text) - Convert.ToDecimal(txtCNDN.Text)));
                    //lblBalance.Text = Convert.ToString(Convert.ToDecimal(lbltotamt.Text ));
                }
                else
                {
                    //lblBalance.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(lbltotamt.Text) - (Convert.ToDecimal(lblAlreadyCNDN.Text))));
                    //lblBalance.Text = Convert.ToString(Convert.ToDecimal(lbltotamt.Text));
                }

                totBalance += Convert.ToDecimal(lbltotamt.Text);

                //totAlreadyCNDN += Convert.ToDecimal(lblAlreadyCNDN.Text);

                if (lblCNDNCurrent != null)
                {//sdsdsdsdsd
                    string cndn = string.Empty;
                    if (ddlCNDN.SelectedValue == "CN")
                    {
                        cndn = bll.vLookUp("SELECT sum(inv_CNAmount) FROM tacc_cndndtl WHERE inv_no ='" + lblinv_no.Text + "' and cndnType='CN' and cndn_cd ='" + userID + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
                    }
                    else if (ddlCNDN.SelectedValue == "DN")
                    {
                        cndn = bll.vLookUp("SELECT sum(inv_DNAmount) FROM tacc_cndndtl WHERE inv_no ='" + lblinv_no.Text + "' and cndnType='DN' and cndn_cd ='" + userID + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
                    }
                    if (cndn != "")
                    {
                        lblCNDNCurrent.Text = cndn;
                    }
                }


                var cnLast = "0";
                var dnLast = "0";

                cnLast = bll.vLookUp("SELECT sum(inv_CNAmount) FROM tacc_cndndtl WHERE cndn_cd in (select cndn_cd from tacc_cndn where cndnAdj_sta_id='A') and  inv_no ='" + lblinv_no.Text + "' and cndnType='CN' and refho_no !='" + userID + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");

                dnLast = bll.vLookUp("SELECT sum(inv_DNAmount) FROM tacc_cndndtl WHERE cndn_cd in (select cndn_cd from tacc_cndn where cndnAdj_sta_id='A') and  inv_no ='" + lblinv_no.Text + "' and cndnType='DN' and refho_no !='" + userID + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");

                if (cnLast != string.Empty)
                {
                    lblAlreadyCN.Text = Convert.ToString(cnLast);
                }
                else { lblAlreadyCN.Text = Convert.ToString(0); }

                if (dnLast != string.Empty)
                {
                    lblAlreadyDN.Text = Convert.ToString(dnLast);
                }
                else { lblAlreadyDN.Text = Convert.ToString(0); }

                var currentCNDN = "0";
                if (ddlCNDN.SelectedValue == "CN")
                {
                    currentCNDN = bll.vLookUp("SELECT sum(inv_CNAmount) FROM tacc_cndndtl WHERE inv_no ='" + lblinv_no.Text + "' and cndnType='CN' and refho_no ='" + userID + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
                }
                else if (ddlCNDN.SelectedValue == "DN")
                {
                    currentCNDN = bll.vLookUp("SELECT sum(inv_DNAmount) FROM tacc_cndndtl WHERE inv_no ='" + lblinv_no.Text + "' and cndnType='DN' and refho_no ='" + userID + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
                }
                if (currentCNDN != string.Empty)
                {
                    totCurrentCNDN += Convert.ToDecimal(currentCNDN);
                }
            }
            else if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate))
            {
                Label lbltotamt = (Label)e.Row.FindControl("lbltotamt");
                TextBox txtCNDN = (TextBox)e.Row.FindControl("txtCNDN");
                Label lblCNDNCurrent = (Label)e.Row.FindControl("lblCNDNCurrent");
                Label lblAlreadyDN = (Label)e.Row.FindControl("lblAlreadyDN");
                Label lblAlreadyCN = (Label)e.Row.FindControl("lblAlreadyCN");
                //Label lblBalance = (Label)e.Row.FindControl("lblBalance");
                Label lbltotVat = (Label)e.Row.FindControl("lbltotVat");
                Label lblinv_no = (Label)e.Row.FindControl("lblinv_no");
                if ((lblAlreadyCN != null || lblAlreadyDN != null) && hdfAutomaticAmount.Value != "")
                {
                    if (ddlOperation.SelectedValue == "Automatic" && Convert.ToDecimal(hdfAutomaticAmount.Value) > 0)
                    {
                        decimal currentAmount = Convert.ToDecimal(hdfAutomaticAmount.Value);
                        decimal totamount = Convert.ToDecimal(lbltotamt.Text);

                        if (currentAmount >= totamount && currentAmount > 0)
                        {
                            if (ddlCNDN.SelectedValue == "CN")
                            {
                                lblAlreadyCN.Text = Convert.ToString(totamount);
                                lblAlreadyDN.Text = Convert.ToString(0);
                            }
                            else if (ddlCNDN.SelectedValue == "DN")
                            {
                                lblAlreadyCN.Text = Convert.ToString(0);
                                lblAlreadyDN.Text = Convert.ToString(totamount);
                            }

                            currentAmount = currentAmount - totamount;
                            hdfAutomaticAmount.Value = Convert.ToString(currentAmount);
                        }
                        else
                        {
                            if (ddlCNDN.SelectedValue == "CN")
                            {
                                lblAlreadyCN.Text = Convert.ToString(currentAmount);
                                lblAlreadyDN.Text = Convert.ToString(0);
                            }
                            else if (ddlCNDN.SelectedValue == "DN")
                            {
                                lblAlreadyCN.Text = Convert.ToString(0);
                                lblAlreadyDN.Text = Convert.ToString(currentAmount);
                            }
                            //currentAmount = currentAmount - totamount;
                            hdfAutomaticAmount.Value = Convert.ToString(0);
                        }
                        //var cndn = bll.vLookUp("SELECT sum(inv_CNDNAmount) FROM tacc_cndndtl WHERE inv_no ='" + lblinv_no.Text + "' and refho_no !='" + userID + "'");
                        //if (cndn != "")
                        //{
                        //    lblAlreadyCNDN.Text = cndn;
                        //}
                    }
                    else
                    {
                        lblAlreadyCN.Text = "0";
                        lblAlreadyDN.Text = "0";
                    }
                }
                if (lblAlreadyCN != null || lblAlreadyDN != null)
                {
                    var cn = string.Empty;
                    var dn = string.Empty;

                    cn = bll.vLookUp("SELECT sum(inv_CNAmount) FROM tacc_cndndtl WHERE inv_no ='" + lblinv_no.Text + "' and refho_no !='" + userID + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");

                    dn = bll.vLookUp("SELECT sum(inv_DNAmount) FROM tacc_cndndtl WHERE inv_no ='" + lblinv_no.Text + "' and refho_no !='" + userID + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");



                    if (cn != "")
                    {
                        lblAlreadyCN.Text = cn;
                    }
                    else { lblAlreadyCN.Text = "0"; }

                    if (dn != "")
                    {
                        lblAlreadyDN.Text = dn;
                    }
                    else { lblAlreadyDN.Text = "0"; }
                }
                if (txtCNDN != null && hdfAutomaticAmount.Value != "")
                {
                    if (ddlOperation.SelectedValue == "Automatic" && Convert.ToDecimal(hdfAutomaticAmount.Value) > 0)
                    {
                        decimal currentAmount = Convert.ToDecimal(hdfAutomaticAmount.Value);
                        decimal totamount = Convert.ToDecimal(txtCNDN.Text);

                        if (currentAmount >= totamount && currentAmount > 0)
                        {
                            txtCNDN.Text = Convert.ToString(totamount);
                            currentAmount = currentAmount - totamount;
                            hdfAutomaticAmount.Value = Convert.ToString(currentAmount);
                        }
                        else
                        {
                            txtCNDN.Text = Convert.ToString(currentAmount);
                            //currentAmount = currentAmount - totamount;
                            hdfAutomaticAmount.Value = Convert.ToString(0);
                        }
                        //var cndn = bll.vLookUp("SELECT sum(inv_CNDNAmount) FROM tacc_cndndtl WHERE inv_no ='" + lblinv_no.Text + "' and refho_no !='" + userID + "'");
                        //if (cndn != "")
                        //{
                        //    txtCNDN.Text = cndn;
                        //}
                    }
                    else
                    {
                        txtCNDN.Text = "0";
                    }
                }
                if (txtCNDN != null)
                {
                    if (txtCNDN.Text == "")
                    {
                        totCNAmount += 0;
                        totDNAmount += 0;
                    }
                    else
                    {
                        totCNAmount += Convert.ToDecimal(txtCNDN.Text);
                        totDNAmount += Convert.ToDecimal(txtCNDN.Text);
                    }

                }
                else
                {

                    totCNAmount += Convert.ToDecimal(lblAlreadyCN.Text);

                    totDNAmount += Convert.ToDecimal(lblAlreadyDN.Text);

                }
                totAmount += Convert.ToDecimal(lbltotamt.Text);
                totVat += Convert.ToDecimal(lbltotVat.Text);

                if (lblAlreadyCN == null || lblAlreadyDN == null)
                {
                    if (txtCNDN.Text == "") txtCNDN.Text = "0";
                    //lblBalance.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(lbltotamt.Text) - Convert.ToDecimal(lblAlreadyCNDN.Text)));
                    //lblBalance.Text = Convert.ToString(Convert.ToDecimal(lbltotamt.Text ));
                }
                else
                {
                    //lblBalance.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(lbltotamt.Text) - Convert.ToDecimal(lblAlreadyCNDN.Text)));
                    //lblBalance.Text = Convert.ToString(Convert.ToDecimal(lbltotamt.Text)); 
                }
                totBalance += Convert.ToDecimal(lbltotamt.Text);
                //totAlreadyCNDN += Convert.ToDecimal(lblAlreadyCNDN.Text);
                if (lblCNDNCurrent != null)
                {//sdsdsdsdsd
                    string cndn = string.Empty;
                    if (ddlCNDN.SelectedValue == "CN")
                    {
                        cndn = bll.vLookUp("SELECT sum(inv_CNAmount) FROM tacc_cndndtl WHERE inv_no ='" + lblinv_no.Text + "' and cndnType='CN' and cndn_cd ='" + userID + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
                    }
                    else if (ddlCNDN.SelectedValue == "DN")
                    {
                        cndn = bll.vLookUp("SELECT sum(inv_DNAmount) FROM tacc_cndndtl WHERE inv_no ='" + lblinv_no.Text + "' and cndnType='DN' and cndn_cd ='" + userID + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
                    }
                    if (cndn != "")
                    {
                        lblCNDNCurrent.Text = cndn;
                    }
                }
                var cnLast = "0";
                var dnLast = "0";

                cnLast = bll.vLookUp("SELECT sum(inv_CNAmount) FROM tacc_cndndtl WHERE inv_no ='" + lblinv_no.Text + "' and cndnType='CN' and refho_no !='" + userID + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");

                dnLast = bll.vLookUp("SELECT sum(inv_DNAmount) FROM tacc_cndndtl WHERE inv_no ='" + lblinv_no.Text + "' and cndnType='DN' and refho_no !='" + userID + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");

                if (cnLast != string.Empty)
                {
                    lblAlreadyCN.Text = Convert.ToString(cnLast);
                }
                else { lblAlreadyCN.Text = Convert.ToString(0); }

                if (dnLast != string.Empty)
                {
                    lblAlreadyDN.Text = Convert.ToString(dnLast);
                }
                else { lblAlreadyDN.Text = Convert.ToString(0); }
                var currentCNDN = "0";
                if (ddlCNDN.SelectedValue == "CN")
                {
                    currentCNDN = bll.vLookUp("SELECT sum(inv_CNAmount) FROM tacc_cndndtl WHERE inv_no ='" + lblinv_no.Text + "' and cndnType='CN' and refho_no ='" + userID + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
                }
                else if (ddlCNDN.SelectedValue == "DN")
                {
                    currentCNDN = bll.vLookUp("SELECT sum(inv_DNAmount) FROM tacc_cndndtl WHERE inv_no ='" + lblinv_no.Text + "' and cndnType='DN' and refho_no ='" + userID + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
                }
                if (currentCNDN != string.Empty)
                {
                    totCurrentCNDN += Convert.ToDecimal(currentCNDN);
                }
            }
            else if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
            {
                Label lbltotamt = (Label)e.Row.FindControl("lbltotamt");
                TextBox txtCNDN = (TextBox)e.Row.FindControl("txtCNDN");
                Label lblCNDNCurrent = (Label)e.Row.FindControl("lblCNDNCurrent");
                Label lblAlreadyCN = (Label)e.Row.FindControl("lblAlreadyCN");
                Label lblAlreadyDN = (Label)e.Row.FindControl("lblAlreadyDN");
                //Label lblBalance = (Label)e.Row.FindControl("lblBalance");
                Label lbltotVat = (Label)e.Row.FindControl("lbltotVat");
                Label lblinv_no = (Label)e.Row.FindControl("lblinv_no");
                if ((lblAlreadyCN != null || lblAlreadyDN != null) && hdfAutomaticAmount.Value != "")
                {
                    if (ddlOperation.SelectedValue == "Automatic" && Convert.ToDecimal(hdfAutomaticAmount.Value) > 0)
                    {
                        decimal currentAmount = Convert.ToDecimal(hdfAutomaticAmount.Value);
                        decimal totamount = Convert.ToDecimal(lbltotamt.Text);

                        if (currentAmount >= totamount && currentAmount > 0)
                        {
                            if (ddlCNDN.SelectedValue == "CN")
                            {
                                lblAlreadyCN.Text = Convert.ToString(totamount);
                                lblAlreadyDN.Text = Convert.ToString(0);
                            }
                            else if (ddlCNDN.SelectedValue == "DN")
                            {
                                lblAlreadyCN.Text = Convert.ToString(0);
                                lblAlreadyDN.Text = Convert.ToString(totamount);
                            }
                            currentAmount = currentAmount - totamount;
                            hdfAutomaticAmount.Value = Convert.ToString(currentAmount);
                        }
                        else
                        {
                            if (ddlCNDN.SelectedValue == "CN")
                            {
                                lblAlreadyCN.Text = Convert.ToString(currentAmount);
                                lblAlreadyDN.Text = Convert.ToString(0);
                            }
                            else if (ddlCNDN.SelectedValue == "DN")
                            {
                                lblAlreadyCN.Text = Convert.ToString(0);
                                lblAlreadyDN.Text = Convert.ToString(currentAmount);
                            }
                            //currentAmount = currentAmount - totamount;
                            hdfAutomaticAmount.Value = Convert.ToString(0);
                        }

                    }
                    else
                    {
                        lblAlreadyCN.Text = "0";
                        lblAlreadyDN.Text = "0";
                    }
                }
                if (lblAlreadyCN != null || lblAlreadyDN != null)
                {
                    var cn = string.Empty;
                    var dn = string.Empty;
                    if (ddlCNDN.SelectedValue == "CN")
                    {
                        cn = bll.vLookUp("SELECT sum(inv_CNAmount) FROM tacc_cndndtl WHERE inv_no ='" + lblinv_no.Text + "' and refho_no !='" + userID + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
                    }
                    else if (ddlCNDN.SelectedValue == "DN")
                    {
                        dn = bll.vLookUp("SELECT sum(inv_DNAmount) FROM tacc_cndndtl WHERE inv_no ='" + lblinv_no.Text + "' and refho_no !='" + userID + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
                    }

                    if (cn != "")
                    {
                        lblAlreadyCN.Text = cn;
                    }
                    else { lblAlreadyCN.Text = "0"; }

                    if (dn != "")
                    {
                        lblAlreadyDN.Text = dn;
                    }
                    else { lblAlreadyDN.Text = "0"; }
                }
                if (txtCNDN != null && hdfAutomaticAmount.Value != "")
                {
                    if (ddlOperation.SelectedValue == "Automatic" && Convert.ToDecimal(hdfAutomaticAmount.Value) > 0)
                    {
                        decimal currentAmount = Convert.ToDecimal(hdfAutomaticAmount.Value);
                        decimal totamount = Convert.ToDecimal(txtCNDN.Text);

                        if (currentAmount >= totamount && currentAmount > 0)
                        {
                            txtCNDN.Text = Convert.ToString(totamount);
                            currentAmount = currentAmount - totamount;
                            hdfAutomaticAmount.Value = Convert.ToString(currentAmount);
                        }
                        else
                        {
                            txtCNDN.Text = Convert.ToString(currentAmount);
                            //currentAmount = currentAmount - totamount;
                            hdfAutomaticAmount.Value = Convert.ToString(0);
                        }

                    }
                    else
                    {
                        txtCNDN.Text = "0";
                    }
                }
                if (txtCNDN != null)
                {
                    if (txtCNDN.Text == "")
                    {
                        totCNAmount += 0;
                        totDNAmount += 0;
                    }
                    else
                    {
                        totCNAmount += Convert.ToDecimal(txtCNDN.Text);
                        totDNAmount += Convert.ToDecimal(txtCNDN.Text);
                    }

                }
                else
                {
                    totCNAmount += Convert.ToDecimal(lblAlreadyCN.Text);
                    totDNAmount += Convert.ToDecimal(lblAlreadyDN.Text);
                }
                totAmount += Convert.ToDecimal(lbltotamt.Text);
                totVat += Convert.ToDecimal(lbltotVat.Text);

                if (lblAlreadyCN == null || lblAlreadyDN == null)
                {
                    if (txtCNDN.Text == "") txtCNDN.Text = "0";
                    //lblBalance.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(lbltotamt.Text) - Convert.ToDecimal(lblAlreadyCNDN.Text)));
                    //lblBalance.Text = Convert.ToString(Convert.ToDecimal(lbltotamt.Text));
                }
                else
                {
                    //lblBalance.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(lbltotamt.Text) - Convert.ToDecimal(lblAlreadyCNDN.Text)));
                    //lblBalance.Text = Convert.ToString(Convert.ToDecimal(lbltotamt.Text)); 
                }
                totBalance += Convert.ToDecimal(lbltotamt.Text);
                //totAlreadyCNDN += Convert.ToDecimal(lblAlreadyCNDN.Text);
                if (lblCNDNCurrent != null)
                {//sdsdsdsdsd
                    string cndn = string.Empty;
                    if (ddlCNDN.SelectedValue == "CN")
                    {
                        cndn = bll.vLookUp("SELECT sum(inv_CNAmount) FROM tacc_cndndtl WHERE inv_no ='" + lblinv_no.Text + "' and cndnType='CN' and cndn_cd ='" + userID + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
                    }
                    else if (ddlCNDN.SelectedValue == "DN")
                    {
                        cndn = bll.vLookUp("SELECT sum(inv_DNAmount) FROM tacc_cndndtl WHERE inv_no ='" + lblinv_no.Text + "' and cndnType='DN' and cndn_cd ='" + userID + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
                    }
                    if (cndn != "")
                    {
                        lblCNDNCurrent.Text = cndn;
                    }
                }
                var cnLast = "0";
                var dnLast = "0";

                cnLast = bll.vLookUp("SELECT sum(inv_CNAmount) FROM tacc_cndndtl WHERE inv_no ='" + lblinv_no.Text + "' and cndnType='CN' and refho_no !='" + userID + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");

                dnLast = bll.vLookUp("SELECT sum(inv_DNAmount) FROM tacc_cndndtl WHERE inv_no ='" + lblinv_no.Text + "' and cndnType='DN' and refho_no !='" + userID + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");

                if (cnLast != string.Empty)
                {
                    lblAlreadyCN.Text = Convert.ToString(cnLast);
                }
                else { lblAlreadyCN.Text = Convert.ToString(0); }

                if (dnLast != string.Empty)
                {
                    lblAlreadyDN.Text = Convert.ToString(dnLast);
                }
                else { lblAlreadyDN.Text = Convert.ToString(0); }

                var currentCNDN = "0";
                if (ddlCNDN.SelectedValue == "CN")
                {
                    currentCNDN = bll.vLookUp("SELECT sum(inv_CNAmount) FROM tacc_cndndtl WHERE inv_no ='" + lblinv_no.Text + "' and cndnType='CN' and refho_no ='" + userID + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
                }
                else if (ddlCNDN.SelectedValue == "DN")
                {
                    currentCNDN = bll.vLookUp("SELECT sum(inv_DNAmount) FROM tacc_cndndtl WHERE inv_no ='" + lblinv_no.Text + "' and cndnType='DN' and refho_no ='" + userID + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
                }
                if (currentCNDN != string.Empty)
                {
                    totCurrentCNDN += Convert.ToDecimal(currentCNDN);
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbVat = (Label)e.Row.FindControl("lbVat");
                Label lbtotAmount = (Label)e.Row.FindControl("lbtotAmount");
                Label lbCNAmount = (Label)e.Row.FindControl("lbCNAmount");
                Label lbDNAmount = (Label)e.Row.FindControl("lbDNAmount");
                Label lbCNDNAmountCurrent = (Label)e.Row.FindControl("lbCNDNAmountCurrent");
                //Label lbtotBalance = (Label)e.Row.FindControl("lbtotBalance");
                Label lblTotVATCNDN = (Label)e.Row.FindControl("lblTotVATCNDN");

                lbtotAmount.Text = String.Format("{0:0.00}", totAmount);
                lbCNAmount.Text = String.Format("{0:0.00}", totCNAmount);
                lbDNAmount.Text = String.Format("{0:0.00}", totDNAmount);
                lbCNDNAmountCurrent.Text = String.Format("{0:0.00}", totCurrentCNDN);
                //lbtotBalance.Text = String.Format("{0:0.00}", totBalance);

                lbVat.Text = String.Format("{0:0.00}", totVat);

                if (ddlCNDNType.SelectedValue == "VAT")
                {
                    string calVAT = string.Empty;
                    if (ddlCNDN.SelectedValue == "CN")
                    {
                        calVAT = bll.vLookUp("SELECT sum(inv_CNAmount) FROM tacc_cndndtl WHERE inv_no in (select inv_no  from tdosales_invoice where cust_cd='" + hdcust.Value + "' and  inv_sta_id in ('P','R') and salespointcd ='" + Convert.ToString(Request.Cookies["sp"].Value) + "')  and refho_no ='" + userID + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
                        if (calVAT != "")
                        {
                            totCNAmount = Convert.ToDecimal(calVAT);
                        }
                    }
                    else if (ddlCNDN.SelectedValue == "DN")
                    {
                        calVAT = bll.vLookUp("SELECT sum(inv_DNAmount) FROM tacc_cndndtl WHERE inv_no in (select inv_no  from tdosales_invoice where cust_cd='" + hdcust.Value + "' and  inv_sta_id in ('P','R') and salespointcd ='" + Convert.ToString(Request.Cookies["sp"].Value) + "')  and refho_no ='" + userID + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
                        if (calVAT != "")
                        {

                            totDNAmount = Convert.ToDecimal(calVAT);

                        }
                    }

                    if (ddlCNDN.SelectedValue == "CN")
                    {
                        lblVat.Text = String.Format("{0:0.00}", (totCNAmount - ((totCNAmount * 100) / 105)));
                    }
                    else if (ddlCNDN.SelectedValue == "DN")
                    {
                        lblVat.Text = String.Format("{0:0.00}", (totDNAmount - ((totDNAmount * 100) / 105)));
                    }
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
            lblVat.Text = "0";
            txtAutomaticAmount.Text = "0";
            hdfAutomaticAmount.Value = "0";
            totCNAmount = 0;
            totDNAmount = 0;
            txtRemarks.Text = "";
            txtRef.Text = "";
            dtCNDNDate.Text = "";
            //ddlCNDN.SelectedValue = "CN";
            btnAutomatic.Enabled = true;
            ddlCNDNType.SelectedValue = "NonVAT";
            txtAdditional.Text = "";
            ddlHOFormType.SelectedValue = "HODIFF";
            ddlHOFormType_SelectedIndexChanged(sender, e);
            dvAutomactic.Visible = false;
            dvbtnAutomatic.Visible = false;
            
            //ddlOperation_SelectedIndexChanged(sender, e);
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@refho_no", Convert.ToString(Request.Cookies["usr_id"].Value)));
            arr.Add(new cArrayList("@salespointcd", Convert.ToString(Request.Cookies["sp"].Value)));
            bll.vtacc_cndndtl_dtl_dlt(arr);

            ddlOperation.SelectedValue = "Manual";
            //ddlOperation_SelectedIndexChanged(sender, e);

            // Enh : 22 June 2019 : Customer Transfer Blocked - CIN
            string sCustomerTransferBlock = bll.vLookUp("select dbo.fn_customertransferpending('" + hdcust.Value.ToString() + "','"+ Request.Cookies["sp"].Value.ToString() + "')");
            if (sCustomerTransferBlock != "ok")
            {
                hdcust.Value = ""; txcust.Text = "";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('This customer block for sales caused there is pending in customer transfer ','" + sCustomerTransferBlock + "','warning');", true);
                return;
            }

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

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Invoice load failed','Invoice Load','warning');", true);
            ut.Logs("", "CNDN Adjustment", "CNDN Adjustment", "fm_acccndn", "btShowInvoice_Click", "Exception", ex.Message + ex.InnerException);
        }
    }


    protected void btSave_Click(object sender, EventArgs e)
    {
        try
        {
            decimal vatCNDN = 0;
            string cndn_cd = string.Empty;
            string refho_no = string.Empty;
            string fileExtension = string.Empty;
            creport rep = new creport();
            //if (Convert.ToString(txtRef.Text) == "")
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Ref required','Ref required','warning');", true);
            //    return;
            //}

            DateTime cndnDate = DateTime.ParseExact(dtCNDNDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            if(cndnDate > DateTime.Now)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('CNDN date cann't greater than today','Wrong CNDN date','warning');", true);
                return;
            }

            if (upl.HasFile==false)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please upload document','Upload document','warning');", true);
                return;
            }
            else if (upl.HasFile)
            {
                FileInfo fi = new FileInfo(upl.FileName);
                string ext = fi.Extension;
                fileExtension = fi.Extension;
                byte[] fs = upl.FileBytes;
                if (fs.Length <= 104857600)
                {
                    if ((upl.FileName != "") || (upl.FileName != null))
                    {
                        //if (File.Exists(bll.sGetControlParameter("image_path") + "/CNDNAdj/" + Convert.ToString(Request.Cookies["sp"].Value) + "_" + hdcust.Value + "_" + upl.FileName))
                        //{
                        //    File.Delete(bll.sGetControlParameter("image_path") + "/CNDNAdj/" + Convert.ToString(Request.Cookies["sp"].Value) + "_" + hdcust.Value + "_" + upl.FileName);
                        //}

                        //upl.SaveAs(bll.sGetControlParameter("image_path") + "/CNDNAdj/" + Convert.ToString(Request.Cookies["sp"].Value) + "_" + hdcust.Value + "_" + upl.FileName);
                        //hdfFileName.Value = Convert.ToString(Request.Cookies["sp"].Value) + "_" + hdcust.Value + "_" + upl.FileName;
                        if (File.Exists(bll.sGetControlParameter("image_path") + "/CNDNAdj/" + Convert.ToString(Request.Cookies["sp"].Value) + "_" + upl.FileName))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('File already exist"+ upl.FileName + ", please select another file','please select another file','warning');", true);
                            return;
                        }

                        else
                        {
                            if (File.Exists(bll.sGetControlParameter("image_path") + "/CNDNAdj/" + Convert.ToString(Request.Cookies["sp"].Value) + "_" + upl.FileName))
                            {
                                File.Delete(bll.sGetControlParameter("image_path") + "/CNDNAdj/" + Convert.ToString(Request.Cookies["sp"].Value) + "_" + upl.FileName);
                            }

                            upl.SaveAs(bll.sGetControlParameter("image_path") + "/CNDNAdj/" + Convert.ToString(Request.Cookies["sp"].Value) + "_" + upl.FileName);
                            hdfFileName.Value = Convert.ToString(Request.Cookies["sp"].Value) + "_" + upl.FileName;
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please size very big.Please upload small size','Please size very big','warning');", true);
                    return;
                }
            }

            if (Convert.ToString(txtRef.Text) == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Clearance No required','Clearance No required','warning');", true);
                return;
            }
            else if (ddlHOFormType.SelectedValue == "HOCM")
            {
                if (txtRef.Text.Length > 7)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Can not insert more than 7 digit for HOCM','Clearance No big size','warning');", true);
                    return;
                }
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

            string userID = Convert.ToString(Request.Cookies["usr_id"].Value);

            string cnt = bll.vLookUp("SELECT count(*) FROM tacc_cndndtl WHERE  refho_no ='" + userID + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
            if (cnt == "0")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('CN DN Wrong, Select atleast one invoice','CN DN wrong','warning');", true);
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

            #region Here we are making refho_no .

            if (txtAdditional.Text != "")
            {
                refho_no = ddlHOFormType.SelectedValue + "-" + txtRef.Text + "-" + txtAdditional.Text;
            }
            else
            {
                refho_no = ddlHOFormType.SelectedValue + "-" + txtRef.Text;
            }
            #endregion


            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@reasn_cd", Convert.ToString(ddlReason.SelectedValue)));
            arr.Add(new cArrayList("@cndnType", Convert.ToString(ddlCNDN.SelectedValue)));

            if (ddlCNDN.SelectedValue == "CN")
            {
                arr.Add(new cArrayList("@totamtCN", totCNAmount));
                arr.Add(new cArrayList("@totamtDN", 0));
            }
            else if (ddlCNDN.SelectedValue == "DN")
            {
                arr.Add(new cArrayList("@totamtCN", 0));
                arr.Add(new cArrayList("@totamtDN", totDNAmount));
            }
            arr.Add(new cArrayList("@cust_cd", hdcust.Value));
            arr.Add(new cArrayList("@remark", Convert.ToString(txtRemarks.Text)));
            arr.Add(new cArrayList("@vatamt", vatCNDN));
            arr.Add(new cArrayList("@vatincluded", vatincluded));
            arr.Add(new cArrayList("@refho_no", refho_no));
            arr.Add(new cArrayList("@salespointcd", Convert.ToString(Request.Cookies["sp"].Value)));
            arr.Add(new cArrayList("@post_dt", DateTime.ParseExact(dtpost.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@cndn_dt", DateTime.ParseExact(dtCNDNDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@createdby", Convert.ToString(Request.Cookies["usr_id"].Value)));
            arr.Add(new cArrayList("@ClearanceNo", Convert.ToString(txtRef.Text)));
            arr.Add(new cArrayList("@HOFormType", Convert.ToString(ddlHOFormType.SelectedValue)));
            arr.Add(new cArrayList("@OnAccountOf", Convert.ToString(ddlOnAccount.SelectedValue)));
            arr.Add(new cArrayList("@addtional", Convert.ToString(txtAdditional.Text)));
            arr.Add(new cArrayList("@cndnAdj_sta_id", Convert.ToString("N")));
            arr.Add(new cArrayList("@uploadByUser", Convert.ToString(hdfFileName.Value)));
            arr.Add(new cArrayList("@fileExtension", fileExtension));

            bll.vInsertACC_cndn(arr, ref cndn_cd);

            
            System.IO.File.Move(bll.sGetControlParameter("image_path") + @"CNDNAdj\"+ hdfFileName.Value, bll.sGetControlParameter("image_path") + @"CNDNAdj\" + cndn_cd + "_doc"  + fileExtension);

            if (cndn_cd == "-2")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Ref no already available.','Ref no already available. ','warning');", true);
                return;
            }
            else
            {
                lbsysno.Text = cndn_cd;
                #region
                //int nrnd = 0;
                //Random rnd = new Random();
                //nrnd = rnd.Next(1000, 9999);
                //string ssalespoint = bll.vLookUp("select salespointcd +'-'+salespoint_nm from tmst_salespoint where salespointcd=" + Request.Cookies["sp"].Value.ToString());
                //string stoken = bll.vLookUp("select short_ip from tmst_salespoint where salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'") + nrnd.ToString();
                //List<string> lapproval = bll.lGetApproval(cbapproval.SelectedValue.ToString());
                //string cndnAmount = bll.vLookUp("select sum(totamtCN + totamtDN + vatamt) from tacc_cndn  where cndn_cd = '" + cndn_cd + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
                //string sMsg = "#CNDN Adjustment-"+ ssalespoint + "-" + refho_no + ", CNDN Amount '"+ cndnAmount + "' , do you want to approved : (Y/N)" + stoken;
                //arr.Clear();
                //arr.Add(new cArrayList("@token", stoken));
                //arr.Add(new cArrayList("@doc_no", cndn_cd));
                //arr.Add(new cArrayList("@doc_typ", "CNDNAdjustment"));
                //arr.Add(new cArrayList("@to", lapproval[0]));
                //arr.Add(new cArrayList("@msg", sMsg.TrimEnd()));
                //bll.vInsertSmsOutbox(arr);

                //// Sending Email 
                //string sSubject = ""; string sMessage = "";
                //string sfile_attachment = string.Empty;
                //string slink_branch = bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='link_branch'");
                //string stitle = bll.vLookUp("select fld_desc from tfield_value where fld_nm='job_title_cd' and fld_valu=( select job_title_cd from tmst_employee where emp_cd='" + "" + "')");
                ////string cashout_typ = ddlCNDN.SelectedValue.ToString();
                
                //sSubject = "#CNDN Adjustment Request Branch " + bll.sGetSalespointname(Request.Cookies["sp"].Value.ToString()) + " has been created ";

                //string filePath =  bll.sGetControlParameter("image_path") + @"CNDNAdj\";
                //string fileName = cndn_cd + ".pdf";
                //string fileExcelName = cndn_cd + ".xls";
                //arr.Clear();
                //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //arr.Add(new cArrayList("@RefNo", cndn_cd));
                //arr.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));

                //rep.vShowReportToPDF("rp_acccndnadjRefNo.rpt", arr, bll.sGetControlParameter("image_path") + @"CNDNAdj\" + fileName);
                ////rep.vShowReportToPDF("rp_acccndnAdjApp.rpt", arr, bll.sGetControlParameter("image_path") + @"CNDNAdj\" + fileName);
                ////rep.vShowReportToEXCEL("rp_acccndnAdjApp.rpt", arr, bll.sGetControlParameter("image_path") + @"CNDNAdj\" + fileExcelName);


                //sMessage = "<table><tr><td>#CNDN Adjustment Request Approval</td></tr><tr><td>Salespoint</td><td>:</td><td>" + ssalespoint + "</td></tr>"+
                //    "<tr><td>CNDN Code</td><td>:</td><td>" + cndn_cd + "</td></tr>" +
                //    "<tr><td>Ref HO</td><td>:</td><td>" + refho_no + "</td></tr>" +
                //    "<tr><td>CNDN Amount</td><td>:</td><td>" + cndnAmount + "</td></tr>" +
                //"<tr><td>Please Click this  for View Document</td><td>:</td><td> <a href='" + slink_branch
                //    + "/images/CNDNAdj/" + refho_no + "_doc" + fileExtension + "'>View Document</a></td></tr>" +
                //    "</table>" +
                //"<p> Please Click this  for approved : <a href='" + slink_branch
                //+ "/landingpage2.aspx?src=acccndnAdjApp&salespointcd=" + Request.Cookies["sp"].Value.ToString()
                //+ "&ids=" + cndn_cd + "&sta=A&updatMethod=email&appBy=" + cbapproval.SelectedValue + "'>Approve</a>, or for rejected please click <a href='"
                //+ slink_branch + "/landingpage2.aspx?src=acccndnAdjApp&salespointcd=" + Request.Cookies["sp"].Value.ToString() + "&ids="
                //+ cndn_cd + "&sta=R&updatMethod=email&appBy=" + cbapproval.SelectedValue + "'>Reject</a></p>" +
                //" \n\r\n\r\n\r\n Wazaran Admin";
                
                //bll.vSendMail(lapproval[1], sSubject,sMessage,  @"CNDNAdj\" + fileName);
                //arr.Clear();
                //arr.Add(new cArrayList("@trxcd", "acccndnAdjApp"));
                //arr.Add(new cArrayList("@token", nrnd.ToString()));
                //arr.Add(new cArrayList("@doc_no", cndn_cd));
                //bll.vInsertEmailSent(arr);
                //arr.Clear();
                //sfile_attachment = @"CNDNAdj\" + fileName;
                //arr.Add(new cArrayList("@token", nrnd.ToString()));
                //arr.Add(new cArrayList("@doc_typ", "acccndnAdjAppPDF"));
                //arr.Add(new cArrayList("@to", lapproval[1]));
                //arr.Add(new cArrayList("@doc_no", cndn_cd));
                //arr.Add(new cArrayList("@emailsubject", sSubject));
                //arr.Add(new cArrayList("@msg", sMessage));
                //arr.Add(new cArrayList("@file_attachment", sfile_attachment));
                //bll.vInsertEmailOutbox(arr);
                //arr.Clear();
                //sfile_attachment = @"CNDNAdj\" + fileExcelName;
                //arr.Add(new cArrayList("@token", nrnd.ToString()));
                //arr.Add(new cArrayList("@doc_typ", "acccndnAdjAppExcel"));
                //arr.Add(new cArrayList("@to", lapproval[1]));
                //arr.Add(new cArrayList("@doc_no", cndn_cd));
                //arr.Add(new cArrayList("@emailsubject", sSubject));
                //arr.Add(new cArrayList("@msg", sMessage));
                //arr.Add(new cArrayList("@file_attachment", sfile_attachment));
                //bll.vInsertEmailOutbox(arr);
                //arr.Clear();
                //sfile_attachment = @"CNDNAdj\" + hdfFileName.Value;
                //arr.Add(new cArrayList("@token", nrnd.ToString()));
                //arr.Add(new cArrayList("@doc_typ", "acccndnAdjAppDoc"));
                //arr.Add(new cArrayList("@to", lapproval[1]));
                //arr.Add(new cArrayList("@doc_no", cndn_cd));
                //arr.Add(new cArrayList("@emailsubject", sSubject));
                //arr.Add(new cArrayList("@msg", sMessage));
                //arr.Add(new cArrayList("@file_attachment", sfile_attachment));
                //bll.vInsertEmailOutbox(arr);
                #endregion


                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Data Save Successfully.','Data Save Successfully.','success');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=acccndnRefNo&RefNo=" + cndn_cd + "');", true);
                lblVat.Text = "0";
                txtAutomaticAmount.Text = "0";
                hdcust.Value = "";
                hdfAutomaticAmount.Value = "0";
                totCNAmount = 0;
                totDNAmount = 0;
                txtRemarks.Text = "";
                vatCNDN = 0;
                vatincluded = false;
                txcust.Text = "";
                txtRef.Text = "";
                dtCNDNDate.Text = "";
                ddlCNDN.SelectedValue = "CN";
                btnAutomatic.Enabled = true;
                ddlCNDNType.SelectedValue = "NonVAT";
                txtAdditional.Text = "";
                ddlHOFormType.SelectedValue = "HODIFF";
                ddlHOFormType_SelectedIndexChanged(sender, e);
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
                btSave.Enabled = false;
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('"+ ex.Message + ex.InnerException + "','Error during save','warning');", true);

            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('" + ex.Message.ToString() + ex.InnerException.ToString() + "','" + ex.Message.ToString() + ex.InnerException.ToString() + "','warning');", true);
            ut.Logs("", "Account", "CN DN Adjustment", "fm_acccndn", "btsave", "Exception", ex.Message + ex.InnerException);
        }
    }

    protected void btAutomatic_Click(object sender, EventArgs e)
    {
        try
        {
            decimal dAutomaticAmount = 0;
            //string lbtotBalance = (grd.FooterRow.FindControl("lbtotBalance") as Label).Text;
            string lbtotAmount = (grd.FooterRow.FindControl("lbtotAmount") as Label).Text;
            hdfAutomaticAmount.Value = Convert.ToString(txtAutomaticAmount.Text);
            if (!decimal.TryParse(txtAutomaticAmount.Text, out dAutomaticAmount))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please amount on numeric only','Amount not inserted properly','warning');", true);
                return;
            }
            else if (Convert.ToDecimal(lbtotAmount) < Convert.ToDecimal(hdfAutomaticAmount.Value))
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
            bll.vBindingGridToSp(ref grd, "sp_tdosales_invoiceByStat_get", arr);
            btnAutomatic.Enabled = false;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Error during Automatic','Error during Automatic','warning');", true);

            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('" + ex.Message.ToString() + ex.InnerException.ToString() + "','" + ex.Message.ToString() + ex.InnerException.ToString() + "','warning');", true);
            ut.Logs("", "Account", "CN DN Adjustment", "fm_acccndn", "btAutomatic_Click", "Exception", ex.Message + ex.InnerException);
        }
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
            btShowInvoice_Click(sender, e);
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
        string userID = Convert.ToString(Request.Cookies["usr_id"].Value);
        if (ddlCNDNType.SelectedValue == "VAT" && ddlOperation.SelectedValue == "Automatic" && Convert.ToDecimal(txtAutomaticAmount.Text) > 0)
        {
            //dvCNDNVatCal.Visible = true;
            //B9-((B9*100)/105)

            decimal vatAmount = 0;
            decimal dCNDNAmount = Convert.ToDecimal(txtAutomaticAmount.Text);
            vatAmount = dCNDNAmount - ((dCNDNAmount * 100) / 105);
            lblVat.Text = String.Format("{0:0.00}", vatAmount);

            showTax.Attributes.Remove("style");
        }
        else if (ddlCNDNType.SelectedValue == "VAT")
        {
            //string lbCNDNAmount = (grd.FooterRow.FindControl("lbCNDNAmount") as Label).Text;
            //if (lbCNDNAmount != "")
            //{
            //    decimal vatAmount = 0;
            //    decimal dCNDNAmount = Convert.ToDecimal(lbCNDNAmount);
            //    vatAmount = dCNDNAmount - ((dCNDNAmount * 100) / 105);
            //    lblVat.Text = Convert.ToString(vatAmount);
            //}
            if (ddlCNDNType.SelectedValue == "VAT")
            {
                string calVAT = string.Empty;
                if (ddlCNDN.SelectedValue == "CN")
                {
                    calVAT = bll.vLookUp("SELECT sum(inv_CNAmount) FROM tacc_cndndtl WHERE inv_no in (select inv_no  from tdosales_invoice where cust_cd='" + hdcust.Value + "' and  inv_sta_id in ('P','R') and salespointcd ='" + Convert.ToString(Request.Cookies["sp"].Value) + "')  and refho_no ='" + userID + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
                    if (calVAT != "")
                    {
                        totCNAmount = Convert.ToDecimal(calVAT);
                    }
                }
                else if (ddlCNDN.SelectedValue == "DN")
                {
                    calVAT = bll.vLookUp("SELECT sum(inv_DNAmount) FROM tacc_cndndtl WHERE inv_no in (select inv_no  from tdosales_invoice where cust_cd='" + hdcust.Value + "' and  inv_sta_id in ('P','R') and salespointcd ='" + Convert.ToString(Request.Cookies["sp"].Value) + "')  and refho_no ='" + userID + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
                    if (calVAT != "")
                    {
                        totDNAmount = Convert.ToDecimal(calVAT);
                    }
                }

                if (ddlCNDN.SelectedValue == "CN")
                {
                    lblVat.Text = String.Format("{0:0.00}", (totCNAmount - ((totCNAmount * 100) / 105)));
                }
                else if (ddlCNDN.SelectedValue == "DN")
                {
                    lblVat.Text = String.Format("{0:0.00}", (totDNAmount - ((totDNAmount * 100) / 105)));
                }
            }
            showTax.Attributes.Remove("style");
        }
        else
        {
            lblVat.Text = Convert.ToString(0);
        }
    }
    protected void ddlCNDN_SelectedIndexChanged(object sender, EventArgs e)
    {
        btShowInvoice_Click(sender, e);
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
            totCNDetailsAmount += Convert.ToDecimal(lblCN.Text);
            totDNDetailsAmount += Convert.ToDecimal(lblDN.Text);
        }
        else if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate))
        {
            Label lblCN = (Label)e.Row.FindControl("lblCN");
            Label lblDN = (Label)e.Row.FindControl("lblDN");
            totCNDetailsAmount += Convert.ToDecimal(lblCN.Text);
            totDNDetailsAmount += Convert.ToDecimal(lblDN.Text);
        }
        else if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
        {
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbtotDtlCNAmount = (Label)e.Row.FindControl("lbtotDtlCNAmount");
            Label lbtotDtlDNAmount = (Label)e.Row.FindControl("lbtotDtlDNAmount");
            lbtotDtlCNAmount.Text = String.Format("{0:0.00}", totCNDetailsAmount);
            lbtotDtlDNAmount.Text = String.Format("{0:0.00}", totDNDetailsAmount);
        }
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

        Label lblinv_no = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lblinv_no");
        if (lblinv_no.Text != "")
        {
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
    protected void ddlHOFormType_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (ddlHOFormType.SelectedValue == "HODIFF")
        {
            lblSample.Text = "HODIFF-1700776-1";
            lblFormType.Text = "HODIFF";
            lblClearance.Text = "1700776";
            txtRef.Text = "";
            txtAdditional.Text = "";
            arr.Add(new cArrayList("@reasn_typ", Convert.ToString("cndn")));
            bll.vBindingComboToSp(ref ddlReason, "sp_tmst_reason_get", "reasn_cd", "reasn_nm", arr);
        }
        if (ddlHOFormType.SelectedValue == "HOCM")
        {
            lblSample.Text = "HOCM-171824-1";
            lblFormType.Text = "HOCM";
            lblClearance.Text = "171824";
            txtRef.Text = "";
            txtAdditional.Text = "";
            arr.Add(new cArrayList("@reasn_typ", Convert.ToString("cndnHOCM")));
            bll.vBindingComboToSp(ref ddlReason, "sp_tmst_reason_get", "reasn_cd", "reasn_nm", arr);
        }
    }
    protected void txtRef_TextChanged(object sender, EventArgs e)
    {
        if (ddlHOFormType.SelectedValue == "HOCM")
        {
            if (txtRef.Text.Length > 7)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Can not insert more than 7 digit for HOCM','Clearance No big size','warning');", true);
                return;
            }
        }
        lblFormType.Text = ddlHOFormType.SelectedValue;
        lblClearance.Text = txtRef.Text;
        lblAdditional.Text = txtAdditional.Text;
        //lblSample.Text = "HODIFF-1700776-1";
        if (txtAdditional.Text != "")
        {
            lblSample.Text = ddlHOFormType.SelectedValue + "-" + lblClearance.Text + "-" + txtAdditional.Text;
        }
        else
        {
            lblSample.Text = ddlHOFormType.SelectedValue + "-" + lblClearance.Text;
        }
    }
    protected void txtAdditional_TextChanged(object sender, EventArgs e)
    {
        if (ddlHOFormType.SelectedValue == "HOCM")
        {
            if (txtRef.Text.Length > 7)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Can not insert more than 7 digit for HOCM','Clearance No big size','warning');", true);
                return;
            }
        }
        lblFormType.Text = ddlHOFormType.SelectedValue;
        lblClearance.Text = txtRef.Text;
        lblAdditional.Text = txtAdditional.Text;
        if (txtAdditional.Text != "")
        {
            lblSample.Text = ddlHOFormType.SelectedValue + "-" + lblClearance.Text + "-" + txtAdditional.Text;
        }
        else
        {
            lblSample.Text = ddlHOFormType.SelectedValue + "-" + lblClearance.Text;
        }
    }
    protected void btnViewCNDN_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_acccndnBranchReport.aspx");
    }
    protected void btlookup_Click(object sender, EventArgs e)
    {
        string refho_no = string.Empty;
        string[] ret = hdfCNDNID.Value.Split('|');
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=acccndnRefNo&RefNo=" + Convert.ToString(ret[2]) + "');", true);
    }


    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListTax(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lCustomer = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string sCustomer = string.Empty;
        arr.Add(new cArrayList("@tax_cd", prefixText));
        arr.Add(new cArrayList("@tax_type", "E"));
        bll.vGetMstTax(arr, ref rs);
        while (rs.Read())
        {
            sCustomer = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["tax_cd"].ToString() + "-" + rs["tax_desc"], rs["tax_cd"].ToString());
            lCustomer.Add(sCustomer);
        }
        return (lCustomer.ToArray());
    }
    protected void bttax_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cndn_cd", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@tax_cd", hdtaxall.Value));
        arr.Add(new cArrayList("@amount", 0));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vInsertACCcndn_tax(arr);
        arr.Clear();
        arr.Add(new cArrayList("@cndn_cd", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBindingGridToSp(ref grdtax, "sp_tacc_cndn_tax_get", arr);
        hdtaxall.Value = "";
        txtaxall.Text = "";
    } 

    protected void grdtax_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //double dVat = 0, dValue = 0, dAmt = Double.Parse(txamt.Text.ToString());
        //if (grdtax.Rows.Count > 0)
        //{
        //    dVat = Double.Parse(bll.vLookUp("select sum(amount) from tcashout_request where cashout_cd='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'"));
        //    dValue = dAmt - dVat;
        //}
        //txamt.Text = Math.Round(dValue, 5, MidpointRounding.AwayFromZero).ToString(); // String.Format("{0:0.00}", dValue);
        //lbvat.Text = Math.Round(dVat, 5, MidpointRounding.AwayFromZero).ToString();// String.Format("{0:0.00}", dVat);

        HiddenField tax_cd = (HiddenField)grdtax.Rows[e.RowIndex].FindControl("tax_cd");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cndn_cd", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@tax_cd", tax_cd.Value));
        bll.vDeleteACCcndn_tax(arr);
        arr.Clear();
        arr.Add(new cArrayList("@cndn_cd", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBindingGridToSp(ref grdtax, "sp_tacc_cndn_tax_get", arr);
    }
}