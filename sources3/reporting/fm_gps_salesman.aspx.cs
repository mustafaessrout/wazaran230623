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

public partial class fm_gps_salesman : System.Web.UI.Page
{
    cbll bll = new cbll();
    double dtotSalesRPSAmt = 0;
    double dtotSalesNRPSAmt = 0;
    double dtotpaymentRPSamt = 0;
    double dtotpaymentNRPSamt = 0;
    double dtotreturRPSamt = 0;
    double dtotreturNRPSamt = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@emp_cd", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingComboToSpHO(ref cbsp, "sp_tmst_salespoint_GPS_get", "salespointcd", "salespoint_desc",arr);
            cbsp_SelectedIndexChanged(sender, e);
            
        }
    }


    public void BindGMapHO()
    {
        //DateTime d = Convert.ToDateTime( dtgps.Text);
        //string s = d.Year + "-" + d.Month + "-" + d.Day;
        SqlDataReader rs = null;
        List<tsaleslocation_tran> AddressList = new List<tsaleslocation_tran>();
        List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@dt", s));
        arr.Add(new cArrayList("@dt", System.DateTime.ParseExact(dtgps.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@salesman_cd", cbsalesman.SelectedValue.ToString()));
        bll.vGetgetmaps_salesman_tran(arr, ref rs);
        while (rs.Read())
        {
            tsaleslocation_tran MapAddressSalesman = new tsaleslocation_tran(rs["salesman_cd"].ToString(), string.Format("{0:yyyy-MM-dd HH:mm:ss}", rs["dt"]), Convert.ToDouble(rs["lng"].ToString()), Convert.ToDouble(rs["lat"].ToString()), rs["trx_typ"].ToString(), rs["trandescription"].ToString(), rs["maptitle"].ToString());
            AddressList.Add(MapAddressSalesman);
        }
        rs.Close();


        if (AddressList.Count > 0)
        {
            string jSon = JsonConvert.SerializeObject(AddressList);

            var jsnye = @"var markers=" + jSon;
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), System.Guid.NewGuid().ToString(), jsnye, true);

        }
        else { Response.Redirect("fm_gps_salesman.aspx"); }
    }


    protected void btview_Click(object sender, EventArgs e)
    {
        if (dtgps.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select date!','Date Want To Tracking','warning');", true);
            return;
        }
        BindGMapHO();
        Response.Write("<div></div>");

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "initMap();", true);

        //List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@dt", System.DateTime.ParseExact(dtgps.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        //arr.Add(new cArrayList("@salesman_cd", cbsalesman.SelectedValue.ToString()));
        //bll.vBindingGridToSpHO(ref grdgps, "sp_getmaps_salesman_tran_summ", arr);
        
    }


    protected void cbsp_SelectedIndexChanged(object sender, EventArgs e)
    {

        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", cbsp.SelectedValue.ToString()));
        arr.Add(new cArrayList("@emp_cd", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingComboToSpHO(ref cbsalesman, "sp_tmst_employee_getbyprofile_accessgps", "salesman_cd", "emp_desc", arr);
    }
    protected void grdgps_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbSalesRPSAmt = (Label)e.Row.FindControl("lbSalesRPSAmt");
            Label lbSalesNRPSAmt = (Label)e.Row.FindControl("lbSalesNRPSAmt");
            Label lbpaymentRPSamt = (Label)e.Row.FindControl("lbpaymentRPSamt");
            Label lbpaymentNRPSamt = (Label)e.Row.FindControl("lbpaymentNRPSamt");
            Label lbreturRPSamt = (Label)e.Row.FindControl("lbreturRPSamt");
            Label lbreturNRPSamt = (Label)e.Row.FindControl("lbreturNRPSamt");
            dtotSalesRPSAmt += Convert.ToDouble(lbSalesRPSAmt.Text);
            dtotSalesNRPSAmt += Convert.ToDouble(lbSalesNRPSAmt.Text);
            dtotpaymentRPSamt += Convert.ToDouble(lbpaymentRPSamt.Text);
            dtotpaymentNRPSamt += Convert.ToDouble(lbpaymentNRPSamt.Text);
            dtotreturRPSamt += Convert.ToDouble(lbreturRPSamt.Text);
            dtotreturNRPSamt += Convert.ToDouble(lbreturNRPSamt.Text);

        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbtotSalesRPSAmt = (Label)e.Row.FindControl("lbtotSalesRPSAmt");
            Label lbtotSalesNRPSAmt = (Label)e.Row.FindControl("lbtotSalesNRPSAmt");
            Label lbtotpaymentRPSamt = (Label)e.Row.FindControl("lbtotpaymentRPSamt");
            Label lbtotpaymentNRPSamt = (Label)e.Row.FindControl("lbtotpaymentNRPSamt");
            Label lbtotreturRPSamt = (Label)e.Row.FindControl("lbtotreturRPSamt");
            Label lbtotreturNRPSamt = (Label)e.Row.FindControl("lbtotreturNRPSamt");
            lbtotSalesRPSAmt.Text = dtotSalesRPSAmt.ToString();
            lbtotSalesNRPSAmt.Text = dtotSalesNRPSAmt.ToString();
            lbtotpaymentRPSamt.Text = dtotpaymentRPSamt.ToString();
            lbtotpaymentNRPSamt.Text = dtotpaymentNRPSamt.ToString();
            lbtotreturRPSamt.Text = dtotreturRPSamt.ToString();
            lbtotreturNRPSamt.Text = dtotreturNRPSamt.ToString();
        }
    }
}

    [DataContract]
public class tsaleslocation_tran
{
    public string _salesman_cd;
    public string _dt;
    public double _latitude;
    public double _longitude;
    public string _trx_typ;
    public string _trandescription;
    public string _maptitle;
    public tsaleslocation_tran(string ssalesman_cd, string dtdt, double slatitude, double slongitude, string strx_typ, string strandescription, string smaptitle)
    {
        salesman_cd = ssalesman_cd;
        dt = dtdt;
        lat = slatitude;
        lng = slongitude;
        trx_typ = strx_typ;
        trandescription = strandescription;
        maptitle = smaptitle;
    }
    
    [DataMember]
    public string salesman_cd { set { _salesman_cd = value; } get { return (_salesman_cd); } }

    [DataMember]
    public string dt { set { _dt = value; } get { return (_dt); } }
    [DataMember]
    public double lat { get { return (_latitude); } set { _latitude = value; } }
    [DataMember]
    public double lng { get { return (_longitude); } set { _longitude = value; } }
    [DataMember]
    public string trx_typ { get { return (_trx_typ); } set { _trx_typ = value; } }
    [DataMember]
    public string trandescription { get { return (_trandescription); } set { _trandescription = value; } }
    [DataMember]
    public string maptitle { get { return (_maptitle); } set { _maptitle = value; } }
}


   

