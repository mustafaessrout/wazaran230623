using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_mstclaimho : System.Web.UI.Page
{
    cbll bll = new cbll();
    String listCheckClaim = "";


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["salespoint"] = "";
            //bll.vBindingFieldValueToComboAll(ref cbstatus, "cl_sta_id");            
            btnMenu.Attributes.Add("style", "display:none");
            dtSalesSnd.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Now);
            dtSalesRcv.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Now);
            dtSendClaim.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Now);
            dtSendClaim.Attributes.Add("readonly", "readonly");
            bll.vBindingComboToSp(ref cbbranch, "sp_tmst_salespoint_get ", "salespointcd", "salespoint_nm");
            bll.vBindingComboToSp(ref cbvendor, "sp_tmst_vendor_get", "vendor_cd", "vendor_nm");
            bll.vBindingComboToSp(ref cbstatus, "sp_tfield_value_get5", "fld_valu", "fld_note");
            bll.vBindingComboToSp(ref cbYear, "sp_tmst_year_getbyclaim", "fld_valu", "fld_valu");
            cbvendor.Items.Insert(0,"ALL");
            cbstatus.Items.Insert(0, new ListItem("All Status","ALL"));
            cbbranch.Items.Insert(0, new ListItem("All Branch", "ALL"));
            cbMonth.Items.Insert(0, new ListItem("ALL Month", "ALL"));
            cbYear.Items.Insert(0, new ListItem("ALL Year", "ALL"));
            btnew.Visible = true;
            btprint.Visible = false;
            cbYear.SelectedValue = "ALL";
            cbMonth.SelectedValue = "ALL";
            cbstatus.SelectedValue = "ALL";
            chall.Attributes.Add("style", "display:none");
            //cbstatus_SelectedIndexChanged(sender, e);
        }        
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetListClaim(string prefixText, int count, string contextKey)
    {
        HttpCookie cook;
        cook = HttpContext.Current.Request.Cookies["sp"];
        cbll bll = new cbll();
        List<string> lItem = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string sItem = string.Empty;
        System.Data.SqlClient.SqlDataReader rs = null;
        arr.Add(new cArrayList("@claim", prefixText));
        arr.Add(new cArrayList("@branch", HttpContext.Current.Session["salespoint"].ToString()));
        arr.Add(new cArrayList("@vendor", HttpContext.Current.Session["vendor"].ToString()));
        arr.Add(new cArrayList("@status", HttpContext.Current.Session["status"].ToString()));
        arr.Add(new cArrayList("@month", HttpContext.Current.Session["month"].ToString()));
        arr.Add(new cArrayList("@year", HttpContext.Current.Session["year"].ToString()));
        bll.vSearchMstClaimHO(arr, ref rs);
        while (rs.Read())
        {
            sItem = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["prop_no"].ToString() , rs["prop_no"].ToString());
            lItem.Add(sItem);
        }
        rs.Close();
        return (lItem.ToArray());
    }

    protected void grdclaim_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdclaim.PageIndex = e.NewPageIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@status", cbstatus.SelectedValue.ToString()));
        arr.Add(new cArrayList("@branch", cbbranch.SelectedValue.ToString()));
        arr.Add(new cArrayList("@vendor", cbvendor.SelectedValue.ToString()));
        arr.Add(new cArrayList("@month", cbMonth.SelectedValue.ToString()));
        arr.Add(new cArrayList("@year", cbYear.SelectedValue.ToString()));
        arr.Add(new cArrayList("@claim_no", txsearch.Text));
        bll.vBindingGridToSp(ref grdclaim, "sp_tmst_claim_ho_get", arr);
    }

    protected void grdclaimheader_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdclaimheader.PageIndex = e.NewPageIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@status", cbstatus.SelectedValue.ToString()));
        arr.Add(new cArrayList("@salespointcd", cbbranch.SelectedValue.ToString()));
        arr.Add(new cArrayList("@month", cbMonth.SelectedValue.ToString()));
        arr.Add(new cArrayList("@year", cbYear.SelectedValue.ToString()));
        arr.Add(new cArrayList("@claim_no", txsearch.Text));
        arr.Add(new cArrayList("@vendor", cbvendor.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grdclaimheader, "sp_tclaim_header_get", arr);
    }

    protected void chall_CheckedChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();

        if (chall.Checked)
        {
            arr.Add(new cArrayList("@status", cbstatus.SelectedValue.ToString()));
            arr.Add(new cArrayList("@branch", cbbranch.SelectedValue.ToString()));
            arr.Add(new cArrayList("@vendor", cbvendor.SelectedValue.ToString()));
            arr.Add(new cArrayList("@month", cbMonth.SelectedValue.ToString()));
            arr.Add(new cArrayList("@year", cbYear.SelectedValue.ToString()));
            arr.Add(new cArrayList("@claim_no", txsearch.Text));
            bll.vBindingGridToSp(ref grdclaim, "sp_tmst_claim_ho_get",arr);
            arr.Clear();
            if (grdclaim.Rows.Count == 0)
            {
                btnMenu.Attributes.Add("style", "display:none");
            }
            else
            {
                btnMenu.Attributes.Remove("style");
            }
        }
        else
        {
            if (hdclaim.Value.ToString() == "")
            {
                arr.Add(new cArrayList("@status", cbstatus.SelectedValue.ToString()));
                arr.Add(new cArrayList("@branch", cbbranch.SelectedValue.ToString()));
                arr.Add(new cArrayList("@vendor", cbvendor.SelectedValue.ToString()));
                arr.Add(new cArrayList("@month", cbMonth.SelectedValue.ToString()));
                arr.Add(new cArrayList("@year", cbYear.SelectedValue.ToString()));
                arr.Add(new cArrayList("@claim_no", txsearch.Text));
                bll.vBindingGridToSp(ref grdclaim, "sp_tmst_claim_ho_get", arr);                
                arr.Clear();                
            }
            else
            {
                arr.Add(new cArrayList("@status", cbstatus.SelectedValue.ToString()));
                arr.Add(new cArrayList("@claim_no", txsearch.Text));
                arr.Add(new cArrayList("@branch", cbbranch.SelectedValue.ToString()));
                arr.Add(new cArrayList("@vendor", cbvendor.SelectedValue.ToString()));
                arr.Add(new cArrayList("@month", cbMonth.SelectedValue.ToString()));
                arr.Add(new cArrayList("@year", cbYear.SelectedValue.ToString()));
                bll.vBindingGridToSp(ref grdclaim, "sp_tmst_claim_ho_get", arr);
                arr.Clear();                
            }
            chkSelect(sender, e);
        }
    }

    protected void chkSelect(object sender, EventArgs e)
    {
        bool checkSl = false,checkProp = false;
        string proposal = "";
        foreach (GridViewRow row in grdclaim.Rows)
        {
            CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
            Label lbclaim = (Label)row.FindControl("lbclaim");
            Label lbproposal = (Label)row.FindControl("lbprop");
            if (chckrw.Checked == true)
            {
                if (proposal != "")
                {
                    if (proposal == lbproposal.Text)
                    {
                        checkProp = false;
                        checkSl = true;
                    }
                    else
                    {
                        checkProp = true;
                        checkSl = false;
                    }
                }
                else
                {
                    checkProp = false;
                    checkSl = true;
                }                
                proposal = lbproposal.Text;
            }
        }
        
        if (checkSl == false)
        {
            btnMenu.Attributes.Add("style", "display:none");
        }
        else
        {
            btnMenu.Attributes.Remove("style");
        }
        if (checkProp == true)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Proposal Must be same in Sending Claim','Proposal No','warning');", true);
            return;
        }
        
    }

    protected void btclaim_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (cbstatus.SelectedValue.ToString() == "H-01")
        {
            grdclaim.Visible = true;
            grdclaimheader.Visible = false;
            arr.Add(new cArrayList("@status", cbstatus.SelectedValue.ToString()));
            arr.Add(new cArrayList("@claim_no", txsearch.Text));
            arr.Add(new cArrayList("@branch", cbbranch.SelectedValue.ToString()));
            arr.Add(new cArrayList("@vendor", cbvendor.SelectedValue.ToString()));
            arr.Add(new cArrayList("@month", cbMonth.SelectedValue.ToString()));
            arr.Add(new cArrayList("@year", cbYear.SelectedValue.ToString()));
            bll.vBindingGridToSp(ref grdclaim, "sp_tmst_claim_ho_get", arr);
        }
        else if (cbstatus.SelectedValue.ToString() == "R")
        {
            grdclaim.Visible = false;
            grdclaimheader.Visible = true;
            arr.Add(new cArrayList("@status", cbstatus.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespointcd", cbbranch.SelectedValue.ToString()));
            arr.Add(new cArrayList("@vendor", cbvendor.SelectedValue.ToString()));
            arr.Add(new cArrayList("@month", cbMonth.SelectedValue.ToString()));
            arr.Add(new cArrayList("@year", cbYear.SelectedValue.ToString()));
            arr.Add(new cArrayList("@claim_no", txsearch.Text));
            bll.vBindingGridToSp(ref grdclaimheader, "sp_tclaim_header_get", arr);
        }
        
        
    }

    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_claimentry_ho.aspx");
    }

    protected void btprint_Click(object sender, EventArgs e)
    {        
    }

    protected void btSaveSales_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (cbstatus.SelectedValue.ToString() == "H-01")
        {            
            foreach (GridViewRow row in grdclaim.Rows)
            {
                CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
                Label lbclaim = (Label)row.FindControl("lbclaim");
                if (chckrw.Checked == true)
                {
                    arr.Clear();
                    arr.Add(new cArrayList("@approve", "R"));
                    arr.Add(new cArrayList("@claim_no", lbclaim.Text));
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    arr.Add(new cArrayList("@claim_dt", DateTime.ParseExact(dtSalesSnd.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                    arr.Add(new cArrayList("@remark", txRemarkSalesSnd.Text));
                    arr.Add(new cArrayList("@status", "0"));
                    bll.vInsertClaimSalesDepartment(arr);
                }
            }
        }
        else if (cbstatus.SelectedValue.ToString() == "R")
        {
            foreach (GridViewRow row in grdclaim.Rows)
            {
                CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
                Label lbclaim = (Label)row.FindControl("lbclaim");
                if (chckrw.Checked == true)
                {
                    arr.Clear();
                    arr.Add(new cArrayList("@approve", "E"));
                    arr.Add(new cArrayList("@claim_no", lbclaim.Text));
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    arr.Add(new cArrayList("@claim_dt", DateTime.ParseExact(dtSalesRcv.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                    arr.Add(new cArrayList("@remark", txRemarkSalesRcv.Text));
                    arr.Add(new cArrayList("@status", "1"));
                    bll.vInsertClaimSalesDepartment(arr);
                }
            }
        }
        else if (cbstatus.SelectedValue.ToString() == "E")
        {
            foreach (GridViewRow row in grdclaim.Rows)
            {
                CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
                Label lbclaim = (Label)row.FindControl("lbclaim");
                if (chckrw.Checked == true)
                {
                    arr.Clear();
                    arr.Add(new cArrayList("@approve", "R"));
                    arr.Add(new cArrayList("@claim_no", lbclaim.Text));
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    arr.Add(new cArrayList("@claim_dt", DateTime.ParseExact(dtSalesSnd.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                    arr.Add(new cArrayList("@remark", txRemarkSalesSnd.Text));
                    arr.Add(new cArrayList("@status", "0"));
                    bll.vInsertClaimSalesDepartment(arr);
                }
            }
        }
        cbstatus_SelectedIndexChanged(sender, e);
    }

    protected void chkAllSelect(object sender, EventArgs e)
    {
        CheckBox chckheader = (CheckBox)grdclaim.HeaderRow.FindControl("chkAll");
        foreach (GridViewRow row in grdclaim.Rows)
        {
            CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
            Label lbclaim = (Label)row.FindControl("lbclaim");
            if (chckheader.Checked == true)
            {
                listCheckClaim += lbclaim.Text + ", ";                
                chckrw.Checked = true;
                btnMenu.Attributes.Remove("style");
            }
            else
            {
                listCheckClaim = "";
                chckrw.Checked = false;
                btnMenu.Attributes.Add("style", "display:none");
            }
        }
        
    }

    protected void grdclaim_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
        GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
        Label lbstatus = (Label)row.FindControl("lbstatus");
        int index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName.Equals("edit"))
        {
            string code = grdclaim.DataKeys[index].Value.ToString();

        }
        else if (e.CommandName.Equals("Print"))
        {
            string clHeaderNo = grdclaimheader.DataKeys[index].Value.ToString();
            string type = bll.vLookUp("select distinct discount_mec from tclaim_header left join tmst_claim on tclaim_header.claim_no = tmst_claim.claim_no where clh_no = '" + clHeaderNo + "' ");
            if (type == "CSH")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=sendclaimCO&clh=" + clHeaderNo + "');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=sendclaim&clh=" + clHeaderNo + "');", true);
            }    
        }
        else if (e.CommandName.Equals("Check"))
        {
            if (bll.nCheckAccess("CLAIMPAYMENTVATINV", Request.Cookies["usr_id"].Value.ToString()) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','Payment Claim !!','warning');", true);
                return;
            }
            string clHeaderNo = grdclaimheader.DataKeys[index].Value.ToString();
            //if ((cbstatus.SelectedValue.ToString() == "H-05") || (cbstatus.SelectedValue.ToString() == "H-06"))
            if ((lbstatus.Text == "Approved Paid"))
            {
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openwindow('fm_claiminfopayment.aspx?dc=" + clHeaderNo + "');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openwindow('fm_paymentclaim2.aspx?dc=" + clHeaderNo + "');", true);
            }
        }
        else if (e.CommandName.Equals("Pay"))
        {
            if (bll.nCheckAccess("CLAIMPAYMENTVATINV", Request.Cookies["usr_id"].Value.ToString()) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','Payment Claim !!','warning');", true);
                return;
            }
            string clHeaderNo = grdclaimheader.DataKeys[index].Value.ToString();
            //if ((cbstatus.SelectedValue.ToString() == "H-05") || (cbstatus.SelectedValue.ToString() == "H-06"))
            if ((lbstatus.Text == "Approved-Full (Prc)") || (lbstatus.Text == "Approved-Partial (Prc)") || (lbstatus.Text == "Approved Paid") || (lbstatus.Text == "Approved Unpaid"))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openwindow('fm_paymentclaim2.aspx?dc=" + clHeaderNo + "');", true);
            }
        }
        else if (e.CommandName.Equals("Pay to Branch"))
        {
            if (bll.nCheckAccess("CLAIMPAYMENTVATINV", Request.Cookies["usr_id"].Value.ToString()) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','Payment Claim !!','warning');", true);
                return;
            }
            string clHeaderNo = grdclaimheader.DataKeys[index].Value.ToString();
            if ((lbstatus.Text == "Approved Paid"))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openwindow('fm_paymentclaimtobranch.aspx?dc=" + clHeaderNo + "');", true);
            }
        }
    }

    protected void cbstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        claimSending.Attributes.Remove("data-target");
        if (cbstatus.SelectedValue.ToString() == "H-01")
        {
            grdclaim.Visible = true;
            grdclaimheader.Visible = false;
            txsearch.Attributes.Remove("readonly");
            chall_CheckedChanged(sender, e);
            claimSending.Attributes.Add("data-target", "#sendingClaim");
        }
        else //if(cbstatus.SelectedValue.ToString() == "R")
        {
            grdclaim.Visible = false;
            grdclaimheader.Visible = true;
            //txsearch.Attributes.Add("readonly", "readonly");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@status", cbstatus.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespointcd", cbbranch.SelectedValue.ToString()));
            arr.Add(new cArrayList("@month", cbMonth.SelectedValue.ToString()));
            arr.Add(new cArrayList("@year", cbYear.SelectedValue.ToString()));
            arr.Add(new cArrayList("@claim_no", txsearch.Text));
            arr.Add(new cArrayList("@vendor", cbvendor.SelectedValue.ToString()));
            bll.vBindingGridToSp(ref grdclaimheader, "sp_tclaim_header_get", arr);
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "vDisableShow();", true);
        Session["status"] = cbstatus.SelectedValue.ToString();
    }

    protected void cbbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbstatus.SelectedValue.ToString() == "H-01")
        {
            chall_CheckedChanged(sender, e);
        }
        else //if (cbstatus.SelectedValue.ToString() == "R")
        {
            cbstatus_SelectedIndexChanged(sender, e);
        }        
        Session["salespoint"] = cbbranch.SelectedValue.ToString();
    }

    protected void cbvendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbstatus.SelectedValue.ToString() == "H-01")
        {
            chall_CheckedChanged(sender, e);
        }
        else //if (cbstatus.SelectedValue.ToString() == "R")
        {
            cbstatus_SelectedIndexChanged(sender, e);
        }
        Session["vendor"] = cbvendor.SelectedValue.ToString();
    }

    protected void cbmonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbstatus.SelectedValue.ToString() == "H-01")
        {
            chall_CheckedChanged(sender, e);
        }
        else //if (cbstatus.SelectedValue.ToString() == "R")
        {
            cbstatus_SelectedIndexChanged(sender, e);
        }
        Session["month"] = cbMonth.SelectedValue.ToString();
    }

    protected void cbyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbstatus.SelectedValue.ToString() == "H-01")
        {
            chall_CheckedChanged(sender, e);
        }
        else //if (cbstatus.SelectedValue.ToString() == "R")
        {
            cbstatus_SelectedIndexChanged(sender, e);
        }
        Session["year"] = cbYear.SelectedValue.ToString();
    }

    protected void btSendClaim_Click(object sender, EventArgs e)
    {
        string clHeaderNo = "",listAttn = "",listCC = ""; string[] vendor;
        int i = 1;
        vendor = txto.Text.Split('-');
        System.Data.SqlClient.SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@sys", "clheader"));
        arr.Add(new cArrayList("@sysno", ""));
        bll.vGetDiscountNo(arr, ref rs);
        while (rs.Read())
        {
            clHeaderNo = rs["generated"].ToString();
        }
        foreach (ListItem latn in lstAttn.Items)
        {
            if (latn.Selected)
            {
                if (i % 2 == 0) { listAttn += ","; }
                listAttn += latn.Text;                
                i++;
            }
        }
        i = 1;
        foreach (ListItem lcc in lstCC.Items)
        {
            if (lcc.Selected)
            {
                if (i % 2 == 0) { listCC += ","; }
                listCC += lcc.Text;
                i++;
            }
        }
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        //bll.vDelWrkClaimSendPrincipal(arr);
        if (cbstatus.SelectedValue.ToString() == "H-01")
        {
            foreach (GridViewRow row in grdclaim.Rows)
            {
                CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
                Label lbclaim = (Label)row.FindControl("lbclaim");
                if (chckrw.Checked == true)
                {
                    arr.Clear();
                    arr.Add(new cArrayList("@clh_no", clHeaderNo));
                    arr.Add(new cArrayList("@claim_no", lbclaim.Text));
                    arr.Add(new cArrayList("@salespoint", bll.vLookUp("select salespointcd from tmst_claim where claim_no = '" + lbclaim.Text + "'")));
                    arr.Add(new cArrayList("@claimby", Request.Cookies["usr_id"].Value.ToString()));
                    arr.Add(new cArrayList("@claim_dt", DateTime.ParseExact(dtSendClaim.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                    arr.Add(new cArrayList("@att", listAttn));
                    arr.Add(new cArrayList("@cc", listCC));
                    arr.Add(new cArrayList("@remarks", txRemarks.Text.ToString()));
                    arr.Add(new cArrayList("@cprocess", "1"));
                    if ((vendor[0].Replace(" ", "") == "V1600") || (vendor[0].Replace(" ", "") == "V1700") || (vendor[0].Replace(" ", "") == "V1400"))
                    {
                        arr.Add(new cArrayList("@cstatus", "H-02"));
                    }
                    else
                    {
                        arr.Add(new cArrayList("@cstatus", "H-06"));
                    }

                    bll.vInsertClaimSendPrincipal(arr);
                }
            }
            string type = bll.vLookUp("select distinct discount_mec from tclaim_header left join tmst_claim on tclaim_header.claim_no = tmst_claim.claim_no where clh_no = '" + clHeaderNo + "' ");
            if (type == "CSH")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=sendclaimCO&clh=" + clHeaderNo + "');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=sendclaim&clh=" + clHeaderNo + "');", true);
            }
            if ((vendor[0].Replace(" ", "") == "V1600") || (vendor[0].Replace(" ", "") == "V1700"))
            {
                arr.Clear();
                arr.Add(new cArrayList("@clh_no", clHeaderNo));
                arr.Add(new cArrayList("@typ", type));
                bll.vBatchEmailtoPAFL(arr);
            }
        }
        arr.Clear();
        cbstatus_SelectedIndexChanged(sender, e);
    }

    protected void btSendClose_Click(object sender, EventArgs e)
    {
        //listCheckClaim = "";
        //arr.Clear();
        //arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        //bll.vDelWrkClaimSendPrincipal(arr);
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (cbstatus.SelectedValue.ToString() == "H-01")
        {
            chall_CheckedChanged(sender, e);
        }
        else //if (cbstatus.SelectedValue.ToString() == "R")
        {
            cbstatus_SelectedIndexChanged(sender, e);
        }
    }

    protected void send_Click(object sender, EventArgs e)
    {
        string claim = "", vendor = "";
        foreach (GridViewRow row in grdclaim.Rows)
        {
            CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
            Label lbclaim = (Label)row.FindControl("lbclaim");
            if (chckrw.Checked == true)
            {
                claim = lbclaim.Text;
                break;
            }
        }
        vendor = bll.vLookUp("select vendor_cd from tmst_claim where claim_no = '" + claim + "'");
        txto.Text = bll.vLookUp("select (vendor_cd+' - '+vendor_nm) as vendor from tmst_vendor where vendor_cd = '" + vendor + "'");
        txto.Attributes.Add("readonly", "readonly");
        txRemarks.Text = bll.vLookUp("select bgremark from tmst_proposal where prop_no = (select prop_no from tmst_claim where claim_no = '"+ claim +"')");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@vendor_cd", vendor));
        bll.vBindingListToSp(ref lstAttn, "sp_tproposal_signbyvendor_get", "fullname", "fullname", arr);
        arr.Clear();
        bll.vBindingListToSp(ref lstCC, "sp_tproposal_signoff_get", "emp_desc", "emp_desc", arr);
    }

}

