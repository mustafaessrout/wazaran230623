using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_faq : System.Web.UI.Page
{
    cbll bll = new cbll();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CBTYPE_SelectedIndexChanged(sender,e);
        }
    }
    protected void CBTYPE_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@type", CBTYPE.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grdfaq, "sp_faq_list", arr);
    }
}