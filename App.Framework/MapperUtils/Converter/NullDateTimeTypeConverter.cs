using AutoMapper;
using System;

namespace Vitali.Framework.MapperUtils.Converter
{
    public class NullDateTimeTypeConverter : TypeConverter<string, DateTime?>
    {
        protected override DateTime? ConvertCore(string source)
        {
            DateTime dataTest;
            if (source == null || !DateTime.TryParse(source, out dataTest))
                return null;
            else
            {
                return dataTest;
            }
        }
    }
}
