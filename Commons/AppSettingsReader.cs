using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Json;
using Microsoft.Extensions.Configuration;

namespace Commons.ConfigReader
{
    public class AppSettingsReader
    {
        public IConfiguration conf;

        /// <summary>
        /// 設定ファイル読み込み
        /// </summary>
        private AppSettingsReader() 
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);
            conf = builder.Build();
        } 
    }
}
