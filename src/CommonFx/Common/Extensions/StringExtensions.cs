using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonFx.Common
{
    public static class StringExtensions
    {
        /// <summary>
        /// [A,B] => "A,B"
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string JoinToString<T>(this IEnumerable<T> values, string separator = ",")
        {
            if (values == null)
            {
                return string.Empty;
            }
            return string.Join(separator, values);
        }
        
        /// <summary>
        /// "A,B" => [A,B]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static IList<string> NbSplit(this string values, char separator = ',')
        {
            var list = new List<string>();
            if (string.IsNullOrWhiteSpace(values))
            {
                return list;
            }

            list = values.Split(separator).ToList();
            return list;
        }
        
        /// <summary>
        /// 宽松的字符串比较，如果双方为空或null都视为相等
        /// </summary>
        /// <param name="value"></param>
        /// <param name="value2"></param>
        /// <param name="stringComparison"></param>
        /// <returns></returns>
        public static bool NbEquals(this string value, string value2, StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.IsNullOrWhiteSpace(value2);
            }

            if (string.IsNullOrWhiteSpace(value2))
            {
                return string.IsNullOrWhiteSpace(value);
            }

            return value.Trim().Equals(value2.Trim(), stringComparison);
        }

        public static bool NbContains(this string source, string toCheck, StringComparison comp = StringComparison.OrdinalIgnoreCase)
        {
            if (source == null)
            {
                return false;
            }
            return source.IndexOf(toCheck, comp) >= 0;
        }

        /// <summary>
        /// 截取等宽中英文字符串
        /// </summary>
        /// <param name="str">要截取的字符串</param>
        /// <param name="length">要截取的中文字符长度</param>
        /// <param name="appendStr">截取后后追加的字符串</param>
        /// <returns>截取后的字符串</returns>
        public static string CutStr(this string str, int length, string appendStr = "")
        {
            if (str == null) return string.Empty;

            var len = length * 2;
            int aequilateLength = 0, cutLength = 0;
            var encoding = Encoding.GetEncoding("gb2312");

            var cutStr = str;
            var strLength = cutStr.Length;
            byte[] bytes;
            for (int i = 0; i < strLength; i++)
            {
                bytes = encoding.GetBytes(cutStr.Substring(i, 1));
                if (bytes.Length >= 2)//不是英文
                    aequilateLength += 2;
                else
                    aequilateLength++;

                if (aequilateLength <= len) cutLength += 1;

                if (aequilateLength > len)
                    return cutStr.Substring(0, cutLength) + appendStr;
            }
            return cutStr;
        }
        
        public static string CutStrDefault(this string str)
        {
            //todo read from config
            int length = 20;
            string appendStr = "...";
            return CutStr(str, length, appendStr);
        }
        public static string CutStrShort(this string str)
        {
            //todo read from config
            int length = 10;
            string appendStr = "...";
            return CutStr(str, length, appendStr);
        }
        public static string CutStrLong(this string str)
        {
            //todo read from config
            int length = 100;
            string appendStr = "...";
            return CutStr(str, length, appendStr);
        }

        /// <summary>
        /// IsNullOrWhiteSpace
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static string DefaultWith(this string value, string defaultValue = "未指定")
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return defaultValue;
            }
            return value;
        }

        /// <summary>
        /// Format
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string FormatAs(this string format, params object[] args)
        {
            return string.Format(format, args);
        }

        /// <summary>
        /// 检测字符串长度
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static bool LengthShouldBetween(this string value, int min, int max = -1)
        {
            if (value == null)
            {
                return 0 >= min;
            }
            return value.Length >= min && (value.Length <= max || max < 0);
        }

        /// <summary>
        /// 检测字符串长度
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static bool LengthNotBetween(this string value, int min, int max = -1)
        {
            return !LengthShouldBetween(value, min, max);
        }
    }
}
