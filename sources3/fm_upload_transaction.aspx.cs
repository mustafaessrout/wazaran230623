using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;
using System.IO;

public partial class fm_upload_transaction : System.Web.UI.Page
{
    public string pfilename { set; get; }
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                List<cArrayList> arr = new List<cArrayList>();
                arr.Clear();
                string waz_dt;
                string sho;

                waz_dt = Request.Cookies["waz_dt"].Value.ToString();
                DateTime dtwaz_dt = DateTime.ParseExact(waz_dt, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                arr.Clear();
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                bll.vBindingComboToSp(ref cbSalesPointCD, "sp_tmst_salespoint_getuser", "salespointcd", "salespoint_desc", arr);
                sho = Request.Cookies["sp"].Value.ToString();
                if (sho == "0")
                {
                    cbSalesPointCD.Enabled = true;
                    //cbSalesPointCD.CssClass = "";
                }
                else
                {
                    cbSalesPointCD.SelectedValue = sho;
                    cbSalesPointCD.Enabled = false;
                    cbSalesPointCD.CssClass = "makeitreadonly ro form-control";
                }
                arr.Clear();
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //bll.vDelTmpSalesOrderXls(arr);
                arr.Clear();
                bindinggrd();
            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_upload_transaction");
                Response.Redirect("fm_loginol.aspx");
            }
            
        }
    }


    private void bindinggrd()
    {
        dtbranch.Text = bll.sGetControlParameterSalespoint("wazaran_dt", cbSalesPointCD.SelectedValue.ToString());
        string ttype = cbtypetrans.SelectedValue.ToString();
    }

    protected void cbSalesPointCD_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void cbtypetrans_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    void vSearchGrid()
    {
        try
        {
            if (!fup.HasFile)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('File excel must selected','File Upload XLS','warning');", true);
                return;
            }
            FileInfo fi = null;
            fi = new FileInfo(fup.FileName);

            pfilename = "S_" + Request.Cookies["sp"].Value.ToString() + "_" + Request.Cookies["usr_id"].Value.ToString() + "_" + Convert.ToString(DateTime.Today.Day) + Convert.ToString(DateTime.Today.Month) + Convert.ToString(DateTime.Today.Year) + fi.Extension.ToString();
            fup.SaveAs(bll.vLookUp("select dbo.fn_getcontrolparameter('image_path')") + @"\temp\transaction\" + pfilename);


            string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\wazaran\\images\\temp\\transaction\\" + pfilename + ";Extended Properties=Excel 12.0";
            //string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\images\\temp\\transaction\\" + pfilename + ";Extended Properties=Excel 12.0";
            OleDbConnection oledbConn = new OleDbConnection(connString);
            oledbConn.Open();
            OleDbCommand cmd = new OleDbCommand();
            DataTable Sheets = new DataTable();
            Sheets = oledbConn.GetSchema("Tables");
            string firstSheets = Sheets.Rows[0][2].ToString();
            Console.WriteLine(firstSheets);

            cmd = new OleDbCommand("SELECT * FROM [" + firstSheets + "] where Branch_Code is not null", oledbConn);
            OleDbDataAdapter oleda = new OleDbDataAdapter();
            oleda.SelectCommand = cmd;
            DataSet ds = new DataSet();
            oleda.Fill(ds, "transactionxls");
            grdto.DataSource = ds.Tables[0].DefaultView;
            grdto.DataBind();
            grdto.Style.Add("display", "normal");

            oledbConn.Close();
            oledbConn = null;

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_upload_transaction");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void btsearch_Click(object sender, EventArgs e)
    {
        if (!fup.HasFile)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('select XLS file','File not selected','warning');", true);
            return;
        }
        vSearchGrid();
    }

    protected void btexport_Click(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            string isSalesmanAvb = "", isCustomerAvb = "", isCustomerSalesman = "", isItemAvb = "";
            foreach (GridViewRow row in grdto.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    Label lbso_dt = (Label)row.FindControl("lbso_dt");
                    Label lbsp = (Label)row.FindControl("lbsp");
                    Label lbsalesman = (Label)row.FindControl("lbsalesman");
                    Label lbcustomer = (Label)row.FindControl("lbcustomer");
                    Label lbitem = (Label)row.FindControl("lbitem");

                    isSalesmanAvb = bll.vLookUp("select isnull(emp_cd,'') from tmst_employee where emp_cd = '" + lbsalesman.Text.ToString() + "' and salespointcd='" + lbsp.Text.ToString() + "'");
                    isCustomerAvb = bll.vLookUp("select isnull(cust_cd,'') from tmst_customer where cust_cd = '" + lbcustomer.Text.ToString() + "'");
                    isCustomerSalesman = bll.vLookUp("select isnull(cust_cd,'') from tmst_customer where cust_cd='" + lbcustomer.Text.ToString() + "' and salesman_cd='" + lbsalesman.Text.ToString() + "'");
                    isItemAvb = bll.vLookUp("select isnull(item_cd,'') from tmst_item where item_cd='" + lbitem.Text.ToString() + "'");

                    if (lbsp.Text.ToString() != Request.Cookies["sp"].Value.ToString())
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Branch Code must be same with this branch !!!','Check Branch Code','warning');", true);
                        return;
                    }
                    // Check Data Salesman & Customer & Item
                    if (isItemAvb == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Item : " + lbitem.Text.ToString() + ", not available','Check Item Code','warning');", true);
                        return;
                    }
                    if (isSalesmanAvb == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Salesman : " + lbsalesman.Text.ToString() + ", not available in this branch','Check Salesman Code','warning');", true);
                        return;
                    }
                    if (isCustomerAvb == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Customer : " + lbcustomer.Text.ToString() + ", not available in this branch','Check Customer Code','warning');", true);
                        return;
                    }
                    if (isCustomerSalesman == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Customer : " + lbcustomer.Text.ToString() + ", not belongs to this Salesman : " + lbsalesman.Text.ToString() + "','Check Customer/Salesman Code','warning');", true);
                        return;
                    }
                    //
                    if (lbsp.Text.ToString() != Request.Cookies["sp"].Value.ToString())
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Branch Code must be same with this branch !!!','Check Branch Code','warning');", true);
                        return;
                    }
                    if (DateTime.ParseExact(lbso_dt.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture) != DateTime.ParseExact(dtbranch.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Transaction Date must be same with branch date !!!','Check Transaction Date','warning');", true);
                        return;
                    }
                }
            }

            foreach (GridViewRow row in grdto.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    Label lbso_dt = (Label)row.FindControl("lbso_dt");
                    Label lbsp = (Label)row.FindControl("lbsp");
                    Label lbsalesman = (Label)row.FindControl("lbsalesman");
                    Label lbcustomer = (Label)row.FindControl("lbcustomer");
                    Label lbpayment = (Label)row.FindControl("lbpayment");
                    Label lbst = (Label)row.FindControl("lbst");
                    Label lbordertyp = (Label)row.FindControl("lbordertyp");
                    Label lbitem = (Label)row.FindControl("lbitem");
                    HiddenField hdqty_ctn = (HiddenField)row.FindControl("hdqty_ctn");
                    HiddenField hdqty_pcs = (HiddenField)row.FindControl("hdqty_pcs");
                    HiddenField hdprice_ctn = (HiddenField)row.FindControl("hdprice_ctn");
                    HiddenField hdprice_pcs = (HiddenField)row.FindControl("hdprice_pcs");
                    Label lbsubtotal = (Label)row.FindControl("lbsubtotal");
                    Label lbtot_inv = (Label)row.FindControl("lbtot_inv");
                    Label lbtot_payment = (Label)row.FindControl("lbtot_payment");
                    arr.Clear();

                    arr.Add(new cArrayList("@branch_code", lbsp.Text.ToString()));
                    arr.Add(new cArrayList("@salesman_code", lbsalesman.Text.ToString()));
                    arr.Add(new cArrayList("@customer_code", lbcustomer.Text.ToString()));
                    arr.Add(new cArrayList("@transaction_date", lbso_dt.Text.ToString()));
                    arr.Add(new cArrayList("@payment_type", lbpayment.Text.ToString()));
                    arr.Add(new cArrayList("@source_tax", lbst.Text.ToString()));
                    arr.Add(new cArrayList("@order_type", lbordertyp.Text.ToString()));
                    arr.Add(new cArrayList("@item_code", lbitem.Text.ToString()));
                    arr.Add(new cArrayList("@qty_ctn", hdqty_ctn.Value.ToString()));
                    arr.Add(new cArrayList("@qty_pcs", hdqty_pcs.Value.ToString()));
                    arr.Add(new cArrayList("@price_ctn", hdprice_ctn.Value.ToString()));
                    arr.Add(new cArrayList("@price_pcs", hdprice_pcs.Value.ToString()));
                    arr.Add(new cArrayList("@subtotal", lbsubtotal.Text.ToString()));
                    arr.Add(new cArrayList("@total_invoice", lbtot_inv.Text.ToString()));
                    arr.Add(new cArrayList("@total_payment", lbtot_payment.Text.ToString()));
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));

                    bll.vInsTmpSalesOrderXls(arr);
                }
            }
            // Check Again before export to transaction (duplicate data)
            string checkData = "ok";
            checkData = bll.vLookUp("exec sp_check_salesorder_import '"+ Request.Cookies["sp"].Value.ToString() + "','"+ DateTime.ParseExact(dtbranch.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture) + "','"+ Request.Cookies["usr_id"].Value.ToString() + "'");
            if (checkData != "ok")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Available transaction duplicate in data Excel !!!','"+checkData+"','warning');", true);
                return;
            }


            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@transaction_dt", DateTime.ParseExact(dtbranch.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vImportTmpSalesOrderXls(arr);
            arr.Clear();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Transation has been imported!','successfully','success');", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_upload_transaction");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        
    }

}