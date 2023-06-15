using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using AjaxControlToolkit;
public partial class fm_adjustpricelist : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingFieldValueToCombo(ref cbadjp_type, "adjp_type");
            bll.vBindingFieldValueToCombo(ref cbotlcd, "otlcd");
            bll.vBindingFieldValueToCombo(ref cbcusgrcd, "cusgrcd");
            cbadjp_type_SelectedIndexChanged(sender, e);
            bindinggrd();
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmAdjustPrice.aspx");
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        string sadjp_cd = "0";
        if (txstart_dt.Text  == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('','Receipt Date must be fill','danger')", true);
            return;
        }
        if (cbadjp_type.SelectedValue == "cust_cd" && hdcust_cd.Value == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('','Customer must be fill','danger')", true);
            return;
        }

       // bll.vLookUp("select dbo.fn_getadjustmentprice('" + hdcust_cd.Value.ToString() +  "')");

        if (txadjp_cd.Text == null || txadjp_cd.Text == "")
        {
            List<cArrayList> arr = new List<cArrayList>();
           
            arr.Add(new cArrayList("@start_dt", DateTime.ParseExact(txstart_dt.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@adjp_type", cbadjp_type.SelectedValue));
            arr.Add(new cArrayList("@SalesPointCD",hdSalesPointCD.Value));
            arr.Add(new cArrayList("@cust_cd",hdcust_cd.Value));
            arr.Add(new cArrayList("@otlcd", hdotlcd.Value));
            arr.Add(new cArrayList("@cusgrcd",hdcusgrcd.Value));
            arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
            bll.vInserttitem_adjustment_price_ins(arr, ref sadjp_cd);
            txadjp_cd.Text = sadjp_cd;

        }
        else
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@adjp_cd",txadjp_cd.Text));
            arr.Add(new cArrayList("@start_dt", DateTime.ParseExact(txstart_dt.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@adjp_type", cbadjp_type.SelectedValue));
            arr.Add(new cArrayList("@SalesPointCD", hdSalesPointCD.Value));
            arr.Add(new cArrayList("@cust_cd", hdcust_cd.Value));
            arr.Add(new cArrayList("@otlcd", hdotlcd.Value));
            arr.Add(new cArrayList("@cusgrcd", hdcusgrcd.Value));
            bll.vUpdateitem_adjustment_price(arr);
        }
        bindinggrd();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Adjustment price saved successfully','" + sadjp_cd + "','success');", true);
    }
    protected void btDelete_Click(object sender, EventArgs e)
    {
        if (txadjp_cd.Text != "")
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@adjp_cd", txadjp_cd.Text));
            bll.vDeletetitem_adjustment_price(arr);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('','Data Deleted successfully !','success')", true);
            Response.Redirect("frmAdjustPrice.aspx");
        }
    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        cbadjp_type_SelectedIndexChanged(sender, e);
        txsearchitemPnl.CssClass = "";
        txamtPnl.CssClass = "";

        string adjp_cd;

        if (hditem_cd.Value == "" || txamt.Text == "" || txamt.Text == "0")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "swal('Alert', 'Item Name and Amount must be fill', 'error');", true);
            if (txsearchitem.Text == "")
            {
                txsearchitemPnl.CssClass = "error";
            }
            if (txamt.Text == "" || txamt.Text == "0")
            {
                txamtPnl.CssClass = "error";
            }
            return;
        }
        if (txadjp_cd.Text == "") { adjp_cd = Request.Cookies["usr_id"].Value.ToString(); } else { adjp_cd = txadjp_cd.Text; }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@adjp_cd", adjp_cd));
        arr.Add(new cArrayList("@item_cd", hditem_cd.Value));
        arr.Add(new cArrayList("@amt",  txamt.Text));
        bll.vInserttitem_adjustment_price_dtl(arr);
        clearAdd();
        txsearchitem.Focus();
        bindinggrd();
    }
    void clearAdd()
    {
        txsearchitem.Text = "";
        hditem_cd.Value = "";
        txamt.Text = "0";
    }
    private void bindinggrd()
    {
        string adjp_cd;
        if (txadjp_cd.Text == "") { adjp_cd = Request.Cookies["usr_id"].Value.ToString(); } else { adjp_cd = txadjp_cd.Text; }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@adjp_cd", adjp_cd));
        bll.vBindingGridToSp(ref grd, "sp_titem_adjustment_price_dtl_get", arr);
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lbitem_cd = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbitem_cd");
        Label lbitem_nm = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbitem_nm");
        Label lbamt = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbamt");

        txsearchitem.Text = lbitem_cd.Text + " | " + lbitem_nm.Text;
        hditem_cd.Value = lbitem_cd.Text;
        txamt.Text = lbamt.Text;  
    }
    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbadjp_cd = (Label)grd.Rows[e.RowIndex].FindControl("lbadjp_cd");
        Label lbitem_cd = (Label)grd.Rows[e.RowIndex].FindControl("lbitem_cd");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@adjp_cd", lbadjp_cd.Text));
        arr.Add(new cArrayList("@item_cd", lbitem_cd.Text));
        bll.vDeletetitem_adjustment_price_dtl(arr);
        bindinggrd();
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListcust(string prefixText, int count, string contextKey)
    {
        //HttpCookie cookieSP;
        //cookieSP = HttpContext.Current.Request.Cookies["sp"];
        cbll bll = new cbll();
        SqlDataReader rs = null;
        string sCust = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        List<string> lCust = new List<string>();
        arr.Add(new cArrayList("@cust_nm", prefixText));
        bll.vSearchMstCust3(arr, ref rs);
        while (rs.Read())
        {
            sCust = AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + " | " + rs["cust_nm"].ToString(), rs["cust_cd2"].ToString());
            lCust.Add(sCust);
        } rs.Close();
        return (lCust.ToArray());
    }

    
    protected void cbadjp_type_SelectedIndexChanged(object sender, EventArgs e)
    {
    if (cbadjp_type.SelectedValue=="cust_cd")
    {
        txsearchcust.Visible = true;
        cbotlcd.Visible = false;
        cbcusgrcd.Visible = false;
        hdotlcd.Value = "";
        hdcusgrcd.Value = "";

    }
        else if (cbadjp_type.SelectedValue=="otlcd")
    {
        txsearchcust.Visible = false;
        cbotlcd.Visible = true;
        cbcusgrcd.Visible = false;
        cbotlcd_SelectedIndexChanged(sender, e);
    }
    else if (cbadjp_type.SelectedValue == "cusgrcd")
    {
        txsearchcust.Visible = false;
        cbotlcd.Visible = false;
        cbcusgrcd.Visible = true;
        cbcusgrcd_SelectedIndexChanged(sender, e);
    }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListitem(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        SqlDataReader rs = null;
        string sItem = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        List<string> lItem = new List<string>();
        arr.Add(new cArrayList("@item_nm", prefixText));
        bll.vSearchMstItem2(arr, ref rs);
        while (rs.Read())
        {
            sItem = AutoCompleteExtender.CreateAutoCompleteItem(rs["item_cd"].ToString() + " | " + rs["item_nm"].ToString(), rs["item_cd"].ToString());
            lItem.Add(sItem);
        } rs.Close();
        return (lItem.ToArray());
    }
    protected void cbotlcd_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdotlcd.Value = cbotlcd.SelectedValue;
        hdSalesPointCD.Value = "";
        hdcust_cd.Value = "";
        hdcusgrcd.Value = "";
    }
    protected void cbcusgrcd_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdcusgrcd.Value = cbcusgrcd.SelectedValue;
        hdSalesPointCD.Value = "";
        hdcust_cd.Value = "";
        hdotlcd.Value = "";
    }
    protected void bttmp_Click(object sender, EventArgs e)
    {
        SqlDataReader rs = null;
        txadjp_cd.Text = Convert.ToString(Session["looadjp_cd"]);
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@adjp_cd", txadjp_cd.Text));
        bll.vGettitem_adjustment_price(arr, ref rs);
        while (rs.Read())
        {
            txstart_dt.Text = string.Format("{0:d/M/yyyy}", rs["start_dt"]);
            cbadjp_type.SelectedValue = rs["adjp_type"].ToString();
            if (rs["adjp_type"].ToString() == "cust_cd")
            {
                txsearchcust.Text = rs["cust_cd"].ToString() + " | " + bll.vLookUp("select cust_nm from tmst_customer where Cust_CD='" + rs["cust_cd"].ToString() + "'");
                hdcust_cd.Value = rs["cust_cd"].ToString();
                hdSalesPointCD.Value = rs["SalesPointCD"].ToString();
                hdotlcd.Value = "";
                hdcusgrcd.Value = "";
            }
            if (rs["adjp_type"].ToString() == "otlcd")
            {
                cbotlcd.SelectedValue = rs["otlcd"].ToString();
                cbotlcd_SelectedIndexChanged(sender, e);
            }
            if (rs["adjp_type"].ToString() == "cusgrcd")
            {
                cbcusgrcd.SelectedValue = rs["cusgrcd"].ToString();
                cbcusgrcd_SelectedIndexChanged(sender, e);
            }
        } rs.Close();
        cbadjp_type_SelectedIndexChanged(sender, e);
        bindinggrd();
    }
}