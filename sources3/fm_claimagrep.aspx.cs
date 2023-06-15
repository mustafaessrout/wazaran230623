using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_claimagrep : System.Web.UI.Page
{

    cbll bll = new cbll();


    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        {
            dtstart.Text = String.Format("{0:d/M/yyyy}", DateTime.Now).ToString();
            dtend.Text = String.Format("{0:d/M/yyyy}", DateTime.Now).ToString();
            cbtype_SelectedIndexChanged(sender, e);
            
        }

    }

    protected void grdpayment_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdpayment.PageIndex = e.NewPageIndex;
        cbtype_SelectedIndexChanged(sender, e);
    }

    protected void btsearch_ServerClick(object sender, EventArgs e)
    {
        cbtype_SelectedIndexChanged(sender, e);
    }
    protected void btprint_ServerClick(object sender, EventArgs e)
    {
        DateTime dts = DateTime.ParseExact(dtstart.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime dte = DateTime.ParseExact(dtend.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        string sdate, edate;
        sdate = dts.Year.ToString() + "-" + dts.Month.ToString() + "-" + dts.Day.ToString();
        edate = dte.Year.ToString() + "-" + dte.Month.ToString() + "-" + dte.Day.ToString();

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=agclaimrep&type=" + cbtype.SelectedValue.ToString() + "&dtstart=" + sdate + "&dtend=" + edate + "')", true);
    }
    protected void cbtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        DateTime dts = DateTime.ParseExact(dtstart.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime dte = DateTime.ParseExact(dtend.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        string sdate,edate;
        sdate = dts.Year.ToString() + "-" + dts.Month.ToString() + "-" + dts.Day.ToString();
        edate = dte.Year.ToString() + "-" + dte.Month.ToString() + "-" + dte.Day.ToString();
        
        List<cArrayList> arr = new List<cArrayList>();
        arr.Clear();
        arr.Add(new cArrayList("@type", cbtype.SelectedValue.ToString()));
        arr.Add(new cArrayList("@dtstart", sdate));
        arr.Add(new cArrayList("@dtend", edate));
        bll.vBindingGridToSp(ref grdpayment, "sp_tcontract_payment_get", arr);
    }
}