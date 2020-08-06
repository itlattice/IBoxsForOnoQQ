using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IBoxs.Tool.IniConfig
{
   public static class IOIniConfig
    {
        [DllImport("kernel32")]
        private static extern long GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filepath);
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filepath);

        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string readIni(string section, string key,string fileName)//读配置文件
        {
            if (File.Exists(fileName))
            {
                StringBuilder temp = new StringBuilder(1024);
                GetPrivateProfileString(section, key, "1", temp, 1024, fileName);
                return temp.ToString().Trim();
            }
            else
            {
                return "1";
            }
        }

        /// <summary>
        /// 写入配置文件
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static long writeini(string section, string key, string value,string filePath)
        {
            return WritePrivateProfileString(section, key, value, filePath);
        }
    }
}
