

using YourProfExpert.Core.Tests;
using YourProfExpert.Infrastructure.Contexts.Creators.Interfaces;

namespace YourProfExpert.Core.Services;

/// <summary>
/// Интерфейс, представляющий методы для прохождения теста.
/// Имплементации этого интерфейса должны хранить состояние пользователей.
/// </summary>
public interface IExecutorTestService
{
    public void StartTest(long userId, Test test);

    public void CloseTest(long userId);

    public int GetCurrentIndex(long userId);

    public string GetQuestion(long userId);

    public IEnumerable<string> GetAnswers(long userId);

    public bool IsEnd(long userId);

    // Sync методы

    public void SaveResult(long userId);

    // Async методы

    public void SaveResultAsync(long userId);
}