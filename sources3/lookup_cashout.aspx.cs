using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lookup_cashout : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
          // dtcashout1.Text = Request.Cookies["waz_dt"].Value.ToString();
        }
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        if (dtcashout1.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please select date cashout request','Cashout Date','warning');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cashout_dt", DateTime.ParseExact(dtcashout1.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        bll.vBindingGridToSp(ref grd, "sp_tcashregout_search", arr);
    }
}