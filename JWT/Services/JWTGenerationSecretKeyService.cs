using System.Security.Cryptography;


namespace JWT_Bearer_Token.JWT.Services
{
    public static class JWTGenerationSecretKeyService
    {
        private static string _key;

        public static string GetOrCreateKey(IConfiguration configuration)
        {
            if (string.IsNullOrEmpty(_key))
            {
                _key = configuration["JWT:Key"] ?? "";
                if (string.IsNullOrEmpty(_key))
                {
                    _key = GenerateRandomKey();
                }
            }
            return _key;
        }

        private static string GenerateRandomKey(int length = 32)
        {
            var randomBytes = new byte[length];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }
            return Convert.ToBase64String(randomBytes);
        }
    }
}
