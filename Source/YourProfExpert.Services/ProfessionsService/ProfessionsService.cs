#nullable disable

using Microsoft.Extensions.Logging;
using YourProfExpert.Core.Services;
using YourProfExpert.Core.Types;

namespace YourProfExpert.Services;

public class ProfessionsService : IProfessionsService
{
    // long = int, userId = текущая страница
    private readonly IDictionary<long, int> _usersPages;
    private readonly int _professionsPerPage;

    private readonly ILogger<ProfessionsService> _logger;

    private Profession[] _professions;

    public ProfessionsService(ILogger<ProfessionsService> logger, int ProfessionsPerPage = 5)
    {
        _usersPages = new Dictionary<long, int>();
        _professionsPerPage = ProfessionsPerPage;

        _logger = logger;
    }
 
    public void ClosePage(long userId)
    {
        _logger.LogDebug($"{userId} закрыл страницу с профессиями");

        _usersPages.Remove(userId);
    }

    public int CountOfPages()
    {
        double countOfPages = (double) _professions.Length / _professionsPerPage;

        // Если деление целочисленно, то нужно вернуть результат деления
        // Если нет, то вернуть инкрементированное значение
        if ( countOfPages % 1 == 0.0 ) return (int) countOfPages;

        return (int) countOfPages + 1;
    }

    public IEnumerable<Profession> GetProfessionsFromPage(long userId)
    {
        _logger.LogDebug($"{userId} просматривает страницу №{_usersPages[userId]} с профессиями");

        return _professions
            .Skip( _professionsPerPage * _usersPages[userId] )
            .Take( _professionsPerPage );
    }

    public bool IsUserOpenPage(long userId)
    {
        _logger.LogDebug($"{userId} проверяет на просмотр страницы");

        return _usersPages.ContainsKey(userId);
    }

    public bool NextPage(long userId)
    {
        // -1 для того, чтобы корректно считать от нуля
        if ( _usersPages[userId] >= CountOfPages() - 1 )
        {
            _logger.LogDebug($"{userId} неуспешно перешел на следующую страницу: №{_usersPages[userId]}");

            return false;
        }

        _logger.LogDebug($"{userId} успешно перешел на следующую страницу");

        ++_usersPages[userId];

        return true;
    }

    public void OpenPage(long userId)
    {
        _logger.LogDebug($"{userId} открыл страницу с профессиями");

        _usersPages[userId] = 0;
    }

    public bool PreviousPage(long userId)
    {
        if ( _usersPages[userId] <= 0 )
        {
            _logger.LogDebug($"{userId} неуспешно перешел на предыдущую страницу: №{_usersPages[userId]}");

            return false;
        } 

        _logger.LogDebug($"{userId} успешно перешел на предыдущую страницу");

        --_usersPages[userId];

        return true;
    }

    public Profession SelectProfession(long userId, int indexOfProfession)
    {
        _logger.LogDebug($"{userId} выбрал профессию №{indexOfProfession} на странице №{_usersPages[userId]}");

        return GetProfessionsFromPage(userId).ElementAt(indexOfProfession);
    }

    public void SetProfessions(Profession[] Professions)
    {
        _professions = Professions;
    }

    public bool TrySelectProfession(long userId, int indexOfProfession, out Profession Profession)
    {
        if ( indexOfProfession < 0 || indexOfProfession < _professionsPerPage - 1 )
        {
            Profession = null;

            _logger.LogDebug($"{userId} неудачно выбрал профессию №{indexOfProfession} на странице №{_usersPages[userId]}");

            return false;
        }

        Profession = SelectProfession(userId, indexOfProfession);

        return true;
    }
}