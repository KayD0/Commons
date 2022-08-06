using Commons.DbAccessor;
using Commons.DbAccessor.Parameters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Commons.DbAccesor.Services
{
    public class ServiceMssql
    {
        private AccessorMssql mssql = new AccessorMssql();

        public JObject GetStudents()
        {
            DataTable dt = new DataTable();
            List<DbParamerter> prams = new List<DbParamerter>();

            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" SELECT ");
            sql.AppendLine("   * ");
            sql.AppendLine(" FROM ");
            sql.AppendLine("   PlayGround.dbo.Student ");
            mssql.ExecuteQuery(ref dt, sql.ToString(), prams);

            string tblJson = JsonConvert.SerializeObject(dt);
            return JObject.Parse(tblJson);
        }

        public JObject InsertStudents()
        {
            DataTable dt = new DataTable();
            List<DbParamerter> prams = new List<DbParamerter>();

            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" SELECT ");
            sql.AppendLine("   * ");
            sql.AppendLine(" FROM ");
            sql.AppendLine("   PlayGround.dbo.Student ");
            mssql.ExecuteQuery(ref dt, sql.ToString(), prams);

            string tblJson = JsonConvert.SerializeObject(dt);
            return JObject.Parse(tblJson);
        }

        public JObject UpdateStudents()
        {
            DataTable dt = new DataTable();
            List<DbParamerter> prams = new List<DbParamerter>();

            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" SELECT ");
            sql.AppendLine("   * ");
            sql.AppendLine(" FROM ");
            sql.AppendLine("   PlayGround.dbo.Student ");
            mssql.ExecuteQuery(ref dt, sql.ToString(), prams);

            string tblJson = JsonConvert.SerializeObject(dt);
            return JObject.Parse(tblJson);
        }
    }
}


