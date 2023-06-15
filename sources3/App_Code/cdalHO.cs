using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// Summary description for cdalHO
/// </summary>
public class cdalHO
{
    public string vLookUpHO(string sSQL)
    {
        string sTemp = "";
        SqlDataReader rsx = null;
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = cd.getConnectionHO();
        cmd.CommandText = sSQL;
        cmd.CommandType = CommandType.Text;
        rsx = cmd.ExecuteReader();
        while (rsx.Read())
        {
            sTemp = rsx[0].ToString();
        }
        rsx.Close();
        return (sTemp);
    }
    public void vGetRecordsetSPHO(string sSPName, ref SqlDataReader rs, List<cArrayList> arr)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = cd.getConnectionHO();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = sSPName;
        cmd.CommandTimeout = 0;
        vPrepareParameter(ref cmd, arr);
        rs = cmd.ExecuteReader();
    }
    protected void vPrepareParameter(ref SqlCommand cmd, List<cArrayList> arr)
    {
        for (int i = 0; i < arr.Count; i++)
        {
            cmd.Parameters.AddWithValue(arr[i].paramid, arr[i].ParamValue);
        }
    }
    public void vGetRecordsetSPHO(string sSPName, ref SqlDataReader rs)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = cd.getConnectionHO();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = sSPName;
        cmd.CommandTimeout = 0;
        rs = cmd.ExecuteReader();
    }
    public void vGetRecordsetSP(string sSPName, ref SqlDataAdapter rs, List<cArrayList> arr)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = cd.getConnectionHO();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = sSPName;
        cmd.CommandTimeout = 0;
        vPrepareParameter(ref cmd, arr);
        rs = new SqlDataAdapter(cmd);
    }
    public void vExecuteStructuredSPHO(SqlParameter param)
    {
        SqlConnection conn = new SqlConnection();
        try
        {
            SqlCommand sqlCmd = new SqlCommand("sp_inst_ErrorLogDetails");
            //conn.Open();
            sqlCmd.Connection = cd.getConnectionHO();
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.Add(param);

            sqlCmd.ExecuteNonQuery();
            conn.Close();


        }
        catch (Exception ex)
        {
            conn.Close();
            //conn.Close();
        }
    }
    public void vExecuteSQLHO(string sSQL)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = sSQL;
        cmd.Connection = cd.getConnectionHO();
        cmd.ExecuteNonQuery();
    }
    public void vGetRecordSqlHO(string sSQL, ref SqlDataReader rs)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = cd.getConnectionHO();
        cmd.CommandText = sSQL;
        cmd.CommandType = CommandType.Text;
        //vPrepareParameter(ref cmd, arr);
        cmd.CommandTimeout = 0;
        rs = cmd.ExecuteReader();
    }
    public void vExecuteSPHO(string sSPName, List<cArrayList> arr)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = cd.getConnectionHO();
        cmd.CommandText = sSPName;
        cmd.CommandType = CommandType.StoredProcedure;
        vPrepareParameter(ref cmd, arr);
        cmd.CommandTimeout = 0;
        cmd.ExecuteNonQuery();
    }
    public void vExecuteSPHO(string sSPName)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = cd.getConnectionHO();
        cmd.CommandText = sSPName;
        cmd.CommandTimeout = 0;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();
    }
    public void vExecuteSPHO(string sSPName, List<cArrayList> arr, string paramout, ref string sOutValue)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = cd.getConnectionHO();
        cmd.CommandText = sSPName;
        cmd.CommandTimeout = 0;
        cmd.CommandType = CommandType.StoredProcedure;
        SqlParameter oParam = new SqlParameter();
        oParam.Direction = ParameterDirection.Output;
        oParam.ParameterName = paramout;
        oParam.Value = sOutValue;
        oParam.SqlDbType = SqlDbType.VarChar;
        oParam.Size = 50;
        cmd.Parameters.Add(oParam);
        vPrepareParameter(ref cmd, arr);
        cmd.ExecuteNonQuery();
        sOutValue = oParam.Value.ToString();
    }
    //public void vExecuteSPHO(string sSPName, List<cArrayList> arr)
    //{
    //    SqlCommand cmd = new SqlCommand();
    //    cmd.Connection = cd.getConnectionHO();
    //    cmd.CommandText = sSPName;
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    vPrepareParameter(ref cmd, arr);
    //    cmd.CommandTimeout = 0;
    //    cmd.ExecuteNonQuery();
    //}
    //public void vExecuteSPHO(string sSPName, List<cArrayList> arr, string paramout, ref string sOutValue)
    //{
    //    SqlCommand cmd = new SqlCommand();
    //    cmd.Connection = cd.getConnectionHO();
    //    cmd.CommandText = sSPName;
    //    cmd.CommandTimeout = 0;
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    SqlParameter oParam = new SqlParameter();
    //    oParam.Direction = ParameterDirection.Output;
    //    oParam.ParameterName = paramout;
    //    oParam.Value = sOutValue;
    //    oParam.SqlDbType = SqlDbType.VarChar;
    //    oParam.Size = 50;
    //    cmd.Parameters.Add(oParam);
    //    vPrepareParameter(ref cmd, arr);
    //    cmd.ExecuteNonQuery();
    //    sOutValue = oParam.Value.ToString();
    //}
   
}