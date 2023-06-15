using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

public partial class fm_reprintDocument : System.Web.UI.Page
{
    string invoiceNo = "";
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                string sho;
                string waz_dt;
                waz_dt = Request.Cookies["waz_dt"].Value.ToString();
                DateTime dtwaz_dt = DateTime.ParseExact(waz_dt, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                List<cArrayList> arr = new List<cArrayList>();
                arr.Clear();
                arr.Add(new cArrayList("@qry_cd", "SalesJob"));
                bll.vBindingComboToSpWithEmptyChoosen(ref cbsalesman, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
                arr.Clear();
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                bll.vBindingComboToSpWithEmptyChoosen(ref cbSalesPointCD, "sp_tmst_salespoint_getuser", "salespointcd", "salespoint_desc", arr);
                sho = Request.Cookies["sp"].Value.ToString();
                if (sho == bll.sGetControlParameter("salespointho"))
                {
                    cbSalesPointCD.Enabled = true;
                }
                else
                {
                    cbSalesPointCD.SelectedValue = sho;
                    cbSalesPointCD.Enabled = false;
                    //cbSalesPointCD.CssClass = "makeitreadonly ro form-control";
                    cd.v_disablecontrol(cbSalesPointCD);
                }
                cd.v_disablecontrol(cbsalesman);
                //cbSalesPointCD_SelectedIndexChanged(sender, e);
            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_reprintDocument");
                Response.Redirect("fm_ErrorPage.aspx");
            }
        }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCustomerList(string prefixText, int count)
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
        bll.vSearchMstCustomerInRPS(arr, ref rs);
        while (rs.Read())
        {
            sCust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + "-" + rs["cust_nm"].ToString() + " - " + rs["cust_arabic"].ToString(), rs["cust_cd"].ToString());
            lCust.Add(sCust);
        }
        rs.Close();
        return (lCust.ToArray());
    }
    protected void grd_reprintDoc_RowDataBound(Object sender, GridViewRowEventArgs e)
    {
        try
        {
            e.Row.Attributes.Add("style", "cursor:pointer;");
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowState == DataControlRowState.Alternate)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='orange'");
                    //e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#47A3DA'");
                    //e.Row.BackColor = Color.FromName("#47A3DA");
                }
            }
            else
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='orange'");
                    //e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#FFF'");
                    //e.Row.BackColor = Color.FromName("#FFF");
                }
            }

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_reprintDocument");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void grd_reprintDoc_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //try
        //{

        //    grd_reprintDoc.PageIndex = e.NewPageIndex;
        //    string invNo = txt_txNo.Text;

        //    List<cArrayList> arr = new List<cArrayList>();
        //    //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        //    arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue.ToString()));
        //    if (invNo != "")
        //    {
        //        arr.Add(new cArrayList("@number", invNo.Trim()));
        //    }

        //    if (cbtype.SelectedValue.ToString() == "TO")
        //    {
        //        bll.vBindingGridToSp(ref grd_reprintDoc, "sp_tmst_reprintDocSO", arr);
        //    }
        //    else if (cbtype.SelectedValue.ToString() == "CO")
        //    {
        //        bll.vBindingGridToSp(ref grd_reprintDoc, "sp_tmst_reprintDocCO", arr);
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
        //    bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_reprintDocument");
        //    Response.Redirect("fm_ErrorPage.aspx");
        //}
        try
        {
            grd_reprintDoc.PageIndex = e.NewPageIndex;
            List<cArrayList> arr = new List<cArrayList>();
            //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue));

            if (cbtype.SelectedValue.ToString() == "TO")
            {
                grd_reprintDoc.Visible = true;
                grdCashout.Visible = false;
                grdint.Visible = false;
                arr.Add(new cArrayList("@salesman_cd", cbsalesman.SelectedValue));
                arr.Add(new cArrayList("@cust_cd", hdcustomer.Value));
                bll.vBindingGridToSp(ref grd_reprintDoc, "sp_tmst_reprintDocSO", arr);
            }
            else if (cbtype.SelectedValue.ToString() == "CO")
            {
                grd_reprintDoc.Visible = true;
                grdCashout.Visible = false;
                grdint.Visible = false;
                bll.vBindingGridToSp(ref grd_reprintDoc, "sp_tmst_reprintDocCO", arr);
            }
            else if (cbtype.SelectedValue.ToString() == "IB")
            {
                grd_reprintDoc.Visible = true;
                grdCashout.Visible = false;
                grdint.Visible = false;
                bll.vBindingGridToSp(ref grd_reprintDoc, "sp_tmst_reprintDocIB", arr);
            }
            else if (cbtype.SelectedValue.ToString() == "2")
            {
                grd_reprintDoc.Visible = false;
                grdCashout.Visible = true;
                grdint.Visible = false;
                bll.vBindingGridToSp(ref grdCashout, "sp_tmst_reprintDocCHO", arr);
            }
            else if (cbtype.SelectedValue.ToString() == "3")
            {
                grdCashout.Visible = false;
                grd_reprintDoc.Visible = false;
                grdint.Visible = true;
                bll.vBindingGridToSp(ref grdint, "sp_tmst_reprintDocint", arr);
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_reprintDocument");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        finally
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
        }

    }

    protected void cbtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            List<cArrayList> arr = new List<cArrayList>();
            //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue));

            if (cbtype.SelectedValue.ToString() == "TO")
            {
                grd_reprintDoc.Visible = true;
                grdCashout.Visible = false;
                grdint.Visible = false;
                arr.Add(new cArrayList("@salesman_cd", cbsalesman.SelectedValue));
                arr.Add(new cArrayList("@cust_cd", hdcustomer.Value));
                bll.vBindingGridToSp(ref grd_reprintDoc, "sp_tmst_reprintDocSO", arr);
            }
            else if (cbtype.SelectedValue.ToString() == "CO")
            {
                grd_reprintDoc.Visible = true;
                grdCashout.Visible = false;
                grdint.Visible = false;
                arr.Add(new cArrayList("@cust_cd", hdcustomer.Value));
                bll.vBindingGridToSp(ref grd_reprintDoc, "sp_tmst_reprintDocCO", arr);
            }
            else if (cbtype.SelectedValue.ToString() == "IB")
            {
                grd_reprintDoc.Visible = true;
                grdCashout.Visible = false;
                grdint.Visible = false;
                bll.vBindingGridToSp(ref grd_reprintDoc, "sp_tmst_reprintDocIB", arr);
            }
            else if (cbtype.SelectedValue.ToString() == "2")
            {
                grd_reprintDoc.Visible = false;
                grdCashout.Visible = true;
                grdint.Visible = false;
                bll.vBindingGridToSp(ref grdCashout, "sp_tmst_reprintDocCHO", arr);
            }
            else if (cbtype.SelectedValue.ToString() == "3")
            {
                grdCashout.Visible = false;
                grd_reprintDoc.Visible = false;
                grdint.Visible = true;
                bll.vBindingGridToSp(ref grdint, "sp_tmst_reprintDocint", arr);
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_reprintDocument");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        finally
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
        }
    }

    protected void grd_reprintDoc_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction Date Out Of Periode Date','error');", true);
    }
    string x;
    protected void grd_reprintDoc_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            x = grd_reprintDoc.SelectedRow.Cells[0].Text;
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_reprintDocument");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        //List<cArrayList> arr = new List<cArrayList>();
        //arr.Clear();
        //string invoiceNo = grd_reprintDoc.SelectedRow.Cells[0].Text;
        //if (cbtype.SelectedValue.ToString() == "TO")
        //{

        //    arr.Add(new cArrayList("@print_cd", "TOINV")); //Take Order INvoice
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "reprintDoc", "openreport1('fm_report2.aspx?src=so&so=" + invoiceNo + "','fm_report2.aspx?src=so1&so=" + invoiceNo + "');", true);
        //}
        //else if (cbtype.SelectedValue.ToString() == "CO")
        //{
        //    string getInv = bll.vLookUp("SELECT inv_no from tmst_dosales where so_cd='" + invoiceNo + "'");
        //    arr.Clear();
        //    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        //    arr.Add(new cArrayList("@inv_no", getInv));
        //    bll.vInsertRptDosalesInvoice(arr);
        //    arr.Clear();
        //    arr.Add(new cArrayList("@print_cd", "CANVINV")); //Canvas INvoice            
        //    string sDiscAmt = bll.vLookUp("select sum(amt) from tcanvasorder_disccash where so_cd='" + invoiceNo + "'");
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=inv&no=" + getInv + "&amt=" + sDiscAmt + "');", true);
        //}

        //arr.Add(new cArrayList("@doc_no", invoiceNo));
        //bll.vInsertPrintControl(arr);


    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        try
        {

            List<cArrayList> arr = new List<cArrayList>();
            arr.Clear();
            GridViewRow grd = ((LinkButton)sender).NamingContainer as GridViewRow;
            Label lblsocd = (Label)grd.FindControl("so_cd");
            string sInvNo = bll.vLookUp("select inv_no from tmst_dosales where so_cd='" + lblsocd.Text + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            Label txmanualno = (Label)grd.FindControl("txmanualno");
            Label lbinv_no = (Label)grd.FindControl("lbinv_no");
            x = lblsocd.Text;
            if (cbtype.SelectedValue.ToString() == "TO")
            {
                if (bll.nCheckAccess("reprintloadto", Request.Cookies["usr_id"].Value.ToString()) == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','Print Loading Take Order !!','warning');", true);
                    return;
                }
                string sSta = bll.vLookUp("select so_sta_id from tmst_salesorder where so_cd='" + x + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                if (sSta == "N")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You you can not print this TO because not yet print from TO Screen','Plese print from main screen TO !!','warning');", true);
                    return;
                }
                arr.Clear();
                arr.Add(new cArrayList("@print_cd", "TOINV"));
                arr.Add(new cArrayList("@doc_no", lbinv_no.Text));
                //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue.ToString()));
                bll.vInsertPrintControl(arr);
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "reprintDoc", "openreport1('fm_report2.aspx?src=so_uom&so=" + x + "','fm_report2.aspx?src=so1_uom&so=" + x + "');", true);


                string sDiscAmt = bll.vLookUp("select sum(amt) from tcanvasorder_disccash where so_cd='" + x + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                string getInv = bll.vLookUp("SELECT inv_no from tmst_dosales where so_cd='" + x + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "reprintDoc", "openreport1('fm_report2.aspx?src=so_uom&so=" + x + "','fm_report2.aspx?src=blwprice&no=" + getInv + "&amt=" + sDiscAmt + "');", true);
            }
            else if (cbtype.SelectedValue.ToString() == "CO")
            {

                string getInv = bll.vLookUp("SELECT inv_no from tmst_dosales where so_cd='" + x + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                arr.Clear();
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@inv_no", sInvNo));
                //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue.ToString()));
                bll.vInsertRptDosalesInvoice(arr);

                // arr.Add(new cArrayList("@manual_no", ))
                arr.Clear();
                arr.Add(new cArrayList("@print_cd", "CANVINV")); //Canvas INvoice    
                arr.Add(new cArrayList("@doc_no", getInv));
                //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue.ToString()));
                bll.vInsertPrintControl(arr);
                string sDiscAmt = bll.vLookUp("select sum(amt) from tcanvasorder_disccash where so_cd='" + x + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=inv_uom&no=" + getInv + "&amt=" + sDiscAmt + "');", true);
                //bll.vInsertPrintControl(arr);
            }
            if (cbtype.SelectedValue.ToString() == "IB")
            {
                if (bll.nCheckAccess("reprintloadto", Request.Cookies["usr_id"].Value.ToString()) == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','Print Loading Take Order !!','warning');", true);
                    return;
                }
                string sSta = bll.vLookUp("select so_sta_id from tmst_salesorder where so_cd='" + x + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                if (sSta == "N")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You you can not print this TO because not yet print from TO Screen','Plese print from main screen TO !!','warning');", true);
                    return;
                }
                arr.Clear();
                arr.Add(new cArrayList("@print_cd", "CONTRACTINV"));
                arr.Add(new cArrayList("@doc_no", lbinv_no.Text));
                //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue.ToString()));
                bll.vInsertPrintControl(arr);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "reprintDoc", "openreport1('fm_report2.aspx?src=sofree&so=" + x + "');", true);
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_reprintDocument");
            Response.Redirect("fm_ErrorPage.aspx");
        }


    }

    protected void LinkButton2_Click(object sender, EventArgs e) //Print invoice
    {
        try
        {

            if (bll.nCheckAccess("reprintinvto", Request.Cookies["usr_id"].Value.ToString()) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','Print invoice Take Order !!','warning');", true);
                return;
            }

            List<cArrayList> arr = new List<cArrayList>();
            arr.Clear();
            GridViewRow grd = ((LinkButton)sender).NamingContainer as GridViewRow;
            Label lblsocd = (Label)grd.FindControl("so_cd");
            Label txmanualno = (Label)grd.FindControl("txmanualno");
            x = lblsocd.Text;
            string sSta = bll.vLookUp("select so_sta_id from tmst_salesorder where so_cd='" + x + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            if ((sSta == "L") || (sSta == "N"))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You you can not print this TO because not yet print from TO Screen','Plese print from main screen TO !!','warning');", true);
                return;
            }
            string sDiscAmt = bll.vLookUp("select sum(amt) from tcanvasorder_disccash where so_cd='" + x + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            string getInv = bll.vLookUp("SELECT inv_no from tmst_dosales where so_cd='" + x + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            string manual_no = bll.vLookUp("select manual_no from tdosales_invoice where inv_no='" + getInv + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");

            if (cbtype.SelectedValue.ToString() == "TO")
            {
                /*if (manual_no  == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Invoice for '"+ x +"'not available','error');", true);
                    return;
                }*/

                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@inv_no", getInv));
                //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue.ToString()));
                bll.vInsertRptDosalesInvoice(arr);
                //arr.Clear();
                //arr.Add(new cArrayList("@inv_no", getInv));
                //arr.Add(new cArrayList("@manual_no", txmanualno.Text));
                //bll.vUpdateReprintDocByManualNo(arr);
                arr.Clear();

                arr.Add(new cArrayList("@print_cd", "TOINV")); //Take Order INvoice
                arr.Add(new cArrayList("@doc_no", getInv));
                //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue.ToString()));
                bll.vInsertPrintControl(arr);

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport1('fm_report2.aspx?src=invto_uom&no=" + getInv + "&amt=" + sDiscAmt + "');", true);
            }
            else if (cbtype.SelectedValue.ToString() == "CO")
            {
                //string getInv = bll.vLookUp("SELECT inv_no from tmst_dosales where so_cd='" + x + "'");
                arr.Clear();
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@inv_no", getInv));
                //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue.ToString()));
                bll.vInsertRptDosalesInvoice(arr);
                //arr.Clear();
                //arr.Add(new cArrayList("@inv_no", getInv));
                //arr.Add(new cArrayList("@manual_no", txmanualno.Text));
                //bll.vUpdateReprintDocByManualNo(arr);
                arr.Clear();
                arr.Add(new cArrayList("@print_cd", "CANVINV")); //Canvas INvoice  
                arr.Add(new cArrayList("@doc_no", getInv));
                //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue.ToString()));
                bll.vInsertPrintControl(arr);
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=inv&no=" + getInv + "&amt=" + sDiscAmt + "');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=inv&no=" + getInv + "&amt=" + sDiscAmt + "');", true);
            }
            else if (cbtype.SelectedValue.ToString() == "IB")
            {
                //string getInv = bll.vLookUp("SELECT inv_no from tmst_dosales where so_cd='" + x + "'");
                arr.Clear();
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@inv_no", lblsocd.Text));
                //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue.ToString()));
                bll.vInsertRptDosalesInvoice(arr);
                arr.Clear();
                arr.Add(new cArrayList("@print_cd", "CONTRACTINV")); //BONUS / FREE INvoice  
                arr.Add(new cArrayList("@doc_no", lblsocd.Text));
                //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue.ToString()));
                bll.vInsertPrintControl(arr);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport1('fm_report2.aspx?src=invto_uom&no=" + lblsocd.Text + "&amt=0');", true);
            }


        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_reprintDocument");
            Response.Redirect("fm_ErrorPage.aspx");
        }

    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        try
        {

            GridViewRow grd = ((LinkButton)sender).NamingContainer as GridViewRow;
            Label lblCHO = (Label)grd.FindControl("lbCHO");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=croreq&no=" + lblCHO.Text + "');", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_reprintDocument");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void LinkButton5_Click(object sender, EventArgs e)
    {
        try
        {

            GridViewRow grdint = ((LinkButton)sender).NamingContainer as GridViewRow;
            Label lblINT = (Label)grdint.FindControl("lbINT");
            List<cArrayList> arr = new List<cArrayList>();
            //arr.Add(new cArrayList("@SalesPointCD", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue.ToString()));
            arr.Add(new cArrayList("@trf_no", lblINT.Text));
            arr.Add(new cArrayList("@printby", Request.Cookies["usr_id"].Value.ToString()));
            Session["lParamit1"] = arr;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=it1');", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_reprintDocument");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        try
        {

            if (Convert.ToInt16(bll.vLookUp("select dbo.fn_checkaccess('reprintfreeto','" + Request.Cookies["usr_id"].Value.ToString() + "')")) == 0)
            {

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "sweetAlert('You have no access to print free inv','Please request to Admin','warning');", true);
                return;
            }

            List<cArrayList> arr = new List<cArrayList>();
            arr.Clear();
            GridViewRow grd = ((LinkButton)sender).NamingContainer as GridViewRow;
            Label lblsocd = (Label)grd.FindControl("so_cd");
            x = lblsocd.Text;
            string getInv = bll.vLookUp("SELECT inv_no from tmst_dosales where so_cd='" + x + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            if (bll.vLookUp("select inv_no from tdosalesinvoice_free where inv_no='" + getInv + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") == "" || bll.vLookUp("select inv_no from tdosalesinvoice_free where inv_no='" + getInv + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") == null)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "sweetAlert('This invoice Doesnot has free','Invoice No." + getInv + "','warning');", true);
                return;
            }
            string manual_no = bll.vLookUp("select manual_no from tdosales_invoice where inv_no='" + getInv + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");

            if (manual_no == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "sweetAlert('Manual Invoice Free not avaiable','Invoice No." + getInv + "','warning');", true);
                return;
            }

            if (cbtype.SelectedValue.ToString() == "TO")
            {

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport1('fm_report2.aspx?src=invf&noi=" + getInv + "','fm_report2.aspx?src=invf2&noi=" + getInv + "');", true);
            }
            else if (cbtype.SelectedValue.ToString() == "CO")
            {
                string invFreeNo = bll.vLookUp("select manual_no from tdosalesinvoice_free where inv_no='" + getInv + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                if (invFreeNo == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "sweetAlert('Manual Invoice Free not avaiable','Invoice No." + getInv + "','warning');", true);
                    return;
                }
                arr.Clear();

                //arr.Add(new cArrayList("@inv_no", getInv));
                //arr.Add(new cArrayList("@manual_no", manual_no));
                //bll.vUpdateDoSalesInvoiceFree(arr);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport1('fm_report2.aspx?src=invf&noi=" + getInv + "','fm_report2.aspx?src=invf2&noi=" + getInv + "');", true);
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_reprintDocument");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void LinkButton6_Click(object sender, EventArgs e)
    {
        try
        {

            //if (Convert.ToInt16(bll.vLookUp("select dbo.fn_checkaccess('receipt','" + Request.Cookies["usr_id"].Value.ToString() + "')")) == 0)
            //{

            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "sweetAlert('You have no access to print free inv','Please request to Admin','warning');", true);
            //    return;
            //}

            List<cArrayList> arr = new List<cArrayList>();
            arr.Clear();
            GridViewRow grd = ((LinkButton)sender).NamingContainer as GridViewRow;
            Label lblsocd = (Label)grd.FindControl("lbinv_no");
            x = lblsocd.Text;
            //string getInv = bll.vLookUp("SELECT inv_no from tmst_dosales where so_cd='" + x + "'");        

            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport1('fm_report2.aspx?src=invoiceTaxeng&iv=" + txinvoiceno.Text + "','fm_report2.aspx?src=invoiceTaxara&iv=" + txinvoiceno.Text + "');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport1('fm_report2.aspx?src=invoiceTaxara&iv=" + lblsocd.Text + "');", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_reprintDocument");
            Response.Redirect("fm_ErrorPage.aspx");
        }

    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        try
        {

            string invNo = txt_txNo.Text;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@number", invNo));
            //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue.ToString()));
            if (cbtype.SelectedValue.ToString() == "TO")
            {

                bll.vBindingGridToSp(ref grd_reprintDoc, "sp_tmst_reprintDocSO", arr);
            }
            else if (cbtype.SelectedValue.ToString() == "CO")
            {
                bll.vBindingGridToSp(ref grd_reprintDoc, "sp_tmst_reprintDocCO", arr);
            }
            else if (cbtype.SelectedValue.ToString() == "2")
            {
                bll.vBindingGridToSp(ref grdCashout, "sp_tmst_reprintDocCHO", arr);
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_reprintDocument");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void grd_reprintDoc_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grd_reprintDoc.Rows[rowIndex];
            string invoiceNo = row.Cells[0].Text;

            //string invoiceNo = row.Cells[0].Text;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Clear();
            string getInv = bll.vLookUp("SELECT inv_no from tmst_dosales where so_cd='" + invoiceNo + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            if (e.CommandName == "printLoad")
            {

                if (cbtype.SelectedValue.ToString() == "TO")
                {
                    arr.Add(new cArrayList("@print_cd", "TOINV")); //Take Order INvoice
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "reprintDoc", "openreport1('fm_report2.aspx?src=so_uom&so=" + invoiceNo + "','fm_report2.aspx?src=so1_uom&so=" + invoiceNo + "');", true);
                }

                else if (cbtype.SelectedValue.ToString() == "CO")
                {
                    //string getInv = bll.vLookUp("SELECT inv_no from tmst_dosales where so_cd='" + invoiceNo + "'");
                    arr.Clear();
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue.ToString()));
                    arr.Add(new cArrayList("@inv_no", getInv));
                    bll.vInsertRptDosalesInvoice(arr);
                    arr.Clear();
                    arr.Add(new cArrayList("@print_cd", "CANVINV")); //Canvas INvoice            
                    string sDiscAmt = bll.vLookUp("select sum(amt) from tcanvasorder_disccash where so_cd='" + invoiceNo + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "reprintDoc", "openreport('fm_report2.aspx?src=invto_uom&no=" + getInv + "&amt=" + sDiscAmt + "');", true);
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "reprintDoc", "openreport('fm_report2.aspx?src=inv&no=" + getInv + "&amt=" + sDiscAmt + "');", true);
                }
            }
            else if (e.CommandName == "printInv")
            {
                if (cbtype.SelectedValue.ToString() == "TO")
                {
                    arr.Add(new cArrayList("@print_cd", "TOINV")); //Take Order INvoice
                }
                else if (cbtype.SelectedValue.ToString() == "CO")
                {
                    arr.Add(new cArrayList("@print_cd", "CANVINV")); //Canvas INvoice  
                }
                else if (cbtype.SelectedValue.ToString() == "IB")
                {
                    arr.Add(new cArrayList("@print_cd", "CONTRACTINV")); //Contract / free / bonus INvoice  
                }

                string sDiscAmt = bll.vLookUp("select isnull(sum(amt),0) from tsalesorder_disccash where so_cd='" + invoiceNo + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "reprintDoc", "openreport1('fm_report2.aspx?src=invto_uom&no=" + getInv + "&amt=" + sDiscAmt + "');", true);
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "reprintDoc", "openreport1('fm_report2.aspx?src=invto_uom&no=" + getInv + "&amt=" + sDiscAmt + "','fm_report2.aspx?src=invto1_uom&no=" + getInv + "&amt=" + sDiscAmt + "');", true);

            }

            else if (e.CommandName == "printInvFree")
            {
                if (cbtype.SelectedValue.ToString() == "TO")
                {
                    arr.Add(new cArrayList("@print_cd", "TOINV")); //Take Order INvoice
                }
                else if (cbtype.SelectedValue.ToString() == "CO")
                {
                    arr.Add(new cArrayList("@print_cd", "CANVINV")); //Canvas INvoice  
                }
                //string getInv = bll.vLookUp("SELECT inv_no from tmst_dosales where so_cd='" + invoiceNo + "'");
                string invoiceFreeNo = bll.vLookUp("select manual_no from tdosales_invoice where inv_no='" + getInv + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                arr.Add(new cArrayList("@inv_no", invoiceNo));
                arr.Add(new cArrayList("@manual_no", invoiceFreeNo));
                //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue.ToString()));
                bll.vUpdateDoSalesInvoiceFree(arr);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "reprintDoc", "openreport1('fm_report2.aspx?src=invf&noi=" + invoiceNo + "','fm_report2.aspx?src=invf2&noi=" + invoiceNo + "');", true);

            }

            arr.Add(new cArrayList("@doc_no", invoiceNo));
            //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue.ToString()));
            bll.vInsertPrintControl(arr);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_reprintDocument");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void grdCashout_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {

            grdCashout.PageIndex = e.NewPageIndex;
            string invNo = txt_txNo.Text;

            List<cArrayList> arr = new List<cArrayList>();
            //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue.ToString()));
            if (invNo != "")
            {
                arr.Add(new cArrayList("@number", invNo.Trim()));
            }
            if (cbtype.SelectedValue.ToString() == "2")
            {
                bll.vBindingGridToSp(ref grdCashout, "sp_tmst_reprintDocCHO", arr);
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_reprintDocument");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void grdint_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {

            grdint.PageIndex = e.NewPageIndex;
            string invNo = txt_txNo.Text;

            List<cArrayList> arr = new List<cArrayList>();
            //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue.ToString()));
            if (cbtype.SelectedValue.ToString() == "3")
            {
                bll.vBindingGridToSp(ref grdint, "sp_tmst_reprintDocint", arr);
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_reprintDocument");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grdint_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            e.Row.Attributes.Add("style", "cursor:pointer;");
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowState == DataControlRowState.Alternate)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='orange'");
                    //e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#47A3DA'");
                    //e.Row.BackColor = Color.FromName("#47A3DA");
                }
            }
            else
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='orange'");
                    //e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#FFF'");
                    //e.Row.BackColor = Color.FromName("#FFF");
                }
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_reprintDocument");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grdCashout_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            e.Row.Attributes.Add("style", "cursor:pointer;");
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowState == DataControlRowState.Alternate)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='orange'");
                    //e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#47A3DA'");
                    //e.Row.BackColor = Color.FromName("#47A3DA");
                }
            }
            else
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='orange'");
                    //e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#FFF'");
                    //e.Row.BackColor = Color.FromName("#FFF");
                }
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_reprintDocument");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void cbSalesPointCD_SelectedIndexChanged(object sender, EventArgs e)
    {
        dtbranch.Text = bll.sGetControlParameterSalespoint("wazaran_dt", cbSalesPointCD.SelectedValue.ToString());
        cbtype_SelectedIndexChanged(sender, e);
    }

    protected void btreprintbl_Click(object sender, EventArgs e)
    {
        GridViewRow _row = ((LinkButton)sender).NamingContainer as GridViewRow;
        Label lblsocd = (Label)_row.FindControl("so_cd");
        string _do_no = bll.vLookUp("select do_no from tmst_dosales where so_cd='" + lblsocd.Text + "'");
        string _bl_no = bll.vLookUp("select top 1 bl_no from tdosales_bl where do_no='" + _do_no + "'");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                "openreport('fm_report2.aspx?src=bl&no=" + _bl_no + "');", true);
    }

    protected void cbsalesman_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salesman_cd", cbsalesman.SelectedValue));
        //cbtype.SelectedValue = string.Empty;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
    }

    protected void btlookup_Click(object sender, EventArgs e)
    {
        cbtype.SelectedValue = "0";
    }
}