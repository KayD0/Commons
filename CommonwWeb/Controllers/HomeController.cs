using System;
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

namespace CommonwWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

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

        //[HttpPost]
        //public async Task<IActionResult> Download(HomeViewModel m)
        //{

            //var dataBase1 = Request.Form["DataBase2"].FirstOrDefault();
            //var iid1 = Request.Form["Iid2"].FirstOrDefault();
            //var fileType1 = Request.Form["FileType2"].FirstOrDefault();

            //AccessorMssql mssql = new AccessorMssql();
            //string cmd = "SELECT * FROM PlayGround.dbo.Student";
            //List<DbParamerter> paramList = new List<DbParamerter>();
            //DataTable dt = new DataTable();
            //mssql.ExecuteQuery(ref dt, cmd, paramList);

            //UtilCsv cc = new UtilCsv();
            //string csvData = cc.ConvertToCsvData(dt, true);
            //byte[] csvDataByte = Encoding.ASCII.GetBytes(csvData);

            //return File(csvDataByte, fileType1);
        //}

        //public async Task<IActionResult> Download2()
        //{
            //var req = Request;
            //var dataBase1 = Request.Form["DataBase2"];
            //var iid1 = Request.Form["Iid2"];
            //var fileType1 = Request.Form["FileType2"];

            //AccessorMssql mssql = new AccessorMssql();
            //string cmd = "SELECT * FROM PlayGround.dbo.Student";
            //List<DbParamerter> paramList = new List<DbParamerter>();
            //DataTable dt = new DataTable();
            //mssql.ExecuteQuery(ref dt, cmd, paramList);

            //UtilCsv cc = new UtilCsv();
            //string csvData = cc.ConvertToCsvData(dt, true);
            //byte[] csvDataByte = Encoding.ASCII.GetBytes(csvData);

            //return PhysicalFile(csvDataByte, "text/csv");
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
