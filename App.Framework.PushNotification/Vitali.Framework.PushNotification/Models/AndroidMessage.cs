using System.Collections.Generic;
using App.Framework.PushNotification.Models.Common;

namespace App.Framework.PushNotification.Models
{
    public class AndroidMessage : CommonMessage
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public AndroidMessage()
            :base(null, null, 1, 2)
        {
            this.DeviceTokens = new List<string>();
            this.Vibrate = new long[] { 500, 500, 500, 500 };
        }

        public AndroidMessage(string title, string message, IEnumerable<string> devicesId, object payload = null, string subTitle = null, string tickerText = null)
            :base(title, payload)
        {
            this.Title = title;
            this.Message = message;
            this.SubTitle = subTitle;
            this.TickerText = tickerText;
            this.Badge = 1;
            this.Sound = 2;
            this.Vibrate = new long[] { 500, 500, 500, 500 };

            this.DeviceTokens = devicesId;

            if (tickerText == null)
            {
                this.TickerText = this.Title;
            }
        }

        /// <summary>
        /// Devices for receive push notification
        /// </summary>
        public IEnumerable<string> DeviceTokens { get; set; }

        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Message { get; set; }
        public string TickerText { get; set; }
        public long[] Vibrate { get; set; }
    }
}
