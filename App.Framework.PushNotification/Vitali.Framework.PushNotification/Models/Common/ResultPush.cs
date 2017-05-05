using Newtonsoft.Json;
using System;
using Vitali.Framework.PushNotification.Core;
using Vitali.Framework.PushNotification.Projects.Android;

namespace Vitali.Framework.PushNotification.Models.Common
{
    public class ResultPush
    {
        public ResultPush()
        {
            this.ErrorMessage = null;
        }
        public bool Success
        {
            get
            {
                return String.IsNullOrWhiteSpace(ErrorMessage);
            }
        }
        public string ErrorMessage { get; set; }

        #region [ Callback Framework ]

        internal delegate void NotificationSentDelegate(object sender, INotification notification);
        internal delegate void NotificationFailedDelegate(object sender, INotification notification, Exception error);
        internal event NotificationSentDelegate OnNotificationSent;
        internal event NotificationFailedDelegate OnNotificationFailed;
        internal void CallbackSent(object sender, INotification notification)
        {
            OnNotificationSent += NotificationSent;
            OnNotificationSent.Invoke(sender, notification);
        }
        internal void CallbackFailed(object sender, INotification notification, Exception error)
        {
            OnNotificationFailed += NotificationFailed;
            OnNotificationFailed.Invoke(sender, notification, error);
        }

        #endregion

        #region [ Callback Client ]

        public delegate void NotificationSentCallback(object sender, ResultPushData resultPushData);
        public event NotificationSentCallback OnNotificationSentCallback;

        public delegate void NotificationFailedCallback(object sender, Exception error, ResultPushData resultPushData);
        public event NotificationFailedCallback OnNotificationFailedCallback;

        #endregion
        
        private void NotificationSent(object sender, INotification notification)
        {
            if (notification.GetType() == new GcmNotification().GetType())
            {
                GcmNotification gcmNotification = (GcmNotification)notification;
                var payLoad = JsonConvert.DeserializeObject<dynamic>(gcmNotification.GetJson());
                ResultPushData resultPushData = new ResultPushData(payLoad);

                if (OnNotificationSentCallback != null)
                {
                    NotificationSentCallback notificationSentCallback = new NotificationSentCallback(OnNotificationSentCallback);
                    notificationSentCallback.Invoke(sender, resultPushData);
                }
            }
        }

        private void NotificationFailed(object sender, INotification notification, Exception error)
        {
            if (notification.GetType() == new GcmNotification().GetType())
            {
                GcmNotification gcmNotification = (GcmNotification)notification;
                var payLoad = JsonConvert.DeserializeObject<dynamic>(gcmNotification.GetJson());
                ResultPushData resultPushData = new ResultPushData(payLoad);

                if (OnNotificationFailedCallback != null)
                {
                    NotificationFailedCallback notificationFailedCallback = new NotificationFailedCallback(OnNotificationFailedCallback);
                    notificationFailedCallback.Invoke(sender, error, resultPushData);
                }
            }
        }
    }
}
