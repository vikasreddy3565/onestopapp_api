using System;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using OneStopApp_Api.Interface;
using OneStopApp_Api.ViewModel;

namespace OneStopApp_Api.Service
{
    public class SaltPasswordService : ISaltPasswordService
    {
        private readonly IOptions<SaltPasswordViewModel> _saltPasswordSettings;
        
        public SaltPasswordService(IOptions<SaltPasswordViewModel> saltPasswordSettings)
        {
            _saltPasswordSettings = saltPasswordSettings;
        }
       
        public string SaltPassword(string plainTextPassword)
        {
            //Generate a random salt
            var csprng = new RNGCryptoServiceProvider();
            var salt = new byte[_saltPasswordSettings.Value.SaltBytes];
            csprng.GetBytes(salt);

            //Hash the password and encode the parameters
            var hash = Hash(plainTextPassword, salt, _saltPasswordSettings.Value.Iterations, _saltPasswordSettings.Value.HashBytes);

            //Return the SaltedHash
           return string.Format("{0}:{1}:{2}", _saltPasswordSettings.Value.Iterations, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
        }
        private byte[] Hash(string password, byte[] salt, int iterations, int outputBytes)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt);
            pbkdf2.IterationCount = iterations;
            return pbkdf2.GetBytes(outputBytes);
        }

        private bool SlowEquals(byte[] a, byte[] b)
        {
            var diff = (uint)a.Length ^ (uint)b.Length;
            for (var i = 0; i < a.Length && i < b.Length; i++)
                diff |= (uint)(a[i] ^ b[i]);
            return diff == 0;
        }

        public bool ComparePasswords(string encryptedPassword ,string plainTextPassword)
        {
            // Extract the parameters from the hash
            char[] delimiter = { ':' };
            var split = encryptedPassword.Split(delimiter);
            var iterations = Int32.Parse(split[_saltPasswordSettings.Value.IterationIndex]);
            var salt = Convert.FromBase64String(split[_saltPasswordSettings.Value.SaltIndex]);
            var hash = Convert.FromBase64String(split[_saltPasswordSettings.Value.Index]);

            byte[] testHash = Hash(plainTextPassword, salt, iterations, hash.Length);
            return SlowEquals(hash, testHash);
        }

    }    
}