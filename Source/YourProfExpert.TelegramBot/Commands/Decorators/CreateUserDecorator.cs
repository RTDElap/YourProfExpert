

using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;
using YourProfExpert.Core.Services;
using YourProfExpert.Infrastructure.Contexts.Creators.Interfaces;
using YourProfExpert.TelegramBot.Commands;

namespace YourProfExpert.TelegramBot.Commands;

/// <summary>
/// Автоматически вносит пользователя в базу данных, если это необходимо
/// </summary>
public class CreateUserDecorator : Decorator
{
    private readonly ILogger<CreateUserDecorator> _logger;
    private readonly IUserService _userService;

    public CreateUserDecorator(IUserService userService, ILogger<CreateUserDecorator> logger)
    {
        _logger = logger;
        _userService = userService;
    }

    public override async Task RunFromCallbackAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string[] args)
    {
        long userId = update.CallbackQuery.From.Id;

        _logger.LogDebug($"Проверка существование пользователя {userId}");

        if ( ! await _userService.IsUserExistsAsync(userId) )
        {
            _logger.LogInformation($"Создание пользователя {userId}");

            await _userService.CreateUserAsync(userId);
        }

        await _innerCommand.RunFromCallbackAsync(botClient, update, cancellationToken, args);
    }

    public override async Task RunFromMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string[] args)
    {
        long userId = update.Message.From.Id;

        _logger.LogDebug($"Проверка существование пользователя {userId}");

        if ( ! await _userService.IsUserExistsAsync(userId) )
        {
            _logger.LogInformation($"Создание пользователя {userId}");

            await _userService.CreateUserAsync(userId);
        }

        await _innerCommand.RunFromMessageAsync(botClient, update, cancellationToken, args);
    }
}
