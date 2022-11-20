using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities/{cityId}/pointsofintrest")]
    public class PointsOfIntrestController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<PointOfIntrestDto>> GetPointsOfIntrest(int cityId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            return Ok(city.PointsOfIntrest);

        }

        [HttpGet("{pointofintrestid}")]
        public ActionResult<PointOfIntrestDto> GetPointOfIntrest(int cityId, int pointofintrestid)
        {

            // Find city
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            // Find point of intrest
            var pointOfIntrest = city.PointsOfIntrest.FirstOrDefault(p => p.Id == pointofintrestid);

            if (pointOfIntrest == null)
            {
                return NotFound();
            }

            return Ok(pointOfIntrest);

        }
    }


}