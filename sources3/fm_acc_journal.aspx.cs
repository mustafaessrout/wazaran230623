using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_acc_journal : System.Web.UI.Page
{
    cbll bll = new cbll();
    List<cArrayList> arr = new List<cArrayList>();
    string txPeriod = null;
    string txjournal = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_get", "salespointcd", "salespoint_desc");
            cbsalespoint.SelectedValue = Request.Cookies["sp"].Value.ToString();
            //if (Request.Cookies["sp"].Value.ToString() == "0")
            //{
            //    cbsalespoint.Enabled = true;
            //}
            //else
            //{
            //    cbsalespoint.Enabled = false;
            //}
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
    }
    protected void btprintjournal_Click(object sender, EventArgs e)
    {
        arr.Clear();
        txjournal = txjournalno.Text.ToString();
        arr.Add(new cArrayList("@journal_no", txjournal));
        arr.Add(new cArrayList("@post_periodbyperiodbyuow", null));
        arr.Add(new cArrayList("@post_periodbyperiodalluow", null));
        arr.Add(new cArrayList("@salespointcd", null));
        arr.Add(new cArrayList("p_user", Request.Cookies["fullname"].Value.ToString()));

        Session["lParamjournal"] = arr;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=acc_journal');", true);

    }

    protected void btprintbyperiodbyuow_Click(object sender, EventArgs e)
    {
        arr.Clear();
        txPeriod = txpostperiodbyperiodbyuow.Text.ToString();
        arr.Add(new cArrayList("@post_periodbyperiodbyuow", txPeriod));
        arr.Add(new cArrayList("@journal_no", ""));
        //arr.Add(new cArrayList("@post_periodbyperiodbyuow", txpostperiodbyperiodbyuow.Text));
        arr.Add(new cArrayList("@post_periodbyperiodalluow", ""));
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        arr.Add(new cArrayList("p_user", Request.Cookies["fullname"].Value.ToString()));

        Session["lParamjournalbyperiodbyuow"] = arr;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=acc_journalbyperiodbyuow');", true);

    }
    protected void btprintbyperiodalluow_Click(object sender, EventArgs e)
    {
        arr.Clear();
        txPeriod = txpostperiodbyperiodbyuow.Text.ToString();
        arr.Add(new cArrayList("@post_periodbyperiodalluow", txPeriod));
        arr.Add(new cArrayList("@journal_no", ""));
        arr.Add(new cArrayList("@post_periodbyperiodbyuow", ""));
        //arr.Add(new cArrayList("@post_periodbyperiodalluow", txpostperiodbyperiodalluow.Text));
        arr.Add(new cArrayList("@salespointcd", ""));
        arr.Add(new cArrayList("p_user", Request.Cookies["fullname"].Value.ToString()));

        Session["lParamjournalbyperiodalluow"] = arr;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=acc_journalbyperiodalluow');", true);

    }
}