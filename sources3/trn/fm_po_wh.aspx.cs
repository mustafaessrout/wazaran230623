using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class trn_fm_po_wh : System.Web.UI.Page
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
            cbdeliverytype_SelectedIndexChanged(sender, e);
        }
    }
    private void bindControl()
    {
        List<cArrayList> arr = new List<cArrayList>();
        DataTable dt = new DataTable();
        bll.vBindingFieldValueToCombo(ref cbdeliverytype, "delivery_typ");
        bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespointSN_get", "salespointcd", "salespoint_desc", arr);
        arr.Clear();
        arr.Add(new cArrayList("@qry_cd", "driver"));
        bll.vBindingComboToSp(ref cbemp_cd, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
        arr.Clear();
        arr.Add(new cArrayList("@qry_cd", "driver"));
        arr.Add(new cArrayList("@salespointcd", Convert.ToString(cbsalespoint.SelectedValue)));
        bll.vBindingComboToSp(ref cbemp_cd, "sp_tmst_employee_getbyqrybysalpay", "emp_cd", "emp_desc", arr);

    }
    protected void cbsalespoint_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGriddo();
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@qry_cd", "driver"));
        arr.Add(new cArrayList("@salespointcd", Convert.ToString(cbsalespoint.SelectedValue)));
        bll.vBindingComboToSp(ref cbemp_cd, "sp_tmst_employee_getbyqrybysalpay", "emp_cd", "emp_desc", arr);
    }
    protected void grddo_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lbldo = (Label)grddo.Rows[e.NewSelectedIndex].FindControl("lbldo");
        hdfdo.Value = lbldo.Text;
        txdono.Text = hdfdo.Value;

        List<cArrayList> arr = new List<cArrayList>();
        DataTable dt = new DataTable();
        arr.Add(new cArrayList("@do_no", hdfdo.Value));
        dt = cdl.GetValueFromSP("sp_tdo_dtl_get", arr);
        if (dt.Rows.Count > 0)
        {
            BindGrid();
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('No do details found !','No do details found','warning');", true);
        }
    }
    protected void grddo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grddo.PageIndex = e.NewPageIndex;
        BindGriddo();
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
        arr.Add(new cArrayList("@do_no", hdfdo.Value));
        dt = cdl.GetValueFromSP("sp_tdo_dtl_get", arr);
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
    private void BindGriddo()
    {
        List<cArrayList> arr = new List<cArrayList>();
        DataTable dt = new DataTable();
        arr.Add(new cArrayList("@do_sta_id", "L"));
        arr.Add(new cArrayList("@salespointcd", Convert.ToString(cbsalespoint.SelectedValue)));
        dt = cdl.GetValueFromSP("sp_tmst_do_getBySalespoint", arr);


        if (dt.Rows.Count > 0)
        {
            hdfdo.Value = Convert.ToString(dt.Rows[0]["do_no"]);
            grddo.DataSource = dt;
            grddo.DataBind();
        }
        else
        {
            grddo.DataSource = null;
            grddo.DataBind();
        }
    }
    protected void cbdeliverytype_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@comp_sta_id", cbdeliverytype.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbexpedition, "sp_tmst_company_expedition_get", "comp_cd", "comp_nm", arr);
        if (cbdeliverytype.SelectedValue == "OWN")
        {
            cbemp_cd.Visible = true;
            txdriver_name.Visible = false;
        }
        else
        {
            cbemp_cd.Visible = false;
            txdriver_name.Visible = true;
        }
    }
    protected void cbemp_cd_SelectedIndexChanged(object sender, EventArgs e)
    {
        txdriver_name.Text = bll.vLookUp("select emp_nm from tmst_employee where emp_cd='" + cbemp_cd.SelectedValue + "'");
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        DataTable dt = new DataTable();
        arr.Clear();
        try
        {
            if (Convert.ToString(txdono.Text) == "" || Convert.ToString(txdono.Text) == null)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please select DO','Please select DO','warning');", true);
                return;
            }

            arr.Add(new cArrayList("@do_no", Convert.ToString(txdono.Text)));
            arr.Add(new cArrayList("@do_sta_id", "D"));
            arr.Add(new cArrayList("@delivery_typ", Convert.ToString(cbdeliverytype.SelectedValue)));
            if (cbdeliverytype.SelectedValue == "OWN")
            {

                arr.Add(new cArrayList("@comp_cd", Convert.ToString(cbemp_cd.SelectedValue)));
                arr.Add(new cArrayList("@driver_name", null));
                bll.vUpdateDOHO(arr);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Data saved successfully','Data saved successfully','success');", true);
                grd.DataSource = null;
                grd.DataBind();
            }
            else
            {
                //arr.Clear();
                if (Convert.ToString(txdriver_name.Text) == "" || Convert.ToString(txdriver_name.Text) == null)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Enter driver name','Enter driver name','warning');", true);
                    return;
                }
                arr.Add(new cArrayList("@comp_cd", null));
                arr.Add(new cArrayList("@driver_name", Convert.ToString(txdriver_name.Text)));
                bll.vUpdateDOHO(arr);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Data saved successfully','Data saved successfully','success');", true);
                grd.DataSource = null;
                grd.DataBind();
            }
            BindGriddo();
        }
        catch (Exception ex) {
            throw ;
        }
    }
}