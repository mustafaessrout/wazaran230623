using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class promotor_fm_exhibitorder : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vDeleteWrkExhibitSales(arr);
            bll.vDeleteWrkExhibitSalesDisc(arr);
            bll.vBindingComboToSp(ref cbexhibition, "sp_tmst_exhibition_get", "exhibit_cd", "exhibit_nm");
            if (Request.Cookies["exh_cd"] != null)
            {
                cbexhibition.SelectedValue = Request.Cookies["exh_cd"].Value.ToString();
                cbexhibition_SelectedIndexChanged(sender, e);
                cbexhibition.CssClass = "form-control ro";
            }
         
            bll.vBindingFieldValueToCombo(ref cbuom, "uom");
            dtorder.Text = Request.Cookies["exh_dt"].Value.ToString();
            dtorder.CssClass = "form-control ro";
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
        List<string> datanya = contextKey.Split(',').ToList<string>();
        arr.Add(new cArrayList("@exhibit_cd", datanya[0]));
        arr.Add(new cArrayList("@prod_cd", datanya[1]));
        arr.Add(new cArrayList("@item_cd", prefixText));
        //bll.vSearchMstItemBySalespoint(arr, ref rs);
        bll.vSearchMstItemByExhibitionBooth(arr, ref rs);
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
        if (Convert.ToDecimal(lbunitprice.Text) == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Unitprice has not yet setup!','"+hditem.Value.ToString()+"','warning');", true);
            return;
        }

        if (lbstockavl.Text == "0")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Stock is not enough!','Please do internal transfer to section','warning');", true);
            return;
        }

        if (hditem.Value == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Item not yet selected!','Pls select item','warning');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@item_cd", hditem.Value.ToString()));
        arr.Add(new cArrayList("@qty", txqty.Text));
        arr.Add(new cArrayList("@uom", cbuom.SelectedValue.ToString()));
        arr.Add(new cArrayList("@unitprice", lbunitprice.Text));
        bll.vInsertWrkExhibitSales(arr);
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_twrk_exhibitsales_get", arr);
        txitem.Text = "";
        hditem.Value = ""; txqty.Text = ""; cbuom.SelectedValue = ""; lbunitprice.Text = ""; lbstockavl.Text = "";
    }
    protected void cbuom_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbunitprice.Text = bll.vLookUp("select dbo.fn_getexhibitprice('"+hditem.Value.ToString()+"','"+cbexhibition.SelectedValue.ToString()+"')");
    }
    protected void cbexhibition_SelectedIndexChanged(object sender, EventArgs e)
    {
        txitem_AutoCompleteExtender.ContextKey = cbexhibition.SelectedValue.ToString();
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@exhibit_cd", cbexhibition.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbbooth, "sp_texhibition_booth_get", "product_cd","prod_nm", arr);
        cbbooth_SelectedIndexChanged(sender, e);
    }
    protected void btprice_Click(object sender, EventArgs e)
    {
        string sPrice = bll.vLookUp("select qty from texhibitionbooth_stock where exhibit_cd='" + cbexhibition.SelectedValue.ToString() + "' and item_cd='" + hditem.Value.ToString() + "' and prod_cd='" + cbbooth.SelectedValue.ToString() + "'"); 
        lbstockavl.Text = (sPrice == "" ? "0" : sPrice);
        cbexhibition.CssClass = "form-control ro"; cbbooth.CssClass = "form-control ro";
    }
    protected void btdisccalc_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@exhibit_cd", cbexhibition.SelectedValue.ToString()));
        bll.vBatchExhibitDiscCalc(arr);
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grddiscount, "sp_twrk_exhibitsalesdisc_get", arr);
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@exhso_dt", System.DateTime.ParseExact( Request.Cookies["exh_dt"].Value.ToString(),"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@exhibit_cd", cbexhibition.SelectedValue.ToString()));
        arr.Add(new cArrayList("@exh_sta_id", "N"));
        arr.Add(new cArrayList("@prod_cd", cbbooth.SelectedValue.ToString()));
        
        string sNo = string.Empty;
        bll.vInsertExhibitOrder(arr, ref sNo);
        lborderno.Text = sNo;
        btsave.CssClass = "btn btn-danger ro";
        hdso.Value = sNo;
        ScriptManager.RegisterStartupScript(Page,Page.GetType(),Guid.NewGuid().ToString(),"sweetAlert('Order has been saved!','"+sNo+"','success');",true);

    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_exhibitorder.aspx");
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('../fm_report2.aspx?src=exhibitinv&exhno="+hdso.Value.ToString()+"');", true);
    }
    protected void btlookup_Click(object sender, EventArgs e)
    {
        lborderno.Text = hdso.Value.ToString();
        System.Data.SqlClient.SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@exhso_cd", hdso.Value.ToString()));
        bll.vGetExhibitionOrder(arr, ref rs);
        while (rs.Read())
        {
            dtorder.Text = rs["exhso_cd"].ToString();
        }
        rs.Close();
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {

    }
    protected void cbbooth_SelectedIndexChanged(object sender, EventArgs e)
    {
        txitem_AutoCompleteExtender.ContextKey = cbexhibition.SelectedValue.ToString() + "," + cbbooth.SelectedValue.ToString();
    }
}