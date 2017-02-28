/*******************************************************************************
 * Copyright © 2017 EquipManage.Framework 版权所有
 * Author: 王成元
 * Description: 设备管理系统-匠盟科技
 * Date:2017-02-17
*********************************************************************************/
using System.Security.Cryptography;
using System.Text;

namespace EquipManage.Code
{
    /// <summary>
    /// MD5加密
    /// </summary>
    public class Md5
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str">加密字符</param>
        /// <param name="code">加密位数16/32</param>
        /// <returns></returns>
        public static string md5(string str, int code)
        {
            string strEncrypt = string.Empty;
            if (code == 16)
            {
                //MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
                //byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(str));
                //StringBuilder sBuilder = new StringBuilder();
                //for (int i = 0; i < data.Length; i++)
                //{
                //    sBuilder.Append(data[i].ToString("x2"));
                //}
                ////return sBuilder.ToString();
                //strEncrypt = sBuilder.ToString().Substring(8, 16);
                strEncrypt = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").Substring(8, 16);
            }

            if (code == 32)
            {
                //MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
                //byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(str));
                //StringBuilder sBuilder = new StringBuilder();
                //for (int i = 0; i < data.Length; i++)
                //{
                //    sBuilder.Append(data[i].ToString("x2"));
                //}
                ////return sBuilder.ToString();
                //strEncrypt = sBuilder.ToString();
                strEncrypt = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");
            }

            return strEncrypt;
        }
    }
}
