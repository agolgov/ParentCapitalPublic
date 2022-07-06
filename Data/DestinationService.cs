using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ParentCapitalBlazor.Data
{
    public interface IDestinationService
    {
        Task<List<Destination>> GetAllAsync();
        Task<Destination> GetOneAsync(int id);
        Task<bool> CreateAsync(Destination newDestination);
        Task<bool> UpdateAsync(Destination editedDestination);
        Task<bool> DeleteAsync(int id);
    }

    public class DestinationService : IDestinationService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

        public DestinationService(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<List<Destination>> GetAllAsync()
        {
            using var context = _dbFactory.CreateDbContext();
            var destinations = await context.Destinations.ToListAsync();
            return destinations;
        }

        public async Task<Destination> GetOneAsync(int id)
        {
            using var context = _dbFactory.CreateDbContext();
            return await context.Destinations.Where(d => d.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> CreateAsync(Destination newDestination)
        {
            using var context = _dbFactory.CreateDbContext();
            var destinations = context.Destinations;
            await destinations.AddAsync(newDestination);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(Destination editedDestination)
        {
            using var context = _dbFactory.CreateDbContext();
            var destination = context.Destinations.Where(d => d.Id == editedDestination.Id).FirstOrDefault();
            destination.Name = editedDestination.Name;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var context = _dbFactory.CreateDbContext();
            var destinations = context.Destinations;
            var destination = await destinations.Where(d => d.Id == id).FirstOrDefaultAsync();
            if(destination != null)
            {
                destinations.Remove(destination);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
