using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_claimexpenserep : System.Web.UI.Page
{

    cbll bll = new cbll();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dtstart.Text = String.Format("{0:M/d/yyyy}", DateTime.Now);
            dtend.Text = String.Format("{0:M/d/yyyy}", DateTime.Now);
            cbType_SelectedIndexChanged(sender, e);   
        }
    }

    protected void cbType_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@dtstart", DateTime.ParseExact(dtstart.Text, "M/d/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@dtend", DateTime.ParseExact(dtend.Text, "M/d/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        if (cbType.SelectedValue.ToString() == "CO")
        {
            grdcashout.Visible = true;
            grdcn.Visible = false;
            bll.vBindingGridToSp(ref grdcashout, "sp_tproposal_paid_get",arr);
        }else
        {
            grdcn.Visible = true;
            grdcashout.Visible = false;
            bll.vBindingGridToSp(ref grdcn, "sp_tclaim_reqcndn_get",arr);
        }
    }
    protected void grdcashout_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdcashout.PageIndex = e.NewPageIndex;
        cbType_SelectedIndexChanged(sender, e); 
    }
    protected void grdcn_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdcn.PageIndex = e.NewPageIndex;
        cbType_SelectedIndexChanged(sender, e); 
    }
    protected void btsearch_ServerClick(object sender, EventArgs e)
    {
        cbType_SelectedIndexChanged(sender, e); 
    }
    protected void btprint_ServerClick(object sender, EventArgs e)
    {
        string dtYearStart, dtMonthStart, dtDayStart, dtStart;
        string dtYearEnd, dtMonthEnd, dtDayEnd, dtEnd;
        dtYearStart = DateTime.ParseExact(dtstart.Text, "M/d/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year.ToString();
        dtMonthStart = DateTime.ParseExact(dtstart.Text, "M/d/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month.ToString();
        dtDayStart = DateTime.ParseExact(dtstart.Text, "M/d/yyyy", System.Globalization.CultureInfo.InvariantCulture).Day.ToString();
        dtStart = dtYearStart + "-" + dtMonthStart + "-" + dtDayStart;

        dtYearEnd = DateTime.ParseExact(dtend.Text, "M/d/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year.ToString();
        dtMonthEnd = DateTime.ParseExact(dtend.Text, "M/d/yyyy", System.Globalization.CultureInfo.InvariantCulture).Month.ToString();
        dtDayEnd = DateTime.ParseExact(dtend.Text, "M/d/yyyy", System.Globalization.CultureInfo.InvariantCulture).Day.ToString();
        dtEnd = dtYearEnd + "-" + dtMonthEnd + "-" + dtDayEnd;

        if (cbType.SelectedValue.ToString() == "CO")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=cashoutclaimrep&dtstart=" + dtStart + "&dtend=" + dtEnd + "')", true);
        }
        else
        {
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=cndnclaimrep&dtstart=" + dtStart + "&dtend=" + dtEnd + "')", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=cndnclaimrep&dtstart=" + dtStart + "&dtend=" + dtEnd + "')", true);
        }
    }
}