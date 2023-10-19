
using YourProfExpert.Infrastructure.Models;

namespace YourProfExpert.Tests.Mocks;

public partial class ContextBuilder
{
    public ContextBuilder AddUser(int id)
    {
        _users.Add( new User() { Id = id } );

        return this;
    }

    public ContextBuilder AddTestInformation(int id, string title)
    {
        _testInformations.Add( new TestInformation() { Id = id, Title = title } );

        return this;
    }

    public ContextBuilder AddAvailableTestResult(int id, int testInformationId, int orderId , string name, string description)
    {
        _availableTestResults.Add
        (
            new AvailableTestResult()
            {
                Id = id,
                OrderId = orderId,
                Test =_testInformations.Single(t => t.Id == testInformationId),
                Name = name,
                Description = description
            }
        );

        return this;
    }

    public ContextBuilder AddPassedTest(int id, int userId, int availableTestResultId)
    {
        _passedTests.Add
        (
            new PassedTest()
            {
                Id = id,
                Result = _availableTestResults.Single( a => a.Id == availableTestResultId ),
                User = _users.Single(u => u.Id == userId)
            }
        );

        return this;
    }
}