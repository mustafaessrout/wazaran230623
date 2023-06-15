using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_fm_custedit : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingComboToSp(ref cbcustcate, "sp_tmst_customercategory_get", "custcate_cd", "custcate_nm");
            bll.vBindingFieldValueToCombo(ref cbcusgrcd, "cusgrcd");
            bll.vBindingFieldValueToCombo(ref cbchannel, "otlcd");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@level_no", 2));
            //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingComboToSp(ref cblocation, "sp_tmst_location_get", "loc_cd", "loc_nm", arr);
            cblocation_SelectedIndexChanged(sender, e);
            cbcustcate_SelectedIndexChanged(sender, e);
        }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
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
        bll.vSearchMstCustomer(arr, ref rs);
        while (rs.Read())
        {
            sCust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + "-" + rs["cust_nm"].ToString(), rs["cust_cd"].ToString());
            lCust.Add(sCust);
        }
        rs.Close();
        return (lCust.ToArray());
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        System.Data.SqlClient.SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
        bll.vGetMstCustomer(arr, ref rs);
        while (rs.Read())
        {
            lbcustcode.Text = rs["cust_cd"].ToString();
            txcust.Text = rs["cust_nm"].ToString();
            txadderss.Text = rs["addr"].ToString();
            //lbcity.Text = rs["city_cd"].ToString();            
            if (rs["city_cd"].ToString() != "") { cblocation.SelectedValue = rs["city_cd"].ToString(); }
            cblocation_SelectedIndexChanged(sender, e);
            if (rs["district_cd"].ToString() != "") { cbdistrict.SelectedValue = rs["district_cd"].ToString(); }
            cbcustcate.SelectedValue = rs["cuscate_cd"].ToString();
            txtop.Text = rs["payment_term"].ToString();
            txcl.Text = rs["credit_limit"].ToString();
            cbcusgrcd.SelectedValue = rs["cusgrcd"].ToString();
            cbchannel.SelectedValue = rs["otlcd"].ToString();
            txcustname.Text = rs["cust_nm"].ToString();
            txvat.Text = rs["tax_no"].ToString();
            txcustarvat.Text = rs["vat_custarabic"].ToString();
            txcustenvat.Text = rs["vat_custname"].ToString();
            txcustarabic.Text = rs["cust_arabic"].ToString();
            txsnname.Text = rs["cust_sn"].ToString();
            hdlat.Value = rs["latitude"].ToString();
            hdlng.Value = rs["longitude"].ToString();
            txlatitude.Text = rs["latitude"].ToString();
            txlongitude.Text = rs["longitude"].ToString();
            txlongitude.CssClass = cd.csstextro;
            txlatitude.CssClass = cd.csstextro;
        }
        rs.Close();
        //cblocation_SelectedIndexChanged(sender, e);
        //cbcustcate_SelectedIndexChanged(sender, e);
        //string sDistrict = bll.vLookUp("select district_cd from tcustomer_info where cust_cd='" + hdcust.Value + "'");
        //if (!string.IsNullOrEmpty(sDistrict))
        //{
        //    cbdistrict.SelectedValue = sDistrict;
        //}

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "fn_getaddress('" + hdlat.Value + "','" + hdlng.Value + "');", true);
    }
    protected void btsave_Click(object sender, EventArgs e)
    {

        //if (Convert.ToDouble(txcl.Text) < Convert.ToDouble(lblmincredit.Text))
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Credit Limit can't less than min','Credit Limit','warning');", true);
        //    return;
        //}
        //if (Convert.ToDouble(txcl.Text) > Convert.ToDouble(lblmaxcredit.Text))
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Credit Limit can't more than Max','Credit Limit','warning');", true);
        //    return;
        //}


        double creditLimit = double.Parse(txcl.Text);
        double minCredit = double.Parse(lblmincredit.Text);
        double maxCredit = double.Parse(lblmaxcredit.Text);

        if (cbcustcate.SelectedValue.ToString() != "CASH")
        {
            string creditCategory = cbcustcate.SelectedValue.ToString();
            if (creditCategory == "NL" && creditLimit >= 2000) {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Credit Limit can't more than 2000','Credit Limit','warning');", true);
                return;
            }
            else if (creditCategory == "CREDIT1" && creditLimit >= 10000) {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Credit Limit can't more than 10000','Credit Limit','warning');", true);
                return;
            }
            else if (creditCategory == "CREDIT2" && creditLimit >= 100000) {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Credit Limit can't more than 100000','Credit Limit','warning');", true);
                return;
            }
            else if (creditCategory == "CREDIT3" && creditLimit <= 100001) {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Credit Limit can't less than 100000','Credit Limit','warning');", true);
                return;
            }
        }
        


        //if (Convert.ToDouble(txcl.Text) < Convert.ToDouble(lblmincredit.Text))
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Credit Limit can't less than min','Credit Limit','warning');", true);
        //    return;
        //}
        //if (Convert.ToDouble(txcl.Text) > Convert.ToDouble(lblmaxcredit.Text))
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Credit Limit can't more than Max','Credit Limit','warning');", true);
        //    return;
        //}

        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
        arr.Add(new cArrayList("@credit_limit", txcl.Text));
        arr.Add(new cArrayList("@cusgrcd", cbcusgrcd.SelectedValue.ToString()));
        arr.Add(new cArrayList("@cuscate_cd", cbcustcate.SelectedValue.ToString()));
        arr.Add(new cArrayList("@payment_term", txtop.Text));
        arr.Add(new cArrayList("@otlcd", cbchannel.SelectedValue.ToString()));
        arr.Add(new cArrayList("@vat_custname", txcustenvat.Text));
        arr.Add(new cArrayList("@vat_custarabic", txcustarvat.Text));
        arr.Add(new cArrayList("@cust_nm", txcustname.Text));
        arr.Add(new cArrayList("@tax_no", txvat.Text));
        arr.Add(new cArrayList("@cust_arabic", txcustarabic.Text));
        arr.Add(new cArrayList("@cust_sn", txsnname.Text));
        arr.Add(new cArrayList("@addr", txadderss.Text));
        arr.Add(new cArrayList("@city_cd", cblocation.SelectedValue.ToString()));
        arr.Add(new cArrayList("@district_cd", cbdistrict.SelectedValue.ToString()));
        bll.vUpdateMstCustomerByApps(arr);
        arr.Clear();
        arr.Add(new cArrayList("@audit_object", "tmst_customer"));
        arr.Add(new cArrayList("@audit_typ", "U"));
        arr.Add(new cArrayList("@reasn", txreason.Text));
        arr.Add(new cArrayList("@executedby", Request.Cookies["usr_id"].Value.ToString()));
        bll.vInsertAuditTrail(arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Customer has been successfully changed!','" + txcust.Text + "','success');", true);
    }
    protected void cblocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@loc_cd_parent", cblocation.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbdistrict, "sp_tmst_location_get", "loc_cd", "loc_nm", arr);
    }

    protected void cbcustcate_SelectedIndexChanged(object sender, EventArgs e)
    {
        System.Data.SqlClient.SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("custcate_cd", cbcustcate.SelectedValue.ToString()));
        //if (cbcustcate.SelectedValue.ToString() == "CASH")
        //{
        //    bll.vBindingComboToSp(ref cbtermpayment, "sp_paymenttermcashcustomer", "fld_valu", "fld_desc");
        //}
        //else
        //{
        //    bll.vBindingFieldValueToCombo(ref cbtermpayment, "payment_term");
        //}
        //bll.vBindingGridToSp(ref grdcate, "sp_tcustomercategory_doc_get", arr);
        //lbcate.Text = bll.vLookUp("select custcate_desc from tmst_customercategory where custcate_cd='" + cbcustcate.SelectedValue.ToString() + "'");
        bll.vGetMstCustomerCategory(arr, ref rs);
        while (rs.Read())
        {
            lblmincredit.Text = rs["min_creditlimit"].ToString();
            lblmaxcredit.Text = rs["max_creditlimit"].ToString();
        }
        rs.Close();
    }

}