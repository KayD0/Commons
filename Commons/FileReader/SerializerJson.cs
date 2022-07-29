using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
//using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Commons.FileReader
{

    public class SerializerJson
   {
        /// <summary>
        /// 
        /// </summary>
        private string filePath = string.Empty;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="fileName"></param>
        public SerializerJson(string fileName) 
        {
            this.filePath = System.Environment.CurrentDirectory + @"\" + fileName;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Deserialize() 
        {
            if (!File.Exists(this.filePath)) 
            {
                LoggerBase.logger.Error("[ProcessId:{0}] [JSONデシリアライズ実行処理] ファイルが存在しません。", LoggerBase.ProcessId);
                return "";
            }

            string jsonData = string.Empty;
            try
            {    
                LoggerBase.logger.Info("[ProcessId:{0}] [JSONデシリアライズ実行処理] 開始", LoggerBase.ProcessId);
                using (var sr = new StreamReader(this.filePath, System.Text.Encoding.UTF8))
                {
                    jsonData = sr.ReadToEnd();
                }
                LoggerBase.logger.Info("[ProcessId:{0}] [JSONデシリアライズ実行処理] 成功", LoggerBase.ProcessId);
            }
            catch (Exception e)
            {
                LoggerBase.logger.Info("[ProcessId:{0}] [JSONデシリアライズ実行処理] 異常終了", LoggerBase.ProcessId);
                LoggerBase.logger.Info(e.Message);
            }
            return jsonData;
        }

        public JObject DeserializeToObjext()
        {
            if (!File.Exists(this.filePath))
            {
                LoggerBase.logger.Error("[ProcessId:{0}] [JSONデシリアライズ実行処理] ファイルが存在しません。", LoggerBase.ProcessId);
                return null;
            }

            string jsonData = string.Empty;
            try
            {
                LoggerBase.logger.Info("[ProcessId:{0}] [JSONデシリアライズ実行処理] 開始", LoggerBase.ProcessId);
                using (var sr = new StreamReader(this.filePath, System.Text.Encoding.UTF8))
                {
                    jsonData = sr.ReadToEnd();
                }
                LoggerBase.logger.Info("[ProcessId:{0}] [JSONデシリアライズ実行処理] 成功", LoggerBase.ProcessId);
            }
            catch (Exception e)
            {
                LoggerBase.logger.Info("[ProcessId:{0}] [JSONデシリアライズ実行処理] 異常終了", LoggerBase.ProcessId);
                LoggerBase.logger.Info(e.Message);
            }

            return JObject.Parse(jsonData);
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
                LoggerBase.logger.Info("[ProcessId:{0}] [JSONデリアライズ実行処理] 開始", LoggerBase.ProcessId);
                string jsonData = JsonConvert.SerializeObject(obj);
                File.WriteAllText(this.filePath, jsonData);
                LoggerBase.logger.Info("[ProcessId:{0}] [JSONデリアライズ実行処理] 成功", LoggerBase.ProcessId);
            }
            catch (Exception e) 
            {
                LoggerBase.logger.Info("[ProcessId:{0}] [JSONデリアライズ実行処理] 異常終了", LoggerBase.ProcessId);
                LoggerBase.logger.Info(e.Message);
            }
            LoggerBase.logger.Info("[ProcessId:{0}] [JSONシリアライズ実行処理] 終了", LoggerBase.ProcessId);
        }
    }
}
