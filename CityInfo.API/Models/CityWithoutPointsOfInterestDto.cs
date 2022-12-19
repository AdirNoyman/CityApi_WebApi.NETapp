using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Models
{
    public class CityWithoutPointsOfInterestDto
    {

        /// <summary>
        /// The ID of a city
        /// </summary>
        public int CityId { get; set; }
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The Description of a city
        /// </summary>
        public string? Description { get; set; }

    }
}