using System;
using System.ComponentModel;

namespace App.Framework.Extension
{
    public static class EnumExtension
    {
        public static String GetEnumDescription(this Enum obj)
        {
            try
            {
                System.Reflection.FieldInfo fieldInfo =
                    obj.GetType().GetField(obj.ToString());

                object[] attribArray = fieldInfo.GetCustomAttributes(false);

                if (attribArray.Length > 0)
                {
                    var attrib = attribArray[0] as DescriptionAttribute;

                    if (attrib != null)
                        return attrib.Description;
                }
                return obj.ToString();
            }
            catch (NullReferenceException ex)
            {
                return "Unknown Description Enum";
            }
        }
    }
}
