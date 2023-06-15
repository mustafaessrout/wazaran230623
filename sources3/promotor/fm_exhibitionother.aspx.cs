using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_exhibitionother : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vDelWrkExhibitOtherOrder(arr);
            bll.vBindingFieldValueToCombo(ref cbtranstype, "otherordertyp");
            bll.vBindingFieldValueToCombo(ref cbuom, "uom");
            bll.vBindingComboToSp(ref cbexhibition, "sp_tmst_exhibition_get", "exhibit_cd", "exhibit_nm");
            if (Request.Cookies["exh_cd"] != null)
            { 
                cbexhibition.SelectedValue = Request.Cookies["exh_cd"].Value.ToString();
                cbexhibition_SelectedIndexChanged(sender, e);
                cbexhibition.CssClass = "form-control ro";
            }
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
    protected void cbuom_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbunitprice.Text = bll.vLookUp("select dbo.fn_getexhibitprice('" + hditem.Value.ToString() + "','" + cbexhibition.SelectedValue.ToString() + "')");
    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        double dCheck = 0;
        if (!double.TryParse(txqty.Text, out dCheck))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('Qty must numeric!','Check Qty');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@item_cd", hditem.Value.ToString()));
        arr.Add(new cArrayList("@qty", txqty.Text));
        arr.Add(new cArrayList("@uom", cbuom.SelectedValue.ToString()));
        bll.vInsertWrkExhibitOtherOrder(arr);
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_twrk_exhibitotherorder_get", arr);
        cbbooth.CssClass = "form-control ro";
    }
    protected void cbexhibition_SelectedIndexChanged(object sender, EventArgs e)
    {
        txitem_AutoCompleteExtender.ContextKey = cbexhibition.SelectedValue.ToString();
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@exhibit_cd", cbexhibition.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbbooth, "sp_texhibition_booth_get", "product_cd", "prod_nm", arr);
        cbbooth_SelectedIndexChanged(sender, e);
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_twrk_exhibitotherorder_get", arr);
    }
    protected void btlookup_Click(object sender, EventArgs e)
    {
        string sStock = bll.vLookUp("select qty from texhibitionbooth_stock where exhibit_cd='" + cbexhibition.SelectedValue.ToString() + "' and item_cd='" + hditem.Value.ToString() + "'");
        lbstockavl.Text = sStock;
    }
    protected void cbbooth_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@exhibit_cd", cbexhibition.SelectedValue.ToString()));
        arr.Add(new cArrayList("@prod_cd", cbbooth.SelectedValue.ToString()));
        arr.Add(new cArrayList("@item_cd", hditem.Value.ToString()));
        //lbpromoter.Text = bll.vLookUp("select dbo.fn_getpromoter('" + cbbooth.SelectedValue.ToString() + "','" + cbexhibition.SelectedValue.ToString() + "')");
        txitem_AutoCompleteExtender.ContextKey = cbexhibition.SelectedValue.ToString() + "," + cbbooth.SelectedValue.ToString();
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        if (txremark.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('Please fill remark!','Remark can not empty');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@order_dt", System.DateTime.ParseExact( Request.Cookies["exh_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@remark", txremark.Text));
        arr.Add(new cArrayList("@exhibit_cd", Request.Cookies["exh_cd"].Value.ToString()));
        arr.Add(new cArrayList("@prod_cd", cbbooth.SelectedValue.ToString()));
        arr.Add(new cArrayList("@otherordertyp", cbtranstype.SelectedValue.ToString()));
        //arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        string sNo = string.Empty;
        bll.vInsertExhibitOtherOrder(arr, ref sNo);
        lbtransno.Text = sNo;
        txremark.CssClass = "form-control ro";
        txitem.CssClass = "form-control ro";
        txqty.CssClass = "form-control ro";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgSuccess('Othe order has been saved!','"+sNo+"');", true);
    }
}