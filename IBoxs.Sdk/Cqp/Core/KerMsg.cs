using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IBoxs.Sdk.Cqp.Core
{
    class KerMsg
    {
        /// <summary>
        /// 对返回值进行Unicode解码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FromUnicodeString(string str)
        {
            StringBuilder strResult = new StringBuilder();
            if (!string.IsNullOrEmpty(str))
            {
                string[] strlist = str.Replace("\\", "").Split('u');
                try
                {
                    for (int i = 1; i < strlist.Length; i++)
                    {
                        int charCode = Convert.ToInt32(strlist[i], 16);
                        strResult.Append((char)charCode);
                    }
                }
                catch
                {
                    return Regex.Unescape(str);
                }
            }
            string json= strResult.ToString();
            json = json.Replace("&nbsp;", " ");
            return json;
        }

        /// <summary>
        /// 时间戳转换为北京时间
        /// </summary>
        /// <param name="timeStamp">时间戳</param>
        /// <returns></returns>
        public static DateTime GetDateTime(long timeStamp)
        {
            DateTime dtStart = Convert.ToDateTime("1970-01-01 8:00:00");
            DateTime d = dtStart.AddSeconds(timeStamp);
            return d;
        }
    }
}
