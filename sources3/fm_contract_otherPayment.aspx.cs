using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_contract_otherPayment : System.Web.UI.Page
{
    cbll bll = new cbll();
    cdal cdl = new cdal();
    Utitlity ut = new Utitlity();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindControl();
            cbpaymenttype_SelectedIndexChanged(sender, e);
        }
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
    protected void btShowInvoice_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        // arr.Add(new cArrayList("@salesman_cd", contextKey));
        arr.Add(new cArrayList("@cust_cd", hdcust.Value));
        bll.vBindingGridToSp(ref grd, "sp_tcontract_invoice_getByCust", arr);

        arr.Clear();
        arr.Add(new cArrayList("@cust_cd", hdcust.Value));
        bll.vBindingGridToSp(ref grdDetails, "sp_tcontract_otherPayment_getByCust", arr);
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lblcontract_no = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lblcontract_no");
        if (lblcontract_no.Text != "")
        {
            SqlDataReader rs = null;
            List<cArrayList> arr = new List<cArrayList>();
            // arr.Add(new cArrayList("@salesman_cd", contextKey));
            arr.Add(new cArrayList("@contract_no", lblcontract_no.Text));
            bll.vGetContractByNumber(ref rs, arr);
            while (rs.Read())
            {
                lblContractNumber.Text = rs["contract_no"].ToString();
                lblprop_no.Text = rs["prop_no"].ToString();
                lblSalesman.Text = rs["empName"].ToString();
                llbCustomer.Text = rs["custName"].ToString();
                hdfSalesman.Value = rs["salesman_cd"].ToString();
                lblstatusv.Text = rs["statusv"].ToString();
                lblBalance.Text = rs["balance"].ToString();
            }
            rs.Close();

        }
    }

    void BindControl()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@reasn_typ", Convert.ToString("conpay")));
        bll.vBindingComboToSp(ref ddlReason, "sp_tmst_reason_get", "reasn_cd", "reasn_nm", arr);

        bll.vBindingFieldValueToCombo(ref cbpaymenttype, "payment_typ");
        cbpaymenttype.Items.Remove(cbpaymenttype.Items[2]);
        cbpaymenttype.Items.Remove(cbpaymenttype.Items[3]);

        lblContractNumber.Text = string.Empty;
        lblBalance.Text = string.Empty;
        txtPayment.Text = string.Empty;
        hdcust.Value = string.Empty;
        hdfSalesman.Value = string.Empty;
        dtcheque.Text = string.Empty;
        txcheque.Text = string.Empty;
        txtPayment.Text = string.Empty;
        txtRemarks.Text = string.Empty;

        arr.Clear();
        arr.Add(new cArrayList("@cust_cd", hdcust.Value));
        bll.vBindingGridToSp(ref grdDetails, "sp_tcontract_otherPayment_getByCust", arr);

    }
    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        try
        {
            string topID = string.Empty;
            if (Convert.ToString(cbpaymenttype.SelectedValue) != "CH")
            {
                if (Convert.ToString(dtcheque.Text) == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select Cheque/BT Date','Cheque/BT Date','warning');", true);
                    return;
                }
                else if (Convert.ToString(txcheque.Text) == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Cheque Number required','Cheque Number required','warning');", true);
                    return;
                }
            }
            else if (Convert.ToString(lblContractNumber.Text) == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Contract required','Contract required','warning');", true);
                return;
            }
            else if (Convert.ToString(txtPayment.Text) == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Payment amount required','Payment amount required','warning');", true);
                return;
            }
            else if (Convert.ToDecimal(txtPayment.Text) <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Payment amount must be greater than zero','Payment amount wrong','warning');", true);
                return;
            }
            else if (Convert.ToDecimal(lblBalance.Text) < Convert.ToDecimal(txtPayment.Text))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Payment greater than balance','Payment amount wrong','warning');", true);
                return;
            }

            List<cArrayList> arr = new List<cArrayList>();

            arr.Add(new cArrayList("@salespointcd", Convert.ToString(Request.Cookies["sp"].Value)));
            arr.Add(new cArrayList("@contract_no", Convert.ToString(lblContractNumber.Text)));
            arr.Add(new cArrayList("@contractAmount", Convert.ToDecimal(lblBalance.Text)));
            arr.Add(new cArrayList("@contractAmountPay", Convert.ToDecimal(txtPayment.Text)));
            arr.Add(new cArrayList("@cust_cd", Convert.ToString(hdcust.Value)));
            arr.Add(new cArrayList("@salesman_cd", Convert.ToString(hdfSalesman.Value)));
            arr.Add(new cArrayList("@payment_typ", Convert.ToString(cbpaymenttype.SelectedValue)));
            arr.Add(new cArrayList("@bank_cd", Convert.ToString(cbbank.SelectedValue)));
            if (Convert.ToString(cbpaymenttype.SelectedValue) != "CH")
            {
                arr.Add(new cArrayList("@chequedue_dt", DateTime.ParseExact(dtcheque.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@chequeNumber", Convert.ToString(txcheque.Text)));
            }
            else
            {
                arr.Add(new cArrayList("@chequedue_dt", null));
                arr.Add(new cArrayList("@chequeNumber", null));
            }
            arr.Add(new cArrayList("@reason_cd", Convert.ToString(ddlReason.SelectedValue)));
            arr.Add(new cArrayList("@createdBy", Convert.ToString(Request.Cookies["usr_id"].Value)));
            arr.Add(new cArrayList("@contractAmountVatPay", Convert.ToString(0)));
            arr.Add(new cArrayList("@remarks", Convert.ToString(txtRemarks.Text)));

            bll.vInsertContract_otherPayment(arr, ref topID);



            if (topID == "-2")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('payment already available.','Ref no already available. ','warning');", true);
                return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Data Save Successfully.','Data Save Successfully.','success');", true);
                arr.Clear();
                // arr.Add(new cArrayList("@salesman_cd", contextKey));
                arr.Add(new cArrayList("@cust_cd", hdcust.Value));
                bll.vBindingGridToSp(ref grdDetails, "sp_tcontract_otherPayment_getByCust", arr);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Error during save','Error during save','warning');", true);

            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('" + ex.Message.ToString() + ex.InnerException.ToString() + "','" + ex.Message.ToString() + ex.InnerException.ToString() + "','warning');", true);
            ut.Logs("", "Account", "CN DN Adjustment", "fm_acccndn", "grd_RowUpdating", "Exception", ex.Message + ex.InnerException);
        }

    }
    protected void cbpaymenttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbpaymenttype.SelectedValue.ToString() == "CH")
        {
            dtcheque.CssClass = "form-control ro"; //dtdue.CssClass = "form-control ro";
            txcheque.CssClass = "form-control ro"; cbbank.CssClass = "form-control ro";
            cbbank.Items.Clear();
        }
        else
        {
            dtcheque.CssClass = "form-control";// dtdue.CssClass = "form-control";
            txcheque.CssClass = "form-control"; cbbank.CssClass = "form-control-static";
            bll.vBindingComboToSp(ref cbbank, "sp_tmst_bankaccount_get", "acc_no", "bank_desc");

        }
    }
}