using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lookup_emp : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingComboToSp(ref cbsp, "sp_tmst_salespoint_getall", "salespointcd", "salespoint_nm");
        }
    }
   
    protected void btsearch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (cbsp.SelectedValue.ToString()!="-1")
        { arr.Add(new cArrayList("@salespointcd", cbsp.SelectedValue.ToString())); }

        arr.Add(new cArrayList("@emp_nm", txsearch.Text));
        bll.vBindingGridToSp(ref grd, "sp_tmst_employee_search", arr);
    }
}