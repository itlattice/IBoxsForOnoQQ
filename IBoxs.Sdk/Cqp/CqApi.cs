using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using IBoxs.Sdk.Cqp.Model;

namespace IBoxs.Sdk.Cqp
{
    /// <summary>
    /// 酷Q Api封装类
    /// </summary>
    public class CqApi
    {
        # region 消息
        /// <summary>
        /// 发送私聊消息
        /// </summary>
        /// <param name="RobotQQ">机器人QQ</param>
        /// <param name="qqId">对方QQ</param>
        /// <param name="message">消息内容</param>
        /// <param name="Type">消息类型（1为普通，2为匿名）</param>
        /// <param name="Bubble">气泡ID（-1为随机）</param>
        /// <returns></returns>
        public string SendPrivateMessage(long RobotQQ, long qqId, string message, int Type = 1, int Bubble = -1)
        {
            string c = Marshal.PtrToStringAnsi(CQP.Api_SendMsg(RobotQQ.ToString(), 1, "", qqId.ToString(), message, Bubble, Type));
            return c;
        }

        /// <summary>
        /// 发送群消息
        /// </summary>
        /// <param name="groupId">目标群</param>
        /// <param name="message">消息内容</param>
        /// <returns>失败返回负值, 成功返回消息 Id</returns>
        public string SendGroupMessage(long RobotQQ, long groupId, string message, int Type = 1, int Bubble = -1)
        {
            string c = Marshal.PtrToStringAnsi(CQP.Api_SendMsg(RobotQQ.ToString(), 2, groupId.ToString(), String.Empty, message, Bubble, Type));
            return c;
        }

        /// <summary>
        /// 发送讨论组消息
        /// </summary>
        /// <param name="discussId">目标讨论组</param>
        /// <param name="message">消息内容</param>
        /// <returns></returns>
        public string SendDiscussMessage(long RobotQQ, long discussId, string message, int Type = 1, int Bubble = -1)
        {
            string c = Marshal.PtrToStringAnsi(CQP.Api_SendMsg(RobotQQ.ToString(), 3, discussId.ToString(), String.Empty, message, Bubble, Type));
            return c;
        }

        /// <summary>
        /// 发送群私聊消息
        /// </summary>
        /// <param name="RobotQQ">机器人QQ</param>
        /// <param name="groupId">所在群号</param>
        /// <param name="qqId">QQ号</param>
        /// <param name="message">消息内容</param>
        /// <param name="Type">消息类型（1为普通，2为匿名）</param>
        /// <param name="Bubble">气泡ID（-1为随机）</param>
        /// <returns></returns>
        public string SendDiscussMessage(long RobotQQ, long groupId,long qqId, string message, int Type = 1, int Bubble = -1)
        {
            string c = Marshal.PtrToStringAnsi(CQP.Api_SendMsg(RobotQQ.ToString(), 4, groupId.ToString(), qqId.ToString(), message, Bubble, Type));
            return c;
        }

        /// <summary>
        /// 发送赞
        /// </summary>
        /// <param name="qqId">目标QQ</param>
        /// <param name="count">赞的次数，最多10次（留空为1次）</param>
        /// <returns></returns>
        public string SendPraise(string RobotQQ, string qqId, int count = 1)
        {
            if (count < 1)
            {
                count = 1;
            }
            if (count > 10)
            {
                count = 10;
            }
            for (int i = 0; i < count; i++)
                Marshal.PtrToStringAnsi(CQP.Api_UpVote(RobotQQ, qqId));
            return String.Empty;
        }

        #endregion

        #region --Ono码--
        /// <summary>
        /// 获取酷Q "At某人" 代码
        /// </summary>
        /// <param name="qqId">QQ号, 填写 -1 为At全体成员</param>
        /// <param name="addSpacing">默认为True, At后添加空格, 可使At更规范美观. 如果不需要添加空格, 请置本参数为False</param>
        /// <returns></returns>
        public string CqCode_At(long qqId = -1, bool addSpacing = true)
        {
            if (qqId == -1)
            {
                return "[@all]";
            }
            else
            {
                return string.Format("[@{0}]", qqId.ToString());
            }
        }
        /// <summary>
        /// 获取酷Q "emoji表情" 代码
        /// </summary>
        /// <param name="id">表情Id</param>
        /// <returns></returns>
        public string CqCode_Emoji(int id)
        {
            return string.Format("[Face{0}.gif]", id);
        }
        /// <summary>
        /// 获取酷Q "表情" 代码
        /// </summary>
        /// <param name="face">表情枚举</param>
        /// <returns></returns>
        public string CqCode_Face(int id)
        {
            return string.Format("[Face{0}.gif]", id);
        }
        /// <summary>
        /// 获取字符串的转义形式
        /// </summary>
        /// <param name="str">欲转义字符串</param>
        /// <param name="commaTrope">逗号转义, 默认: False</param>
        /// <returns></returns>
        public string CqCode_Trope(string str, bool commaTrope = false)
        {
            StringBuilder @string = new StringBuilder(str);
            @string = @string.Replace("&", "&amp;");
            @string = @string.Replace("[", "&#91;");
            @string = @string.Replace("]", "&#93;");
            if (commaTrope)
            {
                @string = @string.Replace(",", "&#44;");
            }
            return @string.ToString();
        }
        /// <summary>
        /// 获取字符串的非转义形式
        /// </summary>
        /// <param name="str">欲反转义字符串</param>
        /// <returns></returns>
        public string CqCode_UnTrope(string str)
        {
            StringBuilder @string = new StringBuilder(str);
            @string = @string.Replace("&#91;", "[");
            @string = @string.Replace("&#93;", "]");
            @string = @string.Replace("&#44;", ",");
            @string = @string.Replace("&amp;", "&");
            return @string.ToString();
        }
        /// <summary>
        /// 获取酷Q "图片" 代码
        /// </summary>
        /// <param name="filePath">图片路径
        /// <para>将图片放在 data\image 下，并填写相对路径。如 data\image\1.jpg 则填写 1.jpg</para></param>
        ///<param name = "destory">是否为闪图</param>
        /// <returns></returns>
        public string CqCode_Image(string filePath, bool destory = false)
        {
            if (destory)
            {
                return string.Format("[FlashPic={0}]", CqCode_Trope(filePath, true));
            }
            return string.Format("[pic={0}]", CqCode_Trope(filePath, true));
        }
        #endregion

        #region --框架--
        /// <summary>
        /// 取在线QQ
        /// </summary>
        /// <returns>返回整数</returns>
        public List<long> GetLoginQQ()
        {
            string qq = Marshal.PtrToStringAnsi(CQP.Api_GetQQList()).Trim();
            if (qq == null)
                return (new List<long>());
            if (qq.Length < 1)
                return (new List<long>());
            string[] qqstr = qq.Split('\n');
            List<long> qqlist = new List<long>();
            for (int i = 0; i < qqstr.Length; i++)
            {
                string temp = qqstr[i].Trim();
                if (IBoxs.Tool.Tools.ToolsBox.isQQ(temp))
                {
                    long qtemp = Convert.ToInt64(temp);
                    if (!qqlist.Contains(qtemp))
                        qqlist.Add(qtemp);
                }
            }
            return qqlist;
        }
        /// <summary>
        /// 取对象昵称
        /// </summary>
        /// <param name="robotQQ">机器人QQ</param>
        /// <param name="qq">对象QQ</param>
        /// <returns></returns>
        public string GetQQNick(string robotQQ, string qq)
        {
            return Marshal.PtrToStringAnsi(CQP.Api_GetNick(robotQQ, qq));
        }
        /// <summary>
        /// 获取好友列表
        /// </summary>
        /// <param name="robotQQ"></param>
        /// <returns></returns>
        public List<FriendInfo> GetFriendsList(string robotQQ)
        {
            string json = Marshal.PtrToStringAnsi(CQP.Api_GetFriendList(robotQQ));
            json = Cqp.Core.KerMsg.FromUnicodeString(json);
            return Core.Handle.FriendListHandle.getFriends(json);
        }
        /// <summary>
        /// 获取群列表（含群名称）
        /// </summary>
        /// <param name="robotQQ"></param>
        /// <returns></returns>
        public List<GroupInfo> GetGroupList(long robotQQ)
        {
            string json = Marshal.PtrToStringAnsi(CQP.Api_GetGroupList(robotQQ.ToString()));
            if (json == null || json == string.Empty)
                return null;
            json = Cqp.Core.KerMsg.FromUnicodeString(json);
            return Core.Handle.GroupListHandle.getGroupList(json, robotQQ);
        }
        /// <summary>
        /// 获取群列表（仅有群号）
        /// </summary>
        /// <param name="robotQQ">机器人QQ</param>
        /// <returns></returns>
        public List<long> GetGroupIdList(long robotQQ)
        {
            string result = Marshal.PtrToStringAnsi(CQP.Api_GetGroupList_A(robotQQ.ToString()));
            if (result == null)
                return (new List<long>());
            if (result.Length < 1)
                return (new List<long>());
            string[] groupstr = result.Split('\n');
            List<long> grouplist = new List<long>();
            for (int i = 0; i < groupstr.Length; i++)
            {
                string temp = groupstr[i].Trim();
                if (IBoxs.Tool.Tools.ToolsBox.isQQ(temp))
                {
                    long qtemp = Convert.ToInt64(temp);
                    if (!grouplist.Contains(qtemp))
                        grouplist.Add(qtemp);
                }
            }
            return grouplist;
        }
        
        /// <summary>
        /// 获取群成员人数与最大人数
        /// </summary>
        /// <param name="robotQQ"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        public bool GetGroupMemberCount(long robotQQ, long group, out int memberCount, out int maxCount)
        {
            memberCount = 0;
            maxCount = 0;
            string json = Marshal.PtrToStringAnsi(CQP.Api_GetGroupMemberList(robotQQ.ToString(), group.ToString())).Trim();
            if (json.Length < 1)
                return false;
            json = Cqp.Core.KerMsg.FromUnicodeString(json);
            bool success = Core.Handle.MemberListHandle.GetGroupCur(json, out maxCount, out memberCount);
            return success;
        }
        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="robotQQ"></param>
        /// <param name="guid"></param>
        /// <param name="type">1 群 讨论组 2临时会话和好友</param>
        /// <param name="from">图片所属对应的群号和好友QQ</param>
        /// <returns></returns>
        public string ReceiveImage(long robotQQ,string guid,int type,long from)
        {
            string url= Marshal.PtrToStringAnsi(CQP.Api_GetPicLink(robotQQ.ToString(),type,from.ToString(),guid));
            string data = Application.StartupPath + @"\Data\" +IBoxs.Tool.Tools.ToolsBox.GetTimestamp(DateTime.Now).ToString()+ IBoxs.Tool.Tools.ToolsBox.GetRandomString(5) + ".jpg";
            Core.KerMsg.HttpDownloadFile(url, data);
            return data;
        }
        /// <summary>
        /// 获取QQ性别
        /// </summary>
        /// <param name="qq"></param>
        /// <returns></returns>
        public Enum.Sex GetQQGender(long robotQQ,long group, long qq)
        {
            GroupMemberInfo gm = new GroupMemberInfo();
            gm = GetMemberInfo(robotQQ, group, qq);
            if (gm == null)
                return Enum.Sex.Unknown;
            else
                return gm.Sex;
        }
        /// <summary>
        /// 获取群信息
        /// </summary>
        /// <param name="robotQQ"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        public GroupInfo GetGroupInfo(long robotQQ, long group)
        {
            List<GroupInfo> gl = new List<GroupInfo>();
            gl = GetGroupList(robotQQ);
            if (gl == null || gl.Count < 1)
                return null;
            for (int i = 0; i < gl.Count; i++)
            {
                if (gl[i].Id == group)
                {
                    GroupInfo g = new GroupInfo();
                    g.Id = gl[i].Id;
                    g.Name = gl[i].Name;
                    g.owner = gl[i].owner;
                    g.GroupLavel = gl[i].GroupLavel;
                    return g;
                }
            }
            return null;
        }
        /// <summary>
        /// 获取群成员列表
        /// </summary>
        /// <param name="robotQQ"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        public List<GroupMemberInfo> GetMemberList(long robotQQ, long group)
        {
            string json = Marshal.PtrToStringAnsi(CQP.Api_GetGroupMemberList(robotQQ.ToString(),group.ToString())).Trim();
            if (json.Length < 1)
                return null;
            json = Cqp.Core.KerMsg.FromUnicodeString(json);
            return Core.Handle.MemberListHandle.getMemberList(json,group);
        }
        /// <summary>
        /// 获取群成员列表（无昵称）
        /// </summary>
        /// <param name="robotQQ"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        public List<GroupMemberInfo> GetMemberList_B(long robotQQ, long group)
        {
            string json = Marshal.PtrToStringAnsi(CQP.Api_GetGroupMemberList_B(robotQQ.ToString(), group.ToString())).Trim();
            if (json.Length < 1)
                return null;
            json = Cqp.Core.KerMsg.FromUnicodeString(json);

            File.WriteAllText("D:/1.txt", json);

            return null;
        }

        #endregion

        #region --接口--
        /// <summary>
        /// 置群成员禁言
        /// </summary>
        /// <param name="robotQQ"></param>
        /// <param name="group"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public int SetGroupBanSpeak(long robotQQ, long group,long qq, int t)
        {
            CQP.Api_ShutUP(robotQQ.ToString(), group.ToString(), qq.ToString(), t);
            return 1;
        }
        /// <summary>
        /// 撤回消息
        /// </summary>
        /// <returns></returns>
        public int RepealMessage(long robotQQ, long group, string msgNum, string msgID)
        {
            CQP.Api_WithdrawMsg(robotQQ.ToString(), group.ToString(), msgNum.ToString(), msgID.ToString());
            return 1;
        }
        /// <summary>
        /// 获取用户昵称
        /// </summary>
        /// <param name="robotQQ"></param>
        /// <param name="qq"></param>
        /// <returns></returns>
        public string GetQQNick(long robotQQ, long qq)
        {
          return  Marshal.PtrToStringAnsi(CQP.Api_GetNick(robotQQ.ToString(),qq.ToString()));
        }
        /// <summary>
        /// 处理好友添加请求
        /// </summary>
        public int SetFriendAddRequest(long robotQQ, long qq, Enum.ResponseType HandleType,string note=null)
        {
            int ps = 10;
            if (HandleType == Enum.ResponseType.PASS)
                ps = 10;
            else if (HandleType == Enum.ResponseType.FAIL)
                ps = 20;
            else if (HandleType == Enum.ResponseType.IGNORE)
                ps = 30;
            CQP.Api_HandleFriendEvent(robotQQ.ToString(), qq.ToString(), ps, note);
            return 0;
        }
        /// <summary>
        /// 处理加群请求
        /// </summary>
        /// <returns></returns>
        public int SetGroupAddRequest(long robotQQ,long group,string seq,Enum.RequestType reptype, long qq, Enum.ResponseType HandleType, string note = null)
        {
            CQP.Api_HandleGroupEvent(robotQQ.ToString(), (int)reptype, qq.ToString(), group.ToString(), seq, (int)HandleType, note);
            return 1;
        }
        /// <summary>
        /// 获取群成员信息
        /// </summary>
        /// <param name="robotQQ"></param>
        /// <param name="group"></param>
        /// <param name="qq"></param>
        /// <returns></returns>
        public GroupMemberInfo GetMemberInfo(long robotQQ, long group, long qq)
        {
            List<GroupMemberInfo> gmi = new List<GroupMemberInfo>();
            gmi = GetMemberList(robotQQ, group);
            if (gmi == null)
                return null;
            for (int i = 0; i < gmi.Count; i++)
            {
                if (gmi[i].QQId == qq)
                {
                    return gmi[i];
                }
            }
            return null;
        }
        /// <summary>
        /// 移除群成员
        /// </summary>
        /// <param name="robotQQ"></param>
        /// <param name="group"></param>
        /// <param name="qq"></param>
        /// <returns></returns>
        public int SetGroupMemberRemove(long robotQQ, long group, long qq,bool black)
        {
            CQP.Api_KickGroupMBR(robotQQ.ToString(), group.ToString(), qq.ToString(), black);
            return 1;
        }
        /// <summary>
        /// 退出群
        /// </summary>
        /// <param name="robotQQ"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        public int SetGroupExit(long robotQQ, long group, bool black)
        {
            CQP.Api_QuitGroup(robotQQ.ToString(), group.ToString());
            return 1;
        }

        #endregion
    }
}
