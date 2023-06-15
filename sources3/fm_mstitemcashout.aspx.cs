using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_mstitemcashout : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingFieldValueToCombo(ref cbitemtype, "cashout_typ");
            bll.vBindingFieldValueToCombo(ref cbroutine, "routine");
           // bll.vBindingGridToSp(ref grd, "sp_tmst_itemcashout_get");
            bll.vBindingFieldValueToCombo(ref cbinout, "inout");
            cbitemtype_SelectedIndexChanged(sender, e);
            cbroutine_SelectedIndexChanged(sender, e);
            cbinout_SelectedIndexChanged(sender, e);
        }
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        txitemcodePnl.CssClass = "";
        txitemnamePnl.CssClass = "";
        if (txitemcode.Text == "")
        {
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('New Item Cashout Code','can not empty','warning');", true);
            txitemcodePnl.CssClass = "error err-empty";
            return;
        }

        if (txitemname.Text=="")
        {
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "swal('New Item Cashout Name','can not empty','warning');", true);
            txitemnamePnl.CssClass = "error err-empty";
            return;
        }
        if (txitemname.Text != "" && txitemcode.Text != "")
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@itemco_cd", txitemcode.Text));
            arr.Add(new cArrayList("@itemco_nm", txitemname.Text));
            arr.Add(new cArrayList("@cashout_typ", cbitemtype.SelectedValue.ToString()));
            arr.Add(new cArrayList("@routine", cbroutine.SelectedValue.ToString()));
            arr.Add(new cArrayList("@inout", cbinout.SelectedValue.ToString()));
            bll.vInsertMstItemCashout(arr);
            arr.Clear();
            //arr.Add(new cArrayList("@cashout_typ", cbitemtype.SelectedValue.ToString()));
            //bll.vBindingGridToSp(ref grd, "sp_tmst_itemcashout_get", arr);
            BindingItemCashout();
            
           ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('The record have been sucessfuly added','','success');", true);
        }
        
    }

    void BindingItemCashout()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cashout_typ", cbitemtype.SelectedValue.ToString()));
        arr.Add(new cArrayList("@inout", cbinout.SelectedValue.ToString()));
        arr.Add(new cArrayList("@routine", cbroutine.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tmst_itemcashout_get", arr);
    }
    protected void cbitemtype_SelectedIndexChanged(object sender, EventArgs e)
    {
       // List<cArrayList> arr = new List<cArrayList>();
       // arr.Add(new cArrayList("@cashout_typ", cbitemtype.SelectedValue.ToString()));
       //// arr.Add(new cArrayList("@inout", cbinout.SelectedValue.ToString()));
       // bll.vBindingGridToSp(ref grd, "sp_tmst_itemcashout_get", arr);
        BindingItemCashout();
    }
    

    protected void cbinout_SelectedIndexChanged(object sender, EventArgs e)
    {
       // List<cArrayList> arr = new List<cArrayList>();
       // arr.Add(new cArrayList("@cashout_typ", cbitemtype.SelectedValue.ToString()));
       //// arr.Add(new cArrayList("@inout", cbinout.SelectedValue.ToString()));
       // bll.vBindingGridToSp(ref grd, "sp_tmst_itemcashout_get", arr);
        BindingItemCashout();
    }
    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbitemcode = (Label)grd.Rows[e.RowIndex].FindControl("lbitemcode");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@itemco_cd", lbitemcode.Text));
        bll.vDelMstItemCashout(arr);
        arr.Clear();        
        arr.Add(new cArrayList("@cashout_typ", cbitemtype.SelectedValue.ToString()));
       // arr.Add(new cArrayList("@inout", cbinout.SelectedValue.ToString()));       
        bll.vBindingGridToSp(ref grd, "sp_tmst_itemcashout_get", arr);
    }

    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=mschout');", true);
    }
    protected void cbroutine_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindingItemCashout();
    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Label lbroutine = (Label)grd.Rows[e.NewEditIndex].FindControl("lbroutine");
        Label lbinout = (Label)grd.Rows[e.NewEditIndex].FindControl("lbinout");
        grd.EditIndex = e.NewEditIndex;
        BindingItemCashout();
        DropDownList cbinout = (DropDownList)grd.Rows[e.NewEditIndex].FindControl("cbinout");
        DropDownList cbroutine = (DropDownList)grd.Rows[e.NewEditIndex].FindControl("cbroutine");
        bll.vBindingFieldValueToCombo(ref cbinout, "inout");
        bll.vBindingFieldValueToCombo(ref cbroutine, "routine");
        cbroutine.SelectedValue = lbroutine.Text;
        cbinout.SelectedValue = lbinout.Text;
        cbinout.Focus();
    }
    protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grd.EditIndex = -1;
        BindingItemCashout();
    }
    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        DropDownList cbinout = (DropDownList)grd.Rows[e.RowIndex].FindControl("cbinout");
        DropDownList cbroutine = (DropDownList)grd.Rows[e.RowIndex].FindControl("cbroutine");
        Label lbitemco = (Label)grd.Rows[e.RowIndex].FindControl("lbitemcode");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@itemco_cd", lbitemco.Text));
        arr.Add(new cArrayList("@inout", cbinout.SelectedValue.ToString()));
        arr.Add(new cArrayList("@routine", cbroutine.SelectedValue.ToString()));
        bll.vUpdateMstItemCashout(arr);
        grd.EditIndex = -1;
        BindingItemCashout();
    }
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd.PageIndex = e.NewPageIndex;
        BindingItemCashout();
    }
}