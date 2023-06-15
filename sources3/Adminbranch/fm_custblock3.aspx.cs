using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_fm_custblock3 : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbsp.Text = bll.vLookUp("select salespointcd+'-'+salespoint_nm from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            bll.vBindingFieldValueToCombo(ref cbcustcate_cd, "custcate_cd");
            bindinggrdcustcate_cd();
            bindinggrdcust_cd();
        }
        
    }
    protected void btAdd_Click(object sender, EventArgs e)
    {
        int chkdoc_cd = 0;
        int chkotlcd = 0;
        foreach (ListItem lst in chdoc_cd.Items)
        {
            if (lst.Selected == true)
            {
                chkdoc_cd += 1;
            }

        }
        if (chkdoc_cd == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please check minimum one Document !','warning');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "vDisableShow();", true);
            return;
        }
        
        //check otlcd
        foreach (ListItem lst2 in chotlcd.Items)
        {
            if (lst2.Selected == true)
            {
                chkotlcd += 1;
            }

        }
        if (chkotlcd == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please check minimum one Outled !','warning');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "vDisableShow();", true);
            return;
        }
        
        if (dtfr.Text.ToString() == "" || dtto.Text.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Date start and date to can not empty !','warning');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "vDisableShow();", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@custcate_cd", cbcustcate_cd.SelectedValue));
        arr.Add(new cArrayList("@fr_dt", DateTime.ParseExact(dtfr.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@to_dt", DateTime.ParseExact(dtto.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@doc_cd", lbdoc_cd.Text));
        arr.Add(new cArrayList("@otlcd", lbotlcd.Text));
        bll.vInserttdocblockcustomer_custcate_cd(arr);

        lbdoc_cd.Text = "";
        foreach (ListItem lst in chdoc_cd.Items)
        {
            if (lst.Selected == true)
            {
                arr.Clear();
                arr.Add(new cArrayList("@custcate_cd", cbcustcate_cd.SelectedValue));
                arr.Add(new cArrayList("@doc_cd", lst.Value));
                bll.vInserttdocblockcustomer_doc_cd(arr);
            }

        }
        chdoc_cd.ClearSelection();

        lbotlcd.Text = "";
        foreach (ListItem lst2 in chotlcd.Items)
        {
            if (lst2.Selected == true)
            {
                arr.Clear();
                arr.Add(new cArrayList("@custcate_cd", cbcustcate_cd.SelectedValue));
                arr.Add(new cArrayList("@otlcd", lst2.Value));
                bll.vInserttdocblockcustomer_otlcd(arr);
            }

        }
        chotlcd.ClearSelection();
        bindinggrdcustcate_cd();
        bindinggrdcust_cd();
    }
    private void bindinggrdcustcate_cd()
    {
        bll.vBindingGridToSp(ref grdcustcate_cd, "sp_tcustomerdocblock_custcate_cd_get");
    }
    protected void chdoc_cd_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbdoc_cd.Text = "";
        string selectedItems = String.Join(",",
        chdoc_cd.Items.OfType<ListItem>().Where(r => r.Selected)
        .Select(r => r.Text));
        lbdoc_cd.Text = selectedItems;  
    }
    protected void chotlcd_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbotlcd.Text = "";
        string selectedItems = String.Join(",",
        chotlcd.Items.OfType<ListItem>().Where(r => r.Selected)
        .Select(r => r.Text));
        lbotlcd.Text = selectedItems;  
    }
    protected void grdcustcate_cd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbcustcate_cd = (Label)grdcustcate_cd.Rows[e.RowIndex].FindControl("lbcustcate_cd");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@custcate_cd", lbcustcate_cd.Text));
        bll.vDeletetdocblockcustomer_custcate_cd(arr);
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
        bll.vSearchtdocblockcustomer_cust(arr, ref rs);
        while (rs.Read())
        {
            sCust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + "-" + rs["cust_nm"].ToString(), rs["cust_cd"].ToString());
            lCust.Add(sCust);
        }
        rs.Close();
        return (lCust.ToArray());
    }
    protected void btAddCust_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cust_cd", hdcust.Value));
        bll.vInserttdocblockcustomer_cust_cd(arr);
        hdcust.Value = null;
        txcustomer.Text = null;
        bindinggrdcust_cd();

    }
    private void bindinggrdcust_cd()
    {
        bll.vBindingGridToSp(ref grdcust_cd, "sp_tdocblockcustomer_cust_cd_exclude_get");
    }
    protected void grdcust_cd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbcust_cd = (Label)grdcust_cd.Rows[e.RowIndex].FindControl("lbcust_cd");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cust_cd", lbcust_cd.Text));
        bll.vDeletetdocblockcustomer_cust_cd_exclude(arr);
        bindinggrdcust_cd();
    }
    protected void cbcustcate_cd_SelectedIndexChanged(object sender, EventArgs e)
    {
    
    //    ListItem item;
    //foreach ( itemlist in CheckBoxList1.Items) {
    //if (itemlist .Value == 4) {
    //        itemlist .Attributes.Add("style", "display:none");
    //    }
    //} 

    }
}