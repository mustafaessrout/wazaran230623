using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_acccndnCustomerBranchReport : System.Web.UI.Page
{
    cbll bll = new cbll();
    cdal cdl = new cdal();
    Utitlity ut = new Utitlity();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dtCNDNFromDate.Text = DateTime.Now.ToString("d/M/yyyy");
            dtCNDNToDate.Text = DateTime.Now.ToString("d/M/yyyy");
            
        }
    }
    protected void btPrintCNDN_Click(object sender, EventArgs e)
    {
        BindGrid();
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
    public static string[] GetCompletionCNDNList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        System.Data.SqlClient.SqlDataReader rs = null;
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        List<string> lCust = new List<string>();
        string sCust = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        // arr.Add(new cArrayList("@salesman_cd", contextKey));
        arr.Add(new cArrayList("@hoRefNo", prefixText));
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        bll.vSearchCNDNAdjustment(arr, ref rs);
        while (rs.Read())
        {
            sCust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["refho_no"].ToString() + "-" + rs["cust_cd"].ToString() + "-" + rs["cust_nm"].ToString(), rs["cndn_no"].ToString());
            lCust.Add(sCust);
        }
        rs.Close();
        return (lCust.ToArray());
    }

    void BindGrid() {
        List<cArrayList> arr = new List<cArrayList>();
        if (rbCNDNSearch.Checked == true)
        {
            arr.Add(new cArrayList("@query", 1));
            arr.Add(new cArrayList("@cndnFrom", DateTime.ParseExact(dtCNDNFromDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@cndnTo", DateTime.ParseExact(dtCNDNToDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        }
        else if (rbCustomerSearch.Checked == true)
        {
            arr.Add(new cArrayList("@query",2));
            arr.Add(new cArrayList("@cust_cd", Convert.ToString(hdcust.Value)));
        }
        else if (rbHOSearch.Checked == true)
        {
            arr.Add(new cArrayList("@query", 4));
            arr.Add(new cArrayList("@cndn_no", Convert.ToString(hdfcndn.Value)));
        }
        //else if (rbPostSearch.Checked == true)
        //{
        //    arr.Add(new cArrayList("@query", 1));
        //    arr.Add(new cArrayList("@postFrom", DateTime.ParseExact(dtPostFromDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        //    arr.Add(new cArrayList("@postTo", DateTime.ParseExact(dtPostToDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        //}
        bll.vBindingGridToSp(ref grd, "sp_tmst_cndnCustomerCustom_get", arr);
        //ShowingGroupingDataInGridView(grd.Rows, 0, 3);
    }

    void ShowingGroupingDataInGridView(GridViewRowCollection gridViewRows, int startIndex, int totalColumns)
    {
        if (totalColumns == 0) return;
        int i, count = 1;
        ArrayList lst = new ArrayList();
        lst.Add(gridViewRows[0]);
        var ctrl = gridViewRows[0].Cells[startIndex];
        for (i = 1; i < gridViewRows.Count; i++)
        {
            TableCell nextTbCell = gridViewRows[i].Cells[startIndex];
            if (ctrl.Text == nextTbCell.Text)
            {
                count++;
                nextTbCell.Visible = false;
                lst.Add(gridViewRows[i]);
            }
            else
            {
                if (count > 1)
                {
                    ctrl.RowSpan = count;
                    ShowingGroupingDataInGridView(new GridViewRowCollection(lst), startIndex + 1, totalColumns - 1);
                }
                count = 1;
                lst.Clear();
                ctrl = gridViewRows[i].Cells[startIndex];
                lst.Add(gridViewRows[i]);
            }
        }
        if (count > 1)
        {
            ctrl.RowSpan = count;
            
            //
            ShowingGroupingDataInGridView(new GridViewRowCollection(lst), startIndex + 1, (totalColumns + 2) - 1);
            ShowingGroupingDataInGridView(new GridViewRowCollection(lst), startIndex + 1, (totalColumns + 3)- 1);
            ShowingGroupingDataInGridView(new GridViewRowCollection(lst), startIndex + 1, totalColumns - 1);
        }
        count = 1;
        lst.Clear();
    }

    protected void btnViewReport_Click(object sender, EventArgs e)
    {
        if (rbCNDNSearch.Checked == true)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=cndnCustomerCustom&query=1" +
                "&cndnFrom=" + dtCNDNFromDate.Text + "&cndnTo="+ dtCNDNToDate.Text + "&cndnType=sort');", true);
        }
        else if (rbCustomerSearch.Checked == true)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=cndnCustomerCustom&query=2&cust_cd=" + Convert.ToString(hdcust.Value) + "&cndnType=sort');", true);
        }
        if (rbHOSearch.Checked == true)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=cndnCustomerCustom&query=3&cndn_no=" + Convert.ToString(hdfcndn.Value) + "&cndnType=sort');", true);
        }
        //else if (rbPostSearch.Checked == true)
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=acccndnCustom&query=1" +
        //      "&postFrom=" + dtPostFromDate.Text + "&postTo=" + dtPostToDate.Text + "');", true);
        //}
    }

    protected void btnViewReportDetails_Click(object sender, EventArgs e)
    {
        if (rbCNDNSearch.Checked == true)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=cndnCustomerCustom&query=1" +
                "&cndnFrom=" + dtCNDNFromDate.Text + "&cndnTo=" + dtCNDNToDate.Text + "&cndnType=details');", true);
        }
        else if (rbCustomerSearch.Checked == true)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=cndnCustomerCustom&query=2&cust_cd=" + Convert.ToString(hdcust.Value) + "&cndnType=details');", true);
        }
        if (rbHOSearch.Checked == true)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=cndnCustomerCustom&query=3&cndn_no=" + Convert.ToString(hdfcndn.Value) + "&cndnType=details');", true);
        }
        //else if (rbPostSearch.Checked == true)
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=acccndnCustom&query=1" +
        //      "&postFrom=" + dtPostFromDate.Text + "&postTo=" + dtPostToDate.Text + "');", true);
        //}
    }
}