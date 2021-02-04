using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBoxs.Sdk.Cqp.Enum
{
    /// <summary>
    /// 加好友验证类型
    /// </summary>
    public enum RequestSignType
    {
        /// <summary>
        /// 允许任何人
        /// </summary>
        Everyone = 0,
        /// <summary>
        /// 需要身份验证
        /// </summary>
        Authentication = 1,
        /// <summary>
        /// 需正确回答问题
        /// </summary>
        Answer = 3,
        /// <summary>
        /// 需要回答问题
        /// </summary>
        Question = 4,
        /// <summary>
        /// 已经是好友
        /// </summary>
        isFriend = 99,
        /// <summary>
        /// 未知原因
        /// </summary>
        None = -1
    }
}
