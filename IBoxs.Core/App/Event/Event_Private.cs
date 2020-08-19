using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBoxs.Sdk.Cqp.EventArgs;
using System.Data;
using System.Windows.Forms;

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
            Common.CqApi.SendPrivateMessage(e.RobotQQ, e.FromQQ, Common.CqApi.CqCode_At(e.FromQQ) + " 你发送了这样的消息：" + e.Message);
            return 1;
        }
        /// <summary>
        /// 收到好友添加请求
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static int ReceiveFriendAddRequest(CqAddFriendRequestEventArgs e)
        { 
            return 1;
        }
        /// <summary>
        /// 收到转账消息
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static int ReceiveTranceAccounts(CqTransferAccountsEventArgs e)
        {
            return 1;
        }
    }
}
