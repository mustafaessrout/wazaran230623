using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;

public partial class fm_reqdiscount : System.Web.UI.Page
{
    cbll bll = new cbll();
    
    //test
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));            
            bll.vDelWrkItem(arr);
            bll.vDelWrkFreeItem(arr);
            bll.vDelWrkFreeProduct(arr);
            bll.vDelwrkFormulaDiscount(arr);
            bll.vDelWrkCusGrCD(arr);
            bll.vDelWrkDiscountProduct(arr);
            bll.vDelWrkSalespoint(arr);
            bll.vDelWrkCustType(arr);
            arr.Add(new cArrayList("@salespointcd", null));
            arr.Add(new cArrayList("@cust_cd", null));
            bll.vDelWrkCustomer(arr);
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));   
            lbsalespoint.Text = bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            bll.vBindingFieldValueToCombo(ref cbdiscountmech, "discount_mec");
            bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm");
            //Check full access or not 
            bool bAccess = Convert.ToBoolean(bll.vLookUp("select fullaccess from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'"));
            if (bAccess)
            { cbsalespoint.Enabled = true; challsp.Enabled = true; }
            else { cbsalespoint.SelectedValue = Request.Cookies["sp"].Value.ToString(); cbsalespoint.Enabled = false; challsp.Enabled = false; }
            arr.Clear();
            arr.Add(new cArrayList("@level_no", 1));
            bll.vBindingComboToSp(ref cbbrandedfree, "sp_tmst_product_get", "prod_cd", "prod_nm", arr);
            bll.vBindingFieldValueToCombo(ref cbmethod, "disc_method");
            bll.vBindingFieldValueToCombo(ref cbdiscountmech, "discount_mec");
            bll.vBindingFieldValueToCombo(ref cbdisctype, "disc_typ");
            bll.vBindingComboToSp(ref cbvendor, "sp_tmst_vendor_get", "vendor_cd", "vendor_nm");
        //    bll.vBindingComboToSp(ref cbproposal, "sp_tmst_proposal_get", "prop_no", "remark");
            //cbitemproduct_SelectedIndexChanged(sender, e);
            rditem_SelectedIndexChanged(sender, e);
            rdcustomer_SelectedIndexChanged(sender, e);
            cbdiscountmech_SelectedIndexChanged(sender, e);
            cbdisctype_SelectedIndexChanged(sender, e);

            //dtrequest.Text = System.DateTime.Today.ToShortDateString();
           // dtstart.Text = System.DateTime.Today.ToShortDateString();
            dtstart.Text = System.DateTime.Today.ToString("d/M/yyyy");
            dtdelivery.Text = System.DateTime.Today.ToString("d/M/yyyy");  // (DateTime.ParseExact(dtstart.Text,"d/M/yyyy",System.Globalization.CultureInfo.InvariantCulture).AddDays(1)).ToString("d/M/yyyy"); //by yanto 12-9-15
            dtend.Text = System.DateTime.Today.ToString("d/M/yyyy");
            txitemsearch.Visible = false;

            cbbrandedfree_SelectedIndexChanged(sender, e);
            bll.sFormat2ddmmyyyy(ref dtdelivery);
            //    bll.sFormat2ddmmyyyy(ref dtrequest);
            bll.sFormat2ddmmyyyy(ref dtstart);
            bll.sFormat2ddmmyyyy(ref dtend);
            btprint.Visible = false;
            btsave.Visible = true;
            btedit.Visible = false;
            btnew.Visible = true;
            cbcustsalespoint.Visible = false;
            txdiscno.ReadOnly = true;
            txdiscno.Text = "NEW";
            bll.vBindingFieldValueToCombo(ref cbuom, "uom");
            bll.vBindingFieldValueToCombo(ref cbuomfree, "uom");
            cbitemproduct.Visible = false;
            cbprod.Visible = false;
            cbgroupprod.Visible = false;
            txcust.Visible = false;
            cbcust.Visible = false;
            dtdisc.Text = System.DateTime.Today.ToString("d/M/yyyy");
            bll.sFormat2ddmmyyyy(ref dtdisc);
           
            if (Request.QueryString["dc"] != null)
            {
                System.Data.SqlClient.SqlDataReader rs = null;
                string sDiscNo = Request.QueryString["dc"];
                txdiscno.Text = sDiscNo;
                arr.Clear();
                arr.Add(new cArrayList("@disc_cd", sDiscNo));
                bll.vGetMstDiscount(arr, ref rs);
                while (rs.Read())
                {
                    btsave.Visible = false;
                    btnew.Visible = true;
                    btprint.Visible = true;
                    btedit.Visible = true;
                    dtdisc.Text = rs["disc_dt"].ToString();
                    txremark.Text = rs["remark"].ToString();
                    cbdisctype.SelectedValue = rs["disc_typ"].ToString();
                    rdcustomer.SelectedValue = rs["rdcustomer"].ToString();
                    rditem.SelectedValue = rs["rditem"].ToString();
                    dtstart.Text = rs["start_dt"].ToString();
                    dtend.Text = rs["end_dt"].ToString();
                    dtdelivery.Text = rs["delivery_dt"].ToString();
                    rddiscusage.SelectedValue = rs["discount_use"].ToString();
                    txvendorref.Text = rs["propvendor_no"].ToString();
                    txrefno.Text = rs["proposal_no"].ToString();
                    cbvendor.SelectedValue = rs["vendor_cd"].ToString();
                    txbenefit.Text = rs["benefitpromotion"].ToString();
                    txregularcost.Text = rs["regularcost"].ToString();
                    txnetcost.Text = rs["netcost"].ToString();
                    txminqty.Text = rs["qty_min"].ToString();
                    txmaxorder.Text = rs["qty_max"].ToString();
                    cbdiscountmech.SelectedValue = rs["discount_mec"].ToString();
                    cbdiscountmech_SelectedIndexChanged(sender, e);
                    rdcustomer_SelectedIndexChanged(sender, e);

                    if (rdcustomer.SelectedValue == "T")
                    {
                        arr.Clear();
                        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                        arr.Add(new cArrayList("@disc_cd", sDiscNo));
                        bll.vInsertWrkCustTypeFromDiscount(arr);
                        arr.Clear(); arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                        bll.vBindingGridToSp(ref grdcusttype, "sp_twrk_custtype_get", arr);
                        grdcusttype.Visible = true;

                    }
                    if (rdcustomer.SelectedValue.ToString() == "C")
                    {
                        arr.Clear();
                        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                        arr.Add(new cArrayList("@disc_cd", sDiscNo));
                        bll.vInsertWrkCustomerFromDiscount(arr);
                        arr.Clear(); arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                        bll.vBindingGridToSp(ref grdcust, "sp_twrk_customer_get", arr);
                        grdcust.Visible = true;
                    }
                    if (rdcustomer.SelectedValue.ToString() == "G")
                    {
                        arr.Clear();
                        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                        arr.Add(new cArrayList("@disc_cd", sDiscNo));
                        bll.vInsertWrkCusgrcdFromDiscount(arr);
                        arr.Clear(); arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                        bll.vBindingGridToSp(ref grdcustgroup, "sp_twrk_cusgrcd_get", arr);
                        grdcustgroup.Visible = true;
                    }

                    arr.Clear();
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    arr.Add(new cArrayList("@disc_cd", sDiscNo));
                    bll.vInsertWrkSalespointFromDiscount(arr);
                    arr.Clear();
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    bll.vBindingGridToSp(ref grdsp, "sp_twrk_salespoint_get", arr);

                    if (rditem.SelectedValue.ToString() == "P")
                    {
                        arr.Clear();
                        arr.Add(new cArrayList("@disc_cd", sDiscNo));
                        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                        bll.vInsertWrkItemFromDiscount(arr);
                        arr.Clear(); arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                        bll.vBindingGridToSp(ref grditem, "sp_twrk_item_get", arr);

                    }
                    else if (rditem.SelectedValue.ToString() == "G")
                    {
                        arr.Clear();
                        arr.Add(new cArrayList("@disc_cd", sDiscNo));
                        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                        bll.vInsertWrkDiscProductFromDiscount(arr); arr.Clear();
                        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                        bll.vBindingGridToSp(ref grdproduct, "sp_twrk_discountprod_get", arr);
                        grdproduct.Visible = true;

                    }

                    arr.Clear();
                    arr.Add(new cArrayList("@disc_cd", sDiscNo));
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    bll.vInsertWrkFormulaDiscountfromDiscount(arr); arr.Clear();
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    bll.vBindingGridToSp(ref grdformula, "sp_twrk_formuladiscount_get", arr);
                    arr.Clear();
                    arr.Add(new cArrayList("@disc_cd", sDiscNo));
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    bll.vInsertWrkFreeItemFromDiscount(arr);
                    arr.Clear(); arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    bll.vBindingGridToSp(ref grdfreeitem, "sp_twrk_freeitem_get", arr);

                    grdfreeitem.Enabled = false;
                    cbbrandedfree.Enabled = false;
                    cbprodgroupfree.Enabled = false;
                    cbitemfree.Enabled = false;
                    txcash.Enabled = false;
                    cbmethod.Enabled = false;
                    txordermin.Enabled = false;
                    cbuom.Enabled = false;
                    cbuomfree.Enabled = false;
                    txqty.Enabled = false;
                    txminqty.Enabled = false;
                    txmaxorder.Enabled = false;
                    txregularcost.Enabled = false;
                    txnetcost.Enabled = false;
                    cbdiscountmech.Enabled = false;
                    grdproduct.Enabled = false;
                    grditem.Enabled = false;
                    grdsp.Enabled = false;
                    txcust.Enabled = false;
                    cbcust.Enabled = false;
                    btaddcust.Enabled = false;
                    grdcustgroup.Enabled = false;
                    grdcust.Enabled = false;
                    grdcusttype.Enabled = false;
                    txbenefit.Enabled = false;
                    txvendorref.Enabled = false;
                    txrefno.Enabled = false;
                    cbvendor.Enabled = false;
                    dtdisc.Enabled = false;
                    txremark.Enabled = false;
                    cbdisctype.Enabled = false;
                    grddoc.Enabled = false;
                    dtstart.Enabled = false;
                    dtend.Enabled = false;
                    dtdelivery.Enabled = false;
                    rddiscusage.Enabled = false;
                    rdcustomer.Enabled = false;
                    rditem.Enabled = false;
                    btsave.Visible = false;
                }
                rs.Close();

            }
        }       
    }

     public void WorkThreadFunction()
     {
         //showmessagex.Attributes.Remove("class");
         //showmessagex.Attributes.Add("class", "showmessage");
     }
    protected void rdcustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
    //    Thread thread = new Thread(new ThreadStart(WorkThreadFunction));
    //    thread.Start();
        
       // System.Threading.Thread.Sleep(5000);
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        
        bll.vDelWrkCusGrCD(arr);
        bll.vDelWrkCustType(arr);
        grdcust.Visible = false;
        grdcustgroup.Visible = false;
        grdcusttype.Visible = false;
        arr.Add(new cArrayList("@salespointcd", null));
        arr.Add(new cArrayList("@cust_cd", null));
        bll.vDelWrkCustomer(arr);
       // bll.vBindingGridToSp(ref grdcust, "sp_twrk_customer_get", arr);
        switch (rdcustomer.SelectedValue.ToString())
        { 
            case "C":
                if (grdsp.Rows.Count==null){
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please add salespoint first','Salespoint can't be empty','warning');", true);
                    return;
                }
                cbcustsalespoint.Visible = true;
                //bll.vBindingComboToSp(ref cbcustsalespoint, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm");
                arr.Clear();
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                bll.vBindingComboToSp(ref cbcustsalespoint, "sp_twrk_salespoint_get", "salespointcd", "salespoint_nm",arr);
                cbcust.Visible = false;
                txcust.Visible = true;
                cbcust.DataSource = null;
                cbcust.DataBind();
                break;
            case "G":
                bll.vBindingFieldValueToCombo(ref cbcust, "cusgrcd");
                cbcust.Visible = true;
                cbcustsalespoint.Visible = false;
                txcust.Visible = false;
                cbcust.Enabled = true;
                
                break;
            case "T":
                bll.vBindingFieldValueToCombo(ref cbcust, "otlcd");
                cbcustsalespoint.Visible = false;
                cbcust.Visible = true;
                txcust.Visible = false;
                cbcust.Enabled = true;
                break;
        }
      //  thread.Abort();
     //   showmessagex.Attributes.Remove("class");
     //   showmessagex.Attributes.Add("class", "hidemessage");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "disab", "$get('showmessagex').className = 'hidemessage';", true);
       
    }
    protected void btaddcust_Click(object sender, EventArgs e)
    {
      //  hldisc.Text = "";
        System.Data.SqlClient.SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        try
        {
            switch (rdcustomer.SelectedValue.ToString())
            {
                case "C":
                    if (!chall.Checked)
                    {
                        if (txcust.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please Enter customers!','At least select one.','error');", true);
                            return;
                        }
                        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                        arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
                        arr.Add(new cArrayList("@salespointcd", cbcustsalespoint.SelectedValue.ToString()));
                        bll.vInsertWrkCustomer(arr);
                    }
                    else
                    {
                        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                        arr.Add(new cArrayList("@salespointcd", cbcustsalespoint.SelectedValue.ToString()));
                        arr.Add(new cArrayList("@cust_cd", null));
                        bll.vInsertWrkCustomer(arr);
                    }
                    txcust.Text = "";
                    arr.Clear();
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                  //  arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    bll.vBindingGridToSp(ref grdcust, "sp_twrk_customer_get", arr);
                    cbcust.Enabled = false;
                    grdcustgroup.Visible = false;
                    grdcust.Visible = true;
                    grdcusttype.Visible = false;
                    //Set discount replace
                    arr.Clear();
                    arr.Add(new cArrayList("@rdcustomer", rdcustomer.SelectedValue));
                    arr.Add(new cArrayList("@rditem", rditem.SelectedValue));
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    bll.vBindingGridToSp(ref grddisc, "sp_checkdiscount", arr);
                    //   hldisc.Text = "By Customer direct";
                    break;
                case "G":
                    if (chall.Checked)
                    {
                        arr.Clear();
                        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                        bll.vDelWrkCusGrCD(arr);
                        foreach (ListItem li in cbcust.Items)
                        {
                            arr.Clear();
                            arr.Add(new cArrayList("@cusgrcd", li.Value.ToString()));
                            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                            bll.vInsertWrkCusGrCd(arr);
                        }
                    }
                    else
                    {
                        arr.Add(new cArrayList("@cusgrcd", cbcust.SelectedValue.ToString()));
                        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                        bll.vInsertWrkCusGrCd(arr);
                    }
                    arr.Clear();
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    bll.vBindingGridToSp(ref grdcustgroup, "sp_twrk_cusgrcd_get", arr);
                    grdcustgroup.Visible = true;
                    grdcust.Visible = false;
                    grdcusttype.Visible = false;
                    //Get by Group
                    //  hldisc.Text = "By Group";
                    break;
                case "T":
                    if (chall.Checked)
                    {
                        arr.Clear();
                        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                        bll.vDelWrkCustType(arr);
                        foreach (ListItem li in cbcust.Items)
                        {
                            arr.Clear();
                            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                            arr.Add(new cArrayList("@cust_typ", li.Value.ToString()));
                            bll.vInsertWrkCustType(arr);
                        }
                    }
                    else
                    {
                        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                        arr.Add(new cArrayList("@cust_typ", cbcust.SelectedValue.ToString()));
                        bll.vInsertWrkCustType(arr);
                    }
                    arr.Clear();
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    bll.vBindingGridToSp(ref grdcusttype, "sp_twrk_custtype_get", arr);
                    //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    //bll.vInsertWrkCustByType(arr);
                    grdcustgroup.Visible = false;
                    grdcust.Visible = false;
                    grdcusttype.Visible = true;
                    //Get By Type


                    break;
            }
            vSearchDiscount();
        }catch(Exception ex){
            arr.Clear();
            arr.Add(new cArrayList("@err_source","Discount Add"));
            arr.Add(new cArrayList("@err_description", ex.Message.ToString()));
            bll.vInsertErrorLog(arr);
        }
    }

    void vSearchDiscount()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@rdcustomer", rdcustomer.SelectedValue));
        arr.Add(new cArrayList("@rditem", rditem.SelectedValue));
        bll.vBindingGridToSp(ref grddisc, "sp_checkdiscount", arr);
    }
    protected void rditem_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vDelWrkItem(arr);
        bll.vDelWrkDiscountProduct(arr);
        grditem.DataSource = null;
        grditem.DataBind();
        grdproduct.DataSource = null;
        grdproduct.DataBind();
        switch (rditem.SelectedValue.ToString())
        {
            case "G":
            arr.Clear();
            arr.Add(new cArrayList("@level_no", 1));
            bll.vBindingComboToSp(ref cbitemproduct, "sp_tmst_product_get", "prod_cd","prod_nm", arr);
            cbitemproduct_SelectedIndexChanged(sender, e);
            txitemsearch.Visible = false;
            cbitemproduct.Visible = true;
            cbgroupprod.Visible = true;
            cbprod.Visible = true;   
            grditem.Visible = false;
            grdproduct.Visible = true;
            break;
            case "P":
            txitemsearch.Visible= true;
          //  txitemsearch.Focus();
           // cbitemproduct.Enabled = false;
            cbgroupprod.Visible = false;
            cbgroupprod.Items.Clear();
            cbitemproduct.Items.Clear();
            cbitemproduct.Visible = false;
            cbprod.Visible = false;
            grditem.Visible = true;
            grdproduct.Visible = false;
            
            break;
        }
       
    }
    protected void cbitemproduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@level_no", 2));
        arr.Add(new cArrayList("@prod_cd_parent", cbitemproduct.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbgroupprod, "sp_tmst_product_get", "prod_cd", "prod_nm", arr);
        cbgroupprod_SelectedIndexChanged(sender, e);
    }
   
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionCust(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
       // HttpCookie cok;        
        //cok = HttpContext.Current.Request.Cookies["sp"];
        System.Data.SqlClient.SqlDataReader rs = null;
        string sCust = string.Empty;
        List<string> lCust = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", contextKey));
        arr.Add(new cArrayList("@cust_cd", prefixText));
        bll.vSearchMstCustomer(arr, ref rs);
        while (rs.Read())
        { 
            sCust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + " - "  + rs["cust_nm"].ToString(),rs["cust_cd"].ToString());
            lCust.Add(sCust);
        } rs.Close();
        return (lCust.ToArray());
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList2(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        System.Data.SqlClient.SqlDataReader rs = null;
        string sCust = string.Empty;
        List<string> lCust = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        arr.Add(new cArrayList("@item_nm", prefixText));
        bll.vSearchMstItem(arr, ref rs);
        while (rs.Read())
        {
            sCust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["item_cd"].ToString() +  " - "  + rs["item_nm"].ToString() + " - " + rs["size"].ToString(), rs["item_cd"].ToString());
            lCust.Add(sCust);
        } rs.Close();
        return (lCust.ToArray());
    }

    //public static string[] GetCompletionList3(string prefixText, int count, string contextKey)
    //{
    //    cbll bll = new cbll();
    //    HttpCookie cok;
    //    cok = HttpContext.Current.Request.Cookies["sp"];
    //    System.Data.SqlClient.SqlDataReader rs = null;
    //    string sCust = string.Empty;
    //    List<string> lCust = new List<string>();
    //    List<cArrayList> arr = new List<cArrayList>();
    //    //arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
    //    arr.Add(new cArrayList("@item_nm", prefixText));
    //    bll.vSearchMstItem(arr, ref rs);
    //    while (rs.Read())
    //    {
    //        sCust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["item_cd"].ToString() + " - " + rs["item_nm"].ToString() + " - " + rs["size"].ToString(), rs["item_cd"].ToString());
    //        lCust.Add(sCust);
    //    } rs.Close();
    //    return (lCust.ToArray());
    //}
    protected void btadditem_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (rditem.SelectedValue.Equals(null) || rditem.SelectedValue == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Select discount by Item/Product','Select Item/Product','warning');", true);
            return;
        }
        switch (rditem.SelectedValue.ToString())
        { 
            case "P":
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@item_cd", hditem.Value.ToString()));
                bll.vInsertWrkItem(arr);arr.Clear();
                 arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                bll.vBindingGridToSp(ref grditem, "sp_twrk_item_get", arr);
            break;
            case "G":
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@prod_cd", cbprod.SelectedValue.ToString()));
              //  bll.vInsertWrkItemByProd(arr);
                bll.vInsertWrkDiscountProduct(arr);
                arr.Clear();
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                bll.vBindingGridToSp(ref grdproduct, "sp_twrk_discountprod_get", arr);
            break;
        }
       
        arr.Clear();
       
        txitemsearch.Text = "";
        vSearchDiscount();
      //  txitemsearch.Focus();
    }
    protected void btaddfree_Click(object sender, EventArgs e)
    {
        if (cbuomfree.SelectedValue.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('UOM must be selected','Select UOM','warning');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@item_cd", cbitemfrees.SelectedValue.ToString()));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@uom", cbuomfree.SelectedValue.ToString()));
        bll.vInsertWrkFreeItem(arr);
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdfreeitem, "sp_twrk_freeitem_get", arr);
        arr.Clear();
        
    }
    protected void cbbrandedfree_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@level_no", 2));
        arr.Add(new cArrayList("@prod_cd_parent", cbbrandedfree.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbprodgroupfree, "sp_tmst_product_get", "prod_cd", "prod_nm", arr);
        cbprodgroupfree_SelectedIndexChanged(sender, e);
    }
    protected void cbprodgroupfree_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@level_no", 3));
        arr.Add(new cArrayList("@prod_cd_parent", cbprodgroupfree.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbitemfree, "sp_tmst_product_get", "prod_cd", "prod_nm", arr);
        cbitemfree_SelectedIndexChanged(sender, e);
    }

    protected void cbitemfree_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("prod_cd", cbitemfree.SelectedValue.ToString()));
        //arr.Add(new cArrayList("@prod_cd_parent", cbprodgroupfree.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbitemfrees, "sp_tmst_item_get", "item_cd", "item_desc", arr);
    }

    protected void btadditemfree_Click(object sender, EventArgs e)
    {
        if (cbuomfree.SelectedValue.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('UOM Item Free !','Please select UOM item free !','warning');", true);
            return;
        }

        if (rdfreeitem.SelectedValue.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Select group and product !','Product or Product Group','warning');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        if (rdfreeitem.SelectedValue.ToString() == "I")
        {
            arr.Add(new cArrayList("@prod_cd", cbitemfree.SelectedValue.ToString()));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            //   arr.Add(new cArrayList("@uom", cbuomfree.SelectedValue.ToString()));
            bll.vInsertWrkFreeItem2(arr); arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grdfreeitem, "sp_twrk_freeitem_get", arr);

        }
        else if (rdfreeitem.SelectedValue.ToString() == "G")
        {
            arr.Add(new cArrayList("@prod_cd", cbitemfree.SelectedValue.ToString()));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vInsertWrkFreeProduct(arr);
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grdfreeproduct, "sp_twrk_freeproduct_get", arr);

        }
       
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        string sDiscNo = ""; string sProdCode = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        if (Request.Cookies["sp"].Value.ToString() == "0")
        {
        if (txrefno.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Proposal number can not be empty','Propsal Number','warning');", true);
            return;
        }
        }
        string sCount=bll.vLookUp("select count(1) from tmst_discount where proposal_no='" + txrefno.Text + "'") ;
        if (Convert.ToInt32(sCount) > 0)
        {
           ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Proposal number already exist','Propsal Number','warning');", true);
           return;
        }
        if (Request.QueryString["dc"] != null)
        {
            arr.Add(new cArrayList("@disc_cd", Request.QueryString["dc"]));
            arr.Add(new cArrayList("@proposal_no", txrefno.Text));
            bll.vUpdateMstDiscount(arr);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Proposal number has been added','Proposal No.','success');", true);
            txrefno.CssClass = "makeitreadonly";
            txrefno.Enabled = false;
            return;
        }

        
        if ((grdcust.Rows.Count == 0) && (grdcustgroup.Rows.Count == 0) && (grdcusttype.Rows.Count == 0))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Please customer for target discount !','Group, Type Or Customer Direct !','warning');", true);
            return;
        }

        if ((grditem.Rows.Count == 0) && (grdproduct.Rows.Count == 0))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Please item product for target discount !','Product group or item direct !','warning');", true);
            return;
        }

        if (grdsp.Rows.Count==0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Salespoint has not yet entry !','Select salespoint discount !','warning');", true);
            return;
        }
        string sSP = bll.vLookUp("select dbo.fn_checkwrksalespoint('"+Request.Cookies["usr_id"].Value.ToString()+"')");
        if (sSP != "ok")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Salespoint has not yet selected !','Re select salespoint !','warning');", true);
            return;
        }

        if (grdformula.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Formula has not yet entry !','Entry Formula discount !','warning');", true);
            return;
        }

        double dOut = 0; double dMax = 0;
        if (!double.TryParse(txminqty.Text, out dOut))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Minimum quantity must numeric !','wrong quantity','warning');", true);
            return;
        }
        if (!double.TryParse(txmaxorder.Text, out dMax))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Maximum quantity must numeric !','wrong quantity','warning');", true);
            return;
        }
        if (dMax > 0)
        {
            if (dMax < dOut)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Maximum order can not less than minimum !','wrong quantity','warning');", true);
                return;
            }
        }

        //if (DateTime.ParseExact( dtdelivery.Text,"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture) <= DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(),"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture))
        //{
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Delivery date minimum 1 day next from system date!','Wrong Delivery Date','warning');", true);
        //        return;
        //}
        //by yanto 11-9-15
        if (DateTime.ParseExact(dtdelivery.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture) < DateTime.ParseExact(dtstart.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Delivery date minimum same or  day next from start date!','Wrong Delivery Date','warning');", true);
            return;
        }
        //***************
        double dRegCost;
        if (!double.TryParse(txregularcost.Text, out dRegCost))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Regular Cost must be numeric !','Regular Cost','warning');", true);
            return;
        }

        if (!double.TryParse(txnetcost.Text, out dRegCost))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Net Cost must be numeric !','Net Cost','warning');", true);
            return;
        }

        //Edit
        if (txdiscno.Text != "NEW")
        {
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@disc_cd", sDiscNo));
            bll.vUpdateMstDiscount2(arr);
            return;
        }

        if (cbdiscountmech.SelectedValue.ToString() == "FG")
        {
            if (rdfreeitem.SelectedValue.ToString() == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please select free item or product','Free Item or Product','warning');", true);
                return;
            }
        }

      
        //Save main discount data
        arr.Add(new cArrayList("@request_dt", DateTime.ParseExact( dtdisc.Text,"d/M/yyyy",System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@discount_mec", cbdiscountmech.SelectedValue.ToString()));
        arr.Add(new cArrayList("@start_dt", DateTime.ParseExact( dtstart.Text,"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@end_dt", DateTime.ParseExact( dtend.Text,"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@delivery_dt", DateTime.ParseExact( dtdelivery.Text,"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@rdcustomer", rdcustomer.SelectedValue.ToString()));
        arr.Add(new cArrayList("@rditem", rditem.SelectedValue.ToString()));
        arr.Add(new cArrayList("@disc_typ", cbdisctype.SelectedValue.ToString()));
        arr.Add(new cArrayList("@remark", txremark.Text));
        arr.Add(new cArrayList("@usr_id",Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@discount_use", rddiscusage.SelectedValue.ToString()));
        arr.Add(new cArrayList("@provendor_no", txvendorref.Text));
        arr.Add(new cArrayList("@vendor_cd", cbvendor.SelectedValue.ToString()));
        arr.Add(new cArrayList("@benefitpromotion", txbenefit.Text));
        arr.Add(new cArrayList("@disc_dt", DateTime.ParseExact( dtdisc.Text,"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@qty_min", txminqty.Text));
        arr.Add(new cArrayList("@qty_max", txmaxorder.Text));
        arr.Add(new cArrayList("@regularcost", txregularcost.Text));
        arr.Add(new cArrayList("@netcost", txnetcost.Text));
        arr.Add(new cArrayList("@rdfreeitem", rdfreeitem.SelectedValue ));
        bll.vInsertRequestDiscount(arr, ref sDiscNo);
        txdiscno.Text = sDiscNo;
        arr.Clear();
        // Proposal Number
        arr.Add(new cArrayList("@disc_cd", sDiscNo));
        arr.Add(new cArrayList("@proposal_no", txrefno.Text));
        bll.vUpdateMstDiscount(arr);

        if (rditem.SelectedValue.ToString() == "P")
        {
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@disc_cd", sDiscNo));
            bll.vInsertDiscountItem(arr);
        }
        else if (rditem.SelectedValue.ToString() == "G")
        {
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@disc_cd", sDiscNo));
            bll.vInsertDiscountProduct(arr); 
            //Get product code 
            sProdCode = bll.vLookUp("select distinct prod_cd from twrk_discountprod where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "'");

        }

        if (rdcustomer.SelectedValue.ToString() == "C")
        {
            //Fill Discount Customer
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@disc_cd", sDiscNo));
            bll.vInsertDiscountCustomer(arr);
        }
        else if (rdcustomer.SelectedValue.ToString() == "G")
        {
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@disc_cd", sDiscNo));
            bll.vInsertDiscountCusGrCd(arr);
        }
        else if (rdcustomer.SelectedValue.ToString() == "T")
        {
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@disc_cd", sDiscNo));
            bll.vInsertDiscountCusttype(arr);
        }
        //Fill Salesoint discount
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@disc_cd", sDiscNo));
        bll.vInsertDiscountSalespoint(arr);
        //Fill Item Discounted
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@disc_cd", sDiscNo));
       //  bll.vInsertDiscountItem(arr);
        //Fill item Free
        if (rdfreeitem.SelectedValue.ToString() == "I")
        {
            bll.vInsertDiscountFreeitem(arr);
        }
        else if (rdfreeitem.SelectedValue.ToString() == "G")
        {
            bll.vInsertDiscountFreeProduct(arr);
        }
        bll.vInsertDiscountFormula(arr);
        //Discount Document
        foreach (GridViewRow row in grddoc.Rows)
        {
                FileUpload upl = (FileUpload)row.FindControl("uplfile");
                Label lbdoccode = (Label)row.FindControl("lbdoccode");
                arr.Clear();
                arr.Add(new cArrayList("@disc_cd", sDiscNo));
                arr.Add(new cArrayList("@doc_cd", lbdoccode.Text));
                if ((upl.FileName != "") || (upl.FileName.Equals(null)))
                { 
                    arr.Add(new cArrayList("@filename", sDiscNo + lbdoccode.Text + ".jpg"));
                    upl.SaveAs(bll.sGetControlParameter("image_path") + sDiscNo + lbdoccode.Text + ".jpg");
                }
            //    bll.vInsertDiscountDocument(arr);
        }
        //Discount Issued
        foreach (GridViewRow row in grdissued.Rows)
        {
             CheckBox chk = (CheckBox)row.FindControl("chk");
             if (chk.Checked)
             {
                 Label lbissuedcode = (Label)row.FindControl("lbissuedcode");
                 arr.Clear();
                 arr.Add(new cArrayList("@disc_cd", sDiscNo));
                 arr.Add(new cArrayList("@issued_cd", lbissuedcode.Text));
                 bll.vInsertDiscountIssued(arr);
             }
        }
       // arr.Clear();
       // string sBranded = "Indomie";
        //List<string> lInfo = bll.lGetApproval("discount", 1);
        string sSalespointtype = bll.vLookUp("select salespoint_typ from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
        if (sSalespointtype == "SP") { 
        //List<string> lInfo = bll.lProductSupervisorApproval(sProdCode);
        List<string> lInfo = bll.lGetApproval("branchspv", 1);
        Random rnd = new Random();
        int nRnd = rnd.Next();
        string sText = "<html><head><body>Dear New Discount been created , with no. " + sDiscNo + "</br>" +
            "<table><tr><td>Start Period  </td><td>:</td><td>" + dtstart.Text + "</td></tr>" +
            "<tr><td>End Period  </td><td>:</td><td> " + dtend.Text + "</td></tr>" +
            "<tr><td>Delivery Date </td><td>: </td><td>" + dtdelivery.Text + "</td></tr>" +
            "<tr><td>Salespoint </td><td>:</td><td>" + bll.sDiscountSalespoint(sDiscNo) + "</td></tr>";
        if (rditem.SelectedValue.ToString() == "G")
        {
            sText += "<tr><td>Items</td><td>:</td><td>" + bll.sDiscountProduct(sDiscNo) + "</td></tr>";
        }
        else {
            sText+= "<tr><td>Items</td><td>:</td><td>" + bll.sDiscountItem(sDiscNo) + "</td></tr>";
            }
        sText += "<tr><td>Discount Mechanism </td><td>:</td><td>" + cbdiscountmech.SelectedItem.Text + "</td></tr>" +
             "<tr><td>Quantity Limit order </td><td>:</td><td>" + txminqty.Text + "</td></tr></table></br>";
        //    "<p>Please Click this  for approved : <a href='" + Request.Url.GetLeftPart(UriPartial.Authority) + "/landingpage.aspx?appcode=" + nRnd.ToString() + "&sta=A'>Approve</a>, or for rejected please click <a href='" + Request.Url.GetLeftPart(UriPartial.Authority) + "/landingpage.aspx?appcode=" + nRnd.ToString() + "&sta=R'>Reject</a></p>";

        //sText += "</br></br>Note : If you not take action , within 3 days this new discount will be disabled.";
        string sSubject = "New Discount Has Been Created";
        
        string semail = lInfo[1];
        bll.vSendMail(semail, sSubject, sText);
        arr.Clear();
        arr.Add(new cArrayList("@trxcd", "discount"));
        arr.Add(new cArrayList("@token", nRnd.ToString()));
        arr.Add(new cArrayList("@doc_no", sDiscNo));
        bll.vInsertEmailSent(arr);
        //SMS Sending
        rnd = new Random();
        int nRandom = rnd.Next(1000,9999);
        string sMobileNo = lInfo[0];
        cd.vSendSms("New Discount No. " + sDiscNo + ", Proposal Number : "  + txrefno.Text +    " : , Please check your email for detail content" , sMobileNo);
        //arr.Clear();
        //arr.Add(new cArrayList("@doc_typ", "discount"));
        //arr.Add(new cArrayList("@token", nRandom.ToString()));
        //arr.Add(new cArrayList("@receiver", sMobileNo));
        //arr.Add(new cArrayList("@doc_no", sDiscNo));
        } // ENd of check salespoint
       // bll.vInsertSMSSent(arr);
        btsave.Visible = false;
        btprint.Visible = true;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opd", "preventBack();", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('New discount has been saved successfully ....','Disc No. " + sDiscNo + "','success');", true);
    }
    protected void btaddmethod_Click(object sender, EventArgs e)
    {

        double dOrderMin = 0;

        if (cbuom.SelectedValue.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alr", "sweetAlert('UOM Must be selected !','UOM','warning');", true);
            return;
        }

        if (cbdiscountmech.SelectedValue.ToString() == "FG") { 
        if (cbuomfree.SelectedValue.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alr", "sweetAlert('UOM Free Must be selected !','UOM Free','warning');", true);
            return;
        }
        }
        if (!double.TryParse(txordermin.Text, out dOrderMin))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alr", "sweetAlert('Quantity Order min must numeric','Please fill qty order in numeric','warning');", true); return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@amt", txcash.Text));
        arr.Add(new cArrayList("@qty", txqty.Text));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@disc_typ", cbmethod.SelectedValue.ToString()));
        arr.Add(new cArrayList("@min_qty", txordermin.Text));
        arr.Add(new cArrayList("@uom", cbuom.SelectedValue.ToString()));
        arr.Add(new cArrayList("@uom_free", cbuomfree.SelectedValue.ToString()));
        arr.Add(new cArrayList("@percentage", txpercent.Text));
        bll.vInsertWrkFomulaDiscount(arr);
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdformula, "sp_twrk_formuladiscount_get", arr);
        txordermin.Enabled = true;
        txordermin.CssClass = "makeitreadwrite";
        //txordermin.Focus();
        txordermin.Text = "";
        txqty.Text = "0";
        txcash.Text = "0";

    }
    protected void grdcust_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        grdcust.PageIndex = e.NewPageIndex;
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdcust, "sp_twrk_customer_get", arr);    
    }
    protected void cbdiscountmech_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbdiscountmech.SelectedValue.ToString() == "CH")
        {
            txqty.Enabled = false;
            txqty.CssClass = "makeitreadonly";
            txcash.Enabled = true;
            txcash.CssClass = "maketireadwrite";
            txpercent.Enabled = false;
            txpercent.CssClass = "makeitreadonly";
            txqty.Text = "0";
            txcash.Text = "0";
            txpercent.Text = "0";
            grdfreeitem.Visible = false;
            tbdiscount.Visible = false;
        }
        else if (cbdiscountmech.SelectedValue.ToString() == "FG") {
            txqty.Enabled = true;
            txqty.CssClass = "makeitreadwrite";
            txcash.Enabled = false;
            txcash.CssClass = "makeitreadonly";
            txcash.Text = "0";
            txpercent.Enabled = false;
            txpercent.Text = "0";
            txpercent.CssClass = "makeitreadonly";
            txqty.Text = "0";
            tbdiscount.Visible = true;
            
         }
        else if (cbdiscountmech.SelectedValue.ToString() == "PC")
        {
            txqty.Enabled = false;
            txqty.CssClass = "makeitreadonly";
            txcash.Enabled = false;
            txcash.CssClass = "makeitreadonly";
            txcash.Text = "0";
            txpercent.Text = "0";
            txqty.Text = "0";
            tbdiscount.Visible = false;
            txpercent.Enabled = true;
            txpercent.CssClass = "makeitreadwrite";
        }


    }
    protected void grdfreeitem_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbitemcode = (Label)grdfreeitem.Rows[e.RowIndex].FindControl("lbitemcode");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vDelWrkFreeItem(arr);
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
     //   bll.vBindingGridToSp(ref grdfreeitem, "sp_twrk_freeitem_get", arr);
    }
    protected void btaddgroupfree_Click(object sender, EventArgs e)
    {

    }
    protected void grdfreeitem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbbranded = (Label)e.Row.FindControl("lbbranded");
            Label lbitemcode = (Label)e.Row.FindControl("lbitemcode");
            lbbranded.Text = bll.sGetBranded(lbitemcode.Text);
        }
    }
    protected void grditem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbitemcode = (Label)e.Row.FindControl("lbitemcode");
            Label lbbranded = (Label)e.Row.FindControl("lbbranded");
            lbbranded.Text = bll.sGetBranded(lbitemcode.Text);
        }
    }
    protected void grditem_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        Label lbitemcode = (Label)grditem.Rows[e.RowIndex].FindControl("lbitemcode");
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
        bll.vDelWrkItem(arr);
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grditem, "sp_twrk_item_get", arr);
    }
    protected void grdcustgroup_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbcustgroup = (Label)grdcustgroup.Rows[e.RowIndex].FindControl("lbcustgroup");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cusgrcd", lbcustgroup.Text));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vDelWrkCusGrCD(arr);
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdcustgroup, "sp_twrk_cusgrcd_get", arr);
    }
    protected void grdcustgroup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        grdcustgroup.PageIndex = e.NewPageIndex;
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdcustgroup, "sp_twrk_cusgrcd_get", arr);
    }
    protected void cbdisctype_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@fld_nm", "issued_cd"));
        bll.vBindingGridToSp(ref grdissued, "sp_tfield_value_get", arr);
        arr.Clear();
        arr.Add(new cArrayList("@disc_typ", cbdisctype.SelectedValue.ToString()));
        //bll.vBindingGridToSp(ref grddoc, "sp_tdisctype_document_get", arr);
        //arr.Add(new cArrayList("@doc_typ", "DISC"));
        //bll.vBindingGridToSp(ref grddoc, "sp_tmst_document_get", arr);
    }
   
    protected void Button1_Click(object sender, EventArgs e)
    {
        //showmessagex.Attributes.Remove("class");
        //showmessagex.Attributes.Add("class", "hidemessage");
    }
    protected void grdfreeitem_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        grdfreeitem.PageIndex = e.NewPageIndex;
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdfreeitem, "sp_twrk_freeitem_get", arr);
    }
    protected void grdfreeitem_RowDeleting1(object sender, GridViewDeleteEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (rdfreeitem.SelectedValue.ToString() == "I")
        {
            Label lbitemcode = (Label)grdfreeitem.Rows[e.RowIndex].FindControl("lbitemcode");
            arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vDelWrkFreeItem(arr);
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grdfreeitem, "sp_twrk_freeitem_get", arr);
        }
        else if (rdfreeitem.SelectedValue.ToString()=="G")
        {
            Label lbprodcode = (Label)grdfreeproduct.Rows[e.RowIndex].FindControl("lbprodcode");
            arr.Add(new cArrayList("@prod_cd", lbprodcode.Text));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vDelWrkFreeProduct(arr);
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grdfreeproduct, "sp_twrk_freeproduct_get", arr);
        }
        
         }
    protected void cbgroupprod_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@level_no", 3));
        arr.Add(new cArrayList("@prod_cd_parent", cbgroupprod.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbprod, "sp_tmst_product_get", "prod_cd", "prod_nm", arr);

    }
    protected void grdformula_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbminqty = (Label)grdformula.Rows[e.RowIndex].FindControl("lbminqty");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@min_qty", lbminqty.Text));
        bll.vDelwrkFormulaDiscount(arr); arr.Clear(); arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdformula, "sp_twrk_formuladiscount_get", arr);
    }
    protected void txminqty_TextChanged(object sender, EventArgs e)
    {
        txordermin.Text = txminqty.Text;
        txordermin.Enabled = false;
        txordermin.CssClass = "makeitreadonly";
    }
    protected void btaddsalespoint_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (challsp.Checked)
        {
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vInsertWrkSalespointAll(arr);
        }
        else
        {
            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vInsertWrkSalespoint(arr);
        }
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdsp, "sp_twrk_salespoint_get", arr);
        bll.vBindingComboToSp(ref cbcustsalespoint, "sp_twrk_salespoint_get", "salespointcd", "salespoint_nm",arr);
    }
    protected void grdsp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        grdsp.PageIndex = e.NewPageIndex;
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdsp, "sp_twrk_salespoint_get", arr);
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@disc_cd", txdiscno.Text));
        bll.vReportDiscount(arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "orep", "openreport('fm_report2.aspx?src=dc&no=" + txdiscno.Text +  "');", true);
    }
    protected void grdcust_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbcustcode =(Label) grdcust.Rows[e.RowIndex].FindControl("lbcustcode");
        Label lbsalespointcd = (Label)grdcust.Rows[e.RowIndex].FindControl("lbsalespointcd");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@cust_cd", lbcustcode.Text));
        arr.Add(new cArrayList("@salespointcd", lbsalespointcd.Text));
        bll.vDelWrkCustomer(arr); arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdcust, "sp_twrk_customer_get", arr);
    }
    protected void grdcusttype_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbcusttype = (Label)grdcusttype.Rows[e.RowIndex].FindControl("lbcusttype");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cust_typ", lbcusttype.Text));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vDelWrkCustType(arr); arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdcusttype, "sp_twrk_custtype_get", arr);
        bll.vBindingGridToSp(ref grdcust, "sp_twrk_customer_get", arr);
    }
    protected void grdsp_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbsalespoint = (Label)grdsp.Rows[e.RowIndex].FindControl("lbsalespoint");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", lbsalespoint.Text));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));        
        bll.vDelWrkSalespoint(arr);
        //arr.Add(new cArrayList("@salespointcd", null));
        bll.vDelWrkCustomer(arr);
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdsp, "sp_twrk_salespoint_get", arr);
        bll.vBindingComboToSp(ref cbcustsalespoint, "sp_twrk_salespoint_get", "salespointcd", "salespoint_nm",arr);
        bll.vBindingGridToSp(ref grdcust, "sp_twrk_customer_get",arr);
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));        
    }
    protected void grditemfree_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbminqty = (Label)grdformula.Rows[e.RowIndex].FindControl("lbminqty");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@min_qty", lbminqty.Text));
        bll.vDelwrkFormulaDiscount(arr);
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdformula, "sp_twrk_formuladiscount_get", arr);
    }
    protected void challsp_CheckedChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vDelWrkSalespoint(arr);
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdsp, "sp_twrk_salespoint_get", arr);
    }
    protected void grddoc_RowCreated(object sender, GridViewRowEventArgs e)
    {
        HiddenField hddic = (HiddenField)e.Row.FindControl("hddic");
        FileUpload upl = (FileUpload)e.Row.FindControl("upl");
        if (hddic.Value.ToString()=="HO")
        {
            upl.Enabled=false;
        }
        else
        {upl.Enabled=true;}
    }
    protected void grddoc_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbfileupload = (Label)e.Row.FindControl("lbfilename");
            Label lbdoccode = (Label)e.Row.FindControl("lbdoccode");
            lbfileupload.Text = bll.vLookUp("select [filename] from twrk_fileupload where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and doc_cd='" + lbdoccode.Text + "'");
        }
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_reqdiscount.aspx");
    }
    protected void rddiscusage_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btedit_Click(object sender, EventArgs e)
    {
        btsave.Visible = true;
        //txordermin.Enabled = true;
        //cbuom.Enabled = true;
        //cbuomfree.Enabled = true;
        //txqty.Enabled = true;
        //txcash.Enabled = true;
        //grdformula.Enabled = true;
        txrefno.Enabled = true;
        txrefno.CssClass = "makeitreadwrite";
        txrefno.Focus();
        //txordermin.Focus();
    }
    protected void grdproduct_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        
        List<cArrayList> arr = new List<cArrayList>();
        Label lbprodcode = (Label)grdproduct.Rows[e.RowIndex].FindControl("lbprodcode");
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@prod_cd", lbprodcode.Text));
        bll.vDeltwrk_discountprod(arr);
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdproduct, "sp_twrk_discountprod_get", arr);
    }
    protected void rdfreeitem_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdfreeitem.SelectedValue.ToString() == "I")
        {
            cbitemfrees.Visible = true;
            btaddfree.Visible = true;
            btadditemfree.Visible = false;
            grdfreeproduct.Visible = false;
            grdfreeitem.Visible = true;
        }
        else if (rdfreeitem.SelectedValue.ToString()=="G")
        {
            cbitemfrees.Visible = false;
            btaddfree.Visible = false;
            btadditemfree.Visible = true;
            grdfreeproduct.Visible = true;
            grdfreeitem.Visible = false;
        }
    }
    protected void grddoc_RowEditing(object sender, GridViewEditEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        grddoc.EditIndex = e.NewEditIndex;
        arr.Clear();
        arr.Add(new cArrayList("@disc_typ", cbdisctype.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grddoc, "sp_tdisctype_document_get", arr);
        //cbdisctype_SelectedIndexChanged(sender, e);
    }
    protected void grddoc_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        FileUpload uplfile = (FileUpload)grddoc.Rows[e.RowIndex].FindControl("uplfile");
        Label lbdoccode = (Label)grddoc.Rows[e.RowIndex].FindControl("lbdoccode");
        if (uplfile.HasFile) 
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@filename", uplfile.FileName));
            arr.Add(new cArrayList("@doc_cd", lbdoccode.Text));
            bll.vInsertWrkFileUpload(arr);

        }
        grddoc.EditIndex = -1;
        cbdisctype_SelectedIndexChanged(sender, e);
    }
    protected void grddoc_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grddoc.EditIndex = -1;
        cbdisctype_SelectedIndexChanged(sender, e);
    }
    protected void cbcustsalespoint_SelectedIndexChanged(object sender, EventArgs e)
    {
        txcust_AutoCompleteExtender.ContextKey = cbcustsalespoint.SelectedValue;
    }
}