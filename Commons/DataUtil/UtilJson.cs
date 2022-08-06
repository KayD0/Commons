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

    public class UtilJson : UtilStream
    {

        /// <summary>
        /// 
        /// </summary>
        public UtilJson()
        {
        }

        public UtilJson(string path)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public string ConvertFileToJString(string filePath)
        {
            string jsonData = string.Empty;
            Encoding encoding = Encoding.GetEncoding(this.Encode);
            try
            {
                using (var sr = new StreamReader(filePath, encoding))
                {
                    jsonData = sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
            }
            return jsonData;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JObject ConvertFileToJObject(string filePath)
        {
            string jsonData = string.Empty;
            Encoding encoding = Encoding.GetEncoding(this.Encode);
            try
            {
                using (var sr = new StreamReader(filePath, encoding))
                {
                    jsonData = sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
            }
            return JObject.Parse(jsonData);
        }

        public T ConvertJStringToObject<T>(string context)
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
    }
}
