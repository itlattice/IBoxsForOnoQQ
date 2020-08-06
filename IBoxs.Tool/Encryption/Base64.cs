using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBoxs.Tool.Encryption
{
    public static class Base64
    {
        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string Base64Encode(string source)//Encoding encodeType, 
        {
            string encode = string.Empty;
            byte[] bytes = (Encoding.UTF8.GetBytes(source));//encodeType.GetBytes(source);
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = source;
            }
            return encode;
        }
    }
}
