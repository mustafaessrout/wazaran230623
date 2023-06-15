using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppClassTools;

public partial class fm_salestargetsalespoint : System.Web.UI.Page
{
    cbll bll = new cbll();
    cdal cdl = new cdal();
    Utitlity ut = new Utitlity();
    AppClass app = new AppClass();
    //fm_returhoentry
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Visible = false;
        if (!IsPostBack)
        {
            try
            {
                BindControl();
                cbperiod_SelectedIndexChanged(sender, e);
                cbsalesman_SelectedIndexChanged(sender, e);
                cbprod_SelectedIndexChanged(sender, e);
            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_salestargetsalespoint");
                Response.Redirect("fm_ErrorPage.aspx");
            }
            //catch (Exception ex)
            //{
            //    app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            //    ut.Logs("", "Sales Target", "Sales Target Branch", "fm_salestargetsalespoint", "PageLoad", "Exception", ex.Message + ex.InnerException);
            //}
        }
    }

    void BindControl()
    {
        try
        {
            bll.vBindingComboToSp(ref cbperiod, "sp_tmst_period_get", "period_cd", "period_nm");
            string period = Convert.ToString(DateTime.Now.Year) + (Convert.ToString(DateTime.Now.Month).Length == 1 ? ("0" + Convert.ToString(DateTime.Now.Month)) : Convert.ToString(DateTime.Now.Month));
            cbperiod.SelectedValue = period;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@qry_cd", "SalesJob"));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));

            //bll.vBindingComboToSp(ref cbsalesman, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);

            DataTable dtDDL = new DataTable();
            dtDDL = cdl.GetValueFromSP("sp_tmst_employee_getbyqry", arr);

            if (dtDDL.Rows.Count > 0)
            {
                cbsalesman.DataValueField = "emp_cd";
                cbsalesman.DataTextField = "emp_desc";
                cbsalesman.DataSource = dtDDL.DefaultView;
                cbsalesman.DataBind();

                var SalesmanGroup = Convert.ToInt32(bll.vLookUp("select count(*) from tsalestargetsalespointSalesGroup where GroupSalesman in ( select SalesmanGroup  from temployee_info where emp_cd='" + Convert.ToString(dtDDL.Rows[0]["emp_cd"]) + "')"));
                List<cArrayList> arrSalesmanGroup = new List<cArrayList>();
                DataTable dtSalesmanGroup = new DataTable();
                arrSalesmanGroup.Add(new cArrayList("@empID", Convert.ToString(dtDDL.Rows[0]["emp_cd"])));
                dtSalesmanGroup = cdl.GetValueFromSP("sp_SalesmanGroup", arrSalesmanGroup);

                if (dtSalesmanGroup.Rows.Count > 0)
                {
                    lblGroupName.Text = Convert.ToString(dtSalesmanGroup.Rows[0][0]);
                }
                else { lblGroupName.Text = string.Empty; }

                //lblGroupName.Text = bll.vLookUp("select SalesmanGroup  from temployee_info where emp_cd='" + Convert.ToString(dtDDL.Rows[0]["emp_cd"]) + "'");

                if (SalesmanGroup > 0)
                {
                    txtUptoDateTarget.Enabled = false;
                    txtMinTarget.Enabled = false;
                    txtPriority.Text = "True";

                    txtUptoDateTarget.Text = bll.sGetControlParameter("tragetDateInMonth") + "/" + cbperiod.SelectedValue.Substring(cbperiod.SelectedValue.Length - 2) + '/' + cbperiod.SelectedValue.Substring(0, 4);
                }
                else
                {
                    txtUptoDateTarget.Enabled = false;
                    txtMinTarget.Enabled = false;
                    txtPriority.Text = "False";
                    txtUptoDateTarget.Text = "";
                }
            }



            arr.Clear();
            //arr.Add(new cArrayList("@level_no", 2));
            arr.Add(new cArrayList("@period", cbperiod.SelectedValue.ToString()));
            bll.vBindingComboToSp(ref cbprod, "sp_tmst_product_getbytarget", "prod_cd", "prod_desc", arr);
            // cbprod_SelectedIndexChanged(sender, e);
            txtMinTarget.Text = "0";
            //txtUptoDateTarget.Text = "";
            txqty.Text = "0";
            txtabqty.Text = "0";

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_salestargetsalespoint");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    void ClearControl(object sender, EventArgs e)
    {
        txtMinTarget.Text = "";
        txqty.Text = "";
        txtabqty.Text = "";
    }

    protected void btadd_Click(object sender, EventArgs e)
    {
        try
        {
            double dQty = 0; double dTab = 0;
            double dPriority = 0;
            txtargethoPnl.CssClass = "";
            txqtyPnl.CssClass = "";
            txtabqtyPnl.CssClass = "";


            if (txtargetho.Text == "")
            {
                app.BootstrapAlert(lblMsg, "HO has not setup the target for this product", app.alertType = AppClassTools.AppClass.BootstrapAlertType.Warning, true);
                txtargethoPnl.CssClass = "error";
                return;
            }
            else if (!double.TryParse(txqty.Text, out dQty))
            {
                app.BootstrapAlert(lblMsg, "Qty target must numeric", app.alertType = AppClassTools.AppClass.BootstrapAlertType.Warning, true);
                txqtyPnl.CssClass = "error";
                return;
            }
            else if (Convert.ToDecimal(txqty.Text) <= 0)
            {
                app.BootstrapAlert(lblMsg, "Sales target always greater than 0", app.alertType = AppClassTools.AppClass.BootstrapAlertType.Warning, true);
                txqtyPnl.CssClass = "error";
                return;
            }


            else if (!double.TryParse(txtabqty.Text, out dQty))
            {
                app.BootstrapAlert(lblMsg, "Qty target additional for tablet must numeric", app.alertType = AppClassTools.AppClass.BootstrapAlertType.Warning, true);
                txtabqtyPnl.CssClass = "error";
                return;
            }
            //else if (Convert.ToDecimal(txtabqty.Text) <= 0)
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Qty target additional   always greater than 0','QTY','warning');", true);
            //    return;
            //}

            else if (Convert.ToDouble(txtargetho.Text) < (Convert.ToDouble(txused.Text) + Convert.ToDouble(txqty.Text)))
            {
                app.BootstrapAlert(lblMsg, "Target is greater that HO", app.alertType = AppClassTools.AppClass.BootstrapAlertType.Warning, true);
                return;
            }
            else if (Convert.ToString(txtPriority.Text) == "True")
            {
                if (!double.TryParse(txtMinTarget.Text, out dPriority))
                {
                    app.BootstrapAlert(lblMsg, "Minimum  target must numeric", app.alertType = AppClassTools.AppClass.BootstrapAlertType.Warning, true);
                    return;
                }
                //else if (lblGroupName.Text != "VAN")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('For priority product salesman must be in Van group','Group','warning');", true);
                //    return;
                //}
                else if (Convert.ToDecimal(txtMinTarget.Text) <= 0)
                {
                    app.BootstrapAlert(lblMsg, "Minimum  target always greater than 0", app.alertType = AppClassTools.AppClass.BootstrapAlertType.Warning, true);
                    return;
                }
                else if (txtUptoDateTarget.Text.Length == 0)
                {
                    app.BootstrapAlert(lblMsg, "Assign date", app.alertType = AppClassTools.AppClass.BootstrapAlertType.Warning, true);
                    return;
                }
            }

            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@period", cbperiod.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salesman_cd", cbsalesman.SelectedValue.ToString()));
            arr.Add(new cArrayList("@prod_cd", cbprod.SelectedValue.ToString()));
            arr.Add(new cArrayList("@target_ho", Convert.ToDecimal(txtargetho.Text)));
            arr.Add(new cArrayList("@tabtarget", Convert.ToDecimal(txtabqty.Text)));
            //arr.Add(new cArrayList("@qty", Convert.ToDecimal(txqty.Text)));
            arr.Add(new cArrayList("@qty", txqty.Text));
            if (Convert.ToString(txtPriority.Text) == "True")
            {

                arr.Add(new cArrayList("@UptoDateTarget", null));
            }
            else { arr.Add(new cArrayList("@UptoDateTarget", null)); }
            arr.Add(new cArrayList("@MinTarget", (txtMinTarget.Text == "" ? 0 : Convert.ToDecimal(txtMinTarget.Text))));
            arr.Add(new cArrayList("@isPriority", Convert.ToBoolean(txtPriority.Text)));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            DataTable dt = new DataTable();

            //bll.vInsertTargetSalespoint(arr);
            dt = cdl.GetValueFromSP("sp_tsalestargetsalespoint_ins", arr);
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToString(dt.Rows[0]["StatusValue"]) == "Update")
                {
                    app.BootstrapAlert(lblMsg, "Data updated successfully", app.alertType = AppClassTools.AppClass.BootstrapAlertType.Success, true);
                    //SendEmail("UpdateRecord");
                    ClearControl(sender, e);
                    //cbperiod_SelectedIndexChanged(sender, e);
                    cbprod_SelectedIndexChanged(sender, e);
                }
                else if (Convert.ToString(dt.Rows[0]["StatusValue"]) == "Insert")
                {
                    app.BootstrapAlert(lblMsg, "Data saved successfully", app.alertType = AppClassTools.AppClass.BootstrapAlertType.Success, true);
                    //SendEmail("AddRecord");
                    ClearControl(sender, e);
                    //cbperiod_SelectedIndexChanged(sender, e);
                    cbprod_SelectedIndexChanged(sender, e);
                }
            }

            arr.Clear();
            arr.Add(new cArrayList("@period", cbperiod.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salesman_cd", cbsalesman.SelectedValue.ToString()));
            arr.Add(new cArrayList("@prod_cd",cbprod.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            //bll.vBindingGridToSp(ref grd, "sp_ttargetsalespoint_get", arr);
            bll.vBindingGridToSp(ref grd, "sp_tsalestargetBySalesPoint_get", arr);
            cbprod_SelectedIndexChanged(sender, e);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_salestargetsalespoint");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        //catch (Exception ex)
        //{
        //    app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
        //    ut.Logs("", "Sales Target", "Sales Target Branch", "fm_salestargetsalespoint", "btadd_Click", "Exception", ex.Message + ex.InnerException);
        //}
    }
    protected void cbprod_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //txtargetho.Text = bll.vLookUp("select dbo.fn_getsalestargetho('"+ cbprod.SelectedValue.ToString() +"','"+Request.Cookies["sp"].Value.ToString()+"','"+cbperiod.SelectedValue.ToString()+"')");
            List<cArrayList> arr = new List<cArrayList>();
            DataTable dt = new DataTable();

            arr.Add(new cArrayList("@prod_cd", Convert.ToString(cbprod.SelectedValue)));
            arr.Add(new cArrayList("@salespointcd", Convert.ToString(Request.Cookies["sp"].Value)));
            arr.Add(new cArrayList("@period", Convert.ToString(cbperiod.SelectedValue)));
            arr.Add(new cArrayList("@GroupSalesman", Convert.ToString(lblGroupName.Text)));
            dt = cdl.GetValueFromSP("sp_getsalestargetho", arr);

            //txtargetho.Text = bll.vLookUp("	select sum(qty)  from tsalestargetho where period='" + Convert.ToString(cbperiod.SelectedValue)
            //    + "' and prod_cd='" + Convert.ToString(cbprod.SelectedValue) + "' and salespointcd='" + Convert.ToString(Request.Cookies["sp"].Value) + "'").ToString();
            //txtMinTarget.Text = bll.vLookUp("	select miniLoadPerSalesman  from tsalestargetho where period='" + Convert.ToString(cbperiod.SelectedValue)
                //+ "' and prod_cd='" + Convert.ToString(cbprod.SelectedValue) + "' and salespointcd='" + Convert.ToString(Request.Cookies["sp"].Value) + "'").ToString();

            txtargetho.Text = bll.vLookUp("	select sum(qty)  from tsalestargetho where period='" + Convert.ToString(cbperiod.SelectedValue)
                + "' and prod_cd in (select prod_cd from tmst_product where level_no='2' and prod_nm='" + Convert.ToString(cbprod.SelectedValue) + "') and salespointcd='" + Convert.ToString(Request.Cookies["sp"].Value) + "'").ToString();
            txtMinTarget.Text = bll.vLookUp("	select sum(miniLoadPerSalesman)  from tsalestargetho where period='" + Convert.ToString(cbperiod.SelectedValue)
                + "' and prod_cd in (select prod_cd from tmst_product where level_no='2' and prod_nm='" + Convert.ToString(cbprod.SelectedValue) + "') and salespointcd='" + Convert.ToString(Request.Cookies["sp"].Value) + "'").ToString();

            if (dt.Rows.Count > 0)
            {
                //txtargetho.Text = Convert.ToString(dt.Rows[0]["qty"]);

                var results = from myRow in dt.AsEnumerable()
                              where myRow.Field<string>("prod_cd") == Convert.ToString(cbprod.SelectedValue)
                              select myRow;

                if (Convert.ToBoolean(results.FirstOrDefault()["isPriority"]) == true)
                {
                    txtUptoDateTarget.Enabled = false;
                    txtMinTarget.Enabled = false;
                    txtPriority.Text = "True";

                    txtUptoDateTarget.Text = bll.sGetControlParameter("tragetDateInMonth") + "/" + cbperiod.SelectedValue.Substring(cbperiod.SelectedValue.Length - 2) + '/' + cbperiod.SelectedValue.Substring(0, 4);
                }
                else
                {
                    txtUptoDateTarget.Enabled = false;
                    txtMinTarget.Enabled = false;
                    txtPriority.Text = "False";
                    txtUptoDateTarget.Text = "";
                }
            }
            else
            {
                txtUptoDateTarget.Enabled = false;
                txtMinTarget.Enabled = false;
                txtPriority.Text = "False";
            }

            txused.Text = bll.vLookUp("select dbo.fn_getsalestargetused('" + cbprod.SelectedValue.ToString() + "','" + cbperiod.SelectedValue.ToString() + "')").ToString();
            if (txused.Text != txtargetho.Text)
            {
                lbremark.Text = "Target HO and Usage in Salespoint is not the same";
            }
            else { lbremark.Text = "Target and Usage is MATCH"; }

            vBindingGridByProduct();
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_salestargetsalespoint");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        //catch (Exception ex)
        //{
        //    app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
        //    ut.Logs("", "Sales Target", "Sales Target Branch", "fm_salestargetsalespoint", "cbprod_SelectedIndexChanged", "Exception", ex.Message + ex.InnerException);
        //}

    }
    protected void cbperiod_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            vBindingGrid();
            arr.Add(new cArrayList("@period", cbperiod.SelectedValue.ToString()));
            bll.vBindingComboToSp(ref cbprod, "sp_tmst_product_getbytarget", "prod_cd", "prod_desc", arr);
            cbprod_SelectedIndexChanged(sender, e);
            // cbsalesman_SelectedIndexChanged(sender, e);
            // cbprod_SelectedIndexChanged(sender, e);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_salestargetsalespoint");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        //catch (Exception ex)
        //{
        //    app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
        //    ut.Logs("", "Sales Target", "Sales Target Branch", "fm_salestargetsalespoint", "cbperiod_SelectedIndexChanged", "Exception", ex.Message + ex.InnerException);
        //}
    }

    void vBindingGrid()
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@period", cbperiod.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salesman_cd", cbsalesman.SelectedValue.ToString()));
            arr.Add(new cArrayList("@prod_cd", cbprod.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_tsalestargetBySalesPoint_get", arr);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_salestargetsalespoint");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        //catch (Exception ex)
        //{
        //    app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
        //    ut.Logs("", "Sales Target", "Sales Target Branch", "fm_salestargetsalespoint", "vBindingGrid", "Exception", ex.Message + ex.InnerException);
        //}
    }

    void vBindingGridByProduct()
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@period", cbperiod.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salesman_cd", null));
            arr.Add(new cArrayList("@prod_cd", cbprod.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_tsalestargetBySalesPointByProduct_get", arr);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_salestargetsalespoint");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        //catch (Exception ex)
        //{
        //    app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
        //    ut.Logs("", "Sales Target", "Sales Target Branch", "fm_salestargetsalespoint", "vBindingGrid", "Exception", ex.Message + ex.InnerException);
        //}
    }
    void vBindingGridBySalesMan()
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@period", cbperiod.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salesman_cd", cbsalesman.SelectedValue.ToString()));
            arr.Add(new cArrayList("@prod_cd", null));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_tsalestargetBySalesPoint_get", arr);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_salestargetsalespoint");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        //catch (Exception ex)
        //{
        //    app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
        //    ut.Logs("", "Sales Target", "Sales Target Branch", "fm_salestargetsalespoint", "vBindingGrid", "Exception", ex.Message + ex.InnerException);
        //}
    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            grd.EditIndex = e.NewEditIndex;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@period", cbperiod.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salesman_cd", null));
            arr.Add(new cArrayList("@prod_cd", cbprod.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_tsalestargetBySalesPoint_get", arr);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_salestargetsalespoint");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        //catch (Exception ex)
        //{
        //    app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
        //    ut.Logs("", "Sales Target", "Sales Target Branch", "fm_salestargetsalespoint", "grd_RowEditing", "Exception", ex.Message + ex.InnerException);
        //}
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "editTable", "editTable();", true);
    }
    protected void cbsalesman_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //vBindingGrid();
            //vBindingGridBySalesMan();
            List<cArrayList> arr = new List<cArrayList>();
            DataTable dtSalesmanGroup = new DataTable();
            arr.Add(new cArrayList("@empID", Convert.ToString(cbsalesman.SelectedValue)));
            dtSalesmanGroup = cdl.GetValueFromSP("sp_SalesmanGroup", arr);

            if (dtSalesmanGroup.Rows.Count > 0)
            {
                lblGroupName.Text = Convert.ToString(dtSalesmanGroup.Rows[0][0]);
            }
            else { lblGroupName.Text = string.Empty; }
            //lblGroupName.Text = bll.vLookUp("select SalesmanGroup  from temployee_info where emp_cd='" + Convert.ToString(cbsalesman.SelectedValue) + "'");
            cbprod_SelectedIndexChanged(sender, e);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_salestargetsalespoint");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        //catch (Exception ex)
        //{
        //    app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
        //    ut.Logs("", "Sales Target", "Sales Target Branch", "fm_salestargetsalespoint", "cbsalesman_SelectedIndexChanged", "Exception", ex.Message + ex.InnerException);
        //}
    }
    protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            grd.EditIndex = -1;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@period", cbperiod.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salesman_cd", null));
            arr.Add(new cArrayList("@prod_cd", cbprod.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_tsalestargetBySalesPoint_get", arr);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_salestargetsalespoint");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        //catch (Exception ex)
        //{
        //    app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
        //    ut.Logs("", "Sales Target", "Sales Target Branch", "fm_salestargetsalespoint", "grd_RowCancelingEdit", "Exception", ex.Message + ex.InnerException);
        //}
    }
    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            HiddenField hdprodcd = (HiddenField)grd.Rows[e.RowIndex].FindControl("hdprodcd");
            HiddenField hdfGroup = (HiddenField)grd.Rows[e.RowIndex].FindControl("hdfGroup");
            TextBox txqty = (TextBox)grd.Rows[e.RowIndex].FindControl("txqty");
            HiddenField hdfQty = (HiddenField)grd.Rows[e.RowIndex].FindControl("hdfQty");
            TextBox txtabqty = (TextBox)grd.Rows[e.RowIndex].FindControl("txtabtarget");
            TextBox txtMinTarget = (TextBox)grd.Rows[e.RowIndex].FindControl("txMintarget");
            HiddenField hdSalesmanCd = (HiddenField)grd.Rows[e.RowIndex].FindControl("hdSalesmanCd");
            Label txUptoDateTarget = (Label)grd.Rows[e.RowIndex].FindControl("txUptoDateTarget");
            Label lbhotarget = (Label)grd.Rows[e.RowIndex].FindControl("lbhotarget");

            decimal qty = (txqty.Text == "" ? 0 : Convert.ToDecimal(txqty.Text));

            double dQty = 0; double dTab = 0;
            double dPriority = 0;
            if (txtargetho.Text == "")
            {
                app.BootstrapAlert(lblMsg, "HO has not setup the target for this product", app.alertType = AppClassTools.AppClass.BootstrapAlertType.Warning, true);
                return;
            }

            else if (!double.TryParse(txqty.Text, out dQty))
            {
                app.BootstrapAlert(lblMsg, "Qty target must numeric", app.alertType = AppClassTools.AppClass.BootstrapAlertType.Warning, true);
                return;
            }
            else if (Convert.ToDecimal(txqty.Text) <= 0)
            {
                app.BootstrapAlert(lblMsg, "Sales target always greater than 0", app.alertType = AppClassTools.AppClass.BootstrapAlertType.Warning, true);
                return;
            }

            else if (!double.TryParse(txtabqty.Text, out dQty))
            {
                app.BootstrapAlert(lblMsg, "Qty target additional for tablet must numeric", app.alertType = AppClassTools.AppClass.BootstrapAlertType.Warning, true);
                return;
            }
            //else if (Convert.ToDecimal(txtabqty.Text) <= 0)
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Qty target additional   always greater than 0','QTY','warning');", true);
            //    return;
            //}
            else if (Convert.ToString(txtPriority.Text) == "True")
            {
                if (!double.TryParse(txtMinTarget.Text, out dPriority))
                {
                    app.BootstrapAlert(lblMsg, "Minimum  target must numeric", app.alertType = AppClassTools.AppClass.BootstrapAlertType.Warning, true);
                    return;
                }
                //else if (Convert.ToString(hdfGroup.Value) != "VAN")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('For priority product salesman must be in Van group','Group','warning');", true);
                //    return;
                //}
                else if (Convert.ToDecimal(txtMinTarget.Text) <= 0)
                {
                    app.BootstrapAlert(lblMsg, "Minimum  target always greater than 0", app.alertType = AppClassTools.AppClass.BootstrapAlertType.Warning, true);
                    return;
                }
                else if (txtUptoDateTarget.Text.Length == 0)
                {
                    app.BootstrapAlert(lblMsg, "Assign date", app.alertType = AppClassTools.AppClass.BootstrapAlertType.Warning, true);
                    return;
                }
            }

            List<cArrayList> arr = new List<cArrayList>();
            arr.Clear();
            arr.Add(new cArrayList("@period", cbperiod.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salesman_cd", null));
            arr.Add(new cArrayList("@prod_cd", cbprod.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            DataTable validateDt = new DataTable();
            validateDt = cdl.GetValueFromSP("sp_tsalestargetBySalesPoint_get", arr);

            string[] excludeStatus = { hdSalesmanCd.Value.ToString() };
            var filteredRows = validateDt.AsEnumerable()
                .Where(row => !excludeStatus.Contains(row.Field<string>("salesman_cd")));
            decimal alreadyHave = 0;
            foreach (DataRow dr in filteredRows.ToList())
            {
                alreadyHave += Convert.ToDecimal(dr["qty"]);
            }
            // alreadyHave = Convert.ToDecimal(validateDt.Compute("Sum(qty)", ""));

            var currentQty = Convert.ToDecimal(lbhotarget.Text) - alreadyHave - qty;
            if (currentQty >= 0)
            {
                arr.Clear();
                arr.Add(new cArrayList("@period", cbperiod.SelectedValue.ToString()));
                arr.Add(new cArrayList("@salesman_cd", Convert.ToString(hdSalesmanCd.Value)));
                arr.Add(new cArrayList("@prod_cd", hdprodcd.Value.ToString()));
                arr.Add(new cArrayList("@qty", qty));
                arr.Add(new cArrayList("@target_ho", (txtargetho.Text == "" ? 0 : Convert.ToDecimal(txtargetho.Text))));
                arr.Add(new cArrayList("@tabtarget", (txtabqty.Text == "" ? 0 : Convert.ToDecimal(txtabqty.Text))));
                arr.Add(new cArrayList("@MinTarget", (txtMinTarget.Text == "" ? 0 : Convert.ToDecimal(txtMinTarget.Text))));
                if (txUptoDateTarget.Text == "")
                {
                    arr.Add(new cArrayList("@UptoDateTarget", null));
                }
                else
                {
                    arr.Add(new cArrayList("@UptoDateTarget", DateTime.Now.Date));
                }
                //bll.vUpdateSalesTargetSalespoint(arr);


                // validate qty not more than ho


                DataTable dt = new DataTable();
                dt = cdl.GetValueFromSP("sp_tsalestargetsalespoint_upd", arr);
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToString(dt.Rows[0]["StatusValue"]) == "Update")
                    {
                        //SendEmail("UpdateRecordByGrid", hdprodcd.Value.ToString());
                    }
                    //else if (Convert.ToString(dt.Rows[0]["StatusValue"]) == "Insert")
                    //{
                    //    SendEmail("AddRecord");
                    //}
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Qty is greater than HO target','error');", true);
            }

            grd.EditIndex = -1;
            arr.Clear();
            arr.Add(new cArrayList("@period", cbperiod.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salesman_cd", null));
            arr.Add(new cArrayList("@prod_cd", cbprod.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_tsalestargetBySalesPoint_get", arr);
            cbprod_SelectedIndexChanged(sender, e);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_salestargetsalespoint");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        //catch (Exception ex)
        //{
        //    app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
        //    ut.Logs("", "Sales Target", "Sales Target Branch", "fm_salestargetsalespoint", "grd_RowUpdating", "Exception", ex.Message + ex.InnerException);
        //}
    }


    private void SendHtmlFormattedEmail(string recepientEmail, string subject, string body)
    {
        try
        {
            using (MailMessage mailMessage = new MailMessage())
            {
                //mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["UserName"]);
                //mailMessage.Subject = subject;
                //mailMessage.Body = body;
                //mailMessage.IsBodyHtml = true;
                //mailMessage.To.Add(new MailAddress(recepientEmail));
                //SmtpClient smtp = new SmtpClient();
                //smtp.Host = ConfigurationManager.AppSettings["Host"];
                ////smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
                //System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                //NetworkCred.UserName = ConfigurationManager.AppSettings["UserName"];
                //NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
                //smtp.UseDefaultCredentials = true;
                //smtp.Credentials = NetworkCred;
                //smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
                //smtp.Send(mailMessage);

                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@emailsubject", subject));
                arr.Add(new cArrayList("@msg", body));
                arr.Add(new cArrayList("@file_attachment", null));
                arr.Add(new cArrayList("@to", recepientEmail));
                arr.Add(new cArrayList("@token", null));
                arr.Add(new cArrayList("@doc_no", null));
                arr.Add(new cArrayList("@doc_typ", "Salespoint Target to Salesman"));

                DataTable dt = new DataTable();
                //dt = cdl.GetValueFromSP("sp_temail_outbox_ins", arr);
                if (dt.Rows.Count > 0) { }
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_salestargetsalespoint");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        //catch (Exception ex)
        //{
        //    app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
        //    ut.Logs("", "Sales Target", "Sales Target Branch", "fm_salestargetsalespoint", "SendHtmlFormattedEmail", "Exception", ex.Message + ex.InnerException);
        //}
    }

    protected void SendEmail(string action, string prodID)
    {
        try
        {
            string body = string.Empty;
            bool isSendMail = false;
            using (StreamReader reader = new StreamReader(Server.MapPath("SalespointTargetToSalesmanUpdate.html")))
            {
                body = reader.ReadToEnd();
            }

            var branchName = bll.vLookUp("select salespointcd+'-'+salespoint_nm from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            List<cArrayList> arr = new List<cArrayList>();

            arr.Add(new cArrayList("@period", cbperiod.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salesman_cd", cbsalesman.SelectedValue.ToString()));
            arr.Add(new cArrayList("@prod_cd", cbprod.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));

            DataTable dt = new DataTable();

            dt = cdl.GetValueFromSP("sp_tsalestargetBySalesPoint_get", arr);



            if (dt.Rows.Count > 0)
            {
                var results = from myRow in dt.AsEnumerable()
                              where myRow.Field<string>("prod_cd") == prodID
                              select myRow;
                isSendMail = Convert.ToBoolean(results.FirstOrDefault()["isPriority"]);
                if (isSendMail != false)
                {

                    var alreadyUsed = bll.vLookUp("select dbo.fn_getsalestargetused('" + cbprod.SelectedValue.ToString() + "','" + cbperiod.SelectedValue.ToString() + "')").ToString();
                    var stock = bll.vLookUp(" select sum(stkbalance)stock from tblstock " +
                      "inner join tmst_item on tblstock.item_cd=tmst_item.item_cd " +
                      "where monthcd='" + cbperiod.SelectedValue.ToString() + "' and prod_cd='" + cbprod.SelectedValue.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "' ").ToString();

                    decimal stockvalue = (stock == "" ? 0 : Convert.ToDecimal(stock)) - Convert.ToDecimal(results.FirstOrDefault()["qty"]) - Convert.ToDecimal(results.FirstOrDefault()["tabtarget"]);

                    body = body.Replace("{UserName}", Convert.ToString(ConfigurationManager.AppSettings["sendToName"]));
                    body = body.Replace("{Period}", Convert.ToString(cbperiod.SelectedItem.Text));
                    body = body.Replace("{Salesman}", Convert.ToString(cbsalesman.SelectedItem.Text));
                    body = body.Replace("{BranchName}", branchName);

                    body = body.Replace("{ProductGroup}", Convert.ToString(results.FirstOrDefault()["prod_nm"]));

                    body = body.Replace("{HeadfficeTarget}", Convert.ToString(results.FirstOrDefault()["prod_nm"]));
                    body = body.Replace("{Priority}", Convert.ToString(results.FirstOrDefault()["isPriority"]));
                    body = body.Replace("{MinTarget}", Convert.ToString(results.FirstOrDefault()["MinTarget"]));
                    body = body.Replace("{UptoDate}", Convert.ToString(Convert.ToDateTime(results.FirstOrDefault()["UptoDateTarget"]).ToString("dd-MMM-yyyy")));
                    body = body.Replace("{AlreadyUsed}", Convert.ToString(alreadyUsed));
                    body = body.Replace("{SalesTarget}", Convert.ToString(results.FirstOrDefault()["qty"]));
                    body = body.Replace("{TabletAdd}", Convert.ToString(results.FirstOrDefault()["tabtarget"]));
                    body = body.Replace("{stock}", Convert.ToString(stockvalue));
                }
            }

            if (isSendMail != false)
            {
                if (action == "UpdateRecordByGrid")
                {
                    DataTable dtEmail = new DataTable();
                    var salesPointCD = Request.Cookies["sp"].Value.ToString();
                    List<cArrayList> arrEmail = new List<cArrayList>();
                    arrEmail.Add(new cArrayList("@salespointcd", salesPointCD));
                    dtEmail = cdl.GetValueFromSP("sp_BranchSupervisor_get", arrEmail);
                    if (dtEmail.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtEmail.Rows)
                        {
                            this.SendHtmlFormattedEmail(Convert.ToString(dr["email"]), "Product group data updated!", body);
                        }
                        this.SendHtmlFormattedEmail(("hani@sbtcgroup.com"), "Product group data updated!", body);

                    }
                }
            }

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_salestargetsalespoint");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        //catch (Exception ex)
        //{
        //    app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
        //    ut.Logs("", "Sales Target", "Sales Target Branch", "fm_salestargetsalespoint", "SendEmail", "Exception", ex.Message + ex.InnerException);
        //}
    }
    protected void SendEmail(string action)
    {
        try
        {
            string body = this.PopulateBody(Convert.ToString(ConfigurationManager.AppSettings["sendToName"]), action);
            if (action == "AddRecord")
            {
                DataTable dtEmail = new DataTable();
                var salesPointCD = Request.Cookies["sp"].Value.ToString();
                List<cArrayList> arrEmail = new List<cArrayList>();
                arrEmail.Add(new cArrayList("@salespointcd", salesPointCD));
                dtEmail = cdl.GetValueFromSP("sp_BranchSupervisor_get", arrEmail);
                if (dtEmail.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtEmail.Rows)
                    {
                        this.SendHtmlFormattedEmail(Convert.ToString(dr["email"]), "New product data created!", body);
                    }
                    this.SendHtmlFormattedEmail(("hani@sbtcgroup.com"), "New product data created!", body);
                }

                //this.SendHtmlFormattedEmail(Convert.ToString(ConfigurationManager.AppSettings["sendTo"]), "New product data created!", body);
            }
            else if (action == "UpdateRecord")
            {
                DataTable dtEmail = new DataTable();
                var salesPointCD = Request.Cookies["sp"].Value.ToString();
                List<cArrayList> arrEmail = new List<cArrayList>();
                arrEmail.Add(new cArrayList("@salespointcd", salesPointCD));
                dtEmail = cdl.GetValueFromSP("sp_BranchSupervisor_get", arrEmail);
                if (dtEmail.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtEmail.Rows)
                    {
                        this.SendHtmlFormattedEmail(Convert.ToString(dr["email"]), "Product group data updated!", body);
                    }
                    this.SendHtmlFormattedEmail(("hani@sbtcgroup.com"), "Product group data updated!", body);
                }
                //this.SendHtmlFormattedEmail(Convert.ToString(ConfigurationManager.AppSettings["sendTo"]), "Product group data updated!", body);
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_salestargetsalespoint");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        //catch (Exception ex)
        //{
        //    app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
        //    ut.Logs("", "Sales Target", "Sales Target Branch", "fm_salestargetsalespoint", "SendEmail", "Exception", ex.Message + ex.InnerException);
        //}
    }


    private string PopulateBody(string UserName, string action)
    {
        string body = string.Empty;
        try
        {

            if (action == "AddRecord")
            {
                using (StreamReader reader = new StreamReader(Server.MapPath("SalespointTargetToSalesmanAdd.html")))
                {
                    body = reader.ReadToEnd();
                }
            }
            else if (action == "UpdateRecord")
            {
                using (StreamReader reader = new StreamReader(Server.MapPath("SalespointTargetToSalesmanUpdate.html")))
                {
                    body = reader.ReadToEnd();
                }
            }
            var branchName = bll.vLookUp("select salespointcd+'-'+salespoint_nm from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            var stock = bll.vLookUp(" select sum(stkbalance)stock from tblstock " +
                  "inner join tmst_item on tblstock.item_cd=tmst_item.item_cd " +
                  "where monthcd='" + cbperiod.SelectedValue.ToString() + "' and prod_cd='" + cbprod.SelectedValue.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "' ").ToString();

            decimal stockvalue = (stock == "" ? 0 : Convert.ToDecimal(stock)) - (txqty.Text == "" ? 0 : Convert.ToDecimal(txqty.Text)) - Convert.ToDecimal(txtabqty.Text);


            body = body.Replace("{UserName}", UserName);
            body = body.Replace("{Period}", Convert.ToString(cbperiod.SelectedItem.Text));
            body = body.Replace("{Salesman}", Convert.ToString(cbsalesman.SelectedItem.Text));
            body = body.Replace("{BranchName}", branchName);

            body = body.Replace("{ProductGroup}", Convert.ToString(cbprod.SelectedItem.Text));

            body = body.Replace("{HeadfficeTarget}", Convert.ToString(txtargetho.Text));
            body = body.Replace("{Priority}", Convert.ToString(txtPriority.Text));
            body = body.Replace("{MinTarget}", Convert.ToString(txtMinTarget.Text));
            body = body.Replace("{UptoDate}", Convert.ToString(bll.sGetControlParameter("tragetDateInMonth") + "/" + cbperiod.SelectedValue.Substring(cbperiod.SelectedValue.Length - 2) + '/' + cbperiod.SelectedValue.Substring(0, 4)));
            body = body.Replace("{AlreadyUsed}", Convert.ToString(txused.Text));
            body = body.Replace("{SalesTarget}", Convert.ToString(txqty.Text));
            body = body.Replace("{TabletAdd}", Convert.ToString(txtabqty.Text));
            body = body.Replace("{stock}", Convert.ToString(stockvalue));
            
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_salestargetsalespoint");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        //catch (Exception ex)
        //{
        //    app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
        //    ut.Logs("", "Sales Target", "Sales Target Branch", "fm_salestargetsalespoint", "PopulateBody", "Exception", ex.Message + ex.InnerException);
        //    return body;
        //}
        return body;
    }

    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=saltar&scd=" + cbsalesman.SelectedValue.ToString() + "&period=" + cbperiod.SelectedValue.ToString() + "');", true);
    }
    protected void btprintho_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opp", "openreport('fm_report2.aspx?src=spall&prd=" + cbperiod.SelectedValue.ToString() + "');", true);
    }
    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txMintarget = (TextBox)e.Row.FindControl("txMintarget");
                Label lbUptoDateTarget = (Label)e.Row.FindControl("lbUptoDateTarget");
                Label txUptoDateTarget = (Label)e.Row.FindControl("txUptoDateTarget");
                TextBox txqty = (TextBox)e.Row.FindControl("txqty");
                Label lbhotarget = (Label)e.Row.FindControl("lbhotarget");
                HiddenField hdfIsPriority = (HiddenField)e.Row.FindControl("hdfIsPriority");
                
                if (hdfIsPriority.Value == "True")
                {
                    if ((txMintarget != null))
                    {
                        txMintarget.ReadOnly = false;
                    }
                    if ((lbhotarget != null))
                    {
                        if (hdfIsPriority.Value != "False")
                        {
                            if (txUptoDateTarget != null)
                            {
                                txUptoDateTarget.Text = Convert.ToString(bll.sGetControlParameter("tragetDateInMonth") + "/" + cbperiod.SelectedValue.Substring(cbperiod.SelectedValue.Length - 2) + '/' + cbperiod.SelectedValue.Substring(0, 4));
                            }
                            
                                
                                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#E56E94'");
                                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='orange'");
                                e.Row.BackColor = Color.FromName("orange");

                                if (lbUptoDateTarget != null)
                                {
                                    lbUptoDateTarget.Text = Convert.ToString(bll.sGetControlParameter("tragetDateInMonth") + "/" + cbperiod.SelectedValue.Substring(cbperiod.SelectedValue.Length - 2) + '/' + cbperiod.SelectedValue.Substring(0, 4));
                                }
                        }
                        else if (hdfIsPriority.Value == "False")
                        {
                            if (txUptoDateTarget != null)
                            {
                                txUptoDateTarget.Text = string.Empty;
                            }
                            lbUptoDateTarget.Text = string.Empty;
                        }
                    }


                }
                else if (hdfIsPriority.Value == "False")
                {
                    if ((txMintarget != null))
                    {
                        txMintarget.ReadOnly = true;
                    }
                    if (hdfIsPriority.Value != "False")
                    {
                        if (txUptoDateTarget != null)
                        {
                            txUptoDateTarget.Text = Convert.ToString(bll.sGetControlParameter("tragetDateInMonth") + "/" + cbperiod.SelectedValue.Substring(cbperiod.SelectedValue.Length - 2) + '/' + cbperiod.SelectedValue.Substring(0, 4));
                        }

                        e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#E56E94'");
                        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='orange'");
                        e.Row.BackColor = Color.FromName("orange");
                        if (lbUptoDateTarget != null)
                        {
                            lbUptoDateTarget.Text = Convert.ToString(bll.sGetControlParameter("tragetDateInMonth") + "/" + cbperiod.SelectedValue.Substring(cbperiod.SelectedValue.Length - 2) + '/' + cbperiod.SelectedValue.Substring(0, 4));
                        }
                    }
                    else if (hdfIsPriority.Value == "False")
                    {
                        if (txUptoDateTarget != null)
                        {
                            txUptoDateTarget.Text = string.Empty;
                        }
                    }
                    if ((txqty != null))
                    {


                    }
                }
                if (txMintarget != null)
                {
                    txMintarget.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_salestargetsalespoint");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        //catch (Exception ex)
        //{
        //    app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
        //    ut.Logs("", "Sales Target", "Sales Target Branch", "grd_RowDataBound", "PopulateBody", "Exception", ex.Message + ex.InnerException);
        //}
    }
}