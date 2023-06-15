using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_mstcontractentry : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
            bll.vDelContractPaySchedule(arr);
            bll.vDelContractItem(arr);
            bll.vDelContractProduct(arr);
            bll.vDelContractFreeProduct(arr);
            bll.vDelContractFreeItem(arr);
            txcontractno.Text = "NEW";
            bll.vBindingFieldValueToCombo(ref cbuom, "uom");
            lbstatus.Text = bll.vLookUp("select fld_desc from tfield_value where fld_nm='cont_sta_id' and fld_valu='N'");
            arr.Clear();
            bll.vBindingFieldValueToCombo(ref cbcontractype, "contract_typ");
            bll.vBindingFieldValueToCombo(ref cbind, "cust_ind");
            bll.vBindingFieldValueToCombo(ref cbcontractterm, "contract_term");            
            bll.vBindingFieldValueToCombo(ref cbcusgrcd, "cusgrcd");
            bll.vBindingFieldValueToCombo(ref cbcontractpayment, "contract_payment");
            txcust.CssClass = "ro";
            arr.Add(new cArrayList("@doc_typ", "CONT"));
            bll.vBindingComboToSp(ref cbdocument, "sp_tmst_document_get","doc_cd","doc_nm", arr);
            lbsp.Text = bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            dtstart.Text = System.DateTime.Today.ToString("d/M/yyyy");
            dtend.Text = System.DateTime.Today.ToString("d/M/yyyy");
            dtcontract.Text = System.DateTime.Today.ToString("d/M/yyyy");
            cbcontractterm_SelectedIndexChanged(sender, e);
            cbcontractpayment_SelectedIndexChanged(sender, e);
            cbind_SelectedIndexChanged(sender, e);
            cbcontractype_SelectedIndexChanged(sender, e);
        }
    }
    protected void cbind_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbind.SelectedValue.ToString() == "G")
        {
            cbcusgrcd.CssClass = "readwrite";
            txcust.CssClass = "ro";
            bll.vBindingFieldValueToCombo(ref cbcusgrcd, "cusgrcd");
            //txpctbonus.CssClass = "ro";
            //txsold.CssClass = "ro";
            
        }
        else
        {
            cbcusgrcd.CssClass = "ro";
            txcust.CssClass = "makeitreadwrite";
            cbcusgrcd.Items.Clear();
            //txpctbonus.CssClass = "makeitreadwrite";
            //txsold.CssClass = "makeitreadwrite";
        }
    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        string i = bll.vLookUp("select doc_cd from tmp_contract_document where doc_cd='co1' and usr_id=" + Request.Cookies["usr_id"].Value.ToString());
        string j = bll.vLookUp("select doc_cd from tmp_contract_document where doc_cd='co2' and usr_id=" + Request.Cookies["usr_id"].Value.ToString());
        if (cbdocument.SelectedValue == "CO1")
        {
            if (i == "CO1")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','You are allowed to add one picture','error');", true);
            }
            else
            {
                inserttmp();
            }
        }
        if (cbdocument.SelectedValue == "CO2")
        {
            if (j == "CO2") { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','You are allowed to add one legal agreement','error');", true); }
            else { inserttmp(); }
        }
                             
    }
    protected void inserttmp()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@doc_cd", cbdocument.SelectedValue.ToString()));
        arr.Add(new cArrayList("@image_path", upl.FileName));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vInserttmp_contractdocument(arr);
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grddoc, "sp_tmp_contractdocument_get", arr);
        string sLocFile = bll.sGetControlParameter("image_path");
        if (upl.FileName != "")
        { upl.SaveAs(sLocFile + ".jpg"); }

    }
    protected void btsave_Click(object sender, EventArgs e)
    {                    
        string sSoNo = "";
        List<cArrayList> arr = new List<cArrayList>();
        if (dtcontract.Text == "" && dtstart.Text == "" && dtend.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Contract dates!','Please select contract and start and end dates !','warning');", true);
            return;
        }

        if (txmanualno.Text == ""&&txcust.Text=="")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Manual No!','Please write manual NO. !','warning');", true);
            return;
        }
        if (cbind.SelectedValue=="C" && txcust.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Customer Name!','Please write Customer Name. !','warning');", true);
            return;
        }

        if (txamount.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Total Amount / Qty can not empty','Amount / Qty !','warning');", true);
            return;
        }

        if (grdprod.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Product not yet selected','Product','warning');", true);
            return;
        }
        double dAmt = 0;
        if (!double.TryParse(txamount.Text, out dAmt))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Total Amt / Qty must be numeric','Total Amount/Qty','warning');", true);
            return;
        }
       
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@contract_dt", DateTime.ParseExact(dtcontract.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@start_dt", DateTime.ParseExact(dtstart.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@end_dt", DateTime.ParseExact(dtend.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@contract_typ", cbcontractype.SelectedValue.ToString()));
        arr.Add(new cArrayList("@contract_term", cbcontractterm.SelectedValue.ToString()));
        arr.Add(new cArrayList("@contract_payment", cbcontractpayment.SelectedValue));
        arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
        arr.Add(new cArrayList("@cusgrcd", cbcusgrcd.SelectedValue));
        arr.Add(new cArrayList("@cust_ind", cbind.SelectedValue));
        arr.Add(new cArrayList("@cont_sta_id", "N"));
        arr.Add(new cArrayList("@amt", txamount.Text));
        arr.Add(new cArrayList("@manual_no", txmanualno.Text));
        arr.Add(new cArrayList("@rdfreeitem", rdfreeitem.SelectedValue.ToString()));
        arr.Add(new cArrayList("@rditem", rditem.SelectedValue.ToString()));
        arr.Add(new cArrayList("@deleted", 0));
        arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
        bll.vInserttmst_contract(arr, ref sSoNo);
        txcontractno.Text = sSoNo;
        
        arr.Clear();
        arr.Add(new cArrayList("@contract_no", sSoNo));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vUpdateContractPaySchedule(arr);

        if (rditem.SelectedValue.ToString() == "I")
        {
            foreach (GridViewRow row in grdprod.Rows)
            {
                Label lbitemcode = (Label)row.FindControl("lbitemcode");

                arr.Clear();
                arr.Add(new cArrayList("@contract_no", sSoNo));
                arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
                bll.vInsertContractItem(arr);
            }
        }
        else if (rditem.SelectedValue.ToString() == "G")
        {
            foreach (GridViewRow row in grdprod.Rows)
            {
                Label lbitemcode = (Label)row.FindControl("lbitemcode");

                arr.Clear();
                arr.Add(new cArrayList("@contract_no", sSoNo));
                arr.Add(new cArrayList("@prod_cd", lbitemcode.Text));
                bll.vInsertContractProduct(arr);
            }
        }

       
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('" + sSoNo + "','New contract has been created !','success');", true);
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
    protected void cbcontractterm_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
        bll.vDelContractPaySchedule(arr);
        int nCount = Convert.ToInt16(cbcontractterm.SelectedValue);
        if (nCount != 1)
        {
            nCount /= 30;
        }
        for (int i = 1; i <= nCount;i++ )
        {
            arr.Clear();
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@sequenceno", i));
            arr.Add(new cArrayList("@payment_dt", System.DateTime.Today));
            arr.Add(new cArrayList("@paycont_sta_id", "N"));
            bll.vInsertContractPaySchedule(arr);
        }
        arr.Clear();
        arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdschedule, "sp_tcontract_payschedule_get", arr);
        cbcontractpayment_SelectedIndexChanged(sender, e);
      //  lbpayment.Text = "Paid Every " + cbcontractterm.SelectedItem.Text;
    }
    protected void rditem_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (rditem.SelectedValue.ToString() == "I")
        {
            arr.Clear();
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
            bll.vDelContractProduct(arr);
            bll.vBindingGridToSp(ref grdprod, "sp_tcontract_product_get", arr);
            txitem.Visible = true;
            cbprod.Visible = false;
            btaddprod.Visible = true;
        }
        else if (rditem.SelectedValue.ToString() == "P")
        {
            arr.Clear();
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
            bll.vDelContractItem(arr);
            bll.vBindingGridToSp(ref grdprod, "sp_tcontract_item_get", arr);
            arr.Clear();
            arr.Add(new cArrayList("@level_no", 3));
            bll.vBindingComboToSp(ref cbprod, "sp_tmst_product_get", "prod_cd", "prod_nm",arr);
            txitem.Visible = false;
            cbprod.Visible = true;
            btaddprod.Visible = true;
        }
    }
    protected void btaddprod_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (rditem.SelectedValue.ToString() == "I")
        {
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@item_cd", hditem.Value.ToString()));
            bll.vInsertContractItem(arr); arr.Clear();
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grdprod, "sp_tcontract_item_get", arr);
        }
        else if (rditem.SelectedValue.ToString() == "P")
        {
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@prod_cd",cbprod.SelectedValue.ToString()));
            bll.vInsertContractProduct(arr);
            arr.Clear();
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grdprod, "sp_tcontract_product_get", arr);
        }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList2(string prefixText, int count, string contextKey)
    {
        List<cArrayList> arr = new List<cArrayList>();
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        List<string> litem = new List<string>();
        string sitem = string.Empty;
        System.Data.SqlClient.SqlDataReader rs = null;
        cbll bll = new cbll();
        arr.Add(new cArrayList("@item_cd", prefixText));
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        bll.vSearchMstItemBySalespoint(arr, ref rs);
        while (rs.Read())
        { 
            sitem = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["item_cd"].ToString() + "-" + rs["item_nm"].ToString() + "-" + rs["size"].ToString() + "-" + rs["branded_nm"].ToString(),rs["item_cd"].ToString());
            litem.Add(sitem);
        }
        rs.Close();
        return (litem.ToArray());
    }
    protected void cbcontractpayment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbcontractpayment.SelectedValue.ToString() == "FG")
        {
            lbamt.Text = "TOT QTY";// lbpayment.Text = "N/A";
        }else if (cbcontractpayment.SelectedValue.ToString() == "CH")
        {
            lbamt.Text = "TOT AMOUNT";
            //lbpayment.Text = "Paid Every " + cbcontractpayment.SelectedItem.Text;
        }

        foreach (GridViewRow row in grdschedule.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                if (cbcontractpayment.SelectedValue.ToString() == "FG")
                {
                    TextBox txamt = (TextBox)row.FindControl("txamt");
                    TextBox txqty = (TextBox)row.FindControl("txqty");
                    DropDownList cbuom = (DropDownList)row.FindControl("cbuom");
                    txamt.CssClass = "ro"; txqty.CssClass = "makeitreadwrite";
                    txamt.Text = "0"; cbuom.CssClass = "makeitreadwrite";
                }
                else if (cbcontractpayment.SelectedValue.ToString() == "CH")
                {
                    TextBox txamt = (TextBox)row.FindControl("txamt");
                    TextBox txqty = (TextBox)row.FindControl("txqty");
                    DropDownList cbuom = (DropDownList)row.FindControl("cbuom");
                    txqty.CssClass = "ro"; txamt.CssClass = "makeitreadwrite";
                    txqty.Text = "0"; cbuom.SelectedValue = "";
                    cbuom.CssClass = "ro";
                    
                    
                }
            }
        }
    }
    protected void grdschedule_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList cbuom = (DropDownList) e.Row.FindControl("cbuom");
            bll.vBindingFieldValueToCombo(ref cbuom, "uom");
        }
    }
    protected void chkincentive_CheckedChanged(object sender, EventArgs e)
    {
        
    }
    protected void rdfreeitem_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (rdfreeitem.SelectedValue.ToString() == "I")
        {
            arr.Clear();
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
            bll.vDelContractFreeItem(arr);
            //bll.vBindingGridToSp(ref cbfreeprod, "sp_tcontract_product_get", arr);
            txitemfree.Visible = true;
            txitemfree.CssClass = "makeitreadwrite";
            cbfreeprod.Visible = false;
            btaddprod.Visible = true;
        }
        else if (rdfreeitem.SelectedValue.ToString() == "P")
        {
            arr.Clear();
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
            bll.vDelContractFreeProduct(arr);
            //bll.vBindingGridToSp(ref grdprod, "sp_tcontract_item_get", arr);
            arr.Clear();
            arr.Add(new cArrayList("@level_no", 3));
            bll.vBindingComboToSp(ref cbprod, "sp_tmst_product_get", "prod_cd", "prod_nm", arr);
            txitemfree.Visible = false;
            cbfreeprod.Visible = true;
            cbfreeprod.CssClass = "makeitreadwrite";
            btaddprod.Visible = true;
            arr.Clear();
            arr.Add(new cArrayList("@level_no", "3"));
            bll.vBindingComboToSp(ref cbfreeprod, "sp_tmst_product_get", "prod_cd", "prod_nm", arr);
        }
    }
    protected void cbcontractype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbcontractype.SelectedValue.ToString() == "T")
        {
            txpctbonus.CssClass = "makeitreadwrite";
            txsold.CssClass = "makeitreadwrite";
        }
        else if (cbcontractype.SelectedValue.ToString() == "G")
        {
            txpctbonus.CssClass = "ro";
            txsold.CssClass = "ro";
        }
    }
    protected void btaddfree_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
        if (rdfreeitem.SelectedValue.ToString() == "I")
        {
            arr.Add(new cArrayList("@item_cd", hditemfree.Value.ToString()));
            bll.vInsertContractFreeItem(arr);
            arr.Clear();
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grdfreeprod, "sp_tcontract_freeitem_get", arr);
        }
        else if (rdfreeitem.SelectedValue.ToString() == "P") 
        {
                arr.Add(new cArrayList("@prod_cd", cbfreeprod.SelectedValue.ToString()));
                bll.vInsertContractFreeProduct(arr);
                arr.Clear();
                arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
                bll.vBindingGridToSp(ref grdfreeprod, "sp_tcontract_freeproduct_get", arr);
        }
        
    }
    protected void grdprod_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbitemcode = (Label)grdprod.Rows[e.RowIndex].FindControl("lbitemcode");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
        bll.vDelContractItem(arr);
        arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@prod_cd", lbitemcode.Text));
        bll.vDelContractProduct(arr);
        arr.Clear();arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        if (rditem.SelectedValue.ToString() == "I")
        {bll.vBindingGridToSp(ref grdprod, "sp_tcontract_item_get", arr);}
        else if (rditem.SelectedValue.ToString() == "P")
        { bll.vBindingGridToSp(ref grdprod, "sp_tcontract_product_get", arr); }


    }
}