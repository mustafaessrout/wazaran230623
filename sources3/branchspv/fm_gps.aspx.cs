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
            cbsp.SelectedValue = Request.Cookies["sp"].Value.ToString();
            
            cbsp_SelectedIndexChanged(sender, e);
            cbsp.CssClass = "form-control ro";
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
        bool bCheck = false; string sCanvas = string.Empty; string sTO = string.Empty; string sreturn = string.Empty; ; string sPayment = string.Empty; string sColl = string.Empty;
        string sIcon = string.Empty;
        List<ProgramAddresses> AddressList = new List<ProgramAddresses>();
        arr.Add(new cArrayList("@salesman_cd", cbsalesman.SelectedValue.ToString()));
        arr.Add(new cArrayList("@dt", System.DateTime.ParseExact(dtgps.Text,"d/M/yyyy",System.Globalization.CultureInfo.InvariantCulture)));
        bll.vGetMaps(arr, ref rs);
        while (rs.Read())
        {
            sCanvas = bll.vLookUp("select dbo.fn_gettranstab('" + cbsalesman.SelectedValue.ToString() + "','" + System.DateTime.ParseExact(dtgps.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture) + "','canvas','" + rs["cust_cd"].ToString() + "')");
            sTO = bll.vLookUp("select dbo.fn_gettranstab('" + cbsalesman.SelectedValue.ToString() + "','" + System.DateTime.ParseExact(dtgps.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture) + "','to','" + rs["cust_cd"].ToString() + "')");
            sColl = bll.vLookUp("select dbo.fn_gettranstab('" + cbsalesman.SelectedValue.ToString() + "','" + System.DateTime.ParseExact(dtgps.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture) + "','payment','" + rs["cust_cd"].ToString() + "')");
            sreturn = bll.vLookUp("select dbo.fn_gettranstab('" + cbsalesman.SelectedValue.ToString() + "','" + System.DateTime.ParseExact(dtgps.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture) + "','return','" + rs["cust_cd"].ToString() + "')");
            if ((Convert.ToDouble( sCanvas) == 0) && (Convert.ToDouble( sTO) == 0) && (Convert.ToDouble( sColl) == 0) && (Convert.ToDouble( sreturn) == 0))
            {
                sIcon = bll.sGetControlParameter("link_branch") + "/branchspv/red-dot.png";
            }
            else
            {
                sIcon = bll.sGetControlParameter("link_branch") + "/branchspv/green-dot.png";
            }
            //Response.Write(sIcon);
            ProgramAddresses MapAddress = new ProgramAddresses(rs["cust_cd"].ToString(),@"<b>"+rs["cust_cd"].ToString()+':'+ rs["cust_nm"].ToString() +" ["+rs["otlcd"].ToString()+"]" + @"</b>" +
             "<br/>Canvas:"+bll.vLookUp("select dbo.fn_gettranstab('"+cbsalesman.SelectedValue.ToString()+"','"+System.DateTime.ParseExact(dtgps.Text,"d/M/yyyy",System.Globalization.CultureInfo.InvariantCulture)+"','canvas','"+rs["cust_cd"].ToString()+"')") + "<br/>Take Order:" +
              bll.vLookUp("select dbo.fn_gettranstab('" + cbsalesman.SelectedValue.ToString() + "','" + System.DateTime.ParseExact(dtgps.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture) + "','to','" + rs["cust_cd"].ToString() + "')") + "<br/>Collection:" +
               bll.vLookUp("select dbo.fn_gettranstab('" + cbsalesman.SelectedValue.ToString() + "','" + System.DateTime.ParseExact(dtgps.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture) + "','payment','" + rs["cust_cd"].ToString() + "')") + "<br/>Return:" +
                bll.vLookUp("select dbo.fn_gettranstab('" + cbsalesman.SelectedValue.ToString() + "','" + System.DateTime.ParseExact(dtgps.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture) + "','return','" + rs["cust_cd"].ToString() + "')") , Convert.ToDouble(rs["latitude"]), Convert.ToDouble(rs["longitude"]) , sIcon);
            AddressList.Add(MapAddress);
        }
        rs.Close();
        if (AddressList.Count > 0)
        {
            string jSon = JsonConvert.SerializeObject(AddressList);

            var jsnye = @"var markers=" + jSon;
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), System.Guid.NewGuid().ToString(), jsnye, true);

        }
        else { Response.Redirect("fm_gps.aspx"); }
    }

    protected void cbsp_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", cbsp.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbsalesman, "sp_tmst_employee_getbyprofile", "salesman_cd", "emp_desc", arr);
    }

    protected void btview_Click(object sender, EventArgs e)
    {
        if (dtgps.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select date!','Date Want To Tracking','warning');", true);
            return;
        }
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
    private string _icon;
    public ProgramAddresses(string sTitle, string sDesc, double dLat, double dLon, string sIcon)
    {
        description = sDesc;
        lat = dLat;
        lng = dLon;
        title = sTitle;
        icon = sIcon;
    }
    [DataMember]
    public string icon { set { _icon = value; } get { return (_icon); } }

    [DataMember]
    public string title { set { _title = value; } get { return (_title); } }
    [DataMember]
    public double lat { get { return (_lat); } set { _lat = value; } }
    [DataMember]
    public double lng { get { return (_lon); } set { _lon = value; } }
    [DataMember]
    public string description { get { return (_description); } set { _description = value; } }
}