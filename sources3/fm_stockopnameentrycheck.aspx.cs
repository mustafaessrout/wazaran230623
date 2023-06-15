using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using AjaxControlToolkit;
public partial class fm_stockopnameentrycheck : System.Web.UI.Page
{
    cbll bll = new cbll();

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Data.SqlClient.SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        if (!IsPostBack)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
            dtstocktopname.Text = Request.Cookies["waz_dt"].Value.ToString();
            bll.vBindingFieldValueToCombo(ref cbUOM, "uom");
            bll.vBindingFieldValueToCombo(ref cbwhstype, "whs_typ");
            cbwhstype_SelectedIndexChanged(sender, e);
            dtstocktopname.CssClass="makereadonly form-control ro";
            dtstocktopname.Enabled = false;
            txstockno.Text = "NEW"; txstockno.ReadOnly = true;
            if (Request.QueryString["st"] != null)
            {
                string sStockNo = Request.QueryString["st"].ToString();
                arr.Add(new cArrayList("@stock_no", sStockNo));
                bll.vGetStockOpname(arr, ref rs);
                while (rs.Read())
                {
                    txstockno.Text = sStockNo;
                    dtstocktopname.Text =  rs["stock_dt"].ToString();
                    cbwhstype.SelectedValue = rs["whs_typ"].ToString();
                    cbwhstype_SelectedIndexChanged(sender, e);
                    cbwhs.SelectedValue = rs["whs_cd"].ToString();
                    cbwhs_SelectedIndexChanged(sender, e);
                    cbbin.SelectedValue = rs["bin_cd"].ToString();
                } rs.Close();
                cbwhstype.Enabled = false;
                cbwhs.Enabled = false;
                cbbinb.Enabled = false;
                //btgenerate.Enabled = false;
                grd.Columns[5].Visible = false;
                ////Fill Detail
                //arr.Clear();
                //arr.Add(new cArrayList("@stock_no", sStockNo));
                //bll.vBindingGridToSp(ref grd, "sp_tstockopname_dtl_get", arr);
                
            }
            bindinggrd();
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
    }
    protected void bttmp_Click(object sender, EventArgs e)
    {
        SqlDataReader rs = null;
        txstockno.Text = Convert.ToString(Session["loostockno"]);
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@stock_no", txstockno.Text));
        bll.vGetStockOpname(arr, ref rs);
        while (rs.Read())
        {
            dtstocktopname.Text = string.Format("{0:d/M/yyyy}", rs["stock_dt"]);
            cbwhstype.SelectedValue = rs["whs_typ"].ToString();
            cbwhstype_SelectedIndexChanged(sender, e);
            cbwhs.SelectedValue = rs["whs_cd"].ToString();
            cbwhs_SelectedIndexChanged(sender, e);
            cbbin.SelectedValue = rs["bin_cd"].ToString();
        } rs.Close();
        cbwhstype.Enabled = false;
        cbwhs.Enabled = false;
        cbbinb.Enabled = false;
        dtstocktopname.Enabled = false;
        //btgenerate.Enabled = false;
        bindinggrd();
    }
    private void bindinggrd()
    {
        string stock_no;
        if (txstockno.Text == "" || txstockno.Text == "NEW") { stock_no = Request.Cookies["usr_id"].Value.ToString(); } else { stock_no = txstockno.Text; }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@stock_no", stock_no));
        bll.vBindingGridToSp(ref grd, "sp_tstockopnamecheck_dtl_get", arr);
    }
    protected void cbwhstype_SelectedIndexChanged(object sender, EventArgs e)
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
            arr.Add(new cArrayList("@job_title", "SalesCD"));
            bll.vBindingComboToSp(ref cbwhs, "sp_tmst_vehicle_get", "vhc_cd", "emp_nm", arr);
        }
        cbwhs_SelectedIndexChanged(sender, e);
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
        string qty;
        DateTime dt = DateTime.ParseExact(dtstocktopname.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        qty = bll.vLookUp("select [dbo].[sfnGetStock]('" + hditem.Value + "','" + cbbinb.SelectedValue + "','" + cbwhs.SelectedValue + "','" + cbwhstype.SelectedValue + "','" + dt.Year + "-" + dt.Month + "-" + dt.Day + "')");
        qty = Convert.ToString(string.IsNullOrEmpty(qty) ? "0" : qty);
        string sQTY = bll.vLookUp("select dbo.sfnUomQtyConv('" + hditem.Value.ToString() + "','CTN','" + cbUOM.SelectedValue.ToString() + "',1)");
        txqty_system.Text = (Convert.ToDouble(qty) * Convert.ToDouble(sQTY)).ToString(); 
    }
    protected void cbwhs_SelectedIndexChanged(object sender, EventArgs e)
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
    protected void btgenerate_Click(object sender, EventArgs e)
    {

        

        //if (cbwhstype.SelectedValue=="VS")
        //{
            DateTime dtdate = DateTime.ParseExact(dtstocktopname.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            string strdate = Convert.ToDateTime(dtdate).ToString("yyyy-MM-dd");
            //string sdate = dtstocktopname.Text.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "popupwindow('fm_rptstockopnameformVS.aspx?qswhs_cd=" + cbwhs.SelectedValue + "&qsdate=" + strdate + "&qsstockno=" + txstockno.Text + "');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
            return;
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
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        if (Request.Cookies["waz_dt"].Value.ToString() != dtstocktopname.Text)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction Date Out Of Periode Date','error');", true);
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
        bll.vBindingGridToSp(ref grd, "sp_tstockopnamecheck_dtl_get", arr);
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
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "editTable", "editTable();", true);
        bll.vBindingFieldValueToCombo(ref cboUOM, "uom");
        cboUOM.SelectedValue = lblUOM.Text;
    }
    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
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
        //arr.Add(new cArrayList("@bin_cd", cbbin.SelectedValue.ToString()));
        arr.Add(new cArrayList("@qty", txtqtyactual.Text));
        arr.Add(new cArrayList("@location", txtlocation.Text));
        arr.Add(new cArrayList("@UOM", cboUOM.SelectedValue.ToString()));
        arr.Add(new cArrayList("@expire_dt", DateTime.ParseExact(txtexpire_dt.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@bin_cd", cbobincd.SelectedValue.ToString()));
        arr.Add(new cArrayList("@reason", txtreason.Text));
        arr.Add(new cArrayList("@seqID", lbseqID.Text));
        bll.vUpdateStockOpnamecheckDtl(arr);
        grd.EditIndex = -1; arr.Clear();
        //arr.Add(new cArrayList("@stock_no", txstockno.Text));
        //bll.vBindingGridToSp(ref grd, "sp_tstockopname_dtl_get", arr);
        bindinggrd();
    }
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
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
      
        bll.vBindingGridToSp(ref grd, "sp_tstockopnamecheck_dtl_get", arr);
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        //List<string> lFormula = new List<string>();
        //lFormula.Add("{tstock_opname.stock_no} = '"  +  txstockno.Text + "'");
        //Session["lformula"] = lFormula;
        //Response.Redirect("fm_report.aspx?src=sop");

        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@stock_no", txstockno.Text));
        Session["lParamsopcheck"] = arr;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=sopcheck');", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_stockopnameentrycheck.aspx");
    }
    
    protected void btDelete_Click(object sender, EventArgs e)
    {
        if (Request.Cookies["waz_dt"].Value.ToString() != dtstocktopname.Text)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction Date Out Of Periode Date','error');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
            return;
        }
        if (txstockno.Text != "" || txstockno.Text != "NEW")
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@stock_no", txstockno.Text));
            bll.vDeletetstock_opname_check(arr);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "window.alert('Data Deleted successfully !')", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
            Response.Redirect("fm_stockopnameentrycheck.aspx");
        }
    }
    
    protected void btadd_Click(object sender, EventArgs e)
    {
        if (Request.Cookies["waz_dt"].Value.ToString() != dtstocktopname.Text)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction Date Out Of Periode Date','error');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
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
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','UOM must be fill','warning');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
            cbUOMPnl.CssClass = "error";
            return;
        }
        string stock_no;

        txitem_cdPnl.CssClass ="";
        txqty_actualPnl.CssClass = "";
        if (hditem.Value == "" || txqty_actual.Text == "" || txqty_actual.Text == "0")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Item Name and quantity must be fill','warning');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);

            if (hditem.Value == "")
            {
                txitem_cdPnl.CssClass ="error";
            }
            if (txqty_actual.Text == "" || txqty_actual.Text == "0")
            {
                txqty_actualPnl.CssClass = "error";
            }
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
        arr.Add(new cArrayList("@reason",txreason.Text));
        bll.vInserttstockopnamecheck_dtl(arr);
        clearAdd();
        txitem_cd.Focus();
        bindinggrd();
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
    protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grd.EditIndex = -1;
        bindinggrd();
    }
    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Request.Cookies["waz_dt"].Value.ToString() != dtstocktopname.Text)
        {

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction Date Out Of Periode Date','success');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
            return;
        }
        Label lbseqID = (Label)grd.Rows[e.RowIndex].FindControl("lbseqID");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@seqID", lbseqID.Text));
        bll.vDeletetstockopnamecheck_dtl(arr);
        bindinggrd();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "window.alert('Data Deleted successfully !')", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
    }

    protected void btautoadj_Click(object sender, EventArgs e)
    {
        string stkadjno = "";
        if (txstockno.Text == null || txstockno.Text == "NEW")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Stock opname not complete, please save before auto ajustment','error');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@stock_no", txstockno.Text));
        bll.vInserttblstockadjAuto(arr, ref stkadjno);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Auto adjusment successfully ..','Adjusment No. " + stkadjno + "','success');", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
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
        if (Request.Cookies["waz_dt"].Value.ToString() != dtstocktopname.Text)
        {
            
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction Date Out Of Periode Date','error');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
            return;
        }
        string sStock_no = "";
        List<cArrayList> arr = new List<cArrayList>();
        if (txstockno.Text == "" || txstockno.Text == "NEW")
        {
            arr.Add(new cArrayList("@bin_cd", cbbinb.SelectedValue.ToString()));
            arr.Add(new cArrayList("@whs_cd", cbwhs.SelectedValue.ToString()));
            arr.Add(new cArrayList("@whs_typ", cbwhstype.SelectedValue.ToString()));
            arr.Add(new cArrayList("@stock_dt", DateTime.ParseExact(dtstocktopname.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
            bll.vGenStockOpnamecheck(arr, ref sStock_no);
            txstockno.Text = sStock_no;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Data Save successfully ..','Stock Opname No. " + txstockno.Text + "','success');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Only Quantity can update, Data saved succesfully ..','Stock Opname No. " + txstockno.Text + "','success');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
        }
        bindinggrd();
        cbwhstype.Enabled = false;
        cbwhs.Enabled = false;
        cbbinb.Enabled = false;
        
    }
}