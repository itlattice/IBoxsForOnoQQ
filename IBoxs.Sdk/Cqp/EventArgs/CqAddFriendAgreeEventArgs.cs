﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBoxs.Sdk.Cqp.EventArgs
{
    /// <summary>
    /// 表示添加好友请求事件参数的类
    /// </summary>
    public class CqAddFriendAgreeEventArgs
    {
        /// <summary>
        /// 获取当前事件触发时间
        /// </summary>
        public DateTime SendTime { get; private set; }

        /// <summary>
        /// 获取当前消息的来源QQ号
        /// </summary>
        public long FromQQ { get; private set; }
        /// <summary>
        /// 机器人QQ
        /// </summary>
        public long RobotQQ { get; set; }

        /// <summary>
        /// 获取或设置一个值, 指示当前是否处理过此事件. 若此值为 True 将停止处理后续事件
        /// </summary>
        public bool Handler { get; set; }

        /// <summary>
        /// 获取或设置一个值, 指示当前是否处理过此事件. 若此值为 True 将停止处理后续事件
        /// </summary>
        public bool Agree { get; set; }

        /// <summary>
        /// 初始化 <see cref="CqAddFriendRequestEventArgs"/> 类的一个新实例
        /// </summary>
        /// <param name="id">事件ID</param>
        /// <param name="name">事件名称</param>
        /// <param name="sendTime">触发时间</param>
        /// <param name="fromQQ">来源QQ</param>
        /// <param name="message">附加消息</param>
        /// <param name="flag">反馈标识</param>
        public CqAddFriendAgreeEventArgs(DateTime sendTime,bool Agree,long robotQQ, long fromQQ)
        {
            this.SendTime = sendTime;
            this.RobotQQ = robotQQ;
            this.FromQQ = fromQQ;
            this.Agree = Agree;
        }
    }
}
