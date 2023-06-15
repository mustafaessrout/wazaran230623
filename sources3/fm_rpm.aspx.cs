using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_rpm : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingFieldValueToCombo(ref cbday, "day_cd");
            bll.vBindingFieldValueToCombo(ref cbsequence, "sequenceno");
        }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetMerchandiser(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lEmployee = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string sEmployee = string.Empty;
        arr.Add(new cArrayList("@emp_nm", prefixText));
        arr.Add(new cArrayList("@job_title_cd", "merchandcd"));
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        bll.vSearchMstEmployeeByJobTitleQry(arr, ref rs);
        while (rs.Read())
        {
            sEmployee = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["emp_cd"].ToString() + "-" + rs["emp_nm"], rs["emp_cd"].ToString());
            lEmployee.Add(sEmployee);
        }
        rs.Close();
        return (lEmployee.ToArray());
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCustomer(string prefixText, int count, string contextKey)
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
            sCust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + "-" + rs["cust_nm"].ToString(), rs["cust_cd"].ToString());
            lCust.Add(sCust);
        }
        rs.Close();
        return (lCust.ToArray());
    }

    protected void btadd_Click(object sender, EventArgs e)
    {
        string sCheck = bll.vLookUp("select count(1) from trpm_dtl where emp_cd='" + hdmerchandiser.Value + "' and sequenceno=" + cbsequence.SelectedValue + " and day_cd=" + cbday.SelectedValue + "");
        if (Convert.ToDouble(sCheck) > 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please delete first existing sequence','Sequence already exists','warning');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@emp_cd", hdmerchandiser.Value));
        arr.Add(new cArrayList("@cust_cd", hdcust.Value));
        arr.Add(new cArrayList("@day_cd", cbday.SelectedValue));
        arr.Add(new cArrayList("@sequenceno", cbsequence.SelectedValue));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value));
        arr.Add(new cArrayList("@created_dt", System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@deleted", 0));
        bll.vInsertRpmDtl(arr);
        arr.Clear();
        arr.Add(new cArrayList("@emp_cd", hdmerchandiser.Value));
        arr.Add(new cArrayList("@day_cd", cbday.SelectedValue));
        bll.vBindingGridToSp(ref grd, "sp_trpm_dtl_get", arr);
        txmerchandiser.CssClass = cd.csstextro;
        txcustomer.Text = string.Empty;
        hdcust.Value = string.Empty;
        cbsequence.SelectedValue = (grd.Rows.Count + 1).ToString();
        //cbday.CssClass = "form-control ro";


    }

    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbdaycode = (Label)grd.Rows[e.RowIndex].FindControl("lbdaycode");
        Label lbsequence = (Label)grd.Rows[e.RowIndex].FindControl("lbsequence");
        HiddenField hdcustomer = (HiddenField)grd.Rows[e.RowIndex].FindControl("hdcustomer");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@emp_cd", hdmerchandiser.Value));
        arr.Add(new cArrayList("@day_cd", lbdaycode.Text));
        arr.Add(new cArrayList("@sequenceno", lbsequence.Text));
        //arr.Add(new cArrayList("@cust_cd", hdcust.Value));
        bll.vDelRpmDtl(arr);
        arr.Clear();
        arr.Add(new cArrayList("@emp_cd", hdmerchandiser.Value));
        arr.Add(new cArrayList("@day_cd", cbday.SelectedValue));
        bll.vBindingGridToSp(ref grd, "sp_trpm_dtl_get", arr);
    }

    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_rpm.aspx");
    }

    protected void cbday_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@emp_cd", hdmerchandiser.Value));
        arr.Add(new cArrayList("@day_cd", cbday.SelectedValue));
        bll.vBindingGridToSp(ref grd, "sp_trpm_dtl_get", arr);
        cbsequence.SelectedValue = (grd.Rows.Count + 1).ToString();
        //cbday.CssClass = "form-control ro";
    }

    protected void cbsequence_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btsearch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@emp_cd", hdmerchandiser.Value));
        arr.Add(new cArrayList("@day_cd", cbday.SelectedValue));
        bll.vBindingGridToSp(ref grd, "sp_trpm_dtl_get", arr);
        txmerchandiser.CssClass = cd.csstextro;
        cbsequence.SelectedValue = (grd.Rows.Count + 1).ToString();
    }
}