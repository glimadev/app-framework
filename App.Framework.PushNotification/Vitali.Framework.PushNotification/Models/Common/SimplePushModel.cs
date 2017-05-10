using System.Collections.Generic;

namespace App.Framework.PushNotification.Models.Common
{
    public class SimplePushModel
    {
        public object Payload { get; set; }
        public List<string> DeviceTokens { get; set; }
        public TypePushNotification TypeNotification { get; set; }
    }
}
