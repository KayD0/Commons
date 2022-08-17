using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Commons.DataUtil.UtilJsonModel;
using CsvHelper;
using CsvHelper.Configuration;
//using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Commons.DataUtil
{

    public static class UtilContextType
    {
        #region 拡張子からコンテキストタイプを取得
        /// <summary>
        /// 拡張子からコンテキストタイプを取得
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        public static string GetContextType(string extension) 
        {
            string contextType = string.Empty;
            switch (extension) 
            {
                case "csv":
                    contextType = "text/csv";
                    break;
                case "txt":
                    contextType = "text/plain";
                    break;
                case "html":
                    contextType = "text/html";
                    break;
                case "css":
                    contextType = "text/css";
                    break;
                case "js":
                    contextType = "text/javascript";
                    break;
                case "exe":
                    contextType = "application/octet-stream";
                    break;
                case "json":
                    contextType = "application/json";
                    break;
                case "pdf":
                    contextType = "application/pdf";
                    break;
                case "xls":
                    //EXCELファイル( .xls OFFICE 2007より過去)
                    contextType = "application/vnd.ms-excel";
                    break;
                case "xlsx":
                    //EXCELファイル( .xlsx OFFICE 2007以降)
                    contextType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    break;
                case "ppt":
                    //PowerPointファイル( .ppt OFFICE 2007より過去)
                    contextType = "application/vnd.ms-powerpoint";
                    break;
                case "pptx":
                    //PowerPointファイル( .pptx OFFICE 2007以降)
                    contextType = "application/vnd.openxmlformats-officedocument.presentationml.presentation";
                    break;
                case "doc":
                    //WORDファイル( .doc OFFICE 2007より過去)
                    contextType = "application/msword";
                    break;
                case "docx":
                    //WORDファイル(.docx OFFICE 2007以降)
                    contextType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    break;
                case "jpeg":
                case "jpg":
                    contextType = "image/jpeg";
                    break;
                case "png":
                    contextType = "image/png";
                    break;
                case "gif":
                    contextType = "image/gif";
                    break;
                case "bmp":
                    contextType = "image/bmp";
                    break;
                case "svg":
                    contextType = "image/svg+xml";
                    break;
                case "zip":
                    contextType = "application/zip";
                    break;
                case "tar":
                case "gzip":
                    contextType = "application/x-tar";
                    break;
                case "mp3":
                    contextType = "audio/mpeg";
                    break;
                case "mp4":
                    contextType = "video/mp4";
                    break;
                case "mpeg":
                    contextType = "video/mpeg";
                    break;
            }
            return contextType;
        }
        #endregion 
    }
}
