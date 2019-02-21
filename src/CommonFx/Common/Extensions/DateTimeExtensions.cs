using System;

namespace CommonFx.Common
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 格式化时间
        /// </summary>
        /// <param name="datetime"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToFormat(this DateTime datetime, string format = "yyyy-MM-dd HH:mm:ss")
        {
            return datetime.ToString(format);
        }

        /// <summary>
        /// 格式化日期
        /// </summary>
        /// <param name="datetime"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToDateFormat(this DateTime datetime, string format = "yyyy-MM-dd")
        {
            return datetime.ToString(format);
        }
    }
}
