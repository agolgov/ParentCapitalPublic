using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ParentCapitalBlazor.Data
{
    public interface IIndexationService
    {
        Task<List<Indexation>> GetAllAsync();
        Task<Indexation> GetOneAsync(int id);
        Task<bool> CreateAsync(Indexation newIndexation);
        Task<bool> UpdateAsync(Indexation editedIndexation);
        Task<bool> DeleteAsync(int id);
    }

    public class IndexationService : IIndexationService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

        public IndexationService(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<List<Indexation>> GetAllAsync()
        {
            using var context = _dbFactory.CreateDbContext();
            var indexations = await context.Indexations.ToListAsync();
            return indexations;
        }

        public async Task<Indexation> GetOneAsync(int id)
        {
            using var context = _dbFactory.CreateDbContext();
            return await context.Indexations.Where(o => o.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> CreateAsync(Indexation newIndexation)
        {
            using var context = _dbFactory.CreateDbContext();
            var indexations = context.Indexations;
            await indexations.AddAsync(newIndexation);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(Indexation editedIndexation)
        {
            using var context = _dbFactory.CreateDbContext();
            var indexation = context.Indexations.Where(o => o.Id == editedIndexation.Id).FirstOrDefault();
            indexation.Date = editedIndexation.Date;
            indexation.Number = editedIndexation.Number;
            indexation.Sum = editedIndexation.Sum;
            indexation.Description = editedIndexation.Description;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var context = _dbFactory.CreateDbContext();
            var indexations = context.Indexations;
            var indexation = await indexations.Where(o => o.Id == id).FirstOrDefaultAsync();
            if(indexation != null)
            {
                indexations.Remove(indexation);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
