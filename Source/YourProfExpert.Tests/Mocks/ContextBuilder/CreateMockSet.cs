
using Microsoft.EntityFrameworkCore;

namespace YourProfExpert.Tests.Mocks;

public partial class ContextBuilder
{
    private DbSet<T> CreateMockSet<T>(List<T> values) where T : class
    {
        IQueryable<T> valuesQueryable = values.AsQueryable();

        var mockSet = new Mock<DbSet<T>>();

        mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(valuesQueryable.Provider);
        mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(valuesQueryable.Expression);
        mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(valuesQueryable.ElementType);
        mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => valuesQueryable.GetEnumerator());

        return mockSet.Object;
    }
}