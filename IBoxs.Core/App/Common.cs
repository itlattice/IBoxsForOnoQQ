using IBoxs.Sdk.Cqp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity;

namespace IBoxs.Core.App
{
	/// <summary>
	/// 用于存放 App 数据的公共类
	/// </summary>
	public static class Common
    {
        /// <summary>
        /// 获取应用名称
        /// </summary>
        public static string AppName { get; set; }
        /// <summary>
        /// 应用版本号
        /// </summary>
        public static string AppVersion { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public static string Author { get; set; }

        public static string Description { get; set; }

        /// <summary>
        /// Skey
        /// </summary>
        public static string skey
        {
            get
            {
                return "8956RTEWDFG3216598WERDF3";
            }
            set { }
        }
        
        public static string SDK { get; set; }

        /// <summary>
		/// 获取或设置 App 在运行期间所使用的数据路径
		/// </summary>
		public static string AppDirectory { get; set; }
        
        /// <summary>
        /// 获取或设置当前 App 使用的依赖注入容器实例
        /// </summary>
        public static IUnityContainer UnityContainer { get; set; }

        public static CqApi CqApi=new CqApi();
    }
}
