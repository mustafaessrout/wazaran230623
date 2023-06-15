using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_discountexcluded : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dtstart.Text = System.DateTime.Today.ToString("d/M/yyyy"); //Request.Cookies["waz_dt"].Value.ToString();
            dtend.Text = System.DateTime.Today.ToString("d/M/yyyy"); 
            bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm");
            bll.vBindingGridToSp(ref grd, "sp_tdiscount_excluded_get");
            cbdiscount.CssClass = "divhid";
            cbgroup.CssClass = "divhid";
            txcust.CssClass = "divhid";
            cbsalespoint.CssClass = "divhid";
        }
    }
    protected void rdcust_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdcust.SelectedValue.ToString() == "G")
        {
            bll.vBindingFieldValueToCombo(ref cbgroup, "cusgrcd");
            cbgroup.CssClass = "form-control-static";
            txcust.CssClass = "divhid";
            hdcust.Value = "";
        }
        else if (rdcust.SelectedValue.ToString()=="C"){
            txcust.CssClass = "form-control-static";
            cbgroup.CssClass = "divhid";
        }
    }
    protected void btaddcust_Click(object sender, EventArgs e)
    {
        if (rdcust.SelectedValue.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please select by customer or group','Cust/Group Cust','warning');", true);
            return;
        }
        if (rdcust.SelectedValue.ToString() == "C")
        {
            if (hdcust.Value.ToString() == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Customer is not selected','Select customer','warning');", true);
                return;
            }
        }
        if (rddiscount.SelectedValue.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please select by some discount or all discount','Discount status','warning');", true);
            return;
        }
        DateTime dtst = System.DateTime.ParseExact(dtstart.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime dted = System.DateTime.ParseExact(dtend.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        if (dted < dtst)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('End date can not less than start date','Period','warning');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@rdcust", rdcust.SelectedValue.ToString()));
        arr.Add(new cArrayList("@rddiscount", rddiscount.SelectedValue.ToString()));
        if (rdcust.SelectedValue.ToString() == "C")
        {
            arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
            arr.Add(new cArrayList("@cusgrcd", DBNull.Value));
        }
        else
        {
            arr.Add(new cArrayList("@cusgrcd", cbgroup.SelectedValue.ToString()));
            arr.Add(new cArrayList("@cust_cd", DBNull.Value));
        }
        if (rddiscount.SelectedValue.ToString() == "A")
        {
            arr.Add(new cArrayList("@disc_cd", DBNull.Value));
        }
        else
        {
            arr.Add(new cArrayList("@disc_cd", cbdiscount.SelectedValue.ToString()));
        }
        if (rdsalespoint.SelectedValue.ToString()=="S")
        {arr.Add(new cArrayList("@salespointcd",cbsalespoint.SelectedValue.ToString()));}else{arr.Add(new cArrayList("@salespointcd",DBNull.Value));}
        arr.Add(new cArrayList("@start_dt", DateTime.ParseExact(dtstart.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@end_dt", DateTime.ParseExact(dtend.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@deleted", 0));
        bll.vInsertDiscountExcluded(arr);
        bll.vBindingGridToSp(ref grd, "sp_tdiscount_excluded_get");
    }
    protected void cbdiscount_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void rddiscount_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rddiscount.SelectedValue.ToString() == "D")
        {
            cbdiscount.CssClass = "divnormal form-control-static";
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
            bll.vBindingComboToSp(ref cbdiscount, "sp_tmst_discount_get2", "disc_cd", "disc_nm");
        }
        else if (rddiscount.SelectedValue.ToString() == "A")
        {
            cbdiscount.CssClass = "divhid";
           // bll.vBindingComboToSp(ref cbdiscount, "sp_tmst_discount_get2", "disc_cd", "disc_nm");
        }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionCust(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        // HttpCookie cok;        
        //cok = HttpContext.Current.Request.Cookies["sp"];
        System.Data.SqlClient.SqlDataReader rs = null;
        string sCust = string.Empty;
        List<string> lCust = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", contextKey));
        arr.Add(new cArrayList("@cust_cd", prefixText));
        bll.vSearchMstCustomer(arr, ref rs);
        while (rs.Read())
        {
            sCust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + " - " + rs["cust_nm"].ToString(), rs["cust_cd"].ToString());
            lCust.Add(sCust);
        } rs.Close();
        return (lCust.ToArray());
    }
    protected void cbsalespoint_SelectedIndexChanged(object sender, EventArgs e)
    {
        txcust_AutoCompleteExtender.ContextKey = cbsalespoint.SelectedValue.ToString();
        rddiscount_SelectedIndexChanged(sender, e);
    }
    protected void rdsalespoint_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdsalespoint.SelectedValue.ToString() == "S")
        {
            rdcust.CssClass = "form-control";
            cbsalespoint.CssClass = "form-control-static";
            cbsalespoint_SelectedIndexChanged(sender, e);
            
        }
        else if (rdsalespoint.SelectedValue.ToString()=="A")
        {
            rdcust.SelectedValue = "G";
            rdcust.CssClass = "ro form-control";
            rdcust_SelectedIndexChanged(sender, e);
            cbsalespoint.CssClass = "divhid";
        }
    }

    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        HiddenField sIDS = (HiddenField)grd.Rows[e.RowIndex].FindControl("hdids");
        arr.Add(new cArrayList("@IDS", sIDS.Value.ToString()));
        bll.vDelDiscountExcluded(arr);
        bll.vBindingGridToSp(ref grd, "sp_tdiscount_excluded_get");
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=disex');", true);
    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grd.EditIndex = e.NewEditIndex;
        List<cArrayList> arr = new List<cArrayList>();
        bll.vBindingGridToSp(ref grd, "sp_tdiscount_excluded_get");
    }
    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        HiddenField hdids = (HiddenField)grd.Rows[e.RowIndex].FindControl("hdids");
        TextBox dtend = (TextBox)grd.Rows[e.RowIndex].FindControl("dtend");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@ids", hdids.Value.ToString()));
        arr.Add(new cArrayList("@end_dt", dtend.Text));
        bll.vUpdateDiscountExcluded(arr);
        grd.EditIndex = -1;
        bll.vBindingGridToSp(ref grd, "sp_tdiscount_excluded_get");
    }
    protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grd.EditIndex = -1;
        bll.vBindingGridToSp(ref grd, "sp_tdiscount_excluded_get");
    }
}