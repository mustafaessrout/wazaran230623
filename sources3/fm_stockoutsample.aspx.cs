using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
public partial class fm_stockoutsample : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string _salespointcd = Request.Cookies["sp"].Value;

            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", _salespointcd));
            bll.vBindingComboToSpWithEmptyChoosen(ref cbwhs, "sp_tmst_warehouse_get", "whs_cd", "whs_nm", arr);
            bll.vBindingFieldValueToComboWithChoosen(ref cbuom, "uom");
            bll.vBindingFieldValueToComboWithChoosen(ref cbsampleoutby, "sampleoutby");
            arr.Clear();
            arr.Add(new cArrayList("@reasn_typ", "stockout"));
            bll.vBindingComboToSpWithEmptyChoosen(ref cbreason, "sp_tmst_reason_get", "reasn_cd", "reasn_nm", arr);
            List<tstocksample_dtl> _tstocksample_dtl = new List<tstocksample_dtl>();
            Session["tstocksample_dtl"] = _tstocksample_dtl;
            dtsample.Text = Request.Cookies["waz_dt"].Value;
            hditem.Value = string.Empty;
            hdemp.Value = string.Empty;
            hdcust.Value = string.Empty;
            cd.v_disablecontrol(dtsample);
            cd.v_hiddencontrol(btsave);
            cd.v_hiddencontrol(btprint);
            //cd.v_disablecontrol(txcustomer);
            cd.v_disablecontrol(lbsampleno);
            cd.v_disablecontrol(cbwhs);
            cd.v_disablecontrol(cbbin);
            cd.v_disablecontrol(cbuom);
            cd.v_disablecontrol(txqty);
            cd.v_hiddencontrol(btadd);
            cd.v_disablecontrol(txitem);
            cd.v_disablecontrol(txsamplename);
            cd.v_disablecontrol(cbreason);
            cd.v_disablecontrol(cbsampleoutby);
            cd.v_disablecontrol(txrefno);
            cd.v_hiddencontrol(txcustomer);
            cd.v_showcontrol(txother);
            chcust.Checked = false;
        }
    }

    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_stockoutsample.aspx");
    }

    protected void btadd_Click(object sender, EventArgs e)
    {
        if (txrefno.Text == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please fill reference no!','Reference No','warning');", true);
            return;
        }
        if (hdemp.Value == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select PIC!','Person In Charge','warning');", true);
            return;
        }
        if (txsamplename.Text == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please fill sample name!','Sample Name','warning');", true);
            return;
        }
        if (cbreason.SelectedValue == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select reason!','Reason','warning');", true);
            return;
        }
        if (cbsampleoutby.SelectedValue == String.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please sample taken by!','Taken out by','warning');", true);
            return;
        }
        if (cbwhs.SelectedValue == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select warehouse!','Warehouse','warning');", true);
            return;
        }
        if (cbbin.SelectedValue == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select BIN!','Bin Warehouse','warning');", true);
            return;
        }

        if (hditem.Value == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select item !','Item','warning');", true);
            return;
        }
        decimal _stockavl = Convert.ToDecimal(lbstockavl.Text);
        decimal _qty = 0;

        if (_stockavl == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Stock is not enough !','Stock zero','warning');", true);
            return;
        }

        if (!decimal.TryParse(txqty.Text, out _qty))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please entry correct qty request!','Qty','warning');", true);
            return;
        }
        if (cbsampleoutby.SelectedValue == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select sample out!','Sample Out By,'warning');", true);
            return;
        }
        bool _check = false;
        List<tstocksample_dtl> _tstocksample_dtl = (List<tstocksample_dtl>)Session["tstocksample_dtl"];
        foreach (tstocksample_dtl _dtl in _tstocksample_dtl)
        {
            if (_dtl.item_cd == hditem.Value)
            {
                _check = true;
                _dtl.qty = Convert.ToDecimal(txqty.Text);
            }
        }
        if (!_check)
        {
            _tstocksample_dtl.Add(new tstocksample_dtl
            {
                whs_cd = cbwhs.SelectedValue,
                bin_cd = cbbin.SelectedValue,
                whs_nm = bll.vLookUp("select whs_nm from tmst_warehouse where whs_cd='" + cbwhs.SelectedValue + "'"),
                bin_nm = bll.vLookUp("select bin_nm from twarehouse_bin where whs_cd='" + cbwhs.SelectedValue + "' and bin_cd='" + cbbin.SelectedValue + "'"),
                item_cd = hditem.Value,
                item_nm = bll.vLookUp("select item_nm from tmst_item where item_cd='" + hditem.Value + "'"),
                size = bll.vLookUp("select size from tmst_item where item_cd='" + hditem.Value + "'"),
                qty = _qty,
                uom = cbuom.SelectedValue,
                stockavl = Convert.ToDecimal(lbstockavl.Text),
                sample_cd = lbsampleno.Text,
                qtysent = 0
            });
        }
        grd.DataSource = _tstocksample_dtl;
        grd.DataBind();
        Session["tstocksample_dtl"] = _tstocksample_dtl;
        cbwhs.SelectedValue = string.Empty;
        cbbin.SelectedValue = string.Empty;
        //cbbin.SelectedValue = string.Empty;
        hditem.Value = string.Empty;
        txitem.Text = string.Empty;
        txqty.Text = string.Empty;
        cbuom.SelectedValue = string.Empty;
        lbstockavl.Text = string.Empty;
        cd.v_enablecontrol(cbbin);
        cd.v_enablecontrol(txitem);
        cd.v_showcontrol(btsave);
        cd.v_disablecontrol(txrefno);
        cd.v_disablecontrol(txsamplename);
        cd.v_disablecontrol(chcust);
        cd.v_disablecontrol(cbsampleoutby);
        cd.v_disablecontrol(cbreason);
    }
    protected void cbwhs_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@whs_cd", cbwhs.SelectedValue));
        bll.vBindingComboToSpWithEmptyChoosen(ref cbbin, "sp_twarehouse_bin_get", "bin_cd", "bin_nm", arr);
        cd.v_enablecontrol(cbbin);
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetEmployeeList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lEmployee = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string sEmployee = string.Empty;
        arr.Add(new cArrayList("@salespointcd", contextKey));
        arr.Add(new cArrayList("@emp_nm", prefixText));
        //bll.vSearchMstEmployeeByPIC(arr, ref rs);
        bll.vSearchMstEmployee(arr, ref rs);
        while (rs.Read())
        {
            sEmployee = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["emp_cd"].ToString() + "-" + rs["emp_nm"], rs["emp_cd"].ToString());
            lEmployee.Add(sEmployee);
        }
        return (lEmployee.ToArray());
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
        //IAG : 22-Feb-2019 ---> Modify only item in warehouse only will show
        //if (!string.IsNullOrEmpty(contextKey))
        //{
        //    arr.Add(new cArrayList("@whs_cd", contextKey));
        //}
        bll.vSearchMstItemBySalespoint(arr, ref rs);
        while (rs.Read())
        {
            sItem = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["item_cd"].ToString() + "-" + rs["item_nm"].ToString() + "-" + rs["size"].ToString() + "-" + rs["branded_nm"].ToString(), rs["item_cd"].ToString());
            lItem.Add(sItem);

        }
        rs.Close();
        return (lItem.ToArray());
    }

    protected void btitem_Click(object sender, EventArgs e)
    {
        //cbuom.SelectedValue = bll.vLookUp("select uom_base from tmst_item where item_cd='" + hditem.Value + "'");
        //cbwhs.SelectedValue = bll.vLookUp("select dbo.fn_getproductwarehouse('" + hditem.Value + "','" + Request.Cookies["sp"].Value + "')");
        //cbwhs_SelectedIndexChanged(sender, e);

        cbuom.SelectedValue = string.Empty;
        txqty.Text = string.Empty;
        cbwhs.SelectedValue = string.Empty;
        cbuom.SelectedValue = string.Empty;
        lbstockavl.Text = string.Empty;
        txqty.Text = string.Empty;
        cd.v_disablecontrol(cbbin);

        cd.v_disablecontrol(cbuom);
        cd.v_enablecontrol(cbwhs);

    }

    protected void cbbin_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DateTime _dt = System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //string _stockavl = bll.vLookUp("select dbo.[sfnGetStockBooking]('" + Request.Cookies["sp"].Value + "','" + hditem.Value.ToString() + "','" + cbbin.SelectedValue + "','" + cbwhs.SelectedValue + "','DEPO','" + _dt + "')");//by yanto 25-6-2016 
        //lbstockavl.Text = _stockavl;
        //cd.v_enablecontrol(txqty);
        cd.v_enablecontrol(cbuom);
        cd.v_showcontrol(btadd);
    }

    protected void btsave_Click(object sender, EventArgs e)
    {
        if (grd.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please item to be sample!','select item','warning');", true);
            return;
        }

        if (chcust.Checked)
        {
            if (hdcust.Value == string.Empty)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select customer !','Customer Choosen','warning');", true);
                return;
            }
        }
        else
        {
            if (txother.Text == String.Empty)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please fill destination for sample !','Instead Of Customer','warning');", true);
                return;
            }
        }

        if (cbreason.SelectedValue == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select reason !','Reason Choosen','warning');", true);
            return;
        }
       
        string _item = string.Empty;
        List<tstocksample_dtl> _tstocksample_dtl = (List<tstocksample_dtl>)Session["tstocksample_dtl"];
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@sample_nm", txsamplename.Text));
        arr.Add(new cArrayList("@sample_dt", System.DateTime.ParseExact(dtsample.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@ref_no", txrefno.Text));
        arr.Add(new cArrayList("@remark", txsamplename.Text));
        arr.Add(new cArrayList("@created_dt", System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@responsible_by", hdemp.Value));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        arr.Add(new cArrayList("@sample_sta_id", "N"));
        arr.Add(new cArrayList("@sampleoutby", cbsampleoutby.SelectedValue));
        arr.Add(new cArrayList("@reasn_cd", cbreason.SelectedValue));
        if (chcust.Checked)
        {
            arr.Add(new cArrayList("@cust_cd", hdcust.Value));
        }
        else
        {
            arr.Add(new cArrayList("@cust_cd", txother.Text));
        }
        string _sample_cd = string.Empty;
        bll.vInsertStockSample(arr, ref _sample_cd);

        lbsampleno.Text = _sample_cd;
        foreach (tstocksample_dtl _dtl in _tstocksample_dtl)
        {
            arr.Clear();
            arr.Add(new cArrayList("@item_cd", _dtl.item_cd));
            arr.Add(new cArrayList("@qty", _dtl.qty));
            arr.Add(new cArrayList("@stockavl", _dtl.stockavl));
            arr.Add(new cArrayList("@uom", _dtl.uom));
            arr.Add(new cArrayList("@whs_cd", _dtl.whs_cd));
            arr.Add(new cArrayList("@bin_cd", _dtl.bin_cd));
            arr.Add(new cArrayList("@sample_cd", _sample_cd));
            arr.Add(new cArrayList("@qtysent", _dtl.qtysent));
            bll.vInsertStockSampleDtl(arr);
            if (_item == string.Empty)
            {
                _item += _dtl.item_cd + " | " + _dtl.item_nm + " | " + _dtl.size + " | " + _dtl.qty.ToString() + " | " + _dtl.uom;
            }
            else
            {
                _item += "%0D%0C" + _dtl.item_cd + " | " + _dtl.item_nm + " | " + _dtl.size + " | " + _dtl.qty.ToString() + " | " + _dtl.uom;
            }
        }
        Random _rnd = new Random();
        Int32 _token = _rnd.Next(100000, 999999);
        string _tokensent = _token.ToString();
        string _wa = "#New request sample has been created with no. *" + _sample_cd + "* , Purpose: *" + txsamplename.Text;
        _wa += "* %0D%0ADetail request : " + _item + "%0D%0A";
        _wa += "Please approve with reply *Y" + _tokensent + "* , and reject with reply *N" + _tokensent + "*";
        //if (_salespointho == Request.Cookies["sp"].Value)
        //{
        //    _wa += "Please continue to stock out from warehouse !";
        //}
        //else
        //{
        //    _wa += "Please approve with reply *Y" + _tokensent + "* , and reject with reply *N" + _tokensent + "*";
        //}
        //arr.Clear();
        //arr.Add(new cArrayList("@qry_cd", "bm"));
        //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        string _bm = string.Empty;
        List<tuser_profiles_approval> _approval = bll.luserprofilegetbyapproval(_bm);
        foreach (tuser_profiles_approval _dtl in _approval)
        {
            arr.Clear();
            arr.Add(new cArrayList("@wa_typ", "sample"));
            arr.Add(new cArrayList("@token_sent", _tokensent));
            arr.Add(new cArrayList("@mobileno", _dtl.whatsapp_no));
            arr.Add(new cArrayList("@refno", _sample_cd));
            arr.Add(new cArrayList("@emp_cd", _dtl.emp_cd));
            bll.vInsertWhatsappOutbox(arr);
            //bll.vSendWhatsapp(_dtl.whatsapp_no, _wa);
        }
        cd.v_hiddencontrol(btsave);
        cd.v_showcontrol(btprint);
        cd.v_disablecontrol(txrefno);
        cd.v_disablecontrol(txsamplename);
        cd.v_disablecontrol(txqty);
        cd.v_disablecontrol(txitem);
        cd.v_disablecontrol(cbuom);
        cd.v_disablecontrol(grd);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Request sample has been created!','Wait for Approval BM " + _sample_cd + "','success');", true);
    }

    protected void btemp_Click(object sender, EventArgs e)
    {
        cd.v_disablecontrol(txpic);
        cd.v_enablecontrol(txsamplename);
        cd.v_enablecontrol(cbreason);
        cd.v_enablecontrol(cbsampleoutby);
        cd.v_enablecontrol(txitem);
        cd.v_enablecontrol(txrefno);
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCustList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        System.Data.SqlClient.SqlDataReader rs = null;
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        List<string> lCust = new List<string>();
        string sCust = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        // arr.Add(new cArrayList("@salesman_cd", contextKey));
        arr.Add(new cArrayList("@cust_cd", prefixText));
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        //arr.Add(new cArrayList("@salespointcd", contextKey));
        bll.vSearchMstCustomerBySalespoint(arr, ref rs);
        while (rs.Read())
        {
            sCust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + "-" + rs["cust_nm"].ToString(), rs["cust_cd"].ToString());
            lCust.Add(sCust);
        }
        rs.Close();
        return (lCust.ToArray());
    }

    protected void chcust_CheckedChanged(object sender, EventArgs e)
    {
        if (chcust.Checked)
        {
            //cd.v_enablecontrol(txcustomer);
            cd.v_showcontrol(txcustomer);
            cd.v_hiddencontrol(txother);
        }
        else { cd.v_hiddencontrol(txcustomer);
            cd.v_showcontrol(txother);
        }
        cd.v_disablecontrol(chcust);
    }

    protected void btcust_Click(object sender, EventArgs e)
    {
        cd.v_disablecontrol(txcustomer);
        //cd.v_disablecontrol(cbsalespoint);
    }

    protected void btsearch_Click(object sender, EventArgs e)
    {

    }

    protected void cbsalespoint_SelectedIndexChanged(object sender, EventArgs e)
    {
        //txcustomer_AutoCompleteExtender.ContextKey = cbsalespoint.SelectedValue;

    }

    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbitemcode = (Label)grd.Rows[e.RowIndex].FindControl("lbitemcode");
        List<tstocksample_dtl> _tstocksample_dtl = (List<tstocksample_dtl>)Session["tstocksample_dtl"];
        _tstocksample_dtl.RemoveAt(e.RowIndex);

        grd.DataSource = _tstocksample_dtl;
        grd.DataBind();
    }



    protected void cbuom_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbbin.SelectedValue == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select bin!','Bin Mandatory','warning');", true);
            return;
        }
        DateTime _dt = System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        decimal _stockavl = Convert.ToDecimal(bll.vLookUp("select dbo.[sfnGetStockBooking]('" + Request.Cookies["sp"].Value + "','" + hditem.Value.ToString() + "','" + cbbin.SelectedValue + "','" + cbwhs.SelectedValue + "','DEPO','" + _dt + "')"));//by yanto 25-6-2016 

        lbstockavl.Text = bll.vLookUp("select dbo.fn_getitemconversion('" + hditem.Value + "'," + _stockavl + ",'CTN','" + cbuom.SelectedValue + "')");
        cd.v_enablecontrol(txqty);
    }

    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_reportx.aspx?src=stocksample&s=" + lbsampleno.Text + "');", true);
    }

    protected void btlookup_Click(object sender, EventArgs e)
    {
        lbsampleno.Text = hdsample.Value;
        txrefno.Text = bll.vLookUp("Select ref_no from tstock_sample where sample_cd='" + hdsample.Value + "'");
        hdemp.Value = bll.vLookUp("Select responsible_by from tstock_sample where sample_cd='" + hdsample.Value + "'");
        txsamplename.Text = bll.vLookUp("Select remark from tstock_sample where sample_cd='" + hdsample.Value + "'");
        cbreason.SelectedValue = bll.vLookUp("Select reasn_cd from tstock_sample where sample_cd='" + hdsample.Value + "'");
        txpic.Text = bll.vLookUp("select emp_nm from tmst_employee where emp_cd='" + hdemp.Value + "'");
        cbsampleoutby.SelectedValue = bll.vLookUp("Select sampleoutby from tstock_sample where sample_cd='" + hdsample.Value + "'");
        string _cust = bll.vLookUp("Select isnull(cust_cd,'') from tstock_sample where sample_cd='" + hdsample.Value + "'");
        if (_cust != string.Empty)
        {
            chcust.Checked = false;
            txcustomer.Text = _cust;
        }
        else
        {
            chcust.Checked = true;
            txcustomer.Text = bll.vLookUp("select cust_nm from tmst_customer where cust_cd='" + _cust + "'");
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@sample_cd", hdsample.Value));
        List<tstocksample_dtl> _tstocksample_dtl = bll.lstoksampledtl(arr);
        grd.DataSource = _tstocksample_dtl;
        grd.DataBind();
        cd.v_showcontrol(btprint);
        cd.v_hiddencontrol(btsave);
        cd.v_disablecontrol(txrefno);
        cd.v_disablecontrol(txpic);
        cd.v_disablecontrol(chcust);
        cd.v_disablecontrol(txsamplename);
        cd.v_disablecontrol(txcustomer);
        cd.v_disablecontrol(cbsampleoutby);

    }
}