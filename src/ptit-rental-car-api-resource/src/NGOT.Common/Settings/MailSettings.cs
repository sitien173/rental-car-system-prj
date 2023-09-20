namespace NGOT.Common.Settings;

public class MailSettings : ISettings
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Host { get; set; } = null!;
    public int Port { get; set; }
    public bool UseSsl { get; set; }
    public string SectionName => nameof(MailSettings);
}