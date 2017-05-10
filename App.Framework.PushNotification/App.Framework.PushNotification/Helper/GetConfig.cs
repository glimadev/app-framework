using App.Framework.Helper;

namespace App.Framework.PushNotification.Helper
{
    public static class GetConfig
    {
        public static class Certificate
        {
            public static string IOS = ConfigHelper.GetSetting("certificateIOS");
            public static string IOSPassword = ConfigHelper.GetSetting("passwordIOS");
        }

        public static class Key
        {
            public static string Android = ConfigHelper.GetSetting("androidKey");
        }        
    }
}
