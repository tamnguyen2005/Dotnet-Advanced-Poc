namespace Identity_RefreshToken.Interfaces
{
    public interface ITokenServices
    {
        string GenerateToken(string userName);

        string GenerateRefreshToken();
    }
}