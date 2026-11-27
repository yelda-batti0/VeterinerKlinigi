using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeterinerKlinigi.Context;
using VeterinerKlinigi.Dtos.HizmetDtos;

namespace VeterinerKlinigi.ViewComponents.DefaultViewComponents
{
    public class _DefaultHizmetComponentPartial : ViewComponent
    {
        private readonly VeterinerContext _context;
        private readonly IMapper _mapper;

        public _DefaultHizmetComponentPartial(VeterinerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var hizmetler = await _context.Hizmetler
                .Include(h => h.Klinik)
                .OrderBy(h => h.Ad)
                .ToListAsync();

            var values = _mapper.Map<List<ResultHizmetDto>>(hizmetler);
            return View(values);
        }
    }
}