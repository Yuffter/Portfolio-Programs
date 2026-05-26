using UnityEngine;

namespace Logger
{
    public static class LoggerManager
    {
        [System.Diagnostics.Conditional("ENABLE_LOG")]
        public static void Log(object message, GameObject context = null)
        {
            Debug.Log(message, context);
        }

        [System.Diagnostics.Conditional("ENABLE_LOG")]
        public static void LogWarning(object message, GameObject context = null)
        {
            Debug.LogWarning(message, context);
        }

        [System.Diagnostics.Conditional("ENABLE_LOG")]
        public static void LogError(object message, GameObject context = null)
        {
            Debug.LogError(message, context);
        }
    }
}
