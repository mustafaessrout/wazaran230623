using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_tabsalesreturn : System.Web.UI.Page
{
    cbll bll = new cbll();
    cbll2 bll2 = new cbll2();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //try
            //{
                bll.vBindingFieldValueToComboWithChoosen(ref cbstatus, "tab_sta_id");
                //cbstatus_SelectedIndexChanged(sender, e);

            //}
            //catch (Exception ex)
            //{
            //    Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            //    bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_tabsalesreturn");
            //    Response.Redirect("fm_ErrorPage.aspx");
            //}
        }
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "tablePageCopy", "tablePageCopy()", true);
    }
    protected void grdtab_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            Label lbtabretur_no = (Label)grdtab.Rows[e.NewSelectedIndex].FindControl("lbtabretur_no");
            Label lbretur_typ = (Label)grdtab.Rows[e.NewSelectedIndex].FindControl("lbretur_typ");
            hdreturno.Value = lbtabretur_no.Text;
            hdretur_typ.Value = lbretur_typ.Text;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@tabretur_no", hdreturno.Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grdtabdtl, "sp_ttab_tsalesreturn_dtl_get", arr);
            //  hdreturno.Value = lbtabretur_no.Text;

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_tabsalesreturn");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btapply_Click(object sender, EventArgs e)
    {

        try
        {
            foreach (GridViewRow row in grdtab.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chk");
                if ((row.RowType == DataControlRowType.DataRow) && chk.Checked)
                {
                    Label lbtabretur_no = (Label)row.FindControl("lbtabretur_no");
                    string sMsgdupretur = bll.vLookUp("select dbo.fn_checkdupreturtab('" + lbtabretur_no.Text + "','" + Request.Cookies["sp"].Value.ToString() + "')");
                    if (sMsgdupretur != "ok")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Return number ','" + sMsgdupretur + " already transfer to backoffice','warning');", true);
                        return;
                    }
                    int dCount = Convert.ToInt32(bll.vLookUp("select count(1) from ttab_salesreturn_dtl where tabretur_no='" + lbtabretur_no.Text + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'"));
                    if (dCount == 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('One of More return details not yet transferred!','Please contact Prod Support Wazaran','warning');", true);
                        return;
                    }
                    int dCountpay = Convert.ToInt32(bll.vLookUp("select count(1) from ttab_paymentreceipt where tab_no=(select payment_no from ttab_salesreturn where  tabretur_no='" + lbtabretur_no.Text + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "')"));
                    if (dCountpay == 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('One of More payment return not yet transferred!','Please contact Prod Support Wazaran','warning');", true);
                        return;
                    }
                }
            }
            //string sReturNo = "";
            string _returno = string.Empty;
            foreach (GridViewRow row in grdtab.Rows)
            {
                List<cArrayList> arr = new List<cArrayList>();
                CheckBox chk = (CheckBox)row.FindControl("chk");
                Label lbsalespointcd = (Label)row.FindControl("lbsalespointcd");
                Label lbretur_typ = (Label)row.FindControl("lbretur_typ");
                Label lbsalesman_cd = (Label)row.FindControl("lbsalesman_cd");
                Label lbcust_cd = (Label)row.FindControl("lbcust_cd");
                Label lbretur_dt = (Label)row.FindControl("lbretur_dt");
                Label lbmanual_no = (Label)row.FindControl("lbmanual_no");
                Label lbcustmanual_no = (Label)row.FindControl("lbcustmanual_no");
                Label lbremark = (Label)row.FindControl("lbremark");
                Label lbtabretur_no = (Label)row.FindControl("lbtabretur_no");

                if (chk.Checked == true)
                {
                    arr.Clear();
                    arr.Add(new cArrayList("@tabretur_no", lbtabretur_no.Text));
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
                    bll2.vBatchSalesreturnTab(arr, ref _returno);

                    //try
                    //{
                    //int dCount = Convert.ToInt32( bll.vLookUp("select count(1) from ttab_salesreturn_dtl where tabretur_no='" + lbtabretur_no.Text + "'"));
                    //if (dCount == 0) 
                    //{
                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "One of return has ", true);
                    //    return;
                    //}
                    //List<cArrayList> arr = new List<cArrayList>();
                    //arr.Add(new cArrayList("@retur_typ", lbretur_typ.Text));
                    //arr.Add(new cArrayList("@salesman_cd", lbsalesman_cd.Text));
                    //arr.Add(new cArrayList("@cust_cd", lbcust_cd.Text));
                    //arr.Add(new cArrayList("@retur_dt", DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                    //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    //arr.Add(new cArrayList("@manual_no", lbmanual_no.Text));
                    //arr.Add(new cArrayList("@custmanual_no", lbcustmanual_no.Text));
                    //arr.Add(new cArrayList("@remark", lbremark.Text));
                    //arr.Add(new cArrayList("@tabretur_no", lbtabretur_no.Text));
                    //arr.Add(new cArrayList("@returnDriver_cd", lbsalesman_cd.Text));
                    ////bll.vInsertSalesReturn(arr, ref sReturNo);

                    //arr.Clear();
                    //arr.Add(new cArrayList("@tabretur_no", lbtabretur_no.Text));
                    //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    //bll.vInsertttab_salesreturn_dtl2(arr);
                    //sReturNo = bll.vLookUp("select retur_no from tsalesreturn where tab_no='" + lbtabretur_no.Text + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                    //nCount++;
                    //}
                    //catch (Exception ex)
                    //{
                    //    var st = new System.Diagnostics.StackTrace(ex, true);
                    //    // Get the top stack frame
                    //    var frame = st.GetFrame(0);
                    //    // Get the line number from the stack frame
                    //    var line = frame.GetFileLineNumber();
                    //    List<cArrayList> arr = new List<cArrayList>();
                    //    arr.Add(new cArrayList("@err_source", "save tablet SalesReturn"));
                    //    arr.Add(new cArrayList("@err_description", "Line [" + line.ToString() + "]" + ex.Message.ToString()));
                    //    bll.vInsertErrorLog(arr);
                    //}
                }

            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "window.opener.SelectReturn('" + _returno + "');window.close();", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_tabsalesreturn");
            Response.Redirect("fm_ErrorPage.aspx");
        }

    }

    private void bindinggrdtab()
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@tab_sta_id", cbstatus.SelectedValue));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grdtab, "sp_ttab_tsalesreturn_get", arr);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tablePageCopy", "tablePageCopy()", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_tabsalesreturn");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void cbstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            bindinggrdtab();
            if (cbstatus.SelectedValue == "TRF" || cbstatus.SelectedValue == "DEL")
            {
                btapply.Visible = false;
                btcancel.Visible = false;
                btpostpone.Visible = false;
            }
            else
            {
                btapply.Visible = true;
                btcancel.Visible = true;
                btpostpone.Visible = true;
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tablePageCopy", "tablePageCopy()", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_tabsalesreturn");
            Response.Redirect("fm_ErrorPage.aspx");
        }

    }


    protected void btcancel_Click(object sender, EventArgs e)
    {
        try
        {

            List<cArrayList> arr = new List<cArrayList>();
            foreach (GridViewRow row in grdtab.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chk = (CheckBox)row.FindControl("chk");
                    if (chk.Checked)
                    {
                        Label lbtabretur_no = (Label)row.FindControl("lbtabretur_no");
                        arr.Clear();
                        arr.Add(new cArrayList("@tabretur_no", lbtabretur_no.Text));
                        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                        bll.vDeleteTTabSalesreturn(arr);
                    }
                }
            }
            bindinggrdtab();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alr", "sweetAlert('Tablet salesreturn has been deleted','Delete tablet salesretur','success');", true);
            //arr.Add(new cArrayList("@tabretur_no", ))
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tablePageCopy", "tablePageCopy()", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_tabsalesreturn");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btpostpone_Click(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            foreach (GridViewRow row in grdtab.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chk = (CheckBox)row.FindControl("chk");
                    if (chk.Checked)
                    {
                        Label lbtabretur_no = (Label)row.FindControl("lbtabretur_no");
                        arr.Clear();
                        arr.Add(new cArrayList("@tabretur_no", lbtabretur_no.Text));
                        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                        bll.vUpdateTTabSalesReturn(arr);
                    }
                }
            }
            bindinggrdtab();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alr", "sweetAlert('Tablet salesreturn has been postpone','Postpone to next day succeeded','success');", true);
            //arr.Add(new cArrayList("@tabretur_no", ))
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tablePageCopy", "tablePageCopy()", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_tabsalesreturn");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grdtabdtl_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {

            grdtabdtl.EditIndex = e.NewEditIndex;
            // bindinggrdtab();
            // Label lbtabretur_no = (Label)grdtab.Rows[e.NewEditIndex].FindControl("lbtabretur_no");


            Label lbwhs_cd = (Label)grdtabdtl.Rows[e.NewEditIndex].FindControl("lbwhs_cd");
            Label lbbin_cd = (Label)grdtabdtl.Rows[e.NewEditIndex].FindControl("lbbin_cd");

            List<cArrayList> arr = new List<cArrayList>();
            arr.Clear();
            arr.Add(new cArrayList("@tabretur_no", hdreturno.Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grdtabdtl, "sp_ttab_tsalesreturn_dtl_get", arr);

            DropDownList cbbin_cd = (DropDownList)grdtabdtl.Rows[e.NewEditIndex].FindControl("cbbin_cd");
            arr.Clear();
            if (hdretur_typ.Value == "I")
            {
                arr.Add(new cArrayList("@whs_cd", lbwhs_cd.Text));
                bll.vBindingComboToSp(ref cbbin_cd, "sp_twarehouse_bin_get", "bin_cd", "bin_nm", arr);
            }
            else
            {
                arr.Add(new cArrayList("@vhc_cd", lbwhs_cd.Text));
                bll.vBindingComboToSp(ref cbbin_cd, "sp_van_bin_get", "bin_cd", "bin_nm", arr);
            }
            cbbin_cd.SelectedValue = lbbin_cd.Text;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tablePageCopy", "tablePageCopy()", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_tabsalesreturn");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grdtabdtl_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {

            grdtabdtl.EditIndex = -1;
            Label lbtabretur_no = (Label)grdtab.Rows[e.RowIndex].FindControl("lbtabretur_no");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@tabretur_no", hdreturno.Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grdtabdtl, "sp_ttab_tsalesreturn_dtl_get", arr);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tablePageCopy", "tablePageCopy()", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_tabsalesreturn");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grdtabdtl_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {

            Label lbitemcode = (Label)grdtabdtl.Rows[e.RowIndex].FindControl("lbitemcode");
            TextBox dtexp = (TextBox)grdtabdtl.Rows[e.RowIndex].FindControl("dtexp");
            TextBox txqty = (TextBox)grdtabdtl.Rows[e.RowIndex].FindControl("txqty");
            DropDownList cbbin_cd = (DropDownList)grdtabdtl.Rows[e.RowIndex].FindControl("cbbin_cd");

            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@tabretur_no", hdreturno.Value.ToString()));
            arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
            arr.Add(new cArrayList("@exp_dt", dtexp.Text));
            arr.Add(new cArrayList("@qty", txqty.Text));
            arr.Add(new cArrayList("@bin_cd", cbbin_cd.SelectedValue));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vUpdateTTabSalesReturnDtl(arr);
            grdtabdtl.EditIndex = -1;

            arr.Clear();
            arr.Add(new cArrayList("@tabretur_no", hdreturno.Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grdtabdtl, "sp_ttab_tsalesreturn_dtl_get", arr);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tablePageCopy", "tablePageCopy()", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_tabsalesreturn");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void grdtab_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {

            grdtab.PageIndex = e.NewPageIndex;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@tab_sta_id", cbstatus.SelectedValue));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grdtab, "sp_ttab_tsalesreturn_get", arr);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_tabsalesreturn");
            Response.Redirect("fm_ErrorPage.aspx");
        }

    }
    protected void grdtabdtl_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {

            grdtabdtl.PageIndex = e.NewPageIndex;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Clear();
            arr.Add(new cArrayList("@tabretur_no", hdreturno.Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grdtabdtl, "sp_ttab_tsalesreturn_dtl_get", arr);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_tabsalesreturn");
            Response.Redirect("fm_ErrorPage.aspx");
        }

    }
}