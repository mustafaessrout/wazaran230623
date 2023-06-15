using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_acc_gl : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_get", "salespointcd", "salespoint_desc");
            cbsalespoint.SelectedValue = Request.Cookies["sp"].Value.ToString();
            //if (Request.Cookies["sp"].Value.ToString() == "0")
            //{
            //    cbsalespoint.Enabled = true;
            //}
            //else
            //{
            //    cbsalespoint.Enabled = false;
            //}
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
    }
    protected void btprintbyuow_Click(object sender, EventArgs e)
    {
        if (dtfrom.Text == "" | dtto.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Both Start Date and End Date cannot be empty!','','warning');", true);
        }
        else
        {
            DateTime dtfromP = DateTime.ParseExact(dtfrom.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime dttoP = DateTime.ParseExact(dtto.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            string dtfromStr = dtfromP.Year.ToString() + "-" + dtfromP.Month.ToString("00") + "-" + dtfromP.Day.ToString("00");
            string dttoStr = dttoP.Year.ToString() + "-" + dttoP.Month.ToString("00") + "-" + dttoP.Day.ToString("00");

            if (dtfromP.Year.ToString() != dttoP.Year.ToString())
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please change both Start Date and End Date to be in the same year!','','warning');", true);
            }
            else
            {
                string salesPointName = bll.sGetSalespointname(cbsalespoint.SelectedValue.ToString());

                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@startdate", dtfromStr));
                arr.Add(new cArrayList("@enddate", dttoStr));
                arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
                arr.Add(new cArrayList("p_user", Request.Cookies["spn"].Value.ToString()));
                arr.Add(new cArrayList("p_salesPointName", salesPointName));
                Session["lParamglbyuow"] = arr;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=acc_glbyuow');", true);
            }
        }

    }
    protected void btprintalluow_Click(object sender, EventArgs e)
    {
        if (dtfrom2.Text == "" | dtto2.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Both Start Date and End Date cannot be empty!','','warning');", true);
        }
        else
        {
            DateTime dtfromP2 = DateTime.ParseExact(dtfrom2.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime dttoP2 = DateTime.ParseExact(dtto2.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            string dtfromStr2 = dtfromP2.Year.ToString() + "-" + dtfromP2.Month.ToString("00") + "-" + dtfromP2.Day.ToString("00");
            string dttoStr2 = dttoP2.Year.ToString() + "-" + dttoP2.Month.ToString("00") + "-" + dttoP2.Day.ToString("00");

            if (dtfromP2.Year.ToString() != dttoP2.Year.ToString())
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please change both Start Date and End Date to be in the same year!','','warning');", true);
            }
            else
            {
                cbll bl = new cbll();
                string salesPointName = bl.sGetSalespointname(cbsalespoint.SelectedValue.ToString());

                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@startdate", dtfromStr2));
                arr.Add(new cArrayList("@enddate", dttoStr2));
                //arr.Add(new cArrayList("@salespointcd", null));
                arr.Add(new cArrayList("@salespointcd", "ALL"));
                arr.Add(new cArrayList("p_user", Request.Cookies["spn"].Value.ToString()));
                arr.Add(new cArrayList("p_salesPointName", salesPointName));
                Session["lParamglalluow"] = arr;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=acc_glalluow');", true);
            }
        }
    }
}