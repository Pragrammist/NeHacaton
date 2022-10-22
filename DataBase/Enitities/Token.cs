using System.ComponentModel.DataAnnotations;

namespace DataBase.Entities
{
    public class Token
    {
        public string AccessTokenHash { get; set; } = null!;
        public int ExpiresIn { get; set; }
        public string TokenType { get; set; } = null!;
    }
}