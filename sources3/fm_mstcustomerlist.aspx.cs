using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_mstcustomerlist : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vBindingGridToSp(ref grd, "sp_tmst_customer_get",arr);
            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstcustomerlist");
                Response.Redirect("fm_ErrorPage.aspx");
            }
        }
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_customerentry.aspx");
    }
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grd.PageIndex = e.NewPageIndex;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_tmst_customer_get",arr);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstcustomerlist");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            Label lbcustcode = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbcustcode");
            // Label lbcustcode = (Label)grd.Rows[e.NewEditIndex].FindControl("lbcustcode");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@cust_cd", lbcustcode.Text));
            arr.Add(new cArrayList("@cust_sta_id", "I"));
            bll.vUpdateMstCustomer(arr);
            //  grd.EditIndex = -1;
            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_tmst_customer_get",arr);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Customer No. " + lbcustcode.Text + " has been deactivated !','Deactivated','warning');", true);
            // Response.Redirect("fm_mstcustomerentry.aspx?cus=" + lbcustcode.Text);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstcustomerlist");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        cbll bll = new cbll();
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lcust = new List<string>();
        string sCust = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cust_cd", prefixText));
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        bll.vSearchMstCustomer(arr, ref rs);
        while (rs.Read())
        {
            sCust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + " - " + rs["cust_nm"].ToString() + " - " + rs["cust_arabic"].ToString(),rs["cust_cd"].ToString());
            lcust.Add(sCust);
        } rs.Close();
        return (lcust.ToArray());
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            if (txcustsearch.Text == "")
            {
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            }
            else
            {
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
            }
            bll.vBindingGridToSp(ref grd, "sp_tmst_customer_get", arr);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstcustomerlist");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Clear();
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        //arr.Add(new cArrayList("@cust_cd", null));
        Session["lParam_mst_customer"] = arr;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=master_customer');", true);
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=cu');", true);
    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            Label lbcustcode = (Label)grd.Rows[e.NewEditIndex].FindControl("lbcustcode");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@cust_cd", lbcustcode.Text));
            arr.Add(new cArrayList("@cust_sta_id", "I"));
            bll.vUpdateMstCustomer(arr);
            grd.EditIndex = -1;
            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_tmst_customer_get");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Customer No. " + lbcustcode.Text + " has been deactivated !','Deactivated','warning');", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstcustomerlist");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grd_RowEditing1(object sender, GridViewEditEventArgs e)
    {
        try
        {
            grd.EditIndex = e.NewEditIndex;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_tmst_customer_get",arr);
            //TextBox txlatitude = (TextBox)grd.Rows[e.NewEditIndex].FindControl("txlatitude");
            //TextBox txlongitude = (TextBox)grd.Rows[e.NewEditIndex].FindControl("txlongitude");
            //List<cArrayList> arr = new List<cArrayList>();
            //arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
            //arr.Add(new cArrayList("@latitude", txlatitude.Text));
            //arr.Add(new cArrayList("@longitude", txlongitude.Text));
            //bll.vUpdateTMstCustomerByMaps(arr);
            //arr.Clear();
            //grd.EditIndex = -1;
            //bll.vBindingGridToSp(ref grd, "sp_tmst_customer_get");

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstcustomerlist");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {

            grd.EditIndex = -1;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_tmst_customer_get",arr);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstcustomerlist");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            TextBox txlatitude = (TextBox)grd.Rows[e.RowIndex].FindControl("txlatitude");
            TextBox txlongitude = (TextBox)grd.Rows[e.RowIndex].FindControl("txlongitude");
            Label lbcustcode = (Label)grd.Rows[e.RowIndex].FindControl("lbcustcode");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@cust_cd", lbcustcode.Text));
            arr.Add(new cArrayList("@latitude", txlatitude.Text));
            arr.Add(new cArrayList("@longitude", txlongitude.Text));
            bll.vUpdateTMstCustomerByMaps(arr);
            arr.Clear();
            grd.EditIndex = -1;
            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_tmst_customer_get",arr);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstcustomerlist");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
}