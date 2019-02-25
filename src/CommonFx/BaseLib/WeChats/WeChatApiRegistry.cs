using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CommonFx.BaseLib.WeChats
{
    public class WeChatApiRegistry
    {
        public WeChatApiRegistry()
        {
            WeChatApis = new Dictionary<Type, WeChatApiEntry>();
        }

        public IDictionary<Type, WeChatApiEntry> WeChatApis { get; set; }
        public void RegisterIfNecessary(WeChatApiRequest request)
        {
            var type = request.GetType();
            if (WeChatApis.ContainsKey(type))
            {
                return;
            }
            WeChatApis[type] = new WeChatApiEntry(type, request.ApiUriFormat);
        }
        public void Init(IList<Assembly> assemblies)
        {
            if (assemblies == null)
            {
                throw new ArgumentNullException("assemblies");
            }
            var baseClass = typeof(WeChatApiRequest);
            var types = assemblies
                .SelectMany(s => s.GetTypes())
                .Where(p => baseClass.IsAssignableFrom(p) && !p.IsGenericType && !p.IsAbstract && !p.IsInterface)
                .ToArray();

            foreach (var type in types)
            {
                if (WeChatApis.ContainsKey(type))
                {
                    return;
                }
                var request = (WeChatApiRequest)Activator.CreateInstance(type);
                WeChatApis[type] = new WeChatApiEntry(type, request.ApiUriFormat);
            }
        }

        public static WeChatApiRegistry Instance = new WeChatApiRegistry();
    }

    public class WeChatApiEntry
    {
        public Type WeChatApiType { get; set; }
        public string WeChatApiUriFormat { get; set; }

        public WeChatApiEntry(Type type, string apiUriFormat)
        {
            WeChatApiType = type;
            WeChatApiUriFormat = apiUriFormat;
        }
    }
}
