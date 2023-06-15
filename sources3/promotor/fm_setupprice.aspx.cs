using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class promotor_fm_setupprice : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingComboToSp(ref cbexhibition, "sp_tmst_exhibition_get", "exhibit_cd", "exhibit_nm");
            cbexhibition_SelectedIndexChanged(sender, e);
            if (Request.Cookies["exh_cd"].Value != null)
            {
                cbexhibition.SelectedValue = Request.Cookies["exh_cd"].Value.ToString();
                cbexhibition_SelectedIndexChanged(sender, e);                
                cbexhibition.CssClass = "form-control ro";
            }
        }
    }
    protected void cbexhibition_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        lblocation.Text = bll.vLookUp("select loc_nm from tmst_exhibition where exhibit_cd='"+cbexhibition.SelectedValue.ToString()+"'");
        arr.Add(new cArrayList("@exhibit_cd", cbexhibition.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_texhibition_price_get", arr);
        txitem_AutoCompleteExtender.ContextKey = cbexhibition.SelectedValue.ToString();
    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        if (hiditem.Value.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('Item Not Yet Selected!','Select Item');", true);
            return;
        }

        if (txbuyprice.Text == "0")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('Branch price is not yet setup!','"+hiditem.Value.ToString()+" [Pls contact HO Claim Team for price]');", true);
            return;
        }

        double dSell = 0;
        if (!double.TryParse(txsellprice.Text, out dSell))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('Sell price must numeric!','" + hiditem.Value.ToString() + " [Pls contact HO Claim Team for price]');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@item_cd", hiditem.Value.ToString()));
        arr.Add(new cArrayList("@exhibit_cd", cbexhibition.SelectedValue.ToString()));
        arr.Add(new cArrayList("@buyprice", txbuyprice.Text));
        arr.Add(new cArrayList("@sellprice", txsellprice.Text));
        bll.vInsertExhibitPrice(arr);
        arr.Clear();
        arr.Add(new cArrayList("@exhibit_cd", cbexhibition.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_texhibition_price_get", arr);
        hiditem.Value = "";
        txitem.Text = "";
        txbuyprice.Text = "";
        txsellprice.Text = "";
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
    protected void btprice_Click(object sender, EventArgs e)
    {
        string sPrice = bll.vLookUp("select unitprice from tcustomertype_price where cust_typ='BRN' and item_cd='"+hiditem.Value.ToString()+"'");
        txbuyprice.Text = (sPrice == "") ? "0" : sPrice;
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('/fm_report2.aspx?src=exhprice&exh_cd="+cbexhibition.SelectedValue.ToString()+"');", true);
    }
}