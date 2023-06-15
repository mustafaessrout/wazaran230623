using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_salesreturn : System.Web.UI.Page
{
    cbll bll = new cbll();
    double dTotCustPrice = 0; double dTotSubTotal = 0; double dVat = 0; double _amountinclusivevat = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        {
            try
            {
                string sDate = Request.Cookies["waz_dt"].Value.ToString();
                dtretur.Text = sDate;
                dtretur.CssClass = "form-control ro";
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@reasn_typ", "return"));
                bll.vBindingFieldValueToComboWithChoosen(ref cbuom, "uom");
                arr.Clear();
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                bll.vDeleteWrkSalesReturn(arr);
                txreturno.Text = "NEW";
                btcancel.CssClass = "divhid";
                dtretur.Text = sDate;
                dtcustman.Text = sDate;
                btprint.CssClass = "divhid";
                txpaymentno.CssClass = cd.csstextro;
                dtretur.CssClass = cd.csstext;
                txsearchcust.CssClass = cd.csstextro;
                btsave.CssClass = "divhid";
                //string sEditRTV = bll.vLookUp("select count(1) from taccess_user where access_cd='editrtv' and usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "'");
                string _privilegeprice = bll.vLookUp("select dbo.fn_checkaccessgranular('" + Request.Cookies["usr_id"].Value + "','editrtv')");
                if (_privilegeprice != "ok")
                {
                    //txcustprice.CssClass = "form-control input-sm ro";
                    cd.v_disablecontrol(txcustprice);
                }
                else
                {
                    //txcustprice.CssClass = cd.csstext;
                    cd.v_enablecontrol(txcustprice);
                }
                dtretur.CssClass = "form-control ro";
                dtretur.Text = Request.Cookies["waz_dt"].Value.ToString();
                arr.Clear();
                arr.Add(new cArrayList("@reasn_typ", "return"));
                bll.vBindingComboToSpWithEmptyChoosen(ref cbremark, "sp_tmst_reason_get", "reasn_cd", "reasn_nm", arr);
                txmanualno.CssClass = cd.csstextro;
                txcustmanualno.CssClass = cd.csstextro;
                cbsalesman.CssClass = cd.csstextro;
                dtexp.CssClass = cd.csstextro;
                dtcustman.CssClass = cd.csstextro;
                txitemsearch.CssClass = cd.csstextro;
                txpaymentno.CssClass = cd.csstextro;
                txtabno.CssClass = cd.csstextro;
                cbremark.CssClass = cd.csstextro;
                cbuom.CssClass = cd.csstextro;
                lbvat.Text = string.Empty;
                arr.Clear();
                //arr.Add(new cArrayList("@job_title_cd", "5"));
                //arr.Add(new cArrayList("@level_cd", "1"));
                //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //bll.vBindingComboToSp(ref cbDriver, "sp_tmst_employee_getbyjobtitle", "emp_cd", "emp_desc", arr);
                arr.Add(new cArrayList("@qry_cd", "SalesJob"));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
                bll.vBindingComboToSpWithEmptyChoosen(ref cbDriver, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
                hdreturno.Value = string.Empty;
                //dtexp.Text = Request.Cookies["waz_dt"].Value;
                //txinvoicesearch.CssClass = cd.csstextro;
                //dtexp.ReadOnly = true;
                //dtexp_CalendarExtender.StartDate = System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_salesreturn");
                Response.Redirect("fm_ErrorPage.aspx");
            }
        }
    }


    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lCust = new List<string>();
        string sCust = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@salesman_cd", contextKey));
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        arr.Add(new cArrayList("@cust_cd", prefixText));
        //bll.vSearchCustomerBySales(arr, ref rs);
        bll.vSearchMstCustomerInRPS(arr, ref rs);
        while (rs.Read())
        {
            sCust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + "-" + rs["cust_nm"].ToString(), rs["cust_cd"].ToString());
            lCust.Add(sCust);
        }
        rs.Close();
        return (lCust.ToArray());
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetInvoiceList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        HttpCookie cok;
        //cok = HttpContext.Current.Request.Cookies["sp"];
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lCust = new List<string>();
        string sCust = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@salesman_cd", contextKey));
        string[] arritem = contextKey.Split(';');
        arr.Add(new cArrayList("@item_cd", arritem[1]));
        arr.Add(new cArrayList("@cust_cd", arritem[0]));
        arr.Add(new cArrayList("@inv_no", prefixText));
        bll.vSearchInvoiceByItem(ref rs, arr);
        while (rs.Read())
        {
            sCust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["inv_no"].ToString(), rs["inv_no"].ToString());
            lCust.Add(sCust);
        }
        rs.Close();
        return (lCust.ToArray());
    }
    protected void cbsalesman_SelectedIndexChanged(object sender, EventArgs e)
    {
        txsearchcust_AutoCompleteExtender.ContextKey = cbsalesman.SelectedValue.ToString();
    }
    protected void btsearchcust_Click(object sender, EventArgs e)
    {
        try
        {
            System.Data.SqlClient.SqlDataReader rs = null;
            if (rdreturtype.SelectedValue.ToString() == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select Return Type','Retur Type','warning');", true);
                hdcust.Value = ""; txsearchcust.Text = "";
                return;
            }
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
            bll.vGetMstCustomer(arr, ref rs);
            while (rs.Read())
            {
                lbaddress.Text = (rs["addr"].Equals(DBNull.Value) ? "-" : rs["addr"].ToString());

                lbcusttype.Text = rs["otlcd"].ToString();
                lbcity.Text = bll.vLookUp("select loc_nm from tmst_location where loc_cd='" + rs["city_cd"].ToString() + "'");
                lbcl.Text = rs["credit_limit"].ToString();
                arr.Clear(); // Get salesman Info
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@qry_cd", "SalesJob"));
                bll.vBindingComboToSp(ref cbsalesman, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
                cbsalesman.SelectedValue = rs["salesman_cd"].ToString();
                if (rdreturtype.SelectedValue.ToString() == "C")
                {
                    cbwhs.SelectedValue = bll.vLookUp("Select vhc_cd from tmst_vehicle where emp_cd='" + rs["salesman_cd"].ToString() + "'");
                }
                else
                {
                }
                cbsalesman.CssClass = cd.csstextro;
                txsearchcust.CssClass = cd.csstextro;
                txinvoicesearch.CssClass = cd.csstext;
                txitemsearch.CssClass = cd.csstext;
            }
            rs.Close();
            arr.Clear();
            arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));

            if (rdreturtype.SelectedValue.ToString() != "F")
            {
                bll.vBindingGridToSp(ref grdinv, "sp_tdosales_invoice_getbyoutstanding", arr);
                grdinv.Columns[5].Visible = false;
            }
            else
            {
                bll.vBindingGridToSp(ref grdinv, "sp_tdosales_invoice_getbyfullreturn", arr);
                grdinv.Columns[5].Visible = true;
            }

            txitemsearch.CssClass = cd.csstext;
            cbuom.CssClass = cd.csstext;
            dtexp.CssClass = cd.csstext;

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_salesreturn");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        finally
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
        }

    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList2(string prefixText, int count, string contextKey)
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
    protected void btadd_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdcust.Value.ToString() == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Customer not yet selected !','Can not find price','warning');", true);
                return;
            }

            if (txqty.Text == "")
            {
                if (hdcust.Value.ToString() == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Quantity for retur','Quantity can not empty','warning');", true);
                    return;
                }
            }
            double dCustPrice = 0;
            if (!double.TryParse(txcustprice.Text, out dCustPrice))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Unit Price from salesman must be currency','Price Entry','warning');", true);
                return;
            }

            if (dCustPrice > Convert.ToDouble(lbprice.Text))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Unit Price salesman can not bigger than unit price','Price Problem','warning');", true);
                return;
            }

            if (dtexp.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Expiration product must be filled','Product Expire','warning');", true);
                return;
            }
            if (hditem.Value.ToString() == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Select item to be returned','Item Must be selected','warning');", true);
                return;
            }

            //if (cbbin.SelectedValue.ToString() == "")
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Stock condition (Bad Stock, Near Exp or Damage) is not allowed','Check expiration date','warning');", true);
            //    return;
            //}
            double dUnitprice = 0;
            double dOut = 0;
            if (double.TryParse(lbprice.Text, out dOut))
            {
                dUnitprice = dOut;
            }
            else
            {
                dUnitprice = Convert.ToDouble(bll.vLookUp("select dbo.fn_getadjustmentprice ('" + hditem.Value.ToString() + "','" + hdcust.Value.ToString() + "','" + cbuom.SelectedValue.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "')"));
            }

            //double dQtyAvail = Convert.ToDouble(bll.vLookUp("select dbo.fn_checkqtyreturninvoice('" + hdinvoice.Value + "','" + hditem.Value + "','" + txqty.Text + "','" + cbuom.SelectedValue + "')"));
            if (lbqtyavl.Text == string.Empty)
            {
                lbqtyavl.Text = "0";
            }
            double dQtyAvail = Convert.ToDouble(lbqtyavl.Text);
            if (hdinvoice.Value != string.Empty)
            {
                if ((Convert.ToDouble(txqty.Text) > dQtyAvail) && (txreturno.Text == "NEW"))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Can not exceeded available qty for return','Check qty already use for retur from invoice','warning');", true);
                    return;
                }
            }
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@item_cd", hditem.Value.ToString()));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@qty", txqty.Text));
            arr.Add(new cArrayList("@uom", cbuom.SelectedValue.ToString()));
            //arr.Add(new cArrayList("@reason", cbreason.SelectedValue));
            arr.Add(new cArrayList("@condition", lbexp.Text));
            arr.Add(new cArrayList("@exp_dt", DateTime.ParseExact(dtexp.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@whs_cd", cbwhs.SelectedValue.ToString()));
            arr.Add(new cArrayList("@bin_cd", cbbin.SelectedValue.ToString()));
            arr.Add(new cArrayList("@unitprice", dUnitprice));
            arr.Add(new cArrayList("@custprice", dCustPrice));
            arr.Add(new cArrayList("@reason", cbremark.SelectedValue.ToString()));   // (dCustPrice * Convert.ToDouble( txqty.Text))));
            arr.Add(new cArrayList("@vat", lbvat.Text));
            arr.Add(new cArrayList("@isvat", cbvat.SelectedValue.ToString()));
            arr.Add(new cArrayList("@inv_no", hdinvoice.Value));
            bll.vInsertWrkSalesReturn(arr);
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_twrk_salesreturn_get", arr);
            txitemsearch.Text = string.Empty;
            hditem.Value = string.Empty;
            txqty.Text = string.Empty;
            // lbamt.Text =  bll.vLookUp("select cast(sum(qty * custprice) as numeric(18,2)) from twrk_salesreturn where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "'");
            Label lbtotamt = (Label)grd.FooterRow.FindControl("lbtotcustprice");
            Label lbtotvat = (Label)grd.FooterRow.FindControl("lbtotvat");
            lbamt.Text = lbtotamt.Text;
            lbexp.Text = string.Empty;
            dtexp.Text = string.Empty;
            txcustprice.Text = string.Empty;
            lbprice.Text = string.Empty;
            cbuom.SelectedValue = string.Empty;
            lbprice.Text = string.Empty;
            lbtotprice.Text = string.Empty;
            lbvat.Text = string.Empty;
            txinvoicesearch.Text = string.Empty;
            txitemsearch.CssClass = cd.csstext;
            hdinvoice.Value = string.Empty;
            btsave.CssClass = "btn btn-warning btn-save";
            lbqtyavl.Text = string.Empty;

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_salesreturn");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            bool bZero = false;
            string scd = bll.vLookUp("select  dbo.sfnGetcountdown('" + Request.Cookies["sp"].Value.ToString() + "')");
            if (scd == "0" && txreturno.Text == "NEW")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Block entry because deadline to daily closing !','Please daily closing !','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
                return;
            }
            if (cbremark.Text == string.Empty)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select reason!','Reaturn Remark','warning');", true);
                return;
            }
            string sReturNo = string.Empty; string sEmpCode = string.Empty;
            if (grd.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select item to be returned','select item','warning');", true);
                return;
            }

            if (txmanualno.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Manual No can not empty','select manual no','warning');", true);
                return;
            }

            string sExistNo = bll.vLookUp("select dbo.fn_checkmanualno('retur','" + txmanualno.Text + "','" + Request.Cookies["sp"].Value.ToString() + "')");
            if (hdreturno.Value.ToString() != "")
            {
                sExistNo = "ok";
            }
            if (sExistNo != "ok")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('" + sExistNo + "','select manual no','warning');", true);
                return;
            }

            if (rdreturtype.SelectedValue.ToString() == "F")
            {
                if (grd.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select invoice to be returned','select invoice','warning');", true);
                    return;
                }
            }
            if (dtretur.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select return Date','select retur date','warning');", true);
                return;
            }

            // Add Document must be upload for return 

            if (upl.FileName == "" || (upl.FileName.Equals(null)))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('No Document uploaded','Please scan the document and upload','warning');", true);
                return;
            }
            FileInfo fi = new FileInfo(upl.FileName);
            string ext = fi.Extension;
            byte[] fs = upl.FileBytes;
            if (fs.Length <= 5242880)
            {
                if (ext != ".jpg" && ext != ".jpeg" && ext != ".bmp" && ext != ".gif" && ext != ".png" && ext != ".JPEG" && ext != ".JPG" && ext != ".BMP" && ext != ".GIF" && ext != ".PNG" && ext != ".pdf" && ext != ".PDF")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('You should upload image','jpg,bmp,gif,png or pdf upload document again');", true);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Image size too big','Image size must be less than 5MB');", true);
                return;
            }


            List<cArrayList> arr = new List<cArrayList>();
            arr.Clear();
            arr.Add(new cArrayList("@retur_typ", rdreturtype.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salesman_cd", cbsalesman.SelectedValue.ToString()));
            arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
            arr.Add(new cArrayList("@retur_dt", DateTime.ParseExact(dtretur.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@manual_no", txmanualno.Text));
            arr.Add(new cArrayList("@custmanual_no", txcustmanualno.Text));
            arr.Add(new cArrayList("@remark", cbremark.SelectedValue.ToString()));
            arr.Add(new cArrayList("@returnDriver_cd", cbDriver.SelectedValue.ToString()));
            arr.Add(new cArrayList("@tabretur_no", txtabno.Text));
            //arr.Add(new cArrayList("@prevstk", chprevstk.Checked));
            arr.Add(new cArrayList("@remarks", txRemarks.Text));
            if (hdreturno.Value.ToString() == "") // New Retur
            {
                bll.vInsertSalesReturn(arr, ref sReturNo);
                txreturno.Text = sReturNo;
            }
            else { sReturNo = txreturno.Text; }
            foreach (GridViewRow row in grd.Rows)
            {
                Label lbitemcode = (Label)row.FindControl("lbitemcode");
                Label lbqty = (Label)row.FindControl("lbqty");
                TextBox txunitprice = (TextBox)row.FindControl("txunitprice");
                Label lbcustprice = (Label)row.FindControl("lbcustprice");
                Label lbunitprice = (Label)row.FindControl("lbunitprice");
                Label lbexp = (Label)row.FindControl("lbexp");
                Label lbcondition = (Label)row.FindControl("lbcondition");
                Label lbreason = (Label)row.FindControl("lbreason");
                Label lbwhs = (Label)row.FindControl("lbwhs");
                Label lbbin = (Label)row.FindControl("lbbin");
                Label lbsubtotal = (Label)row.FindControl("lbsubtotal");
                Label lbuom = (Label)row.FindControl("lbuom");
                Label lbvat = (Label)row.FindControl("lbvat");
                Label lbinvo = (Label)row.FindControl("lbinvno");
                HiddenField hdvat = (HiddenField)row.FindControl("hdvat");
                if (Convert.ToDouble(lbcustprice.Text) != 0)
                { bZero = true; }
                arr.Clear();
                arr.Add(new cArrayList("@retur_no", sReturNo));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
                arr.Add(new cArrayList("@qty", lbqty.Text));
                arr.Add(new cArrayList("@unitprice", lbunitprice.Text));
                arr.Add(new cArrayList("@custprice", lbcustprice.Text));
                arr.Add(new cArrayList("@exp_dt", DateTime.ParseExact(lbexp.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@condition", lbcondition.Text));
                arr.Add(new cArrayList("@whs_cd", lbwhs.Text));
                arr.Add(new cArrayList("@bin_cd", lbbin.Text));
                arr.Add(new cArrayList("@subtotal", (Convert.ToDouble(lbqty.Text) * Convert.ToDouble(lbcustprice.Text)).ToString()));//(Convert.ToDouble(lbqty.Text) * Convert.ToDouble(lbcustprice.Text)).ToString()
                arr.Add(new cArrayList("@uom", lbuom.Text));
                arr.Add(new cArrayList("@vat", lbvat.Text));
                arr.Add(new cArrayList("@isvat", hdvat.Value.ToString()));
                arr.Add(new cArrayList("@inv_no", lbinvo.Text));
                bll.vInsertSalesReturDtl(arr); //Retur in atau Retur 

                //List<string> lformula = new List<string>();
                //lformula.Add("{tsalesreturn.retur_no} = '" + Request.QueryString["returno"] + "'");
                //creport _report = new creport();
                //string _path = bll.sGetControlParameter("image_path") + @"return_doc\";
                //_report.vShowReportToPDF("rp_salesreturn2.rpt", lformula, _path + sReturNo + ".pdf");
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report3.aspx?src=retprodspv&returno=" + sReturNo + "');", true);
            }
            List<string> lformula = new List<string>();
            lformula.Add("{tsalesreturn.retur_no} = '" + Request.QueryString["returno"] + "'");
            creport _report = new creport();
            string _path = bll.sGetControlParameter("image_path") + @"return_doc\";
            _report.vShowReportToPDF("rp_salesreturn2.rpt", lformula, _path + sReturNo + ".pdf");
            //arr.Clear();
            //arr.Add(new cArrayList("@stockcard_typ", "RETURNCUST"));
            //arr.Add(new cArrayList("@refno", sReturNo));
            //bll.vtacc_stock_cardBySalesReturn(arr);

            // Upload Document to Server 
            //upl.SaveAs(bll.sGetControlParameter("image_path") + "/invoice_doc/" + sReturNo.ToString() + ext);
            //arr.Clear();
            //arr.Add(new cArrayList("@fileinv", sReturNo + ext));
            //arr.Add(new cArrayList("@inv_no", sReturNo));
            //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            //bll.vUploadInvoice(arr);
            //arr.Clear();
            //arr.Add(new cArrayList("@disc_cd", ""));
            //arr.Add(new cArrayList("@isstamp", "1"));
            //arr.Add(new cArrayList("@remark", ""));
            //arr.Add(new cArrayList("@issign", "1"));
            //arr.Add(new cArrayList("@isexclude", "1"));
            //arr.Add(new cArrayList("@inv_no", sReturNo));
            //arr.Add(new cArrayList("@received_dt", DateTime.ParseExact(dtretur.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            //bll.vInsertClaimConfirm(arr);
            arr.Clear();
            hpfile_nm.Visible = true;
            upl.Visible = false;
            lblocfile.Text = sReturNo.ToString() + ext;
            hpfile_nm.NavigateUrl = "/images/invoice_doc/" + sReturNo.ToString() + ext;
            // Upload Document to Server 


            if (bZero)
            {
                //try
                //{
                //int nrnd; System.Data.SqlClient.SqlDataReader rs = null;
                //List<string> lapproval = bll.lGetApproval("return", 1); //Old version I am not remove (IAG)
                //arr.Clear();
                //arr.Add(new cArrayList("@emp_cd", cbsalesman.SelectedValue.ToString()));
                //bll.vGetDirectSpv(arr, ref rs);
                //while (rs.Read())
                //{
                //    lapproval[0] = rs["mobile_no"].ToString();
                //    lapproval[1] = rs["email"].ToString();
                //}
                //rs.Close();

                //string sSubject = "New Return Request";
                //string sItem = "";
                //// System.Data.SqlClient.SqlDataReader rs = null;
                //Random rnd = new Random();
                //nrnd = rnd.Next(1000, 9999);
                ////string sMsg = "Dear Product Supervisor , <br<br>New retur product request with no." + sReturNo + "<br><br>" +
                ////"Please approved or reject this request via SMS already sent.<br><br>Best Regards,<br><br>Wazaran Admin.";
                //string ssalespoint = Request.Cookies["sp"].Value.ToString();
                //string scustomer = bll.vLookUp("select cust_cd+'''-'''+cust_nm from tmst_customer where cust_cd='" + hdcust.Value.ToString() + "'and salespointcd='" + ssalespoint + "'");
                //string saddress = lbaddress.Text + "-'" + lbcity.Text;
                //string slastreturnamt = bll.vLookUp("select top 1 sum(custprice) from tsalesreturn inner join tsalesreturn_dtl on tsalesreturn.retur_no=tsalesreturn_dtl.retur_no and tsalesreturn.salespointcd=tsalesreturn_dtl.salespointcd where tsalesreturn.retur_no<>'" + txreturno.Text + "'  and cust_cd='" + hdcust.Value.ToString() + "'and tsalesreturn.salespointcd='" + ssalespoint + "' group by retur_dt,tsalesreturn.retur_no order by retur_dt desc ");
                //string slastgrandtotalreturnamt = bll.vLookUp("select sum(custprice) from tsalesreturn inner join tsalesreturn_dtl on tsalesreturn.retur_no=tsalesreturn_dtl.retur_no and tsalesreturn.salespointcd=tsalesreturn_dtl.salespointcd where year(retur_dt)=year('" + DateTime.ParseExact(dtretur.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture) + "')  and cust_cd='" + hdcust.Value.ToString() + "'and tsalesreturn.salespointcd='" + ssalespoint + "'");
                //string sMsg = "<html><head><div><h2>Sales Retur</h2></div></head><body><table><tr><td>Salespoint</td><td>:</td><td>" + ssalespoint + "</td></tr><tr><td>Customer</td><td>:</td><td>" + scustomer + "</td></tr><tr><td>Adress</td><td>:</td><td>" + saddress + "></td></tr><tr><td>Salesman</td><td>:</td><td>" + cbsalesman.SelectedItem.Text + "</td></tr><tr><td>Return No.</td><td>:</td><td>" + txreturno.Text + "</td></tr><tr><td>Manual No.</td><td>:</td><td>" + txreturno.Text + "</td></tr><tr><td colspan=3>Last Return Amount</td><td>:</td><td>" + slastreturnamt + "</td></tr><tr><td colspan=3>Last Grand Total Receipt Return</td><td>:</td><td>" + slastgrandtotalreturnamt + "</td></tr></table><table><tr><td>Code</td><td>Item</td><td>Brand</td><td>Size</td><td>Qty</td><td>Unit Price</td><td>Amount</td><td>Cust Amt</td><td>Exp Date</td><td>Bin</td></tr>";
                //arr.Clear();
                //arr.Add(new cArrayList("@retur_no", txreturno.Text));
                //arr.Add(new cArrayList("@salespointcd", ssalespoint));
                //bll.vGettsalesreturn_dtl(arr, ref rs);
                //while (rs.Read())
                //{
                //    DateTime dtdate = Convert.ToDateTime(rs["exp_dt"]);
                //    string sdate = dtdate.ToShortDateString(); //String.Format("{0:d/M/yyyy}", dtdate);
                //                                               //DateTime dtdate = DateTime.ParseExact(sdate, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                //    sItem += "<tr><td>" + rs["item_cd"].ToString() + "</td><td>" + rs["item_nm"] + "</td><td>" + rs["branded_nm"] + "</td><td>" + rs["size"] + "</td><td>" + rs["qty"].ToString() + "</td><td>" + rs["unitprice"] + "</td><td>" + rs["subtotal"] + "</td><td>" + rs["custprice"] + "</td><td>" + sdate + "</td><td>" + rs["bin_cd"] + "</td><td></tr>";
                //}
                //rs.Close();
                //sMsg += sItem + "</table></body></html>\r\n\r\n\r\n Wazaran Admin";
                //if (rdreturtype.SelectedValue.ToString() != "F")
                //{

                //    //string sSMS = "New Retur No." + sReturNo + " has requested by " + bll.sGetSalespointname(Request.Cookies["sp"].Value.ToString());
                //    //sSMS += "Please approve with reply Y or N (ex. Y1000) token:" + nrnd.ToString();
                //    string ssp = Request.Cookies["sp"].Value.ToString();
                //    string sreturnamt = bll.vLookUp("select sum(qty*custprice) from tsalesreturn inner join tsalesreturn_dtl on tsalesreturn.retur_no=tsalesreturn_dtl.retur_no and tsalesreturn.salespointcd=tsalesreturn_dtl.salespointcd where tsalesreturn.retur_no='" + txreturno.Text + "'and tsalesreturn.salespointcd='" + ssp + "'");
                //    string sSMS = bll.vLookUp("select [dbo].[fn_smsappsalesreturn]('" + ssp + "','" + hdcust.Value + "'," + sreturnamt + ")");
                //    //  cd.vSendSms(sSMS + nrnd.ToString(), lapproval[0]);
                //    string stoken = bll.vLookUp("select short_ip from tmst_salespoint where salespointcd=(select parm_valu from tcontrol_parameter where parm_nm='salespoint')") + nrnd.ToString();
                //    //bll.vSendMail(lapproval[1], sSubject, sMsg);
                //    arr.Clear();
                //    arr.Add(new cArrayList("@token", stoken.ToString()));
                //    arr.Add(new cArrayList("@doc_typ", "retur"));
                //    arr.Add(new cArrayList("@to", lapproval[1]));
                //    arr.Add(new cArrayList("@doc_no", sReturNo));
                //    arr.Add(new cArrayList("@emailsubject", sSubject));
                //    arr.Add(new cArrayList("@msg", sMsg));
                //    bll.vInsertEmailOutbox(arr);
                //    arr.Clear();
                //    arr.Add(new cArrayList("@token", stoken.ToString()));
                //    arr.Add(new cArrayList("@doc_no", sReturNo));
                //    arr.Add(new cArrayList("@doc_typ", "retur"));
                //    arr.Add(new cArrayList("@to", lapproval[0]));
                //    arr.Add(new cArrayList("@msg", sSMS.TrimEnd() + stoken.ToString()));
                //    bll.vInsertSmsOutbox(arr);

                //}
                //}
                //catch (Exception ex)
                //{
                //    arr.Clear();
                //    arr.Add(new cArrayList("@err_source", "Retur Save"));
                //    arr.Add(new cArrayList("@err_description", ex.Message.ToString()));
                //    bll.vInsertErrorLog(arr);
                //}
                //  btsave.Visible = false; btprint.Visible = true;}
            }
            btsave.CssClass = "divhid"; btprint.CssClass = "btn btn-info btn-print";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Retur has save succeeded','Retur No." + sReturNo + "','success');", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_salesreturn");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txunitprice = (TextBox)e.Row.FindControl("txunitprice");
            Label lbitemcode = (Label)e.Row.FindControl("lbitemcode");
            Label lbqty = (Label)e.Row.FindControl("lbqty");
            Label lbcustprice = (Label)e.Row.FindControl("lbcustprice");
            Label lbsubtotal = (Label)e.Row.FindControl("lbsubtotal");
            Label lbunitprice = (Label)e.Row.FindControl("lbunitprice");
            Label lbvat = (Label)e.Row.FindControl("lbvat");
            Label lbamtinclusivevat = (Label)e.Row.FindControl("lbamtinclusivevat");
            lbsubtotal.Text = (Convert.ToDouble(lbqty.Text) * Convert.ToDouble(lbcustprice.Text)).ToString();
            dTotCustPrice += Convert.ToDouble(lbcustprice.Text);
            dTotSubTotal += Convert.ToDouble(lbsubtotal.Text);
            dVat += Convert.ToDouble(lbvat.Text);
            _amountinclusivevat += (dVat + dTotSubTotal);
            lbamtinclusivevat.Text = _amountinclusivevat.ToString("N2");

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbtotcustprice = (Label)e.Row.FindControl("lbtotcustprice");
            Label lbtotsubtotal = (Label)e.Row.FindControl("lbtotsubtotal");
            Label lbtotvat = (Label)e.Row.FindControl("lbtotvat");
            Label lbtotamtinclusivevat = (Label)e.Row.FindControl("lbtotamtinclusivevat");
            lbtotcustprice.Text = dTotCustPrice.ToString();
            lbtotsubtotal.Text = dTotSubTotal.ToString();
            lbtotvat.Text = dVat.ToString();
            lbtotamtinclusivevat.Text = _amountinclusivevat.ToString("N2");
        }
    }
    protected void dtexp_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string sExp = bll.sGetControlParameter("max_expiration");
            double dexp = Convert.ToDouble(sExp);
            DateTime dtexpire = DateTime.ParseExact(dtexp.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            if (System.DateTime.Today.AddDays(Convert.ToDouble(sExp)) <= dtexpire)
            {
                lbexp.Text = "GOOD";
            }
            else
            {
                if (System.DateTime.Today < dtexpire)
                {
                    lbexp.Text = "NEAR EXPIRED";
                }
                else { lbexp.Text = "EXPIRED"; }
            }
            cbwhs_SelectedIndexChanged(sender, e);
            // Here three section.
            // Good / Damage
            // Near Expired
            // Bad Stock
            // 

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_salesreturn");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void rdreturtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vDelWrkSalesReturn(arr);
            grdinv.DataSource = null;
            grdinv.DataBind();
            bll.vBindingGridToSp(ref grd, "sp_twrk_salesreturn_get", arr);
            hdcust.Value = "";
            txsearchcust.Text = "";
            lbaddress.Text = "";
            lbcity.Text = ""; lbamt.Text = ""; lbcl.Text = "0"; lbcusttype.Text = ""; lbexp.Text = ""; lbprice.Text = "";
            lbtotprice.Text = ""; lbwhs.Text = ""; //cbsalesman.Text = "";
            arr.Clear();

            if (rdreturtype.SelectedValue.ToString() == "I")
            {
                lbwhs.Text = "Bin Warehouse";
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vBindingComboToSpWithEmptyChoosen(ref cbwhs, "sp_tmst_warehouse_get", "whs_cd", "whs_nm", arr);
            }
            else if (rdreturtype.SelectedValue.ToString() == "C")
            {
                lbwhs.Text = "Bin Van";
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vBindingComboToSpWithEmptyChoosen(ref cbwhs, "sp_tmst_vehicle_get", "vhc_cd", "vhc_desc", arr);
            }
            else if (rdreturtype.SelectedValue.ToString() == "F")
            {
                cbwhs.CssClass = cd.csstextro;
                cbbin.CssClass = cd.csstextro;
            }
            cbwhs_SelectedIndexChanged(sender, e);
            txsearchcust.CssClass = cd.csstext;
            cbsalesman.Items.Clear(); cbsalesman.CssClass = cd.csstextro;
            txmanualno.CssClass = cd.csstext;
            btsave.CssClass = "btn btn-warning btn-save";
            //cbreason.CssClass = cd.csstext;
            cbremark.CssClass = cd.csstext;
            txcustmanualno.CssClass = cd.csstext;
            txsearchcust.CssClass = cd.csstext;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            lbretursta.Text = "NEW RETURN FROM CUSTOMER";

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_salesreturn");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void cbwhs_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            // Here three section.
            // 1:- Good / Damage
            // 2:- Near Expired
            // 3 :-Bad Stock
            List<cArrayList> arr = new List<cArrayList>();
            if (dtexp.Text != "")
            {
                DateTime dtexpire = DateTime.ParseExact(dtexp.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                if (System.DateTime.Today.AddDays(Convert.ToDouble(1)) >= dtexpire)
                {
                    // Bad Stock -- Rename by IAG, 3-Jul-18, Purposed : Is not allowed for bad stock
                    if (rdreturtype.SelectedValue.ToString() == "I")
                    {
                        arr.Add(new cArrayList("@whs_cd", cbwhs.SelectedValue.ToString()));
                        arr.Add(new cArrayList("@level", 0));
                        bll.vBindingComboToSpWithEmptyChoosen(ref cbbin, "sp_twarehouse_bin_getLevel", "bin_cd", "bin_nm", arr);
                    }
                    else
                    {
                        arr.Add(new cArrayList("@vhc_cd", cbwhs.SelectedValue.ToString()));
                        arr.Add(new cArrayList("@level", 0));
                        bll.vBindingComboToSpWithEmptyChoosen(ref cbbin, "sp_tvan_bin_getLevel", "bin_cd", "bin_nm", arr);

                    }

                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('It is not allowed return BAD STOCK','Pls contact Product Spv/Mgr','warning')", true);
                    //lbexp.Text = string.Empty; dtexp.Text = string.Empty;
                    //return;
                }
                else if (System.DateTime.Today.AddDays(Convert.ToDouble(90)) >= dtexpire)
                {
                    // Near Expired
                    if (rdreturtype.SelectedValue.ToString() == "I")
                    {
                        arr.Add(new cArrayList("@whs_cd", cbwhs.SelectedValue.ToString()));
                        arr.Add(new cArrayList("@level", 1));
                        bll.vBindingComboToSp(ref cbbin, "sp_twarehouse_bin_getLevel", "bin_cd", "bin_nm", arr);
                    }
                    else
                    {
                        arr.Add(new cArrayList("@vhc_cd", cbwhs.SelectedValue.ToString()));
                        arr.Add(new cArrayList("@level", 1));
                        bll.vBindingComboToSp(ref cbbin, "sp_tvan_bin_getLevel", "bin_cd", "bin_nm", arr);
                    }
                }
                else if (System.DateTime.Today.AddDays(Convert.ToDouble(90)) <= dtexpire)
                {
                    // Good / Damage
                    if (rdreturtype.SelectedValue.ToString() == "I")
                    {
                        arr.Add(new cArrayList("@whs_cd", cbwhs.SelectedValue.ToString()));
                        arr.Add(new cArrayList("@level", 2));
                        bll.vBindingComboToSp(ref cbbin, "sp_twarehouse_bin_getLevel", "bin_cd", "bin_nm", arr);
                    }
                    else
                    {
                        arr.Add(new cArrayList("@vhc_cd", cbwhs.SelectedValue.ToString()));
                        arr.Add(new cArrayList("@level", 2));
                        bll.vBindingComboToSp(ref cbbin, "sp_tvan_bin_getLevel", "bin_cd", "bin_nm", arr);
                    }
                }
            }

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_salesreturn");
            Response.Redirect("fm_ErrorPage.aspx");
        }



    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=rtn&returno=" + txreturno.Text + "');", true);
    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        grd.EditIndex = e.NewEditIndex;
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_twrk_salesreturn_get", arr);
    }
    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            TextBox txunitprice = (TextBox)grd.Rows[e.RowIndex].FindControl("txunitprice");
            Label lbitemcode = (Label)grd.Rows[e.RowIndex].FindControl("lbitemcode");
            Label lbqty = (Label)grd.Rows[e.RowIndex].FindControl("lbqty");
            Label lbexp = (Label)grd.Rows[e.RowIndex].FindControl("lbexp");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
            arr.Add(new cArrayList("@unitprice", txunitprice.Text));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@qty", lbqty.Text));
            arr.Add(new cArrayList("@exp_dt", System.DateTime.ParseExact(lbexp.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            bll.vUpdateWrkSalesReturn(arr);
            grd.EditIndex = -1;
            arr.Clear();
            BindingGrid();

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_salesreturn");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    void BindingGrid()
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_twrk_salesreturn_get", arr);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_salesreturn");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btcheckprice_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdcust.Value.ToString() == string.Empty)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Price can not be determined','Customer Not yet selected','warning');", true);
                txitemsearch.Text = string.Empty; hditem.Value = string.Empty; hdinvoice.Value = string.Empty;
                return;
            }

            //bool bTemp = Convert.ToBoolean(bll.vLookUp("select dbo.fn_checkcustreturninvoice('" + hdcust.Value + "','"+ Request.Cookies["sp"].Value.ToString() + "')"));
            //if (bTemp == true)
            //{
            //    txinvoicesearch.Text = bll.vLookUp("select dbo.fn_getinvoicereturn('" + hdcust.Value + "','" + hditem.Value + "','" + Request.Cookies["usr_id"].Value.ToString() + "','"+ Request.Cookies["sp"].Value.ToString() + "')");

            //    if ((txinvoicesearch.Text == string.Empty) || (txinvoicesearch.Text == ""))
            //    {
            //        hdinvoice.Value = string.Empty;
            //        txinvoicesearch.Text = "Invoice not found";
            //        //txinvoicesearch.CssClass = cd.csstextro;
            //    }
            //    else
            //    {
            hdinvoice.Value = txinvoicesearch.Text;
            lbqtyavl.Text = bll.vLookUp("select dbo.fn_checkqtyavlreturn('" + hdinvoice.Value + "','" + hditem.Value + "','" + Request.Cookies["usr_id"].Value.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "')");
            //    }

            //}
            //else
            //{
            //    hdinvoice.Value = string.Empty;
            //    txinvoicesearch.Text = "Invoice not found";
            //    //txinvoicesearch.CssClass = cd.csstextro;
            //}
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            txinvoicesearch.ForeColor = System.Drawing.Color.Red;
            txitemsearch.CssClass = cd.csstextro;
            //txinvoicesearch.CssClass = cd.csstextro;
            //hdinvoice.Value = txinvoicesearch.Text;
            cbuom.SelectedValue = string.Empty;
            txinvoicesearch_AutoCompleteExtender.ContextKey = hdcust.Value + ";" + hditem.Value;

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_salesreturn");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_salesreturn.aspx");
    }
    protected void cbuom_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (hditem.Value.ToString() == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Item not yet selected','select item','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                cbuom.SelectedValue = "";
                return;
            }

            if (cbuom.SelectedValue == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                return;
            }

            string sQtyAvl = lbqtyavl.Text = bll.vLookUp("select dbo.fn_checkqtyavlreturn('" + hdinvoice.Value + "','" + hditem.Value + "','" + Request.Cookies["usr_id"].Value.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "')");
            lbqtyavl.Text = bll.vLookUp("select dbo.fn_ItemConversion('CTN','" + cbuom.SelectedValue + "','" + hditem.Value + "'," + sQtyAvl + ")");
            dtexp.CssClass = cd.csstext;
            btprice_Click(sender, e);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_salesreturn");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btprice_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdcust.Value.Equals(DBNull.Value) || hdcust.Value == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Warning','Price can not determined because customer not yet selected !','warning');", true);
                txitemsearch.Text = "";
                hditem.Value = "";
                return;
            }

            double dConv = Convert.ToDouble(bll.vLookUp("select dbo.fn_convertsalesuom('" + hditem.Value.ToString() + "','" + cbuom.SelectedValue.ToString() + "')"));
            if (dConv == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There is no price setup or no setup UOM conversion!','Contact to wazaran admin','warning');", true);
                //lbstock.Text = "";
                lbprice.Text = "";
                cbuom.SelectedValue = "";
                return;
            }
            string sItemBlock = bll.vLookUp("select dbo.fn_checkitemblock('" + hdcust.Value.ToString() + "','" + hditem.Value.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "')");
            if (sItemBlock != "ok")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Item:" + hditem.Value.ToString() + "','Blocked for customer : " + hdcust.Value.ToString() + "','warning');", true);
                return;
            }
            //Check Adjustment Price
            double dPrice = 0;
            string sCustType = "";
            sCustType = bll.vLookUp("select otlcd from tmst_customer where cust_cd='" + hdcust.Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            double dAdjust = Convert.ToDouble(bll.vLookUp("select dbo.fn_getadjustmentprice ('" + hditem.Value.ToString() + "','" + hdcust.Value.ToString() + "','" + cbuom.SelectedValue.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "')"));
            if (dAdjust > 0)
            {
                dPrice = dAdjust;
            }
            else
            {

                dPrice = bll.dGetItemPrice(hditem.Value.ToString(), sCustType, cbuom.SelectedValue.ToString());
            }
            if (dPrice == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Price not yet setup or item conversion not setup!','Contact wazaran admin','warning');", true);
                lbprice.Text = string.Empty;
                cbuom.SelectedValue = "";
                return;
            }
            lbprice.Text = dPrice.ToString();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_salesreturn");
            Response.Redirect("fm_ErrorPage.aspx");
        }

    }
    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Label lbitemcode = (Label)grd.Rows[e.RowIndex].FindControl("lbitemcode");
            Label lbexp = (Label)grd.Rows[e.RowIndex].FindControl("lbexp");
            DateTime dtexpire = System.DateTime.ParseExact(lbexp.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
            arr.Add(new cArrayList("@exp_dt", dtexpire)); //Mod IAG , 5-May-2018
            bll.vDelWrkSalesReturn(arr);
            BindingGrid();
            Label lbtotamt = (Label)grd.FooterRow.FindControl("lbtotcustprice");
            lbamt.Text = lbtotamt.Text;
            //lbamt.Text = bll.vLookUp("select cast(sum(qty * custprice) as varchar) from twrk_salesreturn where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "'"); 

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_salesreturn");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btpaid_Click(object sender, EventArgs e)
    {
        try
        {
            double dBalance = 0;
            if ((lbamt.Text == "") || (lbamt.Text == "0"))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Amount is empty','Select item to be returned','success');", true);
                return;
            }

            if (grdinv.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('No Invoices to be paid','Payment will transferred to suspense','success');", true);
                return;
            }
            double dpaid = Convert.ToDouble(lbamt.Text);
            foreach (GridViewRow row in grdinv.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    Label lbbalance = (Label)row.FindControl("lbbalance");
                    TextBox txpaid = (TextBox)row.FindControl("txpaid");
                    dBalance = Convert.ToDouble(lbbalance.Text);
                    if ((dpaid - dBalance) > 0)
                    {
                        txpaid.Text = dBalance.ToString();
                    }
                    else if (dpaid > 0)
                    {
                        txpaid.Text = dpaid.ToString();

                    }
                    else if (dpaid <= 0)
                    {
                        txpaid.Text = "0";
                    }
                    dpaid -= dBalance;
                }
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_salesreturn");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void txqty_TextChanged(object sender, EventArgs e)
    {
        try
        {
            double dQty = 0;
            if (!double.TryParse(txqty.Text, out dQty))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Qty must numeric','wrong input','warning');", true);
                return;
            }
            txcustprice.Text = "0";
            lbtotprice.Text = "0";
            lbvat.Text = "0";
            //lbtotprice.Text = (dQty * Convert.ToDouble(lbprice.Text)).ToString();
            //string sVAT = bll.sGetControlParameter("vat");
            //double dVAT = Convert.ToDouble(sVAT);
            //double dTotVat = dVAT * Convert.ToDouble(lbtotprice.Text);
            //lbvat.Text = dTotVat.ToString();

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_salesreturn");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btsearchret_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "popupwindow('fm_lookup_return1.aspx');", true);
    }
    protected void btrefresh_Click(object sender, EventArgs e)
    {
        try
        {
            System.Data.SqlClient.SqlDataReader rs = null; string sReturStaID = string.Empty;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@retur_no", hdreturno.Value.ToString()));
            bll.vGetSalesReturn(ref rs, arr);
            while (rs.Read())
            {
                rdreturtype.SelectedValue = rs["retur_typ"].ToString();
                rdreturtype_SelectedIndexChanged(sender, e);
                rdreturtype.CssClass = "well well-sm radio radio-inline no-margin ro";
                txpaymentno.Text = rs["payment_no"].ToString();
                txreturno.Text = rs["retur_no"].ToString();
                txreturno.CssClass = "form-control ro";
                //  txdesc.Text = rs["remark"].ToString();
                try
                {
                    cbremark.SelectedValue = rs["remark"].ToString();
                }
                catch (Exception ex)
                {
                    bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : Combo Remark on return");
                }
                dtretur.Text = Convert.ToDateTime(rs["retur_dt"]).ToString("d/M/yyyy");
                dtretur.CssClass = cd.csstextro;
                hdcust.Value = rs["cust_cd"].ToString();
                txtabno.Text = rs["tab_no"].ToString();
                txtabno.CssClass = cd.csstextro;
                hdcust.Value = rs["cust_cd"].ToString();
                sReturStaID = rs["retur_sta_id"].ToString();
                txmanualno.Text = rs["manual_no"].ToString();
                lbretursta.Text = bll.vLookUp("select fld_desc from tfield_value where fld_nm='retur_sta_id' and fld_valu='" + rs["retur_sta_id"].ToString() + "'");
                txmanualno.CssClass = cd.csstextro;
                txcustmanualno.Text = rs["custmanual_no"].ToString();
                txcustmanualno.CssClass = cd.csstextro;
                txsearchcust.Text = bll.vLookUp("select cust_cd + '-' + cust_nm from tmst_customer where cust_cd='" + hdcust.Value.ToString() + "'");
                btsave.CssClass = "divhid";
                cbremark.CssClass = cd.csstextro;
                btprint.CssClass = "btn btn-info btn-print";
                btnew.CssClass = "btn btn-success btn-add";
                if (sReturStaID == "N")
                {
                    btadd.CssClass = "btn btn-success btn-add";
                    grd.AutoGenerateDeleteButton = false;
                    btcancel.CssClass = "btn btn-danger btn-delete";
                }
                else { btadd.CssClass = "divhid"; btcancel.CssClass = "divhid"; }
            }
            rs.Close();
            btsearchcust_Click(sender, e);
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@retur_no", hdreturno.Value.ToString()));
            bll.vInsertTwrkSalesReturnFromCore(arr);
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_twrk_salesreturn_get", arr);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_salesreturn");
            Response.Redirect("fm_ErrorPage.aspx");
        }

    }
    protected void grdinv_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            Label lbinvno = (Label)grdinv.Rows[e.NewSelectedIndex].FindControl("lbinvno");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@inv_no", lbinvno.Text));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vInsertWrkSalesreturnFromCore(arr);
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_twrk_salesreturn_get", arr);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_salesreturn");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void bttabsearch_Click(object sender, EventArgs e)
    {
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "window.open('fm_tabsalesreturn.aspx','mywindow','toolbar=n,scrollbars=y,width=800,height=800,top=75,left=300',true);", true);

    }
    protected void btcancel_Click(object sender, EventArgs e)
    {
        if (hdreturno.Value.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Retur not yet selected','Select retur to be cancelled','warning');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@retur_no", hdreturno.Value.ToString()));

    }
    protected void btedit_Click(object sender, EventArgs e)
    {
        grd.AutoGenerateSelectButton = false;
        grd.AutoGenerateDeleteButton = true;
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            System.Data.SqlClient.SqlDataReader rs = null; List<cArrayList> arr = new List<cArrayList>();

            Label lbitemcode = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbitemcode");
            Label lbexpx = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbexp");
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
            arr.Add(new cArrayList("@exp_dt", System.DateTime.ParseExact(lbexpx.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            bll.vGetWrkSalesReturn(arr, ref rs);
            while (rs.Read())
            {
                hditem.Value = lbitemcode.Text;
                txitemsearch.Text = rs["item_nm"].ToString();
                cbuom.SelectedValue = rs["uom"].ToString();
                txqty.Text = rs["qty"].ToString();
                lbprice.Text = rs["unitprice"].ToString();
                txcustprice.Text = rs["custprice"].ToString();
                dtexp.Text = Convert.ToDateTime(rs["exp_dt"]).ToString("d/M/yyyy");
                cbwhs.SelectedValue = rs["whs_cd"].ToString();
                lbtotprice.Text = (Convert.ToDouble(txqty.Text) * Convert.ToDouble(txcustprice.Text)).ToString();
                string sVAT = bll.sGetControlParameter("vat");
                double dVAT = Convert.ToDouble(sVAT);
                double dTotVat = dVAT * Convert.ToDouble(lbtotprice.Text);
                lbvat.Text = dTotVat.ToString();
                lbexp.Text = Convert.ToDateTime(rs["exp_dt"]).ToString("d/M/yyyy");
                dtexp.Text = Convert.ToDateTime(rs["exp_dt"]).ToString("d/M/yyyy");
                cbwhs.CssClass = cd.csstextro;
                cbbin.CssClass = cd.csstextro;
                txqty.CssClass = cd.csstextro;
                dtexp.CssClass = cd.csstextro;
                txitemsearch.CssClass = cd.csstextro;
                txqty.CssClass = cd.csstextro;
                cbuom.CssClass = cd.csstextro;
                //txinvoicesearch.CssClass = cd.csstextro;
                txcustprice.CssClass = cd.csstextro;
                cbwhs_SelectedIndexChanged(sender, e);
                cbbin.SelectedValue = rs["bin_cd"].ToString();
                txinvoicesearch.Text = rs["inv_no"].ToString();
                if (txinvoicesearch.Text != string.Empty)
                {
                    hdinvoice.Value = txinvoicesearch.Text;
                }
                else { hdinvoice.Value = string.Empty; }
                //txinvoicesearch.CssClass = cd.csstextro;
            }
            rs.Close();
            if (bll.nCheckAccess("editrtv", Request.Cookies["usr_id"].Value.ToString()) == 1)
            {
                txcustprice.CssClass = cd.csstext;
            }
            else { txcustprice.CssClass = cd.csstextro; }

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_salesreturn");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btcancel_Click2(object sender, EventArgs e)
    {

    }
    protected void btcancel2_Click(object sender, EventArgs e)
    {
        try
        {
            //Check available stock 
            string sCheck = bll.vLookUp("select dbo.fn_checkstocksalesreturn('" + hdreturno.Value.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "')");
            if (sCheck != "ok")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Retur No. " + hdreturno.Value.ToString() + " can not cancellation cause no stock available for item','" + sCheck + "','error');", true);
                return;
            }

            string sRet = bll.vLookUp("select dbo.fn_returhaspaid('" + hdreturno.Value.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "')");
            if (sRet != "ok")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('" + sRet + "','" + hdreturno.Value.ToString() + "','warning');", true);
                return;
            }
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@retur_no", hdreturno.Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@retur_sta_id", "L"));
            bll.vUpdateSalesreturnByStatus(arr);
            btsave.CssClass = "divhid";
            btcancel.CssClass = "divhid";
            btnew.CssClass = "btn btn-success btn-new";
            lbretursta.Text = "Cancelled";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Return has been cancelled','" + hdreturno.Value.ToString() + "','success');", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_salesreturn");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void chprevstk_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime ddate = DateTime.ParseExact(dtretur.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            var dateAsString = ddate.ToString("yyyy-MM-dd");
            string swhs_cd = bll.vLookUp("select whs_cd from tstockopname_schedule where whs_cd='" + cbwhs.SelectedValue + "' and schedule_dt='" + dateAsString + "'");
            if (swhs_cd != cbwhs.SelectedValue)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please entri schedule jaret !','Prev stk can not changed ','error');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
                // if (chprevstk.Checked == true) { chprevstk.Checked = false; } else { chprevstk.Checked = true; }
                return;
            }
            string slevel_no = bll.vLookUp("select level_no from tmst_warehouse  where whs_cd='" + cbwhs.SelectedValue + "'");
            if (slevel_no == "1")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Prev stk Only for Sub Depo and Van !','Prev stk can not changed ','error');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
                //  if (chprevstk.Checked == true) { chprevstk.Checked = false; } else { chprevstk.Checked = true; }
                return;
            }
            if (grd.Rows.Count != 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Item already inserted !','Prev stk can not changed ','error');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
                //  if (chprevstk.Checked == true) { chprevstk.Checked = false; } else { chprevstk.Checked = true; }
                return;
            }

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_salesreturn");
            Response.Redirect("fm_ErrorPage.aspx");
        }

    }

    protected string sEmailText(string sReturNo)
    {
        List<string> lapproval = bll.lAppProdSpv(sReturNo);
        if (lapproval.Count == 0)
        {
            return "No Approval";
        }

        string sTemp = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@retur_no", sReturNo));
        string sSubject = "#Return To Customer No." + sReturNo;



        return sTemp;
    }

    protected void txcustprice_TextChanged(object sender, EventArgs e)
    {
        try
        {
            double dQty = 0; double dCustPrice = 0; double dUnitPrice = 0;
            if (!double.TryParse(txqty.Text, out dQty))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Qty must numeric','wrong input','warning');", true);
                return;
            }
            if (!double.TryParse(txcustprice.Text, out dCustPrice))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Cust Price must numeric','wrong input','warning');", true);
                txcustprice.Text = "0";
                return;
            }
            if (!double.TryParse(lbprice.Text, out dUnitPrice))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Unit Price must numeric','wrong Unit Price','warning');", true);
                lbprice.Text = "";
                return;
            }

            if (dCustPrice > dUnitPrice)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Cust Price can not bigger than unit price','Check salesman price','warning');", true);
                dCustPrice = 0; lbvat.Text = ""; lbtotprice.Text = "";
                txcustprice.Text = "0";
                return;

            }

            lbtotprice.Text = (dQty * Convert.ToDouble(txcustprice.Text)).ToString();
            string sVAT = bll.sGetControlParameter("vat");
            double dVAT = Convert.ToDouble(sVAT);
            double dTotVat = dVAT * Convert.ToDouble(lbtotprice.Text);
            cbvat_SelectedIndexChanged(sender, e);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_salesreturn");
            Response.Redirect("fm_ErrorPage.aspx");
        }

    }

    protected void btReset_Click(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            txitemsearch.Text = string.Empty;
            hditem.Value = string.Empty;
            txqty.Text = string.Empty;
            lbqtyavl.Text = string.Empty;
            // lbamt.Text =  bll.vLookUp("select cast(sum(qty * custprice) as numeric(18,2)) from twrk_salesreturn where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "'");
            Label lbtotamt = new Label();
            if (grd.Rows.Count == 0) { }
            else { lbtotamt = (Label)grd.FooterRow.FindControl("lbtotcustprice"); }

            lbamt.Text = lbtotamt.Text;
            lbexp.Text = "";
            dtexp.Text = "";
            txcustprice.Text = "";
            lbprice.Text = "";
            cbuom.SelectedValue = "";
            lbprice.Text = "";
            lbtotprice.Text = "";
            lbvat.Text = "";
            txinvoicesearch.Text = string.Empty;
            txitemsearch.CssClass = cd.csstext;
            txqty.CssClass = cd.csstext;
            cbuom.CssClass = cd.csstext;
            dtexp.CssClass = cd.csstext;

            lbexp.CssClass = cd.csstext;
            if (rdreturtype.SelectedValue.ToString() == "I")
            {
                lbwhs.Text = "Bin Warehouse";
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vBindingComboToSp(ref cbwhs, "sp_tmst_warehouse_get", "whs_cd", "whs_nm", arr);
            }
            else if (rdreturtype.SelectedValue.ToString() == "C")
            {
                lbwhs.Text = "Bin Van";
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vBindingComboToSp(ref cbwhs, "sp_tmst_vehicle_get", "vhc_cd", "vhc_desc", arr);
                cbwhs.CssClass = cd.csstext;
                cbbin.CssClass = cd.csstext;
            }
            else if (rdreturtype.SelectedValue.ToString() == "F")
            {
                cbwhs.CssClass = cd.csstextro;
                cbbin.CssClass = cd.csstextro;
            }
            cbwhs_SelectedIndexChanged(sender, e);
            btsave.CssClass = "btn btn-warning btn-save";

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_salesreturn");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void cbvat_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (hditem.Value.ToString() == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Item is not yet selected','Select Item','warning');", true);
                cbvat.SelectedValue = "1";
                return;
            }
            if (cbvat.SelectedValue == "1")
            {
                lbvat.Text = (Convert.ToDouble(bll.sGetControlParameter("vat")) * (Convert.ToDouble(txcustprice.Text) * Convert.ToDouble(txqty.Text))).ToString();
                //lbtotprice.Text = (Convert.ToDouble(lbvat.Text) + Convert.ToDouble(lbtotprice.Text)).ToString();
            }
            else if (cbvat.SelectedValue == "0")
            {

                //lbtotprice.Text = (Convert.ToDouble(lbtotprice.Text) - Convert.ToDouble(lbvat.Text)).ToString();
                lbvat.Text = "0";
                //btprice_Click(sender, e);
            }

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_salesreturn");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void grdinv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    protected void btinvselect_Click(object sender, EventArgs e)
    {
        try
        {
            string sQtyAvl = bll.vLookUp("select dbo.fn_checkqtyavlreturn('" + hdinvoice.Value + "','" + hditem.Value + "','" + Request.Cookies["usr_id"].Value.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "')");
            lbqtyavl.Text = bll.vLookUp("select dbo.fn_ItemConversion('CTN','" + cbuom.SelectedValue + "','" + hditem.Value + "'," + sQtyAvl + ")");

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_salesreturn");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
}