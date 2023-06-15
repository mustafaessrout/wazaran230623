using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_fm_custblock : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            bll.vBindingFieldValueToCombo(ref cbotlcd, "otlcd");
            arr.Add(new cArrayList("@fld_nm", "startblock"));
            bll.vBindingGridToSp(ref grd, "sp_tfield_value_get", arr);
        }
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        Label lbchannel = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbchannel");
        dtblock.Text = bll.vLookUp("select fld_desc from tfield_value where fld_nm='startblock' and fld_valu='"+lbchannel.Text+"'");
        cbotlcd.SelectedValue = lbchannel.Text;
    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@fld_nm", "startblock"));
        arr.Add(new cArrayList("@fld_valu", cbotlcd.SelectedValue.ToString()));
        arr.Add(new cArrayList("@fld_desc", dtblock.Text));
        bll.vUpdateFieldValue(arr);
        arr.Clear();
        arr.Add(new cArrayList("@fld_nm", "startblock"));
        bll.vBindingGridToSp(ref grd, "sp_tfield_value_get", arr);
    }
}