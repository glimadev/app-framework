using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace Vitali.Framework.Json
{
    public static class JsonUtils
    {
        /// <summary>
        /// Convert object to JSON
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="ignoreNullable"></param>
        /// <param name="formatDate"></param>
        /// <returns></returns>
        public static string ConvertToJson(object obj, bool ignoreNullable = true, bool formatDateJson = true)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            
            if (ignoreNullable)
            {
                settings.NullValueHandling = NullValueHandling.Ignore;
            }

            if (formatDateJson)
            {
                settings.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
            }

            return JsonConvert.SerializeObject(obj, settings);
        }

        /// <summary>
        /// Deserialize to object
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static object ConvertToObject(string json)
        {
            return JsonConvert.DeserializeObject(json);
        }

        /// <summary>
        /// Deserialize to object type
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T ConvertToObject<T>(object json)
        {
            return JsonConvert.DeserializeObject<T>(json.ToString());
        }

        /// <summary>
        /// Convert JArray to Object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonArray"></param>
        /// <returns></returns>
        public static List<T> ConvertJArrayToObject<T>(object jsonArray)
        {
            if (jsonArray != null && jsonArray is JArray)
            {
                return ((JArray)jsonArray).ToObject<List<T>>();
            }

            return null;
        }
    }
}
