using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_mstcontract2 : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            bll.vBindingFieldValueToCombo(ref cbagreetype, "agree_typ");
          //  arr.Add(new cArrayList("@agree_typ", "GON"));
          //  bll.vBindingGridToSp(ref grdagree, "tmst_agreement_get", arr);
            bll.vBindingFieldValueToCombo(ref cbloc, "display_loc");
            arr.Clear();
            arr.Add(new cArrayList("@fld_nm", "display_typ"));
            bll.vBindingGridToSp(ref grddisplay, "sp_tfield_value_get", arr);
            arr.Clear();
            arr.Add(new cArrayList("@agree_typ", "TRM"));
            bll.vBindingGridToSp(ref grdterm, "tmst_agreement_get", arr);
            arr.Clear();
            arr.Add(new cArrayList("@agree_typ", "DMG"));
            bll.vBindingGridToSp(ref grddamage, "tmst_agreement_get", arr);
            arr.Clear();
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
            bll.vDelContractPaySchedule(arr);
            bll.vDelContractItem(arr);
            bll.vDelContractProduct(arr);
            bll.vDelContractFreeProduct(arr);
            bll.vDelContractFreeItem(arr);
            bll.vDelContractCustomer(arr);
            bll.vDelContractCusgrcd(arr);
            bll.vBindingFieldValueToCombo(ref cbpaymenttype, "contract_payment");
            //txcontractno.Text = "NEW";
            //bll.vBindingFieldValueToCombo(ref cbuom, "uom");
            //lbstatus.Text = bll.vLookUp("select fld_desc from tfield_value where fld_nm='cont_sta_id' and fld_valu='N'");
            //arr.Clear();
            //bll.vBindingFieldValueToCombo(ref cbcontractype, "contract_typ");
            //bll.vBindingFieldValueToCombo(ref cbind, "cust_ind");
            bll.vBindingFieldValueToCombo(ref cbcontractterm, "contract_term");
            cbcontractterm_SelectedIndexChanged(sender, e);
            cbagreetype_SelectedIndexChanged(sender, e);
            dtcontract.Text = System.DateTime.Today.ToString("d/M/yyyy");
        }
    }
    protected void rdcust_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdcust.SelectedValue.ToString() == "C")
        { txcust.Visible = true; cbgroupcust.Visible = false;  }
        else { txcust.Visible = false; cbgroupcust.Visible = true; bll.vBindingFieldValueToCombo(ref cbgroupcust, "cusgrcd"); }
    }
    protected void cbagreetype_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (cbagreetype.SelectedValue.ToString() == "GON")
        {
            txpct.CssClass = "form-control input-sm ro"; txprevsold.CssClass = "form-control input-sm ro"; txincreasing.CssClass = "form-control input-sm ro";
            lbB.Text = "Type Of Display";
            cbcontractterm.CssClass = "form-control input-sm";
            cbpaymenttype.CssClass = "form-control input-sm";
            txpct.Text = "0";
            txincreasing.Text = "0";
            txprevsold.Text = "0";
        }
        else if (cbagreetype.SelectedValue.ToString() == "TAC")
        {
            txpct.CssClass = "form-control input-sm"; txprevsold.CssClass = "form-control input-sm";
            txincreasing.CssClass = "form-control input-sm"; lbB.Text = "The Period";
            cbcontractterm.CssClass = "form-control input-sm ro";
            cbpaymenttype.CssClass = "form-control input-sm ro";
       
        }
        arr.Add(new cArrayList("@agree_typ", cbagreetype.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grdagree, "tmst_agreement_get", arr);

    }
    protected void cbpayment_SelectedIndexChanged(object sender, EventArgs e)
    {
        
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
        for (int i = 1; i <= nCount; i++)
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
        cbpaymenttype_SelectedIndexChanged(sender, e);
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        string sContract = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();

        if (cbagreetype.SelectedValue.ToString() == "TAC")
        {
            double dpct = 0;
            double dincreasing = 0;
            double dprev = 0;
            if (!double.TryParse( txpct.Text , out dpct))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Bonus Percentage must numeric or can not empty','Tactical bonus percentage','warning');", true);
                return;
            }

            if (!double.TryParse( txincreasing.Text, out dincreasing))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Increasing percentage must numeric or can not empty','Increment percentage sold','warning');", true);
                return;
            }

            if (!double.TryParse( txprevsold.Text, out dprev))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Previous sold must numeric or can not empty','Previous year qty sold','warning');", true);
                return;
            }
        }

        if (grditem.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Item Or Product Group must be selected !','Which Item/Product Group to be displayed','warning');", true);
            return;
        }
        if (cbpaymenttype.SelectedValue.ToString() == "FG")
        {
            if (grdfreeitem.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Item Or Product for paid must be selected !','Which Item/Product Group to be Paid','warning');", true);
                return;
            }
        }

        if ((dtstart.Text == "") || (dtend.Text == ""))
        {
             ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Period contract must be completed !','Start and End Date','warning');", true);
             return;
        }
       
            arr.Add(new cArrayList("@start_dt", DateTime.ParseExact(dtstart.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@contract_dt", DateTime.ParseExact(dtcontract.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@end_dt", DateTime.ParseExact(dtend.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@rdcust", rdcust.SelectedValue.ToString()));
            arr.Add(new cArrayList("@rditem", rditem.SelectedValue.ToString()));
            arr.Add(new cArrayList("@rdfreeitem", rdfreeitem.SelectedValue.ToString()));
            arr.Add(new cArrayList("@contract_term", cbcontractterm.SelectedValue.ToString()));
            arr.Add(new cArrayList("@contract_payment", cbpaymenttype.SelectedValue.ToString()));
            arr.Add(new cArrayList("@totamt", "0"));
            arr.Add(new cArrayList("@deleted", "0"));
            arr.Add(new cArrayList("@pctbonus", txpct.Text));
            arr.Add(new cArrayList("@pctincreasing", txincreasing.Text));
            arr.Add(new cArrayList("@prevsold", txprevsold.Text));
            bll.vInsertMstContract(arr, ref sContract);
            txcontractno.Text = sContract;
            txcontractno.CssClass = "ro";
        if (rdcust.SelectedValue.ToString() == "C")
        {
            arr.Clear();
            arr.Add(new cArrayList("@contract_no", sContract));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vUpdateContractCustomer(arr);

        }
        else
        {
            arr.Clear();
            arr.Add(new cArrayList("@contract_no", sContract));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vUpdateContractCusgrcd(arr);
        }

        if (cbpaymenttype.SelectedValue.ToString() == "FG")
        {
            if (rdfreeitem.SelectedValue.ToString() == "I")
            {
                arr.Clear();
                arr.Add(new cArrayList("@contract_no", sContract));
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                bll.vUpdateContractFreeitem(arr);
            }
            else {
                arr.Clear();
                arr.Add(new cArrayList("@contract_no", sContract));
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                bll.vUpdateContractCusgrcd(arr);
            }
        }
        foreach (GridViewRow row in grdagree.Rows)
        {
            Label lbagreecode = (Label)row.FindControl("lbagreecode");
            arr.Clear();
            arr.Add(new cArrayList("@agree_cd", lbagreecode.Text));
            arr.Add(new cArrayList("@contract_no", sContract));
            bll.vInsertContractAgreement(arr);
        }
        btsave.Visible = false;
        btnew.Visible = true;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('New contract has been saved','" + sContract + "','success');", true);
    }
    protected void cbpaymenttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbpaymenttype.SelectedValue.ToString() == "CH")
        {
            btaddfree.Visible = false;
            foreach (GridViewRow row in grdschedule.Rows)
            {
                TextBox txamt = (TextBox)row.FindControl("txamt");
                TextBox txqty = (TextBox)row.FindControl("txqty");
                txqty.CssClass = "form-control input-sm ro";
                txamt.CssClass = "form-control input-sm";
                cbfreeprod.CssClass = "form-control input-sm ro";
                txfreeitem.CssClass = "form-control input-sm ro";
                
            }
        }
        else if (cbpaymenttype.SelectedValue.ToString() == "FG")
        {
            btaddfree.Visible = true;
            foreach (GridViewRow row in grdschedule.Rows)
            {
                TextBox txamt = (TextBox)row.FindControl("txamt");
                TextBox txqty = (TextBox)row.FindControl("txqty");
                txqty.CssClass = "form-control input-sm";
                txamt.CssClass = "form-control input-sm ro";
                cbfreeprod.CssClass = "form-control input-sm";
                txfreeitem.CssClass = "form-control input-sm";
            }
        }
    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (rdcust.SelectedValue.ToString() == "C")
        {
            arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
            arr.Add(new cArrayList("@contract_no" ,  Request.Cookies["usr_id"].Value.ToString()));
            bll.vInsertContractCustomer(arr);
            arr.Clear();
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grdcust, "sp_tcontract_customer_get", arr);
        }
        else if (rdcust.SelectedValue.ToString() == "G")
        {
            arr.Add(new cArrayList("@cusgrcd", cbgroupcust.SelectedValue.ToString()));
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
            bll.vInsertContractCusgrcd(arr); arr.Clear();
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grdcust, "sp_tcontract_cusgrcd_get", arr);
        }
    }
    protected void btadditem_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (rditem.SelectedValue.ToString() == "I")
        { arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@item_cd", hditem.Value.ToString()));
        bll.vInsertContractItem(arr);
        arr.Clear();
        arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grditem, "sp_tcontract_item_get", arr);
        }
        else
        {
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@prod_cd", cbprod.SelectedValue.ToString()));
            bll.vInsertContractProduct(arr);
            arr.Clear();
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grditem, "sp_tcontract_product_get", arr);
        }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        string sCust = string.Empty;
        HttpCookie cok= HttpContext.Current.Request.Cookies["sp"];
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lcust = new List<string>();
        cbll bll = new cbll();
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cust_nm", prefixText));
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        bll.vSearchMstCust2(arr, ref rs);
        while (rs.Read())
        { 
            sCust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + "-" + rs["cust_nm"].ToString(), rs["cust_cd"].ToString());
            lcust.Add(sCust);
        }
        rs.Close();
        return lcust.ToArray();
    }
    protected void rditem_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (rditem.SelectedValue.ToString() == "I")
        { cbprod.Visible = false; txitem.Visible = true; }
        else { cbprod.Visible = true; txitem.Visible = false; arr.Add(new cArrayList("@level_no","3"));bll.vBindingComboToSp(ref cbprod, "sp_tmst_product_get", "prod_cd", "prod_nm", arr); }
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
            sitem = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["item_cd"].ToString() + "-" + rs["item_nm"].ToString() + "-" + rs["size"].ToString() + "-" + rs["branded_nm"].ToString(), rs["item_cd"].ToString());
            litem.Add(sitem);
        }
        rs.Close();
        return (litem.ToArray());
    }

    //[System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    //public static string[] GetCompletionList3(string prefixText, int count, string contextKey)
    //{
    //    List<cArrayList> arr = new List<cArrayList>();
    //    HttpCookie cok;
    //    cok = HttpContext.Current.Request.Cookies["sp"];
    //    List<string> litem = new List<string>();
    //    string sitem = string.Empty;
    //    System.Data.SqlClient.SqlDataReader rs = null;
    //    cbll bll = new cbll();
    //    arr.Add(new cArrayList("@item_cd", prefixText));
    //    arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
    //    bll.vSearchMstItemBySalespoint(arr, ref rs);
    //    while (rs.Read())
    //    {
    //        sitem = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["item_cd"].ToString() + "-" + rs["item_nm"].ToString() + "-" + rs["size"].ToString() + "-" + rs["branded_nm"].ToString(), rs["item_cd"].ToString());
    //        litem.Add(sitem);
    //    }
    //    rs.Close();
    //    return (litem.ToArray());
    //}
    protected void btaddfree_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (rdfreeitem.SelectedValue.ToString() == "I")
        {
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@item_cd", hditemfree.Value.ToString()));
            bll.vInsertContractFreeItem(arr); arr.Clear();
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grdfreeitem, "sp_tcontract_freeitem_get", arr);
        }
        else {
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@prod_cd", cbfreeprod.SelectedValue.ToString()));
            bll.vInsertContractFreeProduct(arr); arr.Clear();
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grdfreeitem, "sp_tcontract_freeproduct_get", arr);
        }
        //if (rdfreeitem.SelectedValue.ToString() == "I")
        //{ txfreeitem.Visible = true; cbfreeprod.Visible = false; }
        //else {
        //    txfreeitem.Visible = false; cbfreeprod.Visible = true; bll.vBindingComboToSp(ref cbfreeprod, "sp_tmst_product_get", "prod_cd", "prod_nm");
        //}
        
    }
    protected void rdfreeitem_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdfreeitem.SelectedValue.ToString() == "I")
        {
            txfreeitem.Visible = true; cbfreeprod.Visible = false;
        }
        else
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@level_no", "3"));
            txfreeitem.Visible = false; cbfreeprod.Visible = true;
            bll.vBindingComboToSp(ref cbfreeprod, "sp_tmst_product_get", "prod_cd", "prod_nm", arr);
        }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList3(string prefixText, int count, string contextKey)
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
            sitem = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["item_cd"].ToString() + "-" + rs["item_nm"].ToString() + "-" + rs["size"].ToString() + "-" + rs["branded_nm"].ToString(), rs["item_cd"].ToString());
            litem.Add(sitem);
        }
        rs.Close();
        return (litem.ToArray());
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_mstcontract2.aspx");
    }
    protected void grdcust_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbcustcode = (Label)grdcust.Rows[e.RowIndex].FindControl("lbcustcode");
       
        List<cArrayList> arr = new List<cArrayList>();
        if (rdcust.SelectedValue.ToString() == "C")
        {
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@cust_cd", lbcustcode.Text));
            bll.vDelContractCustomer(arr); arr.Clear();
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grdcust, "sp_tcontract_customer_get", arr);
        }
        else
        {
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@cusgrcd", lbcustcode.Text));
            bll.vDelContractCusgrcd(arr); arr.Clear();
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grdcust, "sp_tcontract_cusgrcd_get", arr);
        }
        
    }
    protected void grditem_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        Label lbitemcode = (Label)grditem.Rows[e.RowIndex].FindControl("lbitemcode");
        if (rditem.SelectedValue.ToString()=="I")
        {
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
            bll.vDelContractItem(arr); arr.Clear();
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grditem, "sp_tcontract_item_get", arr);
        }else{
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@prod_cd", lbitemcode.Text));
            bll.vDelContractProduct(arr); arr.Clear();
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grditem, "sp_tcontract_product_get", arr);
        }
    }
    protected void grdfreeitem_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        Label lbitemcode = (Label)grdfreeitem.Rows[e.RowIndex].FindControl("lbitemcode");
        if (rdfreeitem.SelectedValue.ToString() == "I")
        {
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
            bll.vDelContractFreeItem(arr); arr.Clear();
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grdfreeitem, "sp_tcontract_freeitem_get", arr);
        }
        else {
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@prod_cd", lbitemcode.Text));
            bll.vDelContractFreeProduct(arr); arr.Clear();
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grdfreeitem, "sp_tcontract_freeproduct_get", arr);
        }
    }
}