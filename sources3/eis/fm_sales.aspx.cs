using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class eis_fm_sales : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_getbyall", "salespointcd", "salespoint_nm");
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
        }
    }
    protected void btview_Click(object sender, EventArgs e)
    {
    
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        arr.Add(new cArrayList("@start_dt", System.DateTime.ParseExact(dtstart.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@end_dt", System.DateTime.ParseExact(dtend.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        bll.vBindingGridToSp(ref grd, "sp_rpt_salesbranches", arr);
    }
    protected void btgraph_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('lookupgraph.aspx?sp="+cbsalespoint.SelectedValue.ToString()+"&sd="+dtstart.Text+"&ed="+dtend.Text+"');", true);
        //System.Data.SqlClient.SqlDataReader rs = null;
       
        //List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        //arr.Add(new cArrayList("@start_dt", System.DateTime.ParseExact(dtstart.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        //arr.Add(new cArrayList("@end_dt", System.DateTime.ParseExact(dtstart.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        //bll.vRptSalesBranches(arr, ref rs);
        //arr.Clear();
        //while (rs.Read())
        //{ 
        //    arr.Add(new cArrayList(rs["item_cd"].ToString(),rs["qty"].ToString()));
        //}
        //rs.Close();
        //string[] x = new string[arr.Count];
        //double[] y = new double[arr.Count];
        //for (int i = 0; i < arr.Count; i++)
        //{
        //    x[i] = arr[i].paramid;
        //    y[i] = Convert.ToDouble( arr[i].ParamValue);
        //}
       
        //charteis.Series[0].Points.DataBindXY(x, y);
        //charteis.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Pie;
        //charteis.ChartAreas["charteis"].Area3DStyle.Enable3D = true;
        //charteis.ChartAreas["charteis"].AxisX.Interval = 1;
    }
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd.PageIndex = e.NewPageIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        arr.Add(new cArrayList("@start_dt", System.DateTime.ParseExact(dtstart.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@end_dt", System.DateTime.ParseExact(dtend.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        bll.vBindingGridToSp(ref grd, "sp_rpt_salesbranches", arr);
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('/fm_report2.aspx?src=eissales&sd="+dtstart.Text+"&ed="+dtend.Text+"&sp="+cbsalespoint.SelectedValue.ToString()+"');", true);
    }
}