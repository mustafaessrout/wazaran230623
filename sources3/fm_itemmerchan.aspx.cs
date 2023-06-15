using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_itemmerchan : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingGridToSp(ref grd, "sp_titem_merchandiser_get");
        }
    }

    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbitemcode = (Label)grd.Rows[e.RowIndex].FindControl("lbitemcode");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
        bll.vDeleteItemMerchandiser(arr);
        bll.vBindingGridToSp(ref grd, "sp_titem_merchandiser_get");
    }

    protected void chkmerchan_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow item in grd.Rows)
        {
            // check row is datarow
            if (item.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk = (item.FindControl("chmerchan") as CheckBox);
                if (chk.Checked)
                {
                    Label lbitemcode = (Label)item.FindControl("lbitemcode");
                    List<cArrayList> arr = new List<cArrayList>();
                    arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
                    arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value));
                    arr.Add(new cArrayList("@created_dt", System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                    bll.vInsertItemMerchandiser(arr);
                  
                }
                else
                {
                    Label lbitemcode = (Label)item.FindControl("lbitemcode");
                    List<cArrayList> arr = new List<cArrayList>();
                    arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
                    bll.vDeleteItemMerchandiser(arr);
                   // ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Item for merchandiser has been deleted successfully','Item Merchandiser','success');", true);
                }
            }
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Item for merchandiser has been modified successfully','Item Merchandiser','success');", true);
    }
    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbitemcode = (Label)e.Row.FindControl("lbitemcode");
            CheckBox chmerchan = (CheckBox)e.Row.FindControl("chmerchan");
            int nCheck = Convert.ToInt16( bll.vLookUp("select count(1) from titem_merchandiser where item_cd='"+lbitemcode.Text+"'"));
            if (nCheck > 0)
            {
                chmerchan.Checked = true;
            }
            else { chmerchan.Checked = false; }
        }
    }

    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd.PageIndex = e.NewPageIndex;
        bll.vBindingGridToSp(ref grd, "sp_titem_merchandiser_get");
    }

    protected void btsearch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@item_cd", txitemcode.Text));
        bll.vBindingGridToSp(ref grd, "sp_titem_merchandiser_get", arr);
    }

    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=itemmerchan');", true);
    }
}
