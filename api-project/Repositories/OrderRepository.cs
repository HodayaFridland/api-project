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







        
        ////users fuctions
        public async Task<IEnumerable<Gifts>> GetAllGifts(int ? minprice,int?maxprice,string? category)
        {
            return await _context.Gifts.Where(g=>
                (!minprice.HasValue || g.Price >= minprice.Value) &&
                (!maxprice.HasValue || g.Price <= maxprice.Value) &&
                (string.IsNullOrEmpty(category) || g.category!.CategoryName == category)
            ).ToListAsync();
        }
        public async Task<IEnumerable<Orders?>?> AddItemToBasket(int userId, int giftId, int quantity = 1)
        {
            if (await IsHasWinner(giftId)) 
                return null;
                for (int i = 0; i < quantity; i++)
                {
                    var order = new Orders
                    {
                        UserId = userId,
                        GiftId = giftId,
                        IsConfirmed = false
                    };
                    _context.Orders.Add(order);
                }
                await _context.SaveChangesAsync();
            
                var orders = await _context.Orders
                    .Where(o => o.UserId == userId && !o.IsConfirmed)
                    .Include(o => o.gift)
                    .ToListAsync();
                return orders;
        }

        public async Task<bool> ConfirmOrder(int userId)
        {
            
            var orders = await _context.Orders
                .Where(o => o.UserId == userId && !o.IsConfirmed)
                .Include(o => o.gift).Where(o => o.gift.winner == null)
                .ToListAsync();

            if (!orders.Any()) return false;
            foreach (var order in orders)
            {
                order.IsConfirmed = true;
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> IsHasWinner(int giftId)
        {
            return await _context.Gifts.Where(g => g.Id == giftId)
                .AnyAsync(g => g.winner!=null);

        }
        public async Task<bool> RemoveItemFromBasket(int userId, int giftId)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.UserId == userId && o.GiftId == giftId && !o.IsConfirmed);
            if (order == null) return false;
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<IEnumerable<Gifts>> GetAllGiftsWithWinners()
        {
            return await _context.Gifts.
                Include(g => g.winner)
               .ToListAsync();
        }
    }
}
