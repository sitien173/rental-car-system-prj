namespace NGOT.Common.Settings;

public class JwtSettings : ISettings
{
    public string Scheme { get; set; } = string.Empty;
    public string Secret { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public int AccessTokenExpiration { get; set; }
    public int RefreshTokenExpiration { get; set; }
    public string Authority { get; set; } = string.Empty;
    public string SectionName => nameof(JwtSettings);
}