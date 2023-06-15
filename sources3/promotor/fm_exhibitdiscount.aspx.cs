using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_exhibitdiscount : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id",  Request.Cookies["usr_id"].Value.ToString()));
            bll.vDeleteWrkExhibitionDiscItem(arr);
            bll.vDeleteWrkExhibitionDiscFormula(arr);
            bll.vBindingFieldValueToCombo(ref cbuom, "uom");
            bll.vBindingFieldValueToCombo(ref cbdiscmethod, "disc_method");
            bll.vBindingComboToSp(ref cbexhibition, "sp_tmst_exhibition_get", "exhibit_cd", "exhibit_nm");
            if (Request.Cookies["exh_cd"] != null)
            { 
                cbexhibition.SelectedValue = Request.Cookies["exh_cd"].Value.ToString();
                cbexhibition_SelectedIndexChanged(sender, e);
                cbexhibition.CssClass = "form-control ro";
            }
            dtdiscount.Text = Request.Cookies["exh_dt"].Value.ToString();
            dtdiscount.CssClass = "form-control ro";
        }
    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@minqty", txminqty.Text));
        //arr.Add(new cArrayList("@item_cd", hditem.Value.ToString()));
        arr.Add(new cArrayList("@freecash", txfreecash.Text));
        arr.Add(new cArrayList("@disc_method", cbdiscmethod.SelectedValue.ToString()));
        arr.Add(new cArrayList("@uom", cbuom.SelectedValue.ToString()));
        bll.vInsertWrkExhibitionDiscFormula(arr);
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_twrk_exhibitiondiscformula_get", arr);
        txfreecash.Text = "";
        txitem.Text = "";
        hditem.Value = "";

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
        arr.Add(new cArrayList("@exhibit_cd", contextKey));
        arr.Add(new cArrayList("@item_cd", prefixText));
        bll.vSearchMstItemByExhibition(arr, ref rs);
        while (rs.Read())
        {
            sItem = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["item_cd"].ToString() + "-" + rs["item_nm"].ToString() + "-" + rs["size"].ToString() + "-" + rs["branded_nm"].ToString(), rs["item_cd"].ToString());
            lItem.Add(sItem);

        }
        rs.Close();
        return (lItem.ToArray());
    }
    protected void btadditem_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@item_cd", hditem.Value.ToString()));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vInsertWrkExhibitDiscItem(arr);
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grditem, "sp_twrk_exhibitdiscitem_get", arr);
        txitem.Text = ""; hditem.Value = "";
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        if (grditem.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('No item to be discount selected!','Please add item want to discount');", true);
            return;
        }
        if (grd.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('No Formula to be created!','Please add formula  for discount');", true);
            return;
        }
        string sNo = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@disc_desc", txremark.Text));
        arr.Add(new cArrayList("@exhibit_cd", cbexhibition.SelectedValue.ToString()));
        arr.Add(new cArrayList("@deleted",0));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vInsertExhibitionMstDiscount(arr, ref sNo);
        lbdisccode.Text = sNo;
        btsave.CssClass = "btn btn-info ro";
        txitem.CssClass = "form-control ro";
        txremark.CssClass = "form-control ro";
        txminqty.CssClass = "form-control ro";
        cbdiscmethod.CssClass = "form-control ro";
        cbexhibition.CssClass = "form-control ro";
        cbuom.CssClass = "form-control ro";
        grditem.CssClass = "mydatagrid ro";
        grd.CssClass = "mydatagrid ro";
        btadd.CssClass = "btn btn-primary ro";
        btadditem.CssClass = "btn btn-primary ro";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('New Discount has been created!','"+sNo+"','success');", true);
    }
    //protected void btsearch_Click(object sender, EventArgs e)
    //{
    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "popupcenter('/promotor/lookupdiscount.aspx');", true);
    //}
    protected void btlookup_Click(object sender, EventArgs e)
    {
        System.Data.SqlClient.SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@disc_cd", hddisccode.Value.ToString()));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vGetExhibitMstDiscount(arr, ref rs);
        while (rs.Read())
        {
            lbdisccode.Text = hddisccode.Value.ToString();
            txremark.Text = rs["disc_desc"].ToString();
            txremark.CssClass = "form-control ro";
            lbdisccode.CssClass = "form-control ro";
            cbexhibition.SelectedValue = rs["exhibit_cd"].ToString();
            cbexhibition.CssClass = "form-control ro";
            txitem.CssClass = "form-control ro";
            btadd.CssClass = "btn btn-primary ro";
            btadditem.CssClass = "btn btn-primary ro";
            btsave.CssClass = "btn btn-info ro";
            grd.CssClass = "mydatagrid ro";
            grditem.CssClass = "mydatagrid ro";
        }
        rs.Close();
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grditem, "sp_twrk_exhibitdiscitem_get", arr);
        bll.vBindingGridToSp(ref grd, "sp_twrk_exhibitiondiscformula_get", arr);
    }
    protected void cbexhibition_SelectedIndexChanged(object sender, EventArgs e)
    {
        txitem_AutoCompleteExtender.ContextKey = cbexhibition.SelectedValue.ToString();
    }
}