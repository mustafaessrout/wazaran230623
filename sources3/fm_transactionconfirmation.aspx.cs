using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_transactionconfirmation : System.Web.UI.Page
{
    cbll bll = new cbll();
    decimal totalQty = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                List<cArrayList> arr = new List<cArrayList>();
                string waz_dt;
                string sho;

                waz_dt = Request.Cookies["waz_dt"].Value.ToString();
                DateTime dtwaz_dt = DateTime.ParseExact(waz_dt, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                arr.Clear();
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                bll.vBindingComboToSp(ref cbSalesPointCD, "sp_tmst_salespoint_getuser", "salespointcd", "salespoint_desc", arr);
                sho = Request.Cookies["sp"].Value.ToString();
                if (sho == "0")
                {
                    cbSalesPointCD.Enabled = true;
                    //cbSalesPointCD.CssClass = "";
                }
                else
                {
                    cbSalesPointCD.SelectedValue = sho;
                    cbSalesPointCD.Enabled = false;
                    cbSalesPointCD.CssClass = "makeitreadonly ro form-control";
                }
                bindinggrd();
            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_transactionconfirmation");
                Response.Redirect("fm_ErrorPage.aspx");
            }
        }
    }

    private void bindinggrd()
    {
        // Transaction Type 
        // t1 -> TO with Payment
        // t2 -> CN with Payment 
        // t3 -> Payment Receipt 
        // t4 -> Cashout Request
        // t5 -> Stock Opname 
        // t6 -> Internal Transfer

        dtbranch.Text = bll.sGetControlParameterSalespoint("wazaran_dt",cbSalesPointCD.SelectedValue.ToString());
        string ttype = cbtypetrans.SelectedValue.ToString();
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue.ToString()));
        arr.Add(new cArrayList("@number", txtrans_no.Text.ToString()));

        switch (ttype)
        {
            case "t1":
                grdto.Visible = true;
                grdtodtl.Visible = false;
                grdit.Visible = false;
                grditdtl.Visible = false;
                bll.vBindingGridToSp(ref grdto, "sp_tmst_reviewSO", arr);
                arr.Clear();
                break;
            case "t2":
                //grdto.Visible = true;
                //grdtodtl.Visible = false;
                //grdit.Visible = false;
                //grditdtl.Visible = false;
                bll.vBindingGridToSp(ref grdto, "sp_tmst_reviewCN", arr);
                arr.Clear();
                break;
            case "t3":
                //grdto.Visible = false;
                //grdtodtl.Visible = false;
                //grdit.Visible = false;
                //grditdtl.Visible = false;
                //bll.vBindingGridToSp(ref grdto, "sp_tmst_reviewCN", arr);
                arr.Clear();
                break;
            case "t6":
                grdto.Visible = false;
                grdtodtl.Visible = false;
                grdit.Visible = true;
                grditdtl.Visible = true;
                bll.vBindingGridToSp(ref grdit, "sp_tmst_reviewIT", arr);
                arr.Clear();
                break;
            default:
                break;

        }

        //arr.Add(new cArrayList("@sta_id", "W"));
        //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        //bll.vBindingGridToSp(ref grdtrf, "sp_app_internaltransfer_get", arr);
    }

    protected void btapprove_Click(object sender, EventArgs e)
    {
        //List<cArrayList> arr = new List<cArrayList>();
        //foreach (GridViewRow row in grdtrf.Rows)
        //{
        //    if (row.RowType == DataControlRowType.DataRow)
        //    {
        //        CheckBox chk = (CheckBox)row.FindControl("chk");
        //        if (chk.Checked)
        //        {
        //            Label lb_trf_no = (Label)row.FindControl("lb_trf_no");
        //            HiddenField hdsalespointcd = (HiddenField)row.FindControl("hdsalespointcd");
        //            arr.Clear();
        //            arr.Add(new cArrayList("@trf_no", lb_trf_no.Text));
        //            //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        //            arr.Add(new cArrayList("@salespointcd", hdsalespointcd.Value.ToString()));
        //            arr.Add(new cArrayList("@sta_id", "A"));
        //            bll.vUpdateAppInternalTransfer(arr);
        //        }
        //    }
        //}
        //bindinggrd();
        //grdtrfdtl.DataSource = null;
        //grdtrfdtl.DataBind();
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alr", "sweetAlert('Internal Transfer has been approved','Internal Transfer Request','success');", true);
    }

    protected void btcancel_Click(object sender, EventArgs e)
    {
        //List<cArrayList> arr = new List<cArrayList>();
        //foreach (GridViewRow row in grdtrf.Rows)
        //{
        //    if (row.RowType == DataControlRowType.DataRow)
        //    {
        //        CheckBox chk = (CheckBox)row.FindControl("chk");
        //        if (chk.Checked)
        //        {
        //            Label lb_trf_no = (Label)row.FindControl("lb_trf_no");
        //            HiddenField hdsalespointcd = (HiddenField)row.FindControl("hdsalespointcd");
        //            arr.Clear();
        //            arr.Add(new cArrayList("@trf_no", lb_trf_no.Text));
        //            //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        //            arr.Add(new cArrayList("@salespointcd", hdsalespointcd.Value.ToString()));
        //            arr.Add(new cArrayList("@sta_id", "L"));
        //            bll.vUpdateAppInternalTransfer(arr);
        //        }
        //    }
        //}
        //bindinggrd();
        //grdtrfdtl.DataSource = null;
        //grdtrfdtl.DataBind();
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alr", "sweetAlert('Internal Transfer has been rejected','Internal Transfer Request','success');", true);
    }

    


    protected void grdto_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdto.PageIndex = e.NewPageIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue.ToString()));
        arr.Add(new cArrayList("@number", txtrans_no.Text.ToString()));
        if (cbtypetrans.SelectedValue.ToString() == "t1")
        {
            bll.vBindingGridToSp(ref grdto, "sp_tmst_reviewSO", arr);
        }
        else if(cbtypetrans.SelectedValue.ToString() == "t2")
        {
            bll.vBindingGridToSp(ref grdto, "sp_tmst_reviewCN", arr);
        }
        
    }

    protected void grdto_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        grdtodtl.Visible = true;
        Label lbso_cd = (Label)grdto.Rows[e.NewSelectedIndex].FindControl("lbso_cd");
        Label lbinv_no = (Label)grdto.Rows[e.NewSelectedIndex].FindControl("lbinv_no");
        Label lbsalespointcd = (Label)grdto.Rows[e.NewSelectedIndex].FindControl("lbsalespointcd");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@inv_no", lbinv_no.Text));
        arr.Add(new cArrayList("@salespointcd", lbsalespointcd.Text));
        bll.vBindingGridToSp(ref grdtodtl, "sp_tmst_reviewPaymentDtl", arr);
    }

    protected void cbtypetrans_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindinggrd();
    }

    protected void cbSalesPointCD_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindinggrd();
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        bindinggrd();
    }


    protected void grdto_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            List<cArrayList> arr = new List<cArrayList>();
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdto.Rows[index];
            Label lbso_cd = (Label)grdto.Rows[index].FindControl("lbso_cd");
            Label lbinv_no = (Label)grdto.Rows[index].FindControl("lbinv_no");
            Label lbsalespointcd = (Label)grdto.Rows[index].FindControl("lbsalespointcd");

            
            if (e.CommandName == "reject")
            {
                if (bll.nCheckAccess("review_to", Request.Cookies["usr_id"].Value.ToString()) == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To Review / Cancel this Transaction','warning');", true);
                    return;
                }

                if (lbso_cd.Text.ToString() == "" || lbinv_no.Text.ToString() == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Select invoice !!!','warning');", true);
                    return;
                }

                if (grdtodtl.Rows.Count == 0)
                {
                    arr.Clear();
                    arr.Add(new cArrayList("@inv_no", lbinv_no.Text));
                    arr.Add(new cArrayList("@salespointcd", lbsalespointcd.Text));
                    bll.vBindingGridToSp(ref grdtodtl, "sp_tmst_reviewPaymentDtl", arr);
                }

                if (grdtodtl.Rows.Count > 1)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Can not Review / Cancel This TO','Payment Receipt more than 1 (one).','warning');", true);
                    return;
                }

                arr.Clear();
                arr.Add(new cArrayList("@so_cd", lbso_cd.Text));
                arr.Add(new cArrayList("@inv_no", lbinv_no.Text));
                arr.Add(new cArrayList("@salespointcd", lbsalespointcd.Text));
                bll.vReviewTmstSO(arr);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alr", "sweetAlert('TO with Payment has been reviewed','"+lbinv_no.Text.ToString()+"','success');", true);
            }
            arr.Clear();
            bindinggrd();

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_transactionconfirmation");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void grdtodtl_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void grdit_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        grditdtl.Visible = true;
        Label lb_trf_no = (Label)grdit.Rows[e.NewSelectedIndex].FindControl("lb_trf_no");
        Label lbsalespointcd = (Label)grdit.Rows[e.NewSelectedIndex].FindControl("lbsalespointcd");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@trf_no", lb_trf_no.Text));
        arr.Add(new cArrayList("@salespointcd", lbsalespointcd.Text));
        bll.vBindingGridToSp(ref grditdtl, "sp_tmst_reviewITDtl", arr);
    }

    protected void grdit_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdit.PageIndex = e.NewPageIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue.ToString()));
        arr.Add(new cArrayList("@number", txtrans_no.Text.ToString()));
        //if (cbtypetrans.SelectedValue.ToString() == "t6")
        //{
            bll.vBindingGridToSp(ref grdit, "sp_tmst_reviewIT", arr);
        //}
    }

    protected void grdit_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            List<cArrayList> arr = new List<cArrayList>();
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdit.Rows[index];
            Label lb_trf_no = (Label)grdit.Rows[index].FindControl("lb_trf_no");
            Label lbsalespointcd = (Label)grdit.Rows[index].FindControl("lbsalespointcd");


            if (e.CommandName == "reject")
            {
                if (bll.nCheckAccess("review_it", Request.Cookies["usr_id"].Value.ToString()) == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To Review / Cancel this Transaction','warning');", true);
                    return;
                }

                arr.Clear();
                arr.Add(new cArrayList("@trf_no", lb_trf_no.Text));
                arr.Add(new cArrayList("@salespointcd", lbsalespointcd.Text));
                bll.vReviewTmstIT(arr);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alr", "sweetAlert('Internal Transfer has been reviewed','" + lb_trf_no.Text.ToString() + "','success');", true);
            }
            arr.Clear();
            bindinggrd();

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_transactionconfirmation");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void grditdtl_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}