using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class maps_json_infocustomer : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Data.SqlClient.SqlDataReader rs = null;

        Response.ContentType = "application/json"; //JSON Text output

        List<InfoCustomer> customer = new List<InfoCustomer>();

        bll.vJSONGetInfoCustomer(ref rs);
        while (rs.Read())
        {
            InfoCustomer cust = new InfoCustomer();
            cust.allcode = rs["allcode"].ToString();
            cust.cust_cd = rs["cust_cd"].ToString();
            cust.cust_nm = rs["cust_nm"].ToString();
            cust.salespoint_nm = rs["salespoint_nm"].ToString();
            cust.salespoint_cd = rs["salespointcd"].ToString();
            cust.latitude = rs["latitude"].ToString();
            cust.longitude = rs["longitude"].ToString();
            cust.otlcd = rs["otlcd"].ToString();
            cust.cusgrcd = rs["cusgrcd"].ToString();
            cust.cusgrnm = rs["cusgrnm"].ToString();
            cust.salesman_cd = rs["salesman_cd"].ToString();
            cust.salesman_nm = rs["salesman_nm"].ToString();
            cust.status = rs["status"].ToString();
            customer.Add(cust);
        } rs.Close();

        JavaScriptSerializer serializer = new JavaScriptSerializer();
        string jsonDataString = serializer.Serialize(customer);
        Response.Write(jsonDataString);
    }

}

[Serializable]
public class InfoCustomer
{
    public string allcode { get; set; }
    public string cust_cd { get; set; }
    public string cust_nm { get; set; }
    public string salespoint_cd { get; set; }
    public string salespoint_nm { get; set; }
    public string latitude { get; set; }
    public string longitude { get; set; }
    public string otlcd { get; set; }
    public string cusgrcd { get; set; }
    public string cusgrnm { get; set; }
    public string salesman_cd { get; set; }
    public string salesman_nm { get; set; }
    public string status { get; set; }

}