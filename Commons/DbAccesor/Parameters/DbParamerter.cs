using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using Npgsql;

namespace Commons.DbAccessor.Parameters
{
    public class DbParamerter
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string Val = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public int Size;

        /// <summary>
        /// 
        /// </summary>
        public ParameterDirection Direction;

        /// <summary>
        /// 
        /// </summary>
        public SqlDbType DbTypeMssql;

        /// <summary>
        /// 
        /// </summary>
        public MySqlDbType DbTypeMysql;

        /// <summary>
        /// 
        /// </summary>
        public OracleDbType DbTypeOracle;

      /// <summary>
      /// 
      /// </summary>
      public NpgsqlTypes.NpgsqlDbType DbTypeNpgsql;
   }
}
