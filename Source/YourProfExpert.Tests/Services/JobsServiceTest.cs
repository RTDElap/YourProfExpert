

namespace YourProfExpert.Tests;

public class JobsServiceTest
{
    [Fact]
    public void GetAllPagesEven()
    {
        IJobsService jobsService = new JobsService();

        var jobsArray = Enumerable.Range(0, 10).Select( i => new Job() { Name = $"{i}", Description = $"{i}" } ).ToArray();

        jobsService.SetJobs( jobsArray );
        jobsService.OpenJobs(1);

        int count = jobsService.CountOfPages();
    
        Assert.True
        (
            count == 2
        );
    }

    [Fact]
    public void GetAllPagesNotEven()
    {
        IJobsService jobsService = new JobsService();

        var jobsArray = Enumerable.Range(0, 11).Select( i => new Job() { Name = $"{i}", Description = $"{i}" } ).ToArray();

        jobsService.SetJobs( jobsArray );
        jobsService.OpenJobs(1);

        int count = jobsService.CountOfPages();
    
        Assert.True
        (
            count == 3
        );
    }

    [Fact]
    public void GetFirstPage()
    {
        IJobsService jobsService = new JobsService();
        jobsService.SetJobs( new Job[] { new Job{ Name = "1", Description = "1" } } );
        jobsService.OpenJobs(1);

        IEnumerable<Job> jobs = jobsService.GetJobsFromPage(1);
    
        Assert.True
        (
            jobs.Count() == 1 &&
            jobs.First().Name == "1" &&
            jobs.First().Description == "1"
        );
    }

    [Fact]
    public void GetFirstPageWithCheckData()
    {
        IJobsService jobsService = new JobsService();
        
        var jobsArray = Enumerable.Range(0, 11).Select( i => new Job() { Name = $"{i}", Description = $"{i}" } ).ToArray();
        
        jobsService.SetJobs( jobsArray );
        jobsService.OpenJobs(1);

        IEnumerable<Job> jobs = jobsService.GetJobsFromPage(1);
    
        Assert.Equal<IEnumerable<Job>>
        (
            jobsArray.Take(5), jobs
        );
    }

    [Fact]
    public void GetLastPageWithCheckCountOfPages()
    {
        IJobsService jobsService = new JobsService();
        
        var jobsArray = Enumerable.Range(0, 11).Select( i => new Job() { Name = $"{i}", Description = $"{i}" } ).ToArray();
        
        jobsService.SetJobs( jobsArray );
        jobsService.OpenJobs(1);

        int countOfPages = 1;

        // Пропуск до последней страницы
        while ( jobsService.NextPage(1) ) ++countOfPages;

        Assert.Equal
        (
            countOfPages, jobsService.CountOfPages()
        );
    }

    [Fact]
    public void GetLastPageWithCheckData()
    {
        IJobsService jobsService = new JobsService();
        
        var jobsArray = Enumerable.Range(0, 11).Select( i => new Job() { Name = $"{i}", Description = $"{i}" } ).ToArray();
        
        jobsService.SetJobs( jobsArray );
        jobsService.OpenJobs(1);

        while ( jobsService.NextPage(1) );
    
        var jobs = jobsService.GetJobsFromPage(1);

        Assert.Equal<IEnumerable<Job>>
        (
            jobsArray.Skip(10).Take(5), jobs
        );
    }
}