using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
//using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Commons.DataUtil
{

    public class UtilJson
    {

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UtilJson()
        {
        }
        

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="path"></param>
        public UtilJson(string path)
        {
        }
        #endregion

        #region File → JString 
        /// <summary>
        /// File → JString 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public string ConvertFileToJString(string filePath)
        {
            try
            {
                string jsonData = string.Empty;
                Encoding encoding = Encoding.GetEncoding("UTF-8");

                using (var sr = new StreamReader(filePath, encoding))
                {
                    jsonData = sr.ReadToEnd();
                }
                return jsonData;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region File → JObject
        /// <summary>
        /// File → JObject
        /// </summary>
        /// <returns></returns>
        public JObject ConvertFileToJObject(string filePath)
        {
            try
            {
                string jsonData = string.Empty;
                Encoding encoding = Encoding.GetEncoding("UTF-8");

                using (var sr = new StreamReader(filePath, encoding))
                {
                    jsonData = sr.ReadToEnd();
                }
                return JObject.Parse(jsonData);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region JString → Object
        public T ConvertJStringToObject<T>(string context)
        {
            try
            {
                using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(context)))
                {
                    //var setting = new DataContractJsonSerializerSettings()
                    //{
                    //    UseSimpleDictionaryFormat = true,
                    //};
                    var serializer = new DataContractJsonSerializer(typeof(T)/*, setting*/);
                    return (T)serializer.ReadObject(stream);
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
