using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_fm_claimcashoutmap : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            bll.vBindingFieldValueToCombo(ref cbpromokind, "promokind");
            cbpromokind_SelectedIndexChanged(sender, e);
            arr.Clear();
            bll.vBindingComboToSp(ref cbcashout, "sp_tmst_itemcashout_get", "itemco_cd", "itemco_nm", arr);
        }
    }
    protected void cbpromogroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@promo_cd", cbpromogroup.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbpromotype, "sp_tpromotion_dtl_get", "promo_typ", "promotyp_nm", arr);
        bindinggrid();
    }

    void bindinggrid()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@promo_cd", cbpromogroup.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tpromotion_dtl_getbykind", arr);
    }
    protected void cbpromokind_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@promokind", cbpromokind.SelectedItem.Text));
        bll.vBindingComboToSp(ref cbpromogroup,"sp_tmst_promotion_get","promo_cd","promo_nm", arr);
        cbpromogroup_SelectedIndexChanged(sender, e);
        arr.Clear();
        arr.Add(new cArrayList("@cashout_typ", cbpromokind.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbcashout, "sp_tmst_itemcashout_get", "itemco_cd", "itemco_nm", arr);
       
    }
    protected void cbpromotype_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    protected void btupdate_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@itemco_cd", cbcashout.SelectedValue.ToString()));
        arr.Add(new cArrayList("@promo_cd", cbpromogroup.SelectedValue.ToString()));
        arr.Add(new cArrayList("@promo_typ", cbpromotype.SelectedValue.ToString()));
        bll.vUpdatePromotionDtlByItemCo(arr);
        bindinggrid();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Promotion detail updated','Update Successfully','warning');", true);
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lbpromocode = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbpromocode");
        Label lbpromotype = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbpromotype");
        string sItemCode = bll.vLookUp("select itemco_cd from tpromotion_dtl where promo_cd='"+lbpromocode.Text+"' and promo_typ='"+lbpromotype.Text+"'");
        if (sItemCode != "")
        {
            cbcashout.SelectedValue = sItemCode;
        }
    }
}