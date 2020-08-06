using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBoxs.Sdk.Cqp.EventArgs
{
	/// <summary>
	/// 表示群成员增加事件参数的类
	/// </summary>
	public class CqGroupMemberIncreaseEventArgs
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
		/// 初始化 <see cref="CqGroupMemberIncreaseEventArgs"/> 类的一个新实例
		/// </summary>
		/// <param name="sendTime">发送时间</param>
		/// <param name="fromGroup">来源群</param>
		/// <param name="fromQQ">操作者QQ</param>
		/// <param name="operateQQ">被操作QQ</param>
		public CqGroupMemberIncreaseEventArgs (DateTime sendTime,long robotQQ, long fromGroup, long fromQQ, long operateQQ)
		{
			this.SendTime = sendTime;
            this.RobotQQ = robotQQ;
			this.FromGroup = fromGroup;
			this.FromQQ = fromQQ;
			this.BeingOperateQQ = operateQQ;
		}
	}
}
