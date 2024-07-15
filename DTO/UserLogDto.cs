namespace JWT_Bearer_Token.DTO
{
    public record UserLogDto
    {
        public UserLogDto(string mail, string password)
        {
            Mail = mail;
            Password = password;
        }

        public required string Mail { get; init; }
        public required string Password { get; init; }
    }
}
