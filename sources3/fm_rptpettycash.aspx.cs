using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_rptpettycash : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dtstart.Text = Request.Cookies["waz_dt"].Value;
            dtend.Text = Request.Cookies["waz_dt"].Value;
           
            cd.v_disablecontrol(txopeningbalance);
            cd.v_disablecontrol(txendingbalance);
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
            bll.vBindingComboToSpWithEmptyChoosen(ref cbcashierid, "sp_tcashregister_id_get", "cashier_id", "name", arr);
            
        }
    }

    protected void btsearch_Click(object sender, EventArgs e)
    {
        DateTime _startdate = System.DateTime.ParseExact(dtstart.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime _enddate = System.DateTime.ParseExact(dtend.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@start_dt", System.DateTime.ParseExact(dtstart.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@end_dt", System.DateTime.ParseExact(dtend.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        arr.Add(new cArrayList("@cashier_id", cbcashierid.SelectedValue));
        bll.vBindingGridToSp(ref grd, "rptcashieradvance", arr);
        txopeningbalance.Text = bll.vLookUp("select dbo.fn_pettycashbalance('" + cbcashierid.SelectedValue + "','" + _startdate.ToString() + "')");
        txendingbalance.Text = bll.vLookUp("select dbo.fn_pettycashendingbalance('" + cbcashierid.SelectedValue + "','" +_enddate .ToString("M/d/yyyy") + "')");
    }

    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
            "openreport('fm_report2.aspx?src=cashierpettycash&s=" + dtstart.Text+"&e="+dtend.Text+"&c="+cbcashierid.SelectedValue+"');", true);
    }
}