using System;
using System.Security.Cryptography;
using System.Text;
namespace Biblioteca.Models
{
    public static class Criptografia
    {
        public static string TextoCriptografado(string texto)
        {
            MD5 hash = MD5.Create();
            byte[] by = Encoding.Default.GetBytes(texto);
            byte[] bytesCriptografados = hash.ComputeHash(by);

            StringBuilder sb = new StringBuilder();
            foreach(byte b in bytesCriptografados)
            {
                string debugB = b.ToString("x2");
                sb.Append(debugB);
            }
            return sb.ToString();
        }
    }
}