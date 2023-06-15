using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_mstpaymentpromise : System.Web.UI.Page
{
    
    cbll bll = new cbll();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cbtype.Items.Insert(0, "By Paid");
            cbtype.Items.Insert(1, "By Promise");
            bll.vBindingComboToSp(ref cbbranch, "sp_tmst_salespoint_get", "salespointcd", "salespoint_desc");
            cbbranch.SelectedValue = Request.Cookies["sp"].Value.ToString();
            cbbranch.Attributes.Add("disabled", "disabled");
            cbstatus.Items.Insert(0, "Completed");
            cbstatus.Items.Insert(1, "InComplete");

        }
    }

    protected void btprint_Click(object sender, EventArgs e)
    {
        string type = "";
        if (cbtype.SelectedValue.ToString() == "By Paid") { type = "paid"; } else { type = "promise"; }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=promise&status=" + cbstatus.SelectedValue.ToString() + "&type=" + type + "');", true);
    }

}