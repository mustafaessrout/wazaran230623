using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class eis_fm_contribute2 : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            bll.vBindingComboToSp(ref cbarea, "sp_tmst_region_getbyall", "region_cd", "region_nm");
            cbarea_SelectedIndexChanged(sender, e);
            //bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_getall","salespointcd", "salespoint_nm", arr);
            arr.Add(new cArrayList("@level_no", 2));
            bll.vBindingComboToSp(ref cbprod, "sp_tmst_product_getbylevel", "prod_cd", "prod_nm", arr);
            cbprod_SelectedIndexChanged(sender, e);
        }
    }
    protected void cbprod_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@prod_cd_parent", cbprod.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbitem, "sp_tmst_item_getbylevel2", "item_cd", "item_desc", arr);
    }
    protected void btview_Click(object sender, EventArgs e)
    {
        if (dtstart.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('Start Date can not empty!','Select Start Date');", true);
            return;
        }

        if (dtend.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('End Date can not empty!','Select End Date');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@region_cd", cbarea.SelectedValue.ToString()));
        arr.Add(new cArrayList("@start_dt", System.DateTime.ParseExact(dtstart.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@end_dt", System.DateTime.ParseExact(dtend.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@prod_cd", cbprod.SelectedValue.ToString()));
        arr.Add(new cArrayList("@item_cd", cbitem.SelectedValue.ToString()));
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_eis_contribute2", arr);
    }
    protected void cbarea_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@region_cd", cbarea.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salesregion_getbyall", "salespointcd", "salespoint_nm", arr);
    }
}