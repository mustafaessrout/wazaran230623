using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Adminbranch_fm_advancedtimer : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            string sSP = bll.sGetControlParameter("salespoint");
            lbbranch.Text = sSP + ':'+ bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd='"+sSP+"'");
            DateTime dt=Convert.ToDateTime( bll.sGetControlParameter("time_deadline_closing"));
                string stime = dt.ToString("HH:mm");
                string sdate = dt.Year + "-" + dt.Month + "-" + dt.Day;

                txtime.Text = stime;
                txdate.Text = sdate;
        }
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        DateTime d = Convert.ToDateTime(txdate.Text);
        string sdatetime = d.Year + "-" + d.Month + "-" + d.Day + " "+ txtime.Text;

        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@parm_nm", "time_deadline_closing"));
        arr.Add(new cArrayList("@parm_valu", sdatetime));
        bll.vUpdateControlParameter(arr);
        txtime.CssClass = "form-control ro";
        txdate.CssClass = "form-control ro";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgSuccess('Time changed!','Advanced Timer');", true);
    }
}