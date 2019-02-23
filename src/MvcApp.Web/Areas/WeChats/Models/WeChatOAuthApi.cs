using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CommonFx.Common;
using CommonFx.Common.Web.WebApi;
using MvcApp.Web.Areas.WeChats.Models.WeChatApis;

namespace MvcApp.Web.Areas.WeChats.Models
{
    public class WeChatOAuthApi
    {
        //doc => https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421140842
        public static string UriFormat_Authorize = "https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope={2}&state={3}#wechat_redirect";
        public static string UriFormat_AccessToken = "https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code";
        public static string UriFormat_RefreshToken = "https://api.weixin.qq.com/sns/oauth2/refresh_token?appid={0}&grant_type=refresh_token&refresh_token={1}";
        public static string UriFormat_UserInfo = "https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang=zh_CN";

        public string CreateOAuth2AuthorizeUri(string appid, string redirectUri, string scope, string state)
        {
            return string.Format(UriFormat_Authorize, appid, redirectUri, scope, state);
        }

        public string CreateOAuth2TokenUri(string appid, string secret, string code)
        {
            return string.Format(UriFormat_AccessToken, appid, secret, code);
        }

        public WeChatResult<GetAccessToken> GetAccessToken(string appid, string secret, string code)
        {
            var getAccessToken = new GetAccessToken();
            var callGetAccessToken = getAccessToken.CallGetAccessToken(appid, secret, code);
            return callGetAccessToken;
        }

        public dynamic GetCurrentUser(string token, string tokenType)
        {
            var getUrl = string.Format("https://api.github.com/user?access_token={0}&token_type={1}", WebUtility.UrlEncode(token), WebUtility.UrlEncode(tokenType));
            var result = WebApiHelper.GetAsJson<dynamic>(getUrl);
            return result;
        }

    }
}