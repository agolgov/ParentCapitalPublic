using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ParentCapitalBlazor.Data
{
    public interface IGenderService
    {
        Task<List<Gender>> GetAllAsync();
        Task<Gender> GetOneAsync(int id);
        Task<bool> CreateAsync(Gender newGender);
        Task<bool> UpdateAsync(Gender editedGender);
        Task<bool> DeleteAsync(int id);
    }

    public class GenderService : IGenderService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

        public GenderService(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<List<Gender>> GetAllAsync()
        {
            using var context = _dbFactory.CreateDbContext();
            var genders = await context.Genders.ToListAsync();
            return genders;
        }

        public async Task<Gender> GetOneAsync(int id)
        {
            using var context = _dbFactory.CreateDbContext();
            return await context.Genders.Where(g => g.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> CreateAsync(Gender newGender)
        {
            using var context = _dbFactory.CreateDbContext();
            var genders = context.Genders;
            await genders.AddAsync(newGender);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(Gender editedGender)
        {
            using var context = _dbFactory.CreateDbContext();
            var gender = context.Genders.Where(g => g.Id == editedGender.Id).FirstOrDefault();
            gender.Name = editedGender.Name;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var context = _dbFactory.CreateDbContext();
            var genders = context.Genders;
            var gender = await genders.Where(g => g.Id == id).FirstOrDefaultAsync();
            if(gender != null)
            {
                genders.Remove(gender);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
