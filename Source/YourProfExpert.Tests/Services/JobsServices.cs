

namespace YourProfExpert.Tests.Services;

public class JobsServiceTest
{
    [Fact]
    public void TestOpenJobs()
    {
        long userId = 1;

        Job[] jobs = new Job[] { new() { Name = "", Description = "" } };
        IJobsService jobsService = new JobsService(jobs);

        jobsService.OpenJobs(userId);

        Assert.True( jobsService.IsUserOpenJobs( userId ) );
    }

    [Fact]
    public void TestCloseJobs()
    {
        long userId = 1;

        Job[] jobs = new Job[] { new() { Name = "", Description = "" } };
        IJobsService jobsService = new JobsService(jobs);

        jobsService.OpenJobs(userId);
        jobsService.CloseJobs(userId);

        Assert.False( jobsService.IsUserOpenJobs( userId ) );
    }

    [Fact]
    public void TestGetJobsOfPageSingle()
    {
        long userId = 1;

        Job[] jobs = new Job[] { new() { Name = "", Description = "" } };
        IJobsService jobsService = new JobsService(jobs);

        jobsService.OpenJobs(userId);
        
        int count = jobsService.GetJobsFromPage(userId).Count();

        Assert.True( count == 1 );
    }

    [Fact]
    public void TestGetJobsOfPageFirst()
    {
        long userId = 1;

        Job[] jobs = new Job[] { new() { Name = "", Description = "" }, new() { Name = "", Description = "" } };
        IJobsService jobsService = new JobsService(jobs, 1);

        jobsService.OpenJobs(userId);
        
        int count = jobsService.GetJobsFromPage(userId).Count();

        Assert.True( count == 1 );
    }

    [Fact]
    public void TestGetJobsOfPage3MaxPage()
    {
        long userId = 1;

        Job[] jobs = new Job[] { new() { Name = "", Description = "" }, new() { Name = "", Description = "" }, new() { Name = "", Description = "" } };
        IJobsService jobsService = new JobsService(jobs, 1);

        jobsService.OpenJobs(userId);
        
        int countOfPages = 0;

        do
        {
            ++countOfPages;

        } while ( jobsService.NextPage(userId) );

        Assert.True( countOfPages == 3 );
    }

    [Fact]
    public void TestGetJobsOfPage2MaxPages()
    {
        long userId = 1;

        Job[] jobs = new Job[] { new() { Name = "", Description = "" }, new() { Name = "", Description = "" }, new() { Name = "", Description = "" } };
        IJobsService jobsService = new JobsService(jobs, 2);

        jobsService.OpenJobs(userId);
        
        int countOfPages = 0;

        do
        {
            ++countOfPages;

        } while ( jobsService.NextPage(userId) );

        Assert.True( countOfPages == 2 );
    }

    [Fact]
    public void TestGetJobsOfPageEvenCountOfPages()
    {
        long userId = 1;

        Job[] jobs = new Job[] { new() { Name = "", Description = "" }, new() { Name = "", Description = "" } };
        IJobsService jobsService = new JobsService(jobs, 2);

        jobsService.OpenJobs(userId);
        
        int countOfPages = 0;

        do
        {
            ++countOfPages;

        } while ( jobsService.NextPage(userId) );

        Assert.True( countOfPages == 1 );
    }
}