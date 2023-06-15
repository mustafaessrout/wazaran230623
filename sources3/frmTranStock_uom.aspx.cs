using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using AjaxControlToolkit;
using System.IO;
public partial class frmTranStock_uom : System.Web.UI.Page
{
    cbll bll = new cbll();
    creport rep = new creport();
    decimal totalQty = 0;
    decimal totalAmount = 0;
    public static int PreviousIndex;
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            try
            {

                dttrnstkDate.Enabled = false;
                lbdoc5.Visible = false;
                txtrnstkNo.Text = "NEW";
                lbstatus.Text = bll.vLookUp("select fld_desc from tfield_value where fld_nm='sta_id' and fld_valu='N'");
                bll.vLookUp("delete tblTrnStock_document where trnstkno='NEW' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
                dttrnstkDate.Text = Request.Cookies["waz_dt"].Value.ToString();
                bll.vBindingComboToSp(ref cbSalesPointCD, "sp_tmst_salespoint_get", "salespointcd", "salespoint_desc");
                cbSalesPointCD.SelectedValue = Request.Cookies["sp"].Value.ToString();

                //bll.vBindingFieldValueToCombo(ref cbUOM, "uom");
                bll.vBindingFieldValueToComboWithEmptyChoosen(ref cbUOM, "uom_tf", "uom");
                bll.vBindingFieldValueToComboWithEmptyChoosen(ref cbUOM2, "uom_tf", "uom");
                cbUOM.SelectedValue = "CTN";
                cbUOM2.SelectedValue = "PCS";
                //cbUOM.CssClass = "ro";
                //cbUOM2.CssClass = "ro";
                cd.v_disablecontrol(cbUOM);
                cd.v_disablecontrol(cbUOM2);
                txqty.Text = "0";
                txqty2.Text = "0";
               
                bll.vBindingComboToSpWithEmptyChoosen(ref cbinvtype, "sp_invtype_get", "invtype", "stktypeName");
                //cbinvtype_SelectedIndexChanged(sender, e);
                //cbinvtype.CssClass = "form-control";
                cd.v_enablecontrol(cbinvtype);
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vBindingComboToSp(ref cbwhs_cd, "sp_tmst_warehouse_get", "whs_cd", "whs_nm", arr);

                if (txtrnstkNo.Text == "NEW" || txtrnstkNo.Text == "")
                {
                    bll.vBindingComboToSp(ref ddpspv, "sp_get_tbltrnstock_not_appv", "supervisor_cd", "emp_nm",arr);
                    Panel1.Visible = false;
                }
                else
                {
                    bll.vBindingComboToSp(ref ddpspv, "sp_get_prodspv", "supervisor_cd", "name");
                    Panel1.Visible = true;
                    bindinggrddoc();
                    ddpspv.Enabled = false;
                }
                bindinggrd();
                arr.Clear();
                arr.Add(new cArrayList("@trnstkNo", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vBindingGridToSp(ref grd, "sp_del_tblTrnEmp", arr);
                PreviousIndex = cbinvtype.SelectedIndex;
                dttrnstkDate_CalendarExtender.StartDate = DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
                vClaim.Visible = false;
            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : frmTranStock");
                Response.Redirect("fm_ErrorPage.aspx");
            }
            finally
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            }
        }
    }
    private void bindinggrd()
    {
        try
        {
            totalQty = 0;
            totalAmount = 0;
            string trnstkNo;
            if (txtrnstkNo.Text == "" || txtrnstkNo.Text == "NEW") { trnstkNo = Request.Cookies["usr_id"].Value.ToString(); } else { trnstkNo = txtrnstkNo.Text; }
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@trnstkNo", trnstkNo));
            arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue));
            arr.Add(new cArrayList("@invtype", cbinvtype.SelectedValue));
            bll.vBindingGridToSp(ref grd, "sp_tbltrnstockDtl_get2", arr);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : frmTranStock");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    private void bindinggrddoc()
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@trnstkNo", txtrnstkNo.Text));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@doc_typ", "DESTROY"));
            arr.Add(new cArrayList("@status", lbstatus.Text));
            bll.vBindingGridToSp(ref grddoc, "sp_tmst_document_get3", arr);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : frmTranStock");
            Response.Redirect("fm_ErrorPage.aspx");
        }

    }
    protected void bttmp_Click(object sender, EventArgs e)
    {
        try
        {

            SqlDataReader rs = null;
            string sstatus;
            bll.vBindingComboToSp(ref ddpspv, "sp_get_prodspv", "supervisor_cd", "name");
            txtrnstkNo.Text = Convert.ToString(Session["lootrnstkNo"]);
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@trnstkNo", txtrnstkNo.Text));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vGettbltrnstock(arr, ref rs);
            while (rs.Read())
            {
                string sdate = string.Format("{0:d/M/yyyy}", rs["trnstkDate"]);
                DateTime dtdate = Convert.ToDateTime(sdate);
                string strDate = dtdate.ToString("d/M/yyyy");
                sstatus = rs["sta_id"].ToString();
                dttrnstkDate.Text = strDate;//string.Format("{0:d/M/yyyy}", rs["trnstkDate"]);
                cbSalesPointCD.SelectedValue = rs["salespointcd"].ToString();
                cbinvtype.SelectedValue = rs["invtype"].ToString();
                cbinvtype_SelectedIndexChanged(sender, e);
                cbwhs_cd.SelectedValue = rs["whs_CD"].ToString();
                cbbin_cd.SelectedValue = rs["bin_cd"].ToString();
                ddpspv.SelectedValue = rs["prod_spv"].ToString();
                ddpspv.Enabled = false;
                txtrnstkRemark.Text = rs["trnstkRemark"].ToString();
                chclaim.Checked = Convert.ToBoolean(rs["claim"].ToString());
                lbstatus.Text = bll.vLookUp("select fld_desc from tfield_value where fld_nm='sta_id' and fld_valu='" + sstatus + "'");
                if (txtrnstkNo.Text == "NEW" || txtrnstkNo.Text == "")
                {
                    Panel1.Visible = false;
                    if (cbinvtype.SelectedValue == "12")
                    {
                        btdestroy.Visible = true;
                    }
                    else
                    {
                        btdestroy.Visible = false;
                    }
                }
                else
                {
                    if (cbinvtype.SelectedValue == "12")
                    {
                        Panel1.Visible = true;
                        btdestroy.Visible = false;
                        bindinggrddoc();
                    }
                    else
                    {
                        Panel1.Visible = false;
                        btdestroy.Visible = false;
                    }
                }
                bindinggrd();
            }
            rs.Close();
            bindinggrd();

            btsave.Visible = false;
            btadd.Visible = false;

            if (lbstatus.Text == "APPROVED" || lbstatus.Text == "COMPLETE")
            {
                SqlDataReader rsa = null;
                lbdoc5.Visible = true;
                arr.Clear();
                arr.Add(new cArrayList("@doc_cd", "DST5"));
                arr.Add(new cArrayList("@trnstkno", txtrnstkNo.Text));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vGetTranstckApp(arr, ref rsa);
                while (rsa.Read())
                {
                    lbldoc5.Text = rsa["filename"].ToString();
                    lbldoc5.NavigateUrl = "/images/" + rsa["filename"].ToString();
                }
                rsa.Close();
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : frmTranStock");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmTranStock_uom.aspx");
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            if (lbstatus.Text == "COMPLETE")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Destroy Completed','Success');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                return;
            }
            int count = 0;
            foreach (GridViewRow row in grddoc.Rows)
            {
                try
                {
                    if (count > 0) { break; }
                    Label lbfilename = (Label)row.FindControl("lbfilename");
                    System.Diagnostics.Debug.WriteLine(lbfilename.Text);
                    if (cbinvtype.SelectedValue == "12" && (lbfilename.Text == "" || lbfilename.Text == null))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "aal", "sweetAlert('Attachment is Null ','Please Add Photo in  Photo Before','warning');", true);
                        return;
                    }
                    count++;
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "aal", "sweetAlert('File not uploaded','Please click update to upload file','warning');", true);
                    return;
                }

            }

            if (grd.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opd", "preventBack();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('No Item In Detail','Please Add Item In the Detail','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                return;
            }
            if (lbstatus.Text == "COMPLETE")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opd", "preventBack();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction already complete can not be save','error');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                return;
            }
            if (txtrnstkRemark.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opd", "preventBack();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Please entry remark','error');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);

                return;
            }
            if (lbstatus.Text == "APPROVED" && cbinvtype.SelectedValue == "12")
            {

                foreach (GridViewRow row in grddoc.Rows)
                {
                    Label lbfilename = (Label)row.FindControl("lbfilename");
                    Label docnm = (Label)row.FindControl("docnm");
                    if (lbfilename.Text == "" || lbfilename.Text == null)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "aal", "sweetAlert('Attachment is Null ','Please Add Photo in " + docnm.Text + "','warning');", true);
                        return;
                    }
                }

                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@trnstkno", txtrnstkNo.Text));
                arr.Add(new cArrayList("@sta_id", "C"));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vAppDestroy(arr);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "asl", "sweetAlert('Destroy Complete','" + txtrnstkNo.Text + " destroyed','success');", true);
                lbstatus.Text = "COMPLETE";
            }
            if ((txtrnstkNo.Text == "NEW" || txtrnstkNo.Text == "") && lbstatus.Text == "NEW")
            {
                if (Request.Cookies["waz_dt"].Value.ToString() != dttrnstkDate.Text)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opd", "preventBack();", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction Date Out Of Periode Date','error');", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                    return;
                }
                int totItemNull = 0;
                totItemNull = int.Parse(bll.vLookUp("select count(*) from tbltrnstockdtl where (item_cd='' or item_cd is null) and trnstkNo = '"+ Request.Cookies["usr_id"].Value.ToString() + "' "));

                if (totItemNull > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opd", "preventBack();", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','No Item Detected in this Transaction. Pleas Add the correct item !!!','error');", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                    return;
                }

                string atechmn = "";
                string strnstkNo = "";
                List<cArrayList> arr = new List<cArrayList>();
                //arr.Add(new cArrayList("@trnstkNo", txtrnstkNo.Text));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@trnstkDate", DateTime.ParseExact(dttrnstkDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@invtype", cbinvtype.SelectedValue));
                arr.Add(new cArrayList("@whs_CD", cbwhs_cd.SelectedValue));
                arr.Add(new cArrayList("@bin_cd", cbbin_cd.SelectedValue));
                arr.Add(new cArrayList("@trnstkRemark", txtrnstkRemark.Text));
                arr.Add(new cArrayList("@createdtby", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@claim", chclaim.Checked));
                arr.Add(new cArrayList("@trnstkmanno", txtrnstkmanno.Text));
                arr.Add(new cArrayList("@prod_spv", ddpspv.SelectedValue));
                bll.vInserttbltrnstock(arr, ref strnstkNo);
                txtrnstkNo.Text = strnstkNo;
                // send email

                string sinvtypename;
                sinvtypename = cbinvtype.SelectedItem.Text;
                //if  (cbinvtype.SelectedValue=="12")
                //{ 

                //List<string> lInfo = bll.ltrnstockApproval("trnstockApproval");
                List<string> lapproval = bll.lGetApproval(sinvtypename, 1);
                //Random rnd = new Random();
                //int nRnd = rnd.Next(1000, 9999);
                string ssp = Request.Cookies["sp"].Value.ToString();
                string ssp1 = ssp.Substring(0, 1);
                string ssp2 = ssp.Substring(0, 1) + ssp.Substring(ssp.Length - 1);
                string strantoken = "00" + bll.vLookUp("select fld_valu from tfield_value where fld_nm='trantoken' and fld_desc='internaltransfer'");
                string stoken = txtrnstkNo.Text.Substring(txtrnstkNo.Text.Length - 4);
                if (stoken.Substring(0, 3) == "000") { stoken = strantoken + stoken.Substring(stoken.Length - 1); }
                if (stoken.Substring(0, 2) == "00") { stoken = ssp2 + stoken.Substring(stoken.Length - 2); }
                if (stoken.Substring(0, 1) == "0") { stoken = ssp1 + stoken.Substring(stoken.Length - 3); }
                string stoken2 = bll.vLookUp("select short_ip from tmst_salespoint where salespointcd=(select parm_valu from tcontrol_parameter where parm_nm='salespoint')") + stoken;
                string sMsg = sinvtypename + " " + cbSalesPointCD.SelectedItem.Text + ", no." + txtrnstkNo.Text + ", detail item in email, do you want to approved : (Y/N)" + stoken2;
                // cd.vSendSms(sMsg, lapproval[0]); 
                //arr.Clear();
                //arr.Add(new cArrayList("@token", nRnd.ToString()));
                //arr.Add(new cArrayList("@doc_typ", "destroy"));
                //arr.Add(new cArrayList("@to", lapproval[0]));
                //arr.Add(new cArrayList("@doc_no", txtrnstkNo.Text));
                //arr.Add(new cArrayList("@msg", sMsg.TrimEnd() + nRnd.ToString()));
                //bll.vInsertSMSSent(arr);

                //arr.Clear();
                //arr.Add(new cArrayList("@token", stoken2));
                //arr.Add(new cArrayList("@msg", sMsg));
                //arr.Add(new cArrayList("@doc_typ", sinvtypename));
                //arr.Add(new cArrayList("@to", lapproval[0]));
                //arr.Add(new cArrayList("@doc_no", txtrnstkNo.Text));
                //bll.vInsertSmsOutbox(arr); //by yanto 15-11-2016

                //string slink_ho = bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='link_ho'");
                string slink_branch = bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='link_branch'");
                String sText = "<html><head><body>Dear, New stock " + sinvtypename + " has been created , with no. " + txtrnstkNo.Text +
                    "<p> Date  : " + dttrnstkDate.Text + " Salespoint : " + cbSalesPointCD.SelectedItem +
                     //"<tr><td>Warehouse </td><td>:</td><td>" + cbwhs_cd.SelectedItem.Text + "</td>" +
                     //"<tr<td>Note </td><td>:</td><td>" + txtrnstkRemark.Text + </td></tr>
                     "<p> for detail please see attached file </p>" +
                     "<p>Please Click this  for approved : <a href='" + slink_branch + "/landingpage.aspx?trnname=trnstock&salespointcd=" + cbSalesPointCD.SelectedValue + "&RefNo=" + txtrnstkNo.Text + "&appcode=" + stoken.ToString() + "&sta=A'>Approve</a>, or for rejected please click <a href='" + slink_branch + "/landingpage.aspx?trnname=trnstock&salespointcd=" + cbSalesPointCD.SelectedItem + "&RefNo=" + txtrnstkNo.Text + "&appcode=" + stoken.ToString() + "&sta=L'>Reject</a></p>";
                //"<p>Please Click this  for approved : <a href='" + slink_ho + "/landingpage.aspx?trnname=trnstock&salespointcd=" + cbSalesPointCD.SelectedValue + "&appcode=" + nRnd.ToString() + "&sta=A'>Approve</a>, or for rejected please click <a href='" + slink_ho + "/landingpage.aspx?trnname=trnstock&salespointcd=" + cbSalesPointCD.SelectedValue + "&appcode=" + nRnd.ToString() + "&sta=R'>Reject</a></p>";
                //<table><tr><td>Item Cd</td><td>Item Name</td><td>Size</td><td>Branded</td><td>Qty</td><td>Uom</td><td>Unit Price</td><td>Amount</td></tr>";
                //string sItem = "";
                //System.Data.SqlClient.SqlDataReader rs = null;
                //arr.Clear();
                //arr.Add(new cArrayList("@trnstkNo", txtrnstkNo.Text));
                //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //arr.Add(new cArrayList("@invtype", cbinvtype.SelectedValue));
                //bll.vGettbltrnstockDtl(arr, ref rs);
                //while (rs.Read())
                //{
                //    sItem += "<tr><td>" + rs["item_cd"].ToString() + "</td><td>" + rs["item_nm"] + "</td><td>" + rs["size"].ToString() + "</td><td>" + rs["branded_nm"].ToString() + "</td><td>" + rs["qty"].ToString() + "</td><td>" + rs["uom"].ToString() + "</td><td>" + rs["unitprice"].ToString() + "</td><td>" + rs["Amount"].ToString() + "</td></tr>";
                //}
                //rs.Close();
                //sText += sItem + "</table><p>Please Click this  for approved : <a href='" + Request.Url.GetLeftPart(UriPartial.Authority) + "/landingpage.aspx?trnname='trnstock'&salespointcd='" + cbSalesPointCD.SelectedValue + "'&appcode=" + nRnd.ToString() + "&sta=A'>Approve</a>, or for rejected please click <a href='" + Request.Url.GetLeftPart(UriPartial.Authority) + "/landingpage.aspx?trnname='trnstock'&salespointcd='" + cbSalesPointCD.SelectedValue + "'&appcode=" + nRnd.ToString() + "&sta=R'>Reject</a></p>";

                //sText += "</br></br>Note : If you not take action , within 3 days this  will be disabled.";
                //"<p>Please Click this  for approved : <a href='" + Request.Url.GetLeftPart(UriPartial.Authority) + "/landingpage.aspx?trnname=trnstock&salespointcd=" + cbSalesPointCD.SelectedValue + "&appcode=" + nRnd.ToString() + "&sta=A'>Approve</a>, or for rejected please click <a href='" + Request.Url.GetLeftPart(UriPartial.Authority) + "/landingpage.aspx?trnname=trnstock&salespointcd=" + cbSalesPointCD.SelectedValue + "&appcode=" + nRnd.ToString() + "&sta=R'>Reject</a></p>";
                List<string> lapproval2 = bll.lGetApprovalall(sinvtypename);
                int n = sText.Length;
                string sPath = bll.sGetControlParameter("image_path")+"stock\\";
                string sPdfName = sinvtypename + txtrnstkNo.Text + ".pdf";
                string atch = sPdfName + ";";


                SqlDataReader rs = null;
                arr.Clear();
                arr.Add(new cArrayList("@trnstkno", "NEW"));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vGetTblTrnStockDocumentUpd(arr, ref rs);
                while (rs.Read())
                {
                    var sourcePath = sPath + rs["filename"].ToString();
                    var exe = Path.GetExtension(sourcePath);
                    var destinationPath = sPath + rs["salespointcd"].ToString() + strnstkNo + rs["doc_cd"].ToString() + exe;
                    System.IO.File.Move(sourcePath, destinationPath);

                    arr.Clear();
                    arr.Add(new cArrayList("@trnstkno", strnstkNo));
                    arr.Add(new cArrayList("@filenmbf", rs["filename"].ToString()));
                    arr.Add(new cArrayList("@filenmaf", rs["salespointcd"].ToString() + strnstkNo + rs["doc_cd"].ToString() + exe));
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    bll.vTrnStockSetDocumentUpd(arr);

                    //atch += rs["salespointcd"].ToString() + strnstkNo + rs["doc_cd"].ToString() + exe + ";";
                    atechmn = rs["salespointcd"].ToString() + strnstkNo + rs["doc_cd"].ToString() + exe;
                }
                arr.Clear();
                arr.Add(new cArrayList("@trnstkNo", txtrnstkNo.Text));
                arr.Add(new cArrayList("@printby", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue));
                rep.vShowReportToPDFWithSP("rptTranStock_uom.rpt", arr, sPath + sPdfName);
                string sSubject = "New " + sinvtypename + " Has Been Created";
                string semail = "0";
                int i = 0;
                foreach (var email in lapproval2.Skip(1))
                {
                    i = i + 1;
                    if (i % 2 != 0)
                    {
                        if (semail == "0")
                            semail = email;
                        else
                            semail = semail + ';' + email;
                    }
                }
                //bll.vSendMail(semail, sSubject, sText);
                //bll.vSendMail(semail, sSubject, sText, sPdfName);
                //arr.Clear();
                //arr.Add(new cArrayList("@token", stoken));
                //arr.Add(new cArrayList("@doc_typ", sinvtypename));
                //arr.Add(new cArrayList("@to", semail));
                //arr.Add(new cArrayList("@doc_no", txtrnstkNo.Text));
                //arr.Add(new cArrayList("@emailsubject", sSubject));
                //arr.Add(new cArrayList("@msg", sText));
                //arr.Add(new cArrayList("@file_attachment", sPdfName));
                //bll.vInsertEmailOutbox(arr); //by yanto 23-1-2017
                bll.vLookUp("update tbltrnstock set fileAttachment ='" + sPdfName + "' where trnstkno='" + txtrnstkNo.Text + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");


                //arr.Clear();
                //arr.Add(new cArrayList("@trxcd", "stock " + sinvtypename));
                //arr.Add(new cArrayList("@token", stoken));
                //arr.Add(new cArrayList("@doc_no", txtrnstkNo.Text));
                //bll.vInsertEmailSent(arr);
                bindinggrddoc();


                //}
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opd", "preventBack();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Data Save successfully ..','Tran No. " + txtrnstkNo.Text + "','info');", true);
                ////ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Destroy Completed','Success','success');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                // if (bll.vLookUp("select count(*) from  tblTrnStock_document where trnstkno='" + txtrnstkNo.Text + "' and salespointcd='" + cbSalesPointCD.SelectedValue + "' and [filename]   is null") == "0")
                // {
                //List<cArrayList> arr = new List<cArrayList>();
                //arr.Add(new cArrayList("@trnstkNo", txtrnstkNo.Text));
                //arr.Add(new cArrayList("@salespointCD", cbSalesPointCD.SelectedValue));
                //bll.vUpdatetblTrnStock(arr);
                //lbstatus.Text = "COMPLETE";
                // }

                //    if (lbstatus.Text != "NEW")
                //    {
                //        //Discount Document
                //        foreach (GridViewRow row in grddoc.Rows)
                //        {

                //            FileUpload upl = (FileUpload)row.FindControl("upl");
                //            string ss = upl.FileName;
                //            Label lbdoccode = (Label)row.FindControl("lbdoccode");
                //            List<cArrayList> arr = new List<cArrayList>();
                //            arr.Add(new cArrayList("@trnstkno", txtrnstkNo.Text));
                //            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //            arr.Add(new cArrayList("@doc_cd", lbdoccode.Text));
                //            if ((upl.FileName != "") || (upl.FileName.Equals(null)))
                //            {
                //                arr.Add(new cArrayList("@filename", txtrnstkNo.Text + lbdoccode.Text + ".jpg"));
                //                upl.SaveAs(bll.sGetControlParameter("image_path") + txtrnstkNo.Text + lbdoccode.Text + ".jpg");
                //            }
                //            bll.vInserttblTrnStock_document(arr);
                //        }
                //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Update Data  successfully ..','Tran No. " + txtrnstkNo.Text + "','info');", true);
                //    }
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : frmTranStock");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btDelete_Click(object sender, EventArgs e)
    {
        try
        {
            if (lbstatus.Text == "COMPLETE")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction already complete can not be delete','error');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                return;
            }
            if (Request.Cookies["waz_dt"].Value.ToString() != dttrnstkDate.Text)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction Date Out Of Periode Date','error');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                return;
            }
            if (txtrnstkNo.Text != "" || txtrnstkNo.Text != "NEW")
            {
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@trnstkNo", txtrnstkNo.Text));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vDeletetbltrnstock(arr);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Data Deleted successfully !','error');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                Response.Redirect("frmTranStock_uom.aspx");
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : frmTranStock");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@trnstkNo", txtrnstkNo.Text));
            arr.Add(new cArrayList("@printby", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue));
            Session["lParamtrnstk"] = arr;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=trnstk');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : frmTranStock");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        try
        {
            if (lbstatus.Text == "COMPLETE")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction already complete can not be save','error');", true);
                return;
            }
            if (Request.Cookies["waz_dt"].Value.ToString() != dttrnstkDate.Text)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction Date Out Of Periode Date','error');", true);
                return;
            }
            if (txsearchitem.Text == "" || hditem.Value.ToString() == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Please fill Item Name coloumn','warning');", true);

                return;
            }
            //if (cbUOM.SelectedIndex == 0)
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Please fill UOM coloumn','warning');", true);

            //    return;
            //}
            getprice();

            double dQtyCheck = 0, dQtyCheck2 = 0;
            if (!double.TryParse(txqty.Text, out dQtyCheck) || !double.TryParse(txqty2.Text, out dQtyCheck2))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Qty must be numeric','Check Qty','warning');", true);
                return;
            }
            if (Convert.ToDouble(txqty.Text) < 0 || Convert.ToDouble(txqty2.Text) < 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Qty must bigger then zero','Check Qty','warning');", true);
                return;
            }

            // Loss / Destroy Stock
            if (cbinvtype.SelectedValue.ToString() != "13")
            {

                DateTime ddate = DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                //double dStock = 0, dQty = 0;
                //string sStock = "";

                double dStock = 0, dQty = 0, dQty2 = 0, dQtyTotal = 0;
                string sStock = "", sStock_conv = "";

                sStock = bll.vLookUp("select isnull(dbo.[sfnGetStockBooking]('" + Request.Cookies["sp"].Value.ToString() + "','" + hditem.Value.ToString() + "','" + cbbin_cd.SelectedValue.ToString() + "','" + cbwhs_cd.SelectedValue.ToString() + "','0','" + ddate.Year + "-" + ddate.Month + "-" + ddate.Day + "'),0)");

                dStock = double.Parse(sStock);
                dQty = double.Parse(txqty.Text);
                dQty2 = double.Parse(txqty2.Text);
                dQtyTotal = dQty + (double.Parse(bll.vLookUp("select dbo.sfnUomQtyConv('" + hditem.Value.ToString() + "','" + cbUOM2.SelectedValue.ToString() + "','CTN','" + txqty2.Text + "')")));
                sStock_conv = bll.vLookUp("select dbo.fn_getqtyconv('" + hditem.Value.ToString() + "','CTN'," + dStock.ToString() + ")");

                //dStock = double.Parse(sStock);
                //dQty = double.Parse(txqty.Text);

                //dQty = double.Parse(bll.vLookUp("select dbo.sfnUomQtyConv('" + hditem.Value.ToString() + "','" + cbUOM.SelectedValue.ToString() + "','CTN','" + txqty.Text + "')"));
                if (dStock < dQtyTotal)
                {
                    string smsgstockallert = "Stock is not enough ! " + hditem.Value.ToString() + " Qty Stock :" + sStock_conv.ToString() + " Qty Transfer : " + dQtyTotal.ToString();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('" + smsgstockallert + "' ,'Stock Not Enough','warning');", true);
                    return;
                }

            }

            List<cArrayList> arr = new List<cArrayList>();
            String trnstkNo;
            if (txtrnstkNo.Text == "" || txtrnstkNo.Text == "NEW")
            {
                trnstkNo = Request.Cookies["usr_id"].Value.ToString();
            }
            else { trnstkNo = txtrnstkNo.Text; }
            arr.Add(new cArrayList("@trnstkNo", trnstkNo));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@invtype", cbinvtype.SelectedValue));
            arr.Add(new cArrayList("@item_cd", hditem.Value.ToString()));
            arr.Add(new cArrayList("@qty", txqty.Text));
            arr.Add(new cArrayList("@qty2", txqty2.Text));
            arr.Add(new cArrayList("@uom", cbUOM.SelectedValue.ToString()));
            arr.Add(new cArrayList("@uom2", cbUOM2.SelectedValue.ToString()));
            arr.Add(new cArrayList("@unitprice", txunitprice.Text));
            bll.vInserttbltrnstockDtl(arr);
            bindinggrd();
            hditem.Value = "";
            txqty.Text = "0";
            txqty2.Text = "0";
            txunitprice.Text = "0";
            txsearchitem.Text = "";
            txsearchitem.Focus();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : frmTranStock");
            Response.Redirect("fm_ErrorPage.aspx");
        }

    }
    protected void cbwhs_cd_SelectedIndexChanged(object sender, EventArgs e)
    {
        //List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@whs_cd", cbwhs_cd.SelectedValue.ToString()));
        //bll.vBindingComboToSp(ref cbbin_cd, "sp_twarehouse_bin_get", "bin_cd", "bin_nm", arr);
    }

    protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {

            grd.EditIndex = -1;
            bindinggrd();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : frmTranStock");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {

            TextBox txtqty = (TextBox)grd.Rows[e.RowIndex].FindControl("txtqty");
            DropDownList cboUOM = (DropDownList)grd.Rows[e.RowIndex].FindControl("cboUOM");
            Label lblsalespointCD = (Label)grd.Rows[e.RowIndex].FindControl("lblsalespointCD");
            Label lbltrnstkNo = (Label)grd.Rows[e.RowIndex].FindControl("lbltrnstkNo");
            Label lbitemcode = (Label)grd.Rows[e.RowIndex].FindControl("lbitemcode");

            if (Convert.ToDecimal(txtqty.Text) > Convert.ToDecimal(maxQty.Value))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Qty To Much','Qty can not more than " + maxQty.Value + "','error');", true);
                return;
            }

            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@trnstkNo", lbltrnstkNo.Text));
            arr.Add(new cArrayList("@salespointCD", lblsalespointCD.Text));
            arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
            arr.Add(new cArrayList("@qty", txtqty.Text));
            arr.Add(new cArrayList("@uom", cboUOM.SelectedValue));
            bll.vUpdatetbltrnstockDtl(arr);
            grd.EditIndex = -1; arr.Clear();
            bindinggrd();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : frmTranStock");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    
    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            if (lbstatus.Text == "COMPLETE")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction already complete can not be delete','error');", true);
                return;
            }
            if (Request.Cookies["waz_dt"].Value.ToString() != dttrnstkDate.Text)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction Date Out Of Periode Date','error');", true);
                return;
            }
            Label lblsalespointCD = (Label)grd.Rows[e.RowIndex].FindControl("lblsalespointCD");
            Label lbltrnstkNo = (Label)grd.Rows[e.RowIndex].FindControl("lbltrnstkNo");
            Label lbitemcode = (Label)grd.Rows[e.RowIndex].FindControl("lbitemcode");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@trnstkNo", lbltrnstkNo.Text));
            arr.Add(new cArrayList("@salespointCD", lblsalespointCD.Text));
            arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
            bll.vDeletetbltrnstockDtl(arr);
            bindinggrd();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : frmTranStock");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {

            Label lblqty = (Label)grd.Rows[e.NewEditIndex].FindControl("lblqty");
            Label lbitemcode = (Label)grd.Rows[e.NewEditIndex].FindControl("lbitemcode");
            string trnstkNo;
            if (txtrnstkNo.Text == "" || txtrnstkNo.Text == "NEW") { trnstkNo = Request.Cookies["usr_id"].Value.ToString(); } else { trnstkNo = txtrnstkNo.Text; }

            maxQty.Value = bll.vLookUp("select  dbo.fn_getqtydestroy( '" + cbwhs_cd.SelectedValue + "', '" + lbitemcode.Text + "', '" + ddpspv.Text + "','"+ Request.Cookies["sp"].Value.ToString() + "')");

            if (lbstatus.Text == "COMPLETE")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction already complete can not be edit','error');", true);
                return;
            }
            if (Request.Cookies["waz_dt"].Value.ToString() != dttrnstkDate.Text)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction Date Out Of Periode Date','error');", true);
                grd.EditIndex = -1;
                bindinggrd();
                return;
            }
            Label lblUOM = (Label)grd.Rows[e.NewEditIndex].FindControl("lblUOM");
            grd.EditIndex = e.NewEditIndex;
            bindinggrd();
            DropDownList cboUOM = (DropDownList)grd.Rows[e.NewEditIndex].FindControl("cboUOM");
            bll.vBindingFieldValueToCombo(ref cboUOM, "uom");
            cboUOM.SelectedValue = lblUOM.Text;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "editTable", "editTable();", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : frmTranStock");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            grd.PageIndex = e.NewPageIndex;
            bindinggrd();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : frmTranStock");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }


    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        SqlDataReader rs = null;
        string sItem = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        List<string> lItem = new List<string>();
        arr.Add(new cArrayList("@item_nm", prefixText));
        bll.vSearchMstItem2(arr, ref rs);
        while (rs.Read())
        {
            sItem = AutoCompleteExtender.CreateAutoCompleteItem(rs["item_cd"].ToString() + " | " + rs["item_shortname"].ToString() + " | " + rs["item_nm"].ToString() , rs["item_cd"].ToString());
            lItem.Add(sItem);
        } rs.Close();
        return (lItem.ToArray());
    }

   
    protected void cbinvtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            cbinvtype.CssClass = "form-control ro";
            string GetPreviousValue = cbinvtype.Items[PreviousIndex].Value;
            btdestroy.Visible = false;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "console.log('" + GetPreviousValue + "');", true);
            //if (PreviousIndex != cbinvtype.SelectedIndex)
            //{
            //    cbinvtype.Enabled = false;
            //    cbinvtype.CssClass = "form-control ro";
            //}
            cbwhs_cd.Enabled = true;
            if (cbinvtype.SelectedValue == "13")  //add
            {
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vBindingComboToSp(ref cbwhs_cd, "sp_tmst_warehouse_tmst_vehicle_salesman_get", "whs_cd", "whs_desc", arr);
            }
            else
            {
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vBindingComboToSp(ref cbwhs_cd, "sp_tmst_warehouse_get", "whs_cd", "whs_nm", arr);
            }
            PreviousIndex = cbinvtype.SelectedIndex;
            if (cbinvtype.SelectedValue == "12")  //destroy
            {
                bll.vBindingComboToSp(ref cbbin_cd, "sp_uom_destroy_get", "fld_valu", "fld_desc");
                //chclaim.Visible = true;
                //dv.Visible = true;
                //btdestroy.Visible = true;
                //Panel2.Visible = false;
                //Panel1.Visible = true;
                //bindinggrddoc();

                chclaim.Visible = false;
                dv.Visible = false;
                btdestroy.Visible = false;
                Panel2.Visible = true;
                Panel1.Visible = false;

            }
            else
            {
                bll.vBindingComboToSp(ref cbbin_cd, "sp_uom_add_lose_get", "fld_valu", "fld_desc");
                chclaim.Visible = false;
                dv.Visible = false;
                btdestroy.Visible = false;
                Panel2.Visible = true;
                Panel1.Visible = false;
            }

            bindinggrd();

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : frmTranStock");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btdestroy_Click(object sender, EventArgs e)
    {
        try
        {

            if (ddpspv.SelectedValue == "" || ddpspv.SelectedValue == null)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Please Select Product SPV','error');", true);
                return;
            }

            if (lbstatus.Text == "COMPLETE")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction already complete can not be save','error');", true);
                return;
            }
            if (Request.Cookies["waz_dt"].Value.ToString() != dttrnstkDate.Text)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction Date Out Of Periode Date','error');", true);
                return;
            }
            string sdestroy = bll.vLookUp("select dbo.fn_checkdestroy('"+ Request.Cookies["sp"].Value.ToString() + "')");
            if (sdestroy == "ok")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al2", "sweetAlert('no Item tobe destroy !','empty data','warning');", true);
                return;
            }
            if (txtrnstkNo.Text == "" || txtrnstkNo.Text == "NEW")
            {
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@invtype", cbinvtype.SelectedValue));
                arr.Add(new cArrayList("@whs_cd", cbwhs_cd.SelectedValue));
                arr.Add(new cArrayList("@bin_cd", cbbin_cd.Text));
                arr.Add(new cArrayList("@dtdate", DateTime.ParseExact(dttrnstkDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@userid", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@prodSpv", ddpspv.SelectedValue));
                bll.vInserttbltrnstockDtl_destroy(arr);
                bindinggrd();
                bindinggrddoc();
                grddoc.Visible = true;
                Panel1.Visible = true;
                btdestroy.Visible = false;
                cbinvtype.Enabled = false;
                cbwhs_cd.Enabled = false;
                ddpspv.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : frmTranStock");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        
    }
    protected void txsearchitem_TextChanged(object sender, EventArgs e)
    {
        //txunitprice.Text = bll.vLookUp("select price_sell from tmst_item where item_cd='" + hditem.Value + "'");
        cbUOM_SelectedIndexChanged(sender, e);
    }

    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblqty = (Label)e.Row.FindControl("lblqty");
                Label lblAmount = (Label)e.Row.FindControl("lblAmount");
                decimal qty;
                decimal amount;
                if (lblAmount != null)
                    amount = decimal.Parse(lblAmount.Text);
                else
                    amount = 0;
                if (lblqty != null)
                    qty = decimal.Parse(lblqty.Text);
                else
                    qty = 0;
                totalQty = totalQty + qty;
                totalAmount = totalAmount + amount;
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalqty = (Label)e.Row.FindControl("lblTotalqty");
                Label lblTotalAmount = (Label)e.Row.FindControl("lblTotalAmount");
                lblTotalqty.Text = totalQty.ToString();
                lblTotalAmount.Text = totalAmount.ToString("#,##0.00");
            }

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : frmTranStock");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }


    protected void grddoc_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {

            Label lbdoccode = (Label)grddoc.Rows[e.NewEditIndex].FindControl("lbdoccode");

            if (lbstatus.Text == "APPROVED")
            {
                if (lbdoccode.Text == "DST1" || lbdoccode.Text == "DST2")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "aal", "sweetAlert('Destroy Already Approved','You Can't change or upload file in Branch Supervisor Approval and Photo Before	','warning');", true);
                    grddoc.EditIndex = -1;
                    bindinggrddoc();
                }
                else
                {
                    grddoc.EditIndex = e.NewEditIndex;
                    bindinggrddoc();
                }
            }
            else if (lbstatus.Text == "COMPLETE")
            {

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "aal", "sweetAlert('Destroy Already Approved','You Can't change or upload file in Branch Supervisor Approval and Photo Before	','warning');", true);
                grddoc.EditIndex = -1;
                bindinggrddoc();
            }
            else
            {
                grddoc.EditIndex = e.NewEditIndex;
                bindinggrddoc();
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : frmTranStock");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grddoc_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            grddoc.EditIndex = -1;
            bindinggrddoc();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : frmTranStock");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grddoc_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {

            Label lbdoccode = (Label)grddoc.Rows[e.RowIndex].FindControl("lbdoccode");
            FileUpload FileUpload1 = (FileUpload)grddoc.Rows[e.RowIndex].FindControl("FileUpload1");
            int size = FileUpload1.PostedFile.ContentLength;
            if (size > 981753)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "aal", "sweetAlert('File Size To Large','your maximal file size is 1Mb','warning');", true);
                return;
            }

            if (FileUpload1.HasFile)
            {
                string imgpatch = bll.sGetControlParameter("image_path") + "stock\\" + cbSalesPointCD.SelectedValue + txtrnstkNo.Text + lbdoccode.Text + System.IO.Path.GetExtension(FileUpload1.FileName);
                string imgname = cbSalesPointCD.SelectedValue + txtrnstkNo.Text + lbdoccode.Text + System.IO.Path.GetExtension(FileUpload1.FileName);
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@trnstkNo", txtrnstkNo.Text));
                arr.Add(new cArrayList("@salespointCD", cbSalesPointCD.SelectedValue));
                arr.Add(new cArrayList("@doc_cd", lbdoccode.Text));
                arr.Add(new cArrayList("@filename", imgname));
                //  arr.Add(new cArrayList("@path", Server.MapPath(FileUpload1.FileName)));
                FileUpload1.SaveAs(imgpatch);
                bll.vUpdatetblTrnStock_document(arr);
            }


            grddoc.EditIndex = -1;
            bindinggrddoc();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : frmTranStock");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void cbUOM_SelectedIndexChanged(object sender, EventArgs e)
    {
        getprice();
    }
    private void getprice()
    {
        try
        {
            double dPrice = 0;
            string sCustType = "";
            sCustType = bll.vLookUp("select top 1 fld_valu from tfield_value where fld_nm='otlbrn'");
            dPrice = bll.dGetItemPrice(hditem.Value.ToString(), sCustType, ( (double.Parse(txqty2.Text) > 0 ) ? cbUOM2.SelectedValue.ToString() : cbUOM.SelectedValue.ToString() ));
            txunitprice.Text = dPrice.ToString();

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : frmTranStock");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
}

                    