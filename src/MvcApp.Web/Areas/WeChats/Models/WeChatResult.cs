using System;
using System.Net.Http;
using System.Threading.Tasks;
using CommonFx.Common;
using CommonFx.Common.Web.WebApi;

namespace MvcApp.Web.Areas.WeChats.Models
{
    /// <summary>
    /// 微信返回Json结果
    /// 正确时：{"errcode":0,"errmsg":"ok"}
    /// 错误时：{"errcode":40018,"errmsg":"invalid button name size"}
    /// </summary>
    public class WeChatResult<T> : WeChatCommand where T : new()
    {
        /// <summary>
        /// 返回代码
        /// </summary>
        public int errcode { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string errmsg { get; set; }

        /// <summary>
        /// 调用微信Api异常
        /// </summary>
        public bool CallApiException { get; set; }

        /// <summary>
        /// 是否调用成功
        /// </summary>
        /// <returns></returns>
        public bool Success()
        {
            return errcode == 0 && !CallApiException;
        }

        /// <summary>
        /// 显示用的友好信息
        /// </summary>
        public string DisplayMessage { get; set; }

        /// <summary>
        /// 包装的结果DTO
        /// </summary>
        public T Result { get; set; }
    }

    public abstract class WeChatCommand
    {
        protected WeChatCommand()
        {
            ApiKey = GetType().Name;
        }
        public string ApiKey { get; set; }
        public string ApiUriFormat { get; set; }
        public string CreateApiUri(params object[] args)
        {
            return string.Format(ApiUriFormat, args);
        }
        
        #region static helpers

        public static HttpClient CreateHttpClient(bool https = true)
        {
            return WebApiHelper.Create(https);
        }
        public static WeChatResult<TApiResult> CreateCallApiExceptionResult<TApiResult>() where TApiResult : new()
        {
            var message = string.Format("[{0}] 微信接口调用发生异常。", typeof(TApiResult).Name);
            return new WeChatResult<TApiResult>() { CallApiException = true, DisplayMessage = message };
        }
        public static WeChatResult<TApiResult> CallApi<TApiResult>(Func<Task<HttpResponseMessage>> apiFunc) where TApiResult : new()
        {
            var failAppendDesc = typeof(TApiResult).Name;
            var responseMessage = apiFunc().Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                //{"errcode":40029,"errmsg":"invalid code, hints: [ req_id: xflAP0yFe-lted_ ]"}
                var apiJson = responseMessage.Content.ReadAsStringAsync().Result;
                var weChatResult = JsonHelper.Deserialize<WeChatResult<TApiResult>>(apiJson);
                if (weChatResult.Success())
                {
                    weChatResult.DisplayMessage = string.Format("[{0}] 微信接口调用失败，返回提示信息。", failAppendDesc);
                }
                else
                {
                    weChatResult.DisplayMessage = string.Format("[{0}] 微信接口调用成功，返回结果。", failAppendDesc);
                }
                return weChatResult;
            }
            return CreateCallApiExceptionResult<TApiResult>();
        }

        #endregion
    }
}
