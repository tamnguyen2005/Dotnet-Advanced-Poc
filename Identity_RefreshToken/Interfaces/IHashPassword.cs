namespace Identity_RefreshToken.Interfaces
{
    public interface IHashPassword
    {
        string HashPassword(string password);

        bool VerifyPassword(string hashedPassword, string password);
    }
}