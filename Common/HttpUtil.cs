using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;

namespace Common
{
    public static class HttpUtil
    {
        public static string BaseUrl = "http://junhong.goho.co:8889";

        /// <summary>
        /// 创建请求
        /// </summary>
        /// <param name="url">接口路径</param>
        /// <param name="webHeader">请求头</param>
        /// <param name="nameValue">参数</param>
        public static string CreateRequest(string url, WebHeaderCollection webHeader, NameValueCollection nameValue)
        {
            WebClient webClient = new WebClient();
            webClient.Headers = webHeader;
            webClient.Encoding = System.Text.UTF8Encoding.UTF8;
            webClient.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";

            var ret = webClient.UploadValues(BaseUrl + url, nameValue);

            return System.Text.Encoding.UTF8.GetString(ret);
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="machineCode">机器码</param>
        /// <returns></returns>
        public static BaseResponse Login(string userName, string password, string machineCode)
        {
            WebHeaderCollection webHeader = new WebHeaderCollection();
            webHeader.Add("_username", userName);
            webHeader.Add("_pwd", Util.GetMd5Hash(password));

            NameValueCollection nameValue = new NameValueCollection();
            nameValue.Add("_appKey", machineCode);
            nameValue.Add("_ts", Util.TryDateTimeToLong(DateTime.Now).ToString());

            var ret = CreateRequest("/API/Login", webHeader, nameValue);

            return JsonConvert.DeserializeObject<BaseResponse>(ret);
        }

        /// <summary>
        /// 自建库查询
        /// </summary>
        /// <param name="ss">ss号</param>
        /// <param name="token">登陆token</param>
        /// <returns></returns>
        public static BaseResponse UpdateSource(string ss, string token)
        {
            WebHeaderCollection webHeader = new WebHeaderCollection();
            webHeader.Add("ss", ss);
            webHeader.Add("_token", token);

            var ret = CreateRequest("/API/UpdateScore", webHeader, null);

            return JsonConvert.DeserializeObject<BaseResponse>(ret);
        }
    }
}
