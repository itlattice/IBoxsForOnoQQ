using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBoxs.Sdk.Cqp.Model;
using System.Windows.Forms;

namespace IBoxs.Sdk.Cqp.Core.Handle
{
    class MemberListHandle
    {
        /// <summary>
        /// 获取群最大人数和当前人数
        /// </summary>
        /// <param name="json"></param>
        public static void GetGroupCur(string json,out int max,out int cur)
        {
            max = 0;
            cur = 0;
            Root rt = JsonConvert.DeserializeObject<Root>(json);
            if (rt.errcode != 0)
                return;
            max = rt.max_count;
            cur = rt.search_count;
        }

        public static List<Model.GroupMemberInfo> getMemberList(string json,long group)
        {
            Root rt = JsonConvert.DeserializeObject<Root>(json);
            if (rt.errcode != 0)
                return null;
            List<GroupMemberInfo> member = new List<GroupMemberInfo>();
            for (int i = 0, len = rt.mems.Count; i < len; i++)
            {
                GroupMemberInfo temp = new GroupMemberInfo();
                if (rt.mems.Count <= 0)
                {
                    return null;
                }
                temp.GroupId = group;
                temp.Age = 0;
                temp.Area = "";
                temp.BadRecord = false;
                temp.CanModifiedCard = false;
                temp.Card = rt.mems[i].card;
                temp.JoiningTime = KerMsg.GetDateTime(rt.mems[i].join_time);
                temp.LastDateTime =KerMsg.GetDateTime( rt.mems[i].last_speak_time);
                temp.Level = rt.mems[i].lv.level.ToString();
                temp.Nick = rt.mems[i].nick;
                int role = rt.mems[i].role;
                if (role == 0)
                    temp.PermitType = Enum.PermitType.Holder;
                else if (role == 1)
                    temp.PermitType = Enum.PermitType.Manage;
                else if (role == 2)
                    temp.PermitType = Enum.PermitType.None;
                else
                    temp.PermitType = Enum.PermitType.None;
                temp.QQId = rt.mems[i].uin;
                int s = rt.mems[i].g;
                if (s == 0)
                    temp.Sex = Enum.Sex.Man;
                else if (s == 1)
                    temp.Sex = Enum.Sex.Woman;
                else
                    temp.Sex = Enum.Sex.Unknown;
                temp.SpecialTitle = "";
                temp.SpecialTitleDurationTime = DateTime.Now;
                member.Add(temp);
            }
            return member;
        }

        public class Lv
        {
            /// <summary>
            /// 
            /// </summary>
            public int point { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int level { get; set; }
        }

        public class Mems
        {
            /// <summary>
            /// QQ号
            /// </summary>
            public long uin { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int role { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int g { get; set; }
            /// <summary>
            /// 进群时间
            /// </summary>
            public long join_time { get; set; }
            /// <summary>
            /// 最后发言时间
            /// </summary>
            public long last_speak_time { get; set; }
            /// <summary>
            /// 群内等级
            /// </summary>
            public Lv lv { get; set; }
            /// <summary>
            /// 群名片
            /// </summary>
            public string card { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string tags { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int flag { get; set; }
            /// <summary>
            /// 路上孤魂
            /// </summary>
            public string nick { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int qage { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int rm { get; set; }
        }

        public class Root
        {
            /// <summary>
            /// 
            /// </summary>
            public int ec { get; set; }
            /// <summary>
            /// 错误码
            /// </summary>
            public int errcode { get; set; }
            /// <summary>
            /// 错误信息
            /// </summary>
            public string em { get; set; }
            /// <summary>
            /// 缓存
            /// </summary>
            public int cache { get; set; }
            /// <summary>
            /// 管理员数量
            /// </summary>
            public int adm_num { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string levelname { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<Mems> mems { get; set; }
            /// <summary>
            /// 群成员数量
            /// </summary>
            public int count { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int svr_time { get; set; }
            /// <summary>
            /// 最大人数
            /// </summary>
            public int max_count { get; set; }
            /// <summary>
            /// 当前人数
            /// </summary>
            public int search_count { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int extmode { get; set; }
        }

    }
}
