using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBoxs.Sdk.Cqp.Interface;
using IBoxs.Sdk.Cqp.EventArgs;
using System.Data;
using DataHandle;

namespace IBoxs.Core.App.Event
{
    public class Event_PrivateMsg : IReceiveFriendMessage
    {
        /// <summary>
        /// 好友消息事件接口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ReceiveFriendMessage(object sender, CqPrivateMessageEventArgs e)
        {
            if (e.FromQQ < 0)
                return;
            if (e.Handler)
                return;
            string error = null;
            if (!DataHandle.GetData.GetMicData.QurBool(out error))
            {
                Common.CqApi.SendPrivateMessage(e.FromQQ, "机器人授权异常，原因【" + error + "】，请联系管理员");
                return;
            }
            if (DataHandle.Common.isRuning && DataHandle.PublicData.PublicData.FunctionBool.Private)
            {
                Task t1 = new Task(() => PrivateMsg(e));
                t1.Start();
                e.Handler = true;
            }
        }

        public static void PrivateMsg(CqPrivateMessageEventArgs e)
        {
            /* 1.检查客服系统是否在运转中
             * 2.传入官方指令处理层（改授权、拿授权码等）
             * 3.传入指令处理层处理
             */
            if (DataHandle.Customer.CurtomerHandleClass.CurtomerHandleMain(e.FromQQ, e.Message))
            {
                return;
            }
            string reply = null;
            reply = Handle.AdminMsgHandle.MsgHandleMain(e.FromQQ, e.Message);
            if (reply != null)
            {
                if (reply == "n")
                    return;
                SendMsg.SendPrivateMsg(e.FromQQ, reply);
                return;
            }
            /******************************/
            reply = Handle.MsgHandle.MsgHandleMain(e.FromQQ, e.Message);
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

    public class Event_FriendAdd : IReceiveFriendAddRequest
    {
        /// <summary>
        /// 收到好友添加请求
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ReceiveFriendAddRequest(object sender, CqAddFriendRequestEventArgs e)
        {

        }
    }

    public class Event_AddFriend : IReceiveFriendIncrease
    {
        /// <summary>
        /// 好友已添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ReceiveFriendIncrease(object sender, CqFriendIncreaseEventArgs e)
        {

        }
    }
}
