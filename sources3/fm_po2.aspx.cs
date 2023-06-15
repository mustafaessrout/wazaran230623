using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using AjaxControlToolkit;
using System.Net.NetworkInformation;
public partial class fm_po2 : System.Web.UI.Page
{
    cbll bll = new cbll();
    //cbllHO bllHO = new cbllHO();
    Utitlity ut = new Utitlity();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_get", "salespointcd", "salespoint_desc");
                
                bll.vBindingFieldValueToCombo(ref cbuom, "uom_po", "uom");
                //CheckHOStatus();
                //   ModalPopupExtender1.Show();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@po_no", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vDelPoDtl3(arr);
                //bllHO.vDelPoDtl3(arr);
                arr.Clear();
                arr.Add(new cArrayList("@fld_nm", "po_typ"));
                arr.Add(new cArrayList("@hiddendata", 0));
                bll.vBindingFieldValueToCombo(ref cbtype, arr);
                string sdate = bll.sGetControlParameterSalespoint("wazaran_dt", Request.Cookies["sp"].Value.ToString());
                //bll.vLookUp("select dbo.fn_getsystemdate('"+ Request.Cookies["sp"].Value.ToString() + "')").Substring(0, 10);
                arr.Clear();
                arr.Add(new cArrayList("@vendor_typ", "SP"));
                bll.vBindingComboToSp(ref cbvendor, "sp_tmst_vendor_get", "vendor_cd", "vendor_nm", arr);

                dtpo_delivery_dt_CalendarExtender.StartDate = DateTime.Now.AddDays(1).Date;
                arr.Clear(); arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                //if (grdpo.Rows.Count > 1)
                //{ 
                //txtotqty.Text = bll.dblSumTmpPoDtl(arr).ToString();
                //}
                //dtpo.Text = System.DateTime.Today.ToShortDateString();//Request.Cookies["waz_dt"].Value.ToString();
                //dtpo_delivery_dt.Text = System.DateTime.Today.ToShortDateString();
                //bll.sFormat2ddmmyyyy(ref dtpo_delivery_dt);
                //bll.sFormat2ddmmyyyy(ref dtpo);
                dtpo.Text = Request.Cookies["waz_dt"].Value.ToString();
                dtpo_delivery_dt.Text = Request.Cookies["waz_dt"].Value.ToString();
                cbtype_SelectedIndexChanged(sender, e);
                //lbsalespoint.Text = Request.Cookies["sp"].Value.ToString() + "-" + bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                //btprint.Enabled = false;
                txpono.Text = "NEW";
                btedit.Visible = false;
                //bll.vButtonInit(ref btnew, ref btedit, ref btsave, ref btprint);
                if (Request.QueryString["po"] != null)
                {
                    System.Data.SqlClient.SqlDataReader rs = null;
                    string sPoNo = Request.QueryString["po"].ToString();
                    txpono.Text = sPoNo;
                    arr.Clear();
                    arr.Add(new cArrayList("@po_no", sPoNo));
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    bll.vGetMstPO(ref rs, arr);
                    while (rs.Read())
                    {
                        dtpo.Text = string.Format("{0:d/M/yyyy}", rs["po_dt"]);
                        //dtpo.Text = rs["po_dt"].ToString();
                        dtpo_delivery_dt.Text = string.Format("{0:d/M/yyyy}", rs["po_delivery_dt"]);
                        //dtpo_delivery_dt.Text = rs["po_delivery_dt"].ToString(); //by yanto 4-8-15
                        cbtype.SelectedValue = rs["delivery_typ"].ToString();
                        lbaddress.Text = rs["to_addr"].ToString();
                        lbphone.Text = rs["to_phone"].ToString();
                        txremark.Text = rs["remark"].ToString();
                        lbstatus.Text = rs["po_sta_nm"].ToString();
                        cbvendor.SelectedValue = rs["vendor_cd"].ToString();
                        //arr.Clear();
                        //arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                        //arr.Add(new cArrayList("@po_no", sPoNo));
                        //bll.vInsertWrkPoDtl2(arr);
                        //arr.Clear();
                        //arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                        //bll.vBindingGridToSp(ref grdpo, "sp_twrk_podtl_get", arr);
                        bindinggrd();
                        btprint.Enabled = true;
                    }
                    rs.Close();
                    //txtotqty.Text = bll.vLookUp("select sum(qty) from twrk_podtl where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "'");
                    //txtotalprice.Text = bll.vLookUp("select sum(qty * price_sell) from twrk_podtl a join tmst_item b on a.item_cd=b.item_cd  where a.usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "'");
                    txitemname.Text = "";
                    txqty.Text = "";
                    //cbmaster.Enabled = false;
                    //cbtype.Enabled = false;
                    //cbvendor.Enabled = false;
                    //txqty.Enabled = false;
                    //txremark.Enabled = false;
                    txsearch.Enabled = false;
                    //txtotalprice.Enabled = false;
                    //txtotqty.Enabled = false;
                    btsave.Visible = true;
                    btnew.Visible = true;
                    btprint.Visible = true;
                    btedit.Visible = true;
                    grdpo.Columns[9].Visible = false;
                    //txitemname.Enabled = false;
                    //dtpo.Enabled = false;
                    //dtpo_delivery_dt.Enabled = false;
                    txpono.Enabled = false;
                    txstock.Enabled = false;
                    //btadd.Enabled = false;
                    btnew.Enabled = false;
                    //btsearchpo.Enabled = false;
                }
                bindinggrdloading();
            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_po");
                Response.Redirect("fm_ErrorPage.aspx");
            }
        }
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);

    }

    private void bindinggrdloading()
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grdloading, "sp_tinternal_transfer_summary_get", arr);
            if (grdloading.Rows.Count > 0)
            {
                Label lbtotqty = (Label)grdloading.FooterRow.FindControl("lbtotqty");
                lbtotqty.Text = bll.vLookUp("select sum(b.qty) from tinternal_transfer a inner join tinternal_transfer_dtl b on a.trf_no=b.trf_no and a.salespointcd=b.salespointcd where a.sta_id in ('A','W') and a.trf_typ = 'I' and a.salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_po");
            Response.Redirect("fm_ErrorPage.aspx");
        }
           
    }

    //void CheckHOStatus() {
    //    Ping pg = new Ping();
    //    PingReply reply = pg.Send("172.16.1.18");
    //    bool status = reply.Status == IPStatus.Success;
    //    if (status == false)
    //    {
    //        dvHOStatusValue.Style.Add("background-color", "red");
    //        hdfBranchConnected.Value = "false";
    //    }
    //    else
    //    {
    //        dvHOStatusValue.Style.Add("background-color", "green");
    //        hdfBranchConnected.Value = "true";
    //    }
    //}
    protected void cbproduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        //  arr.Add(new cArrayList("@prod_cd", cbproduct.SelectedValue.ToString()));
        // bll.vBindingComboToSp(ref cbitem, "sp_tmst_item_get", "item_cd", "item_nm", arr);
    }
   
    protected void grdpo_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {

            string po_no;
            if (txpono.Text == "" || txpono.Text == "NEW") { po_no = Request.Cookies["usr_id"].Value.ToString(); } else { po_no = txpono.Text; }
            if (lbstatus.Text == "Completed")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction has been complete, data can not save','error');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
                return;
            }
            if (Request.Cookies["waz_dt"].Value.ToString() != dtpo.Text)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction Date Out Of Periode Date','error');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
                return;
            }
            Label lbitemcode = (Label)grdpo.Rows[e.RowIndex].FindControl("lbitemcode");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
            arr.Add(new cArrayList("@po_no", po_no));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vDelPoDtl(arr);
            //arr.Clear();
            //arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            //bll.vBindingGridToSp(ref grdpo, "sp_twrk_podtl_get", arr);
            bindinggrd();
            //txtotqty.Text = bll.vLookUp("select sum(qty) from twrk_podtl where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "'");
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_po");
            Response.Redirect("fm_ErrorPage.aspx");
        }

    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            string po_no;
            string sto_nm = "";
            string typeCustomer = "";
            if (cbtype.SelectedValue.ToString().Trim() == "CU") { typeCustomer = bll.vLookUp("select cuscate_cd from tmst_customer where cust_cd='" + hdcust.Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'"); sto_nm = txsearch.Text; } else { sto_nm = cbmaster.SelectedItem.Text; }
            if (txpono.Text == "" || txpono.Text == "NEW") { po_no = Request.Cookies["usr_id"].Value.ToString(); } else { po_no = txpono.Text; }
            if (cbtype.SelectedValue == "CU" && hdcust.Value == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Please input customer , data can not save','error');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
                return;
            }
            if (lbstatus.Text == "Completed")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction has been complete, data can not save','error');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
                return;
            }
            if (Request.Cookies["waz_dt"].Value.ToString() != dtpo.Text)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction Date Out Of Periode Date','error');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
                return;
            }
            if (txpono.Text == "NEW" || txpono.Text == "")
            {
                if (grdpo.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Item has not yet selected !','Please select Item To Be Ordered','warning');", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
                    return;
                }
                List<cArrayList> arr = new List<cArrayList>();
                string sPoNo = "";
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@po_dt", DateTime.ParseExact(dtpo.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@po_delivery_dt", DateTime.ParseExact(dtpo_delivery_dt.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture))); // by yanto 4-8-15
                arr.Add(new cArrayList("@po_typ", cbtype.SelectedValue.ToString()));
                arr.Add(new cArrayList("@vendor_cd", cbvendor.SelectedValue.ToString()));
                arr.Add(new cArrayList("@remark", txremark.Text));
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@to_nm", sto_nm));
                arr.Add(new cArrayList("@to_addr", lbaddress.Text + "," + lbcity.Text));
                arr.Add(new cArrayList("@delivery_typ", cbtype.SelectedValue.ToString()));
                //arr.Add(new cArrayList("@tot_amt", txtotalprice.Text));

                Label lbtotsubtotal = (Label)grdpo.FooterRow.FindControl("lbtotsubtotal");
                arr.Add(new cArrayList("@tot_amt", lbtotsubtotal.Text));


                bll.vInsertMstPO(arr, ref sPoNo);
                //if (hdfBranchConnected.Value == "true")
                //{

                //    arr.Add(new cArrayList("@po_no", sPoNo));
                //    //bllHO.vInsertMstPO(arr);
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('HO not connected !','HO not connected','warning');", true);
                //}

                txpono.Text = sPoNo;
                btprint.Enabled = true;
                // send email
                //System.Data.SqlClient.SqlDataReader rs = null;
                //string sSubject = ""; string sMessage = "";
                //string sItem = string.Empty;
                //sSubject = "New PO Branch " + bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                //sMessage = "PO No. " + txpono.Text + " created by " + Request.Cookies["usr_id"].Value.ToString() + ", the following item was request \n\r";
                //arr.Clear();
                //arr.Add(new cArrayList("@po_no", txpono.Text));
                //arr.Add(new cArrayList("@do_dt", DateTime.ParseExact(dtpo_delivery_dt.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //bll.vGetPoDtl(arr, ref rs);
                //sItem = "<table><tr style='background-color:silver'><td>Item Code</td><td>Item Name</td><td>Quantity Order</td><td>UOM</td></tr>";
                //while (rs.Read())
                //{
                //    sItem += "<tr><td></td>" + rs["item_cd"].ToString() + "</td><td>" + rs["item_nm"] + "</td><td>" + rs["qty"].ToString() + "</td><td>"+rs["uom"].ToString()+"</td></tr>";
                //}
                //rs.Close(); sItem += "</table>";
                //sMessage += sItem + "\r\n\r\n\r\n Wazaran Admin";
                //List<string> lapproval = bll.lGetApproval("pobranch", 1);
                ////bll.vSendMail(lapproval[1], sSubject, sMessage);
                //arr.Clear();
                //arr.Add(new cArrayList("@token", 0));
                //arr.Add(new cArrayList("@doc_typ", "PO"));
                //arr.Add(new cArrayList("@to", lapproval[1]));
                //arr.Add(new cArrayList("@doc_no", txpono.Text));
                //arr.Add(new cArrayList("@emailsubject", sSubject));
                //arr.Add(new cArrayList("@msg", sMessage));
                //arr.Add(new cArrayList("@file_attachment", null));
                //bll.vInsertEmailOutbox(arr); //by yanto 23-1-2017
                btsave.Visible = false;
                btprint.Visible = true;
                btnew.Visible = true;
                btedit.Visible = true;
                grdpo.Columns[9].Visible = false;
                cbtype.CssClass = "makeitreadonly";
                cbmaster.CssClass = "makeitreadonly";
                cbvendor.CssClass = "makeitreadonly";
                txsearch.CssClass = "makeitreadonly";
                txqty.CssClass = "makeitreadonly";
                txremark.CssClass = "makeitreadonly";
                txstock.CssClass = "makeitreadonly";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('PO has saved succesfully ..','PO No. " + sPoNo + "','success');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
            }
            else
            {
                List<cArrayList> arr = new List<cArrayList>();

                arr.Clear();
                arr.Add(new cArrayList("@po_no", txpono.Text));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@po_dt", DateTime.ParseExact(dtpo.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));

                arr.Add(new cArrayList("@po_delivery_dt", DateTime.ParseExact(dtpo_delivery_dt.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture))); // by yanto 4-8-15
                                                                                                                                                                       //arr.Add(new cArrayList("@po_typ", cbtype.SelectedValue.ToString()));
                arr.Add(new cArrayList("@vendor_cd", cbvendor.SelectedValue.ToString()));
                arr.Add(new cArrayList("@remark", txremark.Text));
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@to_nm", sto_nm));
                arr.Add(new cArrayList("@to_addr", lbaddress.Text + "," + lbcity.Text));
                arr.Add(new cArrayList("@delivery_typ", cbtype.SelectedValue.ToString()));
                //arr.Add(new cArrayList("@tot_amt", txtotalprice.Text));

                Label lbtotsubtotal = (Label)grdpo.FooterRow.FindControl("lbtotsubtotal");
                arr.Add(new cArrayList("@tot_amt", lbtotsubtotal.Text));

                bll.vUpdateTmst_po(arr);
                //if (hdfBranchConnected.Value == "true")
                //{

                //    bllHO.vUpdateTmst_po(arr);
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('HO not connected !','HO not connected','warning');", true);
                //}
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('PO has saved succesfully updated ..','PO No. " + txpono.Text + "','info');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_po");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        
    }
    //protected void btSearchHO_Click(object sender, EventArgs e)
    //{
    //    CheckHOStatus();
    //}
    
    protected void bttmp3_Click(object sender, EventArgs e)
    {
        try
        {
            SqlDataReader rs = null;
            txpono.Text = Convert.ToString(Session["loopo_no"]);
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@po_no", txpono.Text));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vGetMstPO(ref rs, arr);
            while (rs.Read())
            {
                dtpo.Text = string.Format("{0:d/M/yyyy}", rs["po_dt"]);
                dtpo_delivery_dt.Text = string.Format("{0:d/M/yyyy}", rs["po_delivery_dt"]);
                cbtype.SelectedValue = rs["delivery_typ"].ToString();
                lbaddress.Text = rs["to_addr"].ToString();
                lbphone.Text = rs["to_phone"].ToString();
                txremark.Text = rs["remark"].ToString();
                lbstatus.Text = rs["po_sta_nm"].ToString();
                cbvendor.SelectedValue = rs["vendor_cd"].ToString();
                if (rs["delivery_typ"].ToString() == "CU")
                {
                    txsearch.Text = rs["to_nm"].ToString();
                }
            }
            rs.Close();
            string[] cust_cd = txsearch.Text.Replace(" ", "").Split('-');
            hdcust.Value = cust_cd[0];
            bindinggrd();
            btsave.Visible = false;

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_po");
            Response.Redirect("fm_ErrorPage.aspx");
        }

    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        try
        {

            double dOut = 0; double dqty = 0;
            string po_no;
            txstock.Text = "0";
            if (lbstatus.Text == "Completed")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction has been complete, data can not save','error');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
                return;
            }
            txitemnamePnl.CssClass = "";
            if (hditem.Value.ToString() == "" || hditem.Value.Equals(null))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Item Not selected !','Please select item','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
                txitemnamePnl.CssClass = "error";
                return;
            }
            txqtyPnl.CssClass = "";
            if (!double.TryParse(txqty.Text, out dqty))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('There no item order quantity','Check your qty order','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
                txqtyPnl.CssClass = "error";
                return;
            }

            string priceItem = "";
            priceItem = bll.vLookUp("select unitprice from tcustomertype_price where cust_typ in (select qry_data from tmap_query where qry_cd='price_po') and item_cd='" + hditem.Value.ToString() + "'");
            if (priceItem == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Unitprice not available.','Contact Head Office!!!','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
                return;
            }

            // Modified 27-11-2019 : Overdue Customer can't make transaction (all type: cash/credit)
            if (cbtype.SelectedValue.ToString() == "CU")
            {
                if (bll.vLookUp("select dbo.fn_checkcustblocked('" + hdcust.Value.ToString() + "','S','" + Request.Cookies["sp"].Value.ToString() + "')") == "ok")
                {
                    double dmaxTransaction = Double.Parse(bll.vLookUp("select dbo.fn_getmaxtransactionamt('" + hdcust.Value.ToString() + "','"+ Request.Cookies["sp"].Value.ToString() + "')"));
                    double doverdue = Double.Parse(bll.vLookUp("select isnull(dbo.fn_getoverdue('" + hdcust.Value.ToString() + "','"+ Request.Cookies["sp"].Value.ToString() + "'),0)"));
                    if (doverdue > 0)
                    {
                        if (doverdue > dmaxTransaction)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Not Allow to Continue the Process! Please Check Customer Due Invoice','','warning');", true);
                            return;
                        }
                    }
                }
            }
            // Modified 27-11-2019 : Overdue Customer can't make transaction (all type: cash/credit)


            if (txpono.Text == "" || txpono.Text == "NEW") { po_no = Request.Cookies["usr_id"].Value.ToString(); } else { po_no = txpono.Text; }
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@item_cd", hditem.Value.ToString()));
            arr.Add(new cArrayList("@po_no", po_no));
            arr.Add(new cArrayList("@qty", txqty.Text));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@stock", txstock.Text));
            arr.Add(new cArrayList("@whs_cd", cbmaster.SelectedValue.ToString()));
            arr.Add(new cArrayList("@uom", cbuom.SelectedValue.ToString()));
            bll.vInserttpo_dtl_ins3(arr);

            //if (hdfBranchConnected.Value == "true")
            //{
            //    
            //    //bllHO.vInserttpo_dtl_ins3(arr);
            //}
            //else {
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('HO not connected !','HO not connected','warning');", true);
            //}
            arr.Clear();
           
            txitemname.Text = "";
            txqty.Text = "";
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "set", "$('#txqty').val('0');", true);
            //txitemname.Focus();
            //CheckHOStatus();
            //grdpo.Focus();
            bindinggrd();
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_po");
            Response.Redirect("fm_ErrorPage.aspx");
        }        
    }
    private void bindinggrd()
    {
        try
        {

            string po_no; string typeCustomer = "";
            typeCustomer = bll.vLookUp("select cuscate_cd from tmst_customer where cust_cd='" + hdcust.Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            if (txpono.Text == "" || txpono.Text == "NEW") { po_no = Request.Cookies["usr_id"].Value.ToString(); } else { po_no = txpono.Text; }
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@po_no", po_no));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@whs_cd", cbmaster.SelectedValue.ToString()));
            bll.vBindingGridToSp(ref grdpo, "sp_tpo_dtl_get2", arr);
            //bll.vBindingGridToSp(ref grdpo1, "sp_tpo_dtl_get2", arr);
            //if (typeCustomer == "SP")
            //{
            //    if (grdpo1.Rows.Count > 0)
            //    {
            //        Label lbtotqty = (Label)grdpo1.FooterRow.FindControl("lbtotqty");
            //        Label lbtotsubtotal = (Label)grdpo1.FooterRow.FindControl("lbtotsubtotal");
            //        lbtotqty.Text = bll.vLookUp("select sum(qty) from tpo_dtl where po_no='" + po_no + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            //        lbtotsubtotal.Text = bll.vLookUp("select sum(subtotal) from tpo_dtl where po_no='" + po_no + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            //    }            
            //    vNotSpecial.Visible = false;
            //    vSpecial.Visible = true;
            //    vSpecial.Attributes.Remove("style");
            //    vNotSpecial.Attributes.Add("style", "display:none");
            //}
            //else
            //{
            if (grdpo.Rows.Count > 0)
            {
                Label lbtotqty = (Label)grdpo.FooterRow.FindControl("lbtotqty");
                Label lbtotsubtotal = (Label)grdpo.FooterRow.FindControl("lbtotsubtotal");
                lbtotqty.Text = bll.vLookUp("select sum(qty) from tpo_dtl where po_no='" + po_no + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                lbtotsubtotal.Text = bll.vLookUp("select sum(subtotal) from tpo_dtl where po_no='" + po_no + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            }
            vNotSpecial.Visible = true;
            //vSpecial.Visible = false;
            vNotSpecial.Attributes.Remove("style");
            //vSpecial.Attributes.Add("style", "display:none");
            //}
            //txtotqty.Text = bll.vLookUp("select sum(qty) from tpo_dtl where po_no='" + po_no + "'");
            //txtotalprice.Text = bll.vLookUp("select sum(subtotal) from tpo_dtl where po_no='" + po_no + "'");
            
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_po");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void cbtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            lbaddress.Text = "";
            List<cArrayList> arr = new List<cArrayList>();
            if (cbtype.SelectedValue.ToString().Trim() == "DP")
            {
                cbmaster.Visible = true;
                arr.Clear();
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@level_no", "1"));
                bll.vBindingComboToSp(ref cbmaster, "sp_tmst_warehouse_get", "whs_cd", "whs_nm", arr);
                lbaddress.Text = "";
                lbcity.Text = "";
                txsearch.Visible = false;
                btsearchpo.Visible = false;
                vNotSpecial.Visible = true;
                //vSpecial.Visible = true;
                //vSpecial.Attributes.Remove("style");
                //vNotSpecial.Attributes.Add("style", "display:none");
            }
            else if (cbtype.SelectedValue.ToString().Trim() == "SD")
            {
                arr.Clear();
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@level_no", "2"));
                bll.vBindingComboToSp(ref cbmaster, "sp_tmst_warehouse_get", "whs_cd", "whs_nm", arr);
                lbaddress.Text = "";
                lbcity.Text = "";
                cbmaster.Visible = true;
                txsearch.Visible = false;
                btsearchpo.Visible = false;
                cbmaster_SelectedIndexChanged(sender, e);
                vNotSpecial.Visible = true;
                //vSpecial.Visible = true;
                //vSpecial.Attributes.Remove("style");
                //vNotSpecial.Attributes.Add("style", "display:none");
            }
            else if (cbtype.SelectedValue.ToString().Trim() == "CU")
            {
                arr.Clear();
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vBindingComboToSp(ref cbmaster, "sp_tmst_customer_get", "cust_cd", "cust_nm");
                cbmaster.Visible = false;
                txsearch.Visible = true;
                btsearchpo.Visible = true;
                cbmaster_SelectedIndexChanged(sender, e);
                vNotSpecial.Visible = true;
                //vSpecial.Visible = true;
                //vSpecial.Attributes.Remove("style");
                //vNotSpecial.Attributes.Add("style", "display:none");
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_po");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void cbmaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (cbtype.SelectedValue.ToString().Trim() == "CU")
            {
                SqlDataReader rs = null;
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@cust_cd", cbmaster.SelectedValue.ToString()));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vGetMstCustomer(arr, ref rs);
                while (rs.Read())
                {
                    lbaddress.Text = rs["addr"].ToString();
                    lbcity.Text = bll.vLookUp("select loc_nm from tmst_location where loc_cd='" + rs["city_cd"].ToString() + "'");
                    lbphone.Text = rs["phone_no"].ToString();

                }
                rs.Close();
            }
            else if (cbtype.SelectedValue.ToString().Trim() == "DP" || cbtype.SelectedValue.ToString().Trim() == "SD")
            {
                lbaddress.Text = bll.vLookUp("select addr from tmst_warehouse where whs_cd='" + cbmaster.SelectedValue.ToString() + "'");

            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_po");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void bttmp2_Click(object sender, EventArgs e)
    {

    }
    protected void cbmaster_TextChanged(object sender, EventArgs e)
    {
        cbmaster_SelectedIndexChanged(sender, e);
    }
    protected void bttmp2_Click1(object sender, EventArgs e)
    {
        cbmaster_SelectedIndexChanged(sender, e);
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        //List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        //bll.vDelTmpPoDtl(arr);
        Response.Redirect("fm_po.aspx");

    }


    //[System.Web.Services.WebMethod()]

    public static string sGetStockAmount()
    {
        string sTemp = "0";
        cbll bll = new cbll();
        sTemp = "100";
        return (sTemp);
    }
    protected void txsearch_TextChanged(object sender, EventArgs e)
    {
        //lbaddress.Text = "Semprul";
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        try
        {

            List<string> lFormula = new List<string>();
            lFormula.Add("{tmst_po.po_no} = '" + txpono.Text + "'");
            lFormula.Add("{tmst_po.salespointcd} = '" + Request.Cookies["sp"].Value.ToString() + "'");
            Session["lformula"] = lFormula;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=po&no=" + txpono.Text + "');", true);
            // Response.Redirect("fm_report.aspx?src=po");

            //List<cArrayList> arr1 = new List<cArrayList>();
            //arr1.Add(new cArrayList("@SalesPointCD", Request.Cookies["sp"].Value.ToString()));
            //arr1.Add(new cArrayList("@trf_no", txpono.Text));
            //arr1.Add(new cArrayList("@sta_id", 'C'));
            //bll.vUpdatetinternal_transfer(arr1);
            //lbstatus.Text = bll.vLookUp("select fld_desc from tfield_value where fld_nm='sta_id' and fld_valu='C'");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_po");
            Response.Redirect("fm_ErrorPage.aspx");
        }

    }
    protected void btsearchpo_Click(object sender, EventArgs e)
    {
        try
        {

            SqlDataReader rs = null;
            string typeCustomer = "";
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vGetMstCustomer(arr, ref rs);
            while (rs.Read())
            {
                lbaddress.Text = rs["addr"].ToString();
                lbcity.Text = bll.vLookUp("select loc_nm from tmst_location where loc_cd='" + rs["city_cd"].ToString() + "'");
                lbphone.Text = rs["phone_no"].ToString();
                typeCustomer = rs["cuscate_cd"].ToString();
            }
            rs.Close();
            //txsearch.Focus();
            //if (typeCustomer == "SP")
            //{
            //    //vNotSpecial.Visible = true;
            //    //vSpecial.Visible = true;
            //    //vSpecial.Attributes.Remove("style");
            //    //vNotSpecial.Attributes.Add("style", "display:none");
            //}
            //else
            //{
            vNotSpecial.Visible = true;
            //    //vSpecial.Visible = true;
            //    //vSpecial.Attributes.Remove("style");
            //    //vNotSpecial.Attributes.Add("style", "display:none");
            //}
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_po");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetItemList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll(); SqlDataReader rs = null;
        string sItem = string.Empty;
        List<string> lItem = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@item_nm", prefixText));
        bll.vSearchMstItem2(arr, ref rs);
        while (rs.Read())
        {
            sItem = AutoCompleteExtender.CreateAutoCompleteItem(rs["item_cd"].ToString() + "-" + rs["item_nm"].ToString() + "'-" + rs["size"].ToString() + "-" + rs["branded_nm"].ToString(), rs["item_cd"].ToString());
            lItem.Add(sItem);
        }
        rs.Close();
        return (lItem.ToArray());
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListCust(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll(); SqlDataReader rs = null;
        HttpCookie cok = HttpContext.Current.Request.Cookies["sp"];
        string sSP = cok.Value.ToString();
        string sCust = string.Empty;
        List<string> lCust = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cust_cd", prefixText));
        arr.Add(new cArrayList("@salespointcd", sSP));
        bll.vSearchMstCustomer(arr, ref rs);
        while (rs.Read())
        {
            sCust = AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"] + " - " + rs["cust_nm"].ToString(), rs["cust_cd"].ToString());
            lCust.Add(sCust);
        }
        rs.Close();
        return (lCust.ToArray());
    }
    protected void txitemname_TextChanged(object sender, EventArgs e)
    {
        //txstock.Text = bll.vLookUp("select isnull(sum(stock_display),0) from tmst_stock where whs_cd in (select qry_data from tmap_query where qry_cd='whs_do') and bin_cd in (select qry_data from tmap_query where qry_cd='bin_do') and item_cd='" + hditem.Value + "'");
        //txstock.Text = bll.vLookUp("select isnull(sum(stkbalance),0) from tblstock where whs_cd in (select qry_data from tmap_query where qry_cd='whs_do') and bin_cd in (select qry_data from tmap_query where qry_cd='bin_do')  and monthcd= dbo.fnFormatDate ([dbo].[fn_getsystemdate](), 'YYYYMM') and item_cd='" + hditem.Value + "'"); //by yanto 10-7-2016
        //txmax_stock.Text = bll.vLookUp("select isnull(sum(max_stock),0) from tmst_stock where whs_cd in (select qry_data from tmap_query where qry_cd='whs_do') and bin_cd in (select qry_data from tmap_query where qry_cd='bin_do') and item_cd='" + hditem.Value + "'");
        //txitemname.Focus();
        // txqty.Focus();
    }

    protected void grdpo_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {

            string po_no;
            if (txpono.Text == "" || txpono.Text == "NEW") { po_no = Request.Cookies["usr_id"].Value.ToString(); } else { po_no = txpono.Text; }
            if (lbstatus.Text == "Completed")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction has been complete, data can not save','error');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
                grdpo.EditIndex = -1;
                return;
            }
            if (Request.Cookies["waz_dt"].Value.ToString() != dtpo.Text)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction Date Out Of Periode Date','error');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
                grdpo.EditIndex = -1;
                return;
            }
            grdpo.EditIndex = e.NewEditIndex;
            //List<cArrayList> arr = new List<cArrayList>();
            //arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            //bll.vBindingGridToSp(ref grdpo, "sp_twrk_podtl_get", arr);
            bindinggrd();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "editTable", "editTable();", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_po");
            Response.Redirect("fm_ErrorPage.aspx");
        }

    }

    protected void grdpo_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {

            string po_no;
            if (txpono.Text == "" || txpono.Text == "NEW") { po_no = Request.Cookies["usr_id"].Value.ToString(); } else { po_no = txpono.Text; }
            List<cArrayList> arr = new List<cArrayList>();
            Label lbitemcode = (Label)grdpo.Rows[e.RowIndex].FindControl("lbitemcode");
            TextBox txqty = (TextBox)grdpo.Rows[e.RowIndex].FindControl("txqty");
            arr.Add(new cArrayList("@po_no", po_no));
            arr.Add(new cArrayList("@qty", txqty.Text));
            arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vupdatetpo_dtl(arr); arr.Clear();
            grdpo.EditIndex = -1;
            //arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            //bll.vBindingGridToSp(ref grdpo, "sp_twrk_podtl_get", arr);
            bindinggrd();
            //txtotqty.Text = bll.vLookUp("select sum(qty) from tpo_dtl where po_no='" + po_no + "'");
            //txtotalprice.Text = bll.vLookUp("select sum(subtotal) from tpo_dtl where po_no='" + po_no + "'");
            //txtotqty.Text = bll.vLookUp("select sum(qty) from twrk_podtl where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "'").ToString(System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
            //txtotalprice.Text = bll.vLookUp("select sum(qty * price_sell) from twrk_podtl a join tmst_item b on a.item_cd=b.item_cd  where a.usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "'").ToString(System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_po");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grdpo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        //bll.vBindingGridToSp(ref grdpo, "sp_twrk_podtl_get", arr);
        bindinggrd();
        grdpo.EditIndex = -1;
    }


    protected void btedit_Click1(object sender, EventArgs e)
    {
        if (Request.QueryString["po"] != null)
        {

            //cbmaster.Enabled = true;
            //cbtype.Enabled = true;
            //cbvendor.Enabled = true;
            //txqty.Enabled = true;
            //txremark.Enabled = true;
            txsearch.Enabled = true;
            //txtotalprice.Enabled = false;
            //txtotqty.Enabled = true;
            btsave.Visible = true;
            btnew.Visible = false;
            btprint.Visible = true;
            grdpo.Columns[9].Visible = true;
            // txitemname.Enabled = true;
            // dtpo.Enabled = true;
            //    dtpo_delivery_dt.Enabled = true;
            //  txpono.Enabled = false;
            //  txstock.Enabled = false;
            //  btadd.Enabled = true;
            // btnew.Enabled = false;
            // btsearchpo.Enabled = true;
        }
    }

    protected void btstock_Click(object sender, EventArgs e)
    {
        try
        {

            //string typeCustomer = bll.vLookUp("select cuscate_cd from tmst_customer where cust_cd='" + hdcust.Value.ToString() + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
            //if (typeCustomer == "SP")
            //{
            //txstock1.Text = bll.vLookUp("select round(dbo.sfnGetStockBookingAll('"+ Request.Cookies["sp"].Value.ToString() + "','" + hditem1.Value + "','" + cbmaster.SelectedValue.ToString() + "'),0)");
            //hdstock1.Value = bll.vLookUp("select round(dbo.sfnGetStockBookingAll('"+ Request.Cookies["sp"].Value.ToString() + "','" + hditem1.Value + "','" + cbmaster.SelectedValue.ToString() + "'),0)");
            //txqty1.Focus();
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "set", "$('#txqty1').val('" + bll.vLookUp("select round(dbo.sfnGetStockBookingAll('"+ Request.Cookies["sp"].Value.ToString() + "','" + hditem1.Value + "','" + cbmaster.SelectedValue.ToString() + "'),0)") + "')", true);
            //}
            //else
            //{
            txstock.Text = bll.vLookUp("select round(dbo.sfnGetStockBookingAll('" + Request.Cookies["sp"].Value.ToString() + "','" + hditem.Value + "','" + cbmaster.SelectedValue.ToString() + "'),0)");
            hdstock.Value = bll.vLookUp("select round(dbo.sfnGetStockBookingAll('" + Request.Cookies["sp"].Value.ToString() + "','" + hditem.Value + "','" + cbmaster.SelectedValue.ToString() + "'),0)");
            //txqty.Focus();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "set", "$('#txqty').val('" + bll.vLookUp("select round(dbo.sfnGetStockBookingAll('" + Request.Cookies["sp"].Value.ToString() + "','" + hditem.Value + "','" + cbmaster.SelectedValue.ToString() + "'),0)") + "')", true);
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction has been complete, data can not save','error');", true);
            //    return;
            //}
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_po");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void cbuom_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btadd1_Click(object sender, EventArgs e)
    {
        //double dOut = 0; double dqty = 0;
        //string po_no;
        //txstock1.Text = "0";
        //if (lbstatus.Text == "Completed")
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction has been complete, data can not save','error');", true);
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
        //    return;
        //}
        //txitemnamePnl1.CssClass = "";
        //if (hditem1.Value.ToString() == "" || hditem1.Value.Equals(null))
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Item Not selected !','Please select item','warning');", true);
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
        //    txitemnamePnl1.CssClass = "error";
        //    return;
        //}
        ////txqtyPnl1.CssClass = "";
        //if (!double.TryParse(txqty1.Text, out dqty))
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('There no item order quantity','Check your qty order','warning');", true);
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
        //    //txqtyPnl1.CssClass = "error";
        //    return;
        //}

        //string priceItem = "";
        //priceItem = bll.vLookUp("select unitprice from tcustomertype_price where cust_typ in (select qry_data from tmap_query where qry_cd='price_po') and item_cd='" + hditem1.Value.ToString() + "'");
        //if (priceItem == "")
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Unitprice not available.','Contact Head Office!!!','warning');", true);
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
        //    return;
        //}

        //if (txpono.Text == "" || txpono.Text == "NEW") { po_no = Request.Cookies["usr_id"].Value.ToString(); } else { po_no = txpono.Text; }
        //List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@item_cd", hditem1.Value.ToString()));
        //arr.Add(new cArrayList("@po_no", po_no));
        //arr.Add(new cArrayList("@qty", txqty1.Text));
        //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        //arr.Add(new cArrayList("@stock", txstock1.Text));
        //arr.Add(new cArrayList("@whs_cd", cbmaster.SelectedValue.ToString()));
        //arr.Add(new cArrayList("@uom", cbuom.SelectedValue.ToString()));
        //bll.vInserttpo_dtl_ins3(arr);
        ////if (hdfBranchConnected.Value == "true")
        ////{
        ////    
        ////    //bllHO.vInserttpo_dtl_ins3(arr);
        ////}
        ////else {
        ////    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('HO not connected !','HO not connected','warning');", true);
        ////}
        //arr.Clear();
        //bindinggrd();
        //txitemname1.Text = "";
        //txqty1.Text = "";
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "set", "$('#txqty1').val('0');", true);
        //txitemname1.Focus();
        //CheckHOStatus();
    }

    protected void grdpo1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //string po_no;
        //if (txpono.Text == "" || txpono.Text == "NEW") { po_no = Request.Cookies["usr_id"].Value.ToString(); } else { po_no = txpono.Text; }
        //if (lbstatus.Text == "Completed")
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction has been complete, data can not save','error');", true);
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
        //    return;
        //}
        //if (Request.Cookies["waz_dt"].Value.ToString() != dtpo.Text)
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction Date Out Of Periode Date','error');", true);
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
        //    return;
        //}
        //Label lbitemcode = (Label)grdpo1.Rows[e.RowIndex].FindControl("lbitemcode");
        //List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
        //arr.Add(new cArrayList("@po_no", po_no));
        //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString())); 
        //bll.vDelPoDtl(arr);
        ////arr.Clear();
        ////arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        ////bll.vBindingGridToSp(ref grdpo, "sp_twrk_podtl_get", arr);
        //bindinggrd();
        //txtotqty.Text = bll.vLookUp("select sum(qty) from twrk_podtl where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "'");
    }

    protected void grdpo1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //string po_no;
        //if (txpono.Text == "" || txpono.Text == "NEW") { po_no = Request.Cookies["usr_id"].Value.ToString(); } else { po_no = txpono.Text; }
        //if (lbstatus.Text == "Completed")
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction has been complete, data can not save','error');", true);
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
        //    grdpo.EditIndex = -1;
        //    return;
        //}
        //if (Request.Cookies["waz_dt"].Value.ToString() != dtpo.Text)
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction Date Out Of Periode Date','error');", true);
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
        //    grdpo.EditIndex = -1;
        //    return;
        //}
        //grdpo1.EditIndex = e.NewEditIndex;
        //bindinggrd();
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "editTable", "editTable();", true);
    }

    protected void grdpo1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //string po_no;
        //if (txpono.Text == "" || txpono.Text == "NEW") { po_no = Request.Cookies["usr_id"].Value.ToString(); } else { po_no = txpono.Text; }
        //List<cArrayList> arr = new List<cArrayList>();
        //Label lbitemcode = (Label)grdpo1.Rows[e.RowIndex].FindControl("lbitemcode");
        //TextBox txqty = (TextBox)grdpo1.Rows[e.RowIndex].FindControl("txqty");
        //arr.Add(new cArrayList("@po_no", po_no));
        //arr.Add(new cArrayList("@qty", txqty.Text));
        //arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
        //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        //bll.vupdatetpo_dtl(arr); arr.Clear();
        //grdpo1.EditIndex = -1;
        //bindinggrd();
    }

    protected void grdpo_RowCreated(object sender, GridViewRowEventArgs e)
    {
        // Adding a column manually once the header created
        if (e.Row.RowType == DataControlRowType.Header) // If header created
        {
            GridView POGrid = (GridView)sender;

            // Creating a Row
            GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

            // Merging Column
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "No.";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.RowSpan = 2; // For merging first, second row cells to one
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.Text = "Code";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.RowSpan = 2;
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.Text = "Item Name";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.RowSpan = 2;
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Size";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.RowSpan = 2;
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.Text = "Branded";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.RowSpan = 2;
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.Text = "Old Qty";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.RowSpan = 2;
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);

            //Adding Revenue Column
            HeaderCell = new TableCell();
            HeaderCell.Text = "Stock";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.ColumnSpan = 3; // For merging three columns 
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);

            HeaderRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.Text = "Unit Price";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.RowSpan = 2;
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);
            HeaderRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.Text = "Sub Total";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.RowSpan = 2;
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);


            //Adding the Row at the 0th position (first row) in the Grid
            POGrid.Controls[0].Controls.AddAt(0, HeaderRow);
        }
    }

    protected void grdpo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[10].Visible = false;
        }
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    double dblDirectRevenue = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "DirectRevenue").ToString());
        //    double dblReferralRevenue = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ReferralRevenue").ToString());
        //    Label lblTotalRevenue = ((Label)e.Row.FindControl("lblTotalRevenue"));
        //    lblTotalRevenue.Text = string.Format("{0:0.00}", (dblDirectRevenue + dblReferralRevenue));
        //}
    }


    protected void cbsalespoint_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}