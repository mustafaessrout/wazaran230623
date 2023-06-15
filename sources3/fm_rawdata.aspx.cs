using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_rawdata : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dtstart.Text = Request.Cookies["waz_dt"].Value;
            dtend.Text = Request.Cookies["waz_dt"].Value;
        }
    }

    protected void btsearch_Click(object sender, EventArgs e)
    {
        DateTime _dtstart = System.DateTime.ParseExact(dtstart.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime _dtend = System.DateTime.ParseExact(dtend.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@dtord1", _dtstart));
        arr.Add(new cArrayList("@dtord2", _dtend));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        bll.vBindingGridToSp(ref grd, "rptrawdata", arr);
    }

    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), 
            "openreport('fm_report2.aspx?src=rawdata&s="+dtstart.Text+"&e="+dtend.Text+"');", true);
    }
}