using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_ActiveDriver : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cbDriver_SelectedIndexChanged(sender, e);
        }
    }

    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd.PageIndex = e.NewPageIndex;
        List<cArrayList> arr = new List<cArrayList>();

        if (cbDriver.SelectedValue == "ActiveDriver")
        {
            arr.Add(new cArrayList("@salespointcd", HttpContext.Current.Request.Cookies["sp"].Value));
            arr.Add(new cArrayList("@emp_cd", null));
            bll.vBindingGridToSp(ref grd, "sp_tmst_activedriver_getByemp", arr);
        }
        else
        {
            arr.Add(new cArrayList("@salespointcd", HttpContext.Current.Request.Cookies["sp"].Value));
            arr.Add(new cArrayList("@emp_cd", null));
            bll.vBindingGridToSp(ref grd, "sp_tmst_activedriver_getAllDriver", arr);
        }
    }

    protected void cbDriver_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (cbDriver.SelectedValue == "ActiveDriver")
        {
            arr.Add(new cArrayList("@salespointcd", HttpContext.Current.Request.Cookies["sp"].Value));
            arr.Add(new cArrayList("@emp_cd", null));
            bll.vBindingGridToSp(ref grd, "sp_tmst_activedriver_getByemp", arr);
        }
        else
        {
            arr.Add(new cArrayList("@salespointcd", HttpContext.Current.Request.Cookies["sp"].Value));
            arr.Add(new cArrayList("@emp_cd", null));
            bll.vBindingGridToSp(ref grd, "sp_tmst_activedriver_getAllDriver", arr);
        }
    }
}