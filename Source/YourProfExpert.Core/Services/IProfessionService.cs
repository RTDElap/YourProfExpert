

using YourProfExpert.Core.Types;
using YourProfExpert.Infrastructure.Models;

namespace YourProfExpert.Core.Services;

/// <summary>
/// Интерфейс представляет методы для получения страниц существующих профессий.
/// Имплементация интерфейса должна хранить состояние для каждого пользователя,
/// поэтому необходимо в каждый метод добавлять параметр userId.
/// 
/// Страница - это доступная часть для просмотра из массива
/// </summary>
public interface IProfessionsService
{
    /// <summary>
    /// Устанавливает список профессий
    /// </summary>
    /// <param name="Professions">Профессия</param>
    public void SetProfessions(Profession[] Professions);

    /// <summary>
    /// Открывает страницу с профессиями для пользователя
    /// </summary>
    /// <param name="userId">Пользователь</param>
    public void OpenPage(long userId);

    /// <summary>
    /// Закрывает страницу с профессией для пользователя
    /// </summary>
    /// <param name="userId">Пользователь</param>
    public void ClosePage(long userId);

    /// <summary>
    /// Открывает следующую страницу с профессиями для пользователя
    /// </summary>
    /// <param name="userId">Пользователь</param>
    /// <returns>Флаг возможности перехода вперед</returns>
    public bool NextPage(long userId);

    /// <summary>
    /// Открывает предыдущую страницу с профессиями для пользователя
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
    public bool IsUserOpenPage(long userId);

    /// <summary>
    /// Возвращает список профессий на основе открытой страницы
    /// </summary>
    /// <param name="userId">Пользователь</param>
    /// <returns>Профессии со страницы</returns>
    public IEnumerable<Profession> GetProfessionsFromPage(long userId);

    /// <summary>
    /// Возвращает профессии по индексу для пользователя
    /// </summary>
    /// <param name="userId">Пользователь</param>
    /// <param name="indexOfProfession">Индекс профессии на текущей странице</param>
    /// <returns>Специальность</returns>
    public Profession SelectProfession(long userId, int indexOfProfession);

    /// <summary>
    /// Возвращает профессии по индексу для пользователя
    /// </summary>
    /// <param name="userId">Пользователь</param>
    /// <param name="indexOfProfession">Индекс профессии на текущей странице</param>
    /// <returns>Специальность через out; результат выполнения</returns>
    public bool TrySelectProfession(long userId, int indexOfProfession, out Profession? Profession);
}