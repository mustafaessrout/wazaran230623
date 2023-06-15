using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_mstdisctypedoc : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@doc_typ", "PROMO"));
            bll.vBindingComboToSp(ref cbpromo, "sp_tmst_promotion_get", "promo_cd","promo_nm");
            bll.vBindingComboToSp(ref cbdocument, "sp_tmst_document_get", "doc_cd", "doc_nm", arr);
            bll.vBindingFieldValueToCombo(ref cbdic, "dic");
            cbdisctype_SelectedIndexChanged(sender, e);
        }
    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@promo_cd", cbpromo.SelectedValue.ToString()));
        arr.Add(new cArrayList("@doc_cd", cbdocument.SelectedValue.ToString()));
        arr.Add(new cArrayList("@dic", cbdic.SelectedValue.ToString()));
        arr.Add(new cArrayList("@promo_typ", cbpromotype.SelectedValue.ToString()));
        bll.vInsertPromotionDoc(arr);
        arr.Clear();
        arr.Add(new cArrayList("@promo_cd", cbpromo.SelectedValue.ToString()));
        arr.Add(new cArrayList("@promo_typ", cbpromotype.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tpromotion_doc_get", arr);
      
    }
    protected void cbdisctype_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@promo_cd", cbpromo.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbpromotype, "sp_tpromotion_dtl_get","promo_typ","promotyp_nm", arr);
        cbpromotype_SelectedIndexChanged(sender, e);
        
    }
    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        Label lbdisctype = (Label)grd.Rows[e.RowIndex].FindControl("lbdisctype");
        Label lbdoccode = (Label)grd.Rows[e.RowIndex].FindControl("lbdoccode");
        arr.Add(new cArrayList("@disc_typ", lbdisctype.Text));
        arr.Add(new cArrayList("@doc_cd", lbdoccode.Text));
        bll.vDeleteDocTypeDocument(arr); arr.Clear();
        arr.Add(new cArrayList("@promo_cd", cbpromo.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tpromotion_doc_get", arr);
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lbdisctype = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbdisctype");
        Label lbdoccode = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbdoccode");

    }
    protected void cbpromotype_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@promo_cd", cbpromo.SelectedValue.ToString()));
        arr.Add(new cArrayList("@promo_typ", cbpromotype.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tpromotion_doc_get", arr);

    }
}