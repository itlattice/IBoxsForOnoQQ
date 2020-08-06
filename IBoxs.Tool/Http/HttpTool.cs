using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace IBoxs.Tool.Http
{
	/// <summary>
	/// Http 工具类
	/// </summary>
	public static class HttpTool
	{
		/// <summary>
		/// 使用默认编码对 URL 进行编码
		/// </summary>
		/// <param name="url">要编码的地址</param>
		/// <returns>编码后的地址</returns>
		public static string UrlEncode (string url)
		{
			return HttpUtility.UrlEncode (url);
		}
		
		/// <summary>
		/// 使用指定的编码 <see cref="Encoding"/> 对 URL 进行编码
		/// </summary>
		/// <param name="url">要编码的地址</param>
		/// <param name="encoding">编码类型</param>
		/// <returns>编码后的地址</returns>
		public static string UrlEncode (string url, Encoding encoding)
		{
			return HttpUtility.UrlEncode (url, encoding);
		}
		
		/// <summary>
		/// 使用默认编码对 URL 进行解码
		/// </summary>
		/// <param name="url">要解码的地址</param>
		/// <returns>编码后的地址</returns>
		public static string UrlDecode (string url)
		{
			return HttpUtility.UrlDecode (url);
		}
		
		/// <summary>
		/// 使用指定的编码 <see cref="Encoding"/> 对 URL 进行解码
		/// </summary>
		/// <param name="url">要解码的地址</param>
		/// <param name="encoding">编码类型</param>
		/// <returns>编码后的地址</returns>
		public static string UrlDecode (string url, Encoding encoding)
		{
			return HttpUtility.UrlDecode (url, encoding);
		}
        
        /// <summary>
        /// 检查网络是否连通
        /// </summary>
        /// <returns></returns>
        public static bool IsConnectInternet()
        {
            try
            {
                string url = "https://www.baidu.com";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "text/html;charset=UTF-8";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
                string a = retString;
                if (a.Length > 0)
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 发起GET请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <returns>返回内容</returns>
        public static string GET(string url)
        {
            if (!IsConnectInternet())
            {
                return null ;
            }
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }
        /// <summary>
        /// 获取网页源代码
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GETHTML(string url)
        {
            string htmlCode;
            HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            webRequest.Timeout = 30000;
            webRequest.Method = "GET";
            webRequest.UserAgent = "Mozilla/4.0";
            webRequest.Headers.Add("Accept-Encoding", "gzip, deflate");
            HttpWebResponse webResponse = (System.Net.HttpWebResponse)webRequest.GetResponse();
            if (webResponse.ContentEncoding.ToLower() == "gzip")//如果使用了GZip则先解压            {
                using (System.IO.Stream streamReceive = webResponse.GetResponseStream())
                {
                    using (var zipStream =
                        new System.IO.Compression.GZipStream(streamReceive, System.IO.Compression.CompressionMode.Decompress))
                    {
                        using (StreamReader sr = new System.IO.StreamReader(zipStream, Encoding.UTF8))
                        {
                            htmlCode = sr.ReadToEnd();
                        }
                    }
                }

            else
            {
                using (System.IO.Stream streamReceive = webResponse.GetResponseStream())
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(streamReceive, Encoding.UTF8))
                    {
                        htmlCode = sr.ReadToEnd();
                    }
                }
            }

            return htmlCode;
        }
        
        /// <summary>
        /// 带cookie发起POST请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="cc"></param>
        /// <returns></returns>
        public static string CPost(string url, string postData, CookieCollection cc)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/x-www-form-urlencoded";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";
            request.ProtocolVersion = HttpVersion.Version11;
            request.AllowAutoRedirect = true;
            request.ContentLength = byteArray.Length;
            request.CookieContainer.Add(cc);
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();
            return responseFromServer;
        }
        /// <summary>
        /// 发起post请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public static string Post(string url, string postData)
        {
            if (!IsConnectInternet())
            {
                return "";
            }
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/x-www-form-urlencoded";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";
            request.ProtocolVersion = HttpVersion.Version11;
            request.AllowAutoRedirect = true;
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();
            return responseFromServer;
        }
    }
}
