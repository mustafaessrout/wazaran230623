using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_reqreturnexpired : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Clear();
            arr.Add(new cArrayList("@reasn_typ", "return"));
            bll.vBindingComboToSp(ref cbremark, "sp_tmst_reason_get", "reasn_cd", "reasn_nm", arr);
            bll.vBindingFieldValueToCombo(ref rdreturtype, "retur_typ");
            rdreturtype_SelectedIndexChanged(sender, e);
            arr.Clear();
            arr.Add(new cArrayList("@qry_cd", "driver"));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingComboToSp(ref cbdriver, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
            bll.vBindingFieldValueToCombo(ref cbuom, "uom");
            //bll.vBindingFieldValueToCombo(ref cbreturcost, "returcost");
            dtexp.Text = Request.Cookies["waz_dt"].Value;
            dtrequest.Text = Request.Cookies["waz_dt"].Value;
            dtrequest.CssClass = cd.csstextro;
            dtexp_CalendarExtender.EndDate = System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            string sApp = bll.vLookUp("select emp_cd from tapprovalpattern where doc_typ='branchspv'");
            txapproval.Text = bll.sGetEmployeeName(sApp);
            arr.Clear();
            arr.Add(new cArrayList("@retur_no", Request.Cookies["usr_id"].Value));
            bll.vDelSalesReturnCosting(arr);
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value));
            bll.vDelWrkSalesReturn(arr);
            txreqno.Text = "NEW";
            btsave.CssClass = "btn btn-default ro";
            btprint.CssClass = "btn btn-default ro";
            txsalesman.Text = string.Empty;
            hdemp.Value = string.Empty;
            txsalesman.CssClass = cd.csstextro;
        }
    }

    protected void btadd_Click(object sender, EventArgs e)
    {

        if (hdcust.Value.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Customer not yet selected !','Can not find price','warning');", true);
            return;
        }

        if (txqty.Text == "")
        {
            if (hdcust.Value.ToString() == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Quantity for retur','Quantity can not empty','warning');", true);
                return;
            }
        }
        double dCustPrice = 0;
        if (!double.TryParse(txcustprice.Text, out dCustPrice))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Unit Price from salesman must be currency','Price Entry','warning');", true);
            return;
        }

        if (dCustPrice > Convert.ToDouble(lbprice.Text))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Unit Price salesman can not bigger than unit price','Price Problem','warning');", true);
            return;
        }

        if (dtexp.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Expiration product must be filled','Product Expire','warning');", true);
            return;
        }
        if (hditem.Value.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Select item to be returned','Item Must be selected','warning');", true);
            return;
        }

        //if (cbbin.SelectedValue.ToString() == "")
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Stock condition (Bad Stock, Near Exp or Damage) is not allowed','Check expiration date','warning');", true);
        //    return;
        //}

        if (txmanualno.Text == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please fill manual no','Manual No','warning');", true);
            return;
        }
        txmanualno.CssClass = cd.csstextro;
        txcustomer.CssClass = cd.csstextro;
        txsalesman.CssClass = cd.csstext;
        double dUnitprice = 0;
        double dOut = 0;
        if (double.TryParse(lbprice.Text, out dOut))
        {
            dUnitprice = dOut;
        }
        else
        {
            dUnitprice = Convert.ToDouble(bll.vLookUp("select dbo.fn_getadjustmentprice ('" + hditem.Value.ToString() + "','" + hdcust.Value.ToString() + "','" + cbuom.SelectedValue.ToString() + "')"));
        }

        double dQty = 0;
        if (!double.TryParse(txqty.Text, out dQty))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Quantity must numeric','Check quantity','warning');", true);
            return;
        }

        if (lbqtyavl.Text == string.Empty)
        {
            lbqtyavl.Text = "0";
        }
        double dQtyAvail = Convert.ToDouble(lbqtyavl.Text);
        if (hdinvoice.Value != string.Empty)
        {
            if ((Convert.ToDouble(txqty.Text) > dQtyAvail) && (txreqno.Text == "NEW"))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Can not exceeded available qty for return','Check qty already use for retur from invoice','warning');", true);
                return;
            }
        }

        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@item_cd", hditem.Value.ToString()));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@qty", txqty.Text));
        arr.Add(new cArrayList("@uom", cbuom.SelectedValue.ToString()));
        //arr.Add(new cArrayList("@reason", cbreason.SelectedValue));
        arr.Add(new cArrayList("@condition", lbexp.Text));
        arr.Add(new cArrayList("@exp_dt", DateTime.ParseExact(dtexp.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@whs_cd", cbwhs.SelectedValue.ToString()));
        arr.Add(new cArrayList("@bin_cd", cbbin.SelectedValue.ToString()));
        arr.Add(new cArrayList("@unitprice", dUnitprice));
        arr.Add(new cArrayList("@custprice", dCustPrice));
        arr.Add(new cArrayList("@reason", cbremark.SelectedValue.ToString()));   // (dCustPrice * Convert.ToDouble( txqty.Text))));
        arr.Add(new cArrayList("@vat", lbvat.Text));
        arr.Add(new cArrayList("@isvat", cbvat.SelectedValue.ToString()));
        arr.Add(new cArrayList("@inv_no", hdinvoice.Value));
        bll.vInsertWrkSalesReturn(arr);
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_twrk_salesreturn_get", arr);
        if (grd.Rows.Count > 0)
        {
            Label lbtotsubtotal = (Label)grd.FooterRow.FindControl("lbtotsubtotal");
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value));
            string sTotAmt = bll.vLookUp("select sum(custprice * qty) + sum(vat) from twrk_salesreturn where usr_id='" + Request.Cookies["usr_id"].Value + "'");
            lbtotsubtotal.Text = sTotAmt;
        }
        txitemsearch.Text = "";
        hditem.Value = "";
        txqty.Text = "";
        // lbamt.Text =  bll.vLookUp("select cast(sum(qty * custprice) as numeric(18,2)) from twrk_salesreturn where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "'");
        Label lbtotamt = (Label)grd.FooterRow.FindControl("lbtotcustprice");
        Label lbtotvat = (Label)grd.FooterRow.FindControl("lbtotvat");
        //lbamt.Text = lbtotamt.Text;
        //lbexp.Text = "";
        dtexp.Text = "";
        txcustprice.Text = "";
        lbprice.Text = "";
        cbuom.SelectedValue = "";
        lbprice.Text = "";
        lbtotprice.Text = "";
        lbvat.Text = "";
        dtexp.Text = Request.Cookies["waz_dt"].Value;
        btsave.CssClass = "btn btn-info";
        btsearch.CssClass = "btn btn-default ro";
        txitemsearch.CssClass = cd.csstext;
        //btsave.CssClass = "btn btn-warning btn-save";
    }

    protected void cbuom_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (hditem.Value.ToString() == "")
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Item not yet selected','select item','warning');", true);
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
        //    cbuom.SelectedValue = "";
        //    return;
        //}

        //if (cbuom.SelectedValue == "")
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
        //    return;
        //}
        ////List<cArrayList> arr = new List<cArrayList>();
        ////double dprice = bll.dGetItemPrice(hditem.Value.ToString(), lbcusttype.Text, cbuom.SelectedValue.ToString());
        ////double dprice = Convert.ToDouble(bll.vLookUp("select dbo.fn_getadjustmentprice ('" + hditem.Value.ToString() + "','" + hdcust.Value.ToString() + "','" + cbuom.SelectedValue.ToString() + "')"));
        ////lbprice.Text = dprice.ToString();
        //dtexp.CssClass = cd.csstext;
        //btprice_Click(sender, e);
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);


        //if (hditem.Value.ToString() == "")
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Item not yet selected','select item','warning');", true);
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
        //    cbuom.SelectedValue = "";
        //    return;
        //}

        if (cbuom.SelectedValue == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            return;
        }

        string sQtyAvl = lbqtyavl.Text = bll.vLookUp("select dbo.fn_checkqtyavlreturn('" + hdinvoice.Value + "','" + hditem.Value + "','" + Request.Cookies["usr_id"].Value + "')");
        lbqtyavl.Text = bll.vLookUp("select dbo.fn_ItemConversion('CTN','" + cbuom.SelectedValue + "','" + hditem.Value + "'," + sQtyAvl + ")");
        dtexp.CssClass = cd.csstext;
        btprice_Click(sender, e);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);



    }

    protected void cbvat_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (hditem.Value.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Item is not yet selected','Select Item','warning');", true);
            cbvat.SelectedValue = "1";
            return;
        }
        if (cbvat.SelectedValue == "1")
        {
            lbvat.Text = (Convert.ToDouble(bll.sGetControlParameter("vat")) * (Convert.ToDouble(txcustprice.Text) * Convert.ToDouble(txqty.Text))).ToString();
            //lbtotprice.Text = (Convert.ToDouble(lbvat.Text) + Convert.ToDouble(lbtotprice.Text)).ToString();
        }
        else if (cbvat.SelectedValue == "0")
        {

            //lbtotprice.Text = (Convert.ToDouble(lbtotprice.Text) - Convert.ToDouble(lbvat.Text)).ToString();
            lbvat.Text = "0";
            //btprice_Click(sender, e);
        }
    }

    protected void cbwhs_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@whs_cd", cbwhs.SelectedValue));
        //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        bll.vBindingComboToSp(ref cbbin, "sp_twarehouse_bin_get", "bin_cd", "bin_nm", arr);
        cbbin.SelectedValue = "BS"; cbbin.CssClass = cd.csstextro;
    }

    protected void dtexp_TextChanged(object sender, EventArgs e)
    {

    }

    protected void txcustprice_TextChanged(object sender, EventArgs e)
    {
        double dQty = 0; double dCustPrice = 0; double dUnitPrice = 0;
        if (!double.TryParse(txqty.Text, out dQty))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Qty must numeric','wrong input','warning');", true);
            return;
        }
        if (!double.TryParse(txcustprice.Text, out dCustPrice))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Cust Price must numeric','wrong input','warning');", true);
            txcustprice.Text = "0";
            return;
        }
        if (!double.TryParse(lbprice.Text, out dUnitPrice))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Unit Price must numeric','wrong Unit Price','warning');", true);
            lbprice.Text = "";
            return;
        }

        if (dCustPrice > dUnitPrice)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Cust Price can not bigger than unit price','Check salesman price','warning');", true);
            dCustPrice = 0; lbvat.Text = ""; lbtotprice.Text = "";
            txcustprice.Text = "0";
            return;

        }

        lbtotprice.Text = (dQty * Convert.ToDouble(txcustprice.Text)).ToString();
        string sVAT = bll.sGetControlParameter("vat");
        double dVAT = Convert.ToDouble(sVAT);
        double dTotVat = dVAT * Convert.ToDouble(lbtotprice.Text);
        cbvat_SelectedIndexChanged(sender, e);

    }

    protected void txqty_TextChanged(object sender, EventArgs e)
    {

    }

    protected void btReset_Click(object sender, EventArgs e)
    {
        txitemsearch.Text = string.Empty;
        lbvat.Text = string.Empty;
        lbprice.Text = string.Empty;
        lbexp.Text = "EXPIRED";
        txitemsearch.CssClass = cd.csstext;
        cbuom.SelectedValue = "";
        txqty.Text = string.Empty;
        txcustprice.CssClass = string.Empty;
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList2(string prefixText, int count, string contextKey)
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
        bll.vSearchMstItemBySalespoint(arr, ref rs);
        while (rs.Read())
        {
            sItem = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["item_cd"].ToString() + "-" + rs["item_nm"].ToString() + "-" + rs["size"].ToString() + "-" + rs["branded_nm"].ToString(), rs["item_cd"].ToString());
            lItem.Add(sItem);

        }
        rs.Close();
        return (lItem.ToArray());
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetEmployeeList(string prefixText, int count, string contextKey)
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
        bll.vSearchMstEmployee(arr, ref rs);
        while (rs.Read())
        {
            sEmployee = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["emp_cd"].ToString() + "-" + rs["emp_nm"], rs["emp_cd"].ToString());
            lEmployee.Add(sEmployee);
        }
        return (lEmployee.ToArray());
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lCust = new List<string>();
        string sCust = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@salesman_cd", contextKey));
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        arr.Add(new cArrayList("@cust_cd", prefixText));
        //bll.vSearchCustomerBySales(arr, ref rs);
        bll.vSearchMstCustomerInRPS(arr, ref rs);
        while (rs.Read())
        {
            sCust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + "-" + rs["cust_nm"].ToString(), rs["cust_cd"].ToString());
            lCust.Add(sCust);
        }
        rs.Close();
        return (lCust.ToArray());
    }

    protected void btprice_Click(object sender, EventArgs e)
    {
        if (hdcust.Value.Equals(DBNull.Value) || hdcust.Value == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Warning','Price can not determined because customer not yet selected !','warning');", true);
            txitemsearch.Text = "";
            hditem.Value = "";
            return;
        }

        double dConv = Convert.ToDouble(bll.vLookUp("select dbo.fn_convertsalesuom('" + hditem.Value.ToString() + "','" + cbuom.SelectedValue.ToString() + "')"));
        if (dConv == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There is no price setup or no setup UOM conversion!','Contact to wazaran admin','warning');", true);
            //lbstock.Text = "";
            lbprice.Text = "";
            cbuom.SelectedValue = "";
            return;
        }
        string sItemBlock = bll.vLookUp("select dbo.fn_checkitemblock('" + hdcust.Value.ToString() + "','" + hditem.Value.ToString() + "')");
        if (sItemBlock != "ok")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Item:" + hditem.Value.ToString() + "','Blocked for customer : " + hdcust.Value.ToString() + "','warning');", true);
            return;
        }
        //Check Adjustment Price
        double dPrice = 0;
        string sCustType = "";
        sCustType = bll.vLookUp("select otlcd from tmst_customer where cust_cd='" + hdcust.Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
        double dAdjust = Convert.ToDouble(bll.vLookUp("select dbo.fn_getadjustmentprice ('" + hditem.Value.ToString() + "','" + hdcust.Value.ToString() + "','" + cbuom.SelectedValue.ToString() + "')"));
        if (dAdjust > 0)
        {
            dPrice = dAdjust;
        }
        else
        {

            dPrice = bll.dGetItemPrice(hditem.Value.ToString(), sCustType, cbuom.SelectedValue.ToString());
        }
        if (dPrice == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Price not yet setup or item conversion not setup!','Contact wazaran admin','warning');", true);
            lbprice.Text = "";
            cbuom.SelectedValue = "";
            return;
        }
        lbprice.Text = dPrice.ToString();
        txitemsearch.CssClass = cd.csstextro;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
    }

    protected void rdreturtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vDelWrkSalesReturn(arr);
        //grdinv.DataSource = null;
        //grdinv.DataBind();
        bll.vBindingGridToSp(ref grd, "sp_twrk_salesreturn_get", arr);
        hdcust.Value = "";
        //txsearchcust.Text = "";
        //lbaddress.Text = "";
        //lbcity.Text = ""; lbamt.Text = ""; lbcl.Text = "0"; lbcusttype.Text = ""; lbexp.Text = ""; lbprice.Text = "";
        lbtotprice.Text = ""; lbwhs.Text = ""; //cbsalesman.Text = "";
        arr.Clear();

        if (rdreturtype.SelectedValue.ToString() == "I")
        {
            lbwhs.Text = "Bin Warehouse";
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingComboToSp(ref cbwhs, "sp_tmst_warehouse_get", "whs_cd", "whs_nm", arr);
        }
        else if (rdreturtype.SelectedValue.ToString() == "C")
        {
            lbwhs.Text = "Bin Van";
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingComboToSp(ref cbwhs, "sp_tmst_vehicle_get", "vhc_cd", "vhc_desc", arr);
        }
        else if (rdreturtype.SelectedValue.ToString() == "F")
        {
            cbwhs.CssClass = cd.csstextro;
            cbbin.CssClass = cd.csstextro;
        }
        cbwhs_SelectedIndexChanged(sender, e);
        //txsearchcust.CssClass = cd.csstext;
        //cbsalesman.Items.Clear(); cbsalesman.CssClass = cd.csstextro;
        txmanualno.CssClass = cd.csstext;
        //btsave.CssClass = "btn btn-warning btn-save";
        //cbreason.CssClass = cd.csstext;
        cbremark.CssClass = cd.csstext;
        //txcustmanualno.CssClass = cd.csstext;
        //txsearchcust.CssClass = cd.csstext;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
        //lbretursta.Text = "NEW RETURN FROM CUSTOMER";
    }

    protected void btsubtotal_Click(object sender, EventArgs e)
    {
        //lbtotprice.Text = "455";
    }

    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbsubtotal = (Label)e.Row.FindControl("lbsubtotal");
            Label lbqty = (Label)e.Row.FindControl("lbqty");
            Label lbcustprice = (Label)e.Row.FindControl("lbcustprice");
            Label lbvat = (Label)e.Row.FindControl("lbvat");
            decimal dqty = Convert.ToDecimal(lbqty.Text);
            decimal dcustprice = Convert.ToDecimal(lbcustprice.Text);
            decimal dvat = Convert.ToDecimal(lbvat.Text);
            decimal dSubTotal = (dqty * dcustprice) + dvat;
            lbsubtotal.Text = dSubTotal.ToString();
        }
    }

    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbitemcode = (Label)grd.Rows[e.RowIndex].FindControl("lbitemcode");
        Label lbexpdate = (Label)grd.Rows[e.RowIndex].FindControl("lbexpdate");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value));
        arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
        arr.Add(new cArrayList("@exp_dt", System.DateTime.ParseExact(lbexpdate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        bll.vDelWrkSalesReturn(arr); arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_twrk_salesreturn_get", arr);
        Label lbtotsubtotal = (Label)grd.FooterRow.FindControl("lbtotsubtotal");
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value));
        string sTotAmt = bll.vLookUp("select sum(custprice * qty) + sum(vat) from twrk_salesreturn where usr_id='" + Request.Cookies["usr_id"].Value + "'");
        lbtotsubtotal.Text = sTotAmt;
    }

    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        System.Data.SqlClient.SqlDataReader rs = null; List<cArrayList> arr = new List<cArrayList>();

        Label lbitemcode = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbitemcode");
        Label lbexpx = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbexp");
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
        arr.Add(new cArrayList("@exp_dt", System.DateTime.ParseExact(lbexpx.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        bll.vGetWrkSalesReturn(arr, ref rs);
        while (rs.Read())
        {
            hditem.Value = lbitemcode.Text;
            txitemsearch.Text = rs["item_nm"].ToString();
            cbuom.SelectedValue = rs["uom"].ToString();
            txqty.Text = rs["qty"].ToString();
            lbprice.Text = rs["unitprice"].ToString();
            txcustprice.Text = rs["custprice"].ToString();
            dtexp.Text = Convert.ToDateTime(rs["exp_dt"]).ToString("d/M/yyyy");
            cbwhs.SelectedValue = rs["whs_cd"].ToString();
            lbtotprice.Text = (Convert.ToDouble(txqty.Text) * Convert.ToDouble(txcustprice.Text)).ToString();
            string sVAT = bll.sGetControlParameter("vat");
            double dVAT = Convert.ToDouble(sVAT);
            double dTotVat = dVAT * Convert.ToDouble(lbtotprice.Text);
            lbvat.Text = dTotVat.ToString();
            lbexp.Text = Convert.ToDateTime(rs["exp_dt"]).ToString("d/M/yyyy");
            dtexp.Text = Convert.ToDateTime(rs["exp_dt"]).ToString("d/M/yyyy");
            cbwhs.CssClass = cd.csstextro;
            cbbin.CssClass = cd.csstextro;
            txqty.CssClass = cd.csstextro;
            dtexp.CssClass = cd.csstextro;
            txitemsearch.CssClass = cd.csstextro;
            txqty.CssClass = cd.csstextro;
            cbuom.CssClass = cd.csstextro;
            txinvoicesearch.CssClass = cd.csstextro;
            txcustprice.CssClass = cd.csstextro;
            cbwhs_SelectedIndexChanged(sender, e);
            cbbin.SelectedValue = rs["bin_cd"].ToString();
            txinvoicesearch.Text = rs["inv_no"].ToString();
            if (txinvoicesearch.Text != string.Empty)
            {
                hdinvoice.Value = txinvoicesearch.Text;
            }
            else { hdinvoice.Value = string.Empty; }
            //txinvoicesearch.CssClass = cd.csstextro;
        }
        rs.Close();
        if (bll.nCheckAccess("editrtv", Request.Cookies["usr_id"].Value.ToString()) == 1)
        {
            txcustprice.CssClass = cd.csstext;
        }
        else { txcustprice.CssClass = cd.csstextro; }
    }

    protected void btsave_Click(object sender, EventArgs e)
    {
        string scd = bll.vLookUp("select  dbo.sfnGetcountdown('"+ Request.Cookies["sp"].Value.ToString() + "')");
        if (scd == "0" && txreqno.Text == "NEW")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Block entry because deadline to daily closing !','Please daily closing !','warning');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
            return;
        }
        if (cbremark.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please Write remark first','Reaturn Remark','warning');", true);
            return;
        }
        string sReturNo = string.Empty; string sEmpCode = string.Empty;
        if (grd.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select item to be returned','select item','warning');", true);
            return;
        }

        if (txmanualno.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Manual No can not empty','select manual no','warning');", true);
            return;
        }

        //string sExistNo = bll.vLookUp("select dbo.fn_checkmanualno('retur','" + txmanualno.Text + "')");
        //if (hdreturno.Value.ToString() != "")
        //{
        //    sExistNo = "ok";
        //}
        //if (sExistNo != "ok")
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('" + sExistNo + "','select manual no','warning');", true);
        //    return;
        //}

        if (rdreturtype.SelectedValue.ToString() == "F")
        {
            if (grd.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select invoice to be returned','select invoice','warning');", true);
                return;
            }
        }
        if (dtrequest.Text == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select return Date','select retur date','warning');", true);
            return;
        }

        if (grdemp.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Cost must be assign to employee','select employee for assign cost bad stock','warning');", true);
            return;
        }

        Label lbtotsubtotal = (Label)grd.FooterRow.FindControl("lbtotsubtotal");
        string sExisting = bll.vLookUp("select sum(amt) from tsalesreturn_costing where retur_no='"+Request.Cookies["usr_id"].Value+"'");
        double dExisting = Convert.ToDouble(sExisting);
        double dSubTotal = Convert.ToDouble(lbtotsubtotal.Text);
        if (dExisting != dSubTotal)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Sum of return is not same with costly for employee','Total return value must same with assigned to employee','warning');", true);
            return;
        }

        // Add Document must be upload for return 

        if (upl.FileName == "" || (upl.FileName.Equals(null)))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('No Document uploaded','Please scan the document and upload','warning');", true);
            return;
        }
        FileInfo fi = new FileInfo(upl.FileName);
        string ext = fi.Extension;
        byte[] fs = upl.FileBytes;
        if (fs.Length <= 5242880)
        {
            if (ext != ".jpg" && ext != ".jpeg" && ext != ".bmp" && ext != ".gif" && ext != ".png" && ext != ".JPEG" && ext != ".JPG" && ext != ".BMP" && ext != ".GIF" && ext != ".PNG" && ext != ".pdf" && ext != ".PDF")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('You should upload image','jpg,bmp,gif,png or pdf upload document again');", true);
                return;
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Image size too big','Image size must be less than 5MB');", true);
            return;
        }

        List<cArrayList> arr = new List<cArrayList>();
        arr.Clear();
        arr.Add(new cArrayList("@retur_typ", rdreturtype.SelectedValue.ToString()));
        arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
        arr.Add(new cArrayList("@retur_dt", DateTime.ParseExact(dtrequest.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@manual_no", txmanualno.Text));
        arr.Add(new cArrayList("@custmanual_no", txmanualno.Text));
        arr.Add(new cArrayList("@remark", cbremark.SelectedValue.ToString()));
        arr.Add(new cArrayList("@returnDriver_cd", cbdriver.SelectedValue.ToString()));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value));
        //arr.Add(new cArrayList("@returcost", cbreturcost.SelectedValue));
        arr.Add(new cArrayList("@emp_cd", hdemp.Value));
        arr.Add(new cArrayList("@remarks", txRemarks.Text));
        bll.vInsertSalesReturn3(arr, ref sReturNo);
        txreqno.Text = sReturNo;
        arr.Clear();
        arr.Add(new cArrayList("@retur_no", sReturNo));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value));
        bll.vUpdateSalesReturnCostingByReturNo(arr);

        // Upload Document to Server 
        upl.SaveAs(bll.sGetControlParameter("image_path") + "/invoice_doc/" + sReturNo.ToString() + ext);
        arr.Clear();
        arr.Add(new cArrayList("@fileinv", sReturNo + ext));
        arr.Add(new cArrayList("@inv_no", sReturNo));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vUploadInvoice(arr);
        arr.Clear();
        arr.Add(new cArrayList("@disc_cd", ""));
        arr.Add(new cArrayList("@isstamp", "1"));
        arr.Add(new cArrayList("@remark", ""));
        arr.Add(new cArrayList("@issign", "1"));
        arr.Add(new cArrayList("@isexclude", "1"));
        arr.Add(new cArrayList("@inv_no", sReturNo));
        arr.Add(new cArrayList("@received_dt", DateTime.ParseExact(dtrequest.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        bll.vInsertClaimConfirm(arr);
        arr.Clear();
        hpfile_nm.Visible = true;
        upl.Visible = false;
        lblocfile.Text = sReturNo.ToString() + ext;
        hpfile_nm.NavigateUrl = "/images/invoice_doc/" + sReturNo.ToString() + ext;
        // Upload Document to Server 


        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report3.aspx?src=retprodspv&returno=" + sReturNo + "');", true);
        btadd.CssClass = "btn btn-default ro";
        btReset.CssClass = "btn btn-default ro";
        //arr.Clear();
        //arr.Add(new cArrayList("@stockcard_typ", "RETURNCUST"));
        //arr.Add(new cArrayList("@refno", sReturNo));
        //bll.vtacc_stock_cardBySalesReturn(arr);

        //if (bZero)
        //{
        try
        {
            int nrnd; System.Data.SqlClient.SqlDataReader rs = null;
            List<string> lapproval = bll.lGetApproval("return", 1); //Old version I am not remove (IAG)
            arr.Clear();
            arr.Add(new cArrayList("@emp_cd", hdsalesman.Value));
            bll.vGetDirectSpv(arr, ref rs);
            while (rs.Read())
            {
                lapproval[0] = rs["mobile_no"].ToString();
                lapproval[1] = rs["email"].ToString();
            }
            rs.Close();

            string sSubject = "New Return Request";
            string sItem = "";
            // System.Data.SqlClient.SqlDataReader rs = null;
            Random rnd = new Random();
            nrnd = rnd.Next(1000, 9999);
            //string sMsg = "Dear Product Supervisor , <br<br>New retur product request with no." + sReturNo + "<br><br>" +
            //"Please approved or reject this request via SMS already sent.<br><br>Best Regards,<br><br>Wazaran Admin.";
            string ssalespoint = Request.Cookies["sp"].Value.ToString();
            string scustomer = bll.vLookUp("select cust_cd+'''-'''+cust_nm from tmst_customer where cust_cd='" + hdcust.Value.ToString() + "'and salespointcd='" + ssalespoint + "'");
            //string saddress = lbaddress.Text + "-'" + lbcity.Text;
            string slastreturnamt = bll.vLookUp("select sum(custprice) from tsalesreturn inner join tsalesreturn_dtl on tsalesreturn.retur_no=tsalesreturn_dtl.retur_no and tsalesreturn.salespointcd=tsalesreturn_dtl.salespointcd where tsalesreturn.retur_no !='" + txreqno.Text + "'  and cust_cd='" + hdcust.Value.ToString() + "'and tsalesreturn.salespointcd='" + ssalespoint + "' group by retur_dt,tsalesreturn.retur_no order by retur_dt desc ");
            string slastgrandtotalreturnamt = bll.vLookUp("select sum(custprice) from tsalesreturn inner join tsalesreturn_dtl on tsalesreturn.retur_no=tsalesreturn_dtl.retur_no and tsalesreturn.salespointcd=tsalesreturn_dtl.salespointcd where year(retur_dt)=year('" + DateTime.ParseExact(dtrequest.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture) + "')  and cust_cd='" + hdcust.Value.ToString() + "'and tsalesreturn.salespointcd='" + ssalespoint + "'");
            string sMsg = "<html><head><div><h2>Sales Retur</h2></div></head><body><table><tr><td>Salespoint</td><td>:</td><td>" + ssalespoint + "</td></tr><tr><td>Customer</td><td>:</td><td>" + scustomer + "</td></tr><tr><td>Adress</td><td>:</td><td>" + "" + "></td></tr><tr><td>Salesman</td><td>:</td><td>" + txsalesman.Text + "</td></tr><tr><td>Return No.</td><td>:</td><td>" + txreqno.Text + "</td></tr><tr><td>Manual No.</td><td>:</td><td>" + txreqno.Text + "</td></tr><tr><td colspan=3>Last Return Amount</td><td>:</td><td>" + slastreturnamt + "</td></tr><tr><td colspan=3>Last Grand Total Receipt Return</td><td>:</td><td>" + slastgrandtotalreturnamt + "</td></tr></table><table><tr><td>Code</td><td>Item</td><td>Brand</td><td>Size</td><td>Qty</td><td>Unit Price</td><td>Amount</td><td>Cust Amt</td><td>Exp Date</td><td>Bin</td></tr>";
            arr.Clear();
            arr.Add(new cArrayList("@retur_no", txreqno.Text));
            arr.Add(new cArrayList("@salespointcd", ssalespoint));
            bll.vGettsalesreturn_dtl(arr, ref rs);
            while (rs.Read())
            {
                DateTime dtdate = Convert.ToDateTime(rs["exp_dt"]);
                string sdate = dtdate.ToShortDateString(); //String.Format("{0:d/M/yyyy}", dtdate);
                                                           //DateTime dtdate = DateTime.ParseExact(sdate, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                sItem += "<tr><td>" + rs["item_cd"].ToString() + "</td><td>" + rs["item_nm"] + "</td><td>" + rs["branded_nm"] + "</td><td>" + rs["size"] + "</td><td>" + rs["qty"].ToString() + "</td><td>" + rs["unitprice"] + "</td><td>" + rs["subtotal"] + "</td><td>" + rs["custprice"] + "</td><td>" + sdate + "</td><td>" + rs["bin_cd"] + "</td><td></tr>";
            }
            rs.Close();
            sMsg += sItem + "</table></body></html>\r\n\r\n\r\n Wazaran Admin";
            if (rdreturtype.SelectedValue.ToString() != "F")
            {

                //string sSMS = "New Retur No." + sReturNo + " has requested by " + bll.sGetSalespointname(Request.Cookies["sp"].Value.ToString());
                //sSMS += "Please approve with reply Y or N (ex. Y1000) token:" + nrnd.ToString();
                string ssp = Request.Cookies["sp"].Value.ToString();
                string sreturnamt = bll.vLookUp("select sum(qty*custprice) from tsalesreturn inner join tsalesreturn_dtl on tsalesreturn.retur_no=tsalesreturn_dtl.retur_no and tsalesreturn.salespointcd=tsalesreturn_dtl.salespointcd where tsalesreturn.retur_no='" + txreqno.Text + "'and tsalesreturn.salespointcd='" + ssp + "'");
                string sSMS = bll.vLookUp("select [dbo].[fn_smsappsalesreturn]('" + ssp + "','" + hdcust.Value + "'," + sreturnamt + ")");
                //  cd.vSendSms(sSMS + nrnd.ToString(), lapproval[0]);
                bll.vSendMail(lapproval[1], sSubject, sMsg);
                arr.Clear();
                arr.Add(new cArrayList("@token", nrnd.ToString()));
                arr.Add(new cArrayList("@doc_no", sReturNo));
                arr.Add(new cArrayList("@doc_typ", "retur"));
                arr.Add(new cArrayList("@to", lapproval[0]));
                arr.Add(new cArrayList("@msg", sSMS.TrimEnd() + nrnd.ToString()));
                bll.vInsertSmsOutbox(arr);

            }
        }
        catch (Exception ex)
        {
            arr.Clear();
            arr.Add(new cArrayList("@err_source", "Retur Save"));
            arr.Add(new cArrayList("@err_description", ex.Message.ToString()));
            bll.vInsertErrorLog(arr);
        }
        //  btsave.Visible = false; btprint.Visible = true;}
        //}
        btsave.CssClass = "divhid"; btprint.CssClass = "btn btn-info btn-print";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Retur has save succeeded','Retur No." + sReturNo + " need approval from BRN SPV','success');", true);
    }

    protected void btsearchcust_Click(object sender, EventArgs e)
    {
        //   string sSalesman = bll.vLookUp("select salesman_cd from tmst_customer where cust_cd='" + hdcust.Value + "'");
        //  hdsalesman.Value = sSalesman;
        //  txsalesman.Text = bll.sGetEmployeeName(sSalesman);
        txcustomer.CssClass = cd.csstextro;
        txsalesman.CssClass = cd.csstext;
    }

    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_reqreturnexpired.aspx");
    }

    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(),  Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=rtn&returno=" + txreqno.Text + "');", true);
    }

    protected void dtexp_TextChanged1(object sender, EventArgs e)
    {

    }

    protected void btsearch_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "PopupCenter('lookupreturnbs.aspx','Lookup',800,600);", true);
    }

    protected void btreturn_Click(object sender, EventArgs e)
    {
        System.Data.SqlClient.SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@retur_no", hdreturno.Value));
        bll.vGetSalesReturn(ref rs, arr);
        while (rs.Read())
        {
            txreqno.Text = hdreturno.Value;
            txmanualno.Text = rs["manual_no"].ToString();
            txcustomer.Text = bll.vLookUp("select cust_cd+':'+cust_nm from tmst_customer where cust_cd='"+ rs["cust_cd"].ToString() + "'");
         //   txsalesman.Text = bll.vLookUp("select (emp_cd +':'+ (select emp_nm from tmst_employee where emp_cd=tsalesreturn_info.emp_cd)) from tsalesreturn_info where retur_no='"+hdreturno.Value+"'");
            cbremark.SelectedValue = rs["remark"].ToString();
            cbdriver.SelectedValue = rs["returndriver_cd"].ToString();
            cbdriver.CssClass = cd.csstextro;
            cbremark.CssClass = cd.csstextro;
            txreqno.CssClass = cd.csstextro;
            txcustomer.CssClass = cd.csstextro;
            txmanualno.CssClass = cd.csstextro;
            txsalesman.CssClass = cd.csstextro;
            arr.Clear();
            arr.Add(new cArrayList("@retur_no", hdreturno.Value));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value));
            bll.vInsertWrkSalesreturnFromCore(arr);
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value));
            bll.vBindingGridToSp(ref grd, "sp_twrk_salesreturn_get", arr);
            grdemp.CssClass = "mGrid ro";
            arr.Clear();
            arr.Add(new cArrayList("@retur_no", hdreturno.Value));
            bll.vBindingGridToSp(ref grdemp, "sp_tsalesreturn_costing_get", arr);

            btprint.CssClass = "btn btn-info";
            btadd.CssClass = "btn btn-default ro";
            btReset.CssClass = "btn btn-default ro";
        }
        rs.Close();
    }

    protected void btemp_Click(object sender, EventArgs e)
    {
        lbjobtitle.Text =  bll.vLookUp("select fld_desc from tfield_value where fld_nm='job_title_cd' and fld_valu=(select job_title_cd from tmst_employee where emp_cd='"+hdemp.Value+"')");

    }

    protected void btaddamt_Click(object sender, EventArgs e)
    {
        double dAmt = 0;
        if (!double.TryParse(txempamt.Text, out dAmt))
            {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Amount must numeric','Check Amount','warning');", true);
            return;
        }

        if (grd.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There is no return must be costly to employee','Please select return with value','warning');", true);
            return;

        }
        Label lbtotsubtotal = (Label)grd.FooterRow.FindControl("lbtotsubtotal");
        double dSubTotal = Convert.ToDouble( lbtotsubtotal.Text);
        string sExistTotal = bll.vLookUp("select sum(amt) from tsalesreturn_costing where retur_no='"+Request.Cookies["usr_id"].Value+"'");
        if (sExistTotal == string.Empty)
        {
            sExistTotal = "0";
        }
        double dExistTotal = Convert.ToDouble(sExistTotal);
        if ((dAmt + dExistTotal) > dSubTotal)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Over cost return to employee','Please reduce for change total amount for employee cost','warning');", true);
            return;
        }

        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@emp_cd", hdemp.Value));
        arr.Add(new cArrayList("@retur_no", Request.Cookies["usr_id"].Value));
        arr.Add(new cArrayList("@amt", dAmt));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        bll.vInsertSalesReturnCosting(arr);
        arr.Clear();
        arr.Add(new cArrayList("@retur_no", Request.Cookies["usr_id"].Value));
        bll.vBindingGridToSp(ref grdemp, "sp_tsalesreturn_costing_get", arr);
        txsalesman.Text = string.Empty;
        lbjobtitle.Text = string.Empty;
        txempamt.Text = string.Empty;

    }

    protected void grdemp_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbempcode = (Label)grdemp.Rows[e.RowIndex].FindControl("lbempcode");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@emp_cd", lbempcode.Text));
        arr.Add(new cArrayList("@retur_no", Request.Cookies["usr_id"].Value));
        bll.vDelSalesReturnCosting(arr);
        arr.Clear();
        arr.Add(new cArrayList("@retur_no", Request.Cookies["usr_id"].Value));
        bll.vBindingGridToSp(ref grdemp, "sp_tsalesreturn_costing_get", arr);
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetInvoiceList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        HttpCookie cok;
        //cok = HttpContext.Current.Request.Cookies["sp"];
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lCust = new List<string>();
        string sCust = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@salesman_cd", contextKey));
        string[] arritem = contextKey.Split(';');
        arr.Add(new cArrayList("@item_cd", arritem[1]));
        arr.Add(new cArrayList("@cust_cd", arritem[0]));
        arr.Add(new cArrayList("@inv_no", prefixText));
        bll.vSearchInvoiceByItem(ref rs, arr);
        while (rs.Read())
        {
            sCust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["inv_no"].ToString(), rs["inv_no"].ToString());
            lCust.Add(sCust);
        }
        rs.Close();
        return (lCust.ToArray());
    }

    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        TextBox txunitprice = (TextBox)grd.Rows[e.RowIndex].FindControl("txunitprice");
        Label lbitemcode = (Label)grd.Rows[e.RowIndex].FindControl("lbitemcode");
        Label lbqty = (Label)grd.Rows[e.RowIndex].FindControl("lbqty");
        Label lbexp = (Label)grd.Rows[e.RowIndex].FindControl("lbexp");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
        arr.Add(new cArrayList("@unitprice", txunitprice.Text));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@qty", lbqty.Text));
        arr.Add(new cArrayList("@exp_dt", System.DateTime.ParseExact(lbexp.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        bll.vUpdateWrkSalesReturn(arr);
        grd.EditIndex = -1;
        arr.Clear();
        BindingGrid();
    }

    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        grd.EditIndex = e.NewEditIndex;
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_twrk_salesreturn_get", arr);
    }

    void BindingGrid()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_twrk_salesreturn_get", arr);
    }

    protected void btinvselect_Click(object sender, EventArgs e)
    {
        string sQtyAvl = bll.vLookUp("select dbo.fn_checkqtyavlreturn('" + hdinvoice.Value + "','" + hditem.Value + "','" + Request.Cookies["usr_id"].Value + "')");
        lbqtyavl.Text = bll.vLookUp("select dbo.fn_ItemConversion('CTN','" + cbuom.SelectedValue + "','" + hditem.Value + "'," + sQtyAvl + ")");
    }

    protected void btcheckprice_Click(object sender, EventArgs e)
    {
        if (hdcust.Value.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Price can not be determined','Customer Not yet selected','warning');", true);
            txitemsearch.Text = string.Empty; hditem.Value = string.Empty; hdinvoice.Value = string.Empty;
            return;
        }

        bool bTemp = Convert.ToBoolean(bll.vLookUp("select dbo.fn_checkcustreturninvoice('" + hdcust.Value + "')"));
        if (bTemp == true)
        {
            txinvoicesearch.Text = bll.vLookUp("select dbo.fn_getinvoicereturn('" + hdcust.Value + "','" + hditem.Value + "','" + Request.Cookies["usr_id"].Value + "')");

            if ((txinvoicesearch.Text == string.Empty) || (txinvoicesearch.Text == ""))
            {
                hdinvoice.Value = string.Empty;
                txinvoicesearch.Text = "Invoice not found";
                txinvoicesearch.CssClass = cd.csstextro;
            }
            else
            {
                hdinvoice.Value = txinvoicesearch.Text;
                lbqtyavl.Text = bll.vLookUp("select dbo.fn_checkqtyavlreturn('" + hdinvoice.Value + "','" + hditem.Value + "','" + Request.Cookies["usr_id"].Value + "')");
            }

        }
        else
        {
            hdinvoice.Value = string.Empty;
            txinvoicesearch.Text = "Invoice not found";
            txinvoicesearch.CssClass = cd.csstextro;
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
        txinvoicesearch.ForeColor = System.Drawing.Color.Red;
        txitemsearch.CssClass = cd.csstextro;
        //txinvoicesearch.CssClass = cd.csstextro;
        //hdinvoice.Value = txinvoicesearch.Text;
        cbuom.SelectedValue = "";
        txinvoicesearch_AutoCompleteExtender.ContextKey = hdcust.Value + ";" + hditem.Value;
    }
}