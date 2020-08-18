using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBoxs.Sdk.Cqp.Enum;

namespace IBoxs.Sdk.Cqp.EventArgs
{
   public class CqQQStuatsChangeEventArgs
    {
        /// <summary>
        /// 机器人QQ
        /// </summary>
        public long RobotQQ { get; set; }
        /// <summary>
        /// 在线类型
        /// </summary>
        public QQLineType LineType { get; private set; }

        /// <summary>
        /// 初始化 <see cref="CqQQStuatsChangeEventArgs"/> 类的一个新实例
        /// </summary>
        /// <param name="id">事件ID</param>
        /// <param name="name">事件名称</param>
        /// <param name="msgId">消息ID</param>
        /// <param name="fromQQ">来源QQ</param>
        /// <param name="msg">消息内容</param>
        public CqQQStuatsChangeEventArgs(long robotQQ, int type)
        {
            this.RobotQQ = robotQQ;
            switch (type)
            {
                case 1101:this.LineType = QQLineType.LoginComplete;break;
                case 1102:this.LineType = QQLineType.ManualOffline;break;
                case 1103: this.LineType = QQLineType.ForceOffline; break;
                case 1104: this.LineType = QQLineType.OffLine; break;
                case 1105: this.LineType = QQLineType.FreezeOffline; break;
                case 1106: this.LineType = QQLineType.AbnormalOffline; break;
                case 1107: this.LineType = QQLineType.LoginFailed; break;
            }
        }
    }
}
