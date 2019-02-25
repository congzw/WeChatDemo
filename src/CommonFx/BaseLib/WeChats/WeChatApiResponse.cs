using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CommonFx.Common;

namespace CommonFx.BaseLib.WeChats
{
    public abstract class WeChatApiResponse<T> : WeChatApiRequest where T : WeChatApiResponse<T>, new()
    {
        /// <summary>
        /// 出错的结果
        /// </summary>
        public ErrorResult ErrorResult { get; set; }
        /// <summary>
        /// 非预期结果
        /// </summary>
        public UnexpectResult UnexpectResult { get; set; }

        /// <summary>
        /// 显示用的友好信息
        /// </summary>
        public string DisplayMessage { get; set; }
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success()
        {
            return ErrorResult == null && UnexpectResult == null;
        }

        public static T CreateResult(HttpResponseMessage responseMessage, string appendDesc = null)
        {
            var failAppendDesc = typeof(T).Name;
            if (responseMessage.IsSuccessStatusCode)
            {
                //{"errcode":40029,"errmsg":"invalid code, hints: [ req_id: xflAP0yFe-lted_ ]"}
                var apiJson = responseMessage.Content.ReadAsStringAsync().Result;
                if (apiJson.Contains("errcode") && apiJson.Contains("errmsg"))
                {
                    var errorResult = JsonHelper.Deserialize<ErrorResult>(apiJson);
                    return CreateErrorResult(errorResult, failAppendDesc);
                }
                //{...}
                return CreateSuccessResult(apiJson, failAppendDesc);
            }
            return CreateUnexpectResult(new UnexpectResult() { StatusCode = (int)responseMessage.StatusCode }, failAppendDesc);
        }
        public static T CreateSuccessResult(string json, string appendDesc = null)
        {
            var deserialize = JsonHelper.Deserialize<T>(json);
            deserialize.DisplayMessage = string.Format("微信接口调用成功，返回结果信息。{0}", appendDesc);
            return deserialize;
        }
        public static T CreateErrorResult(ErrorResult errorResult, string appendDesc = null)
        {
            return new T
            {
                ErrorResult = errorResult,
                DisplayMessage = string.Format("微信接口调用失败，返回出错信息。{0}", appendDesc)
            };
        }
        public static T CreateUnexpectResult(UnexpectResult unexpectResult, string appendDesc = null)
        {
            return new T
            {
                UnexpectResult = unexpectResult,
                DisplayMessage = string.Format("微信接口调用异常。{0}", appendDesc)
            };
        }
    }

    public class ErrorResult
    {
        public ErrorResult()
        {
            hints = new List<string>();
        }

        /// <summary>
        /// 返回代码
        /// </summary>
        public int errcode { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string errmsg { get; set; }
        /// <summary>
        /// 调试用信息
        /// </summary>
        public IList<string> hints { get; set; }
    }

    public class UnexpectResult
    {
        public int StatusCode { get; set; }
    }
}
