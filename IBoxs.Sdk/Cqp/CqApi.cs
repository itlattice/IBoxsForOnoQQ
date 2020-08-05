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
        public string SendPrivateMessage(string RobotQQ, string qqId, string message,int Type=1,int Bubble=-1)
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
        public string SendGroupMessage(string RobotQQ, string groupId, string message, int Type = 1, int Bubble = -1)
        {
            string c = Marshal.PtrToStringAnsi(CQP.Api_SendMsg(RobotQQ.ToString(), 2,groupId, String.Empty, message, Bubble, Type));
            return c;
        }

        /// <summary>
        /// 发送讨论组消息
        /// </summary>
        /// <param name="discussId">目标讨论组</param>
        /// <param name="message">消息内容</param>
        /// <returns>失败返回负值, 成功返回消息 Id</returns>
        public string SendDiscussMessage(string RobotQQ, string discussId, string message, int Type = 1, int Bubble = -1)
        {
            string c = Marshal.PtrToStringAnsi(CQP.Api_SendMsg(RobotQQ.ToString(), 3, discussId, String.Empty, message, Bubble, Type));
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
            for(int i=0;i<count;i++)
                Marshal.PtrToStringAnsi(CQP.Api_UpVote(RobotQQ, qqId));
            return String.Empty;
        }

        /// <summary>
        /// 撤回消息
        /// </summary>
        /// <param name="id">消息ID</param>
        /// <returns></returns>
        public int RepealMessage(string RobotQQ,string groupId,string msgNum,string MsgID)
        {
            return Convert.ToInt32(Marshal.PtrToStringAnsi(CQP.Api_WithdrawMsg(RobotQQ,groupId,msgNum,MsgID)));
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
                return string.Format("[@{0}]",qqId.ToString());
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
            if (Path.IsPathRooted(filePath))
            {
                File.Copy(filePath, Application.StartupPath + @"\data\image\" + Path.GetFileName(filePath), true);
            }
            if (destory)
            {
                return string.Format("[FlashPic={0}]", CqCode_Trope(filePath, true));
            }
            return string.Format("[pic={0}]", CqCode_Trope(filePath, true));
        }
        #endregion
        
        #region --框架--
        /// <summary>
        /// 取登录QQ
        /// </summary>
        /// <returns>返回整数</returns>
        public List<string> GetLoginQQ()
        {
            string qq=Marshal.PtrToStringAnsi(CQP.Api_GetQQList());
            if (qq.Length < 1)
                return (new List<string>());
            string[] qqstr = qq.Split('\r');
            List<string> qqlist = new List<string>();
            for (int i = 0; i < qqstr.Length; i++)
            {
                qqlist.Add(qqstr[i].Trim());
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
            string json= Marshal.PtrToStringAnsi(CQP.Api_GetFriendList(robotQQ));
            json = Cqp.Core.KerMsg.FromUnicodeString(json);
            return Core.Handle.FriendListHandle.getFriends(json);
        }
        /// <summary>
        /// 获取群列表
        /// </summary>
        /// <param name="robotQQ"></param>
        /// <returns></returns>
        public List<GroupInfo> GetGroupList(string robotQQ)
        {
            string json = Marshal.PtrToStringAnsi(CQP.Api_GetGroupList(robotQQ));
            json = Cqp.Core.KerMsg.FromUnicodeString(json);
            return Core.Handle.GroupListHandle.getGroupList(json);
        }
        /// <summary>
        /// 获取群信息
        /// </summary>
        /// <param name="robotQQ"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        public GroupInfo GetGroupInfo(string robotQQ, string group)
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
                    g.CurrentNumber = 0;
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
        public List<GroupMemberInfo> GetMemberList(string robotQQ, string group)
        {
            string json = Marshal.PtrToStringAnsi(CQP.Api_GetGroupMemberList(robotQQ,group)).Trim();
            if (json.Length < 1)
                return null;
            json = Cqp.Core.KerMsg.FromUnicodeString(json);
            return Core.Handle.MemberListHandle.getMemberList(json,group);
        }
        #endregion
    }
}
