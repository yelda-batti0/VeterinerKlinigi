using VeterinerKlinigi.Entities;

namespace VeterinerKlinigi.Services
{
    public interface IVeterinerService
    {
        Task<List<VeterinerHekim>> GetAllAsync(int? klinikId = null);
        Task<VeterinerHekim?> GetByIdAsync(int id);

        Task<string?> CreateAsync(VeterinerHekim veteriner, List<int> uzmanlikIds, List<int> hizmetIds, string email, string sifre);
        Task<string?> UpdateAsync(VeterinerHekim veteriner, List<int> uzmanlikIds, List<int> hizmetIds);
        Task<string?> DeleteAsync(int id);

        Task<string?> MusaitlikEkleAsync(HekimMusaitlik musaitlik);
        Task<string?> MusaitlikSilAsync(int musaitlikId);
    }
}