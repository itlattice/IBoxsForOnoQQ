using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBoxs.Sdk.Cqp.Interface;
using IBoxs.Sdk.Cqp.EventArgs;
using System.Data;

namespace IBoxs.Core.App.Event
{
    public class Event_DiscussMsg : IReceiveDiscussMessage
    {
        /// <summary>
        /// 讨论组消息事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ReceiveDiscussMessage(object sender, CqDiscussMessageEventArgs e)
        {

        }
    }


    public class Event_DiscussPMsg : IReceiveDiscussPrivateMessage
    {
        /// <summary>
        /// 讨论组私聊消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ReceiveDiscussPrivateMessage(object sender, CqPrivateMessageEventArgs e)
        {

        }
    }
}
