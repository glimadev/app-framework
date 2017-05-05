
namespace Vitali.Framework.PushNotification.Models
{
    public class IosMessagePush
    {
        public IosMessagePush(IosMessage iosMessage)
        {
            this.badge = iosMessage.Badge;
            this.alert = iosMessage.Alert;
            this.sound = iosMessage.Sound;
        }

        public string alert { get; set; }
        public int? badge { get; set; }
        public int? sound { get; set; }
    }
}
