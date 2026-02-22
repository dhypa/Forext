namespace Forext.CcyProvider.Database;

public class DatabaseConfig
{
    public required string Host {  get; set; }
    public required string Port { get; set; }
    public required string Name { get; set; }
    public bool Local { get; set; }
    public static string GetConnectionString(IConfiguration config)
    {
        var dbConfig = config.GetSection("Database") as DatabaseConfig;
        if(dbConfig == null)
        {
            throw new Exception("fix this shit");
        }

        return dbConfig.Local ?
            $"Host={dbConfig.Host},{dbConfig.Port};Database={dbConfig.Name};Authentication=Active Directory Managed Identity"
            : "idk yet";
    }
}
