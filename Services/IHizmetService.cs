using VeterinerKlinigi.Entities;

namespace VeterinerKlinigi.Services
{
    public interface IHizmetService
    {
        Task<List<Hizmet>> GetAllAsync(int? klinikId = null);
        Task<Hizmet?> GetByIdAsync(int id);
        Task<string?> CreateAsync(Hizmet hizmet);
        Task<string?> UpdateAsync(Hizmet hizmet);
        Task<string?> DeleteAsync(int id);
    }
}