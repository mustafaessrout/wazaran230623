using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using AjaxControlToolkit;
public partial class fm_stockopnameentry_uom : System.Web.UI.Page
{
    cbll bll = new cbll();

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Data.SqlClient.SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        if (!IsPostBack)
        {
            try
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
                dtstocktopname.Text = Request.Cookies["waz_dt"].Value.ToString();
                //bll.vBindingFieldValueToCombo(ref cbUOM, "uom");
                bll.vBindingFieldValueToCombo(ref cbUOM, "uom_tf", "uom");
                bll.vBindingFieldValueToCombo(ref cbwhstype, "whs_typ");
                cbwhstype.SelectedValue = "DEPO";
                cbwhstype_SelectedIndexChanged(sender, e);
                dtstocktopname.CssClass = "makereadonly form-control";
                dtstocktopname.Enabled = false;
                txstockno.Text = "NEW"; txstockno.ReadOnly = true;
                txstockno.Enabled = false;
                arr.Clear();
                arr.Add(new cArrayList("@stock_no", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vDeletetstock_opname(arr);
                arr.Clear();
                if (Request.QueryString["st"] != null)
                {
                    string sStockNo = Request.QueryString["st"].ToString();
                    arr.Add(new cArrayList("@stock_no", sStockNo));
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    bll.vGetStockOpname(arr, ref rs);
                    while (rs.Read())
                    {
                        txstockno.Text = sStockNo;
                        dtstocktopname.Text = rs["stock_dt"].ToString();
                        cbwhstype.SelectedValue = rs["whs_typ"].ToString();
                        cbwhstype_SelectedIndexChanged(sender, e);
                        cbwhs.SelectedValue = rs["whs_cd"].ToString();
                        cbwhs_SelectedIndexChanged(sender, e);
                        cbbinb.SelectedValue = rs["bin_cd"].ToString();
                    }
                    rs.Close();
                    cbwhstype.Enabled = false;
                    cbwhs.Enabled = false;
                    cbbinb.Enabled = false;
                    btgenerate.Enabled = false;
                    btgenerate.Attributes.Add("style", "display:none");
                    grd.Columns[5].Visible = false;
                    grditem.Visible = false;
                    ////Fill Detail
                    //arr.Clear();
                    //arr.Add(new cArrayList("@stock_no", sStockNo));
                    //bll.vBindingGridToSp(ref grd, "sp_tstockopname_dtl_get", arr);

                }                
                bindinggrd();
                btDelete.CssClass = "divhid";
                btprint.CssClass = "divhid";
                btautoadj.CssClass = "divhid";
                btnew.CssClass = "divhid";
                btsave.CssClass = "divhid";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tableDropdown", "tableDropdown();", true);
            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_stockopnameentry");
                Response.Redirect("fm_ErrorPage.aspx");
            }            
        }
        
    }
    protected void bttmp_Click(object sender, EventArgs e)
    {
        try
        {
            SqlDataReader rs = null;
            txstockno.Text = Convert.ToString(Session["loostockno"]);
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@stock_no", txstockno.Text));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vGetStockOpname(arr, ref rs);
            while (rs.Read())
            {
                dtstocktopname.Text = string.Format("{0:d/M/yyyy}", rs["stock_dt"]);
                cbwhstype.SelectedValue = rs["whs_typ"].ToString();
                cbwhstype_SelectedIndexChanged(sender, e);
                cbwhs.SelectedValue = rs["whs_cd"].ToString();
                cbwhs_SelectedIndexChanged(sender, e);
                cbbinb.SelectedValue = rs["bin_cd"].ToString();
            }
            rs.Close();
            cbwhstype.Enabled = false;
            cbwhs.Enabled = false;
            cbbinb.Enabled = false;
            dtstocktopname.Enabled = false;
            //btgenerate.Enabled = false;
            bindinggrd();
            btsave.CssClass = "divhid";
            btprint.CssClass = "btn btn-info";
            btDelete.CssClass = "btn btn-danger";
            btsave.Attributes.Add("style", "display:none");
            btprint.Attributes.Remove("style");
            btDelete.Attributes.Remove("style");
            btnew.Attributes.Remove("style");
            btnew.CssClass = "btn-success btn btn-add";
            btgenerate.CssClass = "divhid";
            btgenerate.Attributes.Add("style", "display:none");
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_stockopnameentry");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    private void bindinggrd()
    {
        try
        {
            string stock_no;
            List<cArrayList> arr = new List<cArrayList>();
            if (txstockno.Text == "" || txstockno.Text == "NEW") 
            { 
                stock_no = Request.Cookies["usr_id"].Value.ToString();
                //arr.Clear();
                //if (cbwhstype.SelectedValue.ToString() == "DEPO")
                //{
                //    arr.Add(new cArrayList("@stock_no", stock_no));
                //    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //    arr.Add(new cArrayList("@whs_cd", cbwhs.SelectedValue.ToString()));
                //    arr.Add(new cArrayList("@bin_cd", cbbin.SelectedValue.ToString()));
                //    bll.vAutoStockOpname(arr);
                //}                
            } 
            else { stock_no = txstockno.Text; }
            arr.Clear();
            arr.Add(new cArrayList("@stock_no", stock_no));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            //bll.vBindingGridToSp(ref grd, "sp_tstockopname_dtl_get", arr);
            bll.vBindingGridToSp(ref grditem, "sp_tstockopname_dtl_get", arr);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_stockopnameentry");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void cbwhstype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            if (cbwhstype.SelectedValue == "DEPO")
            {
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@level_no", 1));
                bll.vBindingComboToSp(ref cbwhs, "sp_tmst_warehouse_get", "whs_cd", "whs_nm", arr);
            }
            if (cbwhstype.SelectedValue == "SUB")
            {
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@level_no", 2));
                bll.vBindingComboToSp(ref cbwhs, "sp_tmst_warehouse_get", "whs_cd", "whs_nm", arr);
            }
            if (cbwhstype.SelectedValue == "VS")
            {
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //arr.Add(new cArrayList("@job_title", "SalesJob"));
                //bll.vBindingComboToSp(ref cbwhs, "sp_tmst_vehicle_get", "vhc_cd", "emp_nm", arr);
                bll.vBindingComboToSp(ref cbwhs, "sp_tmst_vehicle_salesman_get", "vhc_cd", "vhc_desc", arr);
            }
            cbwhs_SelectedIndexChanged(sender, e);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_stockopnameentry");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void cbbin_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string qty;
        //if  (cbwhstype.SelectedValue=="VS")
        //{
        //    qty = bll.vLookUp("select stock_amt from tmst_van_stock where vhc_cd='" + cbwhs.SelectedValue + "'  and bin_cd='" + cbbin.SelectedValue + "' and item_cd='" + hditem.Value+ "'");
    
        //}
        //    else
        //{
        //    qty = bll.vLookUp("select stock_amt from tmst_stock where whs_cd='" + cbwhs.SelectedValue + "'  and bin_cd='" + cbbin.SelectedValue + "' and item_cd='" + hditem.Value+"'");
        //}
        //DateTime dt = DateTime.ParseExact(dtstocktopname.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //qty = bll.vLookUp("select [dbo].[sfnGetStock]('" + hditem.Value + "','" + cbbin.SelectedValue + "','" + cbwhs.SelectedValue + "','" + cbwhstype.SelectedValue + "','" + dt.ToShortDateString() + "')");
        //qty = Convert.ToString(string.IsNullOrEmpty(qty) ? "0" : qty);
        //string sQTY = bll.vLookUp("select dbo.sfnUomQtyConv('" + hditem.Value.ToString() + "','CTN','" + cbUOM.SelectedValue.ToString() + "',1)");
        //txqty_system.Text = (Convert.ToDouble(qty) * Convert.ToDouble(sQTY)).ToString(); 
        getstock();
    }
    public void getstock()
    {
        try
        {
            string qty;
            DateTime dt = DateTime.ParseExact(dtstocktopname.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            qty = bll.vLookUp("select [dbo].[sfnGetStock]('"+ Request.Cookies["sp"].Value.ToString() + "','" + hditem.Value + "','" + cbbinb.SelectedValue + "','" + cbwhs.SelectedValue + "','0','" + dt.Year + "-" + dt.Month + "-" + dt.Day + "')");
            qty = Convert.ToString(string.IsNullOrEmpty(qty) ? "0" : qty);
            string sQTY = bll.vLookUp("select dbo.sfnUomQtyConv('" + hditem.Value.ToString() + "','CTN','" + cbUOM.SelectedValue.ToString() + "',1)");
            txqty_system.Text = (Convert.ToDouble(qty) * Convert.ToDouble(sQTY)).ToString();

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_stockopnameentry");
            Response.Redirect("fm_ErrorPage.aspx");
        }

    }
    protected void cbwhs_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            cbbin.Items.Clear();
            List<cArrayList> arr = new List<cArrayList>();
            if (cbwhstype.SelectedValue.ToString() == "VS")
            {
                arr.Add(new cArrayList("@vhc_cd", cbwhs.SelectedValue.ToString()));
                bll.vBindingComboToSp(ref cbbin, "sp_tvan_bin_get", "bin_cd", "bin_nm", arr);
                bll.vBindingComboToSp(ref cbbinb, "sp_tvan_bin_get", "bin_cd", "bin_nm", arr);

            }
            if (cbwhstype.SelectedValue.ToString() == "DEPO")
            {
                arr.Add(new cArrayList("@whs_cd", cbwhs.SelectedValue.ToString()));
                //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vBindingComboToSp(ref cbbin, "sp_twarehouse_bin_get", "bin_cd", "bin_nm", arr);
                bll.vBindingComboToSp(ref cbbinb, "sp_twarehouse_bin_get", "bin_cd", "bin_nm", arr);
            }
            if (cbwhstype.SelectedValue.ToString() == "SUB")
            {
                arr.Add(new cArrayList("@whs_cd", cbwhs.SelectedValue.ToString()));
                //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vBindingComboToSp(ref cbbin, "sp_twarehouse_bin_get", "bin_cd", "bin_nm", arr);
                bll.vBindingComboToSp(ref cbbinb, "sp_twarehouse_bin_get", "bin_cd", "bin_nm", arr);
            }
            getstock();
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_stockopnameentry");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btgenerate_Click(object sender, EventArgs e)
    {
        try
        {
            string stock_no;
            List<cArrayList> arr = new List<cArrayList>();
            if (txstockno.Text == "" || txstockno.Text == "NEW")
            {
                stock_no = Request.Cookies["usr_id"].Value.ToString();
                arr.Clear();
                if ((cbwhstype.SelectedValue.ToString() == "DEPO") ||(cbwhstype.SelectedValue.ToString() == "SUB"))
                {
                    arr.Add(new cArrayList("@stock_no", stock_no));
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    arr.Add(new cArrayList("@whs_cd", cbwhs.SelectedValue.ToString()));
                    arr.Add(new cArrayList("@bin_cd", cbbinb.SelectedValue.ToString()));
                    bll.vAutoStockOpname(arr);
                    bindinggrd();
                    btnew.CssClass = "btn btn-add btn-succes";
                    btsave.CssClass = "btn btn-save btn-warning";
                    btnew.Attributes.Remove("style");
                    btsave.Attributes.Remove("style");
                }
                else if (cbwhstype.SelectedValue.ToString() == "VS")
                {
                    arr.Add(new cArrayList("@stock_no", stock_no));
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    arr.Add(new cArrayList("@whs_cd", cbwhs.SelectedValue.ToString()));
                    arr.Add(new cArrayList("@bin_cd", cbbinb.SelectedValue.ToString()));
                    bll.vAutoStockOpname(arr);
                    bindinggrd();
                    btnew.CssClass = "btn btn-add btn-succes";
                    btsave.CssClass = "btn btn-save btn-warning";
                    btnew.Attributes.Remove("style");
                    btsave.Attributes.Remove("style");
                }
            }

            //if (cbwhstype.SelectedValue=="VS")
            //{
            //DateTime dtdate = DateTime.ParseExact(dtstocktopname.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //string strdate = Convert.ToDateTime(dtdate).ToString("yyyy-MM-dd");
            ////string sdate = dtstocktopname.Text.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "popupwindow('fm_rptstockopnameformVS.aspx?qswhs_cd=" + cbwhs.SelectedValue + "&qsdate=" + strdate + "&qsstockno=" + txstockno.Text + "');", true);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
            //return;
            //}
            //else
            //{
            //arr.Clear();
            //arr.Add(new cArrayList("@stock_no", txstockno.Text));
            //Session["lParamStockOpname"] = arr;
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=StockOpname');", true);
            //}

            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ds", "window.alert('Generation successfully');", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_stockopnameentry");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        finally
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
        }

    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            if (Request.Cookies["waz_dt"].Value.ToString() != dtstocktopname.Text)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "swal('Attention','Transaction Date Out Of Periode Date','error');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
                grd.EditIndex = -1;
                return;
            }
            Label lblUOM = (Label)grd.Rows[e.NewEditIndex].FindControl("lblUOM");
            Label lblbin_cd = (Label)grd.Rows[e.NewEditIndex].FindControl("lblbin_cd");
            List<cArrayList> arr = new List<cArrayList>();
            grd.EditIndex = e.NewEditIndex;
            string stock_no;
            if (txstockno.Text == "" || txstockno.Text == "NEW") { stock_no = Request.Cookies["usr_id"].Value.ToString(); } else { stock_no = txstockno.Text; }
            arr.Add(new cArrayList("@stock_no", stock_no));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_tstockopname_dtl_get", arr);
            TextBox txqty = (TextBox)grd.Rows[e.NewEditIndex].FindControl("txtqtyactual");
            DropDownList cbobincd = (DropDownList)grd.Rows[e.NewEditIndex].FindControl("cbobincd");

            DropDownList cboUOM = (DropDownList)grd.Rows[e.NewEditIndex].FindControl("cboUOM");

            arr.Clear();
            if (cbwhstype.SelectedValue.ToString() == "VS")
            {
                arr.Add(new cArrayList("@vhc_cd", cbwhs.SelectedValue.ToString()));
                bll.vBindingComboToSp(ref cbobincd, "sp_tvan_bin_get", "bin_cd", "bin_nm", arr);

            }
            if (cbwhstype.SelectedValue.ToString() == "DEPO")
            {
                arr.Add(new cArrayList("@whs_cd", cbwhs.SelectedValue.ToString()));
                bll.vBindingComboToSp(ref cbobincd, "sp_twarehouse_bin_get", "bin_cd", "bin_nm", arr);
            }
            if (cbwhstype.SelectedValue.ToString() == "SUB")
            {
                arr.Add(new cArrayList("@whs_cd", cbwhs.SelectedValue.ToString()));
                bll.vBindingComboToSp(ref cbobincd, "sp_twarehouse_bin_get", "bin_cd", "bin_nm", arr);
            }
            cbobincd.SelectedValue = lblbin_cd.Text;

            bll.vBindingFieldValueToCombo(ref cboUOM, "uom");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "editTable", "editTable();", true);
            cboUOM.SelectedValue = lblUOM.Text;

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_stockopnameentry");
            Response.Redirect("fm_ErrorPage.aspx");
        }

    }
    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {

            // find  id of edit row

            //string seqID = grd.DataKeys[e.RowIndex].Value.ToString();

            // find updated values for update
            Label lbitemcode = (Label)grd.Rows[e.RowIndex].FindControl("lbitemcode");
            TextBox txtqtyactual = (TextBox)grd.Rows[e.RowIndex].FindControl("txtqtyactual");
            TextBox txtlocation = (TextBox)grd.Rows[e.RowIndex].FindControl("txtlocation");
            DropDownList cboUOM = (DropDownList)grd.Rows[e.RowIndex].FindControl("cboUOM");
            TextBox txtexpire_dt = (TextBox)grd.Rows[e.RowIndex].FindControl("txtexpire_dt");
            DropDownList cbobincd = (DropDownList)grd.Rows[e.RowIndex].FindControl("cbobincd");
            TextBox txtreason = (TextBox)grd.Rows[e.RowIndex].FindControl("txtreason");
            Label lbseqID = (Label)grd.Rows[e.RowIndex].FindControl("lbseqID");

            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@stock_no", txstockno.Text));
            arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
            //arr.Add(new cArrayList("@whs_cd", cbwhs.SelectedValue.ToString()));
            arr.Add(new cArrayList("@bin_cd", cbbin.SelectedValue.ToString()));
            arr.Add(new cArrayList("@qty", txtqtyactual.Text));
            arr.Add(new cArrayList("@location", txtlocation.Text));
            arr.Add(new cArrayList("@UOM", cboUOM.SelectedValue.ToString()));
            arr.Add(new cArrayList("@expire_dt", DateTime.ParseExact(txtexpire_dt.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@bin_cd", cbobincd.SelectedValue.ToString()));
            arr.Add(new cArrayList("@reason", txtreason.Text));
            arr.Add(new cArrayList("@seqID", lbseqID.Text));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vUpdateStockOpnameDtl(arr);
            grd.EditIndex = -1; arr.Clear();
            //arr.Add(new cArrayList("@stock_no", txstockno.Text));
            //bll.vBindingGridToSp(ref grd, "sp_tstockopname_dtl_get", arr);
            bindinggrd();
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_stockopnameentry");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {

            List<cArrayList> arr = new List<cArrayList>();
            grd.PageIndex = e.NewPageIndex;
            if (txstockno.Text == "NEW")
            {
                arr.Add(new cArrayList("@stock_no", Request.Cookies["usr_id"].Value.ToString()));
            }
            else
            {
                arr.Add(new cArrayList("@stock_no", txstockno.Text));
            }
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_tstockopname_dtl_get", arr);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_stockopnameentry");
            Response.Redirect("fm_ErrorPage.aspx");
        }

    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        try
        {
            //List<string> lFormula = new List<string>();
            //lFormula.Add("{tstock_opname.stock_no} = '"  +  txstockno.Text + "'");
            //Session["lformula"] = lFormula;
            //Response.Redirect("fm_report.aspx?src=sop");

            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@stock_no", txstockno.Text));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            Session["lParamsopu"] = arr;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=sopu');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_stockopnameentry");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_stockopnameentry_uom.aspx");
    }
    
    protected void btDelete_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.Cookies["waz_dt"].Value.ToString() != dtstocktopname.Text)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "swal('Attention','Transaction Date Out Of Periode Date','error');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
                return;
            }
            if (txstockno.Text != "" || txstockno.Text != "NEW")
            {
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@stock_no", txstockno.Text));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@status", "L"));
                bll.vDeletetstock_opname(arr);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "swal('Success','Data Deleted successfully !','success');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
                //Response.Redirect("fm_stockopnameentry.aspx");
                btsave.CssClass = "divhid";
                btprint.CssClass = "divhid";
                btDelete.CssClass = "divhid";
            }

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_stockopnameentry");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    
    protected void btadd_Click(object sender, EventArgs e) 
    {
        try
        {
            if (Request.Cookies["waz_dt"].Value.ToString() != dtstocktopname.Text)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "swal('Attention','Transaction Date Out Of Periode Date','error');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
                return;
            }
            //if (txstockno.Text == "" || txstockno.Text == "NEW")
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "window.alert('Please generate the create form')", true);
            //    return;
            //}

            cbUOMPnl.CssClass = "";
            if (cbUOM.SelectedValue == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "swal('Warning','UOM must be fill!','error');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
                cbUOMPnl.CssClass = "error";
                return;
            }
            string stock_no;
            txitem_cdPnl.CssClass = "";
            txqty_actualPnl.CssClass = "";
            if (hditem.Value == "" || txqty_actual.Text == "" || txqty_actual.Text == "0")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "swal('Warning','Item Name and quantity must be fill','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
                if (txitem_cd.Text == "") { txitem_cdPnl.CssClass = "error"; }
                if (txqty_actual.Text == "" || txqty_actual.Text == "0") { txqty_actualPnl.CssClass = "error"; }
                return;
            }
            txexpire_dtPnl.CssClass = "";
            if (txexpire_dt.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "swal('Warning','Check Expire Date must be fill','warning');", true);
                txexpire_dtPnl.CssClass = "error";
                return;
            }
            if (txstockno.Text == "" || txstockno.Text == "NEW") { stock_no = Request.Cookies["usr_id"].Value.ToString(); } else { stock_no = txstockno.Text; }
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@stock_no", stock_no));
            arr.Add(new cArrayList("@item_cd", hditem.Value));
            arr.Add(new cArrayList("@bin_cd", cbbin.SelectedValue));
            arr.Add(new cArrayList("@location", txlocation.Text));
            arr.Add(new cArrayList("@qty_system", txqty_system.Text));
            arr.Add(new cArrayList("@qty_actual", txqty_actual.Text));
            arr.Add(new cArrayList("@uom", cbUOM.SelectedValue));
            arr.Add(new cArrayList("@expire_dt", DateTime.ParseExact(txexpire_dt.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@reason", txreason.Text));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vInserttstockopname_dtl(arr);
            clearAdd();
            txitem_cd.Focus();
            bindinggrd();

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_stockopnameentry");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    void clearAdd()
    {
        txitem_cd.Text = "";
        hditem.Value = "";
        txqty_system.Text = "0";
        txqty_actual.Text = "0";
        txexpire_dt.Text = "";
        txreason.Text = "";
        txlocation.Text = "";
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListitem_cd(string prefixText, int count, string contextKey)
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
    protected void txitem_cd_TextChanged(object sender, EventArgs e)
    {
        getstock();
    }

    protected void txexpire_dt_TextChanged(object sender, EventArgs e)
    {
        try
        {

            DateTime dtsystem = DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime dtexpire = DateTime.ParseExact(txexpire_dt.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            TimeSpan ts = dtexpire.Subtract(dtsystem);
            if (ts.Days <= 0)
            {
                cbbin.SelectedValue = "BS";

            }
            else if (ts.Days > 90)
            {
                cbbin.SelectedValue = "GS";
            }
            else if (ts.Days > 0 || ts.Days < 90)
            {
                cbbin.SelectedValue = "NE";

            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_stockopnameentry");
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
            if (Request.Cookies["waz_dt"].Value.ToString() != dtstocktopname.Text)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "swal('Attention','Transaction Date Out Of Periode Date','error');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
                return;
            }
            Label lbseqID = (Label)grd.Rows[e.RowIndex].FindControl("lbseqID");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@seqID", lbseqID.Text));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vDeletetstockopname_dtl(arr);
            bindinggrd();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "swal('Seuccess','Data Deleted successfully !','success');", true);

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_stockopnameentry");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void btautoadj_Click(object sender, EventArgs e)
    {
        try
        {
            string stkadjno = "";
            if (txstockno.Text == null || txstockno.Text == "NEW")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "swal('Attention','Stock opname not complete, please save before auto ajustment','error');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
                return;
            }
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@stock_no", txstockno.Text));
            bll.vInserttblstockadjAuto(arr, ref stkadjno);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "swal('Auto adjusment successfully ..','Adjusment No. " + stkadjno + "','info');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_stockopnameentry");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {

    }
    protected void cbUOM_SelectedIndexChanged(object sender, EventArgs e)
    {
        getstock();
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.Cookies["waz_dt"].Value.ToString() != dtstocktopname.Text)
            {

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "swal('Attention','Transaction Date Out Of Periode Date','error');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
                return;
            }
            string sStock_no = "";
            List<cArrayList> arr = new List<cArrayList>();
            if (txstockno.Text == "" || txstockno.Text == "NEW")
            {
                foreach (GridViewRow row in grditem.Rows)
                {
                    Label lbitemcd = (Label)row.FindControl("lbitemcd");
                    Label lbqtysys = (Label)row.FindControl("lbqtysys");
                    //TextBox txqty = (TextBox)row.FindControl("txqty");
                    TextBox txexpiredt = (TextBox)row.FindControl("txexpiredt");
                    TextBox txreason = (TextBox)row.FindControl("txreason");
                    //DropDownList cbuom = (DropDownList)row.FindControl("cbuom");
                    //DropDownList cbbin = (DropDownList)row.FindControl("cbbin");

                    TextBox txqty_ctn = (TextBox)row.FindControl("txqty_ctn");
                    TextBox txqty_pcs = (TextBox)row.FindControl("txqty_pcs");
                    DropDownList cbuom_ctn = (DropDownList)row.FindControl("cbuom_ctn");
                    DropDownList cbuom_pcs = (DropDownList)row.FindControl("cbuom_pcs");


                    if (txqty_ctn.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Add Actual Qty ','Item. " + lbitemcd.Text + "','warning');", true);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
                        return;
                    }

                    DateTime dtsystem = DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    DateTime dtexpire = DateTime.ParseExact(txexpiredt.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    TimeSpan ts = dtexpire.Subtract(dtsystem);

                    //cbbin.SelectedValue = "GS";

                    //if (ts.Days <= 0)
                    //{
                    //    cbbin.SelectedValue = "BS";
                    //}
                    //else if (ts.Days > 90)
                    //{
                    //    cbbin.SelectedValue = "GS";
                    //}
                    //else if (ts.Days > 0 || ts.Days < 90)
                    //{
                    //    cbbin.SelectedValue = "NE";
                    //}

                    arr.Clear();
                    arr.Add(new cArrayList("@stock_no", Request.Cookies["usr_id"].Value.ToString()));
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    arr.Add(new cArrayList("@bin_cd", "GS"));
                    arr.Add(new cArrayList("@item_cd", lbitemcd.Text));
                    arr.Add(new cArrayList("@qty", txqty_ctn.Text));
                    arr.Add(new cArrayList("@qty2", txqty_pcs.Text));
                    arr.Add(new cArrayList("@uom", cbuom_ctn.SelectedValue.ToString()));
                    arr.Add(new cArrayList("@uom2", cbuom_pcs.SelectedValue.ToString()));
                    arr.Add(new cArrayList("@expire_dt", DateTime.ParseExact(txexpiredt.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                    arr.Add(new cArrayList("@reason", txreason.Text));
                    bll.vUpdateStockOpnameDtlAuto(arr);

                }
                arr.Clear();
                arr.Add(new cArrayList("@bin_cd", "GS"));
                arr.Add(new cArrayList("@whs_cd", cbwhs.SelectedValue.ToString()));
                arr.Add(new cArrayList("@whs_typ", cbwhstype.SelectedValue.ToString()));
                arr.Add(new cArrayList("@stock_dt", DateTime.ParseExact(dtstocktopname.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vGenStockOpname(arr, ref sStock_no);
                txstockno.Text = sStock_no;
                btprint.CssClass = "btn btn-info";
                btDelete.CssClass = "btn btn-danger";
                btsave.CssClass = "divhid";
                //btautoadj.CssClass = "btn btn-primary";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "swal('Data Save successfully ..','Stock Opname No. " + txstockno.Text + "','info');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "swal('Only Quantity can update, Data saved succesfully ..','Stock Opname No. " + txstockno.Text + "','info');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
            }
            bindinggrd();
            cbwhstype.Enabled = false;
            cbwhs.Enabled = false;
            cbbinb.Enabled = false;

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_stockopnameentry");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        
    }

    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //try
        //{

        //}
        //catch (Exception ex)
        //{
        //    Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
        //    bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_stockopnameentry");
        //    Response.Redirect("fm_ErrorPage.aspx");
        //}
    }

    protected void grditem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList cbuom_ctn = (DropDownList)e.Row.FindControl("cbuom_ctn");
                DropDownList cbuom_pcs = (DropDownList)e.Row.FindControl("cbuom_pcs");
                DropDownList cbbin = (DropDownList)e.Row.FindControl("cbbin");

                bll.vBindingFieldValueToCombo(ref cbuom_ctn, "uom_tf", "uom");
                bll.vBindingFieldValueToCombo(ref cbuom_pcs, "uom_tf", "uom");
                cbuom_ctn.SelectedValue = "CTN";
                cbuom_pcs.SelectedValue = "PCS";

                cbbin.Items.Clear();
                List<cArrayList> arr = new List<cArrayList>();
                if (cbwhstype.SelectedValue.ToString() == "VS")
                {
                    arr.Add(new cArrayList("@vhc_cd", cbwhs.SelectedValue.ToString()));
                    bll.vBindingComboToSp(ref cbbin, "sp_tvan_bin_get", "bin_cd", "bin_nm", arr);

                }
                if (cbwhstype.SelectedValue.ToString() == "DEPO")
                {
                    arr.Add(new cArrayList("@whs_cd", cbwhs.SelectedValue.ToString()));
                    bll.vBindingComboToSp(ref cbbin, "sp_twarehouse_bin_get", "bin_cd", "bin_nm", arr);
                }
                if (cbwhstype.SelectedValue.ToString() == "SUB")
                {
                    arr.Add(new cArrayList("@whs_cd", cbwhs.SelectedValue.ToString()));
                    bll.vBindingComboToSp(ref cbbin, "sp_twarehouse_bin_get", "bin_cd", "bin_nm", arr);
                }

                if (txstockno.Text != "" || txstockno.Text != "NEW")
                {
                    txqty_actual.Enabled = false;
                    cbuom_ctn.Enabled = false;
                    cbuom_pcs.Enabled = false;
                    cbbin.Enabled = false;
                    txlocation.Enabled = false;
                    txreason.Enabled = false;
                }

            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_stockopnameentry");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

}