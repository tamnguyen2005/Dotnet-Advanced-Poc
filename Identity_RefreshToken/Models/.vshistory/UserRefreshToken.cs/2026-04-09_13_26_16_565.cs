namespace Identity_RefreshToken.Models
{
    public class UserRefreshToken
    {
        public string UserName { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; }
        public bool IsRevoked { get; set; }
    }
}