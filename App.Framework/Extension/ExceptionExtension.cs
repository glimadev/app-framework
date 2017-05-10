using System;

namespace App.Framework.Extension
{
    public static class ExceptionExtension
    {
        public static string GetErrorMessage(this Exception ex)
        {
            return ex.InnerException == null ? ex.Message : ex.InnerException.Message;
        }
    }
}
