using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace App.Framework.Helper
{
    public static class ExcelHelper
    {
        public static byte[] WriteTsv<T>(T data)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));

            using (ExcelPackage package = new ExcelPackage())
            {
                foreach (PropertyInfo propertyInfo in data.GetType().GetProperties())
                {
                    BuildServiceExcel(package, propertyInfo, propertyInfo.GetValue(data, null));
                }

                return package.GetAsByteArray();
            }
        }

        private static void BuildServiceExcel(ExcelPackage package, PropertyInfo propertyInfo, object p)
        {
            if (p != null)
            {
                var obj = (IEnumerable)(p);

                ExcelWorksheet ws = package.Workbook.Worksheets.Add(ExtensionMethods.GetDisplayName(propertyInfo));

                ws.View.ShowGridLines = true;

                int indexLine = 1;

                foreach (var exportLead in obj)
                {
                    BuildCells(ws, exportLead, ref indexLine);
                    indexLine++;
                }
            }
        }

        public static object GetValue(PropertyInfo propertyInfoLead, object obj)
        {
            if (propertyInfoLead.PropertyType == typeof(DateTime))
            {
                return Convert.ToDateTime(propertyInfoLead.GetValue(obj)).ToString("dd/MM/yyyy hh:mm");
            }
            else if (propertyInfoLead.PropertyType == typeof(DateTime?))
            {
                if (propertyInfoLead.GetValue(obj) != null)
                {
                    return Convert.ToDateTime(propertyInfoLead.GetValue(obj)).ToString("dd/MM/yyyy hh:mm");
                }
            }

            return propertyInfoLead.GetValue(obj);
        }

        private static void BuildCells(ExcelWorksheet ws, object obj, ref int indexLine)
        {
            int indexColumn = 1;

            if (indexLine == 1)
            {
                foreach (PropertyInfo propertyInfoLead in obj.GetType().GetProperties())
                {
                    bool hidden = ExtensionMethods.HiddenAttribute(propertyInfoLead);

                    if (!hidden)
                    {
                        ws.Cells[1, indexColumn].Value = ExtensionMethods.GetDisplayName(propertyInfoLead);
                        indexColumn++;
                    }
                }

                indexLine++;
            }

            indexColumn = 1;

            foreach (PropertyInfo propertyInfoLead in obj.GetType().GetProperties())
            {
                string name = ExtensionMethods.GetDisplayName(propertyInfoLead);
                bool hidden = ExtensionMethods.HiddenAttribute(propertyInfoLead);

                if (!hidden)
                {
                    ws.Cells[indexLine, indexColumn].Value = GetValue(propertyInfoLead, obj);

                    indexColumn++;
                }
            }
        }

        public static class ExtensionMethods
        {
            public static string GetDisplayName(PropertyInfo prop)
            {
                if (prop.CustomAttributes == null || prop.CustomAttributes.Count() == 0)
                    return prop.Name;

                var displayNameAttribute = prop.CustomAttributes.Where(x => x.AttributeType == typeof(DisplayNameAttribute)).FirstOrDefault();

                if (displayNameAttribute == null || displayNameAttribute.ConstructorArguments == null || displayNameAttribute.ConstructorArguments.Count == 0)
                    return prop.Name;

                return displayNameAttribute.ConstructorArguments[0].Value.ToString() ?? prop.Name;
            }

            public static bool HiddenAttribute(PropertyInfo prop)
            {
                if (prop.CustomAttributes == null || prop.CustomAttributes.Count() == 0)
                    return false;

                var hiddenAttribute = prop.CustomAttributes.Where(x => x.AttributeType == typeof(DesignerSerializationVisibilityAttribute)).FirstOrDefault();

                if (hiddenAttribute != null)
                {
                    return !Convert.ToBoolean(hiddenAttribute.ConstructorArguments[0].Value);
                }

                return false;
            }
        }
    }
}
