using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{

    [Route("api/cities/{cityId}/pointsofintrest")]
    [ApiController]
    public class PointsOfIntrestController : ControllerBase
    {
        private readonly ILogger<PointsOfIntrestController> _logger;

        public PointsOfIntrestController(ILogger<PointsOfIntrestController> logger)
        {

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public ActionResult<IEnumerable<PointOfIntrestDto>> GetPointsOfIntrest(int cityId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                Console.WriteLine("I was called 🤓");
                _logger.LogInformation($"City with id {cityId} wasn't found when trying to access point of intrest 😩");
                return NotFound();
            }

            return Ok(city.PointsOfIntrest);

        }

        // Name = the route name (alias) we gave it
        [HttpGet("{pointofintrestid}", Name = "GetPointsOfIntrest")]
        public ActionResult<PointOfIntrestDto> GetPointOfIntrest(int cityId, int pointofintrestid)
        {

            // Find city
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound("City not found 😢");
            }

            // Find point of intrest
            var pointOfIntrest = city.PointsOfIntrest.FirstOrDefault(p => p.Id == pointofintrestid);

            if (pointOfIntrest == null)
            {
                return NotFound();
            }

            return Ok(pointOfIntrest);

        }

        [HttpPost]
        public ActionResult<PointOfIntrestDto> CreatePointOfIntrest(int cityId, PointOfIntrestForCreationDto pointOfIntrest)
        {

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound("Oopsss...City not found 🤷‍♂️");
            }

            // Just for demo purposes we will find what is the higest Id we have and add 1 to it (later)
            var maxPointOfIntrestId = CitiesDataStore.Current.Cities.SelectMany(c => c.PointsOfIntrest).Max(p => p.Id);

            var finalPointOfIntrest = new PointOfIntrestDto()
            {
                // add 1 to the Id
                Id = ++maxPointOfIntrestId,
                // Get Name and description for the request body
                Name = pointOfIntrest.Name,
                Description = pointOfIntrest.Description

            };

            // Add the newly created point of intrest to the Data store
            city.PointsOfIntrest.Add(finalPointOfIntrest);

            return CreatedAtRoute("GetPointsOfIntrest",
            new
            {
                cityId = cityId,
                pointofintrestid = finalPointOfIntrest.Id
            },
            finalPointOfIntrest
            );
        }

        [HttpPut("{pointofintrestid}")]
        public ActionResult UpdatePointOfIntrest(int cityId, int pointofintrestid, PointOfIntrestForUpdateDto pointOfIntrest)
        {
            // Find city
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound("City not found 😢");
            }

            // Find point of intrest
            var pointOfIntrestFromStore = city.PointsOfIntrest.FirstOrDefault(p => p.Id == pointofintrestid);

            if (pointOfIntrestFromStore == null)
            {
                return NotFound("Point of intrest not found 😢");
            }

            pointOfIntrestFromStore.Name = pointOfIntrest.Name;
            pointOfIntrestFromStore.Description = pointOfIntrest.Description;

            return NoContent();
        }

        [HttpPatch("{pointofintrestid}")]
        public ActionResult PartiallyUpdatePointOfIntrest(int cityId, int pointofintrestid, JsonPatchDocument<PointOfIntrestForUpdateDto> patchDocument)
        {
            // Find city
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound("City not found 😢");
            }

            // Find point of intrest
            var pointOfIntrestFromStore = city.PointsOfIntrest.FirstOrDefault(p => p.Id == pointofintrestid);

            if (pointOfIntrestFromStore == null)
            {
                return NotFound("Point of intrest not found 😢");
            }

            var pointOfIntresToPatch = new PointOfIntrestForUpdateDto()
            {
                Name = pointOfIntrestFromStore.Name,
                Description = pointOfIntrestFromStore.Description
            };

            patchDocument.ApplyTo(pointOfIntresToPatch, ModelState);

            // Check if the patch requested update request is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if the patch requested update request is valid after doing the update to the model
            if (!TryValidateModel(pointOfIntresToPatch))
            {
                return BadRequest(ModelState);
            }


            // If the update is valid...
            pointOfIntrestFromStore.Name = pointOfIntresToPatch.Name;
            pointOfIntrestFromStore.Description = pointOfIntresToPatch.Description;

            return NoContent();


        }

        [HttpDelete("{pointofintrestid}")]
        public ActionResult DeletePointOfIntrest(int cityId, int pointofintrestid)
        {
            // Find city
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound("City not found 😢");
            }

            // Find point of intrest
            var pointOfIntrestFromStore = city.PointsOfIntrest.FirstOrDefault(p => p.Id == pointofintrestid);

            if (pointOfIntrestFromStore == null)
            {
                return NotFound("Point of intrest not found 😢");
            }

            city.PointsOfIntrest.Remove(pointOfIntrestFromStore);
            return NoContent();
        }
    }


}