using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks_Api.Models.Domain;
using NZWalks_Api.Repositories;

namespace NZWalks_Api.Controllers
{
    [ApiController]
    [Route("Regions")]
    
    public class Regions : Controller
    {
        private readonly IRegionRepository regionRepository;
        private IMapper _mapper { get; }

        public Regions(IRegionRepository regionRepository,IMapper mapper)
        {
            this.regionRepository = regionRepository;
            _mapper = mapper;
        }

        

        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            /* var regions = new List<Region>()
             {
                 new Region{Id=Guid.NewGuid(),Name="Welligton",Code="WLG",Area=227755,Long=299.88,population=500000},
                 new Region{Id=Guid.NewGuid(),Name="Aukland",Code="AUK",Area=217755,Long=399.88,population=400000}

             };*/
            var regions = await regionRepository.GetAllAsync();

           /* var regionsDTO = new List<Models.DTO.Region>();
            regions.ToList().ForEach(region => {
                var regionDTO=new Models.DTO.Region()
                {
                    Id= region.Id,
                    Name= region.Name,
                    Code= region.Code,
                    Long= region.Long,
                    Area= region.Area,
                    population=region.population,
                    Lat= region.Lat,
                };

                regionsDTO.Add(regionDTO);
            });*/
           var regionsDTO=_mapper.Map<List<Models.DTO.Region>>(regions);
            
            return Ok(regionsDTO);
        }
    }
}
