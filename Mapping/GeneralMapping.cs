using AutoMapper;
using VeterinerKlinigi.Dtos.AsiKaydiDtos;
using VeterinerKlinigi.Dtos.EvcilHayvanDtos;
using VeterinerKlinigi.Dtos.HizmetDtos;
using VeterinerKlinigi.Dtos.KlinikDtos;
using VeterinerKlinigi.Dtos.MusaitlikDtos;
using VeterinerKlinigi.Dtos.RandevuDtos;
using VeterinerKlinigi.Dtos.UzmanlikAlaniDtos;
using VeterinerKlinigi.Dtos.VeterinerDtos;
using VeterinerKlinigi.Entities;

namespace VeterinerKlinigi.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            // ---------- KLİNİK ----------
            CreateMap<Klinik, ResultKlinikDto>()
                .ForMember(dest => dest.HizmetSayisi, opt => opt.MapFrom(src => src.Hizmetler.Count))
                .ForMember(dest => dest.VeterinerSayisi, opt => opt.MapFrom(src => src.Veterinerler.Count));
            CreateMap<Klinik, CreateKlinikDto>().ReverseMap();
            CreateMap<Klinik, UpdateKlinikDto>().ReverseMap();
            CreateMap<Klinik, DeleteKlinikDto>()
                .ForMember(dest => dest.HizmetSayisi, opt => opt.MapFrom(src => src.Hizmetler.Count))
                .ForMember(dest => dest.VeterinerSayisi, opt => opt.MapFrom(src => src.Veterinerler.Count));

            // ---------- HİZMET ----------
            CreateMap<Hizmet, ResultHizmetDto>()
                .ForMember(dest => dest.KlinikAd, opt => opt.MapFrom(src => src.Klinik!.Ad))
                .ForMember(dest => dest.VeterinerSayisi, opt => opt.MapFrom(src => src.Veterinerler.Count))
                .ForMember(dest => dest.RandevuSayisi, opt => opt.MapFrom(src => src.Randevular.Count));
            CreateMap<Hizmet, CreateHizmetDto>().ReverseMap();
            CreateMap<Hizmet, UpdateHizmetDto>().ReverseMap();
            CreateMap<Hizmet, DeleteHizmetDto>()
                .ForMember(dest => dest.KlinikAd, opt => opt.MapFrom(src => src.Klinik!.Ad))
                .ForMember(dest => dest.RandevuSayisi, opt => opt.MapFrom(src => src.Randevular.Count));

            // ---------- UZMANLIK ALANI ----------
            CreateMap<UzmanlikAlani, ResultUzmanlikAlaniDto>()
                .ForMember(dest => dest.VeterinerSayisi, opt => opt.MapFrom(src => src.Veterinerler.Count));
            CreateMap<UzmanlikAlani, CreateUzmanlikAlaniDto>().ReverseMap();
            CreateMap<UzmanlikAlani, UpdateUzmanlikAlaniDto>().ReverseMap();
            CreateMap<UzmanlikAlani, DeleteUzmanlikAlaniDto>()
                .ForMember(dest => dest.VeterinerSayisi, opt => opt.MapFrom(src => src.Veterinerler.Count));

            // ---------- VETERİNER ----------
            CreateMap<Veteriner, ResultVeterinerDto>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.AppUser!.Email))
                .ForMember(dest => dest.KlinikAd, opt => opt.MapFrom(src => src.Klinik!.Ad))
                .ForMember(dest => dest.Uzmanliklar, opt => opt.MapFrom(src => src.UzmanlikAlanlari.Select(u => u.Ad).ToList()))
                .ForMember(dest => dest.Hizmetler, opt => opt.MapFrom(src => src.Hizmetler.Select(h => h.Ad).ToList()))
                .ForMember(dest => dest.CalismaGunleri, opt => opt.MapFrom(src => src.Musaitlikler.Select(m => m.Gun).Distinct().ToList()))
                .ForMember(dest => dest.HaftalikCalismaDakika, opt => opt.MapFrom(src => (int)src.Musaitlikler.Sum(m => (m.BitisSaati - m.BaslangicSaati).TotalMinutes)))
                .ForMember(dest => dest.RandevuSayisi, opt => opt.MapFrom(src => src.Randevular.Count));
            CreateMap<Veteriner, CreateVeterinerDto>().ReverseMap();
            CreateMap<Veteriner, UpdateVeterinerDto>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.AppUser!.Email))
                .ForMember(dest => dest.SeciliUzmanlikIds, opt => opt.MapFrom(src => src.UzmanlikAlanlari.Select(u => u.UzmanlikAlaniId).ToList()))
                .ForMember(dest => dest.SeciliHizmetIds, opt => opt.MapFrom(src => src.Hizmetler.Select(h => h.HizmetId).ToList()))
                .ReverseMap();
            CreateMap<Veteriner, DeleteVeterinerDto>()
                .ForMember(dest => dest.KlinikAd, opt => opt.MapFrom(src => src.Klinik!.Ad))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.AppUser!.Email))
                .ForMember(dest => dest.RandevuSayisi, opt => opt.MapFrom(src => src.Randevular.Count));

            // ---------- MÜSAİTLİK ----------
            CreateMap<Musaitlik, ResultMusaitlikDto>();
            CreateMap<Musaitlik, UpdateMusaitlikDto>().ReverseMap();
            CreateMap<Musaitlik, DeleteMusaitlikDto>();

            // ---------- EVCİL HAYVAN ----------
            CreateMap<EvcilHayvan, ResultEvcilHayvanDto>()
                .ForMember(dest => dest.SahipAdSoyad, opt => opt.MapFrom(src => src.AppUser!.Ad + " " + src.AppUser.Soyad))
                .ForMember(dest => dest.AsiSayisi, opt => opt.MapFrom(src => src.AsiKayitlari.Count))
                .ForMember(dest => dest.YaklasanAsiTarihi, opt => opt.MapFrom(src => src.AsiKayitlari
                    .Where(a => a.SonrakiDozTarihi.HasValue && a.SonrakiDozTarihi.Value.Date >= DateTime.Today)
                    .Select(a => a.SonrakiDozTarihi)
                    .Min()));
            CreateMap<EvcilHayvan, CreateEvcilHayvanDto>().ReverseMap();
            CreateMap<EvcilHayvan, UpdateEvcilHayvanDto>()
                .ReverseMap()
                .ForMember(dest => dest.FotografUrl, opt => opt.Ignore());
            CreateMap<EvcilHayvan, DeleteEvcilHayvanDto>()
                .ForMember(dest => dest.AsiSayisi, opt => opt.MapFrom(src => src.AsiKayitlari.Count))
                .ForMember(dest => dest.RandevuSayisi, opt => opt.MapFrom(src => src.Randevular.Count));

            // ---------- AŞI KAYDI ----------
            CreateMap<AsiKaydi, ResultAsiKaydiDto>()
                .ForMember(dest => dest.EvcilHayvanAd, opt => opt.MapFrom(src => src.EvcilHayvan!.Ad));
            CreateMap<AsiKaydi, CreateAsiKaydiDto>().ReverseMap();
            CreateMap<AsiKaydi, UpdateAsiKaydiDto>()
                .ReverseMap()
                .ForMember(dest => dest.EvcilHayvanId, opt => opt.Ignore());
            CreateMap<AsiKaydi, DeleteAsiKaydiDto>()
                .ForMember(dest => dest.EvcilHayvanAd, opt => opt.MapFrom(src => src.EvcilHayvan!.Ad));

            // ---------- RANDEVU ----------
            CreateMap<Randevu, ResultRandevuDto>()
                .ForMember(dest => dest.EvcilHayvanAd, opt => opt.MapFrom(src => src.EvcilHayvan!.Ad))
                .ForMember(dest => dest.SahipAdSoyad, opt => opt.MapFrom(src => src.EvcilHayvan!.AppUser!.Ad + " " + src.EvcilHayvan.AppUser.Soyad))
                .ForMember(dest => dest.HizmetAd, opt => opt.MapFrom(src => src.Hizmet!.Ad))
                .ForMember(dest => dest.VeterinerAdSoyad, opt => opt.MapFrom(src => "Vet. Hek. " + src.Veteriner!.Ad + " " + src.Veteriner.Soyad))
                .ForMember(dest => dest.KlinikAd, opt => opt.MapFrom(src => src.Veteriner!.Klinik!.Ad));
            CreateMap<Randevu, UpdateRandevuDto>()
                .ForMember(dest => dest.Tarih, opt => opt.MapFrom(src => src.BaslangicZamani.Date))
                .ForMember(dest => dest.Saat, opt => opt.MapFrom(src => src.BaslangicZamani.TimeOfDay));
            CreateMap<Randevu, MuayeneNotuDto>()
                .ForMember(dest => dest.MuayeneNotu, opt => opt.NullSubstitute(string.Empty));
            CreateMap<Randevu, DeleteRandevuDto>()
                .ForMember(dest => dest.EvcilHayvanAd, opt => opt.MapFrom(src => src.EvcilHayvan!.Ad))
                .ForMember(dest => dest.HizmetAd, opt => opt.MapFrom(src => src.Hizmet!.Ad))
                .ForMember(dest => dest.VeterinerAdSoyad, opt => opt.MapFrom(src => "Vet. Hek. " + src.Veteriner!.Ad + " " + src.Veteriner.Soyad));
        }
    }
}