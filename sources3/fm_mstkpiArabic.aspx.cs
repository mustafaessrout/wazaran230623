using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_mstkpiArabic : System.Web.UI.Page
{
    cbll bll = new cbll();
    Utitlity ut = new Utitlity();
    cdal cdl = new cdal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingFieldValueToCombo(ref cbkr, "keyresp_cd");
            bll.vBindingFieldValueToCombo(ref cbsection, "section_cd");
            bll.vBindingFieldValueToCombo(ref cbjobtitle, "job_title_cd");
            bll.vBindingFieldValueToCombo(ref cblevel, "level_cd");
            cbkr_SelectedIndexChanged(sender, e);
            btprint.CssClass = "btn btn-success";
        }
    }
    protected void cbkr_SelectedIndexChanged(object sender, EventArgs e)
    {
        vInitGrid();
    }

    void vInitGrid()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@keyresp_cd", cbkr.SelectedValue.ToString()));
        arr.Add(new cArrayList("@section_cd", cbsection.SelectedValue.ToString()));
        arr.Add(new cArrayList("@level_cd", cblevel.SelectedValue.ToString()));
        arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));

        bll.vBindingGridToSp(ref grd, "sp_thrd_mst_kpi_getArabic", arr);
        if (grd.Rows.Count > 0)
        {
            //Label lbweightindividual = (Label)grd.FooterRow.FindControl("lbweightindividual");
            Label lbweightkpi = (Label)grd.FooterRow.FindControl("lbweightkpi");
           // lbweightindividual.Text = bll.vLookUp("select sum(weight_individual) from thrd_mst_kpi where level_cd='" + cblevel.SelectedValue.ToString() + "' and job_title_cd='" + cbjobtitle.SelectedValue.ToString() + "'");
            lbweightkpi.Text = bll.vLookUp("select sum(weight_kpi) from thrd_mst_kpi where level_cd='" + cblevel.SelectedValue.ToString() + "' and job_title_cd='" + cbjobtitle.SelectedValue.ToString() + "'");
        }
    }
    protected void cbsection_SelectedIndexChanged(object sender, EventArgs e)
    {
        vInitGrid();
    }
    protected void cbjobtitle_SelectedIndexChanged(object sender, EventArgs e)
    {
        vInitGrid();
    }
    protected void cblevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        vInitGrid();
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_mstkpi.aspx");
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        decimal weightKPI = 0;
        //foreach (GridViewRow row in grd.Rows)
        //{
        //    HiddenField hdfKPIWeight = (HiddenField)row.FindControl("hdfKPIWeight");
        //    if (hdfKPIWeight != null)
        //    {
        //        weightKPI += Convert.ToDecimal(hdfKPIWeight.Value);
        //    }
        //}
        DataTable dt = new DataTable();
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@keyresp_cd", cbkr.SelectedValue.ToString()));
        arr.Add(new cArrayList("@section_cd", cbsection.SelectedValue.ToString()));
        arr.Add(new cArrayList("@level_cd", cblevel.SelectedValue.ToString()));
        arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
        dt = cdl.GetValueFromSP("sp_thrd_mst_kpi_get", arr);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            weightKPI += Convert.ToDecimal(dt.Rows[i]["weight_kpi"]);
        }

        //if (weightKPI > 100 || weightKPI < 100)
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('KPI weight can not greater and less than 100 ','KPI weight','warning');", true);
        //    return;
        //}

        //else 
        if (txweightkpi.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please entry weight key responsible on value!','KPI Responsible','true');", true);
            return;
        }

        //if (txweightobj.Text == "")
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please entry weight kpi on value in KPI!','KPI Weight','true');", true);
        //    return;
        //}

        if (txkpi.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please entry KPI!','KPI','true');", true);
            return;
        }

        if (txobjective.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please entry Objective!','Objective','true');", true);
            return;
        }

        if (txpoor.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please entry Poor definition!','Poor','true');", true);
            return;
        }

        if (txfair.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please entry Fair definition!','Fair','true');", true);
            return;
        }
        if (txgood.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please entry Good definition!','Good','true');", true);
            return;
        }
        if (txverygood.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please entry Very Good definition!','Very good','true');", true);
            return;
        }

        arr.Clear();
        arr.Add(new cArrayList("@keyresp_cd", cbkr.SelectedValue.ToString()));
        arr.Add(new cArrayList("@section_cd", cbsection.SelectedValue.ToString()));
        arr.Add(new cArrayList("@level_cd", cblevel.SelectedValue.ToString()));
        arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
        arr.Add(new cArrayList("@weight_kpi", txweightkpi.Text));
        arr.Add(new cArrayList("@weight_individual", "0"));
        arr.Add(new cArrayList("@deleted", 0));
        arr.Add(new cArrayList("@objective", txobjective.Text));
        arr.Add(new cArrayList("@kpi", txkpi.Text));
        arr.Add(new cArrayList("@kpi_arabic", txkpiarabic.Text));
        arr.Add(new cArrayList("@objective_arabic", txobjarabic.Text));
        arr.Add(new cArrayList("@poor", txpoor.Text));
        arr.Add(new cArrayList("@poor_arabic", txpoorarabic.Text));
        arr.Add(new cArrayList("@fair", txfair.Text));
        arr.Add(new cArrayList("@fair_arabic", txfairarabic.Text));
        arr.Add(new cArrayList("@good", txgood.Text));
        arr.Add(new cArrayList("@good_arabic", txgoodarabic.Text));
        arr.Add(new cArrayList("@verygood", txverygood.Text));
        arr.Add(new cArrayList("@verygood_arabic", txverygoodarabic.Text));
        bll.vInsertMstKPI(arr);
        vInitGrid();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('New KPI has been saved successfully!','New KPI','true');", true);
        btsave.CssClass = "btn btn-default ro";
        txobjarabic.Text = ""; txobjective.Text = ""; txkpiarabic.Text = ""; txkpi.Text = ""; txweightkpi.Text = ""; txweightobj.Text = "";
        txobjective.CssClass = "form-control ro";
        txkpi.CssClass = "form-control ro";
        txweightobj.CssClass = "form-control ro";
        txweightkpi.CssClass = "form-control ro";
        txpoor.CssClass = "form-control ro";
        txpoorarabic.CssClass = "form-control ro";
        txfairarabic.CssClass = "form-control ro";
        txfair.CssClass = "form-control ro";
        txgoodarabic.CssClass = "form-control ro";
        txgood.CssClass = "form-control ro";
        txverygoodarabic.CssClass = "form-control ro";
        txverygood.CssClass = "form-control ro";
        txfair.Text = ""; txfairarabic.Text = "";
        txgood.Text = ""; txgoodarabic.Text = ""; txpoorarabic.Text = ""; txpoor.Text = ""; txgoodarabic.Text = ""; txgood.Text = "";
        txverygood.Text = ""; txverygoodarabic.Text = "";

    }
    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        HiddenField hdids = (HiddenField)grd.Rows[e.RowIndex].FindControl("hdids");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@ids", hdids.Value.ToString()));
        bll.vDeleteMstKPI(arr);
        vInitGrid();

    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grd.EditIndex = e.NewEditIndex;
        vInitGrid();
        DropDownList cbsection = (DropDownList)grd.Rows[e.NewEditIndex].FindControl("cbsection");
        bll.vBindingFieldValueToCombo(ref cbsection, "section_cd");
    }
    protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grd.EditIndex = -1;
        vInitGrid();
    }
    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        decimal totalWeightKPI = 0;
        decimal oldWeightKPI = 0;
        DataTable dt = new DataTable();
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@keyresp_cd", cbkr.SelectedValue.ToString()));
        arr.Add(new cArrayList("@section_cd", cbsection.SelectedValue.ToString()));
        arr.Add(new cArrayList("@level_cd", cblevel.SelectedValue.ToString()));
        arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
        dt = cdl.GetValueFromSP("sp_thrd_mst_kpi_get", arr);

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                totalWeightKPI += Convert.ToDecimal(dt.Rows[i]["weight_kpi"]);
            }
        }
       

        HiddenField hdids = (HiddenField)grd.Rows[e.RowIndex].FindControl("hdids");
        TextBox txweightkpi = (TextBox)grd.Rows[e.RowIndex].FindControl("txweightkpi");
        var results = from myRow in dt.AsEnumerable()
                      where Convert.ToString(myRow.Field<Guid>("IDS")) == Convert.ToString(hdids.Value.ToString())
                      select myRow;

        foreach (DataRow dr in results.ToList())
        {
            oldWeightKPI += Convert.ToDecimal(dr["weight_kpi"]);
        }

        totalWeightKPI = totalWeightKPI - oldWeightKPI + Convert.ToDecimal(txweightkpi.Text);

        //if (totalWeightKPI > 100 || totalWeightKPI < 100)
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('KPI weight can not greater and less than 100!','KPI weight','warning');", true);
        //    return;
        //}

        arr.Clear();
        
        TextBox txweightindividual = (TextBox)grd.Rows[e.RowIndex].FindControl("txweightindividual");
       
        arr.Add(new cArrayList("@ids", hdids.Value.ToString()));
        arr.Add(new cArrayList("@weight_kpi", txweightkpi.Text));
        arr.Add(new cArrayList("@weight_individual", "0"));
        bll.vUpdateHRDmstKPI(arr);
        grd.EditIndex = -1;
        vInitGrid();
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=kpi&lv=" + cblevel.SelectedValue.ToString() + "&jt=" + cbjobtitle.SelectedValue.ToString() + "');", true);
    }
}