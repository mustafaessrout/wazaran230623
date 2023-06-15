using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;

public partial class fm_acc_stockprice : System.Web.UI.Page
{
    cbll bll = new cbll();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_getall", "salespointcd", "salespoint_desc");
            //cbsalespoint.Items.Remove(cbsalespoint.Items.FindByValue("0"));
            ListItemCollection liCol = cbsalespoint.Items;
            for (int i = 0; i < liCol.Count; i++)
            {
                ListItem li = liCol[i];
                if (li.Value == "0")
                    cbsalespoint.Items.Remove(li);
            }
            cbsalespoint.SelectedValue = Request.Cookies["sp"].Value.ToString();
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
    }
  

    protected void btPrintstockpricelist_Click(object sender, EventArgs e)
    {
        string strSalespoint; string strDt;

        strSalespoint = Request.Cookies["sp"].Value.ToString();
        strDt = dt.Text.ToString();

        Printstockpricelist(strSalespoint, strDt);
    }


    public void Printstockpricelist(string strSalespoint, string strDt)
    {
        List<cArrayList> arr = new List<cArrayList>();

        string strDtMonth;
        string strDtDay;
        string strDate;


        if (strDt.ToString().Length == 10)
        {
            strDate = strDt.ToString().Substring(6, 4) + '-' + strDt.ToString().Substring(3, 2) + '-' + strDt.ToString().Substring(0, 2);
        }
        else if (strDt.ToString().Length == 8)
        {
            strDate = strDt.ToString().Substring(4, 4) + '-' + '0' + strDt.ToString().Substring(2, 1) + '-' + '0' + strDt.ToString().Substring(0, 1);
        }
        else
        {
            if (strDt.ToString().Substring(2, 1) == "/" && strDt.ToString().Substring(4, 1) == "/")
            {
                strDtMonth = "0" + strDt.ToString().Substring(3, 1);
                strDtDay = strDt.ToString().Substring(0, 2);
            }
            else
            {
                strDtMonth = strDt.ToString().Substring(2, 2);
                strDtDay = "0" + strDt.ToString().Substring(0, 1);
            }

            strDate = strDt.ToString().Substring(strDt.ToString().Length - 4, 4) + "-" + strDtMonth + "-" + strDtDay + " 00:00:00.000";
        }

        arr.Clear();
        arr.Add(new cArrayList("@salespoint_cd", strSalespoint));
        arr.Add(new cArrayList("@paramDate", strDate));
        arr.Add(new cArrayList("p_user", Request.Cookies["fullname"].Value.ToString()));
        string salesPointName = bll.sGetSalespointname(strSalespoint);
        arr.Add(new cArrayList("@salesPointName", salesPointName));

        Session["lParamstockpricelist"] = arr;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=acc_stockpricelist');", true);
    }

}