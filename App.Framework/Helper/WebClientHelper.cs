using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace App.Framework.Helper
{
    public class WebClientHelper
    {
        public static async Task<string> DownloadImageAsync(string url, string destination)
        {
            return await AsyncHelper.AsyncFunction<string>(() =>
            {
                string name = string.Format("{0}.jpg", Guid.NewGuid().ToString());

                string location = string.Format("{0}{1}", destination, name);

                using (WebClient webClient = new WebClient())
                {
                    byte[] data = webClient.DownloadData(url);

                    using (MemoryStream mem = new MemoryStream(data))
                    {
                        using (var image = Image.FromStream(mem))
                        {
                            image.Save(@location, ImageFormat.Jpeg);
                        }
                    }
                }

                return name;
            });
        }

        public static string GetIPAddress()
        {
            #region [ Ip ]

            var host = Dns.GetHostEntry(Dns.GetHostName());

            string Ip = "";

            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    Ip = ip.ToString();
                }
            }

            #endregion

            return Ip;
        }
    }
}
