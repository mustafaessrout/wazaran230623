using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_cashoutlist : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@cashout_sta_id", "N"));
            bll.vBindingGridToSp(ref grd, "sp_tcashregout_get", arr);
        }
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        HiddenField hdcashout = (HiddenField)grd.Rows[e.NewSelectedIndex].FindControl("hdcashout");
        arr.Add(new cArrayList("@ids_cashout", hdcashout.Value.ToString()));
        bll.vBindingGridToSp(ref grddtl, "sp_tcashregout_dtl_get", arr);

    }
    protected void grd_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
       // List<cArrayList> arr = new List<cArrayList>();
        //HiddenField hdcashout = (HiddenField)grd.Rows[e.NewSelectedIndex].FindControl("hdcashout");
        grd.EditIndex = e.NewEditIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cashout_sta_id", "N"));
        bll.vBindingGridToSp(ref grd, "sp_tcashregout_get", arr);
        DropDownList cbstatus = (DropDownList)grd.Rows[e.NewEditIndex].FindControl("cbstatus");
        arr.Clear();
        arr.Add(new cArrayList("@hiddendata", 0));
        arr.Add(new cArrayList("@fld_nm", "cashout_sta_id"));
        bll.vBindingFieldValueToCombo(ref cbstatus, arr);
    }
}