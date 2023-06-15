using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_hrdpaMerchandiser : System.Web.UI.Page
{
    cbll bll = new cbll();
    cdal cdl = new cdal();
    Utitlity ut = new Utitlity();
    double dTotWeight = 0; double dTotResult = 0; double dTotQtyWeight = 0; double dTotQtyResult = 0; double dTotQalWeight = 0; double dTotQalResult = 0;
    double dTotQtyOtherWeight = 0; double dTotQtyOtherResult = 0; double dTotQalOtherWeight = 0; double dTotQalOtherResult = 0;
    //sp_driverIncetiveDetails
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindControl();
            ddlRecommendation_SelectedIndexChanged(sender, e);
            cbemployee_SelectedIndexChanged(sender, e);

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
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@period", cbperiod.SelectedValue));
        bll.vBindingComboToSp(ref cbemployee, "sp_tmst_employee_getActiveMerchandiser", "emp_cd", "emp_desc", arr);
        cbemployee_SelectedIndexChanged(sender, e);
    }
    protected void cbjobtitle_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
        arr.Add(new cArrayList("@level_cd", cblevel.SelectedValue.ToString()));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@period", cbperiod.SelectedValue));
        bll.vBindingComboToSp(ref cbemployee, "sp_tmst_employee_getActiveMerchandiser", "emp_cd", "emp_desc", arr);
        cbemployee_SelectedIndexChanged(sender, e);
    }


    void vInitGrid()
    {
        lblTotalDays.Text = string.Empty;
        //lblTotalFriday.Text = string.Empty;
        lblTotalAttendance.Text = string.Empty;
        lblPercentage.Text = string.Empty;

        List<cArrayList> arr = new List<cArrayList>();


        lblTotalDays.Text = "26";//bll.vLookUp("select dbo.fn_getDaysInMonth('" + cbperiod.SelectedValue + "')").ToString();
        //lblTotalFriday.Text = bll.vLookUp("select dbo.fn_getFriday('" + cbperiod.SelectedValue + "')").ToString();
        lblTotalAttendance.Text = bll.vLookUp("select dbo.fn_MerchandiserAttendanceMonth('" + cbemployee.SelectedValue + "','" + cbperiod.SelectedValue + "')").ToString();
        lblPercentage.Text = bll.vLookUp("select dbo.fn_MerchandiserAttendance('" + cbemployee.SelectedValue + "','" + cbperiod.SelectedValue + "')").ToString() + "%";

        arr.Clear();
        arr.Add(new cArrayList("@period", cbperiod.SelectedValue.ToString()));
        arr.Add(new cArrayList("@emp_cd", cbemployee.SelectedValue.ToString()));
        arr.Add(new cArrayList("@level_cd", cblevel.SelectedValue.ToString()));
        arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        DataTable dtGrd = new DataTable();
        dtGrd = cdl.GetValueFromSP("sp_trpt_hrd_kpiresultdtl_getMerch", arr);
        if (dtGrd.Rows.Count > 0)
        {
            grd.DataSource = dtGrd;
            grd.DataBind();
            txtDevelopmentProgram.Text = Convert.ToString(dtGrd.Rows[0]["DevelopmentProgram"]);
            txtEvaluationPurpose.Text = Convert.ToString(dtGrd.Rows[0]["EvaluationPurpose"]);
            txtRecommendation.Text = Convert.ToString(dtGrd.Rows[0]["Recommendation"]);
            txtStrengths.Text = Convert.ToString(dtGrd.Rows[0]["commentsStrengths"]);
            txtWeakness.Text = Convert.ToString(dtGrd.Rows[0]["commentsWeakness"]);
            txtRemarks.Text = Convert.ToString(dtGrd.Rows[0]["remarks"]);
        }
        else
        {
            grd.DataSource = null;
            grd.DataBind();
        }

        DataTable dtGrdOtehr = new DataTable();
        dtGrdOtehr = cdl.GetValueFromSP("sp_trpt_hrd_kpiresultdtl_other_getMerch", arr);
        if (dtGrdOtehr.Rows.Count > 0)
        {
            grdOther.DataSource = dtGrdOtehr;
            grdOther.DataBind();
        }
        else
        {
            grdOther.DataSource = null;
            grdOther.DataBind();
        }
        //bll.vBindingGridToSp(ref grd, "sp_trpt_hrd_kpiresultdtl_get", arr);
    }
    void vInitGridBHV()
    {
        lblTotalDays.Text = string.Empty;
        //lblTotalFriday.Text = string.Empty;
        lblTotalAttendance.Text = string.Empty;
        lblPercentage.Text = string.Empty;

        List<cArrayList> arr = new List<cArrayList>();


        lblTotalDays.Text = "26";//bll.vLookUp("select dbo.fn_getDaysInMonth('" + cbperiod.SelectedValue + "')").ToString();
        //lblTotalFriday.Text = bll.vLookUp("select dbo.fn_getFriday('" + cbperiod.SelectedValue + "')").ToString();
        lblTotalAttendance.Text = bll.vLookUp("select dbo.fn_MerchandiserAttendanceMonth('" + cbemployee.SelectedValue + "','" + cbperiod.SelectedValue + "')").ToString();
        lblPercentage.Text = bll.vLookUp("select dbo.fn_MerchandiserAttendance('" + cbemployee.SelectedValue + "','" + cbperiod.SelectedValue + "')").ToString() + "%";

        arr.Clear();
        arr.Add(new cArrayList("@period", cbperiod.SelectedValue.ToString()));
        arr.Add(new cArrayList("@emp_cd", cbemployee.SelectedValue.ToString()));
        arr.Add(new cArrayList("@level_cd", cblevel.SelectedValue.ToString()));
        arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        DataTable dtGrd = new DataTable();
        dtGrd = cdl.GetValueFromSP("sp_trpt_hrd_kpiresultdtl_getMerchBHV", arr);
        if (dtGrd.Rows.Count > 0)
        {
            grdBHV.DataSource = dtGrd;
            grdBHV.DataBind();
            txtDevelopmentProgram.Text = Convert.ToString(dtGrd.Rows[0]["DevelopmentProgram"]);
            txtEvaluationPurpose.Text = Convert.ToString(dtGrd.Rows[0]["EvaluationPurpose"]);
            txtRecommendation.Text = Convert.ToString(dtGrd.Rows[0]["Recommendation"]);
            txtStrengths.Text = Convert.ToString(dtGrd.Rows[0]["commentsStrengths"]);
            txtWeakness.Text = Convert.ToString(dtGrd.Rows[0]["commentsWeakness"]);
            txtRemarks.Text = Convert.ToString(dtGrd.Rows[0]["remarks"]);

            decimal totWeight = 0;
            decimal totResult = 0;

            foreach (GridViewRow row in grdBHV.Rows)
            {
                Label lblWeight = (Label)row.FindControl("lblWeight");
                Label lbachievement = (Label)row.FindControl("lbresult");
                if (lbachievement.Text == "")
                {
                    lbachievement.Text = "0";
                    totResult += 0;
                }
                else { totResult += Convert.ToDecimal(lbachievement.Text.Remove(lbachievement.Text.Length - 1).Trim()); }
                totWeight += Convert.ToDecimal(lblWeight.Text.Remove(lblWeight.Text.Length - 1).Trim());//lbachievement.Text.Remove(lbachievement.Text.Length - 1).Trim()


                // here you'll get all rows with RowType=DataRow
                // others like Header are omitted in a foreach
            }
            foreach (GridViewRow row in grd.Rows)
            {
                Label lblWeight = (Label)row.FindControl("lblWeight");
                Label lbachievement = (Label)row.FindControl("lbresult");
                totWeight += Convert.ToDecimal(lblWeight.Text.Remove(lblWeight.Text.Length - 1).Trim());//lbachievement.Text.Remove(lbachievement.Text.Length - 1).Trim()
                if (lbachievement.Text == "")
                {
                    lbachievement.Text = "0";
                    totResult += 0;
                }
                else { totResult += Convert.ToDecimal(lbachievement.Text.Remove(lbachievement.Text.Length - 1).Trim()); }

                // here you'll get all rows with RowType=DataRow
                // others like Header are omitted in a foreach
            }
            lbtotWeight1.Text = String.Format("{0:0.00}", totWeight) + " %";
            lbtotResult1.Text = String.Format("{0:0.00}", totResult) + " %";
        }
        else
        {
            grdBHV.DataSource = null;
            grdBHV.DataBind();
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
    protected void grdBHV_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdBHV.EditIndex = e.NewEditIndex;
        vInitGridBHV();
    }
    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void grdBHV_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grd.EditIndex = -1;
        vInitGrid();
    }

    protected void grdBHV_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdBHV.EditIndex = -1;
        vInitGridBHV();
    }

    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        grd.EditIndex = -1;
        vInitGrid();
    }
    protected void grdBHV_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //if (lblIsIncentive.Text == "True")
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Can not delete becuase incentive already apporved','Invalid to delete','warning');", true);
        //    return;
        //}
        HiddenField hdmerchUpload_cd = (HiddenField)grdBHV.Rows[e.RowIndex].FindControl("hdmerchUpload_cd");
        HiddenField hdftarget = (HiddenField)grdBHV.Rows[e.RowIndex].FindControl("hdftarget");
        TextBox txachievement = (TextBox)grdBHV.Rows[e.RowIndex].FindControl("txachievement");
        if (Convert.ToDouble(txachievement.Text) > Convert.ToDouble(hdftarget.Value))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Can not achievement greater then target','Invalid achievement','warning');", true);
            return;
        }
        else
        {
            bll.vLookUp("update trpt_hrd_kpiresultdtl set achievement = " + Convert.ToDecimal(txachievement.Text) + " where ids='" + hdmerchUpload_cd.Value + "' and emp_cd='" + cbemployee.SelectedValue + "'");
        }
        grdBHV.EditIndex = -1;
        vInitGridBHV();
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

                if (lbtarget != null && lblids != null)
                {
                    if (lblkeyresp_nm.Text == "ATTENDANCE")
                    {
                        decimal daysInMonth = Convert.ToDecimal(bll.vLookUp("select dbo.fn_getDaysInMonth('" + cbperiod.SelectedValue + "')").ToString());
                        //decimal totalFriday = Convert.ToDecimal(bll.vLookUp("select dbo.fn_getFriday('" + cbperiod.SelectedValue + "')").ToString());
                        decimal totalAttendance = Convert.ToDecimal(bll.vLookUp("select dbo.fn_MerchandiserAttendanceMonth('" + cbemployee.SelectedValue + "','" + cbperiod.SelectedValue + "')").ToString());
                        decimal percentage = Convert.ToDecimal(bll.vLookUp("select dbo.fn_MerchandiserAttendance('" + cbemployee.SelectedValue + "','" + cbperiod.SelectedValue + "')").ToString());

                        decimal target = Convert.ToDecimal(lbtarget.Text);
                        //decimal achievement = Convert.ToDecimal((percentage * target) / 100);
                        decimal achievement = (Convert.ToDecimal(lblTotalAttendance.Text) / Convert.ToDecimal(lblTotalDays.Text)) * target;
                        lbachievement.Text = String.Format("{0:0.00}", achievement);
                        bll.vLookUp("update  trpt_hrd_kpiresultdtl set achievement=  " + lbachievement.Text +
                            "where period='" + cbperiod.SelectedValue + "' and emp_cd='" + cbemployee.SelectedValue
                            + "' and job_title_cd='" + cbjobtitle.SelectedValue + "' and ids='" + lblids.Text + "'");


                        spAttAchievement.InnerText = "( " + Convert.ToString(lblTotalAttendance.Text) + " / " + Convert.ToString(Convert.ToInt32(lblTotalDays.Text)) + " ) X  " + Convert.ToString(target);
                        spAttResult.InnerText = "( " + Convert.ToString(lblTotalAttendance.Text) + " / " + Convert.ToString(Convert.ToInt32(lblTotalDays.Text)) + " ) /  " + Convert.ToString(lblWeight.Text);


                    }
                    else if (lblkeyresp_nm.Text == "INVENTORY  DATA REPORT")
                    {
                        decimal tot = 0;

                        var jared = bll.vLookUp("select dbo.fn_merchandiserjaredInctentive('" + Convert.ToString(cbperiod.SelectedValue) + "','" + Convert.ToString(cbemployee.SelectedValue) + "')");
                        if (Convert.ToDecimal(jared) > 0)
                        {
                            lbachievement.Text = Convert.ToString(lbtarget.Text);
                        }
                        else { lbachievement.Text = Convert.ToString(tot); }
                        bll.vLookUp("update  trpt_hrd_kpiresultdtl set achievement=  " + lbachievement.Text +
                            "where period='" + cbperiod.SelectedValue + "' and emp_cd='" + cbemployee.SelectedValue
                            + "' and job_title_cd='" + cbjobtitle.SelectedValue + "' and ids='" + lblids.Text + "'");
                    }
                    else if (lblkeyresp_nm.Text == "NEAR EXPIRY REPORT")
                    {
                        decimal tot = 0;

                        List<cArrayList> ner = new List<cArrayList>();
                        ner.Add(new cArrayList("@emp_cd", Convert.ToString(cbemployee.SelectedValue)));
                        ner.Add(new cArrayList("@period_cd", Convert.ToString(cbperiod.SelectedValue)));
                        DataTable dtner = new DataTable();
                        dtner = cdl.GetValueFromSP("sp_rpt_stockcustomerne_emp", ner);
                        if (dtner.Rows.Count > 0)
                        {
                            lbachievement.Text = Convert.ToString(lbtarget.Text);
                        }
                    }
                    else if (lblkeyresp_nm.Text == "STORE VISIT")
                    {
                        decimal tot = 0;
                        List<cArrayList> drOther = new List<cArrayList>();
                        drOther.Add(new cArrayList("@emp_cd", Convert.ToString(cbemployee.SelectedValue)));
                        drOther.Add(new cArrayList("@period_cd", cbperiod.SelectedValue));
                        DataTable dt = cdl.GetValueFromSP("sp_rp_merchandiservisitCount", drOther);

                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0][0].ToString() == "0.00") { tot = 0; }
                            else if (dt.Rows[0][0].ToString() == "") { tot = 0; }
                            else { tot = Convert.ToDecimal(dt.Rows[0][0]) / 60; }
                            //contents = serializer.Serialize(dt);
                        }
                        else
                        {
                            tot = 0;
                        }
                        //var visit = bll.vLookUp("select dbo.fn_rp_merchandiservisit('" + Convert.ToString(cbperiod.SelectedValue) + "','" + Convert.ToString(cbemployee.SelectedValue) + "')");

                        //if (visit != "") {
                        //    tot = Convert.ToDecimal(visit)/60;
                        //}

                        decimal target = Convert.ToDecimal(lbtarget.Text);
                        decimal achievement = Convert.ToDecimal((Convert.ToDecimal(tot) * target) / 100);

                        if (Convert.ToDecimal(tot) > 0)
                        {
                            lbachievement.Text = String.Format("{0:0.00}", tot);
                        }
                        else { lbachievement.Text = String.Format("{0:0.00}", tot); }
                        bll.vLookUp("update  trpt_hrd_kpiresultdtl set achievement=  " + lbachievement.Text +
                        "where period='" + cbperiod.SelectedValue + "' and emp_cd='" + cbemployee.SelectedValue
                        + "' and job_title_cd='" + cbjobtitle.SelectedValue + "' and ids='" + lblids.Text + "'");
                    }

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
                            dTotQtyResult += dResult;
                            dTotQtyWeight += dWeight;

                            dTotResult += dResult;
                            dTotWeight += dWeight;
                            if (lblkeyresp_nm.Text == "ATTENDANCE")
                                lblResult.Text = lbresult.Text;
                        }
                    }
                }
                else if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate))
                {
                    Label lblids = (Label)e.Row.FindControl("lblids");
                    Label lbresult = (Label)e.Row.FindControl("lbresult");
                    Label lbachievement = (Label)e.Row.FindControl("lbachievement");
                    Label lblobjective = (Label)e.Row.FindControl("lblobjective");
                    Label lbtarget = (Label)e.Row.FindControl("lbtarget");
                    Label lblkeyresp_nm = (Label)e.Row.FindControl("lblkeyresp_nm");
                    Label lblWeight = (Label)e.Row.FindControl("lblWeight");
                    HiddenField hdids = (HiddenField)e.Row.FindControl("ids");

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
                    dTotQtyResult += dResult;
                    dTotQtyWeight += dWeight;
                    dTotResult += dResult;
                    dTotWeight += dWeight;

                    if (lblkeyresp_nm.Text == "ATTENDANCE")
                        lblResult.Text = lbresult.Text;
                }
                else if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
                {
                    Label lbresult = (Label)e.Row.FindControl("lbresult");
                    TextBox txachievement = (TextBox)e.Row.FindControl("txachievement");
                    Label lbtarget = (Label)e.Row.FindControl("lbtarget");
                    Label lblWeight = (Label)e.Row.FindControl("lblWeight");
                    Label lbachievement = (Label)e.Row.FindControl("lbachievement");
                    Label lblkeyresp_nm = (Label)e.Row.FindControl("lblkeyresp_nm");
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
                    dTotQtyResult += dResult;
                    dTotQtyWeight += dWeight;
                    dTotResult += dResult;
                    dTotWeight += dWeight;

                    if (lblkeyresp_nm.Text == "ATTENDANCE")
                        lblResult.Text = lbresult.Text;
                }
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    Label lbtotWeight = (Label)e.Row.FindControl("lbtotWeight");
                    Label lbtotResult = (Label)e.Row.FindControl("lbtotResult");
                    lbtotWeight.Text = String.Format("{0:0.00}", dTotQtyWeight) + " %";
                    lbtotResult.Text = String.Format("{0:0.00}", dTotQtyResult) + " %";
                    lbtotWeight1.Text = String.Format("{0:0.00}", dTotWeight) + " %";
                    lbtotResult1.Text = String.Format("{0:0.00}", dTotResult) + " %";
                }
            }
        catch (Exception ex)
        {
            ut.Logs("", "Merchandiser Incentive", "KPI HRD", "fm_HRDPA", "grd_RowDataBound", "Exception", ex.Message + ex.InnerException);
            throw;
        }
    }
    protected void grdBHV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbresult = (Label)e.Row.FindControl("lbresult");
                Label lbachievement = (Label)e.Row.FindControl("lbachievement");
                Label lblobjective = (Label)e.Row.FindControl("lblobjective");
                Label lbtarget = (Label)e.Row.FindControl("lbtarget");
                Label lblkeyresp_nm = (Label)e.Row.FindControl("lblkeyresp_nm");
                Label lblWeight = (Label)e.Row.FindControl("lblWeight");
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

                        dTotQalResult += dResult;
                        dTotQalWeight += dWeight;
                    }
                }
            }
            else if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate))
            {
                Label lbresult = (Label)e.Row.FindControl("lbresult");
                Label lbachievement = (Label)e.Row.FindControl("lbachievement");
                Label lblobjective = (Label)e.Row.FindControl("lblobjective");
                Label lbtarget = (Label)e.Row.FindControl("lbtarget");
                Label lblkeyresp_nm = (Label)e.Row.FindControl("lblkeyresp_nm");
                Label lblWeight = (Label)e.Row.FindControl("lblWeight");
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
            }
            else if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
            {
                Label lbresult = (Label)e.Row.FindControl("lbresult");
                Label lbachievement = (Label)e.Row.FindControl("lbachievement");
                Label lblobjective = (Label)e.Row.FindControl("lblobjective");
                Label lbtarget = (Label)e.Row.FindControl("lbtarget");
                Label lblkeyresp_nm = (Label)e.Row.FindControl("lblkeyresp_nm");
                Label lblWeight = (Label)e.Row.FindControl("lblWeight");
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

                        dTotQalResult += dResult;
                        dTotQalWeight += dWeight;
                    }
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbtotWeight = (Label)e.Row.FindControl("lbtotWeight");
                Label lbtotResult = (Label)e.Row.FindControl("lbtotResult");
                lbtotWeight.Text = String.Format("{0:0.00}", dTotQalWeight) + " %";
                lbtotResult.Text = String.Format("{0:0.00}", dTotQalResult) + " %";

                lbtotWeight1.Text = String.Format("{0:0.00}", dTotWeight) + " %";
                lbtotResult1.Text = String.Format("{0:0.00}", dTotResult) + " %";
            }
        }
        catch (Exception ex)
        {
            ut.Logs("", "Merchandiser Incentive", "KPI HRD", "fm_HRDPA", "grdBHV_RowDataBound", "Exception", ex.Message + ex.InnerException);
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
            //BindControl();
            dTotResult = 0;
            dTotWeight = 0;
            vInitGrid();
            vInitGridBHV();
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
            ut.Logs("", "Merchandiser Incentive", "KPI HRD", "fm_HRDPA", "btnsave_Click", "Exception", ex.Message + ex.InnerException);
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
        lblNationalit.Text = "Non Saudi";
        //}
        vInitGrid();
        vInitGridBHV();
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

    protected void cbperiod_SelectedIndexChanged(object sender, EventArgs e)
    {
        vInitGrid();
        vInitGridBHV();
    }
    protected void btprintMerch_Visit_Click(object sender, EventArgs e)
    {
        if (cbemployee.SelectedValue.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Cannot Print Report!','Employee is not Selected','warning');", true);
            return;
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=MerchindiserVisit&" +
            "period=" + cbperiod.SelectedValue.ToString() + "&emp=" + cbemployee.SelectedValue.ToString() + "');", true);
    }



    protected void btnViewAttendance_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "popupwindow('fm_MerchandiserAttendance.aspx?emp_cd=" + cbemployee.SelectedValue + "&period=" + cbperiod.SelectedValue + "');", true);
    }

    protected void btnMerchOther_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=MerchandiserHis&" +
            "period_cd=" + cbperiod.SelectedValue.ToString() + "&driver_cd=" + cbemployee.SelectedValue.ToString() + "');", true);
    }
    protected void grdOther_RowDataBound(object sender, GridViewRowEventArgs e)
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

                if (lbtarget != null && lblids != null)
                {
                    if (lblobjective.Text == "Customer Coverage")
                    {
                        decimal tot = 0;
                        decimal qty = 0;
                        List<cArrayList> arr = new List<cArrayList>();
                        arr.Add(new cArrayList("@driver_cd", cbemployee.SelectedValue));
                        arr.Add(new cArrayList("@period_cd", cbperiod.SelectedValue));
                        DataTable dtGrd = new DataTable();
                        dtGrd = cdl.GetValueFromSP("sp_driverIncetiveDetails", arr);
                        if (dtGrd.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtGrd.Rows)
                            {

                                qty += Convert.ToDecimal(dr["qty"]);
                            }
                        }

                        lbachievement.Text = Convert.ToString(qty);
                        bll.vLookUp("update  trpt_hrd_kpiresultdtl_other set achievement=  " + lbachievement.Text +
                            "where period='" + cbperiod.SelectedValue + "' and emp_cd='" + cbemployee.SelectedValue
                            + "' and job_title_cd='" + cbjobtitle.SelectedValue + "' and ids='" + lblids.Text + "'");
                    }
                    else if (lblobjective.Text == "Fulfillment of Customer/store visit with agreed route and expected time duration")
                    {
                        decimal tot = 0;
                        decimal countCustomer = 0;
                        List<cArrayList> arr = new List<cArrayList>();
                        arr.Add(new cArrayList("@driver_cd", cbemployee.SelectedValue));
                        arr.Add(new cArrayList("@period_cd", cbperiod.SelectedValue));
                        DataTable dtGrd = new DataTable();
                        dtGrd = cdl.GetValueFromSP("sp_driverIncetiveDetails", arr);
                        if (dtGrd.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtGrd.Rows)
                            {
                                countCustomer = Convert.ToDecimal(dr["COuntCustomer"]);
                            }
                        }

                        lbachievement.Text = Convert.ToString(countCustomer);

                        bll.vLookUp("update  trpt_hrd_kpiresultdtl_other set achievement=  " + lbachievement.Text +
                            "where period='" + cbperiod.SelectedValue + "' and emp_cd='" + cbemployee.SelectedValue
                            + "' and job_title_cd='" + cbjobtitle.SelectedValue + "' and ids='" + lblids.Text + "'");
                    }

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
                        dTotQtyOtherResult += dResult;
                        dTotQtyOtherWeight += dWeight;

                        dTotResult += dResult;
                        dTotWeight += dWeight;
                        //if (lblkeyresp_nm.Text == "ATTENDANCE")
                        //lblResult.Text = lbresult.Text;
                    }
                }
            }
            else if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate))
            {
                Label lblids = (Label)e.Row.FindControl("lblids");
                Label lbresult = (Label)e.Row.FindControl("lbresult");
                Label lbachievement = (Label)e.Row.FindControl("lbachievement");
                Label lblobjective = (Label)e.Row.FindControl("lblobjective");
                Label lbtarget = (Label)e.Row.FindControl("lbtarget");
                Label lblkeyresp_nm = (Label)e.Row.FindControl("lblkeyresp_nm");
                Label lblWeight = (Label)e.Row.FindControl("lblWeight");
                HiddenField hdids = (HiddenField)e.Row.FindControl("ids");

                double dAchivement = Convert.ToDouble(lbachievement.Text);
                double dTarget = Convert.ToDouble(lbtarget.Text);
                double dWeight = Convert.ToDouble(lblWeight.Text);
                double dResult = (dTarget == 0 ? 0 : ((dAchivement * dWeight) / dTarget));
                if (dResult >= dWeight)
                {
                    dResult = dWeight;
                }
                lbresult.Text = String.Format("{0:0.00}", dResult) + " %"; // dResult.ToString("0.00") + " %";
                dTotQtyOtherResult += dResult;
                dTotQtyOtherWeight += dWeight;
                dTotResult += dResult;
                dTotWeight += dWeight;

                //if (lblkeyresp_nm.Text == "ATTENDANCE")
                //lblResult.Text = lbresult.Text;
            }
            else if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
            {
                Label lbresult = (Label)e.Row.FindControl("lbresult");
                TextBox txachievement = (TextBox)e.Row.FindControl("txachievement");
                Label lbtarget = (Label)e.Row.FindControl("lbtarget");
                Label lblWeight = (Label)e.Row.FindControl("lblWeight");
                Label lbachievement = (Label)e.Row.FindControl("lbachievement");
                Label lblkeyresp_nm = (Label)e.Row.FindControl("lblkeyresp_nm");
                double dAchivement = Convert.ToDouble(lbachievement.Text);
                double dTarget = Convert.ToDouble(lbtarget.Text);
                double dWeight = Convert.ToDouble(lblWeight.Text);
                double dResult = (dTarget == 0 ? 0 : ((dAchivement * dWeight) / dTarget));
                if (dResult >= dWeight)
                {
                    dResult = dWeight;
                }
                lbresult.Text = String.Format("{0:0.00}", dResult) + " %"; // dResult.ToString("0.00") + " %";
                dTotQtyOtherResult += dResult;
                dTotQtyOtherWeight += dWeight;
                dTotResult += dResult;
                dTotWeight += dWeight;

                //if (lblkeyresp_nm.Text == "ATTENDANCE")
                //lblResult.Text = lbresult.Text;
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbtotWeight = (Label)e.Row.FindControl("lbtotWeight");
                Label lbtotResult = (Label)e.Row.FindControl("lbtotResult");
                lbtotWeight.Text = String.Format("{0:0.00}", dTotQtyOtherWeight) + " %";
                lbtotResult.Text = String.Format("{0:0.00}", dTotQtyOtherResult) + " %";
                //lbtotWeight1.Text = String.Format("{0:0.00}", dTotWeight) + " %";
                //lbtotResult1.Text = String.Format("{0:0.00}", dTotResult) + " %";
            }
        }
        catch (Exception ex)
        {
            ut.Logs("", "Merchandiser Incentive", "KPI HRD", "fm_HRDPA", "grd_RowDataBound", "Exception", ex.Message + ex.InnerException);
            throw;
        }
    }

    protected void btnNearExpired_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
            "openreport('fm_report2.aspx?src=stockcustomerne_emp&emp=" + cbemployee.SelectedValue.ToString() + "&period_cd=" + cbperiod.SelectedValue.ToString() + "');", true);
    }
}