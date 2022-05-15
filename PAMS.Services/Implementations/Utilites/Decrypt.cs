using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace PAMS.Services.Implementations.Utilites
{
    public class Decrypt
    {
        public static string DeCrypt(string cipherText, string secretKey, string iV)
        {
            //Create a new instance of the Aes class. This generates a new Key and initialization Vector (IV).
            using (Aes myAes = Aes.Create())
            {
                myAes.Key = System.Text.Encoding.UTF8.GetBytes(secretKey);
                myAes.IV = System.Text.Encoding.UTF8.GetBytes(iV);

                //Decrypt the bytes to a string
                string roundtrip = DecryptStringFromBytes_Aes(cipherText, myAes.Key, myAes.IV);

                return roundtrip;
            }
        }
        
        private static string DecryptStringFromBytes_Aes(string cipherText, byte[] Key, byte[] IV)
        {
            //check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("ciphertext");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            //Declare the string used to hold the decrypted text.
            string plainText = null;

            //Create an Aes object with the specified key and IV
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                byte[] cipherbytes = HexadecimalStringToByteArray(cipherText);
                // Create the streams used for decryption.
                using(MemoryStream msDecrypt = new MemoryStream(cipherbytes))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            //Read the decrypted bytes from the decrypting stream and place them in a string.
                            plainText = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return plainText;
        }

        private static byte[] HexadecimalStringToByteArray(string cipherText)
        {
            var outputLength = cipherText.Length / 2;
            var output = new byte[outputLength];
            using (var sr = new StringReader(cipherText))
            {
                for (var i = 0; i < outputLength; i++)
                    output[i] = Convert.ToByte(new string(new char[2] { (char)sr.Read(),
                    (char)sr.Read() }), 16);
            }
            return output;
        }
    }
}
