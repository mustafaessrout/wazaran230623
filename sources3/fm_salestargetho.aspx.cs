using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_salestargetho : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm");

            arr.Add(new cArrayList("@level_no", 1));
            bll.vBindingComboToSp(ref cbprod1, "sp_tmst_product_get", "prod_cd", "prod_nm", arr);
            cbprod1_SelectedIndexChanged(sender, e);
            bll.vBindingFieldValueToCombo(ref cbmonth, "mth");
            bll.vBindingFieldValueToCombo(ref cbyear, "yr");
            bll.vBindingFieldValueToCombo(ref cbuom, "uom");
            cbuom.SelectedValue = "CTN";
           // Refreshgridview(this.grd, e);
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vDelWrkSalesTargetDtl(arr);
            bll.vDelWrkSalesTargetHo(arr);
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
    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        //if (txqty.Text != "" && cbuom.SelectedItem.Text != "")
        //{
        //    string sPoNo = "";

        //    List<cArrayList> arr = new List<cArrayList>();
        //    arr.Add(new cArrayList("@monthcd", cbmonth.SelectedValue.ToString()));
        //    arr.Add(new cArrayList("@yearcd", cbyear.SelectedValue.ToString()));
        //    arr.Add(new cArrayList("@uom", cbuom.SelectedValue.ToString()));
        //    arr.Add(new cArrayList("@qty", txqty.Text));
        //    arr.Add(new cArrayList("@prod_cd", cbprod3.SelectedValue.ToString()));
        //    arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        //    bll.vInsertSalesTargetHO(arr, ref sPoNo);
        //    arr.Clear();
        //    Refreshgridview(this.grd, e);
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('"+sPoNo+"...','The record has been inserted','success');", true);//by Othman 30-8-15
        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please insert ...','quantity and UOM ','error');", true);//by Othman 30-8-15
        //}
         List<cArrayList> arr = new List<cArrayList>();
        //lb_taget_cd.Text = bll.vLookUp("select target_cd from tmst_salestargetho where monthcd='" + cbmonth.SelectedValue + "' and yearcd='" + cbyear.SelectedValue + "'");
        //    if (txqty.Text != "" && cbuom.SelectedItem.Text != "")
        //    {
               
                arr.Add(new cArrayList("@created_dt", DateTime.Today.Date.ToString()));
                arr.Add(new cArrayList("@monthcd", cbmonth.SelectedValue.ToString()));
                arr.Add(new cArrayList("@yearcd", cbyear.SelectedValue.ToString()));
                arr.Add(new cArrayList("@uom", cbuom.SelectedValue.ToString()));
                arr.Add(new cArrayList("@qty", txqty.Text));
                arr.Add(new cArrayList("@prod_cd", cbprod3.SelectedValue.ToString()));
                arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                bll.vInserttworkSalesTargetHO(arr);
                arr.Clear();
                //arr.Add(new cArrayList("@prod_cd", cbprod3.SelectedValue.ToString()));
                //arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                bll.vBindingGridToSp(ref grd, "sp_twrk_tsalestargetho_get", arr);
            //    cbmonth.Enabled = false;
            //    cbyear.Enabled = false;
            //    cbsalespoint.Enabled = false;
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please insert ...','quantity and UOM ','error');", true);//by Othman 30-8-15
            //}              
    }
  //  protected void Refreshgridview(object sender, EventArgs e)
    //{
      //  List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@monthcd", cbmonth.SelectedValue.ToString()));
        //arr.Add(new cArrayList("@yearcd", cbyear.SelectedValue.ToString()));
        //arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        //bll.vBindingGridToSp(ref grd, "sp_tsalestargetho_get", arr);
    //}

    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
     //TextBox txqty = (TextBox)grd.Rows[e.RowIndex].FindControl("txqty");
     //if (txqty.Text != "")
     //{
     //    List<cArrayList> arr = new List<cArrayList>();
     //    Label lbmonthcd = (Label)grd.Rows[e.RowIndex].FindControl("lbmonthcd");
     //    Label lbyearcd = (Label)grd.Rows[e.RowIndex].FindControl("lbyearcd");
     //    HiddenField hdprod_nm = (HiddenField)grd.Rows[e.RowIndex].FindControl("hdprod_nm");
     //    HiddenField hdsalespoint_nm = (HiddenField)grd.Rows[e.RowIndex].FindControl("hdsalespoint_nm");
     //    arr.Add(new cArrayList("@monthcd", lbmonthcd.Text));
     //    arr.Add(new cArrayList("@yearcd", lbyearcd.Text));
     //    arr.Add(new cArrayList("@prod_cd", hdprod_nm.Value.ToString()));
     //    arr.Add(new cArrayList("@salespointcd", hdsalespoint_nm.Value.ToString()));
     //    arr.Add(new cArrayList("@qty", txqty.Text));
     //    bll.vUpdateSalesTargetHO(arr);
     //    grd.EditIndex = -1;            
     //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('The record has been updated','Sucessfully...','success');", true);//by Othman 30-8-15        
     //    Refreshgridview(this.grd, e);
     //}
     //else
     //{
     //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please insert the quantity...','','error');", true);//by Othman 30-8-15
     //}
    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
     //  grd.EditIndex = e.NewEditIndex;
     //  Refreshgridview(this.grd, e);
    }
    protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {       
     //  grd.EditIndex = -1;
     //  Refreshgridview(this.grd, e);
    }

    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        Label lbqty = (Label)grd.Rows[e.RowIndex].FindControl("qty");
        Label lbmonthcd = (Label)grd.Rows[e.RowIndex].FindControl("lbmonthcd");
        Label lbyearcd = (Label)grd.Rows[e.RowIndex].FindControl("lbyearcd");
        HiddenField hdprod_nm = (HiddenField)grd.Rows[e.RowIndex].FindControl("hdprod_nm");
        HiddenField hdsalespoint_nm = (HiddenField)grd.Rows[e.RowIndex].FindControl("hdsalespoint_nm");
        arr.Add(new cArrayList("@monthcd", lbmonthcd.Text));
        arr.Add(new cArrayList("@yearcd", lbyearcd.Text));
        arr.Add(new cArrayList("@prod_cd", hdprod_nm.Value.ToString()));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
       // bll. (arr);
        grd.EditIndex = -1;
       // Refreshgridview(this.grd, e);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('The record has been Deleted','Sucessfully...','success');", true);//by Othman 30-8-15             
    }

    protected void btprint_Click(object sender, EventArgs e)
    {
//ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=STHOmn&monthcd=" + cbmonth.SelectedValue + "&&yearcd=" + cbyear.SelectedValue + "&salespointcd=" + cbsalespoint.SelectedValue + "');", true);            
    }
    protected void btprint1_Click(object sender, EventArgs e)
    {
//ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=STHOyr&yearcd=" + cbyear.SelectedValue + "&salespointcd=" + cbsalespoint.SelectedValue + "');", true);            
    }
    protected void btsave_Click(object sender, EventArgs e)
    {

        string sTargetCode = string.Empty;
            List<cArrayList> arr = new List<cArrayList>();
            //arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            //arr.Add(new cArrayList("@monthcd", cbmonth.SelectedValue.ToString()));
            //arr.Add(new cArrayList("@yearcd", cbyear.SelectedValue.ToString()));
            //bll.vInsertSalesTargetHO(arr, ref sTargetCode);
            //lb_taget_cd.Text = sTargetCode;
            arr.Clear();
            arr.Add(new cArrayList("@target_cd", sTargetCode));
            bll.vBindingGridToSp(ref grd, "sp_tsalestargetho_dtl_get", arr);
           // string sPoNo = "";
           // if (lb_taget_cd.Text == "")
           // {
           //     bll.vInserttmstSalesTargetHO(arr, ref sPoNo);
           //     lb_taget_cd.Text = sPoNo;
           
           //}
           // lb_taget_cd.Visible = true;
            
           // arr.Add(new cArrayList("@target_cd", lb_taget_cd.Text));
           // bll.vInserttSalesTargetdtl(arr);
           // if (sPoNo != "")
           // {
           //     ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('" + sPoNo + "...','The record has been inserted','success');", true);//by Othman 30-8-15
           // }
           // else
           // {
           //     ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('" + lb_taget_cd.Text + "...','The record has been inserted','success');", true);//by Othman 30-8-15
           // }
            btsave.Enabled = false;       

    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_salestargetho.aspx");
    }
}