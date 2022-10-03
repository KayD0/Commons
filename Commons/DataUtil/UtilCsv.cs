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
        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="fileName"></param>
        public UtilCsv() 
        {
            
        }
        #endregion

        #region DataTable → CSV File
        /// <summary>
        /// DataTable → CSV File
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="writeHeader"></param>
        /// <param name="filePath"></param>
        public void ConvertDtToCsvFile(DataTable dt, bool writeHeader, string filePath)
        {
            try
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
            catch (Exception)
            {
                throw;
            }
            
        }
        #endregion

        #region DataTable → CSV Image
        /// <summary>
        /// DataTable → CSV Image
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="writeHeader"></param>
        /// <returns></returns>
        public byte[] ConvertDtToCsvImage(DataTable dt, bool writeHeader)
        {
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    using (var streamWriter = new StreamWriter(memoryStream))
                    using (var csvWriter = new CsvWriter(streamWriter, new CultureInfo("ja-JP", false)))
                    {
                        //ヘッダー
                        if (writeHeader)
                        {
                            foreach (DataColumn column in dt.Columns)
                            {
                                csvWriter.WriteField(column.ColumnName);
                            }
                        }
                        //データ部
                        foreach (DataRow row in dt.Rows)
                        {
                            for (var i = 0; i < dt.Columns.Count; i++)
                            {
                                csvWriter.WriteField(row[i]);
                            }
                            csvWriter.NextRecord();
                        }
                    }
                    return memoryStream.ToArray();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Object → Csv File
        /// <summary>
        /// Object → Csv File
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="writeHeader"></param>
        /// <param name="filePath"></param>
        public void ConvertObjToCsvFile<T>(List<T> obj, bool writeHeader, string filePath)
        {
            try
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
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Object → Csv Image
        /// <summary>
        /// Object → Csv Image
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="writeHeader"></param>
        /// <returns></returns>
        public byte[] ConvertObjToCsvImage<T>(List<T> obj, bool writeHeader)
        {
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    using (var streamWriter = new StreamWriter(memoryStream))
                    using (var csvWriter = new CsvWriter(streamWriter, new CultureInfo("ja-JP", false)))
                    {
                        //ヘッダー
                        if (writeHeader)
                        {
                            csvWriter.WriteHeader<T>();
                            csvWriter.NextRecord();
                        }
                        //データ部
                        foreach (var t in obj)
                        {
                            csvWriter.WriteRecord(t);
                            csvWriter.NextRecord();
                        }
                    }
                    return memoryStream.ToArray();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Csv File → Object
        /// <summary>
        /// Csv File → Object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public List<T> ConvertCsvFileToObject<T>(string filePath)
        {
            try
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
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region List<DataRow> → CSVイメージ（Byte[]）
        /// <summary>
        /// List<DataRow> → CSVイメージ（Byte[]）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="writeHeader"></param>
        /// <returns></returns>
        public byte[] ConvertRowListToCsvImage(List<DataRow> obj, bool writeHeader)
        {
            try
            {
                var head = obj.FirstOrDefault();
                using (var memoryStream = new MemoryStream())
                {
                    using (var streamWriter = new StreamWriter(memoryStream, Encoding.GetEncoding(this.Encode)))
                    using (var csvWriter = new CsvWriter(streamWriter, new CultureInfo("ja-JP", false)))
                    {
                        //ヘッダー
                        if (writeHeader)
                        {
                            foreach (DataColumn column in head.Table.Columns)
                            {
                                csvWriter.WriteField(column.ColumnName);
                            }
                            csvWriter.NextRecord();
                        }
                        //データ部
                        foreach (var t in obj)
                        {
                            foreach (object ob in t.ItemArray)
                            {
                                csvWriter.WriteField(ob.ToString());
                            }
                            csvWriter.NextRecord();
                        }
                    }
                    return memoryStream.ToArray();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
