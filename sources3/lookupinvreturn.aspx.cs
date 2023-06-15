using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lookupinvreturn : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.DataBind();
        if (!IsPostBack)
        {
            //dtInvoiceFromDate.Text = DateTime.Now.ToString("d/M/yyyy");
            //dtInvoiceToDate.Text = DateTime.Now.ToString("d/M/yyyy");
            //cbInvoiceType
            
        }
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
    public static string[] GetCompletionInvoiceFullReturnList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        System.Data.SqlClient.SqlDataReader rs = null;
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        List<string> lCust = new List<string>();
        string sCust = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        // arr.Add(new cArrayList("@salesman_cd", contextKey));

        arr.Add(new cArrayList("@query", 5));
        arr.Add(new cArrayList("@inv_no", prefixText));
        arr.Add(new cArrayList("@inv_dtFrom", null));
        arr.Add(new cArrayList("@inv_dtTo", null));
        arr.Add(new cArrayList("@cust_cd", null));
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        bll.vSearchFullReturn(arr, ref rs);
        while (rs.Read())
        {
            sCust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["invoice_sum"].ToString(), rs["inv_no"].ToString());
            lCust.Add(sCust);
        }
        rs.Close();
        return (lCust.ToArray());
    }

    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            Label lbinvoiceno = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbinvoiceno");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "clo", "window.opener.InvSelected('" + lbinvoiceno.Text + "');window.close();", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : lookupinvreturn");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd.PageIndex = e.NewPageIndex;
        BindGrid(null);
    }

    protected void btShowData_Click(object sender, EventArgs e)
    {
        BindGrid(null);
    }

    protected void btnAllData_Click(object sender, EventArgs e)
    {
        BindGrid(4);
    }

    void BindGrid(int? query)
    {
        try
        {

            List<cArrayList> arr = new List<cArrayList>();
            if (query == 4)
            {

                arr.Add(new cArrayList("@query", 4));
                arr.Add(new cArrayList("@inv_no", null));
                arr.Add(new cArrayList("@inv_dtFrom", null));
                arr.Add(new cArrayList("@inv_dtTo", null));
                arr.Add(new cArrayList("@cust_cd", null));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vBindingGridToSp(ref grd, "sp_tdosales_invoice_getCustFullReturn", arr);
            }

            else
            {
                if (rbCustomerSearch.Checked == true)
                {
                    if (hdcust.Value == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Customer has not yet selected!','Select Customer','warning');", true);
                        return;
                    }
                    else
                    {
                        arr.Clear();
                        arr.Add(new cArrayList("@query", 3));
                        arr.Add(new cArrayList("@inv_no", null));
                        arr.Add(new cArrayList("@inv_dtFrom", null));
                        arr.Add(new cArrayList("@inv_dtTo", null));
                        arr.Add(new cArrayList("@cust_cd", Convert.ToString(hdcust.Value)));
                        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                        bll.vBindingGridToSp(ref grd, "sp_tdosales_invoice_getCustFullReturn", arr);
                    }
                }
                else if (rbInvoice.Checked == true)
                {
                    if (hdfInv.Value == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Invoice has not yet selected!','Select Invoice','warning');", true);
                        return;
                    }
                    else
                    {
                        arr.Clear();
                        arr.Add(new cArrayList("@query", 1));
                        arr.Add(new cArrayList("@inv_no", Convert.ToString(hdfInv.Value)));
                        arr.Add(new cArrayList("@inv_dtFrom", null));
                        arr.Add(new cArrayList("@inv_dtTo", null));
                        arr.Add(new cArrayList("@cust_cd", null));
                        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                        bll.vBindingGridToSp(ref grd, "sp_tdosales_invoice_getCustFullReturn", arr);
                    }
                }
                else if (rbApprovedInv.Checked == true)
                {

                    arr.Clear();
                    arr.Add(new cArrayList("@query", 6));
                    arr.Add(new cArrayList("@inv_no", null));
                    arr.Add(new cArrayList("@inv_dtFrom", null));
                    arr.Add(new cArrayList("@inv_dtTo", null));
                    arr.Add(new cArrayList("@cust_cd", null));
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    bll.vBindingGridToSp(ref grd, "sp_tdosales_invoice_getCustFullReturn", arr);


                }

            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : lookupinvreturn");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

   
}