using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lookup_mst_asset : System.Web.UI.Page
{
    cbll bll = new cbll();
    cdal cdl = new cdal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindControl();
        }
    }

    private void bindControl()
    {
        List<cArrayList> arr = new List<cArrayList>();
        bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespointSN_get", "salespointcd", "salespoint_desc", arr);
        var wadiItem = cbsalespoint.Items[cbsalespoint.Items.Count - 2];
        cbsalespoint.Items.Remove(wadiItem);

        //bll.vBindingFieldValueToComboWithALL(ref cbAsset_type, "acc_asset_type");

        DataTable dt = new DataTable();
        arr.Add(new cArrayList("@fld_nm", "acc_asset_type"));
        dt = cdl.GetValueFromSP("sp_tfield_valueWithfld_get", arr);
        cbAsset_type.DataSource = dt;
        cbAsset_type.DataValueField = "fld_valu";
        cbAsset_type.DataTextField = "fld_desc";
        cbAsset_type.DataBind();

        var firstitem = cbAsset_type.Items[0];

        //cbAsset_type.Items.Clear();
        cbAsset_type.Items.Remove(firstitem);
        BindGrid();
    }

    protected void btsearch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@salespointcd", Convert.ToString(cbsalespoint.SelectedValue)));
        arr.Add(new cArrayList("@asset_typ", Convert.ToString(cbAsset_type.SelectedValue)));
        bll.vBindingGridToSp(ref grd, "Sp_tacc_mst_asset_get", arr);
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lblAssetno = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lblAssetno");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "window.opener.SelectData('" + lblAssetno.Text + "');window.close();", true);
    }
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd.PageIndex = e.NewPageIndex;
        List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@salespointcd", Convert.ToString(cbsalespoint.SelectedValue)));
        arr.Add(new cArrayList("@asset_typ", Convert.ToString(cbAsset_type.SelectedValue)));
        bll.vBindingGridToSp(ref grd, "Sp_tacc_mst_asset_get", arr);
    }
    private void BindGrid()
    {
        List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@salespointcd", Convert.ToString(cbsalespoint.SelectedValue)));
        arr.Add(new cArrayList("@asset_typ", Convert.ToString(cbAsset_type.SelectedValue)));
        bll.vBindingGridToSp(ref grd, "Sp_tacc_mst_asset_get", arr);
    }
}