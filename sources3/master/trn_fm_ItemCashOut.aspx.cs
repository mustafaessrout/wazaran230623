using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class trn_fm_ItemCashOut : System.Web.UI.Page
{
    cbll bll = new cbll();
    cbllHO bllHO = new cbllHO();
    Utitlity ut = new Utitlity();
    cdal cdl = new cdal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindControl();
            BindGrid();
            cbitemtype_SelectedIndexChanged(sender, e);
            cbroutine_SelectedIndexChanged(sender, e);
            cbinout_SelectedIndexChanged(sender, e);
        }
    }
    private void bindControl()
    {
        bll.vBindingFieldValueToCombo(ref cbitemtype, "cashout_typ");
        bll.vBindingFieldValueToCombo(ref cbroutine, "routine");
        bll.vBindingFieldValueToCombo(ref cbinout, "inout");
       
    }



    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    private void BindGrid()
    {
        List<cArrayList> arr = new List<cArrayList>();
        DataTable dt = new DataTable();
       arr.Add(new cArrayList("@cashout_typ", cbitemtype.SelectedValue.ToString()));
        arr.Add(new cArrayList("@inout", cbinout.SelectedValue.ToString()));
        arr.Add(new cArrayList("@routine", cbroutine.SelectedValue.ToString()));
       dt = cdl.GetValueFromSP("sp_tmst_itemcashout_get", arr);
        arr.Clear();
        if (dt.Rows.Count == 0)
        {
            grd.DataSource = null;
            grd.DataBind();
        }
        else
        {
            grd.DataSource = dt;
            grd.DataBind();

        }

    }


    protected void btsave_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        DataTable dt = new DataTable();
        arr.Clear();
        try
        {
            if (Convert.ToString(txitemname.Text) == "" || Convert.ToString(txitemname.Text) == null)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Cash Out Name required','Name required','warning');", true);
                return;
            }
            

            string ItemCashOutCD = string.Empty;
            arr.Add(new cArrayList("@itemco_nm", Convert.ToString( txitemname.Text).ToUpper()));
            arr.Add(new cArrayList("@cashout_typ", cbitemtype.SelectedValue.ToString()));
            arr.Add(new cArrayList("@routine", cbroutine.SelectedValue.ToString()));
            arr.Add(new cArrayList("@inout", cbinout.SelectedValue.ToString()));

            if (btsave.Text == "Save")
            {
                bll.vInsertMstItemCashoutByID(arr, ref ItemCashOutCD);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Data saved successfully','" + ItemCashOutCD + "','success');", true);
            }
            else if (btsave.Text == "Update")
            {
                arr.Add(new cArrayList("@itemco_cd", Convert.ToString(hdfItemCashOut_cdValue.Value)));
                bll.vUpdateMstItemCashoutByID(arr);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Data updated successfully','" + ItemCashOutCD + "','success');", true);
            }
            grd.DataSource = null;
            grd.DataBind();
            //bindControl();
            clearControl();
            BindGrid();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    void clearControl()
    {
        
        txitemname.Text = string.Empty;
        txtCashOut.Text = string.Empty;
        hdfItemCashOut_cdValue.Value = string.Empty;
        btsave.Text = "Save"; ;
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        HiddenField hdfItemCashOut_cd = (HiddenField)grd.Rows[e.NewSelectedIndex].FindControl("hdfItemCashOut_cd");
        
        List<cArrayList> arr = new List<cArrayList>();
        DataTable dt = new DataTable();
        arr.Add(new cArrayList("@itemco_cd", Convert.ToString(hdfItemCashOut_cd.Value)));
        dt = cdl.GetValueFromSP("sp_tmst_itemcashoutByCode_get", arr);
        bindControl();
        if (dt.Rows.Count > 0)
        {
            txitemname.Text = Convert.ToString(dt.Rows[0]["itemco_nm"]);
            cbinout.SelectedValue = Convert.ToString(dt.Rows[0]["inout"]);
            cbitemtype.SelectedValue = Convert.ToString(dt.Rows[0]["cashout_typ"]);
            cbroutine.SelectedValue = Convert.ToString(dt.Rows[0]["routine"]);
            txtCashOut.Text = Convert.ToString(dt.Rows[0]["itemco_cd"]);
            hdfItemCashOut_cdValue.Value = Convert.ToString(dt.Rows[0]["itemco_cd"]);
            btsave.Text = "Update"; ;
        }
    }
    protected void btlookup_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        DataTable dt = new DataTable();
        arr.Add(new cArrayList("@supplier_cd", Convert.ToString(hdfItemCashOut_cdValue.Value)));
        dt = cdl.GetValueFromSP("sp_tacc_mst_supplier_get", arr);
        if (dt.Rows.Count > 0)
        {
           txitemname.Text = Convert.ToString(dt.Rows[0]["supplier_nm"]);
            txtCashOut.Text = Convert.ToString(dt.Rows[0]["companyRegistration"]);
            cbroutine.SelectedValue = Convert.ToString(dt.Rows[0]["companyRegistration"]);
            cbitemtype.SelectedValue = Convert.ToString(dt.Rows[0]["companyRegistration"]);
            cbinout.SelectedValue = Convert.ToString(dt.Rows[0]["companyRegistration"]);
            hdfItemCashOut_cdValue.Value = Convert.ToString(dt.Rows[0]["supplier_cd"]);
            btsave.Text = "Update"; ;
        }
    }
    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        HiddenField hdfItemCashOut_cd = (HiddenField)grd.Rows[e.RowIndex].FindControl("hdfItemCashOut_cd");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@itemco_cd", hdfItemCashOut_cd.Value));
        bll.vDelMstItemCashout(arr);

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Data deleted successfully','" + hdfItemCashOut_cd.Value + "','success');", true);
        clearControl();
        BindGrid();
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('../fm_report2.aspx?src=AllSupplier');", true);
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        bindControl();
        clearControl();
    }

    protected void cbitemtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void cbinout_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void cbroutine_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
}