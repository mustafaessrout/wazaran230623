using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_returbyexp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        cbll bll = new cbll();
        if (!IsPostBack)
        {
            bll.vBindingComboToSp(ref cbbranch, "sp_tmst_salespoint_get", "salespointcd", "salespoint_desc");
            cbbranch.SelectedValue = Request.Cookies["sp"].Value.ToString();
        }
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();        
        arr.Add(new cArrayList("@salespointcd", cbbranch.SelectedValue.ToString()));
        arr.Add(new cArrayList("@month", cbreport.SelectedValue.ToString()));
        //arr.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
        //arr.Add(new cArrayList("@salesman_cd", null));
        //arr.Add(new cArrayList("@cbprod_cdFr", cbProd_cdFr.SelectedValue));
        //arr.Add(new cArrayList("@cbprod_cdTo", cbProd_cdTo.SelectedValue));
        //arr.Add(new cArrayList("@cbprod_cdFrtx", cbProd_cdFr.SelectedItem.Text));
        //arr.Add(new cArrayList("@cbprod_cdTotx", cbProd_cdTo.SelectedItem.Text));
        Session["lParamreturbyexp"] = arr;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=returbyexp');", true);

    }
}