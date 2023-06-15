using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_collection : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingComboToSp(ref cbsalespointcd, "sp_tmst_salespoint_get", "salespointcd", "salespoint_desc");
            cbsalespointcd.SelectedValue = Request.Cookies["sp"].Value;
            cbsalespointcd.Enabled = false;            
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@monthcd", "12"));
            bll.vBindingComboToSp(ref cbperiod, "sp_tblTRYearMonth_get", "ymtEND", "yearcd", arr);
        }
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        try
        {
            //DateTime dt = Convert.ToDateTime(bll.sFormat2ddmmyyyy(cbperiod.SelectedValue.ToString()));
            //DateTime dt = DateTime.ParseExact(cbperiod.SelectedValue.ToString(), "M/d/yyyy", System.Globalization.CultureInfo.InvariantCulture);        
            TextBox tx = new TextBox();

            DateTime dt = new DateTime();
            dt = Convert.ToDateTime(cbperiod.SelectedValue.ToString());
            //tx.Text = dt.ToShortDateString();
            tx.Text = dt.ToString("d/M/yyyy");
            //tx.Text = tx.Text.ToString("d/M/yyyy");
            bll.sFormat2ddmmyyyy(ref tx);

            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", cbsalespointcd.SelectedValue.ToString()));
            bll.vDeletetsoa_collection(arr);
            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", cbsalespointcd.SelectedValue.ToString()));
            arr.Add(new cArrayList("@dt", DateTime.ParseExact(tx.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            bll.vbatchtsoa_collection(arr);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=collec&spcd=" + cbsalespointcd.SelectedValue.ToString() + " &dt=" + tx + "');", true);
        }
        catch (Exception ex)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Clear();
            arr.Add(new cArrayList("@err_source", "Collection Rpt"));
            arr.Add(new cArrayList("@err_description", ex.Message));
            bll.vInsertErrorLog(arr);

        }
    }
}