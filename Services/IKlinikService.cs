using VeterinerKlinigi.Entities;

namespace VeterinerKlinigi.Services
{
    public interface IKlinikService
    {
        Task<List<Klinik>> GetAllAsync();
        Task<Klinik?> GetByIdAsync(int id);

        // Hata varsa hata mesajı döner, başarılıysa null döner
        Task<string?> CreateAsync(Klinik klinik);
        Task<string?> UpdateAsync(Klinik klinik);
        Task<string?> DeleteAsync(int id);
    }
}
