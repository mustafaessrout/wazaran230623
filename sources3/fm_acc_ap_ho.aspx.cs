using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Web.Script.Services;

public partial class fm_acc_ap_ho : System.Web.UI.Page
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


    protected void btprintapho_Click(object sender, EventArgs e)
    {
        string strSalespoint; string strDtfrom; string strDtto;

        strSalespoint = Request.Cookies["sp"].Value.ToString();
        strDtfrom = dtfrom.Text.ToString();
        strDtto = dtto.Text.ToString();

        Printapho_Click(strSalespoint, strDtfrom, strDtto);
    }


    public void Printapho_Click(string strSalespoint, string strDtfrom, string strDtto)
    {
        List<cArrayList> arr = new List<cArrayList>();

        string strDtfromMonth;
        string strDtfromDay;
        string strDttoMonth;
        string strDttoDay;
        string strDtfromStr;
        string strDttoStr;


        if (strDtfrom.ToString().Length == 10)
        {
            strDtfromStr = strDtfrom.ToString().Substring(6, 4) + '-' + strDtfrom.ToString().Substring(3, 2) + '-' + strDtfrom.ToString().Substring(0, 2);
        }
        else if (strDtfrom.ToString().Length == 8)
        {
            strDtfromStr = strDtfrom.ToString().Substring(4, 4) + '-' + '0' + strDtfrom.ToString().Substring(2, 1) + '-' + '0' + strDtfrom.ToString().Substring(0, 1);
        }
        else
        {
            if (strDtfrom.ToString().Substring(2, 1) == "/" && strDtfrom.ToString().Substring(4, 1) == "/")
            {
                strDtfromMonth = "0" + strDtfrom.ToString().Substring(3, 1);
                strDtfromDay = strDtfrom.ToString().Substring(0, 2);
            }
            else
            {
                strDtfromMonth = strDtfrom.ToString().Substring(2, 2);
                strDtfromDay = "0" + strDtfrom.ToString().Substring(0, 1);
            }

            //strDtfromStr = strDtfrom.ToString().Substring(strDtfrom.ToString().Length - 4, 4) + "-" + strDtfromMonth + "-" + strDtfromDay + " 00:00:00.000";
            strDtfromStr = strDtfrom.ToString().Substring(strDtfrom.ToString().Length - 4, 4) + "-" + strDtfromMonth + "-" + strDtfromDay;
        }


        if (strDtto.ToString().Length == 10)
        {
            strDttoStr = strDtto.ToString().Substring(6, 4) + '-' + strDtto.ToString().Substring(3, 2) + '-' + strDtto.ToString().Substring(0, 2);
        }
        else if (strDtto.ToString().Length == 8)
        {
            strDttoStr = strDtto.ToString().Substring(4, 4) + '-' + '0' + strDtto.ToString().Substring(2, 1) + '-' + '0' + strDtto.ToString().Substring(0, 1);
        }
        else
        {
            if (strDtto.ToString().Substring(2, 1) == "/" && strDtto.ToString().Substring(4, 1) == "/")
            {
                strDttoMonth = "0" + strDtto.ToString().Substring(3, 1);
                strDttoDay = strDtto.ToString().Substring(0, 2);
            }
            else
            {
                strDttoMonth = strDtto.ToString().Substring(2, 2);
                strDttoDay = "0" + strDtto.ToString().Substring(0, 1);
            }

            //strDttoStr = strDtto.ToString().Substring(strDtto.ToString().Length - 4, 4) + "-" + strDttoMonth + "-" + strDttoDay + " 00:00:00.000";
            strDttoStr = strDtto.ToString().Substring(strDtto.ToString().Length - 4, 4) + "-" + strDttoMonth + "-" + strDttoDay;
        }

        if (strDtfrom.ToString().Substring(strDtfrom.ToString().Length - 4, 4) != strDtto.ToString().Substring(strDtto.ToString().Length - 4, 4))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please change both Start Date and End Date to be in the same year!','','warning');", true);
        }
        else
        {
            arr.Clear();
            arr.Add(new cArrayList("@salespoint_cd", strSalespoint));
            arr.Add(new cArrayList("@start_dt", strDtfromStr));
            arr.Add(new cArrayList("@end_dt", strDttoStr));
            arr.Add(new cArrayList("@user", bll.vLookUp("select fullname from tuser_profile where emp_cd='" + Request.Cookies["usr_id"].Value.ToString() + "'")));
            arr.Add(new cArrayList("p_user", Request.Cookies["fullname"].Value.ToString()));
            string salesPointName = bll.sGetSalespointname(strSalespoint);
            arr.Add(new cArrayList("@salesPointName", salesPointName));


            Session["lParamapho"] = arr;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=acc_ap_ho');", true);
        }
    }

    ////protected void btprintapho_Click(object sender, EventArgs e)
    ////{
    ////    if (dtfrom.Text == "" | dtto.Text == "")
    ////    {
    ////        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Both Start Date and End Date cannot be empty!','','warning');", true);
    ////    }
    ////    else
    ////    {
    ////        string strl = "localhost";
    ////        string strp = "9999";
    ////        string strn = "fm_acc_ap_ho_print.aspx";
    ////        string field1 = "strSalespoint";
    ////        string field2 = "strDtfrom";
    ////        string field3 = "strDtto";
    ////        string par1 = cbsalespoint.SelectedValue.ToString();
    ////        string par2 = dtfrom.Text.ToString();
    ////        string par3 = dtto.Text.ToString();

    ////        printreport.Attributes.Add("onclick", "Show('" + strl + "','" + strp + "','" + strn + "','" + field1 + "','" + field2 + "','" + field3 + "','" + par1 + "','" + par2 + "','" + par3 + "')");
    ////    }

    ////}
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //[WebMethod]
    //public static string GetUrlData()
    //{
    //    string result;

    //    SqlConnection con = new SqlConnection(ConfigurationManager
    //    .ConnectionStrings["SBTCDBConnectionString"].ConnectionString);
    //    SqlCommand SelectCommand = new SqlCommand("select top(1) sp.comp_cd from tcontrol_parameter p left join tmst_salespoint sp on p.parm_valu=sp.salespointcd where parm_nm='salespoint'", con);
    //    con.Open();
    //    result = (string)SelectCommand.ExecuteScalar().ToString();
    //    con.Close();

    //    return result;
    //}

    ////public class ClsUrl
    ////{
    ////    public string urlName;

    ////}

    ////static List<ClsUrl> lsUrl = new List<ClsUrl> { };

    ////[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    ////[WebMethod]
    ////public List<ClsUrl> GetUrlData()
    ////{
    ////    string query = "select top(1) sp.comp_cd from tcontrol_parameter p left join tmst_salespoint sp on p.parm_valu=sp.salespointcd where parm_nm='salespoint'";
    ////    SqlCommand cmd = new SqlCommand(query);
    ////    DataSet ds = GetConnection(cmd);
    ////    //DataTable dt = ds.Tables[0];
    ////    foreach (DataRow item in ds.Tables[0].Rows)
    ////    {
    ////        ClsUrl clsUrl = new ClsUrl();
    ////        clsUrl.urlName = item["comp_cd"].ToString();
    ////        lsUrl.Add(clsUrl);
    ////    }

    ////    return lsUrl;
    ////}
    ////private static DataSet GetConnection(SqlCommand cmd)
    ////{
    ////    using (SqlConnection con = new SqlConnection(ConfigurationManager
    ////    .ConnectionStrings["SBTCDBConnectionString"].ConnectionString))
    ////    {
    ////        using (SqlDataAdapter sda = new SqlDataAdapter())
    ////        {
    ////            cmd.Connection = con;
    ////            sda.SelectCommand = cmd;
    ////            using (DataSet ds = new DataSet())
    ////            {
    ////                sda.Fill(ds);
    ////                return ds;
    ////            }
    ////        }
    ////    }
    ////}
}