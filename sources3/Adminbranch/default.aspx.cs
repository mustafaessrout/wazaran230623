using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_Default : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingComboToSp(ref cbrole, "sp_tmst_role_get", "role_cd", "role_name");
            bll.vBindingGridToSp(ref grd, "sp_tmst_menu_get");
            
        }
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@role_cd", cbrole.SelectedValue.ToString()));
        bll.vDelMenuRole(arr);
        foreach (GridViewRow row in grd.Rows)
        {
            Label lbmenucode = (Label)row.FindControl("lbmenucode");
            CheckBox chaccess = (CheckBox)row.FindControl("chaccess");

            if (chaccess.Checked)
            {
                arr.Clear();
                arr.Add(new cArrayList("@role_cd", cbrole.SelectedValue.ToString()));
                arr.Add(new cArrayList("@menu_cd", lbmenucode.Text));
                bll.vInsertMenuRole(arr);
            }
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Access has been saved','Successfully','success');", true);

    }
    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbmenucode = (Label)e.Row.FindControl("lbmenucode");
            CheckBox chk = (CheckBox)e.Row.FindControl("chaccess");
            if (bll.vLookUp("select count(1) from tmenu_role where role_cd='" + cbrole.SelectedValue.ToString() + "' and menu_cd='" + lbmenucode.Text + "'") == "1")
            {
                chk.Checked = true;
            }
            else { chk.Checked = false; }
        }
    }
    protected void cbrole_SelectedIndexChanged(object sender, EventArgs e)
    {
        bll.vBindingGridToSp(ref grd, "sp_tmst_menu_get");
    }
}