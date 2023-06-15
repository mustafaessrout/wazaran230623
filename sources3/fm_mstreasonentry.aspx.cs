using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_mstreasonentry : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingFieldValueToCombo(ref cbreasontyp, "reasn_typ");
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@reasn_cd", txreasoncode.Text));
        arr.Add(new cArrayList("@reasn_nm", txreasonname.Text));
        arr.Add(new cArrayList("@reasn_arabic", txreasonarabic.Text));
        arr.Add(new cArrayList("@reasn_typ", cbreasontyp.SelectedValue.ToString()));
        bll.vInsertMstReason(arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Data Deleted successfully','','success');", true);
        Response.Redirect("fm_mstreasonlist.aspx");
    }
}