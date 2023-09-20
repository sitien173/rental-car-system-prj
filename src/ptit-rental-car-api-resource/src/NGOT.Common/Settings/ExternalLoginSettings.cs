namespace NGOT.Common.Settings;

public class ExternalLoginSettings : ISettings
{
    public ExternalLoginSetting Google { get; set; } = new();
    public ExternalLoginSetting Facebook { get; set; } = new();
    public string SectionName => nameof(ExternalLoginSettings);

    public class ExternalLoginSetting
    {
        public string ClientId { get; set; } = null!;
        public string ClientSecret { get; set; } = null!;
    }
}