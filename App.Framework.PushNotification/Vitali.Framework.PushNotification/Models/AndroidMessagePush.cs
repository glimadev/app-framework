
namespace App.Framework.PushNotification.Models
{
    public class AndroidMessagePush
    {
        public AndroidMessagePush(AndroidMessage androidMessage)
        {
            this.title = androidMessage.Title;
            this.subtitle = androidMessage.SubTitle;
            this.message = androidMessage.Message;
            this.tickerText = androidMessage.TickerText;
            this.badge = androidMessage.Badge;
            this.sound = androidMessage.Sound;
            this.vibrate = androidMessage.Vibrate;
            this.payload = androidMessage.Payload;
        }

        public string title { get; set; }
        public string subtitle { get; set; }
        public string message { get; set; }
        public string tickerText { get; set; }
        public int? badge { get; set; }
        public int? sound { get; set; }
        public long[] vibrate { get; set; }
        public object payload { get; set; }
    }
}
