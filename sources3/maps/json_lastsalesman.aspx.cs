using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class maps_json_lastsalesman : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Data.SqlClient.SqlDataReader rs = null;

        Response.ContentType = "application/json"; //JSON Text output

        List<Salesman> salesman = new List<Salesman>();

        bll.vJSONGetLastSalesman(ref rs);
        while (rs.Read())
        {
            Salesman sls = new Salesman();
            sls.gps_no = rs["gps_no"].ToString();
            sls.emp_cd = rs["emp_cd"].ToString();
            sls.locdatetime = rs["locdatetime"].ToString();
            sls.distance = rs["distance_tot"].ToString();
            sls.latitude = rs["latitude"].ToString();
            sls.longitude = rs["longitude"].ToString();
            sls.status = rs["status"].ToString();
            salesman.Add(sls);
        } rs.Close();

        JavaScriptSerializer serializer = new JavaScriptSerializer();
        string jsonDataString = serializer.Serialize(salesman);
        Response.Write(jsonDataString);
    }

}

[Serializable]
//public class Salesman
//{
//    public string salespoint_cd { get; set; }
//    public string salespoint_nm { get; set; }
//    public string salesman_cd { get; set; }
//    public string salesman_nm { get; set; }
//    public string latitude { get; set; }
//    public string longtitude { get; set; }
//    public string Locdatetime { get; set; }

//}
public class Salesman
{
    public string gps_no { get; set; }
    public string emp_cd { get; set; }
    public string locdatetime { get; set; }
    public string distance { get; set; }
    public string latitude { get; set; }
    public string longitude { get; set; }
    public string status { get; set; }

}