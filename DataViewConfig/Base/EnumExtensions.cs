using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataViewConfig.Base
{
    public static  class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            if (value == null )
            {
                return string.Empty;
            }
            FieldInfo field = value.GetType().GetField(value.ToString());
            if (field == null) return string.Empty;
            DescriptionAttribute attribute = (DescriptionAttribute)field.GetCustomAttribute(typeof(DescriptionAttribute));
            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
}
