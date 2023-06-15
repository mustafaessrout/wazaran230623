using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_datarowproposal : System.Web.UI.Page
{
    cbll bll = new cbll();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingComboToSp(ref cbvendor, "sp_tmst_vendor_get", "vendor_cd", "vendor_nm");
            bll.vBindingFieldValueToCombo(ref cbmarketingcost, "rdcost");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@promo_cd", null));
            bll.vBindingComboToSp(ref cbpromotype, "sp_tpromotion_dtl_get", "promo_typ", "promotyp_nm", arr);
            arr.Clear();
            string TableName = "tmst_proposal", FieldName = "prop_year";
            arr.Add(new cArrayList("@TableName", TableName));
            arr.Add(new cArrayList("@FieldName", FieldName));
            bll.vBindingComboToSp(ref cbYear, "sp_tmst_period_byyear", FieldName, FieldName,arr);
            arr.Clear();
            cbpromotype.Items.Insert(0, "-All Promo-");
            cbmarketingcost.Items.Insert(0, "-All Cost-");
            cbvendor.Items.Insert(0, "-All Principal-");
            cbMonth.Items.Insert(0, "-All Month-");
            cbYear.Items.Insert(0, "-All Year-");
        }
    }

    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=darowprop&vendor=" + ((cbvendor.SelectedValue.ToString() == "-All Principal-") ? "ALL" : cbvendor.SelectedValue.ToString()) + "&cost=" + ((cbmarketingcost.SelectedValue.ToString() == "-All Cost-") ? "ALL" : cbmarketingcost.SelectedValue.ToString()) + "&promo=" + ((cbpromotype.SelectedValue.ToString() == "-All Promo-") ? "ALL" : cbpromotype.SelectedValue.ToString()) + "&year=" + ((cbYear.SelectedValue.ToString() == "-All Year-") ? "ALL" : cbYear.SelectedValue.ToString()) + "&month=" + ((cbMonth.SelectedValue.ToString() == "-All Month-") ? "ALL" : cbMonth.SelectedValue.ToString()) + "');", true);
    }
}