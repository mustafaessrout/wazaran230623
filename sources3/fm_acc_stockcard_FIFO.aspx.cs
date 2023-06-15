using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;

public partial class fm_acc_stockcard_FIFO : System.Web.UI.Page
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


            bll.vBindingComboToSp(ref cbItemCode, "sp_tmst_item_FIFO_get", "item_cd", "item_desc");
            ListItemCollection liCol2 = cbItemCode.Items;
            for (int i = 0; i < liCol2.Count; i++)
            {
                ListItem li2 = liCol2[i];
                if (li2.Value == "0")
                {
                    cbItemCode.Items.Remove(li2);
                }

            }            
            //cbItemCode.Items.Insert(0, new ListItem("<< Please Select >>", "NA"));
            cbItemCode.Items.Insert(0, new ListItem("All Item", "All"));

            //SqlDataReader rdr = null;
            //SqlConnection con = null;
            //SqlCommand cmd = null;

            //try
            //{
            //    // Open connection to the database
            //    con = new SqlConnection(ConfigurationManager
            //    .ConnectionStrings["SBTCDBConnectionString"].ConnectionString);

            //    con.Open();

            //    //// Set up a command with the given query and associate
            //    //// this with the current connection.
            //    //string CommandText = "SELECT FirstName, LastName" +
            //    //                     "  FROM Employees" +
            //    //                     " WHERE (LastName LIKE @Find)";
            //    string CommandText = "select item_cd from tmst_item" +
            //                         "  where item_cd not in(select item_cd_ from tacc_stock_card_FIFO)";
            //    cmd = new SqlCommand(CommandText);
            //    cmd.Connection = con;

            //    //// Add LastName to the above defined paramter @Find
            //    //cmd.Parameters.Add(
            //    //    new SqlParameter(
            //    //    "@Find", // The name of the parameter to map
            //    //    System.Data.SqlDbType.VarChar, // SqlDbType values
            //    //    20, // The width of the parameter
            //    //    "LastName"));  // The name of the source column

            //    //// Fill the parameter with the value retrieved
            //    //// from the text field
            //    //cmd.Parameters["@Find"].Value = txtFind.Text;

            //    // Execute the query
            //    rdr = cmd.ExecuteReader();

            //    //// Fill the list box with the values retrieved
            //    //lbFound.Items.Clear();
            //    //while (rdr.Read())
            //    //{
            //    //    lbFound.Items.Add(rdr["FirstName"].ToString() +
            //    //    " " + rdr["LastName"].ToString());
            //    //}



            //    while (rdr.Read())
            //    {
            //        string itemToRmv = rdr["item_cd"].ToString();
            //        //cbItemCode.Items.Remove(cbItemCode.Items.FindByValue(itemToRmv));

            //        //ListItem removeItem = cbItemCode.Items.FindByValue(itemToRmv);
            //        //cbItemCode.Items.Remove(removeItem);

            //        //cbItemCode.Items.Remove(li2);

            //        //cbItemCode.Items
            //        //       .OfType<ListItem>()
            //        //       .Where(li => li.Value == itemToRmv)
            //        //       .ToList()
            //        //       .ForEach(li => cbItemCode.Items.Remove(li));

            //        //cbItemCode.Items.RemoveAt(i);

            //        cbItemCode.Items.Remove(cbItemCode.Items.FindByValue(itemToRmv));
            //    }
            //}
            //catch (Exception ex)
            //{
            //    //// Print error message
            //    //MessageBox.Show(ex.Message);
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('" + ex.Message + "');", true);
            //}
            //finally
            //{
            //    // Close data reader object and database connection
            //    if (rdr != null)
            //        rdr.Close();

            //    if (con.State == ConnectionState.Open)
            //        con.Close();
            //}

        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
    }
  
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

    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //[WebMethod]
    //public static string GetFullUrlData(string strSalespoint, string strItem, string strDtfrom, string strDtto)
    //{
    //    string Geturl = "http://" + GetUrlData() + ':' + "80" + '/' + "fm_acc_stockcard_FIFO.aspx" + '?' + "strSalespoint" + '=' + strSalespoint + '&' + "strItem" + '=' + strItem + '&' + "strDtfrom" + '=' + strDtfrom + '&' + "strDtto" + '=' + strDtto;
    //    return Geturl;
    //}

    protected void btPrintStockCardFIFO_Click(object sender, EventArgs e)
    {
        string strSalespoint; string strItem; string strDtfrom; string strDtto;

        strSalespoint = Request.Cookies["sp"].Value.ToString();
        strItem = cbItemCode.SelectedValue.ToString();
        strDtfrom = dtfrom.Text.ToString();
        strDtto = dtto.Text.ToString();

        PrintStockCardFIFO(strSalespoint, strItem, strDtfrom, strDtto);
    }


    public void PrintStockCardFIFO(string strSalespoint, string strItem, string strDtfrom, string strDtto)
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
            arr.Add(new cArrayList("@item_cd", strItem));
            arr.Add(new cArrayList("@start_dt", strDtfromStr));
            arr.Add(new cArrayList("@end_dt", strDttoStr));
            arr.Add(new cArrayList("@user", bll.vLookUp("select fullname from tuser_profile where emp_cd='" + Request.Cookies["usr_id"].Value.ToString() + "'")));
            arr.Add(new cArrayList("p_user", Request.Cookies["fullname"].Value.ToString()));
            string salesPointName = bll.sGetSalespointname(strSalespoint);
            arr.Add(new cArrayList("@salesPointName", salesPointName));


            if (strItem == "All")
            {
                Session["lParamstockcardfifoall"] = arr;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=acc_stockcardFIFOAll');", true);
            }
            else
            {
                Session["lParamstockcardfifo"] = arr;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=acc_stockcardFIFO');", true);
            }
        }
    }

}