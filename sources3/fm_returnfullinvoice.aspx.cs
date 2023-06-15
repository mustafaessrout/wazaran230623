using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_returnfullinvoice : System.Web.UI.Page
{
    cbll bll = new cbll();
    cdal cdl = new cdal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                txinvoiceno.CssClass = "form-control ro";
                txinvoiceno.Enabled = false;
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@reasn_typ", "100"));
                bll.vBindingComboToSp(ref cbreason, "sp_tmst_reason_get", "reasn_cd", "reasn_nm", arr);
                btnew.CssClass = "btn btn-success btn-new ";
                btprint.CssClass = "divhid";
                btsave.CssClass = "btn btn-warning btn-save";

            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_returnfullinvoice");
                Response.Redirect("fm_ErrorPage.aspx");
            }
        }
    }


    protected void btsearch_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(),"opm", "popupwindow('lookupinvreturn.aspx');", true);
    }
    protected void btsearchApproval_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opm", "popupwindow('lookupinvreturnApproval.aspx');", true);
    }
    protected void btlookup_Click(object sender, EventArgs e)
    {
        try
        {
            System.Data.SqlClient.SqlDataReader rs = null;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@inv_no", hdinvoice.Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vGetDosalesInvoiceByReturnFull(arr, ref rs);
            while (rs.Read())
            {
                lbmanno.Text = rs["manual_no"].ToString();
                lbcust.Text = rs["cust_desc"].ToString();
                lbsalesman.Text = rs["emp_desc"].ToString();
                txinvoiceno.Text = rs["inv_no"].ToString();
                lbtotamt.Text = rs["totamt"].ToString();
                lbbalance.Text = rs["balance"].ToString();
                lbinvdate.Text = Convert.ToDateTime(rs["inv_dt"]).ToString("d/M/yyyy");
                lbduedate.Text = Convert.ToDateTime(rs["due_dt"]).ToString("d/M/yyyy");
                lborderno.Text = bll.vLookUp("select so_cd from tmst_dosales where inv_no='" + hdinvoice.Value.ToString() + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
                arr.Clear();
                arr.Add(new cArrayList("@inv_no", hdinvoice.Value.ToString()));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vBindingGridToSp(ref grd, "sp_tdosalesinvoice_dtl_get", arr);
                bll.vBindingGridToSp(ref grddtl, "sp_orderfree_get", arr);
                lbstatus.Text = bll.vLookUp("select fld_desc from tfield_value where fld_nm='inv_sta_id' and fld_valu='" + rs["inv_sta_id"].ToString() + "'");
                if (bll.vLookUp("select can_sta_id from tInvoice_fullReturn where inv_no='" + hdinvoice.Value.ToString() + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'") == "A")
                {
                    btprint.CssClass = "btn btn-info btn-print";
                    btsave.CssClass = "divhid";
                }

            }
            rs.Close();

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_returnfullinvoice");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            string pendInv = bll.vLookUp("select  count(*) from tInvoice_fullReturn  where inv_no = '" + hdinvoice.Value.ToString() + "' and can_sta_id='N' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
            if (Convert.ToDecimal(pendInv) > 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Invoice full return approval pending , Invoice number =!"+ hdinvoice.Value.ToString() + "','Select Invoice','warning');", true);
                return;
            }
            if (hdinvoice.Value.ToString() == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Invoice for full return has not yet selected!','Select Invoice','warning');", true);
                return;
            }

            if (bll.vLookUp("select dbo.fn_checkinvhaspayment('" + hdinvoice.Value.ToString() + "','"+ Request.Cookies["sp"].Value.ToString() + "')") == "yes")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('This invoice " + hdinvoice.Value.ToString() + " can not returned FULL!','This invoice already has payment or payment not yet cleareance','warning');", true);
                return;
            }
            if (fuconfirmation.FileName == string.Empty || fuconfirmation.FileName == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('File confirmation must specified','File upload evidence','warning');", true);
                return;
            }

            if (fuconfirmation.HasFile)
            {
                FileInfo fi = new FileInfo(fuconfirmation.FileName);
                string ext = fi.Extension;
                //fileExtension = fi.Extension;
                byte[] fs = fuconfirmation.FileBytes;
                if (fs.Length >= 1048576)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please size very big.Please upload small size','Please size very big','warning');", true);
                    return;
                }
            }

            string invIFR_no = string.Empty;
            List<cArrayList> arr = new List<cArrayList>();

            //arr.Add(new cArrayList("@inv_no", hdinvoice.Value.ToString()));
            //arr.Add(new cArrayList("@inv_sta_id", "F"));
            ////arr.Add(new cArrayList("@cancel_dt", Request.Cookies["waz_dt"].Value.ToString()));
            //arr.Add(new cArrayList("@reasn_cd", cbreason.SelectedValue.ToString()));
            //bll.vUpdateDosalesInvoiceByStatus(arr);

            arr.Add(new cArrayList("@inv_no", hdinvoice.Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@reasn_cd", cbreason.SelectedValue.ToString()));
            arr.Add(new cArrayList("@can_sta_id", "N"));
            arr.Add(new cArrayList("@createdBy", Convert.ToString(HttpContext.Current.Request.Cookies["usr_id"].Value.ToString())));
            bll.vInsertFullReturn(arr, ref invIFR_no);

            string sFilename = invIFR_no + System.IO.Path.GetExtension(this.fuconfirmation.PostedFile.FileName);
            //sTrfNo = "ID";
            arr.Clear();
            arr.Add(new cArrayList("@attachment", sFilename));
            arr.Add(new cArrayList("@invIFR_no", invIFR_no));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vUpdateFullReturn(arr);
            fuconfirmation.SaveAs(bll.sGetControlParameter("image_path") + "return\\" + sFilename);

            arr.Clear();
            DataTable dt = new DataTable();

            arr.Add(new cArrayList("@doc_typ", "invFullRet"));
            arr.Add(new cArrayList("@level_no", "1"));
            dt = cdl.GetValueFromSP("sp_tapprovalpattern_get", arr);

            //foreach (DataRow dr in dt.Rows)
            //{

            //    arr.Clear();
            //    List<string> lapp = bll.lGetApproval("invFullRet", 1);
            //    string htmlData = string.Empty;
            //    string branchName = bll.vLookUp("SELECT salespointcd + '-' + salespoint_nm FROM tmst_salespoint WHERE salespointcd='" + Convert.ToString(Request.Cookies["sp"].Value) + "'");
            //    string customer = lbcust.Text;
            //    string invoice = hdinvoice.Value;
            //    string amount = lbbalance.Text;
            //    string rem = cbreason.SelectedItem.Text;

            //    htmlData = "<body>" + 
            //                    "<p><span style = 'color: #339966;'>Branch Name: - <span style = 'color: #ff0000;'>"+ branchName + "</span></span></p>" +
            //                    "<p><span style = 'color: #339966;'> Invoice Full Return Details are given below.</span></p>"+
            //                    "<table style ='height: 63px;' width= '600' border ='1'>" +
            //                        "<tbody>" + 
            //                            "<tr>" +
            //                                "<td style = 'width: 200px;'><span style = 'color: #0000ff;'>Customer</span></td>"+
            //                                "<td style = 'width: 128px;'><span style = 'color: #0000ff;'>Invoice Number</span></td>" + 
            //                                "<td style = 'width: 128px;'><span style = 'color: #0000ff;'> Amount </span></td></tr>"+
            //                                "<td style = 'width: 128px;'><span style = 'color: #0000ff;'> Remarks </span></td></tr>" +
            //                                "<tr>" + 
            //                                "<td style = 'width: 200px;'><span style = 'color: #ff6600;'> "+ customer + " </span></td>" +
            //                                "<td style = 'width: 128px;'><span style = 'color: #ff6600;'> "+ invoice + " </span></td>"+
            //                                "<td style = 'width: 128px;'><p><span style = 'color: #ff6600;'> "+ amount + " </span></p></td>" +
            //                                "<td style = 'width: 128px;'><p><span style = 'color: #ff6600;'> "+ rem + " </span></p></td>" + 
            //                            "</tr>"+
            //                        "</tbody>" +          
            //                    "</table>" +
            //                    "<p><span style = 'color: #000000;'>" +
            //                    "This is only information to you.Please login to <a title = 'Wazaran Approval Systems' href = 'http://172.16.1.26:8089/' target = '_blank' rel = 'noopener'> Wazaran Approval Systems</a> to make approval/ rejection.</span></p>" + 
            //                "</body>";

            //    //arr.Add(new cArrayList("@msg", "#Cust:" + lbcust.Text + ",has retur full invoice, no:" + hdinvoice.Value + ",amt:" + lbbalance.Text + ",This is only information to you.Please login to Approval Web Site to make approval/rejection."));
            //    arr.Add(new cArrayList("@msg", "Branch "+ branchName + " #Cust:" + lbcust.Text + ",has retur full invoice, no:" + hdinvoice.Value + ",amt:" + lbbalance.Text + "remarks:"+ rem +",This is only information to you.Please login to Approval Web Site to make approval/rejection."));
            //    arr.Add(new cArrayList("@to", dr["mobile_no"]));
            //    arr.Add(new cArrayList("@token", "0"));
            //    arr.Add(new cArrayList("@doc_no", hdinvoice.Value.ToString()));
            //    arr.Add(new cArrayList("@doc_typ", "retfull"));
            //    bll.vInsertSmsOutbox(arr);
            //    string sSubject = "#Return full invoice.";
            //    string sBody = "Dears, <br/><br/>Customer return full invoice no." + txinvoiceno + " from " + Request.Cookies["sp"].Value.ToString() + " has been requested.<br/>Please login to Approval Web Site to make approval/rejection.";
            //    sBody += "<br>Wazaran Admin";
            //    List<string> lapproval = bll.lGetApproval("invFullRet", 1);
            //    string sEmailTo = lapproval[1];
            //    arr.Clear();
            //    arr.Add(new cArrayList("@emailsubject", sSubject));
            //    arr.Add(new cArrayList("@msg", htmlData));
            //    arr.Add(new cArrayList("@to", dr["email"]));
            //    arr.Add(new cArrayList("@doc_no", invIFR_no));
            //    arr.Add(new cArrayList("@doc_typ", "custtransfer"));
            //    arr.Add(new cArrayList("@file_attachment", sFilename));
            //    bll.vInsertEmailOutbox(arr);
            //}
            // Walking on the moon
            //arr.Clear();
            //arr.Add(new cArrayList("@stockcard_typ", "SALESCANCELFULL"));
            //arr.Add(new cArrayList("@refno", hdinvoice.Value.ToString()));
            //bll.vBatchStockCard(arr);
            arr.Clear();
            //string sSOCD = bll.vLookUp("select so_cd from tmst_dosales where inv_no='" + hdinvoice.Value.ToString() + "'");
            //arr.Add(new cArrayList("@so_cd", sSOCD));
            //arr.Add(new cArrayList("@dosales_sta_id", "E"));
            //bll.vUpdateMstDosalesByStatus(arr);
            // End of WOM
            // Info about full return
            arr.Clear();
            //arr.Add(new cArrayList("@inv_no", hdinvoice.Value.ToString()));
            //arr.Add(new cArrayList("@fretur_by", Request.Cookies["usr_id"].Value.ToString()));
            //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            //bll.vInsertTdosalesinvoice_fullreturn(arr);

            //btsave.CssClass = "divhid";
            btsave.CssClass = "btn btn-warning btn-save";
            //btprint.CssClass = "btn btn-info btn-print";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Invoice " + hdinvoice.Value.ToString() + " has saved successfully for Full Return, Please contact Branch Supervisor or Head Office!','Full Return Invoice','success');", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_returnfullinvoice");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        try
        {

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=cancelinv&iv=" + hdinvoice.Value.ToString() + "')", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_returnfullinvoice");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_returnfullinvoice.aspx");
    }
}