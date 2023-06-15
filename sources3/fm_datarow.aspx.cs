using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_datarow : System.Web.UI.Page
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
            bll.vBindingComboToSp(ref cbSalesPointCD, "sp_tmst_salespoint_getuser", "salespointcd", "salespoint_desc", arr);
            cbSalesPointCD.Items.Insert(0, new ListItem("<< ALL Branch >>", "-1"));

            cbSalesPointCD.SelectedValue = Request.Cookies["sp"].Value.ToString();
            sho = Request.Cookies["sp"].Value.ToString();
            if (sho == "0")
            {
                cbSalesPointCD.Enabled = true;
                //cbSalesPointCD.CssClass = "";
                //cbSalesPointCD.Items.RemoveAt(0);
            }
            else
            {
                cbSalesPointCD.Enabled = false;
                cbSalesPointCD.CssClass = "makeitreadonly ro form-control";
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
        }
        
    }

    protected void btprint_Click(object sender, EventArgs e)
    {
        DateTime dtfrom = Convert.ToDateTime(DateTime.ParseExact(dtdata.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture));
        DateTime dtto = Convert.ToDateTime(DateTime.ParseExact(dtdata1.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture));

        if (dtfrom != dtto)
        {
            if (Request.Cookies["sp"].Value.ToString() == "0")
            {
                dtdata1.Text = dtdata.Text;
            }
        }

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=darow&date=" + dtdata.Text + "&date1=" + dtdata1.Text + "&type=" + CBTYPE.SelectedValue.ToString() + "&sp="+ cbSalesPointCD.SelectedValue.ToString() +"');", true);
    }

}