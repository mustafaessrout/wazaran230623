using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppClassTools;

public partial class fm_mstcompetency : System.Web.UI.Page
{
    cbll bll = new cbll();
    AppClass app = new AppClass();
    Utitlity ut = new Utitlity();
    cdal cdl = new cdal();

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Visible = false;
        if (!IsPostBack)
        {
            try
            {
                bll.vBindingFieldValueToCombo(ref cbcat, "appraisal");
                bll.vBindingFieldValueToCombo(ref cbfor, "appraisalfor");
                bll.vBindingFieldValueToCombo(ref cbjobtitle, "job_title_cd");
                bll.vBindingComboToSp(ref cbsp, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm");
                bll.vBindingComboToSp(ref cbperiod, "sp_tmst_period_get", "period_cd", "period_nm");
                cbperiod.SelectedValue = bll.vLookUp("select dbo.fn_getcontrolparameter('period')");
                cbjobtitle_SelectedIndexChanged(sender, e);
            }
            catch (Exception ex)
            {
                app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
                ut.Logs("", "Appraisal", "Master Key Appraisal", "fm_mstcompetency", "Page_Load", "Exception", ex.Message + ex.InnerException);
            }
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            string error = string.Empty;
            double dQty = 0;
            if (txdesc.Text == "" && Convert.ToString(cbcat.SelectedValue) != "KPI") error += "Please assign appraisal description. <br/> ";
            if (txpoor.Text == "" && Convert.ToString(cbcat.SelectedValue) != "KPI") error += "Please assign poor category appraisal details. <br/> ";
            if (txfair.Text == "" && Convert.ToString(cbcat.SelectedValue) != "KPI") error += "Please assign fair category appraisal details. <br/> ";
            if (txgood.Text == "" && Convert.ToString(cbcat.SelectedValue) != "KPI") error += "Please assign good category appraisal details. <br/> ";
            if (txverygood.Text == "" && Convert.ToString(cbcat.SelectedValue) != "KPI") error += "Please assign very good category appraisal details. <br/> ";
            if (txexcellent.Text == "" && Convert.ToString(cbcat.SelectedValue) != "KPI") error += "Please assign excellent category appraisal details. <br/> ";

            if (!double.TryParse(txtPoorIncentive.Text, out dQty) || !double.TryParse(txtFairIncentive.Text, out dQty)
                || !double.TryParse(txtGoodIncentive.Text, out dQty) || !double.TryParse(txtVeryGoodIncentive.Text, out dQty)
                || !double.TryParse(txtExcellentIncentive.Text, out dQty))
            {
                error += "Incentive must numeric. <br/> ";
            }
            else if (!double.TryParse(txtPoorIncentiveMax.Text, out dQty) || !double.TryParse(txtFairIncentiveMax.Text, out dQty)
                || !double.TryParse(txtGoodIncentiveMax.Text, out dQty) || !double.TryParse(txtVeryGoodIncentiveMax.Text, out dQty)
                || !double.TryParse(txtExcellentIncentiveMax.Text, out dQty))
            {
                error += "Poor/Fair/Good/Very Good/Excellent Incentive Max must numeric. <br/> ";
            }
            else if (!double.TryParse(txtPoorIncentiveMin.Text, out dQty) || !double.TryParse(txtFairIncentiveMin.Text, out dQty)
                || !double.TryParse(txtGoodIncentiveMin.Text, out dQty) || !double.TryParse(txtVeryGoodIncentiveMin.Text, out dQty)
                || !double.TryParse(txtExcellentIncentiveMin.Text, out dQty))
            {
                error += "Poor/Fair/Good/Very Good/Excellent Incentive Min must numeric. <br/> ";
            }
            else if (Convert.ToString(cbcat.SelectedValue) == "KPI") {
                if (!double.TryParse(txtQTYWeight.Text, out dQty) || !double.TryParse(txtInvoiceWeight.Text, out dQty)) {
                    error += "Qty / Invoice weight  must numeric. <br/> ";
                }
            }
            else if (Convert.ToString(cbcat.SelectedValue) == "CMP")
            {
                if (!double.TryParse(txtMasterWeight.Text, out dQty) )
                {
                    error += "Weight  must numeric. <br/> ";
                }
            }
            if (error.Length > 0 && Convert.ToString(cbcat.SelectedValue)!="KPI")
            {
                app.BootstrapAlert(lblMsg, error, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Warning, true);
            }
            else
            {
                if (!((Convert.ToDecimal(txtPoorIncentiveMax.Text) > Convert.ToDecimal(txtPoorIncentiveMin.Text))
                    && (Convert.ToDecimal(txtFairIncentiveMin.Text) > Convert.ToDecimal(txtPoorIncentiveMax.Text))
                    && (Convert.ToDecimal(txtFairIncentiveMax.Text) > Convert.ToDecimal(txtFairIncentiveMin.Text))
                    && (Convert.ToDecimal(txtGoodIncentiveMin.Text) > Convert.ToDecimal(txtFairIncentiveMax.Text))
                    && (Convert.ToDecimal(txtGoodIncentiveMax.Text) > Convert.ToDecimal(txtGoodIncentiveMin.Text))
                    && (Convert.ToDecimal(txtVeryGoodIncentiveMin.Text) > Convert.ToDecimal(txtGoodIncentiveMax.Text))
                   && (Convert.ToDecimal(txtVeryGoodIncentiveMax.Text) > Convert.ToDecimal(txtVeryGoodIncentiveMin.Text))
                    && (Convert.ToDecimal(txtExcellentIncentiveMin.Text) > Convert.ToDecimal(txtVeryGoodIncentiveMax.Text))
                    && (Convert.ToDecimal(txtExcellentIncentiveMax.Text) > Convert.ToDecimal(txtExcellentIncentiveMin.Text))) && Convert.ToString(cbcat.SelectedValue) != "KPI")
                {
                    error += "Please check range <br/> ";
                }


                // for driver validation

                if (error.Length > 0)
                {
                    app.BootstrapAlert(lblMsg, error, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Warning, true);
                }
                else
                {

                    List<cArrayList> arr = new List<cArrayList>();
                    
                    arr.Add(new cArrayList("@poor", txpoor.Text));
                    arr.Add(new cArrayList("@fair", txfair.Text));
                    arr.Add(new cArrayList("@good", txgood.Text));
                    arr.Add(new cArrayList("@verygood", txverygood.Text));
                    arr.Add(new cArrayList("@appraisal_cat", cbcat.SelectedValue.ToString()));
                    arr.Add(new cArrayList("@excellent", txexcellent.Text));
                    arr.Add(new cArrayList("@appraisalfor", cbfor.SelectedValue.ToString()));
                    arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
                    arr.Add(new cArrayList("@OrderBy", Convert.ToInt32(txtOrderBy.Text)));
                    arr.Add(new cArrayList("@salespoint_cd", Convert.ToString(cbsp.SelectedValue)));
                    arr.Add(new cArrayList("@period", Convert.ToString(cbperiod.SelectedValue)));
                    arr.Add(new cArrayList("@nationality", Convert.ToString(ddlAppraisalFor.SelectedValue)));

                    if (cbcat.SelectedValue == "CMP" )
                    {
                        arr.Add(new cArrayList("@WeightValue", Convert.ToInt32(txtMasterWeight.Text)));
                        arr.Add(new cArrayList("@TargetValue", Convert.ToInt32(cbTargetMaster.SelectedValue)));
                    }
                    else if (cbcat.SelectedValue == "KR")
                    {
                        arr.Add(new cArrayList("@WeightValue", 0));
                        arr.Add(new cArrayList("@TargetValue", 0));
                    }

                    arr.Add(new cArrayList("@PoorIncentive", Convert.ToDecimal(txtPoorIncentive.Text)));
                    arr.Add(new cArrayList("@FairIncentive", Convert.ToDecimal(txtFairIncentive.Text)));
                    arr.Add(new cArrayList("@GoodIncentive", Convert.ToDecimal(txtGoodIncentive.Text)));
                    arr.Add(new cArrayList("@VeryGoodIncentive", Convert.ToString(txtVeryGoodIncentive.Text)));
                    arr.Add(new cArrayList("@ExcellentIncentive", Convert.ToString(txtExcellentIncentive.Text)));

                    arr.Add(new cArrayList("@PoorIncentiveMax", Convert.ToDecimal(txtPoorIncentiveMax.Text)));
                    arr.Add(new cArrayList("@FairIncentiveMax", Convert.ToDecimal(txtFairIncentiveMax.Text)));
                    arr.Add(new cArrayList("@GoodIncentiveMax", Convert.ToDecimal(txtGoodIncentiveMax.Text)));
                    arr.Add(new cArrayList("@VeryGoodIncentiveMax", Convert.ToString(txtVeryGoodIncentiveMax.Text)));
                    arr.Add(new cArrayList("@ExcellentIncentiveMax", Convert.ToString(txtExcellentIncentiveMax.Text)));

                    arr.Add(new cArrayList("@PoorIncentiveMin", Convert.ToDecimal(txtPoorIncentiveMin.Text)));
                    arr.Add(new cArrayList("@FairIncentiveMin", Convert.ToDecimal(txtFairIncentiveMin.Text)));
                    arr.Add(new cArrayList("@GoodIncentiveMin", Convert.ToDecimal(txtGoodIncentiveMin.Text)));
                    arr.Add(new cArrayList("@VeryGoodIncentiveMin", Convert.ToString(txtVeryGoodIncentiveMin.Text)));
                    arr.Add(new cArrayList("@ExcellentIncentiveMin", Convert.ToString(txtExcellentIncentiveMin.Text)));
                    arr.Add(new cArrayList("@CreatedBy", Convert.ToString(Session["usr_id"])));
                    arr.Add(new cArrayList("@CreateDate", DateTime.Now.Date));
                    arr.Add(new cArrayList("@UpdatedBy", Convert.ToString(Session["usr_id"])));
                    arr.Add(new cArrayList("@UpdatedDate", DateTime.Now.Date));
                    if (Convert.ToString(cbcat.SelectedValue) == "CMP" || Convert.ToString(cbcat.SelectedValue) == "KR")
                    {
                        arr.Add(new cArrayList("@appraisal_desc", txdesc.Text));
                    }
                    else {
                        
                        if (cbcat.SelectedValue == "KPI")
                        {
                            
                                arr.Add(new cArrayList("@WeightValue", Convert.ToInt32(txtQTYWeight.Text)));
                                arr.Add(new cArrayList("@TargetValue", 0));
                                arr.Add(new cArrayList("@appraisal_desc", "KPI QTY"));
                                bll.vInsertMstAppraisal(arr);


                                arr.Clear();
                                arr.Add(new cArrayList("@poor", txpoor.Text));
                                arr.Add(new cArrayList("@fair", txfair.Text));
                                arr.Add(new cArrayList("@good", txgood.Text));
                                arr.Add(new cArrayList("@verygood", txverygood.Text));
                                arr.Add(new cArrayList("@appraisal_cat", cbcat.SelectedValue.ToString()));
                                arr.Add(new cArrayList("@excellent", txexcellent.Text));
                                arr.Add(new cArrayList("@appraisalfor", cbfor.SelectedValue.ToString()));
                                arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
                                arr.Add(new cArrayList("@OrderBy", Convert.ToInt32(txtOrderBy.Text)));
                                arr.Add(new cArrayList("@salespoint_cd", Convert.ToString(cbsp.SelectedValue)));
                                arr.Add(new cArrayList("@period", Convert.ToString(cbperiod.SelectedValue)));
                                arr.Add(new cArrayList("@nationality", Convert.ToString(ddlAppraisalFor.SelectedValue)));

                                arr.Add(new cArrayList("@PoorIncentive", Convert.ToDecimal(txtPoorIncentive.Text)));
                                arr.Add(new cArrayList("@FairIncentive", Convert.ToDecimal(txtFairIncentive.Text)));
                                arr.Add(new cArrayList("@GoodIncentive", Convert.ToDecimal(txtGoodIncentive.Text)));
                                arr.Add(new cArrayList("@VeryGoodIncentive", Convert.ToString(txtVeryGoodIncentive.Text)));
                                arr.Add(new cArrayList("@ExcellentIncentive", Convert.ToString(txtExcellentIncentive.Text)));

                                arr.Add(new cArrayList("@PoorIncentiveMax", Convert.ToDecimal(txtPoorIncentiveMax.Text)));
                                arr.Add(new cArrayList("@FairIncentiveMax", Convert.ToDecimal(txtFairIncentiveMax.Text)));
                                arr.Add(new cArrayList("@GoodIncentiveMax", Convert.ToDecimal(txtGoodIncentiveMax.Text)));
                                arr.Add(new cArrayList("@VeryGoodIncentiveMax", Convert.ToString(txtVeryGoodIncentiveMax.Text)));
                                arr.Add(new cArrayList("@ExcellentIncentiveMax", Convert.ToString(txtExcellentIncentiveMax.Text)));

                                arr.Add(new cArrayList("@PoorIncentiveMin", Convert.ToDecimal(txtPoorIncentiveMin.Text)));
                                arr.Add(new cArrayList("@FairIncentiveMin", Convert.ToDecimal(txtFairIncentiveMin.Text)));
                                arr.Add(new cArrayList("@GoodIncentiveMin", Convert.ToDecimal(txtGoodIncentiveMin.Text)));
                                arr.Add(new cArrayList("@VeryGoodIncentiveMin", Convert.ToString(txtVeryGoodIncentiveMin.Text)));
                                arr.Add(new cArrayList("@ExcellentIncentiveMin", Convert.ToString(txtExcellentIncentiveMin.Text)));

                                arr.Add(new cArrayList("@WeightValue", Convert.ToInt32(txtInvoiceWeight.Text)));
                                arr.Add(new cArrayList("@TargetValue", 0));
                                arr.Add(new cArrayList("@appraisal_desc", "KPI Invoice"));
                                arr.Add(new cArrayList("@CreatedBy", Convert.ToString(Session["usr_id"])));
                                arr.Add(new cArrayList("@CreateDate", DateTime.Now.Date));
                                arr.Add(new cArrayList("@UpdatedBy", Convert.ToString(Session["usr_id"])));
                                arr.Add(new cArrayList("@UpdatedDate", DateTime.Now.Date));
                                bll.vInsertMstAppraisal(arr);
                            }
                     
                        
                        else { bll.vInsertMstAppraisal(arr); }

                        
                    }
                    if (Convert.ToString(cbjobtitle.SelectedValue) == "5" && (Convert.ToString(cbcat.SelectedValue)=="CMP"  || Convert.ToString(cbcat.SelectedValue)=="KR"))
                    {
                        //"sp_tmst_appraisal_checkDriverKPI"
                        
                            bll.vInsertMstAppraisal(arr);
                            ClearControl();
                        
                    }
                }
                vBindingKRGrid();
                vBindingKPIGrid();
                vBindingCMPGrid();
            }
        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "Appraisal", "Master Key Appraisal", "fm_mstcompetency", "btsave_Click", "Exception", ex.Message + ex.InnerException);
        }
    }

    void vBindingKRGrid()
    {
        try
        {
            if (cbcat.SelectedValue == "KR")
            {
                dvDescription.Visible = true;
                dvKPI.Visible = false;
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@appraisal_cat", cbcat.SelectedValue.ToString()));
                arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
                arr.Add(new cArrayList("@salespoint_cd", Convert.ToString(cbsp.SelectedValue)));
                arr.Add(new cArrayList("@period", Convert.ToString(cbperiod.SelectedValue)));
                arr.Add(new cArrayList("@nationality", Convert.ToString(ddlAppraisalFor.SelectedValue)));

                bll.vBindingGridToSp(ref grdKR, "sp_tmst_appraisal_get", arr);
                int cnt = cdl.GetValueFromSP("sp_tmst_appraisal_get", arr).Rows.Count;
                if (cnt != 0)
                {
                    cnt = Convert.ToInt32(cdl.GetValueFromSP("sp_tmst_appraisal_get", arr).Compute("max([OrderBy])", string.Empty));
                }
                txtOrderBy.Text = Convert.ToString(cnt + 1);
                lblWeightMaster.Visible = false;
                txtMasterWeight.Visible = false;
                lblTargetMaster.Visible = false;
                cbTargetMaster.Visible = false;
            }
            else
            {
                grdKR.DataSource = null;
                grdKR.DataBind();
            }


        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "Appraisal", "Master Key Appraisal", "fm_mstcompetency", "vBindingGrid", "Exception", ex.Message + ex.InnerException);
        }
    }
    void vBindingKPIGrid()
    {
        try
        {
            if (Convert.ToString(cbcat.SelectedValue) == "KPI")
            {
                dvDescription.Visible = false;
                dvKPI.Visible = true;

                string cntQTY = string.Empty;
                string cntInvoice = string.Empty;
                List<cArrayList> arrCheckpQTY = new List<cArrayList>();
                List<cArrayList> arrCheckpInvoice = new List<cArrayList>();
                arrCheckpQTY.Add(new cArrayList("@appraisal_desc", "KPI QTY"));
                arrCheckpQTY.Add(new cArrayList("@appraisal_cat", cbcat.SelectedValue.ToString()));
                arrCheckpQTY.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
                arrCheckpQTY.Add(new cArrayList("@salespoint_cd", Convert.ToString(cbsp.SelectedValue)));
                arrCheckpQTY.Add(new cArrayList("@period", Convert.ToString(cbperiod.SelectedValue)));
                arrCheckpQTY.Add(new cArrayList("@nationality", Convert.ToString(ddlAppraisalFor.SelectedValue)));

                bll.nCheckDriverKPI(arrCheckpQTY, ref cntQTY);

                arrCheckpInvoice.Add(new cArrayList("@appraisal_desc", "KPI Invoice"));
                arrCheckpInvoice.Add(new cArrayList("@appraisal_cat", cbcat.SelectedValue.ToString()));
                arrCheckpInvoice.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
                arrCheckpInvoice.Add(new cArrayList("@salespoint_cd", Convert.ToString(cbsp.SelectedValue)));
                arrCheckpInvoice.Add(new cArrayList("@period", Convert.ToString(cbperiod.SelectedValue)));
                arrCheckpInvoice.Add(new cArrayList("@nationality", Convert.ToString(ddlAppraisalFor.SelectedValue)));

                bll.nCheckDriverKPI(arrCheckpInvoice, ref cntInvoice);

                if (Convert.ToInt32(cntQTY) >= 1)
                {
                    //app.BootstrapAlert(lblMsg, "Can not assign more than defintion.", app.alertType = AppClassTools.AppClass.BootstrapAlertType.Warning, true);
                }
                else
                {
                    //arr.Add(new cArrayList("@appraisal_desc", "KPI QTY"));
                    //bll.vInsertMstAppraisal(arr);
                }
                if (Convert.ToInt32(cntQTY) >= 1)
                {
                    //app.BootstrapAlert(lblMsg, "Can not assign more than defintion.", app.alertType = AppClassTools.AppClass.BootstrapAlertType.Warning, true);
                }
                else
                {
                    //arr.Add(new cArrayList("@appraisal_desc", "KPI QTY"));
                    //bll.vInsertMstAppraisal(arr);
                }

                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@appraisal_cat", cbcat.SelectedValue.ToString()));
                arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
                arr.Add(new cArrayList("@salespoint_cd", Convert.ToString(cbsp.SelectedValue)));
                arr.Add(new cArrayList("@period", Convert.ToString(cbperiod.SelectedValue)));
                arr.Add(new cArrayList("@nationality", Convert.ToString(ddlAppraisalFor.SelectedValue)));


                bll.vBindingGridToSp(ref grdKPI, "sp_tmst_appraisal_get", arr);
                int cnt = cdl.GetValueFromSP("sp_tmst_appraisal_get", arr).Rows.Count;
                if (cnt != 0)
                {
                    cnt = Convert.ToInt32(cdl.GetValueFromSP("sp_tmst_appraisal_get", arr).Compute("max([OrderBy])", string.Empty));
                }
                txtOrderBy.Text = Convert.ToString(cnt + 1);
                lblWeightMaster.Visible = true;
                txtMasterWeight.Visible = true;
                lblTargetMaster.Visible = false;
                cbTargetMaster.Visible = false;
            }
            else
            {


                grdKPI.DataSource = null;
                grdKPI.DataBind();
            }


        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "Appraisal", "Master Key Appraisal", "fm_mstcompetency", "vBindingGrid", "Exception", ex.Message + ex.InnerException);
        }
    }

    void vBindingCMPGrid()
    {
        try
        {
            if (Convert.ToString(cbcat.SelectedValue) == "CMP")
            {
                dvDescription.Visible = true;
                dvKPI.Visible = false;
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@appraisal_cat", cbcat.SelectedValue.ToString()));
                arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
                arr.Add(new cArrayList("@salespoint_cd", Convert.ToString(cbsp.SelectedValue)));
                arr.Add(new cArrayList("@period", Convert.ToString(cbperiod.SelectedValue)));
                arr.Add(new cArrayList("@nationality", Convert.ToString(ddlAppraisalFor.SelectedValue)));


                bll.vBindingGridToSp(ref grdCMP, "sp_tmst_appraisal_get", arr);
                int cnt = cdl.GetValueFromSP("sp_tmst_appraisal_get", arr).Rows.Count;
                if (cnt != 0)
                {
                    cnt = Convert.ToInt32(cdl.GetValueFromSP("sp_tmst_appraisal_get", arr).Compute("max([OrderBy])", string.Empty));
                }
                txtOrderBy.Text = Convert.ToString(cnt + 1);
                lblWeightMaster.Visible = true;
                txtMasterWeight.Visible = true;
                lblTargetMaster.Visible = true;
                cbTargetMaster.Visible = true;
            }
            else
            {

                grdCMP.DataSource = null;
                grdCMP.DataBind();
            }


        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "Appraisal", "Master Key Appraisal", "fm_mstcompetency", "vBindingGrid", "Exception", ex.Message + ex.InnerException);
        }
    }
    

    void FixedBindingForKPI(string appraisal_desc)
    {
        try {
            
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@appraisal_desc", appraisal_desc));
        arr.Add(new cArrayList("@poor", txpoor.Text));
        arr.Add(new cArrayList("@fair", txfair.Text));
        arr.Add(new cArrayList("@good", txgood.Text));
        arr.Add(new cArrayList("@verygood", txverygood.Text));
        arr.Add(new cArrayList("@appraisal_cat", cbcat.SelectedValue.ToString()));
        arr.Add(new cArrayList("@excellent", txexcellent.Text));
        arr.Add(new cArrayList("@appraisalfor", cbfor.SelectedValue.ToString()));
        arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
        arr.Add(new cArrayList("@OrderBy", Convert.ToInt32(txtOrderBy.Text)));
        arr.Add(new cArrayList("@salespoint_cd", Convert.ToString(cbsp.SelectedValue)));
        arr.Add(new cArrayList("@period", Convert.ToString(cbperiod.SelectedValue)));
        arr.Add(new cArrayList("@nationality", Convert.ToString(ddlAppraisalFor.SelectedValue)));
        arr.Add(new cArrayList("@TargetValue", 0));
        arr.Add(new cArrayList("@WeightValue", 0));

        arr.Add(new cArrayList("@PoorIncentive", Convert.ToDecimal(txtPoorIncentive.Text)));
        arr.Add(new cArrayList("@FairIncentive", Convert.ToDecimal(txtFairIncentive.Text)));
        arr.Add(new cArrayList("@GoodIncentive", Convert.ToDecimal(txtGoodIncentive.Text)));
        arr.Add(new cArrayList("@VeryGoodIncentive", Convert.ToString(txtVeryGoodIncentive.Text)));
        arr.Add(new cArrayList("@ExcellentIncentive", Convert.ToString(txtExcellentIncentive.Text)));

        arr.Add(new cArrayList("@PoorIncentiveMax", Convert.ToDecimal(txtPoorIncentiveMax.Text)));
        arr.Add(new cArrayList("@FairIncentiveMax", Convert.ToDecimal(txtFairIncentiveMax.Text)));
        arr.Add(new cArrayList("@GoodIncentiveMax", Convert.ToDecimal(txtGoodIncentiveMax.Text)));
        arr.Add(new cArrayList("@VeryGoodIncentiveMax", Convert.ToString(txtVeryGoodIncentiveMax.Text)));
        arr.Add(new cArrayList("@ExcellentIncentiveMax", Convert.ToString(txtExcellentIncentiveMax.Text)));

        arr.Add(new cArrayList("@PoorIncentiveMin", Convert.ToDecimal(txtPoorIncentiveMin.Text)));
        arr.Add(new cArrayList("@FairIncentiveMin", Convert.ToDecimal(txtFairIncentiveMin.Text)));
        arr.Add(new cArrayList("@GoodIncentiveMin", Convert.ToDecimal(txtGoodIncentiveMin.Text)));
        arr.Add(new cArrayList("@VeryGoodIncentiveMin", Convert.ToString(txtVeryGoodIncentiveMin.Text)));
        arr.Add(new cArrayList("@ExcellentIncentiveMin", Convert.ToString(txtExcellentIncentiveMin.Text)));
        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "Appraisal", "Master Key Appraisal", "fm_mstcompetency", "FixedBindingForKPI", "Exception", ex.Message + ex.InnerException);
        }
    }

    protected void cbperiod_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            vBindingKRGrid();
            vBindingCMPGrid();
            vBindingKPIGrid();
        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "Appraisal", "Target Driver", "fm_targetdriver", "cbperiod_SelectedIndexChanged", "Exception", ex.Message + ex.InnerException);
        }
    }
    protected void cbcat_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            vBindingKRGrid();
            vBindingCMPGrid();
            vBindingKPIGrid();
        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "Appraisal", "Master Key Appraisal", "fm_mstcompetency", "cbcat_SelectedIndexChanged", "Exception", ex.Message + ex.InnerException);
        }
    }
    protected void grdKR_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            HiddenField hdappraisal = (HiddenField)grdKR.Rows[e.RowIndex].FindControl("hdappraisal");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@appraisal_id", hdappraisal.Value.ToString()));
            bll.vDelMstAppraisal(arr);
            vBindingKRGrid();
        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "Appraisal", "Master Key Appraisal", "fm_mstcompetency", "grdKR_RowDeleting", "Exception", ex.Message + ex.InnerException);
        }
    }


    private void ClearControl()
    {
        txdesc.Text = string.Empty;
        txexcellent.Text = string.Empty;
        txfair.Text = string.Empty;
        txgood.Text = string.Empty;
        txverygood.Text = string.Empty;
        txpoor.Text = string.Empty;
    }

    protected void cbjobtitle_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClearControl();
            vBindingKRGrid();
            vBindingKPIGrid();
            vBindingCMPGrid();
        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "Appraisal", "Master Key Appraisal", "fm_mstcompetency", "cbjobtitle_SelectedIndexChanged", "Exception", ex.Message + ex.InnerException);
        }
    }

    protected void ddlAppraisalFor_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClearControl();
            vBindingKRGrid();
            vBindingKPIGrid();
            vBindingCMPGrid();
        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "Appraisal", "Master Key Appraisal", "fm_mstcompetency", "ddlAppraisalFor_SelectedIndexChanged", "Exception", ex.Message + ex.InnerException);
        }
    }

    protected void grdKR_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        { }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "Appraisal", "Master Key Appraisal", "fm_mstcompetency", "grdKR_RowDataBound", "Exception", ex.Message + ex.InnerException);
        }
    }

    protected void grdKR_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            grdKR.EditIndex = e.NewEditIndex;
            List<cArrayList> arr = new List<cArrayList>();
            vBindingKRGrid();

        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "APPRAISAL", "Master Key Appraisal", "fm_mstcompetency", "grdKR_RowEditing", "Exception", ex.Message + ex.InnerException);
        }
    }
    protected void grdKR_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            grdKR.EditIndex = -1;
            arr.Add(new cArrayList("@appraisal_cat", cbcat.SelectedValue.ToString()));
            arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespoint_cd", Convert.ToString(cbsp.SelectedValue)));
            arr.Add(new cArrayList("@period", Convert.ToString(cbperiod.SelectedValue)));
            arr.Add(new cArrayList("@nationality", Convert.ToString(ddlAppraisalFor.SelectedValue)));

            bll.vBindingGridToSp(ref grdKR, "sp_tmst_appraisal_get", arr);
        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "APPRAISAL", "Master Key Appraisal", "fm_mstcompetency", "grdKR_RowCancelingEdit", "Exception", ex.Message + ex.InnerException);
        }
    }
    protected void grdKR_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            HiddenField appraisal_id = (HiddenField)grdKR.Rows[e.RowIndex].FindControl("hdappraisal");
            HiddenField hdfTargetValue = (HiddenField)grdKR.Rows[e.RowIndex].FindControl("hdfTargetValue");
            HiddenField hdfWeightValue = (HiddenField)grdKR.Rows[e.RowIndex].FindControl("hdfWeightValue");
            HiddenField hdfAchiveTarget = (HiddenField)grdKR.Rows[e.RowIndex].FindControl("hdfAchiveTarget");
            HiddenField hdfAchiveWeight = (HiddenField)grdKR.Rows[e.RowIndex].FindControl("hdfAchiveWeight");
            HiddenField hdfappraisal_desc = (HiddenField)grdKR.Rows[e.RowIndex].FindControl("hdfappraisal_desc");

            TextBox txtOrderBy = (TextBox)grdKR.Rows[e.RowIndex].FindControl("txtOrderBy");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@appraisal_id", Convert.ToString(appraisal_id.Value)));
            arr.Add(new cArrayList("@salespoint_cd", Convert.ToString(cbsp.SelectedValue)));
            arr.Add(new cArrayList("@period", Convert.ToString(cbperiod.SelectedValue)));
            arr.Add(new cArrayList("@appraisal_cat", cbcat.SelectedValue.ToString()));
            arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
            arr.Add(new cArrayList("@appraisalfor", cbfor.SelectedValue.ToString()));
            arr.Add(new cArrayList("@nationality", Convert.ToString(ddlAppraisalFor.SelectedValue)));
            arr.Add(new cArrayList("@TargetValue", Convert.ToDecimal(hdfTargetValue.Value)));
            arr.Add(new cArrayList("@WeightValue", Convert.ToDecimal(hdfWeightValue.Value)));
            arr.Add(new cArrayList("@appraisal_desc", Convert.ToString(hdfappraisal_desc.Value)));
            //arr.Add(new cArrayList("@AchiveTarget", Convert.ToDecimal(hdfAchiveTarget.Value)));
            //arr.Add(new cArrayList("@AchiveWeight", Convert.ToDecimal(hdfAchiveWeight.Value)));

            arr.Add(new cArrayList("@OrderBy", Convert.ToInt32(txtOrderBy.Text)));
            arr.Add(new cArrayList("@CreatedBy", Convert.ToString(Session["usr_id"])));
            arr.Add(new cArrayList("@CreateDate", DateTime.Now.Date));
            arr.Add(new cArrayList("@UpdatedBy", Convert.ToString(Session["usr_id"])));
            arr.Add(new cArrayList("@UpdatedDate", DateTime.Now.Date));



            bll.vInsertMstAppraisal(arr);
            arr.Clear();
            arr.Add(new cArrayList("@appraisal_cat", cbcat.SelectedValue.ToString()));
            arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespoint_cd", Convert.ToString(cbsp.SelectedValue)));
            arr.Add(new cArrayList("@period", Convert.ToString(cbperiod.SelectedValue)));
            arr.Add(new cArrayList("@nationality", Convert.ToString(ddlAppraisalFor.SelectedValue)));

            vBindingKRGrid();
            int cnt = cdl.GetValueFromSP("sp_tmst_appraisal_get", arr).Rows.Count;
            cnt = Convert.ToInt32(cdl.GetValueFromSP("sp_tmst_appraisal_get", arr).Compute("max([OrderBy])", string.Empty));
            txtOrderBy.Text = Convert.ToString(cnt + 1);
            grdKR_RowCancelingEdit(sender, null);
        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "APPRAISAL", "Master Key Appraisal", "fm_mstcompetency", "grdKR_RowUpdating", "Exception", ex.Message + ex.InnerException);
        }
    }

    protected void grdKPI_RowDataBound(object sender, GridViewRowEventArgs e) { }
    protected void grdKPI_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdKPI.EditIndex = e.NewEditIndex;
        List<cArrayList> arr = new List<cArrayList>();
        vBindingKPIGrid();
    }
    protected void grdKPI_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e) { }
    protected void grdKPI_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            HiddenField appraisal_id = (HiddenField)grdKPI.Rows[e.RowIndex].FindControl("hdappraisal");
            HiddenField hdfTargetValue = (HiddenField)grdKPI.Rows[e.RowIndex].FindControl("hdfTargetValue");
            TextBox txtOrderBy = (TextBox)grdKPI.Rows[e.RowIndex].FindControl("txtOrderBy");
            TextBox txtWeightValue = (TextBox)grdKPI.Rows[e.RowIndex].FindControl("txtWeightValue");


            HiddenField hdfAchiveTarget = (HiddenField)grdKPI.Rows[e.RowIndex].FindControl("hdfAchiveTarget");
            HiddenField hdfAchiveWeight = (HiddenField)grdKPI.Rows[e.RowIndex].FindControl("hdfAchiveWeight");
            HiddenField hdfappraisal_desc = (HiddenField)grdKPI.Rows[e.RowIndex].FindControl("hdfappraisal_desc");



            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@appraisal_id", Convert.ToString(appraisal_id.Value)));
            arr.Add(new cArrayList("@TargetValue", Convert.ToString(hdfTargetValue.Value)));
            arr.Add(new cArrayList("@WeightValue", Convert.ToString(txtWeightValue.Text)));
            arr.Add(new cArrayList("@salespoint_cd", Convert.ToString(cbsp.SelectedValue)));
            arr.Add(new cArrayList("@period", Convert.ToString(cbperiod.SelectedValue)));
            arr.Add(new cArrayList("@appraisal_cat", cbcat.SelectedValue.ToString()));
            arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
            arr.Add(new cArrayList("@appraisalfor", cbfor.SelectedValue.ToString()));
            arr.Add(new cArrayList("@nationality", Convert.ToString(ddlAppraisalFor.SelectedValue)));
            arr.Add(new cArrayList("@appraisal_desc", Convert.ToString(hdfappraisal_desc.Value)));
            arr.Add(new cArrayList("@OrderBy", Convert.ToInt32(txtOrderBy.Text)));

            //arr.Add(new cArrayList("@PoorIncentive", string.Empty));
            //arr.Add(new cArrayList("@FairIncentive", string.Empty));
            //arr.Add(new cArrayList("@GoodIncentive", string.Empty));
            //arr.Add(new cArrayList("@VeryGoodIncentive", string.Empty));
            //arr.Add(new cArrayList("@ExcellentIncentive", string.Empty));

            //arr.Add(new cArrayList("@PoorIncentiveMax", Convert.ToDecimal(txtPoorIncentiveMax.Text)));
            //arr.Add(new cArrayList("@FairIncentiveMax", Convert.ToDecimal(txtFairIncentiveMax.Text)));
            //arr.Add(new cArrayList("@GoodIncentiveMax", Convert.ToDecimal(txtGoodIncentiveMax.Text)));
            //arr.Add(new cArrayList("@VeryGoodIncentiveMax", Convert.ToString(txtVeryGoodIncentiveMax.Text)));
            //arr.Add(new cArrayList("@ExcellentIncentiveMax", Convert.ToString(txtExcellentIncentiveMax.Text)));

            //arr.Add(new cArrayList("@PoorIncentiveMin", Convert.ToDecimal(txtPoorIncentiveMin.Text)));
            //arr.Add(new cArrayList("@FairIncentiveMin", Convert.ToDecimal(txtFairIncentiveMin.Text)));
            //arr.Add(new cArrayList("@GoodIncentiveMin", Convert.ToDecimal(txtGoodIncentiveMin.Text)));
            //arr.Add(new cArrayList("@VeryGoodIncentiveMin", Convert.ToString(txtVeryGoodIncentiveMin.Text)));
            //arr.Add(new cArrayList("@ExcellentIncentiveMin", Convert.ToString(txtExcellentIncentiveMin.Text)));
            arr.Add(new cArrayList("@CreatedBy", Convert.ToString(Session["usr_id"])));
            arr.Add(new cArrayList("@CreateDate", DateTime.Now.Date));
            arr.Add(new cArrayList("@UpdatedBy", Convert.ToString(Session["usr_id"])));
            arr.Add(new cArrayList("@UpdatedDate", DateTime.Now.Date));
            bll.vInsertMstAppraisal(arr);
            arr.Clear();
            arr.Add(new cArrayList("@appraisal_cat", cbcat.SelectedValue.ToString()));
            arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespoint_cd", Convert.ToString(cbsp.SelectedValue)));
            arr.Add(new cArrayList("@period", Convert.ToString(cbperiod.SelectedValue)));
            arr.Add(new cArrayList("@nationality", Convert.ToString(ddlAppraisalFor.SelectedValue)));

            vBindingKPIGrid();
            bll.vBindingGridToSp(ref grdKPI, "sp_tmst_appraisal_get", arr);
            int cnt = cdl.GetValueFromSP("sp_tmst_appraisal_get", arr).Rows.Count;
            cnt = Convert.ToInt32(cdl.GetValueFromSP("sp_tmst_appraisal_get", arr).Compute("max([OrderBy])", string.Empty));
            txtOrderBy.Text = Convert.ToString(cnt + 1);
            grdKPI_RowCancelingEdit(sender, null);
        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "APPRAISAL", "Master Key Appraisal", "fm_mstcompetency", "grdKPI_RowUpdating", "Exception", ex.Message + ex.InnerException);
        }
    }
    protected void grdCMP_RowDataBound(object sender, GridViewRowEventArgs e) { }
    protected void grdCMP_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdCMP.EditIndex = e.NewEditIndex;
        List<cArrayList> arr = new List<cArrayList>();
        vBindingCMPGrid();
    }
    protected void grdCMP_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try { }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "Appraisal", "Master Key Appraisal", "fm_mstcompetency", "grdCMP_RowDataBound", "Exception", ex.Message + ex.InnerException);
        }
    }
    protected void grdCMP_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            HiddenField appraisal_id = (HiddenField)grdCMP.Rows[e.RowIndex].FindControl("hdappraisal");
            HiddenField hdfappraisal_desc = (HiddenField)grdCMP.Rows[e.RowIndex].FindControl("hdfappraisal_desc");
            TextBox txtOrderBy = (TextBox)grdCMP.Rows[e.RowIndex].FindControl("txtOrderBy");
            TextBox txtCMPWeight = (TextBox)grdCMP.Rows[e.RowIndex].FindControl("txtCMPWeight");
            DropDownList cbTarget = (DropDownList)grdCMP.Rows[e.RowIndex].FindControl("cbTarget");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@appraisal_id", Convert.ToString(appraisal_id.Value)));
            arr.Add(new cArrayList("@salespoint_cd", Convert.ToString(cbsp.SelectedValue)));
            arr.Add(new cArrayList("@period", Convert.ToString(cbperiod.SelectedValue)));
            arr.Add(new cArrayList("@appraisal_cat", cbcat.SelectedValue.ToString()));
            arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
            arr.Add(new cArrayList("@appraisalfor", cbfor.SelectedValue.ToString()));
            arr.Add(new cArrayList("@nationality", Convert.ToString(ddlAppraisalFor.SelectedValue)));
            arr.Add(new cArrayList("@OrderBy", Convert.ToDouble(txtOrderBy.Text)));
            arr.Add(new cArrayList("@WeightValue", Convert.ToDouble(txtCMPWeight.Text)));
            arr.Add(new cArrayList("@TargetValue", Convert.ToString(cbTarget.SelectedValue)));
            arr.Add(new cArrayList("@appraisal_desc", Convert.ToString(hdfappraisal_desc.Value)));
            arr.Add(new cArrayList("@CreatedBy", Convert.ToString(Session["usr_id"])));
            arr.Add(new cArrayList("@CreateDate", DateTime.Now.Date));
            arr.Add(new cArrayList("@UpdatedBy", Convert.ToString(Session["usr_id"])));
            arr.Add(new cArrayList("@UpdatedDate", DateTime.Now.Date));
            bll.vInsertMstAppraisal(arr);
            arr.Clear();
            arr.Add(new cArrayList("@appraisal_cat", cbcat.SelectedValue.ToString()));
            arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespoint_cd", Convert.ToString(cbsp.SelectedValue)));
            arr.Add(new cArrayList("@period", Convert.ToString(cbperiod.SelectedValue)));
            arr.Add(new cArrayList("@nationality", Convert.ToString(ddlAppraisalFor.SelectedValue)));

            vBindingCMPGrid();
            int cnt = cdl.GetValueFromSP("sp_tmst_appraisal_get", arr).Rows.Count;
            cnt = Convert.ToInt32(cdl.GetValueFromSP("sp_tmst_appraisal_get", arr).Compute("max([OrderBy])", string.Empty));
            txtOrderBy.Text = Convert.ToString(cnt + 1);
            grdCMP_RowCancelingEdit(sender, null);
        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "APPRAISAL", "Master Key Appraisal", "fm_mstcompetency", "grdCMP_RowUpdating", "Exception", ex.Message + ex.InnerException);
        }
    }

    protected void grdKPI_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        HiddenField hdappraisal = (HiddenField)grdKPI.Rows[e.RowIndex].FindControl("hdappraisal");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@appraisal_id", hdappraisal.Value.ToString()));
        bll.vDelMstAppraisal(arr);
        vBindingKPIGrid();
    }
    protected void grdCMP_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        HiddenField hdappraisal = (HiddenField)grdCMP.Rows[e.RowIndex].FindControl("hdappraisal");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@appraisal_id", hdappraisal.Value.ToString()));
        bll.vDelMstAppraisal(arr);
        vBindingCMPGrid();
    }
    protected void cbsp_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            vBindingKRGrid();
            vBindingCMPGrid();
            vBindingKPIGrid();
        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "Appraisal", "Target Driver", "fm_targetdriver", "cbsp_SelectedIndexChanged", "Exception", ex.Message + ex.InnerException);
        }
    }
}