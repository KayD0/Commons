using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Commons.DataUtil.UtilJsonModel;
using CsvHelper;
using CsvHelper.Configuration;
//using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Commons.DataUtil
{

    public class UtilCsv : UtilStream
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="fileName"></param>
        public UtilCsv() 
        {
            
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="writeHeader"></param>
        public void ConvertDtToCsvFile(DataTable dt, bool writeHeader, string filePath)
        {
            using (var textWriter = File.CreateText(filePath))
            using (var csv = new CsvWriter(textWriter, new CultureInfo("ja-JP", false)))
            {
                // Write columns
                if (writeHeader) 
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        csv.WriteField(column.ColumnName);
                    }
                    csv.NextRecord();
                }
                // Write row values
                foreach (DataRow row in dt.Rows)
                {
                    for (var i = 0; i < dt.Columns.Count; i++)
                    {
                        csv.WriteField(row[i]);
                    }
                    csv.NextRecord();
                }

                var test = csv.ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="writeHeader"></param>
        public string ConvertDtToCsvData(DataTable dt, bool writeHeader)
        {

            int colCount = dt.Columns.Count;
            int lastColIndex = colCount - 1;
            var sb = new StringBuilder();

            //ヘッダを書き込む
            if (writeHeader)
            {
                for (int i = 0; i < colCount; i++)
                {
                    //ヘッダの取得
                    string field = dt.Columns[i].Caption;
                    //"で囲む
                    field = EncloseDoubleQuotesIfNeed(field);
                    //フィールドを書き込む
                    sb.Append($@"""{field}"",");
                    //カンマを書き込む
                }
                //改行する
                sb.Remove(sb.Length - 1, 1).ToString();
                sb.AppendLine("");
            }

            //レコードを書き込む
            foreach (DataRow row in dt.Rows)
            {
                for (int i = 0; i < colCount; i++)
                {
                    //フィールドの取得
                    string field = row[i].ToString();
                    //"で囲む
                    field = EncloseDoubleQuotesIfNeed(field);
                    //フィールドを書き込む
                    sb.Append($@"""{field}"",");
                }
                //改行する
                //改行する
                sb.Remove(sb.Length - 1, 1).ToString();
                sb.AppendLine("");
            }

            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="writeHeader"></param>
        public void ConvertObjToCsvFile<T>(List<T> obj, bool writeHeader, string filePath)
        {
            using (FileStream fs = File.Create(filePath))
            {
                fs.Close();
                fs.Dispose();
            }
            using (var streamWriter = new StreamWriter(filePath))
            using (var csv = new CsvWriter(streamWriter, new CultureInfo("ja-JP", false)))
            {
                //ヘッダー
                if (writeHeader) 
                {
                    csv.WriteHeader<T>();
                    csv.NextRecord();
                }
                //データ部
                foreach (var t in obj)
                {
                    csv.WriteRecord(t);
                    csv.NextRecord();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="writeHeader"></param>
        public List<T> ConvertCsvFileToCsv<T>(string filePath)
        {
            using (var reader = new StreamReader(filePath, Encoding.GetEncoding(this.Encode)))
            using (var csv = new CsvReader(reader, new CultureInfo("ja-JP", false)))
            {
                //csv.Configuration.HasHeaderRecord = false;
                //csv.Configuration.Delimiter = ",";
                //csv.Configuration.IgnoreBlankLines = true;

                return csv.GetRecords<T>().ToList();
            }
        }


        /// <summary>
        /// 必要ならば、文字列をダブルクォートで囲む
        /// </summary>
        private string EncloseDoubleQuotesIfNeed(string field)
        {
            if (NeedEncloseDoubleQuotes(field))
            {
                return EncloseDoubleQuotes(field);
            }
            return field;
        }

        /// <summary>
        /// 文字列をダブルクォートで囲む
        /// </summary>
        private string EncloseDoubleQuotes(string field)
        {
            if (field.IndexOf('"') > -1)
            {
                //"を""とする
                field = field.Replace("\"", "\"\"");
            }
            return "\"" + field + "\"";
        }

        /// <summary>
        /// 文字列をダブルクォートで囲む必要があるか調べる
        /// </summary>
        private bool NeedEncloseDoubleQuotes(string field)
        {
            return field.IndexOf('"') > -1 ||
                field.IndexOf(',') > -1 ||
                field.IndexOf('\r') > -1 ||
                field.IndexOf('\n') > -1 ||
                field.StartsWith(" ") ||
                field.StartsWith("\t") ||
                field.EndsWith(" ") ||
                field.EndsWith("\t");
        }

        public void Main()
        {
            
        }
    }

    
}
