using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class maps_fm_maps_view : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            //arr.Add(new cArrayList("@emp_cd", Request.Cookies["usr_id"].Value.ToString()));
            //bll.vBindingComboToSpHO(ref cbsp, "sp_tmst_salespoint_GPS_get", "salespointcd", "salespoint_desc", arr);
            //Response.Cookies["spgps"].Value = cbsp.SelectedValue.ToString();
            //bindinggrdgps();
        }
    }
    protected void cbsalespoint_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}