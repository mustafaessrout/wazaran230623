using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using AjaxControlToolkit;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_repack : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string _salespointcd = Request.Cookies["sp"].Value;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", _salespointcd));
            bll.vBindingComboToSpWithEmptyChoosen(ref cbosource, "sp_tmst_bom_getitem", "bom_cd", "item_nm", arr); //bom_item

            //bll.vBindingFieldValueToComboWithChoosen(ref cbfromto, "fromto");
            arr.Clear();
            arr.Add(new cArrayList("@group_cd", bll.sGetControlParameter("grouprepacking")));
            //bll.vBindingComboToSpWithEmptyChoosen(ref cbitemsupport, "sp_tmst_itemsupport_getbygroup", "item_cd", "item_nm", arr);
            //bll.vBindingFieldValueToComboWithChoosen(ref cbinout, "inoutbound");
            //bll.vBindingFieldValueToCombo(ref cbuom, "uom");
            dtprocessingpack.Text = Request.Cookies["waz_dt"].Value;

            //List<tlog_processingdtl> _tlog_processingdtl = new List<tlog_processingdtl>();
            //Session["tlog_processingdtl"] = _tlog_processingdtl;
            //List<tlog_repackingsupportusage> _tlog_repackingsupportusage = new List<tlog_repackingsupportusage>();
            //Session["tlog_repackingsupportusage"] = _tlog_repackingsupportusage;
            List<tlog_titem_processingdtl> _tlog_titem_processingdtl = new List<tlog_titem_processingdtl>();
            Session["tlog_titem_processingdtl"] = _tlog_titem_processingdtl;


            //cd.v_disablecontrol(dtbom);
            cd.v_hiddencontrol(btsave);
            cd.v_hiddencontrol(btupdate);
            cd.v_hiddencontrol(btprint);
            cd.v_showcontrol(btnew);
            cd.v_disablecontrol(txprocessingno);
            //cd.v_disablecontrol(txstock);
            btsearch.Focus();
        }
    }

    protected void cbosource_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        bll.vBindingFieldValueToComboWithChoosen(ref cbuompack, "uom");
        string sItemcd = bll.vLookUp("select bom_item from tmst_bom where bom_cd='" + cbosource.SelectedValue + "'");
        string sUom = bll.vLookUp("select uom_base from tmst_item where item_cd='" + sItemcd + "'");
        //DateTime ddate = System.DateTime.ParseExact(dtrepacking.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //string _stock = bll.vLookUp("select dbo.[sfnGetStockBooking]('" + cbosource.SelectedValue + "','" + cbbin.SelectedValue.ToString() + "','" + cbwhs.SelectedValue.ToString() + "','DEPO','" + ddate.Year + "-" + ddate.Month + "-" + ddate.Day + "')");//by yanto 25-6-2016 
        //txstock.Text = _stock;
        string sWhs = bll.vLookUp("select dbo.fn_getproductwarehouse('" + sItemcd + "','" + Request.Cookies["sp"].Value + "')");
        cbuompack.SelectedValue = sUom;

        cd.v_disablecontrol(cbuompack);
        cd.v_disablecontrol(cbwhs);
        cd.v_disablecontrol(cbbin);

        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        bll.vBindingComboToSpWithEmptyChoosen(ref cbwhs, "sp_tmst_warehouse_getbydc", "whs_cd", "whs_nm", arr);
        cbwhs.SelectedValue = sWhs;
        cbwhs_SelectedIndexChanged(sender, e);
        //cbuom.SelectedValue = bll.vLookUp("select uom_base from tmst_item where item_cd='" + cbosource.SelectedValue + "'");
        //cbwhs.SelectedValue = bll.vLookUp("select dbo.fn_getproductwarehouse('" + cbosource.SelectedValue + "','" + Request.Cookies["sp"].Value + "')");

        //List<cArrayList> arr = new List<cArrayList>();
        //bll.vBindingFieldValueToComboWithChoosen(ref cbuom, "uom");
        ////string sUom = bll.vLookUp("select uom_base from tmst_item where item_cd='" + cbosource.SelectedValue + "'");
        ////DateTime ddate = System.DateTime.ParseExact(dtrepacking.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        ////string _stock = bll.vLookUp("select dbo.[sfnGetStockBooking]('" + cbosource.SelectedValue + "','" + cbbin.SelectedValue.ToString() + "','" + //cbwhs.SelectedValue.ToString() + "','DEPO','" + ddate.Year + "-" + ddate.Month + "-" + ddate.Day + "')");//by yanto 25-6-2016 
        ////txstock.Text = _stock;
        ////string sWhs = bll.vLookUp("select dbo.fn_getproductwarehouse('" + cbosource.SelectedValue + "','" + Request.Cookies["sp"].Value + "')");
        ////cbuom.SelectedValue = sUom;

        //cd.v_disablecontrol(cbuom);
        ////cd.v_disablecontrol(cbwhs);
        ////cd.v_disablecontrol(cbbin);

        //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        ////bll.vBindingComboToSpWithEmptyChoosen(ref cbwhs, "sp_tmst_warehouse_getbydc", "whs_cd", "whs_nm", arr);
        ////cbwhs.SelectedValue = sWhs;
        //cbwhs_SelectedIndexChanged(sender, e);
        ////cbuom.SelectedValue = bll.vLookUp("select uom_base from tmst_item where item_cd='" + cbosource.SelectedValue + "'");
        ////cbwhs.SelectedValue = bll.vLookUp("select dbo.fn_getproductwarehouse('" + cbosource.SelectedValue + "','" + Request.Cookies["sp"].Value + "')");
    }

    //protected void btadd_Click(object sender, EventArgs e)
    //{
    //    //if (cbfromto.SelectedValue == string.Empty)
    //    //{
    //    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select from / to','From To','warning');", true);
    //    //    return;
    //    //}

    //    //if (cbwhs.SelectedValue == string.Empty)
    //    //{
    //    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select warehouse!','Warehouse','warning');", true);
    //    //    return;
    //    //}

    //    //if (cbbin.SelectedValue == string.Empty)
    //    //{
    //    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select BIN','Bin','warning');", true);
    //    //    return;
    //    //}

    //    //if (cbosource.SelectedValue == string.Empty)
    //    //{
    //    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select item for process!','Item Warehouse','warning');", true);
    //    //    return;
    //    //}
    //    //decimal _qty = 0;
    //    //if (!decimal.TryParse(txqty.Text, out _qty))
    //    //{
    //    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Unit item must define','Qty','warning');", true);
    //    //    return;
    //    //}

    //    //if (_qty == 0)
    //    //{
    //    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Qty can not zero','Qty','warning');", true);
    //    //    return;
    //    //}


    //    //decimal _stock = Convert.ToDecimal(txstock.Text);
    //    //if ((_stock == 0) && (cbfromto.SelectedValue == "F"))
    //    //{
    //    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Stock is zero','Pls stock in !','warning');", true);
    //    //    return;
    //    //}
    //    //if (cbfromto.SelectedValue == "F")
    //    //{

    //    //    if (_qty > _stock)
    //    //    {
    //    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Qty bigger than stock','Pls check qty stock available !','warning');", true);
    //    //        return;
    //    //    }
    //    //}

    //    //if ((_stock < 0) && (cbfromto.SelectedValue == "F"))
    //    //{
    //    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Stock is not enough','Pls check qty stock available !','warning');", true);
    //    //    return;
    //    //}

    //    List<tlog_bomdtl> _tlog_bomdtl = (List<tlog_bomdtl>)Session["tlog_bomdtl"];
    //    _tlog_bomdtl.Add(new tlog_bomdtl
    //    {
    //        item_cd = hditem.Value.ToString(),
    //        item_nm = bll.vLookUp("select item_nm from tmst_item where item_cd='" + hditem.Value.ToString() + "'"),
    //        //size = bll.vLookUp("select size from tmst_item where item_cd='" + cbosource.SelectedValue + "'"),
    //        //bin_cd = cbbin.SelectedValue,
    //        //whs_cd = cbwhs.SelectedValue,
    //        //qty = _qty,
    //        //fromto = cbfromto.SelectedValue,
    //        //uom = cbuom.SelectedValue,
    //        //fromto_nm = bll.vLookUp("select fld_desc from tfield_value where fld_nm='fromto' and fld_valu='" + cbfromto.SelectedValue + "'")
    //    });

    //    grd.DataSource = _tlog_bomdtl;
    //    grd.DataBind();

    //    //cbfromto.SelectedValue = string.Empty;
    //    //txstock.Text = string.Empty;
    //    //cbosource.Items.Clear();
    //    //txqty.Text = string.Empty;
    //    //cbuom.Items.Clear();
    //    //cbwhs.Items.Clear();
    //    //cbbin.Items.Clear();
    //    //cbuom.SelectedValue = string.Empty;
    //    //cbwhs.SelectedValue = string.Empty;
    //    //cbbin.SelectedValue = string.Empty;
    //    //btsave.Style.Add("display", "normal");
    //    cd.v_showcontrol(btsave);

    //}

    protected void cbfromto_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@group_cd", bll.sGetControlParameter("grouprepacking")));
        //cbosource.Items.Clear();
        //cbuom.Items.Clear();
        //cbosource.SelectedValue = string.Empty;
        //cbuom.SelectedValue = string.Empty;
        //arr.Add(new cArrayList("qry_cd", "whs_branch"));
        //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        //bll.vBindingComboToSpWithEmptyChoosen(ref cbwhs, "sp_tmst_warehouse_getbydc", "whs_cd", "whs_nm", arr);
        //cbfromto_SelectedIndexChanged(sender, e);
        //bll.vBindingComboToSpWithEmptyChoosen(ref cbitemsupport, "sp_tmst_itemsupport_getbygroup", "item_cd", "item_nm", arr);
        arr.Clear();
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        //if (cbfromto.SelectedValue == "F")
        //{
        //    arr.Add(new cArrayList("@qry_cd", "repackingfrom"));
        //}
        //else if (cbfromto.SelectedValue == "T")
        //{
        //    arr.Add(new cArrayList("@qry_cd", "repackingto"));
        //}
        //bll.vBindingComboToSpWithEmptyChoosen(ref cbosource, "sp_tmst_item_getbymapquery", "item_cd", "item_nm", arr);
        //cbosource_SelectedIndexChanged(sender, e);
        //cbwhs.SelectedValue = bll.vLookUp("select dbo.fn_getproductwarehouse('" + hditem.Value + "','" + Request.Cookies["sp"].Value + "')");
    }

    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbitemcode = (Label)grd.Rows[e.RowIndex].FindControl("lbitemcode");
        List<tlog_titem_processingdtl> _tlog_titem_processingdtl = (List<tlog_titem_processingdtl>)Session["tlog_titem_processingdtl"];
        _tlog_titem_processingdtl.RemoveAt(e.RowIndex);

        grd.DataSource = _tlog_titem_processingdtl;
        grd.DataBind();

        //Label lbitemcode = (Label)grd.Rows[e.RowIndex].FindControl("lbitemcode");
        //List<tlog_bomdtl> _tlog_bomdtl = (List<tlog_bomdtl>)Session["tlog_bomdtl"];
        //_tlog_bomdtl.RemoveAt(e.RowIndex);

        //grd.DataSource = _tlog_bomdtl;
        //grd.DataBind();
        //List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
        //arr.Add(new cArrayList("@packing_cd", Request.Cookies["usr_id"].Value));
        //bll.vDeleteLogRepackingDtl(arr);
        //arr.Clear();
        //arr.Add(new cArrayList("@packing_cd", Request.Cookies["usr_id"].Value));
        //bll.vBindingGridToSp(ref grd, "sp_tlog_repackingdtl_get", arr);
    }

    protected void btsave_Click(object sender, EventArgs e)
    {

        decimal _outqty = 0; //qty hasil kali item bom master
        decimal _qtystockavl = 0; //gudang tes stock kosong 
        decimal _inqty = 0; //qty masuk bom master
        //decimal _tallysheet = 0; //qty masuk bom master
        foreach (GridViewRow row in grd.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                if (row.RowState == DataControlRowState.Edit)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please updated row for stock qty shippment changed!','Edit Mode','warning');", true);
                    return;
                }

                Label lboutqty = (Label)row.FindControl("lboutqty"); //2400
                Label lbstockavl = (Label)row.FindControl("lbstockavl"); //gudang tes stock kosong 
                Label lbinqty = (Label)row.FindControl("lbinqty");
                TextBox txtallysheet = (TextBox)row.FindControl("txtallysheet");
                Label lbitemcode = (Label)row.FindControl("lbitemcode");
                _qtystockavl = Convert.ToDecimal(lbstockavl.Text); //gudang tes stock kosong 
                _outqty = Convert.ToDecimal(lboutqty.Text);
                _inqty = Convert.ToDecimal(_inqty);
                //_tallysheet = Convert.ToDecimal(txtallysheet.Text);

                //decimal dQtyout = 0; decimal dQtystockavl = 0; decimal dQtytally = 0;
                //if (!decimal.TryParse(txtallysheet.Text, out _tallysheet))
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please entry on decimal for actual qty tally','Actual qty tally','warning');", true);
                //    return;
                //}

                //if (!decimal.TryParse(lboutqty.Text, out dQtyout))
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Qty must numeric','check order qty','warning');", true);
                //    return;
                //}

                //if (!decimal.TryParse(lbstockavl.Text, out dQtystockavl))
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Qty must numeric','check order qty','warning');", true);
                //    return;
                //}

                //if (!decimal.TryParse(txtallysheet.Text, out dQtytally))
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Qty tally sheet must numeric','check tally sheet qty','warning');", true);
                //    return;
                //}



                //if (dQtyout > dQtystockavl)
                //{
                //    string _item_cd = lbitemcode.Text;
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Qty Pack Out can not bigger than stock available','Check Qty Pack Out and stock available and item code " + _item_cd + "','warning');", true);
                //    return;
                //}

                //if (dQtyout > dQtytally)
                //{
                //    string _item_cd = lbitemcode.Text;
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Qty Tally sheet can not less than Out qty item','Check Qty Tally sheet and item code " + _item_cd + "','warning');", true);
                //    return;
                //}


            }


        }

        string sItemcd = bll.vLookUp("select bom_item from tmst_bom where bom_cd='" + cbosource.SelectedValue + "'");
        string sWhs = bll.vLookUp("select dbo.fn_getproductwarehouse('" + sItemcd + "','" + Request.Cookies["sp"].Value + "')");
        string sBin = bll.vLookUp("select bin_cd from twarehouse_bin where whs_cd = '" + sWhs + "' and bin_cd in (select fld_valu from tfield_value where fld_nm = 'bin_gs')");

        DateTime ddate = System.DateTime.ParseExact(dtprocessingpack.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        string _stock = bll.vLookUp("select dbo.[fn_checkstock]('" + sItemcd + "','" + sWhs + "','" + sBin + "')");//by yanto 25-6-2016 


        List<cArrayList> arr = new List<cArrayList>();

        //arr.Clear();
        //arr.Add(new cArrayList("@so_cd", hdto.Value));
        //arr.Add(new cArrayList("@so_sta_id", "L"));
        //arr.Add(new cArrayList("@ref_no", ""));
        //bll.vUpdateMstProcessing(arr);
        //bll.vUpdateMstSalesOrder(arr);
        arr.Clear();
        string _processing_no = string.Empty;
        arr.Add(new cArrayList("@created_by", Request.Cookies["usr_id"].Value));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        arr.Add(new cArrayList("@processing_item", sItemcd));
        arr.Add(new cArrayList("@qty", txqtypack.Text));
        arr.Add(new cArrayList("@order_no", hddoc.Value));
        arr.Add(new cArrayList("@bom_cd", cbosource.SelectedValue));
        arr.Add(new cArrayList("@processing_sta_id", "A"));
        arr.Add(new cArrayList("@remark", txtremark.Text));
        arr.Add(new cArrayList("@processing_dt", System.DateTime.ParseExact(dtprocessingpack.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@created_dt", System.DateTime.ParseExact(dtprocessingpack.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@processing_type", "P"));
        arr.Add(new cArrayList("@qty_stock", Convert.ToDecimal(_stock)));
        //bll.vInsertMstProcessing(arr, ref _processing_no);
        txprocessingno.Text = _processing_no;

        //insert processing dtl
        List<tlog_titem_processingdtl> _tlog_titem_processingdtl = (List<tlog_titem_processingdtl>)Session["tlog_titem_processingdtl"];
        foreach (tlog_titem_processingdtl _dtl in _tlog_titem_processingdtl)
        {
            arr.Clear();
            arr.Add(new cArrayList("@item_cd", _dtl.item_cd));
            arr.Add(new cArrayList("@uom", _dtl.uom));
            arr.Add(new cArrayList("@qty", _dtl.out_qty));
            arr.Add(new cArrayList("@whs_cd", _dtl.whs_cd));
            arr.Add(new cArrayList("@bin_cd", _dtl.bin_cd));
            arr.Add(new cArrayList("@processing_no", _processing_no));
            arr.Add(new cArrayList("@qty_tally", _dtl.qty_tally));
            //bll.vInsertTitemProcessingDtl(arr);
            //bll.vInsertLogRepackingDtl(arr);
        }

        cd.v_disablecontrol(grd);
        cd.v_showcontrol(btprint);
        cd.v_hiddencontrol(btsave);
        cd.v_disablecontrol(cbosource);

        //string _so_cd = hdto.Value;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Packing Processing done, Please print document!','" + _processing_no + "','warning');", true);
        return;

    }

    protected void btupdate_Click(object sender, EventArgs e)
    {
        if (grd.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Items to be BOM is not available','Please select item BOM','warning');", true);
            return;
        }

        string _salespointcd = string.Empty;
        string _bom_item = string.Empty;
        string _bom_nm = string.Empty;

        //List<tlog_bomdtl> _tlog_bomdtl = (List<tlog_bomdtl>)Session["tlog_bomdtl"];
        //foreach (tlog_bomdtl _dtl in _tlog_bomdtl)
        //{
        //    _salespointcd = Request.Cookies["sp"].Value;
        //    _bom_item = _dtl.item_cd;
        //    _bom_nm = _dtl.item_nm;
        //}

        string _bom_no = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@created_date", System.DateTime.ParseExact(dtprocessingpack.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@created_by", Request.Cookies["usr_id"].Value));
        arr.Add(new cArrayList("@salespointcd", _salespointcd));
        arr.Add(new cArrayList("@bom_item", _bom_item));
        arr.Add(new cArrayList("@bom_nm", _bom_nm));
        //bll.vUpdateLogmstbomitem(arr, ref _repacking_no);
        //bll.vInsertLogmstbomitem(arr, ref _bom_no);

        txprocessingno.Text = _bom_no;

        //foreach (tlog_bomdtl _dtl in _tlog_bomdtl)
        //{
        //    arr.Clear();
        //    arr.Add(new cArrayList("@bom_cd", _bom_no));
        //    arr.Add(new cArrayList("@item_compose", _dtl.item_nm));
        //    arr.Add(new cArrayList("@unit_item", _dtl.qty));
        //    //bll.vInsertLogBomDtl(arr);
        //    bll.vUpdateLogBomDtl(arr);
        //}

        cd.v_disablecontrol(grd);
        cd.v_showcontrol(btprint);
        cd.v_hiddencontrol(btsave);

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Build of Material update done, Please print document!','" + _bom_no + "','warning');", true);
        return;
    }

    protected void btprint_Click(object sender, EventArgs e)
    {
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=repacking&pkg=" + txbomno.Text + "');", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=bomitemprocessing&processing=" + txprocessingno.Text + "');", true);
    }

    protected void btlookup_Click(object sender, EventArgs e)
    {
        System.Data.SqlClient.SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@processing_no", hdprocessing.Value));
        //bll.vGetLogRepacking(arr, ref rs); 
       //bll.vGetLogProcessingitem(arr, ref rs);
       // while (rs.Read())
       // {
       //     txprocessingno.Text = hdprocessing.Value;
       //     dtprocessingpack.Text = rs["created_dt"].ToString();
       //     //cbosource.SelectedValue = rs["processing_item"].ToString();

       // }
       // rs.Close();
        arr.Clear();
        arr.Add(new cArrayList("@processing_no", hdprocessing.Value));
        bll.vBindingGridToSp(ref grd, "sp_titem_processing_dtl_get", arr); ///sp_tlog_titem_processingdtl
        //grd.CssClass = "table table-bordered table-condensed ro";

        //string sItem_cd = bll.vLookUp("select processing_item from tmst_processing where processing_no='" + hdprocessing.Value.ToString() + "'");
        //List<tlog_processingdtl> _tlog_processingdtl = (List<tlog_processingdtl>)Session["tlog_processingdtl"];
        //_tlog_processingdtl.Add(new tlog_processingdtl
        //{
        //    item_cd = bll.vLookUp("select processing_item from tmst_processing where bom_cd='" + hdprocessing.Value.ToString() + "'"),
        //    item_nm = bll.vLookUp("select bom_nm from tmst_bom where bom_cd='" + hdprocessing.Value.ToString() + "'"),
        //    qty = Convert.ToDecimal(bll.vLookUp("select unit_item from tbom_detail where bom_cd='" + hdprocessing.Value.ToString() + "'")),
        //    uom = bll.vLookUp("select uom_base from tmst_item where item_cd='" + sItem_cd.ToString() + "'"),

        //});

        //grd.DataSource = _tlog_processingdtl;
        //grd.DataBind();
        //txqty.Text = string.Empty;
        //cbuom.SelectedValue = string.Empty;


        cd.v_disablecontrol(grd);
        cd.v_hiddencontrol(btsave);
        //cd.v_enablecontrol(btprint);
        cd.v_hiddencontrol(btupdate);
        //btsave.Style.Add("display", "none");
        btprint.Style.Add("display", "normal");
    }

    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_repack.aspx");
    }

    protected void cbwhs_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        //string sbin = bll.vLookUp("select dbo.fn_getproductwarehouse('" + cbwhs.SelectedValue + "','" + Request.Cookies["sp"].Value + "')");
        arr.Add(new cArrayList("@whs_cd", cbwhs.SelectedValue));
        bll.vBindingComboToSp(ref cbbin, "sp_twarehouse_bin_get_top1", "bin_cd", "bin_nm", arr);
        //string sbin = bll.vLookUp("select bin_cd from twarehouse_bin where whs_cd='" + cbwhs.SelectedValue + "'");
        //cbbin.SelectedValue = sbin;
        //txqty.Text = string.Empty;
        //DateTime ddate = System.DateTime.ParseExact(dtrepacking.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //string _stock = bll.vLookUp("select dbo.[sfnGetStockBooking]('" + cbosource.SelectedValue + "','" + cbbin.SelectedValue.ToString() + "','" + cbwhs.SelectedValue.ToString() + "','DEPO','" + ddate.Year + "-" + ddate.Month + "-" + ddate.Day + "')");//by yanto 25-6-2016 
        //txstock.Text = _stock;     


        //List<cArrayList> arr = new List<cArrayList>();
        ////string sbin = bll.vLookUp("select dbo.fn_getproductwarehouse('" + cbwhs.SelectedValue + "','" + Request.Cookies["sp"].Value + "')");
        ////arr.Add(new cArrayList("@whs_cd", cbwhs.SelectedValue));
        ////bll.vBindingComboToSp(ref cbbin, "sp_twarehouse_bin_get_top1", "bin_cd", "bin_nm", arr);
        ////string sbin = bll.vLookUp("select bin_cd from twarehouse_bin where whs_cd='" + cbwhs.SelectedValue + "'");
        ////cbbin.SelectedValue = sbin;
        //txqty.Text = string.Empty;
        //DateTime ddate = System.DateTime.ParseExact(dtbom.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //string _stock = bll.vLookUp("select dbo.[sfnGetStockBooking]('" + cbosource.SelectedValue + "','" + cbbin.SelectedValue.ToString() + "','" + cbwhs.SelectedValue.ToString() + "','DEPO','" + ddate.Year + "-" + ddate.Month + "-" + ddate.Day + "')");//by yanto 25-6-2016 
        //txstock.Text = _stock;
        //arr.Clear();
        //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        //if (cbfromto.SelectedValue == "F")
        //{
        //    arr.Add(new cArrayList("@qry_cd", "repackingfrom"));
        //}
        //else if (cbfromto.SelectedValue == "T")
        //{
        //    arr.Add(new cArrayList("@qry_cd", "repackingto"));
        //}
        //bll.vBindingComboToSpWithEmptyChoosen(ref cbosource, "sp_tmst_item_getbymapquery", "item_cd", "item_nm", arr);
        //cbosource_SelectedIndexChanged(sender, e);
    }

    protected void btaddnontrade_Click(object sender, EventArgs e)
    {
        //if (cbitemsupport.SelectedValue == string.Empty)
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select item non trade!','Item Non Trade','warning');", true);
        //    return;
        //}

        //decimal _qty = 0;
        //decimal _stockavl = 0;

        //if (!decimal.TryParse(txqtynontrade.Text, out _qty))
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please enter correct qty usage','Qty usage','warning');", true);
        //    return;
        //}

        //_stockavl = Convert.ToDecimal(lbstocksupportavl.Text);

        //if (_qty > _stockavl)
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Stock is not enough','Check Stock','warning');", true);
        //    return;

        //}

        //if (_qty == 0)
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Qty usage must be greather than zero','Qty Usage','warning');", true);
        //    return;
        //}
        //double dStockSupport = 0;

        //if (!double.TryParse(lbstocksupportavl.Text, out dStockSupport))
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Stock Not Available','Item Non Trade','warning');", true);
        //    return;
        //}

        //if (dStockSupport == 0)
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Stock is not enough!','Item Non Trade','warning');", true);
        //    return;
        //}

        //double dQtySupport = 0;
        //if (!double.TryParse(txqtynontrade.Text, out dQtySupport))
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please entry qty usage!','Item Non Trade','warning');", true);
        //    return;
        //}

        //List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@item_cd", cbitemsupport.SelectedValue));
        //arr.Add(new cArrayList("@packing_cd", Request.Cookies["usr_id"].Value));
        //arr.Add(new cArrayList("@uom", lbuomsupport.Text));
        //arr.Add(new cArrayList("@qty", dQtySupport));
        //bll.vInsertRepackingDtlNonTrade(arr);
        //arr.Clear();
        //arr.Add(new cArrayList("@packing_cd", Request.Cookies["usr_id"].Value));

        //bll.vBindingGridToSp(ref grditemsupport, "sp_tlog_repackingdtl_nontrade_get", arr);

        //bool _foundkey = false;


        //List<tlog_repackingsupportusage> _tlog_repackingsupportusage = (List<tlog_repackingsupportusage>)Session["tlog_repackingsupportusage"];

        //foreach (tlog_repackingsupportusage _dtl in _tlog_repackingsupportusage)
        //{
        //    if (_dtl.item_cd == cbitemsupport.SelectedValue)
        //    {
        //        _dtl.qty = Convert.ToDecimal(txqtynontrade.Text);
        //        _foundkey = true;
        //        _dtl.inoutbound = cbinout.SelectedValue;
        //    }
        //}

        //if (!_foundkey)
        //{
        //    _tlog_repackingsupportusage.Add(new tlog_repackingsupportusage
        //    {
        //        item_cd = cbitemsupport.SelectedValue,
        //        item_nm = cbitemsupport.SelectedItem.Text,
        //        qty = _qty,
        //        uom = lbuomsupport.Text,
        //        inoutbound = cbinout.SelectedValue
        //    });
        //}
        //grditemsupport.DataSource = _tlog_repackingsupportusage;
        //grditemsupport.DataBind();

        //Session["tlog_repackingsupportusage"] = _tlog_repackingsupportusage;
        //txqtynontrade.Text = string.Empty;
        //lbuomsupport.Text = string.Empty;
        //cbitemsupport.SelectedValue = string.Empty;
        //lbstocksupportavl.Text = string.Empty;
    }

    protected void grditemsupport_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //HiddenField hditemsupport = (HiddenField)grditemsupport.Rows[e.RowIndex].FindControl("hditemsupport");
        //List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@packing_cd", Request.Cookies["usr_id"].Value));
        //arr.Add(new cArrayList("@item_cd", hditemsupport.Value));
        //bll.vDelRepackingDtlNonTrade(arr);
        //arr.Clear();
        //arr.Add(new cArrayList("@packing_cd", Request.Cookies["usr_id"].Value));
        //List<tlog_repackingsupportusage> _tlog_repackingsupportusage = (List<tlog_repackingsupportusage>)Session["tlog_repackingsupportusage"];
        //_tlog_repackingsupportusage.RemoveAt(e.RowIndex);
        //grditemsupport.DataSource = _tlog_repackingsupportusage;
        //grditemsupport.DataBind();
        //bll.vBindingGridToSp(ref grditemsupport, "sp_tlog_repackingdtl_nontrade_get", arr);
        //Session["tlog_repackingsupportusage"] = _tlog_repackingsupportusage;
    }

    protected void cbitemsupport_SelectedIndexChanged(object sender, EventArgs e)
    {
        //lbstocksupportavl.Text = bll.vLookUp("select dbo.fn_getrepackingstock('" + cbitemsupport.SelectedValue + "','" + Request.Cookies["sp"].Value + "')");
        //lbuomsupport.Text = bll.vLookUp("select uom_base from tmst_itemsupport where item_cd='" + cbitemsupport.SelectedValue + "'");
    }

    protected void btcheckstock_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "PopupCenter('lookupcheckstocksupport.aspx','Check Stock',800,800);", true);
    }

    //protected void cbbin_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    List<cArrayList> arr = new List<cArrayList>();
    //    string sbin = bll.vLookUp("select dbo.fn_getproductwarehouse('" + cbwhs.SelectedValue + "','" + Request.Cookies["sp"].Value + "')");
    //    arr.Add(new cArrayList("@whs_cd", cbwhs.SelectedValue));
    //    bll.vBindingComboToSpWithEmptyChoosen(ref cbbin, "sp_twarehouse_bin_get", "bin_cd", "bin_nm", arr);
    //    cbbin.SelectedValue = sbin;
    //    //DateTime ddate = System.DateTime.ParseExact(dtrepacking.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
    //    //string _stock = bll.vLookUp("select dbo.[sfnGetStockBooking]('" + cbosource.SelectedValue + "','" + cbbin.SelectedValue.ToString() + "','" + cbwhs.SelectedValue.ToString() + "','DEPO','" + ddate.Year + "-" + ddate.Month + "-" + ddate.Day + "')");//by yanto 25-6-2016 
    //    //txstock.Text = _stock;
    //    //cd.v_disablecontrol(cbbin);

    //}

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {

        cbll bll = new cbll();
        SqlDataReader rs = null;
        string sItem = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        List<string> lItem = new List<string>();
        arr.Add(new cArrayList("@item_nm", prefixText));
        bll.vSearchMstItem2(arr, ref rs);
        while (rs.Read())
        {
            sItem = AutoCompleteExtender.CreateAutoCompleteItem(rs["item_cd"].ToString() + " | " + rs["item_nm"].ToString(), rs["item_cd"].ToString());
            lItem.Add(sItem);
        }
        rs.Close();
        return (lItem.ToArray());
    }

    protected void txitemsearch_TextChanged(object sender, EventArgs e)
    {
        //txstockcust.Text = "0";
        //lbprice.Text = "0";
        //lbstock.Text = "0";
    }
    protected void btlookupitem_Click(object sender, EventArgs e)
    {
        //cbuom.SelectedValue = bll.vLookUp("select uom_base from tmst_item where item_cd='" + hditem.Value + "'").ToString();
        cbuom_SelectedIndexChanged(sender, e);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
    }
    protected void cbuom_SelectedIndexChanged(object sender, EventArgs e)
    {
        //*getprice*/();
    }
    protected void cbqty_SelectedIndexChanged(object sender, EventArgs e)
    {
        //*getprice*/();
    }
    protected void cbdocno_SelectedIndexChanged(object sender, EventArgs e)
    {
        //*getprice*/();
    }
    protected void cbuompack_SelectedIndexChanged(object sender, EventArgs e)
    {
        //*getprice*/();
    }

    protected void btbomredyClick(object sender, EventArgs e)
    {
        //Label lbto = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbto");
        //hdto.Value = lbto.Text;
        //lbtod.Text = lbto.Text;
        //List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@so_cd", lbto.Text));
        //List<tloadingorder_dtl> _tloadingorder_dtl = bll.ltloadingorder_dtl(arr);
        //grdload.DataSource = _tloadingorder_dtl;
        //grdload.DataBind();
        //Session["tloadingorder_dtl"] = _tloadingorder_dtl;
        //cd.v_showcontrol(btprintapprove);
        //cd.v_showcontrol(btnew);

        decimal _inqty = 0;
        decimal _outqty = 0;
        decimal _unitqty = 0;
        if (!decimal.TryParse(txqtypack.Text, out _inqty))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Qty item must define','Qty','warning');", true);
            return;
        }

        if (_inqty == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Qty can not zero','Qty','warning');", true);
            return;
        }

        //Label lbto = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbto");
        //hdto.Value = lbto.Text;
        //lbtod.Text = lbto.Text;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@bom_cd", cbosource.SelectedValue));
        arr.Add(new cArrayList("@in_qty", txqtypack.Text));
        //List<tlog_titem_processingdtl> _tlog_titem_processingdtl = bll.tlog_titem_processingdtl(arr);
        //grd.DataSource = _tlog_titem_processingdtl;
        //grd.DataBind();
        //Session["tlog_titem_processingdtl"] = _tlog_titem_processingdtl;
        //decimal _unitqty = Convert.ToDecimal(bll.vLookUp("select unit_item from tbom_detail where bom_cd='" + hdbom.Value.ToString() + "'")),

        //List<tlog_titem_processingdtl> _tlog_titem_processingdtl = (List<tlog_titem_processingdtl>)Session["tlog_titem_processingdtl"];
        //_tlog_titem_processingdtl.Add(new tlog_titem_processingdtl
        //{
        //    item_cd = bll.vLookUp("select item_compose from tbom_detail where bom_cd='" + cbosource.SelectedValue + "'"),
        //    item_nm = bll.vLookUp("select item_nm from tmst_item where item_cd='" + cbosource.SelectedValue + "'"),
        //    //size = bll.vLookUp("select size from tmst_item where item_cd='" + cbosource.SelectedValue + "'"),
        //    //bin_cd = cbbin.SelectedValue,
        //    //whs_cd = cbwhs.SelectedValue,
        //    in_qty = _inqty,
        //    out_qty = _outqty,
        //    unit_qty = Convert.ToDecimal(bll.vLookUp("select unit_item from tbom_detail where bom_cd='" + cbosource.SelectedValue + "'")),
        //    //fromto = cbfromto.SelectedValue,
        //    uom = cbuompack.SelectedValue,
        //    //fromto_nm = bll.vLookUp("select fld_desc from tfield_value where fld_nm='fromto' and fld_valu='" + cbfromto.SelectedValue + "'")
        //});

        //grd.DataSource = _tlog_titem_processingdtl;
        //grd.DataBind();

        //cbfromto.SelectedValue = string.Empty;
        //txstock.Text = string.Empty;
        //cbosource.Items.Clear();
        //txqty.Text = string.Empty;
        //cbuom.Items.Clear();
        //cbwhs.Items.Clear();
        //cbbin.Items.Clear();
        //cbuom.SelectedValue = string.Empty;
        //cbwhs.SelectedValue = string.Empty;
        //cbbin.SelectedValue = string.Empty;
        //btsave.Style.Add("display", "normal");
        cd.v_disablecontrol(txqtypack);
        cd.v_showcontrol(btsave);
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList1(string prefixText, int count, string contextKey)
    {
        HttpCookie cook;
        cook = HttpContext.Current.Request.Cookies["sp"];
        System.Data.SqlClient.SqlDataReader rs = null;
        cbll bll = new cbll();
        List<string> lcust = new List<string>();
        string scust = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@do_no", prefixText));
        arr.Add(new cArrayList("@salespointcd", cook.Value));
        //bll.vSearchStatusdocall(ref rs, arr);
        //while (rs.Read())
        //{
        //    scust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["do_desc"].ToString(), rs["do_no"].ToString());
        //    lcust.Add(scust);
        //}
        //rs.Close();
        return (lcust.ToArray());
    }
    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Label lbitemcode = (Label)grd.Rows[e.RowIndex].FindControl("lbitemcode");
        Label lbstockavl = (Label)grd.Rows[e.RowIndex].FindControl("lbstockavl");
        Label lboutqty = (Label)grd.Rows[e.RowIndex].FindControl("lboutqty");
        TextBox txqty = (TextBox)grd.Rows[e.RowIndex].FindControl("txqty");
        TextBox txtallysheet = (TextBox)grd.Rows[e.RowIndex].FindControl("txtallysheet");

        decimal _tallysheet = 0; decimal dQtystockavl = 0; decimal dQtytally = 0; decimal dQty = 0;
        if (!decimal.TryParse(txtallysheet.Text, out _tallysheet))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please entry on decimal for actual qty tally','Actual qty tally','warning');", true);
            return;
        }

        /////
        ///
        if (!decimal.TryParse(lboutqty.Text, out dQty))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Qty must numeric','check order qty','warning');", true);
            return;
        }

        if (!decimal.TryParse(lbstockavl.Text, out dQtystockavl))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Qty must numeric','check order qty','warning');", true);
            return;
        }

        if (!decimal.TryParse(txtallysheet.Text, out dQtytally))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Qty tally sheet must numeric','check tally sheet qty','warning');", true);
            return;
        }



        if (dQty > dQtystockavl)
        {
            string _item_cd = lbitemcode.Text;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Qty Pack Out can not bigger than stock available','Check Qty Pack Out and stock available and item code " + _item_cd + "','warning');", true);
            return;
        }

        if (dQty > dQtytally)
        {
            string _item_cd = lbitemcode.Text;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Qty Tally sheet can not less than Out qty item','Check Qty Tally sheet and item code " + _item_cd + "','warning');", true);
            return;
        }



        List<tlog_titem_processingdtl> _tlog_titem_processingdtl = (List<tlog_titem_processingdtl>)Session["tlog_titem_processingdtl"];
        foreach (tlog_titem_processingdtl _dtl in _tlog_titem_processingdtl)
        {
            if (_dtl.item_cd == lbitemcode.Text)
            {
                _dtl.qty_tally = _tallysheet;
                //_dtl.tallysheet = _tallysheet;
                //_dtl.adjustvalue = _tallysheet - (_dtl.qty - _dtl.susut);
            }
        }

        grd.EditIndex = -1;
        grd.DataSource = _tlog_titem_processingdtl;
        grd.DataBind();

        Session["tlog_titem_processingdtl"] = _tlog_titem_processingdtl;

    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        List<tlog_titem_processingdtl> _tlog_titem_processingdtl = (List<tlog_titem_processingdtl>)Session["tlog_titem_processingdtl"];
        grd.EditIndex = e.NewEditIndex;
        grd.DataSource = _tlog_titem_processingdtl;
        grd.DataBind();

        Session["tlog_titem_processingdtl"] = _tlog_titem_processingdtl;
    }
    protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grd.EditIndex = -1;
        vBindingGrid();
    }
    void vBindingGrid()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@bom_cd", cbosource.SelectedValue));
        arr.Add(new cArrayList("@in_qty", txqtypack.Text));
        //List<tlog_titem_processingdtl> _tlog_titem_processingdtl = bll.tlog_titem_processingdtl(arr);
        //grd.DataSource = _tlog_titem_processingdtl;
        //grd.DataBind();
        //Session["tlog_titem_processingdtl"] = _tlog_titem_processingdtl;
    }
}

//public class tlog_repackingsupportusage
//{
//    public string item_cd { set; get; }
//    public decimal qty { set; get; }
//    public string uom { set; get; }
//    public string item_nm { set; get; }
//    public string salespointcd { set; get; }
//    public string inoutbound { set; get; }
//}