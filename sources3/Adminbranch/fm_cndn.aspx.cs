using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Adminbranch_fm_cndn : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@sta_id", "N"));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_tblarcn_getbystatus", arr);

        }
    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grd.EditIndex = e.NewEditIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@sta_id", "N"));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tblarcn_getbystatus", arr);
    }
    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        DropDownList cbapproval = (DropDownList)grd.Rows[e.RowIndex].FindControl("cbapproval");
        Label lbcndnrefno = (Label)grd.Rows[e.RowIndex].FindControl("lbcndnrefno");
        Label lbarcnno = (Label)grd.Rows[e.RowIndex].FindControl("lbarcnno");
        arr.Add(new cArrayList("@sta_id", cbapproval.SelectedValue.ToString()));
        arr.Add(new cArrayList("@arcn_no", lbarcnno.Text));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@cndnrefno", lbcndnrefno.Text));
        bll.vUpdatetblARCN(arr);
        grd.EditIndex = -1;
        arr.Clear();
        arr.Add(new cArrayList("@sta_id", "N"));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tblarcn_getbystatus", arr);
    }
}