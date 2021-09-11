﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using IBoxs.Sdk.Cqp.EventArgs;

namespace IBoxs.Core.App.Core
{
    public class OnoQQ_Fun
    {
        private static Encoding _defaultEncoding = null;

        /// <summary>
        /// 静态构造函数, 注册依赖注入回调
        /// </summary>
        static OnoQQ_Fun()
        {
            _defaultEncoding = Encoding.GetEncoding("GB18030");

            // 初始化 Costura.Fody
            CosturaUtility.Initialize();

            // 初始化依赖注入容器
            Common.UnityContainer = new UnityContainer();

        }

        /// <summary>
        /// 返回 AppID 与 ApiVer, 本方法在模板运行后会根据项目名称自动填写 AppID 与 ApiVer
        /// </summary>
        /// <returns></returns>
        [DllExport(ExportName = "OQ_Create", CallingConvention = CallingConvention.StdCall)]
        private static string OQ_Create()
        {
            /* 打个小广告：
             * 1.格子吧一站式软件授权服务系统：https://auth.itgz8.com
             * 2.为您提供插件收费、授权管理、卡密授权一站式服务
             */
            Common.AppName = "C# For SDK";
            Common.AppVersion = "1.1.4";
            Common.AppVersionId = 1;
            Common.Author = "IT格子";
            Common.AppDirectory = Application.StartupPath + @"\Config\" + Common.AppName;
            Common.Description = "C# SDK";
            Common.skey = "8956RTEWDFG3216598WERDF3";
            Common.SDK = "S3";
            Initialize();
            return AppInfoHandle();
        }

        private static string AppInfoHandle()
        {
            string ret = "插件名称{" + Common.AppName + "}\r" +
                "插件版本{" + Common.AppVersion + "}\r" +
                "插件作者{" + Common.Author + "}\r" +
                "插件说明{" + Common.Description + "}\r" +
                "插件skey{" + Common.skey + "}\r" +
                "插件sdk{" + Common.SDK + "}";
            return ret;
        }

        private static int Initialize()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            return 0;
        }

        /// <summary>
        /// 此子程序用于接收所有原始封包内容
        /// </summary>
        /// <param name="RobotQQ">接收到消息的机器人QQ</param>
        /// <param name="type">通信类型（UDP收到的原始信息）</param>
        /// <param name="TeaMsg">经过Tea加密的原文</param>
        /// <param name="Cookie">用于登录网页所需的cookies</param>
        /// <param name="SessionKey">用于登录网页所需的cookies</param>
        /// <param name="ClientKey">登录网页服务用的秘钥</param>
        /// <returns></returns>
        [DllExport(ExportName = "OQ_Message", CallingConvention = CallingConvention.StdCall)]
        private static int OQ_Message(string qq, int type, string teamsg, string cookie, string session, string ClientKey)
        {
            return 1;
        }
        /// <summary>
        /// 分发机器人收到的所有事件和消息
        /// </summary>
        /// <param name="RobotQQ">接收到消息的机器人QQ</param>
        /// <param name="type">此处仅列举： -1 未定义事件 0,在线状态临时会话 1,好友信息 2,群信息 3,讨论组信息 4,群临时会话 5,讨论组临时会话 6,财付通转账 7,好友验证回复会话</param>
        /// <param name="Subtype">有不同的定义，暂定：接收财付通转账时 1待确认收款 0为已收款    有人请求入群时，不良成员这里为1</param>
        /// <param name="From">消息的来源</param>
        /// <param name="Fromqq">触发的主动对象</param>
        /// <param name="Passiveqq">触发的被动对象</param>
        /// <param name="Message">消息内容</param>
        /// <param name="MsgNum">消息序号</param>
        /// <param name="MsgID">消息ID</param>
        /// <param name="TeaMsg">原始信息（通常为json结构）</param>
        /// <param name="json">转账信息时这里为转账的原始json</param>
        /// <param name="note">信息回传文本指针</param>
        /// <returns></returns>
        [DllExport(ExportName = "OQ_Event", CallingConvention = CallingConvention.StdCall)]
        public static int OQ_Event(string micqq, int type, int lowtype, string from, string fromqq, string bqq, string msg, string MsgNum, string msgID, string teamsg, string json, int note)
        {
            if (type == 1)
            {
                // Common.CqApi.GetMemberList(micqq, "901224469");
            }
            int ret = 1;
            switch (type)
            {
                case 1101: ret = Event.Event_AppMain.CqAppEnable(); break;
                case 12000: ret = Event.Event_AppMain.CqStartup(); break;
                case 12001: ret = Event.Event_AppMain.CqAppEnable(); break;
                case 12002: ret = Event.Event_AppMain.CqAppDisable(); break;
            }

            if (type < 0)  //未定义事件
            {

            }
            else if (type == 1)  //好友消息
            {
                CqPrivateMessageEventArgs e = new CqPrivateMessageEventArgs(msgID, MsgNum, Convert.ToInt64(micqq), Convert.ToInt64(from), msg);
                ret = Event.Event_Private.ReceiveFriendMessage(e);
            }
            else if (type == 2)  //群消息
            {
                CqGroupMessageEventArgs e = new CqGroupMessageEventArgs("群聊消息", msgID, MsgNum, Convert.ToInt64(micqq), Convert.ToInt64(from), Convert.ToInt64(fromqq), msg);
                ret = Event.Event_Group.ReceiveGroupMessage(e);
            }
            else if (type == 4)  //群私聊消息
            {
                CqGroupPrivateMessageEventArgs e = new CqGroupPrivateMessageEventArgs(msgID, MsgNum, Convert.ToInt64(micqq), Convert.ToInt64(from), Convert.ToInt64(fromqq), msg);
                ret = Event.Event_Group.ReceiveGroupPrivateMessage(e);
            }
            else if (type == 101)  //收到好友申请
            {
                CqAddFriendRequestEventArgs e = new CqAddFriendRequestEventArgs(DateTime.Now, Convert.ToInt64(micqq), Convert.ToInt64(from), msg, teamsg);
                ret = Event.Event_Private.ReceiveFriendAddRequest(e);
            }
            else if (type == 102)   //被同意加为好友
            {
                CqAddFriendAgreeEventArgs e = new CqAddFriendAgreeEventArgs(DateTime.Now, true, Convert.ToInt64(micqq), Convert.ToInt64(from));
                ret = Event.Event_Private.ReceiveAgreeFriends(e);
            }
            else if (type == 103)   //被拒绝加为好友
            {
                CqAddFriendAgreeEventArgs e = new CqAddFriendAgreeEventArgs(DateTime.Now, false, Convert.ToInt64(micqq), Convert.ToInt64(from));
                ret = Event.Event_Private.ReceiveAgreeFriends(e);
            }
            else if (type == 112)   //已成功加为好友
            {
                CqAddFriendAgreeEventArgs e = new CqAddFriendAgreeEventArgs(DateTime.Now, true, Convert.ToInt64(micqq), Convert.ToInt64(from));
                ret = Event.Event_Private.ReceiveAgreeFriends(e);
            }
            else if (type == 202)  //群成员被移除
            {
                CqGroupMemberDecreaseEventArgs e = new CqGroupMemberDecreaseEventArgs(DateTime.Now, Convert.ToInt64(from), Convert.ToInt64(micqq), Convert.ToInt64(fromqq), Convert.ToInt64(bqq));
                ret = Event.Event_Group.ReceiveGroupMemberRemove(e);
            }
            else if (type == 212)   //群成员主动进群
            {
                CqGroupMemberIncreaseEventArgs e = new CqGroupMemberIncreaseEventArgs(DateTime.Now, Convert.ToInt64(micqq), Convert.ToInt64(from), Convert.ToInt64(fromqq), Convert.ToInt64(bqq));
                ret = Event.Event_Group.ReceiveGroupMemberPass(e);
            }
            else if (type == 201)  //群成员退出群
            {
                CqGroupMemberDecreaseEventArgs e = new CqGroupMemberDecreaseEventArgs(DateTime.Now, Convert.ToInt64(from), Convert.ToInt64(micqq), Convert.ToInt64(fromqq), Convert.ToInt64(bqq));
                ret = Event.Event_Group.ReceiveGroupMemberLeave(e);
            }
            else if (type == 213)  //有人申请进群
            {
                CqAddGroupRequestEventArgs e = new CqAddGroupRequestEventArgs(DateTime.Now, Convert.ToInt64(from), Convert.ToInt64(micqq), Convert.ToInt64(fromqq), msg, teamsg);
                ret = Event.Event_Group.ReceiveAddGroupRequest(e);
            }
            else if (type == 214)
            {
                CqAddGroupRequestEventArgs e = new CqAddGroupRequestEventArgs(DateTime.Now, Convert.ToInt64(from), Convert.ToInt64(micqq), Convert.ToInt64(fromqq), msg, teamsg);
                ret = Event.Event_Group.ReceiveAddGroupBeInvitee(e);
            }
            else if (type == 219)  //群成员被邀请进群
            {
                CqGroupMemberIncreaseEventArgs e = new CqGroupMemberIncreaseEventArgs(DateTime.Now, Convert.ToInt64(micqq), Convert.ToInt64(from), Convert.ToInt64(fromqq), Convert.ToInt64(bqq));
                ret = Event.Event_Group.ReceiveGroupMemberBeInvitee(e);
            }
            return 1;
        }

        /// <summary>
        /// 用户按下设置时执行本函数
        /// </summary>
        [DllExport(ExportName = "OQ_SetUp", CallingConvention = CallingConvention.StdCall)]
        private static void OQ_SetUp()
        {
            Event.Event_AppMain.CallMenu();
        }

        /// <summary>
        /// 插件被卸载时执行本函数
        /// </summary>
        /// <returns></returns>
        [DllExport(ExportName = "OQ_DestroyPlugin", CallingConvention = CallingConvention.StdCall)]
        private static int OQ_DestroyPlugin()
        {
            Event.Event_AppMain.CqAppDisable();
            return 0;
        }

        /// <summary>
        /// 全局异常捕获, 用于捕获开发者未处理的异常, 此异常将回弹至酷Q进行处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            if (ex != null)
            {
                Common.CqApi.OutPutLog(ex.ToString());
            }
        }
    }
}
