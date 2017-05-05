using System;

namespace Vitali.Framework.Extension
{
    public static class NullableIntExtension
    {
        public static bool IsValid(this Nullable<int> nullableInt)
        {
            return nullableInt.HasValue && nullableInt.Value != 0;
        }
    }
}
