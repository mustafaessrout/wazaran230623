using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lookcustomer : System.Web.UI.Page
{
    cbll bll = new cbll();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbsalespoint.Text = bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            cbotl.Items.Insert(0, "-- All Outlet --");
            cbgroup.Items.Insert(0, "-- All Group --");
            cbgroup_SelectedIndexChanged(sender, e);
        }
    }

    protected void cbgroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (cbgroup.SelectedIndex == 0)
        {
            arr.Add(new cArrayList("@group", null));
        }
        else
        {
            arr.Add(new cArrayList("@group", (cbgroup.SelectedIndex - 1).ToString()));
        }
        if (cbotl.SelectedIndex == 0)
        {
            arr.Add(new cArrayList("@outlet", null));
        }
        else
        {
            arr.Add(new cArrayList("@outlet", (cbotl.SelectedIndex - 1).ToString()));
        }
        arr.Add(new cArrayList("@cust", txsearchcust.Text));
        bll.vBindingGridToSp(ref grd, "sp_lookup_customer1", arr);
    }

    protected void grdprop_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd.PageIndex = e.NewPageIndex;
        List<cArrayList> arr = new List<cArrayList>();
        if (cbgroup.SelectedIndex == 0)
        {
            arr.Add(new cArrayList("@group", null));
        }
        else
        {
            arr.Add(new cArrayList("@group", (cbgroup.SelectedIndex - 1).ToString()));
        }
        if (cbotl.SelectedIndex == 0)
        {
            arr.Add(new cArrayList("@outlet", null));
        }
        else
        {
            arr.Add(new cArrayList("@outlet", (cbotl.SelectedIndex - 1).ToString()));
        }
        arr.Add(new cArrayList("@cust", txsearchcust.Text));
        bll.vBindingGridToSp(ref grd, "sp_lookup_customer1", arr);
    }

    protected void btsearch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (cbgroup.SelectedIndex == 0)
        {
            arr.Add(new cArrayList("@group", null));
        }
        else
        {
            arr.Add(new cArrayList("@group", (cbgroup.SelectedIndex - 1).ToString()));
        }
        if (cbotl.SelectedIndex == 0)
        {
            arr.Add(new cArrayList("@outlet", null));
        }
        else
        {
            arr.Add(new cArrayList("@outlet", (cbotl.SelectedIndex - 1).ToString()));
        }
        arr.Add(new cArrayList("@cust", txsearchcust.Text));
        bll.vBindingGridToSp(ref grd, "sp_lookup_customer1", arr);
    }


}