using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Threading;
using IBoxs.Sdk.Cqp.EventArgs;
using System.Windows.Forms;

namespace IBoxs.Core.App.Event
{
    public class Event_Group
    {
        /// <summary>
        /// 群消息事件
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static int ReceiveGroupMessage(CqGroupMessageEventArgs e)
        {
            return 1;
        }

        /// <summary>
        /// 群成员增加：被邀入群
        /// </summary>
        /// <param name="e"></param>
        public static int ReceiveGroupMemberBeInvitee(CqGroupMemberIncreaseEventArgs e)
        {
            return 1;
        }

        /// <summary>
        /// 群成员减少：成员离开
        /// </summary>
        /// <param name="e"></param>
        public static int ReceiveGroupMemberLeave(CqGroupMemberDecreaseEventArgs e)
        {
            return 1;
        }

        /// <summary>
        /// 群成员增加：主动入群
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static int ReceiveGroupMemberPass(CqGroupMemberIncreaseEventArgs e)
        {
            return 1;
        }

        /// <summary>
        /// 群成员减少：被移除
        /// </summary>
        /// <param name="e"></param>
        public static int ReceiveGroupMemberRemove(CqGroupMemberDecreaseEventArgs e)
        {
            return 1;
        }

        /// <summary>
        /// 群私聊消息
        /// </summary>
        /// <param name="e"></param>
        public static int ReceiveGroupPrivateMessage(CqGroupPrivateMessageEventArgs e)
        {
            return 1;
        }
        
        /// <summary>
        /// 申请入群消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static int ReceiveAddGroupRequest(CqAddGroupRequestEventArgs e)
        {
            return 1;
        }

        /// <summary>
        /// 机器人被邀请事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static int ReceiveAddGroupBeInvitee(CqAddGroupRequestEventArgs e)
        {
            return 1;
        }
    }
}

