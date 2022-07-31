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
    public class AccessorMySql : IAccessor, IDisposable
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
        public override void Close()
        {
            this.conn.Close();
            this.conn.Dispose();
        }

        /// <summary>
        /// プロシージャの実行処理
        /// </summary>
        /// <param name="packageName"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        public override bool ExecuteProcedure(string packageName,ref List<DbParamerter> paramList)
        {
            bool result = true;

            //コマンドを生成する
            using (MySqlCommand command = new MySqlCommand(packageName, this.conn))
            {
                //接続
                try
                {
                    LoggerBase.logger.Info("[ProcessId:{0}] [パッケージコマンド実行処理] 開始", LoggerBase.ProcessId);
                    LoggerBase.logger.Info("[ProcessId:{0}] [DB接続] 開始", LoggerBase.ProcessId);
                    command.Connection.Open();
                    LoggerBase.logger.Info("[ProcessId:{0}] [DB接続] 成功", LoggerBase.ProcessId);


                    using (MySqlTransaction transaction = command.Connection.BeginTransaction(IsolationLevel.ReadCommitted))
                    {
                        //引数設定
                        command.Transaction = transaction;
                        command.CommandType = CommandType.StoredProcedure;
                        foreach (DbParamerter para in paramList)
                        {
                            command.Parameters.Add(para.Name, para.DbTypeMysql, para.Size);
                        }

                        //ストアドを実行する
                        LoggerBase.logger.Info("[ProcessId:{0}] [コマンド実行] 開始", LoggerBase.ProcessId);
                        command.ExecuteNonQuery();
                        transaction.Commit();
                        LoggerBase.logger.Info("[ProcessId:{0}] [コマンド実行] 成功", LoggerBase.ProcessId);
                    }

                }
                catch (MySqlException e)
                {
                    LoggerBase.logger.Fatal("[ProcessId:{0}] [パッケージコマンド実行処理] 異常終了", LoggerBase.ProcessId);
                    LoggerBase.logger.Fatal(e.ToString());
                    result = false;
                }
                finally 
                {
                    command.Connection.Close();
                    LoggerBase.logger.Info("[ProcessId:{0}] [DB接続] 終了", LoggerBase.ProcessId);
                    LoggerBase.logger.Info("[ProcessId:{0}] [パッケージコマンド実行処理] 終了", LoggerBase.ProcessId);
                }
            }
            return result;
        }

        /// <summary>
        /// SQL実行処理
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dataset"></param>
        /// <param name="commandSql"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        public override bool ExecuteQuery(string tableName, ref DataSet dataset, string commandSql, List<DbParamerter> paramList)
        {
            bool result = true;
            using (MySqlCommand command = new MySqlCommand(commandSql, this.conn))
            {

                //接続
                try
                {
                    LoggerBase.logger.Info("[ProcessId:{0}] [SQLコマンド実行処理] 開始", LoggerBase.ProcessId);
                    LoggerBase.logger.Info("[ProcessId:{0}] [DB接続] 開始", LoggerBase.ProcessId);
                    command.Connection.Open();
                    LoggerBase.logger.Info("[ProcessId:{0}] [DB接続] 成功", LoggerBase.ProcessId);

                    //パラメタ,コマンドタイプ定義
                    command.CommandType = CommandType.Text;
                    foreach (DbParamerter para in paramList)
                    {
                        command.Parameters.Add(para.Name,para.DbTypeMysql,para.Size);
                    }

                    //コマンド実行
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        LoggerBase.logger.Info("[ProcessId:{0}] [コマンド実行] 開始", LoggerBase.ProcessId);
                        MySqlCommandBuilder builder = new MySqlCommandBuilder(adapter);
                        adapter.Fill(dataset, tableName);
                        LoggerBase.logger.Info("[ProcessId:{0}] [コマンド実行] 成功", LoggerBase.ProcessId);
                    }

                }
                catch (MySqlException e)
                {
                    LoggerBase.logger.Fatal("[ProcessId:{0}] [SQLコマンド実行処理] 異常終了", LoggerBase.ProcessId);
                    LoggerBase.logger.Fatal(e.ToString());
                    result = false;
                }
                finally
                {
                    command.Connection.Close();
                    LoggerBase.logger.Info("[ProcessId:{0}] [DB接続] 終了", LoggerBase.ProcessId);
                    LoggerBase.logger.Info("[ProcessId:{0}] [SQLコマンド実行処理] 終了", LoggerBase.ProcessId);

                }
            }
            return result;
        }

        /// <summary>
        /// SQL実行処理
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dataset"></param>
        /// <param name="commandSql"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        public override bool ExecuteQuery(ref DataTable datatable, string commandSql, List<DbParamerter> paramList)
        {
            bool result = true;
            using (MySqlCommand command = new MySqlCommand(commandSql, this.conn))
            {

                //接続
                try
                {
                    LoggerBase.logger.Info("[ProcessId:{0}] [SQLコマンド実行処理] 開始", LoggerBase.ProcessId);
                    LoggerBase.logger.Info("[ProcessId:{0}] [DB接続] 開始", LoggerBase.ProcessId);
                    command.Connection.Open();
                    LoggerBase.logger.Info("[ProcessId:{0}] [DB接続] 成功", LoggerBase.ProcessId);

                    //パラメタ,コマンドタイプ定義
                    command.CommandType = CommandType.Text;
                    foreach (DbParamerter para in paramList)
                    {
                        command.Parameters.Add(para.Name, para.DbTypeMysql, para.Size);
                    }

                    //コマンド実行
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        LoggerBase.logger.Info("[ProcessId:{0}] [コマンド実行] 開始", LoggerBase.ProcessId);
                        MySqlCommandBuilder builder = new MySqlCommandBuilder(adapter);
                        adapter.Fill(datatable);
                        LoggerBase.logger.Info("[ProcessId:{0}] [コマンド実行] 成功", LoggerBase.ProcessId);
                    }

                }
                catch (MySqlException e)
                {
                    LoggerBase.logger.Fatal("[ProcessId:{0}] [SQLコマンド実行処理] 異常終了", LoggerBase.ProcessId);
                    LoggerBase.logger.Fatal(e.ToString());
                    result = false;
                }
                finally
                {
                    command.Connection.Close();
                    LoggerBase.logger.Info("[ProcessId:{0}] [DB接続] 終了", LoggerBase.ProcessId);
                    LoggerBase.logger.Info("[ProcessId:{0}] [SQLコマンド実行処理] 終了", LoggerBase.ProcessId);

                }
            }
            return result;
        }

        /// <summary>
        /// SQL実行処理
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dataset"></param>
        /// <param name="commandSql"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        public override bool ExecuteQueryForScalar(ref object obj, string commandSql, List<DbParamerter> paramList)
        {
            bool result = true;
            using (MySqlCommand command = new MySqlCommand(commandSql, this.conn))
            {

                //接続
                try
                {
                    LoggerBase.logger.Info("[ProcessId:{0}] [SQLコマンド実行処理] 開始", LoggerBase.ProcessId);
                    LoggerBase.logger.Info("[ProcessId:{0}] [DB接続] 開始", LoggerBase.ProcessId);
                    command.Connection.Open();
                    LoggerBase.logger.Info("[ProcessId:{0}] [DB接続] 成功", LoggerBase.ProcessId);

                    //パラメタ,コマンドタイプ定義
                    command.CommandType = CommandType.Text;
                    foreach (DbParamerter para in paramList)
                    {
                        command.Parameters.Add(para.Name, para.DbTypeMysql, para.Size);
                    }

                    //コマンド実行
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        LoggerBase.logger.Info("[ProcessId:{0}] [コマンド実行] 開始", LoggerBase.ProcessId);
                        MySqlCommandBuilder builder = new MySqlCommandBuilder(adapter);
                        obj = command.ExecuteScalar();
                        LoggerBase.logger.Info("[ProcessId:{0}] [コマンド実行] 成功", LoggerBase.ProcessId);
                    }

                }
                catch (MySqlException e)
                {
                    LoggerBase.logger.Fatal("[ProcessId:{0}] [SQLコマンド実行処理] 異常終了", LoggerBase.ProcessId);
                    LoggerBase.logger.Fatal(e.ToString());
                    result = false;
                }
                finally
                {
                    command.Connection.Close();
                    LoggerBase.logger.Info("[ProcessId:{0}] [DB接続] 終了", LoggerBase.ProcessId);
                    LoggerBase.logger.Info("[ProcessId:{0}] [SQLコマンド実行処理] 終了", LoggerBase.ProcessId);

                }
            }
            return result;
        }
    }
}
