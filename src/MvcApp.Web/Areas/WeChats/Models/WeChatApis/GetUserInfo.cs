using System.Collections.Generic;

namespace MvcApp.Web.Areas.WeChats.Models.WeChatApis
{
    public class GetUserInfo : WeChatResult<GetUserInfo>
    {
        public GetUserInfo()
        {
            privilege = new List<string>();
        }

        public string openid { get; set; }
        public string nickname { get; set; }
        public string sex { get; set; }
        public string province { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string headimgurl { get; set; }
        public IList<string> privilege { get; set; }
        public string unionid { get; set; }

        protected override string GetApiUriFormat()
        {
            return "https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang=zh_CN";
        }
        
        public WeChatResult<GetUserInfo> CallGetUserInfo(string access_token, string openid)
        {
            var httpClient = CreateHttpClient(true);
            var apiUri = CreateApiUri(access_token, openid);
            var weChatResult = CallApi<GetUserInfo>(() => httpClient.GetAsync(apiUri));
            return weChatResult.Result;
        }
    }
}
