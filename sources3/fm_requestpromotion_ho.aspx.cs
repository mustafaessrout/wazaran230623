using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_requestpromotionho : System.Web.UI.Page
{
    cbll bll = new cbll();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            lbSp.Text = Request.Cookies["spn"].Value.ToString();
            lbpromotionno.Text = "NEW";
            dtstart.Text = System.DateTime.Today.ToString("dd/MM/yyyy");
            dtend.Text = System.DateTime.Today.ToString("dd/MM/yyyy");
            cbpromokind_SelectedIndexChanged(sender, e);
            rdcust_SelectedIndexChanged(sender, e);
            rditem_SelectedIndexChanged(sender, e);
            cbbgitem_SelectedIndexChanged(sender, e);
            bll.vBindingFieldValueToCombo(ref cbpaymenttype, "rdpayment");
            btedit.Visible = false;
            btprint.Visible = false;
            btnew.Visible = false;
            btapprove.Visible = false;
            btreject.Visible = false;
            arr.Clear();
            arr.Add(new cArrayList("@promo_no", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingSalespointToCombo(ref cbsalespoint);
            //bll.vDelReqPromoCustomer(arr);
            //bll.vDelReqPromoCusgrcd(arr);
            //bll.vDelReqPromoCustType(arr);
            //bll.vDelReqPromoItem(arr);
            //bll.vDelReqPromoProduct(arr);

        }
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
        bll.vSearchMstCustomer(arr, ref rs);
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
    protected void btsearchpromotion_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "popupwindow('lookupreqdiscount_ho.aspx');", true);
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
        }
        else if (rdcust.SelectedValue.ToString() == "G")
        {
            txsearchcust.Attributes.Add("style", "display:none");
            cbcusgrcd.Attributes.Remove("style");
            grdcusgrcd.Visible = true;
            grdcust.Visible = false;
            grdcusttype.Visible = false;
            bll.vBindingFieldValueToCombo(ref cbcusgrcd, "cusgrcd");
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
        }
        arr.Clear();
        arr.Add(new cArrayList("@promo_no", Request.Cookies["usr_id"].Value.ToString()));
        //bll.vDelReqPromoCustomer(arr);
        //bll.vDelReqPromoCusgrcd(arr);
        //bll.vDelReqPromoCustType(arr);
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

    }
    protected void grdcust_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void grdcusgrcd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void grdcusttype_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

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
                arr.Add(new cArrayList("@salespoint_cd", Request.Cookies["sp"].Value.ToString()));
            }
            else
            {
                arr.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
                arr.Add(new cArrayList("@salespoint_cd", bll.vLookUp("select salespoint_cd from tmst_reqdiscount where disc_cd='"+hdpromo.Value.ToString()+"'")));
            }
            
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
                arr.Add(new cArrayList("@salespoint_cd", Request.Cookies["sp"].Value.ToString()));
            }
            else
            {
                arr.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
                arr.Add(new cArrayList("@salespoint_cd", bll.vLookUp("select salespoint_cd from tmst_reqdiscount where disc_cd='" + hdpromo.Value.ToString() + "'")));
            }
            
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
        bll.vBindingGridToSp(ref grditem, "sp_treqdiscount_product_get", arr);
    }
    protected void cbpaymenttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbpaymenttype.SelectedValue.ToString() == "FG")
        {
            lbsar.Text = "CTN";
        }
        else if (cbpaymenttype.SelectedValue.ToString() == "CH")
        {
            lbsar.Text = "SAR";
        }
        else if (cbpaymenttype.SelectedValue.ToString() == "CR")
        {
            lbsar.Text = "SAR";
        }
        else if (cbpaymenttype.SelectedValue.ToString() == "CN")
        {
            lbsar.Text = "SAR";
        }
    }
    protected void btnew_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("fm_requestpromotion.aspx");
    }
    protected void btapprove_Click(object sender, EventArgs e)
    {
        string sDiscount = ""; string sRdCust = ""; string sRdItem = "";

        if (btapprove.Text == "Processed to Proposal")
        {
            if (bll.nCheckAccess("appreqprop", Request.Cookies["usr_id"].Value.ToString()) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To approve this request promotion, contact Administrator !!','warning');", true);
                return;
            }

            if (txremark.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Remark can not empty.','To Process this request promotion, fill remarks... !!','warning');", true);
                return;
            }

            if (cbpaymenttype.SelectedValue.ToString() == "FG")
            {
                if (rdFreegood.SelectedValue.ToString() == "N")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Freegood must be selected','To Process this request promotion, add freegood. !!','warning');", true);
                    return;
                }
                else
                {
                    if (rditemfree.SelectedValue.ToString() == "I")
                    {
                        if (grditemfree.Rows.Count <= 0)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Freegood must be add','To Process this request promotion, add freegood. !!','warning');", true);
                            return;
                        }
                    }
                    else
                    {
                        if (grdgroupfree.Rows.Count <= 0)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Freegood must be add','To Process this request promotion, add freegood. !!','warning');", true);
                            return;
                        }
                    }
                }
            }
            else
            {
                if (grditemfree.Rows.Count > 0 || grdgroupfree.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('If Not Freegood can not add Freeitem.','To Process this request promotion, delete freegood. !!','warning');", true);
                    return;
                }
            }

            if (txproposal.Text == "")
            {
                List<cArrayList> arr = new List<cArrayList>();
                arr.Clear();
                arr.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
                arr.Add(new cArrayList("@approveby", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@disc_sta_id", "AP"));
                bll.vApprovalMstReqPromo(arr);
                arr.Clear();
                Response.Redirect("fm_proposal.aspx");
            }
            else
            {
                List<cArrayList> arr = new List<cArrayList>();
                arr.Clear();
                arr.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
                arr.Add(new cArrayList("@prop_no", txproposal.Text.ToString()));
                arr.Add(new cArrayList("@approveby", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@disc_sta_id", "AP"));
                bll.vApprovalMstReqPromo(arr);
                arr.Clear();
                btapprove.Text = "Processed to Scheme";
            }

        }
        else if (btapprove.Text == "Processed to Scheme")
        {
            if (bll.nCheckAccess("appreqscheme", Request.Cookies["usr_id"].Value.ToString()) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To approve this request promotion, contact Administrator !!','warning');", true);
                return;
            }

            if (txproposal.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Proposal Must be Selected. If Not available create new Proposal','Request Promotion (Approval)','warning');", true);
                return;
            }

            List<cArrayList> arr = new List<cArrayList>();
            arr.Clear();
            arr.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
            arr.Add(new cArrayList("@prop_no", txproposal.Text.ToString()));
            arr.Add(new cArrayList("@approveby", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@disc_sta_id", "AS"));
            bll.vApprovalMstReqPromo(arr);
            arr.Clear();
            btapprove.Text = "Approve";
        }
        else if (btapprove.Text == "Approve") 
        {
            if (bll.nCheckAccess("appreq", Request.Cookies["usr_id"].Value.ToString()) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To approve this request promotion, contact Administrator !!','warning');", true);
                return;
            }

            if (txproposal.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Proposal Must be Selected. If Not available create new Proposal','Request Promotion (Approval)','warning');", true);
                return;
            }

            List<cArrayList> arr = new List<cArrayList>();
            string sPromoNo = hdpromo.Value.ToString();
            arr.Clear();
            arr.Add(new cArrayList("@promo_no", sPromoNo));
            arr.Add(new cArrayList("@approveby", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@disc_sta_id", "A"));
            bll.vApprovalMstReqPromo(arr);
            arr.Clear();

            // Generated Discount 
            if (cbpromotype.SelectedValue.ToString() == "SC" || cbpromotype.SelectedValue.ToString() == "LT" || cbpromotype.SelectedValue.ToString() == "IP" || cbpromotype.SelectedValue.ToString() == "MF")
            {
                //if (chdiscount.Checked)
                //{
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
                arr.Add(new cArrayList("@promo_no", sPromoNo));
                arr.Add(new cArrayList("@disc_no", sDiscount));
                //bll.vGeneratedProposalDisc(arr);
                bll.vInsertDiscSalespointPromo(arr);
                sRdCust = rdcust.SelectedValue.ToString();
                if (sRdCust == "C")
                {
                    bll.vInsertDiscCustomerPromo(arr);
                }
                else if (sRdCust == "G")
                {
                    bll.vInsertDiscCusgrcdPromo(arr);
                }
                else
                {
                    bll.vInsertDiscCustypePromo(arr);
                }
                sRdItem = rditem.SelectedValue.ToString();
                if (sRdItem == "I")
                {
                    bll.vInsertDiscItemPromo(arr);
                }
                else
                {
                    bll.vInsertDiscProductPromo(arr);
                }
                bll.vGeneratedDiscountFormulaPromo(arr);
                string itemFree = "";
                itemFree = bll.vLookUp("select top 1 rditemfree from tmst_proposal where prop_no='" + txproposal.Text + "'");
                if (itemFree == "") { itemFree = rditemfree.SelectedValue.ToString(); }
                if (itemFree == "I")
                {
                    bll.vGeneratedDiscountFreeitem(arr);
                }
                else if (itemFree == "G")
                {
                    bll.vGeneratedDiscountFreeProduct(arr);
                }
                arr.Clear();
                //Save main discount data
                arr.Add(new cArrayList("@disc_cd", sDiscount));
                arr.Add(new cArrayList("@proposal_no", txproposal.Text));
                arr.Add(new cArrayList("@start_dt", DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@end_dt", DateTime.ParseExact(dtend.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@delivery_dt", DateTime.ParseExact(dtdelivery.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@disc_dt", DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@disc_typ", cbpromotype.SelectedValue.ToString()));
                arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@disc_sta_id", "A"));
                //arr.Add(new cArrayList("@remark", txbgremark.Text));
                arr.Add(new cArrayList("@rdcustomer", rdcust.SelectedValue.ToString()));
                //arr.Add(new cArrayList("@propvendor_no", txpropvendor.Text));
                arr.Add(new cArrayList("@discount_mec", cbpaymenttype.SelectedValue.ToString()));
                arr.Add(new cArrayList("@discount_use", "A"));
                //arr.Add(new cArrayList("@vendor_cd", cbvendor.SelectedValue.ToString()));
                arr.Add(new cArrayList("@benefitpromotion", "NONE"));
                if (sRdItem == "I")
                {
                    arr.Add(new cArrayList("@qty_min", bll.vLookUp("select top 1 x from treqdiscount_item where disc_cd='" + sPromoNo + "'")));
                    arr.Add(new cArrayList("@rditem", "P"));
                }
                else
                {
                    arr.Add(new cArrayList("@qty_min", bll.vLookUp("select top 1 x from treqdiscount_product where disc_cd='" + sPromoNo + "'")));
                    arr.Add(new cArrayList("@rditem", "G"));
                }
                arr.Add(new cArrayList("@qty_max", txbudget.Text.Replace(",", "").ToString()));
                arr.Add(new cArrayList("@regularcost", "0.0"));
                arr.Add(new cArrayList("@netcost", "0.0"));
                arr.Add(new cArrayList("@rdfreeitem", bll.vLookUp("select top 1 rditemfree from tmst_reqdiscount where disc_cd='" + sPromoNo + "'")));
                arr.Add(new cArrayList("@catalogimage", ""));
                arr.Add(new cArrayList("@reqdisc_cd", sPromoNo));
                bll.vInsertMstDiscountPromo(arr);

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Request Promotion(Scheme) Approved.','" + sPromoNo + ", and " + sDiscount + " generated.','success');", true);
                


            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Request Promotion(Scheme) Approved.','" + sPromoNo + "','success');", true);
            }

            btapprove.Visible = false;
            btreject.Visible = false;
        }      

        
    }

    protected void btreject_Click(object sender, EventArgs e)
    {
        if (bll.nCheckAccess("appproposal", Request.Cookies["usr_id"].Value.ToString()) == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To reject this request promotion, contact Administrator !!','warning');", true);
            return;
        }

        if (txnoted.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Insert Remarks for rejected request discount.','Request Promotion (Approval)','warning');", true);
            return;
        }

        List<cArrayList> arr = new List<cArrayList>();
        string sPromoNo = hdpromo.Value.ToString();
        arr.Clear();
        arr.Add(new cArrayList("@promo_no", sPromoNo));
        arr.Add(new cArrayList("@prop_no", txproposal.Text));
        arr.Add(new cArrayList("@approveby", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@noted", txnoted.Text));
        arr.Add(new cArrayList("@disc_sta_id", "R"));
        bll.vApprovalMstReqPromo(arr);
        arr.Clear();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Request Promotion(Scheme) Rejected.','" + sPromoNo + "','success');", true);
        return;
    }

    protected void btprint_ServerClick(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=reqdiscount&disc=" + lbpromotionno.Text.ToString() + "&type=" + rdpromotion.SelectedValue.ToString() + "');", true);
    }
    protected void btedit_ServerClick(object sender, EventArgs e)
    {
        if (btedit.Text == "Edit")
        {
            btnew.Visible = false;
            btapprove.Visible = false;
            btprint.Visible = false;
            btreject.Visible = false;
            btedit.Text = "Update";

            txremark.Enabled = true;
            txbudget.Enabled = true;
            cbpaymenttype.Enabled = true;
            dtstart.Enabled = true;
            dtend.Enabled = true;
            dtdelivery.Enabled = true;
            btadd.Visible = true;
            btaddfree.Visible = true;

            btadd.Visible = true;
            btaddcust.Visible = true;
            btaddfree.Visible = true;
            dtstart.Enabled = true;
            dtend.Enabled = true;
            dtdelivery.Enabled = true;
            rdpromotion.Enabled = true;
            cbpromokind.Enabled = true;
            cbpromogroup.Enabled = true;
            cbpromotype.Enabled = true;
            rdcust.Enabled = true;
            txsearchcust.Enabled = true;
            cbcusgrcd.Enabled = true;
            rditem.Enabled = true;
            txsearchitem.Enabled = true;
            cbgroup.Enabled = true;
            cbbgitem.Enabled = true;
            txbgitemax.Enabled = true;
            txbgitemay.Enabled = true;
            txbgitemalimit.Enabled = true;
            txbgitembx.Enabled = true;
            txbgitemby.Enabled = true;
            txbgitemcx.Enabled = true;
            txbgitemcy.Enabled = true;
            txbgitemdx.Enabled = true;
            txbgitemex.Enabled = true;
            txbudget.Enabled = true;
            cbgroupfree.Enabled = true;
            rditemfree.Enabled = true;
            txsearchitemfree.Enabled = true;
            grditemfree.Columns[2].Visible = true;
            grdgroupfree.Columns[2].Visible = true;
            grdsalespoint.Columns[2].Visible = true;
            cbpaymenttype.Enabled = true;
            cbp_uom.Enabled = true;
            txcbp.Enabled = true;
            txremark.Enabled = true;

        }
        else
        {

            List<cArrayList> arr = new List<cArrayList>();
            arr.Clear();
            arr.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
            arr.Add(new cArrayList("@remark", txremark.Text.ToString()));
            arr.Add(new cArrayList("@budget", txbudget.Text.ToString()));
            arr.Add(new cArrayList("@rdpayment", cbpaymenttype.SelectedValue.ToString()));
            arr.Add(new cArrayList("@start_dt", DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@end_dt", DateTime.ParseExact(dtend.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@delivery_dt", DateTime.ParseExact(dtdelivery.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            bll.vUpdateMstReqPromo(arr);
            arr.Clear();

            btedit.Text = "Edit";
            btnew.Visible = false;
            btapprove.Visible = true;
            btreject.Visible = true;
            btprint.Visible = true;

            txremark.Enabled = false;
            txbudget.Enabled = false;
            cbpaymenttype.Enabled = false;

            dtstart.Enabled = false;
            dtend.Enabled = false;
            dtdelivery.Enabled = false;
        }
    }
    protected void btdiscount_Click(object sender, EventArgs e)
    {
        btapprove.Visible = false;
        btnew.Visible = false;
        btprint.Visible = true;
        btedit.Visible = false;
        lbpromotionno.Text = hdpromo.Value;
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
            lbtitle.Text = "Request Promotion (Approval), Status : " + rs["status"].ToString();
            rdFreegood.SelectedValue = rs["rdfreegood"].ToString();
            txproposal.Text = rs["prop_no"].ToString();
            rditemfree.SelectedValue = rs["rditemfree"].ToString();
            txremark.Text = rs["remark"].ToString();
            txcbp.Text = rs["cbp"].ToString();
            cbp_uom.SelectedValue = rs["cbp_uom"].ToString();
            txapproval.Text = rs["app_by"].ToString() + " - " + rs["app_nm"].ToString();
            if (rs["disc_sta_id"].ToString() == "A" || rs["disc_sta_id"].ToString() == "R")
            {
                btapprove.Visible = false; btreject.Visible = false; btprint.Visible = true;
            }
            else
            {
                if (rs["disc_sta_id"].ToString() == "N") { btapprove.Text = "Processed to Proposal"; btedit.Visible = true; }
                else if (rs["disc_sta_id"].ToString() == "AP") { btapprove.Text = "Processed to Scheme"; btedit.Visible = false; }
                else if (rs["disc_sta_id"].ToString() == "AS") { btapprove.Text = "Approve"; btedit.Visible = false; }
                btapprove.Visible = true; btreject.Visible = true; btprint.Visible = true;
            }
        }
        rs.Close();
        rdcust_SelectedIndexChanged(sender, e);
        rditem_SelectedIndexChanged(sender, e);
        rdFreegood_SelectedIndexChanged(sender, e);
        
        grdsalespoint.Visible = true;
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

        arr.Clear();
        // Salespoint 
        if (hdpromo.Value.ToString() == "")
        {
            arr.Add(new cArrayList("@promo_no", Request.Cookies["usr_id"].Value.ToString()));
        }
        else
        {
            arr.Add(new cArrayList("@promo_no", hdpromo.Value.ToString()));
        }
        bll.vBindingGridToSp(ref grdsalespoint, "sp_treqdiscount_salespoint_get", arr);

        btadd.Visible = false;
        btaddcust.Visible = false;
        btaddfree.Visible = false;
        dtstart.Enabled = false;
        dtend.Enabled = false;
        dtdelivery.Enabled = false;
        rdpromotion.Enabled = false;
        cbpromokind.Enabled = false;
        cbpromogroup.Enabled = false;
        cbpromotype.Enabled = false;
        rdcust.Enabled = false;
        txsearchcust.Enabled = false;
        cbcusgrcd.Enabled = false;
        rditem.Enabled = false;
        txsearchitem.Enabled = false;
        cbgroup.Enabled = false;
        cbbgitem.Enabled = false;
        txbgitemax.Enabled = false;
        txbgitemay.Enabled = false;
        txbgitemalimit.Enabled = false;
        txbgitembx.Enabled = false;
        txbgitemby.Enabled = false;
        txbgitemcx.Enabled = false;
        txbgitemcy.Enabled = false;
        txbgitemdx.Enabled = false;
        txbgitemex.Enabled = false;
        txbudget.Enabled = false;
        cbgroupfree.Enabled = false;
        rditemfree.Enabled = false;
        txsearchitemfree.Enabled = false;
        grditemfree.Columns[2].Visible = false;
        grdgroupfree.Columns[2].Visible = false;
        grdsalespoint.Columns[2].Visible = false;
        cbpaymenttype.Enabled = false;
        cbp_uom.Enabled = false;
        txcbp.Enabled = false;
        txremark.Enabled = false;
        btreject.Visible = true;
        //btapprove.Visible = true;
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
}