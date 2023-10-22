#nullable disable

using YourProfExpert.TelegramBot.Bot.Builders;
using Microsoft.Extensions.DependencyInjection;
using YourProfExpert.TelegramBot.ChainHandlers.Handlers.Interfaces;
using YourProfExpert.TelegramBot.Extensions;
using YourProfExpert.Core.Services;
using YourProfExpert.TestOfKlimov;
using YourProfExpert.Infrastructure.Contexts.Creators.Interfaces;
using Microsoft.Extensions.Logging;
using YourProfExpert.TelegramBot.Commands;

namespace YourProfExpert.TelegramBot;

internal static partial class Program
{
    static async Task Main()
    {
        var botBuilder = new BotBuilder();

#region Регистрация сервисов

        // Чтение и инициализация конфига
        botBuilder
            .ReadConfigFrom( Path.Combine( Environment.CurrentDirectory, "appsettings.json" ) )
            .AddBotConfig();

        // Настройка логирования
        botBuilder.Services
            .AddLogging
            (
                l => l
                    .AddConsole()
                    .SetMinimumLevel(LogLevel.Trace)
            );

        // Регистрация сервисов
        botBuilder
            .AddSqliteCreator("Sqlite")
            .AddTestService()
            .AddExecutorTestService()
            .AddProfessionsService()
            .AddUserService()
            .AddCommandHandler();
#endregion

#region Создание цепочки обработчиков
        botBuilder
            .UseCommandHandler();
#endregion

#region Настройка зарегистрированных сервисов
        
        // Добавление команд в ICommandHandler
        botBuilder
            .AddCustomCommand<CommandTest>("/test", $"📄 {KlimovTestData.KLIMOV_TITLE}")
            .WithCreateUser()
            .Build();

        botBuilder
            .AddCommand<CommandStart>("/start", "🏠 Главная", "🏠 В главное меню")
            .AddCommand<CommandAbout>("👤 О боте")
            .AddCommand<CommandTests>("/tests", "📄 Тесты")
            .AddCommand<CommandRedirectDialog>("/dialog")
            .AddCommand<CommandSelectAnswer>("/selectAnswer");

        // Добавление записей в базу данных
        await botBuilder.ServiceProvider.GetService<IContextCreator>()
            .CreateContext()
            .AddKlimovTestAsync();

        // Добавление данных с тестами в ITestService
        botBuilder.ServiceProvider.GetService<ITestService>()
            .AddKlimovTest();

        // Установка значений с профессиями
        botBuilder.ServiceProvider.GetService<IProfessionsService>()
            .SetProfessions( botBuilder.BotConfig.Professions );
#endregion

        var bot = botBuilder.Build();

        bot.StartAsLongPolling();

        Console.ReadLine();
    }
}