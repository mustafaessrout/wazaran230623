using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_hrdpa : System.Web.UI.Page
{
    cbll bll = new cbll();
    cdal cdl = new cdal();
    Utitlity ut = new Utitlity();
    double dTotWeight = 0; double dTotResult = 0;
    //sp_driverIncetiveDetails
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindControl();
            ddlRecommendation_SelectedIndexChanged(sender, e);
            cbemployee_SelectedIndexChanged(sender, e);
            DataTable dtStoreCalculation = new DataTable();
            dtStoreCalculation.Columns.Add("period", typeof(string));
            dtStoreCalculation.Columns.Add("job_title_cd", typeof(string));
            dtStoreCalculation.Columns.Add("level_cd", typeof(string));
            dtStoreCalculation.Columns.Add("emp_cd", typeof(string));
            dtStoreCalculation.Columns.Add("ids", typeof(string));
            dtStoreCalculation.Columns.Add("weight_kpi", typeof(decimal));
            dtStoreCalculation.Columns.Add("keyresp_nm", typeof(string));
            dtStoreCalculation.Columns.Add("objective", typeof(string));
            dtStoreCalculation.Columns.Add("kpi", typeof(string));
            dtStoreCalculation.Columns.Add("TargetValue", typeof(decimal));
            dtStoreCalculation.Columns.Add("achievement", typeof(decimal));
            dtStoreCalculation.Columns.Add("achievementPercentage", typeof(decimal));
            dtStoreCalculation.Columns.Add("kpi_Recommendation_cd", typeof(string));
            dtStoreCalculation.Columns.Add("kpi_EvaluationPurposeOther_cd", typeof(string));

            Session["dtStoreCalculation"] = dtStoreCalculation;
        }
    }

    private void BindControl()
    {
        bll.vBindingFieldValueToCombo(ref ddlRecommendation, "kpi_Recommendation_cd");
        bll.vBindingFieldValueToCombo(ref ddlEvaluationPurpose, "kpi_EvaluationPurposeOther_cd");
        bll.vBindingComboToSp(ref cbperiod, "sp_tmst_period_get", "period_cd", "period_nm");
        cbperiod.SelectedValue = bll.sGetControlParameter("period");
        bll.vBindingFieldValueToCombo(ref cbjobtitle, "job_title_cd");
        bll.vBindingFieldValueToCombo(ref cblevel, "level_cd");

    }
    protected void cblevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
        arr.Add(new cArrayList("@level_cd", cblevel.SelectedValue.ToString()));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        bll.vBindingComboToSp(ref cbemployee, "sp_tmst_employee_getbyjobtitle", "emp_cd", "emp_desc", arr);
        cbemployee_SelectedIndexChanged(sender, e);
    }
    protected void cbjobtitle_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
        arr.Add(new cArrayList("@level_cd", cblevel.SelectedValue.ToString()));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        bll.vBindingComboToSp(ref cbemployee, "sp_tmst_employee_getbyjobtitle", "emp_cd", "emp_desc", arr);
        cbemployee_SelectedIndexChanged(sender, e);
    }
    protected void btcalculation_Click(object sender, EventArgs e)
    {
        Session["dtStoreCalculation"] = null;
        DataTable dtStoreCalculation = new DataTable();
        dtStoreCalculation.Columns.Add("period", typeof(string));
        dtStoreCalculation.Columns.Add("job_title_cd", typeof(string));
        dtStoreCalculation.Columns.Add("level_cd", typeof(string));
        dtStoreCalculation.Columns.Add("emp_cd", typeof(string));
        dtStoreCalculation.Columns.Add("ids", typeof(string));
        dtStoreCalculation.Columns.Add("weight_kpi", typeof(decimal));
        dtStoreCalculation.Columns.Add("keyresp_nm", typeof(string));
        dtStoreCalculation.Columns.Add("objective", typeof(string));
        dtStoreCalculation.Columns.Add("kpi", typeof(string));
        dtStoreCalculation.Columns.Add("TargetValue", typeof(decimal));
        dtStoreCalculation.Columns.Add("achievement", typeof(decimal));
        dtStoreCalculation.Columns.Add("achievementPercentage", typeof(decimal));
        dtStoreCalculation.Columns.Add("kpi_Recommendation_cd", typeof(string));
        dtStoreCalculation.Columns.Add("kpi_EvaluationPurposeOther_cd", typeof(string));

        Session["dtStoreCalculation"] = dtStoreCalculation;

        List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
        //arr.Add(new cArrayList("@level_cd", cblevel.SelectedValue.ToString()));
        arr.Add(new cArrayList("@period", cbperiod.SelectedValue.ToString()));
        arr.Add(new cArrayList("@emp_cd", cbemployee.SelectedValue.ToString()));
        bll.vBatchResultKPI(arr);
        vInitGrid();

        CalculateKPI();
    }

    void vInitGrid()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@period", cbperiod.SelectedValue.ToString()));
        arr.Add(new cArrayList("@emp_cd", cbemployee.SelectedValue.ToString()));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        DataTable dt = new DataTable();
        dt = cdl.GetValueFromSP("sp_trpt_hrd_kpiresultdtl_get", arr);
        if (dt.Rows.Count > 0)
        {
            grd.DataSource = dt;
            grd.DataBind();
            txtDevelopmentProgram.Text = Convert.ToString(dt.Rows[0]["DevelopmentProgram"]);
            txtEvaluationPurpose.Text = Convert.ToString(dt.Rows[0]["EvaluationPurpose"]);
            txtRecommendation.Text = Convert.ToString(dt.Rows[0]["Recommendation"]);
            txtStrengths.Text = Convert.ToString(dt.Rows[0]["commentsStrengths"]);
            txtWeakness.Text = Convert.ToString(dt.Rows[0]["commentsWeakness"]);
            txtRemarks.Text = Convert.ToString(dt.Rows[0]["remarks"]);
        }
        else
        {
            grd.DataSource = null;
            grd.DataBind();
        }
        //bll.vBindingGridToSp(ref grd, "sp_trpt_hrd_kpiresultdtl_get", arr);
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        // we calculate  Tootal Score
        decimal totlaBF = 0;
        decimal totlaDP = 0;
        DataTable dtKPI_BF = new DataTable();
        DataTable dtKPI_DP = new DataTable();
        List<cArrayList> drKPI_BF = new List<cArrayList>();
        drKPI_BF.Add(new cArrayList("@emp_cd", Convert.ToString(cbemployee.SelectedValue)));
        drKPI_BF.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        drKPI_BF.Add(new cArrayList("@totlaBF", ""));
        drKPI_BF.Add(new cArrayList("@totlaDP", ""));
        drKPI_BF.Add(new cArrayList("@period", Convert.ToString(cbperiod.SelectedValue)));
        dtKPI_BF = cdl.GetValueFromSP("sp_kpi_BF_get", drKPI_BF);
        dtKPI_DP = cdl.GetValueFromSP("sp_kpi_DP_get", drKPI_BF);

        if (dtKPI_BF.Rows.Count > 0)
        {
            decimal BFTarget = 0;
            decimal BFAchievement = 0;
            decimal achievementValue = 0;
            decimal sumPercentage = 0;
            decimal weight_kpi = 0;
            foreach (DataRow dr in dtKPI_BF.Rows)
            {
                BFTarget = Convert.ToDecimal(dr["targetValue"]);
                BFAchievement = Convert.ToDecimal(dr["AchievementValue"]);

                achievementValue = (BFAchievement * 100) / BFTarget;
                weight_kpi += Convert.ToDecimal(dr["weight_kpi"]);
                sumPercentage += achievementValue;
            }

            totlaBF = ((sumPercentage / 3) * weight_kpi) / 100;
        }
        if (dtKPI_DP.Rows.Count > 0)
        {
            decimal BFTarget = 0;
            decimal BFAchievement = 0;
            decimal achievementValue = 0;
            decimal sumPercentage = 0;
            decimal weight_kpi = 0;
            foreach (DataRow dr in dtKPI_DP.Rows)
            {
                BFTarget = Convert.ToDecimal(dr["targetValue"]);
                BFAchievement = Convert.ToDecimal(dr["AchievementValue"]);

                achievementValue = (BFAchievement * 100) / BFTarget;
                weight_kpi += Convert.ToDecimal(dr["weight_kpi"]);
                sumPercentage += achievementValue;
            }

            totlaDP = ((sumPercentage / 2) * weight_kpi) / 100;
        }

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=pa&emp="
            + cbemployee.SelectedValue.ToString() + "&period=" + cbperiod.SelectedValue.ToString() + "&totlaBF=" + String.Format("{0:0.00}", totlaBF) + "&totlaDP=" + String.Format("{0:0.00}", totlaDP) + "');", true);
    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grd.EditIndex = e.NewEditIndex;
        vInitGrid();
    }
    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grd.EditIndex = -1;
        vInitGrid();
    }

    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Label lblobjective = (Label)grd.Rows[e.RowIndex].FindControl("lblobjective");
        Label lbtarget = (Label)grd.Rows[e.RowIndex].FindControl("lbtarget");
        TextBox txachievement = (TextBox)grd.Rows[e.RowIndex].FindControl("txachievement");
        Label lblkeyresp_nm = (Label)grd.Rows[e.RowIndex].FindControl("lblkeyresp_nm");
        HiddenField hdids = (HiddenField)grd.Rows[e.RowIndex].FindControl("hdids");
        Label lblobjectiveDP1 = (Label)grd.Rows[3].FindControl("lblobjective");
        Label txachievementDP1 = (Label)grd.Rows[3].FindControl("lbachievement");
        Label lbtargetDP1 = (Label)grd.Rows[3].FindControl("lbtarget");
        Label lblkeyresp_nmDP1 = (Label)grd.Rows[3].FindControl("lblkeyresp_nm");
        HiddenField hdidsDP1 = (HiddenField)grd.Rows[3].FindControl("hdids");


        Label lblobjectiveDP2 = (Label)grd.Rows[4].FindControl("lblobjective");
        Label txachievementDP2 = (Label)grd.Rows[4].FindControl("lbachievement");
        Label lbtargetDP2 = (Label)grd.Rows[4].FindControl("lbtarget");
        Label lblkeyresp_nmDP2 = (Label)grd.Rows[4].FindControl("lblkeyresp_nm");
        HiddenField hdidsDP2 = (HiddenField)grd.Rows[4].FindControl("hdids");


        if (lblkeyresp_nm.Text == "DELIVERY PRODUCT")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('You can not edit delivery product!','DELIVERY PRODUCT','warning');", true);
            return;
        }
        else
        {

            List<cArrayList> arr = new List<cArrayList>();
            if (Convert.ToDecimal(txachievement.Text) > Convert.ToDecimal(lbtarget.Text))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('" + lblobjective.Text + "  Achievement is greater then traget !','Achievement Wrong','warning');", true);
                return;
            }
            else
            {
                arr.Add(new cArrayList("@ids", hdids.Value.ToString()));
                arr.Add(new cArrayList("@achievement", txachievement.Text));
                arr.Add(new cArrayList("@emp_cd", cbemployee.SelectedValue.ToString()));
                arr.Add(new cArrayList("@period", cbperiod.SelectedValue.ToString()));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
                bll.vUpdateRptHRDKPIResult(arr);
            }

            arr.Clear();
            arr.Add(new cArrayList("@ids", hdidsDP1.Value.ToString()));
            arr.Add(new cArrayList("@achievement", txachievementDP1.Text));
            arr.Add(new cArrayList("@emp_cd", cbemployee.SelectedValue.ToString()));
            arr.Add(new cArrayList("@period", cbperiod.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
            bll.vUpdateRptHRDKPIResult(arr);

            arr.Clear();
            arr.Add(new cArrayList("@ids", hdidsDP2.Value.ToString()));
            arr.Add(new cArrayList("@achievement", txachievementDP2.Text));
            arr.Add(new cArrayList("@emp_cd", cbemployee.SelectedValue.ToString()));
            arr.Add(new cArrayList("@period", cbperiod.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
            bll.vUpdateRptHRDKPIResult(arr);

        }
        grd.EditIndex = -1;
        vInitGrid();
    }
    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblids = (Label)e.Row.FindControl("lblids");
                Label lbresult = (Label)e.Row.FindControl("lbresult");
                Label lbachievement = (Label)e.Row.FindControl("lbachievement");
                Label lblobjective = (Label)e.Row.FindControl("lblobjective");
                Label lbtarget = (Label)e.Row.FindControl("lbtarget");
                Label lblkeyresp_nm = (Label)e.Row.FindControl("lblkeyresp_nm");
                Label lblWeight = (Label)e.Row.FindControl("lblWeight");
                HiddenField hdids = (HiddenField)e.Row.FindControl("ids");

                var sum = string.Empty;
                if (lbtarget != null && lblids != null)
                {
                    sum = bll.vLookUp("select sum(qty) from thrd_kpitargetdtl where ids='" + lblids.Text.ToString() + "' and emp_cd='" + cbemployee.SelectedValue.ToString() + "' and period='" + cbperiod.SelectedValue.ToString() + "'");
                    if (lblobjective.Text == "Fulfillment of delivery product with agreed quantity ")
                    //if (lblobjective.Text == "Fulfillment of delivery")
                    {
                        decimal tot = 0;
                        decimal contract = 0;
                        decimal invoice = 0;

                        //var tempData_Invoice = bll.vLookUp("select dbo.fn_totalInvoiceAchive_invoice('" + Convert.ToString(cbemployee.SelectedValue) + "','" + Convert.ToString(cbperiod.SelectedValue) + "')");
                        //var tempData_contract = bll.vLookUp("select dbo.fn_totalInvoiceAchive_contract('" + Convert.ToString(cbemployee.SelectedValue) + "','" + Convert.ToString(cbperiod.SelectedValue) + "')");
                        List<cArrayList> arrInvoice = new List<cArrayList>();
                        DataTable dtInvoice = new DataTable();
                        arrInvoice.Add(new cArrayList("@driver_cd", Convert.ToString(cbemployee.SelectedValue)));
                        arrInvoice.Add(new cArrayList("@period_cd", cbperiod.SelectedValue));
                        dtInvoice = cdl.GetValueFromSP("sp_driverIncetiveDetails", arrInvoice);
                        if (dtInvoice.Rows.Count > 0)
                        {
                            invoice = Convert.ToDecimal(dtInvoice.Compute("Sum(qty)", string.Empty));
                        }

                        //List<cArrayList> arrContract = new List<cArrayList>();
                        //DataTable dtContract = new DataTable();
                        //arrContract.Add(new cArrayList("@driver_cd", Convert.ToString(cbemployee.SelectedValue)));
                        //arrContract.Add(new cArrayList("@period_cd", cbperiod.SelectedValue));
                        //dtContract = cdl.GetValueFromSP("sp_driverIncetiveDetails", arrContract);
                        //if (dtContract.Rows.Count > 0)
                        //{
                        //    contract = Convert.ToDecimal(dtContract.Compute("Sum(qty)", string.Empty));
                        //}

                        if (lbachievement == null && lblkeyresp_nm.Text == "DELIVERY PRODUCT")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('You can not edit delivery product!','DELIVERY PRODUCT','warning');", true);
                        }
                        else
                        {
                            if (contract == 0 && invoice == 0)
                            {
                                lbachievement.Text = Convert.ToString("0.00");
                            }
                            else {
                                //invoice = tempData_Invoice != "" ? Convert.ToDecimal(tempData_Invoice) : 0;
                                //contract = tempData_contract != "" ? Convert.ToDecimal(tempData_contract) : 0;
                                tot = contract + invoice;
                                lbachievement.Text = Convert.ToString(tot);
                            }
                            hdfCartoonAchievement.Value = Convert.ToString(tot);
                        }
                    }
                    else if (lblobjective.Text == "Customer Coverage")
                    {
                        decimal tot = 0;
                        decimal contract = 0;
                        decimal invoice = 0;
                        var tempData_Invoice = bll.vLookUp("select dbo.fn_totalCustomerAchiveDuplicateCustSameDate('" + Convert.ToString(cbemployee.SelectedValue) + "','" + Convert.ToString(cbperiod.SelectedValue) + "')");
                        var tempData_Contract = bll.vLookUp("select dbo.fn_totalCustomerAchiveDuplicateCustSameDate_Contract('" + Convert.ToString(cbemployee.SelectedValue) + "','" + Convert.ToString(cbperiod.SelectedValue) + "')");
                        if (lbachievement == null && lblkeyresp_nm.Text == "DELIVERY PRODUCT")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('You can not edit delivery product!','DELIVERY PRODUCT','warning');", true);
                        }
                        else
                        {
                            if (tempData_Invoice == "" && tempData_Contract=="")
                            {
                                lbachievement.Text = Convert.ToString("0.00");
                            }
                            else {
                                invoice = tempData_Invoice != "" ? Convert.ToDecimal(tempData_Invoice) : 0;
                                contract = tempData_Contract != "" ? Convert.ToDecimal(tempData_Contract) : 0;
                                tot = contract + invoice; 
                                lbachievement.Text = Convert.ToString(tot);
                            }
                            hdfCustomerAchievement.Value = Convert.ToString(tot);
                        }
                    }
                }

                if (sum != "")
                {
                    lbtarget.Text = Convert.ToString(sum);
                }
                else if (lbtarget != null && lblids != null)
                {
                    //sum = bll.vLookUp("select sum(qty) from trpt_hrd_kpiresultdtl where ids='" + lblids.Text + "' and emp_cd='" + cbemployee.SelectedValue + "'");
                }
                if (lbachievement != null && lbtarget.Text != null)
                {
                    if (lbachievement.Text != "" && lbtarget.Text != "")
                    {
                        double dAchivement = Convert.ToDouble(lbachievement.Text);
                        double dTarget = Convert.ToDouble(lbtarget.Text);
                        double dWeight = Convert.ToDouble(lblWeight.Text);

                        //double dResult = (dAchivement * 100) / dTarget;//(dAchivement * 100) / dTarget;
                        double dResult = (dTarget == 0 ? 0 : ((dAchivement * dWeight) / dTarget));
                        if (dResult >= dWeight)
                        {
                            dResult = dWeight;
                        }
                        lbresult.Text = String.Format("{0:0.00}", dResult) + " %"; // dResult.ToString("0.00") + " %";
                        dTotResult += dResult;
                        dTotWeight += dWeight;
                    }
                }
                if (lbtarget != null && lblids != null)
                {
                    DataTable dt = new DataTable();
                    dt = (DataTable)Session["dtStoreCalculation"];

                    if (dt == null)
                    {
                        DataRow dr = dt.NewRow();
                        HiddenField hdfweight_kpi = (HiddenField)e.Row.FindControl("hdfweight_kpi");

                        Label lblkpi = (Label)e.Row.FindControl("lblkpi");
                        dr["period"] = Convert.ToString(cbperiod.SelectedValue);
                        dr["job_title_cd"] = Convert.ToString(cbjobtitle.SelectedValue);
                        dr["level_cd"] = Convert.ToString(cblevel.SelectedValue);
                        dr["emp_cd"] = Convert.ToString(cbemployee.SelectedValue);
                        dr["ids"] = Convert.ToString(lblids.Text);
                        dr["weight_kpi"] = Convert.ToDecimal(hdfweight_kpi.Value);
                        dr["keyresp_nm"] = Convert.ToString(lblkeyresp_nm.Text);
                        dr["objective"] = Convert.ToString(lblobjective.Text);
                        dr["kpi"] = Convert.ToString(lblkpi.Text);
                        dr["TargetValue"] = Convert.ToDecimal(lbtarget.Text);
                        if (lbachievement.Text == "")
                        {
                            dr["achievement"] = 0;
                        }
                        else if (lbachievement.Text != "") { dr["achievement"] = Convert.ToDecimal(lbachievement.Text); }
                        if (lbresult.Text == "")
                        {
                            dr["achievementPercentage"] = 0;
                        }
                        else if (lbresult.Text != "") { dr["achievementPercentage"] = Convert.ToDecimal(lbresult.Text.Split('%')[0].Trim()); }
                        dr["kpi_Recommendation_cd"] = Convert.ToString(ddlRecommendation.SelectedValue);
                        dr["kpi_EvaluationPurposeOther_cd"] = Convert.ToString(ddlEvaluationPurpose.SelectedValue);

                        dt.Rows.Add(dr);

                        Session["dtStoreCalculation"] = dt;
                    }
                    else
                    {
                        var results = from myRow in dt.AsEnumerable()
                                      where myRow.Field<string>("ids") == Convert.ToString(lblids.Text)
                                      select myRow;

                        if (results.ToList().Count() == 0)
                        {
                            DataRow dr = dt.NewRow();
                            HiddenField hdfweight_kpi = (HiddenField)e.Row.FindControl("hdfweight_kpi");
                            Label lblkpi = (Label)e.Row.FindControl("lblkpi");
                            dr["period"] = Convert.ToString(cbperiod.SelectedValue);
                            dr["job_title_cd"] = Convert.ToString(cbjobtitle.SelectedValue);
                            dr["level_cd"] = Convert.ToString(cblevel.SelectedValue);
                            dr["emp_cd"] = Convert.ToString(cbemployee.SelectedValue);
                            dr["ids"] = Convert.ToString(lblids.Text);
                            dr["weight_kpi"] = Convert.ToDecimal(hdfweight_kpi.Value);
                            dr["keyresp_nm"] = Convert.ToString(lblkeyresp_nm.Text);
                            dr["objective"] = Convert.ToString(lblobjective.Text);
                            dr["kpi"] = Convert.ToString(lblkpi.Text);
                            if (lbtarget.Text == "")
                            {
                                dr["TargetValue"] = 0;
                            }
                            else
                            {
                                dr["TargetValue"] = Convert.ToDecimal(lbtarget.Text);
                            }
                            if (lbachievement != null)
                            {
                                if (lbachievement.Text == "")
                                {
                                    dr["achievement"] = 0;
                                }
                            }
                            else if (lbachievement.Text != "") { dr["achievement"] = Convert.ToDecimal(lbachievement.Text); }
                            if (lbresult.Text == "")
                            {
                                dr["achievementPercentage"] = 0;
                            }
                            else if (lbresult.Text != "") { dr["achievementPercentage"] = Convert.ToDecimal(lbresult.Text.Split('%')[0].Trim()); }
                            dr["kpi_Recommendation_cd"] = Convert.ToString(ddlRecommendation.SelectedValue);
                            dr["kpi_EvaluationPurposeOther_cd"] = Convert.ToString(ddlEvaluationPurpose.SelectedValue);

                            dt.Rows.Add(dr);

                            Session["dtStoreCalculation"] = dt;
                        }

                    }
                }
            }
            else if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate))
            {
                HiddenField hdids = (HiddenField)e.Row.FindControl("ids");
                Label lbresult = (Label)e.Row.FindControl("lbresult");
                Label lbachievement = (Label)e.Row.FindControl("lbachievement");
                Label lblWeight = (Label)e.Row.FindControl("lblWeight");
                Label lbtarget = (Label)e.Row.FindControl("lbtarget");
                if (lbachievement.Text == "")
                {
                    lbachievement.Text = "0";
                }
                if (lbtarget != null && hdids != null)
                {
                    lbtarget.Text = bll.vLookUp("select sum(qty) from thrd_kpitargetdtl where ids='" + hdids.Value.ToString() + "' and emp_cd='" + cbemployee.SelectedValue.ToString() + "' and period='" + cbperiod.SelectedValue.ToString() + "'");
                }
                double dAchivement = Convert.ToDouble(lbachievement.Text);
                double dTarget = Convert.ToDouble(lbtarget.Text);
                double dWeight = Convert.ToDouble(lblWeight.Text);
                //double dResult = (dAchivement / dTarget) * 100;
                //lbresult.Text = dResult.ToString() + " %";


                //double dResult = (dAchivement * dWeight) / dTarget;
                double dResult = (dTarget == 0 ? 0 : ((dAchivement * dWeight) / dTarget));
                if (dResult >= dWeight)
                {
                    dResult = dWeight;
                }
                lbresult.Text = String.Format("{0:0.00}", dResult) + " %"; // dResult.ToString("0.00") + " %";
                dTotResult += dResult;
                dTotWeight += dWeight;
            }
            else if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
            {
                Label lbresult = (Label)e.Row.FindControl("lbresult");
                TextBox txachievement = (TextBox)e.Row.FindControl("txachievement");
                Label lbtarget = (Label)e.Row.FindControl("lbtarget");
                Label lblWeight = (Label)e.Row.FindControl("lblWeight");
                if (txachievement.Text == "")
                {
                    txachievement.Text = "0";
                }
                double dAchivement = Convert.ToDouble(txachievement.Text);
                double dTarget = Convert.ToDouble(lbtarget.Text);
                double dWeight = Convert.ToDouble(lblWeight.Text);
                //double dResult = (dAchivement / dTarget) * 100;
                //lbresult.Text = dResult.ToString() + " %";


                //double dResult = (dAchivement * dWeight) / dTarget;
                double dResult = (dTarget == 0 ? 0 : ((dAchivement * dWeight) / dTarget));
                if (dResult >= dWeight) {
                    dResult = dWeight;
                }
                lbresult.Text = String.Format("{0:0.00}", dResult) + " %"; // dResult.ToString("0.00") + " %";
                dTotResult += dResult;
                dTotWeight += dWeight;
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbtotWeight = (Label)e.Row.FindControl("lbtotWeight");
                Label lbtotResult = (Label)e.Row.FindControl("lbtotResult");
                lbtotWeight.Text = String.Format("{0:0.00}", dTotWeight) + " %";
                lbtotResult.Text = String.Format("{0:0.00}", dTotResult) + " %";

            }
            //for (int i = 0; i < grd.Rows.Count; i++)
            //{
            //    var a = grd.Rows[i].Cells[0].Text;
            //    var b = grd.Rows[i].Cells[1].Text;
            //    var c = grd.Rows[i].Cells[2].Text;
            //    var d = grd.Rows[i].Cells[3].Text;
            //    var ee = grd.Rows[i].Cells[4].Text;
            //    var f = grd.Rows[i].Cells[5].Text;
            //    var g = grd.Rows[i].Cells[6].Text;
            //}
        }
        catch (Exception ex)
        {
            ut.Logs("", "Driver Incentive", "KPI HRD", "fm_HRDPA", "grd_RowDataBound", "Exception", ex.Message + ex.InnerException);
            throw;
        }
    }

    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlRecommendation.SelectedValue.ToString() == "OTHERS" && txtRecommendation.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please enter other recommendation','other recommendation','warning');", true);
                return;
            }
            else if (txtStrengths.Text == "" || txtStrengths.Text == null)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please enter strengths','Strengths','warning');", true);
                return;
            }
            else if (txtWeakness.Text == "" || txtWeakness.Text == null)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please enter weakness','weakness','warning');", true);
                return;
            }
            else if (txtDevelopmentProgram.Text == "" || txtDevelopmentProgram.Text == null)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please enter development program suggestion','Development Program','warning');", true);
                return;
            }
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@emp_cd", cbemployee.SelectedValue.ToString()));
            arr.Add(new cArrayList("@period", Convert.ToString(cbperiod.SelectedValue)));
            arr.Add(new cArrayList("@commentsStrengths", txtStrengths.Text));
            arr.Add(new cArrayList("@commentsWeakness", txtWeakness.Text));
            arr.Add(new cArrayList("@remarks", txtRemarks.Text));
            arr.Add(new cArrayList("@DevelopmentProgram", txtDevelopmentProgram.Text));
            arr.Add(new cArrayList("@Recommendation", ddlRecommendation.SelectedValue));
            arr.Add(new cArrayList("@EvaluationPurpose", ddlEvaluationPurpose.SelectedValue));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
            if (ddlRecommendation.SelectedValue.ToString() == "OTHERS")
            {
                arr.Add(new cArrayList("@RecommendationOther", txtRecommendation.Text));
            }
            else { arr.Add(new cArrayList("@RecommendationOther", string.Empty)); }

            if (ddlEvaluationPurpose.SelectedValue.ToString() == "Other")
            {
                arr.Add(new cArrayList("@EvaluationPurposeOther", txtRecommendation.Text));
            }
            else { arr.Add(new cArrayList("@EvaluationPurposeOther", string.Empty)); }


            arr.Add(new cArrayList("@customerAchievement", hdfCustomerAchievement.Value == "" ? 0 : Convert.ToDecimal(hdfCustomerAchievement.Value)));
            arr.Add(new cArrayList("@cartoonAchievement", hdfCartoonAchievement.Value == "" ? 0 : Convert.ToDecimal(hdfCartoonAchievement.Value)));
            bll.vInsertOtherRecommendation(arr);
            btcalculation_Click(sender, e);
            //BindControl();
            dTotResult = 0;
            dTotWeight = 0;
            vInitGrid();
            txtDevelopmentProgram.Text = string.Empty;
            txtEvaluationPurpose.Text = string.Empty;
            txtRecommendation.Text = string.Empty;
            txtStrengths.Text = string.Empty;
            txtWeakness.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Data has been saved successfully','Successfully','success');", true);
        }
        catch (Exception ex)
        {
            ut.Logs("", "Driver Incentive", "KPI HRD", "fm_HRDPA", "btnsave_Click", "Exception", ex.Message + ex.InnerException);
            throw;
        }
    }
    protected void ddlRecommendation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRecommendation.SelectedValue.ToString() == "OTHERS")
        {
            txtRecommendation.Visible = true;
        }
        else { txtRecommendation.Visible = false; }
    }
    protected void ddlEvaluationPurpose_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEvaluationPurpose.SelectedValue.ToString() == "Other")
        {
            txtEvaluationPurpose.Visible = true;
        }
        else { txtEvaluationPurpose.Visible = false; }
    }
    protected void btnIncentive_Click(object sender, EventArgs e)
    {
        // we calculate  Tootal Score
        decimal totlaBF = 0;
        decimal totlaDP = 0;

        DataTable dtKPI_BF = new DataTable();
        DataTable dtKPI_DP = new DataTable();
        List<cArrayList> drKPI_BF = new List<cArrayList>();
        drKPI_BF.Add(new cArrayList("@emp_cd", Convert.ToString(cbemployee.SelectedValue)));
        drKPI_BF.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        drKPI_BF.Add(new cArrayList("@totlaBF", ""));
        drKPI_BF.Add(new cArrayList("@totlaDP", ""));
        drKPI_BF.Add(new cArrayList("@period", Convert.ToString(cbperiod.SelectedValue)));
        dtKPI_BF = cdl.GetValueFromSP("sp_kpi_Incentive_get", drKPI_BF);

        if (dtKPI_BF.Rows.Count > 0)
        {
            decimal BFTarget = 0;
            decimal BFAchievement = 0;
            decimal achievementValue = 0;
            decimal sumPercentage = 0;
            decimal weight_kpi = 0;
            foreach (DataRow dr in dtKPI_BF.Rows)
            {
                BFTarget = Convert.ToDecimal(dr["targetValue"]);
                BFAchievement = Convert.ToDecimal(dr["AchievementValue"]);

                if (BFTarget == 0) { achievementValue = 0; }
                else { achievementValue = (BFAchievement * 100) / BFTarget; }

                weight_kpi += Convert.ToDecimal(dr["weight_kpi"]);
                sumPercentage += achievementValue;
            }

            totlaBF = (sumPercentage / 5);
        }

        //if (lblNationalit.Text == "Non Saudi")
        //{
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=IncentiveNew&emp="
            + cbemployee.SelectedValue.ToString() + "&period=" + cbperiod.SelectedValue.ToString() + "&totlaBF=" + String.Format("{0:0.00}", totlaBF) + "&totlaDP=" + String.Format("{0:0.00}", totlaDP) + "');", true);
        //}
        //else 
        //    if (lblNationalit.Text == "Saudi")
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=IncentiveNewSaudi&emp="
        //        + cbemployee.SelectedValue.ToString() + "&period=" + cbperiod.SelectedValue.ToString() + "&totlaBF=" + String.Format("{0:0.00}", totlaBF) + "&totlaDP=" + String.Format("{0:0.00}", totlaDP) + "');", true);
        //}
    }
    protected void cbemployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        var nationality = bll.vLookUp("select nationality  from tmst_employee where emp_cd='" + cbemployee.SelectedValue.ToString() + "'");
        //if (Convert.ToString(nationality) == "SAUDI")
        //{
        //    lblNationalit.Text = "Saudi";
        //    btcalculation_Click(sender, e);
        //}
        //else { 
        lblNationalit.Text = "Non Saudi"; btcalculation_Click(sender, e);
        //}


    }

    void CalculateKPI()
    {
        if (cbemployee.SelectedValue != "" && cblevel.SelectedValue == "1" && cbjobtitle.SelectedValue == "5")
        {
            DataTable dt = new DataTable();
            string CalIncetiveID = string.Empty;
            decimal perctnageSum = 0;
            dt = (DataTable)Session["dtStoreCalculation"];

            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
            arr.Add(new cArrayList("@level_cd", cblevel.SelectedValue.ToString()));
            arr.Add(new cArrayList("@emp_cd", cbemployee.SelectedValue.ToString()));
            arr.Add(new cArrayList("@kpi_Recommendation_cd", ddlRecommendation.SelectedValue.ToString()));
            arr.Add(new cArrayList("@kpi_EvaluationPurposeOther_cd", ddlEvaluationPurpose.SelectedValue.ToString()));
            arr.Add(new cArrayList("@period", cbperiod.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespointcd", Convert.ToString(Request.Cookies["sp"].Value)));
            arr.Add(new cArrayList("@CreatedBy", Request.Cookies["usr_id"].Value));
            bll.vInsertCalculateIncetive(arr, ref CalIncetiveID);

            if (dt.Rows.Count > 0)
            {
                // here we creating history for transaction.
                foreach (DataRow dr in dt.Rows)
                {
                    arr.Clear();
                    arr.Add(new cArrayList("@CalIncetiveID", CalIncetiveID));
                    arr.Add(new cArrayList("@hrd_kpiresultdtlIds", Convert.ToString(dr["ids"])));
                    arr.Add(new cArrayList("@weight_kpi", Convert.ToDecimal(dr["weight_kpi"])));
                    arr.Add(new cArrayList("@keyresp_nm", Convert.ToString(dr["keyresp_nm"])));
                    arr.Add(new cArrayList("@objective", Convert.ToString(dr["objective"])));
                    arr.Add(new cArrayList("@kpi", Convert.ToString(dr["kpi"])));
                    arr.Add(new cArrayList("@TargetValue", Convert.ToDecimal(dr["TargetValue"])));
                    if (dr["achievement"].ToString() != "")
                    {
                        arr.Add(new cArrayList("@achievement", Convert.ToDecimal(dr["achievement"])));
                    }
                    else
                    {
                        //List<cArrayList> arr1 = new List<cArrayList>();
                        //arr1.Add(new cArrayList("@period", cbperiod.SelectedValue.ToString()));
                        //arr1.Add(new cArrayList("@emp_cd", cbemployee.SelectedValue.ToString()));
                        //arr1.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
                        //arr1.Add(new cArrayList("@ids", Request.Cookies["sp"].Value));
                        //DataTable dt1 = new DataTable();
                        //dt1 = cdl.GetValueFromSP("sp_trpt_hrd_kpiresultdtl_get", arr);


                        var achievement = bll.vLookUp("select d.achievement  from trpt_hrd_kpiresultdtl d  join trpt_hrd_kpiresult m on m.emp_cd=d.emp_cd and m.period=d.period where ids='" + Convert.ToString(dr["ids"]) + "' and m.period='" + Convert.ToString(cbperiod.SelectedValue) + "' and m.emp_cd='" + Convert.ToString(cbemployee.SelectedValue) + "' and d.salespointcd = '" + Convert.ToString(Request.Cookies["sp"].Value) + "'");
                        //bll.vLookUp("select d.achievement  from trpt_hrd_kpiresultdtl d  join trpt_hrd_kpiresult m on m.emp_cd=d.emp_cd and m.period=d.period where ids='" + Convert.ToString(dr["ids"]) + "' and m.period='" + Convert.ToString(cbperiod.SelectedValue) + "' and m.emp_cd='" + Convert.ToString(cbemployee.SelectedValue) + "' and d.salespointcd = '" + Convert.ToString(Request.Cookies["sp"].Value) + "'");
                        if (achievement == "")
                        {
                            arr.Add(new cArrayList("@achievement", 0));
                        }
                        else
                        {
                            arr.Add(new cArrayList("@achievement", Convert.ToDecimal(achievement)));
                        }
                    }
                    arr.Add(new cArrayList("@achievementPercentage", Convert.ToDecimal(dr["achievementPercentage"])));
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));




                    bll.vInsertCalculateDtlIncetive(arr);
                    //arr.Clear();
                    //arr.Add(new cArrayList("@ids", Convert.ToString(dr["ids"])));
                    //arr.Add(new cArrayList("@achievement", Convert.ToDecimal(dr["achievement"])));
                    //arr.Add(new cArrayList("@emp_cd", cbemployee.SelectedValue.ToString()));
                    //arr.Add(new cArrayList("@period", cbperiod.SelectedValue.ToString()));
                    //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
                    //bll.vUpdateRptHRDKPIResult(arr);

                }
                perctnageSum = Convert.ToDecimal(dt.Compute("Sum(achievementPercentage)", string.Empty));
            }

            //if (perctnageSum > 0)
            //{
            //    perctnageSum = perctnageSum / Convert.ToDecimal(dt.Rows.Count);
            //}
            arr.Clear();
            //if (lblNationalit.Text == "Saudi")
            //{
            //    arr.Add(new cArrayList("@nationalityType", "Saudi"));
            //}
            //else
            //{
            arr.Add(new cArrayList("@nationalityType", "NonSaudi"));
            //}
            DataTable dtRange = new DataTable();
            dtRange = cdl.GetValueFromSP("sp_tmst_IncetiveRange_get", arr);

            if (dtRange.Rows.Count > 0)
            {
                foreach (DataRow dr in dtRange.Rows)
                {
                    arr.Clear();
                    bool isSeleted = false;
                    decimal IncentiveAmount = 0;
                    if (perctnageSum >= Convert.ToDecimal(dr["minRange"]) && perctnageSum <= Convert.ToDecimal(dr["maxRange"]))
                    {
                        isSeleted = true;
                        IncentiveAmount = Convert.ToDecimal(dr["IncetiveAmount"]);
                    }
                    arr.Add(new cArrayList("@CalIncetiveID", CalIncetiveID));
                    arr.Add(new cArrayList("@IncetiveRangeID", Convert.ToString(dr["incetiveRangeID"])));
                    arr.Add(new cArrayList("@incetiveTypeID", Convert.ToString(dr["incetiveTypeID"])));
                    arr.Add(new cArrayList("@minRange", Convert.ToDecimal(dr["minRange"])));
                    arr.Add(new cArrayList("@maxRange", Convert.ToDecimal(dr["maxRange"])));
                    arr.Add(new cArrayList("@rangeAmount", Convert.ToDecimal(dr["IncetiveAmount"])));
                    arr.Add(new cArrayList("@nationalityType", Convert.ToString(dr["nationalityType"])));
                    arr.Add(new cArrayList("@IncentiveAmount", IncentiveAmount));
                    arr.Add(new cArrayList("@isSeleted", isSeleted));
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
                    bll.vInsertCalculateRangeDtl(arr);
                }
            }
        }
    }

    protected void btnValidationSheet_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=ValidationSheet');", true);
    }
    protected void btnPrintDriverHis_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=DriverHis&emp="
          + cbemployee.SelectedValue.ToString() + "&period=" + cbperiod.SelectedValue.ToString() + "');", true);
    }
    protected void btnPrintDriverHisAllDriver_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=DriverHisAllDriver&" +
    "period=" + cbperiod.SelectedValue.ToString() + "');", true);

    }
}