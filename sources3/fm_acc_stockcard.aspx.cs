using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class fm_acc_stockcard : System.Web.UI.Page
{
    cbll bll = new cbll();
    cdal cdl = new cdal();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_get", "salespointcd", "salespoint_desc");
            cbsalespoint.SelectedValue = Request.Cookies["sp"].Value.ToString();
            if (Request.Cookies["sp"].Value.ToString() == "0")
            {
                cbsalespoint.Enabled = true;
            }
            else
            {
                cbsalespoint.Enabled = false;
            }

            List<cArrayList> arr = new List<cArrayList>();
            DataTable dt = new DataTable();

            arr.Clear();
            if (dt.Rows.Count > 0)
            {
                dt = new DataTable();
            }
            //arr.Add(new cArrayList("@whs_cd", null));
            dt = cdl.GetValueFromSP("sp_tacc_warehouseOrVanBin_get", arr);
            ddlWhBin.DataSource = dt;
            ddlWhBin.DataValueField = "bin_cd";
            ddlWhBin.DataTextField = "bin_nm";
            ddlWhBin.DataBind();

            rdmoy.SelectedValue = "M";

        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        Boolean isPrint = true;
        
        getStockCardReportParameters(isPrint);
    }

    protected void ddlWhBin_SelectedIndexChanged(object sender, EventArgs e)
    {
        Boolean isPrint = false;

        getStockCardReportParameters(isPrint);
    }

    protected void getStockCardReportParameters(Boolean isPrint)
    {
        string whType = null;
        string whCode = null;
        string whBin = null;

        List<cArrayList> arr = new List<cArrayList>();

        //char[] delimiterChars = { '|', ':' };
        char[] delimiterChars = { '|' };

        //string[] words = cbWhBin.SelectedItem.Text.Split(delimiterChars);
        string[] words = ddlWhBin.SelectedValue.ToString().Split(delimiterChars);

        int i = 1;
        Dictionary<int, string> dictionary = new Dictionary<int, string>();
        foreach (string s in words)
        {
            dictionary.Add(i, s);
            i = i + 1;
        }
        if (dictionary.ContainsKey(1))
        {
            whType = dictionary[1].ToString().Substring(0, dictionary[1].ToString().Length - 1);
        }
        if (dictionary.ContainsKey(2))
        {
            whCode = dictionary[2].ToString().Substring(1, dictionary[2].ToString().Length - 2);
        }
        if (dictionary.ContainsKey(3))
        {
            whBin = dictionary[3].ToString().Substring(1, dictionary[3].ToString().Length - 1);
        }

        DataTable dt = new DataTable();
        arr.Clear();
        if (dt.Rows.Count > 0)
        {
            dt = new DataTable();
        }

        if (isPrint == true)
        {
            doPrint(whType, whCode, whBin, arr, dt);
        }
        else
        {
            changeDdlItemCode(whType, whCode, whBin, arr, dt);
        }

    }

    protected void changeDdlItemCode(string whType, string whCode, string whBin, List<cArrayList> arr, DataTable dt)
    {
        if (ddlWhBin.SelectedValue.ToString() == "All")
        {
            arr.Clear();
            arr.Add(new cArrayList("@sp", cbsalespoint.SelectedValue.ToString()));
            arr.Add(new cArrayList("@isAll", true));
            arr.Add(new cArrayList("@whs_type", ""));
            arr.Add(new cArrayList("@whs_cd", ""));
            arr.Add(new cArrayList("@bin_cd", ""));
            dt = cdl.GetValueFromSP("sp_tacc_warehouseOrVanItemCode_get", arr);
            ddlItemCode.DataSource = dt;
            ddlItemCode.DataValueField = "item_cd";
            ddlItemCode.DataTextField = "item_nm";
            ddlItemCode.DataBind();
        }
        else
        {
            arr.Clear();
            arr.Add(new cArrayList("@sp", cbsalespoint.SelectedValue.ToString()));
            arr.Add(new cArrayList("@isAll", false));
            arr.Add(new cArrayList("@whs_type", whType));
            arr.Add(new cArrayList("@whs_cd", whCode));
            arr.Add(new cArrayList("@bin_cd", whBin));
            dt = cdl.GetValueFromSP("sp_tacc_warehouseOrVanItemCode_get", arr);
            ddlItemCode.DataSource = dt;
            ddlItemCode.DataValueField = "item_cd";
            ddlItemCode.DataTextField = "item_nm";
            ddlItemCode.DataBind();
        }
    }

    protected void doPrint(string whType, string whCode, string whBin, List<cArrayList> arr, DataTable dt)
    {
        if (ddlItemCode.SelectedValue.ToString() == "All") 
        {
            arr.Clear();
            DateTime dtpayp1 = DateTime.ParseExact(asofdate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //DateTime dtpayp1 = DateTime.ParseExact(asofdate.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            //dtpayp1 = dtpayp1.AddDays(1);
            string dt1 = dtpayp1.Year.ToString() + "-" + dtpayp1.Month.ToString("00") + "-" + dtpayp1.Day.ToString("00");
            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
            arr.Add(new cArrayList("@itemcd", ddlItemCode.SelectedValue.ToString()));
            arr.Add(new cArrayList("@asofdate", dt1));
            arr.Add(new cArrayList("@whtype", ""));
            arr.Add(new cArrayList("@whs", ""));
            arr.Add(new cArrayList("@whbin", ""));
            //arr.Add(new cArrayList("@user", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@user", bll.vLookUp("select emp_nm from tmst_employee where emp_cd='" + Request.Cookies["usr_id"].Value.ToString() + "'")));
            arr.Add(new cArrayList("@moy", rdmoy.SelectedValue.ToString()));
            Session["lParamstockcardall"] = arr;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('/fm_report2.aspx?src=acc_stockcard_all');", true); 
        }
        else
        {
            arr.Clear();
            DateTime dtpayp1 = DateTime.ParseExact(asofdate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //dtpayp1 = dtpayp1.AddDays(1);
            string dt1 = dtpayp1.Year.ToString() + "-" + dtpayp1.Month.ToString("00") + "-" + dtpayp1.Day.ToString("00");
            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
            arr.Add(new cArrayList("@itemcd", ddlItemCode.SelectedValue.ToString()));
            arr.Add(new cArrayList("@asofdate", dt1));
            arr.Add(new cArrayList("@whtype", whType));
            arr.Add(new cArrayList("@whs", whCode));
            arr.Add(new cArrayList("@whbin", whBin));
            //arr.Add(new cArrayList("@user", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@user", bll.vLookUp("select emp_nm from tmst_employee where emp_cd='" + Request.Cookies["usr_id"].Value.ToString() + "'")));
            arr.Add(new cArrayList("@moy", rdmoy.SelectedValue.ToString()));
            Session["lParamstockcard"] = arr;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('/fm_report2.aspx?src=acc_stockcard');", true); 
        }
    }

}