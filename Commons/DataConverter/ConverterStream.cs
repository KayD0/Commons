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

    public class ConverterStream
    {
        /// <summary>
        /// エンコード
        /// </summary>
        protected string encode {get; set;}
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ConverterStream() 
        {

        }

        /// <summary>
        /// String → Stream
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public Stream ConvertStreamFromString(string str)
        {
            Encoding encoding = Encoding.GetEncoding(this.encode);
            return new MemoryStream(encoding.GetBytes(str));
        }

        /// <summary>
        /// String → JsonStream
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public JsonReader ConvertJsonStreamFromString(string str)
        {
            Encoding encoding = Encoding.GetEncoding(this.encode);
            var msr = new MemoryStream(encoding.GetBytes(str));
            var sr = new StreamReader(msr);
            var jsr = new JsonTextReader(sr);
            return jsr;
        }

        /// <summary>
        /// File → String
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string ConvertStringFromFile(string filePath)
        {
            Encoding encoding = Encoding.GetEncoding(this.encode);
            string content = "";
            using (StreamReader sr = new StreamReader(filePath, encoding)) 
            {
                content = sr.ReadToEnd();
            }
            return content;
        }

        /// <summary>
        /// Stream → String
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string ConvertStringFromStream(string str)
        {
            Encoding encoding = Encoding.GetEncoding(this.encode);
            MemoryStream stream = new MemoryStream(encoding.GetBytes(str));
            return encoding.GetString(stream.ToArray());
        }
    }
}
