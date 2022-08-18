using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

    public static class UtilValidator
    {
        #region モデルのバリデーションを行う
        /// <summary>
        /// モデルのバリデーションを行う
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static List<string> GetContextType<T>(T obj) 
        {
            var ctx = new ValidationContext(obj);
            var results = new List<ValidationResult>();
            List<string> errorList = new List<string>();
            if (!Validator.TryValidateObject(obj, ctx, results, true)) 
            {
                foreach (var result in results) 
                {
                    errorList.Add(result.ErrorMessage);
                }
            }
            return errorList;
        }
        #endregion 
    }
}
