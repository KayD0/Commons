using System;
using System.Data;
using System.Collections.Generic;
using Commons.DataUtil;
using Commons.DataUtil.UtilJsonModel;
using Commons.DataUtil.UtilXmlModel;
using NUnit.Framework;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Linq;
using Commons.DbAccessor;
using Commons.DbAccessor.Parameters;

namespace CommonsNUnitTest
{
    public class AccessorMSSQLTest
    {
        [Test]
        public void AccessorMssqlTest()
        {
            AccessorMssql accessor = new AccessorMssql();

            DataTable dt = new DataTable();
            List<DbParamerter> prams = new List<DbParamerter>();
            string command = "SELECT * FROM PlayGround.dbo.Student";
            accessor.ExecuteQuery(ref dt, command, prams);
            //var r = dt.Rows;

            var result = Newtonsoft.Json.JsonConvert.SerializeObject(dt);
            var test = "";

        }

    }
}