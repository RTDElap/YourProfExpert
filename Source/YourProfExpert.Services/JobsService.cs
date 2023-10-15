using System;

using YourProfExpert.Core.Services;
using YourProfExpert.Infrastructure.Models;

namespace YourProfExpert.Services;

public class JobsService : IJobsService
{
    /// <summary>
    /// long = int, id пользователя = текущая страница
    /// </summary>
    private readonly IDictionary<long, int> _usersPages;
    
    private readonly Job[] _availableJobs;

    private readonly int _jobsPerPage;

    private readonly int _maxPages;

    public JobsService(Job[] availableJobs, int jobsPerPage = 5)
    {
        _availableJobs = availableJobs;

        _usersPages = new Dictionary<long, int>();

        _jobsPerPage = jobsPerPage;

        // Округление в большую сторону
        // -1 нужен для того, чтобы корректно считать с нуля
        _maxPages = (int) Math.Round( (double) availableJobs.Length / jobsPerPage ) - 1;
    }

    public void OpenJobs(long userId)
    {
        // -1, поскольку необходимо вызывать метод NextJob
        _usersPages[userId] = -1;
    }

    public void CloseJobs(long userId)
    {
        _usersPages.Remove(userId);
    }

    public IEnumerable<Job> GetJobsFromPage(long userId)
    {
        int currentPageOfUser = _usersPages[userId];

        return _availableJobs
            .Skip( currentPageOfUser * _jobsPerPage )
            .Take( _jobsPerPage );
    }

    public bool NextPage(long userId)
    {
        int currentPageOfUser = _usersPages[userId];

        if ( currentPageOfUser >= _maxPages ) return false;

        ++_usersPages[userId];

        return true;
    }

    public bool PreviousPage(long userId)
    {
        int currentPageOfUser =  _usersPages[userId];

        if ( currentPageOfUser <= 0 ) return  false;

        --_usersPages[userId];

        return true;
    }

    public bool IsUserOpenJobs(long userId)
    {
        return _usersPages.ContainsKey( userId );
    }
}