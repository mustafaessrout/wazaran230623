using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_reqitem : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vDelWrkSalespoint(arr);
            bll.vDelWrkItemPrice(arr);
            arr.Clear();
            arr.Add(new cArrayList("@level_no", 1));
            bll.vBindingComboToSp(ref cbbranded, "sp_tmst_product_get", "prod_cd", "prod_nm", arr);
            bll.vBindingFieldValueToCombo(ref cbpacking, "packing");
            bll.vBindingFieldValueToCombo(ref cbuom, "uom");
            bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm");
            bll.vBindingFieldValueToCombo(ref cbcusttype, "otlcd");
            bll.vBindingComboToSp(ref cbvendor, "sp_tmst_vendor_get","vendor_cd","vendor_nm");
            bll.vBindingFieldValueToCombo(ref cbprodtype, "prod_typ");
            cbbranded_SelectedIndexChanged(sender, e);
            cbprodgroup_SelectedIndexChanged(sender, e);
            txrequestno.Text = "NEW";
            txrequestno.ReadOnly = true;
           
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
    }
    protected void cbbranded_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@level_no", 2));
        arr.Add(new cArrayList("@prod_cd_parent", cbbranded.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbprodgroup, "sp_tmst_product_get", "prod_cd", "prod_nm", arr);
        cbprodgroup_SelectedIndexChanged(sender, e);
    }
    protected void cbprodgroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@level_no", 3));
        arr.Add(new cArrayList("@prod_cd_parent", cbprodgroup.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbproduct, "sp_tmst_product_get", "prod_cd", "prod_nm", arr);
    }
    protected void btspadd_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (chk.Checked)
        {
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vDelWrkSalespoint(arr);
            bll.vInsertWrkSalespointAll(arr);
        }
        else
        {
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
            bll.vInsertWrkSalespoint(arr); arr.Clear();
        }
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdsp, "sp_twrk_salespoint_get", arr);
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        string sRequestNo = "";
        arr.Add(new cArrayList("@prod_cd", cbproduct.SelectedValue.ToString()));
        arr.Add(new cArrayList("@item_nm",  txitemname.Text.ToUpper()));
        arr.Add(new cArrayList("@item_arabic", txarabic.Text));
        arr.Add(new cArrayList("@uom", cbuom.SelectedValue.ToString()));
        arr.Add(new cArrayList("@size", txsize.Text));
        arr.Add(new cArrayList("@packing", cbpacking.SelectedValue.ToString()));
        arr.Add(new cArrayList("@remark", txremark.Text));
        arr.Add(new cArrayList("@buy_price", txbuyprice.Text));
        arr.Add(new cArrayList("@vendor_cd", cbvendor.SelectedValue.ToString()));
        arr.Add(new cArrayList("@prod_typ", cbprodtype.SelectedValue.ToString()));
        arr.Add(new cArrayList("@dim_depth", txdepth.Text));
        arr.Add(new cArrayList("@dim_width", txwidth.Text));
        arr.Add(new cArrayList("@dim_length", txlength.Text));
        arr.Add(new cArrayList("@barcode_ctn", txbarcodectn.Text));
        arr.Add(new cArrayList("@barcode_box", txbarcodebox.Text));
        arr.Add(new cArrayList("@barcode_prod", txbarcodectn.Text));
        
        if (avatarUpload.Value != "")
        {
            string sFile = avatarUpload.Value;
            
            arr.Add(new cArrayList("@imagename", sFile));
        }
        bll.vInsertRequestItem(arr, ref sRequestNo);
        txrequestno.Text = sRequestNo;
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@item_cd", sRequestNo));
        bll.vInsertItemSalespoint4Wrk(arr);
        if (avatarUpload.Value != "")
        {
            //upl. = avatarUpload.Value;
           
            string sLocFile = bll.sGetControlParameter("image_path");
          //  upl.SaveAs(sLocFile + "I" + sRequestNo + "jpg");
            avatarUpload.PostedFile.SaveAs(sLocFile + "I" + sRequestNo + ".jpg");
            
        }
        //Price
        arr.Clear();
        arr.Add(new cArrayList("@usr_id",Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@item_cd", sRequestNo));
        bll.vInsertCustomertypePrice4Wrk(arr);
        //get approval
        List<string> lapproval = bll.lGetApproval("item", 1);
        cd.vSendSms("New Item requested, with no. request : " + sRequestNo + ", please login to wazaran for detail information.", lapproval[0]);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "alert('request saved, now in approval ..');", true);
    }
    protected void grdsp_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbsalespoint = (Label)grdsp.Rows[e.RowIndex].FindControl("lbsalespointcd");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", lbsalespoint.Text));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vDelWrkSalespoint(arr);
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdsp, "sp_twrk_salespoint_get", arr);
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        System.Data.SqlClient.SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@size", prefixText));
        List<string> lsize = new List<string>();
        string ssize = string.Empty;
        bll.vGetMstItemSize(arr, ref rs);
        while (rs.Read())
        { 
            ssize = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["size"].ToString(),rs["size"].ToString());
            lsize.Add(ssize);
        }
        rs.Close();
        return (lsize.ToArray());
    }
    protected void btaddcusttype_Click(object sender, EventArgs e)
    {
        double dCheck=0;
        if (!double.TryParse(txbuyprice.Text, out dCheck))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "alert('Vendor price must numeric !');", true);
            return;
        }
        if (!double.TryParse(txprice.Text, out dCheck))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "alert('Price must numeric !');", true);
            return;
        }

        if (Convert.ToDouble(txprice.Text) < Convert.ToDouble(txbuyprice.Text))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "alert('Sell price can not less than buy price !');", true);
            return;
        }
        txbuyprice.ReadOnly = true;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@sell_price", txprice.Text));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@otlcd", cbcusttype.SelectedValue.ToString()));
        arr.Add(new cArrayList("@start_dt", DateTime.ParseExact(dtstart.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        bll.vInsertWrkItemPrice(arr); arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
      
        bll.vBindingGridToSp(ref grdcusttyp, "sp_twrk_itemprice_get", arr);
    }
}