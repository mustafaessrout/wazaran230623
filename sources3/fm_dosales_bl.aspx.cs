using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class fm_dosales_bl : System.Web.UI.Page
{
    cbll bll = new cbll();
    cbll2 bll2 = new cbll2();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@so_sta_id", "L"));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
            bll.vBindingGridToSp(ref grd, "sp_tmst_salesorder_getbybl", arr);
            arr.Clear();
            arr.Add(new cArrayList("@qry_cd", "driver"));
            bll.vBindingComboToSpWithEmptyChoosen(ref cbdriver, "sp_tmst_employee_getbyqry", "emp_cd", "emp_nm", arr);
            cd.v_disablecontrol(cbdriver);
            cd.v_disablecontrol(txbl);
            cd.v_hiddencontrol(btprint);
            cd.v_hiddencontrol(btprintinvoice);
            cd.v_hiddencontrol(btsave);
        }
    }

    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        HiddenField lbdono = (HiddenField)grd.Rows[e.NewSelectedIndex].FindControl("hddono");
        Label lbso = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbsocd");
        lbdonoselected.Text = lbdono.Value;
        lbsocd.Text = lbso.Text;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@do_no", lbdono.Value));
        bll.vBindingGridToSp(ref grddtl, "sp_tdosales_dtl_getbybl", arr);

        Boolean _checkcomplete = true;
        foreach (GridViewRow row in grddtl.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                Label lbqty = (Label)row.FindControl("lbqty");
                Label lbdelivered = (Label)row.FindControl("lbdelivered");
                double _qty = Convert.ToDouble(lbqty.Text);
                double _delivered = Convert.ToDouble(lbdelivered.Text);

                if (_qty != _delivered)
                {
                    _checkcomplete = false;
                }
            }
        }

        if (_checkcomplete)
        {
            //cd.v_showcontrol(btprint);
            cd.v_showcontrol(btprintinvoice);
        }
        else { cd.v_showcontrol(btsave); }
        string _checkinvoiceready = bll.vLookUp("select dbo.fn_checkinvoiceready('" + lbdonoselected.Text + "')");
        if (_checkinvoiceready == "ok")
        {
            cd.v_showcontrol(btprintinvoice);
        }
        cd.v_enablecontrol(cbdriver);
        cd.v_disablecontrol(grd);
    }

    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_dosales_bl.aspx");
    }

    protected void grddtl_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lbitemcode = (Label)e.Row.FindControl("lbitemcode");
            Label lbdelivered = (Label)e.Row.FindControl("lbdelivered");
            TextBox txdelivery = (TextBox)e.Row.FindControl("txdelivery");
            Label lbqty = (Label)e.Row.FindControl("lbqty");
            TextBox dtexpire = (TextBox)e.Row.FindControl("dtexpire");
            DateTime _dtwaz = System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime _dt = _dtwaz.AddYears(1);
            dtexpire.Text = _dt.ToString("d/M/yyyy");
            double _delived = Convert.ToDouble(bll.vLookUp("select dbo.fn_getdeliverybl('" + lbdonoselected.Text + "','" + lbitemcode.Text + "')"));
            lbdelivered.Text = _delived.ToString("N2");
            double _order = Convert.ToDouble(lbqty.Text);
            double _delivery = _order - _delived;

            txdelivery.Text = _delivery.ToString("N2");
            if (_order == _delived)
            {
                cd.v_disablecontrol(txdelivery);
            }

        }
    }

    protected void btsave_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (cbdriver.SelectedValue == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select driver!','Driver selection','warning');", true);
            return;
        }
        foreach (GridViewRow row in grddtl.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                TextBox txdelivery = (TextBox)row.FindControl("txdelivery");
                Label lbdelivered = (Label)row.FindControl("lbdelivered");
                Label lbqty = (Label)row.FindControl("lbqty");
                TextBox dtexpire = (TextBox)row.FindControl("dtexpire");
                DateTime _dt = DateTime.Now;
                DateTime _expire = DateTime.ParseExact(dtexpire.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                double _qtyorder = Convert.ToDouble(lbqty.Text);
                double _qtydelivered = Convert.ToDouble(lbdelivered.Text);
                double _qtydelivery = 0;
                if (dtexpire.Text == string.Empty)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please put correct date expire!','Date Expiration','warning');", true);
                    return;

                }
                if (!double.TryParse(txdelivery.Text, out _qtydelivery))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                      "sweetAlert('Qty delivery must numeric!','Check Qty Delivery','warning');", true);
                    return;
                }

                if ((_qtyorder - _qtydelivered) < _qtydelivery)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                        "sweetAlert('Qty delivery can not bigger than remain delivery!','Check Qty Delivery','warning');", true);
                    return;
                }
            }
        }
        string _invoice_no = bll.vLookUp("select inv_no from tmst_dosales where do_no='" + lbdonoselected.Text + "'");
        string _blno = string.Empty;
        arr.Clear();
        arr.Add(new cArrayList("@driver_cd", cbdriver.SelectedValue));
        arr.Add(new cArrayList("@do_no", lbdonoselected.Text));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        arr.Add(new cArrayList("@bl_dt", System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        bll2.vInsertDosalesBl(arr, ref _blno);
        txbl.Text = _blno;
        foreach (GridViewRow row in grddtl.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                TextBox txdelivery = (TextBox)row.FindControl("txdelivery");
                Label lbdelivered = (Label)row.FindControl("lbdelivered");
                Label lbqty = (Label)row.FindControl("lbqty");
                Label lbitemcode = (Label)row.FindControl("lbitemcode");
                Label lbuom = (Label)row.FindControl("lbuom");
                TextBox dtexpire = (TextBox)row.FindControl("dtexpire");

                double _qtyorder = Convert.ToDouble(lbqty.Text);
                double _qtydelivered = Convert.ToDouble(lbdelivered.Text);
                double _qtydelivery = Convert.ToDouble(txdelivery.Text);
                arr.Clear();
                arr.Add(new cArrayList("@uom", lbuom.Text));
                arr.Add(new cArrayList("@bl_no", _blno));
                arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
                arr.Add(new cArrayList("@qty", _qtydelivery));
                arr.Add(new cArrayList("@expire_dt", System.DateTime.ParseExact(dtexpire.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                bll2.vInsertDosalesBlDtl(arr);
            }
        }

        string _checkinvoiceready = bll.vLookUp("select dbo.fn_checkinvoiceready('" + lbdonoselected.Text + "')");
        if (_checkinvoiceready == "ok")
        {
            arr.Clear();
            arr.Add(new cArrayList("@inv_no", _invoice_no));
            arr.Add(new cArrayList("@received_dt", System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@received_by", cbdriver.SelectedValue));
            arr.Add(new cArrayList("@receiveddriver_dt", System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@rcp_sta_id", "N"));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
            bll2.vInsertDosalesInvoiceReceived(arr);
            cd.v_showcontrol(btprintinvoice);

        }
        string _sql = "update tdosalesinvoice_info set driver_sta_id='Y' where inv_no='" + _invoice_no + "'";
        bll.vExecuteSQL(_sql);
        foreach (GridViewRow _row in grddtl.Rows)
        {
            if (_row.RowType == DataControlRowType.DataRow)
            {
                Label lbitemcode = (Label)_row.FindControl("lbitemcode");
                TextBox txqty = (TextBox)_row.FindControl("txdelivery");
                Label lbuom = (Label)_row.FindControl("lbuom");
                DateTime _dt = System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                arr.Clear();
                arr.Add(new cArrayList("@inv_no", _invoice_no));
                arr.Add(new cArrayList("@emp_cd", cbdriver.SelectedValue));
                arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
                arr.Add(new cArrayList("@qty", txqty.Text));
                arr.Add(new cArrayList("@uom", lbuom.Text));
                arr.Add(new cArrayList("@driver_receipt_dt", _dt));
                bll2.vInsertDosalesinvoiceDriver(arr);
            }
        }
        cd.v_disablecontrol(txbl);
        cd.v_disablecontrol(cbdriver);
        cd.v_disablecontrol(grddtl);
        cd.v_disablecontrol(grd);
        cd.v_hiddencontrol(btsave);
        cd.v_showcontrol(btprint);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('New BL has been create !','" + _blno + "','success');", true);


    }

    protected void btprint_Click(object sender, EventArgs e)
    {
        if (bll.nCheckAccess("invto", Request.Cookies["usr_id"].Value.ToString()) == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('You dont have access','Print invoice Take Order !!','warning');", true);
            return;
        }
        string _invoiceno = bll.vLookUp("select inv_no from tmst_dosales where do_no='" + lbdonoselected.Text + "'");
        string _checkinvoiceready = bll.vLookUp("select dbo.fn_checkinvoiceready('" + lbdonoselected.Text + "')");
        if (_checkinvoiceready == "ok")
        {
            cd.v_showcontrol(btprintinvoice);
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=bl&no=" + txbl.Text + "');", true);
    }

    protected void btprintinvoice2_Click(object sender, EventArgs e)
    {
        PrintInvoice();
    }

    void PrintInvoice()
    {
        try
        {

            if (bll.nCheckAccess("invto", Request.Cookies["usr_id"].Value.ToString()) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('You dont have access','Print invoice Take Order !!','warning');", true);
                return;
            }
            //if (txmanualinv.Text == "")
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Manual No','Please fill manual invoice !!','warning');", true);
            //    return;
            //}

            //string sManualNo = bll.vLookUp("select dbo.fn_checkmanualno('invoice','" + txmanualinv.Text + "','" + Request.Cookies["sp"].Value.ToString() + "')"); //bll.vLookUp("select dbo.fn_getmanualno('" + txmanualinv.Text + "','" + txorderno.Text + "')");
            //if (sManualNo != "ok")
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('This manual no. " + txmanualinv.Text + "  already used','Please use another !!','warning');", true);
            //    return;


            string sLoading = bll.vLookUp("select dbo.fn_checkstockloading('" + lbsocd.Text + "','" + Request.Cookies["sp"].Value.ToString() + "')");
            if (sLoading != "ok")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There are no stock enough for Item Ordered','" + sLoading + "','warning');", true);
                return;
            }
            string _invoiceno = bll.vLookUp("select inv_no from tmst_dosales where do_no='" + lbdonoselected.Text + "'");

            List<cArrayList> arr = new List<cArrayList>();
            arr.Clear();
            arr.Add(new cArrayList("@so_cd", lbsocd.Text));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vInsertfreebyitem(arr);
            arr.Clear();
            arr.Add(new cArrayList("@inv_no", _invoiceno));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vInsertsalesorderfreebyitem(arr);
            string Sord = bll.vLookUp("select dbo.fn_orderproblem('" + _invoiceno + "','" + Request.Cookies["sp"].Value.ToString() + "')");
            if (Sord == "ok")
            {
                arr.Clear();
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@inv_no", _invoiceno));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vInsertRptDosalesInvoice(arr);
                arr.Clear();
                arr.Add(new cArrayList("@so_cd", lbsocd.Text));
                arr.Add(new cArrayList("@so_sta_id", "C"));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vUpdateMstSalesOrder(arr);
                arr.Clear();
                arr.Add(new cArrayList("@so_cd", lbsocd.Text));
                arr.Add(new cArrayList("@manual_no", string.Empty));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vUpdateDosalesInvoiceByManualNo(arr);
                arr.Clear();
                arr.Add(new cArrayList("@doc_no", _invoiceno));
                arr.Add(new cArrayList("@print_cd", "TOINV")); //Canvas INvoice
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vInsertPrintControl(arr);
                arr.Clear();
                //arr.Add(new cArrayList("@so_cd", txorderno.Text));
                //arr.Add(new cArrayList("@vhc_cd", cbvehicle.SelectedValue.ToString()));
                //arr.Add(new cArrayList("@driver_cd", cbdriver.SelectedValue.ToString()));
                //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //bll.vUpdateMstDosalesByDriver(arr);
                //Walking on the moon : 18-Dec-2017
                arr.Clear();
                arr.Add(new cArrayList("@dosales_sta_id", "R"));
                arr.Add(new cArrayList("@so_cd", lbsocd.Text));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vUpdateMstDosalesByStatus(arr);
                // End Of Walking on the moon :
                arr.Clear();
                string sSOCd = bll.vLookUp("select so_cd from tmst_dosales where inv_no='" + _invoiceno + "' and so_typ='to' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                string sDiscAmt = bll.vLookUp("select sum(amt) from tsalesorder_disccash where so_cd='" + sSOCd + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                //btprintfreeinv.CssClass = "btn btn-info ";

                arr.Clear();
                arr.Add(new cArrayList("@inv_no", _invoiceno));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //bll.vBatchSendEmailProdSpv(arr);
                // StockCard ONDELIVERY
                // arr.Clear();
                // arr.Add(new cArrayList("@refno", txinvoiceno.Text));
                // arr.Add(new cArrayList("@stockcard_typ", "ONDELIVERY"));
                // bll.vBatchStockCard(arr);
                // int freeitem = 0;
                // freeitem = int.Parse(bll.vLookUp("select count(*) from tdosalesinvoice_free where inv_no='"+txinvoiceno.Text+"'"));
                // if (freeitem > 0)
                // {
                // arr.Clear();
                // arr.Add(new cArrayList("@refno", txinvoiceno.Text));
                // arr.Add(new cArrayList("@stockcard_typ", "ONDELIVERYFREE"));
                // bll.vBatchStockCard(arr);
                // }
                //--------------------- 
                ctowords wor = new ctowords();
                decimal _balance = Convert.ToDecimal(bll.vLookUp("select balance from tdosales_invoice where inv_no='" + _invoiceno + "'"));
                // string _f = wor.ConvertNumberToWords(_balance, Language.French);
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport1('fm_report2.aspx?src=invto&no=" + txinvoiceno.Text + "&amt=" + sDiscAmt + "','fm_report2.aspx?src=invto1&no=" + txinvoiceno.Text + "&amt=" + sDiscAmt + "');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=invto_uom&no=" + _invoiceno + "&amt=" + sDiscAmt + "');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There are Unitprice problem with this order contact wazaran programmers','Unitprice Issues','warning');", true);
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_takeorderentry_uom");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void bthist_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        HiddenField hddono = (HiddenField)row.FindControl("hddono");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
            "PopupCenter('lookupdelivered.aspx?d=" + hddono.Value + "','',800,800);", true);

    }

    protected void grd_DataBinding(object sender, EventArgs e)
    {
       
    }

    protected void grd_DataBound(object sender, EventArgs e)
    {
        
    }

    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            foreach (LinkButton button in e.Row.Cells[5].Controls.OfType<LinkButton>())
            {
                if (button.CommandName == "Select")
                {
                    button.Attributes.Add("OnClick", "ShowProgress();");
                }
            }
        }
    }
}

