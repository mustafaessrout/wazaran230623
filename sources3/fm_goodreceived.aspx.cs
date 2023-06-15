using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities.Configuration;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;

public partial class fm_goodreceived : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            //List<cArrayList> arr = new List<cArrayList>();
            bll.vBindingFieldValueToComboWithChoosen(ref cbtypeofstockin, "typeofstockin");
            List<tgoodreceipt_dtl> _tgoodreceived_dtl = new List<tgoodreceipt_dtl>();
            Session["tgoodreceipt_dtl"] = _tgoodreceived_dtl;
            //arr.Add(new cArrayList("@qry_cd", "uom_stockin"));
            bll.vBindingFieldValueToComboByQryWithEmptyChoosen(ref cbuom, "uom", "uom_stockin");
            DateTime _dt = DateTime.ParseExact(Request.Cookies["waz_dt"].Value, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            dtreceipt.Text = Request.Cookies["waz_dt"].Value;
            dtwarehouse_CalendarExtender.EndDate = _dt;
            dtwarehouse.Text = Request.Cookies["waz_dt"].Value;
            lbgdnnav.Text = String.Empty;
            dtexpire_CalendarExtender.StartDate = System.DateTime.ParseExact( Request.Cookies["waz_dt"].Value, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            dtexpire.ReadOnly = true;
            bll.vBindingComboToSpWithEmptyChoosen(ref cbvendor, "sp_tmst_vendor_get", "vendor_cd", "vendor_nm");
            //cd.v_disablecontrol(cbvendor);
            Session["tgoodreceipt_dtl"] = _tgoodreceived_dtl;
            DateTime _dtexpire = _dt.AddDays(365);
            dtexpire.Text = _dtexpire.ToString("d/M/yyyy");
            dtpostgdn.Text = Request.Cookies["waz_dt"].Value;
            cd.v_hiddencontrol(pnlgrdnav);
            cd.v_hiddencontrol(pnlgrdnormal);
            cd.v_hiddencontrol(btreceived);
            cd.v_hiddencontrol(btprint);
            cd.v_disablecontrol(cbwhs);
            cd.v_disablecontrol(cbbin);
            cd.v_disablecontrol(txgrnno);
            cd.v_hiddencontrol(pnlnav);
            cd.v_hiddencontrol(pnlgoodreceivedbranch);
            cd.v_disablecontrol(btadd);
            cd.v_disablecontrol(txitemsearch);
            cd.v_disablecontrol(txqty);
            cd.v_disablecontrol(cbuom);
            cd.v_disablecontrol(txdrivername);
            cd.v_hiddencontrol(btprint);
            
            //cd.v_disablecontrol(dtreceipt);
            //cd.v_disablecontrol(txqty2);
            cd.v_enablecontrol(dtwarehouse);
            cd.v_hiddencontrol(btsavestockin);
            cd.v_disablecontrol(dtreceipt);
            cd.v_enablecontrol(txdrivername);
            cd.v_disablecontrol(btsearch);
            cd.v_disablecontrol(btgdnnav);
            cd.v_enablecontrol(cbvendor);
            dtwarehouse.ReadOnly = true;
            dtreceipt.ReadOnly = true;

        }

    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetItemList(string prefixText, int count, string contextKey)
    {
        HttpCookie cook;
        cook = HttpContext.Current.Request.Cookies["sp"];
        HttpCookie otlcd;
        otlcd = HttpContext.Current.Request.Cookies["otlcd"];
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
            sItem = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["item_cd"].ToString() + "|" + rs["item_nm"].ToString() + "|" + rs["size"].ToString() + "|" + rs["branded_nm"].ToString(), rs["item_cd"].ToString());
            lItem.Add(sItem);
        }
        rs.Close();
        return (lItem.ToArray());
    }
    public void vBindingGrid()
    {
        List<nav_header> _navheaderbysalespoint = new List<nav_header>();
        string _salespoint = Request.Cookies["sp"].Value;
        List<nav_header> _nav_header = lNavHeader();
        foreach(nav_header _h in _nav_header)
        {
            if ((_h.DO_Branch_Code == _salespoint) && (_h.Canceled == false))
            {
                _navheaderbysalespoint.Add(_h);
            }
        }
        grd.DataSource = _navheaderbysalespoint;
        grd.DataBind();
        Session["nav_header"] = _navheaderbysalespoint;
    }
    //public List<do_header> lDoHeader()
    public List<nav_header> lNavHeader()
    {
        //List<do_header> _tmp = new List<do_header>();
        List<nav_header> _Value = new List<nav_header>();
        List<root_header> _tmp = new List<root_header>();
        //List<> _filteredvalue = new List<Value>();
        string _do_no = string.Empty;
        //string _url = bll.sGetControlParameter("api_external");
        //string _url_do_header = bll.sGetFieldValue("api_do", "api_do_all") + Request.Cookies["sp"].Value.ToString();
        // string _url = "http://192.168.120.123:8058/SAWAMAG_API/OData/SalesLine?$format=json&$filter=Posting_Date%20gt%20datetime%272022-10-01%27";
        //string _url = "http://192.168.120.123:8058/SAWAMAG_API/OData/SalesHeader?$format=json&$filter=Posting_Date%20gt%20datetime%272022-10-01%27";
        //string _url = "http://192.168.1.123:8058/SAWAMAG_API/OData/SalesHeader?$format=json";
        DateTime _dt = System.DateTime.ParseExact(dtpostgdn.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        string _formatdate = _dt.Year.ToString() + "-" + fn_changedecimal( _dt.Month) + "-" + fn_changedecimal( _dt.Day);
        string _url = "http://192.168.90.2:8068/SAWAMAG_API/OData/Company('SAWAMAG')/APISalesHeader?$format=json&$filter=Posting_Date%20eq%20datetime%27"+_formatdate+"%27";
        //string _url = bll.sGetControlParameter("api_navision");
        List<root_header> json_filter = new List<root_header>();
        using (WebClient wc = new WebClient())
        {

            try
            {
                wc.Credentials = new NetworkCredential("NAVAPI", "vJ217239fm2PThC+h9Qe+SJIQNIdfd4V9U+NVTkzj1k=");
                ServicePointManager.ServerCertificateValidationCallback += (senderX, certificate, chain, sslPolicyErrors) => true;
                var _data = wc.DownloadString(_url); //+ _url_do_header);

                if (!_data.Equals(null))
                {
                    root_header json = JsonConvert.DeserializeObject<root_header>(_data);
                    _Value = (List<nav_header>) json.value;
                    Session["nav_header"] = _Value; //json_filter;
                }
                else
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
                            //Response.Redirect("fm_goodreceipt.aspx?err=fm_goodreceipt_nav", false);
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), 
                                "sweetAlert('"+ex.Message.ToString()+"','Error API Navision','warning');", true);
                            break;
                    }
                }
            }
        }
        return _Value; //json_filter;
    }

    protected string fn_changedecimal(int _value)
    {
        string _temp = string.Empty;
        if (_value < 10)
        {
            _temp = "0" + _value.ToString();
        }
        else { _temp = _value.ToString(); }

        return _temp;
    }
    public List<nav_detail2> lNavDetail()
    {
        List<nav_detail2> _Value = new List<nav_detail2>();
        List<root_detail> _tmp = new List<root_detail>();
        //List<> _filteredvalue = new List<Value>();
        string _do_no = string.Empty;
        DateTime _dt = DateTime.ParseExact(dtpostgdn.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //string _url = bll.sGetControlParameter("api_external");
        //string _url_do_header = bll.sGetFieldValue("api_do", "api_do_all") + Request.Cookies["sp"].Value.ToString();
        // string _url = "http://192.168.120.123:8058/SAWAMAG_API/OData/SalesLine?$format=json&$filter=Posting_Date%20gt%20datetime%272022-10-01%27";
        //string _url = @"http://192.168.120.123:8058/SAWAMAG_API/OData/SalesLine?$format=json&$filter=Posting_Date%20gt%20datetime%272022-10-01%27";
        //string _url = "http://192.168.1.123:8058/SAWAMAG_API/OData/SalesLine?$format=json";
        //string _url = "http://192.168.90.2:8068/SAWAMAG_API/OData/Company('SAWAMAG')/APISalesLine?$format=json";
        string _url = "http://192.168.90.2:8068/SAWAMAG_API/OData/Company('SAWAMAG')/APISalesLine?$format=json&$filter=Posting_Date%20eq%20datetime%27" + _dt.Year.ToString() + "-" + fn_changedecimal(_dt.Month) + "-" + fn_changedecimal(_dt.Day)  +"%27";
        List<root_detail> json_filter = new List<root_detail>();
        using (WebClient wc = new WebClient())
        {

            try
            {

                // wc.BaseAddress = _url;

                wc.Credentials = new NetworkCredential("NAVAPI", "vJ217239fm2PThC+h9Qe+SJIQNIdfd4V9U+NVTkzj1k=");
                ServicePointManager.ServerCertificateValidationCallback += (senderX, certificate, chain, sslPolicyErrors) => true;
                var _data = wc.DownloadString(_url); //+ _url_do_header);

                if (!_data.Equals(null))
                {
                    root_detail json = JsonConvert.DeserializeObject<root_detail>(_data);
                    _Value = (List<nav_detail2>)json.value;
                    Session["nav_detail"] = _Value; //json_filter;
                }
                else
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
        return _Value; //json_filter;
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        List<nav_detail2> _nav_detail = new List<nav_detail2>();
        List<nav_header> _selectedheader = new List<nav_header>();
        List<nav_header> _nav_header = (List<nav_header>)Session["nav_header"];

        Label lbdono = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbdono");
        lbgdnnav.Text = lbdono.Text;
        foreach (nav_header _h in _nav_header)
        {
            if (lbdono.Text == _h.No)
            {
                _selectedheader.Add(_h);
                txdrivername.Text = _h.Driver_Name;
                lbrefno.Text = _h.Reference_No;
            }
        }
        grd.DataSource = _selectedheader;
        grd.DataBind();

        List<nav_detail2> _filtereddetail = new List<nav_detail2>();
        List<nav_detail2> _nav_detail2 = lNavDetail();
        foreach(nav_detail2 _h in _nav_detail2)
        {
            if (_h.Document_No == lbdono.Text)
            {
                _filtereddetail.Add(_h);
            }
        }
        grddetail.DataSource = _filtereddetail;
        grddetail.DataBind();

        arr.Clear();
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        bll.vBindingComboToSpWithEmptyChoosen(ref cbwhs, "sp_tmst_warehouse_get", "whs_cd", "whs_nm", arr);
        cd.v_enablecontrol(cbwhs);
        cd.v_disablecontrol(grd);
        cd.v_disablecontrol(txgdnav);
       
    }

    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            foreach (LinkButton button in e.Row.Cells[8].Controls.OfType<LinkButton>())
            {
                if ((button.CommandName == "Edit") || (button.CommandName == "Select"))
                {
                    button.Attributes.Add("OnClick", "ShowProgress();");
                }
            }
            Label lbdono = (Label)e.Row.FindControl("lbdono");
            int _count = Convert.ToInt16(bll.vLookUp("select count(1) from tmst_goodreceipt where do_no='" + lbdono.Text + "'"));
            if (_count > 0)
            {
                //e.Row.Enabled = false;
                e.Row.BackColor = System.Drawing.Color.LightGray;
                e.Row.Visible = false;
            }
            //Label lbitemcodewazaran = (Label)e.Row.FindControl("lbitemcodewazaran");
            //Label lbitemnav = (Label)e.Row.FindControl("lbitemnav");
            //lbitemcodewazaran.Text = bll.vLookUp("select item_cd from tmst_item where item_cd_nav='"+lbitemnav.Text+"'");
        }
    }

    protected void btreceived_Click(object sender, EventArgs e)
    {
        
        //Label lbdono = (Label)grd.SelectedRow.FindControl("lbdono");
        bool _checkdate = false;

        if (cbtypeofstockin.SelectedValue == String.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                  "sweetAlert('Stockin type must be select !','Stockin type','warning');", true);
            return;
        }

        foreach (GridViewRow _row in grddetail.Rows)
        {
            if (_row.RowType == DataControlRowType.DataRow)
            {
                TextBox dtprod = (TextBox)_row.FindControl("dtprod");
                TextBox dtexp = (TextBox)_row.FindControl("dtexp");
                Label lbitemcode = (Label)_row.FindControl("lbitemcodewazaran");
                if (dtprod.Text == String.Empty || dtexp.Text == String.Empty || lbitemcode.Text == string.Empty )
                {
                    _checkdate = true;
                }

            }
        }
        if (_checkdate)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                    "sweetAlert('There is expire or production date empty or no item code mapping with navision !','Can not empty','warning');", true);
            return;
        }
        if (txdrivername.Text == String.Empty)
        {
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
            //    "sweetAlert('Please entry driver name !','driver name','warning');", true);
            //return;
        }
        if (cbvendor.SelectedValue == String.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
             "sweetAlert('Please select vendor !','Vendor selection','warning');", true);
            return;
        }
        if (cbwhs.SelectedValue == String.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                  "sweetAlert('Please select warehouse !','Warehouse destination','warning');", true);
            return;
        }
        if (cbbin.SelectedValue == String.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                  "sweetAlert('Please select Bin !','Bin destination','warning');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        arr.Add(new cArrayList("@do_no", lbgdnnav.Text));
        arr.Add(new cArrayList("@receipt_dt", System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value));
        arr.Add(new cArrayList("@register_dt", System.DateTime.ParseExact(dtwarehouse.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@driver_nm", ((txdrivername.Text == string.Empty)?"NA":txdrivername.Text)));
        arr.Add(new cArrayList("@source", cbtypeofstockin.SelectedValue));
        arr.Add(new cArrayList("@vendor_cd", cbvendor.SelectedValue));
        arr.Add(new cArrayList("@remark", txremark.Text));
        arr.Add(new cArrayList("@po_no", lbrefno.Text));
        string _grn_no = string.Empty;
        bll.vInsertMstGoodReceipt(arr, ref _grn_no);
        txgrnno.Text = _grn_no;
        foreach (GridViewRow row in grddetail.Rows)
        {
            Label lbitemcodewazaran = (Label)row.FindControl("lbitemcodewazaran");
            string _uom2 = bll.vLookUp("select uom_base from tmst_item where item_cd='" + lbitemcodewazaran.Text + "'");
            Label lbqty = (Label)row.FindControl("lbqty");
            Label lbuom = (Label)row.FindControl("lbuom");
            TextBox dtprod = (TextBox)row.FindControl("dtprod");
            TextBox dtexp = (TextBox)row.FindControl("dtexp");
            Label lblotno = (Label)row.FindControl("lblotno");
            DateTime _dtprod = Convert.ToDateTime( dtprod.Text); //System.DateTime.ParseExact(dtprod.Text, "M/d/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime _dtexpire = Convert.ToDateTime( dtexp.Text); // System.DateTime.ParseExact(dtexp.Text, "M/d/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            arr.Clear();
            arr.Add(new cArrayList("@item_cd", lbitemcodewazaran.Text));
            arr.Add(new cArrayList("@receipt_no", _grn_no));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
            arr.Add(new cArrayList("@qty", lbqty.Text));
            arr.Add(new cArrayList("@qty2", 0));
            arr.Add(new cArrayList("@qty_shipment", lbqty.Text));
            arr.Add(new cArrayList("@uom", ((lbuom.Text=="CAR")?"CTN":lbuom.Text)));
            arr.Add(new cArrayList("@uom2", _uom2));
            arr.Add(new cArrayList("@whs_cd", cbwhs.SelectedValue));
            arr.Add(new cArrayList("@bin_cd", cbbin.SelectedValue));
            arr.Add(new cArrayList("@expire_date", _dtexpire));
            arr.Add(new cArrayList("@prod_date", _dtprod));
            arr.Add(new cArrayList("@lot_no", lblotno.Text));
            bll.vInsertGoodReceiptDtl(arr);
        }
        cd.v_showcontrol(btprint);
        cd.v_hiddencontrol(btreceived);
        cd.v_disablecontrol(cbwhs);
        cd.v_disablecontrol(cbbin);
        cd.v_disablecontrol(dtwarehouse);
        cd.v_disablecontrol(grd);
        cd.v_disablecontrol(grddetail);
        cd.v_disablecontrol(grddetailnormal);
        cd.v_disablecontrol(txdrivername);
        cd.v_disablecontrol(dtreceipt);
        cd.v_disablecontrol(txremark);
        cd.v_disablecontrol(txitemsearch);
        cd.v_disablecontrol(txqty);
        cd.v_disablecontrol(cbuom);
        cd.v_disablecontrol(grddetailnormal);
        //vBindingGrid();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
            "sweetAlert('GDN has been received','GRN No." + _grn_no + "','success');", true);
    }

    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_goodreceived.aspx");
    }

    protected void cbwhs_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@whs_cd", cbwhs.SelectedValue));
        arr.Add(new cArrayList("@qry_cd", "bin_goodreceived"));
        bll.vBindingComboToSpWithEmptyChoosen(ref cbbin, "sp_twarehouse_bin_getbyquery", "bin_cd", "bin_nm", arr);
        cd.v_enablecontrol(cbbin);
        cd.v_disablecontrol(cbwhs);
        if (cbtypeofstockin.SelectedValue == "SID")
        {
            cd.v_enablecontrol(txitemsearch);
            cd.v_enablecontrol(txqty);
            cd.v_enablecontrol(cbuom);
            cd.v_enablecontrol(btadd);
        }
        else if (cbtypeofstockin.SelectedValue == "NAV")
        {
            cd.v_showcontrol(btreceived);
        }
    }

    protected void grddetail_DataBound(object sender, EventArgs e)
    {

    }

    protected void grddetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbitemcodenav = (Label)e.Row.FindControl("lbitemcodenav");
            Label lbitemcodewazaran = (Label)e.Row.FindControl("lbitemcodewazaran");
           
          
            TextBox dtprod = (TextBox)e.Row.FindControl("dtprod");
            TextBox dtexp = (TextBox)e.Row.FindControl("dtexp");
            cd.v_disablecontrol(dtprod);cd.v_disablecontrol(dtexp);
            //dtexp.Text = Request.Cookies["waz_dt"].Value;
            //dtprod.Text = Request.Cookies["waz_dt"].Value;
            lbitemcodewazaran.Text = bll.vLookUp("select item_cd from tmst_item where item_cd_nav='" + lbitemcodenav.Text + "'");
            Label lbsize = (Label)e.Row.FindControl("lbsize");
            string _size = bll.vLookUp("select size from tmst_item where item_cd='" + lbitemcodewazaran.Text + "'");
            lbsize.Text = _size;
        }
    }

    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=grnav&n=" + txgrnno.Text + "');", true);
    }

    protected void cbtypeofstockin_SelectedIndexChanged(object sender, EventArgs e)
    {
        // dtprod.Text = Request.Cookies["waz_dt"].Value;
        List<cArrayList> arr = new List<cArrayList>();
        DateTime _dtprod = System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime _expired = _dtprod.AddDays(1);
        dtprod_CalendarExtender.StartDate = _dtprod.AddDays(-10);
        dtexpire_CalendarExtender.StartDate = _expired;
        // dtexpire.Text 
        if (cbtypeofstockin.SelectedValue == "NAV")
        {
            dtprod.Text = _dtprod.ToString("d/M/yyyy");
            dtexpire.Text = _expired.ToString("d/M/yyyy");
            cd.v_hiddencontrol(pnlgoodreceivedbranch);
            cd.v_showcontrol(pnlnav);
            cd.v_disablecontrol(btadd);
            cd.v_showcontrol(pnlgrdnav);
            cd.v_hiddencontrol(pnlgrdnormal);
            cd.v_hiddencontrol(btsavestockin);
            cd.v_disablecontrol(dtprod);
            cd.v_disablecontrol(dtexpire);
            cd.v_disablecontrol(txqty);
            //cd.v_disablecontrol(txqty2);
            cd.v_enablecontrol(txdrivername);
            vBindingGrid();
            string _defaultvendor = bll.sGetControlParameter("vendordefault");
            cbvendor.SelectedValue = _defaultvendor;
            cd.v_disablecontrol(cbvendor);
            cd.v_enablecontrol(btsearch);

        }
        else if (cbtypeofstockin.SelectedValue == "BTF")
        {
            cd.v_hiddencontrol(pnlnav);
            cd.v_showcontrol(pnlgoodreceivedbranch);
            cd.v_disablecontrol(btadd);
            grd.DataSource = null;
            grd.DataBind();
            grddetail.DataSource = null;
            grddetail.DataBind();
            grddetailnormal.DataSource = null;
            grddetailnormal.DataBind();
            cd.v_enablecontrol(txdrivername);
        }
        else if (cbtypeofstockin.SelectedValue == "SID")
        {
            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
            bll.vBindingComboToSpWithEmptyChoosen(ref cbwhs, "sp_tmst_warehouse_get", "whs_cd", "whs_nm", arr);
            dtprod.Text = _dtprod.ToString("d/M/yyyy");
            dtexpire.Text = _expired.ToString("d/M/yyyy");
            grd.DataSource = null;
            grd.DataBind();
            grddetail.DataSource = null;
            grddetail.DataBind();
            grddetailnormal.DataSource = null;
            grddetailnormal.DataBind();
            //bll.vBindingComboToSpWithEmptyChoosen(ref cbvendor, "sp_tmst_vendor_get", "vendor_cd", "vendor_nm");
            cd.v_showcontrol(pnlgrdnormal);
            cd.v_showcontrol(pnlgrdnav);
            //cd.v_enablecontrol(txitemsearch);
            //cd.v_enablecontrol(txqty);
            //cd.v_enablecontrol(btadd);
            cd.v_enablecontrol(cbwhs);
            cd.v_enablecontrol(cbbin);
            cd.v_showcontrol(btsavestockin);
            cd.v_enablecontrol(btsearch);
            cd.v_enablecontrol(txdrivername);
            cd.v_enablecontrol(cbvendor);
        }
        cd.v_disablecontrol(txgdnav);
        dtwarehouse.ReadOnly = true;
        dtreceipt.ReadOnly = true;
        lbcaption.Text = cbtypeofstockin.SelectedItem.Text;
        cd.v_disablecontrol(cbtypeofstockin);
    }

    protected void btitem_Click(object sender, EventArgs e)
    {
        string _uom = bll.vLookUp("select uom_base from tmst_item where item_cd='" + hditem.Value + "'");
        //lbuom2.Text = _uom;
        //txqty2.Text = "0";

        dtprod.ReadOnly = true;
        dtexpire.ReadOnly = true;
        cd.v_enablecontrol(cbuom);
    }

    protected void btadd_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);

        if (Session["tgoodreceipt_dtl"] == null)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
           "sweetAlert('Your session expired !','Please start from beginning','warning');", true);
            return;
        }
        if (txdrivername.Text == String.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
             "sweetAlert('Please entry driver name !','Driver name','warning');", true);
            return;
        }
        if (cbvendor.SelectedValue == String.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
             "sweetAlert('Please select vendor !','Vendor selection','warning');", true);
            return;
        }
        if (dtwarehouse.Text == String.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select date received !','Actual received by warehouse','warning');", true);
            return;
        }

        if (cbwhs.SelectedValue == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select warehouse!','Warehouse','warning');", true);
            return;
        }

        if (cbbin.SelectedValue == String.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select bin!','Bin','warning');", true);
            return;
        }
        if (cbuom.SelectedValue == String.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select UOM!','UOM','warning');", true);
            return;
        }

        decimal _qty = 0;

        if (!decimal.TryParse(txqty.Text, out _qty))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please put correct qty!','Check Qty','warning');", true);
            return;
        }


        if (dtprod.Text == String.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please production date!','Prod Date','warning');", true);
            return;
        }
        if (dtexpire.Text == String.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please expiration date!','Expire Date','warning');", true);
            return;
        }

        if (txlotno.Text == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please entry lot number!','Lot','warning');", true);
            return;
        }
        //DateTime _prod = System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //dtprod_CalendarExtender.StartDate = _prod.AddDays(-10);
        //DateTime _expire = _prod.AddDays(90);
        //dtexpire_CalendarExtender.StartDate = _expire;
        DateTime _prod = DateTime.ParseExact(dtprod.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime _expire = DateTime.ParseExact(dtexpire.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);

        if (_expire < _prod)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Expired date can not less than product date!','Expire Date','warning');", true);
            return;
        }
        bool _checkedit = false;
        List<tgoodreceipt_dtl> _tgoodreceipt_dtl = (List<tgoodreceipt_dtl>)Session["tgoodreceipt_dtl"];
        decimal _totqty = 0;
        foreach (tgoodreceipt_dtl _tp in _tgoodreceipt_dtl)
        {

            if (_tp.item_cd == hditem.Value)
            {
                _tp.qty = Convert.ToDecimal(txqty.Text);
                _tp.qty2 = 0;
                _tp.uom = cbuom.SelectedValue;
                _tp.uom2 = "PCS";
                _checkedit = true;
                _tp.lot_no = txlotno.Text;
            }
            //_totqty += _tp.qty;
        }
        if (!_checkedit)
        {
            _tgoodreceipt_dtl.Add(new tgoodreceipt_dtl
            {
                bin_cd = cbbin.SelectedValue,
                whs_cd = cbwhs.SelectedValue,
                expire_date = System.DateTime.ParseExact(dtexpire.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                prod_date = System.DateTime.ParseExact(dtprod.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                qty = Convert.ToDecimal(txqty.Text),
                qty_received = Convert.ToDecimal(txqty.Text),
                receipt_no = hdreceipt.Value,
                salespointcd = Request.Cookies["sp"].Value,
                stock_typ = "I",
                uom = cbuom.SelectedValue,
                item_cd = hditem.Value,
                item_nm = txitemsearch.Text,
                branded_nm = bll.vLookUp("select branded_nm from tmst_item where item_cd='" + hditem.Value + "'"),
                size = bll.vLookUp("select size from tmst_item where item_cd='" + hditem.Value + "'"),
                uom2 = "PCS",
                qty2 = 0,
                lot_no = txlotno.Text
            }); ;
        }
        foreach (tgoodreceipt_dtl _d in _tgoodreceipt_dtl)
        {
            _totqty += _d.qty;
        }
        Session["tgoodreceipt_dtl"] = _tgoodreceipt_dtl;
        grddetailnormal.DataSource = _tgoodreceipt_dtl;
        grddetailnormal.DataBind();
        Label lbtotqty = (Label)grddetailnormal.FooterRow.FindControl("lbtotalqty");
        lbtotqty.Text = _totqty.ToString("N2");
        txitemsearch.Text = String.Empty;
        txqty.Text = String.Empty;
        cbuom.SelectedValue = String.Empty;
        dtexpire.Text = _expire.ToString("d/M/yyyy");
        dtprod.Text = _prod.ToString("d/M/yyyy");
        //lbuom2.Text = String.Empty;
        //txqty2.Text = String.Empty;
        //cd.v_disablecontrol(dtprod);
        //cd.v_disablecontrol(dtexpire);
        cd.v_disablecontrol(cbwhs);
        cd.v_disablecontrol(cbbin);
        cd.v_enablecontrol(txdrivername);
        cd.v_disablecontrol(cbvendor);
    }

    protected void grddetailnormal_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        List<tgoodreceipt_dtl> _tgoodreceipt_dtl = (List<tgoodreceipt_dtl>)Session["tgoodreceipt_dtl"];

        _tgoodreceipt_dtl.RemoveAt(e.RowIndex);
        grddetailnormal.DataSource = _tgoodreceipt_dtl;
        grddetailnormal.DataBind();

        Session["tgoodreceipt_dtl"] = _tgoodreceipt_dtl;
    }

    protected void btsavestockin_Click(object sender, EventArgs e)
    {

        if (cbtypeofstockin.SelectedValue == String.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                  "sweetAlert('Stockin type must be select !','Stockin type','warning');", true);
            return;
        }

        if (txdrivername.Text == String.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Driver must entry!','Please entry driver name','warning');", true);
            return;
        }
        if (cbvendor.SelectedValue == String.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
             "sweetAlert('Please select vendor !','Vendor selection','warning');", true);
            return;
        }
        if (grddetailnormal.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('No item selected!','Please select item to be stockin','warning');", true);
            return;
        }
        if (cbwhs.SelectedValue == String.Empty || cbbin.SelectedValue == String.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                 "sweetAlert('Warehouse or bin must be selected !','Warehouse and bin','warning');", true);
            return;
        }

        //if (txlotno.Text == string.Empty)
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
        //        "sweetAlert('Lot no must be filled !','Lot number','warning');", true);
        //    return;
        //}
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        arr.Add(new cArrayList("@do_no", string.Empty));
        arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value));
        arr.Add(new cArrayList("@source", cbtypeofstockin.SelectedValue));
        arr.Add(new cArrayList("@receipt_dt", System.DateTime.ParseExact(dtreceipt.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@register_dt", System.DateTime.ParseExact(dtwarehouse.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@driver_nm", txdrivername.Text));
        arr.Add(new cArrayList("@vendor_cd", cbvendor.SelectedValue));
        //arr.Add(new cArrayList("@lot_no", txlotno.Text));   
        string _receipt_no = string.Empty;

        bll.vInsertMstGoodReceipt(arr, ref _receipt_no);
        List<tgoodreceipt_dtl> _tgoodreceipt_dtl = (List<tgoodreceipt_dtl>)Session["tgoodreceipt_dtl"];
        foreach (tgoodreceipt_dtl _tp in _tgoodreceipt_dtl)
        {
            arr.Clear();
            arr.Add(new cArrayList("@item_cd", _tp.item_cd));
            arr.Add(new cArrayList("@qty", _tp.qty));
            arr.Add(new cArrayList("@uom", _tp.uom));
            arr.Add(new cArrayList("@receipt_no", _receipt_no));
            arr.Add(new cArrayList("@salespointcd", _tp.salespointcd));
            arr.Add(new cArrayList("@whs_cd", _tp.whs_cd));
            arr.Add(new cArrayList("@bin_cd", _tp.bin_cd));
            arr.Add(new cArrayList("@qty_shipment", _tp.qty_received));
            arr.Add(new cArrayList("@expire_date", _tp.expire_date));
            arr.Add(new cArrayList("@prod_date", _tp.prod_date));
            arr.Add(new cArrayList("@qty2", _tp.qty2));
            arr.Add(new cArrayList("@uom2", _tp.uom2));
            arr.Add(new cArrayList("@lot_no", _tp.lot_no));
            bll.vInsertGoodReceiptDtl(arr);

        }
        txgrnno.Text = _receipt_no;
        cd.v_showcontrol(btprint);
        cd.v_disablecontrol(dtexpire);
        cd.v_disablecontrol(dtprod);
        cd.v_disablecontrol(txitemsearch);
        cd.v_disablecontrol(txqty);
        cd.v_disablecontrol(grddetailnormal);
        cd.v_hiddencontrol(btsavestockin);
        cd.v_disablecontrol(cbuom);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Stockin save successfully!','Stock In Without GDN Factory No." + _receipt_no + "','success');", true);
    }

    protected void grddetailnormal_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbitemcode = (Label)e.Row.FindControl("lbitemcode");
            Label lbsize = (Label)e.Row.FindControl("lbsize");
            string _size = bll.vLookUp("select size from tmst_item where item_cd='"+lbitemcode.Text+"'");
            lbsize.Text = _size;
            foreach (LinkButton button in e.Row.Cells[9].Controls.OfType<LinkButton>())
            {
                if ((button.CommandName == "Edit") || (button.CommandName == "Delete"))
                {
                    button.Attributes.Add("OnClick", "ShowProgress();");
                }
            }
        }
    }

    protected void btlookup_Click(object sender, EventArgs e)
    {
        txgrnno.Text = hdreceipt.Value;
        DateTime _dtreceipt = Convert.ToDateTime(bll.vLookUp("select receipt_dt from tmst_goodreceipt where receipt_no='" + hdreceipt.Value + "'"));
        DateTime _dtregister = Convert.ToDateTime(bll.vLookUp("select register_dt from tmst_goodreceipt where receipt_no='" + hdreceipt.Value + "'"));
        string _source = bll.vLookUp("select source from tmst_goodreceipt where receipt_no='" + hdreceipt.Value + "'");
        string _drivername = bll.vLookUp("select driver_nm from tmst_goodreceipt where receipt_no='" + hdreceipt.Value + "'");
        string _dono = bll.vLookUp("select do_no from tmst_goodreceipt where receipt_no='" + hdreceipt.Value + "'");
        cbtypeofstockin.SelectedValue = _source;
        txdrivername.Text = _drivername;
        dtreceipt.Text = _dtreceipt.ToString("d/M/yyyy");
        dtwarehouse.Text = _dtregister.ToString("d/M/yyyy");
        txgdnav.Text = _dono;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@receipt_no", hdreceipt.Value));
        bll.vBindingGridToSp(ref grddetailnormal, "sp_tgoodreceipt_dtl_get", arr);
        cd.v_disablecontrol(cbtypeofstockin);
        cd.v_disablecontrol(dtwarehouse);
        cd.v_disablecontrol(dtreceipt);
        cd.v_enablecontrol(txdrivername);
        cd.v_showcontrol(btprint);
        cd.v_hiddencontrol(btsavestockin);
        cd.v_hiddencontrol(btreceived);
        cd.v_disablecontrol(txgrnno);
        cd.v_disablecontrol(cbwhs);
        cd.v_disablecontrol(cbbin);
        cd.v_disablecontrol(txgdnav);
        if (_dono == String.Empty || _dono == null)
        {
            cd.v_enablecontrol(btgdnnav);
        }
    }

    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        List<nav_header> _nav_header = (List<nav_header>)Session["nav_header"];
        grd.PageIndex = e.NewPageIndex;
        grd.DataSource = _nav_header; 
        grd.DataBind();
        Session["nav_header"] = _nav_header;
    }

    protected void cbvendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        cd.v_disablecontrol(cbvendor);
    }

    protected void cbbin_SelectedIndexChanged(object sender, EventArgs e)
    {
        cd.v_disablecontrol(cbbin);
    }

    protected void btsearchgdn_Click(object sender, EventArgs e)
    {
        vBindingGrid();
    }
}