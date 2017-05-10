using App.Framework.PushNotification.Core;

namespace App.Framework.PushNotification.Projects.Android
{
	public static class GcmPushBrokerExtensions
	{
		public static void RegisterGcmService(this PushBroker broker, GcmPushChannelSettings channelSettings, IPushServiceSettings serviceSettings = null)
		{
			RegisterGcmService (broker, channelSettings, null, serviceSettings);
		}

		public static void RegisterGcmService(this PushBroker broker, GcmPushChannelSettings channelSettings, string applicationId, IPushServiceSettings serviceSettings = null)
		{
			broker.RegisterService<GcmNotification>(new GcmPushService(new GcmPushChannelFactory(), channelSettings, serviceSettings), applicationId);
		}

		public static GcmNotification GcmNotification(this PushBroker broker)
		{
			return new GcmNotification();
		}
	}
}
