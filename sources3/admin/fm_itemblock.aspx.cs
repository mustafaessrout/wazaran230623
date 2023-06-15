using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_fm_itemblock : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txcust.CssClass = "divhid"; cbcusgrcd.CssClass = "divhid";
            bll.vBindingComboToSp(ref cbsp, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm");
            vInitGrid();
        }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
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
    protected void btadd_Click(object sender, EventArgs e)
    {
        if (hditem.Value.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "wr", "sweetAlert('Item not yet selected','Select Item','warning');", true);
            return;
        }

        if (rdcust.SelectedValue.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "wx", "sweetAlert('Blocked for customer not yet selected','Select Customer blocked','warning');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@item_cd", hditem.Value.ToString()));
        arr.Add(new cArrayList("@start_dt", System.DateTime.ParseExact(dtstart.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@end_dt", System.DateTime.ParseExact(dtend.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        if (rdcust.SelectedValue.ToString() == "G")
        {
            arr.Add(new cArrayList("@cusgrcd", cbcusgrcd.SelectedValue.ToString()));
            bll.vInsertItemBlockCusgrcd(arr);
        }
        else if (rdcust.SelectedValue.ToString() == "T")
        {
            arr.Add(new cArrayList("@otlcd", cbcusgrcd.SelectedValue.ToString()));
            bll.vInsertItemBlockOtlCd(arr);
        }
         vInitGrid();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "wr", "sweetAlert('Block Item has been succeeded','Save Successfully','warning');", true);
           
    }

    void vInitGrid()
    {
        bll.vBindingGridToSp(ref grd, "sp_titemblock_cusgrcd_get");
    }
    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        HiddenField hdis = (HiddenField)grd.Rows[e.RowIndex].FindControl("hdids");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@deleted", "1"));
        arr.Add(new cArrayList("@IDS", hdis.Value.ToString()));
        bll.vUpdateItemBlockCusgrcd(arr);
        vInitGrid();
    }
    protected void rdcust_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdcust.SelectedValue.ToString() == "G")
        {
            cbcusgrcd.CssClass = "form-control-static input-sm";
            txcust.CssClass = "divhid";
            bll.vBindingFieldValueToCombo(ref cbcusgrcd, "cusgrcd");
        }
        else if (rdcust.SelectedValue.ToString() == "T")
        {
            cbcusgrcd.CssClass = "form-control-static input-sm";
            txcust.CssClass = "divhid";
            bll.vBindingFieldValueToCombo(ref cbcusgrcd, "otlcd");
        }
        else if (rdcust.SelectedValue.ToString() == "C")
        {
            cbcusgrcd.CssClass = "divhid"; cbcusgrcd.Items.Clear();
            txcust.CssClass = "form-control input-sm";
        }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList2(string prefixText, int count, string contextKey)
    {
        return default(string[]);
    }
}