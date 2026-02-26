using Microsoft.EntityFrameworkCore;
using System;
using UserDirectory.Data;

namespace UserDirectory.Tests
{
    public abstract class TestBase : IDisposable
    {
        protected readonly AppDbContext Context;

        protected TestBase()
        {
            // Each test gets a unique in-memory database
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            Context = new AppDbContext(options);
            Context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}