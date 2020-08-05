using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

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



        #region --CQ码--
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
            return Marshal.PtrToStringAnsi(CQP.Api_GetQQList());
        }

        /// <summary>
        /// 获取当前登录QQ的昵称
        /// </summary>
        /// <returns>返回 GB108030 编码的字符串</returns>
        public string GetLoginNick()
        {
            return CQP.CQ_getLoginNick(_authCode).ToString(_defaultEncoding);
        }

        /// <summary>
        /// 取应用目录
        /// </summary>
        /// <returns>返回本地路径字符串</returns>
        public string GetAppDirectory()
        {
            if (_appDirCache == null)
            {
                _appDirCache = CQP.CQ_getAppDirectory(_authCode).ToString(_defaultEncoding); ;
            }
            return _appDirCache;
        }

        /// <summary>
        /// 获取 Cookies 慎用,此接口需要严格授权
        /// </summary>
        /// <returns>返回 Cookies 字符串</returns>
        [Obsolete("此方法已失效, 请使用 GetCookies 的第二个重载. 此方法将永远抛出异常")]
        public string GetCookies()
        {
            return CQP.CQ_getCookies(_authCode).ToString(_defaultEncoding);
        }

        /// <summary>
        /// 获取 Cookies 慎用,此接口需要严格授权
        /// </summary>
        /// <param name="domain">目标域名, 如 api.example.com</param>
        /// <returns>返回 Cookies 字符串</returns>
        public string GetCookies(string domain)
        {
            return CQP.CQ_getCookiesV2(_authCode, domain).ToString(_defaultEncoding);
        }

        /// <summary>
        /// 获取 Cookies 慎用, 此接口需要严格授权
        /// </summary>
        /// <param name="domain">目标域名, 如 api.example.com</param>
        /// <returns>返回 <see cref="CookieCollection"/> 对象</returns>
        public CookieCollection GetCookieCollection(string domain)
        {
           
        }

        /// <summary>
        /// 即QQ网页用到的bkn/g_tk等 慎用,此接口需要严格授权
        /// </summary>
        /// <returns>返回 bkn/g_tk 字符串</returns>
        public int GetCsrfToken()
        {
            return Convert.ToInt32(Marshal.PtrToStringAnsi(CQP.CQ_getCsrfToken(_authCode)));
        }

        /// <summary>
        /// 获取QQ信息
        /// </summary>
        /// <param name="qqId">目标QQ</param>
        /// <param name="notCache">不使用缓存, 默认为"False"，通常忽略本参数，仅在必要时使用</param>
        /// <returns>获取成功返回 <see cref="QQInfo"/>, 失败返回 null</returns>
        public QQInfo GetQQInfo(long qqId, bool notCache = true)
        {
            string result = CQP.CQ_getStrangerInfo(_authCode, qqId, notCache).ToString(Encoding.ASCII);
            if (string.IsNullOrEmpty(result))
            {
                return null;
            }
            using (BinaryReader binary = new BinaryReader(new MemoryStream(Convert.FromBase64String(result))))
            {
                try
                {
                    QQInfo qqInfo = new QQInfo();
                    qqInfo.Id = binary.ReadInt64_Ex();
                    qqInfo.Nick = binary.ReadString_Ex(_defaultEncoding);
                    qqInfo.Sex = (Sex)binary.ReadInt32_Ex();
                    qqInfo.Age = binary.ReadInt32_Ex();
                    return qqInfo;
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 获取群成员信息
        /// </summary>
        /// <param name="groupId">目标群</param>
        /// <param name="qqId">目标QQ</param>
        /// <param name="notCache">默认为 "Flase", 通常忽略本参数, 仅在必要的是否使用</param>
        /// <returns>获取成功返回 <see cref="GroupMemberInfo"/>, 失败返回 null</returns>
        public GroupMemberInfo GetMemberInfo(long groupId, long qqId, bool notCache = true)
        {
            string result = "";
            try
            {
                result = CQP.CQ_getGroupMemberInfoV2(_authCode, groupId, qqId, notCache).ToString(Encoding.ASCII);
            }
            catch
            {
                return null;
            }
            if (string.IsNullOrEmpty(result))
            {
                return null;
            }
            #region --其它_转换_文本到群成员信息--
            using (BinaryReader binary = new BinaryReader(new MemoryStream(Convert.FromBase64String(result))))
            {
                GroupMemberInfo member = new GroupMemberInfo();
                member.GroupId = binary.ReadInt64_Ex();
                member.QQId = binary.ReadInt64_Ex();
                member.Nick = binary.ReadString_Ex(_defaultEncoding);
                member.Card = binary.ReadString_Ex(_defaultEncoding);
                member.Sex = (Sex)binary.ReadInt32_Ex();
                member.Age = binary.ReadInt32_Ex();
                member.Area = binary.ReadString_Ex(_defaultEncoding);
                member.JoiningTime = binary.ReadInt32_Ex().ToDateTime();
                member.LastDateTime = binary.ReadInt32_Ex().ToDateTime();
                member.Level = binary.ReadString_Ex(_defaultEncoding);
                member.PermitType = (PermitType)binary.ReadInt32_Ex();
                member.BadRecord = binary.ReadInt32_Ex() == 1;
                member.SpecialTitle = binary.ReadString_Ex(_defaultEncoding);
                member.SpecialTitleDurationTime = binary.ReadInt32_Ex().ToDateTime();
                member.CanModifiedCard = binary.ReadInt32_Ex() == 1;
                return member;
            }
            #endregion
        }

        /// <summary>
        /// 获取群成员列表
        /// </summary>
        /// <param name="groupId">目标群</param>
        /// <returns>获取成功返回 <see cref="List{GroupMember}"/>, 失败返回 null</returns>
        public List<GroupMemberInfo> GetMemberList(long groupId)
        {
            string result = CQP.CQ_getGroupMemberList(_authCode, groupId).ToString(Encoding.ASCII);
            if (string.IsNullOrEmpty(result))
            {
                return null;
            }
            #region --其他_转换_文本到群成员列表信息a--
            using (BinaryReader binary = new BinaryReader(new MemoryStream(Convert.FromBase64String(result))))
            {
                List<GroupMemberInfo> memberInfos = new List<GroupMemberInfo>();
                for (int i = 0, len = binary.ReadInt32_Ex(); i < len; i++)
                {
                    if (binary.Length() <= 0)
                    {
                        return null;
                    }
                    #region --其它_转换_ansihex到群成员信息--
                    using (BinaryReader tempBinary = new BinaryReader(new MemoryStream(binary.ReadToken_Ex()))) //解析群成员信息
                    {
                        GroupMemberInfo member = new GroupMemberInfo();
                        member.GroupId = tempBinary.ReadInt64_Ex();
                        member.QQId = tempBinary.ReadInt64_Ex();
                        member.Nick = tempBinary.ReadString_Ex(_defaultEncoding);
                        member.Card = tempBinary.ReadString_Ex(_defaultEncoding);
                        member.Sex = (Sex)tempBinary.ReadInt32_Ex();
                        member.Age = tempBinary.ReadInt32_Ex();
                        member.Area = tempBinary.ReadString_Ex(_defaultEncoding);
                        member.JoiningTime = tempBinary.ReadInt32_Ex().ToDateTime();
                        member.LastDateTime = tempBinary.ReadInt32_Ex().ToDateTime();
                        member.Level = tempBinary.ReadString_Ex(_defaultEncoding);
                        member.PermitType = (PermitType)tempBinary.ReadInt32_Ex();
                        member.BadRecord = tempBinary.ReadInt32_Ex() == 1;
                        member.SpecialTitle = tempBinary.ReadString_Ex(_defaultEncoding);
                        member.SpecialTitleDurationTime = tempBinary.ReadInt32_Ex().ToDateTime();
                        member.CanModifiedCard = tempBinary.ReadInt32_Ex() == 1;
                        memberInfos.Add(member);
                    }
                    #endregion
                }
                return memberInfos;
            }
            #endregion
        }

        /// <summary>
        /// 获取群列表
        /// </summary>
        /// <returns>获取成功返回 <see cref="List{Group}"/>, 失败返回 null</returns>
        public List<Model.GroupInfo> GetGroupList()
        {
            string result = CQP.CQ_getGroupList(_authCode).ToString(Encoding.ASCII);
            if (string.IsNullOrEmpty(result))
            {
                return null;
            }
            List<Model.GroupInfo> groups = new List<Model.GroupInfo>();
            #region --其他_转换_文本到群列表信息a--
            using (BinaryReader binary = new BinaryReader(new MemoryStream(Convert.FromBase64String(result))))
            {
                for (int i = 0, len = binary.ReadInt32_Ex(); i < len; i++)
                {
                    if (binary.Length() <= 0)
                    {
                        return null;
                    }
                    #region --其他_转换_ansihex到群信息--
                    using (BinaryReader tempBinary = new BinaryReader(new MemoryStream(binary.ReadToken_Ex())))
                    {
                        Model.GroupInfo group = new Model.GroupInfo();
                        group.Id = tempBinary.ReadInt64_Ex();
                        group.Name = tempBinary.ReadString_Ex(_defaultEncoding);
                        group.CurrentNumber = tempBinary.ReadInt32_Ex();
                        group.MaximumNumber = tempBinary.ReadInt32_Ex();
                        groups.Add(group);
                    }
                    #endregion
                }
                return groups;
            }
            #endregion
        }

        /// <summary>
        /// 获取发送语音支持
        /// </summary>
        /// <returns>获取成功则返回 <code>true</code>, 否则返回 <code>false</code></returns>
        public bool GetSendRecordSupport()
        {
            return Convert.ToInt32(Marshal.PtrToStringAnsi(CQP.CQ_canSendRecord(_authCode))) > 0;
        }

        /// <summary>
        /// 获取发送图片支持
        /// </summary>
        /// <returns>获取成功则返回 <code>true</code>, 否则返回 <code>false</code></returns>
        public bool GetSendImageSupport()
        {
            //  MessageBox.Show(Marshal.PtrToStringAnsi(CQP.CQ_canSendImage(_authCode)));
            return CQP.CQ_canSendImage(_authCode) > 0;
        }

        /// <summary>
        /// 获取群信息
        /// </summary>
        /// <param name="groupId">群号码</param>
        /// <param name="notCache">不使用缓存, 通常为 <code>false</code>, 仅在必要时使用</param>
        /// <returns>如果成功返回 <see cref="GroupInfo"/>, 若失败返回 null</returns>
        public GroupInfo GetGroupInfo(long groupId, bool notCache = false)
        {
            string result = CQP.CQ_getGroupInfo(_authCode, groupId, false).ToString(_defaultEncoding);
            if (string.IsNullOrEmpty(result))
            {
                return null;
            }
            #region --其他_转换_文本到群信息--
            using (BinaryReader reader = new BinaryReader(new MemoryStream(Convert.FromBase64String(result))))
            {
                if (reader.Length() < 18)
                {
                    return null;
                }

                GroupInfo info = new GroupInfo();
                info.Id = reader.ReadInt64_Ex();
                info.Name = reader.ReadString_Ex(_defaultEncoding);
                info.CurrentNumber = reader.ReadInt32_Ex();
                info.MaximumNumber = reader.ReadInt32_Ex();
                return info;
            }
            #endregion
        }

        /// <summary>
        /// 获取好友列表
        /// </summary>
        /// <returns>获取成功返回 <see cref="List{FriendInfo}"/>, 否则返回 null</returns>
        public List<FriendInfo> GetFriendList()
        {
            string result = CQP.CQ_getFriendList(_authCode, false).ToString(_defaultEncoding);
            if (string.IsNullOrEmpty(result))
            {
                return null;
            }

            #region --其他_转换_文本到好友列表信息a--
            using (BinaryReader reader = new BinaryReader(new MemoryStream(Convert.FromBase64String(result))))
            {
                if (reader.Length() < 4)
                {
                    return null;
                }

                List<FriendInfo> friends = new List<FriendInfo>();
                for (int i = 0, len = reader.ReadInt32_Ex(); i < len; i++)
                {
                    FriendInfo temp = new FriendInfo();
                    if (reader.Length() <= 0)
                    {
                        return null;
                    }

                    #region --其他_转换_ansihex到好友信息--
                    using (BinaryReader tempReader = new BinaryReader(new MemoryStream(reader.ReadToken_Ex())))
                    {
                        if (tempReader.Length() < 12)
                        {
                            return null;
                        }

                        temp.Id = tempReader.ReadInt64_Ex();
                        temp.Nick = tempReader.ReadString_Ex(_defaultEncoding);
                        temp.Note = tempReader.ReadString_Ex(_defaultEncoding);
                    }
                    #endregion

                    friends.Add(temp);
                }
                return friends;
            }
            #endregion
        }
        #endregion

        #region --日志--
        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="level">级别</param>
        /// <param name="type">类型</param>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public int AddLoger(LogerLevel level, string type, string content)
        {
            GCHandle handle = content.GetStringGCHandle(_defaultEncoding);
            CQP.CQ_addLog(_authCode, (int)level, type, handle.AddrOfPinnedObject());
            handle.Free();
            return 0;

        }

        /// <summary>
        /// 添加致命错误提示
        /// </summary>
        /// <param name="message">错误信息</param>
        /// <returns></returns>
        public int AddFatalError(string message)
        {
            return Convert.ToInt32(Marshal.PtrToStringAnsi(CQP.CQ_setFatal(_authCode, message)));
        }
        #endregion

        #region --请求--
        /// <summary>
        /// 置好友添加请求
        /// </summary>
        /// <param name="tag">请求反馈标识</param>
        /// <param name="response">反馈类型</param>
        /// <param name="notes">备注</param>
        /// <returns></returns>
        public void SetFriendAddRequest(string tag, ResponseType response, string notes = null)
        {
            if (notes == null)
            {
                notes = string.Empty;
            }
            GCHandle handle = notes.GetStringGCHandle(_defaultEncoding);
            Marshal.PtrToStringAnsi(CQP.CQ_setFriendAddRequest(_authCode, tag, (int)response, handle.AddrOfPinnedObject()));
            handle.Free();
        }

        /// <summary>
        /// 置群添加请求
        /// </summary>
        /// <param name="tag">请求反馈标识</param>
        /// <param name="request">请求类型</param>
        /// <param name="response">反馈类型</param>
        /// <param name="appendMsg">备注</param>
        /// <returns></returns>
        public void SetGroupAddRequest(string tag, RequestType request, ResponseType response, string appendMsg)
        {
            if (appendMsg == null)
            {
                appendMsg = string.Empty;
            }
            GCHandle handle = appendMsg.GetStringGCHandle(_defaultEncoding);
            CQP.CQ_setGroupAddRequestV2(_authCode, tag, (int)request, (int)response, handle.AddrOfPinnedObject());
            handle.Free();
            return;
        }
        #endregion

        #region --管理--
        /// <summary>
        /// 置匿名群员禁言
        /// </summary>
        /// <param name="groupId">目标群</param>
        /// <param name="anonymous">匿名参数</param>
        /// <param name="time">禁言时间, 单位: 秒, 不支持解禁</param>
        /// <returns></returns>
        public int SetGroupAnonymousBanSpeak(long groupId, string anonymous, TimeSpan time)
        {
            if (time.TotalSeconds <= 0)
            {
                time = TimeSpan.Zero;
            }
            GCHandle handle = anonymous.GetStringGCHandle(_defaultEncoding);
            CQP.CQ_setGroupAnonymousBan(_authCode, groupId, handle.AddrOfPinnedObject(), (long)time.TotalSeconds);
            handle.Free();
            return 0;
        }

        /// <summary>
        /// 置群员禁言
        /// </summary>
        /// <param name="groupId">目标群</param>
        /// <param name="qqId">目标QQ</param>
        /// <param name="time">禁言的时间，单位为秒。如果要解禁，请给TimeSpan.Zero</param>
        /// <returns></returns>
        public int SetGroupBanSpeak(long groupId, long qqId, TimeSpan time)
        {
            if (time.Ticks < 0)
            {
                time = TimeSpan.Zero;
            }
            CQP.CQ_setGroupBan(_authCode, groupId, qqId, (long)time.TotalSeconds);
            return 0;
        }

        /// <summary>
        /// 置全群禁言
        /// </summary>
        /// <param name="groupId">目标群</param>
        /// <param name="isOpen">是否开启</param>
        /// <returns></returns>
        public int SetGroupWholeBanSpeak(long groupId, bool isOpen)
        {
            return Convert.ToInt32(Marshal.PtrToStringAnsi(CQP.CQ_setGroupWholeBan(_authCode, groupId, isOpen)));
        }

        /// <summary>
        /// 置群成员名片
        /// </summary>
        /// <param name="groupId">目标群</param>
        /// <param name="qqId">目标QQ</param>
        /// <param name="newNick">新昵称</param>
        /// <returns></returns>
        public int SetGroupMemberNewCard(long groupId, long qqId, string newNick)
        {
            GCHandle handle = newNick.GetStringGCHandle(_defaultEncoding);
            CQP.CQ_setGroupCard(_authCode, groupId, qqId, handle.AddrOfPinnedObject());
            handle.Free();
            return 0;
        }

        /// <summary>
        /// 置群成员专属头衔
        /// </summary>
        /// <param name="groupId">目标群</param>
        /// <param name="qqId">目标QQ</param>
        /// <param name="specialTitle">如果要删除，这里填空</param>
        /// <param name="time">专属头衔有效期，单位为秒。如果永久有效，time填写负数</param>
        /// <returns></returns>
        public int SetGroupSpecialTitle(long groupId, long qqId, string specialTitle, TimeSpan time)
        {
            if (time.Ticks < 0)
            {
                time = new TimeSpan(-10000000);     //-1秒
            }
            GCHandle handle = specialTitle.GetStringGCHandle(_defaultEncoding);
            CQP.CQ_setGroupSpecialTitle(_authCode, groupId, qqId, handle.AddrOfPinnedObject(), (long)time.TotalSeconds);
            handle.Free();
            return 0;
        }

        /// <summary>
        /// 置群管理员
        /// </summary>
        /// <param name="groupId">目标群</param>
        /// <param name="qqId">目标QQ</param>
        /// <param name="isCalcel">True: 设置管理员, False: 取消管理员</param>
        /// <returns></returns>
        public int SetGroupManager(long groupId, long qqId, bool isCalcel)
        {
            CQP.CQ_setGroupAdmin(_authCode, groupId, qqId, isCalcel);
            return 0;
        }

        /// <summary>
        /// 置群匿名设置
        /// </summary>
        /// <param name="groupId">目标群</param>
        /// <param name="isOpen">是否打开</param>
        /// <returns></returns>
        public int SetAnonymousStatus(long groupId, bool isOpen)
        {
            CQP.CQ_setGroupAnonymous(_authCode, groupId, isOpen);
            return 0;
        }

        /// <summary>
        /// 置群退出 慎用,此接口需要严格授权
        /// </summary>
        /// <param name="groupId">目标群</param>
        /// <param name="dissolve">默认为False, True: 解散本群(群主) False: 退出本群(管理、群成员)</param>
        /// <returns></returns>
        public int SetGroupExit(long groupId, bool dissolve = false)
        {
            CQP.CQ_setGroupLeave(_authCode, groupId, dissolve);
            return 0;
        }

        /// <summary>
        /// 置群员移除
        /// </summary>
        /// <param name="groupId">目标群</param>
        /// <param name="qqId">目标QQ</param>
        /// <param name="notAccept">如果为True，则“不再接收此人加群申请”，请慎用。留空为False</param>
        /// <returns></returns>
        public int SetGroupMemberRemove(long groupId, long qqId, bool notAccept = false)
        {
            CQP.CQ_setGroupKick(_authCode, groupId, qqId, notAccept);
            return 0;
        }

        /// <summary>
        /// 置讨论组退出
        /// </summary>
        /// <param name="discussId">目标讨论组</param>
        /// <returns></returns>
        public int SetDiscussExit(long discussId)
        {
            CQP.CQ_setDiscussLeave(_authCode, discussId);
            return 0;
        }
        #endregion

        #region --其它--
        /// <summary>
        /// 获取匿名信息
        /// </summary>
        /// <param name="source">匿名参数</param>
        /// <returns></returns>
        public GroupAnonymous GetAnonymous(string source)
        {
            BinaryReader binary = new BinaryReader(new MemoryStream(Convert.FromBase64String(source)));
            GroupAnonymous anonymous = new GroupAnonymous();
            anonymous.Id = binary.ReadInt64_Ex();
            anonymous.CodeName = binary.ReadString_Ex();
            anonymous.Token = binary.ReadToken_Ex();
            return anonymous;
        }

        /// <summary>
        /// 获取群文件
        /// </summary>
        /// <param name="source">群文件参数</param>
        /// <returns></returns>
        public GroupFile GetFile(string source)
        {
            BinaryReader binary = new BinaryReader(new MemoryStream(Convert.FromBase64String(source)));
            GroupFile file = new GroupFile();
            file.Id = binary.ReadString_Ex(_defaultEncoding);      // 参照官方SDK, 编码为 ASCII
            file.Name = binary.ReadString_Ex(_defaultEncoding);    // 参照官方SDK, 编码为 ASCII
            file.Size = binary.ReadInt64_Ex();
            file.Busid = Convert.ToInt32(binary.ReadInt64_Ex());
            return file;
        }

        /// <summary>
        /// 编码悬浮窗数据置文本
        /// </summary>
        /// <param name="floatWindow"></param>
        /// <returns></returns>
        public string FormatStringFloatWindow(FloatWindow floatWindow)
        {
            BinaryWriter binary = new BinaryWriter(new MemoryStream());
            binary.Write_Ex(floatWindow.Data);
            binary.Write_Ex(floatWindow.Unit);
            binary.Write_Ex((int)floatWindow.Color);

            return Convert.ToBase64String(binary.ToArray());
        }
        #endregion

    }
}
