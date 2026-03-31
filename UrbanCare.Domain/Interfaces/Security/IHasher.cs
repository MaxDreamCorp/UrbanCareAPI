namespace UrbanCare.Domain.Interfaces.Security
{
    public interface IHasher
    {
        (byte[] hash, byte[] salt) Hash(string data, byte[]? salt = null);
        bool Verify(string enteredData, byte[] storedData, byte[] storedSalt);
    }
}
