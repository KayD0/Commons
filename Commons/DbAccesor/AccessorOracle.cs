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
using Oracle;

namespace Commons.DbAccessor
{
    public class AccessorOracle : IDisposable
    {
        /// <summary>
        /// O接続情報
        /// </summary>
        private readonly OracleConnection conn;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AccessorOracle()
        {
            string ConnectionStr = ConfigurationManager.ConnectionStrings["NPWEBDataSource"].ConnectionString;
            this.conn = new OracleConnection(ConnectionStr);
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
            using (OracleCommand command = new OracleCommand(packageName, this.conn))
            {
                try
                {
                    if (command.Connection.State != ConnectionState.Open)
                    {
                        command.Connection.Open();
                    }

                    using (OracleTransaction transaction = command.Connection.BeginTransaction(IsolationLevel.ReadCommitted))
                    {
                        command.Transaction = transaction;
                        command.CommandType = CommandType.StoredProcedure;
                        foreach (var param in dbParams)
                        {
                            command.Parameters.Add(param.Key, param.Value);
                        }

                        command.ExecuteNonQuery();
                        transaction.Commit();
                    }

                }
                catch (OracleException e)
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
            using (OracleCommand command = new OracleCommand(commandSql, this.conn))
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
                        command.Parameters.Add(param.Key, param.Value);
                    }

                    DataSet ds = new DataSet();
                    using (OracleDataAdapter adapter = new OracleDataAdapter(command))
                    {
                        OracleCommandBuilder builder = new OracleCommandBuilder(adapter);
                        adapter.Fill(ds);
                    }
                    return ds;
                }
                catch (OracleException e)
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
            using (OracleCommand command = new OracleCommand(commandSql, this.conn))
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
                        command.Parameters.Add(param.Key, param.Value);
                    }

                    DataTable dt = new DataTable();
                    using (OracleDataAdapter adapter = new OracleDataAdapter(command))
                    {
                        OracleCommandBuilder builder = new OracleCommandBuilder(adapter);
                        adapter.Fill(dt);
                    }
                    return dt;
                }
                catch (OracleException e)
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
            using (OracleCommand command = new OracleCommand(commandSql, this.conn))
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
                        command.Parameters.Add(param.Key, param.Value);
                    }

                    using (OracleTransaction transaction = command.Connection.BeginTransaction(IsolationLevel.ReadCommitted))
                    {
                        command.ExecuteNonQuery();
                        transaction.Commit();
                    }

                }
                catch (OracleException e)
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
            using (OracleCommand command = new OracleCommand(commandSql, this.conn))
            {
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
                        command.Parameters.Add(param.Key, param.Value);
                    }

                    using (OracleDataAdapter adapter = new OracleDataAdapter(command))
                    {
                        OracleCommandBuilder builder = new OracleCommandBuilder(adapter);
                        obj = command.ExecuteScalar();
                    }
                    return obj;
                }
                catch (OracleException e)
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
