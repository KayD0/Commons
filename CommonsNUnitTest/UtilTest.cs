using System;
using System.Data;
using System.Collections.Generic;
using Commons.DataUtil;
using NUnit.Framework;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Commons.DataUtil.UtilXmlModel;
using System.Text;
using System.IO;
using Commons.DataUtil.UtilJsonModel;
using Commons.DbAccessor;
using Commons.DbAccessor.Parameters;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using AutoMapper;
using System.Net;
using HtmlAgilityPack;

namespace CommonsNUnitTest
{
    public class UtilTest
    {
        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void XmlTest_String_XmlObj_File_XmlObj()
        {
            //String_XmlObj
            UtilXml cXml = new UtilXml();
            string xml = "<User><Details><ID>1020</ID><Name>ABCD</Name><City>NY</City><Country>USA</Country><TestList Test1=\"1\" Test2=\"2\" Test3=\"3\" Test4=\"4\"/></Details><Details><ID>1020</ID><Name>ABCD</Name><City>NY</City><Country>USA</Country><TestList Test1=\"1\" Test2=\"2\" Test3=\"3\" Test4=\"4\"/></Details></User>";
            Users users = cXml.ConverStringToXmlObj<Users>(xml);

            //XmlObj_File
            cXml.ConvertObjToXmlFile(users, @"C:\work\users.xml");

            //File_XmlObj
            Users users2 = cXml.ConvertFileToXmlObj<Users>(@"C:\work\users.xml");

            //Obj_XmlStr
            string xmlStr = cXml.ConvertObjToXmlStr(users2);
        }

        [Test]
        public void StreamTest_String_XmlObj_File_XmlObj()
        {
            //String_File
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("test");
            sb.AppendLine("test");
            sb.AppendLine("test");
            UtilStream uStrem = new UtilStream();
            uStrem.ConvertStringToFile(sb.ToString(), @"C:\work\string.txt");

            //File_String
            var str = uStrem.ConvertFileToString(@"C:\work\string.txt");

            //String_Stream
            var stream = uStrem.ConvertStringToStream(str);

            //Stream_String
            StreamReader sr = new StreamReader(stream);
            var s = sr.ReadToEnd();

            //String_JsonStream
            var jstream = uStrem.ConvertStringToStream(str);
        }

        [Test]
        public void JsonTest_String_XmlObj_File_XmlObj()
        {
            //String_File
            Students students = new Students();
            students.StudentList = new List<Student>();
            Student st = new Student();
            st.Name = "test";
            st.Age = "30";
            st.Height = "162";
            students.StudentList.Add(st);
            students.StudentList.Add(st);
            students.StudentList.Add(st);

            //obj Å® json string
            UtilJson uJson = new UtilJson();
            var jsonStudents = JsonConvert.SerializeObject(students);

            //json string Å® obj
            var sts = uJson.ConvertJStringToObject<Students>(jsonStudents);

            string jsontest = "{\"StudentList\":[{\"name\":\"test\",\"age\":\"30\",\"height\":\"162\"}]}";
            var sts2 = uJson.ConvertJStringToObject<Students>(jsontest);
        }

        [Test]
        public void CSvTest()
        {
            
            AccessorMssql accessor = new AccessorMssql();
            DataTable dt = new DataTable();
            List<DbParamerter> prams = new List<DbParamerter>();
            string command = "SELECT * FROM PlayGround.dbo.Student";
            accessor.ExecuteQuery(ref dt, command, prams);

            //obj Å® json string
            UtilCsv uCsv = new UtilCsv();
            //uCsv.ConvertDtToCsvFile(dt,true, @"C:\work\students.csv");
            //var csvData = uCsv.ConvertDtToCsvData(dt, true);

            Students stList = new Students();
            stList.StudentList = new List<Student>();

            //List<Student> stList = new List<Student>();
            Student st = new Student();
            st.Name = "test";
            st.Age = "30";
            st.Height = "162";
            stList.StudentList.Add(st);
            stList.StudentList.Add(st);
            stList.StudentList.Add(st);

            var jsonStudents = JsonConvert.SerializeObject(stList);
            var jo = JObject.Parse(jsonStudents);

            uCsv.ConvertDtToCsvFile(dt, true, @"C:\work\students11.csv");
            List<Student> ss = uCsv.ConvertCsvFileToObject<Student>(@"C:\work\students.csv");
            uCsv.ConvertObjToCsvFile(ss, true, @"C:\work\students5.csv");
        }

        [Test]
        public void UtilMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Data1, DataDTO1>();
            });

            IMapper mapper = config.CreateMapper();
            var source = new Data1();

            source.ID = 1;
            source.Name = "test";
            source.Age = 29;

            var dest = mapper.Map<Data1, DataDTO1>(source);

        }

        [Test]
        public void UtilScrape()
        {
            WebClient wc = new WebClient();
            string htmlStr = wc.DownloadString("https://happyhotel.jp/search/address/pref/13");
            if (htmlStr != null)
            {
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(htmlStr);
                HtmlNodeCollection nodes = htmlDoc.DocumentNode.SelectNodes("/html/head/meta");
                foreach (HtmlNode node in nodes)
                {
                    foreach (HtmlAttribute attr in node.Attributes)
                    {
                        Console.WriteLine(attr.Name + " " +attr.Value);
                    }
                }
            }

        }
    }

    public class Data1 {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
    public class DataDTO1
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }

    
}