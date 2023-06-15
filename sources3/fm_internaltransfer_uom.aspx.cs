using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
//using Org.BouncyCastle.Crypto.Engines;
using Microsoft.Data.Edm.Expressions;

public partial class fm_internaltransfer_uom : System.Web.UI.Page
{
    cbll bll = new cbll();
    cbll2 bll2 = new cbll2();
    creport rep = new creport();
    decimal totalQty = 0;
    decimal totalQtyCtn = 0, totalQtyPcs = 0;
    decimal totalStockQtyCtn = 0, totalStockQtyPcs = 0;
    decimal totalStockQty = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                //bll.vBindingFieldValueToComboByQryWithEmptyChoosen(ref cbuom, "uom", "uom_transfer");
                //bll.vBindingFieldValueToComboByQryWithEmptyChoosen(ref cbuom2, "uom", "uom_transfer");
                //cbuom.SelectedValue = "CTN";
                //cbuom2.SelectedValue = "PCS";
                //cbuom.CssClass = "ro";
                //cbuom2.CssClass = "ro";

                // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
                lbstatus.Text = bll.vLookUp("select fld_desc from tfield_value where fld_nm='sta_id' and fld_valu='N'");
                dttrf.Text = Request.Cookies["waz_dt"].Value.ToString();
                DateTime dateloading = DateTime.ParseExact(dttrf.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                dateloading = dateloading.AddDays(0);
                string strdateloading = string.Format("{0:d/M/yyyy}", dateloading);
                //dateloading = DateTime.ParseExact(strdateloading, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                //strdateloading = dateloading.ToString("d");
                dtloading.Text = strdateloading;
                //List<cArrayList> arr = new List<cArrayList>();
                //arr.Add(new cArrayList("@trf_no", Request.Cookies["usr_id"].Value.ToString()));
                //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //bll.vDelInternalTransferDtl(arr);
                ////bll.vDelWrkItemQty(arr);
                //bindinggrd();
                Session["tinternal_transfer_dtl"] = new List<tinternal_transfer_dtl>();
                txtransferno.Text = "NEW";
                txstk.Text = "0";
                //if not transfer BS no need reason
                //cd.v_disablecontrol(cbuom);
                //cd.v_disablecontrol(cbuom2);
                //lbreason.Visible = false;
                //cbreason.Visible = false;
                cd.v_hiddencontrol(lbreason);
                cd.v_hiddencontrol(cbreason);
                txtransferno.Text = String.Empty;
                txtabno.Text = String.Empty;
                //  bll.sFormat2ddmmyyyy(ref dttrf);
                cd.v_showcontrol(btnew);
                cd.v_hiddencontrol(btprint);
                cd.v_hiddencontrol(btsearch);
                cd.v_hiddencontrol(btsave_i);
                cd.v_hiddencontrol(btsave_b);
                cd.v_hiddencontrol(btsave_o);
                cd.v_hiddencontrol(btsave_v);
                cd.v_disablecontrol(txitem);
                cd.v_disablecontrol(txqty);
                cd.v_disablecontrol(txqty2);
                cd.v_disablecontrol(txstk);
                cd.v_disablecontrol(txtabno);
                cd.v_disablecontrol(txmanualno);
                cd.v_disablecontrol(txremark);
            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_internaltransfer_uom");
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
            sItem = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["item_cd"].ToString() + " - " + rs["item_shortname"].ToString() + " - " + rs["item_nm"].ToString(), rs["item_cd"].ToString());
            lItem.Add(sItem);
        }
        rs.Close();
        return (lItem.ToArray());

    }

    protected void btsearch_Click(object sender, EventArgs e) //This actually add button 
    {
        try
        {
            if (Session["tinternal_transfer_dtl"] == null)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();sweetAlert('Your session on this screen has expired!','Please re-open internal transfer','warning');", true);
                return;
            }
            double _qty = 0;
            if (cbfrom.SelectedValue == String.Empty || cbbinfrom.SelectedValue == String.Empty || cbto.SelectedValue == String.Empty || cbbinto.SelectedValue == String.Empty)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();sweetAlert('Warehouse and bin must be selected!','Warehouse and Bin','warning');", true);
                return;
            }

            if (hditem.Value == String.Empty)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();sweetAlert('Item not Yet Selected','Check Item','warning');", true);
                return;
            }

            if (!double.TryParse(txqty.Text, out _qty))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();sweetAlert('Qty must be numeric','Check Qty','warning');", true);
                return;
            }
            double _qtypcs = 0;
            if (!double.TryParse(txqty2.Text, out _qtypcs))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();sweetAlert('Qty on PCS must be numeric !','Check Qty PCS','warning');", true);
                return;
            }

            if (Convert.ToDouble(txqty.Text) < 0 || Convert.ToDouble(txqty2.Text) < 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();sweetAlert('Qty must bigger then zero','Check Qty','warning');", true);
                return;
            }

            if ((cbfrom.SelectedValue.ToString() == cbto.SelectedValue.ToString()) && (cbbinfrom.SelectedValue.ToString() == cbbinto.SelectedValue.ToString()))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();sweetAlert('Can not same destination and from','Check Destination','warning');", true);
                return;
            }

            double _stock = 0;
            if (!double.TryParse(txstk.Text, out _stock))
            {

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();sweetAlert('Check stock !','There no value for stock','warning');", true);
                return;
            }
            if (_qty > _stock)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();sweetAlert('Qty order can not bigger than stock!','Check stock','warning');", true);
                return;

            }
            //if ((cbbinto.SelectedValue.ToString() == "NES") && (cbbinfrom.SelectedValue.ToString() != "NE"))
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Can not transfer to NE Special if not from NE Stock','Check Destination','warning');", true);
            //    return;
            //}
          
            double _maxqtypcs = Convert.ToDouble( bll.vLookUp("select qty from titem_conversion where uom_from='CTN' and uom_to='PCS' and " +
                "item_cd='"+hditem.Value+"'"));

           
            if (_qtypcs >= _maxqtypcs)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();sweetAlert('Qty on PCS can not exceed or equel with " + _maxqtypcs.ToString("N2") + "!','Please order on CTN','warning');", true);
                return;
            }

            if (_qty == 0 && _qtypcs==0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();sweetAlert('Qty can not zero !','Please check qty','warning');", true);
                return;
            }
            DateTime ddate = DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            double dStock = 0, dQty = 0, dQty2 = 0, dQtyTotal = 0;
            string sStock = String.Empty, sStock_conv = String.Empty;

            if (txtransferno.Text == String.Empty || txtransferno.Text == "NEW")
            {
                sStock = bll.vLookUp("select isnull(dbo.[sfnGetStockBooking]('" + Request.Cookies["sp"].Value.ToString() + "','" + hditem.Value.ToString() + "','" + cbbinfrom.SelectedValue.ToString() + "','" + cbfrom.SelectedValue.ToString() + "','0','" + ddate.Year + "-" + ddate.Month + "-" + ddate.Day + "'),0)");
            }
            else
            {
                { sStock = bll.vLookUp("select isnull(dbo.sfnGetStockBookingseries('" + Request.Cookies["sp"].Value.ToString() + "','" + hditem.Value + "','" + cbbinfrom.SelectedValue.ToString() + "','" + cbfrom.SelectedValue.ToString() + "','" + txtransferno.Text + "'),0) "); }
            }



            dStock = double.Parse(sStock);
            //dQty = double.Parse(txqty.Text);
            ////dQty = _qty;
            //dQty2 = double.Parse(txqty2.Text);
            //dQtyTotal = dQty + (double.Parse(bll.vLookUp("select dbo.sfnUomQtyConv('" + hditem.Value.ToString() + "','" + cbuom2.SelectedValue.ToString() + "','CTN','" + txqty2.Text + "')")));
            //sStock_conv = bll.vLookUp("select dbo.fn_getqtyconv('" + hditem.Value.ToString() + "','CTN'," + dStock.ToString() + ")");

            //if (optwhs.SelectedValue != "B") // B to B
            //{
            //    dQty = double.Parse(bll.vLookUp("select dbo.sfnUomQtyConv('" + hditem.Value.ToString() + "','" + cbuom.SelectedValue.ToString() + "','CTN','" + txqty.Text + "')"));
            //    if (dStock < dQtyTotal)
            //    {
            //        string smsgstockallert = "Stock is not enough ! " + hditem.Value.ToString() + " Qty Stock :" + sStock_conv + " ";
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('" + smsgstockallert + "' ,'Stock Not Enough','warning');", true);
            //        // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
            //        return;
            //    }
            //}
            string _itemshortname = bll.vLookUp("select item_shortname from tmst_item where item_cd='" + hditem.Value + "'");
            string _branded_nm = bll.vLookUp("select branded_nm from tmst_item where item_cd='" + hditem.Value + "'");
            string _size = bll.vLookUp("select size from tmst_item where item_cd='" + hditem.Value + "'");
            List<tinternal_transfer_dtl> _tinternal_transfer_dtl = (List<tinternal_transfer_dtl>)Session["tinternal_transfer_dtl"];
            _tinternal_transfer_dtl.Add(new tinternal_transfer_dtl
            {
                trf_no = String.Empty,
                salespointcd = Request.Cookies["sp"].Value,
                item_cd = hditem.Value,
                qty = _qty,
                qty2 = _qtypcs,
                uom =  ((_qtypcs == 0)?"CTN":"PCS"),
                uom2 = "PCS",
                stock_qty = dStock,
                branded_nm = _branded_nm,
                item_shortname = _itemshortname,
                size = _size,
                qty_ctn = _qty,
                qty_pcs =_qtypcs,
                seqID = Guid.NewGuid().ToString(),
                qty_conv =  ((_qtypcs==0)?_qty:_qtypcs + Convert.ToDouble( (bll.vLookUp("select dbo.fn_itemconversion('CTN','PCS','"+hditem.Value+"',"+_qty+")")))),
                stockqty_conv = Convert.ToDouble(txstk.Text),
                item_nm = bll.vLookUp("select item_nm from tmst_item where item_cd='" + hditem.Value + "'")

            });

            grd.DataSource = _tinternal_transfer_dtl;
            grd.DataBind();
            Session["tinternal_transfer_dtl"] = _tinternal_transfer_dtl;
            //string trf_no = String.Empty;
            //if (txtransferno.Text == "" || txtransferno.Text == "NEW") { trf_no = Request.Cookies["usr_id"].Value.ToString(); } else { trf_no = txtransferno.Text; }
            //List<cArrayList> arr = new List<cArrayList>();
            //arr.Add(new cArrayList("@trf_no", trf_no));
            //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            //arr.Add(new cArrayList("@item_cd", hditem.Value.ToString()));
            //arr.Add(new cArrayList("@qty", txqty.Text));
            //arr.Add(new cArrayList("@qty2", txqty2.Text));
            //arr.Add(new cArrayList("@uom", cbuom.SelectedValue.ToString()));
            //arr.Add(new cArrayList("@uom2", cbuom2.SelectedValue.ToString()));
            //arr.Add(new cArrayList("@stock_qty", dStock.ToString()));
            //bll.vInserttinternal_transfer_dtl(arr);
            Label lbtotqtyctn = (Label)grd.FooterRow.FindControl("lbtotqtyctn");
            Label lbtotqtypcs = (Label)grd.FooterRow.FindControl("lbtotqtypcs");
            lbtotqtyctn.Text = bll2.fn_gettotalfooter(ref grd, "lbqtyctn").ToString("N2");
            lbtotqtypcs.Text = bll2.fn_gettotalfooter(ref grd, "lbqtypcs").ToString("N2");
            //bindinggrd();
            txstk.Text = "0";
            hditem.Value = String.Empty;
            txitem.Text = String.Empty;
            txqty.Text = String.Empty;
            txstk.Text = String.Empty;
            txqty2.Text = "0";
            txqty.Text = "0";
            cd.v_showcontrol(btsave_i);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_internaltransfer_uom");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        finally
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
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
            bll.vBindingGridToSp(ref grd, "sp_tinternal_transfer_dtl_get2", arr);
            txitem.Text = "";
            hditem.Value = "";
            txqty.Text = "0";
            txqty2.Text = "0";
            //txitem.Focus();
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_internaltransfer_uom");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void optwhs_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            List<tinternal_transfer_dtl> _tinternal_transfer_dtl = (List<tinternal_transfer_dtl>)Session["tinternal_transfer_dtl"];
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
            if (optwhs.SelectedValue.ToString() == "I")
            {
                //txqty.Attributes.Add("type", "number");
                lbtrtx1.Text = "Warehouse";
                lbtrtx2.Text = "Van";
                bll.vBindingComboToSpWithEmptyChoosen(ref cbto, "sp_tmst_vehicle_salesman_get", "vhc_cd", "vhc_desc", arr);
                bll.vBindingComboToSpWithEmptyChoosen(ref cbfrom, "sp_tmst_warehouse_get", "whs_cd", "whs_nm", arr);
                cd.v_showcontrol(lbloadingdt);
                cd.v_showcontrol(dtloading);
                cd.v_showcontrol(cburgent);
            }
            else if (optwhs.SelectedValue.ToString() == "O")
            {
                txqty.Attributes.Remove("type");
                lbtrtx1.Text = "Warehouse";
                lbtrtx2.Text = "Warehouse";
                bll.vBindingComboToSpWithEmptyChoosen(ref cbto, "sp_tmst_warehouse_get", "whs_cd", "whs_nm", arr);
                bll.vBindingComboToSpWithEmptyChoosen(ref cbfrom, "sp_tmst_warehouse_get", "whs_cd", "whs_nm", arr);
                lbloadingdt.Visible = false;

                dtloading.Visible = false;
                cburgent.Visible = false;
            }
            else if (optwhs.SelectedValue.ToString() == "V")
            {
                txqty.Attributes.Remove("type");
                lbtrtx1.Text = "Van";
                lbtrtx2.Text = "Warehouse";
                bll.vBindingComboToSpWithEmptyChoosen(ref cbfrom, "sp_tmst_vehicle_salesman_get", "vhc_cd", "vhc_desc", arr);
                // bll.vBindingComboToSp(ref cbto, "sp_tmst_warehouse_get","whs_cd","whs_nm",arr);
                bll.vBindingComboToSpWithEmptyChoosen(ref cbto, "sp_tmst_warehouse_get", "whs_cd", "whs_nm", arr);
                lbloadingdt.Visible = false;

                dtloading.Visible = false;
                cburgent.Visible = false;
            }
            else if (optwhs.SelectedValue == "B") //Branch To Brance
            {
                arr.Clear();
                lbtrtx1.Text = "Warehouse";
                lbtrtx2.Text = "Warehouse";
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
                bll.vBindingComboToSpWithEmptyChoosen(ref cbfrom, "sp_tmst_warehouse_getbysalespoint", "whs_cd", "whs_nm", arr);
                bll.vBindingComboToSpWithEmptyChoosen(ref cbto, "sp_tmst_warehouse_all_get", "whs_cd", "whs_desc");
                lbloadingdt.Visible = true;
                dtloading.Visible = true;
                cburgent.Visible = true;
                cd.v_enablecontrol(cbfrom);
                cd.v_enablecontrol(cbto);
                cd.v_enablecontrol(cbto);
                cd.v_enablecontrol(cbfrom);
            }
            else
            {
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), 
                //    "sweetAlert('Please select internal transfer type !','Internal Transfer Type','warning');", true);
                //return;
            }
            optwhs.Enabled = false;
            cd.v_disablecontrol(optwhs);
            if (lbstatus.Text == "NEW")
            { cd.v_showcontrol(btsave_i); cd.v_hiddencontrol(btprint); cd.v_showcontrol(btsearch); }
            cd.v_disablecontrol(btsearch2);
            cd.v_disablecontrol(bttabsearch);
            cd.v_enablecontrol(txremark);
            cd.v_enablecontrol(txmanualno);

            Session["tinternal_transfer_dtl"] = _tinternal_transfer_dtl;
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_internaltransfer_uom");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        finally
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
        }
    }
    protected void cbfrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            List<tinternal_transfer_dtl> _tinternal_transfer_dtl = (List<tinternal_transfer_dtl>)Session["tinternal_transfer_dtl"];
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            List<cArrayList> arr = new List<cArrayList>();
            if (optwhs.SelectedValue.ToString() == "I")
            {
                arr.Clear();
                arr.Add(new cArrayList("@whs_cd", cbfrom.SelectedValue.ToString()));
                arr.Add(new cArrayList("@qry_cd", "showbininternalfrom")); ;
                bll.vBindingComboToSpWithEmptyChoosen(ref cbbinfrom, "sp_twarehouse_bin_getbytype", "bin_cd", "bin_nm", arr);

            }
            if (optwhs.SelectedValue.ToString() == "O")
            {
                arr.Clear();
                arr.Add(new cArrayList("@whs_cd", cbfrom.SelectedValue.ToString()));
                arr.Add(new cArrayList("@qry_cd", "showbininternalfrom"));
                bll.vBindingComboToSpWithEmptyChoosen(ref cbbinfrom, "sp_twarehouse_bin_getbytype", "bin_cd", "bin_nm", arr);

            }
            else if (optwhs.SelectedValue.ToString() == "V")
            {
                string trf_no = "";
                arr.Clear();
                arr.Add(new cArrayList("@vhc_cd", cbfrom.SelectedValue.ToString()));
                bll.vBindingComboToSpWithEmptyChoosen(ref cbbinfrom, "sp_tvan_bin_get", "bin_cd", "bin_nm", arr);
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
            else if (optwhs.SelectedValue == "B")
            {
                arr.Clear();
                //cbfrom.SelectedValue = bll.vLookUp("select whs_cd from tmst_warehouse where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                arr.Add(new cArrayList("@whs_cd", cbfrom.SelectedValue.ToString()));
                arr.Add(new cArrayList("@qry_cd", "showbininternalfrom"));
                bll.vBindingComboToSpWithEmptyChoosen(ref cbbinfrom, "sp_twarehouse_bin_getbytype", "bin_cd", "bin_nm", arr);
                //cbfrom.CssClass = "ro";
                //cbbinfrom.CssClass = "ro";
            }
            else
            {
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), 
                //    "sweetAlert('Please select transfer type!','Transfer Type','warning');", true);
                //return;
            }
            //try { cbbinfrom.SelectedValue = "BINGS"; cbbinto.SelectedValue = "BINGS"; }
            //catch { cbbinfrom.SelectedValue = "GS"; cbbinto.SelectedValue = "GS"; }
            cd.v_enablecontrol(txitem);
            cd.v_enablecontrol(txqty);
            //cd.v_enablecontrol(cbuom);
            cd.v_enablecontrol(txqty2);

            //cd.v_enablecontrol(txqty2);
            //cd.v_enablecontrol(cbuom2);
            txqty2.Text = "0";
            //cbuom2.SelectedValue = "PCS";
            Session["tinternal_transfer_dtl"] = _tinternal_transfer_dtl;
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_internaltransfer_uom");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void cbto_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            List<tinternal_transfer_dtl> _tinternal_transfer_dtl = (List<tinternal_transfer_dtl>)Session["tinternal_transfer_dtl"];
            List<cArrayList> arr = new List<cArrayList>();
            if (optwhs.SelectedValue.ToString() == "I")
            {

                arr.Add(new cArrayList("@vhc_cd", cbto.SelectedValue.ToString()));
                bll.vBindingComboToSpWithEmptyChoosen(ref cbbinto, "sp_tvan_bin_get", "bin_cd", "bin_nm", arr);

                // bll.vBindingComboToSp(ref cbbinto, "sp_twarehouse_bin_get", "bin_cd", "bin_nm", arr);
            }
            else if (optwhs.SelectedValue.ToString() == "O")
            {
                arr.Add(new cArrayList("@whs_cd", cbto.SelectedValue.ToString()));
                bll.vBindingComboToSpWithEmptyChoosen(ref cbbinto, "sp_twarehouse_bin_get", "bin_cd", "bin_nm", arr);
            }
            else if (optwhs.SelectedValue.ToString() == "V")
            {
                arr.Add(new cArrayList("@whs_cd", cbto.SelectedValue.ToString()));
                bll.vBindingComboToSpWithEmptyChoosen(ref cbbinto, "sp_twarehouse_bin_get", "bin_cd", "bin_nm", arr);
            }
            else
            {
                arr.Add(new cArrayList("@whs_cd", cbto.SelectedValue.ToString()));
                bll.vBindingComboToSpWithEmptyChoosen(ref cbbinto, "sp_twarehouse_bin_get", "bin_cd", "bin_nm", arr);
            }
            string sDefBin = bll.sGetControlParameter("defaultbin");
            Session["tinternal_transfer_dtl"] = _tinternal_transfer_dtl;
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_internaltransfer_uom");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        finally
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
        }

    }
    protected void btsave_i_Click(object sender, EventArgs e)
    {
        //try
        //{

        if (optwhs.SelectedValue.ToString() == "I" && (txtransferno.Text == "" || txtransferno.Text == "NEW"))
        {
            DateTime dateloading = DateTime.ParseExact(dttrf.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            string suser = Request.Cookies["usr_id"].Value;
            string sdate = dateloading.Day.ToString();
            string sMonth = dateloading.Month.ToString();
            string syear = dateloading.Year.ToString();
            string dttrfdate = syear + "-" + sMonth + "-" + sdate;
            string sCheckloadingpriority = bll.vLookUp("select dbo.fn_checkITItemPriority('" + cbto.SelectedValue + "','" + dttrfdate + "','" + suser + "')");
            if (sCheckloadingpriority != "ok")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();sweetAlert('There are item Priority should be loading ','" + sCheckloadingpriority + "','warning');", true);
                return;
            }
        }
        string sCheckpendinglastmonth = bll.vLookUp("select dbo.fn_checkpendinglastmonth('" + Request.Cookies["sp"].Value.ToString() + "')");
        if (sCheckpendinglastmonth != "ok")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();sweetAlert('There pending transaction','" + sCheckpendinglastmonth + " need approval or must be printed!','warning');", true);
            return;
        }
        if (txmanualno.Text == String.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();sweetAlert('Attention','Please fill manual !!','warning');", true);
            return;
        }
        string sManualNo = bll.vLookUp("select dbo.fn_checkmanualno('internaltransfer','" + txmanualno.Text + "','" + Request.Cookies["sp"].Value.ToString() + "')"); //bll.vLookUp("select dbo.fn_getmanualno('" + txmanualinv.Text + "','" + txorderno.Text + "')");
        if (sManualNo != "ok")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();sweetAlert('This manual no. " + txmanualno.Text + "  already used','Please use another !!','warning');", true);
            return;
        }

        if (grd.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();sweetAlert('Please select Item for transfer','Item Empty','warning');", true);
            return;
        }

        if (optwhs.SelectedValue == String.Empty || optwhs.SelectedValue.Equals(null))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();sweetAlert('please select Source and Destination for item transfer !','Source Destination','warning');", true);
            return;
        }

        if (txmanualno.Text == String.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();sweetAlert('Manual no can not be empty !','manual no.','warning');", true);
            return;
        }
        if (grd.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();sweetAlert('Item has not yet selected !','Please select Item To Be Tranfer','warning');", true);
            return;
        }
        if (lbstatus.Text.ToUpper() == "COMPLETE")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();sweetAlert('Attention','Transaction has been complete, data can not save','error');", true);
            return;
        }

        //Boolean stockAvb = true;

        if (txremark.Text == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();sweetAlert('Remark is mandatory' ,'Please fill remark','warning');", true);
            return;
        }

        if (Session["tinternal_transfer_dtl"] == null)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();sweetAlert('Your session on screen expired!' ,'Please re-open internal transfer','warning');", true);
            return;
        }

        if (txtransferno.Text == String.Empty || txtransferno.Text == "NEW")
        {
            string _status = String.Empty;
            string _reason = String.Empty;


            _reason = cbreason.SelectedValue;

            _status = "N";
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@trf_dt", DateTime.ParseExact(dttrf.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@trf_typ", optwhs.SelectedValue));
            arr.Add(new cArrayList("@whs_cd_from", cbfrom.SelectedValue));
            arr.Add(new cArrayList("@bin_from", cbbinfrom.SelectedValue));
            arr.Add(new cArrayList("@whs_cd_to", cbto.SelectedValue));
            arr.Add(new cArrayList("@bin_to", cbbinto.SelectedValue));
            arr.Add(new cArrayList("@item_cd", hditem.Value));
            arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value));
            arr.Add(new cArrayList("@manual_no", txmanualno.Text));
            arr.Add(new cArrayList("@tab_no", txtabno.Text));
            arr.Add(new cArrayList("@prevstk", chprevstk.Checked));
            arr.Add(new cArrayList("@sta_id", _status));
            arr.Add(new cArrayList("@reason", _reason));
            arr.Add(new cArrayList("@remark", txremark.Text));
            string _trf_no = string.Empty;
            bll.vInsertInternalTransfer(arr, ref _trf_no);
            txtransferno.Text = _trf_no;

            List<tinternal_transfer_dtl> _tinternal_transfer_dtl = (List<tinternal_transfer_dtl>)Session["tinternal_transfer_dtl"];

            foreach (tinternal_transfer_dtl _tmp in _tinternal_transfer_dtl)
            {
                arr.Clear();
                arr.Add(new cArrayList("@item_cd", _tmp.item_cd));
                arr.Add(new cArrayList("@qty", (( _tmp.qty_pcs == 0)?_tmp.qty : _tmp.qty_conv)));
                arr.Add(new cArrayList("@uom", _tmp.uom));
                arr.Add(new cArrayList("@stock_qty", _tmp.stock_qty));
                arr.Add(new cArrayList("@salespointcd", _tmp.salespointcd));
                arr.Add(new cArrayList("@unitprice", _tmp.unitprice));
                arr.Add(new cArrayList("@trf_no", _trf_no));
                bll.vInsertInternalTransferDtl(arr);
            }
            // if (ssta_id == "A") { lbstatus.Text = "Approved"; } else if (ssta_id == "W") { lbstatus.Text = "Waiting Aproval"; } else if (ssta_id == "N") { lbstatus.Text = "New"; }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();sweetAlert('Internal Transfer has saved successfully!','Internal Tranfer No. " + txtransferno.Text + "','success');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();sweetAlert('Only Quantity can update, Data saved succesfully ..','Internal Tranfer No. " + txtransferno.Text + "','success');", true);
            // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "alert('Only Quantity can update, Data saved succesfully ...');", true);
        }
        bindinggrd(); //btsearch.Visible = false;
        cd.v_hiddencontrol(btsearch);
        //btnew.Visible = true; btsave_i.Visible = false; btprint.Visible = false;
        cd.v_hiddencontrol(btsave_i);
        cd.v_showcontrol(btnew);
        cd.v_showcontrol(btprint);
        // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
        //}
        //catch (Exception ex)
        //{
        //    Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
        //    bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_internaltransfer_uom");
        //    Response.Redirect("fm_ErrorPage.aspx");
        //}

    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        try
        {

            if (txmanualno.Text == String.Empty)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HidesweetAlert('Attention','Please fill manual internal transfer !!','warning');", true);
                // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
                return;
            }

            string sManualNo = bll.vLookUp("select dbo.fn_checkmanualno('internaltransfer','" + txmanualno.Text + "','" + Request.Cookies["sp"].Value.ToString() + "')");
            if (sManualNo != "ok")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();sweetAlert('This manual no. " + txmanualno.Text + "  already used','Please use another !!','warning');", true);
                //   ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
                return;
            }
            if (txtransferno.Text == "" || txtransferno.Text == "NEW")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();sweetAlert('Attention','Please Save Before Print, data can not print','error');", true);
                //  ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
                return;
            }

            if (lbstatus.Text.ToUpper() == "COMPLETE")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();sweetAlert('Attention','Transaction has been complete, data can not print','error');", true);
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
                return;
            }
            if (lbstatus.Text == "Waiting Approval")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();sweetAlert('Attention','Transaction waiting approval, data can not print','error');", true);
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
                return;
            }
            if (lbstatus.Text == "CANCEL")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();sweetAlert('Attention','Transaction Cancel, data can not print','error');", true);
                return;
            }


            if (optwhs.SelectedValue.ToString() != "B")
            {
                foreach (GridViewRow row in grd.Rows)
                {
                    HiddenField lbqty = (HiddenField)row.FindControl("lbqty");
                    HiddenField hdqty_ctn = (HiddenField)row.FindControl("hdqty_ctn");
                    HiddenField hdqty_pcs = (HiddenField)row.FindControl("hdqty_pcs");
                    Label lbitemcode = (Label)row.FindControl("lbitemcode");

                    double dStock = 0, dQty = 0, dQty2 = 0, dQtyTotal = 0;
                    string sStock = "", sStock_conv = "";

                    sStock = bll.vLookUp("select isnull(dbo.sfnGetStockBookingseries('" + Request.Cookies["sp"].Value.ToString() + "','" + lbitemcode.Text + "','" + cbbinfrom.SelectedValue.ToString() + "','" + cbfrom.SelectedValue.ToString() + "','" + txtransferno.Text + "'),0) ");

                    dStock = double.Parse(sStock);
                    dQty = double.Parse(hdqty_ctn.Value);
                    dQty2 = double.Parse(hdqty_pcs.Value);
                    dQtyTotal = dQty + (double.Parse(bll.vLookUp("select dbo.sfnUomQtyConv('" + lbitemcode.Text + "','PCS','CTN','" + txqty2.Text + "')")));
                    sStock_conv = bll.vLookUp("select dbo.fn_getqtyconv('" + lbitemcode.Text + "','CTN'," + dStock.ToString() + ")");


                    if (dQtyTotal < 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();sweetAlert('Cant add Qty Minus','Item. " + lbitemcode.Text + "','warning');", true);
                        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
                        return;
                    }
                   
                    if (dStock < dQtyTotal)
                    {
                        string smsgstockallert = "Stock is not enough ! " + lbitemcode.Text.ToString() + " Qty Stock :" + sStock_conv + " ";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();sweetAlert('" + smsgstockallert + "' ,'Stock Not Enough','warning');", true);
                        return;
                    }
                }
            }
            string _status = string.Empty;
            List<cArrayList> arr = new List<cArrayList>();
            switch (optwhs.SelectedValue)
            {
                case "I":
                    _status = "C";
                    break;
                case "V":
                    _status = "C";
                    break;
                case "O":
                    _status = "W";
                    break;
                case "B":
                    _status = "W"; //Waiting approval from spv logistict
                    break;

            }
            List<cArrayList> arr1 = new List<cArrayList>();
            arr1.Add(new cArrayList("@SalesPointCD", Request.Cookies["sp"].Value));
            arr1.Add(new cArrayList("@trf_no", txtransferno.Text));
            arr1.Add(new cArrayList("@trn_trf_dt", System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr1.Add(new cArrayList("@sta_id", _status));
            bll.vUpdatetinternal_transfer(arr1);

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();openreport('fm_report2.aspx?src=it&no=" + txtransferno.Text + "');", true);



            lbstatus.Text = bll.vLookUp("select fld_desc from tfield_value where fld_nm='sta_id' and fld_valu='" + (optwhs.SelectedValue.ToString() == "B" ? "D" : "C") + "'");

            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            //bll.vBatchStockConfirm(arr);
            cd.v_hiddencontrol(btprint);
            // ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();dvshow.setAttribute('class','divhid');", true);
            // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "xux", "popupreport()", true);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=it');", true);
            //Response.Redirect("fm_report.aspx?src=it");
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_internaltransfer_uom");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void bttmp_Click(object sender, EventArgs e)
    {
        string _trf_typ = string.Empty;
        try
        {

            SqlDataReader rs = null;
            txtransferno.Text = Convert.ToString(Session["lootrf_no"]);
            string ssta_id = String.Empty;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@trf_no", txtransferno.Text));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vGettinternal_transfer(arr, ref rs);
            while (rs.Read())
            {
                string prevstk = rs["prevstk"].ToString();
                if (prevstk == String.Empty) { prevstk = "false"; }
                chprevstk.Checked = Boolean.Parse(prevstk);
                dttrf.Text = string.Format("{0:d/M/yyyy}", rs["trf_dt"]);
                txmanualno.Text = rs["manual_no"].ToString();
                optwhs.Text = rs["trf_typ"].ToString();
                _trf_typ = rs["trf_typ"].ToString();
                optwhs_SelectedIndexChanged(sender, e);
                if (_trf_typ == "I") //Whs To Van
                {
                    arr.Clear();
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
                    bll.vBindingComboToSpWithEmptyChoosen(ref cbto, "sp_tmst_vehicle_get", "vhc_cd", "vhc_nm");
                    bll.vBindingComboToSpWithEmptyChoosen(ref cbfrom, "sp_tmst_warehouse_get", "whs_cd", "whs_nm", arr);
                    cbfrom.SelectedValue = rs["whs_cd_from"].ToString();
                    arr.Clear();
                    arr.Add(new cArrayList("@whs_cd", cbfrom.SelectedValue));
                    bll.vBindingComboToSpWithEmptyChoosen(ref cbbinfrom, "sp_twarehouse_bin_get", "bin_cd", "bin_nm", arr);
                    cbbinfrom.SelectedValue = rs["bin_from"].ToString();
                    arr.Clear();
                    arr.Add(new cArrayList("@vhc_cd", cbto.SelectedValue));
                    bll.vBindingComboToSpWithEmptyChoosen(ref cbbinto, "sp_tvan_bin_get", "bin_cd", "bin_nm", arr);
                    cbto.SelectedValue = rs["whs_cd_to"].ToString();
                    cbbinto.SelectedValue = rs["bin_to"].ToString();
                }
                else if (_trf_typ == "V") //Van To Whs
                {
                    arr.Clear();
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
                    bll.vBindingComboToSpWithEmptyChoosen(ref cbfrom, "sp_tmst_vehicle_get", "vhc_cd", "vhc_nm");
                    bll.vBindingComboToSpWithEmptyChoosen(ref cbto, "sp_tmst_warehouse_get", "whs_cd", "whs_nm", arr);
                    cbfrom.SelectedValue = rs["whs_cd_from"].ToString();
                    arr.Clear();
                    arr.Add(new cArrayList("@vhc_cd", cbfrom.SelectedValue));
                    bll.vBindingComboToSpWithEmptyChoosen(ref cbbinfrom, "sp_tvan_bin_get", "bin_cd", "bin_nm", arr);
                    cbbinfrom.SelectedValue = rs["bin_from"].ToString();

                    cbto.SelectedValue = rs["whs_cd_to"].ToString();
                    arr.Clear();
                    arr.Add(new cArrayList("@whs_cd", cbfrom.SelectedValue));
                    bll.vBindingComboToSpWithEmptyChoosen(ref cbbinto, "sp_twarehouse_bin_get", "bin_cd", "bin_nm", arr);


                    cbbinto.SelectedValue = rs["bin_to"].ToString();
                }
                else if (_trf_typ == "O") //BRN to BRN
                {
                    arr.Clear();
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
                    bll.vBindingComboToSpWithEmptyChoosen(ref cbfrom, "sp_tmst_warehouse_get", "whs_cd", "whs_nm");
                    bll.vBindingComboToSpWithEmptyChoosen(ref cbto, "sp_tmst_warehouse_get", "whs_cd", "whs_nm", arr);
                    cbto.SelectedValue = rs["whs_cd_to"].ToString();
                    cbto_SelectedIndexChanged(sender, e);
                    cbbinto.SelectedValue = rs["bin_to"].ToString();
                    cbfrom.SelectedValue = rs["whs_cd_from"].ToString();
                    cbfrom_SelectedIndexChanged(sender, e);
                    cbbinfrom.SelectedValue = rs["bin_from"].ToString();
                }
                else
                {
                    arr.Clear();
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
                    bll.vBindingComboToSpWithEmptyChoosen(ref cbfrom, "sp_tmst_warehouse_get", "whs_cd", "whs_nm", arr);
                    bll.vBindingComboToSpWithEmptyChoosen(ref cbto, "sp_tmst_warehouse_get", "whs_cd", "whs_nm", arr);
                }



                txtabno.Text = rs["tab_no"].ToString();
                //txtabno.CssClass = "form-control ro";
                cd.v_disablecontrol(txtabno);
                txtabno.Enabled = false;
                dtloading.Text = string.Format("{0:d/M/yyyy}", rs["loading_dt"]);
                bool b = Convert.ToBoolean(Convert.ToInt32(rs["urgent"]));
                cburgent.Checked = b;
                ssta_id = rs["sta_id"].ToString();
                lbstatus.Text = bll.vLookUp("select fld_desc from tfield_value where fld_nm='sta_id' and fld_valu='" + ssta_id + "'");
                if (cbbinto.SelectedValue == "BS") { cbreason.SelectedValue = rs["reason"].ToString(); }
                //   txtabno.Text = rs["tab_no"].ToString();
                txremark.Text = rs["remark"].ToString();
                //txremark.CssClass = "form-control ro";
                //txmanualno.CssClass = "form-control ro";
                cd.v_disablecontrol(txremark);
                cd.v_disablecontrol(txmanualno);
            }
            rs.Close();
            disableheader();

            bindinggrd();

            if (ssta_id == "C")
            {
                grd.Columns[8].Visible = false; grd.Columns[7].Visible = false; cd.v_hiddencontrol(btprint); cd.v_hiddencontrol(btsave_i); cd.v_hiddencontrol(btsearch);
                //lbstatus.Text = "Completed";
            }
            else if (ssta_id == "N")
            {
                cd.v_hiddencontrol(btsave_i);
                cd.v_hiddencontrol(btsave_v);
                cd.v_hiddencontrol(btsave_o);
                cd.v_showcontrol(btprint);
                cd.v_hiddencontrol(btsearch);
            }
            else if (ssta_id == "W")
            {
                cd.v_hiddencontrol(btsave_i);
                cd.v_hiddencontrol(btprint);
                cd.v_hiddencontrol(btsearch);
                grd.Columns[8].Visible = false;
                grd.Columns[7].Visible = false;
                //lbstatus.Text = "Waiting for approval";
            }
            else if (ssta_id == "A")
            {
                cd.v_hiddencontrol(btsave_i);
                cd.v_showcontrol(btprint);
                cd.v_hiddencontrol(btsearch);
                grd.Columns[7].Visible = false; grd.Columns[8].Visible = false;
                //lbstatus.Text = "Approved , need print document";
            }

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_internaltransfer_uom");
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
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_internaltransfer_uom");
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
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_internaltransfer_uom");
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
        Response.Redirect("fm_internaltransfer_uom.aspx");
    }
    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {

            if (lbstatus.Text == "COMPLETE")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction has been complete, data can not delete','error');", true);
                // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
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
            //Label lbseqID = (Label)grd.Rows[e.RowIndex].FindControl("lbseqID");
            //List<cArrayList> arr = new List<cArrayList>();
            //arr.Add(new cArrayList("@seqID", lbseqID.Text));
            ////arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            ////bll.vDeletetinternal_transfer_dtl(arr);
            //string _sql = "delete tinternal_transfer_dtl where seqid='"+lbseqID.Text+"'";
            //bindinggrd();
            int _idx = 0;
            Label lbitemcode = (Label)grd.Rows[e.RowIndex].FindControl("lbitemcode");
            List<tinternal_transfer_dtl> _tinternal_transfer_dtl = (List<tinternal_transfer_dtl>)Session["tinternal_transfer_dtl"];
            foreach (tinternal_transfer_dtl _d in _tinternal_transfer_dtl)
            {
                if (_d.item_cd == lbitemcode.Text)
                {
                    _idx = e.RowIndex;
                }
            }
            _tinternal_transfer_dtl.RemoveAt(_idx);
            grd.DataSource = _tinternal_transfer_dtl;
            grd.DataBind();
            Label lbtotqtyctn = (Label)grd.FooterRow.FindControl("lbtotqtyctn");
            Label lbtotqtypcs = (Label)grd.FooterRow.FindControl("lbtotqtypcs");
            lbtotqtyctn.Text = bll2.fn_gettotalfooter(ref grd, "lbqtyctn").ToString("N2");
            lbtotqtypcs.Text = bll2.fn_gettotalfooter(ref grd, "lbqtypcs").ToString("N2");
            Session["tinternal_transfer_dtl"] = _tinternal_transfer_dtl;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Data Deleted successfully !','error');", true);
            // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_internaltransfer_uom");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btsearch2_Click(object sender, EventArgs e)
    {

    }
    protected void btsearch2_Click1(object sender, EventArgs e)
    {

    }
    protected void txqty_TextChanged(object sender, EventArgs e)
    {
        try
        {
            double dQtyCheck = 0;

            if (!double.TryParse(txqty.Text, out dQtyCheck))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Qty must be numeric','Check Qty','warning');", true);
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
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_internaltransfer_uom");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //try
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        HiddenField lbqty = (HiddenField)e.Row.FindControl("lbqty");
        //        HiddenField hdqty_ctn = (HiddenField)e.Row.FindControl("hdqty_ctn");
        //        HiddenField hdqty_pcs = (HiddenField)e.Row.FindControl("hdqty_pcs");
        //        HiddenField hdstockqty = (HiddenField)e.Row.FindControl("hdstockqty");
        //        HiddenField hdstockqty_ctn = (HiddenField)e.Row.FindControl("hdstockqty_ctn");
        //        HiddenField hdstockqty_pcs = (HiddenField)e.Row.FindControl("hdstockqty_pcs");
        //        Label lbitemcode = (Label)e.Row.FindControl("lbitemcode");

        //        totalQtyCtn = totalQtyCtn + decimal.Parse(hdqty_ctn.Value);
        //        totalQtyPcs = totalQtyPcs + decimal.Parse(hdqty_pcs.Value);
        //        totalStockQtyCtn = totalStockQtyCtn + decimal.Parse(hdstockqty_ctn.Value);
        //        totalStockQtyPcs = totalStockQtyPcs + decimal.Parse(hdstockqty_pcs.Value);
        //    }
        //    if (e.Row.RowType == DataControlRowType.Footer)
        //    {
        //        Label lblTotalqty = (Label)e.Row.FindControl("lblTotalqty");
        //        Label lblTotalstockqty = (Label)e.Row.FindControl("lblTotalstockqty");
        //        lblTotalqty.Text = totalQtyCtn.ToString() + " CTN, " + totalQtyPcs.ToString() + " PCS";
        //        lblTotalstockqty.Text = totalStockQtyCtn.ToString() + " CTN, " + totalStockQtyPcs.ToString() + " PCS";
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
        //    bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_internaltransfer_uom");
        //    Response.Redirect("fm_ErrorPage.aspx");
        //}
    }
    protected void btrefresh_Click(object sender, EventArgs e)
    {
        try
        {
            List<tinternal_transfer_dtl> _tinternal_transfer_dtl = (List<tinternal_transfer_dtl>)Session["tinternal_transfer_dtl"];
            DateTime dt = DateTime.ParseExact(dttrf.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            if ((optwhs.SelectedValue == String.Empty) || (optwhs.SelectedValue == null))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Transfer Stock type has not yet selected','Type Transfer','warning');", true);
                return;
            }

            DateTime ddate = DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            string sStock = "0";
            if (chprevstk.Checked == true)
            {
                sStock = bll.vLookUp("select dbo.[sfnGetStock]('" + Request.Cookies["sp"].Value.ToString() + "','" + hditem.Value.ToString() + "','" + cbbinfrom.SelectedValue.ToString() + "','" + cbfrom.SelectedValue.ToString() + "','0','" + ddate.Year + "-" + ddate.Month + "-" + ddate.Day + "')");//by yanto 25-6-2016 
            }
            else
            {
                if (txtransferno.Text == String.Empty || txtransferno.Text == "NEW")
                {
                    sStock = bll.vLookUp("select dbo.[sfnGetStockBooking]('" + Request.Cookies["sp"].Value.ToString() + "','" + hditem.Value.ToString() + "','" + cbbinfrom.SelectedValue.ToString() + "','" + cbfrom.SelectedValue.ToString() + "','0','" + ddate.Year + "-" + ddate.Month + "-" + ddate.Day + "')");//by yanto 25-6-2016 
                }
                else
                {
                    { sStock = bll.vLookUp("select dbo.sfnGetStockBookingseries('" + Request.Cookies["sp"].Value.ToString() + "','" + hditem.Value + "','" + cbbinfrom.SelectedValue.ToString() + "','" + cbfrom.SelectedValue.ToString() + "','" + txtransferno.Text + "')"); }
                }
            }




            if (sStock == null || sStock == string.Empty) { sStock = "0"; }
            txstk.Text = sStock;
            //txqty.Text = sStock;
            double _stock = Convert.ToDouble(sStock);
            //double _stockinpcs = Convert.ToDouble(bll.vLookUp("select dbo.fn_getqtyconv_uom('" + hditem.Value + "','" + cbuom.SelectedValue + "'," + sStock + ",'PCS')"));
            txqty2.Text = "0";//_stockinpcs.ToString();
            txqty.Text = "0";
            cd.v_disablecontrol(txstk);
            cd.v_showcontrol(btsearch);
            cd.v_enablecontrol(txqty2);
            Session["tinternal_transfer_dtl"] = _tinternal_transfer_dtl;
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_internaltransfer_uom");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        finally
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
        }

    }
    protected void txitem_TextChanged(object sender, EventArgs e)
    {
        try
        {

            // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
            disableheader();
            //txqty.Focus();
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_internaltransfer_uom");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    private void disableheader()
    {
        //cbfrom.CssClass = "from-control input-sm ro";
        //cbfrom.Enabled = false;
        //cbbinfrom.CssClass = "from-control input-sm ro";
        //cbbinfrom.Enabled = false;
        //cbto.CssClass = "from-control input-sm ro";
        //cbto.Enabled = false;
        //cbbinto.CssClass = "from-control input-sm ro";
        //cbbinto.Enabled = false;
        //dttrf.CssClass = "from-control  ro";
        //dttrf.Enabled = false;
        cd.v_disablecontrol(cbfrom);
        cd.v_disablecontrol(cbbinfrom);
        cd.v_disablecontrol(cbto);
        cd.v_disablecontrol(cbbinto);
        cd.v_disablecontrol(dttrf);

        //btsave_i.Visible = false;
    }
    protected void bttabsearch_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "window.open('fm_tabinternaltransfer.aspx','mywindow','toolbar=n,scrollbars=y,width=800,height=800,top=75,left=300',true);", true);
    }

    protected void chprevstk_CheckedChanged(object sender, EventArgs e)
    {
        try
        {

            if (optwhs.SelectedValue == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please Choose  Transfer type!','Prev stk can not changed ','error');", true);
                //  ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
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
                //     ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
                if (chprevstk.Checked == true) { chprevstk.Checked = false; } else { chprevstk.Checked = true; }
                return;
            }

            if (grd.Rows.Count != 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Item already inserted !','Prev stk can not changed ','error');", true);
                //  ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
                if (chprevstk.Checked == true) { chprevstk.Checked = false; } else { chprevstk.Checked = true; }
                return;
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_internaltransfer_uom");
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
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_internaltransfer_uom");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }



    protected void cbreason_SelectedIndexChanged(object sender, EventArgs e)
    {
        //cbreason.CssClass = "ro";
        //cbreason.Enabled = false;
        List<tinternal_transfer_dtl> _tinternal_transfer_dtl = (List<tinternal_transfer_dtl>)Session["tinternal_transfer_dtl"];
        cd.v_disablecontrol(cbreason);
        Session["tinternal_transfer_dtl"] = _tinternal_transfer_dtl;
    }



    protected void cbuom_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btsave_o_Click(object sender, EventArgs e)
    {

    }

    protected void btsave_v_Click(object sender, EventArgs e)
    {
        // Van To Depo
        if (optwhs.SelectedValue == string.Empty || optwhs.SelectedValue == null)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();sweetAlert('Please select type','Type transfer selection','warning');", true);
            return;
        }
        string _status = "N";
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@trf_dt", DateTime.ParseExact(dttrf.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@trf_typ", optwhs.SelectedValue));
        arr.Add(new cArrayList("@whs_cd_from", cbfrom.SelectedValue));
        arr.Add(new cArrayList("@bin_from", cbbinfrom.SelectedValue));
        arr.Add(new cArrayList("@whs_cd_to", cbto.SelectedValue));
        arr.Add(new cArrayList("@bin_to", cbbinto.SelectedValue));
        arr.Add(new cArrayList("@item_cd", hditem.Value));
        arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value));
        arr.Add(new cArrayList("@manual_no", txmanualno.Text));
        arr.Add(new cArrayList("@tab_no", txtabno.Text));
        arr.Add(new cArrayList("@prevstk", chprevstk.Checked));
        arr.Add(new cArrayList("@sta_id", _status));
        arr.Add(new cArrayList("@reason", cbreason.SelectedValue));
        arr.Add(new cArrayList("@remark", txremark.Text));
        string sTrfNo = string.Empty;
        bll.vInsertInternalTransfer(arr, ref sTrfNo);
        txtransferno.Text = sTrfNo;
        // if (ssta_id == "A") { lbstatus.Text = "Approved"; } else if (ssta_id == "W") { lbstatus.Text = "Waiting Aproval"; } else if (ssta_id == "N") { lbstatus.Text = "New"; }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();sweetAlert('Data Save successfully ..','Internal Tranfer No. " + txtransferno.Text + "','info');", true);
    }

    protected void btsave_b_Click(object sender, EventArgs e)
    {

    }
}