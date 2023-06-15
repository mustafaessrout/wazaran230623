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

public partial class fm_acc_ap_ho_print : System.Web.UI.Page
{
    cbll bll = new cbll();

    //SqlConnection conn = new SqlConnection(ConfigurationManager
    //.ConnectionStrings["SBTCDBConnectionString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            String strSalespoint = Request.QueryString["strSalespoint"];
            String strDtfrom = Request.QueryString["strDtfrom"];
            String strDtto = Request.QueryString["strDtto"];

            printapho(strSalespoint, strDtfrom, strDtto);

        }


        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
    }
    //protected void printapho(string strSalespoint, string strDtfrom, string strDtto)
    public void printapho(string strSalespoint, string strDtfrom, string strDtto)
    {
        if (strDtfrom == "" | strDtto == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Both Start Date and End Date cannot be empty!','','warning');", true);
        }
        else
        {
            DataTable dt = GetSPResult(strSalespoint, strDtfrom, strDtto);
            //ReportViewer1.Visible = true;
            //ReportViewer1.LocalReport.ReportPath = "rp_acc_ap_ho.rdlc";
            //ReportViewer1.LocalReport.DataSources.Clear();
            //ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet_acc_ap_ho", dt));
        }

    }
    public DataTable GetSPResult(string strSalespoint, string strDtfrom, string strDtto)
    {
        DataTable ResultsTable = new DataTable();

        try
        {
            ////DateTime strDtfromP = DateTime.ParseExact(strDtfrom.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            ////DateTime strDttoP = DateTime.ParseExact(strDtto.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            ////string strDtfromStr = strDtfromP.Year.ToString() + "-" + strDtfromP.Month.ToString("00") + "-" + strDtfromP.Day.ToString("00");
            ////string strDttoStr = strDttoP.Year.ToString() + "-" + strDttoP.Month.ToString("00") + "-" + strDttoP.Day.ToString("00");
            //string strDtfromMonth;
            //string strDtfromDay;
            //string strDttoMonth;
            //string strDttoDay;
            //string strDtfromStr;
            //string strDttoStr;


            //if (strDtfrom.ToString().Length == 10)
            //{
            //    //strDtfromStr = strDtfrom.ToString().Substring(7, 4) + '-' + strDtfrom.ToString().Substring(4, 2) + '-' + strDtfrom.ToString().Substring(1, 2);
            //    strDtfromStr = strDtfrom.ToString().Substring(6, 4) + '-' + strDtfrom.ToString().Substring(3, 2) + '-' + strDtfrom.ToString().Substring(0, 2);
            //}
            //else if (strDtfrom.ToString().Length == 8)
            //{
            //    //strDtfromStr = strDtfrom.ToString().Substring(5, 4) + '-' + strDtfrom.ToString().Substring(3, 1) + '-' + strDtfrom.ToString().Substring(1, 1);
            //    strDtfromStr = strDtfrom.ToString().Substring(4, 4) + '-' + '0' + strDtfrom.ToString().Substring(2, 1) + '-' + '0' + strDtfrom.ToString().Substring(0, 1);
            //}
            //else
            //{
            //    //if (strDtfrom.ToString().Substring(3, 1) == "/" && strDtfrom.ToString().Substring(5, 1) == "/")
            //    if (strDtfrom.ToString().Substring(2, 1) == "/" && strDtfrom.ToString().Substring(4, 1) == "/")
            //    {
            //        //strDtfromMonth = "0" + strDtfrom.ToString().Substring(4, 1);
            //        strDtfromMonth = "0" + strDtfrom.ToString().Substring(3, 1);
            //        //strDtfromDay = strDtfrom.ToString().Substring(1, 2);
            //        strDtfromDay = strDtfrom.ToString().Substring(0, 2);
            //    }
            //    else //if (strDtfrom.ToString().Substring(2, 1) == "/" && strDtfrom.ToString().Substring(5, 1) == "/")
            //    {
            //        //strDtfromMonth = strDtfrom.ToString().Substring(3, 2);
            //        strDtfromMonth = strDtfrom.ToString().Substring(2, 2);
            //        //strDtfromDay = "0" + strDtfrom.ToString().Substring(1, 1);
            //        strDtfromDay = "0" + strDtfrom.ToString().Substring(0, 1);
            //    }

            //    strDtfromStr = strDtfrom.ToString().Substring(strDtfrom.ToString().Length - 4, 4) + "-" + strDtfromMonth + "-" + strDtfromDay + " 00:00:00.000";
            //}


            ////-----------
            //if (strDtto.ToString().Length == 10)
            //{
            //    //strDttoStr = strDtto.ToString().Substring(7, 4) + '-' + strDtto.ToString().Substring(4, 2) + '-' + strDtto.ToString().Substring(1, 2);
            //    strDttoStr = strDtto.ToString().Substring(6, 4) + '-' + strDtto.ToString().Substring(3, 2) + '-' + strDtto.ToString().Substring(0, 2);
            //}
            //else if (strDtto.ToString().Length == 8)
            //{
            //    //strDttoStr = strDtto.ToString().Substring(5, 4) + '-' + strDtto.ToString().Substring(3, 1) + '-' + strDtto.ToString().Substring(1, 1);
            //    strDttoStr = strDtto.ToString().Substring(4, 4) + '-' + '0' + strDtto.ToString().Substring(2, 1) + '-' + '0' + strDtto.ToString().Substring(0, 1);
            //}
            //else
            //{
            //    //if (strDtto.ToString().Substring(3, 1) == "/" && strDtto.ToString().Substring(5, 1) == "/")
            //    if (strDtto.ToString().Substring(2, 1) == "/" && strDtto.ToString().Substring(4, 1) == "/")
            //    {
            //        //strDttoMonth = "0" + strDtto.ToString().Substring(4, 1);
            //        strDttoMonth = "0" + strDtto.ToString().Substring(3, 1);
            //        //strDttoDay = strDtto.ToString().Substring(1, 2);
            //        strDttoDay = strDtto.ToString().Substring(0, 2);
            //    }
            //    else //if (strDtto.ToString().Substring(2, 1) == "/" && strDtto.ToString().Substring(5, 1) == "/")
            //    {
            //        //strDttoMonth = strDtto.ToString().Substring(3, 2);
            //        strDttoMonth = strDtto.ToString().Substring(2, 2);
            //        //strDttoDay = "0" + strDtto.ToString().Substring(1, 1);
            //        strDttoDay = "0" + strDtto.ToString().Substring(0, 1);
            //    }

            //    strDttoStr = strDtto.ToString().Substring(strDtto.ToString().Length - 4, 4) + "-" + strDttoMonth + "-" + strDttoDay + " 00:00:00.000";
            //}
            ////-----------

            ////if (strDtfromP.Year.ToString() != strDttoP.Year.ToString())
            //if (strDtfrom.ToString().Substring(strDtfrom.ToString().Length - 4, 4) != strDtto.ToString().Substring(strDtto.ToString().Length - 4, 4))
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please change both Start Date and End Date to be in the same year!','','warning');", true);
            //}
            //else
            //{
            //    if (conn == null)
            //    {
            //        conn.Open();
            //    }

            //    SqlCommand cmd = new SqlCommand("sp_ap_ho_get",conn);
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.AddWithValue("@salespoint_cd", strSalespoint);
            //    cmd.Parameters.AddWithValue("@start_dt", strDtfromStr);
            //    //cmd.Parameters.AddWithValue("@start_dt", DateTime.ParseExact(strDtfromStr, "yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture));
            //    cmd.Parameters.AddWithValue("@end_dt", strDttoStr);
            //    //cmd.Parameters.AddWithValue("@end_dt", DateTime.ParseExact(strDttoStr, "yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture));
            //    cmd.Parameters.AddWithValue("@user", bll.vLookUp("select fullname from tuser_profile where emp_cd='" + Request.Cookies["usr_id"].Value.ToString() + "'"));
            //    string salesPointName = bll.sGetSalespointname(strSalespoint);
            //    cmd.Parameters.AddWithValue("@salesPointName", salesPointName);
            //    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //    adapter.SelectCommand = cmd;
            //    adapter.Fill(ResultsTable);

            //    if (conn != null)
            //    {
            //        conn.Close();
            //    }
            //}
        }

        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
        return ResultsTable;
    }
}