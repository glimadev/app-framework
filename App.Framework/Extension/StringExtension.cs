﻿using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace App.Framework.Extension
{
    public static class StringExtension
    {
        /// <summary>
        /// Verify if is null or empty or space
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrEmptyOrSpace(this string str)
        {
            return (String.IsNullOrWhiteSpace(str));
        }

        /// <summary>
        /// Concat string with other between space
        /// </summary>
        /// <param name="text"></param>
        /// <param name="textToConcat"></param>
        /// <returns></returns>
        public static string ConcatWithSpace(this string text, string textToConcat)
        {
            if (!String.IsNullOrEmpty(text))
            {
                text = text.Trim();
            }

            if (!String.IsNullOrEmpty(textToConcat))
            {
                textToConcat = textToConcat.Trim();
            }

            return text + " " + textToConcat;
        }

        /// <summary>
        /// Get first string splited by space
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string GetFirstStringSplit(this string text)
        {
            if (!String.IsNullOrWhiteSpace(text))
            {
                return text.Split(' ')[0];
            }

            return text;
        }

        /// <summary>
        /// Get second and more strings splited by space
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string GetSecondAndMoreStringSplit(this string text)
        {
            string textSplited = null;

            if (!String.IsNullOrWhiteSpace(text))
            {
                string[] splited = text.Split(' ');

                for (int i = 0; i < splited.Length; i++)
                {
                    if (i != 0)
                    {
                        textSplited = textSplited.ConcatWithSpace(splited[i]);
                    }
                }
            }

            return (textSplited != null) ? textSplited.Trim() : " ";
        }

        /// <summary>
        /// Concat string with other between dash
        /// </summary>
        /// <param name="text"></param>
        /// <param name="textToConcat"></param>
        /// <returns></returns>
        public static string ConcatWithDash(this string text, string textToConcat)
        {
            return text + " - " + textToConcat;
        }

        /// <summary>
        /// Trim and remove double spaces
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string TrimAndReduce(this string str)
        {
            return ConvertWhitespacesToSingleSpaces(str).Trim();
        }

        /// <summary>
        /// Remove double space to once space
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ConvertWhitespacesToSingleSpaces(this string value)
        {
            return Regex.Replace(value, @"\s+", " ");
        }

        /// <summary>
        /// Convert simple to double quote
        /// </summary>
        /// <returns></returns>
        public static string ConvertSimpleQuoteToDoubleQuoteSQL(this string value)
        {
            return "'" + value.Replace("'", "''") + "'";
        }

        public static string BetweenSingleQuoteSQL(this string value)
        {
            return "'" + value + "'";
        }

        public static DateTime ConvertToDateTime(this string value)
        {
            DateTime datetime = new DateTime();

            DateTime.TryParse(value, out datetime);

            return datetime;
        }

        public static void GetSplitedName(this string fullname, out string firstName, out string lastName)
        {
            string[] Name;
            Name = fullname.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries);

            string lastNameSplited = String.Empty;
            string firstNameSplited = String.Empty;

            for (int i = 0; i < Name.Length; i++)
            {
                if (i == 0)
                {
                    firstNameSplited = Name[i];
                }
                else
                {
                    lastNameSplited = lastNameSplited.ConcatWithSpace(Name[i]);
                }
            }

            firstName = firstNameSplited;
            lastName = lastNameSplited.Trim();
        }

        /// <summary>
        /// First letter to uppercase
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string FirstCharToUpper(string input)
        {
            if (!String.IsNullOrEmpty(input))
                return input.First().ToString().ToUpper() + input.Substring(1);

            return null;
        }

        public static string PhoneMask(this string input)
        {
            if (String.IsNullOrEmpty(input))
                return null;

            long result = 0;

            long.TryParse(input, out result);

            if (input.Length == 11)
            {
                return string.Format("{0:(##) # ####-####}", result);
            }

            return string.Format("{0:(##) ####-####}", result);
        }

        public static string GenerateSlug(this string phrase)
        {
            string str = phrase.RemoveAccent().ToLower();
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            return str;
        }

        public static string RemoveAccent(this string txt)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }

        public static T ApplyFunctionToStrings<T>(this T obj, Func<string, string> func)
        {
            if (obj is IList && obj.GetType().IsGenericType)
            {
                IEnumerable auxAsEnumerableValue = (IEnumerable)obj;

                foreach (var auxAsEnumerable in auxAsEnumerableValue)
                {
                    if (auxAsEnumerable != null && auxAsEnumerable.GetType().Name != "String")
                    {
                        auxAsEnumerable.ApplyFunctionToStrings(func);
                    }
                }
            }
            else
            {
                var props = obj.GetType().GetProperties();

                foreach (var prop in props)
                {
                    var value = prop.GetValue(obj, null);

                    if (value != null)
                    {
                        if (prop.PropertyType.Name == "String")
                        {
                            prop.SetValue(obj, func(value.ToString()));
                        }
                        else if (!value.GetType().IsPrimitive && prop.PropertyType.Name != "DateTime")
                        {
                            value.ApplyFunctionToStrings(func);
                        }
                    }
                }
            }

            return obj;
        }

        public static string HasSlash(string FabMod)
        {
            return FabMod.Substring(2, 1) != "/" ? null : FabMod;
        }

        public static string StringSeparator(this string input, int Position, char Separator)
        {
            return string.IsNullOrEmpty(input) ? null : input.Split(Separator)[Position];
        }

        public static string FormatHelper(this string input, params object[] args)
        {
            return string.Format(input, args);
        }

        public static string SubstringHelper(this string input, int startIndex, int? get)
        {
            if (get.HasValue)
            {
                if (get.Value + startIndex > input.Length)
                {
                    get = startIndex - input.Length;
                }
            }
            else
            {
                get = startIndex - input.Length;
            }

            return input.Substring(startIndex, get.Value);
        }

        public static string GetValueOrDefault(this string input, string _default)
        {
            return (string.IsNullOrEmpty(input)) ? _default : input;
        }

        public static string TruncateLeft(this string InputString, int Size)
        {
            if (string.IsNullOrEmpty(InputString))
            {
                return InputString;
            }

            int Length = InputString.Length;

            if (Length <= Size)
            {
                return InputString;
            }

            return InputString.Substring(0, Size);
        }

        public static string TruncateRight(this string InputString, int Size)
        {
            if (string.IsNullOrEmpty(InputString))
            {
                return InputString;
            }

            int Length = InputString.Length;

            if (Length <= Size)
            {
                return InputString;
            }

            return InputString.Substring(Length - Size);
        }

        public static string RemoveAccents(this string input)
        {
            return Regex.Replace(input, "[A-Za-z0-9]", "");
        }

        public static string NotANumberToNull(this string input)
        {
            int result = 0;

            int.TryParse(input, out result);

            return (result == 0) ? null : result.ToString();
        }

        public static string GetSQLMoneyFormat(this decimal input)
        {
            return string.Format(CultureInfo.GetCultureInfo("en-US"), "{0:0.00}", input);
        }

        public static string GetDateTimeFormat(this DateTime input, string type)
        {
            string date = "";

            switch (type)
            {
                //Oracle
                case "Oracle":
                    date = string.Format("TO_DATE('{0}', 'YYYY/MM/DD')", input.ToString("yyyy/MM/dd"));
                    break;
                //Firebird
                case "Firebird":
                    date = "'" + input.ToString("yyyy/MM/dd") + "'";
                    break;
                //Informix
                case "Informix":
                    date = string.Format("TO_DATE('{0}', '%Y-%m-%d')", input.ToString("yyyy/MM/dd"));
                    break;
                //SQL Server
                case "SQL Server":
                    date = string.Format("CONVERT(datetime, '{0}', 103)", input.ToString("dd/MM/yyyy"));
                    break;
                case "MySQL":
                    date = string.Format("STR_TO_DATE('{0}', '%d/%m/%Y')", input.ToString("dd/MM/yyyy"));
                    break;
                default:
                    date = input.ToString("dd/MM/yyyy");
                    break;
            }

            return date;
        }
    }
}
