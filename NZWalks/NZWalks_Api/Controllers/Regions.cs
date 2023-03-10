using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
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
            // Validate The Request
            
            var region = await regionRepository.GetAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            var regionDTO = _mapper.Map<Models.DTO.Region>(region);
            return Ok(regionDTO);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
        {
            //validate request...
            if (!ValidateAddRegionAsync(addRegionRequest))
            {
                return BadRequest(ModelState);
            }
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
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> UpdateSync([FromRoute]Guid id,[FromBody]Models.DTO.UpdateRegionRequest updateRegionRequest)
        {
            // Validate the incoming request
            if (!ValidateUpdateRegionAsync(updateRegionRequest))
            {
                return BadRequest(ModelState);
            }
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
        private bool ValidateAddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
        {
            if (addRegionRequest == null)
            {
                ModelState.AddModelError(nameof(addRegionRequest),
                    $"Add Region Data is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(addRegionRequest.Code))
            {
                ModelState.AddModelError(nameof(addRegionRequest.Code),
                    $"{nameof(addRegionRequest.Code)} cannot be null or empty or white space.");
            }

            if (string.IsNullOrWhiteSpace(addRegionRequest.Name))
            {
                ModelState.AddModelError(nameof(addRegionRequest.Name),
                    $"{nameof(addRegionRequest.Name)} cannot be null or empty or white space.");
            }

            if (addRegionRequest.Area <= 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Area),
                    $"{nameof(addRegionRequest.Area)} cannot be less than or equal to zero.");
            }

            if (addRegionRequest.population < 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.population),
                    $"{nameof(addRegionRequest.population)} cannot be less than zero.");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }


        private bool ValidateUpdateRegionAsync(Models.DTO.UpdateRegionRequest updateRegionRequest)
        {
            if (updateRegionRequest == null)
            {
                ModelState.AddModelError(nameof(updateRegionRequest),
                    $"Add Region Data is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(updateRegionRequest.Code))
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Code),
                    $"{nameof(updateRegionRequest.Code)} cannot be null or empty or white space.");
            }

            if (string.IsNullOrWhiteSpace(updateRegionRequest.Name))
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Name),
                    $"{nameof(updateRegionRequest.Name)} cannot be null or empty or white space.");
            }

            if (updateRegionRequest.Area <= 0)
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Area),
                    $"{nameof(updateRegionRequest.Area)} cannot be less than or equal to zero.");
            }

            if (updateRegionRequest.population < 0)
            {
                ModelState.AddModelError(nameof(updateRegionRequest.population),
                    $"{nameof(updateRegionRequest.population)} cannot be less than zero.");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

    }
}
