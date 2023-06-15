using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
//using Org.BouncyCastle.Crypto.Engines;
public partial class fm_internaltransfer2 : System.Web.UI.Page
{
    cbll bll = new cbll();
    creport rep = new creport();
    decimal totalQty = 0;
    decimal totalStockQty = 0;
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                bll.vBindingFieldValueToCombo(ref cbuom, "uom_tf", "uom");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
                lbstatus.Text = bll.vLookUp("select fld_desc from tfield_value where fld_nm='sta_id' and fld_valu='N'");
                dttrf.Text = Request.Cookies["waz_dt"].Value.ToString();
                DateTime dateloading = DateTime.ParseExact(dttrf.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                dateloading = dateloading.AddDays(0);
                string strdateloading = string.Format("{0:d/M/yyyy}", dateloading);
                //dateloading = DateTime.ParseExact(strdateloading, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                //strdateloading = dateloading.ToString("d");
                dtloading.Text = strdateloading;
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@trf_no", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vDelInternalTransferDtl(arr);
                //bll.vDelWrkItemQty(arr);
                bindinggrd();
                txtransferno.Text = "NEW";
                txstk.Text = "0";
                //if not transfer BS no need reason
                lbreason.Visible = false;
                cbreason.Visible = false;
                //  bll.sFormat2ddmmyyyy(ref dttrf);
                btsave.Visible = false;
                btprint.Visible = false;
                btsearch.Visible = false;
            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_internaltransfer2");
                Response.Redirect("fm_ErrorPage.aspx");
            }

        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        string sItem = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@item_nm", prefixText));
        List<string> lItem = new List<string>();
        System.Data.SqlClient.SqlDataReader rs = null;
        bll.vSearchMstItem(arr, ref rs);
        while (rs.Read())
        {
            sItem = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["item_cd"].ToString() + " - " + rs["item_shortname"].ToString() + " - " + rs["branded_nm"].ToString(), rs["item_cd"].ToString());
            lItem.Add(sItem);
        }
        rs.Close();
        return (lItem.ToArray());

    }
   
    protected void btsearch_Click(object sender, EventArgs e)
    {
        try
        {
            double dQtyCheck = 0;

            if (!double.TryParse(txqty.Text, out dQtyCheck))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Qty must be numeric','Check Qty','warning');", true);
                return;
            }
            //lbdisc.Text = "Discount has been applied in this item , pleae check !";
            if (Request.Cookies["waz_dt"].Value.ToString() != dttrf.Text)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Out Of period transaction','Check Transfer Date','warning');", true);
                return;
            }
            if (Convert.ToDouble(txqty.Text) <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Qty must bigger then zero','Check Qty','warning');", true);
                return;
            }
            if ((cbfrom.SelectedValue.ToString() == cbto.SelectedValue.ToString()) && (cbbinfrom.SelectedValue.ToString() == cbbinto.SelectedValue.ToString()))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Can not same destination and from','Check Destination','warning');", true);
                return;
            }

            if ((cbbinto.SelectedValue.ToString() == "NES") && (cbbinfrom.SelectedValue.ToString() != "NE"))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Can not transfer to NE Special if not from NE Stock','Check Destination','warning');", true);
                return;
            }

            if (hditem.Value.ToString() == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Item not Yet Selected','Check Item','warning');", true);
                return;
            }

            DateTime ddate = DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            double dStock = 0, dQty = 0;
            string sStock = "";

            if (txtransferno.Text == "" || txtransferno.Text == "NEW")
            {
                sStock = bll.vLookUp("select isnull(dbo.[sfnGetStockBooking]('" + Request.Cookies["sp"].Value.ToString() + "','" + hditem.Value.ToString() + "','" + cbbinfrom.SelectedValue.ToString() + "','" + cbfrom.SelectedValue.ToString() + "','0','" + ddate.Year + "-" + ddate.Month + "-" + ddate.Day + "'),0)");
            }
            else
            {
                { sStock = bll.vLookUp("select isnull(dbo.sfnGetStockBookingseries('" + Request.Cookies["sp"].Value.ToString() + "','" + hditem.Value + "','" + cbbinfrom.SelectedValue.ToString() + "','" + cbfrom.SelectedValue.ToString() + "','" + txtransferno.Text + "'),0) "); }
            }

            dStock = double.Parse(sStock);
            dQty = double.Parse(txqty.Text);

            if (optwhs.SelectedValue != "B")
            {
                dQty = double.Parse(bll.vLookUp("select dbo.sfnUomQtyConv('"+hditem.Value.ToString()+"','"+cbuom.SelectedValue.ToString()+"','CTN','"+txqty.Text+"')"));
                if (dStock < dQty)
                {
                    string smsgstockallert = "Stock is not enough ! " + hditem.Value.ToString() + " Qty Stock :" + dStock.ToString() + " Qty Transfer : " + txqty.Text;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('" + smsgstockallert + "' ,'Stock Not Enough','warning');", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
                    return;
                }
            }


            string trf_no;
            if (txtransferno.Text == "" || txtransferno.Text == "NEW") { trf_no = Request.Cookies["usr_id"].Value.ToString(); } else { trf_no = txtransferno.Text; }
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@trf_no", trf_no));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@item_cd", hditem.Value.ToString()));
            arr.Add(new cArrayList("@qty", txqty.Text));
            arr.Add(new cArrayList("@stock_qty", txstk.Text));
            arr.Add(new cArrayList("@uom", cbuom.SelectedValue.ToString()));
            bll.vInserttinternal_transfer_dtl(arr);

            //if (txtransferno.Text != "NEW")  //add tblstock when insert after save
            //{
            //    List<cArrayList> arr2 = new List<cArrayList>();
            //    arr2.Add(new cArrayList("@trf_no", trf_no));
            //    arr2.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            //    arr2.Add(new cArrayList("@item_cd", hditem.Value.ToString()));
            //    arr2.Add(new cArrayList("@trf_typ", optwhs.SelectedValue));
            //  //  bll.vInserttinternal_transfer_dtl_tblstock(arr2);
            //}
            //arr.Clear();
            //arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            //bll.vBindingGridToSp(ref grd, "sp_twrk_itemqty_get", arr);
            //txitem.Text = "";
            //txqty.Text = "";
            bindinggrd();
            txstk.Text = "0";
            //txitem.Focus();
            //   txqty.Text = "";
            //  txitem.Focus();

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_internaltransfer2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    private void bindinggrd()
    {
        try
        {
            string trf_no;
            if (txtransferno.Text == "" || txtransferno.Text == "NEW") { trf_no = Request.Cookies["usr_id"].Value.ToString(); } else { trf_no = txtransferno.Text; }
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@trf_no", trf_no));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_tinternal_transfer_dtl_get", arr);
            txitem.Text = "";
            hditem.Value = "";
            txqty.Text = "";
            //txitem.Focus();
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_internaltransfer2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void optwhs_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            if (optwhs.SelectedValue.ToString() == "I")
            {
                //txqty.Attributes.Add("type", "number");
                lbtrtx1.Text = "Warehouse";
                lbtrtx2.Text = "Van";
                bll.vBindingComboToSp(ref cbto, "sp_tmst_vehicle_salesman_get", "vhc_cd", "vhc_desc", arr);
                bll.vBindingComboToSp(ref cbfrom, "sp_tmst_warehouse_get", "whs_cd", "whs_nm", arr);
                lbloadingdt.Visible = true;

                dtloading.Visible = true;
                cburgent.Visible = true;
            }
            else if (optwhs.SelectedValue.ToString() == "O")
            {
                txqty.Attributes.Remove("type");
                lbtrtx1.Text = "Warehouse";
                lbtrtx2.Text = "Warehouse";
                bll.vBindingComboToSp(ref cbto, "sp_tmst_warehouse_get", "whs_cd", "whs_nm", arr);
                bll.vBindingComboToSp(ref cbfrom, "sp_tmst_warehouse_get", "whs_cd", "whs_nm", arr);
                lbloadingdt.Visible = false;

                dtloading.Visible = false;
                cburgent.Visible = false;
            }
            else if (optwhs.SelectedValue.ToString() == "V")
            {
                txqty.Attributes.Remove("type");
                lbtrtx1.Text = "Van";
                lbtrtx2.Text = "Warehouse";
                bll.vBindingComboToSp(ref cbfrom, "sp_tmst_vehicle_salesman_get", "vhc_cd", "vhc_desc", arr);
                // bll.vBindingComboToSp(ref cbto, "sp_tmst_warehouse_get","whs_cd","whs_nm",arr);
                bll.vBindingComboToSp(ref cbto, "sp_tmst_warehouse_get", "whs_cd", "whs_nm", arr);
                lbloadingdt.Visible = false;

                dtloading.Visible = false;
                cburgent.Visible = false;
            }
            else
            {
                arr.Clear();
                lbtrtx1.Text = "Warehouse";
                lbtrtx2.Text = "Warehouse";
                bll.vBindingComboToSp(ref cbfrom, "sp_tmst_warehouse_get", "whs_cd", "whs_nm", arr);
                bll.vBindingComboToSp(ref cbto, "sp_tmst_warehouse_all_get", "whs_cd", "whs_desc", arr);
                lbloadingdt.Visible = true;

                dtloading.Visible = true;
                cburgent.Visible = true;
            }

            cbfrom_SelectedIndexChanged(sender, e);
            cbto_SelectedIndexChanged(sender, e);
            optwhs.CssClass = "ro";
            optwhs.Enabled = false;
            if (lbstatus.Text == "NEW") { btsave.Visible = true; btprint.Visible = false; btsearch.Visible = true; }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_internaltransfer2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void cbfrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            List<cArrayList> arr = new List<cArrayList>();
            if (optwhs.SelectedValue.ToString() == "I")
            {
                arr.Clear();
                arr.Add(new cArrayList("@whs_cd", cbfrom.SelectedValue.ToString()));
                arr.Add(new cArrayList("@qry_cd", "showbininternalfrom"));
                bll.vBindingComboToSp(ref cbbinfrom, "sp_twarehouse_bin_getbytype", "bin_cd", "bin_nm", arr);

            }
            if (optwhs.SelectedValue.ToString() == "O")
            {
                arr.Clear();
                arr.Add(new cArrayList("@whs_cd", cbfrom.SelectedValue.ToString()));
                arr.Add(new cArrayList("@qry_cd", "showbininternalfrom"));
                bll.vBindingComboToSp(ref cbbinfrom, "sp_twarehouse_bin_getbytype", "bin_cd", "bin_nm", arr);

            }
            else if (optwhs.SelectedValue.ToString() == "V")
            {
                string trf_no = "";
                arr.Clear();
                arr.Add(new cArrayList("@vhc_cd", cbfrom.SelectedValue.ToString()));
                bll.vBindingComboToSp(ref cbbinfrom, "sp_tvan_bin_get", "bin_cd", "bin_nm", arr);
                arr.Clear();

                if (txtransferno.Text == "" || txtransferno.Text == "NEW")
                {
                    trf_no = Request.Cookies["usr_id"].Value.ToString();
                    arr.Add(new cArrayList("@trf_no", trf_no));
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    arr.Add(new cArrayList("@salesman_cd", cbfrom.SelectedValue.ToString()));
                    arr.Add(new cArrayList("@bin_cd", cbbinfrom.SelectedValue.ToString()));
                    bll.vAutoInternalTransfer(arr);
                    bindinggrd();
                }
            }
            else
            {
                arr.Clear();
                cbfrom.SelectedValue = bll.vLookUp("select whs_cd from tmst_warehouse where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                arr.Add(new cArrayList("@whs_cd", cbfrom.SelectedValue.ToString()));
                arr.Add(new cArrayList("@qry_cd", "showbininternalfrom"));
                bll.vBindingComboToSp(ref cbbinfrom, "sp_twarehouse_bin_getbytype", "bin_cd", "bin_nm", arr);
                //cbfrom.CssClass = "ro";
                //cbbinfrom.CssClass = "ro";
            }

            //try { cbbinfrom.SelectedValue = "BINGS"; cbbinto.SelectedValue = "BINGS"; }
            //catch { cbbinfrom.SelectedValue = "GS"; cbbinto.SelectedValue = "GS"; }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_internaltransfer2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void cbto_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            List<cArrayList> arr = new List<cArrayList>();
            if (optwhs.SelectedValue.ToString() == "I")
            {

                arr.Add(new cArrayList("@vhc_cd", cbto.SelectedValue.ToString()));
                bll.vBindingComboToSp(ref cbbinto, "sp_tvan_bin_get", "bin_cd", "bin_nm", arr);

                // bll.vBindingComboToSp(ref cbbinto, "sp_twarehouse_bin_get", "bin_cd", "bin_nm", arr);
            }
            else if (optwhs.SelectedValue.ToString() == "O")
            {
                arr.Add(new cArrayList("@whs_cd", cbto.SelectedValue.ToString()));
                bll.vBindingComboToSp(ref cbbinto, "sp_twarehouse_bin_get", "bin_cd", "bin_nm", arr);
            }
            else if (optwhs.SelectedValue.ToString() == "V")
            {
                arr.Add(new cArrayList("@whs_cd", cbto.SelectedValue.ToString()));
                bll.vBindingComboToSp(ref cbbinto, "sp_twarehouse_bin_get", "bin_cd", "bin_nm", arr);
            }
            else
            {
                arr.Add(new cArrayList("@whs_cd", cbto.SelectedValue.ToString()));
                bll.vBindingComboToSp(ref cbbinto, "sp_twarehouse_bin_get", "bin_cd", "bin_nm", arr);
            }
            string sDefBin = bll.sGetControlParameter("defaultbin");
            cbbinto.SelectedValue = sDefBin;
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_internaltransfer2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
       
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {

            if (optwhs.SelectedValue.ToString() == "I" && (txtransferno.Text == "" || txtransferno.Text == "NEW"))
            {
                DateTime dateloading = DateTime.ParseExact(dttrf.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                string suser = Request.Cookies["usr_id"].Value.ToString();
                string sdate = dateloading.Day.ToString();
                string sMonth = dateloading.Month.ToString();
                string syear = dateloading.Year.ToString();
                string dttrfdate = syear + "-" + sMonth + "-" + sdate;
                string sCheckloadingpriority = bll.vLookUp("select dbo.fn_checkITItemPriority('" + cbto.SelectedValue + "','" + dttrfdate + "','" + suser + "')");
                if (sCheckloadingpriority != "ok")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There are item Priority should be loading ','" + sCheckloadingpriority + "','warning');", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
                    return;
                }
            }
            string sCheckpendinglastmonth = bll.vLookUp("select dbo.fn_checkpendinglastmonth('" + Request.Cookies["sp"].Value.ToString() + "')");
            if (sCheckpendinglastmonth != "ok")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There pending transaction','" + sCheckpendinglastmonth + "','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
                return;
            }
            if (txmanualno.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Attention','Please fill manual !!','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
                return;
            }
            string sManualNo = bll.vLookUp("select dbo.fn_checkmanualno('internaltransfer','" + txmanualno.Text + "','" + Request.Cookies["sp"].Value.ToString() + "')"); //bll.vLookUp("select dbo.fn_getmanualno('" + txmanualinv.Text + "','" + txorderno.Text + "')");
            if (sManualNo != "ok")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('This manual no. " + txmanualno.Text + "  already used','Please use another !!','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
                return;
            }
            //if (Request.Cookies["waz_dt"].Value.ToString() != dttrf.Text)
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Attention','Transaction Date Out Of Periode Date','error');", true);
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
            //    return;
            //}
            if (grd.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select Item for transfer','Item Empty','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
                return;
            }

            if (optwhs.SelectedValue == "" || optwhs.SelectedValue.Equals(null))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('please select Source and Destination for item transfer !','Source Destination','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
                return;
            }

            if (txmanualno.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Manual no can not be empty !','manual no.','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
                return;
            }
            if (grd.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Item has not yet selected !','Please select Item To Be Tranfer','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
                return;
            }
            if (lbstatus.Text == "COMPLETE")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Attention','Transaction has been complete, data can not save','error');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
                return;
            }

            foreach (GridViewRow row in grd.Rows)
            {
                Label lbqty = (Label)row.FindControl("lbqty");
                Label lbitemcode = (Label)row.FindControl("lbitemcode");
                decimal dqty = Convert.ToDecimal(lbqty.Text);
                if (dqty < 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Cant add Qty Minus','Item. " + lbitemcode.Text + "','warning');", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Stock is not enough !' " + lbitemcode.Text + " Qty Stock :" + strbookqty + " Qty Transfer : " + lbqty.Text + " ,'Stock Not Enough','warning');", true);
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
                    return;
                }
            }

            Boolean stockAvb = true;
            foreach (GridViewRow row in grd.Rows)
            {
                Label lbqty = (Label)row.FindControl("lbqty");
                Label lbitemcode = (Label)row.FindControl("lbitemcode");
                string strbookqty = bll.vLookUp("select dbo.sfnGetStockBooking('" + Request.Cookies["sp"].Value.ToString() + "','" + lbitemcode.Text.ToString() + "','" + cbbinfrom.SelectedValue.ToString() + "','" + cbfrom.SelectedValue.ToString() + "','0','" + System.DateTime.ParseExact(dttrf.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture) + "')");
                if (Convert.ToDecimal(strbookqty) < 0) { strbookqty = "0"; }
                decimal dqty = Convert.ToDecimal(lbqty.Text);
                decimal dbookqty = Convert.ToDecimal(strbookqty);
                if (dqty > dbookqty)
                {
                    stockAvb = false;
                    
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Stock is not enough !' " + lbitemcode.Text + " Qty Stock :" + strbookqty + " Qty Transfer : " + lbqty.Text + " ,'Stock Not Enough','warning');", true);
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
                    //return;
                }
            }

            if (txremark.Text == string.Empty)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Remark is mandatory' ,'Please fill remark','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
                return;
            }

            if (txtransferno.Text == "" || txtransferno.Text == "NEW")
            {
                string ssta_id;
                string sreason;
                if (cbbinto.SelectedValue == "BS" || cbbinto.SelectedValue == "NE") { ssta_id = "W"; } else { if (stockAvb) { ssta_id = "A"; } else { ssta_id = "W"; } };
                if (cbbinto.SelectedValue == "BS" || cbbinto.SelectedValue == "NE") sreason = cbreason.SelectedValue; else sreason = "";
                ssta_id = "w";
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@trf_dt", DateTime.ParseExact(dttrf.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@trf_typ", optwhs.SelectedValue.ToString()));
                arr.Add(new cArrayList("@whs_cd_from", cbfrom.SelectedValue.ToString()));
                arr.Add(new cArrayList("@bin_from", cbbinfrom.SelectedValue.ToString()));
                arr.Add(new cArrayList("@whs_cd_to", cbto.SelectedValue.ToString()));
                arr.Add(new cArrayList("@bin_to", cbbinto.SelectedValue.ToString()));
                arr.Add(new cArrayList("@item_cd", hditem.Value.ToString()));
                arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@manual_no", txmanualno.Text));
                arr.Add(new cArrayList("@tab_no", txtabno.Text));
                arr.Add(new cArrayList("@prevstk", chprevstk.Checked));
                arr.Add(new cArrayList("@sta_id", ssta_id));
                arr.Add(new cArrayList("@reason", sreason));
                arr.Add(new cArrayList("@remark", txremark.Text));
                string sTrfNo = "";
                bll.vInsertInternalTransfer(arr, ref sTrfNo);
                txtransferno.Text = sTrfNo;
                if (ssta_id == "A") { lbstatus.Text = "Approved"; } else if (ssta_id == "W") { lbstatus.Text = "Waiting Aproval"; } else if (ssta_id == "N") { lbstatus.Text = "New"; }
                if (cbbinto.SelectedValue == "BS" || cbbinto.SelectedValue == "NE")
                {
                    //string slink_branch = bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='link_branch'");
                    //string ssp = Request.Cookies["sp"].Value.ToString();
                    //string ssp_sn = bll.vLookUp("select salespointcd + '-'+salespoint_sn from tmst_salespoint where salespointcd='"+ssp+"'");
                    //string ssp_nm = bll.vLookUp("select salespointcd + '-'+salespoint_nm from tmst_salespoint where salespointcd='" + ssp + "'");
                    //lbstatus.Text = "Waiting Approval";
                    //List<string> lapproval = bll.lGetApproval("internaltransfer", 1);
                    ////Random rnd = new Random();
                    ////int nRnd = rnd.Next(1000, 9999);
                    //string ssp1 = ssp.Substring(0, 1);
                    //string ssp2 = ssp.Substring(0, 1) + ssp.Substring(ssp.Length - 1);
                    //string strantoken = "00"+bll.vLookUp("select fld_valu from tfield_value where fld_nm='trantoken' and fld_desc='internaltransfer'"); 
                    //string stoken = txtransferno.Text.Substring(txtransferno.Text.Length - 4);
                    //if (stoken.Substring(0, 3) == "000") { stoken = strantoken + stoken.Substring(stoken.Length - 1); }
                    //if (stoken.Substring(0, 2) == "00") { stoken = ssp2+stoken.Substring(stoken.Length - 2); }
                    //if (stoken.Substring(0, 1) == "0") { stoken = ssp1 + stoken.Substring(stoken.Length - 3); }
                    //string stoken2 = bll.vLookUp("select short_ip from tmst_salespoint where salespointcd=(select parm_valu from tcontrol_parameter where parm_nm='salespoint')") + stoken;
                    //string sMsg = ssp_sn+" Transfer " + cbbinfrom.SelectedItem + " to " + cbbinto.SelectedItem + " tran no." + txtransferno.Text + ", detail item in email, do you want to approved : (Y/N)" + stoken2;
                    //arr.Clear();
                    //arr.Add(new cArrayList("@token", stoken2));
                    //arr.Add(new cArrayList("@msg", sMsg));
                    //arr.Add(new cArrayList("@doc_typ", "internaltransfer"));
                    //arr.Add(new cArrayList("@to", lapproval[0]));
                    //arr.Add(new cArrayList("@doc_no", txtransferno.Text));
                    //bll.vInsertSmsOutbox(arr); //by yanto 15-11-2016

                    //String sText = "<html><head><body>Dear, New Internal transfer  " + cbbinfrom.SelectedItem + " to " + cbbinto.SelectedItem + " has been created , with no. " + txtransferno.Text +
                    //    "<p> Date  : " + dttrf.Text + " Salespoint : " + ssp_nm +
                    //     "<p> for detail please see attached file </p>" +
                    //     "<p>Please Click this  for approved : <a href='" + slink_branch + "/landingpage.aspx?trnname=internaltransfer&salespointcd=" + ssp + "&RefNo=" + txtransferno.Text + "&appcode=" + stoken.ToString() + "&sta=A'>Approve</a>, or for rejected please click <a href='" + slink_branch + "/landingpage.aspx?trnname=internaltransfer&salespointcd=" + ssp + "&RefNo=" + txtransferno.Text + "&appcode=" + stoken.ToString() + "&sta=L'>Reject</a></p>";
                    //List<string> lapproval2 = bll.lGetApprovalall("internaltransfer");
                    //int n = sText.Length;
                    //string sPath = bll.sGetControlParameter("image_path");
                    //string sPdfName = txtransferno.Text + ".pdf";
                    //string sSubject = "New Internal Transfer No. " + txtransferno.Text + " Has Been Created";
                    //string semail="0";
                    //int i=0;
                    //foreach (var email in lapproval2.Skip(1))
                    //{
                    //    i = i + 1;
                    //    if (i % 2 != 0)
                    //    { 
                    //    if (semail == "0")
                    //        semail = email;
                    //    else
                    //        semail = semail + ';' + email;
                    //    }
                    //}
                    //arr.Clear();
                    //arr.Add(new cArrayList("@trf_no", txtransferno.Text));
                    //arr.Add(new cArrayList("@SalesPointCD", ssp));
                    //arr.Add(new cArrayList("@printby", Request.Cookies["usr_id"].Value.ToString()));
                    //rep.vShowReportToPDFWithSP("rptInternalTranfer.rpt", arr, sPath + sPdfName);
                    //arr.Clear();
                    //arr.Add(new cArrayList("@token", stoken));
                    //arr.Add(new cArrayList("@doc_typ", "internaltransfer"));
                    //arr.Add(new cArrayList("@to", semail));
                    //arr.Add(new cArrayList("@doc_no", txtransferno.Text));
                    //arr.Add(new cArrayList("@emailsubject", sSubject));
                    //arr.Add(new cArrayList("@msg", sText));
                    //arr.Add(new cArrayList("@file_attachment", sPdfName));
                    //bll.vInsertEmailOutbox(arr); //by yanto 23-1-2017

                    ////bll.vSendMail(semail, sSubject, sText, sPdfName);
                    //arr.Clear();
                    //arr.Add(new cArrayList("@trxcd", "internaltransfer"));
                    //arr.Add(new cArrayList("@token", stoken));
                    //arr.Add(new cArrayList("@doc_no", txtransferno.Text));
                    //bll.vInsertEmailSent(arr);
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Data Save successfully ..','Internal Tranfer No. " + txtransferno.Text + "','info');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Only Quantity can update, Data saved succesfully ..','Internal Tranfer No. " + txtransferno.Text + "','info');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "alert('Only Quantity can update, Data saved succesfully ...');", true);
            }
            bindinggrd(); btsearch.Visible = false;
            btnew.Visible = true; btsave.Visible = false; btprint.Visible = false;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_internaltransfer2");
            Response.Redirect("fm_ErrorPage.aspx");
        }

    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        try
        {

            if (txmanualno.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Attention','Please fill manual internal transfer !!','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
                return;
            }

            string sManualNo = bll.vLookUp("select dbo.fn_checkmanualno('internaltransfer','" + txmanualno.Text + "','" + Request.Cookies["sp"].Value.ToString() + "')");
            if (sManualNo != "ok")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('This manual no. " + txmanualno.Text + "  already used','Please use another !!','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
                return;
            }
            if (txtransferno.Text == "" || txtransferno.Text == "NEW")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Attention','Please Save Before Print, data can not print','error');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
                return;
            }

            if (lbstatus.Text == "COMPLETE")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Attention','Transaction has been complete, data can not print','error');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
                return;
            }
            if (lbstatus.Text == "Waiting Approval")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Attention','Transaction waiting approval, data can not print','error');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
                return;
            }
            if (lbstatus.Text == "CANCEL")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Attention','Transaction Cancel, data can not print','error');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
                return;
            }

            if (optwhs.SelectedValue.ToString() != "B")
            {
                foreach (GridViewRow row in grd.Rows)
                {
                    Label lbqty = (Label)row.FindControl("lbqty");
                    Label lbitemcode = (Label)row.FindControl("lbitemcode");
                    decimal dqty = Convert.ToDecimal(lbqty.Text);
                    if (dqty < 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Cant add Qty Minus','Item. " + lbitemcode.Text + "','warning');", true);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Stock is not enough !' " + lbitemcode.Text + " Qty Stock :" + strbookqty + " Qty Transfer : " + lbqty.Text + " ,'Stock Not Enough','warning');", true);
                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
                        return;
                    }
                }
            }
            

            foreach (GridViewRow row in grd.Rows)
            {
                Label lbqty = (Label)row.FindControl("lbqty");
                Label lbitemcode = (Label)row.FindControl("lbitemcode");
                Label lbuom = (Label)row.FindControl("lbuom");
                string strbookqty = bll.vLookUp("select dbo.sfnGetStockBookingseries('" + Request.Cookies["sp"].Value.ToString() + "','" + lbitemcode.Text.ToString() + "','" + cbbinfrom.SelectedValue.ToString() + "','" + cbfrom.SelectedValue.ToString() + "','" + txtransferno.Text + "')");
                if (Convert.ToDecimal(strbookqty) < 0) { strbookqty = "0"; }
                decimal dqty = Convert.ToDecimal(lbqty.Text);
                decimal dbookqty = Convert.ToDecimal(strbookqty);
                dqty = decimal.Parse(bll.vLookUp("select dbo.sfnUomQtyConv('" + lbitemcode.Text + "','" + lbuom.Text + "','CTN','" + lbqty.Text + "')"));
                if (dqty > dbookqty)
                {
                    string smsgstockallert = "Stock is not enough ! " + lbitemcode.Text + " Qty Stock :" + strbookqty + " Qty Transfer : " + lbqty.Text;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('" + smsgstockallert + "' ,'Stock Not Enough','warning');", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
                    return;
                }
            }
            List<cArrayList> arr = new List<cArrayList>();
            arr.Clear();
            //arr.Add(new cArrayList("@SalesPointCD", Request.Cookies["sp"].Value.ToString()));
            //arr.Add(new cArrayList("@trf_no", txtransferno.Text));
            //arr.Add(new cArrayList("@printby", Request.Cookies["usr_id"].Value.ToString()));
            //arr.Clear();
            //Session["lParamit"] = arr;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=it&no="+ txtransferno.Text + "');", true);

            List<cArrayList> arr1 = new List<cArrayList>();
            arr1.Add(new cArrayList("@SalesPointCD", Request.Cookies["sp"].Value.ToString()));
            arr1.Add(new cArrayList("@trf_no", txtransferno.Text));
            arr1.Add(new cArrayList("@sta_id", (optwhs.SelectedValue.ToString() == "B" ? "D" : "C")));
            bll.vUpdatetinternal_transfer(arr1);
            lbstatus.Text = bll.vLookUp("select fld_desc from tfield_value where fld_nm='sta_id' and fld_valu='" + (optwhs.SelectedValue.ToString() == "B" ? "D" : "C") + "'");

            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBatchStockConfirm(arr);

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "dvshow.setAttribute('class','divhid');", true);
            // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "xux", "popupreport()", true);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=it');", true);
            //Response.Redirect("fm_report.aspx?src=it");
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_internaltransfer2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void bttmp_Click(object sender, EventArgs e)
    {
        try
        {

            SqlDataReader rs = null;
            txtransferno.Text = Convert.ToString(Session["lootrf_no"]);
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@trf_no", txtransferno.Text));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vGettinternal_transfer(arr, ref rs);
            while (rs.Read())
            {
                //string zz;
                //zz = rs["whs_cd_from"].ToString();
                string prevstk = rs["prevstk"].ToString();
                if (prevstk == "") { prevstk = "false"; }
                chprevstk.Checked = Boolean.Parse(prevstk);
                dttrf.Text = string.Format("{0:d/M/yyyy}", rs["trf_dt"]);
                txmanualno.Text = rs["manual_no"].ToString();
                optwhs.Text = rs["trf_typ"].ToString();
                optwhs_SelectedIndexChanged(sender, e);
                cbfrom.SelectedValue = rs["whs_cd_from"].ToString();
                cbbinfrom.SelectedValue = rs["bin_from"].ToString();
                cbto.SelectedValue = rs["whs_cd_to"].ToString();
                cbbinto.SelectedValue = rs["bin_to"].ToString();
                txtabno.Text = rs["tab_no"].ToString();
                txtabno.CssClass = "form-control ro";
                txtabno.Enabled = false;
                dtloading.Text = string.Format("{0:d/M/yyyy}", rs["loading_dt"]);
                bool b = Convert.ToBoolean(Convert.ToInt32(rs["urgent"]));
                cburgent.Checked = b;
                string ssta_id = rs["sta_id"].ToString();
                lbstatus.Text = bll.vLookUp("select fld_desc from tfield_value where fld_nm='sta_id' and fld_valu='" + ssta_id + "'");
                if (cbbinto.SelectedValue == "BS") { cbreason.SelectedValue = rs["reason"].ToString(); }
                //   txtabno.Text = rs["tab_no"].ToString();
                if (ssta_id == "C") { grd.Columns[8].Visible = false; grd.Columns[9].Visible = false; btprint.Visible = false; btsave.Visible = false; btsearch.Visible = false; }
                else if (ssta_id == "N") { btsave.Visible = true; btprint.Visible = false; btsearch.Visible = false; }
                else if (ssta_id == "W") { btsave.Visible = false; btprint.Visible = false; btsearch.Visible = false; grd.Columns[8].Visible = false; grd.Columns[9].Visible = false; }
                else if (ssta_id == "A") { btsave.Visible = false; btprint.Visible = true; btsearch.Visible = false; grd.Columns[8].Visible = false; grd.Columns[9].Visible = false; }
            }
            rs.Close();
            disableheader();
            bindinggrd();
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_internaltransfer2");
            Response.Redirect("fm_ErrorPage.aspx");
        }

    }
    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {

            if (Request.Cookies["waz_dt"].Value.ToString() != dttrf.Text)
            {

                grd.EditIndex = -1;
                return;
            }

            Label lbseqID = (Label)grd.Rows[e.RowIndex].FindControl("lbseqID");
            Label lbtrf_no = (Label)grd.Rows[e.RowIndex].FindControl("lbtrf_no");
            TextBox txtqty = (TextBox)grd.Rows[e.RowIndex].FindControl("txtqty");
            Label lbstock_qty = (Label)grd.Rows[e.RowIndex].FindControl("lbstock_qty");
            Label lbitemcode = (Label)grd.Rows[e.RowIndex].FindControl("lbitemcode");
            DropDownList cbuom = (DropDownList)grd.Rows[e.RowIndex].FindControl("cbuom");
            DateTime ddate = DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            string sStock = "0";
            if (chprevstk.Checked == true)
            {
                sStock = bll.vLookUp("select dbo.[sfnGetStock]('" + Request.Cookies["sp"].Value.ToString() + "','" + hditem.Value.ToString() + "','" + cbbinfrom.SelectedValue.ToString() + "','" + cbfrom.SelectedValue.ToString() + "','0','" + ddate + "')");//by yanto 25-6-2016 
            }
            else
            {
                if (txtransferno.Text == "" || txtransferno.Text == "NEW")
                {
                    sStock = bll.vLookUp("select dbo.[sfnGetStockBooking]('" + Request.Cookies["sp"].Value.ToString() + "','" + lbitemcode.Text.ToString() + "','" + cbbinfrom.SelectedValue.ToString() + "','" + cbfrom.SelectedValue.ToString() + "','0','" + ddate + "')");//by yanto 25-6-2016 
                }
                else
                {
                    { sStock = bll.vLookUp("select dbo.sfnGetStockBookingseries('" + Request.Cookies["sp"].Value.ToString() + "','" + lbitemcode.Text.ToString() + "','" + cbbinfrom.SelectedValue.ToString() + "','" + cbfrom.SelectedValue.ToString() + "','" + txtransferno.Text + "')"); }
                }
            }

            if (sStock == null || sStock == "") { sStock = "0"; }

            if (Convert.ToDouble(txtqty.Text) > Convert.ToDouble(sStock))
            {
                txtqty.Text = sStock;
            }
            else if (Convert.ToDouble(txtqty.Text) < 0)
            {
                txtqty.Text = "0";
            }

            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@seqID", lbseqID.Text));
            arr.Add(new cArrayList("@qty", txtqty.Text));
            arr.Add(new cArrayList("@stock_qty", sStock));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@uom", cbuom.SelectedValue.ToString()));
            bll.vUpdatetinternal_transfer_dtl(arr);
            grd.EditIndex = -1;
            bindinggrd();
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_internaltransfer2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            if (lbstatus.Text == "COMPLETE")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction has been complete, data can not edit','error');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
                grd.EditIndex = -1;
                return;
            }
            //if (Request.Cookies["waz_dt"].Value.ToString() != dttrf.Text)
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction Date Out Of Periode Date','error');", true);
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
            //    grd.EditIndex = -1;
            //    return;
            //}
            grd.EditIndex = e.NewEditIndex;
            bindinggrd();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "editTable", "editTable();", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_internaltransfer2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grd.EditIndex = -1;
        bindinggrd();
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_internaltransfer2.aspx");
    }
    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {

            if (lbstatus.Text == "COMPLETE")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction has been complete, data can not delete','error');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
                grd.EditIndex = -1;
                return;
            }
            //if (Request.Cookies["waz_dt"].Value.ToString() != dttrf.Text)
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction Date Out Of Periode Date','error');", true);
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
            //    grd.EditIndex = -1;
            //    return;
            //}
            Label lbseqID = (Label)grd.Rows[e.RowIndex].FindControl("lbseqID");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@seqID", lbseqID.Text));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vDeletetinternal_transfer_dtl(arr);
            bindinggrd();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Data Deleted successfully !','error');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_internaltransfer2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btsearch2_Click(object sender, EventArgs e)
    {

    }
    protected void btsearch2_Click1(object sender, EventArgs e)
    {

    }
    //protected void txitem_TextChanged(object sender, EventArgs e)
    //{
    //    DateTime dt = DateTime.ParseExact(dttrf.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
    //    string ssitedest;
    //    if (optwhs.SelectedValue == "V") { ssitedest = "VS"; } else { ssitedest = "DEPO"; }

    //    txstk.Text = bll.vLookUp("select [dbo].[sfnGetStock]('" + hditem.Value + "','" + cbbinfrom.SelectedValue + "','" + cbfrom.SelectedValue + "','" + ssitedest + "','" + dt.ToShortDateString() + "')"); 
    //}
    protected void txqty_TextChanged(object sender, EventArgs e)
    {
        try
        {
            double dQtyCheck = 0;

            if (!double.TryParse(txqty.Text, out dQtyCheck))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Qty must be numeric','Check Qty','warning');", true);
                return;
            }

            if (Convert.ToDouble(txqty.Text) > Convert.ToDouble(txstk.Text))
            {
                //txqty.Text = txstk.Text;
            }
            else if (Convert.ToDouble(txqty.Text) < 0)
            {
                txqty.Text = "0";
            }
            //btsearch.Focus();

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_internaltransfer2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbqty = (Label)e.Row.FindControl("lbqty");
                Label lbstock_qty = (Label)e.Row.FindControl("lbstock_qty");
                Label lbuom = (Label)e.Row.FindControl("lbuom");
                Label lbitemcode = (Label)e.Row.FindControl("lbitemcode");
                //TextBox txqty = (TextBox)e.Row.FindControl("txqty");
                decimal qty;
                if (lbqty != null)
                    qty = decimal.Parse(bll.vLookUp("select dbo.sfnUomQtyConv('" + lbitemcode.Text + "','" + lbuom.Text + "','CTN','" + lbqty.Text + "')"));
                //if (txqty != null)
                //    qty = decimal.Parse(txqty.Text);
                else
                    qty = 0;

                totalQty = totalQty + qty;
                totalStockQty = totalStockQty + decimal.Parse(lbstock_qty.Text);
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalqty = (Label)e.Row.FindControl("lblTotalqty");
                Label lblTotalstockqty = (Label)e.Row.FindControl("lblTotalstockqty");
                lblTotalqty.Text = totalQty.ToString();
                lblTotalstockqty.Text = totalStockQty.ToString();
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_internaltransfer2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btrefresh_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime dt = DateTime.ParseExact(dttrf.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //string ssitedest;
            if ((optwhs.SelectedValue == "") || (optwhs.SelectedValue == null))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Transfer Stock type has not yet selected','Type Transfer','warning');", true);
                return;
            }

            //if (optwhs.SelectedValue == "V")
            ////{ ssitedest = "VS"; } else { ssitedest = "DEPO";
            //{
            //    //txstk.Text = bll.vLookUp("select dbo.sp_getcurrentvanstock('" + cbfrom.SelectedValue.ToString() + "','" + cbbinfrom.SelectedValue.ToString() + "','" + hditem.Value.ToString() + "')").ToString();
            //    txstk.Text = bll.vLookUp("select dbo.sfnGetStockBooking('"+ Request.Cookies["sp"].Value.ToString() + "','" + hditem.Value.ToString() + "','" + cbbinfrom.SelectedValue.ToString() + "','" + cbfrom.SelectedValue.ToString() + "','0','" + System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture) + "')");
            //}

            //if ((optwhs.SelectedValue.ToString() == "I") || (optwhs.SelectedValue == "O"))
            //{
            //    //  txstk.Text = bll.vLookUp("select dbo.fn_checkcurrentstock('"+hditem.Value.ToString()+"','"+cbfrom.SelectedValue.ToString()+"','"+cbbinfrom.SelectedValue.ToString()+"','stockamt')");
            //    txstk.Text = bll.vLookUp("select dbo.sfnGetStockBooking('"+ Request.Cookies["sp"].Value.ToString() + "','" + hditem.Value.ToString() + "','" + cbbinfrom.SelectedValue.ToString() + "','" + cbfrom.SelectedValue.ToString() + "','0','" + System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture) + "')");

            //}
            DateTime ddate = DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            string sStock = "0";
            if (chprevstk.Checked == true)
            {
                sStock = bll.vLookUp("select dbo.[sfnGetStock]('" + Request.Cookies["sp"].Value.ToString() + "','" + hditem.Value.ToString() + "','" + cbbinfrom.SelectedValue.ToString() + "','" + cbfrom.SelectedValue.ToString() + "','0','" + ddate.Year + "-" + ddate.Month + "-" + ddate.Day + "')");//by yanto 25-6-2016 
            }
            else
            {
                if (txtransferno.Text == "" || txtransferno.Text == "NEW")
                {
                    sStock = bll.vLookUp("select dbo.[sfnGetStockBooking]('" + Request.Cookies["sp"].Value.ToString() + "','" + hditem.Value.ToString() + "','" + cbbinfrom.SelectedValue.ToString() + "','" + cbfrom.SelectedValue.ToString() + "','0','" + ddate.Year + "-" + ddate.Month + "-" + ddate.Day + "')");//by yanto 25-6-2016 
                }
                else
                {
                    { sStock = bll.vLookUp("select dbo.sfnGetStockBookingseries('" + Request.Cookies["sp"].Value.ToString() + "','" + hditem.Value + "','" + cbbinfrom.SelectedValue.ToString() + "','" + cbfrom.SelectedValue.ToString() + "','" + txtransferno.Text + "')"); }
                }
            }

            if (sStock == null || sStock == "") { sStock = "0"; }
            txstk.Text = sStock;
            txqty.Text = sStock;
            //if (optwhs.SelectedValue == "I")
            //{
            //    txstk.Text = bll.vLookUp("select stock_display from tmst_stock where item_cd='" + hditem.Value + "' and whs_cd='"+cbfrom.SelectedValue +"' and bin_cd='"+ cbbinfrom.SelectedValue + "'"); 
            //}
            //else
            //{
            //  txstk.Text = bll.vLookUp("select [dbo].[sfnGetStock]('" + hditem.Value + "','" + cbbinfrom.SelectedValue + "','" + cbfrom.SelectedValue + "','" + ssitedest + "','" + dt.ToShortDateString() + "')"); 
            // Change Source Stock to mst stock 1 Jun 2016 by IA


            //}

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_internaltransfer2");
            Response.Redirect("fm_ErrorPage.aspx");
        }

    }
    protected void txitem_TextChanged(object sender, EventArgs e)
    {
        try
        {

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
            disableheader();
            //txqty.Focus();
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_internaltransfer2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
   private void disableheader()
    {
        cbfrom.CssClass = "from-control input-sm ro";
        cbfrom.Enabled = false;
        cbbinfrom.CssClass = "from-control input-sm ro";
        cbbinfrom.Enabled = false;
        cbto.CssClass = "from-control input-sm ro";
        cbto.Enabled = false;
        cbbinto.CssClass = "from-control input-sm ro";
        cbbinto.Enabled = false;
        dttrf.CssClass = "from-control  ro";
        dttrf.Enabled = false;
        //btsave.Visible = false;
    }
    protected void bttabsearch_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "window.open('fm_tabinternaltransfer.aspx','mywindow','toolbar=n,scrollbars=y,width=800,height=800,top=75,left=300',true);", true);
    }
    
    protected void chprevstk_CheckedChanged(object sender, EventArgs e)
    {
        try
        {

            if (optwhs.SelectedValue == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please Choose  Transfer type!','Prev stk can not changed ','error');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
                if (chprevstk.Checked == true) { chprevstk.Checked = false; } else { chprevstk.Checked = true; }
                return;
            }
            DateTime ddate = DateTime.ParseExact(dttrf.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            var dateAsString = ddate.ToString("yyyy-MM-dd");
            string swhs_cd = bll.vLookUp("select whs_cd from tstockopname_schedule where (whs_cd='" + cbfrom.SelectedValue + "' or whs_cd='" + cbto.SelectedValue + "') and schedule_dt='" + dateAsString + "'");
            if (swhs_cd == cbfrom.SelectedValue || swhs_cd == cbto.SelectedValue)
            { }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please entri schedule jaret !','Prev stk can not changed ','error');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
                if (chprevstk.Checked == true) { chprevstk.Checked = false; } else { chprevstk.Checked = true; }
                return;
            }

            if (grd.Rows.Count != 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Item already inserted !','Prev stk can not changed ','error');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
                if (chprevstk.Checked == true) { chprevstk.Checked = false; } else { chprevstk.Checked = true; }
                return;
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_internaltransfer2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void cbbinto_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            cbbinto.CssClass = "ro";
            cbbinto.Enabled = false;
            if (cbbinto.SelectedValue == "BS")
            {
                lbreason.Visible = true;
                cbreason.Visible = true;
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@reasn_typ", "return"));
                bll.vBindingComboToSp(ref cbreason, "sp_tmst_reason_get", "reasn_cd", "reasn_nm", arr);

            }
            else
            {
                lbreason.Visible = false;
                cbreason.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_internaltransfer2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }



    protected void cbreason_SelectedIndexChanged(object sender, EventArgs e)
    {
        cbreason.CssClass = "ro";
        cbreason.Enabled = false;
    }



    protected void cbuom_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}