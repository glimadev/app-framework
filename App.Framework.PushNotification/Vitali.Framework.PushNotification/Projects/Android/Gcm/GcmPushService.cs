using System;
using Vitali.Framework.PushNotification.Core;

namespace Vitali.Framework.PushNotification.Projects.Android
{
	public class GcmPushService : PushServiceBase
	{
		public GcmPushService(GcmPushChannelSettings channelSettings)
			: this(default(IPushChannelFactory), channelSettings, default(IPushServiceSettings))
		{
		}

		public GcmPushService(GcmPushChannelSettings channelSettings, IPushServiceSettings serviceSettings)
			: this(default(IPushChannelFactory), channelSettings, serviceSettings)
		{
		}

		public GcmPushService(IPushChannelFactory pushChannelFactory, GcmPushChannelSettings channelSettings)
			: this(pushChannelFactory, channelSettings, default(IPushServiceSettings))
		{
		}

		public GcmPushService(IPushChannelFactory pushChannelFactory, GcmPushChannelSettings channelSettings, IPushServiceSettings serviceSettings)
			: base(pushChannelFactory ?? new GcmPushChannelFactory(), channelSettings, serviceSettings)
		{
		}
	}

	public class GcmPushChannelFactory : IPushChannelFactory
	{
		public IPushChannel CreateChannel(IPushChannelSettings channelSettings)
		{
			if (!(channelSettings is GcmPushChannelSettings))
				throw new ArgumentException("channelSettings must be of type " + typeof(GcmPushChannelSettings).Name);

			return new GcmPushChannel(channelSettings as GcmPushChannelSettings);
		}
	}
}
