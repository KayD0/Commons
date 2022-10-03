using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using Commons;
using Commons.DbAccessor;
using Commons.DbAccessor.Parameters;
using Npgsql;
using MySql.Data.MySqlClient;

namespace Commons.DbAccessor
{
    public class AccessorNpgsql : IDisposable
    {
        /// <summary>
        /// O接続情報
        /// </summary>
        private readonly NpgsqlConnection conn;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AccessorNpgsql()
        {
            string ConnectionStr = ConfigurationManager.ConnectionStrings["NPWEBDataSource"].ConnectionString;
            this.conn = new NpgsqlConnection(ConnectionStr);
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
            using (NpgsqlCommand command = new NpgsqlCommand(packageName, this.conn))
            {
                try
                {
                    if (command.Connection.State != ConnectionState.Open)
                    {
                        command.Connection.Open();
                    }

                    using (NpgsqlTransaction transaction = command.Connection.BeginTransaction(IsolationLevel.ReadCommitted))
                    {
                        command.Transaction = transaction;
                        command.CommandType = CommandType.StoredProcedure;
                        foreach (var param in dbParams)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }

                        command.ExecuteNonQuery();
                        transaction.Commit();
                    }

                }
                catch (NpgsqlException e)
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
            using (NpgsqlCommand command = new NpgsqlCommand(commandSql, this.conn))
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

                    DataSet ds = new DataSet();
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
                    {
                        NpgsqlCommandBuilder builder = new NpgsqlCommandBuilder(adapter);
                        adapter.Fill(ds);
                    }
                    return ds;
                }
                catch (NpgsqlException e)
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
            using (NpgsqlCommand command = new NpgsqlCommand(commandSql, this.conn))
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
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
                    {
                        NpgsqlCommandBuilder builder = new NpgsqlCommandBuilder(adapter);
                        adapter.Fill(dt);
                    }
                    return dt;
                }
                catch (NpgsqlException e)
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
            using (NpgsqlCommand command = new NpgsqlCommand(commandSql, this.conn))
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

                    using (NpgsqlTransaction transaction = command.Connection.BeginTransaction(IsolationLevel.ReadCommitted))
                    {
                        command.ExecuteNonQuery();
                        transaction.Commit();
                    }

                }
                catch (NpgsqlException e)
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
            using (NpgsqlCommand command = new NpgsqlCommand(commandSql, this.conn))
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

                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
                    {
                        NpgsqlCommandBuilder builder = new NpgsqlCommandBuilder(adapter);
                        obj = command.ExecuteScalar();
                    }
                    return obj;
                }
                catch (NpgsqlException e)
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
