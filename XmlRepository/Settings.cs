namespace XmlRepository
{
    public class Settings
    {
        public DataProviderType DataProvider { get; set; }
        public string DataFileLocation { get; set; }
    }

    public enum DataProviderType
    {
        SqlServer,
        XmlFileStore
    }
}