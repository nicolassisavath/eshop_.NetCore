using eshop.Models;
using eshop.Repositories.Core;
using eshop.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace eshop.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private int saltLength = 64;
        private int hashLength = 128;


        private IRepositoryWrapper _repoWrapper;

        public AuthenticationService(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }

        public Users InitializeSaltAndPasswordUser(Users user)
        {
            byte[] salt = GenerateRdmSaltByte();
            string hashPassword = GetHashPassword(user.UPassword, salt);
            user.UPassword = hashPassword;

            string saltStr = ConvertSaltByteToSaltString(salt);
            user.USalt = saltStr;

            return user;
        }

        private byte[] GenerateRdmSaltByte()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] salt = new byte[saltLength];
            rng.GetBytes(salt);

            return salt;
        }

        private string ConvertSaltByteToSaltString(byte[] salt)
        {
            return Convert.ToBase64String(salt);
        }

        private byte[] ConvertSaltStringToByte(string salt)
        {
            return Convert.FromBase64String(salt);
        }

        private string GetHashPassword(string password, byte[] salt)
        {
            int iterationCount = 10;

            Rfc2898DeriveBytes hashGenerator = new Rfc2898DeriveBytes(password, salt);
            hashGenerator.IterationCount = iterationCount;

            byte[] hash = hashGenerator.GetBytes(hashLength);
            string hashText = Convert.ToBase64String(hash);

            return hashText;
        }

        public Users AuthentUser(Users credentials)
        {
            Users dbUser = _repoWrapper.UserRep.FindOneByCondition(u => u.ULogin == credentials.ULogin);

            if (dbUser == null)
                return null;

            //Hashe le mot de passe saisi par l'utilisateur
            string saltText = dbUser.USalt;
            byte[] salt = ConvertSaltStringToByte(saltText);
            string hashedCredentials = GetHashPassword(credentials.UPassword, salt);

            if (hashedCredentials != dbUser.UPassword)
                return null;
            else
            {
                dbUser.ULastlogin = DateTime.Now;
                _repoWrapper.UserRep.Update(dbUser);
                _repoWrapper.Save();

                dbUser.UPassword = null;
                dbUser.USalt = null;
                return dbUser;
            }
        }
    }
}
