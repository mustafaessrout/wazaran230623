using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class promotor_fm_exhibitioninttrf : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vDeleteWrkExhibitInternalTrf(arr);
            bll.vBindingComboToSp(ref cbexhibition, "sp_tmst_exhibition_get", "exhibit_cd", "exhibit_nm");
            if (Request.Cookies["exh_cd"].Value != null)
            {
                cbexhibition.SelectedValue = Request.Cookies["exh_cd"].Value.ToString();
                cbexhibition.CssClass = "form-control ro";
                cbexhibition_SelectedIndexChanged(sender, e);
                cbbooth_SelectedIndexChanged(sender, e);
            }
          
            bll.vBindingFieldValueToCombo(ref cbuom, "uom");
            rdtrftype_SelectedIndexChanged(sender, e);
        }
    }
    protected void cbexhibition_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@exhibit_cd", cbexhibition.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbbooth, "sp_texhibition_booth_get", "product_cd", "prod_nm", arr);
      
        //bll.vBindingComboToSp(ref cbitem, "sp_texhibition_stock_get", "item_cd", "item_nm", arr);
        //cbitem_SelectedIndexChanged(sender, e);
    }
    protected void cbbooth_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@exhibit_cd", cbexhibition.SelectedValue.ToString()));
        arr.Add(new cArrayList("@prod_cd", cbbooth.SelectedValue.ToString()));
        arr.Add(new cArrayList("@item_cd", hditem.Value.ToString()));
        lbpromoter.Text = bll.vLookUp("select dbo.fn_getpromoter('"+cbbooth.SelectedValue.ToString()+"','"+cbexhibition.SelectedValue.ToString()+"')");
        txitem_AutoCompleteExtender.ContextKey = cbexhibition.SelectedValue.ToString() +"," + cbbooth.SelectedValue.ToString();
    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        if (hditem.Value.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('Item not yet selected!','select item');", true);
            return;
        }

        double dQty = 0;
        if (!double.TryParse(lbqty.Text, out dQty))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('Stock is not available!','Check Stock');", true);
            return;
        
        }

        if (dQty==0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('Stock is not available!','Check Stock');", true);
            return;
        }

        double dTrf = 0;
        if (!double.TryParse(txtrfamt.Text, out dTrf))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('Qty transfer must be numeric!','Check Transfer Qty');", true);
            return;
        }

        if (dTrf > dQty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('Stock is not enough!','Pls change qty transfer');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@item_cd", hditem.Value.ToString()));
        arr.Add(new cArrayList("@qty", txtrfamt.Text));
        arr.Add(new cArrayList("@uom", cbuom.SelectedValue.ToString()));
        bll.vInsertWrkExhibitionInternalTrf(arr);
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_twrk_exhibitinternaltrf_get", arr);
        hditem.Value = ""; txitem.Text = ""; txtrfamt.Text = ""; lbqty.Text = "";
        cbbooth.CssClass = "form-control ro"; cbexhibition.CssClass = "form-control ro";
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
    protected void btprice_Click(object sender, EventArgs e)
    {
        if (rdtrftype.SelectedValue.ToString() == "DS")
        {
            lbqty.Text = bll.vLookUp("select stock_amt from texhibition_stock where exhibit_cd='" + cbexhibition.SelectedValue.ToString() + "' and item_cd='" + hditem.Value.ToString() + "'");
        }
        else if (rdtrftype.SelectedValue.ToString() == "SD")
        {
            lbqty.Text = bll.vLookUp("select qty from texhibitionbooth_stock where exhibit_cd='"+cbexhibition.SelectedValue.ToString()+"' and prod_cd='"+cbbooth.SelectedValue.ToString()+"' and item_cd='"+hditem.Value.ToString()+"'");
            if (lbqty.Text == ""){lbqty.Text = "0";}
        }
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        string sNo = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@exhibit_cd", cbexhibition.SelectedValue.ToString() ));
        arr.Add(new cArrayList("@trf_dt", System.DateTime.ParseExact( Request.Cookies["exh_dt"].Value.ToString(),"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
	    arr.Add(new cArrayList("@prod_cd", cbbooth.SelectedValue.ToString()));
        arr.Add(new cArrayList("@deleted", 0));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@exhtrftyp", rdtrftype.SelectedValue.ToString()));
        
        bll.vInsertExhibitInternalTransfer(arr, ref sNo);
        lbtrfno.Text = sNo;
        btsave.CssClass = "btn btn-info ro";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Internal Transfer has been saved!','"+sNo+"','success');", true);
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_exhibitioninttrf.aspx");
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {

    }
    protected void rdtrftype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdtrftype.SelectedValue == "DS")
        {
            lbdepo.InnerText = "From Depo";
            lbsection.InnerText = "To Sect";
            txitem.Text = ""; hditem.Value = ""; txtrfamt.Text = ""; lbqty.Text = ""; lbtrfno.Text = "";
           
        }
        else if (rdtrftype.SelectedValue == "SD")
        {
            lbsection.InnerText = "From Sect";
            lbdepo.InnerText = "To Depo";
            txitem.Text = ""; hditem.Value = ""; txtrfamt.Text = ""; lbqty.Text = ""; lbtrfno.Text = "";
        }
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString() ,"openreport('/fm_report2.aspx?src=exhitrf&no="+lbtrfno.Text+"');", true);
    }
    protected void btlookup_Click(object sender, EventArgs e)
    {
        lbtrfno.Text = hdtrfno.Value.ToString();
    }
}