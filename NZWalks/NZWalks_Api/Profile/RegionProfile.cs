using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;

namespace NZWalks_Api;

public class RegionProfile : Profile 
{
    public RegionProfile() 
    {
        CreateMap<Models.Domain.Region, Models.DTO.Region>().ReverseMap();
    
    }

}
