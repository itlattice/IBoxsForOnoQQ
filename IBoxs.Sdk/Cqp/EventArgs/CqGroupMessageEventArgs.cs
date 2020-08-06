using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBoxs.Sdk.Cqp.EventArgs
{
    /// <summary>
	/// 表示群组消息事件参数的类
	/// </summary>
	public class CqGroupMessageEventArgs
    {
        /// <summary>
        /// 获取或设置一个值, 表示当前事件所产生消息的唯一编号, 可用于撤回消息
        /// </summary>
        public int MsgId { get; set; }
        /// <summary>
        /// 机器人QQ
        /// </summary>
        public long RobotQQ { get; set; }
        /// <summary>
        /// 消息序号
        /// </summary>
        public int MsgNum { get; set; }
        /// <summary>
        /// 获取当前消息的来源群组号
        /// </summary>
        public long FromGroup { get; private set; }

        /// <summary>
        /// 获取当前消息的来源QQ号
        /// </summary>
        public long FromQQ { get; private set; }
        
        /// <summary>
        /// 获取当前消息的消息内容
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// 获取或设置一个值, 指示当前是否处理过此事件. 若此值为 True 将停止处理后续事件
        /// </summary>
        public bool Handler { get; set; }

        /// <summary>
        /// 初始化 <see cref="CqGroupMessageEventArgs"/> 类的一个新实例
        /// </summary>
        /// <param name="name">事件名称</param>
        /// <param name="msgId">消息ID</param>
        /// <param name="fromGroup">来源群组</param>
        /// <param name="fromQQ">来源QQ</param>
        /// <param name="anonymous">来源匿名</param>
        /// <param name="msg">消息</param>
        public CqGroupMessageEventArgs(string name, int msgId,int msgNum,long robotQQ, long fromGroup, long fromQQ, string msg)
        {
            this.MsgId = msgId;
            this.MsgNum = msgNum;
            this.RobotQQ = robotQQ;
            this.FromGroup = fromGroup;
            this.FromQQ = fromQQ;
            this.Message = msg;
        }
    }
}
