using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_userprofilelist : System.Web.UI.Page
{

    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingGridToSp(ref grd, "sp_tuser_profile_get");
        }
    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
}