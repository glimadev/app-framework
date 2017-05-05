using System;

namespace Vitali.Framework.Extension
{
    public static class DateTimeExtension
    {
        /// <summary>
        /// Format yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static string EnglishFormat(this DateTime datetime)
        {
            return datetime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// Format yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static string BrazilianFormat(this DateTime datetime)
        {
            return datetime.ToString("dd/MM/yyyy HH:mm:ss");
        }

        /// <summary>
        /// Format yyyy-MM-dd HH:mm
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static string BrazilianFormatNoSeconds(this DateTime? datetime)
        {
            if (datetime.HasValue)
            {
                return datetime.Value.ToString("dd/MM/yyyy HH:mm");
            }

            return null;
        }

        /// <summary>
        /// Format yyyy-MM-dd HH:mm
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static string BrazilianFormatNoSeconds(this DateTime datetime)
        {
            return datetime.ToString("dd/MM/yyyy HH:mm");
        }

        /// <summary>
        /// Convert nullable date to string
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static string ToStringNullable(this DateTime? datetime)
        {
            if (!datetime.HasValue)
            {
                return null;
            }

            return datetime.Value.BrazilianFormat();
        }
    }
}
