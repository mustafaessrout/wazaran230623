using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_claimconfirm : System.Web.UI.Page
{
    cbll bll = new cbll();

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        {
            bll.vBindingGridToSp(ref grdCLAIM, "sp_tclaimconfirm_get");
            //cbbranch.SelectedValue = Request.Cookies["sp"].Value.ToString();
            //cbbranch.Enabled = false;
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tablePageCopy", "tablePageCopy();", true);
    }
    protected void chkboxSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkBoxHeader = (CheckBox)grdCLAIM.HeaderRow.FindControl("chkboxSelectAll");
        foreach (GridViewRow row in grdCLAIM.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("approve");
            if (ChkBoxHeader.Checked == true)
            {
                ChkBoxRows.Checked = true;
            }
            else
            {
                ChkBoxRows.Checked = false;
            }
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tablePageCopy", "tablePageCopy();", true);
    }
    protected void btconfirm_Click(object sender, EventArgs e)
    {
        try
        {
            string statusBudget = "";
            string invoiceOrder = "";
            foreach (GridViewRow row in grdCLAIM.Rows)
            {
                //string dtp1 = Request.Cookies["waz_dt"].ToString("d/M/yyyy");
                DateTime dtpayp1 = DateTime.ParseExact(Request.Cookies["waz_dt"].Value, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("approve") as CheckBox);
                    if (chkRow.Checked)
                    {
                        CheckBox chpostpone = (CheckBox)row.FindControl("chpostpone");

                        Label lbType = (Label)row.FindControl("lbType");
                        Label lbinvno = (Label)row.FindControl("lbinvoice");
                        Label lbdisc = (Label)row.FindControl("lbdiscount");
                        TextBox txremark = (TextBox)row.FindControl("txremark");
                        CheckBox chsign = (CheckBox)row.FindControl("chsign");
                        CheckBox chexclude = (CheckBox)row.FindControl("chexclude");

                        //FileUpload uplo = (FileUpload)row.FindControl("uplo");
                        //FileUpload uplf = (FileUpload)row.FindControl("uplf");
                        List<cArrayList> arr = new List<cArrayList>();

                        if (chpostpone.Checked)
                        {
                            string status = bll.vLookUp("select count(*) as total from tclaim_confirm_postpone where disc_cd='" + lbdisc.Text + "' and inv_no='" + lbinvno.Text + "' and  datediff(day,convert(date,received_dt,104),convert(date,postpone_dt,104)) >= (select parm_valu from tcontrol_parameter where parm_nm='maxdailypostpone')");

                            if (status == "0")
                            {
                                arr.Clear();
                                arr.Add(new cArrayList("@disc_cd", lbdisc.Text));
                                arr.Add(new cArrayList("@inv_no", lbinvno.Text));
                                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                                arr.Add(new cArrayList("@received_dt", dtpayp1.Year + "-" + dtpayp1.Month + "-" + dtpayp1.Day));
                                arr.Add(new cArrayList("@postpone_dt", dtpayp1.Year + "-" + dtpayp1.Month + "-" + dtpayp1.Day));
                                bll.vInsertClaimConfirmPostpone(arr);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('This Invoice can not Postpone','Please check the invoice!','warning');", true);
                                return;
                            }

                        }
                        else
                        {
                            arr.Clear();
                            //countInv = bll.vLookUp("select * from ");
                            arr.Add(new cArrayList("@disc_cd", lbdisc.Text));
                            if (chsign.Checked)
                            {
                                arr.Add(new cArrayList("@isstamp", "1"));
                            }
                            else
                            {
                                arr.Add(new cArrayList("@isstamp", "0"));
                                if (txremark.Text == "")
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Remark Can not empty, If No Stamp.','Please check the Stamp or Remark can not Empty !','warning');", true);
                                    return;
                                }
                                else
                                {
                                    arr.Add(new cArrayList("@remark", txremark.Text));
                                }
                            }

                            invoiceOrder = bll.vLookUp("SELECT isnull(fileinv,fileinv_f) as fileinv FROM tclaim_invoice where inv_no='" + lbinvno.Text + "'");

                            if (invoiceOrder == "")
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('This invoice, not yet upload!!!','Please upload this invoice first!','warning');", true);
                                return;
                            }

                            arr.Add(new cArrayList("@issign", "1"));
                            arr.Add(new cArrayList("@isexclude", chexclude.Checked == true ? "1" : "0"));
                            arr.Add(new cArrayList("@inv_no", lbinvno.Text));
                            arr.Add(new cArrayList("@received_dt", dtpayp1.Year + "-" + dtpayp1.Month + "-" + dtpayp1.Day));
                            //statusBudget = bll.vLookUp("select tp.rdcost from tmst_discount td left join tmst_proposal tp on td.proposal_no = tp.prop_no where disc_cd = '" + lbdisc.Text + "' and tp.rdcost is not null");
                            bll.vInsertClaimConfirm(arr);
                            arr.Clear();
                            arr.Add(new cArrayList("@inv_no", lbinvno.Text));
                            arr.Add(new cArrayList("@disc_cd", lbdisc.Text));
                            bll.vDelClaimConfirmPostpone(arr);
                        }



                        //if (statusBudget == "principal")
                        //{
                        //    if (lbType.Text == "FREE")
                        //    {
                        //        if (uplo.HasFile && uplf.HasFile)
                        //        {
                        //            FileInfo fio = new FileInfo(uplo.FileName);
                        //            string exto = fio.Extension;
                        //            byte[] fso = uplo.FileBytes;
                        //            FileInfo fif = new FileInfo(uplf.FileName);
                        //            string extf = fif.Extension;
                        //            byte[] fsf = uplf.FileBytes;
                        //            if (fso.Length <= 104857600 && fsf.Length <= 104857600)
                        //            {
                        //                if ((uplo.FileName != "" && uplf.FileName != "") || (uplo.FileName != null && uplf.FileName != null))
                        //                {
                        //                    arr.Add(new cArrayList("@fileinv", lbinvno.Text + "_O_" + exto));
                        //                    arr.Add(new cArrayList("@fileinv_f", lbinvno.Text + "_F_"+ extf));
                        //                    uplo.SaveAs(bll.sGetControlParameter("image_path") + "/invoice_doc/" + lbinvno.Text + "_O_" + exto);
                        //                    uplf.SaveAs(bll.sGetControlParameter("image_path") + "/invoice_doc/" + lbinvno.Text + "_F_" + extf);
                        //                }
                        //                else
                        //                {
                        //                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Please attach all the documents required','Invoice Scan!');", true);
                        //                    return;
                        //                }
                        //            }
                        //            else
                        //            {
                        //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Image size too big','Image size must be less than 100MB');", true);
                        //                return;
                        //            }
                        //        }
                        //        else
                        //        {
                        //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Upload Invoice needed, if the budget from Principal.','Please scan the invoice !','warning');", true);
                        //            return;
                        //        }
                        //    }
                        //    else
                        //    {
                        //        if (uplo.HasFile)
                        //        {
                        //            FileInfo fio = new FileInfo(uplo.FileName);
                        //            string exto = fio.Extension;
                        //            byte[] fso = uplo.FileBytes;
                        //            if (fso.Length <= 104857600)
                        //            {
                        //                if ((uplo.FileName != "") || (uplo.FileName != null))
                        //                {
                        //                    arr.Add(new cArrayList("@fileinv", lbinvno.Text + "_O_" + exto));
                        //                    //arr.Add(new cArrayList("@fileinv_f", lbinvno.Text + "_F_"+ extf));
                        //                    uplo.SaveAs(bll.sGetControlParameter("image_path") + "/invoice_doc/" + lbinvno.Text + "_O_" + exto);
                        //                    //uplf.SaveAs(bll.sGetControlParameter("image_path") + "/invoice_doc/" + lbinvno.Text + "_F_" + extf);
                        //                }
                        //                else
                        //                {
                        //                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Please attach all the documents required','Invoice Scan!');", true);
                        //                    return;
                        //                }
                        //            }
                        //            else
                        //            {
                        //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Image size too big','Image size must be less than 100MB');", true);
                        //                return;
                        //            }
                        //        }
                        //        else
                        //        {
                        //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Upload Invoice needed, if the budget from Principal.','Please scan the invoice !','warning');", true);
                        //            return;
                        //        }
                        //    }
                        //}

                    }
                }
            }
        }
        catch (Exception ex)
        {
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : Daily Upload Invoice");
        }

        bll.vBindingGridToSp(ref grdCLAIM, "sp_tclaimconfirm_get");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tablePageCopy", "tablePageCopy();", true);
    }
    protected void invoice_Click(object sender, EventArgs e)
    {
        hdopinv.Value = "1";
    }
    protected void grdCLAIM_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdCLAIM.PageIndex = e.NewPageIndex;
        bll.vBindingGridToSp(ref grdCLAIM, "sp_tclaimconfirm_get");
    }
    protected void chapprove_CheckedChanged(object sender, EventArgs e)
    {

        foreach (GridViewRow row in grdCLAIM.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("approve") as CheckBox);
                if (chkRow.Checked)
                {
                    Label lbinvno = (Label)row.FindControl("lbinvoice");
                    Label lbdisc = (Label)row.FindControl("lbdiscount");
                    List<cArrayList> arr = new List<cArrayList>();
                    string x = bll.vLookUp("select count(inv_no) from twrk_claimconfirm where disc_cd='" + lbdisc.Text + "' and inv_no='" + lbinvno.Text + "'");
                    if (x == "1")
                    {
                        chkRow.Checked = true;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Check invoice detail','Please open the invoice and check if there is item needs to scratch !','warning');", true);
                        chkRow.Checked = false;
                        return;


                    }
                }
            }
        }
        //if (hdopinv.Value.ToString() == "1")
        //{
        //    hdopinv.Value = "0";
        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Check invoice detail','Please open the invoice and check if there is item needs to scratch !','warning');", true);
        //    foreach (GridViewRow row in grdCLAIM.Rows)
        //    {
        //        CheckBox approve = (CheckBox)row.FindControl("approve");
        //        approve.Checked = false;
        //    }
        //    return;
        //}

    }
    //protected void lnkinv_Click(object sender, EventArgs e)
    //{

    //    //foreach (GridViewRow row in grdCLAIM.Rows)
    //    //{
    //    //    //string dtp1 = Request.Cookies["waz_dt"].ToString("d/M/yyyy");
    //    //    DateTime dtpayp1 = DateTime.ParseExact(Request.Cookies["waz_dt"].Value, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);

    //    //    if (row.RowType == DataControlRowType.DataRow)
    //    //    {

    //    //        Label lbinvno = (Label)row.FindControl("lbinvoice");
    //    //        Label lbdisc = (Label)row.FindControl("lbdiscount");
    //    //        List<cArrayList> arr = new List<cArrayList>();
    //    //        arr.Add(new cArrayList("@disc_cd", lbdisc.Text));
    //    //        arr.Add(new cArrayList("@inv_no", lbinvno.Text));
    //    //        bll.vInsertTWRKClaimConfirm(arr);

    //    //    }
    //    //}
    //}
    protected void grdCLAIM_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //foreach (GridViewRow row in grdCLAIM.Rows)
        //{
        //    Label lbType = (Label)row.FindControl("lbType");
        //    Label lbfreeInv = (Label)row.FindControl("lbFreeInv");
        //    FileUpload uplf = (FileUpload)row.FindControl("uplf");
        //    if (lbType.Text == "FREE")
        //    {
        //        uplf.Attributes.Remove("style");
        //        lbfreeInv.Attributes.Remove("style");
        //    }
        //    else
        //    {
        //        uplf.Attributes.Add("style", "display:none");
        //        lbfreeInv.Attributes.Add("style", "display:none");
        //    }
        //}
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@search", txsearch.Text.ToString()));
        bll.vBindingGridToSp(ref grdCLAIM, "sp_tclaimconfirm_get", arr);
    }
}