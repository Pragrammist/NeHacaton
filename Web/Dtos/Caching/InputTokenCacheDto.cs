namespace Web.Dtos.Caching
{
    public class InputTokenCacheDto
    {
        public string AccessToken { get; set; } = null!;
        public int ExpiresIn { get; set; }
        public string UserLogin { get; set; } = null!;

    }
}
