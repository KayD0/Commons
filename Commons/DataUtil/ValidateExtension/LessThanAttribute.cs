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
    public class LessThanAttribute : ValidationAttribute
    {
        #region 変数
        public string OtherProperty { get; private set; }
        public string OtherPropertyDisplayName { get; internal set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// 
        /// </summary>
        /// <param name="otherProperty"></param>
        public LessThanAttribute(string otherProperty)
        {
            OtherProperty = otherProperty;
            ErrorMessage = "{0}は{1}より小さい日付を指定してください。";
        }
        #endregion

        #region エラーメッセージフォーマッタ
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public override string FormatErrorMessage(string name)
        {
            // エラーメッセージを返す
            return String.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, OtherPropertyDisplayName ?? OtherProperty);
        }
        #endregion

        #region バリデーション
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // 比較対象のPropertyInfo
            PropertyInfo propertyInfo = validationContext.ObjectType.GetProperty(OtherProperty);
            this.OtherPropertyDisplayName = GetDisplayNameForProperty(validationContext.ObjectType, OtherProperty);
            // 比較対象のプロパティの値
            object propertyValue = propertyInfo.GetValue(validationContext.ObjectInstance, null);
            Type type = propertyInfo.PropertyType;

            if (!string.IsNullOrEmpty(value.ToString()) && !string.IsNullOrEmpty(propertyValue.ToString()))
            {
                DateTime from = DateTime.Parse(value.ToString());
                DateTime to = DateTime.Parse(propertyValue.ToString());
                // ここで値の比較。条件を満たしていれば検証成功を返す
                if (from < to)
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

        #region 比較対象のプロパティ名を取得する。
        /// <summary>
        /// 比較対象のプロパティ名を取得する。
        /// </summary>
        /// <param name="containerType"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private static string GetDisplayNameForProperty(Type containerType, string propertyName)
        {
            ICustomTypeDescriptor typeDescriptor = GetTypeDescriptor(containerType);
            PropertyDescriptor property = typeDescriptor.GetProperties().Find(propertyName, true);

            if (property == null)
            {
                throw new ArgumentException();
            }

            IEnumerable<Attribute> attributes = property.Attributes.Cast<Attribute>();
            DisplayAttribute display = attributes.OfType<DisplayAttribute>().FirstOrDefault();

            if (display != null)
            {
                // DisplayAttributeがついてたらその名称を返す
                return display.GetName();
            }

            DisplayNameAttribute displayName = attributes.OfType<DisplayNameAttribute>().FirstOrDefault();

            if (displayName != null)
            {
                // DisplayNameAttributeがついてたらその名称を返す
                return displayName.DisplayName;
            }
            return propertyName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static ICustomTypeDescriptor GetTypeDescriptor(Type type)
        {
            return new AssociatedMetadataTypeTypeDescriptionProvider(type).GetTypeDescriptor(type);
        }
        #endregion
    }
}
