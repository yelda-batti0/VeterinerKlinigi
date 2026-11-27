using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeterinerKlinigi.Context;
using VeterinerKlinigi.Dtos.VeterinerDtos;

namespace VeterinerKlinigi.ViewComponents.DefaultViewComponents
{
    public class _DefaultVeterinerComponentPartial : ViewComponent
    {
        private readonly VeterinerContext _context;
        private readonly IMapper _mapper;

        public _DefaultVeterinerComponentPartial(VeterinerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var veterinerler = await _context.Veterinerler
                .Include(v => v.Klinik)
                .Include(v => v.UzmanlikAlanlari)
                .AsSplitQuery()
                .OrderBy(v => v.Ad)
                .Take(8)
                .ToListAsync();

            var values = _mapper.Map<List<ResultVeterinerDto>>(veterinerler);
            return View(values);
        }
    }
}