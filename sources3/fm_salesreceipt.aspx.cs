using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_salesreceipt : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            lbperiod.Text = bll.vLookUp("select dbo.fn_getsystemmonthname('"+ Request.Cookies["sp"].Value.ToString() + "')");
            dtreceived.Text = Request.Cookies["waz_dt"].Value.ToString();
            bll.vBindingFieldValueToCombo(ref cbrcpstatus, "rcp_sta_id");
            //arr.Add(new cArrayList("@reasn_typ", "invrcp"));
            //bll.vBindingComboToSp(ref cbreason, "sp_tmst_reason_get", "reasn_cd", "reasn_nm",arr);
           
            arr.Clear();
            arr.Add(new cArrayList("@qry_cd", "driver"));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingComboToSp(ref cbdriver, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
            dtdriverdate.CssClass = "form-control ro";
            dtdriverdate.Enabled = false;
            dtdriverdate.Text = Request.Cookies["waz_dt"].Value.ToString();
            dtreceived.CssClass = "form-control ro";
            dtreceived.Enabled = false;
            dtreceived.Text = Request.Cookies["waz_dt"].Value.ToString();
        }
    }
    protected void btnew_Click(object sender, EventArgs e)
    {        
        Response.Redirect("fm_salesreceipt.aspx");
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "popupwindow('lookup_inv.aspx');",true);
        return;
    }
    protected void btrefresh_Click(object sender, EventArgs e)
    {
        txmanualno.Text = "";
        txcust.Text = "";
        txaddress.Text = "";
        txamt.Text = "";
        txcustname.Text = "";
        txcity.Text = "";

        System.Data.SqlClient.SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        txinvno.Text = hdinv.Value.ToString();
        arr.Add(new cArrayList("@inv_no", hdinv.Value.ToString()));
        bll.vGetDosalesinvoiceReceived(arr, ref rs);
        while (rs.Read())
        { 
            txmanualno.Text = rs["manual_no"].ToString();
            txcust.Text = rs["cust_cd"].ToString();            
            txaddress.Text = bll.vLookUp("select addr from tmst_customer where cust_cd='"+ rs["cust_cd"].ToString() + "'");
            txamt.Text = rs["totamt"].ToString();
            txcustname.Text = rs["cust_nm"].ToString();
            string sDriver = bll.vLookUp("select driver_cd from tmst_dosales where inv_no='"+hdinv.Value.ToString()+"'");
            if (sDriver != "")
            { cbdriver.SelectedValue = sDriver; }
            lbprintdate.Text = Convert.ToDateTime(rs["inv_dt"]).ToString("d/M/yyyy");
            txcity.Text = bll.vLookUp("select loc_nm from tmst_location where loc_cd='"+ rs["city_cd"].ToString() +"'");
            txbalance.Text = rs["balance"].ToString();
           // txstatus.Text = "CREDIT";
            imgsign.ImageUrl = bll.vLookUp("select dbo.fn_getimagepath('" + txcust.Text + "','CR7')");
            imgstamp.ImageUrl = bll.vLookUp("select dbo.fn_getimagepath('" + txcust.Text + "','CR5')");
            txcity.CssClass = "form-control ro";
            txcity.Enabled = false;
            txaddress.CssClass = "form-control ro";
            txaddress.Enabled = false;
            txcustname.CssClass = "form-control ro";
            txcustname.Enabled = false;
            txqtyinv.Text = bll.vLookUp("select sum(qty) from ( select round(dbo.sfnUomQtyConv(item_cd,uom,'CTN',qty),2)qty from tdosalesinvoice_dtl where inv_no='" + hdinv.Value + "' union all select round(dbo.sfnUomQtyConv(item_cd,uom,'CTN',qty),2)qty from tdosalesinvoice_free where inv_no='"+hdinv.Value+"')a"); 

        }
        rs.Close();
        btreceived.Visible = true;
        string sCheck = bll.vLookUp("select count(1) from tdosalesinvoice_info where inv_no='" +hdinv.Value.ToString()+ "' and driver_sta_id='Y'");
        if (Convert.ToInt16(sCheck) == 0)
        {
            //dtdriverdate.CssClass = "form-control ro";
            dtdriverdate.CssClass = "form-control ro";
            dtdriverdate.Enabled = false;
            btreceiptdriver.CssClass = "btn btn-warning";
            lbcap.Text = "Inv has not yet receipt by driver"; cbrcpstatus.CssClass = "form-control"; cbrcpstatus.Enabled = true;
            cbreason.CssClass = "form-control"; cbreason.Enabled = true;
        }
        else
        {
            btreceiptdriver.CssClass = "divhid"; cbreason.CssClass = "form-control ro"; cbreason.Enabled = false;
        dtdriverdate.Text = Convert.ToDateTime(bll.vLookUp("select do_dt from tmst_dosales where inv_no='"+hdinv.Value.ToString()+"'")).ToString("d/M/yyyy");
        lbcap.Text = "Inv already receipt by driver"; cbrcpstatus.CssClass = "form-control ro"; cbrcpstatus.Enabled = false;
        cbreason.CssClass = "form-control ro"; cbreason.Enabled = false;
        }
        dtreceived.CssClass = "form-control ro"; dtreceived.Enabled = false;
        dtreceived.Text = Request.Cookies["waz_dt"].Value.ToString();

        BindingGrid();
    }
    protected void btreceived_Click(object sender, EventArgs e)
    {
        if (hdinv.Value.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alr", "sweetAlert('Please select invoice','Check your invoice to be received','error');", true);
            return;
        }
        if (cbrcpstatus.SelectedValue.ToString() == "A")
        {
            if (dtdriverdate.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alr", "sweetAlert('Please specify driver receipt date','Check received by driver','error');", true);
                return;
            }
        }

        string sReceipt = bll.vLookUp("select count(1) from tdosalesinvoice_info where inv_no='"+hdinv.Value.ToString()+"' and driver_sta_id='N'");
        if (Convert.ToInt16(sReceipt) != 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrx", "sweetAlert('Please receipt by driver','Receipt by driver then received by customer','error');", true);
            return;
        }
        DateTime dtrec = System.DateTime.ParseExact(dtreceived.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime dtdriver = System.DateTime.ParseExact(dtdriverdate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        if (dtrec < dtdriver)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrx2", "sweetAlert('Customer invoice received can not less than driver receipt','Check customer date received','error');", true);
            return;
        }
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@inv_no", hdinv.Value.ToString()));
            arr.Add(new cArrayList("@received_by", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@received_dt", DateTime.ParseExact(dtreceived.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@rcp_sta_id", cbrcpstatus.SelectedValue.ToString()));
            arr.Add(new cArrayList("@reasn_cd", cbreason.SelectedValue.ToString()));
            if (dtdriverdate.Text != "")
            {
                arr.Add(new cArrayList("@receiveddriver_dt", DateTime.ParseExact(dtdriverdate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            }
            bll.vInsertDosalesInvoiceReceived(arr);
            arr.Clear();
            arr.Add(new cArrayList("@inv_no", hdinv.Value.ToString()));
            arr.Add(new cArrayList("@driver_cd", cbdriver.SelectedValue.ToString()));
            bll.vUpdateMstDoSalesByDriverByInv(arr);
            btreceived.Visible = false;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Received by customer has been done','"+hdinv.Value.ToString()+"','success');", true);
            return;
        }
        catch (Exception ex)
        {
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : Save Sales Receipt");
        }
      
    }
    protected void cbrcpstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (cbrcpstatus.SelectedValue.ToString() == "A")
        {
            cbreason.Items.Clear();
            dtdriverdate.Text = Request.Cookies["waz_dt"].Value.ToString();
            btreceiptdriver.CssClass = "btn btn-warning";
            btreceived.Text = "RECEIVED BY CUSTOMER";
            btreceived.CssClass = "btn btn-primary btn-lg";
            
        }
        else if (cbrcpstatus.SelectedValue.ToString() == "L")
        {
            arr.Add(new cArrayList("@reasn_typ", "invrcp"));
            bll.vBindingComboToSp(ref cbreason, "sp_tmst_reason_get", "reasn_cd", "reasn_nm", arr);
            dtdriverdate.Text = "";
            btreceiptdriver.CssClass = "divhid";
            btreceived.Text = "CANCELLED RECEIVED INVOICE";
            btreceived.CssClass = "btn btn-primary btn-lg";
        }
    }
    protected void cbdriver_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbvhc.Text = bll.vLookUp("select vhc_typ from tmst_vehicle where emp_cd='" + cbdriver.SelectedValue.ToString() + "'");
    }
    protected void btreceiptdriver_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        try
        {
            if (dtdriverdate.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Date driver receipt is not set properly','Received Driver Date','warning');", true);
                return;
            }
			if (lbprintdate.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Invoice has not yet print!','Invoice Print','warning');", true);
                return;
            }
            DateTime dtpr = System.DateTime.ParseExact(lbprintdate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime dtrec = System.DateTime.ParseExact(dtdriverdate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            if (dtrec < dtpr)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Date received can not less than print invoice date','Check received driver date','warning');", true);
                return;
            }
            if (txqty.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Quantity can not empty!','Empty Qty','warning');", true);
                return;
            }
            string sqtydrvrcp = bll.vLookUp("select sum(qty) from tdosalesinvoice_driver where inv_no='" + hdinv.Value + "'");
            if (sqtydrvrcp == "") { sqtydrvrcp = "0"; }
            double balqty = Convert.ToDouble(txqtyinv.Text) - Convert.ToDouble(sqtydrvrcp) - Convert.ToDouble(txqty.Text);
            if (balqty < 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Quantity received can not greather than total quantity invoice','Check quantity','warning');", true);
                return;
            }
            arr.Add(new cArrayList("@inv_no", hdinv.Value.ToString()));
            arr.Add(new cArrayList("@emp_cd", cbdriver.SelectedValue.ToString()));
            arr.Add(new cArrayList("@driver_receipt_dt", System.DateTime.ParseExact(dtdriverdate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@qty", txqty.Text));
            bll.vInserttdosalesinvoice_driver(arr);
            BindingGrid();

            double balqtyrcv = Convert.ToDouble(txqtyinv.Text) - Convert.ToDouble(sqtydrvrcp) - Convert.ToDouble(txqty.Text);
            txqty.Text = null; //after add set qty to null
            if (balqtyrcv==0)
            {
                arr.Clear();
                arr.Add(new cArrayList("@inv_no", hdinv.Value.ToString()));
                arr.Add(new cArrayList("@do_dt", System.DateTime.ParseExact(dtdriverdate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@driver_cd", cbdriver.SelectedValue.ToString()));
                arr.Add(new cArrayList("@driver_sta_id", "Y"));
                bll.vUpdateDoSalesINvoiceByDriver(arr);
                btreceiptdriver.CssClass = "divhid";
                btreceived.CssClass = "btn btn-success";
                cbdriver.CssClass = "form-control ro";
                cbdriver.Enabled = false;
                dtdriverdate.CssClass = "form-control ro";
                dtdriverdate.Enabled = false;
                lbcap.Text = "Inv already receipt by driver";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "sweetAlert('Receipt By Driver succedeed','" + cbdriver.SelectedItem.Text + "','success');", true);
            }
        }
        catch (Exception ex)
        {
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : Save Driver Receipt");
        }

    }
    void BindingGrid()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@inv_no", hdinv.Value));
        bll.vBindingGridToSp(ref grd, "sp_tdosalesinvoice_driver_get",arr);
    }
}