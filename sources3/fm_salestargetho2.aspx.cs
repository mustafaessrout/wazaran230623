using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppClassTools;

public partial class fm_salestargetho2 : System.Web.UI.Page
{
    cbll bll = new cbll();
    Utitlity ut = new Utitlity();
    cdal cdl = new cdal();
    AppClass app = new AppClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Visible = false;
        if (!IsPostBack)
        {
            try { 
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@level_no", 3));
               // bll.vBindingGridToSp(ref grd, "sp_tmst_product_get", arr);
                bll.vBindingComboToSp(ref cbperiod, "sp_tmst_period_get","period_cd","period_nm");
                bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_get", "salespointcd", "salespoint_desc");
                bll.vBindingComboToSp(ref cbdistri, "sp_tmst_vendor_get", "vendor_cd", "vendor_nm");
                //bll.vBindingFieldValueToCombo(ref cbdistri, "distributor");
                string period = Convert.ToString(DateTime.Now.Year) + (Convert.ToString(DateTime.Now.Month).Length == 1 ? ("0" + Convert.ToString(DateTime.Now.Month)) : Convert.ToString(DateTime.Now.Month));
                cbperiod.SelectedValue = period;
                //cbsalespoint.SelectedValue = "101";
                cblevel.SelectedValue = "2";
                cblevel_SelectedIndexChanged(sender, e);
                btsearch.Visible = false;
                cblevel.Enabled = false;

            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_salestargetho2");
                Response.Redirect("fm_ErrorPage.aspx");
            }
            //catch (Exception ex)
            //{
            //    app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            //    ut.Logs("", "Sales Target", "Sales Target Head Office", "fm_salestargetho2", "PageLoad", "Exception", ex.Message + ex.InnerException);
            //    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('"+ex.Message+"','warning');", true);
            //    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "vDisableShow();", true);
            //    //return;
            //}
        }
    }
    protected void cblevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@level_no", cblevel.SelectedValue.ToString()));
            arr.Add(new cArrayList("@distrib_cd", cbdistri.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_tmst_product_getbydistrib", arr);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_salestargetho2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        //catch (Exception ex)
        //{
        //    app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
        //    ut.Logs("", "Sales Target", "Sales Target Head Office", "fm_salestargetho2", "cblevel_SelectedIndexChanged", "Exception", ex.Message + ex.InnerException);
        //}
    }
    protected void cbdistri_SelectedIndexChanged(object sender, EventArgs e)
    {
        cblevel_SelectedIndexChanged(sender, e);
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            bool isVaidData = true;
            string prodName = string.Empty;
            string minimumLoad = string.Empty;
            string minimumLoadQty = string.Empty;
            string assignGroup = string.Empty;

            foreach (GridViewRow row in grd.Rows)
            {
                HiddenField lbprodcd = (HiddenField)row.FindControl("hdprodcd");
                TextBox txqty = (TextBox)row.FindControl("txqty");
                TextBox txMinimumLoading = (TextBox)row.FindControl("txMinimumLoading");
                CheckBox chkIsPriority = (CheckBox)row.FindControl("chkIsPriority");
                Label lblAssignGroup = (Label)row.FindControl("lblAssignGroup");
                Label lbprodname = (Label)row.FindControl("lbprodname");
                if (chkIsPriority.Checked == true && lblAssignGroup.Text == "")
                {
                    assignGroup += lbprodname.Text + "<br>";
                    
                    isVaidData = false;
                }

                if (chkIsPriority.Checked == true && (txMinimumLoading.Text == "" || txMinimumLoading.Text == "0"))
                {
                    //app.BootstrapAlert(lblMsg, "Please insert minimum load", app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
                    minimumLoad += lbprodname.Text + "<br>";
                    isVaidData = false;
                }
                else if (chkIsPriority.Checked == true && (Convert.ToDecimal(txMinimumLoading.Text) > Convert.ToDecimal(txqty.Text)))
                {
                    minimumLoadQty += lbprodname.Text + "<br>";
                    isVaidData = false;
                    //app.BootstrapAlert(lblMsg, "Minimum load should less than Qty", app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
                }
            }
            if (!isVaidData) {
                string msg = string.Empty;
                if (minimumLoad != "") {
                    msg += "Please insert minimum load " + minimumLoad ;
                }
                 if (minimumLoadQty != "") 
                {
                    msg += "Minimum load should less than Qty " + minimumLoadQty;
                }
                 if (assignGroup != "") 
                {
                    msg += "Please select group for " + assignGroup;
                }
                app.BootstrapAlert(lblMsg, msg, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
                //app.BootstrapAlert(lblMsg, "Please select group for " + prodName.Remove(prodName.Length - 1) , app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Please select group for " + prodName.Remove(prodName.Length - 1) + "','error');", true); 
            }
            if (isVaidData)
            {
                foreach (GridViewRow row in grd.Rows)
                {
                    HiddenField lbprodcd = (HiddenField)row.FindControl("hdprodcd");
                    TextBox txqty = (TextBox)row.FindControl("txqty");
                    CheckBox chkIsPriority = (CheckBox)row.FindControl("chkIsPriority");
                    TextBox txMinimumLoading = (TextBox)row.FindControl("txMinimumLoading");
                    if (txqty.Text == "")
                    {
                        txqty.Text = "0";
                    }
                    if (txqty.Text != "0")
                    {
                        arr.Clear();
                        int minimumLoading = 0;
                        if (txMinimumLoading.Text == "") {
                            minimumLoading = 0;
                        }
                        else {
                            minimumLoading = Convert.ToInt32(txMinimumLoading.Text);
                        }
                        arr.Add(new cArrayList("@period", cbperiod.SelectedValue.ToString()));
                        arr.Add(new cArrayList("@prod_cd", lbprodcd.Value.ToString()));
                        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
                        arr.Add(new cArrayList("@qty", txqty.Text));
                        arr.Add(new cArrayList("@uom", "CTN"));
                        arr.Add(new cArrayList("@entryby", Request.Cookies["usr_id"].Value.ToString()));
                        arr.Add(new cArrayList("@miniLoadPerSalesman", minimumLoading));
                        arr.Add(new cArrayList("@IsPriority", chkIsPriority.Checked));
                        bll.vInsertSalesTargetHO(arr);
                    }
                }

                List<cArrayList> arr1 = new List<cArrayList>();
                arr1.Add(new cArrayList("@level_no", cblevel.SelectedValue.ToString()));
                arr1.Add(new cArrayList("@distrib_cd", cbdistri.SelectedValue.ToString()));
                //arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
                bll.vBindingGridToSp(ref grd, "sp_tmst_product_getbydistrib", arr1);
                app.BootstrapAlert(lblMsg, "Target has been save successfully", app.alertType = AppClassTools.AppClass.BootstrapAlertType.Success, true);
                
            }

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_salestargetho2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        //catch (Exception ex)
        //{
        //    if (ex.Message.Contains("duplicate") == true) {
        //        app.BootstrapAlert(lblMsg, "Duplicate records found", app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
        //    }
        //    else { app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true); }

        //    ut.Logs("", "Sales Target", "Sales Target Head Office", "fm_salestargetho2", "btsave_Click", "Exception", ex.Message + ex.InnerException);
        //}
    }

    protected void cbsalespoint_SelectedIndexChanged(object sender, EventArgs e)
    {
        cblevel_SelectedIndexChanged(sender, e);
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try { 
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@level_no", cblevel.SelectedValue.ToString()));
        arr.Add(new cArrayList("@distrib_cd", cbdistri.SelectedValue.ToString()));
        //arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tmst_product_getbydistrib", arr);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_salestargetho2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        //catch (Exception ex)
        //{
        //    app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
        //    ut.Logs("", "Sales Target", "Sales Target Head Office", "fm_salestargetho2", "btnUpdate_Click", "Exception", ex.Message + ex.InnerException);
        //}
    }
    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdprodcd = (HiddenField)e.Row.FindControl("hdprodcd");
                TextBox txqty = (TextBox)e.Row.FindControl("txqty");
                LinkButton lnk_productid = (LinkButton)e.Row.FindControl("lnk_productid");
                TextBox txMinimumLoading = (TextBox)e.Row.FindControl("txMinimumLoading");
                Label lblAssignGroup = (Label)e.Row.FindControl("lblAssignGroup");
                CheckBox chkIsPriority = (CheckBox)e.Row.FindControl("chkIsPriority");
                chkIsPriority.CheckedChanged += new EventHandler(OnChangeHandler);
                List<cArrayList> arr = new List<cArrayList>();
                ////txqty.Text = bll.vLookUp("select qty  from tsalestargetho where period='" + cbperiod.SelectedValue.ToString() + "' and "
                ////   + " prod_cd='" + hdprodcd.Value.ToString() + "' and salespointcd='" + cbsalespoint.SelectedValue.ToString() + "'");

                //var isPriority = bll.vLookUp("select isPriority from tsalestargetho where period='" + cbperiod.SelectedValue.ToString() + "' and "
                //  + " prod_cd='" + hdprodcd.Value.ToString() + "' and salespointcd='" + cbsalespoint.SelectedValue.ToString() + "'");

                //var miniLoad = bll.vLookUp("select miniLoadPerSalesman from tsalestargetho where period='" + cbperiod.SelectedValue.ToString() + "' and "
                //  + " prod_cd='" + hdprodcd.Value.ToString() + "' and salespointcd='" + cbsalespoint.SelectedValue.ToString() + "'");

                txqty.Text = bll.vLookUp("select sum(qty)  from tsalestargetho where period='" + cbperiod.SelectedValue.ToString() + "' and "
                + " prod_cd in (select prod_cd from tmst_product where level_no='2' and prod_nm='" + hdprodcd.Value.ToString() + "') and salespointcd='" + cbsalespoint.SelectedValue.ToString() + "'");

                var isPriority = bll.vLookUp("select distinct isPriority from tsalestargetho where period='" + cbperiod.SelectedValue.ToString() + "' and "
                  + " prod_cd in (select prod_cd from tmst_product where level_no='2' and prod_nm='" + hdprodcd.Value.ToString() + "') and salespointcd='" + cbsalespoint.SelectedValue.ToString() + "'");

                var miniLoad = bll.vLookUp("select sum(miniLoadPerSalesman) from tsalestargetho where period='" + cbperiod.SelectedValue.ToString() + "' and "
                  + " prod_cd in (select prod_cd from tmst_product where level_no='2' and prod_nm='" + hdprodcd.Value.ToString() + "') and salespointcd='" + cbsalespoint.SelectedValue.ToString() + "'");

                var period = Convert.ToString(cbperiod.SelectedValue);
                var salespointValue = Convert.ToString(cbsalespoint.SelectedValue);
                var prodcd = Convert.ToString(hdprodcd.Value);


                //LinkButton lnk = (LinkButton)e.Row.FindControl("lnk_productid");
                //string url = "fm_salestargethoPopup.aspx?period=" + period + "&salesPoint=" + salespointValue + "&prodcd=" + prodcd;
                string url = "fm_salestargethoPopup.aspx?period=" + period + "&salesPoint=" + salespointValue + "&prodcd=" + prodcd + "&qty=" + Convert.ToString(txqty.Text);

                lnk_productid.Attributes.Add("onClick", "JavaScript: window.open('" + url + "','','_blank','width=500,height=245,left=350,top=400')");


                DataTable dt = new DataTable();

                arr.Add(new cArrayList("@period", period));
                arr.Add(new cArrayList("@SalesPointCD", cbsalespoint.SelectedValue.ToString()));
                arr.Add(new cArrayList("@prod_cd", prodcd));
                arr.Add(new cArrayList("@GroupSalesman", null));

                dt = cdl.GetValueFromSP("sp_tsalestargetsalespointSalesGroup_get", arr);
                if (dt.Rows.Count > 0) {
                    foreach (DataRow dr in dt.Rows)
                    {
                        lblAssignGroup.Text += Convert.ToString(dr["GroupSalesman"]) + ",";
                    }

                    if (lblAssignGroup.Text.Length > 0) {
                       lblAssignGroup.Text = lblAssignGroup.Text.Remove(lblAssignGroup.Text.Length - 1);
                    }
                }

                if (isPriority == "False" || isPriority == "")
                {
                    chkIsPriority.Checked = false;
                    lnk_productid.Visible = false;
                    txMinimumLoading.Visible = false;
                }
                else { 
                    chkIsPriority.Checked = Convert.ToBoolean(isPriority); 
                    lnk_productid.Visible = true;
                    txMinimumLoading.Visible = true;
                    txMinimumLoading.Text = Convert.ToString(miniLoad);
                    if (lblAssignGroup.Text.Length > 0) {
                        chkIsPriority.Enabled = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_salestargetho2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        //catch (Exception ex)
        //{
        //    app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
        //    ut.Logs("", "Sales Target", "Sales Target Head Office", "fm_salestargetho2", "grd_RowDataBound", "Exception", ex.Message + ex.InnerException);
        //}
    }

    protected void OnChangeHandler(object sender, System.EventArgs e)
    {
        // Handle the event...
    }

    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=sto&salespointcd="+cbsalespoint.SelectedValue+"&period="+cbperiod.SelectedValue+"');", true);
    }
    protected void cbperiod_SelectedIndexChanged(object sender, EventArgs e)
    {
        cblevel_SelectedIndexChanged(sender, e);
    }
    protected void chkIsPriority_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox cb = (CheckBox)sender;
            GridViewRow parentRow = cb.NamingContainer as GridViewRow;
            Label lblRate = parentRow.FindControl("Label3") as Label;
            TextBox txqty = (TextBox)parentRow.FindControl("txqty");
            TextBox txMinimumLoading = (TextBox)parentRow.FindControl("txMinimumLoading");
            LinkButton lnk_productid = (LinkButton)parentRow.FindControl("lnk_productid");
            if (cb.Checked)
            {
                if (txqty.Text == "")
                {

                    app.BootstrapAlert(lblMsg, "Please insert qty", app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
                    cb.Checked = false;
                }
                else if (txqty != null)
                {
                    lnk_productid.Enabled = true;
                    lnk_productid.Visible = true;
                    txMinimumLoading.Enabled = true;
                    txMinimumLoading.Visible = true;

                    HiddenField hdprodcd = (HiddenField)parentRow.FindControl("hdprodcd");

                    var period = Convert.ToString(cbperiod.SelectedValue);
                    var salespointValue = Convert.ToString(cbsalespoint.SelectedValue);
                    var prodcd = Convert.ToString(hdprodcd.Value);

                    string url = "fm_salestargethoPopup.aspx?period=" + period + "&salesPoint=" + salespointValue + "&prodcd=" + prodcd + "&qty=" + Convert.ToString(txqty.Text);

                    lnk_productid.Attributes.Add("onClick", "JavaScript: window.open('" + url + "','','_blank','width=500,height=245,left=350,top=400')");
                }
            }
            if (!cb.Checked)
            {
                if (txqty != null)
                {
                    lnk_productid.Enabled = false;
                    lnk_productid.Visible = false;
                    txMinimumLoading.Enabled = false;
                    txMinimumLoading.Visible = false;
                }
            }

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_salestargetho2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void txMinimumLoading_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox txMinimumLoading = (TextBox)sender;
            GridViewRow parentRow = txMinimumLoading.NamingContainer as GridViewRow;
            CheckBox cb = parentRow.FindControl("chkIsPriority") as CheckBox; // (CheckBox)sender;
            Label lblRate = parentRow.FindControl("Label3") as Label;
            TextBox txqty = (TextBox)parentRow.FindControl("txqty");
            //  = (TextBox)parentRow.FindControl("txMinimumLoading");
            LinkButton lnk_productid = (LinkButton)parentRow.FindControl("lnk_productid");
            if (cb.Checked)
            {
                if (txMinimumLoading.Text == "" || txMinimumLoading.Text == "0")
                {
                    app.BootstrapAlert(lblMsg, "Please insert minimum load", app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
                }
                else if (Convert.ToDecimal(txMinimumLoading.Text) > Convert.ToDecimal(txqty.Text))
                {
                    app.BootstrapAlert(lblMsg, "Minimum load should less than Qty", app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
                }
                else { lblMsg.Visible = false; }
            }

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_salestargetho2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void btnAchievement_Click(object sender, EventArgs e)
    {
        try
        {
            if (bll.nCheckAccess("ViewAchievement", Request.Cookies["usr_id"].Value.ToString()) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','View Achievement !!','warning');", true);
                return;
            }
            Response.Redirect("fm_salestargetho2Achievement.aspx");

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_salestargetho2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
}