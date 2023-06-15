using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_rptcashier : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            dtcashier.Text = Request.Cookies["waz_dt"].Value.ToString();
        }
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        DateTime dts = DateTime.ParseExact(dtcashier.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        string dtcash = dts.Year.ToString() + "-" + dts.Month.ToString() + "-" + dts.Day.ToString();
        //arr.Add(new cArrayList("@closing_dt", DateTime.ParseExact( dtcashier.Text,"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        //arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));

        //if (!Convert.ToBoolean(bll.vLookUp("select dbo.fn_checkrptclosingcashier()"))) 
        //{
        //bll.vBatchBfrClosingCashier(arr);
        //}else{
        //bll.vBatchClosingCashier(arr);
        //}
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=csh&tg=" + dtcash + "');", true);
    }
}