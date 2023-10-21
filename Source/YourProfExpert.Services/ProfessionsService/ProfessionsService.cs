#nullable disable

using YourProfExpert.Core.Services;
using YourProfExpert.Core.Types;

namespace YourProfExpert.Services;

public class ProfessionsService : IProfessionsService
{
    // long = int, userId = текущая страница
    private readonly IDictionary<long, int> _usersPages;
    private readonly int _professionsPerPage;

    private Profession[] _professions;

    public ProfessionsService(int ProfessionsPerPage = 5)
    {
        _usersPages = new Dictionary<long, int>();
        _professionsPerPage = ProfessionsPerPage;
    }
 
    public void ClosePage(long userId)
    {
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
        return _professions
            .Skip( _professionsPerPage * _usersPages[userId] )
            .Take( _professionsPerPage );
    }

    public bool IsUserOpenPage(long userId)
    {
        return _usersPages.ContainsKey(userId);
    }

    public bool NextPage(long userId)
    {
        // -1 для того, чтобы корректно считать от нуля
        if ( _usersPages[userId] >= CountOfPages()  - 1 ) return false;

        ++_usersPages[userId];

        return true;
    }

    public void OpenPage(long userId)
    {
        _usersPages[userId] = 0;
    }

    public bool PreviousPage(long userId)
    {
        if ( _usersPages[userId] <= 0 ) return false;

        --_usersPages[userId];

        return true;
    }

    public Profession SelectProfession(long userId, int indexOfProfession)
    {
        return GetProfessionsFromPage(userId).ElementAt(indexOfProfession);
    }

    public void SetProfessions(Profession[] Professions)
    {
        _professions = Professions;
    }

    public bool TrySelectProfession(long userId, int indexOfProfession, out Profession? Profession)
    {
        if ( indexOfProfession < 0 || indexOfProfession < _professionsPerPage - 1 )
        {
            Profession = null;

            return false;
        }

        Profession = SelectProfession(userId, indexOfProfession);

        return true;
    }
}