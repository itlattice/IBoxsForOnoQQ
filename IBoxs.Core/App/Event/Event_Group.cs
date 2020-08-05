using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBoxs.Sdk.Cqp.Interface;
using IBoxs.Sdk.Cqp.EventArgs;
using System.Data;
using System.Threading;
using DataHandle;

namespace IBoxs.Core.App.Event
{
    public class Event_GroupMsg : IReceiveGroupMessage
    {
        /// <summary>
        /// 群消息事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ReceiveGroupMessage(object sender, CqGroupMessageEventArgs e)
        {
            if (e.FromGroup < 0)
                return;
            if (e.Handler||e.IsAnonymous)
                return;
            if (DataHandle.Common.isRuning && DataHandle.PublicData.PublicData.FunctionBool.Group&&DataHandle.GetData.GetGroupData.GetAdminAdmBool(e.FromGroup))
            {
                Task t1 = new Task(() => GroupMsg(e));
                t1.Start();
                e.Handler = true;
            }
        }
        private void GroupMsg(CqGroupMessageEventArgs e)
        {
            /* 1.监控处理
             * 2.传入指令功能处理层
             */
            string error = null;
            if (!DataHandle.GetData.GetMicData.QurBool(out error))
            {
                Common.CqApi.SendGroupMessage(e.FromGroup, "机器人授权异常，原因【" + error + "】，请联系管理员");
                return;
            }
            Task exam = new Task(() => DataHandle.GroupExam.MsgExam.MsgExamClass.MsgExamMain(e.FromGroup, e.Message, e.FromQQ, e.MsgId));
            exam.Start();
            string at = Common.CqApi.CqCode_At(Common.CqApi.GetLoginQQ(), false).Trim();
            string msg=e.Message.Replace(at,"").Trim();
            if (msg.Length < 1)
            {
                if (e.Message.Contains(at))
                {
                    string reply =DataHandle.Config.Msg.Default.Group;
                    SendMsg.SendGroupMsg(e.FromGroup, reply, e.FromQQ);
                }
                return;
            }
            string re = Handle.MsgHandle.MsgHandleMain(e.FromQQ, msg, e.FromGroup, e.MsgId);
            if (re == "n")
                return;
            if (re == null)
            {
                string rep = null;
                if (DataHandle.Config.Config.Smart.SmartBool)
                {
                    rep = DataHandle.GetData.GetWebData.SmartChat(e.FromQQ, msg);
                }
                if(rep==null)
                    rep= DataHandle.Config.Msg.Default.Group;
                DataHandle.SendMsg.SendGroupMsg(e.FromGroup, rep, e.FromQQ);
            }
            else
            {
                DataHandle.SendMsg.SendGroupMsg(e.FromGroup, re, e.FromQQ);
            }
        }
    }

    public class Event_GroupUnloadFile : IReceiveGroupFileUpload
    {
        /// <summary>
        /// 群文件上传事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ReceiveGroupFileUpload(object sender, CqGroupFileUploadEventArgs e)
        {

        }
    }

    public class Event_GroupAdminIncrease : IReceiveGroupManageIncrease
    {
        /// <summary>
        /// 管理员增加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ReceiveGroupManageIncrease(object sender, CqGroupManageChangeEventArgs e)
        {

        }
    }

    public class Event_GroupAdminDecrease : IReceiveGroupManageDecrease
    {
        /// <summary>
        /// 管理员减少
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ReceiveGroupManageDecrease(object sender, CqGroupManageChangeEventArgs e)
        {

        }
    }

    public class Event_GroupMemberInvitee : IReceiveGroupMemberBeInvitee
    {
        /// <summary>
        /// 群成员增加：被邀入群
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ReceiveGroupMemberBeInvitee(object sender, CqGroupMemberIncreaseEventArgs e)
        {

        }
    }

    public class Event_GroupMemberLeave : IReceiveGroupMemberLeave
    {
        /// <summary>
        /// 群成员减少：成员离开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ReceiveGroupMemberLeave(object sender, CqGroupMemberDecreaseEventArgs e)
        {

        }
    }

    public class Event_GroupMemberPass : IReceiveGroupMemberPass
    {
        /// <summary>
        /// 群成员增加：主动入群
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ReceiveGroupMemberPass(object sender, CqGroupMemberIncreaseEventArgs e)
        {

        }
    }

    public class Event_GroupMember : IReceiveGroupMemberRemove
    {
        /// <summary>
        /// 群成员减少：被移除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ReceiveGroupMemberRemove(object sender, CqGroupMemberDecreaseEventArgs e)
        {

        }
    }

    public class Event_GroupMemberd : IReceiveGroupPrivateMessage
    {
        /// <summary>
        /// 群私聊消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ReceiveGroupPrivateMessage(object sender, CqPrivateMessageEventArgs e)
        {
            if (e.Handler)
                return;
            string error = null;
            if (!DataHandle.GetData.GetMicData.QurBool(out error))
            {
                Common.CqApi.SendPrivateMessage(e.FromQQ, "机器人授权异常，原因【" + error + "】，请联系管理员");
                return;
            }
            if (DataHandle.Common.isRuning && DataHandle.PublicData.PublicData.FunctionBool.Group)
            {
                Task t1 = new Task(() => GroupPMsg(e));
                t1.Start();
                e.Handler = true;
            }
        }
        private void GroupPMsg(CqPrivateMessageEventArgs e)
        {
            /* 1.监控处理
             * 2.传入指令功能处理层
             */
            if (DataHandle.Customer.CurtomerHandleClass.CurtomerHandleMain(e.FromQQ, e.Message))
            {
                return;
            }
            string reply = null;
            reply = Handle.AdminMsgHandle.MsgHandleMain(e.FromQQ, e.Message,0);
            if (reply != null)
            {
                if (reply == "n")
                    return;
                SendMsg.SendPrivateMsg(e.FromQQ, reply);
                return;
            }
            /******************************/
            reply = Handle.MsgHandle.MsgHandleMain(e.FromQQ, e.Message,0);
            if (reply == "n")
                return;
            if (reply == null)
            {
                if (DataHandle.Config.Config.Smart.SmartBool)
                {
                    reply = DataHandle.GetData.GetWebData.SmartChat(e.FromQQ, e.Message);
                }
                if (reply == null)
                    reply = DataHandle.Config.Msg.Default.Private;
                DataHandle.SendMsg.SendPrivateMsg(e.FromQQ, reply);
            }
            else
            {
                DataHandle.SendMsg.SendPrivateMsg(e.FromQQ, reply);
            }
        }
    }

    public class Event_GroupBanBreak : IReceiveSetGroupBan
    {
        /// <summary>
        /// 群禁言事件接口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ReceiveSetGroupBan(object sender, CqGroupBanEventArgs e)
        {

        }
    }

    public class Event_GroupRemoveBanBreak : IReceiveRemoveGroupBan
    {
        /// <summary>
        /// 群解除禁言接口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ReceiveRemoveGroupBan(object sender, CqGroupBanEventArgs e)
        {

        }
    }

    public class Event_AddGroup : IReceiveAddGroupRequest
    {
        /// <summary>
        /// 申请入群消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ReceiveAddGroupRequest(object sender, CqAddGroupRequestEventArgs e)
        {

        }
    }

    public class Event_GroupAdd : IReceiveAddGroupBeInvitee
    {
        /// <summary>
        /// 机器人被邀请事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ReceiveAddGroupBeInvitee(object sender, CqAddGroupRequestEventArgs e)
        {

        }
    }
    
    }

