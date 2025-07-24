namespace Services.settingOption
{
    public class JwtSettingOption
    {
        public static string SectionName="JWT";
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string SecretKey { get; set; } = string.Empty;
        public int ExpirationInMinutes { get; set; } 
        public int RefreshTokenExpirationInDays { get; set; } 
    }
}
