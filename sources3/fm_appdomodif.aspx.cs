using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_appdomodif : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@do_sta_id", "W"));
            bll.vBindingGridToSp(ref grddo, "sp_tmst_do_get", arr);
        }
    }
    protected void grddo_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label lbdono = (Label) grddo.SelectedRow.FindControl("lbdono");
        Label lbpono = (Label)grddo.SelectedRow.FindControl("lbpono");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@do_no", lbdono.Text));
        bll.vBindingGridToSp(ref grddodtl, "sp_tdo_dtl_get", arr);

        arr.Clear();
        arr.Add(new cArrayList("@po_no", lbpono.Text));
        bll.vBindingGridToSp(ref grdpo, "sp_tpo_dtl_get", arr);
    }
    protected void grddo_RowEditing(object sender, GridViewEditEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        grddo.EditIndex = e.NewEditIndex;
        arr.Add(new cArrayList("@do_sta_id", "W"));
        bll.vBindingGridToSp(ref grddo, "sp_tmst_do_get", arr);
        DropDownList cbstatus = (DropDownList)grddo.Rows[e.NewEditIndex].FindControl("cbstatus");
        cbstatus.Items.Add("Approved");
        cbstatus.Items.Add("Rejected");
    }
    protected void grddo_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Label lbpono = (Label)grddo.Rows[e.RowIndex].FindControl("lbpono");
        Label lbdono = (Label)grddo.Rows[e.RowIndex].FindControl("lbdono");
        DropDownList cbstatus = (DropDownList)grddo.Rows[e.RowIndex].FindControl("cbstatus");
        string sWhsCode = bll.vLookUp("select whs_cd from tmst_do where do_no='" + lbdono.Text + "'");
        string sWhsType = bll.vLookUp("select whs_typ from tmst_warehouse where whs_cd='" + sWhsCode + "'");
        List<cArrayList> arr = new List<cArrayList>();
        if (sWhsType == "L")
        {
            arr.Add(new cArrayList("@do_sta_id", "D"));
        }
        else
        {
            arr.Add(new cArrayList("@do_sta_id", "L"));
        }
        arr.Add(new cArrayList("@do_no", lbdono.Text));
        bll.vUpdateMstDo2(arr); //Update status saja
        grddo.EditIndex = -1;
        arr.Clear();
        arr.Add(new cArrayList("@do_sta_id", "W"));
        bll.vBindingGridToSp(ref grddo, "sp_tmst_do_get", arr);
        grddodtl.DataSource = null;
        grddodtl.DataBind();
        grdpo.DataSource = null;
        grdpo.DataBind();
    }
    protected void grddo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grddo.EditIndex = -1;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@do_sta_id", "W"));
        bll.vBindingGridToSp(ref grddo, "sp_tmst_do_get", arr);
    }
}