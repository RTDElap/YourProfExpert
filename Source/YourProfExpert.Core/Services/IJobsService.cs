

using YourProfExpert.Core.Types;
using YourProfExpert.Infrastructure.Models;

namespace YourProfExpert.Core.Services;

/// <summary>
/// Интерфейс представляет методы для получения страниц существующих работ.
/// Имплементация интерфейса должна хранить состояние для каждого пользователя,
/// поэтому необходимо в каждый метод добавлять параметр userId
/// </summary>
public interface IJobsService
{
    public void SetJobs(Job[] jobs);

    public void OpenJobs(long userId);

    public void CloseJobs(long userId);

    public bool NextPage(long userId);

    public bool PreviousPage(long userId);

    public bool IsUserOpenJobs(long userId);

    public IEnumerable<Job> GetJobsFromPage(long userId);
}