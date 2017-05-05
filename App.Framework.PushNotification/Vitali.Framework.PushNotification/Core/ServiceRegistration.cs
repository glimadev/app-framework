using System;

namespace Vitali.Framework.PushNotification.Core
{
	public class ServiceRegistration
	{
		public static ServiceRegistration Create<TNotification>(IPusherService service, string applicationId = null)
		{
			return new ServiceRegistration () {
				ApplicationId =  applicationId,
				Service = service,
				NotificationType = typeof(TNotification)
			};
		}

		public IPusherService Service { get;set; }
		public string ApplicationId { get;set; }
		public Type NotificationType { get;set; }
	}
}

