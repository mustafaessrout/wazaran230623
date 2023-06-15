using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lookupcndn : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            bll.vBindingFieldValueToComboWithChoosen(ref cbstatus, "cndn_sta_id");
        }
   
    }

    protected void cbstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cndnadj_sta_id", cbstatus.SelectedValue));
        bll.vBindingGridToSp(ref grd, "sp_tacc_cndn_getbystatus", arr);
    }
}