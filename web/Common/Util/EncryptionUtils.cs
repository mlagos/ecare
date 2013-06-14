using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Nextgal.ECare.Common.Util
{
    public class EncryptionUtils
    {
        private static readonly byte[] _key;
        private static readonly byte[] _initVector;

        private static DESCryptoServiceProvider _provider;

        static EncryptionUtils()
        {
            _provider = new DESCryptoServiceProvider();
            _key = Encoding.Default.GetBytes("jHJs3Hsd");
            _initVector = Encoding.Default.GetBytes("jHduDgdT");
        }


        public static string Encrypt(string contents)
        {
            ICryptoTransform transform = _provider.CreateEncryptor(_key, _initVector);

            byte[] bArray = Encoding.Default.GetBytes(contents);
            byte[] bOutput = transform.TransformFinalBlock(bArray, 0, bArray.Length);

            return Convert.ToBase64String(bOutput);
        }

        public static string Decrypt(string contents)
        {
            ICryptoTransform transform = _provider.CreateDecryptor(_key, _initVector);

            Convert.FromBase64String(contents);
            byte[] bArray = Convert.FromBase64String(contents);
            byte[] bOutput = transform.TransformFinalBlock(bArray, 0, bArray.Length);

            return Encoding.Default.GetString(bOutput);
        }
    }
}