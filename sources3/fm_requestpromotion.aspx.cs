using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_requestpromotion : System.Web.UI.Page
{
    cbll bll = new cbll();

    
    protected void Page_Load(object sender, EventArgs e)
    {
        //Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        {
            Session["proposal"] = "";
            List<cArrayList> arr = new List<cArrayList>();
            //lbSp.Text = Request.Cookies["spn"].Value.ToString();
            lbpromotionno.Text = "NEW";
            dtstart.Text = System.DateTime.Today.AddDays(1).ToString("dd/MM/yyyy");
            dtend.Text = System.DateTime.Today.AddDays(7).ToString("dd/MM/yyyy");
            dtdelivery.Text = System.DateTime.Today.AddDays(1).ToString("dd/MM/yyyy");
            cbpromokind_SelectedIndexChanged(sender, e);
            rdcust_SelectedIndexChanged(sender, e);
            rditem_SelectedIndexChanged(sender, e);
            cbbgitem_SelectedIndexChanged(sender, e);
            //bll.vBindingFieldValueToCombo(ref cbpaymenttype, "rdpayment");

            bll.vBindingSalespointToCombo(ref cbsalespoint);
            cbsalespoint.Items.Insert(0, new ListItem("All Branch", "ALL"));
            cbsalespoint.SelectedValue = Request.Cookies["sp"].Value.ToString();
            
            cbpromokind.SelectedValue = "BTL";
            cbpromokind_SelectedIndexChanged(sender, e);

            btedit.Visible = false;
            btprint.Visible = false;
            btnew.Visible = true;
            btsave.Visible = true;
            viewProposal.Attributes.Add("style", "display:none");
            viewFreegood.Attributes.Add("style", "display:none");
            arr.Clear();
            arr.Add(new cArrayList("@promo_no", Request.Cookies["usr_id"].Value.ToString()));
            bll.vDelReqPromoCustomer(arr);
            bll.vDelReqPromoCusgrcd(arr);
            bll.vDelReqPromoCustType(arr);
            bll.vDelReqPromoItem(arr);
            bll.vDelReqPromoProduct(arr);
            bll.vDelReqPromoSalespoint(arr);
            bll.vDelReqPromoItemFree(arr);
            bll.vDelReqPromoProductFree(arr);
            rdFreegood_SelectedIndexChanged(sender, e);
            rdProposal_SelectedIndexChanged(sender, e);
            rdcust.SelectedValue = "C";
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
    }


    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    
    public static string[] GetListProposal(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lCust = new List<string>();
        string sCust = string.Empty;
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@prop_no", prefixText));
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        bll.vSearchProposalBySalespoint(arr, ref rs);
        while (rs.Read())
        {
            sCust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["prop_no"].ToString(), rs["prop_no"].ToString());
            lCust.Add(sCust);
        }
        rs.Close();
        return (lCust.ToArray());
    }


    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetListCustomer(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lCust = new List<string>();
        string sCust = string.Empty;
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cust_cd", prefixText));
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        arr.Add(new cArrayList("@prop_no", HttpContext.Current.Session["proposal"].ToString()));
        bll.vSearchMstCustomerPro(arr, ref rs);
        while (rs.Read())
        {
            sCust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + "-" + rs["cust_nm"].ToString(), rs["cust_cd"].ToString());
            lCust.Add(sCust);
        }
        rs.Close();
        return (lCust.ToArray());
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetListItem(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        List<string> lItem = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string sItem = string.Empty;
        System.Data.SqlClient.SqlDataReader rs = null;
        arr.Add(new cArrayList("@item_nm", prefixText));
        bll.vSearchMstItem2(arr, ref rs);
        while (rs.Read())
        {
            sItem = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["item_cd"].ToString() + "-" + rs["item_nm"].ToString() + "-" + rs["size"].ToString(), rs["item_cd"].ToString());
            lItem.Add(sItem);

        }
        rs.Close();
        return (lItem.ToArray());
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetListApproval(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lEmployee = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string sEmployee = string.Empty;
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        arr.Add(new cArrayList("@emp_nm", prefixText));
        arr.Add(new cArrayList("@job_title", "cashout_job"));
        bll.vSearchMstEmployee2bysalespoint(arr, ref rs);
        while (rs.Read())
        {
            sEmployee = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["emp_cd"].ToString() + "-" + rs["emp_nm"], rs["emp_cd"].ToString());
            lEmployee.Add(sEmployee);
        }
        return (lEmployee.ToArray());
    }
    protected void btsearchpromotion_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "popupwindow('lookupreqdiscount.aspx');", true);
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
    protected void rdcust_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();

        if (rdcust.SelectedValue.ToString() == "C")
        {
            cbcusgrcd.Attributes.Add("style", "display:none");
            txsearchcust.Attributes.Remove("style");
            grdcusgrcd.Visible = false;
            grdcust.Visible = true;
            grdcusttype.Visible = false;
            branch.Visible = false;
        }
        else if (rdcust.SelectedValue.ToString() == "G")
        {
            txsearchcust.Attributes.Add("style", "display:none");
            cbcusgrcd.Attributes.Remove("style");
            grdcusgrcd.Visible = true;
            grdcust.Visible = false;
            grdcusttype.Visible = false;
            bll.vBindingFieldValueToCombo(ref cbcusgrcd, "cusgrcd");
            branch.Visible = true;
        }
        else if (rdcust.SelectedValue.ToString() == "T")
        {
            txsearchcust.Attributes.Add("style", "display:none");
            cbcusgrcd.Attributes.Remove("style");
            grdcusgrcd.Visible = false;
            grdcust.Visible = false;
            grdcusttype.Visible = true;
            arr.Clear();
            arr.Add(new cArrayList("@fld_nm", "otlcd"));
            arr.Add(new cArrayList("@hiddendata", "0"));
            bll.vBindingFieldValueToCombo(ref cbcusgrcd, arr);
            branch.Visible = true;
        }
        arr.Clear();
        arr.Add(new cArrayList("@promo_no", Request.Cookies["usr_id"].Value.ToString()));
        bll.vDelReqPromoCustomer(arr);
        bll.vDelReqPromoCusgrcd(arr);
        bll.vDelReqPromoCustType(arr);
        bll.vDelReqPromoItem(arr);
        bll.vDelReqPromoProduct(arr);
    }
    protected void btaddcust_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (rdcust.SelectedValue.ToString() == "C")
        {
            if (hdpromo.Value.ToString() == "")
            {
                arr.Add(new cArrayList("@promo_no", Request.Cookies["usr_id"].Value.ToString()));
            }
            else
            {
                arr.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
            }
            arr.Add(new cArrayList("@salespoint_cd", Request.Cookies["sp"].Value.ToString()));            
            arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
            bll.vInsertReqPromoCustomer(arr); arr.Clear();
            if (hdpromo.Value.ToString() == "")
            {
                arr.Add(new cArrayList("@promo_no", Request.Cookies["usr_id"].Value.ToString()));
            }
            else
            {
                arr.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
            }
            bll.vBindingGridToSp(ref grdcust, "sp_treqdiscount_customer_get", arr);
            grdcust.Visible = true;
            grdcusgrcd.Visible = false;
            grdcusttype.Visible = false;
            txsearchcust.Text = "";
        }
        else if (rdcust.SelectedValue.ToString() == "G")
        {
            if (hdpromo.Value.ToString() == "")
            {
                arr.Add(new cArrayList("@promo_no", Request.Cookies["usr_id"].Value.ToString()));
            }
            else
            {
                arr.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
            }
            arr.Add(new cArrayList("@salespoint_cd", Request.Cookies["sp"].Value.ToString()));   
            arr.Add(new cArrayList("@cusgrcd", cbcusgrcd.SelectedValue.ToString()));
            bll.vInsertReqPromoCusgrcd(arr); arr.Clear();
            if (hdpromo.Value.ToString() == "")
            {
                arr.Add(new cArrayList("@promo_no", Request.Cookies["usr_id"].Value.ToString()));
            }
            else
            {
                arr.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
            }
            bll.vBindingGridToSp(ref grdcusgrcd, "sp_treqdiscount_cusgrcd_get", arr);
            grdcust.Visible = false;
            grdcusgrcd.Visible = true;
            grdcusttype.Visible = false;
        }
        else if (rdcust.SelectedValue.ToString() == "T")
        {
            if (hdpromo.Value.ToString() == "")
            {
                arr.Add(new cArrayList("@promo_no", Request.Cookies["usr_id"].Value.ToString()));
            }
            else
            {
                arr.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
            }
            arr.Add(new cArrayList("@salespoint_cd", Request.Cookies["sp"].Value.ToString()));   
            arr.Add(new cArrayList("@cust_typ", cbcusgrcd.SelectedValue.ToString()));
            bll.vInsertReqPromoCustType(arr); arr.Clear();
            if (hdpromo.Value.ToString() == "")
            {
                arr.Add(new cArrayList("@promo_no", Request.Cookies["usr_id"].Value.ToString()));
            }
            else
            {
                arr.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
            }
            bll.vBindingGridToSp(ref grdcusttype, "sp_treqdiscount_custtype_get", arr);
            grdcust.Visible = false;
            grdcusgrcd.Visible = false;
            grdcusttype.Visible = true;
        }
    }
    protected void grdcust_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbcustcode;
        List<cArrayList> arr = new List<cArrayList>();
        lbcustcode = (Label)grdcust.Rows[e.RowIndex].FindControl("lbcustcode");
        if (hdpromo.Value.ToString() == "")
        {
            arr.Add(new cArrayList("@promo_no", Request.Cookies["usr_id"].Value.ToString()));
        }
        else
        {
            arr.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
        }
        arr.Add(new cArrayList("@cust_cd", lbcustcode.Text));
        bll.vDelReqPromoCustomer(arr);
        arr.Clear();
        if (hdpromo.Value.ToString() == "")
        {
            arr.Add(new cArrayList("@promo_no", Request.Cookies["usr_id"].Value.ToString()));
        }
        else
        {
            arr.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
        }
        bll.vBindingGridToSp(ref grdcust, "sp_treqdiscount_customer_get", arr);
    }
    protected void grdcust_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void grdcusgrcd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbcusgrcd;
        List<cArrayList> arr = new List<cArrayList>();
        lbcusgrcd = (Label)grdcusgrcd.Rows[e.RowIndex].FindControl("lbcusgrcd");
        if (hdpromo.Value.ToString() == "")
        {
            arr.Add(new cArrayList("@promo_no", Request.Cookies["usr_id"].Value.ToString()));
        }
        else
        {
            arr.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
        }
        arr.Add(new cArrayList("@cusgrcd", lbcusgrcd.Text));
        bll.vDelReqPromoCusgrcd(arr);
        arr.Clear();
        if (hdpromo.Value.ToString() == "")
        {
            arr.Add(new cArrayList("@promo_no", Request.Cookies["usr_id"].Value.ToString()));
        }
        else
        {
            arr.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
        }
        bll.vBindingGridToSp(ref grdcusgrcd, "sp_treqdiscount_cusgrcd_get", arr);
    }
    protected void grdcusttype_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbcusttype;
        List<cArrayList> arr = new List<cArrayList>();
        lbcusttype = (Label)grdcusttype.Rows[e.RowIndex].FindControl("lbcusttype");
        if (hdpromo.Value.ToString() == "")
        {
            arr.Add(new cArrayList("@promo_no", Request.Cookies["usr_id"].Value.ToString()));
        }
        else
        {
            arr.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
        }
        arr.Add(new cArrayList("@otlcd", lbcusttype.Text));
        bll.vDelReqPromoCustType(arr);
        arr.Clear();
        if (hdpromo.Value.ToString() == "")
        {
            arr.Add(new cArrayList("@promo_no", Request.Cookies["usr_id"].Value.ToString()));
        }
        else
        {
            arr.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
        }
        bll.vBindingGridToSp(ref grdcusttype, "sp_treqdiscount_custtype_get", arr);
    }
    protected void rditem_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (rditem.SelectedValue.ToString() == "I")
        {

            cbgroup.Attributes.Add("style", "display:none");
            txsearchitem.Attributes.Remove("style");
            cbbgitem.Attributes.Remove("style");
            grdgroup.Visible = false;
            grditem.Visible = true;

        }
        else if (rditem.SelectedValue.ToString() == "G")
        {
            txsearchitem.Attributes.Add("style", "display:none");
            cbgroup.Attributes.Remove("style");
            cbbgitem.Attributes.Remove("style");
            grdgroup.Visible = true;
            grditem.Visible = false;
            arr.Add(new cArrayList("@level_no", "3"));
            bll.vBindingComboToSp(ref cbgroup, "sp_tmst_product_get", "prod_cd", "prod_nm", arr);
        }
    }

    protected void cbbgitem_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbbgitem.SelectedValue.ToString() == "A")
        {

            bga.Attributes.Remove("style");
            bgb.Attributes.Add("style","display:none");
            bgc.Attributes.Add("style", "display:none");
            bgd.Attributes.Add("style", "display:none");
            bge.Attributes.Add("style", "display:none");
        }
        else if (cbbgitem.SelectedValue.ToString() == "B")
        {
            bgb.Attributes.Remove("style");
            bga.Attributes.Add("style", "display:none");
            bgc.Attributes.Add("style", "display:none");
            bgd.Attributes.Add("style", "display:none");
            bge.Attributes.Add("style", "display:none");
        }        
        else if (cbbgitem.SelectedValue.ToString() == "C")
        {
            bgc.Attributes.Remove("style");
            bgb.Attributes.Add("style", "display:none");
            bga.Attributes.Add("style", "display:none");
            bgd.Attributes.Add("style", "display:none");
            bge.Attributes.Add("style", "display:none");
        }
        else if (cbbgitem.SelectedValue.ToString() == "D")
        {
            bgd.Attributes.Remove("style");
            bgb.Attributes.Add("style", "display:none");
            bgc.Attributes.Add("style", "display:none");
            bga.Attributes.Add("style", "display:none");
            bge.Attributes.Add("style", "display:none");
        }
        else
        {
            bge.Attributes.Remove("style");
            bgb.Attributes.Add("style", "display:none");
            bgc.Attributes.Add("style", "display:none");
            bgd.Attributes.Add("style", "display:none");
            bga.Attributes.Add("style", "display:none");
        }
    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        string statusPrice = "";
        List<cArrayList> arr = new List<cArrayList>();
        if (rditem.SelectedValue.ToString() == "I")
        {
            if (cbbgitem.SelectedValue.ToString() == "A")
            {
                arr.Add(new cArrayList("@x", txbgitemax.Text.ToString()));
                arr.Add(new cArrayList("@y", txbgitemay.Text.ToString()));
                arr.Add(new cArrayList("@target", txbgitemalimit.Text.ToString()));
            }
            else if (cbbgitem.SelectedValue.ToString() == "B")
            {
                arr.Add(new cArrayList("@x", txbgitembx.Text.ToString()));
                arr.Add(new cArrayList("@y", txbgitemby.Text.ToString()));
                arr.Add(new cArrayList("@target", cbbgitembtarget.SelectedValue.ToString()));
            }
            else if (cbbgitem.SelectedValue.ToString() == "C")
            {
                arr.Add(new cArrayList("@x", txbgitemcx.Text.ToString()));
                arr.Add(new cArrayList("@y", txbgitemcy.Text.ToString()));
                arr.Add(new cArrayList("@target", ""));
            }
            else if (cbbgitem.SelectedValue.ToString() == "D")
            {
                arr.Add(new cArrayList("@x", txbgitemdx.Text.ToString()));
                arr.Add(new cArrayList("@y", "0"));
                arr.Add(new cArrayList("@target", ""));
            }
            else
            {
                arr.Add(new cArrayList("@x", txbgitemex.Text.ToString()));
                arr.Add(new cArrayList("@y", "0"));
                arr.Add(new cArrayList("@target", ""));
            }
            arr.Add(new cArrayList("@item_cd", hditem.Value.ToString()));
            arr.Add(new cArrayList("@item_bg", cbbgitem.SelectedValue.ToString()));
            if (hdpromo.Value.ToString() == "")
            {
                arr.Add(new cArrayList("@promo_no", Request.Cookies["usr_id"].Value.ToString()));
            }
            else
            {
                arr.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
            }
            arr.Add(new cArrayList("@salespoint_cd", Request.Cookies["sp"].Value.ToString()));
            statusPrice = bll.vLookUp("exec sp_treqdiscount_item_get_total ''");
            bll.vInsertReqPromoItem(arr); arr.Clear();
            if (hdpromo.Value.ToString() == "")
            {
                arr.Add(new cArrayList("@promo_no", Request.Cookies["usr_id"].Value.ToString()));
            }
            else
            {
                arr.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
            }
            arr.Add(new cArrayList("@customer", rdcust.SelectedValue.ToString()));
            bll.vBindingGridToSp(ref grditem, "sp_treqdiscount_item_get", arr);
            grditem.Visible = true;
            grdgroup.Visible = false;            
        }
        else
        {
            if (cbbgitem.SelectedValue.ToString() == "A")
            {
                arr.Add(new cArrayList("@x", txbgitemax.Text.ToString()));
                arr.Add(new cArrayList("@y", txbgitemay.Text.ToString()));
                arr.Add(new cArrayList("@target", txbgitemalimit.Text.ToString()));
            }
            else if (cbbgitem.SelectedValue.ToString() == "B")
            {
                arr.Add(new cArrayList("@x", txbgitembx.Text.ToString()));
                arr.Add(new cArrayList("@y", txbgitemby.Text.ToString()));
                arr.Add(new cArrayList("@target", cbbgitembtarget.SelectedValue.ToString()));
            }
            else if (cbbgitem.SelectedValue.ToString() == "C")
            {
                arr.Add(new cArrayList("@x", txbgitemcx.Text.ToString()));
                arr.Add(new cArrayList("@y", txbgitemcy.Text.ToString()));
                arr.Add(new cArrayList("@target", ""));
            }
            else if (cbbgitem.SelectedValue.ToString() == "D")
            {
                arr.Add(new cArrayList("@x", txbgitemdx.Text.ToString()));
                arr.Add(new cArrayList("@y", "0"));
                arr.Add(new cArrayList("@target", ""));
            }
            else
            {
                arr.Add(new cArrayList("@x", txbgitemex.Text.ToString()));
                arr.Add(new cArrayList("@y", "0"));
                arr.Add(new cArrayList("@target", ""));
            }
            arr.Add(new cArrayList("@prod_cd", cbgroup.SelectedValue.ToString()));
            arr.Add(new cArrayList("@prod_bg", cbbgitem.SelectedValue.ToString()));
            if (hdpromo.Value.ToString() == "")
            {
                arr.Add(new cArrayList("@promo_no", Request.Cookies["usr_id"].Value.ToString()));
            }
            else
            {
                arr.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
            }
            arr.Add(new cArrayList("@salespoint_cd", Request.Cookies["sp"].Value.ToString()));
            bll.vInsertReqPromoProduct(arr); arr.Clear();
            if (hdpromo.Value.ToString() == "")
            {
                arr.Add(new cArrayList("@promo_no", Request.Cookies["usr_id"].Value.ToString()));
            }
            else
            {
                arr.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
            }
            arr.Add(new cArrayList("@customer", rdcust.SelectedValue.ToString()));
            bll.vBindingGridToSp(ref grdgroup, "sp_treqdiscount_product_get", arr);
            grditem.Visible = false;
            grdgroup.Visible = true;
        }

        if (cbpaymenttype.SelectedValue.ToString() == "CH")
        {
            int multiprice = 0;
            if (rditem.SelectedValue.ToString() == "I")
            {
                multiprice = int.Parse(bll.vLookUp("exec sp_treqdiscount_item_price '" + Request.Cookies["usr_id"].Value.ToString() + "','"+rdcust.SelectedValue.ToString()+"'"));
            }
            else
            {
                multiprice = int.Parse(bll.vLookUp("exec sp_treqdiscount_product_price '" + Request.Cookies["usr_id"].Value.ToString() + "','"+rdcust.SelectedValue.ToString()+"'"));
            }

            if (multiprice > 1)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Price only one type, delete another item or product.','Product / Item','warning');", true);
                return;
            }
            
        }

        txsearchitem.Text = "";
        txbgitemax.Text = "";
        txbgitemay.Text = "";
        txbgitemalimit.Text = "";
        txbgitembx.Text = "";
        txbgitemby.Text = "";
        txbgitemcx.Text = "";
        txbgitemcy.Text = "";
        txbgitemdx.Text = "";
        txbgitemex.Text = "";
    }
    protected void grditem_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbitemcode;
        List<cArrayList> arr = new List<cArrayList>();
        lbitemcode = (Label)grditem.Rows[e.RowIndex].FindControl("lbitemcode");
        if (hdpromo.Value.ToString() == "")
        {            
            arr.Add(new cArrayList("@promo_no", Request.Cookies["usr_id"].Value.ToString()));
        }
        else
        {
            arr.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
        }
        arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
        bll.vDelReqPromoItem(arr);
        arr.Clear();
        if (hdpromo.Value.ToString() == "")
        {
            arr.Add(new cArrayList("@promo_no", Request.Cookies["usr_id"].Value.ToString()));
        }
        else
        {
            arr.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
        }
        bll.vBindingGridToSp(ref grditem, "sp_treqdiscount_item_get", arr);
    }
    protected void grdgroup_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbgroupcode;
        List<cArrayList> arr = new List<cArrayList>();
        lbgroupcode = (Label)grdgroup.Rows[e.RowIndex].FindControl("lbgroupcode");
        if (hdpromo.Value.ToString() == "")
        {
            arr.Add(new cArrayList("@promo_no", Request.Cookies["usr_id"].Value.ToString()));
        }
        else
        {
            arr.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
        }
        arr.Add(new cArrayList("@prod_cd", lbgroupcode.Text));
        bll.vDelReqPromoProduct(arr);
        arr.Clear();
        if (hdpromo.Value.ToString() == "")
        {
            arr.Add(new cArrayList("@promo_no", Request.Cookies["usr_id"].Value.ToString()));
        }
        else
        {
            arr.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
        }
        bll.vBindingGridToSp(ref grdgroup, "sp_treqdiscount_product_get", arr);
    }
    protected void cbpaymenttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbpaymenttype.SelectedValue.ToString() == "FG")
        {
            lbsar.Text = "CTN";
        }
        else if (cbpaymenttype.SelectedValue.ToString() == "CH")
        {
            lbsar.Text = "CTN";
        }
        else if (cbpaymenttype.SelectedValue.ToString() == "CR")
        {
            lbsar.Text = "CTN";
        }
        else if (cbpaymenttype.SelectedValue.ToString() == "CN")
        {
            lbsar.Text = "CTN";
        }
    }
    protected void btnew_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("fm_requestpromotion.aspx");
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {

            if (rdcust.SelectedValue.ToString() == "C")
            {
                if (grdcust.Rows.Count <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Plase add customer.','Customer','warning');", true);
                    return;
                }
            }
            else if (rdcust.SelectedValue.ToString() == "G")
            {
                if (grdcusgrcd.Rows.Count <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Plase add group customer.','Customer','warning');", true);
                    return;
                }
                if (grdsalespoint.Rows.Count <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Plase add branch.','branch','warning');", true);
                    return;
                }
            }
            else if (rdcust.SelectedValue.ToString() == "T")
            {
                if (grdcusttype.Rows.Count <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Plase add type customer.','Customer','warning');", true);
                    return;
                }
                if (grdsalespoint.Rows.Count <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Plase add branch.','branch','warning');", true);
                    return;
                }
            }


            if (cbpaymenttype.SelectedValue.ToString() == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please Select Payment Type.','Payment Type','warning');", true);
                return;
            }

            if (txcbp.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please Input CBP Price.','CBP','warning');", true);
                return;
            }

            if (hdemp.Value.ToString() == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please Input Approval By Employee.','Approval By','warning');", true);
                return;
            }

            if (cbpaymenttype.SelectedValue.ToString() == "CH")
            {
                int multiprice = 0;
                if (rditem.SelectedValue.ToString() == "I")
                {
                    multiprice = int.Parse(bll.vLookUp("exec sp_treqdiscount_item_price '" + Request.Cookies["usr_id"].Value.ToString() + "','" + rdcust.SelectedValue.ToString() + "'"));
                }
                else
                {
                    multiprice = int.Parse(bll.vLookUp("exec sp_treqdiscount_product_price '" + Request.Cookies["usr_id"].Value.ToString() + "','" + rdcust.SelectedValue.ToString() + "'"));
                }

                if (multiprice > 1)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Price only one type, delete another item or product.','Product / Item','warning');", true);
                    return;
                }

            }

            if (rditem.SelectedValue.ToString() == "I")
            {
                if (grditem.Rows.Count <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Plase add item / Product.','Customer','warning');", true);
                    return;
                }
            }
            else if (rditem.SelectedValue.ToString() == "G")
            {
                if (grdgroup.Rows.Count <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Plase add item / Product.','Customer','warning');", true);
                    return;
                }
            }

            btsave.Visible = false;
            System.Data.SqlClient.SqlDataReader rs = null;
            List<cArrayList> arr = new List<cArrayList>();
            string sPromoNo = "";
            arr.Add(new cArrayList("@sys", "reqdiscount"));
            arr.Add(new cArrayList("@sysno", ""));
            bll.vGetDiscountNo(arr, ref rs);
            while (rs.Read())
            {
                sPromoNo = rs["generated"].ToString();
            }
            arr.Clear();
            arr.Add(new cArrayList("@promo_no", sPromoNo));
            arr.Add(new cArrayList("@salespoint_cd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@start_dt", DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@end_dt", DateTime.ParseExact(dtend.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@delivery_dt", DateTime.ParseExact(dtdelivery.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@rdcustomer", rdcust.SelectedValue.ToString()));
            arr.Add(new cArrayList("@rditem", rditem.SelectedValue.ToString()));
            arr.Add(new cArrayList("@promo_cd", cbpromogroup.SelectedValue.ToString()));
            arr.Add(new cArrayList("@promo_typ", cbpromotype.SelectedValue.ToString()));
            arr.Add(new cArrayList("@rdpayment", cbpaymenttype.SelectedValue.ToString()));
            arr.Add(new cArrayList("@budget", txbudget.Text));
            arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@disc_sta_id", "N"));
            arr.Add(new cArrayList("@rdproposal", rdProposal.SelectedValue.ToString()));
            arr.Add(new cArrayList("@rdfreegood", rdFreegood.SelectedValue.ToString()));
            arr.Add(new cArrayList("@prop_no", txproposal.Text));
            arr.Add(new cArrayList("@rditemfree", rditemfree.SelectedValue.ToString()));
            arr.Add(new cArrayList("@remark", txremark.Text));
            arr.Add(new cArrayList("@promotion", rdpromotion.SelectedValue.ToString()));
            arr.Add(new cArrayList("@cbp", txcbp.Text));
            arr.Add(new cArrayList("@cbp_uom", cbp_uom.SelectedValue.ToString()));
            arr.Add(new cArrayList("@app_by", hdemp.Value.ToString()));
            if (cbpaymenttype.SelectedValue.ToString() == "FG")
            {
                arr.Add(new cArrayList("@regcost", 0.0));
                arr.Add(new cArrayList("@netcost", 0.0));
            }
            else
            {
                if (rditem.SelectedValue.ToString() == "I")
                {
                    arr.Add(new cArrayList("@regcost", double.Parse(bll.vLookUp("exec sp_treqdiscount_item_pricecost '" + Request.Cookies["usr_id"].Value.ToString() + "','" + rdcust.SelectedValue.ToString() + "','reg'"))));
                    arr.Add(new cArrayList("@netcost", double.Parse(bll.vLookUp("exec sp_treqdiscount_item_pricecost '" + Request.Cookies["usr_id"].Value.ToString() + "','" + rdcust.SelectedValue.ToString() + "','net'"))));
                }
                else
                {
                    arr.Add(new cArrayList("@regcost", double.Parse(bll.vLookUp("exec sp_treqdiscount_product_pricecost '" + Request.Cookies["usr_id"].Value.ToString() + "','" + rdcust.SelectedValue.ToString() + "','reg'"))));
                    arr.Add(new cArrayList("@netcost", double.Parse(bll.vLookUp("exec sp_treqdiscount_product_pricecost '" + Request.Cookies["usr_id"].Value.ToString() + "','" + rdcust.SelectedValue.ToString() + "','net'"))));
                }                
            }

            bll.vInsertMstReqPromo(arr);
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@promo_no", sPromoNo));
            bll.vUpdateMstReqPromoNo(arr);
            lbpromotionno.Text = sPromoNo;

            if (rdcust.SelectedValue.ToString() == "C")
            {
                arr.Clear();
                arr.Add(new cArrayList("@promo_no", sPromoNo));
                arr.Add(new cArrayList("@salespoint_cd", Request.Cookies["sp"].Value.ToString()));
                bll.vInsertReqPromoSalespoint(arr);
            }

            // Upload Document 
            if (FileUploadControl.HasFile)
            {
                try
                {
                    FileInfo fi = new FileInfo(Path.GetFileName(FileUploadControl.FileName));
                    string ext = fi.Extension;
                    string filename = Path.GetFileName(FileUploadControl.FileName);
                    FileUploadControl.SaveAs(bll.sGetControlParameter("image_path") + "/request_discount/" + sPromoNo + ext);
                    arr.Clear();
                    hpfile_nm.Visible = true;
                    FileUploadControl.Visible = false;
                    lblocfile.Text = sPromoNo + ext;
                    hpfile_nm.NavigateUrl = "/images/request_discount/" + sPromoNo + ext;
                }
                catch (Exception ex)
                {
                    lblocfile.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                }
            }
            


            // Sent Email into Product Supervisor and Claimer 

            List<string> lapproval = bll.lGetApproval("reqdiscount", 1);

            string sSubject = ""; string sMessage = ""; string sItem = "";

            if (rditem.SelectedValue.ToString() == "I")
            {
                sItem = bll.vLookUp("SELECT STUFF((SELECT ';' + t1.item_cd FROM (select item_cd from treqdiscount_item where disc_cd='" + sPromoNo.ToString() + "') as t1 FOR XML PATH('')),1,1,'') as item");
            }
            else
            {
                sItem = bll.vLookUp("SELECT STUFF((SELECT ';' + t1.prod_cd FROM (select prod_cd from treqdiscount_product where disc_cd='" + sPromoNo.ToString() + "') as t1 FOR XML PATH('')),1,1,'') as item");
            }

            sSubject = "Request Discount (" + sPromoNo.ToString() + ") Branch " + bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");

            sMessage = "Request Discount No. " + sPromoNo.ToString() +
                ", \n Branch : " + bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") +
                ",\n Request By : " + Request.Cookies["usr_id"].Value.ToString() +
                ",\n Proposal No : " + txproposal.Text +
                ",\n Promotion : " + cbpromokind.SelectedValue.ToString() + " - " + cbpromotype.SelectedValue.ToString() + " - " + bll.vLookUp("select promotyp_nm from tpromotion_dtl where promo_cd='" + cbpromogroup.SelectedValue.ToString() + "' and promo_typ='" + cbpromotype.SelectedValue.ToString() + "'") +
                ",\n Start Date : " + dtstart.Text + " , End Date : " + dtend.Text +
                ",\n Item / Product : " + sItem.ToString() +
                " \n\r";

            string semail = "0"; string semail2 = "";
            int i = 0;
            foreach (var email in lapproval.Skip(1))
            {
                i = i + 1;
                if (i % 2 != 0)
                {
                    if (semail == "0")
                        semail = email;
                    else
                        semail = semail + ';' + email;
                }
            }

            if (rditem.SelectedValue.ToString() == "I")
            {
                semail2 = bll.vLookUp("SELECT STUFF((SELECT ';' + t1.email FROM (select top 3 a.prod_cd,a.supervisor_cd,b.mobile_no,b.email from tmst_product a inner join tuser_profile b on a.supervisor_cd=b.emp_cd where level_no=3 and prod_cd in (select x.prod_cd from tmst_item x inner join treqdiscount_item y on x.item_cd=y.item_cd where y.disc_cd='" + sPromoNo.ToString() + "')) as t1 FOR XML PATH('')),1,1,'') as email");
                semail = semail + ";" + semail2;

            }
            else
            {
                semail2 = bll.vLookUp("SELECT STUFF((SELECT ';' + t1.email FROM (select top 3 a.prod_cd,a.supervisor_cd,b.mobile_no,b.email from tmst_product a inner join tuser_profile b on a.supervisor_cd=b.emp_cd where level_no=3 and prod_cd in (select x.prod_cd from treqdiscount_product x where x.disc_cd='" + sPromoNo.ToString() + "')) as t1 FOR XML PATH('')),1,1,'') as email");
                semail = semail + ";" + semail2;
            }

            arr.Clear();
            arr.Add(new cArrayList("@token", 0));
            arr.Add(new cArrayList("@doc_typ", "ReqDiscount"));
            arr.Add(new cArrayList("@to", semail));
            arr.Add(new cArrayList("@doc_no", sPromoNo.ToString()));
            arr.Add(new cArrayList("@emailsubject", sSubject));
            arr.Add(new cArrayList("@msg", sMessage));
            arr.Add(new cArrayList("@file_attachment", null));
            bll.vInsertEmailOutbox(arr);

            btprint.Visible = true;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Request Discount(Scheme) Saved.','" + sPromoNo + "','success');", true);
            return;
        
        }
        catch(Exception ex)
        {
             bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : Add Request Promotion");
        }
        
    }
    protected void btprint_ServerClick(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=reqdiscount&disc=" + lbpromotionno.Text.ToString() + "&type="+rdpromotion.SelectedValue.ToString()+"');", true);
    }
    protected void btedit_ServerClick(object sender, EventArgs e)
    {

    }
    protected void btdiscount_Click(object sender, EventArgs e)
    {
        btsave.Visible = false;
        btnew.Visible = true;
        btprint.Visible = true;
        btedit.Visible = false;
        lbpromotionno.Text = hdpromo.Value;
        List<cArrayList> arr = new List<cArrayList>();
        System.Data.SqlClient.SqlDataReader rs = null;
        arr.Add(new cArrayList("@promo_no", hdpromo.Value));
        arr.Add(new cArrayList("@status", ""));
        bll.vGetReqPromo(ref rs, arr);
        while (rs.Read())
        {   
            dtstart.Text = Convert.ToDateTime(rs["start_dt"]).ToString("d/M/yyyy");
            dtend.Text = Convert.ToDateTime(rs["end_dt"]).ToString("d/M/yyyy");
            dtdelivery.Text = Convert.ToDateTime(rs["delivery_dt"]).ToString("d/M/yyyy");
            rdpromotion.SelectedValue = rs["deal"].ToString();
            cbpromokind.SelectedValue = rs["promokind"].ToString();
            cbpromokind_SelectedIndexChanged(sender, e);
            cbpromogroup.SelectedValue = rs["promo_cd"].ToString();
            cbpromogroup_SelectedIndexChanged(sender, e);
            cbpromotype.SelectedValue = rs["promo_typ"].ToString();
            rdcust.SelectedValue = rs["rdcustomer"].ToString();
            rditem.SelectedValue = rs["rditem"].ToString();
            cbpaymenttype.SelectedValue = rs["rdpayment"].ToString();
            txbudget.Text = rs["budget"].ToString();
            lbtitle.Text = "Request Promotion (Entry), Status : " + rs["status"].ToString();
            rdProposal.SelectedValue = rs["rdproposal"].ToString();
            rdFreegood.SelectedValue = rs["rdfreegood"].ToString();
            txproposal.Text = rs["prop_no"].ToString();
            rditemfree.SelectedValue = rs["rditemfree"].ToString();
            txremark.Text = rs["remark"].ToString();
            txcbp.Text = rs["cbp"].ToString();
            cbp_uom.SelectedValue = rs["cbp_uom"].ToString();
            txapproval.Text = rs["app_by"].ToString() + " - " + rs["app_nm"].ToString();
            txnoted.Text = rs["noted"].ToString();
        }
        rs.Close();
        rdcust_SelectedIndexChanged(sender, e);
        rditem_SelectedIndexChanged(sender, e);
        rdFreegood_SelectedIndexChanged(sender, e);
        arr.Clear();
        if (rdcust.SelectedValue.ToString() == "C")
        {
            if (hdpromo.Value.ToString() == "")
            {
                arr.Add(new cArrayList("@promo_no", Request.Cookies["usr_id"].Value.ToString()));
            }
            else
            {
                arr.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
            }
            bll.vBindingGridToSp(ref grdcust, "sp_treqdiscount_customer_get", arr);
            grdcust.Visible = true;
            grdcusgrcd.Visible = false;
            grdcusttype.Visible = false;
            txsearchcust.Text = "";
        }
        else if (rdcust.SelectedValue.ToString() == "G")
        {
            if (hdpromo.Value.ToString() == "")
            {
                arr.Add(new cArrayList("@promo_no", Request.Cookies["usr_id"].Value.ToString()));
            }
            else
            {
                arr.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
            }
            bll.vBindingGridToSp(ref grdcusgrcd, "sp_treqdiscount_cusgrcd_get", arr);
            grdcust.Visible = false;
            grdcusgrcd.Visible = true;
            grdcusttype.Visible = false;
        }
        else if (rdcust.SelectedValue.ToString() == "T")
        {
            if (hdpromo.Value.ToString() == "")
            {
                arr.Add(new cArrayList("@promo_no", Request.Cookies["usr_id"].Value.ToString()));
            }
            else
            {
                arr.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
            }
            bll.vBindingGridToSp(ref grdcusttype, "sp_treqdiscount_custtype_get", arr);
            grdcust.Visible = false;
            grdcusgrcd.Visible = false;
            grdcusttype.Visible = true;
        }
        arr.Clear();
        if (rditem.SelectedValue.ToString() == "I")
        {
            if (hdpromo.Value.ToString() == "")
            {
                arr.Add(new cArrayList("@promo_no", Request.Cookies["usr_id"].Value.ToString()));
            }
            else
            {
                arr.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
            }
            arr.Add(new cArrayList("@customer", rdcust.SelectedValue.ToString()));
            bll.vBindingGridToSp(ref grditem, "sp_treqdiscount_item_get", arr);
            grditem.Visible = true;
            grdgroup.Visible = false;
        }
        else
        {
            if (hdpromo.Value.ToString() == "")
            {
                arr.Add(new cArrayList("@promo_no", Request.Cookies["usr_id"].Value.ToString()));
            }
            else
            {
                arr.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
            }
            arr.Add(new cArrayList("@customer", rdcust.SelectedValue.ToString()));
            bll.vBindingGridToSp(ref grdgroup, "sp_treqdiscount_product_get", arr);
            grditem.Visible = false;
            grdgroup.Visible = true;
        }
        arr.Clear();
        if (rdFreegood.SelectedValue.ToString() == "Y")
        {
            if (rditemfree.SelectedValue.ToString() == "I")
            {
                if (hdpromo.Value.ToString() == "")
                {
                    arr.Add(new cArrayList("@promo_no", Request.Cookies["usr_id"].Value.ToString()));
                }
                else
                {
                    arr.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
                }
                bll.vBindingGridToSp(ref grditemfree, "sp_treqdiscount_itemfree_get", arr);
                grditemfree.Visible = true;
                grdgroupfree.Visible = false;
            }
            else
            {
                if (hdpromo.Value.ToString() == "")
                {
                    arr.Add(new cArrayList("@promo_no", Request.Cookies["usr_id"].Value.ToString()));
                }
                else
                {
                    arr.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
                }
                bll.vBindingGridToSp(ref grdgroupfree, "sp_treqdiscount_productfree_get", arr);
                grditemfree.Visible = false;
                grdgroupfree.Visible = true;
            }
        }
        string sfile_nm = bll.vLookUp("select fileloc from treqdiscount_doc where disc_cd ='" + hdpromo.Value.ToString() + "'");
        if (sfile_nm != "")
        {
            hpfile_nm.Visible = true;
            FileUploadControl.Visible = false;
            lblocfile.Text = sfile_nm;
            hpfile_nm.NavigateUrl = "/images/request_discount/" + sfile_nm;
        }
        
    }
    //protected void chproposal_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (chproposal.Checked)
    //    {
    //        viewProposal.Attributes.Remove("style");
    //    }
    //    else
    //    {
    //        viewProposal.Attributes.Add("style","display:none");
    //    }
        
    //}
    protected void rdProposal_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdProposal.SelectedValue.ToString() == "Y")
        {
            viewProposal.Attributes.Remove("style");
            txproposal.CssClass = "form-control input-sm ";
            txproposal.Enabled = true;
        }
        else
        {
            viewProposal.Attributes.Add("style", "display:none");
        }
    }
    protected void btproposal_Click(object sender, EventArgs e)
    {
        Session["proposal"] = hdproposal.Value.ToString();
        System.Data.SqlClient.SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        List<cArrayList> arr1 = new List<cArrayList>();
        if (hdpromo.Value.ToString() == "")
        {
            arr1.Add(new cArrayList("@promo_no", Request.Cookies["usr_id"].Value.ToString()));
        }
        else
        {
            arr1.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
        }
        arr.Add(new cArrayList("@prop_no", hdproposal.Value.ToString()));
        bll.vGetProposal(arr, ref rs);
        while (rs.Read())
        {
            cbpromokind.SelectedValue = rs["promokind"].ToString();
            cbpromokind_SelectedIndexChanged(sender, e);
            cbpromogroup.SelectedValue = rs["promo_cd"].ToString();
            cbpromogroup_SelectedIndexChanged(sender, e);
            cbpromotype.SelectedValue = rs["promo_typ"].ToString();
            rdcust.SelectedValue = rs["rdcust"].ToString();
            rditem.SelectedValue = rs["rditem"].ToString();
            cbpaymenttype.SelectedValue = rs["rdpayment"].ToString();
            if (rs["rdcust"].ToString() == "C")
            {
                arr1.Add(new cArrayList("@prop_no", hdproposal.Value.ToString()));
                arr1.Add(new cArrayList("@salespoint_cd", Request.Cookies["sp"].Value.ToString()));
                bll.vInsertReqPromoCustomer(arr1); arr1.Clear();
                if (hdpromo.Value.ToString() == "")
                {
                    arr1.Add(new cArrayList("@promo_no", Request.Cookies["usr_id"].Value.ToString()));
                }
                else
                {
                    arr1.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
                }
                bll.vBindingGridToSp(ref grdcust, "sp_treqdiscount_customer_get", arr1);
                //bll.vBindingGridToSp(ref grdcust, "sp_tproposal_customer_get2", arr);
                grdcust.Visible = true;
                grdcusgrcd.Visible = false;
                grdcusttype.Visible = false;
            }else{
                rdcust.SelectedValue = "C";
                grdcust.Visible = true;
                grdcusgrcd.Visible = false;
                grdcusttype.Visible = false;
            }
            //else if (rs["rdcust"].ToString() == "G")
            //{
            //    arr1.Add(new cArrayList("@prop_no", hdproposal.Value.ToString()));
            //    arr1.Add(new cArrayList("@salespoint_cd", Request.Cookies["sp"].Value.ToString()));
            //    bll.vInsertReqPromoCusgrcd(arr1); arr1.Clear();
            //    if (hdpromo.Value.ToString() == "")
            //    {
            //        arr1.Add(new cArrayList("@promo_no", Request.Cookies["usr_id"].Value.ToString()));
            //    }
            //    else
            //    {
            //        arr1.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
            //    }
            //    bll.vBindingGridToSp(ref grdcusgrcd, "sp_treqdiscount_cusgrcd_get", arr1);
            //    //bll.vBindingGridToSp(ref grdcusgrcd, "sp_tproposal_cusgrcd_get2", arr);
            //    grdcust.Visible = false;
            //    grdcusgrcd.Visible = true;
            //    grdcusttype.Visible = false;
            //}
            //else if (rs["rdcust"].ToString() == "T")
            //{
            //    arr1.Add(new cArrayList("@prop_no", hdproposal.Value.ToString()));
            //    arr1.Add(new cArrayList("@salespoint_cd", Request.Cookies["sp"].Value.ToString()));
            //    bll.vInsertReqPromoCustType(arr1); arr1.Clear();
            //    if (hdpromo.Value.ToString() == "")
            //    {
            //        arr1.Add(new cArrayList("@promo_no", Request.Cookies["usr_id"].Value.ToString()));
            //    }
            //    else
            //    {
            //        arr1.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
            //    }
            //    //arr1.Add(new cArrayList("@prop_no", hdproposal.Value.ToString()));
            //    bll.vBindingGridToSp(ref grdcusttype, "sp_treqdiscount_custtype_get", arr1);
            //    //bll.vBindingGridToSp(ref grdcusttype, "sp_tproposal_custtype_get2", arr);
            //    grdcust.Visible = false;
            //    grdcusgrcd.Visible = false;
            //    grdcusttype.Visible = true;
            //}
            
            if (rs["rditem"].ToString() == "I")
            {
                arr1.Add(new cArrayList("@prop_no", hdproposal.Value.ToString()));
                arr1.Add(new cArrayList("@salespoint_cd", Request.Cookies["sp"].Value.ToString()));
                bll.vInsertReqPromoItem(arr1); arr1.Clear();
                if (hdpromo.Value.ToString() == "")
                {
                    arr1.Add(new cArrayList("@promo_no", Request.Cookies["usr_id"].Value.ToString()));
                }
                else
                {
                    arr1.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
                }
                arr1.Add(new cArrayList("@customer", rdcust.SelectedValue.ToString()));
                bll.vBindingGridToSp(ref grditem, "sp_treqdiscount_item_get", arr1);
                //arr.Add(new cArrayList("@cust", rs["rdcust"].ToString()));
                grditem.Visible = true;
                grdgroup.Visible = false;   
            }
            else if (rs["rditem"].ToString() == "G")
            {
                arr1.Add(new cArrayList("@prop_no", hdproposal.Value.ToString()));
                arr1.Add(new cArrayList("@salespoint_cd", Request.Cookies["sp"].Value.ToString()));
                bll.vInsertReqPromoProduct(arr1); arr1.Clear();
                if (hdpromo.Value.ToString() == "")
                {
                    arr1.Add(new cArrayList("@promo_no", Request.Cookies["usr_id"].Value.ToString()));
                }
                else
                {
                    arr1.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
                }
                arr1.Add(new cArrayList("@customer", rdcust.SelectedValue.ToString())); 
                bll.vBindingGridToSp(ref grdgroup, "sp_treqdiscount_product_get", arr1);
                //arr.Add(new cArrayList("@cust", rs["rdcust"].ToString()));
                //bll.vBindingGridToSp(ref grdgroup, "sp_tproposal_productgroup_get", arr);
                grditem.Visible = false;
                grdgroup.Visible = true;   
            }
            //rdcust_SelectedIndexChanged(sender, e);
            //rditem_SelectedIndexChanged(sender, e);
        }
        txproposal.CssClass = "form-control input-sm ro";
        txproposal.Enabled = false;
        cbpromokind.CssClass = "form-control input-sm ro";
        cbpromokind.Enabled = false;
        cbpromogroup.CssClass = "form-control input-sm ro";
        cbpromogroup.Enabled = false;
        cbpromotype.CssClass = "form-control input-sm ro";
        cbpromotype.Enabled = false;
    }
    protected void rdFreegood_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdFreegood.SelectedValue.ToString() == "Y")
        {
            viewFreegood.Attributes.Remove("style");
            rditemfree_SelectedIndexChanged(sender, e);
        }
        else
        {
            viewFreegood.Attributes.Add("style", "display:none");
        }
    }
    protected void rditemfree_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (rditemfree.SelectedValue.ToString() == "I")
        {

            cbgroupfree.Attributes.Add("style", "display:none");
            txsearchitemfree.Attributes.Remove("style");
            grdgroupfree.Visible = false;
            grditemfree.Visible = true;

        }
        else if (rditemfree.SelectedValue.ToString() == "G")
        {
            txsearchitemfree.Attributes.Add("style", "display:none");
            cbgroupfree.Attributes.Remove("style");
            grdgroupfree.Visible = true;
            grditemfree.Visible = false;
            arr.Add(new cArrayList("@level_no", "3"));
            bll.vBindingComboToSp(ref cbgroupfree, "sp_tmst_product_get", "prod_cd", "prod_nm", arr);
        }
    }
    protected void btaddfree_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (rditemfree.SelectedValue.ToString() == "I")
        {
            
            arr.Add(new cArrayList("@item_cd", hditemfree.Value.ToString()));
            if (hdpromo.Value.ToString() == "")
            {
                arr.Add(new cArrayList("@promo_no", Request.Cookies["usr_id"].Value.ToString()));
            }
            else
            {
                arr.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
            }
            arr.Add(new cArrayList("@salespoint_cd", Request.Cookies["sp"].Value.ToString()));
            bll.vInsertReqPromoItemFree(arr); arr.Clear();
            if (hdpromo.Value.ToString() == "")
            {
                arr.Add(new cArrayList("@promo_no", Request.Cookies["usr_id"].Value.ToString()));
            }
            else
            {
                arr.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
            }
            bll.vBindingGridToSp(ref grditemfree, "sp_treqdiscount_itemfree_get", arr);
            grditemfree.Visible = true;
            grdgroupfree.Visible = false;
        }
        else
        {
            arr.Add(new cArrayList("@prod_cd", cbgroupfree.SelectedValue.ToString()));
            if (hdpromo.Value.ToString() == "")
            {
                arr.Add(new cArrayList("@promo_no", Request.Cookies["usr_id"].Value.ToString()));
            }
            else
            {
                arr.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
            }
            arr.Add(new cArrayList("@salespoint_cd", Request.Cookies["sp"].Value.ToString()));
            bll.vInsertReqPromoProductFree(arr); arr.Clear();
            if (hdpromo.Value.ToString() == "")
            {
                arr.Add(new cArrayList("@promo_no", Request.Cookies["usr_id"].Value.ToString()));
            }
            else
            {
                arr.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
            }
            bll.vBindingGridToSp(ref grdgroupfree, "sp_treqdiscount_productfree_get", arr);
            grditemfree.Visible = false;
            grdgroupfree.Visible = true;
        }
        txsearchitemfree.Text = "";
    }
    protected void grditemfree_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbitemcode;
        List<cArrayList> arr = new List<cArrayList>();
        lbitemcode = (Label)grditemfree.Rows[e.RowIndex].FindControl("lbitemcode");
        if (hdpromo.Value.ToString() == "")
        {
            arr.Add(new cArrayList("@promo_no", Request.Cookies["usr_id"].Value.ToString()));
        }
        else
        {
            arr.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
        }
        arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
        bll.vDelReqPromoItemFree(arr);
        arr.Clear();
        if (hdpromo.Value.ToString() == "")
        {
            arr.Add(new cArrayList("@promo_no", Request.Cookies["usr_id"].Value.ToString()));
        }
        else
        {
            arr.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
        }
        bll.vBindingGridToSp(ref grditemfree, "sp_treqdiscount_itemfree_get", arr);
    }
    protected void grdgroupfree_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbgroupcode;
        List<cArrayList> arr = new List<cArrayList>();
        lbgroupcode = (Label)grdgroupfree.Rows[e.RowIndex].FindControl("lbgroupcode");
        if (hdpromo.Value.ToString() == "")
        {
            arr.Add(new cArrayList("@promo_no", Request.Cookies["usr_id"].Value.ToString()));
        }
        else
        {
            arr.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
        }
        arr.Add(new cArrayList("@prod_cd", lbgroupcode.Text));
        bll.vDelReqPromoProductFree(arr);
        arr.Clear();
        if (hdpromo.Value.ToString() == "")
        {
            arr.Add(new cArrayList("@promo_no", Request.Cookies["usr_id"].Value.ToString()));
        }
        else
        {
            arr.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
        }
        bll.vBindingGridToSp(ref grdgroupfree, "sp_treqdiscount_productfree_get", arr);
    }
    protected void dtstart_TextChanged(object sender, EventArgs e)
    {
        DateTime dt = DateTime.ParseExact(dtstart.Text.ToString(), "dd/MM/yyyy", null);
        if (dt <= System.DateTime.Today)
        {
            dtstart.Text = String.Format("{0:dd/MM/yyyy}", System.DateTime.Today.AddDays(1));
            dtend.Text = String.Format("{0:dd/MM/yyyy}", System.DateTime.Today.AddDays(7));
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Start Date must be in the next day.','Start date','warning');", true);
        }
        else
        {
            dtend.Text = String.Format("{0:dd/MM/yyyy}", dt.AddDays(7));
        }
    }
    protected void btaddbranch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (rdcust.SelectedValue.ToString() != "C")
        {
            if (hdpromo.Value.ToString() == "")
            {
                arr.Add(new cArrayList("@promo_no", Request.Cookies["usr_id"].Value.ToString()));
            }
            else
            {
                arr.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
            }
            arr.Add(new cArrayList("@salespoint_cd", cbsalespoint.SelectedValue.ToString()));
            bll.vInsertReqPromoSalespoint(arr); arr.Clear();
            if (hdpromo.Value.ToString() == "")
            {
                arr.Add(new cArrayList("@promo_no", Request.Cookies["usr_id"].Value.ToString()));
            }
            else
            {
                arr.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
            }
            bll.vBindingGridToSp(ref grdsalespoint, "sp_treqdiscount_salespoint_get", arr);
            grdsalespoint.Visible = true;
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Add other branch only for Group and Type.','Add Branch','warning');", true);
            return;
        }
    }
    protected void grdsalespoint_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbsalespoint;
        List<cArrayList> arr = new List<cArrayList>();
        lbsalespoint = (Label)grdsalespoint.Rows[e.RowIndex].FindControl("lbsalespoint");
        if (hdpromo.Value.ToString() == "")
        {
            arr.Add(new cArrayList("@promo_no", Request.Cookies["usr_id"].Value.ToString()));
        }
        else
        {
            arr.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
        }
        arr.Add(new cArrayList("@salespoint_cd", lbsalespoint.Text));
        bll.vDelReqPromoSalespoint(arr);
        arr.Clear();
        if (hdpromo.Value.ToString() == "")
        {
            arr.Add(new cArrayList("@promo_no", Request.Cookies["usr_id"].Value.ToString()));
        }
        else
        {
            arr.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
        }
        bll.vBindingGridToSp(ref grdcust, "sp_treqdiscount_salespoint_get", arr);
    }
    protected void dtdelivery_TextChanged(object sender, EventArgs e)
    {
        DateTime dts = DateTime.ParseExact(dtstart.Text.ToString(), "dd/MM/yyyy", null);
        DateTime dte = DateTime.ParseExact(dtdelivery.Text.ToString(), "dd/MM/yyyy", null);
        if (dts < dte)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Delivery Date must be, before start Date','Delivery Date','warning');", true);
            return;
        }
    }
    protected void dtend_TextChanged(object sender, EventArgs e)
    {
        DateTime dt = DateTime.ParseExact(dtend.Text.ToString(), "dd/MM/yyyy", null);
        if (dt <= System.DateTime.Today)
        {
            dtdelivery.Text = String.Format("{0:dd/MM/yyyy}", System.DateTime.Today.AddDays(1));
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Delivery Date minimum, must be in the next day.','Delivery date','warning');", true);
        }
    }
}