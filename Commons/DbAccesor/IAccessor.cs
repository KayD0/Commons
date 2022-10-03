//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Oracle.ManagedDataAccess.Client;
//using System.Configuration;
//using Commons.DbAccessor.Parameters;

//namespace Commons.DbAccessor
//{
//    public abstract class IAccessor
//    {
//        /// <summary>
//        /// 切断とインスタンスの破棄
//        /// </summary>
//        public abstract void Close();

//        /// <summary>
//        /// パッケージの実行
//        /// </summary>
//        /// <param name="packageName"></param>
//        /// <param name="comCd"></param>
//        /// <param name="facCd"></param>
//        /// <param name="jornalNo"></param>
//        public abstract bool ExecuteProcedure(string packageName, ref List<DbParamerter> paramList);


        
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="datatable"></param>
//        /// <param name="commandSql"></param>
//        /// <param name="paramList"></param>
//        /// <returns></returns>
//        public abstract bool ExecuteQuery(ref DataTable datatable, string commandSql, List<DbParamerter> paramList);

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="obj"></param>
//        /// <param name="commandSql"></param>
//        /// <param name="paramList"></param>
//        /// <returns></returns>
//        public abstract bool ExecuteQueryForScalar(ref object obj, string commandSql, List<DbParamerter> paramList);
//    }
//}
