using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_goodreceipt_clr : System.Web.UI.Page
{
    cbll bll = new cbll();
    List<nav_header> _nav_header = new List<nav_header>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //List<nav_header> _nav_header = lNavHeader(); //new List<nav_header>();
            _nav_header = lNavHeader();
            bll.vBindingGridToSp(ref grd, "sp_tmst_goodreceipt_getbynogdn");
            //Session["nav_header"] = _nav_header;
        }
    }

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
        string _url = "http://192.168.1.123:8058/SAWAMAG_API/OData/SalesHeader?$format=json";
        List<root_header> json_filter = new List<root_header>();
        using (WebClient wc = new WebClient())
        {

            try
            {

                // wc.BaseAddress = _url;
                string _token = bll.sGetControlParameterSalespoint("token_nav", Request.Cookies["sp"].Value);
                //wc.Credentials = new NetworkCredential("NAVAPI", "mBU0bV3/cCxe/y21KvwuGFkyxnwaxmwg4M2XxkKZJ3M=");
                wc.Credentials = new NetworkCredential("NAVAPI", _token);
                ServicePointManager.ServerCertificateValidationCallback += (senderX, certificate, chain, sslPolicyErrors) => true;
                var _data = wc.DownloadString(_url); //+ _url_do_header);

                if (!_data.Equals(null))
                {
                    root_header json = JsonConvert.DeserializeObject<root_header>(_data);
                    _Value = (List<nav_header>)json.value;
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
                            Response.Redirect("fm_goodreceipt.aspx?err=fm_goodreceipt_nav", false);
                            break;
                    }
                }
            }
        }
        return _Value; //json_filter;
    }

    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        List<nav_header> _nav_header = (List<nav_header>)Session["nav_header"];
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            bool _check = true;
            List<cArrayList> arr = new List<cArrayList>();
            DropDownList cbnav = (DropDownList)e.Row.FindControl("cbnav");
            ListItem itemx = new ListItem();
            itemx.Value = string.Empty;
            itemx.Text = "----Select----";
            cbnav.Items.Add(itemx);
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
            List<tmst_goodreceipt> _tmst_goodreceipt = bll.lMstGoodReceipt(arr);
            //List<nav_header> _nav_header = lNavHeader();
            foreach (nav_header _nv in _nav_header)
            {
                foreach (tmst_goodreceipt _r in _tmst_goodreceipt)
                {
                    if (_r.do_no == _nv.No)
                    {
                        _check = false;
                    }
                }
                if (_check)
                {
                    ListItem item = new ListItem();
                    item.Value = _nv.No;
                    item.Text = _nv.No;
                    cbnav.Items.Add(item);
                }
                _check = true;
            }
        }
            //}
            //Session["nav_header"] = _nav_header;
        }

    protected void btpost_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        DropDownList cbnav = (DropDownList)row.FindControl("cbnav");
        Label lbgrnno = (Label)row.FindControl("lbreceiptno");
        string _sql = "update tmst_goodreceipt set do_no='" + cbnav.SelectedValue + "' where receipt_no='" + lbgrnno.Text + "'";
        bll.vExecuteSQL(_sql);
        List<nav_header> _nav_header = lNavHeader();
        bll.vBindingGridToSp(ref grd, "sp_tmst_goodreceipt_getbynogdn");
        cd.v_disablecontrol(grd);
        cd.v_disablecontrol(grddtlnav);
        cd.v_disablecontrol(grdgrdtl);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
            "sweetAlert('Clereance GRN has been completed !','" + lbgrnno.Text + "','success');", true);
    }


    public List<nav_detail2> lNavDetail()
    {
        List<nav_detail2> _Value = new List<nav_detail2>();
        List<root_detail> _tmp = new List<root_detail>();
        //List<> _filteredvalue = new List<Value>();
        string _do_no = string.Empty;
        //string _url = bll.sGetControlParameter("api_external");
        //string _url_do_header = bll.sGetFieldValue("api_do", "api_do_all") + Request.Cookies["sp"].Value.ToString();
        // string _url = "http://192.168.120.123:8058/SAWAMAG_API/OData/SalesLine?$format=json&$filter=Posting_Date%20gt%20datetime%272022-10-01%27";
        //string _url = @"http://192.168.120.123:8058/SAWAMAG_API/OData/SalesLine?$format=json&$filter=Posting_Date%20gt%20datetime%272022-10-01%27";
        string _url = "http://192.168.1.123:8058/SAWAMAG_API/OData/SalesLine?$format=json";
        List<root_detail> json_filter = new List<root_detail>();
        using (WebClient wc = new WebClient())
        {

            try
            {

                // wc.BaseAddress = _url;
                string _token = bll.sGetControlParameterSalespoint("token_nav", Request.Cookies["sp"].Value);
                //wc.Credentials = new NetworkCredential("NAVAPI", "mZ01oj751WiM5M3TuDY/gIImjgSRZPN5d9b1ISfHXhk=");
                wc.Credentials = new NetworkCredential("NAVAPI", _token);
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
        Label lbreceiptno = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbreceiptno");
        DropDownList cbnav = (DropDownList)grd.Rows[e.NewSelectedIndex].FindControl("cbnav");
        List<cArrayList> arr = new List<cArrayList>();
        List<nav_header> _nav_header = (List<nav_header>)Session["nav_header"];
        List<nav_header> _selectedheader = new List<nav_header>();
        arr.Add(new cArrayList("@receipt_no", lbreceiptno.Text));
        bll.vBindingGridToSp(ref grdgrdtl, "sp_tgoodreceipt_dtl_get", arr);

        List<nav_detail2> _nav_detail = new List<nav_detail2>();
        foreach (nav_header _h in _nav_header)
        {
            if (cbnav.SelectedValue == _h.No)
            {
                _selectedheader.Add(_h);
            }
        }
        //grd.DataSource = _selectedheader;
        //grd.DataBind();

        List<nav_detail2> _filtereddetail = new List<nav_detail2>();
        List<nav_detail2> _nav_detail2 = lNavDetail();
        foreach (nav_detail2 _h in _nav_detail2)
        {
            if (_h.Document_No == cbnav.SelectedValue)
            {
                _filtereddetail.Add(_h);
            }
        }
        grddtlnav.DataSource = _filtereddetail;
        grddtlnav.DataBind();
    }

    protected void grddtlnav_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbitemcode = (Label)e.Row.FindControl("lbitemcode");
            Label lbitemnav = (Label)e.Row.FindControl("lbitemnav");
            Label lbitemname = (Label)e.Row.FindControl("lbitemname");
            string _itemcode = bll.vLookUp("select item_cd from tmst_item where item_cd_nav='" + lbitemnav.Text + "'");
            string _itemname = bll.vLookUp("select item_nm from tmst_item where item_cd='" + _itemcode + "'");
            lbitemcode.Text = _itemcode;
            lbitemname.Text = _itemname;
        }
    }

    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_goodreceipt_clr.aspx");
    }

    protected void cbnav_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList cbo = (DropDownList)sender;
        GridViewRow row = (GridViewRow)cbo.NamingContainer;

        Label lbreceiptno = (Label)row.FindControl("lbreceiptno");
        DropDownList cbnav = (DropDownList)row.FindControl("cbnav");
        List<cArrayList> arr = new List<cArrayList>();
        List<nav_header> _nav_header = (List<nav_header>)Session["nav_header"];
        List<nav_header> _selectedheader = new List<nav_header>();
        arr.Add(new cArrayList("@receipt_no", lbreceiptno.Text));
        bll.vBindingGridToSp(ref grdgrdtl, "sp_tgoodreceipt_dtl_get", arr);

        List<nav_detail2> _nav_detail = new List<nav_detail2>();
        foreach (nav_header _h in _nav_header)
        {
            if (cbnav.SelectedValue == _h.No)
            {
                _selectedheader.Add(_h);
            }
        }
        //grd.DataSource = _selectedheader;
        //grd.DataBind();

        List<nav_detail2> _filtereddetail = new List<nav_detail2>();
        List<nav_detail2> _nav_detail2 = lNavDetail();
        foreach (nav_detail2 _h in _nav_detail2)
        {
            if (_h.Document_No == cbnav.SelectedValue)
            {
                _filtereddetail.Add(_h);
            }
        }
        grddtlnav.DataSource = _filtereddetail;
        grddtlnav.DataBind();
    }
}