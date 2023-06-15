using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_mstcustomerentry : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e) 
    {

        if (!IsPostBack)
        {
            try
            {

                //   txcustcode.Text= bll.vLookUp("select max(cust_code)+1 from tmst_customer where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "')
                List<cArrayList> arr = new List<cArrayList>();
                arr.Clear();
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_getuser", "salespointcd", "salespoint_desc", arr);
                if (Request.Cookies["sp"].Value.ToString() != "0") { cbsalespoint.SelectedValue = Request.Cookies["sp"].Value.ToString();  cbsalespoint.Enabled = false; }
                cbsalespoint_SelectedIndexChanged(sender, e);
                //bll.vBindingComboToSp(ref cbcategory, "sp_tmst_customercategory_get");
                bll.vBindingComboToSp(ref cbcategory, "sp_tmst_customercategory_get", "custcate_cd", "custcate_nm");
                arr.Clear();
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                bll.vDelWrkCustomerContact(arr);
                bll.vDelWrkCustDoc(arr);
                arr.Clear();
                
                //  bll.vBindingFieldValueToCombo(ref cbclass, "class_cd");
                //arr.Clear();
                //arr.Add(new cArrayList("@job_title_cd", "SAL")); // Salesman Code
                //bll.vBindingComboToSp(ref cbsalesman, "sp_tmst_employee_get", "emp_cd", "emp_desc", arr);
                //arr.Clear();
                //arr.Add(new cArrayList("@pos_id", "MER")); // Salesman Code
                //bll.vBindingComboToSp(ref cbmerchandiser, "sp_tmst_employee_get", "emp_cd", "emp_desc", arr);
                arr.Clear();
                arr.Add(new cArrayList("@fld_nm", "otlcd"));
                arr.Add(new cArrayList("@hiddendata", "0"));
                bll.vBindingComboToSp(ref cbotlcd, "sp_tfield_value_withselect_get", "fld_valu", "FLD_DESC", arr);
                //bll.vBindingFieldValueToCombo(ref cbotlcd, "otlcd");
                arr.Clear();
                arr.Add(new cArrayList("@fld_nm", "cusgrcd"));
                bll.vBindingComboToSp(ref cbcusgroup, "sp_tfield_value_withselect_get", "fld_valu", "FLD_DESC", arr);
                arr.Clear();
                arr.Add(new cArrayList("@fld_nm", "payment_term"));
                bll.vBindingComboToSp(ref cbtermpayment, "sp_tfield_value_withselect_get", "fld_valu", "FLD_DESC", arr);
                //bll.vBindingFieldValueToCombo(ref cbtermpayment, "payment_term");
                arr.Clear();
                arr.Add(new cArrayList("@fld_nm", "day_cd"));
                bll.vBindingComboToSp(ref cbrps, "sp_tfield_value_withselect_get", "fld_valu", "FLD_DESC", arr);

                //bll.vBindingFieldValueToCombo(ref cbrps, "day_cd");
                lbsp.Text = bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                arr.Clear();
                arr.Add(new cArrayList("@level_no", 1));
                //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vBindingComboToSp(ref cblocation, "sp_tmst_location_get", "loc_cd", "loc_nm", arr);
                cblocation_SelectedIndexChanged(sender, e);
                cbcategory_SelectedIndexChanged(sender, e);
                btsave.Visible = true; btupdate.Visible = false;
                if (Request.QueryString["cust"] != null)
                {
                    btnew.Visible = true;
                    vDisableControl();
                    txaddress.Enabled = true;
                    txaddress.CssClass = "makeitreadwrite form-control";
                    txarabic.Enabled = true;
                    txarabic.CssClass = "makeitreadwrite form-control";
                    txcl.Enabled = true;
                    txcl.CssClass = "makeitreadwrite form-control";
                    txlatitude.Enabled = true;
                    txlatitude.CssClass = "makeitreadwrite form-control";
                    txlongitude.Enabled = true;
                    txlongitude.CssClass = "makeitreadwrite form-control";
                    txmaxinvoice.Enabled = false;
                    txmaxinvoice.CssClass = "makeitreadwrite form-control";
                    txshortname.Enabled = true;
                    txshortname.CssClass = "makeitreadwrite form-control";
                    cbcategory.Enabled = true;
                    cbcategory.CssClass = "makeitreadwrite form-control";
                    cbcusgroup.Enabled = true;
                    cbcusgroup.CssClass = "makeitreadwrite form-control";
                    cblocation.Enabled = true;
                    cblocation.CssClass = "makeitreadwrite form-control";
                    cbmerchandiser.Enabled = true;
                    cbmerchandiser.CssClass = "makeitreadwrite form-control";
                    cbsalesman.Enabled = true;
                    btsave.Enabled = false;
                    cbsalesman.CssClass = "makeitreadwrite form-control";
                    cbotlcd.Enabled = true;
                    cbotlcd.CssClass = "makeitreadwrite form-control";
                    cbtermpayment.Enabled = true;
                    cbtermpayment.CssClass = "makeitreadwrite form-control";
                    System.Data.SqlClient.SqlDataReader rs = null;
                    arr.Clear();
                    arr.Add(new cArrayList("@cust_cd", Request.QueryString["cust"].ToString()));
                    bll.vGetMstCustomer(arr, ref rs);
                    while (rs.Read())
                    {
                        cbsalespoint.SelectedValue = rs["salespointcd"].ToString();
                        cbsalespoint_SelectedIndexChanged(sender, e);
                        cbsalespoint.Enabled = false;
                        txcustcode.Enabled = false;
                        cbcategory.SelectedValue = rs["cusgrcd"].ToString();
                        txcustcode.Text = rs["cust_cd"].ToString();
                        txcusname.Text = rs["cust_nm"].ToString();
                        txcl.Text = rs["credit_limit"].ToString();
                        txshortname.Text = rs["cust_sn"].ToString();
                        txarabic.Text = rs["cust_arabic"].ToString();
                        cbotlcd.SelectedValue = rs["otlcd"].ToString();
                        cbcusgroup.SelectedValue = rs["cusgrcd"].ToString();

                        txaddress.Text = rs["address1"].ToString();
                        txaddress2.Text = rs["address2"].ToString();
                        if (rs["city"].ToString() != "") { cblocation.SelectedValue = rs["city"].ToString(); }
                        if (rs["state"].ToString() != "") { cbdistrict.SelectedValue = rs["state"].ToString(); }
                        txzipcode.Text = rs["zipcode"].ToString();

                        txphoneno.Text = rs["phone_no"].ToString();
                        txfaxno.Text = rs["fax_no"].ToString();
                        cbtermpayment.SelectedValue = rs["payment_term"].ToString();
                        txlatitude.Text = rs["latitude"].ToString();
                        txlongitude.Text = rs["longitude"].ToString();
                        cbcategory.SelectedValue = rs["cuscate_cd"].ToString();
                        txcl.Text = rs["credit_limit"].ToString();
                        txmaxinvoice.Text = rs["max_invoice"].ToString();
                        cbsalesman.SelectedValue = rs["salesman_cd"].ToString();
                        if (rs["merchandizer_cd"].ToString() == null || rs["merchandizer_cd"].ToString() == "") { chmerchand.Checked = true; chmerchand_CheckedChanged(sender, e); }
                        else { chmerchand_CheckedChanged(sender, e); cbmerchandiser.SelectedValue = rs["merchandizer_cd"].ToString(); }
                        cbsalesman.Enabled = false;
                        txcontactname.Text = rs["contact1"].ToString();
                        txidentity.Text = rs["idno1"].ToString();
                        txmobile.Text = rs["mobileno1"].ToString();
                        txcontactmail.Text = rs["email"].ToString();
                        txcontactname2.Text = rs["contact2"].ToString();
                        txidentity2.Text = rs["idno2"].ToString();
                        txmobile2.Text = rs["mobileno2"].ToString();
                        txcontactmail2.Text = rs["email2"].ToString();
                        txbankacc.Text = rs["bankacc_no"].ToString();
                        txvat.Text = rs["tax_no"].ToString();
                        txcrno.Text = rs["cr_no"].ToString();
                        txvatname.Text = rs["vat_custname"].ToString();
                        txvatarabic.Text = rs["vat_custarabic"].ToString();
                        
                        arr.Clear();
                        arr.Add(new cArrayList("custcate_cd", cbcategory.SelectedValue.ToString()));
                        bll.vBindingGridToSp(ref grdcate, "sp_tcustomercategory_doc_get", arr);
                        arr.Clear();
                        arr.Add(new cArrayList("@cust_cd", txcustcode.Text));
                        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                        bll.vBindingGridToSp(ref grddoc, "sp_tcustomer_document_get", arr);
                    }
                    
                    rs.Close();
                    rs = null;
                    arr.Clear();
                    arr.Add(new cArrayList("custcate_cd", cbcategory.SelectedValue.ToString()));
                    string bkacc = bll.vLookUp("select isnull(bankacc_no,0) from tcustomer_info where cust_cd='" + txcustcode.Text + "'");
                    if (bkacc == "0")
                    {
                        txbankacc.Text = "";
                    }
                    else
                    {
                        txbankacc.Text = bkacc;
                    }
                    lbcate.Text = bll.vLookUp("select custcate_desc from tmst_customercategory where custcate_cd='" + cbcategory.SelectedValue.ToString() + "'");
                    bll.vGetMstCustomerCategory(arr, ref rs);
                    while (rs.Read())
                    {
                        lblmincredit.Text = rs["min_creditlimit"].ToString();
                        lblmaxcredit.Text = rs["max_creditlimit"].ToString();
                    }
                    rs.Close(); btsave.Visible = false; btupdate.Visible = true;
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstcustomerentry");
                Response.Redirect("fm_ErrorPage.aspx");
            }
        }
    }

    void vDisableControl()
    {
        txaddress.Enabled = false;
        txaddress.CssClass = "makeitreadonly form-control";
        txarabic.Enabled = false;
        txarabic.CssClass = "makeitreadonly form-control";
        txcl.Enabled = false;
        txcl.CssClass = "makeitreadonly form-control";
        txcusname.Enabled = false;
        txcusname.CssClass = "makeitreadonly form-control";
        txlatitude.Enabled = false;
        txlatitude.CssClass = "makeitreadonly form-control";
        txlongitude.Enabled = false;
        txlongitude.CssClass = "makeitreadonly form-control";
        txmaxinvoice.Enabled = false;
        txmaxinvoice.CssClass = "makeitreadonly form-control";
        txshortname.Enabled = false;
        txshortname.CssClass = "makeitreadonly form-control";
        cbcategory.Enabled = false;
        cbcategory.CssClass = "makeitreadonly form-control";
        cbcusgroup.Enabled = false;
        cbcusgroup.CssClass = "makeitreadonly form-control";
        cblocation.Enabled = false;
        cblocation.CssClass = "makeitreadonly form-control";
        cbmerchandiser.Enabled = false;
        cbmerchandiser.CssClass = "makeitreadonly form-control";
        cbsalesman.Enabled = false;
        cbsalesman.CssClass = "makeitreadonly form-control";
        cbotlcd.Enabled = false;
        cbotlcd.CssClass = "makeitreadonly form-control";
        cbtermpayment.Enabled = false;
        cbtermpayment.CssClass = "makeitreadonly form-control";

    }
    protected void cbcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            System.Data.SqlClient.SqlDataReader rs = null;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("custcate_cd", cbcategory.SelectedValue.ToString()));
            if (cbcategory.SelectedValue.ToString() == "CASH")
            {
                bll.vBindingComboToSp(ref cbtermpayment, "sp_paymenttermcashcustomer", "fld_valu", "fld_desc");
            }
            else
            {
                bll.vBindingFieldValueToCombo(ref cbtermpayment, "payment_term");
            }
            bll.vBindingGridToSp(ref grdcate, "sp_tcustomercategory_doc_get", arr);
            lbcate.Text = bll.vLookUp("select custcate_desc from tmst_customercategory where custcate_cd='" + cbcategory.SelectedValue.ToString() + "'");
            bll.vGetMstCustomerCategory(arr, ref rs);
            while (rs.Read())
            {
                lblmincredit.Text = rs["min_creditlimit"].ToString();
                lblmaxcredit.Text = rs["max_creditlimit"].ToString();
            }
            rs.Close();

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstcustomerentry");
            Response.Redirect("fm_ErrorPage.aspx");
        }

    }
    protected void grdcate_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            CheckBox chkcompleted = (CheckBox)grdcate.Rows[e.NewSelectedIndex].FindControl("chkcompleted");

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dd", "alert('okeee')", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstcustomerentry");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grdcate_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {

            List<cArrayList> arr = new List<cArrayList>();
            foreach (GridViewRow row in grdcate.Rows)
            {
                FileUpload upl = (FileUpload)row.FindControl("upl");
                if (upl.HasFile)
                {
                    FileInfo fi = new FileInfo(upl.FileName);
                    string ext = fi.Extension;
                    byte[] fs = upl.FileBytes;
                    if (fs.Length > 1000000)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Picture Size must be less than 1 MB','Images Size','warning');", true);
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Images Must be uploaded','Images not uploaded','warning');", true);
                    return;
                }
            }
            foreach (GridViewRow row in grdcate.Rows)
            {
                Label lbdoccode = (Label)row.FindControl("lbdoccode");
                FileUpload upl = (FileUpload)row.FindControl("upl");
                if (upl.HasFile)
                {
                    arr.Clear();
                    arr.Add(new cArrayList("@doc_cd", lbdoccode.Text));
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    arr.Add(new cArrayList("@filename", Server.MapPath(upl.FileName)));
                    bll.vInsertWrkCustDoc(arr);
                }
            }
            txcusnamePnl.CssClass = "";
            if (cbcusgroup.SelectedIndex.ToString() == "0")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Please select customer group ','Check group','warning');", true);
                cbcusgroupPnl.CssClass = "error";
                return;
            }
            if (cbotlcd.SelectedIndex.ToString() == "0")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Please select customer channel ','Check channel','warning');", true);
                cbotlcdPnl.CssClass = "error";
                return;
            }
            if (cbrps.SelectedIndex.ToString() == "0")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Please select RPS DAY ','Check RPS DAY','warning');", true);
                cbotlcdPnl.CssClass = "error";
                return;
            }
            if (txvat.Text != "")
            {
                if (txvatarabic.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('VAT customer name Arabic must be filled ','Check Name','warning');", true);
                    txvatarabicPnl.CssClass = "error";
                    return;
                }
            }

            if (txcusname.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Customer name must be filled ','Check Name','warning');", true);
                txcusnamePnl.CssClass = "error";
                return;
            }
            txarabicPnl.CssClass = "";
            if (txarabic.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Customer Arabic name must be filled','Check Arabic','warning');", true);
                txarabicPnl.CssClass = "error";
                return;
            }
            txshortnamePnl.CssClass = "";
            if (txshortname.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Shortname must be filled','Check Shortname','warning');", true);
                txshortnamePnl.CssClass = "error";
                return;
            }
            double dCL = 0;
            txclPnl.CssClass = "";
            if (!double.TryParse(txcl.Text, out dCL))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Credit limit must numeric','Check Credit Limit','warning');", true);
                txclPnl.CssClass = "error";
                return;
            }
            if (Convert.ToDouble(txcl.Text) < Convert.ToDouble(lblmincredit.Text))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Credit limit Problem','Must not less than " + lblmincredit.Text + "','warning');", true);
                txclPnl.CssClass = "error";
                return;
            }
            if (Convert.ToDouble(txcl.Text) > Convert.ToDouble(lblmaxcredit.Text))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Credit limit Problem','Must not more than " + lblmaxcredit.Text + "','warning');", true);
                txclPnl.CssClass = "error";
                return;
            }

            if (cbsalesman.SelectedValue == string.Empty)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Salesman not yet selected!','Select salesman','warning');", true);
                return;
            }

            btsave.Enabled = false;
            btsave.Visible = false;
            //  foreach (GridViewRow row in grdcate.Rows)
            //  {
            //      FileUpload upl = (FileUpload)row.FindControl("upl");
            //      if (!upl.HasFile)
            //      {
            //          ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alr", "sweetAlert('Please upload document needed','Please make all doc completed','warning');", true);
            //          return;
            //      }
            //  }
            //string sMobileNo = "";
            //  csendsms csms = new csendsms();
            //List<cArrayList> arr = new List<cArrayList>();
            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
            // arr.Add(new cArrayList("@cust_cd", txcuscode.Text));
            arr.Add(new cArrayList("@cust_nm", txcusname.Text));
            arr.Add(new cArrayList("@cust_sn", txshortname.Text));
            arr.Add(new cArrayList("@cust_arabic", txarabic.Text));
            arr.Add(new cArrayList("@otlcd", cbotlcd.SelectedValue.ToString()));
            arr.Add(new cArrayList("@custgrcd", cbcusgroup.SelectedValue.ToString()));
            arr.Add(new cArrayList("@addr", txaddress.Text));
            arr.Add(new cArrayList("@city_cd", cblocation.SelectedValue.ToString()));
            arr.Add(new cArrayList("@phone_no", txphoneno.Text));
            arr.Add(new cArrayList("@fax_no", txfaxno.Text));
            arr.Add(new cArrayList("@payment_term", cbtermpayment.SelectedValue.ToString()));
            arr.Add(new cArrayList("@latitude", txlatitude.Text));
            arr.Add(new cArrayList("@longitude", txlongitude.Text));
            arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@cuscate_cd", cbcategory.SelectedValue.ToString()));
            arr.Add(new cArrayList("@credit_limit", txcl.Text));
            arr.Add(new cArrayList("@max_invoice", txmaxinvoice.Text));
            arr.Add(new cArrayList("@salesmancd", cbsalesman.SelectedValue.ToString()));
            arr.Add(new cArrayList("@merchandizer_cd", cbmerchandiser.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salesblock", chsales.Checked));
            arr.Add(new cArrayList("@cust_sta_id", "A")); //D is Draft Request
            arr.Add(new cArrayList("@createdt", DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@contact1", txcontactname.Text));
            arr.Add(new cArrayList("@idno1", txidentity.Text));
            arr.Add(new cArrayList("@mobile1", txmobile.Text));
            arr.Add(new cArrayList("@email", txcontactmail.Text));
            arr.Add(new cArrayList("@contact2", txcontactname2.Text));
            arr.Add(new cArrayList("@idno2", txidentity2.Text));
            arr.Add(new cArrayList("@mobile2", txmobile2.Text));
            arr.Add(new cArrayList("@email2", txcontactmail2.Text));
            arr.Add(new cArrayList("@vat_no", txvat.Text));
            arr.Add(new cArrayList("@cr_no", txcrno.Text));
            arr.Add(new cArrayList("@vat_custname", txvatname.Text));
            arr.Add(new cArrayList("@vat_custarabic", txvatarabic.Text));
            //     arr.Add(new cArrayList("@dt_exp1", DateTime.ParseExact(dtexp1.Text,"d/M/yyyy",System.Globalization.CultureInfo.InvariantCulture )));
            //     arr.Add(new cArrayList("@dt_exp2", DateTime.ParseExact (dtexp2.Text,"d/M/yyyy",System.Globalization.CultureInfo.InvariantCulture)));
            string sCustNo = "";
            //try
            //{
            bll.vinsertMstCustomer(arr, ref sCustNo);
            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
            arr.Add(new cArrayList("@cust_cd", sCustNo.ToString()));
            arr.Add(new cArrayList("@address1", txaddress.Text.ToString()));
            arr.Add(new cArrayList("@address2", txaddress2.Text.ToString()));
            arr.Add(new cArrayList("@city", cblocation.SelectedValue.ToString()));
            arr.Add(new cArrayList("@state", cbdistrict.SelectedValue.ToString()));
            arr.Add(new cArrayList("@zipcode", txzipcode.Text.ToString()));



            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Success','Customer: " + sCustNo + " Has been Created', 'success');", true);
                //Response.Write("<script language='javascript'>window.location='fm_mstcustomerentry.aspx?cust=" + sCustNo + "';</script>");
            //}
            //catch (Exception ex)
            //{
            //    bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : Save Mst Customer");
            //}
            txcustcode.Text = sCustNo;
            //insert document
            foreach (GridViewRow row in grdcate.Rows)
            {
                Label lbdoccode = (Label)row.FindControl("lbdoccode");
                Label lbdocname = (Label)row.FindControl("lbdocname");
                TextBox dtexp = (TextBox)row.FindControl("dtexp");
                TextBox txserial = (TextBox)row.FindControl("txserialno");
                FileUpload upl = (FileUpload)row.FindControl("upl");

                //if (upl.HasFile)
                //{
                FileInfo fi = new FileInfo(upl.FileName);
                string ext = fi.Extension;
                //    byte[] fs = upl.FileBytes;
                //    if (fs.Length <= 1000000)
                //    {

                arr.Clear();
                arr.Add(new cArrayList("@cust_cd", txcustcode.Text));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@doc_cd", lbdoccode.Text));
                arr.Add(new cArrayList("@doc_nm", lbdocname.Text));
                if (dtexp.Text == "")
                {
                    arr.Add(new cArrayList("@exp_dt", null));
                }
                else
                {
                    DateTime dt = DateTime.ParseExact(dtexp.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    arr.Add(new cArrayList("@exp_dt", dt.Year + "-" + dt.Month + "-" + dt.Day));
                }



                if (txserial.Text != "")
                {
                    arr.Add(new cArrayList("@serial", txserial.Text));
                }
                else
                {
                    arr.Add(new cArrayList("@serial", null));
                }
                if ((upl.FileName != "") || (upl.FileName != null))
                {

                    arr.Add(new cArrayList("@fileloc", Request.Cookies["sp"].Value.ToString() + "-" + txcustcode.Text + lbdoccode.Text + ext));
                    upl.SaveAs(bll.sGetControlParameter("image_path") + "/customer/" + Request.Cookies["sp"].Value.ToString() + "-" + txcustcode.Text + lbdoccode.Text + ext);

                }

                bll.vInsertCustomerDocument(arr);
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Image size too big','Image size must be less than 500KB');", true);

                //    return;
                //}
                //}
            }
            // Random random = new Random();
            // int nRandom = random.Next(1000, 9999);
            // List<string> lApp = bll.lGetApproval("customer", 1);
            // sMobileNo = lApp[0];
            // try
            //{
            //cd.vSendSms("New Cust No. : " + Request.Cookies["sp"].Value.ToString() + sCustNo + " created." +
            //    "CL=" + txcl.Text + ",TERM:" + cbtermpayment.SelectedValue.ToString() + ". Do you want approve ? Reply with (Y/N)" + nRandom.ToString(), sMobileNo);
            //  }
            // catch (Exception ex)
            // {
            //   bll.vHandledError(ref ex,"Send SMS approval customer");

            //}

            //arr.Clear();
            //arr.Add(new cArrayList("@token", nRandom.ToString()));
            //arr.Add(new cArrayList("@doc_typ", "customer"));
            //arr.Add(new cArrayList("@receiver", sMobileNo));
            //arr.Add(new cArrayList("@doc_no", sCustNo));
            //bll.vInsertSMSSent(arr);
            btsave.Enabled = false;
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dd", "alert('Customer Has been Created')", true);
            btprint.Enabled = true;
            // Response.Redirect("fm_mstcustomerlist.aspx");
            int rps = Convert.ToInt32(bll.vLookUp("select isnull(max(sequenceno),0)+1 from trps_dtl where day_cd='" + cbrps.SelectedValue.ToString() + "' and emp_cd='" + cbsalesman.SelectedValue.ToString() + "'"));
            arr.Clear();
            //Send Email New customer

            string sSubject = "New customer has been created in  " + bll.vLookUp("select salespointcd+' - '+salespoint_nm from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            string sBody = "<html><head><body>Customer " + txcustcode.Text + ", " + txcusname.Text + " has been created with type " + cbotlcd.SelectedItem.Text + " <br/>and credit limit " + txcl.Text + " and POT " + cbtermpayment.SelectedValue.ToString() + " <br/> in branch " + bll.vLookUp("select salespointcd+' - '+salespoint_nm from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + "</body></head></html>";
            List<string> lnotify;
            lnotify = bll.lGetApproval("notifyaccbalance", 1);
            arr.Clear();
            arr.Add(new cArrayList("@token", null));
            arr.Add(new cArrayList("@doc_typ", "newcustomer"));
            arr.Add(new cArrayList("@to", lnotify[1]));
            arr.Add(new cArrayList("@doc_no", txcustcode.Text));
            arr.Add(new cArrayList("@emailsubject", sSubject));
            arr.Add(new cArrayList("@msg", sBody));
            arr.Add(new cArrayList("@file_attachment", null));
            bll.vInsertEmailOutbox(arr); //by yanto 23-1-2017
                                         //
            arr.Clear();

            arr.Add(new cArrayList("@sequenceno", rps));
            arr.Add(new cArrayList("@day_cd", cbrps.SelectedValue.ToString()));
            arr.Add(new cArrayList("@emp_cd", cbsalesman.SelectedValue.ToString()));
            arr.Add(new cArrayList("@cust_cd", sCustNo));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@created_dt", System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
            bll.vInsertRpsDtl(arr);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Images Must be uploaded','Images not uploaded','warning');", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstcustomerentry");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grdcate_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {

            grdcate.PageIndex = e.NewPageIndex;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("custcate_cd", cbcategory.SelectedValue.ToString()));
            bll.vBindingGridToSp(ref grdcate, "sp_tcustomercategory_doc_get", arr);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstcustomerentry");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=cust&cust_cd=" + txcustcode.Text + "');", true);
    }

    protected void chmerchand_CheckedChanged(object sender, EventArgs e)
    {
        try
        {

            if (chmerchand.Checked)
            {
                cbmerchandiser.Enabled = false;
                cbmerchandiser.Items.Clear();
            }
            else
            {
                List<cArrayList> arr = new List<cArrayList>();
                cbmerchandiser.Enabled = true;
                arr.Add(new cArrayList("@qry_cd", "merchandcd"));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vBindingComboToSp(ref cbmerchandiser, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstcustomerentry");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btedit_Click(object sender, EventArgs e)
    {
        txaddress.Enabled = true;
        txarabic.Enabled = true;
        txshortname.Enabled = true;
        txshortname.CssClass = "makeitreadwrite form-control";
        txaddress.CssClass = "makeitreadwrite form-control";
        txarabic.CssClass = "makeitreadwrite form-control";
        txcl.CssClass = "makeitreadwrite form-control";
        List<cArrayList> arr = new List<cArrayList>();
        try
        {
            arr.Add(new cArrayList("@cust_cd", txcustcode.Text));
            if (txbankacc.Text != "")
            {
                arr.Add(new cArrayList("@bankacc_no", txbankacc.Text));
            }
            else
            {
                arr.Add(new cArrayList("@bankacc_no", null));
            }
            arr.Add(new cArrayList("@district_cd", cbdistrict.SelectedValue.ToString()));
            //arr.Add(new cArrayList("@cr_no", txcrno.Text));
            //arr.Add(new cArrayList("@tax_no", cbdistrict.SelectedValue.ToString()));
            bll.vInsertCustomerInfo(arr);


            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
            arr.Add(new cArrayList("@cust_cd", txcustcode.Text));
            arr.Add(new cArrayList("@cust_sn", txshortname.Text));
            arr.Add(new cArrayList("@cust_arabic", txarabic.Text));
            arr.Add(new cArrayList("@cusgrcd", cbcusgroup.SelectedValue.ToString()));
            arr.Add(new cArrayList("@addr", txaddress.Text));
            //arr.Add(new cArrayList("@city_cd", cblocation.SelectedValue.ToString()));
            arr.Add(new cArrayList("@phone_no", txphoneno.Text));
            //arr.Add(new cArrayList("@fax_no", txfaxno.Text));
            arr.Add(new cArrayList("@payment_term", cbtermpayment.SelectedValue.ToString()));
            arr.Add(new cArrayList("@otlcd", cbotlcd.SelectedValue.ToString()));
            arr.Add(new cArrayList("@cuscate_cd", cbcategory.SelectedValue.ToString()));
            arr.Add(new cArrayList("@credit_limit", txcl.Text));

            arr.Add(new cArrayList("@cr_no", txcrno.Text));
            arr.Add(new cArrayList("@tax_no", txvat.Text));
            arr.Add(new cArrayList("@vat_custname", txvatname.Text));
            arr.Add(new cArrayList("@vat_custarabic", txvatarabic.Text));

            //arr.Add(new cArrayList("@merchandizer_cd", cbmerchandiser.SelectedValue.ToString()));
            //arr.Add(new cArrayList("@contact1",txcontactname.Text));
            arr.Add(new cArrayList("@idno1", txidentity.Text));
            //arr.Add(new cArrayList("@mobileno1",txmobile.Text));
            //arr.Add(new cArrayList("@email",txcontactmail.Text));
            //arr.Add(new cArrayList("@contact2", txcontactname2.Text));
            arr.Add(new cArrayList("@idno2", txidentity2.Text));
            //arr.Add(new cArrayList("@mobileno2", txmobile2.Text));
            //arr.Add(new cArrayList("@email2", txcontactmail2.Text));

            arr.Add(new cArrayList("@address1", txaddress.Text));
            arr.Add(new cArrayList("@address2", txaddress2.Text));
            arr.Add(new cArrayList("@city", cblocation.SelectedValue.ToString()));
            arr.Add(new cArrayList("@state", cbdistrict.SelectedValue.ToString()));
            arr.Add(new cArrayList("@zipcode", txzipcode.Text));

            bll.vUpdateTmst_Customer(arr);
            foreach (GridViewRow row in grdcate.Rows)
            {
                Label lbdoccode = (Label)row.FindControl("lbdoccode");
                Label lbdocname = (Label)row.FindControl("lbdocname");
                TextBox dtexp = (TextBox)row.FindControl("dtexp");
                FileUpload upl = (FileUpload)row.FindControl("upl");
                TextBox txserial = (TextBox)row.FindControl("txserialno");
                if (upl.HasFile)
                {
                    FileInfo fi = new FileInfo(upl.FileName);
                    string ext = fi.Extension;
                    byte[] fs = upl.FileBytes;
                    if (fs.Length <= 500000)
                    {
                        if (ext == ".jpg" || ext == ".jpeg" || ext == ".bmp" || ext == ".gif" || ext == ".png" || ext == ".JPEG" || ext == ".JPG" || ext == ".BMP" || ext == ".GIF" || ext == ".PNG")
                        {
                            arr.Clear();
                            arr.Add(new cArrayList("@cust_cd", txcustcode.Text));
                            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                            arr.Add(new cArrayList("@doc_cd", lbdoccode.Text));
                            arr.Add(new cArrayList("@doc_nm", lbdocname.Text));
                            if (txserial.Text != "")
                            {
                                arr.Add(new cArrayList("@serial", txserial.Text));
                            }
                            else
                            {
                                arr.Add(new cArrayList("@serial", null));
                            }
                            if (dtexp.Text != "")
                            {
                                DateTime dt = DateTime.ParseExact(dtexp.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                                arr.Add(new cArrayList("@exp_dt", dt.Year + "-" + dt.Month + "-" + dt.Day));
                            }
                            else
                            {
                                arr.Add(new cArrayList("@exp_dt", null));
                            }

                            if ((upl.FileName != "") || (upl.FileName != null))
                            {

                                arr.Add(new cArrayList("@fileloc", Request.Cookies["sp"].Value.ToString() + "-" + txcustcode.Text + lbdoccode.Text + ext));
                                upl.SaveAs(bll.sGetControlParameter("image_path") + "/customer/" + Request.Cookies["sp"].Value.ToString() + "-" + txcustcode.Text + lbdoccode.Text + ext);

                            }
                            bll.vInsertCustomerDocument(arr);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('You should upload image','jpg,bmp,gif and png upload document again');", true);
                            return;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Image size too big','Image size must be less than 500KB');", true);
                        return;
                    }

                }
                //else
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('File should be uploaded','');", true);
                //    return;
                //}
            }


            //Response.Write("<script language='javascript'>window.alert('Customer: " + Request.QueryString["cust"] + " Has been Updated');window.location='fm_mstcustomerentry.aspx?cust=" + Request.QueryString["cust"] + "';</script>");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Success','Customer: " + txcustcode.Text + " Has been Updated.', 'success');", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstcustomerentry");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void cblocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@loc_cd_parent", cblocation.SelectedValue.ToString()));
            bll.vBindingComboToSp(ref cbdistrict, "sp_tmst_location_get", "loc_cd", "loc_nm", arr);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstcustomerentry");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grdcate_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox dtexp = (TextBox)e.Row.FindControl("dtexp");
                Label lbdoccode = (Label)e.Row.FindControl("lbdoccode");
                TextBox txserialno = (TextBox)e.Row.FindControl("txserialno");
                FileUpload upl = (FileUpload)e.Row.FindControl("upl");
                int nDoc = Convert.ToInt16(bll.vLookUp("select count(1) from tdocument_info where doc_cd='" + lbdoccode.Text + "'"));
                if (nDoc > 0)
                {
                    bool nDate = Convert.ToBoolean(bll.vLookUp("select isdate from tdocument_info where doc_cd='" + lbdoccode.Text + "'"));
                    bool bSerial = Convert.ToBoolean(bll.vLookUp("select isserial from tdocument_info where doc_cd='" + lbdoccode.Text + "'"));
                    bool bPic = Convert.ToBoolean(bll.vLookUp("select ispic from tdocument_info where doc_cd='" + lbdoccode.Text + "'"));
                    if (nDate)
                    {
                        dtexp.CssClass = "makeitreadwrite form-control";
                        dtexp.Enabled = true;
                        //dtexp.Text = System.DateTime.Today.ToString("d/M/yyyy");
                    }
                    else
                    {
                        dtexp.CssClass = "form-control ro";
                        dtexp.Enabled = false;
                    }
                    if (bSerial)
                    {
                        txserialno.CssClass = "makeitreadwrite form-control";
                        txserialno.Enabled = true;
                    }
                    else
                    {
                        txserialno.CssClass = "form-control ro";
                        txserialno.Enabled = false;
                    }
                    if (bPic)
                    {
                        upl.CssClass = "makeitreadwrite form-control";
                        upl.Enabled = true;
                    }
                    else
                    {
                        upl.CssClass = "form-control ro";
                        upl.Enabled = false;
                    }
                }
                else
                {
                    dtexp.CssClass = "form-control ro";
                    dtexp.Enabled = false;
                    txserialno.CssClass = "form-control ro";
                    txserialno.Enabled = false;
                    upl.CssClass = "form-control ro";
                    upl.Enabled = false;
                }
            }

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstcustomerentry");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void txcusname_TextChanged(object sender, EventArgs e)
    {
        txcusname.Text = txcusname.Text.ToUpper();
    }
    protected void txshortname_TextChanged(object sender, EventArgs e)
    {
        txshortname.Text = txshortname.Text.ToUpper();
    }
    protected void btnew_Click(object sender, EventArgs e)
    {

        Response.Redirect("fm_mstcustomerentry.aspx");

    }

    protected void cbsalespoint_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();

        arr.Add(new cArrayList("@qry_cd", "merchandcd"));
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbmerchandiser, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
        arr.Clear();
        arr.Add(new cArrayList("@qry_cd", "SalesJob"));
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbsalesman, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
    }
}