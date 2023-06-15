using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using AjaxControlToolkit;

public partial class fm_bor : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vDelWrkBor(arr);
            bll.vBindingFieldValueToCombo(ref cbstatus, "bor_sta_id");
            bll.vBindingFieldValueToCombo(ref cbdestinatio, "po_typ",false);
            lbsalespoint.Text = bll.vLookUp("select salespointcd+':'+salespoint_nm from tmst_salespoint where salespointcd='"+Request.Cookies["sp"].Value.ToString()+"'");
            dtbor_CalendarExtender.StartDate = System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            dtestimate_CalendarExtender.StartDate = System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            dtbor.Text = Request.Cookies["waz_dt"].Value.ToString();
        }
    }
    protected void cbdestinatio_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbdestinatio.SelectedValue == "CU")
        {
            txcustomer.CssClass = "form-control";
        }
        else if (cbdestinatio.SelectedValue == "DP")
        {
            txcustomer.Text = bll.vLookUp("select top 1 whs_nm from tmst_warehouse where salespointcd='"+Request.Cookies["sp"].Value.ToString()+"'");
            txcustomer.CssClass = "form-control ro";
        }
        else if (cbdestinatio.SelectedValue == "SD")
        {
            txcustomer.Text = "-";
            txcustomer.CssClass = "form-control ro";
        }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll(); SqlDataReader rs = null;
        string sItem = string.Empty;
        List<string> lItem = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@item_nm", prefixText));
        bll.vSearchMstItem2(arr, ref rs);
        while (rs.Read())
        {
            sItem = AutoCompleteExtender.CreateAutoCompleteItem(rs["item_cd"].ToString() + "-" + rs["item_nm"].ToString() + "'-" + rs["size"].ToString() + "-" + rs["branded_nm"].ToString(), rs["item_cd"].ToString());
            lItem.Add(sItem);
        }
        rs.Close();
        return (lItem.ToArray());
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@item_cd", hditem.Value.ToString()));
        bll.vGetMstItem2(arr, ref rs);
        while (rs.Read())
        { 
            lbbranded.Text = rs["branded_nm"].ToString();
            lbsize.Text = rs["size"].ToString();
            string sWhs = bll.vLookUp("select top 1 whs_cd from tmst_warehouse where salespointcd='"+Request.Cookies["sp"].Value.ToString()+"'");
            lbcurrentstock.Text = bll.vLookUp("select dbo.sfnGetStock('"+hditem.Value.ToString()+"','GS','"+sWhs+"','DEPO',dbo.fn_getsystemdate())");
            lbunitprice.Text = bll.vLookUp("select unitprice from tcustomertype_price where item_cd='"+hditem.Value.ToString()+"' and cust_typ='BRN'");
        }
        rs.Close();
        bll.vBindingFieldValueToCombo(ref cbuom, "uom");
        cbuom.SelectedValue = "CTN"; cbuom.CssClass = "form-control ro";
    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        double dOut = 0;
        if (!double.TryParse(txqty.Text, out dOut))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Quantity order must numeric!','Check Quantity Order','warning');", true);
            return;
        }

        if (cbuom.SelectedValue == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Pls select UOM!','Check UOM','warning');", true);
            return;
        }

        if (lbunitprice.Text == "")
        {
         ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('No Branch Price setup for this item!','Contact to Claim HO','warning');", true);
            return;
        }

        if (hditem.Value == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Item is not selected!','Pls select item','warning');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@item_cd", hditem.Value.ToString()));
        arr.Add(new cArrayList("@stock_amt", lbcurrentstock.Text));
        arr.Add(new cArrayList("@qty", txqty.Text));
        arr.Add(new cArrayList("@unitprice", lbunitprice.Text));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vInsertWrkBOR(arr);
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_twrk_bor_get", arr);
        
        hdcust.Value = "";
        txitemsearch.Text = "";
        lbbranded.Text = "";
        lbcurrentstock.Text = "";
        lbsize.Text = "";
        lbunitprice.Text = "";
        
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList2(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll(); SqlDataReader rs = null;
        HttpCookie cok = HttpContext.Current.Request.Cookies["sp"];
        string sSP = cok.Value.ToString();
        string sCust = string.Empty;
        List<string> lCust = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cust_cd", prefixText));
        arr.Add(new cArrayList("@salespointcd", sSP));
        bll.vSearchMstCustomer(arr, ref rs);
        while (rs.Read())
        {
            sCust = AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"] + " - " + rs["cust_nm"].ToString(), rs["cust_cd"].ToString());
            lCust.Add(sCust);
        }
        rs.Close();
        return (lCust.ToArray());
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        if (grd.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Select item to be ordered!','Item','warning');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@bor_dt", dtbor.Text));
    }
}