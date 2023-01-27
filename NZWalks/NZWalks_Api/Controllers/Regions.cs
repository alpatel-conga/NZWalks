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
        private IMapper _mapper;

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

            var regionsDTO = new List<Models.DTO.Region>();
            regions.ToList().ForEach(region =>
            {
                var regionDTO = new Models.DTO.Region()
                {
                    Id = region.Id,
                    Name = region.Name,
                    Code = region.Code,
                    Long = region.Long,
                    Area = region.Area,
                    population = region.population,
                    Lat = region.Lat,
                };

                regionsDTO.Add(regionDTO);
            });
          /*  var regionsDTO = _mapper.Map<List<Models.Domain.Region>>(regions);*/

            return Ok(regionsDTO);
        }


        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region = await regionRepository.GetAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            var regionDTO = _mapper.Map<Models.DTO.Region>(region);
            return Ok(regionDTO);
        }
        [HttpPost]
        
        public async Task<IActionResult> AddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
        {
            //Request mathi je data avaya a aaapne pela domail naakhya..
            var region = new Models.Domain.Region()
            {
                Code= addRegionRequest.Code,
                Name= addRegionRequest.Name,
                Area= addRegionRequest.Area,
                Long= addRegionRequest.Long,
                Lat= addRegionRequest.Lat,
                population=addRegionRequest.population

            };
            region=await regionRepository.AddAsync(region);

            var regionDTO = new Models.DTO.Region()
            {
                Id=region.Id,
                Code=region.Code,
                Name=region.Name,
                Area=region.Area,
                Long=region.Long,
                Lat=region.Lat,
                population=region.population
            };
            return CreatedAtAction(("GetRegionAsync"),new {id=region.Id},regionDTO);
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var region= await regionRepository.DeleteRegionAsync(id);

            if(region==null)
            {
                return null;
            }

            var regionDTO = new Models.DTO.Region()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,


                Area = region.Area,
                Long = region.Long,
                Lat = region.Lat,
                population = region.population
            };
            return Ok(regionDTO);
        }


        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateSync([FromRoute]Guid id,[FromBody]Models.DTO.UpdateRegionRequest updateRegionRequest)
        {
            var region = new Models.Domain.Region()
            {
               Code=updateRegionRequest.Code,
               Name=updateRegionRequest.Name,
               Area=updateRegionRequest.Area,
               Long=updateRegionRequest.Long,
               Lat=updateRegionRequest.Lat,
               population= updateRegionRequest.population
            };

            region=await regionRepository.UpdateRegionAsync(id,region);

            if(region==null)
            {
                return NotFound();
            }

            var regionDTO = new Models.DTO.Region()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,


                Area = region.Area,
                Long = region.Long,
                Lat = region.Lat,
                population = region.population
            };

            return Ok(regionDTO);
        }
    }
}
