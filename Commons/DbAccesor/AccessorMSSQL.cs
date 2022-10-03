using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using Commons;
using Commons.DbAccessor;
using Commons.DbAccessor.Parameters;
using Commons.DataUtil;
using Newtonsoft.Json.Linq;

namespace Commons.DbAccessor
{
    public class AccessorMssql : IDisposable
    {
        /// <summary>
        /// 接続情報
        /// </summary>
        private readonly SqlConnection conn;
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AccessorMssql()
        {
            UtilJson sj = new UtilJson();
            JObject jo = sj.ConvertFileToJObject(@"C:\99_playground\CommonsNUnitTest\appsettings.json");
            string ConnectionStr = jo["Data"]["PlayConnectionString"].ToString();
            this.conn = new SqlConnection(ConnectionStr);
        }

        /// <summary>
        /// クラスプロパティの破棄
        /// </summary>
        public void Dispose()
        {
            this.conn.Close();
            ((IDisposable)this.conn).Dispose();
        }

        /// <summary>
        /// 切断とインスタンスの破棄
        /// </summary>
        public void Close()
        {
            this.conn.Close();
            this.conn.Dispose();
        }

        /// <summary>
        /// プロシージャの実行処理
        /// </summary>
        /// <param name="packageName"></param>
        /// <param name="dbParams"></param>
        public void ExecuteProcedure(string packageName, Dictionary<string, object> dbParams)
        {
            using (SqlCommand command = new SqlCommand(packageName, this.conn))
            {
                try
                {
                    if (command.Connection.State != ConnectionState.Open)
                    {
                        command.Connection.Open();
                    }

                    using (SqlTransaction transaction = command.Connection.BeginTransaction(IsolationLevel.ReadCommitted))
                    {
                        //引数設定
                        command.Transaction = transaction;
                        command.CommandType = CommandType.StoredProcedure;
                        foreach (var param in dbParams)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }

                        //ストアドを実行する
                        command.ExecuteNonQuery();
                        transaction.Commit();
                    }

                }
                catch (SqlException e)
                {
                    throw;
                }
                finally 
                {
                    command.Connection.Close();
                }
            }
        }

        /// <summary>
        /// SQL実行処理
        /// </summary>
        /// <param name="commandSql"></param>
        /// <param name="dbParams"></param>
        /// <returns></returns>
        public DataSet ExecuteQueryDs(string commandSql, Dictionary<string, object> dbParams)
        {
            using (SqlCommand command = new SqlCommand(commandSql, this.conn))
            {
                try
                {
                    if (command.Connection.State != ConnectionState.Open)
                    {
                        command.Connection.Open();
                    }

                    command.CommandType = CommandType.Text;
                    foreach (var param in dbParams)
                    {
                        command.Parameters.AddWithValue(param.Key, param.Value);
                    }

                    //コマンド実行
                    DataSet ds = new DataSet();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                        adapter.Fill(ds);
                    }
                    return ds;
                }
                catch (SqlException e)
                {
                    throw;
                }
                finally
                {
                    command.Connection.Close();
                }
            }
        }

        /// <summary>
        /// SQL実行処理
        /// </summary>
        /// <param name="commandSql"></param>
        /// <param name="dbParams"></param>
        /// <returns></returns>
        public DataTable ExecuteQueryDt(string commandSql, Dictionary<string, object> dbParams)
        {
            using (SqlCommand command = new SqlCommand(commandSql, this.conn))
            {
                try
                {
                    if (command.Connection.State != ConnectionState.Open)
                    {
                        command.Connection.Open();
                    }

                    command.CommandType = CommandType.Text;
                    foreach (var param in dbParams)
                    {
                        command.Parameters.AddWithValue(param.Key, param.Value);
                    }

                    DataTable dt = new DataTable();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                        adapter.Fill(dt);
                    }
                    return dt;
                }
                catch (SqlException e)
                {
                    throw;
                }
                finally
                {
                    command.Connection.Close();
                }
            }
        }

        /// <summary>
        /// SQL実行処理
        /// </summary>
        /// <param name="commandSql"></param>
        /// <param name="dbParams"></param>
        public void ExecuteQueryStored(string commandSql, Dictionary<string, object> dbParams)
        {
            using (SqlCommand command = new SqlCommand(commandSql, this.conn))
            {
                try
                {
                    if (command.Connection.State != ConnectionState.Open)
                    {
                        command.Connection.Open();
                    }

                    command.CommandType = CommandType.Text;
                    foreach (var param in dbParams)
                    {
                        command.Parameters.AddWithValue(param.Key, param.Value);
                    }

                    using (SqlTransaction transaction = command.Connection.BeginTransaction(IsolationLevel.ReadCommitted))
                    {
                        command.ExecuteNonQuery();
                        transaction.Commit();
                    }

                }
                catch (SqlException e)
                {
                    throw;
                }
                finally
                {
                    command.Connection.Close();
                }
            }
        }

        /// <summary>
        /// SQL実行処理
        /// </summary>
        /// <param name="commandSql"></param>
        /// <param name="dbParams"></param>
        /// <returns></returns>
        public object ExecuteQueryForScalar(string commandSql, Dictionary<string,object> dbParams)
        {
            using (SqlCommand command = new SqlCommand(commandSql, this.conn))
            {
                //接続
                try
                {
                    object obj = new object();
                    if (command.Connection.State != ConnectionState.Open) {
                        command.Connection.Open();
                    }
                    
                    command.CommandType = CommandType.Text;
                    foreach (var param in dbParams)
                    {
                        command.Parameters.AddWithValue(param.Key,param.Value);
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                        obj = command.ExecuteScalar();
                    }
                    return obj;
                }
                catch (SqlException e)
                {
                    throw;
                }
                finally
                {
                    command.Connection.Close();
                }
            }
        }
    }
}
