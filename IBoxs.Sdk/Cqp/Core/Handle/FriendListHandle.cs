using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBoxs.Sdk.Cqp.Model;
using LitJson;
using System.Collections;

namespace IBoxs.Sdk.Cqp.Core.Handle
{
    class FriendListHandle
    {
        public static List<Model.FriendInfo> getFriends(string json)
        {
            if (json.Length < 5)
                return null;
            if (!json.Contains("{\"ec\":0,\""))
                return null;
            for (int i = 0; i < 10; i++)
            {
                json = json.Replace("\""+i.ToString()+"\":", "");
            }
            json = json.Replace("}}}", "}}");
            Root rt = JsonConvert.DeserializeObject<Root>(json);
            if (rt.errcode != 0)
                return null;
            List<FriendInfo> friends = new List<FriendInfo>();
            for (int i = 0, len = rt.result.mems.Count; i < len; i++)
            {
                FriendInfo temp = new FriendInfo();
                if (rt.result.mems.Count <= 0)
                {
                    return null;
                }
                temp.Id = rt.result.mems[i].uin;
                temp.Nick = rt.result.mems[i].name;
                friends.Add(temp);
            }
            return friends;
        }

        public class MemsItem
        {
            /// <summary>
            /// 昵称
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// QQ
            /// </summary>
            public string uin { get; set; }
        }

        public class Result
        {
            /// <summary>
            /// 列表信息
            /// </summary>
            public List<MemsItem> mems { get; set; }
        }

        public class Root
        {
            /// <summary>
            /// 错误代码
            /// </summary>
            public int errcode { get; set; }
            /// <summary>
            /// 结果
            /// </summary>
            public Result result { get; set; }
        }
    }
}
