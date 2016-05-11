using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Configuration;
using NHH.Framework.Utility;


namespace NHH.Framework.Wechat
{
    public class WechatPush
    {
        /// <summary>
        /// 发送微信模板
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        public bool SendWechat(requestTemplate template)
        {
            var access_token = GetAccessToken();
            try
            {
                var url = ConfigurationManager.AppSettings["wechat_api_url"];
                // 构建URL内容
                ////   https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=ACCESS_TOKEN
                var reqUrl = string.Format("{0}message/template/send?access_token={1}", url, access_token);

                // 创建网络请求  
                HttpWebRequest request = WebRequest.Create(reqUrl) as HttpWebRequest;

                // 构建Head
                request.Method = "POST";
                Encoding myEncoding = Encoding.GetEncoding("utf-8");
                request.Accept = "application/json";
                request.ContentType = "application/json;charset=utf-8";


                byte[] byteData = UTF8Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(template));

                // 开始请求
                using (Stream postStream = request.GetRequestStream())
                {
                    postStream.Write(byteData, 0, byteData.Length);
                }

                // 获取请求
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    string responseStr = reader.ReadToEnd();

                    if (responseStr != null && responseStr.Length > 0)
                    {
                        //出现问题的地方
                        var resp = JsonConvert.DeserializeObject<responseData>(responseStr);
                        if (resp != null && resp.errcode == "0")
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;//抛异常
            }
            return false;
        }


        /// <summary>
        /// 获取微信的 access_token--从配置中获取appId和appSecrect
        /// </summary>
        /// <returns></returns>
        private string GetAccessToken()
        {
            var cacheManager = Caching.CacheManager.Default;
            string access_token = cacheManager.Get<string>("Wechat_AccessToken");
            if (!string.IsNullOrEmpty(access_token) && access_token.Length > 0)
            {
                return access_token;
            }

            var url = ConfigurationManager.AppSettings["wechat_api_url"];
            var appid = ConfigurationManager.AppSettings["wechat_appid"];
            var appSecret = ConfigurationManager.AppSettings["wechat_secret"];
            // 构建URL内容
            var reqUrl = string.Format("{0}token?grant_type=client_credential&appid={1}&secret={2}", url, appid, appSecret);

            // 创建网络请求  
            HttpWebRequest request = WebRequest.Create(reqUrl) as HttpWebRequest;
            request.Method = "GET";
            request.Accept = "application/json";
            request.ContentType = "application/json;charset=utf-8";
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string responseStr = reader.ReadToEnd();

                if (responseStr != null && responseStr.Length > 0)
                {
                    var resp = JsonConvert.DeserializeObject<responseRequest>(responseStr);
                    access_token = resp.access_token;
                }
            }

            if (access_token.Length > 0)
            {
                cacheManager.SlidingExpiration = new TimeSpan(1, 58, 58);
                cacheManager.Set<string>("Wechat_AccessToken", access_token);
            }
            return access_token;
        }

        /// <summary>
        ///从数据库中获取appId和appSecrect
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private string GetAccessToken(configData data)
        {
            var cacheManager = Caching.CacheManager.Default;
            string access_token = cacheManager.Get<string>("Wechat_AccessToken");
            if (!string.IsNullOrEmpty(access_token) && access_token.Length > 0)
            {
                return access_token;
            }

            var url = ConfigurationManager.AppSettings["wechat_api_url"];
            // 构建URL内容
            var reqUrl = string.Format("{0}token?grant_type=client_credential&appid={1}&secret={2}", url, data.AppId, data.AppSecrect);

            // 创建网络请求  
            HttpWebRequest request = WebRequest.Create(reqUrl) as HttpWebRequest;
            request.Method = "GET";
            request.Accept = "application/json";
            request.ContentType = "application/json;charset=utf-8";
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string responseStr = reader.ReadToEnd();

                if (responseStr != null && responseStr.Length > 0)
                {
                    var resp = JsonConvert.DeserializeObject<responseRequest>(responseStr);
                    access_token = resp.access_token;
                }
            }

            if (access_token.Length > 0)
            {
                cacheManager.SlidingExpiration = new TimeSpan(1, 58, 58);
                cacheManager.Set<string>("Wechat_AccessToken", access_token);
            }
            return access_token;
        }

        /// <summary>
        /// 上传图片并且获取MediaId
        /// </summary>
        /// <param name="info"></param>
        /// <param name="fileUrl"></param>
        /// <returns></returns>
        public string UploadPicture(configData info, string fileUrl)
        {
            var access_token = GetAccessToken(info);
            var type = "image";
            var url = ConfigurationManager.AppSettings["wechat_file_url"];

            // 构建URL内容
            ///http://file.api.weixin.qq.com/cgi-bin/media/upload?access_token=ACCESS_TOKEN&type=TYPE
            var reqUrl = string.Format("{0}media/upload?access_token={1}&type={2}", url, access_token, type);

            WebClient myWebClient = new WebClient();
            myWebClient.Credentials = CredentialCache.DefaultCredentials;
            try
            {
                //下载图片到指定位置
                var fileDirectory = AppDomain.CurrentDomain.BaseDirectory + @"Images\";
                if (!Directory.Exists(fileDirectory))
                {
                    Directory.CreateDirectory(fileDirectory);
                }
                var path = string.Format("{0}{1}{2}", fileDirectory, DateTime.Now.ToString("yyyyMMddHHmmss-"), Path.GetFileName(fileUrl));
                myWebClient.DownloadFile(fileUrl, path);

                //上传图片
                byte[] responseArray = myWebClient.UploadFile(reqUrl, "POST", path);

                //删除指定位置的图片
                if (File.Exists(path)) File.Delete(path);

                var result = System.Text.Encoding.UTF8.GetString(responseArray, 0, responseArray.Length);

                fileResponseData _mode = JsonConvert.DeserializeObject<fileResponseData>(result);
                return _mode.media_id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 设置图文消息
        /// </summary>
        /// <param name="config">开发者帐号</param>
        /// <param name="articles">内容</param>
        /// <returns></returns>
        public string UploadNews(configData config, string articles)
        {
            var access_token = GetAccessToken(config);
            try
            {
                var url = ConfigurationManager.AppSettings["wechat_api_url"];
                // 构建URL内容
                ///https://api.weixin.qq.com/cgi-bin/media/uploadnews?access_token=ACCESS_TOKEN
                var reqUrl = string.Format("{0}media/uploadnews?access_token={1}", url, access_token);

                // 创建网络请求  
                HttpWebRequest request = WebRequest.Create(reqUrl) as HttpWebRequest;

                // 构建Head
                request.Method = "POST";
                Encoding myEncoding = Encoding.GetEncoding("utf-8");
                request.Accept = "application/json";
                request.ContentType = "application/json;charset=utf-8";

                byte[] byteData = UTF8Encoding.UTF8.GetBytes(articles);

                // 开始请求
                using (Stream postStream = request.GetRequestStream())
                {
                    postStream.Write(byteData, 0, byteData.Length);
                }

                // 获取请求
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    string responseStr = reader.ReadToEnd();

                    if (responseStr != null && responseStr.Length > 0)
                    {
                        //出现问题的地方
                        var _mode = JsonConvert.DeserializeObject<fileResponseData>(responseStr);
                        return _mode.media_id;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;//抛异常
            }
            return null;
        }

        /// <summary>
        /// 消息群发
        /// </summary>
        /// <param name="config">开发者帐号</param>
        /// <param name="articles">群发内容</param>
        /// <returns></returns>
        public bool SendAll(configData config, string articles)
        {
            var access_token = GetAccessToken(config);
            try
            {
                var url = ConfigurationManager.AppSettings["wechat_api_url"];
                // 构建URL内容
                ////https://api.weixin.qq.com/cgi-bin/message/mass/sendall?access_token=ACCESS_TOKEN
                var reqUrl = string.Format("{0}message/mass/sendall?access_token={1}", url, access_token);

                // 创建网络请求  
                HttpWebRequest request = WebRequest.Create(reqUrl) as HttpWebRequest;

                // 构建Head
                request.Method = "POST";
                Encoding myEncoding = Encoding.GetEncoding("utf-8");
                request.Accept = "application/json";
                request.ContentType = "application/json;charset=utf-8";

                byte[] byteData = UTF8Encoding.UTF8.GetBytes(articles);

                // 开始请求
                using (Stream postStream = request.GetRequestStream())
                {
                    postStream.Write(byteData, 0, byteData.Length);
                }

                // 获取请求
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    string responseStr = reader.ReadToEnd();

                    if (responseStr != null && responseStr.Length > 0)
                    {
                        //出现问题的地方
                        var resp = JsonConvert.DeserializeObject<responseData>(responseStr);
                        if (resp != null && resp.errcode == "0")
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;//抛异常
            }
            return false;
        }
    }
}
