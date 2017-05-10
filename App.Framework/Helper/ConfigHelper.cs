using System;

namespace App.Framework.Helper
{
    public static class ConfigHelper
    {
        public static string GetSetting(string key)
        {
            string result = System.Configuration.ConfigurationManager.AppSettings[key];

            if (String.IsNullOrWhiteSpace(result))
            {
                throw new ArgumentException("Parâmetro inválido : " + key);
            }

            return result;
        }
    }
}
