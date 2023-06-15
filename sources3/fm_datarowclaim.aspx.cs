using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_datarowclaim : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        cbll bll = new cbll();
        bll.vBindingComboToSp(ref cbYear, "sp_tmst_period_getbyyear", "yearvalue", "yearvalue");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
    }

    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=darowclaimdetail&year=" + cbYear.SelectedValue.ToString() + "');", true);
    }
}