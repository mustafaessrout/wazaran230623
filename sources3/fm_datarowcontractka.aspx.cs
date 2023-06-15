using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_datarowcontractka : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddMonth.Items.Insert(0, new ListItem("<< ALL Month >>", "ALL"));
            ddYear.Items.Insert(0, new ListItem("<< ALL Year >>", "ALL"));

        }
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=darowagho&month=" + ddMonth.SelectedValue.ToString() + "&year=" + ddYear.SelectedValue.ToString() + "&type=" + CBTYPE.SelectedValue.ToString() + "');", true);
    }

}