using System.Security.Cryptography;
using UrbanCare.Domain.Interfaces.Security;

namespace UrbanCare.Infrastructure.Security
{
    public class SHA512Hasher : IHasher
    {
        public (byte[] hash, byte[] salt) Hash(string data, byte[]? salt = null)
        {
            if (salt is null)
                salt = SaltGenerator.Generate(32);

            using (var pbkdf2 = new Rfc2898DeriveBytes(data, salt, 300000, HashAlgorithmName.SHA512))
            {
                return (pbkdf2.GetBytes(255), salt);
            }
        }

        public bool Verify(string enteredData, byte[] storedData, byte[] storedSalt)
        {
            var hashedEnteredData = Hash(enteredData, storedSalt);
            return CryptographicOperations.FixedTimeEquals(storedData, hashedEnteredData.hash);
        }
    }
}
