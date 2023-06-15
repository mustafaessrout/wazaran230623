using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class trn_fm_supplier : System.Web.UI.Page
{
    cbll bll = new cbll();
    Utitlity ut = new Utitlity();
    cdal cdl = new cdal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindControl();
        }
    }
    private void bindControl()
    {
        List<cArrayList> arr = new List<cArrayList>();
        DataTable dt = new DataTable();
        bll.vBindingFieldValueToCombo(ref ddlCountry, "nationality");
        BindGrid();
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
        dt = cdl.GetValueFromSP("sp_tacc_mst_supplier_get", arr);
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
            if (Convert.ToString(txtSupplierName.Text) == "" || Convert.ToString(txtSupplierName.Text) == null)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Supplier name required','Name required','warning');", true);
                return;
            }
            else if (Convert.ToString(ddlCountry.Text) == "" || Convert.ToString(ddlCountry.Text) == null)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Country name required','Country name required','warning');", true);
                return;
            }
            else if (Convert.ToString(txtSupplierAddress.Text) == "" || Convert.ToString(txtSupplierAddress.Text) == null)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Supplier address required','address required','warning');", true);
                return;
            }
        
            string supplierCD = string.Empty;
            arr.Add(new cArrayList("@supplier_nm", Convert.ToString(txtSupplierName.Text)));
            arr.Add(new cArrayList("@companyRegistration", Convert.ToString(txtCompanyRegistration.Text)));
            arr.Add(new cArrayList("@supplier_address", Convert.ToString(txtSupplierAddress.Text)));
            arr.Add(new cArrayList("@supplier_city", Convert.ToString(txtCity.Text)));
            arr.Add(new cArrayList("@supplier_country", Convert.ToString(ddlCountry.SelectedValue)));
            arr.Add(new cArrayList("@supplier_contactNumber", Convert.ToString(txtContactNumber.Text)));
            arr.Add(new cArrayList("@supplier_sta_id", Convert.ToString("A")));
            
            arr.Add(new cArrayList("@supplierTax_no", txtSupplierTax_no.Text.TrimEnd().TrimStart()));

            if (btsave.Text == "Save")
            {
                arr.Add(new cArrayList("@createdby", Convert.ToString(Request.Cookies["usr_id"].Value.ToString())));
                bll.vInsertMastAccSupplier(arr, ref supplierCD);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Data saved successfully','" + supplierCD + "','success');", true);
            }
            else if (btsave.Text == "Update")
            {
                arr.Add(new cArrayList("@supplier_cd", Convert.ToString(hdfSupplier_cdValue.Value)));
                bll.vUpdateMastAccSupplier(arr);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Data updated successfully','" + supplierCD + "','success');", true);
            }
            grd.DataSource = null;
            grd.DataBind();
            bindControl();
            clearControl();
            BindGrid();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    void clearControl() {
        txtSupplier.Text = string.Empty;
        txtSupplierName.Text = string.Empty;
        txtCompanyRegistration.Text = string.Empty;
        txtSupplierAddress.Text = string.Empty;
        txtCity.Text = string.Empty;
        txtContactNumber.Text = string.Empty;
        txtSupplierTax_no.Text = string.Empty;
        hdfSupplier_cdValue.Value = string.Empty;
        btsave.Text = "Save"; ;
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        HiddenField hdfSupplier_cd = (HiddenField)grd.Rows[e.NewSelectedIndex].FindControl("hdfSupplier_cd");
        bindControl();
        List<cArrayList> arr = new List<cArrayList>();
        DataTable dt = new DataTable();
        arr.Add(new cArrayList("@supplier_cd", Convert.ToString(hdfSupplier_cd.Value)));
        dt = cdl.GetValueFromSP("sp_tacc_mst_supplier_get", arr);
        if (dt.Rows.Count > 0)
        {
            txtSupplier.Text = Convert.ToString(dt.Rows[0]["supplier_cd"]);
            txtSupplierName.Text = Convert.ToString(dt.Rows[0]["supplier_nm"]);
            txtCompanyRegistration.Text = Convert.ToString(dt.Rows[0]["companyRegistration"]);
            txtSupplierAddress.Text = Convert.ToString(dt.Rows[0]["supplier_address"]);
            txtCity.Text = Convert.ToString(dt.Rows[0]["supplier_city"]);
            ddlCountry.SelectedValue = Convert.ToString(dt.Rows[0]["supplier_country"]);
            txtContactNumber.Text = Convert.ToString(dt.Rows[0]["supplier_contactNumber"]);
            txtSupplierTax_no.Text = Convert.ToString(dt.Rows[0]["supplierTax_no"]);
            hdfSupplier_cdValue.Value = Convert.ToString(dt.Rows[0]["supplier_cd"]);
            btsave.Text = "Update"; ;
        }
    }
    protected void btlookup_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        DataTable dt = new DataTable();
        arr.Add(new cArrayList("@supplier_cd", Convert.ToString(hdfSupplier_cdValue.Value)));
        dt = cdl.GetValueFromSP("sp_tacc_mst_supplier_get", arr);
        if (dt.Rows.Count > 0)
        {
            txtSupplierName.Text = Convert.ToString(dt.Rows[0]["supplier_nm"]);
            txtCompanyRegistration.Text = Convert.ToString(dt.Rows[0]["companyRegistration"]);
            txtSupplierAddress.Text = Convert.ToString(dt.Rows[0]["supplier_address"]);
            txtCity.Text = Convert.ToString(dt.Rows[0]["supplier_city"]);
            ddlCountry.SelectedValue = Convert.ToString(dt.Rows[0]["supplier_country"]);
            txtContactNumber.Text = Convert.ToString(dt.Rows[0]["supplier_contactNumber"]);
            txtSupplierTax_no.Text = Convert.ToString(dt.Rows[0]["supplierTax_no"]);
            hdfSupplier_cdValue.Value = Convert.ToString(dt.Rows[0]["supplier_cd"]);
            btsave.Text = "Update"; ;
        }
    }
    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        HiddenField hdfSupplier_cd = (HiddenField)grd.Rows[e.RowIndex].FindControl("hdfSupplier_cd");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@supplier_cd", hdfSupplier_cd.Value));
        bll.vDeleteMastAccSupplier(arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Data deleted successfully','" + hdfSupplier_cd.Value + "','success');", true);
        bindControl();
        clearControl();
        BindGrid();
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('../fm_report2.aspx?src=AllSupplier');", true);
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        clearControl();
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        System.Data.SqlClient.SqlDataReader rs = null;
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        List<string> lCust = new List<string>();
        string sCust = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        // arr.Add(new cArrayList("@salesman_cd", contextKey));
        arr.Add(new cArrayList("@suppliertax_no", prefixText));
        bll.vSearchAccMstSupplier(arr, ref rs);
        while (rs.Read())
        {
            sCust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + "-" + rs["cust_nm"].ToString(), rs["cust_cd"].ToString());
            lCust.Add(sCust);
        }
        rs.Close();
        return (lCust.ToArray());
    }
    protected void txtSupplierTax_no_TextChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@suppliertax_no", txtSupplierTax_no.Text));
        bll.vBindingGridToSp(ref grd, "sp_tacc_mst_supplier_search", arr);
    }
}