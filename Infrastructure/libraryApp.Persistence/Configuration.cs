using Microsoft.Extensions.Configuration;

namespace libraryApp.Persistence
{
    public class Configuration
    {
        public static string ConnectionString
        {
            get
            {

                ConfigurationManager configurationManager = new();

                configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/libraryApp.API"));
                configurationManager.AddJsonFile("appsettings.json");
                return configurationManager.GetConnectionString("MsSQL");
            }
        }
    }
}
