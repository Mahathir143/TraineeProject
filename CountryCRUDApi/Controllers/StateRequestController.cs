using CountryCRUDApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CountryCRUDApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateRequestController : ControllerBase
    {
        private readonly CountryDBContext _context;

        public StateRequestController(CountryDBContext context)
        {
            _context = context;
        }
        [HttpGet("GetAllTheCountryMaster")]
        public IActionResult Get()
        {
            var TheCountryMasters = _context.TheCountryMasters.ToList();
            if (TheCountryMasters.Count == 0)
            {
                return NotFound("No Data Found ");
            }
            return Ok(TheCountryMasters);
        }
    }
}
