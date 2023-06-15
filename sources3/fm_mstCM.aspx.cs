using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_mstCM : System.Web.UI.Page
{
    cbll bll = new cbll();

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack) 
        {
            //lbhocm_no.Text = "NEW";
            Session["hdsalespoint"] = "";
            bll.vBindingComboToSp(ref cbchbank, "sp_tmst_bankaccount_get", "acc_no", "bank_desc");
            bll.vBindingComboToSp(ref cbbtbank, "sp_tmst_bankaccount_get", "acc_no", "bank_desc");
            bll.vBindingComboToSp(ref cbcdbranch, "sp_tmst_salespoint_get", "salespointcd", "salespoint_desc");
            bll.vBindingComboToSp(ref cbbranch, "sp_tmst_salespoint_get", "salespointcd", "salespoint_desc");
            bll.vBindingFieldValueToCombo(ref cbbrcustomer, "cusgrcd");
            cbchbank.Items.Insert(0, "NONE");
            cbbtbank.Items.Insert(0, "NONE");
            cbcdbranch.Items.Insert(0, "NONE");
            cbbranch.Items.Insert(0, "NONE");
            //cbpayment_SelectedIndexChanged(sender, e);
            List<cArrayList> arr = new List<cArrayList>();
            if (hdcm.Value.ToString() == "")
            {
                arr.Add(new cArrayList("@cm_no", Request.Cookies["usr_id"].Value.ToString()));
            }
            else
            {
                arr.Add(new cArrayList("@cm_no", hdcm.Value.ToString()));
            }
            bll.vDeleteCreditMemoCash(arr);
            bll.vDeleteCreditMemoBank(arr);
            bll.vDeleteCreditMemoDebt(arr);
            bll.vDeleteCreditMemoAgreement(arr);
            bll.vDeleteCreditMemoTransfer(arr);
            arr.Clear();
            arr.Add(new cArrayList("@doc_cd", null));
            arr.Add(new cArrayList("@doc_typ", "CREDITMEMO"));
            bll.vBindingGridToSp(ref grddoc, "sp_tmst_document_get", arr);
            btupload.Visible = false;
            btedit.Visible = false;
            btdelete.Visible = false;
            btapprove.Visible = false;
            btreject.Visible = false;
        }
    }


    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetListHoAg(string prefixText, int count, string contextKey)
    {
        HttpCookie cook;
        cook = HttpContext.Current.Request.Cookies["sp"];
        cbll bll = new cbll();
        List<string> lItem = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string sItem = string.Empty;
        System.Data.SqlClient.SqlDataReader rs = null;
        arr.Add(new cArrayList("@ag_no", prefixText));
        bll.vSearchMstHoAg(arr, ref rs);
        while (rs.Read())
        {
            sItem = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["contract_no"].ToString() + " | " + rs["contract_sbtc_no"].ToString() + " | " + rs["contract_cust_no"].ToString() + " | " + rs["amount"].ToString(), rs["contract_no"].ToString());
            lItem.Add(sItem);

        }
        rs.Close();
        return (lItem.ToArray());
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetListCustomer(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lCust = new List<string>();
        string sCust = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cust_cd", prefixText));
        arr.Add(new cArrayList("@salespointcd", HttpContext.Current.Session["hdsalespoint"].ToString()));
        bll.vSearchMstCustomerCM(arr, ref rs);
        while (rs.Read())
        {
            sCust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + "-" + rs["cust_nm"].ToString(), rs["cust_cd"].ToString());
            lCust.Add(sCust);
        }
        rs.Close();
        return (lCust.ToArray());
    }

    protected void btsearchhoag_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ops", "popupwindow('fm_lookupCM.aspx');", true);
    }
    protected void btnaddch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();

        if (hdcm.Value.ToString() == "")
        {
            arr.Add(new cArrayList("@cm_no", Request.Cookies["usr_id"].Value.ToString()));
        }
        else
        {
            arr.Add(new cArrayList("@cm_no", hdcm.Value.ToString()));
        }

        arr.Add(new cArrayList("@cash_no", txchno.Text));
        arr.Add(new cArrayList("@rv_no", txrvno.Text));
        arr.Add(new cArrayList("@bank_cd", cbchbank.SelectedValue.ToString()));
        arr.Add(new cArrayList("@cash_notes", txchnotes.Text));
        arr.Add(new cArrayList("@amount", txchvalue.Text));
        bll.vInsertCreditMemoCash(arr);
        arr.Clear();
        if (hdcm.Value.ToString() == "")
        {
            arr.Add(new cArrayList("@cm_no", Request.Cookies["usr_id"].Value.ToString()));
        }
        else
        {
            arr.Add(new cArrayList("@cm_no", hdcm.Value.ToString()));
        }
        bll.vBindingGridToSp(ref grdch, "sp_tcreditmemo_cash_get", arr);
    }
    protected void grdch_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbchno = (Label)grdch.Rows[e.RowIndex].FindControl("lbchno");
        HiddenField hdchcm = (HiddenField)grdch.Rows[e.RowIndex].FindControl("hdchcm");
        List<cArrayList> arr = new List<cArrayList>();
        if (hdcm.Value.ToString() == "")
        {
            arr.Add(new cArrayList("@cm_no", Request.Cookies["usr_id"].Value.ToString()));
        }
        else
        {
            arr.Add(new cArrayList("@cm_no", hdcm.Value.ToString()));
        }
        arr.Add(new cArrayList("@cash_no", lbchno.Text));
        bll.vDeleteCreditMemoCash(arr);
        arr.Clear();
        if (hdcm.Value.ToString() == "")
        {
            arr.Add(new cArrayList("@cm_no", Request.Cookies["usr_id"].Value.ToString()));
        }
        else
        {
            arr.Add(new cArrayList("@cm_no", hdcm.Value.ToString()));
        }
        bll.vBindingGridToSp(ref grdch, "sp_tcreditmemo_cash_get", arr);
    }
    protected void cbpayment_SelectedIndexChanged(object sender, EventArgs e)
    {
        ////if (cbpayment.SelectedValue.ToString() == "CH" || cbpayment.SelectedValue.ToString() == "BT") 
        ////{
        ////    CHscreen.Attributes.Remove("style");
        ////    CDscreen.Attributes.Add("style", "display:none");
        ////}
        ////else
        ////{
        ////    CDscreen.Attributes.Remove("style");
        ////    CHscreen.Attributes.Add("style", "display:none");
        ////}
        //CHscreen.Attributes.Remove("style");
        //BTscreen.Attributes.Remove("style");
        //CDscreen.Attributes.Remove("style");
        //AGscreen.Attributes.Remove("style");
    }
    protected void btnaddcd_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();

        if (hdcm.Value.ToString() == "")
        {
            arr.Add(new cArrayList("@cm_no", Request.Cookies["usr_id"].Value.ToString()));
        }
        else
        {
            arr.Add(new cArrayList("@cm_no", hdcm.Value.ToString()));
        }

        arr.Add(new cArrayList("@debt_no", txcdno.Text));
        arr.Add(new cArrayList("@cust_no", hdcdcust.Value));
        arr.Add(new cArrayList("@salespoint_cd", cbcdbranch.SelectedValue.ToString()));
        arr.Add(new cArrayList("@debt_notes", txcdnotes.Text));
        arr.Add(new cArrayList("@amount", txcdvalue.Text));
        bll.vInsertCreditMemoDebt(arr);
        arr.Clear();
        if (hdcm.Value.ToString() == "")
        {
            arr.Add(new cArrayList("@cm_no", Request.Cookies["usr_id"].Value.ToString()));
        }
        else
        {
            arr.Add(new cArrayList("@cm_no", hdcm.Value.ToString()));
        }
        bll.vBindingGridToSp(ref grdcd, "sp_tcreditmemo_debt_get", arr);
    }
    protected void grdcd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbdtno = (Label)grdcd.Rows[e.RowIndex].FindControl("lbdtno");
        HiddenField hddtcm = (HiddenField)grdcd.Rows[e.RowIndex].FindControl("hddtcm");
        List<cArrayList> arr = new List<cArrayList>();
        if (hdcm.Value.ToString() == "")
        {
            arr.Add(new cArrayList("@cm_no", Request.Cookies["usr_id"].Value.ToString()));
        }
        else
        {
            arr.Add(new cArrayList("@cm_no", hdcm.Value.ToString()));
        }
        arr.Add(new cArrayList("@debt_no", lbdtno.Text));
        bll.vDeleteCreditMemoDebt(arr);
        arr.Clear();
        if (hdcm.Value.ToString() == "")
        {
            arr.Add(new cArrayList("@cm_no", Request.Cookies["usr_id"].Value.ToString()));
        }
        else
        {
            arr.Add(new cArrayList("@cm_no", hdcm.Value.ToString()));
        }
        bll.vBindingGridToSp(ref grdch, "sp_tcreditmemo_debt_get", arr);
    }
    protected void btnaddbt_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();

        if (hdcm.Value.ToString() == "")
        {
            arr.Add(new cArrayList("@cm_no", Request.Cookies["usr_id"].Value.ToString()));
        }
        else
        {
            arr.Add(new cArrayList("@cm_no", hdcm.Value.ToString()));
        }

        arr.Add(new cArrayList("@bank_no", txbtno.Text));
        arr.Add(new cArrayList("@rv_no", txbtrvno.Text));
        arr.Add(new cArrayList("@bank_cd", cbbtbank.SelectedValue.ToString()));
        arr.Add(new cArrayList("@bank_notes", txbtnotes.Text));
        arr.Add(new cArrayList("@amount", txbtvalue.Text));
        bll.vInsertCreditMemoBank(arr);
        arr.Clear();
        if (hdcm.Value.ToString() == "")
        {
            arr.Add(new cArrayList("@cm_no", Request.Cookies["usr_id"].Value.ToString()));
        }
        else
        {
            arr.Add(new cArrayList("@cm_no", hdcm.Value.ToString()));
        }
        bll.vBindingGridToSp(ref grdbt, "sp_tcreditmemo_bank_get", arr);
    }
    protected void grdbt_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbbtno = (Label)grdbt.Rows[e.RowIndex].FindControl("lbbtno");
        HiddenField hdbtcm = (HiddenField)grdbt.Rows[e.RowIndex].FindControl("hdbtcm");
        List<cArrayList> arr = new List<cArrayList>();
        if (hdcm.Value.ToString() == "")
        {
            arr.Add(new cArrayList("@cm_no", Request.Cookies["usr_id"].Value.ToString()));
        }
        else
        {
            arr.Add(new cArrayList("@cm_no", hdcm.Value.ToString()));
        }
        arr.Add(new cArrayList("@bank_no", lbbtno.Text));
        bll.vDeleteCreditMemoBank(arr);
        arr.Clear();
        if (hdcm.Value.ToString() == "")
        {
            arr.Add(new cArrayList("@cm_no", Request.Cookies["usr_id"].Value.ToString()));
        }
        else
        {
            arr.Add(new cArrayList("@cm_no", hdcm.Value.ToString()));
        }
        bll.vBindingGridToSp(ref grdch, "sp_tcreditmemo_bank_get", arr);
    }
    protected void btnaddag_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();

        if (hdcm.Value.ToString() == "")
        {
            arr.Add(new cArrayList("@cm_no", Request.Cookies["usr_id"].Value.ToString()));
        }
        else
        {
            arr.Add(new cArrayList("@cm_no", hdcm.Value.ToString()));
        }

        arr.Add(new cArrayList("@ag_no", hdhoag.Value));
        arr.Add(new cArrayList("@prop_no", cbagprop.SelectedValue.ToString()));
        arr.Add(new cArrayList("@amount", txagvalue.Text));
        arr.Add(new cArrayList("@isvat", cbvat.SelectedValue.ToString()));
        bll.vInsertCreditMemoAgreement(arr);
        arr.Clear();
        if (hdcm.Value.ToString() == "")
        {
            arr.Add(new cArrayList("@cm_no", Request.Cookies["usr_id"].Value.ToString()));
        }
        else
        {
            arr.Add(new cArrayList("@cm_no", hdcm.Value.ToString()));
        }
        bll.vBindingGridToSp(ref grdag, "sp_tcreditmemo_agreement_get", arr);
    }
    protected void grdag_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbagno = (Label)grdag.Rows[e.RowIndex].FindControl("lbagno");
        HiddenField hdagcm = (HiddenField)grdag.Rows[e.RowIndex].FindControl("hdagcm");
        List<cArrayList> arr = new List<cArrayList>();
        if (hdcm.Value.ToString() == "")
        {
            arr.Add(new cArrayList("@cm_no", Request.Cookies["usr_id"].Value.ToString()));
        }
        else
        {
            arr.Add(new cArrayList("@cm_no", hdcm.Value.ToString()));
        }
        arr.Add(new cArrayList("@ag_no", lbagno.Text));
        bll.vDeleteCreditMemoAgreement(arr);
        arr.Clear();
        if (hdcm.Value.ToString() == "")
        {
            arr.Add(new cArrayList("@cm_no", Request.Cookies["usr_id"].Value.ToString()));
        }
        else
        {
            arr.Add(new cArrayList("@cm_no", hdcm.Value.ToString()));
        }
        bll.vBindingGridToSp(ref grdag, "sp_tcreditmemo_agreement_get", arr);
    }
    protected void btlookupag_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@ag_no", hdhoag.Value.ToString()));
        bll.vBindingComboToSp(ref cbagprop, "tcontract_ka_proposal_get", "proposal_no", "proposal_no", arr);
    }
    protected void btnaddbr_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();

        if (hdcm.Value.ToString() == "")
        {
            arr.Add(new cArrayList("@cm_no", Request.Cookies["usr_id"].Value.ToString()));
        }
        else
        {
            arr.Add(new cArrayList("@cm_no", hdcm.Value.ToString()));
        }

        arr.Add(new cArrayList("@salespoint_cd", cbbranch.SelectedValue.ToString()));
        arr.Add(new cArrayList("@cusgrcd", cbbrcustomer.SelectedValue.ToString()));
        arr.Add(new cArrayList("@amount", txbrvalue.Text));
        bll.vInsertCreditMemoTransfer(arr);
        arr.Clear();
        if (hdcm.Value.ToString() == "")
        {
            arr.Add(new cArrayList("@cm_no", Request.Cookies["usr_id"].Value.ToString()));
        }
        else
        {
            arr.Add(new cArrayList("@cm_no", hdcm.Value.ToString()));
        }
        bll.vBindingGridToSp(ref grdbr, "sp_tcreditmemo_transfer_get", arr);
    }
    protected void grdbr_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbbrcm = (Label)grdbr.Rows[e.RowIndex].FindControl("lbbrcm");
        HiddenField hdbrcm = (HiddenField)grdbr.Rows[e.RowIndex].FindControl("hdbrcm");
        List<cArrayList> arr = new List<cArrayList>();
        if (hdcm.Value.ToString() == "")
        {
            arr.Add(new cArrayList("@cm_no", Request.Cookies["usr_id"].Value.ToString()));
        }
        else
        {
            arr.Add(new cArrayList("@cm_no", hdcm.Value.ToString()));
        }
        arr.Add(new cArrayList("@salespoint_cd", lbbrcm.Text));
        bll.vDeleteCreditMemoTransfer(arr);
        arr.Clear();
        if (hdcm.Value.ToString() == "")
        {
            arr.Add(new cArrayList("@cm_no", Request.Cookies["usr_id"].Value.ToString()));
        }
        else
        {
            arr.Add(new cArrayList("@cm_no", hdcm.Value.ToString()));
        }
        bll.vBindingGridToSp(ref grdbr, "sp_tcreditmemo_transfer_get", arr);
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_mstCM.aspx");
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        string sCM = "";
        List<cArrayList> arr = new List<cArrayList>();
        try
        {
            if (btsave.Text == "Save")
            {
                if (txcmrefno.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Insert HOCM Reference NO','Add Reference HOCM','warning');", true);
                    return;
                }
                System.Data.SqlClient.SqlDataReader rs = null;
                arr.Clear();
                arr.Add(new cArrayList("@sys", "CreditMemo"));
                arr.Add(new cArrayList("@sysno", ""));
                bll.vGetContractNo(arr, ref rs);
                while (rs.Read())
                {
                    sCM = rs["generated"].ToString();
                }
                lbhocm_no.Text = sCM;
                arr.Clear();
                arr.Add(new cArrayList("@cm_no", sCM));
                arr.Add(new cArrayList("@cm_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@cm_ref_no", txcmrefno.Text));
                arr.Add(new cArrayList("@cm_dt", DateTime.ParseExact(dtCMDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@created_by", Request.Cookies["usr_id"].Value.ToString()));
                bll.vInsertCreditMemoHO(arr);
                btupload_Click(sender, e);
                grdch.Columns[5].Visible = false;
                grdbt.Columns[5].Visible = false;
                grdag.Columns[5].Visible = false;
                grdbr.Columns[3].Visible = false;
                btsave.Visible = false;
                btnaddch.Visible = false;
                btnaddbt.Visible = false;
                btnaddcd.Visible = false;
                btnaddag.Visible = false;
                btnaddbr.Visible = false;
                btupload.Visible = false;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Credit Memo has been saved ("+sCM+").',' Credit Memo.','success');", true);
                return;
            }
            else
            {
                arr.Clear();
                arr.Add(new cArrayList("@cm_no", hdcm.Value.ToString()));
                arr.Add(new cArrayList("@cm_ref_no", txcmrefno.Text));
                arr.Add(new cArrayList("@usr_id", null));
                bll.vUpdateCreditMemoHO(arr);
                btsave.Text = "Save";
                btsave.Visible = false;
                
            }
        }
        catch (Exception ex)
        {
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : SAVE CREDIT MEMO (HO)");
        }
    }

    protected void cbcdbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["hdsalespoint"] = cbcdbranch.SelectedValue.ToString();
        if (cbcdbranch.SelectedValue.ToString() == "NONE")
        {
            txcdcustno.Text = "None";
        }
        else
        {
            txcdcustno.Text = "";
        }
    }
    protected void btlookupcm_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Clear();
        System.Data.SqlClient.SqlDataReader rs = null;
        arr.Add(new cArrayList("@cm_no", hdcm.Value.ToString()));
        bll.vGetmstcreditmemobyno(ref rs, arr);
        while (rs.Read())
        {

            lbhocm_no.Text = rs["cm_no"].ToString();
            txcmrefno.Text = rs["cm_ref_no"].ToString();
            dtCMDate.Text = rs["cm_dt"].ToString();
        }

        rs.Close();
        arr.Clear();
        if (hdcm.Value.ToString() == "")
        {
            arr.Add(new cArrayList("@cm_no", Request.Cookies["usr_id"].Value.ToString()));
        }
        else
        {
            arr.Add(new cArrayList("@cm_no", hdcm.Value.ToString()));
        }
        bll.vBindingGridToSp(ref grdch, "sp_tcreditmemo_cash_get", arr);
        bll.vBindingGridToSp(ref grdbt, "sp_tcreditmemo_bank_get", arr);
        bll.vBindingGridToSp(ref grdag, "sp_tcreditmemo_agreement_get", arr);
        bll.vBindingGridToSp(ref grdbr, "sp_tcreditmemo_transfer_get", arr);
        bll.vBindingGridToSp(ref grdviewdoc, "sp_tcreditmemo_doc_get", arr);
        grdch.Columns[5].Visible = false;
        grdbt.Columns[5].Visible = false;
        grdag.Columns[5].Visible = false;
        grdbr.Columns[3].Visible = false;
        btsave.Visible = false;
        btnaddch.Visible = false;
        btnaddbt.Visible = false;
        btnaddcd.Visible = false;
        btnaddag.Visible = false;
        btnaddbr.Visible = false;
        btupload.Visible = true;
        btdelete.Visible = false;
        btapprove.Visible = true;
        btreject.Visible = true;
        btedit.Visible = true;
    }
    protected void btedit_Click(object sender, EventArgs e)
    {
        if (bll.nCheckAccess("cmedit", Request.Cookies["usr_id"].Value.ToString()) == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To delete this credit memo, Administrator !!','warning');", true);
            return;
        }
        grdch.Columns[5].Visible = true;
        grdbt.Columns[5].Visible = true;
        grdag.Columns[5].Visible = true;
        grdbr.Columns[3].Visible = true;
        btsave.Text = "Update";
        btsave.Visible = true;
        btnaddch.Visible = true;
        btnaddbt.Visible = true;
        btnaddcd.Visible = true;
        btnaddag.Visible = true;
        btnaddbr.Visible = true;
        btupload.Visible = true;
        btdelete.Visible = false;
        btapprove.Visible = false;
        btreject.Visible = false;
        btedit.Visible = false;
    }
    protected void btupload_Click(object sender, EventArgs e)
    {
        try
        {
            string CMCode = "";
            List<cArrayList> arr = new List<cArrayList>();
            foreach (GridViewRow row in grddoc.Rows)
            {
                Label lbdoccode = (Label)row.FindControl("lbdoccode");
                Label lbdocname = (Label)row.FindControl("lbdocname");
                FileUpload upl = (FileUpload)row.FindControl("upl");
                if (upl.HasFile)
                {
                    FileInfo fi = new FileInfo(upl.FileName);
                    string ext = fi.Extension;
                    byte[] fs = upl.FileBytes;
                    //if (fs.Length <= 6000000)
                    //{
                        if ((upl.FileName != "") || (upl.FileName != null))
                        {
                            arr.Clear();
                            if (hdcm.Value.ToString() == "")
                            {
                                CMCode = lbhocm_no.Text;
                            }
                            else
                            {
                                CMCode = hdcm.Value.ToString();
                            }
                            arr.Add(new cArrayList("@cm_no", CMCode));
                            arr.Add(new cArrayList("@doc_cd", lbdoccode.Text));
                            arr.Add(new cArrayList("@fileloc", CMCode + "-" + lbdoccode.Text + ext));
                            arr.Add(new cArrayList("@doc_nm", lbdocname.Text));
                            bll.vInsertCreditMemoDoc(arr);
                            upl.SaveAs(bll.sGetControlParameter("image_path") + "/creditmemo_doc/" + CMCode + "-" + lbdoccode.Text + ext);                            
                        }
                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Image size too big','Image size must be less than 500MB');", true);
                    //    return;
                    //}
                }
                //else
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('No Image Upload','Image must be upload. (" + lbdoccode.Text.ToString() + "_" + lbdocname.Text.ToString()+ ")');", true);
                //    //    return;
                //}
            }
            arr.Clear();
            arr.Add(new cArrayList("@cm_no", CMCode));
            bll.vBindingGridToSp(ref grdviewdoc, "sp_tcreditmemo_doc_get", arr);
        }
        catch (Exception ex)
        {
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : UPLOAD DOCUMENT CREDIT MEMO (HO)");
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('upload error','"+ ex.ToString() +"');", true);
            //return;
        }
        
    }

    protected void btdelete_Click(object sender, EventArgs e)
    {
        if (bll.nCheckAccess("cmdel", Request.Cookies["usr_id"].Value.ToString()) == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To delete this credit memo, Administrator !!','warning');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Clear();
        arr.Add(new cArrayList("@cm_no", hdcm.Value.ToString()));
        arr.Add(new cArrayList("@cm_ref_no", "Delete"));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vUpdateCreditMemoHO(arr);
        btdelete.Visible = false;
        btsave.Visible = false;
        btedit.Visible = false;
        btapprove.Visible = false;
        btreject.Visible = false;
        btnew.Visible = true;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Credit Memo has been deleted (" + hdcm.Value.ToString() + ").',' Credit Memo.','success');", true);
        return;

    }
    protected void btapprove_Click(object sender, EventArgs e)
    {
        if (bll.nCheckAccess("cmapp", Request.Cookies["usr_id"].Value.ToString()) == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To approve this credit memo, Administrator !!','warning');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Clear();
        arr.Add(new cArrayList("@cm_no", hdcm.Value.ToString()));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@cm_ref_no", "Approve"));
        bll.vUpdateCreditMemoHO(arr);
        btdelete.Visible = false;
        btsave.Visible = false;
        btedit.Visible = false;
        btapprove.Visible = false;
        btreject.Visible = false;
        btnew.Visible = true;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Credit Memo has been approved (" + hdcm.Value.ToString() + ").',' Credit Memo.','success');", true);
        return;
    }
    protected void btreject_Click(object sender, EventArgs e)
    {
        if (bll.nCheckAccess("cmapp", Request.Cookies["usr_id"].Value.ToString()) == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To reject this credit memo, Administrator !!','warning');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Clear();
        arr.Add(new cArrayList("@cm_no", hdcm.Value.ToString()));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@cm_ref_no", "Reject"));
        bll.vUpdateCreditMemoHO(arr);
        btdelete.Visible = false;
        btsave.Visible = false;
        btedit.Visible = false;
        btnew.Visible = true;
        btapprove.Visible = false;
        btreject.Visible = false;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Credit Memo has been rejected (" + hdcm.Value.ToString() + ").',' Credit Memo.','success');", true);
        return;
    }
    protected void grdag_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdag.PageIndex = e.NewPageIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Clear();
        if (hdcm.Value.ToString() == "")
        {
            arr.Add(new cArrayList("@cm_no", Request.Cookies["usr_id"].Value.ToString()));
        }
        else
        {
            arr.Add(new cArrayList("@cm_no", hdcm.Value.ToString()));
        }
        bll.vBindingGridToSp(ref grdag, "sp_tcreditmemo_agreement_get", arr);
    }
}