using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace WorldOfExtreme.Infrastructure.Interface
{
    public interface ISeeder
    {
        Task SeedAsync(DbContext dbContext, IServiceProvider serviceProvider);
    }
}
