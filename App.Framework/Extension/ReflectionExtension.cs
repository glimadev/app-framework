using System.Collections;

namespace App.Framework.Extension
{
    public static class ReflectionExtension
    {
        public static string GetStringFromObject<T>(this T obj)
        {
            string aux = "";

            var props = obj.GetType().GetProperties();

            foreach (var prop in props)
            {
                var auxValue = prop.GetValue(obj, null);

                if (auxValue is IList && auxValue.GetType().IsGenericType)
                {
                    IEnumerable auxAsEnumerableValue = (IEnumerable)auxValue;

                    foreach (var auxAsEnumerable in auxAsEnumerableValue)
                    {
                        aux += auxAsEnumerable.GetStringFromObject();
                    }
                }
                else
                {
                    aux += prop.GetValue(obj, null); // against prop.Name
                }
            }

            return aux;
        }

        public static string GetStringFromObjectWithPropName<T>(this T obj)
        {
            string aux = "";

            var props = obj.GetType().GetProperties();

            foreach (var prop in props)
            {
                aux += string.Format("{0} = {1};", prop.Name, prop.GetValue(obj, null)); // against prop.Name
            }

            return aux;
        }
    }
}
