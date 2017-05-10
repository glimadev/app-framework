using System;

namespace App.Framework.Extension
{
    public static class NullableIntExtension
    {
        public static bool IsValid(this Nullable<int> nullableInt)
        {
            return nullableInt.HasValue && nullableInt.Value != 0;
        }
    }
}
