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
using Commons.DbAccesorContext;

namespace CommonsNUnitTest
{
    public class DbContextTest
    {
        PlayDbContext dbContext = new PlayDbContext();

        [Test]
        public void test() 
        {
            var test = dbContext.Student;
            var select = test.Where(x => x.Age == 20 );
        }
    }
}