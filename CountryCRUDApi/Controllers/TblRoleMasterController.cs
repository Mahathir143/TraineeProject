using CountryCRUDApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CountryCRUDApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TblRoleMasterController : ControllerBase
    {
        private readonly CountryDBContext _context;

        public TblRoleMasterController(CountryDBContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllRole")]
        public IActionResult Get()
        {
            var TblRoleMasters = _context.TblRoleMasters.ToList();
            if (TblRoleMasters.Count == 0)
            {
                return NotFound("No Data Found ");
            }
            return Ok(TblRoleMasters);
        }
        [HttpGet("GetById/{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                if(id < 0) {
                    return BadRequest($"TblRoleMaster Details Not Found {id}");
                }
                var TblRoleMaster = _context.TblRoleMasters.Find(id);
                if (TblRoleMaster != null)
                {
                    return NoContent();
                }
                return Ok(TblRoleMaster);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public IActionResult Post([FromBody] List<TblRoleMaster> models)
        {
            
            if (models == null || models.Count == 0)
            {
                return BadRequest("Data Models Not Found");
            }

            try
            {
                foreach (var model in models)
                {
                    _context.Add(model);
                }

                _context.SaveChanges();
                return Ok("Added Successfully");
            }
            catch (Exception ex)
            {


                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Update")]
        public IActionResult Update([FromBody] List<TblRoleMaster> models)
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

                var TblRoleMaster = _context.TblRoleMasters.Find(model.Id);
                if (TblRoleMaster == null)
                {
                    return NotFound($"The CountryMaster with Id {model.Id} is not found");
                }

                TblRoleMaster.RoleCode = model.RoleCode;
                TblRoleMaster.RoleName = model.RoleName;
                TblRoleMaster.Isactive = model.Isactive;
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
                var model = _context.TblRoleMasters.Find(id);
                if (model == null)
                {
                    return NotFound($"Model with ID {id} not found");
                }
                _context.TblRoleMasters.Remove(model);
            }

            _context.SaveChanges();
            return Ok("Models deleted successfully");
        }


    }

}
