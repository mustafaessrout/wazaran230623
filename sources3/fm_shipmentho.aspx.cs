using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_shipmentho : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingFieldValueToCombo(ref cbpostatus, "po_sta_id");
            bll.vBindingGridToSp(ref grdpo, "sp_tmst_poho_get");
        }
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {

    }
    protected void grdpo_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
       // grdpo.Rows[e.NewSelectedIndex].BackColor = System.Drawing.Color.AliceBlue;
        Label lbpono = (Label)grdpo.Rows[e.NewSelectedIndex].FindControl("lbpono");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@po_no", lbpono.Text));
        bll.vBindingGridToSp(ref grddtl, "sp_tpoho_dtl_get", arr);
        bll.vBindingGridToSp(ref grdshipment, "sp_tshipmentho_schedule_get", arr);
    }
    protected void grddtl_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Label lbpono = (Label)grdpo.SelectedRow.FindControl("lbpono");
        grddtl.EditIndex = e.NewEditIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@po_no", lbpono.Text));
        bll.vBindingGridToSp(ref grddtl, "sp_tpoho_dtl_get", arr);
        DropDownList cbdest = (DropDownList)grddtl.Rows[e.NewEditIndex].FindControl("cbdestination");
        TextBox dtshipment = (TextBox)grddtl.Rows[e.NewEditIndex].FindControl("dtshipment");
        bll.vBindingComboToSp(ref cbdest, "sp_tmst_shipment_destination_get","dest_cd","dest_nm");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dd", "$(function () {$(" + dtshipment.ClientID + ").datepicker({ dateFormat: 'mm/dd/yy'}).val();});", true);
    }
    protected void grddtl_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grddtl.EditIndex = -1;
        Label lbpono = (Label)grdpo.SelectedRow.FindControl("lbpono");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@po_no", lbpono.Text));
        bll.vBindingGridToSp(ref grddtl, "sp_tpoho_dtl_get", arr);
    }
    protected void grddtl_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Label lbpono = (Label)grdpo.SelectedRow.FindControl("lbpono");
        Label lbitemcode = (Label)grddtl.Rows[e.RowIndex].FindControl("lbitemcode");
        TextBox dtshipment = (TextBox)grddtl.Rows[e.RowIndex].FindControl("dtshipment");
        TextBox txqty = (TextBox)grddtl.Rows[e.RowIndex].FindControl("txqty");
        TextBox txcontainer = (TextBox)grddtl.Rows[e.RowIndex].FindControl("txcontainer");
        DropDownList cbdestination = (DropDownList)grddtl.Rows[e.RowIndex].FindControl("cbdestination");

        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@po_no", lbpono.Text));
        arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
        arr.Add(new cArrayList("@shipment_dt", dtshipment.Text));
        arr.Add(new cArrayList("@qty", txqty.Text));
        arr.Add(new cArrayList("@container_cd", txcontainer.Text));
        arr.Add(new cArrayList("@dest_cd", cbdestination.SelectedValue.ToString()));
        bll.vInsertShipmentHOSchedule(arr); arr.Clear();
        //Refresh Detail
        grddtl.EditIndex = -1;
        arr.Add(new cArrayList("@po_no", lbpono.Text));
        bll.vBindingGridToSp(ref grddtl, "sp_tpoho_dtl_get", arr);
        bll.vBindingGridToSp(ref grdshipment, "sp_tshipmentho_schedule_get", arr);
    }
}