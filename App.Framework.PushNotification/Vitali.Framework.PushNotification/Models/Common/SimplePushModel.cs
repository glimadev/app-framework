using System.Collections.Generic;

namespace Vitali.Framework.PushNotification.Models.Common
{
    public class SimplePushModel
    {
        public object Payload { get; set; }
        public List<string> DeviceTokens { get; set; }
        public TypePushNotification TypeNotification { get; set; }
    }
}
