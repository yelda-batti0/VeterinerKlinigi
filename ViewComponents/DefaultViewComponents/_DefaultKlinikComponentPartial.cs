using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeterinerKlinigi.Context;
using VeterinerKlinigi.Dtos.KlinikDtos;

namespace VeterinerKlinigi.ViewComponents.DefaultViewComponents
{
    public class _DefaultKlinikComponentPartial : ViewComponent
    {
        private readonly VeterinerContext _context;
        private readonly IMapper _mapper;

        public _DefaultKlinikComponentPartial(VeterinerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var klinikler = await _context.Klinikler
                .Include(k => k.Hizmetler)
                .Include(k => k.Veterinerler)
                .AsSplitQuery()
                .OrderBy(k => k.Ad)
                .ToListAsync();

            var values = _mapper.Map<List<ResultKlinikDto>>(klinikler);
            return View(values);
        }
    }
}