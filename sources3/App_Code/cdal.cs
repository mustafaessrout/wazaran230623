using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


public class cdal
{
public DataTable GetValueFromSP(string sSPName)
    {
        SqlDataReader rs = null;
        vGetRecordsetSP(sSPName, ref rs);
        DataTable dta = new DataTable();
        dta.Load(rs);
        rs.Close();
        return dta;
    }

public void vExecuteStructuredSP(SqlParameter param)
    {
        SqlConnection conn = new SqlConnection();
        try
        {
            SqlCommand sqlCmd = new SqlCommand("sp_inst_ErrorLogDetails");
            //conn.Open();
            sqlCmd.Connection = cd.getConnection();
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
    
public void vExecuteSQL(string sSQL)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = sSQL;
        cmd.Connection = cd.getConnection();
        cmd.ExecuteNonQuery();
    }
    public void vGetRecordSql(string sSQL, ref SqlDataReader rs)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = cd.getConnection();
        cmd.CommandText = sSQL;
        cmd.CommandType = CommandType.Text;
        //vPrepareParameter(ref cmd, arr);
        cmd.CommandTimeout = 0;
        rs= cmd.ExecuteReader();
    }
    protected void vPrepareParameter(ref SqlCommand cmd, List<cArrayList> arr)
    {
        for (int i = 0; i < arr.Count; i++)
        {
            cmd.Parameters.AddWithValue(arr[i].paramid, arr[i].ParamValue);
        }
    }

    public void vExecuteSP(string sSPName, List<cArrayList> arr)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = cd.getConnection();
        cmd.CommandText = sSPName;
        cmd.CommandType = CommandType.StoredProcedure;
        vPrepareParameter(ref cmd, arr);
        cmd.CommandTimeout = 0;
        cmd.ExecuteNonQuery();
    }

    public void vExecuteSP(string sSPName)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = cd.getConnection();
        cmd.CommandText = sSPName;
        cmd.CommandTimeout = 0;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();
    }
    public void vExecuteSP(string sSPName, List<cArrayList> arr, string paramout, ref string sOutValue)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = cd.getConnection();
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
    public void vGetRecordsetSP(string sSPName, ref SqlDataReader rs, List<cArrayList> arr)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = cd.getConnection();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = sSPName;
        cmd.CommandTimeout = 0;
        vPrepareParameter(ref cmd, arr);
        rs = cmd.ExecuteReader();
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

    public void vGetRecordsetSP(string sSPName, ref SqlDataReader rs)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = cd.getConnection();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = sSPName;
        cmd.CommandTimeout = 0;
        rs = cmd.ExecuteReader();
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
    public DataTable GetValueFromSP(string sSPName, List<cArrayList> arr)
    {
        SqlDataReader rs = null;
        vGetRecordsetSP(sSPName, ref rs, arr);
        DataTable dta = new DataTable();
        dta.Load(rs);
        rs.Close();
        return dta;
    }


    public DataSet GetDataSetValueFromSP(string sSPName, List<cArrayList> arr)
    {
        SqlDataAdapter rs = null;
        vGetRecordsetSP2(sSPName, ref rs, arr);
        DataSet dta = new DataSet();
        rs.Fill(dta);
        rs.Dispose();
        return dta;
    }

    public void vGetRecordsetSP2(string sSPName, ref SqlDataAdapter rs, List<cArrayList> arr)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = cd.getConnection();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = sSPName;
        cmd.CommandTimeout = 0;
        vPrepareParameter(ref cmd, arr);
        rs = new SqlDataAdapter(cmd);
    }
}
