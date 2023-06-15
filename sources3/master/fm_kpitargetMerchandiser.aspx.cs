using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_kpitargetMerchandiser : System.Web.UI.Page
{
    cbll bll = new cbll();
    Utitlity ut = new Utitlity();
    cdal cdl = new cdal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            //select top(1)* from thrd_kpi_target
            //select top(1)* from thrd_kpitargetdtl
            //select top(1)* from trpt_hrd_kpiresult
            //select top(1)* from  trpt_hrd_kpiresultdtl
            //Only above Table using in Form 
            List<cArrayList> arr = new List<cArrayList>();
            bll.vBindingFieldValueToCombo(ref cbjobtitle, "job_title_cd");
            bll.vBindingFieldValueToCombo(ref cblevel, "level_cd");
            bll.vBindingComboToSp(ref cbperiod, "sp_tmst_period_get", "period_cd", "period_nm");
            cbperiod.SelectedValue = bll.sGetControlParameter("period");
            bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm");
            cbsalespoint_SelectedIndexChanged(sender, e);
            arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
            arr.Add(new cArrayList("@level_cd", cblevel.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
            //arr.Add(new cArrayList("@isTL", ckTL.Checked));
            arr.Add(new cArrayList("@period", cbperiod.SelectedValue));
            bll.vBindingComboToSp(ref cbemployee, "sp_tmst_employee_getActiveMerchandiser", "emp_cd", "emp_desc", arr);
            
            arr.Clear();
            arr.Add(new cArrayList("@level_no", 2));
            
        }
    }
    protected void cbperiod_SelectedIndexChanged(object sender, EventArgs e)
    {
        vBindingGrid();
    }

    void vBindingGrid()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@level_cd", cblevel.SelectedValue.ToString()));
        arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_thrd_mst_kpi_get", arr);
    }
    protected void cblevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@job_title_cd",cbjobtitle.SelectedValue.ToString()));
        arr.Add(new cArrayList("@level_cd", cblevel.SelectedValue.ToString()));
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        //arr.Add(new cArrayList("@isTL", ckTL.Checked));
        arr.Add(new cArrayList("@period", cbperiod.SelectedValue));
        bll.vBindingComboToSp(ref cbemployee, "sp_tmst_employee_getActiveMerchandiser", "emp_cd", "emp_desc", arr);
        vBindingGrid();
    }
    protected void cbjobtitle_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
        arr.Add(new cArrayList("@level_cd", cblevel.SelectedValue.ToString()));
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        //arr.Add(new cArrayList("@isTL", ckTL.Checked));
        arr.Add(new cArrayList("@period", cbperiod.SelectedValue));
        bll.vBindingComboToSp(ref cbemployee, "sp_tmst_employee_getActiveMerchandiser", "emp_cd", "emp_desc", arr);
        vBindingGrid();
    }
    protected void btsave_Click(object sender, EventArgs e)
    {

        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            //foreach (GridViewRow row in grddtl.Rows)
            //{
            //    if (row.RowType == DataControlRowType.DataRow)
            //    {
            //        arr.Clear();
            //        HiddenField hdids = (HiddenField)row.FindControl("hdids");
            //        TextBox txqty = (TextBox)row.FindControl("txtarget");
            //        arr.Clear();
            //        arr.Add(new cArrayList("@emp_cd", cbemployee.SelectedValue.ToString()));
            //        arr.Add(new cArrayList("@IDS", hdids.Value.ToString()));
            //        arr.Add(new cArrayList("@period", cbperiod.SelectedValue.ToString()));
            //        arr.Add(new cArrayList("@qty", txqty.Text));
            //        arr.Add(new cArrayList("@target_desc", "-"));
            //        bll.vInsertHRDKPITarget(arr);

            //    }
            //}
            HiddenField hdidsDt = new HiddenField();
            arr.Clear();
            arr.Add(new cArrayList("@level_cd", cblevel.SelectedValue.ToString()));
            arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));

            DataTable dt = new DataTable();
            dt = cdl.GetValueFromSP("sp_thrd_mst_kpi_get", arr);
            int cnt = 0;
            foreach (DataRow dr in dt.Rows)
            {
                arr.Clear();
                int cntDtl = 0;
                decimal targetQty = 0;
                foreach (GridViewRow row in grd.Rows)
                {

                    if (cntDtl == cnt)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            HiddenField hdids = (HiddenField)row.FindControl("hdids");
                            TextBox txqty = (TextBox)row.FindControl("txtarget");
                            Label lbtarget = (Label)row.FindControl("lbtarget");
                            if (txqty != null)
                            {
                                targetQty = Convert.ToDecimal(txqty.Text);
                            }
                            else if (txqty == null && 		lbtarget.Text!=""	) {
                                targetQty = Convert.ToDecimal(lbtarget.Text);
                            }
                            hdidsDt = hdids;
                            
                        }
                    }
                    cntDtl += 1;
                }


                arr.Add(new cArrayList("@emp_cd", cbemployee.SelectedValue.ToString()));
                arr.Add(new cArrayList("@IDS", hdidsDt.Value.ToString()));

                arr.Add(new cArrayList("@period", cbperiod.SelectedValue.ToString()));

                var itemQty =  bll.vLookUp("select sum(qty) from thrd_kpitargetdtl where ids='" + hdidsDt.Value.ToString() + "' and emp_cd='" + cbemployee.SelectedValue.ToString() + "' and period='" + cbperiod.SelectedValue.ToString() + "'");


                if (itemQty=="")
                { arr.Add(new cArrayList("@qty", targetQty)); }
                else {
                    arr.Add(new cArrayList("@qty", Convert.ToDecimal(itemQty)));
                }
                //arr.Add(new cArrayList("@qty", bll.vLookUp("select qty from thrd_kpitargetdtl where ids='" + hdidsDt.Value.ToString() + "' and emp_cd='" + cbemployee.SelectedValue.ToString() + "' and period='" + cbperiod.SelectedValue.ToString() + "'")));
                arr.Add(new cArrayList("@jobTitleCd", Convert.ToString(cbjobtitle.SelectedValue)));
                arr.Add(new cArrayList("@levelcd", Convert.ToString(cblevel.SelectedValue)));
                arr.Add(new cArrayList("@target_desc", "-"));
                arr.Add(new cArrayList("@salespointcd", Convert.ToString(cbsalespoint.SelectedValue)));
                bll.vInsertHRDKPITargetHO(arr);
                cnt += 1;
            }

            vBindingGrid();
            
            //btsave.Text = "Add";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('KPI Target has been saved','Successfully','success');", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('" + ex.Message + ex.InnerException + "','" + ex.Message + ex.InnerException + "','warning');", true);
            ut.Logs("", "Driver Incentive", "KPI Master", "fm_kpitarget", "btnsave_Click", "Exception", ex.Message + ex.InnerException);
        }
    }
    protected void cbemployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        vBindingGrid();
    }
    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtarget = (TextBox)e.Row.FindControl("txtarget");
            Label lbtarget = (Label)e.Row.FindControl("lbtarget");
            HiddenField hdids = (HiddenField)e.Row.FindControl("hdids");
            if (txtarget != null)
            {
                txtarget.Text = bll.vLookUp("select sum(qty) from thrd_kpitargetdtl where ids='" + hdids.Value.ToString() + "' and emp_cd='" + cbemployee.SelectedValue.ToString() + "' and period='" + cbperiod.SelectedValue.ToString() + "'");
                if (txtarget.Text != "") {
                    
                    txtarget.Enabled = false;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('You can not edit , check product list!','Can not edit','warning');", true);
                }
                else
                {
                    
                    txtarget.Enabled = true;
                    txtarget.Text = bll.vLookUp("select qty from thrd_kpi_target where ids='" + hdids.Value.ToString() + "' and emp_cd='" + cbemployee.SelectedValue.ToString() + "' and period='" + cbperiod.SelectedValue.ToString() + "'");
                }
            }
            else if (lbtarget != null)
            {
                lbtarget.Text = bll.vLookUp("select sum(qty) from thrd_kpitargetdtl where  ids='" + hdids.Value.ToString() + "' and emp_cd='" + cbemployee.SelectedValue.ToString() + "' and period='" + cbperiod.SelectedValue.ToString() + "'");
            }

            if (lbtarget != null)
            {
                if (lbtarget.Text == "")
                {
                    lbtarget.Text = bll.vLookUp("select qty from thrd_kpi_target where ids='" + hdids.Value.ToString() + "' and emp_cd='" + cbemployee.SelectedValue.ToString() + "' and period='" + cbperiod.SelectedValue.ToString() + "'");
                }
            }
        }
        else if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate))
        {
            Label lbtarget = (Label)e.Row.FindControl("lbtarget");
            HiddenField hdids = (HiddenField)e.Row.FindControl("hdids");
            lbtarget.Text = bll.vLookUp("select qty from thrd_kpi_target where ids='" + hdids.Value.ToString() + "' and emp_cd='" + cbemployee.SelectedValue.ToString() + "' and period='" + cbperiod.SelectedValue.ToString() + "'");
        }
        else if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
        {
            TextBox txtarget = (TextBox)e.Row.FindControl("txtarget");
            HiddenField hdids = (HiddenField)e.Row.FindControl("hdids");
            txtarget.Text = bll.vLookUp("select qty from thrd_kpi_target where ids='" + hdids.Value.ToString() + "' and emp_cd='" + cbemployee.SelectedValue.ToString() + "' and period='" + cbperiod.SelectedValue.ToString() + "'");
        }
         
    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        if (cbemployee.SelectedValue.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('No employee selected!','Employee Not found','warning');", true);
            grd.EditIndex = -1;
            vBindingGrid();
            //btsave.CssClass = "btn btn-default ro";
            return;
        }
        else
        {
            grd.EditIndex = e.NewEditIndex;
            vBindingGrid();
            //btsave.CssClass = "btn btn-success";
        }
        cbemployee.CssClass = cd.csstextro;
        cbperiod.CssClass = cd.csstextro;
        cblevel.CssClass = cd.csstextro;
        cbjobtitle.CssClass = cd.csstextro;
    }
    protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grd.EditIndex = -1;
        vBindingGrid();
        cbemployee.CssClass = cd.csstext;
        cbperiod.CssClass = cd.csstext;
        cblevel.CssClass = cd.csstext;
        cbjobtitle.CssClass = cd.csstext;
    }
    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        double dOut = 0;
        TextBox txtarget = (TextBox)grd.Rows[e.RowIndex].FindControl("txtarget");
        HiddenField hdids = (HiddenField)grd.Rows[e.RowIndex].FindControl("hdids");
        if (!double.TryParse(txtarget.Text, out dOut))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Target must be numeric!','Check target given','true');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@emp_cd", cbemployee.SelectedValue.ToString()));
        arr.Add(new cArrayList("@IDS", hdids.Value.ToString()));
        arr.Add(new cArrayList("@period", cbperiod.SelectedValue.ToString()));
        arr.Add(new cArrayList("@qty", txtarget.Text));
        arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue));
        arr.Add(new cArrayList("@level_cd", cblevel.SelectedValue));
        arr.Add(new cArrayList("@target_desc", "-"));
        arr.Add(new cArrayList("@salespointcd",Convert.ToString(cbsalespoint.SelectedValue)));
        bll.vInsertHRDKPITarget(arr);
        grd.EditIndex = -1;
        vBindingGrid();
        cbemployee.CssClass = cd.csstext;
        cbperiod.CssClass = cd.csstext;
        cblevel.CssClass = cd.csstext;
        cbjobtitle.CssClass = cd.csstext;
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_kpitarget.aspx");
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        //vBindingGrdDtl();
    }

    
    
    protected void grd_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
    

   
    
    protected void cbsalespoint_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
        arr.Add(new cArrayList("@level_cd", cblevel.SelectedValue.ToString()));
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        //arr.Add(new cArrayList("@isTL", ckTL.Checked));
        arr.Add(new cArrayList("@period", cbperiod.SelectedValue));
        bll.vBindingComboToSp(ref cbemployee, "sp_tmst_employee_getActiveMerchandiser", "emp_cd", "emp_desc", arr);
    }

    protected void cbTL_CheckedChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@job_title_cd",  cbjobtitle.SelectedValue.ToString()));
        arr.Add(new cArrayList("@level_cd", cblevel.SelectedValue.ToString()));
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        //arr.Add(new cArrayList("@isTL", ckTL.Checked));
        arr.Add(new cArrayList("@period", cbperiod.SelectedValue));
        bll.vBindingComboToSp(ref cbemployee, "sp_tmst_employee_getActiveMerchandiser", "emp_cd", "emp_desc", arr);
    }
}