
namespace AIYTVideoSummarizer.Application.Interfaces.Security
{
    public interface IPasswordHasher
    {
        public byte[] GenerateSalt();
        public string? GenerateHashedPassword(string password, byte[] salt);
        public bool VerifyPassword(string userPassowrd, string hashedPassword, byte[] salt);

    }
}
