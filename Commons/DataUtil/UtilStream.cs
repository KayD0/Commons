using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
//using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Commons.DataUtil
{

    public class UtilStream
    {
        //【文字コード一覧】※よく使うやつ
        //■規定で用意されているもの
        //・Unicode(UTF-16)
        //・UTF-8
        //■日本語独自のエンコーディング
        //・Shift-JIS
        //・EUC-JP
        //・iso-2020-jp
        public string Encode { get; set; }

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UtilStream()
        {
            this.Encode = "UTF-8";
        }
        #endregion

        #region String → Stream
        /// <summary>
        /// String → Stream
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public Stream ConvertStringToStream(string str)
        {
            Encoding encoding = Encoding.GetEncoding(this.Encode);
            return new MemoryStream(encoding.GetBytes(str));
        }
        #endregion

        #region String → JsonStream
        /// <summary>
        /// String → JsonStream
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public JsonReader ConvertStringToJsonStream(string str)
        {
            Encoding encoding = Encoding.GetEncoding(this.Encode);
            var msr = new MemoryStream(encoding.GetBytes(str));
            var sr = new StreamReader(msr);
            var jsr = new JsonTextReader(sr);
            return jsr;
        }
        #endregion

        #region String → File
        /// <summary>
        /// String → File
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public void ConvertStringToFile(string context, string filePath)
        {
            Encoding encoding = Encoding.GetEncoding(this.Encode);
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.Write(context);
            }
        }
        #endregion

        #region File → String
        /// <summary>
        /// File → String
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string ConvertFileToString(string filePath)
        {
            Encoding encoding = Encoding.GetEncoding(this.Encode);
            string content = "";
            using (StreamReader sr = new StreamReader(filePath, encoding))
            {
                content = sr.ReadToEnd();
            }
            return content;
        }
        #endregion
    }
}
