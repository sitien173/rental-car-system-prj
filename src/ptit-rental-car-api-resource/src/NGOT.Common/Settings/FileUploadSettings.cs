namespace NGOT.Common.Settings;

public class FileUploadSettings : ISettings
{
    public ThumbnailConfig ThumbnailConfigs { get; set; }
    public string SectionName => nameof(FileUploadSettings);

    public class ThumbnailConfig
    {
        public ImageConfig ImageConfigs { get; set; }

        public class ImageConfig
        {
            public int Width { get; set; }
            public int Height { get; set; }
            public int Quality { get; set; }
        }
    }
}