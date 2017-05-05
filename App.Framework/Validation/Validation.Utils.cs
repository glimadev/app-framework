using System;
using System.Data.SqlTypes;

namespace Vitali.Framework.Validation
{
    /// <summary>
    /// Classe para validar dados
    /// </summary>
    public static partial class Validation
    {
        //Validação de valores vazios
        public static class Utils
        {
            #region .: Valor Vazio :.

            public static bool ValorVazio(bool blnValor)
            {
                return !blnValor;
            }

            public static bool ValorVazio(DateTime dteDateTime)
            {
                return (dteDateTime.Equals(new DateTime()) || dteDateTime.Equals(new SqlDateTime()));
            }

            public static bool ValorVazio(decimal decValor)
            {
                return decValor.Equals((decimal)0M);
            }

            public static bool ValorVazio(double dblValor)
            {
                return dblValor.Equals((double)0.0);
            }

            public static bool ValorVazio(Guid guidValor)
            {
                return guidValor.Equals(Guid.Empty);
            }

            public static bool ValorVazio(short shtValor)
            {
                return shtValor.Equals((short)0);
            }

            public static bool ValorVazio(int intValor)
            {
                return intValor.Equals(0);
            }

            public static bool ValorVazio(long lngValor)
            {
                return lngValor.Equals((long)0L);
            }

            public static bool ValorVazio(object objValor)
            {
                switch (objValor.GetType().Name.ToString())
                {
                    case "String":
                        return ValorVazio(objValor.ToString());

                    case "DateTime":
                        return ValorVazio((DateTime)objValor);

                    case "Decimal":
                        return ValorVazio((decimal)objValor);

                    case "Int32":
                        return ValorVazio((int)objValor);

                    case "Int16":
                        return ValorVazio((short)objValor);

                    case "Int64":
                        return ValorVazio((long)objValor);

                    case "float":
                        return ValorVazio((float)objValor);

                    case "Double":
                        return ValorVazio((double)objValor);

                    case "Guid":
                        return ValorVazio((Guid)objValor);
                }
                return true;
            }

            public static bool ValorVazio(float fltValor)
            {
                return fltValor.Equals((float)0f);
            }

            public static bool ValorVazio(string strValor)
            {
                return string.IsNullOrEmpty(strValor);
            }

            #endregion
        }
    }
}
