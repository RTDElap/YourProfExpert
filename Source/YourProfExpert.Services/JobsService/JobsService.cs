#nullable disable

using YourProfExpert.Core.Services;
using YourProfExpert.Core.Types;

namespace YourProfExpert.Services;

public class JobsService : IJobsService
{
    // long = int, userId = текущая страница
    private readonly IDictionary<long, int> _usersPages;
    private readonly int _jobsPerPage;

    private Job[] _jobs;

    public JobsService(int jobsPerPage = 5)
    {
        _usersPages = new Dictionary<long, int>();
        _jobsPerPage = jobsPerPage;
    }
 
    public void CloseJobs(long userId)
    {
        _usersPages.Remove(userId);
    }

    public int CountOfPages()
    {
        double countOfPages = (double) _jobs.Length / _jobsPerPage;

        // Если деление целочисленно, то нужно вернуть результат деления
        // Если нет, то вернуть инкрементированное значение
        if ( countOfPages % 1 == 0.0 ) return (int) countOfPages;

        return (int) countOfPages + 1;
    }

    public IEnumerable<Job> GetJobsFromPage(long userId)
    {
        return _jobs
            .Skip( _jobsPerPage * _usersPages[userId] )
            .Take( _jobsPerPage );
    }

    public bool IsUserOpenJobs(long userId)
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

    public void OpenJobs(long userId)
    {
        _usersPages[userId] = 0;
    }

    public bool PreviousPage(long userId)
    {
        if ( _usersPages[userId] <= 0 ) return false;

        --_usersPages[userId];

        return true;
    }

    public void SetJobs(Job[] jobs)
    {
        _jobs = jobs;
    }
}