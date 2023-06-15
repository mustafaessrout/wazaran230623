using AjaxControlToolkit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_po_transaction : System.Web.UI.Page
{
    cbll bll = new cbll();
    cbllHO bllHO = new cbllHO();
    Utitlity ut = new Utitlity();
    cdal cdl = new cdal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindControl();
            //ddlPOStatus_SelectedIndexChanged(sender, e);
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@do_no", Request.Cookies["usr_id"].Value.ToString()));

            bll.vDelDoDtl3(arr);
            arr.Clear();
            arr.Add(new cArrayList("@po_no", Request.Cookies["usr_id"].Value.ToString()));
            bll.vDelPoDtl3(arr);
            BindGridPO();
            cbsalespoint_SelectedIndexChanged(sender, e);
            isUpdatePO.Value = "false";
            bll.vBindingFieldValueToCombo(ref cbexpedition, "comp_sta_id");
            cbexpedition_SelectedIndexChanged(sender, e);
            arr.Clear();
            arr.Add(new cArrayList("@qry_cd", "whs_do"));
            bll.vBindingComboToSp(ref cbwarehouse, "sp_tmst_warehouse_getbyqry", "whs_cd", "whs_desc", arr);
            cbwarehouse_SelectedIndexChanged(sender, e);
            dtdo.Text = DateTime.Now.ToString("d/M/yyyy");
        }
    }

    private void bindControl()
    {
        List<cArrayList> arr = new List<cArrayList>();
        DataTable dt = new DataTable();
        arr.Clear();
        bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespointSN_get", "salespointcd", "salespoint_desc", arr);
        var wadiItem = cbsalespoint.Items[cbsalespoint.Items.Count - 2];
        cbsalespoint.Items.Remove(wadiItem);

        bll.vBindingFieldValueToCombo(ref ddlPOStatus, "po_sta_id");
        ddlPOStatus.SelectedValue = "N";
        bll.vBindingComboToSp(ref cbtrella, "sp_tmst_vehicle_get", "vhc_cd", "vhc_nm");
        bll.vBindingComboToSp(ref cbbox, "sp_tmst_trailer_box_get", "box_cd", "box_nm");
    }

    private void BindGrid()
    {
        List<cArrayList> arr = new List<cArrayList>();
        DataTable dt = new DataTable();
        DataTable dtAccordingUser = new DataTable();

        arr.Add(new cArrayList("@do_no", Request.Cookies["usr_id"].Value.ToString()));
        dtAccordingUser = cdl.GetValueFromSP("sp_tdo_dtl_get2", arr);
        dt = cdl.GetValueFromSP("sp_tpo_dtl_get2", arr);
         if (dt.Rows.Count == 0)
        {
            grd.DataSource = null;
            grd.DataBind();
        }
        else
        {
            grd.DataSource = dt;
            grd.DataBind();
        }

    }

    private void BindGridDODt() {
        List<cArrayList> arr = new List<cArrayList>();
        DataTable dtAccordingUser = new DataTable();
        arr.Add(new cArrayList("@do_no", Request.Cookies["usr_id"].Value.ToString()));
        dtAccordingUser = cdl.GetValueFromSP("sp_tdo_dtl_get2", arr);

        if (dtAccordingUser.Rows.Count > 0)
        {
            grd.DataSource = dtAccordingUser;
            grd.DataBind();
        }
        else {
            grd.DataSource = null;
            grd.DataBind();
        }
    }

    private void BindGridPO()
    {
        List<cArrayList> arr = new List<cArrayList>();
        DataTable dt = new DataTable();
        arr.Add(new cArrayList("@po_sta_id", Convert.ToString(ddlPOStatus.SelectedValue)));
        arr.Add(new cArrayList("@salespointcd", Convert.ToString(cbsalespoint.SelectedValue)));
        dt = cdl.GetValueFromSP("sp_tmst_po_getbysalespoint", arr);

        if (dt.Rows.Count > 0)
        {
            hdfPO.Value = Convert.ToString(dt.Rows[0]["po_no"]);
            grdPO.DataSource = dt;
            grdPO.DataBind();
        }
        else
        {
            grdPO.DataSource = null;
            grdPO.DataBind();
        }
    }

    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        decimal qty = 0;
        int inQty = 0;
        HiddenField hdfItem_cd = (HiddenField)grd.Rows[e.NewSelectedIndex].FindControl("hdfItem_cd");
        Label lblItemName = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lblItemName");
        Label lblQty = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lblQty");
        Label lblUnit_price = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lblUnit_price");
        hditem.Value = Convert.ToString(hdfItem_cd.Value);
        txitemname.Text = lblItemName.Text;
        qty = Convert.ToDecimal(lblQty.Text);
        inQty = Convert.ToInt32(qty);
        txqty.Text = Convert.ToString(inQty);
        lblUnitPrice.Text = Convert.ToString(lblUnit_price.Text);
        headerItem.InnerText = "Updated Item";
        headerBtAdd.InnerText = "Update";
        btadd.Text = "Update";
        isUpdatePO.Value = "true";
        //BindGrid(); 
    }
    protected void grdPO_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        lblUnitPrice.Text = string.Empty;
        txitemname.Text = string.Empty;
        txqty.Text = string.Empty;
        Label lblPO = (Label)grdPO.Rows[e.NewSelectedIndex].FindControl("lblPO");
        hdfPO.Value = lblPO.Text;
        txpono.Text = hdfPO.Value;

        List<cArrayList> arr = new List<cArrayList>();
        DataTable dt = new DataTable();
        arr.Add(new cArrayList("@po_no", hdfPO.Value));
        arr.Add(new cArrayList("@salespointcd", Convert.ToString(cbsalespoint.SelectedValue)));
        dt = cdl.GetValueFromSP("sp_tpo_dtl_get2", arr);
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                decimal unitPrice = 0;

                arr.Clear();
                arr.Add(new cArrayList("@item_cd",Convert.ToString( dr["item_cd"])));
                arr.Add(new cArrayList("@do_no", Request.Cookies["usr_id"].Value));
                arr.Add(new cArrayList("@qty", Convert.ToString(dr["qty"])));
                arr.Add(new cArrayList("@unitprice", Convert.ToString(dr["unit_price"])));
                bll.vUpdateDoDtlHO(arr);
            }
            BindGridDODt();
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('No PO details found !','No PO details found','warning');", true);
        }
    }

    void InsertTempDataInSO() {
        try
        {
            foreach (GridViewRow row in grd.Rows)
            {
                decimal unitPrice = 0;
                List<cArrayList> arrDtl = new List<cArrayList>();
                HiddenField hdfItem_cd = (HiddenField)row.FindControl("hdfItem_cd");
                Label lblQty = (Label)row.FindControl("lblQty");
                Label lblUnit_price = (Label)row.FindControl("lblUnit_price");
                
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@item_cd", hdfItem_cd.Value.ToString()));
                arr.Add(new cArrayList("@do_no", Request.Cookies["usr_id"].Value));
                arr.Add(new cArrayList("@qty", lblQty.Text));
                arr.Add(new cArrayList("@unitprice", Convert.ToDecimal(lblUnit_price.Text)));
                bll.vUpdateDoDtlHO(arr);
            }
        }
       catch (Exception ex) {
           ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('" + ex.Message + ex.InnerException + "','" + ex.Message + ex.InnerException + "','warning');", true);
           ut.Logs("", "HO Transaction", "PO Transaction", "fm_po_trnasaction", "InsertTempDataInSO", "Exception", ex.Message + ex.InnerException);
        }
    }
    protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grd.EditIndex = -1;
    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grd.EditIndex = e.NewEditIndex;
        Label lblPO_No = (Label)grd.Rows[e.NewEditIndex].FindControl("lblPO_No");
    }
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void grdPO_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPO.PageIndex = e.NewPageIndex;
        BindGridPO();
    }

    protected void OnSelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in grdPO.Rows)
        {
            if (row.RowIndex == grdPO.SelectedIndex)
            {
                row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
            }
            else
            {
                row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
            }
        }
    }
    protected void cbsalespoint_SelectedIndexChanged(object sender, EventArgs e)
    {
        bool status = false;
        Ping pg = new Ping();

        if (cbsalespoint.SelectedValue == "0")
        {
            status = false;
        }
        else
        {
            List<cArrayList> arr = new List<cArrayList>();
            DataTable dt = new DataTable();
            arr.Add(new cArrayList("@salespointcd", Convert.ToString(cbsalespoint.SelectedValue)));
            arr.Add(new cArrayList("@region", null));
            dt = cdl.GetValueFromSP("sp_tmst_salespoint_get", arr);

            if (dt.Rows.Count > 0)
            {
                PingReply reply = pg.Send(Convert.ToString(dt.Rows[0]["comp_cd"]));
                status = reply.Status == IPStatus.Success;
            }
        }
        if (status == false)
        {
            //lblHOStat.Text = "disconnected";

            //dvHOStatusValue.Style.Add("box-shadow", "inset 0 0 5px rgba(200, 236, 214, 0.71)");
            //dvHOStatusValue.Style.Add("background", "radial-gradient(#e74c3c 80%,#a7f1c6)");
            //dvHOStatusValue.Style.Add("background-color", "red");
            hdfBranchConnected.Value = "false";
        }
        else
        {
            //lblHOStat.Text = "connected";
            //dvHOStatusValue.Style.Add("box-shadow", "inset 0 0 5px rgba(200, 236, 214, 0.71)");
            //dvHOStatusValue.Style.Add("background", "radial-gradient(#2ecc71 80%,#a7f1c6)");
            //dvHOStatusValue.Style.Add("background-color", "green");
            hdfBranchConnected.Value = "true";
        }
        BindGridPO();
        grd.DataSource = null;
        grd.DataBind();
        cbexpedition_SelectedIndexChanged(sender, e);
    }
    protected void btSearchHO_Click(object sender, EventArgs e)
    {
        try
        {
            Ping pg = new Ping();
            PingReply reply = pg.Send("172.16.14.5");
            bool status = reply.Status == IPStatus.Success;
            if (status == false)
            {
                //dvHOStatusValue.Style.Add("background-color", "red");
                hdfBranchConnected.Value = "false";
            }
            else
            {
                //dvHOStatusValue.Style.Add("background-color", "green");
                hdfBranchConnected.Value = "true";
            }
        }
        catch (Exception ex)
        {
            //app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "HO Transaction", "PO Transaction", "fm_po_trnasaction", "btSearchHO_Click", "Exception", ex.Message + ex.InnerException);
        }
    }
    protected void ddlPOStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGridPO();
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetItemList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll(); SqlDataReader rs = null;
        string sItem = string.Empty;
        List<string> lItem = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@item_nm", prefixText));
        bll.vSearchMstItem2(arr, ref rs);
        while (rs.Read())
        {
            sItem = AutoCompleteExtender.CreateAutoCompleteItem(rs["item_cd"].ToString() + "-" + rs["item_nm"].ToString() + "'-" + rs["size"].ToString() + "-" + rs["branded_nm"].ToString(), rs["item_cd"].ToString());
            lItem.Add(sItem);
        }
        rs.Close();
        return (lItem.ToArray());
    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        try
        {
            double dOut = 0; double dqty = 0;
            string po_no;
            if (txpono.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please select PO..','Please select PO..','warning');", true);
                return;
            }
            else if (hditem.Value == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please select Item..','Please select Item..','warning');", true);
                return;

            }
            else if (hditem.Value.ToString() == "" || hditem.Value.Equals(null))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Item Not selected !','Please select item','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
                return;
            }

            else if (!double.TryParse(txqty.Text, out dqty))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('There no item order quantity','Check your qty order','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
                return;
            }
            else if (Convert.ToString(lblUnitPrice.Text) == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Unit price must be greater than 0','Unit Price','warning');", true);
                return;
            }
            else if (Convert.ToDecimal(lblUnitPrice.Text) <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Unit price must be greater than 0','Unit Price','warning');", true);
                return;
            }
            else if (Convert.ToDecimal(txqty.Text) <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('There no item order quantity','Check your qty order','warning');", true);
                return;
            }
            if (txpono.Text == "" || txpono.Text == "NEW") { po_no = Request.Cookies["usr_id"].Value.ToString(); } else { po_no = txpono.Text; }
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@item_cd", hditem.Value.ToString()));
            arr.Add(new cArrayList("@do_no", Request.Cookies["usr_id"].Value));
            arr.Add(new cArrayList("@qty", txqty.Text));
            arr.Add(new cArrayList("@unitprice", Convert.ToDecimal(lblUnitPrice.Text)));
            //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));

            if (btadd.Text == "Update")
            {
                bll.vUpdateDoDtlHO(arr);
                //bll.vUpdatePoDtl(arr);
                btadd.Text = "Add";
                isUpdatePO.Value = "true";
            }
            else
            {
                bll.vUpdateDoDtlHO(arr);
                arr.Clear();
                arr.Add(new cArrayList("@item_cd", hditem.Value.ToString()));
                arr.Add(new cArrayList("@qty", txqty.Text));
                //arr.Add(new cArrayList("@subtotal", 0));
                arr.Add(new cArrayList("@unit_price", lblUnitPrice.Text));
                arr.Add(new cArrayList("@po_no", Request.Cookies["usr_id"].Value));
                arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue));
                //bll.vUpdatePoDtl(arr);
                isUpdatePO.Value = "true";
            }
            arr.Clear();
            BindGridDODt();
            txitemname.Text = "";
            lblUnitPrice.Text = "";
            txqty.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "set", "$('#txqty').val('0');", true);
            txitemname.Focus();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Data saved succesfully..','Data saved succesfully..','success');", true);
        }
        catch (Exception ex) {
            ut.Logs("", "HO Transaction", "PO Transaction", "fm_po_trnasaction", "btadd_Click", "Exception", ex.Message + ex.InnerException);
        }
    }

    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@po_no", txpono.Text.ToString()));
            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue));
            arr.Add(new cArrayList("@po_sta_id", "P"));

            bll.vUpdatePoStatus(arr);
            decimal grantTotal = 0;
            foreach (GridViewRow row in grd.Rows)
            {
                decimal unitPrice = 0;
                List<cArrayList> arrDtl = new List<cArrayList>();
                HiddenField hdfItem_cd = (HiddenField)row.FindControl("hdfItem_cd");
                Label lblQty = (Label)row.FindControl("lblQty");

                arrDtl.Add(new cArrayList("@item_cd", hdfItem_cd.Value.ToString()));
                arrDtl.Add(new cArrayList("@do_no", Request.Cookies["usr_id"].Value));
                arrDtl.Add(new cArrayList("@qty", lblQty.Text));
                unitPrice = Convert.ToDecimal(Convert.ToDecimal(bll.vLookUp("select unitprice  from tcustomertype_price where cust_typ='BRN' and  item_cd=" + hdfItem_cd.Value.ToString())));
                arrDtl.Add(new cArrayList("@unitprice", unitPrice));
                grantTotal += unitPrice * Convert.ToDecimal(lblQty.Text);
                bll.vUpdateDoDtlHO(arrDtl);

            }
            string sDO = "";
            arr.Clear();
            arr.Add(new cArrayList("@do_dt", DateTime.ParseExact(dtdo.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@po_no", txpono.Text.ToString()));
            arr.Add(new cArrayList("@grandtotal", grantTotal));
            arr.Add(new cArrayList("@whs_cd", cbwarehouse.SelectedValue.ToString()));
            arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@delivery_typ", cbexpedition.SelectedValue.ToString()));
            arr.Add(new cArrayList("@driver_nm", txdrivername.Text));
            arr.Add(new cArrayList("@driver_cd", cbdriver.SelectedValue.ToString()));
            arr.Add(new cArrayList("@comp_cd", cbcompany.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
            arr.Add(new cArrayList("@box_cd", cbbox.SelectedValue.ToString()));
            arr.Add(new cArrayList("@vhc_cd", cbtrella.SelectedValue.ToString()));
            arr.Add(new cArrayList("@manual_no", txmanualinvoice.Text));
            arr.Add(new cArrayList("@docref_no", txgdn.Text));
            arr.Add(new cArrayList("@do_sta_id", "L"));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vInsertMstDOHO(arr, ref sDO);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Data saved succesfully..','Data saved succesfully..','success');", true);
            //bllHO.vInsertMstPO(arr);

            txpono.Text = string.Empty;
            BindGridPO();
            BindGridDODt();
        }
        catch (Exception ex)
        {
            ut.Logs("", "HO Transaction", "PO Transaction", "fm_po_trnasaction", "btsave_Click", "Exception", ex.Message + ex.InnerException);
        }

    }
    protected void cbwarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lbcap.Text = bll.vLookUp("select captionusage from tmst_warehouse where whs_cd='" + cbwarehouse.SelectedValue.ToString() + "'");
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('" + ex.Message + ex.InnerException + "','" + ex.Message + ex.InnerException + "','warning');", true);
            ut.Logs("", "HO Transaction", "PO Transaction", "fm_po_trnasaction", "cbwarehouse_SelectedIndexChanged", "Exception", ex.Message + ex.InnerException);
        }
    }
    protected void cbexpedition_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindTransporatation();
        }
        catch (Exception ex)
        {
            ut.Logs("", "HO Transaction", "PO Transaction", "fm_po_trnasaction", "cbexpedition_SelectedIndexChanged", "Exception", ex.Message + ex.InnerException);
        }
    }

    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        HiddenField hdfItem_cd = (HiddenField)grd.Rows[e.RowIndex].FindControl("hdfItem_cd");
        HiddenField hdfdo_no = (HiddenField)grd.Rows[e.RowIndex].FindControl("hdfdo_no");
        arr.Add(new cArrayList("@do_no", hdfdo_no.Value.ToString()));
        arr.Add(new cArrayList("@item_cd", hdfItem_cd.Value.ToString()));
        bll.vDelDoDtlByItem(arr);
       BindGridDODt();
       ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Data deleted succesfully..','Data deleted succesfully..','success');", true);
    }

    void BindTransporatation()
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@comp_sta_id", cbexpedition.SelectedValue.ToString()));
            bll.vBindingComboToSp(ref cbcompany, "sp_tmst_company_expedition_get", "comp_cd", "comp_nm", arr);
            if (cbexpedition.SelectedValue == "OWN")
            {
                cbdriver.Visible = true;
                cbdriverPanel.Visible = true;
                txdrivername.Visible = false;
                arr.Clear();
                arr.Add(new cArrayList("@qry_cd", "driver"));
                arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue));
                bll.vBindingComboToSp(ref cbdriver, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
            }
            else
            {
                cbdriver.Visible = false;
                cbdriverPanel.Visible = false;
                txdrivername.Visible = true;
                txdrivername.Text = "";
            }
        }
        catch (Exception ex)
        {
            ut.Logs("", "HO Transaction", "PO Transaction", "fm_po_trnasaction", "BindTransporatation", "Exception", ex.Message + ex.InnerException);
        }
    }
    protected void cbdriver_SelectedIndexChanged(object sender, EventArgs e)
    {
        string sVehicle = bll.vLookUp("select vhc_cd from tmst_vehicle where emp_cd='" + cbdriver.SelectedValue.ToString() + "'");
        if (!sVehicle.Equals(""))
        {
            cbtrella.SelectedValue = sVehicle;
        }
    }
    protected void txitemname_TextChanged(object sender, EventArgs e)
    {
        if (txpono.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please select PO..','Please select PO..','warning');", true);
            return;
        }
        else if (hditem.Value == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please select Item..','Please select Item..','warning');", true);
            return;
           
        }
        else {
            var price = bll.vLookUp("select unitprice  from tcustomertype_price where cust_typ='BRN' and  item_cd=" + hditem.Value);
            if (		Convert.ToDecimal(price)==0	)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Price must be greater than 0','Invalid price','warning');", true);
            }
            else
            {
                lblUnitPrice.Text = bll.vLookUp("select unitprice  from tcustomertype_price where cust_typ='BRN' and  item_cd=" + hditem.Value);
                txitemname.Focus();
            }
        }
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('../fm_report2.aspx?src=po_ho&salespointCD=" + cbsalespoint.SelectedValue + "');", true);
    }
    protected void btnSummary_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('../fm_report2.aspx?src=poSummary_ho&salespointCD=" + cbsalespoint.SelectedValue + "');", true);
    }
}