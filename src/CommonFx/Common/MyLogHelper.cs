using System;

namespace CommonFx.Common
{
    public interface IMyLogHelper
    {
        void Log(object message, LogLevel logLevel = LogLevel.Debug);
    }

    public enum LogLevel
    {
        /// <summary>
        /// Debug
        /// </summary>
        Debug = 1,
        /// <summary>
        /// Info
        /// </summary>
        Info = 2,
        /// <summary>
        /// Warn
        /// </summary>
        Warn = 3,
        /// <summary>
        /// Error
        /// </summary>
        Error = 4,
        /// <summary>
        /// Fatal
        /// </summary>
        Fatal = 5
    }

    public class MyLogHelper : IMyLogHelper
    {
        #region for di extensions

        private static Func<Type, IMyLogHelper> _resolve = t => new MyLogHelper() { ForType = t };
        public static Func<Type, IMyLogHelper> Resolve
        {
            get { return _resolve; }
            set { _resolve = value; }
        }

        #endregion

        public Type ForType { get; set; }

        public void Log(object message, LogLevel logLevel = LogLevel.Debug)
        {
            //todo filter by config level
            var typeValue = ForType == null ? "Common" : ForType.FullName;
            var messageValue = message == null ? "" : message.ToString();
            System.Diagnostics.Trace.WriteLine(string.Format("[{0}][{1}] => {2}", logLevel, typeValue, messageValue));
        }
    }
}
