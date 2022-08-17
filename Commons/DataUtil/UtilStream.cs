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
        #region メンバ変数
        //【文字コード一覧】※よく使うやつ
        //■規定で用意されているもの
        //・Unicode(UTF-16)
        private const string UNICODE = "Unicode";
        //・UTF-8
        private const string UTF_8 = "UTF-8";
        //■日本語独自のエンコーディング
        //・Shift-JIS
        private const string SHIFT_JIS = "Shift_JIS";
        //・JIS
        private const string JIS = "JIS";
        //・EUC-JP
        private const string EUC_JP = "EUC-JP";
        //・iso-2020-jp
        private const string ISO_2020_JP = "iso-2020-jp";
        public string Encode { get; set; }
        #endregion

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

        #region Stream → String
        /// <summary>
        /// Stream → String
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public string ConvertStreamToString(MemoryStream stream, string encode)
        {
            string ret = string.Empty;
            switch (encode) 
            {
                case UtilStream.UNICODE:
                    ret = Encoding.Unicode.GetString(stream.ToArray());
                    break;
                case UtilStream.UTF_8:
                    ret = Encoding.UTF8.GetString(stream.ToArray());
                    break;
                case UtilStream.SHIFT_JIS:
                    //932
                    ret = Encoding.GetEncoding(UtilStream.SHIFT_JIS).GetString(stream.ToArray());
                    break;
                case UtilStream.JIS:
                    //50220
                    ret = Encoding.GetEncoding(UtilStream.JIS).GetString(stream.ToArray());
                    break;
                case UtilStream.EUC_JP:
                    //51932
                    ret = Encoding.GetEncoding(UtilStream.EUC_JP).GetString(stream.ToArray());
                    break;
                case UtilStream.ISO_2020_JP:
                    ret = Encoding.GetEncoding(UtilStream.ISO_2020_JP).GetString(stream.ToArray());
                    break;
            }
            return ret;
        }
        #endregion
    }
}
