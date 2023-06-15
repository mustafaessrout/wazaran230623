using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_appcreditnote : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindinggrdapp(); 
        }
    }
    protected void grdapp_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        grdapp.EditIndex = -1;
        bindinggrdapp(); 
    }
    private void bindinggrdapp()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@sta_id", "N"));
        bll.vBindingGridToSp(ref grdapp, "sp_tblARCN_getbystatus", arr);

    }
    protected void grdapp_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdapp.EditIndex = e.NewEditIndex;
        bindinggrdapp();
        DropDownList cbapp = (DropDownList)grdapp.Rows[e.NewEditIndex].FindControl("cbapp");
        bll.vBindingFieldValueToCombo(ref cbapp, "sta_id", false);
    }
    protected void grdapp_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        TextBox txtcndnrefno = (TextBox)grdapp.Rows[e.RowIndex].FindControl("txtcndnrefno");
        Label lbarcn_no = (Label)grdapp.Rows[e.RowIndex].FindControl("lbarcn_no");
        DropDownList cbapp = (DropDownList)grdapp.Rows[e.RowIndex].FindControl("cbapp");
        if (txtcndnrefno.Text=="")
        {
            ScriptManager.RegisterClientScriptBlock(base.Page, this.GetType(), "FooterRequired", "alert('Notification : CN/DN Ref No. must be fill');", true);
            grdapp.EditIndex = -1;
            bindinggrdapp();
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@arcn_no", lbarcn_no.Text));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@sta_id", cbapp.SelectedValue.ToString()));
        arr.Add(new cArrayList("@cndnrefno", txtcndnrefno.Text));
        bll.vUpdatetblARCN(arr);
        arr.Clear();
        grdapp.EditIndex = -1;
        bindinggrdapp();
    }
    protected void grdapp_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        
    }
    protected void grdapp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdapp.PageIndex = e.NewPageIndex;
        bindinggrdapp();
    }
    protected void grdapp_PreRender(object sender, EventArgs e)
    {
       
    }
} 