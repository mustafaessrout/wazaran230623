using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_appcndnsalesman : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            vBindigGrid();
        }
    }

    void vBindigGrid()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cndn_sta_id", "W"));
        bll.vBindingGridToSp(ref grd, "sp_tcndn_salesman_getbystatus", arr);
    }
    protected void btapprove_Click(object sender, EventArgs e)
    {
        DateTime _dt = System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        Label lbcndncode = (Label)row.FindControl("lbcndncode");
        string _sql = "update tcndn_salesman set cndn_sta_id='C', app_dt='" + _dt.ToString("M/d/yyyy") + "', app_by='" + Request.Cookies["usr_id"].Value + "' where cndn_cd='" + lbcndncode.Text + "'";
        bll.vExecuteSQL(_sql);
        vBindigGrid();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), 
            "sweetAlert('Approved has been done !','"+lbcndncode.Text+"','info');", true);
    }

    protected void btreject_Click(object sender, EventArgs e)
    {
        DateTime _dt = System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        Label lbcndncode = (Label)row.FindControl("lbcndncode");
        string _sql = "update tcndn_salesman set cndn_sta_id='E', app_dt='" + _dt.ToString("M/d/yyyy")+"', app_by='" + Request.Cookies["usr_id"].Value +"' where cndn_cd='" + lbcndncode.Text + "'";
        bll.vExecuteSQL(_sql);
        vBindigGrid();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
          "sweetAlert('Reject has been done !','" + lbcndncode.Text + "','info');", true);
    }
}