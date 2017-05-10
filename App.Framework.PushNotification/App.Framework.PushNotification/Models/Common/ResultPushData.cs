using System.Linq;

namespace App.Framework.PushNotification.Models.Common
{
    public class ResultPushData
    {
        public ResultPushData(dynamic payload)
        {
            this.DeviceTokens = payload.registration_ids.ToObject<string[]>();

            if (payload.data != null)
            {
                this.Message = payload.data.message;
                this.Payload = payload.data.payload;
            }
        }

        private string[] DeviceTokens { get; set; }
        public string DeviceToken { get { return DeviceTokens.FirstOrDefault(); } }
        public string Message { get; set; }
        public object Payload { get; set; }
    }
}
