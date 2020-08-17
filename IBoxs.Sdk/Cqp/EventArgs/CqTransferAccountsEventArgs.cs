using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBoxs.Sdk.Cqp.Enum;

namespace IBoxs.Sdk.Cqp.EventArgs
{
    /// <summary>
    /// 表示转账消息事件参数的类
    /// </summary>
    public class CqTransferAccountsEventArgs
    {
        /// <summary>
        /// 机器人QQ
        /// </summary>
        public long RobotQQ { get; set; }
        /// <summary>
        /// 获取当前消息的来源QQ号
        /// </summary>
        public long FromQQ { get; private set; }

        /// <summary>
        /// 金额
        /// </summary>
        public double Money { get; private set; }
        /// <summary>
        /// 账单号
        /// </summary>
        public string OrderId { get; private set; }
        /// <summary>
        /// 留言内容
        /// </summary>
        public string Note { get; private set; }
        /// <summary>
        /// 收款状态
        /// </summary>
        public CashType Stauts { get; private set; }

        /// <summary>
        /// 初始化 <see cref="CqPrivateMessageEventArgs"/> 类的一个新实例
        /// </summary>
        /// <param name="id">事件ID</param>
        /// <param name="name">事件名称</param>
        /// <param name="msgId">消息ID</param>
        /// <param name="fromQQ">来源QQ</param>
        /// <param name="msg">消息内容</param>
        public CqTransferAccountsEventArgs(long robotQQ, long fromqq,int stauts, string json)
        {
            this.RobotQQ = robotQQ;
            this.FromQQ = fromqq;
            Root rt= JsonConvert.DeserializeObject<Root>(json);
            this.Money = rt.Money;
            this.OrderId = rt.ID;
            this.Note = rt.Msg;
            if (stauts == 0)
                this.Stauts = CashType.Success;
            else
                this.Stauts = CashType.Wait;
        }

        public class Root
        {
            public double Money { get; set; }
            public string ID { get; set; }
            public string Msg { get; set; }
        }
    }
}
