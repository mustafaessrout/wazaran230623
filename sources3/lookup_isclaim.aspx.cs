using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lookup_isclaim : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vDelWrkIsClaim(arr);
            arr.Clear();
            arr.Add(new cArrayList("@disc_sta_id", "A"));
            bll.vBindingComboToSp(ref cbproposal, "sp_tmst_discount_get", "proposal_no", "proposal_no", arr);
        }
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@proposal_no", cbproposal.SelectedValue.ToString()));
        arr.Add(new cArrayList("@remark", txremark.Text));
        bll.vInsertWrkIsClaim(arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "clo", "window.close();", true);
    }
}