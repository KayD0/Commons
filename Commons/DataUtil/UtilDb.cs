using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Commons.DataUtil.UtilJsonModel;
using Commons.DbAccessor.Parameters;
using CsvHelper;
using CsvHelper.Configuration;
//using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Commons.DataUtil
{

    public class UtilDb
    {
        #region モデルリストからインサートクエリを作成
        /// <summary>
        /// モデルリストからインサートクエリを作成
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        public List<string> GetModelToInsertQuery<T>(List<T> objList,string database,string table)
        {
            List<string> queryList = new List<string>();
            foreach (var item in objList)
            {
                var props = item.GetType().GetProperties();

                //ヘッダ作成
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($" INSERT INTO {database}.dbo.{table}");
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

                //Value部分作成
                sb.AppendLine(" VALUE ");
                sb.AppendLine("( ");
                int bodyIndex = 1;
                string value = string.Empty;
                foreach (var prop in props)
                {
                    value = prop.GetValue(item).ToString();
                    value = this.IsAddSingleQuate(prop) ? "'" + value + "'" : value;
                    var propTypeName = prop.PropertyType.FullName;
                    if (bodyIndex == 1)
                    {
                        sb.AppendLine(value);
                    }
                    else
                    {
                        sb.AppendLine("," + value);
                    }
                    bodyIndex++;
                }
                sb.AppendLine(") ");
                queryList.Add(sb.ToString());
            }
            return queryList;
        }
        #endregion

        /// <summary>
        /// シングルクオウト必要か判断
        /// </summary>
        /// <param name="prop"></param>
        /// <returns></returns>
        public bool IsAddSingleQuate(PropertyInfo prop)
        {
            string propTypeName = prop.PropertyType.FullName;
            bool IsAdd = false;
            switch (propTypeName) 
            {
                case "System.String":
                case "System.Char":
                case "System.Char[]":
                case "System.DateTime":
                    IsAdd = true;
                    break;
                case "System.Int16":
                case "System.Int32":
                case "System.Int64":
                case "System.Decimal":
                case "System.Float":
                case "System.Double":
                case "System.Bool":
                    IsAdd = false;
                    break;
            }
            return IsAdd;
        }
    }
}
