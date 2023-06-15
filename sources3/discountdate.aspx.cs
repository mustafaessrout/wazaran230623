using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class discountdate : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbdisccode.Text = Request.QueryString["disccode"];
            DateTime dt = DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            dt = dt.AddDays(1);
            lbdelivery_dt.Text = dt.ToString("d/M/yyyy");
            dtend_dt.Text = dt.ToString("d/M/yyyy");
        }
    }

    
    protected void btsave_Click(object sender, EventArgs e)
    {
        if (DateTime.ParseExact(dtend_dt.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture) < DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Expire Date should be greather system date','error');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@disc_cd", lbdisccode.Text));
        arr.Add(new cArrayList("@delivery_dt", DateTime.ParseExact(lbdelivery_dt.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@end_dt",DateTime.ParseExact(dtend_dt.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@disc_sta_id", "A"));
        bll.vUpdateMstDiscount3(arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "window.opener.RefreshData();window.close();", true);
    }

    
    protected void btclose_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "window.close();", true);
    }
}