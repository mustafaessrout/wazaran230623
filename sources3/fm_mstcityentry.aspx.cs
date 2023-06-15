using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class fm_mstcityentry : System.Web.UI.Page
{
    cbll bll = new cbll();
    List<cArrayList> arr = new List<cArrayList>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingComboToSp(ref cbregion, "sp_tmst_region_get","region_cd","region_nm");
            if (Request.QueryString["city"] != null)
            {
                string sCity = Request.QueryString["city"].ToString();
                SqlDataReader rs = null;
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@city_cd", sCity));
                bll.vGetMstCity(ref rs, arr);
                while (rs.Read())
                {
                    txcitycode.Text = sCity;
                    txcityname.Text = rs["city_nm"].ToString();
                    txarabic.Text =  rs["city_arabic"].ToString();
                } rs.Close();
            }
        }
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@city_cd", txcitycode.Text));
        arr.Add(new cArrayList("@city_nm", txcityname.Text));
        arr.Add(new cArrayList("@city_arabic", txarabic.Text));
        arr.Add(new cArrayList("@region_cd", cbregion.SelectedValue.ToString()));
        bll.vinsertMstCity(arr);
        Response.Redirect("fm_mstcity.aspx");
    }
}