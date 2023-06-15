using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_contractKA : System.Web.UI.Page
{
    cbll bll = new cbll();
    public string globalContractNo = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            lbhoag_no.Text = "NEW";
            // Periode
            bll.vBindingComboToSp(ref ddYear, "sp_tmst_year_getbyclaim", "fld_valu", "fld_valu");
            string currentMonth = DateTime.Now.Month.ToString();
            string currentYear = DateTime.Now.Year.ToString();
            if (currentMonth.Length == 1)
            {
                currentMonth = "0" + currentMonth;
            }
            ddMonth.SelectedValue = currentMonth;
            ddYear.SelectedValue = currentYear;
            // Session Configuration
            Session["hdcontract"] = "";
            hdcontract.Value = "";

            arr.Add(new cArrayList("@promokind", cbpromokind.SelectedValue.ToString()));
            bll.vBindingComboToSp(ref cbpromogroup, "sp_tmst_promotion_get", "promo_cd", "promo_nm", arr);
            bll.vBindingFieldValueToCombo(ref cbCusGrp, "cusgrcd");
            bll.vBindingSalespointToCombo(ref cbbranch);
            cbbranch.Items.Insert(0, "ALL | SBTC KINGDOM");
            budgetProposal.Attributes.Add("style", "display:none");
            cbCustomer_SelectedIndexChanged(sender, e);
            cbpromogroup_SelectedIndexChanged(sender, e);
            arr.Clear();
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
            bll.vDeleteContractKASalespoint(arr);
            bll.vDeleteContractKAAgreement(arr);
            bll.vDeleteContractKAProposal(arr);
            bll.vDeleteContractKACusgrcd(arr);
            bll.vDeleteContractKACustomer(arr);
            btsave.Visible = true;
            btupdate.Visible = false;
            btdelete.Visible = false;
            btapprove.Visible = false;
            if (hdcontract.Value.ToString() == "")
            {
                this.globalContractNo = Request.Cookies["usr_id"].Value.ToString();
            }
            else
            {
                this.globalContractNo = hdcontract.Value.ToString();
            }
            dtCrDate.Text = String.Format("{0:d/MM/yyyy}", DateTime.Now);
        }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetListCustomer(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        System.Data.SqlClient.SqlDataReader rs = null;
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        List<string> lCust = new List<string>();
        string sCust = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cust_cd", prefixText));
        if (HttpContext.Current.Session["hdcontract"].ToString() == "")
        {
            arr.Add(new cArrayList("@contract_no", HttpContext.Current.Request.Cookies["usr_id"].Value.ToString()));
        }
        else
        {
            arr.Add(new cArrayList("@contract_no", HttpContext.Current.Session["hdcontract"].ToString()));
        }
        bll.vSearchMstCustomerAG(arr, ref rs);
        while (rs.Read())
        {
            sCust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + "-" + rs["cust_nm"].ToString(), rs["cust_cd"].ToString());
            lCust.Add(sCust);
        }
        rs.Close();
        return (lCust.ToArray());
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetListProposal(string prefixText, int count, string contextKey)
    {
        HttpCookie cook;
        cook = HttpContext.Current.Request.Cookies["sp"];
        cbll bll = new cbll();
        List<string> lItem = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string sItem = string.Empty;
        System.Data.SqlClient.SqlDataReader rs = null;
        arr.Add(new cArrayList("@prop_no", prefixText));
        bll.vSearchMstProposalAll(arr, ref rs);
        while (rs.Read())
        {
            sItem = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["prop_no"].ToString(), rs["prop_no"].ToString());
            lItem.Add(sItem);

        }
        rs.Close();
        return (lItem.ToArray());
    }

    protected void btnaddbranch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (cbbranch.SelectedValue.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Salespoint Not yet selcted','select Salespoint','warning');", true);
            return;
        }
        else
        {
            if (hdcontract.Value.ToString() == "")
            {
                arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
            }
            else
            {
                arr.Add(new cArrayList("@contract_no", hdcontract.Value.ToString()));
            }
            arr.Add(new cArrayList("@salespoint_cd", cbbranch.SelectedValue.ToString()));
            bll.vInsertContractKASalespoint(arr);
            arr.Clear();
            if (hdcontract.Value.ToString() == "")
            {
                arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
            }
            else
            {
                arr.Add(new cArrayList("@contract_no", hdcontract.Value.ToString()));
            }
            bll.vBindingGridToSp(ref grdbranch, "sp_tcontract_ka_salespoint_get", arr);
        }
    }
    protected void cbCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbCustomer.SelectedValue.ToString() == "C")
        {
            txCustomer.Attributes.Remove("style");
            cbCusGrp.Attributes.Add("style", "display:none");
        }
        else
        {
            txCustomer.Attributes.Add("style", "display:none");
            cbCusGrp.Attributes.Remove("style");
        }
    }
    protected void grdbranch_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lblsalespointcd = (Label)grdbranch.Rows[e.RowIndex].FindControl("lblslspointcd");
        List<cArrayList> arr = new List<cArrayList>();
        if (hdcontract.Value.ToString() == "")
        {
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
        }
        else
        {
            arr.Add(new cArrayList("@contract_no", hdcontract.Value.ToString()));
        }
        arr.Add(new cArrayList("@salespoint_cd", lblsalespointcd.Text));
        bll.vDeleteContractKASalespoint(arr);
        arr.Clear();
        if (hdcontract.Value.ToString() == "")
        {
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
        }
        else
        {
            arr.Add(new cArrayList("@contract_no", hdcontract.Value.ToString()));
        }
        bll.vBindingGridToSp(ref grdbranch, "sp_tcontract_ka_salespoint_get", arr);
    }
    protected void cbpromokind_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@promokind", cbpromokind.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbpromogroup, "sp_tmst_promotion_get", "promo_cd", "promo_nm", arr);
        cbpromogroup_SelectedIndexChanged(sender, e);
    }

    protected void cbpromogroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@promo_cd", cbpromogroup.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbpromotype, "sp_tpromotion_dtl_get", "promo_typ", "promotyp_nm", arr);
    }
    protected void grdAgreement_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        HiddenField lbcontractagno = (HiddenField)grdAgreement.Rows[e.RowIndex].FindControl("lbcontractagno");
        HiddenField lbcontractno = (HiddenField)grdAgreement.Rows[e.RowIndex].FindControl("lbcontractno");
        HiddenField lbagsbtcno = (HiddenField)grdAgreement.Rows[e.RowIndex].FindControl("lbagsbtcno");
        HiddenField lbagcustno = (HiddenField)grdAgreement.Rows[e.RowIndex].FindControl("lbagcustno");

        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@contract_no", lbcontractno.Value.ToString()));
        arr.Add(new cArrayList("@contract_ag_no", lbcontractagno.Value.ToString()));
        bll.vDeleteContractKAAgreement(arr);
        arr.Clear();
        arr.Add(new cArrayList("@contract_no", lbcontractno.Value.ToString()));
        bll.vBindingGridToSp(ref grdbranch, "sp_tcontract_ka_salespoint_get", arr);
    }
    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        bool checkSl = false, checkContract = false;
        string contractAg = ""; int sequence = 0;
        foreach (GridViewRow row in grdAgreement.Rows)
        {
            CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
            HiddenField lbcontractagno = (HiddenField)row.FindControl("lbcontractagno");
            HiddenField lbcontractno = (HiddenField)row.FindControl("lbcontractno");
            HiddenField lbagsbtcno = (HiddenField)row.FindControl("lbagsbtcno");
            HiddenField lbagcustno = (HiddenField)row.FindControl("lbagcustno");
            if (chckrw.Checked == true)
            {
                sequence = sequence + 1;
                if (contractAg != "")
                {
                    if (contractAg == lbcontractagno.Value.ToString())
                    {
                        checkContract = false;
                        checkSl = true;
                    }
                    else
                    {
                        checkContract = true;
                        checkSl = false;
                    }
                }
                else
                {
                    checkContract = false;
                    checkSl = true;
                }
                contractAg = lbcontractagno.Value.ToString();
            }
        }

        if (checkSl == false)
        {
            budgetProposal.Attributes.Add("style", "display:none");
        }
        else
        {
            budgetProposal.Attributes.Remove("style");
        }
        if (sequence > 1)
        {
            foreach (GridViewRow row in grdAgreement.Rows)
            {
                CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
                chckrw.Checked = false;
            }
            budgetProposal.Attributes.Add("style", "display:none");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Agreement Must be select only 1 (one)','Agreement HO','warning');", true);
            return;
        }
    }
    protected void btnaddprop_Click(object sender, EventArgs e)
    {
        btnaddprop.Attributes.Add("style", "display:none");
        List<cArrayList> arr = new List<cArrayList>();
        if (txbgtpropno.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Proposal No can not be empty!!!','Insert Proposal No','warning');", true);
            return;
        }
        if (txbgtamt.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Amount can not be empty!!!','Insert Amount','warning');", true);
            return;
        }

        foreach (GridViewRow row in grdAgreement.Rows)
        {
            CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
            HiddenField lbcontractagno = (HiddenField)row.FindControl("lbcontractagno");
            HiddenField lbcontractno = (HiddenField)row.FindControl("lbcontractno");
            HiddenField lbagsbtcno = (HiddenField)row.FindControl("lbagsbtcno");
            HiddenField lbagcustno = (HiddenField)row.FindControl("lbagcustno");
            if (chckrw.Checked == true)
            {
                arr.Add(new cArrayList("@contract_no", lbcontractno.Value.ToString()));
                arr.Add(new cArrayList("@contract_ag_no", lbcontractagno.Value.ToString()));
                arr.Add(new cArrayList("@contract_sbtc_no", lbagsbtcno.Value.ToString()));
                arr.Add(new cArrayList("@contract_cust_no", lbagcustno.Value.ToString()));
                arr.Add(new cArrayList("@proposal_no", txbgtpropno.Text));
                arr.Add(new cArrayList("@amount", txbgtamt.Text));
                arr.Add(new cArrayList("@created_dt", DateTime.ParseExact(dtCrDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                if (cbvat.SelectedValue.ToString() == "0") { arr.Add(new cArrayList("@vat", 0.00)); } else { arr.Add(new cArrayList("@vat", 0.05)); }
                
                bll.vInsertContractKAProposal(arr);
                arr.Clear();
                if (hdcontract.Value.ToString() == "")
                {
                    arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
                }
                else
                {
                    arr.Add(new cArrayList("@contract_no", hdcontract.Value.ToString()));
                }
                arr.Add(new cArrayList("@contract_ag_no", lbcontractagno.Value.ToString()));
                arr.Add(new cArrayList("@contract_sbtc_no", lbagsbtcno.Value.ToString()));
                arr.Add(new cArrayList("@contract_cust_no", lbagcustno.Value.ToString()));
                bll.vBindingGridToSp(ref grdProposal, "sp_tcontract_ka_proposal_get", arr);
                arr.Clear();
                if (hdcontract.Value.ToString() == "")
                {
                    arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
                }
                else
                {
                    arr.Add(new cArrayList("@contract_no", hdcontract.Value.ToString()));
                }
                bll.vBindingGridToSp(ref grdAgreement, "sp_tcontract_ka_agreement_get", arr);
                chckrw.Checked = true;
            }
        }
        txbgtpropno.Text = "";
        txbgtamt.Text = "";
        btnaddprop.Attributes.Remove("Style");
    }
    protected void grdProposal_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        HiddenField lbcontractagno = (HiddenField)grdProposal.Rows[e.RowIndex].FindControl("lbcontractagno");
        HiddenField lbcontractno = (HiddenField)grdProposal.Rows[e.RowIndex].FindControl("lbcontractno");
        HiddenField lbagsbtcno = (HiddenField)grdProposal.Rows[e.RowIndex].FindControl("lbagsbtcno");
        HiddenField lbagcustno = (HiddenField)grdProposal.Rows[e.RowIndex].FindControl("lbagcustno");
        Label lbpropno = (Label)grdProposal.Rows[e.RowIndex].FindControl("lbpropno");
        List<cArrayList> arr = new List<cArrayList>();

        arr.Add(new cArrayList("@contract_no", lbcontractno.Value.ToString()));
        arr.Add(new cArrayList("@contract_ag_no", lbcontractagno.Value.ToString()));
        arr.Add(new cArrayList("@proposal_no", lbpropno.Text.ToString()));
        bll.vDeleteContractKAProposal(arr);

        arr.Clear();
        arr.Add(new cArrayList("@contract_no", lbcontractno.Value.ToString()));
        arr.Add(new cArrayList("@contract_ag_no", lbcontractagno.Value.ToString()));
        arr.Add(new cArrayList("@contract_sbtc_no", lbagsbtcno.Value.ToString()));
        arr.Add(new cArrayList("@contract_cust_no", lbagcustno.Value.ToString()));
        bll.vBindingGridToSp(ref grdProposal, "sp_tcontract_ka_proposal_get", arr);
        arr.Clear();
        arr.Add(new cArrayList("@contract_no", lbcontractno.Value.ToString()));
        bll.vBindingGridToSp(ref grdAgreement, "sp_tcontract_ka_agreement_get", arr);

    }
    protected void btsearchhoag_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ops", "popupwindow('fm_lookupcontractKA.aspx');", true);
    }

    protected void btlookcontract_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        this.globalContractNo = hdcontract.Value.ToString();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Clear();
        System.Data.SqlClient.SqlDataReader rs = null;
        arr.Add(new cArrayList("@contract_no", hdcontract.Value.ToString()));
        bll.vGetmstcontractKAbyno(ref rs, arr);
        while (rs.Read())
        {

            lbhoag_no.Text = rs["contract_no"].ToString();
            cbCustomer.SelectedValue = rs["rdcust"].ToString();
            cbCustomer_SelectedIndexChanged(sender, e);
            ddMonth.SelectedValue = rs["tx_month"].ToString();
            ddYear.SelectedValue = rs["tx_year"].ToString();
            //if (rs["rdcust"].ToString() == "G")
            //{
            //    cbCusGrp.SelectedValue = rs["cust_cd"].ToString();
            //}
            //else
            //{
            //    txCustomer.Text = rs["customer"].ToString(); ;
            //}
            lblocfile.Text = rs["fileloc"].ToString();
            hpfile_nm.NavigateUrl = "/images/contractka_doc/" + rs["fileloc"].ToString();
        }

        rs.Close();
        arr.Clear();
        arr.Add(new cArrayList("@contract_no", hdcontract.Value.ToString()));
        bll.vBindingGridToSp(ref grdbranch, "sp_tcontract_ka_salespoint_get", arr);
        if (cbCustomer.SelectedValue.ToString() == "C")
        {
            bll.vBindingGridToSp(ref grdcust, "sp_tcontract_ka_cust_get", arr);
        }
        else
        {
            bll.vBindingGridToSp(ref grdcusgrcd, "sp_tcontract_ka_cusgrcd_get", arr);
        }

        bll.vBindingGridToSp(ref grdAgreement, "sp_tcontract_ka_agreement_get", arr);

        if (grdAgreement.Rows.Count > 0)
        {
            budgetProposal.Attributes.Remove("style");
        }
        else
        {
            budgetProposal.Attributes.Add("style", "display:none");
        }

        bll.vBindingGridToSp(ref grdProposal, "sp_tcontract_ka_proposal_get", arr);

        btnaddbranch.Visible = false;
        btnaddAg.Visible = false;
        btnaddprop.Visible = false;
        btsave.Visible = false;
        btupdate.Visible = true;
        btdelete.Visible = true;
        btapprove.Visible = true;
        hpfile_nm.Visible = true;
        upl.Visible = false;
        //this.grdbranch.AutoGenerateDeleteButton = false;
        //this.grdAgreement.AutoGenerateDeleteButton = false;
        //this.grdProposal.AutoGenerateDeleteButton = false;
        Session["hdcontract"] = hdcontract.Value.ToString();
    }
    protected void btlookproposal_Click(object sender, EventArgs e)
    {
        string budgetProposal = bll.vLookUp("select isnull(budget_limit,0) from tmst_proposal where prop_no='" + txbgtpropno.Text + "'");
        txbgtprop.Text = budgetProposal;
    }
    protected void btnaddcustomer_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        string[] custName;
        if (cbCustomer.SelectedValue.ToString() == "C")
        {
            if (txCustomer.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Customer Must be selected','select Customer','warning');", true);
                return;
            }
            else
            {
                custName = txCustomer.Text.ToString().Split('-');
                arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
                arr.Add(new cArrayList("@cust_nm", custName[1].ToString()));
            }
        }
        else
        {
            if (cbCusGrp.SelectedValue.ToString() == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Customer Must be selected','select Customer','warning');", true);
                return;
            }
            else
            {
                arr.Add(new cArrayList("@cust_cd", cbCusGrp.SelectedValue.ToString()));
                arr.Add(new cArrayList("@cust_nm", ""));
            }
        }

        if (hdcontract.Value.ToString() == "")
        {
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
        }
        else
        {
            arr.Add(new cArrayList("@contract_no", hdcontract.Value.ToString()));
        }
        bll.vInsertContractKACustomer(arr);
        arr.Clear();
        if (hdcontract.Value.ToString() == "")
        {
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
        }
        else
        {
            arr.Add(new cArrayList("@contract_no", hdcontract.Value.ToString()));
        }

        if (cbCustomer.SelectedValue.ToString() == "C")
        {
            bll.vBindingGridToSp(ref grdcust, "sp_tcontract_ka_cust_get", arr);
        }
        else
        {
            bll.vBindingGridToSp(ref grdcusgrcd, "sp_tcontract_ka_cusgrcd_get", arr);
        }
    }
    protected void grdcust_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        HiddenField lbcustcd = (HiddenField)grdcust.Rows[e.RowIndex].FindControl("lbcustcd");
        HiddenField lbsalespointcd = (HiddenField)grdcust.Rows[e.RowIndex].FindControl("lbsalespointcd");
        List<cArrayList> arr = new List<cArrayList>();
        if (hdcontract.Value.ToString() == "")
        {
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
        }
        else
        {
            arr.Add(new cArrayList("@contract_no", hdcontract.Value.ToString()));
        }
        arr.Add(new cArrayList("@cust_cd", lbcustcd.Value.ToString()));
        arr.Add(new cArrayList("@salespointcd", lbsalespointcd.Value.ToString()));
        bll.vDeleteContractKACustomer(arr);
        arr.Clear();
        if (hdcontract.Value.ToString() == "")
        {
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
        }
        else
        {
            arr.Add(new cArrayList("@contract_no", hdcontract.Value.ToString()));
        }
        bll.vBindingGridToSp(ref grdcust, "sp_tcontract_ka_cust_get", arr);
    }
    protected void grdcusgrcd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbcustcode = (Label)grdcusgrcd.Rows[e.RowIndex].FindControl("lbcustcode");
        List<cArrayList> arr = new List<cArrayList>();
        if (hdcontract.Value.ToString() == "")
        {
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
        }
        else
        {
            arr.Add(new cArrayList("@contract_no", hdcontract.Value.ToString()));
        }
        arr.Add(new cArrayList("@cusgrcd", lbcustcode.Text));
        bll.vDeleteContractKACusgrcd(arr);
        arr.Clear();
        if (hdcontract.Value.ToString() == "")
        {
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
        }
        else
        {
            arr.Add(new cArrayList("@contract_no", hdcontract.Value.ToString()));
        }
        bll.vBindingGridToSp(ref grdcusgrcd, "sp_tcontract_ka_cusgrcd_get", arr);
    }

    protected void btnaddAg_Click(object sender, EventArgs e)
    {
        btnaddAg.Attributes.Add("style", "display:none");
        string sbtc_no = ""; string sAgNo = "";
        List<cArrayList> arr = new List<cArrayList>();
        System.Data.SqlClient.SqlDataReader rs = null;
        arr.Clear();
        arr.Add(new cArrayList("@sys", "contractAGKA"));
        arr.Add(new cArrayList("@sysno", ""));
        bll.vGetContractNo(arr, ref rs);
        while (rs.Read())
        {
            sAgNo = rs["generated"].ToString();
        }

        sbtc_no = bll.vLookUp("select contract_sbtc_no from tcontract_ka_agreement where contract_no='" + Request.Cookies["usr_id"].Value.ToString() + "' and contract_ag_no='" + sAgNo.ToString() + "' and contract_sbtc_no='" + txAgSbtcNo.Text + "' and contract_cust_no='" + txAgCustNo.Text + "'");

        if (sbtc_no != "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('AG SBTC NO / AG CUSTOMER NO already used.','Insert SBTC AG NO','warning');", true);
            return;
        }

        if (txAgSbtcNo.Text.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('AG SBTC NO can not empty','Insert SBTC AG NO','warning');", true);
            return;
        }
        if (txAgCustNo.Text.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('AG CUSTOMER NO can not empty','Insert CUSTOMER AG NO','warning');", true);
            return;
        } if (txAgSbtcNo.Text.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('AG SBTC NO can not empty','Insert SBTC AG NO','warning');", true);
            return;
        }
        arr.Clear();
        if (hdcontract.Value.ToString() == "")
        {
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
        }
        else
        {
            arr.Add(new cArrayList("@contract_no", hdcontract.Value.ToString()));
        }
        arr.Add(new cArrayList("@contract_ag_no", sAgNo.ToString()));
        arr.Add(new cArrayList("@contract_sbtc_no", txAgSbtcNo.Text));
        arr.Add(new cArrayList("@contract_cust_no", txAgCustNo.Text));
        arr.Add(new cArrayList("@promogroup", cbpromogroup.SelectedValue.ToString()));
        arr.Add(new cArrayList("@promotype", cbpromotype.SelectedValue.ToString()));
        bll.vInsertContractKAAgreement(arr);
        arr.Clear();
        if (hdcontract.Value.ToString() == "")
        {
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
        }
        else
        {
            arr.Add(new cArrayList("@contract_no", hdcontract.Value.ToString()));
        }
        bll.vBindingGridToSp(ref grdAgreement, "sp_tcontract_ka_agreement_get", arr);
        txAgSbtcNo.Text = "";
        txAgCustNo.Text = "";
        btnaddAg.Attributes.Remove("style");
    }

    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_contractKA.aspx");
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        btsave.Visible = false;
        string sContract = "";
        List<cArrayList> arr = new List<cArrayList>();
        try
        {
            if (lbhoag_no.Text == "NEW")
            {
                if (grdAgreement.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Add Agreement HO First','Add Agreemet HO','warning');", true);
                    return;
                }

                System.Data.SqlClient.SqlDataReader rs = null;
                arr.Clear();
                arr.Add(new cArrayList("@sys", "contractKA"));
                arr.Add(new cArrayList("@sysno", ""));
                arr.Add(new cArrayList("@periode", ddMonth.SelectedValue.ToString() + ddYear.SelectedValue.ToString()));
                bll.vGetContractNo(arr, ref rs);
                while (rs.Read())
                {
                    sContract = rs["generated"].ToString();
                }                

                FileInfo fi = new FileInfo(upl.FileName);
                string ext = fi.Extension;
                byte[] fs = upl.FileBytes;
                if (fs.Length <= 1073741824)
                {
                    if (ext == ".jpg" || ext == ".jpeg" || ext == ".bmp" || ext == ".gif" || ext == ".png" || ext == ".pdf" || ext == ".JPEG" || ext == ".JPG" || ext == ".BMP" || ext == ".GIF" || ext == ".PNG" || ext == ".PDF")
                    {
                        if ((upl.FileName != "") || (upl.FileName != null))
                        {
                            lbhoag_no.Text = sContract;
                            upl.SaveAs(bll.sGetControlParameter("image_path") + "/contractka_doc/" + sContract.ToString() + ext);
                            arr.Clear();
                            arr.Add(new cArrayList("@contract_no", sContract));
                            arr.Add(new cArrayList("@contract_id", Request.Cookies["usr_id"].Value.ToString()));
                            arr.Add(new cArrayList("@rdcust", cbCustomer.SelectedValue.ToString()));
                            arr.Add(new cArrayList("@created_by", Request.Cookies["usr_id"].Value.ToString()));
                            arr.Add(new cArrayList("@tx_month", ddMonth.SelectedValue.ToString()));
                            arr.Add(new cArrayList("@tx_year", ddYear.SelectedValue.ToString()));
                            bll.vInsertContractKA(arr);
                            arr.Clear();
                            arr.Add(new cArrayList("@contract_no", sContract));
                            arr.Add(new cArrayList("@file", sContract.ToString() + ext));
                            bll.vInsertContractKADocument(arr);
                            hpfile_nm.Visible = true;
                            upl.Visible = false;
                            lblocfile.Text = sContract.ToString() + ext;
                            hpfile_nm.NavigateUrl = "/images/contractka_doc/" + sContract.ToString() + ext;
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

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('AG HO Saved.','" + sContract.ToString() + "','success');", true);
                return;
            }
            else
            {
                string statusHOG = "", contract_no_new = "";
                statusHOG = bll.vLookUp("select * from tmst_contract_ka where contract_no='"+hdcontract.Value+"' and tx_month='"+ddMonth.SelectedValue.ToString()+"' and tx_year='"+ddYear.SelectedValue.ToString()+"'");

                arr.Clear();

                if (statusHOG == "") {

                    System.Data.SqlClient.SqlDataReader rs1 = null;
                    List<cArrayList> arr1 = new List<cArrayList>();
                    arr1.Clear();
                    arr1.Add(new cArrayList("@sys", "contractKA"));
                    arr1.Add(new cArrayList("@sysno", ""));
                    arr1.Add(new cArrayList("@periode", ddMonth.SelectedValue.ToString() + ddYear.SelectedValue.ToString()));
                    bll.vGetContractNo(arr1, ref rs1);
                    while (rs1.Read())
                    {
                        contract_no_new = rs1["generated"].ToString();
                    }
                    arr.Add(new cArrayList("@contract_id", contract_no_new));
                }
                else { arr.Add(new cArrayList("@contract_id", hdcontract.Value)); }

                arr.Add(new cArrayList("@contract_no", hdcontract.Value));
                arr.Add(new cArrayList("@rdcust", cbCustomer.SelectedValue.ToString()));
                arr.Add(new cArrayList("@tx_month", ddMonth.SelectedValue.ToString()));
                arr.Add(new cArrayList("@tx_year", ddYear.SelectedValue.ToString()));
                bll.vUpdateContractKA(arr);

                if (upl.HasFile)
                {
                    FileInfo fi = new FileInfo(upl.FileName);
                    string ext = fi.Extension;
                    byte[] fs = upl.FileBytes;

                    if ((upl.FileName != "") || (upl.FileName != null))
                    {
                        arr.Clear();
                        if (statusHOG == "") 
                        { 
                            upl.SaveAs(bll.sGetControlParameter("image_path") + "/contractka_doc/" + contract_no_new + ext);
                            hpfile_nm.Visible = true;
                            upl.Visible = false;
                            lblocfile.Text = contract_no_new + ext;
                            hpfile_nm.NavigateUrl = "/images/contractka_doc/" + contract_no_new + ext;
                            arr.Clear();
                            arr.Add(new cArrayList("@contract_no", contract_no_new));
                            arr.Add(new cArrayList("@file", contract_no_new + ext));
                            bll.vUpdateContractKADocument(arr);
                        } 
                        else 
                        { 
                            upl.SaveAs(bll.sGetControlParameter("image_path") + "/contractka_doc/" + hdcontract.Value + ext);
                            hpfile_nm.Visible = true;
                            upl.Visible = false;
                            lblocfile.Text = hdcontract.Value + ext;
                            hpfile_nm.NavigateUrl = "/images/contractka_doc/" + hdcontract.Value + ext;
                            arr.Clear();
                            arr.Add(new cArrayList("@contract_no", hdcontract.Value));
                            arr.Add(new cArrayList("@file", hdcontract.Value + ext));
                            bll.vUpdateContractKADocument(arr);
                        }
                        
                    }
                }

                btnew.Visible = true;
                btnaddbranch.Visible = false;
                btnaddAg.Visible = false;
                btnaddprop.Visible = false;
                btsave.Visible = false;
                btupdate.Visible = true;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('AG HO Updated.','" + sContract.ToString() + "','success');", true);
                return;
            }
        }
        catch (Exception ex)
        {
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : SAVE AGREEMENT (HO)");
        }
    }

    protected void btupdate_Click(object sender, EventArgs e)
    {
        string statusCM = "";
        statusCM = bll.vLookUp("select isnull(a.ag_no,'no') from tcreditmemo_agreement a inner join tmst_creditmemo b on a.cm_no=b.cm_no where a.ag_no='" + hdcontract.Value + "' and b.cm_sta_id = 'A'");
        //if (statusCM == "")
        //{
            btnew.Visible = false;
            btnaddbranch.Visible = true;
            btnaddAg.Visible = true;
            btnaddprop.Visible = true;
            btsave.Visible = true;
            btupdate.Visible = false;
            upl.Visible = true;
        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('This AG have been used in Credit Memo','Deleted AG','warning');", true);
        //    return;
        //}
        
    }

    protected void btdelete_Click(object sender, EventArgs e)
    {
        if (bll.nCheckAccess("aghodel", Request.Cookies["usr_id"].Value.ToString()) == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To delete this agreement HO, Administrator !!','warning');", true);
            return;
        }   
        List<cArrayList> arr = new List<cArrayList>();
        try
        {
            string statusCM = "";
            statusCM = bll.vLookUp("select isnull(a.ag_no,'no') from tcreditmemo_agreement a inner join tmst_creditmemo b on a.cm_no=b.cm_no where a.ag_no='" + hdcontract.Value + "' and b.cm_sta_id = 'A'");
            if (statusCM != "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('This AG have been used in Credit Memo','Deleted AG','warning');", true);
                return;
            }
            else
            {
                arr.Add(new cArrayList("@contract_no", hdcontract.Value));
                arr.Add(new cArrayList("@delete_by", Request.Cookies["usr_id"].Value.ToString()));
                bll.vDelContractKA(arr);
                arr.Clear();
                btdelete.Visible = false;
                btnew.Visible = true;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('AG HO Deleted.','" + hdcontract.Value.ToString() + "','success');", true);
                return;
            }
            
        }
        catch (Exception ex)
        {
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : DELETE AGREEMENT (HO)");
        }

    }
    protected void btapprove_Click(object sender, EventArgs e)
    {
        if (bll.nCheckAccess("aghoapp", Request.Cookies["usr_id"].Value.ToString()) == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To approve this agreement HO, Administrator !!','warning');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        try
        {
            arr.Add(new cArrayList("@contract_no", hdcontract.Value));
            arr.Add(new cArrayList("@approve_by", Request.Cookies["usr_id"].Value.ToString()));
            bll.vAppContractKA(arr);
            arr.Clear();
            btapprove.Visible = false;
            btdelete.Visible = false;
            btnew.Visible = true;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('AG HO Approved.','" + hdcontract.Value.ToString() + "','success');", true);
            return;

        }
        catch (Exception ex)
        {
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : APPROVE AGREEMENT (HO)");
        }
    }
}

