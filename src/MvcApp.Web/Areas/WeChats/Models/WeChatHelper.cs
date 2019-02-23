using System;
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

    //public class AccessTokenDto
    //{
    //    //{
    //    //    "access_token": "18_vY4GNn6Yk_gWI5Br-I3QQ7ST-AuVA-u7yN3vgMU1iHm2h5k8-VsRjFxAVUXyu29mQg37GZT77ShVoKRKaCCJdVOPzRDcTdmb6CMWciG3Sc8",
    //    //    "expires_in": 7200,
    //    //    "refresh_token": "18_NNlYhUlY83i4ML3gxCMwyj7b4rft8Fi1rS7lHhjNsVxkSUePT8H0Mkde_loqz8ZjTWHgmZ-8wwJt281J8MN3k-SSpMTOG7ynxU3ue4OXmOw",
    //    //    "openid": "oGiFG0wmNfczZfDxu1AKx1t9lork",
    //    //    "scope": "snsapi_userinfo"
    //    //}

    //    public string access_token { get; set; }
    //    public string expires_in { get; set; }
    //    public string refresh_token { get; set; }
    //    public string openid { get; set; }
    //    public string scope { get; set; }
    //}
}
