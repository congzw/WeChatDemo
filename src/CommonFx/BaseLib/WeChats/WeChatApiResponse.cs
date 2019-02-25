using System.Collections.Generic;

namespace CommonFx.BaseLib.WeChats
{
    public class WeChatApiResponse<T>
    {
        /// <summary>
        /// 正确的结果
        /// </summary>
        public T SuccessResult { get; set; }
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


        public static WeChatApiResponse<T> CreateSuccessResult(T successResult, string appendDesc = null)
        {
            return new WeChatApiResponse<T>()
            {
                SuccessResult = successResult,
                DisplayMessage = string.Format("微信接口调用成功，返回结果信息。{0}", appendDesc)
            };
        }
        public static WeChatApiResponse<T> CreateErrorResult(ErrorResult errorResult, string appendDesc = null)
        {
            return new WeChatApiResponse<T>()
            {
                ErrorResult = errorResult,
                DisplayMessage = string.Format("微信接口调用失败，返回出错信息。{0}", appendDesc)
            };
        }
        public static WeChatApiResponse<T> CreateUnexpectResult(UnexpectResult unexpectResult, string appendDesc = null)
        {
            return new WeChatApiResponse<T>()
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
