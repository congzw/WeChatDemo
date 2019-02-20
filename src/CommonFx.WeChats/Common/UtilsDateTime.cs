using System;

namespace CommonFx.Common
{
    public class UtilsDateTime
    {
        #region for di extensions

        private static Func<DateTime> _resolve = () => DateTime.Now;
        public static Func<DateTime> GetTime
        {
            get { return _resolve; }
            set { _resolve = value; }
        }

        #endregion

        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}