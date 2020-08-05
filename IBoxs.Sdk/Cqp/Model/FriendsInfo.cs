using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBoxs.Sdk.Cqp.Model
{
    /// <summary>
	/// 描述 QQ 好友信息的类
	/// </summary>
	public class FriendInfo
    {
        /// <summary>
        /// QQ帐号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// QQ昵称
        /// </summary>
        public string Nick { get; set; }
    }
}
