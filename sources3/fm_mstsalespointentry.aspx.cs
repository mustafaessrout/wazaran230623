using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_mstsalespointentry : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@level_no", 2));
            bll.vBindingComboToSp(ref cbarea, "sp_tmst_location_get", "loc_cd", "loc_nm", arr);
            bll.vBindingFieldValueToCombo(ref cbsptype, "salespoint_typ");
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
    }
}