using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class master_fm_updateCustomer : System.Web.UI.Page
{
    cbll bll = new cbll();
    Utitlity ut = new Utitlity();
    cdal cdl = new cdal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespointSN_get", "salespointcd", "salespoint_desc");
        }
    }

    protected void cbsalespoint_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespoint", cbsalespoint.SelectedValue));
        bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespointSN_get", "salespointcd", "salespoint_desc", arr);
    }

    protected void cboSalesman_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}