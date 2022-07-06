using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ParentCapitalBlazor.Data
{
    public static class FactoryExtensions
    {
        public static IServiceCollection AddDbContextFactory<TContext>(
            this IServiceCollection collection,
            Action<DbContextOptionsBuilder> optionsAction = null,
            ServiceLifetime contextAndOptionsLifetime = ServiceLifetime.Singleton)
            where TContext : DbContext
        {
            // instantiate with the correctly scoped provider
            collection.Add(new ServiceDescriptor(
                typeof(IDbContextFactory<TContext>),
                sp => new DbContextFactory<TContext>(sp),
                contextAndOptionsLifetime));

            // dynamically run the builder on each request
            collection.Add(new ServiceDescriptor(
                typeof(DbContextOptions<TContext>),
                sp => GetOptions<TContext>(optionsAction, sp),
                contextAndOptionsLifetime));

            return collection;
        }

        private static DbContextOptions<TContext> GetOptions<TContext>(
            Action<DbContextOptionsBuilder> action,
            IServiceProvider sp = null) where TContext : DbContext
        {
            var optionsBuilder = new DbContextOptionsBuilder<TContext>();
            if (sp != null)
            {
                optionsBuilder.UseApplicationServiceProvider(sp);
            }
            action?.Invoke(optionsBuilder);
            return optionsBuilder.Options;
        }
    }
}
