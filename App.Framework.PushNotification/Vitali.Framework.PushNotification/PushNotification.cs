using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Vitali.Framework.PushNotification.Core;
using Vitali.Framework.PushNotification.Helper;
using Vitali.Framework.PushNotification.Models;
using Vitali.Framework.PushNotification.Models.Common;
using Vitali.Framework.PushNotification.Projects.Android;
using Vitali.Framework.PushNotification.Projects.Ios;

namespace Vitali.Framework.PushNotification
{
    public static class PushNotification
    {
        private static PushBroker Pusher(ResultPush resultPush)
        {
            PushBroker push = new PushBroker(resultPush);

            push.OnNotificationSent += NotificationSent;
            push.OnChannelException += ChannelException;
            push.OnServiceException += ServiceException;
            push.OnNotificationFailed += NotificationFailed;
            push.OnDeviceSubscriptionExpired += DeviceSubscriptionExpired;
            push.OnDeviceSubscriptionChanged += DeviceSubscriptionChanged;
            push.OnChannelCreated += ChannelCreated;
            push.OnChannelDestroyed += ChannelDestroyed;

            return push;
        }

        private static JsonSerializerSettings SerializerSettings()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();

            settings.NullValueHandling = NullValueHandling.Ignore;
            settings.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            settings.Formatting = Formatting.Indented;

            return settings;
        }

        public static Task<ResultPush> AndroidAsync(AndroidMessage androidMessage)
        {
            return Task<ResultPush>.Factory.StartNew(() =>
            {
                return Android(androidMessage);
            });
        }

        public static ResultPush Android(AndroidMessage androidMessage)
        {
            ResultPush resultPush = new ResultPush();

            if (androidMessage != null)
            {
                PushBroker push = Pusher(resultPush);

                push.RegisterGcmService(new GcmPushChannelSettings(GetConfig.Key.Android));
                var jsonObject = JsonConvert.SerializeObject(androidMessage, SerializerSettings());

                push.QueueNotification(new GcmNotification()
                    .ForDeviceRegistrationId(androidMessage.DeviceTokens)
                    .WithJson(jsonObject));
            }

            return resultPush;
        }

        public static ResultPush Ios(IosMessage iosMessage)
        {
            ResultPush resultPush = new ResultPush();
            X509Certificate2 certificate = new X509Certificate2(File.ReadAllBytes(GetConfig.Certificate.IOS), GetConfig.Certificate.IOSPassword);

            var appleCert = File.ReadAllBytes(GetConfig.Certificate.IOS);
            PushBroker push = Pusher(resultPush);

            push.RegisterAppleService(new ApplePushChannelSettings(false, certificate));

            AppleNotificationPayload appleNotificationPayload = new AppleNotificationPayload(iosMessage.Alert, iosMessage.Badge.Value, iosMessage.Sound.ToString());

            var appleNotification = new AppleNotification()
                                       .ForDeviceToken(iosMessage.DeviceToken)
                                       .WithPayload(appleNotificationPayload);


            if (iosMessage.Sound.HasValue)
            {
                appleNotification.WithSound(iosMessage.Sound.ToString());
            }

            if (iosMessage.Badge.HasValue)
            {
                appleNotification.WithBadge(iosMessage.Badge.Value);
            }

            push.QueueNotification(appleNotification);

            return resultPush;
        }

        #region [ Callback Methods ]

        private static void DeviceSubscriptionChanged(object sender, string oldSubscriptionId, string newSubscriptionId, INotification notification)
        {
            //Currently this event will only ever happen for Android GCM
            Console.WriteLine("Device Registration Changed:  Old-> " + oldSubscriptionId + "  New-> " + newSubscriptionId + " -> " + notification);
        }

        public static void NotificationSent(object sender, INotification notification, ResultPush resultPush)
        {
            resultPush.CallbackSent(sender, notification);
            Console.WriteLine("Sent: " + sender + " -> " + notification);
        }

        public static void NotificationFailed(object sender, INotification notification, Exception notificationFailureException, ResultPush resultPush)
        {
            resultPush.CallbackFailed(sender, notification, notificationFailureException);
            Console.WriteLine("Failure: " + sender + " -> " + notificationFailureException.Message + " -> " + notification);
        }

        private static void ChannelException(object sender, IPushChannel channel, Exception exception)
        {
            Console.WriteLine("Channel Exception: " + sender + " -> " + exception);
        }

        private static void ServiceException(object sender, Exception exception)
        {
            Console.WriteLine("Service Exception: " + sender + " -> " + exception);
        }

        private static void DeviceSubscriptionExpired(object sender, string expiredDeviceSubscriptionId, DateTime timestamp, INotification notification)
        {
            Console.WriteLine("Device Subscription Expired: " + sender + " -> " + expiredDeviceSubscriptionId);
        }

        private static void ChannelDestroyed(object sender)
        {
            Console.WriteLine("Channel Destroyed for: " + sender);
        }

        private static void ChannelCreated(object sender, IPushChannel pushChannel)
        {
            Console.WriteLine("Channel Created for: " + sender);
        }

        #endregion
    }
}
