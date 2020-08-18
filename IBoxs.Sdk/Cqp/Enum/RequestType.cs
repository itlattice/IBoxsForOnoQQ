using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBoxs.Sdk.Cqp.Enum
{
    /// <summary>
    /// 请求类型
    /// </summary>
   public enum RequestType
    {
        /// <summary>
        /// 进群申请
        /// </summary>
        GroupAdd=213,
        /// <summary>
        /// 机器人被被邀请进群
        /// </summary>
        GroupInvitation=214,
        /// <summary>
        /// 某人被邀请进群
        /// </summary>
        MemberInvitation=215,
        /// <summary>
        /// 某人请求加为好友
        /// </summary>
        FriendAdd=101
    }
}
