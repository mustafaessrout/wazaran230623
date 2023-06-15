using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmARRecDev : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cbSalesPointCD.SelectedValue = Request.Cookies["sp"].Value;
            bll.vBindingComboToSp(ref cbSalesPointCD, "sp_tmst_salespoint_get", "salespointcd", "salespoint_desc");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
            arr.Add(new cArrayList("@qry_cd", "SalesJob"));
            bll.vBindingComboToSp(ref cbSalesCD, "sp_tmst_employee_sal_get", "emp_cd", "emp_desc", arr);

            bll.vBindingFieldValueToCombo(ref cbARCType, "pmtType");
            //arr.Clear();
            //arr.Add(new cArrayList("@salesPointCD", Request.Cookies["sp"].Value));
            //bll.vBindingComboToSp(ref cbBankID, "sp_tblBank_get", "BankID", "banViewName", arr);

            paymentTypeDataUnVisible();
            txrecDate.Text = DateTime.Now.ToString("d/M/yyyy");
            displayGrd();
            
        }
    }
    protected void cbARCType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbARCType.Text == "2")
        {
            paymentTypeDataUnVisible();
            grd.Columns[4].Visible = true;
            grd.Columns[5].Visible = true;
            grd.Columns[6].Visible = true;
        }
        else if (cbARCType.Text == "3")
        {
            paymentTypeDataUnVisible();
            grd.Columns[4].Visible = true;
            grd.Columns[5].Visible = true;
            grd.Columns[6].Visible = true;
            grd.Columns[7].Visible = true;
        }
        else
        {
            paymentTypeDataUnVisible();

        }
        displayGrd();
    }
    void paymentTypeDataUnVisible()
    {
        grd.Columns[4].Visible = false;
        grd.Columns[5].Visible = false;
        grd.Columns[6].Visible = false;
        grd.Columns[7].Visible = false;
    }
    private void displayGrd()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salesPointCD", cbSalesPointCD.SelectedValue));
        arr.Add(new cArrayList("@recDate", DateTime.ParseExact(txrecDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@ARCType", cbARCType.SelectedValue));
        arr.Add(new cArrayList("@SalesCD", cbSalesCD.SelectedValue));
        bll.vBindingGridToSp(ref grd, "sp_tblARRecDtlDev_get", arr);
    }
    protected void txrecDate_TextChanged(object sender, EventArgs e)
    {
        displayGrd();
    }
    protected void cbSalesCD_SelectedIndexChanged(object sender, EventArgs e)
    {
        displayGrd();
    }
}