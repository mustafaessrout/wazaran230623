using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_lookup_SalesOrderDisc : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@Prod_cd", Session["looDiscProd_cd"]));
            arr.Add(new cArrayList("@cust_cd", Session["looDisccust_cd"]));
            arr.Add(new cArrayList("@qty", Session["looDiscqty"]));
            arr.Add(new cArrayList("@so_dt", Session["looDiscSoDocDate"]));
            arr.Add(new cArrayList("@salespointcd", Session["looDiscsalespointcd"]));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value));
            arr.Add(new cArrayList("@SOID", Session["looDiscSOID"]));
            bll.vInserttwrk_tblSODtlDisc(arr);
            binding();
            
        }
    }
   void binding()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", Session["looDiscsalespointcd"]));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value));
        arr.Add(new cArrayList("@SOID", Session["looDiscSOID"]));
        bll.vBindingGridToSp(ref grd, "sp_twrk_tblSODtlDisc_get", arr);
    }
   //void inserttwrk_tblSODtlDisc()
   //{
   //    List<cArrayList> arr = new List<cArrayList>();
   //    arr.Add(new cArrayList("@Prod_cd", Session["looDiscProd_cd"]));
   //    arr.Add(new cArrayList("@cust_cd", Session["looDisccust_cd"]));
   //    arr.Add(new cArrayList("@qty", Session["looDiscqty"]));
   //    arr.Add(new cArrayList("@so_dt", Session["looDiscSoDocDate"]));
   //    arr.Add(new cArrayList("@salespointcd", Session["looDiscsalespointcd"]));
   //    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value));
   //    arr.Add(new cArrayList("@SOID", Session["looDiscSOID"]));
   //    bll.vInserttwrk_tblSODtlDisc( arr);
   //}
    protected void btok_Click(object sender, EventArgs e)
    {

        
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@SOID", Session["looDiscSOID"]));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value));
        arr.Add(new cArrayList("@SalesPointCD", Session["looDiscsalespointcd"]));
        bll.vInserttblSODtlDisc(arr);

        Session["looSOID"] = Session["looDiscSOID"];
        Session["looSOSalespointCD"] = Session["looDiscsalespointcd"];
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "updpnl2", "closewin()", true);
    }
    protected void grd_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Label lbSOID = (Label)grd.SelectedRow.FindControl("lbSOID");
        Label lbsalespointCD = (Label)grd.SelectedRow.FindControl("lbsalespointCD");

        //Session["looSOSOID"] = lbSOID.Text;
        Session["looSOSalespointCD"] = lbsalespointCD.Text;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "updpnl", "closewin()", true);
    }
    protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grd.EditIndex = -1;
        binding();
    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grd.EditIndex = e.NewEditIndex;
        binding();
        GridViewRow row = (GridViewRow)grd.Rows[e.NewEditIndex];
        Label lbfree = (Label)row.FindControl("lbfree");
        TextBox txQtyFree = (TextBox)row.FindControl("txQtyFree");
        txQtyFree.Text = lbfree.Text;
    }
    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = (GridViewRow)grd.Rows[e.RowIndex];
        Label lbID = (Label)row.FindControl("lbID");
        Label lbfree = (Label)row.FindControl("lbfree");
        TextBox txQtyFree = (TextBox)row.FindControl("txQtyFree");
        Label lbdisc_cd = (Label)row.FindControl("lbdisc_cd");
        Label lbusr_id = (Label)row.FindControl("lbusr_id");
        Label lbSOID = (Label)row.FindControl("lbSOID");

        double dQtySODisc = Convert.ToDouble(bll.vLookUp("select sum(isnull(sopDQty,0)) from tblSODtlDisc where disc_cd='" + lbdisc_cd.Text + "' and soid='" + lbSOID.Text + "'"));
        double dQty =  Convert.ToDouble(bll.vLookUp("select sum(isnull(QtyFree,0)) from twrk_tblSODtlDisc where usr_id='" + lbusr_id.Text + "' AND disc_cd='" + lbdisc_cd.Text + "' and soid='"+lbSOID.Text+"'"));
        double dQtyFree = dQtySODisc+dQty + Convert.ToDouble(string.IsNullOrEmpty((string)txQtyFree.Text) ? "0" : txQtyFree.Text);
        double dFree = Convert.ToDouble(lbfree.Text) - dQty;

        if (dQtyFree > dFree)
        {

            txQtyFree.Text = Convert.ToString(dFree);
        }

        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@ID", lbID.Text));
        arr.Add(new cArrayList("@QtyFree", Convert.ToDecimal(txQtyFree.Text)));
        bll.vUpdatetwrk_tblSODtlDisc(arr);

        grd.EditIndex = -1;
        binding();
        lbTotal.Text = bll.vLookUp("select sum(QtyFree) from twrk_tblSODtlDisc where usr_id='" + Request.Cookies["usr_id"].Value + "'"); 
        
    }
}