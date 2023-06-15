using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_tabinternaltransfer : System.Web.UI.Page
{
    cbll bll = new cbll();
    decimal totalQty = 0, totalStock = 0;
    decimal totalQtyCtn = 0, totalQtyPcs = 0;
    decimal totalStockQtyCtn = 0, totalStockQtyPcs = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                bll.vBindingFieldValueToComboWithChoosen(ref cbstatus, "tab_sta_id");
                cbstatus_SelectedIndexChanged(sender, e);
                cd.v_hiddencontrol(btpostphone);
                cd.v_hiddencontrol(btcancel);
                cd.v_hiddencontrol(btapply);
            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_tabinternaltransfer");
                Response.Redirect("fm_ErrorPage.aspx");
            }
        }
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tablePageCopy", "tablePageCopy();", true);

    }
    protected void grdtab_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            Label lbtab_trf_no = (Label)grdtab.Rows[e.NewSelectedIndex].FindControl("lbtab_trf_no");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@tab_trf_no", lbtab_trf_no.Text));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grdtabdtl, "sp_ttab_internaltransfer_dtl_get", arr);
            if (cbstatus.SelectedValue == "TRF" || cbstatus.SelectedValue == "DEL")
            {
                cd.v_disablecontrol(grdtabdtl);
                cd.v_hiddencontrol(btapply);
                cd.v_hiddencontrol(btpostphone);
                cd.v_hiddencontrol(btcancel);
            }else if (cbstatus.SelectedValue == "NTRF")
            {
                cd.v_enablecontrol(grdtabdtl);
                cd.v_showcontrol(btapply);
                cd.v_showcontrol(btpostphone);
                cd.v_showcontrol(btcancel);
            }

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_tabinternaltransfer");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void btapply_Click(object sender, EventArgs e)
    {
        try
        {
            string sTrfNo = "";
            //int nCount = 0;
            string sReturNo = "";

            if (grdtabdtl.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('No device internal transfer to be transferred!','No data selected found','warning');", true);
                return;
            }
            bool _check = false;
            foreach(GridViewRow row in grdtab.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chk = (CheckBox)row.FindControl("chk");
                    if (chk.Checked)
                    {
                        _check = true;
                    }
                }
            }
            if (!_check)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('No data selected!','Please check selection','warning');", true);
                return;
            }
            foreach (GridViewRow row in grdtab.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chk");
                Label lbsalespointcd = (Label)row.FindControl("lbsalespointcd");
                Label lbtrf_dt = (Label)row.FindControl("lbtrf_dt");
                Label lbfrf_typ = (Label)row.FindControl("lbfrf_typ");
                Label lbwhs_cd = (Label)row.FindControl("lbwhs_cd");
                Label lbbin_cd = (Label)row.FindControl("lbbin_cd");
                Label lbvhc_cd = (Label)row.FindControl("lbvhc_cd");
                Label lbbin_cd_vhc = (Label)row.FindControl("lbbin_cd_vhc");
                Label lbmanual_no = (Label)row.FindControl("lbmanual_no");
                Label lbtab_trf_no = (Label)row.FindControl("lbtab_trf_no");

                if (chk.Checked == true)
                {
                    //try
                    //{

                    string swhs_cd_fr, sbin_cd_fr, swhs_cd_to, sbin_cd_to;
                    if (bll.vLookUp("select fld_valu from tfield_value where fld_nm='trf_typ' and fld_desc='" + lbfrf_typ.Text + "'") == "I")
                    {
                        swhs_cd_fr = lbwhs_cd.Text;
                        sbin_cd_fr = lbbin_cd.Text;
                        swhs_cd_to = lbvhc_cd.Text;
                        sbin_cd_to = lbbin_cd_vhc.Text;
                    }
                    else
                    {
                        swhs_cd_fr = lbvhc_cd.Text;
                        sbin_cd_fr = lbbin_cd_vhc.Text;
                        swhs_cd_to = lbwhs_cd.Text;
                        sbin_cd_to = lbbin_cd.Text;
                    }
                    string sMsgdupIT = bll.vLookUp("select dbo.fn_checkdupITtab('" + lbtab_trf_no.Text + "','" + Request.Cookies["sp"].Value.ToString() + "')");
                    if (sMsgdupIT != "ok")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Internal transfer number ','" + sMsgdupIT + "' already transfer to backoffice,'warning');", true);
                        return;
                    }
                    Boolean stockAvb = true;
                    string ssta_id;
                    string sWarn = bll.vLookUp("select dbo.fn_checkavlstockIT('" + lbtab_trf_no.Text + "','" + Request.Cookies["sp"].Value.ToString() + "')");
                    if (sWarn != "ok")
                    {
                        stockAvb = false;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('" + sWarn + "','" + lbtab_trf_no.Text + "','warning');", true);
                        return;
                    }
                    //if (stockAvb) { ssta_id = "A"; } else { ssta_id = "W"; }
                    string _trftype = bll.vLookUp("select fld_valu from tfield_value where fld_nm='trf_typ' and fld_desc='" + lbfrf_typ.Text + "'");
                    if (_trftype == "V" || _trftype == "I")
                    {
                        ssta_id = "N";
                    }
                    else
                    {
                        ssta_id = "W";
                    }
                    List<cArrayList> arr = new List<cArrayList>();
                    arr.Add(new cArrayList("@salespointcd", lbsalespointcd.Text));
                    arr.Add(new cArrayList("@trf_dt", DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                    arr.Add(new cArrayList("@trf_typ", bll.vLookUp("select fld_valu from tfield_value where fld_nm='trf_typ' and fld_desc='" + lbfrf_typ.Text + "'")));
                    arr.Add(new cArrayList("@whs_cd_from", swhs_cd_fr));
                    arr.Add(new cArrayList("@bin_from", sbin_cd_fr));
                    arr.Add(new cArrayList("@whs_cd_to", swhs_cd_to));
                    arr.Add(new cArrayList("@bin_to", sbin_cd_to));
                    arr.Add(new cArrayList("@item_cd", ""));
                    arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
                    arr.Add(new cArrayList("@manual_no", lbmanual_no.Text));
                    arr.Add(new cArrayList("@tab_no", lbtab_trf_no.Text));
                    arr.Add(new cArrayList("@prevstk", false));
                    arr.Add(new cArrayList("@sta_id", ssta_id));
                    //  bll.vLookUp("select dbo.fn_checkavlstock('"+lbitemcode.Text+"','"+swhs_cd_fr+"','"+sbin_cd_fr+"',"++")");
                    bll.vInsertInternalTransfer(arr, ref sTrfNo);
                    arr.Clear();
                    arr.Add(new cArrayList("@salespointcd", lbsalespointcd.Text));
                    arr.Add(new cArrayList("@trf_no", sTrfNo));
                    arr.Add(new cArrayList("@tab_trf_no", lbtab_trf_no.Text));
                    bll.vInserttab_tinternaltransfer_dtl(arr);
                    arr.Clear();
                    arr.Add(new cArrayList("@tab_trf_no", lbtab_trf_no.Text));
                    arr.Add(new cArrayList("@salespointcd", lbsalespointcd.Text));
                    bll.vUpdateTabInternalTransferByTrfDate(arr);
                    bindinggrdtab();
                    //}
                    //catch (Exception ex)
                    //{
                    //    //List<cArrayList> arr = new List<cArrayList>();
                    //    //arr.Add(new cArrayList("@err_source", "save Internal Transfer"));
                    //    //arr.Add(new cArrayList("@err_description", ex.Message.ToString()));
                    //    //bll.vInsertErrorLog(arr);
                    //    bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : btnapply_Click");
                    //}
                }

            }
            Session["lootrf_no"] = sTrfNo;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cls", "window.opener.RefreshData('" + sTrfNo + "');window.close();", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_tabinternaltransfer");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    private void bindinggrdtab()
    {
        try
        {

            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@tab_sta_id", cbstatus.SelectedValue));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
            bll.vBindingGridToSp(ref grdtab, "sp_ttab_internaltransfer_get", arr);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_tabinternaltransfer");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void cbstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            bindinggrdtab();
            grdtabdtl.DataSource = null;
            grdtabdtl.DataBind();

            if (cbstatus.SelectedValue == "TRF" || cbstatus.SelectedValue == "DEL")
            {
                //btapply.Visible = false;
                //btcancel.Visible = false;
                //btpostphone.Visible = false;
                cd.v_hiddencontrol(btapply);
                cd.v_hiddencontrol(btcancel);
                cd.v_hiddencontrol(btpostphone);
            }
            else
            {
                //btapply.Visible = true;
                //btcancel.Visible = true;
                //btpostphone.Visible = true;
                cd.v_showcontrol(btapply);
                cd.v_showcontrol(btcancel);
                cd.v_showcontrol(btpostphone);
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_tabinternaltransfer");
            Response.Redirect("fm_ErrorPage.aspx");
        }

    }


    protected void btcancel_Click(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            foreach (GridViewRow row in grdtab.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chk = (CheckBox)row.FindControl("chk");
                    if (chk.Checked)
                    {
                        Label lbtab_trf_no = (Label)row.FindControl("lbtab_trf_no");
                        arr.Clear();
                        arr.Add(new cArrayList("@tab_trf_no", lbtab_trf_no.Text));
                        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                        bll.vDelTTabInternalTransfer(arr);
                    }
                }
            }
            bindinggrdtab();
            grdtabdtl.DataSource = null;
            grdtabdtl.DataBind();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alr", "sweetAlert('Tab Internal Transfer has been deleted','Tablet Internal Transfer','success');", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_tabinternaltransfer");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btpostphone_Click(object sender, EventArgs e)
    {
        try
        {

            List<cArrayList> arr = new List<cArrayList>();
            foreach (GridViewRow row in grdtab.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chk = (CheckBox)row.FindControl("chk");
                    if (chk.Checked)
                    {
                        Label lbtab_trf_no = (Label)row.FindControl("lbtab_trf_no");
                        arr.Clear();
                        arr.Add(new cArrayList("@tab_trf_no", lbtab_trf_no.Text));
                        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                        bll.vUpdateTTabInternalTransfer(arr);
                    }
                }
            }
            bindinggrdtab();
            grdtabdtl.DataSource = null;
            grdtabdtl.DataBind();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alr", "sweetAlert('Tab Internal Transfer has been postphone','To Next Day','success');", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_tabinternaltransfer");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grdtab_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdtab.PageIndex = e.NewPageIndex;
            bindinggrdtab();

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_tabinternaltransfer");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grdtabdtl_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            grdtabdtl.EditIndex = -1;
            //    Label lbtab_trf_no = (Label)grdtab.Rows[e.RowIndex].FindControl("lbtab_trf_no");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@tab_trf_no", hdtrfno.Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grdtabdtl, "sp_ttab_internaltransfer_dtl_get", arr);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_tabinternaltransfer");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grdtabdtl_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            grdtabdtl.EditIndex = e.NewEditIndex;
            // bindinggrdtab();
            Label lbtabretur_no = (Label)grdtab.SelectedRow.FindControl("lbtab_trf_no");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@tab_trf_no", lbtabretur_no.Text));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grdtabdtl, "sp_ttab_internaltransfer_dtl_get", arr);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_tabinternaltransfer");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grdtabdtl_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {

            Label lbtab_trf_no = (Label)grdtab.Rows[e.NewSelectedIndex].FindControl("lbtab_trf_no");
            hdtrfno.Value = lbtab_trf_no.Text;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@tab_trf_no", hdtrfno.Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grdtabdtl, "sp_ttab_internaltransfer_dtl_get", arr);
            //  hdreturno.Value = lbtabretur_no.Text;
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_tabinternaltransfer");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grdtabdtl_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            Label lbitemcode = (Label)grdtabdtl.Rows[e.RowIndex].FindControl("lbitemcode");
            TextBox txqty = (TextBox)grdtabdtl.Rows[e.RowIndex].FindControl("txqty");
            Label lbtabretur_no = (Label)grdtab.SelectedRow.FindControl("lbtab_trf_no");
            Label lbbincode = (Label)grdtab.SelectedRow.FindControl("lbbin_cd");
            Label lbwarehouse = (Label)grdtab.SelectedRow.FindControl("lbwhs_cd");
            Label lbsalesman = (Label)grdtab.SelectedRow.FindControl("lbsalesman_cd");
            HiddenField hdstockqty = (HiddenField)grdtabdtl.Rows[e.RowIndex].FindControl("hdstockqty");
            double dQty = 0;
            string sPeriod = bll.sGetControlParameterSalespoint("period", Request.Cookies["sp"].Value.ToString());
            if (!double.TryParse(txqty.Text, out dQty))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Qty edit must numeric!','Pls correct value qty','warning');", true);
                return;
            }
            string sGetStockCurrent = bll.vLookUp("select dbo.sfnGetStockBooking('" + Request.Cookies["sp"].Value.ToString() + "','" + lbitemcode.Text + "','" + lbbincode.Text + "','" + lbwarehouse.Text + "','0', dbo.fn_getsystemdate('" + Request.Cookies["sp"].Value.ToString() + "'))");
            string sProdCode = bll.vLookUp("select prod_cd_parent from tmst_product where prod_cd=(select prod_cd from tmst_item where item_cd='" + lbitemcode.Text + "')");

            //if (sGetStockCurrent == "") { sGetStockCurrent = "0"; }
            //int nPriority = Convert.ToInt16(bll.vLookUp("select count(1) from tsalestargetsalespoint where period='" + sPeriod + "' and prod_cd='" + sProdCode + "' and salesman_cd='" + lbsalesman.Text + "'"));
            //if (nPriority == 1)
            //{
            //    if (dQty < Convert.ToDouble(sGetStockCurrent))
            //    {
            //        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Qty can not be edited because stock is enough!','Item priority','warning');", true);
            //        //return;
            //    }
            //}
            double _stock = Convert.ToDouble(hdstockqty.Value);
            double _qty = Convert.ToDouble(txqty.Text);
            if (_qty > _stock)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Edit qty can not bigger than stock!','Check stock','warning');", true);
                return;
            }

            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@tab_trf_no", lbtabretur_no.Text));
            arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
            arr.Add(new cArrayList("@qty", txqty.Text));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vUpdateTTabInternalTransferDtl(arr);
            grdtabdtl.EditIndex = -1;
            arr.Clear();
            arr.Add(new cArrayList("@tab_trf_no", lbtabretur_no.Text));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grdtabdtl, "sp_ttab_internaltransfer_dtl_get", arr);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_tabinternaltransfer");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grdtabdtl_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdqty = (HiddenField)e.Row.FindControl("hdqty");
                HiddenField hdqty_ctn = (HiddenField)e.Row.FindControl("hdqty_ctn");
                HiddenField hdqty_pcs = (HiddenField)e.Row.FindControl("hdqty_pcs");
                HiddenField hdstockqty = (HiddenField)e.Row.FindControl("hdstockqty");
                HiddenField hdstockqty_ctn = (HiddenField)e.Row.FindControl("hdstockqty_ctn");
                HiddenField hdstockqty_pcs = (HiddenField)e.Row.FindControl("hdstockqty_pcs");
                Label lbitemcode = (Label)e.Row.FindControl("lbitemcode");

                totalQtyCtn = totalQtyCtn + decimal.Parse(hdqty_ctn.Value);
                totalQtyPcs = totalQtyPcs + decimal.Parse(hdqty_pcs.Value);
                totalStockQtyCtn = totalStockQtyCtn + decimal.Parse(hdstockqty_ctn.Value);
                totalStockQtyPcs = totalStockQtyPcs + decimal.Parse(hdstockqty_pcs.Value);
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalqty = (Label)e.Row.FindControl("lblTotalqty");
                Label lbtotstockavl = (Label)e.Row.FindControl("lbtotstockavl");
                lblTotalqty.Text = totalQtyCtn.ToString() + " CTN, " + totalQtyPcs.ToString() + " PCS";
                lbtotstockavl.Text = totalStockQtyCtn.ToString() + " CTN, " + totalStockQtyPcs.ToString() + " PCS";
            }

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_tabinternaltransfer");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
}