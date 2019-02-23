namespace MvcApp.Web.Areas.WeChats.Models.WeChatApis
{
    public class GetAccessToken : WeChatResult<GetAccessToken>
    {
        public string access_token { get; set; }
        public string expires_in { get; set; }
        public string refresh_token { get; set; }
        public string openid { get; set; }
        public string scope { get; set; }

        protected override string GetApiUriFormat()
        {
            return "https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code";
        }

        public WeChatResult<GetAccessToken> CallGetAccessToken(string appid, string secret, string code)
        {
            var httpClient = CreateHttpClient(true);
            var apiUri = CreateApiUri(appid, secret, code);
            var weChatResult = CallApi<GetAccessToken>(() => httpClient.GetAsync(apiUri));
            return weChatResult.Result;
        }
    }
}
