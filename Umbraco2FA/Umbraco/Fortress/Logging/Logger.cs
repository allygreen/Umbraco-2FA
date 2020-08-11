using log4net;
using System;

namespace Orc.Fortress.Logging
{
    public static class Logger
    {
        /// <summary>
        /// Method to list Error messages to the db
        /// </summary>
        /// <param name="type"></param>
        /// <param name="message"></param>
        /// <param name="error"></param>
        public static void Error(Type type, string message, Exception error)
        {
            var logger = LogManager.GetLogger(type);
            if (logger == null) return;
            logger.Error(message, error);
        }

        /// <summary>
        /// Method to list Info messages to the db
        /// </summary>
        /// <param name="type"></param>
        /// <param name="message"></param>
        public static void Info(Type type, string message)
        {
            var logger = LogManager.GetLogger(type);
            if (logger == null) return;
            logger.InfoFormat(message);
        }

        /// <summary>
        /// Method to list Debug messages to the db
        /// </summary>
        /// <param name="type"></param>
        /// <param name="message"></param>
        public static void Debug(Type type, string message)
        {
            var logger = LogManager.GetLogger(type);
            if (logger == null) return;
            logger.DebugFormat(message);
        }

    }
}
