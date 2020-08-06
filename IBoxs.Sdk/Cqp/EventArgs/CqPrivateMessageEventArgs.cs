using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBoxs.Sdk.Cqp.EventArgs
{
	/// <summary>
	/// 表示私聊消息事件参数的类
	/// </summary>
	public class CqPrivateMessageEventArgs
	{
		/// <summary>
		/// 获取或设置一个值, 表示当前事件所产生消息的唯一编号, 可用于撤回消息
		/// </summary>
		public string MsgId { get; set; }
        /// <summary>
        /// 消息序号
        /// </summary>
        public string MsgNum { get; set; }
        /// <summary>
        /// 机器人QQ
        /// </summary>
        public long RobotQQ { get; set; }
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
        /// 初始化 <see cref="CqPrivateMessageEventArgs"/> 类的一个新实例
        /// </summary>
        /// <param name="id">事件ID</param>
        /// <param name="name">事件名称</param>
        /// <param name="msgId">消息ID</param>
        /// <param name="fromQQ">来源QQ</param>
        /// <param name="msg">消息内容</param>
        public CqPrivateMessageEventArgs(string msgId, string msgNum,long robotQQ, long fromQQ, string msg)
		{
			this.MsgId = msgId;
            this.RobotQQ = robotQQ;
            this.MsgNum = MsgNum;
			this.FromQQ = fromQQ;
			this.Message = msg;
		}
	}
}
