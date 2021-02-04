﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBoxs.Sdk.Cqp.EventArgs
{
    /// <summary>
    /// 表示群成员减少事件参数的类
    /// </summary>
    public class CqGroupMemberDecreaseEventArgs
    {
        /// <summary>
        /// 获取当前事件触发时间
        /// </summary>
        public DateTime SendTime { get; private set; }

        /// <summary>
        /// 获取当前消息的来源群组号
        /// </summary>
        public long FromGroup { get; private set; }
        /// <summary>
        /// 机器人QQ
        /// </summary>
        public long RobotQQ { get; set; }
        /// <summary>
        /// 获取当前消息的来源QQ号
        /// </summary>
        public long FromQQ { get; private set; }

        /// <summary>
        /// 获取当前事件触发时的目标QQ
        /// </summary>
        public long BeingOperateQQ { get; private set; }

        /// <summary>
        /// 获取或设置一个值, 指示当前是否处理过此事件. 若此值为 True 将停止处理后续事件
        /// </summary>
        public bool Handler { get; set; }

        /// <summary>
        /// 初始化 <see cref="CqGroupMemberDecreaseEventArgs"/> 类的一个新实例
        /// </summary>
        /// <param name="id">事件ID</param>
        /// <param name="name">事件名称</param>
        /// <param name="sendTime">发送时间</param>
        /// <param name="fromGroup">来源群</param>
        /// <param name="fromQQ">操作者QQ</param>
        /// <param name="operateQQ">被操作QQ</param>
        public CqGroupMemberDecreaseEventArgs (DateTime sendTime, long fromGroup,long robotQQ, long fromQQ, long operateQQ)
        {
            this.SendTime = sendTime;
            this.RobotQQ = robotQQ;
            this.FromGroup = fromGroup;
            this.FromQQ = fromQQ;
            this.BeingOperateQQ = operateQQ;
        }
    }
}
