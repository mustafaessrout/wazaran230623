using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_lookupinv : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            bll.vBindingGridToSp(ref grd, "sp_tdosales_invoice_getbystatus");
        }
    }
    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DateTime dtcurrent = Convert.ToDateTime( bll.vLookUp("select dbo.fn_getsystemdate()"));
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hdinvdate = (HiddenField)e.Row.FindControl("hdinvdate");
            DateTime dt = Convert.ToDateTime(hdinvdate.Value);
            if (dtcurrent.Month != dt.Month)
            {
                e.Row.Enabled = false; e.Row.ToolTip = "Out of  period to be cancelled!";
            }
        }
    }
}