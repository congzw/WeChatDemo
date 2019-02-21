using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web;

namespace CommonFx.Common.Web
{
    public interface IRequestContextHelper
    {
        /// <summary>
        /// 获取当期的请求上下文
        /// </summary>
        /// <returns></returns>
        HttpContextBase GetHttpContext();

        /// <summary>
        /// 尝试从当前获取键的值
        /// 检索不到则返回""
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string TryGetFromRequestParams(string key);

        /// <summary>
        /// args -> query
        /// </summary>
        /// <param name="args"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        string ObjectToQueryString(object args, string separator = ",");

        /// <summary>
        /// query -> set args
        /// </summary>
        /// <param name="query"></param>
        /// <param name="args"></param>
        /// <param name="separator"></param>
        /// <param name="throwEx"></param>
        void QueryStringToObject(string query, object args, string separator = ",", bool throwEx = false);

        /// <summary>
        /// Scheme://主机名或 IP 地址和端口号 => (Scheme + Authority)
        /// </summary>
        /// <param name="currentRequestUri"></param>
        /// <returns></returns>
        string GetCurrentBaseRequestUrl(Uri currentRequestUri = null);
    }

    public class RequestContextHelper : IRequestContextHelper
    {
        #region for di extensions

        private static Func<IRequestContextHelper> _resolve = () => ResolveAsSingleton.Resolve<RequestContextHelper, IRequestContextHelper>();
        /// <summary>
        /// 支持动态替换（ResolveAsSingleton）
        /// </summary>
        /// <returns></returns>
        public static Func<IRequestContextHelper> Resolve
        {
            get { return _resolve; }
            set { _resolve = value; }
        }

        #endregion

        public HttpContextBase GetHttpContext()
        {
            if (HttpContext.Current == null)
            {
                return null;
            }
            return new HttpContextWrapper(HttpContext.Current);
        }

        public string TryGetFromRequestParams(string key)
        {
            var httpContextBase = GetHttpContext();
            if (httpContextBase != null)
            {
                var value = httpContextBase.Request.Params[key];
                return value;
            }
            return null;
        }

        public string ObjectToQueryString(object args, string separator = ",")
        {
            if (args == null)
                throw new ArgumentNullException("obj");

            if (args.GetType().IsPrimitive)
            {
                //简单类型
                throw new ArgumentException("不支持简单类型");
            }

            // Get all properties on the object
            var properties = args.GetType().GetProperties()
                .Where(x => x.CanRead)
                //.Where(x => x.GetValue(obj, null) != null)
                .ToDictionary(x => x.Name, x => x.GetValue(args, null));

            // Get names for all IEnumerable properties (excl. string)
            var propertyNames = properties
                .Where(x => !(x.Value is string) && x.Value is IEnumerable)
                .Select(x => x.Key)
                .ToList();

            // Concat all IEnumerable properties into a comma separated string
            foreach (var key in propertyNames)
            {
                var valueType = properties[key].GetType();
                var valueElemType = valueType.IsGenericType
                                        ? valueType.GetGenericArguments()[0]
                                        : valueType.GetElementType();
                if (valueElemType.IsPrimitive || valueElemType == typeof(string))
                {
                    var enumerable = properties[key] as IEnumerable;
                    properties[key] = string.Join(separator, enumerable.Cast<object>());
                }
            }

            // Concat all key/value pairs into a string separated by ampersand
            return string.Join("&", properties
                .Select(x => string.Concat(
                    Uri.EscapeDataString(x.Key), "=",
                    Uri.EscapeDataString(x.Value == null ? string.Empty : x.Value.ToString()))));
        }

        public void QueryStringToObject(string query, object args, string separator = ",", bool throwEx = false)
        {
            //todo suppport separator!
            if (string.IsNullOrWhiteSpace(query))
            {
                return;
            }
            //fix PathAdnQuery
            var indexOfMark = query.IndexOfAny(new[] { '?' });
            if (indexOfMark >= 0)
            {
                query = query.Substring(indexOfMark, query.Length - indexOfMark);
            }

            var dict = HttpUtility.ParseQueryString(query);
            Parse(dict, args, separator, throwEx);
        }

        public string GetCurrentBaseRequestUrl(Uri currentRequestUri = null)
        {
            if (currentRequestUri == null)
            {
                currentRequestUri = GetCurrentRequestUri();
            }
            return "{0}://{1}".FormatAs(currentRequestUri.Scheme, currentRequestUri.Authority);
        }

        //helpers
        private object Parse(string valueToConvert, Type dataType, string separator)
        {
            if (string.IsNullOrWhiteSpace(valueToConvert))
            {
                return null;
            }

            TypeConverter converter = TypeDescriptor.GetConverter(dataType);
            var canConvertFrom = converter.CanConvertFrom(typeof(string));
            if (canConvertFrom)
            {
                object value = converter.ConvertFromString(null, CultureInfo.InvariantCulture, valueToConvert);
                return value;
            }

            //"X,Y,Z..."
            var values = valueToConvert.Split(new string[] { separator }, StringSplitOptions.None) as IEnumerable;
            return values;
        }

        private void Parse(NameValueCollection dict, object args, string separator, bool throwEx)
        {
            var properties = args.GetType().GetProperties();
            foreach (var property in properties)
            {
                var valueAsString = dict[property.Name];
                try
                {
                    var value = Parse(valueAsString, property.PropertyType, separator);
                    if (value == null)
                        continue;
                    property.SetValue(args, value, null);
                }
                catch (Exception)
                {
                    if (throwEx)
                    {
                        throw;
                    }
                }
            }
        }
        private Uri GetCurrentRequestUri()
        {
            var httpContextBase = GetHttpContext();
            if (httpContextBase == null)
            {
                return null;
            }
            return httpContextBase.Request.Url;
        }
    }
}
