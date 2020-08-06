using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IBoxs.Tool.Encryption
{
    public static class MD5
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string md5(string str)
        {
            byte[] result = Encoding.Default.GetBytes(str.Trim());  //tbPass为输入密码的文本框
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            string st = BitConverter.ToString(output).Replace("-", "").ToLower().Trim().ToString(); //
            // SplachForm f = new SplachForm();
            //   f.ShowDialog();
            return st;
        }
    }
}
