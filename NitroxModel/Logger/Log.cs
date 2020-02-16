using System;
using System.Diagnostics;
using NLog;

namespace NitroxModel.Logger
{
    public static class Log
    {
        // Private variables
        private static readonly NLog.Logger logger = LogManager.GetCurrentClassLogger();
        private static InGameLogger inGameLogger;
        private static bool inGameMessagesEnabled = false;

        // Public API
        [Conditional("DEBUG")]
        public static void Trace(string message)
        {
            logger.Trace(message);
        }
        [Conditional("DEBUG")]
        public static void Debug(string message)
        {
            logger.Debug(message);
        }

        public static void Info(string message)
        {
            logger.Info(message);
        }

        public static void Warn(string message)
        {
            logger.Warn(message);
        }

        public static void Error(string message)
        {
            logger.Error(message);
        }

        public static void Fatal(string message)
        {
            logger.Fatal(message);
        }

        // Sensitive
        [Conditional("DEBUG")]
        public static void DebugSensitive(string message, params object[] args)
        {
            LogFullSensitiveInfo(message, args);
            Debug(message);
        }

        public static void InfoSensitive(string message, params object[] args)
        {
            LogFullSensitiveInfo(message, args);
            Info(message);
        }

        public static void ErrorSensitive(string message, params object[] args)
        {
            LogFullSensitiveInfo(message, args);
            Error(message);
        } 

        public static void Exception(string message, Exception ex)
        {
            logger.Error(ex, message);
        }

        // In game messages
        public static void ShowInGameMessage(string message, bool containsPersonalInfo = false)
        {
            if (inGameLogger == null)
            {
                logger.Warn("InGameLogger has not been registered");
                return;
            }

            if (!inGameMessagesEnabled)
            {
                logger.Warn("InGameMessages have not been enabled");
            }
            inGameLogger.Log(message);
            
            // Only log locally if it contains no personal information
            if (!containsPersonalInfo)
            {
                logger.Debug(message);
            }
            
        }

        public static void RegisterInGameLogger(InGameLogger gameLogger)
        {
            logger.Info("Registered InGameLogger");
            inGameLogger = gameLogger;
        }

        public static void SetInGameMessagesEnabled(bool enabled)
        {
            inGameMessagesEnabled = enabled;
        }

        // Private methods
        /// <summary>
        /// When we log with sensitive info we still want to see the info in the console log.
        /// </summary>
        private static void LogFullSensitiveInfo(string message, params object[] args)
        {
            logger.Trace(message, args);
        }
    }
}
