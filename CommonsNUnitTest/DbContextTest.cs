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
using Commons.DbMigrationContext;
using System.Text;

namespace CommonsNUnitTest
{
    public class DbContextTest
    {
        PlayDbContext dbContext = new PlayDbContext();
        MigDbContext migDbContext = new MigDbContext();

        [Test]
        public void test() 
        {
            var test = dbContext.Students;
            var select = test.Where(x => x.Age == 20 );
        }

        [Test]
        public void test2()
        {
            var sts = dbContext.Students.ToList();
            var jsonStudents = JsonConvert.SerializeObject(sts);
            var jArray = JArray.Parse(jsonStudents);

            this.GetModelToInsertQuery(sts);

            //foreach (var st in sts) 
            //{
                
            //}

            //var select = test.Where(x => x.Age == 20);
        }

        //[Test]
        //public void test3()
        //{
        //    DateTime a = 1;
            
        //    var at = a.GetType().FullName;
        //}

        public List<string> GetModelToInsertQuery<T>(List<T> objList) 
        {
            List<string> queryList = new List<string>();
            foreach (var item in objList)
            {
                var props = item.GetType().GetProperties();

                //ÉwÉbÉ_çÏê¨
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(string.Format(" INSERT INTO {0}.dbo.{1}" ,"TEST","TEST"));
                sb.AppendLine("( ");
                int headIndex = 1;
                foreach (var prop in props) 
                {
                    if (headIndex == 1)
                    {
                        sb.AppendLine(prop.Name);
                    }
                    else
                    {
                        sb.AppendLine("," + prop.Name);
                    }
                    
                    headIndex++;
                }
                sb.AppendLine(") ");

                //Valueïîï™çÏê¨
                sb.AppendLine(" VALUE ");
                sb.AppendLine("( ");
                int bodyIndex = 1;
                foreach (var prop in props)
                {
                    var propTypeName = prop.PropertyType.FullName;
                    if (bodyIndex == 1) 
                    {
                        sb.AppendLine(prop.GetValue(item).ToString());
                    }
                    else 
                    {
                        sb.AppendLine("," + prop.GetValue(item).ToString());
                    }
                    bodyIndex++;
                }
                sb.AppendLine(") ");
                queryList.Add(sb.ToString());
            }
            return queryList;
        }
    }
}