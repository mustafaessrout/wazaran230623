using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class vendor_entry : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           // bll.vBindingComboToSp(ref cbcity, "sp_tmst_city_get", "city_cd", "city_nm");
            bll.vBindingFieldValueToCombo(ref cbcurr, "curr_cd");
            bll.vBindingComboToSp(ref cbgroup, "sp_tmst_group_vendor_get", "grp_cd", "grp_nm");
             
        }
    }
    protected void btSave_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@vendor_cd", txcode.Text));
        arr.Add(new cArrayList("@vendor_nm", txname.Text));
        arr.Add(new cArrayList("@check_nm", txchkname.Text));
        arr.Add(new cArrayList("@contact", txcontact.Text));
        arr.Add(new cArrayList("@address1", txaddr1.Text));
        arr.Add(new cArrayList("@address2", txaddr2.Text));
        arr.Add(new cArrayList("@address3", txaddr3.Text));
        arr.Add(new cArrayList("@city_cd", cbcity.SelectedValue.ToString()));
        arr.Add(new cArrayList("@post_code", txpostcode.Text));

    }
}