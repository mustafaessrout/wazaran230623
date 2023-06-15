using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class fm_mstentry2 : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        string sItemID = "";
        if (!IsPostBack)
        {
            try
            {
                lbstatus.Text = bll.vLookUp("select fld_desc from tfield_value where fld_valu='N' and fld_nm='item_sta_id'");
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@level_no", "1"));
                bll.vBindingComboToSp(ref cbbranded, "sp_tmst_product_get", "prod_cd", "prod_nm", arr);
                bll.vBindingComboToSp(ref cbvendor, "sp_tmst_vendor_get", "vendor_cd", "vendor_nm");
                arr.Clear();
                arr.Add(new cArrayList("@fld_nm", "prod_typ"));
                arr.Add(new cArrayList("@hiddendata", 0));
                bll.vBindingComboToSp(ref cbprodtype, "sp_tfield_value_desc_get", "fld_valu", "fld_valudesc", arr);
                //bll.vBindingFieldValueToCombo(ref cbprodtype, "prod_typ");
                bll.vBindingFieldValueToCombo(ref cbbrand, "branded_nm");
                //  bll.vBindingFieldValueToCombo(ref cbsize, "size");
                bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_get", "salespointcd", "salespoint_desc");
                bll.vBindingFieldValueToCombo(ref cbuomfrom, "uom");
                bll.vBindingFieldValueToCombo(ref cbuomto, "uom");
                bll.vBindingFieldValueToCombo(ref cbpacking, "packing");
                //Clear TEmporary
                arr.Clear();
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                bll.vDelWrkSalespoint(arr);
                bll.vDelWrkUomConversion(arr);
                dtend.Text = "31/12/9999";
                txunitprice.ReadOnly = true;
                txvendorprice.ReadOnly = true;

                // bll.sFormat2ddmmyyyy(ref dtend);
                cbbranded_SelectedIndexChanged(sender, e);
                if (Request.QueryString["item"] != null)
                {

                    SqlDataReader rs = null;
                    string sProdCode = "";
                    string sGroupCode = "";
                    string sBranded = "";
                    sItemID = Request.QueryString["item"].ToString();
                    txitemcode.Text = sItemID;
                    arr.Clear();
                    arr.Add(new cArrayList("@item_cd", sItemID));
                    bll.vGetMstItem2(arr, ref rs);
                    while (rs.Read())
                    {
                        txitemname.Text = rs["item_nm"].ToString();
                        txitemshortname.Text = rs["item_shortname"].ToString();
                        txarabic.Text = rs["item_arabic"].ToString();
                        //   cbvendorcode.SelectedValue= rs["vendor_cd"].ToString();
                        if (rs["start_dt"] == DBNull.Value)
                        {
                            dtstart.Text = System.DateTime.Today.ToString("d/M/yyyy");
                        }
                        else
                        {
                            dtstart.Text = Convert.ToDateTime(rs["start_dt"]).ToString("d/M/yyyy");
                        }
                        //bll.sFormat2ddmmyyyy(ref dtstart);
                        if (rs["end_dt"] == DBNull.Value)
                        { dtend.Text = "31/12/9999"; }
                        else
                        {
                            dtend.Text = Convert.ToDateTime(rs["end_dt"]).ToString("d/M/yyyy");
                        }
                        // bll.sFormat2ddmmyyyy(ref dtend);
                        cbuom.SelectedValue = rs["uom_base"].ToString();
                        txsizesearch.Text = rs["size"].ToString();
                        hdsize.Value = rs["size"].ToString();
                        cbpacking.SelectedValue = rs["packing"].ToString();
                        Image1.ImageUrl = rs["imagename"].ToString();
                        txunitprice.Text = rs["price_sell"].ToString();
                        sProdCode = rs["prod_cd"].ToString();
                        txvendorprice.Text = rs["price_buy"].ToString();
                        txremark.Text = rs["remark"].ToString();
                        txbarcodectn.Text = rs["barcode_ctn"].ToString();
                        txbarcodebox.Text = rs["barcode_box"].ToString();
                        txbarcodeprod.Text = rs["barcode_prod"].ToString();
                        txlength.Text = rs["dim_length"].ToString();
                        txwidth.Text = rs["dim_width"].ToString();
                        txheigth.Text = rs["dim_depth"].ToString();
                        lbstatus.Text = bll.vLookUp("select fld_desc from tfield_value where fld_valu='" + rs["item_sta_id"].ToString() + "' and fld_nm='item_sta_id'");
                        cbbrand.SelectedValue = rs["branded_nm"].ToString();
                        cbvendor.SelectedValue = rs["vendor_cd"].ToString();
                    }
                    rs.Close();
                    // This handle product code header
                    sGroupCode = bll.vLookUp("select prod_cd_parent from tmst_product where prod_cd='" + sProdCode + "'");
                    sBranded = bll.vLookUp("select prod_cd_parent from tmst_product where prod_cd='" + sGroupCode + "'");
                    cbbranded.SelectedValue = sBranded;
                    cbbranded_SelectedIndexChanged(sender, e);
                    cbsubbranded.SelectedValue = sGroupCode;
                    cbsubbranded_SelectedIndexChanged(sender, e);
                    cbproduct.SelectedValue = sProdCode;

                    //Get TWRK Converion
                    arr.Clear();
                    arr.Add(new cArrayList("@item_cd", sItemID));
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    bll.vInsertWrkItemConversionFromReal(arr);
                    bll.vInsertWrkSalespointFromReal(arr);
                    arr.Clear();
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    bll.vBindingGridToSp(ref grduomconvert, "sp_twrk_uomconversion_get", arr);
                    bll.vBindingGridToSp(ref grdsp, "sp_twrk_salespoint_get", arr);
                    // make readonly
                    txitemcode.CssClass = "form-control makeitreadonly";
                    txitemcode.ReadOnly = true;
                    //All should be REadonly
                    MakeItReadOnly(true);
                    //reqsize.Enabled = false;                

                }

            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstentry2");
                Response.Redirect("fm_ErrorPage.aspx");
            }
        }
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
    }

    void MakeItReadOnly(bool bAdd)
    {
        if (bAdd)
        {
            txarabic.ReadOnly = true;
            txconvertqty.ReadOnly = true;
          //  txcostcenter.ReadOnly = true;
            txitemcode.ReadOnly = true;
            txitemname.ReadOnly = true;
            txitemshortname.ReadOnly = true;
            txremark.ReadOnly = true;
            //txsize.ReadOnly = true;
            txunitprice.ReadOnly = true;
           // cbvendorcode.Enabled = true;
            txvendorprice.ReadOnly = true;
            dtend.ReadOnly = true;
            dtstart.ReadOnly = true;
            cbbranded.Enabled = false;
            cbpacking.Enabled = false;
            cbprodtype.Enabled = false;
            cbproduct.Enabled = false;
            cbsalespoint.Enabled = false;
            cbuom.Enabled = false;
            cbuomfrom.Enabled = false;
            cbuomto.Enabled = false;
            cbvendor.Enabled = false;
            cbsubbranded.Enabled = false;
            btadd.Enabled = false;
            btaddconvert.Enabled = false;
            btprint.Enabled = true;
            btsave.Enabled = false;
        }
        else
        {
            txarabic.ReadOnly = false;
            txconvertqty.ReadOnly = false;
      //      txcostcenter.ReadOnly = false;
            txitemcode.ReadOnly = false;
            txitemname.ReadOnly = false;
            txitemshortname.ReadOnly = false;
            txremark.ReadOnly = false;
            //txsize.ReadOnly = false;
            txunitprice.ReadOnly = true;
          //  cbvendorcode.Enabled = false;
            txvendorprice.ReadOnly = true;
            dtend.ReadOnly = false;
            dtstart.ReadOnly = false;
            cbbranded.Enabled = true;
            cbpacking.Enabled = true;
            cbprodtype.Enabled = true;
            cbproduct.Enabled = true;
            cbsalespoint.Enabled = true;
            cbuom.Enabled = true;
            cbuomfrom.Enabled = true;
            cbuomto.Enabled = true;
            cbvendor.Enabled = true;
            cbsubbranded.Enabled = true;
            btadd.Enabled = true;
            btaddconvert.Enabled = true;
            btprint.Enabled = false;
            btsave.Enabled = true;
        }
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> lGetListSp(string prefixText, int count)
    {
        SqlDataReader rs = null;
        cbll bll = new cbll();
        List<cArrayList> arr = new List<cArrayList>();
        List<string> spname = new List<string>();
        arr.Add(new cArrayList("@salespoint_nm", prefixText));
        bll.vSearchMstSalespoint(arr, ref rs);
        while (rs.Read())
        {
            spname.Add(rs["salespointcd"].ToString() + " | " + rs["salespoint_nm"].ToString());
        }
        rs.Close();
        return (spname);
    }
    protected void cbbranded_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@level_no", "2"));
            arr.Add(new cArrayList("@prod_cd_parent", cbbranded.SelectedValue.ToString()));
            bll.vBindingComboToSp(ref cbsubbranded, "sp_tmst_product_get", "prod_cd", "prod_nm", arr);
            bll.vBindingFieldValueToCombo(ref cbuom, "uom");
            cbsubbranded_SelectedIndexChanged(sender, e);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void cbsubbranded_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@level_no", "3"));
            arr.Add(new cArrayList("@prod_cd_parent", cbsubbranded.SelectedValue.ToString()));
            bll.vBindingComboToSp(ref cbproduct, "sp_tmst_product_get", "prod_cd", "prod_nm", arr);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
      
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {

            if (txitemshortname.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Short name can not empty !','Short Name','warning');", true);
                return;
            }
            if (txarabic.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Arabic Name can not be empty!','Arabic Name','warning');", true);
                return;

            }

            if (dtstart.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Start date can not empty','Start Date','warning');", true);
                return;
            }
            //if (txarabic.Text == "")
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "alert('Arabic name can not empty !');", true);
            //    return;
            //}

            //if (txsize.Text == "")
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "alert('Size can not empty !');", true);
            //    return;
            //}

            //if (txvol.Text == "")
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "alert('Size name can not empty !');", true);
            //    return;
            //}

            if (grduomconvert.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Conversion Item UOM !','Conversion','warning');", true);
                return;
            }

            if (grdsp.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Item selling to salespoint !','Item sell to','warning');", true);
                return;
            }
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@item_cd", txitemcode.Text));
            arr.Add(new cArrayList("@item_nm", txitemname.Text));
            arr.Add(new cArrayList("@item_shortname", txitemshortname.Text));
            arr.Add(new cArrayList("@item_arabic", txarabic.Text));
            arr.Add(new cArrayList("@vendor_cd", cbvendor.SelectedValue.ToString()));
            arr.Add(new cArrayList("@costcenter", "0000000"));
            arr.Add(new cArrayList("@prod_cd", cbproduct.SelectedValue.ToString()));
            arr.Add(new cArrayList("@uom_base", cbuom.SelectedValue.ToString()));
            arr.Add(new cArrayList("@size", (hdsize.Value.ToString() == null ? txsizesearch.Text.ToString() : hdsize.Value.ToString()) ));
            if (txvendorprice.Text != "")
            {
                arr.Add(new cArrayList("@price_buy", txvendorprice.Text));
            }
            else
            {
                arr.Add(new cArrayList("@price_buy", null));
            }
            if (txunitprice.Text != "")
            {
                arr.Add(new cArrayList("@price_sell", txunitprice.Text));
            }
            else
            {
                arr.Add(new cArrayList("@price_sell", null));
            }
            arr.Add(new cArrayList("@deleted", "0"));
            if (uplfile.FileName != "")
            {
                arr.Add(new cArrayList("@imagename", "image/I" + txitemcode.Text + ".jpg"));
            }

            arr.Add(new cArrayList("@start_dt", DateTime.ParseExact(dtstart.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            if (dtend.Text != "")
            {
                arr.Add(new cArrayList("@end_dt", DateTime.ParseExact(dtend.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            }
            else
            {
                arr.Add(new cArrayList("@end_dt", null));
            }
            arr.Add(new cArrayList("@remark", txremark.Text));
            arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@prod_typ", cbprodtype.SelectedValue.ToString()));
            arr.Add(new cArrayList("@packing", cbpacking.SelectedValue.ToString()));
            if (txlength.Text != "")
            {
                arr.Add(new cArrayList("@dim_length", txlength.Text));
            }
            else
            {
                arr.Add(new cArrayList("@dim_length", null));
            }
            if (txwidth.Text != "")
            {
                arr.Add(new cArrayList("@dim_width", txwidth.Text));
            }
            else
            {
                arr.Add(new cArrayList("@dim_width", null));
            }
            if (txwidth.Text != "")
            {
                arr.Add(new cArrayList("@dim_depth", txheigth.Text));
            }
            else
            {
                arr.Add(new cArrayList("@dim_depth", null));
            }
            arr.Add(new cArrayList("@barcode_ctn", txbarcodectn.Text));
            arr.Add(new cArrayList("@barcode_box", txbarcodebox.Text));
            arr.Add(new cArrayList("@barcode_prod", txheigth.Text));
            arr.Add(new cArrayList("@branded_nm", cbbrand.SelectedValue.ToString()));
            arr.Add(new cArrayList("@item_sta_id", "A"));
            bll.vInsertMstItem(arr);
            if (uplfile.FileName != "")
            {
                uplfile.SaveAs(bll.sGetControlParameter("image_path") + "I" + txitemcode.Text + ".jpg");
            }

            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@item_cd", txitemcode.Text));
            bll.vInsertItemCoversionFromWrk(arr);
            bll.vInsertSalesPointFromWrk(arr);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "wd", "sweetAlert('Item has been saved ..','Item','success');", true);
            lbstatus.Text = bll.vLookUp("select fld_desc from tfield_value where fld_valu=(select item_sta_id from tmst_item where item_cd='" + txitemcode.Text + "') and fld_nm='item_sta_id'");
            btsave.CssClass = "divhid";
            btprint.CssClass = "button2 print";
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            if (chk.Checked)
            {
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                bll.vInsertWrkSalespointAll(arr);
            }
            else
            {
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
                arr.Add(new cArrayList("@salespoint_nm", cbsalespoint.SelectedItem.Text));
                bll.vInsertWrkSalespoint(arr);
            }
            arr.Clear(); arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grdsp, "sp_twrk_salespoint_get", arr);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }

    }
    protected void grdsp_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Label lbspcode = (Label)grdsp.Rows[e.RowIndex].FindControl("lbsalespointcode");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", lbspcode.Text));
            bll.vDelWrkSalespoint(arr);
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grdsp, "sp_twrk_salespoint_get", arr);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grdsp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            grdsp.PageIndex = e.NewPageIndex;
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grdsp, "sp_twrk_salespoint_get", arr);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btaddconvert_Click(object sender, EventArgs e)
    {
        try
        {
            double dQtyCheck = 0;

            if (!double.TryParse(txconvertqty.Text, out dQtyCheck))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Qty must be numeric','Check Qty','warning');", true);
                return;
            }

            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@item_cd", txitemcode.Text));
            arr.Add(new cArrayList("@uom_from", cbuomfrom.SelectedValue.ToString()));
            arr.Add(new cArrayList("@uom_to", cbuomto.SelectedValue.ToString()));
            arr.Add(new cArrayList("@qty", txconvertqty.Text));
            bll.vInsertWrkUomConversion(arr);
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grduomconvert, "sp_twrk_uomconversion_get", arr);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grduomconvert_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Label lbuomfrom = (Label)grduomconvert.Rows[e.RowIndex].FindControl("lbuomfrom");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@uom_from", lbuomfrom.Text));
            arr.Add(new cArrayList("@item_cd", txitemcode.Text));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vDelWrkUomConversion(arr); arr.Clear(); arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grduomconvert, "sp_twrk_uomconversion_get", arr);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btedit_Click(object sender, EventArgs e)
    {

        MakeItReadOnly(false);
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        SqlDataReader rs = null;
        cbll bll = new cbll();
        List<cArrayList> arr = new List<cArrayList>();
        List<string> sSize = new List<string>();
        arr.Add(new cArrayList("@fld_nm", "size"));
        arr.Add(new cArrayList("@fld_valu", prefixText));
        bll.vSearchFldValue(arr, ref rs);
        while (rs.Read())
        {
            sSize.Add(rs["fld_valu"].ToString());
        }
        rs.Close();
        return (sSize.ToArray());
    }
}