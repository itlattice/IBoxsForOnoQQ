using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBoxs.Sdk.Cqp.Enum
{
	/// <summary>
	/// 转账收款状态
	/// </summary>
	public enum CashType
	{
		/// <summary>
		/// 等待收款
		/// </summary>
		Wait = 1,
		/// <summary>
		/// 已收款
		/// </summary>
		Success = 0
	}
}
