using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_duedatereport : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_get", "salespointcd", "salespoint_desc");
            cbsalespoint.SelectedValue = Request.Cookies["sp"].Value.ToString();
            if (Request.Cookies["sp"].Value.ToString() == "0")
            {
                cbsalespoint.Enabled = true;
            }
            else
            {
                cbsalespoint.Enabled = false;
            }
            bll.vBindingFieldValueToCombo(ref cbtop,"payment_term");
        }
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@SalesPointCD", cbsalespoint.SelectedValue));
        if (chall.Checked)
        {
            arr.Add(new cArrayList("@payment_term", null));
        }
        else
        {
            arr.Add(new cArrayList("@payment_term", cbtop.SelectedValue));
        }      
        arr.Add(new cArrayList("@enddate", DateTime.ParseExact(Request.Cookies["waz_dt"].Value, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToShortDateString()));               
        Session["lParamcolldudt"] = arr;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(),Guid.NewGuid().ToString(), "HideProgress();openreport('fm_report2.aspx?src=collduedt&rptyp=" + cbtype.SelectedValue + "');", true);
    }
    protected void chall_CheckedChanged(object sender, EventArgs e)
    {
        if (chall.Checked)
        {
            cbtop.Enabled = false;
        }
        else
        {
            cbtop.Enabled = true;
        }
        
    }
}