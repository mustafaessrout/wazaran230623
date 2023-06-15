﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_outlettarget : System.Web.UI.Page
{
    cbll bll = new cbll();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@qry_cd", "SalesJob"));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingComboToSp(ref cbsalesmancd, "sp_tmst_employee_getbyqry", "emp_cd", "emp_nm", arr);
            arr.Clear();            
            bll.vBindingFieldValueToCombo(ref cbmonth, "mth");
            bll.vBindingFieldValueToCombo(ref cbyear, "yr");
            arr.Add(new cArrayList("@monthcd", cbmonth.SelectedValue.ToString()));
            arr.Add(new cArrayList("@yearcd", cbyear.SelectedValue.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_toutlettarget_get", arr);
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
    }
    
    
    protected void btadd_Click(object sender, EventArgs e)
    {
        txqtyPnl.CssClass = "";
        if (txqty.Text != "")
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@monthcd", cbmonth.SelectedValue.ToString()));
            arr.Add(new cArrayList("@yearcd", cbyear.SelectedValue.ToString()));
            arr.Add(new cArrayList("@qty_outlet", txqty.Text));                        
            arr.Add(new cArrayList("@salesman_cd", cbsalesmancd.SelectedValue.ToString()));            
            bll.vInsertOutletTarget(arr);
            arr.Clear();
            arr.Add(new cArrayList("@monthcd", cbmonth.SelectedValue.ToString()));
            arr.Add(new cArrayList("@yearcd", cbyear.SelectedValue.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_toutlettarget_get", arr);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Sucessfully...','The record has been inserted','success');", true);//by Othman 30-8-15        
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please insert ...','quantity and UOM ','warning');", true);//by Othman 30-8-15
            txqtyPnl.CssClass = "error";
        }

    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grd.EditIndex = e.NewEditIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@monthcd", cbmonth.SelectedValue.ToString()));
        arr.Add(new cArrayList("@yearcd", cbyear.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_toutlettarget_get", arr);
    }
    protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@monthcd", cbmonth.SelectedValue.ToString()));
        arr.Add(new cArrayList("@yearcd", cbyear.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_toutlettarget_get", arr);
        grd.EditIndex = -1;
    }

    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        var ss = false;
        TextBox txqty = (TextBox)grd.Rows[e.RowIndex].FindControl("txqty");
        if (txqty.Text != "")
        {
            List<cArrayList> arr = new List<cArrayList>();           
            Label lbmonthcd = (Label)grd.Rows[e.RowIndex].FindControl("lbmonthcd");
            Label lbyearcd = (Label)grd.Rows[e.RowIndex].FindControl("lbyearcd");
            HiddenField hdsalesman_nm = (HiddenField)grd.Rows[e.RowIndex].FindControl("hdsalesman_nm");
            arr.Add(new cArrayList("@monthcd", lbmonthcd.Text));
            arr.Add(new cArrayList("@yearcd", lbyearcd.Text));
            arr.Add(new cArrayList("@qty_outlet", txqty.Text));
            arr.Add(new cArrayList("@salesman_cd", hdsalesman_nm.Value.ToString()));
            bll.vUpdateOutletTarget(arr);
            grd.EditIndex = -1;
            arr.Clear();
            arr.Add(new cArrayList("@monthcd", lbmonthcd.Text));
            arr.Add(new cArrayList("@yearcd", lbyearcd.Text));
            bll.vBindingGridToSp(ref grd, "sp_toutlettarget_get", arr);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('The record has been inserted','Sucessfully...','success');", true);//by Othman 30-8-15        
        }
        else{
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please insert the quantity...','','error');", true);//by Othman 30-8-15
            

        }

    }

    protected void refreshgridview(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@monthcd", cbmonth.SelectedValue.ToString()));
        arr.Add(new cArrayList("@yearcd", cbyear.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_toutlettarget_get", arr);

    }
}