using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using eSuperMap.Objects.Data;

namespace NC.HPS.Lib
{
    public class NCCryp
    {
        private static SymmetricAlgorithm mobjCryptoService = new RijndaelManaged();
        private static string Key="Guz(%&hj7x89H$yuBI0456FtmaT5&fvHUFCy77*h%(HilJ$lhj!y6&(*jkP87jH7";
        private static string productName = "HPS";
        /// <summary>
        /// 获得密钥
        /// </summary>
        /// <returns>密钥</returns>
        private static byte[] GetLegalKey()
        {
            string sTemp = Key;
            mobjCryptoService.GenerateKey();
            byte[] bytTemp = mobjCryptoService.Key;
            int KeyLength = bytTemp.Length;
            if (sTemp.Length > KeyLength)
            sTemp = sTemp.Substring(0, KeyLength);
            else if (sTemp.Length < KeyLength)
            sTemp = sTemp.PadRight(KeyLength, ' ');
            return ASCIIEncoding.ASCII.GetBytes(sTemp);
        }
        /// <summary>
        /// MD5
        /// </summary>
        /// <param name="dataBytes"></param>
        /// <returns></returns>
        public static string GetMd5HashValue(byte[] dataBytes)
        {
            try
            {
                MD5CryptoServiceProvider servProvider = new MD5CryptoServiceProvider();
                byte[] md5Bytes = servProvider.ComputeHash(dataBytes);

                StringBuilder sb = new StringBuilder(32);
                foreach (byte md5Data in md5Bytes)
                {
                    sb.Append(md5Data.ToString("X2"));
                }

                return sb.ToString();
            }
            catch (Exception e)
            {
                //string logMsg = "MD5计算失败！";
                NCLogger.GetInstance().WriteExceptionLog(e);
            }

            return null;
        }
        /// <summary>
        /// 许可检查
        /// </summary>
        public static string getLic(string device_id, string system_id)
        {
            return GetMd5HashValue(Encoding.Default.GetBytes("highway" + device_id + "kl" + system_id));
        }


        /// <summary>
        /// 获得初始向量IV
        /// </summary>
        /// <returns>初试向量IV</returns>
        private static byte[] GetLegalIV()
        {
            string sTemp = "E4ghj*Ghg7!rNIfb&95GUY86GfghUb#er57HBh(u%g6HJ($jhWk7&!hg4ui%$hjk";
            mobjCryptoService.GenerateIV();
            byte[] bytTemp = mobjCryptoService.IV;
            int IVLength = bytTemp.Length;
            if (sTemp.Length > IVLength)
            sTemp = sTemp.Substring(0, IVLength);
            else if (sTemp.Length < IVLength)
            sTemp = sTemp.PadRight(IVLength, ' ');
            return ASCIIEncoding.ASCII.GetBytes(sTemp);
        }
        /// <summary>
        /// 加密方法
        /// </summary>
        /// <param name="Source">待加密的串</param>
        /// <returns>经过加密的串</returns>
        public static string Encrypto(string Source)
        {
            byte[] bytIn = UTF8Encoding.UTF8.GetBytes(Source);
            MemoryStream ms = new MemoryStream();
            mobjCryptoService.Key = GetLegalKey();
            mobjCryptoService.IV = GetLegalIV();
            ICryptoTransform encrypto = mobjCryptoService.CreateEncryptor();
            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);
            cs.Write(bytIn, 0, bytIn.Length);
            cs.FlushFinalBlock();
            ms.Close();
            byte[] bytOut = ms.ToArray();
            string ret = Convert.ToBase64String(bytOut);
            return ret.Replace("+","%2B");
        }
        /// <summary>
        /// 解密方法
        /// </summary>
        /// <param name="Source">待解密的串</param>
        /// <returns>经过解密的串</returns>
        public static string Decrypto(string Source)
        {
            byte[] bytIn = Convert.FromBase64String(Source.Replace("%2B", "+"));
            MemoryStream ms = new MemoryStream(bytIn, 0, bytIn.Length);
            mobjCryptoService.Key = GetLegalKey();
            mobjCryptoService.IV = GetLegalIV();
            ICryptoTransform encrypto = mobjCryptoService.CreateDecryptor();
            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }
        /// <summary>
        /// 取得本系统产品编号
        /// </summary>
        /// <returns></returns>
        public static string getProductID()
        {
            try
            {
                string deviceId = Toolkit.GetDeviceID();
                string productId = Encrypto(deviceId + productName);
                return productId;
            }
            catch (Exception ex)
            {
                NCLogger.GetInstance().WriteExceptionLog(ex);
            }
            return null;
        }
        /// <summary>
        /// 取得本系统产品编号
        /// </summary>
        /// <returns></returns>
        public static string getProductID(string product)
        {
            try
            {
                string deviceId = Toolkit.GetDeviceID();
                string productId = Encrypto(deviceId + product);
                return productId;
            }
            catch (Exception ex)
            {
                NCLogger.GetInstance().WriteExceptionLog(ex);
            }
            return null;
        }
        /// <summary>
        /// 从产品ID取得硬件ID
        /// </summary>
        /// <returns></returns>
        public static string getHardIDFromProductId(string productID)
        {
            try
            {
                String hardId = Decrypto(productID);
                if (hardId.LastIndexOf(productName) > -1)
                {
                    return hardId.Substring(0, hardId.LastIndexOf(productName));
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                NCLogger.GetInstance().WriteExceptionLog(ex);
            }
            return null;
        }
        /// <summary>
        /// 从产品ID取得硬件ID
        /// </summary>
        /// <returns></returns>
        public static string getHardIDFromProductId(string productID,string product)
        {
            try
            {
                String hardId = Decrypto(productID);
                if (hardId.LastIndexOf(product) > -1)
                {
                    return hardId.Substring(0, hardId.LastIndexOf(product));
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                NCLogger.GetInstance().WriteExceptionLog(ex);
            }
            return null;
        }
        /// <summary>
        /// 许可取得
        /// </summary>
        public static string getLic(string hardId)
        {
            return GetMd5HashValue(Encoding.Default.GetBytes("highway" + hardId + "kl" + productName));
        }
        /// <summary>
        /// 许可检查
        /// </summary>
        public static Boolean checkLic(string Lic)
        {
            try
            {
                string deviceId = Toolkit.GetDeviceID();
                string lic = GetMd5HashValue(Encoding.Default.GetBytes("highway" + deviceId + "kl" + productName));
                if (lic == Lic) return true;
            }
            catch (Exception ex)
            {
                NCLogger.GetInstance().WriteExceptionLog(ex);
            }
            return false;

        }
        /// <summary>
        /// 许可检查
        /// </summary>
        public static Boolean checkLic(string Lic,string system_id)
        {
            try
            {
                string deviceId = Toolkit.GetDeviceID();
                string lic = GetMd5HashValue(Encoding.Default.GetBytes("highway" + deviceId + "kl" + system_id));
                if (lic == Lic) return true;
            }
            catch (Exception ex)
            {
                NCLogger.GetInstance().WriteExceptionLog(ex);
            }
            return false;

        }

    }
}
