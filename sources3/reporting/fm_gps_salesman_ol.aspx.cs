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

public partial class fm_gps_salesman_ol : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@emp_cd", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingComboToSpHO(ref cbsp, "sp_tmst_salespoint_GPS_get", "salespointcd", "salespoint_desc", arr);
            Response.Cookies["spgps"].Value = cbsp.SelectedValue.ToString();
            bindinggrdgps();
        }
    }
    //public static List<object> GetData()
    //{
    //    List<object> lists = new List<object>();
     
    //    System.Data.SqlClient.SqlDataReader rs = null;
    //    string sDoStaID = "";
    //    List<cArrayList> arr = new List<cArrayList>();
    //    arr.Add(new cArrayList("@do_no", hddo.Value.ToString()));
    //    bll.vGetMstDO(arr, ref rs);
    //    while (rs.Read())
    //    {
    //        lists.Add(new
    //        {
    //            Emp_cd = rs["Emp_cd"].ToString();
    //            Locdatetime = rs["Locdatetime"].ToString();
    //            Latitude = rs["Latitude"].ToString();
    //            Longitude = rs["Longitude"].ToString();
    //            Accuracy = rs["Accuracy"].ToString();
    //            nm = rs["nm"].ToString();
    //        });
    //    }
    //    rs.Close();

    //    return lists;
    //}
    public void bindinggrdgps()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", cbsp.SelectedValue.ToString()));
        arr.Add(new cArrayList("@emp_cd_usr", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSpHO(ref grdgps, "sp_gps_salesman_ol_get", arr);
    }
    [System.Web.Services.WebMethod]
    public static List<object> GetData()
    {
        HttpCookie cokusr_id,cokspgps;
        cokusr_id = HttpContext.Current.Request.Cookies["usr_id"];
        cokspgps = HttpContext.Current.Request.Cookies["spgps"];

        List<object> lists = new List<object>();
        string conString = System.Configuration.ConfigurationManager.ConnectionStrings["connstrho"].ConnectionString;
        SqlCommand cmd = new SqlCommand("select s.*,(select emp_cd+'-'+emp_sn from tmst_employee where emp_cd=s.emp_cd)nm,round(dbo.fn_CalcDistanceKM(c.latitude,s.latitude,c.longitude,s.longitude),2)distanceKM,(select salespoint_nm from tmst_salespoint where salespointcd=p.salespointcd)sp_nm,'' office from tsaleslocationol s inner join ttablet_profile p on s.emp_cd=p.salesman_cd inner join  ttab_control c on p.salespointcd=c.salespointcd where  case when  p.salespointcd ='" + cokspgps.Value.ToString() + "' or " + cokspgps.Value.ToString() + "='-1'  then '1' else '0' end =1 	and p.salespointcd in (SELECT salespointcd FROM tsaleslocation_salespoint where emp_cd='" + cokusr_id.Value.ToString() + "') UNION ALL select '0' emp_cd,null locdatetime,latitude,longitude,'0' accuracy,0 processed,'0' nm,0.1 distanceKm,(select salespoint_nm from tmst_salespoint where salespointcd=C.salespointcd)sp_nm,'O' office from ttab_control C");
        using (SqlConnection con = new SqlConnection(conString))
        {
            cmd.Connection = con;
            cmd.Connection.Open();
            using (SqlDataReader sdr = cmd.ExecuteReader())
            {
                while (sdr.Read())
                {
                    lists.Add(new
                    {
                        Emp_cd = sdr["Emp_cd"],
                        Locdatetime = sdr["Locdatetime"].ToString(),
                        Latitude = sdr["Latitude"],
                        Longitude = sdr["Longitude"],
                        Accuracy = sdr["Accuracy"],
                        nm = sdr["nm"],
                        distanceKM = sdr["distanceKM"],
                        sp_nm = sdr["sp_nm"],
                        office=sdr["office"]
                    });
                }
            }
            cmd.Connection.Close();
        }

        return lists;
    }
    protected void cbsp_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Cookies["spgps"].Value =cbsp.SelectedValue.ToString();
        bindinggrdgps();
    }
    protected void btview_Click(object sender, EventArgs e)
    {
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "LoadMap()", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "SetMarker()", true);
    }

    protected void TimerMarker_Tick(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "SetMarker()", true);
        bindinggrdgps();
    }
}
