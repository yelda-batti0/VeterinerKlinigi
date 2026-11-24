using VeterinerKlinigi.Entities;

namespace VeterinerKlinigi.Services
{
    public interface IUzmanlikAlaniService
    {
        Task<List<UzmanlikAlani>> GetAllAsync();
        Task<UzmanlikAlani?> GetByIdAsync(int id);
        Task<string?> CreateAsync(UzmanlikAlani uzmanlik);
        Task<string?> UpdateAsync(UzmanlikAlani uzmanlik);
        Task<string?> DeleteAsync(int id);
    }
}