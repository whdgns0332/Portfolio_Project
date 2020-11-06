using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFactoryProject_Final.Common
{
    class DBConnect
    {
        public static string GetConString()
        {
            IniFile ini = new IniFile();
            ini.Load(IniData.SettingIniFile);
            IniSection dbSection = ini["DBConnect"];
            return $"Data Source={dbSection["DatabaseIP"]};" +
                   $"Initial Catalog={dbSection["DatabaseName"]};" +
                   $"Persist Security Info={dbSection["Security"]};" +
                   $"User ID={dbSection["DatabaseID"]};" +
                   $"Password={dbSection["DatabasePW"]}";
        }

        /// <summary>
        /// 주어진 쿼리문을 실행해 그 결과를 DataSet으로 리턴하는 함수
        /// </summary>
        /// <param name="commandText">입력 쿼리문</param>
        /// <param name="dataset">출력 DataSet</param>
        /// <returns>통신이 정상적으로 이루어졌는지의 여부</returns>
        public bool Search(string commandText, out DataSet dataset)
        {
            SqlCommand command = new SqlCommand(commandText);
            return Search(command, out dataset);
        }

        /// <summary>
        /// SqlCommand를 실행해 그 결과를 DataSet으로 리턴하는 함수
        /// </summary>
        /// <param name="command">입력 SqlCommand</param>
        /// <param name="dataset">출력 DataSet</param>
        /// <returns>통신이 정상적으로 이루어졌는지의 여부</returns>
        public bool Search(SqlCommand command, out DataSet dataset)
        {
            SqlConnection connection = null;
            DataSet result = null;
            SqlDataAdapter adapter = null;

            try
            {
                using (connection = new SqlConnection(GetConString()))
                {
                    command.Connection = connection;
                    command.Connection.Open();
                    command.CommandTimeout = 1000;

                    adapter = new SqlDataAdapter(command);
                    result = new DataSet();
                    adapter.Fill(result, "TB_result");

                    command.Connection.Close();
                }

                if (result.Tables[0].Rows.Count > 0)
                    dataset = result;
                else
                    dataset = null;
            }
            catch (Exception excep)
            {
                string className = nameof(DBConnect);
                string funcName = nameof(Search);
                string logText = string.Concat(excep.Message.ToString(), Environment.NewLine,
                                               command.Connection.ConnectionString, Environment.NewLine,
                                               command.CommandText);
                Log.WriteLog(Log.LogType.Error, className, funcName, logText);
                dataset = null;
                return false;
            }
            finally
            {
                if (connection != null)
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                    connection.Dispose();
                }
                if (result != null)
                    result.Dispose();
                if (adapter != null)
                    adapter.Dispose();
            }
            return true;
        }

        /// <summary>
        /// SqlParameter를 지원하는 저장 프로시저 실행 함수
        /// DB 접근 중에 예외가 발생했다면 null을 리턴한다
        /// </summary>
        /// <param name="Proc">실행할 저장 프로시저의 명칭</param>
        /// <param name="ParamType">사용할 매개변수들의 타입. 매개변수가 없다면 null로 할것</param>
        /// <param name="ParamVal">사용할 매개변수들의 값. 매개변수가 없다면 null로 할것</param>
        /// <returns></returns>
        public DataSet ExecuteProcedure(string Proc, SqlDbType[] ParamType = null, object[] ParamVal = null)
        {
            string connectString = GetConString();
            SqlConnection connection = new SqlConnection(connectString);
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter();
            string commandText = "";

            DataSet result = new DataSet();

            try
            {
                command = GetSqlCommandForProd(Proc, ParamType, ParamVal);
                command.Connection = connection;

                adapter.SelectCommand = command;
                adapter.SelectCommand.CommandTimeout = 1000;

                adapter.Fill(result, "List");
                if (result.Tables.Count > 1)
                {
                    for (int k = 0; k < result.Tables.Count; k++)
                    {
                        result.Tables[k].TableName = "LIST_" + (k + 1).ToString();
                    }
                }

                return result;
            }
            catch (Exception excep)
            {
                // Log 기록
                string className = nameof(DBConnect);
                string funcName = nameof(ExecuteProcedure);
                string logText = string.Concat(excep.Message.ToString(), Environment.NewLine,
                                               connectString, Environment.NewLine,
                                               commandText);
                Log.WriteLog(Log.LogType.Error, className, funcName, logText);
                return null;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
                connection.Dispose();
                command.Dispose();
                adapter.Dispose();
                result.Dispose();
            }
        }

        public SqlCommand GetSqlCommandForProd(string Proc, SqlDbType[] ParamType = null, object[] ParamVal = null)
        {
            SqlCommand command = new SqlCommand();
            string commandText = "";

            try
            {
                commandText = "EXEC " + Proc;

                if (ParamType != null && ParamType.Length == ParamVal.Length)
                {
                    for (int i = 0; i < ParamType.Length; i++)
                    {
                        if (ParamVal[i] != null)
                        {
                            commandText += (i > 0 ? ", " : " ") + $"@PARAM{i}";

                            SqlParameter param = new SqlParameter($"@PARAM{i}", ParamType[i]);
                            param.Value = ParamVal[i];
                            command.Parameters.Add(param);
                        }
                        else
                        {
                            commandText += (i > 0 ? ", " : " ") + "NULL";
                        }
                    }
                }

                command.CommandText = commandText;

                return command;
            }
            catch (Exception excep)
            {
                string className = nameof(DBConnect);
                string funcName = nameof(GetSqlCommandForProd);
                string logText = string.Concat(excep.Message.ToString());
                Log.WriteLog(Log.LogType.Error, className, funcName, logText);
                return null;
            }
        }

        public void TryTransaction(SqlCommand[] commands)
        {
            SqlConnection connection = null;
            SqlTransaction trans = null;

            try
            {
                connection = new SqlConnection(GetConString());
                connection.Open();
                trans = connection.BeginTransaction();

                foreach (SqlCommand command in commands)
                {
                    if (command != null)
                    {
                        command.Connection = connection;
                        command.Transaction = trans;
                        command.ExecuteNonQuery();
                    }
                }

                trans.Commit();
                return;
            }
            catch (Exception excep)
            {
                if(trans != null)
                    trans.Rollback();

                string className = nameof(DBConnect);
                string funcName = nameof(TryTransaction);
                string logText = string.Concat(excep.Message.ToString());
                Log.WriteLog(Log.LogType.Error, className, funcName, logText);
                return;
            }
            finally
            {
                if (connection != null)
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                    connection.Dispose();
                }

                if (trans != null)
                {
                    trans.Dispose();
                }

                for (int i = 0; i < commands.Length; i++)
                {
                    commands[i].Dispose();
                }
            }
        }
    }
}
