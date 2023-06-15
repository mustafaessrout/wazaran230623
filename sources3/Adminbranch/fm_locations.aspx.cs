using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_locations : System.Web.UI.Page
{

    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        txlocno.Text = "NEW";
        //if (!IsPostBack)
        //{
        //    //vBindingGrid();

        //}
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        string sSoNo = "";
        if (txloc_nm.Text == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Lcation name can not empty','Check Location','warning');", true);
            return;
        }

        try
        {
            btsave.Visible = false;
            btsave.Enabled = false;
            List<cArrayList> arr = new List<cArrayList>();
            if (rbloc.SelectedValue.ToString() == "dist")
            {
                arr.Add(new cArrayList("@loc_cd_parent", cbcity.SelectedValue.ToString()));
                arr.Add(new cArrayList("@loc_typ", "DIS"));
                arr.Add(new cArrayList("@level_no", "3"));
            }
            else
            {
                arr.Add(new cArrayList("@loc_cd_parent", null));
                arr.Add(new cArrayList("@loc_typ", "CIT"));
                arr.Add(new cArrayList("@level_no", "2"));
            }
            arr.Add(new cArrayList("@loc_nm", txloc_nm.Text));
            bll.vInsertMstLocation(arr, ref sSoNo);
            txlocno.Text = sSoNo;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Location has been saved','Location no : " + sSoNo + "','success');", true);
            if (rbloc.SelectedValue.ToString() == "city")
            {
                arr.Clear();
                arr.Add(new cArrayList("@level_no", "2"));
                bll.vBindingGridToSp(ref grd, "sp_tmst_location_get", arr);
            }
            else if (rbloc.SelectedValue.ToString() == "dist")
            {
                arr.Clear();
                arr.Add(new cArrayList("@loc_cd_parent", cbcity.SelectedValue.ToString()));
                bll.vBindingGridToSp(ref grd, "sp_tmst_location_get", arr);
            }

        }
        catch (Exception ex)
        {
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : Save Take Order");
        }

    }
    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //Label lbcustcode = (Label)grd.Rows[e.RowIndex].FindControl("lbcustcode");
        //List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@cust_cd", lbcustcode.Text));
        //arr.Add(new cArrayList("@blocked_typ", "S"));
        //bll.vDelCustomerBlocked(arr);
        //vBindingGrid();
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
    }

    protected void rblock_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbloc.SelectedValue.ToString() == "city")
        {
            //txcity.Visible = true;

            txloc_nm.CssClass = "form-control";
            cbcity.CssClass = "form-control ro";
            //gridview update with cityes
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@level_no", "2"));
            bll.vBindingGridToSp(ref grd, "sp_tmst_location_get", arr);
        }
        else if (rbloc.SelectedValue.ToString() == "dist")
        {
            cbcity.CssClass = "form-control";
            txloc_nm.CssClass = "form-control";
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@level_no", "2"));
            bll.vBindingComboToSp(ref cbcity, "sp_tmst_location_get", "loc_cd", "loc_nm", arr);
            cbcity_SelectedIndexChanged(sender, e);

            //grdview update with districts
        }
    }
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd.PageIndex = e.NewPageIndex;
        List<cArrayList> arr = new List<cArrayList>();
        if (rbloc.SelectedValue.ToString() == "city")
        {
            arr.Add(new cArrayList("@level_no", "2"));
            bll.vBindingGridToSp(ref grd, "sp_tmst_location_get", arr);
        }
        else if (rbloc.SelectedValue.ToString() == "dist")
        {
            arr.Add(new cArrayList("@loc_cd_parent", cbcity.SelectedValue.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_tmst_location_get", arr);
        }
    }

    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Adminbranch/fm_locations.aspx");
    }
    protected void cbcity_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();

        arr.Add(new cArrayList("@loc_cd_parent", cbcity.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tmst_location_get", arr);
    }
}