using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class eis_fm_contribute : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_getbyall", "salespointcd", "salespoint_nm");
            cbsalespoint_SelectedIndexChanged(sender, e);
            arr.Add(new cArrayList("@level_no", 2));
            bll.vBindingComboToSp(ref cbprodgroup, "sp_tmst_product_getbylevel", "prod_cd", "prod_nm", arr);
            cbprodgroup_SelectedIndexChanged(sender, e);
            bll.vBindingFieldValueToComboWithALL(ref cbchannel, "otlcd");
            arr.Clear();
            //arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
            //arr.Add(new cArrayList("@qry_cd", "SalesJob"));
            //bll.vBindingComboToSp(ref cbsalesman, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
        }
    }
    protected void cbprodgroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@prod_cd_parent", cbprodgroup.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbitem, "sp_tmst_item_getbylevel2", "item_cd", "item_desc", arr);
    }
    protected void cbsalespoint_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        arr.Add(new cArrayList("@qry_cd", "SalesJob"));
        bll.vBindingComboToSp(ref cbsalesman, "sp_tmst_employee_getbyqryall", "emp_cd", "emp_desc", arr);
        cbsalesman.SelectedValue = "ALL";
    }
    protected void btview_Click(object sender, EventArgs e)
    {
        if (dtstart.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('Please start date','Start Date');", true);
            return;
        }
        if (dtend.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('Please End  Date','End Date');", true);
            return;
        }

        if (cbsalespoint.SelectedValue == "ALL")
        {
            lbtotcustomer.Text = bll.vLookUp("select count(1) from tmst_customer");
        }
        else
        { 
            lbtotcustomer.Text = bll.vLookUp("select count(1) from tmst_customer where salespointcd='"+cbsalespoint.SelectedValue.ToString()+"'");
        }
            //lbtotcustbuy.Text = bll.vLookUp("select dbo.fn_checkcustomerbuy('"+System.DateTime.ParseExact(dtstart.Text,"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)+"','"+ System.DateTime.ParseExact(dtend.Text,"d/M/yyyy",System.Globalization.CultureInfo.InvariantCulture)+"','"+cbsalespoint.SelectedValue.ToString()+"','"+cbchannel.SelectedValue.ToString()+"','"+cbsalesman.SelectedValue.ToString()+"','"+cbprodgroup.SelectedValue.ToString()+"','"+cbitem.SelectedValue.ToString()+"')");
        List<cArrayList> arr = new List<cArrayList>();

        arr.Add(new cArrayList("@start_dt", System.DateTime.ParseExact(dtstart.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@end_dt", System.DateTime.ParseExact(dtend.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        arr.Add(new cArrayList("@otlcd", cbchannel.SelectedValue.ToString()));
        arr.Add(new cArrayList("@salesman_cd", cbsalesman.SelectedValue.ToString()));
        arr.Add(new cArrayList("@prod_cd", cbprodgroup.SelectedValue.ToString()));
        arr.Add(new cArrayList("@item_cd", cbitem.SelectedValue.ToString()));

        lbtotcustbuy.Text = bll.dEISGetCustomerBuy(arr).ToString();
        lbtotnotbuy.Text = ( Convert.ToInt16(lbtotcustomer.Text.ToString()) - Convert.ToInt16(lbtotcustbuy.Text.ToString())).ToString();

        bll.vBindingGridToSp(ref grd, "sp_getcustomerbuy", arr);
    }
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd.PageIndex = e.NewPageIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@start_dt", System.DateTime.ParseExact(dtstart.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@end_dt", System.DateTime.ParseExact(dtend.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        arr.Add(new cArrayList("@otlcd", cbchannel.SelectedValue.ToString()));
        arr.Add(new cArrayList("@salesman_cd", cbsalesman.SelectedValue.ToString()));
        arr.Add(new cArrayList("@prod_cd", cbprodgroup.SelectedValue.ToString()));
        arr.Add(new cArrayList("@item_cd", cbitem.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_getcustomerbuy", arr);
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('/fm_report2.aspx?src=cusbuy&sd="+dtstart.Text+"&ed="+dtend.Text+"&sal="+cbsalesman.SelectedValue.ToString()+"&ch="+cbchannel.SelectedValue.ToString()+"&it="+cbitem.SelectedValue.ToString()+"&pd="+cbprodgroup.SelectedValue.ToString()+"');", true);
    }
}