using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Adminbranch_fm_channeldeniedbo : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingFieldValueToCombo(ref cbchannel, "otlcd", false);
            cbchannel_SelectedIndexChanged(sender, e);
        }
    }

    void vBindingGrid()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@qry_cd", "channeldenied"));
        bll.vBindingGridToSp(ref grd, "sp_tmap_query_get", arr);
    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@qry_cd", "channeldenied"));
        arr.Add(new cArrayList("@qry_data", cbchannel.SelectedValue.ToString()));
        bll.vInsertMapQuery(arr);
        vBindingGrid();
    }
    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbchannelcode = (Label)grd.Rows[e.RowIndex].FindControl("lbchannelcode");
        TextBox txreason = (TextBox)grd.Rows[e.RowIndex].FindControl("txreason");
        if (txreason.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Enter reason for release channel!','Pls enter reason','warning');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@qry_cd", "channeldenied"));
        arr.Add(new cArrayList("@qry_data", lbchannelcode.Text));
        bll.vDeleteMapQuery(arr);
        arr.Clear();
        arr.Add(new cArrayList("@audit_object", "tmap_query:Remove Trxn Denied from BO"));
        arr.Add(new cArrayList("@audit_typ", "D"));
        arr.Add(new cArrayList("@reasn", txreason.Text));
        arr.Add(new cArrayList("@executedby", Request.Cookies["usr_id"].Value.ToString()));
        bll.vInsertAuditTrail(arr);
        vBindingGrid();
    }
    protected void cbchannel_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@qr_cd", "channeldenied"));
        vBindingGrid();
    }
}