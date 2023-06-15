using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_soa : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            try
            {
                List<cArrayList> arr = new List<cArrayList>();
                cbrepby_SelectedIndexChanged(sender, e);
                bll.vBindingFieldValueToCombo(ref cbcusgr, "CusGrCD");
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
                bll.vBatchBfrClosingday(arr);
                decimal x = Convert.ToDecimal(bll.vLookUp("select sum(balance) from tsoa_dtl where salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'"));
                decimal y = Convert.ToDecimal(bll.vLookUp("select dbo.fn_gettotbalance('"+ Request.Cookies["sp"].Value.ToString() + "')"));
                lbl.Text = (x - y).ToString();
                arr.Clear();
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value));
                bll.vBindingComboToSp(ref cbtypofrep, "sp_soagetreporttype", "fld_valu", "fld_desc", arr);
                arr.Clear();
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@report", "SOA"));
                bll.vDelsoasalesman1(arr);
                bll.vDelsoacustomer1(arr);
                arr.Clear();
                dtstart.Text = Request.Cookies["waz_dt"].Value.ToString();
                dtend.Text = Request.Cookies["waz_dt"].Value.ToString();
                bll.vBindingComboToSp(ref cbbranch, "sp_tmst_salespoint_get", "salespointcd", "salespoint_desc");
                cbbranch.SelectedValue = Request.Cookies["sp"].Value.ToString();
                if (Request.Cookies["sp"].Value.ToString() == "0")
                {
                    cbbranch.Enabled = true;
                }
                else
                {
                    cbbranch.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_soa");
                Response.Redirect("fm_ErrorPage.aspx");
            }
        }
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        if (bll.nCheckAccess("soa", Request.Cookies["usr_id"].Value.ToString()) == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','Print Statment Of Account !!','warning');", true);
            return;
        }
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            //cmpVal1.Validate();
            arr.Add(new cArrayList("@salespointcd", cbbranch.SelectedValue.ToString()));
            bll.vBatchBfrClosingday(arr);
            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", cbbranch.SelectedValue.ToString()));
            //arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
            arr.Add(new cArrayList("@startdate", dtstart.Text));
            arr.Add(new cArrayList("@enddate", dtend.Text));


            if (cbtypofrep.SelectedValue.ToString() == "SOA1")
            {
                //arr.Add(new cArrayList("@type", "Detail"));
                if (cbrepby.SelectedValue.ToString() == "0")
                {
                    Session["lParamsoabr"] = arr;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=soa1bybranch');", true);
                }
                else if (cbrepby.SelectedValue.ToString() == "1")
                {
                    if (grdsl.Rows.Count.ToString() == "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Salesman Error','Please insert at least one salesman !!','warning');", true);
                        return;
                    }
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    Session["lParamsoabr"] = arr;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=soa1byslsman&p=1');", true);
                }
                else if (cbrepby.SelectedValue.ToString() == "2")
                {
                    if (grdcust.Rows.Count.ToString() == "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Customer Error','Please insert at least one customer !!','warning');", true);
                        return;
                    }
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    Session["lParamsoabr"] = arr;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=soa1bycust&p=2');", true);
                }
                else if (cbrepby.SelectedValue.ToString() == "3")
                {
                    arr.Add(new cArrayList("@cusgrcd", cbcusgr.SelectedValue.ToString()));
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    Session["lParamsoabr"] = arr;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=soa1bycusgr&p=3');", true);
                }
                else
                {
                    return;
                }
            }
            else
            {
                //arr.Add(new cArrayList("@type", "Summary"));
                if (cbtypofrep.SelectedValue.ToString() == "SOA2")
                {
                    if (cbrepby.SelectedValue.ToString() == "0")
                    {
                        Session["lParamsoabr"] = arr;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=soa2bybranch');", true);
                    }
                    else if (cbrepby.SelectedValue.ToString() == "1")
                    {
                        if (grdsl.Rows.Count.ToString() == "0")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Salesman Error','Please insert at least one salesman !!','warning');", true);
                            return;
                        }
                        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                        Session["lParamsoabr"] = arr;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=soa2byslsman&p=1');", true);
                    }
                    else if (cbrepby.SelectedValue.ToString() == "2")
                    {
                        if (grdcust.Rows.Count.ToString() == "0")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Customer Error','Please insert at least one customer !!','warning');", true);
                            return;
                        }
                        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                        Session["lParamsoabr"] = arr;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=soa2bycust&p=2');", true);
                    }
                    else if (cbrepby.SelectedValue.ToString() == "3")
                    {
                        arr.Add(new cArrayList("@cusgrcd", cbcusgr.SelectedValue.ToString()));
                        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                        Session["lParamsoabr"] = arr;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=soa2bycusgr&p=3');", true);
                    }
                    else
                    {
                        return;
                    }

                }
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_soa");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        List<string> lcust = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string scust = string.Empty;
        System.Data.SqlClient.SqlDataReader rs = null;
        //arr.Add(new cArrayList("@cust_cd", prefixText));
        arr.Add(new cArrayList("@salesman_cd", prefixText));
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        bll.vSearchSalesman(arr, ref rs);
        //bll.vSearchMstCustomer(arr, ref rs);
        while (rs.Read())
        {
            scust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["emp_cd"].ToString() + "-" + rs["emp_nm"].ToString(), rs["emp_cd"].ToString());
            lcust.Add(scust);
        }
        rs.Close();

        return (lcust.ToArray());
    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        try
        {
            txsalesman.Focus();
            txsalesmanPnl.CssClass = "";
            if (txsalesman.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Salesman Error','Please select Salesman','warning');", true);
                txsalesmanPnl.CssClass = "error err-empty";
                return;
            }
            List<cArrayList> arr = new List<cArrayList>();
            string x = bll.vLookUp("select max(seq_no) from trp_bysalesman where report='SOA' and usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='" + cbbranch.SelectedValue.ToString() + "'");
            if (x == "" || x == null)
            {
                x = "0";
            }

            arr.Add(new cArrayList("@salespointcd", cbbranch.SelectedValue.ToString()));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salesman_nm", bll.vLookUp("select emp_nm from tmst_employee where emp_cd='" + hdsalesman_cd.Value + "'")));
            arr.Add(new cArrayList("@salesman_cd", hdsalesman_cd.Value));
            arr.Add(new cArrayList("@report", "SOA"));
            arr.Add(new cArrayList("@seq_no", Convert.ToInt16(x) + 1));
            bll.vInsertsoasalesman(arr);
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@report", "SOA"));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grdsl, "sp_trp_bysalesman_get", arr);
            txsalesman.Text = "";
            hdsalesman_cd.Value = null;
            txsalesman.Focus();
            // bll.vBindingComboToSp(ref cbcity, "sp_tmst_location_get3", "loc_cd", "loc_nm", arr, "ALL");

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_soa");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grdsl_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Label lbsalesman_cd = (Label)grdsl.Rows[e.RowIndex].FindControl("lbsalesman_cd");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", cbbranch.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salesman_cd", lbsalesman_cd.Text));
            arr.Add(new cArrayList("@report", "SOA"));
            bll.vDelsoasalesman(arr); arr.Clear(); arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@report", "SOA"));
            arr.Add(new cArrayList("@salespointcd", cbbranch.SelectedValue.ToString()));
            bll.vBindingGridToSp(ref grdsl, "sp_trp_bysalesman_get", arr);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_soa");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void cbrepby_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbrepby.SelectedValue.ToString() == "0")
        {
            txsalesman.CssClass = "form-control ro";
            txsalesman.Enabled = false;
            grdsl.Visible = false;
            txcust.CssClass = "form-control ro";
            txcust.Enabled = false;
            grdcust.Visible = false;
            btadd.Enabled = false;
            btaddcust.Enabled = false;
            cbcusgr.Visible = false;
            Panel5.Visible = false;
            lbcusgrcd.Visible = false;
        }
        else if (cbrepby.SelectedValue.ToString() == "1")
        {
            txsalesman.CssClass = "form-control ";
            txsalesman.Enabled = true;
            grdsl.Visible = true;
            txcust.CssClass = "form-control ro";
            txcust.Enabled = false;
            grdcust.Visible = false;
            btadd.Enabled = true;
            btaddcust.Enabled = false;
            cbcusgr.Visible = false;
            Panel5.Visible = false;
            lbcusgrcd.Visible = false;
        }
        else if (cbrepby.SelectedValue.ToString() == "2")
        {
            txsalesman.CssClass = "form-control ro";
            txsalesman.Enabled = false;
            grdsl.Visible = false;
            txcust.CssClass = "form-control ";
            txcust.Enabled = true;
            grdcust.Visible = true;
            btadd.Enabled = false;
            btaddcust.Enabled = true;
            cbcusgr.Visible = false;
            Panel5.Visible = false;
            lbcusgrcd.Visible = false;
        }
        else if (cbrepby.SelectedValue.ToString() == "3")
        {
            lbcusgrcd.Visible = true;
            txsalesman.CssClass = "form-control ro";
            txsalesman.Enabled = false;
            grdsl.Visible = false;
            grdcust.Visible = false;
            txcust.CssClass = "form-control ro";
            txcust.Enabled = false;
            btadd.Enabled = false;
            btaddcust.Enabled = false;
            cbcusgr.Visible = true;
            Panel5.Visible = true;
        }
    }
    protected void btaddcust_Click(object sender, EventArgs e)
    {
        try
        {
            txcustPnl.CssClass = "";
            if (txcust.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Customer Error','Please insert Customer','warning');", true);
                txcustPnl.CssClass = "error err-empty";
                return;
            }
            List<cArrayList> arr = new List<cArrayList>();
            string x = bll.vLookUp("select max(seq_no) from trp_bysalesman where report='SOA' and usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='" + cbbranch.SelectedValue.ToString() + "'");
            if (x == "" || x == null)
            {
                x = "0";
            }

            arr.Add(new cArrayList("@salespointcd", cbbranch.SelectedValue.ToString()));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@cust_nm", bll.vLookUp("select cust_nm from tmst_customer where cust_cd='" + hdcust_cd.Value + "'")));
            arr.Add(new cArrayList("@cust_cd", hdcust_cd.Value));
            arr.Add(new cArrayList("@report", "SOA"));
            arr.Add(new cArrayList("@seq_no", Convert.ToInt16(x) + 1));
            bll.vInsertsoacustomer(arr);
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@report", "SOA"));
            arr.Add(new cArrayList("@salespointcd", cbbranch.SelectedValue.ToString()));
            bll.vBindingGridToSp(ref grdcust, "sp_trp_bycustomer_get", arr);
            txcust.Text = "";
            hdcust_cd.Value = null;
            txcust.Focus();

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_soa");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grdcust_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Label lbcust_cd = (Label)grdcust.Rows[e.RowIndex].FindControl("lbcust_cd");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", cbbranch.SelectedValue.ToString()));
            arr.Add(new cArrayList("@cust_cd", lbcust_cd.Text));
            arr.Add(new cArrayList("@report", "SOA"));
            bll.vDelsoacustomer(arr); arr.Clear(); arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@report", "SOA"));
            arr.Add(new cArrayList("@salespointcd", cbbranch.SelectedValue.ToString()));
            bll.vBindingGridToSp(ref grdcust, "sp_trp_bycustomer_get", arr);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_soa");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList1(string prefixText, int count, string contextKey)
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

    protected void btdiff_Click(object sender, EventArgs e)
    {
        try
        {
            bll.vBindingGridToSp(ref grddiff, "sp_soadiff");
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_soa");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void chsummary_CheckedChanged(object sender, EventArgs e)
    {

    }
}