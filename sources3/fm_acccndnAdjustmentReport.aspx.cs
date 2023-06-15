using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_acccndnAdjustmentReport : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        //select* from tdosales_invoice where inv_no = 'IV10108183776'
        //select* from tpayment_dtl where inv_no = 'IV10108183776'
        //select* from tmst_payment where payment_no = 'PY10108184673'
        //select* from tmst_dosales where inv_no = 'IV10108183776'
        //select top(4) *from tmst_salesorder where so_cd = 'TO10108183309'
        //select *from tsoa_dtl where doc_no = 'PY10108184673' and refinv = 'IV10108183776'
        //select amt from tdosalesinvoice_disccash  where  inv_no = refinv

        if (!IsPostBack)
        {
            dtPostFromDate.Text = DateTime.Now.ToString("d/M/yyyy");
            dtPostToDate.Text = DateTime.Now.ToString("d/M/yyyy");
        }
    }
    protected void btPrintCNDN_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=acccndnHO');", true);
    }

    protected void btprintDiscount_Click(object sender, EventArgs e)
    {
        // not in use
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=DiscountReport&cndnFrom=" +dtPostFromDate.Text + "&cndnTo=" +dtPostToDate.Text + "');", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=DiscountReport&cndnFrom=" + dtPostFromDate.Text + "&cndnTo=" + dtPostToDate.Text + "');", true);
    }

    protected void btnInvoice_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=DiscountReportByInv&cndnFrom=" + dtPostFromDate.Text + "&cndnTo=" + dtPostToDate.Text + "');", true);
    }

    protected void btnInvoiceRaw_Click(object sender, EventArgs e)
    {
        // not in use
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=DiscountReport&cndnFrom=" + dtPostFromDate.Text + "&cndnTo=" + dtPostToDate.Text + "');", true);
    }

    protected void btnsoaRaw_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=DiscountSales&cndnFrom=" + dtPostFromDate.Text + "&cndnTo=" + dtPostToDate.Text + "');", true);
    }
    protected void btnPrintCNDNHO_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=acccndnCustomTot&query=2" + "&cndnFrom=" + dtPostFromDate.Text + "&cndnTo=" + dtPostToDate.Text + "');", true);
    }
}