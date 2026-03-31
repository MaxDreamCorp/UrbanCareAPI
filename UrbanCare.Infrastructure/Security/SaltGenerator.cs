using System.Security.Cryptography;

namespace UrbanCare.Infrastructure.Security
{
    public static class SaltGenerator
    {
        public static byte[] Generate(int length)
            => RandomNumberGenerator.GetBytes(length);
    }
}
