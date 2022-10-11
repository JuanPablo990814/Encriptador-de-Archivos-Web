using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Encriptardor_Archivos_Online.Encriptador
{
    public class DEncriptador
    {

        #region Encriptado de texto
        public string EncriptarTexto(string texto, string personalkey)
        {
            string encriptado = null;
            try
            {
                byte[] bytesEncriptados = null;
                byte[] bytesAencriptar = Encoding.UTF8.GetBytes(texto);
                byte[] clave = Encoding.UTF8.GetBytes(personalkey);
                byte[] salt = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
                
                using (var memoStream = new MemoryStream())
                {
                    using (var aes = new RijndaelManaged())
                    {
                        aes.KeySize = 256;
                        aes.BlockSize = 128;
                        var key = new Rfc2898DeriveBytes(clave, salt, 1000);
                        aes.Key = key.GetBytes(aes.KeySize / 8);
                        aes.IV = key.GetBytes(aes.BlockSize / 8);
                        aes.Mode = CipherMode.CBC;
                        using (CryptoStream cryptoStream = new CryptoStream(memoStream, aes.CreateEncryptor(),
                            CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(bytesAencriptar, 0, bytesAencriptar.Length);
                            cryptoStream.Close();
                        }
                        bytesEncriptados = memoStream.ToArray();
                        encriptado = Convert.ToBase64String(bytesEncriptados, 0, bytesEncriptados.Length);
                    }
                }
            }
            catch
            {
                encriptado = "Fail Encrypted";
            }
            
            return encriptado;
        }

        internal string DescriptarTexto(string textocontenido, object textollave)
        {
            throw new NotImplementedException();
        }

        public string DesencriptarTexto(string texto, string personalkey)
        {
            string decriptado = null;

            try
            {
                byte[] bytesEncriptados = null;
                byte[] bytesADesencriptar = Convert.FromBase64String(texto);
                byte[] clave = Encoding.UTF8.GetBytes(personalkey);
                byte[] salt = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
                
                using (var memoStream = new MemoryStream())
                {
                    using (var aes = new RijndaelManaged())
                    {
                        aes.KeySize = 256;
                        aes.BlockSize = 128;
                        var key = new Rfc2898DeriveBytes(clave, salt, 1000);
                        aes.Key = key.GetBytes(aes.KeySize / 8);
                        aes.IV = key.GetBytes(aes.BlockSize / 8);
                        aes.Mode = CipherMode.CBC;
                        using (CryptoStream cryptoStream = new CryptoStream(memoStream, aes.CreateDecryptor(),
                            CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(bytesADesencriptar, 0, bytesADesencriptar.Length);
                            cryptoStream.Close();
                        }
                        bytesEncriptados = memoStream.ToArray();
                        decriptado = Encoding.UTF8.GetString(bytesEncriptados, 0, bytesEncriptados.Length);
                    }
                }
            }
            catch
            {
                decriptado = "Fail Decrypted: It's not key";
            }
            
            return decriptado;
        }

        #endregion

        #region Encriptado de Archivos
        public byte[] EncriptarArchivo(string rutaArchivo, string personalkey)
        {
            byte[] bytesEncriptados = null;
            byte[] bytesAencriptar = File.ReadAllBytes(rutaArchivo);
            byte[] clave = Encoding.UTF8.GetBytes(personalkey);
            byte[] salt = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            using (var memoStream = new MemoryStream())
            {
                using (var aes = new RijndaelManaged())
                {
                    aes.KeySize = 256;
                    aes.BlockSize = 128;
                    var key = new Rfc2898DeriveBytes(clave, salt, 1000);
                    aes.Key = key.GetBytes(aes.KeySize / 8);
                    aes.IV = key.GetBytes(aes.BlockSize / 8);
                    aes.Mode = CipherMode.CBC;
                    using (CryptoStream cryptoStream = new CryptoStream(memoStream, aes.CreateEncryptor(),
                        CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(bytesAencriptar, 0, bytesAencriptar.Length);
                        cryptoStream.Close();
                    }
                    bytesEncriptados = memoStream.ToArray();
                    //sobrescribiendo el archivo existente
                    File.WriteAllBytes(rutaArchivo, bytesEncriptados);
                }
            }
            return bytesEncriptados;
        }

        public byte[] DesencriptarArchivo(string rutaArchivo, string personalkey)
        {
            byte[] bytesEncriptados = null;
            byte[] bytesADesencriptar = File.ReadAllBytes(rutaArchivo);
            byte[] clave = Encoding.UTF8.GetBytes(personalkey);
            byte[] salt = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            using (var memoStream = new MemoryStream())
            {
                using (var aes = new RijndaelManaged())
                {
                    aes.KeySize = 256;
                    aes.BlockSize = 128;
                    var key = new Rfc2898DeriveBytes(clave, salt, 1000);
                    aes.Key = key.GetBytes(aes.KeySize / 8);
                    aes.IV = key.GetBytes(aes.BlockSize / 8);
                    aes.Mode = CipherMode.CBC;
                    using (CryptoStream cryptoStream = new CryptoStream(memoStream, aes.CreateDecryptor(),
                        CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(bytesADesencriptar, 0, bytesADesencriptar.Length);
                        cryptoStream.Close();
                    }
                    bytesEncriptados = memoStream.ToArray();
                    //sobrescribiendo el archivo existente
                    File.WriteAllBytes(rutaArchivo, bytesEncriptados);
                }
            }
            return bytesEncriptados;
        }
        #endregion

    }
}
