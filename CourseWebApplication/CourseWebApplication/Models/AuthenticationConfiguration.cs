namespace WebApp.Models
{
    public class AuthenticationConfiguration
    {
        public string Secret { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public int AccessTokenExpiration { get; set; }

    }
}
