using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.IO;


//public class Budget
//{
//    public string prodCd { get; set; }
//    public double target { get; set; } 
//}


public partial class fm_proposal : System.Web.UI.Page
{
    //var list = (List<Budget>)Session["budgetTotal"];
    //List<Budget> bgt = new List<Budget>();
    //Session["var"] = bgt;
    cbll bll = new cbll();
    
    public string proposalNo = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["edit"] = "";
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        {
            //btedit.Enabled = false;
            txpropno.CssClass = "divnormal";
            cbappvendor.Visible = false;
            List<cArrayList> arr = new List<cArrayList>();
            List<cArrayList> arr1 = new List<cArrayList>();
            arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
            bll.vDelProposalItem(arr);
            bll.vDelProposalProduct(arr);
            bll.vDelProposalCustomer(arr);
            bll.vDelProposalCusgrcd(arr);
            // start @05062016 By Nico
            bll.vDelProposalPayment(arr);
            bll.vDelProposalMechRebate(arr);
            bll.vDelProposalMechOtherPromo(arr);
            bll.vDelProposalMechDisplayRent(arr);
            bll.vDelProposalMechFee(arr);
            // end
            // start @19062016 By Nico
            bll.vDelProposalProductGroup(arr);
            bll.vDelProposalSign(arr);
            // end
            // start @03082017 By Nico 
            bll.vDelWrkFreeItem2(arr);
            bll.vDelWrkFreeProduct2(arr);
            // end 
            txsearchitem.CssClass = "divhid";
            cbgroup.CssClass = "divhid";
            grditem.CssClass = "divgrid";
            grdgroup.CssClass = "divgrid";
            grditem.Visible = true;
            grdgroup.Visible = true;
            grdviewitem.Visible = true;
            grdviewitem.CssClass = "divgrid";
            grdviewgroup.Visible = true;
            grdviewgroup.CssClass = "divgrid";
            //grditem.Visible = false;
            //grdgroup.Visible = false;
            cbcusgrcd.CssClass = "divhid";
            txsearchcust.CssClass = "divhid";
            // start @28052016 By Nico
            cbbgitem.CssClass = "divhid";            
            // end
            // start @31052016 By Nico
            // salespoint region
            bll.vBindingRegionToCombo(ref cbregion);
            cbregion.Items.Insert(0, "Kingdom");
            cbregion_SelectedIndexChanged(sender, e);
            //bll.vBindingSalespointToCombo(ref cbsalespoint);
            //cbsalespoint.Items.Insert(0, "ALL | SBTC KINGDOM");
            bll.vDelProposalSalespoint(arr);
            // end
            grdcust.CssClass = "divhid";
            grdcusgrcd.CssClass = "divhid";
            grdcusttype.CssClass = "divhid";
            bll.vBindingComboToSp(ref cbvendor, "sp_tmst_vendor_get", "vendor_cd","vendor_nm");
            //bll.vBindingFieldValueToCombo(ref cbuom, "uom");
            bll.vBindingFieldValueToCombo(ref cbpaymentterm, "prop_term");
            //bll.vBindingFieldValueToCombo(ref cbbudgettype, "rdbudget");
            bll.vBindingFieldValueToCombo(ref cbmarketingcost, "rdcost");
            bll.vBindingFieldValueToCombo(ref cbpaymenttype, "rdpayment");
            arr.Clear();
            arr.Add(new cArrayList("@promokind", cbpromokind.SelectedValue.ToString()));
            bll.vBindingComboToSp(ref cbpromogroup, "sp_tmst_promotion_get", "promo_cd", "promo_nm", arr);
            arr.Clear();
            arr.Add(new cArrayList("@job_cd", "CAP"));
            bll.vBindingComboToSp(ref cbclaimdepthead, "sp_tproposal_signoff_get","ids","emp_desc", arr);
            arr.Clear();
            arr.Add(new cArrayList("@job_cd", "APC"));
            bll.vBindingComboToSp(ref cbapcoordinator, "sp_tproposal_signoff_get", "ids", "emp_desc", arr);
            arr.Clear();
            arr.Add(new cArrayList("@job_cd", "GMSBTC"));
            bll.vBindingComboToSp(ref cbgm, "sp_tproposal_signoff_get", "ids", "emp_desc", arr);
            arr.Clear();
            arr.Add(new cArrayList("@job_cd", "PRODMGR"));
            bll.vBindingComboToSp(ref cbprodman, "sp_tproposal_signoff_get", "ids", "emp_desc", arr);
            arr.Clear();
            arr.Add(new cArrayList("@job_cd", "KAMGR"));
            bll.vBindingComboToSp(ref cbkamgr, "sp_tproposal_signoff_get", "ids", "emp_desc", arr);
            arr.Clear();
            arr.Add(new cArrayList("@job_cd", "MARKETMGR"));
            bll.vBindingComboToSp(ref cbmarketmgr, "sp_tproposal_signoff_get", "ids", "emp_desc", arr);
            arr.Clear();
            arr.Add(new cArrayList("@doc_typ", "proposal"));

            // SignProposal by Principal @12062016 By Nico
            arr.Clear();
            arr.Add(new cArrayList("@position", "MARKETMGR"));
            arr.Add(new cArrayList("@vendor_cd", cbvendor.SelectedValue.ToString()));
            bll.vBindingComboToSp(ref cbmarketmgrpcp, "sp_tproposal_signbyvendor_get", "idv", "fullname", arr);
            arr.Clear();
            arr.Add(new cArrayList("@position", "NSPM"));
            arr.Add(new cArrayList("@vendor_cd", cbvendor.SelectedValue.ToString()));
            bll.vBindingComboToSp(ref cbnspmpcp, "sp_tproposal_signbyvendor_get", "idv", "fullname", arr);
            arr.Clear();
            arr.Add(new cArrayList("@position", "GMDIR"));
            arr.Add(new cArrayList("@vendor_cd", cbvendor.SelectedValue.ToString()));
            bll.vBindingComboToSp(ref cbgmpcp, "sp_tproposal_signbyvendor_get", "idv", "fullname", arr);
            arr.Clear();
            arr.Add(new cArrayList("@position", "FINDEP"));
            arr.Add(new cArrayList("@vendor_cd", cbvendor.SelectedValue.ToString()));
            bll.vBindingComboToSp(ref cbfinpcp, "sp_tproposal_signbyvendor_get", "idv", "fullname", arr);
            arr.Clear();
            arr.Add(new cArrayList("@position", "MARKETDIR"));
            arr.Add(new cArrayList("@vendor_cd", cbvendor.SelectedValue.ToString()));
            bll.vBindingComboToSp(ref cbmarketingdirpcp, "sp_tproposal_signbyvendor_get", "idv", "fullname", arr);
            // SignProposal by Principal

            
            // Dihilangkan @25-05-2016 By Nico
            //bll.vBindingComboToSp(ref cbsbtcapp, "sp_tapprovalpattern_getproposal","emp_cd","emp_desc", arr);
            
            rditem_SelectedIndexChanged(sender, e);
            rdcust_SelectedIndexChanged(sender, e);
            //dtstart.Text= Request.Cookies["waz_dt"].Value.ToString();
            //dtend.Text = Request.Cookies["waz_dt"].Value.ToString();
            //dtdelivery.Text = Request.Cookies["waz_dt"].Value.ToString();
            //dtprop.Text = Request.Cookies["waz_dt"].Value.ToString();
            //dtagree.Text = Request.Cookies["waz_dt"].Value.ToString();
            btaddcust.CssClass = "button2 add";
            btadd.CssClass = "button2 add";
            cbmarketingcost_SelectedIndexChanged(sender, e);
            cbbudgettype_SelectedIndexChanged(sender, e);
            cbpromogroup_SelectedIndexChanged(sender, e);
            cbpaymentterm_SelectedIndexChanged(sender, e);
            chagreement_CheckedChanged(sender, e);
            txcostsbtc.Text = "";
            txprincipalcost.Text = ""; 
            txpropvendor.Text = "";

            // start @04062016 By Nico
            // Table Mechanism
            tblRebate.Visible = false;
            tblOther.Visible = false;
            tblRent.Visible = false;
            tblFee.Visible = false;
            tblCar.Visible = false;
            tblCook.Visible = false;
            tblSignBoard.Visible = false;
            tblChep.Visible = false;
            // end

            // start @06062016 By Nico
            cbbgitem.CssClass = "divnormal";
            txbgitempercent.Visible = false;
            lblbgitem.Visible = false;
            lblbgtitleitem.Visible = false;
            txbgitemtarget.Visible = false;
            cbbgitemtarget.Visible = false;
            // end

            // Konfigurasi Button 
            btprint.Visible = false;
            btprint2.Visible = false;
            btedit.Visible = false;
            btapprove.Visible = false;
            btcancel.Visible = false;
            btupdate.Visible = false;
            btdelete.Visible = false;
            //btsave.Visible = false;
            // End Konfigurasi Button
            txfreegood.CssClass = "divnormal";

            // Date Configuration 
            //dtstart_CalendarExtender.StartDate = DateTime.Now;
            dtstart.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Now);
            dtprop.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Now);
            dtend.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Now);
            //dtdelivery_CalendarExtender.StartDate = DateTime.Now.AddDays(-7);
            dtdelivery.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Now.AddDays(-7));
            dtclaim_CalendarExtender.StartDate = DateTime.Now.AddDays(10);
            dtclaim.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Now.AddDays(10));
            dtagree.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Now);
            //dtadjuststart.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Now);
            //dtadjustend.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Now);
            // End Date Configuration

            // Session Configuration
            Session["freeproduct"] = 0.0;
            Session["budgetLimit"] = 0.0;
            Session["edit"] = "";
            Session["hdprop"] = "";
            Session["cust"] = "";
            Session["addproduct"] = "";
            // End Session Configuration

            //txbudgetmtd.CssClass = "ro";
            //txbudgetytd.CssClass = "ro";
            txbudgetmtd.Text = "0.0";
            txbudgetytd.Text = "0.0";
            txaddbudget.Text = "0.0";
            txbudget.Text = "0";
            txfreegood.Text = "0";
            
            // Proposal Sign Add Null Value 
            cbapcoordinator.Items.Insert(0, "NULL");
            cbprodman.Items.Insert(0, "NULL");
            cbgm.Items.Insert(0, "NULL");
            cbclaimdepthead.Items.Insert(0, "NULL");
            cbkamgr.Items.Insert(0, "NULL");
            cbmarketmgr.Items.Insert(0, "NULL");
            cbmarketmgrpcp.Items.Insert(0, "NULL");
            cbnspmpcp.Items.Insert(0, "NULL");
            cbgmpcp.Items.Insert(0, "NULL");
            cbfinpcp.Items.Insert(0, "NULL");
            cbmarketingdirpcp.Items.Insert(0, "NULL");
            // End Proposal Sign Add Null Value

            tblGroupBudget.Visible = false;

            // Table Row Show
            lblpayment.Visible = false;
            lbldotpayment.Visible = false;
            grdpayment.Visible = false;
            cbpaymentterm.Visible = false;
            //showDocument.Visible = false;   // List Documents
            // End Table Row Show

            // Set Proposal Vendor and Reference 
            txpropvendor.Text = "None";
            txrefno.Text = "None";

            // Upload File
            //upl.Visible = false;
            btupload.Visible = false;
            grdcate.Visible = false;
            btnuploaddoc.Visible = false;
            grddoc.Visible = false;

            // Freegood budgeting
            chfreegood_CheckedChanged(sender, e);
            arr.Clear();
            arr.Add(new cArrayList("@level_no", 1));
            bll.vBindingComboToSp(ref cbbrandedfree, "sp_tmst_product_get", "prod_cd", "prod_nm", arr);
            cbbrandedfree_SelectedIndexChanged(sender, e);
            arr.Clear();

            // Status Proposal
            bll.vBindingComboToSp(ref cbstatus, "sp_tmst_proposal_status", "status_no", "status_nm");
            viewStatus.Visible = false;

            // 
            grdfreeitem.Visible = true;

            txreqdisc.Enabled = false;
        }
    }

    protected void btdate_Click(object sender, EventArgs e)
    {
        DateTime dt = DateTime.ParseExact(dtstart.Text.ToString(), "dd/MM/yyyy", null);
        //dtdelivery_CalendarExtender.StartDate = dt.AddDays(-7);
        dtdelivery.Text = String.Format("{0:dd/MM/yyyy}", dt.AddDays(-7));
    }

    protected void btend_Click(object sender, EventArgs e)
    {
        DateTime dt = DateTime.ParseExact(dtend.Text.ToString(), "dd/MM/yyyy", null);
        //dtclaim_CalendarExtender.StartDate = dt.AddDays(10);
        dtclaim.Text = String.Format("{0:dd/MM/yyyy}", dt.AddDays(10));
    }

    protected void rditem_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (rditem.SelectedValue.ToString() == "I")
        {
            cbgroup.CssClass = "divhid"; txsearchitem.CssClass = "divnormal"; cbbgitem.CssClass = "divnormal";
            //cbfreegroup.CssClass = "divhid"; txsearchfreeitem.CssClass = "divnormal"; 
            grditem.CssClass = "divgrid";
            grdgroup.CssClass = "divhid";
            grdfreeitem.CssClass = "divgrid";
            //grdfreegroup.CssClass = "divhid";
            //grditem.Visible = true;
            //grdgroup.Visible = false;
            //arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
            //bll.vBindingGridToSp(ref grditem, "sp_tproposal_item_get", arr);
            //cbbgitem.CssClass = "ro";
            cbpromogroup_SelectedIndexChanged(sender, e);

        }
        else if (rditem.SelectedValue.ToString()=="G")
        {
            cbgroup.CssClass = "divnormal"; txsearchitem.CssClass = "divhid"; cbbgitem.CssClass = "divnormal";
            //cbfreegroup.CssClass = "divnormal"; txsearchfreeitem.CssClass = "divhid"; 
            arr.Clear();
            //arr.Add(new cArrayList("@level_no", 3));
            arr.Add(new cArrayList("@vendor_cd", cbvendor.SelectedValue.ToString()));
            bll.vBindingComboToSp(ref cbgroup, "sp_tmst_product_getbyvendor", "prod_cd","prod_nm", arr);
            //bll.vBindingComboToSp(ref cbfreegroup, "sp_tmst_product_getbyvendor", "prod_cd", "prod_nm", arr);
            grdgroup.CssClass = "divgrid";
            grditem.CssClass = "divhid";
            grdfreeitem.CssClass = "divhid";
            //grdfreegroup.CssClass = "divgrid";
            //grdgroup.Visible = true;
            //grditem.Visible = false;
            //cbbgitem.CssClass = "ro";
            cbpromogroup_SelectedIndexChanged(sender, e);
        }
    }
     protected void btadd_Click(object sender, EventArgs e)
    {
        Session["addproduct"] = "true";
        double totalBudget = 0.0;
        double totalFree = 0.0;
        grditem.Visible = true;
        grdgroup.Visible = true;
        tblGroupBudget.Visible = true;
        if (rditem.SelectedValue.ToString() == "I")
        {
            if (hditem.Value.ToString() == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Item Not yet selcted','select Item','warning');", true);
                return;
            }
        }
        List<cArrayList> arr = new List<cArrayList>();
        if (rditem.SelectedValue.ToString()=="I")
        {
            if (cbbgitem.SelectedValue.ToString() == "A")
            {
                arr.Add(new cArrayList("@x", txbgitemx.Text.ToString()));
                arr.Add(new cArrayList("@y", txbgitemy.Text.ToString()));
                arr.Add(new cArrayList("@target", txbgitemtarget.Text.ToString()));
            }
            else if(cbbgitem.SelectedValue.ToString() == "B")
            {
                arr.Add(new cArrayList("@x", txbgitempercent.Text.ToString()));
                arr.Add(new cArrayList("@y", txbgitemtarget.Text.ToString()));
                arr.Add(new cArrayList("@target", cbbgitemtarget.SelectedValue.ToString()));
            }
            else if (cbbgitem.SelectedValue.ToString() == "D")
            {
                arr.Add(new cArrayList("@x", txbgitempercent.Text.ToString()));
                arr.Add(new cArrayList("@y", "0"));
                arr.Add(new cArrayList("@target", ""));
            }
            else if (cbbgitem.SelectedValue.ToString() == "C")
            {
                arr.Add(new cArrayList("@x", txbgdirect.Text.ToString()));
                arr.Add(new cArrayList("@y", txmaxdirect.Text.ToString()));
                arr.Add(new cArrayList("@target", ""));
            }
            else
            {
                arr.Add(new cArrayList("@x", txbgdirect.Text.ToString()));
                arr.Add(new cArrayList("@y", "0"));
                arr.Add(new cArrayList("@target", ""));
            }
            arr.Add(new cArrayList("@item_cd", hditem.Value.ToString()));
            arr.Add(new cArrayList("@item_bg", cbbgitem.SelectedValue.ToString()));
                if (hdprop.Value.ToString() == "")
                {
                    arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
                }
                else
                {
                    arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
                }
            bll.vInsertProposalItem(arr); arr.Clear();
            if (cbbgitem.SelectedValue.ToString() == "A")
            {
                arr.Add(new cArrayList("@x", txbgitemx.Text.ToString()));
                arr.Add(new cArrayList("@y", txbgitemy.Text.ToString()));
                arr.Add(new cArrayList("@target", txbgitemtarget.Text.ToString()));
            }
            else if (cbbgitem.SelectedValue.ToString() == "B")
            {
                arr.Add(new cArrayList("@x", txbgitempercent.Text.ToString()));
                arr.Add(new cArrayList("@y", txbgitemtarget.Text.ToString()));
                arr.Add(new cArrayList("@target", cbbgitemtarget.SelectedValue.ToString()));
                if (cbpromogroup.SelectedValue.ToString() == "RB")
                {
                    Session["budgetLimit"] = double.Parse(txbgitemtarget.Text.ToString());
                    txbudget.Text = Session["budgetLimit"].ToString();
                    Session["budgetLimit"] = 0.0;
                    System.Data.SqlClient.SqlDataReader rs = null;
                    List<cArrayList> arr1 = new List<cArrayList>();
                        if (hdprop.Value.ToString() == "")
                        {
                            arr1.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
                        }
                        else
                        {
                            arr1.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
                        }
                    bll.vGetBudgetItemProposal(arr1, ref rs);
                    while (rs.Read())
                    {
                        totalFree = totalFree + double.Parse(rs["freegood"].ToString());
                    }
                    arr1.Clear();
                    txfreegood.Text = Convert.ToString(totalFree);
                    cbpaymenttype_SelectedIndexChanged(sender, e);
                    txtrgtsales.Text = txbgitemtarget.Text.ToString();
                    txtrgtcost.Text = Convert.ToString(totalFree);
                }
                else if (cbpromogroup.SelectedValue.ToString() == "TB")
                {
                    System.Data.SqlClient.SqlDataReader rs = null;
                    List<cArrayList> arr1 = new List<cArrayList>();
                        if (hdprop.Value.ToString() == "")
                        {
                            arr1.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
                        }
                        else
                        {
                            arr1.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
                        }
                    bll.vGetBudgetItemProposal(arr1, ref rs);
                    while (rs.Read())
                    {
                        totalBudget = totalBudget + double.Parse(rs["y"].ToString());
                        totalFree = totalFree + double.Parse(rs["freegood"].ToString());
                    }
                    arr1.Clear();
                    txbudget.Text = Convert.ToString(totalBudget);
                    txfreegood.Text = Convert.ToString(totalFree);
                    //cbpaymenttype_SelectedIndexChanged(sender, e);
                }
            }
            else if (cbbgitem.SelectedValue.ToString() == "D")
            {
                arr.Add(new cArrayList("@x", txbgitempercent.Text.ToString()));
                arr.Add(new cArrayList("@y", "0"));
                arr.Add(new cArrayList("@target", ""));
            }
            else if (cbbgitem.SelectedValue.ToString() == "C")
            {
                Session["budgetLimit"] = 0.0;
                arr.Add(new cArrayList("@x", txbgdirect.Text.ToString()));
                arr.Add(new cArrayList("@y", txmaxdirect.Text.ToString()));
                arr.Add(new cArrayList("@target", ""));
                //Session["budgetLimit"] = double.Parse(txmaxdirect.Text.ToString());
                txbudget.Text = txmaxdirect.Text.ToString();
                if ((double.Parse(txbgdirect.Text.ToString())) >= (double.Parse(txfreegood.Text.ToString())))
                {
                    txfreegood.Text = txbgdirect.Text.ToString();
                }                
                txcostfeesr.Text = Session["budgetLimit"].ToString();
                txvaluechep.Text = Session["budgetLimit"].ToString();
            }
            else
            {
                Session["budgetLimit"] = 0.0;
                txbudget.Text = "0";
                txfreegood.Text = "0";
                txtotalbudget.Text = txbgdirect.Text.ToString();
                arr.Add(new cArrayList("@x", txbgdirect.Text.ToString()));
                arr.Add(new cArrayList("@y", "0"));
                arr.Add(new cArrayList("@target", ""));
            }
            arr.Add(new cArrayList("@bgitem", cbbgitem.SelectedValue.ToString()));
            arr.Add(new cArrayList("@cust", rdcust.SelectedValue.ToString()));
                if (hdprop.Value.ToString() == "")
                {
                    arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
                }
                else
                {
                    arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
                }
            bll.vBindingGridToSp(ref grditem, "sp_tproposal_item_get", arr);
            grditem.CssClass = "divgrid";
            grditem.Visible = true;
            txsearchitem.Text = "";
            grdviewitem.CssClass = "divhid";
            grdviewitem.Visible = false;
        }
        else if (rditem.SelectedValue.ToString() == "G")
        {
            if (cbbgitem.SelectedValue.ToString() == "A")
            {
                arr.Add(new cArrayList("@x", txbgitemx.Text.ToString()));
                arr.Add(new cArrayList("@y", txbgitemy.Text.ToString()));
                arr.Add(new cArrayList("@target", txbgitemtarget.Text.ToString()));
            }
            else if (cbbgitem.SelectedValue.ToString() == "B")
            {
                arr.Add(new cArrayList("@x", txbgitempercent.Text.ToString()));
                arr.Add(new cArrayList("@y", txbgitemtarget.Text.ToString()));
                arr.Add(new cArrayList("@target", cbbgitemtarget.SelectedValue.ToString()));
            }
            else if (cbbgitem.SelectedValue.ToString() == "D")
            {
                arr.Add(new cArrayList("@x", txbgitempercent.Text.ToString()));
                arr.Add(new cArrayList("@y", "0"));
                arr.Add(new cArrayList("@target", ""));
            }
            else if (cbbgitem.SelectedValue.ToString() == "C")
            {
                arr.Add(new cArrayList("@x", txbgdirect.Text.ToString()));
                arr.Add(new cArrayList("@y", txmaxdirect.Text.ToString()));
                arr.Add(new cArrayList("@target", ""));
            }
            else
            {
                arr.Add(new cArrayList("@x", txbgdirect.Text.ToString()));
                arr.Add(new cArrayList("@y", "0"));
                arr.Add(new cArrayList("@target", ""));
            }
            arr.Add(new cArrayList("@prod_cd", cbgroup.SelectedValue.ToString()));
            arr.Add(new cArrayList("@prod_bg", cbbgitem.SelectedValue.ToString()));
                if (hdprop.Value.ToString() == "")
                {
                    arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
                }
                else
                {
                    arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
                }
            bll.vInsertProposalProduct(arr); arr.Clear();
            if (cbbgitem.SelectedValue.ToString() == "A")
            {
                arr.Add(new cArrayList("@x", txbgitemx.Text.ToString()));
                arr.Add(new cArrayList("@y", txbgitemy.Text.ToString()));
                arr.Add(new cArrayList("@target", txbgitemtarget.Text.ToString()));
            }
            else if (cbbgitem.SelectedValue.ToString() == "B")
            {
                arr.Add(new cArrayList("@x", txbgitempercent.Text.ToString()));
                arr.Add(new cArrayList("@y", txbgitemtarget.Text.ToString()));
                arr.Add(new cArrayList("@target", cbbgitemtarget.SelectedValue.ToString()));
                if (cbpromogroup.SelectedValue.ToString() == "RB")
                {
                    Session["budgetLimit"] = double.Parse(txbgitemtarget.Text.ToString());
                    txbudget.Text = Session["budgetLimit"].ToString();
                    Session["budgetLimit"] = 0.0;
                    System.Data.SqlClient.SqlDataReader rs = null;
                    List<cArrayList> arr1 = new List<cArrayList>();
                        if (hdprop.Value.ToString() == "")
                        {
                            arr1.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
                        }
                        else
                        {
                            arr1.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
                        }
                    bll.vGetBudgetProductProposal(arr1, ref rs);
                    while (rs.Read())
                    {
                        totalFree = totalFree + double.Parse(rs["freegood"].ToString());
                    }
                    arr1.Clear();
                    txfreegood.Text = Convert.ToString(totalFree);
                    txtrgtsales.Text = txbgitemtarget.Text.ToString();
                    txtrgtcost.Text = Convert.ToString(totalFree);
                    //cbpaymenttype_SelectedIndexChanged(sender, e);
                }
                else if (cbpromogroup.SelectedValue.ToString() == "TB")
                {
                    System.Data.SqlClient.SqlDataReader rs = null;
                    List<cArrayList> arr1 = new List<cArrayList>();
                        if (hdprop.Value.ToString() == "")
                        {
                            arr1.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
                        }
                        else
                        {
                            arr1.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
                        }
                    bll.vGetBudgetProductProposal(arr1, ref rs);
                    while (rs.Read())
                    {
                        totalBudget = totalBudget + double.Parse(rs["y"].ToString());
                        totalFree = totalFree + double.Parse(rs["freegood"].ToString());
                    }
                    arr1.Clear();
                    txbudget.Text = Convert.ToString(totalBudget);
                    txfreegood.Text = Convert.ToString(totalFree);
                    //cbpaymenttype_SelectedIndexChanged(sender, e);
                }
            }
            else if (cbbgitem.SelectedValue.ToString() == "D")
            {
                arr.Add(new cArrayList("@x", txbgitempercent.Text.ToString()));
                arr.Add(new cArrayList("@y", "0"));
                arr.Add(new cArrayList("@target", ""));
            }
            else if (cbbgitem.SelectedValue.ToString() == "C")
            {
                Session["budgetLimit"] = 0.0;
                arr.Add(new cArrayList("@x", txbgdirect.Text.ToString()));
                arr.Add(new cArrayList("@y", txmaxdirect.Text.ToString()));
                arr.Add(new cArrayList("@target", ""));
                //Session["budgetLimit"] = double.Parse(txmaxdirect.Text.ToString());
                txbudget.Text = txmaxdirect.Text.ToString();
                if ((double.Parse(txbgdirect.Text.ToString())) >= (double.Parse(txfreegood.Text.ToString())))
                {
                    txfreegood.Text = txbgdirect.Text.ToString();
                }
                txcostfeesr.Text = Session["budgetLimit"].ToString();
                txvaluechep.Text = Session["budgetLimit"].ToString();
            }
            else
            {
                Session["budgetLimit"] = 0.0;
                txbudget.Text = "0";
                txfreegood.Text = "0";
                txtotalbudget.Text = txbgdirect.Text.ToString();
                arr.Add(new cArrayList("@x", txbgdirect.Text.ToString()));
                arr.Add(new cArrayList("@y", "0"));
                arr.Add(new cArrayList("@target", ""));           
            }
            arr.Add(new cArrayList("@prod_bg", cbbgitem.SelectedValue.ToString()));
            arr.Add(new cArrayList("@cust", rdcust.SelectedValue.ToString()));
                if (hdprop.Value.ToString() == "")
                {
                    arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
                }
                else
                {
                    arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
                }
            bll.vBindingGridToSp(ref grdgroup, "sp_tproposal_productgroup_get", arr);
            grdgroup.CssClass = "divgrid";
            grdgroup.Visible = true;
            grdviewgroup.CssClass = "divhid";
            grdviewgroup.Visible = false;
        }

        if (a1.Text == "a1")
        {
            // Get Column For Group Budget
            string propCode = string.Empty;
            string ska1 = ""; string ska2 = ""; string ska3 = ""; string ska4 = "";
            string ska5 = ""; string ska6 = ""; string ska7 = ""; string ska8 = ""; string ska9 = "";
            string snka1 = ""; string snka2 = ""; string snka3 = ""; string snka4 = "";
            string snka5 = ""; string snka6 = ""; string snka7 = ""; string snka8 = ""; string snka9 = ""; 
            string st1 = ""; string st2 = ""; string st3 = ""; string st4 = "";
            string st5 = ""; string st6 = ""; string st7 = ""; string st8 = ""; string st9 = ""; 
            System.Data.SqlClient.SqlDataReader rsProp = null;
            List<cArrayList> arrProp = new List<cArrayList>();
                if (hdprop.Value.ToString() == "")
                {
                    arrProp.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
                }
                else
                {
                    arrProp.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
                }
            arrProp.Add(new cArrayList("@rditem", rditem.SelectedValue.ToString()));
            arrProp.Add(new cArrayList("@promo", cbpromogroup.SelectedValue.ToString()));
            bll.vGetColumnBudget(arrProp, ref rsProp);
            int numRows = 0;
            List<string> lColumn = new List<string>();
            while (rsProp.Read())
            {
                lColumn.Add(rsProp["group_budget"].ToString());
            }
            string[] arrColumn = lColumn.ToArray();
            numRows = arrColumn.Length;

            if (rditem.SelectedValue.ToString() == "I")
            {
                propCode = bll.vLookUp("select distinct prod_code from tmst_product_code where prod_cd IN (select prod_cd_parent from tmst_product where prod_cd in (select distinct prod_cd from tmst_item where item_cd IN (select item_cd from tproposal_item where prop_no='" + Request.Cookies["usr_id"].Value.ToString() + "')))");
            }
            else
            {
                propCode = bll.vLookUp("select distinct prod_code from tmst_product_code where prod_cd IN (select prod_cd_parent from tmst_product where prod_cd in (select prod_cd from tproposal_product where prop_no='" + Request.Cookies["usr_id"].Value.ToString() + "'))");
            }

            if (numRows == 1)
            {
                ska1 = bll.vLookUp("EXEC sp_tproposal_budget_balance @prop_code='"+propCode+"', @vendor='" + cbvendor.SelectedValue.ToString() + "', @year='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year + "', @month='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month + "', @type='" + arrColumn[0].ToString() + "', @outlet='KA'").ToString();
                snka1 = bll.vLookUp("EXEC sp_tproposal_budget_balance @prop_code='"+propCode+"', @vendor='" + cbvendor.SelectedValue.ToString() + "', @year='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year + "', @month='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month + "', @type='" + arrColumn[0].ToString() + "', @outlet='NKA'").ToString();
                st1 = (double.Parse((ska1 == "" ? "0" : ska1)) + double.Parse((snka1 == "" ? "0" : snka1))).ToString();
                a2.Visible = false; a3.Visible = false; a4.Visible = false; a5.Visible = false; a6.Visible = false; a7.Visible = false; a8.Visible = false; a9.Visible = false;
                cka2.Visible = false; cka3.Visible = false; cka4.Visible = false; cka5.Visible = false; cka6.Visible = false; cka7.Visible = false; cka8.Visible = false; cka9.Visible = false;
                cnka2.Visible = false; cnka3.Visible = false; cnka4.Visible = false; cnka5.Visible = false; cnka6.Visible = false; cnka7.Visible = false; cnka8.Visible = false; cnka9.Visible = false;
                t2.Visible = false; t3.Visible = false; t4.Visible = false; t5.Visible = false; t6.Visible = false; t7.Visible = false; t8.Visible = false; t9.Visible = false;
                a1.Text = arrColumn[0];
                ka1.Text = String.Format("{0:n}", Convert.ToDecimal((ska1 == "" ? "0" : ska1))); 
                nka1.Text = String.Format("{0:n}", Convert.ToDecimal((snka1 == "" ? "0" : snka1))); 
                t1.Text = String.Format("{0:n}", Convert.ToDecimal(st1)); 
            }
            else if (numRows == 2)
            {
                ska1 = bll.vLookUp("EXEC sp_tproposal_budget_balance @prop_code='" + propCode + "', @vendor='" + cbvendor.SelectedValue.ToString() + "', @year='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year + "', @month='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month + "', @type='" + arrColumn[0].ToString() + "', @outlet='KA'").ToString();
                snka1 = bll.vLookUp("EXEC sp_tproposal_budget_balance @prop_code='" + propCode + "', @vendor='" + cbvendor.SelectedValue.ToString() + "', @year='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year + "', @month='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month + "', @type='" + arrColumn[0].ToString() + "', @outlet='NKA'").ToString();
                ska2 = bll.vLookUp("EXEC sp_tproposal_budget_balance @prop_code='" + propCode + "', @vendor='" + cbvendor.SelectedValue.ToString() + "', @year='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year + "', @month='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month + "', @type='" + arrColumn[1].ToString() + "', @outlet='KA'").ToString();
                snka2 = bll.vLookUp("EXEC sp_tproposal_budget_balance @prop_code='" + propCode + "', @vendor='" + cbvendor.SelectedValue.ToString() + "', @year='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year + "', @month='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month + "', @type='" + arrColumn[1].ToString() + "', @outlet='NKA'").ToString();
                st1 = (double.Parse((ska1 == "" ? "0" : ska1)) + double.Parse((snka1 == "" ? "0" : snka1))).ToString();
                st2 = (double.Parse((ska2 == "" ? "0" : ska2)) + double.Parse((snka2 == "" ? "0" : snka2))).ToString();
                a3.Visible = false; a4.Visible = false; a5.Visible = false; a6.Visible = false; a7.Visible = false; a8.Visible = false; a9.Visible = false;
                cka3.Visible = false; cka4.Visible = false; cka5.Visible = false; cka6.Visible = false; cka7.Visible = false; cka8.Visible = false; cka9.Visible = false;
                cnka3.Visible = false; cnka4.Visible = false; cnka5.Visible = false; cnka6.Visible = false; cnka7.Visible = false; cnka8.Visible = false; cnka9.Visible = false;
                t3.Visible = false; t4.Visible = false; t5.Visible = false; t6.Visible = false; t7.Visible = false; t8.Visible = false; t9.Visible = false;
                a1.Text = arrColumn[0];
                a2.Text = arrColumn[1];
                ka1.Text = String.Format("{0:n}", Convert.ToDecimal((ska1 == "" ? "0" : ska1))); ka2.Text = String.Format("{0:n}", Convert.ToDecimal((ska2 == "" ? "0" : ska2)));
                nka1.Text = String.Format("{0:n}", Convert.ToDecimal((snka1 == "" ? "0" : snka1))); nka2.Text = String.Format("{0:n}", Convert.ToDecimal((snka2 == "" ? "0" : snka2)));
                t1.Text = String.Format("{0:n}", Convert.ToDecimal(st1)); t2.Text = String.Format("{0:n}", Convert.ToDecimal(st2)); 
            }
            else if (numRows == 3)
            {
                ska1 = bll.vLookUp("EXEC sp_tproposal_budget_balance @prop_code='" + propCode + "', @vendor='" + cbvendor.SelectedValue.ToString() + "', @year='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year + "', @month='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month + "', @type='" + arrColumn[0].ToString() + "', @outlet='KA'").ToString();
                snka1 = bll.vLookUp("EXEC sp_tproposal_budget_balance @prop_code='" + propCode + "', @vendor='" + cbvendor.SelectedValue.ToString() + "', @year='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year + "', @month='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month + "', @type='" + arrColumn[0].ToString() + "', @outlet='NKA'").ToString();
                ska2 = bll.vLookUp("EXEC sp_tproposal_budget_balance @prop_code='" + propCode + "', @vendor='" + cbvendor.SelectedValue.ToString() + "', @year='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year + "', @month='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month + "', @type='" + arrColumn[1].ToString() + "', @outlet='KA'").ToString();
                snka2 = bll.vLookUp("EXEC sp_tproposal_budget_balance @prop_code='" + propCode + "', @vendor='" + cbvendor.SelectedValue.ToString() + "', @year='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year + "', @month='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month + "', @type='" + arrColumn[1].ToString() + "', @outlet='NKA'").ToString();
                ska3 = bll.vLookUp("EXEC sp_tproposal_budget_balance @prop_code='" + propCode + "', @vendor='" + cbvendor.SelectedValue.ToString() + "', @year='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year + "', @month='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month + "', @type='" + arrColumn[2].ToString() + "', @outlet='KA'").ToString();
                snka3 = bll.vLookUp("EXEC sp_tproposal_budget_balance @prop_code='" + propCode + "', @vendor='" + cbvendor.SelectedValue.ToString() + "', @year='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year + "', @month='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month + "', @type='" + arrColumn[2].ToString() + "', @outlet='NKA'").ToString();
                st1 = (double.Parse((ska1 == "" ? "0" : ska1)) + double.Parse((snka1 == "" ? "0" : snka1))).ToString();
                st2 = (double.Parse((ska2 == "" ? "0" : ska2)) + double.Parse((snka2 == "" ? "0" : snka2))).ToString();
                st3 = (double.Parse((ska3 == "" ? "0" : ska3)) + double.Parse((snka3 == "" ? "0" : snka3))).ToString();
                a4.Visible = false; a5.Visible = false; a6.Visible = false; a7.Visible = false; a8.Visible = false; a9.Visible = false;
                cka4.Visible = false; cka5.Visible = false; cka6.Visible = false; cka7.Visible = false; cka8.Visible = false; cka9.Visible = false;
                cnka4.Visible = false; cnka5.Visible = false; cnka6.Visible = false; cnka7.Visible = false; cnka8.Visible = false; cnka9.Visible = false;
                t4.Visible = false; t5.Visible = false; t6.Visible = false; t7.Visible = false; t8.Visible = false; t9.Visible = false;
                a1.Text = arrColumn[0];
                a2.Text = arrColumn[1];
                a3.Text = arrColumn[2];
                ka1.Text = String.Format("{0:n}", Convert.ToDecimal((ska1 == "" ? "0" : ska1))); ka2.Text = String.Format("{0:n}", Convert.ToDecimal((ska2 == "" ? "0" : ska2))); ka3.Text = String.Format("{0:n}", Convert.ToDecimal((ska3 == "" ? "0" : ska3)));
                nka1.Text = String.Format("{0:n}", Convert.ToDecimal((snka1 == "" ? "0" : snka1))); nka2.Text = String.Format("{0:n}", Convert.ToDecimal((snka2 == "" ? "0" : snka2))); nka3.Text = String.Format("{0:n}", Convert.ToDecimal((snka3 == "" ? "0" : snka3)));
                t1.Text = String.Format("{0:n}", Convert.ToDecimal(st1)); t2.Text = String.Format("{0:n}", Convert.ToDecimal(st2)); t3.Text = String.Format("{0:n}", Convert.ToDecimal(st3)); 
            }
            else if (numRows == 4)
            {
                ska1 = bll.vLookUp("EXEC sp_tproposal_budget_balance @prop_code='" + propCode + "', @vendor='" + cbvendor.SelectedValue.ToString() + "', @year='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year + "', @month='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month + "', @type='" + arrColumn[0].ToString() + "', @outlet='KA'").ToString();
                snka1 = bll.vLookUp("EXEC sp_tproposal_budget_balance @prop_code='" + propCode + "', @vendor='" + cbvendor.SelectedValue.ToString() + "', @year='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year + "', @month='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month + "', @type='" + arrColumn[0].ToString() + "', @outlet='NKA'").ToString();
                ska2 = bll.vLookUp("EXEC sp_tproposal_budget_balance @prop_code='" + propCode + "', @vendor='" + cbvendor.SelectedValue.ToString() + "', @year='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year + "', @month='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month + "', @type='" + arrColumn[1].ToString() + "', @outlet='KA'").ToString();
                snka2 = bll.vLookUp("EXEC sp_tproposal_budget_balance @prop_code='" + propCode + "', @vendor='" + cbvendor.SelectedValue.ToString() + "', @year='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year + "', @month='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month + "', @type='" + arrColumn[1].ToString() + "', @outlet='NKA'").ToString();
                ska3 = bll.vLookUp("EXEC sp_tproposal_budget_balance @prop_code='" + propCode + "', @vendor='" + cbvendor.SelectedValue.ToString() + "', @year='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year + "', @month='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month + "', @type='" + arrColumn[2].ToString() + "', @outlet='KA'").ToString();
                snka3 = bll.vLookUp("EXEC sp_tproposal_budget_balance @prop_code='" + propCode + "', @vendor='" + cbvendor.SelectedValue.ToString() + "', @year='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year + "', @month='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month + "', @type='" + arrColumn[2].ToString() + "', @outlet='NKA'").ToString();
                ska4 = bll.vLookUp("EXEC sp_tproposal_budget_balance @prop_code='" + propCode + "', @vendor='" + cbvendor.SelectedValue.ToString() + "', @year='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year + "', @month='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month + "', @type='" + arrColumn[3].ToString() + "', @outlet='KA'").ToString();
                snka4 = bll.vLookUp("EXEC sp_tproposal_budget_balance @prop_code='" + propCode + "', @vendor='" + cbvendor.SelectedValue.ToString() + "', @year='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year + "', @month='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month + "', @type='" + arrColumn[3].ToString() + "', @outlet='NKA'").ToString();
                st1 = (double.Parse((ska1 == "" ? "0" : ska1)) + double.Parse((snka1 == "" ? "0" : snka1))).ToString();
                st2 = (double.Parse((ska2 == "" ? "0" : ska2)) + double.Parse((snka2 == "" ? "0" : snka2))).ToString();
                st3 = (double.Parse((ska3 == "" ? "0" : ska3)) + double.Parse((snka3 == "" ? "0" : snka3))).ToString();
                st4 = (double.Parse((ska4 == "" ? "0" : ska4)) + double.Parse((snka4 == "" ? "0" : snka4))).ToString();
                a5.Visible = false; a6.Visible = false; a7.Visible = false; a8.Visible = false; a9.Visible = false;
                cka5.Visible = false; cka6.Visible = false; cka7.Visible = false; cka8.Visible = false; cka9.Visible = false;
                cnka5.Visible = false; cnka6.Visible = false; cnka7.Visible = false; cnka8.Visible = false; cnka9.Visible = false;
                t5.Visible = false; t6.Visible = false; t7.Visible = false; t8.Visible = false; t9.Visible = false;
                a1.Text = arrColumn[0];
                a2.Text = arrColumn[1];
                a3.Text = arrColumn[2];
                a4.Text = arrColumn[3];
                ka1.Text = String.Format("{0:n}", Convert.ToDecimal((ska1 == "" ? "0" : ska1))); ka2.Text = String.Format("{0:n}", Convert.ToDecimal((ska2 == "" ? "0" : ska2))); ka3.Text = String.Format("{0:n}", Convert.ToDecimal((ska3 == "" ? "0" : ska3))); ka4.Text = String.Format("{0:n}", Convert.ToDecimal((ska4 == "" ? "0" : ska4)));
                nka1.Text = String.Format("{0:n}", Convert.ToDecimal((snka1 == "" ? "0" : snka1))); nka2.Text = String.Format("{0:n}", Convert.ToDecimal((snka2 == "" ? "0" : snka2))); nka3.Text = String.Format("{0:n}", Convert.ToDecimal((snka3 == "" ? "0" : snka3))); nka4.Text = String.Format("{0:n}", Convert.ToDecimal((snka4 == "" ? "0" : snka4)));
                t1.Text = String.Format("{0:n}", Convert.ToDecimal(st1)); t2.Text = String.Format("{0:n}", Convert.ToDecimal(st2)); t3.Text = String.Format("{0:n}", Convert.ToDecimal(st3)); t4.Text = String.Format("{0:n}", Convert.ToDecimal(st4)); 
            }
            else if (numRows == 5)
            {
                ska1 = bll.vLookUp("EXEC sp_tproposal_budget_balance @prop_code='" + propCode + "', @vendor='" + cbvendor.SelectedValue.ToString() + "', @year='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year + "', @month='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month + "', @type='" + arrColumn[0].ToString() + "', @outlet='KA'").ToString();
                snka1 = bll.vLookUp("EXEC sp_tproposal_budget_balance @prop_code='" + propCode + "', @vendor='" + cbvendor.SelectedValue.ToString() + "', @year='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year + "', @month='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month + "', @type='" + arrColumn[0].ToString() + "', @outlet='NKA'").ToString();
                ska2 = bll.vLookUp("EXEC sp_tproposal_budget_balance @prop_code='" + propCode + "', @vendor='" + cbvendor.SelectedValue.ToString() + "', @year='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year + "', @month='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month + "', @type='" + arrColumn[1].ToString() + "', @outlet='KA'").ToString();
                snka2 = bll.vLookUp("EXEC sp_tproposal_budget_balance @prop_code='" + propCode + "', @vendor='" + cbvendor.SelectedValue.ToString() + "', @year='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year + "', @month='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month + "', @type='" + arrColumn[1].ToString() + "', @outlet='NKA'").ToString();
                ska3 = bll.vLookUp("EXEC sp_tproposal_budget_balance @prop_code='" + propCode + "', @vendor='" + cbvendor.SelectedValue.ToString() + "', @year='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year + "', @month='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month + "', @type='" + arrColumn[2].ToString() + "', @outlet='KA'").ToString();
                snka3 = bll.vLookUp("EXEC sp_tproposal_budget_balance @prop_code='" + propCode + "', @vendor='" + cbvendor.SelectedValue.ToString() + "', @year='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year + "', @month='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month + "', @type='" + arrColumn[2].ToString() + "', @outlet='NKA'").ToString();
                ska4 = bll.vLookUp("EXEC sp_tproposal_budget_balance @prop_code='" + propCode + "', @vendor='" + cbvendor.SelectedValue.ToString() + "', @year='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year + "', @month='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month + "', @type='" + arrColumn[3].ToString() + "', @outlet='KA'").ToString();
                snka4 = bll.vLookUp("EXEC sp_tproposal_budget_balance @prop_code='" + propCode + "', @vendor='" + cbvendor.SelectedValue.ToString() + "', @year='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year + "', @month='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month + "', @type='" + arrColumn[3].ToString() + "', @outlet='NKA'").ToString();
                ska5 = bll.vLookUp("EXEC sp_tproposal_budget_balance @prop_code='" + propCode + "', @vendor='" + cbvendor.SelectedValue.ToString() + "', @year='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year + "', @month='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month + "', @type='" + arrColumn[4].ToString() + "', @outlet='KA'").ToString();
                snka5 = bll.vLookUp("EXEC sp_tproposal_budget_balance @prop_code='" + propCode + "', @vendor='" + cbvendor.SelectedValue.ToString() + "', @year='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year + "', @month='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month + "', @type='" + arrColumn[4].ToString() + "', @outlet='NKA'").ToString();
                st1 = (double.Parse((ska1 == "" ? "0" : ska1)) + double.Parse((snka1 == "" ? "0" : snka1))).ToString();
                st2 = (double.Parse((ska2 == "" ? "0" : ska2)) + double.Parse((snka2 == "" ? "0" : snka2))).ToString();
                st3 = (double.Parse((ska3 == "" ? "0" : ska3)) + double.Parse((snka3 == "" ? "0" : snka3))).ToString();
                st4 = (double.Parse((ska4 == "" ? "0" : ska4)) + double.Parse((snka4 == "" ? "0" : snka4))).ToString();
                st5 = (double.Parse((ska5 == "" ? "0" : ska5)) + double.Parse((snka5 == "" ? "0" : snka5))).ToString();
                a6.Visible = false; a7.Visible = false; a8.Visible = false; a9.Visible = false;
                cka6.Visible = false; cka7.Visible = false; cka8.Visible = false; cka9.Visible = false;
                cnka6.Visible = false; cnka7.Visible = false; cnka8.Visible = false; cnka9.Visible = false;
                t6.Visible = false; t7.Visible = false; t8.Visible = false; t9.Visible = false;
                a1.Text = arrColumn[0];
                a2.Text = arrColumn[1];
                a3.Text = arrColumn[2];
                a4.Text = arrColumn[3];
                a5.Text = arrColumn[4];
                ka1.Text = String.Format("{0:n}", Convert.ToDecimal((ska1 == "" ? "0" : ska1))); ka2.Text = String.Format("{0:n}", Convert.ToDecimal((ska2 == "" ? "0" : ska2))); ka3.Text = String.Format("{0:n}", Convert.ToDecimal((ska3 == "" ? "0" : ska3))); ka4.Text = String.Format("{0:n}", Convert.ToDecimal((ska4 == "" ? "0" : ska4))); ka5.Text = String.Format("{0:n}", Convert.ToDecimal((ska5 == "" ? "0" : ska5)));
                nka1.Text = String.Format("{0:n}", Convert.ToDecimal((snka1 == "" ? "0" : snka1))); nka2.Text = String.Format("{0:n}", Convert.ToDecimal((snka2 == "" ? "0" : snka2))); nka3.Text = String.Format("{0:n}", Convert.ToDecimal((snka3 == "" ? "0" : snka3))); nka4.Text = String.Format("{0:n}", Convert.ToDecimal((snka4 == "" ? "0" : snka4))); nka5.Text = String.Format("{0:n}", Convert.ToDecimal((snka5 == "" ? "0" : snka5)));
                t1.Text = String.Format("{0:n}", Convert.ToDecimal(st1)); t2.Text = String.Format("{0:n}", Convert.ToDecimal(st2)); t3.Text = String.Format("{0:n}", Convert.ToDecimal(st3)); t4.Text = String.Format("{0:n}", Convert.ToDecimal(st4)); t5.Text = String.Format("{0:n}", Convert.ToDecimal(st5)); 
            }
            else if (numRows == 6)
            {
                ska1 = bll.vLookUp("EXEC sp_tproposal_budget_balance @prop_code='" + propCode + "', @vendor='" + cbvendor.SelectedValue.ToString() + "', @year='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year + "', @month='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month + "', @type='" + arrColumn[0].ToString() + "', @outlet='KA'").ToString();
                snka1 = bll.vLookUp("EXEC sp_tproposal_budget_balance @prop_code='" + propCode + "', @vendor='" + cbvendor.SelectedValue.ToString() + "', @year='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year + "', @month='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month + "', @type='" + arrColumn[0].ToString() + "', @outlet='NKA'").ToString();
                ska2 = bll.vLookUp("EXEC sp_tproposal_budget_balance @prop_code='" + propCode + "', @vendor='" + cbvendor.SelectedValue.ToString() + "', @year='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year + "', @month='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month + "', @type='" + arrColumn[1].ToString() + "', @outlet='KA'").ToString();
                snka2 = bll.vLookUp("EXEC sp_tproposal_budget_balance @prop_code='" + propCode + "', @vendor='" + cbvendor.SelectedValue.ToString() + "', @year='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year + "', @month='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month + "', @type='" + arrColumn[1].ToString() + "', @outlet='NKA'").ToString();
                ska3 = bll.vLookUp("EXEC sp_tproposal_budget_balance @prop_code='" + propCode + "', @vendor='" + cbvendor.SelectedValue.ToString() + "', @year='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year + "', @month='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month + "', @type='" + arrColumn[2].ToString() + "', @outlet='KA'").ToString();
                snka3 = bll.vLookUp("EXEC sp_tproposal_budget_balance @prop_code='" + propCode + "', @vendor='" + cbvendor.SelectedValue.ToString() + "', @year='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year + "', @month='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month + "', @type='" + arrColumn[2].ToString() + "', @outlet='NKA'").ToString();
                ska4 = bll.vLookUp("EXEC sp_tproposal_budget_balance @prop_code='" + propCode + "', @vendor='" + cbvendor.SelectedValue.ToString() + "', @year='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year + "', @month='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month + "', @type='" + arrColumn[3].ToString() + "', @outlet='KA'").ToString();
                snka4 = bll.vLookUp("EXEC sp_tproposal_budget_balance @prop_code='" + propCode + "', @vendor='" + cbvendor.SelectedValue.ToString() + "', @year='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year + "', @month='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month + "', @type='" + arrColumn[3].ToString() + "', @outlet='NKA'").ToString();
                ska5 = bll.vLookUp("EXEC sp_tproposal_budget_balance @prop_code='" + propCode + "', @vendor='" + cbvendor.SelectedValue.ToString() + "', @year='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year + "', @month='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month + "', @type='" + arrColumn[4].ToString() + "', @outlet='KA'").ToString();
                snka5 = bll.vLookUp("EXEC sp_tproposal_budget_balance @prop_code='" + propCode + "', @vendor='" + cbvendor.SelectedValue.ToString() + "', @year='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year + "', @month='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month + "', @type='" + arrColumn[4].ToString() + "', @outlet='NKA'").ToString();
                ska6 = bll.vLookUp("EXEC sp_tproposal_budget_balance @prop_code='" + propCode + "', @vendor='" + cbvendor.SelectedValue.ToString() + "', @year='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year + "', @month='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month + "', @type='" + arrColumn[5].ToString() + "', @outlet='KA'").ToString();
                snka6 = bll.vLookUp("EXEC sp_tproposal_budget_balance @prop_code='" + propCode + "', @vendor='" + cbvendor.SelectedValue.ToString() + "', @year='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year + "', @month='" + DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month + "', @type='" + arrColumn[5].ToString() + "', @outlet='NKA'").ToString();
                st1 = (double.Parse((ska1 == "" ? "0" : ska1)) + double.Parse((snka1 == "" ? "0" : snka1))).ToString();
                st2 = (double.Parse((ska2 == "" ? "0" : ska2)) + double.Parse((snka2 == "" ? "0" : snka2))).ToString();
                st3 = (double.Parse((ska3 == "" ? "0" : ska3)) + double.Parse((snka3 == "" ? "0" : snka3))).ToString();
                st4 = (double.Parse((ska4 == "" ? "0" : ska4)) + double.Parse((snka4 == "" ? "0" : snka4))).ToString();
                st5 = (double.Parse((ska5 == "" ? "0" : ska5)) + double.Parse((snka5 == "" ? "0" : snka5))).ToString();
                st6 = (double.Parse((ska6 == "" ? "0" : ska6)) + double.Parse((snka6 == "" ? "0" : snka6))).ToString();
                a7.Visible = false; a8.Visible = false; a9.Visible = false;
                cka7.Visible = false; cka8.Visible = false; cka9.Visible = false;
                cnka7.Visible = false; cnka8.Visible = false; cnka9.Visible = false;
                t7.Visible = false; t8.Visible = false; t9.Visible = false;
                a1.Text = arrColumn[0];
                a2.Text = arrColumn[1];
                a3.Text = arrColumn[2];
                a4.Text = arrColumn[3];
                a5.Text = arrColumn[4];
                a6.Text = arrColumn[5];
                ka1.Text = String.Format("{0:n}", Convert.ToDecimal((ska1 == "" ? "0" : ska1))); ka2.Text = String.Format("{0:n}", Convert.ToDecimal((ska2 == "" ? "0" : ska2))); ka3.Text = String.Format("{0:n}", Convert.ToDecimal((ska3 == "" ? "0" : ska3))); ka4.Text = String.Format("{0:n}", Convert.ToDecimal((ska4 == "" ? "0" : ska4))); ka5.Text = String.Format("{0:n}", Convert.ToDecimal((ska5 == "" ? "0" : ska5))); ka6.Text = String.Format("{0:n}", Convert.ToDecimal((ska6 == "" ? "0" : ska6)));
                nka1.Text = String.Format("{0:n}", Convert.ToDecimal((snka1 == "" ? "0" : snka1))); nka2.Text = String.Format("{0:n}", Convert.ToDecimal((snka2 == "" ? "0" : snka2))); nka3.Text = String.Format("{0:n}", Convert.ToDecimal((snka3 == "" ? "0" : snka3))); nka4.Text = String.Format("{0:n}", Convert.ToDecimal((snka4 == "" ? "0" : snka4))); nka5.Text = String.Format("{0:n}", Convert.ToDecimal((snka5 == "" ? "0" : snka5))); nka6.Text = String.Format("{0:n}", Convert.ToDecimal((snka6 == "" ? "0" : snka6)));
                t1.Text = String.Format("{0:n}", Convert.ToDecimal(st1)); t2.Text = String.Format("{0:n}", Convert.ToDecimal(st2)); t3.Text = String.Format("{0:n}", Convert.ToDecimal(st3)); t4.Text = String.Format("{0:n}", Convert.ToDecimal(st4)); t5.Text = String.Format("{0:n}", Convert.ToDecimal(st5)); t6.Text = String.Format("{0:n}", Convert.ToDecimal(st6)); 
            }
            else if (numRows == 7)
            {
                a8.Visible = false; a9.Visible = false;
                cka8.Visible = false; cka9.Visible = false;
                cnka8.Visible = false; cnka9.Visible = false;
                t8.Visible = false; t9.Visible = false;
                a1.Text = arrColumn[0];
                a2.Text = arrColumn[1];
                a3.Text = arrColumn[2];
                a4.Text = arrColumn[3];
                a5.Text = arrColumn[4];
                a6.Text = arrColumn[5];
                a7.Text = arrColumn[6];
                ka1.Text = String.Format("{0:n}", Convert.ToDecimal((ska1 == "" ? "0" : ska1))); ka2.Text = String.Format("{0:n}", Convert.ToDecimal((ska2 == "" ? "0" : ska2))); ka3.Text = String.Format("{0:n}", Convert.ToDecimal((ska3 == "" ? "0" : ska3))); ka4.Text = String.Format("{0:n}", Convert.ToDecimal((ska4 == "" ? "0" : ska4))); ka5.Text = String.Format("{0:n}", Convert.ToDecimal((ska5 == "" ? "0" : ska5))); ka6.Text = String.Format("{0:n}", Convert.ToDecimal((ska6 == "" ? "0" : ska6))); ka7.Text = String.Format("{0:n}", Convert.ToDecimal((ska7 == "" ? "0" : ska7)));
                nka1.Text = String.Format("{0:n}", Convert.ToDecimal((snka1 == "" ? "0" : snka1))); nka2.Text = String.Format("{0:n}", Convert.ToDecimal((snka2 == "" ? "0" : snka2))); nka3.Text = String.Format("{0:n}", Convert.ToDecimal((snka3 == "" ? "0" : snka3))); nka4.Text = String.Format("{0:n}", Convert.ToDecimal((snka4 == "" ? "0" : snka4))); nka5.Text = String.Format("{0:n}", Convert.ToDecimal((snka5 == "" ? "0" : snka5))); nka6.Text = String.Format("{0:n}", Convert.ToDecimal((snka6 == "" ? "0" : snka6))); nka7.Text = String.Format("{0:n}", Convert.ToDecimal((snka7 == "" ? "0" : snka7)));
                t1.Text = String.Format("{0:n}", Convert.ToDecimal(st1)); t2.Text = String.Format("{0:n}", Convert.ToDecimal(st2)); t3.Text = String.Format("{0:n}", Convert.ToDecimal(st3)); t4.Text = String.Format("{0:n}", Convert.ToDecimal(st4)); t5.Text = String.Format("{0:n}", Convert.ToDecimal(st5)); t6.Text = String.Format("{0:n}", Convert.ToDecimal(st6)); t7.Text = String.Format("{0:n}", Convert.ToDecimal(st7)); 
            }
            tblGroupBudget.Visible = true;
            getBudgetMtdYtd(propCode);
            // End Get Colum For Group Budget
        }
        
    }

     [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
     public static string[] GetCompletionList(string prefixText, int count, string contextKey)
     {
         HttpCookie cook;
         cook = HttpContext.Current.Request.Cookies["sp"];
         cbll bll = new cbll();
         List<string> lItem = new List<string>();
         List<cArrayList> arr = new List<cArrayList>();
         string sItem = string.Empty;
         System.Data.SqlClient.SqlDataReader rs = null;
         arr.Add(new cArrayList("@salespointcd", cook.Value.ToString()));
         arr.Add(new cArrayList("@item_cd", prefixText));
         arr.Add(new cArrayList("@vendor_cd", contextKey.ToString()));
         bll.vSearchMstItemBySalespoint(arr, ref rs);
         while (rs.Read())
         {
             sItem = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["item_cd"].ToString() + "-" + rs["item_nm"].ToString() + "-" + rs["size"].ToString(), rs["item_cd"].ToString());
             lItem.Add(sItem);

         }
         rs.Close();
         return (lItem.ToArray());
     }
     protected void rdcust_SelectedIndexChanged(object sender, EventArgs e)
     {
         List<cArrayList> arr = new List<cArrayList>();
             if (hdprop.Value.ToString() == "")
             {
                 arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
                 bll.vDelProposalCusgrcd(arr);
                 bll.vDelProposalCustomer(arr);
                 bll.vDelProposalCustType(arr);
             }
             else
             {
                 arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
                 if (Session["edit"].ToString() == "true")
                 {
                     if (Session["cust"].ToString() != rdcust.SelectedValue.ToString())
                     {
                         bll.vDelProposalCusgrcd(arr);
                         bll.vDelProposalCustomer(arr);
                         bll.vDelProposalCustType(arr);
                     }                     
                 }
             }
         if (rdcust.SelectedValue.ToString() == "C")
         { 
             txsearchcust.CssClass = "divnormal"; cbcusgrcd.CssClass = "divhid"; grdcusgrcd.CssClass = "divhid"; 
             grdcust.CssClass = "divgrid"; grdcusttype.CssClass = "divhid";
         }
         else if (rdcust.SelectedValue.ToString() == "G")
         {
             txsearchcust.CssClass = "divhid";
             cbcusgrcd.CssClass = "divnormal";
             bll.vBindingFieldValueToCombo(ref cbcusgrcd, "cusgrcd");
             grdcust.CssClass = "divhid";
             grdcusgrcd.CssClass = "divgrid";
             grdcusttype.CssClass = "divhid";
         }
         else if (rdcust.SelectedValue.ToString() == "T")
         {
             txsearchcust.CssClass = "divhid";
             cbcusgrcd.CssClass = "divnormal";
             bll.vBindingFieldValueToCombo(ref cbcusgrcd, "otlcd");
             grdcust.CssClass = "divhid";
             grdcusgrcd.CssClass = "divhid";
             grdcusttype.CssClass = "divgrid";
         }
     }
     protected void cbgroup_SelectedIndexChanged(object sender, EventArgs e)
     {
         if (cbgroup.SelectedValue.ToString() == "C")
         { txsearchcust.CssClass = "divnormal"; cbgroup.CssClass = "divhid"; }
         else if (cbgroup.SelectedValue.ToString() == "G")
         { txsearchcust.CssClass = "divhid"; cbgroup.CssClass = "divnormal"; }
     }

     

     [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
     public static string[] GetCompletionList2(string prefixText, int count, string contextKey)
     {
         cbll bll = new cbll();
         System.Data.SqlClient.SqlDataReader rs = null;
         List<string> lCust = new List<string>();
         string sCust = string.Empty;
         List<cArrayList> arr = new List<cArrayList>();
         arr.Add(new cArrayList("@cust_cd", prefixText));
         if (HttpContext.Current.Session["hdprop"].ToString() == "") {
             arr.Add(new cArrayList("@prop_no", HttpContext.Current.Request.Cookies["usr_id"].Value.ToString()));
         }
         else
         {
             arr.Add(new cArrayList("@prop_no", HttpContext.Current.Session["hdprop"].ToString()));
         }
         bll.vSearchMstCustomerPro(arr, ref rs);
         while (rs.Read())
         {
             sCust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + "-" + rs["cust_nm"].ToString(), rs["cust_cd"].ToString());
             lCust.Add(sCust);
         }
         rs.Close();
         return (lCust.ToArray());
     }
     protected void btaddcust_Click(object sender, EventArgs e)
     {
         if (rdcust.SelectedValue.ToString() == "C")
         {
             if (hdcust.Value.ToString() == "")
             {
                 ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Customer Not yet selcted','select customer','warning');", true);
                 return;
             }
         }
         List<cArrayList> arr = new List<cArrayList>();
         if (rdcust.SelectedValue.ToString()=="C")
         {
             if (hdprop.Value.ToString() == "")
             {
                 arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
             }
             else
             {
                 arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
             }
             arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
             arr.Add(new cArrayList("@cust_nm", txsearchcust.Text.ToString()));
             bll.vInsertProposalCustomer(arr); arr.Clear();
             if (hdprop.Value.ToString() == "")
             {
                 arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
             }
             else
             {
                 arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
             }
             bll.vBindingGridToSp(ref grdcust, "sp_tproposal_customer_get", arr);
             grdcust.CssClass = "divgrid";
             grdcusgrcd.CssClass = "divhid";
             grdcusttype.CssClass = "divhid";
         }else if (rdcust.SelectedValue.ToString()=="G")
         {
             if (hdprop.Value.ToString() == "")
             {
                 arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
             }
             else
             {
                 arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
             }
             arr.Add(new cArrayList("@cusgrcd", cbcusgrcd.SelectedValue.ToString()));
             bll.vInsertProposalCusgrcd(arr); arr.Clear();
             if (hdprop.Value.ToString() == "")
             {
                 arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
             }
             else
             {
                 arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
             }
             bll.vBindingGridToSp(ref grdcusgrcd, "sp_tproposal_cusgrcd_get", arr);
             grdcust.CssClass = "divhid";
             grdcusgrcd.CssClass = "divgrid";
             grdcusttype.CssClass = "divhid";
         }
         else if (rdcust.SelectedValue.ToString() == "T")
         {
             if (hdprop.Value.ToString() == "")
             {
                 arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
             }
             else
             {
                 arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
             }
             arr.Add(new cArrayList("@cust_typ", cbcusgrcd.SelectedValue.ToString()));
             bll.vInsertProposalCustType(arr); arr.Clear();
             if (hdprop.Value.ToString() == "")
             {
                 arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
             }
             else
             {
                 arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
             }
             bll.vBindingGridToSp(ref grdcusttype, "sp_tproposal_custtype_get", arr);
             grdcust.CssClass = "divhid";
             grdcusgrcd.CssClass = "divhid";
             grdcusttype.CssClass = "divnormal";
         }
        
     }
     protected void cbpaymentterm_SelectedIndexChanged(object sender, EventArgs e)
     {
         List<cArrayList> arr = new List<cArrayList>();
             if (hdprop.Value.ToString() == "")
             {
                 arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
                 bll.vDelProposalPayment(arr);
                 for (int i = 1; i <= Convert.ToInt16(cbpaymentterm.SelectedValue); i++)
                 {
                     arr.Clear();
                     arr.Add(new cArrayList("@sequenceno", i));
                     arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
                     arr.Add(new cArrayList("@payment_dt", DateTime.ParseExact(DateTime.Now.ToString("d/M/yyyy"), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                     arr.Add(new cArrayList("@qty", 0));
                     bll.vInsertProposalPayment(arr);
                 }
             }
             else
             {
                 arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
                 if (Session["edit"] == "true")
                 {
                     bll.vDelProposalPayment(arr);
                     for (int i = 1; i <= Convert.ToInt16(cbpaymentterm.SelectedValue); i++)
                     {
                         arr.Clear();
                         arr.Add(new cArrayList("@sequenceno", i));
                         arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
                         arr.Add(new cArrayList("@payment_dt", DateTime.ParseExact(Request.Cookies["waz_dt"].Value, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                         arr.Add(new cArrayList("@qty", 0));
                         bll.vInsertProposalPayment(arr);
                     }
                 }
             }
         arr.Clear();
             if (hdprop.Value.ToString() == "")
             {
                 arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
             }
             else
             {
                 arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
             }
         bll.vBindingGridToSp(ref grdpayment, "sp_tproposal_payment_get", arr);
     }
     protected void btsave_Click(object sender, EventArgs e)
     {
         //if (txpropvendor.Text == "")
         //{
         //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Proposal Vendor can not empty','Proposal Vendor No','warning');", true);
         //    return;
         //}

         //string cekStatusProp = "";
         //cekStatusProp = bll.vLookUp("select  top 1 prop_no from tmst_proposal where prop_year >= '2018' and approval = 0 and prop_dt <= (getdate()-convert(int,(select parm_valu from tcontrol_parameter where parm_nm='proposalblocked'))) and vendor_cd='"+cbvendor.SelectedValue.ToString()+"'");

         //if (cekStatusProp == "")
         //{
             if (dtprop.Text == "")
             {
                 ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Proposal Vendor can not empty','Proposal Vendor No','warning');", true);
                 return;
             }
             double dBudget;
             if (!double.TryParse(txbudget.Text, out dBudget))
             {
                 ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Budget Limit can not empty or non numeric','Budget Limit','warning');", true);
                 return;
             }
             if (rditem.SelectedValue.ToString() == "I")
             {
                 if (grditem.Rows.Count == 0)
                 {
                     ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Item Not Yet Selected','Item','warning');", true);
                     return;
                 }
             }
             else if (rditem.SelectedValue.ToString() == "G")
             {
                 if (grdgroup.Rows.Count == 0)
                 {
                     ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Product group not Yet Selected','Product Group','warning');", true);
                     return;
                 }
             }
             if (rdcust.SelectedValue.ToString() == "C")
             {
                 if (grdcust.Rows.Count == 0)
                 {
                     ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Customer Not Yet Selected','Customer','warning');", true);
                     return;
                 }
             }
             else if (rdcust.SelectedValue.ToString() == "G")
             {
                 if (grdcusgrcd.Rows.Count == 0)
                 {
                     ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Group customer not yet Selected','Customer Group','warning');", true);
                     return;
                 }
             }
             else if (rdcust.SelectedValue.ToString() == "T")
             {
                 if (grdcusttype.Rows.Count == 0)
                 {
                     ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Customer Type not yet Selected','Customer Type','warning');", true);
                     return;
                 }
             }

             if (cbmarketingcost.SelectedValue.ToString().ToLower() == "principal")
             {
                 txprincipalcost.Text = "100";
                 txcostsbtc.Text = "0";
             }
             else if (cbmarketingcost.SelectedValue.ToString().ToLower() == "sbtc")
             {
                 txcostsbtc.Text = "100";
                 txprincipalcost.Text = "0";
             }
             else if (cbmarketingcost.SelectedValue.ToString().ToLower() == "percentage")
             {
                 double dSbtc; double dPrincipal; double total;
                 if (!double.TryParse(txcostsbtc.Text, out dSbtc))
                 {
                     ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Percentage SBTC must numeric','Percentage','warning');", true);
                     return;
                 }
                 if (!double.TryParse(txprincipalcost.Text, out dPrincipal))
                 {
                     ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Percentage Principal must numeric','Percentage','warning');", true);
                     return;
                 }
                 total = dSbtc + dPrincipal;
                 if (total != 100)
                 {
                     ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Total Percentage must be 100%','Percentage','warning');", true);
                     return;
                 }
             }
             else if (cbmarketingcost.SelectedValue.ToString().ToLower() == "branch")
             {
                 txcostsbtc.Text = "0";
                 txprincipalcost.Text = "0";
             }
             //double dQty = 0;
             //foreach (GridViewRow row in grdpayment.Rows)
             //{
             //    if (row.RowType == DataControlRowType.DataRow)
             //    {
             //        TextBox txqty = (TextBox)row.FindControl("txqty");
             //        if (!double.TryParse(txqty.Text, out dQty))
             //        {
             //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Quantity payment must numeric','Quantity Paid','warning');", true);
             //            return;
             //        }
             //        if (dQty == 0)
             //        {
             //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Quantity payment can not zero','Quantity Paid','warning');", true);
             //            return;
             //        }
             //    }
             //}

             if (cbpaymenttype.SelectedValue.ToString() == "")
             {
                 ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Payment Type Must Be Selected','Payment Type','warning');", true);
                 return;
             }

             //if (upl.FileName == "")
             //{
             //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('No Image uploaded','Please scan the document and upload','warning');", true);
             //    return;
             //}  

             string sPropNo = string.Empty;
             string propCode = string.Empty;
             int propDigit = 0;
             int propYear = 0;
             System.Data.SqlClient.SqlDataReader rs = null;
             List<cArrayList> arr = new List<cArrayList>();
             if (txpropno.Text.ToString() != "")
             {
                 string[] code = txpropno.Text.ToString().Split('/');
                 if (code.Length == 5)
                 {
                     sPropNo = code[0] + "/" + cbpromotype.SelectedValue.ToString() + "/" + code[2] + "/" + code[3] + "/" + code[4];
                 }
                 else
                 {
                     sPropNo = txpropno.Text.ToString();
                 }

                 propCode = code[2];
                 propDigit = Convert.ToInt16(code[0].ToString());
                 propYear = Convert.ToInt16(code[4].ToString());
             }
             else
             {
                 arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
                 arr.Add(new cArrayList("@rditem", rditem.SelectedValue.ToString()));
                 arr.Add(new cArrayList("@promo", cbpromotype.SelectedValue.ToString()));
                 arr.Add(new cArrayList("@year", DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year));
                 arr.Add(new cArrayList("@month", DateTime.ParseExact(dtdelivery.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                 bll.vGetProposalNo(arr, ref rs);
                 while (rs.Read())
                 {
                     propCode = rs["group_code"].ToString();
                     sPropNo = rs["proposal_code"].ToString();
                     propDigit = Convert.ToInt16(rs["prop_digit"].ToString());
                     propYear = Convert.ToInt16(rs["year"].ToString());
                 }
             }

             arr.Clear();
             arr.Add(new cArrayList("@prop_dt", DateTime.ParseExact(dtprop.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
             arr.Add(new cArrayList("@budget", txbudget.Text.Replace(",", "").ToString()));
             arr.Add(new cArrayList("@prop_no_vendor", txpropvendor.Text));
             arr.Add(new cArrayList("@start_dt", DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
             arr.Add(new cArrayList("@end_dt", DateTime.ParseExact(dtend.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
             arr.Add(new cArrayList("@delivery_dt", DateTime.ParseExact(dtdelivery.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
             arr.Add(new cArrayList("@claim_dt", DateTime.ParseExact(dtclaim.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
             arr.Add(new cArrayList("@rditem", rditem.SelectedValue.ToString()));
             arr.Add(new cArrayList("@rdcust", rdcust.SelectedValue.ToString()));
             arr.Add(new cArrayList("@vendor_cd", cbvendor.SelectedValue.ToString()));
             arr.Add(new cArrayList("@prop_term", cbpaymentterm.SelectedValue.ToString()));
             arr.Add(new cArrayList("@agreement_no", txagreementno.Text));
             arr.Add(new cArrayList("@agreement_dt", DateTime.ParseExact(dtagree.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
             arr.Add(new cArrayList("@prop_payment", cbpaymentterm.SelectedValue.ToString()));
             //arr.Add(new cArrayList("@uom", cbuom.SelectedValue.ToString()));

             arr.Add(new cArrayList("@promogrp", cbpromogroup.SelectedValue.ToString()));
             arr.Add(new cArrayList("@promotyp", cbpromotype.SelectedValue.ToString()));

             //Dihilangkan @25-05-2016 By Nico
             //arr.Add(new cArrayList("@sbtc_app", cbsbtcapp.SelectedValue.ToString()));

             arr.Add(new cArrayList("@vendor_app", cbappvendor.SelectedValue.ToString()));
             arr.Add(new cArrayList("@rdcost", cbmarketingcost.SelectedValue.ToString()));
             arr.Add(new cArrayList("@sbtccost", txcostsbtc.Text));
             arr.Add(new cArrayList("@principalcost", txprincipalcost.Text));
             arr.Add(new cArrayList("@ref_no", txrefno.Text));
             arr.Add(new cArrayList("@remark", txremark.Text));
             //arr.Add(new cArrayList("@rdbudget",cbbudgettype.SelectedValue.ToString()));
             arr.Add(new cArrayList("@rdpayment", cbpaymenttype.SelectedValue.ToString()));
        	arr.Add(new cArrayList("@prop_no", sPropNo));
             arr.Add(new cArrayList("@prop_code", propCode));
             arr.Add(new cArrayList("@prop_digit", propDigit));
             arr.Add(new cArrayList("@prop_year", propYear));

             arr.Add(new cArrayList("@bgremark", txbgremark.Text.ToString()));
             arr.Add(new cArrayList("@budgetadd", txaddbudget.Text.Replace(",", "").ToString()));
             arr.Add(new cArrayList("@mtd", txbudgetmtd.Text.Replace(",", "").ToString()));
             arr.Add(new cArrayList("@ytd", txbudgetytd.Text.Replace(",", "").ToString()));
             arr.Add(new cArrayList("@claim", chclaim.Checked.ToString()));
             arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));

             arr.Add(new cArrayList("@sign_frame", cbsignframe.SelectedValue.ToString()));

             arr.Add(new cArrayList("@budget_calc", chbudget.Checked.ToString()));
             // Freegood Calculation
             arr.Add(new cArrayList("@budget_free", chfreegood.Checked.ToString()));
             arr.Add(new cArrayList("@rditemfree", rdfreeitem.SelectedValue.ToString()));
             // Automatic Scheme
             arr.Add(new cArrayList("@bscheme", chdiscount.Checked.ToString()));
             // Price Budget Calculation
             arr.Add(new cArrayList("@price_calc", cbprice.SelectedValue.ToString()));
             arr.Add(new cArrayList("@pricecalc_typ", cbpricetype.SelectedValue.ToString()));

             if (cbpromogroup.SelectedValue.ToString() == "RB")
             {
                 arr.Add(new cArrayList("@budgetlimit", txfreegood.Text));
             }
             else
             {
                 //if (cbpromogroup.SelectedValue.ToString() == "TB")
                 //{
                 //    if (rditem.SelectedValue.ToString() == "I")
                 //    {
                 //        Label lbdbp = (Label)grdcostitemTB.Rows[0].FindControl("dbp");
                 //        arr.Add(new cArrayList("@budgetlimit", lbdbp.Text));
                 //    }
                 //    else
                 //    {
                 //        Label lbdbp = (Label)grdcostprodTB.Rows[0].FindControl("dbp");
                 //        arr.Add(new cArrayList("@budgetlimit", lbdbp.Text));
                 //    }
                 //}
                 //else
                 //{
                 //if ((cbbgitem.SelectedValue.ToString() == "C") || (cbbgitem.SelectedValue.ToString() == "E") || (cbbgitem.SelectedValue.ToString() == "B") )
                 //{
                 arr.Add(new cArrayList("@budgetlimit", txtotalbudget.Text.Replace(",", "").ToString()));
                 //}
                 //else
                 //{
                 //    if (cbbgitem.SelectedValue.ToString() == "A")
                 //    {
                 //        arr.Add(new cArrayList("@budgetlimit", txtotalbudget.Text));
                 //    }
                 //    else
                 //    {
                 //        if (cbpaymenttype.SelectedValue.ToString() == "FG")
                 //        {
                 //            Label lbdbp = (Label)grdcost.Rows[0].FindControl("dbp");
                 //            arr.Add(new cArrayList("@budgetlimit", lbdbp.Text));
                 //        }
                 //        else
                 //        {
                 //            Label lbrbp = (Label)grdcost.Rows[0].FindControl("rbp");
                 //            arr.Add(new cArrayList("@budgetlimit", lbrbp.Text));
                 //        }
                 //    }
                 //}
                 //}
             }

             //Add Request Promotion from branch
             arr.Add(new cArrayList("@reqdisc_cd", hdpromo.Value.ToString()));

             //string sPropNo = string.Empty;
             bll.vInsertMstProposal(arr);
             // Add Proposal Sign @130162016 By Nico
             arr.Clear();
             arr.Add(new cArrayList("@prop_no", sPropNo));
             //if (cbapcoordinator.SelectedValue.ToString() != "NULL"){
             arr.Add(new cArrayList("@sbtc_apc", cbapcoordinator.SelectedValue.ToString()));
             //}
             //if (cbprodman.SelectedValue.ToString() != "NULL")
             //{
             arr.Add(new cArrayList("@sbtc_prodmgr", cbprodman.SelectedValue.ToString()));
             //}
             //if (cbgm.SelectedValue.ToString() != "NULL")
             //{
             arr.Add(new cArrayList("@sbtc_gm", cbgm.SelectedValue.ToString()));
             //}
             //if (cbclaimdepthead.SelectedValue.ToString() != "NULL")
             //{
             arr.Add(new cArrayList("@sbtc_cap", cbclaimdepthead.SelectedValue.ToString()));
             //}
             //if (cbkamgr.SelectedValue.ToString() != "NULL")
             //{
             arr.Add(new cArrayList("@sbtc_kamgr", cbkamgr.SelectedValue.ToString()));
             arr.Add(new cArrayList("@sbtc_marketmgr", cbmarketmgr.SelectedValue.ToString()));
             //}
             //if (cbmarketmgrpcp.SelectedValue.ToString() != "NULL")
             //{
             arr.Add(new cArrayList("@vendor_marketmgr", cbmarketmgrpcp.SelectedValue.ToString()));
             //}else{
             //    arr.Add(new cArrayList("@vendor_marketmgr", null));
             //}
             //if (cbnspmpcp.SelectedValue.ToString() != "NULL")
             //{
             arr.Add(new cArrayList("@vendor_nspm", cbnspmpcp.SelectedValue.ToString()));
             //}else{
             //    arr.Add(new cArrayList("@vendor_nspm", null));
             //}
             //if (cbfinpcp.SelectedValue.ToString() != "NULL")
             //{
             arr.Add(new cArrayList("@vendor_findep", cbfinpcp.SelectedValue.ToString()));
             //}else{
             //    arr.Add(new cArrayList("@vendor_findep", null));
             //}
             //if (cbmarketingdirpcp.SelectedValue.ToString() != "NULL")
             //{
             arr.Add(new cArrayList("@vendor_marketdir", cbmarketingdirpcp.SelectedValue.ToString()));
             //}else{
             //    arr.Add(new cArrayList("@vendor_marketdir", null));
             //}         
             arr.Add(new cArrayList("@vendor_gmdir", cbgmpcp.SelectedValue.ToString()));
             bll.vInsertProposalSign(arr);
             // Proposal Sign
             arr.Clear();
             arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
             arr.Add(new cArrayList("@proposal_code", sPropNo));
             bll.vUpdatetMstProposalNo(arr);
             // Proposal Group Budget
             arr.Clear();
             arr.Add(new cArrayList("@prop_no", sPropNo));
             arr.Add(new cArrayList("@ka1", ka1.Text.Replace(",", "").ToString()));
             arr.Add(new cArrayList("@ka2", ka2.Text.Replace(",", "").ToString()));
             arr.Add(new cArrayList("@ka3", ka3.Text.Replace(",", "").ToString()));
             arr.Add(new cArrayList("@ka4", ka4.Text.Replace(",", "").ToString()));
             arr.Add(new cArrayList("@ka5", ka5.Text.Replace(",", "").ToString()));
             arr.Add(new cArrayList("@ka6", ka6.Text.Replace(",", "").ToString()));
             arr.Add(new cArrayList("@ka7", ka7.Text.Replace(",", "").ToString()));
             arr.Add(new cArrayList("@ka8", ka8.Text.Replace(",", "").ToString()));
             arr.Add(new cArrayList("@ka9", ka9.Text.Replace(",", "").ToString()));
             arr.Add(new cArrayList("@nka1", nka1.Text.Replace(",", "").ToString()));
             arr.Add(new cArrayList("@nka2", nka2.Text.Replace(",", "").ToString()));
             arr.Add(new cArrayList("@nka3", nka3.Text.Replace(",", "").ToString()));
             arr.Add(new cArrayList("@nka4", nka4.Text.Replace(",", "").ToString()));
             arr.Add(new cArrayList("@nka5", nka5.Text.Replace(",", "").ToString()));
             arr.Add(new cArrayList("@nka6", nka6.Text.Replace(",", "").ToString()));
             arr.Add(new cArrayList("@nka7", nka7.Text.Replace(",", "").ToString()));
             arr.Add(new cArrayList("@nka8", nka8.Text.Replace(",", "").ToString()));
             arr.Add(new cArrayList("@nka9", nka9.Text.Replace(",", "").ToString()));
             arr.Add(new cArrayList("@t1", ka1.Text.Replace(",", "").ToString()));
             arr.Add(new cArrayList("@t2", ka2.Text.Replace(",", "").ToString()));
             arr.Add(new cArrayList("@t3", ka3.Text.Replace(",", "").ToString()));
             arr.Add(new cArrayList("@t4", ka4.Text.Replace(",", "").ToString()));
             arr.Add(new cArrayList("@t5", ka5.Text.Replace(",", "").ToString()));
             arr.Add(new cArrayList("@t6", ka6.Text.Replace(",", "").ToString()));
             arr.Add(new cArrayList("@t7", ka7.Text.Replace(",", "").ToString()));
             arr.Add(new cArrayList("@t8", ka8.Text.Replace(",", "").ToString()));
             arr.Add(new cArrayList("@t9", ka9.Text.Replace(",", "").ToString()));
             bll.vInsertBudgetProduct(arr);
             arr.Clear();
             // Insert Adjustment Date
             arr.Add(new cArrayList("@prop_no", sPropNo));
             if (dtadjuststart.Text.ToString() != "") { arr.Add(new cArrayList("@dtfrom", DateTime.ParseExact(dtadjuststart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))); }
             if (dtadjustend.Text.ToString() != "") { arr.Add(new cArrayList("@dtto", DateTime.ParseExact(dtadjustend.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))); }
             bll.vInsertAdjustmentProposal(arr);
             arr.Clear();
             // Adjustment Date
             arr.Add(new cArrayList("@sbtc_apc", cbapcoordinator.SelectedValue.ToString()));
             txpropno.Text = sPropNo;
             txpropno.CssClass = "divnormal";
             txbudget.CssClass = "ro";
             txagreementno.CssClass = "ro";
             txpropvendor.CssClass = "divnormal";
             txsearchcust.CssClass = "ro";
             txsearchitem.CssClass = "ro";
             cbpaymentterm.CssClass = "ro";
             cbgroup.CssClass = "ro";
             cbcusgrcd.CssClass = "ro";
             cbpaymentterm.CssClass = "ro";
             //cbuom.CssClass = "ro";
             cbvendor.CssClass = "ro";
             btadd.CssClass = "divhid";
             btaddcust.CssClass = "divhid";
             ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('New proposal has been created','" + sPropNo + "','success');", true);
             btprint.Visible = true;
             btprint2.Visible = true;
             btsave.Visible = false;
             if ((Request.Cookies["usr_id"].Value.ToString() == "3064") || (Request.Cookies["usr_id"].Value.ToString() == "1287"))
             {
                 btapprove.Visible = false;
                 btcancel.Visible = false;
                 btedit.Visible = false;
                 btdelete.Visible = false;
             }
             else
             {
                 btapprove.Visible = false;
                 btcancel.Visible = false;
                 btedit.Visible = false;
                 btdelete.Visible = false;
             }

             if ((Request.Cookies["usr_id"].Value.ToString() == "3064") || (Request.Cookies["usr_id"].Value.ToString() == "1287"))
             {
                 btapprove.Visible = true;
                 btcancel.Visible = true;
                 btedit.Visible = false;
                 btdelete.Visible = true;
             }

             btedit.Visible = true;
             //return;

             grdcate.Visible = true;
             btnuploaddoc.Visible = true;
             grddoc.Visible = true;

             // Document Proposal 
             arr.Clear();
             arr.Add(new cArrayList("@promo_cd", cbpromotype.SelectedValue.ToString()));
             arr.Add(new cArrayList("@dic", null));
             bll.vBindingGridToSp(ref grdcate, "sp_tpromotion_doc_get", arr);

             arr.Clear();
             arr.Add(new cArrayList("@prop_no", txpropno.Text));
             bll.vBindingGridToSp(ref grddoc, "sp_tproposal_document_get", arr);


            // Send Email to Ex.COM 
             if (txreqdisc.Text != "" && hdpromo.Value.ToString() != "")
             {
                 if (cbmarketingcost.SelectedValue.ToString() == "sbtc" || cbmarketingcost.SelectedValue.ToString() == "branch")
                 {
                     if (cbgm.SelectedValue.ToString() != "NULL")
                     {
                         string email = bll.vLookUp("select email from tproposal_signoff a inner join tuser_profile b on a.emp_cd=b.emp_cd where ids='"+cbgm.SelectedValue.ToString()+"' ");
                         if (email != "")
                         {
                             arr.Clear();
                             arr.Add(new cArrayList("@prop_no", txpropno.Text.ToString()));
                             arr.Add(new cArrayList("@email", email.ToString()));
                             //bll.vBatchEmailProposal(arr);
                         }
                     }
                 }
             }   


         //}
         //else
         //{
         //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('There\' Proposal not yet approve from 2018.','Please approve that proposal first.','warning');", true);
         //    return;
         //}

     }
     protected void btsearchprop_Click(object sender, EventArgs e)
     {
         Session["edit"] = ""; 
         ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "popupwindow('lookproposal.aspx');", true);
         btprint.Visible = true;
         btprint2.Visible = true;
         btedit.Visible = true;
         btsave.Visible = false;

         if ((Request.Cookies["usr_id"].Value.ToString() == "3064") || (Request.Cookies["usr_id"].Value.ToString() == "1287"))
         {
             btapprove.Visible = false;
             btcancel.Visible = false;
             btedit.Visible = true;
             btdelete.Visible = false;
         }
         else
         {
             btapprove.Visible = false;
             btcancel.Visible = false;
             btedit.Visible = false;
             btdelete.Visible = false;
         }

         if ((Request.Cookies["usr_id"].Value.ToString() == "3064") || (Request.Cookies["usr_id"].Value.ToString() == "1287"))
         {
             btapprove.Visible = true;
             btcancel.Visible = true;
             btedit.Visible = true;
             btdelete.Visible = true;
         }
     }
     protected void cbbudgettype_SelectedIndexChanged(object sender, EventArgs e)
     {
         //if (cbbudgettype.SelectedValue.ToString() == "1")
         //{ 
         //    lbsar.Text = "SAR"; cbuom.CssClass = "ro"; cbuom.SelectedValue = "";
         //}
         //else { 
         //    lbsar.Text = "QTY"; cbuom.CssClass = "divnormal";
         //}
     }
     protected void cbvendor_SelectedIndexChanged(object sender, EventArgs e)
     {
         //string cekStatusProp = "";
         //cekStatusProp = bll.vLookUp("select  top 1 prop_no from tmst_proposal where prop_year >= '2018' and approval = 0 and prop_dt <= (getdate()-convert(int,(select parm_valu from tcontrol_parameter where parm_nm='proposalblocked'))) and vendor_cd='"+cbvendor.SelectedValue.ToString()+"'");

         //if (cekStatusProp == "")
         //{
             txsearchitem_AutoCompleteExtender.ContextKey = cbvendor.SelectedValue.ToString();
             //txsearchfreeitem_AutoCompleteExtender.ContextKey = cbvendor.SelectedValue.ToString();
             List<cArrayList> arr = new List<cArrayList>();
             arr.Add(new cArrayList("@vendor_cd", cbvendor.SelectedValue.ToString()));
             bll.vBindingComboToSp(ref cbappvendor, "sp_tproposal_appvendor_get", "app_cd", "app_cd", arr);
             //bll.vBindingComboToSp(ref cbgmpcp, "sp_tproposal_appvendor_get", "app_cd", "app_cd", arr);
             arr.Clear();
             arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
             bll.vDelProposalItem(arr);
             arr.Add(new cArrayList("@bgitem", cbbgitem.SelectedValue.ToString()));
             arr.Add(new cArrayList("@cust", rdcust.SelectedValue.ToString()));
             bll.vBindingGridToSp(ref grditem, "sp_tproposal_item_get", arr);
             arr.Clear();
             arr.Add(new cArrayList("@vendor_cd", cbvendor.SelectedValue.ToString()));
             bll.vBindingComboToSp(ref cbgroup, "sp_tmst_product_getbyvendor", "prod_cd", "prod_nm", arr);
             //bll.vBindingComboToSp(ref cbfreegroup, "sp_tmst_product_getbyvendor", "prod_cd", "prod_nm", arr);
             // SignProposal by Principal @12062016 By Nico
             arr.Clear();
             arr.Add(new cArrayList("@position", "MARKETMGR"));
             arr.Add(new cArrayList("@vendor_cd", cbvendor.SelectedValue.ToString()));
             bll.vBindingComboToSp(ref cbmarketmgrpcp, "sp_tproposal_signbyvendor_get", "idv", "fullname", arr);
             arr.Clear();
             arr.Add(new cArrayList("@position", "NSPM"));
             arr.Add(new cArrayList("@vendor_cd", cbvendor.SelectedValue.ToString()));
             bll.vBindingComboToSp(ref cbnspmpcp, "sp_tproposal_signbyvendor_get", "idv", "fullname", arr);
             arr.Clear();
             arr.Add(new cArrayList("@position", "GMDIR"));
             arr.Add(new cArrayList("@vendor_cd", cbvendor.SelectedValue.ToString()));
             bll.vBindingComboToSp(ref cbgmpcp, "sp_tproposal_signbyvendor_get", "idv", "fullname", arr);
             arr.Clear();
             arr.Add(new cArrayList("@position", "FINDEP"));
             arr.Add(new cArrayList("@vendor_cd", cbvendor.SelectedValue.ToString()));
             bll.vBindingComboToSp(ref cbfinpcp, "sp_tproposal_signbyvendor_get", "idv", "fullname", arr);
             arr.Clear();
             arr.Add(new cArrayList("@position", "MARKETDIR"));
             arr.Add(new cArrayList("@vendor_cd", cbvendor.SelectedValue.ToString()));
             bll.vBindingComboToSp(ref cbmarketingdirpcp, "sp_tproposal_signbyvendor_get", "idv", "fullname", arr);
             // SignProposal by Principal
             // Proposal Sign Add Null Value 
             cbmarketmgrpcp.Items.Insert(0, "NULL");
             cbnspmpcp.Items.Insert(0, "NULL");
             cbgmpcp.Items.Insert(0, "NULL");
             cbfinpcp.Items.Insert(0, "NULL");
             cbmarketingdirpcp.Items.Insert(0, "NULL");
             // End Proposal Sign Add Null Value
             //btsave.Visible = true;
         //}
         //else
         //{
         //    //btsave.Visible = false;
         //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('New proposal has been created','','success');", true);
         //    return;
         //}
     }
     protected void btnew_Click(object sender, EventArgs e)
     {
         Response.Redirect("fm_proposal.aspx");
     }
     protected void cbmarketingcost_SelectedIndexChanged(object sender, EventArgs e)
     {
         if (cbmarketingcost.SelectedValue.ToString().ToUpper() == "PERCENTAGE")
         { txcostsbtc.CssClass = "divnormal"; txprincipalcost.CssClass = "divnormal"; }
         else
         { txcostsbtc.CssClass = "ro"; txprincipalcost.CssClass = "ro"; txcostsbtc.Text = ""; txprincipalcost.Text = ""; }
     }
     protected void cbpromogroup_SelectedIndexChanged(object sender, EventArgs e)
     {
         lbsar.Text = "CTN";
         lblfreeproduct.Text = "QTY";
         List<cArrayList> arr = new List<cArrayList>();
             if (hdprop.Value.ToString() == "")
             {
                 arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
                 bll.vDelProposalItem(arr);
                 bll.vDelProposalProduct(arr);
                 bll.vDelProposalProductGroup(arr);
             }
             else
             {
                 arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
                 if (Session["edit"].ToString() == "true")
                 {
                     bll.vDelProposalItem(arr);
                     bll.vDelProposalProduct(arr);
                     bll.vDelProposalProductGroup(arr);
                 }
             }

         txbgitemtarget.Text = "";
         txbgitempercent.Text = "";
         txbudget.Text = "0";
         txfreegood.Text = "0";
         cbpaymenttype.SelectedValue = "";
         cbpaymenttype.CssClass = "divnormal";
         txfreegood.CssClass = "divnormal";
         grditem.CssClass = "divhid";
         grdgroup.CssClass = "divhid";
         //grditem.Visible = false;
         //grdgroup.Visible = false;
         arr.Clear();
         arr.Add(new cArrayList("@promo_cd", cbpromogroup.SelectedValue.ToString()));
         bll.vBindingComboToSp(ref cbpromotype, "sp_tpromotion_dtl_get","promo_typ","promotyp_nm",arr);
         cbpromotype_SelectedIndexChanged(sender, e);
         if (cbpromogroup.SelectedValue.ToString() == "RB")
         {
             cbbgitem.SelectedValue = "B";
             cbbgitem_SelectedIndexChanged(sender, e);
             txbudget.CssClass = "divnormal";
             txbudget.Text = "0";
             //cbbgitemtarget.CssClass = "ro";
             cbbgitemtarget.SelectedValue = "S";
             cbpaymenttype.SelectedValue = "CH";
             cbpaymenttype.CssClass = "divnormal";
             lbsar.Text = "SAR";
             lblfreeproduct.Text = "SAR";
             txrbtcondition.CssClass = "divnormal"; txtrgtsales.CssClass = "divnormal"; txtrgtcost.CssClass = "divnormal";
             // display_rent or signboard
             txsizerent.CssClass = "ro"; dtrentfrm.CssClass = "ro"; dtrentto.CssClass = "ro";
             txsizerent.Text = ""; dtrentfrm.Text = ""; dtrentto.Text = "";
             // listing_fee
             txcostfeesr.CssClass = "ro"; txcostfeeqty.CssClass = "ro";
             txcostfeesr.Text = ""; txcostfeeqty.Text = "";
             // other
             dtotherpromofrm.CssClass = "ro"; dtotherpromoto.CssClass = "ro";
             dtotherpromoto.Text = ""; dtotherpromofrm.Text = "";
             tblRebate.Visible = true;
             tblOther.Visible = false;
             tblRent.Visible = false;
             tblFee.Visible = false;
             tblCar.Visible = false;
             tblCook.Visible = false;
             tblSignBoard.Visible = false;
             tblChep.Visible = false;
             arr.Clear();
                 if (hdprop.Value.ToString() == "")
                 {
                     arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
                     bll.vDelProposalMechRebate(arr);
                     bll.vDelProposalMechOtherPromo(arr);
                     bll.vDelProposalMechDisplayRent(arr);
                     bll.vDelProposalMechFee(arr);
                     bll.vDelProposalMechCar(arr);
                     bll.vDelProposalMechCook(arr);
                     bll.vDelProposalMechSignboard(arr);
                     bll.vDelProposalMechChep(arr);
                 }
                 else
                 {
                     arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
                     if (Session["edit"].ToString() == "true")
                     {
                         bll.vDelProposalMechRebate(arr);
                         bll.vDelProposalMechOtherPromo(arr);
                         bll.vDelProposalMechDisplayRent(arr);
                         bll.vDelProposalMechFee(arr);
                         bll.vDelProposalMechCar(arr);
                         bll.vDelProposalMechCook(arr);
                         bll.vDelProposalMechSignboard(arr);
                         bll.vDelProposalMechChep(arr);
                     }
                 }

         }
         else if (cbpromogroup.SelectedValue.ToString() == "DR"){
             cbbgitem.SelectedValue = "C";
             txbudget.CssClass = "divnormal";
             txbudget.Text = "0";
             lbsar.Text = "SAR";
             cbbgitem_SelectedIndexChanged(sender, e);
             txsizerent.CssClass = "divnormal"; dtrentfrm.CssClass = "divnormal"; dtrentto.CssClass = "divnormal";
             dtrentfrm.Text = dtstart.Text.ToString();
             dtrentto.Text = dtend.Text.ToString();
             //rebate
             txrbtcondition.CssClass = "ro"; txtrgtsales.CssClass = "ro"; txtrgtcost.CssClass = "ro";
             txrbtcondition.Text = ""; txtrgtsales.Text = ""; txtrgtcost.Text = "";
             // listing_fee
             txcostfeesr.CssClass = "ro"; txcostfeeqty.CssClass = "ro";
             txcostfeesr.Text = ""; txcostfeeqty.Text = "";
             // other
             dtotherpromofrm.CssClass = "ro"; dtotherpromoto.CssClass = "ro";
             dtotherpromoto.Text = ""; dtotherpromofrm.Text = "";
             tblRebate.Visible = false;
             tblOther.Visible = false;
             tblRent.Visible = true;
             tblFee.Visible = false;
             tblCar.Visible = false;
             tblCook.Visible = false;
             tblSignBoard.Visible = false;
             tblChep.Visible = false;
             arr.Clear();
             if (hdprop.Value.ToString() == "")
             {
                 arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
                 bll.vDelProposalMechRebate(arr);
                 bll.vDelProposalMechOtherPromo(arr);
                 bll.vDelProposalMechDisplayRent(arr);
                 bll.vDelProposalMechFee(arr);
                 bll.vDelProposalMechCar(arr);
                 bll.vDelProposalMechCook(arr);
                 bll.vDelProposalMechSignboard(arr);
                 bll.vDelProposalMechChep(arr);
             }
             else
             {
                 arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
                 if (Session["edit"].ToString() == "true")
                 {
                     bll.vDelProposalMechRebate(arr);
                     bll.vDelProposalMechOtherPromo(arr);
                     bll.vDelProposalMechDisplayRent(arr);
                     bll.vDelProposalMechFee(arr);
                     bll.vDelProposalMechCar(arr);
                     bll.vDelProposalMechCook(arr);
                     bll.vDelProposalMechSignboard(arr);
                     bll.vDelProposalMechChep(arr);
                 }
             }

         }
         else if ((cbpromogroup.SelectedValue.ToString() == "OF") || (cbpromogroup.SelectedValue.ToString() == "PB") || (cbpromogroup.SelectedValue.ToString() == "LF"))
         {
             cbbgitem.SelectedValue = "C";
             txbudget.CssClass = "divnormal";
             txbudget.Text = "0";
             lbsar.Text = "SAR";
             cbbgitem_SelectedIndexChanged(sender, e);
             txcostfeesr.CssClass = "divnormal"; txcostfeeqty.CssClass = "divnormal";
             txcostfeesr.Text = "0"; txcostfeeqty.Text = "0";
             // rebate
             txrbtcondition.CssClass = "ro"; txtrgtsales.CssClass = "ro"; txtrgtcost.CssClass = "ro";
             txrbtcondition.Text = ""; txtrgtsales.Text = ""; txtrgtcost.Text = "";
             // display_rent or signboard
             txsizerent.CssClass = "ro"; dtrentfrm.CssClass = "ro"; dtrentto.CssClass = "ro";
             txsizerent.Text = ""; dtrentfrm.Text = ""; dtrentto.Text = "";
             // other
             dtotherpromofrm.CssClass = "ro"; dtotherpromoto.CssClass = "ro";
             dtotherpromoto.Text = ""; dtotherpromofrm.Text = "";
             tblRebate.Visible = false;
             tblOther.Visible = false;
             tblRent.Visible = false;
             tblFee.Visible = true;
             tblCar.Visible = false;
             tblCook.Visible = false;
             tblSignBoard.Visible = false;
             tblChep.Visible = false;
             arr.Clear();
             if (hdprop.Value.ToString() == "")
             {
                 arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
                 bll.vDelProposalMechRebate(arr);
                 bll.vDelProposalMechOtherPromo(arr);
                 bll.vDelProposalMechDisplayRent(arr);
                 bll.vDelProposalMechFee(arr);
                 bll.vDelProposalMechCar(arr);
                 bll.vDelProposalMechCook(arr);
                 bll.vDelProposalMechSignboard(arr);
                 bll.vDelProposalMechChep(arr);
             }
             else
             {
                 arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
                 if (Session["edit"].ToString() == "true")
                 {
                     bll.vDelProposalMechRebate(arr);
                     bll.vDelProposalMechOtherPromo(arr);
                     bll.vDelProposalMechDisplayRent(arr);
                     bll.vDelProposalMechFee(arr);
                     bll.vDelProposalMechCar(arr);
                     bll.vDelProposalMechCook(arr);
                     bll.vDelProposalMechSignboard(arr);
                     bll.vDelProposalMechChep(arr);
                 }
             }
             
         }
         else if ((cbpromogroup.SelectedValue.ToString() == "GS") || (cbpromogroup.SelectedValue.ToString() == "IP") || (cbpromogroup.SelectedValue.ToString() == "LT") || (cbpromogroup.SelectedValue.ToString() == "NE") || (cbpromogroup.SelectedValue.ToString() == "MF") || (cbpromogroup.SelectedValue.ToString() == "SD") || (cbpromogroup.SelectedValue.ToString() == "SI"))
         {
             cbbgitem.SelectedValue = "A";
             cbbgitem.CssClass = "divnormal";
             txbudget.CssClass = "divnormal";
             cbbgitem_SelectedIndexChanged(sender, e);
             dtotherpromofrm.CssClass = "divnormal"; dtotherpromoto.CssClass = "divnormal";
             dtotherpromofrm.Text = dtstart.Text.ToString();
             dtotherpromoto.Text = dtend.Text.ToString();
             // rebate
             txrbtcondition.CssClass = "ro"; txtrgtsales.CssClass = "ro"; txtrgtcost.CssClass = "ro";
             txrbtcondition.Text = ""; txtrgtsales.Text = ""; txtrgtcost.Text = "";
             // display_rent or signboard
             txsizerent.CssClass = "ro"; dtrentfrm.CssClass = "ro"; dtrentto.CssClass = "ro";
             txsizerent.Text = ""; dtrentfrm.Text = ""; dtrentto.Text = "";
             // listing_fee
             txcostfeesr.CssClass = "ro"; txcostfeeqty.CssClass = "ro";
             txcostfeesr.Text = ""; txcostfeeqty.Text = "";
             tblRebate.Visible = false;
             tblOther.Visible = true;
             tblRent.Visible = false;
             tblFee.Visible = false;
             tblCar.Visible = false;
             tblCook.Visible = false;
             tblSignBoard.Visible = false;
             tblChep.Visible = false;
             arr.Clear();
             if (hdprop.Value.ToString() == "")
             {
                 arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
                 bll.vDelProposalMechRebate(arr);
                 bll.vDelProposalMechOtherPromo(arr);
                 bll.vDelProposalMechDisplayRent(arr);
                 bll.vDelProposalMechFee(arr);
                 bll.vDelProposalMechCar(arr);
                 bll.vDelProposalMechCook(arr);
                 bll.vDelProposalMechSignboard(arr);
                 bll.vDelProposalMechChep(arr);
             }
             else
             {
                 arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
                 if (Session["edit"].ToString() == "true")
                 {
                     bll.vDelProposalMechRebate(arr);
                     bll.vDelProposalMechOtherPromo(arr);
                     bll.vDelProposalMechDisplayRent(arr);
                     bll.vDelProposalMechFee(arr);
                     bll.vDelProposalMechCar(arr);
                     bll.vDelProposalMechCook(arr);
                     bll.vDelProposalMechSignboard(arr);
                     bll.vDelProposalMechChep(arr);
                 }
             }
             
         }
         else if ((cbpromogroup.SelectedValue.ToString() == "TB"))
         {
             cbbgitem.SelectedValue = "B";
             txbudget.CssClass = "divnormal";
             txbudget.Text = "0";
             cbbgitem_SelectedIndexChanged(sender, e);
             //cbbgitemtarget.CssClass = "ro";
             cbbgitemtarget.SelectedValue = "Q";
             cbpaymenttype.SelectedValue = "FG";
             cbpaymenttype.CssClass = "divnormal";
             lbsar.Text = "CTN";
             dtotherpromofrm.CssClass = "divnormal"; dtotherpromoto.CssClass = "divnormal";
             dtotherpromofrm.Text = dtstart.Text.ToString();
             dtotherpromoto.Text = dtend.Text.ToString();
             // rebate
             txrbtcondition.CssClass = "ro"; txtrgtsales.CssClass = "ro"; txtrgtcost.CssClass = "ro";
             txrbtcondition.Text = ""; txtrgtsales.Text = ""; txtrgtcost.Text = "";
             // display_rent or signboard
             txsizerent.CssClass = "ro"; dtrentfrm.CssClass = "ro"; dtrentto.CssClass = "ro";
             txsizerent.Text = ""; dtrentfrm.Text = ""; dtrentto.Text = "";
             // listing_fee
             txcostfeesr.CssClass = "ro"; txcostfeeqty.CssClass = "ro";
             txcostfeesr.Text = ""; txcostfeeqty.Text = "";
             tblRebate.Visible = false;
             tblOther.Visible = true;
             tblRent.Visible = false;
             tblFee.Visible = false;
             tblCar.Visible = false;
             tblCook.Visible = false;
             tblSignBoard.Visible = false;
             tblChep.Visible = false;
             arr.Clear();
             if (hdprop.Value.ToString() == "")
             {
                 arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
                 bll.vDelProposalMechRebate(arr);
                 bll.vDelProposalMechOtherPromo(arr);
                 bll.vDelProposalMechDisplayRent(arr);
                 bll.vDelProposalMechFee(arr);
                 bll.vDelProposalMechCar(arr);
                 bll.vDelProposalMechCook(arr);
                 bll.vDelProposalMechSignboard(arr);
                 bll.vDelProposalMechChep(arr);
             }
             else
             {
                 arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
                 if (Session["edit"].ToString() == "true")
                 {
                     bll.vDelProposalMechRebate(arr);
                     bll.vDelProposalMechOtherPromo(arr);
                     bll.vDelProposalMechDisplayRent(arr);
                     bll.vDelProposalMechFee(arr);
                     bll.vDelProposalMechCar(arr);
                     bll.vDelProposalMechCook(arr);
                     bll.vDelProposalMechSignboard(arr);
                     bll.vDelProposalMechChep(arr);
                 }
             }
             
         }
         else if (cbpromogroup.SelectedValue.ToString() == "CB")
         {
             cbbgitem.SelectedValue = "C";
             txbudget.CssClass = "divnormal";
             txbudget.Text = "0";
             lbsar.Text = "SAR";
             cbbgitem_SelectedIndexChanged(sender, e);
             tblRebate.Visible = false;
             tblOther.Visible = false;
             tblRent.Visible = false;
             tblFee.Visible = false;
             tblCar.Visible = true;
             tblCook.Visible = false;
             tblSignBoard.Visible = false;
             tblChep.Visible = false;
             arr.Clear();
             if (hdprop.Value.ToString() == "")
             {
                 arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
                 bll.vDelProposalMechRebate(arr);
                 bll.vDelProposalMechOtherPromo(arr);
                 bll.vDelProposalMechDisplayRent(arr);
                 bll.vDelProposalMechFee(arr);
                 bll.vDelProposalMechCar(arr);
                 bll.vDelProposalMechCook(arr);
                 bll.vDelProposalMechSignboard(arr);
                 bll.vDelProposalMechChep(arr);
             }
             else
             {
                 arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
                 if (Session["edit"].ToString() == "true")
                 {
                     bll.vDelProposalMechRebate(arr);
                     bll.vDelProposalMechOtherPromo(arr);
                     bll.vDelProposalMechDisplayRent(arr);
                     bll.vDelProposalMechFee(arr);
                     bll.vDelProposalMechCar(arr);
                     bll.vDelProposalMechCook(arr);
                     bll.vDelProposalMechSignboard(arr);
                     bll.vDelProposalMechChep(arr);
                 }
             }
             
         }
         else if ((cbpromogroup.SelectedValue.ToString() == "CD") || (cbpromogroup.SelectedValue.ToString() == "SP") || (cbpromogroup.SelectedValue.ToString() == "RP"))
         {
             cbbgitem.SelectedValue = "C";
             txbudget.CssClass = "divnormal";
             txbudget.Text = "0";
             lbsar.Text = "SAR";
             cbbgitem_SelectedIndexChanged(sender, e);
             dtcookfrm.Text = dtstart.Text.ToString();
             dtcookto.Text = dtstart.Text.ToString();
             tblRebate.Visible = false;
             tblOther.Visible = false;
             tblRent.Visible = false;
             tblFee.Visible = false;
             tblCar.Visible = false;
             tblCook.Visible = true;
             tblSignBoard.Visible = false;
             tblChep.Visible = false;
             arr.Clear();
             if (hdprop.Value.ToString() == "")
             {
                 arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
                 bll.vDelProposalMechRebate(arr);
                 bll.vDelProposalMechOtherPromo(arr);
                 bll.vDelProposalMechDisplayRent(arr);
                 bll.vDelProposalMechFee(arr);
                 bll.vDelProposalMechCar(arr);
                 bll.vDelProposalMechCook(arr);
                 bll.vDelProposalMechSignboard(arr);
                 bll.vDelProposalMechChep(arr);
             }
             else
             {
                 arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
                 if (Session["edit"].ToString() == "true")
                 {
                     bll.vDelProposalMechRebate(arr);
                     bll.vDelProposalMechOtherPromo(arr);
                     bll.vDelProposalMechDisplayRent(arr);
                     bll.vDelProposalMechFee(arr);
                     bll.vDelProposalMechCar(arr);
                     bll.vDelProposalMechCook(arr);
                     bll.vDelProposalMechSignboard(arr);
                     bll.vDelProposalMechChep(arr);
                 }
             }

         }
         else if (cbpromogroup.SelectedValue.ToString() == "SB")
         {
             cbbgitem.SelectedValue = "C";
             txbudget.CssClass = "divnormal";
             txbudget.Text = "0";
             lbsar.Text = "SAR";
             cbbgitem_SelectedIndexChanged(sender, e);
             dtsbfrm.Text = dtstart.Text.ToString();
             dtsbto.Text = dtend.Text.ToString();
             tblRebate.Visible = false;
             tblOther.Visible = false;
             tblRent.Visible = false;
             tblFee.Visible = false;
             tblCar.Visible = false;
             tblCook.Visible = false;
             tblSignBoard.Visible = true;
             tblChep.Visible = false;
             arr.Clear();
             if (hdprop.Value.ToString() == "")
             {
                 arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
                 bll.vDelProposalMechRebate(arr);
                 bll.vDelProposalMechOtherPromo(arr);
                 bll.vDelProposalMechDisplayRent(arr);
                 bll.vDelProposalMechFee(arr);
                 bll.vDelProposalMechCar(arr);
                 bll.vDelProposalMechCook(arr);
                 bll.vDelProposalMechSignboard(arr);
                 bll.vDelProposalMechChep(arr);
             }
             else
             {
                 arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
                 if (Session["edit"].ToString() == "true")
                 {
                     bll.vDelProposalMechRebate(arr);
                     bll.vDelProposalMechOtherPromo(arr);
                     bll.vDelProposalMechDisplayRent(arr);
                     bll.vDelProposalMechFee(arr);
                     bll.vDelProposalMechCar(arr);
                     bll.vDelProposalMechCook(arr);
                     bll.vDelProposalMechSignboard(arr);
                     bll.vDelProposalMechChep(arr);
                 }
             }
             
         }
         else if (cbpromogroup.SelectedValue.ToString() == "CP")
         {
             cbbgitem.SelectedValue = "C";
             txbudget.CssClass = "divnormal";
             txbudget.Text = "0";
             lbsar.Text = "SAR";
             cbbgitem_SelectedIndexChanged(sender, e);
             tblRebate.Visible = false;
             tblOther.Visible = false;
             tblRent.Visible = false;
             tblFee.Visible = false;
             tblCar.Visible = false;
             tblCook.Visible = false;
             tblSignBoard.Visible = false;
             tblChep.Visible = true;
             arr.Clear();
             if (hdprop.Value.ToString() == "")
             {
                 arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
                 bll.vDelProposalMechRebate(arr);
                 bll.vDelProposalMechOtherPromo(arr);
                 bll.vDelProposalMechDisplayRent(arr);
                 bll.vDelProposalMechFee(arr);
                 bll.vDelProposalMechCar(arr);
                 bll.vDelProposalMechCook(arr);
                 bll.vDelProposalMechSignboard(arr);
                 bll.vDelProposalMechChep(arr);
             }
             else
             {
                 arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
                 if (Session["edit"].ToString() == "true")
                 {
                     bll.vDelProposalMechRebate(arr);
                     bll.vDelProposalMechOtherPromo(arr);
                     bll.vDelProposalMechDisplayRent(arr);
                     bll.vDelProposalMechFee(arr);
                     bll.vDelProposalMechCar(arr);
                     bll.vDelProposalMechCook(arr);
                     bll.vDelProposalMechSignboard(arr);
                     bll.vDelProposalMechChep(arr);
                 }
             }

         }
         if (Session["edit"] == "true")
         {
             string[] code = txpropno.Text.Split('/');
             if (code[1] != cbpromotype.SelectedValue.ToString())
             {
                 txpropno.Text = code[0] + "/" + cbpromotype.SelectedValue.ToString() + "/" + code[2] + "/" + code[3] + "/" + code[4];
             }
         }
     }
     protected void cbpromotype_SelectedIndexChanged(object sender, EventArgs e)
     {
         if (Session["edit"] == "true")
         {
             string[] code = txpropno.Text.Split('/');
             if (code[1] != cbpromotype.SelectedValue.ToString())
             {
                 txpropno.Text = code[0] + "/" + cbpromotype.SelectedValue.ToString() + "/" + code[2] + "/" + code[3] + "/" + code[4];
             }
         }
     }
     protected void grditem_RowDeleting(object sender, GridViewDeleteEventArgs e)
     {
         Label lbitemcode;
         List<cArrayList> arr = new List<cArrayList>();
         if (hdprop.Value.ToString() == "")
         {
             lbitemcode = (Label)grditem.Rows[e.RowIndex].FindControl("lbitemcode");
             arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
         }
         else
         {
             if (Session["addproduct"].ToString() != "")
             {
                 lbitemcode = (Label)grditem.Rows[e.RowIndex].FindControl("lbitemcode");
                 //arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
             }
             else
             {
                 lbitemcode = (Label)grdviewitem.Rows[e.RowIndex].FindControl("lbitemcode");
                 //arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
             }
             arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
         }
         arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
         bll.vDelProposalItem(arr);
         arr.Clear();
         arr.Add(new cArrayList("@bgitem", cbbgitem.SelectedValue.ToString()));
         arr.Add(new cArrayList("@cust", rdcust.SelectedValue.ToString()));
         if (hdprop.Value.ToString() == "")
         {
             arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
             
         }
         else
         {
             arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
             
         }
         bll.vBindingGridToSp(ref grditem, "sp_tproposal_item_get", arr);
         bll.vBindingGridToSp(ref grdviewitem, "sp_tproposal_item_get", arr);
     }

     protected void chagreement_CheckedChanged(object sender, EventArgs e)
     {
         if (chagreement.Checked)
         {
             dtagree.CssClass = "divnormal";
             txagreementno.CssClass = "divnormal";
             lblpayment.Visible = true;
             lbldotpayment.Visible = true;
             grdpayment.Visible = true;
             cbpaymentterm.Visible = true;
             //showPayment.Visible = false;
             //grdpayment.Visible = true;
         }
         else { 
             dtagree.CssClass = "ro"; txagreementno.CssClass = "ro";
             lblpayment.Visible = false;
             lbldotpayment.Visible = false;
             grdpayment.Visible = false;
             cbpaymentterm.Visible = false;
             //showPayment.Visible = false;
         }
     }

     protected void grdcusttype_RowDeleting(object sender, GridViewDeleteEventArgs e)
     {
         Label lbcusttype = (Label)grdcusttype.Rows[e.RowIndex].FindControl("lbcusttype");
         List<cArrayList> arr = new List<cArrayList>();
         arr.Clear();
         if (hdprop.Value.ToString() == "")
         {
             arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
         }
         else
         {
             arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
         }
         arr.Add(new cArrayList("@cust_typ", lbcusttype.Text));
         bll.vDelProposalCustType(arr);
         arr.Clear();
         if (hdprop.Value.ToString() == "")
         {
             arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
         }
         else
         {
             arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
         }
         bll.vBindingGridToSp(ref grdcusttype, "sp_tproposal_custtype_get", arr);
     }

     protected void btaddsls_Click(object sender, EventArgs e)
     {
         List<cArrayList> arr = new List<cArrayList>();
         if (cbsalespoint.SelectedValue.ToString() == "")
         {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Salespoint Not yet selcted','select Salespoint','warning');", true);
            return;
         }
         else
         {
             if (hdprop.Value.ToString() == ""){
                 arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
             }else{
                 arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
             }             
             arr.Add(new cArrayList("@salespoint_cd", cbsalespoint.SelectedValue.ToString()));             
             bll.vInsertProposalSalespoint(arr); arr.Clear();
             if (hdprop.Value.ToString() == "")
             {
                 arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
             }
             else
             {
                 arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
             } 
             bll.vBindingGridToSp(ref grdslspoint, "sp_tproposal_salespoint_get", arr);
             if (cbsalespoint.SelectedValue.ToString() == "ALL | SBTC KINGDOM")
             {
                 btaddsls.CssClass = "ro";
             }
             else
             {
                 btaddsls.CssClass = "divnormal";
             }
         }
     }

     protected void grdsalespoint_RowDeleting(object sender, GridViewDeleteEventArgs e)
     {
         Label lblsalespointcd = (Label)grdslspoint.Rows[e.RowIndex].FindControl("lblslspointcd");
         List<cArrayList> arr = new List<cArrayList>();
         if (hdprop.Value.ToString() == "")
         {
             arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
         }
         else
         {
             arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
         }
         arr.Add(new cArrayList("@salespoint_cd", lblsalespointcd.Text));
         bll.vDelProposalSalespoint(arr);
         arr.Clear();
         if (hdprop.Value.ToString() == "")
         {
             arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
         }
         else
         {
             arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
         }
         bll.vBindingGridToSp(ref grdslspoint, "sp_tproposal_salespoint_get", arr);
         if (grdslspoint.Rows.Count == 0)
         {
             btaddsls.CssClass = "divnormal";
         }
     }

     protected void grdcust_RowDeleting(object sender, GridViewDeleteEventArgs e)
     {
         Label lblsalespointcd = (Label)grdcust.Rows[e.RowIndex].FindControl("lblsalespointcd");
         Label lblcustcode = (Label)grdcust.Rows[e.RowIndex].FindControl("lbcustcode");
         List<cArrayList> arr = new List<cArrayList>();
         if (hdprop.Value.ToString() == "")
         {
             arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
         }
         else
         {
             arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
         }
         arr.Add(new cArrayList("@salespoint_cd", lblsalespointcd.Text));
         arr.Add(new cArrayList("@cust_cd", lblcustcode.Text));
         bll.vDelProposalCustomerSelected(arr);
         arr.Clear();
         if (hdprop.Value.ToString() == "")
         {
             arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
         }
         else
         {
             arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
         }
         bll.vBindingGridToSp(ref grdcust, "sp_tproposal_customer_get", arr);
     }

     protected void grdcusgrcd_RowDeleting(object sender, GridViewDeleteEventArgs e)
     {
         Label lbcusgrcd = (Label)grdcusgrcd.Rows[e.RowIndex].FindControl("lbcusgrcd");
         List<cArrayList> arr = new List<cArrayList>();
         if (hdprop.Value.ToString() == "")
         {
             arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
         }
         else
         {
             arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
         }
         arr.Add(new cArrayList("@cusgrcd", lbcusgrcd.Text));
         bll.vDelProposalCustomerGroupSelected(arr);
         arr.Clear();
         if (hdprop.Value.ToString() == "")
         {
             arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
         }
         else
         {
             arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
         }
         bll.vBindingGridToSp(ref grdcusgrcd, "sp_tproposal_cusgrcd_get", arr);
     }
     protected void cbpromokind_SelectedIndexChanged(object sender, EventArgs e)
     {
         List<cArrayList> arr = new List<cArrayList>();
         arr.Add(new cArrayList("@promokind", cbpromokind.SelectedValue.ToString()));
         bll.vBindingComboToSp(ref cbpromogroup, "sp_tmst_promotion_get", "promo_cd", "promo_nm", arr);
         cbpromogroup_SelectedIndexChanged(sender, e);
     }
     protected void grdgroup_RowDeleting(object sender, GridViewDeleteEventArgs e)
     {
         int totalRow = 0;
         double totalBudget = 0.0;
         double totalFree = 0.0;
         Label lbgroupcode, lblitemcode, findlbgroup;
         List<cArrayList> arr = new List<cArrayList>();
         bool deleteProduct = true;
         //if (cbpromogroup.SelectedValue.ToString() == "TB")
         //{
         if (hdprop.Value.ToString() == "")
         {
             arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
             lbgroupcode = (Label)grdgroup.Rows[e.RowIndex].FindControl("lbgroupcode");
             lblitemcode = (Label)grdgroup.Rows[e.RowIndex].FindControl("lblitemcode");
         }
         else
         {
             if (Session["addproduct"].ToString() != "")
             {
                 //arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
                 lbgroupcode = (Label)grdgroup.Rows[e.RowIndex].FindControl("lbgroupcode");
                 lblitemcode = (Label)grdgroup.Rows[e.RowIndex].FindControl("lblitemcode");
             }
             else
             {
                 //arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
                 lbgroupcode = (Label)grdviewgroup.Rows[e.RowIndex].FindControl("lbgroupcode");
                 lblitemcode = (Label)grdviewgroup.Rows[e.RowIndex].FindControl("lblitemcode");
             }
             arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
         }
         string[] code = lblitemcode.Text.Split('_');
         arr.Add(new cArrayList("@prod_cd", lbgroupcode.Text));
         arr.Add(new cArrayList("@item_cd", code[0]));
         bll.vInsProposalProductGroup(arr);
         arr.Clear();         
         if (hdprop.Value.ToString() == "")
         {
             
             if (grdgroup.Rows.Count == 1)
             {
                 arr.Clear();
                 if (hdprop.Value.ToString() == "")
                 {
                     arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
                 }
                 else
                 {
                     arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
                 }
                 bll.vDelProposalProduct(arr);
                 bll.vDelProposalProductGroup(arr);
                 grdcost.Visible = false;
                 grdcostprodTB.Visible = false;
                 grdcostitemTB.Visible = false;
                 txtotalbudget.Visible = false;
                 lbltotalbudget.Visible = false;
                 txbudget.Text = "";
                 txfreegood.Text = "";
                 Session["budgetlimit"] = 0.0;
                 Session["freeproduct"] = 0.0;
             }
             else
             {
                 arr.Clear();
                 if (hdprop.Value.ToString() == "")
                 {
                     arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
                 }
                 else
                 {
                     arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
                 }
                 foreach (System.Web.UI.WebControls.GridViewRow row in grdgroup.Rows)
                 {
                     findlbgroup = (Label)row.FindControl("lbgroupcode");
                     if (findlbgroup.Text == lbgroupcode.Text)
                     {
                         totalRow = totalRow + 1;
                     }
                 }
                 foreach (System.Web.UI.WebControls.GridViewRow row in grdgroup.Rows)
                 {
                     findlbgroup = (Label)row.FindControl("lbgroupcode");
                     if (findlbgroup.Text == lbgroupcode.Text)
                     {
                         if (totalRow != 1)
                         {
                             deleteProduct = false;
                             break;
                         }                         
                     }
                 }
                 if (deleteProduct == true)
                 {
                     arr.Add(new cArrayList("@prod_cd", lbgroupcode.Text));
                     bll.vDelProposalProduct(arr);
                     bll.vDelProposalProductGroup(arr);
                     grdcost.Visible = false;
                     grdcostprodTB.Visible = false;
                     grdcostitemTB.Visible = false;
                     txtotalbudget.Visible = false;
                     lbltotalbudget.Visible = false;
                     txbudget.Text = "";
                     txfreegood.Text = "";
                     Session["budgetlimit"] = 0.0;
                     Session["freeproduct"] = 0.0;
                 }
             }
             arr.Clear();
             arr.Add(new cArrayList("@prod_bg", cbbgitem.SelectedValue.ToString()));
             arr.Add(new cArrayList("@cust", rdcust.SelectedValue.ToString()));
             arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
             bll.vBindingGridToSp(ref grdgroup, "sp_tproposal_productgroup_get", arr);
         }
         else
         {             
             if (Session["addproduct"].ToString() != "")
             {
                 if (grdgroup.Rows.Count == 1)
                 {
                     arr.Clear();
                     if (hdprop.Value.ToString() == "")
                     {
                         arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
                     }
                     else
                     {
                         arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
                     }
                     bll.vDelProposalProduct(arr);
                     bll.vDelProposalProductGroup(arr);
                     grdcost.Visible = false;
                     grdcostprodTB.Visible = false;
                     grdcostitemTB.Visible = false;
                     txtotalbudget.Visible = false;
                     lbltotalbudget.Visible = false;
                     txbudget.Text = "";
                     txfreegood.Text = "";
                     Session["budgetlimit"] = 0.0;
                     Session["freeproduct"] = 0.0;
                 }
                 else
                 {
                     arr.Clear();
                     if (hdprop.Value.ToString() == "")
                     {
                         arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
                     }
                     else
                     {
                         arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
                     }
                     foreach (System.Web.UI.WebControls.GridViewRow row in grdgroup.Rows)
                     {
                         findlbgroup = (Label)row.FindControl("lbgroupcode");
                         if (findlbgroup.Text == lbgroupcode.Text)
                         {
                             totalRow = totalRow + 1;
                         }
                     }
                     foreach (System.Web.UI.WebControls.GridViewRow row in grdgroup.Rows)
                     {
                         findlbgroup = (Label)row.FindControl("lbgroupcode");
                         if (findlbgroup.Text == lbgroupcode.Text)
                         {
                             if (totalRow != 1)
                             {
                                 deleteProduct = false;
                                 break;
                             }                             
                         }
                     }
                     if (deleteProduct == true)
                     {
                         arr.Add(new cArrayList("@prod_cd", lbgroupcode.Text));
                         bll.vDelProposalProduct(arr);
                         bll.vDelProposalProductGroup(arr);
                         grdcost.Visible = false;
                         grdcostprodTB.Visible = false;
                         grdcostitemTB.Visible = false;
                         txtotalbudget.Visible = false;
                         lbltotalbudget.Visible = false;
                         txbudget.Text = "";
                         txfreegood.Text = "";
                         Session["budgetlimit"] = 0.0;
                         Session["freeproduct"] = 0.0;
                     }
                 }
                 arr.Clear();
                 arr.Add(new cArrayList("@prod_bg", cbbgitem.SelectedValue.ToString()));
                 arr.Add(new cArrayList("@cust", rdcust.SelectedValue.ToString()));
                 arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
                 bll.vBindingGridToSp(ref grdgroup, "sp_tproposal_productgroup_get", arr);
             }
             else
             {                 
                 if (grdviewgroup.Rows.Count == 1)
                 {
                     arr.Clear();
                     if (hdprop.Value.ToString() == "")
                     {
                         arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
                     }
                     else
                     {
                         arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
                     }
                     bll.vDelProposalProduct(arr);
                     bll.vDelProposalProductGroup(arr);
                     grdcost.Visible = false;
                     grdcostprodTB.Visible = false;
                     grdcostitemTB.Visible = false;
                     txtotalbudget.Visible = false;
                     lbltotalbudget.Visible = false;
                     txbudget.Text = "";
                     txfreegood.Text = "";
                     Session["budgetlimit"] = 0.0;
                     Session["freeproduct"] = 0.0;
                 }
                 else
                 {
                     arr.Clear();
                     if (hdprop.Value.ToString() == "")
                     {
                         arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
                     }
                     else
                     {
                         arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
                     }
                     foreach (System.Web.UI.WebControls.GridViewRow row in grdgroup.Rows)
                     {
                         findlbgroup = (Label)row.FindControl("lbgroupcode");
                         if (findlbgroup.Text == lbgroupcode.Text)
                         {
                             totalRow = totalRow + 1;
                         }
                     }
                     foreach (System.Web.UI.WebControls.GridViewRow row in grdviewgroup.Rows)
                     {
                         findlbgroup = (Label)row.FindControl("lbgroupcode");
                         if (findlbgroup.Text == lbgroupcode.Text)
                         {
                             if (totalRow != 1)
                             {
                                 deleteProduct = false;
                                 break;
                             }                             
                         }
                     }
                     if (deleteProduct == true)
                     {
                         arr.Add(new cArrayList("@prod_cd", lbgroupcode.Text));
                         bll.vDelProposalProduct(arr);
                         bll.vDelProposalProductGroup(arr);
                         grdcost.Visible = false;
                         grdcostprodTB.Visible = false;
                         grdcostitemTB.Visible = false;
                         txtotalbudget.Visible = false;
                         lbltotalbudget.Visible = false;
                         txbudget.Text = "";
                         txfreegood.Text = "";
                         Session["budgetlimit"] = 0.0;
                         Session["freeproduct"] = 0.0;
                     }
                 }
                 arr.Clear();
                 arr.Add(new cArrayList("@prod_bg", cbbgitem.SelectedValue.ToString()));
                 arr.Add(new cArrayList("@cust", rdcust.SelectedValue.ToString()));
                 arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
                 bll.vBindingGridToSp(ref grdviewgroup, "sp_tproposal_productgroup_get", arr);
             }   
         }
         cbpaymenttype_SelectedIndexChanged(sender, e);
     }

     protected void btaddrebate_Click(object sender, EventArgs e)
     {
        List<cArrayList> arr = new List<cArrayList>();
            if (hdprop.Value.ToString() == "")
            {
                arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
            }
            else
            {
                arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
            }
        arr.Add(new cArrayList("@rbtcondition", txrbtcondition.Text.ToString()));
        arr.Add(new cArrayList("@trgtsales", txtrgtsales.Text.ToString()));
        arr.Add(new cArrayList("@trgtcost", txtrgtcost.Text.ToString()));
        bll.vInsertProposalMechRebate(arr); arr.Clear();
            if (hdprop.Value.ToString() == "")
            {
                arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
            }
            else
            {
                arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
            }
        bll.vBindingGridToSp(ref grdrebate, "sp_tproposal_mech_rebate_get", arr);
          
     }

     protected void btaddotherpromo_Click(object sender, EventArgs e)
     {
        List<cArrayList> arr = new List<cArrayList>();
            if (hdprop.Value.ToString() == "")
            {
                arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
            }
            else
            {
                arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
            }
        arr.Add(new cArrayList("@dtfrom", DateTime.ParseExact(dtotherpromofrm.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@dtto", DateTime.ParseExact(dtotherpromoto.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        bll.vInsertProposalMechOtherPromo(arr); arr.Clear();
            if (hdprop.Value.ToString() == "")
            {
                arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
            }
            else
            {
                arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
            }
        bll.vBindingGridToSp(ref grdotherpromo, "sp_tproposal_mech_otherpromo_get", arr);
     }

     protected void btaddrent_Click(object sender, EventArgs e)
     {
        List<cArrayList> arr = new List<cArrayList>();
            if (hdprop.Value.ToString() == "")
            {
                arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
            }
            else
            {
                arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
            }
        arr.Add(new cArrayList("@dtfrom", DateTime.ParseExact(dtrentfrm.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@dtto", DateTime.ParseExact(dtrentto.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@size", txsizerent.Text.ToString()));
        bll.vInsertProposalMechDisplayRent(arr); arr.Clear();
            if (hdprop.Value.ToString() == "")
            {
                arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
            }
            else
            {
                arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
            }
        bll.vBindingGridToSp(ref grddisplayrent, "sp_tproposal_mech_rent_get", arr);
     }

     protected void btaddfee_Click(object sender, EventArgs e)
     {
        List<cArrayList> arr = new List<cArrayList>();
            if (hdprop.Value.ToString() == "")
            {
                arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
            }
            else
            {
                arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
            }
        arr.Add(new cArrayList("@costval", txcostfeesr.Text.ToString()));
        arr.Add(new cArrayList("@costqty", txcostfeeqty.Text.ToString()));
        bll.vInsertProposalMechFee(arr); arr.Clear();
            if (hdprop.Value.ToString() == "")
            {
                arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
            }
            else
            {
                arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
            }
        bll.vBindingGridToSp(ref grdfee, "sp_tproposal_mech_fee_get", arr);
     }

     protected void btaddcar_Click(object sender, EventArgs e)
     {
         List<cArrayList> arr = new List<cArrayList>();
             if (hdprop.Value.ToString() == "")
             {
                 arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
             }
             else
             {
                 arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
             }
         arr.Add(new cArrayList("@size", txsizecar.Text.ToString()));
         arr.Add(new cArrayList("@placed", txplacecar.Text.ToString()));
         arr.Add(new cArrayList("@type", txtypecar.Text.ToString()));
         bll.vInsertProposalMechCar(arr); arr.Clear();
             if (hdprop.Value.ToString() == "")
             {
                 arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
             }
             else
             {
                 arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
             }
         bll.vBindingGridToSp(ref grdcar, "sp_tproposal_mech_car_get", arr);
     }

     protected void btaddcook_Click(object sender, EventArgs e)
     {
         List<cArrayList> arr = new List<cArrayList>();
             if (hdprop.Value.ToString() == "")
             {
                 arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
             }
             else
             {
                 arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
             }
         arr.Add(new cArrayList("@dtfrom", DateTime.ParseExact(dtcookfrm.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
         arr.Add(new cArrayList("@dtto", DateTime.ParseExact(dtcookto.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
         bll.vInsertProposalMechCook(arr); arr.Clear();
             if (hdprop.Value.ToString() == "")
             {
                 arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
             }
             else
             {
                 arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
             }
         bll.vBindingGridToSp(ref grdcook, "sp_tproposal_mech_cook_get", arr);
     }

     protected void btaddsb_Click(object sender, EventArgs e)
     {
         List<cArrayList> arr = new List<cArrayList>();
             if (hdprop.Value.ToString() == "")
             {
                 arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
             }
             else
             {
                 arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
             }
         arr.Add(new cArrayList("@dtfrom", DateTime.ParseExact(dtsbfrm.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
         arr.Add(new cArrayList("@dtto", DateTime.ParseExact(dtsbto.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
         arr.Add(new cArrayList("@size", txsizesb.Text.ToString()));
         arr.Add(new cArrayList("@place", cbplacesb.SelectedValue.ToString()));
         bll.vInsertProposalMechSignboard(arr); arr.Clear();
             if (hdprop.Value.ToString() == "")
             {
                 arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
             }
             else
             {
                 arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
             }
         bll.vBindingGridToSp(ref grdsignboard, "sp_tproposal_mech_signboard_get", arr);
     }

     protected void btaddchep_Click(object sender, EventArgs e)
     {
         List<cArrayList> arr = new List<cArrayList>();
             if (hdprop.Value.ToString() == "")
             {
                 arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
             }
             else
             {
                 arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
             }
         arr.Add(new cArrayList("@value", txvaluechep.Text.ToString()));
         arr.Add(new cArrayList("@place", txmechchep.Text.ToString()));
         bll.vInsertProposalMechChep(arr); arr.Clear();
             if (hdprop.Value.ToString() == "")
             {
                 arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
             }
             else
             {
                 arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
             }
         bll.vBindingGridToSp(ref grdchep, "sp_tproposal_mech_chep_get", arr);
     }

     protected void grdrebate_RowDeleting(object sender, GridViewDeleteEventArgs e)
     {
         Label lblrbtcondition = (Label)grdrebate.Rows[e.RowIndex].FindControl("lblrbtcondition");
         List<cArrayList> arr = new List<cArrayList>();
             if (hdprop.Value.ToString() == "")
             {
                 arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
             }
             else
             {
                 arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
             }
         arr.Add(new cArrayList("@rbtcondition", lblrbtcondition.Text));
         bll.vDelProposalMechRebate(arr);
         arr.Clear();
             if (hdprop.Value.ToString() == "")
             {
                 arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
             }
             else
             {
                 arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
             }
         bll.vBindingGridToSp(ref grdrebate, "sp_tproposal_mech_rebate_get", arr);
     }

     protected void grdotherpromo_RowDeleting(object sender, GridViewDeleteEventArgs e)
     {
         Label lbldtotherpromofrm = (Label)grdotherpromo.Rows[e.RowIndex].FindControl("lbldtotherpromofrm");
         Label lbldtotherpromoto = (Label)grdotherpromo.Rows[e.RowIndex].FindControl("lbldtotherpromoto");
         List<cArrayList> arr = new List<cArrayList>();
             if (hdprop.Value.ToString() == "")
             {
                 arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
             }
             else
             {
                 arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
             }
         arr.Add(new cArrayList("@dtfrom", lbldtotherpromofrm.Text));
         arr.Add(new cArrayList("@dtto", lbldtotherpromoto.Text));
         bll.vDelProposalMechOtherPromo(arr);
         arr.Clear();
             if (hdprop.Value.ToString() == "")
             {
                 arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
             }
             else
             {
                 arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
             }
         bll.vBindingGridToSp(ref grdotherpromo, "sp_tproposal_mech_otherpromo_get", arr);
     }

     protected void grddisplayrent_RowDeleting(object sender, GridViewDeleteEventArgs e)
     {
         Label lblsizerent = (Label)grddisplayrent.Rows[e.RowIndex].FindControl("lblsizerent");
         List<cArrayList> arr = new List<cArrayList>();
             if (hdprop.Value.ToString() == "")
             {
                 arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
             }
             else
             {
                 arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
             }
         arr.Add(new cArrayList("@size", lblsizerent.Text));
         bll.vDelProposalMechDisplayRent(arr);
         arr.Clear();
             if (hdprop.Value.ToString() == "")
             {
                 arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
             }
             else
             {
                 arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
             }
         bll.vBindingGridToSp(ref grddisplayrent, "sp_tproposal_mech_rent_get", arr);
     }

     protected void grdfee_RowDeleting(object sender, GridViewDeleteEventArgs e)
     {
         Label lblcostval = (Label)grdfee.Rows[e.RowIndex].FindControl("lblcostval");
         Label lblcostqty = (Label)grdfee.Rows[e.RowIndex].FindControl("lblcostqty");
         List<cArrayList> arr = new List<cArrayList>();
             if (hdprop.Value.ToString() == "")
             {
                 arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
             }
             else
             {
                 arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
             }
         arr.Add(new cArrayList("@costval", lblcostval.Text));
         arr.Add(new cArrayList("@costqty", lblcostqty.Text));
         bll.vDelProposalMechFee(arr);
         arr.Clear();
             if (hdprop.Value.ToString() == "")
             {
                 arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
             }
             else
             {
                 arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
             }
         bll.vBindingGridToSp(ref grdfee, "sp_tproposal_mech_fee_get", arr);
     }

     protected void grdcar_RowDeleting(object sender, GridViewDeleteEventArgs e)
     {
         Label lblsizecar = (Label)grdcar.Rows[e.RowIndex].FindControl("lblcarsize");
         List<cArrayList> arr = new List<cArrayList>();
             if (hdprop.Value.ToString() == "")
             {
                 arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
             }
             else
             {
                 arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
             }
         arr.Add(new cArrayList("@size", lblsizecar.Text));
         bll.vDelProposalMechCar(arr);
         arr.Clear();
             if (hdprop.Value.ToString() == "")
             {
                 arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
             }
             else
             {
                 arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
             }
         bll.vBindingGridToSp(ref grdfee, "sp_tproposal_mech_car_get", arr);
     }

     protected void grdcook_RowDeleting(object sender, GridViewDeleteEventArgs e)
     {
         Label lbldtcookfrm = (Label)grdcook.Rows[e.RowIndex].FindControl("lbldtcookfrm");
         Label lbldtcookto = (Label)grdcook.Rows[e.RowIndex].FindControl("lbldtcookto");
         List<cArrayList> arr = new List<cArrayList>();
             if (hdprop.Value.ToString() == "")
             {
                 arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
             }
             else
             {
                 arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
             }
         arr.Add(new cArrayList("@dtfrom", lbldtcookfrm.Text));
         arr.Add(new cArrayList("@dtto", lbldtcookto.Text));
         bll.vDelProposalMechCook(arr);
         arr.Clear();
             if (hdprop.Value.ToString() == "")
             {
                 arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
             }
             else
             {
                 arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
             }
         bll.vBindingGridToSp(ref grdcook, "sp_tproposal_mech_cook_get", arr);
     }

     protected void grdsignboard_RowDeleting(object sender, GridViewDeleteEventArgs e)
     {
         Label lblsizesb = (Label)grdsignboard.Rows[e.RowIndex].FindControl("lblsizesb");
         List<cArrayList> arr = new List<cArrayList>();
             if (hdprop.Value.ToString() == "")
             {
                 arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
             }
             else
             {
                 arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
             }
         arr.Add(new cArrayList("@size", lblsizesb.Text));
         bll.vDelProposalMechSignboard(arr);
         arr.Clear();
             if (hdprop.Value.ToString() == "")
             {
                 arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
             }
             else
             {
                 arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
             }
         bll.vBindingGridToSp(ref grdsignboard, "sp_tproposal_mech_signboard_get", arr);
     }

     protected void grdchep_RowDeleting(object sender, GridViewDeleteEventArgs e)
     {
         Label lblvaluechep = (Label)grdchep.Rows[e.RowIndex].FindControl("lblvaluechep");
         List<cArrayList> arr = new List<cArrayList>();
             if (hdprop.Value.ToString() == "")
             {
                 arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
             }
             else
             {
                 arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
             }
         arr.Add(new cArrayList("@value", lblvaluechep.Text));
         bll.vDelProposalMechChep(arr);
         arr.Clear();
             if (hdprop.Value.ToString() == "")
             {
                 arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
             }
             else
             {
                 arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
             }
         bll.vBindingGridToSp(ref grdchep, "sp_tproposal_mech_chep_get", arr);
     }

     protected void cbbgitem_SelectedIndexChanged(object sender, EventArgs e)
     {
         if (cbbgitem.SelectedValue.ToString() == "B")
         {
             txbgitempercent.Visible = true;
             lblbgitem.Visible = true;
             lblbgtitleitem.Visible = true;
             lblbgtitleitem.Text = "Target";
             txbgitemtarget.Visible = true;
             cbbgitemtarget.Visible = true;
             txbgitemx.Visible = false;
             txbgitemy.Visible = false;
             lblbgitemmanual.Visible = false;
             
             lblddirect.Visible = false;
             lblbgdirect.Visible = false;
             txbgdirect.Visible = false;
             lblmaxdirect.Visible = false;
             txmaxdirect.Visible = false;
             lblmaxdirectq.Visible = false;

         }
         else if (cbbgitem.SelectedValue.ToString() == "A")
         {
             txbgitempercent.Visible = false;
             lblbgitem.Visible = false;
             lblbgtitleitem.Visible = true;
             lblbgtitleitem.Text = "Limit";
             txbgitemtarget.Visible = true;
             cbbgitemtarget.Visible = false;
             txbgitemx.Visible = true;
             txbgitemy.Visible = true;
             lblbgitemmanual.Visible = true;

             lblddirect.Visible = false;
             lblbgdirect.Visible = false;
             txbgdirect.Visible = false;
             lblmaxdirect.Visible = false;
             txmaxdirect.Visible = false;
             lblmaxdirectq.Visible = false;
         }
         else if (cbbgitem.SelectedValue.ToString() == "C")
         {
             txbgitempercent.Visible = false;
             lblbgitem.Visible = false;
             lblbgtitleitem.Visible = false;
             txbgitemtarget.Visible = false;
             cbbgitemtarget.Visible = false;
             txbgitemx.Visible = false;
             txbgitemy.Visible = false;
             lblbgitemmanual.Visible = false;

             lblddirect.Visible = true;
             lblbgdirect.Visible = true;
             txbgdirect.Visible = true;
             lblmaxdirect.Visible = true;
             txmaxdirect.Visible = true;
             lblmaxdirectq.Visible = true;
         }
         else if (cbbgitem.SelectedValue.ToString() == "D")
         {
             txbgitempercent.Visible = true;
             lblbgitem.Visible = true;
             lblbgtitleitem.Visible = false;
             txbgitemtarget.Visible = false;
             cbbgitemtarget.Visible = false;
             txbgitemx.Visible = false;
             txbgitemy.Visible = false;
             lblbgitemmanual.Visible = false;

             lblddirect.Visible = false;
             lblbgdirect.Visible = false;
             txbgdirect.Visible = false;
             lblmaxdirect.Visible = false;
             txmaxdirect.Visible = false;
             lblmaxdirectq.Visible = false;
         }
         else
         {
             txbgitempercent.Visible = false;
             lblbgitem.Visible = false;
             lblbgtitleitem.Visible = false;
             txbgitemtarget.Visible = false;
             cbbgitemtarget.Visible = false;
             txbgitemx.Visible = false;
             txbgitemy.Visible = false;
             lblbgitemmanual.Visible = false;

             lblddirect.Visible = false;
             lblbgdirect.Visible = true;
             txbgdirect.Visible = true;
             lblmaxdirect.Visible = false;
             txmaxdirect.Visible = false;
             lblmaxdirectq.Visible = false;
         }
     }
     protected void btprint_Click(object sender, EventArgs e)
     {
         creport rep = new creport();
         List<cArrayList> arr = new List<cArrayList>();
         arr.Clear();
         arr.Add(new cArrayList("@prop_no", txpropno.Text));
         arr.Add(new cArrayList("@cust", rdcust.SelectedValue.ToString()));
         arr.Add(new cArrayList("@product", rditem.SelectedValue.ToString()));
         arr.Add(new cArrayList("@salespoint", null));
         arr.Add(new cArrayList("@dbp", "yes"));
         arr.Add(new cArrayList("@vendor", null));
         arr.Add(new cArrayList("@cost", null));
         arr.Add(new cArrayList("@promo", null));
         arr.Add(new cArrayList("@year", null));
         arr.Add(new cArrayList("@month", null));
         //Server.MapPath("upload//proposal//" + txpropno.Text.ToString().Replace("/", "_"))

         string sPath = bll.sGetControlParameter("image_path") + "/proposal_doc/" + txpropno.Text.ToString().Replace("/", "_") + ".pdf";//Server.MapPath("upload//proposal//"); //bll.sGetControlParameter("image_path") + "/claim_doc/";
         string sPdfName0 = txpropno.Text.ToString().Replace("/", "_") + ".pdf";
         rep.vShowReportToPDF("rp_proposal.rpt", arr, sPath);

         //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=prop&prop_no=" + txpropno.Text + "&cust=" + rdcust.SelectedValue.ToString() + "&product=" + rditem.SelectedValue.ToString() + "&dbp=yes');", true);
     }
     protected void btprint2_Click(object sender, EventArgs e)
     {
         ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=prop&prop_no=" + txpropno.Text + "&cust=" + rdcust.SelectedValue.ToString() + "&product=" + rditem.SelectedValue.ToString() + "&dbp=no');", true);
     }

     protected void btapprove_Click(object sender, EventArgs e)
     {
         string sDiscount = ""; string sRdCust = ""; string sRdItem = "";
         List<cArrayList> arr = new List<cArrayList>();

         if (bll.nCheckAccess("appproposal", Request.Cookies["usr_id"].Value.ToString()) == 0)
         {
             ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To approve this proposal, contact Administrator !!','warning');", true);
             return;
         }
         else
         {
             if (Double.Parse(txtotalbudget.Text.ToString()) >= 5000 && Double.Parse(txtotalbudget.Text.ToString()) <= 10000)
             {
                 if (bll.nCheckAccess("propbg10000", Request.Cookies["usr_id"].Value.ToString()) == 0)
                 {
                     ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To approve this proposal, contact Administrator !!','warning');", true);
                     return;
                 }
             }
             else if (Double.Parse(txtotalbudget.Text.ToString()) <= 5000)
             {
                 if (bll.nCheckAccess("propbg5000", Request.Cookies["usr_id"].Value.ToString()) == 0)
                 {
                     ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To approve this proposal, contact Administrator !!','warning');", true);
                     return;
                 }
             }
             else if (Double.Parse(txtotalbudget.Text.ToString()) > 10000)
             {
                 if (bll.nCheckAccess("propbgunl", Request.Cookies["usr_id"].Value.ToString()) == 0)
                 {
                     ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To approve this proposal, contact Administrator !!','warning');", true);
                     return;
                 }
             }
         }

         

         arr.Add(new cArrayList("@prop_no", txpropno.Text.ToString()));
         arr.Add(new cArrayList("@approveby", Request.Cookies["usr_id"].Value.ToString()));
         arr.Add(new cArrayList("@approval", 1));
         bll.vUpdatetMstProposal(arr);
         arr.Clear();
         
         btprint.Visible = true;
         btprint2.Visible = true;
         btsave.Visible = false;
         if ((Request.Cookies["usr_id"].Value.ToString() == "3064") || (Request.Cookies["usr_id"].Value.ToString() == "1287"))
         {
             btapprove.Visible = false;
             btcancel.Visible = true;
             btedit.Visible = true;
             btdelete.Visible = true;
         }
         else
         {
             btapprove.Visible = false;
             btcancel.Visible = false;
             btedit.Visible = false;
             btdelete.Visible = false;
         }
         btedit.Visible = false;
         //upl.Visible = true;
         //btupload.Visible = true;

         // Generated Discount 
         if (cbpromotype.SelectedValue.ToString() == "SC" || cbpromotype.SelectedValue.ToString() == "LT" || cbpromotype.SelectedValue.ToString() == "IP" || cbpromotype.SelectedValue.ToString() == "MF")
         {
             if (chdiscount.Checked)
             {
                arr.Clear();
                System.Data.SqlClient.SqlDataReader rs = null;
                arr.Add(new cArrayList("@sys", "discount"));
                arr.Add(new cArrayList("@sysno", ""));
                bll.vGetDiscountNo(arr, ref rs);
                while (rs.Read())
                {
                    sDiscount = rs["generated"].ToString();
                }
                arr.Clear();
                arr.Add(new cArrayList("@prop_no", txpropno.Text));
                arr.Add(new cArrayList("@disc_no", sDiscount));
                //bll.vGeneratedProposalDisc(arr);
                bll.vInsertDiscSalespoint(arr);
                sRdCust = rdcust.SelectedValue.ToString();
                if (sRdCust == "C")
                {
                    bll.vInsertDiscCustomer(arr);
                }
                else if (sRdCust == "G")
                {
                    bll.vInsertDiscCusgrcd(arr);
                }
                else
                {
                    bll.vInsertDiscCustype(arr);
                }
                sRdItem = rditem.SelectedValue.ToString();
                if (sRdItem == "I")
                {
                    bll.vInsertDiscItem(arr);
                }
                else
                {
                    bll.vInsertDiscProduct(arr);
                }
                bll.vGeneratedDiscountFormula(arr);
                if (bll.vLookUp("select rditemfree from tmst_proposal where prop_no='" + txpropno.Text + "'") == "I")
                {
                    bll.vGeneratedDiscountFreeitem(arr);
                }
                else if (bll.vLookUp("select rditemfree from tmst_proposal where prop_no='" + txpropno.Text + "'") == "G")
                {
                    bll.vGeneratedDiscountFreeProduct(arr);
                }
                arr.Clear();
                //Save main discount data
                arr.Add(new cArrayList("@disc_cd", sDiscount));
                arr.Add(new cArrayList("@proposal_no", txpropno.Text));
                arr.Add(new cArrayList("@start_dt", DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@end_dt", DateTime.ParseExact(dtend.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@delivery_dt", DateTime.ParseExact(dtdelivery.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@disc_dt", DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));                
                arr.Add(new cArrayList("@disc_typ", cbpromotype.SelectedValue.ToString()));
                arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@disc_sta_id", "A"));
                arr.Add(new cArrayList("@remark", txbgremark.Text));
                arr.Add(new cArrayList("@rdcustomer", rdcust.SelectedValue.ToString())); 
                arr.Add(new cArrayList("@propvendor_no", txpropvendor.Text));
                arr.Add(new cArrayList("@discount_mec", cbpaymenttype.SelectedValue.ToString()));
                arr.Add(new cArrayList("@discount_use", "A"));
                arr.Add(new cArrayList("@vendor_cd", cbvendor.SelectedValue.ToString()));
                arr.Add(new cArrayList("@benefitpromotion", "NONE"));
                if (sRdItem == "I")
                {
                    arr.Add(new cArrayList("@qty_min", bll.vLookUp("select top 1 x from tproposal_item where prop_no='" + txpropno.Text + "'")));
                    arr.Add(new cArrayList("@rditem", "P"));
                }
                else
                {
                    arr.Add(new cArrayList("@qty_min", bll.vLookUp("select top 1 x from tproposal_product where prop_no='" + txpropno.Text + "'")));
                    arr.Add(new cArrayList("@rditem", "G"));
                }
                arr.Add(new cArrayList("@qty_max", txbudget.Text.Replace(",", "").ToString()));
                arr.Add(new cArrayList("@regularcost", "0.0"));
                arr.Add(new cArrayList("@netcost", "0.0"));
                arr.Add(new cArrayList("@rdfreeitem", bll.vLookUp("select rditemfree from tmst_proposal where prop_no='" + txpropno.Text + "'")));
                arr.Add(new cArrayList("@catalogimage", ""));
                bll.vInsertMstDiscount(arr);

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Proposal has been Approved','" + txpropno.Text + ", and "+sDiscount+" generated.','success');", true);
             }else
             {
                sDiscount = "No Discount";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Proposal has been Approved','" + txpropno.Text + ", and "+sDiscount+" generated.','success');", true);
             }
         }else{
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Proposal has been Approved','" + txpropno.Text + "','success');", true);
         }
         
         return;
     }

     protected void btcancel_Click(object sender, EventArgs e)
     {
         List<cArrayList> arr = new List<cArrayList>();
         arr.Add(new cArrayList("@prop_no", txpropno.Text.ToString()));
         arr.Add(new cArrayList("@approval", 2));
         bll.vUpdatetMstProposal(arr);
         arr.Clear();
         ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Proposal has been Cancel / Reject','" + txpropno.Text + "','success');", true);
         btprint.Visible = true;
         btprint2.Visible = true;
         btsave.Visible = false;
         if ((Request.Cookies["usr_id"].Value.ToString() == "3064") || (Request.Cookies["usr_id"].Value.ToString() == "1287"))
         {
             btapprove.Visible = true;
             btcancel.Visible = false;
             btedit.Visible = true;
             btdelete.Visible = true;
         }
         else
         {
             btapprove.Visible = false;
             btcancel.Visible = false;
             btedit.Visible = false;
             btdelete.Visible = false;
         }
         btedit.Visible = false;
         return;
     }

     protected void btlookup_Click(object sender, EventArgs e)
     {
         Session["edit"] = "";
         //btedit.Enabled = false;
         cbappvendor.Visible = false;
         System.Data.SqlClient.SqlDataReader rs = null;
         List<cArrayList> arr = new List<cArrayList>();
         arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
         Session["hdprop"] = hdprop.Value.ToString();
         bll.vGetProposal(arr, ref rs);
         while (rs.Read())
         {
             if (rs["approval"].ToString() == "Approved")
             {
                 
                 if ((Request.Cookies["usr_id"].Value.ToString() == "3064") || (Request.Cookies["usr_id"].Value.ToString() == "1287"))
                 {
                     btapprove.Visible = false;
                     btcancel.Visible = false;
                     btedit.Visible = true;
                     btdelete.Visible = false;
                 }
                 else
                 {
                     btapprove.Visible = false;
                     btcancel.Visible = false;
                     btedit.Visible = false;
                     btdelete.Visible = false;
                 }

                 if ((Request.Cookies["usr_id"].Value.ToString() == "3064") || (Request.Cookies["usr_id"].Value.ToString() == "1287"))
                 {
                     btapprove.Visible = false;
                     btcancel.Visible = true;
                     btedit.Visible = true;
                     btdelete.Visible = true;
                 }
             }
             else
             {
                 if ((Request.Cookies["usr_id"].Value.ToString() == "3064") || (Request.Cookies["usr_id"].Value.ToString() == "1287"))
                 {
                     btapprove.Visible = false;
                     btcancel.Visible = false;
                     btedit.Visible = true;
                     btdelete.Visible = false;
                 }
                 else
                 {
                     btapprove.Visible = false;
                     btcancel.Visible = false;
                     btedit.Visible = false;
                     btdelete.Visible = false;
                 }

                 if ((Request.Cookies["usr_id"].Value.ToString() == "3064") || (Request.Cookies["usr_id"].Value.ToString() == "1287"))
                 {
                     btapprove.Visible = true;
                     btcancel.Visible = true;
                     btedit.Visible = true;
                     btdelete.Visible = true;
                 }
             }     

             if (rs["prop_scan"].ToString() == "")
             {
                 if (rs["approval"].ToString() == "Approved")
                 {
                     //upl.Visible = true;
                     //btupload.Visible = true;
                     //hpfile_nm.Visible = false;
                     //grdcate.Visible = true;
                     //btnuploaddoc.Visible = true;                     
                 }
                 else
                 {
                     //upl.Visible = false;
                     //btupload.Visible = false;
                     //hpfile_nm.Visible = false;
                 }                 
             }
             else
             {
                 //btupload.Visible = false;
                 //hpfile_nm.Visible = true;
                 //upl.Visible = false;
                 //lbfileloc.Text = rs["prop_scan"].ToString();
                 //hpfile_nm.NavigateUrl = "/images/proposal/" + rs["prop_scan"].ToString();
                 //hpfile_nm.NavigateUrl = "file:///d:/images/proposal/" + rs["prop_scan"].ToString();
                 //hpfile_nm.NavigateUrl = "~\\upload\\proposal\\" + rs["prop_scan"].ToString();
             }

             if (rs["sign_frame"].ToString() == "")
             {
                 cbsignframe.SelectedValue = "F1";
             }
             else
             {
                 cbsignframe.SelectedValue = rs["sign_frame"].ToString();
             }
             //// Budget Calculation 
             //chfreegood.CssClass = "ro";
             //chfreegood.Checked = rs["budget_free"].ToString();
             //rdfreeitem.SelectedValue = rs["rditem"].ToString();
             // Budget Calculation 
             cbsignframe.CssClass = "ro";
             txpropno.Text = hdprop.Value.ToString();
             cbvendor.SelectedValue = rs["vendor_cd"].ToString();
             cbvendor.CssClass = "ro";
             cbvendor_SelectedIndexChanged(sender, e);
             dtprop.Text = rs["prop_dt"].ToString();
             dtprop.CssClass = "ro";
             rditem.SelectedValue = rs["rditem"].ToString();
             rditem.CssClass = "ro";
             dtstart.Text = rs["start_dt"].ToString();
             dtstart.CssClass = "ro";
             dtend.Text = rs["end_dt"].ToString();
             dtend.CssClass = "ro";
             dtdelivery.Text = rs["delivery_dt"].ToString();
             dtdelivery.CssClass = "ro";
             dtclaim.Text = rs["claim_dt"].ToString();
             dtclaim.CssClass = "ro";
             dtadjuststart.Text = rs["adjuststart_dt"].ToString();
             dtadjuststart.CssClass = "ro";
             dtadjustend.Text = rs["adjustend_dt"].ToString();
             dtadjustend.CssClass = "ro";
             //cbappvendor.SelectedValue = rs["vendor_app"].ToString();
             //cbappvendor.CssClass = "ro";
             txpropvendor.Text = rs["prop_no_vendor"].ToString();
             txpropvendor.CssClass = "ro";
             txrefno.Text = rs["ref_no"].ToString();
             txrefno.CssClass = "ro";
             cbpromokind.SelectedValue = rs["promokind"].ToString();
             cbpromokind.CssClass = "ro";
             cbpromokind_SelectedIndexChanged(sender, e);
             cbpromogroup.SelectedValue = rs["promo_cd"].ToString();
             cbpromogroup.CssClass = "ro";
             cbpromogroup_SelectedIndexChanged(sender, e);
             cbpromotype.SelectedValue = rs["promo_typ"].ToString();
             cbpromotype.CssClass = "ro";
             cbmarketingcost.SelectedValue = rs["rdcost"].ToString();
             cbmarketingcost.CssClass = "ro";
             txcostsbtc.Text = rs["sbtccost"].ToString();
             txcostsbtc.CssClass = "ro";
             txprincipalcost.Text = rs["principalcost"].ToString();
             txprincipalcost.CssClass = "ro";
             chclaim.Checked = bool.Parse(rs["bclaim"].ToString());
             //chclaim.CssClass = "ro";
             // salespoint
             tblSalespoint.Visible = false;
             bll.vBindingGridToSp(ref grdslspoint, "sp_tproposal_salespoint_get", arr);
             grdslspoint.CssClass = "ro";
             // salespoint
             //customer
             rdcust.SelectedValue = rs["rdcust"].ToString();
             Session["cust"] = rs["rdcust"].ToString();
             rdcust_SelectedIndexChanged(sender, e);
             if (rs["rdcust"].ToString() == "C")
             {
                 grdcust.Visible = true;
                 grdcusgrcd.Visible = false;
                 grdcusttype.Visible = false;
                 bll.vBindingGridToSp(ref grdcust, "sp_tproposal_customer_get", arr);
             }
             else if (rs["rdcust"].ToString() == "G")
             {
                 grdcusgrcd.Visible = true;
                 grdcust.Visible = false;
                 grdcusttype.Visible = false;
                 bll.vBindingGridToSp(ref grdcusgrcd, "sp_tproposal_cusgrcd_get", arr);
             }
             else if (rs["rdcust"].ToString() == "T")
             {
                 grdcusttype.Visible = true;
                 grdcusgrcd.Visible = false;
                 grdcust.Visible = false;
                 bll.vBindingGridToSp(ref grdcusttype, "sp_tproposal_custtype_get", arr);
             }
             tblCustomer.Visible = false;
             rdcust.CssClass = "ro";
             grdcust.CssClass = "ro";
             grdcusgrcd.CssClass = "ro";
             grdcusttype.CssClass = "ro";
             //customer
             //Item
             tblItem.Visible = false;
             //grditem.CssClass = "ro";
             //grdgroup.CssClass = "ro";
             rditem_SelectedIndexChanged(sender, e);
             if (rs["rditem"].ToString() == "I")
             {
                 arr.Add(new cArrayList("@cust", rs["rdcust"].ToString()));
                 bll.vBindingGridToSp(ref grdviewitem, "sp_tproposal_item_get", arr);
                 grdviewitem.CssClass = "divgrid";
                 grdviewgroup.CssClass = "divhid";
                 grdviewitem.CssClass = "ro";
                 grdviewitem.Visible = true;
                 //tblItem.Visible = true;
                 //grditem.CssClass = "divgrid";
                 //grdgroup.CssClass = "divgrid";
                 //arr.Add(new cArrayList("@cust", rs["rdcust"].ToString()));
                 //bll.vBindingGridToSp(ref grditem, "sp_tproposal_item_get", arr);
             }
             else if (rs["rditem"].ToString() == "G")
             {
                 arr.Add(new cArrayList("@cust", rs["rdcust"].ToString()));
                 bll.vBindingGridToSp(ref grdviewgroup, "sp_tproposal_productgroup_get", arr);
                 grdviewgroup.CssClass = "divgrid";
                 grdviewitem.CssClass = "divhid";
                 grdviewgroup.CssClass = "ro";
                 grdviewgroup.Visible = true;
                 //grditem.CssClass = "divgrid";
                 //grdgroup.CssClass = "divgrid";
                 //arr.Add(new cArrayList("@cust", rs["rdcust"].ToString()));
                 //bll.vBindingGridToSp(ref grdgroup, "sp_tproposal_product_get", arr);
             }   
             //Item
             //Mechanism
             //cbpromogroup_SelectedIndexChanged(sender, e);
             arr.Clear();
             arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
             tblRebate.Visible = false;
             tblOther.Visible = false;
             tblRent.Visible = false;
             tblFee.Visible = false;
             tblCar.Visible = false;
             tblCook.Visible = false;
             tblSignBoard.Visible = false;
             tblChep.Visible = false;
             
             if (rs["promo_cd"].ToString() == "RB")
             {
                 bll.vBindingGridToSp(ref grdrebate, "sp_tproposal_mech_rebate_get", arr);
                 grdrebate.CssClass = "divgrid";
                 grddisplayrent.CssClass = "divhid";
                 grdfee.CssClass = "divhid";
                 grdotherpromo.CssClass = "divhid";
                 grdcar.CssClass = "divhid";
                 grdcook.CssClass = "divhid";
                 grdsignboard.CssClass = "divhid";
                 grdchep.CssClass = "divhid";
             }
             else if (rs["promo_cd"].ToString() == "DR")
             {
                 bll.vBindingGridToSp(ref grddisplayrent, "sp_tproposal_mech_rent_get", arr);
                 grdrebate.CssClass = "divhid";
                 grddisplayrent.CssClass = "divgrid";
                 grdfee.CssClass = "divhid";
                 grdotherpromo.CssClass = "divhid";
                 grdcar.CssClass = "divhid";
                 grdcook.CssClass = "divhid";
                 grdsignboard.CssClass = "divhid";
                 grdchep.CssClass = "divhid";
             }
             else if ((rs["promo_cd"].ToString() == "OF") || (rs["promo_cd"].ToString() == "PB") || (rs["promo_cd"].ToString() == "LF"))
             {
                 bll.vBindingGridToSp(ref grdfee, "sp_tproposal_mech_fee_get", arr);
                 grdrebate.CssClass = "divhid";
                 grddisplayrent.CssClass = "divhid";
                 grdfee.CssClass = "divgrid";
                 grdotherpromo.CssClass = "divhid";
                 grdcar.CssClass = "divhid";
                 grdcook.CssClass = "divhid";
                 grdsignboard.CssClass = "divhid";
                 grdchep.CssClass = "divhid";
             }
             else if ((rs["promo_cd"].ToString() == "GS") || (rs["promo_cd"].ToString() == "IP") || (rs["promo_cd"].ToString() == "LT") || (rs["promo_cd"].ToString() == "NE") || (rs["promo_cd"].ToString() == "MF") || (rs["promo_cd"].ToString() == "SD") || (rs["promo_cd"].ToString() == "SI") || (rs["promo_cd"].ToString() == "TB"))
             {
                 bll.vBindingGridToSp(ref grdotherpromo, "sp_tproposal_mech_otherpromo_get", arr);
                 grdrebate.CssClass = "divhid";
                 grddisplayrent.CssClass = "divhid";
                 grdfee.CssClass = "divhid";
                 grdotherpromo.CssClass = "divgrid";
                 grdcar.CssClass = "divhid";
                 grdcook.CssClass = "divhid";
                 grdsignboard.CssClass = "divhid";
                 grdchep.CssClass = "divhid";
             }
             else if (rs["promo_cd"].ToString() == "CB")
             {
                 bll.vBindingGridToSp(ref grdcar, "sp_tproposal_mech_car_get", arr);
                 grdrebate.CssClass = "divhid";
                 grddisplayrent.CssClass = "divhid";
                 grdfee.CssClass = "divhid";
                 grdotherpromo.CssClass = "divhid";
                 grdcar.CssClass = "divgrid";
                 grdcook.CssClass = "divhid";
                 grdsignboard.CssClass = "divhid";
                 grdchep.CssClass = "divhid";
             }
             else if ((rs["promo_cd"].ToString() == "CD") || (rs["promo_cd"].ToString() == "SP") || (rs["promo_cd"].ToString() == "RP"))
             {
                 bll.vBindingGridToSp(ref grdcook, "sp_tproposal_mech_cook_get", arr);
                 grdrebate.CssClass = "divhid";
                 grddisplayrent.CssClass = "divhid";
                 grdfee.CssClass = "divhid";
                 grdotherpromo.CssClass = "divhid";
                 grdcar.CssClass = "divhid";
                 grdcook.CssClass = "divgrid";
                 grdsignboard.CssClass = "divhid";
                 grdchep.CssClass = "divhid";
             }
             else if (rs["promo_cd"].ToString() == "SB")
             {
                 bll.vBindingGridToSp(ref grdsignboard, "sp_tproposal_mech_signboard_get", arr);
                 grdrebate.CssClass = "divhid";
                 grddisplayrent.CssClass = "divhid";
                 grdfee.CssClass = "divhid";
                 grdotherpromo.CssClass = "divhid";
                 grdcar.CssClass = "divhid";
                 grdcook.CssClass = "divhid";
                 grdsignboard.CssClass = "divgrid";
                 grdchep.CssClass = "divhid";
             }
             else if (rs["promo_cd"].ToString() == "CP")
             {
                 bll.vBindingGridToSp(ref grdchep, "sp_tproposal_mech_chep_get", arr);
                 grdrebate.CssClass = "divhid";
                 grddisplayrent.CssClass = "divhid";
                 grdfee.CssClass = "divhid";
                 grdotherpromo.CssClass = "divhid";
                 grdcar.CssClass = "divhid";
                 grdcook.CssClass = "divhid";
                 grdsignboard.CssClass = "divhid";
                 grdchep.CssClass = "divgrid";
             }
             grdrebate.CssClass = "ro";
             grddisplayrent.CssClass = "ro";
             grdfee.CssClass = "ro";
             grdotherpromo.CssClass = "ro";
             grdcar.CssClass = "ro";
             grdcook.CssClass = "ro";
             grdsignboard.CssClass = "ro";
             grdchep.CssClass = "ro";
             //Mechanism
             // Agreement
             dtagree.Text = rs["agreement_dt"].ToString();
             dtagree.CssClass = "ro";
             chagreement.CssClass = "ro";
             txagreementno.Text = rs["agreement_no"].ToString();
             txagreementno.CssClass = "ro";
             if (txagreementno.Text != "")
             {
                 chagreement.Checked = true;
             }
             else
             {
                 chagreement.Checked = false;
             }
             // Agreement

             //cbbudgettype.SelectedValue = rs["rdbudget"].ToString();
             //cbbudgettype.CssClass = "ro";
             //cbbudgettype_SelectedIndexChanged(sender, e);
             // background product
             if (rs["bg"].ToString() != "")
             {
                 cbbgitem.SelectedValue = rs["bg"].ToString();
                 cbbgitem_SelectedIndexChanged(sender, e);
             }             
             // background product
             //Background Calculation
             chbudget.CssClass = "ro";
             chbudget.Checked = bool.Parse(rs["budget_calc"].ToString());
             //Background Calculation
             txbudget.Text = rs["budget"].ToString();
             txbudget.CssClass = "ro";
             txfreegood.CssClass = "ro";
             txtotalbudget.CssClass = "ro";
             txtotalbudget.Text = rs["budget_limit"].ToString();
             txaddbudget.Text = rs["budget_add"].ToString();
             txaddbudget.CssClass = "ro";
             txbudgetmtd.CssClass = "ro";
             txbudgetytd.CssClass = "ro";
             txbudgetmtd.Text = rs["mtd"].ToString();
             txbudgetytd.Text = rs["ytd"].ToString();

             //Free Product Budgeting
             chfreegood.Checked = bool.Parse(rs["budget_free"].ToString());
             if (chfreegood.Checked)
             {
                 arr.Clear();
                 rdfreeitem.SelectedValue = rs["rditemfree"].ToString();
                 rdfreeitem_SelectedIndexChanged(sender, e);
                 if (rdfreeitem.SelectedValue.ToString() == "I")
                 {
                     arr.Add(new cArrayList("@usr_id", hdprop.Value.ToString()));
                     bll.vBindingGridToSp(ref grdfreeitem, "sp_twrk_freeitem_get2", arr);
                     grdfreeitem.Visible = true;
                 }
                 else if (rdfreeitem.SelectedValue.ToString() == "G")
                 {
                     arr.Add(new cArrayList("@usr_id", hdprop.Value.ToString()));
                     bll.vBindingGridToSp(ref grdfreeproduct, "sp_twrk_freeproduct_get2", arr);
                     grdfreeproduct.Visible = true;
                 }
             }

             cbpaymenttype.SelectedValue = rs["rdpayment"].ToString();
             cbpaymenttype.CssClass = "ro";
             cbpaymenttype_SelectedIndexChanged(sender, e);
             //cbuom.SelectedValue = rs["uom"].ToString();
             //cbuom.CssClass = "ro";             

             // Price Budget Calculation 
             cbprice.CssClass = "ro";
             cbprice.SelectedValue = rs["price_calc"].ToString();
             cbpricetype.CssClass = "ro";
             cbpricetype.SelectedValue = rs["pricecalc_typ"].ToString();

             //Scheme Automation
             chdiscount.Checked = bool.Parse(rs["bscheme"].ToString());

             arr.Clear();
             arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
             bll.vBindingGridToSp(ref grdpayment, "sp_tproposal_payment_get", arr);

             //Payment
             cbpaymentterm.SelectedValue = rs["prop_term"].ToString();
             cbpaymentterm.CssClass = "ro";
             arr.Clear();
             arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
             bll.vBindingGridToSp(ref grdpayment, "sp_tproposal_payment_get", arr);
             grdpayment.CssClass = "ro";
             //Payment

             txremark.Text = rs["remark"].ToString();
             txremark.CssClass = "ro";
             txbgremark.Text = rs["bgremark"].ToString();
             txbgremark.CssClass = "ro";

             // Proposal Sign
             if (rs["sbtc_apc"].ToString() != "")
             {
                 cbapcoordinator.SelectedValue = rs["sbtc_apc"].ToString();
             }
             cbapcoordinator.CssClass = "ro";
             if (rs["sbtc_prodmgr"].ToString() != "")
             {
                 cbprodman.SelectedValue = rs["sbtc_prodmgr"].ToString();
             }             
             cbprodman.CssClass = "ro";
             if (rs["sbtc_gm"].ToString() != "")
             {
                 cbgm.SelectedValue = rs["sbtc_gm"].ToString();
             }             
             cbgm.CssClass = "ro";
             if (rs["sbtc_cap"].ToString() != "")
             {
                 cbclaimdepthead.SelectedValue = rs["sbtc_cap"].ToString();
             }             
             cbclaimdepthead.CssClass = "ro";
             if (rs["sbtc_kamgr"].ToString() != "")
             {
                 cbkamgr.SelectedValue = rs["sbtc_kamgr"].ToString();
             }             
             cbkamgr.CssClass = "ro";
             if (rs["sbtc_marketmgr"].ToString() != "")
             {
                 cbmarketmgr.SelectedValue = rs["sbtc_marketmgr"].ToString();
             }             
             cbmarketmgr.CssClass = "ro";
             if (rs["vendor_marketmgr"].ToString() != "")
             {
                 cbmarketmgrpcp.SelectedValue = rs["vendor_marketmgr"].ToString();
             }             
             cbmarketmgrpcp.CssClass = "ro";
             if (rs["vendor_nspm"].ToString() != "")
             {
                 cbnspmpcp.SelectedValue = rs["vendor_nspm"].ToString();
             }             
             cbnspmpcp.CssClass = "ro";
             if (rs["vendor_gmdir"].ToString() != "")
             {
                 cbgmpcp.SelectedValue = rs["vendor_gmdir"].ToString();
             }             
             cbgmpcp.CssClass = "ro";
             if (rs["vendor_findep"].ToString() != "")
             {
                 cbfinpcp.SelectedValue = rs["vendor_findep"].ToString();
             }             
             cbfinpcp.CssClass = "ro";
             if (rs["vendor_marketdir"].ToString() != "")
             {
                 cbmarketingdirpcp.SelectedValue = rs["vendor_marketdir"].ToString();
             }             
             cbmarketingdirpcp.CssClass = "ro";
             // Proposal Sign

             // Budget Group
             ka1.Text = rs["ka1"].ToString(); ka2.Text = rs["ka2"].ToString(); ka3.Text = rs["ka3"].ToString(); ka4.Text = rs["ka4"].ToString(); ka5.Text = rs["ka5"].ToString(); ka6.Text = rs["ka6"].ToString(); ka7.Text = rs["ka7"].ToString(); ka8.Text = rs["ka8"].ToString(); ka9.Text = rs["ka9"].ToString();
             nka1.Text = rs["nka1"].ToString(); nka2.Text = rs["nka2"].ToString(); nka3.Text = rs["nka3"].ToString(); nka4.Text = rs["nka4"].ToString(); nka5.Text = rs["nka5"].ToString(); nka6.Text = rs["nka6"].ToString(); nka7.Text = rs["nka7"].ToString(); nka8.Text = rs["nka8"].ToString(); nka9.Text = rs["nka9"].ToString();
             t1.Text = rs["t1"].ToString(); t2.Text = rs["t2"].ToString(); t3.Text = rs["t3"].ToString(); t4.Text = rs["t4"].ToString(); t5.Text = rs["t5"].ToString(); t6.Text = rs["t6"].ToString(); t7.Text = rs["t7"].ToString(); t8.Text = rs["t8"].ToString(); t9.Text = rs["t9"].ToString();
             // Budget Group

             // Proposal Status 
             cbstatus.SelectedValue = (rs["status_no"].ToString() == "" ? "S0" : rs["status_no"].ToString());
             txstatus.Text = rs["status_remark"].ToString();

         } rs.Close();

         ka1.CssClass = "ro"; ka2.CssClass = "ro"; ka3.CssClass = "ro"; ka4.CssClass = "ro"; ka5.CssClass = "ro"; ka6.CssClass = "ro"; ka7.CssClass = "ro"; ka8.CssClass = "ro"; ka9.CssClass = "ro";
         nka1.CssClass = "ro"; nka2.CssClass = "ro"; nka3.CssClass = "ro"; nka4.CssClass = "ro"; nka5.CssClass = "ro"; nka6.CssClass = "ro"; nka7.CssClass = "ro"; nka8.CssClass = "ro"; nka9.CssClass = "ro";
         t1.CssClass = "ro"; t2.CssClass = "ro"; t3.CssClass = "ro"; t4.CssClass = "ro"; t5.CssClass = "ro"; t6.CssClass = "ro"; t7.CssClass = "ro"; t8.CssClass = "ro"; t9.CssClass = "ro";

         // Get Column For Group Budget
         string propCode = string.Empty;
         System.Data.SqlClient.SqlDataReader rsProp = null;
         List<cArrayList> arrProp = new List<cArrayList>();
         arrProp.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
         arrProp.Add(new cArrayList("@rditem", rditem.SelectedValue.ToString()));
         arrProp.Add(new cArrayList("@promo", cbpromogroup.SelectedValue.ToString()));
         bll.vGetColumnBudget(arrProp, ref rsProp);
         int numRows = 0;
         List<string> lColumn = new List<string>();
         while (rsProp.Read())
         {
             lColumn.Add(rsProp["group_budget"].ToString());
         }
         string[] arrColumn = lColumn.ToArray();
         numRows = arrColumn.Length;
         if (numRows == 1)
         {
             a2.Visible = false; a3.Visible = false; a4.Visible = false; a5.Visible = false; a6.Visible = false; a7.Visible = false; a8.Visible = false; a9.Visible = false;
             cka2.Visible = false; cka3.Visible = false; cka4.Visible = false; cka5.Visible = false; cka6.Visible = false; cka7.Visible = false; cka8.Visible = false; cka9.Visible = false;
             cnka2.Visible = false; cnka3.Visible = false; cnka4.Visible = false; cnka5.Visible = false; cnka6.Visible = false; cnka7.Visible = false; cnka8.Visible = false; cnka9.Visible = false;
             t2.Visible = false; t3.Visible = false; t4.Visible = false; t5.Visible = false; t6.Visible = false; t7.Visible = false; t8.Visible = false; t9.Visible = false;
             a1.Text = arrColumn[0];
         }
         else if (numRows == 2)
         {
             a3.Visible = false; a4.Visible = false; a5.Visible = false; a6.Visible = false; a7.Visible = false; a8.Visible = false; a9.Visible = false;
             cka3.Visible = false; cka4.Visible = false; cka5.Visible = false; cka6.Visible = false; cka7.Visible = false; cka8.Visible = false; cka9.Visible = false;
             cnka3.Visible = false; cnka4.Visible = false; cnka5.Visible = false; cnka6.Visible = false; cnka7.Visible = false; cnka8.Visible = false; cnka9.Visible = false;
             t3.Visible = false; t4.Visible = false; t5.Visible = false; t6.Visible = false; t7.Visible = false; t8.Visible = false; t9.Visible = false;
             a1.Text = arrColumn[0];
             a2.Text = arrColumn[1];
         }
         else if (numRows == 3)
         {
             a4.Visible = false; a5.Visible = false; a6.Visible = false; a7.Visible = false; a8.Visible = false; a9.Visible = false;
             cka4.Visible = false; cka5.Visible = false; cka6.Visible = false; cka7.Visible = false; cka8.Visible = false; cka9.Visible = false;
             cnka4.Visible = false; cnka5.Visible = false; cnka6.Visible = false; cnka7.Visible = false; cnka8.Visible = false; cnka9.Visible = false;
             t4.Visible = false; t5.Visible = false; t6.Visible = false; t7.Visible = false; t8.Visible = false; t9.Visible = false;
             a1.Text = arrColumn[0];
             a2.Text = arrColumn[1];
             a3.Text = arrColumn[2];
         }
         else if (numRows == 4)
         {
             a5.Visible = false; a6.Visible = false; a7.Visible = false; a8.Visible = false; a9.Visible = false;
             cka5.Visible = false; cka6.Visible = false; cka7.Visible = false; cka8.Visible = false; cka9.Visible = false;
             cnka5.Visible = false; cnka6.Visible = false; cnka7.Visible = false; cnka8.Visible = false; cnka9.Visible = false;
             t5.Visible = false; t6.Visible = false; t7.Visible = false; t8.Visible = false; t9.Visible = false;
             a1.Text = arrColumn[0];
             a2.Text = arrColumn[1];
             a3.Text = arrColumn[2];
             a4.Text = arrColumn[3];
         }
         else if (numRows == 5)
         {
             a6.Visible = false; a7.Visible = false; a8.Visible = false; a9.Visible = false;
             cka6.Visible = false; cka7.Visible = false; cka8.Visible = false; cka9.Visible = false;
             cnka6.Visible = false; cnka7.Visible = false; cnka8.Visible = false; cnka9.Visible = false;
             t6.Visible = false; t7.Visible = false; t8.Visible = false; t9.Visible = false;
             a1.Text = arrColumn[0];
             a2.Text = arrColumn[1];
             a3.Text = arrColumn[2];
             a4.Text = arrColumn[3];
             a5.Text = arrColumn[4];
         }
         else if (numRows == 6)
         {
             a7.Visible = false; a8.Visible = false; a9.Visible = false;
             cka7.Visible = false; cka8.Visible = false; cka9.Visible = false;
             cnka7.Visible = false; cnka8.Visible = false; cnka9.Visible = false;
             t7.Visible = false; t8.Visible = false; t9.Visible = false;
             a1.Text = arrColumn[0];
             a2.Text = arrColumn[1];
             a3.Text = arrColumn[2];
             a4.Text = arrColumn[3];
             a5.Text = arrColumn[4];
             a6.Text = arrColumn[5];
         }
         else if (numRows == 7)
         {
             a8.Visible = false; a9.Visible = false;
             cka8.Visible = false; cka9.Visible = false;
             cnka8.Visible = false; cnka9.Visible = false;
             t8.Visible = false; t9.Visible = false;
             a1.Text = arrColumn[0];
             a2.Text = arrColumn[1];
             a3.Text = arrColumn[2];
             a4.Text = arrColumn[3];
             a5.Text = arrColumn[4];
             a6.Text = arrColumn[5];
             a7.Text = arrColumn[6];
         }
         tblGroupBudget.Visible = true;
         // End Get Colum For Group Budget   

         // Document Proposal 
         grdcate.Visible = true;
         btnuploaddoc.Visible = true;
         grddoc.Visible = true;
         
         arr.Clear();
         arr.Add(new cArrayList("@promo_cd", cbpromotype.SelectedValue.ToString()));
         arr.Add(new cArrayList("@dic", null));
         bll.vBindingGridToSp(ref grdcate, "sp_tpromotion_doc_get", arr);

         arr.Clear();
         arr.Add(new cArrayList("@prop_no", txpropno.Text));
         bll.vBindingGridToSp(ref grddoc, "sp_tproposal_document_get", arr);

         // Proposal status
         viewStatus.Visible = true;

     }

     
     protected void cbpaymenttype_SelectedIndexChanged(object sender, EventArgs e)
     {
         if (cbpaymenttype.SelectedValue.ToString() == "")
         {
             txfreegood.Text = "";
             grdcost.Visible = false;
             grdcostprodTB.Visible = false;
             grdcostitemTB.Visible = false;
             txtotalbudget.Visible = false;
             lbltotalbudget.Visible = false;
         }
         else
         {
             if (cbpaymenttype.SelectedValue.ToString() == "FG")
             {
                 lblfreeproducttitle.Text = "Free Product";
                 lblfreeproduct.Text = "QTY";
             }
             else 
             {
                 lblfreeproducttitle.Text = "Free Cost";
                 lblfreeproduct.Text = "SAR";
             }

             if ((cbbgitem.SelectedValue.ToString() != "C") && (cbbgitem.SelectedValue.ToString() != "E") && (cbbgitem.SelectedValue.ToString() != "B"))
             {
                 grdcost.Visible = true;
                 grdcostprodTB.Visible = true;
                 grdcostitemTB.Visible = true;
                 txtotalbudget.Visible = false;
                 lbltotalbudget.Visible = false;
                 System.Data.SqlClient.SqlDataReader rs = null;
                 List<cArrayList> arr = new List<cArrayList>();
                 arr.Clear();
                 if (hdprop.Value.ToString() == "")
                 {
                     arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
                     arr.Add(new cArrayList("@bg", cbbgitem.SelectedValue.ToString()));
                 }
                 else
                 {
                     arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
                     arr.Add(new cArrayList("@bg", cbbgitem.SelectedValue.ToString()));
                 }
                 arr.Add(new cArrayList("@cust", rdcust.SelectedValue.ToString()));
                 arr.Add(new cArrayList("@product", rditem.SelectedValue.ToString()));
                 arr.Add(new cArrayList("@lmt", txbudget.Text.ToString()));
                 arr.Add(new cArrayList("@type", cbpaymenttype.SelectedValue.ToString()));
                 arr.Add(new cArrayList("@promo", cbpromogroup.SelectedValue.ToString()));
                 if (chbudget.Checked)
                 {
                     arr.Add(new cArrayList("@cbudget", chbudget.Checked.ToString()));
                 }
                 else
                 {
                     arr.Add(new cArrayList("@cbudget", null));
                 }
                 if (chfreegood.Checked)
                 {
                     arr.Add(new cArrayList("@cfree", "1"));
                     arr.Add(new cArrayList("@cfreeitem", rdfreeitem.SelectedValue.ToString()));
                 }
                 else
                 {
                     arr.Add(new cArrayList("@cfree", null));
                 }
                 
                 //if ((cbpromogroup.SelectedValue.ToString() != "TB"))
                 //{
                 //    if ((cbpromogroup.SelectedValue.ToString() != "RB"))
                 //    {
                 //        if (cbbgitem.SelectedValue.ToString() == "A")
                 //        {
                             double free = 0, budget = 0, total = 0;
                             grdcost.Visible = false;
                             grdcostprodTB.Visible = false;
                             grdcostitemTB.Visible = false;
                             txtotalbudget.Visible = true;
                             lbltotalbudget.Visible = true;
                             if (chbudget.Checked)
                             {
                                 bll.vGetEstimatedCost(arr, ref rs);
                                 while (rs.Read())
                                 {
                                     free += double.Parse((rs["freegood"].ToString() == "") ? "0" : rs["freegood"].ToString());
                                     budget += double.Parse((rs["target"].ToString() == "") ? "0" : rs["target"].ToString());
                                     if (cbpaymenttype.SelectedValue.ToString() == "CH")
                                     {
                                         total += double.Parse((rs["rbp"].ToString() == "" ? "0" : rs["rbp"].ToString()));
                                     }
                                     else if (cbpaymenttype.SelectedValue.ToString() == "FG")
                                     {
                                         total += double.Parse((rs["dbp"].ToString() == "" ? "0" : rs["dbp"].ToString()));
                                     }
                                     else
                                     {
                                         total += double.Parse((rs["rbp"].ToString() == "" ? "0" : rs["rbp"].ToString()));
                                     }                                     
                                 }
                                 txfreegood.Text = free.ToString();
                                 txbudget.Text = budget.ToString();
                                 txtotalbudget.Text = total.ToString();
                             }
                             else
                             {
                                 bll.vGetEstimatedCost(arr, ref rs);
                                 while (rs.Read())
                                 {
                                     free += double.Parse((rs["freegood"].ToString() == "") ? "0" : rs["freegood"].ToString() );
                                     if (cbpaymenttype.SelectedValue.ToString() == "CH")
                                     {
                                         total += double.Parse((rs["rbp"].ToString() == "" ? "0" : rs["rbp"].ToString()));
                                     }
                                     else if (cbpaymenttype.SelectedValue.ToString() == "FG")
                                     {
                                         total += double.Parse( (rs["dbp"].ToString()==""?"0":rs["dbp"].ToString()) );
                                     }
                                     else
                                     {
                                         total += double.Parse((rs["dbp"].ToString() == "" ? "0" : rs["dbp"].ToString()));
                                     }
                                     
                                     break;
                                 }
                                 txfreegood.Text = free.ToString();
                                 txtotalbudget.Text = total.ToString();
                             }
                             
                 //        }
                 //        else
                 //        {
                 //            bll.vGetEstimatedCost(arr, ref rs);
                 //            while (rs.Read())
                 //            {
                 //                txfreegood.Text = rs["freegood"].ToString();
                 //            }
                 //            bll.vBindingGridToSp(ref grdcost, "sp_tproposal_cost", arr);
                 //        }
                         
                 //    }
                 //}
                 //else 
                 //{
                 //    if ((cbpromogroup.SelectedValue.ToString() == "TB"))
                 //    {
                 //        if (rditem.SelectedValue.ToString() == "I")
                 //        {
                 //            bll.vBindingGridToSp(ref grdcostitemTB, "sp_tproposal_cost", arr);
                 //        }
                 //        else
                 //        {
                 //            bll.vBindingGridToSp(ref grdcostprodTB, "sp_tproposal_cost", arr);
                 //        }
                 //    }
                 //}
             }
             else if (cbbgitem.SelectedValue.ToString() == "B")
             {
                 System.Data.SqlClient.SqlDataReader rs = null;
                 List<cArrayList> arr = new List<cArrayList>();
                 arr.Clear();
                 if (hdprop.Value.ToString() == "")
                 {
                     arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));                     
                 }
                 else
                 {
                     arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
                 }
                 arr.Add(new cArrayList("@product", rditem.SelectedValue.ToString()));
                 arr.Add(new cArrayList("@bg", cbbgitem.SelectedValue.ToString()));
                 //arr.Add(new cArrayList("@cust", rdcust.SelectedValue.ToString()));
                 bll.vGetBgCustom(arr, ref rs);
                 double free = 0, budget = 0, total = 0;
                 while (rs.Read())
                 {
                     free += double.Parse((rs["freegood"].ToString() == "") ? "0" : rs["freegood"].ToString() );
                     budget += double.Parse((rs["y"].ToString() == "") ? "0" : rs["y"].ToString() );
                     total += double.Parse((rs["total"].ToString() == "") ? "0" : rs["total"].ToString() );
                 }
                 grdcost.Visible = false;
                 grdcostprodTB.Visible = false;
                 grdcostitemTB.Visible = false;
                 txtotalbudget.Visible = true;
                 lbltotalbudget.Visible = true;
                 txfreegood.Text = free.ToString();
                 txbudget.Text = budget.ToString();
                 txtotalbudget.Text = total.ToString();
             }
             else if (cbbgitem.SelectedValue.ToString() == "C")
             {
                 System.Data.SqlClient.SqlDataReader rs = null;
                 List<cArrayList> arr = new List<cArrayList>();
                 arr.Clear();
                 if (hdprop.Value.ToString() == "")
                 {
                     arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
                 }
                 else
                 {
                     arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
                 }
                 arr.Add(new cArrayList("@product", rditem.SelectedValue.ToString()));
                 bll.vGetBgCustom(arr, ref rs);
                 double free = 0, budget = 0, total = 0;
                 int i = 0;
                 while (rs.Read())
                 {
                     i++;
                     free += double.Parse(rs["x"].ToString());
                     budget += double.Parse(rs["y"].ToString());
                     total += double.Parse(rs["x"].ToString()) * double.Parse(rs["y"].ToString()); 
                 }
                 grdcost.Visible = false;
                 grdcostprodTB.Visible = false;
                 grdcostitemTB.Visible = false;
                 txtotalbudget.Visible = true;
                 lbltotalbudget.Visible = true;
                 if (i > 1)
                 {
                     txfreegood.Text = free.ToString();
                     txbudget.Text = budget.ToString();
                     txtotalbudget.Text = total.ToString();
                 }
                 else
                 {
                     txfreegood.Text = free.ToString();
                     total = double.Parse(txbudget.Text) * double.Parse(txfreegood.Text);                     
                     txtotalbudget.Text = total.ToString();
                     arr.Clear();
                     if (hdprop.Value.ToString() == "")
                     {
                         arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
                     }
                     else
                     {
                         arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
                     }
                     arr.Add(new cArrayList("@product", rditem.SelectedValue.ToString()));
                     arr.Add(new cArrayList("@budget", txbudget.Text));
                     bll.vUpdateBgProposal(arr);
                     arr.Clear();
                 }

             }
             else if (cbbgitem.SelectedValue.ToString() == "E")
             {
                 System.Data.SqlClient.SqlDataReader rs = null;
                 List<cArrayList> arr = new List<cArrayList>();
                 arr.Clear();
                 if (hdprop.Value.ToString() == "")
                 {
                     arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
                 }
                 else
                 {
                     arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
                 }
                 arr.Add(new cArrayList("@product", rditem.SelectedValue.ToString()));
                 arr.Add(new cArrayList("@bg", cbbgitem.SelectedValue.ToString()));
                 bll.vGetBgCustom(arr, ref rs);
                 double free = 0, budget = 0, total = 0;
                 while (rs.Read())
                 {
                     //free += double.Parse(rs["freegood"].ToString());
                     budget += double.Parse(rs["x"].ToString());
                     total += double.Parse(rs["x"].ToString());
                 }
                 grdcost.Visible = false;
                 grdcostprodTB.Visible = false;
                 grdcostitemTB.Visible = false;
                 txtotalbudget.Visible = true;
                 lbltotalbudget.Visible = true;
                 txfreegood.Text = free.ToString();
                 txbudget.Text = budget.ToString();
                 txtotalbudget.Text = total.ToString();
             }
             else
             {
                 grdcost.Visible = false;
                 grdcostprodTB.Visible = false;
                 grdcostitemTB.Visible = false;
                 txtotalbudget.Visible = true;
                 lbltotalbudget.Visible = true;
             }             
         }
         //cbpaymenttype.CssClass = "ro";
         chbudget.CssClass = "ro";
         chfreegood.CssClass = "ro";
     }

     protected void btedit_Click(object sender, EventArgs e)
     {

         Session["edit"] = "true";
         btsave.Visible = false;
         btupdate.Visible = true;
         btapprove.Visible = false;
         btdelete.Visible = false;
         btcancel.Visible = false;
         btedit.Visible = false;
         btprint.Visible = false;
         btprint2.Visible = false;
         //upl.Visible = false;

         txpropno.CssClass = "ro";
         txpropno.CssClass = "divnormal";
         cbsignframe.CssClass = "divnormal";

         cbvendor.CssClass = "divnormal";
         //cbappvendor.CssClass = "divnormal";

         dtstart.CssClass = "divnormal";
         dtdelivery.CssClass = "divnormal";
         dtend.CssClass = "divnormal";
         dtprop.CssClass = "divnormal";
         dtclaim.CssClass = "divnormal";
         dtadjuststart.CssClass = "divnormal";
         dtadjustend.CssClass = "divnormal";
                     
         //if (dtadjuststart.Text == "") { dtadjuststart.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Now); }
         //if (dtadjustend.Text == "") { dtadjustend.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Now); }

         rditem.CssClass = "divnormal";

         txpropvendor.CssClass = "divnormal";
         txrefno.CssClass = "divnormal";

         cbpromokind.CssClass = "divnormal";
         cbpromogroup.CssClass = "divnormal";
         cbpromotype.CssClass = "divnormal";
         cbmarketingcost.CssClass = "divnormal";
         chclaim.CssClass = "divnormal";

         tblSalespoint.Visible = true;
         grdslspoint.CssClass = "divnormal";

         rdcust.CssClass = "divnormal";
         tblCustomer.Visible = true;
         grdcust.CssClass = "divgrid";
         grdcusgrcd.CssClass = "divgrid";
         grdcusttype.CssClass = "divgrid";

         tblItem.Visible = true;
         //grditem.CssClass = "divgrid";
         //grdgroup.CssClass = "divgrid";
         if (rditem.SelectedValue.ToString() == "I")
         {
             grdviewitem.CssClass = "divnormal";
         }
         else
         {
             grdviewgroup.CssClass = "divnormal";
         }
         
         
         txbgremark.CssClass = "divnormal";

         // Mechanism

         if (cbpromogroup.SelectedValue.ToString() == "RB")
         {
             tblRebate.Visible = true;
             grdrebate.CssClass = "divgrid";
             grddisplayrent.CssClass = "divhid";
             grdfee.CssClass = "divhid";
             grdotherpromo.CssClass = "divhid";
             grdcar.CssClass = "divhid";
             grdcook.CssClass = "divhid";
             grdsignboard.CssClass = "divhid";
             grdchep.CssClass = "divhid";
         }
         else if (cbpromogroup.SelectedValue.ToString() == "DR")
         {
             tblRent.Visible = true;
             grdrebate.CssClass = "divhid";
             grddisplayrent.CssClass = "divgrid";
             grdfee.CssClass = "divhid";
             grdotherpromo.CssClass = "divhid";
             grdcar.CssClass = "divhid";
             grdcook.CssClass = "divhid";
             grdsignboard.CssClass = "divhid";
             grdchep.CssClass = "divhid";
         }
         else if ((cbpromogroup.SelectedValue.ToString() == "OF") || (cbpromogroup.SelectedValue.ToString() == "PB") || (cbpromogroup.SelectedValue.ToString() == "LF"))
         {
             tblFee.Visible = true;
             grdrebate.CssClass = "divhid";
             grddisplayrent.CssClass = "divhid";
             grdfee.CssClass = "divgrid";
             grdotherpromo.CssClass = "divhid";
             grdcar.CssClass = "divhid";
             grdcook.CssClass = "divhid";
             grdsignboard.CssClass = "divhid";
             grdchep.CssClass = "divhid";
         }
         else if ((cbpromogroup.SelectedValue.ToString() == "GS") || (cbpromogroup.SelectedValue.ToString() == "IP") || (cbpromogroup.SelectedValue.ToString() == "LT") || (cbpromogroup.SelectedValue.ToString() == "NE") || (cbpromogroup.SelectedValue.ToString() == "MF") || (cbpromogroup.SelectedValue.ToString() == "SD") || (cbpromogroup.SelectedValue.ToString() == "SI") || (cbpromogroup.SelectedValue.ToString() == "TB"))
         {
             tblOther.Visible = true;
             grdrebate.CssClass = "divhid";
             grddisplayrent.CssClass = "divhid";
             grdfee.CssClass = "divhid";
             grdotherpromo.CssClass = "divgrid";
             grdcar.CssClass = "divhid";
             grdcook.CssClass = "divhid";
             grdsignboard.CssClass = "divhid";
             grdchep.CssClass = "divhid";
         }
         else if (cbpromogroup.SelectedValue.ToString() == "CB")
         {
             tblCar.Visible = true;
             grdrebate.CssClass = "divhid";
             grddisplayrent.CssClass = "divhid";
             grdfee.CssClass = "divhid";
             grdotherpromo.CssClass = "divhid";
             grdcar.CssClass = "divgrid";
             grdcook.CssClass = "divhid";
             grdsignboard.CssClass = "divhid";
             grdchep.CssClass = "divhid";
         }
         else if ((cbpromogroup.SelectedValue.ToString() == "CD") || (cbpromogroup.SelectedValue.ToString() == "SP") || (cbpromogroup.SelectedValue.ToString() == "RP"))
         {
             tblCook.Visible = true;
             grdrebate.CssClass = "divhid";
             grddisplayrent.CssClass = "divhid";
             grdfee.CssClass = "divhid";
             grdotherpromo.CssClass = "divhid";
             grdcar.CssClass = "divhid";
             grdcook.CssClass = "divgrid";
             grdsignboard.CssClass = "divhid";
             grdchep.CssClass = "divhid";
         }
         else if (cbpromogroup.SelectedValue.ToString() == "SB")
         {
             tblSignBoard.Visible = true;
             grdrebate.CssClass = "divhid";
             grddisplayrent.CssClass = "divhid";
             grdfee.CssClass = "divhid";
             grdotherpromo.CssClass = "divhid";
             grdcar.CssClass = "divhid";
             grdcook.CssClass = "divhid";
             grdsignboard.CssClass = "divgrid";
             grdchep.CssClass = "divhid";
         }
         else if (cbpromogroup.SelectedValue.ToString() == "CP")
         {
             tblChep.Visible = true;
             grdrebate.CssClass = "divhid";
             grddisplayrent.CssClass = "divhid";
             grdfee.CssClass = "divhid";
             grdotherpromo.CssClass = "divhid";
             grdcar.CssClass = "divhid";
             grdcook.CssClass = "divhid";
             grdsignboard.CssClass = "divhid";
             grdchep.CssClass = "divgrid";
         }
         grdrebate.CssClass = "divnormal";
         grddisplayrent.CssClass = "divnormal";
         grdfee.CssClass = "divnormal";
         grdotherpromo.CssClass = "divnormal";
         grdcar.CssClass = "divnormal";
         grdcook.CssClass = "divnormal";
         grdsignboard.CssClass = "divnormal";
         grdchep.CssClass = "divnormal";
         // Mechanism

         chagreement.CssClass = "divnormal";
         dtagree.CssClass = "divnormal";
         txagreementno.CssClass = "divnormal";
         chagreement_CheckedChanged(sender, e);

         chfreegood.CssClass = "divnormal";
         chbudget.CssClass = "divnormal";
         txbudget.CssClass = "divnormal";
         txfreegood.CssClass = "divnormal";
         txaddbudget.CssClass = "divnormal";
         cbpaymenttype.CssClass = "divnormal";

         txbudgetmtd.CssClass = "divnormal";
         txbudgetytd.CssClass = "divnormal";

         ka1.CssClass = "divnormal"; ka2.CssClass = "divnormal"; ka3.CssClass = "divnormal"; ka4.CssClass = "divnormal"; ka5.CssClass = "divnormal"; ka6.CssClass = "divnormal"; ka7.CssClass = "divnormal"; ka8.CssClass = "divnormal"; ka9.CssClass = "divnormal";
         nka1.CssClass = "divnormal"; nka2.CssClass = "divnormal"; nka3.CssClass = "divnormal"; nka4.CssClass = "divnormal"; nka5.CssClass = "divnormal"; nka6.CssClass = "divnormal"; nka7.CssClass = "divnormal"; nka8.CssClass = "divnormal"; nka9.CssClass = "divnormal";
         t1.CssClass = "divnormal"; t2.CssClass = "divnormal"; t3.CssClass = "divnormal"; t4.CssClass = "divnormal"; t5.CssClass = "divnormal"; t6.CssClass = "divnormal"; t7.CssClass = "divnormal"; t8.CssClass = "divnormal"; t9.CssClass = "divnormal";

         cbpaymentterm.CssClass = "divnormal";
         grdpayment.CssClass = "divnormal";

         txremark.CssClass = "divnormal";

         // Proposal Sign
         cbapcoordinator.CssClass = "divnormal";
         cbprodman.CssClass = "divnormal";
         cbgm.CssClass = "divnormal";
         cbclaimdepthead.CssClass = "divnormal";
         cbkamgr.CssClass = "divnormal";
         cbmarketmgr.CssClass = "divnormal";
         cbmarketmgrpcp.CssClass = "divnormal";
         cbnspmpcp.CssClass = "divnormal";
         cbgmpcp.CssClass = "divnormal";
         cbfinpcp.CssClass = "divnormal";
         cbmarketingdirpcp.CssClass = "divnormal";
         // Proposal Sign
         // Proposal Status 
         viewStatus.Visible = true;
     }

     protected void btupdate_Click(object sender, EventArgs e)
     {

         int sumDisc = 0;
         System.Data.SqlClient.SqlDataReader rs = null;
         List<cArrayList> arr = new List<cArrayList>();
         arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
         bll.vCheckDiscount(arr, ref rs);
         while (rs.Read())
         {
             sumDisc = Convert.ToInt16(rs["sumDisc"].ToString());
         }
         arr.Clear();

         if (txpropno.Text.ToString() != hdprop.Value.ToString())
         {
             if (sumDisc > 0)
             {
                 ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Can not Update Proposal No','Proposal No','warning');", true);
                 return;
             }
         }


         double dBudget;
         if (!double.TryParse(txbudget.Text, out dBudget))
         {
             ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Budget Limit can not empty or non numeric','Budget Limit','warning');", true);
             return;
         }        

         if (cbmarketingcost.SelectedValue.ToString().ToLower() == "principal")
         {
             txprincipalcost.Text = "100";
             txcostsbtc.Text = "0";
         }
         else if (cbmarketingcost.SelectedValue.ToString().ToLower() == "sbtc")
         {
             txcostsbtc.Text = "100";
             txprincipalcost.Text = "0";
         }
         else if (cbmarketingcost.SelectedValue.ToString().ToLower() == "percentage")
         {
             double dSbtc; double dPrincipal; double total;
             if (!double.TryParse(txcostsbtc.Text, out dSbtc))
             {
                 ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Percentage SBTC must numeric','Percentage','warning');", true);
                 return;
             }
             if (!double.TryParse(txprincipalcost.Text, out dPrincipal))
             {
                 ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Percentage Principal must numeric','Percentage','warning');", true);
                 return;
             }
             total = dSbtc + dPrincipal;
             if (total != 100)
             {
                 ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Total Percentage must be 100%','Percentage','warning');", true);
                 return;
             }
         }
         else if (cbmarketingcost.SelectedValue.ToString().ToLower() == "branch")
         {
             txcostsbtc.Text = "0";
             txprincipalcost.Text = "0";
         }

         arr.Add(new cArrayList("@prop_dt", DateTime.ParseExact(dtprop.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
         arr.Add(new cArrayList("@budget", Convert.ToDouble(txbudget.Text)));
         arr.Add(new cArrayList("@prop_no_vendor", txpropvendor.Text));
         arr.Add(new cArrayList("@start_dt", DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
         arr.Add(new cArrayList("@end_dt", DateTime.ParseExact(dtend.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
         arr.Add(new cArrayList("@delivery_dt", DateTime.ParseExact(dtdelivery.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
         arr.Add(new cArrayList("@claim_dt", DateTime.ParseExact(dtclaim.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
         arr.Add(new cArrayList("@rditem", rditem.SelectedValue.ToString()));
         arr.Add(new cArrayList("@rdcust", rdcust.SelectedValue.ToString()));
         arr.Add(new cArrayList("@vendor_cd", cbvendor.SelectedValue.ToString()));
         arr.Add(new cArrayList("@prop_term", cbpaymentterm.SelectedValue.ToString()));
         arr.Add(new cArrayList("@agreement_no", txagreementno.Text));
         arr.Add(new cArrayList("@agreement_dt", DateTime.ParseExact(dtagree.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
         arr.Add(new cArrayList("@prop_payment", cbpaymentterm.SelectedValue.ToString()));
         //arr.Add(new cArrayList("@uom", cbuom.SelectedValue.ToString()));

         arr.Add(new cArrayList("@promogrp", cbpromogroup.SelectedValue.ToString()));
         arr.Add(new cArrayList("@promotyp", cbpromotype.SelectedValue.ToString()));

         //Dihilangkan @25-05-2016 By Nico
         //arr.Add(new cArrayList("@sbtc_app", cbsbtcapp.SelectedValue.ToString()));

         arr.Add(new cArrayList("@vendor_app", cbappvendor.SelectedValue.ToString()));
         arr.Add(new cArrayList("@rdcost", cbmarketingcost.SelectedValue.ToString()));
         arr.Add(new cArrayList("@sbtccost", txcostsbtc.Text));
         arr.Add(new cArrayList("@principalcost", txprincipalcost.Text));
         arr.Add(new cArrayList("@ref_no", txrefno.Text));
         arr.Add(new cArrayList("@remark", txremark.Text));
         //arr.Add(new cArrayList("@rdbudget",cbbudgettype.SelectedValue.ToString()));
         arr.Add(new cArrayList("@rdpayment", cbpaymenttype.SelectedValue.ToString()));
         arr.Add(new cArrayList("@prop_no", txpropno.Text.ToString()));
         arr.Add(new cArrayList("@prop_no_old", hdprop.Value.ToString()));
         string[] propcode = txpropno.Text.ToString().Split('/');
         arr.Add(new cArrayList("@prop_code", propcode[2]));

         arr.Add(new cArrayList("@bgremark", txbgremark.Text.ToString()));
         arr.Add(new cArrayList("@budgetadd", txaddbudget.Text.ToString()));
         arr.Add(new cArrayList("@mtd", txbudgetmtd.Text.ToString()));
         arr.Add(new cArrayList("@ytd", txbudgetytd.Text.ToString()));
         arr.Add(new cArrayList("@claim", chclaim.Checked.ToString()));

         arr.Add(new cArrayList("@sign_frame", cbsignframe.SelectedValue.ToString()));

         arr.Add(new cArrayList("@budget_calc", chbudget.Checked.ToString()));

         // Freegood Calculation
         arr.Add(new cArrayList("@budget_free", chfreegood.Checked.ToString()));
         arr.Add(new cArrayList("@rditemfree", rdfreeitem.SelectedValue.ToString()));
         // Automatic Scheme
         arr.Add(new cArrayList("@bscheme", chdiscount.Checked.ToString()));

         //if (cbpromogroup.SelectedValue.ToString() == "RB")
         //{
         //    arr.Add(new cArrayList("@budgetlimit", txfreegood.Text));
         //}
         //else
         //{
             //if (cbpromogroup.SelectedValue.ToString() == "TB")
             //{
             //    if (rditem.SelectedValue.ToString() == "I")
             //    {
             //        if (grdcostitemTB.Rows.Count != 0)
             //        {
             //            Label lbdbp = (Label)grdcostitemTB.Rows[0].FindControl("dbp");
             //            arr.Add(new cArrayList("@budgetlimit", lbdbp.Text));
             //        }
             //        else
             //        {
             //            arr.Add(new cArrayList("@budgetlimit", "0"));
             //        }                     
             //    }
             //    else
             //    {
             //        if (grdcostprodTB.Rows.Count != 0)
             //        {
             //            Label lbdbp = (Label)grdcostprodTB.Rows[0].FindControl("dbp");
             //            arr.Add(new cArrayList("@budgetlimit", lbdbp.Text));
             //        }
             //        else
             //        {
             //            arr.Add(new cArrayList("@budgetlimit", "0"));
             //        }
                     
             //    }
             //}
             //else
             //{
                 //if ((cbbgitem.SelectedValue.ToString() == "C") || (cbbgitem.SelectedValue.ToString() == "E") || (cbbgitem.SelectedValue.ToString() == "B"))
                 //{
                     arr.Add(new cArrayList("@budgetlimit", txtotalbudget.Text));
                 //}
                 //else
                 //{
                 //    if (cbbgitem.SelectedValue.ToString() == "A")
                 //    {
                 //        arr.Add(new cArrayList("@budgetlimit", txtotalbudget.Text));
                 //    }
                 //    else
                 //    {
                 //        if (cbpaymenttype.SelectedValue.ToString() == "FG")
                 //        {
                 //            if (grdcost.Rows.Count != 0)
                 //            {
                 //                Label lbdbp = (Label)grdcost.Rows[0].FindControl("dbp");
                 //                arr.Add(new cArrayList("@budgetlimit", lbdbp.Text));
                 //            }
                 //            else
                 //            {
                 //                arr.Add(new cArrayList("@budgetlimit", "0"));
                 //            }
                 //        }
                 //        else
                 //        {
                 //            if (grdcost.Rows.Count != 0)
                 //            {
                 //                Label lbrbp = (Label)grdcost.Rows[0].FindControl("rbp");
                 //                arr.Add(new cArrayList("@budgetlimit", lbrbp.Text));
                 //            }
                 //            else
                 //            {
                 //                arr.Add(new cArrayList("@budgetlimit", "0"));
                 //            }
                 //        }
                 //    }
                 //}
             //}
         //}

         //string sPropNo = string.Empty;

         bll.vUpdatetMstProposal(arr);
         
         // Add Proposal Sign @130162016 By Nico
         arr.Clear();
         arr.Add(new cArrayList("@prop_no_old", hdprop.Value.ToString()));
         arr.Add(new cArrayList("@prop_no", txpropno.Text.ToString()));
         //if (cbapcoordinator.SelectedValue.ToString() != "NULL"){
         arr.Add(new cArrayList("@sbtc_apc", cbapcoordinator.SelectedValue.ToString()));
         //}
         //if (cbprodman.SelectedValue.ToString() != "NULL")
         //{
         arr.Add(new cArrayList("@sbtc_prodmgr", cbprodman.SelectedValue.ToString()));
         //}
         //if (cbgm.SelectedValue.ToString() != "NULL")
         //{
         arr.Add(new cArrayList("@sbtc_gm", cbgm.SelectedValue.ToString()));
         //}
         //if (cbclaimdepthead.SelectedValue.ToString() != "NULL")
         //{
         arr.Add(new cArrayList("@sbtc_cap", cbclaimdepthead.SelectedValue.ToString()));
         //}
         //if (cbkamgr.SelectedValue.ToString() != "NULL")
         //{
         arr.Add(new cArrayList("@sbtc_kamgr", cbkamgr.SelectedValue.ToString()));
         arr.Add(new cArrayList("@sbtc_marketmgr", cbmarketmgr.SelectedValue.ToString()));
         //}
         //if (cbmarketmgrpcp.SelectedValue.ToString() != "NULL")
         //{
         arr.Add(new cArrayList("@vendor_marketmgr", cbmarketmgrpcp.SelectedValue.ToString()));
         //}else{
         //    arr.Add(new cArrayList("@vendor_marketmgr", null));
         //}
         //if (cbnspmpcp.SelectedValue.ToString() != "NULL")
         //{
         arr.Add(new cArrayList("@vendor_nspm", cbnspmpcp.SelectedValue.ToString()));
         //}else{
         //    arr.Add(new cArrayList("@vendor_nspm", null));
         //}
         //if (cbfinpcp.SelectedValue.ToString() != "NULL")
         //{
         arr.Add(new cArrayList("@vendor_findep", cbfinpcp.SelectedValue.ToString()));
         //}else{
         //    arr.Add(new cArrayList("@vendor_findep", null));
         //}
         //if (cbmarketingdirpcp.SelectedValue.ToString() != "NULL")
         //{
         arr.Add(new cArrayList("@vendor_marketdir", cbmarketingdirpcp.SelectedValue.ToString()));
         //}else{
         //    arr.Add(new cArrayList("@vendor_marketdir", null));
         //}         
         arr.Add(new cArrayList("@vendor_gmdir", cbgmpcp.SelectedValue.ToString()));

         bll.vUpdateProposalSign(arr); //UPDATESQL
         
         // Proposal Sign

         // Proposal Group Budget
         arr.Clear();
         arr.Add(new cArrayList("@prop_no_old", hdprop.Value.ToString()));
         arr.Add(new cArrayList("@prop_no", txpropno.Text.ToString()));
         arr.Add(new cArrayList("@ka1", ka1.Text.ToString()));
         arr.Add(new cArrayList("@ka2", ka2.Text.ToString()));
         arr.Add(new cArrayList("@ka3", ka3.Text.ToString()));
         arr.Add(new cArrayList("@ka4", ka4.Text.ToString()));
         arr.Add(new cArrayList("@ka5", ka5.Text.ToString()));
         arr.Add(new cArrayList("@ka6", ka6.Text.ToString()));
         arr.Add(new cArrayList("@ka7", ka7.Text.ToString()));
         arr.Add(new cArrayList("@ka8", ka8.Text.ToString()));
         arr.Add(new cArrayList("@ka9", ka9.Text.ToString()));
         arr.Add(new cArrayList("@nka1", nka1.Text.ToString()));
         arr.Add(new cArrayList("@nka2", nka2.Text.ToString()));
         arr.Add(new cArrayList("@nka3", nka3.Text.ToString()));
         arr.Add(new cArrayList("@nka4", nka4.Text.ToString()));
         arr.Add(new cArrayList("@nka5", nka5.Text.ToString()));
         arr.Add(new cArrayList("@nka6", nka6.Text.ToString()));
         arr.Add(new cArrayList("@nka7", nka7.Text.ToString()));
         arr.Add(new cArrayList("@nka8", nka8.Text.ToString()));
         arr.Add(new cArrayList("@nka9", nka9.Text.ToString()));
         arr.Add(new cArrayList("@t1", ka1.Text.ToString()));
         arr.Add(new cArrayList("@t2", ka2.Text.ToString()));
         arr.Add(new cArrayList("@t3", ka3.Text.ToString()));
         arr.Add(new cArrayList("@t4", ka4.Text.ToString()));
         arr.Add(new cArrayList("@t5", ka5.Text.ToString()));
         arr.Add(new cArrayList("@t6", ka6.Text.ToString()));
         arr.Add(new cArrayList("@t7", ka7.Text.ToString()));
         arr.Add(new cArrayList("@t8", ka8.Text.ToString()));
         arr.Add(new cArrayList("@t9", ka9.Text.ToString()));

         bll.vUpdateBudgetProduct(arr); 
         
         arr.Clear();

         ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Proposal has been Update','" + txpropno.Text.ToString() + "','success');", true);
         btupdate.Visible = false;
         btnew.Visible = true;
         btprint.Visible = true;
         btprint2.Visible = true;

         if (txpropno.Text.ToString() != hdprop.Value.ToString())
         {
             arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
             arr.Add(new cArrayList("@proposal_code", txpropno.Text.ToString()));
             bll.vUpdatetMstProposalNo(arr);
             arr.Clear();
         }

         // Insert Adjustment Date
         arr.Add(new cArrayList("@prop_no", txpropno.Text.ToString()));
         if (dtadjuststart.Text.ToString() != "") { arr.Add(new cArrayList("@dtfrom", DateTime.ParseExact(dtadjuststart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))); }
         if (dtadjustend.Text.ToString() != "") { arr.Add(new cArrayList("@dtto", DateTime.ParseExact(dtadjustend.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))); }         
         bll.vUpdateAdjustmentProposal(arr);
         arr.Clear();

         // Insert Proposal status 
         arr.Clear();
         arr.Add(new cArrayList("@prop_no", txpropno.Text.ToString()));
         arr.Add(new cArrayList("@status_no", cbstatus.SelectedValue.ToString()));
         arr.Add(new cArrayList("@remarks", txstatus.Text.ToString()));
         bll.vUpdateStatusProposal(arr);

         Session["edit"] = "";
         Session["cust"] = "";
         Session["addproduct"] = "";

         return;
     }

     protected void cbregion_SelectedIndexChanged(object sender, EventArgs e)
     {
         List<cArrayList> arr = new List<cArrayList>();
         arr.Add(new cArrayList("@region", cbregion.SelectedValue.ToString()));
         bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_get", "salespointcd", "salespoint_desc", arr);
         arr.Clear();
         if (cbregion.SelectedValue.ToString() == "Kingdom")
         {
             cbsalespoint.Items.Insert(0, "ALL | SBTC KINGDOM");
         }
         arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
         //bll.vDelProposalSalespoint(arr);
         arr.Clear();
     }

     protected void btupload_Click(object sender, EventArgs e)
     {
         //if (upl.FileName == "")
         //{
         //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('No Image uploaded','Please scan the document and upload','warning');", true);
         //    return;
         //}
         //else
         //{
         //    FileInfo fi = new FileInfo(upl.FileName);
         //    string ext = fi.Extension;
         //    byte[] fs = upl.FileBytes;
         //    if (fs.Length <= 500000)
         //    {
         //        if (ext == ".jpg" || ext == ".jpeg" || ext == ".bmp" || ext == ".gif" || ext == ".png" || ext == ".JPEG" || ext == ".JPG" || ext == ".BMP" || ext == ".GIF" || ext == ".PNG")
         //        {
         //            if ((upl.FileName != "") || (upl.FileName != null))
         //            {
         //                List<cArrayList> arr = new List<cArrayList>();
         //                arr.Add(new cArrayList("@upload", "upload"));
         //                arr.Add(new cArrayList("@prop_no", txpropno.Text.ToString()));
         //                arr.Add(new cArrayList("@prop_scan", txpropno.Text.ToString().Replace("/", "_") + ext));
         //                upl.SaveAs(bll.sGetControlParameter("image_path") + "/proposal/" + txpropno.Text.ToString().Replace("/", "_") + ext);
         //                //string savedFileName = Server.MapPath("upload//proposal//" + txpropno.Text.ToString().Replace("/", "_") + ext);
         //                //upl.SaveAs(savedFileName);

         //                bll.vUpdatetMstProposal(arr);
         //                arr.Clear();
         //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Image Uploaded','" + txpropno.Text.ToString().Replace("/", "_") + ext + "','success');", true);

         //                btupload.Visible = false;
         //                hpfile_nm.Visible = true;
         //                upl.Visible = false;
         //                lbfileloc.Text = txpropno.Text.ToString().Replace("/", "_") + ext;
         //                //hpfile_nm.NavigateUrl = "file:///d:\\images\\proposal\\" + txpropno.Text.ToString().Replace("/", "_") + ext;
         //                //hpfile_nm.NavigateUrl = "~\\upload\\proposal\\" + txpropno.Text.ToString().Replace("/", "_") + ext;
         //                hpfile_nm.NavigateUrl = "/images/proposal/" + txpropno.Text.ToString().Replace("/", "_") + ext;
         //                return;
         //            }
         //        }
         //        else
         //        {
         //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('You should upload image','jpg,bmp,gif and png upload document again');", true);
         //            return;
         //        }
         //    }
         //    else
         //    {
         //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Image size too big','Image size must be less than 500KB');", true);
         //        return;
         //    }
         //}  
     }

     protected void btdelete_Click(object sender, EventArgs e)
     {
         List<cArrayList> arr = new List<cArrayList>();
         arr.Add(new cArrayList("@prop_no", txpropno.Text.ToString()));
         arr.Add(new cArrayList("@approval", 3));
         bll.vUpdatetMstProposal(arr);
         arr.Clear();
         ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Proposal has been Deactivated','" + txpropno.Text + "','success');", true);
         btprint.Visible = true;
         btprint2.Visible = true;
         btsave.Visible = false;
         if ((Request.Cookies["usr_id"].Value.ToString() == "3064") || (Request.Cookies["usr_id"].Value.ToString() == "1287"))
         {
             btapprove.Visible = false;
             btcancel.Visible = false;
             btedit.Visible = false;
             btdelete.Visible = false;
         }
         else
         {
             btapprove.Visible = false;
             btcancel.Visible = false;
             btedit.Visible = false;
             btdelete.Visible = false;
         }
         btedit.Visible = false;
         //upl.Visible = true;
         //btupload.Visible = true;
         return;
     }

     protected void btnuploaddoc_Click(object sender, EventArgs e)
     {
         List<cArrayList> arr = new List<cArrayList>();
         foreach (GridViewRow row in grdcate.Rows)
         {
             Label lbdoccode = (Label)row.FindControl("lbdoccode");
             Label lbdocname = (Label)row.FindControl("lbdocname");
             FileUpload upl = (FileUpload)row.FindControl("upl");
             if (upl.HasFile)
             {
                 FileInfo fi = new FileInfo(upl.FileName);
                 string ext = fi.Extension;
                 byte[] fs = upl.FileBytes;
                 if (fs.Length <= 1073741824)
                 {
                     
                     if ((upl.FileName != "") || (upl.FileName != null))
                     {
                         arr.Clear();
                         arr.Add(new cArrayList("@prop_no", txpropno.Text));
                         arr.Add(new cArrayList("@doc_cd", lbdoccode.Text));
                         arr.Add(new cArrayList("@fileloc", txpropno.Text.ToString().Replace("/", "_") + "-" + lbdoccode.Text + ext));
                         arr.Add(new cArrayList("@doc_nm", lbdocname.Text));
                         upl.SaveAs(bll.sGetControlParameter("image_path") + "/proposal_doc/" + txpropno.Text.ToString().Replace("/", "_") + "-" + lbdoccode.Text + ext);
                         bll.vInsertTproposalDoc(arr);
                     }
                     else
                     {
                         ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Please attach all the documents required','Proposal');", true);
                         return;
                     }
                 }
                 else
                 {
                     ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Image size too big','Image size must be less than 500MB');", true);
                     return;
                 }
             }
         }
         arr.Clear();
         arr.Add(new cArrayList("@prop_no", txpropno.Text));
         bll.vBindingGridToSp(ref grddoc, "sp_tproposal_document_get", arr);
     }

     protected void chbudget_CheckedChanged(object sender, EventArgs e)
     {
         int totalLimit = 0;
         System.Data.SqlClient.SqlDataReader rs = null;
         List<cArrayList> arr = new List<cArrayList>();

         if (cbbgitem.SelectedValue.ToString() == "A")
         {
             if (chbudget.Checked.ToString() == "1")
             {
                 if (hdprop.Value.ToString() == "")
                 {
                     arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
                 }
                 else
                 {
                     arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
                 }
                 if (rditem.SelectedValue.ToString() == "I")
                 {
                     bll.vGetBudgetLimitItem(arr, ref rs);
                     while (rs.Read())
                     {
                         totalLimit += int.Parse(rs["target"].ToString());
                     }
                     txbudget.Text = totalLimit.ToString();
                 }
                 else
                 {
                     bll.vGetBudgetLimitProduct(arr, ref rs);
                     while (rs.Read())
                     {
                         totalLimit += int.Parse(rs["target"].ToString());
                     }
                     txbudget.Text = totalLimit.ToString();
                 }
             }
             else
             {
                 //txbudget.Text = "0";
             }
         }         
     }

     protected void chfreegood_CheckedChanged(object sender, EventArgs e)
     {         
         List<cArrayList> arr = new List<cArrayList>();
         if (chfreegood.Checked == true)
         {
             lblfreeitem.Visible = true;
             lbldotfreeitem.Visible = true;
             rdfreeitem.Visible = true;
             tbaddfree.Visible = true;
             //lblfgbranded.Visible = true;
             //lblfggroup.Visible = true;
             //lblfgproduct.Visible = true;
             //lblfgitem.Visible = true;
         }
         else
         {
             lblfreeitem.Visible = false;
             lbldotfreeitem.Visible = false;
             rdfreeitem.Visible = false;
             tbaddfree.Visible = false;
             //lblfgbranded.Visible = false;
             //lblfggroup.Visible = false;
             //lblfgproduct.Visible = false;
             //lblfgitem.Visible = false;
         }
     }

     protected void rdfreeitem_SelectedIndexChanged(object sender, EventArgs e)
     {
         if (rdfreeitem.SelectedValue.ToString() == "I")
         {
             cbitemfrees.Visible = true;
             lblfgitem.Visible = true;
         }
         else if (rdfreeitem.SelectedValue.ToString() == "G")
         {
             cbitemfrees.Visible = false;
             lblfgitem.Visible = false;
         }
     }

     protected void cbbrandedfree_SelectedIndexChanged(object sender, EventArgs e)
     {
         List<cArrayList> arr = new List<cArrayList>();
         arr.Add(new cArrayList("@level_no", 2));
         arr.Add(new cArrayList("@prod_cd_parent", cbbrandedfree.SelectedValue.ToString()));
         bll.vBindingComboToSp(ref cbprodgroupfree, "sp_tmst_product_get", "prod_cd", "prod_nm", arr);
         cbprodgroupfree_SelectedIndexChanged(sender, e);
     }
     protected void cbprodgroupfree_SelectedIndexChanged(object sender, EventArgs e)
     {
         List<cArrayList> arr = new List<cArrayList>();
         arr.Add(new cArrayList("@level_no", 3));
         arr.Add(new cArrayList("@prod_cd_parent", cbprodgroupfree.SelectedValue.ToString()));
         bll.vBindingComboToSp(ref cbitemfree, "sp_tmst_product_get", "prod_cd", "prod_nm", arr);
         cbitemfree_SelectedIndexChanged(sender, e);
     }
     protected void cbitemfree_SelectedIndexChanged(object sender, EventArgs e)
     {
         List<cArrayList> arr = new List<cArrayList>();
         arr.Add(new cArrayList("prod_cd", cbitemfree.SelectedValue.ToString()));
         //arr.Add(new cArrayList("@prod_cd_parent", cbprodgroupfree.SelectedValue.ToString()));
         bll.vBindingComboToSp(ref cbitemfrees, "sp_tmst_item_get", "item_cd", "item_desc", arr);
     }
     protected void btadditemfree_Click(object sender, EventArgs e)
     {
         if (rdfreeitem.SelectedValue.ToString() == "")
         {
             ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Select group and product !','Product or Product Group','warning');", true);
             return;
         }
         List<cArrayList> arr = new List<cArrayList>();
         if (rdfreeitem.SelectedValue.ToString() == "I")
         {
             arr.Add(new cArrayList("@prod_cd", cbitemfrees.SelectedValue.ToString()));
             arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
             bll.vInsertWrkFreeItem2(arr); arr.Clear();
             arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
             bll.vBindingGridToSp(ref grdfreeitem, "sp_twrk_freeitem_get2", arr);
             grdfreeitem.Visible = true;
         }
         else if (rdfreeitem.SelectedValue.ToString() == "G")
         {
             arr.Add(new cArrayList("@prod_cd", cbitemfree.SelectedValue.ToString()));
             arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
             bll.vInsertWrkFreeProduct2(arr);
             arr.Clear();
             arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
             bll.vBindingGridToSp(ref grdfreeproduct, "sp_twrk_freeproduct_get2", arr);
             grdfreeproduct.Visible = true;
         }
     }     

     protected void grdfreeitem_PageIndexChanging(object sender, GridViewPageEventArgs e)
     { }

     protected void grdfreeitem_RowDeleting1(object sender, GridViewDeleteEventArgs e)
     {
         Label lbitemcode;
         lbitemcode = (Label)grdfreeitem.Rows[e.RowIndex].FindControl("lbitemcode");
         List<cArrayList> arr = new List<cArrayList>();
         arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
         arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
         bll.vDelWrkFreeItem2(arr); arr.Clear();
         arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
         bll.vBindingGridToSp(ref grdfreeitem, "sp_twrk_freeitem_get2", arr);
         grdfreeitem.Visible = true;
         
     }

     protected void grdfreeproduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
     { }

     protected void grdfreeproduct_RowDeleting1(object sender, GridViewDeleteEventArgs e)
     {
         Label lbprodcode;
         lbprodcode = (Label)grdfreeproduct.Rows[e.RowIndex].FindControl("lbprodcode");
         List<cArrayList> arr = new List<cArrayList>();
         arr.Add(new cArrayList("@prod_cd", lbprodcode.Text));
         arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
         bll.vDelWrkFreeProduct2(arr); arr.Clear();
         arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
         bll.vBindingGridToSp(ref grdfreeproduct, "sp_twrk_freeproduct_get2", arr);
         grdfreeproduct.Visible = true;
     }

     protected void getBudgetMtdYtd(string code)
     {
         string sPropNo = string.Empty;
         string propCode = code;
         System.Data.SqlClient.SqlDataReader rs = null;
         List<cArrayList> arr = new List<cArrayList>();
         arr.Clear(); 
         arr.Add(new cArrayList("@prop_code", propCode));
         arr.Add(new cArrayList("@cost", cbmarketingcost.SelectedValue.ToString()));
         arr.Add(new cArrayList("@vendor", cbvendor.SelectedValue.ToString()));
         arr.Add(new cArrayList("@month", DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month));
         arr.Add(new cArrayList("@year", DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year));
         
         //if (cbmarketingcost.SelectedValue.ToString() == "Percentage")
         //{
         //    arr.Add(new cArrayList("@pctsbtc", txcostsbtc.Text.ToString()));
         //    arr.Add(new cArrayList("@pctprincipal", txprincipalcost.Text.ToString()));
         //}

         bll.vGetBudgetMtdYtd(arr, ref rs);
         while (rs.Read())
         {
             txbudgetmtd.Text = (rs["mtd"].ToString() == "" ? "0" : rs["mtd"].ToString());
             txbudgetytd.Text = (rs["ytd"].ToString() == "" ? "0" : rs["ytd"].ToString());
         }
         txbudgetmtd.Text = String.Format("{0:n}", Convert.ToDecimal(txbudgetmtd.Text));
         txbudgetytd.Text = String.Format("{0:n}", Convert.ToDecimal(txbudgetytd.Text));
     }

     protected void btaddcustex_Click(object sender, EventArgs e)
     {
        List<cArrayList> arr = new List<cArrayList>();
        if (hdprop.Value.ToString() == "")
        {
            arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
        }
        else
        {
            arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
        }
        arr.Add(new cArrayList("@cust_cd", hdcustex.Value.ToString()));
        arr.Add(new cArrayList("@cust_nm", txsearchcustex.Text.ToString()));
        bll.vInsertProposalCustomerEx(arr); arr.Clear();
        if (hdprop.Value.ToString() == "")
        {
            arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
        }
        else
        {
            arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
        }
        bll.vBindingGridToSp(ref grdcustex, "sp_tproposal_customer_ex_get", arr);
        grdcustex.CssClass = "divgrid";
     }
     protected void grdcustex_RowDeleting(object sender, GridViewDeleteEventArgs e)
     {
         Label lblsalespointcd = (Label)grdcustex.Rows[e.RowIndex].FindControl("lblsalespointcd");
         Label lblcustcode = (Label)grdcustex.Rows[e.RowIndex].FindControl("lbcustcode");
         List<cArrayList> arr = new List<cArrayList>();
         if (hdprop.Value.ToString() == "")
         {
             arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
         }
         else
         {
             arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
         }
         arr.Add(new cArrayList("@salespoint_cd", lblsalespointcd.Text));
         arr.Add(new cArrayList("@cust_cd", lblcustcode.Text));
         bll.vDelProposalCustomerEx(arr);
         arr.Clear();
         if (hdprop.Value.ToString() == "")
         {
             arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
         }
         else
         {
             arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
         }
         bll.vBindingGridToSp(ref grdcustex, "sp_tproposal_customer_ex_get", arr);
     }

     protected void btsearchdisc_Click(object sender, EventArgs e)
     {
         ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "popupwindow('lookupreqdiscount_ho.aspx');", true);
         
     }
     protected void btdiscount_Click(object sender, EventArgs e)
     {
         string statusDiscount = "", budget = "", payment = "";
         txreqdisc.Text = hdpromo.Value.ToString();
         statusDiscount = bll.vLookUp("select disc_cd from tmst_reqdiscount where disc_cd='"+hdpromo.Value.ToString()+"' and (prop_no = '' or prop_no is null) and disc_sta_id = 'AP'");

         if (statusDiscount != "")
         {
             if (Session["edit"] != "")
             {
                 txreqdisc.Text = hdpromo.Value.ToString();
             }
             else
             {
                 txreqdisc.Text = hdpromo.Value.ToString();

                 List<cArrayList> arr = new List<cArrayList>();
                 System.Data.SqlClient.SqlDataReader rs = null;
                 arr.Add(new cArrayList("@promo_no", hdpromo.Value));
                 arr.Add(new cArrayList("@status", ""));
                 arr.Add(new cArrayList("@branch", ""));
                 arr.Add(new cArrayList("@product", ""));
                 arr.Add(new cArrayList("@supervisor", ""));
                 arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                 bll.vGetReqPromo(ref rs, arr);
                 while (rs.Read())
                 {
                     dtstart.Text = Convert.ToDateTime(rs["start_dt"]).ToString("dd/MM/yyyy");
                     dtend.Text = Convert.ToDateTime(rs["end_dt"]).ToString("dd/MM/yyyy");
                     dtdelivery.Text = Convert.ToDateTime(rs["delivery_dt"]).ToString("dd/MM/yyyy");
                     cbpromokind.SelectedValue = rs["promokind"].ToString();
                     cbpromokind_SelectedIndexChanged(sender, e);
                     cbpromogroup.SelectedValue = rs["promo_cd"].ToString();
                     cbpromogroup_SelectedIndexChanged(sender, e);
                     cbpromotype.SelectedValue = rs["promo_typ"].ToString();
                     rdcust.SelectedValue = rs["rdcustomer"].ToString();
                     rditem.SelectedValue = rs["rditem"].ToString();
                     payment = rs["rdpayment"].ToString();
                     budget = rs["budget"].ToString();
                     txbgremark.Text = rs["remark"].ToString();
                     cbvendor.SelectedValue = rs["vendor_cd"].ToString();
                 }
                 rs.Close();


                 // Add Salespoint to Proposal 
                 arr.Clear();
                 arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
                 arr.Add(new cArrayList("@salespoint_cd", null));
                 arr.Add(new cArrayList("@reqdisc_cd", hdpromo.Value.ToString()));
                 bll.vInsertProposalSalespoint(arr); arr.Clear();
                 arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
                 bll.vBindingGridToSp(ref grdslspoint, "sp_tproposal_salespoint_get", arr);

                 // Add Customer / Group to Proposal
                 rdcust_SelectedIndexChanged(sender, e);
                 if (rdcust.SelectedValue.ToString() == "C")
                 {
                     arr.Clear();
                     arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
                     arr.Add(new cArrayList("@reqdisc_cd", hdpromo.Value.ToString()));
                     bll.vInsertProposalCustomer(arr); arr.Clear();
                     arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
                     bll.vBindingGridToSp(ref grdcust, "sp_tproposal_customer_get", arr);
                     grdcust.CssClass = "divgrid";
                     grdcusgrcd.CssClass = "divhid";
                     grdcusttype.CssClass = "divhid";
                 }
                 else if (rdcust.SelectedValue.ToString() == "G")
                 {
                     arr.Clear();
                     arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
                     arr.Add(new cArrayList("@reqdisc_cd", hdpromo.Value.ToString()));
                     bll.vInsertProposalCusgrcd(arr); arr.Clear();
                     arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
                     bll.vBindingGridToSp(ref grdcusgrcd, "sp_tproposal_cusgrcd_get", arr);
                     grdcust.CssClass = "divhid";
                     grdcusgrcd.CssClass = "divgrid";
                     grdcusttype.CssClass = "divhid";
                 }
                 else if (rdcust.SelectedValue.ToString() == "T")
                 {
                     arr.Clear();
                     arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
                     arr.Add(new cArrayList("@reqdisc_cd", hdpromo.Value.ToString()));
                     bll.vInsertProposalCustType(arr); arr.Clear();
                     arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
                     bll.vBindingGridToSp(ref grdcusttype, "sp_tproposal_custtype_get", arr);
                     grdcust.CssClass = "divhid";
                     grdcusgrcd.CssClass = "divhid";
                     grdcusttype.CssClass = "divnormal";
                 }

                 // Add Item / Product to Proposal
                 rditem_SelectedIndexChanged(sender, e);
                 arr.Clear();
                 if (rditem.SelectedValue.ToString() == "I")
                 {
                     arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
                     arr.Add(new cArrayList("@reqdisc_cd", hdpromo.Value.ToString()));
                     bll.vInsertProposalItem(arr); arr.Clear();
                     arr.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
                     arr.Add(new cArrayList("@customer", rdcust.SelectedValue.ToString()));
                     bll.vBindingGridToSp(ref grditem, "sp_treqdiscount_item_get", arr);
                     //bll.vBindingGridToSp(ref grditem, "sp_tproposal_item_get", arr);
                     grditem.CssClass = "divgrid";
                     grditem.Visible = true;
                     txsearchitem.Text = "";
                     grdviewitem.CssClass = "divhid";
                     grdviewitem.Visible = false;
                     string itembg = bll.vLookUp("select top 1 item_bg from treqdiscount_item where disc_cd='" + hdpromo.Value.ToString() + "'");
                     cbbgitem.SelectedValue = itembg;
                 }
                 else if (rditem.SelectedValue.ToString() == "G")
                 {
                     arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString())); 
                     arr.Add(new cArrayList("@reqdisc_cd", hdpromo.Value.ToString()));
                     bll.vInsertProposalProduct(arr); arr.Clear();
                     arr.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
                     arr.Add(new cArrayList("@customer", rdcust.SelectedValue.ToString()));
                     bll.vBindingGridToSp(ref grdgroup, "sp_treqdiscount_product_get", arr);
                     //bll.vBindingGridToSp(ref grdgroup, "sp_tproposal_productgroup_get", arr);
                     grdgroup.CssClass = "divgrid";
                     grdgroup.Visible = true;
                     grdviewgroup.CssClass = "divhid";
                     grdviewgroup.Visible = false;
                     string itembg = bll.vLookUp("select top 1 prod_bg from treqdiscount_product where disc_cd='" + hdpromo.Value.ToString() + "'");
                     cbbgitem.SelectedValue = itembg;
                 }
                 txbudget.Text = budget.ToString();
                 cbpaymenttype.SelectedValue = payment.ToString();
                 cbpaymenttype_SelectedIndexChanged(sender, e);
             }
         }
         else
         {
             hdpromo.Value = "";
             txreqdisc.Text = "";
             ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Request Promotion Already Used/Status not yet PROCESSED TO PROPOSAL','select other request discount','warning');", true);
             return;
         }


     }
}