using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ParentCapitalBlazor.Data
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllAsync();
        Task<Order> GetOneAsync(int id);
        Task<bool> CreateAsync(Order newOrder);
        Task<bool> UpdateAsync(Order editedOrder);
        Task<bool> DeleteAsync(int id);
    }

    public class OrderService : IOrderService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

        public OrderService(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<List<Order>> GetAllAsync()
        {
            using var context = _dbFactory.CreateDbContext();
            var orders = await context.Orders.Include(o => o.Destination).ToListAsync();
            return orders;
        }

        public async Task<Order> GetOneAsync(int id)
        {
            using var context = _dbFactory.CreateDbContext();
            return await context.Orders.Where(o => o.Id == id).Include(o => o.Destination).FirstOrDefaultAsync();
        }

        public async Task<bool> CreateAsync(Order newOrder)
        {
            using var context = _dbFactory.CreateDbContext();
            var orders = context.Orders;
            await orders.AddAsync(newOrder);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(Order editedOrder)
        {
            using var context = _dbFactory.CreateDbContext();
            var order = context.Orders.Where(o => o.Id == editedOrder.Id).FirstOrDefault();

            order.Date = editedOrder.Date;
            order.Number = editedOrder.Number;
            order.DatePay = editedOrder.DatePay;
            order.DestinationId = editedOrder.DestinationId;
            order.Sum = editedOrder.Sum;
            order.Description = editedOrder.Description;
            
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var context = _dbFactory.CreateDbContext();
            var orders = context.Orders;
            var order = await orders.Where(o => o.Id == id).FirstOrDefaultAsync();
            if(order != null)
            {
                orders.Remove(order);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
