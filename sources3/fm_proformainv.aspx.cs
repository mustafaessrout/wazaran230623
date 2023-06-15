using AjaxControlToolkit;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_proformainv : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingComboToSp(ref cbwh, "sp_tmst_warehouse_get", "whs_cd", "whs_nm", arr);
            cbwarehouse_SelectedIndexChanged(sender, e);
            bll.vBindingFieldValueToCombo(ref cbuom, "uom");
            txSalespoint.Text = Request.Cookies["spn"].Value.ToString();
            dtCreated.Text = Request.Cookies["waz_dt"].Value.ToString();
            btprint.Enabled = false;
            btprint.Visible = false;
            btedit.Visible = false;
            btnew.Visible = true;
            btsave.Visible = true;
            btapprove.Visible = false;
            btreject.Visible = false;
            btsave.Text = "Save";
            lbinvno.Text = "NEW";
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vDeltpreinv_dtl(arr);
        }
    }

    private void bindinggrd()
    {
        string inv_no;
        if (lbinvno.Text == "" || lbinvno.Text == "NEW") { inv_no = Request.Cookies["usr_id"].Value.ToString(); } else { inv_no = lbinvno.Text; }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@inv_no", inv_no));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBindingGridToSp(ref grditem, "sp_tpreinv_dtl_get", arr);
        Label lbtotqty = (Label)grditem.FooterRow.FindControl("lbtotqty");
        Label lbtotsubtotal = (Label)grditem.FooterRow.FindControl("lbtotsubtotal");
        lbtotqty.Text = bll.vLookUp("select isnull(sum(qty),0) from tpreinvoice_dtl where inv_no='" + inv_no + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
        lbtotsubtotal.Text = bll.vLookUp("select isnull(sum(qty*unitprice),0) from tpreinvoice_dtl where inv_no='" + inv_no + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");

    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetItemList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        SqlDataReader rs = null;
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
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCustomerList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        System.Data.SqlClient.SqlDataReader rs = null;
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        List<string> lCust = new List<string>();
        string sCust = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cust_cd", prefixText));
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        bll.vGetMstCustomerSpecial(arr, ref rs);
        while (rs.Read())
        {
            sCust = AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + "-" + rs["cust_nm"].ToString(), rs["cust_cd"].ToString());
            lCust.Add(sCust);
        }
        rs.Close();
        return (lCust.ToArray());
    }

    protected void btsearchinv_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ops", "popupwindow('fm_lookupproformainvoice.aspx');", true);
    }

    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_proformainv.aspx");
    }

    protected void btedit_Click(object sender, EventArgs e)
    {
        if (bll.nCheckAccess("proformaedit", Request.Cookies["usr_id"].Value.ToString()) == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','Edit Proforma','warning');", true);
            return;
        }
        btedit.Visible = false;
        btnew.Visible = false;
        btprint.Visible = false;
        btapprove.Visible = false;
        btreject.Visible = false;
        btsave.Text = "Update";

    }

    protected void btsave_Click(object sender, EventArgs e)
    {
        string inv_no;
        string sto_nm = "";
        if (lbinvno.Text == "" || lbinvno.Text == "NEW") { inv_no = Request.Cookies["usr_id"].Value.ToString(); } else { inv_no = lbinvno.Text; }
        //if (btsave.Text == "Update"){inv_no = lbinvno.Text;}else{inv_no = Request.Cookies["usr_id"].Value.ToString();}

        if (lbstatus.Text == "Completed")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction has been complete, data can not save','error');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
            return;
        }
        if (lbinvno.Text == "NEW" || lbinvno.Text == "")
        {
            if (grditem.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Item has not yet selected !','Please select Item To Be Ordered','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
                return;
            }
            List<cArrayList> arr = new List<cArrayList>();
            string sInvNo = "";
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@inv_dt", DateTime.ParseExact(dtCreated.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@whs_cd", cbwh.SelectedValue.ToString()));
            arr.Add(new cArrayList("@bin_cd", cbbin.SelectedValue.ToString()));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@cust_cd", hdcust.Value));
            bll.vInsertMstPreInv(arr, ref sInvNo);

            lbinvno.Text = sInvNo;
            btprint.Enabled = true;
            btprint.Visible = true;

            btsave.Visible = false;
            btnew.Visible = true;
            btedit.Visible = true;
            btadd.Visible = false;
            grditem.Columns[7].Visible = false;
            cbwh.CssClass = "makeitreadonly";
            cbbin.CssClass = "makeitreadonly";
            txcustomer.CssClass = "makeitreadonly";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Pro-Forma Invoice has saved succesfully ..','Pro-Forma No. " + sInvNo + "','success');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
        }
    }

    protected void cbuom_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        string stock = bll.vLookUp("select dbo.[sfnGetStockBooking]('"+hditem.Value.ToString()+"','"+cbbin.SelectedValue.ToString()+"','"+cbwh.SelectedValue.ToString()+"','0',dbo.fn_getsystemdate())");
        string price = bll.vLookUp("select dbo.fn_getadjustmentprice ('" + hditem.Value.ToString() + "','" + hdcust.Value.ToString() + "','" + cbuom.SelectedValue.ToString() + "')");
        txstock.Text = stock;
        txcurrentprice.Text = price;
        txqty.Focus();
    }

    protected void btadd_Click(object sender, EventArgs e)
    {
        double dSumQty = 0;
        double dStock = Convert.ToDouble(txstock.Text.Replace(" ", ""));
        double dQty = Convert.ToDouble(txqty.Text.Replace(" ", ""));
        string inv_no = "";
        List<cArrayList> arr = new List<cArrayList>();

        if (lbstatus.Text == "Completed")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction has been complete, data can not save','error');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
            return;
        }

        if (hditem.Value.ToString() == "" || hditem.Value.Equals(null))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Item Not selected !','Please select item','warning');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
            txitemnamePnl.CssClass = "error";
            return;
        }

        if (lbinvno.Text == "" || lbinvno.Text == "NEW") { inv_no = Request.Cookies["usr_id"].Value.ToString(); } else { inv_no = lbinvno.Text; }

        //if (btsave.Text == "Update")
        //{
        //    inv_no = lbinvno.Text;            
        //}
        //else
        //{
        //    inv_no = Request.Cookies["usr_id"].Value.ToString();
        //}

        dSumQty = Convert.ToDouble(bll.vLookUp("select isnull(sum(qty),0) from tpreinvoice_dtl where inv_no='" + inv_no + "'"));
        if (dStock < (dQty) || dStock <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Stock Not Available.','Create PO Branch / Internal Transfer ','warning');", true);
            return;
        }

        arr.Clear();
        arr.Add(new cArrayList("@inv_no", inv_no));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@item_cd", hditem.Value.ToString()));
        arr.Add(new cArrayList("@uom", cbuom.SelectedValue.ToString()));
        arr.Add(new cArrayList("@qty", txqty.Text));
        arr.Add(new cArrayList("@stock", txstock.Text));
        arr.Add(new cArrayList("@unitprice", txprice.Text));
        arr.Add(new cArrayList("@current_price", txcurrentprice.Text));
        arr.Add(new cArrayList("@expiry_dt", DateTime.ParseExact(dtExpiry.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        bll.vInserttpreinv_dtl_ins(arr);
        arr.Clear();
        bindinggrd();
        txitemname.Text = "";
        txqty.Text = "";
        cbuom.SelectedValue = "";
        txstock.Text = "";
        txcurrentprice.Text = "";
        txprice.Text = "";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "set", "$('#txqty').val('0');", true);
        txitemname.Focus();
    }

    protected void grditem_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string inv_no;

        if (lbinvno.Text == "" || lbinvno.Text == "NEW") { inv_no = Request.Cookies["usr_id"].Value.ToString(); } else { inv_no = lbinvno.Text; }
        if (lbstatus.Text == "Completed")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction has been complete, data can not delete','error');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
            return;
        }

        HiddenField hditemlist = (HiddenField)grditem.Rows[e.RowIndex].FindControl("hditemlist");
        Label lbuom = (Label)grditem.Rows[e.RowIndex].FindControl("lbuom");
        Label lbprice = (Label)grditem.Rows[e.RowIndex].FindControl("lbprice");

        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@item_cd", hditemlist.Value));
        arr.Add(new cArrayList("@uom", lbuom.Text));
        arr.Add(new cArrayList("@unitprice", lbprice.Text));
        arr.Add(new cArrayList("@inv_no", inv_no));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vDeltpreinv_dtl(arr);
        bindinggrd();
    }


    protected void cbwarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@whs_cd", cbwh.SelectedValue.ToString()));
        arr.Add(new cArrayList("@type", "specialbin"));
        bll.vBindingComboToSp(ref cbbin, "sp_twarehouse_bin_get", "bin_cd", "bin_nm", arr);
        //cbbin.SelectedValue = "NES";
    }

    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=proformainv&no=" + lbinvno.Text + "');", true);
    }

    protected void btlookinv_Click(object sender, EventArgs e)
    {
        lbinvno.Text = hdinv.Value.ToString();
        System.Data.SqlClient.SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@inv_no", hdinv.Value.ToString()));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vGetMstPreInv(arr, ref rs);
        while (rs.Read())
        {
            lbstatus.Text = rs["status"].ToString();
            lbinvno.Text = rs["inv_no"].ToString();
            dtCreated.Text = rs["inv_dt"].ToString();
            cbwh.SelectedValue = rs["whs_cd"].ToString();
            cbwarehouse_SelectedIndexChanged(sender, e);
            cbbin.SelectedValue = rs["bin_cd"].ToString();
            txcustomer.Text = rs["cust_cd"].ToString() + " - " + rs["cust_nm"].ToString();            
        }
        rs.Close();
        arr.Clear();
        lbinvno.Text = hdinv.Value.ToString();
        bindinggrd();        
        btsave.Visible = false;
        btedit.Visible = true;
        btprint.Visible = true;
        btprint.Enabled = true;
        btapprove.Visible = true;
        btreject.Visible = true;
    }

    protected void btapprove_Click(object sender, EventArgs e)
    {
        if (bll.nCheckAccess("proformaapp", Request.Cookies["usr_id"].Value.ToString()) == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','Edit Proforma','warning');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@inv_no", hdinv.Value));
        arr.Add(new cArrayList("@status", "A"));
        bll.vUpdMstPreInv(arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Pro-Forma Invoice has approve succesfully ..','Pro-Forma No. " + hdinv.Value + "','success');", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
    }

    protected void btreject_Click(object sender, EventArgs e)
    {
        if (bll.nCheckAccess("proformaapp", Request.Cookies["usr_id"].Value.ToString()) == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','Edit Proforma','warning');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@inv_no", hdinv.Value));
        arr.Add(new cArrayList("@status", "E"));
        bll.vUpdMstPreInv(arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Pro-Forma Invoice has reject succesfully ..','Pro-Forma No. " + hdinv.Value + "','success');", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
    }
}