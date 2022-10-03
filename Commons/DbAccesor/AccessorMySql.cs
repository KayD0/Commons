using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using MySql.Data.MySqlClient;
using Commons;
using Commons.DbAccessor;
using Commons.DbAccessor.Parameters;

namespace Common.DbAccessor
{
    public class AccessorMySql : IDisposable
    {
        /// <summary>
        /// 接続情報
        /// </summary>
        private readonly MySqlConnection conn;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AccessorMySql()
        {
            string ConnectionStr = ConfigurationManager.ConnectionStrings["NPWEBDataSource"].ConnectionString;
            this.conn = new MySqlConnection(ConnectionStr);
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
        public  void Close()
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
            using (MySqlCommand command = new MySqlCommand(packageName, this.conn))
            {
                try
                {
                    if (command.Connection.State != ConnectionState.Open)
                    {
                        command.Connection.Open();
                    }

                    using (MySqlTransaction transaction = command.Connection.BeginTransaction(IsolationLevel.ReadCommitted))
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
                catch (MySqlException e)
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
            using (MySqlCommand command = new MySqlCommand(commandSql, this.conn))
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
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        MySqlCommandBuilder builder = new MySqlCommandBuilder(adapter);
                        adapter.Fill(ds);
                    }
                    return ds;
                }
                catch (MySqlException e)
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
            using (MySqlCommand command = new MySqlCommand(commandSql, this.conn))
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
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        MySqlCommandBuilder builder = new MySqlCommandBuilder(adapter);
                        adapter.Fill(dt);
                    }
                    return dt;
                }
                catch (MySqlException e)
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
            using (MySqlCommand command = new MySqlCommand(commandSql, this.conn))
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

                    using (MySqlTransaction transaction = command.Connection.BeginTransaction(IsolationLevel.ReadCommitted))
                    {
                        command.ExecuteNonQuery();
                        transaction.Commit();
                    }

                }
                catch (MySqlException e)
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
        public object ExecuteQueryForScalar(string commandSql, Dictionary<string, object> dbParams)
        {
            using (MySqlCommand command = new MySqlCommand(commandSql, this.conn))
            {
                //接続
                try
                {
                    object obj = new object();
                    if (command.Connection.State != ConnectionState.Open)
                    {
                        command.Connection.Open();
                    }

                    command.CommandType = CommandType.Text;
                    foreach (var param in dbParams)
                    {
                        command.Parameters.AddWithValue(param.Key, param.Value);
                    }

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        MySqlCommandBuilder builder = new MySqlCommandBuilder(adapter);
                        obj = command.ExecuteScalar();
                    }
                    return obj;
                }
                catch (MySqlException e)
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
