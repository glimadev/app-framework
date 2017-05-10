using System;

namespace App.Framework.PushNotification.Core
{
	public class MaxSendAttemptsReachedException : Exception
	{
		public MaxSendAttemptsReachedException() : base("The maximum number of Send attempts to send the notification was reached!") { }
	}

	public class DeviceSubscriptonExpiredException : Exception
	{
		public DeviceSubscriptonExpiredException() : base("Device Subscription has Expired")
		{
		}
	}
}
