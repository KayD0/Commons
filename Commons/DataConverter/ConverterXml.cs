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

    public class ConverterXml : ConverterStream
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ConverterXml() 
        {
            this.encode = "Shift_JIS";   
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ConverterXml(string enc)
        {
            this.encode = enc;
        }

        /// <summary>
        /// String → Stream
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public T ConvertXmlFromString<T>(string str)
        {
            //XmlSerializerオブジェクトを作成
            System.Xml.Serialization.XmlSerializer serializer =
                new System.Xml.Serialization.XmlSerializer(typeof(T));
            //文字列を読込
            var sr = this.ConvertStreamFromString(str);
            //XMLファイルから読み込み、逆シリアル化する
            T obj = (T)serializer.Deserialize(sr);
            //ファイルを閉じる
            sr.Close();
            //返却
            return obj;
        }
    }
}
