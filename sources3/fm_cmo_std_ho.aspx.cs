using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_cmo_std_ho : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            dtstd.Text = DateTime.Now.ToString("d/M/yyyy");
            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", "0"));
            bll.vBindingComboToSp(ref cbFrom, "sp_tmst_warehouse_get", "whs_cd", "whs_desc",arr);
            arr.Clear();
            bll.vBindingComboToSp(ref cbTo, "sp_tmst_salespoint_getall", "salespointcd", "salespoint_desc");
            bll.vBindingFieldValueToCombo(ref cbuom, "uom");
            cbuom.SelectedValue = "CTN";
        }
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

    //[System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    //public static string[] GetItemList(string prefixText, int count, string contextKey)
    //{
    //    HttpCookie cook;
    //    cook = HttpContext.Current.Request.Cookies["sp"];
    //    cbll bll = new cbll();
    //    List<string> lItem = new List<string>();
    //    List<cArrayList> arr = new List<cArrayList>();
    //    string sItem = string.Empty;
    //    System.Data.SqlClient.SqlDataReader rs = null;
    //    arr.Add(new cArrayList("@item_cd", prefixText));
    //    bll.vSearchMstItem(arr, ref rs);
    //    while (rs.Read())
    //    {
    //        sItem = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["item_cd"].ToString() + "-" + rs["item_nm"].ToString() + "-" + rs["size"].ToString(), rs["item_cd"].ToString());
    //        lItem.Add(sItem);

    //    }
    //    rs.Close();
    //    return (lItem.ToArray());
    //}

    void vbindGrid()
    {
        List<cArrayList> arr = new List<cArrayList>();
        DateTime dtwaz_dt = DateTime.ParseExact(dtstd.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        arr.Add(new cArrayList("@std_dt", dtwaz_dt));
        bll.vBindingGridToSp(ref grditem, "sp_tpch_std_get", arr);
    }

    protected void dtstd_TextChanged(object sender, EventArgs e)
    {
        vbindGrid();
    }

    protected void btadd_Click(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@std_ref", txstdref.Text));
            arr.Add(new cArrayList("@std_dt", DateTime.ParseExact(dtstd.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@vhc_cd", txvehicle.Text));
            arr.Add(new cArrayList("@whs_cd", cbFrom.SelectedValue.ToString()));
            arr.Add(new cArrayList("@ship_to", cbTo.SelectedValue.ToString()));
            arr.Add(new cArrayList("@item_cd", hditem.Value));
            arr.Add(new cArrayList("@qty", txitem.Text));
            arr.Add(new cArrayList("@uom", cbuom.SelectedValue.ToString()));
            bll.vInsertSTD(arr);
            arr.Clear();
            txitem.Text = "";
            txqty.Text = "";
            vbindGrid();
        }
        catch (Exception ex)
        {
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : Add STD");
        }
        finally
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('STD Updated.','','success');", true);
        }
    }

    protected void grditem_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }

    protected void grditem_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void grditem_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }

    protected void grditem_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void grditem_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
}