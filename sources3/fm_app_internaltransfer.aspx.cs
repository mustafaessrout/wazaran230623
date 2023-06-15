using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_app_internaltransfer : System.Web.UI.Page
{
    cbll bll = new cbll();
    decimal totalQty = 0;
    decimal totalQtyCtn = 0, totalQtyPcs = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindinggrd();
            cd.v_showcontrol(btapprove);
            cd.v_showcontrol(btcancel);
        }
    }

    private void bindinggrd()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@sta_id", "W"));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBindingGridToSp(ref grdtrf, "sp_app_internaltransfer_get", arr);
        grdtrfdtl.DataSource = null;
        grdtrfdtl.DataBind();
    }

    protected void btapprove_Click(object sender, EventArgs e)
    {
        if (grdtrf.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), 
                "sweetAlert('No internal transfer approval pending !','No data','warning');", true);
            return;
        }

        foreach(GridViewRow _row in grdtrf.Rows)
        {
            if (_row.RowType == DataControlRowType.DataRow)
            {
                CheckBox _chk = (CheckBox)_row.FindControl("chk");
                if (_chk.Checked)
                {
                    Label lbtransferno = (Label)_row.FindControl("lb_trf_no");
                    string _sql = "update tinternal_transfer set sta_id='A' where trf_no='"+lbtransferno.Text+"'";
                    bll.vExecuteSQL(_sql);
                }
            }
        }
        bindinggrd();
    
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Internal Transfer has been approved','Internal Transfer Request','success');", true);
        //try
        //{
        //    List<cArrayList> arr = new List<cArrayList>();
        //    foreach (GridViewRow row in grdtrf.Rows)
        //    {
        //        if (row.RowType == DataControlRowType.DataRow)
        //        {
        //            CheckBox chk = (CheckBox)row.FindControl("chk");

        //            if (chk.Checked)
        //            {
        //                Label lb_trf_no = (Label)row.FindControl("lb_trf_no");
        //                HiddenField hdsalespointcd = (HiddenField)row.FindControl("hdsalespointcd");
        //                Label lbfrf_typ = (Label)row.FindControl("lbfrf_typ");
        //                Label lbwhs_cd = (Label)row.FindControl("lbwhs_cd");
        //                Label lbbin_cd = (Label)row.FindControl("lbbin_cd");

        //                if (lbfrf_typ.Text != "Branch To Branch")
        //                {
        //                    foreach (GridViewRow rowdtl in grdtrfdtl.Rows)
        //                    {
        //                        HiddenField lbqty = (HiddenField)row.FindControl("lbqty");
        //                        HiddenField hdqty_ctn = (HiddenField)row.FindControl("hdqty_ctn");
        //                        HiddenField hdqty_pcs = (HiddenField)row.FindControl("hdqty_pcs");
        //                        Label lbitemcode = (Label)row.FindControl("lbitemcode");

        //                        double dStock = 0, dQty = 0, dQty2 = 0, dQtyTotal = 0;
        //                        string sStock = "", sStock_conv = "";

        //                        sStock = bll.vLookUp("select isnull(dbo.sfnGetStockBookingseries('" + Request.Cookies["sp"].Value + "','" + lbitemcode.Text + "','" + lbbin_cd.Text + "','" + lbwhs_cd.Text + "','" + lb_trf_no.Text + "'),0) ");

        //                        dStock = double.Parse(sStock);
        //                        dQty = double.Parse(hdqty_ctn.Value);
        //                        dQty2 = double.Parse(hdqty_pcs.Value);
        //                        dQtyTotal = dQty + (double.Parse(bll.vLookUp("select dbo.sfnUomQtyConv('" + lbitemcode.Text + "','PCS','CTN','" + dQty2 + "')")));
        //                        sStock_conv = bll.vLookUp("select dbo.fn_getqtyconv('" + lbitemcode.Text + "','CTN'," + dStock.ToString() + ")");


        //                        if (dQtyTotal < 0)
        //                        {
        //                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alr", "sweetAlert('Cant add Qty Minus','Item. " + lbitemcode.Text + "','Internal Transfer Request','warning');", true);
        //                            return;
        //                        }

        //                        if (dStock < dQtyTotal)
        //                        {
        //                            string smsgstockallert = "Stock is not enough ! " + lbitemcode.Text.ToString() + " Qty Stock :" + sStock_conv + " ";
        //                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alr", "sweetAlert('" + smsgstockallert + "' ,'Stock Not Enough','warning');", true);
        //                            return;
        //                        }

        //                    }
        //                }


        //                arr.Clear();
        //                arr.Add(new cArrayList("@trf_no", lb_trf_no.Text));
        //                //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        //                arr.Add(new cArrayList("@salespointcd", hdsalespointcd.Value.ToString()));
        //                arr.Add(new cArrayList("@sta_id", "A"));
        //                bll.vUpdateAppInternalTransfer(arr);
        //            }
        //        }
        //    }
        //    bindinggrd();
        //    grdtrfdtl.DataSource = null;
        //    grdtrfdtl.DataBind();
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alr", "sweetAlert('Internal Transfer has been approved','Internal Transfer Request','success');", true);
        //}
        //catch (Exception ex)
        //{
        //    Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
        //    bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_app_internaltransfer");
        //    Response.Redirect("fm_ErrorPage.aspx");
        //}

    }

    protected void btcancel_Click(object sender, EventArgs e)
    {
        foreach(GridViewRow _row in grdtrf.Rows)
        {
            if (_row.RowType == DataControlRowType.DataRow)
            {
                Label lbtransferno = (Label)_row.FindControl("lb_trf_no");
                string _sql = "update tinternal_transfer set sta_id='L' where trf_no='"+lbtransferno.Text+"'";
                bll.vExecuteSQL(_sql);

            }
        }
        bindinggrd();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), 
            "sweetAlert('Internal Transfer request has been rejected!','Rejected Succeeded','success');", true);
        //try
        //{

        //    List<cArrayList> arr = new List<cArrayList>();
        //    foreach (GridViewRow row in grdtrf.Rows)
        //    {
        //        if (row.RowType == DataControlRowType.DataRow)
        //        {
        //            CheckBox chk = (CheckBox)row.FindControl("chk");
        //            if (chk.Checked)
        //            {
        //                Label lb_trf_no = (Label)row.FindControl("lb_trf_no");
        //                HiddenField hdsalespointcd = (HiddenField)row.FindControl("hdsalespointcd");
        //                arr.Clear();
        //                arr.Add(new cArrayList("@trf_no", lb_trf_no.Text));
        //                //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        //                arr.Add(new cArrayList("@salespointcd", hdsalespointcd.Value.ToString()));
        //                arr.Add(new cArrayList("@sta_id", "L"));
        //                bll.vUpdateAppInternalTransfer(arr);
        //            }
        //        }
        //    }
        //    bindinggrd();
        //    grdtrfdtl.DataSource = null;
        //    grdtrfdtl.DataBind();
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alr", "sweetAlert('Internal Transfer has been rejected','Internal Transfer Request','success');", true);
        //}
        //catch (Exception ex)
        //{
        //    Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
        //    bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_app_internaltransfer");
        //    Response.Redirect("fm_ErrorPage.aspx");
        //}
    }

    protected void grdtrfdtl_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdtrfdtl.EditIndex = -1;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@trf_no", hdtrfno.Value.ToString()));
        arr.Add(new cArrayList("@salespointcd", hdsalespoint.Value.ToString()));
        //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBindingGridToSp(ref grdtrfdtl, "sp_app_internaltransfer_dtl_get", arr);
    }

    protected void grdtrfdtl_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdtrfdtl.EditIndex = e.NewEditIndex;
        Label lb_trf_no = (Label)grdtrf.SelectedRow.FindControl("lb_trf_no");
        HiddenField hdsalespointcd = (HiddenField)grdtrf.SelectedRow.FindControl("hdsalespointcd");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@trf_no", lb_trf_no.Text));
        arr.Add(new cArrayList("@salespointcd", hdsalespointcd.Value));
        //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBindingGridToSp(ref grdtrfdtl, "sp_app_internaltransfer_dtl_get", arr);
    }

    protected void grdtrfdtl_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Label lbitemcode = (Label)grdtrfdtl.Rows[e.RowIndex].FindControl("lbitemcode");
        TextBox txqty = (TextBox)grdtrfdtl.Rows[e.RowIndex].FindControl("txqty");
        Label lb_trf_no = (Label)grdtrf.SelectedRow.FindControl("lb_trf_no");
        HiddenField hdsalespointcd = (HiddenField)grdtrf.SelectedRow.FindControl("hdsalespointcd");
        Label lbbincode = (Label)grdtrf.SelectedRow.FindControl("lbbin_cd");
        Label lbwarehouse = (Label)grdtrf.SelectedRow.FindControl("lbwhs_cd");
        Label lbsalesman = (Label)grdtrf.SelectedRow.FindControl("lbsalesman_cd");
        double dQty = 0;
        //string sPeriod = bll.sGetControlParameterSalespoint("period", Request.Cookies["sp"].Value.ToString());
        string sPeriod = bll.sGetControlParameterSalespoint("period", hdsalespointcd.Value.ToString());
        if (!double.TryParse(txqty.Text, out dQty))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Qty edit must numeric!','Pls correct value qty','warning');", true);
            return;
        }
        //string sGetStockCurrent = bll.vLookUp("select dbo.sfnGetStockBooking('" + Request.Cookies["sp"].Value.ToString() + "','" + lbitemcode.Text + "','" + lbbincode.Text + "','" + lbwarehouse.Text + "','0', dbo.fn_getsystemdate('"+ Request.Cookies["sp"].Value.ToString() + "'))");
        string sGetStockCurrent = bll.vLookUp("select dbo.sfnGetStockBooking('" + hdsalespointcd.Value.ToString() + "','" + lbitemcode.Text + "','" + lbbincode.Text + "','" + lbwarehouse.Text + "','0', dbo.fn_getsystemdate('" + hdsalespointcd.Value.ToString() + "'))");
        string sProdCode = bll.vLookUp("select prod_cd_parent from tmst_product where prod_cd=(select prod_cd from tmst_item where item_cd='" + lbitemcode.Text + "')");

        //if (sGetStockCurrent == "") { sGetStockCurrent = "0"; }
        int nPriority = Convert.ToInt16(bll.vLookUp("select count(1) from tsalestargetsalespoint where period='" + sPeriod + "' and prod_cd='" + sProdCode + "' and salesman_cd='" + lbsalesman.Text + "'"));
        if (nPriority == 1)
        {
            if (dQty < Convert.ToDouble(sGetStockCurrent))
            {
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Qty can not be edited because stock is enough!','Item priority','warning');", true);
                //return;
            }
        }

        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@trf_no", lb_trf_no.Text));
        //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@salespointcd", hdsalespointcd.Value.ToString()));
        arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
        arr.Add(new cArrayList("@qty", txqty.Text));
        bll.vUpdateAppInternalTransferDtl(arr);
        grdtrfdtl.EditIndex = -1;
        arr.Clear();
        arr.Add(new cArrayList("@trf_no", lb_trf_no.Text));
        //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@salespointcd", hdsalespointcd.Value.ToString()));
        bll.vBindingGridToSp(ref grdtrfdtl, "sp_app_internaltransfer_dtl_get", arr);
    }

    protected void grdtrfdtl_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lb_trf_no = (Label)grdtrf.Rows[e.NewSelectedIndex].FindControl("lb_trf_no");
        HiddenField hdsalespointcd = (HiddenField)grdtrf.Rows[e.NewSelectedIndex].FindControl("hdsalespointcd");
        hdtrfno.Value = lb_trf_no.Text;
        hdsalespoint.Value = hdsalespointcd.Value.ToString();
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@trf_no", hdtrfno.Value.ToString()));
        //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@salespointcd", hdsalespoint.Value.ToString()));
        bll.vBindingGridToSp(ref grdtrfdtl, "sp_app_internaltransfer_dtl_get", arr);
    }

    protected void grdtrfdtl_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {   
            HiddenField hdqty_ctn = (HiddenField)e.Row.FindControl("hdqty_ctn");
            HiddenField hdqty_pcs = (HiddenField)e.Row.FindControl("hdqty_pcs");
            Label lbitemcode = (Label)e.Row.FindControl("lbitemcode");

            totalQtyCtn = totalQtyCtn + decimal.Parse(hdqty_ctn.Value);
            totalQtyPcs = totalQtyPcs + decimal.Parse(hdqty_pcs.Value);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblTotalqty = (Label)e.Row.FindControl("lblTotalqty");
            lblTotalqty.Text = totalQtyCtn.ToString() + " CTN, " + totalQtyPcs.ToString() + " PCS";
        }

    }

    protected void grdtrf_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdtrf.PageIndex = e.NewPageIndex;
        bindinggrd();
    }

    protected void grdtrf_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lb_trf_no = (Label)grdtrf.Rows[e.NewSelectedIndex].FindControl("lb_trf_no");
        HiddenField hdsalespointcd = (HiddenField)grdtrf.Rows[e.NewSelectedIndex].FindControl("hdsalespointcd");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@trf_no", lb_trf_no.Text));
        arr.Add(new cArrayList("@salespointcd", hdsalespointcd.Value.ToString()));
        //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBindingGridToSp(ref grdtrfdtl, "sp_app_internaltransfer_dtl_get", arr);
    }
}