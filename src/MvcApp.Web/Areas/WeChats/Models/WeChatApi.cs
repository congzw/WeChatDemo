//using System;
//using System.Collections.Generic;
//using CommonFx.Common.Web.WebApi;

//namespace MvcApp.Web.Areas.WeChats.Models
//{
//    public class WeChatApi
//    {
//        public WeChatApi()
//        {
//            UriFormats = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
//        }

//        public Dictionary<string, string> UriFormats { get; set; }
        
//        public string AccessToken = "AccessToken";
//        public string AccessToken_UriFormat = "https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code";
//        public string AccessToken_CreateUri(string appid, string secret, string code)
//        {
//            return CreateUri(this, AccessToken, appid, secret, code);
//        }
//        public WeChatResult<AccessTokenDto> GetAccessToken(string appid, string secret, string code)
//        {
//            var httpClient = WebApiHelper.Create(true);
//            var apiUri = AccessToken_CreateUri(appid, secret, code);
//            var weChatResult = WeChatResult<AccessTokenDto>.CallApi<AccessTokenDto>(() => httpClient.GetAsync(apiUri));
//            return weChatResult;
//        }
        



//        #region static helpers for extensions
        
//        /// <summary>
//        /// singleton for static extensions
//        /// </summary>
//        public static WeChatApi Instance = new WeChatApi();
//        /// <summary>
//        /// 注册，如果没有注册过
//        /// </summary>
//        /// <param name="instance"></param>
//        /// <param name="apiKey"></param>
//        /// <param name="uriFormat"></param>
//        public static void RegisterUriFromatIfNecessary(WeChatApi instance, string apiKey, string uriFormat)
//        {
//            if (instance.UriFormats.ContainsKey(apiKey))
//            {
//                return;
//            }
//            instance.UriFormats[apiKey] = uriFormat;
//        }
//        /// <summary>
//        /// 填充合成Uri
//        /// </summary>
//        /// <param name="instance"></param>
//        /// <param name="apiKey"></param>
//        /// <param name="args"></param>
//        /// <returns></returns>
//        public static string CreateUri(WeChatApi instance, string apiKey, params object[] args)
//        {
//            return string.Format(instance.UriFormats[apiKey], args);
//        }

//        #endregion
//    }
//}