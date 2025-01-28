using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace DPAPI
{
    public partial class DPAPIHelper
    {
        // 用於用戶範圍的加密
        public string? Encrypt(string data)
        {
            try
            {
                byte[] dataBytes = Encoding.UTF8.GetBytes(data);
                byte[] encryptedData = ProtectedData.Protect(dataBytes, null, DataProtectionScope.CurrentUser);
                return Convert.ToBase64String(encryptedData);
            }
            catch (Exception ex)
            {
                Console.WriteLine("加密失敗: " + ex.Message);
                return null;
            }
        }
        public string? Encrypt(byte[] dataBytes)
        {
            try
            {
                byte[] encryptedData = ProtectedData.Protect(dataBytes, null, DataProtectionScope.CurrentUser);
                return Convert.ToBase64String(encryptedData);
            }
            catch (Exception ex)
            {
                Console.WriteLine("加密失敗: " + ex.Message);
                return null;
            }
        }

        // 用於用戶範圍的解密
        public string? Decrypt(string encryptedData)
        {
            try
            {
                byte[] encryptedBytes = Convert.FromBase64String(encryptedData);
                byte[] decryptedData = ProtectedData.Unprotect(encryptedBytes, null, DataProtectionScope.CurrentUser);
                return Encoding.UTF8.GetString(decryptedData);
            }
            catch (Exception ex)
            {
                Console.WriteLine("解密失敗: " + ex.Message);
                return null;
            }
        }

        // 創建一個亂數的密鑰
        public string CreateKey(int length = 16)
        {
            using var rng = RandomNumberGenerator.Create();
            byte[] keyBytes = new byte[length];
            rng.GetNonZeroBytes(keyBytes);
            return Convert.ToBase64String(keyBytes);
        }
    }
}