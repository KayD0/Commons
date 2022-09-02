using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Commons.DataUtil.ValidateExtension
{
    /// <summary>
    /// ValidationAttributeの拡張クラス
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DateCheckerAttribute : ValidationAttribute
    {
        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="otherProperty"></param>
        public DateCheckerAttribute()
        {
            ErrorMessage = "";
        }
        #endregion

        #region エラーメッセージフォーマッタ
        /// <summary>
        /// エラーメッセージフォーマッタ
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public override string FormatErrorMessage(string name)
        {
            // エラーメッセージを返す
            return String.Format(CultureInfo.CurrentCulture, ErrorMessageString, name);
        }
        #endregion

        #region 日付書式チェック
        /// <summary>
        /// 日付Form、Toの整合性チェック
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(value.ToString()))
            {
                DateTime dt;
                // ここで値の比較。条件を満たしていれば検証成功を返す
                if (DateTime.TryParse(value.ToString(), out dt))
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
            else
            {
                return ValidationResult.Success;
            }
        }
        #endregion
    }
}
