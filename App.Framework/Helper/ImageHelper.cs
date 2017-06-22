using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;

namespace App.Framework.Helper
{
    public static class ImageHelper
    {
        public static Image GetImageFromUrl(string imgUrl)
        {
            Image image;

            using (var wc = new WebClient())
            {
                using (var imgStream = new MemoryStream(wc.DownloadData(imgUrl)))
                {
                    using (var objImage = Image.FromStream(imgStream))
                    {
                        image = objImage;
                    }
                }
            }

            return image;
        }

        public static bool Crop(string folder, string fileName, int width = 250, int height = 250)
        {
            try
            {
                MemoryStream original = new MemoryStream(System.IO.File.ReadAllBytes(folder + fileName));

                MemoryStream result = new MemoryStream();

                Image image = Image.FromStream(original);

                var bwidth = width;

                var bheight = height;

                Bitmap thumbnail = new Bitmap(width, height);

                if (image.Width >= image.Height)
                {
                    height = Convert.ToInt32(Math.Round(Convert.ToDouble(image.Height) / Convert.ToDouble(image.Width) * Convert.ToDouble(width), 0));
                }
                else
                {
                    width = Convert.ToInt32(Math.Round(Convert.ToDouble(image.Width) / Convert.ToDouble(image.Height) * Convert.ToDouble(height), 0));
                }

                Graphics graphics = Graphics.FromImage(thumbnail);

                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.Clear(Color.White);
                graphics.DrawImage(image, (bwidth == width ? 0 : (bwidth / 2) - (width / 2)), (bheight == height ? 0 : (bheight / 2) - (height / 2)), width, height);


                ImageCodecInfo[] info = ImageCodecInfo.GetImageEncoders();
                EncoderParameters encoderParameters;
                encoderParameters = new EncoderParameters(1);
                encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 100L);

                thumbnail.Save(result, info[1], encoderParameters);

                result.Position = 0;

                // --> Gravar a imagem na folder
                if (!System.IO.Directory.Exists(folder))
                {
                    System.IO.Directory.CreateDirectory(folder);
                }

                thumbnail.Save(folder + fileName);

                return true;
            }
            catch (Exception ex)
            {
                // Get stack trace for the exception with source file information
                var st = new System.Diagnostics.StackTrace(ex, true);
                // Get the top stack frame
                var frame = st.GetFrame(0);
                // Get the line number from the stack frame
                var line = frame.GetFileLineNumber();

                string error = String.Format("Message: {1}\n Source: {0}\n Line: {2}\n StackTrace: {3}", ex.Source, ex.Message, line, ex.StackTrace);

                //using (var sw = new System.IO.StreamWriter()
                //{
                //    sw.WriteLine(error);
                //}
            }

            return false;
        }

        public static void Delete(string file, string[] excepts)
        {
            //evitar de deletar imagens padrões
            if (excepts == null)
            {
                throw new Exception("Informe as exeções");
            }

            if (!excepts.Contains(file) && File.Exists(file))
            {
                File.Delete(file);
            }
        }

        public static void Move(string srcFile, string dstFile)
        {
            File.Move(srcFile, dstFile);
        }
    }
}
