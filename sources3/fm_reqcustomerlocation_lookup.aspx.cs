using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_reqcustomerlocation_lookup : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            string cust_cd = Request.QueryString["dc"];
            string salespointcd = Request.QueryString["sp"];

            lbcustomer.Text = bll.vLookUp("select cust_cd+' - '+cust_nm from tmst_customer where cust_cd='"+cust_cd+"' and salespointcd='"+salespointcd+"'");
            arr.Clear();
            arr.Add(new cArrayList("@cust_cd", cust_cd));
            arr.Add(new cArrayList("@salespointcd", salespointcd));
            bll.vBindingGridToSp(ref grdlocation, "sp_tmstcustomer_location_get", arr);

        }
    }
}