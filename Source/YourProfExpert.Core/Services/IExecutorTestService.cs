

using YourProfExpert.Core.Tests;
using YourProfExpert.Infrastructure.Contexts.Creators.Interfaces;

namespace YourProfExpert.Core.Services;

/// <summary>
/// Интерфейс, представляющий методы для прохождения теста.
/// Имплементации этого интерфейса должны хранить состояние пользователей.
/// </summary>
public interface IExecutorTestService
{
    /// <summary>
    /// Создает и хранит для пользователя TestExecutor,
    /// применяемый для прохождения теста
    /// </summary>
    /// <param name="userId">Пользователь</param>
    /// <param name="test">Тест для прохождения</param>
    public void StartTest(long userId, FunctionalTest test);

    /// <summary>
    /// Удаляет для пользователя TestExecutor
    /// </summary>
    /// <param name="userId">Пользователь</param>
    public void CloseTest(long userId);

    /// <summary>
    /// Возвращает порядковый номер вопроса
    /// </summary>
    /// <param name="userId">Пользователь</param>
    /// <returns>Порядковый номер вопроса</returns>
    public int GetCurrentIndexOfQuestion(long userId);

    /// <summary>
    /// Возвращает текст вопроса
    /// </summary>
    /// <param name="userId">Пользователь</param>
    /// <returns>Текст вопроса</returns>
    public string GetQuestion(long userId);

    /// <summary>
    /// Возвращает текущие ответы на вопрос
    /// </summary>
    /// <param name="userId">Пользователь</param>
    /// <returns></returns>
    public IEnumerable<string> GetAnswers(long userId);

    /// <summary>
    /// Позволяет выбрать ответ на вопрос
    /// </summary>
    /// <param name="userId">Пользователь</param>
    /// <param name="index">Индекс ответа</param>
    public void SelectAnswer(long userId, int index);

    /// <summary>
    /// Определяет закончились ли вопросы у теста
    /// </summary>
    /// <param name="userId">Пользователь</param>
    /// <returns>Флаг, указывающий на отсутствие доступных вопросов в тесте</returns>
    public bool IsEnd(long userId);

    // Sync методы

    /// <summary>
    /// Сохраняет результат в базу данных
    /// </summary>
    /// <param name="userId">Пользователь</param>
    public void SaveResult(long userId);

    // Async методы

    /// <summary>
    /// Сохраняет результат в базу данных
    /// </summary>
    /// <param name="userId">Пользователь</param>
    public void SaveResultAsync(long userId);
}