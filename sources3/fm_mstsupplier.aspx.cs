using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_mstsupplier : System.Web.UI.Page
{
    cbll bll = new cbll();
    cbll2 bll2 = new cbll2();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cd.v_disablecontrol(txsuppliercode);
            cd.v_hiddencontrol(btsave);
            cd.v_hiddencontrol(btprint);
        }
    }


    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_mstsupplier.aspx");
    }

    protected void btsave_Click(object sender, EventArgs e)
    {
        string _suppliercode = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@supplier_nm", txsuppliername.Text));
        arr.Add(new cArrayList("@companyRegistration", txvatname.Text));
        arr.Add(new cArrayList("@supplier_addres", txsuppliername.Text));
        arr.Add(new cArrayList("@supplier_city", hdcity.Value));
        arr.Add(new cArrayList("@supplier_country", txcontactname.Text));
        arr.Add(new cArrayList("@supplier_contactNumber", ""));
        arr.Add(new cArrayList("@supplier_sta_id", "A"));
        arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value));
        arr.Add(new cArrayList("@supplierTax_no", txtaxno.Text));
        bll2.vInsertAccMstSupplier(arr);

    }
}