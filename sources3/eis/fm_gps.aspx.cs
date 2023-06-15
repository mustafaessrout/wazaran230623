using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Newtonsoft.Json;
using GoogleMaps.LocationServices;
using System.Runtime.Serialization;

public partial class fm_gps : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // BindGMap();
            bll.vBindingComboToSp(ref cbsp, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm");
            cbsp_SelectedIndexChanged(sender, e);
            //var c = Request.QueryString["err"];
            //if (Request.QueryString.Count > 0)
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('No Data Found!','No Salesman Data','warning');", true);
            //}
        }
       
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        //BindGMap();
    }

    public void BindGMap()
    {
        SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        List<ProgramAddresses> AddressList = new List<ProgramAddresses>();
        arr.Add(new cArrayList("@salesman_cd", cbsalesman.SelectedValue.ToString()));
        arr.Add(new cArrayList("@dt", System.DateTime.ParseExact(dtgps.Text,"d/M/yyyy",System.Globalization.CultureInfo.InvariantCulture)));
        bll.vGetMaps(arr, ref rs);
        while (rs.Read())
        {
            ProgramAddresses MapAddress = new ProgramAddresses(rs["cust_cd"].ToString(),@"<b>"+rs["cust_cd"].ToString()+':'+ rs["cust_nm"].ToString() +" ["+rs["otlcd"].ToString()+"]" + @"</b>" +
             "<br/>Canvas:"+bll.vLookUp("select dbo.fn_gettranstab('"+cbsalesman.SelectedValue.ToString()+"','"+System.DateTime.ParseExact(dtgps.Text,"d/M/yyyy",System.Globalization.CultureInfo.InvariantCulture)+"','canvas','"+rs["cust_cd"].ToString()+"')") + "<br/>Take Order:" +
              bll.vLookUp("select dbo.fn_gettranstab('" + cbsalesman.SelectedValue.ToString() + "','" + System.DateTime.ParseExact(dtgps.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture) + "','to','" + rs["cust_cd"].ToString() + "')") + "<br/>Collection:" +
               bll.vLookUp("select dbo.fn_gettranstab('" + cbsalesman.SelectedValue.ToString() + "','" + System.DateTime.ParseExact(dtgps.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture) + "','payment','" + rs["cust_cd"].ToString() + "')") + "<br/>Return:" +
                bll.vLookUp("select dbo.fn_gettranstab('" + cbsalesman.SelectedValue.ToString() + "','" + System.DateTime.ParseExact(dtgps.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture) + "','return','" + rs["cust_cd"].ToString() + "')") , Convert.ToDouble(rs["latitude"]), Convert.ToDouble(rs["longitude"]));
            AddressList.Add(MapAddress);
        }
        rs.Close();
        if (AddressList.Count > 0)
        {
            string jSon = JsonConvert.SerializeObject(AddressList);

            var jsnye = @"var markers=" + jSon;
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), System.Guid.NewGuid().ToString(), jsnye, true);

        }
        else
        {
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('No Data for this salesman!','Pls check on branch','warning');", true);
            //Session["err"] = "No data found!";
            Response.Redirect("fm_gps.aspx");
        }
            //Response.Write("No Data Found"); }
    }

    protected void cbsp_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", cbsp.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbsalesman, "sp_tmst_employee_getbyprofile", "salesman_cd", "emp_desc", arr);
    }

    protected void btview_Click(object sender, EventArgs e)
    {
        BindGMap();
        Response.Write("<div></div>");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "LoadMap();", true);
    }

  }

[DataContract]
public class ProgramAddresses
{
    private string _title;
    private string _description;
    private double _lat;
    private double _lon;
    public ProgramAddresses(string sTitle, string sDesc, double dLat, double dLon)
    {
        description = sDesc;
        lat = dLat;
        lng = dLon;
        title = sTitle;
    }
    [DataMember]
    public string title { set { _title = value; } get { return (_title); } }
    [DataMember]
    public double lat { get { return (_lat); } set { _lat = value; } }
    [DataMember]
    public double lng { get { return (_lon); } set { _lon = value; } }
    [DataMember]
    public string description { get { return (_description); } set { _description = value; } }
}