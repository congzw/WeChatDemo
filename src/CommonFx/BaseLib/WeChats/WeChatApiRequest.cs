using System;
using System.Net.Http;
using CommonFx.Common;
using CommonFx.Common.Web.WebApi;
using System.Threading.Tasks;

namespace CommonFx.BaseLib.WeChats
{
    public abstract class WeChatApiRequest
    {
        protected WeChatApiRequest()
        {
            ApiKey = GetType().Name;
            ApiUriFormat = InitApiUriFormat();
        }

        public string ApiKey { get; set; }
        public string ApiUriFormat { get; set; }
        public string CreateApiUri(params object[] args)
        {
            return string.Format(ApiUriFormat, args);
        }

        protected abstract string InitApiUriFormat();

        public HttpClient CreateHttpClient(bool https = true)
        {
            return WebApiHelper.Create(https);
        }

        public WeChatApiResponse<TApiResult> CallApi<TApiResult>(Func<Task<HttpResponseMessage>> apiFunc) where TApiResult : new()
        {
            var failAppendDesc = typeof(TApiResult).Name;
            var responseMessage = apiFunc().Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                //{"errcode":40029,"errmsg":"invalid code, hints: [ req_id: xflAP0yFe-lted_ ]"}
                var apiJson = responseMessage.Content.ReadAsStringAsync().Result;
                if (apiJson.Contains("errcode") && apiJson.Contains("errmsg"))
                {
                    var errorResult = JsonHelper.Deserialize<ErrorResult>(apiJson);
                    return WeChatApiResponse<TApiResult>.CreateErrorResult(errorResult, failAppendDesc);
                }
                //{...}
                var weChatResult = JsonHelper.Deserialize<TApiResult>(apiJson);
                return WeChatApiResponse<TApiResult>.CreateSuccessResult(weChatResult, failAppendDesc);
            }
            return WeChatApiResponse<TApiResult>.CreateUnexpectResult(new UnexpectResult() { StatusCode = (int)responseMessage.StatusCode }, failAppendDesc);
        }
    }
}
