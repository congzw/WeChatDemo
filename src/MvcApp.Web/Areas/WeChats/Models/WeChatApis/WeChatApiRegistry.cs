using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MvcApp.Web.Areas.WeChats.Models.WeChatApis
{
    public class WeChatApiRegistry
    {
        public WeChatApiRegistry()
        {
            WeChatCommands = new Dictionary<Type, WeChatApiEntry>();
        }

        public IDictionary<Type, WeChatApiEntry> WeChatCommands { get; set; }
        public void RegisterIfNecessary(WeChatCommand command)
        {
            var type = command.GetType();
            if (WeChatCommands.ContainsKey(type))
            {
                return;
            }
            WeChatCommands[type] = new WeChatApiEntry(type, command.ApiUriFormat);
        }
        public void Init(IList<Assembly> assemblies)
        {
            if (assemblies == null)
            {
                throw new ArgumentNullException("assemblies");
            }
            var baseClass = typeof (WeChatCommand);
            var types = assemblies
                .SelectMany(s => s.GetTypes())
                .Where(p => baseClass.IsAssignableFrom(p) && !p.IsGenericType && !p.IsAbstract && !p.IsInterface)
                .ToArray();

            foreach (var type in types)
            {
                if (WeChatCommands.ContainsKey(type))
                {
                    return;
                }
                var command = (WeChatCommand)Activator.CreateInstance(type);
                WeChatCommands[type] = new WeChatApiEntry(type, command.ApiUriFormat);
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
