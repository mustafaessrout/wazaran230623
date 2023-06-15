using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class maps_json_branch : System.Web.UI.Page
{
    cbll bll = new cbll();

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Data.SqlClient.SqlDataReader rs = null;

        Response.ContentType = "application/json"; //JSON Text output

        List<Branch> branch = new List<Branch>();

        bll.vJSONGetSalespoint(ref rs);
        while (rs.Read())
        {
            Branch brn = new Branch();
            brn.salespoint_cd = rs["salespointcd"].ToString();
            brn.salespoint_nm = rs["salespoint_nm"].ToString();
            brn.tipe = rs["tipe"].ToString();
            brn.name = rs["name"].ToString();
            brn.otlcd = rs["otlcd"].ToString();
            brn.latitude = rs["latitude"].ToString();
            brn.longitude = rs["longitude"].ToString();
            brn.salesman = rs["salesman"].ToString();
            branch.Add(brn);
        } rs.Close();

        JavaScriptSerializer serializer = new JavaScriptSerializer();
        string jsonDataString = serializer.Serialize(branch);
        Response.Write(jsonDataString);

    }
}

[Serializable]
public class Branch
{
    public string salespoint_cd { get; set; }
    public string salespoint_nm { get; set; }

    public string tipe { get; set; }

    public string name { get; set; }

    public string otlcd { get; set; }

    public string latitude { get; set; }

    public string longitude { get; set; }
    public string salesman { get; set; }

}

