using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_rptstockcard : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingComboToSpWithEmptyChoosen(ref cbperiod, "sp_tmst_period_get", "period_cd", "period_nm");
            bll.vBindingComboToSpWithEmptyChoosen(ref cbitem, "sp_tmst_item_get", "item_cd", "item_nm");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
            bll.vBindingComboToSpWithEmptyChoosen(ref cbwhs, "sp_tmst_warehouse_get", "whs_cd", "whs_nm", arr);

        }
    }

    protected void cbwhs_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@whs_cd",cbwhs.SelectedValue));
        bll.vBindingComboToSpWithEmptyChoosen(ref cbbin, "sp_twarehouse_bin_get", "bin_cd", "bin_nm", arr);
    }

    protected void btsearch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@item_cd", cbitem.SelectedValue));
        arr.Add(new cArrayList("@whs_cd", cbwhs.SelectedValue));
        arr.Add(new cArrayList("@bin_cd", cbbin.SelectedValue));
        arr.Add(new cArrayList("@period", cbperiod.SelectedValue));
        bll.vBindingGridToSp(ref grd, "rptstockcard", arr);
    }

    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), 
            "openreport('fm_report2.aspx?src=stockcard&p="+cbperiod.SelectedValue+"&i="+cbitem.SelectedValue+
                "&w="+cbwhs.SelectedValue+"&b="+cbbin.SelectedValue+"');", true);
    }
}