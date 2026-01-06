using api_project.DTO;
using api_project.Models;

namespace api_project.Interfaces
{
    public interface IGiftRepository
    {
        Task<Gifts> CreateGift(Gifts gift);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Gifts>> GetAllGiftsAsync();
        Task<Donors?> GetDonorByGiftIdAsync(int giftId);
        Task<IEnumerable<Gifts>> GetGiftByDonorName(string donorName);
        Task<Gifts?> GetGiftByIdAsync(int id);
        Task<Gifts?> GetGiftByName(string giftName);
        Task<IEnumerable<Gifts?>> GetGiftByNumOfPurchases(int numOfPurchases);
        Task<decimal> GetPriceByGiftName(int giftId);
        Task<Gifts?> UpdateAsync(Gifts gift);
    }
}