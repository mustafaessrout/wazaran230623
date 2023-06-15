using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class grp_vendor_entry : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btSave_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@grp_cd", txcode.Text));
        arr.Add(new cArrayList("@grp_nm", txname.Text));
        arr.Add(new cArrayList("@udf1", txopt1.Text));
        arr.Add(new cArrayList("@udf2", txopt2.Text));
        bll.vInsertMstGroupVendor(arr);
        Response.Redirect("grp_vendorlist.aspx");
    }
}