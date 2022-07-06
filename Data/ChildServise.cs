using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ParentCapitalBlazor.Data
{
    public interface IChildService
    {
        Task<List<Child>> GetAllAsync();
        Task<Child> GetOneAsync(int id);
        Task<bool> CreateAsync(Child newChild);
        Task<bool> UpdateAsync(Child editedChild);
        Task<bool> DeleteAsync(int id);
    }

    public class ChildService : IChildService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

        public ChildService(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<List<Child>> GetAllAsync()
        {
            using var context = _dbFactory.CreateDbContext();
            var children = await context.Children.Include(c => c.Gender).ToListAsync();
            return children;
        }

        public async Task<Child> GetOneAsync(int id)
        {
            using var context = _dbFactory.CreateDbContext();
            return await context.Children.Where(c => c.Id == id).Include(c => c.Gender).FirstOrDefaultAsync();
        }

        public async Task<bool> CreateAsync(Child newChild)
        {
            using var context = _dbFactory.CreateDbContext();
            var children = context.Children;
            await children.AddAsync(newChild);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(Child editedChild)
        {
            using var context = _dbFactory.CreateDbContext();
            var child = context.Children.Where(c => c.Id == editedChild.Id).FirstOrDefault();
            
            child.Name = editedChild.Name;
            child.GenderId = editedChild.GenderId;
            child.DateOfBirth = editedChild.DateOfBirth;
            child.PlaceOfBirth = editedChild.PlaceOfBirth;
            child.Document = editedChild.Document;
            child.Citizenship = editedChild.Citizenship;
            child.Address = editedChild.Address;
            //child.CertificateId = editedChild.CertificateId;

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var context = _dbFactory.CreateDbContext();
            var children = context.Children;
            var child = await children.Where(c => c.Id == id).FirstOrDefaultAsync();
            if(child != null)
            {
                children.Remove(child);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
