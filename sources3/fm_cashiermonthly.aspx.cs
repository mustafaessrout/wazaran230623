using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_cashiermonthly : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sho;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_getuser", "salespointcd", "salespoint_desc", arr);
            cbsalespoint.Items.Insert(0, new ListItem("<< ALL Branch >>", "-1"));
            cbsalespoint.SelectedValue = Request.Cookies["sp"].Value.ToString();
            sho = Request.Cookies["sp"].Value.ToString();
            if (sho == "0")
            {
                cbsalespoint.Enabled = true;
                cbsalespoint.CssClass = "form-control";
            }
            else
            {
                cbsalespoint.Enabled = false;
                cbsalespoint.CssClass = "makeitreadonly ro form-control";
            }
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        DateTime dtpayp1 = DateTime.ParseExact(dtfrom.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime dtpayp2 = DateTime.ParseExact(dtto.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        string dt1 = dtpayp1.Year.ToString() + "-" + dtpayp1.Month.ToString("00") + "-" + dtpayp1.Day.ToString("00");
        string dt2 = dtpayp2.Year.ToString() + "-" + dtpayp2.Month.ToString("00") + "-" + dtpayp2.Day.ToString("00");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@startdate", dt1));
        arr.Add(new cArrayList("@enddate", dt2));
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
        Session["lParamcsh"] = arr;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=cashiermonthly');", true);

    }
}