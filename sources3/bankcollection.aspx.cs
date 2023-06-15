using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class bankcollection : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btprint_Click(object sender, EventArgs e)
    {
        if (dtfrom.Text == "" || dtto.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please Insert Start Date  and End date','Date Error !!','warning');", true);
            return;
        }
   
        DateTime dtfrom1 = DateTime.ParseExact(dtfrom.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime dtto1 = DateTime.ParseExact(dtto.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@start_dt", dtfrom1.Year + "-" + dtfrom1.Month + "-" + dtfrom1.Day));
        arr.Add(new cArrayList("@end_dt", dtto1.Year + "-" + dtto1.Month + "-" + dtto1.Day));
        if (txamount.Text == "")
        {
            arr.Add(new cArrayList("@totamt", null));
        }
        else
        {
            arr.Add(new cArrayList("@totamt", txamount.Text));
        }
        if (txdepositno.Text == "")
        {
            arr.Add(new cArrayList("@deposit_no", null));
        }
        else
        {
            arr.Add(new cArrayList("@deposit_no", txdepositno.Text));
        }

        bll.vBindingGridToSp(ref grd, "sp_bankcollectionreport", arr);
    }
}