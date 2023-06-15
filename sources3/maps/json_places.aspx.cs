using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class maps_json_places : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Data.SqlClient.SqlDataReader rs = null;

        Response.ContentType = "application/json"; //JSON Text output

        List<Places> place = new List<Places>();

        bll.vJSONGetPlaces(ref rs);
        while (rs.Read())
        {
            Places plc = new Places();
            plc.salespoint_cd = rs["salespointcd"].ToString();
            plc.salespoint_nm = rs["salespoint_nm"].ToString();
            plc.tipe = rs["tipe"].ToString();
            plc.name = rs["name"].ToString();
            plc.otlcd = rs["otlcd"].ToString();
            plc.latitude = rs["latitude"].ToString();
            plc.longtitude = rs["longitude"].ToString();            
            place.Add(plc);
        } rs.Close();

        JavaScriptSerializer serializer = new JavaScriptSerializer();
        serializer.MaxJsonLength = Int32.MaxValue; 
        string jsonDataString = serializer.Serialize(place);
        Response.Write(jsonDataString);
    }
}


[Serializable]
public class Places
{
    public string salespoint_cd { get; set; }
    public string salespoint_nm { get; set; }
    public string tipe { get; set; }
    public string name { get; set; }
    public string otlcd { get; set; }
    public string latitude { get; set; }
    public string longtitude { get; set; }

}