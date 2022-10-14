using System.ComponentModel.DataAnnotations;

namespace DataBase.Entities
{
    public class Token
    {
        [Key]
        public string AccessTokenHash { get; set; } = null!;

        [Required]
        public int ExpiresIn { get; set; }
        
        public string TokenType { get; set; } = null!;
    }
}