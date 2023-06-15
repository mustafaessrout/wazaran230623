using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_cashregoutall : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingGridToSp(ref grd, "sp_tcashregister_getbycashstaid");
        }
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        foreach (GridViewRow row in grd.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                HiddenField lbcashid = (HiddenField)row.FindControl("lbcashid");
                arr.Clear();
                arr.Add(new cArrayList("@cash_id", lbcashid.Value.ToString()));
                bll.vUpdateCashRegisterByCashStaID(arr);
            }
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "clo", "window.close();", true);
    }
}