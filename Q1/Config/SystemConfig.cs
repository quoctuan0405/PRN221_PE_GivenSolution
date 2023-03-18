using Microsoft.Extensions.Configuration;

namespace Q1.Config
{
    public class SystemConfig
    {
        private static readonly IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        public static string DbConnect
        {
            get { return configuration.GetConnectionString("Default"); }
        }
    }
}
