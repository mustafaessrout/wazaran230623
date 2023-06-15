using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_paycleareanceho : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@dep_sta_id", "N"));
            bll.vBindingGridToSp(ref grd, "sp_tbank_deposit_get", arr);
        }
    }
    protected void btrefresh_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@dep_sta_id", "N"));
        bll.vBindingGridToSp(ref grd, "sp_tbank_deposit_get", arr);
    }
}