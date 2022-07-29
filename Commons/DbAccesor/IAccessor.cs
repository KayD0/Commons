using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using Commons.DbAccessor.Parameters;

namespace Commons.DbAccessor
{
    public abstract class IAccessor
    {
        /// <summary>
        /// 切断とインスタンスの破棄
        /// </summary>
        public abstract void Close();

        /// <summary>
        /// パッケージの実行
        /// </summary>
        /// <param name="packageName"></param>
        /// <param name="comCd"></param>
        /// <param name="facCd"></param>
        /// <param name="jornalNo"></param>
        public abstract bool ExecuteProcedure(string packageName, ref List<DbParamerter> paramList);


        /// <summary>
        /// プリントワークテーブル取得処理(クエリカスタマイズ可能)
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="slitOutputNo"></param>
        /// <param name="dataset"></param>
        /// <param name="commandSql"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        public abstract bool ExecuteQuery(string tableName, ref DataSet dataset, string commandSql, List<DbParamerter> paramList);
    }
}
