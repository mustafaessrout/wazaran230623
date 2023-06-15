using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_adjustmentprice : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingFieldValueToCombo(ref cbcust, "cusgrcd");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vDeltwrkadjustmentPrice(arr);
        }
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList1(string prefixText, int count, string contextKey)
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
    protected void btsearch_Click(object sender, EventArgs e)
    {
        string sSalesmanCode = string.Empty;
        if (txcustomer.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Alert','Please type for search customer ! | يرجى كتابة العملاء','warning');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        System.Data.SqlClient.SqlDataReader rs = null;
        arr.Clear();
        arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
        bll.vGetMstCustomer(arr, ref rs);
        while (rs.Read())
        {
           // lbaddress.Text = rs["addr"].ToString();
            var culture = System.Globalization.CultureInfo.GetCultureInfo("id-ID");
            //lbcredit.Text = string.Format(culture, "{0:n}", Convert.ToDouble(rs["credit_limit"]));
           // lbcusttype.Text = rs["otlnm"].ToString();
            //lbterm.Text = rs["term"].ToString();
            sSalesmanCode = rs["salesman_cd"].ToString();
           // lbcity.Text = rs["city_cd"].ToString();
            txcustomer.CssClass = "ro";
           // txclremain.Text = bll.vLookUp("select dbo.fn_getremaincl('" + hdcust.Value.ToString() + "')");
          //  lbcredittype.Text = rs["cuscate_cd"].ToString();
            //  txcustomer.Enabled = false;
        }
        rs.Close();
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdcust.SelectedValue=="1")
        {
            txcustomer.Visible = false;
            cbcust.Visible = true;
            lbcust.Text = "Custmer GR:";
            btsearch.Visible = false;
            lbcust.Visible = true;
            grditem.Columns[0].HeaderText="Custmer Group";
            rdcust.Enabled = false;
        }
        else
        {
            txcustomer.Visible = true;
            cbcust.Visible = false;
            lbcust.Text = "Customer:";
            lbcust.Visible = true;
            grditem.Columns[0].HeaderText = "Custmer Code";
            rdcust.Enabled = false;
        }
    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        if (rdcust.Items[0].Selected == false && rdcust.Items[1].Selected == false)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alr", "sweetAlert('Please Select customer or customer group!','Customer','warning');", true);
            return;
        }
        if (rdcust.Items[0].Selected == true)
        {
            if (txcustomer.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alr", "sweetAlert('Please insert Customer!','customer','warning');", true);
                return;
            }
        }
       
            if (txitem.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alr", "sweetAlert('Please insert item!','item','warning');", true);
                return;
            }
            if (txadjust_value.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alr", "sweetAlert('Please insert Adjust Value!','Adjust Value','warning');", true);
                return;
            }
      
        
        if (rdcust.Items[1].Selected == true)
        {
            string check = bll.vLookUp("select item_cd from twrkadjustmentpricecusgrcd where cusgrcd='" + cbcust.SelectedValue.ToString() + "' and usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and item_cd='" + hditem.Value.ToString() + "'");        
        if (check != "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alr", "sweetAlert('This Item for "+cbcust.SelectedItem.ToString()+" is already added!','Adjustment Price','warning');", true);
            return;
        }
        }
        if (rdcust.Items[0].Selected == true)
        {
            string check = bll.vLookUp("select item_cd from twrkadjustmentpricecusgrcd where cusgrcd='" + txcustomer.Text + "' and usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and item_cd='" + hditem.Value.ToString() + "'");        
            if (check != "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alr", "sweetAlert('This Item for " + cbcust.SelectedItem.ToString() + " is already added!','Adjustment Price','warning');", true);
                return;
            }
        }
        
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@item_cd", hditem.Value.ToString()));
            if (rdcust.Items[0].Selected == true)
            {
                arr.Add(new cArrayList("@cusgrcd", cbcust.SelectedValue.ToString()));
            }
            else if (rdcust.Items[1].Selected == true)
            {
                arr.Add(new cArrayList("@cusgrcd", txcustomer.Text));
            }
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["SP"].Value.ToString()));
            arr.Add(new cArrayList("@adjust_value", txadjust_value.Text));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vInserttwrkadjustmentPrice(arr);
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grditem, "sp_twrkadjustmentprice_get", arr);
        
    }
    protected void grditem_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbitemcode = (Label)grditem.Rows[e.RowIndex].FindControl("lbitemcode");
        Label lbcusgr = (Label)grditem.Rows[e.RowIndex].FindControl("lbcusgr");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@cusgrcd", lbcusgr.Text));
        bll.vDeltwrkadjustmentPrice(arr);
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grditem, "sp_twrkadjustmentprice_get", arr);
    }
    protected void grditem_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        grditem.PageIndex = e.NewPageIndex;
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grditem, "sp_twrkadjustmentprice_get", arr);    
    }
    protected void btsave_Click(object sender, EventArgs e)
    {

    }
}