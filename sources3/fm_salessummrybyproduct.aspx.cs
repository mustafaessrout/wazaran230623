using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_salessummrybyproduct : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txdt1.Text = Request.Cookies["waz_dt"].Value.ToString();
            txdt2.Text = Request.Cookies["waz_dt"].Value.ToString();
        }
    }

    protected void btreport_Click(object sender, EventArgs e)
    {
        DateTime dtdate1 = Convert.ToDateTime(DateTime.ParseExact(txdt1.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture));
        DateTime dtdate12 = new DateTime(dtdate1.Year, dtdate1.Month, dtdate1.Day);
        DateTime dtdate2=  Convert.ToDateTime(DateTime.ParseExact(txdt2.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture));
        DateTime dtdate21 = new DateTime(dtdate2.Year, dtdate2.Month, dtdate2.Day);
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@startdate", dtdate12));
        arr.Add(new cArrayList("@enddate", dtdate21));
        arr.Add(new cArrayList("@salespointcd1", Request.Cookies["sp"].Value.ToString()));
        bll.vInserttmp_sumrysales(arr);
        arr.Clear();
        if (!chCust.Checked)
        {
            arr.Add(new cArrayList("@emp_cd", hdemp.Value));
        }
        else
        {
            arr.Add(new cArrayList("@emp_cd", null));
        }
        arr.Add(new cArrayList("@usr_nm",Request.Cookies["usr_id"].Value.ToString()));
        Session["lParamsumsale"] = arr;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=sumsale');", true);
    }
    protected void chCust_CheckedChanged(object sender, EventArgs e)
    {
        if (chCust.Checked)
        {
            txCust.Enabled = false;
        }
        else
        {
            txCust.Enabled = true;

        }
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        HttpCookie cookieSP;
        cookieSP = HttpContext.Current.Request.Cookies["sp"];
        string sEmp = string.Empty;
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lEmp = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", cookieSP.Value.ToString()));
        arr.Add(new cArrayList("@job_title", "SALESCD"));
        //arr.Add(new cArrayList("@emp_nm", prefixText));
        cbll bll = new cbll();
        bll.vSearchMstEmployee2bysalespoint(arr, ref rs);
        while (rs.Read())
        {
            sEmp = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["emp_desc"].ToString(), rs["emp_cd"].ToString());
            lEmp.Add(sEmp);
        } rs.Close();
        return (lEmp.ToArray());
    }
}