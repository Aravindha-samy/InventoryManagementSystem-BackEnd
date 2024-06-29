using Serilog;
namespace InventoryManagementSystem.CommonUtility

{
    public class ProductLogger
    {
        public void BuildConfigure()
        {
            var configuration = new ConfigurationBuilder()
                           .AddJsonFile("appsettings.json").Build();
            Log.Logger = new LoggerConfiguration().
                    ReadFrom.Configuration(configuration)
                    .CreateLogger();
        }
    }
}



