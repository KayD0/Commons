using System;
using System.Data;
using System.Collections.Generic;
using Commons.DataUtil;
using Commons.FileReader.Model;
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
using Commons.DbAccesor.Services;

namespace CommonsNUnitTest
{
    public class ServiceTest
    {

        ServiceMssql sMssql = new ServiceMssql();

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void InsertPdfImage()
        {
            //データベース書き込み
            //string p_fn = @"C:\work\RM内定通知書畑迫様.pdf";
            //byte[] imageByteArray = File.ReadAllBytes(p_fn);
            //sMssql.InsertImage(2, imageByteArray, "RM内定通知書畑迫様.pdf");

            //データベース読み取り
            byte[] readImage = sMssql.ReadImage(2);

            //ファイル書き込み
            File.WriteAllBytes(@"C:\work\RM内定通知書畑迫様2.pdf", readImage);
        }

    }
}   