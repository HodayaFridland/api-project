using api_project.Data;
using api_project.Models;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;

namespace api_project.Repositories
{
    public class OrderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Orders>> GetAllOrdersByGiftAsync(int giftId)
        {
            return await _context.Orders
                .Where(o => o.GiftId == giftId && o.IsConfirmed)
                .ToListAsync();
        }
        public async Task<IEnumerable<Gifts>> GetGiftsDesc()
        {
            return await _context.Gifts
           .OrderByDescending(g => g.Price)
           .ToListAsync();
        }
        public async Task<IEnumerable<Gifts>> GetThePopularGiftAsync()
        {
            return await _context.Gifts
               .OrderByDescending(g =>
                _context.Orders.Count(o => o.GiftId == g.Id && o.IsConfirmed))
                .ToListAsync();
        }
        public async Task<IEnumerable<Users>> GetPurchasers()
        {
            return await _context.Orders
                .Where(o => o.IsConfirmed)
                .Select(o => o.User!)
                .Distinct()
                .ToListAsync();
        }

        

    }
}
