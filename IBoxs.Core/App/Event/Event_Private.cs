using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBoxs.Sdk.Cqp.EventArgs;
using System.Data;

namespace IBoxs.Core.App.Event
{
    public class Event_Private
    {
        /// <summary>
        /// 好友消息事件接口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static int ReceiveFriendMessage(CqPrivateMessageEventArgs e)
        {
            Common.CqApi.SendPrivateMessage(e.RobotQQ, e.FromQQ, e.Message);
            return 1;
        }

        /// <summary>
        /// 收到好友添加请求
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static int ReceiveFriendAddRequest(CqAddFriendRequestEventArgs e)
        {
            Common.CqApi.SendPrivateMessage(e.RobotQQ, 320587491, "收到好友添加请求"+e.FromQQ.ToString());
            return 1;
        }
    }
}
