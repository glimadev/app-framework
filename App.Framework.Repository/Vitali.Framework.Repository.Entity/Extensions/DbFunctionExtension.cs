using System;
using System.Data.Entity;

namespace Vitali.Framework.Repository.Entity.Extensions
{
    public static class DbFunctionExtension
    {
        [DbFunction("SqlServer", "SOUNDEX")]
        public static string Soundex(this string input)
        {
            throw new NotImplementedException();
        }

        [DbFunction("CodeFirstDatabaseSchema", "SoundsLike")]
        public static string SoundsLike(this string input)
        {
            throw new NotImplementedException();
        }

        [DbFunction("CodeFirstDatabaseSchema", "GenerateSlug")]
        public static string GenerateSlug(string str)
        {
            throw new NotImplementedException();
        }
    }
}
