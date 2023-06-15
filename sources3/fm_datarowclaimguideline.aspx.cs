using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_datarowclaimguideline : System.Web.UI.Page
{

    cbll bll = new cbll();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cbDocument_SelectedIndexChanged(sender, e);
        }
    }
    protected void cbDocument_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Clear();
        arr.Add(new cArrayList("@doc", cbDocument.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grddocument, "sp_tclaim_guideline_get", arr);
    }

}