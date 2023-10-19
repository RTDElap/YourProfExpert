

using YourProfExpert.Core.Types;
using YourProfExpert.Infrastructure.Models;

namespace YourProfExpert.Core.Services;

/// <summary>
/// Интерфейс представляет методы для получения страниц существующих работ.
/// Имплементация интерфейса должна хранить состояние для каждого пользователя,
/// поэтому необходимо в каждый метод добавлять параметр userId.
/// 
/// Страница - это доступная часть для просмотра из массива
/// </summary>
public interface IJobsService
{
    /// <summary>
    /// Устанавливает список специальностей
    /// </summary>
    /// <param name="jobs">Специальности</param>
    public void SetJobs(Job[] jobs);

    /// <summary>
    /// Открывает страницу с специальностями для пользователя
    /// </summary>
    /// <param name="userId">Пользователь</param>
    public void OpenJobs(long userId);

    /// <summary>
    /// Закрывает страницу с специальностями для пользователя
    /// </summary>
    /// <param name="userId">Пользователь</param>
    public void CloseJobs(long userId);

    /// <summary>
    /// Открывает следующую страницу с специальностями для пользователя
    /// </summary>
    /// <param name="userId">Пользователь</param>
    /// <returns>Флаг возможности перехода вперед</returns>
    public bool NextPage(long userId);

    /// <summary>
    /// Открывает предыдущую страницу с специальностями для пользователя
    /// </summary>
    /// <param name="userId">Пользователь</param>
    /// <returns>Флаг возможности перехода назад</returns>
    public bool PreviousPage(long userId);

    /// <summary>
    /// Возвращает общее количество страниц
    /// </summary>
    /// <returns>Количество</returns>
    public int CountOfPages();

    /// <summary>
    /// Определение существования страницы для пользователя
    /// </summary>
    /// <param name="userId">Пользователь</param>
    /// <returns>Флаг, указывающий открыта ли страница для пользователя</returns>
    public bool IsUserOpenJobs(long userId);

    /// <summary>
    /// Возвращает список специальностей на основе открытой страницы
    /// </summary>
    /// <param name="userId">Пользователь</param>
    /// <returns>Специальности со страницы</returns>
    public IEnumerable<Job> GetJobsFromPage(long userId);
}