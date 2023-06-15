using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class eis_lookupgraph : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            System.Data.SqlClient.SqlDataReader rs = null;

            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", Request.QueryString["sp"]));
            arr.Add(new cArrayList("@start_dt", System.DateTime.ParseExact(Request.QueryString["sd"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@end_dt", System.DateTime.ParseExact(Request.QueryString["ed"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            bll.vRptSalesBranches(arr, ref rs);
            arr.Clear();
            while (rs.Read())
            {
                arr.Add(new cArrayList(rs["item_cd"].ToString(), rs["qty"].ToString()));
            }
            rs.Close();
            string[] x = new string[arr.Count];
            double[] y = new double[arr.Count];
            for (int i = 0; i < arr.Count; i++)
            {
                x[i] = arr[i].paramid;
                y[i] = Convert.ToDouble(arr[i].ParamValue);
            }

            charteis.Series[0].Points.DataBindXY(x, y);
            charteis.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Pie;
            charteis.ChartAreas["charteis"].Area3DStyle.Enable3D = true;
            charteis.ChartAreas["charteis"].AxisX.Interval = 1;
        }
    }
}