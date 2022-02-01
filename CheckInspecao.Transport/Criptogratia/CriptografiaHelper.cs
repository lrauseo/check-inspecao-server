
using CheckInspecao.Transport.DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace CheckInspecao.Transport.Criptogratia
{
    public class CriptografiaHelper
    {
        public readonly CriptografiaConfigurationDTO Configuration;
        
        public CriptografiaHelper(IOptions<CriptografiaConfigurationDTO> config)
        {
            Configuration = config.Value;
            
        }
        public string AesEncrypt(string dados)
        {
            try
            {
                string textToEncrypt = dados;
                string ToReturn = "";
                string publickey = Configuration.PublicKey;
                string secretkey = Configuration.PrivateKey;
                byte[] secretkeyByte = { };
                secretkeyByte = System.Text.Encoding.UTF8.GetBytes(secretkey);
                byte[] publickeybyte = { };
                publickeybyte = System.Text.Encoding.UTF8.GetBytes(publickey);
                MemoryStream ms = null;
                CryptoStream cs = null;
                byte[] inputbyteArray = System.Text.Encoding.UTF8.GetBytes(textToEncrypt);
                using (AesManaged des = new AesManaged())
                {                    
                    des.Padding = PaddingMode.PKCS7;
                    des.Mode = CipherMode.CBC;
                    ms = new MemoryStream();
                    cs = new CryptoStream(ms, des.CreateEncryptor(publickeybyte, secretkeyByte), CryptoStreamMode.Write);
                    cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                    cs.FlushFinalBlock();
                    ToReturn = Convert.ToBase64String(ms.ToArray());
                }
                return ToReturn;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public string AesDecrypt(string dados)
        {
            try
            {
                string textToDecrypt = dados;
                string ToReturn = "";
                string publickey = Configuration.PublicKey;
                string secretkey = Configuration.PrivateKey;
                byte[] privatekeyByte = System.Text.Encoding.UTF8.GetBytes(secretkey);
                byte[] publickeybyte = System.Text.Encoding.UTF8.GetBytes(publickey);
                MemoryStream ms = null;
                CryptoStream cs = null;
                byte[] inputbyteArray = new byte[textToDecrypt.Length];
                inputbyteArray = Convert.FromBase64String(textToDecrypt);
                using (AesManaged  aes = new AesManaged())
                {                              
                    aes.Padding = PaddingMode.PKCS7;
                    aes.Mode = CipherMode.CBC;
                    ms = new MemoryStream();
                    cs = new CryptoStream(ms, aes.CreateDecryptor(publickeybyte, privatekeyByte), CryptoStreamMode.Write);
                    cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                    cs.FlushFinalBlock();
                    Encoding encoding = Encoding.UTF8;
                    ToReturn = encoding.GetString(ms.ToArray());
                }
                return ToReturn;
            }
            catch (Exception ae)
            {
                throw new Exception(ae.Message, ae.InnerException);
            }

        }
        public string HMacSha256Encrypt(string dados)
        {
            var data = dados;
            var secretKey = Configuration.SecretKey;
            // Initialize the keyed hash object using the secret key as the key
            HMACSHA256 hashObject = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey));

            // Computes the signature by hashing the salt with the secret key as the key
            var signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(data));

            // Base 64 Encode
            var encodedSignature = Convert.ToBase64String(signature);
            return encodedSignature;

        }
    }
}
