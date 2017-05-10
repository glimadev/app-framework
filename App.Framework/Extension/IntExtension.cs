
namespace App.Framework.Extension
{
    public static class IntExtension
    {
        public static bool IsZero(this int integer)
        {
            return integer == 0;
        }

        public static int? TryConvert(this string input)
        {
            int result = 0;

            int.TryParse(input, out result);

            return (result == 0) ? new int?() : result;
        }
    }
}
