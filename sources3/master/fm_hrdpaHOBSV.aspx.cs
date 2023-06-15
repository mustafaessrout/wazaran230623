using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_hrdpaHOBSV : System.Web.UI.Page
{
    cbll bll = new cbll();
    cdal cdl = new cdal();
    Utitlity ut = new Utitlity();
    // here we handle BSV-- So job level, & title are fixed
    double dTotWeight = 0; double dTotResult = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm");
            cbsalespoint_SelectedIndexChanged(sender, e);
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

        DataTable dtTemp = (DataTable)cbperiod.DataSource;

        DataTable dtPeriod = new DataTable();
        List<cArrayList> arr = new List<cArrayList>();
        dtPeriod = cdl.GetValueFromSP("sp_tmst_period_get", arr);

        if (dtPeriod != null)
        {
            if (dtPeriod.Rows.Count > 0)
            {
                cbperiod.DataValueField = "period_cd";
                cbperiod.DataTextField = "period_nm";
                cbperiod.DataSource = dtPeriod;
                cbperiod.DataBind();
                cbperiod.SelectedValue = bll.sGetControlParameter("period");
            }
        }

       
    }
    protected void cblevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@job_title_cd", "5"));
        arr.Add(new cArrayList("@level_cd", "1"));
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbemployee, "sp_tmst_employee_getbyjobtitleBSV", "emp_cd", "emp_desc", arr);
        cbemployee_SelectedIndexChanged(sender, e);
    }
    protected void cbjobtitle_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@job_title_cd", "5"));
        arr.Add(new cArrayList("@level_cd", "1"));
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbemployee, "sp_tmst_employee_getbyjobtitleBSV", "emp_cd", "emp_desc", arr);
        cbemployee_SelectedIndexChanged(sender, e);
    }
    

    void vInitGrid()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@period", cbperiod.SelectedValue.ToString()));
        arr.Add(new cArrayList("@emp_cd", cbemployee.SelectedValue.ToString()));
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        DataTable dt = new DataTable();
        dt = cdl.GetValueFromSP("sp_trpt_hrd_kpiresultdtl_getBSV", arr);
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

        if (cbemployee.SelectedValue.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Employee is not Selected ','Please Select Employee!','warning');", true);
            return;
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('../fm_report2.aspx?src=rp_hrdkpiIncentiveBSV&sp=" + Convert.ToString(cbsalespoint.SelectedValue) + "&emp="
            + cbemployee.SelectedValue.ToString() + "&period=" + cbperiod.SelectedValue.ToString() + "');", true);
       
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
        TextBox txachievement = (TextBox)grd.Rows[e.RowIndex].FindControl("txachievement");
        Label lblkeyresp_nm = (Label)grd.Rows[e.RowIndex].FindControl("lblkeyresp_nm");
        HiddenField hdids = (HiddenField)grd.Rows[e.RowIndex].FindControl("hdids");
        if (lblkeyresp_nm.Text == "DELIVERY PRODUCT")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('You can not edit delivery product!','DELIVERY PRODUCT','warning');", true);
            return;
        }
        else
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@ids", hdids.Value.ToString()));
            arr.Add(new cArrayList("@achievement", txachievement.Text));
            arr.Add(new cArrayList("@emp_cd", cbemployee.SelectedValue.ToString()));
            arr.Add(new cArrayList("@period", cbperiod.SelectedValue.ToString()));
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
                HiddenField hdids = (HiddenField)e.Row.FindControl("ids");
                Label lblWeight = (Label)e.Row.FindControl("lblWeight");
                double dWeight = Convert.ToDouble(lblWeight.Text);
                dTotWeight += dWeight;
                var sum = string.Empty;
                if (lbtarget != null && lblids != null)
                {
                    sum = bll.vLookUp("select sum(qty) from thrd_kpitargetdtl where ids='" + lblids.Text.ToString() + "'  and period='" + cbperiod.SelectedValue.ToString() + "' and salespointcd='" + cbsalespoint.SelectedValue.ToString() + "' and emp_Cd in (select emp_cd from tmst_activedriver where salespointcd='"+ cbsalespoint.SelectedValue.ToString() + "' and isBSV<>0 )");
                    if (lblobjective.Text == "Fulfillment of delivery product with agreed quantity ")
                    //if (lblobjective.Text == "Fulfillment of delivery")
                    {


                        //var tempData = bll.vLookUp("select dbo.fn_totalInvoiceAchive_invoice('" + Convert.ToString(cbemployee.SelectedValue) + "','" + Convert.ToString(cbperiod.SelectedValue) + "')");
                        //var tempData = bll.vLookUp("select sum(achievement) from trpt_hrd_kpiresultdtl where achievement >0 and  period ='" + Convert.ToString(cbperiod.SelectedValue) + "' and objective like '%Fulfillment of delivery product with agreed%'  and salespointcd='" + cbsalespoint.SelectedValue.ToString() + "' and emp_Cd in (select emp_cd from tmst_activedriver where salespointcd='" + cbsalespoint.SelectedValue.ToString() + "' and isBSV=0 )");

                        List<cArrayList> arr = new List<cArrayList>();
                        arr.Add(new cArrayList("@emp_cdBSV", cbemployee.SelectedValue.ToString()));
                        arr.Add(new cArrayList("@period", cbperiod.SelectedValue.ToString()));
                        arr.Add(new cArrayList("@salespointcd",cbsalespoint.SelectedValue.ToString()));

                        DataTable dtCust = cdl.GetValueFromSP("sp_kpi_Incentive_getBSV1_qty", arr);
                        decimal tempData = 0;
                        if (dtCust.Rows.Count > 0) {
                            tempData = Convert.ToDecimal(dtCust.Rows[0]["achievementValeu"]);
                        }
                        if (lbachievement == null && lblkeyresp_nm.Text == "DELIVERY PRODUCT")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('You can not edit delivery product!','DELIVERY PRODUCT','warning');", true);
                        }
                        else
                        {
                            lbachievement.Text = Convert.ToString(tempData);
                        }
                    }
                    else if (lblobjective.Text == "Customer Coverage")
                    {
                        //var tempData = bll.vLookUp("select dbo.fn_totalCustomerAchive_invoice('" + Convert.ToString(cbemployee.SelectedValue) + "','" + Convert.ToString(cbperiod.SelectedValue) + "')");
                        //var tempData = bll.vLookUp("select sum(achievement) from trpt_hrd_kpiresultdtl where achievement >0 and  period ='" + Convert.ToString(cbperiod.SelectedValue) + "' and objective like '%Customer Coverage%' and salespointcd='"+cbsalespoint.SelectedValue.ToString() + "' and emp_Cd in (select emp_cd from tmst_activedriver where salespointcd='" + cbsalespoint.SelectedValue.ToString() + "' and isBSV=0 )");
                        List<cArrayList> arr = new List<cArrayList>();
                        arr.Add(new cArrayList("@emp_cdBSV", cbemployee.SelectedValue.ToString()));
                        arr.Add(new cArrayList("@period", cbperiod.SelectedValue.ToString()));
                        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));

                        DataTable dtCust = cdl.GetValueFromSP("sp_kpi_Incentive_getBSV1_cust", arr);
                        decimal tempData = 0;
                        if (dtCust.Rows.Count > 0)
                        {
                            tempData = Convert.ToDecimal(dtCust.Rows[0]["achievementValeu"]);
                        }
                        if (lbachievement == null && lblkeyresp_nm.Text == "DELIVERY PRODUCT")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('You can not edit delivery product!','DELIVERY PRODUCT','warning');", true);
                        }
                        else
                        {
                            lbachievement.Text = Convert.ToString(tempData);
                        }
                    }
                    else if (lblobjective.Text == "APPEARANCE AND MANNER")
                    {
                        //var tempData = bll.vLookUp("select dbo.fn_totalCustomerAchive_invoice('" + Convert.ToString(cbemployee.SelectedValue) + "','" + Convert.ToString(cbperiod.SelectedValue) + "')");
                        var tempData = bll.vLookUp("select sum(achievement) from trpt_hrd_kpiresultdtl where  achievement >0 and  period ='" + Convert.ToString(cbperiod.SelectedValue) + "' and objective like '%APPEARANCE AND MANNER%' and salespointcd='" + cbsalespoint.SelectedValue.ToString() + "' and emp_Cd in (select emp_cd from tmst_activedriver where salespointcd='" + cbsalespoint.SelectedValue.ToString() + "' and isBSV=0 )");
                        if (lbachievement == null && lblkeyresp_nm.Text == "DELIVERY PRODUCT")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('You can not edit delivery product!','DELIVERY PRODUCT','warning');", true);
                        }
                        else
                        {
                            lbachievement.Text = Convert.ToString(tempData);
                        }
                    }
                    else if (lblobjective.Text == "PUNCTUALITY")
                    {
                        //var tempData = bll.vLookUp("select dbo.fn_totalCustomerAchive_invoice('" + Convert.ToString(cbemployee.SelectedValue) + "','" + Convert.ToString(cbperiod.SelectedValue) + "')");
                        var tempData = bll.vLookUp("select sum(achievement) from trpt_hrd_kpiresultdtl where  achievement >0 and  period ='" + Convert.ToString(cbperiod.SelectedValue) + "' and objective like '%PUNCTUALITY%' and salespointcd='" + cbsalespoint.SelectedValue.ToString() + "' and emp_Cd in (select emp_cd from tmst_activedriver where salespointcd='" + cbsalespoint.SelectedValue.ToString() + "' and isBSV=0 )");
                        if (lbachievement == null && lblkeyresp_nm.Text == "DELIVERY PRODUCT")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('You can not edit delivery product!','DELIVERY PRODUCT','warning');", true);
                        }
                        else
                        {
                            lbachievement.Text = Convert.ToString(tempData);
                        }
                    }
                    else if (lblobjective.Text == "ATTITUDE")
                    {
                        //var tempData = bll.vLookUp("select dbo.fn_totalCustomerAchive_invoice('" + Convert.ToString(cbemployee.SelectedValue) + "','" + Convert.ToString(cbperiod.SelectedValue) + "')");
                        var tempData = bll.vLookUp("select sum(achievement) from trpt_hrd_kpiresultdtl where  achievement >0 and  period ='" + Convert.ToString(cbperiod.SelectedValue) + "' and objective like '%ATTITUDE%' and salespointcd='" + cbsalespoint.SelectedValue.ToString() + "' and emp_Cd in (select emp_cd from tmst_activedriver where salespointcd='" + cbsalespoint.SelectedValue.ToString() + "' and isBSV=0 )");
                        if (lbachievement == null && lblkeyresp_nm.Text == "DELIVERY PRODUCT")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('You can not edit delivery product!','DELIVERY PRODUCT','warning');", true);
                        }
                        else
                        {
                            lbachievement.Text = Convert.ToString(tempData);
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
                        
                        //double dResult = (dAchivement * 100) / dTarget;//(dAchivement * 100) / dTarget;
                        //lbresult.Text = String.Format("{0:0.00}", dResult) + " %"; // dResult.ToString("0.00") + " %";

                        double dResult = (dAchivement * dWeight) / dTarget;
                        lbresult.Text = String.Format("{0:0.00}", dResult) + " %"; // dResult.ToString("0.00") + " %";
                        dTotResult += dResult;
                        
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
                        dr["job_title_cd"] = "5";
                        dr["level_cd"] = "1";
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
                            dr["job_title_cd"] = Convert.ToString(5);
                            dr["level_cd"] = Convert.ToString(1);
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
                double dWeight = Convert.ToDouble(lblWeight.Text);
                dTotWeight += dWeight;
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
                
                //double dResult = (dAchivement / dTarget) * 100;
                //lbresult.Text = dResult.ToString() + " %";



                double dResult = (dAchivement * dWeight) / dTarget;
                lbresult.Text = String.Format("{0:0.00}", dResult) + " %"; // dResult.ToString("0.00") + " %";
                dTotResult += dResult;
                
            }
            else if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
            {
                Label lbresult = (Label)e.Row.FindControl("lbresult");
                TextBox txachievement = (TextBox)e.Row.FindControl("txachievement");
                Label lbtarget = (Label)e.Row.FindControl("lbtarget");
                Label lblWeight = (Label)e.Row.FindControl("lblWeight");
                double dWeight = Convert.ToDouble(lblWeight.Text);
                if (txachievement.Text == "")
                {
                    txachievement.Text = "0";
                }
                double dAchivement = Convert.ToDouble(txachievement.Text);
                double dTarget = Convert.ToDouble(lbtarget.Text);
                
                //double dResult = (dAchivement / dTarget) * 100;
                //lbresult.Text = dResult.ToString() + " %";

                double dResult = (dAchivement * dWeight) / dTarget;
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
            //
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Error during data binding','Error','warning');", true);

            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('" + ex.Message.ToString() + ex.InnerException.ToString() + "','" + ex.Message.ToString() + ex.InnerException.ToString() + "','warning');", true);
            ut.Logs("", "Driver Incentive", "KPI HRD", "grd_RowDataBound", "btnsave_Click", "Exception", ex.Message + ex.InnerException);
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
            else if (txtStrengths.Text == "" && txtStrengths.Text == null)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please enter strengths','Strengths','warning');", true);
                return;
            }
            else if (txtWeakness.Text == "" && txtWeakness.Text == null)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please enter weakness','weakness','warning');", true);
                return;
            }
            else if (txtDevelopmentProgram.Text == "" && txtDevelopmentProgram.Text == null)
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
            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
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
            bll.vInsertOtherRecommendation(arr);
            BindControl();
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
    protected void cbemployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        var nationality = bll.vLookUp("select nationality  from tmst_employee where emp_cd='" + cbemployee.SelectedValue.ToString() + "'");
        //if (Convert.ToString(nationality) == "SAUDI")
        //{
        //    lblNationalit.Text = "Saudi";
        //    btcalculation_Click(sender, e);
        //}
        //else { 

        //}
        vInitGrid();
    }

    void CalculateKPI()
    {
        if (cbemployee.SelectedValue != "")
        {
            DataTable dt = new DataTable();
            string CalIncetiveID = string.Empty;
            decimal perctnageSum = 0;
            dt = (DataTable)Session["dtStoreCalculation"];

            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@job_title_cd", "5"));
            arr.Add(new cArrayList("@level_cd", "1"));
            arr.Add(new cArrayList("@emp_cd", cbemployee.SelectedValue.ToString()));
            arr.Add(new cArrayList("@kpi_Recommendation_cd", ddlRecommendation.SelectedValue.ToString()));
            arr.Add(new cArrayList("@kpi_EvaluationPurposeOther_cd", ddlEvaluationPurpose.SelectedValue.ToString()));
            arr.Add(new cArrayList("@period", cbperiod.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespointcd", Convert.ToString(cbsalespoint.SelectedValue.ToString())));
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
                    arr.Add(new cArrayList("@achievement", Convert.ToDecimal(dr["achievement"])));
                    arr.Add(new cArrayList("@salespointcd", Convert.ToString(cbsalespoint.SelectedValue.ToString())));
                    arr.Add(new cArrayList("@achievementPercentage", Convert.ToDecimal(dr["achievementPercentage"])));
                    bll.vInsertCalculateDtlIncetive(arr);

                }
                perctnageSum = Convert.ToDecimal(dt.Compute("Sum(achievementPercentage)", string.Empty));
            }

            if (perctnageSum > 0)
            {
                perctnageSum = perctnageSum / Convert.ToDecimal(dt.Rows.Count);
            }
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
                    arr.Add(new cArrayList("@salespointcd", Convert.ToString(cbsalespoint.SelectedValue.ToString())));
                    arr.Add(new cArrayList("@isSeleted", isSeleted));
                    bll.vInsertCalculateRangeDtl(arr);
                }
            }
        }
    }

   
  
    protected void cbsalespoint_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindControl();
        ddlRecommendation_SelectedIndexChanged(sender, e);
        cblevel_SelectedIndexChanged(sender, e);
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

   

    protected void cbperiod_SelectedIndexChanged(object sender, EventArgs e)
    {
        cbemployee_SelectedIndexChanged(sender, e);
    }

    protected void btRaw_Click(object sender, EventArgs e)
    {
        if (cbemployee.SelectedValue.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Employee is not Selected ','Please Select Employee!','warning');", true);
            return;
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('../fm_report2.aspx?src=rp_hrdkpiIncentiveBSVRaw&sp=" + Convert.ToString(cbsalespoint.SelectedValue) + "&emp="
            + cbemployee.SelectedValue.ToString() + "&period=" + cbperiod.SelectedValue.ToString() + "');", true);
    }

    protected void btPaprint_Click(object sender, EventArgs e)
    {
        if (cbemployee.SelectedValue.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Employee is not Selected ','Please Select Employee!','warning');", true);
            return;
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('../fm_report2.aspx?src=hrdkpiBSV_Pa&sp=" + Convert.ToString(cbsalespoint.SelectedValue) + "&emp="
            + cbemployee.SelectedValue.ToString() + "&period=" + cbperiod.SelectedValue.ToString() + "');", true);
    }
    protected void btInctvprint_Click(object sender, EventArgs e)
    {

        if (cbemployee.SelectedValue.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Employee is not Selected ','Please Select Employee!','warning');", true);
            return;
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('../fm_report2.aspx?src=hrdkpiBSV_Incentive&sp=" + Convert.ToString(cbsalespoint.SelectedValue) + "&emp="
            + cbemployee.SelectedValue.ToString() + "&period=" + cbperiod.SelectedValue.ToString() + "');", true);
    }
    protected void btValidationSheet_Click(object sender, EventArgs e)
    {

        if (cbemployee.SelectedValue.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Employee is not Selected ','Please Select Employee!','warning');", true);
            return;
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('../fm_report2.aspx?src=hrdkpiBSV_ValidationSheet&sp=" + Convert.ToString(cbsalespoint.SelectedValue) + "&period=" + cbperiod.SelectedValue.ToString() + "');", true);
    }
}