using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace IBoxs.Sdk.Cqp.Core.Handle
{
    class IBoxsMemberList
    {
        public static string GetFriendJson(long robotQQ)
        {
            string url = "https://qun.qq.com/cgi-bin/qun_mgr/get_friend_list";
            string data = Marshal.PtrToStringAnsi(CQP.Api_GetCookies(robotQQ.ToString())) + Marshal.PtrToStringAnsi(CQP.Api_GetGroupPsKey(robotQQ.ToString()));
            data = data.Trim().Replace(" ","").Trim();
            
            if (data.Length < 1)
            {
                return "";
            }
            string[] ck = data.Split(';');
            Uri u = new Uri(url);
            CookieContainer cookie = new CookieContainer();
            long bkn = 1000;
            for (int i = 0; i < ck.Length; i++)
            {
                string key = ck[i].Split('=')[0];
                if (key.Trim() == "skey")
                    bkn = GetBkn(ck[i].Split('=')[1]);
                string value = ck[i].Split('=')[1];
                Cookie ckie = new Cookie(key, value);
                cookie.Add(u, ckie);
            }
            data = "bkn=" + bkn.ToString();
            MessageBox.Show(url + data + cookie.ToString());
            string result = IBoxs.Tool.Http.HttpTool.CPost(url, data,cookie);
            return data;
        }
        
        public static long GetBkn(string skey)
        {
            var hash = 5381;
            for (int i = 0, len = skey.Length; i < len; ++i)
            {
                hash += (hash << 5) + (int)skey[i];
            }
            return hash & 2147483647;
        }
    }
}
