using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class fm_mstitementry : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            if (Request.Cookies["lang"].Value.ToString() == "SA")
            {
             //   bll.vLang(ref lbgroup);
             //   bll.vLang(ref lbbrand);bll.vLang(ref lbsubbrand);
                bll.vLang(ref lbalias);
                bll.vLang(ref lbitemname);
                bll.vLang(ref lbitemcode);
                bll.vLang(ref lbshortname);
            }
            if (Request.QueryString["item"] != null)
            {
                //   List<cArrayList> arr = new List<cArrayList>();
                string sItemCode = Request.QueryString["item"].ToString();
                SqlDataReader rs = null;

                arr.Add(new cArrayList("@item_cd", sItemCode));
                bll.vGetMstItem2(arr, ref rs);
                while (rs.Read())
                {
                    img.ImageUrl = @"/image/" + rs["image_path"].ToString();
                    txitemcode.Text = sItemCode;
                    txitemname.Text = rs["item_nm"].ToString();
                    txarabic.Text = rs["item_arabic"].ToString();

                } rs.Close();
          }
            bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm");
            bll.vBindingComboToSp(ref cbvendor, "sp_tmst_vendor_get", "vendor_cd", "vendor_nm");
           // bll.vBindingFieldValueToCombo(ref cbitemtype, "item_typ");
           // bll.vBindingFieldValueToCombo(ref cbcostcode, "sales_tax_opt");
            bll.vBindingFieldValueToCombo(ref cbuom, "uom");
            arr.Clear();
            arr.Add(new cArrayList("@level_no", 1));
            bll.vBindingComboToSp(ref cbbrand, "sp_tmst_product_get", "prod_cd", "prod_nm",arr);
            arr.Clear();
            arr.Add(new cArrayList("@fld_nm", "otlcd"));
            bll.vBindingGridToSp(ref grdcust, "sp_tfield_value_get", arr);
            cbbrand_SelectedIndexChanged(sender, e);
        }
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        string sImagePath = bll.sGetControlParameter("image_path");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@item_cd", txitemcode.Text));
        arr.Add(new cArrayList("@item_nm", txitemname.Text));
        arr.Add(new cArrayList("@item_shortname", txshortname.Text));
        arr.Add(new cArrayList("@item_arabic", txarabic.Text));
      //  arr.Add(new cArrayList("@item_typ", cbitemtype.SelectedValue.ToString()));
        arr.Add(new cArrayList("@min_stock", txminstock.Text));
        arr.Add(new cArrayList("@uom", cbuom.SelectedValue.ToString()));
        arr.Add(new cArrayList("@size", txsize.Text));
        arr.Add(new cArrayList("@unitprice", txunitprice.Text));
        arr.Add(new cArrayList("@item_cd_vendor", txcodevendor.Text));
        arr.Add(new cArrayList("@vendor_cd", cbvendor.SelectedValue.ToString()));
        arr.Add(new cArrayList("@prod_cd", cbprod.ToString()));
        arr.Add(new cArrayList("@image_path",  txitemcode.Text + ".jpg"));
        arr.Add(new cArrayList("@start_dt", dteffective.Text));
        arr.Add(new cArrayList("@expired_dt", dtExpired.Text));
        arr.Add(new cArrayList("@isactive", "1"));
        arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
        bll.vInsertMstItem(arr);
        if (uplitem.FileName != null)
        { uplitem.SaveAs(sImagePath + txitemcode.Text + ".jpg"); }
        Response.Redirect("fm_mstitemlist.aspx");
       // arr.Add(new cArrayList("@sales_tax_opt", cbsalestaxopt))
    }
    protected void cbbrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@prod_cd_parent", cbbrand.SelectedValue.ToString()));
        arr.Add(new cArrayList("@level_no", 2));
        bll.vBindingComboToSp(ref cbprod, "sp_tmst_product_get", "prod_cd", "prod_nm", arr);
        cbprod_SelectedIndexChanged(sender, e);
    }
    protected void cbprod_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@prod_cd_parent", cbprod.SelectedValue.ToString()));
        arr.Add(new cArrayList("@level_no", 3));
     //   bll.vBindingComboToSp(ref cbsubprod, "sp_tmst_product_get", "prod_cd", "prod_nm", arr);

    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        arr.Add(new cArrayList("@salespoint_nm", cbsalespoint.SelectedItem.Text));
        bll.vInsertWrkSalespoint(arr);
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdsp , "sp_twrk_salespoint_get",arr);
    }
    protected void grdsp_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbsalespoint = (Label)grdsp.Rows[e.RowIndex].FindControl("lbsalespoint");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@salespointcd", lbsalespoint.Text));
        bll.vDelWrkSalespoint(arr);
        arr.Clear();
         arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
         bll.vBindingGridToSp(ref grdsp, "sp_tmst_salespoint_get", arr);
    }


    private void RegisterPostBackControl()
    {
        foreach (GridViewRow row in grdsp.Rows)
        {
            LinkButton lnkFull = row.FindControl("lnkFull") as LinkButton;
            ScriptManager.GetCurrent(this).RegisterPostBackControl(lnkFull);
        }
    }

    protected void grdsp_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Response.Write("TEST");
    }
    protected void grdsp_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Response.Write("TEST");
    }
    protected void btdel_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        foreach(GridViewRow row in grdsp.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk = (CheckBox)row.FindControl("chk");
                if (chk.Checked)
                {
                    arr.Clear();
                    Label lblsp = (Label)row.FindControl("lbsalespointcd");
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    arr.Add(new cArrayList("@salespointcd", lblsp.Text));
                    bll.vDelWrkSalespoint(arr);
                }
            }
        
        }
        arr.Clear();arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdsp, "sp_twrk_salespoint_get", arr);
    }
}