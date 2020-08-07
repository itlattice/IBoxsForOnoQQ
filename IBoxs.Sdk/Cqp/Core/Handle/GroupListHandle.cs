using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBoxs.Sdk.Cqp.Model;
using System.Runtime.InteropServices;

namespace IBoxs.Sdk.Cqp.Core.Handle
{
    class GroupListHandle
    {
        public static List<Model.GroupInfo> getGroupList(string json,long robotQQ)
        {
            Root rt = JsonConvert.DeserializeObject<Root>(json);
            if (rt.errcode != 0)
                return null;
            List<GroupInfo> group = new List<GroupInfo>();
            for (int i = 0, len = rt.join.Count; i < len; i++)
            {
                GroupInfo temp = new GroupInfo();
                if (rt.join.Count <= 0)
                {
                    return null;
                }
                temp.Id = rt.join[i].gc;
                temp.Name = rt.join[i].gn;
                temp.owner = rt.join[i].owner.ToString();

                string js = Marshal.PtrToStringAnsi(CQP.Api_GetGroupMemberList(robotQQ.ToString(), temp.Id.ToString())).Trim();
                if (js.Length < 1)
                    return null;
                js = Cqp.Core.KerMsg.FromUnicodeString(js);
                int max = 0;
                int cur = 0;
                Core.Handle.MemberListHandle.GetGroupCur(js, out max, out cur);
                temp.CurrentNumber = cur;
                temp.MaximumNumber = max;
                temp.GroupLavel = 0;
                group.Add(temp);
            }
            return group;
        }


        public class Join
        {
            /// <summary>
            /// 
            /// </summary>
            public long gc { get; set; }
            /// <summary>
            /// 酷Q开发群
            /// </summary>
            public string gn { get; set; }
            /// <summary>
            /// 群主
            /// </summary>
            public long owner { get; set; }
        }

        public class Root
        {
            /// <summary>
            /// 
            /// </summary>
            public int ec { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int errcode { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string em { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<Join> join { get; set; }
        }
    }
}
