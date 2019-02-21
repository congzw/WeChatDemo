using System.Web;
using System.Web.Mvc;

namespace CommonFx.Common.Web.Mvc
{
    public static class JsonExtensions
    {
        public static IHtmlString ToJsonRaw(this object value, bool indented = false)
        {
            return new MvcHtmlString(JsonHelper.Serialize(value, indented));
        }

        public static IHtmlString ToJsonRaw<T>(this T value, bool indented = false)
        {
            return new MvcHtmlString(JsonHelper.Serialize(value, indented));
        }

        public static string ToJson(this object value, bool indented = false)
        {
            return JsonHelper.Serialize(value, indented);
        }

        public static string ToJson<T>(this T value, bool indented = false)
        {
            return JsonHelper.Serialize(value, indented);
        }
    }
}
