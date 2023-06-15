using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_discountinfo : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            System.Data.SqlClient.SqlDataReader rs = null;
          //  grditem.Visible = false;
          //  grdprod.Visible = false;
            List<cArrayList> arr = new List<cArrayList>();
            string sDiscCode = Request.QueryString["dc"];
            string sRDCust = string.Empty;
            string sRdItem = bll.vLookUp("select rditem from tmst_discount where disc_cd='" + sDiscCode + "'");
            arr.Add(new cArrayList("@disc_cd", sDiscCode));
            //arr.Add(new cArrayList("@disc_sta_id", "A"));
            bll.vGetMstDiscount(arr, ref rs);
            while (rs.Read())
            {
                lbdiscno.Text = sDiscCode;
                lbdate.Text = Convert.ToDateTime(rs["disc_dt"]).ToShortDateString();
                bll.sFormat2ddmmyyyy(ref lbdate);
                lbstart.Text = Convert.ToDateTime(rs["start_dt"]).ToShortDateString();
                bll.sFormat2ddmmyyyy(ref lbstart);
                lbend.Text = Convert.ToDateTime(rs["end_dt"]).ToShortDateString();
                bll.sFormat2ddmmyyyy(ref lbend);
                lbdelivery.Text = Convert.ToDateTime(rs["delivery_dt"]).ToShortDateString();
                bll.sFormat2ddmmyyyy(ref lbdelivery);
                lbstatus.Text = rs["disc_sta_nm"].ToString();
                lbqtymax.Text = rs["qty_max"].ToString();
                lbpropno.Text = rs["propvendor_no"].ToString();
                sRDCust = rs["rdcustomer"].ToString();
                txcratedby.Text = rs["createdby"].ToString();
                lbdiscmec.Text = bll.vLookUp("select dbo.fn_getfieldvalue('discount_mec','"+rs["discount_mec"].ToString()+"')");
                lbmaxdisc.Text = rs["blockedtype_nm"].ToString() + " | " + Math.Round(Convert.ToDecimal(rs["amount"].ToString()),2).ToString() + " " + rs["uom"].ToString();
                arr.Clear();
                arr.Add(new cArrayList("@disc_cd", sDiscCode));
                bll.vBindingGridToSp(ref grd, "sp_tdiscount_salespoint_get", arr);
                if (sRdItem == "G")
                {
                    grdprod.Visible = true; grditem.Visible = false;
                    bll.vBindingGridToSp(ref grdprod, "sp_tdiscount_product_get", arr); }
                else {
                    grditem.Visible = true; grdprod.Visible = false;
                    bll.vBindingGridToSp(ref grditem, "sp_tdiscount_item_get", arr); };
                bll.vBindingGridToSp(ref grdformula, "sp_tdiscount_formula_get", arr);
                bll.vBindingGridToSp(ref grdfreeitem, "sp_tdiscount_freeitem_get", arr);
                if (sRDCust == "T")
                {
                    bll.vBindingGridToSp(ref grdcusttype, "sp_tdiscount_custtype_get", arr);
                }
                else if (sRDCust == "G")
                {
                    bll.vBindingGridToSp(ref grdcustgroup, "sp_tdiscount_cusgrcd_get", arr);
                }
                else if (sRDCust == "C")
                {
                    bll.vBindingGridToSp(ref grdcustomer, "sp_tdiscount_customer_get", arr);
                }
                bll.vBindingGridToSp(ref grdfreeproduct, "sp_tdiscount_freeproduct_get", arr);
            
               
            } rs.Close();
        }
    }
    protected void grdfreeitem_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdfreeitem.PageIndex = e.NewPageIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@disc_cd", Request.QueryString["dc"]));
        bll.vBindingGridToSp(ref grdfreeitem, "sp_tdiscount_freeitem_get", arr);
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=dc&no=" + Request.QueryString["dc"] + "');", true);
    }
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd.PageIndex = e.NewPageIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@disc_cd", Request.QueryString["dc"])); 
        bll.vBindingGridToSp(ref grd, "sp_tdiscount_salespoint_get", arr);
    }
    protected void grditem_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grditem.PageIndex = e.NewPageIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@disc_cd", Request.QueryString["dc"]));
        bll.vBindingGridToSp(ref grditem, "sp_tdiscount_item_get", arr);
    }
    protected void grdformula_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdformula.PageIndex = e.NewPageIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@disc_cd", Request.QueryString["dc"]));
        bll.vBindingGridToSp(ref grditem, "sp_tdiscount_formula_get", arr);
    }
}