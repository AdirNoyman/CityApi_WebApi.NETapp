using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace CityInfo.API.Profiles
{
    public class CityProfile : Profile
    {
        // Create fields mapping between the City entity class to the CityPointsOfInterestDto class
        public CityProfile()
        {
            // Source to target mapping
            CreateMap<Entities.City, Models.CityWithoutPointsOfInterestDto>();
            // Source to target mapping
            CreateMap<Entities.City, Models.CityDto>();
        }
    }
}