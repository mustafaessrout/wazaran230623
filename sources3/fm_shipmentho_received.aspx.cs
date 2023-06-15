using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_shipmentho_received : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingGridToSp(ref grdpo, "sp_tmst_poho_get");
        }
    }
    protected void grdpo_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lbpono = (Label)grdpo.Rows[e.NewSelectedIndex].FindControl("lbpono");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@po_no", lbpono.Text));
        bll.vBindingGridToSp(ref grdshipment, "sp_tshipmentho_schedule_get", arr);
        
    }
    protected void grdshipment_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }
    protected void grdshipment_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdshipment.EditIndex = e.NewEditIndex;
        Label lbpono = (Label)grdpo.Rows[e.NewEditIndex].FindControl("lbpono");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@po_no", lbpono.Text));
    }
}