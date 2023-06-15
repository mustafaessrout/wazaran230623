using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
//using System.Web.Services;
//using System.Web.Services.Protocols;
//using System.ComponentModel;

public partial class fm_stockin : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vDeleteWrkExhibitionStockIn(arr);
           
            bll.vBindingGridToSp(ref grd, "sp_twrk_exhibitionstockin_get", arr);
            bll.vBindingComboToSp(ref cbexhibition, "sp_tmst_exhibition_get", "exhibit_cd", "exhibit_nm");
            if (Request.Cookies["exh_cd"].Value != null)
            {
                cbexhibition.SelectedValue = Request.Cookies["exh_cd"].Value.ToString();
                cbexhibition_SelectedIndexChanged(sender, e);
                cbexhibition.CssClass = "form-control ro";
            }
         
            bll.vBindingFieldValueToCombo(ref cbuom, "uom");
            bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_getall", "salespointcd", "salespoint_nm");
            cbsalespoint_SelectedIndexChanged(sender, e);
            txstockin.CssClass = "form-control input-group-sm ro";
            arr.Add(new cArrayList("@exhibit_cd", cbexhibition.SelectedValue.ToString()));
            bll.vInsertWrkExhibitionBoothFromCore(arr);
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grdprod, "sp_twrk_exhibitionbooth_get", arr);
        }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        
        List<string> lItem = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string sItem = string.Empty;
        System.Data.SqlClient.SqlDataReader rs = null;
        arr.Add(new cArrayList("@exhibit_cd", contextKey));
        arr.Add(new cArrayList("@item_cd", prefixText));
        bll.vSearchMstItemByExhibitionBooth(arr, ref rs);
        while (rs.Read())
        {
            sItem = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["item_cd"].ToString() + "-" + rs["item_nm"].ToString() + "-" + rs["size"].ToString() + "-" + rs["branded_nm"].ToString(), rs["item_cd"].ToString());
            lItem.Add(sItem);

        }
        rs.Close();
        return (lItem.ToArray());
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList2(string prefixText, int count, string contextKey)
    {
        HttpCookie cook;
        //cook = HttpContext.Current.Request.Cookies["sp"];
        cbll bll = new cbll();
        List<string> lItem = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string sItem = string.Empty;
        System.Data.SqlClient.SqlDataReader rs = null;
        arr.Add(new cArrayList("@exhibit_cd", contextKey));
        arr.Add(new cArrayList("@item_cd", prefixText));
        //bll.vSearchMstItemByExhibition(arr, ref rs);
        bll.vSearchMstItemByExhibitionInit(arr, ref rs);
        while (rs.Read())
        {
            sItem = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["item_cd"].ToString() + "-" + rs["item_nm"].ToString() + "-" + rs["size"].ToString() + "-" + rs["branded_nm"].ToString(), rs["item_cd"].ToString());
            lItem.Add(sItem);

        }
        rs.Close();
        return (lItem.ToArray());
    }
    protected void cbexhibition_SelectedIndexChanged(object sender, EventArgs e)
    {
        txitemname_AutoCompleteExtender.ContextKey = cbexhibition.SelectedValue.ToString();
    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        if (cbsalespoint.SelectedValue.ToString() == "-1")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('Please select salespoint source','Salespoint');", true);
            return;
        }
        if (hditem.Value.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Item Not yet selected!','Select Item','warning');", true);
            return;
        }

        if (cbuom.SelectedValue.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('UOM is not yet selected!','Select UOM','warning');", true);
            return;
        }

        double dOut = 0;
        if (!double.TryParse(txqty.Text, out dOut))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Quantity must numeric!','QTY','warning');", true);
            return;
        }

        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@item_cd", hditem.Value.ToString()));
        arr.Add(new cArrayList("@qty", txqty.Text));
        arr.Add(new cArrayList("@uom", cbuom.SelectedValue.ToString()));
        bll.vInsertWrkExhibitionStockIn(arr);
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_twrk_exhibitionstockin_get", arr);
        hditem.Value = "";
        txitemname.Text = "";
        txqty.Text = "";
    }
    protected void cbwarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@whs_cd", cbwarehouse.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbbin, "sp_twarehouse_bin_get", "bin_cd", "bin_nm", arr);
    }
    protected void cbsalespoint_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbwarehouse, "sp_tmst_warehouse_get", "whs_cd", "whs_nm", arr);
        cbwarehouse_SelectedIndexChanged(sender, e);
    }
    protected void btsaved_Click(object sender, EventArgs e)
    {
        if (txmanualno.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('Manual No can not empty!','Manual No');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>(); string sNo = string.Empty;
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        arr.Add(new cArrayList("@exhibit_cd", cbexhibition.SelectedValue.ToString()));
        arr.Add(new cArrayList("@whs_cd", cbwarehouse.SelectedValue.ToString()));
        arr.Add(new cArrayList("@bin_cd", cbbin.SelectedValue.ToString()));
        arr.Add(new cArrayList("@createdby",  Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@manualno", txmanualno.Text));
        bll.vInsertExhibitionStockIn(arr, ref sNo);
        txstockin.Text = sNo;
        hdstockin.Value = sNo;
        txstockin.CssClass = cd.csstextro;
        hditem.Value = "";
        txitemname.Text = "";
        txqty.Text = "";
        btsaved.CssClass = "btn btn-info ro";
        grd.CssClass = "mydatagrid ro";
        grdprod.CssClass = "mydatagrid ro";
        btadd.CssClass = "btn btn-primary ro";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgSuccess('Stock In has been created!','"+sNo+"');", true);
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Stock In has been created!','"+sNo+"','success');", true);
    }
    //protected void cbboth_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    txitemname_AutoCompleteExtender.ContextKey = cbboth.SelectedValue.ToString();
    //    lbpromoter.Text = bll.vLookUp("select dbo.fn_getpromoter('"+cbboth.SelectedValue.ToString()+"','"+cbexhibition.SelectedValue.ToString()+"')");
    //}
    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('/fm_report2.aspx?src=exhstockin&stockino="+hdstockin.Value.ToString()+"');", true);
    }
    protected void btlookup_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@stockin_cd", hdstockin.Value.ToString()));
        bll.vInsertWrkExhibitionStockInDtlFromCore(arr);
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_twrk_exhibitionstockin_get", arr);
        btsaved.CssClass = "btn btn-info ro";
       
        txstockin.CssClass = cd.csstextro;
        hditem.Value = "";
        txitemname.Text = "";
        txqty.Text = "";
        btsaved.CssClass = "btn btn-info ro";
        grd.CssClass = "mydatagrid ro";
        grdprod.CssClass = "mydatagrid ro";
        btadd.CssClass = "btn btn-primary ro";
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_stockin.aspx");
    }
    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbitemcode = (Label)grd.Rows[e.RowIndex].FindControl("lbitemcode");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vDeleteWrkExhibitionStockIn(arr);
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_twrk_exhibitionstockin_get", arr);
    }
}
