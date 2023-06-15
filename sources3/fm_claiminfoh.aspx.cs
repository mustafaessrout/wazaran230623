using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_claiminfoh : System.Web.UI.Page
{

    cbll bll = new cbll();
    string sClaimCode = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        {
            sClaimCode = Request.QueryString["dc"];
            lbclaim.Text = sClaimCode;
            System.Data.SqlClient.SqlDataReader rs = null;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@clh_no", sClaimCode));
            bll.vGetMstClaimViewHeader(arr, ref rs);
            while (rs.Read())
            {
                lbclaimdt.Text = Convert.ToDateTime(rs["claim_dt"]).ToShortDateString();
                lbproposal.Text = rs["prop_no"].ToString();
                lbsalespoint.Text = rs["salespoint_nm"].ToString();
                lbstatus.Text = rs["status_nm"].ToString();
                lbremark.Text = rs["remark"].ToString();
            }
            arr.Clear();
            arr.Add(new cArrayList("@clh_no", sClaimCode));
            bll.vBindingGridToSp(ref grdclaim, "sp_tclaim_header_detail ", arr);
            bll.vBindingGridToSp(ref grddocument, "sp_tclaim_document", arr);
            arr.Clear();
            arr.Add(new cArrayList("@sp", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@search", sClaimCode));
            bll.vBindingGridToSp(ref grdinvoice, "sp_report_invoice_claim2", arr);
            bll.vBindingGridToSp(ref grdcontract, "sp_report_contract_claim", arr);
            bll.vBindingComboToSp(ref cbgroup, "sp_get_issue_group", "issue_group", "issue_group");
            cbgroup_SelectedIndexChanged(sender, e);
            arr.Clear();
            arr.Add(new cArrayList("@clh_no", sClaimCode));
            bll.vBindingGridToSp(ref grdvat, "sp_tclaim_header_vat", arr);
        }
    }

    protected void cbgroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@group", cbgroup.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbissue, "sp_get_issue_list", "issue_no", "issue_nm", arr);
    }

    protected void grdclaim_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow) return;
        Label lbstatus = (Label)e.Row.FindControl("lbstatus");

        if (lbstatus.Text == "H-02_Received (Prc)")
        {
            Button btnApprove = (Button)e.Row.FindControl("btnApprove");
            btnApprove.Visible = true;//true;
            Button btnReturn = (Button)e.Row.FindControl("btnReturn");
            btnReturn.Visible = true;
            Button btnReject = (Button)e.Row.FindControl("btnReject");
            btnReject.Visible = true;
            Button btnDelete = (Button)e.Row.FindControl("btnDelete");
            btnDelete.Visible = true;
            Button btnUpdate = (Button)e.Row.FindControl("btnUpdate");
            btnUpdate.Visible = false;
        }
        else if (lbstatus.Text == "H-03_Returned (Prc)")
        {
            Button btnApprove = (Button)e.Row.FindControl("btnApprove");
            btnApprove.Visible = true;//true;
            Button btnReturn = (Button)e.Row.FindControl("btnReturn");
            btnReturn.Visible = false;
            Button btnReject = (Button)e.Row.FindControl("btnReject");
            btnReject.Visible = true;
            Button btnDelete = (Button)e.Row.FindControl("btnDelete");
            btnDelete.Visible = true;
            Button btnUpdate = (Button)e.Row.FindControl("btnUpdate");
            btnUpdate.Visible = false;
        }
        else if (lbstatus.Text == "H-04_Suspended (Prc)")
        {
            Button btnApprove = (Button)e.Row.FindControl("btnApprove");
            btnApprove.Visible = true;//true;
            Button btnReturn = (Button)e.Row.FindControl("btnReturn");
            btnReturn.Visible = false;
            Button btnReject = (Button)e.Row.FindControl("btnReject");
            btnReject.Visible = false;
            Button btnDelete = (Button)e.Row.FindControl("btnDelete");
            btnDelete.Visible = false;
            Button btnUpdate = (Button)e.Row.FindControl("btnUpdate");
            btnUpdate.Visible = false;
        }
        else if (lbstatus.Text == "H-05_Approved-Partial (Prc)")
        {
            Button btnApprove = (Button)e.Row.FindControl("btnApprove");
            btnApprove.Visible = true;// true;
            Button btnReturn = (Button)e.Row.FindControl("btnReturn");
            btnReturn.Visible = true;
            Button btnReject = (Button)e.Row.FindControl("btnReject");
            btnReject.Visible = true;
            Button btnDelete = (Button)e.Row.FindControl("btnDelete");
            btnDelete.Visible = false;
            Button btnUpdate = (Button)e.Row.FindControl("btnUpdate");
            btnUpdate.Visible = true;
        }
        else if (lbstatus.Text == "H-06_Approved-Full (Prc)")
        {
            Button btnApprove = (Button)e.Row.FindControl("btnApprove");
            btnApprove.Visible = false;
            Button btnReturn = (Button)e.Row.FindControl("btnReturn");
            btnReturn.Visible = true;
            Button btnReject = (Button)e.Row.FindControl("btnReject");
            btnReject.Visible = true;
            Button btnDelete = (Button)e.Row.FindControl("btnDelete");
            btnDelete.Visible = false;
            Button btnUpdate = (Button)e.Row.FindControl("btnUpdate");
            btnUpdate.Visible = true;
        }
        else if (lbstatus.Text == "H-07_Approved Unpaid")
        {
            Button btnApprove = (Button)e.Row.FindControl("btnApprove");
            btnApprove.Visible = false;
            Button btnReturn = (Button)e.Row.FindControl("btnReturn");
            btnReturn.Visible = false;
            Button btnReject = (Button)e.Row.FindControl("btnReject");
            btnReject.Visible = true;
            Button btnDelete = (Button)e.Row.FindControl("btnDelete");
            btnDelete.Visible = false;
            Button btnUpdate = (Button)e.Row.FindControl("btnUpdate");
            btnUpdate.Visible = true;
        }
        else
        {
            Button btnApprove = (Button)e.Row.FindControl("btnApprove");
            btnApprove.Visible = true;// true;
            Button btnReturn = (Button)e.Row.FindControl("btnReturn");
            btnReturn.Visible = false;
            Button btnReject = (Button)e.Row.FindControl("btnReject");
            btnReject.Visible = true;
            Button btnDelete = (Button)e.Row.FindControl("btnDelete");
            btnDelete.Visible = false;
            Button btnUpdate = (Button)e.Row.FindControl("btnUpdate");
            btnUpdate.Visible = true;
        }
    }

    protected void grdclaim_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "return")
        {
            if (bll.nCheckAccess("CLAIMVATINV", Request.Cookies["usr_id"].Value.ToString()) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','Approve Claim Invoice !!','warning');", true);
                return;
            }

            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdclaim.Rows[index];
            Label lbclaimno = (Label)grdclaim.Rows[index].FindControl("lbclaimno");
            Label lbstatus = (Label)grdclaim.Rows[index].FindControl("lbstatus");
            Label lbccnr = (Label)grdclaim.Rows[index].FindControl("lbccnr");
            HiddenField lbtotamount = (HiddenField)grdclaim.Rows[index].FindControl("lbtotamount");
            lbstatusCl.Value = lbstatus.Text;
            lblModalTitle.Text = "Return Claim";
            btSave.Text = "Return";
            txClaim.Text = lbclaimno.Text;
            if (lbstatus.Text == "H-05_Approved-Partial (Prc)")
            {
                txAmount.Text = bll.vLookUp("select (a.amount-sum(isnull(b.percentage,0))) from tmst_claim a inner join tclaim_approve b on a.claim_no=b.claim_no and a.salespointcd=b.salespointcd where a.claim_no='" + lbclaimno.Text + "' and a.ccnr_no='" + lbccnr.Text + "' group by a.amount");
            }
            else
            {
                txAmount.Text = "0";
            }
            txtotAmount.Text = lbtotamount.Value.ToString();
            txtotAmount.Attributes.Add("readonly", "readonly");
            txtotAppAmount.Attributes.Add("readonly", "readonly");
            txtotAppAmount.Text = bll.vLookUp("select sum(isnull(b.percentage,0)) from tmst_claim a inner join tclaim_approve b on a.claim_no=b.claim_no and a.salespointcd=b.salespointcd where a.claim_no='" + lbclaimno.Text + "' and a.ccnr_no='" + lbccnr.Text + "'");
            txClaim.Attributes.Add("readonly", "readonly");
            uploadfile.Attributes.Add("style", "display:none");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myReturn').modal();", true);
            upModal.Update();
        }
        else if (e.CommandName == "approve")
        {
            if (bll.nCheckAccess("CLAIMVATINV", Request.Cookies["usr_id"].Value.ToString()) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','Approve Claim Invoice !!','warning');", true);
                return;
            }
            btSave.Visible = true;
            btSave.Enabled = true;
            btSave.Attributes.Remove("style");
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdclaim.Rows[index];
            Label lbclaimno = (Label)grdclaim.Rows[index].FindControl("lbclaimno");
            Label lbccnr = (Label)grdclaim.Rows[index].FindControl("lbccnr");
            Label lbstatus = (Label)grdclaim.Rows[index].FindControl("lbstatus");
            HiddenField lbtotamount = (HiddenField)grdclaim.Rows[index].FindControl("lbtotamount");

            string vendor = bll.vLookUp("select vendor_cd from tmst_claim where claim_no='" + lbclaimno.Text + "' and ccnr_no='" + lbccnr.Text + "'");

            //if (vendor == "V1600" || vendor == "V1700")
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Can not Approve for this claim','Approve Claim','warning');", true);
            //}
            //else
            //{
                lbstatusCl.Value = lbstatus.Text;
                lblModalTitle.Text = "Approve Claim";
                btSave.Text = "Approve";
                txClaim.Text = lbclaimno.Text;
                if (lbstatus.Text == "H-05_Approved-Partial (Prc)")
                {
                    txAmount.Text = bll.vLookUp("select (a.amount-sum(isnull(b.percentage,0))) from tmst_claim a inner join tclaim_approve b on a.claim_no=b.claim_no and a.salespointcd=b.salespointcd where a.claim_no='" + lbclaimno.Text + "' and a.ccnr_no='" + lbccnr.Text + "' group by a.amount");
                }
                else
                {
                    txAmount.Text = "0";
                }
                txtotAmount.Text = lbtotamount.Value.ToString();
                txtotAppAmount.Text = bll.vLookUp("select sum(isnull(b.percentage,0)) from tmst_claim a inner join tclaim_approve b on a.claim_no=b.claim_no and a.salespointcd=b.salespointcd where a.claim_no='" + lbclaimno.Text + "' and a.ccnr_no='" + lbccnr.Text + "'");
                txClaim.Attributes.Add("readonly", "readonly");
                txtotAmount.Attributes.Add("readonly", "readonly");
                txtotAppAmount.Attributes.Add("readonly", "readonly");
                payment.Attributes.Remove("style");
                uploadfile.Attributes.Add("style", "display:none");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myReturn').modal();", true);
                upModal.Update();
            //}
            
        }
        else if (e.CommandName == "reject")
        {
            if (bll.nCheckAccess("CLAIMVATINV", Request.Cookies["usr_id"].Value.ToString()) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','Approve Claim Invoice !!','warning');", true);
                return;
            }

            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdclaim.Rows[index];
            Label lbclaimno = (Label)grdclaim.Rows[index].FindControl("lbclaimno");
            Label lbccnr = (Label)grdclaim.Rows[index].FindControl("lbccnr");
            Label lbstatus = (Label)grdclaim.Rows[index].FindControl("lbstatus");
            HiddenField lbtotamount = (HiddenField)grdclaim.Rows[index].FindControl("lbtotamount");

            lbstatusCl.Value = lbstatus.Text;
            lblModalTitle.Text = "Reject Claim";
            btSave.Text = "Reject";
            txClaim.Text = lbclaimno.Text;
            txClaim.Attributes.Add("readonly", "readonly");
            payment.Attributes.Add("style", "display:none");
            uploadfile.Attributes.Remove("style");

            if (lbstatus.Text == "H-05_Approved-Partial (Prc)")
            {
                txAmount.Text = bll.vLookUp("select (a.amount-sum(isnull(b.percentage,0))) from tmst_claim a inner join tclaim_approve b on a.claim_no=b.claim_no and a.salespointcd=b.salespointcd where a.claim_no='" + lbclaimno.Text + "' and a.ccnr_no='" + lbccnr.Text + "' group by a.amount");
            }
            else
            {
                txAmount.Text = "0";
            }
            txtotAmount.Text = lbtotamount.Value.ToString();
            txtotAppAmount.Text = bll.vLookUp("select sum(isnull(b.percentage,0)) from tmst_claim a inner join tclaim_approve b on a.claim_no=b.claim_no and a.salespointcd=b.salespointcd where a.claim_no='" + lbclaimno.Text + "' and a.ccnr_no='" + lbccnr.Text + "'");
            txtotAmount.Attributes.Add("readonly", "readonly");
            txtotAppAmount.Attributes.Add("readonly", "readonly");

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myReturn').modal();", true);
            upModal.Update();
        }
        else if (e.CommandName == "delete")
        {
            if (bll.nCheckAccess("CLAIMVATINV", Request.Cookies["usr_id"].Value.ToString()) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','Approve Claim Invoice !!','warning');", true);
                return;
            }

            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdclaim.Rows[index];
            Label lbclaimno = (Label)grdclaim.Rows[index].FindControl("lbclaimno");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@clh_no", lbclaim.Text));
            arr.Add(new cArrayList("@claim_no", lbclaimno.Text));
            bll.vDeleteClaimDtl(arr);
            Response.Redirect(Request.Url.AbsoluteUri);
        }
        else 
        {
            if (bll.nCheckAccess("CLAIMVATINV", Request.Cookies["usr_id"].Value.ToString()) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','Approve Claim Invoice !!','warning');", true);
                return;
            }

            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdclaim.Rows[index];
            Label lbclaimno = (Label)grdclaim.Rows[index].FindControl("lbclaimno");
            Label lbccnr = (Label)grdclaim.Rows[index].FindControl("lbccnr");
            Label lbstatus = (Label)grdclaim.Rows[index].FindControl("lbstatus");
            HiddenField lbtotamount = (HiddenField)grdclaim.Rows[index].FindControl("lbtotamount");

            txtotAmount.Text = lbtotamount.Value.ToString();
            txtotAppAmount.Text = bll.vLookUp("select sum(isnull(b.percentage,0)) from tmst_claim a inner join tclaim_approve b on a.claim_no=b.claim_no and a.salespointcd=b.salespointcd where a.claim_no='" + lbclaimno.Text + "' and a.ccnr_no='" + lbccnr.Text + "'");
            txClaim.Attributes.Add("readonly", "readonly");
            txtotAmount.Attributes.Add("readonly", "readonly");
            txtotAppAmount.Attributes.Add("readonly", "readonly");

            lbstatusCl.Value = lbstatus.Text;
            lblModalTitle.Text = "Edit Total Payment";
            btSave.Text = "Update";
            txClaim.Text = lbclaimno.Text;
            txAmount.Text = "0";
            txClaim.Attributes.Add("readonly", "readonly");
            payment.Attributes.Add("style", "display:none");
            issueGroup.Attributes.Add("style", "display:none");
            issueList.Attributes.Add("style", "display:none");
            remarks.Attributes.Add("style", "display:none");
            uploadfile.Attributes.Add("style", "display:none");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myReturn').modal();", true);
            upModal.Update();
        }
    }

    protected void btsave_Click(object sender, EventArgs e)
    {
        btSave.Visible = false;
        btSave.Enabled = false;
        btSave.Attributes.Add("style", "display:none");

        string vendor_cd = "";
        int tx_year = 0;
        string vat_inv = "";
        sClaimCode = Request.QueryString["dc"];
        List<cArrayList> arr = new List<cArrayList>();
        if (btSave.Text == "Return")
        {
            if (txAmount.Text == "" || Convert.ToDouble(txAmount.Text) == 0.0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Amount must be more than 0.','Amount Payment','warning');", true);
                return;
            }
            arr.Add(new cArrayList("@clh_no", sClaimCode));
            arr.Add(new cArrayList("@claim_no", txClaim.Text));
            arr.Add(new cArrayList("@issue", cbissue.SelectedValue.ToString()));
            arr.Add(new cArrayList("@remark", txremarks.Text));
            arr.Add(new cArrayList("@returnby", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@percentage", txAmount.Text));
            bll.vInsReturnClaim(arr);
            arr.Clear();
        }
        else if (btSave.Text == "Reject")
        {
            if (txAmount.Text == "" || Convert.ToDouble(txAmount.Text) < 0.0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Amount must be more than 0.','Amount Payment','warning');", true);
                return;
            }

            //FileInfo fi = new FileInfo(upl.FileName);
            //string ext = fi.Extension;
            //byte[] fs = upl.FileBytes;

            //if ((upl.FileName == "") || (upl.FileName == null))
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Supported Documents must be upload.','Document Upload','warning');", true);
            //    return;
            //}
            
            //if ((upl.FileName != "") || (upl.FileName != null))
            //{
                //upl.SaveAs(bll.sGetControlParameter("image_path") + "/claim_doc/suspend/" + sClaimCode.ToString() + "_" + txClaim.Text + ext);
                arr.Add(new cArrayList("@clh_no", sClaimCode));
                arr.Add(new cArrayList("@claim_no", txClaim.Text));
                arr.Add(new cArrayList("@returnby", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@fileloc", sClaimCode.ToString() + "_" + txClaim.Text));
                bll.vInsertClaimSuspendDocument(arr);
                arr.Clear();    
                arr.Add(new cArrayList("@clh_no", sClaimCode));
                arr.Add(new cArrayList("@claim_no", txClaim.Text));
                arr.Add(new cArrayList("@issue", cbissue.SelectedValue.ToString()));
                arr.Add(new cArrayList("@remark", txremarks.Text));
                arr.Add(new cArrayList("@returnby", Request.Cookies["usr_id"].Value.ToString()));
                bll.vInsRejectClaim(arr);
                arr.Clear();
                
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Upload Document when Reject / Suspened this claim.','Suspened Claim','warning');", true);
            //    return;
            //}
        }
        else if (btSave.Text == "Update")
        {
            arr.Add(new cArrayList("@clh_no", sClaimCode));
            arr.Add(new cArrayList("@claim_no", txClaim.Text));
            arr.Add(new cArrayList("@percentage", txAmount.Text));
            bll.vUpdApproveClaim(arr);
            arr.Clear();
        }
        else
        {
            if ((lbstatusCl.Value == "H-02_Received (Prc)") || (lbstatusCl.Value == "H-04_Suspended (Prc)") || (lbstatusCl.Value == "H-05_Approved-Partial (Prc)") || (lbstatusCl.Value == "H-08_Approved Paid"))
            {
                string amount = bll.vLookUp("select top 1 amount from tmst_claim inner join tclaim_header on tmst_claim.claim_no = tclaim_header.claim_no and tmst_claim.salespointcd = tclaim_header.salespointcd   where tmst_claim.claim_no = '" + txClaim.Text + "' and tclaim_header.clh_no = '" + sClaimCode + "'");

                vendor_cd = bll.vLookUp("select tmst_claim.vendor_cd from tclaim_header left join tmst_claim on tclaim_header.claim_no = tmst_claim.claim_no and tclaim_header.salespointcd = tmst_claim.salespointcd where clh_no = '" + sClaimCode + "'");

                tx_year = int.Parse(bll.vLookUp("select isnull(year(tclaim_header.claim_dt),0) from tclaim_header left join tmst_claim on tclaim_header.claim_no = tmst_claim.claim_no and tclaim_header.salespointcd = tmst_claim.salespointcd where clh_no = '" + sClaimCode + "'"));

                //if (Convert.ToDouble(txAmount.Text) > Convert.ToDouble(amount))
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Amount must be less than" + amount + "','Amount Payment','warning');", true);
                //    return;
                //}
                //else
                //{
                    if (txAmount.Text == "" || Convert.ToDouble(txAmount.Text) == 0.0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Amount must be more than 0.','Amount Payment','warning');", true);
                        return;
                    }
                    arr.Add(new cArrayList("@clh_no", sClaimCode));
                    arr.Add(new cArrayList("@claim_no", txClaim.Text));
                    arr.Add(new cArrayList("@issue", cbissue.SelectedValue.ToString()));
                    arr.Add(new cArrayList("@percentage", txAmount.Text));
                    arr.Add(new cArrayList("@remark", txremarks.Text));
                    arr.Add(new cArrayList("@approveby", Request.Cookies["usr_id"].Value.ToString()));
                    arr.Add(new cArrayList("@payment", cbPayment.SelectedValue.ToString()));
                    bll.vInsApproveClaim(arr);
                    arr.Clear();
                                        
                    // Generate INV VAT 

                    if (vendor_cd == "V1600" || vendor_cd == "V1700")
                    {

                        if (cbPayment.SelectedValue.ToString() != "FG")
                        {
                            if (tx_year >= 2018)
                            {
                                System.Data.SqlClient.SqlDataReader rs = null;
                                arr.Clear();
                                arr.Add(new cArrayList("@sys", "clheaderinv"));
                                arr.Add(new cArrayList("@sysno", ""));
                                bll.vGetDiscountNo(arr, ref rs);
                                while (rs.Read())
                                {
                                    vat_inv = rs["generated"].ToString();
                                }

                                arr.Clear();
                                arr.Add(new cArrayList("@clh_no", sClaimCode));
                                arr.Add(new cArrayList("@inv_no", vat_inv));
                                arr.Add(new cArrayList("@claim_no", txClaim.Text));
                                arr.Add(new cArrayList("@percentage", txAmount.Text));
                                arr.Add(new cArrayList("@approveby", Request.Cookies["usr_id"].Value.ToString()));
                                bll.vInserClaimInvVat(arr);
                            }
                        }
                    }    

                    // Generate INV VAT
                //}
            }
            else if (lbstatusCl.Value == "H-03_Returned (Prc)")
            {
                string amount = bll.vLookUp("select top 1 amount from tmst_claim inner join tclaim_header on tmst_claim.claim_no = tclaim_header.claim_no and tmst_claim.salespointcd = tclaim_header.salespointcd   where tmst_claim.claim_no = '" + txClaim.Text + "' and tclaim_header.clh_no = '" + sClaimCode + "'");

                if (txAmount.Text == "" || Convert.ToDouble(txAmount.Text) == 0.0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Amount must be more than 0.','Amount Payment','warning');", true);
                    return;
                }

                if (Convert.ToDouble(txAmount.Text) > Convert.ToDouble(amount))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Amount must be less than" + amount + "','Amount Payment','warning');", true);
                    return;
                }
                else
                {
                    arr.Add(new cArrayList("@clh_no", sClaimCode));
                    arr.Add(new cArrayList("@claim_no", txClaim.Text));
                    arr.Add(new cArrayList("@issue", cbissue.SelectedValue.ToString()));
                    arr.Add(new cArrayList("@percentage", txAmount.Text));
                    arr.Add(new cArrayList("@remark", txremarks.Text));
                    arr.Add(new cArrayList("@approveby", Request.Cookies["usr_id"].Value.ToString()));
                    arr.Add(new cArrayList("@status", "H-03"));
                    bll.vInsApproveClaim(arr);
                    arr.Clear();
                }
            }
                     
        }
        btSave.Visible = false;
        btSave.Enabled = false;
        btSave.Attributes.Add("style", "display:none");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myReturn').modal('hide');", true);
        upModal.Update();
        Response.Redirect(Request.Url.AbsoluteUri);

    }

    protected void print_Click(object sender, EventArgs e)
    {
        string type = bll.vLookUp("select distinct discount_mec from tclaim_header left join tmst_claim on tclaim_header.claim_no = tmst_claim.claim_no and tclaim_header.salespointcd = tmst_claim.salespointcd where clh_no = '" + Request.QueryString["dc"] + "' ");
        if (type == "CSH" || type == "CNDN")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=sendclaimCO&clh=" + Request.QueryString["dc"] + "');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=sendclaim&clh=" + Request.QueryString["dc"] + "');", true);
        }        
    }

    protected void update_Click(object sender, EventArgs e)
    {
        string vendor = "";
        txclhno.Text = lbclaim.Text;
        vendor = bll.vLookUp("select tmst_claim.vendor_cd from tclaim_header left join tmst_claim on tclaim_header.claim_no = tmst_claim.claim_no and tclaim_header.salespointcd = tmst_claim.salespointcd where clh_no = '" + lbclaim.Text + "'");
        txto.Text = bll.vLookUp("select (vendor_cd+' - '+vendor_nm) as vendor from tmst_vendor where vendor_cd = '" + vendor + "'");
        txto.Attributes.Add("readonly", "readonly");
        txremarkupd.Text = bll.vLookUp("select remark from tclaim_header where clh_no = '" + lbclaim.Text + "'");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@vendor_cd", vendor));
        bll.vBindingListToSp(ref lstAttn, "sp_tproposal_signbyvendor_get", "fullname", "fullname", arr);
        arr.Clear();
        bll.vBindingListToSp(ref lstCC, "sp_tproposal_signoff_get", "emp_desc", "emp_desc", arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#sendingClaim').modal();", true);
        upModal.Update();
    }

    protected void btSendClaim_Click(object sender, EventArgs e)
    {
        string clHeaderNo = "", listAttn = "", listCC = ""; string[] vendor;
        int i = 1;
        vendor = txto.Text.Split('-');
        System.Data.SqlClient.SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
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
        if (listAttn == "" || listCC == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attn or CC Must be Selected','Cover Letter','warning');", true);
            return;
        }
        arr.Clear();
        arr.Add(new cArrayList("@clh_no", lbclaim.Text));
        arr.Add(new cArrayList("@att", listAttn));
        arr.Add(new cArrayList("@cc", listCC));
        arr.Add(new cArrayList("@remarks", txremarkupd.Text.ToString()));
        bll.vUpdateCoverLetter(arr);
        lbremark.Text = txremarkupd.Text.ToString();
    }


    protected void grdvat_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "print")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdvat.Rows[index];
            Label lbinvno = (Label)grdvat.Rows[index].FindControl("lbinvno");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=clhvatinv&inv=" + lbinvno.Text + "');", true);
        }
        else if (e.CommandName == "editData")
        {
            btsavevat.Visible = true;
            btsavevat.Enabled = true;
            btsavevat.Attributes.Remove("style");
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdvat.Rows[index];
            Label lbinvno = (Label)grdvat.Rows[index].FindControl("lbinvno");
            Label lbpropno = (Label)grdvat.Rows[index].FindControl("lbpropno");
            HiddenField lbtotalamountinv = (HiddenField)grdvat.Rows[index].FindControl("lbtotalamountinv");
            HiddenField lbamountinv = (HiddenField)grdvat.Rows[index].FindControl("lbamountinv");
            HiddenField lbvatinv = (HiddenField)grdvat.Rows[index].FindControl("lbvatinv");

            lbEditTitle.Text = "Edit Vat Invoice";
            btsavevat.Text = "Save";
            txvatinvoice.Text = lbinvno.Text;
            txvatproposal.Text = lbpropno.Text;
            txtotalamountinv.Text = lbamountinv.Value.ToString();
            txtotalvatinv.Text = lbvatinv.Value.ToString();
            txtotalamountvatinv.Text = lbtotalamountinv.Value.ToString();

            txvatinvoice.Attributes.Add("readonly", "readonly");
            txvatproposal.Attributes.Add("readonly", "readonly");
            txtotAppAmount.Attributes.Add("readonly", "readonly");
            txtotalvatinv.Attributes.Add("readonly", "readonly");
            txtotalamountvatinv.Attributes.Add("readonly", "readonly");

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myEdit').modal();", true);
            editModal.Update();
        }
    }
    protected void btsavevat_Click(object sender, EventArgs e)
    {
        btsavevat.Visible = false;
        btsavevat.Enabled = false;
        btsavevat.Attributes.Add("style", "display:none");

        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@inv_no", txvatinvoice.Text));
        arr.Add(new cArrayList("@amount", txtotalamountinv.Text));
        arr.Add(new cArrayList("@vat", txtotalvatinv.Text));
        arr.Add(new cArrayList("@total", txtotalamountvatinv.Text));
        arr.Add(new cArrayList("@editby", Request.Cookies["usr_id"].Value.ToString()));
        bll.vInsEditVatInvoice(arr);
        arr.Clear();

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myEdit').modal('hide');", true);
        editModal.Update();
        Response.Redirect(Request.Url.AbsoluteUri);

    }
    protected void txtotalamountinv_TextChanged(object sender, EventArgs e)
    {
        txtotalvatinv.Text = (Double.Parse(txtotalamountinv.Text.ToString()) * 0.05).ToString();
        txtotalamountvatinv.Text = (Double.Parse(txtotalvatinv.Text.ToString())+Double.Parse(txtotalamountinv.Text.ToString())).ToString();
    }
    protected void grddocument_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow) return;
        HiddenField hddoc_cd = (HiddenField)e.Row.FindControl("hddoc_cd");

        if (hddoc_cd.Value == "P02")
        {
            Label lbfilelocproposal = (Label)e.Row.FindControl("lbfilelocproposal");
            Label lbfileloc = (Label)e.Row.FindControl("lbfileloc");
            lbfileloc.Visible = false;
            lbfilelocproposal.Visible = true;
        }
        else
        {
            Label lbfilelocproposal = (Label)e.Row.FindControl("lbfilelocproposal");
            Label lbfileloc = (Label)e.Row.FindControl("lbfileloc");
            lbfileloc.Visible = true;
            lbfilelocproposal.Visible = false;
        }
    }

    protected void grdinvoice_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdinvoice.PageIndex = e.NewPageIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@sp", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@search", Request.QueryString["dc"]));
        bll.vBindingGridToSp(ref grdinvoice, "sp_report_invoice_claim2", arr);
    }

    protected void grdcontract_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdcontract.PageIndex = e.NewPageIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@sp", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@search", Request.QueryString["dc"]));
        bll.vBindingGridToSp(ref grdcontract, "sp_report_contract_claim", arr);
    }
}