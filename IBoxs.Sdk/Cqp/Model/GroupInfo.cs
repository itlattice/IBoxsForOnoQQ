using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBoxs.Sdk.Cqp.Model
{
    public class GroupInfo
    {
        /// <summary>
		/// 群号码
		/// </summary>
		public long Id { get; set; }
        /// <summary>
        /// 群名字
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 当前成员数量, 由 <see cref="CqApi.GetGroupInfo"/> 方法获取
        /// </summary>
        public int CurrentNumber { get; set; }
        /// <summary>
        /// 群主QQ
        /// </summary>
        public string owner { get; set; }
        /// <summary>
        /// 群等级
        /// </summary>
        public int GroupLavel { get; set; }
        /// <summary>
        /// 群最大人数
        /// </summary>
        public int MaximumNumber { get; set; }
    }
}
