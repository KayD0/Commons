using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Xml.Serialization;
//using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Commons.DataUtil
{

    public class UtilXml : UtilStream
    {   
        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UtilXml()
        {

        }
        #endregion

        #region string → XmlObj
        /// <summary>
        /// string → XmlObj
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public T ConverStringToXmlObj<T>(string str)
        {
            try
            {
                //XmlSerializerオブジェクトを作成
                System.Xml.Serialization.XmlSerializer serializer =
                    new System.Xml.Serialization.XmlSerializer(typeof(T));
                
                //文字列を読込
                Encoding encoding = Encoding.GetEncoding("UTF-8");
                var sr = new MemoryStream(encoding.GetBytes(str));

                //XMLファイルから読み込み、逆シリアル化する
                T obj = (T)serializer.Deserialize(sr);
                //ファイルを閉じる
                sr.Close();
                sr.Dispose();

                //返却
                return obj;
            }
            catch (Exception) 
            {
                throw;
            }
        }
        #endregion

        #region file → XmlObj
        /// <summary>
        /// file → XmlObj
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public T ConvertFileToXmlObj<T>(string filePath)
        {
            try
            {
                //XmlSerializerオブジェクトを作成
                System.Xml.Serialization.XmlSerializer serializer =
                    new System.Xml.Serialization.XmlSerializer(typeof(T));
                //ファイルを読込
                Encoding encoding = Encoding.GetEncoding(this.Encode);
                StreamReader sr = new StreamReader(filePath, encoding);
                //XMLファイルから読み込み、逆シリアル化する
                T obj = (T)serializer.Deserialize(sr);
                //ファイルを閉じる
                sr.Close();
                //返却
                return obj;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region obj → XmlStr
        /// <summary>
        /// obj → XmlStr
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string ConvertObjToXmlStr<T>(T obj)
        {
            try
            {
                using (var stringwriter = new System.IO.StringWriter())
                {
                    var serializer = new XmlSerializer(obj.GetType());
                    serializer.Serialize(stringwriter, obj);
                    return stringwriter.ToString();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region obj → XmlFile
        /// <summary>
        /// obj → XmlFile
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="filePath"></param>
        public void ConvertObjToXmlFile<T>(T obj, string filePath)
        {
            try
            {
                var serializer = new XmlSerializer(obj.GetType());
                Encoding encoding = Encoding.GetEncoding(this.Encode);
                using (StreamWriter sw = new StreamWriter(filePath, false, encoding))
                {
                    serializer.Serialize(sw, obj);
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
