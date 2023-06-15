using CrystalDecisions.ReportSource;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_customerentry : System.Web.UI.Page
{
    cbll bll = new cbll();
    cbll2 bll2 = new cbll2();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingFieldValueToComboWithChoosen(ref cbchannel, "otlcd");
            bll.vBindingFieldValueToComboWithChoosen(ref cbgroup, "cusgrcd");
            bll.vBindingComboToSpWithEmptyChoosen(ref cbsp, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm");
            bll.vBindingFieldValueToComboWithChoosen(ref cbcustcate, "custcate_cd");
            bll.vBindingFieldValueToComboWithChoosen(ref cbpaymentterm, "payment_term");
            bll.vBindingFieldValueToComboWithChoosen(ref cbstatus, "cust_sta_id");
            cbstatus.SelectedValue = "I";
            //cd.v_disablecontrol(cbstatus);
            bll.vBindingFieldValueToComboWithChoosen(ref cbaddresstype, "address_typ");
            cbaddresstype.SelectedValue = "INV";
            btmap.Style.Add("display", "none");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@qry_cd", "SalesJob"));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
            bll.vBindingComboToSpWithEmptyChoosen(ref cbsalesman, "sp_tmst_employee_getbyqry", "emp_cd", "emp_nm", arr);
            arr.Clear();
            arr.Add(new cArrayList("@cust_cd", Request.Cookies["usr_id"].Value));
            // bll.vDelCustomerAddress(arr);
            if (Request.QueryString["cust"] != null)
            {
                txcustocode.Text = Request.QueryString["cust"];
                System.Data.SqlClient.SqlDataReader rs = null;
                arr.Clear();
                arr.Add(new cArrayList("@cust_cd", Request.QueryString["cust"]));
                bll2.vGetMstCustomerByCode(arr, ref rs);
                while (rs.Read())
                {
                    txcustname.Text = rs["cust_nm"].ToString();
                    cbgroup.SelectedValue = rs["cusgrcd"].ToString();
                    cbchannel.SelectedValue = rs["otlcd"].ToString();
                    cbcustcate.SelectedValue = rs["cuscate_cd"].ToString();
                    cbsp.SelectedValue = rs["salespointcd"].ToString();
                    cbsalesman.SelectedValue = rs["salesman_cd"].ToString();
                    cbpaymentterm.SelectedValue = rs["payment_term"].ToString();
                    cbgroup.SelectedValue = rs["cusgrcd"].ToString();
                    txcreditlimit.Text = rs["credit_limit"].ToString();
                    txshortname.Text = rs["cust_sn"].ToString();
                    //txaddress.Text = rs["addr"].ToString();
                    //hdcity.Value = rs["city_cd"].ToString();
                    txfax.Text = rs["fax_no"].ToString();
                    txcontact1.Text = rs["contact1"].ToString();
                    txcontact2.Text = rs["contact2"].ToString();
                    txemail.Text = rs["email"].ToString();
                    txphone.Text = rs["phone_no"].ToString();
                    txaddress.Text = rs["addr"].ToString();
                    hddistrict.Value = rs["district_cd"].ToString();
                    txdistrict.Text = bll.vLookUp("select loc_nm from tmst_location where loc_cd='"+hddistrict.Value+"'");
                    hdcity.Value = rs["city_cd"].ToString();
                    txcity.Text = bll.vLookUp("select loc_nm from tmst_location where loc_cd='" + hdcity.Value + "'");
                    //txcity.Text = bll.vLookUp("select loc_nm from tmst_location where loc_cd='" + rs["city_cd"].ToString() + "'");
                    txvirtualaccount.Text = bll.vLookUp("select virtualaccount from tcustomer_info where cust_cd='" + Request.QueryString["cust"] + "'");
                    //txpostcode.Text = rs["postcode"].ToString();
                    //txnpwp.Text = rs["tax_no"].ToString();
                    cd.v_disablecontrol(cbaddresstype);
                    txcustname.CssClass = cd.csstext;
                    cbstatus.SelectedValue = rs["cust_sta_id"].ToString();
                    txlatitude.Text = rs["latitude"].ToString();
                    txlongitude.Text = rs["longitude"].ToString();
                    hdcust.Value = rs["cust_cd"].ToString();
                    txice.Text = rs["ice_no"].ToString();
                    txrc.Text = rs["rc_no"].ToString();
                    txif.Text = rs["if_no"].ToString();
                    txtp.Text = rs["tp_no"].ToString();
                    txcity_AutoCompleteExtender.ContextKey = hdcity.Value;
                    cd.v_hiddencontrol(btmap);
                    cd.v_disablecontrol(txcustocode);
                    cd.v_disablecontrol(cbsalesman);

                }
                rs.Close();
                //arr.Clear();
                //arr.Add(new cArrayList("@cust_cd", Request.QueryString["cust"]));

                //List<tcustomer_address> _tcustomer_address = bll2.lGetCustomerAddress(arr);
                //grdaddress.DataSource = _tcustomer_address;
                //grdaddress.DataBind();
                //bll.vBindingGridToSp(ref grdaddress, "sp_tcustomer_address_get", arr);
                //Session["tcustomer_address"] = _tcustomer_address;
                //cd.v_hiddencontrol(btsave);
                cd.v_hiddencontrol(btedit);
                cd.v_hiddencontrol(btprint);
                cd.v_disablecontrol(btsearch);
                cd.v_enablecontrol(btupdatechannel);
                cd.v_enablecontrol(btupdatecusgrcd);
                cd.v_enablecontrol(btupdateif);
                cd.v_enablecontrol(btupdateice);
                cd.v_enablecontrol(btupdaterc);
                cd.v_enablecontrol(btupdatetp);
                
            }
            else
            {
                hdcust.Value = string.Empty;
                hdcity.Value = string.Empty;
                cd.v_hiddencontrol(btsave);
                cd.v_hiddencontrol(btedit);
                cd.v_hiddencontrol(btprint);
                cd.v_disablecontrol(btsearch);
                cd.v_disablecontrol(btupdatechannel);
                cd.v_disablecontrol(btupdatecusgrcd);
                cd.v_disablecontrol(btupdatecustomername);
                cd.v_disablecontrol(btupdatepaymentterm);
                cd.v_disablecontrol(btupdatecreditlimit);
                cd.v_disablecontrol(btadd);
                cd.v_disablecontrol(btupdateif);
                cd.v_disablecontrol(btupdateice);
                cd.v_disablecontrol(btupdaterc);
                cd.v_disablecontrol(btupdatetp);

            }
            Session["tcustomer_address"] = new List<tcustomer_address>();
        }
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetDistrictList(string prefixText, int count, string contextKey)
    {
        cbll2 bll = new cbll2();
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lCust = new List<string>();
        string sCust = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@city_cd", contextKey));
        arr.Add(new cArrayList("@loc_cd", prefixText));
        bll.vSearchDistrict(arr, ref rs);
        while (rs.Read())
        {
            sCust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["loc_nm"].ToString(), rs["loc_cd"].ToString());
            lCust.Add(sCust);
        }
        rs.Close();
        return (lCust.ToArray());
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetLocationList(string prefixText, int count, string contextKey)
    {
        cbll2 bll = new cbll2();
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lCust = new List<string>();
        string sCust = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@loc_cd", prefixText));
        arr.Add(new cArrayList("@loc_typ", "CIT"));
        bll.vSearchMstLocation(arr, ref rs);
        while (rs.Read())
        {
            sCust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["loc_nm"].ToString(), rs["loc_cd"].ToString());
            lCust.Add(sCust);
        }
        rs.Close();
        return (lCust.ToArray());
    }

    protected void btsave_Click(object sender, EventArgs e)
    {

        //Label lbaddr = (Label)grdaddress.Rows[e.RowIndex].FindControl("lbaddr");

        if (cbgroup.SelectedValue == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select group','Customer Group','warning');", true);
            return;
        }

        if (cbchannel.SelectedValue == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select channel','Channel','warning');", true);
            return;
        }
        if (cbsp.SelectedValue == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select salespoint','Salespoint','warning');", true);
            return;
        }
        if (cbcustcate.SelectedValue == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select customer category','Credit category','warning');", true);
            return;
        }
        if (cbpaymentterm.SelectedValue == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select Term Of Payment','Term Of Payment','warning');", true);
            return;
        }
        if (cbsalesman.SelectedValue == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select salesman','Salesman','warning');", true);
            return;
        }

        if (cbstatus.SelectedValue == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select status','Active / Inactive','warning');", true);
            return;
        }

        if (txcustname.Text == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Customer must be filled ...','','warning');", true);
            return;
        }

        if (txcreditlimit.Text == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Credit Limit must be filled ...','','warning');", true);
            return;
        }
        //ice mandatory
        if (txtp.Text == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('ICE must be filled ...','','warning');", true);
            return;
        }
        //TP mandatory
        if (txcreditlimit.Text == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('TP must be filled ...','','warning');", true);
            return;
        }


        if (txphone.Text == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Phone Number must be filled ...','','warning');", true);
            return;
        }



        if (txcontact1.Text == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Contct 1 must be filled ...','','warning');", true);
            return;
        }
        if (txcontact2.Text == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Contct 2 must be filled ...','','warning');", true);
            return;
        }


        //if (!fuplktp.HasFile)
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Must Upload File KTP must be filled ...','','error');", true);
        //    return;
        //}
        double dCR = 0;
        if (!double.TryParse(txcreditlimit.Text, out dCR))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Credit Limit must numeric!','Please Credit Limit in numeric value','warning');", true);
            return;
        }


        if (grdaddress.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please add address, at least outlet address!','Customer Address','warning');", true);
            return;
        }

        //bool bCheckOutlet = false;
        //foreach (GridViewRow row in grdaddress.Rows)
        //{

        //    Label lbloc = (Label)row.FindControl("lbloc");
        //    Label lbaddr = (Label)row.FindControl("lbaddr");
        //    Label lbpostcode = (Label)row.FindControl("lbpostcode");

        //    if (lbaddr.Text == string.Empty)
        //    {
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Addres must be filled ...','','error');", true);
        //        return;
        //    }
        //    if (lbloc.Text == string.Empty)
        //    {
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('City must be filled ...','','error');", true);
        //        return;
        //    }
        //    if (lbpostcode.Text == string.Empty)
        //    {
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Post Code must be filled ...','','error');", true);
        //        return;
        //    }
        //    if (row.RowType == DataControlRowType.DataRow)
        //    {
        //        HiddenField hdaddrtyp = (HiddenField)row.FindControl("hdaddrtyp");
        //        if (hdaddrtyp.Value == "OUTLET")
        //        {
        //            bCheckOutlet = true;
        //        }
        //    }
        //}

        //if (!bCheckOutlet)
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please at least one address for OUTLET Itself!','Outlet Address Mandatory','warning');", true);
        //    return;
        //}
        if (Session["tcustomer_address"] == null)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                "sweetAlert('Session expired !','Please re-login','warning');", true);
            return;
        }
        bool _check = false;
        List<tcustomer_address> _tcustomer_address = (List<tcustomer_address>)Session["tcustomer_address"];
        foreach (tcustomer_address _t in _tcustomer_address)
        {
            if (_t.addr_typ == "INV")
            {
                _check = true;
            }
        }

        if (!_check)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
               "sweetAlert('At least there are address for invoice !','Address for invoicing','warning');", true);
        }
        string _cust = string.Empty;

        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cust_nm", txcustname.Text));
        arr.Add(new cArrayList("@payment_term", cbpaymentterm.SelectedValue));
        arr.Add(new cArrayList("@custgrcd", cbgroup.SelectedValue));
        arr.Add(new cArrayList("@otlcd", cbchannel.SelectedValue));
        arr.Add(new cArrayList("@salespointcd", cbsp.SelectedValue));
        arr.Add(new cArrayList("@cuscate_cd", cbcustcate.SelectedValue));
        arr.Add(new cArrayList("@credit_limit", txcreditlimit.Text));
        arr.Add(new cArrayList("@salesmancd", cbsalesman.SelectedValue));
        arr.Add(new cArrayList("@addr", "NA"));
        arr.Add(new cArrayList("@city_cd", hdcity.Value));
        arr.Add(new cArrayList("@contact1", txcontact1.Text));
        arr.Add(new cArrayList("@contact2", txcontact2.Text));
        arr.Add(new cArrayList("@email", txemail.Text));
        arr.Add(new cArrayList("@fax_no", txfax.Text));
        arr.Add(new cArrayList("@cust_sn", txshortname.Text));
        arr.Add(new cArrayList("@phone_no", txphone.Text));
        //arr.Add(new cArrayList("@postcode", "NA"));
        arr.Add(new cArrayList("@max_invoice", 0));
        arr.Add(new cArrayList("@cust_sta_id", cbstatus.SelectedValue));
        arr.Add(new cArrayList("@salesblock", 0));
        //arr.Add(new cArrayList("@tax_no", "NA"));
        arr.Add(new cArrayList("@district_cd", hddistrict.Value));
        arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value));
        arr.Add(new cArrayList("@createdt", DateTime.ParseExact(Request.Cookies["waz_dt"].Value, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        bll.vinsertMstCustomer(arr, ref _cust);
        txcustocode.Text = _cust;
        foreach (tcustomer_address _t in _tcustomer_address)
        {
            if (_t.addr_typ == "INV")
            {
                string _sql = "update tmst_customer set addr='" + _t.address1 + "', city_cd='" + _t.district_cd + "' where cust_cd='" + _cust + "'";
                bll.vExecuteSQL(_sql);
            }
        }
        //if (Request.QueryString["cust"] != null)
        //{
        //    arr.Add(new cArrayList("@cust_cd", Request.QueryString["cust"]));
        //   // bll.vUpdateMstCustomerByNeeded(arr);
        //    sCustCodee = Request.QueryString["cust"];
        //}
        //else
        //{
        //    bll.vinsertMstCustomer(arr, ref sCustCodee);
        //    txcustocode.Text = sCustCodee;
        //}
        ////Update file 
        //string sFileName = string.Empty;
        //if (fuplktp.HasFile)
        //{
        //    string sExt = System.IO.Path.GetExtension(fuplktp.FileName);
        //    sFileName = sCustCodee + "_KTP" + sExt;
        //    fuplktp.SaveAs(bll.sGetControlParameter("image_path") + sFileName);
        //}
        //if (fuplnpwp.HasFile)
        //{
        //    string sExt = System.IO.Path.GetExtension(fuplktp.FileName);
        //    sFileName = sCustCodee + "_TAX" + sExt;
        //    fuplnpwp.SaveAs(bll.sGetControlParameter("image_path") + sFileName);
        //}
        //arr.Clear();
        //arr.Add(new cArrayList("@cust_cd", sCustCodee));
        //arr.Add(new cArrayList("@discpct_dc", cbdiscdc.SelectedValue));
        //arr.Add(new cArrayList("@virtualaccount", txvirtualaccount.Text));
        //if (chfacture.Checked)
        //{
        //    arr.Add(new cArrayList("@facturexchange", "Y"));
        //} else { arr.Add(new cArrayList("@facturexchange", "N")); };

        //bll.vInsertCustomerInfo(arr);
        //arr.Clear();
        //arr.Add(new cArrayList("@cust_cd", sCustCodee));
        //arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value));
        //bll2.vUpdateCustomerAddressByCust(arr);

        //// Sending whatsapp approval ----------------------------------------------------------------------

        //string _wa = "#Customer baru sudah di buat di Cabang : "+bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd='"+Request.Cookies["sp"].Value+"'")+", dengan informasi : Nama : *" +   txcustname.Text.Replace("&"," nd ") + "*, TOP : _" + cbpaymentterm.Text + "_ , Tipe Credit : _" + cbcustcate.SelectedValue +
        //        "_ ,Salesman : *" + cbsalesman.SelectedItem.Text + "* ,Channel : _" + cbchannel.SelectedValue + "_  , Credit Limit : *" + string.Format("{0:#,0.####}",  Convert.ToDecimal( txcreditlimit.Text)) +
        //        "* , Group : _" + cbgroup.SelectedItem.Text + "_";

        //Random _rnd = new Random();
        //Int32 _token = _rnd.Next(100000, 999999);
        //_wa += "%0D%0D Silahkan disetujui dengan reply whatsapp : *Y" + _token.ToString() + "*, Atau ditolak dengan reply whatsapp : *N" + _token.ToString() + "*";

        //arr.Clear();
        //arr.Add(new cArrayList("@qry_cd", "appcust" + cbsp.SelectedValue));
        //arr.Add(new cArrayList("@salespointcd", cbsp.SelectedValue));
        ////arr.Add(new cArrayList("@qry_cd", "appcust" + Request.Cookies["sp"].Value));
        ////arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));

        //List<tuser_profiles_approval> _approval = bll2.luserprofilegetbyapproval(arr);
        //foreach (tuser_profiles_approval _app in _approval)
        //{
        //    //bll.vSendWhatsapp(_app.whatsapp_no, _wa);
        //    arr.Clear();
        //    arr.Add(new cArrayList("@wa_typ", "customer"));
        //    arr.Add(new cArrayList("@token_sent", _token.ToString()));
        //    arr.Add(new cArrayList("@mobileno", _app.whatsapp_no));
        //    arr.Add(new cArrayList("@emp_cd", _app.emp_cd));
        //    arr.Add(new cArrayList("@refno", sCustCodee));
        //    bll.vInsertWhatsappOutbox(arr);
        //}

        //-------------------------------------------------------------------------------------------------
        //btsave.Style.Add("display", "none");
        //btprint.Style.Add("display", "normal");
        //btnew.Style.Add("display", "normal");
        cd.v_hiddencontrol(btsave);
        cd.v_showcontrol(btprint);
        cd.v_disablecontrol(txcustname);
        cd.v_disablecontrol(txcontact1);
        cd.v_disablecontrol(txcontact2);
        cd.v_disablecontrol(txaddress);
        cd.v_disablecontrol(txcity);
        cd.v_disablecontrol(txcreditlimit);
        cd.v_disablecontrol(txemail);
        cd.v_disablecontrol(txfax);
        //cd.v_disablecontrol(txnpwp);
        cd.v_disablecontrol(txphone);
        //cd.v_disablecontrol(txpostcode);
        cd.v_disablecontrol(txshortname);
        cd.v_disablecontrol(cbchannel);
        cd.v_disablecontrol(cbcustcate);
        //cd.v_disablecontrol(cbdiscdc);
        cd.v_disablecontrol(cbgroup);
        cd.v_disablecontrol(cbsalesman);
        cd.v_disablecontrol(cbsp);
        cd.v_disablecontrol(txvirtualaccount);
        cd.v_disablecontrol(cbpaymentterm);
        cd.v_disablecontrol(cbstatus);
        cd.v_disablecontrol(cbaddresstype);
        cd.v_disablecontrol(txcustocode);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('New customer has been created','" + txcustname.Text + "','success');", true);
        //if (Request.QueryString["cust"] != null)
        //{ ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Customer existing has been edited','" + txcustname.Text + "','success');", true); }
        //else
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('New customer has been created','" + txcustname.Text + ", need Approval from Accounting Area','success');", true);
        //}
    }

    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_customerentry.aspx");
    }

    protected void btrefresh_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_customerentry.aspx?cust=" + hdcust.Value);

    }

    protected void btadd_Click(object sender, EventArgs e)
    {
        if (cbaddresstype.SelectedValue == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select address type','Address Type','warning');", true);
            return;
        }

        if (txaddress.Text == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please entry address! ','Address','warning');", true);
            return;
        }

        if (hdcity.Value == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select city','City','warning');", true);
            return;
        }

        string _sql = "update tmst_customer set addr='"+txaddress.Text+"', city_cd='"+hdcity.Value+"' , district_cd='"+hddistrict.Value+"' where cust_cd='" + hdcust.Value + "'";
        bll.vExecuteSQL(_sql );

        cd.v_disablecontrol(txaddress);
        cd.v_disablecontrol(txcity);
        cd.v_disablecontrol(txdistrict);
        cd.v_disablecontrol(btadd);
        //if (txpostcode.Text == string.Empty)
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please entry post code!','Post Code','warning');", true);
        //    return;
        //}
        //List<tcustomer_address> _tcustomer_address = (List<tcustomer_address>)Session["tcustomer_address"];
        //_tcustomer_address.Add(new tcustomer_address
        //{
        //    address1 = txaddress.Text,
        //    addr_typ = cbaddresstype.SelectedValue,
        //    addr_typ_nm = cbaddresstype.SelectedItem.Text,
        //    city = txcity.Text,
        //    zipcode = "NA",
        //    salespointcd = Request.Cookies["sp"].Value,
        //    district_cd = hddistrict.Value,

        //});
        //grdaddress.DataSource = _tcustomer_address;
        //grdaddress.DataBind();
        //List<cArrayList> arr = new List<cArrayList>();
        //if (hdcust.Value == string.Empty)
        //{
        //    arr.Add(new cArrayList("@cust_cd", Request.Cookies["usr_id"].Value));
        //}
        //else
        //{ arr.Add(new cArrayList("@cust_cd", hdcust.Value)); }

        //arr.Add(new cArrayList("@loc_cd", hdcity.Value));
        //arr.Add(new cArrayList("@addr_typ", cbaddresstype.SelectedValue));
        //arr.Add(new cArrayList("@addr", txaddress.Text));
        //arr.Add(new cArrayList("@deleted", 0));
        //arr.Add(new cArrayList("@postcode", txpostcode.Text));
        //bll.vInsertCustomerAddress(arr);
        //arr.Clear();
        //if (hdcust.Value == string.Empty)
        //{
        //    hdcust.Value = Request.Cookies["usr_id"].Value;    
        //}

        //arr.Add(new cArrayList("@cust_cd", hdcust.Value));
        //bll.vBindingGridToSp(ref grdaddress, "sp_tcustomer_address_get", arr);
        //cbaddresstype.SelectedValue = string.Empty;
        //txaddress.Text = string.Empty;
        //txcity.Text = string.Empty;
        ////txpostcode.Text = string.Empty;
        //hdcity.Value = string.Empty;
        //cd.v_showcontrol(btsave);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), 
            "sweetAlert('Address customer has been updated !','Successfully','info');", true);
    }

    protected void grdaddress_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        HiddenField hdids = (HiddenField)grdaddress.Rows[e.RowIndex].FindControl("hdids");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@IDS", hdids.Value));
        // bll.vDelCustomerAddress(arr);
        arr.Clear();
        arr.Add(new cArrayList("@cust_cd", hdcust.Value));
        bll.vBindingGridToSp(ref grdaddress, "sp_tcustomer_address_get", arr);
    }

    protected void btmap_Click(object sender, EventArgs e)
    {
        string lat = bll.vLookUp("select latitude from tmst_customer where cust_cd='" + Request.QueryString["cust"] + "'");
        string lon = bll.vLookUp("select longitude from tmst_customer where cust_cd='" + Request.QueryString["cust"] + "'");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "popupwindow('lookup_map.aspx?la=" + lat + "&at=" + lon + "&ct=" + bll.vLookUp("select cust_nm from tmst_customer where cust_cd='" + Request.QueryString["cust"] + "'") + "','',800,800);", true);
    }

    protected void btlistall_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_mstcustomerlist.aspx");
    }

    protected void btedit_Click(object sender, EventArgs e)
    {

    }

    protected void btcity_Click(object sender, EventArgs e)
    {
        txdistrict_AutoCompleteExtender.ContextKey = hdcity.Value;
        cd.v_disablecontrol(txcity);
    }

    protected void btupdatecusgrcd_Click(object sender, EventArgs e)
    {
        string _sql = "update tmst_customer set cusgrcd='" + cbgroup.SelectedValue + "' where cust_cd='" + hdcust.Value + "'";
        bll.vExecuteSQL(_sql);
        cd.v_disablecontrol(cbgroup);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
            "sweetAlert('Customer group has been changed successfully !','Customer Group','info');", true);
    }

    protected void btupdatechannel_Click(object sender, EventArgs e)
    {
        string _sql = "update tmst_customer set otlcd='" + cbchannel.SelectedValue + "' where cust_cd='" + hdcust.Value + "'";
        bll.vExecuteSQL(_sql);
        cd.v_disablecontrol(cbchannel);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
            "sweetAlert('Customer channel has been changed successfully !','Customer Channel','info');", true);
    }

    protected void btupdatepaymentterm_Click(object sender, EventArgs e)
    {
        string _sql = "update tmst_customer set payment_term='" + cbpaymentterm.SelectedValue + "' where cust_cd='" + hdcust.Value + "'";
        bll.vExecuteSQL(_sql);
        cd.v_disablecontrol(cbpaymentterm);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
            "sweetAlert('Customer TOP has been changed successfully !','Customer TOP','info');", true);
    }

    protected void btupdatecreditlimit_Click(object sender, EventArgs e)
    {
        double _creditlimit = 0;
        if (!double.TryParse(txcreditlimit.Text, out _creditlimit))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
            "sweetAlert('Please enter correct credit limit! !','Customer Credit Limit','warning');", true);


        }
        string _sql = "update tmst_customer set credit_limit=" + _creditlimit + " where cust_cd='" + hdcust.Value + "'";
        bll.vExecuteSQL(_sql);
        cd.v_disablecontrol(txcreditlimit);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
            "sweetAlert('Customer credit limit has been changed successfully !','Customer Credit LImit','info');", true);
    }

    protected void btupdatecustomername_Click(object sender, EventArgs e)
    {
        if (txcustname.Text == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
           "sweetAlert('Please enter correct name! !','Customer Name','warning');", true);
        }
        string _sql = "update tmst_customer set cust_nm='" + txcustname.Text + "' where cust_cd='" + hdcust.Value + "'";
        bll.vExecuteSQL(_sql);
        cd.v_disablecontrol(txcustname);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
            "sweetAlert('Customer Name has been changed successfully !','Customer Name','info');", true);
    }

    protected void btupdateice_Click(object sender, EventArgs e)
    {
        string _sql = "update tmst_customer set ice_no='"+txice.Text+"' where cust_cd='"+hdcust.Value+"'";
        bll.vExecuteSQL(_sql);
        cd.v_disablecontrol(txice);
        cd.v_disablecontrol(btupdateice);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
         "sweetAlert('Customer ICE No has been changed successfully !','ICE','info');", true);
    }

    protected void btupdaterc_Click(object sender, EventArgs e)
    {
        string _sql = "update tmst_customer set rc_no='"+txrc.Text+"' where cust_cd='" + hdcust.Value + "'";
        bll.vExecuteSQL(_sql);
        cd.v_disablecontrol(txrc);
        cd.v_disablecontrol(btupdaterc);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
         "sweetAlert('Customer RC No has been changed successfully !','RC','info');", true);
    }

    protected void btupdateif_Click(object sender, EventArgs e)
    {
        string _sql = "update tmst_customer set if_no='" + txif.Text + "' where cust_cd='" + hdcust.Value + "'";
        bll.vExecuteSQL(_sql);
        cd.v_disablecontrol(txif);
        cd.v_disablecontrol(btupdateif);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
         "sweetAlert('Customer IF No has been changed successfully !','IF','info');", true);
    }

    protected void btupdatetp_Click(object sender, EventArgs e)
    {
        string _sql = "update tmst_customer set tp_no='" + txtp.Text + "' where cust_cd='" + hdcust.Value + "'";
        bll.vExecuteSQL(_sql);
        cd.v_disablecontrol(txtp);
        cd.v_disablecontrol(btupdatetp);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
         "sweetAlert('Customer TP No has been changed successfully !','TP','info');", true);
    }

    protected void btupdatecastegory_Click(object sender, EventArgs e)
    {
        string _sql = "update tmst_customer set cuscate_cd='"+cbcustcate.SelectedValue+"' where cust_cd='"+hdcust.Value+"'";
        bll.vExecuteSQL(_sql);
        cd.v_disablecontrol(cbcustcate);
        cd.v_disablecontrol(btupdatecastegory);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
       "sweetAlert('Customer category has been changed successfully !','"+cbcustcate.SelectedItem.Text+"','info');", true);
    }
}
