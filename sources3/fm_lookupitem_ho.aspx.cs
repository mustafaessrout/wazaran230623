using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_lookupitem_ho : System.Web.UI.Page
{
    cbll bll = new cbll();
    double unitprice = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sType = string.Empty;
            lbuomfree.Text = Request.QueryString["u"];
            string suom = Request.QueryString["u"];

            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vDelWrkLookupItem(arr);
            //  bll.vDelSalesOrderFreeItem(arr);
            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", Request.QueryString["sp"]));
            if (Request.QueryString["t"] != null)
            {
                sType = Request.QueryString["t"];
            }
            bll.vBindingFieldValueToCombo(ref cbuomfree, "uom");
            cbuomfree.SelectedValue = suom;
            cbuomfree.CssClass = "ro";
            string sDiscNo = Request.QueryString["dc"];
            string sFreeItemProduct = bll.vLookUp("select rdfreeitem from tmst_discount where disc_cd='" + sDiscNo + "'");
            arr.Clear();
            if (sFreeItemProduct == "I")
            {
                arr.Add(new cArrayList("@disc_cd", Request.QueryString["dc"].ToString()));
                arr.Add(new cArrayList("@uom", cbuomfree.SelectedValue.ToString()));
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                bll.vBindingGridToSp(ref grdsearch, "sp_tdiscount_freeitem_ho", arr);
            }
            else if (sFreeItemProduct == "G")
            {
                arr.Add(new cArrayList("@disc_cd", Request.QueryString["dc"].ToString()));
                arr.Add(new cArrayList("@uom", cbuomfree.SelectedValue.ToString()));
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                bll.vBindingGridToSp(ref grdsearch, "sp_tdiscount_freeproduct_ho", arr);
            }
            cbuomfree_SelectedIndexChanged(sender, e);

        }
    }

    protected void grdsearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        Label lbitemcode = (Label)grdsearch.SelectedRow.FindControl("lbitemcode");
        Label lbitemname = (Label)grdsearch.SelectedRow.FindControl("lbitemname");
        Label lbarabic = (Label)grdsearch.SelectedRow.FindControl("lbarabic");
        Label lbsize = (Label)grdsearch.SelectedRow.FindControl("lbsize");
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
        arr.Add(new cArrayList("@item_nm", lbitemname.Text));
        arr.Add(new cArrayList("@item_arabic", lbarabic.Text));
        arr.Add(new cArrayList("@size", lbsize.Text));
        bll.vInsertWrkLookupItem(arr);
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "okclose", "window.opener.updpnl()", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "okupd", "window.close()", true);
    }
    protected void grdsearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdsearch.PageIndex = e.NewPageIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Clear();
        arr.Add(new cArrayList("@disc_cd", Request.QueryString["dc"].ToString()));
        bll.vBindingGridToSp(ref grdsearch, "sp_tdiscount_freeitem_ho", arr);

    }
    protected void btprice_Click(string item, string uom, EventArgs e)
    {
        unitprice = 0;
        double dConv = Convert.ToDouble(bll.vLookUp("select dbo.fn_convertsalesuom('" + item + "','" + uom + "')"));
        if (dConv == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There is no price setup or no setup UOM conversion!','Contact to wazaran admin','warning');", true);
            return;
        }
        double dPrice = 0;
        string sCustType = "";
        sCustType = bll.vLookUp("select otlcd from tmst_customer where cust_cd='" + Request.QueryString["cust"] + "' and salespointcd='" + Request.QueryString["sp"] + "'");
        double dAdjust = Convert.ToDouble(bll.vLookUp("select dbo.fn_getadjustmentprice_ho ('" + item + "','" + Request.QueryString["cust"] + "','" + uom + "','" + Request.QueryString["sp"] + "')"));
        if (dAdjust > 0)
        {
            dPrice = dAdjust;
        }
        else
        {

            dPrice = bll.dGetItemPrice(item, sCustType, uom);
        }
        if (dPrice == 0)
        {

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Price not yet setup or item conversion not setup!','Contact wazaran admin','warning');", true);

            return;
        }
        unitprice = dPrice;
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        double dTot = 0; double dTemp = 0;
        List<cArrayList> arr = new List<cArrayList>();
        string sDiscNo = Request.QueryString["dc"].ToString();
        string sUOM = Request.QueryString["u"];
        if (sUOM != cbuomfree.SelectedValue.ToString())
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('UOM Free must same with UOM Deliver !','Check UOM','warning');", true);
            return;

        }
        foreach (GridViewRow row in grdsearch.Rows)
        {
            Label lbitemcode = (Label)row.FindControl("lbitemcode");
            TextBox txqty = (TextBox)row.FindControl("txqty");
            if (!double.TryParse(txqty.Text, out dTot))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please entry only numeric !','Numeric Input','warning');", true);
                return;
            }
            else
            {
                dTemp += dTot;
            }

        }
        if (dTemp != Convert.ToDouble(Request.QueryString["f"]))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Total free item qty must be same with " + Request.QueryString["f"] + "','Please use same stock amount','warning');", true);
            return;
        }

        foreach (GridViewRow row in grdsearch.Rows)
        {
            Label lbunitprice = (Label)row.FindControl("lbunitprice");
            Label lbitemcode = (Label)row.FindControl("lbitemcode");
            TextBox txqty = (TextBox)row.FindControl("txqty");
            if (txqty.Text != "0")
            {
                arr.Clear();
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
                arr.Add(new cArrayList("@free_qty", txqty.Text));
                arr.Add(new cArrayList("@disc_cd", Request.QueryString["dc"]));
                arr.Add(new cArrayList("@unitprice", lbunitprice.Text));
                bll.vInsertSalesOrderFreeItem(arr);
                
            }
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "SendData();", true);
    }
    
    protected void grdsearch_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string sStock = string.Empty;
            string sStocktwrk = string.Empty;
            string sStockmst = string.Empty;
            double qty;
            Label lbitemcode = (Label)e.Row.FindControl("lbitemcode");
            //Label lbstock = (Label)e.Row.FindControl("lbstock");
            Label lbunitprice = (Label)e.Row.FindControl("lbunitprice");
            btprice_Click(lbitemcode.Text, Request.QueryString["u"], e);
            lbunitprice.Text = unitprice.ToString();
            DateTime ddate = DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            
            string sQTY = bll.vLookUp("select dbo.sfnUomQtyConv('" + lbitemcode.Text + "','CTN','" + cbuomfree.SelectedValue.ToString() + "',1)");
            if (Request.QueryString["so_cd"] == "NEW")
                sStocktwrk = bll.vLookUp("select sum(QTY) from(SELECT dbo.sfnUomQtyConv(item_cd,uom," + "'CTN'" + ",qty_shipment)qty  From twrk_salesorderdtl  WHERE ITEM_CD='" + lbitemcode.Text + "' and usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' union all select sum(free_qty) from twrk_salesorderfreeitem where item_cd='" + lbitemcode.Text + "' and usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "')a");
            else
                sStocktwrk = bll.vLookUp("select sum(free_qty) from twrk_salesorderfreeitem where item_cd='" + lbitemcode.Text + "' and usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "'");

            if (sStockmst == "") { sStockmst = "0"; }
            if (sStocktwrk == "") { sStocktwrk = "0"; }
            qty = (Convert.ToDouble(sStocktwrk)) * Convert.ToDouble(sQTY);
            sStock = Convert.ToString(qty);
            if (sStock == "")
            {
                sStock = "0";
            }
            //lbstock.Text = sStock;
        }
    }

    void vBindingGrid()
    {
        List<cArrayList> arr = new List<cArrayList>();
        string sDiscNo = Request.QueryString["dc"];
        string sFreeItemProduct = bll.vLookUp("select rdfreeitem from tmst_discount where disc_cd='" + sDiscNo + "'");
        string sType = Request.QueryString["t"];
        arr.Clear();
        if (sFreeItemProduct == "I")
        {
            arr.Add(new cArrayList("@disc_cd", Request.QueryString["dc"].ToString()));
            arr.Add(new cArrayList("@uom", cbuomfree.SelectedValue.ToString()));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grdsearch, "sp_tdiscount_freeitem_ho", arr);
        }
        else if (sFreeItemProduct == "G")
        {
            arr.Add(new cArrayList("@disc_cd", Request.QueryString["dc"].ToString()));
            arr.Add(new cArrayList("@uom", cbuomfree.SelectedValue.ToString()));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grdsearch, "sp_tdiscount_freeproduct_ho", arr);
        }
    }
    protected void cbuomfree_SelectedIndexChanged(object sender, EventArgs e)
    {
        grdsearch.DataSource = null;
        grdsearch.DataBind();
        vBindingGrid();
    }
}