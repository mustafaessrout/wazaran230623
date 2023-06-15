using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class bankcleareance : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                string sAccNoto = string.Empty;
                System.Data.SqlClient.SqlDataReader rs = null;
                string sID = Request.QueryString["id"];
                string sSp = Request.Cookies["sp"].Value.ToString();
                bll.vBindingComboToSp(ref cbbankto, "sp_tmst_bankaccount_get", "acc_no", "bank_desc");
                dtclear.Text = Request.Cookies["waz_dt"].Value.ToString();
                lbcreated.Text = Request.Cookies["usr_id"].Value.ToString(); //''bll.sGetEmployeeName(Request.Cookies["usr_id"].Value.ToString());
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@deposit_id", sID));
                arr.Add(new cArrayList("@salespointcd", sSp));
                bll.vGetBankDeposit(arr, ref rs);
                bll.vBindingFieldValueToCombo(ref ddlPaymentAttribute, "payment_att", false);
                while (rs.Read())
                {
                    lbcashouttype.Text = rs["cashout_typ_nm"].ToString();
                    lbamount.Text = rs["amt"].ToString();
                    lbdepositno.Text = rs["deposit_no"].ToString();
                    lbbankaccountno.Text = rs["acc_no"].ToString();
                    lbbankname.Text = bll.vLookUp("select bank_nm from tmst_bank where bank_cd='" + rs["bank_cd"] + "'");
                    lbcust.Text = bll.vLookUp("select cust_cd +':'+cust_nm from tmst_customer where cust_cd='" + rs["cust_cd"].ToString() + "'");
                    sAccNoto = rs["accnoto"].ToString();//deposit_typ
                    lblPaymentMode.Text = rs["deposit_typ"].ToString();
                    aDownload.HRef = bll.vLookUp("select dbo.fn_getcontrolparameter('link_branch')") + "\\images\\" + rs["attachment"].ToString();
                    lblDownload.Text = rs["attachment"].ToString();
                    lblPaymentNo.Text = rs["ref_no"].ToString();
                    ddlPaymentAttribute.SelectedValue = bll.vLookUp("select paymentAttribute from tpayment_info where payment_no='" + rs["ref_no"].ToString() + "' and salespointcd='" + sSp + "'");
                    lblEmp.Text = rs["emp_nm"].ToString();
                    lblCust.Text = rs["cust_nm"].ToString();
                }
                rs.Close();
                if (sAccNoto != "")
                {
                    cbbankto.SelectedValue = sAccNoto;
                    cbbankto.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_bankcleareance");
                Response.Redirect("fm_ErrorPage.aspx");
            }
        }
    }
    protected void btcancel_Click(object sender, EventArgs e)
    {
        try
        {
            if (txremark.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "sweetAlert('Remark for reject reason can not empty','Remark','warning');", true);
                return;
            }
            List<cArrayList> arr = new List<cArrayList>();
            string sDepositID = Request.QueryString["id"];
            arr.Add(new cArrayList("@deposit_id", sDepositID));
            arr.Add(new cArrayList("@dep_sta_id", "R"));
            arr.Add(new cArrayList("@remark", txremark.Text));
            arr.Add(new cArrayList("@clear_dt", DateTime.ParseExact(dtclear.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vUpdateBankDeposit(arr);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ow", "window.opener.RefreshData();window.close();", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_bankcleareance");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btyes_Click(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            if ((lblPaymentMode.Text == "CQ" || lblPaymentMode.Text == "BT" || lblPaymentMode.Text == "CH") && lblDownload.Text == "N/A" && upl.FileName == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alr", "sweetAlert('Please upload document for payment','Upload document','warning');", true);
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please upload document for payment','warning');", true);
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please upload document for payment','Upload Documents','warning');", true);
                return;
            }

            else if ((lblPaymentMode.Text.ToString() == "BT" || (lblPaymentMode.Text.ToString() == "CQ")))
            {
                if (ddlPaymentAttribute.SelectedValue == "OT")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select payment status  !','Payment Status','warning');", true);
                    return;
                }
            }


            if ((lblPaymentMode.Text == "CQ" || lblPaymentMode.Text == "BT" || lblPaymentMode.Text == "CH") && lblDownload.Text == "N/A")
            {
                List<cArrayList> arrInfo = new List<cArrayList>();
                FileInfo fn = new FileInfo(upl.FileName);
                upl.SaveAs(bll.sGetControlParameter("image_path") + @"\cashout\" + Convert.ToString(lblPaymentNo.Text) + fn.Extension);
                arrInfo.Add(new cArrayList("@docFile", Convert.ToString(lblPaymentNo.Text) + fn.Extension));

                arrInfo.Add(new cArrayList("@payment_no", lblPaymentNo.Text));
                arrInfo.Add(new cArrayList("@initamt", null));
                if ((lblPaymentMode.Text == "BT" || (lblPaymentMode.Text == "CQ") || (lblPaymentMode.Text == "CH")))
                {
                    arrInfo.Add(new cArrayList("@paymentAttribute", ddlPaymentAttribute.SelectedValue));
                }
                else
                {
                    arrInfo.Add(new cArrayList("@paymentAttribute", null));
                }
                arrInfo.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vInsertPaymentInfo(arrInfo);
            }
            else { arr.Add(new cArrayList("@docFile", null)); }

            if ((lblPaymentMode.Text.ToString() == "BT" || (lblPaymentMode.Text.ToString() == "CQ") || (lblPaymentMode.Text.ToString() == "CH")) && ddlPaymentAttribute.SelectedValue != "OT")
            {
                arr.Add(new cArrayList("@paymentAttribute", ddlPaymentAttribute.SelectedValue));
            }
            else
            {
                arr.Add(new cArrayList("@paymentAttribute", null));
            }
            string sDepositID = Request.QueryString["id"];

            arr.Add(new cArrayList("@deposit_id", sDepositID));
            arr.Add(new cArrayList("@dep_sta_id", "C"));
            arr.Add(new cArrayList("@remark", txremark.Text));
            arr.Add(new cArrayList("@clear_dt", DateTime.ParseExact(dtclear.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vUpdateBankDeposit(arr);
            arr.Clear();
            string sPaymentNo = bll.vLookUp("select ref_no from tbank_deposit where deposit_id='" + sDepositID + "'");
            arr.Add(new cArrayList("@trn_typ", "BANKCLEAR"));
            arr.Add(new cArrayList("@refno", sPaymentNo));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBatchAccTransactionLog(arr);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "window.opener.RefreshData();window.close();", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cls", "window.opener.RefreshData();window.close();", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_bankcleareance");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void cbbankto_SelectedIndexChanged(object sender, EventArgs e)
    {
        //List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@bank_cd", cbbankto.SelectedValue.ToString()));
        //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        //bll.vBindingComboToSp(ref cbaccto, "sp_tmst_bankaccount_getbybankcd","acc_no","acc_no", arr);
    }
}