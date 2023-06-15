using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_mstdiscount : System.Web.UI.Page
{
    cbll bll = new cbll();
    string _pref;

    public string pref
    { set { _pref = value; } get { return (_pref); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            bll.vBindingFieldValueToCombo(ref cbcusttyp, "OtlCD");
            bll.vBindingFieldValueToCombo(ref cbschtyp, "sch_typ");
            bll.vBindingFieldValueToCombo(ref cbprogram, "prg_typ");
            bll.vBindingFieldValueToCombo(ref cbpaymenttype, "payment_typ");
            bll.vBindingFieldValueToCombo(ref cbdiscscheme, "disc_typ");
            bll.vBindingFieldValueToCombo(ref cbscale, "disc_method");
            arr.Add(new cArrayList("@level_no", "1"));
            bll.vBindingComboToSp(ref cbbranded, "sp_tmst_product_get","prod_cd","prod_nm", arr);
            cbbranded_SelectedIndexChanged(sender, e);
            cbdiscscheme_SelectedIndexChanged(sender, e);
            cbschtyp_SelectedIndexChanged(sender, e);
        }
    }
    protected void btcust_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (cbcusttyp.SelectedValue.ToString() == "ALL") {
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vInsertWrkCustTypeAll(arr);
        }
        else { 
        arr.Add(new cArrayList("@cust_typ", cbcusttyp.SelectedValue.ToString()));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@typ_nm", cbcusttyp.SelectedItem.ToString()));
        bll.vInsertTwrkCustType(arr);
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
       
        }
        bll.vBindingGridToSp(ref grdcust, "sp_twrk_custtype_get", arr);
    }
    protected void grdcust_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
      //  updcust.Update();
        Response.Write("ERR");
    }
    protected void grdcust_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //Button lb = e.Row.FindControl("btdel") as Button;
        //ScriptManager.GetCurrent(this).RegisterPostBackControl(lb);
    }
    protected void btdel_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        foreach (GridViewRow row in grdcust.Rows)
        { 
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk =  (CheckBox) row.FindControl("chkdel");
                if (chk.Checked)
                {
                    Label lbl = (Label) row.FindControl("lbtypecode");
                    arr.Clear();
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    arr.Add(new cArrayList("@cust_typ", lbl.Text));
                    bll.vDelWrkCustType(arr);
                }
            }
        }
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdcust, "sp_twrk_custtype_get",arr);
    }
    protected void cbbranded_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@level_no", "2"));
        arr.Add(new cArrayList("@prod_cd_parent", cbbranded.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbproduct, "sp_tmst_product_get", "prod_cd", "prod_nm", arr);
        cbproduct_SelectedIndexChanged(sender, e);
    }
    protected void cbproduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@prod_cd", cbproduct.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbitem, "sp_tmst_item_get", "item_cd", "item_nm", arr);
    }
    protected void btitemadd_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@item_cd", cbitem.SelectedValue.ToString()));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@item_nm", cbitem.SelectedItem.Text));
        bll.vInsertWrkItem(arr);
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grditem, "sp_twrk_item_get", arr);
    }
    protected void btitemdel_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        foreach (GridViewRow row in grditem.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
            CheckBox chk = (CheckBox) row.FindControl("chkitemdel");
            if (chk.Checked)
            {
                Label lblitem = (Label)row.FindControl("lbitemcd");
                arr.Clear();
                arr.Add(new cArrayList("@usr_id",Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@item_cd", lblitem.Text));
                bll.vDelWrkItem(arr);
            } 
            }
        }
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grditem, "sp_twrk_item_get", arr);
    }
    protected void cbdiscscheme_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    protected void btdiscadd_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@min_qty", txminqty.Text));
        arr.Add(new cArrayList("@free_qty", txfree.Text));
        arr.Add(new cArrayList("@disc_method", cbscale.SelectedValue.ToString()));
        bll.vInsertWrkDiscMethod(arr);
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grddisc, "sp_twrk_discmethod_get", arr);
    }

    protected void btdiscmetdel_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        foreach (GridViewRow row in grddisc.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkdel = (CheckBox)row.FindControl("chkdeldisc");
                if (chkdel.Checked)
                {
                    Label lbl = (Label)row.FindControl("lbmin");
                    Label lbitemcode = (Label)row.FindControl("lbitemcode");
                    arr.Clear();
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    arr.Add(new cArrayList("@min_qty", lbl.Text));
                  //  arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
                    bll.vDelWrkDiscMethod(arr);
                }
            }
        }
        arr.Clear(); arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grddisc, "sp_twrk_discmethod_get",arr);
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        string sOut="";
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@sch_typ", cbschtyp.SelectedValue.ToString()));
        arr.Add(new cArrayList("@ref", txref.Text));
        arr.Add(new cArrayList("@sch_nm", txdesc.Text));
        arr.Add(new cArrayList("@start_dt", dtstart.Text));
        arr.Add(new cArrayList("@end_dt", dtend.Text));
        arr.Add(new cArrayList("@prg_typ", cbprogram.SelectedValue.ToString()));
        arr.Add(new cArrayList("@payment_typ", cbpaymenttype.SelectedValue.ToString()));
        arr.Add(new cArrayList("@createdby" , Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@disc_typ", cbdiscscheme.SelectedValue.ToString()));
        bll.vInsertMstSchema(arr, ref sOut);
        //For Free Item
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@sch_cd", sOut));
        bll.vInsertSchemaFreeItem(arr);
        //For Item whom is discounted
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@sch_cd", sOut));
        bll.vInsertSchemaItem(arr);
        bll.vInsertSchemaSalespoint(arr);
        bll.vInsertSchemaCustType(arr);
        bll.vInsertSchemaMethodItem(arr);
        // arr.Clear();
        txcode.Text = sOut;

    }
    protected void btsearchdisc_Click(object sender, EventArgs e)
    {
        
    }
    protected void cbschtyp_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbschtyp.SelectedValue.ToString() == "A")
        {
          //  lbItemDisc.Visible = true;
        }
        //else {// lbItemDisc.Visible = false; }
    }
    protected void btsearchdisc_Click1(object sender, EventArgs e)
    {

    }

    public void vRefresh()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdfreeitem, "sp_twrk_freeitem_get",arr);
   //     UpdatePanel6.UpdateMode = UpdatePanelUpdateMode.Conditional;
   //     UpdatePanel6.Update();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdfreeitem,"sp_twrk_freeitem_get", arr);
    }
    protected void rdcust_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdcust.SelectedValue == "CN")
        {
            cbcusttyp.Visible = false;
            btselectcust.Visible = true;
            grdcust.Visible = false; grdbycust.Visible = true;
        }
        else { cbcusttyp.Visible = true; btselectcust.Visible = false; grdcust.Visible = true; grdbycust.Visible = false; }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdbycust, "sp_twrk_lookupcustomer_get",arr);
    }

 
}