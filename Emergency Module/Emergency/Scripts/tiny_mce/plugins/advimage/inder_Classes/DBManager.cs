using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Emergency;
using GS.Common;

public class DBManager
{
    /// <summary>
    /// The Db Manager
    /// common uses of SqlClient
    /// </summary>

    public static string _connectionString = Common.GetConnectionString();

    public static SqlParameter[] GetParametersSetFromSp(string spName)
    {

        SqlConnection cn = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand(spName, cn);
        SqlParameter[] spParameters = null;

        try
        {
            cn.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            SqlCommandBuilder.DeriveParameters(cmd);
            cmd.Parameters.RemoveAt(0);
            spParameters = new SqlParameter[cmd.Parameters.Count];
            cmd.Parameters.CopyTo(spParameters, 0);
            cmd.Parameters.Clear();
        }
        finally
        {
            cmd.Dispose();
            cn.Close();
            cn.Dispose();
        }

        return spParameters;

    } //GetParametersSetFromSp

    public static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
    {

        command.Parameters.Clear();

        //INSTANT C# NOTE: Commented this declaration since looping variables in 'foreach' loops are declared in the 'foreach' header in C#
        //			SqlParameter p = null;
        foreach (SqlParameter p in commandParameters)
        {
            command.Parameters.Add(p);
        }

    } //AttachParameters

    public static void AssignParameterValues(SqlParameter[] commandParameters, object[] parameterValues)
    {

        int i = 0;
        int j = 0;

        if ((commandParameters == null) & (parameterValues == null))
        {
            return;
        }

        j = commandParameters.Length - 1;
        for (i = 0; i <= j; i++)
        {
            if (i <= parameterValues.GetUpperBound(0))
            {
                commandParameters[i].Value = parameterValues[i];
            }
            else
            {
                commandParameters[i].Value = null;
            }
        }

    } //AssignParameterValues

    public static void PrepareCommand(SqlCommand command, SqlConnection connection, CommandType commandType, string commandText, SqlParameter[] commandParameters)
    {

        //if the provided connection is not open, we will open it
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        command.Connection = connection;

        command.CommandText = commandText;

        command.CommandType = commandType;

        if (commandParameters != null)
        {
            AttachParameters(command, commandParameters);
        }

        return;
    } //PrepareCommand

    public static void DisposeComponent(ref SqlConnection cn)
    {
        if (cn.State == ConnectionState.Open)
        {
            cn.Close();
        }

        cn.Dispose();
    }

    public static int ExecuteNonQuery(string spName, params object[] parameterValues)
    {

        SqlParameter[] commandParameters = null;

        if (parameterValues != null & parameterValues.Length > 0)
        {

            commandParameters = GetParametersSetFromSp(spName);

            AssignParameterValues(commandParameters, parameterValues);

            return ExecuteNonQuery(CommandType.StoredProcedure, spName, commandParameters);
        }
        else
        {
            return ExecuteNonQuery(CommandType.StoredProcedure, spName);
        }
    } //ExecuteNonQuery

    public static int ExecuteNonQuery(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        //create & open a SqlConnection, and dispose of it after we are done.
        SqlConnection cn = new SqlConnection(_connectionString);
        try
        {
            cn.Open();
            return ExecuteNonQuery(cn, commandType, commandText, commandParameters);
        }
        finally
        {
            cn.Close();
            cn.Dispose();
        }
    } //ExecuteNonQuery

    public static int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        SqlCommand cmd = new SqlCommand();
        int retval = 0;

        PrepareCommand(cmd, connection, commandType, commandText, commandParameters);

        retval = cmd.ExecuteNonQuery();

        cmd.Parameters.Clear();

        return retval;

    } //ExecuteNonQuery

    public static object ExecuteScalar(string spName, params object[] parameterValues)
    {
        SqlParameter[] commandParameters = null;

        if (parameterValues != null & parameterValues.Length > 0)
        {

            commandParameters = GetParametersSetFromSp(spName);

            AssignParameterValues(commandParameters, parameterValues);

            return ExecuteScalar(CommandType.StoredProcedure, spName, commandParameters);
        }
        else
        {
            return ExecuteScalar(CommandType.StoredProcedure, spName);
        }
    } //ExecuteScalar

    public static object ExecuteScalar(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        SqlConnection cn = new SqlConnection(_connectionString);
        try
        {
            cn.Open();
            return ExecuteScalar(cn, commandType, commandText, commandParameters);
        }
        finally
        {
            cn.Close();
            cn.Dispose();
        }
    } //ExecuteScalar

    public static object ExecuteScalar(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        SqlCommand cmd = new SqlCommand();
        object retval = null;

        PrepareCommand(cmd, connection, commandType, commandText, commandParameters);

        retval = cmd.ExecuteScalar();

        cmd.Parameters.Clear();

        return retval;

    } //ExecuteScalar

    public static DataSet ExecuteDataset(string spName, params object[] parameterValues)
    {

        SqlParameter[] commandParameters = null;

        if (parameterValues != null & parameterValues.Length > 0)
        {

            commandParameters = GetParametersSetFromSp(spName);

            AssignParameterValues(commandParameters, parameterValues);

            return ExecuteDataset(CommandType.StoredProcedure, spName, commandParameters);
        }
        else
        {
            return ExecuteDataset(CommandType.StoredProcedure, spName);
        }
    } //ExecuteDataset

    public static DataSet ExecuteDataset(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        SqlConnection cn = new SqlConnection(_connectionString);
        try
        {
            cn.Open();
            return ExecuteDataset(cn, commandType, commandText, commandParameters);
        }
        finally
        {
            cn.Close();
            cn.Dispose();
        }
    } //ExecuteDataset

    public static DataSet ExecuteDataset(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        SqlCommand cmd = new SqlCommand();
        DataSet ds = new DataSet();
        SqlDataAdapter da = null;

        PrepareCommand(cmd, connection, commandType, commandText, commandParameters);

        da = new SqlDataAdapter(cmd);

        da.Fill(ds);

        cmd.Parameters.Clear();

        return ds;

    } //ExecuteDataset
    public void xyz()
    {
    }
    public static SqlDataReader ExecuteReader(string spName, params object[] parameterValues)
    {
        SqlParameter[] commandParameters = null;

        if (parameterValues != null & parameterValues.Length > 0)
        {

            commandParameters = GetParametersSetFromSp(spName);

            AssignParameterValues(commandParameters, parameterValues);

            return ExecuteReader(CommandType.StoredProcedure, spName, commandParameters);
        }
        else
        {
            return ExecuteReader(CommandType.StoredProcedure, spName);
        }
    } //ExecuteReader

    public static SqlDataReader ExecuteReader(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        SqlConnection cn = new SqlConnection(_connectionString);

        cn.Open();
        return ExecuteReader(cn, commandType, commandText, commandParameters);


    } //ExecuteReader

    public static SqlDataReader ExecuteReader(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        SqlCommand cmd = new SqlCommand();
        SqlDataReader retval = null;

        PrepareCommand(cmd, connection, commandType, commandText, commandParameters);

        retval = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        cmd.Parameters.Clear();

        return retval;

    } //ExecuteReader

    public static SqlDataAdapter ExecuteAdapter(string spName, ref SqlConnection cn, params object[] parameterValues)
    {
        SqlParameter[] commandParameters = null;

        if (parameterValues != null & parameterValues.Length > 0)
        {

            commandParameters = GetParametersSetFromSp(spName);

            AssignParameterValues(commandParameters, parameterValues);

            return ExecuteAdapter(ref cn, CommandType.StoredProcedure, spName, commandParameters);
        }
        else
        {
            return ExecuteAdapter(ref cn, CommandType.StoredProcedure, spName);
        }
    } //ExecuteAdapter

    public static SqlDataAdapter ExecuteAdapter(ref SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {

        if (connection == null)
        {
            connection = new SqlConnection(_connectionString);
            connection.Open();
        }

        SqlCommand cmd = new SqlCommand();
        DataSet ds = new DataSet();
        SqlDataAdapter da = null;

        PrepareCommand(cmd, connection, commandType, commandText, commandParameters);

        da = new SqlDataAdapter(cmd);

        return da;

    } //ExecuteAdapter
}
