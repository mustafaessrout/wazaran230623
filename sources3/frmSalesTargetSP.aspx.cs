using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.SqlClient;
public partial class frmSalesTargetSP : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            List<cArrayList> arr = new List<cArrayList>();
            //arr.Add(new cArrayList("@pos_id", "SAL"));
            //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
            //bll.vBindingComboToSp(ref cbemp_cd, "sp_tmst_employee_sal_get", "emp_cd", "emp_nm", arr);
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
            arr.Add(new cArrayList("@qry_cd", "SalesJob"));
            bll.vBindingComboToSp(ref cbemp_cd, "sp_tmst_employee_sal_get", "emp_cd", "emp_desc", arr);
            bll.vBindingFieldValueToCombo(ref cbSTDuom, "uom");
            txQty.Text = "0";

        }
    }
    protected void btadd_Click(object sender, EventArgs e)
    {

        if (txslsTargetCD.Text == null || txslsTargetCD.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "window.alert('Please select the sales target code !')", true);
        }
        else if (txSTDQty.Text=="")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "window.alert('Quantity must be filed !')", true);
        }
        else
        { 
        int ntarget = int.Parse(bll.vLookUp("select isnull(sum(STDqty),0) from tblSalesTargetSPDet where slsTargetSPID='" + txslsTargetSPID.Text + "'"));
        ntarget = ntarget+int.Parse(txSTDQty.Text);
        if (txslsTargetSPCD.Text == "")
        {
            btsave_Click(sender, e);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "window.alert('Please save the data !')", true);
        }
        //else
        //{
        if (ntarget  >= int.Parse(txQty.Text))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "window.alert('the quantity greater than sales target !')", true);
            }
        else
        { 
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@slsTargetSPID", txslsTargetSPID.Text));
            arr.Add(new cArrayList("@salespointcd", txSalesPointCD.Text));
            arr.Add(new cArrayList("@emp_cd", cbemp_cd.SelectedValue.ToString()));
            arr.Add(new cArrayList("@STDQty", txSTDQty.Text));
            arr.Add(new cArrayList("@STDUOM", cbSTDuom.SelectedValue.ToString()));
            arr.Add(new cArrayList("@STDremark", txSTDremark.Text));
            bll.vInserttblSalesTargetSPDet(arr);
            arr.Clear();
            arr.Add(new cArrayList("@slsTargetSPID", txslsTargetSPID.Text));
            arr.Add(new cArrayList("@salespointcd", txSalesPointCD.Text));
            bll.vBindingGridToSp(ref grd, "sp_tblSalesTargetSPDet_get", arr);
            lbltotal.Text = string.Format("{0:#,#}", ntarget);
            
        }
        }
        txQty.Text = "0";
    }
    protected void grd_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lbemp_cd = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbemp_cd");
        Label lbSTDqty = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbSTDqty");
        Label lbSTDUOM = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbSTDUOM");
        Label lbSTDremark = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbSTDremark");

        cbemp_cd.SelectedValue = lbemp_cd.Text;
        txSTDQty.Text = lbSTDqty.Text;
        cbSTDuom.SelectedValue = lbSTDUOM.Text;
        txSTDremark.Text = lbSTDremark.Text;
    }
    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbseqID = (Label)grd.Rows[e.RowIndex].FindControl("lbseqID");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@seqID", lbseqID.Text));
        bll.vDeletetblSalesTargetSPDet(arr);
        arr.Clear();
        arr.Add(new cArrayList("@slsTargetSPID", txslsTargetSPID.Text));
        arr.Add(new cArrayList("@salespointcd", txSalesPointCD.Text));
        bll.vBindingGridToSp(ref grd, "sp_tblSalesTargetSPDet_get", arr);
        int ntarget = int.Parse(bll.vLookUp("select isnull(sum(STDqty),0) from tblSalesTargetSPDet where slsTargetSPID='" + txslsTargetSPID.Text + "'"));
        lbltotal.Text = string.Format("{0:#,#}", ntarget);
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        if (txslsTargetSPCD.Text == null || txslsTargetSPCD.Text == "")
        {

            string sslsTargetSPCD = "0";
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@monthCD", txMonthCD.Text));
            arr.Add(new cArrayList("@SalespointCD", txSalesPointCD.Text));
            arr.Add(new cArrayList("@refSlsTargetDetID", txrefSlsTargetDetID.Text));
            bll.vInsertTblSalesTargetSP(arr, ref sslsTargetSPCD);
            txslsTargetSPCD.Text = sslsTargetSPCD;
            txslsTargetSPID.Text = bll.vLookUp("select slsTargetSPID from tblSalesTargetSP where slsTargetSPCD='" + sslsTargetSPCD + "'");

        }
        else
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@monthCD", txMonthCD.Text));
            arr.Add(new cArrayList("@SalespointCD", txSalesPointCD.Text));
            arr.Add(new cArrayList("@slsTargetSPCD", txslsTargetSPCD.Text));
            arr.Add(new cArrayList("@refSlsTargetDetID", txrefSlsTargetDetID.Text));
            bll.vUpdateTblSalesTargetSP(arr);
        }

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "window.alert('Data saved successfully !')", true);
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmSalesTargetSP.aspx");
    }
    protected void bttmp_Click(object sender, EventArgs e)
    {

        SqlDataReader rs = null;
        txrefSlsTargetDetID.Text = Convert.ToString(Session["looslstargetDetID"]);
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@SalesPointCD", Request.Cookies["sp"].Value));
        arr.Add(new cArrayList("@slstargetDetID", Convert.ToString(Session["looslstargetDetID"])));
        bll.vGetTblSalesTargetDet(arr, ref rs);
        while (rs.Read())
        {
            
            txslsTargetCD.Text=rs["slsTargetCD"].ToString();
            txrefSlsTargetDetID.Text = rs["slsTargetDetID"].ToString();
            txMonthCD.Text = rs["MonthCD"].ToString();
            txSalesPointCD.Text = rs["SalespointCD"].ToString();
            txprod_cd.Text = rs["prod_cd"].ToString();
            txprod_nm.Text = rs["prod_nm"].ToString();
            txQty.Text = rs["Qty"].ToString();
            txRemark.Text = rs["Remark"].ToString();
            txSalesPointCD.Text = rs["SalesPointCD"].ToString();
            txprod_cd2.Text = rs["prod_cd2"].ToString();
            txprod_nm2.Text = rs["prod_nm2"].ToString();
            txUOM.Text = rs["UOM"].ToString();
        } rs.Close();
        
    }
    protected void bttmp2_Click(object sender, EventArgs e)
    {

        SqlDataReader rs = null;
        txslsTargetSPID.Text = Convert.ToString(Session["looSalesTargetSPID"]);
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@SalesPointCD", Request.Cookies["sp"].Value));
        arr.Add(new cArrayList("@slstargetSPID", txslsTargetSPID.Text));
        bll.vGetTblSalesTargetSP(arr, ref rs);
        while (rs.Read())
        {
            txslsTargetSPCD.Text = rs["slsTargetSPCD"].ToString();
            //txslsTargetSPID.Text = rs["slsTargetSPID"].ToString();
            txslsTargetCD.Text = rs["slsTargetCD"].ToString();
            txrefSlsTargetDetID.Text = rs["slsTargetDetID"].ToString();
            txMonthCD.Text = rs["MonthCD"].ToString();
            txSalesPointCD.Text = rs["SalespointCD"].ToString();
            txprod_cd.Text = rs["prod_cd"].ToString();
            txprod_nm.Text = rs["prod_nm"].ToString();
            txQty.Text = rs["Qty"].ToString();
            txRemark.Text = rs["Remark"].ToString();
            txSalesPointCD.Text = rs["SalesPointCD"].ToString();
            txprod_cd2.Text = rs["prod_cd2"].ToString();
            txprod_nm2.Text = rs["prod_nm2"].ToString();
            txUOM.Text = rs["UOM"].ToString();
            lblfooter.Text = rs["DispRec"].ToString();
        } rs.Close();
        arr.Clear();
        arr.Add(new cArrayList("@slsTargetSPID", txslsTargetSPID.Text));
        arr.Add(new cArrayList("@salespointcd", txSalesPointCD.Text));
        bll.vBindingGridToSp(ref grd, "sp_tblSalesTargetSPDet_get", arr);
        //menampilkan total
        int ntarget = int.Parse(bll.vLookUp("select isnull(sum(STDqty),0) from tblSalesTargetSPDet where slsTargetSPID='" + txslsTargetSPID.Text + "'"));
        lbltotal.Text = string.Format("{0:#,#}", ntarget);
    }
}