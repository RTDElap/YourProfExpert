using Microsoft.Extensions.Configuration;


namespace YourProfExpert.TelegramBot;

internal static partial class Program
{
    internal static IConfigurationRoot Configuration()
    {
        IConfigurationBuilder builder = new ConfigurationBuilder();

        builder
            .AddJsonFile("appsettings.json");

        return builder.Build();
    }
}