using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_datarowho : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            bll.vBindingComboToSp(ref cbbranch, "sp_tmst_salespoint_get ", "salespointcd", "salespoint_nm");
            cbbranch.Items.Insert(0, new ListItem("All Branch", "ALL"));
        }
    }

    protected void btprint_Click(object sender, EventArgs e)
    {

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('/fm_report2.aspx?src=darow&date=" + dtdata.Text + "&date1=" + dtdata1.Text + "&type=" + CBTYPE.SelectedValue.ToString() + "');", true);
    }
}