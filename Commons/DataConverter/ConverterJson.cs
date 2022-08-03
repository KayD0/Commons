using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
//using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Commons.FileReader
{

    public class ConverterJson : ConverterStream
    {
        /// <summary>
        /// 
        /// </summary>
        private string filePath = string.Empty;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="fileName"></param>
        public ConverterJson(string fileName) 
        {
            this.filePath = System.Environment.CurrentDirectory + @"\" + fileName;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ConvertJsonFromFile()
        {
            if (!File.Exists(this.filePath))
            {
                return "";
            }

            string jsonData = string.Empty;
            try
            {
                using (var sr = new StreamReader(this.filePath, System.Text.Encoding.UTF8))
                {
                    jsonData = sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                LoggerBase.logger.Info(e.Message);
            }
            return jsonData;
        }

        public string ConvertJsonFromString()
        {
            if (!File.Exists(this.filePath))
            {
                return "";
            }

            string jsonData = string.Empty;
            try
            {
                using (var sr = new StreamReader(this.filePath, System.Text.Encoding.UTF8))
                {
                    jsonData = sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
            }
            return jsonData;
        }

        public T DeserializeToObjext<T>(string json)
        {

            //文字列を読込
            var sr = this.ConvertJsonStreamFromString(json);


            T obj = (T)JsonSerializer.Deserialize(sr);
            string jsonData = string.Empty;
            try
            {
                
            }
            catch (Exception e)
            {
            }

            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public void Serialize(dynamic obj)
        {
            var options = new System.Text.Json.JsonSerializerOptions { WriteIndented = true };

            try
            {
                string jsonData = JsonConvert.SerializeObject(obj);
                File.WriteAllText(this.filePath, jsonData);
            }
            catch (Exception e)
            {
            }
        }
    }
}
