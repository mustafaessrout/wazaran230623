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

public partial class fm_salestargetsalespointView : System.Web.UI.Page
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
                app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
                ut.Logs("", "Sales Target", "Sales Target Branch", "fm_salestargetsalespoint", "PageLoad", "Exception", ex.Message + ex.InnerException);
            }
        }
    }

    void BindControl()
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

        string emp_cd = Request.Cookies["usr_id"].Value.ToString();
        if (emp_cd == "3091" || emp_cd == "2818")
        {
            bll.vBindingComboToSp(ref cbprod, "sp_tmst_product_getbytarget", "prod_cd", "prod_nm", arr);
        }
        else
        {
            arr.Add(new cArrayList("@emp_cd", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingComboToSp(ref cbprod, "sp_tmst_product_getbytargetUser", "prod_cd", "prod_nm", arr);
        }

        // cbprod_SelectedIndexChanged(sender, e);
        txtMinTarget.Text = "0";
        //txtUptoDateTarget.Text = "";
        txqty.Text = "0";
        txtabqty.Text = "0";
    }

    void ClearControl(object sender, EventArgs e)
    {
        txtMinTarget.Text = "";
        txqty.Text = "";
        txtabqty.Text = "";
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

            txtargetho.Text = bll.vLookUp("	select qty  from tsalestargetho where period='" + Convert.ToString(cbperiod.SelectedValue)
                + "' and prod_cd='" + Convert.ToString(cbprod.SelectedValue) + "' and salespointcd='" + Convert.ToString(Request.Cookies["sp"].Value) + "'").ToString();
            txtMinTarget.Text = bll.vLookUp("	select miniLoadPerSalesman  from tsalestargetho where period='" + Convert.ToString(cbperiod.SelectedValue)
                + "' and prod_cd='" + Convert.ToString(cbprod.SelectedValue) + "' and salespointcd='" + Convert.ToString(Request.Cookies["sp"].Value) + "'").ToString();

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
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "Sales Target", "Sales Target Branch", "fm_salestargetsalespoint", "cbprod_SelectedIndexChanged", "Exception", ex.Message + ex.InnerException);
        }

    }
    protected void cbperiod_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            vBindingGrid();
            string emp_cd = Request.Cookies["usr_id"].Value.ToString();
            arr.Add(new cArrayList("@period", cbperiod.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
            if (emp_cd == "3091" || emp_cd == "2818")
            {
                bll.vBindingComboToSp(ref cbprod, "sp_tmst_product_getbytarget", "prod_cd", "prod_nm", arr);
            }
            else
            {
                arr.Add(new cArrayList("@emp_cd", Request.Cookies["usr_id"].Value.ToString()));
                bll.vBindingComboToSp(ref cbprod, "sp_tmst_product_getbytargetUser", "prod_cd", "prod_nm", arr);
            }
            cbprod_SelectedIndexChanged(sender, e);
            // cbsalesman_SelectedIndexChanged(sender, e);
            // cbprod_SelectedIndexChanged(sender, e);
        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "Sales Target", "Sales Target Branch", "fm_salestargetsalespoint", "cbperiod_SelectedIndexChanged", "Exception", ex.Message + ex.InnerException);
        }
    }

    void vBindingGrid()
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@period", cbperiod.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salesman_cd", cbsalesman.SelectedValue.ToString()));
            arr.Add(new cArrayList("@prod_cd", cbprod.SelectedValue.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_tsalestargetBySalesPoint_get", arr);
        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "Sales Target", "Sales Target Branch", "fm_salestargetsalespoint", "vBindingGrid", "Exception", ex.Message + ex.InnerException);
        }
    }

    void vBindingGridByProduct()
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@period", cbperiod.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salesman_cd", null));
            arr.Add(new cArrayList("@prod_cd", cbprod.SelectedValue.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_tsalestargetBySalesPointByProduct_get", arr);
        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "Sales Target", "Sales Target Branch", "fm_salestargetsalespoint", "vBindingGrid", "Exception", ex.Message + ex.InnerException);
        }
    }
    void vBindingGridBySalesMan()
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@period", cbperiod.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salesman_cd", cbsalesman.SelectedValue.ToString()));
            arr.Add(new cArrayList("@prod_cd", null));
            bll.vBindingGridToSp(ref grd, "sp_tsalestargetBySalesPoint_get", arr);
        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "Sales Target", "Sales Target Branch", "fm_salestargetsalespoint", "vBindingGrid", "Exception", ex.Message + ex.InnerException);
        }
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
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "Sales Target", "Sales Target Branch", "fm_salestargetsalespoint", "cbsalesman_SelectedIndexChanged", "Exception", ex.Message + ex.InnerException);
        }
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
                dt = cdl.GetValueFromSP("sp_temail_outbox_ins", arr);
                if (dt.Rows.Count > 0) { }
            }
        }

        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "Sales Target", "Sales Target Branch", "fm_salestargetsalespoint", "SendHtmlFormattedEmail", "Exception", ex.Message + ex.InnerException);
        }
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
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "Sales Target", "Sales Target Branch", "fm_salestargetsalespoint", "SendEmail", "Exception", ex.Message + ex.InnerException);
        }
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
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "Sales Target", "Sales Target Branch", "fm_salestargetsalespoint", "SendEmail", "Exception", ex.Message + ex.InnerException);
        }
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
            return body;
        }

        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "Sales Target", "Sales Target Branch", "fm_salestargetsalespoint", "PopulateBody", "Exception", ex.Message + ex.InnerException);
            return body;
        }
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
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "Sales Target", "Sales Target Branch", "grd_RowDataBound", "PopulateBody", "Exception", ex.Message + ex.InnerException);
        }
    }

    protected void btnAchievement_Click(object sender, EventArgs e)
    {
        try
        {
            //salestargetAchievementByProd
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=salestargetAchievement&period=" + cbperiod.SelectedValue.ToString() + "'&prod_cd="+cbprod.SelectedValue +");", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=salestargetAchievementByProd&period=" + cbperiod.SelectedValue.ToString() + "&prod_cd=" + cbprod.SelectedValue + "');", true);
        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "Sales Target", "Sales Target Branch", "btnAchievement_Click", "Report", "Exception", ex.Message + ex.InnerException);
        }
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@period", cbperiod.SelectedValue));
            arr.Add(new cArrayList("@prod_cd",cbprod.SelectedValue));
            bll.vSalestargetAchievementByProd(arr);

            app.BootstrapAlert(lblMsg, "Data generated sucessfully for product " + cbperiod.SelectedItem.Text +" & period " + cbperiod.SelectedItem.Text , app.alertType = AppClassTools.AppClass.BootstrapAlertType.Success, true);
        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "Sales Target", "Sales Target Branch", "btnAchievement_Click", "Report", "Exception", ex.Message + ex.InnerException);
        }
    }

    protected void btnAchievementAll_Click(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=salestargetAchievement&period=" + cbperiod.SelectedValue.ToString() + "&prod_cd="+cbprod.SelectedValue +"');", true);
        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "Sales Target", "Sales Target Branch", "btnAchievement_Click", "Report", "Exception", ex.Message + ex.InnerException);
        }
    }
}