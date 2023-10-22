

namespace YourProfExpert.Tests;

public class ProfessionsService_Tests
{
    [Fact]
    public void CountOfPages_if_count_is_Even()
    {
        IProfessionsService professionsService = new ProfessionsService(Logger.CreateMock<ProfessionsService>());

        var professionArray = Enumerable.Range(0, 10).Select( i => new Profession() { Name = $"{i}", Description = $"{i}" } ).ToArray();

        professionsService.SetProfessions( professionArray );
        professionsService.OpenPage(1);

        int count = professionsService.CountOfPages();
    
        Assert.True
        (
            count == 2
        );
    }

    [Fact]
    public void CountOfPages_if_count_is_not_Even()
    {
        IProfessionsService professionsService = new ProfessionsService(Logger.CreateMock<ProfessionsService>());

        var professionArray = Enumerable.Range(0, 11).Select( i => new Profession() { Name = $"{i}", Description = $"{i}" } ).ToArray();

       professionsService.SetProfessions( professionArray );
       professionsService.OpenPage(1);

        int count = professionsService.CountOfPages();
    
        Assert.True
        (
            count == 3
        );
    }

    [Fact]
    public void GetProfessionsFromPage_first_page()
    {
        IProfessionsService professionsService = new ProfessionsService(Logger.CreateMock<ProfessionsService>());
        professionsService.SetProfessions( new Profession[] { new Profession{ Name = "1", Description = "1" } } );
        professionsService.OpenPage(1);

        IEnumerable<Profession> profession = professionsService.GetProfessionsFromPage(1);
    
        Assert.True
        (
            profession.Count() == 1 &&
            profession.First().Name == "1" &&
            profession.First().Description == "1"
        );
    }

    [Fact]
    public void GetProfessionsFromPage_first_page_with_check_data()
    {
        IProfessionsService professionsService = new ProfessionsService(Logger.CreateMock<ProfessionsService>());
        
        var professionArray = Enumerable.Range(0, 11).Select( i => new Profession() { Name = $"{i}", Description = $"{i}" } ).ToArray();
        
        professionsService.SetProfessions( professionArray );
        professionsService.OpenPage(1);

        IEnumerable<Profession> profession = professionsService.GetProfessionsFromPage(1);
    
        Assert.Equal<IEnumerable<Profession>>
        (
            professionArray.Take(5), profession
        );
    }

    [Fact]
    public void GetProfessionsFromPage_last_page_with_check_count_of_pages()
    {
        IProfessionsService professionsService = new ProfessionsService(Logger.CreateMock<ProfessionsService>());
        
        var professionArray = Enumerable.Range(0, 11).Select( i => new Profession() { Name = $"{i}", Description = $"{i}" } ).ToArray();
        
        professionsService.SetProfessions( professionArray );
        professionsService.OpenPage(1);

        int countOfPages = 1;

        // Пропуск до последней страницы
        while (professionsService.NextPage(1) ) ++countOfPages;

        Assert.Equal
        (
            countOfPages, professionsService.CountOfPages()
        );
    }

    [Fact]
    public void GetProfessionsFromPage_last_page_with_check_data()
    {
        IProfessionsService professionsService = new ProfessionsService(Logger.CreateMock<ProfessionsService>());
        
        var professionArray = Enumerable.Range(0, 11).Select( i => new Profession() { Name = $"{i}", Description = $"{i}" } ).ToArray();
        
        professionsService.SetProfessions( professionArray );
        professionsService.OpenPage(1);

        while ( professionsService.NextPage(1) );
    
        var professions = professionsService.GetProfessionsFromPage(1);

        Assert.Equal<IEnumerable<Profession>>
        (
            professionArray.Skip(10).Take(5), professions
        );
    }
}