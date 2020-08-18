using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBoxs.Sdk.Cqp.EventArgs
{
    /// <summary>
    /// 表示添加群请求事件参数的类
    /// </summary>
    public class CqAddGroupRequestEventArgs
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
        /// 获取当前消息的消息内容
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// 反馈标识, 用于对该请求做响应时的标识参数
        /// </summary>
        public string ResponseFlag { get; private set; }
        
        /// <summary>
        /// 初始化 <see cref="CqAddGroupRequestEventArgs"/> 类的一个新实例
        /// </summary>
        /// <param name="sendTime">触发时间</param>
        /// <param name="fromGroup">来源群组</param>
        /// <param name="fromQQ">来源QQ</param>
        /// <param name="message">附加消息</param>
        /// <param name="flag">反馈标识</param>
        public CqAddGroupRequestEventArgs (DateTime sendTime, long fromGroup,long robotQQ, long fromQQ, string message, string flag)
        {
            this.SendTime = sendTime;
            this.FromGroup = fromGroup;
            this.RobotQQ = robotQQ;
            this.FromQQ = fromQQ;
            this.Message = message;
            this.ResponseFlag = flag;
        }
    }
}
