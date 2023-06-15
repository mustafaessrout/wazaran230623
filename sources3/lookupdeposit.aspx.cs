using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

public partial class lookupdeposit : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //vBindingGrid();
            bll.vBindingFieldValueToComboWithChoosen(ref cbstatus, "salesdep_sta_id");
        }
    }

    void vBindingGrid()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        arr.Add(new cArrayList("@salesdep_sta_id" , cbstatus.SelectedValue));
        bll.vBindingGridToSp(ref grd, "sp_salesman_balance_deposit_getbysalespoint", arr);
    }

    protected void btimport_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        Label lbdepositcode = (Label)row.FindControl("lbdepositcode");
        HiddenField hdsalesman = (HiddenField)row.FindControl("hdsalesman");
        Label lbamount = (Label)row.FindControl("lbamount");
        Label lbdepositdate = (Label)row.FindControl("lbdepositdate");
        DateTime _depositdate = System.DateTime.ParseExact(lbdepositdate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        string _deposit = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@tab_deposit_cd", lbdepositcode.Text));
        arr.Add(new cArrayList("@salesman_cd", hdsalesman.Value));
        arr.Add(new cArrayList("@amt", Convert.ToDecimal(lbamount.Text)));
        arr.Add(new cArrayList("@salesdep_sta_id", "N"));
        arr.Add(new cArrayList("@deposit_dt", _depositdate));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        bll.vInsertSalesmanDeposit(arr, ref _deposit);
        string _sql = "update ttab_salesman_balance_deposit set trf_dt=dbo.fn_getsystemdate() where tab_deposit_cd='"+lbdepositcode.Text+"'";
        bll.vExecuteSQL(_sql);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "window.opener.SelectData('" + lbdepositcode.Text + "');window.close();", true);
    }

    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lbdepositcode = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbdepositcode");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "window.opener.SelectData('"+lbdepositcode.Text+"');window.close();", true);
    }

    protected void grd_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void cbstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        vBindingGrid();
    }
}