using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ParentCapitalBlazor.Data
{
    public interface ICertificateService
    {
        Task<List<Certificate>> GetAllAsync();
        Task<List<Certificate>> GetRegistrAsync();
        Task<List<Certificate>> GetAllPeriodAsync(DateTime db, DateTime de);
        Task<List<Certificate>> GetAllForIndexationAsync(DateTime dateInd);
        Task<Certificate> GetOneAsync(int id);
        Task<bool> CreateAsync(Certificate newCertificate);
        Task<bool> UpdateAsync(Certificate editedCertificate);
        Task<bool> DeleteAsync(int id);
        Task<bool> FindByNumberAsync(int number);
        Task<int> CountByNumberAsync(int number);
    }

    public class CertificateService : ICertificateService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

        public CertificateService(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<List<Certificate>> GetAllAsync()
        {
            using var context = _dbFactory.CreateDbContext();
            var certificates = await context.Certificates.
                Include(c => c.Gender).
                OrderByDescending(c => c.Number).ToListAsync();

            return certificates;
        }

        public async Task<List<Certificate>> GetRegistrAsync()
        {
            using var context = _dbFactory.CreateDbContext();

            var certificates = await context.Certificates.
                Include(c => c.Gender).
                Include(c => c.Children).Include("Children.Gender").
                Include(c => c.Orders).Include("Orders.Destination").
                Include(c => c.Indexations).
                OrderBy(c => c.Number).ToListAsync();

            return certificates;
        }

        public async Task<List<Certificate>> GetAllPeriodAsync(DateTime db, DateTime de)
        {
            using var context = _dbFactory.CreateDbContext();
            var certificates = await context.Certificates.Where(c => c.Date >= db & c.Date <= de).
                Include(c => c.Orders).
                Include(c => c.Indexations).ToListAsync<Certificate>();
        
            return certificates;
        }

        public async Task<List<Certificate>> GetAllForIndexationAsync(DateTime dateInd)
        {
            using var context = _dbFactory.CreateDbContext();
            var certificates = await context.Certificates.
                Where(c => c.Date <= dateInd).
                Include(c => c.Orders).
                Include(c => c.Indexations).ToListAsync<Certificate>();
        
            return certificates;
        }

        public async Task<Certificate> GetOneAsync(int id)
        {
            using var context = _dbFactory.CreateDbContext();
            
            Certificate certificate = await context.Certificates.Where(c => c.Id == id).
                Include(c => c.Gender).
                Include(c => c.Children).Include("Children.Gender").
                Include(c => c.Orders).Include("Orders.Destination").
                Include(c => c.Indexations).FirstOrDefaultAsync();

            return certificate;
        }

        public async Task<bool> CreateAsync(Certificate newCertificate)
        {
            using var context = _dbFactory.CreateDbContext();
            var certificates = context.Certificates;

            await certificates.AddAsync(newCertificate);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(Certificate editedCertificate)
        {
            using var context = _dbFactory.CreateDbContext();
            var certificate = context.Certificates.Where(s => s.Id == editedCertificate.Id).FirstOrDefault();
            certificate.Number = editedCertificate.Number;
            certificate.Date = editedCertificate.Date;
            certificate.Parent = editedCertificate.Parent;
            certificate.GenderId = editedCertificate.GenderId;
            certificate.DateOfBirth = editedCertificate.DateOfBirth;
            certificate.Address = editedCertificate.Address;
            certificate.Document = editedCertificate.Document;
            certificate.Description = editedCertificate.Description;
            certificate.Sum = editedCertificate.Sum;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var context = _dbFactory.CreateDbContext();
            var certificates = context.Certificates;
            var certificate = await certificates.Where(s => s.Id == id).FirstOrDefaultAsync();
            if(certificate != null)
            {
                certificates.Remove(certificate);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> FindByNumberAsync(int number)
        {
            using var context = _dbFactory.CreateDbContext();
            
            Certificate certificate = await context.Certificates.Where(c => c.Number == number).FirstOrDefaultAsync();
            if(certificate != null)
            {
                return true;
            }

            return false;
        }

        public async Task<int> CountByNumberAsync(int number)
        {
            using var context = _dbFactory.CreateDbContext();
            return await context.Certificates.Where(c => c.Number == number).CountAsync();
        }
    }
}
