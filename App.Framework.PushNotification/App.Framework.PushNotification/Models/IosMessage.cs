using App.Framework.PushNotification.Models.Common;

namespace App.Framework.PushNotification.Models
{
    public class IosMessage : CommonMessage
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public IosMessage()
        {
            this.DeviceToken = null;
        }

        public IosMessage(string alert, object payload = null, string deviceToken = null)
            :base(alert, payload)
        {
            this.DeviceToken = null;
        }

        /// <summary>
        /// Device id for push
        /// </summary>
        public string DeviceToken { get; set; }
    }
}
