using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_customertransfer : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            try
            {
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@qry_cd", "SalesJob"));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vBindingComboToSp(ref cbsalesman, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
                cbsalesman_SelectedIndexChanged(sender, e);
                arr.Clear();
                arr.Add(new cArrayList("@reasn_typ", "custransfer"));
                bll.vBindingComboToSp(ref cbreason, "sp_tmst_reason_get", "reasn_cd", "reasn_nm", arr);
                dttransfer.Text = Request.Cookies["waz_dt"].Value.ToString();
                dttransfer.CssClass = "form-control ro";
                dttransfer.Enabled = false;
                dteff.Text = Request.Cookies["waz_dt"].Value.ToString();
                txtransferno.Text = "NEW";
                txcust.CssClass = "divhid";
                chenddt.Checked = true;
                chenddt_CheckedChanged(sender, e);
            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_customertransfer");
                Response.Redirect("fm_ErrorPage.aspx");
            }
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        System.Data.SqlClient.SqlDataReader rs = null;
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        List<string> lCust = new List<string>();
        string sCust = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        // arr.Add(new cArrayList("@salesman_cd", contextKey));
        arr.Add(new cArrayList("@cust_cd", prefixText));
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        bll.vSearchMstCustomer(arr, ref rs);
        while (rs.Read())
        {
            sCust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + "-" + rs["cust_nm"].ToString(), rs["cust_cd"].ToString());
            lCust.Add(sCust);
        }
        rs.Close();
        return (lCust.ToArray());
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList2(string prefixText, int count, string contextKey)
    {
        HttpCookie cookieSP;
        cookieSP = HttpContext.Current.Request.Cookies["sp"];
        string sEmp = string.Empty;
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lEmp = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", cookieSP.Value.ToString()));
        arr.Add(new cArrayList("@job_title", "SalesJob"));
        //arr.Add(new cArrayList("@emp_nm", prefixText));
        cbll bll = new cbll();
        bll.vSearchMstEmployee2bysalespoint(arr, ref rs);
        while (rs.Read())
        {
            sEmp = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["emp_desc"].ToString(), rs["emp_cd"].ToString());
            lEmp.Add(sEmp);
        } rs.Close();
        return (lEmp.ToArray());
    }
  
    protected void btsave_Click(object sender, EventArgs e)
    {
        
        try
        {   string fileName = string.Empty;
            string fileName2 = string.Empty;string fileName3 = string.Empty;
            if (fuconfirmation1.HasFile)
            {
                FileInfo fi1 = new FileInfo(fuconfirmation1.FileName);
                string ext1 = fi1.Extension;
                byte[] fs1 = fuconfirmation1.FileBytes;
                if (fs1.Length > 1000000)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('File size cannot exceed 1 MB.','Upload File 1','warning');", true);
                    return;
                }
                if (fuconfirmation1.FileName == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please file agreement','put file 1','warning');", true);
                    return;
                }
                if (fuconfirmation1.FileName == string.Empty)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('File confirmation must specified','File upload 1 evidence','warning');", true);
                    return;
                }
                fileName = fuconfirmation1.FileName;
            }
            if (fuconfirmation2.HasFile)
            {
                FileInfo fi2 = new FileInfo(fuconfirmation2.FileName);
                string ext2 = fi2.Extension;
                byte[] fs2 = fuconfirmation2.FileBytes;
                if (fs2.Length > 1000000)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('File size cannot exceed 1 MB.','Upload File 2','warning');", true);
                    return;
                }
                if (fuconfirmation2.FileName == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please file agreement','put file 2','warning');", true);
                    return;
                }
                if (fuconfirmation2.FileName == string.Empty)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('File confirmation must specified','File upload 2 evidence','warning');", true);
                    return;
                }
                fileName2 =  fuconfirmation2.FileName;
            }
            if (fuconfirmation3.HasFile)
            {
                FileInfo fi3 = new FileInfo(fuconfirmation3.FileName);
                string ext3 = fi3.Extension;
                byte[] fs3 = fuconfirmation3.FileBytes;
                if (fs3.Length > 1000000)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('File size cannot exceed 1 MB.','Upload File 3','warning');", true);
                    return;
                }
                if (fuconfirmation3.FileName == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please file agreement','put file 3','warning');", true);
                    return;
                }
                if (fuconfirmation3.FileName == string.Empty)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('File confirmation must specified','File upload 3 evidence','warning');", true);
                    return;
                }
                fileName3 =fuconfirmation3.FileName;
            }
            int cnt = Convert.ToInt32(bll.vLookUp("select count(*) from tcustomer_transfer where old_salesman='" + cbsalesman.SelectedValue + "' and new_salesman='" + hdemp.Value + "' and cust_cd = '"+ hdcust.Value +"'  and custrf_sta_id='N' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'"));

            if (cnt > 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Approval pending for "+ cbsalesman.SelectedItem.Text + " ','Approval pending','warning');", true);
                return;
            }

            

            if (hdemp.Value.ToString() == cbsalesman.SelectedValue.ToString())
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('New salesman can not same with Old Salesman','Re-select salesman','warning');", true);
                return;
            }

            if (dteff.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Effective date must selected','Effective Date','warning');", true);
                return;
            }

            if (hdemp.Value.ToString() == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select new salesman','New Salesman','warning');", true);
                return;
            }

            string sCheque;
            if (rdcust.SelectedValue.ToString() == "C")
            {
                sCheque = bll.vLookUp("select dbo.fn_outstandingchecque('" + cbsalesman.SelectedValue.ToString() + "','" + hdcust.Value.ToString() +  "','"+ Request.Cookies["sp"].Value.ToString() + "')");
            }
            else
            {
                sCheque = bll.vLookUp("select dbo.fn_outstandingchecque('" + cbsalesman.SelectedValue.ToString() + "',null,'"+ Request.Cookies["sp"].Value.ToString() + "')");
            }
            if (sCheque != "ok")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('There is pending Cheque payment','Cheque No. " + sCheque + "','warning');", true);
                return;
            }
            string sRetur;
            if (rdcust.SelectedValue.ToString() == "C")
            {
                sRetur = bll.vLookUp("select dbo.fn_outstandingreturappv('" + cbsalesman.SelectedValue.ToString() + "','" + hdcust.Value.ToString() + "','"+ Request.Cookies["sp"].Value.ToString() + "')");
            }
            else
            {
                sRetur = bll.vLookUp("select dbo.fn_outstandingreturappv('" + cbsalesman.SelectedValue.ToString() + "',null,'"+ Request.Cookies["sp"].Value.ToString() + "')");
            }
            if (sRetur != "ok")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There is pending Retur Approval','" + sRetur + "','warning');", true);
                return;
            }
            string sPayment;
            if (rdcust.SelectedValue.ToString() == "C")
            {
                sPayment = bll.vLookUp("select dbo.fn_outstandingpaymentrt('" + cbsalesman.SelectedValue.ToString() + "','" + hdcust.Value.ToString() +  "','"+ Request.Cookies["sp"].Value.ToString() + "')");
            }
            else
            {
                sPayment = bll.vLookUp("select dbo.fn_outstandingpaymentrt('" + cbsalesman.SelectedValue.ToString() + "',null,'"+ Request.Cookies["sp"].Value.ToString() + "')");
            }
            if (sPayment != "ok")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There is pending Retur Payment Applied','" + sPayment + "','warning');", true);
                return;
            }

            if (hdemp.Value.ToString() == cbsalesman.SelectedValue.ToString())
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Old salesman can not same with New salesman','Can not same salesman','warning');", true);
                return;
            }

            if (DateTime.ParseExact(dteff.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture) < DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Effective Date can not back date or same','Eff Date must bigger than system date','warning');", true);
                return;
            }

            if (txreason.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Reason transferred can not empty','Reason transfer customer','warning');", true);
                return;
            }
            string sorders;
            if (rdcust.SelectedValue.ToString() == "C")
            {
                sorders = bll.vLookUp("select dbo.fn_ordernotyetprinted('" + cbsalesman.SelectedValue.ToString() + "','" + hdcust.Value.ToString() + "','"+ Request.Cookies["sp"].Value.ToString() + "')");
            }
            else
            {
                sorders = bll.vLookUp("select dbo.fn_ordernotyetprinted('" + cbsalesman.SelectedValue.ToString() + "',null,'"+ Request.Cookies["sp"].Value.ToString() + "')");
            }
            if (sorders != "ok")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There is pending order not yet printed','" + sorders + "','warning');", true);
                return;
            }


            if (txbalancesoa.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Balance SOA that have been signed can not empty','Reason transfer customer','warning');", true);
                return;
            }

            btsave.Enabled = false;
            btsave.Visible = false;

            string sTrfNo=string.Empty;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@old_salesman", cbsalesman.SelectedValue.ToString()));
            arr.Add(new cArrayList("@new_salesman", hdemp.Value.ToString()));
            arr.Add(new cArrayList("@trf_dt", DateTime.ParseExact( dttransfer.Text,"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@eff_dt", DateTime.ParseExact( dteff.Text,"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@end_dt", (!chenddt.Checked) ? DateTime.ParseExact(dtend.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString() : null ));
            arr.Add(new cArrayList("@filename", null));
            arr.Add(new cArrayList("@filename2", null));
            arr.Add(new cArrayList("@filename3", null));
            arr.Add(new cArrayList("@totamt", Convert.ToDecimal( txbalancetotransfer.Text)));
            arr.Add(new cArrayList("@rdcust", rdcust.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@reason", txreason.Text));
            arr.Add(new cArrayList("@totamt_soa", Convert.ToDecimal(txbalancesoa.Text)));
            //  arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            if (rdcust.SelectedValue.ToString()=="C")
            {
                arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
            }
            arr.Add(new cArrayList("@custrf_sta_id", "N")); // Status=N , it is mean need approval before proceed
            arr.Add(new cArrayList("@reasn_cd", cbreason.SelectedValue));
            arr.Add(new cArrayList("@createdBy", Request.Cookies["usr_id"].Value.ToString()));
            bll.vInsertCustomerTransfer(arr,ref sTrfNo);
            txtransferno.Text = sTrfNo;
            txtransferno.CssClass = cd.csstextro;

            //creport rep = new creport();
            //arr.Clear();
            //arr.Add(new cArrayList("@trf_no", sTrfNo));//rp_transfercustomerBeforeAppr
            //arr.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
            //arr.Add(new cArrayList("salespointcd", Request.Cookies["sp"].Value.ToString()));
            //rep.vShowReportToPDF("CustomerTransferBefore.rpt", arr, bll.sGetControlParameter("image_path") + sTrfNo + ".pdf");

            // This remark for change only happen after approved ____________________________________________
            // Batch transfer salesman
            //if (DateTime.ParseExact(dteff.Text,"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture ) == DateTime.ParseExact(dttransfer.Text,"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture))
            ////{
            //    arr.Clear();
            //    arr.Add(new cArrayList("@trf_no", sTrfNo));
            //    arr.Add(new cArrayList("@salesman_cd", cbsalesman.SelectedValue.ToString()));
            //    arr.Add(new cArrayList("@salesman_cd_new", hdemp.Value.ToString()));
            //    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            //    if (rdcust.SelectedValue.ToString() == "C")
            //    {
            //        arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
            //    }
            //bll.vBatchSalesmanTransfer(arr);
            ////}
            //__________________________________________________________________________________________________
            //string sFilename = sTrfNo +"."+ System.IO.Path.GetExtension(this.fuconfirmation.PostedFile.FileName);
            if (fuconfirmation1.HasFile)
            {
                string sFilename = sTrfNo +"_1" + System.IO.Path.GetExtension(this.fuconfirmation1.PostedFile.FileName);

                FileInfo fi = new FileInfo(fuconfirmation1.FileName);
                string ext = fi.Extension;
                byte[] fs = fuconfirmation1.FileBytes;
                if (fs.Length <= 1000000)
                {
                    fuconfirmation1.SaveAs(bll.sGetControlParameter("image_path") + sFilename);
                    arr.Clear();
                    arr.Add(new cArrayList("@filename", sFilename));
                    arr.Add(new cArrayList("@trf_no", sTrfNo));
                    arr.Add(new cArrayList("salespointcd", Request.Cookies["sp"].Value.ToString()));
                    bll.vUpdateCustomerTransfer(arr);
                    bll.vLookUp("update tcustomer_transfer set totalFile = '1' where trf_no = '" + sTrfNo + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
                }
                string sSubject = "#New Transfer Customer Request";
                string sBody = "Dear Manager, <br/><br/>New Customer transfer with no." + sTrfNo + " from " + Request.Cookies["sp"].Value.ToString() + " has been requested.<br/>Please login to Approval Web Site to make approval/rejection.";
                sBody += "<br>Wazaran Admin";
                List<string> lapproval = bll.lGetApproval("custtransfer", 1);
                string sEmailTo = lapproval[1];

                //arr.Clear();
                //arr.Add(new cArrayList("@emailsubject", sSubject));
                //arr.Add(new cArrayList("@msg", sBody));
                //arr.Add(new cArrayList("@to", sEmailTo));
                //arr.Add(new cArrayList("@doc_no", sTrfNo));
                //arr.Add(new cArrayList("@doc_typ", "custtransfer"));
                //arr.Add(new cArrayList("@file_attachment", sFilename));
                //bll.vInsertEmailOutbox(arr);
            }
            if (fuconfirmation2.HasFile)
            {
                string sFilename = sTrfNo + "_2" + System.IO.Path.GetExtension(this.fuconfirmation2.PostedFile.FileName);
                arr.Clear();
                //arr.Add(new cArrayList("@filename2", sFilename));
                //arr.Add(new cArrayList("@trf_no", sTrfNo));
                //bll.vUpdateCustomerTransfer(arr);
                FileInfo fi = new FileInfo(fuconfirmation2.FileName);
                string ext = fi.Extension;
                byte[] fs = fuconfirmation2.FileBytes;
                if (fs.Length <= 1000000)
                {
                    fuconfirmation2.SaveAs(bll.sGetControlParameter("image_path") + sFilename);
                    bll.vLookUp("update tcustomer_transfer set filename2 = '" + sFilename + "' where trf_no = '" + sTrfNo + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
                    bll.vLookUp("update tcustomer_transfer set totalFile = '2' where trf_no = '" + sTrfNo + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
                }
                string sSubject = "#New Transfer Customer Request";
                string sBody = "Dear Manager, <br/><br/>New Customer transfer with no." + sTrfNo + " from " + Request.Cookies["sp"].Value.ToString() + " has been requested.<br/>Please login to Approval Web Site to make approval/rejection.";
                sBody += "<br>Wazaran Admin";
                List<string> lapproval = bll.lGetApproval("custtransfer", 1);
                string sEmailTo = lapproval[1];

                //arr.Clear();
                //arr.Add(new cArrayList("@emailsubject", sSubject));
                //arr.Add(new cArrayList("@msg", sBody));
                //arr.Add(new cArrayList("@to", sEmailTo));
                //arr.Add(new cArrayList("@doc_no", sTrfNo));
                //arr.Add(new cArrayList("@doc_typ", "custtransfer"));
                //arr.Add(new cArrayList("@file_attachment", sFilename));
                //bll.vInsertEmailOutbox(arr);
            }
            if (fuconfirmation3.HasFile)
            {
                string sFilename = sTrfNo + "_3" + System.IO.Path.GetExtension(this.fuconfirmation3.PostedFile.FileName);
                arr.Clear();
                //arr.Add(new cArrayList("@filename3", sFilename));
                //arr.Add(new cArrayList("@trf_no", sTrfNo));
                //bll.vUpdateCustomerTransfer(arr);
                FileInfo fi = new FileInfo(fuconfirmation3.FileName);
                string ext = fi.Extension;
                byte[] fs = fuconfirmation3.FileBytes;
                if (fs.Length <= 1000000)
                {
                    fuconfirmation3.SaveAs(bll.sGetControlParameter("image_path") + sFilename);
                    bll.vLookUp("update tcustomer_transfer set filename3 = '" + sFilename + "' where trf_no = '" + sTrfNo + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
                    bll.vLookUp("update tcustomer_transfer set totalFile = '3' where trf_no = '" + sTrfNo + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
                }
                string sSubject = "#New Transfer Customer Request";
                string sBody = "Dear Manager, <br/><br/>New Customer transfer with no." + sTrfNo + " from " + Request.Cookies["sp"].Value.ToString() + " has been requested.<br/>Please login to Approval Web Site to make approval/rejection.";
                sBody += "<br>Wazaran Admin";
                List<string> lapproval = bll.lGetApproval("custtransfer", 1);
                string sEmailTo = lapproval[1];

                //arr.Clear();
                //arr.Add(new cArrayList("@emailsubject", sSubject));
                //arr.Add(new cArrayList("@msg", sBody));
                //arr.Add(new cArrayList("@to", sEmailTo));
                //arr.Add(new cArrayList("@doc_no", sTrfNo));
                //arr.Add(new cArrayList("@doc_typ", "custtransfer"));
                //arr.Add(new cArrayList("@file_attachment", sFilename));
                //bll.vInsertEmailOutbox(arr);
            }
            dteff.CssClass = cd.csstextro;
            dteff.Enabled = false;
            dttransfer.CssClass = cd.csstextro;
            dttransfer.Enabled = false;
            btsave.Style.Add("display", "none");
            btprint.Style.Add("display", "normal");
            rdcust.CssClass = cd.csstextro;
            rdcust.Enabled = false;
            fuconfirmation1.CssClass = "form-control ro";
            fuconfirmation1.Enabled = false;
            fuconfirmation2.CssClass = "form-control ro";
            fuconfirmation2.Enabled = false;
            fuconfirmation3.CssClass = "form-control ro";
            fuconfirmation3.Enabled = false;
            txreason.CssClass = "form-control ro";
            txreason.Enabled = false;
            txnewsalesman.CssClass = "form-control ro";
            txnewsalesman.Enabled = false;
        
            ScriptManager.RegisterStartupScript(Page, Page.GetType(),Guid.NewGuid().ToString(), "sweetAlert('Transfer customer succeeded','" + sTrfNo + "','success');", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_customertransfer");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void cbsalesman_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            try
            {
                //txbalancetotransfer.Text = bll.vLookUp("select dbo.fn_getbalancecustomer('" + cbsalesman.SelectedValue.ToString() + "','" + hdcust.Value.ToString() +  "')");
                txbalancetotransfer.Text = bll.vLookUp("select dbo.fn_getbalancecustomer('" + cbsalesman.SelectedValue.ToString() + "','" + hdcust.Value.ToString() + "','"+ Request.Cookies["sp"].Value.ToString() + "')"); //modif daryanto 20-11-2016

            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_customertransfer");
                Response.Redirect("fm_ErrorPage.aspx");
            }
        }
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        try
        {

            if (txtransferno.Text == "NEW")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Customer Transfer No not yet selected','Check Cust Trf No','warning');", true);
                return;
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=custtran&no=" + txtransferno.Text + "');", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_customertransfer");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btcust_Click(object sender, EventArgs e)
    {

    }
    protected void rdcust_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rdcust.SelectedValue.ToString() == "C")
            {
                txcust.CssClass = "form-control makeitreadwrite";
                txcust.Enabled = true;
                txcust.Text = ""; hdcust.Value = "";
                txbalancetotransfer.Text = "";
            }
            else 
            {
                txcust.CssClass = "divhid";
                txbalancetotransfer.Text = bll.vLookUp("select dbo.fn_getbalancecustomer('" + cbsalesman.SelectedValue.ToString() + "','" + hdcust.Value.ToString() + "','"+ Request.Cookies["sp"].Value.ToString() + "')"); //modif daryanto 20-11-2016
                                                                                                                                                                             //  cbsalesman.CssClass = "divnormal";
                txnewsalesman.Text = "";
                // txcust.Visible = false;
                txcust.CssClass = "divhid";
                txcust.Text = ""; hdcust.Value = "";
                hdemp.Value = "";
            }

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_customertransfer");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {

            txcust.CssClass = "form-control ro";
            txcust.Enabled = false;
            cbsalesman.SelectedValue = bll.vLookUp("select salesman_cd from tmst_customer where cust_cd='" + hdcust.Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            cbsalesman.CssClass = "form-control ro";
            cbsalesman.Enabled = false;
            //txbalancetotransfer.Text = bll.vLookUp("select dbo.fn_getbalancecustomer('" + cbsalesman.SelectedValue.ToString()+ "','" + hdcust.Value.ToString() + "')");
            txbalancetotransfer.Text = bll.vLookUp("select dbo.fn_getbalancecustomer('" + cbsalesman.SelectedValue.ToString() + "','" + hdcust.Value.ToString() + "','"+ Request.Cookies["sp"].Value.ToString() + "')"); //modif daryanto 20-11-2016
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_customertransfer");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_customertransfer.aspx");
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "popupwindow('lookup_ct.aspx');", true);
    }
    protected void btrefresh_Click(object sender, EventArgs e)
    {
        try
        {
            System.Data.SqlClient.SqlDataReader rs = null;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@trf_no", hdtrf.Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vGetCustomerTransfer(arr, ref rs);
            while (rs.Read())
            {
                txtransferno.Text = rs["trf_no"].ToString(); txtransferno.CssClass = "form-control ro"; txtransferno.Enabled = false;
                dttransfer.Text = Convert.ToDateTime(rs["trf_dt"]).ToString("d/M/yyyy"); dttransfer.CssClass = "form-control ro"; dttransfer.Enabled = false;
                dteff.Text = Convert.ToDateTime(rs["eff_dt"]).ToString("d/M/yyyy"); dteff.CssClass = "form-control ro"; dteff.Enabled = false;
                rdcust.SelectedValue = rs["rdcust"].ToString(); rdcust.CssClass = "well well-sm radio radio-inline no-margin full ro"; rdcust.Enabled = false;
                cbreason.SelectedValue = rs["reasn_cd"].ToString();
                cbreason.CssClass = "well well-sm radio radio-inline no-margin full ro"; cbreason.Enabled = false;
                txreason.Text = rs["reason"].ToString();
                txreason.CssClass = "form-control ro";
                txreason.Enabled = false;
                cbsalesman.SelectedValue = rs["old_salesman"].ToString(); cbsalesman.CssClass = "form-control ro"; cbsalesman.Enabled = false;
                hdemp.Value = rs["new_salesman"].ToString();
                txnewsalesman.Text = bll.vLookUp("select emp_nm from tmst_employee where emp_cd='" + hdemp.Value.ToString() + "'");
                txnewsalesman.CssClass = "form-control ro";
                txnewsalesman.Enabled = false;
                txbalancetotransfer.Text = rs["totamt"].ToString();
                txbalancesoa.Text = rs["totamt_soa"].ToString();
                txbalancesoa.CssClass = "form-control ro";
                txbalancesoa.Enabled = false;
                btsave.Visible = false;
                btprint.Visible = true;
                btnew.Visible = true;
            }
            rs.Close();

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_customertransfer");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void chenddt_CheckedChanged(object sender, EventArgs e)
    {
        if (chenddt.Checked)
        {
            dtend.Enabled = false;
            dtend.Text = "";
        }
        else
        {
            dtend.Enabled = true;
        }
    }

}