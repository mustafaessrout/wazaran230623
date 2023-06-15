using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_upload_transaction_bkp : System.Web.UI.Page
{
    cbll bll = new cbll();
    public string pfilename { set; get; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
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
            bll.vDelTmpSalesOrderXls(arr);
            arr.Clear();
            bindinggrd();
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

    protected void btn_upload_Click(object sender, EventArgs e)
    {
        if (!fut.HasFile)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('File excel must be selected','File Upload xls','warning');", true);
            return;
        }

        pfilename = "S_"+ Request.Cookies["sp"].Value.ToString() + "_"+ Request.Cookies["usr_id"].Value.ToString() + "_" + Convert.ToString(DateTime.Today.Day) + Convert.ToString(DateTime.Today.Month) + Convert.ToString(DateTime.Today.Year);
        fut.SaveAs(bll.vLookUp("select dbo.fn_getcontrolparameter('image_path')") + @"\temp\transaction\" + pfilename);

        //string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\wazaran\\images\\temp\\transaction\\" + pfilename + ";Extended Properties=Excel 12.0";
        string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\images\\temp\\transaction\\" + pfilename + ";Extended Properties=Excel 12.0";
        OleDbConnection oledbConn = new OleDbConnection(connString);
        oledbConn.Open();
        OleDbCommand cmd = new OleDbCommand();
        DataTable Sheets = new DataTable();
        Sheets = oledbConn.GetSchema("Tables");
        string firstSheets = Sheets.Rows[0][2].ToString();

        cmd = new OleDbCommand("SELECT Branch_Code,Salesman_Code,Customer_Code,Transaction_Date,Payment_Type,Source_Tax,Order_Type,Item_Code,Qty_CTN,Qty_PCS,Total_Invoice FROM["+firstSheets+"]", oledbConn);
        OleDbDataAdapter oleda = new OleDbDataAdapter();
        oleda.SelectCommand = cmd;
        DataSet ds = new DataSet();
        oleda.Fill(ds, "tmp_salesorder_xls");
        oledbConn.Close();
        oledbConn = null;
    }
}