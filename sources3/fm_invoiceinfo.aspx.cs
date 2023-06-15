using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_invoiceinfo : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string statusBudget;
            string statusFree;
            string sinvNo = Request.QueryString["inv"];
            string rcv_dt = Request.QueryString["rcv"];
            string sdisc = Request.QueryString["disc"];

            statusBudget = bll.vLookUp("select tp.rdcost from tmst_discount td left join tmst_proposal tp on td.proposal_no = tp.prop_no where disc_cd = '" + sdisc + "' and tp.rdcost is not null");

            statusFree = bll.vLookUp("select distinct isnull(inv_no,'') inv_no from tdosalesinvoice_free where inv_no='"+sinvNo+"'");

            if (bll.vLookUp("select inv_no from tclaim_invoice where inv_no='" + sinvNo + "'") == "")
            {
                if (statusBudget == "principal")
                {
                    lbupfree.Visible = true;
                    uplf.Visible = true;
                    uplo.Visible = true;
                    lbupinv.Visible = true;
                }
                else
                {
                    lbupfree.Visible = true;
                    uplf.Visible = true;
                    uplo.Visible = true;
                    lbupinv.Visible = true;
                }
                if (statusFree == "") { lbupfree.Visible = false; uplf.Visible = false; } else { lbupfree.Visible = true; uplf.Visible = true; }
            }
            else
            {
                if (statusBudget == "principal")
                {
                    lbupfree.Visible = false;
                    uplf.Visible = false;
                    uplo.Visible = false;
                    lbupinv.Visible = false;
                }
                else
                {
                    lbupfree.Visible = false;
                    uplf.Visible = false;
                    uplo.Visible = false;
                    lbupinv.Visible = false;
                }
            }
            
            System.Data.SqlClient.SqlDataReader rs = null;
            //  grditem.Visible = false;
            //  grdprod.Visible = false;
            List<cArrayList> arr = new List<cArrayList>();
            
            string x = bll.vLookUp("select count(inv_no) from twrk_claimconfirm where disc_cd='" + sdisc + "' and inv_no='" + sinvNo + "'");
            if (x == "0")
            {
                arr.Add(new cArrayList("@disc_cd", sdisc));
                arr.Add(new cArrayList("@inv_no", sinvNo));
                bll.vInsertTWRKClaimConfirm(arr);
            }
            arr.Clear();
            arr.Add(new cArrayList("@inv_no", sinvNo.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["SP"].Value.ToString()));
            bll.vGetMstinvoice(arr, ref rs);
            while (rs.Read())
            {
                lbinvoice.Text = sinvNo;
                lbdisc.Text = sdisc;
                lbcust.Text = rs["cust_cd"].ToString() + "-" + rs["cust_nm"].ToString();
                lbotlcd.Text = rs["otlcd"].ToString();
                lbreceiveddt.Text = Convert.ToDateTime(rcv_dt).ToShortDateString();
                bll.sFormat2ddmmyyyy(ref lbreceiveddt);
                lbinvoicedt.Text = Convert.ToDateTime(rcv_dt).ToShortDateString();
                bll.sFormat2ddmmyyyy(ref lbinvoicedt);
                lbmanual.Text = rs["manual_no"].ToString();
                lbfree.Text = rs["freeno"].ToString();
                lborder.Text = rs["so_cd"].ToString();
                lbtype.Text = rs["so_typ"].ToString();
            } rs.Close();
            arr.Clear();
            arr.Add(new cArrayList("@type", "SALES"));
            arr.Add(new cArrayList("@inv_no", sinvNo));
            bll.vBindingGridToSp(ref grdinvoice, "sp_invoice_get", arr);
            arr.Clear();
            arr.Add(new cArrayList("@type", "FREE"));
            arr.Add(new cArrayList("@inv_no", sinvNo));
            bll.vBindingGridToSp(ref grdfree, "sp_invoice_get", arr);            
        }
    }



    protected void btscratch_Click(object sender, EventArgs e)
    {
        //foreach (GridViewRow row in grdinvoice.Rows)
        //{
        //    if (row.RowType == DataControlRowType.DataRow)
        //    {
        //        CheckBox chkRow = (row.Cells[0].FindControl("Scratch") as CheckBox);
        //        if (chkRow.Checked)
        //        {
        //            Label lbitem = (Label)row.FindControl("lbitem");
        //            List<cArrayList> arr = new List<cArrayList>();
        //            //arr.Add(new cArrayList("@item_Cd", lbitem.Text));
        //            //arr.Add(new cArrayList("@inv_no", lbinvoice.Text));
        //            //arr.Add(new cArrayList("@disc_Cd", lbdisc.Text));
        //            //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        //            //arr.Add(new cArrayList("@scratch_type", "order"));
        //            //bll.vInsertTclaimExclude(arr);
        //            arr.Add(new cArrayList("@inv_no", lbinvoice.Text));
        //            arr.Add(new cArrayList("@disc_cd", lbdisc.Text));
        //            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        //            arr.Add(new cArrayList("@qty", "0"));
        //            arr.Add(new cArrayList("@new_qty", "0"));
        //            arr.Add(new cArrayList("@item_cd", lbitem.Text));
        //            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        //            arr.Add(new cArrayList("@scratch_type", "order"));
        //            bll.vInsertWrkClaimExclude(arr);
        //        }
        //    }
        //}
        //foreach (GridViewRow row in grdfree.Rows)
        //{
        //    if (row.RowType == DataControlRowType.DataRow)
        //    {
        //        CheckBox chkRow = (row.Cells[0].FindControl("Scratch") as CheckBox);
        //        if (chkRow.Checked)
        //        {
        //            Label lbitem = (Label)row.FindControl("lbitem");
        //            List<cArrayList> arr = new List<cArrayList>();
        //            //arr.Add(new cArrayList("@item_Cd", lbitem.Text));
        //            //arr.Add(new cArrayList("@inv_no", lbinvoice.Text));
        //            //arr.Add(new cArrayList("@disc_Cd", lbdisc.Text));
        //            //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        //            //arr.Add(new cArrayList("@scratch_type", "free"));
        //            //bll.vInsertTclaimExclude(arr);
        //            arr.Add(new cArrayList("@inv_no", lbinvoice.Text));
        //            arr.Add(new cArrayList("@disc_cd", lbdisc.Text));
        //            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        //            arr.Add(new cArrayList("@qty", "0"));
        //            arr.Add(new cArrayList("@new_qty", "0"));
        //            arr.Add(new cArrayList("@item_cd", lbitem.Text));
        //            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        //            arr.Add(new cArrayList("@scratch_type", "free"));
        //            bll.vInsertWrkClaimExclude(arr);
        //        }
        //    }
        //}
        //Response.Redirect("fm_claimList.aspx", true);
        string statusBudget = bll.vLookUp("select tp.rdcost from tmst_discount td left join tmst_proposal tp on td.proposal_no = tp.prop_no where disc_cd = '" + lbdisc.Text + "' and tp.rdcost is not null");

        string statusFree = bll.vLookUp("select distinct isnull(inv_no,'') inv_no from tdosalesinvoice_free where inv_no='" + lbinvoice.Text + "'");

        if (bll.vLookUp("select inv_no from tclaim_invoice where inv_no='" + lbinvoice.Text + "'") == "")
        {
            if (!uplo.HasFile)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Please attach all the documents required','Invoice Scan!');", true);
                return;
            }

            List<cArrayList> arr = new List<cArrayList>();
            if (uplo.HasFile && uplf.HasFile)
            {
                FileInfo fio = new FileInfo(uplo.FileName);
                string exto = fio.Extension;
                byte[] fso = uplo.FileBytes;
                FileInfo fif = new FileInfo(uplf.FileName);
                string extf = fif.Extension;
                byte[] fsf = uplf.FileBytes;
                if (fso.Length <= 5242880 && fsf.Length <= 5242880)
                {
                    if ((uplo.FileName != "" && uplf.FileName != "") || (uplo.FileName != null && uplf.FileName != null))
                    {
                        arr.Add(new cArrayList("@fileinv", lbinvoice.Text + "_O_" + exto));
                        arr.Add(new cArrayList("@fileinv_f", lbinvoice.Text + "_F_" + extf));
                        uplo.SaveAs(bll.sGetControlParameter("image_path") + "/invoice_doc/" + lbinvoice.Text + "_O_" + exto);
                        uplf.SaveAs(bll.sGetControlParameter("image_path") + "/invoice_doc/" + lbinvoice.Text + "_F_" + extf);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Please attach all the documents required','Invoice Scan!');", true);
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Image size too big','Image size must be less than 5MB');", true);
                    return;
                }
                
                //2 file
            }
            else
            {
                if (uplo.HasFile)
                {
                    FileInfo fio = new FileInfo(uplo.FileName);
                    string exto = fio.Extension;
                    byte[] fso = uplo.FileBytes;
                    if (fso.Length <= 5242880)
                    {
                        if ((uplo.FileName != "") || (uplo.FileName != null))
                        {
                            arr.Add(new cArrayList("@fileinv", lbinvoice.Text + "_O_" + exto));
                            //arr.Add(new cArrayList("@fileinv_f", lbinvno.Text + "_F_"+ extf));
                            uplo.SaveAs(bll.sGetControlParameter("image_path") + "/invoice_doc/" + lbinvoice.Text + "_O_" + exto);
                            //uplf.SaveAs(bll.sGetControlParameter("image_path") + "/invoice_doc/" + lbinvno.Text + "_F_" + extf);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Please attach all the documents required','Invoice Scan!');", true);
                            return;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Image size too big','Image size must be less than 5MB');", true);
                        return;
                    }
                }
                if (statusFree != "")
                {
                    if (uplf.HasFile)
                    {
                        FileInfo fif = new FileInfo(uplf.FileName);
                        string extf = fif.Extension;
                        byte[] fsf = uplf.FileBytes;
                        if (fsf.Length <= 5242880)
                        {
                            if ((uplf.FileName != "") || (uplf.FileName != null))
                            {
                                //arr.Add(new cArrayList("@fileinv", lbinvoice.Text + "_O_" + exto));
                                arr.Add(new cArrayList("@fileinv_f", lbinvoice.Text + "_F_" + extf));
                                //uplo.SaveAs(bll.sGetControlParameter("image_path") + "/invoice_doc/" + lbinvoice.Text + "_O_" + exto);
                                uplf.SaveAs(bll.sGetControlParameter("image_path") + "/invoice_doc/" + lbinvoice.Text + "_F_" + extf);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Please attach all the documents required','Invoice Scan!');", true);
                                return;
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Image size too big','Image size must be less than 5MB');", true);
                            return;
                        }
                    }
                }                
            }
            arr.Add(new cArrayList("@inv_no", lbinvoice.Text));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vUploadInvoice(arr);
        }

        //string qtyDisc = bll.vLookUp("select min_qty from tdiscount_formula where disc_cd = '" + lbdisc.Text + "'");

        //string qtyInv = bll.vLookUp("select sum(case when c.new_qty is null then b.qty else c.new_qty end) as qty from tdosales_invoice a left join tdosalesinvoice_dtl b on a.inv_no = b.inv_no and a.salespointcd=b.salespointcd left join twrk_claimexclude c on b.inv_no=c.inv_no and b.salespointcd=c.salespointcd and b.item_cd=c.item_cd and c.usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and c.scratch_type='order' where a.inv_no = '" + lbinvoice.Text + "'");

        List<cArrayList> arr1 = new List<cArrayList>();
        arr1.Add(new cArrayList("@inv_no", lbinvoice.Text));
        arr1.Add(new cArrayList("@disc_cd", lbdisc.Text));
        arr1.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr1.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vUpdateWrkClaimExclude(arr1);
        arr1.Clear();
        arr1.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vDelWrkClaimExclude(arr1);
        Response.Write("<script>window.close();</" + "script>");
        Response.End();
    }

    protected void btclose_Click(object sender, EventArgs e)
    {
        if (bll.vLookUp("select 1 from tclaim_invoice where inv_no='" + lbinvoice.Text + "'") != null)
        {
            List<cArrayList> arr = new List<cArrayList>();
            if (bll.vLookUp("select 1 from tdosales_free where inv_no='" + Request.QueryString["inv"] + "'") != null)
            {
                if (uplo.HasFile && uplf.HasFile)
                {
                    FileInfo fio = new FileInfo(uplo.FileName);
                    string exto = fio.Extension;
                    byte[] fso = uplo.FileBytes;
                    FileInfo fif = new FileInfo(uplf.FileName);
                    string extf = fif.Extension;
                    byte[] fsf = uplf.FileBytes;
                    if (fso.Length <= 104857600 && fsf.Length <= 104857600)
                    {
                        if ((uplo.FileName != "" && uplf.FileName != "") || (uplo.FileName != null && uplf.FileName != null))
                        {
                            arr.Add(new cArrayList("@fileinv", lbinvoice.Text + "_O_" + exto));
                            arr.Add(new cArrayList("@fileinv_f", lbfree.Text + "_F_" + extf));
                            uplo.SaveAs(bll.sGetControlParameter("image_path") + "/invoice_doc/" + lbinvoice.Text + "_O_" + exto);
                            uplf.SaveAs(bll.sGetControlParameter("image_path") + "/invoice_doc/" + lbinvoice.Text + "_F_" + extf);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Please attach all the documents required','Invoice Scan!');", true);
                            return;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Image size too big','Image size must be less than 100MB');", true);
                        return;
                    }
                }
                //2 file
            }
            else
            {
                if (uplo.HasFile)
                {
                    FileInfo fio = new FileInfo(uplo.FileName);
                    string exto = fio.Extension;
                    byte[] fso = uplo.FileBytes;
                    if (fso.Length <= 104857600)
                    {
                        if ((uplo.FileName != "") || (uplo.FileName != null))
                        {
                            arr.Add(new cArrayList("@fileinv", lbinvoice.Text + "_O_" + exto));
                            //arr.Add(new cArrayList("@fileinv_f", lbinvno.Text + "_F_"+ extf));
                            uplo.SaveAs(bll.sGetControlParameter("image_path") + "/invoice_doc/" + lbinvoice.Text + "_O_" + exto);
                            //uplf.SaveAs(bll.sGetControlParameter("image_path") + "/invoice_doc/" + lbinvno.Text + "_F_" + extf);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Please attach all the documents required','Invoice Scan!');", true);
                            return;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Image size too big','Image size must be less than 100MB');", true);
                        return;
                    }
                }
            }
        }
        Response.Write("<script>window.close();</script>");
        Response.End();
    }
    protected void btdiscount_Click(object sender, EventArgs e)
    {

    }
    protected void grdinvoice_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdinvoice.EditIndex = e.NewEditIndex;
        List<cArrayList> arr = new List<cArrayList>();
        string sinvNo = lbinvoice.Text;
        arr.Add(new cArrayList("@type", "SALES"));
        arr.Add(new cArrayList("@inv_no", sinvNo));
        bll.vBindingGridToSp(ref grdinvoice, "sp_invoice_get", arr);
    }
    protected void grdinvoice_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string sinvNo = lbinvoice.Text;
        string sdisc = lbdisc.Text;
        Label lbitemcd = (Label)grdinvoice.Rows[e.RowIndex].FindControl("lbitem");
        TextBox txqty = (TextBox)grdinvoice.Rows[e.RowIndex].FindControl("txqty");
        Label lbqty = (Label)grdinvoice.Rows[e.RowIndex].FindControl("lbqty");
        TextBox txdiscamt = (TextBox)grdinvoice.Rows[e.RowIndex].FindControl("txdisc_amt");
        Label lbdiscamt = (Label)grdinvoice.Rows[e.RowIndex].FindControl("lbdisc_amt");
        double dOut = 0;
        if (!double.TryParse(txqty.Text, out dOut))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Please enter numeric format','Numeric Only','warning');", true);
            return;
        }
        if (!double.TryParse(txdiscamt.Text, out dOut))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Please enter numeric format','Numeric Only','warning');", true);
            return;
        }

        List<cArrayList> arr = new List<cArrayList>();

        arr.Add(new cArrayList("@inv_no", sinvNo));
        arr.Add(new cArrayList("@disc_cd", sdisc));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@qty", "0"));
        arr.Add(new cArrayList("@new_qty", txqty.Text));
        arr.Add(new cArrayList("@disc", "0"));
        arr.Add(new cArrayList("@new_disc", txdiscamt.Text));
        arr.Add(new cArrayList("@item_cd", lbitemcd.Text));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@scratch_type", "order"));
        bll.vInsertWrkClaimExclude(arr);
        grdinvoice.EditIndex = -1;
        arr.Clear();
        arr.Add(new cArrayList("@type", "SALES"));
        arr.Add(new cArrayList("@inv_no", sinvNo));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdinvoice, "sp_invoice_get", arr);
    }
    protected void grdinvoice_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdinvoice.EditIndex = -1;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@type", "SALES"));
        arr.Add(new cArrayList("@inv_no", lbinvoice.Text));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdinvoice, "sp_invoice_get", arr);
    }
    protected void grdfree_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdfree.EditIndex = -1;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Clear();
        arr.Add(new cArrayList("@type", "FREE"));
        arr.Add(new cArrayList("@inv_no", lbinvoice.Text));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdfree, "sp_invoice_get", arr);
    }
    protected void grdfree_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string sinvNo = lbinvoice.Text;
        string sdisc = lbdisc.Text;
        Label lbitemcd = (Label)grdfree.Rows[e.RowIndex].FindControl("lbitem");
        TextBox txqty = (TextBox)grdfree.Rows[e.RowIndex].FindControl("txqty");
        Label lbqty = (Label)grdfree.Rows[e.RowIndex].FindControl("lbqty");
        double dOut = 0;
        if (!double.TryParse(txqty.Text, out dOut))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Please enter numeric format','Numeric Only','warning');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@inv_no", sinvNo));
        arr.Add(new cArrayList("@disc_cd", sdisc));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@qty", "0"));
        arr.Add(new cArrayList("@disc", "0"));
        arr.Add(new cArrayList("@new_disc", "0"));
        arr.Add(new cArrayList("@new_qty", txqty.Text));
        arr.Add(new cArrayList("@item_cd", lbitemcd.Text));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@scratch_type", "free"));
        bll.vInsertWrkClaimExclude(arr);
        grdfree.EditIndex = -1;
        arr.Clear();
        arr.Add(new cArrayList("@type", "FREE"));
        arr.Add(new cArrayList("@inv_no", sinvNo));        
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdfree, "sp_invoice_get", arr);
    }
    protected void grdfree_RowEditing(object sender, GridViewEditEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        grdfree.EditIndex = e.NewEditIndex;
        string sinvNo = lbinvoice.Text;
        arr.Add(new cArrayList("@type", "FREE"));
        arr.Add(new cArrayList("@inv_no", sinvNo));
        bll.vBindingGridToSp(ref grdfree, "sp_invoice_get", arr);
    }
    protected void btcalc_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@inv_no", lbinvoice.Text));
        arr.Add(new cArrayList("@disc_cd", lbdisc.Text));
        //bll.vBatchTakeOrderDiscount(arr);
        //arr.Clear();
        //arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        //bll.vBindingGridToSp(ref grddisc, "sp_twrk_salesdiscount_get", arr);
        //grd.DataSource = null;
        //grd.DataBind();
        //bll.vBindingGridToSp(ref grd, "sp_twrk_salesorderdtl_get", arr);
    }
}