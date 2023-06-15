using System;
using System.IO;
using Ionic.Zip;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_claimReport : System.Web.UI.Page
{
    cbll bll = new cbll();


    protected void btsearchso_Click(object sender, EventArgs e)
    {
        string reportType = string.Empty;
        reportType = ddReportType.SelectedValue;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "spro", "window.open('lookup_claim.aspx?reportType=" + reportType + "&prin=" + cbprincible.SelectedValue.ToString() + "&month=" + ddMonth.SelectedValue.ToString() + "&year=" + ddYear.SelectedValue.ToString() + "&clsta=" + ddStatus.SelectedValue.ToString() + "','mywindow','toolbar=n,scrollbars=yes,width=800,height=800,top=75,left=300',true);", true);
        
    }

    protected void btsearchProduct_Click(object sender, EventArgs e)
    {
        string prop_no = string.Empty;
        string itemByPropNo = string.Empty;
        // prop_no = txtProposal.Text;
        if (prop_no != string.Empty)
        {
            itemByPropNo = "?prop_no=" + prop_no;
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sprd", "window.open('lookup_itemSimple.aspx?item=" + itemByPropNo + "','mywindow','toolbar=n,scrollbars=y,width=800,height=800,top=75,left=300',true);", true);
    }
    protected void DownloadFiles(object sender, EventArgs e)
    {
        using (ZipFile zip = new ZipFile())
        {
            zip.AlternateEncodingUsage = ZipOption.AsNecessary;
            zip.AddDirectoryByName("Files");
            foreach (GridViewRow row in GridView1.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string filePath = "D:/images/claim_doc/" + (row.FindControl("lblFilePath") as Label).Text;
                    zip.AddFile(filePath, "Files");
                }
            }
            Response.Clear();
            Response.BufferOutput = false;
            string zipName = String.Format("" + txclaimno.Text + "_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd"));
            Response.ContentType = "application/zip";
            Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
            zip.Save(Response.OutputStream);
            Response.End();
        }
    }
    protected void btrefresh_Click(object sender, EventArgs e)
    {
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        btsearchso.Visible = true;
        lbldisc.Visible = true;
        //txtProposal.Visible = false;
        txclaimno.Visible = true;
        btsearchso.Visible = true;
        if (!IsPostBack)
        {

            string accessBy = Request.Cookies["sp"].Value.ToString();
            string currentMonth = DateTime.Now.Month.ToString();
            string currentYear = DateTime.Now.Year.ToString();

            if (currentMonth.Length == 1)
            {
                currentMonth = "0" + currentMonth;
            }
            ddMonth.SelectedValue = currentMonth;
            ddYear.SelectedValue = currentYear;

            if (accessBy == "0")
            {
                accessBy = null;
            }

            List<cArrayList> arr = new List<cArrayList>();
            bll.vBindingComboToSp(ref ddYear, "sp_tmst_period_getbyyear", "yearvalue", "yearvalue");
            arr.Add(new cArrayList("@salespointcd", accessBy));
            bll.vBindingComboToSp(ref ddBranch, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm", arr);
            arr.Clear();
            bll.vBindingComboToSp(ref cbprincible, "sp_mstvendor_get", "vendor_cd", "vendor_nm");
            arr.Clear();
            bll.vBindingFieldValueToCombo(ref ddStatus, "claim_sta_id");
            arr.Clear();
            arr.Add(new cArrayList("@hiddendata", 0));
            arr.Add(new cArrayList("@fld_nm", "claimreport"));
            bll.vBindingFieldValueToCombo(ref ddReportType, arr);
            cbprincible.Items.Insert(1, new ListItem("PAFL & MISFACO", "PM"));
            cbprincible.Items.Insert(2, new ListItem("NON PAFL & MISFACO ", "NPM"));
        }
    }
    protected void ddBranch_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddStatus_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnPrint_Click(Object sender, EventArgs e)
    {
        if (chrep.Checked)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "claimReport", "openreport('fm_report2.aspx?src=claimdtl&branch=" + ddBranch.Text + "&month=" + ddMonth.Text + "&year=" + ddYear.Text + "');", true);
        }
        else
        {
            if (cbtype.SelectedValue.ToString() == "0")
            {
                if (ddReportType.SelectedValue.ToString() == "0")
                {
                    string reportType = string.Empty;
                    reportType = ddReportType.SelectedValue;
                    // if (cbprincible.SelectedValue.ToString() == "-1")
                    //{
                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "claimReport", "openreport('fm_report2.aspx?src=claim" + reportType + "&branch=" + ddBranch.Text + "&status=" + ddStatus.Text + "&month=" + ddMonth.Text + "&year=" + ddYear.Text + "');", true);
                    //}
                    //else
                    //{
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "claimReport", "openreport('fm_report2.aspx?src=claim" + reportType + "&branch=" + ddBranch.Text + "&status=" + ddStatus.Text + "&month=" + ddMonth.Text + "&year=" + ddYear.Text + "&prin=" + cbprincible.SelectedValue.ToString() + "');", true);
                    //}

                }
                else
                {
                    string reportType = string.Empty;
                    reportType = ddReportType.SelectedValue;
                    List<cArrayList> arr = new List<cArrayList>();
                    arr.Add(new cArrayList("@claim_no", txclaimno.Text));
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));

                    Session["FG"] = arr;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "claimReport", "openreport('fm_report2.aspx?src=claim" + reportType + "');", true);
                }
            }
            else if (cbtype.SelectedValue == "1")
            {
                if (ddReportType.SelectedValue.ToString() != "ba")
                {
                    string reportType = string.Empty;
                    reportType = ddReportType.SelectedValue;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "claimReport", "openreport('fm_report2.aspx?src=claim" + reportType + "&branch=" + ddBranch.Text + "&status=" + ddStatus.Text + "&month=" + ddMonth.Text + "&year=" + ddYear.Text + "&prin=" + cbprincible.SelectedValue.ToString() + "');", true);
                }
                else
                {
                    List<cArrayList> arr = new List<cArrayList>();
                    arr.Add(new cArrayList("@claim_no", txclaimno.Text));
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));                    
                    
                    Session["FG"] = arr;
                    string reportType = string.Empty;
                    reportType = ddReportType.SelectedValue;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "claimReport", "openreport('fm_report2.aspx?src=claimFG');", true);
                }
            }
            else if (cbtype.SelectedValue == "2")
            {
                if (ddReportType.SelectedValue.ToString() == "0")
                {
                    string reportType = string.Empty;
                    reportType = ddReportType.SelectedValue;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "claimReport", "openreport('fm_report2.aspx?src=claim" + reportType + "&branch=" + ddBranch.Text + "&status=" + ddStatus.Text + "&month=" + ddMonth.Text + "&year=" + ddYear.Text + "&prin=" + cbprincible.SelectedValue.ToString() + "');", true);
                }
                else
                {
                    List<cArrayList> arr = new List<cArrayList>();
                    arr.Add(new cArrayList("@claim_no", txclaimno.Text));
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    Session["Secndn"] = arr;
                    string reportType = string.Empty;
                    reportType = ddReportType.SelectedValue;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "claimReport", "openreport('fm_report2.aspx?src=claim" + reportType + "');", true);
                }
            }
            else if (cbtype.SelectedValue == "3")
            {
                if (ddReportType.SelectedValue.ToString() == "0")
                {
                    List<cArrayList> arr = new List<cArrayList>();
                    arr.Add(new cArrayList("@claim_no", txclaimno.Text));
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    Session["Secshout"] = arr;
                    string reportType = string.Empty;
                    reportType = ddReportType.SelectedValue;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "claimReport", "openreport('fm_report2.aspx?src=claim" + reportType + "');", true);
                }
                else
                {
                    List<cArrayList> arr = new List<cArrayList>();
                    arr.Add(new cArrayList("@claim_no", txclaimno.Text));
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    arr.Add(new cArrayList("@customer", null));
                    //rep.vShowReportToEXCEL("rp_claimCashOut.rpt", arr, sPath + sPdfName1);
                    Session["Secshout"] = arr;
                    string reportType = string.Empty;
                    reportType = ddReportType.SelectedValue;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "claimReport", "openreport('fm_report2.aspx?src=claim" + reportType + "');", true);
                }
            }
            else if (cbtype.SelectedValue.ToString() == "4")
            {
                string reportType = string.Empty;
                reportType = cbtype.SelectedValue;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "claimReport", "openreport('fm_report2.aspx?src=claimMonthly" + "&branch=" + ddBranch.Text + "&status=" + ddStatus.Text + "&month=" + ddMonth.Text + "&year=" + ddYear.Text + "&prin=" + cbprincible.SelectedValue.ToString() + "');", true);

            }
        }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList2(string prefixText, int count, string contextKey)
    {
        HttpCookie cook;
        cook = HttpContext.Current.Request.Cookies["sp"];
        cbll bll = new cbll();
        List<string> lItem = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string sItem = string.Empty;
        System.Data.SqlClient.SqlDataReader rs = null;
        arr.Add(new cArrayList("@salespointcd", cook.Value.ToString()));
        //arr.Add(new cArrayList("@item_cd", prefixText));
        bll.vSearchMstItem(arr, ref rs);
        while (rs.Read())
        {
            sItem = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["item_cd"].ToString() + "-" + rs["item_nm"].ToString() + "-" + rs["size"].ToString() + "-" + rs["branded_nm"].ToString(), rs["item_cd"].ToString());
            lItem.Add(sItem);
        }
        rs.Close();
        return (lItem.ToArray());
    }
    protected void ddReportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbtype.SelectedValue != "4")
        {
            lbldisc.Visible = true;
            //txtProposal.Visible = true;
            btsearchso.Visible = true;
            txclaimno.Visible = true;
            btsearchso.Visible = true;
        }
        else
        {
            btsearchso.Visible = false;
            lbldisc.Visible = false;
            //txtProposal.Visible = false;
            txclaimno.Visible = false;
            btsearchso.Visible = false;
        }
    }
    protected void chrep_CheckedChanged(object sender, EventArgs e)
    {
        if (chrep.Checked)
        {
            ddReportType.Enabled = false;
            cbdep.Enabled = true;
        }
        else
        {
            ddReportType.Enabled = true;
            cbdep.Enabled = false;
        }
    }
    protected void btsl_Click(object sender, EventArgs e)
    {

        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@claim_no", txclaimno.Text));
        bll.vBindingGridToSp(ref GridView1, "sp_getClaimDoc", arr);
        btnDownload.Visible = true;
    }

    protected void cbtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();

        arr.Add(new cArrayList("@hiddendata", 0));
        if (cbtype.SelectedValue.ToString() == "0")
        {
            ddReportType.Enabled = true;
            arr.Add(new cArrayList("@fld_nm", "claimreport"));
            bll.vBindingFieldValueToCombo(ref ddReportType, arr);
        }
        else if (cbtype.SelectedValue.ToString() == "1")
        {
            ddReportType.Enabled = true;
            arr.Add(new cArrayList("@fld_nm", "claimreportba"));
            bll.vBindingFieldValueToCombo(ref ddReportType, arr);
        }
        else if (cbtype.SelectedValue.ToString() == "2")
        {
            ddReportType.Enabled = true;
            arr.Add(new cArrayList("@fld_nm", "claimreportcndn"));
            bll.vBindingFieldValueToCombo(ref ddReportType, arr);
        }
        else if (cbtype.SelectedValue.ToString() == "3")
        {
            ddReportType.Enabled = true;
            arr.Add(new cArrayList("@fld_nm", "claimreportOT"));
            bll.vBindingFieldValueToCombo(ref ddReportType, arr);
        }
        else if (cbtype.SelectedValue.ToString() == "4")
        {
            btsearchso.Visible = false;
            lbldisc.Visible = false;
            //txtProposal.Visible = false;
            txclaimno.Visible = false;
            btsearchso.Visible = false;
            ddReportType.Enabled = false;
        }

    }
}