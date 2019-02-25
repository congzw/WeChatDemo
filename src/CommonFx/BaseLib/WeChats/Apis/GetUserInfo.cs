using System.Collections.Generic;

namespace CommonFx.BaseLib.WeChats.Apis
{
    public class GetUserInfo : WeChatApiRequest
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

        public WeChatApiResponse<GetUserInfo> CallGetUserInfo(string access_token, string openid)
        {
            var apiUri = CreateApiUri(access_token, openid);
            var httpClient = CreateHttpClient(true);
            var weChatResult = CallApi<GetUserInfo>(() => httpClient.GetAsync(apiUri));
            //if (weChatResult.Success())
            //{
            //    //todo try copy props
            //    this = weChatResult.SuccessResult;
            //}
            return weChatResult;
        }

        protected override string InitApiUriFormat()
        {
            return "https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang=zh_CN";
        }
    }
}
