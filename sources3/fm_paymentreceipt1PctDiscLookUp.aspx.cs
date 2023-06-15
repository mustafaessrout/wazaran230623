using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_paymentreceipt1PctDiscLookUp : System.Web.UI.Page
{
    cbll bll = new cbll();
    cdal cdl = new cdal();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var cust_cd = Convert.ToString(Request.QueryString["cust_cd"]);
            var cusgrcd = Convert.ToString(Request.QueryString["cusgrcd"]);
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@cust_cd", cust_cd == "" ? null : cust_cd));
            arr.Add(new cArrayList("@cusgrcd", cusgrcd == "" ? null : cusgrcd));
            bll.vBindingGridToSp(ref grdInvoice, "sp_tinvoice_1pctDiscount_get", arr);
        }
    }
}