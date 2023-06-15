using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_doloading : System.Web.UI.Page
{
    cbll bll = new cbll();
    decimal totalqty_order = 0, totalqtydeliver = 0, totalqty = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@qry_cd", "driver"));
            bll.vBindingComboToSp(ref cbemp_cd, "sp_tmst_employee_getbyqry","emp_cd" , "emp_desc", arr);
            arr.Clear();
            arr.Add(new cArrayList("@do_sta_id", "L"));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_tmst_do_get", arr);
         //   arr.Clear();
           // arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingComboToSp(ref cbwhs, "sp_tmst_warehouse_getbywhsdo", "whs_cd", "whs_nm");
            // by yanto 5-8-15
            bll.vBindingFieldValueToCombo(ref cbdeliverytype, "delivery_typ");
            cbdeliverytype_SelectedIndexChanged(sender, e);
            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
            //arr.Add(new cArrayList("@qry_CD", "Driver"));
            //bll.vBindingComboToSp(ref cbemp_cd, "sp_tmst_employee_sal_get", "emp_cd", "emp_desc", arr);
            bll.vBindingComboToSp(ref cbvehicle, "sp_tmst_vehicle_get", "vhc_cd", "vhc_nm");
            bll.vBindingComboToSp(ref cbtrailerbox, "sp_tmst_trailer_box_get","box_cd","box_nm");
            btprintload.Visible = true;
            btsave.Visible = true;
            btprintinvo.Visible = false;
            //******************
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tablePageCopy", "tablePageCopy();", true);
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        System.Data.SqlClient.SqlDataReader rs = null;
        Label lbdono = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbdono");
        Label lbdelivery_typ = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbdelivery_typ");
        Label lbcomp_cd = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbcomp_cd");
        Label lbdriver_name = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbdriver_name");
        string sDoStatus = bll.vLookUp("select do_sta_id from tmst_do where do_no='" + lbdono.Text + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
        lbmstdono.Text = lbdono.Text;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@do_no", lbdono.Text));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vGetMstDO(arr, ref rs);
        while (rs.Read())
        {
            cbwhs.SelectedValue = rs["whs_cd"].ToString();
            cbdeliverytype.SelectedValue = rs["delivery_typ"].ToString();
            cbdeliverytype_SelectedIndexChanged(sender, e);
            cbexpedition.SelectedValue = rs["comp_cd"].ToString();
            if (!rs["driver_cd"].Equals(DBNull.Value))
            {
                cbemp_cd.SelectedValue = rs["driver_cd"].ToString();
            }
            txdriver_name.Text = rs["driver_name"].ToString();
            cbvehicle.SelectedValue = rs["vhc_cd"].ToString();
            cbtrailerbox.SelectedValue = rs["box_cd"].ToString();
        } rs.Close();
        //cbdeliverytype.CssClass = "makeitreadonly";
        cbwhs.CssClass = "makeitreadonly";
        cbwhs.Enabled = false;
        //cbdeliverytype.Enabled = false;
        totalqty_order = 0;
        totalqty = 0;
        totalqtydeliver = 0;
        bll.vBindingGridToSp(ref grddtl, "sp_tdo_dtl_get", arr);

        cbwhs_SelectedIndexChanged(sender, e);

        string customer = "", typeCustomer = "";string[] cust_cd;
        customer = bll.vLookUp("select b.to_nm from tmst_do a left join tmst_po b on a.po_no = b.po_no where a.do_no = '"+lbdono.Text+"' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "' and b.delivery_typ = 'CU' ");
        if (customer != "")
        {
            cust_cd = customer.Replace(" ","").Split('-');
            typeCustomer = bll.vLookUp("select cuscate_cd from tmst_customer where cust_cd='" + cust_cd[0] + "'");
            if (typeCustomer == "SP")
            {
                foreach (GridViewRow row in grddtl.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        DropDownList cbbin = (DropDownList)row.FindControl("cbbin");
                        DropDownList cbuom = (DropDownList)row.FindControl("cbuom");
                        Label lbuom = (Label)row.FindControl("lbuom");
                        if (typeCustomer == "SP")
                        {
                            cbbin.SelectedValue = "NE";
                            cbuom.SelectedValue = lbuom.Text;
                            cbbin.Enabled = false;
                            cbuom.Enabled = true;
                        }
                        else
                        {
                            cbbin.SelectedValue = "NE";
                            cbuom.SelectedValue = lbuom.Text;
                            cbbin.Enabled = false;
                            cbuom.Enabled = false;
                        }
                        
                    }
                }
            }
            if (typeCustomer == "SP") { btadd.Enabled = false; }
        }
        lbstatus.Text = bll.vLookUp("select fld_desc from tfield_value where fld_nm='do_sta_id' and fld_valu='" + sDoStatus +  "'");
        if (sDoStatus == "D")
        { btprintinvo.Enabled = true;
        btsave.Enabled = false;
        }
        else { btsave.Enabled = true; btprintinvo.Enabled = false;
        //if (lbdelivery_typ.Text != "") { cbdeliverytype.SelectedValue = lbdelivery_typ.Text; }
        //if (lbcomp_cd.Text != "") { cbexpedition.SelectedValue = lbcomp_cd.Text; }

        }
        //btsave.Enabled = false;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tablePageCopy", "tablePageCopy();", true);
    }
    protected void grddtl_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbqty_order = (Label)e.Row.FindControl("lbqty_order");
            TextBox txqtydeliver = (TextBox)e.Row.FindControl("txqtydeliver");
            Label lbqty = (Label)e.Row.FindControl("lbqty");
            DropDownList cbuom = (DropDownList)e.Row.FindControl("cbuom");

            decimal qty_order, qtydeliver, qty;
            if (lbqty_order != null)
                qty_order = decimal.Parse(lbqty_order.Text);
            else
                qty_order = 0;
            totalqty_order = totalqty_order + qty_order;

            if (txqtydeliver != null)
                qtydeliver = decimal.Parse(txqtydeliver.Text);
            else
                qtydeliver = 0;
            totalqtydeliver = totalqtydeliver + qtydeliver;

            if (lbqty != null)
                qty = decimal.Parse(lbqty.Text);
            else
                qty = 0;
            totalqty = totalqty + qty;

            cbuom.SelectedValue = "CTN";
            cbuom.Enabled = false;

        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblTotalqty_order = (Label)e.Row.FindControl("lblTotalqty_order");
            Label lbltotalqtydeliver = (Label)e.Row.FindControl("lbltotalqtydeliver");
            Label lblTotalqty = (Label)e.Row.FindControl("lblTotalqty");
            lblTotalqty_order.Text = totalqty_order.ToString("#,##0.00");
            lbltotalqtydeliver.Text = totalqtydeliver.ToString("#,##0.00");
            lblTotalqty.Text = totalqty.ToString("#,##0.00");
        }
    }
    
    protected void grddtl_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
      //  ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "alert('setan');", true);
    }
    protected void cbwhs_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@whs_cd", cbwhs.SelectedValue.ToString()));
        foreach (GridViewRow row in grddtl.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                DropDownList cbbin = (DropDownList)row.FindControl("cbbin");
                DropDownList cbuom = (DropDownList)row.FindControl("cbuom");
                bll.vBindingComboToSp(ref cbbin,"sp_twarehouse_bin_get" ,"bin_cd", "bin_nm", arr);
                bll.vBindingFieldValueToCombo(ref cbuom, "ne_special", "uom");
            }
        }
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        if (grddtl.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(Page,Page.GetType(), "al", "alert('Please select first DO will be delivered !');", true);
            return;
        }
        Label lbdono = (Label)grd.SelectedRow.FindControl("lbdono");
        //string sSalespoint = bll.vLookUp("select salespointcd from tmst_do where do_no='" + lbdono.Text + "'");
        string sPo = bll.vLookUp("select po_no from tmst_do where do_no='" + lbdono.Text + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
        List<cArrayList> arr = new List<cArrayList>();
        foreach (GridViewRow row in grddtl.Rows)
        {
            Label lbitemcode = (Label)row.FindControl("lbitemcode");
            TextBox txqtydeliver = (TextBox)row.FindControl("txqtydeliver");
            DropDownList cbuom = (DropDownList)row.FindControl("cbuom");
            arr.Clear();
            arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
            arr.Add(new cArrayList("@qty", txqtydeliver.Text));
            arr.Add(new cArrayList("@do_no", lbdono.Text));
            arr.Add(new cArrayList("@uom", cbuom.SelectedValue.ToString())); 
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vUpdateDoDtl(arr);
            arr.Clear();
            arr.Add(new cArrayList("@do_no", lbdono.Text));
            arr.Add(new cArrayList("@do_sta_id", "D"));
            arr.Add(new cArrayList("@delivery_typ",cbdeliverytype.SelectedValue));
            arr.Add(new cArrayList("@comp_cd",cbexpedition.SelectedValue ));
            arr.Add(new cArrayList("@driver_name", txdriver_name.Text));
            arr.Add(new cArrayList("@vhc_cd", cbvehicle.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vUpdateMstDo(arr);
        }
        arr.Clear();
        arr.Add(new cArrayList("@po_no", sPo));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBatchDO(arr);
        btsave.CssClass = "divhid";
        btprintinvo.Visible = true;
        lbstatus.Text = bll.vLookUp("select fld_desc from tfield_value where fld_nm='do_sta_id' and fld_valu='D'");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "alert('Invoice has been generated !');", true);
    }
    protected void btprintload_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>() ;
        Label lbdono = (Label)grd.SelectedRow.FindControl("lbdono");
        arr.Add(new cArrayList("@do_no", lbdono.Text));
        Session["lParamdoload"] = arr;
        Response.Redirect("fm_report.aspx?src=doload");
    }
    protected void btprintinvo_Click(object sender, EventArgs e)
    {
        Label lbdono = (Label)grd.SelectedRow.FindControl("lbdono");
        string sInvoice = bll.vLookUp("select invoice_no from tmst_do where do_no='" + lbdono.Text + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
        Response.Redirect("fm_report.aspx?src=doinv");
    }
    //by yanto 5-8-15
    protected void cbdeliverytype_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@comp_sta_id", cbdeliverytype.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbexpedition, "sp_tmst_company_expedition_get", "comp_cd", "comp_nm", arr);
        if (cbdeliverytype.SelectedValue=="OWN")
        {
            cbemp_cd.Visible = true;
            txdriver_name.Visible = false;
        }
        else
        {
            cbemp_cd.Visible = false;
            txdriver_name.Visible = true;
        }
    }
    //************
    protected void cbemp_cd_SelectedIndexChanged(object sender, EventArgs e)
    {
        txdriver_name.Text = bll.vLookUp("select emp_nm from tmst_employee where emp_cd='"+cbemp_cd.SelectedValue+"'"); 
    }
    protected void grd_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll(); System.Data.SqlClient.SqlDataReader rs = null;
        string sItem = string.Empty;
        List<string> lItem = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@item_nm", prefixText));
        bll.vSearchMstItem2(arr, ref rs);
        while (rs.Read())
        {
            sItem = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["item_cd"].ToString() + "-" + rs["item_nm"].ToString() + "'-" + rs["size"].ToString(), rs["item_cd"].ToString());
            lItem.Add(sItem);
        }
        rs.Close();
        return (lItem.ToArray());
    }

    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd.PageIndex = e.NewPageIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Clear();
        arr.Add(new cArrayList("@qry_cd", "driver"));
        bll.vBindingComboToSp(ref cbemp_cd, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
        arr.Clear();
        arr.Add(new cArrayList("@do_sta_id", "L"));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString())); 
        bll.vBindingGridToSp(ref grd, "sp_tmst_do_get", arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tablePageCopy", "tablePageCopy();", true);
    }

    
}