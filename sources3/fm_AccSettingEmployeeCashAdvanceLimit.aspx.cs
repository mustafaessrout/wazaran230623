using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using AjaxControlToolkit;
using System.IO;
using System.Web.Services;
using System.Data;
using System.Configuration;
using System.Web.Script.Services;

public partial class fm_AccSettingEmployeeCashAdvanceLimit : System.Web.UI.Page
{
    cbll bll = new cbll();
    creport rep = new creport();
    decimal totalDebit = 0;
    decimal totalCredit= 0;
    public static int PreviousIndex;
    Boolean hasApprovalRole = false;
    Boolean isEmptyGrid = false;
    string userid = null;
    //string userid2 = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        //string userid = Request.Cookies["usr_id"].Value.ToString();
        ////string userid = "2833";

        ////userid2 = bll.vLookUp("select * from tmst_employee where (level_cd between 5 and 8 or level_cd=10)and dept_cd='fi'and salespointcd='0' and emp_cd='" + userid + "'");
        ////if (bll.vLookUp("select * from tmst_employee where (level_cd between 5 and 8 or level_cd=10)and dept_cd='fi'and salespointcd='0' and emp_cd='" + userid + "'") != "")
        //if (bll.vLookUp("select e.emp_cd from tapprovalpattern a left join tmst_employee e on a.emp_cd=e.emp_cd where a.doc_typ='manualjournal'and a.emp_cd='" + userid + "'") != "")
        //{
        //    hasApprovalRole = true;
        //}

        //if (!IsPostBack)
        //{
        //    //bindinggrd(hasApprovalRole, isEmptyGrid); 
        //}

        //if (hasApprovalRole)
        //{
        //    //btaddfinreport.Visible = false;
        //}


        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
    }
    //public string constr = @"Server=.\SQLExpress;Integrated Security=true;AttachDbFilename=|DataDirectory|oc.mdf;User Instance=true;";
    public string constr = ConfigurationManager.
    ConnectionStrings["connstr"].ConnectionString;
    public SqlConnection conX;


    [WebMethod]
    public static string getEmployee()
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connstr"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "fetchemployeeData";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        con.Close();
        DataSet ds = new DataSet();
        da.Fill(ds);
        return Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[0]);

    }

    [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string getNational()
    {
        string returnvalue = "";
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connstr"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "fetchNationality";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        con.Close();
        DataSet ds = new DataSet();
        da.Fill(ds);
        returnvalue += "" + "" + "" + ":All;";
        //returnvalue = "0" + ":All;";
        for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
        {

            returnvalue += ds.Tables[0].Rows[i]["nationalId"].ToString().Trim() + ":" + ds.Tables[0].Rows[i]["natiionName"].ToString().Trim() + ";";
        }
        returnvalue = returnvalue.Remove(returnvalue.LastIndexOf(';'));


        return returnvalue;


    }

    [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string EditUpdateDel(object data)
    {
        bl businessL = new bl();
        string returnMessage = "";


        returnMessage = businessL.insUpdete(data);

        return returnMessage;


    }

    public class bl
    {

        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connstr"].ConnectionString);
        SqlCommand cmd = new SqlCommand();

        public string insUpdete(object param)
        {
            string returnvalue = "";
            try
            {
                Dictionary<string, object> editData = param as Dictionary<string, object>;

                cmd.CommandText = "insUpdDelete";
                cmd.CommandType = CommandType.StoredProcedure;
                if (editData["oper"].ToString() == "del")
                {
                    cmd.Parameters.AddWithValue("@empId", editData["employeeId"] == DBNull.Value ? 0 : editData["employeeId"]);
                    cmd.Parameters.AddWithValue("@Actiontype", editData["oper"].ToString());


                }
                else
                {
                    //cmd.Parameters.AddWithValue("@empId", editData["employeeId"] == DBNull.Value ? 0 : editData["employeeId"]);
                    if (editData["employeeId"] == null)
                    {
                        cmd.Parameters.AddWithValue("@empId", 0);
                    }
                    else if (editData["employeeId"].ToString() != "")
                    {
                        cmd.Parameters.AddWithValue("@empId", Convert.ToInt32(editData["employeeId"]));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@empId", 0);
                    }

                    cmd.Parameters.AddWithValue("@name", editData["name"].ToString());
                    cmd.Parameters.AddWithValue("@Email", editData["email"].ToString());
                    cmd.Parameters.AddWithValue("@pwd", editData["pwd"].ToString());
                    cmd.Parameters.AddWithValue("@phone", editData["phone"].ToString());
                    if (editData["nationalityId"].ToString() == "")
                    {
                        cmd.Parameters.AddWithValue("@nationality", 0);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@nationality", Convert.ToInt32(editData["nationalityId"]));
                    }
                    //cmd.Parameters.AddWithValue("@dateofbirth", editData["dateOfBirth"].ToString());
                    //string dateX = editData["dateOfBirth"].ToString().Substring(6, 4) + "-" + editData["dateOfBirth"].ToString().Substring(0, 2) + "-" + editData["dateOfBirth"].ToString().Substring(3, 2);
                    //string dateX = editData["dateOfBirth"].ToString().Substring(0, 2) + "/" + editData["dateOfBirth"].ToString().Substring(3, 2) + "/" + editData["dateOfBirth"].ToString().Substring(6, 4);
                    //string dateX = editData["dateOfBirth"].ToString().Substring(0, 2) + "/" + editData["dateOfBirth"].ToString().Substring(3, 2) + "/" + editData["dateOfBirth"].ToString().Substring(8, 2)+ " 12:00:00 AM";
                    //DateTime dateX2 = DateTime.Parse(dateX);
                    DateTime dateX2 = DateTime.Parse(editData["dateOfBirth"].ToString());
                    cmd.Parameters.AddWithValue("@dateofbirth", dateX2);
                    cmd.Parameters.AddWithValue("@Actiontype", editData["oper"].ToString());
                }
                cmd.Connection = con;
                con.Open();

                int m = cmd.ExecuteNonQuery();
                if (m > 0)
                {
                    if (editData["oper"].ToString() == "add")
                    {
                        returnvalue = "Record Added successfully";
                    }
                    else if (editData["oper"].ToString() == "edit")
                    {
                        returnvalue = "Record Updated successfully";

                    }

                    else if (editData["oper"].ToString() == "del")
                    {
                        returnvalue = "Record deleted successfully";

                    }
                    else
                    {

                        throw new Exception("there is problem");

                    }


                }


            }
            catch (Exception ex)
            {
                returnvalue = ex.Message.ToString();

            }
            finally
            {
                con.Close();
            }

            return returnvalue;
        }


    }

}

