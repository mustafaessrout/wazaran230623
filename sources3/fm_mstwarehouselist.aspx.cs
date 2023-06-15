using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_mstwarehouselist : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {

                System.Data.SqlClient.SqlDataReader rs = null;
                List<cArrayList> arr = new List<cArrayList>();
                //    //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //      bll.vBindingGridToSp(ref grd, "sp_tmst_warehouse_get");
                //  arr.Add(new cArrayList("@salespointcd",  ))
                bll.vBindingFieldValueToCombo(ref cbstocktype, "stock_typ");
                bll.vBindingFieldValueToCombo(ref cblevel, "whs_level_no");
                arr.Add(new cArrayList("@level_no", 3));
                bll.vBindingComboToSp(ref cbcity, "sp_tmst_location_get", "loc_cd", "loc_nm", arr);
                bll.vGetMstSalespoint(ref rs);
                while (rs.Read())
                {
                    TreeNode tnode = new TreeNode();
                    tnode.Text = rs["salespoint_nm"].ToString();
                    tnode.Value = rs["salespointcd"].ToString();
                    tvw.Nodes.Add(tnode);
                }
                rs.Close();
                bll.vBindingComboToSp(ref cbBranch, "sp_tmst_salespoint_get", "salespointcd", "salespoint_desc");
                cbBranch_SelectedIndexChanged(sender, e);
            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstwarehouselist");
                Response.Redirect("fm_ErrorPage.aspx");
            }
        }
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
    }
    protected void tvw_SelectedNodeChanged(object sender, EventArgs e)
    {
        try
        {

            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", tvw.SelectedValue.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_tmst_warehouse_get", arr);
            bll.vBindingComboToSp(ref cbparent, "sp_tmst_warehouse_get", "whs_cd", "whs_nm", arr);
            txwhscode.Focus();
            cbBranch.SelectedValue = tvw.SelectedValue.ToString();
            grddtl.DataSource = null;
            grddtl.DataBind();
            //  cbsalespoint.SelectedValue = tvw.SelectedValue.ToString();
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstwarehouselist");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lbcode = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbcode");
    //    txcode.Text = lbcode.Text;
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        //List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@whs_cd", txcode.Text));
        //arr.Add(new cArrayList("@whs_nm", txwhsname.Text));
        //arr.Add(new cArrayList("@stock_typ", cbstocktyp.SelectedValue.ToString()));
        //arr.Add(new cArrayList("@parent_whs_cd", cbparent.SelectedValue.ToString()));
        //arr.Add(new cArrayList("@addr", txaddr.Text));
        //arr.Add(new cArrayList("@loc_cd", cbloc.SelectedValue.ToString()));
        //arr.Add(new cArrayList("@level_no", cblevelno.SelectedValue.ToString()));
        //arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        //arr.Add(new cArrayList("@deleted", "0"));
        //bll.vInsertMstWarehouse(arr); arr.Clear();
        //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        //bll.vBindingGridToSp(ref grd, "sp_tmst_warehouse_get", arr);
    }
    protected void grd_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            Label lbwhscode = (Label)grd.SelectedRow.FindControl("lbcode");
            arr.Add(new cArrayList("@whs_cd", lbwhscode.Text));
            bll.vBindingGridToSp(ref grddtl, "sp_twarehouse_bin_get", arr);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstwarehouselist");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_mstwareshouseentry.aspx");
    }
    protected void btaddwhs_Click(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@whs_cd", txwhscode.Text));
            arr.Add(new cArrayList("@whs_nm", txwhsname.Text));
            arr.Add(new cArrayList("@salespointcd", tvw.SelectedNode.Value));
            arr.Add(new cArrayList("@parent_whs_cd", cbparent.SelectedValue.ToString()));
            arr.Add(new cArrayList("@loc_cd", cbcity.SelectedValue.ToString()));
            arr.Add(new cArrayList("@level_no", cblevel.SelectedValue.ToString()));
            arr.Add(new cArrayList("@addr", txaddress.Text));
            arr.Add(new cArrayList("@deleted", 0));
            bll.vInsertMstWarehouse(arr); arr.Clear();
            arr.Add(new cArrayList("@salespointcd", tvw.SelectedValue.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_tmst_warehouse_get", arr);
            bll.vBindingComboToSp(ref cbparent, "sp_tmst_warehouse_get", "whs_cd", "whs_nm", arr);

            txwhscode.Focus();

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstwarehouselist");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btaddbin_Click(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            Label lbwhscode = (Label)grd.SelectedRow.FindControl("lbcode");
            arr.Add(new cArrayList("@whs_cd", lbwhscode.Text));
            arr.Add(new cArrayList("@bin_cd", txcode.Text));
            arr.Add(new cArrayList("@bin_nm", txname.Text));
            arr.Add(new cArrayList("@stock_typ", cbstocktype.SelectedValue.ToString()));
            arr.Add(new cArrayList("@last_stock", txlaststock.Text));
            bll.vInsertWarehouseBin(arr); arr.Clear();
            arr.Add(new cArrayList("@whs_cd", lbwhscode.Text));
            bll.vBindingGridToSp(ref grddtl, "sp_twarehouse_bin_get", arr);

            txcode.Focus();

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstwarehouselist");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void cbBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", cbBranch.SelectedValue.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_tmst_warehouse_get", arr);
            bll.vBindingComboToSp(ref cbparent, "sp_tmst_warehouse_get", "whs_cd", "whs_nm", arr);
            grddtl.DataSource = null;
            grddtl.DataBind();

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstwarehouselist");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
}