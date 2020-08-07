using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Threading;
using System.IO;
using IBoxs.Core;
using System.Windows.Forms;
using IBoxs.Tool;
using System.Data.SQLite;

namespace IBoxs.Core.App.Event
{
    public class Event_AppMain
    {
        /// <summary>
        /// 酷Q启动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static int CqStartup()
        {
            return 1;
        }
        
        /// <summary>
        /// 应用被启用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static int CqAppEnable()
        {
            return 1;
        }
        
        public static int CqAppDisable()
        {
            return 1;
        }
        
        /// <summary>
        /// 菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static int CallMenu()
        {
            return 1;
        }
    }
}
