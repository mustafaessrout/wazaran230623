using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_fm_custblock2 : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbsp.Text = bll.vLookUp("select salespointcd+'-'+salespoint_nm from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            
            bll.vBindingFieldValueToCombo(ref cbotlcd, "otlcd");
            bindinggrdotlcd();
            bll.vBindingComboToSp(ref cbotlcd2, "sp_customerdoc_blockotlcd_get", "otlcd", "otlnm");
            bindinggrdcusgrcd();
            cusgroup();
            bindinggrdcust_cd();

        }
    }
    private void cusgroup()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@otlcd", cbotlcd2.SelectedValue));
        bll.vBindingComboToSp(ref cbcusgrcd, "sp_customerdoc_blockcusgrcd_get", "cusgrcd", "cusgrnm", arr);
    }

    protected void btadd_Click(object sender, EventArgs e)
    {
        int chk = 0;
        foreach (ListItem lst in chdoc_cd.Items)
        {
            if (lst.Selected == true)
            {
                chk += 1;
            }

        }
        if (chk == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please check minimum one Document !','warning');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "vDisableShow();", true);
            return;
        }
        if (dtfr_dt.Text.ToString() == "" || dtto_dt.Text.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Date start and date to can not empty !','warning');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "vDisableShow();", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@otlcd",cbotlcd.SelectedValue));
        arr.Add(new cArrayList("@fr_dt", DateTime.ParseExact(dtfr_dt.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@to_dt", DateTime.ParseExact(dtto_dt.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@doc_cd", lbdoc_cd.Text));
        bll.vInserttcustomerdocblock_otlcd(arr);
        lbdoc_cd.Text = "";

        foreach (ListItem lst in chdoc_cd.Items)
        {
            if (lst.Selected == true)
            {
                arr.Clear();
                arr.Add(new cArrayList("@otlcd", cbotlcd.SelectedValue));
                arr.Add(new cArrayList("@doc_cd", lst.Value));
                bll.vInserttcustomerdocblock_doc_cd(arr);
            }

        }
        chdoc_cd.ClearSelection();

        bindinggrdotlcd();
        bindinggrdcusgrcd();
        cusgroup();
        bindinggrdcust_cd();
    }
    private void bindinggrdotlcd()
    {
        bll.vBindingGridToSp(ref grdotlcd, "sp_tcustomerdocblock_otlcd_get");
    }
    private void bindinggrdcusgrcd()
    {
        bll.vBindingGridToSp(ref grdcusgrcd, "sp_tcustomerdocblock_cusgrcd_get");
    }
    private void bindinggrdcust_cd()
    {
        bll.vBindingGridToSp(ref grdcust_cd, "sp_tcustomerdocblock_cust_cd_get");
    }
    protected void grdotlcd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbOtlcd = (Label)grdotlcd.Rows[e.RowIndex].FindControl("lbOtlcd");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@otlcd", lbOtlcd.Text));
        bll.vDeletetcustomerdocblock_otlcd(arr);
        bindinggrdotlcd();
        bindinggrdcusgrcd();
        cusgroup();
        bindinggrdcust_cd();
    }
    protected void chcuscate_cd_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbcuscate_cd.Text = "";
        string selectedItems = String.Join(",",
         chcuscate_cd.Items.OfType<ListItem>().Where(r => r.Selected)
        .Select(r => r.Text));
        lbcuscate_cd.Text = selectedItems;  
    }
    protected void btaddcusgrcd_Click(object sender, EventArgs e)
    {
        int chk = 0;
        foreach (ListItem lst in chcuscate_cd.Items)
        {
            if (lst.Selected == true)
            {
                chk += 1;
            }

        }
        if (chk == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please check customer category !','" + cbcusgrcd.SelectedValue + "','warning');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "vDisableShow();", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@otlcd", cbotlcd2.SelectedValue));
        arr.Add(new cArrayList("@cusgrcd", cbcusgrcd.SelectedValue));
        arr.Add(new cArrayList("@cuscate_cd", lbcuscate_cd.Text));
        bll.vInserttcustomerdocblock_cusgrcd(arr);

        lbcuscate_cd.Text = "";

        foreach (ListItem lst in chcuscate_cd.Items)
        {
            if (lst.Selected == true)
            {
                arr.Clear();
                arr.Add(new cArrayList("@otlcd", cbotlcd2.SelectedValue));
                arr.Add(new cArrayList("@cusgrcd", cbcusgrcd.SelectedValue));
                arr.Add(new cArrayList("@cuscate_cd", lst.Text));
                bll.vInserttcustomerdocblock_cuscate_cd(arr);
            }

        }
        chcuscate_cd.ClearSelection();
        bindinggrdotlcd();
        bindinggrdcusgrcd();
        cusgroup();
        bindinggrdcust_cd();
    }
    protected void cbotlcd2_SelectedIndexChanged(object sender, EventArgs e)
    {
        cusgroup();
    }
    protected void grdcusgrcd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbOtlcd2 = (Label)grdcusgrcd.Rows[e.RowIndex].FindControl("lbOtlcd2");
        Label lbcusgrcd = (Label)grdcusgrcd.Rows[e.RowIndex].FindControl("lbcusgrcd");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@Otlcd", lbOtlcd2.Text));
        arr.Add(new cArrayList("@cusgrcd", lbcusgrcd.Text));
        bll.vDeletetcustomerdocblock_cusgrcd(arr);
        bindinggrdotlcd();
        bindinggrdcusgrcd();
        cusgroup();
        bindinggrdcust_cd();
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lCust = new List<string>();
        string sCust = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cust_cd", prefixText));
        bll.vSearchtcustomerdocblock_cust(arr, ref rs);
        while (rs.Read())
        {
            sCust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + "-" + rs["cust_nm"].ToString(), rs["cust_cd"].ToString());
            lCust.Add(sCust);
        }
        rs.Close();
        return (lCust.ToArray());
    }
    protected void btaddcust_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cust_cd", hdcust.Value));
        bll.vInserttcustomerdocblock_cust_cd(arr);
        hdcust.Value = null;
        txcustomer.Text = null;
        bindinggrdotlcd();
        bindinggrdcusgrcd();
        cusgroup();
        bindinggrdcust_cd();
    }
    protected void grdcust_cd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //Label lbOtlcd3 = (Label)grdcust_cd.Rows[e.RowIndex].FindControl("lbOtlcd3");
        //Label lbcusgrcd3 = (Label)grdcust_cd.Rows[e.RowIndex].FindControl("lbcusgrcd3");
        Label lbcust_cd = (Label)grdcust_cd.Rows[e.RowIndex].FindControl("lbcust_cd");
        List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@Otlcd", lbOtlcd3.Text));
        //arr.Add(new cArrayList("@cusgrcd", lbcusgrcd3.Text));
        arr.Add(new cArrayList("@cust_cd", lbcust_cd.Text));
        bll.vDeletetcustomerdocblock_cust_cd(arr);
        bindinggrdotlcd();
        bindinggrdcusgrcd();
        cusgroup();
        bindinggrdcust_cd();
    }
    protected void chdoc_cd_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbdoc_cd.Text = "";
        string selectedItems = String.Join(",",
         chdoc_cd.Items.OfType<ListItem>().Where(r => r.Selected)
        .Select(r => r.Text));
        lbdoc_cd.Text = selectedItems;  
    }
}