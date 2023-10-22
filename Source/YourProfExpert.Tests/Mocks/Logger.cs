

using Castle.Core.Logging;
using Microsoft.Extensions.Logging;

namespace YourProfExpert.Tests.Mocks;

public static class Logger
{
    public static ILogger<T> CreateMock<T>()
    {
        return new Mock<ILogger<T>>()
            .Object;
    }
}