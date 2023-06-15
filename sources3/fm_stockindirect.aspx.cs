using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.IO;
using Newtonsoft.Json;
using System.Net;

public partial class fm_stockindirect : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingFieldValueToComboWithChoosen(ref cbuom, "uom");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
            bll.vBindingComboToSpWithEmptyChoosen(ref cbwhs, "sp_tmst_warehouse_get", "whs_cd", "whs_nm", arr);
            cbwhs_SelectedIndexChanged(sender, e);
            arr.Clear();
            arr.Add(new cArrayList("@stockin_no", Request.Cookies["usr_id"].Value));
            bll.vDeleteStockInDtl(arr);
            dtstockin.Text = Request.Cookies["waz_dt"].Value;
           
            dtexp_CalendarExtender.StartDate = System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            dtexp.Text = Request.Cookies["waz_dt"].Value;
            dtprod.Text = Request.Cookies["waz_dt"].Value;
            bll.vBindingFieldValueToComboWithChoosen(ref cbstockinstatus, "stockin_sta_id");
            bll.vBindingComboToSpWithEmptyChoosen(ref cbsalespoint, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm");
            cbsalespoint.SelectedValue = Request.Cookies["sp"].Value;
            //arr.Clear();
            //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
            //arr.Add(new cArrayList("@qry_cd", "vendor_jamur"));
            //bll.vBindingComboToSpWithEmptyChoosen(ref cbsupplier, "sp_tmst_vendor_getbyjamur", "vendor_cd", "vendor_desc", arr);
            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
            //bll.vBindingComboToSpWithEmptyChoosen(ref cbsupplier, "sp_tmst_vendor_getbysalespoint", "vendor_cd", "vendor_nm", arr);
            hditem.Value = string.Empty;
            List<tstockindirect_dtl> _tstockindirect_dtl = new List<tstockindirect_dtl>();
            //lbsusutpercentage.Text = "0 % (Tidak ada susut)";
            Session["tstockindirect_dtl"] = _tstockindirect_dtl;

            cd.v_hiddencontrol(btsave);
            cd.v_hiddencontrol(btprint);
            cd.v_disablecontrol(cbsalespoint);
            cd.v_disablecontrol(cbwhs);
            cd.v_disablecontrol(cbbin);
            cd.v_disablecontrol(cbuom);
            cd.v_disablecontrol(txreference);
            cd.v_disablecontrol(txremark);
            cd.v_disablecontrol(dtstockin);
            cd.v_disablecontrol(txitemsearch);
            cd.v_disablecontrol(dtexp);
            cd.v_disablecontrol(dtprod);
            cd.v_disablecontrol(txpono);
            cd.v_disablecontrol(txqty);
            cd.v_disablecontrol(cbsupplier);
        }
    }

    public void vBindingGrid()
    {
        string _url = @"https://nav.transworld.com.eg/api/nav_do/header.php?do_no=All";
        using (WebClient wc = new WebClient())
        {
            try
            {
                wc.BaseAddress = _url;
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                // var _data = wc.DownloadString("/PO/Getbystatus?sta=" + cbstatus.SelectedValue.ToString() + "&sp=" + cbbranch.SelectedValue.ToString());
                var _data = wc.DownloadString(_url);
                if (!_data.Equals(null))
                {
                    List<tgdn_header_nav> json = JsonConvert.DeserializeObject<List<tgdn_header_nav>>(_data);
                    Session["tgdn_header_nav"] = json;
                    
                    

                    foreach(tgdn_header_nav _hdr in json)
                    {
                        System.Web.UI.WebControls.ListItem _item = new System.Web.UI.WebControls.ListItem();
                        _item.Text = _hdr.DO_No;
                        _item.Value = _hdr.DO_No;
                        cbgdn.Items.Add(_item);
                    }
                }
            }
            catch (Exception ex)
            {
                bll.vHandledError(ref ex, "PO Branch");
            }
        }
    }

    protected void btadd_Click(object sender, EventArgs e)
    {
        if (txremark.Text == String.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Remark must be filled!','Remark','success');", true);
            return;
        }
        if (dtstockin.Text == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Stock In date can not empty','Stock In Date','warning');", true);
            return;
        }

        if (cbbin.SelectedValue == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select bin','To get stock','warning');", true);
            return;
        }

        decimal _qty = 0;

        //if (!decimal.TryParse(txqty.Text, out _qty))
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please enter QTY','Qty to stock in ','warning');", true);
        //    return;
        //}

        if (!string.IsNullOrEmpty(dtexp.Text) || (!string.IsNullOrEmpty(dtprod.Text)))
        {
            if (string.IsNullOrEmpty(dtexp.Text))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Expiration date must choice!','Expiration Date','warning');", true);
                return;
            }
            if (string.IsNullOrEmpty(dtprod.Text))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Production date must choice!','Prod Date','warning');", true);
                return;
            }

            List<tstockindirect_dtl> _tstockindirect_dtl = (List<tstockindirect_dtl>)Session["tstockindirect_dtl"];
            _tstockindirect_dtl.Add(new tstockindirect_dtl
            {
                item_cd = hditem.Value,
                bin_cd = cbbin.SelectedValue,
                whs_cd = cbwhs.SelectedValue,
                exp_dt = System.DateTime.ParseExact(dtexp.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                prod_dt = System.DateTime.ParseExact(dtprod.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                qty = Convert.ToDecimal(txqty.Text),
                stock_avl = Convert.ToDecimal(lbstock.Text),
                //susut = Convert.ToDecimal(lbsusut.Text),
                //tallysheet = ((cbstockinstatus.SelectedValue == "C") ? Convert.ToDecimal(txqty.Text): 0),
                uom = cbuom.SelectedValue,
                item_nm = bll.vLookUp("select item_nm from tmst_item where item_cd='" + hditem.Value + "'"),
                //adjustvalue = ((cbstockinstatus.SelectedValue == "C")? (Convert.ToDecimal(txtallysheet.Text) - (Convert.ToDecimal(txqty.Text) - Convert.ToDecimal(lbsusut.Text))):0),
                //unitprice = Convert.ToDecimal(hdunitprice.Value)
                tallysheet = 0,
               
            }) ;

            grd.DataSource = _tstockindirect_dtl;
            grd.DataBind();


            //List<cArrayList> arr = new List<cArrayList>();
            //arr.Add(new cArrayList("@stockin_no", Request.Cookies["usr_id"].Value));
            //arr.Add(new cArrayList("@item_cd", hditem.Value));
            //arr.Add(new cArrayList("@qty", txqty.Text));
            //arr.Add(new cArrayList("@uom", cbuom.SelectedValue));
            //arr.Add(new cArrayList("@whs_cd", cbwhs.SelectedValue));
            //arr.Add(new cArrayList("@bin_cd", cbbin.SelectedValue));
            //arr.Add(new cArrayList("@unitprice", hdunitprice.Value));
            //if (!string.IsNullOrEmpty(dtexp.Text) || (!string.IsNullOrEmpty(dtprod.Text)))
            //{
            //    if (string.IsNullOrEmpty(dtexp.Text))
            //    {
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Expiration date must choice!','Expiration Date','warning');", true);
            //        return;
            //    }
            //    if (string.IsNullOrEmpty(dtprod.Text))
            //    {
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Production date must choice!','Prod Date','warning');", true);
            //        return;
            //    }
            //    arr.Add(new cArrayList("@exp_dt", DateTime.ParseExact(dtexp.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            //    arr.Add(new cArrayList("@prod_dt", DateTime.ParseExact(dtprod.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            //}
            //bll.vInsertStockInDirectDtl(arr);
            //arr.Clear();
            //arr.Add(new cArrayList("@stockin_no", Request.Cookies["usr_id"].Value));
            //bll.vBindingGridToSp(ref grd, "sp_tstockindirect_dtl_get", arr);
            txitemsearch.Text = string.Empty;
            hditem.Value = string.Empty;
            //txqty.Text = string.Empty;
            //lbstock.Text = string.Empty;
            hdunitprice.Value = string.Empty;
            dtexp.Text = Request.Cookies["waz_dt"].Value;
            dtprod.Text = Request.Cookies["waz_dt"].Value;
            txqty.Text = string.Empty;
            //lbsusut.Text = string.Empty;
            cbuom.SelectedValue = string.Empty;
            cbwhs.SelectedValue = string.Empty;
            cbbin.SelectedValue = string.Empty;
            //lbpctsusut.Text = string.Empty;
            Session["tstockindirect_dtl"] = _tstockindirect_dtl;
            cd.v_disablecontrol(cbsupplier);

            cd.v_disablecontrol(cbstockinstatus);
            cd.v_showcontrol(btsave);
            cd.v_disablecontrol(txremark);
           

        }
    }



    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetItemList(string prefixText, int count, string contextKey)
    {
        HttpCookie cook;
        cook = HttpContext.Current.Request.Cookies["sp"];
        cbll bll = new cbll();
        List<string> lItem = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string sItem = string.Empty;
        System.Data.SqlClient.SqlDataReader rs = null;
        arr.Add(new cArrayList("@salespointcd", cook.Value.ToString()));
        arr.Add(new cArrayList("@item_cd", prefixText));
        bll.vSearchMstItemBySalespoint(arr, ref rs);
        while (rs.Read())
        {
            sItem = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["item_cd"].ToString() + "-" + rs["item_nm"].ToString() + "-" + rs["size"].ToString() + "-" + rs["branded_nm"].ToString(), rs["item_cd"].ToString());
            lItem.Add(sItem);

        }
        rs.Close();
        return (lItem.ToArray());
    }

    protected void btrefresh_Click(object sender, EventArgs e)
    {
        //if (cbsupplier.SelectedValue == string.Empty)
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select supplier !','For determine SUSUT','warning');", true);
        //    txqty.Text = string.Empty;
        //    txitemsearch.Text = string.Empty;
        //    hditem.Value = string.Empty;
        //    return;
        //}
        //bll.vBindingFieldValueToCombo(ref cbuom, "uom");
        //string sUomBase = bll.vLookUp("select uom_base from tmst_item where item_cd='" + hditem.Value + "'");
        //hdunitprice.Value = bll.vLookUp("select dbo.fn_checkpricechannel('" + hditem.Value + "','BRN')");
        //string sWarehouse = bll.vLookUp("select dbo.fn_getproductwarehouse('" + hditem.Value + "','" + Request.Cookies["sp"].Value + "')");
        //cbuom.SelectedValue = sUomBase;

        //// Stock Available
        //DateTime ddate = System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //string sStock = bll.vLookUp("select dbo.[sfnGetStockBooking]('" + hditem.Value.ToString() + "','" + cbbin.SelectedValue.ToString() + "','" + cbwhs.SelectedValue.ToString() + "','DEPO','" + ddate.Year + "-" + ddate.Month + "-" + ddate.Day + "')");//by yanto 25-6-2016 
        //if (string.IsNullOrEmpty(sStock))
        //{
        //    sStock = "0";
        //}
        //lbstock.Text = sStock;
        ////cbuom.CssClass = "form-control ro";
        //string _pctsusut = bll.vLookUp("select pctsusut from tmap_susut where item_cd='" + hditem.Value + "' and vendor_cd='"+cbsupplier.SelectedValue+"' and salespointcd='"+Request.Cookies["sp"].Value+"'");
        //if (_pctsusut == string.Empty)
        //{
        //    _pctsusut = "0";
        //}
        ////lbpctsusut.Text = _pctsusut;
        //cbwhs.SelectedValue = sWarehouse;
        //cbwhs_SelectedIndexChanged(sender, e);
        cd.v_enablecontrol(cbwhs);
        cd.v_enablecontrol(cbuom);
        cd.v_enablecontrol(txqty);


    }

    protected void cbwhs_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@whs_cd", cbwhs.SelectedValue));
        bll.vBindingComboToSpWithEmptyChoosen(ref cbbin, "sp_twarehouse_bin_get", "bin_cd", "bin_nm", arr);
    }

    protected void btsave_Click(object sender, EventArgs e)
    {
        if (cbstockinstatus.SelectedValue == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select stock type!','Reguler or In Transit','warning');", true);
            return;
        }
        if (grd.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Item stock in is not selected','Select item to be stocked in','warning');", true);
            return;
        }
        if (txreference.Text == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Reference must filled','It may from GDN Factory number','warning');", true);
            return;
        }

        if (txremark.Text == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Remark can not empty','Please fill informatio for received','warning');", true);
            return;
        }

        if (txpono.Text == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please put PO Number','PO Number linked with Purchasing','warning');", true);
            return;
        }

        if (cbstockinstatus.SelectedValue == "C")
        {
            //if (!fu_tallysheet.HasFile)
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please upload file tallysheet','Tallysheet file','warning');", true);
            //    return;
            //}
        }

        //if (cbsupplier.SelectedValue == string.Empty)
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select supplier!','to determine susut','warning');", true);
        //    return;
        //}

        string sStockNo = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@stockin_dt", System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value));
        arr.Add(new cArrayList("@refno", txreference.Text));
        arr.Add(new cArrayList("@remark", txremark.Text));
        arr.Add(new cArrayList("@stockin_sta_id", cbstockinstatus.SelectedValue));
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue));
        arr.Add(new cArrayList("@po_no", txpono.Text));
        arr.Add(new cArrayList("@vendor_cd", cbsupplier.SelectedValue));
       
        bll.vInsertStockInDirect(arr, ref sStockNo);
        txstockinno.Text = sStockNo;
        
        if (cbstockinstatus.SelectedValue == "C")
        {
            //FileInfo _fileinfo = new FileInfo(fu_tallysheet.FileName);
            //string _extention = _fileinfo.Extension;

            //string _filename = "TLY" + sStockNo + _extention;

            //string _filenamefull = bll.sGetControlParameter("image_path") + _filename;
            //fu_tallysheet.SaveAs(_filenamefull);

            //string _sql = "update tstockin_direct set file_nm='"+_filename+"' where stockin_no='" + sStockNo + "'";
            //bll.vExecuteSQL(_sql);

        }
        List<tstockindirect_dtl> _tstockindirect_dtl = (List<tstockindirect_dtl>)Session["tstockindirect_dtl"];

        foreach (tstockindirect_dtl _tst in _tstockindirect_dtl)
        {
            arr.Clear();
            arr.Add(new cArrayList("@stockin_no", sStockNo));
            arr.Add(new cArrayList("@item_cd", _tst.item_cd));
            arr.Add(new cArrayList("@unitprice", _tst.unitprice));
            arr.Add(new cArrayList("@qty", _tst.qty));
            arr.Add(new cArrayList("@uom", _tst.uom));
            arr.Add(new cArrayList("@whs_cd", _tst.whs_cd));
            arr.Add(new cArrayList("@bin_cd", _tst.bin_cd));
            arr.Add(new cArrayList("@exp_dt", _tst.exp_dt));
            arr.Add(new cArrayList("@prod_dt", _tst.prod_dt));
            arr.Add(new cArrayList("@tallysheet", _tst.tallysheet));
            arr.Add(new cArrayList("@adjustvalue", _tst.adjustvalue));
            arr.Add(new cArrayList("@susut", _tst.susut));
            bll.vInsertStockInDirectDtl(arr);
        }
        
        
        cd.v_hiddencontrol(btsave);
        cd.v_showcontrol(btprint);
        cd.v_disablecontrol(txpono);
        cd.v_disablecontrol(txremark);
        cd.v_disablecontrol(txreference);
        cd.v_disablecontrol(grd);
        cd.v_hiddencontrol(btadd);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Stock in successfully created','" + sStockNo + "','success');", true);
    }

    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_reportx.aspx?src=stockindirect&s=" + txstockinno.Text + "');", true);
    }

    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_stockindirect.aspx");
    }

    protected void cbbin_SelectedIndexChanged(object sender, EventArgs e)
    {
        DateTime ddate = System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //string sStock = bll.vLookUp("select dbo.fn_checkstock('"+hditem.Value+"','"+cbwhs.SelectedValue+"','"+cbbin.SelectedValue+"')");  // bll.vLookUp("select dbo.[sfnGetStockBooking]('" + hditem.Value.ToString() + "','" + cbbin.SelectedValue.ToString() + "','" + cbwhs.SelectedValue.ToString() + "','DEPO','" + ddate.Year + "-" + ddate.Month + "-" + ddate.Day + "')");//by yanto 25-6-2016 
        //if (string.IsNullOrEmpty(sStock))
        //{
        //    sStock = "0";
        //}
        //lbstock.Text = sStock;
    }

    protected void cbstockinstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbstockinstatus.SelectedValue != string.Empty)
        {
            cd.v_enablecontrol(dtexp);
            cd.v_enablecontrol(txitemsearch);
            cd.v_enablecontrol(txreference);
            cd.v_enablecontrol(txstockinno);
            cd.v_enablecontrol(txitemsearch);
            //cd.v_enablecontrol(txqty);
            cd.v_enablecontrol(dtprod);
            cd.v_enablecontrol(cbuom);
            cd.v_enablecontrol(cbwhs);
            cd.v_enablecontrol(cbbin);
            cd.v_enablecontrol(txremark);
            cd.v_enablecontrol(txpono);
            cd.v_enablecontrol(cbsupplier);
            //cd.v_enablecontrol(cbsupplier);
            cd.v_disablecontrol(cbstockinstatus);

            if (cbstockinstatus.SelectedValue == "C")
            {
                cd.v_enablecontrol(txqty);
                vBindingGrid();
            }
            else { cd.v_disablecontrol(txqty);
                cd.v_disablecontrol(txqty);
                
            }
        }
        else
        {
            cd.v_disablecontrol(dtexp);
            cd.v_disablecontrol(txitemsearch);
            cd.v_disablecontrol(txreference);
            cd.v_disablecontrol(txstockinno);
            cd.v_disablecontrol(txitemsearch);
            //cd.v_disablecontrol(txqty);
            cd.v_disablecontrol(dtprod);
            cd.v_disablecontrol(cbuom);
            cd.v_disablecontrol(cbwhs);
            cd.v_disablecontrol(cbbin);
            cd.v_disablecontrol(txremark);
            cd.v_disablecontrol(txpono);
            //cd.v_disablecontrol(cbsupplier);
            cd.v_disablecontrol(txqty);
            cd.v_disablecontrol(cbstockinstatus);
            cd.v_disablecontrol(cbsupplier);
        }
    }

    protected void cbsupplier_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbaddress.Font.Italic = true;
        lbaddress.Font.Underline = true;
        lbaddress.ForeColor = System.Drawing.Color.Blue;
        lbaddress.Text = bll.vLookUp("select address1 from tmst_vendor where vendor_cd='" + cbsupplier.SelectedValue + "'");
    }

    protected void btchanged_Click(object sender, EventArgs e)
    {
        if (cbsupplier.SelectedValue == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select supplier !','For determine SUSUT','warning');", true);
            //txqty.Text = string.Empty;
            return;
        }

        decimal _qty = 0;
        //if (!decimal.TryParse(txqty.Text, out _qty))
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please correct value for qty !','Enter qty properly','warning');", true);
        //    return;
        //}

        decimal _susut = 0;decimal _susutvalue = 0;
        string _temp = string.Empty;
        //_temp = bll.vLookUp("select pctsusut from tmap_susut where item_cd='" + hditem.Value + "' and vendor_cd='" + cbsupplier.SelectedValue + "'");

        //if (_temp == string.Empty)
        //{ _temp = "0"; }

        //_susut = Convert.ToDecimal(lbpctsusut.Text);

        _susutvalue = _qty * (_susut / 100);

        //lbsusut.Text = _susutvalue.ToString();
        //if (cbsupplier.SelectedValue == string.Empty)
        //{
        //    _susut = 0;lbsusutpercentage.Text = "0 % (Tidak ada susut)";
        //}
        //else
        //{
        //    lbsusutpercentage.Text = bll.vLookUp("select qry_data from tmap_query where qry_cd='vendor_jamur' and salespointcd='"+Request.Cookies["sp"].Value+"'");
        //    if (lbsusutpercentage.Text == string.Empty)
        //    {
        //        lbsusutpercentage.Text = "0 % (TIdak ada susut)";
        //        _susut = 0;
        //    }
        //    else
        //    {
        //        _susut = Convert.ToDecimal(bll.vLookUp("select qry_data from tmap_query where qry_cd='" + cbsupplier.SelectedValue + "' and salespointcd='" + Request.Cookies["sp"].Value + "'"));
        //    }
        //}
        _susutvalue = _qty * (_susut/100);
        //lbsusut.Text = _susutvalue.ToString();

        cd.v_disablecontrol(cbsupplier);
        
    }

    protected void txqty_TextChanged(object sender, EventArgs e)
    {
        btchanged_Click(sender, e);
    }
}

