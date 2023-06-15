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
using System.Data;

public partial class fm_gps_salesman : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@emp_cd", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingComboToSpHO(ref cbsp, "sp_tmst_salespoint_GPS_get", "salespointcd", "salespoint_desc",arr);
            cbsp_SelectedIndexChanged(sender, e);
            cbsalesman_SelectedIndexChanged(sender, e);
            Response.Cookies["spgps"].Value = cbsp.SelectedValue.ToString();

            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[2] { new DataColumn("Cust_cd"), new DataColumn("Cust_nm") });
            ViewState["Customers"] = dt;
            this.BindGrid();
            
        }
    }
    protected void BindGrid()
    {
        GridView1.DataSource = (DataTable)ViewState["Customers"];
        GridView1.DataBind();
    }
    protected void clearbinding()
    {
        //clear GridView1
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[2] { new DataColumn("Cust_cd"), new DataColumn("Cust_nm") });
        ViewState["Customers"] = dt;
        this.BindGrid();
    }
    protected void Insert(object sender, EventArgs e)
{
    DataTable dt = (DataTable)ViewState["Customers"];
    bool ifExist = false;
    foreach (DataRow dr in dt.Rows)
    {
        if (dr["Cust_cd"].ToString() == hdcust.Value)
        {
            ifExist = true;
        }
    }
    if (!ifExist)
    {
        dt.Rows.Add(hdcust.Value, txcustomer.Text);
        ViewState["Customers"] = dt;
        this.BindGrid();
    }
    
}

    [System.Web.Services.WebMethod]
    public static List<object> GetData()
    {
        HttpCookie cokusr_id, cokspgps, coksalesman_cd, coksalesman_sp, cokcust_cd, cokscreeningqty;
        cokusr_id = HttpContext.Current.Request.Cookies["usr_id"];
        cokspgps = HttpContext.Current.Request.Cookies["spgps"];
        coksalesman_cd = HttpContext.Current.Request.Cookies["coksalesman_cd"];
        coksalesman_sp = HttpContext.Current.Request.Cookies["coksalesman_sp"];
        cokcust_cd = HttpContext.Current.Request.Cookies["cokcust_cd"];
        cokscreeningqty = HttpContext.Current.Request.Cookies["cokscreeningqty"];
        if (cokcust_cd.Value == null || cokcust_cd.Value == "") { cokcust_cd.Value = "0"; }

        string query = "SELECT * ,(select top 1 distanceinmeter from [dbo].[GetNearbyLocations3] ("+cokscreeningqty.Value+",latitude,longitude,salesman_cd) where cust_cd<>DATA.cust_cd order by distanceinmeter)nearloc,'' office FROM ( "
+ "select cust_cd,cust_nm,otlcd,cusgrcd,payment_term,latitude,longitude,cuscate_cd,credit_limit,salesman_cd"
+ ",round(dbo.fn_CalcDistanceKM((select latitude from  ttab_control where salespointcd=(select top 1 salespointcd from ttablet_profile  where salesman_cd=c.salesman_cd))"
+ ",c.latitude,(select longitude from  ttab_control where salespointcd=(select top 1 salespointcd from ttablet_profile  where salesman_cd=c.salesman_cd)),c.longitude),2) distanceKM"
+ " from tmst_customer c  where cust_sta_id='A' AND LATITUDE<>'0' and case when '" + cokcust_cd.Value + "' ='0' OR cust_cd in ("+cokcust_cd.Value+") then 1 else 0 end=1 AND SALESMAN_CD='" + coksalesman_cd.Value.ToString() + "' and salespointcd='" + coksalesman_sp.Value.ToString() + "' )DATA"
+" UNION ALL select '0' cust_cd,(select salespoint_nm from tmst_salespoint where salespointcd='"+coksalesman_sp.Value.ToString()+"') cust_nm,'' otlcd,'' cusgrcd ,0 payment_term,latitude,longitude,'' cuscate_cd,0 credit_limit,'0' salesman_cd,0 distanceKm,0 nearloc,'O' office from ttab_control C where salespointcd='"+coksalesman_sp.Value.ToString()+"'";
        List<object> lists = new List<object>();
        string conString = System.Configuration.ConfigurationManager.ConnectionStrings["connstrho"].ConnectionString;
        SqlCommand cmd = new SqlCommand(query);
        //select cust_cd,cust_nm,otlcd,cusgrcd,payment_term,latitude,longitude,cuscate_cd,credit_limit from tmst_customer where cust_sta_id='A' AND LATITUDE<>'0' AND SALESMAN_CD='" + coksalesman_cd.Value.ToString() + "'");
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
                        cust_cd = sdr["cust_cd"],
                        cust_nm = sdr["cust_nm"].ToString(),
                        otlcd = sdr["otlcd"],
                        cusgrcd = sdr["cusgrcd"],
                        payment_term = sdr["payment_term"],
                        latitude = sdr["latitude"],
                        longitude = sdr["longitude"],
                        cuscate_cd = sdr["cuscate_cd"],
                        credit_limit = sdr["credit_limit"],
                        distanceKM=sdr["distanceKM"],
                        nearloc=sdr["nearloc"],
                        office=sdr["office"]
                    });
                }
            }
            cmd.Connection.Close();
        }
        
        return lists;
    }


    protected void btview_Click(object sender, EventArgs e)
    {
        Response.Cookies["cokscreeningqty"].Value = ddscreening.SelectedValue.ToString();
        //filter data with customer 
        string scust_cd = "";
        foreach (GridViewRow row in GridView1.Rows)
        {
            Label lbcust_cd = (Label)row.FindControl("lbcust_cd");
            scust_cd = scust_cd+","+lbcust_cd.Text ;

        }
        //scust_cd = scust_cd.Substring(0, scust_cd.Length - 1); //romove last caracter
        if (scust_cd.Length>0 )
        { 
        scust_cd = scust_cd.Substring(1, scust_cd.Length-1); //romove last caracter  
        }
        Response.Cookies["cokcust_cd"].Value = scust_cd;
        //------------
        
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MarkerAll();", true);

        
    }


    protected void cbsp_SelectedIndexChanged(object sender, EventArgs e)
    {
        clearbinding();
        Response.Cookies["spgps"].Value = cbsp.SelectedValue.ToString();
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", cbsp.SelectedValue.ToString()));
        arr.Add(new cArrayList("@emp_cd", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingComboToSpHO(ref cbsalesman, "sp_tmst_employee_getbyprofile_accessgps", "salesman_cd", "emp_desc", arr);
    }
    protected void cbsalesman_SelectedIndexChanged(object sender, EventArgs e)
    {
        clearbinding();
        Response.Cookies["coksalesman_cd"].Value = cbsalesman.SelectedValue.ToString();
        string ssalesman_sp = bll.vLookUpHO("select salespointcd from ttablet_profile where SALESMAN_CD='" + cbsalesman.SelectedValue.ToString() + "'");
        Response.Cookies["coksalesman_sp"].Value = ssalesman_sp.ToString();
       
        txcustomer_AutoCompleteExtender.ContextKey = cbsalesman.SelectedValue.ToString();
        

        
    }
    
   
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        System.Data.SqlClient.SqlDataReader rs = null;
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["coksalesman_sp"];
        List<string> lCust = new List<string>();
        string sCust = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        arr.Add(new cArrayList("@salesman_cd", contextKey));
        arr.Add(new cArrayList("@cust_cd", prefixText)); 
        bll.vgps_salesman_customer_search(arr, ref rs);
        while (rs.Read())
        {
            sCust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + " " + rs["cust_nm"].ToString(), rs["cust_cd"].ToString());
            lCust.Add(sCust);
        }
        rs.Close();
        return (lCust.ToArray());
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        Insert(sender, e);
        txcustomer.Text = null; 
        //DataTable dt = new DataTable();
        //dt.Columns.Add("Cust_cd", typeof(string));
        //dt.Columns.Add("Cust_nm", typeof(string));
        //dt.Rows.Add(hdcust.Value, txcustomer.Text);
        //GridView1.DataSource = dt;
        //GridView1.DataBind();

        ////Boolean to check if he has row has been
        //bool Found = false;
        //if (GridView1.Rows.Count > 0)
        //{
        //    //Check if the product Id exists with the same Price
        //       foreach (DataGridViewRow  row in GridView1.Rows)
        //       {
        //           if (Convert.ToString(row.Cells[0].Value) == textBox_ProductId.Text && Convert.ToString(row.Cells[1].Value) == textBox_Price.Text)
        //           {
        //               //Update the Quantity of the found row
        //               row.Cells[2].Value = Convert.ToString(1 + Convert.ToInt16(row.Cells[2].Value));
        //               Found = true;
        //           }
 
        //       }
        //       if (!Found)
        //       {
        //           //Add the row to grid view
        //           GridView1.Rows.Add(textBox_ProductId.Text, textBox_Price.Text, 1);
        //       }
 
        //   }
        //   else
        //   {
        //       //Add the row to grid view for the first time
        //       GridView1.Rows.Add(textBox_ProductId.Text, textBox_Price.Text, 1);
        //   }
        //}
        
    }
}

    
   

