using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lookupreqdiscount_ho : System.Web.UI.Page
{
    cbll bll = new cbll();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@level_no", "3"));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@data", "PRODUCT"));
            bll.vBindingComboToSp(ref cbproduct, "sp_tmst_product_get_spv", "prod_cd", "prod_nm", arr);
            arr.Clear();
            arr.Add(new cArrayList("@level_no", "3"));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@data", "SUPERVISOR"));
            bll.vBindingComboToSp(ref cbsupervisor, "sp_tmst_product_get_spv", "emp_cd", "emp_nm", arr);
            arr.Clear();
            bll.vBindingComboToSp(ref cbbranch, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm", arr);
            cbproduct.Items.Insert(0, new ListItem("ALL PRODUCT","ALL"));
            cbsupervisor.Items.Insert(0, new ListItem("ALL SUPERVISOR","ALL"));
            cbbranch.Items.Remove(cbbranch.Items.FindByValue("0"));
            cbbranch.Items.Insert(0, new ListItem("ALL BRANCH","ALL"));
            cbstatus.SelectedValue = "N";
            vBindingGrid();
        }
    }

    void vBindingGrid()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@promo_no", txsearch.Text));
        arr.Add(new cArrayList("@status", cbstatus.SelectedValue.ToString()));
        arr.Add(new cArrayList("@branch", cbbranch.SelectedValue.ToString()));
        arr.Add(new cArrayList("@product", cbproduct.SelectedValue.ToString()));
        arr.Add(new cArrayList("@supervisor", cbsupervisor.SelectedValue.ToString()));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tmst_reqdiscount_get", arr);
    }

    protected void btsearch_Click(object sender, EventArgs e)
    {
        vBindingGrid();
    }
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd.PageIndex = e.NewPageIndex;
        vBindingGrid();
    }
    protected void cbstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        vBindingGrid();
    }
    protected void cbproduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        vBindingGrid();
    }
    protected void cbbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        vBindingGrid();
    }
    protected void cbsupervisor_SelectedIndexChanged(object sender, EventArgs e)
    {
        vBindingGrid();
    }
}   