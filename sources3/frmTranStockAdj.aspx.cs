using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using AjaxControlToolkit;
public partial class frmTranStockAdj : System.Web.UI.Page
{
    cbll bll = new cbll();
    decimal totalQty = 0;
    decimal totalAmount = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                bll.vBindingFieldValueToCombo(ref cbuom, "uom_tf", "uom");
                txstkadjno.Text = "NEW";
                lbstatus.Text = bll.vLookUp("select fld_desc from tfield_value where fld_nm='sta_id' and fld_valu='N'");
                dtstkadjdate.Text = Request.Cookies["waz_dt"].Value.ToString();
                bll.vBindingComboToSp(ref cbSalesPointCD, "sp_tmst_salespoint_get", "salespointcd", "salespoint_desc");
                cbSalesPointCD.SelectedValue = Request.Cookies["sp"].Value.ToString();
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vBindingComboToSp(ref cbwhs_cd, "sp_tmst_warehouse_van_get", "whs_cd", "whs_nm", arr);

                bll.vBindingFieldValueToCombo(ref cbbin_cd_fr, "bin_cd_stkadj");
                bll.vBindingFieldValueToCombo(ref cbbin_cd_to, "bin_cd_stkadj");
                string _sql = "delete tblstockadjdtl where stkadjno='" + Request.Cookies["usr_id"].Value +"'";
                bll.vExecuteSQL(_sql ); 
                bindinggrd();
                cd.v_hiddencontrol(btsave);
                cd.v_hiddencontrol(btprint);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : frmTranStockAdj");
                Response.Redirect("fm_ErrorPage.aspx");
            }
        }
        
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmTranStockAdj.aspx");
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            lbstatusPnl.CssClass = "";
            if (lbstatus.Text == "COMPLETE")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction already complete can not be save','error');", true);
                lbstatusPnl.CssClass = "error";
                return;
            }
            dtstkadjdatePnl.CssClass = "";
            if (Request.Cookies["waz_dt"].Value.ToString() != dtstkadjdate.Text)
            {
                dtstkadjdatePnl.CssClass = "error";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction Date Out Of Periode Date','error');", true);
                return;
            }
            if (txstkadjno.Text == "NEW" || txstkadjno.Text == "")
            {
                string stkadjno = "";
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@stkadjdate", DateTime.ParseExact(dtstkadjdate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@whs_CD", cbwhs_cd.SelectedValue));
                //arr.Add(new cArrayList("@bin_cd", cbbin_cd.SelectedValue));
                arr.Add(new cArrayList("@createdtby", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@stkadjmanno", txstkadjmanno.Text));
                arr.Add(new cArrayList("@prevstk", chprevstk.Checked));
                bll.vInserttblstockadj(arr, ref stkadjno);
                txstkadjno.Text = stkadjno;

              
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Data Save successfully ..','Adjusment No. " + txstkadjno.Text + "','info');", true);
            }
            cd.v_showcontrol(btprint);
            cd.v_hiddencontrol(btsave);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : frmTranStockAdj");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        finally
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
        }
    }
    protected void btDelete_Click(object sender, EventArgs e)
    {

    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        try
        {
            string _sql = "update tblstockadj set sta_id='W' where stkadjno='"+txstkadjno.Text+"'";
            bll.vExecuteSQL(_sql);
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@stkadjno", txstkadjno.Text));
            arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue));
            Session["lParamstkadjno"] = arr;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=stkadj');", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : frmTranStockAdj");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        finally { ScriptManager.RegisterStartupScript(Page,Page.GetType(),Guid.NewGuid().ToString(),"HideProgress();",true); }
    }
    protected void bttmp_Click(object sender, EventArgs e)
    {
        try
        {

            SqlDataReader rs = null;
            string sstatus;
            txstkadjno.Text = Convert.ToString(Session["loostkadjno"]);
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@stkadjno", txstkadjno.Text));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vGettblstockadj(arr, ref rs);
            while (rs.Read())
            {
                string sdate = string.Format("{0:d/M/yyyy}", rs["stkadjdate"]);
                //DateTime dtdate = Convert.ToDateTime(sdate);
                //string strDate = dtdate.ToString("d/M/yyyy");
                sstatus = rs["sta_id"].ToString();
                dtstkadjdate.Text = sdate;
                cbSalesPointCD.SelectedValue = rs["salespointcd"].ToString();
                cbwhs_cd.SelectedValue = rs["whs_CD"].ToString();
                //cbbin_cd.SelectedValue = rs["bin_cd"].ToString();
                lbstatus.Text = bll.vLookUp("select fld_desc from tfield_value where fld_nm='sta_id' and fld_valu='" + sstatus + "'");
                txstkadjmanno.Text = rs["stkadjmanno"].ToString();
                string sprevstk = rs["prevstk"].ToString();
                if (sprevstk == "") { sprevstk = "false"; }
                chprevstk.Checked = Boolean.Parse(sprevstk);
            }
            rs.Close();
            bindinggrd();
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : frmTranStockAdj");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            grd.PageIndex = e.NewPageIndex;
            bindinggrd();

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : frmTranStockAdj");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grd.EditIndex = -1;
        bindinggrd();
    }
    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            lbstatusPnl.CssClass = "";

            if (lbstatus.Text == "COMPLETE")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction already complete can not be delete','error');", true);
                lbstatusPnl.CssClass = "error";
                return;
            }
            dtstkadjdatePnl.CssClass = "";
            if (Request.Cookies["waz_dt"].Value.ToString() != dtstkadjdate.Text)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction Date Out Of Periode Date','error');", true);
                dtstkadjdatePnl.CssClass = "error";
                return;
            }
            Label lblsalespointCD = (Label)grd.Rows[e.RowIndex].FindControl("lblsalespointCD");
            Label lblstkadjno = (Label)grd.Rows[e.RowIndex].FindControl("lblstkadjno");
            Label lbitem_cd_fr = (Label)grd.Rows[e.RowIndex].FindControl("lbitem_cd_fr");
            Label lbitem_cd_to = (Label)grd.Rows[e.RowIndex].FindControl("lbitem_cd_to");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@stkadjno", lblstkadjno.Text));
            arr.Add(new cArrayList("@salespointCD", lblsalespointCD.Text));
            arr.Add(new cArrayList("@item_cd_fr", lbitem_cd_fr.Text));
            arr.Add(new cArrayList("@item_cd_to", lbitem_cd_to.Text));
            bll.vDeletetblstockadjdtl(arr);
            bindinggrd();

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : frmTranStockAdj");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            dtstkadjdatePnl.CssClass = "";
            lbstatusPnl.CssClass = "";

            if (lbstatus.Text == "COMPLETE")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction already complete can not be edit','error');", true);
                lbstatusPnl.CssClass = "error";
                return;
            }
            if (Request.Cookies["waz_dt"].Value.ToString() != dtstkadjdate.Text)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction Date Out Of Periode Date','error');", true);
                dtstkadjdatePnl.CssClass = "error";
                grd.EditIndex = -1;
                bindinggrd();
                return;
            }
            grd.EditIndex = e.NewEditIndex;
            bindinggrd();

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : frmTranStockAdj");
            Response.Redirect("fm_ErrorPage.aspx");
        }
 
    }
    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            TextBox txtqty = (TextBox)grd.Rows[e.RowIndex].FindControl("txtqty");
            Label lblsalespointCD = (Label)grd.Rows[e.RowIndex].FindControl("lblsalespointCD");
            Label lblstkadjno = (Label)grd.Rows[e.RowIndex].FindControl("lblstkadjno");
            Label lbitem_cd_fr = (Label)grd.Rows[e.RowIndex].FindControl("lbitem_cd_fr");
            Label lbitem_cd_to = (Label)grd.Rows[e.RowIndex].FindControl("lbitem_cd_to");

            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@stkadjno", lblstkadjno.Text));
            arr.Add(new cArrayList("@salespointCD", lblsalespointCD.Text));
            arr.Add(new cArrayList("@item_cd_fr", lbitem_cd_fr.Text));
            arr.Add(new cArrayList("@item_cd_to", lbitem_cd_to.Text));
            arr.Add(new cArrayList("@qty", txtqty.Text));
            bll.vUpdatetblstockadjdtl(arr);
            grd.EditIndex = -1; arr.Clear();
            bindinggrd();

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : frmTranStockAdj");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void txitem_cd_fr_TextChanged(object sender, EventArgs e)
    {
        try
        {

            txprod_cd_fr.Text = bll.vLookUp("select prod_cd from tmst_item where item_cd='" + hditem_cd_fr.Value + "'");
            txpacking_fr.Text = bll.vLookUp("select packing from tmst_item where item_cd='" + hditem_cd_fr.Value + "'");
            //txunitprice_fr.Text = bll.vLookUp("select price_sell from tmst_item where item_cd='" + hditem_cd_fr.Value + "'");
            string sCustType = bll.vLookUp("select top 1 fld_valu from tfield_value where fld_nm='otlbrn'");
            double dPrice = bll.dGetItemPrice(hditem_cd_fr.Value.ToString(), sCustType, "CTN");
            txunitprice_fr.Text = dPrice.ToString();
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : frmTranStockAdj");
            Response.Redirect("fm_ErrorPage.aspx");
        }

    }
    
    protected void btadd_Click(object sender, EventArgs e)
    {
        try
        {

            txqtyPnl.CssClass = "";
            txitem_cd_frPnl.CssClass = "";
            cbbin_cd_frPnl.CssClass = "";
            cbbin_cd_toPnl.CssClass = "";
            txprod_cd_toPnl.CssClass = "";
            txprod_cd_frPnl.CssClass = "";
            txpacking_frPnl.CssClass = "";
            txpacking_toPnl.CssClass = "";
            txunitprice_frPnl.CssClass = "";
            txunitprice_toPnl.CssClass = "";

            if (txqty.Text == null || txqty.Text == "0")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Qty must greather then zero','error');", true);
                txqtyPnl.CssClass = "error";
                return;
            }
            if (hditem_cd_fr.Value == hditem_cd_to.Value && cbbin_cd_fr.SelectedValue == cbbin_cd_to.SelectedValue)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Can not ajustment with same Item and Bin','error');", true);
                txitem_cd_frPnl.CssClass = "error";
                cbbin_cd_frPnl.CssClass = "error";
                cbbin_cd_toPnl.CssClass = "error";
                return;
            }
            if (txpacking_fr.Text != txpacking_to.Text || txunitprice_fr.Text != txunitprice_to.Text)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Item must same product, packing and price','error');", true);
                if (txprod_cd_fr.Text != txprod_cd_to.Text)
                {
                    txprod_cd_toPnl.CssClass = "error";
                    txprod_cd_frPnl.CssClass = "error";
                }
                if (txpacking_fr.Text != txpacking_to.Text)
                {
                    txpacking_frPnl.CssClass = "error";
                    txpacking_toPnl.CssClass = "error";
                }
                if (txunitprice_fr.Text != txunitprice_to.Text)
                {
                    txunitprice_frPnl.CssClass = "error";
                    txunitprice_toPnl.CssClass = "error";
                }
                return;
            }
            lbstatusPnl.CssClass = "";
            if (lbstatus.Text == "COMPLETE")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction already complete can not be save','error');", true);
                lbstatusPnl.CssClass = "error";
                return;
            }
            dtstkadjdatePnl.CssClass = "";
            if (Request.Cookies["waz_dt"].Value.ToString() != dtstkadjdate.Text)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction Date Out Of Periode Date','error');", true);
                dtstkadjdatePnl.CssClass = "error";
                return;
            }
            List<cArrayList> arr = new List<cArrayList>();
            String stkadjno;
            if (txstkadjno.Text == "" || txstkadjno.Text == "NEW") { stkadjno = Request.Cookies["usr_id"].Value.ToString(); } else { stkadjno = txstkadjno.Text; }
            arr.Add(new cArrayList("@stkadjno", stkadjno));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@item_cd_fr", hditem_cd_fr.Value));
            arr.Add(new cArrayList("@bin_cd_fr", cbbin_cd_fr.SelectedValue));
            arr.Add(new cArrayList("@prod_cd_fr", txprod_cd_fr.Text));
            arr.Add(new cArrayList("@packing_fr", txpacking_fr.Text));
            arr.Add(new cArrayList("@unitprice_fr", txunitprice_fr.Text));
            arr.Add(new cArrayList("@item_cd_to", hditem_cd_to.Value));
            arr.Add(new cArrayList("@bin_cd_to", cbbin_cd_to.SelectedValue));
            arr.Add(new cArrayList("@prod_cd_to", txprod_cd_to.Text));
            arr.Add(new cArrayList("@packing_to", txpacking_to.Text));
            arr.Add(new cArrayList("@unitprice_to", txunitprice_to.Text));
            arr.Add(new cArrayList("@qty", txqty.Text));
            arr.Add(new cArrayList("@reason", txreason.Text));
            arr.Add(new cArrayList("@uom", cbuom.SelectedValue.ToString()));
            bll.vInserttblstockadjdtl(arr);
            bindinggrd();
            hditem_cd_fr.Value = "";
            txitem_cd_fr.Text = "";
            txprod_cd_fr.Text = "";
            txpacking_fr.Text = "";
            txunitprice_fr.Text = "";
            hditem_cd_to.Value = "";
            txitem_cd_to.Text = "";
            txprod_cd_to.Text = "";
            txpacking_to.Text = "";
            txunitprice_to.Text = "";
            txqty.Text = "";
            txitem_cd_fr.Focus();
            cd.v_showcontrol(btsave);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : frmTranStockAdj");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        finally { ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true); }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        SqlDataReader rs = null;
        string sItem = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        List<string> lItem = new List<string>();
        arr.Add(new cArrayList("@item_nm", prefixText));
        bll.vSearchMstItem2(arr, ref rs);
        while (rs.Read())
        {
            sItem = AutoCompleteExtender.CreateAutoCompleteItem(rs["item_cd"].ToString() + " | " + rs["item_nm"].ToString(), rs["item_cd"].ToString());
            lItem.Add(sItem);
        } rs.Close();
        return (lItem.ToArray());
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListitem_cd_to(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        SqlDataReader rs = null;
        string sItem = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        List<string> lItem = new List<string>();
        arr.Add(new cArrayList("@item_nm", prefixText));
        bll.vSearchMstItem2(arr, ref rs);
        while (rs.Read())
        {
            sItem = AutoCompleteExtender.CreateAutoCompleteItem(rs["item_cd"].ToString() + " | " + rs["item_nm"].ToString(), rs["item_cd"].ToString());
            lItem.Add(sItem);
        } rs.Close();
        return (lItem.ToArray());
    }
    protected void txitem_cd_to_TextChanged(object sender, EventArgs e)
    {
        try
        {
            txprod_cd_to.Text = bll.vLookUp("select prod_cd from tmst_item where item_cd='" + hditem_cd_to.Value + "'");
            txpacking_to.Text = bll.vLookUp("select packing from tmst_item where item_cd='" + hditem_cd_to.Value + "'");
            //txunitprice_to.Text = bll.vLookUp("select price_sell from tmst_item where item_cd='" + hditem_cd_to.Value + "'");
            string sCustType = bll.vLookUp("select top 1 fld_valu from tfield_value where fld_nm='otlbrn'");
            double dPrice = bll.dGetItemPrice(hditem_cd_to.Value.ToString(), sCustType, "CTN");
            txunitprice_to.Text = dPrice.ToString();

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : frmTranStockAdj");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    private void bindinggrd()
    {
        try
        {
            totalQty = 0;
            totalAmount = 0;
            string stkadjno;
            if (txstkadjno.Text == "" || txstkadjno.Text == "NEW") { stkadjno = Request.Cookies["usr_id"].Value.ToString(); } else { stkadjno = txstkadjno.Text; }
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@stkadjno", stkadjno));
            arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue));
            bll.vBindingGridToSp(ref grd, "sp_tblstockadjdtl_get", arr);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : frmTranStockAdj");
            Response.Redirect("fm_ErrorPage.aspx");
        }

    }
    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblqty = (Label)e.Row.FindControl("lblqty");
                Label lblAmount = (Label)e.Row.FindControl("lblAmount");
                decimal qty;
                decimal amount;
                if (lblqty != null)
                    qty = decimal.Parse(lblqty.Text);
                else
                    qty = 0;
                if (lblAmount != null)
                    amount = decimal.Parse(lblAmount.Text);
                else
                    amount = 0;
                totalQty = totalQty + qty;
                totalAmount = totalAmount + amount;
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalqty = (Label)e.Row.FindControl("lblTotalqty");
                Label lblTotalAmount = (Label)e.Row.FindControl("lblTotalAmount");
                lblTotalqty.Text = totalQty.ToString();
                lblTotalAmount.Text = totalAmount.ToString("#,##0.00");
            }

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : frmTranStockAdj");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void chprevstk_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime ddate = DateTime.ParseExact(dtstkadjdate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            var dateAsString = ddate.ToString("yyyy-MM-dd");
            string swhs_cd = bll.vLookUp("select whs_cd from tstockopname_schedule where whs_cd='" + cbwhs_cd.SelectedValue + "' and schedule_dt='" + dateAsString + "'");
            if (swhs_cd != cbwhs_cd.SelectedValue)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please entri schedule jaret !','Prev stk can not changed ','error');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
                if (chprevstk.Checked == true) { chprevstk.Checked = false; } else { chprevstk.Checked = true; }
                return;
            }
            string slevel_no = bll.vLookUp("select level_no from tmst_warehouse  where whs_cd='" + cbwhs_cd.SelectedValue + "'");
            if (slevel_no == "1")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Prev stk Only for Sub Depo and Van !','Prev stk can not changed ','error');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
                if (chprevstk.Checked == true) { chprevstk.Checked = false; } else { chprevstk.Checked = true; }
                return;
            }
            if (grd.Rows.Count != 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Item already inserted !','Prev stk can not changed ','error');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
                if (chprevstk.Checked == true) { chprevstk.Checked = false; } else { chprevstk.Checked = true; }
                return;
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : frmTranStockAdj");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
}