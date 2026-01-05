using api_project.Models;

namespace api_project.Repositories
{
    public interface IDonorRepository
    {
        Task<Donors> CreateDonorAsync(Donors donor);
        Task<bool> DeleteAsync(int id);
        Task<bool> EmailExistsAsync(string email);
        Task<IEnumerable<Donors?>> GetDonorsAsync(string? name, string? email, string? giftName);
        Task<Donors?> GetByEmailAsync(string email);
        Task<Donors?> GetDonorByIdAsync(int id);
        Task<Donors?> UpdateAsync(Donors donor);
    }
}