using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_salesmanvisit : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            lbperiod.Text = bll.sGetControlParameter("period");
            bll.vBindingFieldValueToCombo(ref cbvisit, "visit_typ");
            arr.Add(new cArrayList("@qry_cd", "SalesJob"));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingComboToSp(ref cbsalesman, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
            BindingGrid();
        }
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salesman_cd", cbsalesman.SelectedValue.ToString()));
        arr.Add(new cArrayList("@visit_typ", cbvisit.SelectedValue.ToString()));
        arr.Add(new cArrayList("@visit_dt", DateTime.ParseExact(dtvisit.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@period", lbperiod.Text));
        arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
        bll.vInsertSalesmanVisit(arr);
        BindingGrid();
    }

    void BindingGrid()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@period", lbperiod.Text));
        arr.Add(new cArrayList("@salesman_cd", cbsalesman.SelectedValue.ToString()));
        arr.Add(new cArrayList("@visit_typ", cbvisit.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tsalesman_visit_get", arr);
    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salesman_cd", cbsalesman.SelectedValue.ToString()));
        arr.Add(new cArrayList("@visit_typ", cbvisit.SelectedValue.ToString()));
        arr.Add(new cArrayList("@visit_dt", DateTime.ParseExact(dtvisit.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@period", lbperiod.Text));
        arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
        bll.vInsertSalesmanVisit(arr);
        BindingGrid();

    }
    protected void cbvisit_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindingGrid();
    }
    protected void cbsalesman_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindingGrid();
    }
    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "confirm('Do you want to delete ?);", true);
        HiddenField hdids = (HiddenField)grd.Rows[e.RowIndex].FindControl("hdids");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("IDS", hdids.Value.ToString()));
        bll.vDelSalesmanVisit(arr);
        BindingGrid();
    }
}