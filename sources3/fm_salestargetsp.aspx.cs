using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using AjaxControlToolkit;
public partial class fm_salestargetsp : System.Web.UI.Page
{
    cbll bll = new cbll();
    decimal totalQty = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
            lbstatus.Text = bll.vLookUp("select fld_desc from tfield_value where fld_nm='sta_id' and fld_valu='N'");
            
            bll.vBindingComboToSp(ref cbperiod, "sp_getperiodyear", "period", "ymtname");
            string waz_dt;
            waz_dt = Request.Cookies["waz_dt"].Value.ToString();
            DateTime dtwaz_dt = DateTime.ParseExact(waz_dt, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            waz_dt = dtwaz_dt.ToString("yyyyMM");
            cbperiod.SelectedValue = waz_dt;
            cbperiod_SelectedIndexChanged(sender, e);
            bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm");
            cbsalespoint.SelectedValue = Request.Cookies["sp"].Value.ToString();
            cbsalespoint.CssClass = "ro";
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@qry_cd", "SalesJob"));
            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue));
            bll.vBindingComboToSp(ref cbsalesmancd, "sp_tmst_employee_getbyqry","emp_cd","emp_desc", arr);
            //cbsalesmancd.Enabled = false;
            //cbsalesmancd.CssClass = "makeitreadonly";
            arr.Clear();
            arr.Add(new cArrayList("@level_no", 1));
            bll.vBindingComboToSp(ref cbprod1, "sp_tmst_product_get", "prod_cd", "prod_nm", arr);
            
            //bll.vBindingFieldValueToCombo(ref cbmonth, "mth");
            //bll.vBindingFieldValueToCombo(ref cbyear, "yr");
            bll.vBindingFieldValueToCombo(ref cbuom, "uom");
            cbprod1_SelectedIndexChanged(sender, e);
            cbprod1_SelectedIndexChanged(sender, e);
          //  Refreshgridview(this.grd, e);
            arr.Clear();
            arr.Add(new cArrayList("@target_no", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue));
            bll.vDelSalesTargetDtl(arr);
            bindinggrd();
        }
    }
    protected void cbprod1_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@prod_cd_parent", cbprod1.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbprod2, "sp_tmst_product_get", "prod_cd", "prod_nm", arr);
        cbprod2_SelectedIndexChanged(sender, e);
    }
    protected void cbprod2_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@prod_cd_parent", cbprod2.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbprod3, "sp_tmst_product_get", "prod_cd", "prod_nm", arr);
        //Target HO
        txtargetho.Text = bll.vLookUp("select dbo.fn_getsalestargetho('" + cbprod2.SelectedValue.ToString() + "','" + Request.Cookies["sp"].Value.ToString() +  "')");
    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        if (txqty.Text == "" || cbuom.SelectedItem.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Qty has not selected','Pls fill qty target','warning');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
            return;
        }
        string target_no;
        if (txtargetno.Text == "" || txtargetno.Text == "NEW") { target_no = Request.Cookies["usr_id"].Value.ToString(); } else { target_no = txtargetno.Text; }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@target_no", target_no));
        arr.Add(new cArrayList("@remark", txremark.Text));
        if (chlevel.Checked)
        {
            arr.Add(new cArrayList("@prod_cd", cbprod2.SelectedValue.ToString()));
            arr.Add(new cArrayList("@level_no", 2));
        }
        else { arr.Add(new cArrayList("@prod_cd", cbprod3.SelectedValue.ToString()));
        arr.Add(new cArrayList("@level_no", 3));
        }
        arr.Add(new cArrayList("@qty", txqty.Text));
        arr.Add(new cArrayList("@uom", cbuom.SelectedValue.ToString()));
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        bll.vInsertSalesTargetDtl(arr);
        bindinggrd();
        //arr.Clear();
        //arr.Add(new cArrayList("@target_no", target_no));
        //bll.vBindingGridToSp(ref grd, "sp_tsalestargetsp_dtl_get", arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
        
    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Sucessfully...','The record has been inserted','success');", true);//by Othman 30-8-15        
    //}
    //    else
    //    {
    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please insert ...','Quantity','error');", true);//by Othman 30-8-15
    //    }

    }
    
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //if (lbstatus.Text == "COMPLETE")
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction already complete can not be edit','error');", true);
        //    grd.EditIndex = -1;
        //    bindinggrd();
        //    return;
        //}
        
        Label lblUOM = (Label)grd.Rows[e.NewEditIndex].FindControl("lblUOM");
        grd.EditIndex = e.NewEditIndex;
        bindinggrd();
        DropDownList cboUOM = (DropDownList)grd.Rows[e.NewEditIndex].FindControl("cboUOM");
        bll.vBindingFieldValueToCombo(ref cboUOM, "uom");
        cboUOM.SelectedValue = lblUOM.Text;    
    }
    protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grd.EditIndex = -1;
        bindinggrd();
    }

    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
            
            TextBox txtqty = (TextBox)grd.Rows[e.RowIndex].FindControl("txtqty");
            DropDownList cboUOM = (DropDownList)grd.Rows[e.RowIndex].FindControl("cboUOM");
            Label lblsalespointCD = (Label)grd.Rows[e.RowIndex].FindControl("lblsalespointCD");
            Label lbltarget_No = (Label)grd.Rows[e.RowIndex].FindControl("lbltarget_No");
            Label lblprodcode = (Label)grd.Rows[e.RowIndex].FindControl("lblprodcode");
            TextBox txtremark = (TextBox)grd.Rows[e.RowIndex].FindControl("txtremark");
            
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@target_No", lbltarget_No.Text));
            arr.Add(new cArrayList("@salespointCD", lblsalespointCD.Text));
            arr.Add(new cArrayList("@prod_cd", lblprodcode.Text));
            arr.Add(new cArrayList("@qty", txtqty.Text));
            arr.Add(new cArrayList("@uom", cboUOM.SelectedValue));
            arr.Add(new cArrayList("@remark", txtremark.Text));
            bll.vUpdatetsalestargetsp_dtl(arr);
            grd.EditIndex = -1; arr.Clear();
            bindinggrd();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('The record has been updted','Sucessfully...','success');", true);//by Othman 30-8-15        
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
    }

    protected void Refreshgridview(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();

        //arr.Add(new cArrayList("@monthcd", cbmonth.SelectedValue.ToString()));
        //arr.Add(new cArrayList("@yearcd", cbyear.SelectedValue.ToString()));
        //arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        //arr.Add(new cArrayList("@salesman_cd", cbsalesmancd.SelectedValue.ToString()));
     //   bll.vBindingGridToSp(ref grd, "sp_tsalestargetsp_get", arr); 
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        if (txtargetno.Text == "" || txtargetno.Text == "NEW")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Please Save Before Print, data can not print','error');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
            return;
        }

        if (lbstatus.Text == "COMPLETE")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction has been complete, data can not print','error');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@SalesPointCD", cbsalespoint.SelectedValue));
        arr.Add(new cArrayList("@target_no", txtargetno.Text));
        Session["lParamsalestargetsp"] = arr;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=salestargetsp');", true);
        List<cArrayList> arr1 = new List<cArrayList>();
        arr1.Add(new cArrayList("@SalesPointCD", cbsalespoint.SelectedValue));
        arr1.Add(new cArrayList("@target_no", txtargetno.Text));
        arr1.Add(new cArrayList("@sta_id", 'C'));
        bll.vUpdatetsalestargetsp(arr1);
        lbstatus.Text = bll.vLookUp("select fld_desc from tfield_value where fld_nm='sta_id' and fld_valu='C'");
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=spt&no=" + hdtargetno.Value.ToString() + "');", true);  //+ cbmonth.SelectedValue.ToString() + "&y=" + cbyear.SelectedValue.ToString() + "');", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
    }
    protected void chlevel_CheckedChanged(object sender, EventArgs e)
    {
        if (chlevel.Checked)
        {
            cbprod3.Items.Clear();
            cbprod3.CssClass = "makeitreadonly";
            cbprod3.Enabled = false;
        }
        else
        {
            cbprod2_SelectedIndexChanged(sender, e);
            cbprod3.CssClass = "makeitreadwrite";
            cbprod3.Enabled = true;
        }
    }
    protected void cbperiod_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbmonth.Text = cbperiod.SelectedValue.Substring(4);
        lbyear.Text = cbperiod.SelectedValue.Substring(0,4);
    }
    protected void btsave_Click(object sender, EventArgs e)
    {

        if (grd.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please select product for sales target','product Empty','warning');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
            return;
        }

        
        //if (lbstatus.Text == "COMPLETE")
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction has been complete, data can not save','error');", true);
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
        //    return;
        //}
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@period", cbperiod.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salesman_cd", cbsalesmancd.SelectedValue.ToString()));
            arr.Add(new cArrayList("@remark", txremark.Text));
            arr.Add(new cArrayList("@createdtby", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue));
            string sTargetNo = string.Empty;
            bll.vInsertSalesTargetSP(arr, ref sTargetNo);
            txtargetno.Text = sTargetNo;
            //arr.Clear();
            //arr.Add(new cArrayList("@target_no", sTargetNo));
            //arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            //bll.vUpdateSalesTargetSPDtl(arr);
            bindinggrd();
            btsave.Visible = false;
            cbprod1.Items.Clear();
            cbprod2.Items.Clear();
            cbprod3.Items.Clear();
            cbprod3.CssClass = "ro";
            cbprod2.CssClass = "ro";
            cbprod1.CssClass = "ro";
            cbsalesmancd.CssClass = "ro";
            cbperiod.CssClass = "ro"; cbuom.CssClass = "ro";
            hdtargetno.Value = sTargetNo;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Sales target has been saved','" + sTargetNo + "','success');", true);//by Othman 30-8-15
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
      
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_salestargetsp.aspx");
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "pw", "popupwindow('lookup_targetsp.aspx');", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
    }
    protected void bttmp_Click(object sender, EventArgs e)
    {
        SqlDataReader rs = null;
        string sta_id;
        txtargetno.Text = Convert.ToString(Session["lootarget_no"]);
        cbsalespoint.SelectedValue = Convert.ToString(Session["lootarget_salespointCD"]);
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@SalesPointCD", cbsalespoint.SelectedValue));
        arr.Add(new cArrayList("@target_no", txtargetno.Text));
        bll.vGettsalestargetsp(arr, ref rs);
        while (rs.Read())
        {
            cbperiod.SelectedValue = rs["period"].ToString();
            cbperiod_SelectedIndexChanged(sender, e);
            cbsalesmancd.SelectedValue = rs["salesman_cd"].ToString();
            sta_id = rs["sta_id"].ToString();
            lbstatus.Text = bll.vLookUp("select fld_desc from tfield_value where fld_nm='sta_id' and fld_valu='"+sta_id+"'");
        } rs.Close();
        bindinggrd();
    }
    public void bindinggrd()
    {
        string target_no;
        totalQty = 0;
        if (txtargetno.Text == "" || txtargetno.Text == "NEW") { target_no = Request.Cookies["usr_id"].Value.ToString(); } else { target_no = txtargetno.Text; }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@target_no", target_no));
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue));
        bll.vBindingGridToSp(ref grd, "sp_tsalestargetsp_dtl_get", arr);
    }
    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblqty = (Label)e.Row.FindControl("lblqty");
            decimal qty;
            if (lblqty != null)
                qty = decimal.Parse(lblqty.Text);
            else
                qty = 0;
            totalQty = totalQty + qty;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblTotalqty = (Label)e.Row.FindControl("lblTotalqty");
            lblTotalqty.Text = totalQty.ToString();
        }
    }
    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (lbstatus.Text == "COMPLETE")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction already complete can not be delete','error');", true);
            return;
        }

        Label lblsalespointCD = (Label)grd.Rows[e.RowIndex].FindControl("lblsalespointCD");
        Label lbltarget_No = (Label)grd.Rows[e.RowIndex].FindControl("lbltarget_No");
        Label lblprodcode = (Label)grd.Rows[e.RowIndex].FindControl("lblprodcode");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@target_No", lbltarget_No.Text));
        arr.Add(new cArrayList("@salespointCD", lblsalespointCD.Text));
        arr.Add(new cArrayList("@prod_cd", lblprodcode.Text));
        bll.vDeletetsalestargetsp_dtl(arr);
        bindinggrd();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('The record has been deleted','Sucessfully...','success');", true);    
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
    }
}