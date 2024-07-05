using CountryCRUDApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CountryCRUDApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TheCountryMasterController : ControllerBase
    {
        private readonly CountryDBContext _context;

        public TheCountryMasterController(CountryDBContext context)
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

        [HttpGet("GetById/{id}")]
        public IActionResult Get(int id)
        {
            if (id == 0)
            {
                return NotFound("Id Not found ");

            }
            else if(id == null)
            {
                return BadRequest("Id Should not Null");
            }
            var TheCountryMaster = _context.TheCountryMasters.Find(id);
            if (TheCountryMaster == null)
            {
                return NotFound("The CountryMaster is Null");
            }
            return Ok(TheCountryMaster);
        }
        [HttpPost]
        public IActionResult Post([FromBody] List<TheCountryMaster> models)
        {
            if (models == null)
            {
                return BadRequest("Model list is empty or null");
            }

            foreach (var model in models)
            {
                _context.Add(model);
            }

            _context.SaveChanges();
            return Ok("Models added successfully");
        }

        [HttpPost("Update")]
        public IActionResult Update([FromBody] List<TheCountryMaster> models)
        {
            if (models == null || !models.Any())
            {
                return BadRequest("Data should not be null or empty");
            }

            foreach (var model in models)
            {
                if (model.Id == 0)
                {
                    return BadRequest("There is no Id for one of the objects");
                }

                var TheCountryMaster = _context.TheCountryMasters.Find(model.Id);
                if (TheCountryMaster == null)
                {
                    return NotFound($"The CountryMaster with Id {model.Id} is not found");
                }

                TheCountryMaster.CountryCode = model.CountryCode;
                TheCountryMaster.Name = model.Name;
            }

            _context.SaveChanges();
            return Ok("Models updated successfully");
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete([FromBody] List<int> ids)
        {
            if (ids == null)
            {
                return BadRequest("ID list is empty or null");
            }

            foreach (var id in ids)
            {
                var model = _context.TheCountryMasters.Find(id);
                if (model == null)
                {
                    return NotFound($"Model with ID {id} not found");
                }
                _context.TheCountryMasters.Remove(model);
            }

            _context.SaveChanges();
            return Ok("Models deleted successfully");
        }


    }

}
