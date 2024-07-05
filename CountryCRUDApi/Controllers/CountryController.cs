using CountryCRUDApi.Common;
using CountryCRUDApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CountryCRUDApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly CountryDBContext _context;

        public CountryController(CountryDBContext context)
        {
            _context = context;
        }
        [HttpGet("GetAllTheCountryMaster")]
        public IActionResult Get()
        {
            ApiResponse response = new ApiResponse()
;            var TheCountryMasters = _context.TblRoleMasters.ToList();
            if (TheCountryMasters.Count != 0)
            {
                response = new ApiResponse()
                {
                    StatusCode = StatusCodes.Status404NotFound.ToString(),
                    StatusMessage = "No Data Found",
                    Data = null
                };
                return Ok(response);
            }
            response = new ApiResponse()
            {
                StatusCode = StatusCodes.Status200OK.ToString(),
                StatusMessage = "successfully fetched country list",
                Data = TheCountryMasters
            };
            return Ok(response);
        }
    }
}
