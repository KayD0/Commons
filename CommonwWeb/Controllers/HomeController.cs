﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CommonwWeb.Models;
using Commons.DbAccessor;
using Commons.DbAccessor.Parameters;
using System.Data;
using Commons.FileReader;
using System.Text;
using Microsoft.AspNetCore.Http;
using Commons.DataUtil;
using Commons.DbAccesor.Services;
using System.Collections;
using System.IO;
using System.Net.Http.Headers;

namespace CommonwWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private ServiceMssql sMssql = new ServiceMssql();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// ダウンロード（実態）
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Download(HomeViewModel m)
        {

            //データベース読み取り
            Hashtable ret = new Hashtable();
            byte[] readImage = sMssql.ReadImage(2, ref ret);

            //text/plain                 テキストファイル
            //text/csv                    CSVファイル
            //text/html                   HTMLファイル
            //text/css                    CSSファイル
            //text/javascript             JavaScriptファイル
            //application/octet-stream    EXEファイルなどの実行ファイル
            //application/json            JSONファイル
            //application/pdf             PDFファイル
            //application/vnd.ms-excel    EXCELファイル( .xls OFFICE 2007より過去)
            //application/vnd.openxmlformats-officedocument.spreadsheetml.sheet   EXCELファイル( .xlsx OFFICE 2007以降)
            //application/vnd.ms-powerpoint   PowerPointファイル( .ppt OFFICE 2007より過去)
            //application/vnd.openxmlformats-officedocument.presentationml.presentation   PowerPointファイル( .pptx OFFICE 2007以降)
            //application/msword          WORDファイル( .doc OFFICE 2007より過去)
            //application/vnd.openxmlformats-officedocument.wordprocessingml.document WORDファイル(.docx OFFICE 2007以降)
            //application/zip     Zipファイル
            //application/x-lzh   LZHファイル
            //application/x-tar   tarファイル / tar & gzipファイル
            return File(readImage, ret["FiletContentType"].ToString(),ret["Name"].ToString());
        }

        /// <summary>
        /// ダウンロード（新タブにてブラウザで表示）
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DownloadOpen(HomeViewModel m)
        {

            //データベース読み取り
            Hashtable ret = new Hashtable();
            byte[] readImage = sMssql.ReadImage(2, ref ret);

            //text/plain                 テキストファイル
            //text/csv                    CSVファイル
            //text/html                   HTMLファイル
            //text/css                    CSSファイル
            //text/javascript             JavaScriptファイル
            //application/octet-stream    EXEファイルなどの実行ファイル
            //application/json            JSONファイル
            //application/pdf             PDFファイル
            //application/vnd.ms-excel    EXCELファイル( .xls OFFICE 2007より過去)
            //application/vnd.openxmlformats-officedocument.spreadsheetml.sheet   EXCELファイル( .xlsx OFFICE 2007以降)
            //application/vnd.ms-powerpoint   PowerPointファイル( .ppt OFFICE 2007より過去)
            //application/vnd.openxmlformats-officedocument.presentationml.presentation   PowerPointファイル( .pptx OFFICE 2007以降)
            //application/msword          WORDファイル( .doc OFFICE 2007より過去)
            //application/vnd.openxmlformats-officedocument.wordprocessingml.document WORDファイル(.docx OFFICE 2007以降)
            //application/zip     Zipファイル
            //application/x-lzh   LZHファイル
            //application/x-tar   tarファイル / tar & gzipファイル

            //var content = new MemoryStream(readImage);
            return new FileContentResult(readImage, ret["FiletContentType"].ToString());
        }

        /// <summary>
        /// ダウンロード（新タブにてブラウザで表示）
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DownloadOpenNewTab(HomeViewModel m)
        {

            //データベース読み取り
            Hashtable ret = new Hashtable();
            byte[] readImage = sMssql.ReadImage(2, ref ret);

            //text/plain                 テキストファイル
            //text/csv                    CSVファイル
            //text/html                   HTMLファイル
            //text/css                    CSSファイル
            //text/javascript             JavaScriptファイル
            //application/octet-stream    EXEファイルなどの実行ファイル
            //application/json            JSONファイル
            //application/pdf             PDFファイル
            //application/vnd.ms-excel    EXCELファイル( .xls OFFICE 2007より過去)
            //application/vnd.openxmlformats-officedocument.spreadsheetml.sheet   EXCELファイル( .xlsx OFFICE 2007以降)
            //application/vnd.ms-powerpoint   PowerPointファイル( .ppt OFFICE 2007より過去)
            //application/vnd.openxmlformats-officedocument.presentationml.presentation   PowerPointファイル( .pptx OFFICE 2007以降)
            //application/msword          WORDファイル( .doc OFFICE 2007より過去)
            //application/vnd.openxmlformats-officedocument.wordprocessingml.document WORDファイル(.docx OFFICE 2007以降)
            //application/zip     Zipファイル
            //application/x-lzh   LZHファイル
            //application/x-tar   tarファイル / tar & gzipファイル

            //var content = new MemoryStream(readImage);
            return new FileContentResult(readImage, ret["FiletContentType"].ToString());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
