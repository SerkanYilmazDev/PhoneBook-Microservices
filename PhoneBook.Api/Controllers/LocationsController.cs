using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Api.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Api.Controllers
{
    [Route("api/locations")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly PhoneBookDbContext _dbContext;

        public LocationsController(PhoneBookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{locationId}")]
        public async Task<IActionResult> GetLocation(Guid locationId)
        {
            if (locationId == default)
                return BadRequest();

            var response = await _dbContext.Locations.FirstOrDefaultAsync(s => s.Id == locationId);

            return Ok(response);
        }

        [HttpGet("{locationName}/name")]
        public async Task<IActionResult> GetLocationByName(string locationName)
        {
            if (string.IsNullOrEmpty(locationName))
                return BadRequest();

            var response = await _dbContext.Locations.Where(s => s.LocationName == locationName).ToListAsync();

            return Ok(response);
        }
    }
}
