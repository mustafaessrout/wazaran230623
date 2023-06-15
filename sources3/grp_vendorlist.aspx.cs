using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class grp_vendorlist : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingGridToSp(ref grd, "sp_tmst_group_vendor_get");
            
        }
    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        Response.Redirect("grp_vendor_entry.aspx");
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        //Response.Redirect("fm_report.aspx?src=GRPVEN");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=GRPVEN');", true);            
    }
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd.PageIndex = e.NewPageIndex;
        bll.vBindingGridToSp(ref grd, "sp_tmst_group_vendor_get");
    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grd.EditIndex = e.NewEditIndex;
        bll.vBindingGridToSp(ref grd, "sp_tmst_group_vendor_get");
    }
    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
             List<cArrayList> arr = new List<cArrayList>();
             HiddenField hdgrp_cd = (HiddenField)grd.Rows[e.RowIndex].FindControl("hdgrp_cd");
             arr.Add(new cArrayList("@grp_cd", hdgrp_cd.Value.ToString()));
             bll.vDeletevendorgrp(arr);

             bll.vBindingGridToSp(ref grd, "sp_tmst_group_vendor_get");
        
    }
    protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grd.EditIndex = -1;
        bll.vBindingGridToSp(ref grd, "sp_tmst_group_vendor_get");
        
    }
    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        TextBox txgrp_nm = (TextBox)grd.Rows[e.RowIndex].FindControl("txgrp_nm");
        if (txgrp_nm.Text != "")
        {
            List<cArrayList> arr = new List<cArrayList>();
            HiddenField hdgrp_cd = (HiddenField)grd.Rows[e.RowIndex].FindControl("hdgrp_cd");
            arr.Add(new cArrayList("@grp_cd", hdgrp_cd.Value.ToString()));
            arr.Add(new cArrayList("@grp_nm", txgrp_nm.Text));
            bll.vUpdatevendorgrp(arr);
            grd.EditIndex = -1;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('The record has been updated','Sucessfully...','success');", true);//by Othman 30-8-15        
            bll.vBindingGridToSp(ref grd, "sp_tmst_group_vendor_get");

        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please insert the Group vendor name...','','error');", true);//by Othman 30-8-15
        }
    }
}