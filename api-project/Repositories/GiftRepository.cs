using System.Drawing;
using api_project.Data;
using api_project.DTO;
using api_project.Models;
using Microsoft.EntityFrameworkCore;

namespace api_project.Repositories
{
    public class GiftRepository
    {
        private readonly ApplicationDbContext _context;
        public GiftRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Gifts>> GetAllGiftssAsync()
        {
            return await _context.Gifts.ToListAsync();
        }
        public async Task<Gifts?> GetGiftByIdAsync(int id)
        {
            return await _context.Gifts
                .FindAsync(id);
        }
        public async Task<Gifts> CreateGift(Gifts gift)
        {
            _context.Gifts.Add(gift);
            await _context.SaveChangesAsync();
            return gift;
        }
        public async Task<Gifts?> UpdateAsync(Gifts gift)
        {
            var existing = await _context.Gifts.FindAsync(gift.Id);
            if (existing == null) return null;
            _context.Entry(existing).CurrentValues.SetValues(gift);
            await _context.SaveChangesAsync();
            return existing;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var gift = await _context.Gifts.FindAsync(id);
            if (gift == null) return false;
            _context.Gifts.Remove(gift);
            await _context.SaveChangesAsync();
            return true;
        }
        //public async Task<Donors?> GetDonorByGiftId(int giftID)
        //{
        //    var donor = await _context.Gifts
        //        .Where(g => g.Id == giftID)
        //        .Include(g => g.donors)
        //        .Select(g => g.donors)
        //        .FirstOrDefaultAsync();
        //    return donor;
        //}
        public async Task<Donors?> GetDonorByGiftIdAsync(int giftId)
        {
            var gift = await _context.Gifts
                .Include(g => g.donors)
                .FirstOrDefaultAsync(g => g.Id == giftId);
            return gift?.donors;
        }
         public async Task<Gifts?> GetGiftByName(string giftName)
        {
            return await _context.Gifts
                .FirstOrDefaultAsync(g => g.GiftName == giftName);
        }
        public async Task<IEnumerable<Gifts>> GetGiftByDonorName(string donorName)
        {
            return await _context.Gifts
                .Include(g => g.donors)
                .Where(g => g.donors != null && g.donors.DonorName == donorName)
                .ToListAsync();
        }
        public async Task<IEnumerable<Gifts?>> GetGiftByNumOfPurchases(int numOfPurchases)
        {
           return await _context.Gifts
                .Where(g => g.NumOfPurchases == numOfPurchases)
                .ToListAsync();
        }
        
    }
}
