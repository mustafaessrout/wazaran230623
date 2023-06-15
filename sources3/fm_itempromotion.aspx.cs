using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_itempromotion : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingGridToSp(ref grd, "sp_titem_promotion_getbyrange");
        }
    }

    protected void btadd_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@item_cd", hditem.Value));
        arr.Add(new cArrayList("@cust_cd", hdcust.Value));
        arr.Add(new cArrayList("@start_dt", System.DateTime.ParseExact(dtstart.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@end_dt", System.DateTime.ParseExact(dtend.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@promoted_by", Request.Cookies["usr_id"].Value.ToString()));
        bll.vInsertItemPromotion(arr);
        arr.Clear();
        arr.Add(new cArrayList("@item_cd", hditem.Value));
        bll.vBindingGridToSp(ref grd, "sp_titem_promotion_get", arr);
        txitemcode.Text = string.Empty;
        txcustomer.Text = string.Empty;
        dtstart.Text = string.Empty;
        dtend.Text = string.Empty;
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCustomerList(string prefixText, int count, string contextKey)
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
        bll.vSearchMstCustomerInRPS(arr, ref rs);
        while (rs.Read())
        {
            sCust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + "-" + rs["cust_nm"].ToString(), rs["cust_cd"].ToString());
            lCust.Add(sCust);
        }
        rs.Close();
        return (lCust.ToArray());
    }


    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetItemList(string prefixText, int count, string contextKey)
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

    protected void btsearch_Click(object sender, EventArgs e)
    {
        bll.vBindingGridToSp(ref grd, "sp_titem_promotion_getbyrange");
    }

    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grd.EditIndex = e.NewEditIndex;
        bll.vBindingGridToSp(ref grd, "sp_titem_promotion_getbyrange");
    }

    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbitemcode = (Label)grd.Rows[e.RowIndex].FindControl("lbitemcode");
        Label lbstartdate = (Label)grd.Rows[e.RowIndex].FindControl("lbstartdate");
        Label lbenddate = (Label)grd.Rows[e.RowIndex].FindControl("lbenddate");
        HiddenField hdcustcode = (HiddenField)grd.Rows[e.RowIndex].FindControl("hdcustcode");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
        arr.Add(new cArrayList("@cust_cd", hdcust.Value));
        arr.Add(new cArrayList("@start_dt", System.DateTime.ParseExact(lbstartdate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@end_dt", System.DateTime.ParseExact(lbenddate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        bll.vBindingGridToSp(ref grd, "sp_titem_promotion_getbyrange");
    }
}