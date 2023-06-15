using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.SqlClient;

public partial class frmSalesTarget : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingComboToSp(ref cbMonthCD, "sp_tblTRYearMonth_get", "MonthCD", "ymtName");
            bll.vBindingComboToSp(ref cbSalespointCD, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@level_no", "1"));
            bll.vBindingComboToSp(ref cbprod_cd, "sp_tmst_product_get", "prod_cd", "prod_nm", arr);
            bll.vBindingFieldValueToCombo(ref cbuom, "uom");
            cbprod_cd_SelectedIndexChanged(sender, e);
            txQty.Text = "0";
        }
    }
    protected void btsave_Click(object sender, EventArgs e)
    {

        if (txslsTargetCD.Text == null || txslsTargetCD.Text == "") 
        {
  
        string  sslsTargetCD = "0";
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@monthCD", cbMonthCD.SelectedValue.ToString()));
        arr.Add(new cArrayList("@SalespointCD", cbSalespointCD.SelectedValue.ToString()));
        bll.vInsertTblSalesTarget(arr,  ref sslsTargetCD);
        txslsTargetCD.Text = sslsTargetCD;
        txslsTargetID.Text = bll.vLookUp("select isnull(slsTargetID,0) from tblSalesTarget where slsTargetCD='" + sslsTargetCD + "'");
        
        }
        else
        {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@monthCD", cbMonthCD.SelectedValue.ToString()));
        arr.Add(new cArrayList("@SalespointCD", cbSalespointCD.SelectedValue.ToString()));
        arr.Add(new cArrayList("@slsTargetCD", txslsTargetCD.Text));
        bll.vUpdateTblSalesTarget(arr);
        }
        int ntarget = int.Parse(bll.vLookUp("select isnull(sum(qty),0) from tblSalesTargetDet where slsTargetID='" + txslsTargetID.Text + "'"));
        lbltotal.Text = string.Format("{0:#,#}", ntarget);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "window.alert('Data saved successfully !')", true);

    }
    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        Label lbslstargetDetID2 = (Label)grd.Rows[e.RowIndex].FindControl("lbslstargetDetID");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@slstargetDetID", lbslstargetDetID2.Text));
        bll.vDeletetblSalesTargetDet(arr);
        arr.Clear();
        arr.Add(new cArrayList("@salespointcd", cbSalespointCD.SelectedValue));
        arr.Add(new cArrayList("@slsTargetCD", txslsTargetCD.Text));
        bll.vBindingGridToSp(ref grd, "sp_tblSalesTargetDet_get", arr);
   
    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        if (txslsTargetCD.Text == "")
        {
            btsave_Click( sender,  e);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "window.alert('Please save the data !')", true);
        }
        //else
        //{
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@slsTargetCD", txslsTargetCD.Text));
        arr.Add(new cArrayList("@prod_cd", cbprod_cd.SelectedValue.ToString()));
        arr.Add(new cArrayList("@prod_cd2", cbprod_cd2.SelectedValue.ToString()));
        arr.Add(new cArrayList("@Qty", txQty.Text));
        arr.Add(new cArrayList("@UOM", cbuom.SelectedValue.ToString()));
        arr.Add(new cArrayList("@remark", txremark.Text));
        bll.vInserttblSalesTargetDet(arr);
        arr.Clear();
        arr.Add(new cArrayList("@salespointcd", cbSalespointCD.SelectedValue));
        arr.Add(new cArrayList("@slsTargetCD", txslsTargetCD.Text));
        bll.vBindingGridToSp(ref grd, "sp_tblSalesTargetDet_get", arr);
        //}
        int ntarget = int.Parse(bll.vLookUp("select isnull(sum(qty),0) from tblSalesTargetDet where slsTargetID='" + txslsTargetID.Text + "'"));
        lbltotal.Text = string.Format("{0:#,#}", ntarget);
        txQty.Text = "0";
    }
    protected void bttmp_Click(object sender, EventArgs e)
    {

        SqlDataReader rs = null;
        txslsTargetCD.Text = Convert.ToString(Session["looSalesTargetCD"]);
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@slsTargetCD", txslsTargetCD.Text));
        bll.vGetTblSalesTarget(arr, ref rs);
        while (rs.Read())
        {
            txslsTargetID.Text = rs["slsTargetID"].ToString();
            cbMonthCD.Text = rs["MonthCD"].ToString();
            cbSalespointCD.Text = rs["SalespointCD"].ToString();
            lblfooter.Text = rs["DispRec"].ToString();
            
        } rs.Close();
        arr.Clear();
        arr.Add(new cArrayList("@salespointcd", Convert.ToString(Session["looSalesTargetSalespointCD"])));
        arr.Add(new cArrayList("@slsTargetCD", txslsTargetCD.Text));
        bll.vBindingGridToSp(ref grd, "sp_tblSalesTargetDet_get", arr);
        int ntarget = int.Parse(bll.vLookUp("select isnull(sum(qty),0) from tblSalesTargetDet where slsTargetID='" + txslsTargetID.Text + "'"));
        lbltotal.Text = string.Format("{0:#,#}", ntarget);
        
    }

    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmSalesTarget.aspx");
    }
    protected void cbprod_cd_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@level_no", "2"));
        arr.Add(new cArrayList("@prod_cd_parent", cbprod_cd.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbprod_cd2, "sp_tmst_product_get", "prod_cd", "prod_nm", arr);
        
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

        Label lbprod_cd = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbprod_cd");
        Label lbprod_cd2 = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbprod_cd2");
        Label lbqty = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbqty");
        Label lbUOM = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbUOM");
        Label lbremark = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbremark");
        
        cbprod_cd.SelectedValue = lbprod_cd.Text;
        cbprod_cd_SelectedIndexChanged(sender, e);
        cbprod_cd2.SelectedValue = lbprod_cd2.Text;
        txQty.Text = lbqty.Text;
        cbuom.SelectedValue = lbUOM.Text;
        txremark.Text = lbremark.Text;
     
        
        
    }
    protected void grd_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btdel_Click(object sender, EventArgs e)
    {

    }
    protected void cbprod_cd2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}