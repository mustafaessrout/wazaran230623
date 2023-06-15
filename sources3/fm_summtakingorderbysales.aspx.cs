using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_summtakingorderbysales : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            dtstart.Text = Request.Cookies["waz_dt"].Value.ToString();
            dtend.Text = Request.Cookies["waz_dt"].Value.ToString();
            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_getuser", "salespointcd", "salespoint_desc", arr);
            //bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_get", "salespointcd", "salespoint_desc");
            cbsalespoint.SelectedValue = Request.Cookies["sp"].Value.ToString();

            if (Request.Cookies["sp"].Value.ToString() == "0")
            {
                cbsalespoint.Enabled = true;
            }
            else
            {
                cbsalespoint.Enabled = false;
            }
        }
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBatchBfrClosingday(arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "HideProgress();openreport('fm_report2.aspx?src=summtosa&startdate=" + dtstart.Text + "&enddate=" + dtend.Text + "&sap=" + cbsalespoint.SelectedValue + "&sls="+hdsalesman_cd.Value.ToString()+"&ptyp=" + cbreptype.SelectedValue + "');", true);


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
    protected void cbreptype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbreptype.SelectedValue.ToString() == "1")
        {
            txsalesman.CssClass = "form-control";
            txsalesman.Enabled = true;
        }
        else
        {
            txsalesman.CssClass = "form-control ro";
            txsalesman.Enabled = false;
        }
    }
}