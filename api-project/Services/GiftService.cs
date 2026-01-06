using api_project.DTO;
using api_project.Interfaces;
using api_project.Models;

namespace api_project.Services
{
    public class GiftService : IGiftService
    {
        private readonly IGiftRepository _giftRepository;
        public GiftService(IGiftRepository giftRepository)
        {
            _giftRepository = giftRepository;
        }
        public async Task<IEnumerable<GiftReadDTO?>> GetAllGiftsAsync()
        {
            return (await _giftRepository.GetAllGiftsAsync())
                .Select(g => new GiftReadDTO
                {
                    Id = g.Id,
                    GiftName = g.GiftName,
                    Price = g.Price
                }).ToList();
        }
        public async Task<GiftReadDTO?> GetGiftByIdAsync(int id)
        {
            var gift = await _giftRepository.GetGiftByIdAsync(id);
            if (gift == null) return null;
            return new GiftReadDTO
            {
                Id = gift.Id,
                GiftName = gift.GiftName,
                Price = gift.Price
            };
        }
        public async Task<GiftReadDTO> CreateGiftAsync(GiftCreateDTO giftCreateDTO)
        {
            var gift = new Gifts
            {
                GiftName = giftCreateDTO.GiftName,
                Price = giftCreateDTO.Price,
                Description = giftCreateDTO.Description,
                ImageUrl = giftCreateDTO.ImageUrl,
                CategoryName = giftCreateDTO.CategoryName

            };
            var createdGift = await _giftRepository.CreateGift(gift);
            return new GiftReadDTO
            {
                Id = createdGift.Id,
                GiftName = createdGift.GiftName,
                Price = createdGift.Price
            };
        }
        public async Task<GiftReadDTO?> UpdateGiftAsync(int id, GiftUpdateDto giftUpdateDTO)
        {
            var existingGift = await _giftRepository.GetGiftByIdAsync(id);
            if (existingGift == null) return null;
            existingGift.GiftName = giftUpdateDTO.GiftName;
            existingGift.Price = giftUpdateDTO.Price;
            existingGift.Description = giftUpdateDTO.Description;
            existingGift.ImageUrl = giftUpdateDTO.ImageUrl;
            existingGift.CategoryName = giftUpdateDTO.CategoryName;
            var updatedGift = await _giftRepository.UpdateAsync(existingGift);
            if (updatedGift == null) return null;
            return new GiftReadDTO
            {
                Id = updatedGift.Id,
                GiftName = updatedGift.GiftName,
                Price = updatedGift.Price
            };
        }
        public async Task<bool> DeleteGiftAsync(int id)
        {
            return await _giftRepository.DeleteAsync(id);
        }
        public async Task<DonorReadDTO?> GetDonorByGiftIdAsync(int giftId)
        {
            var donor = await _giftRepository.GetDonorByGiftIdAsync(giftId);
            if (donor == null) return null;
            return new DonorReadDTO
            {
                Id = donor.Id,
                DonorName = donor.DonorName,
                Email = donor.Email
            };
        }
        public async Task<IEnumerable<GiftReadDTO?>> GetGiftByDonorNameAsync(string donorName)
        {
            var gifts = await _giftRepository.GetGiftByDonorName(donorName);
            return gifts.Select(g => new GiftReadDTO
            {
                Id = g.Id,
                GiftName = g.GiftName,
                Price = g.Price
            }).ToList();
        }
        public async Task<IEnumerable<GiftReadDTO?>> GetGiftByNumOfPurchasesAsync(int numOfPurchases)
        {
            var gifts = await _giftRepository.GetGiftByNumOfPurchases(numOfPurchases);
            return gifts.Select(g => new GiftReadDTO
            {
                Id = g.Id,
                GiftName = g.GiftName,
                Price = g.Price
            }).ToList();
        }
        public async Task<decimal> GetPriceByGiftIdAsync(int giftId)
        {
            return await _giftRepository.GetPriceByGiftName(giftId);
        }
        public async Task<GiftReadDTO?> GetGiftByNameAsync(string giftName)
        {
            var gift = await _giftRepository.GetGiftByName(giftName);
            if (gift == null) return null;
            return new GiftReadDTO
            {
                Id = gift.Id,
                GiftName = gift.GiftName,
                Price = gift.Price
            };
        }

    }
}
