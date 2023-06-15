using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_rp_return : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingComboToSp(ref cbbranch, "sp_tmst_salespoint_get", "salespointcd", "salespoint_desc");
            bll.vBindingComboToSp(ref cbperiod, "sp_tmst_period_get_year", "yearvalue", "yearvalue");            
        }
        
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", cbbranch.SelectedValue.ToString()));
        arr.Add(new cArrayList("@period", cbperiod.SelectedValue.ToString()));
        arr.Add(new cArrayList("@bin_cd", null));
        Session["lParamrtn"] = arr;
        if (rdreporttype.SelectedValue.ToString() == "0")
        {            
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=rtn1');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=rtn2');", true);
        }
        
    }
}