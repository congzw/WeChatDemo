using Newtonsoft.Json;

namespace CommonFx.Common
{
    public class JsonHelper
    {
        public static string Serialize(object value, bool indented = true)
        {
            var formatting = indented ? Formatting.Indented : Formatting.None;
            var serializeObject = JsonConvert.SerializeObject(value, formatting);
            return serializeObject;
        }

        public static object Deserialize(string content)
        {
            return JsonConvert.DeserializeObject(content);
        }

        public static T Deserialize<T>(string content)
        {
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
