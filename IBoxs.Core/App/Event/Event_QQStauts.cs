using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBoxs.Sdk.Cqp.EventArgs;

namespace IBoxs.Core.App.Event
{
    public class Event_QQStauts
    {
        /// <summary>
        /// 机器人QQ状态变化事件
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static int LoginSucess(CqQQStuatsChangeEventArgs e)
        {
            return 1;
        }
    }
}
