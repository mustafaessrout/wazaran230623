using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_reqcust : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingFieldValueToCombo(ref cbcustgroup, "otlcd");
            bll.vBindingComboToSp(ref cbcusttyp, "sp_tmst_customercategory_get", "cuscate_cd", "custcate_nm");
            bll.vBindingFieldValueToCombo(ref cbterm, "payment_term");
            cbcusttyp_SelectedIndexChanged(sender, e);
            dtreg.Text = System.DateTime.Today.ToShortDateString();
        }
    }
    protected void cbcusttyp_SelectedIndexChanged(object sender, EventArgs e)
    {
        System.Data.SqlClient.SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cuscate_cd", cbcusttyp.SelectedValue.ToString()));
        bll.vGetMstCustomerCategory(arr, ref rs);
        while (rs.Read())
        { 
            lbcredit.Text = rs["custcate_desc"].ToString();
            RangeValidator1.MinimumValue = rs["min_creditlimit"].ToString();
            RangeValidator1.MaximumValue = rs["max_creditlimit"].ToString();
        } rs.Close();
       
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        System.Data.SqlClient.SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cust_cd", txcustcode.Text));
        arr.Add(new cArrayList("@cust_nm", txcustname.Text));
        arr.Add(new cArrayList("@cust_arabic", txarabic.Text));
        arr.Add(new cArrayList("@cust_typ", cbcusttyp.SelectedValue.ToString()));
        arr.Add(new cArrayList("@otlcd", cbcustgroup.SelectedValue.ToString()));
        arr.Add(new cArrayList("@payment_term", cbterm.SelectedValue.ToString()));
        arr.Add(new cArrayList("@request_dt", dtreg.Text));
        arr.Add(new cArrayList("@cl", txCL.Text));
        arr.Add(new cArrayList("@requestor", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vInsertRequestCustomer(arr);
        arr.Clear(); string sMobileNo = "";
        arr.Add(new cArrayList("@doc_typ", "customer"));
        arr.Add(new cArrayList("@emp_cd", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@cust_cd", txcustcode.Text));
        arr.Add(new cArrayList("@app_sta_id", "D"));
        bll.vGetWrkApproval(arr, ref rs);
        while (rs.Read())
        {
            sMobileNo = rs["mobile_no"].ToString();
        } rs.Close();
        if (sMobileNo != "")
        {
            cd.sDocType = "customer";
            cd.vSendSms("New Cust :" + txcustcode.Text + "-" + txcustname.Text + ",CL:" + txCL.Text + ", Term:" + cbterm.SelectedItem.Text + " created, please review , do you want to approve (Yes/No), reply with Y or N !", sMobileNo);
        }
        // csms = null;
        Response.Redirect("fm_mstcustomerlist.aspx");
        
    }
}