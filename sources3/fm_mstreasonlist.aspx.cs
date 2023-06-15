using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_mstreasonlist : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingFieldValueToCombo(ref cbreason, "reasn_typ");
            bll.vBindingGridToSp(ref grdreason, "sp_tmst_reason_get");
        }
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_mstreasonentry.aspx");
    }
    protected void grdreason_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        HiddenField hdtype = (HiddenField)grdreason.Rows[e.RowIndex].FindControl("hdreasontype");
        Label lbreasoncode = (Label)grdreason.Rows[e.RowIndex].FindControl("lbreasoncode");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@reasn_cd", lbreasoncode.Text));
        arr.Add(new cArrayList("@reasn_typ", hdtype.Value.ToString()));
        bll.vDelMstReason(arr);
        bll.vBindingGridToSp(ref grdreason, "sp_tmst_reason_get");
       
    }
    protected void grdreason_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdreason.PageIndex = e.NewPageIndex;
        bll.vBindingGridToSp(ref grdreason, "sp_tmst_reason_get");
    }
    protected void cbreason_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@reasn_typ", cbreason.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grdreason, "sp_tmst_reason_get",arr);
    }
}