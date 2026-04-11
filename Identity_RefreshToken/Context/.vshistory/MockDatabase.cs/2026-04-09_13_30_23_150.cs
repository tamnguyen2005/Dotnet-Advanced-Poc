namespace Identity_RefreshToken.Context
{
    public static class MockDatabase
    {
        public static readonly Dictionary<string, string> Users = new()
        {
            {"admin","123" }
        };
    }
}