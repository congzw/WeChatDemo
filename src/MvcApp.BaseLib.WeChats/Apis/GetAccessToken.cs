namespace CommonFx.BaseLib.WeChats.Apis
{
    public class GetAccessToken : WeChatApiResponse<GetAccessToken>
    {
        public string access_token { get; set; }
        public string expires_in { get; set; }
        public string refresh_token { get; set; }
        public string openid { get; set; }
        public string scope { get; set; }

        protected override string InitApiUriFormat()
        {
            return "https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code";
        }

        public static GetAccessToken CallGetAccessToken(string appid, string secret, string code)
        {
            var getAccessToken = new GetAccessToken();
            var apiUri = getAccessToken.CreateApiUri(appid, secret, code);
            var httpClient = CreateHttpClient(true);
            var weChatResponse = CallApi<GetAccessToken>(() => httpClient.GetAsync(apiUri));
            return weChatResponse;
        }
    }
}
