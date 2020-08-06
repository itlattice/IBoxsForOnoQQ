using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IBoxs.Tool.Tools
{
   public static class ToolsBox
    {
        /// <summary>
        /// 检查一个字符串是否为长整型
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static bool IsLong(string num)
        {
            try
            {
                long a = Convert.ToInt64(num);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 检测字符串是不是手机号
        /// </summary>
        /// <param name="tel"></param>
        /// <returns></returns>
        public static bool IsTel(string tel)
        {
            if (tel.Length != 11)
                return false;
            string a = tel.Substring(0, 5);
            string b = tel.Substring(tel.Length - 5, 5);
            if ((!IsInt(a)) || (!IsInt(b)))
            {
                return false;
            }
            List<string> t = new List<string>();
            t.Add("13"); t.Add("15"); t.Add("16"); t.Add("17"); t.Add("18"); t.Add("19");
            string c = tel.Substring(0, 2);
            if (t.Contains(c))
                return true;
            return false;
        }

        public static bool GetDotNetVersion(string version)
        {
            string oldname = "0";
            using (RegistryKey ndpKey =
                RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, "").
                OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\"))
            {
                foreach (string versionKeyName in ndpKey.GetSubKeyNames())
                {
                    if (versionKeyName.StartsWith("v"))
                    {
                        RegistryKey versionKey = ndpKey.OpenSubKey(versionKeyName);
                        string newname = (string)versionKey.GetValue("Version", "");
                        if (string.Compare(newname, oldname) > 0)
                        {
                            oldname = newname;
                        }
                        if (newname != "")
                        {
                            continue;
                        }
                        foreach (string subKeyName in versionKey.GetSubKeyNames())
                        {
                            RegistryKey subKey = versionKey.OpenSubKey(subKeyName);
                            newname = (string)subKey.GetValue("Version", "");
                            if (string.Compare(newname, oldname) > 0)
                            {
                                oldname = newname;
                            }
                        }
                    }
                }
            }
            return string.Compare(oldname, version) > 0 ? true : false;
        }
        /**************************************************/
        /// <summary>
        /// 去掉消息中的酷q代码
        /// </summary>
        /// <param name="msg">消息内容</param>
        /// <returns></returns>
        public static string msgDelCQP(string msg)
        {
            string bar_codes = msg;
            List<string> pro = new List<string>();
            MatchCollection match = Regex.Matches(bar_codes, @"\[CQ:([\s\S]*?)\]", RegexOptions.IgnoreCase);
            foreach (Match m in match)
            {
                pro.Add(m.Value);
            }
            if (pro.Count > 0)
            {
                for (int i = 0; i < pro.Count; i++)
                {
                    msg = msg.Replace(pro[i], "");
                }
            }
            msg = msg.Trim();
            return msg;
        }

        /// <summary>
        /// 判断字符串是不是邮箱
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsEmail(string input)
        {
            string pattern = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(input);
        }
        /// <summary>
        /// 对Url进行编码
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="isUpper">编码字符是否转成大写,范例,"http://"转成"http%3A%2F%2F"</param>
        public static string UrlEncode(string url, bool isUpper = false)
        {
            return System.Web.HttpUtility.UrlEncode(url);
        }

        /// <summary>
        /// 判断是否是数字
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsInt(string value)
        {
            try
            {
                double j = Convert.ToDouble(value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 判断是不是一个qq号
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool isQQ(string value)
        {
            try
            {
                if (value.Length < 5 || value.Length > 10)
                    return false;
                long qq = Convert.ToInt64(value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 将Unicode转为字符串
        /// </summary>
        /// <param name="srcText"></param>
        /// <returns></returns>
        public static string UnicodeToString(string srcText)
        {
            string de = Regex.Unescape(srcText);
            de = System.Web.HttpUtility.UrlEncode(de, Encoding.UTF8);
            return de;
        }

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static int GetTimestamp(DateTime d)
        {
            TimeSpan ts = d - new DateTime(1970, 1, 1, 8, 0, 0, 0);
            try
            {
                return Convert.ToInt32(ts.TotalSeconds);
            }
            catch
            {
                return 2047413647;
            }
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
        ///<summary>
        ///生成随机字符串 
        ///</summary>
        ///<param name="length">目标字符串的长度</param>
        ///<returns>指定长度的随机字符串</returns>
        public static string GetRandomString(int length)
        {
            byte[] b = new byte[4];
            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
            Random r = new Random(BitConverter.ToInt32(b, 0));
            string s = null, str = ""; str += "0123456789"; str += "abcdefghijklmnopqrstuvwxyz".ToUpper();
            for (int i = 0; i < length; i++)
            {
                s += str.Substring(r.Next(0, str.Length - 1), 1);
            }
            return s;
        }
    }
}
