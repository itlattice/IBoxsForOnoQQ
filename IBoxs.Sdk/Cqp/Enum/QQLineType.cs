using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBoxs.Sdk.Cqp.Enum
{
    /// <summary>
    /// QQ登录在线类型
    /// </summary>
    public enum QQLineType
    {
        /// <summary>
        /// 登录完成
        /// </summary>
        LoginComplete = 1101,
        /// <summary>
        /// 手动离线
        /// </summary>
        ManualOffline = 1102,
        /// <summary>
        /// 强制离线
        /// </summary>
        ForceOffline = 1103,
        /// <summary>
        /// QQ掉线
        /// </summary>
        OffLine = 1104,
        /// <summary>
        /// 被冻结离线
        /// </summary>
        FreezeOffline = 1105,
        /// <summary>
        /// 异常离线
        /// </summary>
        AbnormalOffline = 1106,
        /// <summary>
        /// QQ登录失败
        /// </summary>
        LoginFailed = 1107
    }
}
