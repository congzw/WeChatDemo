using System.Linq;
using CommonFx.Common;
using CommonFx.Common.AppData;

namespace MvcApp.Web.Areas.WeChats.Models
{
    public class WeChatHelper
    {
        public static WeChatConfig GetWeChatConfig()
        {
            var myAppPath = MyAppPath.Resolve();
            var appData = myAppPath.AppData;
            var typeFilePathHelper = TypeFilePathHelper.Resolve();
            var filePath = typeFilePathHelper.AutoGuessTypeFilePath(appData, typeof(WeChatConfig));
            var fileDbHelper = FileDbHelper.Resolve();
            var weChatConfigs = fileDbHelper.Read<WeChatConfig>(filePath);
            return weChatConfigs.FirstOrDefault();
        }
    }
}
