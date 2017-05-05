
namespace Vitali.Framework.PushNotification.Models.Common
{
    public abstract class CommonMessage
    {
        public CommonMessage(string alert, object payload = null, int? badge = null, int? sound = null)
        {
            this.Alert = null;
            this.Payload = payload;
            this.Badge = (badge.HasValue) ? badge : 1;
            this.Sound = null;
        }

        public CommonMessage()
        {
            this.Alert = null;
            this.Payload = null;
            this.Badge = 1;
            this.Sound = null;
        }

        /// <summary>
        /// Device message on receive
        /// </summary>
        public string Alert { get; set; }
        /// <summary>
        /// Count of notifications
        /// </summary>
        public int? Badge { get; set; }
        /// <summary>
        /// Sound of notification
        /// </summary>
        public int? Sound { get; set; }
        /// <summary>
        /// Object to send on body
        /// </summary>
        public object Payload { get; set; }
    }
}
