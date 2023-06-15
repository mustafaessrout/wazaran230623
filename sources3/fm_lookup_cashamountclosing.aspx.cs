﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_lookup_cashamountclosing : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@chclosingno", txsearch.Text));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tcashregister_closing_get", arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix()", true);
    }

    protected void grd_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label lbchclosingno = (Label)grd.SelectedRow.FindControl("lbchclosingno");

        Session["loochclosingno"] = lbchclosingno.Text;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "updpnl", "closewin()", true);
    }
}