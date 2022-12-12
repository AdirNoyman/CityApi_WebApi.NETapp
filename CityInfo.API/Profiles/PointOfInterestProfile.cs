using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper;

namespace CityInfo.API.Profiles
{
    public class PointOfInterestProfile : Profile
    {
        public PointOfInterestProfile()
        {
            // source (DB entity/table) => to target (Dto)
            CreateMap<Entities.PointOfInterest, Models.PointOfInterestDto>();
            // source (Dto) => to target (DB entity/table)
            CreateMap<Models.PointOfInterestForCreationDto, Entities.PointOfInterest>();
            // source (Dto) => to target (DB entity/table)
            CreateMap<Models.PointOfInterestForUpdateDto, Entities.PointOfInterest>();
            // source (DB entity/table) => to target (Dto)
            CreateMap<Entities.PointOfInterest, Models.PointOfInterestForUpdateDto>();
        }
    }
}