using System;
using System.Collections.Generic;
using System.Text;
using NLog;

namespace Commons
{
   public static class LoggerBase
   {
      public static string ProcessId = DateTime.Now.ToString("yyyyMMddHHmmssfff"); 
      
      public static Logger logger = LogManager.GetCurrentClassLogger();
   }
}
