using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace cherkova_va.Services
{
    public class Hash
    {
        public static string HashPassword(string password)
        {
            using (SHA256 shs256Hash = SHA256.Create())
            {
                byte[] sourceBytePassword = Encoding.UTF8.GetBytes(password);//password принимается методом в виде аргумента
                byte[] hash = shs256Hash.ComputeHash(sourceBytePassword);
                return BitConverter.ToString(hash).Replace("-", String.Empty); //Возвращаем методом строковое значение
            }
        }

    }
}
