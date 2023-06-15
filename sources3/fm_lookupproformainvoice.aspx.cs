using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_lookupproformainvoice : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@inv_no", txsearchinv.Text));
            bll.vBindingGridToSp(ref grdinv, "sp_lookup_preinvoice", arr);
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tablePageCopy", "tablePageCopy();", true);
    }

    protected void btsearch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@inv_no", txsearchinv.Text));
        bll.vBindingGridToSp(ref grdinv, "sp_lookup_preinvoice", arr);
    }

    protected void grdinv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdinv.PageIndex = e.NewPageIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@word", txsearchinv.Text));
        bll.vBindingGridToSp(ref grdinv, "sp_lookup_preinvoice", arr);
    }
}