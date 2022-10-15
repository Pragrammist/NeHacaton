namespace Web.Dtos
{
    public class OutputTokenDto
    {
        public string AccessTokenHash { get; set; } = null!;
        public int ExpiresIn { get; set; }
        public string TokenType { get; set; } = null!;
    }
}

