using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
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

        #region モデルの入力値有無チェックを行う
        /// <summary>
        /// モデルの入力値有無チェックを行う
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsExistConditon<T>(T obj)
        {
            bool isExist = false;
            PropertyInfo[] props = obj.GetType().GetProperties();
            foreach (PropertyInfo prop in props) 
            {
                string val = prop.GetValue(obj).ToString();
                if (!string.IsNullOrEmpty(val)) 
                {
                    isExist = true;
                    break;
                }
            }
            return isExist;
        }
        #endregion 
    }

    #region バリデーションサンプルクラス
    //参考サイト
    //https://qiita.com/ktr1211/items/132e7216709c912917c6z
    //https://qiita.com/grrrr/items/7c8811b5cf37d700adc4
    //http://ssk-development.blogspot.com/2013/08/blog-post.html
    public class UtilValidatorSampleModel
    {
        #region よく使いそうなやつ
        [Display( Name = "")]
        [RegularExpression("^\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$")]
        public string Email { get; set; }

        [Display(Name = "")]
        [RegularExpression("^[a-zA-Z0-9][a-zA-Z0-9-]{1,61}[a-zA-Z0-9]\\.[a-zA-Z]{2,}$")]
        public string Domain { get; set; }

        [Display(Name = "")]
        [RegularExpression("^(http|https)://([\\w-]+\\.)+[\\w-]+(/[\\w-./?%&=]*)?$")]
        public string Url { get; set; }

        [Display(Name = "")]
        [RegularExpression("^[a-zA-Z0-9\\-.]{0,15}$")]
        public string UserName { get; set; }

        [Display(Name = "")]
        [RegularExpression("^0\\d-\\d{4}-\\d{4}$")]
        public string FixedPhone { get; set; }

        [Display(Name = "")]
        [RegularExpression("^(070|080|090)-\\d{4}-\\d{4}$")]
        public string CellPhone { get; set; }

        [Display(Name = "")]
        [RegularExpression("^050-\\d{4}-\\d{4}$")]
        public string IpPhone { get; set; }

        [Display(Name = "")]
        [RegularExpression("^0120-\\d{3}-\\d{3}$")]
        public string FreeDial { get; set; }

        [Display(Name = "")]
        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{8,10}$")]
        public string Password { get; set; }

        [Display(Name = "")]
        [RegularExpression("^\\d{4}-\\d{1,2}-\\d{1,2}$")]
        public string Date { get; set; }

        [Display(Name = "")]
        [RegularExpression("^\\d{3}-\\d{3}$")]
        public string PostNo { get; set; }

        [Display(Name = "")]
        [RegularExpression("^([a-zA-Z]+-?)+[a-zA-Z0-9]+\\\\.[x|X][m|M][l|L]$")]
        public string Xml { get; set; }

        [Display(Name = "")]
        [RegularExpression("\\n\\s*\\r")]
        public string Blank { get; set; }

        [Display(Name = "")]
        [RegularExpression("\\d+\\.\\d+\\.\\d+\\.\\d+")]
        public string IpAdress { get; set; }

        [Display(Name = "")]
        [RegularExpression("((?:(?:25[0-5]|2[0-4]\\\\d|[01]?\\\\d?\\\\d)\\\\.){3}(?:25[0-5]|2[0-4]\\\\d|[01]?\\\\d?\\\\d))")]
        public string IpAdress2 { get; set; }
        #endregion

        #region 数値系
        [Display(Name = "")]
        [RegularExpression("^[0-9]*$")]
        public string Num { get; set; }

        [Display(Name = "")]
        [RegularExpression("^\\D{9}$")]
        public string Num2 { get; set; }

        [Display(Name = "")]
        [RegularExpression("^\\D{9,}$")]
        public string Num3 { get; set; }

        [Display(Name = "")]
        [RegularExpression("^\\d{6,9}")]
        public string Num4 { get; set; }

        [Display(Name = "")]
        [RegularExpression("^(0[0-9]*)$")]
        public string Num5 { get; set; }

        [Display(Name = "")]
        [RegularExpression("^([1-9][0-9]*)$")]
        public string Num6 { get; set; }

        [Display(Name = "")]
        [RegularExpression("^(\\-)?\\d+(\\.\\d{1,2})?$")]
        public string Num7 { get; set; }

        [Display(Name = "")]
        [RegularExpression("^[1-9]\\d*$")]
        public string Num8 { get; set; }

        [Display(Name = "")]
        [RegularExpression("^\\-[1-9]\\d*$")]
        public string Num9 { get; set; }
        #endregion

        #region 文字列系
        [Display(Name = "")]
        [RegularExpression("^[A-Za-z]+$")]
        public string str1 { get; set; }

        [Display(Name = "")]
        [RegularExpression("^[A-Z]+$")]
        public string str2 { get; set; }

        [Display(Name = "")]
        [RegularExpression("^[a-z]+$")]
        public string str3 { get; set; }

        [Display(Name = "")]
        [RegularExpression("^.{3,20}$")]
        public string str4 { get; set; }

        [Display(Name = "")]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string str5 { get; set; }
        #endregion 
    }
    #endregion
}
