using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lookupreceiptgr : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sGR = Request.QueryString["gr"];
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@receipt_no", sGR));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
            bll.vBindingGridToSp(ref grd, "sp_tgoodreceipt_dtl_get", arr);
        }
    }

    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        foreach (GridViewRow row in grd.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                if ((row.RowIndex % 2) == 0)
                {
                    row.BackColor = System.Drawing.Color.White;
                }
                else { row.BackColor = System.Drawing.Color.LightGray; }
            }
        }
    }
}