using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class fm_do : System.Web.UI.Page
{
    cbll bll = new cbll();
    decimal totalQty = 0, totalqtydeliver = 0, totalsubtotal=0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                Session["salespoint"] = "";
                List<cArrayList> arr = new List<cArrayList>();
                arr.Clear();
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_getuser", "salespointcd", "salespoint_desc", arr);
                cbsalespoint.SelectedValue = Request.Cookies["sp"].Value.ToString();
                string sho = Request.Cookies["sp"].Value.ToString();
                if (sho == "0")
                {
                    cbsalespoint.Enabled = true;
                    //cbsalespoint.CssClass = "";
                    //cbsalespoint.Items.RemoveAt(0);
                }
                else
                {
                    cbsalespoint.Enabled = false;
                    cbsalespoint.CssClass = "makeitreadonly ro form-control";
                    cbsalespoint_SelectedIndexChanged(sender, e);
                }
                arr.Clear();
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vDelWrkPoDtl(arr);
                arr.Clear();
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
                bll.vDelWrkPoDtl(arr);
                arr.Clear();
                arr.Add(new cArrayList("@qry_cd", "whs_do"));
                bll.vBindingComboToSp(ref cbwarehouse, "sp_tmst_warehouse_getbyqry", "whs_cd", "whs_desc", arr);
                cbwarehouse_SelectedIndexChanged(sender, e);
                bll.vBindingComboToSp(ref cbtrella, "sp_tmst_vehicle_get", "vhc_cd", "vhc_nm");
                bll.vBindingFieldValueToCombo(ref cbexpedition, "comp_sta_id");
                bll.vBindingComboToSp(ref cbbox, "sp_tmst_trailer_box_get", "box_cd", "box_nm");
                cbexpedition.SelectedValue = "RENT";
                cbexpedition.CssClass = "makeitreadonly ro form-control";
                cbexpedition_SelectedIndexChanged(sender, e);
                //dtdo.Text = System.DateTime.Today.ToShortDateString();
                //bll.sFormat2ddmmyyyy(ref dtdo);
                string sdate = bll.sGetControlParameterSalespoint("wazaran_dt", cbsalespoint.SelectedValue.ToString());
                dtdo.Text = sdate;
                dtgdn.Text = sdate;
                //dtdo.Text = Request.Cookies["waz_dt"].Value.ToString();
                arr.Clear();
                //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                if (Request.QueryString["do"] != null)
                {
                    cbsalespoint.SelectedValue = Request.QueryString["sp"].ToString();
                    cbsalespoint.Enabled = false;
                    txpo.Enabled = false;
                    SqlDataReader rs = null;
                    arr.Clear();
                    arr.Add(new cArrayList("@salespointcd", Request.QueryString["sp"].ToString()));
                    arr.Add(new cArrayList("@do_no", Request.QueryString["do"].ToString()));
                    bll.vGetMstDO(arr, ref rs);
                    while (rs.Read())
                    {
                        txdono.Text = rs["do_no"].ToString();
                        dtdo.Text = string.Format("{0:d/M/yyyy}", rs["do_dt"]);
                        dtgdn.Text = string.Format("{0:d/M/yyyy}", rs["do_dt"]);
                        //dtdo.Text = rs["do_dt"].ToString();
                        //dtdo.Text=DateTime.ParseExact( rs["do_dt"],"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)
                        //txdono.Text = Request.QueryString["do"].ToString();
                        txpo.Text = rs["po_no"].ToString();
                        lbdosta.Text = bll.vLookUp("select fld_desc from tfield_value where fld_nm='do_sta_id' and fld_valu='" + rs["do_sta_id"].ToString() + "'");
                        string sPoStatus = rs["po_sta_id"].ToString();
                        if (sPoStatus == "C")
                        {
                            btsave.Enabled = false;
                            btnew.Enabled = true;
                            btprint.Enabled = true;
                        }
                        grdpodtl.Columns[8].Visible = false;
                        hdpo.Value = rs["po_no"].ToString();
                    }
                    rs.Close();
                    btsearch_Click(sender, e);
                    cbwarehouse.CssClass = "makeitreadonly form-control";
                    cbwarehouse.Enabled = false;
                    cbcompany.CssClass = "makeitreadonly form-control"; cbcompany.Enabled = false;
                    cbdriver.CssClass = "makeitreadonly form-control"; cbdriver.Enabled = false;
                    cbexpedition.CssClass = "makeitreadonly form-control"; cbexpedition.Enabled = false;
                    cbbox.CssClass = "makeitreadonly form-control"; cbbox.Enabled = false;
                    txmanualinvoice.CssClass = "makeitreadonly form-control"; txmanualinvoice.Enabled = false;
                    txgdn.CssClass = "makeitreadonly form-control"; txgdn.Enabled = false;
                    cbtrella.CssClass = "makeitreadonly form-control"; cbtrella.Enabled = false;
                    btadd.Enabled = false; txitemsearch.Enabled = false;
                    btprintinvoice.Visible = false;
                    btnew.Visible = true;
                    btsave.Visible = false;
                    txqty.Enabled = false;
                }
                btprintinvoice.Visible = false;
            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_do");
                Response.Redirect("fm_ErrorPage.aspx");
            }
        }
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        List<string> arr = new List<string>();
       // arr.Add("{tmst_po.po_no} = '" + lbpono.Text +  "'");
       // Session["lformula"] = arr;
      //  Response.Redirect("fm_report.aspx?src=do");
    }
   
    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {

            bool bSendEmail = false; bool bCheckCount = false; int nCount = 0;
            if (hdpo.Value.Equals("") || hdpo.Value.Equals(null))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Purchase Order must be selected','Please select PO','warning');", true);
                return;
            }

            if (dtdo.Text.Equals("") || dtdo.Text.Equals(null) || dtgdn.Text.Equals("") || dtgdn.Text.Equals(null))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Delivery / GDN Date cant empty!!','input date','warning');", true);
                return;
            }


            foreach (GridViewRow row in grdpodtl.Rows)
            {
                Label lbitemcode = (Label)row.FindControl("lbitemcode");
                Label lbitemname = (Label)row.FindControl("lbitemname");
                Label lbqty = (Label)row.FindControl("lbqty");
                Label lbqtydeliver = (Label)row.FindControl("lbqtydeliver");
                TextBox txdeliver = (TextBox)row.FindControl("txdeliver");
                Label lbstock = (Label)row.FindControl("lbstock");
                Label lbbalance = (Label)row.FindControl("lbbalance");
                if (lbstock.Text == "") { lbstock.Text = "0"; }
                decimal dqty = Convert.ToDecimal(lbqty.Text);

                if (txdeliver.Text == "") { txdeliver.Text = "0"; }
                //if (!Convert.ToBoolean(bll.vLookUp("select allowinvoice from tmst_warehouse where whs_cd='" + cbwarehouse.SelectedValue.ToString() + "'")))
                ////{
                //    if (Convert.ToDecimal(lbqtydeliver.Text) + Convert.ToDecimal(txdeliver.Text) > Convert.ToDecimal(lbstock.Text))
                //    {
                //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Stock is not enough !','Stock Not Enough','warning');", true);
                //        return;
                //    }
                //}
                
                if (((Convert.ToDecimal(lbbalance.Text) > 0) && //(Convert.ToDecimal(lbqtydeliver.Text) + 
                    (Convert.ToDecimal(txdeliver.Text) > Convert.ToDecimal(lbbalance.Text))) || (Convert.ToDecimal(lbbalance.Text)==0 && Convert.ToDecimal(txdeliver.Text) > 0))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Item : "+lbitemcode.Text+" - "+lbitemname.Text+" , Qty Delivered more than Balance !','Item Delivered','warning');", true);
                    return;
                }

                if (Convert.ToDecimal(txdeliver.Text) > 0)
                {
                    if (((Convert.ToDecimal(lbqtydeliver.Text) + Convert.ToDecimal(txdeliver.Text) - Convert.ToDecimal(lbqty.Text) > 0)))
                    {
                        bSendEmail = true;
                    }

                    nCount++;
                }
            }

            if (nCount == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('There is no item to be shipment !','Fill qty for item delivered','warning');", true);
                return;
            }

            int nCheckPO = Convert.ToInt32(bll.vLookUp("select count(1) from tpo_dtl where po_no='" + hdpo.Value.ToString() + "' and salespointcd='" + cbsalespoint.SelectedValue.ToString() + "'"));
            if (nCount > nCheckPO)
            {
                bSendEmail = true;
            }
            string sDO = "";
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@do_dt", DateTime.ParseExact(dtdo.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@gdn_dt", DateTime.ParseExact(dtgdn.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@po_no", hdpo.Value.ToString()));
            arr.Add(new cArrayList("@grandtotal", "0"));
            arr.Add(new cArrayList("@whs_cd", cbwarehouse.SelectedValue.ToString()));
            arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@delivery_typ", cbexpedition.SelectedValue.ToString()));
            arr.Add(new cArrayList("@driver_nm", txdrivername.Text));
            arr.Add(new cArrayList("@driver_cd", cbdriver.SelectedValue.ToString()));
            arr.Add(new cArrayList("@comp_cd", cbcompany.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
            arr.Add(new cArrayList("@box_cd", cbbox.SelectedValue.ToString()));
            arr.Add(new cArrayList("@vhc_cd", cbtrella.SelectedValue.ToString()));
            arr.Add(new cArrayList("@vhc_no", txtrella.Text));
            arr.Add(new cArrayList("@manual_no", txmanualinvoice.Text));
            arr.Add(new cArrayList("@docref_no", txgdn.Text));
            if (bSendEmail)
            {
                arr.Add(new cArrayList("@do_sta_id", "W"));
            }
            bll.vInsertMstDO(arr, ref sDO);
            txdono.Text = sDO;
            foreach (GridViewRow row in grdpodtl.Rows)
            {
                TextBox txdeliver = (TextBox)row.FindControl("txdeliver");
                Label lbitemcode = (Label)row.FindControl("lbitemcode");
                Label lbunitprice = (Label)row.FindControl("lbunitprice");
                Label lbsubtotal = (Label)row.FindControl("lbsubtotal");
                DropDownList cbuom = (DropDownList)row.FindControl("cbuom");
                arr.Clear();
                arr.Add(new cArrayList("@do_no", sDO));
                arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
                arr.Add(new cArrayList("@qty", txdeliver.Text));
                arr.Add(new cArrayList("@unitprice", lbunitprice.Text));
                arr.Add(new cArrayList("@subtotal", lbsubtotal.Text));
                arr.Add(new cArrayList("@uom", cbuom.SelectedValue.ToString()));
                arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
                //Only value bigger than zero will be inserted
                if (Convert.ToDouble(txdeliver.Text) > 0)
                {
                    bll.vInsertDoDtl(arr);
                }
            }
            arr.Clear();
            arr.Add(new cArrayList("@po_no", hdpo.Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
            bll.vBatchDO(arr);
            lbstatus.Text = bll.vLookUp("select (select fld_desc from tfield_value where fld_nm='po_sta_id' and fld_valu=tmst_po.po_sta_id) from tmst_po where po_no='" + hdpo.Value.ToString() + "' and salespointcd='" + cbsalespoint.SelectedValue.ToString() + "'");
            lbdosta.Text = bll.vLookUp("select (select fld_desc from tfield_value where fld_nm='do_sta_id' and fld_valu=tmst_do.do_sta_id) from tmst_do where do_no='" + sDO + "' and salespointcd='" + cbsalespoint.SelectedValue.ToString() + "'");
            btsave.Enabled = false;
            btprint.Enabled = true;
            arr.Clear();
            // Refresh Grid
            arr.Add(new cArrayList("@do_no", sDO));
            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
            bll.vBindingGridToSp(ref grdpodtl, "sp_tdo_dtl_get", arr);
            //if (bSendEmail)
            //{

            //    List<string> lapproval;
            //    lapproval = bll.lGetApproval("dobranch", 1);
            //    //string sMsg = "Dear ,\r\n There is modification Delivery PO from Branch for PO No. " + hdpo.Value.ToString() + "\r\n;" +
            //    //    "Please login to wazaran system to approve or reject this request ! \r\n" +
            //    //    "Best Regards ,\r\n\r\n\r\n" +
            //    //    "Wazaran Admin";
            //    //bll.vSendMail(lapproval[1], "PO Branch Modification", sMsg);
            //    //Get SMS
            //    Random rnd = new Random();
            //    int nRnd = rnd.Next(1000, 9999);
            //    string stoken = bll.vLookUp("select short_ip from tmst_salespoint where salespointcd=(select parm_valu from tcontrol_parameter where parm_nm='salespoint')") + nRnd.ToString();
            //   // cd.vSendSms("#PO Delivery changed DO No." + sDO + ", please approval or reject (Y/N)" + nRnd.ToString(), lapproval[0]);
            //    string sSMS = "#PO Delivery HO, DO No." + sDO + ", please approval or reject (Y/N)" + stoken.ToString();
            //    arr.Clear();
            //    arr.Add(new cArrayList("@doc_typ", "domodif"));
            // arr.Add(new cArrayList("@token", stoken.ToString()));
            // arr.Add(new cArrayList("@to", lapproval[0]));
            // arr.Add(new cArrayList("@doc_no",sDO));
            //    arr.Add(new cArrayList("@msg", sSMS.TrimEnd()));
            //    bll.vInsertSmsOutbox(arr);
            //  //  bll.vInsertSMSSent(arr);

            //}
            btsave.Visible = false;
            btprintinvoice.Visible = false;
            btnew.Visible = true;
            cbwarehouse.Enabled = false;
            cbdriver.Enabled = false;
            grdpodtl.Columns[11].Visible = false;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al2", "sweetAlert('Delivery Order has been saved successfully ...','DO No. " + sDO + "','warning');", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_do");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        HttpCookie cook;
        cook = HttpContext.Current.Request.Cookies["sp"];
        cbll bll = new cbll(); SqlDataReader rs = null;
        List<string> lPo = new List<string>();
        string sPo = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@po_no", prefixText));
        arr.Add(new cArrayList("@qry_cd", "po_pending"));
        //arr.Add(new cArrayList("@salespointcd", cook.Value.ToString()));
        arr.Add(new cArrayList("@salespointcd", HttpContext.Current.Session["salespoint"].ToString()));
        bll.vGetMstPOByQry(arr, ref rs);
        //bll.vSearchMstPo(arr, ref rs);
        while (rs.Read())
        {
            sPo = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["po_no"].ToString() + " - Salespoint : " + rs["salespoint_nm"].ToString() + " - Status : " + rs["po_sta_nm"].ToString() + " - " + rs["remark"].ToString() ,rs["po_no"].ToString());
            lPo.Add(sPo);
        }
        rs.Close();
        return (lPo.ToArray());
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        try
        {

            SqlDataReader rs = null; //string sTo; 
            string sStatus = "";
            totalQty = 0;
            totalqtydeliver = 0;
            totalsubtotal = 0;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@po_no", hdpo.Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
            bll.vGetMstPO(ref rs, arr);
            while (rs.Read())
            {
                sStatus = rs["po_sta_id"].ToString();
                lbstatus.Text = rs["po_sta_nm"].ToString();
                lbtoname.Text = rs["to_nm"].ToString();
                lbtoaddress.Text = rs["to_addr"].ToString();
                lbsalespoint.Text = rs["salespoint_nm"].ToString();
                hdsp.Value = rs["salespointcd"].ToString();
                //dtdo.Text = rs["po_sta_id"].ToString();
            }
            rs.Close();

            arr.Add(new cArrayList("@do_dt", DateTime.ParseExact(dtdo.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grdpodtl, "sp_tpo_dtl_get", arr);
            if (sStatus == "C")
            {
                grdpodtl.Columns[8].Visible = false;
                btsave.Visible = false;
            }

            // Check For NE Special Customer
            string customer = "", typecustomer = ""; string[] arrCustomer;
            customer = bll.vLookUp("select to_nm from tmst_po where po_no = '" + hdpo.Value.ToString() + "' and salespointcd='" + cbsalespoint.SelectedValue.ToString() + "'");
            arrCustomer = customer.Replace(" ", "").Split('-');
            typecustomer = bll.vLookUp("select cuscate_cd from tmst_customer where cust_cd = '" + arrCustomer[0] + "' and salespointcd='" + cbsalespoint.SelectedValue.ToString() + "'");

            //if (typecustomer == "SP")
            //{
            //    btadd.Enabled = false;
            //}
            //else
            //{
            btadd.Enabled = true;
            //}
            // Check For NE Special Customer
            grdpodtl.Visible = true;
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_do");
            Response.Redirect("fm_ErrorPage.aspx");
        }

    }
    protected void btprintdo_Click(object sender, EventArgs e)
    {
        List<string> lformula = new List<string>();
    }
    protected void btprintdo_Click1(object sender, EventArgs e)
    {
        try
        {

            List<string> lFormula = new List<string>();
            lFormula.Add("{tinvoice_branch.do_no} = '" + txdono.Text + "'");
            Session["lformula"] = lFormula;
            //Response.Redirect("fm_report.aspx?src=inv");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=inv&no=" + txdono.Text + "');", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_do");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    //protected void cbdeliverytype_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    List<cArrayList> arr = new List<cArrayList>();
    //    arr.Add(new cArrayList("@comp_sta_id", cbdeliverytype.SelectedValue.ToString()));
    //    bll.vBindingComboToSp(ref cbexpedition, "sp_tmst_company_expedition_get", "comp_cd","comp_nm", arr);
    //}

    protected void cbexpedition_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@comp_sta_id", cbexpedition.SelectedValue.ToString()));
            bll.vBindingComboToSp(ref cbcompany, "sp_tmst_company_expedition_get", "comp_cd", "comp_nm", arr);
            if (cbexpedition.SelectedValue == "OWN")
            {
                cbdriver.Visible = true;
                cbdriverPanel.Visible = true;
                txdrivername.Visible = false;
                arr.Clear();
                arr.Add(new cArrayList("@qry_cd", "driver"));
                arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
                bll.vBindingComboToSp(ref cbdriver, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
            }
            else
            {
                cbdriver.Visible = false;
                cbdriverPanel.Visible = false;
                txdrivername.Visible = true;
                txdrivername.Text = "";
                cbtrella.Visible = false;
                txtrella.Visible = true;
                txtrella.Text = "";
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_do");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList2(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll(); SqlDataReader rs = null;
        string sItem = string.Empty;
        List<string> lItem = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@item_nm", prefixText));
        bll.vSearchMstItem2(arr, ref rs);
        while (rs.Read())
        {
            sItem = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["item_cd"].ToString() + "-" + rs["item_nm"].ToString() + "'-" + rs["size"].ToString(), rs["item_cd"].ToString());
            lItem.Add(sItem);
        }
        rs.Close();
        return (lItem.ToArray());
    }
    protected void btcheckstock_Click(object sender, EventArgs e)
    {
        try
        {

            if ((hdpo.Value.Equals(null) || hdpo.Value.ToString() == ""))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('PO Must be select !','Please select PO','warning');", true);
                hditem.Value = "";
                txitemsearch.Text = "";
                txpo.Focus();
                return;
            }
            //double dStockAmt = Convert.ToDouble(bll.vLookUp("select isnull(sum(stock_amt),0) from tmst_stock where whs_cd='" + cbwarehouse.SelectedValue.ToString() + "' and bin_cd=(select qry_data from tmap_query where qry_cd='bin_do' and item_cd='" + hditem.Value.ToString() +  "')")); // Good stock
            DateTime ddate = DateTime.ParseExact(dtdo.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            double dStockAmt = Convert.ToDouble(bll.vLookUp("select isnull(sum(stkbalance),0) from tblstock where whs_cd='" + cbwarehouse.SelectedValue.ToString() + "' and monthcd= dbo.fnFormatDate ('" + ddate + "', 'YYYYMM') and bin_cd=(select qry_data from tmap_query where qry_cd='bin_do' and item_cd='" + hditem.Value.ToString() + "') and SalesPointCD='" + cbsalespoint.SelectedValue.ToString() + "'")); // by yanto 11-7-2016
            lbstock.Text = dStockAmt.ToString();
            txitemsearch.Focus();
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_do");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        try
        {
            double dQty = 0;
            txpoPnl.CssClass = "";
            txitemsearchPnl.CssClass = "";

            if ((hdpo.Value.Equals(null) || hdpo.Value.ToString() == ""))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('PO Must be select !','Please select PO','warning');", true);
                hditem.Value = "";
                txitemsearch.Text = "";
                txpoPnl.CssClass = "error";
                txpo.Focus();
                return;
            }
            if ((txitemsearch.Text == "") || (txitemsearch.Text.Equals(null)))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Item must be selected !','Please select Item','warning');", true);
                txitemsearchPnl.CssClass = "error";
                return;
            }
            if (!double.TryParse(txqty.Text, out dQty))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Qty must be numeric','Check Qty','warning');", true);
                txitemsearchPnl.CssClass = "error";
                return;
            }
            //Get Price
            // string sCustCode = ""; string sSP="";
            //sCustCode = bll.vLookUp("select salespointcd from tmst_po where po_no='" + hdpo.Value.ToString() + "'");

            List<cArrayList> arr = new List<cArrayList>();
            //arr.Add(new cArrayList("@item_cd", hditem.Value.ToString()));
            //arr.Add(new cArrayList("@cust_cd", sCustCode));
            //arr.Add(new cArrayList("@salespointcd",sSP));

            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@item_cd", hditem.Value.ToString()));
            arr.Add(new cArrayList("@qty", txqty.Text));
            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
            bll.vInsertWrkPoDtl(arr);
            btsearch_Click(sender, e);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_do");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_do.aspx");
    }
    protected void cbdriver_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            string sVehicle = bll.vLookUp("select vhc_cd from tmst_vehicle where emp_cd='" + cbdriver.SelectedValue.ToString() + "'");
            if (!sVehicle.Equals(""))
            {
                cbtrella.SelectedValue = sVehicle;
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_do");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grdpodtl_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList cbuom = (DropDownList)e.Row.FindControl("cbuom");
                bll.vBindingFieldValueToCombo(ref cbuom, "uom_po", "uom");
                Label lbqty = (Label)e.Row.FindControl("lbqty");
                Label lbqtydeliver = (Label)e.Row.FindControl("lbqtydeliver");
                Label lbbalance = (Label)e.Row.FindControl("lbbalance");
                Label lbunitprice = (Label)e.Row.FindControl("lbunitprice");
                Label lbsubtotal = (Label)e.Row.FindControl("lbsubtotal");
                lbbalance.Text = (Convert.ToDouble(lbqty.Text) - Convert.ToDouble(lbqtydeliver.Text)).ToString();
                double dSubTotal = Convert.ToDouble((lbunitprice.Text == "" ? "0" : lbunitprice.Text)) * Convert.ToDouble(lbqtydeliver.Text);
                lbsubtotal.Text = dSubTotal.ToString(System.Globalization.CultureInfo.InvariantCulture.NumberFormat);

                decimal qty, qtydeliver, subtotal;
                if (lbqty != null)
                    qty = decimal.Parse(lbqty.Text);
                else
                    qty = 0;
                totalQty = totalQty + qty;

                if (lbqtydeliver != null)
                    qtydeliver = decimal.Parse(lbqtydeliver.Text);
                else
                    qtydeliver = 0;
                totalqtydeliver = totalqtydeliver + qtydeliver;

                if (lbsubtotal != null)
                    subtotal = decimal.Parse(lbsubtotal.Text);
                else
                    subtotal = 0;
                totalsubtotal = totalsubtotal + subtotal;

                // Check For NE Special Customer
                string customer = "", typecustomer = ""; string[] arrCustomer;
                customer = bll.vLookUp("select to_nm from tmst_po where po_no = '" + hdpo.Value.ToString() + "' and salespointcd='" + cbsalespoint.SelectedValue.ToString() + "'");
                arrCustomer = customer.Replace(" ", "").Split('-');
                typecustomer = bll.vLookUp("select cuscate_cd from tmst_customer where cust_cd = '" + arrCustomer[0] + "'");

                //if (typecustomer == "SP")
                //{
                cbuom.Enabled = true;
                //}
                //else
                //{
                //    cbuom.Enabled = false;
                //}
                // Check For NE Special Customer

            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalqty = (Label)e.Row.FindControl("lblTotalqty");
                Label lblTotalqtydeliver = (Label)e.Row.FindControl("lblTotalqtydeliver");
                Label lblTotalsubtotal = (Label)e.Row.FindControl("lblTotalsubtotal");
                lblTotalqty.Text = totalQty.ToString("#,##0.00");
                lblTotalqtydeliver.Text = totalqtydeliver.ToString("#,##0.00");
                lblTotalsubtotal.Text = totalsubtotal.ToString("#,##0.00");
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_do");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void cbwarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            lbcap.Text = bll.vLookUp("select captionusage from tmst_warehouse where whs_cd='" + cbwarehouse.SelectedValue.ToString() + "'");
            btprintinvoice.Visible = Convert.ToBoolean(bll.vLookUp("select isnull(allowinvoice,0) from tmst_warehouse where whs_cd='" + cbwarehouse.SelectedValue.ToString() + "'"));
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_do");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btprintinvoice_Click(object sender, EventArgs e)
    {
        try
        {

            //if ((txmanualinvoice.Text == "") || txmanualinvoice.Equals(null))
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Manual Invoice must key in','please fill manual invoice','warning');", true);
            //    txmanualinvoice.Focus();
            //    return;
            //}

            //if ((hdpo.Value.ToString() == "") || (hdpo.Value.Equals(null)))
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('PO Must be selected !','please PO for next process','warning');", true);
            //    txpo.Focus();
            //    return;
            //}
            List<string> lFormula = new List<string>();
            lFormula.Add("{tinvoice_branch.do_no} = '" + txdono.Text + "'");
            Session["lformula"] = lFormula;
            //Response.Redirect("fm_report.aspx?src=inv");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=inv&no=" + txdono.Text + "');", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_do");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void btprintdo_Click2(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=doho&no=" + txdono.Text + "');", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_do");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void cbsalespoint_SelectedIndexChanged(object sender, EventArgs e)
    {
        string sdate = bll.sGetControlParameterSalespoint("wazaran_dt", cbsalespoint.SelectedValue.ToString());
        dtdo.Text = sdate;
        dtgdn.Text = sdate;
        Session["salespoint"] = cbsalespoint.SelectedValue.ToString();
        hdpo.Value = "";
        txpo.Text = "";
        lbstatus.Text = "";
        lbtoname.Text = "";
        lbtoaddress.Text = "";
        lbsalespoint.Text = "";
        hdsp.Value = "";
        grdpodtl.Visible = false;
    }
}