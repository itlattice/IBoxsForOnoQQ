using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBoxs.Sdk.Cqp.Enum
{
   public enum ResponseType
    {
        /// <summary>
		/// 通过
		/// </summary>
		PASS = 10,
        /// <summary>
        /// 不通过
        /// </summary>
        FAIL = 20,
        /// <summary>
        /// 忽略
        /// </summary>
        IGNORE=30
    }
}
