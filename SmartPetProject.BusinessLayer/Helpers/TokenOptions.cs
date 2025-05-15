namespace SmartPetProject.BusinessLayer.Helpers
{
    public class TokenOptions
    {
        public string SecurityKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int AccessTokenExpiration { get; set; }
        public int RefreshTokenTTL { get; set; }
    }
}
