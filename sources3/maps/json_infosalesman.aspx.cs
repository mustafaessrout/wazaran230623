using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class maps_json_infosalesman : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Data.SqlClient.SqlDataReader rs = null;

        Response.ContentType = "application/json"; //JSON Text output

        List<InfoSalesman> salesman = new List<InfoSalesman>();

        bll.vJSONGetInfoSalesman(ref rs);
        while (rs.Read())
        {
            InfoSalesman sls = new InfoSalesman();
            sls.emp_cd = rs["emp_cd"].ToString();
            sls.emp_nm = rs["emp_nm"].ToString();
            sls.salespoint_nm = rs["salespoint_nm"].ToString();
            sls.mobile_no = rs["mobile_no"].ToString();
            sls.tot_qty = rs["tot_qty"].ToString();
            sls.tot_sales = rs["tot_sales"].ToString();
            sls.tot_collection = rs["tot_collection"].ToString();
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
public class InfoSalesman
{
    public string emp_cd { get; set; }
    public string emp_nm { get; set; }
    public string salespoint_nm { get; set; }
    public string mobile_no { get; set; }
    public string tot_qty { get; set; }
    public string tot_sales { get; set; }
    public string tot_collection { get; set; }

}