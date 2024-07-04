using Microsoft.EntityFrameworkCore;
using Xpto.Infra.Database.Relational;

namespace Xpto.Tests.RepositoryTests;

public sealed class XptoDbContextInMemory : IDisposable
{
    public XptoDbContext CreateConxtext()
    {
        var option = new DbContextOptionsBuilder<XptoDbContext>().UseInMemoryDatabase("memory").Options;

        var context = new XptoDbContext(option);

        if (context != null)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        return context;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}