using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_mstsalesorderentry : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vDeleteWrkSalesOrderDtl(arr);
            bll.vDelSalesOrderFreeItem(arr);
            arr.Clear();
            arr.Add(new cArrayList("@qry_cd", "SalesJob"));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingComboToSp(ref cbsalesman, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
            bll.vBindingFieldValueToCombo(ref cbsourceorder, "so_source");
            bll.vBindingFieldValueToCombo(ref cbsotype, "so_typ");
            bll.vBindingFieldValueToCombo(ref cbuom, "uom");
            cbsalesman_SelectedIndexChanged(sender, e);
            lbsalespoint.Text = bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            dtorder.Text = System.DateTime.Today.ToShortDateString();
            bll.sFormat2ddmmyyyy(ref dtorder);
            txorderno.Text = "NEW";
        }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        System.Data.SqlClient.SqlDataReader  rs = null;
        List<string> lCust = new List<string>();
        string sCust = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salesman_cd", contextKey));
        arr.Add(new cArrayList("@cust_cd", prefixText));
        bll.vSearchCustomerBySales(arr, ref rs);
        while (rs.Read())
        { 
            sCust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + "-" + rs["cust_nm"].ToString() , rs["cust_cd"].ToString());
            lCust.Add(sCust);
        }
        rs.Close();
        return (lCust.ToArray());
    }
    protected void cbsalesman_SelectedIndexChanged(object sender, EventArgs e)
    {
        txcustomer_AutoCompleteExtender.ContextKey = cbsalesman.SelectedValue.ToString();
        txcustomer.Text = "";
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        if (txcustomer.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "alert('Please type for search customer ! | يرجى كتابة العملاء');", true);
                return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        System.Data.SqlClient.SqlDataReader rs = null;
        arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
        bll.vGetMstCustomer(arr, ref rs);
        while (rs.Read())
        {
            lbaddress.Text = rs["addr"].ToString();
            var culture = System.Globalization.CultureInfo.GetCultureInfo("id-ID");
            lbcredit.Text =  string.Format(culture, "{0:n}", Convert.ToDouble( rs["credit_limit"]));
            lbcusttype.Text = rs["otlnm"].ToString();
            lbterm.Text = rs["term"].ToString();
        }
        rs.Close();

    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        double dresult;
        if (!double.TryParse(txstockcust.Text, out dresult))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "alert('Please fill customer stock !');", true);
            return;
        }

        if (hditem.Value.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "alert('Please select item  !');", true);
            return;
        }
       
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@item_cd", hditem.Value.ToString()));
        arr.Add(new cArrayList("@qty", txqty.Text));
        arr.Add(new cArrayList("@stock_cust", txstockcust.Text));
        arr.Add(new cArrayList("@unitprice", lbprice.Text));
        arr.Add(new cArrayList("@uom", cbuom.SelectedValue.ToString()));
        bll.vInsertWrkSalesORderDtl(arr);
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_twrk_salesorderdtl_get", arr);
        lbprice.Text = "0";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "ItemClear();", true);
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetItemListX(string prefixText, int count, string contextKey)
    {
        HttpCookie cook ;
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
            sItem = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["item_cd"].ToString() + "-" + rs["item_nm"].ToString(), rs["item_cd"].ToString());
            lItem.Add(sItem);

        }
        rs.Close();
        return (lItem.ToArray());
    }
    protected void btdisc_Click(object sender, EventArgs e)
    {
        if (hdcust.Value.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "alert('Please select customer !');", true);
            return;
        }
        if (grd.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "alert('You have not item to be calculated  !!');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
        bll.vBatchSalesOrderDiscount(arr);
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grddisc, "sp_twrk_salesdiscount_get", arr);
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        string sTermCd="";
        string sSoNo="";
        if (dtorder.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "alert('Please order date !');", true);
            return;
        }
        if (grd.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "alert('Order item can not empty ');", true);
            return;
        }
        
        sTermCd = bll.vLookUp("select payment_term from tmst_customer where cust_cd='" + hdcust.Value.ToString() + "'");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@so_dt", DateTime.ParseExact( dtorder.Text,"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@so_typ", cbsotype.SelectedValue.ToString()));
        arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
        arr.Add(new cArrayList("@salesman_cd", cbsalesman.SelectedValue.ToString()));
        arr.Add(new cArrayList("@term_cd", sTermCd));
        arr.Add(new cArrayList("@currency_cd","SAR"));
        arr.Add(new cArrayList("@ref_no",""));
        arr.Add(new cArrayList("@remark", txremark.Text));
        arr.Add(new cArrayList("@so_sta_id","N"));
        arr.Add(new cArrayList("@so_source", cbsourceorder.SelectedValue.ToString()));
        bll.vInsertMstSalesOrder(arr, ref sSoNo);
        txorderno.Text = sSoNo;
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@so_cd", sSoNo));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vInsertSalesOrderDtl(arr);
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@so_cd", sSoNo));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vInsertSalesOrderDiscount(arr);
        btsave.Enabled = false;
      //  btsave.CssClass = "button2 disablesave";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "alert('New sales order has been saved !');", true);
    }
    protected void grddisc_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label lbdisccode = (Label)grddisc.SelectedRow.FindControl("lbdisccode");
        Label lbfreeqty = (Label)grddisc.SelectedRow.FindControl("lbfreeqty");
      //  List<cArrayList> arr = new List<cArrayList>();
      //  arr.Add(new cArrayList("@disc_cd", lbdisccode.Text));
      //  bll.vBindingGridToSp(ref grdfree, "sp_tdiscount_freeitem_get", arr);
        ScriptManager.RegisterStartupScript(Page,Page.GetType(),"op","openwindow('fm_lookupitem.aspx?dc=" + lbdisccode.Text +    "&f=" + lbfreeqty.Text  + "');",true);
    }
    protected void btfree_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdfree, "sp_twrk_salesorderfreeitem_get", arr);
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=so&so=" + txorderno.Text +  "');",true);
    }
    protected void btprice_Click(object sender, EventArgs e)
    {
        if (hdcust.Value.Equals(DBNull.Value) || hdcust.Value == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al2", "alert('Price can not determined because customer not yet selected !');", true);
            return;
        }
        string sCustType = "";
        sCustType = bll.vLookUp("select otlcd from tmst_customer where cust_cd='" + hdcust.Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
        string sPrice = bll.vLookUp("select unitprice from tcustomertype_price where item_cd='" + hditem.Value.ToString() + "' and cust_typ='" + sCustType + "'");
        lbprice.Text = sPrice;
    }
    protected void cbsourceorder_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}