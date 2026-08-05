using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BCrypt;
using BCrypt.Net;

namespace Servicios
{
    public class Encriptador_54CS
    {
        // se usa el encriptado reversible. Son constantes xq
        // un mismo texto produce siempre el mismo cifrado.
        private static readonly byte[] Key = new byte[]
        {
            0x2A, 0x7F, 0x14, 0xC3, 0x5E, 0x91, 0xB8, 0x0D,
            0x6A, 0xF2, 0x39, 0x8C, 0x47, 0xD1, 0x05, 0xBE,
            0x73, 0x1C, 0xA9, 0x60, 0xE4, 0x3B, 0x88, 0xD7,
            0x12, 0x5F, 0xC6, 0x9A, 0x04, 0xEB, 0x7D, 0x36
        };

        private static readonly byte[] IV = new byte[]
        {
            0x91, 0x4E, 0x2B, 0xD8, 0x67, 0x05, 0xFA, 0x3C,
            0x1D, 0xB0, 0x79, 0xE6, 0x52, 0xC4, 0x8F, 0x20
        };

        public string EncriptarContraseña(string contra)
        {
            return BCrypt.Net.BCrypt.HashPassword(contra);
        }

        public bool VerificarContraseña(string ingresada, string almacenada)
        {
            return BCrypt.Net.BCrypt.Verify(ingresada, almacenada);
        }

        public string EncriptarReversible(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return texto;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    using (var sw = new StreamWriter(cs, Encoding.UTF8))
                    {
                        sw.Write(texto);
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public string DesencriptarReversible(string textoEncriptado)
        {
            if (string.IsNullOrWhiteSpace(textoEncriptado))
                return textoEncriptado;

            try
            {
                byte[] datos = Convert.FromBase64String(textoEncriptado);

                using (Aes aes = Aes.Create())
                {
                    aes.Key = Key;
                    aes.IV = IV;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                    using (var ms = new MemoryStream(datos))
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    using (var sr = new StreamReader(cs, Encoding.UTF8))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
            catch
            {
                return textoEncriptado;
            }
        }
    }
}
