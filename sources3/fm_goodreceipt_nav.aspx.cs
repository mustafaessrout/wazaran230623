using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Net;
using Newtonsoft.Json;

public partial class fm_goodreceipt_nav : System.Web.UI.Page
{
    cbll bll = new cbll();
    decimal totalqtyshipment = 0, totalqtyreceived=0;
    decimal totalqty = 0, totalqty_received = 0;



    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {

                string sreceipt_no = "", ssalespointcd, sdo_no, sinvoice_no, sdo_dt, sreceipt_dt, sdo_sta_id, sdo_sta_nm, swhs_cd, sfrom;
                DateTime dtreceipt_dt;
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vBindingComboToSp(ref cbwarehouse, "sp_tmst_warehouse_get", "whs_cd", "whs_nm", arr);
                dtgoodreceipt.Text = Request.Cookies["waz_dt"].Value.ToString();
                //bll.sFormat2ddmmyyyy(ref dtgoodreceipt);
                sreceipt_no = Request.QueryString["receipt_no"];

                txgoodreceipt.Text = "NEW";
                if (!string.IsNullOrWhiteSpace(Request.QueryString["receipt_no"]))
                {

                    string doType = sreceipt_no.Substring(0, 2);
                    //ssalespointcd=Request.QueryString["salespointcd"];
                    ssalespointcd = Request.Cookies["sp"].Value.ToString();
                    sdo_no = bll.vLookUp("select do_no from tmst_goodreceipt where receipt_no='" + sreceipt_no + "' and salespointcd='" + ssalespointcd + "'");
                    sreceipt_dt = bll.vLookUp("select FORMAT(CONVERT(date, receipt_dt),'d/M/yyyy') from tmst_goodreceipt where receipt_no='" + sreceipt_no + "' and salespointcd='" + ssalespointcd + "'");
                    swhs_cd = bll.vLookUp("select top 1 whs_cd from tgoodreceipt_dtl where receipt_no='" + sreceipt_no + "' and salespointcd='" + ssalespointcd + "'");

                    if (doType == "DO")
                    {
                        sdo_dt = bll.vLookUp("select FORMAT(CONVERT(date, do_dt),'d/M/yyyy') from tmst_do where do_no='" + sdo_no + "' and salespointcd='" + ssalespointcd + "'");
                        sinvoice_no = bll.vLookUp("select invoice_no from tmst_do where do_no='" + sdo_no + "' and salespointcd='" + ssalespointcd + "'");
                        sdo_sta_id = bll.vLookUp("select do_sta_id from tmst_do where do_no='" + sdo_no + "' and salespointcd='" + ssalespointcd + "'");
                        sdo_sta_nm = bll.vLookUp("select fld_desc from tfield_value where fld_nm='do_sta_id' and fld_valu='" + sdo_sta_id + "'");
                        sfrom = bll.vLookUp("(select a.whs_nm from tmst_warehouse a join tmst_do b on a.whs_cd = b.whs_cd where b.do_no='" + sdo_no + "' and b.salespointcd='" + ssalespointcd + "')");
                    }
                    else //if (doType == "TF")
                    {
                        sdo_dt = bll.vLookUp("select FORMAT(CONVERT(date, trf_dt),'d/M/yyyy') from tinternal_transfer where trf_no='" + sdo_no + "' and salespointcd='" + ssalespointcd + "'");
                        sinvoice_no = "";
                        sdo_sta_id = bll.vLookUp("select sta_id from tinternal_transfer where trf_no='" + sdo_no + "' and salespointcd='" + ssalespointcd + "'");
                        sdo_sta_nm = bll.vLookUp("select fld_desc from tfield_value where fld_nm='sta_id' and fld_valu='" + sdo_sta_id + "'");
                        sfrom = bll.vLookUp("(select a.salespoint_nm from tmst_salespoint a join tinternal_transfer b on a.salespointcd=b.salespointcd where b.trf_no='" + sdo_no + "' and b.salespointcd='" + ssalespointcd + "')");
                    }


                    //dtreceipt_dt = DateTime.ParseExact(sreceipt_dt, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                    //sreceipt_dt = dtreceipt_dt.ToString("d/M/yyyy");
                    txgoodreceipt.Text = sreceipt_no;
                    arr.Clear();
                    arr.Add(new cArrayList("@receipt_no", sreceipt_no));
                    arr.Add(new cArrayList("@salespointcd", ssalespointcd));
                    bll.vBindingGridToSp(ref grddtl, "sp_tgoodreceipt_dtl_get", arr);

                    txgoodreceipt.Text = sreceipt_no;
                    dtgoodreceipt.Text = sreceipt_dt;
                    txdosearch.Text = sdo_no + " - " + sdo_dt;

                    lbdodate.Text = sdo_dt;
                    //lbdodate.Text = Convert.ToDateTime(sdo_dt).ToShortDateString();
                    lbinvoiceno.Text = sinvoice_no;
                    lbdostaid.Text = sdo_sta_nm;
                    txfrom.Text = sfrom;
                    cbwarehouse.SelectedValue = swhs_cd;
                    btsave.Visible = false;
                }
                else
                {
                    string _url = bll.sGetControlParameter("api_external");
                    string _url_do_header = bll.sGetFieldValue("api_do", "api_do_all") + Request.Cookies["sp"].Value.ToString();
                    List<do_header> json_filter = new List<do_header>();
                    using (WebClient wc = new WebClient())
                    {
                        try
                        {
                           // wc.BaseAddress = _url;
                            ServicePointManager.ServerCertificateValidationCallback += (senderX, certificate, chain, sslPolicyErrors) => true;
                            var _data = wc.DownloadString(_url + _url_do_header);
                            if (!_data.Equals(null))
                            {
                                List<do_header> json = JsonConvert.DeserializeObject<List<do_header>>(_data);
                                foreach (var jf in json)
                                {
                                    if (jf.do_branch_code == Request.Cookies["sp"].Value)
                                    {
                                        json_filter.Add(jf);
                                    }
                                }
                                Session["do_header"] = json_filter;
                            }else
                            {
                                Response.Redirect("fm_goodreceipt.aspx?err=fm_goodreceipt_nav");
                            }
                        }
                        catch (WebException ex)
                        {
                            if (ex.Status == WebExceptionStatus.ProtocolError && ex.Response != null)
                            {
                                switch (((HttpWebResponse)ex.Response).StatusCode)
                                {
                                    case HttpStatusCode.NotFound:
                                        Response.Redirect("fm_goodreceipt.aspx?err=fm_goodreceipt_nav", false);
                                        break;
                                    case HttpStatusCode.GatewayTimeout:
                                        Response.Redirect("fm_goodreceipt.aspx?err=fm_goodreceipt_nav", false);
                                        break;
                                    case HttpStatusCode.RequestTimeout:
                                        Response.Redirect("fm_goodreceipt.aspx?err=fm_goodreceipt_nav", false);
                                        break;
                                    default:
                                        Response.Redirect("fm_goodreceipt.aspx?err=fm_goodreceipt_nav", false);
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_goodreceipt_nav");
                Response.Redirect("fm_ErrorPage.aspx");
            }
        }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        HttpCookie cook; 
        cook = HttpContext.Current.Request.Cookies["sp"];
        System.Data.SqlClient.SqlDataReader rs = null;
        cbll bll = new cbll();
        string sDo = string.Empty;
        List<string> lDo = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@do_no", prefixText));
        arr.Add(new cArrayList("@do_sta_id", "D"));
        arr.Add(new cArrayList("@salespointcd", cook.Value.ToString()));
        bll.vSearchMstDO(arr, ref rs);
        while (rs.Read())
        { 
            sDo = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["do_no"].ToString() + " - " + rs["do_dt"].ToString(), rs["do_no"].ToString());
            lDo.Add(sDo);
        } rs.Close();


        string _check = "";
        
        List<do_header> _do_header = (List<do_header>)HttpContext.Current.Session["do_header"];
        if (!_do_header.Equals(null))
        {
            foreach (do_header _dt in _do_header)
            {
                _check = bll.vLookUp("select top 1 do_no from tmst_do where do_sta_id not in ('E') and docref_no = '" + _dt.do_no + "'");
                if (_check == "")
                {
                    sDo = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(_dt.do_no + " - " + _dt.shipment_date, _dt.do_no);
                    lDo.Add(sDo);
                }
                _check = "";
            }
        }
        

        return (lDo.ToArray());
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        try
        {
            System.Data.SqlClient.SqlDataReader rs = null;
            List<cArrayList> arr = new List<cArrayList>();

            if (hddo.Value.ToString() != "")
            {
                string sDoStaID = "";
                string sDOType = "";
                sDOType = hddo.Value.ToString().Substring(0, 2);

                if (sDOType == "DO" || sDOType == "TF")
                {
                    arr.Clear();
                    arr.Add(new cArrayList("@do_no", hddo.Value.ToString()));
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    //  arr.Add(new cArrayList("@do_sta_id", "D"));
                    bll.vGetMstDOAll(arr, ref rs);
                    while (rs.Read())
                    {
                        lbdodate.Text = Convert.ToDateTime(rs["do_dt"]).ToShortDateString();
                        //bll.sFormat2ddmmyyyy(ref lbdodate);
                        lbinvoiceno.Text = rs["invoice_no"].ToString();
                        lbdostaid.Text = rs["do_sta_nm"].ToString();
                        sDoStaID = rs["do_sta_id"].ToString();
                        txgdn.Text = rs["docref_no"].ToString();
                        txfrom.Text = rs["from_nm"].ToString();
                    }
                    rs.Close();
                    totalqtyshipment = 0;
                    totalqtyreceived = 0;
                    bll.vBindingGridToSp(ref grd, "sp_tdo_dtl_get", arr);
                    cbwarehouse_SelectedIndexChanged(sender, e);

                    string customer = "", typeCustomer = ""; string[] cust_cd;
                    customer = bll.vLookUp("select b.to_nm from tmst_do a left join tmst_po b on a.po_no = b.po_no where a.do_no = '" + hddo.Value.ToString() + "' and a.salespointcd='" + Request.Cookies["sp"].Value.ToString() + "' and b.delivery_typ = 'CU' ");
                    if (customer != "")
                    {
                        cust_cd = customer.Replace(" ", "").Split('-');
                        typeCustomer = bll.vLookUp("select cuscate_cd from tmst_customer where cust_cd='" + cust_cd[0] + "'");
                        if (typeCustomer == "SP")
                        {
                            foreach (GridViewRow gr in grd.Rows)
                            {
                                DropDownList cbbin = (DropDownList)gr.FindControl("cbdestination");
                                cbbin.SelectedValue = "NES";
                                cbbin.Enabled = false;
                            }
                            foreach (GridViewRow gr in grd_nav.Rows)
                            {
                                DropDownList cbbin = (DropDownList)gr.FindControl("cbdestination2");
                                cbbin.SelectedValue = "NES";
                                cbbin.Enabled = false;
                            }
                        }
                    }
                }
                else
                {
                    arr.Clear();
                    string _url = bll.sGetControlParameter("api_external");
                    string _url_do_header = bll.sGetFieldValue("api_do", "api_do_branch") + Request.Cookies["sp"].Value.ToString();
                    List<do_header> json_filter = new List<do_header>();
                    using (WebClient wc = new WebClient())
                    {
                        try
                        {
                            wc.BaseAddress = _url;
                            var _data = wc.DownloadString("api/nav_do/header.php?do_no=" + hddo.Value.ToString());
                            if (!_data.Equals(null))
                            {
                                List<do_header> json = JsonConvert.DeserializeObject<List<do_header>>(_data);
                                Session["do_header_no"] = json;
                            }

                            using (WebClient wc2 = new WebClient())
                            {
                                wc2.BaseAddress = _url;
                                var _data_detail = wc2.DownloadString("api/nav_do/detail.php?do_no=" + hddo.Value.ToString());
                                if (!_data_detail.Equals(null))
                                {
                                    List<do_detail> json_detail = JsonConvert.DeserializeObject<List<do_detail>>(_data_detail);
                                    Session["do_detail"] = json_detail;
                                }
                            }

                        }
                        catch (WebException ex)
                        {
                            if (ex.Status == WebExceptionStatus.ProtocolError && ex.Response != null)
                            {
                                switch (((HttpWebResponse)ex.Response).StatusCode)
                                {
                                    case HttpStatusCode.NotFound:
                                        Response.Redirect("fm_goodreceipt.aspx?err=fm_goodreceipt_nav", false);
                                        break;
                                    case HttpStatusCode.GatewayTimeout:
                                        Response.Redirect("fm_goodreceipt.aspx?err=fm_goodreceipt_nav", false);
                                        break;
                                    case HttpStatusCode.RequestTimeout:
                                        Response.Redirect("fm_goodreceipt.aspx?err=fm_goodreceipt_nav", false);
                                        break;
                                    default:
                                        Response.Redirect("fm_goodreceipt.aspx?err=fm_goodreceipt_nav", false);
                                        break;
                                }
                            }
                        }
                    }
                    arr.Clear();
                    List<do_header> _do_header = new List<do_header>();
                    _do_header = (List<do_header>)Session["do_header_no"];

                    if (!_do_header.Equals(null))
                    {
                        foreach (do_header _dt in _do_header)
                        {
                            lbdodate.Text = _dt.shipment_date.ToString();
                            lbinvoiceno.Text = "-";
                            txfrom.Text = _dt.do_no.ToString().Substring(0, 3);
                            txgdn.Text = _dt.do_no.ToString();
                        }
                    }
                    

                    List<do_detail> _do_detail = new List<do_detail>();
                    List<do_detail> _do_detail_upd = new List<do_detail>();
                    _do_detail = (List<do_detail>)Session["do_detail"];

                    if (!_do_detail.Equals(null))
                    {
                        foreach (do_detail _dt in _do_detail)
                        {
                            _do_detail_upd.Add(new do_detail
                            {
                                do_no = _dt.do_no,
                                shipment_date = _dt.shipment_date,
                                do_line_no = _dt.do_line_no,
                                do_item_1 = _dt.do_item_1,
                                do_item_2 = _dt.do_item_2,
                                do_qty = _dt.do_qty,
                                do_unit_price = _dt.do_unit_price,
                                do_uom = _dt.do_uom,
                                do_description = _dt.do_description,
                                item_cd = _dt.do_item_2,
                                item_nm = bll.vLookUp("select item_nm from tmst_item where item_cd='" + _dt.do_item_2 + "'"),
                                size = bll.vLookUp("select size from tmst_item where item_cd='" + _dt.do_item_2 + "'"),
                                uom = (_dt.do_uom == "CAR" ? "CTN" : "PCS"),
                                qty_conv = bll.vLookUp("select dbo.fn_getqtyconv('" + _dt.do_item_2 + "','" + (_dt.do_uom == "CAR" ? "CTN" : "PCS") + "'," + _dt.do_qty + ")"),
                                qty_ctn = Double.Parse(bll.vLookUp("select isnull(dbo.fn_getqtyconv_uom('" + _dt.do_item_2 + "','" + (_dt.do_uom == "CAR" ? "CTN" : "PCS") + "'," + _dt.do_qty + ",'CTN'),0)")),
                                qty_pcs = Double.Parse(bll.vLookUp("select isnull(dbo.fn_getqtyconv_uom('" + _dt.do_item_2 + "','" + (_dt.do_uom == "CAR" ? "CTN" : "PCS") + "'," + _dt.do_qty + ",'PCS'),0)")),
                                whs_cd = bll.vLookUp("select * from tmst_warehouse where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'"),
                                bin_cd = "GS"
                            });
                        }
                    }
                    

                    grd_nav.DataSource = _do_detail_upd;
                    grd_nav.DataBind();
                    Session["do_detail"] = _do_detail_upd;
                    cbwarehouse_SelectedIndexChanged(sender, e);
                }


                if (sDoStaID == "C") { btsave.Enabled = false; }
                else btsave.Enabled = true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('DO No not yet selected !','Please Select DO / TF To Be Receipt','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
                return;
            }
            
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_goodreceipt_nav");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void cbwarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@whs_cd", cbwarehouse.SelectedValue.ToString()));
            foreach (GridViewRow gr in grd.Rows)
            {
                DropDownList cbbin = (DropDownList)gr.FindControl("cbdestination");
                bll.vBindingComboToSp(ref cbbin, "sp_twarehouse_bin_get", "bin_cd", "bin_nm", arr);
            }
            foreach (GridViewRow gr in grd_nav.Rows)
            {
                DropDownList cbbin = (DropDownList)gr.FindControl("cbdestination2");
                bll.vBindingComboToSp(ref cbbin, "sp_twarehouse_bin_get", "bin_cd", "bin_nm", arr);
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_goodreceipt_nav");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected bool CheckDate(string date)
    {

        string[] formats = { "d/MM/yyyy", "dd/MM/yyyy" };
        DateTime parsedDate;
        var isValidFormat = DateTime.TryParseExact(date, formats, new System.Globalization.CultureInfo("en-US"), System.Globalization.DateTimeStyles.None, out parsedDate);

        if (isValidFormat)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool validate_date(string date)
    {
        //string[] formats = { "d/MM/yyyy", "dd/MM/yyyy" };
        string date_format = "d/M/yyyy";
        try
        {
            DateTime.ParseExact(date, date_format, System.Globalization.DateTimeFormatInfo.InvariantInfo);
        }
        catch
        {
            return false;
        }
        return true;
        
    }
   protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            
            if (dtgdn.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('GDN Date not yet selected !','Please input GDN Date','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
                return;
            }
            if (txgdn.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('GDN No not yet input !','Please input GDN No','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
                return;
            }

            string typeDO = hddo.Value.ToString().Substring(0, 2);
            string sDO = ""; string sPO = "";

            if (typeDO == "DO" || typeDO == "TF")
            {
                if (grd.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Item has not yet selected !','Please refresh Item To Be Receipt','warning');", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
                    return;
                }

                foreach (GridViewRow gv in grd.Rows)
                {
                    TextBox txtExpDate = (TextBox)gv.FindControl("txtExpDate");
                    TextBox txtproddate = (TextBox)gv.FindControl("txtproddate");
                    Label lbitemcode = (Label)gv.FindControl("lbitemcode");

                    HiddenField hdqtyshipment = (HiddenField)gv.FindControl("hdqtyshipment");
                    HiddenField hduomshipment = (HiddenField)gv.FindControl("hduomshipment");
                    HiddenField hdqtyreceived = (HiddenField)gv.FindControl("hdqtyreceived");
                    TextBox txqty_ctn = (TextBox)gv.FindControl("txqty_ctn");
                    TextBox txqty_pcs = (TextBox)gv.FindControl("txqty_pcs");
                    DropDownList cbuom_ctn = (DropDownList)gv.FindControl("cbuom_ctn");
                    DropDownList cbuom_pcs = (DropDownList)gv.FindControl("cbuom_pcs");

                    if (txtExpDate.Text.Equals(null) || txtExpDate.Text == "" || validate_date(txtExpDate.Text) == false)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please Entry Expire Date ! in Item : " + lbitemcode.Text + "','warning');", true);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
                        return;
                    }
                    if (txtproddate.Text.Equals(null) || txtproddate.Text == "" || validate_date(txtproddate.Text) == false)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please Entry Production Date ! in Item : " + lbitemcode.Text + "','warning');", true);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
                        return;
                    }

                    double dQtyCtnCheck = 0, dQtyPcsCheck = 0;
                    if (!double.TryParse(txqty_ctn.Text, out dQtyCtnCheck) || !double.TryParse(txqty_pcs.Text, out dQtyPcsCheck))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Qty must be numeric','Check Qty','warning');", true);
                        return;
                    }

                    if (Convert.ToDouble(txqty_ctn.Text) < 0 || Convert.ToDouble(txqty_pcs.Text) < 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Qty must bigger then zero','Check Qty','warning');", true);
                        return;
                    }

                    double dQty = 0, dQty2 = 0, dQtyTotal = 0, dQtyShipment;

                    dQty = double.Parse(txqty_ctn.Text);
                    dQty2 = double.Parse(txqty_pcs.Text);
                    dQtyTotal = dQty + (double.Parse(bll.vLookUp("select dbo.sfnUomQtyConv('" + lbitemcode.Text + "','PCS','CTN','" + txqty_pcs.Text + "')")));
                    dQtyShipment = (double.Parse(bll.vLookUp("select dbo.sfnUomQtyConv('" + lbitemcode.Text + "','" + hduomshipment.Value.ToString() + "','CTN','" + hdqtyshipment.Value.ToString() + "')")));


                    string DoType = hddo.Value.ToString().Substring(0, 2);

                    if (DoType == "TF")
                    {
                        if (dQtyTotal != dQtyShipment)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Qty must be same as a delivery','Check Qty','warning');", true);
                            return;
                        }
                    }

                }
            }
            else
            {
                if (grd_nav.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Item has not yet selected !','Please refresh Item To Be Receipt','warning');", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
                    return;
                }


                foreach (GridViewRow gv in grd_nav.Rows)
                {
                    TextBox txtExpDate = (TextBox)gv.FindControl("txtExpDate");
                    TextBox txtproddate = (TextBox)gv.FindControl("txtproddate");
                    Label lbitemcode = (Label)gv.FindControl("lbitemcode");

                    HiddenField hdqtyshipment = (HiddenField)gv.FindControl("hdqtyshipment");
                    HiddenField hduomshipment = (HiddenField)gv.FindControl("hduomshipment");
                    HiddenField hdqtyreceived = (HiddenField)gv.FindControl("hdqtyreceived");
                    TextBox txqty_ctn = (TextBox)gv.FindControl("txqty_ctn");
                    TextBox txqty_pcs = (TextBox)gv.FindControl("txqty_pcs");
                    DropDownList cbuom_ctn = (DropDownList)gv.FindControl("cbuom_ctn");
                    DropDownList cbuom_pcs = (DropDownList)gv.FindControl("cbuom_pcs");

                    if (txtExpDate.Text.Equals(null) || txtExpDate.Text == "" || validate_date(txtExpDate.Text) == false)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please Entry Expire Date ! in Item : " + lbitemcode.Text + "','warning');", true);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
                        return;
                    }
                    if (txtproddate.Text.Equals(null) || txtproddate.Text == "" || validate_date(txtproddate.Text) == false)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please Entry Production Date ! in Item : " + lbitemcode.Text + "','warning');", true);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
                        return;
                    }

                    double dQtyCtnCheck = 0, dQtyPcsCheck = 0;
                    if (!double.TryParse(txqty_ctn.Text, out dQtyCtnCheck) || !double.TryParse(txqty_pcs.Text, out dQtyPcsCheck))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Qty must be numeric','Check Qty','warning');", true);
                        return;
                    }

                    if (Convert.ToDouble(txqty_ctn.Text) < 0 || Convert.ToDouble(txqty_pcs.Text) < 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Qty must bigger then zero','Check Qty','warning');", true);
                        return;
                    }

                    double dQty = 0, dQty2 = 0, dQtyTotal = 0, dQtyShipment;

                    dQty = double.Parse(txqty_ctn.Text);
                    dQty2 = double.Parse(txqty_pcs.Text);
                    dQtyTotal = dQty + (double.Parse(bll.vLookUp("select dbo.sfnUomQtyConv('" + lbitemcode.Text + "','PCS','CTN','" + txqty_pcs.Text + "')")));
                    dQtyShipment = (double.Parse(bll.vLookUp("select dbo.sfnUomQtyConv('" + lbitemcode.Text + "','" + hduomshipment.Value.ToString() + "','CTN','" + hdqtyshipment.Value.ToString() + "')")));

                }

                DateTime dateDO = DateTime.ParseExact(dtgoodreceipt.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                string sdate = dateDO.Day.ToString();
                string sMonth = dateDO.Month.ToString();
                string syear = dateDO.Year.ToString();
                sPO = bll.vLookUp("select po_no from tmst_po where month(po_dt)='" + sMonth.ToString() + "' and year(po_dt)='" + syear.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "' and po_sta_id not in ('C')");

                arr.Clear();
                arr.Add(new cArrayList("@do_dt", DateTime.ParseExact(dtgoodreceipt.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@gdn_dt", DateTime.ParseExact(dtgdn.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@po_no", sPO));
                arr.Add(new cArrayList("@grandtotal", "0"));
                arr.Add(new cArrayList("@whs_cd", cbwarehouse.SelectedValue.ToString()));
                arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@delivery_typ", "RENT"));
                arr.Add(new cArrayList("@driver_nm", ""));
                arr.Add(new cArrayList("@driver_cd", ""));
                arr.Add(new cArrayList("@comp_cd", ""));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@box_cd", "I"));
                arr.Add(new cArrayList("@vhc_cd", ""));
                arr.Add(new cArrayList("@vhc_no", ""));
                arr.Add(new cArrayList("@manual_no", txmanualno.Text));
                arr.Add(new cArrayList("@docref_no", txgdn.Text));
                bll.vInsertMstDO(arr, ref sDO);
                lbinvoiceno.Text = sDO;

                foreach (GridViewRow gv in grd_nav.Rows)
                {
                    Label lbitemcode = (Label)gv.FindControl("lbitemcode");
                    TextBox txtExpDate = (TextBox)gv.FindControl("txtExpDate");
                    TextBox txtproddate = (TextBox)gv.FindControl("txtproddate");

                    HiddenField hdqtyshipment = (HiddenField)gv.FindControl("hdqtyshipment");
                    HiddenField hdqtyreceived = (HiddenField)gv.FindControl("hdqtyreceived");
                    TextBox txqty_ctn = (TextBox)gv.FindControl("txqty_ctn");
                    TextBox txqty_pcs = (TextBox)gv.FindControl("txqty_pcs");
                    DropDownList cbuom_ctn = (DropDownList)gv.FindControl("cbuom_ctn");
                    DropDownList cbuom_pcs = (DropDownList)gv.FindControl("cbuom_pcs");

                    arr.Clear();
                    arr.Add(new cArrayList("@do_no", sDO));
                    arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
                    arr.Add(new cArrayList("@qty", txqty_ctn.Text));
                    arr.Add(new cArrayList("@unitprice", bll.vLookUp("(select top 1 unitprice from tcustomertype_price where cust_typ in (select qry_data from tmap_query where qry_cd='price_po') and tcustomertype_price.item_cd='"+lbitemcode.Text+"')")));
                    arr.Add(new cArrayList("@subtotal", (double.Parse(txqty_ctn.Text)*double.Parse(bll.vLookUp("(select top 1 unitprice from tcustomertype_price where cust_typ in (select qry_data from tmap_query where qry_cd='price_po') and tcustomertype_price.item_cd='" + lbitemcode.Text + "')"))).ToString() ));
                    arr.Add(new cArrayList("@uom", cbuom_ctn.SelectedValue.ToString()));
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    bll.vInsertDoDtl(arr);
                }

                arr.Clear();
                arr.Add(new cArrayList("@po_no", sPO));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vBatchDO(arr);

            }


            string date_format = "d/M/yyyy";

            string do_no_new = "";
            if (typeDO == "DO" || typeDO == "TF") { do_no_new = hddo.Value.ToString(); } else { do_no_new = lbinvoiceno.Text; }
             
            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@do_no", do_no_new));
            arr.Add(new cArrayList("@receipt_dt", DateTime.ParseExact(dtgoodreceipt.Text, date_format, System.Globalization.DateTimeFormatInfo.InvariantInfo)));
            arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@source", txfrom.Text));

            string sReceiptNo = "";
            bll.vInsertMstGoodReceipt(arr, ref sReceiptNo);

            txgoodreceipt.Text = sReceiptNo;

            if (typeDO == "DO" || typeDO == "TF")
            {
                foreach (GridViewRow gv in grd.Rows)
                {
                    Label lbitemcode = (Label)gv.FindControl("lbitemcode");
                    //Label lbqtyshipment = (Label)gv.FindControl("lbqtyshipment");
                    //TextBox txqtyreceived = (TextBox)gv.FindControl("txreceived");
                    TextBox txtExpDate = (TextBox)gv.FindControl("txtExpDate");
                    TextBox txtproddate = (TextBox)gv.FindControl("txtproddate");
                    //DropDownList cbuom = (DropDownList)gv.FindControl("cbuom");

                    HiddenField hdqtyshipment = (HiddenField)gv.FindControl("hdqtyshipment");
                    HiddenField hdqtyreceived = (HiddenField)gv.FindControl("hdqtyreceived");
                    TextBox txqty_ctn = (TextBox)gv.FindControl("txqty_ctn");
                    TextBox txqty_pcs = (TextBox)gv.FindControl("txqty_pcs");
                    DropDownList cbuom_ctn = (DropDownList)gv.FindControl("cbuom_ctn");
                    DropDownList cbuom_pcs = (DropDownList)gv.FindControl("cbuom_pcs");

                    DateTime dDate;

                    if (DateTime.TryParse(txtproddate.Text, out dDate))
                    {
                        String.Format("{0:d/M/yyyy}", dDate);
                    }

                    //if (txqty_ctn.Text.Equals(null) || txqty_ctn.Text == "")
                    //{
                    //    txqty_ctn.Text = "0";
                    //}


                    arr.Clear();
                    arr.Add(new cArrayList("@receipt_no", sReceiptNo));
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    arr.Add(new cArrayList("@item_cd", lbitemcode.Text));

                    arr.Add(new cArrayList("@qty_shipment", hdqtyshipment.Value.ToString()));
                    arr.Add(new cArrayList("@qty", txqty_ctn.Text));
                    arr.Add(new cArrayList("@qty2", txqty_pcs.Text));
                    arr.Add(new cArrayList("@uom", cbuom_ctn.SelectedValue.ToString()));
                    arr.Add(new cArrayList("@uom2", cbuom_pcs.SelectedValue.ToString()));

                    arr.Add(new cArrayList("@whs_cd", cbwarehouse.SelectedValue.ToString()));
                    //arr.Add(new cArrayList("@qty_received", txqtyreceived.Text));
                    arr.Add(new cArrayList("@bin_cd", cbdestination.SelectedValue.ToString()));
                    arr.Add(new cArrayList("@expire_date", DateTime.ParseExact(txtExpDate.Text, date_format, System.Globalization.DateTimeFormatInfo.InvariantInfo)));
                    arr.Add(new cArrayList("@prod_date", DateTime.ParseExact(txtproddate.Text, date_format, System.Globalization.DateTimeFormatInfo.InvariantInfo)));
                    //arr.Add(new cArrayList("@uom", cbuom.SelectedValue.ToString()));
                    bll.vInsertGoodReceiptDtl(arr);
                }
                arr.Clear();
                arr.Add(new cArrayList("@do_no", hddo.Value.ToString()));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@do_sta_id", "B"));
                bll.vUpdateMstDo2(arr);
            }
            else
            {
                foreach (GridViewRow gv in grd_nav.Rows)
                {
                    Label lbitemcode = (Label)gv.FindControl("lbitemcode");
                    //Label lbqtyshipment = (Label)gv.FindControl("lbqtyshipment");
                    //TextBox txqtyreceived = (TextBox)gv.FindControl("txreceived");
                    TextBox txtExpDate = (TextBox)gv.FindControl("txtExpDate");
                    TextBox txtproddate = (TextBox)gv.FindControl("txtproddate");
                    //DropDownList cbuom = (DropDownList)gv.FindControl("cbuom");

                    HiddenField hdqtyshipment = (HiddenField)gv.FindControl("hdqtyshipment");
                    HiddenField hdqtyreceived = (HiddenField)gv.FindControl("hdqtyreceived");
                    TextBox txqty_ctn = (TextBox)gv.FindControl("txqty_ctn");
                    TextBox txqty_pcs = (TextBox)gv.FindControl("txqty_pcs");
                    DropDownList cbuom_ctn = (DropDownList)gv.FindControl("cbuom_ctn");
                    DropDownList cbuom_pcs = (DropDownList)gv.FindControl("cbuom_pcs");

                    DateTime dDate;

                    if (DateTime.TryParse(txtproddate.Text, out dDate))
                    {
                        String.Format("{0:d/M/yyyy}", dDate);
                    }

                    //if (txqty_ctn.Text.Equals(null) || txqty_ctn.Text == "")
                    //{
                    //    txqty_ctn.Text = "0";
                    //}


                    arr.Clear();
                    arr.Add(new cArrayList("@receipt_no", sReceiptNo));
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    arr.Add(new cArrayList("@item_cd", lbitemcode.Text));

                    arr.Add(new cArrayList("@qty_shipment", hdqtyshipment.Value.ToString()));
                    arr.Add(new cArrayList("@qty", txqty_ctn.Text));
                    arr.Add(new cArrayList("@qty2", txqty_pcs.Text));
                    arr.Add(new cArrayList("@uom", cbuom_ctn.SelectedValue.ToString()));
                    arr.Add(new cArrayList("@uom2", cbuom_pcs.SelectedValue.ToString()));

                    arr.Add(new cArrayList("@whs_cd", cbwarehouse.SelectedValue.ToString()));
                    //arr.Add(new cArrayList("@qty_received", txqtyreceived.Text));
                    arr.Add(new cArrayList("@bin_cd", cbdestination2.SelectedValue.ToString()));
                    arr.Add(new cArrayList("@expire_date", DateTime.ParseExact(txtExpDate.Text, date_format, System.Globalization.DateTimeFormatInfo.InvariantInfo)));
                    arr.Add(new cArrayList("@prod_date", DateTime.ParseExact(txtproddate.Text, date_format, System.Globalization.DateTimeFormatInfo.InvariantInfo)));
                    //arr.Add(new cArrayList("@uom", cbuom.SelectedValue.ToString()));
                    bll.vInsertGoodReceiptDtl(arr);
                }
                arr.Clear();
                arr.Add(new cArrayList("@do_no", sDO));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@do_sta_id", "B"));
                bll.vUpdateMstDo2(arr);
            }

                
            arr.Clear();
            arr.Add(new cArrayList("@stockcard_typ", "GOODRECEIVED"));
            arr.Add(new cArrayList("@refno", sReceiptNo));
            bll.vtacc_stock_cardByGoodReceipt(arr);
            
            btprint.Enabled = true;
            btsave.Enabled = false;
            lbdostaid.Text = bll.vLookUp("select fld_desc from tfield_value where fld_nm='do_sta_id' and fld_valu='B'");
            arr.Clear();
            arr.Add(new cArrayList("@receipt_no", sReceiptNo));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grddtl, "sp_tgoodreceipt_dtl_get", arr);
            grd.Visible = false;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al3", "sweetAlert('Good receipt " + sReceiptNo + " has been saved successfully .... ','Good Receipt','info');", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_goodreceipt_nav");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Label lbqtyshipment = (Label)e.Row.FindControl("lbqtyshipment");
                HiddenField hdqtyshipment = (HiddenField)e.Row.FindControl("hdqtyshipment");
                HiddenField hdqtyreceived = (HiddenField)e.Row.FindControl("hdqtyreceived");
                //TextBox txreceived = (TextBox)e.Row.FindControl("txreceived");
                //DropDownList cbuom = (DropDownList)e.Row.FindControl("cbuom");
                DropDownList cbuom_ctn = (DropDownList)e.Row.FindControl("cbuom_ctn");
                DropDownList cbuom_pcs = (DropDownList)e.Row.FindControl("cbuom_pcs");
                DropDownList cbbin = (DropDownList)e.Row.FindControl("cbbin");

                bll.vBindingFieldValueToCombo(ref cbuom_ctn, "uom_tf", "uom");
                bll.vBindingFieldValueToCombo(ref cbuom_pcs, "uom_tf", "uom");
                cbuom_ctn.SelectedValue = "CTN";
                cbuom_pcs.SelectedValue = "PCS";

                //bll.vBindingFieldValueToCombo(ref cbuom, "uom_po", "uom");

                decimal qtyshipment, qtyreceived;
                if (hdqtyshipment.Value != null)
                    qtyshipment = decimal.Parse(hdqtyshipment.Value.ToString());
                else
                    qtyshipment = 0;
                totalqtyshipment = totalqtyshipment + qtyshipment;

                hdqtyreceived.Value = hdqtyshipment.Value.ToString();
                if (hdqtyreceived.Value != "")
                    qtyreceived = decimal.Parse(hdqtyreceived.Value.ToString());
                else
                    qtyreceived = 0;
                //txreceived.Enabled = false;
                totalqtyreceived = totalqtyreceived + qtyreceived;

                // Check For NE Special Customer
                string customer = "", typecustomer = ""; string[] arrCustomer;
                customer = bll.vLookUp("select to_nm from tmst_po where po_no = (select po_no from tmst_do where do_no='" + hddo.Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "')");
                arrCustomer = customer.Replace(" ", "").Split('-');
                typecustomer = bll.vLookUp("select cuscate_cd from tmst_customer where cust_cd = '" + arrCustomer[0] + "'");


                cbuom_ctn.Enabled = false;
                cbuom_pcs.Enabled = false;
                //if (typecustomer == "SP")
                //{
                //cbuom.Enabled = true;
                //}
                //else
                //{
                //    cbuom.Enabled = false;
                //}
                // Check For NE Special Customer

            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbltotalqtyshipment = (Label)e.Row.FindControl("lbltotalqtyshipment");
                Label lbltotalqtyreceived = (Label)e.Row.FindControl("lbltotalqtyreceived");
                lbltotalqtyshipment.Text = totalqtyshipment.ToString("#,##0.00");
                lbltotalqtyreceived.Text = totalqtyreceived.ToString("#,##0.00");
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_goodreceipt_nav");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        try
        {

            if (txgoodreceipt.Text == "NEW")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al3", "sweetAlert('Good receipt not entry yet, can not print ....','Good Receipt','info');", true);
                return;
            }
            //string typeDO = hddo.Value.ToString().Substring(0, 2);
            List<cArrayList> arr = new List<cArrayList>();
            //if (typeDO == "DO" || typeDO == "TF")
            //{
                hddo.Value = txgoodreceipt.Text;
            //}
            //else
            //{
            //    hddo.Value = lbinvoiceno.Text;
            //}
                
            arr.Add(new cArrayList("@receipt_no", hddo.Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            Session["lformulagoodreceipt"] = arr;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=goodreceipt');", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_goodreceipt_nav");
            Response.Redirect("fm_ErrorPage.aspx");
        }

    }
    protected void grddtl_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblqty = (Label)e.Row.FindControl("lblqty");
                Label lblqty_received = (Label)e.Row.FindControl("lblqty_received");

                decimal qty, qty_received;
                if (lblqty != null)
                    qty = decimal.Parse(lblqty.Text);
                else
                    qty = 0;
                totalqty = totalqty + qty;

                if (lblqty_received.Text != "")
                    qty_received = decimal.Parse(lblqty_received.Text);
                else
                    qty_received = 0;
                totalqty_received = totalqty_received + qty_received;

            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbltotalqty = (Label)e.Row.FindControl("lbltotalqty");
                Label lbltotalqty_received = (Label)e.Row.FindControl("lbltotalqty_received");
                lbltotalqty.Text = totalqty.ToString("#,##0.00");
                lbltotalqty_received.Text = totalqty_received.ToString("#,##0.00");
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_goodreceipt_nav");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("default_2.aspx");
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_goodreceipt_nav.aspx");
    }

    protected void grddtl_nav_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbitem_cd = (Label)e.Row.FindControl("lbitem_cd");
                Label lbitem_nm = (Label)e.Row.FindControl("lbitem_nm");
                Label lbsize = (Label)e.Row.FindControl("lbsize");
                Label lbwhs_cd = (Label)e.Row.FindControl("lbwhs_cd");
                Label lbbin_cd = (Label)e.Row.FindControl("lbbin_cd");

                //HiddenField hdqtyshipment = (HiddenField)e.Row.FindControl("hdqtyshipment");
                //HiddenField hdqtyreceived = (HiddenField)e.Row.FindControl("hdqtyreceived");
                //TextBox txreceived = (TextBox)e.Row.FindControl("txreceived");
                //DropDownList cbuom = (DropDownList)e.Row.FindControl("cbuom");
                //DropDownList cbuom_ctn = (DropDownList)e.Row.FindControl("cbuom_ctn");
                //DropDownList cbuom_pcs = (DropDownList)e.Row.FindControl("cbuom_pcs");
                //DropDownList cbbin = (DropDownList)e.Row.FindControl("cbbin");

                //bll.vBindingFieldValueToCombo(ref cbuom_ctn, "uom_tf", "uom");
                //bll.vBindingFieldValueToCombo(ref cbuom_pcs, "uom_tf", "uom");
                //cbuom_ctn.SelectedValue = "CTN";
                //cbuom_pcs.SelectedValue = "PCS";

                //bll.vBindingFieldValueToCombo(ref cbuom, "uom_po", "uom");

                //decimal qtyshipment, qtyreceived;
                //if (hdqtyshipment.Value != null)
                //    qtyshipment = decimal.Parse(hdqtyshipment.Value.ToString());
                //else
                //    qtyshipment = 0;
                //totalqtyshipment = totalqtyshipment + qtyshipment;

                //hdqtyreceived.Value = hdqtyshipment.Value.ToString();
                //if (hdqtyreceived.Value != "")
                //    qtyreceived = decimal.Parse(hdqtyreceived.Value.ToString());
                //else
                //    qtyreceived = 0;
                ////txreceived.Enabled = false;
                //totalqtyreceived = totalqtyreceived + qtyreceived;

                // Check For NE Special Customer
                //string customer = "", typecustomer = ""; string[] arrCustomer;
                //customer = bll.vLookUp("select to_nm from tmst_po where po_no = (select po_no from tmst_do where do_no='" + hddo.Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "')");
                //arrCustomer = customer.Replace(" ", "").Split('-');
                //typecustomer = bll.vLookUp("select cuscate_cd from tmst_customer where cust_cd = '" + arrCustomer[0] + "'");


                //cbuom_ctn.Enabled = false;
                //cbuom_pcs.Enabled = false;
                //if (typecustomer == "SP")
                //{
                //cbuom.Enabled = true;
                //}
                //else
                //{
                //    cbuom.Enabled = false;
                //}
                // Check For NE Special Customer

            }

            //if (e.Row.RowType == DataControlRowType.Footer)
            //{
            //    Label lbltotalqtyshipment = (Label)e.Row.FindControl("lbltotalqtyshipment");
            //    Label lbltotalqtyreceived = (Label)e.Row.FindControl("lbltotalqtyreceived");
            //    lbltotalqtyshipment.Text = totalqtyshipment.ToString("#,##0.00");
            //    lbltotalqtyreceived.Text = totalqtyreceived.ToString("#,##0.00");
            //}
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_goodreceipt_nav");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void grd_nav_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbitem_cd = (Label)e.Row.FindControl("lbitem_cd");
                Label lbitem_nm = (Label)e.Row.FindControl("lbitem_nm");
                Label lbsize = (Label)e.Row.FindControl("lbsize");

                HiddenField hdqtyshipment = (HiddenField)e.Row.FindControl("hdqtyshipment");
                HiddenField hdqtyreceived = (HiddenField)e.Row.FindControl("hdqtyreceived");
                HiddenField hdqtyreceived_conv = (HiddenField)e.Row.FindControl("hdqtyreceived_conv");
                HiddenField hduomshipment = (HiddenField)e.Row.FindControl("hduomshipment");
                Label lbqtyshipment = (Label)e.Row.FindControl("lbqtyshipment");
                TextBox txqty_ctn = (TextBox)e.Row.FindControl("txqty_ctn");
                TextBox txqty_pcs = (TextBox)e.Row.FindControl("txqty_pcs");

                DropDownList cbuom_ctn = (DropDownList)e.Row.FindControl("cbuom_ctn");
                DropDownList cbuom_pcs = (DropDownList)e.Row.FindControl("cbuom_pcs");
                DropDownList cbbin = (DropDownList)e.Row.FindControl("cbbin");

                bll.vBindingFieldValueToCombo(ref cbuom_ctn, "uom_tf", "uom");
                bll.vBindingFieldValueToCombo(ref cbuom_pcs, "uom_tf", "uom");
                cbuom_ctn.SelectedValue = "CTN";
                cbuom_pcs.SelectedValue = "PCS";

                //bll.vBindingFieldValueToCombo(ref cbuom, "uom_po", "uom");

                decimal qtyshipment, qtyreceived;
                if (hdqtyshipment.Value != null)
                    qtyshipment = decimal.Parse(hdqtyshipment.Value.ToString());
                else
                    qtyshipment = 0;
                totalqtyshipment = totalqtyshipment + qtyshipment;

                hdqtyreceived.Value = hdqtyshipment.Value.ToString();
                if (hdqtyreceived.Value != "")
                    qtyreceived = decimal.Parse(hdqtyreceived.Value.ToString());
                else
                    qtyreceived = 0;
                //txreceived.Enabled = false;
                totalqtyreceived = totalqtyreceived + qtyreceived;

                // Check For NE Special Customer
                string customer = "", typecustomer = ""; string[] arrCustomer;
                customer = bll.vLookUp("select to_nm from tmst_po where po_no = (select po_no from tmst_do where do_no='" + hddo.Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "')");
                arrCustomer = customer.Replace(" ", "").Split('-');
                typecustomer = bll.vLookUp("select cuscate_cd from tmst_customer where cust_cd = '" + arrCustomer[0] + "'");


                cbuom_ctn.Enabled = false;
                cbuom_pcs.Enabled = false;

            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbltotalqtyshipment = (Label)e.Row.FindControl("lbltotalqtyshipment");
                Label lbltotalqtyreceived = (Label)e.Row.FindControl("lbltotalqtyreceived");
                lbltotalqtyshipment.Text = totalqtyshipment.ToString("#,##0.00");
                lbltotalqtyreceived.Text = totalqtyreceived.ToString("#,##0.00");
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_goodreceipt_nav");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
}