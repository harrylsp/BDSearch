using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace Common
{
    public class Util
    {
        /// <summary>
        /// 获取当前exe的路径
        /// </summary>
        /// <returns></returns>
        public static string GetAppExePath()
        {
            string exeFullName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            return Path.GetDirectoryName(exeFullName);
        }

        /// <summary>
        /// 物理地址
        /// </summary>
        /// <returns></returns>
        public static string GetMacAddress()
        {
            try
            {
                //获取网卡硬件地址
                string mac = "";
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        mac = mo["MacAddress"].ToString();
                        break;
                    }
                }
                moc = null;
                mc = null;
                return mac;
            }
            catch (Exception ex)
            {
                return "unknow";
            }
        }

        /// <summary>
        /// MD5
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetMd5Hash(string input)
        {
            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();
            // Create a new instance of the MD5CryptoServiceProvider object.
            using (MD5 md5Hasher = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
            }
            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        /// <summary>
        /// 生成机器码
        /// </summary>
        /// <returns></returns>
        public static string GenerateMachineCode(string userName, string password)
        {
            var mac = GetMacAddress();
            var md5 = GetMd5Hash(userName + "[lsp]" + password + "[lsp]" + mac);

            return md5;
        }

        /// <summary>
        /// 时间戳
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static long TryDateTimeToLong(DateTime dt)
        {
            DateTime dtStart = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.Local);
            TimeSpan toNow = dt.Subtract(dtStart);
            return Convert.ToInt64(toNow.TotalSeconds);
        }

        /// <summary>
        /// 创建Key文件 做合法性软件使用校验
        /// <paramref name="key"></paramref>
        /// </summary>
        public static void CreateKeyFile(string key = "加密文件")
        {
            var filePath = Path.Combine(GetAppExePath(), "key.data");
            if (!File.Exists(filePath))
            {
                using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF7))
                {
                    byte[] b = Encoding.UTF7.GetBytes(key);
                    sw.Write(Encoding.UTF7.GetString(b));
                }
            }
        }

        /// <summary>
        /// 获取Key文件内容
        /// </summary>
        /// <returns></returns>
        public static string GetKeyFile()
        {
            var filePath = Path.Combine(GetAppExePath(), "key.data");
            if (!File.Exists(filePath))
            {
                return "";
            }

            using (StreamReader sr = new StreamReader(filePath, Encoding.UTF7))
            {
                var b = Encoding.UTF7.GetBytes(sr.ReadToEnd());

                return Encoding.UTF7.GetString(b);
            }
        }

        /// <summary>
        /// 本地合法性校验
        /// </summary>
        /// <param name="isLogin"></param>
        /// <returns></returns>
        public static bool VerifyUser(string isLogin)
        {
            var filePath = Path.Combine(GetAppExePath(), "key.data");

            if (isLogin == "1")
            {
                if (!File.Exists(filePath))
                {
                    return false;
                }

                if (GetKeyFile() != GetMacAddress())
                {
                    return false;
                }
            }

            return true;

        }
    }
}
