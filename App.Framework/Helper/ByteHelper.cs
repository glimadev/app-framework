using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace App.Framework.Helper
{
    public static class ByteHelper
    {
        public static byte[] ImageToByteArray(Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, ImageFormat.Jpeg);

                return ms.ToArray();
            }
        }
    }
}
